<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewCarePlan_V2
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
                Try
                    If (IsNothing(gloUC_PatientStrip) = False) Then
                        gloUC_PatientStrip.Dispose()
                        gloUC_PatientStrip = Nothing
                    End If
                Catch ex As Exception

                End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewCarePlan_V2))
        Me.Pnl_main = New System.Windows.Forms.Panel()
        Me.Pnl_grid = New System.Windows.Forms.Panel()
        Me.tbCarePlan = New System.Windows.Forms.TabControl()
        Me.tabHealthConcren = New System.Windows.Forms.TabPage()
        Me.pnlHealthConcern = New System.Windows.Forms.Panel()
        Me.C1_HealthConcern = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tabGoal = New System.Windows.Forms.TabPage()
        Me.pnlGoals = New System.Windows.Forms.Panel()
        Me.C1_Goals = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.tabInterventions = New System.Windows.Forms.TabPage()
        Me.pnlInterventions = New System.Windows.Forms.Panel()
        Me.C1_Interventions = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.tabOutcome = New System.Windows.Forms.TabPage()
        Me.pnlOutcomes = New System.Windows.Forms.Panel()
        Me.C1_Outcomes = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label20 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnHealtConcern = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnGoal = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnIntervention = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnOutcome = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnHistory = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnLegacyPlans = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Pnl_main.SuspendLayout()
        Me.Pnl_grid.SuspendLayout()
        Me.tbCarePlan.SuspendLayout()
        Me.tabHealthConcren.SuspendLayout()
        Me.pnlHealthConcern.SuspendLayout()
        CType(Me.C1_HealthConcern, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabGoal.SuspendLayout()
        Me.pnlGoals.SuspendLayout()
        CType(Me.C1_Goals, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabInterventions.SuspendLayout()
        Me.pnlInterventions.SuspendLayout()
        CType(Me.C1_Interventions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabOutcome.SuspendLayout()
        Me.pnlOutcomes.SuspendLayout()
        CType(Me.C1_Outcomes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl_main
        '
        Me.Pnl_main.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Pnl_main.Controls.Add(Me.Pnl_grid)
        Me.Pnl_main.Controls.Add(Me.Panel2)
        Me.Pnl_main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pnl_main.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl_main.Location = New System.Drawing.Point(0, 56)
        Me.Pnl_main.Name = "Pnl_main"
        Me.Pnl_main.Size = New System.Drawing.Size(1382, 539)
        Me.Pnl_main.TabIndex = 11
        '
        'Pnl_grid
        '
        Me.Pnl_grid.Controls.Add(Me.tbCarePlan)
        Me.Pnl_grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pnl_grid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl_grid.Location = New System.Drawing.Point(0, 32)
        Me.Pnl_grid.Name = "Pnl_grid"
        Me.Pnl_grid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Pnl_grid.Size = New System.Drawing.Size(1382, 507)
        Me.Pnl_grid.TabIndex = 1
        '
        'tbCarePlan
        '
        Me.tbCarePlan.Controls.Add(Me.tabHealthConcren)
        Me.tbCarePlan.Controls.Add(Me.tabGoal)
        Me.tbCarePlan.Controls.Add(Me.tabInterventions)
        Me.tbCarePlan.Controls.Add(Me.tabOutcome)
        Me.tbCarePlan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbCarePlan.Location = New System.Drawing.Point(3, 0)
        Me.tbCarePlan.Name = "tbCarePlan"
        Me.tbCarePlan.SelectedIndex = 0
        Me.tbCarePlan.Size = New System.Drawing.Size(1376, 504)
        Me.tbCarePlan.TabIndex = 24
        '
        'tabHealthConcren
        '
        Me.tabHealthConcren.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tabHealthConcren.Controls.Add(Me.pnlHealthConcern)
        Me.tabHealthConcren.Location = New System.Drawing.Point(4, 23)
        Me.tabHealthConcren.Name = "tabHealthConcren"
        Me.tabHealthConcren.Size = New System.Drawing.Size(1368, 477)
        Me.tabHealthConcren.TabIndex = 0
        Me.tabHealthConcren.Text = "Health Concerns"
        Me.tabHealthConcren.UseVisualStyleBackColor = True
        '
        'pnlHealthConcern
        '
        Me.pnlHealthConcern.Controls.Add(Me.C1_HealthConcern)
        Me.pnlHealthConcern.Controls.Add(Me.Label9)
        Me.pnlHealthConcern.Controls.Add(Me.Label10)
        Me.pnlHealthConcern.Controls.Add(Me.Label11)
        Me.pnlHealthConcern.Controls.Add(Me.Label12)
        Me.pnlHealthConcern.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlHealthConcern.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlHealthConcern.Location = New System.Drawing.Point(0, 0)
        Me.pnlHealthConcern.Name = "pnlHealthConcern"
        Me.pnlHealthConcern.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlHealthConcern.Size = New System.Drawing.Size(1368, 477)
        Me.pnlHealthConcern.TabIndex = 14
        '
        'C1_HealthConcern
        '
        Me.C1_HealthConcern.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1_HealthConcern.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1_HealthConcern.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1_HealthConcern.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1_HealthConcern.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1_HealthConcern.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1_HealthConcern.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1_HealthConcern.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1_HealthConcern.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1_HealthConcern.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1_HealthConcern.Location = New System.Drawing.Point(4, 4)
        Me.C1_HealthConcern.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1_HealthConcern.Name = "C1_HealthConcern"
        Me.C1_HealthConcern.Rows.Count = 1
        Me.C1_HealthConcern.Rows.DefaultSize = 19
        Me.C1_HealthConcern.Rows.Fixed = 0
        Me.C1_HealthConcern.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1_HealthConcern.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1_HealthConcern.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1_HealthConcern.Size = New System.Drawing.Size(1360, 469)
        Me.C1_HealthConcern.StyleInfo = resources.GetString("C1_HealthConcern.StyleInfo")
        Me.C1_HealthConcern.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(4, 473)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1360, 1)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 470)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(1364, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 470)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1362, 1)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "label1"
        '
        'tabGoal
        '
        Me.tabGoal.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tabGoal.Controls.Add(Me.pnlGoals)
        Me.tabGoal.Location = New System.Drawing.Point(4, 23)
        Me.tabGoal.Name = "tabGoal"
        Me.tabGoal.Size = New System.Drawing.Size(1368, 477)
        Me.tabGoal.TabIndex = 1
        Me.tabGoal.Text = "Goals"
        Me.tabGoal.UseVisualStyleBackColor = True
        '
        'pnlGoals
        '
        Me.pnlGoals.Controls.Add(Me.C1_Goals)
        Me.pnlGoals.Controls.Add(Me.Label13)
        Me.pnlGoals.Controls.Add(Me.Label14)
        Me.pnlGoals.Controls.Add(Me.Label15)
        Me.pnlGoals.Controls.Add(Me.Label16)
        Me.pnlGoals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGoals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlGoals.Location = New System.Drawing.Point(0, 0)
        Me.pnlGoals.Name = "pnlGoals"
        Me.pnlGoals.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlGoals.Size = New System.Drawing.Size(1368, 477)
        Me.pnlGoals.TabIndex = 15
        '
        'C1_Goals
        '
        Me.C1_Goals.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1_Goals.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1_Goals.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1_Goals.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1_Goals.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1_Goals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1_Goals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1_Goals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1_Goals.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1_Goals.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1_Goals.Location = New System.Drawing.Point(4, 4)
        Me.C1_Goals.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1_Goals.Name = "C1_Goals"
        Me.C1_Goals.Rows.Count = 1
        Me.C1_Goals.Rows.DefaultSize = 19
        Me.C1_Goals.Rows.Fixed = 0
        Me.C1_Goals.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1_Goals.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1_Goals.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1_Goals.Size = New System.Drawing.Size(1360, 469)
        Me.C1_Goals.StyleInfo = resources.GetString("C1_Goals.StyleInfo")
        Me.C1_Goals.TabIndex = 7
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(4, 473)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1360, 1)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 4)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 470)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1364, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 470)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(3, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1362, 1)
        Me.Label16.TabIndex = 9
        Me.Label16.Text = "label1"
        '
        'tabInterventions
        '
        Me.tabInterventions.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tabInterventions.Controls.Add(Me.pnlInterventions)
        Me.tabInterventions.Location = New System.Drawing.Point(4, 23)
        Me.tabInterventions.Name = "tabInterventions"
        Me.tabInterventions.Size = New System.Drawing.Size(1368, 477)
        Me.tabInterventions.TabIndex = 2
        Me.tabInterventions.Text = "Interventions"
        Me.tabInterventions.UseVisualStyleBackColor = True
        '
        'pnlInterventions
        '
        Me.pnlInterventions.Controls.Add(Me.C1_Interventions)
        Me.pnlInterventions.Controls.Add(Me.Label17)
        Me.pnlInterventions.Controls.Add(Me.Label18)
        Me.pnlInterventions.Controls.Add(Me.Label19)
        Me.pnlInterventions.Controls.Add(Me.Label25)
        Me.pnlInterventions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlInterventions.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlInterventions.Location = New System.Drawing.Point(0, 0)
        Me.pnlInterventions.Name = "pnlInterventions"
        Me.pnlInterventions.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlInterventions.Size = New System.Drawing.Size(1368, 477)
        Me.pnlInterventions.TabIndex = 17
        '
        'C1_Interventions
        '
        Me.C1_Interventions.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1_Interventions.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1_Interventions.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1_Interventions.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1_Interventions.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1_Interventions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1_Interventions.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1_Interventions.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1_Interventions.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1_Interventions.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1_Interventions.Location = New System.Drawing.Point(4, 4)
        Me.C1_Interventions.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1_Interventions.Name = "C1_Interventions"
        Me.C1_Interventions.Rows.Count = 1
        Me.C1_Interventions.Rows.DefaultSize = 19
        Me.C1_Interventions.Rows.Fixed = 0
        Me.C1_Interventions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1_Interventions.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1_Interventions.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1_Interventions.Size = New System.Drawing.Size(1360, 469)
        Me.C1_Interventions.StyleInfo = resources.GetString("C1_Interventions.StyleInfo")
        Me.C1_Interventions.TabIndex = 7
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(4, 473)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1360, 1)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 470)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(1364, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 470)
        Me.Label19.TabIndex = 10
        Me.Label19.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(3, 3)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1362, 1)
        Me.Label25.TabIndex = 9
        Me.Label25.Text = "label1"
        '
        'tabOutcome
        '
        Me.tabOutcome.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tabOutcome.Controls.Add(Me.pnlOutcomes)
        Me.tabOutcome.Location = New System.Drawing.Point(4, 23)
        Me.tabOutcome.Name = "tabOutcome"
        Me.tabOutcome.Size = New System.Drawing.Size(1368, 477)
        Me.tabOutcome.TabIndex = 3
        Me.tabOutcome.Text = "Evaluations and Outcomes"
        '
        'pnlOutcomes
        '
        Me.pnlOutcomes.Controls.Add(Me.C1_Outcomes)
        Me.pnlOutcomes.Controls.Add(Me.Label26)
        Me.pnlOutcomes.Controls.Add(Me.Label27)
        Me.pnlOutcomes.Controls.Add(Me.Label28)
        Me.pnlOutcomes.Controls.Add(Me.Label29)
        Me.pnlOutcomes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlOutcomes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlOutcomes.Location = New System.Drawing.Point(0, 0)
        Me.pnlOutcomes.Name = "pnlOutcomes"
        Me.pnlOutcomes.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlOutcomes.Size = New System.Drawing.Size(1368, 477)
        Me.pnlOutcomes.TabIndex = 18
        '
        'C1_Outcomes
        '
        Me.C1_Outcomes.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1_Outcomes.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1_Outcomes.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1_Outcomes.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1_Outcomes.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1_Outcomes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1_Outcomes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1_Outcomes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1_Outcomes.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1_Outcomes.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1_Outcomes.Location = New System.Drawing.Point(4, 4)
        Me.C1_Outcomes.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1_Outcomes.Name = "C1_Outcomes"
        Me.C1_Outcomes.Rows.Count = 1
        Me.C1_Outcomes.Rows.DefaultSize = 19
        Me.C1_Outcomes.Rows.Fixed = 0
        Me.C1_Outcomes.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1_Outcomes.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1_Outcomes.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1_Outcomes.Size = New System.Drawing.Size(1360, 469)
        Me.C1_Outcomes.StyleInfo = resources.GetString("C1_Outcomes.StyleInfo")
        Me.C1_Outcomes.TabIndex = 7
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(4, 473)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1360, 1)
        Me.Label26.TabIndex = 12
        Me.Label26.Text = "label2"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(3, 4)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 470)
        Me.Label27.TabIndex = 11
        Me.Label27.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(1364, 4)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 470)
        Me.Label28.TabIndex = 10
        Me.Label28.Text = "label3"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(3, 3)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1362, 1)
        Me.Label29.TabIndex = 9
        Me.Label29.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(1382, 32)
        Me.Panel2.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.pnlTopRight)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1376, 26)
        Me.Panel1.TabIndex = 13
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.panel4)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(0, 0)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(1376, 26)
        Me.pnlTopRight.TabIndex = 8
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.White
        Me.panel4.Controls.Add(Me.txtSearch)
        Me.panel4.Controls.Add(Me.label21)
        Me.panel4.Controls.Add(Me.label20)
        Me.panel4.Controls.Add(Me.btnClear)
        Me.panel4.Controls.Add(Me.label22)
        Me.panel4.Controls.Add(Me.label23)
        Me.panel4.Controls.Add(Me.label24)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel4.ForeColor = System.Drawing.Color.Black
        Me.panel4.Location = New System.Drawing.Point(65, 1)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(281, 24)
        Me.panel4.TabIndex = 52
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(6, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(246, 15)
        Me.txtSearch.TabIndex = 0
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.White
        Me.label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.label21.Location = New System.Drawing.Point(6, 0)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(246, 3)
        Me.label21.TabIndex = 37
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.White
        Me.label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label20.Location = New System.Drawing.Point(6, 19)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(246, 5)
        Me.label20.TabIndex = 43
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
        Me.btnClear.Location = New System.Drawing.Point(252, 0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(28, 24)
        Me.btnClear.TabIndex = 50
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'label22
        '
        Me.label22.BackColor = System.Drawing.Color.White
        Me.label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.label22.Location = New System.Drawing.Point(1, 0)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(5, 24)
        Me.label22.TabIndex = 38
        '
        'label23
        '
        Me.label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.label23.Location = New System.Drawing.Point(0, 0)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(1, 24)
        Me.label23.TabIndex = 39
        Me.label23.Text = "label4"
        '
        'label24
        '
        Me.label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.label24.Location = New System.Drawing.Point(280, 0)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(1, 24)
        Me.label24.TabIndex = 40
        Me.label24.Text = "label4"
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(64, 24)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "  Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1374, 1)
        Me.Label2.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 25)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1375, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 25)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(0, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1376, 1)
        Me.Label1.TabIndex = 8
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1382, 56)
        Me.pnlToolStrip.TabIndex = 14
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnHealtConcern, Me.ts_btnGoal, Me.ts_btnIntervention, Me.ts_btnOutcome, Me.ts_btnHistory, Me.ts_btnLegacyPlans, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(1382, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnAdd.Image = Global.gloEMR.My.Resources.Resources.New05
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(37, 50)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&New"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnModify.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
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
        Me.ts_btnDelete.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnDelete.Image = Global.gloEMR.My.Resources.Resources.Delete1
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnHealtConcern
        '
        Me.ts_btnHealtConcern.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnHealtConcern.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnHealtConcern.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnHealtConcern.Image = CType(resources.GetObject("ts_btnHealtConcern.Image"), System.Drawing.Image)
        Me.ts_btnHealtConcern.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnHealtConcern.Name = "ts_btnHealtConcern"
        Me.ts_btnHealtConcern.Size = New System.Drawing.Size(105, 50)
        Me.ts_btnHealtConcern.Tag = "Health Concern"
        Me.ts_btnHealtConcern.Text = "&Health Concern"
        Me.ts_btnHealtConcern.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnGoal
        '
        Me.ts_btnGoal.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnGoal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnGoal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnGoal.Image = CType(resources.GetObject("ts_btnGoal.Image"), System.Drawing.Image)
        Me.ts_btnGoal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnGoal.Name = "ts_btnGoal"
        Me.ts_btnGoal.Size = New System.Drawing.Size(37, 50)
        Me.ts_btnGoal.Tag = "Goal"
        Me.ts_btnGoal.Text = "&Goal"
        Me.ts_btnGoal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnIntervention
        '
        Me.ts_btnIntervention.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnIntervention.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnIntervention.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnIntervention.Image = CType(resources.GetObject("ts_btnIntervention.Image"), System.Drawing.Image)
        Me.ts_btnIntervention.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnIntervention.Name = "ts_btnIntervention"
        Me.ts_btnIntervention.Size = New System.Drawing.Size(89, 50)
        Me.ts_btnIntervention.Tag = "Intervention"
        Me.ts_btnIntervention.Text = "&Intervention"
        Me.ts_btnIntervention.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnOutcome
        '
        Me.ts_btnOutcome.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnOutcome.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOutcome.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnOutcome.Image = CType(resources.GetObject("ts_btnOutcome.Image"), System.Drawing.Image)
        Me.ts_btnOutcome.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOutcome.Name = "ts_btnOutcome"
        Me.ts_btnOutcome.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOutcome.Tag = "Outcome"
        Me.ts_btnOutcome.Text = "&Outcome"
        Me.ts_btnOutcome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnHistory
        '
        Me.ts_btnHistory.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnHistory.Image = CType(resources.GetObject("ts_btnHistory.Image"), System.Drawing.Image)
        Me.ts_btnHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnHistory.Name = "ts_btnHistory"
        Me.ts_btnHistory.Size = New System.Drawing.Size(93, 50)
        Me.ts_btnHistory.Tag = "Audit History"
        Me.ts_btnHistory.Text = "&Audit History"
        Me.ts_btnHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnLegacyPlans
        '
        Me.ts_btnLegacyPlans.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnLegacyPlans.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnLegacyPlans.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnLegacyPlans.Image = CType(resources.GetObject("ts_btnLegacyPlans.Image"), System.Drawing.Image)
        Me.ts_btnLegacyPlans.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnLegacyPlans.Name = "ts_btnLegacyPlans"
        Me.ts_btnLegacyPlans.Size = New System.Drawing.Size(89, 50)
        Me.ts_btnLegacyPlans.Tag = "Legacy Plans"
        Me.ts_btnLegacyPlans.Text = "&Legacy Plans"
        Me.ts_btnLegacyPlans.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnRefresh.Image = Global.gloEMR.My.Resources.Resources.Refresh
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = Global.gloEMR.My.Resources.Resources.Close01
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmViewCarePlan_V2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1382, 595)
        Me.Controls.Add(Me.Pnl_main)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewCarePlan_V2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Patient Care Plan"
        Me.Pnl_main.ResumeLayout(False)
        Me.Pnl_grid.ResumeLayout(False)
        Me.tbCarePlan.ResumeLayout(False)
        Me.tabHealthConcren.ResumeLayout(False)
        Me.pnlHealthConcern.ResumeLayout(False)
        CType(Me.C1_HealthConcern, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabGoal.ResumeLayout(False)
        Me.pnlGoals.ResumeLayout(False)
        CType(Me.C1_Goals, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabInterventions.ResumeLayout(False)
        Me.pnlInterventions.ResumeLayout(False)
        CType(Me.C1_Interventions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabOutcome.ResumeLayout(False)
        Me.pnlOutcomes.ResumeLayout(False)
        CType(Me.C1_Outcomes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pnl_main As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents ts_btnLegacyPlans As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnHealtConcern As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnGoal As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHealthConcern As System.Windows.Forms.Panel
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents pnlGoals As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents C1_Goals As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlInterventions As System.Windows.Forms.Panel
    Friend WithEvents C1_Interventions As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents pnlOutcomes As System.Windows.Forms.Panel
    Friend WithEvents C1_Outcomes As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents tbCarePlan As System.Windows.Forms.TabControl
    Private WithEvents tabHealthConcren As System.Windows.Forms.TabPage
    Friend WithEvents C1_HealthConcern As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents tabGoal As System.Windows.Forms.TabPage
    Private WithEvents tabInterventions As System.Windows.Forms.TabPage
    Friend WithEvents tabOutcome As System.Windows.Forms.TabPage
    Friend WithEvents Pnl_grid As System.Windows.Forms.Panel
    Friend WithEvents ts_btnIntervention As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnOutcome As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnHistory As System.Windows.Forms.ToolStripButton
End Class
