<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientGoal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientGoal))
        Me.tlsp_PatientInjuryDate = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtGoalLoinc = New System.Windows.Forms.TextBox()
        Me.btnGoalLoinc = New System.Windows.Forms.Button()
        Me.btnClearGoalLoinc = New System.Windows.Forms.Button()
        Me.cmbGoalUnit = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtGoalValue = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cmbProblem = New System.Windows.Forms.ComboBox()
        Me.lblExams = New System.Windows.Forms.Label()
        Me.btn_LoadProblem = New System.Windows.Forms.Button()
        Me.gb_GoalAuthor = New System.Windows.Forms.GroupBox()
        Me.rbt_FromBoth = New System.Windows.Forms.RadioButton()
        Me.rbt_FromPatient = New System.Windows.Forms.RadioButton()
        Me.rbt_FromProvider = New System.Windows.Forms.RadioButton()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtGoalNotes = New System.Windows.Forms.RichTextBox()
        Me.btnClearProblems = New System.Windows.Forms.Button()
        Me.txtGoalName = New System.Windows.Forms.TextBox()
        Me.lblConcernName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbHealthConcern = New System.Windows.Forms.ComboBox()
        Me.btn_LoadHealthConcern = New System.Windows.Forms.Button()
        Me.btnClearHealthConcern = New System.Windows.Forms.Button()
        Me.dtpGoalDate = New System.Windows.Forms.DateTimePicker()
        Me.chkTargetDate = New System.Windows.Forms.CheckBox()
        Me.dtpGoalTargetDate = New System.Windows.Forms.DateTimePicker()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.chkGoalDate = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.label19 = New System.Windows.Forms.Label()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label22 = New System.Windows.Forms.Label()
        Me.tlsp_PatientInjuryDate.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlsp_PatientInjuryDate
        '
        Me.tlsp_PatientInjuryDate.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_PatientInjuryDate.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_PatientInjuryDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_PatientInjuryDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_PatientInjuryDate.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_PatientInjuryDate.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tlsp_PatientInjuryDate.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_PatientInjuryDate.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_PatientInjuryDate.Name = "tlsp_PatientInjuryDate"
        Me.tlsp_PatientInjuryDate.Size = New System.Drawing.Size(575, 53)
        Me.tlsp_PatientInjuryDate.TabIndex = 18
        Me.tlsp_PatientInjuryDate.TabStop = True
        Me.tlsp_PatientInjuryDate.Text = "toolStrip1"
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
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(91, 133)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(43, 14)
        Me.Label16.TabIndex = 13069
        Me.Label16.Text = "Loinc :"
        '
        'txtGoalLoinc
        '
        Me.txtGoalLoinc.BackColor = System.Drawing.Color.White
        Me.txtGoalLoinc.Location = New System.Drawing.Point(138, 129)
        Me.txtGoalLoinc.Name = "txtGoalLoinc"
        Me.txtGoalLoinc.ReadOnly = True
        Me.txtGoalLoinc.Size = New System.Drawing.Size(363, 22)
        Me.txtGoalLoinc.TabIndex = 4
        '
        'btnGoalLoinc
        '
        Me.btnGoalLoinc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnGoalLoinc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGoalLoinc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoalLoinc.Image = CType(resources.GetObject("btnGoalLoinc.Image"), System.Drawing.Image)
        Me.btnGoalLoinc.Location = New System.Drawing.Point(506, 130)
        Me.btnGoalLoinc.Name = "btnGoalLoinc"
        Me.btnGoalLoinc.Size = New System.Drawing.Size(21, 21)
        Me.btnGoalLoinc.TabIndex = 5
        Me.btnGoalLoinc.UseVisualStyleBackColor = True
        '
        'btnClearGoalLoinc
        '
        Me.btnClearGoalLoinc.BackColor = System.Drawing.Color.Transparent
        Me.btnClearGoalLoinc.BackgroundImage = CType(resources.GetObject("btnClearGoalLoinc.BackgroundImage"), System.Drawing.Image)
        Me.btnClearGoalLoinc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearGoalLoinc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearGoalLoinc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearGoalLoinc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearGoalLoinc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearGoalLoinc.Image = CType(resources.GetObject("btnClearGoalLoinc.Image"), System.Drawing.Image)
        Me.btnClearGoalLoinc.Location = New System.Drawing.Point(530, 130)
        Me.btnClearGoalLoinc.Name = "btnClearGoalLoinc"
        Me.btnClearGoalLoinc.Size = New System.Drawing.Size(21, 21)
        Me.btnClearGoalLoinc.TabIndex = 6
        Me.btnClearGoalLoinc.UseVisualStyleBackColor = False
        '
        'cmbGoalUnit
        '
        Me.cmbGoalUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoalUnit.FormattingEnabled = True
        Me.cmbGoalUnit.Location = New System.Drawing.Point(351, 158)
        Me.cmbGoalUnit.Name = "cmbGoalUnit"
        Me.cmbGoalUnit.Size = New System.Drawing.Size(150, 22)
        Me.cmbGoalUnit.TabIndex = 8
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(308, 162)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(37, 14)
        Me.Label24.TabIndex = 13064
        Me.Label24.Text = "Unit :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(89, 162)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(45, 14)
        Me.Label23.TabIndex = 13063
        Me.Label23.Text = "Value :"
        '
        'txtGoalValue
        '
        Me.txtGoalValue.BackColor = System.Drawing.Color.White
        Me.txtGoalValue.Location = New System.Drawing.Point(138, 158)
        Me.txtGoalValue.MaxLength = 20
        Me.txtGoalValue.Name = "txtGoalValue"
        Me.txtGoalValue.Size = New System.Drawing.Size(150, 22)
        Me.txtGoalValue.TabIndex = 7
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(73, 18)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(14, 14)
        Me.Label18.TabIndex = 13061
        Me.Label18.Text = "*"
        '
        'cmbProblem
        '
        Me.cmbProblem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProblem.FormattingEnabled = True
        Me.cmbProblem.Location = New System.Drawing.Point(138, 212)
        Me.cmbProblem.Name = "cmbProblem"
        Me.cmbProblem.Size = New System.Drawing.Size(363, 22)
        Me.cmbProblem.TabIndex = 10
        '
        'lblExams
        '
        Me.lblExams.AutoSize = True
        Me.lblExams.Location = New System.Drawing.Point(53, 216)
        Me.lblExams.Name = "lblExams"
        Me.lblExams.Size = New System.Drawing.Size(81, 14)
        Me.lblExams.TabIndex = 13058
        Me.lblExams.Text = "Problem List :"
        '
        'btn_LoadProblem
        '
        Me.btn_LoadProblem.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btn_LoadProblem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_LoadProblem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_LoadProblem.Image = CType(resources.GetObject("btn_LoadProblem.Image"), System.Drawing.Image)
        Me.btn_LoadProblem.Location = New System.Drawing.Point(506, 213)
        Me.btn_LoadProblem.Name = "btn_LoadProblem"
        Me.btn_LoadProblem.Size = New System.Drawing.Size(21, 21)
        Me.btn_LoadProblem.TabIndex = 11
        Me.btn_LoadProblem.UseVisualStyleBackColor = True
        '
        'gb_GoalAuthor
        '
        Me.gb_GoalAuthor.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_GoalAuthor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gb_GoalAuthor.Location = New System.Drawing.Point(27, 381)
        Me.gb_GoalAuthor.Name = "gb_GoalAuthor"
        Me.gb_GoalAuthor.Size = New System.Drawing.Size(13, 10)
        Me.gb_GoalAuthor.TabIndex = 8
        Me.gb_GoalAuthor.TabStop = False
        Me.gb_GoalAuthor.Text = "From"
        '
        'rbt_FromBoth
        '
        Me.rbt_FromBoth.AutoSize = True
        Me.rbt_FromBoth.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_FromBoth.Location = New System.Drawing.Point(329, 187)
        Me.rbt_FromBoth.Name = "rbt_FromBoth"
        Me.rbt_FromBoth.Size = New System.Drawing.Size(51, 18)
        Me.rbt_FromBoth.TabIndex = 2
        Me.rbt_FromBoth.Text = "Both"
        Me.rbt_FromBoth.UseVisualStyleBackColor = True
        '
        'rbt_FromPatient
        '
        Me.rbt_FromPatient.AutoSize = True
        Me.rbt_FromPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_FromPatient.Location = New System.Drawing.Point(239, 187)
        Me.rbt_FromPatient.Name = "rbt_FromPatient"
        Me.rbt_FromPatient.Size = New System.Drawing.Size(64, 18)
        Me.rbt_FromPatient.TabIndex = 1
        Me.rbt_FromPatient.Text = "Patient"
        Me.rbt_FromPatient.UseVisualStyleBackColor = True
        '
        'rbt_FromProvider
        '
        Me.rbt_FromProvider.AutoSize = True
        Me.rbt_FromProvider.Checked = True
        Me.rbt_FromProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_FromProvider.Location = New System.Drawing.Point(144, 187)
        Me.rbt_FromProvider.Name = "rbt_FromProvider"
        Me.rbt_FromProvider.Size = New System.Drawing.Size(76, 18)
        Me.rbt_FromProvider.TabIndex = 0
        Me.rbt_FromProvider.TabStop = True
        Me.rbt_FromProvider.Text = "Provider"
        Me.rbt_FromProvider.UseVisualStyleBackColor = True
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(60, 43)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(74, 14)
        Me.Label53.TabIndex = 13059
        Me.Label53.Text = "Goal Notes :"
        '
        'txtGoalNotes
        '
        Me.txtGoalNotes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoalNotes.Location = New System.Drawing.Point(138, 43)
        Me.txtGoalNotes.MaxLength = 500
        Me.txtGoalNotes.Name = "txtGoalNotes"
        Me.txtGoalNotes.Size = New System.Drawing.Size(363, 50)
        Me.txtGoalNotes.TabIndex = 1
        Me.txtGoalNotes.Text = ""
        '
        'btnClearProblems
        '
        Me.btnClearProblems.BackColor = System.Drawing.Color.Transparent
        Me.btnClearProblems.BackgroundImage = CType(resources.GetObject("btnClearProblems.BackgroundImage"), System.Drawing.Image)
        Me.btnClearProblems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearProblems.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearProblems.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearProblems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearProblems.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearProblems.Image = CType(resources.GetObject("btnClearProblems.Image"), System.Drawing.Image)
        Me.btnClearProblems.Location = New System.Drawing.Point(530, 213)
        Me.btnClearProblems.Name = "btnClearProblems"
        Me.btnClearProblems.Size = New System.Drawing.Size(21, 21)
        Me.btnClearProblems.TabIndex = 12
        Me.btnClearProblems.UseVisualStyleBackColor = False
        '
        'txtGoalName
        '
        Me.txtGoalName.BackColor = System.Drawing.Color.White
        Me.txtGoalName.Location = New System.Drawing.Point(138, 14)
        Me.txtGoalName.MaxLength = 100
        Me.txtGoalName.Name = "txtGoalName"
        Me.txtGoalName.Size = New System.Drawing.Size(363, 22)
        Me.txtGoalName.TabIndex = 0
        '
        'lblConcernName
        '
        Me.lblConcernName.AutoSize = True
        Me.lblConcernName.Location = New System.Drawing.Point(88, 18)
        Me.lblConcernName.Name = "lblConcernName"
        Me.lblConcernName.Size = New System.Drawing.Size(46, 14)
        Me.lblConcernName.TabIndex = 13060
        Me.lblConcernName.Text = "Name :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(35, 244)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 14)
        Me.Label1.TabIndex = 13070
        Me.Label1.Text = "Health Concern :"
        '
        'cmbHealthConcern
        '
        Me.cmbHealthConcern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHealthConcern.FormattingEnabled = True
        Me.cmbHealthConcern.Location = New System.Drawing.Point(138, 241)
        Me.cmbHealthConcern.Name = "cmbHealthConcern"
        Me.cmbHealthConcern.Size = New System.Drawing.Size(363, 22)
        Me.cmbHealthConcern.TabIndex = 13
        '
        'btn_LoadHealthConcern
        '
        Me.btn_LoadHealthConcern.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btn_LoadHealthConcern.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_LoadHealthConcern.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_LoadHealthConcern.Image = CType(resources.GetObject("btn_LoadHealthConcern.Image"), System.Drawing.Image)
        Me.btn_LoadHealthConcern.Location = New System.Drawing.Point(506, 242)
        Me.btn_LoadHealthConcern.Name = "btn_LoadHealthConcern"
        Me.btn_LoadHealthConcern.Size = New System.Drawing.Size(21, 21)
        Me.btn_LoadHealthConcern.TabIndex = 14
        Me.btn_LoadHealthConcern.UseVisualStyleBackColor = True
        '
        'btnClearHealthConcern
        '
        Me.btnClearHealthConcern.BackColor = System.Drawing.Color.Transparent
        Me.btnClearHealthConcern.BackgroundImage = CType(resources.GetObject("btnClearHealthConcern.BackgroundImage"), System.Drawing.Image)
        Me.btnClearHealthConcern.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearHealthConcern.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearHealthConcern.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearHealthConcern.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearHealthConcern.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearHealthConcern.Image = CType(resources.GetObject("btnClearHealthConcern.Image"), System.Drawing.Image)
        Me.btnClearHealthConcern.Location = New System.Drawing.Point(530, 242)
        Me.btnClearHealthConcern.Name = "btnClearHealthConcern"
        Me.btnClearHealthConcern.Size = New System.Drawing.Size(21, 21)
        Me.btnClearHealthConcern.TabIndex = 15
        Me.btnClearHealthConcern.UseVisualStyleBackColor = False
        '
        'dtpGoalDate
        '
        Me.dtpGoalDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpGoalDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpGoalDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpGoalDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpGoalDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpGoalDate.CustomFormat = "MM/dd/ yyyy hh:mm:ss tt"
        Me.dtpGoalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGoalDate.Location = New System.Drawing.Point(155, 100)
        Me.dtpGoalDate.Name = "dtpGoalDate"
        Me.dtpGoalDate.Size = New System.Drawing.Size(192, 22)
        Me.dtpGoalDate.TabIndex = 3
        '
        'chkTargetDate
        '
        Me.chkTargetDate.AutoSize = True
        Me.chkTargetDate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkTargetDate.Location = New System.Drawing.Point(51, 272)
        Me.chkTargetDate.Name = "chkTargetDate"
        Me.chkTargetDate.Size = New System.Drawing.Size(101, 18)
        Me.chkTargetDate.TabIndex = 16
        Me.chkTargetDate.Text = "Target Date :"
        Me.chkTargetDate.UseVisualStyleBackColor = True
        '
        'dtpGoalTargetDate
        '
        Me.dtpGoalTargetDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpGoalTargetDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpGoalTargetDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpGoalTargetDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpGoalTargetDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpGoalTargetDate.CustomFormat = "MM/dd/ yyyy hh:mm:ss tt"
        Me.dtpGoalTargetDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGoalTargetDate.Location = New System.Drawing.Point(155, 270)
        Me.dtpGoalTargetDate.Name = "dtpGoalTargetDate"
        Me.dtpGoalTargetDate.Size = New System.Drawing.Size(192, 22)
        Me.dtpGoalTargetDate.TabIndex = 17
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.Transparent
        Me.panel4.Controls.Add(Me.chkGoalDate)
        Me.panel4.Controls.Add(Me.Label2)
        Me.panel4.Controls.Add(Me.rbt_FromBoth)
        Me.panel4.Controls.Add(Me.rbt_FromPatient)
        Me.panel4.Controls.Add(Me.label19)
        Me.panel4.Controls.Add(Me.rbt_FromProvider)
        Me.panel4.Controls.Add(Me.chkTargetDate)
        Me.panel4.Controls.Add(Me.label20)
        Me.panel4.Controls.Add(Me.dtpGoalTargetDate)
        Me.panel4.Controls.Add(Me.label21)
        Me.panel4.Controls.Add(Me.dtpGoalDate)
        Me.panel4.Controls.Add(Me.label22)
        Me.panel4.Controls.Add(Me.txtGoalNotes)
        Me.panel4.Controls.Add(Me.cmbHealthConcern)
        Me.panel4.Controls.Add(Me.lblConcernName)
        Me.panel4.Controls.Add(Me.btn_LoadHealthConcern)
        Me.panel4.Controls.Add(Me.txtGoalName)
        Me.panel4.Controls.Add(Me.btnClearHealthConcern)
        Me.panel4.Controls.Add(Me.btnClearProblems)
        Me.panel4.Controls.Add(Me.Label1)
        Me.panel4.Controls.Add(Me.Label53)
        Me.panel4.Controls.Add(Me.Label16)
        Me.panel4.Controls.Add(Me.gb_GoalAuthor)
        Me.panel4.Controls.Add(Me.txtGoalLoinc)
        Me.panel4.Controls.Add(Me.btn_LoadProblem)
        Me.panel4.Controls.Add(Me.btnGoalLoinc)
        Me.panel4.Controls.Add(Me.lblExams)
        Me.panel4.Controls.Add(Me.btnClearGoalLoinc)
        Me.panel4.Controls.Add(Me.cmbProblem)
        Me.panel4.Controls.Add(Me.cmbGoalUnit)
        Me.panel4.Controls.Add(Me.Label18)
        Me.panel4.Controls.Add(Me.Label24)
        Me.panel4.Controls.Add(Me.txtGoalValue)
        Me.panel4.Controls.Add(Me.Label23)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel4.Location = New System.Drawing.Point(0, 53)
        Me.panel4.Name = "panel4"
        Me.panel4.Padding = New System.Windows.Forms.Padding(3)
        Me.panel4.Size = New System.Drawing.Size(575, 312)
        Me.panel4.TabIndex = 13076
        '
        'chkGoalDate
        '
        Me.chkGoalDate.AutoSize = True
        Me.chkGoalDate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkGoalDate.Location = New System.Drawing.Point(65, 103)
        Me.chkGoalDate.Name = "chkGoalDate"
        Me.chkGoalDate.Size = New System.Drawing.Size(87, 18)
        Me.chkGoalDate.TabIndex = 2
        Me.chkGoalDate.Text = "Goal Date :"
        Me.chkGoalDate.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(92, 189)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 13076
        Me.Label2.Text = "From :"
        '
        'label19
        '
        Me.label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label19.Location = New System.Drawing.Point(4, 308)
        Me.label19.Name = "label19"
        Me.label19.Size = New System.Drawing.Size(567, 1)
        Me.label19.TabIndex = 12
        Me.label19.Text = "label1"
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.label20.Location = New System.Drawing.Point(4, 3)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(567, 1)
        Me.label20.TabIndex = 11
        Me.label20.Text = "label1"
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.label21.Location = New System.Drawing.Point(3, 3)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(1, 306)
        Me.label21.TabIndex = 10
        Me.label21.Text = "label1"
        '
        'label22
        '
        Me.label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label22.Dock = System.Windows.Forms.DockStyle.Right
        Me.label22.Location = New System.Drawing.Point(571, 3)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(1, 306)
        Me.label22.TabIndex = 9
        Me.label22.Text = "label1"
        '
        'frmPatientGoal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(575, 365)
        Me.Controls.Add(Me.panel4)
        Me.Controls.Add(Me.tlsp_PatientInjuryDate)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientGoal"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Goal"
        Me.tlsp_PatientInjuryDate.ResumeLayout(False)
        Me.tlsp_PatientInjuryDate.PerformLayout()
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents tlsp_PatientInjuryDate As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtGoalLoinc As System.Windows.Forms.TextBox
    Friend WithEvents btnGoalLoinc As System.Windows.Forms.Button
    Friend WithEvents btnClearGoalLoinc As System.Windows.Forms.Button
    Friend WithEvents cmbGoalUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtGoalValue As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cmbProblem As System.Windows.Forms.ComboBox
    Friend WithEvents lblExams As System.Windows.Forms.Label
    Friend WithEvents btn_LoadProblem As System.Windows.Forms.Button
    Friend WithEvents gb_GoalAuthor As System.Windows.Forms.GroupBox
    Friend WithEvents rbt_FromBoth As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_FromPatient As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_FromProvider As System.Windows.Forms.RadioButton
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtGoalNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents btnClearProblems As System.Windows.Forms.Button
    Friend WithEvents txtGoalName As System.Windows.Forms.TextBox
    Friend WithEvents lblConcernName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbHealthConcern As System.Windows.Forms.ComboBox
    Friend WithEvents btn_LoadHealthConcern As System.Windows.Forms.Button
    Friend WithEvents btnClearHealthConcern As System.Windows.Forms.Button
    Friend WithEvents dtpGoalDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkTargetDate As System.Windows.Forms.CheckBox
    Friend WithEvents dtpGoalTargetDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Private WithEvents label19 As System.Windows.Forms.Label
    Private WithEvents label20 As System.Windows.Forms.Label
    Private WithEvents label21 As System.Windows.Forms.Label
    Private WithEvents label22 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkGoalDate As System.Windows.Forms.CheckBox
End Class
