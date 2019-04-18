<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTreatmentPlan
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTreatmentPlan))
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tlsPatientPOT = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnPOTSaveClose = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnPOTClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.pnlExam = New System.Windows.Forms.Panel()
        Me.btnExamClear = New System.Windows.Forms.Button()
        Me.btnExam = New System.Windows.Forms.Button()
        Me.lblExamName = New System.Windows.Forms.Label()
        Me.lblDetails = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblGridBottom = New System.Windows.Forms.Label()
        Me.lblGridLeft = New System.Windows.Forms.Label()
        Me.lblGridRight = New System.Windows.Forms.Label()
        Me.lblGridTop = New System.Windows.Forms.Label()
        Me.txtPlanAssesment = New System.Windows.Forms.TextBox()
        Me.dtpPlanEffectiveTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpPlanEffectiveFrom = New System.Windows.Forms.DateTimePicker()
        Me.txtPlantitle = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlActivityList = New System.Windows.Forms.Panel()
        Me.c1PlanActivity = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlActivityDetail = New System.Windows.Forms.Panel()
        Me.pnlActivityDetailData = New System.Windows.Forms.Panel()
        Me.rbt_Inactive = New System.Windows.Forms.RadioButton()
        Me.rbt_Active = New System.Windows.Forms.RadioButton()
        Me.rbt_Complete = New System.Windows.Forms.RadioButton()
        Me.lblEncounter = New System.Windows.Forms.Label()
        Me.lblLabOrders = New System.Windows.Forms.Label()
        Me.lblNurtitionRecommendation = New System.Windows.Forms.Label()
        Me.cmbProblemList = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtActivity = New System.Windows.Forms.TextBox()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblMedication = New System.Windows.Forms.Label()
        Me.btnClearSelectedProblem = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btnGetPatientProblem = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.dtpActivityEffectiveFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.dtpActivityEffectiveTo = New System.Windows.Forms.DateTimePicker()
        Me.txtSelectedCode = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.pnlActivitytToolStrip = New System.Windows.Forms.Panel()
        Me.tlsp_ActivityDetails = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsp_btnActivitySave = New System.Windows.Forms.ToolStripButton()
        Me.tlsp_btnActivityCancel = New System.Windows.Forms.ToolStripButton()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.pnlActivityType = New System.Windows.Forms.Panel()
        Me.pnlControlContainer = New System.Windows.Forms.Panel()
        Me.pnlNutrition = New System.Windows.Forms.Panel()
        Me.pnlNurtFinding = New System.Windows.Forms.Panel()
        Me.trvNurtFindings = New System.Windows.Forms.TreeView()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.pnlNurtSMSearch = New System.Windows.Forms.Panel()
        Me.txtNurtSMSearch = New gloSnoMed.gloSearchTextBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.btnNurtClear = New System.Windows.Forms.Button()
        Me.PicBxNurt_Search = New System.Windows.Forms.PictureBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.chkNurtCOREProblem = New System.Windows.Forms.CheckBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.pnlLabOrdersList = New System.Windows.Forms.Panel()
        Me.pnltrvList = New System.Windows.Forms.Panel()
        Me.GloUC_trvTest = New gloUserControlLibrary.gloUC_TreeView()
        Me.panel8 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.chkIncludeTestCode = New System.Windows.Forms.CheckBox()
        Me.label39 = New System.Windows.Forms.Label()
        Me.label40 = New System.Windows.Forms.Label()
        Me.label41 = New System.Windows.Forms.Label()
        Me.pnlLabTests = New System.Windows.Forms.Panel()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.btnLabTests = New System.Windows.Forms.Button()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.pnl_btnTests = New System.Windows.Forms.Panel()
        Me.btnTests = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnl_btnRadiologyImaging = New System.Windows.Forms.Panel()
        Me.label36 = New System.Windows.Forms.Label()
        Me.btnRadiologyImaging = New System.Windows.Forms.Button()
        Me.lblRadiologyImaging1 = New System.Windows.Forms.Label()
        Me.lblRadiologyImaging2 = New System.Windows.Forms.Label()
        Me.lblRadiologyImaging4 = New System.Windows.Forms.Label()
        Me.lblRadiologyImaging3 = New System.Windows.Forms.Label()
        Me.pnl_btnRefTest = New System.Windows.Forms.Panel()
        Me.btnRefTest = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.pnl_btnOthers = New System.Windows.Forms.Panel()
        Me.label37 = New System.Windows.Forms.Label()
        Me.btnOthers = New System.Windows.Forms.Button()
        Me.lblOthers1 = New System.Windows.Forms.Label()
        Me.lblOthers2 = New System.Windows.Forms.Label()
        Me.lblOthers3 = New System.Windows.Forms.Label()
        Me.lblOthers4 = New System.Windows.Forms.Label()
        Me.pnl_btnGroups = New System.Windows.Forms.Panel()
        Me.label35 = New System.Windows.Forms.Label()
        Me.label34 = New System.Windows.Forms.Label()
        Me.label33 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.btnGroups = New System.Windows.Forms.Button()
        Me.pnlEncounterSnomed = New System.Windows.Forms.Panel()
        Me.pnlfinding = New System.Windows.Forms.Panel()
        Me.trvFindings = New System.Windows.Forms.TreeView()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.pnlSMSearch = New System.Windows.Forms.Panel()
        Me.txtSMSearch = New gloSnoMed.gloSearchTextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkCOREProblem = New System.Windows.Forms.CheckBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.pnlbtnNutrition = New System.Windows.Forms.Panel()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.btnNutrition = New System.Windows.Forms.Button()
        Me.pnlbtnEncounter = New System.Windows.Forms.Panel()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.btnEncounter = New System.Windows.Forms.Button()
        Me.pnlbntLabs = New System.Windows.Forms.Panel()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.btnLabs = New System.Windows.Forms.Button()
        Me.pnlbtnMedication = New System.Windows.Forms.Panel()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.btnMedication = New System.Windows.Forms.Button()
        Me.pnlCodeList = New System.Windows.Forms.Panel()
        Me.btnAllCode = New System.Windows.Forms.Button()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.CmnuPannedActivity = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.oMnuRemovePlannedActivity = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerNurt = New System.Windows.Forms.Timer(Me.components)
        Me.pnlToolStrip.SuspendLayout()
        Me.tlsPatientPOT.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlExam.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlActivityList.SuspendLayout()
        CType(Me.c1PlanActivity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlActivityDetail.SuspendLayout()
        Me.pnlActivityDetailData.SuspendLayout()
        Me.pnlActivitytToolStrip.SuspendLayout()
        Me.tlsp_ActivityDetails.SuspendLayout()
        Me.pnlActivityType.SuspendLayout()
        Me.pnlControlContainer.SuspendLayout()
        Me.pnlNutrition.SuspendLayout()
        Me.pnlNurtFinding.SuspendLayout()
        Me.pnlNurtSMSearch.SuspendLayout()
        CType(Me.PicBxNurt_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.pnlLabOrdersList.SuspendLayout()
        Me.pnltrvList.SuspendLayout()
        Me.panel8.SuspendLayout()
        Me.pnlLabTests.SuspendLayout()
        Me.pnl_btnTests.SuspendLayout()
        Me.pnl_btnRadiologyImaging.SuspendLayout()
        Me.pnl_btnRefTest.SuspendLayout()
        Me.pnl_btnOthers.SuspendLayout()
        Me.pnl_btnGroups.SuspendLayout()
        Me.pnlEncounterSnomed.SuspendLayout()
        Me.pnlfinding.SuspendLayout()
        Me.pnlSMSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlbtnNutrition.SuspendLayout()
        Me.pnlbtnEncounter.SuspendLayout()
        Me.pnlbntLabs.SuspendLayout()
        Me.pnlbtnMedication.SuspendLayout()
        Me.pnlCodeList.SuspendLayout()
        Me.CmnuPannedActivity.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.tlsPatientPOT)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1150, 56)
        Me.pnlToolStrip.TabIndex = 17
        '
        'tlsPatientPOT
        '
        Me.tlsPatientPOT.BackColor = System.Drawing.Color.Transparent
        Me.tlsPatientPOT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsPatientPOT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsPatientPOT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsPatientPOT.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsPatientPOT.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsPatientPOT.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnPOTSaveClose, Me.ts_btnPOTClose})
        Me.tlsPatientPOT.Location = New System.Drawing.Point(0, 0)
        Me.tlsPatientPOT.Name = "tlsPatientPOT"
        Me.tlsPatientPOT.Size = New System.Drawing.Size(1150, 53)
        Me.tlsPatientPOT.TabIndex = 0
        Me.tlsPatientPOT.Text = "ToolStrip1"
        '
        'ts_btnPOTSaveClose
        '
        Me.ts_btnPOTSaveClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnPOTSaveClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnPOTSaveClose.Image = CType(resources.GetObject("ts_btnPOTSaveClose.Image"), System.Drawing.Image)
        Me.ts_btnPOTSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPOTSaveClose.Name = "ts_btnPOTSaveClose"
        Me.ts_btnPOTSaveClose.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnPOTSaveClose.Tag = "OK"
        Me.ts_btnPOTSaveClose.Text = "&Save&&Cls"
        Me.ts_btnPOTSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnPOTSaveClose.ToolTipText = "Save and Close"
        '
        'ts_btnPOTClose
        '
        Me.ts_btnPOTClose.Image = CType(resources.GetObject("ts_btnPOTClose.Image"), System.Drawing.Image)
        Me.ts_btnPOTClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPOTClose.Name = "ts_btnPOTClose"
        Me.ts_btnPOTClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnPOTClose.Tag = "Close"
        Me.ts_btnPOTClose.Text = "&Close"
        Me.ts_btnPOTClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.pnlExam)
        Me.pnlHeader.Controls.Add(Me.Label24)
        Me.pnlHeader.Controls.Add(Me.lblGridBottom)
        Me.pnlHeader.Controls.Add(Me.lblGridLeft)
        Me.pnlHeader.Controls.Add(Me.lblGridRight)
        Me.pnlHeader.Controls.Add(Me.lblGridTop)
        Me.pnlHeader.Controls.Add(Me.txtPlanAssesment)
        Me.pnlHeader.Controls.Add(Me.dtpPlanEffectiveTo)
        Me.pnlHeader.Controls.Add(Me.dtpPlanEffectiveFrom)
        Me.pnlHeader.Controls.Add(Me.txtPlantitle)
        Me.pnlHeader.Controls.Add(Me.Label4)
        Me.pnlHeader.Controls.Add(Me.Label3)
        Me.pnlHeader.Controls.Add(Me.Label2)
        Me.pnlHeader.Controls.Add(Me.Label1)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 56)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlHeader.Size = New System.Drawing.Size(1150, 170)
        Me.pnlHeader.TabIndex = 18
        '
        'pnlExam
        '
        Me.pnlExam.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlExam.BackColor = System.Drawing.Color.Transparent
        Me.pnlExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlExam.Controls.Add(Me.btnExamClear)
        Me.pnlExam.Controls.Add(Me.btnExam)
        Me.pnlExam.Controls.Add(Me.lblExamName)
        Me.pnlExam.Controls.Add(Me.lblDetails)
        Me.pnlExam.Location = New System.Drawing.Point(14, 131)
        Me.pnlExam.Name = "pnlExam"
        Me.pnlExam.Size = New System.Drawing.Size(1126, 30)
        Me.pnlExam.TabIndex = 75
        '
        'btnExamClear
        '
        Me.btnExamClear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExamClear.AutoEllipsis = True
        Me.btnExamClear.BackColor = System.Drawing.Color.Transparent
        Me.btnExamClear.BackgroundImage = CType(resources.GetObject("btnExamClear.BackgroundImage"), System.Drawing.Image)
        Me.btnExamClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExamClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExamClear.Image = CType(resources.GetObject("btnExamClear.Image"), System.Drawing.Image)
        Me.btnExamClear.Location = New System.Drawing.Point(1086, 2)
        Me.btnExamClear.Name = "btnExamClear"
        Me.btnExamClear.Size = New System.Drawing.Size(24, 24)
        Me.btnExamClear.TabIndex = 82
        Me.btnExamClear.UseVisualStyleBackColor = False
        '
        'btnExam
        '
        Me.btnExam.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExam.BackColor = System.Drawing.Color.Transparent
        Me.btnExam.BackgroundImage = CType(resources.GetObject("btnExam.BackgroundImage"), System.Drawing.Image)
        Me.btnExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExam.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExam.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExam.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExam.Image = CType(resources.GetObject("btnExam.Image"), System.Drawing.Image)
        Me.btnExam.Location = New System.Drawing.Point(1059, 2)
        Me.btnExam.Name = "btnExam"
        Me.btnExam.Size = New System.Drawing.Size(24, 24)
        Me.btnExam.TabIndex = 75
        Me.btnExam.Text = "          "
        Me.btnExam.UseVisualStyleBackColor = False
        '
        'lblExamName
        '
        Me.lblExamName.AutoSize = True
        Me.lblExamName.BackColor = System.Drawing.Color.Transparent
        Me.lblExamName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblExamName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblExamName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblExamName.Location = New System.Drawing.Point(94, 7)
        Me.lblExamName.Name = "lblExamName"
        Me.lblExamName.Size = New System.Drawing.Size(0, 14)
        Me.lblExamName.TabIndex = 81
        Me.lblExamName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDetails
        '
        Me.lblDetails.AutoSize = True
        Me.lblDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblDetails.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDetails.Location = New System.Drawing.Point(43, 7)
        Me.lblDetails.Name = "lblDetails"
        Me.lblDetails.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblDetails.Size = New System.Drawing.Size(49, 14)
        Me.lblDetails.TabIndex = 71
        Me.lblDetails.Text = "Exam :"
        Me.lblDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Red
        Me.Label24.Location = New System.Drawing.Point(53, 13)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(16, 16)
        Me.Label24.TabIndex = 15
        Me.Label24.Text = "*"
        '
        'lblGridBottom
        '
        Me.lblGridBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblGridBottom.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblGridBottom.Location = New System.Drawing.Point(4, 166)
        Me.lblGridBottom.Name = "lblGridBottom"
        Me.lblGridBottom.Size = New System.Drawing.Size(1142, 1)
        Me.lblGridBottom.TabIndex = 14
        Me.lblGridBottom.Text = "label2"
        '
        'lblGridLeft
        '
        Me.lblGridLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblGridLeft.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGridLeft.Location = New System.Drawing.Point(3, 4)
        Me.lblGridLeft.Name = "lblGridLeft"
        Me.lblGridLeft.Size = New System.Drawing.Size(1, 163)
        Me.lblGridLeft.TabIndex = 13
        Me.lblGridLeft.Text = "label4"
        '
        'lblGridRight
        '
        Me.lblGridRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblGridRight.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblGridRight.Location = New System.Drawing.Point(1146, 4)
        Me.lblGridRight.Name = "lblGridRight"
        Me.lblGridRight.Size = New System.Drawing.Size(1, 163)
        Me.lblGridRight.TabIndex = 12
        Me.lblGridRight.Text = "label3"
        '
        'lblGridTop
        '
        Me.lblGridTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblGridTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGridTop.Location = New System.Drawing.Point(3, 3)
        Me.lblGridTop.Name = "lblGridTop"
        Me.lblGridTop.Size = New System.Drawing.Size(1144, 1)
        Me.lblGridTop.TabIndex = 11
        Me.lblGridTop.Text = "label1"
        '
        'txtPlanAssesment
        '
        Me.txtPlanAssesment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPlanAssesment.Location = New System.Drawing.Point(108, 65)
        Me.txtPlanAssesment.Multiline = True
        Me.txtPlanAssesment.Name = "txtPlanAssesment"
        Me.txtPlanAssesment.Size = New System.Drawing.Size(1017, 65)
        Me.txtPlanAssesment.TabIndex = 7
        '
        'dtpPlanEffectiveTo
        '
        Me.dtpPlanEffectiveTo.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpPlanEffectiveTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPlanEffectiveTo.Location = New System.Drawing.Point(369, 38)
        Me.dtpPlanEffectiveTo.Name = "dtpPlanEffectiveTo"
        Me.dtpPlanEffectiveTo.Size = New System.Drawing.Size(192, 22)
        Me.dtpPlanEffectiveTo.TabIndex = 6
        '
        'dtpPlanEffectiveFrom
        '
        Me.dtpPlanEffectiveFrom.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpPlanEffectiveFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPlanEffectiveFrom.Location = New System.Drawing.Point(108, 38)
        Me.dtpPlanEffectiveFrom.Name = "dtpPlanEffectiveFrom"
        Me.dtpPlanEffectiveFrom.Size = New System.Drawing.Size(192, 22)
        Me.dtpPlanEffectiveFrom.TabIndex = 5
        '
        'txtPlantitle
        '
        Me.txtPlantitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPlantitle.Location = New System.Drawing.Point(108, 11)
        Me.txtPlantitle.MaxLength = 200
        Me.txtPlantitle.Name = "txtPlantitle"
        Me.txtPlantitle.Size = New System.Drawing.Size(1017, 22)
        Me.txtPlantitle.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(26, 66)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 14)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Assessment :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(336, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "To :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Effective From :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(66, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Title :"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlActivityList)
        Me.pnlMain.Controls.Add(Me.pnlActivityDetail)
        Me.pnlMain.Controls.Add(Me.pnlActivityType)
        Me.pnlMain.Controls.Add(Me.Panel3)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 226)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1150, 553)
        Me.pnlMain.TabIndex = 19
        '
        'pnlActivityList
        '
        Me.pnlActivityList.Controls.Add(Me.c1PlanActivity)
        Me.pnlActivityList.Controls.Add(Me.Label5)
        Me.pnlActivityList.Controls.Add(Me.Label6)
        Me.pnlActivityList.Controls.Add(Me.Label7)
        Me.pnlActivityList.Controls.Add(Me.Label8)
        Me.pnlActivityList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlActivityList.Location = New System.Drawing.Point(304, 35)
        Me.pnlActivityList.Name = "pnlActivityList"
        Me.pnlActivityList.Padding = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.pnlActivityList.Size = New System.Drawing.Size(846, 216)
        Me.pnlActivityList.TabIndex = 1
        '
        'c1PlanActivity
        '
        Me.c1PlanActivity.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1PlanActivity.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1PlanActivity.AutoGenerateColumns = False
        Me.c1PlanActivity.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.c1PlanActivity.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1PlanActivity.ColumnInfo = resources.GetString("c1PlanActivity.ColumnInfo")
        Me.c1PlanActivity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1PlanActivity.ExtendLastCol = True
        Me.c1PlanActivity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1PlanActivity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1PlanActivity.Location = New System.Drawing.Point(2, 4)
        Me.c1PlanActivity.Name = "c1PlanActivity"
        Me.c1PlanActivity.Padding = New System.Windows.Forms.Padding(3)
        Me.c1PlanActivity.Rows.Count = 1
        Me.c1PlanActivity.Rows.DefaultSize = 19
        Me.c1PlanActivity.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1PlanActivity.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1PlanActivity.Size = New System.Drawing.Size(840, 208)
        Me.c1PlanActivity.StyleInfo = resources.GetString("c1PlanActivity.StyleInfo")
        Me.c1PlanActivity.TabIndex = 142
        Me.c1PlanActivity.Tag = "ClosePeriod"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(2, 212)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(840, 1)
        Me.Label5.TabIndex = 146
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 209)
        Me.Label6.TabIndex = 145
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(842, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 209)
        Me.Label7.TabIndex = 144
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(1, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(842, 1)
        Me.Label8.TabIndex = 143
        Me.Label8.Text = "label1"
        '
        'pnlActivityDetail
        '
        Me.pnlActivityDetail.Controls.Add(Me.pnlActivityDetailData)
        Me.pnlActivityDetail.Controls.Add(Me.pnlActivitytToolStrip)
        Me.pnlActivityDetail.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActivityDetail.Location = New System.Drawing.Point(304, 251)
        Me.pnlActivityDetail.Name = "pnlActivityDetail"
        Me.pnlActivityDetail.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlActivityDetail.Size = New System.Drawing.Size(846, 302)
        Me.pnlActivityDetail.TabIndex = 2
        '
        'pnlActivityDetailData
        '
        Me.pnlActivityDetailData.Controls.Add(Me.rbt_Inactive)
        Me.pnlActivityDetailData.Controls.Add(Me.rbt_Active)
        Me.pnlActivityDetailData.Controls.Add(Me.rbt_Complete)
        Me.pnlActivityDetailData.Controls.Add(Me.lblEncounter)
        Me.pnlActivityDetailData.Controls.Add(Me.lblLabOrders)
        Me.pnlActivityDetailData.Controls.Add(Me.lblNurtitionRecommendation)
        Me.pnlActivityDetailData.Controls.Add(Me.cmbProblemList)
        Me.pnlActivityDetailData.Controls.Add(Me.Label15)
        Me.pnlActivityDetailData.Controls.Add(Me.Label17)
        Me.pnlActivityDetailData.Controls.Add(Me.Label18)
        Me.pnlActivityDetailData.Controls.Add(Me.Label19)
        Me.pnlActivityDetailData.Controls.Add(Me.txtActivity)
        Me.pnlActivityDetailData.Controls.Add(Me.txtReason)
        Me.pnlActivityDetailData.Controls.Add(Me.Label20)
        Me.pnlActivityDetailData.Controls.Add(Me.Label21)
        Me.pnlActivityDetailData.Controls.Add(Me.lblMedication)
        Me.pnlActivityDetailData.Controls.Add(Me.btnClearSelectedProblem)
        Me.pnlActivityDetailData.Controls.Add(Me.Label23)
        Me.pnlActivityDetailData.Controls.Add(Me.btnGetPatientProblem)
        Me.pnlActivityDetailData.Controls.Add(Me.Label26)
        Me.pnlActivityDetailData.Controls.Add(Me.dtpActivityEffectiveFrom)
        Me.pnlActivityDetailData.Controls.Add(Me.Label28)
        Me.pnlActivityDetailData.Controls.Add(Me.dtpActivityEffectiveTo)
        Me.pnlActivityDetailData.Controls.Add(Me.txtSelectedCode)
        Me.pnlActivityDetailData.Controls.Add(Me.Label27)
        Me.pnlActivityDetailData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlActivityDetailData.Location = New System.Drawing.Point(0, 57)
        Me.pnlActivityDetailData.Name = "pnlActivityDetailData"
        Me.pnlActivityDetailData.Size = New System.Drawing.Size(843, 242)
        Me.pnlActivityDetailData.TabIndex = 53
        '
        'rbt_Inactive
        '
        Me.rbt_Inactive.AutoSize = True
        Me.rbt_Inactive.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_Inactive.Location = New System.Drawing.Point(273, 125)
        Me.rbt_Inactive.Name = "rbt_Inactive"
        Me.rbt_Inactive.Size = New System.Drawing.Size(68, 18)
        Me.rbt_Inactive.TabIndex = 60
        Me.rbt_Inactive.Text = "Inactive"
        Me.rbt_Inactive.UseVisualStyleBackColor = True
        '
        'rbt_Active
        '
        Me.rbt_Active.AutoSize = True
        Me.rbt_Active.Checked = True
        Me.rbt_Active.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_Active.Location = New System.Drawing.Point(181, 125)
        Me.rbt_Active.Name = "rbt_Active"
        Me.rbt_Active.Size = New System.Drawing.Size(63, 18)
        Me.rbt_Active.TabIndex = 58
        Me.rbt_Active.TabStop = True
        Me.rbt_Active.Text = "Active"
        Me.rbt_Active.UseVisualStyleBackColor = True
        '
        'rbt_Complete
        '
        Me.rbt_Complete.AutoSize = True
        Me.rbt_Complete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_Complete.Location = New System.Drawing.Point(375, 125)
        Me.rbt_Complete.Name = "rbt_Complete"
        Me.rbt_Complete.Size = New System.Drawing.Size(77, 18)
        Me.rbt_Complete.TabIndex = 59
        Me.rbt_Complete.Text = "Complete"
        Me.rbt_Complete.UseVisualStyleBackColor = True
        '
        'lblEncounter
        '
        Me.lblEncounter.AutoSize = True
        Me.lblEncounter.Location = New System.Drawing.Point(34, 18)
        Me.lblEncounter.Name = "lblEncounter"
        Me.lblEncounter.Size = New System.Drawing.Size(140, 14)
        Me.lblEncounter.TabIndex = 57
        Me.lblEncounter.Text = "Planned Encounter For :"
        '
        'lblLabOrders
        '
        Me.lblLabOrders.AutoSize = True
        Me.lblLabOrders.Location = New System.Drawing.Point(58, 17)
        Me.lblLabOrders.Name = "lblLabOrders"
        Me.lblLabOrders.Size = New System.Drawing.Size(116, 14)
        Me.lblLabOrders.TabIndex = 56
        Me.lblLabOrders.Text = "Planned Lab Order :"
        '
        'lblNurtitionRecommendation
        '
        Me.lblNurtitionRecommendation.AutoSize = True
        Me.lblNurtitionRecommendation.Location = New System.Drawing.Point(14, 17)
        Me.lblNurtitionRecommendation.Name = "lblNurtitionRecommendation"
        Me.lblNurtitionRecommendation.Size = New System.Drawing.Size(161, 14)
        Me.lblNurtitionRecommendation.TabIndex = 61
        Me.lblNurtitionRecommendation.Text = "Nurtition Recommendation :"
        '
        'cmbProblemList
        '
        Me.cmbProblemList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbProblemList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProblemList.FormattingEnabled = True
        Me.cmbProblemList.Location = New System.Drawing.Point(177, 203)
        Me.cmbProblemList.Name = "cmbProblemList"
        Me.cmbProblemList.Size = New System.Drawing.Size(590, 22)
        Me.cmbProblemList.TabIndex = 55
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1, 241)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(841, 1)
        Me.Label15.TabIndex = 54
        Me.Label15.Text = "label2"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(1, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(841, 1)
        Me.Label17.TabIndex = 53
        Me.Label17.Text = "label1"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(842, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 242)
        Me.Label18.TabIndex = 52
        Me.Label18.Text = "label3"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 242)
        Me.Label19.TabIndex = 51
        Me.Label19.Text = "label4"
        '
        'txtActivity
        '
        Me.txtActivity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtActivity.Location = New System.Drawing.Point(177, 43)
        Me.txtActivity.Multiline = True
        Me.txtActivity.Name = "txtActivity"
        Me.txtActivity.Size = New System.Drawing.Size(647, 46)
        Me.txtActivity.TabIndex = 8
        '
        'txtReason
        '
        Me.txtReason.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtReason.Location = New System.Drawing.Point(177, 150)
        Me.txtReason.Multiline = True
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(647, 46)
        Me.txtReason.TabIndex = 50
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(40, 46)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(134, 14)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Panned Activity Detail :"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(115, 207)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(59, 14)
        Me.Label21.TabIndex = 49
        Me.Label21.Text = "Problem :"
        '
        'lblMedication
        '
        Me.lblMedication.AutoSize = True
        Me.lblMedication.Location = New System.Drawing.Point(54, 17)
        Me.lblMedication.Name = "lblMedication"
        Me.lblMedication.Size = New System.Drawing.Size(120, 14)
        Me.lblMedication.TabIndex = 1
        Me.lblMedication.Text = "Planned Medication :"
        '
        'btnClearSelectedProblem
        '
        Me.btnClearSelectedProblem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearSelectedProblem.AutoEllipsis = True
        Me.btnClearSelectedProblem.BackColor = System.Drawing.Color.Transparent
        Me.btnClearSelectedProblem.BackgroundImage = CType(resources.GetObject("btnClearSelectedProblem.BackgroundImage"), System.Drawing.Image)
        Me.btnClearSelectedProblem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSelectedProblem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSelectedProblem.Image = CType(resources.GetObject("btnClearSelectedProblem.Image"), System.Drawing.Image)
        Me.btnClearSelectedProblem.Location = New System.Drawing.Point(799, 202)
        Me.btnClearSelectedProblem.Name = "btnClearSelectedProblem"
        Me.btnClearSelectedProblem.Size = New System.Drawing.Size(24, 24)
        Me.btnClearSelectedProblem.TabIndex = 48
        Me.btnClearSelectedProblem.UseVisualStyleBackColor = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(80, 100)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(94, 14)
        Me.Label23.TabIndex = 2
        Me.Label23.Text = "Effective From :"
        '
        'btnGetPatientProblem
        '
        Me.btnGetPatientProblem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGetPatientProblem.AutoEllipsis = True
        Me.btnGetPatientProblem.BackColor = System.Drawing.Color.Transparent
        Me.btnGetPatientProblem.BackgroundImage = CType(resources.GetObject("btnGetPatientProblem.BackgroundImage"), System.Drawing.Image)
        Me.btnGetPatientProblem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGetPatientProblem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGetPatientProblem.Image = CType(resources.GetObject("btnGetPatientProblem.Image"), System.Drawing.Image)
        Me.btnGetPatientProblem.Location = New System.Drawing.Point(772, 202)
        Me.btnGetPatientProblem.Name = "btnGetPatientProblem"
        Me.btnGetPatientProblem.Size = New System.Drawing.Size(24, 24)
        Me.btnGetPatientProblem.TabIndex = 47
        Me.btnGetPatientProblem.UseVisualStyleBackColor = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(288, 100)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(30, 14)
        Me.Label26.TabIndex = 7
        Me.Label26.Text = "To :"
        '
        'dtpActivityEffectiveFrom
        '
        Me.dtpActivityEffectiveFrom.Checked = False
        Me.dtpActivityEffectiveFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpActivityEffectiveFrom.Location = New System.Drawing.Point(177, 96)
        Me.dtpActivityEffectiveFrom.Name = "dtpActivityEffectiveFrom"
        Me.dtpActivityEffectiveFrom.ShowCheckBox = True
        Me.dtpActivityEffectiveFrom.Size = New System.Drawing.Size(105, 22)
        Me.dtpActivityEffectiveFrom.TabIndex = 9
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(8, 155)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(166, 14)
        Me.Label28.TabIndex = 45
        Me.Label28.Text = "Reason For Planned Activity :"
        '
        'dtpActivityEffectiveTo
        '
        Me.dtpActivityEffectiveTo.Checked = False
        Me.dtpActivityEffectiveTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpActivityEffectiveTo.Location = New System.Drawing.Point(324, 96)
        Me.dtpActivityEffectiveTo.Name = "dtpActivityEffectiveTo"
        Me.dtpActivityEffectiveTo.ShowCheckBox = True
        Me.dtpActivityEffectiveTo.Size = New System.Drawing.Size(105, 22)
        Me.dtpActivityEffectiveTo.TabIndex = 10
        '
        'txtSelectedCode
        '
        Me.txtSelectedCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelectedCode.BackColor = System.Drawing.Color.White
        Me.txtSelectedCode.Enabled = False
        Me.txtSelectedCode.Location = New System.Drawing.Point(177, 14)
        Me.txtSelectedCode.Name = "txtSelectedCode"
        Me.txtSelectedCode.ReadOnly = True
        Me.txtSelectedCode.Size = New System.Drawing.Size(647, 22)
        Me.txtSelectedCode.TabIndex = 11
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(124, 127)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(50, 14)
        Me.Label27.TabIndex = 12
        Me.Label27.Text = "Status :"
        '
        'pnlActivitytToolStrip
        '
        Me.pnlActivitytToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlActivitytToolStrip.Controls.Add(Me.tlsp_ActivityDetails)
        Me.pnlActivitytToolStrip.Controls.Add(Me.Label30)
        Me.pnlActivitytToolStrip.Controls.Add(Me.Label14)
        Me.pnlActivitytToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlActivitytToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlActivitytToolStrip.Name = "pnlActivitytToolStrip"
        Me.pnlActivitytToolStrip.Size = New System.Drawing.Size(843, 57)
        Me.pnlActivitytToolStrip.TabIndex = 54
        '
        'tlsp_ActivityDetails
        '
        Me.tlsp_ActivityDetails.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_ActivityDetails.BackgroundImage = CType(resources.GetObject("tlsp_ActivityDetails.BackgroundImage"), System.Drawing.Image)
        Me.tlsp_ActivityDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_ActivityDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlsp_ActivityDetails.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_ActivityDetails.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsp_btnActivitySave, Me.tlsp_btnActivityCancel})
        Me.tlsp_ActivityDetails.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_ActivityDetails.Location = New System.Drawing.Point(1, 1)
        Me.tlsp_ActivityDetails.Name = "tlsp_ActivityDetails"
        Me.tlsp_ActivityDetails.Size = New System.Drawing.Size(842, 53)
        Me.tlsp_ActivityDetails.TabIndex = 1
        Me.tlsp_ActivityDetails.Text = "toolStrip1"
        '
        'tlsp_btnActivitySave
        '
        Me.tlsp_btnActivitySave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_btnActivitySave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsp_btnActivitySave.Image = CType(resources.GetObject("tlsp_btnActivitySave.Image"), System.Drawing.Image)
        Me.tlsp_btnActivitySave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsp_btnActivitySave.Name = "tlsp_btnActivitySave"
        Me.tlsp_btnActivitySave.Size = New System.Drawing.Size(66, 50)
        Me.tlsp_btnActivitySave.Tag = "OK"
        Me.tlsp_btnActivitySave.Text = "&Save&&Cls"
        Me.tlsp_btnActivitySave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsp_btnActivitySave.ToolTipText = "Save and Close"
        '
        'tlsp_btnActivityCancel
        '
        Me.tlsp_btnActivityCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_btnActivityCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsp_btnActivityCancel.Image = CType(resources.GetObject("tlsp_btnActivityCancel.Image"), System.Drawing.Image)
        Me.tlsp_btnActivityCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsp_btnActivityCancel.Name = "tlsp_btnActivityCancel"
        Me.tlsp_btnActivityCancel.Size = New System.Drawing.Size(43, 50)
        Me.tlsp_btnActivityCancel.Tag = "Cancel"
        Me.tlsp_btnActivityCancel.Text = "&Close"
        Me.tlsp_btnActivityCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Location = New System.Drawing.Point(1, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(842, 1)
        Me.Label30.TabIndex = 9
        Me.Label30.Text = "label1"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 57)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "label4"
        '
        'pnlActivityType
        '
        Me.pnlActivityType.Controls.Add(Me.pnlControlContainer)
        Me.pnlActivityType.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlActivityType.Location = New System.Drawing.Point(0, 35)
        Me.pnlActivityType.Name = "pnlActivityType"
        Me.pnlActivityType.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlActivityType.Size = New System.Drawing.Size(304, 518)
        Me.pnlActivityType.TabIndex = 0
        '
        'pnlControlContainer
        '
        Me.pnlControlContainer.Controls.Add(Me.pnlNutrition)
        Me.pnlControlContainer.Controls.Add(Me.pnlLabOrdersList)
        Me.pnlControlContainer.Controls.Add(Me.pnlEncounterSnomed)
        Me.pnlControlContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlControlContainer.Location = New System.Drawing.Point(3, 0)
        Me.pnlControlContainer.Name = "pnlControlContainer"
        Me.pnlControlContainer.Size = New System.Drawing.Size(298, 515)
        Me.pnlControlContainer.TabIndex = 655555557
        '
        'pnlNutrition
        '
        Me.pnlNutrition.Controls.Add(Me.pnlNurtFinding)
        Me.pnlNutrition.Controls.Add(Me.pnlNurtSMSearch)
        Me.pnlNutrition.Controls.Add(Me.Panel9)
        Me.pnlNutrition.Controls.Add(Me.Panel10)
        Me.pnlNutrition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNutrition.Location = New System.Drawing.Point(0, 0)
        Me.pnlNutrition.Name = "pnlNutrition"
        Me.pnlNutrition.Size = New System.Drawing.Size(298, 515)
        Me.pnlNutrition.TabIndex = 59
        Me.pnlNutrition.Visible = False
        '
        'pnlNurtFinding
        '
        Me.pnlNurtFinding.Controls.Add(Me.trvNurtFindings)
        Me.pnlNurtFinding.Controls.Add(Me.Label25)
        Me.pnlNurtFinding.Controls.Add(Me.Label42)
        Me.pnlNurtFinding.Controls.Add(Me.Label56)
        Me.pnlNurtFinding.Controls.Add(Me.Label60)
        Me.pnlNurtFinding.Controls.Add(Me.Label64)
        Me.pnlNurtFinding.Controls.Add(Me.Label65)
        Me.pnlNurtFinding.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNurtFinding.Location = New System.Drawing.Point(0, 91)
        Me.pnlNurtFinding.Name = "pnlNurtFinding"
        Me.pnlNurtFinding.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlNurtFinding.Size = New System.Drawing.Size(298, 424)
        Me.pnlNurtFinding.TabIndex = 0
        '
        'trvNurtFindings
        '
        Me.trvNurtFindings.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvNurtFindings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvNurtFindings.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvNurtFindings.HideSelection = False
        Me.trvNurtFindings.ItemHeight = 19
        Me.trvNurtFindings.Location = New System.Drawing.Point(5, 9)
        Me.trvNurtFindings.Name = "trvNurtFindings"
        Me.trvNurtFindings.Size = New System.Drawing.Size(292, 414)
        Me.trvNurtFindings.TabIndex = 2
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.White
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label25.Location = New System.Drawing.Point(1, 9)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(4, 414)
        Me.Label25.TabIndex = 43
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.White
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label42.Location = New System.Drawing.Point(1, 4)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(296, 5)
        Me.Label42.TabIndex = 42
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(1, 423)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(296, 1)
        Me.Label56.TabIndex = 41
        Me.Label56.Text = "label1"
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(1, 3)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(296, 1)
        Me.Label60.TabIndex = 45
        Me.Label60.Text = "label1"
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(0, 3)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 421)
        Me.Label64.TabIndex = 46
        Me.Label64.Text = "label1"
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(297, 3)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1, 421)
        Me.Label65.TabIndex = 44
        Me.Label65.Text = "label1"
        '
        'pnlNurtSMSearch
        '
        Me.pnlNurtSMSearch.BackColor = System.Drawing.Color.White
        Me.pnlNurtSMSearch.Controls.Add(Me.txtNurtSMSearch)
        Me.pnlNurtSMSearch.Controls.Add(Me.Label66)
        Me.pnlNurtSMSearch.Controls.Add(Me.Label67)
        Me.pnlNurtSMSearch.Controls.Add(Me.btnNurtClear)
        Me.pnlNurtSMSearch.Controls.Add(Me.PicBxNurt_Search)
        Me.pnlNurtSMSearch.Controls.Add(Me.Label68)
        Me.pnlNurtSMSearch.Controls.Add(Me.Label69)
        Me.pnlNurtSMSearch.Controls.Add(Me.Label70)
        Me.pnlNurtSMSearch.Controls.Add(Me.Label71)
        Me.pnlNurtSMSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlNurtSMSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlNurtSMSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlNurtSMSearch.Location = New System.Drawing.Point(0, 63)
        Me.pnlNurtSMSearch.Name = "pnlNurtSMSearch"
        Me.pnlNurtSMSearch.Size = New System.Drawing.Size(298, 28)
        Me.pnlNurtSMSearch.TabIndex = 28
        '
        'txtNurtSMSearch
        '
        Me.txtNurtSMSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNurtSMSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNurtSMSearch.Location = New System.Drawing.Point(34, 4)
        Me.txtNurtSMSearch.Name = "txtNurtSMSearch"
        Me.txtNurtSMSearch.Size = New System.Drawing.Size(239, 15)
        Me.txtNurtSMSearch.TabIndex = 42
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.White
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.Location = New System.Drawing.Point(34, 1)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(239, 3)
        Me.Label66.TabIndex = 37
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.White
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label67.Location = New System.Drawing.Point(34, 22)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(239, 5)
        Me.Label67.TabIndex = 38
        '
        'btnNurtClear
        '
        Me.btnNurtClear.BackgroundImage = CType(resources.GetObject("btnNurtClear.BackgroundImage"), System.Drawing.Image)
        Me.btnNurtClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNurtClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNurtClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnNurtClear.FlatAppearance.BorderSize = 0
        Me.btnNurtClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnNurtClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnNurtClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNurtClear.Image = CType(resources.GetObject("btnNurtClear.Image"), System.Drawing.Image)
        Me.btnNurtClear.Location = New System.Drawing.Point(273, 1)
        Me.btnNurtClear.Name = "btnNurtClear"
        Me.btnNurtClear.Size = New System.Drawing.Size(24, 26)
        Me.btnNurtClear.TabIndex = 41
        Me.btnNurtClear.UseVisualStyleBackColor = True
        '
        'PicBxNurt_Search
        '
        Me.PicBxNurt_Search.BackColor = System.Drawing.Color.White
        Me.PicBxNurt_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBxNurt_Search.Image = CType(resources.GetObject("PicBxNurt_Search.Image"), System.Drawing.Image)
        Me.PicBxNurt_Search.Location = New System.Drawing.Point(1, 1)
        Me.PicBxNurt_Search.Name = "PicBxNurt_Search"
        Me.PicBxNurt_Search.Size = New System.Drawing.Size(33, 26)
        Me.PicBxNurt_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBxNurt_Search.TabIndex = 9
        Me.PicBxNurt_Search.TabStop = False
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label68.Location = New System.Drawing.Point(1, 27)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(296, 1)
        Me.Label68.TabIndex = 35
        Me.Label68.Text = "label1"
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label69.Location = New System.Drawing.Point(1, 0)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(296, 1)
        Me.Label69.TabIndex = 36
        Me.Label69.Text = "label1"
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label70.Location = New System.Drawing.Point(0, 0)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(1, 28)
        Me.Label70.TabIndex = 39
        Me.Label70.Text = "label4"
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label71.Location = New System.Drawing.Point(297, 0)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(1, 28)
        Me.Label71.TabIndex = 40
        Me.Label71.Text = "label4"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Transparent
        Me.Panel9.BackgroundImage = CType(resources.GetObject("Panel9.BackgroundImage"), System.Drawing.Image)
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.chkNurtCOREProblem)
        Me.Panel9.Controls.Add(Me.Label72)
        Me.Panel9.Controls.Add(Me.Label73)
        Me.Panel9.Controls.Add(Me.Label74)
        Me.Panel9.Controls.Add(Me.Label75)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 33)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel9.Size = New System.Drawing.Size(298, 30)
        Me.Panel9.TabIndex = 33
        '
        'chkNurtCOREProblem
        '
        Me.chkNurtCOREProblem.AutoSize = True
        Me.chkNurtCOREProblem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNurtCOREProblem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkNurtCOREProblem.Location = New System.Drawing.Point(9, 4)
        Me.chkNurtCOREProblem.Name = "chkNurtCOREProblem"
        Me.chkNurtCOREProblem.Size = New System.Drawing.Size(98, 18)
        Me.chkNurtCOREProblem.TabIndex = 43
        Me.chkNurtCOREProblem.Text = "CORE Subset"
        Me.chkNurtCOREProblem.UseVisualStyleBackColor = True
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.Location = New System.Drawing.Point(1, 26)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(296, 1)
        Me.Label72.TabIndex = 32
        Me.Label72.Text = "label1"
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(1, 0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(296, 1)
        Me.Label73.TabIndex = 6
        Me.Label73.Text = "label1"
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.Location = New System.Drawing.Point(297, 0)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(1, 27)
        Me.Label74.TabIndex = 8
        Me.Label74.Text = "label1"
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.Location = New System.Drawing.Point(0, 0)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(1, 27)
        Me.Label75.TabIndex = 9
        Me.Label75.Text = "label1"
        '
        'Panel10
        '
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Panel11)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel10.Size = New System.Drawing.Size(298, 33)
        Me.Panel10.TabIndex = 30
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.Transparent
        Me.Panel11.BackgroundImage = CType(resources.GetObject("Panel11.BackgroundImage"), System.Drawing.Image)
        Me.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel11.Controls.Add(Me.Label76)
        Me.Panel11.Controls.Add(Me.Label77)
        Me.Panel11.Controls.Add(Me.Label78)
        Me.Panel11.Controls.Add(Me.Label79)
        Me.Panel11.Controls.Add(Me.Label80)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(0, 3)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(298, 27)
        Me.Panel11.TabIndex = 32
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.Location = New System.Drawing.Point(1, 0)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(296, 1)
        Me.Label76.TabIndex = 33
        Me.Label76.Text = "label1"
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.Location = New System.Drawing.Point(1, 26)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(296, 1)
        Me.Label77.TabIndex = 32
        Me.Label77.Text = "label1"
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.Transparent
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Location = New System.Drawing.Point(1, 0)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(296, 27)
        Me.Label78.TabIndex = 31
        Me.Label78.Text = " Enter search text, then select SNOMED-CT"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.Location = New System.Drawing.Point(297, 0)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(1, 27)
        Me.Label79.TabIndex = 8
        Me.Label79.Text = "label1"
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.Location = New System.Drawing.Point(0, 0)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(1, 27)
        Me.Label80.TabIndex = 9
        Me.Label80.Text = "label1"
        '
        'pnlLabOrdersList
        '
        Me.pnlLabOrdersList.Controls.Add(Me.pnltrvList)
        Me.pnlLabOrdersList.Controls.Add(Me.pnlLabTests)
        Me.pnlLabOrdersList.Controls.Add(Me.pnl_btnTests)
        Me.pnlLabOrdersList.Controls.Add(Me.pnl_btnRadiologyImaging)
        Me.pnlLabOrdersList.Controls.Add(Me.pnl_btnRefTest)
        Me.pnlLabOrdersList.Controls.Add(Me.pnl_btnOthers)
        Me.pnlLabOrdersList.Controls.Add(Me.pnl_btnGroups)
        Me.pnlLabOrdersList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLabOrdersList.Location = New System.Drawing.Point(0, 0)
        Me.pnlLabOrdersList.Name = "pnlLabOrdersList"
        Me.pnlLabOrdersList.Size = New System.Drawing.Size(298, 515)
        Me.pnlLabOrdersList.TabIndex = 57
        Me.pnlLabOrdersList.Visible = False
        '
        'pnltrvList
        '
        Me.pnltrvList.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrvList.Controls.Add(Me.GloUC_trvTest)
        Me.pnltrvList.Controls.Add(Me.panel8)
        Me.pnltrvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvList.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnltrvList.Location = New System.Drawing.Point(0, 32)
        Me.pnltrvList.Name = "pnltrvList"
        Me.pnltrvList.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnltrvList.Size = New System.Drawing.Size(298, 323)
        Me.pnltrvList.TabIndex = 5
        '
        'GloUC_trvTest
        '
        Me.GloUC_trvTest.AllergyClassID = Nothing
        Me.GloUC_trvTest.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvTest.CheckBoxes = False
        Me.GloUC_trvTest.CodeMember = Nothing
        Me.GloUC_trvTest.ColonAsSeparator = False
        Me.GloUC_trvTest.Comment = Nothing
        Me.GloUC_trvTest.ConceptID = Nothing
        Me.GloUC_trvTest.CPT = Nothing
        Me.GloUC_trvTest.CQMDESC = Nothing
        Me.GloUC_trvTest.CQMID = Nothing
        Me.GloUC_trvTest.DDIDMember = Nothing
        Me.GloUC_trvTest.DescriptionMember = Nothing
        Me.GloUC_trvTest.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
        Me.GloUC_trvTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvTest.DrugFlag = CType(16, Short)
        Me.GloUC_trvTest.DrugFormMember = Nothing
        Me.GloUC_trvTest.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvTest.DurationMember = Nothing
        Me.GloUC_trvTest.EducationMappingSearchType = 1
        Me.GloUC_trvTest.FrequencyMember = Nothing
        Me.GloUC_trvTest.HistoryType = Nothing
        Me.GloUC_trvTest.ICD9 = Nothing
        Me.GloUC_trvTest.ICDRevision = Nothing
        Me.GloUC_trvTest.ImageIndex = -1
        Me.GloUC_trvTest.ImageList = Nothing
        Me.GloUC_trvTest.ImageObject = Nothing
        Me.GloUC_trvTest.Indicator = Nothing
        Me.GloUC_trvTest.IsCPTSearch = False
        Me.GloUC_trvTest.IsDiagnosisSearch = False
        Me.GloUC_trvTest.IsDrug = False
        Me.GloUC_trvTest.IsNarcoticsMember = Nothing
        Me.GloUC_trvTest.IsSearchForEducationMapping = False
        Me.GloUC_trvTest.IsSystemCategory = Nothing
        Me.GloUC_trvTest.Location = New System.Drawing.Point(0, 33)
        Me.GloUC_trvTest.MaximumNodes = 1000
        Me.GloUC_trvTest.mpidmember = Nothing
        Me.GloUC_trvTest.Name = "GloUC_trvTest"
        Me.GloUC_trvTest.NDCCodeMember = Nothing
        Me.GloUC_trvTest.ParentImageIndex = 0
        Me.GloUC_trvTest.ParentMember = Nothing
        Me.GloUC_trvTest.RouteMember = Nothing
        Me.GloUC_trvTest.RowOrderMember = Nothing
        Me.GloUC_trvTest.RxNormCode = Nothing
        Me.GloUC_trvTest.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvTest.SearchBox = True
        Me.GloUC_trvTest.SearchText = Nothing
        Me.GloUC_trvTest.SelectedImageIndex = -1
        Me.GloUC_trvTest.SelectedNode = Nothing
        Me.GloUC_trvTest.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvTest.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvTest.SelectedParentImageIndex = 0
        Me.GloUC_trvTest.Size = New System.Drawing.Size(298, 290)
        Me.GloUC_trvTest.SmartTreatmentId = Nothing
        Me.GloUC_trvTest.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvTest.TabIndex = 42
        Me.GloUC_trvTest.Tag = Nothing
        Me.GloUC_trvTest.UnitMember = Nothing
        Me.GloUC_trvTest.ValueMember = Nothing
        '
        'panel8
        '
        Me.panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.panel8.Controls.Add(Me.Label16)
        Me.panel8.Controls.Add(Me.chkIncludeTestCode)
        Me.panel8.Controls.Add(Me.label39)
        Me.panel8.Controls.Add(Me.label40)
        Me.panel8.Controls.Add(Me.label41)
        Me.panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.panel8.Location = New System.Drawing.Point(0, 3)
        Me.panel8.Name = "panel8"
        Me.panel8.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.panel8.Size = New System.Drawing.Size(298, 30)
        Me.panel8.TabIndex = 43
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(1, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(296, 1)
        Me.Label16.TabIndex = 20
        Me.Label16.Text = "label2"
        '
        'chkIncludeTestCode
        '
        Me.chkIncludeTestCode.AutoSize = True
        Me.chkIncludeTestCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludeTestCode.Location = New System.Drawing.Point(9, 4)
        Me.chkIncludeTestCode.Name = "chkIncludeTestCode"
        Me.chkIncludeTestCode.Size = New System.Drawing.Size(127, 18)
        Me.chkIncludeTestCode.TabIndex = 19
        Me.chkIncludeTestCode.Text = "Include Test Code"
        Me.chkIncludeTestCode.UseVisualStyleBackColor = True
        '
        'label39
        '
        Me.label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label39.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label39.Location = New System.Drawing.Point(1, 26)
        Me.label39.Name = "label39"
        Me.label39.Size = New System.Drawing.Size(296, 1)
        Me.label39.TabIndex = 18
        Me.label39.Text = "label2"
        '
        'label40
        '
        Me.label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label40.Dock = System.Windows.Forms.DockStyle.Left
        Me.label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label40.Location = New System.Drawing.Point(0, 0)
        Me.label40.Name = "label40"
        Me.label40.Size = New System.Drawing.Size(1, 27)
        Me.label40.TabIndex = 17
        Me.label40.Text = "label4"
        '
        'label41
        '
        Me.label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label41.Dock = System.Windows.Forms.DockStyle.Right
        Me.label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label41.Location = New System.Drawing.Point(297, 0)
        Me.label41.Name = "label41"
        Me.label41.Size = New System.Drawing.Size(1, 27)
        Me.label41.TabIndex = 16
        Me.label41.Text = "label3"
        '
        'pnlLabTests
        '
        Me.pnlLabTests.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLabTests.Controls.Add(Me.Label43)
        Me.pnlLabTests.Controls.Add(Me.btnLabTests)
        Me.pnlLabTests.Controls.Add(Me.Label44)
        Me.pnlLabTests.Controls.Add(Me.Label45)
        Me.pnlLabTests.Controls.Add(Me.Label46)
        Me.pnlLabTests.Controls.Add(Me.Label47)
        Me.pnlLabTests.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlLabTests.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlLabTests.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlLabTests.Location = New System.Drawing.Point(0, 355)
        Me.pnlLabTests.Name = "pnlLabTests"
        Me.pnlLabTests.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlLabTests.Size = New System.Drawing.Size(298, 32)
        Me.pnlLabTests.TabIndex = 20
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(0, 4)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 27)
        Me.Label43.TabIndex = 19
        Me.Label43.Text = "label4"
        '
        'btnLabTests
        '
        Me.btnLabTests.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnLabTests.BackgroundImage = CType(resources.GetObject("btnLabTests.BackgroundImage"), System.Drawing.Image)
        Me.btnLabTests.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabTests.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLabTests.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnLabTests.FlatAppearance.BorderSize = 0
        Me.btnLabTests.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabTests.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLabTests.Location = New System.Drawing.Point(0, 4)
        Me.btnLabTests.Name = "btnLabTests"
        Me.btnLabTests.Size = New System.Drawing.Size(297, 27)
        Me.btnLabTests.TabIndex = 0
        Me.btnLabTests.Tag = "5"
        Me.btnLabTests.Text = "Lab Tests"
        Me.btnLabTests.UseVisualStyleBackColor = False
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 31)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(297, 1)
        Me.Label44.TabIndex = 15
        Me.Label44.Text = "Label44"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(0, 4)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(0, 28)
        Me.Label45.TabIndex = 18
        Me.Label45.Text = "Label45"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label46.Location = New System.Drawing.Point(297, 4)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 28)
        Me.Label46.TabIndex = 16
        Me.Label46.Text = "Label46"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(0, 3)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(298, 1)
        Me.Label47.TabIndex = 17
        Me.Label47.Text = "Label47"
        '
        'pnl_btnTests
        '
        Me.pnl_btnTests.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_btnTests.Controls.Add(Me.btnTests)
        Me.pnl_btnTests.Controls.Add(Me.Label9)
        Me.pnl_btnTests.Controls.Add(Me.Label11)
        Me.pnl_btnTests.Controls.Add(Me.Label12)
        Me.pnl_btnTests.Controls.Add(Me.Label13)
        Me.pnl_btnTests.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_btnTests.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnTests.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnTests.Location = New System.Drawing.Point(0, 0)
        Me.pnl_btnTests.Name = "pnl_btnTests"
        Me.pnl_btnTests.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_btnTests.Size = New System.Drawing.Size(298, 32)
        Me.pnl_btnTests.TabIndex = 4
        '
        'btnTests
        '
        Me.btnTests.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnTests.BackgroundImage = CType(resources.GetObject("btnTests.BackgroundImage"), System.Drawing.Image)
        Me.btnTests.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTests.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTests.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTests.FlatAppearance.BorderSize = 0
        Me.btnTests.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTests.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTests.Location = New System.Drawing.Point(1, 4)
        Me.btnTests.Name = "btnTests"
        Me.btnTests.Size = New System.Drawing.Size(296, 27)
        Me.btnTests.TabIndex = 0
        Me.btnTests.Tag = ""
        Me.btnTests.Text = "Lab Orders"
        Me.btnTests.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(1, 31)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(296, 1)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 28)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(297, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 28)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(298, 1)
        Me.Label13.TabIndex = 15
        Me.Label13.Text = "label1"
        '
        'pnl_btnRadiologyImaging
        '
        Me.pnl_btnRadiologyImaging.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_btnRadiologyImaging.Controls.Add(Me.label36)
        Me.pnl_btnRadiologyImaging.Controls.Add(Me.btnRadiologyImaging)
        Me.pnl_btnRadiologyImaging.Controls.Add(Me.lblRadiologyImaging1)
        Me.pnl_btnRadiologyImaging.Controls.Add(Me.lblRadiologyImaging2)
        Me.pnl_btnRadiologyImaging.Controls.Add(Me.lblRadiologyImaging4)
        Me.pnl_btnRadiologyImaging.Controls.Add(Me.lblRadiologyImaging3)
        Me.pnl_btnRadiologyImaging.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnRadiologyImaging.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnRadiologyImaging.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnRadiologyImaging.Location = New System.Drawing.Point(0, 387)
        Me.pnl_btnRadiologyImaging.Name = "pnl_btnRadiologyImaging"
        Me.pnl_btnRadiologyImaging.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_btnRadiologyImaging.Size = New System.Drawing.Size(298, 32)
        Me.pnl_btnRadiologyImaging.TabIndex = 4
        '
        'label36
        '
        Me.label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label36.Dock = System.Windows.Forms.DockStyle.Left
        Me.label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label36.Location = New System.Drawing.Point(0, 4)
        Me.label36.Name = "label36"
        Me.label36.Size = New System.Drawing.Size(1, 27)
        Me.label36.TabIndex = 19
        Me.label36.Text = "label4"
        '
        'btnRadiologyImaging
        '
        Me.btnRadiologyImaging.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnRadiologyImaging.BackgroundImage = CType(resources.GetObject("btnRadiologyImaging.BackgroundImage"), System.Drawing.Image)
        Me.btnRadiologyImaging.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRadiologyImaging.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRadiologyImaging.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRadiologyImaging.FlatAppearance.BorderSize = 0
        Me.btnRadiologyImaging.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRadiologyImaging.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRadiologyImaging.Location = New System.Drawing.Point(0, 4)
        Me.btnRadiologyImaging.Name = "btnRadiologyImaging"
        Me.btnRadiologyImaging.Size = New System.Drawing.Size(297, 27)
        Me.btnRadiologyImaging.TabIndex = 0
        Me.btnRadiologyImaging.Tag = "1"
        Me.btnRadiologyImaging.Text = "Radiology/Imaging"
        Me.btnRadiologyImaging.UseVisualStyleBackColor = False
        '
        'lblRadiologyImaging1
        '
        Me.lblRadiologyImaging1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRadiologyImaging1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblRadiologyImaging1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRadiologyImaging1.Location = New System.Drawing.Point(0, 31)
        Me.lblRadiologyImaging1.Name = "lblRadiologyImaging1"
        Me.lblRadiologyImaging1.Size = New System.Drawing.Size(297, 1)
        Me.lblRadiologyImaging1.TabIndex = 15
        Me.lblRadiologyImaging1.Text = "lblRadiologyImaging1"
        '
        'lblRadiologyImaging2
        '
        Me.lblRadiologyImaging2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRadiologyImaging2.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblRadiologyImaging2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblRadiologyImaging2.Location = New System.Drawing.Point(0, 4)
        Me.lblRadiologyImaging2.Name = "lblRadiologyImaging2"
        Me.lblRadiologyImaging2.Size = New System.Drawing.Size(0, 28)
        Me.lblRadiologyImaging2.TabIndex = 18
        Me.lblRadiologyImaging2.Text = "lblRadiologyImaging2"
        '
        'lblRadiologyImaging4
        '
        Me.lblRadiologyImaging4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRadiologyImaging4.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblRadiologyImaging4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblRadiologyImaging4.Location = New System.Drawing.Point(297, 4)
        Me.lblRadiologyImaging4.Name = "lblRadiologyImaging4"
        Me.lblRadiologyImaging4.Size = New System.Drawing.Size(1, 28)
        Me.lblRadiologyImaging4.TabIndex = 16
        Me.lblRadiologyImaging4.Text = "lblRadiologyImaging4"
        '
        'lblRadiologyImaging3
        '
        Me.lblRadiologyImaging3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRadiologyImaging3.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblRadiologyImaging3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRadiologyImaging3.Location = New System.Drawing.Point(0, 3)
        Me.lblRadiologyImaging3.Name = "lblRadiologyImaging3"
        Me.lblRadiologyImaging3.Size = New System.Drawing.Size(298, 1)
        Me.lblRadiologyImaging3.TabIndex = 17
        Me.lblRadiologyImaging3.Text = "lblRadiologyImaging3"
        '
        'pnl_btnRefTest
        '
        Me.pnl_btnRefTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_btnRefTest.Controls.Add(Me.btnRefTest)
        Me.pnl_btnRefTest.Controls.Add(Me.Label22)
        Me.pnl_btnRefTest.Controls.Add(Me.Label29)
        Me.pnl_btnRefTest.Controls.Add(Me.Label31)
        Me.pnl_btnRefTest.Controls.Add(Me.Label32)
        Me.pnl_btnRefTest.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnRefTest.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnRefTest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnRefTest.Location = New System.Drawing.Point(0, 419)
        Me.pnl_btnRefTest.Name = "pnl_btnRefTest"
        Me.pnl_btnRefTest.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_btnRefTest.Size = New System.Drawing.Size(298, 32)
        Me.pnl_btnRefTest.TabIndex = 4
        '
        'btnRefTest
        '
        Me.btnRefTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnRefTest.BackgroundImage = CType(resources.GetObject("btnRefTest.BackgroundImage"), System.Drawing.Image)
        Me.btnRefTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRefTest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRefTest.FlatAppearance.BorderSize = 0
        Me.btnRefTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefTest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRefTest.Location = New System.Drawing.Point(1, 4)
        Me.btnRefTest.Name = "btnRefTest"
        Me.btnRefTest.Size = New System.Drawing.Size(296, 27)
        Me.btnRefTest.TabIndex = 2
        Me.btnRefTest.Tag = "2"
        Me.btnRefTest.Text = "Referrals"
        Me.btnRefTest.UseVisualStyleBackColor = False
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(1, 31)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(296, 1)
        Me.Label22.TabIndex = 18
        Me.Label22.Text = "label2"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 4)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 28)
        Me.Label29.TabIndex = 17
        Me.Label29.Text = "label4"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label31.Location = New System.Drawing.Point(297, 4)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 28)
        Me.Label31.TabIndex = 16
        Me.Label31.Text = "label3"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(0, 3)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(298, 1)
        Me.Label32.TabIndex = 15
        Me.Label32.Text = "label1"
        '
        'pnl_btnOthers
        '
        Me.pnl_btnOthers.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_btnOthers.Controls.Add(Me.label37)
        Me.pnl_btnOthers.Controls.Add(Me.btnOthers)
        Me.pnl_btnOthers.Controls.Add(Me.lblOthers1)
        Me.pnl_btnOthers.Controls.Add(Me.lblOthers2)
        Me.pnl_btnOthers.Controls.Add(Me.lblOthers3)
        Me.pnl_btnOthers.Controls.Add(Me.lblOthers4)
        Me.pnl_btnOthers.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnOthers.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnOthers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnOthers.Location = New System.Drawing.Point(0, 451)
        Me.pnl_btnOthers.Name = "pnl_btnOthers"
        Me.pnl_btnOthers.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_btnOthers.Size = New System.Drawing.Size(298, 32)
        Me.pnl_btnOthers.TabIndex = 6
        '
        'label37
        '
        Me.label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label37.Dock = System.Windows.Forms.DockStyle.Left
        Me.label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label37.Location = New System.Drawing.Point(0, 4)
        Me.label37.Name = "label37"
        Me.label37.Size = New System.Drawing.Size(1, 27)
        Me.label37.TabIndex = 19
        Me.label37.Text = "label4"
        '
        'btnOthers
        '
        Me.btnOthers.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnOthers.BackgroundImage = CType(resources.GetObject("btnOthers.BackgroundImage"), System.Drawing.Image)
        Me.btnOthers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOthers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOthers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnOthers.FlatAppearance.BorderSize = 0
        Me.btnOthers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOthers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOthers.Location = New System.Drawing.Point(0, 4)
        Me.btnOthers.Name = "btnOthers"
        Me.btnOthers.Size = New System.Drawing.Size(297, 27)
        Me.btnOthers.TabIndex = 0
        Me.btnOthers.Tag = "3"
        Me.btnOthers.Text = "Other"
        Me.btnOthers.UseVisualStyleBackColor = False
        '
        'lblOthers1
        '
        Me.lblOthers1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblOthers1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblOthers1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOthers1.Location = New System.Drawing.Point(0, 31)
        Me.lblOthers1.Name = "lblOthers1"
        Me.lblOthers1.Size = New System.Drawing.Size(297, 1)
        Me.lblOthers1.TabIndex = 15
        Me.lblOthers1.Text = "label36"
        '
        'lblOthers2
        '
        Me.lblOthers2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblOthers2.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblOthers2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblOthers2.Location = New System.Drawing.Point(0, 4)
        Me.lblOthers2.Name = "lblOthers2"
        Me.lblOthers2.Size = New System.Drawing.Size(0, 28)
        Me.lblOthers2.TabIndex = 18
        Me.lblOthers2.Text = "label37"
        '
        'lblOthers3
        '
        Me.lblOthers3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblOthers3.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblOthers3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblOthers3.Location = New System.Drawing.Point(297, 4)
        Me.lblOthers3.Name = "lblOthers3"
        Me.lblOthers3.Size = New System.Drawing.Size(1, 28)
        Me.lblOthers3.TabIndex = 16
        Me.lblOthers3.Text = "label38"
        '
        'lblOthers4
        '
        Me.lblOthers4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblOthers4.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblOthers4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOthers4.Location = New System.Drawing.Point(0, 3)
        Me.lblOthers4.Name = "lblOthers4"
        Me.lblOthers4.Size = New System.Drawing.Size(298, 1)
        Me.lblOthers4.TabIndex = 17
        Me.lblOthers4.Text = "label39"
        '
        'pnl_btnGroups
        '
        Me.pnl_btnGroups.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_btnGroups.Controls.Add(Me.label35)
        Me.pnl_btnGroups.Controls.Add(Me.label34)
        Me.pnl_btnGroups.Controls.Add(Me.label33)
        Me.pnl_btnGroups.Controls.Add(Me.Label38)
        Me.pnl_btnGroups.Controls.Add(Me.btnGroups)
        Me.pnl_btnGroups.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnGroups.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnGroups.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnGroups.Location = New System.Drawing.Point(0, 483)
        Me.pnl_btnGroups.Name = "pnl_btnGroups"
        Me.pnl_btnGroups.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_btnGroups.Size = New System.Drawing.Size(298, 32)
        Me.pnl_btnGroups.TabIndex = 4
        '
        'label35
        '
        Me.label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label35.Dock = System.Windows.Forms.DockStyle.Top
        Me.label35.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label35.Location = New System.Drawing.Point(1, 3)
        Me.label35.Name = "label35"
        Me.label35.Size = New System.Drawing.Size(296, 1)
        Me.label35.TabIndex = 21
        Me.label35.Text = "label2"
        '
        'label34
        '
        Me.label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label34.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label34.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label34.Location = New System.Drawing.Point(1, 31)
        Me.label34.Name = "label34"
        Me.label34.Size = New System.Drawing.Size(296, 1)
        Me.label34.TabIndex = 20
        Me.label34.Text = "label2"
        '
        'label33
        '
        Me.label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label33.Dock = System.Windows.Forms.DockStyle.Right
        Me.label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label33.Location = New System.Drawing.Point(297, 3)
        Me.label33.Name = "label33"
        Me.label33.Size = New System.Drawing.Size(1, 29)
        Me.label33.TabIndex = 19
        Me.label33.Text = "label4"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(0, 3)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 29)
        Me.Label38.TabIndex = 18
        Me.Label38.Text = "label4"
        '
        'btnGroups
        '
        Me.btnGroups.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnGroups.BackgroundImage = CType(resources.GetObject("btnGroups.BackgroundImage"), System.Drawing.Image)
        Me.btnGroups.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGroups.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGroups.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnGroups.FlatAppearance.BorderSize = 0
        Me.btnGroups.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGroups.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGroups.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnGroups.Location = New System.Drawing.Point(0, 3)
        Me.btnGroups.Name = "btnGroups"
        Me.btnGroups.Size = New System.Drawing.Size(298, 29)
        Me.btnGroups.TabIndex = 2
        Me.btnGroups.Tag = "4"
        Me.btnGroups.Text = "Groups"
        Me.btnGroups.UseVisualStyleBackColor = False
        '
        'pnlEncounterSnomed
        '
        Me.pnlEncounterSnomed.Controls.Add(Me.pnlfinding)
        Me.pnlEncounterSnomed.Controls.Add(Me.pnlSMSearch)
        Me.pnlEncounterSnomed.Controls.Add(Me.Panel1)
        Me.pnlEncounterSnomed.Controls.Add(Me.Panel4)
        Me.pnlEncounterSnomed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEncounterSnomed.Location = New System.Drawing.Point(0, 0)
        Me.pnlEncounterSnomed.Name = "pnlEncounterSnomed"
        Me.pnlEncounterSnomed.Size = New System.Drawing.Size(298, 515)
        Me.pnlEncounterSnomed.TabIndex = 58
        Me.pnlEncounterSnomed.Visible = False
        '
        'pnlfinding
        '
        Me.pnlfinding.Controls.Add(Me.trvFindings)
        Me.pnlfinding.Controls.Add(Me.Label48)
        Me.pnlfinding.Controls.Add(Me.Label49)
        Me.pnlfinding.Controls.Add(Me.Label50)
        Me.pnlfinding.Controls.Add(Me.Label51)
        Me.pnlfinding.Controls.Add(Me.Label52)
        Me.pnlfinding.Controls.Add(Me.Label53)
        Me.pnlfinding.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlfinding.Location = New System.Drawing.Point(0, 91)
        Me.pnlfinding.Name = "pnlfinding"
        Me.pnlfinding.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlfinding.Size = New System.Drawing.Size(298, 424)
        Me.pnlfinding.TabIndex = 0
        '
        'trvFindings
        '
        Me.trvFindings.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvFindings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvFindings.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvFindings.HideSelection = False
        Me.trvFindings.ItemHeight = 19
        Me.trvFindings.Location = New System.Drawing.Point(5, 9)
        Me.trvFindings.Name = "trvFindings"
        Me.trvFindings.Size = New System.Drawing.Size(292, 414)
        Me.trvFindings.TabIndex = 2
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.White
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label48.Location = New System.Drawing.Point(1, 9)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(4, 414)
        Me.Label48.TabIndex = 43
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.White
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label49.Location = New System.Drawing.Point(1, 4)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(296, 5)
        Me.Label49.TabIndex = 42
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(1, 423)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(296, 1)
        Me.Label50.TabIndex = 41
        Me.Label50.Text = "label1"
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(1, 3)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(296, 1)
        Me.Label51.TabIndex = 45
        Me.Label51.Text = "label1"
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(0, 3)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(1, 421)
        Me.Label52.TabIndex = 46
        Me.Label52.Text = "label1"
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(297, 3)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1, 421)
        Me.Label53.TabIndex = 44
        Me.Label53.Text = "label1"
        '
        'pnlSMSearch
        '
        Me.pnlSMSearch.BackColor = System.Drawing.Color.White
        Me.pnlSMSearch.Controls.Add(Me.txtSMSearch)
        Me.pnlSMSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSMSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSMSearch.Controls.Add(Me.btnClear)
        Me.pnlSMSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSMSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSMSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSMSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSMSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSMSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSMSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSMSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSMSearch.Location = New System.Drawing.Point(0, 63)
        Me.pnlSMSearch.Name = "pnlSMSearch"
        Me.pnlSMSearch.Size = New System.Drawing.Size(298, 28)
        Me.pnlSMSearch.TabIndex = 28
        '
        'txtSMSearch
        '
        Me.txtSMSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSMSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSMSearch.Location = New System.Drawing.Point(34, 4)
        Me.txtSMSearch.Name = "txtSMSearch"
        Me.txtSMSearch.Size = New System.Drawing.Size(239, 15)
        Me.txtSMSearch.TabIndex = 42
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(34, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(239, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(34, 22)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(239, 5)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(273, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(24, 26)
        Me.btnClear.TabIndex = 41
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(1, 1)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(33, 26)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(1, 27)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(296, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(1, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(296, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 28)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(297, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 28)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.chkCOREProblem)
        Me.Panel1.Controls.Add(Me.Label59)
        Me.Panel1.Controls.Add(Me.Label61)
        Me.Panel1.Controls.Add(Me.Label62)
        Me.Panel1.Controls.Add(Me.Label63)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 33)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(298, 30)
        Me.Panel1.TabIndex = 33
        '
        'chkCOREProblem
        '
        Me.chkCOREProblem.AutoSize = True
        Me.chkCOREProblem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOREProblem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkCOREProblem.Location = New System.Drawing.Point(9, 4)
        Me.chkCOREProblem.Name = "chkCOREProblem"
        Me.chkCOREProblem.Size = New System.Drawing.Size(98, 18)
        Me.chkCOREProblem.TabIndex = 43
        Me.chkCOREProblem.Text = "CORE Subset"
        Me.chkCOREProblem.UseVisualStyleBackColor = True
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(1, 26)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(296, 1)
        Me.Label59.TabIndex = 32
        Me.Label59.Text = "label1"
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(1, 0)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(296, 1)
        Me.Label61.TabIndex = 6
        Me.Label61.Text = "label1"
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.Location = New System.Drawing.Point(297, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(1, 27)
        Me.Label62.TabIndex = 8
        Me.Label62.Text = "label1"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(0, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 27)
        Me.Label63.TabIndex = 9
        Me.Label63.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(298, 33)
        Me.Panel4.TabIndex = 30
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.BackgroundImage = CType(resources.GetObject("Panel5.BackgroundImage"), System.Drawing.Image)
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Label10)
        Me.Panel5.Controls.Add(Me.Label54)
        Me.Panel5.Controls.Add(Me.Label55)
        Me.Panel5.Controls.Add(Me.Label57)
        Me.Panel5.Controls.Add(Me.Label58)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(298, 27)
        Me.Panel5.TabIndex = 32
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(1, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(296, 1)
        Me.Label10.TabIndex = 33
        Me.Label10.Text = "label1"
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(1, 26)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(296, 1)
        Me.Label54.TabIndex = 32
        Me.Label54.Text = "label1"
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.Transparent
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Location = New System.Drawing.Point(1, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(296, 27)
        Me.Label55.TabIndex = 31
        Me.Label55.Text = " Enter search text, then select SNOMED-CT"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(297, 0)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 27)
        Me.Label57.TabIndex = 8
        Me.Label57.Text = "label1"
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(0, 0)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(1, 27)
        Me.Label58.TabIndex = 9
        Me.Label58.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Panel3.Size = New System.Drawing.Size(1150, 35)
        Me.Panel3.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label81)
        Me.Panel2.Controls.Add(Me.pnlbtnNutrition)
        Me.Panel2.Controls.Add(Me.pnlbtnEncounter)
        Me.Panel2.Controls.Add(Me.pnlbntLabs)
        Me.Panel2.Controls.Add(Me.pnlbtnMedication)
        Me.Panel2.Controls.Add(Me.pnlCodeList)
        Me.Panel2.Controls.Add(Me.Label82)
        Me.Panel2.Controls.Add(Me.Label83)
        Me.Panel2.Controls.Add(Me.Label84)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1144, 35)
        Me.Panel2.TabIndex = 655555559
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.Location = New System.Drawing.Point(1143, 1)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(1, 33)
        Me.Label81.TabIndex = 655555560
        Me.Label81.Text = "label4"
        '
        'pnlbtnNutrition
        '
        Me.pnlbtnNutrition.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnNutrition.Controls.Add(Me.Label88)
        Me.pnlbtnNutrition.Controls.Add(Me.btnNutrition)
        Me.pnlbtnNutrition.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlbtnNutrition.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnNutrition.Location = New System.Drawing.Point(556, 1)
        Me.pnlbtnNutrition.Margin = New System.Windows.Forms.Padding(0, 3, 0, 2)
        Me.pnlbtnNutrition.Name = "pnlbtnNutrition"
        Me.pnlbtnNutrition.Size = New System.Drawing.Size(185, 33)
        Me.pnlbtnNutrition.TabIndex = 655555559
        '
        'Label88
        '
        Me.Label88.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label88.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label88.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label88.Location = New System.Drawing.Point(184, 0)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(1, 33)
        Me.Label88.TabIndex = 14
        Me.Label88.Text = "label4"
        '
        'btnNutrition
        '
        Me.btnNutrition.BackColor = System.Drawing.Color.Transparent
        Me.btnNutrition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNutrition.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNutrition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnNutrition.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnNutrition.FlatAppearance.BorderSize = 0
        Me.btnNutrition.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnNutrition.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnNutrition.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNutrition.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNutrition.ForeColor = System.Drawing.Color.White
        Me.btnNutrition.Location = New System.Drawing.Point(0, 0)
        Me.btnNutrition.Name = "btnNutrition"
        Me.btnNutrition.Size = New System.Drawing.Size(185, 33)
        Me.btnNutrition.TabIndex = 2
        Me.btnNutrition.Tag = "4"
        Me.btnNutrition.Text = "Nutrition Recommendation"
        Me.btnNutrition.UseVisualStyleBackColor = False
        '
        'pnlbtnEncounter
        '
        Me.pnlbtnEncounter.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnEncounter.Controls.Add(Me.Label87)
        Me.pnlbtnEncounter.Controls.Add(Me.btnEncounter)
        Me.pnlbtnEncounter.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlbtnEncounter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnEncounter.Location = New System.Drawing.Point(371, 1)
        Me.pnlbtnEncounter.Margin = New System.Windows.Forms.Padding(0, 3, 0, 2)
        Me.pnlbtnEncounter.Name = "pnlbtnEncounter"
        Me.pnlbtnEncounter.Size = New System.Drawing.Size(185, 33)
        Me.pnlbtnEncounter.TabIndex = 18
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label87.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label87.Location = New System.Drawing.Point(184, 0)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(1, 33)
        Me.Label87.TabIndex = 14
        Me.Label87.Text = "label4"
        '
        'btnEncounter
        '
        Me.btnEncounter.BackColor = System.Drawing.Color.Transparent
        Me.btnEncounter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEncounter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEncounter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnEncounter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnEncounter.FlatAppearance.BorderSize = 0
        Me.btnEncounter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnEncounter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnEncounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEncounter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEncounter.ForeColor = System.Drawing.Color.White
        Me.btnEncounter.Location = New System.Drawing.Point(0, 0)
        Me.btnEncounter.Name = "btnEncounter"
        Me.btnEncounter.Size = New System.Drawing.Size(185, 33)
        Me.btnEncounter.TabIndex = 2
        Me.btnEncounter.Tag = "3"
        Me.btnEncounter.Text = "Planned Encounter"
        Me.btnEncounter.UseVisualStyleBackColor = False
        '
        'pnlbntLabs
        '
        Me.pnlbntLabs.BackColor = System.Drawing.Color.Transparent
        Me.pnlbntLabs.Controls.Add(Me.Label86)
        Me.pnlbntLabs.Controls.Add(Me.btnLabs)
        Me.pnlbntLabs.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlbntLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbntLabs.Location = New System.Drawing.Point(186, 1)
        Me.pnlbntLabs.Margin = New System.Windows.Forms.Padding(0, 3, 0, 2)
        Me.pnlbntLabs.Name = "pnlbntLabs"
        Me.pnlbntLabs.Size = New System.Drawing.Size(185, 33)
        Me.pnlbntLabs.TabIndex = 17
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label86.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label86.Location = New System.Drawing.Point(184, 0)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(1, 33)
        Me.Label86.TabIndex = 15
        Me.Label86.Text = "label4"
        '
        'btnLabs
        '
        Me.btnLabs.BackColor = System.Drawing.Color.Transparent
        Me.btnLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabs.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLabs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnLabs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLabs.ForeColor = System.Drawing.Color.White
        Me.btnLabs.Location = New System.Drawing.Point(0, 0)
        Me.btnLabs.Name = "btnLabs"
        Me.btnLabs.Size = New System.Drawing.Size(185, 33)
        Me.btnLabs.TabIndex = 2
        Me.btnLabs.Tag = "2"
        Me.btnLabs.Text = "Planned Orders"
        Me.btnLabs.UseVisualStyleBackColor = False
        '
        'pnlbtnMedication
        '
        Me.pnlbtnMedication.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnMedication.Controls.Add(Me.Label85)
        Me.pnlbtnMedication.Controls.Add(Me.btnMedication)
        Me.pnlbtnMedication.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlbtnMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnMedication.Location = New System.Drawing.Point(1, 1)
        Me.pnlbtnMedication.Margin = New System.Windows.Forms.Padding(0, 3, 0, 2)
        Me.pnlbtnMedication.Name = "pnlbtnMedication"
        Me.pnlbtnMedication.Size = New System.Drawing.Size(185, 33)
        Me.pnlbtnMedication.TabIndex = 655555558
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label85.Location = New System.Drawing.Point(184, 0)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1, 33)
        Me.Label85.TabIndex = 14
        Me.Label85.Text = "label4"
        '
        'btnMedication
        '
        Me.btnMedication.BackColor = System.Drawing.Color.Transparent
        Me.btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.btnMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMedication.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMedication.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMedication.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnMedication.FlatAppearance.BorderSize = 0
        Me.btnMedication.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnMedication.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnMedication.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMedication.ForeColor = System.Drawing.Color.Black
        Me.btnMedication.Location = New System.Drawing.Point(0, 0)
        Me.btnMedication.Name = "btnMedication"
        Me.btnMedication.Size = New System.Drawing.Size(185, 33)
        Me.btnMedication.TabIndex = 1
        Me.btnMedication.Tag = "1"
        Me.btnMedication.Text = "Planned Medication"
        Me.btnMedication.UseVisualStyleBackColor = False
        '
        'pnlCodeList
        '
        Me.pnlCodeList.BackColor = System.Drawing.Color.Transparent
        Me.pnlCodeList.Controls.Add(Me.btnAllCode)
        Me.pnlCodeList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlCodeList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlCodeList.Location = New System.Drawing.Point(200, 200)
        Me.pnlCodeList.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlCodeList.Name = "pnlCodeList"
        Me.pnlCodeList.Size = New System.Drawing.Size(0, 0)
        Me.pnlCodeList.TabIndex = 655555556
        Me.pnlCodeList.TabStop = True
        '
        'btnAllCode
        '
        Me.btnAllCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnAllCode.BackgroundImage = CType(resources.GetObject("btnAllCode.BackgroundImage"), System.Drawing.Image)
        Me.btnAllCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAllCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAllCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAllCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnAllCode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnAllCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllCode.Location = New System.Drawing.Point(0, 0)
        Me.btnAllCode.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnAllCode.Name = "btnAllCode"
        Me.btnAllCode.Size = New System.Drawing.Size(0, 0)
        Me.btnAllCode.TabIndex = 1
        Me.btnAllCode.Tag = ""
        Me.btnAllCode.Text = "All Codes"
        Me.btnAllCode.UseVisualStyleBackColor = False
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.Location = New System.Drawing.Point(0, 1)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(1, 33)
        Me.Label82.TabIndex = 655555561
        Me.Label82.Text = "label4"
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.Location = New System.Drawing.Point(0, 0)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(1144, 1)
        Me.Label83.TabIndex = 655555562
        Me.Label83.Text = "label1"
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.Location = New System.Drawing.Point(0, 34)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(1144, 1)
        Me.Label84.TabIndex = 655555563
        Me.Label84.Text = "label1"
        '
        'Timer1
        '
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'CmnuPannedActivity
        '
        Me.CmnuPannedActivity.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.oMnuRemovePlannedActivity})
        Me.CmnuPannedActivity.Name = "CmnuDiagnosis"
        Me.CmnuPannedActivity.Size = New System.Drawing.Size(207, 26)
        '
        'oMnuRemovePlannedActivity
        '
        Me.oMnuRemovePlannedActivity.Name = "oMnuRemovePlannedActivity"
        Me.oMnuRemovePlannedActivity.Size = New System.Drawing.Size(206, 22)
        Me.oMnuRemovePlannedActivity.Text = "Remove Planned Activity"
        '
        'TimerNurt
        '
        '
        'frmTreatmentPlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1150, 779)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTreatmentPlan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Plan of Treatment"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tlsPatientPOT.ResumeLayout(False)
        Me.tlsPatientPOT.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlExam.ResumeLayout(False)
        Me.pnlExam.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlActivityList.ResumeLayout(False)
        CType(Me.c1PlanActivity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlActivityDetail.ResumeLayout(False)
        Me.pnlActivityDetailData.ResumeLayout(False)
        Me.pnlActivityDetailData.PerformLayout()
        Me.pnlActivitytToolStrip.ResumeLayout(False)
        Me.pnlActivitytToolStrip.PerformLayout()
        Me.tlsp_ActivityDetails.ResumeLayout(False)
        Me.tlsp_ActivityDetails.PerformLayout()
        Me.pnlActivityType.ResumeLayout(False)
        Me.pnlControlContainer.ResumeLayout(False)
        Me.pnlNutrition.ResumeLayout(False)
        Me.pnlNurtFinding.ResumeLayout(False)
        Me.pnlNurtSMSearch.ResumeLayout(False)
        Me.pnlNurtSMSearch.PerformLayout()
        CType(Me.PicBxNurt_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.pnlLabOrdersList.ResumeLayout(False)
        Me.pnltrvList.ResumeLayout(False)
        Me.panel8.ResumeLayout(False)
        Me.panel8.PerformLayout()
        Me.pnlLabTests.ResumeLayout(False)
        Me.pnl_btnTests.ResumeLayout(False)
        Me.pnl_btnRadiologyImaging.ResumeLayout(False)
        Me.pnl_btnRefTest.ResumeLayout(False)
        Me.pnl_btnOthers.ResumeLayout(False)
        Me.pnl_btnGroups.ResumeLayout(False)
        Me.pnlEncounterSnomed.ResumeLayout(False)
        Me.pnlfinding.ResumeLayout(False)
        Me.pnlSMSearch.ResumeLayout(False)
        Me.pnlSMSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlbtnNutrition.ResumeLayout(False)
        Me.pnlbtnEncounter.ResumeLayout(False)
        Me.pnlbntLabs.ResumeLayout(False)
        Me.pnlbtnMedication.ResumeLayout(False)
        Me.pnlCodeList.ResumeLayout(False)
        Me.CmnuPannedActivity.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tlsPatientPOT As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnPOTClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents txtPlanAssesment As System.Windows.Forms.TextBox
    Friend WithEvents dtpPlanEffectiveTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpPlanEffectiveFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPlantitle As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlActivityList As System.Windows.Forms.Panel
    Friend WithEvents pnlActivityType As System.Windows.Forms.Panel
    Friend WithEvents btnEncounter As System.Windows.Forms.Button
    Private WithEvents c1PlanActivity As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents lblGridBottom As System.Windows.Forms.Label
    Private WithEvents lblGridLeft As System.Windows.Forms.Label
    Private WithEvents lblGridRight As System.Windows.Forms.Label
    Private WithEvents lblGridTop As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnEncounter As System.Windows.Forms.Panel
    Friend WithEvents pnlbntLabs As System.Windows.Forms.Panel
    Private WithEvents pnlCodeList As System.Windows.Forms.Panel
    Protected WithEvents btnAllCode As System.Windows.Forms.Button
    Friend WithEvents pnlControlContainer As System.Windows.Forms.Panel
    Friend WithEvents pnlActivityDetail As System.Windows.Forms.Panel
    Friend WithEvents pnlActivitytToolStrip As System.Windows.Forms.Panel
    Private WithEvents tlsp_ActivityDetails As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlsp_btnActivityCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents pnlActivityDetailData As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtActivity As System.Windows.Forms.TextBox
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblMedication As System.Windows.Forms.Label
    Private WithEvents btnClearSelectedProblem As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents btnGetPatientProblem As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents dtpActivityEffectiveFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents dtpActivityEffectiveTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtSelectedCode As System.Windows.Forms.TextBox
    Friend WithEvents cmbProblemList As System.Windows.Forms.ComboBox
    Private WithEvents ts_btnPOTSaveClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlbtnMedication As System.Windows.Forms.Panel
    Friend WithEvents btnMedication As System.Windows.Forms.Button
    Friend WithEvents pnlLabOrdersList As System.Windows.Forms.Panel
    Private WithEvents pnltrvList As System.Windows.Forms.Panel
    Friend WithEvents GloUC_trvTest As gloUserControlLibrary.gloUC_TreeView
    Private WithEvents panel8 As System.Windows.Forms.Panel
    Private WithEvents chkIncludeTestCode As System.Windows.Forms.CheckBox
    Private WithEvents label39 As System.Windows.Forms.Label
    Private WithEvents label40 As System.Windows.Forms.Label
    Private WithEvents label41 As System.Windows.Forms.Label
    Private WithEvents pnl_btnTests As System.Windows.Forms.Panel
    Friend WithEvents btnTests As System.Windows.Forms.Button
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents pnl_btnRadiologyImaging As System.Windows.Forms.Panel
    Private WithEvents label36 As System.Windows.Forms.Label
    Friend WithEvents btnRadiologyImaging As System.Windows.Forms.Button
    Private WithEvents lblRadiologyImaging1 As System.Windows.Forms.Label
    Private WithEvents lblRadiologyImaging2 As System.Windows.Forms.Label
    Private WithEvents lblRadiologyImaging4 As System.Windows.Forms.Label
    Private WithEvents lblRadiologyImaging3 As System.Windows.Forms.Label
    Private WithEvents pnl_btnRefTest As System.Windows.Forms.Panel
    Friend WithEvents btnRefTest As System.Windows.Forms.Button
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents pnl_btnOthers As System.Windows.Forms.Panel
    Private WithEvents label37 As System.Windows.Forms.Label
    Friend WithEvents btnOthers As System.Windows.Forms.Button
    Private WithEvents lblOthers1 As System.Windows.Forms.Label
    Private WithEvents lblOthers2 As System.Windows.Forms.Label
    Private WithEvents lblOthers3 As System.Windows.Forms.Label
    Private WithEvents lblOthers4 As System.Windows.Forms.Label
    Private WithEvents pnl_btnGroups As System.Windows.Forms.Panel
    Private WithEvents label35 As System.Windows.Forms.Label
    Private WithEvents label34 As System.Windows.Forms.Label
    Private WithEvents label33 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents btnGroups As System.Windows.Forms.Button
    Private WithEvents pnlLabTests As System.Windows.Forms.Panel
    Private WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents btnLabTests As System.Windows.Forms.Button
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents pnlEncounterSnomed As System.Windows.Forms.Panel
    Friend WithEvents pnlfinding As System.Windows.Forms.Panel
    Friend WithEvents trvFindings As System.Windows.Forms.TreeView
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Private WithEvents Label50 As System.Windows.Forms.Label
    Private WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents pnlSMSearch As System.Windows.Forms.Panel
    Public WithEvents txtSMSearch As gloSnoMed.gloSearchTextBox
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label59 As System.Windows.Forms.Label
    Private WithEvents Label61 As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Private WithEvents chkCOREProblem As System.Windows.Forms.CheckBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblEncounter As System.Windows.Forms.Label
    Friend WithEvents lblLabOrders As System.Windows.Forms.Label
    Friend WithEvents rbt_Inactive As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_Active As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_Complete As System.Windows.Forms.RadioButton
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents CmnuPannedActivity As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents oMnuRemovePlannedActivity As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnLabs As System.Windows.Forms.Button
    Friend WithEvents pnlNutrition As System.Windows.Forms.Panel
    Friend WithEvents pnlNurtFinding As System.Windows.Forms.Panel
    Friend WithEvents trvNurtFindings As System.Windows.Forms.TreeView
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label60 As System.Windows.Forms.Label
    Private WithEvents Label64 As System.Windows.Forms.Label
    Private WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents pnlNurtSMSearch As System.Windows.Forms.Panel
    Public WithEvents txtNurtSMSearch As gloSnoMed.gloSearchTextBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents btnNurtClear As System.Windows.Forms.Button
    Friend WithEvents PicBxNurt_Search As System.Windows.Forms.PictureBox
    Private WithEvents Label68 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Private WithEvents Label70 As System.Windows.Forms.Label
    Private WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents chkNurtCOREProblem As System.Windows.Forms.CheckBox
    Private WithEvents Label72 As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Private WithEvents Label74 As System.Windows.Forms.Label
    Private WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Private WithEvents Label76 As System.Windows.Forms.Label
    Private WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Private WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnNutrition As System.Windows.Forms.Panel
    Friend WithEvents btnNutrition As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label81 As System.Windows.Forms.Label
    Private WithEvents Label88 As System.Windows.Forms.Label
    Private WithEvents Label87 As System.Windows.Forms.Label
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label82 As System.Windows.Forms.Label
    Private WithEvents Label83 As System.Windows.Forms.Label
    Private WithEvents Label84 As System.Windows.Forms.Label
    Private WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents TimerNurt As System.Windows.Forms.Timer
    Friend WithEvents pnlExam As System.Windows.Forms.Panel
    Friend WithEvents btnExam As System.Windows.Forms.Button
    Friend WithEvents lblDetails As System.Windows.Forms.Label
    Friend WithEvents lblNurtitionRecommendation As System.Windows.Forms.Label
    Private WithEvents tlsp_btnActivitySave As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblExamName As System.Windows.Forms.Label
    Private WithEvents btnExamClear As System.Windows.Forms.Button
End Class
