<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDM_DisplayRecommendations
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {cntMnu_RecommendationOptions}

                components.Dispose()

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
            End If

            Try
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Dispose()
                    _PatientStrip = Nothing
                End If
            Catch

            End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_DisplayRecommendations))
        Me.tlsPatientDM = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnHealthPlan = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.pnlContainer = New System.Windows.Forms.Panel()
        Me.tabCtrl_Recommendations = New System.Windows.Forms.TabControl()
        Me.tbpg_CurrentRecommendation = New System.Windows.Forms.TabPage()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.c1CurrentRecommendation = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.label49 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbpg_PastRecommendation = New System.Windows.Forms.TabPage()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.C1PastRecommendation = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.panel3 = New System.Windows.Forms.Panel()
        Me.label12 = New System.Windows.Forms.Label()
        Me.label16 = New System.Windows.Forms.Label()
        Me.label15 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlRecomendationAlert = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblLastRecomendationName = New System.Windows.Forms.Label()
        Me.lblRecomendationAlert = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cntMnu_RecommendationOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cntMnu_MarkSatisfied = New System.Windows.Forms.ToolStripMenuItem()
        Me.cntMnu_InProcess = New System.Windows.Forms.ToolStripMenuItem()
        Me.cntMnu_ViewHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.cntMnu_UpdateNote = New System.Windows.Forms.ToolStripMenuItem()
        Me.cntMnu_Snooze = New System.Windows.Forms.ToolStripMenuItem()
        Me.cntMnu_Cancel_NA = New System.Windows.Forms.ToolStripMenuItem()
        Me.cntMnu_Reopen = New System.Windows.Forms.ToolStripMenuItem()
        Me.cntMnu_QuickOrders = New System.Windows.Forms.ToolStripMenuItem()
        Me.cntMnu_ViewRefInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.tlsPatientDM.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.pnlContainer.SuspendLayout()
        Me.tabCtrl_Recommendations.SuspendLayout()
        Me.tbpg_CurrentRecommendation.SuspendLayout()
        Me.panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.c1CurrentRecommendation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel1.SuspendLayout()
        Me.tbpg_PastRecommendation.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.C1PastRecommendation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel3.SuspendLayout()
        Me.pnlRecomendationAlert.SuspendLayout()
        Me.cntMnu_RecommendationOptions.SuspendLayout()
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
        Me.tlsPatientDM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnHealthPlan, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.tlsPatientDM.Location = New System.Drawing.Point(0, 0)
        Me.tlsPatientDM.Name = "tlsPatientDM"
        Me.tlsPatientDM.Size = New System.Drawing.Size(1124, 53)
        Me.tlsPatientDM.TabIndex = 0
        Me.tlsPatientDM.Text = "ToolStrip1"
        '
        'ts_btnHealthPlan
        '
        Me.ts_btnHealthPlan.Image = CType(resources.GetObject("ts_btnHealthPlan.Image"), System.Drawing.Image)
        Me.ts_btnHealthPlan.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnHealthPlan.Name = "ts_btnHealthPlan"
        Me.ts_btnHealthPlan.Size = New System.Drawing.Size(81, 50)
        Me.ts_btnHealthPlan.Tag = "Health Plan"
        Me.ts_btnHealthPlan.Text = "&Health Plan"
        Me.ts_btnHealthPlan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
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
        Me.pnlToolStrip.Size = New System.Drawing.Size(1124, 54)
        Me.pnlToolStrip.TabIndex = 15
        '
        'pnlContainer
        '
        Me.pnlContainer.BackColor = System.Drawing.Color.White
        Me.pnlContainer.Controls.Add(Me.tabCtrl_Recommendations)
        Me.pnlContainer.Controls.Add(Me.pnlRecomendationAlert)
        Me.pnlContainer.Controls.Add(Me.Label9)
        Me.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContainer.Location = New System.Drawing.Point(0, 54)
        Me.pnlContainer.Name = "pnlContainer"
        Me.pnlContainer.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlContainer.Size = New System.Drawing.Size(1124, 588)
        Me.pnlContainer.TabIndex = 119
        '
        'tabCtrl_Recommendations
        '
        Me.tabCtrl_Recommendations.Controls.Add(Me.tbpg_CurrentRecommendation)
        Me.tabCtrl_Recommendations.Controls.Add(Me.tbpg_PastRecommendation)
        Me.tabCtrl_Recommendations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabCtrl_Recommendations.Location = New System.Drawing.Point(3, 40)
        Me.tabCtrl_Recommendations.Name = "tabCtrl_Recommendations"
        Me.tabCtrl_Recommendations.Padding = New System.Drawing.Point(9, 6)
        Me.tabCtrl_Recommendations.SelectedIndex = 0
        Me.tabCtrl_Recommendations.Size = New System.Drawing.Size(1118, 545)
        Me.tabCtrl_Recommendations.TabIndex = 143
        '
        'tbpg_CurrentRecommendation
        '
        Me.tbpg_CurrentRecommendation.Controls.Add(Me.panel2)
        Me.tbpg_CurrentRecommendation.Location = New System.Drawing.Point(4, 29)
        Me.tbpg_CurrentRecommendation.Name = "tbpg_CurrentRecommendation"
        Me.tbpg_CurrentRecommendation.Padding = New System.Windows.Forms.Padding(4)
        Me.tbpg_CurrentRecommendation.Size = New System.Drawing.Size(1110, 512)
        Me.tbpg_CurrentRecommendation.TabIndex = 0
        Me.tbpg_CurrentRecommendation.Tag = "CURRENT"
        Me.tbpg_CurrentRecommendation.Text = "Current Recommendations"
        Me.tbpg_CurrentRecommendation.UseVisualStyleBackColor = True
        '
        'panel2
        '
        Me.panel2.Controls.Add(Me.Panel5)
        Me.panel2.Controls.Add(Me.Label4)
        Me.panel2.Controls.Add(Me.panel1)
        Me.panel2.Controls.Add(Me.Label1)
        Me.panel2.Controls.Add(Me.Label2)
        Me.panel2.Controls.Add(Me.Label3)
        Me.panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel2.Location = New System.Drawing.Point(4, 4)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(1102, 504)
        Me.panel2.TabIndex = 119
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.c1CurrentRecommendation)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(1, 27)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(15)
        Me.Panel5.Size = New System.Drawing.Size(1100, 476)
        Me.Panel5.TabIndex = 147
        '
        'c1CurrentRecommendation
        '
        Me.c1CurrentRecommendation.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1CurrentRecommendation.AllowEditing = False
        Me.c1CurrentRecommendation.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.c1CurrentRecommendation.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1CurrentRecommendation.AutoGenerateColumns = False
        Me.c1CurrentRecommendation.BackColor = System.Drawing.Color.White
        Me.c1CurrentRecommendation.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1CurrentRecommendation.ColumnInfo = resources.GetString("c1CurrentRecommendation.ColumnInfo")
        Me.c1CurrentRecommendation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1CurrentRecommendation.ExtendLastCol = True
        Me.c1CurrentRecommendation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1CurrentRecommendation.ForeColor = System.Drawing.Color.Black
        Me.c1CurrentRecommendation.Location = New System.Drawing.Point(15, 15)
        Me.c1CurrentRecommendation.Name = "c1CurrentRecommendation"
        Me.c1CurrentRecommendation.Padding = New System.Windows.Forms.Padding(3)
        Me.c1CurrentRecommendation.Rows.Count = 1
        Me.c1CurrentRecommendation.Rows.DefaultSize = 19
        Me.c1CurrentRecommendation.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1CurrentRecommendation.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1CurrentRecommendation.Size = New System.Drawing.Size(1070, 446)
        Me.c1CurrentRecommendation.StyleInfo = resources.GetString("c1CurrentRecommendation.StyleInfo")
        Me.c1CurrentRecommendation.TabIndex = 141
        Me.c1CurrentRecommendation.Tag = "ClosePeriod"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(1, 503)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1100, 1)
        Me.Label4.TabIndex = 146
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.Transparent
        Me.panel1.BackgroundImage = CType(resources.GetObject("panel1.BackgroundImage"), System.Drawing.Image)
        Me.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel1.Controls.Add(Me.label49)
        Me.panel1.Controls.Add(Me.Label5)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel1.Location = New System.Drawing.Point(1, 1)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(1100, 26)
        Me.panel1.TabIndex = 142
        '
        'label49
        '
        Me.label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.label49.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label49.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label49.Location = New System.Drawing.Point(0, 25)
        Me.label49.Name = "label49"
        Me.label49.Size = New System.Drawing.Size(1100, 1)
        Me.label49.TabIndex = 141
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1100, 26)
        Me.Label5.TabIndex = 142
        Me.Label5.Text = "   Current Recommendation"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1100, 1)
        Me.Label1.TabIndex = 143
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 504)
        Me.Label2.TabIndex = 144
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(1101, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 504)
        Me.Label3.TabIndex = 145
        '
        'tbpg_PastRecommendation
        '
        Me.tbpg_PastRecommendation.Controls.Add(Me.panel4)
        Me.tbpg_PastRecommendation.Location = New System.Drawing.Point(4, 29)
        Me.tbpg_PastRecommendation.Name = "tbpg_PastRecommendation"
        Me.tbpg_PastRecommendation.Padding = New System.Windows.Forms.Padding(4)
        Me.tbpg_PastRecommendation.Size = New System.Drawing.Size(1110, 512)
        Me.tbpg_PastRecommendation.TabIndex = 1
        Me.tbpg_PastRecommendation.Tag = "PAST"
        Me.tbpg_PastRecommendation.Text = "Past Recommendations"
        Me.tbpg_PastRecommendation.UseVisualStyleBackColor = True
        '
        'panel4
        '
        Me.panel4.Controls.Add(Me.Panel6)
        Me.panel4.Controls.Add(Me.Label8)
        Me.panel4.Controls.Add(Me.panel3)
        Me.panel4.Controls.Add(Me.label15)
        Me.panel4.Controls.Add(Me.Label6)
        Me.panel4.Controls.Add(Me.Label7)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel4.Location = New System.Drawing.Point(4, 4)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(1102, 504)
        Me.panel4.TabIndex = 121
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.C1PastRecommendation)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(1, 27)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(15)
        Me.Panel6.Size = New System.Drawing.Size(1100, 476)
        Me.Panel6.TabIndex = 148
        '
        'C1PastRecommendation
        '
        Me.C1PastRecommendation.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1PastRecommendation.AllowEditing = False
        Me.C1PastRecommendation.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1PastRecommendation.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1PastRecommendation.AutoGenerateColumns = False
        Me.C1PastRecommendation.BackColor = System.Drawing.Color.White
        Me.C1PastRecommendation.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1PastRecommendation.ColumnInfo = resources.GetString("C1PastRecommendation.ColumnInfo")
        Me.C1PastRecommendation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1PastRecommendation.ExtendLastCol = True
        Me.C1PastRecommendation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PastRecommendation.ForeColor = System.Drawing.Color.Black
        Me.C1PastRecommendation.Location = New System.Drawing.Point(15, 15)
        Me.C1PastRecommendation.Name = "C1PastRecommendation"
        Me.C1PastRecommendation.Padding = New System.Windows.Forms.Padding(3)
        Me.C1PastRecommendation.Rows.Count = 1
        Me.C1PastRecommendation.Rows.DefaultSize = 19
        Me.C1PastRecommendation.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PastRecommendation.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1PastRecommendation.Size = New System.Drawing.Size(1070, 446)
        Me.C1PastRecommendation.StyleInfo = resources.GetString("C1PastRecommendation.StyleInfo")
        Me.C1PastRecommendation.TabIndex = 144
        Me.C1PastRecommendation.Tag = "ClosePeriod"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(1, 503)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1100, 1)
        Me.Label8.TabIndex = 147
        '
        'panel3
        '
        Me.panel3.BackColor = System.Drawing.Color.Transparent
        Me.panel3.BackgroundImage = CType(resources.GetObject("panel3.BackgroundImage"), System.Drawing.Image)
        Me.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel3.Controls.Add(Me.label12)
        Me.panel3.Controls.Add(Me.label16)
        Me.panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel3.Location = New System.Drawing.Point(1, 1)
        Me.panel3.Name = "panel3"
        Me.panel3.Size = New System.Drawing.Size(1100, 26)
        Me.panel3.TabIndex = 143
        '
        'label12
        '
        Me.label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label12.Location = New System.Drawing.Point(0, 25)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(1100, 1)
        Me.label12.TabIndex = 141
        '
        'label16
        '
        Me.label16.BackColor = System.Drawing.Color.Transparent
        Me.label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label16.ForeColor = System.Drawing.Color.Black
        Me.label16.Location = New System.Drawing.Point(0, 0)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(1100, 26)
        Me.label16.TabIndex = 6
        Me.label16.Text = "   Past Recommendation"
        Me.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label15
        '
        Me.label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label15.Location = New System.Drawing.Point(1, 0)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(1100, 1)
        Me.label15.TabIndex = 140
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 504)
        Me.Label6.TabIndex = 145
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(1101, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 504)
        Me.Label7.TabIndex = 146
        '
        'pnlRecomendationAlert
        '
        Me.pnlRecomendationAlert.BackColor = System.Drawing.Color.Transparent
        Me.pnlRecomendationAlert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlRecomendationAlert.Controls.Add(Me.Label13)
        Me.pnlRecomendationAlert.Controls.Add(Me.lblLastRecomendationName)
        Me.pnlRecomendationAlert.Controls.Add(Me.lblRecomendationAlert)
        Me.pnlRecomendationAlert.Controls.Add(Me.Label10)
        Me.pnlRecomendationAlert.Controls.Add(Me.Label11)
        Me.pnlRecomendationAlert.Controls.Add(Me.Label14)
        Me.pnlRecomendationAlert.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRecomendationAlert.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlRecomendationAlert.Location = New System.Drawing.Point(3, 8)
        Me.pnlRecomendationAlert.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlRecomendationAlert.Name = "pnlRecomendationAlert"
        Me.pnlRecomendationAlert.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlRecomendationAlert.Size = New System.Drawing.Size(1118, 32)
        Me.pnlRecomendationAlert.TabIndex = 145
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(1114, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 24)
        Me.Label13.TabIndex = 144
        '
        'lblLastRecomendationName
        '
        Me.lblLastRecomendationName.AutoSize = True
        Me.lblLastRecomendationName.BackColor = System.Drawing.Color.Transparent
        Me.lblLastRecomendationName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblLastRecomendationName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastRecomendationName.ForeColor = System.Drawing.Color.Black
        Me.lblLastRecomendationName.Location = New System.Drawing.Point(155, 4)
        Me.lblLastRecomendationName.MaximumSize = New System.Drawing.Size(435, 23)
        Me.lblLastRecomendationName.MinimumSize = New System.Drawing.Size(435, 23)
        Me.lblLastRecomendationName.Name = "lblLastRecomendationName"
        Me.lblLastRecomendationName.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblLastRecomendationName.Size = New System.Drawing.Size(435, 23)
        Me.lblLastRecomendationName.TabIndex = 34
        Me.lblLastRecomendationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRecomendationAlert
        '
        Me.lblRecomendationAlert.AutoSize = True
        Me.lblRecomendationAlert.BackColor = System.Drawing.Color.Transparent
        Me.lblRecomendationAlert.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblRecomendationAlert.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecomendationAlert.ForeColor = System.Drawing.Color.Red
        Me.lblRecomendationAlert.Location = New System.Drawing.Point(1, 4)
        Me.lblRecomendationAlert.Name = "lblRecomendationAlert"
        Me.lblRecomendationAlert.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.lblRecomendationAlert.Size = New System.Drawing.Size(154, 18)
        Me.lblRecomendationAlert.TabIndex = 33
        Me.lblRecomendationAlert.Text = "Recommendations (12)"
        Me.lblRecomendationAlert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(1, 28)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1114, 1)
        Me.Label10.TabIndex = 142
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(1, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1114, 1)
        Me.Label11.TabIndex = 143
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(0, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 26)
        Me.Label14.TabIndex = 145
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1118, 5)
        Me.Label9.TabIndex = 144
        '
        'cntMnu_RecommendationOptions
        '
        Me.cntMnu_RecommendationOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cntMnu_MarkSatisfied, Me.cntMnu_InProcess, Me.cntMnu_ViewHistory, Me.cntMnu_UpdateNote, Me.cntMnu_Snooze, Me.cntMnu_Cancel_NA, Me.cntMnu_Reopen, Me.cntMnu_QuickOrders, Me.cntMnu_ViewRefInfo})
        Me.cntMnu_RecommendationOptions.Name = "cntHealthPlan"
        Me.cntMnu_RecommendationOptions.Size = New System.Drawing.Size(225, 202)
        '
        'cntMnu_MarkSatisfied
        '
        Me.cntMnu_MarkSatisfied.BackColor = System.Drawing.Color.PapayaWhip
        Me.cntMnu_MarkSatisfied.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cntMnu_MarkSatisfied.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cntMnu_MarkSatisfied.Image = CType(resources.GetObject("cntMnu_MarkSatisfied.Image"), System.Drawing.Image)
        Me.cntMnu_MarkSatisfied.Name = "cntMnu_MarkSatisfied"
        Me.cntMnu_MarkSatisfied.Size = New System.Drawing.Size(224, 22)
        Me.cntMnu_MarkSatisfied.Tag = "MARK_SATISFIED"
        Me.cntMnu_MarkSatisfied.Text = "Mark Satisfied"
        Me.cntMnu_MarkSatisfied.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cntMnu_InProcess
        '
        Me.cntMnu_InProcess.BackColor = System.Drawing.Color.PapayaWhip
        Me.cntMnu_InProcess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cntMnu_InProcess.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cntMnu_InProcess.Image = CType(resources.GetObject("cntMnu_InProcess.Image"), System.Drawing.Image)
        Me.cntMnu_InProcess.Name = "cntMnu_InProcess"
        Me.cntMnu_InProcess.Size = New System.Drawing.Size(224, 22)
        Me.cntMnu_InProcess.Tag = "INPROCESS"
        Me.cntMnu_InProcess.Text = "Mark In-process"
        Me.cntMnu_InProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cntMnu_ViewHistory
        '
        Me.cntMnu_ViewHistory.BackColor = System.Drawing.Color.PapayaWhip
        Me.cntMnu_ViewHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cntMnu_ViewHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cntMnu_ViewHistory.Image = CType(resources.GetObject("cntMnu_ViewHistory.Image"), System.Drawing.Image)
        Me.cntMnu_ViewHistory.Name = "cntMnu_ViewHistory"
        Me.cntMnu_ViewHistory.Size = New System.Drawing.Size(224, 22)
        Me.cntMnu_ViewHistory.Tag = "VIEWHISTORY"
        Me.cntMnu_ViewHistory.Text = "View History"
        Me.cntMnu_ViewHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cntMnu_UpdateNote
        '
        Me.cntMnu_UpdateNote.BackColor = System.Drawing.Color.PapayaWhip
        Me.cntMnu_UpdateNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cntMnu_UpdateNote.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cntMnu_UpdateNote.Image = CType(resources.GetObject("cntMnu_UpdateNote.Image"), System.Drawing.Image)
        Me.cntMnu_UpdateNote.Name = "cntMnu_UpdateNote"
        Me.cntMnu_UpdateNote.Size = New System.Drawing.Size(224, 22)
        Me.cntMnu_UpdateNote.Tag = "UPDATENOTE"
        Me.cntMnu_UpdateNote.Text = "Update Note"
        '
        'cntMnu_Snooze
        '
        Me.cntMnu_Snooze.BackColor = System.Drawing.Color.PapayaWhip
        Me.cntMnu_Snooze.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cntMnu_Snooze.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cntMnu_Snooze.Image = CType(resources.GetObject("cntMnu_Snooze.Image"), System.Drawing.Image)
        Me.cntMnu_Snooze.Name = "cntMnu_Snooze"
        Me.cntMnu_Snooze.Size = New System.Drawing.Size(224, 22)
        Me.cntMnu_Snooze.Tag = "SNOOZE"
        Me.cntMnu_Snooze.Text = "Snooze Recommendation"
        Me.cntMnu_Snooze.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cntMnu_Cancel_NA
        '
        Me.cntMnu_Cancel_NA.BackColor = System.Drawing.Color.PapayaWhip
        Me.cntMnu_Cancel_NA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cntMnu_Cancel_NA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cntMnu_Cancel_NA.Image = CType(resources.GetObject("cntMnu_Cancel_NA.Image"), System.Drawing.Image)
        Me.cntMnu_Cancel_NA.Name = "cntMnu_Cancel_NA"
        Me.cntMnu_Cancel_NA.Size = New System.Drawing.Size(224, 22)
        Me.cntMnu_Cancel_NA.Tag = "CANCEL_NA"
        Me.cntMnu_Cancel_NA.Text = "Cancel As Not Applicable"
        Me.cntMnu_Cancel_NA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cntMnu_Reopen
        '
        Me.cntMnu_Reopen.BackColor = System.Drawing.Color.PapayaWhip
        Me.cntMnu_Reopen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cntMnu_Reopen.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cntMnu_Reopen.Image = CType(resources.GetObject("cntMnu_Reopen.Image"), System.Drawing.Image)
        Me.cntMnu_Reopen.Name = "cntMnu_Reopen"
        Me.cntMnu_Reopen.Size = New System.Drawing.Size(224, 22)
        Me.cntMnu_Reopen.Tag = "REOPEN"
        Me.cntMnu_Reopen.Text = "Reopen"
        Me.cntMnu_Reopen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cntMnu_QuickOrders
        '
        Me.cntMnu_QuickOrders.BackColor = System.Drawing.Color.PapayaWhip
        Me.cntMnu_QuickOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cntMnu_QuickOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cntMnu_QuickOrders.Image = CType(resources.GetObject("cntMnu_QuickOrders.Image"), System.Drawing.Image)
        Me.cntMnu_QuickOrders.Name = "cntMnu_QuickOrders"
        Me.cntMnu_QuickOrders.Size = New System.Drawing.Size(224, 22)
        Me.cntMnu_QuickOrders.Tag = "QUICKORDERS"
        Me.cntMnu_QuickOrders.Text = "   Quick Orders and Actions"
        Me.cntMnu_QuickOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cntMnu_ViewRefInfo
        '
        Me.cntMnu_ViewRefInfo.BackColor = System.Drawing.Color.PapayaWhip
        Me.cntMnu_ViewRefInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cntMnu_ViewRefInfo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cntMnu_ViewRefInfo.Image = CType(resources.GetObject("cntMnu_ViewRefInfo.Image"), System.Drawing.Image)
        Me.cntMnu_ViewRefInfo.Name = "cntMnu_ViewRefInfo"
        Me.cntMnu_ViewRefInfo.Size = New System.Drawing.Size(224, 22)
        Me.cntMnu_ViewRefInfo.Tag = "VIEWREFINFO"
        Me.cntMnu_ViewRefInfo.Text = "Reference Information"
        Me.cntMnu_ViewRefInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmDM_DisplayRecommendations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1124, 642)
        Me.Controls.Add(Me.pnlContainer)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDM_DisplayRecommendations"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View Recommendations"
        Me.tlsPatientDM.ResumeLayout(False)
        Me.tlsPatientDM.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.pnlContainer.ResumeLayout(False)
        Me.tabCtrl_Recommendations.ResumeLayout(False)
        Me.tbpg_CurrentRecommendation.ResumeLayout(False)
        Me.panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.c1CurrentRecommendation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel1.ResumeLayout(False)
        Me.tbpg_PastRecommendation.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.C1PastRecommendation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel3.ResumeLayout(False)
        Me.pnlRecomendationAlert.ResumeLayout(False)
        Me.pnlRecomendationAlert.PerformLayout()
        Me.cntMnu_RecommendationOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlsPatientDM As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Private WithEvents pnlContainer As System.Windows.Forms.Panel
    Private WithEvents panel4 As System.Windows.Forms.Panel
    Private WithEvents panel3 As System.Windows.Forms.Panel
    Private WithEvents label12 As System.Windows.Forms.Label
    Private WithEvents label16 As System.Windows.Forms.Label
    Private WithEvents label15 As System.Windows.Forms.Label
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents c1CurrentRecommendation As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents label49 As System.Windows.Forms.Label
    Private WithEvents C1PastRecommendation As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tabCtrl_Recommendations As System.Windows.Forms.TabControl
    Friend WithEvents tbpg_CurrentRecommendation As System.Windows.Forms.TabPage
    Friend WithEvents tbpg_PastRecommendation As System.Windows.Forms.TabPage
    Friend WithEvents cntMnu_RecommendationOptions As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cntMnu_MarkSatisfied As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cntMnu_ViewHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cntMnu_UpdateNote As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cntMnu_Snooze As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cntMnu_Cancel_NA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cntMnu_Reopen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cntMnu_QuickOrders As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents ts_btnHealthPlan As System.Windows.Forms.ToolStripButton
    Friend WithEvents cntMnu_ViewRefInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cntMnu_InProcess As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlRecomendationAlert As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblLastRecomendationName As System.Windows.Forms.Label
    Friend WithEvents lblRecomendationAlert As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
