<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientOutcome
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientOutcome))
        Me.btnIntervention = New System.Windows.Forms.Button()
        Me.cmbIntervention = New System.Windows.Forms.ComboBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.lblInterventionMedication = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.btnClearIntervention = New System.Windows.Forms.Button()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.cmbGoals = New System.Windows.Forms.ComboBox()
        Me.lblInterventionGoal = New System.Windows.Forms.Label()
        Me.btnGoals = New System.Windows.Forms.Button()
        Me.btnClearGoals = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.tlsp_PatientOutcome = New gloGlobal.gloToolStripIgnoreFocus()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtOutcomeNotes = New System.Windows.Forms.RichTextBox()
        Me.txtOutcomeName = New System.Windows.Forms.TextBox()
        Me.lblConcernName = New System.Windows.Forms.Label()
        Me.dtpOutcomeDate = New System.Windows.Forms.DateTimePicker()
        Me.chkDate = New System.Windows.Forms.CheckBox()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtlValue = New System.Windows.Forms.TextBox()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.label19 = New System.Windows.Forms.Label()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label22 = New System.Windows.Forms.Label()
        Me.pnlToolstrip = New System.Windows.Forms.Panel()
        Me.tlsp_PatientOutcome.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlToolstrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnIntervention
        '
        Me.btnIntervention.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnIntervention.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnIntervention.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIntervention.Image = CType(resources.GetObject("btnIntervention.Image"), System.Drawing.Image)
        Me.btnIntervention.Location = New System.Drawing.Point(486, 197)
        Me.btnIntervention.Name = "btnIntervention"
        Me.btnIntervention.Size = New System.Drawing.Size(21, 21)
        Me.btnIntervention.TabIndex = 10
        Me.btnIntervention.UseVisualStyleBackColor = True
        '
        'cmbIntervention
        '
        Me.cmbIntervention.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIntervention.FormattingEnabled = True
        Me.cmbIntervention.Location = New System.Drawing.Point(119, 197)
        Me.cmbIntervention.Name = "cmbIntervention"
        Me.cmbIntervention.Size = New System.Drawing.Size(363, 22)
        Me.cmbIntervention.TabIndex = 9
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(119, 225)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(363, 22)
        Me.cmbStatus.TabIndex = 12
        '
        'lblInterventionMedication
        '
        Me.lblInterventionMedication.AutoSize = True
        Me.lblInterventionMedication.Location = New System.Drawing.Point(65, 229)
        Me.lblInterventionMedication.Name = "lblInterventionMedication"
        Me.lblInterventionMedication.Size = New System.Drawing.Size(50, 14)
        Me.lblInterventionMedication.TabIndex = 13125
        Me.lblInterventionMedication.Text = "Status :"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(32, 200)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(83, 14)
        Me.Label37.TabIndex = 13124
        Me.Label37.Text = "Intervention :"
        '
        'btnClearIntervention
        '
        Me.btnClearIntervention.BackColor = System.Drawing.Color.Transparent
        Me.btnClearIntervention.BackgroundImage = CType(resources.GetObject("btnClearIntervention.BackgroundImage"), System.Drawing.Image)
        Me.btnClearIntervention.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearIntervention.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearIntervention.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearIntervention.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearIntervention.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearIntervention.Image = CType(resources.GetObject("btnClearIntervention.Image"), System.Drawing.Image)
        Me.btnClearIntervention.Location = New System.Drawing.Point(511, 197)
        Me.btnClearIntervention.Name = "btnClearIntervention"
        Me.btnClearIntervention.Size = New System.Drawing.Size(21, 21)
        Me.btnClearIntervention.TabIndex = 11
        Me.btnClearIntervention.UseVisualStyleBackColor = False
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
        'cmbGoals
        '
        Me.cmbGoals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoals.FormattingEnabled = True
        Me.cmbGoals.Location = New System.Drawing.Point(119, 169)
        Me.cmbGoals.Name = "cmbGoals"
        Me.cmbGoals.Size = New System.Drawing.Size(363, 22)
        Me.cmbGoals.TabIndex = 6
        '
        'lblInterventionGoal
        '
        Me.lblInterventionGoal.AutoSize = True
        Me.lblInterventionGoal.Location = New System.Drawing.Point(77, 172)
        Me.lblInterventionGoal.Name = "lblInterventionGoal"
        Me.lblInterventionGoal.Size = New System.Drawing.Size(38, 14)
        Me.lblInterventionGoal.TabIndex = 13123
        Me.lblInterventionGoal.Text = "Goal :"
        '
        'btnGoals
        '
        Me.btnGoals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnGoals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGoals.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoals.Image = CType(resources.GetObject("btnGoals.Image"), System.Drawing.Image)
        Me.btnGoals.Location = New System.Drawing.Point(486, 169)
        Me.btnGoals.Name = "btnGoals"
        Me.btnGoals.Size = New System.Drawing.Size(21, 21)
        Me.btnGoals.TabIndex = 7
        Me.btnGoals.UseVisualStyleBackColor = True
        '
        'btnClearGoals
        '
        Me.btnClearGoals.BackColor = System.Drawing.Color.Transparent
        Me.btnClearGoals.BackgroundImage = CType(resources.GetObject("btnClearGoals.BackgroundImage"), System.Drawing.Image)
        Me.btnClearGoals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearGoals.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearGoals.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearGoals.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearGoals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearGoals.Image = CType(resources.GetObject("btnClearGoals.Image"), System.Drawing.Image)
        Me.btnClearGoals.Location = New System.Drawing.Point(511, 169)
        Me.btnClearGoals.Name = "btnClearGoals"
        Me.btnClearGoals.Size = New System.Drawing.Size(21, 21)
        Me.btnClearGoals.TabIndex = 8
        Me.btnClearGoals.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(53, 22)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(14, 14)
        Me.Label18.TabIndex = 13122
        Me.Label18.Text = "*"
        '
        'tlsp_PatientOutcome
        '
        Me.tlsp_PatientOutcome.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_PatientOutcome.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_PatientOutcome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_PatientOutcome.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_PatientOutcome.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_PatientOutcome.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tlsp_PatientOutcome.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_PatientOutcome.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_PatientOutcome.Name = "tlsp_PatientOutcome"
        Me.tlsp_PatientOutcome.Size = New System.Drawing.Size(554, 53)
        Me.tlsp_PatientOutcome.TabIndex = 13
        Me.tlsp_PatientOutcome.TabStop = True
        Me.tlsp_PatientOutcome.Text = "toolStrip1"
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(13, 48)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(102, 14)
        Me.Label53.TabIndex = 13120
        Me.Label53.Text = "Outcome Notes :"
        '
        'txtOutcomeNotes
        '
        Me.txtOutcomeNotes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOutcomeNotes.Location = New System.Drawing.Point(119, 46)
        Me.txtOutcomeNotes.MaxLength = 500
        Me.txtOutcomeNotes.Name = "txtOutcomeNotes"
        Me.txtOutcomeNotes.Size = New System.Drawing.Size(363, 61)
        Me.txtOutcomeNotes.TabIndex = 1
        Me.txtOutcomeNotes.Text = ""
        '
        'txtOutcomeName
        '
        Me.txtOutcomeName.BackColor = System.Drawing.Color.White
        Me.txtOutcomeName.Location = New System.Drawing.Point(119, 18)
        Me.txtOutcomeName.MaxLength = 200
        Me.txtOutcomeName.Name = "txtOutcomeName"
        Me.txtOutcomeName.Size = New System.Drawing.Size(363, 22)
        Me.txtOutcomeName.TabIndex = 0
        '
        'lblConcernName
        '
        Me.lblConcernName.AutoSize = True
        Me.lblConcernName.Location = New System.Drawing.Point(69, 22)
        Me.lblConcernName.Name = "lblConcernName"
        Me.lblConcernName.Size = New System.Drawing.Size(46, 14)
        Me.lblConcernName.TabIndex = 13121
        Me.lblConcernName.Text = "Name :"
        '
        'dtpOutcomeDate
        '
        Me.dtpOutcomeDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpOutcomeDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpOutcomeDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpOutcomeDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpOutcomeDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpOutcomeDate.CustomFormat = "MM/dd/ yyyy hh:mm:ss tt"
        Me.dtpOutcomeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpOutcomeDate.Location = New System.Drawing.Point(138, 141)
        Me.dtpOutcomeDate.Name = "dtpOutcomeDate"
        Me.dtpOutcomeDate.Size = New System.Drawing.Size(192, 22)
        Me.dtpOutcomeDate.TabIndex = 5
        '
        'chkDate
        '
        Me.chkDate.AutoSize = True
        Me.chkDate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkDate.Location = New System.Drawing.Point(72, 143)
        Me.chkDate.Name = "chkDate"
        Me.chkDate.Size = New System.Drawing.Size(60, 18)
        Me.chkDate.TabIndex = 4
        Me.chkDate.Text = "Date :"
        Me.chkDate.UseVisualStyleBackColor = True
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Location = New System.Drawing.Point(332, 113)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(150, 22)
        Me.cmbUnit.TabIndex = 3
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(291, 117)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(37, 14)
        Me.Label24.TabIndex = 13132
        Me.Label24.Text = "Unit :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(70, 117)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(45, 14)
        Me.Label23.TabIndex = 13131
        Me.Label23.Text = "Value :"
        '
        'txtlValue
        '
        Me.txtlValue.BackColor = System.Drawing.Color.White
        Me.txtlValue.Location = New System.Drawing.Point(119, 113)
        Me.txtlValue.MaxLength = 20
        Me.txtlValue.Name = "txtlValue"
        Me.txtlValue.Size = New System.Drawing.Size(150, 22)
        Me.txtlValue.TabIndex = 2
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.label19)
        Me.pnlMain.Controls.Add(Me.chkDate)
        Me.pnlMain.Controls.Add(Me.label20)
        Me.pnlMain.Controls.Add(Me.cmbUnit)
        Me.pnlMain.Controls.Add(Me.label21)
        Me.pnlMain.Controls.Add(Me.Label24)
        Me.pnlMain.Controls.Add(Me.label22)
        Me.pnlMain.Controls.Add(Me.Label23)
        Me.pnlMain.Controls.Add(Me.txtOutcomeName)
        Me.pnlMain.Controls.Add(Me.txtlValue)
        Me.pnlMain.Controls.Add(Me.lblConcernName)
        Me.pnlMain.Controls.Add(Me.dtpOutcomeDate)
        Me.pnlMain.Controls.Add(Me.txtOutcomeNotes)
        Me.pnlMain.Controls.Add(Me.btnIntervention)
        Me.pnlMain.Controls.Add(Me.Label53)
        Me.pnlMain.Controls.Add(Me.cmbIntervention)
        Me.pnlMain.Controls.Add(Me.Label18)
        Me.pnlMain.Controls.Add(Me.cmbStatus)
        Me.pnlMain.Controls.Add(Me.btnClearGoals)
        Me.pnlMain.Controls.Add(Me.lblInterventionMedication)
        Me.pnlMain.Controls.Add(Me.btnGoals)
        Me.pnlMain.Controls.Add(Me.Label37)
        Me.pnlMain.Controls.Add(Me.lblInterventionGoal)
        Me.pnlMain.Controls.Add(Me.btnClearIntervention)
        Me.pnlMain.Controls.Add(Me.cmbGoals)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(554, 267)
        Me.pnlMain.TabIndex = 13133
        '
        'label19
        '
        Me.label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label19.Location = New System.Drawing.Point(4, 263)
        Me.label19.Name = "label19"
        Me.label19.Size = New System.Drawing.Size(546, 1)
        Me.label19.TabIndex = 12
        Me.label19.Text = "label1"
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.label20.Location = New System.Drawing.Point(4, 3)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(546, 1)
        Me.label20.TabIndex = 11
        Me.label20.Text = "label1"
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.label21.Location = New System.Drawing.Point(3, 3)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(1, 261)
        Me.label21.TabIndex = 10
        Me.label21.Text = "label1"
        '
        'label22
        '
        Me.label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label22.Dock = System.Windows.Forms.DockStyle.Right
        Me.label22.Location = New System.Drawing.Point(550, 3)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(1, 261)
        Me.label22.TabIndex = 9
        Me.label22.Text = "label1"
        '
        'pnlToolstrip
        '
        Me.pnlToolstrip.AutoSize = True
        Me.pnlToolstrip.Controls.Add(Me.tlsp_PatientOutcome)
        Me.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolstrip.Name = "pnlToolstrip"
        Me.pnlToolstrip.Size = New System.Drawing.Size(554, 53)
        Me.pnlToolstrip.TabIndex = 13134
        '
        'frmPatientOutcome
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(554, 320)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientOutcome"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Outcome"
        Me.tlsp_PatientOutcome.ResumeLayout(False)
        Me.tlsp_PatientOutcome.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlToolstrip.ResumeLayout(False)
        Me.pnlToolstrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnIntervention As System.Windows.Forms.Button
    Friend WithEvents cmbIntervention As System.Windows.Forms.ComboBox
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblInterventionMedication As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents btnClearIntervention As System.Windows.Forms.Button
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbGoals As System.Windows.Forms.ComboBox
    Friend WithEvents lblInterventionGoal As System.Windows.Forms.Label
    Friend WithEvents btnGoals As System.Windows.Forms.Button
    Friend WithEvents btnClearGoals As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents tlsp_PatientOutcome As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtOutcomeNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents txtOutcomeName As System.Windows.Forms.TextBox
    Friend WithEvents lblConcernName As System.Windows.Forms.Label
    Friend WithEvents dtpOutcomeDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkDate As System.Windows.Forms.CheckBox
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtlValue As System.Windows.Forms.TextBox
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents label19 As System.Windows.Forms.Label
    Private WithEvents label20 As System.Windows.Forms.Label
    Private WithEvents label21 As System.Windows.Forms.Label
    Private WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents pnlToolstrip As System.Windows.Forms.Panel
End Class
