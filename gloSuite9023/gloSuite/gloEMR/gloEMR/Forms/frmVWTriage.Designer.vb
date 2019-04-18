<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVWTriage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Dim dtpContextMenuStrip As ContextMenuStrip() = {cntTriage}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenuStrip)
                gloGlobal.cEventHelper.DisposeAllControls(dtpContextMenuStrip)
            Catch ex As Exception

            End Try
            

        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWTriage))
        Me.pnlReceivedTriage1 = New System.Windows.Forms.Panel
        Me.C1ReceivedTriage = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.pnlTopRight = New System.Windows.Forms.Panel
        Me.lblSearch = New System.Windows.Forms.Label
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.tlsTriage = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlSentTriage1 = New System.Windows.Forms.Panel
        Me.C1SentTriage = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.pnlReceivedTriage = New System.Windows.Forms.Panel
        Me.pnlSentTriage = New System.Windows.Forms.Panel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.cntTriage = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuAddTriage = New System.Windows.Forms.ToolStripMenuItem
        Me.pnlReceivedTriage1.SuspendLayout()
        CType(Me.C1ReceivedTriage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.tlsTriage.SuspendLayout()
        Me.pnlSentTriage1.SuspendLayout()
        CType(Me.C1SentTriage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlReceivedTriage.SuspendLayout()
        Me.pnlSentTriage.SuspendLayout()
        Me.cntTriage.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlReceivedTriage1
        '
        Me.pnlReceivedTriage1.Controls.Add(Me.C1ReceivedTriage)
        Me.pnlReceivedTriage1.Controls.Add(Me.Label1)
        Me.pnlReceivedTriage1.Controls.Add(Me.Label2)
        Me.pnlReceivedTriage1.Controls.Add(Me.Label3)
        Me.pnlReceivedTriage1.Controls.Add(Me.Label4)
        Me.pnlReceivedTriage1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlReceivedTriage1.Location = New System.Drawing.Point(0, 30)
        Me.pnlReceivedTriage1.Name = "pnlReceivedTriage1"
        Me.pnlReceivedTriage1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlReceivedTriage1.Size = New System.Drawing.Size(407, 575)
        Me.pnlReceivedTriage1.TabIndex = 13
        '
        'C1ReceivedTriage
        '
        Me.C1ReceivedTriage.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1ReceivedTriage.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1ReceivedTriage.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1ReceivedTriage.ContextMenuStrip = Me.cntTriage
        Me.C1ReceivedTriage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1ReceivedTriage.ExtendLastCol = True
        Me.C1ReceivedTriage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1ReceivedTriage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1ReceivedTriage.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1ReceivedTriage.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1ReceivedTriage.Location = New System.Drawing.Point(4, 1)
        Me.C1ReceivedTriage.Name = "C1ReceivedTriage"
        Me.C1ReceivedTriage.Rows.Count = 5
        Me.C1ReceivedTriage.Rows.DefaultSize = 19
        Me.C1ReceivedTriage.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1ReceivedTriage.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1ReceivedTriage.Size = New System.Drawing.Size(402, 570)
        Me.C1ReceivedTriage.StyleInfo = resources.GetString("C1ReceivedTriage.StyleInfo")
        Me.C1ReceivedTriage.TabIndex = 32
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 571)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(402, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 571)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(406, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 571)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(404, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlTopRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(407, 30)
        Me.Panel2.TabIndex = 14
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_RightBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_TopBrd)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(404, 24)
        Me.pnlTopRight.TabIndex = 0
        '
        'lblSearch
        '
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(402, 22)
        Me.lblSearch.TabIndex = 1
        Me.lblSearch.Text = "  Received Triage"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(402, 1)
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
        Me.lbl_RightBrd.Location = New System.Drawing.Point(403, 1)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(404, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.tlsTriage)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(837, 53)
        Me.pnlToolStrip.TabIndex = 15
        '
        'tlsTriage
        '
        Me.tlsTriage.BackColor = System.Drawing.Color.Transparent
        Me.tlsTriage.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsTriage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsTriage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlsTriage.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsTriage.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsTriage.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsTriage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.tlsTriage.Location = New System.Drawing.Point(0, 0)
        Me.tlsTriage.Name = "tlsTriage"
        Me.tlsTriage.Size = New System.Drawing.Size(837, 53)
        Me.tlsTriage.TabIndex = 0
        Me.tlsTriage.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(37, 50)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&New"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 50)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlSentTriage1
        '
        Me.pnlSentTriage1.Controls.Add(Me.C1SentTriage)
        Me.pnlSentTriage1.Controls.Add(Me.Label5)
        Me.pnlSentTriage1.Controls.Add(Me.Label6)
        Me.pnlSentTriage1.Controls.Add(Me.Label7)
        Me.pnlSentTriage1.Controls.Add(Me.Label8)
        Me.pnlSentTriage1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSentTriage1.Location = New System.Drawing.Point(0, 30)
        Me.pnlSentTriage1.Name = "pnlSentTriage1"
        Me.pnlSentTriage1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlSentTriage1.Size = New System.Drawing.Size(427, 575)
        Me.pnlSentTriage1.TabIndex = 16
        '
        'C1SentTriage
        '
        Me.C1SentTriage.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1SentTriage.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1SentTriage.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1SentTriage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1SentTriage.ExtendLastCol = True
        Me.C1SentTriage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1SentTriage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1SentTriage.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1SentTriage.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1SentTriage.Location = New System.Drawing.Point(1, 1)
        Me.C1SentTriage.Name = "C1SentTriage"
        Me.C1SentTriage.Rows.Count = 5
        Me.C1SentTriage.Rows.DefaultSize = 19
        Me.C1SentTriage.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.C1SentTriage.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1SentTriage.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1SentTriage.Size = New System.Drawing.Size(422, 570)
        Me.C1SentTriage.StyleInfo = resources.GetString("C1SentTriage.StyleInfo")
        Me.C1SentTriage.TabIndex = 32
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 571)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(422, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 571)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(423, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 571)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(424, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(427, 30)
        Me.Panel1.TabIndex = 14
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(424, 24)
        Me.Panel3.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(1, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(422, 22)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "  Sent Triage"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(1, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(422, 1)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 23)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(423, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 23)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(424, 1)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "label1"
        '
        'pnlReceivedTriage
        '
        Me.pnlReceivedTriage.Controls.Add(Me.pnlReceivedTriage1)
        Me.pnlReceivedTriage.Controls.Add(Me.Panel2)
        Me.pnlReceivedTriage.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlReceivedTriage.Location = New System.Drawing.Point(0, 53)
        Me.pnlReceivedTriage.Name = "pnlReceivedTriage"
        Me.pnlReceivedTriage.Size = New System.Drawing.Size(407, 605)
        Me.pnlReceivedTriage.TabIndex = 17
        '
        'pnlSentTriage
        '
        Me.pnlSentTriage.Controls.Add(Me.pnlSentTriage1)
        Me.pnlSentTriage.Controls.Add(Me.Panel1)
        Me.pnlSentTriage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSentTriage.Location = New System.Drawing.Point(410, 53)
        Me.pnlSentTriage.Name = "pnlSentTriage"
        Me.pnlSentTriage.Size = New System.Drawing.Size(427, 605)
        Me.pnlSentTriage.TabIndex = 18
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(407, 53)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 605)
        Me.Splitter1.TabIndex = 19
        Me.Splitter1.TabStop = False
        '
        'cntTriage
        '
        Me.cntTriage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuAddTriage})
        Me.cntTriage.Name = "cntTriage"
        Me.cntTriage.Size = New System.Drawing.Size(138, 26)
        '
        'cmnuAddTriage
        '
        Me.cmnuAddTriage.Name = "cmnuAddTriage"
        Me.cmnuAddTriage.Size = New System.Drawing.Size(137, 22)
        Me.cmnuAddTriage.Text = "Add Triage"
        '
        'frmVWTriage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(837, 658)
        Me.Controls.Add(Me.pnlSentTriage)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlReceivedTriage)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWTriage"
        Me.Text = "View Triage"
        Me.pnlReceivedTriage1.ResumeLayout(False)
        CType(Me.C1ReceivedTriage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tlsTriage.ResumeLayout(False)
        Me.tlsTriage.PerformLayout()
        Me.pnlSentTriage1.ResumeLayout(False)
        CType(Me.C1SentTriage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlReceivedTriage.ResumeLayout(False)
        Me.pnlSentTriage.ResumeLayout(False)
        Me.cntTriage.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlReceivedTriage1 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tlsTriage As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlSentTriage1 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents C1ReceivedTriage As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1SentTriage As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pnlReceivedTriage As System.Windows.Forms.Panel
    Friend WithEvents pnlSentTriage As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents cntTriage As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmnuAddTriage As System.Windows.Forms.ToolStripMenuItem
End Class
