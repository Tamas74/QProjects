<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientHealthConcern
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientHealthConcern))
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dtpConcernStartDate = New System.Windows.Forms.DateTimePicker()
        Me.cmbProblem = New System.Windows.Forms.ComboBox()
        Me.lblExams = New System.Windows.Forms.Label()
        Me.btn_LoadProblem = New System.Windows.Forms.Button()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.btnBrowserSnomedCode = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnClearSnomed = New System.Windows.Forms.Button()
        Me.gb_Immediacy1 = New System.Windows.Forms.GroupBox()
        Me.rbt_FromBoth = New System.Windows.Forms.RadioButton()
        Me.rbt_FromPatient = New System.Windows.Forms.RadioButton()
        Me.rbt_FromProvider = New System.Windows.Forms.RadioButton()
        Me.gb_Status1 = New System.Windows.Forms.GroupBox()
        Me.rbt_StatusInactive = New System.Windows.Forms.RadioButton()
        Me.rbt_StatusActive = New System.Windows.Forms.RadioButton()
        Me.rbt_StatusCompleted = New System.Windows.Forms.RadioButton()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtHealthConcernNotes = New System.Windows.Forms.RichTextBox()
        Me.txtHealthStatusDesc = New System.Windows.Forms.TextBox()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.btnClearProblems = New System.Windows.Forms.Button()
        Me.txtHealthConcernName = New System.Windows.Forms.TextBox()
        Me.lblConcernName = New System.Windows.Forms.Label()
        Me.tlsp_PatientInjuryDate = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.dtpConcernEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpConcernDate = New System.Windows.Forms.DateTimePicker()
        Me.lbl_Date = New System.Windows.Forms.Label()
        Me.chkEndDate = New System.Windows.Forms.CheckBox()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.chkStartDate = New System.Windows.Forms.CheckBox()
        Me.gb_Status = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.gb_Immediacy = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tlsp_PatientInjuryDate.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.gb_Status.SuspendLayout()
        Me.gb_Immediacy.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(99, 23)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(14, 14)
        Me.Label18.TabIndex = 13043
        Me.Label18.Text = "*"
        '
        'dtpConcernStartDate
        '
        Me.dtpConcernStartDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpConcernStartDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpConcernStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpConcernStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpConcernStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpConcernStartDate.CustomFormat = "MM/dd/ yyyy hh:mm:ss tt"
        Me.dtpConcernStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpConcernStartDate.Location = New System.Drawing.Point(179, 195)
        Me.dtpConcernStartDate.Name = "dtpConcernStartDate"
        Me.dtpConcernStartDate.Size = New System.Drawing.Size(183, 22)
        Me.dtpConcernStartDate.TabIndex = 11
        '
        'cmbProblem
        '
        Me.cmbProblem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProblem.FormattingEnabled = True
        Me.cmbProblem.Location = New System.Drawing.Point(162, 107)
        Me.cmbProblem.Name = "cmbProblem"
        Me.cmbProblem.Size = New System.Drawing.Size(427, 22)
        Me.cmbProblem.TabIndex = 2
        '
        'lblExams
        '
        Me.lblExams.AutoSize = True
        Me.lblExams.Location = New System.Drawing.Point(75, 110)
        Me.lblExams.Name = "lblExams"
        Me.lblExams.Size = New System.Drawing.Size(81, 14)
        Me.lblExams.TabIndex = 13031
        Me.lblExams.Text = "Problem List :"
        '
        'btn_LoadProblem
        '
        Me.btn_LoadProblem.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btn_LoadProblem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_LoadProblem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_LoadProblem.Image = CType(resources.GetObject("btn_LoadProblem.Image"), System.Drawing.Image)
        Me.btn_LoadProblem.Location = New System.Drawing.Point(595, 108)
        Me.btn_LoadProblem.Name = "btn_LoadProblem"
        Me.btn_LoadProblem.Size = New System.Drawing.Size(21, 21)
        Me.btn_LoadProblem.TabIndex = 3
        Me.btn_LoadProblem.UseVisualStyleBackColor = True
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.Color.White
        Me.txtStatus.Location = New System.Drawing.Point(162, 137)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(427, 22)
        Me.txtStatus.TabIndex = 5
        '
        'btnBrowserSnomedCode
        '
        Me.btnBrowserSnomedCode.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowserSnomedCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowserSnomedCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowserSnomedCode.Image = CType(resources.GetObject("btnBrowserSnomedCode.Image"), System.Drawing.Image)
        Me.btnBrowserSnomedCode.Location = New System.Drawing.Point(595, 138)
        Me.btnBrowserSnomedCode.Name = "btnBrowserSnomedCode"
        Me.btnBrowserSnomedCode.Size = New System.Drawing.Size(21, 21)
        Me.btnBrowserSnomedCode.TabIndex = 6
        Me.btnBrowserSnomedCode.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(68, 141)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 14)
        Me.Label9.TabIndex = 13028
        Me.Label9.Text = "Health Status :"
        '
        'btnClearSnomed
        '
        Me.btnClearSnomed.BackColor = System.Drawing.Color.Transparent
        Me.btnClearSnomed.BackgroundImage = CType(resources.GetObject("btnClearSnomed.BackgroundImage"), System.Drawing.Image)
        Me.btnClearSnomed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSnomed.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSnomed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearSnomed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSnomed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearSnomed.Image = CType(resources.GetObject("btnClearSnomed.Image"), System.Drawing.Image)
        Me.btnClearSnomed.Location = New System.Drawing.Point(620, 138)
        Me.btnClearSnomed.Name = "btnClearSnomed"
        Me.btnClearSnomed.Size = New System.Drawing.Size(21, 21)
        Me.btnClearSnomed.TabIndex = 7
        Me.btnClearSnomed.UseVisualStyleBackColor = False
        '
        'gb_Immediacy1
        '
        Me.gb_Immediacy1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_Immediacy1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gb_Immediacy1.Location = New System.Drawing.Point(24, 397)
        Me.gb_Immediacy1.Name = "gb_Immediacy1"
        Me.gb_Immediacy1.Size = New System.Drawing.Size(10, 40)
        Me.gb_Immediacy1.TabIndex = 9
        Me.gb_Immediacy1.TabStop = False
        Me.gb_Immediacy1.Text = "From"
        Me.gb_Immediacy1.Visible = False
        '
        'rbt_FromBoth
        '
        Me.rbt_FromBoth.AutoSize = True
        Me.rbt_FromBoth.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_FromBoth.Location = New System.Drawing.Point(515, 5)
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
        Me.rbt_FromPatient.Location = New System.Drawing.Point(334, 3)
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
        Me.rbt_FromProvider.Location = New System.Drawing.Point(136, 5)
        Me.rbt_FromProvider.Name = "rbt_FromProvider"
        Me.rbt_FromProvider.Size = New System.Drawing.Size(76, 18)
        Me.rbt_FromProvider.TabIndex = 0
        Me.rbt_FromProvider.TabStop = True
        Me.rbt_FromProvider.Text = "Provider"
        Me.rbt_FromProvider.UseVisualStyleBackColor = True
        '
        'gb_Status1
        '
        Me.gb_Status1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_Status1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gb_Status1.Location = New System.Drawing.Point(24, 408)
        Me.gb_Status1.Name = "gb_Status1"
        Me.gb_Status1.Size = New System.Drawing.Size(10, 40)
        Me.gb_Status1.TabIndex = 10
        Me.gb_Status1.TabStop = False
        Me.gb_Status1.Text = "Status"
        Me.gb_Status1.Visible = False
        '
        'rbt_StatusInactive
        '
        Me.rbt_StatusInactive.AutoSize = True
        Me.rbt_StatusInactive.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_StatusInactive.Location = New System.Drawing.Point(515, 5)
        Me.rbt_StatusInactive.Name = "rbt_StatusInactive"
        Me.rbt_StatusInactive.Size = New System.Drawing.Size(86, 18)
        Me.rbt_StatusInactive.TabIndex = 2
        Me.rbt_StatusInactive.Text = "Suspended"
        Me.rbt_StatusInactive.UseVisualStyleBackColor = True
        '
        'rbt_StatusActive
        '
        Me.rbt_StatusActive.AutoSize = True
        Me.rbt_StatusActive.Checked = True
        Me.rbt_StatusActive.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_StatusActive.Location = New System.Drawing.Point(136, 5)
        Me.rbt_StatusActive.Name = "rbt_StatusActive"
        Me.rbt_StatusActive.Size = New System.Drawing.Size(63, 18)
        Me.rbt_StatusActive.TabIndex = 0
        Me.rbt_StatusActive.TabStop = True
        Me.rbt_StatusActive.Text = "Active"
        Me.rbt_StatusActive.UseVisualStyleBackColor = True
        '
        'rbt_StatusCompleted
        '
        Me.rbt_StatusCompleted.AutoSize = True
        Me.rbt_StatusCompleted.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_StatusCompleted.Location = New System.Drawing.Point(334, 3)
        Me.rbt_StatusCompleted.Name = "rbt_StatusCompleted"
        Me.rbt_StatusCompleted.Size = New System.Drawing.Size(84, 18)
        Me.rbt_StatusCompleted.TabIndex = 1
        Me.rbt_StatusCompleted.Text = "Completed"
        Me.rbt_StatusCompleted.UseVisualStyleBackColor = True
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(22, 49)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(135, 14)
        Me.Label53.TabIndex = 13037
        Me.Label53.Text = "Health Concern Notes :"
        '
        'txtHealthConcernNotes
        '
        Me.txtHealthConcernNotes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtHealthConcernNotes.Location = New System.Drawing.Point(162, 51)
        Me.txtHealthConcernNotes.MaxLength = 500
        Me.txtHealthConcernNotes.Name = "txtHealthConcernNotes"
        Me.txtHealthConcernNotes.Size = New System.Drawing.Size(479, 50)
        Me.txtHealthConcernNotes.TabIndex = 1
        Me.txtHealthConcernNotes.Text = ""
        '
        'txtHealthStatusDesc
        '
        Me.txtHealthStatusDesc.BackColor = System.Drawing.Color.White
        Me.txtHealthStatusDesc.Location = New System.Drawing.Point(162, 167)
        Me.txtHealthStatusDesc.MaxLength = 250
        Me.txtHealthStatusDesc.Name = "txtHealthStatusDesc"
        Me.txtHealthStatusDesc.Size = New System.Drawing.Size(481, 22)
        Me.txtHealthStatusDesc.TabIndex = 8
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(34, 171)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(123, 14)
        Me.Label71.TabIndex = 13035
        Me.Label71.Text = "Health Status Desc. :"
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
        Me.btnClearProblems.Location = New System.Drawing.Point(620, 108)
        Me.btnClearProblems.Name = "btnClearProblems"
        Me.btnClearProblems.Size = New System.Drawing.Size(21, 21)
        Me.btnClearProblems.TabIndex = 4
        Me.btnClearProblems.UseVisualStyleBackColor = False
        '
        'txtHealthConcernName
        '
        Me.txtHealthConcernName.BackColor = System.Drawing.Color.White
        Me.txtHealthConcernName.Location = New System.Drawing.Point(162, 19)
        Me.txtHealthConcernName.MaxLength = 100
        Me.txtHealthConcernName.Name = "txtHealthConcernName"
        Me.txtHealthConcernName.Size = New System.Drawing.Size(479, 22)
        Me.txtHealthConcernName.TabIndex = 0
        '
        'lblConcernName
        '
        Me.lblConcernName.AutoSize = True
        Me.lblConcernName.Location = New System.Drawing.Point(111, 23)
        Me.lblConcernName.Name = "lblConcernName"
        Me.lblConcernName.Size = New System.Drawing.Size(46, 14)
        Me.lblConcernName.TabIndex = 13042
        Me.lblConcernName.Text = "Name :"
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
        Me.tlsp_PatientInjuryDate.Size = New System.Drawing.Size(668, 53)
        Me.tlsp_PatientInjuryDate.TabIndex = 15
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
        'dtpConcernEndDate
        '
        Me.dtpConcernEndDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpConcernEndDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpConcernEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpConcernEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpConcernEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpConcernEndDate.CustomFormat = "MM/dd/ yyyy hh:mm:ss tt"
        Me.dtpConcernEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpConcernEndDate.Location = New System.Drawing.Point(460, 195)
        Me.dtpConcernEndDate.Name = "dtpConcernEndDate"
        Me.dtpConcernEndDate.Size = New System.Drawing.Size(183, 22)
        Me.dtpConcernEndDate.TabIndex = 13
        '
        'dtpConcernDate
        '
        Me.dtpConcernDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpConcernDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpConcernDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpConcernDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpConcernDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpConcernDate.CustomFormat = "MM/dd/ yyyy"
        Me.dtpConcernDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpConcernDate.Location = New System.Drawing.Point(10, 422)
        Me.dtpConcernDate.Name = "dtpConcernDate"
        Me.dtpConcernDate.Size = New System.Drawing.Size(125, 22)
        Me.dtpConcernDate.TabIndex = 15
        Me.dtpConcernDate.Visible = False
        '
        'lbl_Date
        '
        Me.lbl_Date.AutoSize = True
        Me.lbl_Date.Location = New System.Drawing.Point(27, 408)
        Me.lbl_Date.Name = "lbl_Date"
        Me.lbl_Date.Size = New System.Drawing.Size(41, 14)
        Me.lbl_Date.TabIndex = 13047
        Me.lbl_Date.Text = "Date :"
        Me.lbl_Date.Visible = False
        '
        'chkEndDate
        '
        Me.chkEndDate.AutoSize = True
        Me.chkEndDate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkEndDate.Location = New System.Drawing.Point(372, 197)
        Me.chkEndDate.Name = "chkEndDate"
        Me.chkEndDate.Size = New System.Drawing.Size(85, 18)
        Me.chkEndDate.TabIndex = 12
        Me.chkEndDate.Text = "End Date :"
        Me.chkEndDate.UseVisualStyleBackColor = True
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.chkStartDate)
        Me.pnlTopRight.Controls.Add(Me.gb_Status)
        Me.pnlTopRight.Controls.Add(Me.gb_Immediacy)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.chkEndDate)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.dtpConcernDate)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.lbl_Date)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.dtpConcernEndDate)
        Me.pnlTopRight.Controls.Add(Me.lblConcernName)
        Me.pnlTopRight.Controls.Add(Me.txtHealthConcernName)
        Me.pnlTopRight.Controls.Add(Me.Label18)
        Me.pnlTopRight.Controls.Add(Me.btnClearProblems)
        Me.pnlTopRight.Controls.Add(Me.Label71)
        Me.pnlTopRight.Controls.Add(Me.dtpConcernStartDate)
        Me.pnlTopRight.Controls.Add(Me.txtHealthStatusDesc)
        Me.pnlTopRight.Controls.Add(Me.cmbProblem)
        Me.pnlTopRight.Controls.Add(Me.txtHealthConcernNotes)
        Me.pnlTopRight.Controls.Add(Me.lblExams)
        Me.pnlTopRight.Controls.Add(Me.Label53)
        Me.pnlTopRight.Controls.Add(Me.btn_LoadProblem)
        Me.pnlTopRight.Controls.Add(Me.gb_Status1)
        Me.pnlTopRight.Controls.Add(Me.txtStatus)
        Me.pnlTopRight.Controls.Add(Me.gb_Immediacy1)
        Me.pnlTopRight.Controls.Add(Me.btnBrowserSnomedCode)
        Me.pnlTopRight.Controls.Add(Me.btnClearSnomed)
        Me.pnlTopRight.Controls.Add(Me.Label9)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(0, 53)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTopRight.Size = New System.Drawing.Size(668, 301)
        Me.pnlTopRight.TabIndex = 13048
        '
        'chkStartDate
        '
        Me.chkStartDate.AutoSize = True
        Me.chkStartDate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkStartDate.Location = New System.Drawing.Point(85, 197)
        Me.chkStartDate.Name = "chkStartDate"
        Me.chkStartDate.Size = New System.Drawing.Size(91, 18)
        Me.chkStartDate.TabIndex = 9
        Me.chkStartDate.Text = "Start Date :"
        Me.chkStartDate.UseVisualStyleBackColor = True
        '
        'gb_Status
        '
        Me.gb_Status.Controls.Add(Me.rbt_StatusInactive)
        Me.gb_Status.Controls.Add(Me.rbt_StatusActive)
        Me.gb_Status.Controls.Add(Me.Label4)
        Me.gb_Status.Controls.Add(Me.rbt_StatusCompleted)
        Me.gb_Status.Location = New System.Drawing.Point(29, 252)
        Me.gb_Status.Name = "gb_Status"
        Me.gb_Status.Size = New System.Drawing.Size(614, 28)
        Me.gb_Status.TabIndex = 13049
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(79, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 14)
        Me.Label4.TabIndex = 13036
        Me.Label4.Text = "Status :"
        '
        'gb_Immediacy
        '
        Me.gb_Immediacy.Controls.Add(Me.rbt_FromBoth)
        Me.gb_Immediacy.Controls.Add(Me.rbt_FromPatient)
        Me.gb_Immediacy.Controls.Add(Me.Label3)
        Me.gb_Immediacy.Controls.Add(Me.rbt_FromProvider)
        Me.gb_Immediacy.Location = New System.Drawing.Point(29, 224)
        Me.gb_Immediacy.Name = "gb_Immediacy"
        Me.gb_Immediacy.Size = New System.Drawing.Size(614, 28)
        Me.gb_Immediacy.TabIndex = 13048
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(86, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 14)
        Me.Label3.TabIndex = 13036
        Me.Label3.Text = "Form :"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(4, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(660, 1)
        Me.Label2.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 294)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(664, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 294)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(3, 297)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(662, 1)
        Me.Label1.TabIndex = 8
        '
        'frmPatientHealthConcern
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(668, 354)
        Me.Controls.Add(Me.pnlTopRight)
        Me.Controls.Add(Me.tlsp_PatientInjuryDate)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientHealthConcern"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Health Concern"
        Me.tlsp_PatientInjuryDate.ResumeLayout(False)
        Me.tlsp_PatientInjuryDate.PerformLayout()
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.gb_Status.ResumeLayout(False)
        Me.gb_Status.PerformLayout()
        Me.gb_Immediacy.ResumeLayout(False)
        Me.gb_Immediacy.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents dtpConcernStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbProblem As System.Windows.Forms.ComboBox
    Friend WithEvents lblExams As System.Windows.Forms.Label
    Friend WithEvents btn_LoadProblem As System.Windows.Forms.Button
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowserSnomedCode As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnClearSnomed As System.Windows.Forms.Button
    Friend WithEvents gb_Immediacy1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbt_FromBoth As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_FromPatient As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_FromProvider As System.Windows.Forms.RadioButton
    Friend WithEvents gb_Status1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbt_StatusInactive As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_StatusActive As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_StatusCompleted As System.Windows.Forms.RadioButton
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtHealthConcernNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents txtHealthStatusDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents btnClearProblems As System.Windows.Forms.Button
    Friend WithEvents txtHealthConcernName As System.Windows.Forms.TextBox
    Friend WithEvents lblConcernName As System.Windows.Forms.Label
    Private WithEvents tlsp_PatientInjuryDate As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents dtpConcernEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpConcernDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbl_Date As System.Windows.Forms.Label
    Friend WithEvents chkEndDate As System.Windows.Forms.CheckBox
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gb_Status As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents gb_Immediacy As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkStartDate As System.Windows.Forms.CheckBox
End Class
