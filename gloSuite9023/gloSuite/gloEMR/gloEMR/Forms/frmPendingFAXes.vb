Imports System.IO
Public Class frmPendingFAXes
    Inherits System.Windows.Forms.Form
    Dim _PatientID As Long
#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.

        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                Try
                    If (IsNothing(dtTo) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtTo)
                        Catch ex As Exception

                        End Try


                        dtTo.Dispose()
                        dtTo = Nothing
                    End If
                Catch
                End Try

                Try
                    If (IsNothing(dtFrom) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtFrom)
                        Catch ex As Exception

                        End Try


                        dtFrom.Dispose()
                        dtFrom = Nothing
                    End If
                Catch
                End Try

                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Try
                    If IsNothing(ContextMenu1) = False Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(ContextMenu1)
                        If (IsNothing(ContextMenu1.MenuItems) = False) Then
                            ContextMenu1.MenuItems.Clear()
                        End If
                        ContextMenu1.Dispose()
                        ContextMenu1 = Nothing
                    End If
                Catch ex As Exception

                End Try
                components.Dispose()
                Try
                    If (IsNothing(SaveFileDialog1) = False) Then
                        SaveFileDialog1.Dispose()
                        SaveFileDialog1 = Nothing
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
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuRefresh As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSelectAll As System.Windows.Forms.MenuItem
    Friend WithEvents mnuClearAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents mnuReSendFAXWithDifferentFAXNo As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReSendFAX As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents flxPendingFAXes As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tblbtnfax As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnClearAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnResendFax As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents tlbbtnPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftTopTop As System.Windows.Forms.Panel
    Private WithEvents cmbFaxStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents numTopRecords As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPendingFAXes))
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.mnuRefresh = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.mnuSelectAll = New System.Windows.Forms.MenuItem()
        Me.mnuClearAll = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.mnuReSendFAX = New System.Windows.Forms.MenuItem()
        Me.mnuReSendFAXWithDifferentFAXNo = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.mnuDelete = New System.Windows.Forms.MenuItem()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.flxPendingFAXes = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblbtnfax = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtnSelectAll = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnClearAll = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnResendFax = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnPreview = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnExport = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.pnlLeftTopTop = New System.Windows.Forms.Panel()
        Me.numTopRecords = New System.Windows.Forms.NumericUpDown()
        Me.cmbFaxStatus = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Label29 = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        CType(Me.flxPendingFAXes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.tblbtnfax.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlLeftTopTop.SuspendLayout()
        CType(Me.numTopRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRefresh, Me.MenuItem2, Me.mnuSelectAll, Me.mnuClearAll, Me.MenuItem5, Me.mnuReSendFAX, Me.mnuReSendFAXWithDifferentFAXNo, Me.MenuItem1, Me.mnuDelete})
        '
        'mnuRefresh
        '
        Me.mnuRefresh.Index = 0
        Me.mnuRefresh.Text = "Refresh"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "-"
        '
        'mnuSelectAll
        '
        Me.mnuSelectAll.Index = 2
        Me.mnuSelectAll.Text = "Select All"
        '
        'mnuClearAll
        '
        Me.mnuClearAll.Index = 3
        Me.mnuClearAll.Text = "Clear All"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 4
        Me.MenuItem5.Text = "-"
        '
        'mnuReSendFAX
        '
        Me.mnuReSendFAX.Index = 5
        Me.mnuReSendFAX.Text = "Re-send selected faxes"
        '
        'mnuReSendFAXWithDifferentFAXNo
        '
        Me.mnuReSendFAXWithDifferentFAXNo.Index = 6
        Me.mnuReSendFAXWithDifferentFAXNo.Text = "Re-Send fax with different fax no"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 7
        Me.MenuItem1.Text = "-"
        '
        'mnuDelete
        '
        Me.mnuDelete.Index = 8
        Me.mnuDelete.Text = "Delete selected fax"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.flxPendingFAXes)
        Me.pnlMain.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlMain.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlMain.Controls.Add(Me.lbl_RightBrd)
        Me.pnlMain.Controls.Add(Me.lbl_TopBrd)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 83)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(829, 443)
        Me.pnlMain.TabIndex = 4
        '
        'flxPendingFAXes
        '
        Me.flxPendingFAXes.BackColor = System.Drawing.Color.GhostWhite
        Me.flxPendingFAXes.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.flxPendingFAXes.ColumnInfo = "13,0,0,0,0,105,Columns:0{Style:""DataType:System.Boolean;ImageAlign:CenterCenter;""" & _
    ";}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{StyleFixed:""TextAlign:CenterCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.flxPendingFAXes.ContextMenu = Me.ContextMenu1
        Me.flxPendingFAXes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxPendingFAXes.Location = New System.Drawing.Point(4, 4)
        Me.flxPendingFAXes.Name = "flxPendingFAXes"
        Me.flxPendingFAXes.Rows.DefaultSize = 21
        Me.flxPendingFAXes.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.flxPendingFAXes.Size = New System.Drawing.Size(821, 435)
        Me.flxPendingFAXes.StyleInfo = resources.GetString("flxPendingFAXes.StyleInfo")
        Me.flxPendingFAXes.TabIndex = 2
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 439)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(821, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 436)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(825, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 436)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(823, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.tblbtnfax)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(829, 56)
        Me.pnlToolStrip.TabIndex = 6
        '
        'tblbtnfax
        '
        Me.tblbtnfax.BackColor = System.Drawing.Color.Transparent
        Me.tblbtnfax.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblbtnfax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtnfax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtnfax.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblbtnfax.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblbtnfax.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtnSelectAll, Me.tlbbtnClearAll, Me.tlbbtnResendFax, Me.tlbbtnPreview, Me.tlbbtnPrint, Me.tlbbtnExport, Me.tlbbtnDelete, Me.tlbbtnRefresh, Me.tlbbtnClose})
        Me.tblbtnfax.Location = New System.Drawing.Point(0, 0)
        Me.tblbtnfax.Name = "tblbtnfax"
        Me.tblbtnfax.Size = New System.Drawing.Size(829, 53)
        Me.tblbtnfax.TabIndex = 0
        Me.tblbtnfax.Text = "ToolStrip1"
        '
        'tlbbtnSelectAll
        '
        Me.tlbbtnSelectAll.Image = CType(resources.GetObject("tlbbtnSelectAll.Image"), System.Drawing.Image)
        Me.tlbbtnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnSelectAll.Name = "tlbbtnSelectAll"
        Me.tlbbtnSelectAll.Size = New System.Drawing.Size(67, 50)
        Me.tlbbtnSelectAll.Tag = "SelectAll"
        Me.tlbbtnSelectAll.Text = "&Select All"
        Me.tlbbtnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtnClearAll
        '
        Me.tlbbtnClearAll.Image = CType(resources.GetObject("tlbbtnClearAll.Image"), System.Drawing.Image)
        Me.tlbbtnClearAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnClearAll.Name = "tlbbtnClearAll"
        Me.tlbbtnClearAll.Size = New System.Drawing.Size(60, 50)
        Me.tlbbtnClearAll.Tag = "ClearAll"
        Me.tlbbtnClearAll.Text = "&Clear All"
        Me.tlbbtnClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnClearAll.Visible = False
        '
        'tlbbtnResendFax
        '
        Me.tlbbtnResendFax.Image = CType(resources.GetObject("tlbbtnResendFax.Image"), System.Drawing.Image)
        Me.tlbbtnResendFax.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnResendFax.Name = "tlbbtnResendFax"
        Me.tlbbtnResendFax.Size = New System.Drawing.Size(100, 50)
        Me.tlbbtnResendFax.Tag = "ReSendFAX"
        Me.tlbbtnResendFax.Text = "&Re-Send Faxes"
        Me.tlbbtnResendFax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtnPreview
        '
        Me.tlbbtnPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnPreview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtnPreview.Image = CType(resources.GetObject("tlbbtnPreview.Image"), System.Drawing.Image)
        Me.tlbbtnPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnPreview.Name = "tlbbtnPreview"
        Me.tlbbtnPreview.Size = New System.Drawing.Size(59, 50)
        Me.tlbbtnPreview.Tag = "Preview"
        Me.tlbbtnPreview.Text = "Preview"
        Me.tlbbtnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtnPrint
        '
        Me.tlbbtnPrint.Image = CType(resources.GetObject("tlbbtnPrint.Image"), System.Drawing.Image)
        Me.tlbbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnPrint.Name = "tlbbtnPrint"
        Me.tlbbtnPrint.Size = New System.Drawing.Size(41, 50)
        Me.tlbbtnPrint.Tag = "Print"
        Me.tlbbtnPrint.Text = "&Print"
        Me.tlbbtnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtnExport
        '
        Me.tlbbtnExport.Image = CType(resources.GetObject("tlbbtnExport.Image"), System.Drawing.Image)
        Me.tlbbtnExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnExport.Name = "tlbbtnExport"
        Me.tlbbtnExport.Size = New System.Drawing.Size(52, 50)
        Me.tlbbtnExport.Tag = "Export"
        Me.tlbbtnExport.Text = "&Export"
        Me.tlbbtnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtnDelete
        '
        Me.tlbbtnDelete.Image = CType(resources.GetObject("tlbbtnDelete.Image"), System.Drawing.Image)
        Me.tlbbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnDelete.Name = "tlbbtnDelete"
        Me.tlbbtnDelete.Size = New System.Drawing.Size(50, 50)
        Me.tlbbtnDelete.Tag = "Delete"
        Me.tlbbtnDelete.Text = "&Delete"
        Me.tlbbtnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtnRefresh
        '
        Me.tlbbtnRefresh.Image = CType(resources.GetObject("tlbbtnRefresh.Image"), System.Drawing.Image)
        Me.tlbbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnRefresh.Name = "tlbbtnRefresh"
        Me.tlbbtnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.tlbbtnRefresh.Tag = "Refresh"
        Me.tlbbtnRefresh.Text = "&Refresh"
        Me.tlbbtnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtnClose
        '
        Me.tlbbtnClose.Image = CType(resources.GetObject("tlbbtnClose.Image"), System.Drawing.Image)
        Me.tlbbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnClose.Name = "tlbbtnClose"
        Me.tlbbtnClose.Size = New System.Drawing.Size(51, 50)
        Me.tlbbtnClose.Tag = "Close"
        Me.tlbbtnClose.Text = " &Close "
        Me.tlbbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnClose.ToolTipText = "Close"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.pnlLeftTopTop)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 56)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel7.Size = New System.Drawing.Size(829, 27)
        Me.Panel7.TabIndex = 11
        '
        'pnlLeftTopTop
        '
        Me.pnlLeftTopTop.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlLeftTopTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftTopTop.Controls.Add(Me.Label29)
        Me.pnlLeftTopTop.Controls.Add(Me.numTopRecords)
        Me.pnlLeftTopTop.Controls.Add(Me.cmbFaxStatus)
        Me.pnlLeftTopTop.Controls.Add(Me.Label28)
        Me.pnlLeftTopTop.Controls.Add(Me.Label26)
        Me.pnlLeftTopTop.Controls.Add(Me.dtTo)
        Me.pnlLeftTopTop.Controls.Add(Me.Label3)
        Me.pnlLeftTopTop.Controls.Add(Me.dtFrom)
        Me.pnlLeftTopTop.Controls.Add(Me.Label2)
        Me.pnlLeftTopTop.Controls.Add(Me.Label17)
        Me.pnlLeftTopTop.Controls.Add(Me.Label18)
        Me.pnlLeftTopTop.Controls.Add(Me.Label19)
        Me.pnlLeftTopTop.Controls.Add(Me.Label20)
        Me.pnlLeftTopTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTopTop.Location = New System.Drawing.Point(3, 0)
        Me.pnlLeftTopTop.Name = "pnlLeftTopTop"
        Me.pnlLeftTopTop.Size = New System.Drawing.Size(823, 24)
        Me.pnlLeftTopTop.TabIndex = 0
        '
        'numTopRecords
        '
        Me.numTopRecords.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numTopRecords.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numTopRecords.Location = New System.Drawing.Point(748, 1)
        Me.numTopRecords.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.numTopRecords.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numTopRecords.Name = "numTopRecords"
        Me.numTopRecords.Size = New System.Drawing.Size(66, 22)
        Me.numTopRecords.TabIndex = 56
        Me.numTopRecords.Tag = "Queue"
        Me.numTopRecords.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numTopRecords.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'cmbFaxStatus
        '
        Me.cmbFaxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFaxStatus.FormattingEnabled = True
        Me.cmbFaxStatus.Location = New System.Drawing.Point(441, 1)
        Me.cmbFaxStatus.Name = "cmbFaxStatus"
        Me.cmbFaxStatus.Size = New System.Drawing.Size(141, 22)
        Me.cmbFaxStatus.TabIndex = 55
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Location = New System.Drawing.Point(384, 5)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(56, 14)
        Me.Label28.TabIndex = 54
        Me.Label28.Text = "Status :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(358, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(19, 22)
        Me.Label26.TabIndex = 9
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtTo
        '
        Me.dtTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTo.CustomFormat = "MM/dd/yyyy"
        Me.dtTo.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(257, 1)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(101, 22)
        Me.dtTo.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(177, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 22)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "          To "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtFrom
        '
        Me.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(75, 1)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(102, 22)
        Me.dtFrom.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 22)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "From "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Location = New System.Drawing.Point(1, 23)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(821, 1)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Location = New System.Drawing.Point(0, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 23)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Location = New System.Drawing.Point(822, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 23)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "label3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(823, 1)
        Me.Label20.TabIndex = 5
        Me.Label20.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'Label29
        '
        Me.Label29.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Location = New System.Drawing.Point(623, 5)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(122, 14)
        Me.Label29.TabIndex = 59
        Me.Label29.Text = "Showing Records :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmPendingFAXes
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(829, 526)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPendingFAXes"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Failed Faxes"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        CType(Me.flxPendingFAXes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblbtnfax.ResumeLayout(False)
        Me.tblbtnfax.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.pnlLeftTopTop.ResumeLayout(False)
        Me.pnlLeftTopTop.PerformLayout()
        CType(Me.numTopRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    'Column No
    Private Const COL_SELECTION = 0
    Private Const COL_FAXID = 1
    Private Const COL_PATID = 2
    Private Const COL_PATNAME = 3
    Private Const COL_FAXTO = 4
    Private Const COL_FAXTYPE = 5
    Private Const COL_FAXNO = 6
    Private Const COL_SENDBY = 7
    Private Const COL_FAXDATE = 8
    Private Const COL_FILENAME = 9
    Private Const COL_NOOFATTEMPTS = 10
    Private Const COL_STATUS = 11
    Private Const COL_Status_Desc = 12
    Private Enum enm_Faxstatus
        All = 0
        [Error] = 1
        Failed = 2
        Cancel = 3
        Max_Attempts = 4
    End Enum
    Private Sub Fill_PendingFAXesWithMaximumNoOfAttempts()
        'Retrieve all Pending FAXes and store it in Table
        Dim dtFiles As New DataTable
        Dim objFAX As New clsFAX

        Try

            '11-Nov-14 Aniket: Resolving Bug #75873: gloEMR > Reports > Fax > Failed Faxes > Showing wrong message while try to enter 'To' date
            If (dtFrom.Value.Date > dtTo.Value.Date) = True Then
                MessageBox.Show("From date cannot be greater than to date. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            'Retrieveing all pending FAXes
            dtFiles = objFAX.Fill_PendingFAXes_Report(cmbFaxStatus.Text.ToString(), dtFrom.Value, dtTo.Value, numTopRecords.Value, 0, gnNoOfAttempts)
            objFAX = Nothing

            Dim nCount As Int16

            With flxPendingFAXes
                .Rows.Count = 1
                .Rows.Fixed = 1
                '.AllowEditing = False

                .SetData(0, COL_SELECTION, "Select")
                .SetData(0, COL_FAXID, "Fax ID")
                .SetData(0, COL_PATID, "Patient ID")
                .SetData(0, COL_PATNAME, "Patient Name")

                .SetData(0, COL_FAXTO, "Fax To")
                .SetData(0, COL_FAXTYPE, "Fax Type")
                .SetData(0, COL_FAXNO, "Fax No")
                .SetData(0, COL_SENDBY, "Sent By")
                .SetData(0, COL_FAXDATE, "Fax Date")
                .SetData(0, COL_FILENAME, "File Name")
                .SetData(0, COL_NOOFATTEMPTS, "Attempts")
                .SetData(0, COL_STATUS, "Current Status")
                .SetData(0, COL_Status_Desc, "Status Description")

                .Cols(COL_SELECTION).Width = 50
                .Cols(COL_FAXID).Width = 0
                .Cols(COL_PATID).Width = 0
                .Cols(COL_FILENAME).Width = 0
                .Cols(COL_STATUS).Width = 130
                .Cols(COL_PATNAME).Width = 150
                .Cols(COL_FAXTO).Width = 150
                .Cols(COL_FAXTYPE).Width = 150
                .Cols(COL_FAXNO).Width = 100
                .Cols(COL_SENDBY).Width = 110
                .Cols(COL_FAXDATE).Width = 130
                .Cols(COL_NOOFATTEMPTS).Width = 90
                .Cols(COL_Status_Desc).Width = 170

                .Cols(COL_SELECTION).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_FAXID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_PATID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_FILENAME).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_STATUS).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_PATNAME).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_FAXTO).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_FAXTYPE).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_FAXNO).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_SENDBY).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_FAXDATE).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_NOOFATTEMPTS).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_Status_Desc).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter



                For nCount = 0 To dtFiles.Rows.Count - 1
                    .Rows.Add()
                    .SetData(.Rows.Count - 1, COL_FAXID, dtFiles.Rows(nCount).Item("FAXID"))
                    .SetData(.Rows.Count - 1, COL_PATID, dtFiles.Rows(nCount).Item("PatientID"))
                    .SetData(.Rows.Count - 1, COL_PATNAME, dtFiles.Rows(nCount).Item("PatientName"))
                    .SetData(.Rows.Count - 1, COL_FAXTO, dtFiles.Rows(nCount).Item("FAXTo"))
                    .SetData(.Rows.Count - 1, COL_FAXTYPE, dtFiles.Rows(nCount).Item("FAXType"))
                    .SetData(.Rows.Count - 1, COL_FAXNO, dtFiles.Rows(nCount).Item("FAXNo"))
                    .SetData(.Rows.Count - 1, COL_SENDBY, dtFiles.Rows(nCount).Item("LoginUser"))
                    .SetData(.Rows.Count - 1, COL_FAXDATE, dtFiles.Rows(nCount).Item("FAXDate"))
                    .SetData(.Rows.Count - 1, COL_FILENAME, dtFiles.Rows(nCount).Item("FAXFileName"))
                    .SetData(.Rows.Count - 1, COL_NOOFATTEMPTS, dtFiles.Rows(nCount).Item("NoOfAttempts"))
                    .SetData(.Rows.Count - 1, COL_STATUS, dtFiles.Rows(nCount).Item("CurrentStatus"))
                    .SetData(.Rows.Count - 1, COL_Status_Desc, dtFiles.Rows(nCount).Item("Status_Desc"))
                Next


                .Cols(COL_SELECTION).AllowEditing = True
                For col As Integer = 1 To .Cols.Count - 1

                    .Cols(col).AllowEditing = False
                Next

                .Cols(COL_FAXID).Visible = False
                .Cols(COL_PATID).Visible = False
                .Cols(COL_FILENAME).Visible = False

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub Fill_FaxStatus()
        ' RemoveHandler cmbFaxStatus.SelectedIndexChanged, AddressOf cmbFaxStatus_SelectedIndexChanged
        'cmbFaxStatus.DataSource = System.Enum.GetValues(GetType(enm_Faxstatus))

        ' AddHandler cmbFaxStatus.SelectedIndexChanged, AddressOf cmbFaxStatus_SelectedIndexChanged

        Dim Faxstatus As enm_Faxstatus
        For Each Faxstatus In [Enum].GetValues(GetType(enm_Faxstatus))
            cmbFaxStatus.Items.Add(Replace(Faxstatus.ToString(), "_", " "))
        Next
        cmbFaxStatus.SelectedIndex = 0
    End Sub
    'Private Sub tlbPendingFAXes_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        'When the user will click on toolbar button
    '        Select Case Trim(e.Button.Tag)
    '            Case "Refresh"
    '                'Refresh Button
    '                Call Fill_PendingFAXesWithMaximumNoOfAttempts()
    '                Exit Sub
    '            Case "SelectAll"
    '                'Select All Button
    '                Call SelectClearAll(True)
    '                Exit Sub
    '            Case "ClearAll"
    '                'Clear All button
    '                Call SelectClearAll(False)
    '                Exit Sub
    '            Case "ReSendFAX"
    '                Call ResendSelectedFAXes()
    '                Exit Sub
    '            Case "Delete"
    '                'Delete Button
    '                Call DeletePendingFAXes()
    '                Exit Sub
    '            Case "Export"
    '                Call ExportReport()
    '                Exit Sub
    '            Case "Print"
    '                Call PrintReport()
    '                Exit Sub
    '            Case "Close"
    '                'Close Window
    '                Me.Close()
    '        End Select
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub SelectClearAll(ByVal blnSelect As Boolean)
        'Select or clear rows for deletion
        Dim nCount As Int16
        For nCount = 1 To flxPendingFAXes.Rows.Count - 1
            flxPendingFAXes.Rows(nCount).Item(COL_SELECTION) = blnSelect
        Next
        ''tlbbtnSelectAll.BackgroundImage = Global.gloEMR.My.Resources.Resources.Select_All1

    End Sub
    Private Sub DeletePendingFAXes()
        'Check any row is available or not

        Try


            If flxPendingFAXes.Rows.Count <= 1 Then Exit Sub

            'If MessageBox.Show("Are you sure, you want to delete all selected FAXes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
            'Check any row is slected or not
            Dim nCount As Int16
            Dim strFileName As String
            Dim objFAX As New clsFAX
            Dim nFaxID As Int64 = 0
            'sarika 20090718  
            Dim blnMsgShown As Boolean = False

            For nCount = 1 To flxPendingFAXes.Rows.Count - 1
                If CBool(flxPendingFAXes.GetData(nCount, COL_SELECTION)) = True Then
                    If blnMsgShown = False Then
                        If MessageBox.Show("Are you sure you want to delete the selected Faxes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
                        blnMsgShown = True
                    End If

                    strFileName = flxPendingFAXes.GetData(nCount, COL_FILENAME)
                    nFaxID = flxPendingFAXes.GetData(nCount, COL_FAXID)
                    'sarika Delete Sent Faxes 20090428
                    'objFAX.DeletePendingFAX(strFileName)
                    objFAX.DeletePendingFAX_ID(nFaxID)


                    'if codition comented and modified by dipak 20091023 for fix bug of pending fax not get deleted
                    'while deleting file which is not present at location fires exception and pending faxes are not get deleted.
                    'If (strFileName <> "") Then
                    If ((strFileName <> "") And (File.Exists(gstrFAXOutputDirectory & "\" & strFileName & ".tif"))) Then
                        '--
                        File.Delete(gstrFAXOutputDirectory & "\" & strFileName & ".tif")
                    Else
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Fax, " Fax file not found", gloAuditTrail.ActivityOutCome.Failure)
                        ''Added Rahul P on 20101011
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Fax, " Fax file not found", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''
                    End If

                End If
            Next
            objFAX = Nothing
            Call Fill_PendingFAXesWithMaximumNoOfAttempts()
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub mnuRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'To refill Pending FAXes without TIFF Files
            Call Fill_PendingFAXesWithMaximumNoOfAttempts()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mnuSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelectAll.Click
        Try
            'Select all Pending FAXes
            Call SelectClearAll(True)
            tlbbtnSelectAll.Visible = False
            tlbbtnClearAll.Visible = True
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClearAll.Click
        Try
            'Unselect all Pending FAXes
            Call SelectClearAll(False)
            tlbbtnSelectAll.Visible = True
            tlbbtnClearAll.Visible = False
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelete.Click
        Try
            'Delete all Selected Pending FAXes
            Call DeletePendingFAXes()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub frmPendingFAXes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(flxPendingFAXes)

        Try
            Me.Cursor = Cursors.WaitCursor
            ''  RemoveHandler numTopRecords.ValueChanged, AddressOf numTopRecords_ValueChanged
            RemoveHandler dtFrom.ValueChanged, AddressOf dtFrom_ValueChanged
            RemoveHandler dtTo.ValueChanged, AddressOf dtTo_ValueChanged

            dtFrom.Value = Get_MinDate() ''System.DateTime.Now.AddDays(-30)
            dtTo.Value = System.DateTime.Now

            AddHandler dtFrom.ValueChanged, AddressOf dtFrom_ValueChanged
            AddHandler dtTo.ValueChanged, AddressOf dtTo_ValueChanged

            RemoveHandler cmbFaxStatus.SelectedIndexChanged, AddressOf cmbFaxStatus_SelectedIndexChanged
            Fill_FaxStatus()
            AddHandler cmbFaxStatus.SelectedIndexChanged, AddressOf cmbFaxStatus_SelectedIndexChanged

            Call Fill_PendingFAXesWithMaximumNoOfAttempts()
            AddHandler numTopRecords.ValueChanged, AddressOf numTopRecords_ValueChanged
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub ResendSelectedFAXes()
        'Check any row is available or not
        If flxPendingFAXes.Rows.Count <= 1 Then Exit Sub

        'Check any row is slected or not
        Dim nCount As Int16
        'Dim strFileName As String
        Dim objFAX As New clsFAX
        Dim blnMsgShown As Boolean = False
        For nCount = 1 To flxPendingFAXes.Rows.Count - 1
            If CBool(flxPendingFAXes.GetData(nCount, COL_SELECTION)) = True Then
                If blnMsgShown = False Then
                    If MessageBox.Show("Are you sure you want to re-send all selected FAXes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
                    blnMsgShown = True
                End If
                objFAX.ReInitialisePendingFAX(flxPendingFAXes.GetData(nCount, COL_FAXID))
            End If
        Next
        objFAX = Nothing
        Call Fill_PendingFAXesWithMaximumNoOfAttempts()
    End Sub

    Private Sub mnuReSendFAX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReSendFAX.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ResendSelectedFAXes()
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub mnuReSendFAXWithDifferentFAXNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReSendFAXWithDifferentFAXNo.Click
        Try
            If flxPendingFAXes.RowSel >= 1 Then
                Dim frm As New frmResendFAXWithFAXNo
                frm.lblFAXID.Text = flxPendingFAXes.GetData(flxPendingFAXes.RowSel, COL_FAXID)
                frm.lblFAXTo.Text = flxPendingFAXes.GetData(flxPendingFAXes.RowSel, COL_FAXTO)
                frm.lblOldFAXNo.Text = flxPendingFAXes.GetData(flxPendingFAXes.RowSel, COL_FAXNO)
                If frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent)) = DialogResult.OK Then
                    Call Fill_PendingFAXesWithMaximumNoOfAttempts()
                End If
                frm.Dispose()
                frm = Nothing
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ExportReport()
        Try
            If flxPendingFAXes.Rows.Count <= 1 Then Exit Sub
            If MessageBox.Show("Are you sure you want to export the report?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                With SaveFileDialog1
                    .Filter = "Excel File(*.xls)|*.xls|Text File (*.txt)|*.txt"
                    .OverwritePrompt = True
                    .ShowHelp = False
                    .Title = "Select export location"
                End With
                If SaveFileDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    If SaveFileDialog1.FileName.ToLower.EndsWith(".xls") = True Then
                        flxPendingFAXes.Cols(0).Visible = False
                        flxPendingFAXes.SaveGrid(SaveFileDialog1.FileName, C1.Win.C1FlexGrid.FileFormatEnum.Excel, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
                    ElseIf SaveFileDialog1.FileName.ToLower.EndsWith(".txt") = True Then
                        flxPendingFAXes.Cols(0).Visible = False
                        flxPendingFAXes.SaveGrid(SaveFileDialog1.FileName, C1.Win.C1FlexGrid.FileFormatEnum.TextTab, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
                    End If
                End If
            End If
            flxPendingFAXes.Cols(0).Visible = True
        Catch ex As Exception
            If ex.ToString().Contains("Failed to create storage file") Then
                MessageBox.Show("Unable to Export the report as file is already open. Close the file to continue.", "FAX", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Unable to export the data due to " & ex.Message, "FAX", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.ExportToExcel, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            ' MessageBox.Show("Unable to export the data due to " & ex.Message, "FAX", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub PrintReport()
        Try
            If flxPendingFAXes.Rows.Count <= 1 Then Exit Sub
            flxPendingFAXes.PrintParameters.PrintDocument.DefaultPageSettings.Margins.Left = 5
            flxPendingFAXes.PrintParameters.PrintDocument.DefaultPageSettings.Landscape = True
            flxPendingFAXes.PrintParameters.PrintPreviewDialog.Text = "Failed Faxes"
            flxPendingFAXes.PrintParameters.PrintPreviewDialog.WindowState = FormWindowState.Maximized
            Dim strHeader As String
            strHeader = "Failed Faxes"
            With flxPendingFAXes
                .Cols(COL_SELECTION).Width = 0
                .Cols(COL_PATNAME).Width = 150
                .Cols(COL_FAXTO).Width = 150
                .Cols(COL_FAXTYPE).Width = 200
                .Cols(COL_FAXNO).Width = 90
                .Cols(COL_SENDBY).Width = 80
                .Cols(COL_FAXDATE).Width = 115
                .Cols(COL_STATUS).Width = 90
                .Cols(COL_NOOFATTEMPTS).Width = 0
                .Cols(COL_Status_Desc).Width = 120
            End With

            'Sandip :: 11 Nov 2014 : changes done for checking if use Default Printer is ON/OFF in gloEMR Application
            If (gblnUseDefaultPrinter) Then
                flxPendingFAXes.PrintGrid("Failed Faxes", C1.Win.C1FlexGrid.PrintGridFlags.ShowPreviewDialog, vbTab & strHeader, vbTab & "{0} of {1}")
            Else
                'flxPendingFAXes.PrintGrid("Failed Faxes", C1.Win.C1FlexGrid.PrintGridFlags.ShowPageSetupDialog, vbTab & strHeader, vbTab & "{0} of {1}")
                flxPendingFAXes.PrintGrid("Failed Faxes", C1.Win.C1FlexGrid.PrintGridFlags.ShowPrintDialog, vbTab & strHeader, vbTab & "{0} of {1}")
            End If
            '**Sandip :: 11 Nov 2014 : changes done for checking if use Default Printer is ON/OFF in gloEMR Application

            With flxPendingFAXes
                .Cols(COL_SELECTION).Width = 50
                .Cols(COL_PATNAME).Width = 150
                .Cols(COL_FAXTO).Width = 150
                .Cols(COL_FAXTYPE).Width = 150
                .Cols(COL_FAXNO).Width = 120
                .Cols(COL_SENDBY).Width = 100
                .Cols(COL_FAXDATE).Width = 150
                .Cols(COL_STATUS).Width = 130
                .Cols(COL_NOOFATTEMPTS).Width = 90
                .Cols(COL_Status_Desc).Width = 170
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show("Unable to export the data due to " & ex.Message, "FAX", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tblbtnfax_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblbtnfax.ItemClicked
        Try
            Me.Cursor = Cursors.WaitCursor
            'When the user will click on toolbar button
            Select Case Trim(e.ClickedItem.Tag)
                Case "Refresh"
                    'Refresh Button
                    Call Fill_PendingFAXesWithMaximumNoOfAttempts()
                    Exit Sub
                Case "SelectAll"
                    'Select All Button
                    Call SelectClearAll(True)
                    tlbbtnSelectAll.Visible = False
                    tlbbtnClearAll.Visible = True
                    Exit Sub
                Case "ClearAll"
                    'Clear All button
                    Call SelectClearAll(False)
                    tlbbtnSelectAll.Visible = True
                    tlbbtnClearAll.Visible = False
                    Exit Sub
                Case "ReSendFAX"
                    Call ResendSelectedFAXes()
                    Exit Sub
                Case "Delete"
                    'Delete Button
                    Call DeletePendingFAXes()
                    Exit Sub
                Case "Export"
                    Call ExportReport()
                    Exit Sub
                Case "Print"
                    Call PrintReport()
                    Exit Sub
                    'Added By Shweta 20091021
                    'To preview the fax pending fax document
                Case "Preview"
                    Call PreviewPendingFax()
                    'End 20091021
                Case "Close"
                    'Close Window
                    'Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub flxPendingFAXes_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles flxPendingFAXes.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub flxPendingFAXes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxPendingFAXes.Click

    End Sub
    'Added By Shweta 20091021
    'To Preview the pending fax
    Public Sub PreviewPendingFax()
        Dim nCount As Integer
        'Dim _FaxID As Int64
        'Dim _PatientID As Int64
        Dim _IsRowChecked As Boolean = False
        Dim sPatientID As String = ""
        Dim sFaxID As String = ""

        'To get the checked row no. & retrive the faxID and PatientID for that row
        For nCount = 1 To flxPendingFAXes.Rows.Count - 1
            If CBool(flxPendingFAXes.GetData(nCount, COL_SELECTION)) = True Then
                _IsRowChecked = True
                If _IsRowChecked = True Then
                    'To get the FaxID of selected row
                    If sFaxID = "" Then
                        sFaxID = CType(flxPendingFAXes.GetData(nCount, 1), String)
                    Else
                        sFaxID = sFaxID + "," + CType(flxPendingFAXes.GetData(nCount, 1), String)
                    End If
                    If sPatientID = "" Then
                        sPatientID = CType(flxPendingFAXes.GetData(nCount, 2), String)
                    Else

                        'To get the same patientID only once in the string 
                        Dim strPatientID As String()
                        strPatientID = sPatientID.Split(",")
                        Dim i As Integer
                        Dim bResult As Boolean = False
                        Dim bAddID As Boolean = True
                        '
                        For i = 0 To strPatientID.Length - 1
                            bResult = (strPatientID.GetValue(i) = (CType(flxPendingFAXes.GetData(nCount, 2), String)))
                            'if the patientID is present then don't add it in patintID string
                            If bResult = True Then
                                bAddID = False           'Set flag to false
                            End If
                        Next
                        'if the patientID is present then don't add it in patintID string
                        If bAddID = True Then
                            sPatientID = sPatientID + "," + CType(flxPendingFAXes.GetData(nCount, 2), String)
                        End If
                    End If
                End If
                '_IsRowChecked = True
            End If
        Next
        'If no record has selected then invoke the message to select the record for preview 
        If _IsRowChecked = False Then
            MessageBox.Show("No fax record has been selected to preview ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'Call the preview form 
        Dim frm As New frmPendingFAXPreview()
        frm.ShowPendingFAXDetails(sFaxID, sPatientID)
        frm.MdiParent = Me.ParentForm
        frm.Show()
    End Sub


    'Private Sub flxPendingFAXes_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxPendingFAXes.DoubleClick
    '    Dim nCount As Integer
    '    Dim _FaxID As Int64
    '    Dim _PatientID As Int64
    '    Dim _IsRowChecked As Boolean = False
    '    Dim sPatientID As String = ""
    '    Dim sFaxID As String = ""

    '    'To get the checked row no. & retrive the faxID and PatientID for that row
    '    For nCount = 1 To flxPendingFAXes.Rows.Count - 1
    '        If CBool(flxPendingFAXes.GetData(nCount, COL_SELECTION)) = True Then
    '            'To get the FaxID of selected row
    '            If sFaxID = "" Then
    '                sFaxID = CType(flxPendingFAXes.GetData(nCount, 1), String)
    '            Else
    '                sFaxID = sFaxID + "," + CType(flxPendingFAXes.GetData(nCount, 1), String)
    '            End If
    '            ' _FaxID = CType(flxPendingFAXes.GetData(nCount, 1), Int64)
    '            'To get the PatientID of selected row
    '            '_PatientID = CType(flxPendingFAXes.GetData(nCount, 2), Int64)
    '            If sPatientID = "" Then
    '                sPatientID = CType(flxPendingFAXes.GetData(nCount, 2), String)
    '            Else
    '                sPatientID = sPatientID + "," + CType(flxPendingFAXes.GetData(nCount, 2), String)
    '            End If
    '            'sPatientID = sPatientID + "," + CType(flxPendingFAXes.GetData(nCount, 2), String)
    '            _IsRowChecked = True
    '        End If
    '    Next

    '    If _IsRowChecked = False Then
    '        'To get the FaxID of selected row
    '        sFaxID = CType(flxPendingFAXes.GetData(flxPendingFAXes.RowSel, 1), Int64)
    '        'To get the PatientID of selected row
    '        sPatientID = CType(flxPendingFAXes.GetData(flxPendingFAXes.RowSel, 2), Int64)

    '    End If

    '    '  'To get the FaxID of selected row
    '    '  Dim _FaxID As Int64 = CType(flxPendingFAXes.GetData(flxPendingFAXes.RowSel, 1), Int64)
    '    ''To get the PatientID of selected row
    '    '  Dim _PatientID As Int64 = CType(flxPendingFAXes.GetData(flxPendingFAXes.RowSel, 2), Int64)

    '    Dim frm As New frmPendingFAXPreview()
    '    frm.ShowPendingFAXDetails(sFaxID, sPatientID)
    '    frm.MdiParent = Me.ParentForm
    '    frm.Show()
    'End Sub
    'End 20091021

    Private Sub tblbtnfax_QueryAccessibilityHelp(ByVal sender As Object, ByVal e As System.Windows.Forms.QueryAccessibilityHelpEventArgs) Handles tblbtnfax.QueryAccessibilityHelp

    End Sub

    Private Sub cmbFaxStatus_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFaxStatus.SelectedIndexChanged
        Fill_PendingFAXesWithMaximumNoOfAttempts()
    End Sub

    Private Sub dtFrom_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtFrom.ValueChanged
        Fill_PendingFAXesWithMaximumNoOfAttempts()
    End Sub

    Private Sub dtTo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtTo.ValueChanged
        Fill_PendingFAXesWithMaximumNoOfAttempts()
    End Sub

    Private Sub numTopRecords_ValueChanged(sender As System.Object, e As System.EventArgs) '' Handles numTopRecords.ValueChanged
        Fill_PendingFAXesWithMaximumNoOfAttempts()
    End Sub
    Private Function Get_MinDate() As DateTime

        Dim result As DateTime = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try
            oDB.Connect(False)

            Dim _strSqlQuery As String = "SELECT ISNULL(Min(FAXPending_MST.dtFAXDate),DATEADD ( day , -30, SYSDATETIME() )) AS MinFaxDate FROM FAXPending_MST "

            result = oDB.ExecuteScalar_Query(_strSqlQuery)
            If result.ToString() = "" Or result.ToString() = "1/1/1900 12:00:00 AM" Then
                result = System.DateTime.Now.AddDays(-30)
            End If
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return result
    End Function
End Class




