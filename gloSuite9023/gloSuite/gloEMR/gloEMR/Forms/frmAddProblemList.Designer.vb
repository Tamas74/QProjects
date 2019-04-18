<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddProblemList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

                Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpOnsetDate, dtResolved}
                Dim cntControls() As System.Windows.Forms.Control = {dtpOnsetDate, dtResolved}
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                If (IsNothing(dtpControls) = False) Then
                    If dtpControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    End If
                End If

                If (IsNothing(cntControls) = False) Then
                    If cntControls.Length > 0 Then
                        gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                    End If
                End If


            End If
            
            If (IsNothing(toolTipSnomed) = False) Then
                toolTipSnomed.Dispose()
                toolTipSnomed = Nothing
            End If
            If (IsNothing(toolTipIcd) = False) Then
                toolTipIcd.Dispose()
                toolTipIcd = Nothing
            End If
            If (IsNothing(toolTipDescription) = False) Then
                toolTipDescription.Dispose()
                toolTipDescription = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddProblemList))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lbl_Problem = New System.Windows.Forms.Label()
        Me.lbl_OnsetDate = New System.Windows.Forms.Label()
        Me.lbl_Location = New System.Windows.Forms.Label()
        Me.lbl_Provider = New System.Windows.Forms.Label()
        Me.txt_Problem = New System.Windows.Forms.TextBox()
        Me.btn_Problem = New System.Windows.Forms.Button()
        Me.gb_Status = New System.Windows.Forms.GroupBox()
        Me.dtResolved = New System.Windows.Forms.DateTimePicker()
        Me.rbInactive = New System.Windows.Forms.RadioButton()
        Me.rbtn_Active = New System.Windows.Forms.RadioButton()
        Me.rbt_Inactive = New System.Windows.Forms.RadioButton()
        Me.gb_Immediacy = New System.Windows.Forms.GroupBox()
        Me.rbtn_Unknown = New System.Windows.Forms.RadioButton()
        Me.rbtn_Chronic = New System.Windows.Forms.RadioButton()
        Me.rbt_Acute = New System.Windows.Forms.RadioButton()
        Me.dgComments = New System.Windows.Forms.DataGridView()
        Me.Col_Date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Comments = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_Add = New System.Windows.Forms.Button()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.btn_Remove = New System.Windows.Forms.Button()
        Me.cmb_Provider = New System.Windows.Forms.ComboBox()
        Me.dtpOnsetDate = New System.Windows.Forms.DateTimePicker()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tls_comment = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlb_OK = New System.Windows.Forms.ToolStripButton()
        Me.tlb_Cancle = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.cmbProblemType = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmbConcernStatus = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.pnlcustomTask = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtLaterality = New System.Windows.Forms.TextBox()
        Me.btnClearLaterality = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnBrowseLaterality = New System.Windows.Forms.Button()
        Me.cmb_Priscription = New System.Windows.Forms.ComboBox()
        Me.gb_Comments = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbExams = New System.Windows.Forms.ComboBox()
        Me.txtlocation = New System.Windows.Forms.TextBox()
        Me.lbl_Priscription = New System.Windows.Forms.Label()
        Me.lblDescriptionID = New System.Windows.Forms.Label()
        Me.lblExams = New System.Windows.Forms.Label()
        Me.lblConceptID = New System.Windows.Forms.Label()
        Me.btn_Priscription = New System.Windows.Forms.Button()
        Me.btn_Exams = New System.Windows.Forms.Button()
        Me.pnlDischargeDate = New System.Windows.Forms.Panel()
        Me.dtpDischargeDate = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.pnlSNOMED = New System.Windows.Forms.Panel()
        Me.txtSnomed = New System.Windows.Forms.TextBox()
        Me.btnBrowseICD9 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnBrowserSnomedCode = New System.Windows.Forms.Button()
        Me.lblICDRevision = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblSnomedCodeMandatory = New System.Windows.Forms.Label()
        Me.txtICD9 = New System.Windows.Forms.TextBox()
        Me.chkEncounter = New System.Windows.Forms.CheckBox()
        Me.btnClearSnomed = New System.Windows.Forms.Button()
        Me.btnClearICD = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gb_Status.SuspendLayout()
        Me.gb_Immediacy.SuspendLayout()
        CType(Me.dgComments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tls_comment.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlStatus.SuspendLayout()
        Me.pnlcustomTask.SuspendLayout()
        Me.gb_Comments.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlDischargeDate.SuspendLayout()
        Me.pnlSNOMED.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbl_Problem
        '
        Me.lbl_Problem.AutoSize = True
        Me.lbl_Problem.Location = New System.Drawing.Point(30, 68)
        Me.lbl_Problem.Name = "lbl_Problem"
        Me.lbl_Problem.Size = New System.Drawing.Size(75, 14)
        Me.lbl_Problem.TabIndex = 0
        Me.lbl_Problem.Text = "Description :"
        '
        'lbl_OnsetDate
        '
        Me.lbl_OnsetDate.AutoSize = True
        Me.lbl_OnsetDate.Location = New System.Drawing.Point(27, 96)
        Me.lbl_OnsetDate.Name = "lbl_OnsetDate"
        Me.lbl_OnsetDate.Size = New System.Drawing.Size(78, 14)
        Me.lbl_OnsetDate.TabIndex = 1
        Me.lbl_OnsetDate.Text = "Onset Date :"
        '
        'lbl_Location
        '
        Me.lbl_Location.AutoSize = True
        Me.lbl_Location.Location = New System.Drawing.Point(46, 385)
        Me.lbl_Location.Name = "lbl_Location"
        Me.lbl_Location.Size = New System.Drawing.Size(61, 14)
        Me.lbl_Location.TabIndex = 2
        Me.lbl_Location.Text = "Location :"
        '
        'lbl_Provider
        '
        Me.lbl_Provider.AutoSize = True
        Me.lbl_Provider.Location = New System.Drawing.Point(48, 414)
        Me.lbl_Provider.Name = "lbl_Provider"
        Me.lbl_Provider.Size = New System.Drawing.Size(59, 14)
        Me.lbl_Provider.TabIndex = 3
        Me.lbl_Provider.Text = "Provider :"
        '
        'txt_Problem
        '
        Me.txt_Problem.BackColor = System.Drawing.Color.White
        Me.txt_Problem.Location = New System.Drawing.Point(109, 64)
        Me.txt_Problem.Name = "txt_Problem"
        Me.txt_Problem.Size = New System.Drawing.Size(388, 22)
        Me.txt_Problem.TabIndex = 7
        '
        'btn_Problem
        '
        Me.btn_Problem.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btn_Problem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Problem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Problem.Image = CType(resources.GetObject("btn_Problem.Image"), System.Drawing.Image)
        Me.btn_Problem.Location = New System.Drawing.Point(501, 65)
        Me.btn_Problem.Name = "btn_Problem"
        Me.btn_Problem.Size = New System.Drawing.Size(21, 21)
        Me.btn_Problem.TabIndex = 6
        Me.btn_Problem.UseVisualStyleBackColor = True
        Me.btn_Problem.Visible = False
        '
        'gb_Status
        '
        Me.gb_Status.Controls.Add(Me.dtResolved)
        Me.gb_Status.Controls.Add(Me.rbInactive)
        Me.gb_Status.Controls.Add(Me.rbtn_Active)
        Me.gb_Status.Controls.Add(Me.rbt_Inactive)
        Me.gb_Status.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_Status.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gb_Status.Location = New System.Drawing.Point(20, 59)
        Me.gb_Status.Name = "gb_Status"
        Me.gb_Status.Size = New System.Drawing.Size(522, 40)
        Me.gb_Status.TabIndex = 11
        Me.gb_Status.TabStop = False
        Me.gb_Status.Text = "Status"
        '
        'dtResolved
        '
        Me.dtResolved.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtResolved.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtResolved.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtResolved.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtResolved.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtResolved.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtResolved.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtResolved.Location = New System.Drawing.Point(339, 14)
        Me.dtResolved.Name = "dtResolved"
        Me.dtResolved.Size = New System.Drawing.Size(122, 22)
        Me.dtResolved.TabIndex = 3
        Me.dtResolved.Visible = False
        '
        'rbInactive
        '
        Me.rbInactive.AutoSize = True
        Me.rbInactive.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbInactive.Location = New System.Drawing.Point(243, 16)
        Me.rbInactive.Name = "rbInactive"
        Me.rbInactive.Size = New System.Drawing.Size(68, 18)
        Me.rbInactive.TabIndex = 2
        Me.rbInactive.Text = "Inactive"
        Me.rbInactive.UseVisualStyleBackColor = True
        '
        'rbtn_Active
        '
        Me.rbtn_Active.AutoSize = True
        Me.rbtn_Active.Checked = True
        Me.rbtn_Active.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtn_Active.Location = New System.Drawing.Point(58, 16)
        Me.rbtn_Active.Name = "rbtn_Active"
        Me.rbtn_Active.Size = New System.Drawing.Size(63, 18)
        Me.rbtn_Active.TabIndex = 0
        Me.rbtn_Active.TabStop = True
        Me.rbtn_Active.Text = "Active"
        Me.rbtn_Active.UseVisualStyleBackColor = True
        '
        'rbt_Inactive
        '
        Me.rbt_Inactive.AutoSize = True
        Me.rbt_Inactive.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_Inactive.Location = New System.Drawing.Point(148, 16)
        Me.rbt_Inactive.Name = "rbt_Inactive"
        Me.rbt_Inactive.Size = New System.Drawing.Size(73, 18)
        Me.rbt_Inactive.TabIndex = 1
        Me.rbt_Inactive.Text = "Resolved"
        Me.rbt_Inactive.UseVisualStyleBackColor = True
        '
        'gb_Immediacy
        '
        Me.gb_Immediacy.Controls.Add(Me.rbtn_Unknown)
        Me.gb_Immediacy.Controls.Add(Me.rbtn_Chronic)
        Me.gb_Immediacy.Controls.Add(Me.rbt_Acute)
        Me.gb_Immediacy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_Immediacy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gb_Immediacy.Location = New System.Drawing.Point(20, 107)
        Me.gb_Immediacy.Name = "gb_Immediacy"
        Me.gb_Immediacy.Size = New System.Drawing.Size(522, 40)
        Me.gb_Immediacy.TabIndex = 12
        Me.gb_Immediacy.TabStop = False
        Me.gb_Immediacy.Text = "Immediacy"
        '
        'rbtn_Unknown
        '
        Me.rbtn_Unknown.AutoSize = True
        Me.rbtn_Unknown.Checked = True
        Me.rbtn_Unknown.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtn_Unknown.Location = New System.Drawing.Point(244, 16)
        Me.rbtn_Unknown.Name = "rbtn_Unknown"
        Me.rbtn_Unknown.Size = New System.Drawing.Size(83, 18)
        Me.rbtn_Unknown.TabIndex = 2
        Me.rbtn_Unknown.TabStop = True
        Me.rbtn_Unknown.Text = "Unknown"
        Me.rbtn_Unknown.UseVisualStyleBackColor = True
        '
        'rbtn_Chronic
        '
        Me.rbtn_Chronic.AutoSize = True
        Me.rbtn_Chronic.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtn_Chronic.Location = New System.Drawing.Point(149, 16)
        Me.rbtn_Chronic.Name = "rbtn_Chronic"
        Me.rbtn_Chronic.Size = New System.Drawing.Size(65, 18)
        Me.rbtn_Chronic.TabIndex = 1
        Me.rbtn_Chronic.TabStop = True
        Me.rbtn_Chronic.Text = "Chronic"
        Me.rbtn_Chronic.UseVisualStyleBackColor = True
        '
        'rbt_Acute
        '
        Me.rbt_Acute.AutoSize = True
        Me.rbt_Acute.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbt_Acute.Location = New System.Drawing.Point(59, 16)
        Me.rbt_Acute.Name = "rbt_Acute"
        Me.rbt_Acute.Size = New System.Drawing.Size(58, 18)
        Me.rbt_Acute.TabIndex = 0
        Me.rbt_Acute.TabStop = True
        Me.rbt_Acute.Text = "Acute"
        Me.rbt_Acute.UseVisualStyleBackColor = True
        '
        'dgComments
        '
        Me.dgComments.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.dgComments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgComments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgComments.BackgroundColor = System.Drawing.Color.White
        Me.dgComments.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(217, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgComments.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgComments.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Date, Me.Col_Comments})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgComments.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgComments.EnableHeadersVisualStyles = False
        Me.dgComments.GridColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgComments.Location = New System.Drawing.Point(1, 1)
        Me.dgComments.Name = "dgComments"
        Me.dgComments.RowHeadersVisible = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.dgComments.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgComments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgComments.Size = New System.Drawing.Size(434, 148)
        Me.dgComments.TabIndex = 3
        '
        'Col_Date
        '
        Me.Col_Date.FillWeight = 71.066!
        Me.Col_Date.HeaderText = "Date"
        Me.Col_Date.Name = "Col_Date"
        Me.Col_Date.ReadOnly = True
        '
        'Col_Comments
        '
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Col_Comments.DefaultCellStyle = DataGridViewCellStyle3
        Me.Col_Comments.FillWeight = 128.934!
        Me.Col_Comments.HeaderText = "Comment"
        Me.Col_Comments.Name = "Col_Comments"
        Me.Col_Comments.ReadOnly = True
        '
        'btn_Add
        '
        Me.btn_Add.BackgroundImage = CType(resources.GetObject("btn_Add.BackgroundImage"), System.Drawing.Image)
        Me.btn_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Add.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Add.Location = New System.Drawing.Point(448, 21)
        Me.btn_Add.Name = "btn_Add"
        Me.btn_Add.Size = New System.Drawing.Size(67, 25)
        Me.btn_Add.TabIndex = 0
        Me.btn_Add.Text = "&Add"
        Me.btn_Add.UseVisualStyleBackColor = True
        '
        'btn_Edit
        '
        Me.btn_Edit.BackgroundImage = CType(resources.GetObject("btn_Edit.BackgroundImage"), System.Drawing.Image)
        Me.btn_Edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Edit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Edit.Location = New System.Drawing.Point(448, 52)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(67, 25)
        Me.btn_Edit.TabIndex = 1
        Me.btn_Edit.Text = "&Edit"
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'btn_Remove
        '
        Me.btn_Remove.BackgroundImage = CType(resources.GetObject("btn_Remove.BackgroundImage"), System.Drawing.Image)
        Me.btn_Remove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Remove.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Remove.Location = New System.Drawing.Point(448, 83)
        Me.btn_Remove.Name = "btn_Remove"
        Me.btn_Remove.Size = New System.Drawing.Size(67, 25)
        Me.btn_Remove.TabIndex = 2
        Me.btn_Remove.Text = "&Remove"
        Me.btn_Remove.UseVisualStyleBackColor = True
        '
        'cmb_Provider
        '
        Me.cmb_Provider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Provider.FormattingEnabled = True
        Me.cmb_Provider.Location = New System.Drawing.Point(109, 410)
        Me.cmb_Provider.Name = "cmb_Provider"
        Me.cmb_Provider.Size = New System.Drawing.Size(388, 22)
        Me.cmb_Provider.TabIndex = 18
        '
        'dtpOnsetDate
        '
        Me.dtpOnsetDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpOnsetDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpOnsetDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpOnsetDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpOnsetDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpOnsetDate.CustomFormat = "MM/dd/ yyyy"
        Me.dtpOnsetDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpOnsetDate.Location = New System.Drawing.Point(109, 92)
        Me.dtpOnsetDate.Name = "dtpOnsetDate"
        Me.dtpOnsetDate.Size = New System.Drawing.Size(177, 22)
        Me.dtpOnsetDate.TabIndex = 8
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.tls_comment)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(572, 54)
        Me.pnlTop.TabIndex = 1
        '
        'tls_comment
        '
        Me.tls_comment.BackColor = System.Drawing.Color.Transparent
        Me.tls_comment.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_comment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_comment.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_comment.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_comment.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_OK, Me.tlb_Cancle})
        Me.tls_comment.Location = New System.Drawing.Point(0, 0)
        Me.tls_comment.Name = "tls_comment"
        Me.tls_comment.Size = New System.Drawing.Size(572, 53)
        Me.tls_comment.TabIndex = 3
        Me.tls_comment.TabStop = True
        Me.tls_comment.Text = "ToolStrip1"
        '
        'tlb_OK
        '
        Me.tlb_OK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_OK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_OK.Image = CType(resources.GetObject("tlb_OK.Image"), System.Drawing.Image)
        Me.tlb_OK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_OK.Name = "tlb_OK"
        Me.tlb_OK.Size = New System.Drawing.Size(66, 50)
        Me.tlb_OK.Tag = "Save"
        Me.tlb_OK.Text = "Save&&Cls"
        Me.tlb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_OK.ToolTipText = "Save and Close"
        '
        'tlb_Cancle
        '
        Me.tlb_Cancle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Cancle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_Cancle.Image = CType(resources.GetObject("tlb_Cancle.Image"), System.Drawing.Image)
        Me.tlb_Cancle.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Cancle.Name = "tlb_Cancle"
        Me.tlb_Cancle.Size = New System.Drawing.Size(43, 50)
        Me.tlb_Cancle.Tag = "Close"
        Me.tlb_Cancle.Text = "&Close"
        Me.tlb_Cancle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Cancle.ToolTipText = "Close"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlStatus)
        Me.pnlMain.Controls.Add(Me.pnlDischargeDate)
        Me.pnlMain.Controls.Add(Me.pnlSNOMED)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(572, 656)
        Me.pnlMain.TabIndex = 0
        '
        'pnlStatus
        '
        Me.pnlStatus.Controls.Add(Me.cmbProblemType)
        Me.pnlStatus.Controls.Add(Me.Label19)
        Me.pnlStatus.Controls.Add(Me.cmbConcernStatus)
        Me.pnlStatus.Controls.Add(Me.Label18)
        Me.pnlStatus.Controls.Add(Me.gb_Status)
        Me.pnlStatus.Controls.Add(Me.pnlcustomTask)
        Me.pnlStatus.Controls.Add(Me.cmb_Provider)
        Me.pnlStatus.Controls.Add(Me.txtLaterality)
        Me.pnlStatus.Controls.Add(Me.lbl_Provider)
        Me.pnlStatus.Controls.Add(Me.btnClearLaterality)
        Me.pnlStatus.Controls.Add(Me.lbl_Location)
        Me.pnlStatus.Controls.Add(Me.Label10)
        Me.pnlStatus.Controls.Add(Me.gb_Immediacy)
        Me.pnlStatus.Controls.Add(Me.btnBrowseLaterality)
        Me.pnlStatus.Controls.Add(Me.cmb_Priscription)
        Me.pnlStatus.Controls.Add(Me.gb_Comments)
        Me.pnlStatus.Controls.Add(Me.cmbExams)
        Me.pnlStatus.Controls.Add(Me.txtlocation)
        Me.pnlStatus.Controls.Add(Me.lbl_Priscription)
        Me.pnlStatus.Controls.Add(Me.lblDescriptionID)
        Me.pnlStatus.Controls.Add(Me.lblExams)
        Me.pnlStatus.Controls.Add(Me.lblConceptID)
        Me.pnlStatus.Controls.Add(Me.btn_Priscription)
        Me.pnlStatus.Controls.Add(Me.btn_Exams)
        Me.pnlStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlStatus.Location = New System.Drawing.Point(4, 146)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(564, 506)
        Me.pnlStatus.TabIndex = 13002
        '
        'cmbProblemType
        '
        Me.cmbProblemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProblemType.FormattingEnabled = True
        Me.cmbProblemType.Location = New System.Drawing.Point(108, 30)
        Me.cmbProblemType.Name = "cmbProblemType"
        Me.cmbProblemType.Size = New System.Drawing.Size(177, 22)
        Me.cmbProblemType.TabIndex = 13008
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(15, 34)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(91, 14)
        Me.Label19.TabIndex = 13007
        Me.Label19.Text = "Problem Type :"
        '
        'cmbConcernStatus
        '
        Me.cmbConcernStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbConcernStatus.FormattingEnabled = True
        Me.cmbConcernStatus.Location = New System.Drawing.Point(108, 4)
        Me.cmbConcernStatus.Name = "cmbConcernStatus"
        Me.cmbConcernStatus.Size = New System.Drawing.Size(177, 22)
        Me.cmbConcernStatus.TabIndex = 13006
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(7, 8)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(99, 14)
        Me.Label18.TabIndex = 13005
        Me.Label18.Text = "Concern Status :"
        '
        'pnlcustomTask
        '
        Me.pnlcustomTask.Controls.Add(Me.Label15)
        Me.pnlcustomTask.Controls.Add(Me.Label14)
        Me.pnlcustomTask.Controls.Add(Me.Label13)
        Me.pnlcustomTask.Controls.Add(Me.Label12)
        Me.pnlcustomTask.Location = New System.Drawing.Point(16, 158)
        Me.pnlcustomTask.Name = "pnlcustomTask"
        Me.pnlcustomTask.Size = New System.Drawing.Size(525, 187)
        Me.pnlcustomTask.TabIndex = 13000
        Me.pnlcustomTask.Visible = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Location = New System.Drawing.Point(1, 186)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(523, 1)
        Me.Label15.TabIndex = 31
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Location = New System.Drawing.Point(1, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(523, 1)
        Me.Label14.TabIndex = 30
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Location = New System.Drawing.Point(524, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 187)
        Me.Label13.TabIndex = 29
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 187)
        Me.Label12.TabIndex = 28
        '
        'txtLaterality
        '
        Me.txtLaterality.BackColor = System.Drawing.Color.White
        Me.txtLaterality.Location = New System.Drawing.Point(109, 352)
        Me.txtLaterality.Name = "txtLaterality"
        Me.txtLaterality.ReadOnly = True
        Me.txtLaterality.Size = New System.Drawing.Size(388, 22)
        Me.txtLaterality.TabIndex = 16
        '
        'btnClearLaterality
        '
        Me.btnClearLaterality.BackColor = System.Drawing.Color.Transparent
        Me.btnClearLaterality.BackgroundImage = CType(resources.GetObject("btnClearLaterality.BackgroundImage"), System.Drawing.Image)
        Me.btnClearLaterality.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearLaterality.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearLaterality.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearLaterality.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearLaterality.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearLaterality.Image = CType(resources.GetObject("btnClearLaterality.Image"), System.Drawing.Image)
        Me.btnClearLaterality.Location = New System.Drawing.Point(525, 353)
        Me.btnClearLaterality.Name = "btnClearLaterality"
        Me.btnClearLaterality.Size = New System.Drawing.Size(21, 21)
        Me.btnClearLaterality.TabIndex = 15
        Me.btnClearLaterality.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(43, 356)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 14)
        Me.Label10.TabIndex = 224
        Me.Label10.Text = "Laterality :"
        '
        'btnBrowseLaterality
        '
        Me.btnBrowseLaterality.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowseLaterality.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseLaterality.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseLaterality.Image = CType(resources.GetObject("btnBrowseLaterality.Image"), System.Drawing.Image)
        Me.btnBrowseLaterality.Location = New System.Drawing.Point(501, 353)
        Me.btnBrowseLaterality.Name = "btnBrowseLaterality"
        Me.btnBrowseLaterality.Size = New System.Drawing.Size(21, 21)
        Me.btnBrowseLaterality.TabIndex = 14
        Me.btnBrowseLaterality.UseVisualStyleBackColor = True
        '
        'cmb_Priscription
        '
        Me.cmb_Priscription.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Priscription.FormattingEnabled = True
        Me.cmb_Priscription.Location = New System.Drawing.Point(109, 439)
        Me.cmb_Priscription.Name = "cmb_Priscription"
        Me.cmb_Priscription.Size = New System.Drawing.Size(388, 22)
        Me.cmb_Priscription.TabIndex = 20
        '
        'gb_Comments
        '
        Me.gb_Comments.Controls.Add(Me.btn_Add)
        Me.gb_Comments.Controls.Add(Me.btn_Edit)
        Me.gb_Comments.Controls.Add(Me.btn_Remove)
        Me.gb_Comments.Controls.Add(Me.Panel1)
        Me.gb_Comments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gb_Comments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gb_Comments.Location = New System.Drawing.Point(20, 155)
        Me.gb_Comments.Name = "gb_Comments"
        Me.gb_Comments.Size = New System.Drawing.Size(522, 177)
        Me.gb_Comments.TabIndex = 13
        Me.gb_Comments.TabStop = False
        Me.gb_Comments.Text = "Comments"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dgComments)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Location = New System.Drawing.Point(6, 21)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(436, 150)
        Me.Panel1.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Location = New System.Drawing.Point(1, 149)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(434, 1)
        Me.Label8.TabIndex = 31
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(434, 1)
        Me.Label7.TabIndex = 30
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Location = New System.Drawing.Point(435, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 150)
        Me.Label6.TabIndex = 29
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 150)
        Me.Label5.TabIndex = 28
        '
        'cmbExams
        '
        Me.cmbExams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExams.FormattingEnabled = True
        Me.cmbExams.Location = New System.Drawing.Point(109, 468)
        Me.cmbExams.Name = "cmbExams"
        Me.cmbExams.Size = New System.Drawing.Size(388, 22)
        Me.cmbExams.TabIndex = 22
        '
        'txtlocation
        '
        Me.txtlocation.Location = New System.Drawing.Point(109, 381)
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.Size = New System.Drawing.Size(388, 22)
        Me.txtlocation.TabIndex = 17
        '
        'lbl_Priscription
        '
        Me.lbl_Priscription.AutoSize = True
        Me.lbl_Priscription.Location = New System.Drawing.Point(29, 443)
        Me.lbl_Priscription.Name = "lbl_Priscription"
        Me.lbl_Priscription.Size = New System.Drawing.Size(78, 14)
        Me.lbl_Priscription.TabIndex = 29
        Me.lbl_Priscription.Text = "Prescription :"
        '
        'lblDescriptionID
        '
        Me.lblDescriptionID.AutoSize = True
        Me.lblDescriptionID.BackColor = System.Drawing.Color.Transparent
        Me.lblDescriptionID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescriptionID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDescriptionID.Location = New System.Drawing.Point(392, 158)
        Me.lblDescriptionID.Name = "lblDescriptionID"
        Me.lblDescriptionID.Size = New System.Drawing.Size(0, 14)
        Me.lblDescriptionID.TabIndex = 44
        Me.lblDescriptionID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblExams
        '
        Me.lblExams.AutoSize = True
        Me.lblExams.Location = New System.Drawing.Point(58, 472)
        Me.lblExams.Name = "lblExams"
        Me.lblExams.Size = New System.Drawing.Size(49, 14)
        Me.lblExams.TabIndex = 29
        Me.lblExams.Text = "Exams :"
        '
        'lblConceptID
        '
        Me.lblConceptID.AutoSize = True
        Me.lblConceptID.BackColor = System.Drawing.Color.Transparent
        Me.lblConceptID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConceptID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblConceptID.Location = New System.Drawing.Point(104, 158)
        Me.lblConceptID.Name = "lblConceptID"
        Me.lblConceptID.Size = New System.Drawing.Size(0, 14)
        Me.lblConceptID.TabIndex = 42
        Me.lblConceptID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btn_Priscription
        '
        Me.btn_Priscription.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btn_Priscription.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Priscription.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Priscription.Image = CType(resources.GetObject("btn_Priscription.Image"), System.Drawing.Image)
        Me.btn_Priscription.Location = New System.Drawing.Point(501, 438)
        Me.btn_Priscription.Name = "btn_Priscription"
        Me.btn_Priscription.Size = New System.Drawing.Size(21, 21)
        Me.btn_Priscription.TabIndex = 19
        Me.btn_Priscription.UseVisualStyleBackColor = True
        '
        'btn_Exams
        '
        Me.btn_Exams.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btn_Exams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Exams.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Exams.Image = CType(resources.GetObject("btn_Exams.Image"), System.Drawing.Image)
        Me.btn_Exams.Location = New System.Drawing.Point(501, 468)
        Me.btn_Exams.Name = "btn_Exams"
        Me.btn_Exams.Size = New System.Drawing.Size(21, 21)
        Me.btn_Exams.TabIndex = 21
        Me.btn_Exams.UseVisualStyleBackColor = True
        '
        'pnlDischargeDate
        '
        Me.pnlDischargeDate.Controls.Add(Me.dtpDischargeDate)
        Me.pnlDischargeDate.Controls.Add(Me.Label16)
        Me.pnlDischargeDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDischargeDate.Location = New System.Drawing.Point(4, 121)
        Me.pnlDischargeDate.Name = "pnlDischargeDate"
        Me.pnlDischargeDate.Size = New System.Drawing.Size(564, 25)
        Me.pnlDischargeDate.TabIndex = 13002
        '
        'dtpDischargeDate
        '
        Me.dtpDischargeDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpDischargeDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpDischargeDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpDischargeDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpDischargeDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpDischargeDate.CustomFormat = "MM/dd/ yyyy"
        Me.dtpDischargeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDischargeDate.Location = New System.Drawing.Point(108, 1)
        Me.dtpDischargeDate.Name = "dtpDischargeDate"
        Me.dtpDischargeDate.Size = New System.Drawing.Size(177, 22)
        Me.dtpDischargeDate.TabIndex = 10
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(8, 5)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(97, 14)
        Me.Label16.TabIndex = 13001
        Me.Label16.Text = "Discharge Date :"
        '
        'pnlSNOMED
        '
        Me.pnlSNOMED.Controls.Add(Me.txtSnomed)
        Me.pnlSNOMED.Controls.Add(Me.dtpOnsetDate)
        Me.pnlSNOMED.Controls.Add(Me.lbl_OnsetDate)
        Me.pnlSNOMED.Controls.Add(Me.btnBrowseICD9)
        Me.pnlSNOMED.Controls.Add(Me.Label11)
        Me.pnlSNOMED.Controls.Add(Me.lbl_Problem)
        Me.pnlSNOMED.Controls.Add(Me.btn_Problem)
        Me.pnlSNOMED.Controls.Add(Me.txt_Problem)
        Me.pnlSNOMED.Controls.Add(Me.btnBrowserSnomedCode)
        Me.pnlSNOMED.Controls.Add(Me.lblICDRevision)
        Me.pnlSNOMED.Controls.Add(Me.Label9)
        Me.pnlSNOMED.Controls.Add(Me.lblSnomedCodeMandatory)
        Me.pnlSNOMED.Controls.Add(Me.txtICD9)
        Me.pnlSNOMED.Controls.Add(Me.chkEncounter)
        Me.pnlSNOMED.Controls.Add(Me.btnClearSnomed)
        Me.pnlSNOMED.Controls.Add(Me.btnClearICD)
        Me.pnlSNOMED.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSNOMED.Location = New System.Drawing.Point(4, 4)
        Me.pnlSNOMED.Name = "pnlSNOMED"
        Me.pnlSNOMED.Size = New System.Drawing.Size(564, 117)
        Me.pnlSNOMED.TabIndex = 13002
        '
        'txtSnomed
        '
        Me.txtSnomed.BackColor = System.Drawing.Color.White
        Me.txtSnomed.Location = New System.Drawing.Point(109, 8)
        Me.txtSnomed.Name = "txtSnomed"
        Me.txtSnomed.ReadOnly = True
        Me.txtSnomed.Size = New System.Drawing.Size(388, 22)
        Me.txtSnomed.TabIndex = 2
        '
        'btnBrowseICD9
        '
        Me.btnBrowseICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowseICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseICD9.Image = CType(resources.GetObject("btnBrowseICD9.Image"), System.Drawing.Image)
        Me.btnBrowseICD9.Location = New System.Drawing.Point(501, 37)
        Me.btnBrowseICD9.Name = "btnBrowseICD9"
        Me.btnBrowseICD9.Size = New System.Drawing.Size(21, 21)
        Me.btnBrowseICD9.TabIndex = 3
        Me.btnBrowseICD9.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(45, 44)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 14)
        Me.Label11.TabIndex = 45
        Me.Label11.Text = "ICD9/10 :"
        '
        'btnBrowserSnomedCode
        '
        Me.btnBrowserSnomedCode.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowserSnomedCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowserSnomedCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowserSnomedCode.Image = CType(resources.GetObject("btnBrowserSnomedCode.Image"), System.Drawing.Image)
        Me.btnBrowserSnomedCode.Location = New System.Drawing.Point(501, 9)
        Me.btnBrowserSnomedCode.Name = "btnBrowserSnomedCode"
        Me.btnBrowserSnomedCode.Size = New System.Drawing.Size(21, 21)
        Me.btnBrowserSnomedCode.TabIndex = 0
        Me.btnBrowserSnomedCode.UseVisualStyleBackColor = True
        '
        'lblICDRevision
        '
        Me.lblICDRevision.AutoSize = True
        Me.lblICDRevision.BackColor = System.Drawing.Color.Transparent
        Me.lblICDRevision.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblICDRevision.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblICDRevision.Location = New System.Drawing.Point(484, 44)
        Me.lblICDRevision.Name = "lblICDRevision"
        Me.lblICDRevision.Size = New System.Drawing.Size(0, 14)
        Me.lblICDRevision.TabIndex = 222
        Me.lblICDRevision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblICDRevision.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(42, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 14)
        Me.Label9.TabIndex = 48
        Me.Label9.Text = "SNOMED :"
        '
        'lblSnomedCodeMandatory
        '
        Me.lblSnomedCodeMandatory.AutoSize = True
        Me.lblSnomedCodeMandatory.ForeColor = System.Drawing.Color.Red
        Me.lblSnomedCodeMandatory.Location = New System.Drawing.Point(31, 12)
        Me.lblSnomedCodeMandatory.Name = "lblSnomedCodeMandatory"
        Me.lblSnomedCodeMandatory.Size = New System.Drawing.Size(14, 14)
        Me.lblSnomedCodeMandatory.TabIndex = 25
        Me.lblSnomedCodeMandatory.Text = "*"
        Me.lblSnomedCodeMandatory.Visible = False
        '
        'txtICD9
        '
        Me.txtICD9.BackColor = System.Drawing.Color.White
        Me.txtICD9.Location = New System.Drawing.Point(109, 36)
        Me.txtICD9.Name = "txtICD9"
        Me.txtICD9.ReadOnly = True
        Me.txtICD9.Size = New System.Drawing.Size(388, 22)
        Me.txtICD9.TabIndex = 5
        '
        'chkEncounter
        '
        Me.chkEncounter.AutoSize = True
        Me.chkEncounter.Location = New System.Drawing.Point(289, 94)
        Me.chkEncounter.Name = "chkEncounter"
        Me.chkEncounter.Size = New System.Drawing.Size(137, 18)
        Me.chkEncounter.TabIndex = 9
        Me.chkEncounter.Text = "Encounter/Diagnosis"
        Me.chkEncounter.UseVisualStyleBackColor = True
        Me.chkEncounter.Visible = False
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
        Me.btnClearSnomed.Location = New System.Drawing.Point(525, 9)
        Me.btnClearSnomed.Name = "btnClearSnomed"
        Me.btnClearSnomed.Size = New System.Drawing.Size(21, 21)
        Me.btnClearSnomed.TabIndex = 1
        Me.btnClearSnomed.UseVisualStyleBackColor = False
        '
        'btnClearICD
        '
        Me.btnClearICD.BackColor = System.Drawing.Color.Transparent
        Me.btnClearICD.BackgroundImage = CType(resources.GetObject("btnClearICD.BackgroundImage"), System.Drawing.Image)
        Me.btnClearICD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearICD.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearICD.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearICD.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearICD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearICD.Image = CType(resources.GetObject("btnClearICD.Image"), System.Drawing.Image)
        Me.btnClearICD.Location = New System.Drawing.Point(525, 37)
        Me.btnClearICD.Name = "btnClearICD"
        Me.btnClearICD.Size = New System.Drawing.Size(21, 21)
        Me.btnClearICD.TabIndex = 4
        Me.btnClearICD.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(568, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 648)
        Me.Label4.TabIndex = 28
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 648)
        Me.Label3.TabIndex = 27
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 652)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(566, 1)
        Me.Label2.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(566, 1)
        Me.Label1.TabIndex = 25
        '
        'frmAddProblemList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(572, 710)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddProblemList"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Problem List"
        Me.gb_Status.ResumeLayout(False)
        Me.gb_Status.PerformLayout()
        Me.gb_Immediacy.ResumeLayout(False)
        Me.gb_Immediacy.PerformLayout()
        CType(Me.dgComments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tls_comment.ResumeLayout(False)
        Me.tls_comment.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        Me.pnlcustomTask.ResumeLayout(False)
        Me.gb_Comments.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlDischargeDate.ResumeLayout(False)
        Me.pnlDischargeDate.PerformLayout()
        Me.pnlSNOMED.ResumeLayout(False)
        Me.pnlSNOMED.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbl_Problem As System.Windows.Forms.Label
    Friend WithEvents lbl_OnsetDate As System.Windows.Forms.Label
    Friend WithEvents lbl_Location As System.Windows.Forms.Label
    Friend WithEvents lbl_Provider As System.Windows.Forms.Label
    Friend WithEvents txt_Problem As System.Windows.Forms.TextBox
    Friend WithEvents btn_Problem As System.Windows.Forms.Button
    Friend WithEvents gb_Status As System.Windows.Forms.GroupBox
    Friend WithEvents rbt_Inactive As System.Windows.Forms.RadioButton
    Friend WithEvents rbtn_Active As System.Windows.Forms.RadioButton
    Friend WithEvents gb_Immediacy As System.Windows.Forms.GroupBox
    Friend WithEvents rbtn_Unknown As System.Windows.Forms.RadioButton
    Friend WithEvents rbtn_Chronic As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_Acute As System.Windows.Forms.RadioButton
    Friend WithEvents dgComments As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Add As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents btn_Remove As System.Windows.Forms.Button
    Friend WithEvents cmb_Provider As System.Windows.Forms.ComboBox
    Friend WithEvents dtpOnsetDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tls_comment As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlb_OK As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlb_Cancle As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btn_Priscription As System.Windows.Forms.Button
    Friend WithEvents lbl_Priscription As System.Windows.Forms.Label
    Friend WithEvents cmb_Priscription As System.Windows.Forms.ComboBox
    Friend WithEvents pnlcustomTask As System.Windows.Forms.Panel
    Friend WithEvents dtResolved As System.Windows.Forms.DateTimePicker
    Friend WithEvents btn_Exams As System.Windows.Forms.Button
    Friend WithEvents lblExams As System.Windows.Forms.Label
    Friend WithEvents cmbExams As System.Windows.Forms.ComboBox
    Friend WithEvents rbInactive As System.Windows.Forms.RadioButton
    Friend WithEvents lblDescriptionID As System.Windows.Forms.Label
    Friend WithEvents lblConceptID As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnBrowseICD9 As System.Windows.Forms.Button
    Friend WithEvents txtlocation As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents gb_Comments As System.Windows.Forms.GroupBox
    Friend WithEvents Col_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Comments As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnBrowserSnomedCode As System.Windows.Forms.Button
    Friend WithEvents lblSnomedCodeMandatory As System.Windows.Forms.Label
    Friend WithEvents chkEncounter As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearSnomed As System.Windows.Forms.Button
    Friend WithEvents btnClearICD As System.Windows.Forms.Button
    Friend WithEvents txtSnomed As System.Windows.Forms.TextBox
    Friend WithEvents txtICD9 As System.Windows.Forms.TextBox
    Friend WithEvents lblICDRevision As System.Windows.Forms.Label
    Friend WithEvents txtLaterality As System.Windows.Forms.TextBox
    Friend WithEvents btnClearLaterality As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnBrowseLaterality As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents dtpDischargeDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel
    Friend WithEvents pnlDischargeDate As System.Windows.Forms.Panel
    Friend WithEvents pnlSNOMED As System.Windows.Forms.Panel
    Friend WithEvents cmbProblemType As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbConcernStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
End Class
