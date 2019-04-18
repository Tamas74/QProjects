Imports System
Imports System.Data
Imports System.IO
Imports System.Runtime.InteropServices

Public Class frmgloDICOM
    Inherits System.Windows.Forms.Form
    Implements IPatientContext
    Private Slicenumber As Int32

    Dim timerval As Integer = 0

    'Shubhangi 20091228
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

#Region " Windows Designer "
    Friend WithEvents AxezDICOMX1 As AxezDICOMax.AxezDICOMX
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents tlsDICOM As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnPrev As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnCopy As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents pnllblSelectPath As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents tlsbtnAcknowledge As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnReview As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnAddNotes As System.Windows.Forms.ToolStripButton
#End Region




    Dim timerintval As Integer = 0
    '  Friend WithEvents AxezDICOMX1 As AxezDICOMax.AxezDICOMX

    ' Private Const COL_PATIENTID As Integer = 0
    ' Private Const Col_FILEPATH As Integer = 1
    Private Const COL_DOCUMENTID As Integer = 0
    Private Const COL_FILENAME As Integer = 1
    '' chetan added on 16-nov-2010
    Private Const COL_ImportDateTime As Integer = 2
    '' chetan added on 16-nov-2010
    Private Const COL_FLAG_ACKNOWLEDGE As Integer = 3 '2
    Private Const COL_FLAG_NOTES As Integer = 4  '3
    ' '' chetan added on 16-nov-2010
    'Private Const COL_ImportDateTime As Integer = 4
    ' '' chetan added on 16-nov-2010
    '  Private Const Col_Const As Integer = 4
    Private Const COL_COUNT As Integer = 5

    Dim strpath As String = ""
    Dim _filename As String = ""

    Dim _LMPatientID As Long = 0
    Public _LMDICOMID As Long = 0
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton

    Dim _CallingForm As String = ""
    ''Sandip Darade 20090313
    Dim _nTestID As Int64 = 0
    Dim _nOrderID As Int64 = 0
    Dim _bIsForTest As Boolean = False
    Friend WithEvents tlsbtn_Delete As System.Windows.Forms.ToolStripButton
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents c1DicomFiles As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents c1DicomFiles1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Dim _sDicomids As String = ""
    Dim c1SelectedRowIndex As Integer
    Dim c1SelectedDicomFile As String = ""
    Dim c1SelectedDicomExt As String = ""

    Public Property TestID() As Int64
        Get
            Return _nTestID
        End Get
        Set(ByVal value As Int64)
            _nTestID = value
        End Set
    End Property

    Public Property OrderID() As Int64
        Get
            Return _nOrderID
        End Get
        Set(ByVal value As Int64)
            _nOrderID = value
        End Set
    End Property

    Public Property IsForTest() As Boolean
        Get
            Return _bIsForTest
        End Get
        Set(ByVal value As Boolean)
            _bIsForTest = value
        End Set
    End Property

    Public Property DICOMids() As String
        Get
            Return _sDicomids
        End Get
        Set(ByVal value As String)
            _sDicomids = value
        End Set
    End Property
#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _LMPatientID = PatientID
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try
                If (IsNothing(ContextMenuStrip1) = False) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ContextMenuStrip1)
                    If (IsNothing(ContextMenuStrip1.Items) = False) Then
                        ContextMenuStrip1.Items.Clear()
                    End If
                    ContextMenuStrip1.Dispose()
                    ContextMenuStrip1 = Nothing
                End If
            Catch

            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
                Try
                    If (IsNothing(OpenFileDialog1) = False) Then
                        OpenFileDialog1.Dispose()
                        OpenFileDialog1 = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(gloUC_PatientStrip1) = False) Then
                        gloUC_PatientStrip1.Dispose()
                        gloUC_PatientStrip1 = Nothing
                    End If
                Catch ex As Exception

                End Try
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.

    Friend WithEvents tlbbtnDICOM As System.Windows.Forms.ToolBar
    Friend WithEvents tlbbtnOpen As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnsep1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnPrev As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnsep2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnNext As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnclose As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnsep4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBrowsePath As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents trvFiles As System.Windows.Forms.TreeView
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents tlbbtnSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnsep5 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents pnlTrackBar As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel

    Friend WithEvents tlbbtnsep6 As System.Windows.Forms.ToolBarButton
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tlbCopyToClipboard As System.Windows.Forms.ToolBarButton

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmgloDICOM))
        Me.tlbbtnDICOM = New System.Windows.Forms.ToolBar()
        Me.tlbbtnOpen = New System.Windows.Forms.ToolBarButton()
        Me.tlbbtnsep1 = New System.Windows.Forms.ToolBarButton()
        Me.tlbbtnPrev = New System.Windows.Forms.ToolBarButton()
        Me.tlbbtnsep2 = New System.Windows.Forms.ToolBarButton()
        Me.tlbbtnNext = New System.Windows.Forms.ToolBarButton()
        Me.tlbbtnSep3 = New System.Windows.Forms.ToolBarButton()
        Me.tlbCopyToClipboard = New System.Windows.Forms.ToolBarButton()
        Me.tlbbtnsep4 = New System.Windows.Forms.ToolBarButton()
        Me.tlbbtnSave = New System.Windows.Forms.ToolBarButton()
        Me.tlbbtnsep5 = New System.Windows.Forms.ToolBarButton()
        Me.tlbbtnclose = New System.Windows.Forms.ToolBarButton()
        Me.tlbbtnsep6 = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.c1DicomFiles = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.c1DicomFiles1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.trvFiles = New System.Windows.Forms.TreeView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnBrowsePath = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.pnllblSelectPath = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.pnlTrackBar = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.AxezDICOMX1 = New AxezDICOMax.AxezDICOMX()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.tlsDICOM = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsbtnOpen = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnPrev = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnNext = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnCopy = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnAcknowledge = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnAddNotes = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnReview = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtn_Delete = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.c1DicomFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.c1DicomFiles1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnllblSelectPath.SuspendLayout()
        Me.pnlTrackBar.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.AxezDICOMX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.tlsDICOM.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlbbtnDICOM
        '
        Me.tlbbtnDICOM.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.tlbbtnDICOM.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tlbbtnOpen, Me.tlbbtnsep1, Me.tlbbtnPrev, Me.tlbbtnsep2, Me.tlbbtnNext, Me.tlbbtnSep3, Me.tlbCopyToClipboard, Me.tlbbtnsep4, Me.tlbbtnSave, Me.tlbbtnsep5, Me.tlbbtnclose, Me.tlbbtnsep6})
        Me.tlbbtnDICOM.ButtonSize = New System.Drawing.Size(30, 10)
        Me.tlbbtnDICOM.Dock = System.Windows.Forms.DockStyle.None
        Me.tlbbtnDICOM.DropDownArrows = True
        Me.tlbbtnDICOM.ImageList = Me.ImageList1
        Me.tlbbtnDICOM.Location = New System.Drawing.Point(2, 560)
        Me.tlbbtnDICOM.Name = "tlbbtnDICOM"
        Me.tlbbtnDICOM.ShowToolTips = True
        Me.tlbbtnDICOM.Size = New System.Drawing.Size(742, 28)
        Me.tlbbtnDICOM.TabIndex = 0
        Me.tlbbtnDICOM.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
        '
        'tlbbtnOpen
        '
        Me.tlbbtnOpen.ImageIndex = 0
        Me.tlbbtnOpen.Name = "tlbbtnOpen"
        Me.tlbbtnOpen.Tag = "Open"
        Me.tlbbtnOpen.Text = "Open"
        Me.tlbbtnOpen.ToolTipText = "Open"
        '
        'tlbbtnsep1
        '
        Me.tlbbtnsep1.Name = "tlbbtnsep1"
        Me.tlbbtnsep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnPrev
        '
        Me.tlbbtnPrev.ImageIndex = 2
        Me.tlbbtnPrev.Name = "tlbbtnPrev"
        Me.tlbbtnPrev.Tag = "Prev"
        Me.tlbbtnPrev.Text = "Prev"
        Me.tlbbtnPrev.ToolTipText = "Previous"
        '
        'tlbbtnsep2
        '
        Me.tlbbtnsep2.Name = "tlbbtnsep2"
        Me.tlbbtnsep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnNext
        '
        Me.tlbbtnNext.ImageIndex = 3
        Me.tlbbtnNext.Name = "tlbbtnNext"
        Me.tlbbtnNext.Tag = "Next"
        Me.tlbbtnNext.Text = "Next"
        Me.tlbbtnNext.ToolTipText = "Next"
        '
        'tlbbtnSep3
        '
        Me.tlbbtnSep3.Name = "tlbbtnSep3"
        Me.tlbbtnSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbCopyToClipboard
        '
        Me.tlbCopyToClipboard.ImageIndex = 6
        Me.tlbCopyToClipboard.Name = "tlbCopyToClipboard"
        Me.tlbCopyToClipboard.Tag = "Copy"
        Me.tlbCopyToClipboard.Text = "Copy"
        Me.tlbCopyToClipboard.ToolTipText = "Copy To Clipboard"
        '
        'tlbbtnsep4
        '
        Me.tlbbtnsep4.Name = "tlbbtnsep4"
        Me.tlbbtnsep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnSave
        '
        Me.tlbbtnSave.ImageIndex = 1
        Me.tlbbtnSave.Name = "tlbbtnSave"
        Me.tlbbtnSave.Tag = "Save"
        Me.tlbbtnSave.Text = "Save"
        Me.tlbbtnSave.ToolTipText = "Save"
        Me.tlbbtnSave.Visible = False
        '
        'tlbbtnsep5
        '
        Me.tlbbtnsep5.Name = "tlbbtnsep5"
        Me.tlbbtnsep5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        Me.tlbbtnsep5.Visible = False
        '
        'tlbbtnclose
        '
        Me.tlbbtnclose.ImageIndex = 4
        Me.tlbbtnclose.Name = "tlbbtnclose"
        Me.tlbbtnclose.Tag = "Close"
        Me.tlbbtnclose.Text = "Close"
        Me.tlbbtnclose.ToolTipText = "Close"
        '
        'tlbbtnsep6
        '
        Me.tlbbtnsep6.Name = "tlbbtnsep6"
        Me.tlbbtnsep6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "Remane.ico")
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(0, 51)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel1.Size = New System.Drawing.Size(300, 608)
        Me.Panel1.TabIndex = 2
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.c1DicomFiles)
        Me.Panel4.Controls.Add(Me.c1DicomFiles1)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(0, 3)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(300, 605)
        Me.Panel4.TabIndex = 20
        '
        'c1DicomFiles
        '
        Me.c1DicomFiles.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1DicomFiles.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1DicomFiles.ColumnInfo = "10,1,0,0,0,105,Columns:"
        Me.c1DicomFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1DicomFiles.ExtendLastCol = True
        Me.c1DicomFiles.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1DicomFiles.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1DicomFiles.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1DicomFiles.Location = New System.Drawing.Point(4, 1)
        Me.c1DicomFiles.Name = "c1DicomFiles"
        Me.c1DicomFiles.Rows.DefaultSize = 21
        Me.c1DicomFiles.ShowCellLabels = True
        Me.c1DicomFiles.Size = New System.Drawing.Size(295, 600)
        Me.c1DicomFiles.StyleInfo = resources.GetString("c1DicomFiles.StyleInfo")
        Me.c1DicomFiles.TabIndex = 24
        '
        'c1DicomFiles1
        '
        Me.c1DicomFiles1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1DicomFiles1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1DicomFiles1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1DicomFiles1.ColumnInfo = "0,0,0,0,0,105,Columns:"
        Me.c1DicomFiles1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1DicomFiles1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1DicomFiles1.Location = New System.Drawing.Point(4, 1)
        Me.c1DicomFiles1.Name = "c1DicomFiles1"
        Me.c1DicomFiles1.Rows.DefaultSize = 21
        Me.c1DicomFiles1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1DicomFiles1.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Columns
        Me.c1DicomFiles1.Size = New System.Drawing.Size(295, 600)
        Me.c1DicomFiles1.StyleInfo = resources.GetString("c1DicomFiles1.StyleInfo")
        Me.c1DicomFiles1.TabIndex = 9
        Me.c1DicomFiles1.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 601)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(295, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 601)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(299, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 601)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(297, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'trvFiles
        '
        Me.trvFiles.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvFiles.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvFiles.ForeColor = System.Drawing.Color.Black
        Me.trvFiles.ImageIndex = 0
        Me.trvFiles.ImageList = Me.ImageList1
        Me.trvFiles.ItemHeight = 18
        Me.trvFiles.Location = New System.Drawing.Point(229, 138)
        Me.trvFiles.Name = "trvFiles"
        Me.trvFiles.SelectedImageIndex = 0
        Me.trvFiles.ShowLines = False
        Me.trvFiles.Size = New System.Drawing.Size(126, 62)
        Me.trvFiles.TabIndex = 7
        Me.trvFiles.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.txtPath)
        Me.Panel2.Controls.Add(Me.Label26)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.Black
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel2.Size = New System.Drawing.Size(865, 23)
        Me.Panel2.TabIndex = 19
        '
        'txtPath
        '
        Me.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPath.ForeColor = System.Drawing.Color.Black
        Me.txtPath.Location = New System.Drawing.Point(8, 5)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(856, 15)
        Me.txtPath.TabIndex = 7
        Me.txtPath.Visible = False
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Location = New System.Drawing.Point(4, 5)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(4, 15)
        Me.Label26.TabIndex = 41
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Location = New System.Drawing.Point(4, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(860, 2)
        Me.Label10.TabIndex = 38
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(4, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(860, 4)
        Me.Label9.TabIndex = 37
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(860, 1)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "label1"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(3, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 22)
        Me.Label13.TabIndex = 39
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(864, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 22)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(3, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(862, 1)
        Me.Label11.TabIndex = 35
        Me.Label11.Text = "label1"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.Label29)
        Me.Panel8.Controls.Add(Me.Label28)
        Me.Panel8.Controls.Add(Me.Label27)
        Me.Panel8.Controls.Add(Me.btnBrowsePath)
        Me.Panel8.Controls.Add(Me.Label17)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(3, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(79, 20)
        Me.Panel8.TabIndex = 39
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label29.Location = New System.Drawing.Point(1, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(77, 1)
        Me.Label29.TabIndex = 43
        Me.Label29.Text = "label2"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(1, 19)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(77, 1)
        Me.Label28.TabIndex = 42
        Me.Label28.Text = "label2"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Location = New System.Drawing.Point(78, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 20)
        Me.Label27.TabIndex = 41
        Me.Label27.Text = "label4"
        '
        'btnBrowsePath
        '
        Me.btnBrowsePath.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowsePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsePath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnBrowsePath.FlatAppearance.BorderSize = 0
        Me.btnBrowsePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsePath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowsePath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowsePath.Image = CType(resources.GetObject("btnBrowsePath.Image"), System.Drawing.Image)
        Me.btnBrowsePath.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBrowsePath.Location = New System.Drawing.Point(1, 0)
        Me.btnBrowsePath.Name = "btnBrowsePath"
        Me.btnBrowsePath.Size = New System.Drawing.Size(78, 20)
        Me.btnBrowsePath.TabIndex = 6
        Me.btnBrowsePath.Text = " Browse"
        Me.btnBrowsePath.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnBrowsePath.Visible = False
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 20)
        Me.Label17.TabIndex = 40
        Me.Label17.Text = "label4"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.pnllblSelectPath)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel6.Size = New System.Drawing.Size(950, 28)
        Me.Panel6.TabIndex = 21
        Me.Panel6.Visible = False
        '
        'pnllblSelectPath
        '
        Me.pnllblSelectPath.BackColor = System.Drawing.Color.Transparent
        Me.pnllblSelectPath.BackgroundImage = CType(resources.GetObject("pnllblSelectPath.BackgroundImage"), System.Drawing.Image)
        Me.pnllblSelectPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblSelectPath.Controls.Add(Me.Label1)
        Me.pnllblSelectPath.Controls.Add(Me.Label3)
        Me.pnllblSelectPath.Controls.Add(Me.Label4)
        Me.pnllblSelectPath.Controls.Add(Me.Label15)
        Me.pnllblSelectPath.Controls.Add(Me.Label16)
        Me.pnllblSelectPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnllblSelectPath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblSelectPath.Location = New System.Drawing.Point(3, 0)
        Me.pnllblSelectPath.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnllblSelectPath.Name = "pnllblSelectPath"
        Me.pnllblSelectPath.Size = New System.Drawing.Size(944, 25)
        Me.pnllblSelectPath.TabIndex = 19
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(942, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = " Select Path"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(1, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(942, 1)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 24)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(943, 1)
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
        Me.Label16.Size = New System.Drawing.Size(944, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label1"
        '
        'Timer1
        '
        '
        'pnlTrackBar
        '
        Me.pnlTrackBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTrackBar.Controls.Add(Me.Label2)
        Me.pnlTrackBar.Controls.Add(Me.TrackBar1)
        Me.pnlTrackBar.Controls.Add(Me.Label22)
        Me.pnlTrackBar.Controls.Add(Me.Label23)
        Me.pnlTrackBar.Controls.Add(Me.Label24)
        Me.pnlTrackBar.Controls.Add(Me.Label25)
        Me.pnlTrackBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTrackBar.Location = New System.Drawing.Point(303, 51)
        Me.pnlTrackBar.Name = "pnlTrackBar"
        Me.pnlTrackBar.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlTrackBar.Size = New System.Drawing.Size(647, 33)
        Me.pnlTrackBar.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(1, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(408, 25)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "  Adjust Speed"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TrackBar1
        '
        Me.TrackBar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TrackBar1.Dock = System.Windows.Forms.DockStyle.Right
        Me.TrackBar1.Location = New System.Drawing.Point(409, 4)
        Me.TrackBar1.Maximum = 100
        Me.TrackBar1.Minimum = 1
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(234, 25)
        Me.TrackBar1.TabIndex = 9
        Me.TrackBar1.TickFrequency = 10
        Me.TrackBar1.Value = 51
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(1, 29)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(642, 1)
        Me.Label22.TabIndex = 14
        Me.Label22.Text = "label2"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 4)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 26)
        Me.Label23.TabIndex = 13
        Me.Label23.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(643, 4)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 26)
        Me.Label24.TabIndex = 12
        Me.Label24.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 3)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(644, 1)
        Me.Label25.TabIndex = 11
        Me.Label25.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.AxezDICOMX1)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.trvFiles)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.Label21)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(303, 84)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(647, 575)
        Me.Panel3.TabIndex = 9
        '
        'AxezDICOMX1
        '
        Me.AxezDICOMX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxezDICOMX1.Location = New System.Drawing.Point(1, 1)
        Me.AxezDICOMX1.Name = "AxezDICOMX1"
        Me.AxezDICOMX1.OcxState = CType(resources.GetObject("AxezDICOMX1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxezDICOMX1.Size = New System.Drawing.Size(642, 570)
        Me.AxezDICOMX1.TabIndex = 8
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(1, 571)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(642, 1)
        Me.Label18.TabIndex = 12
        Me.Label18.Text = "label2"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 571)
        Me.Label19.TabIndex = 11
        Me.Label19.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(643, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 571)
        Me.Label20.TabIndex = 10
        Me.Label20.Text = "label3"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(644, 1)
        Me.Label21.TabIndex = 9
        Me.Label21.Text = "label1"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.tlsDICOM)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(950, 56)
        Me.Panel5.TabIndex = 10
        '
        'tlsDICOM
        '
        Me.tlsDICOM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDICOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDICOM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDICOM.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDICOM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnOpen, Me.tlsbtnPrev, Me.tlsbtnNext, Me.tlsbtnCopy, Me.ts_btnRefresh, Me.tlsbtnAcknowledge, Me.tlsbtnAddNotes, Me.tlsbtnReview, Me.ToolStripButton1, Me.tlsbtn_Delete, Me.tlsbtnClose})
        Me.tlsDICOM.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDICOM.Location = New System.Drawing.Point(0, 0)
        Me.tlsDICOM.Name = "tlsDICOM"
        Me.tlsDICOM.Size = New System.Drawing.Size(950, 53)
        Me.tlsDICOM.TabIndex = 0
        Me.tlsDICOM.Text = "ToolStrip1"
        '
        'tlsbtnOpen
        '
        Me.tlsbtnOpen.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnOpen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnOpen.Image = CType(resources.GetObject("tlsbtnOpen.Image"), System.Drawing.Image)
        Me.tlsbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnOpen.Name = "tlsbtnOpen"
        Me.tlsbtnOpen.Size = New System.Drawing.Size(54, 50)
        Me.tlsbtnOpen.Tag = "Import"
        Me.tlsbtnOpen.Text = "Import"
        Me.tlsbtnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnPrev
        '
        Me.tlsbtnPrev.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnPrev.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnPrev.Image = CType(resources.GetObject("tlsbtnPrev.Image"), System.Drawing.Image)
        Me.tlsbtnPrev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnPrev.Name = "tlsbtnPrev"
        Me.tlsbtnPrev.Size = New System.Drawing.Size(63, 50)
        Me.tlsbtnPrev.Tag = "Prev"
        Me.tlsbtnPrev.Text = "Previous"
        Me.tlsbtnPrev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnNext
        '
        Me.tlsbtnNext.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnNext.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnNext.Image = CType(resources.GetObject("tlsbtnNext.Image"), System.Drawing.Image)
        Me.tlsbtnNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnNext.Name = "tlsbtnNext"
        Me.tlsbtnNext.Size = New System.Drawing.Size(39, 50)
        Me.tlsbtnNext.Tag = "Next"
        Me.tlsbtnNext.Text = "Next"
        Me.tlsbtnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnCopy
        '
        Me.tlsbtnCopy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnCopy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnCopy.Image = CType(resources.GetObject("tlsbtnCopy.Image"), System.Drawing.Image)
        Me.tlsbtnCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnCopy.Name = "tlsbtnCopy"
        Me.tlsbtnCopy.Size = New System.Drawing.Size(42, 50)
        Me.tlsbtnCopy.Tag = "Copy"
        Me.tlsbtnCopy.Text = "Copy"
        Me.tlsbtnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnCopy.ToolTipText = "Copy To Clipboard"
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnAcknowledge
        '
        Me.tlsbtnAcknowledge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnAcknowledge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnAcknowledge.Image = CType(resources.GetObject("tlsbtnAcknowledge.Image"), System.Drawing.Image)
        Me.tlsbtnAcknowledge.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnAcknowledge.Name = "tlsbtnAcknowledge"
        Me.tlsbtnAcknowledge.Size = New System.Drawing.Size(62, 50)
        Me.tlsbtnAcknowledge.Tag = "Acknowledge"
        Me.tlsbtnAcknowledge.Text = "Add Ack"
        Me.tlsbtnAcknowledge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnAcknowledge.ToolTipText = "Add Acknowledgement"
        Me.tlsbtnAcknowledge.Visible = False
        '
        'tlsbtnAddNotes
        '
        Me.tlsbtnAddNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnAddNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnAddNotes.Image = CType(resources.GetObject("tlsbtnAddNotes.Image"), System.Drawing.Image)
        Me.tlsbtnAddNotes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnAddNotes.Name = "tlsbtnAddNotes"
        Me.tlsbtnAddNotes.Size = New System.Drawing.Size(75, 50)
        Me.tlsbtnAddNotes.Tag = "AddNotes"
        Me.tlsbtnAddNotes.Text = "Add Notes"
        Me.tlsbtnAddNotes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnReview
        '
        Me.tlsbtnReview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnReview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnReview.Image = CType(resources.GetObject("tlsbtnReview.Image"), System.Drawing.Image)
        Me.tlsbtnReview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnReview.Name = "tlsbtnReview"
        Me.tlsbtnReview.Size = New System.Drawing.Size(55, 50)
        Me.tlsbtnReview.Tag = "Review"
        Me.tlsbtnReview.Text = "Review"
        Me.tlsbtnReview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnReview.Visible = False
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(92, 50)
        Me.ToolStripButton1.Tag = "Amendments"
        Me.ToolStripButton1.Text = "Amendments"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtn_Delete
        '
        Me.tlsbtn_Delete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtn_Delete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtn_Delete.Image = CType(resources.GetObject("tlsbtn_Delete.Image"), System.Drawing.Image)
        Me.tlsbtn_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtn_Delete.Name = "tlsbtn_Delete"
        Me.tlsbtn_Delete.Size = New System.Drawing.Size(50, 50)
        Me.tlsbtn_Delete.Tag = "Delete"
        Me.tlsbtn_Delete.Text = "Delete"
        Me.tlsbtn_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Tag = "Close"
        Me.tlsbtnClose.Text = "Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Panel2)
        Me.Panel9.Controls.Add(Me.Panel10)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 28)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(950, 23)
        Me.Panel9.TabIndex = 9
        Me.Panel9.Visible = False
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.Panel8)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel10.Location = New System.Drawing.Point(865, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel10.Size = New System.Drawing.Size(85, 23)
        Me.Panel10.TabIndex = 40
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(300, 51)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 608)
        Me.Splitter1.TabIndex = 22
        Me.Splitter1.TabStop = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Panel3)
        Me.Panel7.Controls.Add(Me.pnlTrackBar)
        Me.Panel7.Controls.Add(Me.Splitter1)
        Me.Panel7.Controls.Add(Me.Panel1)
        Me.Panel7.Controls.Add(Me.Panel9)
        Me.Panel7.Controls.Add(Me.Panel6)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 56)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(950, 659)
        Me.Panel7.TabIndex = 10
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'frmgloDICOM
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(950, 715)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.tlbbtnDICOM)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmgloDICOM"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "DICOM"
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.c1DicomFiles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.c1DicomFiles1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.pnllblSelectPath.ResumeLayout(False)
        Me.pnlTrackBar.ResumeLayout(False)
        Me.pnlTrackBar.PerformLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.AxezDICOMX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.tlsDICOM.ResumeLayout(False)
        Me.tlsDICOM.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Custom Constructors"
    Public Sub New(ByVal filename As String, ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _filename = filename
        _LMPatientID = PatientID
    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal CallingFormName As String, ByRef DICOMID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _CallingForm = CallingFormName
        _LMPatientID = PatientID
        _LMDICOMID = DICOMID
    End Sub
#End Region



    Public Function GetDirectories(ByVal oPath As String)
        Try


            With trvFiles
                .Nodes.Clear()
                'Dim oDirectoryNode As TreeNode
                Dim oFileNode As TreeNode
                If System.IO.Directory.Exists(oPath) = True Then
                    '   Dim oDirectories As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(oPath)


                    'If oDirectories.GetDirectories.Length > 0 Then

                    '    Dim oDirectory As System.IO.DirectoryInfo
                    '    For Each oDirectory In oDirectories.GetDirectories()
                    '        'oDirectoryNode = New TreeNode
                    '        'With oDirectoryNode
                    '        '    .Text = oDirectory.Name
                    '        '    .Tag = oDirectory.FullName
                    '        'End With

                    '        Dim oFile As System.IO.FileInfo
                    '        For Each oFile In oDirectory.GetFiles()
                    '            oFileNode = New TreeNode
                    '            With oFileNode
                    '                .Text = oFile.Name
                    '                .Tag = oFile.FullName
                    '                .ImageIndex = 5
                    '                .SelectedImageIndex = 5
                    '            End With
                    '            .Nodes.Add(oFileNode)
                    '            oFileNode = Nothing
                    '        Next
                    '        '.Nodes.Add(oFileNode)
                    '        'oDirectoryNode = Nothing
                    '    Next
                    'Else

                    Dim oDirectory As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(oPath)
                    Dim oFile As System.IO.FileInfo
                    For Each oFile In oDirectory.GetFiles()
                        oFileNode = New TreeNode
                        With oFileNode
                            .Text = oFile.Name
                            .Tag = oFile.FullName
                            .ImageIndex = 5
                            .SelectedImageIndex = 5
                        End With
                        .Nodes.Add(oFileNode)
                        oFileNode = Nothing
                    Next
                    '  End If
                End If
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Private Sub btnBrowsePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsePath.Click
        Dim oBrowse As New System.Windows.Forms.FolderBrowserDialog
        If oBrowse.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
            txtPath.Text = oBrowse.SelectedPath
            txtPath.Tag = Trim(txtPath.Text)
            GetDirectories(txtPath.Text)
        End If
        oBrowse.Dispose()
    End Sub

    Private Sub tlbbtnDICOM_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tlbbtnDICOM.ButtonClick
        Try


            Select Case e.Button.Tag

                Case "Open"
                    '' SUDHIR 20090521 '' CHECK PROVIDER ''
                    If gblnProviderDisable = True Then
                        If ShowAssociateProvider(_LMPatientID, Me) = True Then
                            CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                        End If
                    End If
                    '' END SUDHIR

                    'Dim objsender As Object
                    'Dim obje As System.EventArgs
                    'Button2_Click(objsender, obje)
                    '     OpenFileDialog1.Filter = "*.DCM"
                    If OpenFileDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                        Timer1.Enabled = False
                        If Not IsNothing(OpenFileDialog1.FileName) Then
                            Slicenumber = 1
                            AxezDICOMX1.DCMfilename = OpenFileDialog1.FileName
                        End If
                        Timer1.Enabled = True
                    End If

                    If c1DicomFiles.Rows.Count > 1 Then
                        c1DicomFiles_MouseDown(Nothing, Nothing)
                    End If

                Case "Next"
                    Timer1.Enabled = False
                    If Not IsNothing(trvFiles.SelectedNode) Then

                        If Not IsNothing(trvFiles.SelectedNode.NextNode) Then
                            trvFiles.SelectedNode = trvFiles.SelectedNode.NextNode
                            AxezDICOMX1.DCMfilename = trvFiles.SelectedNode.Tag
                            Slicenumber = 1
                        End If
                    End If
                    Timer1.Enabled = True
                Case "Prev"
                    Timer1.Enabled = False
                    If Not IsNothing(trvFiles.SelectedNode) Then
                        If Not IsNothing(trvFiles.SelectedNode.PrevNode) Then
                            trvFiles.SelectedNode = trvFiles.SelectedNode.PrevNode
                            AxezDICOMX1.DCMfilename = trvFiles.SelectedNode.Tag
                            Slicenumber = 1
                        End If
                    End If
                    Timer1.Enabled = True
                Case "Close"
                    Me.Close()
                Case "Save"
                    SaveDicomFile()
                Case "Copy"
                    Dim blnClipflag As Boolean
                    blnClipflag = AxezDICOMX1.DCMcopyImage2Clipboard()
                    If blnClipflag = True Then
                        MsgBox("The image is copied to Clipboard")
                    End If


            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try

            If AxezDICOMX1.DCMimageSlices > 0 Then
                If Slicenumber <= AxezDICOMX1.DCMimageSlices Then
                    'If Slicenumber > 1 Then
                    '    AxezDICOMX1.DCMunloadImages = Slicenumber - 1
                    'ElseIf Slicenumber = 1 Then
                    '    AxezDICOMX1.DCMunloadImages = 1
                    'End If
                    AxezDICOMX1.set_DCMmosaicX(1, 2, Slicenumber, Slicenumber)
                    Slicenumber = Slicenumber + 1
                Else
                    Slicenumber = 1
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub frmgloDICOM_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmgloDICOM_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not IsNothing(AxezDICOMX1) Then
                AxezDICOMX1.Dispose()
                AxezDICOMX1 = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

    Private Sub frmgloDICOM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            gloC1FlexStyle.Style(c1DicomFiles)
            'Shubhangi 20091228 Add patient details strip
            Call Set_PatientDetailStrip(_LMPatientID)



            Timer1.Enabled = False
            Timer1.Interval = 1000
            getLatestVideoFile()
            'AxezDICOMX1.DCMtoolbar = False
            AxezDICOMX1.DCMtoolbar = True
            AxezDICOMX1.DCMbestFitZoom = True
            AxezDICOMX1.DCMoverlayOn = False


            ' EnableDisableAckReviewBtns()
            '  AxezDICOMX1.DCMcopyImage2Clipboard = True
            'timerval = TrackBar1.Value
            timerintval = Timer1.Interval
            If (c1DicomFiles.Rows.Count > 1) Then

                tlsbtn_Delete.Visible = True
            Else
                ' Dim dtstr As String = Format(Date.Now, "yyyymmdd hhmmss").ToString
                '  c1DicomFiles.Rows.Add()
                ' Dim rowcnt As Integer = c1DicomFiles.Rows.Count - 1
                ' c1DicomFiles.SetData(rowcnt, COL_FILENAME, dtstr)
                ' c1DicomFiles.SetData(rowcnt, COL_DOCUMENTID, -1)
                tlsbtn_Delete.Visible = False
            End If
            Try
                Me.Text = "DICOM"
                gloPatient.gloPatient.GetWindowTitle(Me, _LMPatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch comex As COMException
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub
    'Shubhamgi 20091228
    'Bug No: 4500. Add patient details strip.
    Private Sub Set_PatientDetailStrip(ByVal m_PatientID As Int64)
        ' '' Add Patient Details Control
        If Not IsNothing(gloUC_PatientStrip1) Then
            Me.Controls.Remove(gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip
        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            '' Pass Paarameters Type of Form
            .ShowDetail(m_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.DICOM) ''Sandip Darade 20100107
            .BringToFront()
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        Panel5.SendToBack()
        Panel7.BringToFront()
        'pnlToolStrip.SendToBack()
        'pnlMain.BringToFront()
        'pnlMain.Padding = New Padding(0, 3, 0, 0)

    End Sub

    Private Sub EnableDisableAckReviewBtns(ByVal IsAck As Boolean)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvFiles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvFiles.Click

    End Sub

    Private Sub trvFiles_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvFiles.DoubleClick
        Try
            Timer1.Enabled = False
            If Not IsNothing(trvFiles.SelectedNode) Then
                Slicenumber = 1
                AxezDICOMX1.DCMfilename = trvFiles.SelectedNode.Tag
            End If
            Timer1.Enabled = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

    Private Sub SaveDicomFile()
        Dim dirpath As String
        Dim srcfilepath As String
        Dim destfilepath As String
        Dim dtstr As String

        Try
            'create directory by name PatientID if it does not already exists
            If DICOMPath = "" Then
                MessageBox.Show("Please set the DICOM path from Tool->Settings->Server Path, to save the file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            dirpath = DICOMPath & "\" & _LMPatientID

            If Not Directory.Exists(dirpath) Then
                Directory.CreateDirectory(dirpath)
            End If

            'get the path of the file to be copied from the treeviews selected node
            srcfilepath = trvFiles.SelectedNode.Tag

            'get the date and time in required format
            'destination file name should be in the format : PatientID_currentdatetime
            dtstr = gloGlobal.clsFileExtensions.NewDocumentNameWithoutPath(dirpath, "") '  Format(Date.Now, "yyyymmdd hhmmss").ToString() & System.Guid.NewGuid().ToString()

            'set the path of the destination file
            destfilepath = dirpath & "\" & dtstr

            'copy the file to the destination directory
            File.Copy(srcfilepath, destfilepath)



            'also save irt against the patient in database

            'getPrefix transactionid
            Dim MachineId As Long = 0
            MachineId = GetPrefixTransactionID(_LMPatientID)

            'If _CallingForm <> "" Then
            '    If _CallingForm = "Radiology Orders" Then
            '        InsertDicomFile(MachineId, 0, gnPatientID, dtstr, "DCM", 0, "", "", _LMDICOMID)
            '    End If
            'Else
            '    'form opened directly from Dashboard
            '    InsertDicomFile(MachineId, 0, gnPatientID, dtstr, "DCM", 0, "", "", _LMDICOMID)
            'End If


            '//temporarily take dicomdocname as ""
            Dim sDICOMDocName As String = ""

            InsertDicomFile(MachineId, 0, _LMPatientID, dtstr, "DCM", sDICOMDocName, gnLoginID, _LMDICOMID)


            MsgBox("The selected DICOM file successfully saved against the Patient")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SaveDicomFileOpened(ByVal srcfilepath1 As String)
        'Dim dirpath As String
        'Dim srcfilepath As String
        'Dim destfilepath As String
        ' Dim dtstr As String

        Try
            Dim oFileInfo As New FileInfo(srcfilepath1)
            Dim strArr() As String = srcfilepath1.Split("\")
            Dim MachineId As Long = 0
            MachineId = GetPrefixTransactionID(_LMPatientID)
            Dim sDICOMDocName As String = ""
            Dim dicomFileName() As String = strArr(strArr.Length - 1).Split(".")

            InsertDicomFile(MachineId, 0, _LMPatientID, dicomFileName(0), oFileInfo.Extension, sDICOMDocName, gnLoginID, _LMDICOMID)

            ''Sandip Darade 20090314

            FillDICOMFilesToGrid(MachineId, _LMDICOMID, _LMPatientID, dicomFileName(0), oFileInfo.Extension)
            If _CallingForm <> "" Then
                If _CallingForm = "Radiology Orders" Then
                    frm_LM_Orders.LMDICOMID = _LMDICOMID
                End If
            End If

            'MsgBox("The selected DICOM file successfully saved against the Patient")





            '' Dim oDirInfo As New DirectoryInfo

            ''// srcfilepath = oFileInfo.CopyTo(destfilepath = dirpath & "\" & dtstr & ".dcm") '// srcfilepath1 '   trvFiles.SelectedNode.Tag
            'srcfilepath = srcfilepath1
            ''get the date and time in required format
            ''destination file name should be in the format : PatientID_currentdatetime
            'dtstr = Format(Date.Now, "yyyymmdd hhmmss").ToString

            ''set the path of the destination file
            'destfilepath = dirpath & "\" & dtstr & oFileInfo.Extension

            ''copy the file to the destination directory
            'File.Copy(srcfilepath, destfilepath)

            ''insert the dicom file path in database

            ''getPrefix transactionid
            'Dim MachineId As Long = 0
            'MachineId = GetPrefixTransactionID(_LMPatientID)

            ''If _CallingForm <> "" Then
            ''    If _CallingForm = "Radiology Orders" Then
            ''        InsertDicomFile(MachineId, 0, gnPatientID, dtstr, "DCM", 0, "", "", _LMDICOMID)
            ''    End If
            ''Else
            ''    'form opened directly from Dashboard
            ''    InsertDicomFile(MachineId, 0, gnPatientID, dtstr, "DCM", 0, "", "", _LMDICOMID)
            ''End If

            ''  InsertDicomFile(MachineId, 0, gnPatientID, dtstr, "DCM", 0, "", "", _LMDICOMID)

            ''//temporarily take dicomdocname as ""
            'Dim sDICOMDocName As String = ""

            'InsertDicomFile(MachineId, 0, _LMPatientID, dtstr, oFileInfo.Extension, sDICOMDocName, gnLoginID, _LMDICOMID)

            ' ''Sandip Darade 20090314

            'FillDICOMFilesToGrid(MachineId, _LMDICOMID, _LMPatientID, dtstr, oFileInfo.Extension)
            'If _CallingForm <> "" Then
            '    If _CallingForm = "Radiology Orders" Then
            '        frm_LM_Orders.LMDICOMID = _LMDICOMID
            '    End If
            'End If

            ''MsgBox("The selected DICOM file successfully saved against the Patient")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InsertDicomFile(ByVal MachineID As Long, ByVal DicomID As Long, ByVal nPatientID As Long, ByVal DICOMFileName As String, ByVal DICOMFileExtn As String, ByVal DICOMDocName As String, ByVal nUserID As Long, ByRef RetDICOMID As Long)
        Dim objDicom As New clsDICOM

        Try
            objDicom.InsertDicomFile(MachineID, DicomID, nPatientID, DICOMFileName, DICOMFileExtn, DICOMDocName, nUserID, RetDICOMID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        Dim val As Integer
        val = TrackBar1.Value
        'Dim constval As Integer = 5
        Dim dedval As Integer = 0

        Try
            Timer1.Enabled = False
            dedval = 1000 * val / 100
            Timer1.Interval = 1000 - dedval + 50
            ' Timer1.Interval = val * constval
            'MsgBox(Timer1.Interval.ToString)
            'timerval = val
            Timer1.Enabled = True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub getLatestVideoFile()
        Try
            strpath = DICOMPath & "\" & _LMPatientID
            'Fill the grid with the dicom files if any for the Patient
            DesignGrid(c1DicomFiles)
            If Directory.Exists(strpath) Then
                FillDICOMFiles(_LMPatientID)
                If _filename <> "" Then
                    LoadDicomFile(_filename)
                End If
                c1DicomFiles_MouseDown(Nothing, Nothing)

                'If _CallingForm <> "" Then
                '    If _CallingForm = "Radiology Orders" Then
                '        tlsDICOM.Items("tlsbtnOpen").PerformClick()

                '        Exit Sub
                '    End If
                'End If
                'With trvFiles
                '    .Nodes.Clear()
                '    Dim oFileNode As TreeNode
                '    Dim oDirectory As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(strpath)
                '    Dim oFile As System.IO.FileInfo
                '    For Each oFile In oDirectory.GetFiles()
                '        oFileNode = New TreeNode
                '        With oFileNode
                '            .Text = oFile.Name
                '            .Tag = oFile.FullName
                '            .ImageIndex = 5
                '            .SelectedImageIndex = 5
                '        End With
                '        .Nodes.Add(oFileNode)
                '        oFileNode = Nothing
                '    Next
                'End With

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub LoadDicomFile(ByVal filename As String)
        Dim strFileName As String = ""
        Timer1.Enabled = False
        strFileName = DICOMPath & "\" & _LMPatientID & "\" & filename
        Try
            AxezDICOMX1.DCMfilename = strFileName '& ".jpg"
            ' AxezDICOMX1.DCMfilename = "E:\Developer Working Folder\Temp\DICOM\397693651515772001\20093221 023255.dcm"
        Catch comex As ComponentModel.Win32Exception
        Catch ioex As IOException
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

        Slicenumber = 1
        Timer1.Enabled = True
    End Sub

    Private Sub DesignGrid(ByVal oFillGrid As C1.Win.C1FlexGrid.C1FlexGrid)
        oFillGrid.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        oFillGrid.Rows.Count = 1
        oFillGrid.Rows.Fixed = 1
        oFillGrid.Cols.Count = COL_COUNT
        oFillGrid.Cols.Fixed = 0

        oFillGrid.Cols(COL_DOCUMENTID).Width = 0
        oFillGrid.Cols(COL_FILENAME).Width = 140
        oFillGrid.Cols(COL_FLAG_ACKNOWLEDGE).Width = 20
        oFillGrid.Cols(COL_FLAG_NOTES).Width = 20
        oFillGrid.Cols(COL_ImportDateTime).Width = 120
        oFillGrid.Cols(COL_DOCUMENTID).Visible = False
        oFillGrid.Cols(COL_FILENAME).Visible = True
        oFillGrid.Cols(COL_FLAG_ACKNOWLEDGE).Visible = True
        oFillGrid.Cols(COL_FLAG_NOTES).Visible = True
        oFillGrid.Cols(COL_ImportDateTime).Visible = True
        oFillGrid.Cols(COL_DOCUMENTID).DataType = GetType(System.Int64)
        oFillGrid.Cols(COL_FILENAME).DataType = GetType(System.String)
        oFillGrid.Cols(COL_FLAG_ACKNOWLEDGE).DataType = GetType(System.Int32)
        oFillGrid.Cols(COL_FLAG_NOTES).DataType = GetType(System.Int32)
        oFillGrid.Cols(COL_ImportDateTime).DataType = GetType(System.String)


        oFillGrid.Cols(COL_DOCUMENTID).AllowEditing = False
        oFillGrid.Cols(COL_FILENAME).AllowEditing = False
        oFillGrid.Cols(COL_FLAG_ACKNOWLEDGE).AllowEditing = False
        oFillGrid.Cols(COL_FLAG_NOTES).AllowEditing = False
        oFillGrid.Cols(COL_ImportDateTime).AllowEditing = False

        ' oFillGrid.Rows.Add()
        Dim orowcnt As Integer = oFillGrid.Rows.Count - 1
        oFillGrid.SetData(orowcnt, COL_FILENAME, "File Name")
        oFillGrid.SetData(orowcnt, COL_ImportDateTime, "DateTime")
        ' oFillGrid.Rows(0).Style.BackColor = Color.FromArgb(86, 126, 211)
        'C1.Win.C1FlexGrid.CellStyle ostyle_Document_NotAcknowledge = oFillGrid.Styles.Add("style_Document_NotAcknowledge");
        '         ostyle_Document_NotAcknowledge.Font = new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        '         ostyle_Document_NotAcknowledge.ForeColor = Color.Black;
        '         ostyle_Document_NotAcknowledge.BackColor = Color.FromArgb(255, 255, 255);
        '         //ostyle_Document_NotAcknowledge.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
        '         //ostyle_Document_NotAcknowledge.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
        '         ostyle_Document_NotAcknowledge.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;

        '         C1.Win.C1FlexGrid.CellStyle ostyle_Document_Acknowledge = oFillGrid.Styles.Add("style_Document_Acknowledge");
        '         ostyle_Document_Acknowledge.Font = new System.Drawing.Font(gloEDocV3Admin.gFontName, gloEDocV3Admin.gFontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        '         ostyle_Document_Acknowledge.ForeColor = Color.Black;
        '         ostyle_Document_Acknowledge.BackColor = Color.FromArgb(255, 255, 255);
        '         //ostyle_Document_Acknowledge.BackgroundImage = global::gloEDocument.Properties.Resources.CategoryBackgroundGray;
        '         //ostyle_Document_Acknowledge.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
        '         ostyle_Document_Acknowledge.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack;


    End Sub

    Private Sub FillDICOMFiles(ByVal PatientID As Long)
        Dim dtDicom As New DataTable
        Dim objDICOM As New clsDICOM

        Try
            Dim strFilePath As String = ""
            Dim NewRow As Integer
            'dtDicom = objDICOM.GetDICOMDetails(PatientID)

            If (IsForTest = False And TestID = 0) Then
                dtDicom = objDICOM.GetDICOMDetails(PatientID)
            Else '' Sandip Darade 20090314
                If (DICOMids <> "") Then
                    dtDicom = objDICOM.GetDICOMDetails(PatientID, TestID, OrderID, True, DICOMids)
                End If
            End If
            If dtDicom.Rows.Count > 0 Then


                For i As Integer = 0 To dtDicom.Rows.Count - 1
                    strFilePath = DICOMPath & "\" & _LMPatientID & "\" & dtDicom.Rows(i)("DICOMFileName") & dtDicom.Rows(i)("DICOMFileExtn")
                    If File.Exists(strFilePath) Then

                        ''20120718:On Explicit frmgloDICOM_Load(Nothing, Nothing) call _filename is not changeing;causes error message
                        'If i = 0 Then
                        '    _filename = dtDicom.Rows(i)("DICOMFileName") & dtDicom.Rows(i)("DICOMFileExtn")
                        'End If
                        ' '''

                        '20120717:Newly added Code

                        Dim _AddThisFile As Boolean = True
                        For j As Integer = 0 To c1DicomFiles.Rows.Count - 1
                            Dim strFilename As String = dtDicom.Rows(i)("DICOMFileName")
                            If c1DicomFiles.Rows(j)(COL_FILENAME) = strFilename Then
                                _AddThisFile = False
                            End If
                        Next


                        If _AddThisFile Then
                            With c1DicomFiles
                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                '  Dim strdocid As String = .GetData(0, COL_DOCUMENTID).ToString()
                                .SetData(NewRow, COL_DOCUMENTID, dtDicom.Rows(i)("DicomID"))
                                .SetData(NewRow, COL_FILENAME, dtDicom.Rows(i)("DICOMFileName"))
                                .SetData(NewRow, COL_ImportDateTime, dtDicom.Rows(i)("dtImportDateTime"))
                                Dim AckID As Long = 0
                                Dim NotesID As Long = 0
                                AckID = objDICOM.GetAckID(dtDicom.Rows(i)("DicomID"), 1)
                                If AckID <> 0 Then
                                    .SetCellImage(NewRow, COL_FLAG_ACKNOWLEDGE, Global.gloEMR.My.Resources.Flag_Yellow)
                                End If
                                NotesID = objDICOM.GetAckID(dtDicom.Rows(i)("DicomID"), 3)
                                If NotesID <> 0 Then
                                    .SetCellImage(NewRow, COL_FLAG_NOTES, Global.gloEMR.My.Resources.Custom)
                                End If

                            End With
                        End If
                    End If
                Next
            End If


            If c1DicomFiles.Rows.Count > 1 Then
                ' c1DicomFiles.Row = 1
                c1DicomFiles.Select()
            End If


            ' Dim oFileNode As TreeNode
            Dim oDirectory As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(strpath)
            Dim oFile As System.IO.FileInfo
            For Each oFile In oDirectory.GetFiles()

            Next

            'c1DicomFiles.DataSource = dtDicom
            'fill the grid and set the acknowledge flag

            'Start of Code added by manoj jadhav on 20130315 for selecting current dicom file from task module
            Dim TempDocumentID As Long = 0
            If String.Compare(_CallingForm, "Task", True) = 0 AndAlso _LMDICOMID > 0 AndAlso c1DicomFiles.Rows.Count > 0 Then
                For DRow As Integer = 0 To c1DicomFiles.Rows.Count - 1
                    Long.TryParse(Convert.ToString(c1DicomFiles.GetData(DRow, COL_DOCUMENTID)), TempDocumentID)
                    If TempDocumentID = _LMDICOMID Then
                        c1DicomFiles.Select(DRow, COL_DOCUMENTID)
                        Exit For
                    End If
                Next
            End If
            TempDocumentID = 0
            'end of Code added by manoj jadhav on 20130315 for selecting current dicom file from task module

        Catch ex As Exception

            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Sub

    Private Sub tlsDICOM_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDICOM.ItemClicked
        Select Case e.ClickedItem.Tag

            Case "Import"
                'Dim objsender As Object
                'Dim obje As System.EventArgs
                'Button2_Click(objsender, obje)
                'OpenFileDialog1.Filter = "*.DCM"
                ''Sandip Darade 20090319
                'show only DICOM(.dcm) and
                'OpenFileDialog1.Filter = "DCM Files(*.DCM)|*.DCM|JPEG files(*.jpg*)|*.jpg*"
                ''''''' New Flow '''''''
                Try
                    Dim oImport As New frmImportDICOM(_LMPatientID)
                    oImport.ShowDialog(IIf(IsNothing(oImport.Parent), Me, oImport.Parent))
                    Dim ImportedItems As New ArrayList()
                    ImportedItems = oImport.FilesToImport
                    If ImportedItems.Count > 0 Then
                        Timer1.Enabled = False
                        ''20120718:added for loop to insert multiple file
                        For iCounter As Integer = 0 To ImportedItems.Count - 1
                            If Not IsNothing(ImportedItems(iCounter).ToString()) Then
                                Slicenumber = 1
                                Dim isExist = False
                                Dim oFileInfo As New FileInfo(ImportedItems(iCounter).ToString())
                                If (c1DicomFiles.Rows.Count > 1) Then
                                    Dim strArr() As String = ImportedItems(iCounter).ToString().Split("\")
                                    Dim dicomFileName() As String = strArr(strArr.Length - 1).Split(".")
                                    If (c1DicomFiles.Rows.Count > 1) Then
                                        For fileCount As Integer = 1 To c1DicomFiles.Rows.Count - 1 '   For i As Integer = 0 To c1DicomFiles.Rows.Count - 1 column header is added so 0 change to 1
                                            Dim gridFile As String = Convert.ToString(c1DicomFiles.GetData(fileCount, COL_FILENAME))
                                            If (dicomFileName(0) = Convert.ToString(c1DicomFiles.GetData(fileCount, COL_FILENAME))) Then
                                                c1DicomFiles.Refresh()
                                                isExist = True
                                                File.Delete(ImportedItems(iCounter))
                                                Exit For
                                            End If
                                        Next
                                    End If
                                End If

                                If isExist = False Then
                                    Try
                                        AxezDICOMX1.DCMfilename = ImportedItems(iCounter).ToString()
                                    Catch COmex As COMException
                                        MessageBox.Show(COmex.Message, "gloEMR", MessageBoxButtons.OK)
                                    End Try
                                    If (AxezDICOMX1.get_DCMfilenameSilentErrors(ImportedItems(iCounter).ToString()) <= 0) Then
                                        SaveDicomFileOpened(ImportedItems(iCounter).ToString())
                                        If (c1DicomFiles.Rows.Count > 1) Then
                                            tlsbtn_Delete.Visible = True
                                        Else
                                            tlsbtn_Delete.Visible = False
                                            AxezDICOMX1.DCMunloadImages = True
                                        End If
                                        Reset_DICOMIDstring()
                                    End If
                                End If
                            End If
                        Next

                        Timer1.Enabled = True
                        If _CallingForm <> "" Then
                            _CallingForm = ""
                            'Me.Close() Sandip Darade 20090314
                            Exit Sub
                        End If
                    Else
                        If _CallingForm <> "" Then
                            _CallingForm = ""
                            If _CallingForm = "Radiology Orders" Then
                                frm_LM_Orders.LMDICOMID = 0
                            End If
                            'Me.Close()
                        End If
                    End If

                    'Object Disposed

                    If Not IsNothing(oImport) Then
                        oImport.FilesToImport = Nothing
                        oImport.Dispose()
                        oImport = Nothing
                    End If
                   
                    ImportedItems = Nothing


                    'getLatestVideoFile()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK)
                End Try
                '''''''End of New Flow '''''''


                ''''''' Original Flow '''''''

                'If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                '    Timer1.Enabled = False
                '    If Not IsNothing(OpenFileDialog1.FileName) Then
                '        ''Sandip Darade 20090323
                '        ''check if selected file  is a supported file or not
                '        Slicenumber = 1
                '        Try
                '            AxezDICOMX1.DCMfilename = OpenFileDialog1.FileName
                '        Catch COmex As COMException
                '        End Try
                '        If (AxezDICOMX1.get_DCMfilenameSilentErrors(OpenFileDialog1.FileName) <= 0) Then
                '            'save the dicom file
                '            SaveDicomFileOpened(OpenFileDialog1.FileName)
                '            ''Sandip Darade 20090320
                '            If (c1DicomFiles.Rows.Count > 1) Then
                '                tlsbtn_Delete.Visible = True
                '            Else
                '                tlsbtn_Delete.Visible = False
                '                AxezDICOMX1.DCMunloadImages = True
                '            End If
                '            Reset_DICOMIDstring()
                '        End If
                '    End If
                '    Timer1.Enabled = True
                '    If _CallingForm <> "" Then
                '        _CallingForm = ""
                '        'Me.Close() Sandip Darade 20090314
                '        Exit Sub
                '    End If
                'Else
                '    If _CallingForm <> "" Then
                '        _CallingForm = ""

                '        If _CallingForm = "Radiology Orders" Then
                '            frm_LM_Orders.LMDICOMID = 0
                '        End If
                '        'Me.Close()
                '    End If
                'End If
                ''getLatestVideoFile()

                '''''''End of Original Flow '''''''

            Case "Next"
                Timer1.Enabled = False

                If c1DicomFiles.Rows.Count > 1 Then '  If c1DicomFiles.Rows.Count > 0 Then


                    If Not IsNothing(trvFiles.SelectedNode) Then

                        If Not IsNothing(trvFiles.SelectedNode.NextNode) Then
                            trvFiles.SelectedNode = trvFiles.SelectedNode.NextNode
                            AxezDICOMX1.DCMfilename = trvFiles.SelectedNode.Tag
                            Slicenumber = 1
                        End If
                    End If
                    If c1DicomFiles.Row <> c1DicomFiles.Rows.Count - 1 Then
                        Dim DICOMFileName As String = ""
                        c1DicomFiles.Row += 1
                        ''Sandip Darade 20090321
                        ''Get DICOM ID 
                        Dim DICOMID As Int64 = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_DOCUMENTID), Int64)    'ProblemID  
                        ''Get DICOM File extnsion 
                        Dim strfileExtn As String = getfileextn(DICOMID)
                        ''Add file extension to the file name
                        DICOMFileName = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_FILENAME), String) & strfileExtn
                        'DICOMFileName = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_FILENAME), String)    'ProblemID  
                        LoadDicomFile(DICOMFileName)
                    End If

                End If

                Timer1.Enabled = True
            Case "Prev"
                Timer1.Enabled = False
                If Not IsNothing(trvFiles.SelectedNode) Then
                    If Not IsNothing(trvFiles.SelectedNode.PrevNode) Then
                        trvFiles.SelectedNode = trvFiles.SelectedNode.PrevNode
                        AxezDICOMX1.DCMfilename = trvFiles.SelectedNode.Tag
                        Slicenumber = 1
                    End If
                End If
                If c1DicomFiles.Row > 1 Then    ' If c1DicomFiles.Row > 0 Then ' <> c1DicomFiles.Rows.Count - 1 Then
                    Dim DICOMFileName As String = ""
                    c1DicomFiles.Row -= 1
                    ''Sandip Darade 20090321
                    ''Get DICOM ID 
                    Dim DICOMID As Int64 = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_DOCUMENTID), Int64)    'ProblemID  
                    ''Get DICOM File extnsion 
                    Dim strfileExtn As String = getfileextn(DICOMID)
                    ''Add file extension to the file name
                    DICOMFileName = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_FILENAME), String) & strfileExtn
                    'DICOMFileName = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_FILENAME), String)    'ProblemID  
                    LoadDicomFile(DICOMFileName)
                End If

                Timer1.Enabled = True
            Case "Close"
                If (IsForTest = True) Then
                    PassDICOMIDS()
                End If
                Me.Close()
            Case "Save"
                SaveDicomFile()
            Case "Copy"
                Dim blnClipflag As Boolean
                blnClipflag = AxezDICOMX1.DCMcopyImage2Clipboard()
                'If blnClipflag = True Then
                '    MsgBox("The image is copied to clipboard")
                'End If
            Case "Refresh"
                If AxezDICOMX1.DCMimageSlices > 0 Then
                    AxezDICOMX1.set_DCMmosaicX(1, 2, 1, 1)
                End If
                getLatestVideoFile()
            Case "AddNotes"
                Dim DICOMID As Int64 = 0
                Dim objDICOM As New clsDICOM
                Dim DICOMNotesID As Int64 = 0
                If c1DicomFiles.Row >= 0 Then
                    Dim DICOMFileName As String = ""

                    DICOMID = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_DOCUMENTID), Int64)    'ProblemID  
                    DICOMFileName = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_FILENAME), String)    'ProblemID  
                    DICOMNotesID = 0
                    'DICOMNotesID = objDICOM.GetAckID(DICOMID, 3)
                    Dim ofrmAddDICOMNotes As New frmAddDicomNotes(DICOMID, DICOMNotesID, _LMPatientID)
                    ofrmAddDICOMNotes.ShowDialog(IIf(IsNothing(ofrmAddDICOMNotes.Parent), Me, ofrmAddDICOMNotes.Parent))
                    ofrmAddDICOMNotes.Dispose()
                End If
                getLatestVideoFile()
            Case "Acknowledge"
                Dim DICOMID As Int64 = 0
                Dim DICOMAckID As Int64 = 0
                Dim objRvwDICOM As New clsDICOM

                If c1DicomFiles.Row >= 0 Then
                    Dim DICOMFileName As String = ""

                    DICOMID = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_DOCUMENTID), Int64)    'ProblemID  
                    DICOMFileName = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_FILENAME), String)    'ProblemID  
                    DICOMAckID = objRvwDICOM.GetAckID(DICOMID, 1)
                    Dim ofrm As New frmAddDicom_Acknowledgement(DICOMID, DICOMAckID, True, _LMPatientID)
                    With ofrm
                        .Text = "Acknowledge"
                        .ShowInTaskbar = False
                        .StartPosition = FormStartPosition.CenterParent
                        .ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                        .Dispose()
                    End With

                End If
                objRvwDICOM = Nothing
                getLatestVideoFile()
            Case "Review"
                Dim DICOMID As Int64 = 0
                Dim DICOMDetID As Int64 = 0
                Dim objRvwDICOM As New clsDICOM

                If c1DicomFiles.Row >= 0 Then
                    Dim DICOMFileName As String = ""

                    DICOMID = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_DOCUMENTID), Int64)    'ProblemID  
                    DICOMFileName = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_FILENAME), String)    'ProblemID 

                    DICOMDetID = objRvwDICOM.GetAckID(DICOMID, 2)
                    Dim ofrm As New frmAddDicom_Acknowledgement(DICOMID, DICOMDetID, False, _LMPatientID)
                    With ofrm
                        .Text = "Review"
                        .ShowInTaskbar = False
                        .StartPosition = FormStartPosition.CenterParent
                        .ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                        .Dispose()
                    End With
                End If
                getLatestVideoFile()
                objRvwDICOM = Nothing
            Case "Amendments"
                Dim DICOMID As Int64 = 0
                Dim DICOMDetID As Int64 = 0
                Dim objRvwDICOM As New clsDICOM

                If c1DicomFiles.Row >= 0 Then

                    DICOMID = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_DOCUMENTID), Int64)    'ProblemID  

                    Dim ofrm As New frmViewDICOMAmendments(DICOMID)
                    With ofrm
                        .ShowInTaskbar = False
                        .StartPosition = FormStartPosition.CenterParent
                        .ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                        .Dispose()
                    End With
                End If
                objRvwDICOM = Nothing
            Case "Delete"
                Delete_DICOMFILE()


        End Select
    End Sub

    Private Sub btnBrowsePath_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsePath.MouseHover
        btnBrowsePath.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnBrowsePath.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnBrowsePath_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsePath.MouseLeave
        btnBrowsePath.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnBrowsePath.BackgroundImageLayout = ImageLayout.Stretch
    End Sub


#Region "When opened from other forms besides dashboard"
    Public Function ImportDicomFile() As Long
        Dim DICOMID As Long = 0
        Try
            getLatestVideoFile()
            DICOMID = _LMDICOMID
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return DICOMID
    End Function

#End Region

    Private Sub c1DicomFiles_AfterAddRow(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1DicomFiles1.AfterAddRow, c1DicomFiles.AfterAddRow

    End Sub


    Private Sub c1DicomFiles_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1DicomFiles1.MouseDown, c1DicomFiles.MouseDown
        Try

            Dim DICOMFileName As String = ""
            Dim DICOMID As Int64 = 0
            Dim dicomfileextn As String = ""
            Dim Img As Image

            If c1DicomFiles.Row >= 0 Then
                DICOMID = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_DOCUMENTID), Int64)    'ProblemID  
                DICOMFileName = CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_FILENAME), String)    'ProblemID  
                dicomfileextn = getfileextn(CType(c1DicomFiles.GetData(c1DicomFiles.Row, COL_DOCUMENTID), Int64))
                'Sandip Darade  20090806
                If (Convert.ToString(dicomfileextn).ToUpper <> Convert.ToString(".dcm").ToUpper And dicomfileextn <> "") Then
                    tlsbtnCopy.Visible = False
                Else ''If file is a DICOM file 
                    tlsbtnCopy.Visible = True
                End If

                LoadDicomFile(DICOMFileName & dicomfileextn)
                Img = c1DicomFiles.GetCellImage(c1DicomFiles.Row, COL_FLAG_ACKNOWLEDGE)
                If IsNothing(Img) Then
                    tlsbtnAcknowledge.Visible = True
                    tlsbtnReview.Visible = False
                Else
                    tlsbtnReview.Visible = True
                    tlsbtnAcknowledge.Visible = False
                End If

            End If

            ''20120718
            ''Capture right click on c1grid to rename DICOM file name
            Try
                If e.Button = MouseButtons.Right Then
                    Dim oMenuItem As ToolStripMenuItem
                    '  Dim oChildItem As ToolStripMenuItem
                    '  Dim oMenuItemSeperator As ToolStripSeparator
                    'Try
                    '    If (IsNothing(c1DicomFiles.ContextMenuStrip) = False) Then
                    '        c1DicomFiles.ContextMenuStrip.Dispose()
                    '        c1DicomFiles.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    c1DicomFiles.ContextMenuStrip = Nothing
                    ContextMenuStrip1.Items.Clear()
                    Dim _nRow As Integer = c1DicomFiles.HitTest(e.X, e.Y).Row
                    If _nRow >= 0 Then
                        c1DicomFiles.Row = _nRow
                        oMenuItem = New ToolStripMenuItem
                        oMenuItem.Text = "Rename"
                        oMenuItem.Tag = "Rename"
                        oMenuItem.ForeColor = Color.FromArgb(31, 73, 125)
                        oMenuItem.Image = ImageList1.Images(8)
                        ContextMenuStrip1.Items.Add(oMenuItem)
                        c1SelectedRowIndex = c1DicomFiles.HitTest(e.X, e.Y).Row
                        c1SelectedDicomFile = CType(c1DicomFiles.GetData(c1SelectedRowIndex, COL_FILENAME), String)
                        c1SelectedDicomExt = dicomfileextn
                        AddHandler oMenuItem.Click, AddressOf RenameDicomFile
                    End If
                    'Try
                    '    If (IsNothing(c1DicomFiles.ContextMenuStrip) = False) Then
                    '        c1DicomFiles.ContextMenuStrip.Dispose()
                    '        c1DicomFiles.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    c1DicomFiles.ContextMenuStrip = ContextMenuStrip1
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try

            'c1DicomFiles.[Select](c1Documents.HitTest(e.X, e.Y).Row, COL_DOCUMENTNAME)
            'If e.Button = MouseButtons.Right AndAlso _IsDocumentsLoading = False Then
            '    Dim _rowIndex As Integer = c1Documents.RowSel
            '    'c1Documents.HitTest(e.X, e.Y).Row; 
            '    If _rowIndex >= 0 Then
            '        c1Documents.[Select](_rowIndex, 1)
            '        If c1Documents.GetData(_rowIndex, COL_COLTYPE) IsNot Nothing Then
            '            If DirectCast(c1Documents.GetData(_rowIndex, COL_COLTYPE), enum_DocumentColumnType) = enum_DocumentColumnType.Document Then
            '                If lvwPages.Items.Count > 0 AndAlso lvwPages.SelectedItems.Count = 0 Then
            '                    lvwPages.Items(0).Selected = True
            '                End If
            '                FillContextMenu(_PatientID, gloEDocV3Admin.gClinicID, _SelectedYear, False)
            '            Else
            '                c1Documents.ContextMenuStrip = Nothing
            '            End If
            '        End If
            '    Else
            '        c1Documents.ContextMenuStrip = Nothing
            '    End If
            'End If
        Catch ex As Exception
            ' UnloadDocuments(); 
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Finally
        End Try
    End Sub

    ''20120718
    ''Rename Dicom File name from c1grid on right click
    Public Sub RenameDicomFile()

        Try
            Dim message, title, defaultValue As String
            Dim myValue As Object
            message = "Enter new name for the selected DICOM file"   ' Set prompt.
            title = "Rename DICOM File"   ' Set title.
            defaultValue = c1SelectedDicomFile    ' Set default value.
            If File.Exists(DICOMPath & "\" & _LMPatientID & "\" & defaultValue & c1SelectedDicomExt) Then
                ' Display message, title, and default value.
                myValue = InputBox(message, title, defaultValue, 250, 250)
                myValue = myValue.ToString().Trim()
                If myValue <> "" Then   'Name Must Not be Blank
                    If defaultValue <> myValue Then  'Name Must be Unique
                        If myValue.ToString().Length <= 50 Then  'Name length should be less than equals to 50
                            Dim oDicom As New clsDICOM()
                            If oDicom.CheckValidFileName(myValue) Then 'Check valid File Name
                                Dim srcfilepath As String = DICOMPath & "\" & _LMPatientID & "\" & defaultValue & c1SelectedDicomExt
                                Dim destfilepath As String = DICOMPath & "\" & _LMPatientID & "\" & myValue & c1SelectedDicomExt
                                Dim dInfo As New DirectoryInfo(DICOMPath & "\" & _LMPatientID)
                                Dim dfI() As FileInfo = dInfo.GetFiles()
                                For dCounter As Integer = 0 To dfI.Length - 1
                                    Dim dfName As String = dfI(dCounter).ToString()
                                    Dim strArr() As String = dfName.Split(".")
                                    If strArr(0).ToUpper() = myValue.ToString().ToUpper() Then
                                        MessageBox.Show("File with the name '" + myValue + "' is already present", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    End If
                                Next

                                If File.Exists(destfilepath) Then
                                    MessageBox.Show("File with the name '" + destfilepath + "' is already present", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                Else
                                    File.Copy(srcfilepath, destfilepath)
                                    Try
                                        '20120727::Bug no 32016
                                        File.Delete(srcfilepath)
                                        Dim DICOMid As Int64 = Convert.ToInt64(c1DicomFiles.GetData(c1DicomFiles.RowSel, COL_DOCUMENTID))
                                        oDicom.UpdateDICOMFile(DICOMid, _LMPatientID, myValue)
                                    Catch exa As System.UnauthorizedAccessException
                                        MessageBox.Show("This file cannot be renamed as access to the file is denied.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Dim attribute As FileAttributes = FileAttributes.Normal
                                        System.IO.File.SetAttributes(destfilepath, attribute)
                                        File.Delete(destfilepath)
                                    End Try
                                    frmgloDICOM_Load(Nothing, Nothing)
                                End If
                            End If
                            oDicom = Nothing
                        Else
                            MessageBox.Show("File name having more that 50 characters cannot be saved. Following file name needs to be changed before saving" + Environment.NewLine + "'" + myValue + "'", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            End If
        Catch exa As System.UnauthorizedAccessException
            MessageBox.Show("This file cannot be renamed as access to the file is denied.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Function getfileextn(ByVal _DICOMID As Int64) As String

        Dim objDICOM As New clsDICOM
        Dim str As String = ""
        Try
            str = objDICOM.getFileExtn(_DICOMID)
        Catch ex As Exception
            Throw ex
        End Try

        Return str
    End Function

    ''Sandip Darade 20090314
    Private Sub FillDICOMFilesToGrid(ByVal MachineID As Long, ByVal DicomID As Long, ByVal nPatientID As Long, ByVal DICOMFileName As String, ByVal DICOMFileExtn As String)
        Dim dtDicom As New DataTable
        Dim objDICOM As New clsDICOM

        Try
            Dim strFilePath As String = ""
            Dim NewRow As Integer

            'If (c1DicomFiles.Rows.Count > 1) Then
            '    For i As Integer = 1 To c1DicomFiles.Rows.Count - 1 '   For i As Integer = 0 To c1DicomFiles.Rows.Count - 1 column header is added so 0 change to 1
            '        If (DICOMFileName = Convert.ToString(c1DicomFiles.GetData(i, COL_FILENAME))) Then
            '            c1DicomFiles.Refresh()
            '            Exit Sub
            '        End If
            '    Next
            'End If

            strFilePath = DICOMPath & "\" & nPatientID & "\" & DICOMFileName & DICOMFileExtn
            If File.Exists(strFilePath) Then
                ' Dim blnaddrow As Boolean = True
                'If Not IsNothing(c1DicomFiles.GetData(0, COL_DOCUMENTID).ToString()) Then
                'If (c1DicomFiles.GetData(0, COL_DOCUMENTID) = -1) Then
                'blnaddrow = False
                'End If
                'End If
                With c1DicomFiles
                    'If blnaddrow = True Then
                    .Rows.Add()
                    'End If
                    NewRow = .Rows.Count - 1
                    .SetData(NewRow, COL_DOCUMENTID, DicomID)
                    .SetData(NewRow, COL_FILENAME, DICOMFileName)
                    .SetData(NewRow, COL_ImportDateTime, Format(Date.Now, "MM/dd/yyyy hh:mm:ss tt"))
                    Dim AckID As Long = 0
                    Dim NotesID As Long = 0
                    AckID = objDICOM.GetAckID(DicomID, 1)
                    If AckID <> 0 Then
                        .SetCellImage(NewRow, COL_FLAG_ACKNOWLEDGE, Global.gloEMR.My.Resources.Flag_Yellow)
                    End If
                    NotesID = objDICOM.GetAckID(DicomID, 3)
                    If NotesID <> 0 Then
                        .SetCellImage(NewRow, COL_FLAG_NOTES, Global.gloEMR.My.Resources.Custom)
                    End If
                    'Sandip Darade  20090806
                    'Dim FileExtn As String = getfileextn(CType(c1DicomFiles.GetData(NewRow, COL_DOCUMENTID), Int64))
                    Dim FileExtn As String = DICOMFileExtn
                    c1DicomFiles.Row = NewRow
                    If (Convert.ToString(FileExtn).ToUpper <> Convert.ToString(".dcm").ToUpper And FileExtn <> "") Then
                        tlsbtnCopy.Visible = False
                    Else ''If file is a DICOM file 
                        tlsbtnCopy.Visible = True
                    End If
                End With
            End If

            If c1DicomFiles.Rows.Count > 1 Then '  If c1DicomFiles.Rows.Count > 0 Then
                ' c1DicomFiles.Row = 1
                c1DicomFiles.Select()
            End If


            '  Dim oFileNode As TreeNode
            Dim oDirectory As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(strpath)
            Dim oFile As System.IO.FileInfo

            For Each oFile In oDirectory.GetFiles()

            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Sub
    ''Sandip Darade - to pass back the DICOMIDs to Test- Order  
    Private Sub PassDICOMIDS()
        Try
            If (IsForTest = True) Then
                If (c1DicomFiles.Rows.Count > 1) Then ' If (c1DicomFiles.Rows.Count > 0) Then
                    For i As Integer = 1 To c1DicomFiles.Rows.Count - 1  ' 0 to 1
                        If (i = 1) Then ' 0 to 1
                            frm_LM_Orders.sDICOMIDs = Convert.ToString(c1DicomFiles.GetData(i, COL_DOCUMENTID))
                        Else
                            frm_LM_Orders.sDICOMIDs &= "," & Convert.ToString(c1DicomFiles.GetData(i, COL_DOCUMENTID))
                        End If
                    Next
                Else
                    frm_LM_Orders.sDICOMIDs = ""
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Throw ex
        End Try
    End Sub

    Private Sub Delete_DICOMFILE()
        Try

            Dim DICOMid As Int64 = Convert.ToInt64(c1DicomFiles.GetData(c1DicomFiles.RowSel, COL_DOCUMENTID))

            Dim objDICOM As New clsDICOM

            '20120717:Newly Added Code To Delete file from physical Location
            '''''''''''''''''''''
            Dim DICOMName As String = Convert.ToString(c1DicomFiles.GetData(c1DicomFiles.RowSel, COL_FILENAME))
            Dim dResult As DialogResult = MessageBox.Show("Are you sure you want to delete the selected file?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If dResult.ToString() = "Yes" Then


                'File Deleted From Physical Location
                Dim FileExtn As String = getfileextn(DICOMid)
                Dim strFilePath As String = DICOMPath & "\" & _LMPatientID & "\" & DICOMName & FileExtn
                If File.Exists(strFilePath) Then
                    File.Delete(strFilePath)
                End If
                '''''''''''''''''''''
                'File Deleted From Database(Dicomdetails table)
                objDICOM.DeleteDICOMFile(DICOMid, _LMPatientID)
                Reset_DICOMIDstring()
                ''Sandip Darade 20090814
                ''Update DICOM ID in  table LM_Orders
                If (_CallingForm <> "Radiology Orders") Then
                    Dim odb As New gloDatabaseLayer.DBLayer(GetConnectionString())
                    odb.Connect(False)
                    Try
                        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        'odb.ExecuteScalar_Query("UPDATE  LM_Orders SET  lm_DICOMID =''WHERE lm_DICOMID = '" & Convert.ToString(DICOMid) & "'  AND lm_Patient_ID = " & gnPatientID & "   ")
                        odb.ExecuteScalar_Query("UPDATE  LM_Orders SET  lm_DICOMID =''WHERE lm_DICOMID = '" & Convert.ToString(DICOMid) & "'  AND lm_Patient_ID = " & _LMPatientID & "   ")
                        'end modification
                    Catch ex As Exception
                    End Try
                End If
                frmgloDICOM_Load(Nothing, Nothing)
            End If
        Catch exa As System.UnauthorizedAccessException
            MessageBox.Show("This file cannot be deleted as access to the file is denied.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Finally
            If (c1DicomFiles.Rows.Count > 1) Then
                tlsbtn_Delete.Visible = True
            Else
                tlsbtn_Delete.Visible = False
                ''  AxezDICOMX1.Dispose()
                'Slicenumber = 0
                AxezDICOMX1.DCMunloadImages = True
                AxezDICOMX1.DCMfilename = Nothing
                ''AxezDICOMX1.DCMimageSlices = 0
                AxezDICOMX1.Refresh()
            End If
        End Try
    End Sub
    ''Sandip Darade 20090321
    ''Reset the DICOMids string for order after adding and deleting  DICOM files
    Private Sub Reset_DICOMIDstring()
        Try

            If (c1DicomFiles.Rows.Count > 1) Then
                If (IsForTest = True) Then
                    If (c1DicomFiles.Rows.Count > 1) Then
                        For i As Integer = 1 To c1DicomFiles.Rows.Count - 1   ' 0 to 1
                            If (i = 1) Then
                                DICOMids = Convert.ToString(c1DicomFiles.GetData(i, COL_DOCUMENTID))
                            Else

                                DICOMids &= "," & Convert.ToString(c1DicomFiles.GetData(i, COL_DOCUMENTID))

                            End If
                        Next
                    Else
                        DICOMids = ""
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub c1DicomFiles_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1DicomFiles1.MouseMove, c1DicomFiles.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub


    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _LMPatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

End Class
