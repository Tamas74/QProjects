Imports System.IO
Public Class frmPendingFAXWithOutTIFF
    Inherits System.Windows.Forms.Form

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
            If Not (components Is Nothing) Then
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
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuRefresh As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSelectAll As System.Windows.Forms.MenuItem
    Friend WithEvents mnuClearAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tlbPendingFAXes As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnClearAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents flxPendingFAXes As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPendingFAXWithOutTIFF))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.flxPendingFAXes = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.mnuRefresh = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mnuSelectAll = New System.Windows.Forms.MenuItem
        Me.mnuClearAll = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.mnuDelete = New System.Windows.Forms.MenuItem
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.tlbPendingFAXes = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlbbtnSelectAll = New System.Windows.Forms.ToolStripButton
        Me.tlbbtnClearAll = New System.Windows.Forms.ToolStripButton
        Me.tlbbtnPrint = New System.Windows.Forms.ToolStripButton
        Me.tlbbtnExport = New System.Windows.Forms.ToolStripButton
        Me.tlbbtnDelete = New System.Windows.Forms.ToolStripButton
        Me.tlbbtnRefresh = New System.Windows.Forms.ToolStripButton
        Me.tlbbtnClose = New System.Windows.Forms.ToolStripButton
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.flxPendingFAXes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.tlbPendingFAXes.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Panel3)
        Me.pnlMain.Controls.Add(Me.pnlToolStrip)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(688, 464)
        Me.pnlMain.TabIndex = 3
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel3.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel3.Controls.Add(Me.lbl_RightBrd)
        Me.Panel3.Controls.Add(Me.lbl_TopBrd)
        Me.Panel3.Controls.Add(Me.flxPendingFAXes)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 54)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel3.Size = New System.Drawing.Size(688, 410)
        Me.Panel3.TabIndex = 3
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 406)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(680, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 403)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(684, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 403)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(682, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'flxPendingFAXes
        '
        Me.flxPendingFAXes.BackColor = System.Drawing.Color.GhostWhite
        Me.flxPendingFAXes.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.flxPendingFAXes.ColumnInfo = "12,0,0,0,0,95,Columns:0{Style:""DataType:System.Boolean;ImageAlign:CenterCenter;"";" & _
            "}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{StyleFixed:""TextAlign:CenterCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.flxPendingFAXes.ContextMenu = Me.ContextMenu1
        Me.flxPendingFAXes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxPendingFAXes.Location = New System.Drawing.Point(3, 3)
        Me.flxPendingFAXes.Name = "flxPendingFAXes"
        Me.flxPendingFAXes.Rows.DefaultSize = 19
        Me.flxPendingFAXes.Size = New System.Drawing.Size(682, 404)
        Me.flxPendingFAXes.StyleInfo = resources.GetString("flxPendingFAXes.StyleInfo")
        Me.flxPendingFAXes.TabIndex = 1
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRefresh, Me.MenuItem2, Me.mnuSelectAll, Me.mnuClearAll, Me.MenuItem5, Me.mnuDelete})
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
        'mnuDelete
        '
        Me.mnuDelete.Index = 5
        Me.mnuDelete.Text = "Delete"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.tlbPendingFAXes)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(688, 54)
        Me.pnlToolStrip.TabIndex = 2
        '
        'tlbPendingFAXes
        '
        Me.tlbPendingFAXes.BackColor = System.Drawing.Color.Transparent
        Me.tlbPendingFAXes.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlbPendingFAXes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbPendingFAXes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbPendingFAXes.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlbPendingFAXes.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlbPendingFAXes.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtnSelectAll, Me.tlbbtnClearAll, Me.tlbbtnPrint, Me.tlbbtnExport, Me.tlbbtnDelete, Me.tlbbtnRefresh, Me.tlbbtnClose})
        Me.tlbPendingFAXes.Location = New System.Drawing.Point(0, 0)
        Me.tlbPendingFAXes.Name = "tlbPendingFAXes"
        Me.tlbPendingFAXes.Size = New System.Drawing.Size(688, 53)
        Me.tlbPendingFAXes.TabIndex = 0
        Me.tlbPendingFAXes.Text = "ToolStrip1"
        '
        'tlbbtnSelectAll
        '
        Me.tlbbtnSelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.tlbbtnClearAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnClearAll.Image = CType(resources.GetObject("tlbbtnClearAll.Image"), System.Drawing.Image)
        Me.tlbbtnClearAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnClearAll.Name = "tlbbtnClearAll"
        Me.tlbbtnClearAll.Size = New System.Drawing.Size(60, 50)
        Me.tlbbtnClearAll.Tag = "ClearAll"
        Me.tlbbtnClearAll.Text = "&Clear All"
        Me.tlbbtnClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtnPrint
        '
        Me.tlbbtnPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnPrint.Image = CType(resources.GetObject("tlbbtnPrint.Image"), System.Drawing.Image)
        Me.tlbbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnPrint.Name = "tlbbtnPrint"
        Me.tlbbtnPrint.Size = New System.Drawing.Size(41, 50)
        Me.tlbbtnPrint.Tag = "Print"
        Me.tlbbtnPrint.Text = "&Print"
        Me.tlbbtnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnPrint.ToolTipText = "Print  "
        '
        'tlbbtnExport
        '
        Me.tlbbtnExport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.tlbbtnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.tlbbtnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.tlbbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnClose.Image = CType(resources.GetObject("tlbbtnClose.Image"), System.Drawing.Image)
        Me.tlbbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnClose.Name = "tlbbtnClose"
        Me.tlbbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtnClose.Tag = "Close"
        Me.tlbbtnClose.Text = "&Close"
        Me.tlbbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnClose.ToolTipText = "Close  "
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmPendingFAXWithOutTIFF
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(688, 464)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPendingFAXWithOutTIFF"
        Me.Text = "Pending Faxes without TIFF"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.flxPendingFAXes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tlbPendingFAXes.ResumeLayout(False)
        Me.tlbPendingFAXes.PerformLayout()
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
    Dim nFaxID As Int64 = 0



    Private Sub frmPendingFAXWithOutTIFF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tlbbtnClearAll.Visible = False
        gloC1FlexStyle.Style(flxPendingFAXes)

        Try
            Me.Cursor = Cursors.WaitCursor
            Call Fill_PendingFAXesWithOutTIFFFiles()
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Fill_PendingFAXesWithOutTIFFFiles()
        'Retrieve all Pending FAXes and store it in Table
        Dim dtFiles As New DataTable
        Dim objFAX As New clsFAX
        'Retrieveing all pending FAXes
        '' dtFiles = objFAX.Fill_PendingFAXes()
        dtFiles = objFAX.Fill_RetrieveAllPendingFaxesWithoutBinary()
        objFAX = Nothing

        'Check TIFF File is generated or not
        'If the TIFF File is already generated then delete the pending Fax from table.....as we required only those pending faxes whose TIFF File has not been generated
        Dim nCount As Int16

        Try


            'sarika  Pending Faxes without Tiff 20090609
            'Now faxes which are sent for efax will not be displayed as they will never contain a tiff file

            'For nCount = dtFiles.Rows.Count - 1 To 0 Step -1
            '    ' If File.Exists(gstrFAXOutputDirectory & "\" & dtFiles.Rows(nCount).Item("FAXFileName") & ".tif" Or dtFiles.Rows(nCount).Item("FAXFileName") = "") = True Then
            '    'sarika delete sent fax 20090428
            '    If File.Exists(gstrFAXOutputDirectory & "\" & dtFiles.Rows(nCount).Item("FAXFileName") & ".tif") = True Then
            '        dtFiles.Rows.RemoveAt(nCount)
            '    End If

            'Next


            For nCount = dtFiles.Rows.Count - 1 To 0 Step -1
                ' If File.Exists(gstrFAXOutputDirectory & "\" & dtFiles.Rows(nCount).Item("FAXFileName") & ".tif" Or dtFiles.Rows(nCount).Item("FAXFileName") = "") = True Then
                'sarika delete sent fax 20090428
                ' If dtFiles.Rows(nCount).Item("FaxFileBinaryData") Is Nothing Or dtFiles.Rows(nCount).Item("FaxFileBinaryData") = "" Then
                If File.Exists(gstrFAXOutputDirectory & "\" & dtFiles.Rows(nCount).Item("FAXFileName") & ".tif") = True Then
                    dtFiles.Rows.RemoveAt(nCount)
                End If
                'Else
                'dtFiles.Rows.RemoveAt(nCount)
                'End If

                '----

            Next
            'Bind the Pending FAXes with Flex Grid
            'flxPendingFAXes.DataSource = dtFiles

            With flxPendingFAXes
                .Rows.Count = 1
                .Rows.Fixed = 1

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


                .Cols(COL_SELECTION).Width = 50
                .Cols(COL_FAXID).Width = 0
                .Cols(COL_PATID).Width = 0
                .Cols(COL_FILENAME).Width = 0
                .Cols(COL_STATUS).Width = 0
                .Cols(COL_PATNAME).Width = 150
                .Cols(COL_FAXTO).Width = 150
                .Cols(COL_FAXTYPE).Width = 150
                .Cols(COL_FAXNO).Width = 150
                .Cols(COL_SENDBY).Width = 100
                .Cols(COL_FAXDATE).Width = 150
                .Cols(COL_NOOFATTEMPTS).Width = 100

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
                Next

                .Cols(COL_SELECTION).AllowEditing = True
                'commented by Mayuri:20091024
                'To allow user to select Individual Patient 
                'For col As Integer = 0 To .Cols.Count - 1
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

    'Private Sub tlbPendingFAXes_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tlbPendingFAXes1.ButtonClick
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        'When the user will click on toolbar button
    '        Select Case Trim(e.Button.Tag)
    '            Case "Refresh"
    '                'Refresh Button
    '                Call Fill_PendingFAXesWithOutTIFFFiles()
    '                Exit Sub
    '            Case "SelectAll"
    '                'Select All Button
    '                Call SelectClearAll(True)
    '            Case "ClearAll"
    '                'Clear All button
    '                Call SelectClearAll(False)
    '            Case "Delete"
    '                'Delete Button
    '                Call DeletePendingFAXes()
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
    End Sub
    Private Sub DeletePendingFAXes()
        Dim nCount As Int16
        'variable added by dipak 20091111 to set flag for to show message or not to fix bug no 4735	Reports->Fax->Pending faxes without TIFF

        Dim isShowMessage As Boolean
        'Check any row is available or not
        If flxPendingFAXes.Rows.Count <= 1 Then Exit Sub
        For nCount = 1 To flxPendingFAXes.Rows.Count - 1
            If CBool(flxPendingFAXes.GetData(nCount, COL_SELECTION)) = True Then
                'set to true if atleast 1 record  is selected selected
                isShowMessage = True
                Exit For
            End If
        Next
        If (isShowMessage = False) Then Exit Sub 'exit if no record selected
        If MessageBox.Show("Are you sure, you want to delete all selected FAXes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
        'Check any row is slected or not
        Dim strFileName As String
        Dim objFAX As New clsFAX

        Try

            For nCount = 1 To flxPendingFAXes.Rows.Count - 1
                If CBool(flxPendingFAXes.GetData(nCount, COL_SELECTION)) = True Then
                    strFileName = flxPendingFAXes.Rows(nCount).Item(COL_FILENAME)
                    'Code Added by Mayuri:20091024
                    'To get ID of selected Patient to delete 
                    nFaxID = flxPendingFAXes.GetData(nCount, COL_FAXID)
                    If File.Exists(gstrFAXOutputDirectory & "\" & strFileName & ".tif") = False Then
                        'Commented by Mayuri:20091024
                        'objFAX.DeletePendingFAX(strFileName)
                        'Added by Mayuri:20091024
                        'To delete record of selected fax ID
                        objFAX.DeletePendingFAX_ID(nFaxID)
                        'End Code Added by Mayuri:20091024
                    End If
                End If
            Next
            objFAX = Nothing
            Call Fill_PendingFAXesWithOutTIFFFiles()


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

    Private Sub mnuRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'To refill Pending FAXes without TIFF Files
            Call Fill_PendingFAXesWithOutTIFFFiles()
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mnuSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelectAll.Click
        Try
            'Select all Pending FAXes
            Call SelectClearAll(True)
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClearAll.Click
        Try
            'Unselect all Pending FAXes
            Call SelectClearAll(False)
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

    Private Sub ExportReport()
        Try
            If flxPendingFAXes.Rows.Count <= 1 Then Exit Sub
            If MessageBox.Show("Are you sure, you want to export the report?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                With SaveFileDialog1
                    .Filter = "Excel File(*.xls)|*.xls|Text File (*.txt)|*.txt"
                    .OverwritePrompt = True
                    .ShowHelp = False
                    .Title = "Select export location"
                End With
                If SaveFileDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    If SaveFileDialog1.FileName.ToLower.EndsWith(".xls") = True Then
                        flxPendingFAXes.Cols(COL_SELECTION).Visible = False
                        flxPendingFAXes.Cols(COL_FAXDATE).DataType = GetType(DateTime)
                        flxPendingFAXes.Cols(COL_FAXDATE).Format = "g"

                        flxPendingFAXes.SaveGrid(SaveFileDialog1.FileName, C1.Win.C1FlexGrid.FileFormatEnum.Excel, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
                    ElseIf SaveFileDialog1.FileName.ToLower.EndsWith(".txt") = True Then
                        flxPendingFAXes.SaveGrid(SaveFileDialog1.FileName, C1.Win.C1FlexGrid.FileFormatEnum.TextTab, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
                    End If
                End If
            End If
        Catch ex As Exception

            If ex.ToString().Contains("Failed to create storage file") Then
                MessageBox.Show("Unable to Export the report as file is already open. Close the file to continue.", "FAX", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Unable to export the data due to " & ex.Message, "FAX", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.ExportToExcel, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)


            '  MessageBox.Show("Unable to export the data due to " & ex.Message, "Fax", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            flxPendingFAXes.Cols(COL_SELECTION).Visible = True
        End Try
    End Sub
    Private Sub PrintReport()
        Try
            If flxPendingFAXes.Rows.Count <= 1 Then Exit Sub
            flxPendingFAXes.PrintParameters.PrintDocument.DefaultPageSettings.Margins.Left = 5
            flxPendingFAXes.PrintParameters.PrintDocument.DefaultPageSettings.Landscape = True
            flxPendingFAXes.PrintParameters.PrintPreviewDialog.WindowState = FormWindowState.Maximized
            flxPendingFAXes.PrintParameters.PrintPreviewDialog.Text = "Fax Report"
            Dim strHeader As String
            strHeader = "Pending FAXes Without TIFF"
            With flxPendingFAXes
                .Cols(COL_SELECTION).Width = 0
                .Cols(COL_PATNAME).Width = 150
                .Cols(COL_FAXTO).Width = 150
                .Cols(COL_FAXTYPE).Width = 275
                .Cols(COL_FAXNO).Width = 125
                .Cols(COL_SENDBY).Width = 100
                .Cols(COL_FAXDATE).Width = 150
                .Cols(COL_NOOFATTEMPTS).Width = 0
            End With

            'Sandip :: 11 Nov 2014 : changes done for checking if use Default Printer is ON/OFF in gloEMR Application
            If (gblnUseDefaultPrinter) Then
                flxPendingFAXes.PrintGrid("Fax Report", C1.Win.C1FlexGrid.PrintGridFlags.ShowPreviewDialog, vbTab & strHeader, vbTab & "{0} of {1}")
            Else
                'flxPendingFAXes.PrintGrid("Fax Report", C1.Win.C1FlexGrid.PrintGridFlags.ShowPageSetupDialog, vbTab & strHeader, vbTab & "{0} of {1}")
                flxPendingFAXes.PrintGrid("Fax Report", C1.Win.C1FlexGrid.PrintGridFlags.ShowPrintDialog, vbTab & strHeader, vbTab & "{0} of {1}")
            End If
            '**Sandip :: 11 Nov 2014 : changes done for checking if use Default Printer is ON/OFF in gloEMR Application

            With flxPendingFAXes
                .Cols(COL_SELECTION).Width = 50
                .Cols(COL_FAXID).Width = 0
                .Cols(COL_PATID).Width = 0
                .Cols(COL_FILENAME).Width = 0
                .Cols(COL_STATUS).Width = 0
                .Cols(COL_PATNAME).Width = 150
                .Cols(COL_FAXTO).Width = 150
                .Cols(COL_FAXTYPE).Width = 150
                .Cols(COL_FAXNO).Width = 150
                .Cols(COL_SENDBY).Width = 100
                .Cols(COL_FAXDATE).Width = 150
                .Cols(COL_NOOFATTEMPTS).Width = 100
            End With
        Catch ex As Exception


            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show("Unable to export the data due to " & ex.Message, "Fax", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlbPendingFAXes_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlbPendingFAXes.ItemClicked
        Try
            Me.Cursor = Cursors.WaitCursor
            'When the user will click on toolbar button
            Select Case Trim(e.ClickedItem.Tag)
                Case "Refresh"
                    'Refresh Button
                    Call Fill_PendingFAXesWithOutTIFFFiles()
                    Exit Sub
                Case "SelectAll"
                    'Select All Button
                    Call SelectClearAll(True)
                    'Code Added by Mayuri:20091024
                    tlbbtnSelectAll.Visible = False
                    tlbbtnClearAll.Visible = True
                Case "ClearAll"
                    'Clear All button
                    Call SelectClearAll(False)
                    tlbbtnSelectAll.Visible = True
                    tlbbtnClearAll.Visible = False
                Case "Delete"
                    'Delete Button
                    Call DeletePendingFAXes()
                Case "Export"
                    Call ExportReport()
                    Exit Sub
                Case "Print"
                    Call PrintReport()
                    Exit Sub
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
End Class
