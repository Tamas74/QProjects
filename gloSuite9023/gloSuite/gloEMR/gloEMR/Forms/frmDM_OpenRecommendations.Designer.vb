<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDM_OpenRecommendations
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_OpenRecommendations))
        Me.tlsPatientDM = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_GoToPatient = New System.Windows.Forms.ToolStripButton()
        Me.tls_Refresh_old = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnExportToExcel = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.c1OpenRecommendations = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnlProgressIndication = New System.Windows.Forms.Panel()
        Me.label42 = New System.Windows.Forms.Label()
        Me.picProgressIndication = New System.Windows.Forms.PictureBox()
        Me.label86 = New System.Windows.Forms.Label()
        Me.label94 = New System.Windows.Forms.Label()
        Me.label93 = New System.Windows.Forms.Label()
        Me.label92 = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbProvider = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbRules = New System.Windows.Forms.ComboBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.bgWorkerOpenRecommendations = New System.ComponentModel.BackgroundWorker()
        Me.tlsPatientDM.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        CType(Me.c1OpenRecommendations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlProgressIndication.SuspendLayout()
        CType(Me.picProgressIndication, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlsPatientDM
        '
        Me.tlsPatientDM.BackColor = System.Drawing.Color.Transparent
        Me.tlsPatientDM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsPatientDM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsPatientDM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsPatientDM.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsPatientDM.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsPatientDM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_GoToPatient, Me.tls_Refresh_old, Me.ts_btnRefresh, Me.tls_btnExportToExcel, Me.ts_btnClose})
        Me.tlsPatientDM.Location = New System.Drawing.Point(0, 0)
        Me.tlsPatientDM.Name = "tlsPatientDM"
        Me.tlsPatientDM.Size = New System.Drawing.Size(1002, 53)
        Me.tlsPatientDM.TabIndex = 0
        Me.tlsPatientDM.Text = "ToolStrip1"
        '
        'ts_GoToPatient
        '
        Me.ts_GoToPatient.Image = CType(resources.GetObject("ts_GoToPatient.Image"), System.Drawing.Image)
        Me.ts_GoToPatient.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_GoToPatient.Name = "ts_GoToPatient"
        Me.ts_GoToPatient.Size = New System.Drawing.Size(91, 50)
        Me.ts_GoToPatient.Tag = "GoToPatient"
        Me.ts_GoToPatient.Text = "&GoTo Patient"
        Me.ts_GoToPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tls_Refresh_old
        '
        Me.tls_Refresh_old.Image = CType(resources.GetObject("tls_Refresh_old.Image"), System.Drawing.Image)
        Me.tls_Refresh_old.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Refresh_old.Name = "tls_Refresh_old"
        Me.tls_Refresh_old.Size = New System.Drawing.Size(58, 50)
        Me.tls_Refresh_old.Tag = "Refresh"
        Me.tls_Refresh_old.Text = "&Refresh"
        Me.tls_Refresh_old.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_Refresh_old.ToolTipText = "Refresh"
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(175, 50)
        Me.ts_btnRefresh.Tag = "Generate"
        Me.ts_btnRefresh.Text = "&Refresh Recommendations"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnRefresh.ToolTipText = "Refresh recommendations for all patients"
        Me.ts_btnRefresh.Visible = False
        '
        'tls_btnExportToExcel
        '
        Me.tls_btnExportToExcel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tls_btnExportToExcel.Image = CType(resources.GetObject("tls_btnExportToExcel.Image"), System.Drawing.Image)
        Me.tls_btnExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnExportToExcel.Name = "tls_btnExportToExcel"
        Me.tls_btnExportToExcel.Size = New System.Drawing.Size(105, 50)
        Me.tls_btnExportToExcel.Tag = "Export"
        Me.tls_btnExportToExcel.Text = "Export To Excel"
        Me.tls_btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.tlsPatientDM)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1002, 54)
        Me.pnlToolStrip.TabIndex = 15
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_Base.Controls.Add(Me.c1OpenRecommendations)
        Me.pnl_Base.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_Base.Controls.Add(Me.pnlProgressIndication)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 84)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnl_Base.Size = New System.Drawing.Size(1002, 662)
        Me.pnl_Base.TabIndex = 120
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 658)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(994, 1)
        Me.lbl_BottomBrd.TabIndex = 4
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 658)
        Me.lbl_LeftBrd.TabIndex = 3
        Me.lbl_LeftBrd.Text = "label4"
        '
        'c1OpenRecommendations
        '
        Me.c1OpenRecommendations.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1OpenRecommendations.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1OpenRecommendations.ColumnInfo = resources.GetString("c1OpenRecommendations.ColumnInfo")
        Me.c1OpenRecommendations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1OpenRecommendations.ExtendLastCol = True
        Me.c1OpenRecommendations.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1OpenRecommendations.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1OpenRecommendations.Location = New System.Drawing.Point(3, 1)
        Me.c1OpenRecommendations.Name = "c1OpenRecommendations"
        Me.c1OpenRecommendations.Rows.Count = 1
        Me.c1OpenRecommendations.Rows.DefaultSize = 19
        Me.c1OpenRecommendations.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1OpenRecommendations.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1OpenRecommendations.Size = New System.Drawing.Size(995, 658)
        Me.c1OpenRecommendations.StyleInfo = resources.GetString("c1OpenRecommendations.StyleInfo")
        Me.c1OpenRecommendations.TabIndex = 0
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(998, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 658)
        Me.lbl_RightBrd.TabIndex = 2
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(996, 1)
        Me.lbl_TopBrd.TabIndex = 0
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlProgressIndication
        '
        Me.pnlProgressIndication.BackColor = System.Drawing.Color.White
        Me.pnlProgressIndication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProgressIndication.Controls.Add(Me.label42)
        Me.pnlProgressIndication.Controls.Add(Me.picProgressIndication)
        Me.pnlProgressIndication.Controls.Add(Me.label86)
        Me.pnlProgressIndication.Controls.Add(Me.label94)
        Me.pnlProgressIndication.Controls.Add(Me.label93)
        Me.pnlProgressIndication.Controls.Add(Me.label92)
        Me.pnlProgressIndication.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlProgressIndication.Location = New System.Drawing.Point(3, 0)
        Me.pnlProgressIndication.Name = "pnlProgressIndication"
        Me.pnlProgressIndication.Size = New System.Drawing.Size(996, 659)
        Me.pnlProgressIndication.TabIndex = 14
        '
        'label42
        '
        Me.label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label42.Dock = System.Windows.Forms.DockStyle.Top
        Me.label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label42.Location = New System.Drawing.Point(1, 0)
        Me.label42.Name = "label42"
        Me.label42.Size = New System.Drawing.Size(994, 1)
        Me.label42.TabIndex = 67
        Me.label42.Text = "label1"
        '
        'picProgressIndication
        '
        Me.picProgressIndication.Location = New System.Drawing.Point(539, 340)
        Me.picProgressIndication.Name = "picProgressIndication"
        Me.picProgressIndication.Size = New System.Drawing.Size(73, 71)
        Me.picProgressIndication.TabIndex = 0
        Me.picProgressIndication.TabStop = False
        '
        'label86
        '
        Me.label86.AutoSize = True
        Me.label86.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label86.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.label86.Location = New System.Drawing.Point(501, 410)
        Me.label86.Name = "label86"
        Me.label86.Size = New System.Drawing.Size(148, 29)
        Me.label86.TabIndex = 66
        Me.label86.Text = "Please Wait..."
        '
        'label94
        '
        Me.label94.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label94.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label94.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label94.Location = New System.Drawing.Point(1, 658)
        Me.label94.Name = "label94"
        Me.label94.Size = New System.Drawing.Size(994, 1)
        Me.label94.TabIndex = 65
        Me.label94.Text = "label1"
        '
        'label93
        '
        Me.label93.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label93.Dock = System.Windows.Forms.DockStyle.Right
        Me.label93.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label93.Location = New System.Drawing.Point(995, 0)
        Me.label93.Name = "label93"
        Me.label93.Size = New System.Drawing.Size(1, 659)
        Me.label93.TabIndex = 64
        Me.label93.Text = "label1"
        '
        'label92
        '
        Me.label92.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label92.Dock = System.Windows.Forms.DockStyle.Left
        Me.label92.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label92.Location = New System.Drawing.Point(0, 0)
        Me.label92.Name = "label92"
        Me.label92.Size = New System.Drawing.Size(1, 659)
        Me.label92.TabIndex = 63
        Me.label92.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.Controls.Add(Me.pnlTopRight)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 54)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlSearch.Size = New System.Drawing.Size(1002, 30)
        Me.pnlSearch.TabIndex = 121
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.panel4)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.CmbProvider)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.cmbRules)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label5)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label8)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(996, 24)
        Me.pnlTopRight.TabIndex = 1
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.White
        Me.panel4.Controls.Add(Me.txtSearch)
        Me.panel4.Controls.Add(Me.label20)
        Me.panel4.Controls.Add(Me.label21)
        Me.panel4.Controls.Add(Me.btnClear)
        Me.panel4.Controls.Add(Me.label22)
        Me.panel4.Controls.Add(Me.label23)
        Me.panel4.Controls.Add(Me.label24)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel4.ForeColor = System.Drawing.Color.Black
        Me.panel4.Location = New System.Drawing.Point(65, 1)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(241, 22)
        Me.panel4.TabIndex = 122
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtSearch.TabIndex = 0
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.White
        Me.label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label20.Location = New System.Drawing.Point(5, 17)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(214, 5)
        Me.label20.TabIndex = 43
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.White
        Me.label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.label21.Location = New System.Drawing.Point(5, 0)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(214, 3)
        Me.label21.TabIndex = 37
        '
        'btnClear
        '
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(219, 0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 46
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'label22
        '
        Me.label22.BackColor = System.Drawing.Color.White
        Me.label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.label22.Location = New System.Drawing.Point(1, 0)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(4, 22)
        Me.label22.TabIndex = 38
        '
        'label23
        '
        Me.label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.label23.Location = New System.Drawing.Point(0, 0)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(1, 22)
        Me.label23.TabIndex = 39
        Me.label23.Text = "label4"
        '
        'label24
        '
        Me.label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.label24.Location = New System.Drawing.Point(240, 0)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(1, 22)
        Me.label24.TabIndex = 40
        Me.label24.Text = "label4"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(233, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label3.Size = New System.Drawing.Size(63, 20)
        Me.Label3.TabIndex = 66
        Me.Label3.Text = "Provider :"
        '
        'CmbProvider
        '
        Me.CmbProvider.Dock = System.Windows.Forms.DockStyle.Right
        Me.CmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbProvider.FormattingEnabled = True
        Me.CmbProvider.Location = New System.Drawing.Point(296, 1)
        Me.CmbProvider.Name = "CmbProvider"
        Me.CmbProvider.Size = New System.Drawing.Size(200, 22)
        Me.CmbProvider.TabIndex = 65
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(496, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label2.Size = New System.Drawing.Size(149, 20)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "  Recommendation Rule :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbRules
        '
        Me.cmbRules.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmbRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRules.FormattingEnabled = True
        Me.cmbRules.Location = New System.Drawing.Point(645, 1)
        Me.cmbRules.Name = "cmbRules"
        Me.cmbRules.Size = New System.Drawing.Size(350, 22)
        Me.cmbRules.TabIndex = 49
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(64, 22)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "  Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(994, 1)
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
        Me.Label6.Size = New System.Drawing.Size(1, 23)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(995, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 23)
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
        Me.Label8.Size = New System.Drawing.Size(996, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'bgWorkerOpenRecommendations
        '
        '
        'frmDM_OpenRecommendations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1002, 746)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDM_OpenRecommendations"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Open Recommendations"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tlsPatientDM.ResumeLayout(False)
        Me.tlsPatientDM.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        CType(Me.c1OpenRecommendations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlProgressIndication.ResumeLayout(False)
        Me.pnlProgressIndication.PerformLayout()
        CType(Me.picProgressIndication, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlsPatientDM As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_GoToPatient As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Friend WithEvents c1OpenRecommendations As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents bgWorkerOpenRecommendations As System.ComponentModel.BackgroundWorker
    Private WithEvents pnlProgressIndication As System.Windows.Forms.Panel
    Private WithEvents label42 As System.Windows.Forms.Label
    Private WithEvents picProgressIndication As System.Windows.Forms.PictureBox
    Private WithEvents label86 As System.Windows.Forms.Label
    Private WithEvents label94 As System.Windows.Forms.Label
    Private WithEvents label93 As System.Windows.Forms.Label
    Private WithEvents label92 As System.Windows.Forms.Label
    Friend WithEvents tls_Refresh_old As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbRules As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents tls_btnExportToExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents CmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
End Class
