<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientIntervention
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientIntervention))
        Me.Label18 = New System.Windows.Forms.Label()
        Me.tlsp_PatientIntervention = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtInterventionNotes = New System.Windows.Forms.RichTextBox()
        Me.txtInterventionName = New System.Windows.Forms.TextBox()
        Me.lblConcernName = New System.Windows.Forms.Label()
        Me.cmbInterventionGoals = New System.Windows.Forms.ComboBox()
        Me.lblInterventionGoal = New System.Windows.Forms.Label()
        Me.btnInterventionGoals = New System.Windows.Forms.Button()
        Me.btnClearInterventionGoals = New System.Windows.Forms.Button()
        Me.btnInterventionNutrition = New System.Windows.Forms.Button()
        Me.cmbInterventionPatientEducation = New System.Windows.Forms.ComboBox()
        Me.lblInterventionPatientEducation = New System.Windows.Forms.Label()
        Me.btnInterventionPatientEducation = New System.Windows.Forms.Button()
        Me.btnClearInterventionPatientEducation = New System.Windows.Forms.Button()
        Me.cmbInterventionEncounter = New System.Windows.Forms.ComboBox()
        Me.lblInterventionPatientEncounter = New System.Windows.Forms.Label()
        Me.btnInterventionEncounter = New System.Windows.Forms.Button()
        Me.btnClearInterventionEncounter = New System.Windows.Forms.Button()
        Me.cmbInterventionImmunization = New System.Windows.Forms.ComboBox()
        Me.lblInterventionImmunization = New System.Windows.Forms.Label()
        Me.btnInterventionImmunization = New System.Windows.Forms.Button()
        Me.btnClearInterventionImmunization = New System.Windows.Forms.Button()
        Me.cmbInterventionLabOrder = New System.Windows.Forms.ComboBox()
        Me.lblInterventionLabOrder = New System.Windows.Forms.Label()
        Me.btnInterventionLabOrder = New System.Windows.Forms.Button()
        Me.btnClearInterventionLabOrder = New System.Windows.Forms.Button()
        Me.cmbInterventionNutrition = New System.Windows.Forms.ComboBox()
        Me.cmbInterventionMedication = New System.Windows.Forms.ComboBox()
        Me.lblInterventionMedication = New System.Windows.Forms.Label()
        Me.btnInterventionMedication = New System.Windows.Forms.Button()
        Me.btnClearInterventionMedication = New System.Windows.Forms.Button()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.btnClearInterventionNutrition = New System.Windows.Forms.Button()
        Me.grpbxHideControls = New System.Windows.Forms.GroupBox()
        Me.btnAddPlanned = New System.Windows.Forms.Button()
        Me.rbt_PlannedIntervention = New System.Windows.Forms.RadioButton()
        Me.rbt_ActualIntervention = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnInterventionNutritionAssociationInstructions = New System.Windows.Forms.Button()
        Me.dtpInterventionDate = New System.Windows.Forms.DateTimePicker()
        Me.pnl = New System.Windows.Forms.Panel()
        Me.chkDate = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBottom = New System.Windows.Forms.Label()
        Me.lblTop = New System.Windows.Forms.Label()
        Me.lblLeft = New System.Windows.Forms.Label()
        Me.lblRight = New System.Windows.Forms.Label()
        Me.tlsp_PatientIntervention.SuspendLayout()
        Me.grpbxHideControls.SuspendLayout()
        Me.pnl.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(77, 23)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(14, 14)
        Me.Label18.TabIndex = 13067
        Me.Label18.Text = "*"
        '
        'tlsp_PatientIntervention
        '
        Me.tlsp_PatientIntervention.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_PatientIntervention.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_PatientIntervention.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_PatientIntervention.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_PatientIntervention.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_PatientIntervention.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tlsp_PatientIntervention.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_PatientIntervention.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_PatientIntervention.Name = "tlsp_PatientIntervention"
        Me.tlsp_PatientIntervention.Size = New System.Drawing.Size(661, 53)
        Me.tlsp_PatientIntervention.TabIndex = 27
        Me.tlsp_PatientIntervention.TabStop = True
        Me.tlsp_PatientIntervention.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Save and Close"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(19, 51)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(119, 14)
        Me.Label53.TabIndex = 13065
        Me.Label53.Text = "Intervention Notes :"
        '
        'txtInterventionNotes
        '
        Me.txtInterventionNotes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInterventionNotes.Location = New System.Drawing.Point(142, 50)
        Me.txtInterventionNotes.MaxLength = 500
        Me.txtInterventionNotes.Name = "txtInterventionNotes"
        Me.txtInterventionNotes.Size = New System.Drawing.Size(363, 50)
        Me.txtInterventionNotes.TabIndex = 1
        Me.txtInterventionNotes.Text = ""
        '
        'txtInterventionName
        '
        Me.txtInterventionName.BackColor = System.Drawing.Color.White
        Me.txtInterventionName.Location = New System.Drawing.Point(142, 19)
        Me.txtInterventionName.MaxLength = 100
        Me.txtInterventionName.Name = "txtInterventionName"
        Me.txtInterventionName.Size = New System.Drawing.Size(363, 22)
        Me.txtInterventionName.TabIndex = 0
        '
        'lblConcernName
        '
        Me.lblConcernName.AutoSize = True
        Me.lblConcernName.Location = New System.Drawing.Point(92, 23)
        Me.lblConcernName.Name = "lblConcernName"
        Me.lblConcernName.Size = New System.Drawing.Size(46, 14)
        Me.lblConcernName.TabIndex = 13066
        Me.lblConcernName.Text = "Name :"
        '
        'cmbInterventionGoals
        '
        Me.cmbInterventionGoals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInterventionGoals.FormattingEnabled = True
        Me.cmbInterventionGoals.Location = New System.Drawing.Point(142, 167)
        Me.cmbInterventionGoals.Name = "cmbInterventionGoals"
        Me.cmbInterventionGoals.Size = New System.Drawing.Size(363, 22)
        Me.cmbInterventionGoals.TabIndex = 5
        '
        'lblInterventionGoal
        '
        Me.lblInterventionGoal.AutoSize = True
        Me.lblInterventionGoal.Location = New System.Drawing.Point(100, 171)
        Me.lblInterventionGoal.Name = "lblInterventionGoal"
        Me.lblInterventionGoal.Size = New System.Drawing.Size(38, 14)
        Me.lblInterventionGoal.TabIndex = 13070
        Me.lblInterventionGoal.Text = "Goal :"
        '
        'btnInterventionGoals
        '
        Me.btnInterventionGoals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnInterventionGoals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInterventionGoals.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInterventionGoals.Image = CType(resources.GetObject("btnInterventionGoals.Image"), System.Drawing.Image)
        Me.btnInterventionGoals.Location = New System.Drawing.Point(510, 168)
        Me.btnInterventionGoals.Name = "btnInterventionGoals"
        Me.btnInterventionGoals.Size = New System.Drawing.Size(21, 21)
        Me.btnInterventionGoals.TabIndex = 6
        Me.btnInterventionGoals.UseVisualStyleBackColor = True
        '
        'btnClearInterventionGoals
        '
        Me.btnClearInterventionGoals.BackColor = System.Drawing.Color.Transparent
        Me.btnClearInterventionGoals.BackgroundImage = CType(resources.GetObject("btnClearInterventionGoals.BackgroundImage"), System.Drawing.Image)
        Me.btnClearInterventionGoals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearInterventionGoals.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearInterventionGoals.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearInterventionGoals.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearInterventionGoals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearInterventionGoals.Image = CType(resources.GetObject("btnClearInterventionGoals.Image"), System.Drawing.Image)
        Me.btnClearInterventionGoals.Location = New System.Drawing.Point(535, 168)
        Me.btnClearInterventionGoals.Name = "btnClearInterventionGoals"
        Me.btnClearInterventionGoals.Size = New System.Drawing.Size(21, 21)
        Me.btnClearInterventionGoals.TabIndex = 7
        Me.btnClearInterventionGoals.UseVisualStyleBackColor = False
        '
        'btnInterventionNutrition
        '
        Me.btnInterventionNutrition.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnInterventionNutrition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInterventionNutrition.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInterventionNutrition.Image = CType(resources.GetObject("btnInterventionNutrition.Image"), System.Drawing.Image)
        Me.btnInterventionNutrition.Location = New System.Drawing.Point(510, 199)
        Me.btnInterventionNutrition.Name = "btnInterventionNutrition"
        Me.btnInterventionNutrition.Size = New System.Drawing.Size(21, 21)
        Me.btnInterventionNutrition.TabIndex = 9
        Me.btnInterventionNutrition.UseVisualStyleBackColor = True
        '
        'cmbInterventionPatientEducation
        '
        Me.cmbInterventionPatientEducation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInterventionPatientEducation.FormattingEnabled = True
        Me.cmbInterventionPatientEducation.Location = New System.Drawing.Point(119, 45)
        Me.cmbInterventionPatientEducation.Name = "cmbInterventionPatientEducation"
        Me.cmbInterventionPatientEducation.Size = New System.Drawing.Size(363, 22)
        Me.cmbInterventionPatientEducation.TabIndex = 24
        '
        'lblInterventionPatientEducation
        '
        Me.lblInterventionPatientEducation.AutoSize = True
        Me.lblInterventionPatientEducation.Location = New System.Drawing.Point(46, 49)
        Me.lblInterventionPatientEducation.Name = "lblInterventionPatientEducation"
        Me.lblInterventionPatientEducation.Size = New System.Drawing.Size(76, 14)
        Me.lblInterventionPatientEducation.TabIndex = 13093
        Me.lblInterventionPatientEducation.Text = "Education :"
        '
        'btnInterventionPatientEducation
        '
        Me.btnInterventionPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnInterventionPatientEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInterventionPatientEducation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInterventionPatientEducation.Image = CType(resources.GetObject("btnInterventionPatientEducation.Image"), System.Drawing.Image)
        Me.btnInterventionPatientEducation.Location = New System.Drawing.Point(487, 46)
        Me.btnInterventionPatientEducation.Name = "btnInterventionPatientEducation"
        Me.btnInterventionPatientEducation.Size = New System.Drawing.Size(21, 21)
        Me.btnInterventionPatientEducation.TabIndex = 25
        Me.btnInterventionPatientEducation.UseVisualStyleBackColor = True
        '
        'btnClearInterventionPatientEducation
        '
        Me.btnClearInterventionPatientEducation.BackColor = System.Drawing.Color.Transparent
        Me.btnClearInterventionPatientEducation.BackgroundImage = CType(resources.GetObject("btnClearInterventionPatientEducation.BackgroundImage"), System.Drawing.Image)
        Me.btnClearInterventionPatientEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearInterventionPatientEducation.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearInterventionPatientEducation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearInterventionPatientEducation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearInterventionPatientEducation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearInterventionPatientEducation.Image = CType(resources.GetObject("btnClearInterventionPatientEducation.Image"), System.Drawing.Image)
        Me.btnClearInterventionPatientEducation.Location = New System.Drawing.Point(512, 46)
        Me.btnClearInterventionPatientEducation.Name = "btnClearInterventionPatientEducation"
        Me.btnClearInterventionPatientEducation.Size = New System.Drawing.Size(21, 21)
        Me.btnClearInterventionPatientEducation.TabIndex = 26
        Me.btnClearInterventionPatientEducation.UseVisualStyleBackColor = False
        '
        'cmbInterventionEncounter
        '
        Me.cmbInterventionEncounter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInterventionEncounter.FormattingEnabled = True
        Me.cmbInterventionEncounter.Location = New System.Drawing.Point(119, 14)
        Me.cmbInterventionEncounter.Name = "cmbInterventionEncounter"
        Me.cmbInterventionEncounter.Size = New System.Drawing.Size(363, 22)
        Me.cmbInterventionEncounter.TabIndex = 21
        '
        'lblInterventionPatientEncounter
        '
        Me.lblInterventionPatientEncounter.AutoSize = True
        Me.lblInterventionPatientEncounter.Location = New System.Drawing.Point(43, 18)
        Me.lblInterventionPatientEncounter.Name = "lblInterventionPatientEncounter"
        Me.lblInterventionPatientEncounter.Size = New System.Drawing.Size(78, 14)
        Me.lblInterventionPatientEncounter.TabIndex = 13089
        Me.lblInterventionPatientEncounter.Text = "Encounter :"
        '
        'btnInterventionEncounter
        '
        Me.btnInterventionEncounter.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnInterventionEncounter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInterventionEncounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInterventionEncounter.Image = CType(resources.GetObject("btnInterventionEncounter.Image"), System.Drawing.Image)
        Me.btnInterventionEncounter.Location = New System.Drawing.Point(487, 15)
        Me.btnInterventionEncounter.Name = "btnInterventionEncounter"
        Me.btnInterventionEncounter.Size = New System.Drawing.Size(21, 21)
        Me.btnInterventionEncounter.TabIndex = 22
        Me.btnInterventionEncounter.UseVisualStyleBackColor = True
        '
        'btnClearInterventionEncounter
        '
        Me.btnClearInterventionEncounter.BackColor = System.Drawing.Color.Transparent
        Me.btnClearInterventionEncounter.BackgroundImage = CType(resources.GetObject("btnClearInterventionEncounter.BackgroundImage"), System.Drawing.Image)
        Me.btnClearInterventionEncounter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearInterventionEncounter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearInterventionEncounter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearInterventionEncounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearInterventionEncounter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearInterventionEncounter.Image = CType(resources.GetObject("btnClearInterventionEncounter.Image"), System.Drawing.Image)
        Me.btnClearInterventionEncounter.Location = New System.Drawing.Point(512, 15)
        Me.btnClearInterventionEncounter.Name = "btnClearInterventionEncounter"
        Me.btnClearInterventionEncounter.Size = New System.Drawing.Size(21, 21)
        Me.btnClearInterventionEncounter.TabIndex = 23
        Me.btnClearInterventionEncounter.UseVisualStyleBackColor = False
        '
        'cmbInterventionImmunization
        '
        Me.cmbInterventionImmunization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInterventionImmunization.FormattingEnabled = True
        Me.cmbInterventionImmunization.Location = New System.Drawing.Point(142, 291)
        Me.cmbInterventionImmunization.Name = "cmbInterventionImmunization"
        Me.cmbInterventionImmunization.Size = New System.Drawing.Size(363, 22)
        Me.cmbInterventionImmunization.TabIndex = 18
        '
        'lblInterventionImmunization
        '
        Me.lblInterventionImmunization.AutoSize = True
        Me.lblInterventionImmunization.Location = New System.Drawing.Point(51, 295)
        Me.lblInterventionImmunization.Name = "lblInterventionImmunization"
        Me.lblInterventionImmunization.Size = New System.Drawing.Size(87, 14)
        Me.lblInterventionImmunization.TabIndex = 13085
        Me.lblInterventionImmunization.Text = "Immunization :"
        '
        'btnInterventionImmunization
        '
        Me.btnInterventionImmunization.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnInterventionImmunization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInterventionImmunization.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInterventionImmunization.Image = CType(resources.GetObject("btnInterventionImmunization.Image"), System.Drawing.Image)
        Me.btnInterventionImmunization.Location = New System.Drawing.Point(510, 292)
        Me.btnInterventionImmunization.Name = "btnInterventionImmunization"
        Me.btnInterventionImmunization.Size = New System.Drawing.Size(21, 21)
        Me.btnInterventionImmunization.TabIndex = 19
        Me.btnInterventionImmunization.UseVisualStyleBackColor = True
        '
        'btnClearInterventionImmunization
        '
        Me.btnClearInterventionImmunization.BackColor = System.Drawing.Color.Transparent
        Me.btnClearInterventionImmunization.BackgroundImage = CType(resources.GetObject("btnClearInterventionImmunization.BackgroundImage"), System.Drawing.Image)
        Me.btnClearInterventionImmunization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearInterventionImmunization.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearInterventionImmunization.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearInterventionImmunization.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearInterventionImmunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearInterventionImmunization.Image = CType(resources.GetObject("btnClearInterventionImmunization.Image"), System.Drawing.Image)
        Me.btnClearInterventionImmunization.Location = New System.Drawing.Point(535, 292)
        Me.btnClearInterventionImmunization.Name = "btnClearInterventionImmunization"
        Me.btnClearInterventionImmunization.Size = New System.Drawing.Size(21, 21)
        Me.btnClearInterventionImmunization.TabIndex = 20
        Me.btnClearInterventionImmunization.UseVisualStyleBackColor = False
        '
        'cmbInterventionLabOrder
        '
        Me.cmbInterventionLabOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInterventionLabOrder.FormattingEnabled = True
        Me.cmbInterventionLabOrder.Location = New System.Drawing.Point(142, 260)
        Me.cmbInterventionLabOrder.Name = "cmbInterventionLabOrder"
        Me.cmbInterventionLabOrder.Size = New System.Drawing.Size(363, 22)
        Me.cmbInterventionLabOrder.TabIndex = 15
        '
        'lblInterventionLabOrder
        '
        Me.lblInterventionLabOrder.AutoSize = True
        Me.lblInterventionLabOrder.Location = New System.Drawing.Point(69, 264)
        Me.lblInterventionLabOrder.Name = "lblInterventionLabOrder"
        Me.lblInterventionLabOrder.Size = New System.Drawing.Size(69, 14)
        Me.lblInterventionLabOrder.TabIndex = 13081
        Me.lblInterventionLabOrder.Text = "Lab Order :"
        '
        'btnInterventionLabOrder
        '
        Me.btnInterventionLabOrder.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnInterventionLabOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInterventionLabOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInterventionLabOrder.Image = CType(resources.GetObject("btnInterventionLabOrder.Image"), System.Drawing.Image)
        Me.btnInterventionLabOrder.Location = New System.Drawing.Point(510, 261)
        Me.btnInterventionLabOrder.Name = "btnInterventionLabOrder"
        Me.btnInterventionLabOrder.Size = New System.Drawing.Size(21, 21)
        Me.btnInterventionLabOrder.TabIndex = 16
        Me.btnInterventionLabOrder.UseVisualStyleBackColor = True
        '
        'btnClearInterventionLabOrder
        '
        Me.btnClearInterventionLabOrder.BackColor = System.Drawing.Color.Transparent
        Me.btnClearInterventionLabOrder.BackgroundImage = CType(resources.GetObject("btnClearInterventionLabOrder.BackgroundImage"), System.Drawing.Image)
        Me.btnClearInterventionLabOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearInterventionLabOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearInterventionLabOrder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearInterventionLabOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearInterventionLabOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearInterventionLabOrder.Image = CType(resources.GetObject("btnClearInterventionLabOrder.Image"), System.Drawing.Image)
        Me.btnClearInterventionLabOrder.Location = New System.Drawing.Point(535, 261)
        Me.btnClearInterventionLabOrder.Name = "btnClearInterventionLabOrder"
        Me.btnClearInterventionLabOrder.Size = New System.Drawing.Size(21, 21)
        Me.btnClearInterventionLabOrder.TabIndex = 17
        Me.btnClearInterventionLabOrder.UseVisualStyleBackColor = False
        '
        'cmbInterventionNutrition
        '
        Me.cmbInterventionNutrition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInterventionNutrition.FormattingEnabled = True
        Me.cmbInterventionNutrition.Location = New System.Drawing.Point(142, 198)
        Me.cmbInterventionNutrition.Name = "cmbInterventionNutrition"
        Me.cmbInterventionNutrition.Size = New System.Drawing.Size(363, 22)
        Me.cmbInterventionNutrition.TabIndex = 8
        '
        'cmbInterventionMedication
        '
        Me.cmbInterventionMedication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInterventionMedication.FormattingEnabled = True
        Me.cmbInterventionMedication.Location = New System.Drawing.Point(142, 229)
        Me.cmbInterventionMedication.Name = "cmbInterventionMedication"
        Me.cmbInterventionMedication.Size = New System.Drawing.Size(363, 22)
        Me.cmbInterventionMedication.TabIndex = 12
        '
        'lblInterventionMedication
        '
        Me.lblInterventionMedication.AutoSize = True
        Me.lblInterventionMedication.Location = New System.Drawing.Point(65, 233)
        Me.lblInterventionMedication.Name = "lblInterventionMedication"
        Me.lblInterventionMedication.Size = New System.Drawing.Size(73, 14)
        Me.lblInterventionMedication.TabIndex = 13076
        Me.lblInterventionMedication.Text = "Medication :"
        '
        'btnInterventionMedication
        '
        Me.btnInterventionMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnInterventionMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInterventionMedication.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInterventionMedication.Image = CType(resources.GetObject("btnInterventionMedication.Image"), System.Drawing.Image)
        Me.btnInterventionMedication.Location = New System.Drawing.Point(510, 230)
        Me.btnInterventionMedication.Name = "btnInterventionMedication"
        Me.btnInterventionMedication.Size = New System.Drawing.Size(21, 21)
        Me.btnInterventionMedication.TabIndex = 13
        Me.btnInterventionMedication.UseVisualStyleBackColor = True
        '
        'btnClearInterventionMedication
        '
        Me.btnClearInterventionMedication.BackColor = System.Drawing.Color.Transparent
        Me.btnClearInterventionMedication.BackgroundImage = CType(resources.GetObject("btnClearInterventionMedication.BackgroundImage"), System.Drawing.Image)
        Me.btnClearInterventionMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearInterventionMedication.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearInterventionMedication.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearInterventionMedication.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearInterventionMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearInterventionMedication.Image = CType(resources.GetObject("btnClearInterventionMedication.Image"), System.Drawing.Image)
        Me.btnClearInterventionMedication.Location = New System.Drawing.Point(535, 230)
        Me.btnClearInterventionMedication.Name = "btnClearInterventionMedication"
        Me.btnClearInterventionMedication.Size = New System.Drawing.Size(21, 21)
        Me.btnClearInterventionMedication.TabIndex = 14
        Me.btnClearInterventionMedication.UseVisualStyleBackColor = False
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(76, 202)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(62, 14)
        Me.Label37.TabIndex = 13073
        Me.Label37.Text = "Nutrition :"
        '
        'btnClearInterventionNutrition
        '
        Me.btnClearInterventionNutrition.BackColor = System.Drawing.Color.Transparent
        Me.btnClearInterventionNutrition.BackgroundImage = CType(resources.GetObject("btnClearInterventionNutrition.BackgroundImage"), System.Drawing.Image)
        Me.btnClearInterventionNutrition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearInterventionNutrition.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearInterventionNutrition.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearInterventionNutrition.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearInterventionNutrition.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearInterventionNutrition.Image = CType(resources.GetObject("btnClearInterventionNutrition.Image"), System.Drawing.Image)
        Me.btnClearInterventionNutrition.Location = New System.Drawing.Point(535, 199)
        Me.btnClearInterventionNutrition.Name = "btnClearInterventionNutrition"
        Me.btnClearInterventionNutrition.Size = New System.Drawing.Size(21, 21)
        Me.btnClearInterventionNutrition.TabIndex = 10
        Me.btnClearInterventionNutrition.UseVisualStyleBackColor = False
        '
        'grpbxHideControls
        '
        Me.grpbxHideControls.Controls.Add(Me.lblInterventionPatientEncounter)
        Me.grpbxHideControls.Controls.Add(Me.btnClearInterventionEncounter)
        Me.grpbxHideControls.Controls.Add(Me.btnInterventionEncounter)
        Me.grpbxHideControls.Controls.Add(Me.cmbInterventionEncounter)
        Me.grpbxHideControls.Controls.Add(Me.btnClearInterventionPatientEducation)
        Me.grpbxHideControls.Controls.Add(Me.btnInterventionPatientEducation)
        Me.grpbxHideControls.Controls.Add(Me.lblInterventionPatientEducation)
        Me.grpbxHideControls.Controls.Add(Me.cmbInterventionPatientEducation)
        Me.grpbxHideControls.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpbxHideControls.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpbxHideControls.Location = New System.Drawing.Point(54, 344)
        Me.grpbxHideControls.Name = "grpbxHideControls"
        Me.grpbxHideControls.Size = New System.Drawing.Size(556, 74)
        Me.grpbxHideControls.TabIndex = 3
        Me.grpbxHideControls.TabStop = False
        Me.grpbxHideControls.Text = "Hide Controls"
        '
        'btnAddPlanned
        '
        Me.btnAddPlanned.BackgroundImage = CType(resources.GetObject("btnAddPlanned.BackgroundImage"), System.Drawing.Image)
        Me.btnAddPlanned.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddPlanned.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddPlanned.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddPlanned.Location = New System.Drawing.Point(431, 135)
        Me.btnAddPlanned.Name = "btnAddPlanned"
        Me.btnAddPlanned.Size = New System.Drawing.Size(125, 28)
        Me.btnAddPlanned.TabIndex = 4
        Me.btnAddPlanned.Text = "&Plan of Treatment"
        Me.btnAddPlanned.UseVisualStyleBackColor = True
        '
        'rbt_PlannedIntervention
        '
        Me.rbt_PlannedIntervention.AutoSize = True
        Me.rbt_PlannedIntervention.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_PlannedIntervention.Location = New System.Drawing.Point(253, 140)
        Me.rbt_PlannedIntervention.Name = "rbt_PlannedIntervention"
        Me.rbt_PlannedIntervention.Size = New System.Drawing.Size(68, 18)
        Me.rbt_PlannedIntervention.TabIndex = 2
        Me.rbt_PlannedIntervention.Text = "Planned"
        Me.rbt_PlannedIntervention.UseVisualStyleBackColor = True
        '
        'rbt_ActualIntervention
        '
        Me.rbt_ActualIntervention.AutoSize = True
        Me.rbt_ActualIntervention.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_ActualIntervention.Location = New System.Drawing.Point(142, 140)
        Me.rbt_ActualIntervention.Name = "rbt_ActualIntervention"
        Me.rbt_ActualIntervention.Size = New System.Drawing.Size(59, 18)
        Me.rbt_ActualIntervention.TabIndex = 1
        Me.rbt_ActualIntervention.Text = "Actual"
        Me.rbt_ActualIntervention.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(86, 171)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 14)
        Me.Label1.TabIndex = 13094
        Me.Label1.Text = "*"
        '
        'btnInterventionNutritionAssociationInstructions
        '
        Me.btnInterventionNutritionAssociationInstructions.BackgroundImage = CType(resources.GetObject("btnInterventionNutritionAssociationInstructions.BackgroundImage"), System.Drawing.Image)
        Me.btnInterventionNutritionAssociationInstructions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInterventionNutritionAssociationInstructions.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnInterventionNutritionAssociationInstructions.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInterventionNutritionAssociationInstructions.Location = New System.Drawing.Point(561, 196)
        Me.btnInterventionNutritionAssociationInstructions.Name = "btnInterventionNutritionAssociationInstructions"
        Me.btnInterventionNutritionAssociationInstructions.Size = New System.Drawing.Size(84, 26)
        Me.btnInterventionNutritionAssociationInstructions.TabIndex = 11
        Me.btnInterventionNutritionAssociationInstructions.Text = "Instructions"
        Me.btnInterventionNutritionAssociationInstructions.UseVisualStyleBackColor = True
        '
        'dtpInterventionDate
        '
        Me.dtpInterventionDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpInterventionDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpInterventionDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpInterventionDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpInterventionDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpInterventionDate.CustomFormat = "MM/dd/ yyyy hh:mm:ss tt"
        Me.dtpInterventionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInterventionDate.Location = New System.Drawing.Point(160, 109)
        Me.dtpInterventionDate.Name = "dtpInterventionDate"
        Me.dtpInterventionDate.Size = New System.Drawing.Size(192, 22)
        Me.dtpInterventionDate.TabIndex = 3
        '
        'pnl
        '
        Me.pnl.BackColor = System.Drawing.Color.Transparent
        Me.pnl.Controls.Add(Me.chkDate)
        Me.pnl.Controls.Add(Me.Label2)
        Me.pnl.Controls.Add(Me.btnAddPlanned)
        Me.pnl.Controls.Add(Me.lblBottom)
        Me.pnl.Controls.Add(Me.rbt_PlannedIntervention)
        Me.pnl.Controls.Add(Me.rbt_ActualIntervention)
        Me.pnl.Controls.Add(Me.dtpInterventionDate)
        Me.pnl.Controls.Add(Me.lblTop)
        Me.pnl.Controls.Add(Me.lblLeft)
        Me.pnl.Controls.Add(Me.btnInterventionNutritionAssociationInstructions)
        Me.pnl.Controls.Add(Me.lblRight)
        Me.pnl.Controls.Add(Me.Label1)
        Me.pnl.Controls.Add(Me.txtInterventionName)
        Me.pnl.Controls.Add(Me.grpbxHideControls)
        Me.pnl.Controls.Add(Me.lblConcernName)
        Me.pnl.Controls.Add(Me.btnInterventionNutrition)
        Me.pnl.Controls.Add(Me.txtInterventionNotes)
        Me.pnl.Controls.Add(Me.Label53)
        Me.pnl.Controls.Add(Me.Label18)
        Me.pnl.Controls.Add(Me.btnClearInterventionGoals)
        Me.pnl.Controls.Add(Me.btnInterventionGoals)
        Me.pnl.Controls.Add(Me.lblInterventionGoal)
        Me.pnl.Controls.Add(Me.cmbInterventionGoals)
        Me.pnl.Controls.Add(Me.btnClearInterventionNutrition)
        Me.pnl.Controls.Add(Me.Label37)
        Me.pnl.Controls.Add(Me.cmbInterventionImmunization)
        Me.pnl.Controls.Add(Me.btnClearInterventionMedication)
        Me.pnl.Controls.Add(Me.lblInterventionImmunization)
        Me.pnl.Controls.Add(Me.btnInterventionMedication)
        Me.pnl.Controls.Add(Me.btnInterventionImmunization)
        Me.pnl.Controls.Add(Me.lblInterventionMedication)
        Me.pnl.Controls.Add(Me.btnClearInterventionImmunization)
        Me.pnl.Controls.Add(Me.cmbInterventionMedication)
        Me.pnl.Controls.Add(Me.cmbInterventionLabOrder)
        Me.pnl.Controls.Add(Me.cmbInterventionNutrition)
        Me.pnl.Controls.Add(Me.lblInterventionLabOrder)
        Me.pnl.Controls.Add(Me.btnClearInterventionLabOrder)
        Me.pnl.Controls.Add(Me.btnInterventionLabOrder)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl.Location = New System.Drawing.Point(0, 53)
        Me.pnl.Name = "pnl"
        Me.pnl.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl.Size = New System.Drawing.Size(661, 328)
        Me.pnl.TabIndex = 13098
        '
        'chkDate
        '
        Me.chkDate.AutoSize = True
        Me.chkDate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkDate.Location = New System.Drawing.Point(97, 112)
        Me.chkDate.Name = "chkDate"
        Me.chkDate.Size = New System.Drawing.Size(60, 18)
        Me.chkDate.TabIndex = 2
        Me.chkDate.Text = "Date :"
        Me.chkDate.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(95, 142)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 14)
        Me.Label2.TabIndex = 13098
        Me.Label2.Text = "Type :"
        '
        'lblBottom
        '
        Me.lblBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblBottom.Location = New System.Drawing.Point(4, 324)
        Me.lblBottom.Name = "lblBottom"
        Me.lblBottom.Size = New System.Drawing.Size(653, 1)
        Me.lblBottom.TabIndex = 12
        Me.lblBottom.Text = "label1"
        '
        'lblTop
        '
        Me.lblTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTop.Location = New System.Drawing.Point(4, 3)
        Me.lblTop.Name = "lblTop"
        Me.lblTop.Size = New System.Drawing.Size(653, 1)
        Me.lblTop.TabIndex = 11
        Me.lblTop.Text = "label1"
        '
        'lblLeft
        '
        Me.lblLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblLeft.Location = New System.Drawing.Point(3, 3)
        Me.lblLeft.Name = "lblLeft"
        Me.lblLeft.Size = New System.Drawing.Size(1, 322)
        Me.lblLeft.TabIndex = 10
        Me.lblLeft.Text = "label1"
        '
        'lblRight
        '
        Me.lblRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblRight.Location = New System.Drawing.Point(657, 3)
        Me.lblRight.Name = "lblRight"
        Me.lblRight.Size = New System.Drawing.Size(1, 322)
        Me.lblRight.TabIndex = 9
        Me.lblRight.Text = "label1"
        '
        'frmPatientIntervention
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(661, 381)
        Me.Controls.Add(Me.pnl)
        Me.Controls.Add(Me.tlsp_PatientIntervention)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientIntervention"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Intervention"
        Me.tlsp_PatientIntervention.ResumeLayout(False)
        Me.tlsp_PatientIntervention.PerformLayout()
        Me.grpbxHideControls.ResumeLayout(False)
        Me.grpbxHideControls.PerformLayout()
        Me.pnl.ResumeLayout(False)
        Me.pnl.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents tlsp_PatientIntervention As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtInterventionNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents txtInterventionName As System.Windows.Forms.TextBox
    Friend WithEvents lblConcernName As System.Windows.Forms.Label
    Friend WithEvents cmbInterventionGoals As System.Windows.Forms.ComboBox
    Friend WithEvents lblInterventionGoal As System.Windows.Forms.Label
    Friend WithEvents btnInterventionGoals As System.Windows.Forms.Button
    Friend WithEvents btnClearInterventionGoals As System.Windows.Forms.Button
    Friend WithEvents btnInterventionNutrition As System.Windows.Forms.Button
    Friend WithEvents cmbInterventionPatientEducation As System.Windows.Forms.ComboBox
    Friend WithEvents lblInterventionPatientEducation As System.Windows.Forms.Label
    Friend WithEvents btnInterventionPatientEducation As System.Windows.Forms.Button
    Friend WithEvents btnClearInterventionPatientEducation As System.Windows.Forms.Button
    Friend WithEvents cmbInterventionEncounter As System.Windows.Forms.ComboBox
    Friend WithEvents lblInterventionPatientEncounter As System.Windows.Forms.Label
    Friend WithEvents btnInterventionEncounter As System.Windows.Forms.Button
    Friend WithEvents btnClearInterventionEncounter As System.Windows.Forms.Button
    Friend WithEvents cmbInterventionImmunization As System.Windows.Forms.ComboBox
    Friend WithEvents lblInterventionImmunization As System.Windows.Forms.Label
    Friend WithEvents btnInterventionImmunization As System.Windows.Forms.Button
    Friend WithEvents btnClearInterventionImmunization As System.Windows.Forms.Button
    Friend WithEvents cmbInterventionLabOrder As System.Windows.Forms.ComboBox
    Friend WithEvents lblInterventionLabOrder As System.Windows.Forms.Label
    Friend WithEvents btnInterventionLabOrder As System.Windows.Forms.Button
    Friend WithEvents btnClearInterventionLabOrder As System.Windows.Forms.Button
    Friend WithEvents cmbInterventionNutrition As System.Windows.Forms.ComboBox
    Friend WithEvents cmbInterventionMedication As System.Windows.Forms.ComboBox
    Friend WithEvents lblInterventionMedication As System.Windows.Forms.Label
    Friend WithEvents btnInterventionMedication As System.Windows.Forms.Button
    Friend WithEvents btnClearInterventionMedication As System.Windows.Forms.Button
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents btnClearInterventionNutrition As System.Windows.Forms.Button
    Friend WithEvents grpbxHideControls As System.Windows.Forms.GroupBox
    Friend WithEvents rbt_PlannedIntervention As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_ActualIntervention As System.Windows.Forms.RadioButton
    Friend WithEvents btnAddPlanned As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnInterventionNutritionAssociationInstructions As System.Windows.Forms.Button
    Friend WithEvents dtpInterventionDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents lblBottom As System.Windows.Forms.Label
    Private WithEvents lblTop As System.Windows.Forms.Label
    Private WithEvents lblLeft As System.Windows.Forms.Label
    Private WithEvents lblRight As System.Windows.Forms.Label
    Friend WithEvents chkDate As System.Windows.Forms.CheckBox
End Class
