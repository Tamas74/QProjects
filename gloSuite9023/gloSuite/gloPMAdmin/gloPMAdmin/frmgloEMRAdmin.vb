'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Imports Microsoft.Win32
Imports System.Data.SqlClient
Imports System.IO
Imports gloSSRSApplication
Imports gloEMR.Help
Imports gloServicesDatabase
Imports gloSettings

Public Class frmgloEMRAdmin
    Inherits System.Windows.Forms.Form

    Enum enmSelfNotesCategoryStatus
        Category
        Status
    End Enum

    Enum enmOperation
        Admin
        Audit
        Database
        Tools
    End Enum

    Structure struUser
        Dim nUserID As Int16
        Dim sUserName As String
    End Structure
    'Tooltip instance added to set tooltip on the Hide ToolBar button of the form
    Dim toolTip1 As New ToolTip()
    Dim enmUserOperation As enmOperation
    Dim trvSearchNode As TreeNode
    Friend WithEvents stbPnlBuild As System.Windows.Forms.StatusBarPanel
    Friend WithEvents stbPnlLoginTime As System.Windows.Forms.StatusBarPanel
    Friend WithEvents stbPnlDatabase As System.Windows.Forms.StatusBarPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblMainTop As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbtnWindowsGroupsUsers As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtngloEMRGroups As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnUserMGNT As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnClinic As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnProvider As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnMachine As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnSettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnRxReportDesigner As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnAuditReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnArchiveAudit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnArchivedReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnRestoreArchive As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnStartupSettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnLSAssociation As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnAboutUs As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnLockScreen As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnLogout As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtInstringSearch As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents splAudit As System.Windows.Forms.Splitter
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents SplitterMain As System.Windows.Forms.Splitter
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMainTop As System.Windows.Forms.Panel
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents pnlMainMainTop As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents SplitterMainCategory As System.Windows.Forms.Splitter
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents tsbtnClaimValidationSetting As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents tsbtnCMSSettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnUB04Setting As System.Windows.Forms.ToolStripButton
    Friend WithEvents HelpComponent1 As gloEMR.Help.HelpComponent
    Friend WithEvents tsb_UserGuide As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents pnlClientUpdateDetailsFilter As System.Windows.Forms.Panel
    Friend WithEvents btnSearchClientUpdate As System.Windows.Forms.Button
    Friend WithEvents txtSearchClientUpdate As System.Windows.Forms.TextBox
    Friend WithEvents lblClientUpdateSearch As System.Windows.Forms.Label
    Friend WithEvents cmbClientMachineName As System.Windows.Forms.ComboBox
    Friend WithEvents lblClientMachineName As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Private WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents tsbtnCMSSettingsNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCloneProvider As System.Windows.Forms.ToolStripButton
    ' Friend WithEvents tsbtnClose As System.Windows.Forms.ToolStripButton
    Dim clRights As New Collection
    Public Event HotKeyPressed As HotKeyPressedEventHandler
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        sethelpBuildermode()
        m_hotKeys = New HotKeyCollection(Me)
        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
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
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents trvAdminMenu As System.Windows.Forms.TreeView
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlMainMain As System.Windows.Forms.Panel
    Friend WithEvents dgData As clsDataGrid ' System.Windows.Forms.DataGrid
    Friend WithEvents trvCategory As System.Windows.Forms.TreeView
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents pnlCommand As System.Windows.Forms.Panel
    Friend WithEvents imglstMain As System.Windows.Forms.ImageList
    Friend WithEvents optSelfNotesCategory As System.Windows.Forms.RadioButton
    Friend WithEvents optSelfNotesStatus As System.Windows.Forms.RadioButton
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlCommandButtons As System.Windows.Forms.Panel
    Friend WithEvents btnHideToolBar As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents trvAudit As System.Windows.Forms.TreeView
    Friend WithEvents trvTools As System.Windows.Forms.TreeView
    Friend WithEvents pnlAudit As System.Windows.Forms.Panel
    Friend WithEvents lblAuditCategory As System.Windows.Forms.Label
    Friend WithEvents cmbAuditCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSearchPatient As System.Windows.Forms.ComboBox
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents imglstToolBar As System.Windows.Forms.ImageList
    Friend WithEvents stbPnlVersion As System.Windows.Forms.StatusBarPanel
    Friend WithEvents lblPatientID As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents imglstToolBar_New As System.Windows.Forms.ImageList
    Friend WithEvents imglstMain_New As System.Windows.Forms.ImageList
    Friend WithEvents btnShowAudit As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmgloEMRAdmin))
        Me.imglstToolBar_New = New System.Windows.Forms.ImageList(Me.components)
        Me.imglstToolBar = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.stbPnlLoginTime = New System.Windows.Forms.StatusBarPanel()
        Me.stbPnlVersion = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.stbPnlBuild = New System.Windows.Forms.StatusBarPanel()
        Me.pnlCommand = New System.Windows.Forms.Panel()
        Me.trvTools = New System.Windows.Forms.TreeView()
        Me.imglstMain_New = New System.Windows.Forms.ImageList(Me.components)
        Me.btnHideToolBar = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.pnlCommandButtons = New System.Windows.Forms.Panel()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.trvAudit = New System.Windows.Forms.TreeView()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.splAudit = New System.Windows.Forms.Splitter()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.trvAdminMenu = New System.Windows.Forms.TreeView()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.imglstMain = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlMainMain = New System.Windows.Forms.Panel()
        Me.dgData = New gloPMAdmin.clsDataGrid()
        Me.SplitterMainCategory = New System.Windows.Forms.Splitter()
        Me.trvCategory = New System.Windows.Forms.TreeView()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtInstringSearch = New System.Windows.Forms.TextBox()
        Me.btnShowAudit = New System.Windows.Forms.Button()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.cmbAuditCategory = New System.Windows.Forms.ComboBox()
        Me.lblAuditCategory = New System.Windows.Forms.Label()
        Me.pnlAudit = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblPatientID = New System.Windows.Forms.Label()
        Me.cmbSearchPatient = New System.Windows.Forms.ComboBox()
        Me.txtPatient = New System.Windows.Forms.TextBox()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.optSelfNotesStatus = New System.Windows.Forms.RadioButton()
        Me.optSelfNotesCategory = New System.Windows.Forms.RadioButton()
        Me.lblMainTop = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbtngloEMRGroups = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnWindowsGroupsUsers = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnUserMGNT = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnClinic = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnProvider = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnMachine = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnLSAssociation = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnClaimValidationSetting = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnSettings = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnRxReportDesigner = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnAuditReport = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnArchivedReport = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnArchiveAudit = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnCMSSettings = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnCMSSettingsNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnUB04Setting = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnRestoreArchive = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnStartupSettings = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnLockScreen = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnLogout = New System.Windows.Forms.ToolStripButton()
        Me.tsb_UserGuide = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnAboutUs = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitterMain = New System.Windows.Forms.Splitter()
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnModify = New System.Windows.Forms.ToolStripButton()
        Me.btnCloneProvider = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.pnlMainTop = New System.Windows.Forms.Panel()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.pnlMainMainTop = New System.Windows.Forms.Panel()
        Me.pnlClientUpdateDetailsFilter = New System.Windows.Forms.Panel()
        Me.btnSearchClientUpdate = New System.Windows.Forms.Button()
        Me.txtSearchClientUpdate = New System.Windows.Forms.TextBox()
        Me.lblClientUpdateSearch = New System.Windows.Forms.Label()
        Me.cmbClientMachineName = New System.Windows.Forms.ComboBox()
        Me.lblClientMachineName = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.HelpComponent1 = New gloEMR.Help.HelpComponent(Me.components)
        CType(Me.stbPnlLoginTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stbPnlVersion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stbPnlBuild, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCommand.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlMainMain.SuspendLayout()
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAudit.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.pnlMainTop.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.pnlMainMainTop.SuspendLayout()
        Me.pnlClientUpdateDetailsFilter.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.SuspendLayout()
        '
        'imglstToolBar_New
        '
        Me.imglstToolBar_New.ImageStream = CType(resources.GetObject("imglstToolBar_New.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglstToolBar_New.TransparentColor = System.Drawing.Color.Transparent
        Me.imglstToolBar_New.Images.SetKeyName(0, "")
        Me.imglstToolBar_New.Images.SetKeyName(1, "")
        Me.imglstToolBar_New.Images.SetKeyName(2, "")
        Me.imglstToolBar_New.Images.SetKeyName(3, "")
        Me.imglstToolBar_New.Images.SetKeyName(4, "")
        Me.imglstToolBar_New.Images.SetKeyName(5, "")
        Me.imglstToolBar_New.Images.SetKeyName(6, "")
        Me.imglstToolBar_New.Images.SetKeyName(7, "")
        Me.imglstToolBar_New.Images.SetKeyName(8, "")
        Me.imglstToolBar_New.Images.SetKeyName(9, "")
        Me.imglstToolBar_New.Images.SetKeyName(10, "")
        Me.imglstToolBar_New.Images.SetKeyName(11, "")
        Me.imglstToolBar_New.Images.SetKeyName(12, "")
        Me.imglstToolBar_New.Images.SetKeyName(13, "")
        Me.imglstToolBar_New.Images.SetKeyName(14, "")
        Me.imglstToolBar_New.Images.SetKeyName(15, "")
        Me.imglstToolBar_New.Images.SetKeyName(16, "")
        Me.imglstToolBar_New.Images.SetKeyName(17, "")
        Me.imglstToolBar_New.Images.SetKeyName(18, "")
        Me.imglstToolBar_New.Images.SetKeyName(19, "")
        Me.imglstToolBar_New.Images.SetKeyName(20, "")
        Me.imglstToolBar_New.Images.SetKeyName(21, "")
        Me.imglstToolBar_New.Images.SetKeyName(22, "")
        Me.imglstToolBar_New.Images.SetKeyName(23, "")
        Me.imglstToolBar_New.Images.SetKeyName(24, "")
        Me.imglstToolBar_New.Images.SetKeyName(25, "")
        Me.imglstToolBar_New.Images.SetKeyName(26, "")
        Me.imglstToolBar_New.Images.SetKeyName(27, "")
        Me.imglstToolBar_New.Images.SetKeyName(28, "")
        Me.imglstToolBar_New.Images.SetKeyName(29, "")
        '
        'imglstToolBar
        '
        Me.imglstToolBar.ImageStream = CType(resources.GetObject("imglstToolBar.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglstToolBar.TransparentColor = System.Drawing.Color.Transparent
        Me.imglstToolBar.Images.SetKeyName(0, "Administrator.ico")
        Me.imglstToolBar.Images.SetKeyName(1, "gloEMR Group.ico")
        Me.imglstToolBar.Images.SetKeyName(2, "User management.ico")
        Me.imglstToolBar.Images.SetKeyName(3, "Clinic.ico")
        Me.imglstToolBar.Images.SetKeyName(4, "DoctorType.ico")
        Me.imglstToolBar.Images.SetKeyName(5, "Doctor.ico")
        Me.imglstToolBar.Images.SetKeyName(6, "Doctor User Task Configuration.ico")
        Me.imglstToolBar.Images.SetKeyName(7, "Junior  senior Doctor  Association.ico")
        Me.imglstToolBar.Images.SetKeyName(8, "Doctor Referral Letter Settings.ico")
        Me.imglstToolBar.Images.SetKeyName(9, "Client Settings.ico")
        Me.imglstToolBar.Images.SetKeyName(10, "Clinic Workflow Settings.ico")
        Me.imglstToolBar.Images.SetKeyName(11, "User Group.ico")
        Me.imglstToolBar.Images.SetKeyName(12, "Clearing House.ico")
        Me.imglstToolBar.Images.SetKeyName(13, "")
        Me.imglstToolBar.Images.SetKeyName(14, "")
        Me.imglstToolBar.Images.SetKeyName(15, "")
        Me.imglstToolBar.Images.SetKeyName(16, "")
        Me.imglstToolBar.Images.SetKeyName(17, "")
        Me.imglstToolBar.Images.SetKeyName(18, "")
        Me.imglstToolBar.Images.SetKeyName(19, "")
        Me.imglstToolBar.Images.SetKeyName(20, "")
        Me.imglstToolBar.Images.SetKeyName(21, "")
        Me.imglstToolBar.Images.SetKeyName(22, "")
        Me.imglstToolBar.Images.SetKeyName(23, "")
        Me.imglstToolBar.Images.SetKeyName(24, "")
        Me.imglstToolBar.Images.SetKeyName(25, "")
        Me.imglstToolBar.Images.SetKeyName(26, "")
        Me.imglstToolBar.Images.SetKeyName(27, "")
        Me.imglstToolBar.Images.SetKeyName(28, "")
        Me.imglstToolBar.Images.SetKeyName(29, "")
        Me.imglstToolBar.Images.SetKeyName(30, "")
        Me.imglstToolBar.Images.SetKeyName(31, "")
        Me.imglstToolBar.Images.SetKeyName(32, "")
        Me.imglstToolBar.Images.SetKeyName(33, "")
        Me.imglstToolBar.Images.SetKeyName(34, "")
        Me.imglstToolBar.Images.SetKeyName(35, "")
        Me.imglstToolBar.Images.SetKeyName(36, "")
        Me.imglstToolBar.Images.SetKeyName(37, "")
        Me.imglstToolBar.Images.SetKeyName(38, "")
        Me.imglstToolBar.Images.SetKeyName(39, "")
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 844)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.stbPnlLoginTime, Me.stbPnlVersion, Me.StatusBarPanel1, Me.stbPnlBuild})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1192, 22)
        Me.StatusBar1.TabIndex = 6
        Me.StatusBar1.Text = "StatusBar1"
        '
        'stbPnlLoginTime
        '
        Me.stbPnlLoginTime.Icon = CType(resources.GetObject("stbPnlLoginTime.Icon"), System.Drawing.Icon)
        Me.stbPnlLoginTime.Name = "stbPnlLoginTime"
        Me.stbPnlLoginTime.Width = 250
        '
        'stbPnlVersion
        '
        Me.stbPnlVersion.Icon = CType(resources.GetObject("stbPnlVersion.Icon"), System.Drawing.Icon)
        Me.stbPnlVersion.Name = "stbPnlVersion"
        Me.stbPnlVersion.Width = 150
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.Icon = CType(resources.GetObject("StatusBarPanel1.Icon"), System.Drawing.Icon)
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 350
        '
        'stbPnlBuild
        '
        Me.stbPnlBuild.Icon = CType(resources.GetObject("stbPnlBuild.Icon"), System.Drawing.Icon)
        Me.stbPnlBuild.Name = "stbPnlBuild"
        Me.stbPnlBuild.Width = 270
        '
        'pnlCommand
        '
        Me.pnlCommand.BackColor = System.Drawing.Color.White
        Me.pnlCommand.BackgroundImage = CType(resources.GetObject("pnlCommand.BackgroundImage"), System.Drawing.Image)
        Me.pnlCommand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCommand.Controls.Add(Me.trvTools)
        Me.pnlCommand.Controls.Add(Me.btnHideToolBar)
        Me.pnlCommand.Controls.Add(Me.btnHelp)
        Me.pnlCommand.Controls.Add(Me.pnlCommandButtons)
        Me.pnlCommand.Location = New System.Drawing.Point(313, 649)
        Me.pnlCommand.Name = "pnlCommand"
        Me.pnlCommand.Size = New System.Drawing.Size(693, 25)
        Me.pnlCommand.TabIndex = 12
        Me.pnlCommand.Visible = False
        '
        'trvTools
        '
        Me.trvTools.BackColor = System.Drawing.Color.GhostWhite
        Me.trvTools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.trvTools.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvTools.HideSelection = False
        Me.trvTools.ImageIndex = 0
        Me.trvTools.ImageList = Me.imglstMain_New
        Me.trvTools.Location = New System.Drawing.Point(185, 3)
        Me.trvTools.Name = "trvTools"
        Me.trvTools.SelectedImageIndex = 0
        Me.trvTools.ShowLines = False
        Me.trvTools.Size = New System.Drawing.Size(200, 17)
        Me.trvTools.TabIndex = 39
        Me.trvTools.Visible = False
        '
        'imglstMain_New
        '
        Me.imglstMain_New.ImageStream = CType(resources.GetObject("imglstMain_New.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglstMain_New.TransparentColor = System.Drawing.Color.Transparent
        Me.imglstMain_New.Images.SetKeyName(0, "Administrator.ico")
        Me.imglstMain_New.Images.SetKeyName(1, "Window Group & User_01.ico")
        Me.imglstMain_New.Images.SetKeyName(2, "gloEMR Group.ico")
        Me.imglstMain_New.Images.SetKeyName(3, "User management.ico")
        Me.imglstMain_New.Images.SetKeyName(4, "Clinic.ico")
        Me.imglstMain_New.Images.SetKeyName(5, "Provider Type.ico")
        Me.imglstMain_New.Images.SetKeyName(6, "DoctorType.ico")
        Me.imglstMain_New.Images.SetKeyName(7, "Doctor User Task Configuration.ico")
        Me.imglstMain_New.Images.SetKeyName(8, "Junior  senior Doctor  Association.ico")
        Me.imglstMain_New.Images.SetKeyName(9, "Doctor Referral Letter Settings.ico")
        Me.imglstMain_New.Images.SetKeyName(10, "Client Settings.ico")
        Me.imglstMain_New.Images.SetKeyName(11, "Clinic Workflow Settings.ico")
        Me.imglstMain_New.Images.SetKeyName(12, "User Group.ico")
        Me.imglstMain_New.Images.SetKeyName(13, "Clearing House.ico")
        Me.imglstMain_New.Images.SetKeyName(14, "Tools1.ico")
        Me.imglstMain_New.Images.SetKeyName(15, "Admin login.ico")
        Me.imglstMain_New.Images.SetKeyName(16, "Claim Validation Settings.ico")
        Me.imglstMain_New.Images.SetKeyName(17, "CMS Printer Settings.ico")
        Me.imglstMain_New.Images.SetKeyName(18, "Audit lOG.ico")
        Me.imglstMain_New.Images.SetKeyName(19, "Open report.ico")
        Me.imglstMain_New.Images.SetKeyName(20, "Archive.ico")
        Me.imglstMain_New.Images.SetKeyName(21, "Archived Audit log.ico")
        Me.imglstMain_New.Images.SetKeyName(22, "Archive Audit Report.ico")
        Me.imglstMain_New.Images.SetKeyName(23, "Restore Archive.ico")
        Me.imglstMain_New.Images.SetKeyName(24, "User Group.ico")
        Me.imglstMain_New.Images.SetKeyName(25, "Payer ID.ico")
        Me.imglstMain_New.Images.SetKeyName(26, "Import Fee Schedule.ico")
        Me.imglstMain_New.Images.SetKeyName(27, "Settings1.ico")
        Me.imglstMain_New.Images.SetKeyName(28, "Doctor Speaker.ico")
        Me.imglstMain_New.Images.SetKeyName(29, "Prefix.ico")
        Me.imglstMain_New.Images.SetKeyName(30, "SSRS Report.ico")
        Me.imglstMain_New.Images.SetKeyName(31, "Deploy SSRS Files.ico")
        Me.imglstMain_New.Images.SetKeyName(32, "UB04 Setting.ico")
        Me.imglstMain_New.Images.SetKeyName(33, "Merge Insurance plans.ico")
        Me.imglstMain_New.Images.SetKeyName(34, "Database Criediantial.ico")
        Me.imglstMain_New.Images.SetKeyName(35, "Flag-blue.ico")
        Me.imglstMain_New.Images.SetKeyName(36, "")
        Me.imglstMain_New.Images.SetKeyName(37, "Business Center Rules Setup.ico")
        Me.imglstMain_New.Images.SetKeyName(38, "Account Business Center Utility.ico")
        Me.imglstMain_New.Images.SetKeyName(39, "Worker Comp Billing Utility.ico")
        Me.imglstMain_New.Images.SetKeyName(40, "Delete Follow-up.ico")
        Me.imglstMain_New.Images.SetKeyName(41, "Client Update.ico")
        Me.imglstMain_New.Images.SetKeyName(42, "AdvanceInsuranceMerge.ico")
        '
        'btnHideToolBar
        '
        Me.btnHideToolBar.BackgroundImage = CType(resources.GetObject("btnHideToolBar.BackgroundImage"), System.Drawing.Image)
        Me.btnHideToolBar.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnHideToolBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHideToolBar.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHideToolBar.ForeColor = System.Drawing.Color.Black
        Me.btnHideToolBar.Location = New System.Drawing.Point(0, 0)
        Me.btnHideToolBar.Name = "btnHideToolBar"
        Me.btnHideToolBar.Size = New System.Drawing.Size(112, 23)
        Me.btnHideToolBar.TabIndex = 1
        Me.btnHideToolBar.Text = "Hide Toolbar"
        Me.btnHideToolBar.Visible = False
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.btnHelp.BackgroundImage = CType(resources.GetObject("btnHelp.BackgroundImage"), System.Drawing.Image)
        Me.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHelp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.Location = New System.Drawing.Point(0, 589)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(203, 24)
        Me.btnHelp.TabIndex = 33
        Me.btnHelp.Text = "Help"
        Me.btnHelp.UseVisualStyleBackColor = False
        Me.btnHelp.Visible = False
        '
        'pnlCommandButtons
        '
        Me.pnlCommandButtons.BackColor = System.Drawing.Color.Transparent
        Me.pnlCommandButtons.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlCommandButtons.Location = New System.Drawing.Point(403, 0)
        Me.pnlCommandButtons.Name = "pnlCommandButtons"
        Me.pnlCommandButtons.Size = New System.Drawing.Size(288, 23)
        Me.pnlCommandButtons.TabIndex = 0
        Me.pnlCommandButtons.Visible = False
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLeft.Controls.Add(Me.Panel7)
        Me.pnlLeft.Controls.Add(Me.Panel5)
        Me.pnlLeft.Controls.Add(Me.splAudit)
        Me.pnlLeft.Controls.Add(Me.Panel3)
        Me.pnlLeft.Controls.Add(Me.Panel4)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 56)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(259, 788)
        Me.pnlLeft.TabIndex = 13
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.trvAudit)
        Me.Panel7.Controls.Add(Me.Label12)
        Me.Panel7.Controls.Add(Me.Label17)
        Me.Panel7.Controls.Add(Me.Label18)
        Me.Panel7.Controls.Add(Me.Label19)
        Me.Panel7.Controls.Add(Me.Label20)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(0, 609)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel7.Size = New System.Drawing.Size(259, 179)
        Me.Panel7.TabIndex = 49
        '
        'trvAudit
        '
        Me.trvAudit.BackColor = System.Drawing.Color.GhostWhite
        Me.trvAudit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAudit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAudit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvAudit.ForeColor = System.Drawing.Color.Black
        Me.trvAudit.HideSelection = False
        Me.trvAudit.ImageIndex = 0
        Me.trvAudit.ImageList = Me.imglstMain_New
        Me.trvAudit.ItemHeight = 21
        Me.trvAudit.Location = New System.Drawing.Point(4, 5)
        Me.trvAudit.Name = "trvAudit"
        Me.trvAudit.SelectedImageIndex = 0
        Me.trvAudit.ShowLines = False
        Me.trvAudit.Size = New System.Drawing.Size(254, 170)
        Me.trvAudit.TabIndex = 35
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(4, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(254, 4)
        Me.Label12.TabIndex = 38
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(4, 175)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(254, 1)
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
        Me.Label18.Size = New System.Drawing.Size(1, 175)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(258, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 175)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "label3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(3, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(256, 1)
        Me.Label20.TabIndex = 5
        Me.Label20.Text = "label1"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 581)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel5.Size = New System.Drawing.Size(259, 28)
        Me.Panel5.TabIndex = 48
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.BackgroundImage = CType(resources.GetObject("Panel6.BackgroundImage"), System.Drawing.Image)
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Label2)
        Me.Panel6.Controls.Add(Me.Label13)
        Me.Panel6.Controls.Add(Me.Label14)
        Me.Panel6.Controls.Add(Me.Label15)
        Me.Panel6.Controls.Add(Me.Label16)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel6.Location = New System.Drawing.Point(3, 0)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(256, 25)
        Me.Panel6.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(254, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Audit"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(1, 24)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(254, 1)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 24)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(255, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 24)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(256, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label1"
        '
        'splAudit
        '
        Me.splAudit.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.splAudit.Dock = System.Windows.Forms.DockStyle.Top
        Me.splAudit.Location = New System.Drawing.Point(0, 578)
        Me.splAudit.Name = "splAudit"
        Me.splAudit.Size = New System.Drawing.Size(259, 3)
        Me.splAudit.TabIndex = 47
        Me.splAudit.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.trvAdminMenu)
        Me.Panel3.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 28)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel3.Size = New System.Drawing.Size(259, 550)
        Me.Panel3.TabIndex = 46
        '
        'trvAdminMenu
        '
        Me.trvAdminMenu.BackColor = System.Drawing.Color.White
        Me.trvAdminMenu.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAdminMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAdminMenu.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvAdminMenu.ForeColor = System.Drawing.Color.Black
        Me.trvAdminMenu.HideSelection = False
        Me.trvAdminMenu.ImageIndex = 0
        Me.trvAdminMenu.ImageList = Me.imglstMain_New
        Me.trvAdminMenu.ItemHeight = 21
        Me.trvAdminMenu.Location = New System.Drawing.Point(4, 5)
        Me.trvAdminMenu.Name = "trvAdminMenu"
        Me.trvAdminMenu.SelectedImageIndex = 0
        Me.trvAdminMenu.ShowLines = False
        Me.trvAdminMenu.Size = New System.Drawing.Size(254, 544)
        Me.trvAdminMenu.TabIndex = 3
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(4, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(254, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 38
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(4, 549)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(254, 1)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 549)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(258, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 549)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "label3"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(256, 1)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(259, 28)
        Me.Panel4.TabIndex = 45
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(256, 25)
        Me.Panel2.TabIndex = 19
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(254, 1)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(186, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Administrator"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(1, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(254, 1)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "label2"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 25)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(255, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 25)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "label3"
        '
        'imglstMain
        '
        Me.imglstMain.ImageStream = CType(resources.GetObject("imglstMain.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglstMain.TransparentColor = System.Drawing.Color.Transparent
        Me.imglstMain.Images.SetKeyName(0, "gloEMR Group.ico")
        Me.imglstMain.Images.SetKeyName(1, "User management.ico")
        Me.imglstMain.Images.SetKeyName(2, "Clinic.ico")
        Me.imglstMain.Images.SetKeyName(3, "Provider Type.ico")
        Me.imglstMain.Images.SetKeyName(4, "DoctorType.ico")
        Me.imglstMain.Images.SetKeyName(5, "Doctor User Task Configuration.ico")
        Me.imglstMain.Images.SetKeyName(6, "Junior  senior Doctor  Association.ico")
        Me.imglstMain.Images.SetKeyName(7, "Doctor Referral Letter Settings.ico")
        Me.imglstMain.Images.SetKeyName(8, "Client Settings.ico")
        Me.imglstMain.Images.SetKeyName(9, "Clinic Workflow Settings.ico")
        Me.imglstMain.Images.SetKeyName(10, "User Group.ico")
        Me.imglstMain.Images.SetKeyName(11, "Tools1.ico")
        Me.imglstMain.Images.SetKeyName(12, "Audit lOG.ico")
        Me.imglstMain.Images.SetKeyName(13, "Archive Audit Report.ico")
        Me.imglstMain.Images.SetKeyName(14, "Bullet06.ico")
        Me.imglstMain.Images.SetKeyName(15, "Doctor Speaker.ico")
        Me.imglstMain.Images.SetKeyName(16, "Block Doctor.ico")
        Me.imglstMain.Images.SetKeyName(17, "Active Doctor.ico")
        Me.imglstMain.Images.SetKeyName(18, "ProviderDisable.ico")
        Me.imglstMain.Images.SetKeyName(19, "ProviderReview.ico")
        Me.imglstMain.Images.SetKeyName(20, "ProivderPendingLicense.ico")
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.pnlMainMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(263, 167)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(929, 677)
        Me.pnlMain.TabIndex = 15
        '
        'pnlMainMain
        '
        Me.pnlMainMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMainMain.Controls.Add(Me.dgData)
        Me.pnlMainMain.Controls.Add(Me.SplitterMainCategory)
        Me.pnlMainMain.Controls.Add(Me.trvCategory)
        Me.pnlMainMain.Controls.Add(Me.Label25)
        Me.pnlMainMain.Controls.Add(Me.Label26)
        Me.pnlMainMain.Controls.Add(Me.Label27)
        Me.pnlMainMain.Controls.Add(Me.Label28)
        Me.pnlMainMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMainMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMainMain.Name = "pnlMainMain"
        Me.pnlMainMain.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlMainMain.Size = New System.Drawing.Size(929, 677)
        Me.pnlMainMain.TabIndex = 1
        '
        'dgData
        '
        Me.dgData.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgData.BackColor = System.Drawing.Color.White
        Me.dgData.BackgroundColor = System.Drawing.Color.White
        Me.dgData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgData.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgData.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgData.CaptionForeColor = System.Drawing.Color.White
        Me.dgData.CaptionVisible = False
        Me.dgData.DataMember = ""
        Me.dgData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgData.FullRowSelect = True
        Me.dgData.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgData.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgData.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgData.HeaderForeColor = System.Drawing.Color.White
        Me.dgData.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgData.Location = New System.Drawing.Point(270, 1)
        Me.dgData.Name = "dgData"
        Me.dgData.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgData.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgData.ReadOnly = True
        Me.dgData.RowHeadersVisible = False
        Me.dgData.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.dgData.SelectionForeColor = System.Drawing.Color.Black
        Me.dgData.Size = New System.Drawing.Size(655, 672)
        Me.dgData.TabIndex = 10
        Me.dgData.Visible = False
        '
        'SplitterMainCategory
        '
        Me.SplitterMainCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.SplitterMainCategory.Location = New System.Drawing.Point(269, 1)
        Me.SplitterMainCategory.Name = "SplitterMainCategory"
        Me.SplitterMainCategory.Size = New System.Drawing.Size(1, 672)
        Me.SplitterMainCategory.TabIndex = 15
        Me.SplitterMainCategory.TabStop = False
        '
        'trvCategory
        '
        Me.trvCategory.BackColor = System.Drawing.Color.White
        Me.trvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCategory.Dock = System.Windows.Forms.DockStyle.Left
        Me.trvCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCategory.ForeColor = System.Drawing.Color.Black
        Me.trvCategory.HideSelection = False
        Me.trvCategory.ImageIndex = 0
        Me.trvCategory.ImageList = Me.imglstMain
        Me.trvCategory.Indent = 20
        Me.trvCategory.ItemHeight = 20
        Me.trvCategory.Location = New System.Drawing.Point(1, 1)
        Me.trvCategory.Name = "trvCategory"
        Me.trvCategory.SelectedImageIndex = 0
        Me.trvCategory.ShowLines = False
        Me.trvCategory.Size = New System.Drawing.Size(268, 672)
        Me.trvCategory.TabIndex = 8
        Me.trvCategory.Visible = False
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(1, 673)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(924, 1)
        Me.Label25.TabIndex = 14
        Me.Label25.Text = "label2"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 673)
        Me.Label26.TabIndex = 13
        Me.Label26.Text = "label4"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(925, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 673)
        Me.Label27.TabIndex = 12
        Me.Label27.Text = "label3"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(0, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(926, 1)
        Me.Label28.TabIndex = 11
        Me.Label28.Text = "label1"
        '
        'txtInstringSearch
        '
        Me.txtInstringSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtInstringSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstringSearch.ForeColor = System.Drawing.Color.Black
        Me.txtInstringSearch.Location = New System.Drawing.Point(710, 1)
        Me.txtInstringSearch.Name = "txtInstringSearch"
        Me.txtInstringSearch.Size = New System.Drawing.Size(133, 22)
        Me.txtInstringSearch.TabIndex = 1
        '
        'btnShowAudit
        '
        Me.btnShowAudit.BackColor = System.Drawing.Color.Transparent
        Me.btnShowAudit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnShowAudit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnShowAudit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnShowAudit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowAudit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowAudit.Location = New System.Drawing.Point(858, 0)
        Me.btnShowAudit.Name = "btnShowAudit"
        Me.btnShowAudit.Size = New System.Drawing.Size(58, 24)
        Me.btnShowAudit.TabIndex = 4
        Me.btnShowAudit.Text = "Show"
        Me.btnShowAudit.UseVisualStyleBackColor = False
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(559, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSearch.Size = New System.Drawing.Size(151, 22)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "Software Component :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAuditCategory
        '
        Me.cmbAuditCategory.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbAuditCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAuditCategory.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAuditCategory.Location = New System.Drawing.Point(78, 1)
        Me.cmbAuditCategory.Name = "cmbAuditCategory"
        Me.cmbAuditCategory.Size = New System.Drawing.Size(150, 22)
        Me.cmbAuditCategory.TabIndex = 8
        Me.cmbAuditCategory.Visible = False
        '
        'lblAuditCategory
        '
        Me.lblAuditCategory.AutoSize = True
        Me.lblAuditCategory.BackColor = System.Drawing.Color.Transparent
        Me.lblAuditCategory.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblAuditCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuditCategory.Location = New System.Drawing.Point(1, 1)
        Me.lblAuditCategory.Name = "lblAuditCategory"
        Me.lblAuditCategory.Padding = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.lblAuditCategory.Size = New System.Drawing.Size(77, 21)
        Me.lblAuditCategory.TabIndex = 7
        Me.lblAuditCategory.Text = "Category :"
        Me.lblAuditCategory.Visible = False
        '
        'pnlAudit
        '
        Me.pnlAudit.BackColor = System.Drawing.Color.Transparent
        Me.pnlAudit.Controls.Add(Me.Label3)
        Me.pnlAudit.Controls.Add(Me.lblPatientID)
        Me.pnlAudit.Controls.Add(Me.cmbSearchPatient)
        Me.pnlAudit.Controls.Add(Me.txtPatient)
        Me.pnlAudit.Location = New System.Drawing.Point(861, 197)
        Me.pnlAudit.Name = "pnlAudit"
        Me.pnlAudit.Size = New System.Drawing.Size(358, 32)
        Me.pnlAudit.TabIndex = 6
        Me.pnlAudit.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(116, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = ":"
        '
        'lblPatientID
        '
        Me.lblPatientID.AutoSize = True
        Me.lblPatientID.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientID.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientID.Location = New System.Drawing.Point(51, 7)
        Me.lblPatientID.Name = "lblPatientID"
        Me.lblPatientID.Size = New System.Drawing.Size(70, 14)
        Me.lblPatientID.TabIndex = 3
        Me.lblPatientID.Text = "Patient ID"
        Me.lblPatientID.Visible = False
        '
        'cmbSearchPatient
        '
        Me.cmbSearchPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearchPatient.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearchPatient.Items.AddRange(New Object() {"Patient Code", "First Name", "Last Name"})
        Me.cmbSearchPatient.Location = New System.Drawing.Point(5, 4)
        Me.cmbSearchPatient.Name = "cmbSearchPatient"
        Me.cmbSearchPatient.Size = New System.Drawing.Size(105, 22)
        Me.cmbSearchPatient.TabIndex = 2
        '
        'txtPatient
        '
        Me.txtPatient.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPatient.Location = New System.Drawing.Point(129, 3)
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.Size = New System.Drawing.Size(103, 22)
        Me.txtPatient.TabIndex = 1
        '
        'dtTo
        '
        Me.dtTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTo.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(455, 1)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(104, 22)
        Me.dtTo.TabIndex = 5
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.Color.Transparent
        Me.lblTo.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(399, 1)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Padding = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.lblTo.Size = New System.Drawing.Size(56, 21)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "     To :"
        '
        'dtFrom
        '
        Me.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(295, 1)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(104, 22)
        Me.dtFrom.TabIndex = 3
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(228, 1)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Padding = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.lblFrom.Size = New System.Drawing.Size(67, 21)
        Me.lblFrom.TabIndex = 2
        Me.lblFrom.Text = "    From :"
        '
        'optSelfNotesStatus
        '
        Me.optSelfNotesStatus.BackColor = System.Drawing.Color.Transparent
        Me.optSelfNotesStatus.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSelfNotesStatus.Location = New System.Drawing.Point(259, 0)
        Me.optSelfNotesStatus.Name = "optSelfNotesStatus"
        Me.optSelfNotesStatus.Size = New System.Drawing.Size(80, 24)
        Me.optSelfNotesStatus.TabIndex = 1
        Me.optSelfNotesStatus.Text = "Status"
        Me.optSelfNotesStatus.UseVisualStyleBackColor = False
        Me.optSelfNotesStatus.Visible = False
        '
        'optSelfNotesCategory
        '
        Me.optSelfNotesCategory.BackColor = System.Drawing.Color.Transparent
        Me.optSelfNotesCategory.Checked = True
        Me.optSelfNotesCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSelfNotesCategory.Location = New System.Drawing.Point(20, -1)
        Me.optSelfNotesCategory.Name = "optSelfNotesCategory"
        Me.optSelfNotesCategory.Size = New System.Drawing.Size(154, 24)
        Me.optSelfNotesCategory.TabIndex = 0
        Me.optSelfNotesCategory.TabStop = True
        Me.optSelfNotesCategory.Text = "Category"
        Me.optSelfNotesCategory.UseVisualStyleBackColor = False
        Me.optSelfNotesCategory.Visible = False
        '
        'lblMainTop
        '
        Me.lblMainTop.BackColor = System.Drawing.Color.Transparent
        Me.lblMainTop.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblMainTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainTop.Location = New System.Drawing.Point(1, 1)
        Me.lblMainTop.Name = "lblMainTop"
        Me.lblMainTop.Size = New System.Drawing.Size(504, 23)
        Me.lblMainTop.TabIndex = 0
        Me.lblMainTop.Text = "   "
        Me.lblMainTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtngloEMRGroups, Me.tsbtnWindowsGroupsUsers, Me.tsbtnUserMGNT, Me.tsbtnClinic, Me.tsbtnProvider, Me.tsbtnMachine, Me.tsbtnLSAssociation, Me.tsbtnClaimValidationSetting, Me.tsbtnSettings, Me.tsbtnRxReportDesigner, Me.tsbtnAuditReport, Me.tsbtnArchivedReport, Me.tsbtnArchiveAudit, Me.tsbtnCMSSettings, Me.tsbtnCMSSettingsNew, Me.tsbtnUB04Setting, Me.tsbtnRestoreArchive, Me.tsbtnStartupSettings, Me.tsbtnLockScreen, Me.tsbtnLogout, Me.tsb_UserGuide, Me.tsbtnAboutUs, Me.tsbtnClose})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1192, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbtngloEMRGroups
        '
        Me.tsbtngloEMRGroups.BackgroundImage = CType(resources.GetObject("tsbtngloEMRGroups.BackgroundImage"), System.Drawing.Image)
        Me.tsbtngloEMRGroups.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtngloEMRGroups.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtngloEMRGroups.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtngloEMRGroups.Image = CType(resources.GetObject("tsbtngloEMRGroups.Image"), System.Drawing.Image)
        Me.tsbtngloEMRGroups.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtngloEMRGroups.Name = "tsbtngloEMRGroups"
        Me.tsbtngloEMRGroups.Size = New System.Drawing.Size(74, 50)
        Me.tsbtngloEMRGroups.Tag = "gloEMRGroups"
        Me.tsbtngloEMRGroups.Text = "QEMRGrps"
        Me.tsbtngloEMRGroups.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtngloEMRGroups.ToolTipText = "QEMR Groups"
        '
        'tsbtnWindowsGroupsUsers
        '
        Me.tsbtnWindowsGroupsUsers.BackgroundImage = CType(resources.GetObject("tsbtnWindowsGroupsUsers.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnWindowsGroupsUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnWindowsGroupsUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnWindowsGroupsUsers.Image = CType(resources.GetObject("tsbtnWindowsGroupsUsers.Image"), System.Drawing.Image)
        Me.tsbtnWindowsGroupsUsers.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnWindowsGroupsUsers.Name = "tsbtnWindowsGroupsUsers"
        Me.tsbtnWindowsGroupsUsers.Size = New System.Drawing.Size(80, 50)
        Me.tsbtnWindowsGroupsUsers.Tag = "WindowsGroupsUsers"
        Me.tsbtnWindowsGroupsUsers.Text = "WinUsrGrps"
        Me.tsbtnWindowsGroupsUsers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnWindowsGroupsUsers.ToolTipText = "Windows User Groups"
        Me.tsbtnWindowsGroupsUsers.Visible = False
        '
        'tsbtnUserMGNT
        '
        Me.tsbtnUserMGNT.BackgroundImage = CType(resources.GetObject("tsbtnUserMGNT.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnUserMGNT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnUserMGNT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnUserMGNT.Image = CType(resources.GetObject("tsbtnUserMGNT.Image"), System.Drawing.Image)
        Me.tsbtnUserMGNT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnUserMGNT.Name = "tsbtnUserMGNT"
        Me.tsbtnUserMGNT.Size = New System.Drawing.Size(56, 50)
        Me.tsbtnUserMGNT.Tag = "UserMGNT"
        Me.tsbtnUserMGNT.Text = "UsrMGT"
        Me.tsbtnUserMGNT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnUserMGNT.ToolTipText = "User Management"
        '
        'tsbtnClinic
        '
        Me.tsbtnClinic.BackgroundImage = CType(resources.GetObject("tsbtnClinic.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnClinic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnClinic.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnClinic.Image = CType(resources.GetObject("tsbtnClinic.Image"), System.Drawing.Image)
        Me.tsbtnClinic.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnClinic.Name = "tsbtnClinic"
        Me.tsbtnClinic.Size = New System.Drawing.Size(42, 50)
        Me.tsbtnClinic.Tag = "Clinic"
        Me.tsbtnClinic.Text = "Clinic"
        Me.tsbtnClinic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnClinic.ToolTipText = "Clinic Information"
        '
        'tsbtnProvider
        '
        Me.tsbtnProvider.BackgroundImage = CType(resources.GetObject("tsbtnProvider.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnProvider.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnProvider.Image = CType(resources.GetObject("tsbtnProvider.Image"), System.Drawing.Image)
        Me.tsbtnProvider.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnProvider.Name = "tsbtnProvider"
        Me.tsbtnProvider.Size = New System.Drawing.Size(62, 50)
        Me.tsbtnProvider.Tag = "Provider"
        Me.tsbtnProvider.Text = "Provider"
        Me.tsbtnProvider.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnProvider.ToolTipText = "Provider"
        '
        'tsbtnMachine
        '
        Me.tsbtnMachine.BackgroundImage = CType(resources.GetObject("tsbtnMachine.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnMachine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnMachine.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnMachine.Image = CType(resources.GetObject("tsbtnMachine.Image"), System.Drawing.Image)
        Me.tsbtnMachine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnMachine.Name = "tsbtnMachine"
        Me.tsbtnMachine.Size = New System.Drawing.Size(46, 50)
        Me.tsbtnMachine.Tag = "Machines"
        Me.tsbtnMachine.Text = "Client"
        Me.tsbtnMachine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnMachine.ToolTipText = "Client Settings"
        '
        'tsbtnLSAssociation
        '
        Me.tsbtnLSAssociation.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnLSAssociation.BackgroundImage = CType(resources.GetObject("tsbtnLSAssociation.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnLSAssociation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnLSAssociation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnLSAssociation.Image = CType(resources.GetObject("tsbtnLSAssociation.Image"), System.Drawing.Image)
        Me.tsbtnLSAssociation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnLSAssociation.Name = "tsbtnLSAssociation"
        Me.tsbtnLSAssociation.Size = New System.Drawing.Size(60, 50)
        Me.tsbtnLSAssociation.Tag = "LSAssociation"
        Me.tsbtnLSAssociation.Text = "ClinicWF"
        Me.tsbtnLSAssociation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnLSAssociation.ToolTipText = "Clinic WorkFlow Settings"
        '
        'tsbtnClaimValidationSetting
        '
        Me.tsbtnClaimValidationSetting.Image = CType(resources.GetObject("tsbtnClaimValidationSetting.Image"), System.Drawing.Image)
        Me.tsbtnClaimValidationSetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnClaimValidationSetting.Name = "tsbtnClaimValidationSetting"
        Me.tsbtnClaimValidationSetting.Size = New System.Drawing.Size(87, 50)
        Me.tsbtnClaimValidationSetting.Tag = "Claim Validation Setting"
        Me.tsbtnClaimValidationSetting.Text = "CLM Setting"
        Me.tsbtnClaimValidationSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnClaimValidationSetting.ToolTipText = "Claim Validation Setting"
        '
        'tsbtnSettings
        '
        Me.tsbtnSettings.BackgroundImage = CType(resources.GetObject("tsbtnSettings.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnSettings.Image = CType(resources.GetObject("tsbtnSettings.Image"), System.Drawing.Image)
        Me.tsbtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnSettings.Name = "tsbtnSettings"
        Me.tsbtnSettings.Size = New System.Drawing.Size(63, 50)
        Me.tsbtnSettings.Tag = "Settings"
        Me.tsbtnSettings.Text = "Settings"
        Me.tsbtnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnSettings.ToolTipText = "Settings"
        '
        'tsbtnRxReportDesigner
        '
        Me.tsbtnRxReportDesigner.BackgroundImage = CType(resources.GetObject("tsbtnRxReportDesigner.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnRxReportDesigner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnRxReportDesigner.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnRxReportDesigner.Image = CType(resources.GetObject("tsbtnRxReportDesigner.Image"), System.Drawing.Image)
        Me.tsbtnRxReportDesigner.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnRxReportDesigner.Name = "tsbtnRxReportDesigner"
        Me.tsbtnRxReportDesigner.Size = New System.Drawing.Size(70, 50)
        Me.tsbtnRxReportDesigner.Tag = "RxReportDesigner"
        Me.tsbtnRxReportDesigner.Text = "RxDsgner"
        Me.tsbtnRxReportDesigner.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnRxReportDesigner.ToolTipText = "Rx Report Designer"
        Me.tsbtnRxReportDesigner.Visible = False
        '
        'tsbtnAuditReport
        '
        Me.tsbtnAuditReport.BackgroundImage = CType(resources.GetObject("tsbtnAuditReport.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnAuditReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnAuditReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnAuditReport.Image = CType(resources.GetObject("tsbtnAuditReport.Image"), System.Drawing.Image)
        Me.tsbtnAuditReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnAuditReport.Name = "tsbtnAuditReport"
        Me.tsbtnAuditReport.Size = New System.Drawing.Size(68, 50)
        Me.tsbtnAuditReport.Tag = "AuditReport"
        Me.tsbtnAuditReport.Text = "AuditRpt"
        Me.tsbtnAuditReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnAuditReport.ToolTipText = "Audit Report"
        '
        'tsbtnArchivedReport
        '
        Me.tsbtnArchivedReport.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnArchivedReport.BackgroundImage = CType(resources.GetObject("tsbtnArchivedReport.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnArchivedReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnArchivedReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnArchivedReport.Image = CType(resources.GetObject("tsbtnArchivedReport.Image"), System.Drawing.Image)
        Me.tsbtnArchivedReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnArchivedReport.Name = "tsbtnArchivedReport"
        Me.tsbtnArchivedReport.Size = New System.Drawing.Size(96, 50)
        Me.tsbtnArchivedReport.Tag = "ArchivedReport"
        Me.tsbtnArchivedReport.Text = "ArchAuditRpt"
        Me.tsbtnArchivedReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnArchivedReport.ToolTipText = "Archived Audit Report"
        '
        'tsbtnArchiveAudit
        '
        Me.tsbtnArchiveAudit.BackgroundImage = CType(resources.GetObject("tsbtnArchiveAudit.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnArchiveAudit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnArchiveAudit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnArchiveAudit.Image = CType(resources.GetObject("tsbtnArchiveAudit.Image"), System.Drawing.Image)
        Me.tsbtnArchiveAudit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnArchiveAudit.Name = "tsbtnArchiveAudit"
        Me.tsbtnArchiveAudit.Size = New System.Drawing.Size(73, 50)
        Me.tsbtnArchiveAudit.Tag = "ArchiveAudit"
        Me.tsbtnArchiveAudit.Text = "ArchAudit"
        Me.tsbtnArchiveAudit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnArchiveAudit.ToolTipText = "Archive Audit"
        '
        'tsbtnCMSSettings
        '
        Me.tsbtnCMSSettings.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnCMSSettings.BackgroundImage = CType(resources.GetObject("tsbtnCMSSettings.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnCMSSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnCMSSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnCMSSettings.Image = CType(resources.GetObject("tsbtnCMSSettings.Image"), System.Drawing.Image)
        Me.tsbtnCMSSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnCMSSettings.Name = "tsbtnCMSSettings"
        Me.tsbtnCMSSettings.Size = New System.Drawing.Size(163, 50)
        Me.tsbtnCMSSettings.Tag = "CMSSetting"
        Me.tsbtnCMSSettings.Text = "CMS1500 08/05 Setting"
        Me.tsbtnCMSSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnCMSSettings.ToolTipText = "CMS1500 08/05 Setting"
        '
        'tsbtnCMSSettingsNew
        '
        Me.tsbtnCMSSettingsNew.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnCMSSettingsNew.BackgroundImage = CType(resources.GetObject("tsbtnCMSSettingsNew.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnCMSSettingsNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnCMSSettingsNew.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnCMSSettingsNew.Image = CType(resources.GetObject("tsbtnCMSSettingsNew.Image"), System.Drawing.Image)
        Me.tsbtnCMSSettingsNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnCMSSettingsNew.Name = "tsbtnCMSSettingsNew"
        Me.tsbtnCMSSettingsNew.Size = New System.Drawing.Size(163, 50)
        Me.tsbtnCMSSettingsNew.Tag = "NewCMSSetting"
        Me.tsbtnCMSSettingsNew.Text = "CMS1500 02/12 Setting"
        Me.tsbtnCMSSettingsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnCMSSettingsNew.ToolTipText = "CMS1500 02/12 Setting"
        Me.tsbtnCMSSettingsNew.Visible = False
        '
        'tsbtnUB04Setting
        '
        Me.tsbtnUB04Setting.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnUB04Setting.BackgroundImage = CType(resources.GetObject("tsbtnUB04Setting.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnUB04Setting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnUB04Setting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnUB04Setting.Image = CType(resources.GetObject("tsbtnUB04Setting.Image"), System.Drawing.Image)
        Me.tsbtnUB04Setting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnUB04Setting.Name = "tsbtnUB04Setting"
        Me.tsbtnUB04Setting.Size = New System.Drawing.Size(93, 50)
        Me.tsbtnUB04Setting.Tag = "UB04"
        Me.tsbtnUB04Setting.Text = "UB04 Setting"
        Me.tsbtnUB04Setting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnUB04Setting.ToolTipText = "UB04 Setting"
        '
        'tsbtnRestoreArchive
        '
        Me.tsbtnRestoreArchive.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnRestoreArchive.BackgroundImage = CType(resources.GetObject("tsbtnRestoreArchive.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnRestoreArchive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnRestoreArchive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnRestoreArchive.Image = CType(resources.GetObject("tsbtnRestoreArchive.Image"), System.Drawing.Image)
        Me.tsbtnRestoreArchive.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnRestoreArchive.Name = "tsbtnRestoreArchive"
        Me.tsbtnRestoreArchive.Size = New System.Drawing.Size(78, 50)
        Me.tsbtnRestoreArchive.Tag = "RestoreArchive"
        Me.tsbtnRestoreArchive.Text = "ResArchive"
        Me.tsbtnRestoreArchive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnRestoreArchive.ToolTipText = "Restore Archive"
        '
        'tsbtnStartupSettings
        '
        Me.tsbtnStartupSettings.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnStartupSettings.BackgroundImage = CType(resources.GetObject("tsbtnStartupSettings.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnStartupSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnStartupSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnStartupSettings.Image = CType(resources.GetObject("tsbtnStartupSettings.Image"), System.Drawing.Image)
        Me.tsbtnStartupSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnStartupSettings.Name = "tsbtnStartupSettings"
        Me.tsbtnStartupSettings.Size = New System.Drawing.Size(82, 50)
        Me.tsbtnStartupSettings.Tag = "StartUpSettings"
        Me.tsbtnStartupSettings.Text = "StrtSetting"
        Me.tsbtnStartupSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnStartupSettings.ToolTipText = "Startup Settings"
        Me.tsbtnStartupSettings.Visible = False
        '
        'tsbtnLockScreen
        '
        Me.tsbtnLockScreen.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnLockScreen.BackgroundImage = CType(resources.GetObject("tsbtnLockScreen.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnLockScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnLockScreen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnLockScreen.Image = CType(resources.GetObject("tsbtnLockScreen.Image"), System.Drawing.Image)
        Me.tsbtnLockScreen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnLockScreen.Name = "tsbtnLockScreen"
        Me.tsbtnLockScreen.Size = New System.Drawing.Size(66, 50)
        Me.tsbtnLockScreen.Tag = "LockScreen"
        Me.tsbtnLockScreen.Text = "LockScrn"
        Me.tsbtnLockScreen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnLockScreen.ToolTipText = "Lock Screen"
        '
        'tsbtnLogout
        '
        Me.tsbtnLogout.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnLogout.BackgroundImage = CType(resources.GetObject("tsbtnLogout.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnLogout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnLogout.Image = CType(resources.GetObject("tsbtnLogout.Image"), System.Drawing.Image)
        Me.tsbtnLogout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnLogout.Name = "tsbtnLogout"
        Me.tsbtnLogout.Size = New System.Drawing.Size(56, 50)
        Me.tsbtnLogout.Tag = "Logout"
        Me.tsbtnLogout.Text = "Logout"
        Me.tsbtnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnLogout.ToolTipText = "Logout"
        '
        'tsb_UserGuide
        '
        Me.tsb_UserGuide.BackColor = System.Drawing.Color.Transparent
        Me.tsb_UserGuide.BackgroundImage = CType(resources.GetObject("tsb_UserGuide.BackgroundImage"), System.Drawing.Image)
        Me.tsb_UserGuide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsb_UserGuide.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsb_UserGuide.Image = CType(resources.GetObject("tsb_UserGuide.Image"), System.Drawing.Image)
        Me.tsb_UserGuide.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_UserGuide.Name = "tsb_UserGuide"
        Me.tsb_UserGuide.Size = New System.Drawing.Size(75, 50)
        Me.tsb_UserGuide.Tag = "UserGuide"
        Me.tsb_UserGuide.Text = "&User Guide"
        Me.tsb_UserGuide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_UserGuide.ToolTipText = "User Guide"
        '
        'tsbtnAboutUs
        '
        Me.tsbtnAboutUs.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnAboutUs.BackgroundImage = CType(resources.GetObject("tsbtnAboutUs.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnAboutUs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnAboutUs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnAboutUs.Image = CType(resources.GetObject("tsbtnAboutUs.Image"), System.Drawing.Image)
        Me.tsbtnAboutUs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnAboutUs.Name = "tsbtnAboutUs"
        Me.tsbtnAboutUs.Size = New System.Drawing.Size(64, 50)
        Me.tsbtnAboutUs.Tag = "AboutUs"
        Me.tsbtnAboutUs.Text = "AboutUs"
        Me.tsbtnAboutUs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnAboutUs.ToolTipText = "About Us"
        '
        'tsbtnClose
        '
        Me.tsbtnClose.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnClose.BackgroundImage = CType(resources.GetObject("tsbtnClose.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnClose.Image = CType(resources.GetObject("tsbtnClose.Image"), System.Drawing.Image)
        Me.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnClose.Name = "tsbtnClose"
        Me.tsbtnClose.Size = New System.Drawing.Size(36, 50)
        Me.tsbtnClose.Tag = "Close"
        Me.tsbtnClose.Text = "Exit"
        Me.tsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ToolStrip1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1192, 56)
        Me.Panel1.TabIndex = 17
        '
        'SplitterMain
        '
        Me.SplitterMain.Location = New System.Drawing.Point(259, 56)
        Me.SplitterMain.Name = "SplitterMain"
        Me.SplitterMain.Size = New System.Drawing.Size(4, 788)
        Me.SplitterMain.TabIndex = 18
        Me.SplitterMain.TabStop = False
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.Label33)
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Controls.Add(Me.Label29)
        Me.pnl_tlsp_Top.Controls.Add(Me.Label31)
        Me.pnl_tlsp_Top.Controls.Add(Me.Label34)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(263, 56)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(929, 56)
        Me.pnl_tlsp_Top.TabIndex = 19
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label33.Location = New System.Drawing.Point(1, 52)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(924, 1)
        Me.Label33.TabIndex = 14
        Me.Label33.Text = "label2"
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnModify, Me.btnCloneProvider, Me.btnDelete, Me.btnRefresh, Me.btnClose})
        Me.tstrip.Location = New System.Drawing.Point(1, 1)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(924, 52)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(37, 49)
        Me.btnNew.Tag = "New"
        Me.btnNew.Text = "&New"
        Me.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnNew.ToolTipText = "New"
        '
        'btnModify
        '
        Me.btnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModify.Image = CType(resources.GetObject("btnModify.Image"), System.Drawing.Image)
        Me.btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(53, 49)
        Me.btnModify.Tag = "Modify"
        Me.btnModify.Text = "&Modify"
        Me.btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnModify.ToolTipText = "Modify"
        '
        'btnCloneProvider
        '
        Me.btnCloneProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloneProvider.Image = CType(resources.GetObject("btnCloneProvider.Image"), System.Drawing.Image)
        Me.btnCloneProvider.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCloneProvider.Name = "btnCloneProvider"
        Me.btnCloneProvider.Size = New System.Drawing.Size(45, 49)
        Me.btnCloneProvider.Tag = "Clone"
        Me.btnCloneProvider.Text = "Cl&one"
        Me.btnCloneProvider.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCloneProvider.ToolTipText = "Clone Provider"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(50, 49)
        Me.btnDelete.Tag = "Delete"
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnDelete.ToolTipText = "Delete"
        '
        'btnRefresh
        '
        Me.btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(58, 49)
        Me.btnRefresh.Tag = "Refresh"
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnRefresh.ToolTipText = "Refresh"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(43, 49)
        Me.btnClose.Tag = "Close"
        Me.btnClose.Text = "&Close"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnClose.Visible = False
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label29.Location = New System.Drawing.Point(1, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(924, 1)
        Me.Label29.TabIndex = 10
        Me.Label29.Text = "label2"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(0, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 53)
        Me.Label31.TabIndex = 11
        Me.Label31.Text = "label4"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label34.Location = New System.Drawing.Point(925, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 53)
        Me.Label34.TabIndex = 13
        Me.Label34.Text = "label3"
        '
        'pnlMainTop
        '
        Me.pnlMainTop.Controls.Add(Me.Panel21)
        Me.pnlMainTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMainTop.Location = New System.Drawing.Point(263, 112)
        Me.pnlMainTop.Name = "pnlMainTop"
        Me.pnlMainTop.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlMainTop.Size = New System.Drawing.Size(929, 28)
        Me.pnlMainTop.TabIndex = 81
        '
        'Panel21
        '
        Me.Panel21.BackColor = System.Drawing.Color.Transparent
        Me.Panel21.BackgroundImage = CType(resources.GetObject("Panel21.BackgroundImage"), System.Drawing.Image)
        Me.Panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel21.Controls.Add(Me.lblMainTop)
        Me.Panel21.Controls.Add(Me.Label44)
        Me.Panel21.Controls.Add(Me.Label45)
        Me.Panel21.Controls.Add(Me.Label46)
        Me.Panel21.Controls.Add(Me.Label30)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel21.Location = New System.Drawing.Point(0, 0)
        Me.Panel21.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(926, 25)
        Me.Panel21.TabIndex = 19
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label44.Location = New System.Drawing.Point(1, 24)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(924, 1)
        Me.Label44.TabIndex = 8
        Me.Label44.Text = "label2"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(0, 1)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 24)
        Me.Label45.TabIndex = 7
        Me.Label45.Text = "label4"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label46.Location = New System.Drawing.Point(925, 1)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 24)
        Me.Label46.TabIndex = 6
        Me.Label46.Text = "label3"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label30.Location = New System.Drawing.Point(0, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(926, 1)
        Me.Label30.TabIndex = 9
        Me.Label30.Text = "label2"
        '
        'pnlMainMainTop
        '
        Me.pnlMainMainTop.Controls.Add(Me.pnlClientUpdateDetailsFilter)
        Me.pnlMainMainTop.Controls.Add(Me.Panel8)
        Me.pnlMainMainTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMainMainTop.Location = New System.Drawing.Point(263, 140)
        Me.pnlMainMainTop.Name = "pnlMainMainTop"
        Me.pnlMainMainTop.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlMainMainTop.Size = New System.Drawing.Size(929, 27)
        Me.pnlMainMainTop.TabIndex = 82
        '
        'pnlClientUpdateDetailsFilter
        '
        Me.pnlClientUpdateDetailsFilter.BackColor = System.Drawing.Color.Transparent
        Me.pnlClientUpdateDetailsFilter.BackgroundImage = CType(resources.GetObject("pnlClientUpdateDetailsFilter.BackgroundImage"), System.Drawing.Image)
        Me.pnlClientUpdateDetailsFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.btnSearchClientUpdate)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.txtSearchClientUpdate)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.lblClientUpdateSearch)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.cmbClientMachineName)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.lblClientMachineName)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.Label43)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.Label47)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.Label48)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.Label49)
        Me.pnlClientUpdateDetailsFilter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlClientUpdateDetailsFilter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlClientUpdateDetailsFilter.Location = New System.Drawing.Point(0, 0)
        Me.pnlClientUpdateDetailsFilter.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlClientUpdateDetailsFilter.Name = "pnlClientUpdateDetailsFilter"
        Me.pnlClientUpdateDetailsFilter.Size = New System.Drawing.Size(926, 24)
        Me.pnlClientUpdateDetailsFilter.TabIndex = 21
        '
        'btnSearchClientUpdate
        '
        Me.btnSearchClientUpdate.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchClientUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchClientUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnSearchClientUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnSearchClientUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchClientUpdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchClientUpdate.Location = New System.Drawing.Point(501, 0)
        Me.btnSearchClientUpdate.Name = "btnSearchClientUpdate"
        Me.btnSearchClientUpdate.Size = New System.Drawing.Size(58, 24)
        Me.btnSearchClientUpdate.TabIndex = 4
        Me.btnSearchClientUpdate.Text = "Show"
        Me.btnSearchClientUpdate.UseVisualStyleBackColor = False
        '
        'txtSearchClientUpdate
        '
        Me.txtSearchClientUpdate.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearchClientUpdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchClientUpdate.ForeColor = System.Drawing.Color.Black
        Me.txtSearchClientUpdate.Location = New System.Drawing.Point(357, 1)
        Me.txtSearchClientUpdate.Name = "txtSearchClientUpdate"
        Me.txtSearchClientUpdate.Size = New System.Drawing.Size(133, 22)
        Me.txtSearchClientUpdate.TabIndex = 1
        '
        'lblClientUpdateSearch
        '
        Me.lblClientUpdateSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblClientUpdateSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblClientUpdateSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClientUpdateSearch.Location = New System.Drawing.Point(259, 1)
        Me.lblClientUpdateSearch.Name = "lblClientUpdateSearch"
        Me.lblClientUpdateSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblClientUpdateSearch.Size = New System.Drawing.Size(98, 22)
        Me.lblClientUpdateSearch.TabIndex = 3
        Me.lblClientUpdateSearch.Text = "Search :"
        Me.lblClientUpdateSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbClientMachineName
        '
        Me.cmbClientMachineName.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbClientMachineName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClientMachineName.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClientMachineName.Location = New System.Drawing.Point(109, 1)
        Me.cmbClientMachineName.Name = "cmbClientMachineName"
        Me.cmbClientMachineName.Size = New System.Drawing.Size(150, 22)
        Me.cmbClientMachineName.TabIndex = 8
        '
        'lblClientMachineName
        '
        Me.lblClientMachineName.AutoSize = True
        Me.lblClientMachineName.BackColor = System.Drawing.Color.Transparent
        Me.lblClientMachineName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblClientMachineName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClientMachineName.Location = New System.Drawing.Point(1, 1)
        Me.lblClientMachineName.Name = "lblClientMachineName"
        Me.lblClientMachineName.Padding = New System.Windows.Forms.Padding(3, 4, 3, 3)
        Me.lblClientMachineName.Size = New System.Drawing.Size(108, 21)
        Me.lblClientMachineName.TabIndex = 7
        Me.lblClientMachineName.Text = "Machine Name :"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(1, 23)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(924, 1)
        Me.Label43.TabIndex = 8
        Me.Label43.Text = "label2"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(0, 1)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 23)
        Me.Label47.TabIndex = 7
        Me.Label47.Text = "label4"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label48.Location = New System.Drawing.Point(925, 1)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(1, 23)
        Me.Label48.TabIndex = 6
        Me.Label48.Text = "label3"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(0, 0)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(926, 1)
        Me.Label49.TabIndex = 5
        Me.Label49.Text = "label1"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImage = CType(resources.GetObject("Panel8.BackgroundImage"), System.Drawing.Image)
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.btnShowAudit)
        Me.Panel8.Controls.Add(Me.txtInstringSearch)
        Me.Panel8.Controls.Add(Me.lblSearch)
        Me.Panel8.Controls.Add(Me.dtTo)
        Me.Panel8.Controls.Add(Me.lblTo)
        Me.Panel8.Controls.Add(Me.dtFrom)
        Me.Panel8.Controls.Add(Me.lblFrom)
        Me.Panel8.Controls.Add(Me.cmbAuditCategory)
        Me.Panel8.Controls.Add(Me.lblAuditCategory)
        Me.Panel8.Controls.Add(Me.optSelfNotesCategory)
        Me.Panel8.Controls.Add(Me.Label21)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Controls.Add(Me.Label23)
        Me.Panel8.Controls.Add(Me.Label24)
        Me.Panel8.Controls.Add(Me.optSelfNotesStatus)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(926, 24)
        Me.Panel8.TabIndex = 19
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(1, 23)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(924, 1)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "label2"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(0, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 23)
        Me.Label22.TabIndex = 7
        Me.Label22.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(925, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 23)
        Me.Label23.TabIndex = 6
        Me.Label23.Text = "label3"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(926, 1)
        Me.Label24.TabIndex = 5
        Me.Label24.Text = "label1"
        '
        'HelpComponent1
        '
        Me.HelpComponent1.Mode = gloEMR.Help.HelpComponent.ProviderMode.Client
        '
        'frmgloEMRAdmin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1192, 866)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlAudit)
        Me.Controls.Add(Me.pnlMainMainTop)
        Me.Controls.Add(Me.pnlMainTop)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Controls.Add(Me.SplitterMain)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlCommand)
        Me.Controls.Add(Me.StatusBar1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "frmgloEMRAdmin"
        Me.Text = "QPM Admin"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.stbPnlLoginTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stbPnlVersion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stbPnlBuild, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCommand.ResumeLayout(False)
        Me.pnlLeft.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMainMain.ResumeLayout(False)
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAudit.ResumeLayout(False)
        Me.pnlAudit.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.pnlMainTop.ResumeLayout(False)
        Me.Panel21.ResumeLayout(False)
        Me.pnlMainMainTop.ResumeLayout(False)
        Me.pnlClientUpdateDetailsFilter.ResumeLayout(False)
        Me.pnlClientUpdateDetailsFilter.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    'sarika Audit Log Instr Search
    Private _blnSearch As Boolean = False
    Private _LogoutStatus As Boolean = False
    '---------
    Private m_hotKeys As HotKeyCollection
    Public Delegate Sub HotKeyPressedEventHandler(ByVal sender As Object, ByVal e As HotKeyPressedEventArgs)


    Private Sub sethelpBuildermode()
        Try



            If frmSplash.gstrHelpProvider.ToUpper() = "CLIENT" Then
                gloEMR.Help.HelpComponent.blnbuildmode = False

                Me.HelpComponent1.Mode = HelpComponent.ProviderMode.Client
            Else
                Me.HelpComponent1.Mode = HelpComponent.ProviderMode.Builder


            End If



        Catch ex As Exception

        End Try
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)
        Try

            'Code added by chetan to hide mdichild scrollbar


            'Code added by chetan to hide mdichild scrollbar 30 june

            If (m.Msg = UnmanagedMethods.WM_ACTIVATEAPP) Then
                Dim k As Integer = 0
                'If m.WParam.ToInt32() = 1 And timerLockScreen.Enabled = False Then
                '    UpdateLog("WndProc: If m.WParam.ToInt32() = 1 And timerLockScreen.Enabled = False Then")
                UnRegisterMyHotKey()
                'Else
                If m.WParam.ToInt32() = 1 Then
                    ''UpdateVoiceLog(" HOT Key WM_ACTIVATEAPP " & m.Msg & " - " & m.WParam.ToInt32 & " - " & m.ToString & " RegisterMyHotKey ")
                    '' UpdateLog("WndProc: ElseIf m.WParam.ToInt32() = 1 Then")
                    RegisterMyHotKey()
                    '' EnableProtection()
                ElseIf m.WParam.ToInt32() = 0 Then
                    ''  UpdateVoiceLog(" HOT Key WM_ACTIVATEAPP " & m.Msg & " - " & m.WParam.ToInt32 & " - " & m.ToString & " UnRegisterMyHotKey ")
                    '' UpdateLog("WndProc: ElseIf m.WParam.ToInt32() = 0 Then  , timerLockScreen.Enabled =" & timerLockScreen.Enabled)
                    UnRegisterMyHotKey()
                    '' DisableProtection()
                End If


            ElseIf (m.Msg = UnmanagedMethods.WM_HOTKEY) Then
                ''   gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Initialize, " HOT Key WM_HOTKEY " & m.Msg & " -- " & m.ToString, gloAuditTrail.ActivityOutCome.Success)
                '' UpdateVoiceLog(" HOT Key WM_HOTKEY " & m.Msg & " -- " & m.ToString)
                Dim hotKeyId As Integer = m.WParam.ToInt32()
                Select Case hotKeyId
                    Case UnmanagedMethods.IDHOT_SNAPDESKTOP
                        '' Dim e As System.EventArgs = New System.EventArgs
                        '' RaiseEvent PrintDesktopPressed(Me, e)

                    Case UnmanagedMethods.IDHOT_SNAPWINDOW
                        '' Dim e As System.EventArgs = New System.EventArgs
                        '' RaiseEvent PrintWindowPressed(Me, e)

                    Case Else
                        UpdateLog("WndProc: ElseIf (m.Msg = UnmanagedMethods.WM_HOTKEY) Then, Case Else")
                        Dim htk As HotKey
                        For Each htk In m_hotKeys
                            If (htk.AtomId.Equals(m.WParam)) Then
                                Dim e As HotKeyPressedEventArgs = New HotKeyPressedEventArgs(htk)
                                RaiseEvent HotKeyPressed(Me, e)
                                Exit For
                            End If
                        Next
                End Select
            End If
        Catch ex As Exception
            ' MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Debug.WriteLine("Error:" + ex.Message)
        End Try
    End Sub

    Public Sub UnRegisterMyHotKey()
        '' If HOT Keys are Register then UnRegister HOT Keys
        ' If IsActivated = True Then
        'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Dispose, "UnRegisterHotKey started", gloAuditTrail.ActivityOutCome.Success)
        RemoveHandler Me.HotKeyPressed, AddressOf hotKey_Pressed
        HotKeys.Clear()
        'IsActivated = False
        '  gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Dispose, "UnRegisterHotKey finished ", gloAuditTrail.ActivityOutCome.Success)
        'End If
    End Sub

    Public Sub RegisterMyHotKey()
        '' If HOT Keys are not Register then Register HOT Keys
        '  If IsActivated = False Then
        'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "RegisterHotKey started", gloAuditTrail.ActivityOutCome.Success)
        'UpdateVoiceLog("RegisterHotKey started ")

        Try
            RemoveHandler Me.HotKeyPressed, AddressOf hotKey_Pressed
        Catch ex As Exception
            'sometimes it gives error, that's why blank catch
        End Try

        AddHandler Me.HotKeyPressed, AddressOf hotKey_Pressed

        ''hotkey added for help
        Dim htk As HotKey = New HotKey("gloHelp", Keys.F1, HotKey.HotKeyModifiers.MOD_NONE)
        Me.HotKeys.Add(htk)

    End Sub

    Private Sub hotKey_Pressed(ByVal sender As System.Object, ByVal e As HotKeyPressedEventArgs)
        ''  UpdateLog("Me.WindowState " & Me.WindowState.ToString)
        If Me.WindowState <> FormWindowState.Minimized And Me.Name <> "frmLockScreen" Then
            Dim blnExamChild As Boolean = False
            If e.HotKey.KeyCode.ToString() = "F1" Then
                ShowHelp()
            End If
        End If
        ''

    End Sub


    Public ReadOnly Property HotKeys() As HotKeyCollection
        Get
            HotKeys = m_hotKeys
        End Get
    End Property


    Private Sub ShowHelp()
        'If Not IsNothing(Me.ActiveMdiChild) Then
        '    Me.HelpComponent1.ProcessRequest(Me.ActiveMdiChild.Handle)
        'Else
        '    If Not IsNothing(Me.ActiveForm) Then
        '        Me.HelpComponent1.ProcessRequest(Me.ActiveForm.Handle)
        '    Else
        '        Me.HelpComponent1.ProcessRequest(Me.Handle)
        '    End If
        'End If
        Me.HelpComponent1.ShowHelp(Me)
    End Sub

    Private Sub frmgloEMRAdmin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '---------------------------------------
            'sarika 21st may 07
            'do the status bar settings
            UpdateStatusBar()
            '---------------------------------------

            '-----------------------------------------------
            'sarika 21st may 07
            '  Timer1.Enabled = False

            'code comment start by nilesh on 20110228 for case GLO2010-0008612
            'Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName
            'Dim aProcName As String = System.IO.Path.GetFileNameWithoutExtension(aModuleName)

            'If Process.GetProcessesByName(aProcName).Length > 1 Then
            '    MessageBox.Show("Another instance of this application is already running.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Application.Exit()
            '    End
            '    Exit Sub
            'End If
            'code comment end by nilesh on 20110228 for case GLO2010-0008612

            '-----------------------------------------------
            Me.Cursor = Cursors.WaitCursor
            Call Fill_Admin()
            Call Fill_Audit()
            '-------------------------------
            'sarika 25th apr 2007
            '  Call Fill_DatabaseTools()
            '-------------------------------

            'sarika 26th june 07
            'Call Fill_Tools()
            '----------
            SetToolStrip()
            Dim regKey As RegistryKey
            ''Sandip Darade 20090725
            ''Read values from registry in accordance with whether Admin is being used for gloPM or gloEMR
            If gstrAdminFor = "gloEMR" Then
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            Else
                Me.Text = "QPM Admin"
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, True)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue("LeftWidth")) = False Then
                If CInt(gloRegistrySetting.GetRegistryValue("LeftWidth")) > 10 Then
                    pnlLeft.Width = CInt(gloRegistrySetting.GetRegistryValue("LeftWidth"))
                Else
                    pnlLeft.Width = 290
                End If
            Else
                pnlLeft.Width = 290
            End If
            If IsNothing(gloRegistrySetting.GetRegistryValue("CategoryWidth")) = False Then
                If CInt(gloRegistrySetting.GetRegistryValue("CategoryWidth")) > 10 Then
                    trvCategory.Width = CInt(gloRegistrySetting.GetRegistryValue("CategoryWidth"))
                Else
                    trvCategory.Width = 190
                End If
            Else
                trvCategory.Width = 190
            End If
            gloRegistrySetting.CloseRegistryKey()

            Me.Cursor = Cursors.Default
            ' picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topadministrator.JPG")
            gstrConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
            '' Code added by Ravikiran on 10/02/2007
            '' Call checkRxReportPath()

            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(2)
            Call ShowAdministrator()

            '******By Sandip Deshmukh 18 Oct 07 12.54PM Bug# 353
            '******to show tooltip information
            ' Create the ToolTip and associate with the Form container.
            ' Dim toolTip1 As New ToolTip()

            ' Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000
            toolTip1.InitialDelay = 1000
            toolTip1.ReshowDelay = 100
            ' Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = True

            ' Set up the ToolTip text for the Button and Checkbox.
            If Me.btnHideToolBar.Text = "Hide Toolbar" Then
                toolTip1.SetToolTip(Me.btnHideToolBar, "Hide Toolbar")
            Else
                toolTip1.SetToolTip(Me.btnHideToolBar, "Show Toolbar")
            End If
            '******18 Oct 07 12.54PM Bug# 353

            'code by supriya 11/7/2008
            Dim objSettings As New clsSettings
            If objSettings.GetSettings() = True Then
                'code added by supriya 11/7/2008 Surescript Server
                gblnIsSurescriptEnabled = objSettings.IsSurescriptEnabled
                gblnIsStagingServer = objSettings.IsStagingServer

                ''Added ServicesDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table   
                If Not (String.IsNullOrEmpty(objSettings.ServicesServerName) Or String.IsNullOrEmpty(objSettings.ServicesDatabaseName)) Then
                    gstrServicesDBName = objSettings.ServicesDatabaseName
                    gstrServicesServerName = objSettings.ServicesServerName
                    gstrServicesUserID = objSettings.ServicesUserID
                    Dim objgloServicesDecryptions As New clsEncryption
                    If Not IsNothing(objgloServicesDecryptions) Then
                        gstrServicesPassWord = objgloServicesDecryptions.DecryptFromBase64String(objSettings.ServicesPassword, mdlGeneral.constEncryptDecryptKey)
                        objgloServicesDecryptions = Nothing
                    End If
                    objgloServicesDecryptions = Nothing
                    gbServicesIsSQLAUTHEN = objSettings.ServicesAuthentication
                    gloAUSLibrary.clsGeneral.strgloServiceDatabaseName = gstrServicesDBName
                    gloAUSLibrary.clsGeneral.strgloServicesServerName = gstrServicesServerName
                    gloAUSLibrary.clsGeneral.strgloServicesUserID = gstrServicesUserID
                    gloAUSLibrary.clsGeneral.strgloServicesPassWord = gstrServicesPassWord
                    gloAUSLibrary.clsGeneral.strgloServicesIsSQLAUTHEN = gbServicesIsSQLAUTHEN
                Else
                    MessageBox.Show("Set services database details in Settings -> server settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                ''Added ServicesDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table   
                gstrgloAusPortalURL = objSettings.gloAusPortalUrl
                gstrDemoNPIs = objSettings.DemoNPIs
            End If
            'code by supriya 11/7/2008
            'code by vijay patil 04/10/2010
            If Convert.ToString(objSettings.UB04_EnableBilling) = "False" Or Convert.ToString(objSettings.UB04_EnableBilling) = "" Then
                tsbtnUB04Setting.Visible = False
            End If

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateStatusBar()
        StatusBar1.Panels(0).Text = gstrLoginName & " Login Time " & gstrLoginTime
        ''Sandip Darade 20090421
        ''Read application version from assembly

        'StatusBar1.Panels(1).Text = "Version " & gstrVersion
        '20100105 Mahesh Nawal Logic for the getting the version no from database
        'Dim conn As New SqlConnection
        'Dim cmd As New SqlCommand
        ' ''Dim _strSQL As String = "SELECT ISNULL(sSettingsValue,'') AS  sSettingsValue FROM Settings WHERE sSettingsName = 'Application Version'"
        'Dim _strSQL As String = "SELECT ISNULL(sSettingsValue,'') AS  sSettingsValue FROM Settings WHERE sSettingsName = 'gloPMAdminVersion'"
        'conn.ConnectionString = (gloPMAdmin.mdlGeneral.GetConnectionString)
        'conn.Open()

        'cmd = New SqlCommand(_strSQL, conn)
        '_strSQL = cmd.ExecuteScalar

        StatusBar1.Panels(1).Text = Application.ProductVersion
        StatusBar1.Panels(2).Text = gstrSQLServerName + "   " + gstrDatabaseName


        '' StatusBar1.Panels(1).Text = "Version " & RetrieveVersion()


        ' StatusBar1.Panels(1).Text = "Version 3.5" ' & Application.ProductVersion
        '  Dim objAdminMessage As New clsClientMessage
        ' strAdministratorMessage = objAdminMessage.RetrieveMessage()
        '   objAdminMessage = Nothing
        'If Trim(strAdministratorMessage) <> "" Then
        '    StatusBar1.Panels(2).Text = strAdministratorMessage
        '    StatusBar1.Panels(2).ToolTipText = strAdministratorMessage
        '    'Timer2.Enabled=True
        'End If

        ''glPM5076 Date Format issue.
        stbPnlBuild.Text = "Last Modified Date " & String.Format("{0:MM/dd/yyyy h:m:s tt}", File.GetLastWriteTime(Application.StartupPath & "\" & Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName))



    End Sub

    Private Sub Fill_Audit()
        If (gstrAdminFor = "gloEMR") Then
            With trvAudit
                .BeginUpdate()
                Dim trvNode As TreeNode

                trvNode = New TreeNode
                With trvNode
                    .Text = "Audit Log"
                    .ImageIndex = 18
                    .SelectedImageIndex = 18
                    ' .ForeColor = Color.DarkBlue
                End With
                .Nodes.Add(trvNode)
                trvNode = Nothing

                trvNode = New TreeNode
                With trvNode
                    .Text = "Report"
                    .ImageIndex = 19
                    .SelectedImageIndex = 19
                End With
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing

                trvNode = New TreeNode
                With trvNode
                    .Text = "Archive"
                    .ImageIndex = 20
                    .SelectedImageIndex = 20
                End With
                .Nodes(0).Nodes.Add(trvNode)

                trvNode = New TreeNode
                With trvNode
                    .Text = "Archived Audit Log"
                    .ImageIndex = 21
                    .SelectedImageIndex = 21
                    ' .ForeColor = Color.DarkBlue
                End With
                .Nodes.Add(trvNode)
                trvNode = Nothing

                trvNode = New TreeNode
                With trvNode
                    .Text = "Archived Audit Report"
                    .ImageIndex = 22
                    .SelectedImageIndex = 22
                End With
                .Nodes(1).Nodes.Add(trvNode)

                trvNode = New TreeNode
                With trvNode
                    .Text = "Restore Archive"
                    .ImageIndex = 23
                    .SelectedImageIndex = 23
                End With
                .Nodes(1).Nodes.Add(trvNode)

                trvNode = Nothing
                .SelectedNode = .Nodes(0)
                .ExpandAll()
                .EndUpdate()
            End With
        End If
    End Sub

    'Private Sub Fill_DatabaseTools()
    '    With trvDatabase
    '        .BeginUpdate()
    '        Dim trvNode As TreeNode
    '        trvNode = New TreeNode
    '        With trvNode
    '            .Text = "Database"
    '            .ImageIndex = 20
    '            .SelectedImageIndex = 20
    '            .ForeColor = Color.DarkBlue
    '        End With
    '        .Nodes.Add(trvNode)
    '        trvNode = Nothing

    '        'trvNode = New TreeNode
    '        'With trvNode
    '        '    .Text = "Archive"
    '        '    .ImageIndex = 16
    '        '    .SelectedImageIndex = 16
    '        'End With
    '        '.Nodes(0).Nodes.Add(trvNode)
    '        'trvNode = Nothing

    '        'trvNode = New TreeNode
    '        'With trvNode
    '        '    .Text = "Replication"
    '        '    .ImageIndex = 17
    '        '    .SelectedImageIndex = 17
    '        'End With
    '        '.Nodes(0).Nodes.Add(trvNode)
    '        'trvNode = Nothing

    '        trvNode = New TreeNode
    '        With trvNode
    '            .Text = "Database Tool"
    '            .ImageIndex = 21
    '            .SelectedImageIndex = 21

    '        End With
    '        .Nodes(0).Nodes.Add(trvNode)
    '        trvNode = Nothing

    '        trvNode = New TreeNode
    '        With trvNode
    '            .Text = "Database Update"
    '            .ImageIndex = 21
    '            .SelectedImageIndex = 21

    '        End With
    '        .Nodes(0).Nodes.Add(trvNode)
    '        trvNode = Nothing

    '        .SelectedNode = .Nodes(0)

    '        .ExpandAll()
    '        .EndUpdate()
    '    End With
    'End Sub

    Private Sub Fill_Tools()
        With trvTools
            .BeginUpdate()
            Dim trvNode As TreeNode
            trvNode = New TreeNode
            With trvNode
                .Text = "Tools"
                .ImageIndex = 22
                .SelectedImageIndex = 22
                .ForeColor = Color.Blue
            End With
            .Nodes.Add(trvNode)
            trvNode = Nothing

            trvNode = New TreeNode
            With trvNode
                .Text = "Client Message"
                .ImageIndex = 23
                .SelectedImageIndex = 23
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing

            trvNode = New TreeNode
            With trvNode
                .Text = "Online Updates"
                .ImageIndex = 24
                .SelectedImageIndex = 24
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing

            trvNode = New TreeNode
            With trvNode
                .Text = "Suggestions to gloStream"
                .ImageIndex = 25
                .SelectedImageIndex = 25
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing

            .SelectedNode = .Nodes(0)

            .ExpandAll()
            .EndUpdate()
        End With
    End Sub

    Private Sub Fill_Admin()
        With trvAdminMenu
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvNode As TreeNode
            trvNode = New TreeNode
            With trvNode
                .Text = "Administrator"
                .ImageIndex = 0
                .SelectedImageIndex = 0
                '.ForeColor = Color.DarkBlue
            End With
            .Nodes.Add(trvNode)
            trvNode = Nothing

            ''trvNode = New TreeNode
            ''With trvNode
            ''    .Text = "Windows Groups & Users"
            ''    .ImageIndex = 1
            ''    .SelectedImageIndex = 1
            ''End With
            ''.Nodes(0).Nodes.Add(trvNode)
            ''trvNode = Nothing

            trvNode = New TreeNode
            With trvNode
                .Text = "QEMR Groups"
                .ImageIndex = 2
                .SelectedImageIndex = 2
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing



            trvNode = New TreeNode
            With trvNode
                .Text = "User Management"
                .ImageIndex = 3
                .SelectedImageIndex = 3
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing

            trvNode = New TreeNode
            With trvNode
                .Text = "Clinic"
                .ImageIndex = 4
                .SelectedImageIndex = 4
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing

            'Code commented by Sagar Ghodke on 20100806 
            'BUG#3383
            'trvNode = New TreeNode
            'With trvNode
            '    .Text = "Provider Type"
            '    .ImageIndex = 5
            '    .SelectedImageIndex = 5
            'End With
            '.Nodes(0).Nodes.Add(trvNode)
            'trvNode = Nothing
            'End Code commented by Sagar Ghodke on 20100806 

            trvNode = New TreeNode
            With trvNode
                .Text = "Provider"
                .ImageIndex = 6
                .SelectedImageIndex = 6
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing
            trvNode = New TreeNode
            With trvNode
                .Text = "Client Settings"
                .ImageIndex = 10
                .SelectedImageIndex = 10
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing
            trvNode = New TreeNode
            With trvNode
                .Text = "Client Update Details"
                .ImageIndex = 41
                .SelectedImageIndex = 41
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing
            ''Sandip Darade 20090725
            ''If admin being used for gloEMR
            If (gstrAdminFor = "gloEMR") Then
                trvNode = New TreeNode
                With trvNode
                    .Text = "Provider-User Task Assignment"
                    .ImageIndex = 7
                    .SelectedImageIndex = 7
                End With
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing

                '----------------------------------------------
                'sarika 30th Apr 2007
                trvNode = New TreeNode
                With trvNode
                    .Text = "Junior-Senior Provider Association"
                    .ImageIndex = 8
                    .SelectedImageIndex = 8
                End With
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing
                '----------------------------------------------

                '******By Sandip 13 Oct 07 4.53PM Bug #334
                '******Changed the Text property of control 
                '******from Doctor-Referral to Doctor-Referral Letter 
                trvNode = New TreeNode
                With trvNode
                    '.Text = "Doctor-Referral"
                    .Text = "Provider-Referral Letter"
                    .ImageIndex = 9
                    .SelectedImageIndex = 9
                End With
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing
                '******13 Oct 07 4.53PM Bug #334


                'sarika 28th june 07

                trvNode = New TreeNode
                With trvNode
                    .Text = "Clinic Workflow Settings"
                    .ImageIndex = 11
                    .SelectedImageIndex = 11
                End With
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing

                '---
            End If



            ''trvNode = New TreeNode
            ''With trvNode
            ''    .Text = "DB Management"
            ''    .ImageIndex = 11
            ''    .SelectedImageIndex = 11
            ''    .ForeColor = Color.DarkBlue
            ''End With
            ''.Nodes.Add(trvNode)
            ''trvNode = Nothing

            ''trvNode = New TreeNode
            ''With trvNode
            ''    .Text = "Backup"
            ''    .ImageIndex = 12
            ''    .SelectedImageIndex = 12
            ''End With
            ''.Nodes(1).Nodes.Add(trvNode)
            ''trvNode = Nothing

            ''trvNode = New TreeNode
            ''With trvNode
            ''    .Text = "Restore"
            ''    .ImageIndex = 9
            ''    .SelectedImageIndex = 9
            ''End With
            ''.Nodes(1).Nodes.Add(trvNode)
            ''trvNode = Nothing





            trvNode = New TreeNode
            With trvNode
                .Text = "Tools"
                .ImageIndex = 14
                .SelectedImageIndex = 14
                '.ForeColor = Color.DarkBlue
            End With
            .Nodes.Add(trvNode)
            trvNode = Nothing



            'trvNode = New TreeNode
            'With trvNode
            '    .Text = "Self Notes"
            '    .ImageIndex = 10
            '    .SelectedImageIndex = 10
            'End With
            '.Nodes(2).Nodes.Add(trvNode)
            'trvNode = Nothing

            ''Claim Validation settings 
            trvNode = New TreeNode
            With trvNode
                .Text = "Claim Validation Settings "
                .ImageIndex = 16
                .SelectedImageIndex = 16
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing

            trvNode = New TreeNode
            With trvNode
                .Text = "Settings"
                .ImageIndex = 27
                .SelectedImageIndex = 27
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing

            'SSRS Report Settings
            trvNode = New TreeNode
            With trvNode
                .Text = "SSRS Report Settings"
                .ImageIndex = 30
                .SelectedImageIndex = 30
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing


            trvNode = New TreeNode
            With trvNode
                .Text = "Deploy SSRS Reports"
                .ImageIndex = 31
                .SelectedImageIndex = 31
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing

            ''Update PayerID 
            trvNode = New TreeNode
            With trvNode
                .Text = "Update PayerID"
                .ImageIndex = 25
                .SelectedImageIndex = 25
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing

            If (gstrAdminFor = "gloEMR") Then

                trvNode = New TreeNode
                With trvNode
                    .Text = "Login Users"
                    .ImageIndex = 15
                    .SelectedImageIndex = 15
                End With
                .Nodes(1).Nodes.Add(trvNode)
                trvNode = Nothing
            End If






            trvNode = New TreeNode
            With trvNode
                .Text = "CMS1500 08/05 Settings"
                .ImageIndex = 17
                .SelectedImageIndex = 17
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing

            '' Sameer Shukla 12022013
            trvNode = New TreeNode
            With trvNode
                .Text = "CMS1500 02/12 Settings"
                .ImageIndex = 17
                .SelectedImageIndex = 17
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing


            Dim objsettings As New clsSettings
            objsettings.GetSettings()
            If Convert.ToString(objsettings.UB04_EnableBilling) <> "False" And Convert.ToString(objsettings.UB04_EnableBilling) <> "" Then
                trvNode = New TreeNode
                With trvNode
                    .Text = "UB04 Settings"
                    .ImageIndex = 32
                    .SelectedImageIndex = 32
                End With
                .Nodes(1).Nodes.Add(trvNode)
                trvNode = Nothing
            End If

            ''Added By Debasish on 20th Dec 2010.
            trvNode = New TreeNode
            With trvNode
                .Text = "Merge Insurance Plan"
                .ImageIndex = 33
                .SelectedImageIndex = 33
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing

            trvNode = New TreeNode
            With trvNode
                .Text = "Advance Merge Insurance"
                .ImageIndex = 42
                .SelectedImageIndex = 42
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing

            trvNode = New TreeNode()
            trvNode.Text = "Multiple Database"
            trvNode.ImageIndex = 34
            trvNode.SelectedImageIndex = 34
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing

            ''************************************

            'trvNode = New TreeNode
            'With trvNode
            '    .Text = "Voice Training Document"
            '    .ImageIndex = 13
            '    .SelectedImageIndex = 13
            'End With
            '.Nodes(2).Nodes.Add(trvNode)
            'trvNode = Nothing

            '' Code added by Ravikiran on 10/02/2007
            ''trvNode = New TreeNode
            ''With trvNode
            ''    .Text = "RxReport Designer"
            ''    .ImageIndex = 16
            ''    .SelectedImageIndex = 16
            ''End With
            ''.Nodes(2).Nodes.Add(trvNode)
            ''trvNode = Nothing
            '' code updation ends

            'Added for User groups 
            'Sandip Darade 7th Feb 07
            trvNode = New TreeNode
            With trvNode
                .Text = "User Groups"
                .ImageIndex = 24
                .SelectedImageIndex = 24
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing

            trvNode = New TreeNode
            With trvNode
                .Text = "Clearinghouse"
                .ImageIndex = 13
                .SelectedImageIndex = 13
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing
            'If (gstrAdminFor = "gloPM") Then


            ''code added for site prefix by pradeep on 29/06/2010
            Dim oResult As New Object
            Dim ogloSettings As New gloSettings.GeneralSettings(gloPMAdmin.mdlGeneral.GetConnectionString)
            ogloSettings.GetSetting("UseSitePrefix", oResult)
            Dim _UseSitePrefix As Int32 = 0
            If oResult.ToString() = "1" Then '1 means true
                _UseSitePrefix = Convert.ToInt32(oResult)
                trvNode = New TreeNode()
                trvNode.Text = "Site Prefix"
                trvNode.ImageIndex = 29
                trvNode.SelectedImageIndex = 29
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing
            End If




            trvNode = New TreeNode
            With trvNode
                .Text = "Import Fee Schedule"
                .ImageIndex = 26
                .SelectedImageIndex = 26
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing


            'FOLLOWUP_FEATURE
            Dim objSetting As New clsSettings
            Dim value As New Object
            objSetting.GetSetting("FOLLOWUP_FEATURE", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) And value.ToString() <> "" Then
                If value.ToString() = "1" Then
                    value = True
                ElseIf value.ToString() = "0" Then
                    value = False
                End If
                If (Convert.ToBoolean(value.ToString().Trim) = True) Then
                    trvNode = New TreeNode
                    With trvNode
                        ''Text changed according to phill sir
                        .Text = "Calculate/Recalculate Follow-up"
                        .ImageIndex = 35
                        .SelectedImageIndex = 35
                    End With
                    .Nodes(1).Nodes.Add(trvNode)
                    trvNode = Nothing
                End If
            End If

            ''7022Itens: $0.00 claim queue bug
            ''New tree node added
            ''Text changed according to phill sir
            trvNode = New TreeNode()
            trvNode.Text = "Repair $0.00 claim Follow-up"
            trvNode.ToolTipText = "Repair the $0.00 claim Follow-up from queue"
            trvNode.ImageIndex = 40
            trvNode.SelectedImageIndex = 40
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing

            'trvNode = New TreeNode
            'With trvNode
            '    .Text = "Service Configuration"
            '    .ImageIndex = 36
            '    .SelectedImageIndex = 36
            'End With
            '.Nodes(1).Nodes.Add(trvNode)
            'trvNode = Nothing



            'Business Center Rules Setup
            objSetting = New clsSettings
            value = New Object
            value = objSetting.GetBusinessCenterSettings("BusinessCenter_Feature")
            If Not IsNothing(value) And value.ToString() <> "" Then
                If (Convert.ToBoolean(value.ToString().Trim) = True) Then
                    trvNode = New TreeNode
                    With trvNode
                        .Text = "Setup Business Center Rules"
                        .ImageIndex = 37
                        .SelectedImageIndex = 37
                    End With
                    .Nodes(1).Nodes.Add(trvNode)
                    trvNode = Nothing
                End If
            End If


            'Account Business Center Utility
            objSetting = New clsSettings
            value = New Object
            value = objSetting.GetBusinessCenterSettings("businesscenter_patientaccount")
            If Not IsNothing(value) And value.ToString() <> "" Then
                If (Convert.ToBoolean(value.ToString().Trim) = True) Then
                    trvNode = New TreeNode
                    With trvNode
                        .Text = "Account Business Center Utility"
                        .ImageIndex = 38
                        .SelectedImageIndex = 38
                    End With
                    .Nodes(1).Nodes.Add(trvNode)
                    trvNode = Nothing
                End If
            End If




            'Worker Comp Utility
            objSetting = New clsSettings
            value = New Object
            objSetting.GetSetting("EnableWorkersCompBilling", gnLoginID, _ClinicID, value)
            If Not IsNothing(value) And value.ToString() <> "" Then
                If (Convert.ToBoolean(value.ToString().Trim) = True) Then
                    trvNode = New TreeNode
                    With trvNode
                        .Text = "Workers Comp Billing Utility"
                        .ImageIndex = 39
                        .SelectedImageIndex = 39
                    End With
                    .Nodes(1).Nodes.Add(trvNode)
                    trvNode = Nothing
                End If
            End If

            ' End If
            .ExpandAll()
            .SelectedNode = .Nodes(0)
            .EndUpdate()
        End With
    End Sub

    'Private Sub trvAdminMenu_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvAdminMenu.AfterSelect
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        enmUserOperation = enmOperation.Admin
    '        lblAuditCategory.Visible = False
    '        cmbAuditCategory.Visible = False
    '        pnlAudit.Visible = False
    '        pnlMainMainTop.Visible = False
    '        picMainSepMain.Visible = False
    '        optSelfNotesCategory.Visible = False
    '        optSelfNotesStatus.Visible = False
    '        pnlCommandButtons.Visible = False
    '        If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
    '            Select Case Trim(trvAdminMenu.SelectedNode.Text)
    '                Case "Administrator"
    '                    picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topadministrator.JPG")
    '                Case "DB Management"
    '                    picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdatabasemangement.JPG")
    '                Case "Tools"
    '                    picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toptools.JPG")
    '            End Select
    '            dgData.Visible = False
    '            SplitterMainCategory.Visible = False
    '            trvCategory.Visible = False
    '            picMainSepMain.Visible = False
    '            pnlMainMainTop.Visible = False
    '            picMainSepTop.Visible = False
    '            Me.Cursor = Cursors.Default
    '            Exit Sub
    '        End If
    '        picMainSepTop.Visible = True
    '        trvCategory.Visible = True
    '        SplitterMainCategory.Visible = True
    '        dgData.Visible = True

    '        Select Case Trim(trvAdminMenu.SelectedNode.Text)
    '            Case "Windows Groups & Users"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topwindowsgroupsanduser.JPG")
    '                Call Fill_CategoryWindowsGroupsUsers()
    '                If trvCategory.Nodes(0).Nodes(0).GetNodeCount(True) >= 1 Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0).Nodes(0)
    '                End If
    '            Case "QEMR Groups"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topgloemrgroup.JPG")
    '                Call Fill_CategorygloEMRGroups()
    '                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "User Management"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topusermanagement.JPG")
    '                Call Fill_CategorygloEMRUsers()
    '                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "Doctor"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdoctors.gif")
    '                Call Fill_CategoryProviders()
    '                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "Client Settings"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topclientsettings.gif")
    '                Call Fill_CategoryClientMachines()
    '                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "Clinic"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topclinic.JPG")
    '                Call Fill_CategoryClinics()
    '                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "Backup"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topbackup.JPG")
    '                Call Fill_CategoryBackups()
    '                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "Restore"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toprestore.JPG")
    '                Call Fill_CategoryRestores()
    '                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "Self Notes"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topselfnotes.JPG")
    '                Call Fill_CategorySelfNotes()
    '                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "Login Users"
    '                pnlCommandButtons.Visible = True
    '                btnDelete.Visible = False
    '                btnModify.Visible = False
    '                btnNew.Visible = False
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topwindowsgroupsanduser.JPG")
    '                trvCategory.Visible = False
    '                Call Fill_DetailsLoginUsers()
    '        End Select
    '        Me.Cursor = Cursors.Default
    '    Catch objErr As Exception
    '        Me.Cursor = Cursors.Default
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

#Region "  Fill Categories "
    Private Sub Fill_CategoryWindowsGroupsUsers()
        With trvCategory
            .BeginUpdate()
            Dim trvChildNode As TreeNode
            .Nodes.Clear()
            trvChildNode = New TreeNode
            With trvChildNode
                .Text = "Windows"
                .ImageIndex = 1
                .SelectedImageIndex = 1
                .ForeColor = Color.DarkBlue
            End With
            .Nodes.Add(trvChildNode)
            trvChildNode = Nothing

            trvChildNode = New TreeNode
            With trvChildNode
                .Text = "Groups"
                .ImageIndex = 1
                .SelectedImageIndex = 1
                .ForeColor = Color.Black
            End With
            .Nodes(0).Nodes.Add(trvChildNode)
            trvChildNode = Nothing

            trvChildNode = New TreeNode
            With trvChildNode
                .Text = "Users"
                .ImageIndex = 1
                .SelectedImageIndex = 1
                .ForeColor = Color.Black
            End With
            .Nodes(0).Nodes.Add(trvChildNode)
            trvChildNode = Nothing


            Dim objWindowsGroupsUsers As New clsWindowsGroupsUsers
            Dim arrGroups As New Collection
            arrGroups = objWindowsGroupsUsers.PopulateWindowsGroups()
            Dim nCount As Integer

            For nCount = 1 To arrGroups.Count
                trvChildNode = New TreeNode
                With trvChildNode
                    .Text = arrGroups.Item(nCount)
                    .ImageIndex = 1
                    .SelectedImageIndex = 1
                    .ForeColor = Color.Black
                End With
                .Nodes(0).Nodes(0).Nodes.Add(trvChildNode)
                trvChildNode = Nothing
            Next
            arrGroups = Nothing

            Dim arrUsers As New Collection
            arrUsers = objWindowsGroupsUsers.PopulateWindowsUsers()
            For nCount = 1 To arrUsers.Count
                trvChildNode = New TreeNode
                With trvChildNode
                    .Text = arrUsers.Item(nCount)
                    .ImageIndex = 1
                    .SelectedImageIndex = 1
                    .ForeColor = Color.Black
                End With
                .Nodes(0).Nodes(1).Nodes.Add(trvChildNode)

                trvChildNode = Nothing
            Next
            arrUsers = Nothing

            objWindowsGroupsUsers = Nothing
            .ExpandAll()
            .EndUpdate()
        End With
    End Sub


    Private Sub Fill_CategoryClientMessage()
        'pnlCommandButtons.Visible = True
        'btnNew.Visible = True
        'btnNew.Text = "New"
        'btnModify.Visible = True
        'btnModify.Text = "Modify"
        'btnDelete.Visible = True
        'btnDelete.Text = "Delete"

        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvGroups As TreeNode
            trvGroups = New TreeNode
            With trvGroups
                .Text = "Client Messages"
                .ImageIndex = 20
                .SelectedImageIndex = 20
                .ForeColor = Color.DarkBlue
            End With
            .Nodes.Add(trvGroups)
            Dim clClientMessages As New Collection
            Dim objClientMessage As New clsClientMessage
            clClientMessages = objClientMessage.Fill_Category
            objClientMessage = Nothing

            Dim nCount As Integer
            For nCount = 1 To clClientMessages.Count
                trvGroups = New TreeNode
                With trvGroups
                    .Text = clClientMessages.Item(nCount)
                    .ImageIndex = 20
                    .SelectedImageIndex = 20
                    .ForeColor = Color.Black
                End With
                .Nodes(0).Nodes.Add(trvGroups)
            Next
            trvGroups = New TreeNode
            With trvGroups
                .Text = "All"
                .ImageIndex = 20
                .SelectedImageIndex = 20
                .ForeColor = Color.Black
            End With
            .Nodes(0).Nodes.Add(trvGroups)
            .ExpandAll()
            .EndUpdate()
        End With

    End Sub

    Private Sub Fill_CategoryClientMachines()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "&New"
        btnModify.Visible = True
        btnModify.Text = "&Modify"
        btnDelete.Visible = True
        btnDelete.Text = "&Delete"
        btnDelete.ToolTipText = "Delete"

        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvGroups As TreeNode
            trvGroups = New TreeNode
            With trvGroups
                .Text = "Client Settings"
                .ImageIndex = 8
                .SelectedImageIndex = 8
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes.Add(trvGroups)
            trvGroups = Nothing
            Dim dtClients As New DataTable
            Dim objClientSettings As New clsClientMachines
            dtClients = objClientSettings.Fill_Clients
            objClientSettings = Nothing

            Dim nCount As Integer
            For nCount = 0 To dtClients.Rows.Count - 1
                trvGroups = New TreeNode
                With trvGroups
                    .Text = dtClients.Rows(nCount).Item("ClientMachineName")
                    .Tag = dtClients.Rows(nCount).Item("ClientID")
                    .ImageIndex = 14
                    .SelectedImageIndex = 14
                    .ForeColor = Color.FromArgb(31, 73, 125)
                End With
                .Nodes(0).Nodes.Add(trvGroups)
            Next
            .ExpandAll()
            .EndUpdate()
        End With

    End Sub

    Private Sub Fill_CategoryDBVersions()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "DB Update"

        btnModify.Visible = True
        btnModify.Text = "Send Status"
        btnDelete.Visible = False

        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvGroups As TreeNode
            trvGroups = New TreeNode

            With trvGroups
                .Text = "DB Versions"
                .ImageIndex = 6
                .SelectedImageIndex = 6
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With

            .Nodes.Add(trvGroups)
            trvGroups = Nothing
            Dim clDBVersions As New Collection
            Dim objDBUpdation As New clsDBUpdation(gstrConnectionString)
            clDBVersions = objDBUpdation.RetrieveDBVersions
            objDBUpdation = Nothing

            Dim nCount As Integer
            For nCount = 1 To clDBVersions.Count
                trvGroups = New TreeNode
                With trvGroups
                    .Text = clDBVersions.Item(nCount)
                    .ImageIndex = 6
                    .SelectedImageIndex = 6
                    .ForeColor = Color.FromArgb(31, 73, 125)
                End With
                .Nodes(0).Nodes.Add(trvGroups)
            Next
            trvGroups = New TreeNode
            With trvGroups
                .Text = "All"
                .ImageIndex = 6
                .SelectedImageIndex = 6
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvGroups)
            .ExpandAll()
            .EndUpdate()
        End With

    End Sub


    Private Sub Fill_CategoryProviders()

        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnModify.Visible = True
        btnDelete.Visible = True
        btnDelete.Text = "&Block"
        btnDelete.ToolTipText = "Block"

        trvCategory.Nodes.Clear()

        Dim oProvider As New clsProvider(mdlGeneral.GetConnectionString)
        Dim oActiveTreeNode As New TreeNode
        Dim oBlockedTreeNode As New TreeNode
        Dim oPendingProvidersTreeNode As New TreeNode
        Dim oApprovedProvidersTreeNode As New TreeNode
        Dim oDisabledProvidersTreeNode As New TreeNode
        Dim dtActiveProvider As DataTable
        Dim dtBlockedProvider As DataTable
        Dim dtPendingProvider As DataTable
        Dim dtApprovedProvider As DataTable
        Dim dtDisabledProvider As DataTable

        oActiveTreeNode.Text = "Active Providers"
        oActiveTreeNode.Tag = "Active"
        oActiveTreeNode.ImageIndex = 17
        oActiveTreeNode.SelectedImageIndex = 17
        oActiveTreeNode.ForeColor = Color.FromArgb(31, 73, 125)

        oBlockedTreeNode.Text = "Blocked Providers"
        oBlockedTreeNode.Tag = "Blocked"
        oBlockedTreeNode.ImageIndex = 16
        oBlockedTreeNode.SelectedImageIndex = 16
        oBlockedTreeNode.ForeColor = Color.FromArgb(31, 73, 125)

        oPendingProvidersTreeNode.Text = "Pending For Licenses Approval"
        oPendingProvidersTreeNode.Tag = "pending"
        oPendingProvidersTreeNode.ImageIndex = 20
        oPendingProvidersTreeNode.SelectedImageIndex = 20
        oPendingProvidersTreeNode.ForeColor = Color.FromArgb(31, 73, 125)

        oApprovedProvidersTreeNode.Text = "Approved Licensed Providers"
        oApprovedProvidersTreeNode.Tag = "review"
        oApprovedProvidersTreeNode.ImageIndex = 19
        oApprovedProvidersTreeNode.SelectedImageIndex = 19
        oApprovedProvidersTreeNode.ForeColor = Color.FromArgb(31, 73, 125)

        oDisabledProvidersTreeNode.Text = "Disabled Providers"
        oDisabledProvidersTreeNode.Tag = "disabled"
        oDisabledProvidersTreeNode.ImageIndex = 18
        oDisabledProvidersTreeNode.SelectedImageIndex = 18
        oDisabledProvidersTreeNode.ForeColor = Color.FromArgb(31, 73, 125)


        dtActiveProvider = oProvider.GetProvider_withENM(clsProvider.enmAUSStatus.Active)
        dtBlockedProvider = oProvider.GetProvider_withENM(clsProvider.enmAUSStatus.BlockedProviders)
        dtPendingProvider = oProvider.GetProvider_withENM(clsProvider.enmAUSStatus.PendingForLicense)
        dtApprovedProvider = oProvider.GetProvider_withENM(clsProvider.enmAUSStatus.PendingForReview)
        dtDisabledProvider = oProvider.GetProvider_withENM(clsProvider.enmAUSStatus.DisabledProviders)
        Dim oNode As TreeNode
        If IsNothing(dtActiveProvider) = False Then
            For i As Integer = 0 To dtActiveProvider.Rows.Count - 1
                oNode = New TreeNode
                'oNode.Text = dtActiveProvider.Rows(i)("ProviderName")
                ''Added by Mayuri:20100517-Case No:#4942
                If dtActiveProvider.Rows(i)("MiddleName") <> "" Then
                    oNode.Text = dtActiveProvider.Rows(i)("FirstName") & " " & dtActiveProvider.Rows(i)("MiddleName") & " " & dtActiveProvider.Rows(i)("LastName")
                Else
                    oNode.Text = dtActiveProvider.Rows(i)("FirstName") & " " & dtActiveProvider.Rows(i)("LastName")
                End If
                oNode.Tag = dtActiveProvider.Rows(i)("nProviderID")
                oNode.ImageIndex = 14
                oNode.SelectedImageIndex = 14
                oNode.ForeColor = Color.FromArgb(31, 73, 125)
                oActiveTreeNode.Nodes.Add(oNode)
            Next
        End If

        If IsNothing(dtBlockedProvider) = False Then
            For i As Integer = 0 To dtBlockedProvider.Rows.Count - 1
                oNode = New TreeNode
                'oNode.Text = dtBlockedProvider.Rows(i)("ProviderName")
                ''Added by Mayuri:20100517-Case No:#4942
                If dtBlockedProvider.Rows(i)("MiddleName") <> "" Then
                    oNode.Text = dtBlockedProvider.Rows(i)("FirstName") & " " & dtBlockedProvider.Rows(i)("MiddleName") & " " & dtBlockedProvider.Rows(i)("LastName")
                Else
                    oNode.Text = dtBlockedProvider.Rows(i)("FirstName") & " " & dtBlockedProvider.Rows(i)("LastName")
                End If
                oNode.Tag = dtBlockedProvider.Rows(i)("nProviderID")
                oNode.ImageIndex = 14
                oNode.SelectedImageIndex = 14
                oNode.ForeColor = Color.FromArgb(31, 73, 125)
                oBlockedTreeNode.Nodes.Add(oNode)
            Next
        End If

        If IsNothing(dtPendingProvider) = False Then
            For i As Integer = 0 To dtPendingProvider.Rows.Count - 1
                oNode = New TreeNode
                If dtPendingProvider.Rows(i)("MiddleName") <> "" Then
                    oNode.Text = dtPendingProvider.Rows(i)("FirstName") & " " & dtPendingProvider.Rows(i)("MiddleName") & " " & dtPendingProvider.Rows(i)("LastName")
                Else
                    oNode.Text = dtPendingProvider.Rows(i)("FirstName") & " " & dtPendingProvider.Rows(i)("LastName")
                End If
                oNode.Tag = dtPendingProvider.Rows(i)("nProviderID")
                oNode.ImageIndex = 14
                oNode.SelectedImageIndex = 14
                oNode.ForeColor = Color.FromArgb(31, 73, 125)
                oPendingProvidersTreeNode.Nodes.Add(oNode)
            Next
        End If

        If IsNothing(dtApprovedProvider) = False Then
            For i As Integer = 0 To dtApprovedProvider.Rows.Count - 1
                oNode = New TreeNode
                If dtApprovedProvider.Rows(i)("MiddleName") <> "" Then
                    oNode.Text = dtApprovedProvider.Rows(i)("FirstName") & " " & dtApprovedProvider.Rows(i)("MiddleName") & " " & dtApprovedProvider.Rows(i)("LastName")
                Else
                    oNode.Text = dtApprovedProvider.Rows(i)("FirstName") & " " & dtApprovedProvider.Rows(i)("LastName")
                End If
                oNode.Tag = dtApprovedProvider.Rows(i)("nProviderID")
                oNode.ImageIndex = 14
                oNode.SelectedImageIndex = 14
                oNode.ForeColor = Color.FromArgb(31, 73, 125)
                oApprovedProvidersTreeNode.Nodes.Add(oNode)
            Next
        End If


        If IsNothing(dtDisabledProvider) = False Then
            For i As Integer = 0 To dtDisabledProvider.Rows.Count - 1
                oNode = New TreeNode
                If dtDisabledProvider.Rows(i)("MiddleName") <> "" Then
                    oNode.Text = dtDisabledProvider.Rows(i)("FirstName") & " " & dtDisabledProvider.Rows(i)("MiddleName") & " " & dtDisabledProvider.Rows(i)("LastName")
                Else
                    oNode.Text = dtDisabledProvider.Rows(i)("FirstName") & " " & dtDisabledProvider.Rows(i)("LastName")
                End If
                oNode.Tag = dtDisabledProvider.Rows(i)("nProviderID")
                oNode.ImageIndex = 14
                oNode.SelectedImageIndex = 14
                oNode.ForeColor = Color.FromArgb(31, 73, 125)
                oDisabledProvidersTreeNode.Nodes.Add(oNode)
            Next
        End If


        trvCategory.Nodes.Add(oActiveTreeNode)
        trvCategory.Nodes.Add(oBlockedTreeNode)
        trvCategory.Nodes.Add(oPendingProvidersTreeNode)
        trvCategory.Nodes.Add(oApprovedProvidersTreeNode)
        trvCategory.Nodes.Add(oDisabledProvidersTreeNode)
        ''trvCategory.Sort()
        trvCategory.ExpandAll()

        If Not IsNothing(dtActiveProvider) Then
            dtActiveProvider.Dispose()
            dtActiveProvider = Nothing
        End If
        If Not IsNothing(dtBlockedProvider) Then
            dtBlockedProvider.Dispose()
            dtBlockedProvider = Nothing
        End If
        If Not IsNothing(dtPendingProvider) Then
            dtPendingProvider.Dispose()
            dtPendingProvider = Nothing
        End If
        If Not IsNothing(dtDisabledProvider) Then
            dtDisabledProvider.Dispose()
            dtDisabledProvider = Nothing
        End If
    End Sub

    Private Sub Fill_CategorygloEMRGroups()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "&New"
        btnModify.Visible = True
        btnModify.Text = "&Modify"
        btnDelete.Visible = True
        btnDelete.Text = "&Delete"
        btnDelete.ToolTipText = "Delete"

        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvGroups As TreeNode
            trvGroups = New TreeNode
            With trvGroups
                .Text = "QEMR User Groups"
                .ImageIndex = 0
                .SelectedImageIndex = 0
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes.Add(trvGroups)
            trvGroups = Nothing
            Dim clUserGroups As New Collection
            Dim objUserGroups As New clsUserGroups
            clUserGroups = objUserGroups.PopulateUserGroups
            objUserGroups = Nothing

            Dim nCount As Integer
            For nCount = 1 To clUserGroups.Count
                trvGroups = New TreeNode
                With trvGroups
                    .Text = clUserGroups.Item(nCount)
                    .ImageIndex = 14
                    .SelectedImageIndex = 14
                    .ForeColor = Color.FromArgb(31, 73, 125)
                End With
                .Nodes(0).Nodes.Add(trvGroups)
            Next
            .ExpandAll()
            .EndUpdate()
        End With

    End Sub
    ''code  added by pradeep on 17/06/2010 for prefix
    Private Sub Fill_Prefix()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "&New"
        btnModify.Visible = True
        btnModify.Text = "&Modify"
        btnDelete.Visible = True
        btnDelete.Text = "&Delete"
        btnDelete.ToolTipText = "Delete"
        trvCategory.Visible = False
        Dim dt As New DataTable()
        dt = getPrefix()
        dgData.DataSource = dt

        Dim grdTableStyle As New clsDataGridTableStyle(dt.TableName)

        Dim grdprefixID As New DataGridTextBoxColumn
        grdprefixID.HeaderText = "ID"
        grdprefixID.Alignment = HorizontalAlignment.Left
        grdprefixID.MappingName = "nPrefixID"
        grdprefixID.NullText = ""
        grdprefixID.Width = 0


        Dim grdServer As New DataGridTextBoxColumn
        grdServer.HeaderText = "Server Name"
        grdServer.Alignment = HorizontalAlignment.Left
        grdServer.MappingName = "sServer"
        grdServer.NullText = ""
        grdServer.Width = 0.4 * dgData.Width


        Dim grdDatabase As New DataGridTextBoxColumn
        grdDatabase.HeaderText = "Database Name"
        grdDatabase.Alignment = HorizontalAlignment.Left
        grdDatabase.MappingName = "sDatabase"
        grdDatabase.NullText = ""
        grdDatabase.Width = 0.4 * dgData.Width

        Dim grdPrefix As New DataGridTextBoxColumn
        grdPrefix.HeaderText = "Prefix"
        grdPrefix.Alignment = HorizontalAlignment.Left
        grdPrefix.MappingName = "sPrefix"
        grdPrefix.NullText = ""
        grdPrefix.Width = 0.2 * dgData.Width


        grdTableStyle.GridColumnStyles.Clear()
        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdprefixID, grdServer, grdDatabase, grdPrefix})


        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyle)


    End Sub
    'code added by pradeep 0n 17/06/2010 for 
    Public Function getPrefix() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim dtPrefix As New DataTable()
        Try

            Dim _sqlQuery As String = ""
            oDB.Connect(False)
            _sqlQuery = "Select nPrefixID,sServer,sDatabase,sPrefix from Prefix"
            oDB.Retrive_Query(_sqlQuery, dtPrefix)
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
        Return dtPrefix

    End Function
    'Added for User groups 
    'Sandip Darade 7th Feb 2007
    Private Sub Fill_CategoryUserGroups()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "&New"
        btnModify.Visible = True
        btnModify.Text = "&Modify"
        btnDelete.Visible = True
        btnDelete.Text = "&Delete"
        btnDelete.ToolTipText = "Delete"

        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim oNode As TreeNode
            oNode = New TreeNode
            With oNode
                .Text = "User Groups"
                .ImageIndex = 10
                .SelectedImageIndex = 10
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes.Add(oNode)
            oNode = Nothing


            Dim dt As New DataTable
            Dim objUserGroups As New clsUserGroups
            dt = objUserGroups.GetUserGroups()
            objUserGroups = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    oNode = New TreeNode()
                    If True Then
                        oNode.Text = Convert.ToString(dt.Rows(i)("sGroupName"))
                        oNode.Tag = Convert.ToInt64(dt.Rows(i)("nGroupID"))
                        oNode.ImageIndex = 14
                        oNode.SelectedImageIndex = 14
                    End If
                    'oNode.ForeColor = Color.Black; 
                    trvCategory.Nodes(0).Nodes.Add(oNode)
                Next
            End If
            .ExpandAll()
            .EndUpdate()
        End With

    End Sub



    Private Sub Fill_MultipleDB()
        ''Activating the button
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "New"
        btnModify.Visible = True
        btnModify.Text = "Modify"
        btnDelete.Visible = True
        btnDelete.Text = "Delete"
        btnDelete.ToolTipText = "Delete"

        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()

            Dim trvGroups As TreeNode
            trvGroups = New TreeNode

            trvGroups.Text = "Server Name"
            trvGroups.ImageIndex = 15
            trvGroups.SelectedImageIndex = 15
            trvGroups.ForeColor = Color.DarkBlue
            .Nodes.Add(trvGroups)
            trvGroups = Nothing

            '' SUDHIR 20091226 '' USE LESS CODE ''
            'Dim dtClients As New DataTable
            'Dim objClientSettings As New clsClientMachines
            'dtClients = objClientSettings.Fill_Clients
            'objClientSettings = Nothing
            '' END SUDHIR ''


            Dim dtDatabase As New DataTable

            Dim objDatabase As New ClsMultipleDb

            dtDatabase = objDatabase.Fill_gloServiceServerNameSP()
            objDatabase = Nothing
            If dtDatabase IsNot Nothing Then
                If dtDatabase.Rows.Count > 0 Then
                    Dim nCount As Integer
                    For nCount = 0 To dtDatabase.Rows.Count - 1
                        trvGroups = New TreeNode

                        trvGroups.Text = dtDatabase.Rows(nCount).Item("sServerName")
                        trvGroups.ImageIndex = 14
                        trvGroups.SelectedImageIndex = 14
                        trvGroups.ForeColor = Color.Black
                        .Nodes(0).Nodes.Add(trvGroups)
                    Next
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                End If
            End If

            .ExpandAll()
            .EndUpdate()

        End With
    End Sub
    Public Sub Fill_DetailsMultipleDB()
        If trvCategory.Nodes(0).Nodes.Count > 0 Then
            If trvCategory.SelectedNode IsNot Nothing Then
                Dim objgloServicesDatabase As New ClsMultipleDb
                Dim dtDatabaseValue As New DataTable
                dtDatabaseValue = objgloServicesDatabase.GetServiceDatabaseName(trvCategory.SelectedNode.Text)
                If dtDatabaseValue IsNot Nothing Then
                    If dtDatabaseValue.Rows.Count > 0 Then
                        dgData.DataSource = dtDatabaseValue


                        Dim grdTableStyle As New clsDataGridTableStyle(dtDatabaseValue.TableName)

                        Dim grdColumnPropertyName As New DataGridTextBoxColumn
                        grdColumnPropertyName.HeaderText = "Database Name"
                        grdColumnPropertyName.Alignment = HorizontalAlignment.Left
                        grdColumnPropertyName.MappingName = "Database Name"
                        grdColumnPropertyName.NullText = ""
                        grdColumnPropertyName.Width = 0.5 * dgData.Width - 10


                        Dim grdID As New DataGridTextBoxColumn
                        grdID.HeaderText = "DBConnectionId"
                        grdID.Alignment = HorizontalAlignment.Left
                        grdID.MappingName = "DBConnectionId"
                        grdID.NullText = ""
                        grdID.Width = 0

                        ''taking the column for storing the database ID into datagrid 
                        ''but these column is used for the refrence purpose and kept as hidden
                        ''here only created Column and kept the headder as constant.
                        Dim blnValue As New DataGridTextBoxColumn
                        blnValue.HeaderText = "Is Default"
                        blnValue.Alignment = HorizontalAlignment.Left
                        blnValue.MappingName = "bEnabled"
                        blnValue.NullText = ""
                        blnValue.Width = 0.5 * dgData.Width - 10

                        '''''''Added on 20100701 by sanjog to show UserName and password 
                        Dim grdColumnuserName As New DataGridTextBoxColumn
                        grdColumnuserName.HeaderText = "Database Name"
                        grdColumnuserName.Alignment = HorizontalAlignment.Left
                        grdColumnuserName.MappingName = "SQLUserName"
                        grdColumnuserName.NullText = ""
                        grdColumnuserName.Width = 0


                        Dim grdColumnpass As New DataGridTextBoxColumn
                        grdColumnpass.HeaderText = "Database Name"
                        grdColumnpass.Alignment = HorizontalAlignment.Left
                        grdColumnpass.MappingName = "SQLPassWord"
                        grdColumnpass.NullText = ""
                        grdColumnpass.Width = 0
                        '''''''Added on 20100701 by sanjog to show UserName and password 

                        grdTableStyle.GridColumnStyles.Clear()
                        '''''''Added on 20100701 by sanjog to show UserName and password 
                        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColumnPropertyName, grdID, blnValue, grdColumnuserName, grdColumnpass})
                        '''''''Added on 20100701 by sanjog to show UserName and password 


                        dgData.TableStyles.Clear()
                        dgData.TableStyles.Add(grdTableStyle)

                    End If
                End If

            End If


        End If
    End Sub

    Private Sub Fill_CategorygloEMRUsers()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "&New"
        btnModify.Visible = True
        btnModify.Text = "&Modify"
        btnDelete.Visible = True
        btnDelete.Text = "&Block"
        btnDelete.ToolTipText = "Block"

        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvChild As TreeNode
            trvChild = New TreeNode
            With trvChild
                .Text = "gloEMR Users"
                .ImageIndex = 1
                .SelectedImageIndex = 1
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "Active Users"
                .ImageIndex = 14
                .SelectedImageIndex = 14
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)

            trvChild = New TreeNode
            With trvChild
                .Text = "Blocked Users"
                .ImageIndex = 14
                .SelectedImageIndex = 14
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)


            trvChild = New TreeNode
            With trvChild
                .Text = "All"
                .ImageIndex = 14
                .SelectedImageIndex = 14
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)

            .ExpandAll()
            .EndUpdate()
        End With

    End Sub
    Private Sub Fill_CategoryClinics()

        Dim trvChild As TreeNode
        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            trvChild = New TreeNode
            With trvChild
                .Text = "Clinic"
                .ImageIndex = 2
                .SelectedImageIndex = 2
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes.Add(trvChild)
            trvChild = Nothing
            .ExpandAll()
            .EndUpdate()
        End With

        Dim clClinics As New Collection
        Dim objClinic As New clsClinic
        clClinics = objClinic.PopulateClinic()
        objClinic = Nothing
        Dim nCount As Integer

        For nCount = 1 To clClinics.Count
            trvChild = New TreeNode
            With trvChild
                .Text = clClinics.Item(nCount)
                .ImageIndex = 14
                .SelectedImageIndex = 14
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            trvCategory.Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing
        Next
        trvCategory.ExpandAll()

    End Sub

    Private Sub Fill_CategoryOnlineUpdates()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "Check"
        btnModify.Visible = False
        btnDelete.Visible = False

        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvGroups As TreeNode

            trvGroups = New TreeNode
            With trvGroups
                .Text = "Versions"
                .ImageIndex = 21
                .SelectedImageIndex = 21
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes.Add(trvGroups)
            trvGroups = Nothing

            Dim clOnlineUpdates As New Collection
            Dim objOnlineUpdates As New clsOnlineUpdates
            clOnlineUpdates = objOnlineUpdates.Fill_Versions
            objOnlineUpdates = Nothing

            Dim nCount As Integer
            For nCount = 1 To clOnlineUpdates.Count
                trvGroups = New TreeNode
                With trvGroups
                    .Text = clOnlineUpdates.Item(nCount)
                    .ImageIndex = 21
                    .SelectedImageIndex = 21
                    .ForeColor = Color.FromArgb(31, 73, 125)
                End With
                .Nodes(0).Nodes.Add(trvGroups)
            Next
            trvGroups = New TreeNode
            With trvGroups
                .Text = "All"
                .ImageIndex = 21
                .SelectedImageIndex = 21
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvGroups)
            trvGroups = Nothing
            .ExpandAll()
            .EndUpdate()
        End With

    End Sub

    Private Sub Fill_CategorySuggestions()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "New"
        btnModify.Visible = False
        btnDelete.Visible = True
        btnDelete.Text = "Delete"
        btnDelete.ToolTipText = "Delete"



        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvChild As TreeNode

            trvChild = New TreeNode
            With trvChild
                .Text = "Suggestions"
                .ImageIndex = 22
                .SelectedImageIndex = 22
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes.Add(trvChild)
            trvChild = Nothing


            trvChild = New TreeNode
            With trvChild
                .Text = "Today"
                .ImageIndex = 22
                .SelectedImageIndex = 22
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing


            trvChild = New TreeNode
            With trvChild
                .Text = "Yesterday"
                .ImageIndex = 22
                .SelectedImageIndex = 22
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing


            trvChild = New TreeNode
            With trvChild
                .Text = "Last Week"
                .ImageIndex = 22
                .SelectedImageIndex = 22
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "Last Month"
                .ImageIndex = 22
                .SelectedImageIndex = 22
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "Last Quarter"
                .ImageIndex = 22
                .SelectedImageIndex = 22
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "Last Year"
                .ImageIndex = 22
                .SelectedImageIndex = 22
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "All"
                .ImageIndex = 22
                .SelectedImageIndex = 22
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing
            .ExpandAll()
            .EndUpdate()
        End With

    End Sub
    Private Sub Fill_CategoryBackups()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "&New"
        btnModify.Visible = True
        btnModify.Text = "&Modify"
        btnDelete.Visible = True
        btnDelete.Text = "&Delete"
        btnDelete.ToolTipText = "Delete"


        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()

            Dim trvChild As TreeNode
            trvChild = New TreeNode
            With trvChild
                .Text = "Backup"
                .ImageIndex = 8
                .SelectedImageIndex = 8
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "Today"
                .ImageIndex = 8
                .SelectedImageIndex = 8
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing


            trvChild = New TreeNode
            With trvChild
                .Text = "Yesterday"
                .ImageIndex = 8
                .SelectedImageIndex = 8
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing


            trvChild = New TreeNode
            With trvChild
                .Text = "Last Week"
                .ImageIndex = 8
                .SelectedImageIndex = 8
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "Last Month"
                .ImageIndex = 8
                .SelectedImageIndex = 8
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "Last Quarter"
                .ImageIndex = 8
                .SelectedImageIndex = 8
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "Last Year"
                .ImageIndex = 8
                .SelectedImageIndex = 8
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "All"
                .ImageIndex = 8
                .SelectedImageIndex = 8
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing
            .ExpandAll()
            .EndUpdate()
        End With

    End Sub
    Private Sub Fill_CategoryRestores()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "&New"
        btnModify.Visible = True
        btnModify.Text = "&Modify"
        btnDelete.Visible = True
        btnDelete.Text = "&Delete"
        btnDelete.ToolTipText = "Delete"



        pnlMainMainTop.Visible = True
        'picMainSepMain.Visible = True
        optSelfNotesCategory.Visible = False
        optSelfNotesStatus.Visible = False
        With dtFrom
            .Format = DateTimePickerFormat.Custom
            .CustomFormat = DTFORMAT
            .Value = Date.Now
        End With
        With dtTo
            .Format = DateTimePickerFormat.Custom
            .CustomFormat = DTFORMAT
            .Value = Date.Now
        End With


        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()

            Dim trvChild As TreeNode
            trvChild = New TreeNode
            With trvChild
                .Text = "Backup Type"
                .ImageIndex = 9
                .SelectedImageIndex = 9
                .ForeColor = Color.DarkBlue
            End With
            .Nodes.Add(trvChild)
            trvChild = Nothing


            trvChild = New TreeNode
            With trvChild
                .Text = "Disc"
                .ImageIndex = 9
                .SelectedImageIndex = 9
                .ForeColor = Color.Black
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "Tape"
                .ImageIndex = 9
                .SelectedImageIndex = 9
                .ForeColor = Color.Black
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "All"
                .ImageIndex = 9
                .SelectedImageIndex = 9
                .ForeColor = Color.Black
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            .ExpandAll()
            .EndUpdate()
        End With
    End Sub
    Private Sub Fill_CategorySelfNotes()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "&New"
        btnModify.Visible = True
        btnModify.Text = "&Modify"
        btnDelete.Visible = True
        btnDelete.Text = "&Delete"
        btnDelete.ToolTipText = "Delete"



        pnlMainMainTop.Visible = True
        'picMainSepMain.Visible = True
        optSelfNotesCategory.Visible = True
        optSelfNotesStatus.Visible = True
        With dtFrom
            .Format = DateTimePickerFormat.Custom
            .CustomFormat = DTFORMAT
            .Value = Date.Now
        End With
        With dtTo
            .Format = DateTimePickerFormat.Custom
            .CustomFormat = DTFORMAT
            .Value = Date.Now
        End With
        Call Fill_CategorySelfNotes(enmSelfNotesCategoryStatus.Category)
    End Sub

    Private Sub Fill_CategorySelfNotes(ByVal enmSelfNotesCategoryStatusArg As enmSelfNotesCategoryStatus)
        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvRootNode As New TreeNode
            With trvRootNode
                If enmSelfNotesCategoryStatusArg = enmSelfNotesCategoryStatus.Category Then
                    .Text = "Categories"
                Else
                    .Text = "Status"
                End If

                .ImageIndex = 11
                .SelectedImageIndex = 11
                .ForeColor = Color.DarkBlue
            End With
            .Nodes.Add(trvRootNode)


            Dim clSelfNotesCategory As New Collection
            Dim objSelfNotes As New clsSelfNotes
            If enmSelfNotesCategoryStatusArg = enmSelfNotesCategoryStatus.Category Then
                clSelfNotesCategory = objSelfNotes.PopulateCategory()
            ElseIf enmSelfNotesCategoryStatusArg = enmSelfNotesCategoryStatus.Status Then
                clSelfNotesCategory = objSelfNotes.PopulateStatus
            End If
            objSelfNotes = Nothing
            Dim nCount As Integer
            Dim trvChild As TreeNode
            For nCount = 1 To clSelfNotesCategory.Count
                trvChild = New TreeNode
                With trvChild
                    .Text = clSelfNotesCategory.Item(nCount)
                    .ImageIndex = 11
                    .SelectedImageIndex = 11
                    .ForeColor = Color.Black
                End With
                .Nodes(0).Nodes.Add(trvChild)
                trvChild = Nothing
            Next
            Dim trvChildNode As New TreeNode
            With trvChildNode
                .Text = "All"
                .ImageIndex = 11
                .SelectedImageIndex = 11
                .ForeColor = Color.Black
            End With
            .Nodes(0).Nodes.Add(trvChildNode)

            .ExpandAll()
            .EndUpdate()
        End With


    End Sub
    Private Sub Fill_AuditCategories()
        cmbAuditCategory.Items.Clear()
        Dim objAudit As New clsAudit
        Dim clAudit As New Collection
        clAudit = objAudit.Fill_AuditCategory()
        Dim nCount As Integer
        cmbAuditCategory.Items.Add("All")
        For nCount = 1 To clAudit.Count
            If Not IsDBNull(clAudit.Item(nCount)) Then
                If clAudit.Item(nCount) <> "" Then
                    cmbAuditCategory.Items.Add(clAudit.Item(nCount))
                End If

            End If
        Next
        ' cmbAuditCategory.Items.Add("All")
        cmbAuditCategory.SelectedIndex = 0
    End Sub

    Private Sub Fill_ArchivedAuditCategories()
        cmbAuditCategory.Items.Clear()
        Dim objAudit As New clsAudit
        Dim clAudit As New Collection
        clAudit = objAudit.Fill_ArchivedAuditCategory()
        Dim nCount As Integer
        For nCount = 1 To clAudit.Count
            cmbAuditCategory.Items.Add(clAudit.Item(nCount))
        Next
        cmbAuditCategory.Items.Add("All")
        cmbAuditCategory.SelectedIndex = 0
    End Sub

    'Fill Users for Audit Reports
    Private Sub Fill_CategoryUsers()

        Try


            With trvCategory
                .BeginUpdate()
                .Nodes.Clear()
                Dim trvChild As TreeNode
                trvChild = New TreeNode
                With trvChild
                    .Text = "Users"
                    .ImageIndex = 13
                    .SelectedImageIndex = 13
                    .ForeColor = Color.FromArgb(31, 73, 125)

                End With
                .Nodes.Add(trvChild)
                trvChild = Nothing

                Dim trvRootNode As New TreeNode
                Dim clUsers As New Collection
                Dim objAudit As New clsAudit
                clUsers = objAudit.Fill_Users()
                Dim nCount As Integer


                Dim trvChildNode As New TreeNode
                With trvChildNode
                    .Text = "All"
                    .ImageIndex = 13
                    .SelectedImageIndex = 13
                    .ForeColor = Color.FromArgb(31, 73, 125)
                End With

                .Nodes(0).Nodes.Add(trvChildNode)

                For nCount = 1 To clUsers.Count
                    trvChild = New TreeNode
                    With trvChild
                        .Text = clUsers.Item(nCount)
                        .ImageIndex = 13
                        .SelectedImageIndex = 13
                        .ForeColor = Color.FromArgb(31, 73, 125)
                    End With
                    .Nodes(0).Nodes.Add(trvChild)
                    trvChild = Nothing
                Next


                .ExpandAll()
                .SelectedNode = .Nodes(0).Nodes(0)


            End With
        Catch ex As Exception

        End Try
        trvCategory.EndUpdate()

    End Sub

    'Fill Users for Audit Reports
    Private Sub Fill_CategoryArchivedUsers()
        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Try
                Dim trvChild As TreeNode
                trvChild = New TreeNode
                With trvChild
                    .Text = "Users"
                    .ImageIndex = 13
                    .SelectedImageIndex = 13
                    .ForeColor = Color.FromArgb(31, 73, 125)
                End With
                .Nodes.Add(trvChild)
                trvChild = Nothing

                Dim trvRootNode As New TreeNode
                Dim dtUsers As DataTable
                Dim objAudit As New clsAudit
                dtUsers = objAudit.Fill_ArchivedUsers()
                If (Not IsNothing(dtUsers)) Then
                    Dim nCount As Integer
                    For nCount = 0 To dtUsers.Rows.Count - 1
                        trvChild = New TreeNode
                        With trvChild
                            .Tag = dtUsers.Rows(nCount).Item(0)
                            .Text = dtUsers.Rows(nCount).Item(1)
                            .ImageIndex = 13
                            .SelectedImageIndex = 13
                            .ForeColor = Color.FromArgb(31, 73, 125)
                        End With
                        .Nodes(0).Nodes.Add(trvChild)
                        trvChild = Nothing
                    Next
                    dtUsers = Nothing
                End If

                Dim trvChildNode As New TreeNode
                With trvChildNode
                    .Text = "All"
                    .ImageIndex = 13
                    .SelectedImageIndex = 13
                    .ForeColor = Color.FromArgb(31, 73, 125)
                End With
                .Nodes(0).Nodes.Add(trvChildNode)

            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            .ExpandAll()
            .EndUpdate()
        End With
    End Sub
#End Region

    ''''Private Sub btnAuditReport_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnAuditReport.Image = Image.FromFile(Application.StartupPath & "\Images\orangeauditreport.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try
    ''''End Sub

    ''''Private Sub btnAuditReport_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnAuditReport.Image = Image.FromFile(Application.StartupPath & "\Images\blueauditreport.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try

    ''''End Sub


    ''''Private Sub btnArchiveAudit_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnArchiveAudit.Image = Image.FromFile(Application.StartupPath & "\Images\orangearchiveaudit.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try
    ''''End Sub

    ''''Private Sub btnArchiveAudit_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnArchiveAudit.Image = Image.FromFile(Application.StartupPath & "\Images\bluearchiveaudit.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try
    ''''End Sub

    ''''Private Sub btnDBTools_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnDBTools.Image = Image.FromFile(Application.StartupPath & "\Images\orangedatabasetool.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try

    ''''End Sub

    ''''Private Sub btnDBTools_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnDBTools.Image = Image.FromFile(Application.StartupPath & "\Images\bluedatabasetool.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try

    ''''End Sub

    Private Sub btnHideToolBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHideToolBar.Click
        Try
            pnlLeft.Visible = Not pnlLeft.Visible
            SplitterMain.Visible = Not SplitterMain.Visible
            If Trim(btnHideToolBar.Text) = "Hide Toolbar" Then
                btnHideToolBar.Text = "Show Toolbar"
            Else
                btnHideToolBar.Text = "Hide Toolbar"
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub trvCategory_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCategory.AfterSelect
    '    Try
    '        If enmUserOperation = enmOperation.Admin Then
    '            If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
    '                Exit Sub
    '            End If
    '            Me.Cursor = Cursors.WaitCursor
    '            Select Case Trim(trvAdminMenu.SelectedNode.Text)
    '                Case "Windows Groups & Users"
    '                    Call Fill_DetailsWindowsGroupsUsers()
    '                Case "QEMR Groups"
    '                    Call Fill_DetailsgloEMRGroups()
    '                Case "User Management"
    '                    Call Fill_DetailsgloEMRUsers()
    '                Case "Doctor"
    '                    Call Fill_DetailsProviders()
    '                Case "Client Settings"
    '                    Call Fill_DetailsClientSettings()
    '                Case "Clinic"
    '                    Call Fill_DetailsClinics()
    '                Case "Backup"
    '                    Call Fill_DetailsBackups()
    '                Case "Restore"
    '                    Call Fill_DetailsRestores()
    '                Case "Self Notes"
    '                    Call Fill_DetailsSelfNotes()
    '            End Select
    '        ElseIf enmUserOperation = enmOperation.Audit Then
    '            If Trim(trvAudit.SelectedNode.Text) = "Audit" Then
    '                Me.Cursor = Cursors.Default
    '                Exit Sub
    '            End If
    '            Select Case Trim(trvAudit.SelectedNode.Text)
    '                Case "Report"
    '                    Call Fill_DetailsAuditReports()
    '                Case "Archived Audit Report"
    '                    Call Fill_DetailsArchivedAuditReports()
    '            End Select
    '        ElseIf enmUserOperation = enmOperation.Tools Then
    '            If Trim(trvTools.SelectedNode.Text) = "Tools" Then
    '                Me.Cursor = Cursors.Default
    '                Exit Sub
    '            End If
    '            Select Case Trim(trvTools.SelectedNode.Text)
    '                Case "Client Message"
    '                    Call Fill_DetailsClientMessages()
    '                Case "Online Updates"
    '                    Call Fill_DetailsOnlineUpdates()
    '                Case "Suggestions to gloStream"
    '                    Call Fill_DetailsSuggestions()
    '            End Select
    '        End If
    '        Me.Cursor = Cursors.Default
    '    Catch objErr As Exception
    '        Me.Cursor = Cursors.Default
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

#Region "  Fill Categories Details  "

    Private Sub Fill_DetailsWindowsGroupsUsers()


        'ApplyGridStyle()



        If Trim(trvCategory.SelectedNode.Text) = "Windows" Or Trim(trvCategory.SelectedNode.Text) = "Groups" Or Trim(trvCategory.SelectedNode.Text) = "Users" Then
            Exit Sub
        End If
        Dim dtWindowsGroupsUsers As New DataTable
        Dim objWindowsGroupsUsers As New clsWindowsGroupsUsers
        If Trim(trvCategory.SelectedNode.Parent.Text) = "Groups" Then
            dtWindowsGroupsUsers = objWindowsGroupsUsers.PopulateGroupsUsersInformation(clsWindowsGroupsUsers.enmWindowsGroupsUsers.Groups, Trim(trvCategory.SelectedNode.Text))
        ElseIf Trim(trvCategory.SelectedNode.Parent.Text) = "Users" Then
            dtWindowsGroupsUsers = objWindowsGroupsUsers.PopulateGroupsUsersInformation(clsWindowsGroupsUsers.enmWindowsGroupsUsers.Users, Trim(trvCategory.SelectedNode.Text))
        End If
        dgData.DataSource = dtWindowsGroupsUsers
        dgData.CaptionText = "Windows Groups & Users"

        Dim grdTableStyle As New clsDataGridTableStyle(dtWindowsGroupsUsers.TableName)

        Dim grdColStyleNotesID As New DataGridTextBoxColumn
        With grdColStyleNotesID
            .HeaderText = "Property Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtWindowsGroupsUsers.Columns(0).ColumnName
            .NullText = ""
            .Width = 0.5 * dgData.Width
        End With

        Dim grdColStyleNotesDate As New DataGridTextBoxColumn
        With grdColStyleNotesDate
            .HeaderText = "Property Value"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtWindowsGroupsUsers.Columns(1).ColumnName
            .NullText = ""
            .Width = 0.5 * dgData.Width - 10
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleNotesID, grdColStyleNotesDate})
        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyle)


        objWindowsGroupsUsers = Nothing

        '------------------------------------
        'Sarika 21st April 2007
        Dim objAudit As New clsAudit
        objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The details of Windows group : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
        objAudit = Nothing
        '------------------------------------
    End Sub

    Public Sub ApplyGridStyle()
        Me.dgData.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgData.BackColor = System.Drawing.Color.GhostWhite
        Me.dgData.BackgroundColor = System.Drawing.Color.GhostWhite

        Me.dgData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgData.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgData.CaptionFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgData.CaptionForeColor = System.Drawing.Color.White
        Me.dgData.CaptionVisible = False
        Me.dgData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgData.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgData.ForeColor = System.Drawing.Color.Black
        Me.dgData.FullRowSelect = True
        Me.dgData.GridLineColor = System.Drawing.Color.Black
        Me.dgData.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgData.HeaderForeColor = System.Drawing.Color.White
        Me.dgData.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgData.Location = New System.Drawing.Point(170, 62)
        Me.dgData.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgData.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgData.RowHeadersVisible = False
        Me.dgData.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgData.SelectionForeColor = System.Drawing.Color.Black
        Me.dgData.Size = New System.Drawing.Size(652, 547)
        Me.dgData.TabIndex = 10
    End Sub

    Private Sub Fill_DetailsOnlineUpdates()
        Dim objOnlineUpdates As New clsOnlineUpdates
        Dim dtOnlineUpdates As New DataTable
        dtOnlineUpdates = objOnlineUpdates.ViewOnlineUpdates(Trim(trvCategory.SelectedNode.Text))
        objOnlineUpdates = Nothing
        dgData.DataSource = dtOnlineUpdates
        dgData.CaptionText = "Online Updates"


        Dim grdTableStyle As New clsDataGridTableStyle(dtOnlineUpdates.TableName)

        Dim grdColUpdatesID As New DataGridTextBoxColumn
        With grdColUpdatesID
            .HeaderText = "Updates ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtOnlineUpdates.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColUpdateDate As New DataGridTextBoxColumn
        With grdColUpdateDate
            .HeaderText = "Update Date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtOnlineUpdates.Columns(1).ColumnName
            .NullText = ""
            .Width = 100
        End With

        Dim grdColVersionNo As New DataGridTextBoxColumn
        With grdColVersionNo
            .HeaderText = "Version No"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtOnlineUpdates.Columns(2).ColumnName
            .NullText = ""
            .Width = 100
        End With


        Dim grdColComments As New DataGridTextBoxColumn
        With grdColComments
            .HeaderText = "Comments"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtOnlineUpdates.Columns(3).ColumnName
            .NullText = ""
            .Width = 370
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColUpdatesID, grdColUpdateDate, grdColVersionNo, grdColComments})
        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyle)

        ''------------------------------------
        ''Sarika 21st April 2007
        'Dim objAudit As New clsAudit
        'objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The details of Windows group : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
        'objAudit = Nothing
        ''------------------------------------

    End Sub

    Private Sub Fill_DetailsClientMessages()
        Dim objClientMessage As New clsClientMessage
        Dim dtClientMessage As New DataTable
        dtClientMessage = objClientMessage.ViewMessages(dtFrom.Value.Date, dtTo.Value.Date, Trim(trvCategory.SelectedNode.Text))
        objClientMessage = Nothing

        dgData.DataSource = dtClientMessage
        dgData.CaptionText = "Client Messages"

        '---------------------------------------
        'Sarika 21st April
        If dtClientMessage.Rows.Count > 0 Then
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Query, "The Client message data from '" & dtFrom.Value.Date & "' and '" & dtTo.Value.Date & "' is queried.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
        End If
        '-------------------------------------------


        Dim grdTableStyle As New clsDataGridTableStyle(dtClientMessage.TableName)

        Dim grdColClientMessageID As New DataGridTextBoxColumn
        With grdColClientMessageID
            .HeaderText = "Message ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtClientMessage.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColFromDate As New DataGridTextBoxColumn
        With grdColFromDate
            .HeaderText = "From Date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtClientMessage.Columns(1).ColumnName
            .NullText = ""
            .Width = 150
        End With

        Dim grdColToDate As New DataGridTextBoxColumn
        With grdColToDate
            .HeaderText = "To Date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtClientMessage.Columns(2).ColumnName
            .NullText = ""
            .Width = 150
        End With


        Dim grdColMessageCategory As New DataGridTextBoxColumn
        With grdColMessageCategory
            .HeaderText = "Category"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtClientMessage.Columns(3).ColumnName
            .NullText = ""
            .Width = 150
        End With

        Dim grdColDescription As New DataGridTextBoxColumn
        With grdColDescription
            .HeaderText = "Message"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtClientMessage.Columns(4).ColumnName
            .NullText = ""
            .Width = 300
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColClientMessageID, grdColFromDate, grdColToDate, grdColMessageCategory, grdColDescription})
        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyle)

        ''------------------------------------
        ''Sarika 21st April 2007
        'Dim objAudit As New clsAudit
        'objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The details of Client : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
        'objAudit = Nothing
        ''------------------------------------

    End Sub


    '   sarika Audit Log Instr Search

    Private Sub Fill_DetailsAuditReports()
        Dim objAuditReports As New clsAudit
        Dim dtAuditReports As New DataTable
        Try
            Dim _strSQL As String = ""
            If Trim(txtPatient.Text) = "" Then
                dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text)
            ElseIf Trim(cmbSearchPatient.Text) = "Patient Code" Then
                'chk whether the patient code entered is valid or not
                Dim isvalid As Integer = 0

                _strSQL = "select count(*) from Patient where sPatientCode='" & txtPatient.Text.Trim.ToString() & "'"
                isvalid = IsPatientExists(_strSQL)

                If isvalid > 0 Then
                    'sarika 25th apr 2007
                    dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text, txtPatient.Text)
                    'If IsNumeric(txtPatient.Text) = True Then
                    '    dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text, CInt(txtPatient.Text))
                    '    '-----------------------------------------------
                    '    'Sarika 24th apr 2007
                    'Else
                    '    MessageBox.Show("Please enter valid PatientID.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    txtPatient.Text = ""
                    '    txtPatient.Focus()
                    '    Exit Sub
                    '    '------------------------------------------------
                    'End If
                Else
                    MessageBox.Show("Please enter valid Patient Code.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPatient.Text = ""
                    txtPatient.Focus()
                    Exit Sub
                End If
            ElseIf Trim(cmbSearchPatient.Text) = "First Name" Then
                Dim isvalid As Integer = 0

                _strSQL = "select count(*) from Patient where sFirstName='" & txtPatient.Text.Trim.ToString() & "'"
                isvalid = IsPatientExists(_strSQL)
                If isvalid > 0 Then
                    dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text, , txtPatient.Text)
                Else
                    MessageBox.Show("Please enter valid Patient First Name.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPatient.Text = ""
                    txtPatient.Focus()
                    Exit Sub
                End If

            ElseIf Trim(cmbSearchPatient.Text) = "Last Name" Then

                Dim isvalid As Integer = 0

                _strSQL = "select count(*) from Patient where sLastName='" & txtPatient.Text.Trim.ToString() & "'"
                isvalid = IsPatientExists(_strSQL)
                If isvalid > 0 Then
                    dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text, , , txtPatient.Text)
                Else
                    MessageBox.Show("Please enter valid Patient Last Name.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPatient.Text = ""
                    txtPatient.Focus()
                    Exit Sub
                End If
            End If


            '-----------------------------------------------------------
            'Sarika 21st April 2007
            If dtAuditReports.Rows.Count > 0 Then
                Dim _logText As String = ""

                _logText = "Queried audit data against Category : '" & cmbAuditCategory.SelectedItem & "', from  : '" & dtFrom.Value.Date & "' to : '" & dtTo.Value.Date & "'"
                If Trim(txtPatient.Text) <> "" Then
                    Dim _tstr As String = cmbSearchPatient.Text
                    _logText &= "And " & _tstr & " : '" & txtPatient.Text & "'"
                End If

                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.Query, "Audit Data was queried." & _logText, gstrLoginName, gstrClientMachineName)
                objAudit = Nothing

                '-----------------------------------------------------------

                dgData.DataSource = dtAuditReports.DefaultView
                dgData.CaptionText = "Audit Report"
            Else
                dgData.DataSource = Nothing
                dgData.CaptionText = "Audit Report"

            End If

            'AuditTrailID,ActivityDate,Category, CategoryDescription, PatientCode ,PatientName ,UserName,MachineName

            Dim grdTableStyle As New clsDataGridTableStyle(dtAuditReports.TableName)

            Dim grdColAuditReportID As New DataGridTextBoxColumn
            With grdColAuditReportID
                .HeaderText = "Reports ID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("AuditTrailID").ColumnName
                .NullText = ""
                .Width = 0
            End With

            Dim grdColActivityDate As New DataGridTextBoxColumn
            With grdColActivityDate
                .HeaderText = "Activity Date"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("ActivityDate").ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColSoftwareComp As New DataGridTextBoxColumn
            With grdColSoftwareComp
                .HeaderText = "Software Component"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("sSoftwareComponent").ColumnName
                .NullText = ""
                .Width = 100
            End With


            Dim grdColActivityCategory As New DataGridTextBoxColumn
            With grdColActivityCategory
                .HeaderText = "Category"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("Category").ColumnName
                .NullText = ""
                .Width = 100
            End With



            Dim grdColPatientCode As New DataGridTextBoxColumn
            With grdColPatientCode
                .HeaderText = "Patient Code"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("PatientCode").ColumnName
                .NullText = ""
                .Width = 100
            End With
            Dim grdColOutcome As New DataGridTextBoxColumn
            With grdColOutcome
                .HeaderText = "Outcome"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("sOutcome").ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColPatient As New DataGridTextBoxColumn
            With grdColPatient
                .HeaderText = "Patient"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("PatientName").ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColDescription As New DataGridTextBoxColumn
            With grdColDescription
                .HeaderText = "Description"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("CategoryDescription").ColumnName
                .NullText = ""
                .Width = 200
            End With

            Dim grdColUser As New DataGridTextBoxColumn
            With grdColUser
                .HeaderText = "User"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("UserName").ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColMachine As New DataGridTextBoxColumn
            With grdColMachine
                .HeaderText = "Machine"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("MachineName").ColumnName
                .NullText = ""
                .Width = 100
            End With


            'grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColAuditReportID, grdColActivityDate, grdColActivityCategory, grdColDescription, grdColPatientCode, grdColPatient, grdColUser, grdColMachine, grdColSoftwareComp, grdColOutcome})
            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColAuditReportID, grdColActivityDate, grdColSoftwareComp, grdColMachine, grdColUser, grdColActivityCategory, grdColPatientCode, grdColPatient, grdColDescription, grdColOutcome})
            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    'Private Sub Fill_DetailsAuditReports()
    '    Dim objAuditReports As New clsAudit
    '    Dim dtAuditReports As New DataTable

    '    Try
    '        Dim _strSQL As String = ""

    '        dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text, _strSQL)



    '        '-----------------------------------------------------------
    '        'Sarika 21st April 2007
    '        If dtAuditReports.Rows.Count > 0 Then
    '            Dim _logText As String = ""

    '            _logText = "Queried audit data against Category : '" & cmbAuditCategory.SelectedItem & "', from  : '" & dtFrom.Value.Date & "' to : '" & dtTo.Value.Date & "'"
    '            If Trim(txtPatient.Text) <> "" Then
    '                Dim _tstr As String = cmbSearchPatient.Text
    '                _logText &= "And " & _tstr & " : '" & txtPatient.Text & "'"
    '            End If

    '            Dim objAudit As New clsAudit
    '            objAudit.CreateLog(clsAudit.enmActivityType.Query, "Audit Data was queried." & _logText, gstrLoginName, gstrClientMachineName)
    '            objAudit = Nothing

    '            '-----------------------------------------------------------

    '            dgData.DataSource = dtAuditReports.DefaultView
    '            dgData.CaptionText = "Audit Report"
    '        Else
    '            dgData.DataSource = Nothing
    '            dgData.CaptionText = "Audit Report"

    '        End If

    '        'AuditTrailID,ActivityDate,Category, CategoryDescription, PatientCode ,PatientName ,UserName,MachineName

    '        Dim grdTableStyle As New clsDataGridTableStyle(dtAuditReports.TableName)

    '        Dim grdColAuditReportID As New DataGridTextBoxColumn
    '        With grdColAuditReportID
    '            .HeaderText = "Reports ID"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtAuditReports.Columns("AuditTrailID").ColumnName
    '            .NullText = ""
    '            .Width = 0
    '        End With

    '        Dim grdColActivityDate As New DataGridTextBoxColumn
    '        With grdColActivityDate
    '            .HeaderText = "Activity Date"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtAuditReports.Columns("ActivityDate").ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColSoftwareComp As New DataGridTextBoxColumn
    '        With grdColSoftwareComp
    '            .HeaderText = "Software Component"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtAuditReports.Columns("sSoftwareComponent").ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With


    '        Dim grdColActivityCategory As New DataGridTextBoxColumn
    '        With grdColActivityCategory
    '            .HeaderText = "Category"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtAuditReports.Columns("Category").ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With



    '        Dim grdColPatientCode As New DataGridTextBoxColumn
    '        With grdColPatientCode
    '            .HeaderText = "Patient Code"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtAuditReports.Columns("PatientCode").ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColOutcome As New DataGridTextBoxColumn
    '        With grdColOutcome
    '            .HeaderText = "Outcome"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtAuditReports.Columns("sOutcome").ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColPatient As New DataGridTextBoxColumn
    '        With grdColPatient
    '            .HeaderText = "Patient"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtAuditReports.Columns("PatientName").ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColDescription As New DataGridTextBoxColumn
    '        With grdColDescription
    '            .HeaderText = "Description"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtAuditReports.Columns("CategoryDescription").ColumnName
    '            .NullText = ""
    '            .Width = 200
    '        End With

    '        Dim grdColUser As New DataGridTextBoxColumn
    '        With grdColUser
    '            .HeaderText = "User"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtAuditReports.Columns("UserName").ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColMachine As New DataGridTextBoxColumn
    '        With grdColMachine
    '            .HeaderText = "Machine"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtAuditReports.Columns("MachineName").ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With


    '        'grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColAuditReportID, grdColActivityDate, grdColActivityCategory, grdColDescription, grdColPatientCode, grdColPatient, grdColUser, grdColMachine, grdColSoftwareComp, grdColOutcome})
    '        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColAuditReportID, grdColActivityDate, grdColSoftwareComp, grdColMachine, grdColUser, grdColActivityCategory, grdColPatientCode, grdColPatient, grdColDescription, grdColOutcome})
    '        dgData.TableStyles.Clear()
    '        dgData.TableStyles.Add(grdTableStyle)

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try
    'End Sub


    '   sarika Audit Log Instr Search

    Public Function IsPatientExists(ByVal sqlstr As String) As Integer
        Dim conn As New SqlConnection
        conn.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim cmd As SqlCommand

        Try
            conn.Open()

            cmd = New SqlCommand

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = sqlstr

            Dim isvalid As Integer = cmd.ExecuteScalar()

            Return isvalid

        Catch ex As Exception
            MessageBox.Show("Error while checking Patient exists or not. Error is : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            conn.Close()
        End Try
    End Function


    'Temporary Report Log Code

    Private Sub Fill_DetailsAuditReportsLog()
        Dim objAuditReports As New clsAudit
        Dim dtAuditReports As New DataTable
        Try
            If Trim(txtPatient.Text) = "" Then
                dtAuditReports = objAuditReports.RetrieveAuditReportsLog(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text)
            Else
                If Trim(cmbSearchPatient.Text) = "Patient Code" Then
                    'sarika 25th apr 2007
                    dtAuditReports = objAuditReports.RetrieveAuditReportsLog(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text, txtPatient.Text)
                    'If IsNumeric(txtPatient.Text) = True Then
                    '    dtAuditReports = objAuditReports.RetrieveAuditReportsLog(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text, CInt(txtPatient.Text))
                    '    '-----------------------------------------------
                    '    'Sarika 24th apr 2007
                    'Else
                    '    MessageBox.Show("Please enter valid PatientID.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    txtPatient.Text = ""
                    '    txtPatient.Focus()
                    '    Exit Sub
                    '    '------------------------------------------------
                    'End If
                ElseIf Trim(cmbSearchPatient.Text) = "First Name" Then
                    dtAuditReports = objAuditReports.RetrieveAuditReportsLog(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text, , txtPatient.Text)
                ElseIf Trim(cmbSearchPatient.Text) = "Last Name" Then
                    dtAuditReports = objAuditReports.RetrieveAuditReportsLog(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text, , , txtPatient.Text)
                End If
            End If

            '-----------------------------------------------------------
            'Sarika 21st April 2007
            If dtAuditReports.Rows.Count > 0 Then
                Dim _logText As String = ""

                _logText = "Queried audit data against Category : '" & cmbAuditCategory.SelectedItem & "', from  : '" & dtFrom.Value.Date & "' to : '" & dtTo.Value.Date & "'"
                If Trim(txtPatient.Text) <> "" Then
                    Dim _tstr As String = cmbSearchPatient.Text
                    _logText &= "And " & _tstr & " : '" & txtPatient.Text & "'"
                End If

                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.Query, "Audit Data was queried." & _logText, gstrLoginName, gstrClientMachineName)
                objAudit = Nothing

                '-----------------------------------------------------------

                dgData.DataSource = dtAuditReports.DefaultView
                dgData.CaptionText = "Audit Report"
            Else
                dgData.DataSource = Nothing
                dgData.CaptionText = "Audit Report"

            End If

            'AuditTrailID,ActivityDate,Category, CategoryDescription, PatientCode ,PatientName ,UserName,MachineName

            Dim grdTableStyle As New clsDataGridTableStyle(dtAuditReports.TableName)

            Dim grdColAuditReportID As New DataGridTextBoxColumn
            With grdColAuditReportID
                .HeaderText = "Reports ID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("AuditTrailID").ColumnName
                .NullText = ""
                .Width = 0
            End With

            Dim grdColActivityDate As New DataGridTextBoxColumn
            With grdColActivityDate
                .HeaderText = "Activity Date"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("ActivityDate").ColumnName
                .NullText = ""
                .Width = 100
            End With


            Dim grdColActivityCategory As New DataGridTextBoxColumn
            With grdColActivityCategory
                .HeaderText = "Category"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("Category").ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColDescription As New DataGridTextBoxColumn
            With grdColDescription
                .HeaderText = "Description"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("CategoryDescription").ColumnName
                .NullText = ""
                .Width = 200
            End With

            Dim grdColPatientCode As New DataGridTextBoxColumn
            With grdColPatientCode
                .HeaderText = "Patient Code"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("PatientCode").ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColPatient As New DataGridTextBoxColumn
            With grdColPatient
                .HeaderText = "Patient"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("PatientName").ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColUser As New DataGridTextBoxColumn
            With grdColUser
                .HeaderText = "User"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("UserName").ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColMachine As New DataGridTextBoxColumn
            With grdColMachine
                .HeaderText = "Machine"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("MachineName").ColumnName
                .NullText = ""
                .Width = 100
            End With

            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColAuditReportID, grdColActivityDate, grdColActivityCategory, grdColDescription, grdColPatientCode, grdColPatient, grdColUser, grdColMachine})
            'dgDataLog.TableStyles.Clear()
            'dgDataLog.TableStyles.Add(grdTableStyle)

            If Not dtAuditReports Is Nothing Then
                Dim dtAudits As New DataTable
                dtAudits = dtAuditReports
                If dtAudits.Rows.Count <= 0 Then Exit Sub
                Dim strFileName As String
                With SaveFileDialog1
                    .Filter = "Text File(*.txt)|*.txt"
                    .OverwritePrompt = True
                    .ShowHelp = False
                    .Title = "Select Path to store Audit Log"
                End With
                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                    strFileName = SaveFileDialog1.FileName
                    '----------------------------
                    'sarika 21st May 07
                Else
                    Exit Sub
                    '----------------------------
                End If

                If strFileName.EndsWith(".txt") = False Then
                    MessageBox.Show("Please enter Text file only", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If


                If System.IO.File.Exists(strFileName) = True Then System.IO.File.Delete(strFileName)
                'Export Data in Text File
                Dim nCount As Integer
                Dim objFile As New System.IO.StreamWriter(strFileName)
                Dim strAuditLine As String
                For nCount = 0 To dtAudits.Rows.Count - 1
                    strAuditLine = ""
                    If IsDBNull(dtAudits.Rows(nCount).Item(1)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(1)
                        ' strAuditLine = Format(CType(strAuditLine, DateTime), "MM/dd/yyyy hh:mm:ss")
                    End If
                    strAuditLine = strAuditLine & vbTab

                    If IsDBNull(dtAudits.Rows(nCount).Item(2)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(2)
                    End If
                    strAuditLine = strAuditLine & vbTab

                    If IsDBNull(dtAudits.Rows(nCount).Item(3)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(3)
                    End If
                    strAuditLine = strAuditLine & vbTab

                    If IsDBNull(dtAudits.Rows(nCount).Item(4)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(4)
                    End If
                    strAuditLine = strAuditLine & vbTab

                    If IsDBNull(dtAudits.Rows(nCount).Item(5)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(5)
                    End If
                    strAuditLine = strAuditLine & vbTab

                    If IsDBNull(dtAudits.Rows(nCount).Item(6)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(6)
                    End If
                    strAuditLine = strAuditLine & vbTab

                    'sarika 27th apr 2007

                    If IsDBNull(dtAudits.Rows(nCount).Item(7)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(7)
                    End If
                    strAuditLine = strAuditLine & vbTab

                    If IsDBNull(dtAudits.Rows(nCount).Item(8)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(8)
                    End If
                    strAuditLine = strAuditLine & vbTab

                    If IsDBNull(dtAudits.Rows(nCount).Item(9)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(9)
                    End If
                    strAuditLine = strAuditLine
                    objFile.WriteLine(strAuditLine)
                Next
                objFile.Close()
                MessageBox.Show("Audit Log has been successfully exported at " & strFileName, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                'sarika 21st feb
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has exported Audit Log at " & strFileName, gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Private Sub Fill_DetailsDBUpdations()

        Dim dtDBUpdation As New DataTable
        Dim objDBUpdation As New clsDBUpdation(gstrConnectionString)
        If Trim(trvCategory.SelectedNode.Text) = "All" Then
            dtDBUpdation = objDBUpdation.Fill_DBUpdations()
        Else
            dtDBUpdation = objDBUpdation.Fill_DBUpdations(trvCategory.SelectedNode.Text)
        End If

        objDBUpdation = Nothing
        dgData.DataSource = dtDBUpdation.DefaultView
        dgData.CaptionText = "DB Updations"


        Dim grdTableStyle As New clsDataGridTableStyle(dtDBUpdation.TableName)

        Dim grdColDBUpdationID As New DataGridTextBoxColumn
        With grdColDBUpdationID
            .HeaderText = "ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtDBUpdation.Columns("DBUpdationID").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColDBVersion As New DataGridTextBoxColumn
        With grdColDBVersion
            .HeaderText = "DB Version"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtDBUpdation.Columns("DBVersion").ColumnName
            .NullText = ""
            If Trim(trvCategory.SelectedNode.Text) <> "All" Then
                .Width = 0
            Else
                .Width = dgData.Width / 3 - 100
            End If

        End With


        Dim grdColUpdatedBy As New DataGridTextBoxColumn
        With grdColUpdatedBy
            .HeaderText = "Updated By"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtDBUpdation.Columns("UpdatedBy").ColumnName
            .NullText = ""
            If Trim(trvCategory.SelectedNode.Text) <> "All" Then
                .Width = dgData.Width / 2 - 50
            Else
                .Width = dgData.Width / 3 - 50
            End If
        End With

        Dim grdColUpdateDate As New DataGridTextBoxColumn
        With grdColUpdateDate
            .HeaderText = "Update Date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtDBUpdation.Columns("UpdateDate").ColumnName
            .NullText = ""
            If Trim(trvCategory.SelectedNode.Text) <> "All" Then
                .Width = dgData.Width / 2 - 100
            Else
                .Width = dgData.Width / 3
            End If
        End With


        Dim grdColLogFile As New DataGridTextBoxColumn
        With grdColLogFile
            .HeaderText = "Log File"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtDBUpdation.Columns("LogFile").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColSuccess As New DataGridTextBoxColumn
        With grdColSuccess
            .HeaderText = "Successfully Executed"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtDBUpdation.Columns("SuccessfullyExecuted").ColumnName
            .NullText = ""
            .Width = 150
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColDBUpdationID, grdColDBVersion, grdColUpdatedBy, grdColUpdateDate, grdColLogFile, grdColSuccess})
        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyle)

    End Sub

    Private Sub Fill_DetailsArchivedAuditReports()

        Dim objAuditReports As New clsAudit
        Dim dtAuditReports As New DataTable
        If Trim(txtPatient.Text) = "" Then
            dtAuditReports = objAuditReports.RetrieveArchivedAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Tag)
        Else
            If IsNumeric(txtPatient.Text) = True Then
                Dim isvalid As Integer = 0
                isvalid = IsPatientExists("select count(*) from Patient where nPatientID=" & txtPatient.Text.Trim())
                If isvalid > 0 Then
                    '******By Sandip Deshmukh 24 Oct 07 12.14PM Bug# 242
                    '******in following line the last param converted to Clng as Cint through overflow exception
                    dtAuditReports = objAuditReports.RetrieveArchivedAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Tag, CLng(txtPatient.Text))
                    '******24 Oct 07 12.14PM Bug# 242
                Else
                    MessageBox.Show("Patient with this PatientID do not exists.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPatient.Text = ""
                    txtPatient.Focus()
                    Exit Sub
                End If
                '******By Sanip Deshmukh 24 Oct 2007 10.15a.m. Bug# 242
                '******the following code is added as the code throws unadled exception for non Numeric field of patient ID
            Else
                MessageBox.Show("Please enter valid PatientID.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPatient.Text = ""
                txtPatient.Focus()
                Exit Sub
                '******24 Oct 2007 10.15a.m. Bug# 242
            End If
        End If
        dgData.DataSource = dtAuditReports.DefaultView
        dgData.CaptionText = "Audit Report"
        '-----------------------------------------------------------
        'Sarika 21st April 2007
        If dtAuditReports.Rows.Count > 0 Then
            Dim _logText As String = ""

            _logText = "Queried Archived audit data against Category : '" & cmbAuditCategory.SelectedItem & "', from  : '" & dtFrom.Value.Date & "' to : '" & dtTo.Value.Date & "'"
            If Trim(txtPatient.Text) <> "" Then
                _logText &= "And the cmbSearchPatient.Text : '" & txtPatient.Text & "'"
            End If

            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Query, "Archived Audit Data was queried." & _logText, gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
        End If
        '-----------------------------------------------------------

        Dim grdTableStyle As New clsDataGridTableStyle(dtAuditReports.TableName)
        Dim grdColAuditReportID As New DataGridTextBoxColumn
        With grdColAuditReportID
            .HeaderText = "Reports ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtAuditReports.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColActivityDate As New DataGridTextBoxColumn
        With grdColActivityDate
            .HeaderText = "Activity Date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtAuditReports.Columns(1).ColumnName
            .NullText = ""
            .Width = 100
        End With


        Dim grdColActivityCategory As New DataGridTextBoxColumn
        With grdColActivityCategory
            .HeaderText = "Category"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtAuditReports.Columns(2).ColumnName
            .NullText = ""
            .Width = 100
        End With

        Dim grdColDescription As New DataGridTextBoxColumn
        With grdColDescription
            .HeaderText = "Description"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtAuditReports.Columns(3).ColumnName
            .NullText = ""
            .Width = 200
        End With

        Dim grdColPatient As New DataGridTextBoxColumn
        With grdColPatient
            .HeaderText = "Patient ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtAuditReports.Columns(4).ColumnName
            .NullText = ""
            .Width = 100
        End With

        Dim grdColUser As New DataGridTextBoxColumn
        With grdColUser
            .HeaderText = "User"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtAuditReports.Columns(5).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColMachine As New DataGridTextBoxColumn
        With grdColMachine
            .HeaderText = "Machine"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtAuditReports.Columns(6).ColumnName
            .NullText = ""
            .Width = 100
        End With

        Dim grdColSoftwareComponent As New DataGridTextBoxColumn
        With grdColSoftwareComponent
            .HeaderText = "Software Component"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtAuditReports.Columns(7).ColumnName
            .NullText = ""
            .Width = 100
        End With

        Dim grdColOutcome As New DataGridTextBoxColumn
        With grdColOutcome
            .HeaderText = "Outcome"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtAuditReports.Columns(8).ColumnName
            .NullText = ""
            .Width = 100
        End With

        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColAuditReportID, grdColActivityDate, grdColActivityCategory, grdColDescription, grdColPatient, grdColUser, grdColMachine, grdColSoftwareComponent, grdColOutcome})
        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyle)

    End Sub

    Private Sub Fill_DetailsgloEMRGroups()
        If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then
            Dim objgloEMRGroups As New clsUserGroups
            Dim dtgloEMRGroups As New DataTable
            dtgloEMRGroups = objgloEMRGroups.PopulateUserGroupsRights(Trim(trvCategory.SelectedNode.Text))
            If dtgloEMRGroups.Rows.Count <> 0 Then
                dgData.DataSource = dtgloEMRGroups
                dgData.CaptionText = "QEMR Groups"
            End If


            Dim grdTableStyle As New clsDataGridTableStyle(dtgloEMRGroups.TableName)

            Dim grdColStyleNotesID As New DataGridTextBoxColumn
            With grdColStyleNotesID
                .HeaderText = "User Rights"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtgloEMRGroups.Columns(0).ColumnName
                .NullText = ""
                .Width = dgData.Width - 10
            End With


            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleNotesID})
            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)

            'Sarika 21st April 2007
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "User Rights of User Group : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '------------------------------------

        End If

    End Sub
    'Added for User groups
    'Sandip Darade 7th Feb 2009 
    Private Sub Fill_DetailsUserGroups()

        If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then
            Dim objgloEMRGroups As New clsUserGroups
            Dim dtgloEMRGroups As New DataTable
            dtgloEMRGroups = objgloEMRGroups.GetGroupUsers(Convert.ToInt64(trvCategory.SelectedNode.Tag))

            If dtgloEMRGroups.Rows.Count <> 0 Then
                dgData.DataSource = dtgloEMRGroups
                dgData.CaptionText = "User Groups"
            Else
                dgData.DataSource = Nothing
            End If


            Dim grdTableStyle As New clsDataGridTableStyle(dtgloEMRGroups.TableName)

            Dim grdColStyleNotesID As New DataGridTextBoxColumn
            With grdColStyleNotesID
                .HeaderText = "Users"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtgloEMRGroups.Columns(0).ColumnName
                .NullText = ""
                .Width = dgData.Width - 10
            End With


            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleNotesID})
            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)


        End If

    End Sub

    Private Sub Fill_DetailsgloEMRUsers()
        If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then
            Dim objUsers As New clsUsers
            Dim dtUsers As New DataTable
            Select Case Trim(trvCategory.SelectedNode.Text)
                Case "All"
                    dtUsers = objUsers.PopulateUsers(clsUsers.enmUsersType.All)
                    btnDelete.Visible = False
                Case "Active Users"
                    dtUsers = objUsers.PopulateUsers(clsUsers.enmUsersType.Active)
                    btnDelete.Visible = True
                    btnDelete.Text = "&Block"
                    btnDelete.ToolTipText = "Block"
                Case "Blocked Users"
                    dtUsers = objUsers.PopulateUsers(clsUsers.enmUsersType.NonActive)
                    btnDelete.Visible = True
                    btnDelete.Text = "&Unblock"
                    btnDelete.ToolTipText = "Unblock"
            End Select
            objUsers = Nothing
            dgData.DataSource = dtUsers
            dgData.CaptionText = "gloEMR Users"

            Dim grdTableStyle As New clsDataGridTableStyle(dtUsers.TableName)

            Dim grdColStyleUserID As New DataGridTextBoxColumn
            With grdColStyleUserID
                .HeaderText = "User ID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtUsers.Columns(0).ColumnName
                .NullText = ""
                .Width = 0
            End With

            Dim grdColStyleLoginUserName As New DataGridTextBoxColumn
            With grdColStyleLoginUserName
                .HeaderText = "Login User Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtUsers.Columns(1).ColumnName
                .NullText = ""
                .Width = 0.2 * dgData.Width
            End With

            Dim grdColStyleUserFullName As New DataGridTextBoxColumn
            With grdColStyleUserFullName
                .HeaderText = "Full Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtUsers.Columns(2).ColumnName
                .NullText = ""
                .Width = 0.2 * dgData.Width
            End With

            Dim grdColStyleUserPhoneNo As New DataGridTextBoxColumn
            With grdColStyleUserPhoneNo
                .HeaderText = "Phone No"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtUsers.Columns(3).ColumnName
                .NullText = ""
                .Width = 0.2 * dgData.Width
            End With

            Dim grdColStyleUserMobileNo As New DataGridTextBoxColumn
            With grdColStyleUserMobileNo
                .HeaderText = "Mobile No"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtUsers.Columns(4).ColumnName
                .NullText = ""
                .Width = 0.2 * dgData.Width
            End With

            Dim grdColStyleUserEmail As New DataGridTextBoxColumn
            With grdColStyleUserEmail
                .HeaderText = "Email"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtUsers.Columns(5).ColumnName
                .NullText = ""
                .Width = 0 ' 0.2 * dgData.Width - 10
            End With

            Dim grdColStyleAdministrator As New DataGridTextBoxColumn
            With grdColStyleAdministrator
                .HeaderText = "Is Admin"
                .Alignment = HorizontalAlignment.Center
                .MappingName = dtUsers.Columns(6).ColumnName
                .NullText = ""
                .Width = 0.2 * dgData.Width - 10
            End With

            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleUserID, grdColStyleLoginUserName, grdColStyleUserFullName, grdColStyleUserPhoneNo, grdColStyleUserMobileNo, grdColStyleUserEmail, grdColStyleAdministrator})

            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)

            'Sarika 21st April 2007
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "List of : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '------------------------------------

        End If

    End Sub

    Private Sub Fill_DetailsClientSettings()

        If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then

            Dim dsClientSettings As New DataSet
            Dim dtClientSettings As New DataTable("ClientSettings")
            Dim clmnPropertyName As New DataColumn("PropertyName")
            Dim clmnPropertyValue As New DataColumn("PropertyValue")
            dtClientSettings.Columns.Add(clmnPropertyName)
            dtClientSettings.Columns.Add(clmnPropertyValue)
            dsClientSettings.Tables.Add(dtClientSettings)

            dgData.DataSource = dsClientSettings.Tables(0)
            dgData.CaptionText = "Client Details"

            Dim grdTableStyle As New clsDataGridTableStyle(dtClientSettings.TableName)

            Dim grdColStylePropertyName As New DataGridTextBoxColumn
            With grdColStylePropertyName
                .HeaderText = "Settings"
                .Alignment = HorizontalAlignment.Left
                .MappingName = clmnPropertyName.ColumnName
                .NullText = ""
                .Width = 0.5 * dgData.Width
            End With

            Dim grdColStylePropertyValue As New DataGridTextBoxColumn
            With grdColStylePropertyValue
                .HeaderText = "Value"
                .Alignment = HorizontalAlignment.Left
                .MappingName = clmnPropertyValue.ColumnName
                .NullText = ""
                .Width = 0.5 * dgData.Width - 10
            End With

            grdTableStyle.GridColumnStyles.Clear()
            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePropertyName, grdColStylePropertyValue})

            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)

            Dim dtClients As New DataTable
            Dim objClientMachine As New clsClientMachines
            dtClients = objClientMachine.ScanClientMachine(CType(trvCategory.SelectedNode.Tag, Integer))
            objClientMachine = Nothing
            Dim drRow As DataRow

            If (Not IsNothing(dtClients) And dtClients.Rows.Count > 0) Then

                drRow = dtClientSettings.NewRow
                drRow(0) = "Voice Enabled"
                drRow(1) = dtClients.Rows(0).Item("VoiceEnabled")
                dtClientSettings.Rows.Add(drRow)

                drRow = dtClientSettings.NewRow
                drRow(0) = "Scan Enabled"
                drRow(1) = dtClients.Rows(0).Item("ScanEnabled")
                dtClientSettings.Rows.Add(drRow)

                dgData.Tag = dtClients.Rows(0).Item(0)

            End If
            '------------------------------------
            'Sarika 21st April 2007
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The details of Client : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '------------------------------------
            'dgData.DataSource = Nothing

        End If
    End Sub

    'Private Sub Fill_DetailsProviders()
    '    If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then
    '        Dim dtProvider As New DataTable
    '        Dim objProvider As New clsProvider
    '        dtProvider = objProvider.ScanProvider(Trim(trvCategory.SelectedNode.Text))
    '        objProvider = Nothing
    '        dgData.DataSource = dtProvider
    '        dgData.CaptionText = "Doctor Details"

    '        Dim grdTableStyle As New clsDataGridTableStyle(dtProvider.TableName)

    '        Dim grdColStyleProviderID As New DataGridTextBoxColumn
    '        With grdColStyleProviderID
    '            .HeaderText = "Provider ID"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(0).ColumnName
    '            .NullText = ""
    '            .Width = 0
    '        End With

    '        Dim grdColStyleProviderName As New DataGridTextBoxColumn
    '        With grdColStyleProviderName
    '            .HeaderText = "Provider Name"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(1).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColStyleGender As New DataGridTextBoxColumn
    '        With grdColStyleGender
    '            .HeaderText = "Gender"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(2).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColStyleDEA As New DataGridTextBoxColumn
    '        With grdColStyleDEA
    '            .HeaderText = "DEA"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(3).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColStyleProviderAddress As New DataGridTextBoxColumn
    '        With grdColStyleProviderAddress
    '            .HeaderText = "Address"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(4).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleProviderStreet As New DataGridTextBoxColumn
    '        With grdColStyleProviderStreet
    '            .HeaderText = "Street"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(5).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleProviderCity As New DataGridTextBoxColumn
    '        With grdColStyleProviderCity
    '            .HeaderText = "City"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(6).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleProviderState As New DataGridTextBoxColumn
    '        With grdColStyleProviderState
    '            .HeaderText = "State"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(7).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleProviderZIP As New DataGridTextBoxColumn
    '        With grdColStyleProviderZIP
    '            .HeaderText = "ZIP"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(8).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleProviderPhoneNo As New DataGridTextBoxColumn
    '        With grdColStyleProviderPhoneNo
    '            .HeaderText = "Phone No"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(9).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleProviderFAX As New DataGridTextBoxColumn
    '        With grdColStyleProviderFAX
    '            .HeaderText = "FAX"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(10).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColStyleProviderMobileNo As New DataGridTextBoxColumn
    '        With grdColStyleProviderMobileNo
    '            .HeaderText = "Mobile No"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(11).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColStyleProviderPagerNo As New DataGridTextBoxColumn
    '        With grdColStyleProviderPagerNo
    '            .HeaderText = "Mobile No"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(12).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColStyleProviderEmail As New DataGridTextBoxColumn
    '        With grdColStyleProviderEmail
    '            .HeaderText = "Email"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(13).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColStyleProviderURL As New DataGridTextBoxColumn
    '        With grdColStyleProviderURL
    '            .HeaderText = "URL"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtProvider.Columns(14).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleProviderID, grdColStyleProviderName, grdColStyleGender, grdColStyleDEA, grdColStyleProviderAddress, grdColStyleProviderStreet, grdColStyleProviderCity, grdColStyleProviderState, grdColStyleProviderZIP, grdColStyleProviderPhoneNo, grdColStyleProviderFAX, grdColStyleProviderMobileNo, grdColStyleProviderPagerNo, grdColStyleProviderEmail, grdColStyleProviderURL})
    '        dgData.TableStyles.Clear()
    '        dgData.TableStyles.Add(grdTableStyle)

    '    End If
    'End Sub

    Private Sub Fill_DetailsProviders()

        If IsNothing(trvCategory.SelectedNode) Then
            dgData.DataSource = Nothing
            Exit Sub
        End If

        If trvCategory.SelectedNode.Level <> 1 Then
            dgData.DataSource = Nothing
            Exit Sub
        End If

        If trvCategory.SelectedNode.Parent.Tag = "Active" Then
            btnDelete.Text = "&Block"
            btnDelete.ToolTipText = "Block"
            btnDelete.Enabled = True
        ElseIf trvCategory.SelectedNode.Parent.Tag = "Blocked" Then
            btnDelete.Text = "&Unblock"
            btnDelete.ToolTipText = "Unblock"
            btnDelete.Enabled = True
        ElseIf trvCategory.SelectedNode.Parent.Tag = "pending" Or trvCategory.SelectedNode.Parent.Tag = "review" Then
            btnDelete.Enabled = False
        End If


        Dim dtProvider As New DataTable
        Dim objProvider As New clsProvider
        dtProvider = objProvider.ScanProvider(Convert.ToInt64(trvCategory.SelectedNode.Tag))


        Dim dtClientSettings As New DataTable("Provider")
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtClientSettings.Columns.Add(clmnPropertyName)
        dtClientSettings.Columns.Add(clmnPropertyValue)


        dgData.DataSource = dtClientSettings
        dgData.CaptionText = "Clinic Details"

        Dim grdTableStyle As New clsDataGridTableStyle(dtClientSettings.TableName)

        Dim grdColStylePropertyName As New DataGridTextBoxColumn
        With grdColStylePropertyName
            .HeaderText = "Property Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = clmnPropertyName.ColumnName
            .NullText = ""
            .Width = 0.5 * dgData.Width
        End With

        Dim grdColStylePropertyValue As New DataGridTextBoxColumn
        With grdColStylePropertyValue
            .HeaderText = "Property Value"
            .Alignment = HorizontalAlignment.Left
            .MappingName = clmnPropertyValue.ColumnName
            .NullText = ""
            .Width = 0.5 * dgData.Width - 10
        End With

        grdTableStyle.GridColumnStyles.Clear()
        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePropertyName, grdColStylePropertyValue})

        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyle)
        ' Dim nCount As Int16
        If dtProvider.Rows.Count >= 1 Then
            Dim drRow As DataRow
            dgData.Tag = Convert.ToInt64(trvCategory.SelectedNode.Tag)

            For iCol As Integer = 0 To dtProvider.Columns.Count - 2 '' LAST COLUMN NOT CONSIDERED ''
                drRow = dtClientSettings.NewRow
                drRow("PropertyName") = dtProvider.Columns(iCol).ColumnName
                drRow("PropertyValue") = dtProvider.Rows(0)(iCol).ToString.Trim
                dtClientSettings.Rows.Add(drRow)
            Next

            '' COMMENTED BY SUDHIR 20090625 ''
            'drRow = dtClientSettings.NewRow
            'drRow(0) = "Doctor Name"
            'drRow(1) = dtProvider.Rows(0).Item(1)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "Address"
            'drRow(1) = dtProvider.Rows(0).Item(4)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "Street"
            'drRow(1) = dtProvider.Rows(0).Item(5)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "City"
            'drRow(1) = dtProvider.Rows(0).Item(6)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "State"
            'drRow(1) = dtProvider.Rows(0).Item(7)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "ZIP"
            'drRow(1) = dtProvider.Rows(0).Item(8)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "Phone No"
            'drRow(1) = dtProvider.Rows(0).Item(9)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "FAX"
            'drRow(1) = dtProvider.Rows(0).Item(10)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "Mobile No"
            'drRow(1) = dtProvider.Rows(0).Item(11)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "Pager No"
            'drRow(1) = dtProvider.Rows(0).Item(12)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "Email"
            'drRow(1) = dtProvider.Rows(0).Item(13)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "URL"
            'drRow(1) = dtProvider.Rows(0).Item(14)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "Gender"
            'drRow(1) = dtProvider.Rows(0).Item(2)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "DEA"
            'drRow(1) = dtProvider.Rows(0).Item(3)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "NPI"
            'drRow(1) = dtProvider.Rows(0).Item(16)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "UPIN"
            'drRow(1) = dtProvider.Rows(0).Item(17)
            'dtClientSettings.Rows.Add(drRow)

            'drRow = dtClientSettings.NewRow
            'drRow(0) = "State Medical License No"
            'drRow(1) = dtProvider.Rows(0).Item(18)
            'dtClientSettings.Rows.Add(drRow)
            '' END SUDHIR COMMENT ''

            drRow = dtClientSettings.NewRow
            drRow(0) = "Doctor Type"
            drRow(1) = objProvider.GetProviderType(dtProvider.Rows(0).Item(dtProvider.Columns.Count - 1))
            dtClientSettings.Rows.Add(drRow)

            'Sarika 21st April 2007s
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "Details of Doctor : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '------------------------------------
            objProvider = Nothing
        End If
    End Sub

    'Private Sub Fill_DetailsClinics()
    '    If Trim(trvCategory.SelectedNode.Text) <> "Clinic" Then
    '        Dim dtClinic As New DataTable
    '        Dim objClinic As New clsClinic
    '        dtClinic = objClinic.RetrieveClinicBasicInformation(Trim(trvCategory.SelectedNode.Text))
    '        objClinic = Nothing
    '        dgData.DataSource = dtClinic
    '        dgData.CaptionText = "Clinic"

    '        Dim grdTableStyle As New clsDataGridTableStyle(dtClinic.TableName)

    '        Dim grdColStyleClinicID As New DataGridTextBoxColumn
    '        With grdColStyleClinicID
    '            .HeaderText = "Clinic ID"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtClinic.Columns(0).ColumnName
    '            .NullText = ""
    '            .Width = 0
    '        End With

    '        Dim grdColStyleClinicName As New DataGridTextBoxColumn
    '        With grdColStyleClinicName
    '            .HeaderText = "Clinic Name"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtClinic.Columns(1).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColStyleClinicAddress As New DataGridTextBoxColumn
    '        With grdColStyleClinicAddress
    '            .HeaderText = "Address"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtClinic.Columns(2).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleClinicStreet As New DataGridTextBoxColumn
    '        With grdColStyleClinicStreet
    '            .HeaderText = "Street"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtClinic.Columns(3).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleClinicCity As New DataGridTextBoxColumn
    '        With grdColStyleClinicCity
    '            .HeaderText = "City"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtClinic.Columns(4).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleClinicState As New DataGridTextBoxColumn
    '        With grdColStyleClinicState
    '            .HeaderText = "State"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtClinic.Columns(5).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleClinicZIP As New DataGridTextBoxColumn
    '        With grdColStyleClinicZIP
    '            .HeaderText = "ZIP"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtClinic.Columns(6).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleClinicPhoneNo As New DataGridTextBoxColumn
    '        With grdColStyleClinicPhoneNo
    '            .HeaderText = "Phone No"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtClinic.Columns(7).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With
    '        Dim grdColStyleClinicMobileNo As New DataGridTextBoxColumn
    '        With grdColStyleClinicMobileNo
    '            .HeaderText = "Mobile No"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtClinic.Columns(8).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColStyleClinicFAX As New DataGridTextBoxColumn
    '        With grdColStyleClinicFAX
    '            .HeaderText = "FAX"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtClinic.Columns(9).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With

    '        Dim grdColStyleClinicEmail As New DataGridTextBoxColumn
    '        With grdColStyleClinicEmail
    '            .HeaderText = "Email"
    '            .Alignment = HorizontalAlignment.Left
    '            .MappingName = dtClinic.Columns(10).ColumnName
    '            .NullText = ""
    '            .Width = 100
    '        End With


    '        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleClinicID, grdColStyleClinicName, grdColStyleClinicAddress, grdColStyleClinicStreet, grdColStyleClinicCity, grdColStyleClinicState, grdColStyleClinicZIP, grdColStyleClinicPhoneNo, grdColStyleClinicMobileNo, grdColStyleClinicFAX, grdColStyleClinicEmail})
    '        dgData.TableStyles.Clear()
    '        dgData.TableStyles.Add(grdTableStyle)

    '    End If
    'End Sub

    Private Sub Fill_DetailsClinics()
        If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then
            Dim dtClinic As New DataTable
            Dim objClinic As New clsClinic
            dtClinic = objClinic.RetrieveClinicBasicInformation(Trim(trvCategory.SelectedNode.Text))
            objClinic = Nothing

            Dim dtClientSettings As New DataTable("Clinic")
            Dim clmnPropertyName As New DataColumn("PropertyName")
            Dim clmnPropertyValue As New DataColumn("PropertyValue")
            dtClientSettings.Columns.Add(clmnPropertyName)
            dtClientSettings.Columns.Add(clmnPropertyValue)


            dgData.DataSource = dtClientSettings
            dgData.CaptionText = "Clinic Details"

            Dim grdTableStyle As New clsDataGridTableStyle(dtClientSettings.TableName)

            Dim grdColStylePropertyName As New DataGridTextBoxColumn
            With grdColStylePropertyName
                .HeaderText = "Property Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = clmnPropertyName.ColumnName
                .NullText = ""
                .Width = 0.5 * dgData.Width
            End With

            Dim grdColStylePropertyValue As New DataGridTextBoxColumn
            With grdColStylePropertyValue
                .HeaderText = "Property Value"
                .Alignment = HorizontalAlignment.Left
                .MappingName = clmnPropertyValue.ColumnName
                .NullText = ""
                .Width = 0.5 * dgData.Width - 10
            End With

            grdTableStyle.GridColumnStyles.Clear()
            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePropertyName, grdColStylePropertyValue})

            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)
            'sarika 25th june 07
            '  Dim nCount As Int16
            '----
            Dim drRow As DataRow
            If dtClinic.Rows.Count >= 1 Then
                dgData.Tag = dtClinic.Rows(0).Item(0)
                drRow = dtClientSettings.NewRow
                drRow(0) = "Clinic Name"
                drRow(1) = dtClinic.Rows(0).Item(1)
                dtClientSettings.Rows.Add(drRow)

                '' SUDHIR 20090414 - CLINIC MST CHANGE FOR ADDRESS ''
                drRow = dtClientSettings.NewRow
                drRow(0) = "Address 1"
                drRow(1) = dtClinic.Rows(0).Item(2)
                dtClientSettings.Rows.Add(drRow)

                drRow = dtClientSettings.NewRow
                drRow(0) = "Address 2"
                drRow(1) = dtClinic.Rows(0).Item(3)
                dtClientSettings.Rows.Add(drRow)
                '' SUDHIR END ''

                'drRow = dtClientSettings.NewRow
                'drRow(0) = "Street"
                'drRow(1) = dtClinic.Rows(0).Item(4)
                'dtClientSettings.Rows.Add(drRow)

                drRow = dtClientSettings.NewRow
                drRow(0) = "City"
                drRow(1) = dtClinic.Rows(0).Item(5)
                dtClientSettings.Rows.Add(drRow)

                drRow = dtClientSettings.NewRow
                drRow(0) = "State"
                drRow(1) = dtClinic.Rows(0).Item(6)
                dtClientSettings.Rows.Add(drRow)

                drRow = dtClientSettings.NewRow
                drRow(0) = "ZIP"
                drRow(1) = dtClinic.Rows(0).Item(7)
                dtClientSettings.Rows.Add(drRow)

                drRow = dtClientSettings.NewRow
                drRow(0) = "Phone No"
                If (Convert.ToString(dtClinic.Rows(0).Item(8)) <> "(___) ___-____") Then

                    drRow(1) = dtClinic.Rows(0).Item(8)
                Else

                    drRow(1) = ""
                End If
                dtClientSettings.Rows.Add(drRow)

                drRow = dtClientSettings.NewRow
                drRow(0) = "Mobile No"
                If (Convert.ToString(dtClinic.Rows(0).Item(9)) <> "(___) ___-____") Then

                    drRow(1) = dtClinic.Rows(0).Item(9)
                Else

                    drRow(1) = ""
                End If
                dtClientSettings.Rows.Add(drRow)
                drRow = dtClientSettings.NewRow
                drRow(0) = "FAX"
                drRow(1) = dtClinic.Rows(0).Item(10)
                dtClientSettings.Rows.Add(drRow)

                drRow = dtClientSettings.NewRow
                drRow(0) = "Email"
                drRow(1) = dtClinic.Rows(0).Item(11)
                dtClientSettings.Rows.Add(drRow)

                '------------------------------------
                'Sarika 21st April 2007
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "Details of Clinic : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------------------------

            End If
        End If
    End Sub

    'sarika 11th sept 07
    Public Sub Fill_DetailsDoctorTypes()
        Dim dtProviderType As New DataTable

        Try
            If IsNothing(trvCategory.SelectedNode) = True Then
                Exit Sub
            End If
            If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then


                Dim objProviderType As New clsProviderType(gstrConnectionString)
                dtProviderType = objProviderType.ScanProviderType(Trim(trvCategory.SelectedNode.Text))
                objProviderType = Nothing

                Dim dtDoctorType As New DataTable("DoctorType")
                Dim clmnPropertyName As New DataColumn("PropertyName")
                Dim clmnPropertyValue As New DataColumn("PropertyValue")
                dtDoctorType.Columns.Add(clmnPropertyName)
                dtDoctorType.Columns.Add(clmnPropertyValue)


                dgData.DataSource = dtDoctorType
                dgData.CaptionText = "Doctor Type Details"

                Dim grdTableStyle As New clsDataGridTableStyle(dtDoctorType.TableName)

                Dim grdColStylePropertyName As New DataGridTextBoxColumn
                With grdColStylePropertyName
                    .HeaderText = "Property Name"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = clmnPropertyName.ColumnName
                    .NullText = ""
                    .Width = 0.5 * dgData.Width
                End With

                Dim grdColStylePropertyValue As New DataGridTextBoxColumn
                With grdColStylePropertyValue
                    .HeaderText = "Property Value"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = clmnPropertyValue.ColumnName
                    .NullText = ""
                    .Width = 0.5 * dgData.Width - 10
                End With

                grdTableStyle.GridColumnStyles.Clear()
                grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePropertyName, grdColStylePropertyValue})

                dgData.TableStyles.Clear()
                dgData.TableStyles.Add(grdTableStyle)

                Dim drRow As DataRow
                If dtProviderType.Rows.Count >= 1 Then
                    dgData.Tag = dtProviderType.Rows(0).Item(0)
                    drRow = dtDoctorType.NewRow
                    drRow(0) = "Doctor Type"
                    drRow(1) = dtProviderType.Rows(0).Item(1)
                    dtDoctorType.Rows.Add(drRow)

                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "Details of Clinic : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
                    objAudit = Nothing

                End If
            End If

        Catch ex As Exception
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Other, "Error occurred while viewing " + trvCategory.SelectedNode.Text + " data.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
        End Try
    End Sub
    '---------------------------------------------------------------------------------------------------------

    Private Sub Fill_DetailsSuggestions()
        If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then
            Dim objgloStreamSuggestions As New clsSuggestions
            Dim dtSuggestions As New DataTable
            Select Case Trim(trvCategory.SelectedNode.Text)
                Case "Today"
                    dtSuggestions = objgloStreamSuggestions.RetrieveSuggestions(clsBackupSchedule.enmScheduleCriteria.Today)
                Case "Yesterday"
                    dtSuggestions = objgloStreamSuggestions.RetrieveSuggestions(clsBackupSchedule.enmScheduleCriteria.Yesterday)
                Case "Last Week"
                    dtSuggestions = objgloStreamSuggestions.RetrieveSuggestions(clsBackupSchedule.enmScheduleCriteria.LastWeek)
                Case "Last Month"
                    dtSuggestions = objgloStreamSuggestions.RetrieveSuggestions(clsBackupSchedule.enmScheduleCriteria.LastMonth)
                Case "Last Quarter"
                    dtSuggestions = objgloStreamSuggestions.RetrieveSuggestions(clsBackupSchedule.enmScheduleCriteria.LastQuarter)
                Case "Last Year"
                    dtSuggestions = objgloStreamSuggestions.RetrieveSuggestions(clsBackupSchedule.enmScheduleCriteria.LastYear)
                Case "All"
                    dtSuggestions = objgloStreamSuggestions.RetrieveSuggestions(clsBackupSchedule.enmScheduleCriteria.All)
            End Select
            objgloStreamSuggestions = Nothing
            dgData.DataSource = dtSuggestions
            dgData.CaptionText = "Suggestions to gloStream"


            Dim grdTableStyle As New clsDataGridTableStyle(dtSuggestions.TableName)

            Dim grdColStyleSuggestionID As New DataGridTextBoxColumn
            With grdColStyleSuggestionID
                .HeaderText = "Suggestions ID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSuggestions.Columns(0).ColumnName
                .NullText = ""
                .Width = 0
            End With

            Dim grdColStyleSuggestionDate As New DataGridTextBoxColumn
            With grdColStyleSuggestionDate
                .HeaderText = "Suggestion Date"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSuggestions.Columns(1).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColStyleSubject As New DataGridTextBoxColumn
            With grdColStyleSubject
                .HeaderText = "Schedule Time"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSuggestions.Columns(2).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColStyleComments As New DataGridTextBoxColumn
            With grdColStyleComments
                .HeaderText = "Comments"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSuggestions.Columns(3).ColumnName
                .NullText = ""
                .Width = 300
            End With

            Dim grdColStyleClientName As New DataGridTextBoxColumn
            With grdColStyleClientName
                .HeaderText = "Client Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSuggestions.Columns(4).ColumnName
                .NullText = ""
                .Width = 100
            End With


            Dim grdColStyleEmail As New DataGridTextBoxColumn
            With grdColStyleEmail
                .HeaderText = "Email"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSuggestions.Columns(5).ColumnName
                .NullText = ""
                .Width = 50
            End With


            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleSuggestionID, grdColStyleSuggestionDate, grdColStyleSubject, grdColStyleComments, grdColStyleClientName, grdColStyleEmail})
            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)

            'Sarika 21st April 2007
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "Details of Suggestion : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '------------------------------------

        End If

    End Sub

    Private Sub Fill_DetailsLoginUsers()
        Dim dtLogin As New DataTable
        Dim objLogin As New clsLogin
        dtLogin = objLogin.RetrieveCurrentLoginUsers()
        objLogin = Nothing
        dgData.DataSource = dtLogin
        dgData.CaptionText = "Login Users"

        Dim grdTableStyle As New clsDataGridTableStyle(dtLogin.TableName)

        Dim grdColStyleUser As New DataGridTextBoxColumn
        With grdColStyleUser
            .HeaderText = "User Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(0).ColumnName
            .NullText = ""
            .Width = 0.2 * dgData.Width
        End With

        Dim grdColStyleLoginDateTime As New DataGridTextBoxColumn
        With grdColStyleLoginDateTime
            .HeaderText = "Login Date & Time"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(1).ColumnName
            .NullText = ""
            .Width = 0.6 * dgData.Width
        End With

        Dim grdColStyleMachine As New DataGridTextBoxColumn
        With grdColStyleMachine
            .HeaderText = "Machine Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(2).ColumnName
            .NullText = ""
            .Width = 0.2 * dgData.Width - 5
        End With

        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleUser, grdColStyleLoginDateTime, grdColStyleMachine})
        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyle)
    End Sub

    Private Sub Fill_DetailsBackups()
        If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then
            Dim objBackupSchedule As New clsBackupSchedule
            Dim dtBackupSchedule As New DataTable
            Select Case Trim(trvCategory.SelectedNode.Text)
                Case "Today"
                    dtBackupSchedule = objBackupSchedule.RetrieveBackupSchedule(clsBackupSchedule.enmScheduleCriteria.Today)
                Case "Yesterday"
                    dtBackupSchedule = objBackupSchedule.RetrieveBackupSchedule(clsBackupSchedule.enmScheduleCriteria.Yesterday)
                Case "Last Week"
                    dtBackupSchedule = objBackupSchedule.RetrieveBackupSchedule(clsBackupSchedule.enmScheduleCriteria.LastWeek)
                Case "Last Month"
                    dtBackupSchedule = objBackupSchedule.RetrieveBackupSchedule(clsBackupSchedule.enmScheduleCriteria.LastMonth)
                Case "Last Quarter"
                    dtBackupSchedule = objBackupSchedule.RetrieveBackupSchedule(clsBackupSchedule.enmScheduleCriteria.LastQuarter)
                Case "Last Year"
                    dtBackupSchedule = objBackupSchedule.RetrieveBackupSchedule(clsBackupSchedule.enmScheduleCriteria.LastYear)
                Case "All"
                    dtBackupSchedule = objBackupSchedule.RetrieveBackupSchedule(clsBackupSchedule.enmScheduleCriteria.All)
            End Select
            objBackupSchedule = Nothing
            dgData.DataSource = dtBackupSchedule
            dgData.CaptionText = "Backup Schedule"
            Dim grdTableStyle As New clsDataGridTableStyle(dtBackupSchedule.TableName)

            Dim grdColStyleScheduleID As New DataGridTextBoxColumn
            With grdColStyleScheduleID
                .HeaderText = "Schedule ID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtBackupSchedule.Columns(0).ColumnName
                .NullText = ""
                .Width = 0
            End With

            Dim grdColStyleScheduleJobID As New DataGridTextBoxColumn
            With grdColStyleScheduleJobID
                .HeaderText = "Job ID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtBackupSchedule.Columns(1).ColumnName
                .NullText = ""
                .Width = 0
            End With


            Dim grdColStyleScheduleName As New DataGridTextBoxColumn
            With grdColStyleScheduleName
                .HeaderText = "Schedule Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtBackupSchedule.Columns(2).ColumnName
                .NullText = ""
                .Width = 150
            End With


            Dim grdColStyleCategory As New DataGridTextBoxColumn
            With grdColStyleCategory
                .HeaderText = "Category"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtBackupSchedule.Columns(3).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColStyleEnabled As New DataGridTextBoxColumn
            With grdColStyleEnabled
                .HeaderText = "Enabled"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtBackupSchedule.Columns(4).ColumnName
                .NullText = ""
                .Width = 67
            End With

            Dim grdColStyleCreateDate As New DataGridTextBoxColumn
            With grdColStyleCreateDate
                .HeaderText = "Created Date"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtBackupSchedule.Columns(5).ColumnName
                .NullText = ""
                .Width = 135
            End With

            Dim grdColStyleLastRun As New DataGridTextBoxColumn
            With grdColStyleLastRun
                .HeaderText = "Last Run Date & Time"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtBackupSchedule.Columns(6).ColumnName
                .NullText = ""
                .Width = 155
            End With


            Dim grdColStyleNextRun As New DataGridTextBoxColumn
            With grdColStyleNextRun
                .HeaderText = "Next Run Date & Time"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtBackupSchedule.Columns(7).ColumnName
                .NullText = ""
                .Width = 159
            End With

            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleScheduleID, grdColStyleScheduleJobID, grdColStyleScheduleName, grdColStyleCategory, grdColStyleEnabled, grdColStyleCreateDate, grdColStyleLastRun, grdColStyleNextRun})
            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)

        End If

    End Sub

    Private Sub Fill_DetailsRestores()

    End Sub

    Private Sub Fill_DetailsSelfNotes()
        Try
            Dim objSelfNotes As New clsSelfNotes
            Dim dtSelfNotes As DataTable
            If optSelfNotesCategory.Checked = True Then
                dtSelfNotes = objSelfNotes.ScanSelfNotes(trvCategory.SelectedNode.Text, clsSelfNotes.enmSearchOn.SelfNotesCategory, dtFrom.Value.Date, dtTo.Value.Date)
            Else
                dtSelfNotes = objSelfNotes.ScanSelfNotes(trvCategory.SelectedNode.Text, clsSelfNotes.enmSearchOn.SelfNotesStatus, dtFrom.Value, dtTo.Value)
            End If
            objSelfNotes = Nothing
            dgData.DataSource = dtSelfNotes
            dgData.CaptionText = "Database Backup"

            With dgData
                .AllowNavigation = True
                .BorderStyle = BorderStyle.FixedSingle
                .CaptionText = "Self Notes"
            End With
            Dim grdTableStyle As New clsDataGridTableStyle(dtSelfNotes.TableName)

            Dim grdColStyleNotesID As New DataGridTextBoxColumn
            With grdColStyleNotesID
                .HeaderText = "Notes ID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSelfNotes.Columns(0).ColumnName
                .NullText = ""
                .Width = 0
            End With

            Dim grdColStyleNotesDate As New DataGridTextBoxColumn
            With grdColStyleNotesDate
                .HeaderText = "Notes Date"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSelfNotes.Columns(1).ColumnName
                .NullText = ""
                .Width = 100
            End With


            Dim grdColStyleNotesCategory As New DataGridTextBoxColumn
            With grdColStyleNotesCategory
                .HeaderText = "Notes Category"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSelfNotes.Columns(2).ColumnName
                .Width = 100
                .NullText = ""
            End With


            Dim grdColStyleNotesHead As New DataGridTextBoxColumn
            With grdColStyleNotesHead
                .HeaderText = "Notes Heading"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSelfNotes.Columns(3).ColumnName
                .Width = 250
                .NullText = ""
            End With

            Dim grdColStyleComments As New DataGridTextBoxColumn
            With grdColStyleComments
                .HeaderText = "Comments"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSelfNotes.Columns(4).ColumnName
                .Width = 250
                .NullText = ""
            End With

            Dim grdColStyleStatus As New DataGridTextBoxColumn
            With grdColStyleStatus
                .HeaderText = "Status"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtSelfNotes.Columns(5).ColumnName
                .NullText = ""
                .Width = 100
            End With


            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleNotesID, grdColStyleNotesDate, grdColStyleNotesCategory, grdColStyleNotesHead, grdColStyleComments, grdColStyleStatus})
            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)


            dtSelfNotes = Nothing
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#End Region


    Private Sub optSelfNotesStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSelfNotesStatus.Click
        Try
            Call Fill_CategorySelfNotes(enmSelfNotesCategoryStatus.Status)
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub optSelfNotesCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSelfNotesCategory.Click
        Try
            Call Fill_CategorySelfNotes(enmSelfNotesCategoryStatus.Category)
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            If enmUserOperation = enmOperation.Admin Then
                If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
                    Exit Sub
                End If
                Select Case Trim(trvAdminMenu.SelectedNode.Text)
                    Case "QEMR Groups"
                        Dim frmMaster As New frmUserGroup
                        frmMaster.pnlWindowsGroupsUsers.Enabled = True
                        frmMaster.blnModify = False
                        frmMaster.Fill_UserRights()
                        If frmMaster.ShowDialog() = DialogResult.OK Then
                            Call Fill_CategorygloEMRGroups()
                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                Call Fill_DetailsgloEMRGroups()
                            Else
                                dgData.DataSource = Nothing
                            End If
                        End If
                        Exit Sub
                    Case "User Groups"
                        Dim ofrmusergroups As New frmUserGroups
                        ofrmusergroups.ShowDialog()
                        If ofrmusergroups.ID > 0 Then
                            Fill_CategoryUserGroups()

                            For Each n As TreeNode In trvCategory.Nodes(0).Nodes
                                If Convert.ToInt64(n.Tag) = ofrmusergroups.ID Then
                                    trvCategory.SelectedNode = n
                                    Fill_DetailsUserGroups()
                                    Exit For
                                End If
                            Next
                        End If


                        Exit Sub
                    Case "Multiple Database"
                        Dim objSetting As New frmDBCredentials()
                        objSetting.ShowDialog()
                        btnRefresh_Click(sender, e)
                        If Not IsNothing(objSetting) Then   'Obj Disposed by Mitesh 
                            objSetting.Dispose()
                            objSetting = Nothing
                        End If
                    Case "User Management"
                        'Dim frmMaster As New frmUser
                        Dim frmMaster As New frmUserMgt
                        Dim _IsBusinessCenterFeatureOn As Boolean = False
                        frmMaster.Fill_UserRights()
                        frmMaster.blnModify = False
                        _IsBusinessCenterFeatureOn = IsBusinessCenterFeatureOn()
                        If (_IsBusinessCenterFeatureOn = True) Then
                            Call frmMaster.FillBusinessCenter()
                            frmMaster.cmbBusinessCenter.Visible = True
                            frmMaster.lblBusinessCenter.Visible = True
                        End If
                        Call frmMaster.Fill_gloEMRGroups()
                        Call frmMaster.Fill_MaritalStatus()
                        Call frmMaster.Fill_Gender()
                        Call frmMaster.Fill_Providers()
                        frmMaster.blnCheckRights = True
                        If frmMaster.ShowDialog() = DialogResult.OK Then
                            Call Fill_CategorygloEMRUsers()
                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                Call Fill_DetailsgloEMRUsers()
                            Else
                                dgData.DataSource = Nothing
                            End If
                        End If
                        Exit Sub
                    Case "Client Settings"
                        btnRefresh_Click(sender, e) ''added on 19 dec 2012 
                        Dim frmMaster As New frmClient
                        frmMaster.blnModify = False
                        If frmMaster.ShowDialog() = DialogResult.OK Then
                            Call Fill_CategoryClientMachines()
                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                Call Fill_DetailsClientSettings()
                            Else
                                dgData.DataSource = Nothing
                            End If
                        End If
                        Exit Sub
                    Case "Backup"
                        Dim frmMaster As New frmBackup

                        frmMaster.blnModify = False
                        frmMaster.Fill_OverwriteOptions()
                        frmMaster.dtStartDate.Value = Date.Now.Date
                        frmMaster.dtEndDate.Value = Date.Now.Date
                        Dim objBackup As New clsBackupSchedule
                        frmMaster.txtLocation.Text = objBackup.DefaultBackupPath
                        objBackup = Nothing
                        If frmMaster.ShowDialog() = DialogResult.OK Then
                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                Call Fill_DetailsBackups()
                            Else
                                dgData.DataSource = Nothing
                            End If
                        End If
                        Exit Sub
                    Case "Restore"
                        'Dim frmMaster As New frmRestore
                        'frmMaster.blnModify = False
                        'frmMaster.ShowDialog()
                        'Case "Self Notes"
                        '    Dim frmMaster As New frmSelfNotes
                        '    frmMaster.blnModify = False
                        '    frmMaster.Fill_NotesCategory()
                        '    frmMaster.Fill_NotesStatus()
                        '    If frmMaster.ShowDialog() = DialogResult.OK Then
                        '        Call Fill_CategorySelfNotes()
                        '        If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                        '            trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                        '            Call Fill_DetailsSelfNotes()
                        '        Else
                        '            dgData.DataSource = Nothing
                        '        End If
                        '    End If
                        '    Exit Sub


                        'sarika 11th sept 07
                    Case "Provider Type"
                        UpdateErrorLog("Adding Provider Type", mdlFunctions.enmErrorOccuredForm.MainForm, mdlFunctions.enmOperation.Add)
                        Dim frmgloEMRDoctorType As New frmDoctorType
                        frmgloEMRDoctorType.blnModify = False
                        If frmgloEMRDoctorType.ShowDialog() = DialogResult.OK Then
                            Call Fill_DoctorType()
                        End If
                        frmgloEMRDoctorType = Nothing
                        Exit Sub

                    Case "Clinic"
                        Dim objfrmClinic As New frmClinicNew
                        objfrmClinic.blnModify = False
                        If objfrmClinic.ShowDialog = Windows.Forms.DialogResult.OK Then
                            Call Fill_CategoryClinics()
                        End If
                        objfrmClinic = Nothing
                        If trvCategory.Nodes.Count > 0 Then
                            If trvCategory.Nodes.Item(0).Nodes.Count > 0 Then
                                trvCategory.SelectedNode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                            End If
                        End If
                        Exit Sub

                    Case "Provider"
                        UpdateErrorLog("Adding Provider", mdlFunctions.enmErrorOccuredForm.MainForm, mdlFunctions.enmOperation.Add)
                        Dim frmgloEMRDoctor As New frmDoctor(0)
                        frmgloEMRDoctor.blnModify = False
                        'frmgloEMRDoctor.Fill_DcotorTypes()
                        'If frmgloEMRDoctor.ShowDialog() = DialogResult.OK Then
                        frmgloEMRDoctor.ShowDialog()
                        Call Fill_CategoryProviders()
                        '  End If
                        frmgloEMRDoctor = Nothing
                        If trvCategory.Nodes.Count > 0 Then
                            If trvCategory.Nodes.Item(0).Nodes.Count > 0 Then
                                trvCategory.SelectedNode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                            End If
                        End If
                        Exit Sub

                    Case "Clearinghouse"


                        Dim conn As New SqlConnection()
                        Dim objCmd As SqlCommand
                        Dim _allowMultiple As Boolean = False
                        Dim _result As Object
                        Dim _strSQL As String = ""

                        Try
                            conn.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()

                            conn.Open()
                            _strSQL = "SELECT sSettingsValue FROM dbo.Settings WHERE sSettingsName = 'ISMULTIPLECLEARINGHOUSE' "
                            objCmd = New SqlCommand(_strSQL, conn)
                            _result = objCmd.ExecuteScalar()
                            If _result Is Nothing Then
                                _allowMultiple = False
                            ElseIf _result.ToString() = "0" Then
                                _allowMultiple = False
                            ElseIf _result.ToString() = "1" Then
                                _allowMultiple = True
                            ElseIf _result.ToString() = "" Then
                                _allowMultiple = False
                            End If
                        Catch ex As Exception

                        Finally
                            conn.Close()
                        End Try

                        UpdateErrorLog("Adding Clearinghouse", mdlFunctions.enmErrorOccuredForm.MainForm, mdlFunctions.enmOperation.Add)
                        Dim numRows As Integer = dgData.BindingContext(dgData.DataSource, dgData.DataMember).Count


                        If _allowMultiple = False And numRows >= 1 Then
                            MessageBox.Show("Admin parameter 'Allow multiple clearinghouses' is set to 'No'." & vbCrLf & "Only one clearinghouse can be entered.", "QPM Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            Dim frm As New frmSetupClearingHouse(gstrConnectionString, numRows)
                            frm.ShowDialog()
                            Call Fill_ClearingHouse()
                            frm = Nothing
                        End If
                        Exit Sub

                        'below by hemant
                    Case "CMS1500 02/12 Settings"
                        Dim frm As New frmPrinterSetup(DirectCast(dgData.DataSource, System.Data.DataTable))
                        frm.ShowDialog(Me)
                        frm = Nothing
                        Fill_Printers()

                        'DirectCast(dgData.DataSource, System.Data.DataTable)
                        Exit Sub
                        'above by hemant
                        'code added by pradeep on 17/06/2010 for prefix    
                    Case "UB04 Settings"
                        Dim frm As New frmPrinterSetup(DirectCast(dgData.DataSource, System.Data.DataTable), True)
                        frm.ShowDialog(Me)
                        frm = Nothing
                        Fill_Printers(True)
                        Exit Sub
                    Case "Site Prefix"
                        Dim objfrmPrefix As New gloSettings.frmPrefix(gstrConnectionString)
                        objfrmPrefix.ShowDialog()
                        Call Fill_Prefix()
                        Exit Sub

                        '----------------------------------------------------------------------------------------------
                End Select
                '----------------------------------
                'sarika 25th apr 2007
                'ElseIf enmUserOperation = enmOperation.Database Then
                '    If Trim(trvDatabase.SelectedNode.Text) = "Database" Then
                '        Exit Sub
                '    End If
                '    Select Case Trim(trvDatabase.SelectedNode.Text)
                '        Case "Database Update"
                '            Dim frmDBUpdate As New frmDBUpdate
                '            If frmDBUpdate.ShowDialog = DialogResult.OK Then
                '                Call Fill_CategoryDBVersions()
                '                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                '                    Call Fill_DetailsDBUpdations()
                '                End If
                '            End If
                '            Exit Sub
                '    End Select
                '----------------------------------
            ElseIf enmUserOperation = enmOperation.Tools Then
                If Trim(trvTools.SelectedNode.Text) = "Tools" Then
                    Exit Sub
                End If
                Select Case Trim(trvTools.SelectedNode.Text)
                    'sarika 26th june 07

                    'Case "Client Message"
                    '    Dim frmClient As New frmClientMessage
                    '    frmClient.blnModify = False
                    '    Call frmClient.Fill_Categories()
                    '    If frmClient.ShowDialog() = DialogResult.OK Then
                    '        Call Fill_CategoryClientMessage()
                    '        If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    '            trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    '            Call Fill_DetailsClientMessages()
                    '        Else
                    '            dgData.DataSource = Nothing
                    '        End If
                    '    End If
                    '    Exit Sub

                    'Case "Suggestions to gloStream"
                    '    Dim frmgloStreamSuggestions As New frmSuggestions
                    '    If frmgloStreamSuggestions.ShowDialog() = DialogResult.OK Then
                    '        Call Fill_CategorySuggestions()
                    '        If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                    '            trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    '            Call Fill_DetailsSuggestions()
                    '        Else
                    '            dgData.DataSource = Nothing
                    '        End If
                    '    End If
                    '    Exit Sub


                    'Case "Online Updates"
                    '    Dim frmUpdates As New frmOnlineUpdates
                    '    If frmUpdates.ShowDialog() = DialogResult.OK Then
                    '        Call Fill_CategoryOnlineUpdates()
                    '        If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                    '            trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    '            Call Fill_DetailsOnlineUpdates()
                    '        Else
                    '            dgData.DataSource = Nothing
                    '        End If
                    '    End If
                    '    Exit Sub

                    '--------------------
                End Select
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    ''''Private Sub btnReplication_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnReplication.Image = Image.FromFile(Application.StartupPath & "\Images\orangereplication.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try

    ''''End Sub

    ''''Private Sub btnReplication_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnReplication.Image = Image.FromFile(Application.StartupPath & "\Images\bluereplication.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try

    ''''End Sub


    ''''Private Sub btnDBArchive_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnDBArchive.Image = Image.FromFile(Application.StartupPath & "\Images\orangedatabasearchive.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try

    ''''End Sub

    ''''Private Sub btnDBArchive_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnDBArchive.Image = Image.FromFile(Application.StartupPath & "\Images\bluedatabasearchive.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try

    ''''End Sub


    ''''Private Sub btnOnlineUpdate_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnOnlineUpdate.Image = Image.FromFile(Application.StartupPath & "\Images\orangeonlineupdate.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try
    ''''End Sub

    ''''Private Sub btnOnlineUpdate_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnOnlineUpdate.Image = Image.FromFile(Application.StartupPath & "\Images\blueonlineupdate.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try
    ''''End Sub


    ''''Private Sub btngloStreamSuggestion_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btngloStreamSuggestion.Image = Image.FromFile(Application.StartupPath & "\Images\orangesuggestion.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try

    ''''End Sub

    ''''Private Sub btngloStreamSuggestion_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btngloStreamSuggestion.Image = Image.FromFile(Application.StartupPath & "\Images\bluesuggestion.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try

    ''''End Sub


    ''''Private Sub btnClientMessage_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnClientMessage.Image = Image.FromFile(Application.StartupPath & "\Images\orangeclientmessage.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try
    ''''End Sub

    ''''Private Sub btnClientMessage_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''''    Try
    ''''        btnClientMessage.Image = Image.FromFile(Application.StartupPath & "\Images\blueclientmessage.JPG")
    ''''    Catch objErr As Exception
    ''''        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''    End Try
    ''''End Sub

    Private Sub btnHelp_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.MouseLeave
        Try
            btnHelp.BackgroundImage = Image.FromFile(Application.StartupPath & "\Images\bluemainhelp.gif")
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnHelp_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.MouseEnter
        Try
            btnHelp.BackgroundImage = Image.FromFile(Application.StartupPath & "\Images\yellowmainhelp.gif")
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If Not IsNothing(trvAdminMenu.SelectedNode) Then
                If (Trim(trvAdminMenu.SelectedNode.Text) <> "Client Settings") Then
                    If dgData.CurrentRowIndex < 0 Then
                        Exit Sub
                    End If
                End If
            End If
            If enmUserOperation = enmOperation.Admin Then
                If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
                    Exit Sub
                End If
                Select Case Trim(trvAdminMenu.SelectedNode.Text)
                    Case "QEMR Groups"
                        If Trim(trvCategory.SelectedNode.Text) <> "User Groups" Then
                            If MessageBox.Show("Are you sure, you want to delete this user group?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Dim objUserGroup As New clsUserGroups
                                Dim _UserGroup As String = ""

                                _UserGroup = trvCategory.SelectedNode.Text
                                If objUserGroup.DeleteUserGroup(trvCategory.SelectedNode.Text) = True Then
                                    Call Fill_CategorygloEMRGroups()
                                    If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                        trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                        Call Fill_DetailsgloEMRGroups()
                                    Else
                                        dgData.DataSource = Nothing
                                    End If

                                    'sarika  21 feb
                                    Dim objAudit As New clsAudit
                                    objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrLoginName & " user has deleted the gloEMR Group : " & _UserGroup, gstrLoginName, gstrClientMachineName)
                                    objAudit = Nothing
                                    '-------------
                                Else
                                    MessageBox.Show("Unable to delete the user group", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                                objUserGroup = Nothing
                            End If
                        End If
                    Case "User Groups"
                        If trvCategory.SelectedNode IsNot Nothing AndAlso trvCategory.SelectedNode.Level <> 0 Then
                            If MessageBox.Show("Are you sure you want to delete this group?", "QPM Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Dim ID As Int64 = Convert.ToInt64(trvCategory.SelectedNode.Tag)
                                Dim objUsers As New clsUserGroups
                                objUsers.DeleteGroup(ID)
                                Fill_CategoryUserGroups()

                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                    Call Fill_DetailsUserGroups()
                                Else
                                    dgData.DataSource = Nothing
                                End If
                            End If

                        End If

                    Case "Clearinghouse"



                        If MessageBox.Show("Are you sure, you want to delete this Clearinghouse?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim ID As Int64 = Convert.ToInt64(dgData.Item(dgData.CurrentRowIndex, 0))
                            ''Default
                            Dim _Default As String = Convert.ToString(dgData.Item(dgData.CurrentRowIndex, 8))
                            Dim _IsUsedThenAlsoDelete As Boolean = False
                            Dim _IsUsedThenAlsoDelete1 As Boolean = False

                            If IsMultipleClearingHouse() = False Then
                                _IsUsedThenAlsoDelete1 = True

                            ElseIf _Default.ToUpper().Trim() = "DEFAULT" And _IsUsedThenAlsoDelete1 = False Then
                                MessageBox.Show("The default clearinghouse cannot be deleted. ", "QPM Admin", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub

                            End If

                            ''Used

                            If IsClearingHouseUsed(ID) = True Then
                                If MessageBox.Show("Insurance Plan(s) currently use this clearinghouse.Deleting this clearinghouse will remove the clearinghouse value in insurance plan setup for these plans." & Environment.NewLine & "Continue deleting? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                    ''DeleteFromPlan
                                    DeleteClearingHouseFromPlan(ID)
                                    _IsUsedThenAlsoDelete = True
                                Else
                                    _IsUsedThenAlsoDelete = False
                                End If
                            Else
                                _IsUsedThenAlsoDelete = True
                            End If


                            If _IsUsedThenAlsoDelete = True Then
                                Dim oClearingHouse As New ClearingHouse(gstrConnectionString)
                                oClearingHouse.Delete(ID)
                            End If
                            Fill_ClearingHouse()
                        End If
                        'below by hemant
                    Case "CMS1500 02/12 Settings"
                        If dgData.Item(dgData.CurrentRowIndex, 0) = "Default" Then
                            MessageBox.Show("Default printer cannot be deleted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf MessageBox.Show("Are you sure, you want to delete this Printer?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            DeletePrinter(dgData.Item(dgData.CurrentRowIndex, 0))

                            Fill_Printers()
                        Else

                        End If

                        'above by hemant
                    Case "UB04 Settings"
                        If dgData.Item(dgData.CurrentRowIndex, 0) = "Default" Then
                            MessageBox.Show("Default printer cannot be deleted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf MessageBox.Show("Are you sure, you want to delete this Printer?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            DeletePrinter_UB04(dgData.Item(dgData.CurrentRowIndex, 0))
                            Fill_Printers(True)
                        End If
                    Case "Multiple Database"
                        Dim objDatabase As New ClsMultipleDb()
                        If Not IsNothing(Trim(trvCategory.SelectedNode.Text)) Then

                            If Trim(trvCategory.SelectedNode.Text) <> "Database" Then
                                If (trvCategory.SelectedNode.Text = "Server Name") Then
                                    Exit Sub
                                End If
                                ''here we are checking the current row is selected or not 
                                If (dgData.IsSelected(dgData.CurrentCell.RowNumber) = True) Then
                                    If MessageBox.Show("Do you want to remove this database connection?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                        Dim _Selectednode As String
                                        ''Setting the select node value to the string
                                        _Selectednode = trvCategory.SelectedNode.Text

                                        'If objDatabase.DeleteServiceDatabaseName(dgData.Item(dgData.CurrentCell)) = True Then
                                        If objDatabase.DeleteServiceDatabaseName(dgData.Item(dgData.CurrentCell.RowNumber, 1)) = True Then
                                            Fill_MultipleDB()
                                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                                Fill_DetailsMultipleDB()
                                                btnRefresh_Click(sender, e)
                                            Else
                                                dgData.DataSource = Nothing
                                            End If
                                        Else
                                            MessageBox.Show("Unable to delete the server name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        End If
                                    End If
                                    objDatabase = Nothing
                                End If
                            End If
                        End If

                    Case "User Management"
                        Dim blnBlock As Boolean
                        Dim strMessage As String
                        If Trim(btnDelete.Text) = "&Block" Then
                            blnBlock = True
                            strMessage = "Are you sure, you want to Block this User?"
                        Else
                            blnBlock = False
                            strMessage = "Are you sure, you want to unblock this User?"
                        End If
                        If MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim objUser As New clsUsers

                            Dim _UserName As String = ""
                            If Not dgData.CurrentRowIndex.ToString Is Nothing Then
                                _UserName = dgData.Item(dgData.CurrentRowIndex, 1) & ""
                            End If

                            If objUser.BlockUnblockUser(dgData.Item(dgData.CurrentRowIndex, 0), blnBlock) = True Then
                                Call Fill_CategorygloEMRUsers()
                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                    Call Fill_DetailsgloEMRUsers()
                                Else
                                    dgData.DataSource = Nothing
                                End If

                                'sarika  21 feb
                                If blnBlock = True Then
                                    Dim objAudit As New clsAudit
                                    objAudit.CreateLog(clsAudit.enmActivityType.UserBlocked, _UserName & " user has been blocked by user  " & gstrLoginName, gstrLoginName, gstrClientMachineName)
                                    objAudit = Nothing
                                    '-------------
                                Else
                                    Dim objAudit As New clsAudit
                                    objAudit.CreateLog(clsAudit.enmActivityType.UserUnBlocked, _UserName & " user has been unblocked by user  " & gstrLoginName, gstrLoginName, gstrClientMachineName)
                                    objAudit = Nothing
                                    '-------------
                                End If
                            Else
                                If blnBlock = True Then
                                    MessageBox.Show("Unable to block the user", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Else
                                    MessageBox.Show("Unable to unblock the user", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            End If
                            objUser = Nothing
                        End If
                    Case "Client Settings"
                        If IsNothing(trvCategory.SelectedNode) = True Then
                            Exit Sub
                        End If
                        If IsNothing(trvCategory.SelectedNode.Tag) = True Then
                            Exit Sub
                        End If
                        If Trim(trvCategory.SelectedNode.Text) <> "Client Settings" Then
                            If MessageBox.Show("Are you sure, you want to delete this Client?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Dim objClient As New clsClientMachines
                                Dim _Client As String = ""

                                _Client = trvCategory.SelectedNode.Text

                                If objClient.DeleteClientSettingsByID(trvCategory.SelectedNode.Tag) = True Then
                                    Call Fill_CategoryClientMachines()
                                    If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                        trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                        Call Fill_DetailsClientSettings()
                                    Else
                                        dgData.DataSource = Nothing
                                    End If
                                    'sarika  21 feb
                                    Dim objAudit As New clsAudit
                                    objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrLoginName & " user has deleted Client " & _Client & " Settings.", gstrLoginName, gstrClientMachineName)
                                    objAudit = Nothing
                                    '-------------
                                Else
                                    MessageBox.Show("Unable to delete the Client", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                                objClient = Nothing
                            End If
                        End If
                    Case "Backup"
                        If MessageBox.Show("Are you sure, you want to delete this Backup Schedule?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim objBackup As New clsBackupSchedule
                            If objBackup.DeleteBackup(dgData.Item(dgData.CurrentRowIndex, 0), dgData.Item(dgData.CurrentRowIndex, 1)) = True Then
                                Call Fill_CategoryBackups()
                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0).Nodes(0)
                                    Call Fill_DetailsBackups()
                                Else
                                    dgData.DataSource = Nothing
                                End If
                            Else
                                MessageBox.Show("Unable to delete the Backup Schedule", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            objBackup = Nothing
                        End If
                    Case "Restore"

                    Case "Self Notes"
                        If dgData.CurrentRowIndex >= 0 Then
                            If MessageBox.Show("Are you sure, you want to delete self note?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Dim objSelfNotes As New clsSelfNotes
                                If objSelfNotes.DeleteSelfNotes(dgData.Item(dgData.CurrentRowIndex, 0)) = True Then
                                    Call Fill_CategorySelfNotes()
                                    If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                        trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                        Call Fill_DetailsSelfNotes()
                                    Else
                                        dgData.DataSource = Nothing
                                    End If

                                    'sarika  21 feb
                                    Dim objAudit As New clsAudit
                                    objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrLoginName & "User has deleted self notes.", gstrLoginName, gstrClientMachineName)
                                    objAudit = Nothing
                                    '-------------

                                Else
                                    MessageBox.Show("Unable to delete self note", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                                objSelfNotes = Nothing
                            End If
                        End If

                        'sarika 11th sept 07
                    Case "Provider Type"
                        '******By Sandip Deshmukh 26 Oct 07 4.05PM 
                        '******the code is modified to check wheather delete doctor type is not Senior/Junior
                        If Not (chkforSRJRDoctor(trvCategory.SelectedNode.Text)) Then

                            Dim blnCanDelete As Boolean = False
                            If dgData.CurrentRowIndex >= 0 Then
                                blnCanDelete = GetCanDeleteFlag(dgData.Tag)
                                If blnCanDelete = True Then
                                    If MessageBox.Show("Are you sure, you want to delete this Provider Type ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                        Dim objProviderType As New clsProviderType

                                        If objProviderType.DeleteProviderType(dgData.Item(dgData.CurrentRowIndex, 1)) = True Then
                                            Call Fill_DoctorType()
                                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                                Call Fill_DetailsDoctorTypes()
                                            Else
                                                dgData.DataSource = Nothing
                                            End If


                                            Dim objAudit As New clsAudit
                                            objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrLoginName & "User has deleted Provider Type.", gstrLoginName, gstrClientMachineName)
                                            objAudit = Nothing

                                        Else
                                            MessageBox.Show("Unable to delete Provider Type", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        End If
                                        objProviderType = Nothing

                                    End If
                                Else
                                    MessageBox.Show("Cannot delete Provider type because it is associated with one or more providers", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If

                            End If


                        Else
                            MessageBox.Show("You cannot delete Senior/Junior provider type.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        '****** 26 Oct 07 4.05PM 





                    Case "Provider"
                        If dgData.CurrentRowIndex >= 0 Then
                            Dim objProvider As New clsProvider

                            If btnDelete.Text = "&Block" Then
                                'If MessageBox.Show("Are you sure you want to block this Provider Information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                Dim _ProviderID As Int64 = Convert.ToInt64(trvCategory.SelectedNode.Tag) ''dgData.Tag


                                If objProvider.Block_Unblock_Provider(_ProviderID, True) = True Then
                                    Call Fill_CategoryProviders()
                                    If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                        trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                        Call Fill_DetailsProviders()
                                    Else
                                        dgData.DataSource = Nothing
                                    End If

                                    If MessageBox.Show("Do you want to associate patients with other provider?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                                        Dim ofrm As New frmAssociatePatients(_ProviderID)
                                        ofrm.ShowDialog()
                                        ofrm.Dispose()
                                        ofrm = Nothing
                                    End If

                                    Dim objAudit As New clsAudit
                                    objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrLoginName & "User has deleted Provider record.", gstrLoginName, gstrClientMachineName)
                                    objAudit = Nothing


                                Else
                                    'MessageBox.Show("Unable to delete Provider", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                                objProvider = Nothing
                                'End If
                            Else
                                If MessageBox.Show("Are you sure you want to unblock this provider?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                                    Dim dtprovider As DataTable = objProvider.GetProviderLicenseDetail(dgData.Tag, gstrConnectionString)
                                    Using objAUS As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                                        Dim sMessage As String = objAUS.ValidateLicenseKey("", "", "", "", gstrClinicExternalCode, 0, 0, dtprovider)
                                        If sMessage.Trim = "ok" Then
                                            If objProvider.Block_Unblock_Provider(dgData.Tag, False) = True Then
                                                Call Fill_CategoryProviders()
                                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                                    Call Fill_DetailsProviders()
                                                Else
                                                    dgData.DataSource = Nothing
                                                End If
                                            End If
                                        Else
                                            If sMessage.Trim.Contains("License Key already in use.") Then
                                                If MessageBox.Show("This license key is already in use. " & vbCrLf & "Provider info will be sent to TRIARQ Health and license key will be generated to activate the provider, " & vbCrLf & "are you sure ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                                    Exit Sub
                                                Else
                                                    Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                                                        If oLicense.UpdateProviderLicenseStatus(dgData.Tag, clsProvider.enmAUSStatus.PendingForLicense.GetHashCode, "") Then
                                                            oLicense.SendProviderForApproval(dgData.Tag)
                                                            Call Fill_CategoryProviders()
                                                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                                                Call Fill_DetailsProviders()
                                                            Else
                                                                dgData.DataSource = Nothing
                                                            End If
                                                        End If
                                                    End Using
                                                End If
                                            Else
                                                MessageBox.Show("Cannot unblock provider because it does not have valid license key. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            End If
                                        End If
                                    End Using
                                    If Not IsNothing(dtprovider) Then
                                        dtprovider.Dispose()
                                        dtprovider = Nothing
                                    End If
                                End If
                            End If

                        End If

                    Case "Clinic"
                        If dgData.CurrentRowIndex >= 0 Then
                            If MessageBox.Show("Are you sure, you want to delete this Clinic Information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Dim objClinic As New clsClinic

                                If objClinic.DeleteClinic(dgData.Tag) = True Then
                                    Call Fill_CategoryClinics()
                                    If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                        trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                        Call Fill_DetailsClinics()
                                    Else
                                        dgData.DataSource = Nothing
                                    End If


                                    Dim objAudit As New clsAudit
                                    objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrLoginName & " user has deleted Clinic record.", gstrLoginName, gstrClientMachineName)
                                    objAudit = Nothing


                                Else
                                    MessageBox.Show("Unable to delete Clinic record", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                                objClinic = Nothing
                            End If
                        End If

                        'code added by pradeep on 17/06/2010 for prefix
                    Case "Site Prefix"
                        'If dgData.CurrentRowIndex >= 0 Then
                        'If dgData.SelectionBackColor<>
                        Dim strMessage As String
                        If (dgData.IsSelected(dgData.CurrentCell.RowNumber) = True) Then
                            strMessage = "You are about to change the Patient Code Prefix.Other users are currently logged in to the system.Deleting the prefix while users are logged in is not recommended and could have unpredictable results.Do you wish to proceed?"
                            If MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Dim ID As Long = Convert.ToInt64(dgData.Item(dgData.CurrentRowIndex, 0))
                                Dim objfrmPrefix As New gloSettings.frmPrefix(gstrConnectionString)
                                objfrmPrefix.delete_Prefix(ID)
                                Call Fill_Prefix()
                            Else
                                Exit Sub
                            End If
                        End If
                        '--------------------
                End Select

            ElseIf enmUserOperation = enmOperation.Tools Then
                If Trim(trvTools.SelectedNode.Text) = "Tools" Then
                    Exit Sub
                End If
                Select Case Trim(trvTools.SelectedNode.Text)
                    Case "Client Message"
                        If MessageBox.Show("Are you sure, you want to delete this Client Message?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim objClient As New clsClientMessage
                            If objClient.DeleteMessage(dgData.Item(dgData.CurrentRowIndex, 0)) = True Then
                                Call Fill_CategoryClientMessage()
                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                    Call Fill_DetailsClientMessages()
                                Else
                                    dgData.DataSource = Nothing
                                End If
                                'sarika 21st feb
                                Dim objAudit As New clsAudit
                                objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrLoginName & " has deleted Client message.  ", gstrLoginName, gstrClientMachineName)
                                objAudit = Nothing
                                '-------------
                            Else
                                MessageBox.Show("Unable to delete the Client Message", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            objClient = Nothing

                        End If
                    Case "Suggestions to gloStream"
                        If MessageBox.Show("Are you sure, you want to delete this Suggestion?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim objClientSuggestions As New clsSuggestions
                            If objClientSuggestions.DeleteSuggestions(dgData.Item(dgData.CurrentRowIndex, 0)) = True Then
                                Call Fill_CategorySuggestions()
                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                    Call Fill_DetailsSuggestions()
                                Else
                                    dgData.DataSource = Nothing
                                End If

                                'sarika 21st feb
                                Dim objAudit As New clsAudit
                                objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrLoginName & " has deleted a Suggestion.", gstrLoginName, gstrClientMachineName)
                                objAudit = Nothing
                                '-----------------------------------------------------------``````````````````````````````````````````

                            Else
                                MessageBox.Show("Unable to delete the Suggestion", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            objClientSuggestions = Nothing

                        End If
                End Select
            ElseIf enmUserOperation = enmOperation.Audit Then
                If IsNothing(trvAudit.SelectedNode) = True Then Exit Sub
                Select Case Trim(trvAudit.SelectedNode.Text)
                    Case "Report"
                        Fill_DetailsAuditReportsLog()
                        '-------------


                    Case ""
                End Select
            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        Try
            'If dgData.CurrentRowIndex < 0 And Trim(trvAdminMenu.SelectedNode.Text) <> "User Groups" Then
            '    Exit Sub
            'End If



            If (Trim(trvAdminMenu.SelectedNode.Text) <> "Client Settings") Then
                If dgData.CurrentRowIndex < 0 And Trim(trvAdminMenu.SelectedNode.Text) <> "User Groups" Then
                    Exit Sub
                End If
            End If
            If enmUserOperation = enmOperation.Admin Then
                If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
                    Exit Sub
                End If
                Select Case Trim(trvAdminMenu.SelectedNode.Text)
                    Case "QEMR Groups"
                        'Check Category is selected or not
                        If IsNothing(trvCategory.SelectedNode) = True Then
                            MessageBox.Show("Please select the User Group", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                        If Trim(trvCategory.SelectedNode.Text) <> "User Groups" Then
                            Dim objGroup As New clsUserGroups
                            Dim frmGroup As New frmUserGroup
                            frmGroup.pnlWindowsGroupsUsers.Enabled = False
                            frmGroup.blnModify = True
                            frmGroup.Text = "Modify QEMR Groups"
                            frmGroup.Fill_UserRights()
                            objGroup.SearchGroup(Trim(trvCategory.SelectedNode.Text))
                            frmGroup.txtGroupName.Tag = objGroup.GroupID
                            frmGroup.txtGroupName.Text = Trim(trvCategory.SelectedNode.Text)
                            Dim nCount As Int16
                            For nCount = 1 To objGroup.GroupRights.Count
                                If IsDBNull(objGroup.GroupRights.Item(nCount)) = False Then
                                    CheckUncheckNode(objGroup.GroupRights.Item(nCount), frmGroup.trvRights, True)
                                End If
                            Next
                            objGroup = Nothing
                            If frmGroup.ShowDialog() = DialogResult.OK Then
                                Call Fill_CategorygloEMRGroups()
                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                    Call Fill_DetailsgloEMRGroups()
                                Else
                                    dgData.DataSource = Nothing
                                End If
                            End If
                            Exit Sub
                        End If

                    Case "User Groups"
                        If (trvCategory.Nodes(0).GetNodeCount(True) < 1) Then
                            Return
                        End If

                        If (trvCategory.SelectedNode.Level) <> 0 Then
                            Dim ofrmusergroups As New frmUserGroups(Convert.ToInt64(trvCategory.SelectedNode.Tag), Convert.ToString(trvCategory.SelectedNode.Text))
                            ofrmusergroups.Text = "Modify User Groups"
                            ofrmusergroups.ShowDialog()

                            If ofrmusergroups.ID > 0 Then
                                Fill_CategoryUserGroups()

                                For Each n As TreeNode In trvCategory.Nodes(0).Nodes
                                    If Convert.ToInt64(n.Tag) = ofrmusergroups.ID Then
                                        trvCategory.SelectedNode = n
                                        Fill_DetailsUserGroups()
                                        Exit For
                                    End If
                                Next
                            End If

                        End If
                        Exit Sub

                    Case "Multiple Database"
                        ''Dhruv 20092111---------------------------------------------
                        ''to modify the available database name from the gloService''
                        Dim objSetting As frmDBCredentials
                        If Not IsNothing(Trim(trvCategory.SelectedNode.Text)) Then
                            ''If the selected node is the header then dont do any of the operation
                            If (trvCategory.SelectedNode.Text = "Server Name") Then
                                Exit Sub
                            End If
                            ''Checking wheather the in datagrid there is any of the data is selected or not
                            If (dgData.IsSelected(dgData.CurrentCell.RowNumber) = True) Then
                                '''''''Added on 20100701 by sanjog to show UserName and password 
                                objSetting = New frmDBCredentials(dgData.Item(dgData.CurrentCell.RowNumber, 1), trvCategory.SelectedNode.Text, dgData.Item(dgData.CurrentCell.RowNumber, 0), Convert.ToBoolean(dgData.Item(dgData.CurrentCell.RowNumber, 2)), dgData.Item(dgData.CurrentCell.RowNumber, 3), dgData.Item(dgData.CurrentCell.RowNumber, 4))
                                objSetting.Text = "Modify Database Credentials"
                                '''''''Added on 20100701 by sanjog to show UserName and password 
                                objSetting.ShowDialog()
                                btnRefresh_Click(sender, e)
                            End If
                        End If
                    Case "User Management"
                        Dim objUser As New clsUsers
                        'Dim frmgloEMRuser As New frmUser
                        Dim frmgloEMRuser As New frmUserMgt
                        'Added by Rahul Patel on 08-09-2010 
                        'Added if condition 
                        frmgloEMRuser.Text = "Modify User Management"
                        If (dgData.IsSelected(dgData.CurrentRowIndex)) Then
                            frmgloEMRuser.Fill_UserRights()
                            frmgloEMRuser.blnModify = True
                            Dim _IsBusinessCenterFeatureOn As Boolean = False
                            _IsBusinessCenterFeatureOn = IsBusinessCenterFeatureOn()
                            If (_IsBusinessCenterFeatureOn = True) Then
                                Call frmgloEMRuser.FillBusinessCenter()
                                frmgloEMRuser.cmbBusinessCenter.Visible = True
                                frmgloEMRuser.lblBusinessCenter.Visible = True
                            End If
                            Call frmgloEMRuser.Fill_gloEMRGroups()
                            Call frmgloEMRuser.Fill_MaritalStatus()
                            Call frmgloEMRuser.Fill_Gender()
                            Call frmgloEMRuser.Fill_Providers()
                            ' use this pro if -1 then no row selected
                            frmgloEMRuser.txtUserName.Tag = dgData.Item(dgData.CurrentRowIndex, 0)
                            objUser.SearchUser(dgData.Item(dgData.CurrentRowIndex, 0))

                            frmgloEMRuser.txtUserName.Text = objUser.UserName
                            Dim objEncryption As New clsEncryption
                            frmgloEMRuser.txtPassword.Text = objEncryption.DecryptFromBase64String(objUser.Password, constEncryptDecryptKey)
                            frmgloEMRuser.txtNickName.Text = objEncryption.DecryptFromBase64String(objUser.NickName, constEncryptDecryptKey)

                            frmgloEMRuser.txtConfirmPassword.Text = frmgloEMRuser.txtPassword.Text
                            frmgloEMRuser.txtFirstName.Text = objUser.FirstName
                            frmgloEMRuser.txtMiddleName.Text = objUser.MiddleName
                            frmgloEMRuser.txtLastName.Text = objUser.LastName
                            frmgloEMRuser.txtSSN.Text = objUser.SSNNo
                            frmgloEMRuser.dtDOB.Value = objUser.DOB
                            frmgloEMRuser.cmbGender.SelectedIndex = frmgloEMRuser.cmbGender.FindStringExact(objUser.Gender)
                            frmgloEMRuser.cmbMaritalStatus.SelectedIndex = frmgloEMRuser.cmbMaritalStatus.FindStringExact(objUser.MaritalStatus)

                            'Commented by Rahul Patel on 8-9-2010 due removal of field street
                            'frmgloEMRuser.txtStreet.Text = objUser.Street

                            'frmgloEMRuser.txtAddress.Text = objUser.Address
                            'frmgloEMRuser.txtAddress2.Text = objUser.Address2
                            'frmgloEMRuser.txtCity.Text = objUser.City
                            'frmgloEMRuser.txtState.Text = objUser.State
                            'frmgloEMRuser.txtZip.Text = objUser.ZIP

                            ''Added the New Zip - Code Control
                            frmgloEMRuser.oAddressContol.isFormLoading = True
                            frmgloEMRuser.oAddressContol.txtAddress1.Text = objUser.Address     ''Add1
                            frmgloEMRuser.oAddressContol.txtAddress2.Text = objUser.Address2    ''Add2
                            frmgloEMRuser.oAddressContol.txtCity.Text = objUser.City            ''City
                            frmgloEMRuser.oAddressContol.txtZip.Text = objUser.ZIP              ''Zip

                            frmgloEMRuser.oAddressContol.txtCounty.Text = objUser.County    ''County
                            frmgloEMRuser.oAddressContol.cmbCountry.Text = objUser.Country       ''Country
                            frmgloEMRuser.oAddressContol.cmbState.Text = objUser.State          ''State.
                            frmgloEMRuser.oAddressContol.isFormLoading = False

                            ''end Zip - Code
                            frmgloEMRuser.txtPhoneNo.Text = objUser.PhoneNo
                            frmgloEMRuser.txtMobileNo.Text = objUser.MobileNo
                            frmgloEMRuser.txtFax.Text = objUser.FAX
                            frmgloEMRuser.txtEmailAddress.Text = objUser.Email
                            frmgloEMRuser.chkgloEMRAdmin.Checked = objUser.gloEMRAdministrator
                            frmgloEMRuser.chkAuditTrails.Checked = objUser.IsAuditTrail
                            frmgloEMRuser.chkAccessDenied.Checked = objUser.AccessDenied
                            frmgloEMRuser.txtWindowsLoginName.Text = objUser.WindowLoaginName
                            frmgloEMRuser.txtOCPLoginName.Text = If(objUser.OCPLoginName = "", "", objUser.OCPLoginName)
                            frmgloEMRuser.txtOCPLoginPassword.Text = If(objUser.OCPLoginPassword = "", "", objEncryption.DecryptFromBase64String(objUser.OCPLoginPassword, constEncryptDecryptKey))
                            frmgloEMRuser.txtOCPConfirmPassword.Text = If(objUser.OCPLoginPassword = "", "", objEncryption.DecryptFromBase64String(objUser.OCPLoginPassword, constEncryptDecryptKey))
                            frmgloEMRuser.chkAllowPortalAccess.Checked = objUser.IsAllowPortalAccess
                            frmgloEMRuser.chkSameAsUserDetails.Checked = objUser.IsSameAsUserDetails
                            If objUser.IsSameAsUserDetails Then
                                frmgloEMRuser.rb_UseCurrentCredential.Checked = True
                            Else
                                frmgloEMRuser.rb_NewCredential.Checked = True
                            End If

                            'added by mahendra for Emergency Access 
                            frmgloEMRuser.ChkEmergencyAccess.Checked = objUser.EAPChart
                            If (objUser.EAPChart = True) Then
                                frmgloEMRuser.txtEAPassword.Text = objEncryption.DecryptFromBase64String(objUser.EAPassword, constEncryptDecryptKey)
                                frmgloEMRuser.txtEAConfirmPassword.Text = frmgloEMRuser.txtEAPassword.Text


                                With frmgloEMRuser.dtpValidupto
                                    .Format = DateTimePickerFormat.Custom
                                    .CustomFormat = DTFORMAT
                                End With
                                frmgloEMRuser.dtpValidupto.Value = objUser.ValidDt.ToString()

                            End If
                            'End
                            'frmgloEMRuser.picSignature.Image = objUser.Signature
                            If IsNothing(objUser.Signature) = False Then
                                frmgloEMRuser.picSignature.Image = CType(objUser.Signature, Image)
                                frmgloEMRuser.picSignature.SizeMode = PictureBoxSizeMode.CenterImage '// Strech
                            End If
                            frmgloEMRuser.chkExchnageUser.Checked = objUser.IsExchangeUser

                            frmgloEMRuser.txtExchangeLogin.Text = objUser.ExchangeLogin
                            If (objUser.IsExchangeUser = True) Then
                                frmgloEMRuser.txtExchangePwd.Text = objEncryption.DecryptFromBase64String(objUser.ExchangePassward, constEncryptDecryptKey)
                                frmgloEMRuser.txtExchangePwdConfirm.Text = frmgloEMRuser.txtExchangePwd.Text
                            End If
                            objEncryption = Nothing
                            ' txtExchangeLogin()
                            'sarika 20090515
                            'If objUser.BlockStatus = True Then
                            '    frmgloEMRuser.nBlockStatus = 0
                            '    'sarika 20090515

                            'Else
                            '    frmgloEMRuser.nBlockStatus = 1
                            'End If

                            If Not IsNothing(objUser.BlockStatus) = True Then
                                frmgloEMRuser.nBlockStatus = objUser.BlockStatus
                            End If
                            '-----

                            'If objUser.ProviderID = 0 Then
                            '    frmgloEMRuser.cmbProvider.SelectedIndex = 0
                            'Else
                            '    'Retrieve Provider Name
                            '    Dim strProviderName As String
                            '    Dim objProvider As New clsProvider
                            '    strProviderName = objProvider.RetrieveProviderName(objUser.ProviderID)
                            '    objProvider = Nothing
                            '    If Trim(strProviderName) <> "" Then
                            '        frmgloEMRuser.cmbProvider.Text = strProviderName
                            '    Else
                            '        frmgloEMRuser.cmbProvider.SelectedIndex = 0
                            '    End If
                            'End If

                            'GLO2011-0015056 : gloPM Admin User Settings Inconsistent
                            'show the provider of selected user in provider combo box
                            frmgloEMRuser.cmbProvider.SelectedValue = objUser.ProviderID
                            frmgloEMRuser.cmbBusinessCenter.SelectedValue = objUser.BusinessCenterID
                            Dim clGroups As New Collection
                            clGroups = objUser.UserGroups
                            Dim nCount As Integer
                            Dim nCount1 As Integer
                            For nCount = 1 To clGroups.Count
                                For nCount1 = 0 To frmgloEMRuser.lstGroups.Items.Count - 1
                                    If Trim(clGroups.Item(nCount)) = Trim(frmgloEMRuser.lstGroups.Items(nCount1)) Then
                                        frmgloEMRuser.lstGroups.SetItemChecked(nCount1, True)
                                    End If
                                Next
                            Next
                            Dim clRights As New Collection
                            clRights = objUser.UserRights
                            Dim nTotalNodes As Int16
                            nTotalNodes = frmgloEMRuser.trvUserRights.GetNodeCount(False) - 1
                            For nCount = 1 To clRights.Count
                                For nCount1 = 0 To nTotalNodes
                                    SearchNode(frmgloEMRuser.trvUserRights, clRights.Item(nCount))
                                    If IsNothing(trvSearchNode) = False Then
                                        trvSearchNode.Checked = True
                                        frmgloEMRuser.trvUserRights.SelectedNode = trvSearchNode
                                        'frmgloEMRuser.trvUserRights.SelectedNode.Checked = True
                                    End If
                                Next
                            Next
                            objUser = Nothing
                            frmgloEMRuser.trvUserRights.ExpandAll()
                            frmgloEMRuser.blnCheckRights = True
                            If frmgloEMRuser.ShowDialog() = DialogResult.OK Then
                                Call Fill_CategorygloEMRUsers()
                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                    Call Fill_DetailsgloEMRUsers()
                                Else
                                    dgData.DataSource = Nothing
                                End If
                            End If
                            frmgloEMRuser = Nothing
                            Exit Sub
                        Else
                            MessageBox.Show("Please select the user.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Case "Clinic"
                        'Dim frmgloEMRClinic As New frmClinic
                        Dim frmgloEMRClinic As New frmClinicNew
                        Dim objClinic As New clsClinic
                        frmgloEMRClinic.blnModify = True
                        frmgloEMRClinic.txtName.Tag = dgData.Tag
                        objClinic.SearchClinic(dgData.Tag)
                        frmgloEMRClinic.txtName.Text = objClinic.ClinicName
                        'frmgloEMRClinic.txtAddress1.Text = objClinic.ClinicAddress1
                        'frmgloEMRClinic.txtAddress2.Text = objClinic.ClinicAddress2
                        'frmgloEMRClinic.txtCity.Text = objClinic.City
                        'frmgloEMRClinic.txtState.Text = objClinic.State
                        'frmgloEMRClinic.txtZip.Text = objClinic.ZIP 
                        frmgloEMRClinic.txtLabel.Text = objClinic.ClinicLabel
                        frmgloEMRClinic.txtClinicMaillingContact.Text = objClinic.ContactName
                        frmgloEMRClinic.oAddressContol.isFormLoading = True
                        frmgloEMRClinic.oAddressContol.txtAddress1.Text = objClinic.ClinicAddress1
                        frmgloEMRClinic.oAddressContol.txtAddress2.Text = objClinic.ClinicAddress2
                        frmgloEMRClinic.oAddressContol.txtCity.Text = objClinic.City
                        frmgloEMRClinic.oAddressContol.txtZip.Text = objClinic.ZIP
                        frmgloEMRClinic.oAddressContol.txtAreaCode.Text = objClinic.AreaCode

                        frmgloEMRClinic.oAddressContol.txtCounty.Text = objClinic.County
                        frmgloEMRClinic.oAddressContol.cmbCountry.Text = objClinic.Country
                        frmgloEMRClinic.oAddressContol.cmbState.Text = objClinic.State
                        frmgloEMRClinic.oAddressContol.isFormLoading = False

                        frmgloEMRClinic.txtStreet.Text = objClinic.Street
                        frmgloEMRClinic.txtPhoneNo.Text = objClinic.PhoneNo
                        frmgloEMRClinic.txtMobileNo.Text = objClinic.MobileNo
                        frmgloEMRClinic.txtFax.Text = objClinic.FAX
                        frmgloEMRClinic.txtEmailAddress.Text = objClinic.Email
                        frmgloEMRClinic.txtURL.Text = objClinic.URL
                        frmgloEMRClinic.txtTAXID.Text = objClinic.TAXID
                        'sarika siteid 20090708
                        frmgloEMRClinic.txtSiteID.Text = objClinic.SiteID
                        ''Sandip darade  200091113
                        frmgloEMRClinic.txtClinicNPI.Text = objClinic.ClinicNPI
                        '---
                        frmgloEMRClinic.txtTaxonomy.Text = objClinic.TaxonomyCode

                        If IsNothing(objClinic.ClinicLogo) = False Then
                            frmgloEMRClinic.picLogo.Image = CType(objClinic.ClinicLogo, Image)
                        End If
                        frmgloEMRClinic.picLogo.SizeMode = PictureBoxSizeMode.CenterImage
                        frmgloEMRClinic.txtContactPersonName.Text = objClinic.ContactPersonName
                        frmgloEMRClinic.txtContactPersonAddress1.Text = objClinic.ContactPersonAddress1
                        frmgloEMRClinic.txtContactPersonAddress2.Text = objClinic.ContactPersonAddress2
                        frmgloEMRClinic.txtContactPersonPhoneNo.Text = objClinic.ContactPersonPhone
                        frmgloEMRClinic.txtContactPersonMobileNo.Text = objClinic.ContactPersonMobile
                        frmgloEMRClinic.txtContactPersonFax.Text = objClinic.ContactPersonFAX
                        frmgloEMRClinic.txtContactPersonEmail.Text = objClinic.ContactPersonEmail


                        'Patient physical location address
                        frmgloEMRClinic.txtPLContactName.Text = objClinic.ClinicPLContactName
                        frmgloEMRClinic.oAddressContolPL.isFormLoading = True
                        frmgloEMRClinic.oAddressContolPL.txtAddress1.Text = objClinic.ClinicPLAddressline1
                        frmgloEMRClinic.oAddressContolPL.txtAddress2.Text = objClinic.ClinicPLAddressline2
                        frmgloEMRClinic.oAddressContolPL.txtCity.Text = objClinic.ClinicPLCity

                        frmgloEMRClinic.oAddressContolPL.txtZip.Text = objClinic.ClinicPLZIP
                        frmgloEMRClinic.oAddressContolPL.txtAreaCode.Text = objClinic.ClinicPLAreaCode
                        frmgloEMRClinic.oAddressContolPL.cmbCountry.Text = objClinic.ClinicPLCountry
                        frmgloEMRClinic.oAddressContolPL.txtCounty.Text = objClinic.ClinicPLCounty
                        frmgloEMRClinic.oAddressContolPL.cmbState.Text = objClinic.ClinicPLState
                        frmgloEMRClinic.oAddressContolPL.isFormLoading = False
                        frmgloEMRClinic.mskPLPager.Text = objClinic.ClinicPLPagerNo
                        frmgloEMRClinic.maskedPLPhno.Text = objClinic.ClinicPLPhoneNo
                        frmgloEMRClinic.mskPLFax.Text = objClinic.ClinicPLFAX
                        frmgloEMRClinic.txtPLEMail.Text = objClinic.ClinicPLEmail
                        frmgloEMRClinic.txtPLUrl.Text = objClinic.ClinicPLURL


                        If frmgloEMRClinic.ShowDialog() = DialogResult.OK Then
                            frmgloEMRClinic = Nothing
                            Call Fill_CategoryClinics()
                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                Call Fill_DetailsClinics()
                            Else
                                dgData.DataSource = Nothing
                            End If
                        Else
                            frmgloEMRClinic = Nothing
                        End If

                        Exit Sub

                    Case "Client Settings"
                        If IsNothing(trvCategory.SelectedNode) = True Then
                            Exit Sub
                        End If
                        If IsNothing(trvCategory.SelectedNode.Tag) = True Then
                            Exit Sub
                        End If
                        Dim frmClientSettings As New frmClient
                        frmClientSettings.Text = "Modify Client Settings"
                        Dim objClient As New clsClientMachines
                        objClient.SearchClient(CType(trvCategory.SelectedNode.Tag, Integer))
                        If (objClient.ClientMachineID = 0) Then
                            MessageBox.Show(Me, "Selected machine does not exists, Please add this again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgData.DataSource = Nothing
                            Dim evt As EventArgs
                            btnRefresh_Click(btnRefresh, evt)

                            Exit Sub
                        End If
                        frmClientSettings.blnModify = True
                        frmClientSettings.cmbMachineName.Tag = objClient.ClientMachineID
                        'frmClientSettings.cmbMachineName.Text = Trim(trvCategory.SelectedNode.Text)
                        frmClientSettings.txtMachineName.Text = Trim(trvCategory.SelectedNode.Text)

                        If frmClientSettings.ShowDialog() = DialogResult.OK Then
                            Call Fill_CategoryClientMachines()
                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                Call Fill_DetailsClientSettings()
                            Else
                                dgData.DataSource = Nothing
                            End If
                        End If
                        Exit Sub
                    Case "Backup"
                        Dim frmDBBackup As New frmBackup
                        frmDBBackup.blnModify = True
                        frmDBBackup.Fill_OverwriteOptions()
                        frmDBBackup.strJobID = dgData.Item(dgData.CurrentRowIndex, 1)
                        frmDBBackup.txtScheduleName.Tag = dgData.Item(dgData.CurrentRowIndex, 0)
                        frmDBBackup.Fill_BackupDetails(dgData.Item(dgData.CurrentRowIndex, 1))
                        If frmDBBackup.ShowDialog() = DialogResult.OK Then
                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                Call Fill_DetailsBackups()
                            Else
                                dgData.DataSource = Nothing
                            End If
                        End If
                        Exit Sub
                    Case "Restore"

                        'Case "Self Notes"
                        '    If dgData.CurrentRowIndex >= 0 Then
                        '        Dim objSelfNotes As New clsSelfNotes
                        '        Dim frmAdminSelfNotes As New frmSelfNotes
                        '        frmAdminSelfNotes.Fill_NotesCategory()
                        '        frmAdminSelfNotes.Fill_NotesStatus()
                        '        objSelfNotes.SearchSelfNotes(dgData.Item(dgData.CurrentRowIndex, 0))
                        '        frmAdminSelfNotes.dtNotesDate.Tag = dgData.Item(dgData.CurrentRowIndex, 0)
                        '        frmAdminSelfNotes.blnModify = True
                        '        frmAdminSelfNotes.dtNotesDate.Value = objSelfNotes.SelfNotesDate
                        '        frmAdminSelfNotes.txtNoteHeading.Text = objSelfNotes.NotesHead
                        '        frmAdminSelfNotes.txtComments.Text = objSelfNotes.Comments
                        '        frmAdminSelfNotes.cmbCategory.Text = objSelfNotes.SelfNotesCategory
                        '        frmAdminSelfNotes.cmbStatus.Text = objSelfNotes.Status
                        '        objSelfNotes = Nothing
                        '        If frmAdminSelfNotes.ShowDialog() = DialogResult.OK Then
                        '            Call Fill_CategorySelfNotes()
                        '            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                        '                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                        '                Call Fill_DetailsSelfNotes()
                        '            Else
                        '                dgData.DataSource = Nothing
                        '            End If
                        '        End If
                        '        Exit Sub
                        '    End If

                    Case "Provider Type"
                        'UpdateErrorLog("Adding Doctor Type", mdlFunctions.enmErrorOccuredForm.MainForm, mdlFunctions.enmOperation.Add)
                        '******By Sandip Deshmukh 26 Oct 07 4.05PM 
                        '******the code is modified to check wheather modify doctor type is not Senior/Junior
                        If Not (chkforSRJRDoctor(trvCategory.SelectedNode.Text)) Then
                            Dim frmgloEMRDoctorType As New frmDoctorType()
                            UpdateErrorLog("Modifying Provider Type", mdlFunctions.enmErrorOccuredForm.MainForm, mdlFunctions.enmOperation.Update)
                            'Dim frmgloEMRDoctorType As New frmDoctorType
                            Dim objDoctorType As New clsProviderType(gstrConnectionString)
                            frmgloEMRDoctorType.blnModify = True
                            Dim myNode As New TreeNode
                            objDoctorType.SearchProviderType(trvCategory.SelectedNode.Text)
                            myNode = trvCategory.SelectedNode
                            frmgloEMRDoctorType.txtDoctorType.Tag = objDoctorType.ProviderTypeID
                            frmgloEMRDoctorType.txtDoctorType.Text = objDoctorType.ProviderType

                            If frmgloEMRDoctorType.ShowDialog() = DialogResult.OK Then
                                trvCategory.SelectedNode = myNode
                                Call Fill_DoctorType()
                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                    Call Fill_DetailsDoctorTypes()
                                Else
                                    dgData.DataSource = Nothing
                                End If

                            End If
                            frmgloEMRDoctorType = Nothing
                            objDoctorType = Nothing
                        Else
                            MessageBox.Show("You cannot modify Senior/Junior Doctor type.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        '******26 Oct 07 4.05PM 
                    Case "Clearinghouse"
                        Dim ID As Int64 = 0
                        ID = Convert.ToInt64(dgData.Item(dgData.CurrentRowIndex, 0))
                        Dim numRows As Integer = dgData.BindingContext(dgData.DataSource, dgData.DataMember).Count
                        Dim frm As New frmSetupClearingHouse(gstrConnectionString, ID, numRows)
                        frm.Text = "Modify Clearinghouse"
                        frm.ShowDialog()
                        frm.Dispose()
                        Fill_ClearingHouse()

                        ''Hemant

                    Case "CMS1500 02/12 Settings"
                        Dim frm As New frmCMSPrinterSettingsNew(dgData.Item(dgData.CurrentRowIndex, 0))
                        frm.ShowDialog()
                        frm = Nothing
                        'Hemant
                        ''code added by pradeep on17/06/2010 for prefix
                    Case "UB04 Settings"
                        'lblMainTop.Text = "UB04 Settings"
                        'dgData.Visible = False
                        'SplitterMainCategory.Visible = False
                        'trvCategory.Visible = False
                        'pnlMainMainTop.Visible = False
                        'pnl_tlsp_Top.Visible = False
                        Dim oFrm As New frmUB04PrinterSettings(dgData.Item(dgData.CurrentRowIndex, 0))
                        oFrm.ShowDialog()
                        oFrm.Dispose()
                        oFrm = Nothing
                    Case "Site Prefix"
                        Dim strMessage As String
                        If (dgData.IsSelected(dgData.CurrentCell.RowNumber) = True) Then
                            strMessage = "You are about to change the Patient Code Prefix.Other users are currently logged in to the system.Changing the prefix while users are logged in is not recommended and could have unpredictable results.Do you wish to proceed?"
                            If MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Dim ID As Long = 0
                                Dim sServer As String = ""
                                Dim sDatabase As String = ""
                                Dim sPrefix As String = ""
                                ID = Convert.ToInt64(dgData.Item(dgData.CurrentRowIndex, 0))
                                sServer = dgData.Item(dgData.CurrentRowIndex, 1)
                                sDatabase = dgData.Item(dgData.CurrentRowIndex, 2)
                                sPrefix = dgData.Item(dgData.CurrentRowIndex, 3)
                                Dim objfrmPrefix As New gloSettings.frmPrefix(ID, sServer, sDatabase, sPrefix, gstrConnectionString)
                                objfrmPrefix.ShowDialog()
                                Call Fill_Prefix()
                            Else
                                Exit Sub
                            End If
                        End If
                    Case "Provider"
                        UpdateErrorLog("Modifying Provider", mdlFunctions.enmErrorOccuredForm.MainForm, mdlFunctions.enmOperation.Update)
                        Dim objDoctor As New clsProvider(gstrConnectionString)
                        Dim frmgloEMRDoctor As New frmDoctor(CType(trvCategory.SelectedNode.Tag, Int64))
                        frmgloEMRDoctor.Text = "Modify Provider"
                        'frmgloEMRDoctor.blnModify = True
                        Dim myNode As New TreeNode
                        myNode = trvCategory.SelectedNode
                        'objDoctor.SearchProvider(trvCategory.SelectedNode.Text)
                        'frmgloEMRDoctor.Fill_DcotorTypes()

                        'frmgloEMRDoctor.txtFirstName.Tag = objDoctor.ProviderID
                        'frmgloEMRDoctor.txtFirstName.Text = objDoctor.FirstName
                        'frmgloEMRDoctor.txtMiddleName.Text = objDoctor.MiddleName
                        'frmgloEMRDoctor.txtLastName.Text = objDoctor.LastName
                        'frmgloEMRDoctor.txtAddress.Text = objDoctor.Address
                        'frmgloEMRDoctor.txtStreet.Text = objDoctor.Street
                        'frmgloEMRDoctor.txtCity.Text = objDoctor.City
                        'frmgloEMRDoctor.txtState.Text = objDoctor.State
                        'frmgloEMRDoctor.txtZIP.Text = objDoctor.ZIP
                        'frmgloEMRDoctor.txtPhoneNo.Text = objDoctor.Phone
                        'frmgloEMRDoctor.txtMobileNo.Text = objDoctor.Mobile
                        'frmgloEMRDoctor.txtPager.Text = objDoctor.Pager
                        'frmgloEMRDoctor.txtFAX.Text = objDoctor.FAX
                        'frmgloEMRDoctor.txtEmailAddress.Text = objDoctor.Email
                        'frmgloEMRDoctor.txtURL.Text = objDoctor.URL
                        'frmgloEMRDoctor.txtDEA.Text = objDoctor.DEA
                        'frmgloEMRDoctor.txtNPI.Text = objDoctor.NPI
                        'frmgloEMRDoctor.txtUPIN.Text = objDoctor.UPIN
                        'frmgloEMRDoctor.txtStateMedicalLicenseNo.Text = objDoctor.StateMedicalNo

                        ''sarika 7th june 07
                        'frmgloEMRDoctor.txtPrefix.Text = objDoctor.Prefix
                        '-------------------------
                        'If IsDBNull(objDoctor.ActiveStartTime) Or objDoctor.ActiveStartTime = Nothing Then
                        '    frmgloEMRDoctor.dtpActiveStartTime.Value = DateTime.Now
                        'Else
                        '    frmgloEMRDoctor.dtpActiveStartTime.Value = objDoctor.ActiveStartTime
                        'End If
                        'If IsDBNull(objDoctor.ActiveEndTime) Or objDoctor.ActiveEndTime = Nothing Then
                        '    frmgloEMRDoctor.dtpActiveEndTime.Value = DateTime.Now.AddYears(1)
                        'Else
                        '    frmgloEMRDoctor.dtpActiveEndTime.Value = objDoctor.ActiveEndTime
                        'End If
                        'Dim strServiceLevel As String
                        'If IsDBNull(objDoctor.ServiceLevel) Or objDoctor.ServiceLevel Is Nothing Or objDoctor.ServiceLevel = "" Then
                        '    strServiceLevel = ""
                        'Else
                        '    strServiceLevel = objDoctor.ServiceLevel
                        'End If
                        'If strServiceLevel = "" Then
                        '    frmgloEMRDoctor.chckDisable.Checked = True
                        'ElseIf strServiceLevel = "0000000000000000" Then
                        '    frmgloEMRDoctor.chckDisable.Checked = True
                        'ElseIf strServiceLevel = "0000000000000001" Then
                        '    frmgloEMRDoctor.chckDisable.Checked = False
                        '    frmgloEMRDoctor.chckRefill.Checked = False
                        '    frmgloEMRDoctor.chckNew.Checked = True
                        'ElseIf strServiceLevel = "0000000000000010" Then
                        '    frmgloEMRDoctor.chckDisable.Checked = False
                        '    frmgloEMRDoctor.chckNew.Checked = False
                        '    frmgloEMRDoctor.chckRefill.Checked = True
                        'ElseIf strServiceLevel = "0000000000000011" Then
                        '    frmgloEMRDoctor.chckDisable.Checked = False
                        '    frmgloEMRDoctor.chckRefill.Checked = True
                        '    frmgloEMRDoctor.chckNew.Checked = True
                        'End If
                        'If IsDBNull(objDoctor.SPI) Or objDoctor.SPI Is Nothing Or objDoctor.SPI = "" Then
                        '    frmgloEMRDoctor.rbPrescriber.Checked = True
                        '    frmgloEMRDoctor.pnlSPI.Visible = False
                        '    frmgloEMRDoctor.rbUpdate.Enabled = False
                        'Else
                        '    frmgloEMRDoctor.rbPrescriber.Enabled = False
                        '    frmgloEMRDoctor.rbUpdate.Checked = True
                        '    frmgloEMRDoctor.pnlSPI.Visible = True
                        '    frmgloEMRDoctor.txtSPI.Visible = False
                        '    frmgloEMRDoctor.lblRoot.Visible = False
                        '    frmgloEMRDoctor.lblSPI.Text = objDoctor.SPI
                        '    frmgloEMRDoctor.SPICode = objDoctor.SPI
                        '    frmgloEMRDoctor.txtSPI.Enabled = False
                        'End If

                        'If UCase(Trim(objDoctor.Gender)) = "MALE" Then
                        '    frmgloEMRDoctor.optMale.Checked = True
                        'Else
                        '    frmgloEMRDoctor.optFemale.Checked = True
                        'End If
                        'If IsNothing(objDoctor.Signature) = False Then
                        '    frmgloEMRDoctor.picSignature.Image = CType(objDoctor.Signature, Image)
                        '    frmgloEMRDoctor.picSignature.SizeMode = PictureBoxSizeMode.StretchImage
                        'End If
                        ''Retrieve Provider Type Name
                        'Dim objProviderType As New clsProviderType(gstrConnectionString)
                        'objProviderType.SearchProviderType(CInt(objDoctor.ProviderTypeID))
                        'frmgloEMRDoctor.cmbDoctorType.Text = objProviderType.ProviderType
                        'objProviderType = Nothing

                        'frmgloEMRDoctor.picSignature.Visible = True
                        '' Removed Signature Pad Control 20090603
                        'frmgloEMRDoctor.AxSigPlus1.Visible = False
                        ''
                        'frmgloEMRDoctor.grpUserDetails.Enabled = False

                        If frmgloEMRDoctor.ShowDialog() = DialogResult.OK Then
                            trvCategory.SelectedNode = myNode
                            Call Fill_CategoryProviders()
                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                Call Fill_DetailsProviders()
                            Else
                                dgData.DataSource = Nothing
                            End If
                        End If
                        btnRefresh_Click(Nothing, Nothing)
                        frmgloEMRDoctor = Nothing
                        objDoctor = Nothing
                End Select
                '-------------------------------------------
                'sarika 25th apr 2007
                'ElseIf enmUserOperation = enmOperation.Database Then
                '    If Trim(trvDatabase.SelectedNode.Text) = "Database" Then
                '        Exit Sub
                '    End If
                '    Select Case Trim(trvDatabase.SelectedNode.Text)
                '        Case "Database Update"
                '            Me.Cursor = Cursors.WaitCursor
                '            Dim objDB As New clsDBUpdation(gstrConnectionString)
                '            If dgData.Item(dgData.CurrentRowIndex, 5) = "Yes" Then
                '                objDB.SendStatusTogloStream(dgData.Item(dgData.CurrentRowIndex, 1), dgData.Item(dgData.CurrentRowIndex, 2), dgData.Item(dgData.CurrentRowIndex, 3), dgData.Item(dgData.CurrentRowIndex, 4), True)
                '            Else
                '                objDB.SendStatusTogloStream(dgData.Item(dgData.CurrentRowIndex, 1), dgData.Item(dgData.CurrentRowIndex, 2), dgData.Item(dgData.CurrentRowIndex, 3), dgData.Item(dgData.CurrentRowIndex, 4), False)
                '            End If

                '            objDB = Nothing
                '            Me.Cursor = Cursors.Default
                '    End Select
                '-------------------------------------------
            ElseIf enmUserOperation = enmOperation.Tools Then
                If Trim(trvTools.SelectedNode.Text) = "Tools" Then
                    Exit Sub
                End If

                'sarika 27th june 07
                'Select Case Trim(trvTools.SelectedNode.Text)
                '    Case "Client Message"
                '        Dim frmClient As New frmClientMessage
                '        Dim objClient As New clsClientMessage
                '        frmClient.blnModify = True
                '        objClient.ScanClientMessage(dgData.Item(dgData.CurrentRowIndex, 0))
                '        frmClient.cmbCategory.Tag = dgData.Item(dgData.CurrentRowIndex, 0)
                '        frmClient.cmbCategory.Text = objClient.Category
                '        frmClient.dtFromDate.Value = objClient.FromDate.Date
                '        frmClient.dtFromTime.Value = objClient.FromDate.Date & " " & Format(objClient.FromDate, "hh:mm:ss tt")
                '        If objClient.blnEndDate = True Then
                '            frmClient.optEndDate.Checked = True
                '            frmClient.dtToDate.Enabled = True
                '            frmClient.dtToTime.Enabled = True
                '            frmClient.dtToDate.Value = objClient.ToDate.Date
                '            frmClient.dtToTime.Value = objClient.ToDate.Date & " " & Format(objClient.ToDate, "hh:mm:ss tt")
                '        Else
                '            frmClient.dtToDate.Enabled = False
                '            frmClient.dtToTime.Enabled = False
                '            frmClient.optNoEndDate.Checked = True
                '        End If
                '        frmClient.txtClientMessage.Text = objClient.Message
                '        If frmClient.ShowDialog() = DialogResult.OK Then
                '            Call Fill_CategoryClientMessage()
                '            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                '                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                '                Call Fill_DetailsClientMessages()
                '            Else
                '                dgData.DataSource = Nothing
                '            End If
                '        End If
                '        Exit Sub
                'End Select
                '------------------
            End If
        Catch objErr As Exception
            btnCloneProvider.Visible = False
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'sarika 11th sept 07
    'Public Sub Fill_DoctorTypeInfo()
    '    trvCategory.BeginUpdate()
    '    Dim trvChild As TreeNode
    '    trvCategory.Nodes.Clear()
    '    Dim clCategories As New Collection

    '    Try
    '        Dim objDoctorType As New clsProviderType(gstrConnectionString)
    '        clCategories = objDoctorType.Fill_ProviderTypes
    '        objDoctorType = Nothing


    '        Dim nCount As Integer

    '        For nCount = 1 To clCategories.Count
    '            trvChild = New TreeNode
    '            With trvChild
    '                .Text = clCategories.Item(nCount)
    '                .ImageIndex = 0
    '                .SelectedImageIndex = 0
    '                .ForeColor = Color.Black
    '            End With
    '            trvCategory.Nodes.Add(trvChild)
    '            trvChild = Nothing
    '        Next
    '        trvCategory.EndUpdate()
    '        trvCategory.ExpandAll()
    '        'If trvCategory.GetNodeCount(False) >= 1 Then
    '        '    If Trim(gstrCategory) = "" Then
    '        '        trvCategory.SelectedNode = trvCategory.Nodes(0)
    '        '        gstrCategory = trvCategory.Nodes(0).Text
    '        '    Else
    '        '        If SelectCategoryInTreeView(gstrCategory) = False Then
    '        '            trvCategory.SelectedNode = trvCategory.Nodes(0)
    '        '            gstrCategory = trvCategory.Nodes(0).Text
    '        '        End If
    '        '    End If
    '        '    enmCurrentFacility = enmType
    '        '    Fill_Details(Trim(gstrCategory))
    '        'End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
    '-------------------------------------------------------

    Private Sub picgloEMRAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            trvAdminMenu.Visible = Not trvAdminMenu.Visible
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub picTools_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            trvTools.Visible = Not trvTools.Visible
            If trvTools.Visible = False Then
                'picTools.Dock = DockStyle.Bottom
            Else
                'picTools.Dock = DockStyle.Top
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'sarika 25th apr 2007
    'Private Sub picDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        trvDatabase.Visible = Not trvDatabase.Visible
    '        If trvDatabase.Visible = False Then
    '            picDatabase.Dock = DockStyle.Bottom
    '        Else
    '            picDatabase.Dock = DockStyle.Top
    '        End If
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub picAudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            trvAudit.Visible = Not trvAudit.Visible
            If trvAudit.Visible = False Then
                'picAudit.Dock = DockStyle.Bottom
            Else
                'picAudit.Dock = DockStyle.Top
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Select Case enmUserOperation
                Case enmOperation.Admin
                    If Trim(trvAdminMenu.SelectedNode.Text) Is trvAdminMenu.Nodes(0) Then Exit Sub
                    Select Case Trim(trvAdminMenu.SelectedNode.Text)
                        Case "Windows Groups & Users"
                            Call Fill_CategoryWindowsGroupsUsers()
                            If trvCategory.Nodes(0).Nodes(0).GetNodeCount(True) >= 1 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsWindowsGroupsUsers()
                            End If
                            Exit Sub
                        Case "QEMR Groups"
                            Call Fill_CategorygloEMRGroups()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsgloEMRGroups()
                            End If
                            Exit Sub
                        Case "User Groups"
                            Call Fill_CategoryUserGroups()
                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                Call Fill_DetailsUserGroups()
                            Else
                                dgData.DataSource = Nothing
                            End If
                            Exit Sub
                        Case "Multiple Database"
                            ''it will refresh the treeview catatogery
                            Fill_MultipleDB()
                            ''here we have checked wheather the tree has value or not
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                ''these conditon will be true when it goes to the 
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    ''now set the selected nodes as the first node
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                ''now fill the data to the datagrid
                                Fill_DetailsMultipleDB()
                            End If
                        Case "User Management"
                            Call Fill_CategorygloEMRUsers()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsgloEMRUsers()
                            End If
                            Exit Sub
                        Case "Clinic"
                            Call Fill_CategoryClinics()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsClinics()
                            End If
                            Exit Sub
                        Case "Backup"
                            Call Fill_CategoryBackups()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsBackups()
                            End If
                            Exit Sub
                        Case "Restore"
                            Call Fill_CategoryRestores()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsRestores()
                            End If
                            Exit Sub
                        Case "Self Notes"
                            Call Fill_CategorySelfNotes()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsSelfNotes()
                            End If
                            Exit Sub
                        Case "Login Users"
                            Call Fill_DetailsLoginUsers()
                            Exit Sub
                        Case "Provider"
                            Call Fill_CategoryProviders()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsProviders()
                            End If
                            Exit Sub
                        Case "Client Settings"
                            Call Fill_CategoryClientMachines()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsClientSettings()
                            End If
                            Exit Sub

                            'sarika 11th sept 07
                        Case "Doctor Type"
                            Call Fill_DoctorType()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsDoctorTypes()
                            End If
                            Exit Sub
                        Case "Clearinghouse"
                            Fill_ClearingHouse()
                            Exit Sub
                            'below by hemant
                        Case "CMS1500 02/12 Settings"
                            Fill_Printers()
                            Exit Sub
                            'above by hemant
                        Case "UB04 Settings"
                            Fill_Printers(True)
                            Exit Sub
                    End Select
                Case enmOperation.Audit
                    If Trim(trvAudit.SelectedNode.Text) Is trvAudit.Nodes(0) Then Exit Sub
                    Select Case Trim(trvAudit.SelectedNode.Text)
                        Case "Report"
                            '---------------------------------------
                            'code commented by sarika - 21st may 07

                            '    Call Fill_CategoryUsers()
                            '  Call Fill_AuditCategories()
                            'If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                            '    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                            'End If
                            '---------------------------------------

                            Call Fill_DetailsAuditReports()
                            Exit Sub
                        Case "Archived Audit Log"
                            Fill_CategoryArchivedUsers()
                            Call Fill_ArchivedAuditCategories()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsArchivedAuditReports()
                            End If
                            Exit Sub
                    End Select
                    '-------------------------------------------
                    'sarika 35th apr 2007
                    'Case enmOperation.Database
                    '    If trvDatabase.SelectedNode Is trvDatabase.Nodes(0) Then Exit Sub
                    '    Select Case Trim(trvDatabase.SelectedNode.Text)
                    '        Case "Database Update"
                    '            Call Fill_CategoryDBVersions()
                    '            If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                    '                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    '            End If
                    '            Call Fill_DetailsDBUpdations()
                    '            Exit Sub
                    '    End Select
                    '-------------------------------------------
                Case enmOperation.Tools
                    If trvTools.SelectedNode Is trvTools.Nodes(0) Then Exit Sub
                    Select Case Trim(trvTools.SelectedNode.Text)
                        Case "Client Message"
                            Call Fill_CategoryClientMessage()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then

                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsClientMessages()
                            End If
                            Exit Sub
                        Case "Online Updates"
                            Call Fill_CategoryOnlineUpdates()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsOnlineUpdates()
                            End If
                            Exit Sub
                        Case "Suggestions to gloStream"
                            Call Fill_CategorySuggestions()
                            If trvCategory.Nodes(0).Nodes.Count > 0 Then
                                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                End If
                                Call Fill_DetailsSuggestions()
                            End If
                            Exit Sub
                    End Select

            End Select
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub trvAudit_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvAudit.AfterSelect
    '    Try
    '        enmUserOperation = enmOperation.Audit
    '        pnlAudit.Visible = False
    '        pnlCommandButtons.Visible = True
    '        btnNew.Visible = True
    '        btnNew.Text = "New"
    '        btnModify.Visible = True
    '        btnModify.Text = "Modify"
    '        btnDelete.Visible = True
    '        btnDelete.Text = "Delete"



    '        pnlMainMainTop.Visible = True
    '        picMainSepMain.Visible = True
    '        optSelfNotesCategory.Visible = False
    '        optSelfNotesStatus.Visible = False
    '        With dtFrom
    '            .Format = DateTimePickerFormat.Custom
    '            .CustomFormat = DTFORMAT
    '            .Value = Date.Now
    '        End With
    '        With dtTo
    '            .Format = DateTimePickerFormat.Custom
    '            .CustomFormat = DTFORMAT
    '            .Value = Date.Now
    '        End With

    '        If trvAudit.SelectedNode Is trvAudit.Nodes(0) Then
    '            picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topaudit.JPG")
    '            dgData.Visible = False
    '            SplitterMainCategory.Visible = False
    '            trvCategory.Visible = False
    '            picMainSepMain.Visible = False
    '            pnlMainMainTop.Visible = False
    '            picMainSepTop.Visible = False
    '            Exit Sub
    '        End If

    '        If trvAudit.SelectedNode Is trvAudit.Nodes(1) Then
    '            picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topaudit.JPG")
    '            dgData.Visible = False
    '            SplitterMainCategory.Visible = False
    '            trvCategory.Visible = False
    '            picMainSepMain.Visible = False
    '            pnlMainMainTop.Visible = False
    '            picMainSepTop.Visible = False
    '            Exit Sub
    '        End If

    '        picMainSepTop.Visible = True
    '        trvCategory.Visible = True
    '        SplitterMainCategory.Visible = True
    '        dgData.Visible = True

    '        Select Case Trim(trvAudit.SelectedNode.Text)
    '            Case "Report"
    '                pnlCommandButtons.Visible = False
    '                cmbSearchPatient.SelectedIndex = 0
    '                lblAuditCategory.Visible = True
    '                cmbAuditCategory.Visible = True
    '                pnlAudit.Visible = True
    '                cmbSearchPatient.Visible = True
    '                lblPatientID.Visible = False
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topauditreport.JPG")
    '                Call Fill_CategoryUsers()
    '                Call Fill_AuditCategories()
    '                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "Archive"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topDatabasearchive.JPG")
    '                dgData.Visible = False
    '                SplitterMainCategory.Visible = False
    '                trvCategory.Visible = False
    '                picMainSepMain.Visible = False
    '                pnlMainMainTop.Visible = False
    '                picMainSepTop.Visible = False
    '                Dim frmArchive As New frmArchiveAudit
    '                frmArchive.ShowDialog()
    '            Case "Archived Audit Report"
    '                cmbSearchPatient.Visible = False
    '                lblPatientID.Visible = True
    '                pnlCommandButtons.Visible = False
    '                cmbSearchPatient.SelectedIndex = 0
    '                lblAuditCategory.Visible = True
    '                cmbAuditCategory.Visible = True
    '                pnlAudit.Visible = True
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toparchivedauditreport.gif")
    '                Call Fill_CategoryArchivedUsers()
    '                Call Fill_ArchivedAuditCategories()
    '                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "Restore Archive"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topDatabasearchive.JPG")
    '                dgData.Visible = False
    '                SplitterMainCategory.Visible = False
    '                trvCategory.Visible = False
    '                picMainSepMain.Visible = False
    '                pnlMainMainTop.Visible = False
    '                picMainSepTop.Visible = False
    '                Dim frmArchive As New frmUnArchive
    '                frmArchive.ShowDialog()
    '        End Select

    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub



    'Private Sub trvDatabase_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvDatabase.AfterSelect
    '    Try
    '        enmUserOperation = enmOperation.Database
    '        If Trim(trvDatabase.SelectedNode.Text) = "Database" Then
    '            picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdatabase.JPG")
    '            dgData.Visible = False
    '            SplitterMainCategory.Visible = False
    '            trvCategory.Visible = False
    '            picMainSepMain.Visible = False
    '            pnlMainMainTop.Visible = False
    '            picMainSepTop.Visible = False
    '            Exit Sub
    '        End If
    '        picMainSepTop.Visible = True
    '        trvCategory.Visible = True
    '        SplitterMainCategory.Visible = True
    '        dgData.Visible = True

    '        Select Case Trim(trvDatabase.SelectedNode.Text)
    '            Case "Database Tool"
    '                Dim frmDatabaseTool As New frmDBTool
    '                frmDatabaseTool.ShowDialog()
    '        End Select

    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try


    'End Sub

    'Private Sub trvTools_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvTools.AfterSelect
    '    Try
    '        enmUserOperation = enmOperation.Tools

    '        lblAuditCategory.Visible = False
    '        cmbAuditCategory.Visible = False
    '        pnlAudit.Visible = False
    '        optSelfNotesCategory.Visible = False
    '        optSelfNotesStatus.Visible = False

    '        pnlCommandButtons.Visible = True
    '        btnNew.Visible = True
    '        btnNew.Text = "New"
    '        btnModify.Visible = True
    '        btnModify.Text = "Modify"
    '        btnDelete.Visible = True
    '        btnDelete.Text = "Delete"

    '        pnlMainMainTop.Visible = True
    '        picMainSepMain.Visible = True
    '        With dtFrom
    '            .Format = DateTimePickerFormat.Custom
    '            .CustomFormat = DTFORMAT
    '            .Value = Date.Now
    '        End With
    '        With dtTo
    '            .Format = DateTimePickerFormat.Custom
    '            .CustomFormat = DTFORMAT
    '            .Value = Date.Now
    '        End With

    '        If Trim(trvTools.SelectedNode.Text) = "Tools" Then
    '            picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toptools.JPG")
    '            dgData.Visible = False
    '            SplitterMainCategory.Visible = False
    '            trvCategory.Visible = False
    '            picMainSepMain.Visible = False
    '            pnlMainMainTop.Visible = False
    '            picMainSepTop.Visible = False
    '            Exit Sub
    '        End If
    '        picMainSepTop.Visible = True
    '        trvCategory.Visible = True
    '        SplitterMainCategory.Visible = True
    '        dgData.Visible = True

    '        Select Case Trim(trvTools.SelectedNode.Text)
    '            Case "Client Message"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topclientmessage.JPG")
    '                Call Fill_CategoryClientMessage()
    '                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "Suggestions to gloStream"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topsuggestionstogloStream.JPG")
    '                Call Fill_CategorySuggestions()
    '                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If
    '            Case "Online Updates"
    '                picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toponlineupdates.JPG")
    '                Call Fill_CategoryOnlineUpdates()
    '                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
    '                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
    '                End If


    '        End Select

    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    Private Sub tlbBar_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
        ''Try
        'Select Case tlbBar.Buttons.IndexOf(e.Button)
        '    Case 0
        '        'Windows Groups & Users
        '        trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(0)
        '        Call ShowAdministrator()
        '    Case 1
        '        'QEMR Groups
        '        trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(1)
        '        Call ShowAdministrator()
        '    Case 2
        '        'gloEMR User
        '        trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(2)
        '        Call ShowAdministrator()
        '    Case 4
        '        'Clinic
        '        trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(3)
        '        Call ShowAdministrator()
        '    Case 5
        '        'Doctor
        '        trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(4)
        '        Call ShowAdministrator()
        '    Case 6
        '        'Client Settings
        '        trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(5)
        '        Call ShowAdministrator()
        '    Case 8
        '        'Backup
        '        trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(1).Nodes(0)
        '        Call ShowAdministrator()
        '    Case 11
        '        'Self Notes
        '        trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(2).Nodes(0)
        '        Call ShowAdministrator()
        '    Case 12
        '        'Login Users
        '        trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(2).Nodes(1)
        '        Call ShowAdministrator()
        '    Case 14
        '        'Audit Reports
        '        trvAudit.SelectedNode = trvAudit.Nodes(0).Nodes(0)
        '        Call ShowAudits()
        '    Case 15
        '        'Archive Audit
        '        trvAudit.SelectedNode = trvAudit.Nodes(0).Nodes(1)
        '        Call ShowAudits()
        '    Case 16
        '        'Archived Audit Report
        '        trvAudit.SelectedNode = trvAudit.Nodes(1).Nodes(0)
        '        Call ShowAudits()
        '    Case 17
        '        'Restore Archive
        '        trvAudit.SelectedNode = trvAudit.Nodes(1).Nodes(1)
        '        Call ShowAudits()
        '    Case 21
        '        'Database Tool
        '        trvDatabase.SelectedNode = trvDatabase.Nodes(0).Nodes(0)
        '        Call showDBMGMT()
        '    Case 23
        '        'Client Message
        '        trvTools.SelectedNode = trvTools.Nodes(0).Nodes(0)
        '        Call showDBTool()
        '    Case 24
        '        'Online Updates
        '        trvTools.SelectedNode = trvTools.Nodes(0).Nodes(1)
        '        Call showDBTool()
        '    Case 25
        '        'Suggesstions to gloStream
        '        trvTools.SelectedNode = trvTools.Nodes(0).Nodes(2)
        '        Call showDBTool()
        '    Case 27
        '        'About us
        '        Dim frm As New frmAboutUs
        '        frm.ShowDialog()
        '    Case 29
        '        'Lock Screen
        '        Dim frm As New frmLockScreen
        '        frm.ShowDialog()
        '    Case 31
        '        'Close
        '        Me.Close()
        'End Select
        ''Commented on 25th June 2007 - Shilpa
        Select Case Trim(e.Button.Tag)
            ''        Case "WindowsGroupsUsers"
            ''            'Windows Groups & Users
            ''            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(0)
            ''            Call ShowAdministrator()
            ''        Case "gloEMRGroups"
            ''            'QEMR Groups
            ''            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(1)
            ''            Call ShowAdministrator()
            ''        Case "UserMGNT"
            ''            'gloEMR User
            ''            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(2)
            ''            Call ShowAdministrator()
            ''        Case "Clinic"
            ''            'Clinic
            ''            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(3)
            ''            Call ShowAdministrator()
            ''        Case "Provider"
            ''            'Doctor
            ''            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(4)
            ''            Call ShowAdministrator()
            ''        Case "Machines"
            ''            'Client Settings
            ''            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(5)
            ''            Call ShowAdministrator()
            ''        Case "Backup"
            ''            'Backup
            ''            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(1).Nodes(0)
            ''            Call ShowAdministrator()
            ''        Case "SelfNotes"
            ''            'Self Notes
            ''            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(2).Nodes(0)
            ''            Call ShowAdministrator()
            ''        Case "LoginUsers"
            ''            'Login Users
            ''            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(2).Nodes(1)
            ''            Call ShowAdministrator()
            ''        Case "Settings"
            ''            'Settings
            ''            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(2).Nodes(2)
            ''            Call ShowAdministrator()
            ''        Case "VoiceTrainingDocument"
            ''            'Voice Training Document
            ''            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(2).Nodes(3)
            ''            Call ShowAdministrator()
            ''        Case "AuditReport"
            ''            'Audit Reports
            ''            trvAudit.SelectedNode = trvAudit.Nodes(0).Nodes(0)
            ''            Call ShowAudits()
            ''        Case "ArchiveAudit"
            ''            'Archive Audit
            ''            trvAudit.SelectedNode = trvAudit.Nodes(0).Nodes(1)
            ''            Call ShowAudits()
            ''        Case "ArchivedReport"
            ''            'Archived Audit Report
            ''            trvAudit.SelectedNode = trvAudit.Nodes(1).Nodes(0)
            ''            Call ShowAudits()
            ''        Case "RestoreArchive"
            ''            'Restore Archive
            ''            trvAudit.SelectedNode = trvAudit.Nodes(1).Nodes(1)
            ''            Call ShowAudits()
            ''            '----------------------------------
            ''            'sarika 25th apr 2007
            ''            'Case "DBTool"
            ''            '    'Database Tool
            ''            '    trvDatabase.SelectedNode = trvDatabase.Nodes(0).Nodes(0)
            ''            '    Call showDBMGMT()
            ''            '----------------------------------
            ''        Case "ClientMessage"
            ''            'Client Message
            ''            trvTools.SelectedNode = trvTools.Nodes(0).Nodes(0)
            ''            Call showDBTool()
            ''        Case "OnlineUpdates"
            ''            'Online Updates
            ''            trvTools.SelectedNode = trvTools.Nodes(0).Nodes(1)
            ''            Call showDBTool()
            ''        Case "Suggestion"
            ''            'Suggesstions to gloStream
            ''            trvTools.SelectedNode = trvTools.Nodes(0).Nodes(2)
            ''            Call showDBTool()
            ''        Case "AboutUs"
            ''            'About us
            ''            Dim frm As New frmAboutUs
            ''            frm.ShowDialog()
            ''        Case "LockScreen"
            ''            'Lock Screen
            ''            Dim frm As New frmLockScreen
            ''            frm.ShowDialog()
            ''        Case "StartUpSettings"
            ''            Dim frmSettings As New frmStartupSettings
            ''            frmSettings.blnOpenFromMainForm = True
            ''            frmSettings.ShowDialog()
            ''        Case "Close"
            ''            'Close
            ''            Me.Close()

            ''            'sarika  21 feb
            ''            Dim objAudit As New clsAudit
            ''            objAudit.CreateLog(clsAudit.enmActivityType.Logout, gstrLoginName & " user has logged out.", gstrLoginName, gstrClientMachineName, 0, True)
            ''            objAudit = Nothing
            ''            '-------------

            ''        Case "LogOut"

            ''            'sarika  21 feb
            ''            Dim objAudit As New clsAudit
            ''            objAudit.CreateLog(clsAudit.enmActivityType.Logout, gstrLoginName & " user has logged out.", gstrLoginName, gstrClientMachineName)
            ''            objAudit = Nothing
            ''            '-------------


            ''            Me.Hide()
            ''            Dim frm As New frmSplash
            ''            frm.ShowDialog()


            ''//Code added by Ravikiran on 10/02/2007
            Case "RxReport Designer"
                Dim frm As New frmRxReportDesigner
                frm.ShowDialog()
                '/// Updation Ends

                ''            '------------------------------
                ''            'Sarika 24th Apr 2007
                ''        Case "LSAssociation"
                ''            Dim frmLocStatus As New frmLocationStatus
                ''            frmLocStatus.ShowDialog()

                ''            Dim objAudit As New clsAudit
                ''            objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, gstrLoginName & " user has viewed the Clinic WorkFlow Settings.", gstrLoginName, gstrClientMachineName, 0, True)
                ''            objAudit = Nothing
                ''            '------------------------------
        End Select


        ''Catch objErr As Exception
        ''    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub

    Private Sub frmgloEMRAdmin_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            'sarika  21 feb
            If (_LogoutStatus = True) Then
                _LogoutStatus = False
            Else

                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.Logout, gstrLoginName & " user has logged out.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '-------------

                If MessageBox.Show("Are you sure, you want to close the application?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    'Bug #40928: gloPM Admin >> Application shows “Another instance of this application is already running.” Message.
                    If IsNothing(gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM)) = False Then
                        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, True)
                        gloRegistrySetting.SetRegistryValue("LeftWidth", pnlLeft.Width)
                        gloRegistrySetting.SetRegistryValue("CategoryWidth", trvCategory.Width)
                        gloRegistrySetting.CloseRegistryKey()
                        End
                    End If
                Else
                    e.Cancel = True
                End If
            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub CheckUncheckNode(ByVal strNodeText As String, ByVal trvView As TreeView, Optional ByVal blnCheck As Boolean = True)
        Dim trvNode As TreeNode
        For Each trvNode In trvView.Nodes
            If CheckUncheckNode1(trvNode, strNodeText, blnCheck) = True Then
                Exit Sub
            End If
        Next
    End Sub

    Private Function CheckUncheckNode1(ByVal rootNode As TreeNode, ByVal strNodeText As String, ByVal blnCheck As Boolean) As Boolean
        For Each childNode As TreeNode In rootNode.Nodes
            ' If Trim(childNode.Text) = strNodeText Then
            ''Sandip Darade  20090818
            If (childNode.Tag) = Convert.ToDecimal(strNodeText) Then
                childNode.Checked = blnCheck
                Return True
                Exit Function
            End If
            CheckUncheckNode1(childNode, strNodeText, blnCheck)
        Next
    End Function

    'Private Sub dgData_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgData.MouseUp
    '    Try
    '        If dgData.CurrentRowIndex >= 0 Then
    '            dgData.Select(dgData.CurrentRowIndex)
    '        End If
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    Private Sub txtPatient_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPatient.TextChanged
        'Dim dv As DataView
        'If Trim(txtPatient.Text) = "" Then
        '    dv = CType(dgData.DataSource, DataView)
        '    Exit Sub
        'End If
        'dv = CType(dgData.DataSource, DataView)
        'dv.RowFilter = dv.Table.Columns(4).ColumnName & " Like '%" & Trim(txtPatient.Text) & "%'"
        ''If Trim(cmbSearchPatient.SelectedItem) = "" Then

        ''Else

        ''End If
    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click

    End Sub

    Private Sub picTools_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'picTools.Image = Image.FromFile(Application.StartupPath & "\Images\bluemaintools.gif")
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub picTools_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'picTools.Image = Image.FromFile(Application.StartupPath & "\Images\yellowmaintools.gif")
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'sarika 25th apr 2007
    'Private Sub picDatabase_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        picDatabase.Image = Image.FromFile(Application.StartupPath & "\Images\yellowmaindbmgmt.gif")
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'sarika 25th apr 2007
    'Private Sub picDatabase_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        picDatabase.Image = Image.FromFile(Application.StartupPath & "\Images\bluemaindbmgmt.gif")
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub picAudit_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'picAudit.Image = Image.FromFile(Application.StartupPath & "\Images\yellowmainaudit.gif")
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub picAudit_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'picAudit.Image = Image.FromFile(Application.StartupPath & "\Images\bluemainaudit.gif")
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnHideToolBar_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHideToolBar.MouseEnter
        Try
            'btnHideToolBar.BackgroundImage = Image.FromFile(Application.StartupPath & "\Images\yellowhidetoolbar.gif")
            btnHideToolBar.BackgroundImage = Global.gloPMAdmin.My.Resources.yellowhidetoolbar

            '******By Sandip Deshmukh 18 Oct 07 12.54PM Bug# 353
            '******to show tooltip information
            ' Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000
            toolTip1.InitialDelay = 1000
            toolTip1.ReshowDelay = 100
            ' Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = True
            If btnHideToolBar.Text = "Hide Toolbar" Then
                toolTip1.SetToolTip(Me.btnHideToolBar, "Hide Toolbar")
            Else
                toolTip1.SetToolTip(Me.btnHideToolBar, "Show Toolbar")
            End If
            '******18 Oct 07 12.54PM Bug# 353
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnHideToolBar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHideToolBar.MouseLeave
        Try
            btnHideToolBar.BackgroundImage = Image.FromFile(Application.StartupPath & "\Images\bluehidetoolbar.gif")
            btnHideToolBar.BackgroundImage = Global.gloPMAdmin.My.Resources.bluehidetoolbar
            '******By Sandip Deshmukh 18 Oct 07 12.54PM Bug# 353
            '******to show tooltip information
            ' Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000
            toolTip1.InitialDelay = 1000
            toolTip1.ReshowDelay = 100
            ' Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = True
            If btnHideToolBar.Text = "Hide Toolbar" Then
                toolTip1.SetToolTip(Me.btnHideToolBar, "Hide Toolbar")
            Else
                toolTip1.SetToolTip(Me.btnHideToolBar, "Show Toolbar")
            End If
            '******18 Oct 07 12.54PM Bug# 353
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvAdminMenu_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvAdminMenu.MouseDown
        'Try


        Dim trvNode As TreeNode
        trvNode = trvAdminMenu.GetNodeAt(e.X, e.Y)
        If IsNothing(trvNode) = False Then
            trvAdminMenu.SelectedNode = trvNode
            Call ShowAdministrator()
        End If
        'Catch objErr As Exception
        'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub trvAudit_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvAudit.MouseDown
        Try
            Dim trvNode As TreeNode
            trvNode = trvAudit.GetNodeAt(e.X, e.Y)
            If IsNothing(trvNode) = False Then
                trvAudit.SelectedNode = trvNode
                Call ShowAudits()
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'sarika 25th apr 2007
    'Private Sub trvDatabase_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    Try
    '        Dim trvNode As TreeNode
    '        trvNode = trvDatabase.GetNodeAt(e.X, e.Y)
    '        If IsNothing(trvNode) = False Then
    '            trvDatabase.SelectedNode = trvNode
    '            Call showDBMGMT()
    '        End If
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub trvTools_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvTools.MouseDown
        Try
            Dim trvNode As TreeNode
            trvNode = trvTools.GetNodeAt(e.X, e.Y)
            If IsNothing(trvNode) = False Then
                trvTools.SelectedNode = trvNode
                Call showDBTool()
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvCategory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCategory.MouseDown
        Try
            Dim trvNode As TreeNode
            trvNode = trvCategory.GetNodeAt(e.X, e.Y)
            If IsNothing(trvNode) = False Then
                trvCategory.SelectedNode = trvNode
                If enmUserOperation = enmOperation.Admin Then
                    If IsNothing(trvAdminMenu.SelectedNode) = True Then
                        Exit Sub
                    End If
                    If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
                        Exit Sub
                    End If
                    Me.Cursor = Cursors.WaitCursor
                    Select Case Trim(trvAdminMenu.SelectedNode.Text)
                        Case "Windows Groups & Users"
                            Call Fill_DetailsWindowsGroupsUsers()
                        Case "QEMR Groups"
                            Call Fill_DetailsgloEMRGroups()
                            'Added For User groups
                            'sandip Darade 6th Feb 2009
                        Case "User Groups"
                            Call Fill_DetailsUserGroups()
                        Case "User Management"
                            Call Fill_DetailsgloEMRUsers()
                        Case "Doctor"
                            Call Fill_DetailsProviders()
                        Case "Client Settings"
                            Call Fill_DetailsClientSettings()
                        Case "Clinic"
                            Call Fill_DetailsClinics()
                        Case "Backup"
                            Call Fill_DetailsBackups()
                        Case "Restore"
                            Call Fill_DetailsRestores()
                        Case "Self Notes"
                            Call Fill_DetailsSelfNotes()
                            'sarika 11th sept 07 
                        Case "Provider Type"
                            Call Fill_DetailsDoctorTypes()
                        Case "Provider"
                            Call Fill_DetailsProviders()
                        Case "Multiple Database"
                            Fill_DetailsMultipleDB()
                            '----------------------------------------
                    End Select
                ElseIf enmUserOperation = enmOperation.Audit Then
                    If Trim(trvAudit.SelectedNode.Text) = "Audit" Then
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    '-----------------------------------------------------
                    'sarika 25th apr 2007
                    'Select Case Trim(trvAudit.SelectedNode.Text)
                    '    Case "Report"
                    '        ' Call Fill_DetailsAuditReports()
                    '    Case "Archived Audit Report"
                    '        Call Fill_DetailsArchivedAuditReports()
                    'End Select

                    'ElseIf enmUserOperation = enmOperation.Database Then
                    '    If Trim(trvDatabase.SelectedNode.Text) = "Database" Then
                    '        Me.Cursor = Cursors.Default
                    '        Exit Sub
                    '    End If
                    '    Select Case Trim(trvDatabase.SelectedNode.Text)
                    '        Case "Database Update"
                    '            Call Fill_DetailsDBUpdations()
                    '    End Select
                    '-----------------------------------------------------
                ElseIf enmUserOperation = enmOperation.Tools Then
                    If Trim(trvTools.SelectedNode.Text) = "Tools" Then
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    Select Case Trim(trvTools.SelectedNode.Text)
                        Case "Client Message"
                            Call Fill_DetailsClientMessages()
                        Case "Online Updates"
                            Call Fill_DetailsOnlineUpdates()
                        Case "Suggestions to gloStream"
                            Call Fill_DetailsSuggestions()
                    End Select
                End If
                Me.Cursor = Cursors.Default

            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '******By Sandip Deshmukh 26 Oct 07 4.05PM 
    '******Function created to check wheather delete doctor type is not Senior/Junior
    Public Function chkforSRJRDoctor(ByVal strnodeval As String) As Boolean
        Dim strProviderType As String
        Dim blreturn As Boolean
        Dim _sqlstr As String = ""
        strProviderType = strnodeval
        Try

            _sqlstr = " SELECT  nProviderTypeID as drtype FROM ProviderType_MST WHERE sProviderType ='" & strProviderType & "'"

            Dim objCon As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
            Dim objCmd As New SqlCommand
            Dim objSQLDataReader As SqlDataReader
            objCmd.CommandText = _sqlstr
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader
            If objSQLDataReader.HasRows = True Then
                objSQLDataReader.Read()
                If objSQLDataReader.Item("drtype") = 0 Or objSQLDataReader.Item("drtype") = 1 Then
                    blreturn = True
                Else
                    blreturn = False
                End If
            End If
            objSQLDataReader.Close()
            objCon.Close()
            objCon = Nothing
            Return blreturn
        Catch ex As Exception
            Return False
        End Try
        '******26 Oct 07 4.05PM 
    End Function


    Public Sub ShowAdministrator()
        ''pnlAuditLogSearch.Visible = False

        Me.Cursor = Cursors.WaitCursor
        enmUserOperation = enmOperation.Admin
        lblAuditCategory.Visible = False
        cmbAuditCategory.Visible = False
        pnlAudit.Visible = False
        pnlMainMainTop.Visible = False
        'picMainSepMain.Visible = False
        optSelfNotesCategory.Visible = False
        optSelfNotesStatus.Visible = False
        pnlCommandButtons.Visible = False
        tsbtnCMSSettingsNew.Visible = False 'New by hemant
        btnCloneProvider.Visible = False

        If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then

            Select Case Trim(trvAdminMenu.SelectedNode.Text)
                Case "Administrator"
                    '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topadministrator.JPG")
                    lblMainTop.Text = "Administrator"
                Case "DB Management"
                    '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdatabasemangement.JPG")
                    lblMainTop.Text = "Database Management"
                Case "Tools"
                    '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toptools.JPG")
                    lblMainTop.Text = "Tools"
            End Select
            dgData.Visible = False
            SplitterMainCategory.Visible = False
            trvCategory.Visible = False
            'picMainSepMain.Visible = False
            pnlMainMainTop.Visible = False
            'picMainSepTop.Visible = False

            pnl_tlsp_Top.Visible = False

            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        'picMainSepTop.Visible = True
        trvCategory.Visible = True
        SplitterMainCategory.Visible = True
        dgData.Visible = True
        pnl_tlsp_Top.Visible = True
        Select Case Trim(trvAdminMenu.SelectedNode.Text)
            Case "Windows Groups & Users"
                '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topwindowsgroupsanduser.JPG")
                lblMainTop.Text = "Windows Groups & Users"
                Call Fill_CategoryWindowsGroupsUsers()
                If trvCategory.Nodes(0).Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0).Nodes(0)
                    Call Fill_DetailsWindowsGroupsUsers()
                Else
                    dgData.DataSource = Nothing
                End If
                '------------------
                'Sarika 21st April 2007
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Windows Groups & Users data viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------
            Case "QEMR Groups"
                '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topgloemrgroup.JPG")
                'sarika 26th june 07
                lblMainTop.Text = "QEMR Groups"
                '---
                Call Fill_CategorygloEMRGroups()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsgloEMRGroups()
                Else
                    dgData.DataSource = Nothing
                End If
                '------------------
                'Sarika 21st April 2007
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The QEMR Groups data viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------
                'Added for User groups
                'Sandip Darade 7th Feb 09
            Case "User Groups"

                lblMainTop.Text = "User Groups"
                '---
                Call Fill_CategoryUserGroups()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsUserGroups()
                Else
                    dgData.DataSource = Nothing
                End If
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The user groups data viewed.  ", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------
            Case "Multiple Database"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    'btnRefresh.Image = Global.gloPMAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    'btnRefresh.Image = Global.gloPMAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                lblMainTop.Text = "Multiple Database"
                Call Fill_MultipleDB()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsMultipleDB()
                Else
                    dgData.DataSource = Nothing
                End If
            Case "User Management"
                '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topusermanagement.JPG")
                'sarika 26th june 07
                lblMainTop.Text = "User Management"
                '---
                Call Fill_CategorygloEMRUsers()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsgloEMRUsers()
                Else
                    dgData.DataSource = Nothing
                End If
                '------------------
                'Sarika 21st April 2007
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Users List viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------
            Case "Provider"
                '     picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdoctors.gif")
                'sarika 26th june 07
                'Change by Rahul Patel on 10/09/2010 i.e Changing text from "Provider" to "Providers"
                btnCloneProvider.Visible = True
                lblMainTop.Text = "Providers"
                btnNew.Visible = True
                btnModify.Visible = True
                btnDelete.Visible = True
                '---
                Call Fill_CategoryProviders()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsProviders()
                Else
                    dgData.DataSource = Nothing
                End If
                '------------------
                'Sarika 21st April 2007
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Doctor list viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------
            Case "Provider-User Task Assignment"
                '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdoctors.gif")
                'sarika 26th june 07
                ' lblMainTop.Text = "Doctors"

                '---
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                'picMainSepMain.Visible = False
                pnlMainMainTop.Visible = False
                '******
                pnl_tlsp_Top.Visible = False
                Dim frmConfiguration As New frmProviderUserTaskConfiguration
                frmConfiguration.ShowDialog()
                '----------------------------------------
                'sarika 30th apr 2007
            Case "Junior-Senior Provider Association"
                '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdoctors.gif")
                'sarika 26th june 07
                lblMainTop.Text = "Providers"
                '---
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                '******
                pnl_tlsp_Top.Visible = False
                Dim objfrmJrSrAssociation As New frmJrSrAssociation
                objfrmJrSrAssociation.ShowDialog()

                '------------------
                'Sarika 7 May 2007
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Junior-Senior Doctor Association data viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------
                '----------------------------------------
                '******By Sandip Deshmukh 13 Oct 07 5.20PM Bug# 334
                '******Changed the case name for the bug 
                '******previos was Doctor-Referral
            Case "Provider-Referral Letter"
                '******13 Oct 07 5.20PM Bug# 334
                '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdoctors.gif")
                'sarika 26th june 07
                lblMainTop.Text = "Provider"
                '---
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                'picMainSepMain.Visible = False
                pnlMainMainTop.Visible = False
                '******
                pnl_tlsp_Top.Visible = False

                Dim frmConfiguration As New frmProviderReferralSettings
                frmConfiguration.ShowDialog()
            Case "Client Settings"
                '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topclientsettings.gif")
                'sarika 26th june 07
                lblMainTop.Text = "Client Settings"
                '---
                Call Fill_CategoryClientMachines()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsClientSettings()
                Else
                    dgData.DataSource = Nothing
                End If
                'dgData.Visible = False
                'SplitterMainCategory.Visible = False

                'picMainSepMain.Visible = False
                pnlMainMainTop.Visible = False
                '******
                'pnl_tlsp_Top.Visible = False
                '------------------
                'Sarika 21st April 2007
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Client Settings viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------

            Case "Client Update Details"
                '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topclientsettings.gif")
                'sarika 26th june 07
                lblMainTop.Text = "Client Update Details"
                '---
                'Call Fill_CategoryClientMachines()
                'If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                'trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                'Call Fill_DetailsClientSettings()
                'Else
                'dgData.DataSource = Nothing
                'End If
                'dgData.Visible = False
                'SplitterMainCategory.Visible = False

                'picMainSepMain.Visible = False
                pnlCommandButtons.Visible = False
                pnl_tlsp_Top.Visible = False
                pnlMainMainTop.Visible = True
                Panel8.Visible = False
                trvCategory.Visible = False
                pnlClientUpdateDetailsFilter.Visible = True
                '******
                'pnl_tlsp_Top.Visible = False
                '------------------

                Call Fill_ClientMachineDropDown()
                Call Fill_ClientUpdateDetails()

                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Client Update Details Settings viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
            Case "Clinic"
                '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topclinic.JPG")
                'sarika 26th june 07

                lblMainTop.Text = "Clinic"
                pnlCommandButtons.Visible = True
                btnNew.Visible = False
                btnNew.Text = "&New"
                btnModify.Visible = True
                btnModify.Text = "&Modify"
                btnDelete.Visible = False
                btnDelete.Text = "&Delete"
                btnDelete.ToolTipText = "Delete"

                '---
                Call Fill_CategoryClinics()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsClinics()
                Else
                    dgData.DataSource = Nothing
                End If
                '------------------
                'Sarika 21st April 2007
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Clinic data viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------

                'sarika 29th june 07
            Case "Clinic Workflow Settings"
                '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topclinic.JPG")
                'sarika 26th june 07
                lblMainTop.Text = "Clinic Workflow Settings"
                '---
                'Call Fill_CategoryClinics()
                'If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                '    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                '    Call Fill_DetailsClinics()
                'Else
                '    dgData.DataSource = Nothing
                'End If


                '******
                '******
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                'picMainSepMain.Visible = False
                pnlMainMainTop.Visible = False
                '******
                pnl_tlsp_Top.Visible = False

                Dim frm As New frmLocationStatus
                frm.ShowDialog()

                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Clinic Workflow Settings viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '----------------------
            Case "Backup"
                '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topbackup.JPG")
                'sarika 26th june 07
                lblMainTop.Text = "Backup"
                '---
                Call Fill_CategoryBackups()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsBackups()
                Else
                    dgData.DataSource = Nothing
                End If
            Case "Restore"
                '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toprestore.JPG")
                'sarika 26th june 07
                lblMainTop.Text = "Audit"
                '---
                Call Fill_CategoryRestores()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsRestores()
                Else
                    dgData.DataSource = Nothing
                End If
            Case "Self Notes"
                '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topselfnotes.JPG")
                'sarika 26th june 07
                lblMainTop.Text = "Self Notes"
                '---
                Call Fill_CategorySelfNotes()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsSelfNotes()
                Else
                    dgData.DataSource = Nothing
                End If
            Case "Login Users"
                pnlCommandButtons.Visible = True
                btnDelete.Visible = False
                btnModify.Visible = False
                btnNew.Visible = False
                '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toploginuserstrip.gif")
                'sarika 26th june 07
                lblMainTop.Text = "Login Users"
                '---
                trvCategory.Visible = False
                Call Fill_DetailsLoginUsers()
                '------------------
                'Sarika 21st April 2007
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Login Users list viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------
            Case "Clearinghouse"
                pnlCommandButtons.Visible = True
                btnNew.Visible = True
                btnNew.Text = "&New"
                btnModify.Visible = True
                btnModify.Text = "&Modify"
                btnDelete.Visible = True
                btnDelete.Text = "&Delete"
                btnDelete.ToolTipText = "Delete"

                lblMainTop.Text = "Clearinghouse"
                '---
                trvCategory.Visible = False
                Call Fill_ClearingHouse()
                '------------------
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "Clearinghouse viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                ''Sandip Darade  20090708
                ''Import Fee Shedule
            Case "Import Fee Schedule"
                lblMainTop.Text = "Import Fee Schedule"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False

                Dim frm As New frmImportFeeSchedule()
                frm.ShowDialog()
                frm.Dispose()

                ''As Text changed according to phill sir,So change made case
            Case "Calculate/Recalculate Follow-up"
                lblMainTop.Text = "Calculate/Recalculate Follow-up"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False

                Dim objSettings As New clsSettings
                Dim followupValue As New Object
                objSettings.GetSetting("CALCULATEFOLLOWUP", gnLoginID, _ClinicID, followupValue)
                ''7022Itens: Claim queue reset utility
                ''commented old code.
                'If Not IsNothing(value) And value.ToString() <> "" Then
                '    If (Convert.ToBoolean(value.ToString().Trim) = True) Then
                '        Dim resYesNo As DialogResult = MessageBox.Show("Accounts and Claims Follow-up Dates and Actions are allready set." + Environment.NewLine + "All Patient Accounts and Claims will be reviewed by the system to calculate the starting Follow-up Dates and Actions." + Environment.NewLine + "This may take awhile." + Environment.NewLine + "Continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                '        If resYesNo = Windows.Forms.DialogResult.Yes Then
                '            Dim AutoFollowupUtility As frmAutoFollowupUtility = New frmAutoFollowupUtility()
                '            AutoFollowupUtility.ShowDialog()
                '        End If
                '    Else
                '        Dim res As DialogResult = MessageBox.Show("All Patient Accounts and Claims will be reviewed by the system to calculate the starting Follow-up Dates and Actions." + Environment.NewLine + "This may take awhile." + Environment.NewLine + "Continue?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                '        If res = Windows.Forms.DialogResult.OK Then
                '            Dim AutoFollowupUtility As frmAutoFollowupUtility = New frmAutoFollowupUtility()
                '            AutoFollowupUtility.ShowDialog()
                '        End If
                '    End If
                'Else
                '    Dim res As DialogResult = MessageBox.Show("All Patient Accounts and Claims will be reviewed by the system to calculate the starting Follow-up Dates and Actions." + Environment.NewLine + "This may take awhile." + Environment.NewLine + "Continue?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                '    If res = Windows.Forms.DialogResult.OK Then
                '        Dim AutoFollowupUtility As frmAutoFollowupUtility = New frmAutoFollowupUtility()
                '        AutoFollowupUtility.ShowDialog()
                '    End If
                'End If
                'value = Nothing

                ''7022Itens: Claim queue reset utility
                ''add new code to gives user choice to recalcuate follow-up
                ''MessageID: 0 user wants to calculate follow up first time.
                ''MessageID: 1 user wants to recalculate follow up.
                Dim frm As New frmFollowupCalculator()
                Try
                    If Not IsNothing(followupValue) And followupValue.ToString() <> "" Then
                        If (Convert.ToBoolean(followupValue.ToString().Trim) = True) Then
                            frm.MessageID = 1
                            frm.ShowDialog()
                        Else
                            frm.MessageID = 0
                            frm.ShowDialog()
                        End If
                    Else
                        frm.MessageID = 0
                        frm.ShowDialog()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    If Not IsNothing(frm) Then
                        frm.Dispose()
                    End If
                Finally
                    If Not IsNothing(frm) Then
                        frm.Dispose()
                    End If
                    followupValue = Nothing
                End Try

            Case "Setup Business Center Rules"
                lblMainTop.Text = "Setup Business Center Rules"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False

                Dim BusinessCenterRulesSetup As frmBusinessCenterRulesSetup = New frmBusinessCenterRulesSetup()
                BusinessCenterRulesSetup.ShowDialog()


            Case "Account Business Center Utility"
                lblMainTop.Text = "Account Business Center Utility"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False
                Dim res As DialogResult = MessageBox.Show("The actions taken by this utility cannot be reversed by the utility." + Environment.NewLine + "If accounts are assigned to the wrong business center, they can be reassigned manually." + Environment.NewLine + "Please review the admin business center rules list for accuracy and completeness BEFORE running this utility.", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If res = Windows.Forms.DialogResult.OK Then
                    Dim AccBusCenterUtility As frmAccBusCenterUtility = New frmAccBusCenterUtility()
                    AccBusCenterUtility.ShowDialog()
                End If

            Case "Workers Comp Billing Utility"
                lblMainTop.Text = "Workers Comp Billing Utility"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                Dim res As DialogResult = MessageBox.Show("The actions taken by this utility cannot be reversed by the utility." + Environment.NewLine + "This utility will set all worker comp insurances patient relationship  to employee  and mark patient insurance subscriber as company.", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                pnlMainMainTop.Visible = False
                If res = Windows.Forms.DialogResult.OK Then
                    EDIWorkerCompUtility()
                End If

            Case "Settings"
                ' picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topsetting.gif")
                'sarika 26th june 07
                lblMainTop.Text = "Settings"
                '---
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                'picMainSepMain.Visible = False
                pnlMainMainTop.Visible = False
                'picMainSepTop.Visible = False
                '  Dim frm As New frmSettings
                'changed the form with new GUI 'sarika 21st sept 07'
                Dim objsetting As New clsSettings
                Dim frm As New frmSettings_New
                '-------
                frm.ShowDialog()
                'If (frm.DialogResult = Windows.Forms.DialogResult.OK) Then
                objsetting.GetSettings()
                If Convert.ToString(objsetting.UB04_EnableBilling) = "False" Or Convert.ToString(objsetting.UB04_EnableBilling) = "" Then
                    tsbtnUB04Setting.Visible = False
                Else
                    tsbtnUB04Setting.Visible = True
                End If
                Call Fill_Admin()
                trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(1).Nodes(1)
                'End If
                'Case "Voice Training Document"
                '    ' picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topvoicetrainingdoc.gif")
                '    'sarika 26th june 07
                '    lblMainTop.Text = "Voice Training Document"
                '    '---
                '    dgData.Visible = False
                '    SplitterMainCategory.Visible = False
                '    trvCategory.Visible = False
                '    'picMainSepMain.Visible = False
                '    pnlMainMainTop.Visible = False
                '    'picMainSepTop.Visible = False
                '    Dim frm As New frmVoiceTrainingDocument
                '    frm.ShowDialog()
                ''//Code added by ravikiran on 10/02/2007
                '' For RxReport Designer tool

            Case "SSRS Report Settings"
                lblMainTop.Text = "SSRS Report Settings"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                Dim frm As New Project_Reportview.frmArrangeReport("gloPM", gstrSQLServerName, gstrDatabaseName, Not (gblnWindowsAuthentication), gstrSQLUser, gstrSQLPassword)
                frm.ShowDialog()

            Case "Deploy SSRS Reports"
                lblMainTop.Text = "Deploy SSRS Reports"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                Dim frm As New SSRSApplication.frm_DeployReports("gloPM", gstrSQLServerName, gstrDatabaseName, Not (gblnWindowsAuthentication), gstrSQLUser, gstrSQLPassword)
                frm.ShowDialog()

            Case "RxReport Designer"
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                'picMainSepMain.Visible = False
                'sarika 26th june 07
                lblMainTop.Text = "Rx Report Designer"
                '---
                pnlMainMainTop.Visible = False
                'picMainSepTop.Visible = False


                'sarika 1st nov 07
                'chk for the pre-requisites
                'chk for clinic record if no dodnt show the Report Designer
                'if yes then chk for Clinic logo -- if not prompt the user to select a clinic logo
                Dim objClinic As New clsClinic
                Dim col As New Collection
                col = objClinic.PopulateClinic()

                If col.Count < 0 Then
                    MessageBox.Show("You must enter Clinic information to print the Rx Report Designer", "RxReport Designer", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                col = Nothing
                'chk for clinic logo

                'chk for provider record if no dodnt show the Report Designer
                'if yes then chk for provider sign -- if not prompt the user to select or capture a signature
                Dim objProvider As New clsProvider

                col = objProvider.Fill_Providers()

                If col.Count < 0 Then
                    MessageBox.Show("You must enter Provider information to print the Rx Report Designer", "RxReport Designer", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                col = Nothing

                'chk for provider signature

                Dim objRxRptDict As New ClsRxReportDictionary
                Dim dt As DataTable
                dt = New DataTable
                dt = objRxRptDict.GetClinicLogo()
                If Not IsNothing(dt) Then
                    If IsDBNull(dt.Rows(0)("ClinicLogo")) Then
                        MessageBox.Show("You must select Clinic logo to print the Rx Report Designer", "RxReport Designer", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
                dt = Nothing

                dt = New DataTable
                dt = objRxRptDict.GetProviderSign()
                If Not IsNothing(dt) Then
                    If IsDBNull(dt.Rows(0)("ProviderSignature")) Then
                        MessageBox.Show("You must select imgSignature to print the Rx Report Designer", "RxReport Designer", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
                dt = Nothing
                '-----------------------

                Dim frm As New frmRxReportDesigner
                frm.ShowDialog()
                ''// Updation ends


                'sarika 11th sept 07
            Case "Provider Type"
                pnlCommandButtons.Visible = True
                lblMainTop.Text = "Provider Type"
                btnDelete.Text = "&Delete"
                btnDelete.ToolTipText = "Delete"
                btnDelete.Visible = True
                btnModify.Visible = True
                btnNew.Visible = True

                Call Fill_DoctorType()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsDoctorTypes()
                Else
                    dgData.DataSource = Nothing
                End If
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The provider type information viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing

                ''Claim Validation settings 
            Case "Claim Validation Settings"
                lblMainTop.Text = "Claim Validation Settings"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False

                Dim frm As New frmEDISettings(gstrConnectionString)
                frm.ShowDialog()
            Case "Update PayerID"
                lblMainTop.Text = "Update PayerID"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False

                Dim frm As New frmUpdatePayerID(gstrConnectionString)
                frm.ShowDialog()
                frm.Dispose()

                ''Sandip Darade 20091209
            Case "CMS1500 08/05 Settings"
                lblMainTop.Text = "CMS1500 08/05 Settings"
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False

                pnl_tlsp_Top.Visible = False

                Dim oFrm As New frmCMSPrinterSettings
                oFrm.ShowDialog()
                oFrm.Dispose()
                oFrm = Nothing
                ''Sameer Shukla 12022013
            Case "CMS1500 02/12 Settings"
                'Hemant modified below
                pnlCommandButtons.Visible = True
                tsbtnCMSSettingsNew.Visible = True
                btnNew.Visible = True
                btnNew.Text = "&New"
                btnModify.Visible = True
                btnModify.Text = "&Modify"
                btnDelete.Visible = True
                btnDelete.Text = "&Delete"
                btnDelete.ToolTipText = "Delete"
                trvCategory.Visible = False
                lblMainTop.Text = "Printer Settings"
                Call Fill_Printers()
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "Printer Settings viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                'Hemant commented below

                'lblMainTop.Text = "CMS1500 02/12 Settings"
                'dgData.Visible = False
                'SplitterMainCategory.Visible = False
                'trvCategory.Visible = False

                'pnlMainMainTop.Visible = False

                'pnl_tlsp_Top.Visible = False

                'Dim oFrm As New frmCMSPrinterSettingsNew
                'oFrm.ShowDialog()
                'oFrm.Dispose()
                'oFrm = Nothing
                ' ''vijay patil 20100820
            Case "UB04 Settings"
                lblMainTop.Text = "UB04 Settings"
                pnlCommandButtons.Visible = True
                tsbtnCMSSettingsNew.Visible = True
                btnNew.Visible = True
                btnNew.Text = "&New"
                btnModify.Visible = True
                btnModify.Text = "&Modify"
                btnDelete.Visible = True
                btnDelete.Text = "&Delete"
                btnDelete.ToolTipText = "Delete"
                trvCategory.Visible = False
                Call Fill_Printers(True)
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "Printer Settings viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '    'pradeep godse 20101706 for prefix
            Case "Site Prefix"
                lblMainTop.Text = "Site Prefix"
                Call Fill_Prefix()

            Case "Merge Insurance Plan"
                lblMainTop.Text = "Merge Insurance Plan"
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                pnl_tlsp_Top.Visible = False

                Dim oFrm As New gloContactsMerge.frmContactsMerge(gstrConnectionString)
                oFrm.ShowDialog()
                oFrm.Dispose()
                oFrm = Nothing
                'Case "Service Configuration"
                '    lblMainTop.Text = "Service Configuration"
                '    dgData.Visible = False
                '    SplitterMainCategory.Visible = False
                '    trvCategory.Visible = False
                '    pnlMainMainTop.Visible = False
                '    pnl_tlsp_Top.Visible = False
                '    Dim oFrm As New frmGloService
                '    oFrm.ShowDialog()
                '    oFrm.Dispose()
                '    oFrm = Nothing

                ''7022Itens: $0.00 claim queue bug
                ''Case for $0.00 claim queue bug added
                ''As Text changed according to phill sir,So change made case
            Case "Repair $0.00 claim Follow-up"
                lblMainTop.Text = "Repair $0.00 claim Follow-up"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                ''Text in message box changed according to phill sir
                Dim resYesNo As DialogResult = MessageBox.Show("This utility will remove all $0.00 balance claims from the Follow-up queue. Continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                If resYesNo = Windows.Forms.DialogResult.Yes Then
                    ProcessSteps()
                End If

            Case "Advance Merge Insurance"
                lblMainTop.Text = "Advance Merge Insurance"
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                pnl_tlsp_Top.Visible = False

                Dim oFrm As New gloContactsMerge.frmMergeContacts(gstrConnectionString, True)
                If oFrm.IsInsurancePlanPresent = False Then
                    MessageBox.Show(String.Format("Advance insurance merge is performed on insurance plan.{0}No insurance plan is available in the database.", Environment.NewLine), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    oFrm.ShowDialog(Me)
                End If
                oFrm.Dispose()
                oFrm = Nothing

        End Select
        Me.Cursor = Cursors.Default
    End Sub
    ''7022Itens: $0.00 claim queue bug
    ''fuction to delete $0.00 claim 
    Private Function ProcessSteps() As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            oDB.Execute("gsp_RemoveZeroBalanceAccount", oDBParameters)
            ''Text in message box changed according to phill sir
            MessageBox.Show("$0.00 balance claims have been removed from the Follow-up Queue.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch DBErr As gloDatabaseLayer.DBException
            MessageBox.Show(DBErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Function



    Public Sub ShowAudits()
        '' pnlAuditLogSearch.Visible = False

        enmUserOperation = enmOperation.Audit
        pnlAudit.Visible = False
        pnlCommandButtons.Visible = False
        'btnNew.Visible = True
        'btnNew.Text = "New"
        'btnModify.Visible = True
        'btnModify.Text = "Modify"
        'btnDelete.Visible = True
        'btnDelete.Text = "Delete"

        pnlMainMainTop.Visible = True
        'picMainSepMain.Visible = True
        optSelfNotesCategory.Visible = False
        optSelfNotesStatus.Visible = False
        With dtFrom
            .Format = DateTimePickerFormat.Custom
            .CustomFormat = DTFORMAT
            .Value = Date.Now
        End With
        With dtTo
            .Format = DateTimePickerFormat.Custom
            .CustomFormat = DTFORMAT
            .Value = Date.Now
        End With

        If trvAudit.SelectedNode Is trvAudit.Nodes(0) Then
            '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topaudit.JPG")
            'sarika 26th june 07
            lblMainTop.Text = "Audit"
            '---
            dgData.Visible = False
            SplitterMainCategory.Visible = False
            trvCategory.Visible = False
            'picMainSepMain.Visible = False
            pnlMainMainTop.Visible = False
            'picMainSepTop.Visible = False

            pnl_tlsp_Top.Visible = False

            Exit Sub
        End If

        If trvAudit.SelectedNode Is trvAudit.Nodes(1) Then
            ' picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topaudit.JPG")
            'sarika 26th june 07
            lblMainTop.Text = "Audit"
            '---
            dgData.Visible = False
            SplitterMainCategory.Visible = False
            trvCategory.Visible = False
            'picMainSepMain.Visible = False
            pnlMainMainTop.Visible = False
            'picMainSepTop.Visible = False

            pnl_tlsp_Top.Visible = False

            Exit Sub
        End If

        'picMainSepTop.Visible = True
        trvCategory.Visible = True
        SplitterMainCategory.Visible = True
        dgData.Visible = True

        Select Case Trim(trvAudit.SelectedNode.Text)
            Case "Report"
                _blnSearch = True
                '' pnlAuditLogSearch.Visible = True
                pnl_tlsp_Top.Visible = True
                pnlCommandButtons.Visible = True
                btnNew.Visible = False
                btnModify.Visible = False
                btnDelete.Visible = True
                btnDelete.Text = "Export"
                btnDelete.ToolTipText = "Export"
                btnRefresh.Visible = True
                cmbSearchPatient.SelectedIndex = 0
                lblAuditCategory.Visible = True
                cmbAuditCategory.Visible = True
                pnlAudit.Visible = False
                ' btnShowAudit.Visible = True
                cmbSearchPatient.Visible = True
                lblPatientID.Visible = False
                ' picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topauditreport.JPG")
                'sarika 26th june 07
                lblMainTop.Text = "Report"
                '---
                Call Fill_CategoryUsers()
                Call Fill_AuditCategories()
                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsAuditReports()
                Else
                    dgData.DataSource = Nothing
                End If
                ' pnlAuditLogSearch.Visible = False

            Case "Archive"
                'picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topDatabasearchive.JPG")
                'dgData.Visible = False
                'SplitterMainCategory.Visible = False
                'trvCategory.Visible = False
                'picMainSepMain.Visible = False
                'pnlMainMainTop.Visible = False
                'picMainSepTop.Visible = False
                'Dim frmArchive As New frmArchiveAudit
                'frmArchive.ShowDialog()

                'sarika 24th apr 2007
                Dim strarchiveconn As String = GetArchiveConnectionString()

                If strarchiveconn <> "" Then
                    ' picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topDatabasearchive.JPG")
                    'sarika 26th june 07
                    lblMainTop.Text = "Archive"
                    '---
                    dgData.Visible = False
                    SplitterMainCategory.Visible = False
                    trvCategory.Visible = False
                    ' picMainSepMain.Visible = False
                    pnlMainMainTop.Visible = False
                    'picMainSepTop.Visible = False
                    Dim frmArchive As New frmArchiveAudit
                    frmArchive.ShowDialog()
                End If
            Case "Archived Audit Report"
                'pnlCommandButtons.Visible = True
                'btnNew.Visible = False
                'btnModify.Visible = False
                'btnDelete.Visible = False
                'btnRefresh.Visible = True
                'cmbSearchPatient.Visible = False
                'lblPatientID.Visible = True
                'cmbSearchPatient.SelectedIndex = 0
                'lblAuditCategory.Visible = True
                'cmbAuditCategory.Visible = True
                'pnlAudit.Visible = True
                'picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toparchivedauditreport.gif")
                'Call Fill_CategoryArchivedUsers()
                'Call Fill_ArchivedAuditCategories()
                'If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                '    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                '    Call Fill_DetailsArchivedAuditReports()
                'Else
                '    dgData.DataSource = Nothing
                'End If

                '---------------------------------------------------------------
                'sarika 24th apr 2007
                Dim strarchiveconn As String = GetArchiveConnectionString()

                If strarchiveconn <> "" Then
                    pnlCommandButtons.Visible = True
                    btnNew.Visible = False
                    btnModify.Visible = False
                    btnDelete.Visible = False
                    btnRefresh.Visible = True
                    'cmbSearchPatient.Visible = True
                    lblPatientID.Visible = True
                    cmbSearchPatient.SelectedIndex = 0
                    cmbSearchPatient.Visible = False
                    lblAuditCategory.Visible = True
                    cmbAuditCategory.Visible = True
                    pnlAudit.Visible = False
                    ' picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toparchivedauditreport.gif")
                    'sarika 26th june 07
                    lblMainTop.Text = "Report"
                    '---
                    Call Fill_CategoryArchivedUsers()
                    Call Fill_ArchivedAuditCategories()
                    If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                        trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                        Call Fill_DetailsArchivedAuditReports()
                    Else
                        dgData.DataSource = Nothing
                    End If
                End If
                '---------------------------------------------------------------

            Case "Restore Archive"
                'picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topDatabasearchive.JPG")
                'dgData.Visible = False
                'SplitterMainCategory.Visible = False
                'trvCategory.Visible = False
                'picMainSepMain.Visible = False
                'pnlMainMainTop.Visible = False
                'picMainSepTop.Visible = False
                'Dim frmArchive As New frmUnArchive
                'frmArchive.ShowDialog()

                '---------------------------------------------------------------
                'sarika 24th apr 2007
                Dim strarchiveconn As String = GetArchiveConnectionString()

                If strarchiveconn <> "" Then
                    '    picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topDatabasearchive.JPG")
                    'sarika 26th june 07
                    lblMainTop.Text = "Restore Archive"
                    '---
                    dgData.Visible = False
                    SplitterMainCategory.Visible = False
                    trvCategory.Visible = False
                    'picMainSepMain.Visible = False
                    pnlMainMainTop.Visible = False
                    'picMainSepTop.Visible = False
                    Dim frmArchive As New frmUnArchive
                    frmArchive.ShowDialog()
                    ' frmArchive.MdiParent = Me
                    'frmArchive.Show()

                End If
                '---------------------------------------------------------------
        End Select

        '     pnlAuditLogSearch.Visible = False

    End Sub

    'Public Sub showDBMGMT()
    '    enmUserOperation = enmOperation.Database
    '    If Trim(trvDatabase.SelectedNode.Text) = "Database" Then
    '        picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdatabase.JPG")
    '        dgData.Visible = False
    '        SplitterMainCategory.Visible = False
    '        trvCategory.Visible = False
    '        picMainSepMain.Visible = False
    '        pnlMainMainTop.Visible = False
    '        picMainSepTop.Visible = False
    '        Exit Sub
    '    End If
    '    picMainSepTop.Visible = True
    '    trvCategory.Visible = True
    '    SplitterMainCategory.Visible = True
    '    dgData.Visible = True

    '    Select Case Trim(trvDatabase.SelectedNode.Text)
    '        Case "Database Tool"
    '            Dim frmDatabaseTool As New frmDBTool
    '            frmDatabaseTool.ShowDialog()
    '        Case "Database Update"
    '            picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topclientsettings.gif")
    '            Dim frmDB As New frmDatabaseUpdate
    '            frmDB.ShowDialog()
    '    End Select
    'End Sub

    Public Sub showDBTool()
        ''pnlAuditLogSearch.Visible = False

        enmUserOperation = enmOperation.Tools

        lblAuditCategory.Visible = False
        cmbAuditCategory.Visible = False
        pnlAudit.Visible = False
        optSelfNotesCategory.Visible = False
        optSelfNotesStatus.Visible = False

        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "New"
        btnModify.Visible = True
        btnModify.Text = "Modify"
        btnDelete.Visible = True
        btnDelete.Text = "Delete"
        btnDelete.ToolTipText = "Delete"

        pnlMainMainTop.Visible = True
        'picMainSepMain.Visible = True
        With dtFrom
            .Format = DateTimePickerFormat.Custom
            .CustomFormat = DTFORMAT
            .Value = Date.Now
        End With
        With dtTo
            .Format = DateTimePickerFormat.Custom
            .CustomFormat = DTFORMAT
            .Value = Date.Now
        End With

        If Trim(trvTools.SelectedNode.Text) = "Tools" Then
            '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toptools.JPG")
            'sarika 26th june 07
            lblMainTop.Text = "Tools"
            '---
            dgData.Visible = False
            SplitterMainCategory.Visible = False
            trvCategory.Visible = False
            'picMainSepMain.Visible = False
            pnlMainMainTop.Visible = False
            'picMainSepTop.Visible = False
            Exit Sub
        End If
        'picMainSepTop.Visible = True
        trvCategory.Visible = True
        SplitterMainCategory.Visible = True
        dgData.Visible = True

        Select Case Trim(trvTools.SelectedNode.Text)
            Case "Client Message"
                '    picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topclientmessage.JPG")
                'sarika 26th june 07
                lblMainTop.Text = "Client Messages"
                '---
                Call Fill_CategoryClientMessage()
                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsClientMessages()
                Else
                    dgData.DataSource = Nothing
                End If
            Case "Suggestions to gloStream"
                '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topsuggestionstogloStream.JPG")
                'sarika 26th june 07
                lblMainTop.Text = "Suggestions to gloStream"
                '---
                Call Fill_CategorySuggestions()
                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsSuggestions()
                Else
                    dgData.DataSource = Nothing
                End If
            Case "Online Updates"
                '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toponlineupdates.JPG")
                'sarika 26th june 07
                lblMainTop.Text = "Online Updates"
                '---
                Call Fill_CategoryOnlineUpdates()
                If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsOnlineUpdates()
                Else
                    dgData.DataSource = Nothing
                End If
        End Select

    End Sub

    Private Sub GetCheckedNodes(ByVal rootNode As TreeNode)
        If rootNode.Checked Then
            clRights.Add(rootNode.Text)
        End If
        For Each childNode As TreeNode In rootNode.Nodes
            GetCheckedNodes(childNode)
        Next
    End Sub

    Private Sub CheckUncheckChildNodes(ByVal rootNode As TreeNode, ByVal blnCheck As Boolean)
        For Each childNode As TreeNode In rootNode.Nodes
            childNode.Checked = blnCheck
            CheckUncheckChildNodes(childNode, blnCheck)
        Next
    End Sub

    Public Sub SearchNode(ByVal Trv As TreeView, ByVal strText As String)
        Dim trvNde As TreeNode
        For Each trvNde In Trv.Nodes
            SearchNode(trvNde, strText)
        Next
    End Sub

    Private Sub SearchNode(ByVal rootNode As TreeNode, ByVal strText As String)
        For Each childNode As TreeNode In rootNode.Nodes
            ' If LCase(Trim(childNode.Text)) = LCase(Trim(strText)) Then
            If childNode.Tag = Convert.ToInt64(strText) Then
                trvSearchNode = childNode
                Exit Sub
            End If
            SearchNode(childNode, strText)
        Next
    End Sub
    ''//Code commented by Ravikiran on 14/02/2007
    ''Private Function checkRxReportPath()
    ''    Dim objConn As New SqlConnection
    ''    Dim objcmd As New SqlCommand
    ''    Dim objReader As SqlDataReader
    ''    Try

    ''        Dim _strSQL As String = ""
    ''        objConn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
    ''        objcmd.Connection = objConn
    ''        If objConn.State = ConnectionState.Open Then
    ''            objConn.Close()
    ''        Else
    ''            objConn.Open()
    ''            _strSQL = "Select sSettingsValue from Settings where sSettingsName='RxReportPath'"
    ''            objcmd.CommandText = _strSQL
    ''            Dim RxPath As String
    ''            objReader = objcmd.ExecuteReader
    ''            If Not IsDBNull(objReader) Then
    ''                If objReader.HasRows Then
    ''                    objReader.Read()
    ''                    gstrRxReportpath = objReader("sSettingsValue")
    ''                End If
    ''            End If
    ''        End If

    ''    Catch ex As Exception
    ''        MessageBox.Show(ex.Message)
    ''    Finally
    ''        objReader.Close()
    ''        objConn.Close()
    ''    End Try
    ''End Function





    '----------------------------------------
    'sarika 25th apr 2007
    Private Sub btnShowAudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowAudit.Click
        'Call Fill_DetailsAuditReports()

        If dtFrom.Value.Date > dtTo.Value.Date Then
            MessageBox.Show("From date must be less than or equal to To date.", "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Select Case Trim(trvAudit.SelectedNode.Text)
            Case "Report"
                Call Fill_DetailsAuditReports()

                Try
                    Me.Cursor = Cursors.WaitCursor
                    Dim dvAuditLogReport As DataView
                    dvAuditLogReport = CType(dgData.DataSource(), DataView)

                    If IsNothing(dvAuditLogReport) Then
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If

                    dgData.DataSource = dvAuditLogReport
                    Dim strSearchDetails As String
                    If Trim(txtInstringSearch.Text) <> "" Then
                        strSearchDetails = Replace(txtInstringSearch.Text, "'", "''")
                        ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                        strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                        strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
                    Else
                        strSearchDetails = ""
                    End If

                    Select Case Trim(lblSearch.Text)
                        Case "Software Component"
                            dvAuditLogReport.RowFilter = " sSoftwareComponent Like '%" & strSearchDetails & "%'"
                            'Case "Activity Date"
                            '    dvAuditLogReport.RowFilter = " ActivityDate Like '%" & strSearchDetails & "%'"
                        Case "Category"
                            ' dvAuditLogReport.RowFilter = " sActivityCategory Like '%" & strSearchDetails & "%'"
                            dvAuditLogReport.RowFilter = " Category Like '%" & strSearchDetails & "%'"
                        Case "Machine"
                            dvAuditLogReport.RowFilter = " MachineName Like '%" & strSearchDetails & "%'"
                        Case "User"
                            dvAuditLogReport.RowFilter = " UserName Like '%" & strSearchDetails & "%'"
                        Case "Patient Code"
                            If strSearchDetails = "" Then
                                '  dvAuditLogReport.RowFilter = " PatientCode Like '%" & strSearchDetails & "%' or PatientCode=''"
                                Call Fill_DetailsAuditReports()

                            Else
                                dvAuditLogReport.RowFilter = " PatientCode Like '%" & strSearchDetails & "%'"

                            End If
                        Case "Patient"
                            If strSearchDetails = "" Then
                                '  dvAuditLogReport.RowFilter = " PatientCode Like '%" & strSearchDetails & "%' or PatientCode=''"
                                Call Fill_DetailsAuditReports()

                            Else
                                dvAuditLogReport.RowFilter = " PatientName Like '%" & strSearchDetails & "%'"
                            End If
                        Case "OutCome"
                            dvAuditLogReport.RowFilter = " sOutCome Like '%" & strSearchDetails & "%'"
                        Case "Description"
                            dvAuditLogReport.RowFilter = " CategoryDescription Like '%" & strSearchDetails & "%'"

                    End Select
                    Me.Cursor = Cursors.Default
                Catch objErr As Exception
                    Me.Cursor = Cursors.Default
                    MessageBox.Show(objErr.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            Case "Archived Audit Report"
                Call Fill_DetailsArchivedAuditReports()

                Try
                    Me.Cursor = Cursors.WaitCursor
                    Dim dvAuditLogReport As DataView
                    dvAuditLogReport = CType(dgData.DataSource(), DataView)

                    If IsNothing(dvAuditLogReport) Then
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If

                    dgData.DataSource = dvAuditLogReport
                    Dim strSearchDetails As String
                    If Trim(txtInstringSearch.Text) <> "" Then
                        strSearchDetails = Replace(txtInstringSearch.Text, "'", "''")
                        ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                        strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                        strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
                    Else
                        strSearchDetails = ""
                    End If

                    Select Case Trim(lblSearch.Text)
                        Case "Software Component"
                            dvAuditLogReport.RowFilter = " sSoftwareComponent Like '%" & strSearchDetails & "%'"
                            'Case "Activity Date"
                            '    dvAuditLogReport.RowFilter = " ActivityDate Like '%" & strSearchDetails & "%'"
                        Case "Category"
                            ' dvAuditLogReport.RowFilter = " sActivityCategory Like '%" & strSearchDetails & "%'"
                            dvAuditLogReport.RowFilter = " Category Like '%" & strSearchDetails & "%'"
                        Case "Machine"
                            dvAuditLogReport.RowFilter = " MachineName Like '%" & strSearchDetails & "%'"
                        Case "User"
                            dvAuditLogReport.RowFilter = " UserName Like '%" & strSearchDetails & "%'"
                        Case "Patient Code"
                            If strSearchDetails = "" Then
                                '  dvAuditLogReport.RowFilter = " PatientCode Like '%" & strSearchDetails & "%' or PatientCode=''"
                                Call Fill_DetailsAuditReports()

                            Else
                                dvAuditLogReport.RowFilter = " PatientCode Like '%" & strSearchDetails & "%'"

                            End If
                        Case "Patient"
                            If strSearchDetails = "" Then
                                '  dvAuditLogReport.RowFilter = " PatientCode Like '%" & strSearchDetails & "%' or PatientCode=''"
                                Call Fill_DetailsAuditReports()

                            Else
                                dvAuditLogReport.RowFilter = " PatientName Like '%" & strSearchDetails & "%'"
                            End If
                        Case "OutCome"
                            dvAuditLogReport.RowFilter = " sOutCome Like '%" & strSearchDetails & "%'"
                        Case "Description"
                            dvAuditLogReport.RowFilter = " CategoryDescription Like '%" & strSearchDetails & "%'"

                    End Select
                    Me.Cursor = Cursors.Default
                Catch objErr As Exception
                    Me.Cursor = Cursors.Default
                    MessageBox.Show(objErr.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try


        End Select
    End Sub
    '----------------------------------------

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmJrSrAssociation

        frm.ShowDialog()
    End Sub



    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        Try
            Select Case Trim(e.ClickedItem.Tag)
                Case "WindowsGroupsUsers"
                    'Windows Groups & Users
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(0)
                    Call ShowAdministrator()
                Case "gloEMRGroups"
                    'QEMR Groups
                    'trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(1)
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(0)

                    Call ShowAdministrator()
                Case "UserMGNT"
                    'gloEMR User
                    'trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(2)
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(1)

                    Call ShowAdministrator()
                Case "Clinic"
                    'Clinic
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(2)
                    Call ShowAdministrator()
                Case "Provider"
                    'Doctor
                    '******By Sandip Deshmukh 18 Oct 07 2.23PM Bug# 350
                    '******The code is changed to show the proper window on toolstrip click 
                    '******previous it shows an doctor type instead doctor
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(3)
                    'trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(5)
                    '******By Sandip Deshmukh 18 Oct 07 2.23PM Bug# 350
                    Call ShowAdministrator()
                Case "Machines"
                    'Client Settings
                    '******By Sandip Deshmukh 18 Oct 07 12.01PM Bug# 349
                    '******The code is changed to show the proper window on toolstrip click 
                    '******previous it shows an provider-referral setting instead client settings
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(8)
                    ' trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(9)
                    '******By Sandip Deshmukh 18 Oct 07 12.01PM Bug# 349
                    Call ShowAdministrator()
                Case "Backup"
                    'Backup
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(1).Nodes(0)
                    Call ShowAdministrator()
                Case "SelfNotes"
                    'Self Notes
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(2).Nodes(0)
                    Call ShowAdministrator()
                Case "LoginUsers"
                    'Login Users
                    'trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(2).Nodes(0)
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(1).Nodes(3)

                    Call ShowAdministrator()
                Case "Settings"
                    'Settings
                    'trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(2).Nodes(1)
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(1).Nodes(1)
                    Call ShowAdministrator()
                Case "VoiceTrainingDocument"
                    'Voice Training Document
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(2).Nodes(3)
                    Call ShowAdministrator()
                Case "AuditReport"
                    'Audit Reports
                    trvAudit.SelectedNode = trvAudit.Nodes(0).Nodes(0)
                    Call ShowAudits()
                Case "ArchiveAudit"
                    'Archive Audit
                    trvAudit.SelectedNode = trvAudit.Nodes(0).Nodes(1)
                    Call ShowAudits()
                Case "ArchivedReport"
                    'Archived Audit Report
                    trvAudit.SelectedNode = trvAudit.Nodes(1).Nodes(0)
                    Call ShowAudits()
                Case "RestoreArchive"
                    'Restore Archive
                    trvAudit.SelectedNode = trvAudit.Nodes(1).Nodes(1)
                    Call ShowAudits()
                    '----------------------------------
                    'sarika 25th apr 2007
                    'Case "DBTool"
                    '    'Database Tool
                    '    trvDatabase.SelectedNode = trvDatabase.Nodes(0).Nodes(0)
                    '    Call showDBMGMT()
                    '----------------------------------
                Case "ClientMessage"
                    'Client Message
                    trvTools.SelectedNode = trvTools.Nodes(0).Nodes(0)
                    Call showDBTool()
                Case "OnlineUpdates"
                    'Online Updates
                    trvTools.SelectedNode = trvTools.Nodes(0).Nodes(1)
                    Call showDBTool()
                Case "Suggestion"
                    'Suggesstions to gloStream
                    trvTools.SelectedNode = trvTools.Nodes(0).Nodes(2)
                    Call showDBTool()
                Case "AboutUs"
                    'About us
                    Dim frm As New frmAboutUs
                    frm.ShowDialog()
                Case "LockScreen"
                    'Lock Screen
                    Dim frm As New frmLockScreen
                    frm.ShowDialog()
                    '---------------------------------------
                    'sarika 21st july 08
                    'bug 859
                    'do the status bar settings
                    UpdateStatusBar()
                    '---------------------------------------
                Case "StartUpSettings"
                    Dim frmSettings As New frmStartupSettings
                    frmSettings.blnOpenFromMainForm = True
                    frmSettings.ShowDialog()
                Case "Close"
                    'Close
                    Me.Close()

                    'sarika  21 feb
                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.Logout, gstrLoginName & " user has logged out.", gstrLoginName, gstrClientMachineName, 0, True)
                    objAudit = Nothing
                    '-------------

                Case "Logout"
                    _LogoutStatus = True
                    'sarika  21 feb
                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.Logout, gstrLoginName & " user has logged out.", gstrLoginName, gstrClientMachineName)
                    objAudit = Nothing
                    '-------------

                    'Me.Hide()
                    'Dim frm As New frmServerInstallation
                    'frm.ShowDialog()


                    Dim frm As New frmSplash
                    frm.Show()
                    Me.Close()

                    ''//Code added by Ravikiran on 10/02/2007
                Case "RxReportDesigner"
                    Dim frm As New frmRxReportDesigner
                    frm.ShowDialog()
                    '/// Updation Ends

                    '------------------------------
                    'Sarika 24th Apr 2007
                Case "LSAssociation"
                    '******By Sandip Deshmukh 24 Oct 07 11.13AM Bug# 351
                    '******Open the wrong window at behind on tooltstrip click
                    dgData.Visible = False
                    SplitterMainCategory.Visible = False
                    trvCategory.Visible = False
                    'picMainSepMain.Visible = False
                    pnlMainMainTop.Visible = False
                    pnlCommandButtons.Visible = False
                    'trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(10)
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(9)
                    lblMainTop.Text = "Clinic Workflow Settings"
                    '******24 Oct 07 11.13AM Bug# 351

                    Dim frmLocStatus As New frmLocationStatus
                    frmLocStatus.ShowDialog()

                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, gstrLoginName & " user has viewed the Clinic WorkFlow Settings.", gstrLoginName, gstrClientMachineName, 0, True)
                    objAudit = Nothing
                Case "Claim Validation Setting"
                    'Dim frm As New frmEDISettings(gstrConnectionString)
                    'frm.ShowDialog()
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(1).Nodes(0)
                    '------------------------------
                    Call ShowAdministrator()

                Case "CMSSetting"
                    Dim oFrm As New frmCMSPrinterSettings
                    oFrm.ShowDialog()
                    oFrm.Dispose()
                    oFrm = Nothing
                Case "NewCMSSetting"

                    Dim ofrm As New frmCMSPrinterSettingsNew(dgData.Item(dgData.CurrentRowIndex, 0)) 'added by hemant
                    '   Dim oFrm As New frmCMSPrinterSettingsNew 'commented by hemant
                    oFrm.ShowDialog()
                    oFrm.Dispose()
                    oFrm = Nothing
                Case "UB04"
                    Dim oFrm As New frmUB04PrinterSettings
                    oFrm.ShowDialog()
                    oFrm.Dispose()
                    oFrm = Nothing
                Case "UserGuide"
                    Dim helpFileName As String = System.IO.Path.Combine(Application.StartupPath, "help\gloPM_Admin_User_Manual.chm")
                    If System.IO.File.Exists(helpFileName) Then
                        Help.ShowHelp(Me, "file://" & helpFileName, "Welcome_User_Manual.htm")
                        Help.ShowHelp(Me, "file://" & helpFileName, HelpNavigator.TableOfContents)
                    End If
            End Select


        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub EDIWorkerCompUtility()
        Dim con As New SqlConnection
        Dim dtServicesDatabases As New DataSet()
        con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim oDB As New gloDatabaseLayer.DBLayer(con.ConnectionString)
        Try
            oDB.Connect(False)
            oDB.Execute("EDIWorkerCompUtility")
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Sub

    'sarika 11th sept 07

    Public Sub Fill_DoctorType()
        Dim trvChild As TreeNode

        Try
            With trvCategory
                .BeginUpdate()
                .Nodes.Clear()
                trvChild = New TreeNode
                With trvChild
                    .Text = "Provider Type"
                    .ImageIndex = 3
                    .SelectedImageIndex = 3
                    .ForeColor = Color.FromArgb(31, 73, 125)
                End With
                .Nodes.Add(trvChild)
                trvChild = Nothing
                .ExpandAll()
                .EndUpdate()
            End With


            Dim clDoctorType As New Collection
            Dim objDoctorType As New clsProviderType
            clDoctorType = objDoctorType.Fill_ProviderTypes()
            objDoctorType = Nothing
            Dim nCount As Integer

            For nCount = 1 To clDoctorType.Count
                trvChild = New TreeNode
                With trvChild
                    .Text = clDoctorType.Item(nCount)
                    .ImageIndex = 14
                    .SelectedImageIndex = 14
                    .ForeColor = Color.FromArgb(31, 73, 125)
                End With
                trvCategory.Nodes(0).Nodes.Add(trvChild)
                trvChild = Nothing
            Next
            trvCategory.ExpandAll()


        Catch ex As Exception

        End Try

    End Sub
    'sarika 12th sept 07

    Public Function GetCanDeleteFlag(ByVal ProviderTypeID As Int64) As Boolean
        Dim conn As New SqlConnection
        conn.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim cmd As SqlCommand
        Dim bDelete As Boolean = False

        Try
            conn.Open()


            cmd = New SqlCommand()
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select count(*) from Provider_mst where nProviderType =" & ProviderTypeID

            Dim cnt As Integer = 0

            cnt = cmd.ExecuteScalar()

            If cnt = 0 Then
                bDelete = True
            Else
                bDelete = False
            End If

            Return bDelete
        Catch ex As Exception
            Return False
        Finally
            conn.Close()

        End Try
    End Function

    '------------------------------------------------------------------------------------------

    'sarika Audit Log Instr Search 
#Region "Instring search for audit log reports"

    Private Sub dgData_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgData.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As DataGrid.HitTestInfo = dgData.HitTest(ptPoint)

        Try

            ' If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then
            Select Case htInfo.Column
                'Case 1
                '    lblSearch.Text = "Activity Date"
                Case 2
                    lblSearch.Text = "Software Component"
                Case 3
                    lblSearch.Text = "Machine"
                Case 4
                    lblSearch.Text = "User"
                Case 5
                    lblSearch.Text = "Category"
                Case 6
                    lblSearch.Text = "Patient Code"
                Case 7
                    lblSearch.Text = "Patient"
                Case 8
                    lblSearch.Text = "Description"
                Case 9
                    lblSearch.Text = "Outcome"
            End Select




            If txtInstringSearch.Text = "" Then
                _blnSearch = True
            Else
                _blnSearch = False
                txtInstringSearch.Text = ""
                _blnSearch = True
            End If
            'ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
            '    '    _blnSearch = True
            '    '    Call UpdateCategory()

            '    _blnSearch = True
            '    Select Case htInfo.Column.ToString() 'DataGrid.HitTestType.Cell
            '        'Case 1
            '        '    lblSearch.Text = "Activity Date"
            '        Case 2
            '            lblSearch.Text = "Software Component"
            '        Case 3
            '            lblSearch.Text = "Machine"
            '        Case 4
            '            lblSearch.Text = "User"
            '        Case 5
            '            lblSearch.Text = "Category"
            '        Case 6
            '            lblSearch.Text = "Patient Code"
            '        Case 7
            '            lblSearch.Text = "Patient"
            '        Case 8
            '            lblSearch.Text = "Description"
            '        Case 9
            '            lblSearch.Text = "Outcome"
            '    End Select

            '  End If
        Catch ex As Exception
        End Try

        Try
            'Changes made by Hemant on 11 June 2015
            If (htInfo.Column = 0) Then

                If dgData.CurrentRowIndex < 0 And Trim(trvAdminMenu.SelectedNode.Text) <> "User Groups" Then
                    Exit Sub
                End If
                If enmUserOperation = enmOperation.Admin Then
                    If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
                        Exit Sub
                    End If
                    Select Case Trim(trvAdminMenu.SelectedNode.Text)
                        Case "CMS1500 02/12 Settings"
                            Dim frm As New frmCMSPrinterSettingsNew(dgData.Item(dgData.CurrentRowIndex, 0))
                            frm.ShowDialog()
                            frm = Nothing

                    End Select
                End If
            End If
            'End of changes by Hemant

            If (htInfo.Column > 0) Then

                If dgData.CurrentRowIndex < 0 And Trim(trvAdminMenu.SelectedNode.Text) <> "User Groups" Then
                    Exit Sub
                End If
                If enmUserOperation = enmOperation.Admin Then
                    If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
                        Exit Sub
                    End If
                    Select Case Trim(trvAdminMenu.SelectedNode.Text)

                        Case "User Management"
                            Dim objUser As New clsUsers
                            'Dim frmgloEMRuser As New frmUser
                            Dim frmgloEMRuser As New frmUserMgt
                            frmgloEMRuser.Fill_UserRights()
                            frmgloEMRuser.blnModify = True
                            Call frmgloEMRuser.Fill_gloEMRGroups()
                            Call frmgloEMRuser.Fill_MaritalStatus()
                            Call frmgloEMRuser.Fill_Gender()
                            Call frmgloEMRuser.Fill_Providers()
                            frmgloEMRuser.txtUserName.Tag = dgData.Item(dgData.CurrentRowIndex, 0)
                            objUser.SearchUser(dgData.Item(dgData.CurrentRowIndex, 0))


                            Dim _IsBusinessCenterFeatureOn As Boolean = False
                            _IsBusinessCenterFeatureOn = IsBusinessCenterFeatureOn()
                            If (_IsBusinessCenterFeatureOn = True) Then
                                Call frmgloEMRuser.FillBusinessCenter()
                                frmgloEMRuser.cmbBusinessCenter.Visible = True
                                frmgloEMRuser.lblBusinessCenter.Visible = True
                            End If


                            frmgloEMRuser.txtUserName.Text = objUser.UserName
                            Dim objEncryption As New clsEncryption
                            frmgloEMRuser.txtPassword.Text = objEncryption.DecryptFromBase64String(objUser.Password, constEncryptDecryptKey)
                            frmgloEMRuser.txtNickName.Text = objEncryption.DecryptFromBase64String(objUser.NickName, constEncryptDecryptKey)
                            objEncryption = Nothing
                            frmgloEMRuser.txtConfirmPassword.Text = frmgloEMRuser.txtPassword.Text
                            frmgloEMRuser.txtFirstName.Text = objUser.FirstName
                            frmgloEMRuser.txtMiddleName.Text = objUser.MiddleName
                            frmgloEMRuser.txtLastName.Text = objUser.LastName
                            frmgloEMRuser.txtSSN.Text = objUser.SSNNo
                            frmgloEMRuser.dtDOB.Value = objUser.DOB
                            frmgloEMRuser.cmbGender.SelectedIndex = frmgloEMRuser.cmbGender.FindStringExact(objUser.Gender)
                            frmgloEMRuser.cmbMaritalStatus.SelectedIndex = frmgloEMRuser.cmbMaritalStatus.FindStringExact(objUser.MaritalStatus)

                            'Commented by Rahul Patel on 08-09-2010  
                            'frmgloEMRuser.txtStreet.Text = objUser.Street

                            'frmgloEMRuser.txtAddress.Text = objUser.Address
                            'frmgloEMRuser.txtAddress2.Text = objUser.Address2
                            'frmgloEMRuser.txtCity.Text = objUser.City
                            'frmgloEMRuser.txtState.Text = objUser.State
                            'frmgloEMRuser.txtZip.Text = objUser.ZIP

                            ''Dhruv 
                            frmgloEMRuser.oAddressContol.isFormLoading = True
                            frmgloEMRuser.oAddressContol.txtAddress1.Text = objUser.Address
                            frmgloEMRuser.oAddressContol.txtAddress2.Text = objUser.Address2
                            frmgloEMRuser.oAddressContol.txtCity.Text = objUser.City
                            frmgloEMRuser.oAddressContol.cmbState.Text = objUser.State
                            frmgloEMRuser.oAddressContol.txtZip.Text = objUser.ZIP
                            frmgloEMRuser.oAddressContol.txtCounty.Text = objUser.County
                            frmgloEMRuser.oAddressContol.cmbCountry.Text = objUser.Country
                            frmgloEMRuser.oAddressContol.isFormLoading = False

                            frmgloEMRuser.txtPhoneNo.Text = objUser.PhoneNo
                            frmgloEMRuser.txtMobileNo.Text = objUser.MobileNo
                            frmgloEMRuser.txtFax.Text = objUser.FAX
                            frmgloEMRuser.txtEmailAddress.Text = objUser.Email
                            frmgloEMRuser.chkgloEMRAdmin.Checked = objUser.gloEMRAdministrator
                            frmgloEMRuser.chkAuditTrails.Checked = objUser.IsAuditTrail
                            frmgloEMRuser.chkAccessDenied.Checked = objUser.AccessDenied
                            'frmgloEMRuser.picSignature.Image = objUser.Signature
                            If IsNothing(objUser.Signature) = False Then
                                frmgloEMRuser.picSignature.Image = CType(objUser.Signature, Image)
                                frmgloEMRuser.picSignature.SizeMode = PictureBoxSizeMode.CenterImage '// Strech
                            End If

                            ''''Exchange user
                            frmgloEMRuser.chkExchnageUser.Checked = objUser.IsExchangeUser

                            frmgloEMRuser.txtExchangeLogin.Text = objUser.ExchangeLogin
                            frmgloEMRuser.txtExchangePwd.Text = objUser.ExchangePassward
                            frmgloEMRuser.txtExchangePwdConfirm.Text = objUser.ExchangePassward
                            frmgloEMRuser.txtWindowsLoginName.Text = objUser.WindowLoaginName
                            objEncryption = New clsEncryption()
                            frmgloEMRuser.txtOCPLoginName.Text = If(objUser.OCPLoginName = "", "", objUser.OCPLoginName)
                            frmgloEMRuser.txtOCPLoginPassword.Text = If(objUser.OCPLoginPassword = "", "", objEncryption.DecryptFromBase64String(objUser.OCPLoginPassword, constEncryptDecryptKey))
                            frmgloEMRuser.txtOCPConfirmPassword.Text = If(objUser.OCPLoginPassword = "", "", objEncryption.DecryptFromBase64String(objUser.OCPLoginPassword, constEncryptDecryptKey))
                            frmgloEMRuser.chkAllowPortalAccess.Checked = objUser.IsAllowPortalAccess
                            frmgloEMRuser.chkSameAsUserDetails.Checked = objUser.IsSameAsUserDetails
                            If objUser.IsSameAsUserDetails Then
                                frmgloEMRuser.rb_UseCurrentCredential.Checked = True
                            Else
                                frmgloEMRuser.rb_NewCredential.Checked = True
                            End If
                            objEncryption = Nothing
                            ''''
                            'added by mahendra for Emergency Access 
                            frmgloEMRuser.ChkEmergencyAccess.Checked = objUser.EAPChart
                            If (objUser.EAPChart = True) Then
                                objEncryption = New clsEncryption()
                                frmgloEMRuser.txtEAPassword.Text = objEncryption.DecryptFromBase64String(objUser.EAPassword, constEncryptDecryptKey)
                                frmgloEMRuser.txtEAConfirmPassword.Text = frmgloEMRuser.txtEAPassword.Text
                                With frmgloEMRuser.dtpValidupto
                                    .Format = DateTimePickerFormat.Custom
                                    .CustomFormat = DTFORMAT
                                End With
                                frmgloEMRuser.dtpValidupto.Value = objUser.ValidDt.ToString()

                            End If

                            If Not IsNothing(objUser.BlockStatus) = True Then
                                frmgloEMRuser.nBlockStatus = objUser.BlockStatus
                            End If
                            '-----

                            'If objUser.ProviderID = 0 Then
                            '    frmgloEMRuser.cmbProvider.SelectedIndex = 0
                            'Else
                            '    'Retrieve Provider Name
                            '    Dim strProviderName As String
                            '    Dim objProvider As New clsProvider
                            '    strProviderName = objProvider.RetrieveProviderName(objUser.ProviderID)
                            '    objProvider = Nothing
                            '    If Trim(strProviderName) <> "" Then
                            '        frmgloEMRuser.cmbProvider.Text = strProviderName
                            '    Else
                            '        frmgloEMRuser.cmbProvider.SelectedIndex = 0
                            '    End If
                            'End If

                            'GLO2011-0015056 : gloPM Admin User Settings Inconsistent
                            'show the provider of selected user in provider combo box
                            frmgloEMRuser.cmbProvider.SelectedValue = objUser.ProviderID
                            frmgloEMRuser.cmbBusinessCenter.SelectedValue = objUser.BusinessCenterID
                            Dim clGroups As New Collection
                            clGroups = objUser.UserGroups
                            Dim nCount As Integer
                            Dim nCount1 As Integer
                            For nCount = 1 To clGroups.Count
                                For nCount1 = 0 To frmgloEMRuser.lstGroups.Items.Count - 1
                                    If Trim(clGroups.Item(nCount)) = Trim(frmgloEMRuser.lstGroups.Items(nCount1)) Then
                                        frmgloEMRuser.lstGroups.SetItemChecked(nCount1, True)
                                    End If
                                Next
                            Next
                            Dim clRights As New Collection
                            clRights = objUser.UserRights
                            Dim nTotalNodes As Int16
                            nTotalNodes = frmgloEMRuser.trvUserRights.GetNodeCount(False) - 1
                            For nCount = 1 To clRights.Count
                                For nCount1 = 0 To nTotalNodes
                                    SearchNode(frmgloEMRuser.trvUserRights, clRights.Item(nCount))
                                    If IsNothing(trvSearchNode) = False Then
                                        trvSearchNode.Checked = True
                                        frmgloEMRuser.trvUserRights.SelectedNode = trvSearchNode
                                        'frmgloEMRuser.trvUserRights.SelectedNode.Checked = True
                                    End If
                                Next
                            Next
                            objUser = Nothing
                            frmgloEMRuser.trvUserRights.ExpandAll()
                            frmgloEMRuser.blnCheckRights = True
                            If frmgloEMRuser.ShowDialog() = DialogResult.OK Then
                                Call Fill_CategorygloEMRUsers()
                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                    Call Fill_DetailsgloEMRUsers()
                                Else
                                    dgData.DataSource = Nothing
                                End If
                            End If
                            frmgloEMRuser = Nothing
                            Exit Sub
                        Case "Clearinghouse"
                            Dim ID As Int64 = 0
                            ID = Convert.ToInt64(dgData.Item(dgData.CurrentRowIndex, 0))
                            Dim numRows As Integer = dgData.BindingContext(dgData.DataSource, dgData.DataMember).Count
                            Dim frm As New frmSetupClearingHouse(gstrConnectionString, ID, numRows)
                            frm.ShowDialog()
                            frm.Dispose()
                            Fill_ClearingHouse()
                            'code added by pradeep on 22/06/2010 for prefix
                        Case "Site Prefix"
                            btnModify_Click(Nothing, Nothing)
                    End Select

                ElseIf enmUserOperation = enmOperation.Tools Then

                End If


            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub


    Private Sub txtInstringSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInstringSearch.TextChanged
        Dim objAudit As clsAudit

        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvAuditLogReport As DataView
                dvAuditLogReport = CType(dgData.DataSource(), DataView)

                If IsNothing(dvAuditLogReport) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                dgData.DataSource = dvAuditLogReport
                Dim strSearchDetails As String
                If Trim(txtInstringSearch.Text) <> "" Then
                    strSearchDetails = Replace(txtInstringSearch.Text, "'", "''")
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                    strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
                Else
                    strSearchDetails = ""
                End If

                Select Case Trim(lblSearch.Text)
                    Case "Software Component"
                        dvAuditLogReport.RowFilter = " sSoftwareComponent Like '%" & strSearchDetails & "%'"
                        'Case "Activity Date"
                        '    dvAuditLogReport.RowFilter = " ActivityDate Like '%" & strSearchDetails & "%'"
                    Case "Category"
                        ' dvAuditLogReport.RowFilter = " sActivityCategory Like '%" & strSearchDetails & "%'"
                        dvAuditLogReport.RowFilter = " Category Like '%" & strSearchDetails & "%'"
                    Case "Machine"
                        dvAuditLogReport.RowFilter = " MachineName Like '%" & strSearchDetails & "%'"
                    Case "User"
                        dvAuditLogReport.RowFilter = " UserName Like '%" & strSearchDetails & "%'"
                    Case "Patient Code"
                        If strSearchDetails = "" Then
                            '  dvAuditLogReport.RowFilter = " PatientCode Like '%" & strSearchDetails & "%' or PatientCode=''"
                            Call Fill_DetailsAuditReports()

                        Else
                            dvAuditLogReport.RowFilter = " PatientCode Like '%" & strSearchDetails & "%'"

                        End If
                    Case "Patient"
                        If strSearchDetails = "" Then
                            '  dvAuditLogReport.RowFilter = " PatientCode Like '%" & strSearchDetails & "%' or PatientCode=''"
                            Call Fill_DetailsAuditReports()

                        Else
                            dvAuditLogReport.RowFilter = " PatientName Like '%" & strSearchDetails & "%'"
                        End If
                    Case "OutCome"
                        dvAuditLogReport.RowFilter = " sOutCome Like '%" & strSearchDetails & "%'"
                    Case "Description"
                        dvAuditLogReport.RowFilter = " CategoryDescription Like '%" & strSearchDetails & "%'"




                End Select

                objAudit = New clsAudit

                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "User viewed audit reports for records with : " & lblSearch.Text & " like : '" & txtInstringSearch.Text.Trim & "'", gstrLoginName, gstrClientMachineName, 0, True, clsAudit.enmOutcome.Success)
                objAudit = Nothing


                Me.Cursor = Cursors.Default
            Catch objErr As Exception

                objAudit = New clsAudit

                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "User failed to view audit reports for records with : " & lblSearch.Text & " like : '" & txtInstringSearch.Text.Trim & "'", gstrLoginName, gstrClientMachineName, 0, True, clsAudit.enmOutcome.Failure)
                objAudit = Nothing

                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub


#End Region
    '-----
    Private Sub Fill_ClearingHouse()
        Dim dtLogin As New DataTable
        Dim objLogin As New clsLogin
        dtLogin = GetClearingHouse()
        objLogin = Nothing
        dgData.DataSource = dtLogin
        dgData.CaptionText = "Clearinghouse"

        Dim grdTableStyle As New clsDataGridTableStyle(dtLogin.TableName)

        Dim col1 As New DataGridTextBoxColumn
        With col1
            .HeaderText = "ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With
        Dim col2 As New DataGridTextBoxColumn
        With col2
            .HeaderText = "Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(1).ColumnName
            .NullText = ""
            .Width = 0.14 * dgData.Width
        End With

        Dim col4 As New DataGridTextBoxColumn
        With col4
            .HeaderText = "Receiver ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(2).ColumnName
            .NullText = ""
            .Width = 0.14 * dgData.Width
        End With

        Dim col3 As New DataGridTextBoxColumn
        With col3
            .HeaderText = "Receiver Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(3).ColumnName
            .NullText = ""
            .Width = 0.14 * dgData.Width
        End With
        Dim col5 As New DataGridTextBoxColumn
        With col5
            .HeaderText = "Sender ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(4).ColumnName
            .NullText = ""
            .Width = 0.14 * dgData.Width
        End With
        Dim col6 As New DataGridTextBoxColumn
        With col6
            .HeaderText = "IS1J Qualifier"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(5).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim col7 As New DataGridTextBoxColumn
        With col7
            .HeaderText = "1J Qualifier"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(6).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim col8 As New DataGridTextBoxColumn
        With col8
            .HeaderText = "IS Sender Code"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(7).ColumnName
            .NullText = ""
            .Width = 0
        End With
        Dim col11 As New DataGridTextBoxColumn
        With col11
            .HeaderText = "Sender Code"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(8).ColumnName
            .NullText = ""
            .Width = 0.14 * dgData.Width
        End With
        Dim col10 As New DataGridTextBoxColumn
        With col10
            .HeaderText = "IS Receiver Code"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(9).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim col9 As New DataGridTextBoxColumn
        With col9
            .HeaderText = "Receiver Code"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(10).ColumnName
            .NullText = ""
            .Width = 0.14 * dgData.Width
        End With
        Dim col12 As New DataGridTextBoxColumn
        With col12
            .HeaderText = "IS Loop 1000B NM109"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(11).ColumnName
            .NullText = ""
            .Width = 0
        End With
        ''
        Dim col13 As New DataGridTextBoxColumn
        With col13
            .HeaderText = "Loop 1000B"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(12).ColumnName
            .NullText = ""
            .Width = 0
        End With
        Dim col14 As New DataGridTextBoxColumn
        With col14
            .HeaderText = "Type Of Data"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(13).ColumnName
            .NullText = ""
            .Width = 0
        End With
        Dim col15 As New DataGridTextBoxColumn
        With col15
            .HeaderText = "ISA"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(14).ColumnName
            .NullText = ""
            .Width = 0 ''0.14 * dgData.Width
        End With

        ''If IsMultipleClearingHouse() = True Then

        Dim col16 As New DataGridTextBoxColumn
        With col16
            .HeaderText = "Default"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(15).ColumnName
            .NullText = ""
            .Width = 0.14 * dgData.Width
        End With

        ''End If
        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {col1, col2, col3, col4, col5, col9, col11, col15, col16})
        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyle)

        If IsMultipleClearingHouse() = False Then
            dgData.TableStyles(0).GridColumnStyles(8).Width = 0
        Else
            dgData.TableStyles(0).GridColumnStyles(8).Width = 0.14 * dgData.Width
        End If
        'If IsMultipleClearingHouse() Then
        '    grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {col1, col2, col3, col4, col5, col9, col11, col15, col16})
        '    dgData.TableStyles.Clear()
        '    dgData.TableStyles.Add(grdTableStyle)
        'Else
        '    grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {col1, col2, col3, col4, col5, col9, col11, col15})
        '    dgData.TableStyles.Clear()
        '    dgData.TableStyles.Add(grdTableStyle)
        'End If


    End Sub
    'below by hemant 
    Private Sub Fill_Printers(Optional ByVal isUb04 As Boolean = False)
        Try
            Dim dtPrinter As DataTable
            If isUb04 = False Then
                dtPrinter = GetPrinters()
            Else
                dtPrinter = GetPrinters_UB04()
            End If
            dgData.DataSource = Nothing
            dgData.DataSource = dtPrinter
            dgData.CaptionText = "Printer Setup"
            Dim grdTableStyle As New clsDataGridTableStyle(dtPrinter.TableName)
            Dim col1 As New DataGridTextBoxColumn
            With col1
                .HeaderText = "Printer Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtPrinter.Columns(0).ColumnName
                .NullText = ""
                .Width = 0

            End With
            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {col1})
            dgData.TableStyles.Clear()

            dgData.TableStyles.Add(grdTableStyle)
            dgData.TableStyles(0).GridColumnStyles(0).Width = 1.22 * dgData.Width
            dgData.Select(0)

            dtPrinter = Nothing
            grdTableStyle = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
    End Sub
    'above by hemant 


    Private _ClinicID As Int64 = 1

    Public Function GetClearingHouse() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim dtClearing As New DataTable()
        Try

            Dim _sqlQuery As String = ""

            oDB.Connect(False)

            _sqlQuery = "SELECT ISNULL(nClearingHouseID,0) AS nClearingHouseID,ISNULL(sClearingHouseCode,'') AS sClearingHouseName," _
& " ISNULL(sReceiverID,'') AS sReceiverID,ISNULL(sReceiverName,'') AS sReceiverName,ISNULL(sSubmitterID,'') AS sSubmitterID," _
& " ISNULL(bIsOneJQulifier,'false') AS bIsOneJQulifier,ISNULL(sOneJQulifier,'') AS sOneJQulifier,ISNULL(bIsSenderCode,'false') AS bIsSenderCode," _
& " ISNULL(sSenderCode,'') AS sSenderCode,ISNULL(bIsVenderIDCode,'false') AS bIsVenderIDCode,ISNULL(sVenderIDCode,'') AS sVenderIDCode," _
& " ISNULL(bIsLoop1000BNM109,'false') AS bIsLoop1000BNM109,ISNULL(sLoop1000BNM109,'') AS sLoop1000BNM109,ISNULL(nTypeOfData,0) AS nTypeOfData," _
& " CASE ISNULL(bIsISA,'FALSE') WHEN 'TRUE' THEN 'YES' WHEN 'FALSE' THEN 'NO' END AS bIsISA," _
& " CASE ISNULL(bIsDefault,'FALSE') WHEN 'TRUE' THEN 'Default' WHEN 'FALSE' THEN '' END AS bIsDefault   FROM BL_ClearingHouse_MST  ORDER By isnull(BL_ClearingHouse_MST.bIsDefault,0) desc" 'WHERE nClinicID = " & _ClinicID & " "

            oDB.Retrive_Query(_sqlQuery, dtClearing)
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
        Return dtClearing
    End Function
    'below  by hemant 
    Public Function GetPrinters() As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim dtPrinters As DataTable = Nothing
        Try
            Dim _sqlQuery As String = ""
            oDB.Connect(False)
            _sqlQuery = "select distinct sPrinterName from BL_NewCMS1500PrintSettings"

            oDB.Retrive_Query(_sqlQuery, dtPrinters)

        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
        Return dtPrinters
    End Function

    Public Function GetPrinters_UB04() As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim dtPrinters As DataTable = Nothing
        Try
            Dim _sqlQuery As String = ""
            oDB.Connect(False)
            _sqlQuery = "select distinct case sPrinterName when '' then 'Default' else sPrinterName  end as sPrinterName from BL_UB04PrintSettings"

            oDB.Retrive_Query(_sqlQuery, dtPrinters)

        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
        Return dtPrinters
    End Function

    Public Sub DeletePrinter(ByVal printerName As String)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)

        Try
            Dim _sqlQuery As String = ""
            oDB.Connect(False)
            If printerName.Contains("'") Then
                printerName = printerName.Replace("'", "''")
            End If
            _sqlQuery = "DELETE FROM dbo.BL_NewCMS1500PrintSettings WHERE sPrinterName='" + printerName + "'"
            oDB.Execute_Query(_sqlQuery)
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
        End Try

    End Sub
    'above by hemant 

    Public Sub DeletePrinter_UB04(ByVal printerName As String)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Try
            Dim _sqlQuery As String = ""
            oDB.Connect(False)
            If printerName.Contains("'") Then
                printerName = printerName.Replace("'", "''")
            End If
            _sqlQuery = "DELETE FROM dbo.BL_UB04PrintSettings WHERE sPrinterName='" + printerName + "'"
            oDB.Execute_Query(_sqlQuery)
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
        End Try

    End Sub
    Public Function IsClearingHouseUsed(ByVal _nClearingHouseID As Int64) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim _result As Boolean
        Try

            Dim _sqlQuery As String = ""

            oDB.Connect(False)

            _sqlQuery = "Select ISNULL(Count(nClearingHouse),0) from Contacts_Insurance_DTL where ISNULL(nClearingHouse,0)=" & _nClearingHouseID & ""

            _result = Convert.ToBoolean(oDB.ExecuteScalar_Query(_sqlQuery))
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
        Return _result
    End Function

    Public Sub DeleteClearingHouseFromPlan(ByVal nClearingHouseID As Int64)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)

        Try

            Dim _sqlQuery As String = ""

            oDB.Connect(False)

            _sqlQuery = "UPDATE Contacts_Insurance_DTL  WITH (READPAST) SET nClearingHouse=0 where ISNULL(nClearingHouse,0)=" & nClearingHouseID & ""

            oDB.Execute_Query(_sqlQuery)
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try

    End Sub


    Private Function IsMultipleClearingHouse() As Boolean
        Dim _Isenable As Boolean = False
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim _sqlQuery As String = ""
        Dim oMultipleClearingHouse As New Object()

        Try
            oDB = New gloDatabaseLayer.DBLayer(gstrConnectionString)
            oDB.Connect(False)
            _sqlQuery = " select sSettingsValue from Settings WITH (NOLOCK) where  sSettingsName='ISMULTIPLECLEARINGHOUSE' and nClinicID=" & _ClinicID & ""

            oMultipleClearingHouse = oDB.ExecuteScalar_Query(_sqlQuery)
            If oMultipleClearingHouse IsNot Nothing AndAlso Convert.ToString(oMultipleClearingHouse) <> "" Then
                If Convert.ToString(oMultipleClearingHouse).ToUpper() = "1" Then
                    _Isenable = True
                Else
                    _Isenable = False
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            _Isenable = False
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()

            End If
        End Try

        Return _Isenable

    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Me.Close()
    End Sub

    Private Sub SetToolStrip()

        If (gstrAdminFor = "gloPM") Then

            tsbtnMachine.Visible = False
            tsbtnLSAssociation.Visible = False
            tsbtnAboutUs.Visible = False
            tsbtnArchiveAudit.Visible = False
            tsbtnArchivedReport.Visible = False
            tsbtnAuditReport.Visible = False
            tsbtnRestoreArchive.Visible = False

        End If
    End Sub

    Public Function IsBusinessCenterFeatureOn() As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim _result As Boolean
        Try

            Dim _sqlQuery As String = ""

            oDB.Connect(False)

            _sqlQuery = " select sSettingsValue from Settings where sSettingsName = 'BusinessCenter_Feature'"

            _result = Convert.ToBoolean(oDB.ExecuteScalar_Query(_sqlQuery))

        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
        Return _result
    End Function

    Private Sub Fill_ClientMachineDropDown()
        Try
            cmbClientMachineName.Items.Clear()
            Dim objClientMachine As New clsClientMachines
            Dim clClientMachine As New Collection
            clClientMachine = objClientMachine.Fill_ClientMachines()
            Dim nCount As Integer
            cmbClientMachineName.Items.Add("All")
            If Not IsNothing(clClientMachine) Then
                For nCount = 1 To clClientMachine.Count
                    cmbClientMachineName.Items.Add(clClientMachine.Item(nCount))
                Next
            Else
                Exit Sub
            End If

            cmbClientMachineName.SelectedIndex = 0
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Fill_ClientUpdateDetails()
        Try
            'If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then

            Dim dtClients As New DataTable
            Dim objClientMachine As New clsClientMachines
            'dtClients = objClientMachine.ScanClientMachine(CType(trvCategory.SelectedNode.Tag, Integer))
            'dtClients = objClientMachine.ScanClientMachineUpdates(CType(trvCategory.SelectedNode.Text, String))
            dtClients = objClientMachine.ScanClientMachineUpdates()
            dgData.DataSource = dtClients

            dgData.CaptionText = "Client Update Details"

            Dim grdTableStyle As New clsDataGridTableStyle(dtClients.TableName)

            Dim grdColStylePropertyName As New DataGridTextBoxColumn
            With grdColStylePropertyName
                .HeaderText = "Machine Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtClients.Columns(0).ColumnName
                .NullText = ""
                .Width = 0.2 * dgData.Width
            End With

            Dim grdColStylePropertyType As New DataGridTextBoxColumn
            With grdColStylePropertyType
                .HeaderText = "Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtClients.Columns(1).ColumnName
                .NullText = ""
                .Width = 0.15 * dgData.Width - 10
            End With

            Dim grdColStylePropertyVersion As New DataGridTextBoxColumn
            With grdColStylePropertyVersion
                .HeaderText = "Version"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtClients.Columns(2).ColumnName
                .NullText = ""
                .Width = 0.14 * dgData.Width - 10
            End With

            Dim grdColStylePropertyBuildVersion As New DataGridTextBoxColumn
            With grdColStylePropertyBuildVersion
                .HeaderText = "Build Version"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtClients.Columns(3).ColumnName
                .NullText = ""
                .Width = 0.15 * dgData.Width - 10
            End With

            Dim grdColStylePropertyStatus As New DataGridTextBoxColumn
            With grdColStylePropertyStatus
                .HeaderText = "Status"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtClients.Columns(4).ColumnName
                .NullText = ""
                .Width = 0.2 * dgData.Width - 10
            End With
            Dim grdColStylePropertyInstallDate As New DataGridTextBoxColumn
            With grdColStylePropertyInstallDate
                .HeaderText = "Install Date"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtClients.Columns(5).ColumnName
                .NullText = ""
                .Width = 0.23 * dgData.Width - 10
            End With


            grdTableStyle.GridColumnStyles.Clear()
            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePropertyName, grdColStylePropertyType, grdColStylePropertyVersion, grdColStylePropertyBuildVersion, grdColStylePropertyStatus, grdColStylePropertyInstallDate})

            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)

            objClientMachine = Nothing

            If Not IsNothing(dtClients) AndAlso Not (dtClients.Rows.Count <> 0) Then

                'MessageBox.Show(Me, "Selected machine does not exists, Please add this again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim e As EventArgs
                btnRefresh_Click(btnRefresh, e)
                Exit Sub
            End If
            '------------------------------------
            'Sarika 21st April 2007
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The details of Client : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '------------------------------------
            ' End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbClientMachineName_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbClientMachineName.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvClientUpdateReport As DataView
            Call Fill_ClientUpdateDetails()
            Dim dtTemp As DataTable
            dtTemp = CType(dgData.DataSource, DataTable)
            dvClientUpdateReport = CType(dtTemp.DefaultView, DataView)
            If IsNothing(dvClientUpdateReport) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            dgData.DataSource = dvClientUpdateReport
            Dim strSearchDetails As String
            If Trim(cmbClientMachineName.SelectedItem) <> "" And Trim(cmbClientMachineName.SelectedItem).ToUpper() <> "ALL" Then
                strSearchDetails = Replace(cmbClientMachineName.SelectedItem, "'", "''")
                strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
            Else
                strSearchDetails = "%"
            End If

            dvClientUpdateReport.RowFilter = "[Machine Name] Like '%" & strSearchDetails & "%'"

            Me.Cursor = Cursors.Default
        Catch ex As Exception

        Finally

        End Try
    End Sub


    Private Sub btnSearchClientUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchClientUpdate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvClientUpdateReport As DataView
            Call Fill_ClientUpdateDetails()
            Dim dtTemp As DataTable
            dtTemp = CType(dgData.DataSource, DataTable)
            dvClientUpdateReport = CType(dtTemp.DefaultView, DataView)
            If IsNothing(dvClientUpdateReport) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            dgData.DataSource = dvClientUpdateReport
            Dim strSearchDetails As String
            If txtSearchClientUpdate.Text.Trim() <> "" Then
                strSearchDetails = Replace(txtSearchClientUpdate.Text.Trim(), "'", "''")
                strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
            Else
                strSearchDetails = "%"
            End If

            Dim strCmbClientMachineTxt As String

            If cmbClientMachineName.Text.Trim().ToUpper() = "ALL" Then
                strCmbClientMachineTxt = "%"
            Else
                strCmbClientMachineTxt = cmbClientMachineName.Text.Trim()

            End If
            dvClientUpdateReport.RowFilter = "[Machine Name] Like '%" & strCmbClientMachineTxt & "%' and ([Machine Name] Like '%" & strSearchDetails & "%' or [Product Name] Like '%" & strSearchDetails & "%' or [Version] Like '%" & strSearchDetails & "%'  or [Build Version] Like '%" & strSearchDetails & "%' or [Status] Like '%" & strSearchDetails & "%' or [Install Date] Like '%" & strSearchDetails & "%')"


            Me.Cursor = Cursors.Default
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCloneProvider_Click(sender As System.Object, e As System.EventArgs) Handles btnCloneProvider.Click
        Try
            If (Trim(trvAdminMenu.SelectedNode.Text) <> "Client Settings") Then
                If dgData.CurrentRowIndex < 0 And Trim(trvAdminMenu.SelectedNode.Text) <> "User Groups" Then
                    Exit Sub
                End If
            End If

            If enmUserOperation = enmOperation.Admin Then
                If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
                    Exit Sub
                End If

                Select Case Trim(trvAdminMenu.SelectedNode.Text)
                    Case "Provider"
                        UpdateErrorLog("Cloning Provider", mdlFunctions.enmErrorOccuredForm.MainForm, mdlFunctions.enmOperation.Update)
                        Dim objDoctor As New clsProvider(gstrConnectionString)
                        Dim frmgloEMRDoctor As New frmDoctor(CType(trvCategory.SelectedNode.Tag, Int64), True)
                        frmgloEMRDoctor.Text = "Clone Provider"
                        frmgloEMRDoctor.blnModify = False
                        Dim myNode As New TreeNode
                        myNode = trvCategory.SelectedNode

                        If frmgloEMRDoctor.ShowDialog() = DialogResult.OK Then
                            trvCategory.SelectedNode = myNode
                            Call Fill_CategoryProviders()
                            If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                Call Fill_DetailsProviders()
                            Else
                                dgData.DataSource = Nothing
                            End If
                        End If
                        btnRefresh_Click(Nothing, Nothing)
                        frmgloEMRDoctor = Nothing
                        objDoctor = Nothing
                End Select
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            MessageBox.Show(String.Format("Clone Provider error. Error: {0}", ex.Message), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

End Class
