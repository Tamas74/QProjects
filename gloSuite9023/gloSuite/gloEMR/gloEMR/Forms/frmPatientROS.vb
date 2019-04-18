'Imports Microsoft.Office.core
'Imports wd = Microsoft.Office.Interop.Word
Imports System
Imports System.IO
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports gloUserControlLibrary
'Imports System.Data.SqlClient

Public Class frmPatientROS
    Inherits System.Windows.Forms.Form
    Implements gloVoice
    Implements IPatientContext

    Dim objclsPatientROS As New clsPatientROS
    Private ROSVoiceCol As DNSTools.DgnStrings
    Dim BtnText As String
    Dim strNodeName As String
    Public Shared Note As String
    Private voicecol As DNSTools.DgnStrings
    Private m_VisitID As Long
    Private m_VisitDate As Date
    Private _blnRecordLock As Boolean
    Private key As Int64
    Dim blnDoc As Boolean
    Public Shared blnModify As Boolean
    Public Shared blnChangesMade As Boolean  ' Sets if Changes Made In ROS
    Private m_PatientID As Long
    'Dim blnNoteAdded As Boolean
    Public tblstrip As gloGlobal.gloToolStripIgnoreFocus = Nothing  ''added for bugid 71121
    Friend WithEvents objBtn As System.Windows.Forms.Button
    Dim objlbl As Label
    Dim CategoryColor As ArrayList
    Dim trvSearchNode As TreeNode
    Private m_processTable As DataTable
    Private m_FromProcess As Long
    Public blnOpenFromExam As Boolean = False
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    Enum enmPreviousROS
        Current
        Yesterday
        LastWeek
        LastMonth
        Older
    End Enum
    Private Col_CategoryID As Integer = 0
    Private Col_CategoryName As Integer = 1
    Private Col_ItemName As Integer = 2
    Private Col_Comments As Integer = 3
    Private Col_Source As Integer = 4
    Private Col_PatientFormID As Integer = 5
    Private Col_DateEntered As Integer = 6
    Private Col_Count = 7
    Public myCaller As frmPatientExam
    'variable useed to call function call from root caller ie. frmmessages or any form which has liquid data field.
    Public myCaller1 As Object
    'sarika 27th sept 07
    Dim dtSource As New DataTable
    '------------------
    Dim dt As DataTable
   
    Private arrDataDictionary As New ArrayList ''Contains DataDictionary Items to be DELETED.

#Region " Windows Controls "
    Friend WithEvents C1PatientRos As C1.Win.C1FlexGrid.C1FlexGrid
    'Boolean variable to check that, form is open from Main form or from Patient Exam
    'This variable is used for voice purpose
    Friend WithEvents mnuAddItem As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEditItem As System.Windows.Forms.MenuItem
    Friend WithEvents pnlCatSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents GloUC_trvSource As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents trvSource As System.Windows.Forms.TreeView
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents ts_ROS As gloToolStrip.gloToolStrip
    Friend WithEvents tsbtn_Show As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_SaveAndClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label23 As System.Windows.Forms.Label
#End Region

#Region " Windows Form Designer generated code "

    'Public Sub New()
    '    MyBase.New()
    '    Note = ""
    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub


    Public Sub New(ByVal VisitID As Long, ByVal VisitDate As Date, ByVal PatientID As Long, Optional ByVal blnRecordLock As Boolean = False)
        MyBase.New()
        m_VisitID = VisitID
        m_VisitDate = VisitDate
        ''Sanjog - Added on 2011 June 07 to apply record lock functionality Issue no.13844
        _blnRecordLock = blnRecordLock
        ''Sanjog - Added on 2011 June 07 to apply record lock functionality Issue no.13844
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_PatientID = PatientID
    End Sub
#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    '' Private Shared _mu As New Mutex
    Private Shared frm As frmPatientROS

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                ' Dispose managed resources.
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Dim dtpContextMenu As ContextMenu() = {ContextMenuC1PatientROS, ContextMenutrvPrevROS}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenu)
                    gloGlobal.cEventHelper.DisposeContextMenu(dtpContextMenu)
                Catch ex As Exception

                End Try
                
                'frm = Nothing
            End If
            ' Release unmanaged resources. If disposing is false,
            ' only the following code is executed.

            ' Note that this is not thread safe.
            ' Another thread could start disposing the object
            ' after the managed resources are disposed,
            ' but before the disposed flag is set to true.
            ' If thread safety is necessary, it must be
            ' implemented by the client.
        End If
        frm = Nothing
        Me.blnDisposed = True

    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue
        ' to prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Shared Function GetInstance(ByVal VisitID As Long, ByVal VisitDate As Date, ByVal PatientID As Long, Optional ByVal blnRecordLock As Boolean = False) As frmPatientROS
        '_mu.WaitOne()
        Try
            If frm Is Nothing Then
                frm = New frmPatientROS(VisitID, VisitDate, PatientID, blnRecordLock)
            End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function

#End Region
    'constructor is commented as 
    'Public Sub New(ByVal ProcessTable As DataTable, ByVal FromProcess As Integer, Optional ByVal blnRecordLock As Boolean = False)
    '    MyBase.New()
    '    m_processTable = ProcessTable
    '    m_FromProcess = FromProcess ' 1= Call from OMR-Process
    '    _blnRecordLock = blnRecordLock
    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    Private Sub InitializeToolStrip()
        ts_ROS.ConnectionString = GetConnectionString()
        ts_ROS.ModuleName = Me.Name
        ts_ROS.UserID = gnLoginID
    End Sub
    ''Form overrides dispose to clean up the component list.
    'Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    '    If disposing Then
    '        If Not (components Is Nothing) Then
    '            components.Dispose()
    '        End If
    '    End If
    '    MyBase.Dispose(disposing)
    'End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlOuter As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents ContextMenuC1PatientROS As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuRemove As System.Windows.Forms.MenuItem
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlCatBtn As System.Windows.Forms.Panel
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents PnlRight As System.Windows.Forms.Panel
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlPrevSearch As System.Windows.Forms.Panel
    Friend WithEvents pnltrvSource As System.Windows.Forms.Panel
    Friend WithEvents pnlPatientHeader As System.Windows.Forms.Panel
    Friend WithEvents lblVisitDate As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblVisitID As System.Windows.Forms.Label
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblPatientCode As System.Windows.Forms.Label
    Friend WithEvents lblPatient As System.Windows.Forms.Label
    Friend WithEvents pnltrvTarget As System.Windows.Forms.Panel
    Friend WithEvents txtSearchCategory As System.Windows.Forms.TextBox
    Friend WithEvents mnuMakeCurrent As System.Windows.Forms.MenuItem
    Friend WithEvents trvPrevROS As System.Windows.Forms.TreeView
    Friend WithEvents pnlPrevROS As System.Windows.Forms.Panel
    Friend WithEvents btnPrevROS As System.Windows.Forms.Button
    Friend WithEvents cmbsearchROS As System.Windows.Forms.ComboBox
    Friend WithEvents mnuDeleteROS As System.Windows.Forms.MenuItem
    Friend WithEvents txtsearchROS As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenutrvPrevROS As System.Windows.Forms.ContextMenu
    Friend WithEvents ImgPatientROS1 As System.Windows.Forms.ImageList


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientROS))
        Me.pnlOuter = New System.Windows.Forms.Panel
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.PnlRight = New System.Windows.Forms.Panel
        Me.pnltrvTarget = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.C1PatientRos = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlPatientHeader = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblVisitDate = New System.Windows.Forms.Label
        Me.btnPrevROS = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblVisitID = New System.Windows.Forms.Label
        Me.lblPatientName = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblPatientCode = New System.Windows.Forms.Label
        Me.lblPatient = New System.Windows.Forms.Label
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.pnlPrevROS = New System.Windows.Forms.Panel
        Me.pnl_Base = New System.Windows.Forms.Panel
        Me.trvPrevROS = New System.Windows.Forms.TreeView
        Me.ImgPatientROS1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.pnlPrevSearch = New System.Windows.Forms.Panel
        Me.cmbsearchROS = New System.Windows.Forms.ComboBox
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.txtsearchROS = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.pnlCatBtn = New System.Windows.Forms.Panel
        Me.pnltrvSource = New System.Windows.Forms.Panel
        Me.GloUC_trvSource = New gloUserControlLibrary.gloUC_TreeView
        Me.trvSource = New System.Windows.Forms.TreeView
        Me.Label21 = New System.Windows.Forms.Label
        Me.pnlCatSearch = New System.Windows.Forms.Panel
        Me.txtSearchCategory = New System.Windows.Forms.TextBox
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label
        Me.PicBx_Search = New System.Windows.Forms.PictureBox
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ROS = New gloToolStrip.gloToolStrip
        Me.tsbtn_Show = New System.Windows.Forms.ToolStripButton
        Me.tsbtn_SaveAndClose = New System.Windows.Forms.ToolStripButton
        Me.tsbtn_Close = New System.Windows.Forms.ToolStripButton
        Me.ContextMenuC1PatientROS = New System.Windows.Forms.ContextMenu
        Me.mnuRemove = New System.Windows.Forms.MenuItem
        Me.mnuAddItem = New System.Windows.Forms.MenuItem
        Me.mnuEditItem = New System.Windows.Forms.MenuItem
        Me.ContextMenutrvPrevROS = New System.Windows.Forms.ContextMenu
        Me.mnuDeleteROS = New System.Windows.Forms.MenuItem
        Me.mnuMakeCurrent = New System.Windows.Forms.MenuItem
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlOuter.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.PnlRight.SuspendLayout()
        Me.pnltrvTarget.SuspendLayout()
        CType(Me.C1PatientRos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPatientHeader.SuspendLayout()
        Me.pnlPrevROS.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.pnlPrevSearch.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCatBtn.SuspendLayout()
        Me.pnltrvSource.SuspendLayout()
        Me.pnlCatSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ROS.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlOuter
        '
        Me.pnlOuter.Controls.Add(Me.pnlMain)
        Me.pnlOuter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlOuter.Location = New System.Drawing.Point(0, 56)
        Me.pnlOuter.Name = "pnlOuter"
        Me.pnlOuter.Size = New System.Drawing.Size(792, 509)
        Me.pnlOuter.TabIndex = 1
        '
        'pnlMain
        '
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.PnlRight)
        Me.pnlMain.Controls.Add(Me.Splitter2)
        Me.pnlMain.Controls.Add(Me.pnlPrevROS)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlCatBtn)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(792, 509)
        Me.pnlMain.TabIndex = 0
        '
        'PnlRight
        '
        Me.PnlRight.Controls.Add(Me.pnltrvTarget)
        Me.PnlRight.Controls.Add(Me.pnlPatientHeader)
        Me.PnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlRight.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlRight.Location = New System.Drawing.Point(234, 0)
        Me.PnlRight.Name = "PnlRight"
        Me.PnlRight.Size = New System.Drawing.Size(324, 509)
        Me.PnlRight.TabIndex = 1
        '
        'pnltrvTarget
        '
        Me.pnltrvTarget.Controls.Add(Me.Label7)
        Me.pnltrvTarget.Controls.Add(Me.Label8)
        Me.pnltrvTarget.Controls.Add(Me.Label9)
        Me.pnltrvTarget.Controls.Add(Me.Label10)
        Me.pnltrvTarget.Controls.Add(Me.C1PatientRos)
        Me.pnltrvTarget.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvTarget.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvTarget.Location = New System.Drawing.Point(0, 0)
        Me.pnltrvTarget.Name = "pnltrvTarget"
        Me.pnltrvTarget.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnltrvTarget.Size = New System.Drawing.Size(324, 509)
        Me.pnltrvTarget.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1, 505)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(322, 1)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 502)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(323, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 502)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(324, 1)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "label1"
        '
        'C1PatientRos
        '
        Me.C1PatientRos.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1PatientRos.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1PatientRos.BackColor = System.Drawing.Color.GhostWhite
        Me.C1PatientRos.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1PatientRos.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
            ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1PatientRos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1PatientRos.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PatientRos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1PatientRos.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1PatientRos.Location = New System.Drawing.Point(0, 3)
        Me.C1PatientRos.Name = "C1PatientRos"
        Me.C1PatientRos.Rows.Count = 1
        Me.C1PatientRos.Rows.DefaultSize = 19
        Me.C1PatientRos.Rows.Fixed = 0
        Me.C1PatientRos.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PatientRos.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1PatientRos.ShowSort = False
        Me.C1PatientRos.Size = New System.Drawing.Size(324, 503)
        Me.C1PatientRos.StyleInfo = resources.GetString("C1PatientRos.StyleInfo")
        Me.C1PatientRos.TabIndex = 0
        Me.C1PatientRos.Tree.NodeImageCollapsed = CType(resources.GetObject("C1PatientRos.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1PatientRos.Tree.NodeImageExpanded = CType(resources.GetObject("C1PatientRos.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'pnlPatientHeader
        '
        Me.pnlPatientHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatientHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPatientHeader.Controls.Add(Me.Label2)
        Me.pnlPatientHeader.Controls.Add(Me.Label3)
        Me.pnlPatientHeader.Controls.Add(Me.Label5)
        Me.pnlPatientHeader.Controls.Add(Me.Label6)
        Me.pnlPatientHeader.Controls.Add(Me.lblVisitDate)
        Me.pnlPatientHeader.Controls.Add(Me.btnPrevROS)
        Me.pnlPatientHeader.Controls.Add(Me.Label4)
        Me.pnlPatientHeader.Controls.Add(Me.lblVisitID)
        Me.pnlPatientHeader.Controls.Add(Me.lblPatientName)
        Me.pnlPatientHeader.Controls.Add(Me.Label1)
        Me.pnlPatientHeader.Controls.Add(Me.lblPatientCode)
        Me.pnlPatientHeader.Controls.Add(Me.lblPatient)
        Me.pnlPatientHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPatientHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientHeader.Name = "pnlPatientHeader"
        Me.pnlPatientHeader.Padding = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.pnlPatientHeader.Size = New System.Drawing.Size(324, 48)
        Me.pnlPatientHeader.TabIndex = 0
        Me.pnlPatientHeader.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(2, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(320, 1)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 41)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(322, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 41)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(322, 1)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "label1"
        '
        'lblVisitDate
        '
        Me.lblVisitDate.AutoSize = True
        Me.lblVisitDate.BackColor = System.Drawing.Color.Transparent
        Me.lblVisitDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVisitDate.Location = New System.Drawing.Point(334, 9)
        Me.lblVisitDate.Name = "lblVisitDate"
        Me.lblVisitDate.Size = New System.Drawing.Size(73, 14)
        Me.lblVisitDate.TabIndex = 13
        Me.lblVisitDate.Text = "08/29/2005"
        Me.lblVisitDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnPrevROS
        '
        Me.btnPrevROS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrevROS.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnPrevROS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPrevROS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnPrevROS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnPrevROS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevROS.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrevROS.Location = New System.Drawing.Point(217, 25)
        Me.btnPrevROS.Name = "btnPrevROS"
        Me.btnPrevROS.Size = New System.Drawing.Size(96, 16)
        Me.btnPrevROS.TabIndex = 16
        Me.btnPrevROS.Text = "Prev History"
        Me.btnPrevROS.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(262, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 14)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Visit Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVisitID
        '
        Me.lblVisitID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVisitID.AutoSize = True
        Me.lblVisitID.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblVisitID.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVisitID.Location = New System.Drawing.Point(214, 11)
        Me.lblVisitID.Name = "lblVisitID"
        Me.lblVisitID.Size = New System.Drawing.Size(14, 15)
        Me.lblVisitID.TabIndex = 12
        Me.lblVisitID.Text = "1"
        Me.lblVisitID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblVisitID.Visible = False
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.Location = New System.Drawing.Point(114, 29)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(71, 14)
        Me.lblPatientName.TabIndex = 11
        Me.lblPatientName.Text = "Mike Dodge"
        Me.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(86, 14)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Patient Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPatientCode
        '
        Me.lblPatientCode.AutoSize = True
        Me.lblPatientCode.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientCode.Location = New System.Drawing.Point(114, 9)
        Me.lblPatientCode.Name = "lblPatientCode"
        Me.lblPatientCode.Size = New System.Drawing.Size(35, 14)
        Me.lblPatientCode.TabIndex = 9
        Me.lblPatientCode.Text = "1001"
        Me.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPatient
        '
        Me.lblPatient.AutoSize = True
        Me.lblPatient.BackColor = System.Drawing.Color.Transparent
        Me.lblPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatient.Location = New System.Drawing.Point(7, 29)
        Me.lblPatient.Name = "lblPatient"
        Me.lblPatient.Size = New System.Drawing.Size(89, 14)
        Me.lblPatient.TabIndex = 8
        Me.lblPatient.Text = "Patient Name :"
        Me.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Splitter2.Location = New System.Drawing.Point(558, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(4, 509)
        Me.Splitter2.TabIndex = 13
        Me.Splitter2.TabStop = False
        '
        'pnlPrevROS
        '
        Me.pnlPrevROS.Controls.Add(Me.pnl_Base)
        Me.pnlPrevROS.Controls.Add(Me.pnlPrevSearch)
        Me.pnlPrevROS.Controls.Add(Me.pnlSearch)
        Me.pnlPrevROS.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlPrevROS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPrevROS.Location = New System.Drawing.Point(562, 0)
        Me.pnlPrevROS.Name = "pnlPrevROS"
        Me.pnlPrevROS.Size = New System.Drawing.Size(230, 509)
        Me.pnlPrevROS.TabIndex = 2
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.trvPrevROS)
        Me.pnl_Base.Controls.Add(Me.Label23)
        Me.pnl_Base.Controls.Add(Me.Label22)
        Me.pnl_Base.Controls.Add(Me.Label17)
        Me.pnl_Base.Controls.Add(Me.Label18)
        Me.pnl_Base.Controls.Add(Me.Label19)
        Me.pnl_Base.Controls.Add(Me.Label20)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 54)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_Base.Size = New System.Drawing.Size(230, 455)
        Me.pnl_Base.TabIndex = 3
        '
        'trvPrevROS
        '
        Me.trvPrevROS.BackColor = System.Drawing.Color.White
        Me.trvPrevROS.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvPrevROS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvPrevROS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvPrevROS.ForeColor = System.Drawing.Color.Black
        Me.trvPrevROS.HideSelection = False
        Me.trvPrevROS.ImageIndex = 0
        Me.trvPrevROS.ImageList = Me.ImgPatientROS1
        Me.trvPrevROS.Indent = 20
        Me.trvPrevROS.ItemHeight = 20
        Me.trvPrevROS.Location = New System.Drawing.Point(7, 9)
        Me.trvPrevROS.Name = "trvPrevROS"
        Me.trvPrevROS.SelectedImageIndex = 15
        Me.trvPrevROS.ShowLines = False
        Me.trvPrevROS.ShowNodeToolTips = True
        Me.trvPrevROS.ShowRootLines = False
        Me.trvPrevROS.Size = New System.Drawing.Size(219, 442)
        Me.trvPrevROS.TabIndex = 0
        '
        'ImgPatientROS1
        '
        Me.ImgPatientROS1.ImageStream = CType(resources.GetObject("ImgPatientROS1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgPatientROS1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgPatientROS1.Images.SetKeyName(0, "ROS.ico")
        Me.ImgPatientROS1.Images.SetKeyName(1, "Bullet06.ico")
        Me.ImgPatientROS1.Images.SetKeyName(2, "")
        Me.ImgPatientROS1.Images.SetKeyName(3, "")
        Me.ImgPatientROS1.Images.SetKeyName(4, "Olders.ico")
        Me.ImgPatientROS1.Images.SetKeyName(5, "Olders.ico")
        Me.ImgPatientROS1.Images.SetKeyName(6, "Olders_Disable.ico")
        Me.ImgPatientROS1.Images.SetKeyName(7, "Yesterdays.ico")
        Me.ImgPatientROS1.Images.SetKeyName(8, "Yesterdays_Disable.ico")
        Me.ImgPatientROS1.Images.SetKeyName(9, "Last Week.ico")
        Me.ImgPatientROS1.Images.SetKeyName(10, "Last Week_Disable.ico")
        Me.ImgPatientROS1.Images.SetKeyName(11, "LastMonth.ico")
        Me.ImgPatientROS1.Images.SetKeyName(12, "LastMonth_Disable.ico")
        Me.ImgPatientROS1.Images.SetKeyName(13, "Current.ico")
        Me.ImgPatientROS1.Images.SetKeyName(14, "Current_Disable.ico")
        Me.ImgPatientROS1.Images.SetKeyName(15, "Small Arrow.ico")
        Me.ImgPatientROS1.Images.SetKeyName(16, "Remove ROS.ico")
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.White
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Location = New System.Drawing.Point(1, 9)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(6, 442)
        Me.Label23.TabIndex = 39
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.White
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Location = New System.Drawing.Point(1, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(225, 8)
        Me.Label22.TabIndex = 38
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(1, 451)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(225, 1)
        Me.Label17.TabIndex = 4
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 451)
        Me.Label18.TabIndex = 3
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(226, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 451)
        Me.Label19.TabIndex = 2
        Me.Label19.Text = "label3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(227, 1)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "label1"
        '
        'pnlPrevSearch
        '
        Me.pnlPrevSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlPrevSearch.Controls.Add(Me.cmbsearchROS)
        Me.pnlPrevSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPrevSearch.Location = New System.Drawing.Point(0, 29)
        Me.pnlPrevSearch.Name = "pnlPrevSearch"
        Me.pnlPrevSearch.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.pnlPrevSearch.Size = New System.Drawing.Size(230, 25)
        Me.pnlPrevSearch.TabIndex = 1
        '
        'cmbsearchROS
        '
        Me.cmbsearchROS.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbsearchROS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsearchROS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsearchROS.ForeColor = System.Drawing.Color.Black
        Me.cmbsearchROS.Location = New System.Drawing.Point(0, 0)
        Me.cmbsearchROS.Name = "cmbsearchROS"
        Me.cmbsearchROS.Size = New System.Drawing.Size(227, 22)
        Me.cmbsearchROS.TabIndex = 0
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtsearchROS)
        Me.pnlSearch.Controls.Add(Me.Label11)
        Me.pnlSearch.Controls.Add(Me.Label12)
        Me.pnlSearch.Controls.Add(Me.PictureBox1)
        Me.pnlSearch.Controls.Add(Me.Label13)
        Me.pnlSearch.Controls.Add(Me.Label14)
        Me.pnlSearch.Controls.Add(Me.Label15)
        Me.pnlSearch.Controls.Add(Me.Label16)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(230, 29)
        Me.pnlSearch.TabIndex = 0
        '
        'txtsearchROS
        '
        Me.txtsearchROS.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchROS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchROS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearchROS.ForeColor = System.Drawing.Color.Black
        Me.txtsearchROS.Location = New System.Drawing.Point(29, 8)
        Me.txtsearchROS.Name = "txtsearchROS"
        Me.txtsearchROS.Size = New System.Drawing.Size(197, 15)
        Me.txtsearchROS.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Location = New System.Drawing.Point(29, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(197, 4)
        Me.Label11.TabIndex = 37
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Location = New System.Drawing.Point(29, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(197, 2)
        Me.Label12.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(1, 25)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(225, 1)
        Me.Label13.TabIndex = 35
        Me.Label13.Text = "label1"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Location = New System.Drawing.Point(1, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(225, 1)
        Me.Label14.TabIndex = 36
        Me.Label14.Text = "label1"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Location = New System.Drawing.Point(0, 3)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 23)
        Me.Label15.TabIndex = 39
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Location = New System.Drawing.Point(226, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 23)
        Me.Label16.TabIndex = 40
        Me.Label16.Text = "label4"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(230, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 509)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'pnlCatBtn
        '
        Me.pnlCatBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlCatBtn.Controls.Add(Me.pnltrvSource)
        Me.pnlCatBtn.Controls.Add(Me.pnlCatSearch)
        Me.pnlCatBtn.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlCatBtn.Location = New System.Drawing.Point(0, 0)
        Me.pnlCatBtn.Name = "pnlCatBtn"
        Me.pnlCatBtn.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlCatBtn.Size = New System.Drawing.Size(230, 509)
        Me.pnlCatBtn.TabIndex = 0
        '
        'pnltrvSource
        '
        Me.pnltrvSource.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrvSource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrvSource.Controls.Add(Me.GloUC_trvSource)
        Me.pnltrvSource.Controls.Add(Me.trvSource)
        Me.pnltrvSource.Controls.Add(Me.Label21)
        Me.pnltrvSource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvSource.Location = New System.Drawing.Point(0, 0)
        Me.pnltrvSource.Name = "pnltrvSource"
        Me.pnltrvSource.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnltrvSource.Size = New System.Drawing.Size(230, 506)
        Me.pnltrvSource.TabIndex = 1
        '
        'GloUC_trvSource
        '
        Me.GloUC_trvSource.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvSource.CheckBoxes = False
        Me.GloUC_trvSource.CodeMember = Nothing
        Me.GloUC_trvSource.DDIDMember = Nothing
        Me.GloUC_trvSource.DescriptionMember = Nothing
        Me.GloUC_trvSource.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvSource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvSource.DrugFlag = CType(16, Short)
        Me.GloUC_trvSource.DrugFormMember = Nothing
        Me.GloUC_trvSource.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvSource.DurationMember = Nothing
        Me.GloUC_trvSource.FrequencyMember = Nothing
        Me.GloUC_trvSource.ImageIndex = 1
        Me.GloUC_trvSource.ImageList = Me.ImgPatientROS1
        Me.GloUC_trvSource.ImageObject = Nothing
        Me.GloUC_trvSource.IsDrug = False
        Me.GloUC_trvSource.IsNarcoticsMember = Nothing
        Me.GloUC_trvSource.Location = New System.Drawing.Point(3, 3)
        Me.GloUC_trvSource.MaximumNodes = 1000
        Me.GloUC_trvSource.Name = "GloUC_trvSource"
        Me.GloUC_trvSource.NDCCodeMember = Nothing
        Me.GloUC_trvSource.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.GloUC_trvSource.ParentImageIndex = 0
        Me.GloUC_trvSource.ParentMember = Nothing
        Me.GloUC_trvSource.RouteMember = Nothing
        Me.GloUC_trvSource.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvSource.SearchBox = True
        Me.GloUC_trvSource.SearchText = Nothing
        Me.GloUC_trvSource.SelectedImageIndex = 1
        Me.GloUC_trvSource.SelectedNode = Nothing
        Me.GloUC_trvSource.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvSource.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvSource.SelectedParentImageIndex = 0
        Me.GloUC_trvSource.Size = New System.Drawing.Size(227, 503)
        Me.GloUC_trvSource.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvSource.TabIndex = 50
        Me.GloUC_trvSource.Tag = Nothing
        Me.GloUC_trvSource.UnitMember = Nothing
        Me.GloUC_trvSource.ValueMember = Nothing
        '
        'trvSource
        '
        Me.trvSource.BackColor = System.Drawing.Color.White
        Me.trvSource.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSource.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvSource.ForeColor = System.Drawing.Color.Black
        Me.trvSource.HideSelection = False
        Me.trvSource.ImageIndex = 1
        Me.trvSource.ImageList = Me.ImgPatientROS1
        Me.trvSource.Indent = 20
        Me.trvSource.ItemHeight = 20
        Me.trvSource.Location = New System.Drawing.Point(4, 9)
        Me.trvSource.Name = "trvSource"
        Me.trvSource.SelectedImageIndex = 1
        Me.trvSource.ShowLines = False
        Me.trvSource.ShowNodeToolTips = True
        Me.trvSource.Size = New System.Drawing.Size(225, 467)
        Me.trvSource.TabIndex = 0
        Me.trvSource.Visible = False
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Location = New System.Drawing.Point(4, 60)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(225, 8)
        Me.Label21.TabIndex = 38
        Me.Label21.Visible = False
        '
        'pnlCatSearch
        '
        Me.pnlCatSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlCatSearch.Controls.Add(Me.txtSearchCategory)
        Me.pnlCatSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlCatSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlCatSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlCatSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlCatSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlCatSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlCatSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlCatSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlCatSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlCatSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlCatSearch.Name = "pnlCatSearch"
        Me.pnlCatSearch.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnlCatSearch.Size = New System.Drawing.Size(230, 29)
        Me.pnlCatSearch.TabIndex = 0
        Me.pnlCatSearch.Visible = False
        '
        'txtSearchCategory
        '
        Me.txtSearchCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchCategory.ForeColor = System.Drawing.Color.Black
        Me.txtSearchCategory.Location = New System.Drawing.Point(32, 8)
        Me.txtSearchCategory.Name = "txtSearchCategory"
        Me.txtSearchCategory.Size = New System.Drawing.Size(197, 15)
        Me.txtSearchCategory.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(32, 4)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(197, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(32, 23)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(197, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(4, 4)
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
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(4, 25)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(225, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 3)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(225, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(229, 3)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.ts_ROS)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(792, 56)
        Me.pnlToolStrip.TabIndex = 0
        '
        'ts_ROS
        '
        Me.ts_ROS.AddSeparatorsBetweenEachButton = False
        Me.ts_ROS.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_ROS.BackgroundImage = CType(resources.GetObject("ts_ROS.BackgroundImage"), System.Drawing.Image)
        Me.ts_ROS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ROS.ButtonsToHide = CType(resources.GetObject("ts_ROS.ButtonsToHide"), System.Collections.ArrayList)
        Me.ts_ROS.ConnectionString = Nothing
        Me.ts_ROS.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.ts_ROS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ROS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ROS.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ROS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtn_Show, Me.tsbtn_SaveAndClose, Me.tsbtn_Close})
        Me.ts_ROS.Location = New System.Drawing.Point(0, 0)
        Me.ts_ROS.ModuleName = Nothing
        Me.ts_ROS.Name = "ts_ROS"
        Me.ts_ROS.Size = New System.Drawing.Size(792, 53)
        Me.ts_ROS.TabIndex = 2
        Me.ts_ROS.Text = "GloToolStrip1"
        Me.ts_ROS.UserID = CType(0, Long)
        '
        'tsbtn_Show
        '
        Me.tsbtn_Show.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_Show.Image = CType(resources.GetObject("tsbtn_Show.Image"), System.Drawing.Image)
        Me.tsbtn_Show.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Show.Name = "tsbtn_Show"
        Me.tsbtn_Show.Size = New System.Drawing.Size(46, 50)
        Me.tsbtn_Show.Tag = "Show"
        Me.tsbtn_Show.Text = "Sh&ow"
        Me.tsbtn_Show.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbtn_SaveAndClose
        '
        Me.tsbtn_SaveAndClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_SaveAndClose.Image = CType(resources.GetObject("tsbtn_SaveAndClose.Image"), System.Drawing.Image)
        Me.tsbtn_SaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_SaveAndClose.Name = "tsbtn_SaveAndClose"
        Me.tsbtn_SaveAndClose.Size = New System.Drawing.Size(66, 50)
        Me.tsbtn_SaveAndClose.Tag = "Save and Close"
        Me.tsbtn_SaveAndClose.Text = "&Save&&Cls"
        Me.tsbtn_SaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_SaveAndClose.ToolTipText = "Save and Close"
        '
        'tsbtn_Close
        '
        Me.tsbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_Close.Image = CType(resources.GetObject("tsbtn_Close.Image"), System.Drawing.Image)
        Me.tsbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Close.Name = "tsbtn_Close"
        Me.tsbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tsbtn_Close.Tag = "Close"
        Me.tsbtn_Close.Text = "&Close"
        Me.tsbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ContextMenuC1PatientROS
        '
        Me.ContextMenuC1PatientROS.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRemove, Me.mnuAddItem, Me.mnuEditItem})
        '
        'mnuRemove
        '
        Me.mnuRemove.Index = 0
        Me.mnuRemove.Text = "Remove History"
        '
        'mnuAddItem
        '
        Me.mnuAddItem.Index = 1
        Me.mnuAddItem.Text = "Add ROS Item"
        '
        'mnuEditItem
        '
        Me.mnuEditItem.Index = 2
        Me.mnuEditItem.Text = "Edit ROS Item"
        '
        'ContextMenutrvPrevROS
        '
        Me.ContextMenutrvPrevROS.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteROS, Me.mnuMakeCurrent})
        '
        'mnuDeleteROS
        '
        Me.mnuDeleteROS.Index = 0
        Me.mnuDeleteROS.Text = "&Delete ROS"
        '
        'mnuMakeCurrent
        '
        Me.mnuMakeCurrent.Index = 1
        Me.mnuMakeCurrent.Text = "Make as Current ROS"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmPatientROS
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(792, 565)
        Me.Controls.Add(Me.pnlOuter)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientROS"
        Me.ShowInTaskbar = False
        Me.Text = "Patient ROS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlOuter.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.PnlRight.ResumeLayout(False)
        Me.pnltrvTarget.ResumeLayout(False)
        CType(Me.C1PatientRos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPatientHeader.ResumeLayout(False)
        Me.pnlPatientHeader.PerformLayout()
        Me.pnlPrevROS.ResumeLayout(False)
        Me.pnl_Base.ResumeLayout(False)
        Me.pnlPrevSearch.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCatBtn.ResumeLayout(False)
        Me.pnltrvSource.ResumeLayout(False)
        Me.pnlCatSearch.ResumeLayout(False)
        Me.pnlCatSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ROS.ResumeLayout(False)
        Me.ts_ROS.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUC_PatientStrip

    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        gloUC_PatientStrip1 = New gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            '' Pass Paarameters Type of Form
            .ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.ROS)
            .SendToBack()
            .DTPValue = Format(m_VisitDate, "MM/dd/yyyy")
            .DTPEnabled = False
        End With
        pnlOuter.Controls.Add(gloUC_PatientStrip1)
        ''''
        C1PatientRos.BringToFront()
        '' Hide Previous Patient Details
        pnlPatientHeader.Visible = False
        ' ''
    End Sub

#End Region

    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            dtPatient = New DataTable
            dtPatient = GetPatientInfo(m_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

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

    Private Sub frmPatientROS_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmPatientROS_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If Not IsNothing(tblstrip) Then  ''added for bugid 71121
            tblstrip.Visible = True
        End If
        'code added by dipak 20090922 for solve problem (Dispose object refrenced :Object name "Icon")
        Me.Hide()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub frmPatientROS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        InitializeToolStrip()
        gloC1FlexStyle.Style(C1PatientRos)
       
        Try

            Dim myScreenWidth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth   '' added for bugid 64956
            Dim myScreenHeight As Integer = System.Windows.SystemParameters.PrimaryScreenHeight - 20 ''added for bottom margin
            If ((Me.Width > myScreenWidth) Or (Me.Height > myScreenHeight)) Then
                Me.MaximumSize = New System.Drawing.Size(myScreenWidth, myScreenHeight)
                Me.AutoScroll = True
            End If


            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                'If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnOpenFromExam = False Then
                Try
                    ROSVoiceCol = New DNSTools.DgnStrings
                    voicecol = New DNSTools.DgnStrings
                    Call AddBasicVoiceCommands()
                Catch ex As Exception

                End Try
            End If
            trvSource.AllowDrop = True
            ' trvTarget.AllowDrop = True
            Call Get_PatientDetails()
            lblPatientCode.Text = strPatientCode
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'lblPatientCode.Tag = gnPatientID
            lblPatientCode.Tag = m_PatientID
            'end modification 
            lblPatientName.Text = strPatientFirstName & " " & strPatientLastName

            lblVisitDate.Text = m_VisitDate

            ' ''
            Call Set_PatientDetailStrip()
            ' ''
            Call Fill_Category()

            Dim mstream As ADODB.Stream
            Dim strFileName As String
            mstream = New ADODB.Stream

            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()
            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "temp5.txt" 'SLR: Changed temp5 to uniqueID
            '' SUDHIR 20100102 '' READONLY FILE WAS GIVING WRITE ERROR ''
            If File.Exists(strFileName) Then
                Dim oFileInfo As New FileInfo(strFileName)
                oFileInfo.IsReadOnly = False
                oFileInfo = Nothing
            End If
            '' END SUDHIR ''
            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
            mstream.Close()

            '''''''''''' end textBox

            pnlPrevROS.Visible = False
            btnPrevROS.Text = "Show Previous ROS"
            cmbsearchROS.Items.Add("ROS Date")
            cmbsearchROS.Items.Add("Category")
            cmbsearchROS.Text = "ROS Date"
            'Try
            '    If (IsNothing(trvSource.ContextMenu) = False) Then
            '        trvSource.ContextMenu.Dispose()
            '        trvSource.ContextMenu = Nothing
            '    End If
            'Catch ex As Exception

            'End Try
            trvSource.ContextMenu = Nothing

            txtSearchCategory.Select()

            'if m_FromProcess = 1  (Open from OMR-ICR Process)
            If m_FromProcess = 1 Then

                Dim arrCategorycolor(12) As String
                'Dim k As Integer
                'btnOK.ForeColor.DarkOrange()

                arrCategorycolor(0) = "Red"
                arrCategorycolor(1) = "DarkOrange"
                arrCategorycolor(2) = "Maroon"
                arrCategorycolor(3) = "Blue"
                arrCategorycolor(4) = "Green"
                arrCategorycolor(5) = "BlueViolet"
                arrCategorycolor(6) = "Red"
                arrCategorycolor(7) = "DarkOrange"
                arrCategorycolor(8) = "Maroon"
                arrCategorycolor(9) = "Blue"
                arrCategorycolor(10) = "Green"
                arrCategorycolor(11) = "BlueViolet"


                dt = New DataTable
                dt = m_processTable
            End If

            'set Style for FlexGrid 
            Call SetGridStyle()
            'Fill data for flexgrid at form load
            Call setGridData()


            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Open, "Patient ROS  viewed", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Open, "Patient ROS  viewed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '' TO Make Enable/Disable the Buttons according to the Record Lock Status
            Set_RecordLock(_blnRecordLock)
        End Try
    End Sub

    Public Sub Fill_Category()
        dt = New DataTable
        'Call Procedure to fill all ROS categories
        dt = FillROSCategory()
        ''If gnVisitID = 0 Then
        ''    objclsPatientROS.GenerateVisitID()
        ''End If

        lblVisitDate.Tag = m_VisitID

        'Dim dt As DataTable
        Dim i As Integer
        'Dim j As Integer

        ' dt = objclsPatientROS.GetAllCategory("ROS")
        If IsNothing(dt) = False Then

            ''For i = 0 To dt.Rows.Count - 1
            For Each ROSCategoryRow As DataRow In dt.Rows
                objBtn = New Button
                'objBtn.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                objBtn.ForeColor = Color.FromArgb(31, 73, 125)
                objBtn.Text = ROSCategoryRow(1).ToString
                objBtn.Tag = ROSCategoryRow(0) '.ToString

                If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                    'If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnOpenFromExam = False Then
                    Try
                        ROSVoiceCol.Add(Trim(ROSCategoryRow(1).ToString))
                    Catch ex As Exception

                    End Try

                End If
                ' Add the one event handler
                '  Me.pnlCatBtn.Controls.Add(objBtn)
                Me.pnltrvSource.Controls.Add(objBtn)
                objBtn.Dock = DockStyle.Bottom
                objBtn.BackColor = Color.FromArgb(207, 224, 248)
                objBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                objBtn.BackgroundImageLayout = ImageLayout.Stretch
                objBtn.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9, FontStyle.Bold)
                objBtn.FlatStyle = FlatStyle.Flat
                Dim screenresolution As Integer = System.Windows.SystemParameters.FullPrimaryScreenHeight
                If screenresolution <= 800 Then  ''condition added for bugid 64956
                    objBtn.Height = 23
                Else
                    objBtn.Height = 28
                End If

                If i = 0 Then
                    objBtn.Dock = DockStyle.Top
                    objBtn.BackColor = Color.FromArgb(207, 224, 248)
                    objBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    objBtn.BackgroundImageLayout = ImageLayout.Stretch
                    'objBtn.Visible = False
                    BtnText = objBtn.Text
                    key = objBtn.Tag
                    'lblROSCategory.Text = BtnText
                    '''' To Fill by default One category Items (Allergies)   
                    FillROSCategory1(ROSCategoryRow(1).ToString)
                    i = i + 1

                    Try
                        objBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                        objBtn.BackgroundImageLayout = ImageLayout.Stretch
                    Catch ex As Exception

                    End Try
                End If

                'objBtn.BackColor = BackColor.LightSkyBlue
                AddHandler objBtn.Click, AddressOf objBtn_Click
                AddHandler objBtn.MouseHover, AddressOf objBtn_MouseHover
                AddHandler objBtn.MouseLeave, AddressOf objBtn_MouseLeave
            Next
        End If
        ''''' To Fill by default One category Items (Allergies)   
        'FillROSCategory1(dt.Rows(0)(1).ToString)
        pnlCatSearch.Dock = DockStyle.Top
    End Sub

    Private Sub FillPrevROS()
        If trvPrevROS.GetNodeCount(False) <= 0 Then

            trvPrevROS.Nodes.Add("Patient ROS")
            trvPrevROS.Nodes.Item(0).ImageIndex = 0
            trvPrevROS.Nodes.Item(0).SelectedImageIndex = 0
            With trvPrevROS.Nodes.Item(0)

                '.Nodes.Add(New myTreeNode("Current", 0))
                '.Nodes.Add(New myTreeNode("YesterDay", 1))
                '.Nodes.Add(New myTreeNode("Last Week", 2))
                '.Nodes.Add(New myTreeNode("Last Month", 3))
                '.Nodes.Add(New myTreeNode("Older", 4))

                Dim mychild As myTreeNode

                mychild = New myTreeNode("Current", 0)
                mychild.ForeColor = Color.Blue
                'mychild.ImageIndex = 0
                'mychild.SelectedImageIndex = 0
                .Nodes.Add(mychild)

                mychild = New myTreeNode("Yesterday", 1)
                mychild.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
                'mychild.ImageIndex = 0
                'mychild.SelectedImageIndex = 0
                .Nodes.Add(mychild)

                mychild = New myTreeNode("Last Week", 2)
                mychild.ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
                'mychild.ImageIndex = 0
                'mychild.SelectedImageIndex = 0
                .Nodes.Add(mychild)

                mychild = New myTreeNode("Last Month", 3)
                mychild.ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
                'mychild.ImageIndex = 0
                'mychild.SelectedImageIndex = 0
                .Nodes.Add(mychild)

                mychild = New myTreeNode("Older", 4)
                mychild.ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)
                'mychild.ImageIndex = 0
                'mychild.SelectedImageIndex = 0
                .Nodes.Add(mychild)
            End With
            RefreshROSHistory()
        End If
    End Sub

    'Private Function checkifrootnode(ByVal objmytreenode As TreeNode) As Boolean
    '    Dim objchild As myTreeNode
    '    'For Each objchild In trvTarget.Nodes
    '    '    If objmytreenode Is objchild Then
    '    '        Return True
    '    '        Exit Function
    '    '    End If
    '    'Next
    '    Return False
    'End Function

    Private Sub Set_RecordLock(ByVal locked As Boolean)
        If locked = True Then
            tsbtn_SaveAndClose.Enabled = False
        Else
            tsbtn_SaveAndClose.Enabled = True
        End If
    End Sub

    Public Sub objBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles objBtn.Click
        Try
            trvSource.Nodes.Clear()
            FillROSCategory1(sender.Text)
            '''''BtnText is global variable which we need for further Ref.

            'objBtn = New Button
            'objBtn.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
            'objBtn.ForeColor = Color.White
            'objBtn.Text = BtnText
            'Me.pnltrvSource.Controls.Add(objBtn)
            'AddHandler objBtn.Click, AddressOf objBtn_Click
            'objBtn.Dock = DockStyle.Bottom
            'objBtn.FlatStyle = FlatStyle.Flat

            Dim i As Integer
            For i = 0 To pnltrvSource.Controls.Count - 1
                If TypeOf pnltrvSource.Controls.Item(i) Is Button Then
                    If UCase(pnltrvSource.Controls.Item(i).Text) = UCase(CType(sender, Button).Text) Then
                        sender = pnltrvSource.Controls.Item(i)


                        pnltrvSource.Controls.Item(i).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                        pnltrvSource.Controls.Item(i).BackgroundImageLayout = ImageLayout.Stretch
                        'pnltrvSource.Controls.Item(i).BackColor = Color.FromArgb(255, 197, 108)
                    End If
                End If
            Next

            BtnText = sender.Text
            key = sender.Tag
            ''For i = 0 To pnlCatBtn.Controls.Count - 1
            ''    If TypeOf pnlCatBtn.Controls.Item(i) Is Button Then
            ''        pnlCatBtn.Controls.Item(i).Dock = DockStyle.Bottom
            ''    End If
            ''Next
            For i = 0 To pnltrvSource.Controls.Count - 1
                If TypeOf pnltrvSource.Controls.Item(i) Is Button Then
                    If Not pnltrvSource.Controls.Item(i) Is CType(sender, Button) Then
                        pnltrvSource.Controls.Item(i).Dock = DockStyle.Bottom


                        pnltrvSource.Controls.Item(i).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        pnltrvSource.Controls.Item(i).BackgroundImageLayout = ImageLayout.Stretch
                        pnltrvSource.Controls.Item(i).BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    End If
                End If
            Next

            sender.Dock = DockStyle.Top

            'sender.Visible = False
            'lblROSCategory.Text = BtnText

            '''' to get Reset Search
            Call ResetSearch()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub NewROS()
        'Dim i As Integer
        'For i = 0 To trvTarget.GetNodeCount(False) - 1
        '    trvTarget.Nodes.Item(i).Nodes.Clear()
        'Next

        'gnVisitID = 0

        lblVisitDate.Tag = 0

        '''''' Word Object
        ''wdNarration.CreateNew("Word.Document")
        ''SetWordObjectEntry()
        ''oCurDoc = wdNarration.ActiveDocument
        '''''' Word Object End
        blnModify = False

        'lblVisitID.Text = gnVisitID
        lblVisitDate.Text = Now.Date

        tsbtn_Show.Checked = False

        '''' to get Reset Search
        Call ResetSearch()

    End Sub

    Private Sub ResetSearch()
        txtSearchCategory.Text = ""
        txtSearchCategory.Focus()
    End Sub

    Public Sub CloseROS()
        'If trvTarget.GetNodeCount(False) > 0 Then
        '    If trvTarget.Nodes(0).GetNodeCount(True) > 0 And blnChangesMade = True Then
        '        If MessageBox.Show("Do you want to save changes", "Patient ROS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
        '            SaveROS()
        '        End If
        '    End If
        'End If

        If C1PatientRos.Rows.Count >= 1 And blnChangesMade = True Then
            If MessageBox.Show("Do you want to save changes", "Patient ROS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                SaveROS()
            End If
        End If
        Me.Close()
    End Sub

    Public Sub FinishROS()
        SaveROS()
        Me.Close()
    End Sub

    Public Sub SaveROS()
        ''<Start> By Pramod 25/04/2007
        'Dim i As Integer
        'Dim j As Integer
        'Dim count As Int16
        'Dim strNode As String
        'Dim Node As TreeNode
        'Dim ParentNode As TreeNode
        'Dim blnAllowSave As Boolean

        '' Code to check whether there are Histories Entered to save or Nor
        'For i = 0 To trvTarget.GetNodeCount(False) - 1
        '    If trvTarget.Nodes.Item(i).GetNodeCount(False) > 0 Then
        '        blnAllowSave = True
        '        Exit For
        '    End If
        'Next

        '' to Promt User that he is Not Entered Histories 
        'If blnAllowSave = False Then
        '    MessageBox.Show("No ROS is Added to Save", "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If

        ''''' To Create VisitID If Visit is Not Selected Otherwise Use Selcted  
        'If lblVisitDate.Tag = 0 Then
        '    lblVisitDate.Tag = GenerateVisitID()
        'End If

        '''''' If record is in Edit Mode Then While Save it Delete Its Previous Entries
        '''''If blnModify = True Then
        '''''    objclsPatientROS.DeleteROS(lblVisitDate.Tag, lblPatientCode.Tag)
        '''''End If

        'Dim ArrLst As New ArrayList

        '''''' for AuditLog
        ' ''count = 0
        ''''''

        'For i = 0 To trvTarget.GetNodeCount(False) - 1
        '    ' Node = trvTarget.Nodes.Item(i)
        '    ParentNode = trvTarget.Nodes.Item(i)

        '    For j = 0 To ParentNode.GetNodeCount(False) - 1
        '        Node = ParentNode.Nodes.Item(j)

        '        If InStr(Node.Text, ":-", CompareMethod.Text) <= 0 Then
        '            Note = ""
        '            strNode = Node.Text
        '        Else
        '            Note = Trim(Mid(Trim(Node.Text), InStr(Trim(Node.Text), ":-", CompareMethod.Text) + 2, Len(Trim(Node.Text))))
        '            strNode = Node.Text.Substring(0, InStr(Trim(Node.Text), ":-", CompareMethod.Text) - 2)
        '            'strNode = Split(Node.Text, ":-")
        '        End If
        '        'lblVisitID.Text = gnVisitID

        '        ''If objclsPatientROS.CheckDuplicate(lblVisitDate.Tag, Trim(ParentNode.Text), Trim(strNode)) = True Then
        '        ''    If Trim(ParentNode.Text) <> "Allergies" Then
        '        ''        objclsPatientROS.AddNewROS(count, 1, lblVisitDate.Tag, lblPatientCode.Tag, Trim(ParentNode.Text), Trim(strNode), Note)
        '        ''    End If
        '        ''Else
        '        ''    objclsPatientROS.AddNewROS(count, 0, lblVisitDate.Tag, lblPatientCode.Tag, Trim(ParentNode.Text), Trim(strNode), Note)
        '        ''End If
        '        '''''''' for AuditLog
        '        ''count = 1

        '        Dim lst As New myList

        '        lst.HistoryCategory = Trim(ParentNode.Text)
        '        lst.HistoryItem = Trim(strNode)
        '        lst.Description = Note

        '        ArrLst.Add(lst)

        '        Note = ""
        '    Next
        'Next

        'If objclsPatientROS.AddNewROS(0, lblVisitDate.Tag, lblPatientCode.Tag, ArrLst) = False Then
        '    Exit Sub
        'End If

        '' to clear target treeview
        '' Dim i As Integer
        'For i = 0 To trvTarget.GetNodeCount(False) - 1
        '    trvTarget.Nodes.Item(i).Nodes.Clear()
        'Next

        'NewROS()
        'If pnlPrevROS.Visible = True Then
        '    RefreshROSHistory()
        'End If
        '' <End> By Pramod 25/05/2007
        'eArg as System.Windows.Forms.TreeViewEventArgs

        Try
            Dim i As Integer
            'Dim j As Integer
            'Dim count As Int16
            ''Dim blnAllowSave As Boolean

            ''''' To Create VisitID If Visit is Not Selected Otherwise Use Selcted  
            If m_VisitID = 0 Then
                m_VisitID = GenerateVisitID(m_PatientID)
                lblVisitDate.Tag = m_VisitID
            End If

            '' Bug #12852: Not able to delete ROS item
            '' No need to check row count in the grid, if row deleted from thee grid
            '' should be deleted from table.
            '' so the following condition removed.

            'If C1PatientRos.Rows.Count > 1 Then
            '    '' blnAllowSave = True
            'Else
            '    ''blnAllowSave = False
            '    '' MessageBox.Show("No ROS is added to save", "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    If Not blnModify Then
            '        Exit Sub
            '    End If
            'End If

            Dim StrCategory As String
            Dim StrItem As String
            Dim StrComment As String
            Dim StrSource As String
            Dim nPatFormID As String
            Dim StrDate As String
            Dim arrLst As New ArrayList
            With C1PatientRos
                .Row = 0
                For i = 1 To .Rows.Count - 1
                    If C1PatientRos.GetData(i, Col_ItemName) <> "" Then
                        StrCategory = .GetData(i, Col_CategoryID) & ""
                        StrItem = .GetData(i, Col_ItemName) & ""
                        StrComment = .GetData(i, Col_Comments) & ""
                        StrSource = .GetData(i, Col_Source) & ""
                        nPatFormID = .GetData(i, Col_PatientFormID) & ""
                        StrDate = .GetData(i, Col_DateEntered) & ""

                        Dim lst As New myList

                        lst.HistoryCategory = StrCategory
                        lst.HistoryItem = StrItem
                        lst.Description = StrComment
                        lst.ROSSource = StrSource
                        If nPatFormID <> "" Then
                            lst.ROSPatientFormID = nPatFormID
                        Else
                            lst.ROSPatientFormID = 0
                        End If

                        If StrDate <> "" Then
                            lst.ROSDateEntered = StrDate
                        Else
                            lst.ROSDateEntered = Nothing
                        End If


                        arrLst.Add(lst)

                    End If

                Next
                blnModify = True
                If lblVisitDate.Tag = 0 Then
                    m_VisitID = GenerateVisitID(m_PatientID)
                    lblVisitDate.Tag = m_VisitID
                End If
                If objclsPatientROS.AddNewROS(0, lblVisitDate.Tag, lblPatientCode.Tag, arrLst) = False Then
                    Exit Sub
                End If

            End With
            If arrDataDictionary.Count > 0 Then
                DeleteROSDataDictionary()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub
    'Private Sub FillROSCategory()
    '    Dim ROSTable As New DataTable
    '    Dim cnt As Integer
    '    ROSTable = objclsPatientROS.GetAllCategory("ROS")
    '    Dim ROSNode As myTreeNode
    '    cnt = ROSTable.Rows.Count

    '    For Each ROSRow As DataRow In ROSTable.Rows
    '        'ROSNode = trvTarget.Nodes.Add(CStr( ROSRow("sDescription")))
    '        Dim objTreeNode As New myTreeNode(ROSRow.Item(1), ROSRow.Item(0))
    '        trvTarget.Nodes.Add(objTreeNode)
    '    Next ROSRow

    'End Sub
    Private Function FillROSCategory() As DataTable
        Dim ROSTable As New DataTable
        ROSTable = objclsPatientROS.GetAllCategory("ROS")

        'Dim ROSNode As myTreeNode
        Dim arrCategorycolor(12) As String
        Dim i As Integer
        'btnOK.ForeColor.DarkOrange()

        arrCategorycolor(0) = "Red"
        arrCategorycolor(1) = "DarkOrange"
        arrCategorycolor(2) = "Maroon"
        arrCategorycolor(3) = "Blue"
        arrCategorycolor(4) = "Green"
        arrCategorycolor(5) = "BlueViolet"
        arrCategorycolor(6) = "Red"
        arrCategorycolor(7) = "DarkOrange"
        arrCategorycolor(8) = "Maroon"
        arrCategorycolor(9) = "Blue"
        arrCategorycolor(10) = "Green"
        arrCategorycolor(11) = "BlueViolet"

        For Each ROSRow As DataRow In ROSTable.Rows
            'ROSNode = trvTarget.Nodes.Add(CStr( ROSRow("sDescription")))
            Dim objTreeNode As New myTreeNode(ROSRow.Item(1), ROSRow.Item(0))
            objTreeNode.ImageIndex = 0
            objTreeNode.SelectedImageIndex = 0
            'trvTarget.Nodes.Add(objTreeNode)
            objTreeNode.ForeColor = System.Drawing.Color.FromName(arrCategorycolor(i))

            'arrCategorycolor(i).ToString()
            i = i + 1
            If i >= 10 Then
                i = 0
            End If
        Next ROSRow

        Return ROSTable
    End Function

    Private Sub FillROSCategory1_old(ByVal strGroup As String)
        'code commented from 19/01/2006 by supriya

        'If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
        '    'If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnOpenFromExam = False Then
        '    Try
        '        RemoveVoiceCommands(1000)
        '    Catch ex As Exception
        '    End Try
        'End If
        'code commented from 19/01/2006 by supriya

        'Dim ROSNode As myTreeNode
        ' ROSNode = tvwCustom.Nodes.Add(CStr(strGroup))
        Dim ROSTable As New DataTable

        'strSQL = " SELECT ROS_MST.nROSID,ROS_MST.sDescription " _
        '        & " FROM ROS_MST INNER JOIN Category_MST " _
        '        & " ON ROS_MST.nCategoryID=Category_MST.nCategoryID WHERE Category_MST.sDescription='" & strGroup & "' " _
        '        & " AND Category_MST.sCategoryType='ROS' ORDER BY ROS_MST.nCategoryID"

        ROSTable = objclsPatientROS.GetAllROS(strGroup)

        dtSource = ROSTable

        trvSource.Nodes.Clear()
        For Each ROSRow As DataRow In ROSTable.Rows
            'ROSNode = tvwCustom.Nodes.Add(CStr(ROSRow("sDescription")))
            Dim objTreeNode As New myTreeNode(ROSRow.Item(1), ROSRow.Item(0))
            trvSource.Nodes.Add(objTreeNode)
            'For Voice commands

            'code commented from 19/01/2006 by supriya
            'If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            '    'If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnOpenFromExam = False Then
            '    Try
            '        AddVoiceCommand(1000, objTreeNode.Text)
            '    Catch ex As Exception
            '    End Try
            'End If
            'code commented from 19/01/2006 by supriya

            'commented by mahesh on 31.05.2005 1:28pm
            'ROSNode = trvSource.Nodes.Add(CStr(ROSRow("sDescription")))
        Next ROSRow

    End Sub

    Public Sub trvSource_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trvSource.ItemDrag    ', trvTarget.ItemDrag
        'Set the drag node and initiate the DragDrop
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Public Sub trvSource_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvSource.DragEnter
        'See if there is a TreeNode being dragged

        If e.Data.GetDataPresent("gloEMR.myTreeNode", True) Then
            'TreeNode found allow move effect
            e.Effect = DragDropEffects.Move
        Else
            'No TreeNode found, prevent move
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Public Sub trvTarget_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs)

        'Check that there is a TreeNode being dragged
        If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

        ''''Get the TreeView raising the event (incase multiple on form)
        'Dim selectedTreeview As TreeView = CType(sender, TreeView)
        Dim selectedTreeview As TreeView = CType(sender, TreeView)

        'As the mouse moves over nodes, provide feedback to the user
        'by highlighting the node that is the current drop target

        ''Commented on 30/08.2005. by Mahesh
        Dim pt As Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))

        ' commmented by Mahesh (31.05.20005 11.31 am)
        'Dim targetNode As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)

        Dim targetNode As myTreeNode = selectedTreeview.GetNodeAt(pt)
        'Remove the Drop Node from its Current Location
        'if there is no target node add dragNode to the bottom of the TreeView root
        'Nodes 
        'See if the targetNode is currently selected, if so no need to validate again

        If Not (selectedTreeview Is targetNode) Then
            'Select the node currently under the cursor
            selectedTreeview.SelectedNode = targetNode

            'Check that the selected node is not the dropNode and also that it
            'is not a child of the dropNode and therefore an invalid target
            Dim dropNode As TreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)
            Do Until targetNode Is Nothing
                If targetNode Is dropNode Then
                    e.Effect = DragDropEffects.None
                    Exit Sub
                End If
                targetNode = targetNode.Parent
            Loop
        End If

        'Currently selected node is a suitable target, allow the move
        'trvTarget.ExpandAll()
        e.Effect = DragDropEffects.Move

        '''' to get Reset Search
        Call ResetSearch()

    End Sub

    Public Sub trvSource_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvSource.DragDrop

        'Check that there is a TreeNode being dragged
        'If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
        If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedTreeview As TreeView = CType(sender, TreeView)

        'Get the TreeNode being dragged
        'Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)
        Dim dropNode As myTreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)

        'The target node should be selected from the DragOver event
        'Dim targetNode As TreeNode = selectedTreeview.SelectedNode

        Dim targetNode As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)
        'Remove the drop node from its current location
        'dropNode.Remove()

        'If there is no targetNode add dropNode to the bottom of the TreeView root
        'nodes, otherwise add it to the end of the dropNode child nodes
        'If targetNode Is Nothing Then
        '    selectedTreeview.Nodes.Add(dropNode)
        'elseIf targetNode Is selectedTreeview.Nodes.Item(0) Then

        'Dim i As Integer
        'Dim j As Integer
        Dim ndeSelectedNode As New TreeNode
        '<Start> Pramod 20072605
        'trvTarget.BeginUpdate()
        'If selectedTreeview Is trvTarget Then
        '    For i = 0 To trvTarget.GetNodeCount(False) - 1
        '        If BtnText = trvTarget.Nodes.Item(i).Text Then
        '            Dim childNode As TreeNode

        '            childNode = trvTarget.Nodes.Item(i) 'Rooot Nodes
        '            If childNode.GetNodeCount(False) = 0 Then
        '                Dim nde1 As New myTreeNode
        '                nde1.Text = dropNode.Text
        '                nde1.Tag = dropNode.Text
        '                nde1.ForeColor = trvTarget.Nodes(i).ForeColor
        '                trvTarget.Nodes.Item(i).Nodes.Add(nde1)
        '                nde1.EnsureVisible()
        '                ndeSelectedNode = nde1
        '                'dropNode.Remove()
        '                Exit For
        '            End If
        '            For j = 0 To childNode.GetNodeCount(False) - 1
        '                If dropNode.Text = childNode.Nodes.Item(j).Tag Then
        '                    trvTarget.EndUpdate()
        '                    Exit Sub
        '                End If
        '            Next
        '            Dim nde As New myTreeNode
        '            nde.Text = dropNode.Text
        '            nde.Tag = dropNode.Text

        '            nde.ForeColor = trvTarget.Nodes(i).ForeColor
        '            trvTarget.Nodes.Item(i).Nodes.Add(nde)
        '            nde.EnsureVisible()
        '            ndeSelectedNode = nde
        '            'dropNode.Remove()
        '            Exit For
        '        End If
        '    Next
        'End If
        '<END> Pramod 20072605
        ''''''''''''''''
        '' Comented by Mahesh ///// working

        ' If targetNode Is selectedTreeview.Nodes.Item(0) Then

        ''If selectedTreeview.Nodes.Item(targetNode.Index).Text = BtnText Then
        ''    targetNode.Nodes.Add(dropNode.Text)
        ''    dropNode.Remove()
        ''End If

        ' End If
        ''''''''''''''''

        'targetNode.Index(selectedTreeview.Nodes.IndexOf(dropNode))

        'If targetNode Is selectedTreeview.Nodes.Item(dropNode.Index) Then
        '    If selectedTreeview.Nodes.Item(dropNode.Index).Text = BtnText Then
        '        targetNode.Nodes.Add(dropNode.Text)
        '        dropNode.Remove()
        '    End If
        'End If
        'If selectedTreeview.Nodes.Item(0).Text = BtnText Then

        'targetNode.Nodes.Add(dropNode.Text)
        'dropNode.Remove()
        'End If
        'End If

        'Ensure the newley created node is visible to the user and select it
        'dropNode.EnsureVisible()
        'selectedTreeview.SelectedNode = dropNode
        ''trvTarget.SelectedNode = dropNode
        'trvTarget.ExpandAll()
        'trvTarget.EndUpdate()

        'If IsNothing(ndeSelectedNode) = False Then
        '    trvTarget.SelectedNode = ndeSelectedNode
        '    ndeSelectedNode.EnsureVisible()
        'End If

    End Sub

    ' This subroutine handles the Click events of all the dynamically generated
    '   buttons.  It is attached to all the buttons using the AddHandler function
    '   at the time of button creation.
    Private Sub myButtonHandler_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Verify that the type of control triggering this event is indeed
        '   a Button. This is necessary since this handler can be attached
        '   to any event.
        If TypeOf sender Is Button Then
            ' Let the user know what Button was pressed.
            MsgBox(CType(sender, Button).Text + " was pressed!", _
                    MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    ' This subroutine handles the MouseHover events of all the dynamically generated
    '   buttons.  It is attached to all the buttons using the AddHandler function
    '   at the time of button creation.

    Private Sub myButtonHandler_MouseHover(ByVal sender As Object, ByVal e As EventArgs)
        ' Verify that the type of control triggering this event is indeed
        '   a Button. This is necessary since this handler can be attached
        '   to any event.
        If TypeOf sender Is Button Then
            ' Let the user know what Button was hovered over.
            MsgBox(CType(sender, Button).Text + " was hovered over!", _
                    MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    'Private Sub trvTarget_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvTarget.DoubleClick
    ''''''Commented by Mahesh 08/10/2005 
    '    Try
    '        'Dim strNodeName As String
    '        'check if it open for root node or Child Node 
    '        Dim blnAllergies As Boolean
    '        If IsNothing(sender.selectednode.parent) = True Then
    '            Exit Sub
    '            'chech if it open for Allergies , so as to hide combo & checkbox 
    '        ElseIf sender.selectednode.parent.text = "Allergies" Then
    '            blnAllergies = True
    '        End If
    '        strNodeName = sender.selectednode.text
    '        Dim strNotes As String

    '        If InStr(sender.selectednode.text, ":-") > 0 Then
    '            strNotes = Trim(Mid(Trim(sender.selectednode.Text), InStr(Trim(sender.selectednode.Text), ":-", CompareMethod.Text) + 2, Len(Trim(sender.selectednode.Text))))
    '            strNodeName = sender.selectednode.Text.Substring(0, InStr(Trim(sender.selectednode.Text), ":-", CompareMethod.Text) - 2)
    '            'strNode = Split(Node.Text, ":-")
    '        Else
    '            strNotes = ""
    '        End If

    '        Dim objfrmpatComment As New frmPatROSComment(blnAllergies, strNotes)
    '        objfrmpatComment.Text = BtnText
    '        objfrmpatComment.lblHeading.Text = strNodeName
    '        objfrmpatComment.ShowDialog(Me)

    '        If frmPatROSComment.blnNoteAdded = True Then
    '            strNodeName = strNodeName & " :- " & Note
    '            trvTarget.SelectedNode.Text = strNodeName
    '            strNodeName = ""
    '            frmPatROSComment.blnNoteAdded = False
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    Private Sub trvSource_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvSource.DoubleClick
        Try
            trvSource_DblClick()

            '''' to get Reset Search
            Call ResetSearch()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvSource_DblClick()

        ''''Check that there is a TreeNode being dragged
        ''''Get the TreeView raising the event (incase multiple on form)
        ' <START> Pramod 20072505
        'Dim selectedTreeview As New TreeView

        '''''Get the TreeNode being dragged

        '''''The target node should be selected from the DragOver event
        '''''Dim targetNode As TreeNode = selectedTreeview.SelectedNode

        'Dim targetNode As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)
        '''''Remove the drop node from its current location

        'Dim i As Integer
        'Dim j As Integer

        'Dim ndeSelectedNode As New TreeNode
        'trvTarget.BeginUpdate()
        'For i = 0 To trvTarget.GetNodeCount(False) - 1
        '    If LCase(Trim(BtnText)) = LCase(Trim(trvTarget.Nodes.Item(i).Text)) Then
        '        Dim childNode As myTreeNode

        '        childNode = trvTarget.Nodes.Item(i)
        '        If childNode.GetNodeCount(False) = 0 Then
        '            Dim nde1 As New TreeNode
        '            nde1.Text = trvSource.SelectedNode.Text
        '            nde1.Tag = trvSource.SelectedNode.Text

        '            nde1.ForeColor = trvTarget.Nodes(i).ForeColor
        '            nde1.ImageIndex = 1
        '            nde1.SelectedImageIndex = 1
        '            trvTarget.Nodes.Item(i).Nodes.Add(nde1)
        '            nde1.EnsureVisible()
        '            ndeSelectedNode = nde1
        '            blnChangesMade = True
        '            ''''trvSource.SelectedNode.Remove()
        '            Exit For
        '        End If
        '        For j = 0 To childNode.GetNodeCount(False) - 1
        '            If LCase(Trim(trvSource.SelectedNode.Text)) = LCase(Trim(childNode.Nodes.Item(j).Tag)) Then
        '                trvTarget.EndUpdate()
        '                Exit Sub
        '            End If
        '        Next
        '        Dim nde As New myTreeNode
        '        nde.Text = trvSource.SelectedNode.Text
        '        nde.Tag = trvSource.SelectedNode.Text

        '        nde.ForeColor = trvTarget.Nodes(i).ForeColor
        '        nde.ImageIndex = 1
        '        nde.SelectedImageIndex = 1
        '        trvTarget.Nodes.Item(i).Nodes.Add(nde)
        '        nde.EnsureVisible()
        '        ndeSelectedNode = nde
        '        blnChangesMade = True
        '        ''''trvSource.SelectedNode.Remove()
        '        Exit For
        '    End If
        'Next

        '''''For i = 0 To trvTarget.GetNodeCount(False) - 1
        '''''    If BtnText = trvTarget.Nodes.Item(i).Text Then
        '''''        trvTarget.Nodes.Item(i).Nodes.Add(sender.selectednode.Text)
        '''''        sender.selectednode.Remove()
        '''''    End If
        '''''Next

        ' ''Ensure the newley created node is visible to the user and select it
        'trvTarget.ExpandAll()
        'trvTarget.EndUpdate()

        'If IsNothing(ndeSelectedNode) = False Then
        '    trvTarget.SelectedNode = ndeSelectedNode
        '    ndeSelectedNode.EnsureVisible()
        'End If
        '<End> Pramod 20072505
        If IsNothing(trvSource.SelectedNode) = True Then
            Exit Sub
        End If


        Dim i As Integer
        Dim j As Integer
        With C1PatientRos
            Dim _Row As Integer = 0
            'Dim _tempID As Long

            'C1HistoryDetails.Rows.Count = 1

            For i = 1 To C1PatientRos.Rows.Count - 1

                If C1PatientRos.GetData(i, Col_CategoryID) = BtnText Then                    '' TO Check Duplicate History Item in A Category
                    For j = 1 To C1PatientRos.Rows.Count - 1
                        If .GetData(j, Col_ItemName) = trvSource.SelectedNode.Text And .GetData(j, Col_CategoryID) = BtnText Then
                            Exit Sub
                        End If
                    Next

                    '' TO Insert the New Item At the END of the CAtegory
                    Try
                        If .GetData(i, Col_CategoryID) <> .GetData(i + 1, Col_CategoryID) Then
                            '''' If The Current Category ID Is Not Matchs with the thw Category ID  at Next ROW 
                            '' Then Add new Row at Just After the Current Row i.i At the END of the Category
                            .Rows.Insert(i + 1)
                            _Row = i + 1
                            Exit For
                        End If
                    Catch ex As Exception
                        '''' If The System Does Not Get the ROW At (i+1) Position then it Throws the Exception
                        '' i.e we ahve to add the Row at the End 
                        .Rows.Insert(i + 1)
                        _Row = i + 1
                        Exit For
                    End Try
                End If
            Next


            If _Row = 0 Then
                ''  Category Is Not exists
                .Rows.Add()
                _Row = .Rows.Count - 1
                .SetData(_Row, Col_CategoryID, BtnText)
                .SetData(_Row, Col_CategoryName, BtnText)
                '.SetData(_Row, Col_HistoryCategory_Hidden, BtnText)
                .Rows.Insert(_Row + 1)
                _Row = _Row + 1
            End If

            .SetData(_Row, Col_CategoryID, BtnText)
            '.SetData(_Row, Col_HistoryCategory_Hidden, BtnText)
            .SetData(_Row, Col_ItemName, trvSource.SelectedNode.Text)
            '.SetData(_Row, Col_Reaction, "")
            .Row = _Row
        End With






    End Sub

    Public Sub ShowHidePreviousROS(ByVal strText As String)
        If pnlPrevROS.Visible = False Then
            If strText = "Show" Then
                Call FillPrevROS()
                pnlPrevROS.Visible = True
                'btnPrevROS.Text = "Hide Prev ROS"
                tsbtn_Show.Text = "Hi&de"
                tsbtn_Show.Image = Global.gloEMR.My.Resources.Resources.Hide
                tsbtn_Show.ImageAlign = ContentAlignment.MiddleCenter
                tsbtn_Show.ToolTipText = "Hide Patient ROS"
                ' Fill_PrevROS()
                trvPrevROS.ExpandAll()
                txtsearchROS.Select()
            End If
        Else
            If strText = "Hide" Then
                pnlPrevROS.Visible = False
                'btnPrevROS.Text = "Show Prev ROS"
                tsbtn_Show.Text = "Sh&ow"
                tsbtn_Show.Image = Global.gloEMR.My.Resources.Resources.Show
                tsbtn_Show.ImageAlign = ContentAlignment.MiddleCenter
                tsbtn_Show.ToolTipText = "Show Patient ROS"
                trvPrevROS.Nodes.Clear()
                txtSearchCategory.Select()
            End If
        End If
        'RefreshROSHistory()
    End Sub

    Public Sub ShowHideROSNarrative()
        ''If pnlPrevROS.Visible = False Then
        ''    pnlPrevROS.Visible = True
        ''    'btnPrevROS.Text = "Hide Prev ROS"
        ''    tblShow.Text = "Hide"
        ''    tblShow.ToolTipText = "Hide Patient ROS"
        ''    ' Fill_PrevROS()
        ''    trvPrevROS.ExpandAll()
        ''Else
        ''    pnlPrevROS.Visible = False
        ''    'btnPrevROS.Text = "Show Prev ROS"
        ''    tblShow.Text = "Show"
        ''    tblShow.ToolTipText = "Show Patient ROS"
        ''End If

        ''Dim dt As DataTable
        ''dt = objclsPatientROS.SelectNarration(lblVisitDate.Tag, lblPatientCode.Tag) ' Word Object


        'If pnlWordComp.Visible = False Then
        '    pnlWordComp.Visible = True


        '    ''wdNarration.CreateNew("Word.Document")

        '    ''''''''working code commented by Mahesh 14/10/2005 08:50 pm 
        '    ''Dim dt As DataTable
        '    ''dt = objclsPatientROS.SelectNarration(lblVisitDate.Tag, lblPatientCode.Tag)

        '    '''''' Word Object
        '    'Dim mstream As ADODB.Stream
        '    'Dim strFileName As String
        '    'mstream = New ADODB.Stream
        '    'mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
        '    ''wdNarration.Close()
        '    'mstream.Open()
        '    ''   If dt.Rows.Count > 0 Then
        '    ''        mstream.Write(dt.Rows(0)(0))
        '    'strFileName = Application.StartupPath & "\Temp\Temp5.doc"
        '    'mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
        '    'mstream.Close()
        '    'wdNarration.Open(strFileName)
        '    ''Else
        '    ''   wdNarration.CreateNew("Word.Document")
        '    ''End If

        '    'SetWordObjectEntry()
        '    'oCurDoc = wdNarration.ActiveDocument
        '    tblshowNarrative.Pushed = True
        'Else
        '    pnlWordComp.Visible = False
        '    'wdNarration.CreateNew("Word.Document")
        '    'SetWordObjectEntry()
        '    'oCurDoc = wdNarration.ActiveDocument
        '    tblshowNarrative.Pushed = False
        'End If
        '' Word Object End

    End Sub

    ''Private Sub Fill_PrevROS()
    ''    Dim dt As New DataTable
    ''    dt = objclsPatientROS.GetAllVisits()
    ''    Dim ROSNode As myTreeNode
    ''    For Each ROSRow As DataRow In dt.Rows
    ''        'ROSNode = trvTarget.Nodes.Add(CStr( ROSRow("sDescription")))
    ''        Dim objTreeNode As New myTreeNode(ROSRow.Item(1), ROSRow.Item(0))
    ''        trvTarget.Nodes.Add(objTreeNode)
    ''    Next ROSRow
    ''End Sub

    Private Sub trvPrevROS_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvPrevROS.AfterSelect
        Dim dt As DataTable
        Try
            If trvPrevROS.SelectedNode.Text = "Patient ROS" Then
                '    If trvPrevROS.SelectedNode.Parent.Text <> "Patient ROS" Then
                Exit Sub
            End If

            'to Get visit date
            If IsNothing(trvPrevROS.SelectedNode.Parent) = True Then
                '    If trvPrevROS.SelectedNode.Parent.Text <> "Patient ROS" Then
                Exit Sub
            End If

            If trvPrevROS.SelectedNode.Parent Is trvPrevROS.Nodes(0) Then
                Exit Sub
            End If

            'If trvPrevROS.SelectedNode.Parent.Text = "Patient ROS" Then
            '    'If trvPrevROS.SelectedNode Is trvPrevROS.Nodes(-1) Then
            '    '    Exit Sub
            '    'End If
            '    Dim ParentNode As myTreeNode
            '    ParentNode = CType(trvPrevROS.SelectedNode, myTreeNode)
            '    ParentNode.Nodes.Clear()

            '    If trvPrevROS.SelectedNode.Text = "Current" Then
            'dt = objclsPatientROS.GetPrevROS("C", lblPatientCode.Tag, Now.Date)
            '    ElseIf trvPrevROS.SelectedNode.Text = "Yesterday" Then
            '        dt = objclsPatientROS.GetPrevROS("Y", lblPatientCode.Tag, Now.Date)
            '    ElseIf trvPrevROS.SelectedNode.Text = "Last Week" Then
            '        dt = objclsPatientROS.GetPrevROS("W", lblPatientCode.Tag, Now.Date)
            '    ElseIf trvPrevROS.SelectedNode.Text = "Last Month" Then
            '        dt = objclsPatientROS.GetPrevROS("M", lblPatientCode.Tag, Now.Date)
            '    ElseIf trvPrevROS.SelectedNode.Text = "Older" Then
            '        dt = objclsPatientROS.GetPrevROS("O", lblPatientCode.Tag, Now.Date)
            '    End If

            '    ' Dim ROSNode As myTreeNode
            '    If Not IsNothing(dt) Then
            '        For Each ROSRow As DataRow In dt.Rows
            '            'ROSNode = trvTarget.Nodes.Add(CStr( ROSRow("sDescription")))
            '            Dim objTreeNode As New myTreeNode(Format(ROSRow.Item(1), "MM/dd/yyyy"), ROSRow.Item(0))   'Appontment Date , VisitID
            '            Select Case ParentNode.Text
            '                Case "Current"
            '                    objTreeNode.ForeColor = Color.Blue
            '                Case "Yesterday"
            '                    objTreeNode.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
            '                Case "Last Week"
            '                    objTreeNode.ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
            '                Case "Last Month"
            '                    objTreeNode.ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
            '                Case "Older"
            '                    objTreeNode.ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)
            '            End Select
            '            objTreeNode.ImageIndex = 4
            '            objTreeNode.SelectedImageIndex = 4
            '            ParentNode.Nodes.Add(objTreeNode)
            '        Next ROSRow
            '    End If
            'Else
            ' To Get Details of that perticular Visit Date
            Dim ChildNode As myTreeNode
            If InStr(trvPrevROS.SelectedNode.Text, ":-") = 0 And InStr(trvPrevROS.SelectedNode.Text, ":") = 0 Then
                ChildNode = trvPrevROS.SelectedNode
                ChildNode.Nodes.Clear()
                dt = objclsPatientROS.SelectPatientROS(ChildNode.Key, lblPatientCode.Tag)
                For Each ROSRow As DataRow In dt.Rows
                    Dim ROSNode As New TreeNode
                    ROSNode.ForeColor = ChildNode.ForeColor
                    ROSNode.ImageIndex = 15
                    ROSNode.SelectedImageIndex = 15
                    ChildNode.Nodes.Add(ROSNode)
                    If ROSRow.Item(2).ToString <> "" Then
                        ROSNode.Text = ROSRow.Item(0).ToString & " : " & ROSRow.Item(1).ToString & ":-" & ROSRow.Item(2).ToString
                    Else
                        ROSNode.Text = ROSRow.Item(0).ToString & " : " & ROSRow.Item(1).ToString
                    End If
                    ROSNode.Tag = ROSRow


                    'ChildNode.Nodes.Add(objTreeNode)
                Next ROSRow
            End If

            ''''Dim ROSNode As myTreeNode
            ''For Each ROSRow As DataRow In dt.Rows
            ''    'ROSNode = trvTarget.Nodes.Add(CStr( ROSRow("sDescription")))
            ''    Dim objTreeNode As New myTreeNode(ROSRow.Item(1), ROSRow.Item(0))
            ''    ChildNode.Nodes.Add(objTreeNode)
            ''Next ROSRow
            'End If
            trvPrevROS.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ''Private Sub trvPrevROS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvPrevROS.Click
    ''    Dim dt As DataTable

    ''    Try
    ''        If IsNothing(sender.selectednode.parent) = True Then
    ''            Dim ParentNode As myTreeNode
    ''            ParentNode = CType(sender.selectednode, myTreeNode)
    ''            ParentNode.Nodes.Clear()

    ''            If sender.selectednode.Text = "Current" Then
    ''                dt = objclsPatientROS.GetPrevROS("C", Format(Now.Date, "MM/dd/yyyy"))
    ''            ElseIf sender.selectednode.Text = "YesterDay" Then
    ''                dt = objclsPatientROS.GetPrevROS("Y", Format(Now.Date, "MM/dd/yyyy"))
    ''            ElseIf sender.selectednode.Text = "Last Week" Then
    ''                dt = objclsPatientROS.GetPrevROS("W", Format(Now.Date, "MM/dd/yyyy"))
    ''            ElseIf sender.selectednode.Text = "Last Month" Then
    ''                dt = objclsPatientROS.GetPrevROS("M", Format(Now.Date, "MM/dd/yyyy"))
    ''            ElseIf sender.selectednode.Text = "Older" Then
    ''                dt = objclsPatientROS.GetPrevROS("O", Format(Now.Date, "MM/dd/yyyy"))
    ''            End If

    ''            Dim ROSNode As myTreeNode
    ''            For Each ROSRow As DataRow In dt.Rows
    ''                'ROSNode = trvTarget.Nodes.Add(CStr( ROSRow("sDescription")))
    ''                Dim objTreeNode As New myTreeNode(ROSRow.Item(1), ROSRow.Item(0))
    ''                sender.selectednode.Nodes.Add(objTreeNode)
    ''            Next ROSRow
    ''        End If
    ''    Catch ex As Exception
    ''        MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''    End Try

    ''End Sub

    Private Sub trvPrevROS_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvPrevROS.DoubleClick
        Try
            trvPrevROS_DblClick()
            'If trvPrevROS.SelectedNode.Text = "Patient ROS" Then
            '    ''''    If trvPrevROS.SelectedNode.Parent.Text <> "Patient ROS" Then
            '    Exit Sub
            'End If

            'Dim i As Integer
            'If IsNothing(trvPrevROS.SelectedNode.Parent) = False Then
            '    If trvPrevROS.SelectedNode.GetNodeCount(False) > 0 Then
            '        For i = 0 To trvTarget.GetNodeCount(False) - 1
            '            trvTarget.Nodes.Item(i).Nodes.Clear()
            '        Next
            '    End If
            'End If

            ''''''to move ROS Details of that particular Date in Target TreeView  for Edit

            'Dim DateNode As myTreeNode
            '''' Dim i As Integer
            '''' DateNode = sender.selectednode.nodes
            'For i = 0 To trvPrevROS.SelectedNode.GetNodeCount(False) - 1
            '    DateNode = trvPrevROS.SelectedNode
            '    ''''lblVisitID.Text = DateNode.Key()
            '    lblVisitDate.Tag = DateNode.Key()


            '    Dim arr() As String 'Srting Array
            '    arr = Split(Trim(DateNode.Nodes.Item(i).Text), ":")
            '    Dim targetParentNode As TreeNode

            '    Dim j As Integer
            '    For j = 0 To trvTarget.GetNodeCount(False) - 1
            '        ''''trvTarget.Nodes.Item(j).Nodes.Clear()

            '        If Trim(arr.GetValue(0).ToString) = Trim(trvTarget.Nodes.Item(j).Text) Then
            '            ''''Dim arrChild() As String
            '            ''''arrChild = Split(Trim(arr.GetValue(1).ToString), ":-")
            '            ''''arr.GetValue(2)

            '            lblVisitDate.Text = DateNode.Text

            '            If arr.Length.ToString = 3 Then
            '                Dim nde As New TreeNode
            '                nde.Text = Trim(arr.GetValue(1).ToString & ":" & arr.GetValue(2).ToString)
            '                nde.ForeColor = trvTarget.Nodes(j).ForeColor
            '                trvTarget.Nodes.Item(j).Nodes.Add(nde)
            '                Exit For
            '            ElseIf arr.Length.ToString = 2 Then
            '                Dim nde As New TreeNode
            '                nde.Text = Trim(arr.GetValue(1).ToString)
            '                nde.ForeColor = trvTarget.Nodes(j).ForeColor
            '                trvTarget.Nodes.Item(j).Nodes.Add(nde)
            '                Exit For
            '            End If
            '        End If
            '    Next
            'Next

            'Dim dt As DataTable
            'dt = objclsPatientROS.SelectNarration(lblVisitDate.Tag, lblPatientCode.Tag)

            ''''' Word Object
            'Dim mstream As ADODB.Stream
            'Dim strFileName As String
            'mstream = New ADODB.Stream
            'mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            'wdNarration.Close()
            'mstream.Open()
            'If dt.Rows.Count > 0 Then
            '    mstream.Write(dt.Rows(0)(0))
            '    strFileName = Application.StartupPath & "\Temp\Temp5.doc"
            '    mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
            '    wdNarration.Open(strFileName)
            '    mstream.Close()
            '    SetWordObjectEntry()
            '    oCurDoc = wdNarration.ActiveDocument

            '    If oCurDoc.Words.Count > 1 Then
            '        pnlWordComp.Visible = True
            '    Else
            '        pnlWordComp.Visible = False
            '    End If

            'Else
            '    ''wdNarration.CreateNew("Word.Document")
            '    pnlWordComp.Visible = False
            'End If

            ''''oCurDoc = wdNarration.ActiveDocument
            '''' Word Object End

            blnModify = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvPrevROS_DblClick()
        If IsNothing(trvPrevROS.SelectedNode) = True Then
            ''--- No Node is Seleceted 
            Exit Sub
        End If

        If trvPrevROS.SelectedNode Is trvPrevROS.Nodes(0) Then
            '' if Selected Node is Root Node 
            Exit Sub
        End If

        If trvPrevROS.SelectedNode.Parent Is trvPrevROS.Nodes(0) Then
            ''--- If selected Node is of  'Current', 'Testerday' etc 
            Exit Sub
        End If

        If trvPrevROS.SelectedNode.GetNodeCount(False) = 0 Then
            '' if Selected Node is LastNode
            Exit Sub
        End If
        Dim DateNode As myTreeNode
        DateNode = trvPrevROS.SelectedNode
        lblVisitDate.Tag = DateNode.Key
        gloUC_PatientStrip1.DTP.Value = Convert.ToDateTime(trvPrevROS.SelectedNode.Text)
        ' '' <><><> Record Level Locking <><><><> 
        ' '' Mahesh - 20070723
        If gblnRecordLocking = True Then
            Dim mydt As mytable
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'mydt = Scan_n_Lock_Transaction(TrnType.PatientROS, gnPatientID, DateNode.Key, DateNode.Text)
            mydt = Scan_n_Lock_Transaction(TrnType.PatientROS, m_PatientID, DateNode.Key, DateNode.Text)
            'end modification 
            If (IsNothing(mydt) = False) Then

                If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                    If MessageBox.Show("This Patient ROS is being modified by " & mydt.Code & " on " & mydt.Description & ". Do you want to open it for view ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        '' Open For view 
                        _blnRecordLock = True
                    Else
                        ''
                        If (IsNothing(mydt) = False) Then
                            mydt.Dispose()
                            mydt = Nothing
                        End If
                        Exit Sub
                    End If
                Else
                    '' If Patient ROS is not locked 
                    If _blnRecordLock = True Then
                        '' Currently Opened ROS is locked by some other User on other Machine
                        '' do nothing
                    Else
                        '' Currently Opened ROS is locked by current User on same Machine
                        '' Unlock Currently Opened ROS  , Pass Currently Opened
                        'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        'Call UnLock_Transaction(TrnType.PatientROS, gnPatientID, lblVisitDate.Tag, Now)
                        Call UnLock_Transaction(TrnType.PatientROS, m_PatientID, lblVisitDate.Tag, Now)
                        'end modification

                    End If
                    _blnRecordLock = False
                End If
                If (IsNothing(mydt) = False) Then
                    mydt.Dispose()
                    mydt = Nothing
                End If
            End If

        End If
        Call Set_RecordLock(_blnRecordLock)


        '''' <><><> Record Level Locking <><><><> 

        Dim i As Integer
        If IsNothing(trvPrevROS.SelectedNode.Parent) = False Then
            If trvPrevROS.SelectedNode.GetNodeCount(False) > 0 Then
                C1PatientRos.Rows.Count = 1
            End If
        End If


        'Dim arrSplit() As String

        C1PatientRos.Rows.Count = 1
        'Dim DateNode As myTreeNode
        'DateNode = trvPrevROS.SelectedNode

        For i = 0 To trvPrevROS.SelectedNode.GetNodeCount(False) - 1
            blnModify = True
            DateNode = trvPrevROS.SelectedNode
            lblVisitDate.Tag = DateNode.Key
            Dim strCategoryID As String
            Dim strCategory As String
            Dim strItem As String = String.Empty
            Dim strComment As String = String.Empty
            Dim strSource As String = String.Empty
            Dim nPatFormID As Int64 = 0
            Dim dtTransactionDate As String = String.Empty
            Dim ROSRow As DataRow = Nothing


            Dim arr() As String 'Srting Array
            Dim arrChild() As String
            arr = Split(Trim(DateNode.Nodes.Item(i).Text), ":", 2)
            ROSRow = DateNode.Nodes.Item(i).Tag
            strCategoryID = CStr(arr.GetValue(0)).Trim
            strCategory = CStr(arr.GetValue(0)).Trim

            If arr.Length = 2 Then
                arrChild = Split(Trim(arr.GetValue(1).ToString), ":-")
                If arrChild.Length = 1 Then
                    strItem = CStr(arrChild.GetValue(0)).Trim
                    strComment = ""
                Else
                    strItem = CStr(arrChild.GetValue(0)).Trim
                    strComment = CStr(arrChild.GetValue(1)).Trim
                End If
            End If
            'strItem = ""
            'strComment = ""
            ''Call funtion for fill flexgrid on treeviewPreviewHistory doubleclick
            blnModify = True
            strSource = ROSRow("sROSSource")
            nPatFormID = ROSRow("nPatientFormID")
            dtTransactionDate = ROSRow("dtTransactionDate")
            Call FillGridTrv(strCategoryID, strCategory, strItem, strComment, strSource, nPatFormID, dtTransactionDate)
        Next

        'For i = 0 To trvPrevROS.SelectedNode.GetNodeCount(False) - 1

        '    blnModify = True

        '    Dim strCategory As String
        '    Dim strHistory As String
        '    Dim strComment As String


        '    Dim arr() As String 'Srting Array
        '    arr = Split(Trim(DateNode.Nodes.Item(i).Text, ":")


        '    strCategory = CStr(arr.GetValue(0)).Trim
        '    strHistory = CStr(arr.GetValue(1)).Trim
        '    strComment = CStr(arr.GetValue(2)).Trim
        '    If InStr(strCategory, "Allerg", CompareMethod.Text) = 1 Then
        '        strRection_Status = CStr(arr.GetValue(3)).Trim
        '    Else
        '        strRection_Status = ""
        '    End If

        '    ''Call funtion for fill flexgrid on treeviewPreviewHistory doubleclick
        '    Call FillGridTrv(strCategory, strHistory, strComment, strRection_Status, intDrugID)
        'Next


    End Sub
    Public Sub mnuRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemove.Click
        'Try
        '    If IsNothing(trvTarget.SelectedNode.Parent) = False Then
        '        trvTarget.SelectedNode.Remove()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
        Try

            Dim _selRow As Integer = C1PatientRos.Row
            With C1PatientRos

                If .GetData(_selRow, Col_ItemName) = "" Then
                    ValidateDataDictionary(.GetData(_selRow, Col_CategoryName))
                    For i As Integer = .Rows.Count - 1 To _selRow Step -1
                        '_selRow = .Row
                        'If _selRow = .Rows.Count - 1 Then
                        '    .Rows.Remove(_selRow)
                        '    Exit Sub
                        'End If
                        If .GetData(_selRow, Col_CategoryID) = .GetData(_selRow + 1, Col_CategoryID) Then
                            .Rows.Remove(_selRow + 1)
                            '    If _selRow + 1 > .Rows.Count - 1 Then
                            '        Exit Sub
                            '    End If
                            '    .Rows.Remove(_selRow + 1)
                            'Else
                        End If
                    Next
                    .Rows.Remove(_selRow)
                Else
                    Dim k As Integer = 0
                    For j As Integer = 0 To .Rows.Count - 1
                        If .GetData(j, Col_CategoryID) = .GetData(_selRow, Col_CategoryID) Then
                            k = k + 1
                        End If
                    Next

                    If k = 2 Then
                        ValidateDataDictionary(.GetData(_selRow - 1, Col_CategoryName))
                        .Rows.Remove(_selRow)
                        .Rows.Remove(_selRow - 1)
                    Else
                        .Rows.Remove(_selRow)
                    End If

                End If

            End With
            blnModify = True
        Catch ex As Exception
            C1PatientRos.Rows.Remove(C1PatientRos.Row)
            'MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub AddBasicVoiceCommands()
        'Add Voice Commands

        'RemoveVoiceCommands()
        'AddVoiceCommand("Show Previous", "Show Previous ROS")
        'AddVoiceCommand("Show Current", "Todays ROS", "Current ROS")
        'AddVoiceCommand("Show Yesterday", "Yesterday's ROS", "Yesterday ROS")
        'AddVoiceCommand("Show LastWeek", "Last Week ROS")
        'AddVoiceCommand("Show Last Month", "Last Month ROS")
        'AddVoiceCommand("Show Older", "Older ROS")

        'AddVoiceCommand("New ROS", "Create New")
        'AddVoiceCommand("Save ROS", "Save it")
        'AddVoiceCommand("Finish ROS", "Finish it")
        'AddVoiceCommand("Close Window", "Close ROS", "Close it")
        'AddVoiceCommand("Previous Category Item", "Select Previous Category Item") 'Select Prev
        'AddVoiceCommand("Next Category Item", "Select Next Category Item") 'Select Next Category Item
        'AddVoiceCommand("Select Category Item", "Select Category Item from List") 'Select Category Item
        'AddVoiceCommand("Select Previous ROS Item", "Previous ROS item") 'Select Previous ROS Item
        'AddVoiceCommand("Select Next ROS Item", "Next ROS item") 'Select next ROS Item
        'AddVoiceCommand("Hide Previous", "Hide Previous ROS") 'Hide Previous ROS History
        'AddVoiceCommand("Previous ROS History", "Select Previous ROS History") 'Select Previous ROS 
        'AddVoiceCommand("Next ROS History", "Select Next ROS History")    'Select Next ROS
        'AddVoiceCommand("Delete ROS", "Delete Selected ROS") 'Delete ROS from Previous ROS History 
        'AddVoiceCommand("Modify ROS", "Modify Selected ROS") 'Modify ROS History from Previous ROS History 
        'AddVoiceCommand("Delete ROS Item", "Delete Selected ROS Item") 'Delete ROS History from Previous ROS History 

        voicecol.Clear()
        voicecol.Add("Save it")
        voicecol.Add("Close form")
        voicecol.Add("New form")
        voicecol.Add("Previous Item")
        voicecol.Add("Next Item")
        voicecol.Add("Select Item")

        ROSVoiceCol.Clear()
        ROSVoiceCol.Add("Show Previous")
        ROSVoiceCol.Add("Show Current")
        ROSVoiceCol.Add("Show Yesterday")
        ROSVoiceCol.Add("Show LastWeek")
        ROSVoiceCol.Add("Show LastMonth")
        ROSVoiceCol.Add("Show Older")
        ROSVoiceCol.Add("Previous ROS Item")
        ROSVoiceCol.Add("Next ROS Item")
        ROSVoiceCol.Add("Hide Previous")
        ROSVoiceCol.Add("Previous ROS")
        ROSVoiceCol.Add("Next ROS")
        ROSVoiceCol.Add("Delete ROS")
        ROSVoiceCol.Add("Modify ROS")
        ROSVoiceCol.Add("Delete ROS Item")
        ROSVoiceCol.Add("Save and Close it")
    End Sub
    'Public Sub PreviousROS(ByVal Trv As TreeView, ByVal ROS As enmPreviousROS)
    '    Dim objSender As Object
    '    Dim obje As EventArgs
    '    Select Case ROS
    '        Case enmPreviousROS.Current
    '            SearchNode(trvPrevROS, "Current")
    '        Case enmPreviousROS.Yesterday
    '            SearchNode(trvPrevROS, "YesterDay")
    '        Case enmPreviousROS.LastWeek
    '            SearchNode(trvPrevROS, "Last Week")
    '        Case enmPreviousROS.LastMonth
    '            SearchNode(trvPrevROS, "Last Month")
    '        Case enmPreviousROS.Older
    '            SearchNode(trvPrevROS, "Older")
    '    End Select
    '    If IsNothing(trvSearchNode) = False Then
    '        trvPrevROS.SelectedNode = trvSearchNode
    '        Call trvPrevROS_DoubleClick(objSender, obje)
    '    End If
    Public Sub PreviousROS(ByVal intnode As Integer)
        'Dim e As New System.Windows.Forms.TreeViewEventArgs(trvPrevROS.Nodes.Item(0).Nodes.Item(intnode), TreeViewAction.ByMouse)
        'Dim objSender As Object
        'Call trvPrevROS_AfterSelect(objSender, e)
        trvPrevROS.SelectedNode = trvPrevROS.Nodes.Item(0).Nodes.Item(intnode)
    End Sub
    Private Sub frmROS_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            '*****************code commented by supriya on 24/02/2006
            'change in logic for adding voicecommands
            'If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            '    'If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnOpenFromExam = False Then
            '    RemoveVoiceCommands()
            'End If
            '*****************code commented by supriya on 24/02/2006
            'change in logic for adding voicecommands
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub tblROS_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
        Try
            Select Case e.Button.Text
                Case "&New"
                    NewROS()
                Case "&Save"
                    SaveROS()
                Case "&Finish"
                    FinishROS()
                Case "Show", "Hide"
                    ShowHidePreviousROS(e.Button.Text)
                Case "&Close"
                    CloseROS()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub txtSearchCategory_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSearchCategory.Validating
        ''Try
        ''    If Trim(txtSearchCategory.Text) <> "" Then
        ''        If trvSource.GetNodeCount(False) > 0 Then
        ''            Dim mychildnode As myTreeNode
        ''            'child node collection

        ''            For Each mychildnode In trvSource.Nodes
        ''                Dim str As String
        ''                str = UCase(Trim(mychildnode.Text))
        ''                If Mid(str, 1, Len(Trim(txtSearchCategory.Text))) = UCase(Trim(txtSearchCategory.Text)) Then
        ''                    trvSource.SelectedNode = mychildnode
        ''                    Exit Sub
        ''                End If
        ''            Next
        ''        End If
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub
    Private Sub txtSearchCategory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchCategory.KeyPress
        If trvSource.GetNodeCount(False) > 0 Then
            If (e.KeyChar = ChrW(13)) Then
                trvSource.Select()
                'Else
                '    trvSource.SelectedNode = trvSource.Nodes.Item(0)
            End If
        End If
    End Sub
    Private Sub txtsearchROS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtsearchROS.Validating
        Try
            If cmbsearchROS.Text = "ROS Date" Then
                If IsDate(txtsearchROS.Text) Then
                    Dim mynode As myTreeNode
                    'Root node collection
                    For Each mynode In trvPrevROS.Nodes.Item(0).Nodes
                        Dim mychildnode As myTreeNode
                        'child node collection
                        For Each mychildnode In mynode.Nodes
                            If CType(mychildnode.Text, DateTime).Date = Trim(txtsearchROS.Text) Then
                                mynode.Parent.ExpandAll()
                                trvPrevROS.SelectedNode = mychildnode
                                Exit Sub
                            End If
                        Next
                    Next
                End If
            Else
                If Trim(txtsearchROS.Text) <> "" Then
                    Dim mynode As myTreeNode
                    'Root node collection
                    'mynode is current/yesterday/last week/last month
                    ''20121016::Bug No.38944::V7010::If Condition added
                    If trvPrevROS.Nodes.Count > 0 Then


                        For Each mynode In trvPrevROS.Nodes.Item(0).Nodes

                            Dim mychildnode As myTreeNode
                            'mychildnode is prescriptiondate annd prescription
                            For Each mychildnode In mynode.Nodes
                                Dim myROSNode As TreeNode
                                For Each myROSNode In mychildnode.Nodes
                                    If Trim(myROSNode.Text) <> "" Then
                                        Dim arrstring() As String
                                        arrstring = Split(myROSNode.Text, ":")
                                        'If Len(arrstring) > 0 Then
                                        Dim str As String
                                        str = UCase(CType(arrstring.GetValue(0), String))
                                        If Mid(str, 1, Len(UCase(Trim(txtsearchROS.Text)))) = UCase(Trim(txtsearchROS.Text)) Then
                                            mynode.Parent.ExpandAll()
                                            trvPrevROS.SelectedNode = myROSNode
                                            Exit Sub
                                        End If
                                        'End If
                                    End If
                                Next
                            Next
                        Next
                    End If

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtsearchROS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchROS.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvPrevROS.Select()
        Else
            trvPrevROS.SelectedNode = trvPrevROS.Nodes.Item(0)
        End If
    End Sub

    Private Sub mnuMakeCurrent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMakeCurrent.Click
        Try

            If trvPrevROS.SelectedNode.Text = "Patient ROS" Then
                '    If trvPrevROS.SelectedNode.Parent.Text <> "Patient ROS" Then
                Exit Sub
            End If

            '
            If IsNothing(trvPrevROS.SelectedNode.Parent) = True Then
                '    If trvPrevROS.SelectedNode.Parent.Text <> "Patient ROS" Then
                Exit Sub
            End If

            If trvPrevROS.SelectedNode.Text = "Current" Then
                Exit Sub
            ElseIf trvPrevROS.SelectedNode.Text = "Yesterday" Then
                Exit Sub
            ElseIf trvPrevROS.SelectedNode.Text = "Last Week" Then
                Exit Sub
            ElseIf trvPrevROS.SelectedNode.Text = "Last Month" Then
                Exit Sub
            ElseIf trvPrevROS.SelectedNode.Text = "Older" Then
                Exit Sub
            End If

            ' '' ----------- Record Level Locking ------------
            ' '' Sanjog - 20110607
            If gblnRecordLocking = True Then
                Dim DateNode As myTreeNode
                DateNode = CType(trvPrevROS.SelectedNode, myTreeNode)
                Dim mydt As mytable
                mydt = Scan_n_Lock_Transaction(TrnType.PatientROS, m_PatientID, DateNode.Key, DateNode.Text)
                If (IsNothing(mydt) = False) Then


                    If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                        MessageBox.Show("This patient ROS is being modified by " & mydt.Code & " on " & mydt.Description & ". You can not make as current it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        If (IsNothing(mydt) = False) Then
                            mydt.Dispose()
                            mydt = Nothing
                        End If
                        Exit Sub
                    End If
                    If (IsNothing(mydt) = False) Then
                        mydt.Dispose()
                        mydt = Nothing
                    End If
                End If

            End If
            '''' ---------- Record Level Locking -----------------

            If trvPrevROS.SelectedNode.Parent.Parent.Text = "Patient ROS" Then

                If MessageBox.Show("Are you sure, you want to make this ROS as current ROS?", "Patient ROS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    'Dim i As Integer
                    blnModify = True
                    Dim blnC1PatientFull As Boolean = False
                    ''''' to chech where trvTarget contains ROS records
                    '<Start> Pramod 20072605
                    'For i = 0 To trvTarget.GetNodeCount(False) - 1
                    '    If trvTarget.Nodes.Item(i).GetNodeCount(False) > 0 Then
                    '        blnC1PatientFull = True  ''''''trvTarget contains ROS records
                    '        Exit For
                    '    End If
                    'Next
                    '<END> Pramod 20072605
                    ''''' to chech where C1PatientROS contains ROS records
                    If C1PatientRos.Rows.Count > 1 Then
                        blnC1PatientFull = True  ''''''C1PatientROS contains ROS records
                    End If

                    If blnC1PatientFull = True Then
                        ''''if trvTarget already contains record then he ask for whether to save it or Not 
                        ''''if yes then firstly he save it & open selected record for making changes
                        '''' otherwise it directly open selected record for making changes
                        If MessageBox.Show("Do you want to save currently opened ROS?", "Patient ROS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then

                            SaveROS()
                            MakeCurrentROS()
                        Else
                            ''''directly open selected record for making changes
                            MakeCurrentROS()
                        End If
                    Else
                        ''''directly open selected record for making changes
                        MakeCurrentROS()
                    End If
                End If
            End If

            trvPrevROS.SelectedNode = trvPrevROS.SelectedNode.Parent
            trvPrevROS.Select()

            'trvTarget.Refresh()
            'trvTarget.ExpandAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MakeCurrentROS()
        'to move ROS Details of that particular Date in Target TreeView  for Edit
        'objclsPatientROS.GenerateVisitID()
        '<Start> Pramod 20072605
        'Dim DateNode As New myTreeNode
        'DateNode = CType(trvPrevROS.SelectedNode, myTreeNode)

        'Dim i As Integer
        '''''DateNode = sender.selectednode.nodes
        'For i = 0 To trvPrevROS.SelectedNode.GetNodeCount(False) - 1
        '    'lblVisitDate.Text = DateNode.Text
        '    lblVisitDate.Tag = DateNode.Key()

        '    Dim arr() As String 'Srting Array
        '    arr = Split(Trim(DateNode.Nodes.Item(i).Text), ":")
        '    Dim targetParentNode As TreeNode

        '    Dim j As Integer
        '    For j = 0 To trvTarget.GetNodeCount(False) - 1
        '        'trvTarget.Nodes.Item(j).Nodes.Clear()

        '        If Trim(arr.GetValue(0).ToString) = Trim(trvTarget.Nodes.Item(j).Text) Then
        '            'Dim arrChild() As String
        '            'arrChild = Split(Trim(arr.GetValue(1).ToString), ":-")
        '            'arr.GetValue(2)

        '            lblVisitDate.Text = Now.Date

        '            If arr.Length.ToString = 3 Then
        '                Dim nde As New TreeNode
        '                nde.Text = Trim(arr.GetValue(1).ToString & ":" & arr.GetValue(2).ToString)
        '                nde.Tag = Trim(arr.GetValue(1).ToString)
        '                nde.ForeColor = trvTarget.Nodes(j).ForeColor
        '                trvTarget.Nodes.Item(j).Nodes.Add(nde)
        '                Exit For
        '            ElseIf arr.Length.ToString = 2 Then
        '                Dim nde As New TreeNode
        '                nde.Text = Trim(arr.GetValue(1).ToString)
        '                nde.Tag = Trim(arr.GetValue(1).ToString)
        '                nde.ForeColor = trvTarget.Nodes(j).ForeColor
        '                trvTarget.Nodes.Item(j).Nodes.Add(nde)
        '                Exit For
        '            End If
        '        End If
        '    Next
        'Next
        '<end> Pramod 20072605
        Dim i As Integer
        If IsNothing(trvPrevROS.SelectedNode.Parent) = False Then
            If trvPrevROS.SelectedNode.GetNodeCount(False) > 0 Then
                C1PatientRos.Rows.Count = 1
            End If
        End If


        'Dim arrSplit() As String

        C1PatientRos.Rows.Count = 1
        Dim DateNode As myTreeNode
        'C1PatientRos.BeginInit()
        ' Dim DateNode As myTreeNode
        ' Dim i As Integer
        'DateNode = sender.selectednode.nodes
        DateNode = CType(trvPrevROS.SelectedNode, myTreeNode)
        For i = 0 To trvPrevROS.SelectedNode.GetNodeCount(False) - 1
            blnModify = True
            ' DateNode = trvPrevROS.SelectedNode
            Dim strCategoryID As String = String.Empty
            Dim strCategory As String = String.Empty
            Dim strItem As String = String.Empty
            Dim strComment As String = String.Empty
            Dim strSource As String = String.Empty
            Dim nPatFormID As Int64 = 0
            Dim dtTransactionDate As String = String.Empty
            Dim ROSRow As DataRow = Nothing

            Dim arr() As String 'Srting Array
            Dim arrChild() As String
            arr = Split(Trim(DateNode.Nodes.Item(i).Text), ":", 2)
            ROSRow = DateNode.Nodes.Item(i).Tag

            strCategoryID = CStr(arr.GetValue(0)).Trim
            strCategory = CStr(arr.GetValue(0)).Trim

            If arr.Length = 2 Then
                arrChild = Split(Trim(arr.GetValue(1).ToString), ":-")
                If arrChild.Length = 1 Then
                    strItem = CStr(arrChild.GetValue(0)).Trim
                    strComment = ""
                Else
                    strItem = CStr(arrChild.GetValue(0)).Trim
                    strComment = CStr(arrChild.GetValue(1)).Trim
                End If
            End If
            'strItem = ""
            'strComment = ""
            ''Call funtion for fill flexgrid on treeviewPreviewHistory doubleclick
            blnModify = True

            strSource = ROSRow("sROSSource")
            nPatFormID = ROSRow("nPatientFormID")
            dtTransactionDate = ROSRow("dtTransactionDate")
            Call FillGridTrv(strCategoryID, strCategory, strItem, strComment, strSource, nPatFormID, dtTransactionDate)
        Next
        'Dim dt As DataTable
        'dt = objclsPatientROS.SelectNarration(lblVisitDate.Tag, lblPatientCode.Tag)

        '' Word Object
        'Dim mstream As ADODB.Stream
        'Dim strFileName As String
        'mstream = New ADODB.Stream
        'mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
        ''        wdNarration.Close()
        'mstream.Open()
        'If dt.Rows.Count > 0 Then
        '    mstream.Write(dt.Rows(0)(0))
        '    strFileName = Application.StartupPath & "\Temp\Temp5.txt"
        '    mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
        '    'wdNarration.Open(strFileName)
        '    'wdNarration.Text = mstream.ReadText
        '    wdNarration.LoadFile(strFileName)

        '    mstream.Close()
        '    'SetWordObjectEntry()
        '    'oCurDoc = wdNarration.ActiveDocument

        '    If wdNarration.TextLength > 0 Then
        '        pnlWordComp.Visible = True
        '    Else
        '        pnlWordComp.Visible = False
        '    End If

        'Else
        '    'wdNarration.CreateNew("Word.Document")
        '    pnlWordComp.Visible = False
        'End If

        lblVisitDate.Tag = 0
    End Sub

    Public Sub mnuDeleteROS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteROS.Click
        Try
            ''If IsNothing(trvSource.Parent) Then
            ''    Exit Sub
            ''End If

            If trvPrevROS.SelectedNode.Text = "Patient ROS" Then
                '    If trvPrevROS.SelectedNode.Parent.Text <> "Patient ROS" Then
                Exit Sub
            End If

            '
            If IsNothing(trvPrevROS.SelectedNode.Parent) = True Then
                '    If trvPrevROS.SelectedNode.Parent.Text <> "Patient ROS" Then
                Exit Sub
            End If

            If trvPrevROS.SelectedNode.Text = "Current" Then
                Exit Sub
            ElseIf trvPrevROS.SelectedNode.Text = "Yesterday" Then
                Exit Sub
            ElseIf trvPrevROS.SelectedNode.Text = "Last Week" Then
                Exit Sub
            ElseIf trvPrevROS.SelectedNode.Text = "Last Month" Then
                Exit Sub
            ElseIf trvPrevROS.SelectedNode.Text = "Older" Then
                Exit Sub
            End If

            If trvPrevROS.SelectedNode.Parent.Parent.Text = "Patient ROS" Then
                Dim DateNode As myTreeNode
                DateNode = CType(trvPrevROS.SelectedNode, myTreeNode)

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070723
                If gblnRecordLocking = True Then
                    Dim mydt As mytable
                    'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    'mydt = Scan_n_Lock_Transaction(TrnType.PatientROS, gnPatientID, DateNode.Key, DateNode.Text)
                    mydt = Scan_n_Lock_Transaction(TrnType.PatientROS, m_PatientID, DateNode.Key, DateNode.Text)
                    'end modification
                    If (IsNothing(mydt) = False) Then


                        If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                            MessageBox.Show("This patient ROS is being modified by " & mydt.Code & " on " & mydt.Description & ". You can not delete it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            If (IsNothing(mydt) = False) Then
                                mydt.Dispose()
                                mydt = Nothing
                            End If
                            Exit Sub
                        End If
                        If (IsNothing(mydt) = False) Then
                            mydt.Dispose()
                            mydt = Nothing
                        End If
                    End If

                End If
                '''' <><><> Record Level Locking <><><><> 

                If MessageBox.Show("Are you sure, you want to delete this ROS", "Patient ROS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    objclsPatientROS.DeleteROS(DateNode.Key, lblPatientCode.Tag)
                    objclsPatientROS.DeleteNarration(DateNode.Key, lblPatientCode.Tag)



                    If DateNode.Key = lblVisitDate.Tag Then
                        'Dim i As Integer
                        'For i = 0 To trvTarget.GetNodeCount(False) - 1
                        '    Dim CategoryNode As New TreeNode
                        '    CategoryNode = trvTarget.Nodes.Item(i)
                        '    CategoryNode.Nodes.Clear()

                        '    'Dim j As Integer
                        '    'For j = 0 To CategoryNode.GetNodeCount(False) - 1

                        '    'Next
                        'Next
                        NewROS()
                    End If

                    ''20090821 for refreshing tree view of Patient ROS after deleting ROS
                    'RefreshROSHistory()
                    trvPrevROS.Nodes.Clear()


                    'Shubhangi
                    trvPrevROS.Select()
                    pnlPrevROS.Visible = False
                    'Call tblShow_Click(sender, e)

                    If pnlPrevROS.Visible = True Then
                        ShowHidePreviousROS("Hide")
                    Else
                        ShowHidePreviousROS("Show")
                    End If


                End If
            End If
            'Commented by Shubhangi

            'trvPrevROS.SelectedNode = trvPrevROS.SelectedNode.Parent
            'trvPrevROS.Select()

            'trvTarget.Refresh()
            'trvTarget.ExpandAll()

            'trvPrevROS.Refresh()
            ' RefreshPrevROS(trvPrevROS.SelectedNode.Parent)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshROSHistory()
        'If objclsPatientROS.CheckRecordCount("C") Then
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(0).ImageIndex = 5
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 5
        'Else
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(0).ImageIndex = 6
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 6
        'End If
        'If objclsPatientROS.CheckRecordCount("Y") Then
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(1).ImageIndex = 7
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 7
        'Else
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(1).ImageIndex = 8
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 8
        'End If
        'If objclsPatientROS.CheckRecordCount("W") Then
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(2).ImageIndex = 9
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 9
        'Else
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(2).ImageIndex = 10
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 10
        'End If
        'If objclsPatientROS.CheckRecordCount("M") Then
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(3).ImageIndex = 11
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 11
        'Else
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(3).ImageIndex = 12
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 12
        'End If
        'If objclsPatientROS.CheckRecordCount("O") Then
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(4).ImageIndex = 13
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(4).SelectedImageIndex = 13
        'Else
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(4).ImageIndex = 14
        '    trvPrevROS.Nodes.Item(0).Nodes.Item(4).SelectedImageIndex = 14
        'End If
        Dim dt As New DataTable
        'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'dt = objclsPatientROS.GetPrevROS("A", gnPatientID, Now)
        dt = objclsPatientROS.GetPrevROS("A", m_PatientID, Now)
        'end modification
        Dim oCurrentNode As myTreeNode = trvPrevROS.Nodes(0).Nodes(0)
        Dim oYesterdayNode As myTreeNode = trvPrevROS.Nodes(0).Nodes.Item(1)
        Dim oLastWeekNode As myTreeNode = trvPrevROS.Nodes(0).Nodes(2)
        Dim oLastMonthNode As myTreeNode = trvPrevROS.Nodes(0).Nodes(3)
        Dim oOlderNode As myTreeNode = trvPrevROS.Nodes(0).Nodes(4)

        Dim IscurrentHistory As Boolean = False
        Dim IsYesterdayHistory As Boolean = False
        Dim IsLastWeekHistory As Boolean = False
        Dim IsLastMonthHistory As Boolean = False
        Dim IsOlderHistory As Boolean = False

        If Not IsNothing(dt) Then

            For i As Integer = 0 To dt.Rows.Count - 1
                If GetDateCategory(Format(dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy")) = DateCategory.Today Then
                    Dim objTreeNode As New myTreeNode(Format(dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy"), dt.Rows(i)("nVisitID"))
                    objTreeNode.ForeColor = Color.Blue
                    objTreeNode.ImageIndex = 15
                    objTreeNode.SelectedImageIndex = 15
                    oCurrentNode.Nodes.Add(objTreeNode)
                    oCurrentNode.Key = dt.Rows(i)("nVisitID")
                    IscurrentHistory = True
                ElseIf GetDateCategory(Format(dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy")) = DateCategory.Yesterday Then
                    Dim objTreeNode As New myTreeNode(Format(dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy"), dt.Rows(i)("nVisitID"))
                    objTreeNode.ForeColor = Color.Blue
                    objTreeNode.ImageIndex = 15
                    objTreeNode.SelectedImageIndex = 15
                    oYesterdayNode.Nodes.Add(objTreeNode)
                    oYesterdayNode.Key = dt.Rows(i)("nVisitID")
                    IsYesterdayHistory = True
                ElseIf GetDateCategory(Format(dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy")) = DateCategory.LastWeek Then
                    Dim objTreeNode As New myTreeNode(Format(dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy"), dt.Rows(i)("nVisitID"))
                    objTreeNode.ForeColor = Color.Blue
                    objTreeNode.ImageIndex = 15
                    objTreeNode.SelectedImageIndex = 15
                    oLastWeekNode.Nodes.Add(objTreeNode)
                    oLastMonthNode.Key = dt.Rows(i)("nVisitID")
                    IsLastWeekHistory = True
                ElseIf GetDateCategory(Format(dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy")) = DateCategory.LastMonth Then
                    Dim objTreeNode As New myTreeNode(Format(dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy"), dt.Rows(i)("nVisitID"))
                    objTreeNode.ForeColor = Color.Blue
                    objTreeNode.ImageIndex = 15
                    objTreeNode.SelectedImageIndex = 15
                    oLastMonthNode.Nodes.Add(objTreeNode)
                    oLastMonthNode.Key = dt.Rows(i)("nVisitID")
                    IsLastMonthHistory = True
                ElseIf GetDateCategory(Format(dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy")) = DateCategory.Older Then
                    Dim objTreeNode As New myTreeNode(Format(dt.Rows(i)("dtVisitDate"), "MM/dd/yyyy"), dt.Rows(i)("nVisitID"))
                    objTreeNode.ForeColor = Color.Blue
                    objTreeNode.ImageIndex = 15
                    objTreeNode.SelectedImageIndex = 15
                    oOlderNode.Nodes.Add(objTreeNode)
                    oOlderNode.Key = dt.Rows(i)("nVisitID")
                    IsOlderHistory = True
                End If
            Next
        End If

        If IscurrentHistory Then
            trvPrevROS.Nodes.Item(0).Nodes.Item(0).ImageIndex = 5
            trvPrevROS.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 5
        Else
            trvPrevROS.Nodes.Item(0).Nodes.Item(0).ImageIndex = 6
            trvPrevROS.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 6
        End If
        If IsYesterdayHistory Then
            trvPrevROS.Nodes.Item(0).Nodes.Item(1).ImageIndex = 7
            trvPrevROS.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 7
        Else
            trvPrevROS.Nodes.Item(0).Nodes.Item(1).ImageIndex = 8
            trvPrevROS.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 8
        End If
        If IsLastWeekHistory Then
            trvPrevROS.Nodes.Item(0).Nodes.Item(2).ImageIndex = 9
            trvPrevROS.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 9
        Else
            trvPrevROS.Nodes.Item(0).Nodes.Item(2).ImageIndex = 10
            trvPrevROS.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 10
        End If
        If IsLastMonthHistory Then
            trvPrevROS.Nodes.Item(0).Nodes.Item(3).ImageIndex = 11
            trvPrevROS.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 11
        Else
            trvPrevROS.Nodes.Item(0).Nodes.Item(3).ImageIndex = 12
            trvPrevROS.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 12
        End If
        If IsOlderHistory Then
            trvPrevROS.Nodes.Item(0).Nodes.Item(4).ImageIndex = 13
            trvPrevROS.Nodes.Item(0).Nodes.Item(4).SelectedImageIndex = 13
        Else
            trvPrevROS.Nodes.Item(0).Nodes.Item(4).ImageIndex = 14
            trvPrevROS.Nodes.Item(0).Nodes.Item(4).SelectedImageIndex = 14
        End If
    End Sub
    Private Sub RefreshPrevROS(ByVal objSender As Object)
        'Dim objSender As Object
        Dim obje As EventArgs = Nothing
        trvPrevROS_AfterSelect(objSender, obje)
    End Sub

    'Private Sub SetWordObjectEntry()
    '    'wdNarration.CreateNew("Word.Document")
    '    'wdNewExam.Visible = False
    '    wdNarration.Menubar = False
    '    wdNarration.Toolbars = True
    '    'wdNewExam.Caption = cmbExam.Text
    '    oCurDoc = wdNarration.ActiveDocument

    '    oCurDoc.ActiveWindow.Application.CommandBars("Standard").Enabled = False
    '    oCurDoc.ActiveWindow.Application.CommandBars("Standard").Visible = False

    '    oCurDoc.ActiveWindow.Application.CommandBars("Formatting").Enabled = True
    '    oCurDoc.ActiveWindow.Application.CommandBars("Formatting").Visible = True

    '    oCurDoc.ActiveWindow.Application.CommandBars("Web").Enabled = False
    '    oCurDoc.ActiveWindow.Application.CommandBars("Web").Visible = False

    '    oCurDoc.ActiveWindow.Application.CommandBars("Forms").Enabled = False
    '    oCurDoc.ActiveWindow.Application.CommandBars("Forms").Visible = False

    '    oCurDoc.ActiveWindow.Application.CommandBars("Control Toolbox").Enabled = False
    '    oCurDoc.ActiveWindow.Application.CommandBars("Control Toolbox").Visible = False

    '    oCurDoc.ActiveWindow.Application.CommandBars("Database").Enabled = False
    '    oCurDoc.ActiveWindow.Application.CommandBars("Database").Visible = False

    '    'oCurDoc.ActiveWindow.Application.CommandBars("E-mail").Enabled = False
    '    'oCurDoc.ActiveWindow.Application.CommandBars("E-mail").Visible = False

    '    oCurDoc.ActiveWindow.Application.CommandBars("Frames").Enabled = False
    '    oCurDoc.ActiveWindow.Application.CommandBars("Frames").Visible = False

    '    oCurDoc.ActiveWindow.Application.CommandBars("Mail Merge").Enabled = False
    '    oCurDoc.ActiveWindow.Application.CommandBars("Mail Merge").Visible = False

    '    oCurDoc.ActiveWindow.Application.CommandBars("Outlining").Enabled = False
    '    oCurDoc.ActiveWindow.Application.CommandBars("Outlining").Visible = False

    '    oCurDoc.ActiveWindow.Application.CommandBars("Visual Basic").Enabled = False
    '    oCurDoc.ActiveWindow.Application.CommandBars("Visual Basic").Visible = False

    '    oCurDoc.ActiveWindow.Application.CommandBars("Web Tools").Enabled = False
    '    oCurDoc.ActiveWindow.Application.CommandBars("Web Tools").Visible = False

    '    oCurDoc.ActiveWindow.Application.CommandBars("WordArt").Enabled = False
    '    oCurDoc.ActiveWindow.Application.CommandBars("WordArt").Visible = False
    '    ''oCurDoc1.ActiveWindow.Application.ActiveDocument.ActiveWindow.ActivePane.View.FullScreen = True
    '    'oCurDoc1.ActiveWindow.Application.CommandBars("Form Fields").Enabled = False
    '    'oCurDoc1.ActiveWindow.Application.CommandBars("Form Fields").Visible = False

    'End Sub

    'Private Sub wdNarration_OnFileCommand(ByVal sender As System.Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnFileCommandEvent)

    'End Sub

    'Private Sub wdNarration_OnDocumentOpened(ByVal sender As System.Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent)
    '    blnDoc = True
    'End Sub

    Private Sub txtSearchCategory_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchCategory.TextChanged
        Try


            'sarika 27th sept 07
            'implement the instring search here

            If txtSearchCategory.Tag <> Trim(txtSearchCategory.Text) Then

                AddSrSource(Trim(txtSearchCategory.Text), dtSource)


                txtSearchCategory.Tag = Trim(txtSearchCategory.Text)
                txtSearchCategory.Focus()
            End If

            Exit Sub
            '-------------------------

            'SLR : 8/5/2014: Code review: What is the purpose of following code : i commented ?
            'If Trim(txtSearchCategory.Text) <> "" Then
            '    If trvSource.GetNodeCount(False) > 0 Then
            '        Dim mychildnode As myTreeNode
            '        'child node collection
            '        Dim str As String
            '        For Each mychildnode In trvSource.Nodes
            '            str = String.Empty
            '            str = UCase(Trim(mychildnode.Text))
            '            If Mid(str, 1, Len(Trim(txtSearchCategory.Text))) = UCase(Trim(txtSearchCategory.Text)) Then
            '                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
            '                'If Not IsNothing(trvSource.SelectedNode) Then
            '                '    If Not IsNothing(trvSource.SelectedNode.LastNode) Then
            '                '        trvSource.SelectedNode = trvSource.SelectedNode.LastNode
            '                '    End If
            '                'End If
            '                '*************

            '                trvSource.SelectedNode = trvSource.Nodes(trvSource.GetNodeCount(False) - 1)
            '                trvSource.SelectedNode = mychildnode
            '                txtSearchCategory.Focus()
            '                Exit Sub
            '            End If
            '        Next

            '        '' 20070921 - InString Search Implimented 
            '        For Each mychildnode In trvSource.Nodes
            '            str = String.Empty
            '            str = UCase(Trim(mychildnode.Text))
            '            If InStr(str, UCase(Trim(txtSearchCategory.Text)), CompareMethod.Text) > 0 Then
            '                '' If Sting Found 
            '                trvSource.SelectedNode = trvSource.Nodes(trvSource.GetNodeCount(False) - 1)
            '                trvSource.SelectedNode = mychildnode
            '                txtSearchCategory.Focus()
            '                Exit Sub
            '            End If
            '        Next

            '    End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    'sarika 26th sept 07
    Public Sub AddSrSource(ByVal strsearch As String, ByVal dt As DataTable)
        Try
            'Dim i As Integer
            Dim tdt As DataTable

            Dim dv As New DataView(dt)

            Dim strsearchCategoryItem As String
            If Trim(strsearch) <> "" Then
                strsearchCategoryItem = Replace(strsearch, "'", "''")
                ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                strsearchCategoryItem = Replace(strsearchCategoryItem, "[", "") & ""
                strsearchCategoryItem = mdlGeneral.ReplaceSpecialCharacters(strsearchCategoryItem)
            Else
                strsearchCategoryItem = ""
            End If

            dv.RowFilter = dt.Columns(1).ColumnName & " Like '%" & strsearchCategoryItem & "%'"

            tdt = New DataTable
            tdt = dv.ToTable

            'add the nodes to treenode
            trvSource.Hide()


            trvSource.BeginUpdate()
            trvSource.Nodes.Clear()
            For Each ROSRow As DataRow In tdt.Rows
                'ROSNode = tvwCustom.Nodes.Add(CStr(ROSRow("sDescription")))
                Dim objTreeNode As New myTreeNode(ROSRow.Item(1), ROSRow.Item(0))
                trvSource.Nodes.Add(objTreeNode)

            Next ROSRow

            'trICD9.ExpandAll()
            trvSource.Show()
            ' trvSource.Nodes.Item(0).Expand()
            'trICD9.SelectedNode = trICD9.Nodes.Item(0)

            'trICD9.Select()
            trvSource.EndUpdate()

            ' trvSource.SelectedNode = trvSource.Nodes.Item(0)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '---------------------------------------------------






    'Private Sub pnlWordComp_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlWordComp.VisibleChanged
    '    Try
    '        If pnlWordComp.Visible = True Then
    '            tblshowNarrative.Pushed = True
    '        Else
    '            tblshowNarrative.Pushed = False
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub trvPrevROS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvPrevROS.MouseDown
        Try
            If CType(sender, TreeView) Is trvPrevROS Then
                If e.Button = MouseButtons.Right Then

                    Dim trvNode As TreeNode
                    trvNode = trvPrevROS.GetNodeAt(e.X, e.Y)
                    If IsNothing(trvNode) = False Then
                        trvPrevROS.SelectedNode = trvNode
                    End If

                    If Not trvPrevROS.SelectedNode Is trvPrevROS.Nodes.Item(0) Then
                        If Not trvPrevROS.SelectedNode.Parent Is trvPrevROS.Nodes.Item(0) Then
                            If trvPrevROS.SelectedNode.GetNodeCount(False) > 0 Then
                                'Try
                                '    If (IsNothing(trvPrevROS.ContextMenu) = False) Then
                                '        trvPrevROS.ContextMenu.Dispose()
                                '        trvPrevROS.ContextMenu = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvPrevROS.ContextMenu = ContextMenutrvPrevROS
                                ContextMenutrvPrevROS.GetContextMenu()
                            Else
                                'Try
                                '    If (IsNothing(trvPrevROS.ContextMenu) = False) Then
                                '        trvPrevROS.ContextMenu.Dispose()
                                '        trvPrevROS.ContextMenu = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvPrevROS.ContextMenu = Nothing
                            End If
                            'trvPrevROS.ContextMenu = ContextMenutrvPrevROS
                            'ContextMenutrvPrevROS.GetContextMenu()
                        Else
                            'Try
                            '    If (IsNothing(trvPrevROS.ContextMenu) = False) Then
                            '        trvPrevROS.ContextMenu.Dispose()
                            '        trvPrevROS.ContextMenu = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvPrevROS.ContextMenu = Nothing

                        End If
                    Else
                        'Try
                        '    If (IsNothing(trvPrevROS.ContextMenu) = False) Then
                        '        trvPrevROS.ContextMenu.Dispose()
                        '        trvPrevROS.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvPrevROS.ContextMenu = Nothing
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub trvTarget_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        trvTarget_DblClick()
    '        '''''Dim strNodeName As String
    '        '''''check if it open for root node or Child Node 
    '        'Dim blnAllergies As Boolean
    '        'If IsNothing(trvTarget.SelectedNode.Parent) = True Then
    '        '    Exit Sub
    '        '    ''''chech if it open for Allergies , so as to hide combo & checkbox 
    '        'ElseIf trvTarget.SelectedNode.Parent.Text = "Allergies" Then
    '        '    blnAllergies = True
    '        'End If
    '        'strNodeName = trvTarget.SelectedNode.Text
    '        'Dim strNotes As String

    '        'If InStr(trvTarget.SelectedNode.Text, ":-") > 0 Then
    '        '    strNotes = Trim(Mid(Trim(trvTarget.SelectedNode.Text), InStr(Trim(trvTarget.SelectedNode.Text), ":-", CompareMethod.Text) + 2, Len(Trim(trvTarget.SelectedNode.Text))))
    '        '    strNodeName = trvTarget.SelectedNode.Text.Substring(0, InStr(Trim(trvTarget.SelectedNode.Text), ":-", CompareMethod.Text) - 2)
    '        '    ''''strNode = Split(Node.Text, ":-")
    '        'Else
    '        '    strNotes = ""
    '        'End If

    '        'Dim objfrmpatComment As New frmPatROSComment(blnAllergies, strNotes)
    '        'objfrmpatComment.Text = trvTarget.SelectedNode.Parent.Text
    '        'objfrmpatComment.lblHeading.Text = strNodeName
    '        'objfrmpatComment.ShowDialog(Me)

    '        'If frmPatROSComment.blnNoteAdded = True Then
    '        '    strNodeName = strNodeName & " :- " & Note
    '        '    trvTarget.SelectedNode.Text = strNodeName
    '        '    strNodeName = ""
    '        '    frmPatROSComment.blnNoteAdded = False
    '        'End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    'Private Sub trvTarget_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Try
    '        If e.KeyChar = ChrW(13) Then
    '            trvTarget_DblClick()
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    '<Start> Pramod 20070525
    'Private Sub trvTarget_DblClick()
    '    'Dim strNodeName As String
    '    'check if it open for root node or Child Node 
    '    Dim blnAllergies As Boolean
    '    'If IsNothing(trvTarget.SelectedNode.Parent) = True Then
    '    '    Exit Sub
    '    'chech if it open for Allergies , so as to hide combo & checkbox 
    '    ElseIf trvTarget.SelectedNode.Parent.Text = "Allergies" Then
    '    blnAllergies = True
    '    End If
    '    'strNodeName = trvTarget.SelectedNode.Text
    '    Dim strNotes As String

    '    'If InStr(trvTarget.SelectedNode.Text, ":-") > 0 Then
    '    '    strNotes = Trim(Mid(Trim(trvTarget.SelectedNode.Text), InStr(Trim(trvTarget.SelectedNode.Text), ":-", CompareMethod.Text) + 2, Len(Trim(trvTarget.SelectedNode.Text))))
    '    '    strNodeName = trvTarget.SelectedNode.Text.Substring(0, InStr(Trim(trvTarget.SelectedNode.Text), ":-", CompareMethod.Text) - 1)
    '    '    'strNode = Split(Node.Text, ":-")
    '    'Else
    '    strNotes = ""
    '    End If

    '    Dim objfrmpatComment As New frmPatROSComment(blnAllergies, strNotes)
    '    ' objfrmpatComment.Text = trvTarget.SelectedNode.Parent.Text
    '    objfrmpatComment.lblHeading.Text = strNodeName
    '    objfrmpatComment.ShowDialog(Me)
    '    Note = objfrmpatComment.m_Notes

    '    If frmPatROSComment.blnNoteAdded = True Then
    '        If Len(Note) > 0 Then
    '            strNodeName = strNodeName & ":-" & Note
    '        End If
    '        'trvTarget.SelectedNode.Text = strNodeName
    '        strNodeName = ""
    '        frmPatROSComment.blnNoteAdded = False

    '    End If
    '    '''' to get Reset Search
    '    Call ResetSearch()
    'End Sub
    '<END> Pramod 20070525
    Private Sub txtsearchROS_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearchROS.TextChanged
        Try
            If cmbsearchROS.Text = "ROS Date" Then
                If IsDate(txtsearchROS.Text) Then
                    Dim mynode As myTreeNode
                    'Root node collection
                    For Each mynode In trvPrevROS.Nodes.Item(0).Nodes
                        Dim mychildnode As myTreeNode
                        'child node collection
                        For Each mychildnode In mynode.Nodes
                            If CType(mychildnode.Text, DateTime).Date = Trim(txtsearchROS.Text) Then
                                mynode.Parent.ExpandAll()
                                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                trvPrevROS.SelectedNode = trvPrevROS.SelectedNode.LastNode
                                '*************
                                trvPrevROS.SelectedNode = mychildnode
                                txtsearchROS.Focus()
                            End If
                        Next
                    Next
                End If
            Else
                If Trim(txtsearchROS.Text) <> "" Then
                    Dim mynode As myTreeNode
                    'Root node collection
                    'mynode is current/yesterday/last week/last month
                    For Each mynode In trvPrevROS.Nodes.Item(0).Nodes

                        Dim mychildnode As myTreeNode
                        'mychildnode is prescriptiondate annd prescription
                        For Each mychildnode In mynode.Nodes
                            Dim myROSNode As TreeNode
                            For Each myROSNode In mychildnode.Nodes
                                If Trim(myROSNode.Text) <> "" Then
                                    Dim arrstring() As String
                                    arrstring = Split(myROSNode.Text, ":")
                                    'If Len(arrstring) > 0 Then
                                    Dim str As String
                                    str = UCase(CType(arrstring.GetValue(0), String))
                                    If Mid(str, 1, Len(UCase(Trim(txtsearchROS.Text)))) = UCase(Trim(txtsearchROS.Text)) Then
                                        mynode.Parent.ExpandAll()
                                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                        trvPrevROS.SelectedNode = trvPrevROS.SelectedNode.LastNode
                                        '*************
                                        trvPrevROS.SelectedNode = myROSNode
                                        txtsearchROS.Focus()
                                    End If
                                    'End If
                                End If
                            Next
                        Next
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub trvSource_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvSource.KeyPress
        Try
            If e.KeyChar = ChrW(13) Then
                trvSource_DblClick()
                '''' to get Reset Search
                Call ResetSearch()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub trvPrevROS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvPrevROS.KeyPress
        Try
            If e.KeyChar = ChrW(13) Then
                trvPrevROS_DblClick()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '<START> Pramod 20072605
    'Private Sub trvTarget_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvTarget.MouseDown

    '    Dim trvNode As TreeNode
    '    trvNode = trvTarget.GetNodeAt(e.X, e.Y)
    '    If IsNothing(trvNode) = False Then
    '        trvTarget.SelectedNode = trvNode
    '    End If

    '    If e.Button = MouseButtons.Right Then
    '        If IsNothing(trvTarget.SelectedNode.Parent) = False Then
    '            trvTarget.ContextMenu = ContextMenuC1PatientROS
    '        Else
    '            trvTarget.ContextMenu = Nothing
    '        End If
    '    End If
    'End Sub
    '<END> Pramod 20072605
    Private Sub trvSource_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvSource.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim trvNode As TreeNode
            trvNode = trvSource.GetNodeAt(e.X, e.Y)
            If IsNothing(trvNode) = False Then
                trvSource.SelectedNode = trvNode
                If Not IsNothing(trvSource.SelectedNode) Then
                    ContextMenuC1PatientROS.MenuItems(0).Visible = False ''''Remove 
                    ContextMenuC1PatientROS.MenuItems(1).Visible = True  ''''Add
                    ContextMenuC1PatientROS.MenuItems(2).Visible = True  ''''Edit
                    'Try
                    '    If (IsNothing(trvSource.ContextMenu) = False) Then
                    '        trvSource.ContextMenu.Dispose()
                    '        trvSource.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvSource.ContextMenu = ContextMenuC1PatientROS
                Else
                    ContextMenuC1PatientROS.MenuItems(0).Visible = False ''''Remove 
                    ContextMenuC1PatientROS.MenuItems(1).Visible = True  ''''Add
                    ContextMenuC1PatientROS.MenuItems(2).Visible = False  ''''Edit
                    'Try
                    '    If (IsNothing(trvSource.ContextMenu) = False) Then
                    '        trvSource.ContextMenu.Dispose()
                    '        trvSource.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvSource.ContextMenu = ContextMenuC1PatientROS
                End If
            Else
                ContextMenuC1PatientROS.MenuItems(0).Visible = False ''''Remove 
                ContextMenuC1PatientROS.MenuItems(1).Visible = True  ''''Add
                ContextMenuC1PatientROS.MenuItems(2).Visible = False  ''''Edit
                'Try
                '    If (IsNothing(trvSource.ContextMenu) = False) Then
                '        trvSource.ContextMenu.Dispose()
                '        trvSource.ContextMenu = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                trvSource.ContextMenu = ContextMenuC1PatientROS
            End If
        Else
            'Try
            '    If (IsNothing(trvSource.ContextMenu) = False) Then
            '        trvSource.ContextMenu.Dispose()
            '        trvSource.ContextMenu = Nothing
            '    End If
            'Catch ex As Exception

            'End Try
            trvSource.ContextMenu = Nothing
        End If
    End Sub

    Public Sub SearchNode(ByVal Trv As TreeView, ByVal strText As String)
        Dim trvNde As TreeNode
        For Each trvNde In Trv.Nodes
            SearchNode(trvNde, strText)
        Next
    End Sub
    Private Sub SearchNode(ByVal rootNode As TreeNode, ByVal strText As String)
        If LCase(Trim(rootNode.Text)) = LCase(Trim(strText)) Then
            trvSearchNode = rootNode
            Exit Sub
        Else
            For Each childNode As TreeNode In rootNode.Nodes
                If LCase(Trim(childNode.Text)) = LCase(Trim(strText)) Then
                    trvSearchNode = childNode
                    Exit Sub
                End If
                SearchNode(childNode, strText)
            Next
        End If
    End Sub
    Public Sub InsertROSCategories(ByVal strString As String)
        Dim objSender As Object = Nothing
        Dim obje As EventArgs = Nothing
        SearchNode(trvSource, strString)
        If IsNothing(trvSearchNode) = False Then
            trvSource.SelectedNode = trvSearchNode
            Call trvSource_DoubleClick(objSender, obje)
        End If
    End Sub
    Private Sub frmPatientROS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnOpenFromExam = True Then
                myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.ROS) '' ("ROS")
                '*****************code commented by supriya on 24/02/2006
                'change in logic for adding voicecommands
                'If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                '    myCaller.AddPatientExamVoiceCommands()
                'End If
                '*****************code commented by supriya on 24/02/2006
                'change in logic for adding voicecommands
                blnOpenFromExam = False
            End If
            If Not IsNothing(myCaller1) Then
                CType(myCaller1, IWord).GetdataFromOtherForms(gloEMRWord.enumDocType.ROS)
            End If
            '' <><><> Unlock the Record <><><>
            '' Mahesh - 20070723
            If _blnRecordLock = False Then
                '' if the Locked by by the Current User & on Current Machine only

                'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                'UnLock_Transaction(TrnType.PatientROS, gnPatientID, m_VisitID, m_VisitDate)
                UnLock_Transaction(TrnType.PatientROS, m_PatientID, m_VisitID, m_VisitDate)
                'end modification
            End If
            '' <><><> Unlock the Record <><><>
            Try
                If (IsNothing(gloUC_PatientStrip1) = False) Then
                    gloUC_PatientStrip1.Dispose()
                    gloUC_PatientStrip1 = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                'Application.DoEvents()
                Me.Dispose()
            Catch exdispose As Exception

            End Try
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Close, "Patient ROS  closed", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Close, "Patient ROS  closed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Private Sub frmPatientROS_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed


    'End Sub
    Public Sub NavigateCategoryItem(ByVal blnkeyvalue As Boolean)
        If Not IsNothing(trvSource.SelectedNode) Then
            If blnkeyvalue Then
                'if root node
                If trvSource.SelectedNode Is trvSource.Nodes.Item(0) Then
                    trvSource.Select()
                    trvSource.SelectedNode = CType(trvSource.SelectedNode.NextNode, myTreeNode)
                    'if not last node
                ElseIf Not trvSource.SelectedNode Is trvSource.Nodes.Item(trvSource.GetNodeCount(False) - 1) Then
                    trvSource.Select()
                    trvSource.SelectedNode = CType(trvSource.SelectedNode.NextNode, myTreeNode)
                Else
                    trvSource.Select()
                End If
            Else
                'if not root node
                'if selected node is not first node then
                If Not trvSource.SelectedNode Is trvSource.Nodes.Item(0) Then
                    trvSource.Select()
                    trvSource.SelectedNode = CType(trvSource.SelectedNode.PrevNode, myTreeNode)
                    'Else
                    '    trvSource.SelectedNode = trvSource.Nodes.Item(0)
                Else
                    trvSource.Select()
                End If

            End If
        Else
            If trvSource.GetNodeCount(False) > 0 Then
                trvSource.SelectedNode = trvSource.Nodes.Item(0)
                trvSource.Select()
            End If
        End If
    End Sub
    'Private Sub SelectnextNode(ByVal intnode As Integer)
    '    Try
    '        Dim i As Integer
    '        'For i = intnode + 1 To trvTarget.GetNodeCount(False) - 1
    '        '    Dim myROSItemNode As TreeNode
    '        '    For Each myROSItemNode In trvTarget.Nodes.Item(i).Nodes
    '        '        trvTarget.SelectedNode = myROSItemNode
    '        '        Exit Sub
    '        '    Next
    '        'Next
    '        'trvTarget.SelectedNode = trvTarget.Nodes(i - 1)
    '    Catch ex As Exception
    '        'MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Private Sub SelectPreviousNode(ByVal intnode As Integer)
    '    Dim i As Integer
    '    Dim j As Integer
    '    For i = intnode - 1 To 0 Step -1
    '        Dim myROSItemNode As TreeNode
    '        'For j = trvTarget.Nodes.Item(i).GetNodeCount(False) - 1 To 0 Step -1
    '        '    trvTarget.SelectedNode = trvTarget.Nodes.Item(i).Nodes.Item(j)
    '        '    Exit Sub
    '        'Next
    '    Next
    'End Sub
    Public Sub NavigateROSitem(ByVal blnKeyValue As Boolean)
        '<Start> Pramod 20070525
        'If Not IsNothing(trvTarget.SelectedNode) Then
        '    If blnKeyValue Then
        '        'if selectednode is a root level node

        '        If checkifrootnode(trvTarget.SelectedNode) Then

        '            If trvTarget.SelectedNode.GetNodeCount(False) > 0 Then
        '                trvTarget.SelectedNode = trvTarget.SelectedNode.FirstNode
        '            Else
        '                SelectnextNode(trvTarget.SelectedNode.Index)
        '            End If
        '        Else
        '            'Code commented from 11/12/2005
        '            'check for last node at level 2
        '            If Not trvTarget.SelectedNode Is trvTarget.SelectedNode.Parent.Nodes(trvTarget.SelectedNode.Parent.GetNodeCount(False) - 1) Then
        '                trvTarget.SelectedNode = trvTarget.SelectedNode.NextNode
        '            Else
        '                'if last node then
        '                'If Not IsNothing(trvTarget.SelectedNode.Parent.NextNode) Then
        '                '    trvTarget.SelectedNode = trvTarget.SelectedNode.Parent.NextNode.FirstNode
        '                'Else
        '                '    trvTarget.Select()
        '                'End If
        '                SelectnextNode(trvTarget.SelectedNode.Parent.Index)
        '            End If

        '        End If
        '    Else
        '        'if selectednode is a child level node
        '        If Not checkifrootnode(trvTarget.SelectedNode) Then
        '            'check if there is a previous level one node
        '            If Not trvTarget.SelectedNode Is trvTarget.SelectedNode.Parent.FirstNode Then
        '                trvTarget.SelectedNode = trvTarget.SelectedNode.PrevNode
        '            Else

        '                'If Not IsNothing(trvTarget.SelectedNode.Parent.PrevNode) Then
        '                '    If Not IsNothing(trvTarget.SelectedNode.Parent.PrevNode.LastNode) Then
        '                '        trvTarget.SelectedNode = trvTarget.SelectedNode.Parent.PrevNode.LastNode
        '                '    Else
        '                '        trvTarget.Select()
        '                '    End If
        '                'Else
        '                '    trvTarget.Select()
        '                'End If
        '                SelectPreviousNode(trvTarget.SelectedNode.Parent.Index)
        '            End If
        '        Else
        '            If Not IsNothing(trvTarget.SelectedNode.PrevNode.LastNode) Then
        '                trvTarget.SelectedNode = trvTarget.SelectedNode.PrevNode.LastNode
        '            Else
        '                trvTarget.Select()
        '            End If
        '        End If
        '    End If
        'Else
        '    If trvTarget.GetNodeCount(False) > 0 Then
        '        trvTarget.SelectedNode = trvTarget.Nodes.Item(0)
        '        trvTarget.Select()
        '    End If
        'End If
        '<END> Pramod 20070525
    End Sub
    Public Sub NavigatePreviousROSHistory(ByVal blnKeyValue As Boolean)
        If Not IsNothing(trvPrevROS.SelectedNode) Then
            If blnKeyValue Then

                'if selectednode is rootnode
                If trvPrevROS.SelectedNode Is trvPrevROS.Nodes.Item(0) Then
                    trvPrevROS.SelectedNode = CType(trvPrevROS.Nodes.Item(0).Nodes.Item(0).FirstNode, myTreeNode)
                    trvPrevROS.Select()

                    'if selected node is current/yesterday/last week/last month/older
                ElseIf trvPrevROS.SelectedNode.Parent Is trvPrevROS.Nodes.Item(0) Then
                    trvPrevROS.SelectedNode = CType(trvPrevROS.SelectedNode.FirstNode, myTreeNode)
                    trvPrevROS.Select()

                    'if selected node is prescription history items node 
                    'ElseIf Not CType(trvPrevROS.SelectedNode, myTreeNode).Tag Is Nothing Then
                ElseIf trvPrevROS.SelectedNode.Parent.Parent Is trvPrevROS.Nodes(0) Then
                    If Not trvPrevROS.SelectedNode Is trvPrevROS.SelectedNode.Parent.LastNode Then
                        trvPrevROS.SelectedNode = CType(trvPrevROS.SelectedNode.NextNode, myTreeNode)
                        trvPrevROS.Select()
                    Else
                        'If Not IsNothing(trvPrevROS.SelectedNode.Parent.NextNode) Then
                        '    trvPrevROS.Select()
                        '    trvPrevROS.SelectedNode = CType(trvPrevROS.SelectedNode.Parent.NextNode.FirstNode, myTreeNode)
                        'Else
                        '    trvPrevROS.Select()
                        'End If

                        trvPrevROS.Select()
                    End If
                End If
            Else
                'if selectednode is not rootnode
                If Not trvPrevROS.SelectedNode Is trvPrevROS.Nodes.Item(0) Then
                    'if selected node is current/yesterday/last week/last month/older
                    If trvPrevROS.SelectedNode.Parent Is trvPrevROS.Nodes.Item(0) Then
                        trvPrevROS.SelectedNode = CType(trvPrevROS.SelectedNode.PrevNode.FirstNode, myTreeNode)
                        trvPrevROS.Select()
                        'if selected node is prescription history items node 
                    ElseIf Not trvPrevROS.SelectedNode.Parent.Parent Is trvPrevROS.Nodes(0) Then
                        If Not trvPrevROS.SelectedNode Is trvPrevROS.Nodes.Item(0).Nodes.Item(0).FirstNode Then
                            trvPrevROS.SelectedNode = CType(trvPrevROS.SelectedNode.PrevNode, myTreeNode)
                            trvPrevROS.Select()

                            'Else
                            '    If Not IsNothing(trvPrevROS.SelectedNode.Parent.PrevNode) Then
                            '        trvPrevROS.Select()
                            '        trvPrevROS.SelectedNode = trvPrevROS.SelectedNode.Parent.PrevNode.FirstNode()
                            '    Else
                            '        trvPrevROS.Select()
                            '    End If
                        Else
                            trvPrevROS.Select()
                        End If
                    End If
                End If
            End If
        Else
            If trvPrevROS.GetNodeCount(False) > 0 Then
                trvPrevROS.SelectedNode = trvPrevROS.Nodes.Item(0)
            End If
        End If
    End Sub
    Public Sub ActivateBasicVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds
        If VoiceCol.Count > 0 Then
            Dim objtblbtn As New ToolBarButton
            Dim objsender As Object = Nothing
            Select Case VoiceCol.Item(1)
                Case "Save it"
                    objtblbtn.Text = "&Save"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblROS_ButtonClick(objsender, objtbl)
                Case "Close form"
                    objtblbtn.Text = "&Close"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblROS_ButtonClick(objsender, objtbl)
                Case "New form"
                    objtblbtn.Text = "&New"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblROS_ButtonClick(objsender, objtbl)
                Case "Previous Item"
                    NavigateCategoryItem(False)
                Case "Next Item"
                    NavigateCategoryItem(True)
                Case "Select Item"
                    Dim objkeyex As New KeyPressEventArgs(ChrW(13))
                    trvSource_KeyPress(objsender, objkeyex)
            End Select
            objtblbtn.Dispose()
            objtblbtn = Nothing
        End If
    End Sub
    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds

        If VoiceCol.Count > 0 Then
            Dim objSender As Object = Nothing
            Dim obje As EventArgs = Nothing
            Dim objKeye As KeyEventArgs = Nothing
            Dim objtblbtn As New ToolBarButton

            Select Case VoiceCol.Item(1)
                Case "Show Previous"
                    objtblbtn.Text = "Show"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblROS_ButtonClick(objSender, objtbl)
                Case "Show Current"
                    PreviousROS(0)
                Case "Show Yesterday"
                    PreviousROS(1)
                Case "Show LastWeek"
                    PreviousROS(2)
                Case "Show LastMonth"
                    PreviousROS(3)
                Case "Show Older"
                    PreviousROS(4)
                Case "Previous ROS Item"
                    NavigateROSitem(False)
                Case "Next ROS Item"
                    NavigateROSitem(True)
                Case "Hide Previous"
                    objtblbtn.Text = "Hide"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblROS_ButtonClick(objSender, objtbl)
                Case "Previous ROS"
                    NavigatePreviousROSHistory(False)
                Case "Next ROS"
                    NavigatePreviousROSHistory(True)
                Case "Delete ROS"
                    mnuDeleteROS_Click(objSender, obje)
                Case "Modify ROS"
                    Dim objkeyex As New KeyPressEventArgs(ChrW(13))
                    trvPrevROS_KeyPress(objSender, objkeyex)
                Case "Delete ROS Item"
                    mnuRemove_Click(objSender, obje)
                Case "Save and Close it"
                    objtblbtn.Text = "&Finish"
                    Dim objtbl As New System.Windows.Forms.ToolBarButtonClickEventArgs(objtblbtn)
                    tblROS_ButtonClick(objSender, objtbl)
                Case Else
                    Dim objbtnSender As New Button
                    'objSender1 = eventSender
                    objbtnSender.Text = UCase(Trim(VoiceCol.Item(1)))
                    objBtn_Click(objbtnSender, obje)
            End Select
            objtblbtn.Dispose()
            objtblbtn = Nothing
        End If
    End Sub
    Public Sub AddVoiceCommands() Implements mdlgloVoice.gloVoice.AddVoiceCommands
        vVoiceMenu.Remove(1)
        vVoiceMenu.ListSetStrings("BasicVoiceCommands", voicecol)
        vVoiceMenu.Add(1, "<BasicVoiceCommands>", "", "")

        vVoiceMenu.Remove(2)
        vVoiceMenu.ListSetStrings("MyROS", ROSVoiceCol)
        vVoiceMenu.Add(2, "<MyROS>", "", "")
    End Sub

    Public Sub CustomMakechanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_MakeChangesEvent) Implements mdlgloVoice.gloVoice.CustomMakechanges
        txtSearchCategory.SelectionStart = e.start

        txtSearchCategory.SelectionLength = e.numChars

        txtSearchCategory.SelectedText = e.text

        txtSearchCategory.SelectionStart = e.selStart

        txtSearchCategory.SelectionLength = e.selNumChars
    End Sub

    Public Sub CustomGetchanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_GetChangesEvent) Implements mdlgloVoice.gloVoice.CustomGetchanges
        e.haveChanges = True
        e.text = txtSearchCategory.Text
        e.selStart = txtSearchCategory.SelectionStart
        e.selNumChars = txtSearchCategory.SelectionLength
        e.visibleStart = 0
        e.visibleNumChars = txtSearchCategory.Text
    End Sub

    Private Sub txtSearchCategory_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchCategory.Validated
        'Try
        '    If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
        '        Dim frm As MainMenu
        '        frm = Me.MdiParent
        '        frm.AxDgnDictCustom1.Active = False
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Public Sub SetGridStyle()
        gloC1FlexStyle.Style(C1PatientRos)

        With C1PatientRos
            'Dim i As Int16
            Dim _TotalWidth As Single = .Width - 5
            'Dim cStyle As CellStyle
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = Col_Count

            .Cols.Fixed = 0

            .AllowEditing = True

            .Cols(Col_CategoryID).Width = _TotalWidth * 0
            .SetData(0, Col_CategoryID, "Category ID")
            .Cols(Col_CategoryID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_CategoryName).Width = _TotalWidth * 0.39
            .SetData(0, Col_CategoryName, "Category")
            .Cols(Col_CategoryName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_CategoryName).AllowEditing = False

            .Cols(Col_ItemName).Width = _TotalWidth * 0.44
            .SetData(0, Col_ItemName, "Item")
            .Cols(Col_ItemName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_ItemName).AllowEditing = False

            .Cols(Col_Comments).Width = _TotalWidth * 0.53
            .SetData(0, Col_Comments, "Comments")
            .Cols(Col_Comments).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_Comments).AllowEditing = True


            .Cols(Col_Source).Width = _TotalWidth * 0.2
            .SetData(0, Col_Source, "Source")
            .Cols(Col_Source).AllowEditing = False
            .Cols(Col_Source).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_PatientFormID).Width = _TotalWidth * 0
            .SetData(0, Col_PatientFormID, "FormID")
            .Cols(Col_PatientFormID).AllowEditing = False
            .Cols(Col_PatientFormID).Visible = False
            .Cols(Col_PatientFormID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_DateEntered).Width = _TotalWidth * 0.25
            .SetData(0, Col_DateEntered, "Date Entered")
            .Cols(Col_DateEntered).AllowEditing = False
            .Cols(Col_DateEntered).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


        End With
    End Sub

    Public Sub FillGridTrv(ByVal strCategoryID As String, ByVal strCategory As String, ByVal strItem As String, ByVal strComment As String, ByVal strSource As String, ByVal nPatFormID As Int64, ByVal strDate As String)

        Dim j As Integer
        'Dim k As Integer
        With C1PatientRos
            Dim _Row As Integer = 0
            'Dim _tempID As Long
            For j = 1 To C1PatientRos.Rows.Count - 1
                If .GetData(j, Col_CategoryID) = strCategory Then
                    '' TO Insert the New Item At the END of the CAtegory
                    Try
                        If .GetData(j, Col_CategoryID) <> .GetData(j + 1, Col_CategoryID) Then
                            '''' If The Current Category ID Is Not Matchs with the thw Category Name  at Next ROW 
                            '' Then Add new Row at Just After the Current Row i.e At the END of the Category
                            .Rows.Insert(j + 1)
                            _Row = j + 1
                            Exit For
                        End If
                    Catch ex As Exception
                        '''' If The System Does Not Get the ROW At (i+1) Position then it Throws the Exception
                        '' i.e we ahve to add the Row at the End 
                        .Rows.Insert(j + 1)
                        _Row = j + 1
                        Exit For
                    End Try
                End If
            Next


            If _Row = 0 Then ''  Category Is Not exists
                .Rows.Add()
                _Row = .Rows.Count - 1
                '.SetData(_Row, Col_CategoryID, BtnTag)
                .SetData(_Row, Col_CategoryID, strCategoryID)
                .SetData(_Row, Col_CategoryName, strCategory)
                '.Rows(_Row).AllowEditing = False
                .Rows(_Row).AllowEditing = False
                .Rows.Insert(_Row + 1)
                _Row = _Row + 1
            End If

            ' .SetData(_Row, Col_CategoryID, BtnTag)
            .SetData(_Row, Col_CategoryID, strCategoryID)
            .SetData(_Row, Col_ItemName, strItem)
            .SetData(_Row, Col_Comments, strComment)
            .SetData(_Row, Col_Source, strSource)
            .SetData(_Row, Col_PatientFormID, nPatFormID)
            .SetData(_Row, Col_DateEntered, strDate)
            .Row = _Row
        End With
        C1PatientRos.Row = 1

    End Sub

    Public Sub setGridData()
        'Fill data at form load
        Dim dt As DataTable
        'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'dt = objclsPatientROS.SelectPatientROS(m_VisitID, gnPatientID)
        dt = objclsPatientROS.SelectPatientROS(m_VisitID, m_PatientID)
        'end 
        Dim strCategoryId As String = ""
        Dim strCategory As String = ""
        Dim strItemName As String = ""
        Dim strComments As String = ""
        Dim strSource As String = ""
        Dim nPatFormID As Int64 = 0
        Dim dtTransactionDate As String = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            strCategoryId = dt.Rows(i)("sROSCategory")
            strCategory = dt.Rows(i)("sROSCategory")
            strItemName = dt.Rows(i)("sROSItem")
            strComments = dt.Rows(i)("sComments")
            strSource = dt.Rows(i)("sROSSource")
            nPatFormID = dt.Rows(i)("nPatientFormID")
            If Not IsDBNull(dt.Rows(i)("dtTransactionDate")) Then

                dtTransactionDate = dt.Rows(i)("dtTransactionDate")
            Else
                dtTransactionDate = ""
            End If
            Call FillGridTrv(strCategoryId, strCategory, strItemName, strComments, strSource, nPatFormID, dtTransactionDate)
        Next
    End Sub

    Private Sub C1PatientRos_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1PatientRos.DoubleClick
        If C1PatientRos.Row > 0 Then
            If C1PatientRos.GetData(C1PatientRos.Row, Col_ItemName) = "" Then
                Exit Sub
            End If
            blnModify = True
        End If

    End Sub

    Private Sub C1PatientRos_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PatientRos.MouseDown
        Try
            If e.Button = MouseButtons.Right Then
                'Dim trvNode As myTreeNode
                'trvNode = CType(trPrescriptionDetails.GetNodeAt(e.X, e.Y), myTreeNode)
                ' If IsNothing(trvNode) = False Then
                With C1PatientRos

                    Dim r As Integer = .HitTest(e.X, e.Y).Row
                    If r > 0 Then
                        .Select(r, True)
                        'trPrescriptionDetails.SelectedNode = trvNode

                        'If .GetData(r, Col_ItemName) = "" Then
                        '    C1PatientRos.ContextMenu = Nothing
                        'Else
                        ContextMenuC1PatientROS.MenuItems(0).Visible = True  ''''Remove ROS
                        ContextMenuC1PatientROS.MenuItems(1).Visible = False ''''Add ROS
                        ContextMenuC1PatientROS.MenuItems(2).Visible = False ''''Edit ROS
                        'Try
                        '    If (IsNothing(C1PatientRos.ContextMenu) = False) Then
                        '        C1PatientRos.ContextMenu.Dispose()
                        '        C1PatientRos.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        C1PatientRos.ContextMenu = ContextMenuC1PatientROS
                        If C1PatientRos.GetData(r, Col_ItemName) = "" Then

                            mnuRemove.Text = "Remove ROS Category"

                        Else
                            mnuRemove.Text = "Remove ROS Item"
                        End If
                        'End If
                    Else
                        'Try
                        '    If (IsNothing(C1PatientRos.ContextMenu) = False) Then
                        '        C1PatientRos.ContextMenu.Dispose()
                        '        C1PatientRos.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        C1PatientRos.ContextMenu = Nothing
                    End If
                End With

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    '**************Ojeswini20090226*******************************************************************************
    Private Sub objBtn_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles objBtn.MouseHover

        CType(sender, Button).BackColor = Color.FromArgb(254, 207, 102)
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
    End Sub

    Public Sub objBtn_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles objBtn.Click
        If CType(sender, Button).Dock <> DockStyle.Top Then
            CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            CType(sender, Button).BackColor = Color.FromArgb(207, 224, 248)
        Else
            CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            CType(sender, Button).BackColor = Color.FromArgb(207, 224, 248)
        End If
    End Sub

    Private Sub tblROS_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ROS.ItemClicked
        Try
            Select Case e.ClickedItem.Name
                Case tsbtn_SaveAndClose.Name
                    Call FinishROS()
                Case tsbtn_Close.Name
                    Call CloseROS()
                Case tsbtn_Show.Name
                    If pnlPrevROS.Visible = True Then
                        ShowHidePreviousROS("Hide")
                    Else
                        ShowHidePreviousROS("Show")
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub pnlOuter_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlOuter.Paint

    End Sub

    Private Sub mnuAddItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddItem.Click

        Try

            Dim frm As frmMSTROS
            frm = New frmMSTROS(key)
            frm.MaximizeBox = False
            frm.Text = "New ROS for " & BtnText
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            FillROSCategory1(BtnText)
            frm.Dispose()
            frm = Nothing
            'Dim oBtn As New Button
            'oBtn.Text = BtnText
            'oBtn.Tag = key

            'AddHandler oBtn.Click, AddressOf objBtn_Click

            'objBtn_Click(oBtn, e)
            'oBtn = Nothing




        Catch ex As Exception
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuEditItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditItem.Click
        Try

            Dim selectedNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvSource.SelectedNode, gloUserControlLibrary.myTreeNode)
            Dim ID As Long = selectedNode.ID
            Dim frm As frmMSTROS
            frm = New frmMSTROS(key, ID)
            frm.MaximizeBox = False
            frm.Text = "Modify ROS for " & BtnText
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            FillROSCategory1(BtnText)
            frm.Dispose()
            frm = Nothing
            ' trvSource.SelectedNode = trvSource.Nodes(0)
            'Dim oBtn As New Button
            'oBtn.Text = BtnText
            'oBtn.Tag = key

            'AddHandler oBtn.Click, AddressOf objBtn_Click

            'objBtn_Click(oBtn, e)
            'oBtn = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteROSDataDictionary()
        Try
            If arrDataDictionary.Count > 0 Then
                Dim oDictionary As New clsDataDictionary
                For i As Integer = 0 To arrDataDictionary.Count - 1
                    '' IF CATEGORY NOT PRESSENT IN BUTTONS, THEN FIND WHETHER IT IS IN TRANSACTION OR NOT ''
                    Dim oDBLayer As New ClsDBLayer
                    If oDBLayer.IsCategoryUsedInROS(arrDataDictionary(i)) = False Then
                        oDictionary.DeleteDataDictionary("ROS.sROSItem+ROS.sComments|" & arrDataDictionary(i))
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ValidateDataDictionary(ByVal categoryName As String)
        Try
            '' IF CATEGORY PRESENT, THEN DON'T DELETE DATADICTIONARY.
            '' WE ARE SEARCHING FOR CATEGORY IN BUTTONS. IF CATEGORY BUTTON PRESENT THEN EXIT.
            For Each oButton As Control In Me.pnltrvSource.Controls
                If TypeOf oButton Is Button Then
                    If oButton.Text = categoryName Then
                        Exit Sub
                    End If
                End If
            Next

            arrDataDictionary.Add(categoryName)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub C1PatientRos_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PatientRos.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone

    End Sub

    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone

    End Sub

    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return Me.MdiParent
        End Get
    End Property
    ''Sandip Darade 20090624
    '' Code Region added for  implementation of tree view control
#Region "Tree view control implementation"

    Private Sub FillROSCategory1(ByVal strGroup As String)


        'Dim ROSNode As myTreeNode
        Dim ROSTable As New DataTable


        ROSTable = objclsPatientROS.GetAllROS(strGroup)
        dtSource = ROSTable
        GloUC_trvSource.Clear()
        If Not dtSource Is Nothing Then
            GloUC_trvSource.DataSource = dtSource
            GloUC_trvSource.CodeMember = Convert.ToString(dtSource.Columns("sDescription").ColumnName)
            GloUC_trvSource.ValueMember = Convert.ToString(dtSource.Columns("nROSID").ColumnName)
            GloUC_trvSource.DescriptionMember = Convert.ToString(dtSource.Columns("sDescription").ColumnName)
            GloUC_trvSource.Tag = Convert.ToString(dtSource.Columns("nROSID").ColumnName)

            '' GLO2011-0010684
            '' Set the ROS comment column name to the TreeView
            GloUC_trvSource.Comment = Convert.ToString(dtSource.Columns("sComments").ColumnName)
            GloUC_trvSource.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
            GloUC_trvSource.FillTreeView()

        End If


    End Sub

    Private Sub GloUC_trvSource_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvSource.NodeMouseDoubleClick

        Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
        If IsNothing(oNode) = True Then
            Exit Sub
        End If

        Dim i As Integer
        Dim j As Integer
        With C1PatientRos
            Dim _Row As Integer = 0
            'Dim _tempID As Long

            'C1HistoryDetails.Rows.Count = 1

            For i = 1 To C1PatientRos.Rows.Count - 1

                If C1PatientRos.GetData(i, Col_CategoryID) = BtnText Then                    '' TO Check Duplicate History Item in A Category
                    For j = 1 To C1PatientRos.Rows.Count - 1
                        If .GetData(j, Col_ItemName) = oNode.Text And .GetData(j, Col_CategoryID) = BtnText Then
                            Exit Sub
                        End If
                    Next

                    '' TO Insert the New Item At the END of the CAtegory
                    Try
                        If .GetData(i, Col_CategoryID) <> .GetData(i + 1, Col_CategoryID) Then
                            '''' If The Current Category ID Is Not Matchs with the thw Category ID  at Next ROW 
                            '' Then Add new Row at Just After the Current Row i.i At the END of the Category
                            .Rows.Insert(i + 1)
                            _Row = i + 1
                            Exit For
                        End If

                    Catch ex As Exception
                        '''' If The System Does Not Get the ROW At (i+1) Position then it Throws the Exception
                        '' i.e we ahve to add the Row at the End 
                        .Rows.Insert(i + 1)
                        _Row = i + 1
                        Exit For
                    End Try
                End If
            Next


            If _Row = 0 Then
                ''  Category Is Not exists
                .Rows.Add()
                _Row = .Rows.Count - 1
                .SetData(_Row, Col_CategoryID, BtnText)
                .SetData(_Row, Col_CategoryName, BtnText)
                '.SetData(_Row, Col_HistoryCategory_Hidden, BtnText)
                .Rows.Insert(_Row + 1)
                _Row = _Row + 1
            End If

            .SetData(_Row, Col_CategoryID, BtnText)

            '' GLO2011-0010684
            '' Set the ROS comment value to the Grid
            .SetData(_Row, Col_Comments, oNode.Comments)

            '.SetData(_Row, Col_HistoryCategory_Hidden, BtnText)
            .SetData(_Row, Col_ItemName, oNode.Text)
            '.SetData(_Row, Col_Reaction, "")
            .Row = _Row
        End With
        'Shubhangi 20091209
        'Check the setting Reset search text box after assiging category
        If gblnResetSearchTextBox = True Then
            GloUC_trvSource.txtsearch.ResetText()
        End If

    End Sub

    Private Sub GloUC_trvSource_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvSource.KeyPress
        Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvSource.SelectedNode, gloUserControlLibrary.myTreeNode)
        If IsNothing(oNode) = True Then
            Exit Sub
        End If

        Dim i As Integer
        Dim j As Integer
        With C1PatientRos
            Dim _Row As Integer = 0
            'Dim _tempID As Long

            'C1HistoryDetails.Rows.Count = 1

            For i = 1 To C1PatientRos.Rows.Count - 1

                If C1PatientRos.GetData(i, Col_CategoryID) = BtnText Then                    '' TO Check Duplicate History Item in A Category
                    For j = 1 To C1PatientRos.Rows.Count - 1
                        If .GetData(j, Col_ItemName) = oNode.Text And .GetData(j, Col_CategoryID) = BtnText Then
                            Exit Sub
                        End If
                    Next

                    '' TO Insert the New Item At the END of the CAtegory
                    Try
                        If .GetData(i, Col_CategoryID) <> .GetData(i + 1, Col_CategoryID) Then
                            '''' If The Current Category ID Is Not Matchs with the thw Category ID  at Next ROW 
                            '' Then Add new Row at Just After the Current Row i.i At the END of the Category
                            .Rows.Insert(i + 1)
                            _Row = i + 1
                            Exit For
                        End If
                    Catch ex As Exception
                        '''' If The System Does Not Get the ROW At (i+1) Position then it Throws the Exception
                        '' i.e we ahve to add the Row at the End 
                        .Rows.Insert(i + 1)
                        _Row = i + 1
                        Exit For
                    End Try
                End If
            Next


            If _Row = 0 Then
                ''  Category Is Not exists
                .Rows.Add()
                _Row = .Rows.Count - 1
                .SetData(_Row, Col_CategoryID, BtnText)
                .SetData(_Row, Col_CategoryName, BtnText)
                '.SetData(_Row, Col_HistoryCategory_Hidden, BtnText)
                .Rows.Insert(_Row + 1)
                _Row = _Row + 1
            End If

            .SetData(_Row, Col_CategoryID, BtnText)
            '.SetData(_Row, Col_HistoryCategory_Hidden, BtnText)
            .SetData(_Row, Col_ItemName, oNode.Text)
            '.SetData(_Row, Col_Reaction, "")
            .Row = _Row
        End With

    End Sub
#End Region

    ''Sandip Darade 20090909
    Private Sub GloUC_trvSource_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GloUC_trvSource.MouseDown
        If e.Button = MouseButtons.Right Then

            Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvSource.SelectedNode, gloUserControlLibrary.myTreeNode)


            If IsNothing(oNode) = False Then

                If Not IsNothing(GloUC_trvSource.SelectedNode) Then
                    ContextMenuC1PatientROS.MenuItems(0).Visible = False ''''Remove 
                    ContextMenuC1PatientROS.MenuItems(1).Visible = True  ''''Add
                    ContextMenuC1PatientROS.MenuItems(2).Visible = True  ''''Edit
                    'Try
                    '    If (IsNothing(GloUC_trvSource.ContextMenu) = False) Then
                    '        GloUC_trvSource.ContextMenu.Dispose()
                    '        GloUC_trvSource.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    GloUC_trvSource.ContextMenu = ContextMenuC1PatientROS
                Else
                    ContextMenuC1PatientROS.MenuItems(0).Visible = False ''''Remove 
                    ContextMenuC1PatientROS.MenuItems(1).Visible = True  ''''Add
                    ContextMenuC1PatientROS.MenuItems(2).Visible = False  ''''Edit
                    'Try
                    '    If (IsNothing(GloUC_trvSource.ContextMenu) = False) Then
                    '        GloUC_trvSource.ContextMenu.Dispose()
                    '        GloUC_trvSource.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    GloUC_trvSource.ContextMenu = ContextMenuC1PatientROS
                End If
            Else
                ContextMenuC1PatientROS.MenuItems(0).Visible = False ''''Remove 
                ContextMenuC1PatientROS.MenuItems(1).Visible = True  ''''Add
                ContextMenuC1PatientROS.MenuItems(2).Visible = False  ''''Edit
                'Try
                '    If (IsNothing(GloUC_trvSource.ContextMenu) = False) Then
                '        GloUC_trvSource.ContextMenu.Dispose()
                '        GloUC_trvSource.ContextMenu = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                GloUC_trvSource.ContextMenu = ContextMenuC1PatientROS
            End If
        Else
            'Try
            '    If (IsNothing(GloUC_trvSource.ContextMenu) = False) Then
            '        GloUC_trvSource.ContextMenu.Dispose()
            '        GloUC_trvSource.ContextMenu = Nothing
            '    End If
            'Catch ex As Exception

            'End Try
            GloUC_trvSource.ContextMenu = Nothing
        End If
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID    'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    
End Class
