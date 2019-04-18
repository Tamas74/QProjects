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
Imports Project_Reportview
Imports gloEMR.Help
Imports gloSettings
Imports gloPatientPortal

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
    Private dtLabUsers As DataTable
    Private strUserID As String = String.Empty
    'Tooltip instance added to set tooltip on the Hide ToolBar button of the form
    Dim toolTip1 As New ToolTip()
    Dim enmUserOperation As enmOperation
    Dim trvSearchNode As TreeNode
    Friend WithEvents stbPnlBuild As System.Windows.Forms.StatusBarPanel
    Friend WithEvents stbPnlLoginTime As System.Windows.Forms.StatusBarPanel
    Friend WithEvents stbPnlSQLServer As System.Windows.Forms.StatusBarPanel
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
    Friend WithEvents stbPnlSQLSeverDatabase As System.Windows.Forms.StatusBarPanel
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents cmbSearchPatient As System.Windows.Forms.ComboBox
    Friend WithEvents lblPatientID As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlAudit As System.Windows.Forms.Panel
    Friend WithEvents btnATNALog As System.Windows.Forms.ToolStripButton
    Friend WithEvents HelpComponent1 As gloEMR.Help.HelpComponent
    ' Friend WithEvents tsbtnClose As System.Windows.Forms.ToolStripButton
    Dim clRights As New Collection
    Friend WithEvents tsbtnUserGuide As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAuditLogTampering As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlClientUpdateDetailsFilter As System.Windows.Forms.Panel
    Friend WithEvents btnSearchClientUpdate As System.Windows.Forms.Button
    Friend WithEvents txtSearchClientUpdate As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cmbClientMachineName As System.Windows.Forms.ComboBox
    Friend WithEvents lblClientMachineName As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents tsbtnSyncTime As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnViewHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents lblLogTamperedStatus As System.Windows.Forms.PictureBox
    Private m_hotKeys As HotKeyCollection
    Public Delegate Sub HotKeyPressedEventHandler(ByVal sender As Object, ByVal e As HotKeyPressedEventArgs)
    'Public Event HotKeyPressed As HotKeyPressedEventHandler

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.


        InitializeComponent()
        sethelpBuildermode()
        m_hotKeys = New HotKeyCollection(Me)
        'Add any initialization after the InitializeComponent() call

    End Sub

    'Event HotKeyPressed(ByVal frmgloEMRAdmin As frmgloEMRAdmin, ByVal e As HotKeyPressedEventArgs)
    Public Event HotKeyPressed As HotKeyPressedEventHandler
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
    Friend WithEvents lblAuditCategory As System.Windows.Forms.Label
    Friend WithEvents cmbAuditCategory As System.Windows.Forms.ComboBox
    Friend WithEvents imglstToolBar As System.Windows.Forms.ImageList
    Friend WithEvents stbPnlVersion As System.Windows.Forms.StatusBarPanel
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
        Me.stbPnlSQLSeverDatabase = New System.Windows.Forms.StatusBarPanel()
        Me.stbPnlBuild = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
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
        Me.dgData = New gloEMRAdmin.clsDataGrid()
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
        Me.tsbtnRestoreArchive = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnStartupSettings = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnSyncTime = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnLockScreen = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnLogout = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnUserGuide = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnAboutUs = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitterMain = New System.Windows.Forms.Splitter()
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnModify = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnATNALog = New System.Windows.Forms.ToolStripButton()
        Me.btnAuditLogTampering = New System.Windows.Forms.ToolStripButton()
        Me.btnViewHistory = New System.Windows.Forms.ToolStripButton()
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
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.pnlClientUpdateDetailsFilter = New System.Windows.Forms.Panel()
        Me.btnSearchClientUpdate = New System.Windows.Forms.Button()
        Me.txtSearchClientUpdate = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.cmbClientMachineName = New System.Windows.Forms.ComboBox()
        Me.lblClientMachineName = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.txtPatient = New System.Windows.Forms.TextBox()
        Me.cmbSearchPatient = New System.Windows.Forms.ComboBox()
        Me.lblPatientID = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlAudit = New System.Windows.Forms.Panel()
        Me.HelpComponent1 = New gloEMR.Help.HelpComponent(Me.components)
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.lblLogTamperedStatus = New System.Windows.Forms.PictureBox()
        CType(Me.stbPnlLoginTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stbPnlVersion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stbPnlSQLSeverDatabase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stbPnlBuild, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.pnlMainTop.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.pnlMainMainTop.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.pnlClientUpdateDetailsFilter.SuspendLayout()
        Me.pnlAudit.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.lblLogTamperedStatus, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.StatusBar1.Location = New System.Drawing.Point(0, 0)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.stbPnlLoginTime, Me.stbPnlVersion, Me.stbPnlSQLSeverDatabase, Me.stbPnlBuild, Me.StatusBarPanel1})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1192, 22)
        Me.StatusBar1.SizingGrip = False
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
        '
        'stbPnlSQLSeverDatabase
        '
        Me.stbPnlSQLSeverDatabase.Icon = CType(resources.GetObject("stbPnlSQLSeverDatabase.Icon"), System.Drawing.Icon)
        Me.stbPnlSQLSeverDatabase.Name = "stbPnlSQLSeverDatabase"
        Me.stbPnlSQLSeverDatabase.Width = 450
        '
        'stbPnlBuild
        '
        Me.stbPnlBuild.Icon = CType(resources.GetObject("stbPnlBuild.Icon"), System.Drawing.Icon)
        Me.stbPnlBuild.Name = "stbPnlBuild"
        Me.stbPnlBuild.Width = 270
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 220
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
        Me.imglstMain_New.Images.SetKeyName(5, "DoctorType.ico")
        Me.imglstMain_New.Images.SetKeyName(6, "Doctor.ico")
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
        Me.imglstMain_New.Images.SetKeyName(17, "Settings1.ico")
        Me.imglstMain_New.Images.SetKeyName(18, "Audit lOG.ico")
        Me.imglstMain_New.Images.SetKeyName(19, "Open report.ico")
        Me.imglstMain_New.Images.SetKeyName(20, "Archive.ico")
        Me.imglstMain_New.Images.SetKeyName(21, "Archived Audit log.ico")
        Me.imglstMain_New.Images.SetKeyName(22, "Archive Audit Report.ico")
        Me.imglstMain_New.Images.SetKeyName(23, "Restore Archive.ico")
        Me.imglstMain_New.Images.SetKeyName(24, "User Group.ico")
        Me.imglstMain_New.Images.SetKeyName(25, "Payer ID.ico")
        Me.imglstMain_New.Images.SetKeyName(26, "Import Fee Schedule.ico")
        Me.imglstMain_New.Images.SetKeyName(27, "Prefix.ico")
        Me.imglstMain_New.Images.SetKeyName(28, "Database Criediantial.ico")
        Me.imglstMain_New.Images.SetKeyName(29, "SSRS Report.ico")
        Me.imglstMain_New.Images.SetKeyName(30, "Deploy SSRS Files.ico")
        Me.imglstMain_New.Images.SetKeyName(31, "Merge Insurance plans.ico")
        Me.imglstMain_New.Images.SetKeyName(32, "Lab User Task.ico")
        Me.imglstMain_New.Images.SetKeyName(33, "MobileMGT.ico")
        Me.imglstMain_New.Images.SetKeyName(34, "Setup Unscheduled care template.ico")
        Me.imglstMain_New.Images.SetKeyName(35, "Health Form.ico")
        Me.imglstMain_New.Images.SetKeyName(36, "Client Update.ico")
        Me.imglstMain_New.Images.SetKeyName(37, "User-provider glodirect.ico")
        Me.imglstMain_New.Images.SetKeyName(38, "PortalMessage.ico")
        Me.imglstMain_New.Images.SetKeyName(39, "AdvanceInsuranceMerge.ico")
        Me.imglstMain_New.Images.SetKeyName(40, "TaxID.ico")
        Me.imglstMain_New.Images.SetKeyName(41, "API_Harness.ico")
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
        Me.pnlLeft.Size = New System.Drawing.Size(290, 675)
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
        Me.Panel7.Size = New System.Drawing.Size(290, 66)
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
        Me.trvAudit.Size = New System.Drawing.Size(285, 57)
        Me.trvAudit.TabIndex = 35
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(4, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(285, 4)
        Me.Label12.TabIndex = 38
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(4, 62)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(285, 1)
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
        Me.Label18.Size = New System.Drawing.Size(1, 62)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(289, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 62)
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
        Me.Label20.Size = New System.Drawing.Size(287, 1)
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
        Me.Panel5.Size = New System.Drawing.Size(290, 28)
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
        Me.Panel6.Size = New System.Drawing.Size(287, 25)
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
        Me.Label2.Size = New System.Drawing.Size(285, 23)
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
        Me.Label13.Size = New System.Drawing.Size(285, 1)
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
        Me.Label15.Location = New System.Drawing.Point(286, 1)
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
        Me.Label16.Size = New System.Drawing.Size(287, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label1"
        '
        'splAudit
        '
        Me.splAudit.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.splAudit.Dock = System.Windows.Forms.DockStyle.Top
        Me.splAudit.Location = New System.Drawing.Point(0, 578)
        Me.splAudit.Name = "splAudit"
        Me.splAudit.Size = New System.Drawing.Size(290, 3)
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
        Me.Panel3.Size = New System.Drawing.Size(290, 550)
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
        Me.trvAdminMenu.Size = New System.Drawing.Size(285, 544)
        Me.trvAdminMenu.TabIndex = 3
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(4, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(285, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 38
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(4, 549)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(285, 1)
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
        Me.Label10.Location = New System.Drawing.Point(289, 1)
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
        Me.Label11.Size = New System.Drawing.Size(287, 1)
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
        Me.Panel4.Size = New System.Drawing.Size(290, 28)
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
        Me.Panel2.Size = New System.Drawing.Size(287, 25)
        Me.Panel2.TabIndex = 19
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(285, 1)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(285, 24)
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
        Me.Label4.Size = New System.Drawing.Size(285, 1)
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
        Me.Label6.Location = New System.Drawing.Point(286, 0)
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
        Me.imglstMain.Images.SetKeyName(3, "DoctorType.ico")
        Me.imglstMain.Images.SetKeyName(4, "Doctor.ico")
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
        Me.imglstMain.Images.SetKeyName(15, "Database Criediantial.ico")
        Me.imglstMain.Images.SetKeyName(16, "Block Doctor.ico")
        Me.imglstMain.Images.SetKeyName(17, "Active Doctor.ico")
        Me.imglstMain.Images.SetKeyName(18, "Client Update.ico")
        Me.imglstMain.Images.SetKeyName(19, "ProviderReview.ico")
        Me.imglstMain.Images.SetKeyName(20, "ProivderPendingLicense.ico")
        Me.imglstMain.Images.SetKeyName(21, "ProviderDisable.ico")
        Me.imglstMain.Images.SetKeyName(22, "API_Harness.ico")
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.pnlMainMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(294, 167)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(898, 564)
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
        Me.pnlMainMain.Size = New System.Drawing.Size(898, 564)
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
        Me.dgData.Location = New System.Drawing.Point(262, 1)
        Me.dgData.Name = "dgData"
        Me.dgData.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgData.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgData.ReadOnly = True
        Me.dgData.RowHeadersVisible = False
        Me.dgData.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.dgData.SelectionForeColor = System.Drawing.Color.Black
        Me.dgData.Size = New System.Drawing.Size(632, 559)
        Me.dgData.TabIndex = 10
        Me.dgData.Visible = False
        '
        'SplitterMainCategory
        '
        Me.SplitterMainCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.SplitterMainCategory.Location = New System.Drawing.Point(261, 1)
        Me.SplitterMainCategory.Name = "SplitterMainCategory"
        Me.SplitterMainCategory.Size = New System.Drawing.Size(1, 559)
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
        Me.trvCategory.Size = New System.Drawing.Size(260, 559)
        Me.trvCategory.TabIndex = 8
        Me.trvCategory.Visible = False
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(1, 560)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(893, 1)
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
        Me.Label26.Size = New System.Drawing.Size(1, 560)
        Me.Label26.TabIndex = 13
        Me.Label26.Text = "label4"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(894, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 560)
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
        Me.Label28.Size = New System.Drawing.Size(895, 1)
        Me.Label28.TabIndex = 11
        Me.Label28.Text = "label1"
        '
        'txtInstringSearch
        '
        Me.txtInstringSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtInstringSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstringSearch.ForeColor = System.Drawing.Color.Black
        Me.txtInstringSearch.Location = New System.Drawing.Point(710, 1)
        Me.txtInstringSearch.Name = "txtInstringSearch"
        Me.txtInstringSearch.Size = New System.Drawing.Size(133, 22)
        Me.txtInstringSearch.TabIndex = 1
        '
        'btnShowAudit
        '
        Me.btnShowAudit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowAudit.BackColor = System.Drawing.Color.Transparent
        Me.btnShowAudit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnShowAudit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnShowAudit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnShowAudit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowAudit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowAudit.Location = New System.Drawing.Point(858, 1)
        Me.btnShowAudit.Name = "btnShowAudit"
        Me.btnShowAudit.Size = New System.Drawing.Size(58, 22)
        Me.btnShowAudit.TabIndex = 4
        Me.btnShowAudit.Text = "Show"
        Me.btnShowAudit.UseVisualStyleBackColor = False
        '
        'lblSearch
        '
        Me.lblSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(650, 5)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSearch.Size = New System.Drawing.Size(56, 14)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAuditCategory
        '
        Me.cmbAuditCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbAuditCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAuditCategory.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAuditCategory.Location = New System.Drawing.Point(78, 0)
        Me.cmbAuditCategory.Name = "cmbAuditCategory"
        Me.cmbAuditCategory.Size = New System.Drawing.Size(150, 22)
        Me.cmbAuditCategory.TabIndex = 8
        Me.cmbAuditCategory.Visible = False
        '
        'lblAuditCategory
        '
        Me.lblAuditCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblAuditCategory.AutoSize = True
        Me.lblAuditCategory.BackColor = System.Drawing.Color.Transparent
        Me.lblAuditCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuditCategory.Location = New System.Drawing.Point(1, 3)
        Me.lblAuditCategory.Name = "lblAuditCategory"
        Me.lblAuditCategory.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.lblAuditCategory.Size = New System.Drawing.Size(77, 18)
        Me.lblAuditCategory.TabIndex = 7
        Me.lblAuditCategory.Text = "Category :"
        Me.lblAuditCategory.Visible = False
        '
        'dtTo
        '
        Me.dtTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(455, 1)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(104, 22)
        Me.dtTo.TabIndex = 5
        '
        'lblTo
        '
        Me.lblTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.Color.Transparent
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
        Me.dtFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(295, 1)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(104, 22)
        Me.dtFrom.TabIndex = 3
        '
        'lblFrom
        '
        Me.lblFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
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
        Me.lblMainTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainTop.Location = New System.Drawing.Point(63, 1)
        Me.lblMainTop.Name = "lblMainTop"
        Me.lblMainTop.Size = New System.Drawing.Size(294, 23)
        Me.lblMainTop.TabIndex = 0
        Me.lblMainTop.Text = "     "
        Me.lblMainTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtngloEMRGroups, Me.tsbtnWindowsGroupsUsers, Me.tsbtnUserMGNT, Me.tsbtnClinic, Me.tsbtnProvider, Me.tsbtnMachine, Me.tsbtnLSAssociation, Me.tsbtnClaimValidationSetting, Me.tsbtnSettings, Me.tsbtnRxReportDesigner, Me.tsbtnAuditReport, Me.tsbtnArchivedReport, Me.tsbtnArchiveAudit, Me.tsbtnRestoreArchive, Me.tsbtnStartupSettings, Me.tsbtnSyncTime, Me.tsbtnLockScreen, Me.tsbtnLogout, Me.tsbtnUserGuide, Me.tsbtnAboutUs, Me.tsbtnClose})
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
        Me.tsbtnLSAssociation.Visible = False
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
        Me.tsbtnClaimValidationSetting.Visible = False
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
        '
        'tsbtnSyncTime
        '
        Me.tsbtnSyncTime.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnSyncTime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.tsbtnSyncTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnSyncTime.Image = CType(resources.GetObject("tsbtnSyncTime.Image"), System.Drawing.Image)
        Me.tsbtnSyncTime.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnSyncTime.Name = "tsbtnSyncTime"
        Me.tsbtnSyncTime.Size = New System.Drawing.Size(117, 50)
        Me.tsbtnSyncTime.Tag = "SyncTime"
        Me.tsbtnSyncTime.Text = "Synchronize Time"
        Me.tsbtnSyncTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnSyncTime.ToolTipText = "Synchronize Time"
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
        Me.tsbtnLogout.Size = New System.Drawing.Size(57, 50)
        Me.tsbtnLogout.Tag = "LogOut"
        Me.tsbtnLogout.Text = "LogOut"
        Me.tsbtnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnLogout.ToolTipText = "Log Out"
        '
        'tsbtnUserGuide
        '
        Me.tsbtnUserGuide.BackColor = System.Drawing.Color.Transparent
        Me.tsbtnUserGuide.BackgroundImage = CType(resources.GetObject("tsbtnUserGuide.BackgroundImage"), System.Drawing.Image)
        Me.tsbtnUserGuide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsbtnUserGuide.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnUserGuide.Image = CType(resources.GetObject("tsbtnUserGuide.Image"), System.Drawing.Image)
        Me.tsbtnUserGuide.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnUserGuide.Name = "tsbtnUserGuide"
        Me.tsbtnUserGuide.Size = New System.Drawing.Size(75, 50)
        Me.tsbtnUserGuide.Tag = "User Guide"
        Me.tsbtnUserGuide.Text = "User Guide"
        Me.tsbtnUserGuide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnUserGuide.ToolTipText = "User Guide"
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
        Me.SplitterMain.Location = New System.Drawing.Point(290, 56)
        Me.SplitterMain.Name = "SplitterMain"
        Me.SplitterMain.Size = New System.Drawing.Size(4, 675)
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
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(294, 56)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(898, 56)
        Me.pnl_tlsp_Top.TabIndex = 19
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label33.Location = New System.Drawing.Point(1, 52)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(893, 1)
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
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnModify, Me.btnDelete, Me.btnRefresh, Me.btnATNALog, Me.btnAuditLogTampering, Me.btnViewHistory, Me.btnClose})
        Me.tstrip.Location = New System.Drawing.Point(1, 1)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(893, 52)
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
        'btnATNALog
        '
        Me.btnATNALog.BackColor = System.Drawing.Color.Transparent
        Me.btnATNALog.BackgroundImage = CType(resources.GetObject("btnATNALog.BackgroundImage"), System.Drawing.Image)
        Me.btnATNALog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnATNALog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnATNALog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnATNALog.Image = CType(resources.GetObject("btnATNALog.Image"), System.Drawing.Image)
        Me.btnATNALog.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnATNALog.Name = "btnATNALog"
        Me.btnATNALog.Size = New System.Drawing.Size(71, 49)
        Me.btnATNALog.Tag = "ATNA Log"
        Me.btnATNALog.Text = "&ATNA Log"
        Me.btnATNALog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnATNALog.Visible = False
        '
        'btnAuditLogTampering
        '
        Me.btnAuditLogTampering.BackColor = System.Drawing.Color.Transparent
        Me.btnAuditLogTampering.BackgroundImage = CType(resources.GetObject("btnAuditLogTampering.BackgroundImage"), System.Drawing.Image)
        Me.btnAuditLogTampering.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAuditLogTampering.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAuditLogTampering.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAuditLogTampering.Image = CType(resources.GetObject("btnAuditLogTampering.Image"), System.Drawing.Image)
        Me.btnAuditLogTampering.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAuditLogTampering.Name = "btnAuditLogTampering"
        Me.btnAuditLogTampering.Size = New System.Drawing.Size(131, 49)
        Me.btnAuditLogTampering.Tag = "Tampered"
        Me.btnAuditLogTampering.Text = "View Tampered Log"
        Me.btnAuditLogTampering.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAuditLogTampering.Visible = False
        '
        'btnViewHistory
        '
        Me.btnViewHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewHistory.Image = CType(resources.GetObject("btnViewHistory.Image"), System.Drawing.Image)
        Me.btnViewHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnViewHistory.Name = "btnViewHistory"
        Me.btnViewHistory.Size = New System.Drawing.Size(88, 49)
        Me.btnViewHistory.Tag = "View History"
        Me.btnViewHistory.Text = "&View History"
        Me.btnViewHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnViewHistory.ToolTipText = "View History"
        Me.btnViewHistory.Visible = False
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
        Me.Label29.Size = New System.Drawing.Size(893, 1)
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
        Me.Label34.Location = New System.Drawing.Point(894, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 53)
        Me.Label34.TabIndex = 13
        Me.Label34.Text = "label3"
        '
        'pnlMainTop
        '
        Me.pnlMainTop.Controls.Add(Me.Panel21)
        Me.pnlMainTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMainTop.Location = New System.Drawing.Point(294, 112)
        Me.pnlMainTop.Name = "pnlMainTop"
        Me.pnlMainTop.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlMainTop.Size = New System.Drawing.Size(898, 28)
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
        Me.Panel21.Size = New System.Drawing.Size(895, 25)
        Me.Panel21.TabIndex = 19
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label44.Location = New System.Drawing.Point(1, 24)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(893, 1)
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
        Me.Label46.Location = New System.Drawing.Point(894, 1)
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
        Me.Label30.Size = New System.Drawing.Size(895, 1)
        Me.Label30.TabIndex = 9
        Me.Label30.Text = "label2"
        '
        'pnlMainMainTop
        '
        Me.pnlMainMainTop.Controls.Add(Me.Panel8)
        Me.pnlMainMainTop.Controls.Add(Me.pnlClientUpdateDetailsFilter)
        Me.pnlMainMainTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMainMainTop.Location = New System.Drawing.Point(294, 140)
        Me.pnlMainMainTop.Name = "pnlMainMainTop"
        Me.pnlMainMainTop.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlMainMainTop.Size = New System.Drawing.Size(898, 27)
        Me.pnlMainMainTop.TabIndex = 82
        '
        'Panel8
        '
        Me.Panel8.AutoSize = True
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImage = CType(resources.GetObject("Panel8.BackgroundImage"), System.Drawing.Image)
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.Label24)
        Me.Panel8.Controls.Add(Me.Label21)
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
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Controls.Add(Me.Label23)
        Me.Panel8.Controls.Add(Me.optSelfNotesStatus)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(895, 24)
        Me.Panel8.TabIndex = 19
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(1, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(893, 1)
        Me.Label24.TabIndex = 5
        Me.Label24.Text = "label1"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(1, 23)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(893, 1)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "label2"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 24)
        Me.Label22.TabIndex = 7
        Me.Label22.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(894, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 24)
        Me.Label23.TabIndex = 6
        Me.Label23.Text = "label3"
        '
        'pnlClientUpdateDetailsFilter
        '
        Me.pnlClientUpdateDetailsFilter.BackColor = System.Drawing.Color.Transparent
        Me.pnlClientUpdateDetailsFilter.BackgroundImage = CType(resources.GetObject("pnlClientUpdateDetailsFilter.BackgroundImage"), System.Drawing.Image)
        Me.pnlClientUpdateDetailsFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.btnSearchClientUpdate)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.txtSearchClientUpdate)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.Label32)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.cmbClientMachineName)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.lblClientMachineName)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.Label38)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.Label39)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.Label40)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.Label41)
        Me.pnlClientUpdateDetailsFilter.Controls.Add(Me.RadioButton2)
        Me.pnlClientUpdateDetailsFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlClientUpdateDetailsFilter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlClientUpdateDetailsFilter.Location = New System.Drawing.Point(0, 0)
        Me.pnlClientUpdateDetailsFilter.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlClientUpdateDetailsFilter.Name = "pnlClientUpdateDetailsFilter"
        Me.pnlClientUpdateDetailsFilter.Size = New System.Drawing.Size(895, 24)
        Me.pnlClientUpdateDetailsFilter.TabIndex = 20
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
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(259, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(98, 22)
        Me.Label32.TabIndex = 3
        Me.Label32.Text = "Search :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label38.Location = New System.Drawing.Point(1, 23)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(893, 1)
        Me.Label38.TabIndex = 8
        Me.Label38.Text = "label2"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(0, 1)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 23)
        Me.Label39.TabIndex = 7
        Me.Label39.Text = "label4"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label40.Location = New System.Drawing.Point(894, 1)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 23)
        Me.Label40.TabIndex = 6
        Me.Label40.Text = "label3"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(0, 0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(895, 1)
        Me.Label41.TabIndex = 5
        Me.Label41.Text = "label1"
        '
        'RadioButton2
        '
        Me.RadioButton2.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.Location = New System.Drawing.Point(259, 0)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(80, 24)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Text = "Status"
        Me.RadioButton2.UseVisualStyleBackColor = False
        Me.RadioButton2.Visible = False
        '
        'txtPatient
        '
        Me.txtPatient.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPatient.Location = New System.Drawing.Point(129, 3)
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.Size = New System.Drawing.Size(103, 22)
        Me.txtPatient.TabIndex = 1
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(116, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = ":"
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
        'HelpComponent1
        '
        Me.HelpComponent1.Mode = gloEMR.Help.HelpComponent.ProviderMode.Client
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.Panel9.Controls.Add(Me.lblLogTamperedStatus)
        Me.Panel9.Controls.Add(Me.StatusBar1)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel9.Location = New System.Drawing.Point(0, 731)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1192, 22)
        Me.Panel9.TabIndex = 16
        '
        'lblLogTamperedStatus
        '
        Me.lblLogTamperedStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.lblLogTamperedStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.lblLogTamperedStatus.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblLogTamperedStatus.Image = CType(resources.GetObject("lblLogTamperedStatus.Image"), System.Drawing.Image)
        Me.lblLogTamperedStatus.Location = New System.Drawing.Point(1076, 3)
        Me.lblLogTamperedStatus.Name = "lblLogTamperedStatus"
        Me.lblLogTamperedStatus.Size = New System.Drawing.Size(155, 16)
        Me.lblLogTamperedStatus.TabIndex = 41
        Me.lblLogTamperedStatus.TabStop = False
        '
        'frmgloEMRAdmin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1192, 753)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlAudit)
        Me.Controls.Add(Me.pnlMainMainTop)
        Me.Controls.Add(Me.pnlMainTop)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Controls.Add(Me.SplitterMain)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlCommand)
        Me.Controls.Add(Me.Panel9)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "frmgloEMRAdmin"
        Me.Text = "QEMR Admin"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.stbPnlLoginTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stbPnlVersion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stbPnlSQLSeverDatabase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stbPnlBuild, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.pnlMainMainTop.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.pnlClientUpdateDetailsFilter.ResumeLayout(False)
        Me.pnlClientUpdateDetailsFilter.PerformLayout()
        Me.pnlAudit.ResumeLayout(False)
        Me.pnlAudit.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        CType(Me.lblLogTamperedStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    'sarika Audit Log Instr Search
    Private _blnSearch As Boolean = False
    Private _ShowSplashScreen As Boolean = False
    Private gstrHelpProvider As String = "Client"
    '---------
    Dim _CurrentTime As Date
    Private WithEvents timer_1 As New Timer
    Private bIsAuditLogReport As Boolean = False


    Public ReadOnly Property HotKeys() As HotKeyCollection
        Get
            HotKeys = m_hotKeys
        End Get
    End Property

    Private Sub ShowHelp()
       
        Me.HelpComponent1.ShowHelp(Me)

    End Sub

    Private Sub sethelpBuildermode()
        Try


           
            If frmSplash.gstrHelpProvider = "Client" Then
                gloEMR.Help.HelpComponent.blnbuildmode = False
                Me.HelpComponent1.Mode = HelpComponent.ProviderMode.Client
            Else
                Me.HelpComponent1.Mode = HelpComponent.ProviderMode.Builder

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmgloEMRAdmin_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not IsNothing(timer_1) Then
            timer_1.Dispose()
            timer_1 = Nothing
        End If
    End Sub


    Private Sub frmgloEMRAdmin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try



            timer_1.Interval = 300
            UpdateStatusBar()
            ShowLogTamperingStatus()
            '20-Mar-14 Chetan: Resolving resolution issues bugid 65680
            Dim myScreenWidth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth
            Dim myScreenHeight As Integer = System.Windows.SystemParameters.PrimaryScreenHeight
            If Me.Width > myScreenWidth Or Me.Height > myScreenHeight Then
                If myScreenHeight = 768 Then
                    Me.MaximumSize = New System.Drawing.Size(myScreenWidth, (myScreenHeight - 50))
                    Me.AutoScroll = True
                    Panel3.Height = Panel3.Height - 90
                    Panel7.AutoScroll = True
                    Panel3.AutoScroll = True
                End If
            End If


            '-----------------------------------------------
            Me.Cursor = Cursors.WaitCursor

            Call Fill_Admin()
            Call Fill_Audit()

            SetToolStrip()
            'Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
            If gstrAdminFor = "gloEMR" Then
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)

            Else
                Me.Text = "gloPM Admin"
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
                    trvCategory.Width = 200
                End If
            Else
                trvCategory.Width = 200
            End If
            gloRegistrySetting.CloseRegistryKey()

            Me.Cursor = Cursors.Default
            gstrConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()

            trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(2)
            Call ShowAdministrator()

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
                gbln10dot6Version = objSettings.IsNCPDP10dot6Ver

                gstrSTAGING10DOT6PORTALID = objSettings.STAGING10DOT6PORTALID
                gstrSTAGING8DOT1PORTALID = objSettings.STAGING8DOT1PORTALID
                gstrSTAGING10DOT6ACCOUNTID = objSettings.STAGING10DOT6ACCOUNTID
                gstrSSPRODUCTIONPORTALID = objSettings.SSPRODUCTIONPORTALID
                gstrSSPRODUCTIONACCOUNTID = objSettings.SSPRODUCTIONACCOUNTID

                gstrSSPRODUCTION10dot6PORTALID = objSettings.SSPRODUCTION10DOT6PORTALID

                ''Added ServicesDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table 
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
                Else
                    MessageBox.Show("Set services database details in Settings -> server settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                ''Added ServicesDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table   
                gstrgloAusPortalURL = objSettings.gloAusPortalUrl
                gstrDemoNPIs = objSettings.DemoNPIs
            End If
            'code by supriya 11/7/2008

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        ''GLO2012-0016207 : User is unable to log back into the gloSuite from the 'Screen Saver' the after the password is entered the login screen returnes.
        Try
            RemoveHandler Me.HotKeyPressed, AddressOf hotKey_Pressed
        Catch
            ''Some times blank try catch is also required to by pass the  error which occurs some times.
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





    Private Sub UpdateStatusBar()
        StatusBar1.Panels(0).Text = gstrLoginName & " Login Time " & gstrLoginTime
        ''Sandip Darade 20090421
        ''Read application version from assembly
        StatusBar1.Panels(1).Text = gstrVersion
        '' StatusBar1.Panels(1).Text = "Version " & RetrieveVersion()

        stbPnlSQLSeverDatabase.Text = " " & gstrSQLServerName & "   " & gstrDatabaseName
        stbPnlBuild.Text = "Last Modified Date " & File.GetLastWriteTime(Application.StartupPath & "\" & Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName)

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

            

            trvNode = New TreeNode
            With trvNode
                .Text = "gloEMR Groups"
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


            trvNode = New TreeNode
            With trvNode
                '.Text = "Doctor"
                .Text = "Provider"
                .ImageIndex = 6
                .SelectedImageIndex = 6
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
                    .Text = "Provider DIRECT Message – Assign Users to Provider Inboxes"
                    .ImageIndex = 37
                    .SelectedImageIndex = 37
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
                    .ImageIndex = 36
                    .SelectedImageIndex = 36
                End With
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing

                ''Dhruv 20091202
                ''Added the name against the into the treeview
                trvNode = New TreeNode()
                trvNode.Text = "Multiple Database"
                trvNode.ImageIndex = 28
                trvNode.SelectedImageIndex = 28
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing


                'Developer: Mitesh Patel
                'Date:20-Dec-2011'
                'PRD: Lab Usability Admin Setting
                trvNode = New TreeNode()
                trvNode.Text = "Lab User Task"
                trvNode.ImageIndex = 32
                trvNode.SelectedImageIndex = 32
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing


                
            End If
           


            trvNode = New TreeNode
            With trvNode
                .Text = "Tools"
                .ImageIndex = 14
                .SelectedImageIndex = 14
                '.ForeColor = Color.DarkBlue
            End With
            .Nodes.Add(trvNode)
            trvNode = Nothing



          

            trvNode = New TreeNode
            With trvNode
                .Text = "Settings"
                .ImageIndex = 17
                .SelectedImageIndex = 17
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing

            trvNode = New TreeNode
            With trvNode
                .Text = "SSRS Report Settings"
                .ImageIndex = 29
                .SelectedImageIndex = 29
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing

            trvNode = New TreeNode
            With trvNode
                .Text = "Deploy SSRS Reports"
                .ImageIndex = 30
                .SelectedImageIndex = 30
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing
            ' ''Update PayerID 
            'trvNode = New TreeNode
            'With trvNode
            '    .Text = "Update PayerID"
            '    .ImageIndex = 25
            '    .SelectedImageIndex = 25
            'End With
            '.Nodes(1).Nodes.Add(trvNode)
            'trvNode = Nothing

            ''Added By Debasish on 4th Jan 2011.
            trvNode = New TreeNode
            With trvNode
                .Text = "Merge Insurance Plan"
                .ImageIndex = 31
                .SelectedImageIndex = 31
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing
            ''************************************
            trvNode = New TreeNode
            With trvNode
                .Text = "Advance Merge Insurance"
                .ImageIndex = 39
                .SelectedImageIndex = 39
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

            'Added by Shirish K
            'If GetECGInterfaceStatus() Then
            trvNode = New TreeNode
            With trvNode
                '.Text = "Heart Centric Users Management"
                .Text = "Device User Management"
                .ImageIndex = 15
                .SelectedImageIndex = 15
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing
            'End If
            'End of Shirish K


            trvNode = New TreeNode
            With trvNode
                '.Text = "Heart Centric Users Management"
                .Text = "Mobile User Management"
                .ImageIndex = 33
                .SelectedImageIndex = 33
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing

            ''Added for Patient portal dnyamics form on 20130301
            If getHealthFormSetting() Then
                trvNode = New TreeNode
                With trvNode
                    .Text = "Online Patient Form"
                    .ImageIndex = 35
                    .SelectedImageIndex = 35
                End With
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing
                ''End
            End If

            If getHealthFormSetting() Then
                trvNode = New TreeNode
                With trvNode
                    .Text = "Portal Message Templates"
                    .ImageIndex = 38
                    .SelectedImageIndex = 38
                End With
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing
                ''End
            End If

            'Added by Rohit kanugo
            trvNode = New TreeNode
            With trvNode
                .Text = "User Custom Link"
                .ImageIndex = 16
                .SelectedImageIndex = 16
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing
            'End of Rohit kanugo

            trvNode = New TreeNode
            With trvNode
                .Text = "TaxID Setup"
                .ImageIndex = 40
                .SelectedImageIndex = 40
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing

            trvNode = New TreeNode
            With trvNode
                .Text = "API Access"
                .ImageIndex = 41
                .SelectedImageIndex = 41
            End With
            .Nodes(0).Nodes.Add(trvNode)
            trvNode = Nothing


            ''code added for site prefix by pradeep on 29/06/2010
            Dim oResult As New Object
            Dim ogloSettings As New gloSettings.GeneralSettings(gloEMRAdmin.mdlGeneral.GetConnectionString)
            ogloSettings.GetSetting("UseSitePrefix", oResult)
            Dim _UseSitePrefix As Int32 = 0
            If oResult.ToString() = "1" Then '1 means true
                _UseSitePrefix = Convert.ToInt32(oResult)
                trvNode = New TreeNode()
                trvNode.Text = "Site Prefix"
                trvNode.ImageIndex = 27
                trvNode.SelectedImageIndex = 27
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing
            End If

            trvNode = New TreeNode
            With trvNode
                .Text = "Setup Unscheduled Care Templates"
                .ImageIndex = 34
                .SelectedImageIndex = 34
            End With
            .Nodes(1).Nodes.Add(trvNode)
            trvNode = Nothing

            If (gstrAdminFor = "gloPM") Then

                ''Claim Validation settings 
                trvNode = New TreeNode
                With trvNode
                    .Text = "Claim Validation Settings "
                    .ImageIndex = 16
                    .SelectedImageIndex = 16
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

                trvNode = New TreeNode
                With trvNode
                    .Text = "Clearinghouse"
                    .ImageIndex = 13
                    .SelectedImageIndex = 13
                End With
                .Nodes(0).Nodes.Add(trvNode)
                trvNode = Nothing
                'If (gstrAdminFor = "gloPM") Then
                trvNode = New TreeNode
                With trvNode
                    .Text = "Import Fee Shedule"
                    .ImageIndex = 26
                    .SelectedImageIndex = 26
                End With
                .Nodes(1).Nodes.Add(trvNode)
                trvNode = Nothing
            End If
            .ExpandAll()
            .SelectedNode = .Nodes(0)
            .EndUpdate()
        End With
    End Sub

    

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
    Private Sub Fill_CategoryClientMachines()
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
            With trvGroups
                .Text = "Client Settings"
                .ImageIndex = 6
                .SelectedImageIndex = 6
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

    Private Sub Fill_ClientUdpateMachinesDetails()
        pnlCommandButtons.Visible = False
        pnl_tlsp_Top.Visible = False
        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvGroups As TreeNode
            trvGroups = New TreeNode
            With trvGroups
                .Text = "Client Updates Details"
                .ImageIndex = 18
                .SelectedImageIndex = 18
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
        btnDelete.Text = "Block"
        btnDelete.ToolTipText = "Block"

        trvCategory.Nodes.Clear()

        Dim oProvider As New clsProvider
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
        oDisabledProvidersTreeNode.ImageIndex = 21
        oDisabledProvidersTreeNode.SelectedImageIndex = 21
        oDisabledProvidersTreeNode.ForeColor = Color.FromArgb(31, 73, 125)

        'dtActiveProvider = oProvider.GetProvider(False, False)
        'dtBlockedProvider = oProvider.GetProvider(True, False)
        'dtPendingProvider = oProvider.GetProvider(False, True)

        dtActiveProvider = oProvider.GetProvider_withENM(clsProvider.enmAUSStatus.Active)
        dtBlockedProvider = oProvider.GetProvider_withENM(clsProvider.enmAUSStatus.BlockedProviders)
        dtPendingProvider = oProvider.GetProvider_withENM(clsProvider.enmAUSStatus.PendingForLicense)
        dtApprovedProvider = oProvider.GetProvider_withENM(clsProvider.enmAUSStatus.PendingForReview)
        dtDisabledProvider = oProvider.GetProvider_withENM(clsProvider.enmAUSStatus.DisabledProviders)

        Dim oNode As TreeNode
        If IsNothing(dtActiveProvider) = False Then
            For i As Integer = 0 To dtActiveProvider.Rows.Count - 1
                oNode = New TreeNode
                ''Added by Mayuri:20100517-Case No:#4942
                If dtActiveProvider.Rows(i)("MiddleName") <> "" Then
                    oNode.Text = dtActiveProvider.Rows(i)("FirstName") & " " & dtActiveProvider.Rows(i)("MiddleName") & " " & dtActiveProvider.Rows(i)("LastName")
                Else
                    oNode.Text = dtActiveProvider.Rows(i)("FirstName") & " " & dtActiveProvider.Rows(i)("LastName")
                End If
                'oNode.Text = dtActiveProvider.Rows(i)("FirstName") & dtActiveProvider.Rows(i)("MiddleName") & dtActiveProvider.Rows(i)("LastName")
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
                ''oNode.Text = dtBlockedProvider.Rows(i)("ProviderName") ''Commented by Mayuri 20100518

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
        ''' trvCategory.Sort()
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
            With trvGroups
                .Text = "gloEMR User Groups"
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

    'Added for User groups 
    'Sandip Darade 7th Feb 2007
    Private Sub Fill_CategoryUserGroups()
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
                    oNode.ForeColor = Color.FromArgb(31, 73, 125)
                    trvCategory.Nodes(0).Nodes.Add(oNode)
                Next
            End If
            .ExpandAll()
            .EndUpdate()
        End With

    End Sub
    ''code  added by pradeep on 16/06/2010 for prefix
    Private Sub Fill_Prefix()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "New"
        btnModify.Visible = True
        btnModify.Text = "Modify"
        btnDelete.Visible = True
        btnDelete.Text = "Delete"
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
        grdPrefix.HeaderText = "Site Prefix"
        grdPrefix.Alignment = HorizontalAlignment.Left
        grdPrefix.MappingName = "sPrefix"
        grdPrefix.NullText = ""
        grdPrefix.Width = 0.2 * dgData.Width


        grdTableStyle.GridColumnStyles.Clear()
        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdprefixID, grdServer, grdDatabase, grdPrefix})


        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyle)


    End Sub

    Private Sub Fill_CategorygloEMRUsers()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "New"
        btnModify.Visible = True
        btnModify.Text = "Modify"
        btnDelete.Visible = True
        btnDelete.Text = "Block"
        btnDelete.ToolTipText = "Block"

        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvChild As TreeNode
            trvChild = New TreeNode
            With trvChild
                .Text = "QEMR Users"
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

        If clClinics.Count = 0 Then
            ''If Clinic information is not present then only allow to add Clinic
            btnNew.Visible = True
            btnNew.Text = "New"
            btnModify.Visible = False
            btnModify.Text = "Modify"
        End If

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
        btnNew.Text = "New"
        btnModify.Visible = True
        btnModify.Text = "Modify"
        btnDelete.Visible = True
        btnDelete.Text = "Delete"
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
        btnNew.Text = "New"
        btnModify.Visible = True
        btnModify.Text = "Modify"
        btnDelete.Visible = True
        btnDelete.Text = "Delete"
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
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes.Add(trvChild)
            trvChild = Nothing


            trvChild = New TreeNode
            With trvChild
                .Text = "Disc"
                .ImageIndex = 9
                .SelectedImageIndex = 9
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "Tape"
                .ImageIndex = 9
                .SelectedImageIndex = 9
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = Nothing

            trvChild = New TreeNode
            With trvChild
                .Text = "All"
                .ImageIndex = 9
                .SelectedImageIndex = 9
                .ForeColor = Color.FromArgb(31, 73, 125)
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
        btnNew.Text = "New"
        btnModify.Visible = True
        btnModify.Text = "Modify"
        btnDelete.Visible = True
        btnDelete.Text = "Delete"
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
                .ForeColor = Color.FromArgb(31, 73, 125)
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
                    .ForeColor = Color.FromArgb(31, 73, 125)
                End With
                .Nodes(0).Nodes.Add(trvChild)
                trvChild = Nothing
            Next
            Dim trvChildNode As New TreeNode
            With trvChildNode
                .Text = "All"
                .ImageIndex = 11
                .SelectedImageIndex = 11
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChildNode)

            .ExpandAll()
            .EndUpdate()
        End With


    End Sub

    Private Sub Fill_AuditCategories()


        Dim objAudit As New clsAudit
        Dim clAudit As New Collection
        Dim nCount As Integer

        Try

            cmbAuditCategory.Items.Clear()
            clAudit = objAudit.Fill_AuditCategory()

            cmbAuditCategory.Items.Add("All")

            For nCount = 1 To clAudit.Count
                If Not IsDBNull(clAudit.Item(nCount)) Then
                    If clAudit.Item(nCount) <> "" Then
                        cmbAuditCategory.Items.Add(clAudit.Item(nCount))
                    End If

                End If
            Next

            cmbAuditCategory.Text = "All"
            'cmbAuditCategory.SelectedIndex = 0

            objAudit = Nothing
            clAudit = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Fill_ArchivedAuditCategories()

        Dim objAudit As New clsAudit
        Dim clAudit As New Collection
        Dim nCount As Integer

        Try

            cmbAuditCategory.Items.Clear()
            clAudit = objAudit.Fill_ArchivedAuditCategory()

            If Not IsNothing(clAudit) Then
                For nCount = 1 To clAudit.Count
                    cmbAuditCategory.Items.Add(clAudit.Item(nCount))
                Next
            Else
                Exit Sub
            End If

            cmbAuditCategory.Items.Add("All")
            cmbAuditCategory.Text = "All"
            'cmbAuditCategory.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

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

    'Fill Users for Audit Reports
    Private Sub Fill_CategoryUsers()

        Try


            With trvCategory
                .BeginUpdate()
                .Sorted = False
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

                'Dim trvRootNode As New TreeNode
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
                trvChildNode = Nothing

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
                dtUsers = objAudit.Fill_ArchivedUsers(False)

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
                    'Shubhangi 20091204
                    'to avoid an exception due to Incorrect connection.
                Else
                    Exit Sub
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
                'dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Text)
                dtAuditReports = objAuditReports.NewRetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.Text, trvCategory.SelectedNode.Text, txtInstringSearch.Text)

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
                .Format = "MM/dd/yyyy h:mm:ss tt"
            End With

            Dim grdColSoftwareComp As New DataGridTextBoxColumn
            With grdColSoftwareComp
                .HeaderText = "Software Component"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("SoftwareComponent").ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColActivityCategory As New DataGridTextBoxColumn
            With grdColActivityCategory
                .HeaderText = "Category"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("Category").ColumnName
                .NullText = ""
                .Width = 125
            End With

            Dim grdColActivityAction As New DataGridTextBoxColumn
            With grdColActivityAction
                .HeaderText = "Action"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("Action").ColumnName
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
                .Width = 0
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
                .HeaderText = "User Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("UserName").ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColMachine As New DataGridTextBoxColumn
            With grdColMachine
                .HeaderText = "Machine Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("MachineName").ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColTransactionID As New DataGridTextBoxColumn
            With grdColTransactionID
                .HeaderText = "TransactionID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("nTransactionID").ColumnName
                .NullText = ""
                .Width = 0
            End With

            Dim grdColModuleType As New DataGridTextBoxColumn
            With grdColModuleType
                .HeaderText = "Activity Module"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("sActivityModule").ColumnName
                .NullText = ""
                .Width = 0
            End With



            Dim grdColPatientID As New DataGridTextBoxColumn
            With grdColPatientID
                .HeaderText = "PatientID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("nPatientID").ColumnName
                .NullText = ""
                .Width = 0
            End With

            'Added Code for Audit LOG Enhancement
            Dim grdColLocalMachineIP As New DataGridTextBoxColumn
            With grdColLocalMachineIP
                .HeaderText = "Machine IP"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("Machine IP").ColumnName
                .NullText = ""
                .Width = 100
            End With


            Dim grdColRemoteMachineName As New DataGridTextBoxColumn
            With grdColRemoteMachineName
                .HeaderText = "Remote Machine Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("Remote Machine Name").ColumnName
                .NullText = ""
                .Width = 150
            End With

            Dim grdColRemoteMachineIP As New DataGridTextBoxColumn
            With grdColRemoteMachineIP
                .HeaderText = "Remote Machine IP"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("Remote Machine IP").ColumnName
                .NullText = ""
                .Width = 150
            End With

            Dim grdColRemoteUserName As New DataGridTextBoxColumn
            With grdColRemoteUserName
                .HeaderText = "Remote User Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("Remote User Name").ColumnName
                .NullText = ""
                .Width = 150
            End With


            Dim grdColDomain As New DataGridTextBoxColumn
            With grdColDomain
                .HeaderText = "Domain"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns("Domain").ColumnName
                .NullText = ""
                .Width = 150
            End With






            'grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColAuditReportID, grdColActivityDate, grdColActivityCategory, grdColDescription, grdColPatientCode, grdColPatient, grdColUser, grdColMachine, grdColSoftwareComp, grdColOutcome})
            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColAuditReportID, grdColActivityDate, grdColDescription, grdColUser, grdColMachine, grdColActivityAction, grdColActivityCategory, grdColPatientCode, grdColSoftwareComp, grdColOutcome, grdColPatient, grdColTransactionID, grdColModuleType, grdColPatientID, grdColLocalMachineIP, grdColRemoteMachineName, grdColRemoteMachineIP, grdColRemoteUserName, grdColDomain})
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
        conn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
        Dim sSeparator As String = ""
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
                    .Filter = "Text File(*.txt)|*.txt|Comma Separated File(*.csv)|*.csv"
                    .OverwritePrompt = True
                    .ShowHelp = False
                    .Title = "Select Path to store Audit Log"
                End With
                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                    strFileName = SaveFileDialog1.FileName

                    If Path.GetExtension(strFileName).ToLower() = ".csv" Then
                        sSeparator = "|"
                    Else
                        sSeparator = vbTab
                    End If                
                Else
                    Exit Sub
                    '----------------------------
                End If



                If System.IO.File.Exists(strFileName) = True Then System.IO.File.Delete(strFileName)
                'Export Data in Text File
                Dim nCount As Integer
                Dim objFile As New System.IO.StreamWriter(strFileName)
                Dim strAuditLine As String

                For Each DataColumn As DataColumn In dtAudits.Columns
                    If dtAudits.Columns.IndexOf(DataColumn) > 0 AndAlso dtAudits.Columns.IndexOf(DataColumn) < 10 Then
                        strAuditLine = strAuditLine & DataColumn.Caption & IIf(dtAudits.Columns.IndexOf(DataColumn) < 9, sSeparator, Nothing)
                    End If
                Next

                objFile.WriteLine(strAuditLine)

                For nCount = 0 To dtAudits.Rows.Count - 1
                    strAuditLine = ""
                    If IsDBNull(dtAudits.Rows(nCount).Item(1)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(1)                        
                    End If
                    strAuditLine = strAuditLine & sSeparator

                    If IsDBNull(dtAudits.Rows(nCount).Item(2)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(2)                        
                    End If
                    strAuditLine = strAuditLine & sSeparator

                    If IsDBNull(dtAudits.Rows(nCount).Item(3)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(3)                        
                    End If
                    strAuditLine = strAuditLine & sSeparator

                    If IsDBNull(dtAudits.Rows(nCount).Item(4)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(4)
                    End If
                    strAuditLine = strAuditLine & sSeparator

                    If IsDBNull(dtAudits.Rows(nCount).Item(5)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(5)                        
                    End If
                    strAuditLine = strAuditLine & sSeparator

                    If IsDBNull(dtAudits.Rows(nCount).Item(6)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(6)                        
                    End If
                    strAuditLine = strAuditLine & sSeparator

                    'sarika 27th apr 2007

                    If IsDBNull(dtAudits.Rows(nCount).Item(7)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(7)                        
                    End If
                    strAuditLine = strAuditLine & sSeparator

                    If IsDBNull(dtAudits.Rows(nCount).Item(8)) = False Then
                        strAuditLine = strAuditLine & dtAudits.Rows(nCount).Item(8)                        
                    End If
                    strAuditLine = strAuditLine & sSeparator

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
            'dtAuditReports = objAuditReports.RetrieveArchivedAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Tag)
            'DATABASE NAME PARAMETER IS ADDED IN TO S.P. FOR RETRIVING PATIENTCODE , PATIENT NAME FROM DIFFERENT DB
            dtAuditReports = objAuditReports.RetrieveArchivedAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Tag, gstrDatabaseName, txtInstringSearch.Text)
        Else
            If IsNumeric(txtPatient.Text) = True Then
                Dim isvalid As Integer = 0
                isvalid = IsPatientExists("select count(*) from Patient where nPatientID=" & txtPatient.Text.Trim())
                If isvalid > 0 Then
                    '******By Sandip Deshmukh 24 Oct 07 12.14PM Bug# 242
                    '******in following line the last param converted to Clng as Cint through overflow exception
                    dtAuditReports = objAuditReports.RetrieveArchivedAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbAuditCategory.SelectedItem, trvCategory.SelectedNode.Tag, gstrDatabaseName, CLng(txtPatient.Text))
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
        If Not IsNothing(dtAuditReports) Then


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
            ' Dim grdTableStyle As New clsDataGridTableStyle(dtAuditReports, True)
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
                .Width = 0
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
                .HeaderText = "Machine Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(6).ColumnName
                .NullText = ""
                .Width = 120
            End With

            Dim grdColSoftwareComponent As New DataGridTextBoxColumn
            With grdColSoftwareComponent
                .HeaderText = "Software Component"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(7).ColumnName
                .NullText = ""
                .Width = 150
            End With

            Dim grdColOutcome As New DataGridTextBoxColumn
            With grdColOutcome
                .HeaderText = "Outcome"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(8).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdPatientCode As New DataGridTextBoxColumn
            With grdPatientCode
                .HeaderText = "Patient Code"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(9).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdFirstName As New DataGridTextBoxColumn
            With grdFirstName
                .HeaderText = "First Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(10).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdLastName As New DataGridTextBoxColumn
            With grdLastName
                .HeaderText = "Last Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(11).ColumnName
                .NullText = ""
                .Width = 100
            End With

            'Added Code for Audit LOG Enhancement
            Dim grdColLocalMachineIP As New DataGridTextBoxColumn
            With grdColLocalMachineIP
                .HeaderText = "Machine IP"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(12).ColumnName
                .NullText = ""
                .Width = 100
            End With


            Dim grdColRemoteMachineName As New DataGridTextBoxColumn
            With grdColRemoteMachineName
                .HeaderText = "Remote Machine Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(13).ColumnName
                .NullText = ""
                .Width = 150
            End With

            Dim grdColRemoteMachineIP As New DataGridTextBoxColumn
            With grdColRemoteMachineIP
                .HeaderText = "Remote Machine IP"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(14).ColumnName
                .NullText = ""
                .Width = 150
            End With

            Dim grdColRemoteUserName As New DataGridTextBoxColumn
            With grdColRemoteUserName
                .HeaderText = "Remote User Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(15).ColumnName
                .NullText = ""
                .Width = 150
            End With


            Dim grdColDomain As New DataGridTextBoxColumn
            With grdColDomain
                .HeaderText = "Domain"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(16).ColumnName
                .NullText = ""
                .Width = 150
            End With


            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColAuditReportID, grdColActivityDate, grdColDescription, grdColUser, grdColMachine, grdColActivityCategory, grdColPatient, grdPatientCode, grdColSoftwareComponent, grdColOutcome, grdColLocalMachineIP, grdColRemoteMachineName, grdColRemoteMachineIP, grdColRemoteUserName, grdColDomain}) ', grdFirstName, grdLastName
            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)
        End If
    End Sub

    Private Sub Fill_DetailsgloEMRGroups()
        If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then
            Dim objgloEMRGroups As New clsUserGroups
            Dim dtgloEMRGroups As New DataTable
            dtgloEMRGroups = objgloEMRGroups.PopulateUserGroupsRights(Trim(trvCategory.SelectedNode.Text))
            If dtgloEMRGroups.Rows.Count <> 0 Then
                dgData.DataSource = dtgloEMRGroups
                dgData.CaptionText = "gloEMR Groups"
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
            dtLabUsers = dtgloEMRGroups

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
        btnViewHistory.Visible = True
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
                    btnDelete.Text = "Block"
                    btnDelete.ToolTipText = "Block"
                Case "Blocked Users"
                    dtUsers = objUsers.PopulateUsers(clsUsers.enmUsersType.NonActive)
                    If dtUsers.Rows.Count < 1 Then
                        btnViewHistory.Visible = False
                    Else
                        btnViewHistory.Visible = True
                    End If

                    btnDelete.Visible = True
                    btnDelete.Text = "Unblock"
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

            If Not IsNothing(dtClients) AndAlso Not (dtClients.Rows.Count <> 0) Then

                MessageBox.Show(Me, "Selected machine does not exists, Please add this again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim e As EventArgs
                btnRefresh_Click(btnRefresh, e)
                Exit Sub
            End If
            drRow = dtClientSettings.NewRow
            drRow(0) = "Voice Enabled"
            drRow(1) = dtClients.Rows(0).Item("VoiceEnabled")
            dtClientSettings.Rows.Add(drRow)

            drRow = dtClientSettings.NewRow
            drRow(0) = "Scan Enabled"
            drRow(1) = dtClients.Rows(0).Item("ScanEnabled")
            dtClientSettings.Rows.Add(drRow)

            dgData.Tag = dtClients.Rows(0).Item(0)

            '------------------------------------
            'Sarika 21st April 2007
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The details of Client : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '------------------------------------
        End If
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

                ' MessageBox.Show(Me, "Selected machine does not exists, Please add this again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
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




    Private Function Fill_DetailsProviders() As String

        Dim strProviderName As String

        If IsNothing(trvCategory.SelectedNode) Then
            dgData.DataSource = Nothing
            Exit Function
        End If

        If trvCategory.SelectedNode.Level <> 1 Then
            dgData.DataSource = Nothing
            Exit Function
        End If

        If trvCategory.SelectedNode.Parent.Tag = "Active" Then
            btnDelete.Text = "Block"
            btnDelete.ToolTipText = "Block"
            btnDelete.Enabled = True
        ElseIf trvCategory.SelectedNode.Parent.Tag = "Blocked" Then
            btnDelete.Text = "Unblock"
            btnDelete.ToolTipText = "Unblock"
            btnDelete.Enabled = True
        ElseIf trvCategory.SelectedNode.Parent.Tag = "pending" Or trvCategory.SelectedNode.Parent.Tag = "review" Then
            btnDelete.Enabled = False
        End If


        Dim dtProvider As New DataTable
        Dim objProvider As New clsProvider
        dtProvider = objProvider.ScanProvider(Convert.ToInt64(trvCategory.SelectedNode.Tag))


        Dim dtClientSettings As New DataTable("Doctor")
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

                If dtProvider.Columns(iCol).ColumnName = "Name" Then
                    strProviderName = dtProvider.Rows(0)(iCol).ToString.Trim
                End If
            Next



            drRow = dtClientSettings.NewRow
            drRow(0) = "Doctor Type"
            drRow(1) = objProvider.GetProviderType(dtProvider.Rows(0).Item(dtProvider.Columns.Count - 1))
            dtClientSettings.Rows.Add(drRow)


            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "Details of Doctor : " + trvCategory.SelectedNode.Text + " viewed.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing

            objProvider = Nothing
        End If

        Return strProviderName

    End Function



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

                If (Convert.ToString(dtClinic.Rows(0).Item(8)) <> "(___)___-____") Then

                    drRow(1) = dtClinic.Rows(0).Item(8)
                Else

                    drRow(1) = ""
                End If
                dtClientSettings.Rows.Add(drRow)

                drRow = dtClientSettings.NewRow
                drRow(0) = "Mobile No"
                If (Convert.ToString(dtClinic.Rows(0).Item(9)) <> "(___)___-____") Then

                    drRow(1) = dtClinic.Rows(0).Item(9)
                Else

                    drRow(1) = ""
                End If
                dtClientSettings.Rows.Add(drRow)
                drRow = dtClientSettings.NewRow
                drRow(0) = "FAX"
                'code added  by dipak 20090914 to show ""  string in  dgData if fax value is=(___)___-____"
                If (Convert.ToString(dtClinic.Rows(0).Item(8)) <> "(___)___-____") Then
                    drRow(1) = dtClinic.Rows(0).Item(10)
                Else
                    drRow(1) = ""
                End If
                'end code by dipak 20090914
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
                dgData.CaptionText = "Provider Type Details"

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
                    'drRow(0) = "Doctor Type"
                    drRow(0) = "Provider Type"
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
                    Case "gloEMR Groups"
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
                        If Not IsNothing(frmMaster) Then   'Obj Disposed by Mitesh 
                            frmMaster.Dispose()
                            frmMaster = Nothing
                        End If
                        Exit Sub
                    Case "User Groups"
                        Dim ofrmusergroups As New frmUserGroups
                        ofrmusergroups.Text = "New UserGroup"
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
                        If Not IsNothing(ofrmusergroups) Then   'Obj Disposed by Mitesh 
                            ofrmusergroups.Dispose()
                            ofrmusergroups = Nothing
                        End If


                        Exit Sub
                    Case "API Access"
                        If Not trvCategory.SelectedNode Is Nothing Then

                            If trvCategory.SelectedNode.Text = "Roles" Then
                                Dim frmAPIRoles As New gloPatientPortal.frmAPIAccessRoles(gloEMRAdmin.mdlGeneral.GetConnectionString, 0, gloEMRAdmin.mdlGeneral.gnLoginID)
                                frmAPIRoles.Text = "Add New API Role"
                                frmAPIRoles.ShowDialog()
                                Call Fill_APICategory()
                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                    Call Fill_DetailsAPI()
                                Else
                                    dgData.DataSource = Nothing
                                End If

                                If Not IsNothing(frmAPIRoles) Then   'Obj Disposed by Mitesh 
                                    frmAPIRoles.Dispose()
                                    frmAPIRoles = Nothing
                                End If
                            ElseIf trvCategory.SelectedNode.Text = "Users" Then
                                Dim frmAPIContact As New gloPatientPortal.frmAPIAccessContact(gloEMRAdmin.mdlGeneral.GetConnectionString, 0, gloEMRAdmin.mdlGeneral.gnLoginID)
                                frmAPIContact.Text = "Add New API User"
                                frmAPIContact.ShowDialog()
                                Call Fill_APICategory()
                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(1)
                                    Call Fill_DetailsAPI()
                                Else
                                    dgData.DataSource = Nothing
                                End If
                                If Not IsNothing(frmAPIContact) Then   'Obj Disposed by Mitesh 
                                    frmAPIContact.Dispose()
                                    frmAPIContact = Nothing
                                End If
                            End If

                        End If

                        Exit Sub
                    Case "User Management"
                        'Dim frmMaster As New frmUser
                        Dim frmMaster As New frmUserMgt
                        frmMaster.Fill_UserRights()
                        frmMaster.Fill_AuditRights()
                        frmMaster.blnModify = False
                        Call frmMaster.Fill_gloEMRGroups()
                        Call frmMaster.Fill_MaritalStatus()
                        Call frmMaster.Fill_Gender()
                        Call frmMaster.Fill_Providers()
                        fillStates(frmMaster.cmbState) ''Sandip Darade 20100109 
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
                        If Not IsNothing(frmMaster) Then   'Obj Disposed by Mitesh 
                            frmMaster.Dispose()
                            frmMaster = Nothing
                        End If
                        Exit Sub
                    Case "Client Settings"
                        btnRefresh_Click(sender, e)  ''added on 19 dec 2012 
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
                        If Not IsNothing(frmMaster) Then   'Obj Disposed by Mitesh 
                            frmMaster.Dispose()
                            frmMaster = Nothing
                        End If
                        Exit Sub
                        '''dhruv 20091120
                        '''----------------------
                    Case "Multiple Database"
                        Dim objSetting As New frmDBCredentials()
                        objSetting.ShowDialog()
                        btnRefresh_Click(sender, e)
                        If Not IsNothing(objSetting) Then   'Obj Disposed by Mitesh 
                            objSetting.Dispose()
                            objSetting = Nothing
                        End If
                        '''---------------------
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
                        If Not IsNothing(frmMaster) Then   'Obj Disposed by Mitesh 
                            frmMaster.Dispose()
                            frmMaster = Nothing
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
                        If Not IsNothing(frmgloEMRDoctorType) Then   'Obj Disposed by Mitesh 
                            frmgloEMRDoctorType.Dispose()
                            frmgloEMRDoctorType = Nothing
                        End If

                        Exit Sub

                    Case "Clinic"
                        Dim objfrmClinic As New frmClinicNew
                        objfrmClinic.blnModify = False
                        If objfrmClinic.ShowDialog = Windows.Forms.DialogResult.OK Then
                            Call Fill_CategoryClinics()
                        End If
                        If Not IsNothing(objfrmClinic) Then   'Obj Disposed by Mitesh 
                            objfrmClinic.Dispose()
                            objfrmClinic = Nothing
                        End If
                        If trvCategory.Nodes.Count > 0 Then
                            If trvCategory.Nodes.Item(0).Nodes.Count > 0 Then
                                trvCategory.SelectedNode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                            End If
                        End If


                        Exit Sub
                        ''''''''code added by pradeep on16/06/2010 for prefix
                        'Case "Prefix"
                        '    Dim objfrmPrefix As New frmPrefix
                        '    objfrmPrefix.ShowDialog()
                        '    Call Fill_Prefix()
                        '    Exit Sub

                    Case "Site Prefix"
                        Dim objfrmPrefix As New gloSettings.frmPrefix(gstrConnectionString)
                        objfrmPrefix.ShowDialog()
                        Call Fill_Prefix()
                        If Not IsNothing(objfrmPrefix) Then   'Obj Disposed by Mitesh 
                            objfrmPrefix.Dispose()
                            objfrmPrefix = Nothing
                        End If
                        Exit Sub

                    Case "Provider"
                        UpdateErrorLog("Adding Doctor", mdlFunctions.enmErrorOccuredForm.MainForm, mdlFunctions.enmOperation.Add)
                        Dim frmgloEMRDoctor As New frmDoctor(0)
                        frmgloEMRDoctor.blnModify = False
                        'frmgloEMRDoctor.Fill_DcotorTypes()
                        'If frmgloEMRDoctor.ShowDialog() = DialogResult.OK Then
                        frmgloEMRDoctor.ShowDialog()
                        Call Fill_CategoryProviders()
                        '  End If
                        If Not IsNothing(frmgloEMRDoctor) Then   'Obj Disposed by Mitesh 
                            frmgloEMRDoctor.Dispose()
                            frmgloEMRDoctor = Nothing
                        End If
                        If trvCategory.Nodes.Count > 0 Then
                            If trvCategory.Nodes.Item(0).Nodes.Count > 0 Then
                                trvCategory.SelectedNode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                            End If
                        End If

                        Exit Sub

                    Case "Clearinghouse"
                        UpdateErrorLog("Adding Clearinghouse", mdlFunctions.enmErrorOccuredForm.MainForm, mdlFunctions.enmOperation.Add)
                        Dim frm As New frmSetupClearingHouse(gstrConnectionString)

                        frm.ShowDialog()
                        Call Fill_ClearingHouse()
                        '  End If
                        If Not IsNothing(frm) Then   'Obj Disposed by Mitesh 
                            frm.Dispose()
                            frm = Nothing
                        End If

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
            If dgData.CurrentRowIndex < 0 Then
                Exit Sub
            End If
            If enmUserOperation = enmOperation.Admin Then
                If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
                    Exit Sub
                End If
                Select Case Trim(trvAdminMenu.SelectedNode.Text)
                    Case "gloEMR Groups"
                        If Trim(trvCategory.SelectedNode.Text) <> "User Groups" Then
                            If MessageBox.Show("Are you sure you want to delete this user group?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
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
                            If MessageBox.Show("Are you sure you want to delete this group?", "gloEMR Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Dim ID As Int64 = Convert.ToInt64(trvCategory.SelectedNode.Tag)
                                Dim objUsers As New clsUserGroups
                                'Developer: Mitesh Patel
                                'Date:26-Dec-2011'
                                'PRD: Lab Usability Admin Setting
                                If dtLabUsers.Rows.Count <> 0 Then
                                    Dim strtmp As String = validate_Labusers(dtLabUsers, ID)
                                    If strtmp <> "" Then
                                        Dim oResult As DialogResult = MessageBox.Show(strtmp, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                                        If oResult = Windows.Forms.DialogResult.Yes Then
                                            objUsers.Delete_LabuserTasks(ID, strUserID)
                                            If Not IsNothing(dtLabUsers) Then
                                                dtLabUsers.Dispose()
                                                dtLabUsers = Nothing
                                            End If
                                            strUserID = String.Empty
                                        Else
                                            strUserID = String.Empty
                                            Exit Sub
                                        End If
                                    End If

                                End If

                                objUsers.DeleteGroup(ID)
                                objUsers = Nothing
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
                            Dim oClearingHouse As New ClearingHouse(gstrConnectionString)
                            oClearingHouse.Delete(ID)
                            Fill_ClearingHouse()
                        End If
                    Case "User Management"
                        Dim blnBlock As Boolean
                        Dim strMessage As String
                        If Trim(btnDelete.Text) = "Block" Then
                            blnBlock = True
                            strMessage = "Are you sure you want to block this user?"
                        Else
                            blnBlock = False
                            strMessage = "Are you sure you want to unblock this user?"
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


                        If Not IsNothing((trvCategory.SelectedNode)) AndAlso Trim(trvCategory.SelectedNode.Text) <> "Client Settings" Then
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
                        Else
                            dgData.DataSource = Nothing
                        End If

                        ''Dhruv 20091202
                    Case "Multiple Database"
                        Dim objDatabase As New ClsMultipleDb()
                        If Not IsNothing(Trim(trvCategory.SelectedNode.Text)) Then

                            If Trim(trvCategory.SelectedNode.Text) <> "Database" Then
                                If (trvCategory.SelectedNode.Text = "Server Name") Then
                                    Exit Sub
                                End If
                                ''here we are checking the current row is selected or not 
                                If (dgData.IsSelected(dgData.CurrentCell.RowNumber) = True) Then
                                    If MessageBox.Show("Do you want to delete this database?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
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
                        '-----------------------------------
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
                                    objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrLoginName & " user has deleted Self Notes.", gstrLoginName, gstrClientMachineName)
                                    objAudit = Nothing
                                    '-------------

                                Else
                                    MessageBox.Show("Unable to delete Self Note", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                                            objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrLoginName & " user has deleted Provider Type.", gstrLoginName, gstrClientMachineName)
                                            objAudit = Nothing

                                        Else
                                            MessageBox.Show("Unable to delete Provider Type", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        End If
                                        objProviderType = Nothing

                                    End If
                                Else
                                    MessageBox.Show("Cannot delete Provider Type because it is associated with one or more Providers", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If

                            End If


                        Else
                            MessageBox.Show("You cannot delete Senior/Junior Doctor type.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        '****** 26 Oct 07 4.05PM 





                    Case "Provider"
                        If dgData.CurrentRowIndex >= 0 Then
                            Dim objProvider As New clsProvider

                            If btnDelete.Text = "Block" Then
                                'Yatin 20/04/2012-Prescription Provider Association:  Senior Provider list includes inactive (or deactivated) providers. (FOA Item) 
                                'If MessageBox.Show("Are you sure you want to block this provider information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
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
                                    objAudit.CreateLog(clsAudit.enmActivityType.Delete, gstrLoginName & " user has deleted provider record.", gstrLoginName, gstrClientMachineName)
                                    objAudit = Nothing
                                Else
                                    ' MessageBox.Show("Unable To Block Provider", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                                objProvider = Nothing
                                'End If
                            Else
                                If MessageBox.Show("Are you sure you want to unblock this provider?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                                    Dim dtprovider As DataTable = objProvider.GetProviderLicenseDetail(dgData.Tag)
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
                                                        If oLicense.UpdateProviderLicenseStatus(dgData.Tag, objProvider.enmAUSStatus.PendingForLicense.GetHashCode, "") Then
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
                        'code added by pradeep on 16/06/2010 for prefix
                    Case "Site Prefix"
                        'If dgData.CurrentRowIndex >= 0 Then
                        'If dgData.SelectionBackColor<>
                        'If (dgData.IsSelected(dgData.CurrentCell.RowNumber) = True) Then
                        '    If (dgData.Item(dgData.CurrentCell.RowNumber, 1) <> gstrSQLServerName And dgData.Item(dgData.CurrentCell.RowNumber, 2) <> gstrDatabaseName) Then
                        If (dgData.IsSelected(dgData.CurrentCell.RowNumber) = True) Then
                            Dim strMessage As String
                            strMessage = "You are about to delete the Patient Code Prefix.Other users are currently logged in to the system.Changing the prefix while users are logged in is not recommended and could have unpredictable results.Do you wish to proceed?"
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
                        Call Fill_DetailsAuditReports()

                    Case ""
                End Select
            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        Try
            If dgData.CurrentRowIndex < 0 And Trim(trvAdminMenu.SelectedNode.Text) <> "User Groups" Then
                Exit Sub
            End If
            If enmUserOperation = enmOperation.Admin Then
                If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
                    Exit Sub
                End If
                Select Case Trim(trvAdminMenu.SelectedNode.Text)
                    Case "gloEMR Groups"
                        'Check Category is selected or not
                        If IsNothing(trvCategory.SelectedNode) = True Then
                            MessageBox.Show("Please select the User Group", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                        If Trim(trvCategory.SelectedNode.Text) <> "User Groups" Then
                            Dim objGroup As New clsUserGroups
                            Dim frmGroup As New frmUserGroup
                            frmGroup.Text = "Modify gloEMR Groups"
                            frmGroup.pnlWindowsGroupsUsers.Enabled = False
                            frmGroup.blnModify = True
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
                            ofrmusergroups.Text = "Modify UserGroup"
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
                    Case "API Access"

                        If Not trvCategory.SelectedNode Is Nothing Then
                            Dim ID As Int64 = 0
                            If trvCategory.SelectedNode.Text = "Roles" Then
                                If (dgData.IsSelected(dgData.CurrentRowIndex)) Then
                                    ID = Convert.ToInt64(dgData.Item(dgData.CurrentRowIndex, 0))
                                    Dim frmAPIRoles As New gloPatientPortal.frmAPIAccessRoles(gloEMRAdmin.mdlGeneral.GetConnectionString, ID, gloEMRAdmin.mdlGeneral.gnLoginID)
                                    frmAPIRoles.Text = "Modify API Role"
                                    frmAPIRoles.ShowDialog()
                                    Call Fill_APICategory()
                                    If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                        trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                        Call Fill_DetailsAPI()
                                    Else
                                        dgData.DataSource = Nothing
                                    End If
                                    If Not IsNothing(frmAPIRoles) Then   'Obj Disposed by Mitesh 
                                        frmAPIRoles.Dispose()
                                        frmAPIRoles = Nothing
                                    End If
                                Else
                                    MessageBox.Show("Please select the API Role.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            ElseIf trvCategory.SelectedNode.Text = "Users" Then
                                If (dgData.IsSelected(dgData.CurrentRowIndex)) Then
                                    ID = Convert.ToInt64(dgData.Item(dgData.CurrentRowIndex, 0))
                                    Dim frmAPIContact As New gloPatientPortal.frmAPIAccessContact(gloEMRAdmin.mdlGeneral.GetConnectionString, ID, gloEMRAdmin.mdlGeneral.gnLoginID)
                                    frmAPIContact.Text = "Modify API User"
                                    frmAPIContact.ShowDialog()
                                    Call Fill_APICategory()
                                    If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                        trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(1)
                                        Call Fill_DetailsAPI()
                                    Else
                                        dgData.DataSource = Nothing
                                    End If
                                    If Not IsNothing(frmAPIContact) Then   'Obj Disposed by Mitesh 
                                        frmAPIContact.Dispose()
                                        frmAPIContact = Nothing
                                    End If
                                Else
                                    MessageBox.Show("Please select the API Role.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If

                        End If
                        
                            End If
                        Exit Sub
                    Case "User Management"
                        Dim currentrow As Int64 = dgData.CurrentRowIndex
                        Dim objUser As New clsUsers
                        Dim oAddressControl As gloAddress.gloAddressControl = Nothing
                        'Dim frmgloEMRuser As New frmUser
                        Dim frmgloEMRuser As New frmUserMgt()
                        frmgloEMRuser.Text = "Modify User Management"
                        'Added by Rahul Patel on 21-09-2010
                        'Added condition for checking wheather the user is selected from list before modiying the User.

                        If (dgData.IsSelected(dgData.CurrentRowIndex)) Then
                            frmgloEMRuser.Fill_UserRights()
                            frmgloEMRuser.Fill_AuditRights()
                            frmgloEMRuser.blnModify = True
                            Call frmgloEMRuser.Fill_gloEMRGroups()
                            Call frmgloEMRuser.Fill_MaritalStatus()
                            Call frmgloEMRuser.Fill_Gender()
                            Call frmgloEMRuser.Fill_Providers()
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

                            ''Task #67507: gloEMR & Patient Portal Send Message screen changes
                            frmgloEMRuser.txtPortalDisplayName.Text = objUser.PortalDisplayName

                            '---------------------------------
                            'Added on 3-8-2016 for windows login name added in user manager form
                            frmgloEMRuser.txtWindowsLoginName.Text = objUser.WindowLoaginName
                            '------------------------------------
                            'frmgloEMRuser.txtAddress.Text = objUser.Address                    ''Commented by Dhruv 20100312
                            'frmgloEMRuser.txtAddress2.Text = objUser.Address2 
                            'frmgloEMRuser.txtCity.Text = objUser.City
                            ' fillStates(frmgloEMRuser.cmbState)
                            'frmgloEMRuser.txtZip.Text = objUser.ZIP
                            'frmgloEMRuser.cmbState.Text = objUser.State                        ''End-------------------Dhruv--Comment

                            ''Added the New Zip - Code Control
                            frmgloEMRuser.oAddressContol.isFormLoading = True
                            frmgloEMRuser.oAddressContol.txtAddress1.Text = objUser.Address     ''Add1
                            frmgloEMRuser.oAddressContol.txtAddress2.Text = objUser.Address2    ''Add2
                            frmgloEMRuser.oAddressContol.txtCity.Text = objUser.City            ''City
                            frmgloEMRuser.oAddressContol.txtZip.Text = objUser.ZIP              ''Zip

                            fillStates(frmgloEMRuser.oAddressContol.cmbState)                   ''Filling the State
                            frmgloEMRuser.oAddressContol.txtCounty.Text = objUser.County    ''County
                            frmgloEMRuser.oAddressContol.cmbCountry.Text = objUser.Country       ''Country
                            frmgloEMRuser.oAddressContol.cmbState.Text = objUser.State          ''State.

                            frmgloEMRuser.oAddressContol.isFormLoading = False

                            ''end Zip - Code

                            frmgloEMRuser.txtStreet.Text = objUser.Street
                            frmgloEMRuser.isFormLoading = True
                            frmgloEMRuser.isFormLoading = False
                            frmgloEMRuser.txtPhoneNo.Text = objUser.PhoneNo
                            frmgloEMRuser.txtMobileNo.Text = objUser.MobileNo
                            frmgloEMRuser.txtFax.Text = objUser.FAX
                            frmgloEMRuser.txtEmailAddress.Text = objUser.Email
                            frmgloEMRuser.chkgloEMRAdmin.Checked = objUser.gloEMRAdministrator
                            frmgloEMRuser.chkAuditTrails.Checked = objUser.IsAuditTrail
                            frmgloEMRuser.chkAccessDenied.Checked = objUser.AccessDenied
                            frmgloEMRuser.chkCountforCPOE.Checked = objUser.ISCountforCPOE

                            'Developer: Mitesh Patel
                            'Date:03-Jan-2012'
                            'PRD: Direct Ability
                            frmgloEMRuser.txtAbilityEmail.Text = objUser.AbilityEmail
                            frmgloEMRuser.txtAbilityPassword.Text = objEncryption.DecryptFromBase64String(objUser.AbilityPassword, constEncryptDecryptKey)
                            '-----------x---

                            ''' Added by Ujwala Atre as on 20101004 - Added Emergency Access to patient chart
                            frmgloEMRuser.ChkEmergencyAccess.Checked = objUser.EAPChart
                            If (objUser.EAPChart = True) Then
                                frmgloEMRuser.txtEAPassword.Text = objEncryption.DecryptFromBase64String(objUser.EAPassword, constEncryptDecryptKey)
                                frmgloEMRuser.txtEAConfirmPassword.Text = frmgloEMRuser.txtEAPassword.Text
                                '' Added Valid upto Date for Emergency Access Password as on 12032010
                                frmgloEMRuser.dtpValidupto.Value = objUser.ValidDt
                                '' Added Valid upto Date for Emergency Access Password as on 12032010
                            End If
                            ''' Added by Ujwala Atre as on 20101004 - Added Emergency Access to patient chart

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

                            ''code commented by Sandip Darade 20100327
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
                            ' frmgloEMRuser.cmbProvider.SelectedValue = objUser.ProviderID
                            'End If
                            ''code added  by Sandip Darade to replace code commented above  20100327
                            frmgloEMRuser.cmbProvider.SelectedValue = objUser.ProviderID
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
                            ''Added Rahul for AuditRights on 20101019
                            Dim clAuditRights As New Collection
                            clAuditRights = objUser.AuditRights
                            Dim nAuditTotalNodes As Int16
                            nAuditTotalNodes = frmgloEMRuser.trvAuditRights.GetNodeCount(False) - 1
                            For nCount = 1 To clAuditRights.Count
                                For nCount1 = 0 To nAuditTotalNodes
                                    AuditRightsSearchNode(frmgloEMRuser.trvAuditRights, clAuditRights.Item(nCount))
                                    If IsNothing(trvSearchNode) = False Then
                                        trvSearchNode.Checked = True
                                        frmgloEMRuser.trvAuditRights.SelectedNode = trvSearchNode
                                        'frmgloEMRuser.trvUserRights.SelectedNode.Checked = True
                                    End If
                                Next
                            Next

                            ''End
                            objUser = Nothing
                            frmgloEMRuser.trvUserRights.ExpandAll()
                            frmgloEMRuser.blnCheckRights = True
                            If frmgloEMRuser.ShowDialog() = DialogResult.OK Then
                                Call Fill_CategorygloEMRUsers()
                                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                                    Call Fill_DetailsgloEMRUsers()
                                    dgData.Select(currentrow)
                                    dgData.CurrentRowIndex = currentrow
                                Else
                                    dgData.DataSource = Nothing
                                End If
                            End If
                            frmgloEMRuser = Nothing
                        Else
                            MessageBox.Show("Please select the user.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Exit Sub
                    Case "Clinic"
                        'Dim frmgloEMRClinic As New frmClinic
                        Dim frmgloEMRClinic As New frmClinicNew
                        frmgloEMRClinic.Text = "Modify Clinic"
                        Dim objClinic As New clsClinic
                        frmgloEMRClinic.blnModify = True
                        frmgloEMRClinic.txtName.Tag = dgData.Tag
                        objClinic.SearchClinic(dgData.Tag)
                        frmgloEMRClinic.txtStreet.Text = objClinic.Street
                        frmgloEMRClinic.txtLabel.Text = objClinic.ClinicLabel
                        frmgloEMRClinic.oAddressContol.isFormLoading = True
                        frmgloEMRClinic.txtName.Text = objClinic.ClinicName
                        'frmgloEMRClinic.txtAddress1.Text = objClinic.ClinicAddress1
                        'frmgloEMRClinic.txtAddress2.Text = objClinic.ClinicAddress2
                        'frmgloEMRClinic.txtCity.Text = objClinic.City
                        frmgloEMRClinic.txtClinicMaillingContact.Text = objClinic.ContactName
                        frmgloEMRClinic.oAddressContol.txtAddress1.Text = objClinic.ClinicAddress1
                        frmgloEMRClinic.oAddressContol.txtAddress2.Text = objClinic.ClinicAddress2
                        frmgloEMRClinic.oAddressContol.txtCity.Text = objClinic.City
                        frmgloEMRClinic.oAddressContol.txtZip.Text = objClinic.ZIP

                        frmgloEMRClinic.oAddressContol.txtCounty.Text = objClinic.County
                        frmgloEMRClinic.oAddressContol.cmbCountry.Text = objClinic.Country
                        frmgloEMRClinic.oAddressContol.cmbState.Text = objClinic.State

                        frmgloEMRClinic.oAddressContol.isFormLoading = False
                        'line added by dipak 20090914 for fill passed combobox with all states in database
                        'fillStates(frmgloEMRClinic.cmbState)
                        'frmgloEMRClinic.cmbState.Text = objClinic.State
                        'line added by dipak 20090914 ("frmgloEMRClinic.isTextBoxLoading = True" set true 
                        'to track textchange event of txtZip is fire due to Assignment or by user enter by key board 
                        'frmgloEMRClinic.isTextBoxLoading = True
                        'frmgloEMRClinic.txtZip.Text = objClinic.ZIP
                        'frmgloEMRClinic.isTextBoxLoading = False
                        frmgloEMRClinic.txtPhoneNo.Text = objClinic.PhoneNo
                        frmgloEMRClinic.txtMobileNo.Text = objClinic.MobileNo
                        frmgloEMRClinic.txtFax.Text = objClinic.FAX
                        frmgloEMRClinic.txtEmailAddress.Text = objClinic.Email
                        frmgloEMRClinic.txtURL.Text = objClinic.URL
                        frmgloEMRClinic.txtTAXID.Text = objClinic.TAXID
                        'sarika siteid 20090708
                        frmgloEMRClinic.txtSiteID.Text = objClinic.SiteID
                        ''Sandip darade  20091113
                        frmgloEMRClinic.ClinicNPI = objClinic.ClinicNPI
                        '---
                        frmgloEMRClinic.txtTaxonomy.Text = objClinic.TaxonomyCode
                        frmgloEMRClinic.txtClinicNPI.Text = objClinic.ClinicNPI ''Added By Debasish das on 19th March 2010

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

                        If Not IsNothing(Trim(trvCategory.SelectedNode.Text)) Then
                            ''If the selected node is the header then dont do any of the operation
                            If (trvCategory.SelectedNode.Text = "Client Settings") Then
                                Exit Sub
                            End If
                        End If

                        Dim frmClientSettings As New frmClient
                        frmClientSettings.Text = "Modify Client Settings"
                        Dim objClient As New clsClientMachines
                        objClient.SearchClient(CType(trvCategory.SelectedNode.Tag, Integer))
                        'frmClientSettings.cmbMachineName.Text = Trim(trvCategory.SelectedNode.Text)
                        If (objClient.ClientMachineID = 0) Then
                            MessageBox.Show(Me, "Selected machine does not exists, Please add this again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgData.DataSource = Nothing
                            Dim evt As EventArgs
                            btnRefresh_Click(btnRefresh, evt)

                            Exit Sub
                        End If
                        frmClientSettings.blnModify = True
                        frmClientSettings.cmbMachineName.Tag = objClient.ClientMachineID

                        frmClientSettings.txtMachineName.Text = Trim(trvCategory.SelectedNode.Text)
                        If objClient.VoiceEnabled = True Then
                            frmClientSettings.optVoiceYes.Checked = True
                        Else
                            frmClientSettings.optVoiceNo.Checked = True
                        End If
                        If objClient.ScanEnabled = True Then
                            frmClientSettings.optScanYes.Checked = True
                        Else
                            frmClientSettings.optScanNo.Checked = True
                        End If
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
                        ''--------------------------------------------------------
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
                            '   frmgloEMRDoctorType.Text = "Modify Provider Type"
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
                        Dim frm As New frmSetupClearingHouse(gstrConnectionString, ID)
                        frm.ShowDialog()
                        frm.Dispose()
                        Fill_ClearingHouse()

                        ''code added by pradeep on16/06/2010 for prefix
                    Case "Site Prefix"
                        If (dgData.IsSelected(dgData.CurrentCell.RowNumber) = True) Then
                            Dim strMessage As String
                            Dim ID As Long = 0
                            Dim sServer As String = ""
                            Dim sDatabase As String = ""
                            Dim sPrefix As String = ""
                            strMessage = "You are about to change the Patient Code Prefix.Other users are currently logged in to the system.Changing the prefix while users are logged in is not recommended and could have unpredictable results.Do you wish to proceed?"
                            If MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                ID = Convert.ToInt64(dgData.Item(dgData.CurrentRowIndex, 0))
                                sServer = dgData.Item(dgData.CurrentRowIndex, 1)
                                sDatabase = dgData.Item(dgData.CurrentRowIndex, 2)
                                sPrefix = dgData.Item(dgData.CurrentRowIndex, 3)
                                Dim objfrmPrefix As New gloSettings.frmPrefix(ID, sServer, sDatabase, sPrefix, gstrConnectionString)
                                objfrmPrefix.ShowDialog()
                                Fill_Prefix()
                            Else
                                Exit Sub
                            End If
                        End If


                    Case "Provider"

                        UpdateErrorLog("Modifying Provider", mdlFunctions.enmErrorOccuredForm.MainForm, mdlFunctions.enmOperation.Update)

                        Dim objDoctor As New clsProvider(gstrConnectionString)
                        Dim frmgloEMRDoctor As New frmDoctor(CType(trvCategory.SelectedNode.Tag, Int64))
                        Dim strProviderName As String

                        frmgloEMRDoctor.Text = "Modify Provider"

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

                        '29-Jan-13 Aniket: Fixing Bug #62732 
                        '27-Feb-15 Aniket: Resolving Bug #79545 ( Modified): gloEMR Admin: Provider middle name- Provider name name does not get updated
                        strProviderName = Fill_DetailsProviders()
                        If strProviderName <> "" Then
                            myNode.Text = strProviderName
                        End If
                End Select

            ElseIf enmUserOperation = enmOperation.Tools Then
                If Trim(trvTools.SelectedNode.Text) = "Tools" Then
                    Exit Sub
                End If


            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



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
                        Case "gloEMR Groups"
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
                            ''dhruv 20091120
                            ''----------------------------
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
                            ''----------------------------

                            'sarika 11th sept 07
                        Case "Provider Type"
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
                            'code added by pradeep on 16/06/2010 for prefix
                        Case "Site Prefix"
                            Fill_Prefix()
                            Exit Sub
                            '-------------------
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
                            If txtInstringSearch.Text <> "" Then
                                txtInstringSearch.Text = ""
                            End If
                            Call Fill_DetailsAuditReports()

                            Exit Sub
                            'Case "Archived Audit Log"
                        Case "Archived Audit Report"
                            txtInstringSearch.ResetText()
                            btnShowAudit_Click(sender, e)
                            'Fill_CategoryArchivedUsers()
                            ''Call Fill_ArchivedAuditCategories()
                            'If trvCategory.Nodes(0).Nodes.Count > 0 Then
                            '    If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False Then
                            '        trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)

                            '    End If
                            '    Call Fill_DetailsArchivedAuditReports()
                            'End If
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

    

    Private Sub tlbBar_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
        
        Select Case Trim(e.Button.Tag)
           


            ''//Code added by Ravikiran on 10/02/2007
            Case "RxReport Designer"
                Dim frm As New frmRxReportDesigner
                frm.ShowDialog()
                
        End Select


        ''Catch objErr As Exception
        ''    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub

    Private Sub frmgloEMRAdmin_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            'sarika  21 feb
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Logout, "'" & gstrLoginName & "' successfully logged out.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '-------------
            ''Condition Added by Mayuri:2010222
            'Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
            If _ShowSplashScreen = True Then
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                gloRegistrySetting.SetRegistryValue("LeftWidth", pnlLeft.Width)
                gloRegistrySetting.SetRegistryValue("CategoryWidth", trvCategory.Width)
                gloRegistrySetting.CloseRegistryKey()
            Else
                If MessageBox.Show("Are you sure, you want to close the application?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                    gloRegistrySetting.SetRegistryValue("LeftWidth", pnlLeft.Width)
                    gloRegistrySetting.SetRegistryValue("CategoryWidth", trvCategory.Width)
                    gloRegistrySetting.CloseRegistryKey()

                    'Cleanup Temp Files.
                    Cleanup_TempFolder()
                    End
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
            btnHideToolBar.BackgroundImage = Global.gloEMRAdmin.My.Resources.yellowhidetoolbar

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
            btnHideToolBar.BackgroundImage = Global.gloEMRAdmin.My.Resources.bluehidetoolbar
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
        Try


            Dim trvNode As TreeNode
            trvNode = trvAdminMenu.GetNodeAt(e.X, e.Y)
            If IsNothing(trvNode) = False Then
                trvAdminMenu.SelectedNode = trvNode
                Call ShowAdministrator()
            End If
            btnATNALog.Visible = False
            btnAuditLogTampering.Visible = False
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvAudit_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvAudit.MouseDown
        Try
            btnViewHistory.Visible = False
            Dim trvNode As TreeNode
            trvNode = trvAudit.GetNodeAt(e.X, e.Y)
            If IsNothing(trvNode) = True Then
                Exit Sub
            End If

            lblMainTop.Text = "Audit"
            dgData.Visible = False
            SplitterMainCategory.Visible = False
            trvCategory.Visible = False
            pnlMainMainTop.Visible = False

            If Trim(trvNode.Text) = "Report" Then
                btnATNALog.Visible = True 'Added by kanchan on 20101118 for ATNA
                btnAuditLogTampering.Visible = True
                Me.bIsAuditLogReport = True
                If IsNothing(trvNode) = False Then
                    trvAudit.SelectedNode = trvNode
                    If txtInstringSearch.Text <> "" Then
                        txtInstringSearch.Text = ""
                    End If
                    Call ShowAudits()
                End If
            Else
                btnATNALog.Visible = False 'Added by kanchan on 20101118 for ATNA
                Me.bIsAuditLogReport = False
                'CHECK WHETHER ARCHIVE DATABASE IS SET OR NOT
                If (GetArchiveConnectionString() <> "") Then
                    'CHECK THE DATABASE SETTING FOR ARCHIVE 
                    If (CheckArchiveDB() = False) Then
                        Exit Sub
                    End If

                    ' Dim trvNode As TreeNode
                    'trvNode = trvAudit.GetNodeAt(e.X, e.Y)
                    If IsNothing(trvNode) = False Then
                        trvAudit.SelectedNode = trvNode
                        If txtInstringSearch.Text <> "" Then
                            txtInstringSearch.Text = ""
                        End If
                        Call ShowAudits()
                    End If
                Else
                    Exit Sub
                End If
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
            btnATNALog.Visible = False
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
                    btnATNALog.Visible = False
                    If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
                        Exit Sub
                    End If
                    Me.Cursor = Cursors.WaitCursor
                    Select Case Trim(trvAdminMenu.SelectedNode.Text)
                        Case "Windows Groups & Users"
                            Call Fill_DetailsWindowsGroupsUsers()
                        Case "gloEMR Groups"
                            Call Fill_DetailsgloEMRGroups()
                            'Added For User groups
                            'sandip Darade 6th Feb 2009
                        Case "User Groups"
                            Call Fill_DetailsUserGroups()
                        Case "API Access"
                            Call Fill_DetailsAPI()
                        Case "User Management"
                            Call Fill_DetailsgloEMRUsers()
                        Case "Provider"
                            Call Fill_DetailsProviders()
                        Case "Client Settings"
                            Call Fill_DetailsClientSettings()
                        Case "Client Update Details"
                            Call Fill_ClientUpdateDetails()
                            ''Dhruv 20091209
                        Case "Multiple Database"
                            Fill_DetailsMultipleDB()
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
                            '----------------------------------------
                    End Select
                    Me.Cursor = Cursors.Default
                ElseIf enmUserOperation = enmOperation.Audit Then
                    If Trim(trvAudit.SelectedNode.Text) = "Audit" Then
                        Me.Cursor = Cursors.Default
                        'Exit Sub
                    ElseIf Trim(trvAudit.SelectedNode.Text) = "Report" Then
                        btnRefresh_Click(Me, New System.EventArgs())
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
                    Me.Cursor = Cursors.Default
                ElseIf enmUserOperation = enmOperation.Tools Then
                    btnATNALog.Visible = False
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
                    Me.Cursor = Cursors.Default
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

            Dim objCon As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
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
        btnViewHistory.Visible = False
        ''pnlAuditLogSearch.Visible = False
        'Added to dock Online patient form to right side
        If Trim(trvAdminMenu.SelectedNode.Text) <> "Portal Message Templates" And Trim(trvAdminMenu.SelectedNode.Text) <> "Online Patient Form" Then
            ShowHidePanels(True)
        End If



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
        If Trim(trvAdminMenu.SelectedNode.Text) = "Administrator" Or Trim(trvAdminMenu.SelectedNode.Text) = "DB Management" Or Trim(trvAdminMenu.SelectedNode.Text) = "Tools" Then
            Select Case Trim(trvAdminMenu.SelectedNode.Text)
                Case "Administrator"
                    If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                        btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                        btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                        btnRefresh.Text = "Show"
                        btnRefresh.ToolTipText = "Show"
                    Else
                        btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                        btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                        btnRefresh.Text = "Refresh"
                        btnRefresh.ToolTipText = "Refresh"
                    End If
                    '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topadministrator.JPG")
                    lblMainTop.Text = "Administrator"

                Case "DB Management"
                    If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                        btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                        btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                        btnRefresh.Text = "Show"
                        btnRefresh.ToolTipText = "Show"
                    Else
                        btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                        btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                        btnRefresh.Text = "Refresh"
                        btnRefresh.ToolTipText = "Refresh"
                    End If
                    '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdatabasemangement.JPG")
                    lblMainTop.Text = "Database Management"
                Case "Tools"
                    If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                        btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                        btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                        btnRefresh.Text = "Show"
                        btnRefresh.ToolTipText = "Show"
                    Else
                        btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                        btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                        btnRefresh.Text = "Refresh"
                        btnRefresh.ToolTipText = "Refresh"
                    End If

                    '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\toptools.JPG")
                    lblMainTop.Text = "Tools"
            End Select
            dgData.Visible = False
            SplitterMainCategory.Visible = False
            trvCategory.Visible = False
            'picMainSepMain.Visible = False
            pnlMainMainTop.Visible = False
            'picMainSepTop.Visible = False
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        'picMainSepTop.Visible = True
        trvCategory.Visible = True
        SplitterMainCategory.Visible = True
        dgData.Visible = True
        pnl_tlsp_Top.Visible = True
        lblMainTop.Text = ""
        Select Case Trim(trvAdminMenu.SelectedNode.Text)
            Case "Windows Groups & Users"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
            Case "gloEMR Groups"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topgloemrgroup.JPG")
                'sarika 26th june 07
                lblMainTop.Text = "gloEMR Groups"
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
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The gloEMR Groups data viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------
                'Added for User groups
                'Sandip Darade 7th Feb 09
            Case "User Groups"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
            Case "API Access"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                lblMainTop.Text = "API Access"
                '---
                Call Fill_APICategory()
                If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                    Call Fill_DetailsAPI()
                Else
                    dgData.DataSource = Nothing
                End If
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "API Access viewed", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                'Case "Heart Centric Users Management"
            Case "Device User Management"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                If GetECGInterfaceStatus() Then
                    pnl_tlsp_Top.Visible = False
                    Dim objEcg As New frmEcgCredentials()
                    objEcg.ShowDialog()
                    objEcg.Dispose()
                    objEcg = Nothing
                Else
                    ''commented  messagebox by manoj jadhav on 20111003 for checking device activations
                    'MessageBox.Show("Please enable ECG device interface on Settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ''added  messagebox by manoj jadhav on 20111003 for checking device activations
                    MessageBox.Show("No device interface found. Please activate device interface to configure user accounts at Settings  >> Interface Settings >> Device Interface Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If



            Case "Mobile User Management"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                Dim frmMobileMgt As New frmMobileMgnt
                frmMobileMgt.ShowDialog()
                frmMobileMgt.Dispose()
                frmMobileMgt = Nothing

            Case "Online Patient Form"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                'Added to dock Online patient form to right side
                Dim frm As Form
                For Each frm In Me.MdiChildren
                    If frm.Text = "Online Patient Forms" Then
                        frm.Close()
                    End If
                Next

                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                Dim strDMSconnectionstring As String = String.Empty
                strDMSconnectionstring = gloEMRAdmin.mdlGeneral.GetConnectionString(gDmsServerName, gDmsDatabaseName, gDmsIsSQLAUTHEN, gDmsUserID, gDmsPassWord)
                Dim ObjfrmHealthForm As New frmHealthForm(gstrConnectionString, gnLoginID, strDMSconnectionstring)
                ObjfrmHealthForm.ShowInTaskbar = False
                If (Not Me.IsMdiContainer) Then
                    Me.IsMdiContainer = True
                End If
                'Added to dock Online patient form to right side
                ObjfrmHealthForm.MdiParent = Me
                'ObjfrmHealthForm.ShowDialog()
                ObjfrmHealthForm.BringToFront()
                ShowHidePanels(False)
                ObjfrmHealthForm.Dock = DockStyle.Fill
                lblMainTop.Text = "Online Patient Form"
                ObjfrmHealthForm.Show()

            Case "Portal Message Templates"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                'Added to dock Online patient form to right side
                Dim frm As Form
                For Each frm In Me.MdiChildren
                    If frm.Text = "Portal Message Templates" Then
                        frm.Close()
                    End If
                Next

                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                Dim strDMSconnectionstring As String = String.Empty
                strDMSconnectionstring = gloEMRAdmin.mdlGeneral.GetConnectionString(gDmsServerName, gDmsDatabaseName, gDmsIsSQLAUTHEN, gDmsUserID, gDmsPassWord)
                Dim objfrmPortalMessages As New frmPortalMessages(gstrConnectionString, gnLoginID, strDMSconnectionstring)
                objfrmPortalMessages.ShowInTaskbar = False

                If (Not Me.IsMdiContainer) Then
                    Me.IsMdiContainer = True
                End If

                objfrmPortalMessages.MdiParent = Me
                objfrmPortalMessages.BringToFront()
                ShowHidePanels(False)
                objfrmPortalMessages.Dock = DockStyle.Fill
                lblMainTop.Text = "Portal Message Templates"
                objfrmPortalMessages.Show()

            Case "Setup Unscheduled Care Templates"

                lblMainTop.Text = "Setup Unscheduled Care Templates"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False
                Dim ofrmMigrationByTemplate As New frmMigrationByTemplate(gstrConnectionString)
                ofrmMigrationByTemplate.ShowDialog()
                ofrmMigrationByTemplate.Dispose()
                ofrmMigrationByTemplate = Nothing

            Case "User Custom Link"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False

                Dim objuserCustomLink As New frmUserCustomLinkConfigurations()
                objuserCustomLink.ShowDialog()
                objuserCustomLink.Dispose()
                objuserCustomLink = Nothing
                '---end of code added by Rohit

            Case "Site Prefix"
                lblMainTop.Text = "Site Prefix"
                Call Fill_Prefix()

            Case "User Management"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnViewHistory.Visible = True
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                '     picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdoctors.gif")
                'sarika 26th june 07
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
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                frmConfiguration.Dispose()
                frmConfiguration = Nothing
                '----------------------------------------
                'sarika 30th apr 2007
            Case "Junior-Senior Provider Association"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                objfrmJrSrAssociation.Dispose()
                objfrmJrSrAssociation = Nothing

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
            Case "Provider DIRECT Message – Assign Users to Provider Inboxes"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If

                lblMainTop.Text = "Providers"
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                pnl_tlsp_Top.Visible = False

                Dim frmProviderUserAssocation As New frmProviderUserDirectAssociation
                With frmProviderUserAssocation
                    .ShowDialog()
                    .Close()
                    .Dispose()
                End With

                'Dim objAudit As New clsAudit
                'objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Junior-Senior Doctor Association data viewed.", gstrLoginName, gstrClientMachineName)
                'objAudit = Nothing

            Case "Provider-Referral Letter"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                '******13 Oct 07 5.20PM Bug# 334
                '  picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topdoctors.gif")
                'sarika 26th june 07
                lblMainTop.Text = "Providers"
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
                frmConfiguration.Dispose()
                frmConfiguration = Nothing
                ''Dhruv 20091202 
                ''Fill the treeview--------------------------------------------
            Case "Multiple Database"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
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
                ''-------------------------------------------------------------
                'Developer: Mitesh Patel
                'Date:20-Dec-2011'
                'PRD: Lab Usability
            Case "Lab User Task"
                lblMainTop.Text = "Lab User Task"

                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                Dim frm As New frmLabUserTask
                '-------
                frm.ShowDialog()
                If Not IsNothing(frm) Then
                    frm.Dispose()
                    frm = Nothing
                End If

            Case "Client Settings"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                '------------------
                'Sarika 21st April 2007
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Client Settings viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------
            Case "Client Update Details"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topclientsettings.gif")
                'sarika 26th june 07
                lblMainTop.Text = "Client Update Details"
                '---
                pnlCommandButtons.Visible = False
                pnl_tlsp_Top.Visible = False
                pnlMainMainTop.Visible = True
                Panel8.Visible = False
                trvCategory.Visible = False
                pnlClientUpdateDetailsFilter.Visible = True

                'Call Fill_ClientUdpateMachinesDetails()
                'If trvCategory.Nodes(0).GetNodeCount(True) >= 1 Then
                '    trvCategory.SelectedNode = trvCategory.Nodes(0).Nodes(0)
                '    Call Fill_ClientUpdateDetails()
                'Else
                '    dgData.DataSource = Nothing
                'End If
                Call Fill_ClientMachineDropDown()
                Call Fill_ClientUpdateDetails()

                '------------------
                'Sarika 21st April 2007
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Client Update Details Settings viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------
            Case "Clinic"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                '   picMainTop.Image = Image.FromFile(Application.StartupPath & "\Images\topclinic.JPG")
                'sarika 26th june 07

                lblMainTop.Text = "Clinic"
                pnlCommandButtons.Visible = True

                ''If Clinic information is not present then only allow to add Clinic
                btnNew.Visible = False
                btnNew.Text = "New"
                ''
                btnModify.Visible = True
                btnModify.Text = "Modify"
                btnDelete.Visible = False
                btnDelete.Text = "Delete"
                'btnDelete.ToolTipText = "Delete"

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
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                frm.Dispose()
                frm = Nothing
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Clinic Workflow Settings viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '----------------------
            Case "Backup"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                pnlCommandButtons.Visible = True
                btnNew.Visible = True
                btnNew.Text = "New"
                btnModify.Visible = True
                btnModify.Text = "Modify"
                btnDelete.Visible = True
                btnDelete.Text = "Delete"
                'btnDelete.ToolTipText = "Delete"


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
            Case "Import Fee Shedule"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                lblMainTop.Text = "Import Fee Shedule"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False

                Dim frm As New frmImportFeeSchedule()
                frm.ShowDialog()
                frm.Dispose()
                frm = Nothing
            Case "Settings"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                Dim frm As New frmSettings_New
                '-------
                frm.ShowDialog()
                frm.Dispose()
                frm = Nothing

                'If (frm.DialogResult = Windows.Forms.DialogResult.OK) Then
                Call Fill_Admin()
                trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(1).Nodes(0)
                ' End If

            Case "SSRS Report Settings"
                lblMainTop.Text = "SSRS Report Settings"
                '---
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                Dim frm As New Project_Reportview.frmArrangeReport("gloEMR", gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                '-------
                frm.ShowDialog()
                frm.Dispose()
                frm = Nothing

            Case "Deploy SSRS Reports"
                lblMainTop.Text = "Deploy SSRS Reports"
                '---
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                Dim frm As New SSRSApplication.frm_DeployReports("gloEMR", gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                '-------
                frm.ShowDialog()
                frm.Dispose()
                frm = Nothing

            Case "RxReport Designer"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                frm.Dispose()
                frm = Nothing
                ''// Updation ends


                'sarika 11th sept 07
            Case "Provider Type"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                pnlCommandButtons.Visible = True
                lblMainTop.Text = "Provider Type"
                btnDelete.Text = "Delete"
                'btnDelete.ToolTipText = "Delete"
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
                objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "The Provider Type information viewed.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing

                ''Claim Validation settings 
            Case "Claim Validation Settings"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                lblMainTop.Text = "Claim Validation Settings"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False

                Dim frm As New frmEDISettings(gstrConnectionString)
                frm.ShowDialog()
                frm.Dispose()
                frm = Nothing
            Case "Update PayerID"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                lblMainTop.Text = "Update PayerID"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False

                Dim frm As New frmUpdatePayerID(gstrConnectionString)
                frm.ShowDialog()
                frm.Dispose()
                frm = Nothing
                '--------------------------------------------------------
            Case "Merge Insurance Plan"
                lblMainTop.Text = "Merge Insurance Plan"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False
                Dim oFrm As New gloContactsMerge.frmContactsMerge(gstrConnectionString)
                oFrm.ShowDialog()
                oFrm.Dispose()
                oFrm = Nothing

            Case "Advance Merge Insurance"
                lblMainTop.Text = "Advance Merge Insurance"
                pnl_tlsp_Top.Visible = False
                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False

                pnlMainMainTop.Visible = False
                Dim oFrm As New gloContactsMerge.frmMergeContacts(gstrConnectionString, True)
                oFrm.ShowDialog(Me)
                oFrm.Dispose()
                oFrm = Nothing
            Case "TaxID Setup"
                If trvAdminMenu.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If

                Dim frm As Form
                For Each frm In Me.MdiChildren
                    If frm.Text = "TaxID Setup" Then
                        frm.Close()
                    End If
                Next

                dgData.Visible = False
                SplitterMainCategory.Visible = False
                trvCategory.Visible = False
                pnlMainMainTop.Visible = False
                Dim strDMSconnectionstring As String = String.Empty
                Dim objfrmtaxid As New frmTaxID_View(gstrConnectionString)   ''frmHealthForm(gstrConnectionString, gnLoginID, strDMSconnectionstring)
                objfrmtaxid.ShowInTaskbar = False
                'Added to dock Tax ID form to right side
                objfrmtaxid.MdiParent = Me
                'ObjfrmHealthForm.ShowDialog()
                objfrmtaxid.BringToFront()
                ShowHidePanels(False)
                objfrmtaxid.Dock = DockStyle.Fill
                objfrmtaxid.MaximizeBox = False
                objfrmtaxid.MinimizeBox = False
                lblMainTop.Text = "TaxID Setup"
                objfrmtaxid.Show()

        End Select
        Me.Cursor = Cursors.Default
    End Sub

    'Added to dock Online patient form to right side
    Public Function ShowHidePanels(ByVal bIsShow As Boolean)
        If bIsShow = True Then
            pnlMain.Visible = True
            pnl_tlsp_Top.Visible = True
        Else
            pnlMain.Visible = False
            pnl_tlsp_Top.Visible = False
        End If

    End Function

    Public Sub ShowAudits()

        ShowHidePanels(True)

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
            Exit Sub
        End If

        'picMainSepTop.Visible = True
        trvCategory.Visible = True
        SplitterMainCategory.Visible = True
        dgData.Visible = True


        Select Case Trim(trvAudit.SelectedNode.Text)


            Case "Report"
                If trvAudit.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
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
                pnlClientUpdateDetailsFilter.Visible = False
                Panel8.Visible = True


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



                'If (CheckArchiveDB() = False) Then
                '    Exit Sub
                'End If
                ''Sandip Darade  20100127 to check the availability of archive database
                pnlClientUpdateDetailsFilter.Visible = False
                Panel8.Visible = True
                If trvAudit.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If

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
                Else
                    Exit Sub
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


                'If (CheckArchiveDB() = False) Then
                '    Exit Sub
                'End If
                '---------------------------------------------------------------
                pnlClientUpdateDetailsFilter.Visible = False
                Panel8.Visible = True
                If pnl_tlsp_Top.Visible = False Then
                    pnl_tlsp_Top.Visible = True
                End If
                If trvAudit.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If
                btnShowAudit.Visible = False
                _blnSearch = True
                ''Sandip Darade  20100127 to check the availability of archive database

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
                    '20100511
                    'Call Fill_ArchivedAuditCategories()
                    'If IsNothing(trvCategory.Nodes(0).Nodes(0)) = False 
                    If IsNothing(trvCategory.Nodes(0)) = False Then

                        trvCategory.SelectedNode = trvCategory.Nodes(0)
                        Call Fill_ArchivedAuditCategories()
                        Call Fill_DetailsArchivedAuditReports()
                    Else
                        dgData.DataSource = Nothing
                    End If
                Else
                    Exit Sub
                End If
                '---------------------------------------------------------------

            Case "Restore Archive"
                'If (CheckArchiveDB() = False) Then
                '    Exit Sub
                'End If
                If trvAudit.SelectedNode.Text = "Archived Audit Report" Then
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Show
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Show"
                    btnRefresh.ToolTipText = "Show"
                Else
                    btnRefresh.Image = Global.gloEMRAdmin.My.Resources.Resources.Refresh
                    btnRefresh.ImageAlign = ContentAlignment.MiddleCenter
                    btnRefresh.Text = "Refresh"
                    btnRefresh.ToolTipText = "Refresh"
                End If

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
                ''Sandip Darade  20100127 to check the availability of archive database

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
                    If Not IsNothing(frmArchive) Then   'Obj Disposed by Mitesh 
                        frmArchive.Dispose()
                        frmArchive = Nothing
                    End If
                Else
                    Exit Sub
                End If

                '---------------------------------------------------------------
        End Select

        '     pnlAuditLogSearch.Visible = False

    End Sub
    ''Sandip Darade 20100127
    Public Function CheckArchiveDB() As Boolean

        Dim objSQLSettings As New clsStartUpSettings
        Try

            If (gblnSQLAuthentication = True) Then ''if sql authentication
                If objSQLSettings.IsConnect(gstrSQLServerName, gstrArchiveDatabaseName, True, gstrSQLUserEMR, gstrSQLPasswordEMR) = False Then

                    MessageBox.Show("Set the proper archive database from startup settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False

                End If

            Else ''if windows authentication
                If objSQLSettings.IsConnect(gstrSQLServerName, gstrArchiveDatabaseName, False, "", "") = False Then

                    MessageBox.Show("Archive database not set. Set archive database from Startup settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception

        Finally
            objSQLSettings = Nothing

        End Try
    End Function


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
        ' btnDelete.Text = "Delete"

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

    ''Added Rahul for AuditRights on 20101019
    Public Sub AuditRightsSearchNode(ByVal Trv As TreeView, ByVal strText As String)
        Dim trvNde As TreeNode
        For Each trvNde In Trv.Nodes
            If trvNde.Tag = Convert.ToInt64(strText) Then
                trvSearchNode = trvNde
            End If
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
        If Not IsNothing(frm) Then   'Obj Disposed by Mitesh 
            frm.Dispose()
            frm = Nothing
        End If
    End Sub



    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        Try
            '30-Mar-17 Aniket: Bug #104867: gloEMR : Aoto Archiev audit job : when user click on ArchAuditRpt button View Tempered log button get disappear
            'btnViewHistory.Visible = False
            btnATNALog.Visible = False
            'btnAuditLogTampering.Visible = False
            Select Case Trim(e.ClickedItem.Tag)
                Case "WindowsGroupsUsers"
                    'Windows Groups & Users
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(0)
                    Call ShowAdministrator()
                Case "gloEMRGroups"
                    'gloEMR Groups
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
                    'line added by dipak 20091008 as Node index is change as provider Type Node is removed for Bug 3987 DoctorType
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(3)
                    'line commented by dipak 20091008 
                    'trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(4)
                    'end dipak 2001008
                    'trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(5)
                    '******By Sandip Deshmukh 18 Oct 07 2.23PM Bug# 350
                    Call ShowAdministrator()
                Case "Machines"
                    'Client Settings
                    '******By Sandip Deshmukh 18 Oct 07 12.01PM Bug# 349
                    '******The code is changed to show the proper window on toolstrip click 
                    '******previous it shows an provider-referral setting instead client settings
                    'line added by dipak 20091008 as Node index is change as provider Type Node is removed for Bug 3987 DoctorType
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(8)
                    'line commented by dipak 20091008 
                    ' trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(0).Nodes(8)
                    'end dipak 20091008
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
                    'trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(1).Nodes(1)
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(1).Nodes(0)

                    Call ShowAdministrator()
                Case "VoiceTrainingDocument"
                    'Voice Training Document
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(2).Nodes(3)
                    Call ShowAdministrator()
                Case "AuditReport"
                    'Audit Reports
                    Me.bIsAuditLogReport = True ' Bug : #107847 (gloEMR Admin: After clicking on Audit log, the Pointer is not opening for that modified record)
                    trvAudit.SelectedNode = trvAudit.Nodes(0).Nodes(0)
                    Call ShowAudits()
                Case "ArchiveAudit"
                    'Archive Audit
                    If (GetArchiveConnectionString() = "") Then
                        Exit Sub
                    End If
                    If (CheckArchiveDB() = False) Then
                        Exit Sub
                    End If
                    trvAudit.SelectedNode = trvAudit.Nodes(0).Nodes(1)
                    Call ShowAudits()
                Case "ArchivedReport"
                    If (GetArchiveConnectionString() = "") Then
                        Exit Sub
                    End If
                    If (CheckArchiveDB() = False) Then
                        Exit Sub
                    End If
                    'Archived Audit Report
                    trvAudit.SelectedNode = trvAudit.Nodes(1).Nodes(0)
                    Call ShowAudits()
                Case "RestoreArchive"
                    'CHECK WHETHER ARCHIVE DATABASE IS SET OR NOT
                    If (GetArchiveConnectionString() = "") Then
                        Exit Sub
                    End If
                    'CHECK THE DATABASE SETTING FOR ARCHIVE 
                    If (CheckArchiveDB() = False) Then
                        Exit Sub
                    End If
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
                    If Not IsNothing(frm) Then   'Obj Disposed by Mitesh 
                        frm.Dispose()
                        frm = Nothing
                    End If
                Case "LockScreen"
                    'Lock Screen
                    Dim frm As New frmLockScreen
                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.LockScreen, "'" & gstrLoginName & "' has Locked Screen.", gstrLoginName, gstrClientMachineName, 0, True)
                    objAudit = Nothing
                    frm.ShowDialog()
                    If Not IsNothing(frm) Then   'Obj Disposed by Mitesh 
                        frm.Dispose()
                        frm = Nothing
                    End If

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
                    If Not IsNothing(frmSettings) Then   'Obj Disposed by Mitesh 
                        frmSettings.Dispose()
                        frmSettings = Nothing
                    End If
                Case "Close"
                    'Close
                    Me.Close()

                    'sarika  21 feb
                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.Logout, "'" & gstrLoginName & "' successfully logged out", gstrLoginName, gstrClientMachineName, 0, True)
                    objAudit = Nothing
                    '-------------

                Case "LogOut"

                    'sarika  21 feb
                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.Logout, "'" & gstrLoginName & "' successfully logged out", gstrLoginName, gstrClientMachineName)
                    objAudit = Nothing
                    '-------------

                    'Me.Hide()
                    'Dim frm As New frmServerInstallation
                    'frm.ShowDialog()
                    ''Added by Mayuri:20100222-
                    If tsbtnLogout.Tag = "LogOut" Then
                        _ShowSplashScreen = True
                        Me.Hide()
                        Dim frm As New frmSplash
                        frm.Visible = True

                    Else
                        _ShowSplashScreen = False
                    End If
                    ''end code Added by Mayuri:20100222

                    ''//Code added by Ravikiran on 10/02/2007
                Case "RxReportDesigner"
                    Dim frm As New frmRxReportDesigner
                    frm.ShowDialog()
                    If Not IsNothing(frm) Then   'Obj Disposed by Mitesh 
                        frm.Dispose()
                        frm = Nothing
                    End If
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
                    If Not IsNothing(frmLocStatus) Then   'Obj Disposed by Mitesh 
                        frmLocStatus.Dispose()
                        frmLocStatus = Nothing
                    End If

                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, gstrLoginName & " user has viewed the Clinic WorkFlow Settings.", gstrLoginName, gstrClientMachineName, 0, True)
                    objAudit = Nothing
                Case "Claim Validation Setting"
                    'Dim frm As New frmEDISettings(gstrConnectionString)
                    'frm.ShowDialog()
                    trvAdminMenu.SelectedNode = trvAdminMenu.Nodes(1).Nodes(0)
                    '------------------------------
                    Call ShowAdministrator()
                Case "User Guide"
                    Try
                        Dim helpFileName As String = System.IO.Path.Combine(Application.StartupPath, "help\gloEMR_Admin_User_Manual.chm")
                        If System.IO.File.Exists(helpFileName) Then
                            System.Windows.Forms.Help.ShowHelp(Me, "file://" & helpFileName, "Welcome.htm") 'Welcome_User_Manual.htm
                            System.Windows.Forms.Help.ShowHelp(Me, "file://" & helpFileName, HelpNavigator.TableOfContents, "Welcome.htm")
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Case "SyncTime"
                    Dim ofrmSyncTime As New frmSyncTime
                    ofrmSyncTime.ShowDialog()
                    If Not IsNothing(ofrmSyncTime) Then
                        ofrmSyncTime.Dispose()
                        ofrmSyncTime = Nothing
                    End If
            End Select


        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                    '.ImageIndex = 4
                    '.SelectedImageIndex = 4
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
                    '  .ImageIndex = 4
                    ' .SelectedImageIndex = 4
                    '  .ForeColor = Color.Black
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
        conn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
    '''''''Added on 20100701 by sanjog to show UserName and password 
    ''Handles dgData.DoubleClick
    Private Sub dgData_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        btnModify_Click(sender, e)
    End Sub
    '''''''Added on 20100701 by sanjog to show UserName and password 

    Private Sub dgData_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgData.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As DataGrid.HitTestInfo = dgData.HitTest(ptPoint)

        'Try

        '    ' If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then
        '    ''Sandip Darade 20090912 
        '    ''Commented to implement genaralised search
        '    ''Select Case htInfo.Column
        '    ''    'Case 1
        '    ''    '    lblSearch.Text = "Activity Date"
        '    ''    Case 2
        '    ''        lblSearch.Text = "Software Component"
        '    ''    Case 3
        '    ''        lblSearch.Text = "Machine"
        '    ''    Case 4
        '    ''        lblSearch.Text = "User"
        '    ''    Case 5
        '    ''        lblSearch.Text = "Category"
        '    ''    Case 6
        '    ''        lblSearch.Text = "Patient Code"
        '    ''    Case 7
        '    ''        lblSearch.Text = "Patient"
        '    ''    Case 8
        '    ''        lblSearch.Text = "Description"
        '    ''    Case 9
        '    ''        lblSearch.Text = "Outcome"
        '    ''End Select




        '    If txtInstringSearch.Text = "" Then
        '        _blnSearch = True
        '    Else
        '        _blnSearch = False
        '        txtInstringSearch.Text = ""
        '        _blnSearch = True
        '    End If
        '    'ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
        '    '    '    _blnSearch = True
        '    '    '    Call UpdateCategory()

        '    '    _blnSearch = True
        '    '    Select Case htInfo.Column.ToString() 'DataGrid.HitTestType.Cell
        '    '        'Case 1
        '    '        '    lblSearch.Text = "Activity Date"
        '    '        Case 2
        '    '            lblSearch.Text = "Software Component"
        '    '        Case 3
        '    '            lblSearch.Text = "Machine"
        '    '        Case 4
        '    '            lblSearch.Text = "User"
        '    '        Case 5
        '    '            lblSearch.Text = "Category"
        '    '        Case 6
        '    '            lblSearch.Text = "Patient Code"
        '    '        Case 7
        '    '            lblSearch.Text = "Patient"
        '    '        Case 8
        '    '            lblSearch.Text = "Description"
        '    '        Case 9
        '    '            lblSearch.Text = "Outcome"
        '    '    End Select

        '    '  End If
        'Catch ex As Exception
        'End Try

        Try


            If (htInfo.Column >= 0) Then

                If (dgData.CurrentRowIndex < 0 And Trim(trvAdminMenu.SelectedNode.Text) <> "User Groups") Or htInfo.Row = -1 Then
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
                            ''Added Rahul for AuditRights on 20101019
                            frmgloEMRuser.Fill_AuditRights()
                            ''End
                            frmgloEMRuser.blnModify = True
                            Call frmgloEMRuser.Fill_gloEMRGroups()
                            Call frmgloEMRuser.Fill_MaritalStatus()
                            Call frmgloEMRuser.Fill_Gender()
                            Call frmgloEMRuser.Fill_Providers()
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
                            frmgloEMRuser.txtStreet.Text = objUser.Street

                            ''Task #67507: gloEMR & Patient Portal Send Message screen changes
                            frmgloEMRuser.txtPortalDisplayName.Text = objUser.PortalDisplayName

                            frmgloEMRuser.txtWindowsLoginName.Text = objUser.WindowLoaginName
                            'frmgloEMRuser.txtAddress.Text = objUser.Address
                            'frmgloEMRuser.txtAddress2.Text = objUser.Address2
                            'frmgloEMRuser.txtCity.Text = objUser.City
                            'fillStates(frmgloEMRuser.cmbState)
                            'frmgloEMRuser.cmbState.Text = objUser.State

                            'frmgloEMRuser.isFormLoading = True
                            'frmgloEMRuser.txtZip.Text = objUser.ZIP
                            'frmgloEMRuser.isFormLoading = False

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

                            ''------------
                            frmgloEMRuser.txtPhoneNo.Text = objUser.PhoneNo
                            frmgloEMRuser.txtMobileNo.Text = objUser.MobileNo
                            frmgloEMRuser.txtFax.Text = objUser.FAX
                            frmgloEMRuser.txtEmailAddress.Text = objUser.Email
                            frmgloEMRuser.chkgloEMRAdmin.Checked = objUser.gloEMRAdministrator
                            frmgloEMRuser.chkAuditTrails.Checked = objUser.IsAuditTrail
                            frmgloEMRuser.chkAccessDenied.Checked = objUser.AccessDenied

                            frmgloEMRuser.chkCountforCPOE.Checked = objUser.ISCountforCPOE

                            'frmgloEMRuser.picSignature.Image = objUser.Signature
                            If IsNothing(objUser.Signature) = False Then
                                frmgloEMRuser.picSignature.Image = CType(objUser.Signature, Image)
                                frmgloEMRuser.picSignature.SizeMode = PictureBoxSizeMode.CenterImage '// Strech
                            End If

                            'Developer: Mitesh Patel
                            'Date:03-Jan-2012'
                            'PRD: Direct Ability
                            frmgloEMRuser.txtAbilityEmail.Text = objUser.AbilityEmail
                            frmgloEMRuser.txtAbilityPassword.Text = objEncryption.DecryptFromBase64String(objUser.AbilityPassword, constEncryptDecryptKey)
                            '-----------x---

                            ''' Added by Ujwala Atre as on 20101004 - Added Emergency Access to patient chart
                            frmgloEMRuser.ChkEmergencyAccess.Checked = objUser.EAPChart
                            If (objUser.EAPChart = True) Then
                                frmgloEMRuser.txtEAPassword.Text = objEncryption.DecryptFromBase64String(objUser.EAPassword, constEncryptDecryptKey)
                                frmgloEMRuser.txtEAConfirmPassword.Text = frmgloEMRuser.txtEAPassword.Text
                                '' Added Valid upto Date for Emergency Access Password as on 12032010
                                frmgloEMRuser.dtpValidupto.Value = objUser.ValidDt
                                '' Added Valid upto Date for Emergency Access Password as on 12032010
                            End If

                            objEncryption = Nothing

                            ''' Added by Ujwala Atre as on 20101004 - Added Emergency Access to patient chart


                            ''''Exchange user
                            frmgloEMRuser.chkExchnageUser.Checked = objUser.IsExchangeUser

                            frmgloEMRuser.txtExchangeLogin.Text = objUser.ExchangeLogin
                            frmgloEMRuser.txtExchangePwd.Text = objUser.ExchangePassward
                            frmgloEMRuser.txtExchangePwdConfirm.Text = objUser.ExchangePassward

                            ''''
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
                            frmgloEMRuser.cmbProvider.SelectedValue = objUser.ProviderID
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
                            frmgloEMRuser.trvUserRights.ExpandAll()
                            ''Added Rahul for AuditRights on 20101019
                            Dim clAuditRights As New Collection
                            clAuditRights = objUser.AuditRights
                            Dim nAuditTotalNodes As Int16
                            nAuditTotalNodes = frmgloEMRuser.trvAuditRights.GetNodeCount(False) - 1
                            For nCount = 1 To clAuditRights.Count
                                For nCount1 = 0 To nAuditTotalNodes
                                    AuditRightsSearchNode(frmgloEMRuser.trvAuditRights, clAuditRights.Item(nCount))
                                    If IsNothing(trvSearchNode) = False Then
                                        trvSearchNode.Checked = True
                                        frmgloEMRuser.trvAuditRights.SelectedNode = trvSearchNode
                                        'frmgloEMRuser.trvUserRights.SelectedNode.Checked = True
                                    End If
                                Next
                            Next
                            objUser = Nothing
                            ''End
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
                            If Not IsNothing(frmgloEMRuser) Then   'Obj Disposed by Mitesh 
                                frmgloEMRuser.Dispose()
                                frmgloEMRuser = Nothing
                            End If
                            Exit Sub

                            ''code added by pradeep on 16/06/2010 for prefix
                        Case "Site Prefix"
                            btnModify_Click(Nothing, Nothing)

                            ''Added Rahul on 20101110
                        Case "User Groups"
                            btnModify_Click(Nothing, Nothing)
                            ''End
                        Case "API Access"
                            btnModify_Click(Nothing, Nothing)

                    End Select

                ElseIf enmUserOperation = enmOperation.Tools Then

                ElseIf enmUserOperation = enmOperation.Audit Then

                    If bIsAuditLogReport = False Then
                        Exit Try
                    End If

                    Dim nTransID As Int64 = Nothing
                    Dim sActivityType As String = Nothing
                    Dim sActivityModule As String = Nothing
                    Dim sDescriptionText As String = Nothing
                    Dim nPatientID As Int64 = Nothing
                    Dim nAuditTrailID As Int64 = Nothing
                    Dim sCategoryType As String = Nothing
                    Dim sDescription As String = Nothing
                    nAuditTrailID = Convert.ToInt64(dgData.Item(htInfo.Row, 0))
                    nTransID = Convert.ToInt64(dgData.Item(htInfo.Row, 11))
                    sActivityModule = Convert.ToString((dgData.Item(htInfo.Row, 12)))
                    sActivityType = Convert.ToString((dgData.Item(htInfo.Row, 5)))
                    sDescriptionText = Convert.ToString((dgData.Item(htInfo.Row, 9)))
                    nPatientID = Convert.ToInt64((dgData.Item(htInfo.Row, 13)))
                    sCategoryType = Convert.ToString((dgData.Item(htInfo.Row, 6)))
                    sDescription = Convert.ToString((dgData.Item(htInfo.Row, 2)))
                     Select sActivityModule
                        Case "Patient", "History", "ProblemList", "Medication", "Vitals", "Exam", "Immunization", "CarePlan", "Prescription", "Admin", "CCD", "Labs", "Orders", "SocialPsychologicalBehavioralobservations", "ImplantableDevice"
                            If (sActivityType = "Add" Or sActivityType = "Modify" Or sActivityType = "Delete") Then
                                If nTransID <> 0 And sActivityModule <> "Patient" Then
                                    If sActivityModule = "Labs" Then
                                        OpenAduitTrail(nTransID, sActivityType, sActivityModule)
                                    ElseIf sActivityModule = "CarePlan" Then
                                        OpenAduitTrail(nTransID, sActivityType, sActivityModule, sCategoryType, sDescription, nAuditTrailID)
                                    Else
                                        If sActivityType = "Modify" Then
                                            If sActivityModule = "Exam" Then
                                                OpenAduitTrail(nTransID, sActivityType, sActivityModule)
                                            ElseIf sActivityModule = "Admin" AndAlso (sCategoryType = "ModifyUserRights" Or sCategoryType = "ChangesToUserPrivileges" Or sCategoryType = "AddUserRights" Or sCategoryType = "AdditionOfUserPrivileges") Then
                                                OpenAduitTrail(nTransID, sActivityType, sActivityModule)
                                            Else
                                                OpenAduitTrail(nAuditTrailID, sActivityType, sActivityModule, sCategoryType, sDescription)
                                            End If
                                        Else
                                            OpenAduitTrail(nTransID, sActivityType, sActivityModule, sCategoryType, sDescription)
                                        End If
                                    End If
                                ElseIf nTransID <> 0 And sActivityModule = "Patient" And sCategoryType = "PatientEducation" And sActivityType = "Add" Then
                                    OpenAduitTrail(nTransID, sActivityType, sActivityModule, sCategoryType, sDescription)
                                ElseIf sActivityModule = "SocialPsychologicalBehavioralobservations" And (sActivityType = "Modify" Or sActivityType = "Add") Then
                                    OpenAduitTrail(nAuditTrailID, sActivityType, sActivityModule, sCategoryType, sDescription)
                                Else
                                    If sCategoryType = "SetupPatient" Then
                                        OpenAduitTrail(nPatientID, sActivityType, sActivityModule, nAuditTrailID)
                                    End If
                                End If
                            End If

                        Case Else
                    End Select
                End If


            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub OpenAduitTrail(ByVal nTransactionID As Int64, ByVal sActivityType As String, ByVal ActivityModule As String, Optional ByVal activitycategory As String = "", Optional ByVal description As String = "", Optional ByVal nAuditTrailID As Int64 = 0)
        Dim frm = Nothing
        If activitycategory = "PatientEducation" Then
            ActivityModule = "PatientEducation"
        End If

        If ActivityModule = "Labs" Then
            frm = New frmVWAudit_Labs(nTransactionID, sActivityType, ActivityModule)
        ElseIf ActivityModule = "CarePlan" Then
            Dim sModule As String = ""
            Select Case activitycategory
                Case gloAuditTrail.ActivityCategory.PatientHealthConcern.ToString()
                    sModule = "HealthConcern"
                Case gloAuditTrail.ActivityCategory.PatientGoal.ToString()
                    sModule = "Goal"
                Case gloAuditTrail.ActivityCategory.PatientIntervention.ToString()
                    sModule = "Intervention"
                Case gloAuditTrail.ActivityCategory.PatientOutcome.ToString()
                    sModule = "Outcome"
            End Select
            If sModule = "" Then
                If sActivityType = "Modify" Then
                    frm = New frmVWAudit(nAuditTrailID, sActivityType, ActivityModule, activitycategory, description)
                Else
                    frm = New frmVWAudit(nTransactionID, sActivityType, ActivityModule, activitycategory, description)
                End If
            Else
                frm = New frmCarePlanHistory(0, nTransactionID, sModule)
                Try
                    frm.Width = Screen.PrimaryScreen.WorkingArea.Width '- 40
                Catch

                End Try
            End If
        Else
            If ActivityModule = "Patient" Then
                frm = New frmVWAudit(nTransactionID, sActivityType, ActivityModule, activitycategory, nAuditTrailID, description)
            Else
                frm = New frmVWAudit(nTransactionID, sActivityType, ActivityModule, activitycategory, description)
            End If
        End If

        frm.StartPosition = FormStartPosition.CenterScreen
        frm.BringToFront()
        If ActivityModule = "ProblemList" Then
            frm.Text = "Problem List Audit"
        ElseIf ActivityModule = "Vitals" Then
            frm.Text = "Vitals Audit"
        ElseIf ActivityModule = "CarePlan" Then
            frm.Text = "Care Plan Audit"
        ElseIf ActivityModule = "Medication" Then
            frm.Text = "Medication Audit"
        ElseIf ActivityModule = "Prescription" Then
            frm.Text = "Prescription Audit"
        ElseIf ActivityModule = "History" Then
            frm.Text = "History Audit"
        ElseIf ActivityModule = "Immunization" Then
            frm.Text = "Immunization Audit"
        ElseIf ActivityModule = "Patient" Then
            frm.Text = "Patient Audit"
        ElseIf ActivityModule = "Exam" Then
            frm.Text = "Exam Audit"
        ElseIf ActivityModule = "Admin" Then
            frm.MaximizeBox = True
            frm.MinimizeBox = True
            frm.Text = "User Rights Audit"
        ElseIf ActivityModule = "CCD" AndAlso activitycategory = "Reconciliation" Then
            frm.Text = "CCDA Reconcile Audit"
        ElseIf ActivityModule = "Labs" Then
            frm.Text = "Lab Audit"
        ElseIf ActivityModule = "PatientEducation" Then
            frm.Text = "Patient Education Audit"
        ElseIf ActivityModule = "ImplantableDevice" Then
            frm.text = "Implantable Devices Audit"
        End If

        frm.ShowDialog(Me)
    End Sub

    Private Sub txtInstringSearch_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtInstringSearch.KeyDown
        If e.KeyValue = Keys.Enter Then
            Exit Sub
        End If
        _CurrentTime = DateTime.Now
        timer_1.Stop()
        timer_1.Interval = 1200
        timer_1.Enabled = True
    End Sub
    Private Sub SearchEvent() Handles timer_1.Tick

        Dim objAudit As clsAudit

        Dim strSearchDetails As String
        strSearchDetails = txtInstringSearch.Text
        timer_1.Stop()
        If trvAudit.SelectedNode.Text <> "Report" Then
            Fill_DetailsArchivedAuditReports()
        Else
            Fill_DetailsAuditReports()
        End If


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

                If Trim(txtInstringSearch.Text) <> "" Then
                strSearchDetails = strSearchDetails.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]")
                    strSearchDetails = strSearchDetails.Replace("*", "[*]")
                Else
                    strSearchDetails = ""
                End If
                

                If trvAudit.SelectedNode.Text <> "Report" Then

                    dvAuditLogReport.RowFilter = dvAuditLogReport.Table.Columns(2).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(3).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(6).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(7).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(8).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(9).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(10).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(11).ColumnName & " Like '%" & strSearchDetails & "%' "

                Else
                    dvAuditLogReport.RowFilter = dvAuditLogReport.Table.Columns(2).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(3).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(4).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(5).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(6).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(7).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(8).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(9).ColumnName & " Like '%" & strSearchDetails & "%' OR " _
                                                & dvAuditLogReport.Table.Columns(10).ColumnName & " Like '%" & strSearchDetails & "%' "

                End If

                dgData.DataSource = dvAuditLogReport
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
   

    Private Sub txtInstringSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInstringSearch.TextChanged
        Dim objAudit As clsAudit
        Try
            If timer_1 IsNot Nothing Then
                With timer_1
                    .Stop()
                    .Interval = 1200
                    .Start()
                End With
            End If

        Catch ex As Exception
            objAudit = New clsAudit

            objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "User failed to view audit reports for records with : " & lblSearch.Text & " like : '" & txtInstringSearch.Text.Trim & "'", gstrLoginName, gstrClientMachineName, 0, True, clsAudit.enmOutcome.Failure)
            objAudit = Nothing

            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        

    End Sub


#End Region
    '-----
    Private Sub Fill_ClearingHouse()
        Dim dtLogin As New DataTable
        Dim objLogin As New clsLogin
        dtLogin = GetClearingHouse()
        objLogin = Nothing
        dgData.DataSource = dtLogin
        dgData.CaptionText = "Clearing House"

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

        Dim col3 As New DataGridTextBoxColumn
        With col3
            .HeaderText = "Receiver ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtLogin.Columns(2).ColumnName
            .NullText = ""
            .Width = 0.14 * dgData.Width
        End With

        Dim col4 As New DataGridTextBoxColumn
        With col4
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
        Dim col9 As New DataGridTextBoxColumn
        With col9
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

        Dim col11 As New DataGridTextBoxColumn
        With col11
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
            .Width = 0.14 * dgData.Width
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {col1, col2, col3, col4, col5, col9, col11, col15})
        dgData.TableStyles.Clear()
        dgData.TableStyles.Add(grdTableStyle)

    End Sub
    Private _ClinicID As Int64 = 1
    'code added by pradeep 0n 16/06/2010 for 
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
& " CASE ISNULL(bIsISA,'FALSE') WHEN 'TRUE' THEN 'YES' WHEN 'FALSE' THEN 'NO' END AS bIsISA   FROM BL_ClearingHouse_MST  " 'WHERE nClinicID = " & _ClinicID & " "

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


    Private Sub trvAdminMenu_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvAdminMenu.AfterSelect

    End Sub

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
    Private Sub cmbAuditCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAuditCategory.SelectedIndexChanged
        If txtInstringSearch.Text <> "" Then
            txtInstringSearch.Text = ""
        End If
    End Sub

    Private Sub trvCategory_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvCategory.NodeMouseDoubleClick
        Dim oTrvnode As TreeView = CType(sender, TreeView)
        Dim oNode As TreeNode = oTrvnode.SelectedNode
        If oNode.Nodes.Count = 0 Then
            btnModify_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnATNALog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnATNALog.Click
        Dim obj As ClsATNALog
        Dim ofile As FileInfo
        Dim _FilePath As String = ""
        Dim objDataBaseLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim objDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            If Not IsNothing(dgData) Then
                If Not IsNothing(dgData.CurrentRowIndex) Then
                    obj = New ClsATNALog()
                    Dim _AuditID As String = dgData.Item(dgData.CurrentRowIndex, 0)
                    If Not IsNothing(_AuditID) And _AuditID <> "" Then
                        Dim dt As New DataTable

                        objDBParameters.Add(New gloDatabaseLayer.DBParameter("@AuditTrailID", _AuditID, ParameterDirection.Input, SqlDbType.BigInt))
                        objDataBaseLayer.Connect(False)
                        objDataBaseLayer.Retrive("gsp_GetATNALog", objDBParameters, dt)
                        objDataBaseLayer.Disconnect()
                        objDataBaseLayer.Dispose()
                        objDataBaseLayer = Nothing

                        If Not IsNothing(dt) Then
                            _FilePath = obj.CreateATNALogXMLFILE(dt)
                            If _FilePath <> "" Then
                                Dim objfrmATNALog As frmATNALog = New frmATNALog()
                                ofile = New FileInfo(_FilePath)
                                objfrmATNALog.ATNAWebBrowser.Navigate(ofile.FullName)
                                objfrmATNALog.ShowDialog()
                                If Not IsNothing(objfrmATNALog) Then   'Obj Disposed by Mitesh 
                                    objfrmATNALog.Dispose()
                                    objfrmATNALog = Nothing
                                End If
                            End If
                        End If
                        If Not IsNothing(dt) Then
                            dt.Dispose()
                            dt = Nothing
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        Finally
            If Not IsNothing(obj) Then
                obj = Nothing
            End If
            If Not IsNothing(ofile) Then
                ofile.Delete()
            End If
        End Try
    End Sub

    'funcation modifyed by manoj jadhav on 20111015 for checking ECG device settings
    Private Function GetECGInterfaceStatus() As Boolean
        'Dim oDbLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        'Dim strValue As String = String.Empty
        'Dim blnResult As Boolean = False
        'Try
        '    oDbLayer.Connect(False)
        '    strValue = Convert.ToString(oDbLayer.ExecuteScalar_Query("select sSettingsValue from Settings where Upper(sSettingsName) ='ECGENABLED'"))
        '    If strValue.Trim().Length = 0 Then
        '        blnResult = False
        '    Else
        '        blnResult = Convert.ToBoolean(strValue)
        '    End If
        '    oDbLayer.Disconnect()
        '    Return blnResult
        'Catch ex As Exception
        '    Return False
        'Finally
        '    If Not IsNothing(oDbLayer) Then
        '        oDbLayer.Dispose()
        '        oDbLayer = Nothing
        '    End If
        'End Try
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim sqlQury As String = String.Empty
        Dim Result As Integer = 0
        Try
            sqlQury = "select count(nsettingsId) from Settings where Upper(sSettingsName) IN ('ECGENABLED','USEWELCHALLYNECGDEVICE') AND sSettingsValue ='True'"
            oDbLayer.Connect(False)
            Integer.TryParse(Convert.ToString(oDbLayer.ExecuteScalar_Query(sqlQury)), Result)
            oDbLayer.Disconnect()
            If Result > 0 Then
                GetECGInterfaceStatus = True
            Else
                GetECGInterfaceStatus = False
            End If
        Catch ex As Exception
            ex = Nothing
            GetECGInterfaceStatus = False
        Finally
            sqlQury = String.Empty
            Result = 0
            If Not IsNothing(oDbLayer) Then
                oDbLayer.Dispose()
                oDbLayer = Nothing
            End If
        End Try
    End Function
    'funcation modifyed by manoj jadhav on 20111015 for checking ECG device settings


    Private Function validate_Labusers(ByVal dtTemp As DataTable, ByVal _groupID As Long) As String
        Dim sMessage As String = String.Empty
        Dim str As String = String.Empty
        Try


            strUserID = String.Empty
            For i As Integer = 0 To dtTemp.Rows.Count - 1
                strUserID = strUserID & dtTemp.Rows(i)("nUserID").ToString() & ","
            Next

            Dim newDt As New DataTable

            If strUserID <> "" Then
                strUserID = strUserID.Remove((strUserID.Length - 1), 1)
                Dim oUserGroups As New clsUserGroups
                newDt = oUserGroups.Check_LabUserTask(_groupID.ToString(), strUserID)
                oUserGroups = Nothing
            End If


            If Not IsNothing(newDt) Then
                For i As Integer = 0 To newDt.Rows.Count - 1
                    str = str & vbCrLf & newDt.Rows(i)("sProviderName").ToString() & ":- " & newDt.Rows(i)("sUserName").ToString()
                Next
            End If


            If Not IsNothing(newDt) Then
                newDt.Dispose()
                newDt = Nothing
            End If


            If str <> "" Then
                sMessage = "Following users are assigned for Lab User Tasks." & vbCrLf & "Removing them from User/Groups will remove them from Lab User Tasks also. " & vbCrLf & str & vbCrLf & vbCrLf & "Do you want to continue?"
            End If

            Return sMessage
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""

        End Try
    End Function

    ''Added for Patient portal dnyamics form on 20130325
    Private Function getHealthFormSetting() As Boolean
        'Dim ogloSettings As clsSettings
        'Dim _result As Boolean = False
        'Dim isPortalEnable As Object = Nothing
        'Try
        '    ogloSettings = New clsSettings()
        '    'ogloSettings.GetSetting("HealthForms", 0, gnClinicID, _result)
        '    ogloSettings.GetSetting("PatientPortalEnabled", gnLoginID, gnClinicID, isPortalEnable)

        '    If isPortalEnable.ToLower() = "true" Then
        '        _result = True
        '    End If


        '    Return _result
        'Catch ex As Exception
        '    Return False
        'Finally
        '    ogloSettings = Nothing
        'End Try
        Return True
    End Function
    ''End Patient portal dnyamics form

    Private Sub btnAuditLogTampering_Click(sender As Object, e As System.EventArgs) Handles btnAuditLogTampering.Click

        Dim objDataBaseLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim objDBParameters As New gloDatabaseLayer.DBParameters()
        Dim nTamperingcount As Integer

        objDBParameters.Add(New gloDatabaseLayer.DBParameter("@GetCountOnly", 0, ParameterDirection.Input, SqlDbType.Int))

        objDataBaseLayer.Connect(False)
        nTamperingcount = objDataBaseLayer.ExecuteScalar("gsp_RetrieveAuditLogTampering", objDBParameters)
        objDataBaseLayer.Disconnect()

        objDataBaseLayer.Dispose()
        objDataBaseLayer = Nothing

        If nTamperingcount > 0 Then
            Dim frmAuditLogTampered As frmAuditLogTampered = frmAuditLogTampered.GetInstance
            frmAuditLogTampered.ShowDialog()
            frmAuditLogTampered.BringToFront()
            lblLogTamperedStatus.Visible = False
        Else
            MsgBox("No Audit Log tampering was detected.", MsgBoxStyle.Information)
        End If

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
            Me.Cursor = Cursors.Default
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
            Me.Cursor = Cursors.Default
        Finally

        End Try
    End Sub

    
    Private Sub btnViewHistory_Click(sender As Object, e As System.EventArgs) Handles btnViewHistory.Click
        Try
            If dgData.CurrentRowIndex > -1 Then
                If (dgData.IsSelected(dgData.CurrentRowIndex)) Then
                    Dim frmUserHistory As New frmUserHistory(dgData.Item(dgData.CurrentRowIndex, 0))
                    frmUserHistory.WindowState = FormWindowState.Normal
                    frmUserHistory.StartPosition = FormStartPosition.CenterScreen
                    frmUserHistory.ShowInTaskbar = False
                    frmUserHistory.ShowDialog(IIf(IsNothing(frmUserHistory.Parent), Me, frmUserHistory.Parent))
                    Me.Cursor = Cursors.Arrow
                    frmUserHistory.Dispose()
                    frmUserHistory = Nothing
                Else
                    MessageBox.Show("Please select the user.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
 


    End Sub

   
    Private Sub lblLogTamperedStatus_Click(sender As System.Object, e As System.EventArgs) Handles lblLogTamperedStatus.Click
        Try
            Dim frmAuditLogTampered As frmAuditLogTampered = frmAuditLogTampered.GetInstance
            'frmAuditLogTampered.tamperedlogstatus = True
            frmAuditLogTampered.ShowDialog()
            lblLogTamperedStatus.Visible = False
            'frmAuditLogTampered.BringToFront()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub ShowLogTamperingStatus()
        Try
            Dim objDataBaseLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
            Dim objDBParameters As New gloDatabaseLayer.DBParameters()
            Dim nTamperingcount As Integer

            objDBParameters.Add(New gloDatabaseLayer.DBParameter("@GetCountOnly", 2, ParameterDirection.Input, SqlDbType.Int))

            objDataBaseLayer.Connect(False)
            nTamperingcount = objDataBaseLayer.ExecuteScalar("gsp_RetrieveAuditLogTampering", objDBParameters)
            objDataBaseLayer.Disconnect()

            objDataBaseLayer.Dispose()
            objDataBaseLayer = Nothing
            If nTamperingcount > 0 Then
                lblLogTamperedStatus.Visible = True
            Else
                lblLogTamperedStatus.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
     
    End Sub

#Region "API access"
    Private Sub Fill_APICategory()
        pnlCommandButtons.Visible = True
        btnNew.Visible = True
        btnNew.Text = "New"
        btnModify.Visible = True
        btnModify.Text = "Modify"
        btnDelete.Visible = True
        btnDelete.Text = "Block"
        btnDelete.ToolTipText = "Block"
        '   btnViewHistory.Visible = False
        '  btnRefresh.Visible = False
        With trvCategory
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvChild As TreeNode
            trvChild = New TreeNode
            With trvChild
                .Text = "API Access"
                .ImageIndex = 22
                .SelectedImageIndex = 22
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes.Add(trvChild)
            trvChild = Nothing
            trvChild = New TreeNode
            With trvChild
                .Text = "Roles"
                .ImageIndex = 14
                .SelectedImageIndex = 14
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)
            trvChild = New TreeNode
            With trvChild
                .Text = "Users"
                .ImageIndex = 14
                .SelectedImageIndex = 14
                .ForeColor = Color.FromArgb(31, 73, 125)
            End With
            .Nodes(0).Nodes.Add(trvChild)

            .ExpandAll()
            .EndUpdate()
        End With

    End Sub

    Private Sub Fill_DetailsAPI()
        btnViewHistory.Visible = False
        If Not (trvCategory.SelectedNode Is trvCategory.Nodes(0)) Then
            Dim objAPIRole As New gloPatientPortal.clsAPIRole(gloEMRAdmin.mdlGeneral.GetConnectionString)
            Dim objAPIUsers As New gloPatientPortal.clsAPIContact(gloEMRAdmin.mdlGeneral.GetConnectionString)
            Dim dtUsers As New DataTable
            Select Case Trim(trvCategory.SelectedNode.Text)
                Case "Roles"
                    dtUsers = objAPIRole.GetAPIRoles(0)
                    btnDelete.Visible = False
                    dgData.CaptionText = "API Roles"

                    dgData.DataSource = dtUsers

                    Dim grdTableStyle As New clsDataGridTableStyle(dtUsers.TableName)

                    Dim grdColStyleRoleID As New DataGridTextBoxColumn
                    With grdColStyleRoleID
                        .HeaderText = "Role ID"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtUsers.Columns(0).ColumnName
                        .NullText = ""
                        .Width = 0
                    End With

                    Dim grdColStyleRoleName As New DataGridTextBoxColumn
                    With grdColStyleRoleName
                        .HeaderText = "Role  Name"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtUsers.Columns(1).ColumnName
                        .NullText = ""
                        .Width = 0.2 * dgData.Width
                    End With

                    Dim grdColStylebIsSystemDefined As New DataGridTextBoxColumn
                    With grdColStylebIsSystemDefined
                        .HeaderText = "System Defined"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtUsers.Columns(2).ColumnName
                        .NullText = ""
                        .Width = 0.2 * dgData.Width
                    End With



                    grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleRoleID, grdColStyleRoleName, grdColStylebIsSystemDefined})

                    dgData.TableStyles.Clear()
                    dgData.TableStyles.Add(grdTableStyle)

                    'Sarika 21st April 2007
                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "API Access: " + trvCategory.SelectedNode.Text + " viewed", gstrLoginName, gstrClientMachineName)
                    objAudit = Nothing

                Case "Users"
                    dtUsers = objAPIUsers.GetAPIContacts(0)
                    btnDelete.Visible = False
                    'btnDelete.Text = "Block"
                    'btnDelete.ToolTipText = "Block"
                    dgData.CaptionText = "API Users"
                    dgData.DataSource = dtUsers

                    Dim grdTableStyle As New clsDataGridTableStyle(dtUsers.TableName)

                    Dim grdColStyleRoleID As New DataGridTextBoxColumn
                    With grdColStyleRoleID
                        .HeaderText = "User ID"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtUsers.Columns(0).ColumnName
                        .NullText = ""
                        .Width = 0
                    End With
                    Dim grdColStyleRoleName As New DataGridTextBoxColumn
                    With grdColStyleRoleName
                        .HeaderText = "Role Name"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtUsers.Columns(1).ColumnName
                        .NullText = ""
                        .Width = 0.2 * dgData.Width
                    End With

                    Dim grdColStyleFirstName As New DataGridTextBoxColumn
                    With grdColStyleFirstName
                        .HeaderText = "First Name"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtUsers.Columns(2).ColumnName
                        .NullText = ""
                        .Width = 0.2 * dgData.Width
                    End With
                    Dim grdColStyleMiddleName As New DataGridTextBoxColumn
                    With grdColStyleMiddleName
                        .HeaderText = "Middle Name"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtUsers.Columns(3).ColumnName
                        .NullText = ""
                        .Width = 0.2 * dgData.Width
                    End With
                    Dim grdColStyleLastName As New DataGridTextBoxColumn
                    With grdColStyleLastName
                        .HeaderText = "Last Name"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtUsers.Columns(4).ColumnName
                        .NullText = ""
                        .Width = 0.2 * dgData.Width
                    End With

                    Dim grdColStyleGender As New DataGridTextBoxColumn
                    With grdColStyleGender
                        .HeaderText = "Gender"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtUsers.Columns(5).ColumnName
                        .NullText = ""
                        .Width = 0.2 * dgData.Width
                    End With
                    Dim grdColStyleEmail As New DataGridTextBoxColumn
                    With grdColStyleEmail
                        .HeaderText = "Email ID"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtUsers.Columns(6).ColumnName
                        .NullText = ""
                        .Width = 0.2 * dgData.Width
                    End With


                    grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleRoleID, grdColStyleRoleName, grdColStyleFirstName, grdColStyleMiddleName, grdColStyleLastName, grdColStyleGender, grdColStyleEmail})

                    dgData.TableStyles.Clear()
                    dgData.TableStyles.Add(grdTableStyle)

                    'Sarika 21st April 2007
                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.RecordViewed, "API Access: " + trvCategory.SelectedNode.Text + " viewed", gstrLoginName, gstrClientMachineName)
                    objAudit = Nothing
            End Select
            objAPIRole = Nothing
            objAPIUsers = Nothing

            '------------------------------------

        End If

    End Sub
#End Region


End Class
