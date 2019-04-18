<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImTransaction
    Inherits gloEMR.frmBaseForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImTransaction))
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cmbAdministred = New System.Windows.Forms.ComboBox()
        Me.dttransaction_date = New System.Windows.Forms.DateTimePicker()
        Me.optRefused = New System.Windows.Forms.RadioButton()
        Me.optReported = New System.Windows.Forms.RadioButton()
        Me.optAdministered = New System.Windows.Forms.RadioButton()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.TabImmunization = New System.Windows.Forms.TabControl()
        Me.TabPageAdministratin = New System.Windows.Forms.TabPage()
        Me.BtnAddVaccineCategory = New System.Windows.Forms.Button()
        Me.BtnAddManufacturerCategory = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.BtnAddTradeNameCategory = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnScan = New System.Windows.Forms.Button()
        Me.txt_refused_by = New System.Windows.Forms.TextBox()
        Me.pnlMvxControl = New System.Windows.Forms.Panel()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.pnlCvxControl = New System.Windows.Forms.Panel()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.pnlTradeNameControl = New System.Windows.Forms.Panel()
        Me.btnClearCPT = New System.Windows.Forms.Button()
        Me.btnClearDiagnosis = New System.Windows.Forms.Button()
        Me.txt_notes = New System.Windows.Forms.TextBox()
        Me.txtNDCcode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnBrowsCPT = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnBrowsDiagnosis = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbFunding = New System.Windows.Forms.ComboBox()
        Me.cmbIcd = New System.Windows.Forms.ComboBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.cmbCpt = New System.Windows.Forms.ComboBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.dtDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.chkSetReminder = New System.Windows.Forms.CheckBox()
        Me.dtexpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.btnSearchsku = New System.Windows.Forms.Button()
        Me.lblRefusedBy = New System.Windows.Forms.Label()
        Me.btnTradeName = New System.Windows.Forms.Button()
        Me.lblRefusalreason = New System.Windows.Forms.Label()
        Me.btnCvx = New System.Windows.Forms.Button()
        Me.cmbRefusalreason = New System.Windows.Forms.ComboBox()
        Me.btnMvx = New System.Windows.Forms.Button()
        Me.txt_refusal_comments = New System.Windows.Forms.TextBox()
        Me.cmbSKU = New System.Windows.Forms.ComboBox()
        Me.cmbLotNumber = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.lblLotNumber = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtMvx = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCvx = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_TradeName = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.txt_vis = New System.Windows.Forms.TextBox()
        Me.cmbRoute = New System.Windows.Forms.ComboBox()
        Me.dtpublication_date = New System.Windows.Forms.DateTimePicker()
        Me.cmbSite = New System.Windows.Forms.ComboBox()
        Me.chk_vis_given = New System.Windows.Forms.CheckBox()
        Me.txt_dosage_given = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txt_amount_given = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txt_units = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblDosesGiven = New System.Windows.Forms.Label()
        Me.TabPageReaction = New System.Windows.Forms.TabPage()
        Me.grpReaction = New System.Windows.Forms.GroupBox()
        Me.dtOnsetDate = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnBrowseReaction = New System.Windows.Forms.Button()
        Me.lstReaction = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rdo_PatientRecoveredNo = New System.Windows.Forms.RadioButton()
        Me.rdo_PatientRecoveredUnknown = New System.Windows.Forms.RadioButton()
        Me.rdo_PatientRecoveredYes = New System.Windows.Forms.RadioButton()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.dtPatientDied = New System.Windows.Forms.DateTimePicker()
        Me.txtHospitalizationDays = New System.Windows.Forms.TextBox()
        Me.chk_NoneOfTheAbove = New System.Windows.Forms.CheckBox()
        Me.chk_ResultedInPermDisability = New System.Windows.Forms.CheckBox()
        Me.chk_RequiredEmergencyRoom = New System.Windows.Forms.CheckBox()
        Me.chk_ResultedInProlongation = New System.Windows.Forms.CheckBox()
        Me.chk_LifeThreateningIllness = New System.Windows.Forms.CheckBox()
        Me.chk_RequiredHospitalization = New System.Windows.Forms.CheckBox()
        Me.chkPatientDied = New System.Windows.Forms.CheckBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtAdverseEvent = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblNote = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.chkPatientHasReaction = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tblStrip = New gloToolStrip.gloToolStrip()
        Me.tblbtn_PrintVis = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Save = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabImmunization.SuspendLayout()
        Me.TabPageAdministratin.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabPageReaction.SuspendLayout()
        Me.grpReaction.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tblStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(16, 55)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(103, 32)
        Me.Label30.TabIndex = 8
        Me.Label30.Text = "Administered or :   Recorded by"
        '
        'cmbAdministred
        '
        Me.cmbAdministred.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbAdministred.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAdministred.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAdministred.FormattingEnabled = True
        Me.cmbAdministred.Location = New System.Drawing.Point(122, 52)
        Me.cmbAdministred.Name = "cmbAdministred"
        Me.cmbAdministred.Size = New System.Drawing.Size(84, 22)
        Me.cmbAdministred.TabIndex = 5
        '
        'dttransaction_date
        '
        Me.dttransaction_date.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dttransaction_date.CalendarMonthBackground = System.Drawing.Color.White
        Me.dttransaction_date.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dttransaction_date.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dttransaction_date.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dttransaction_date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dttransaction_date.Location = New System.Drawing.Point(122, 17)
        Me.dttransaction_date.Name = "dttransaction_date"
        Me.dttransaction_date.Size = New System.Drawing.Size(108, 22)
        Me.dttransaction_date.TabIndex = 1
        '
        'optRefused
        '
        Me.optRefused.AutoSize = True
        Me.optRefused.Location = New System.Drawing.Point(785, 19)
        Me.optRefused.Name = "optRefused"
        Me.optRefused.Size = New System.Drawing.Size(69, 18)
        Me.optRefused.TabIndex = 4
        Me.optRefused.TabStop = True
        Me.optRefused.Text = "Refused"
        Me.optRefused.UseVisualStyleBackColor = True
        '
        'optReported
        '
        Me.optReported.AutoSize = True
        Me.optReported.Location = New System.Drawing.Point(702, 19)
        Me.optReported.Name = "optReported"
        Me.optReported.Size = New System.Drawing.Size(76, 18)
        Me.optReported.TabIndex = 3
        Me.optReported.TabStop = True
        Me.optReported.Text = "Reported"
        Me.optReported.UseVisualStyleBackColor = True
        '
        'optAdministered
        '
        Me.optAdministered.AutoSize = True
        Me.optAdministered.Location = New System.Drawing.Point(599, 19)
        Me.optAdministered.Name = "optAdministered"
        Me.optAdministered.Size = New System.Drawing.Size(96, 18)
        Me.optAdministered.TabIndex = 2
        Me.optAdministered.TabStop = True
        Me.optAdministered.Text = "Administered"
        Me.optAdministered.UseVisualStyleBackColor = True
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(78, 21)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(41, 14)
        Me.Label43.TabIndex = 1
        Me.Label43.Text = "Date :"
        '
        'TabImmunization
        '
        Me.TabImmunization.Controls.Add(Me.TabPageAdministratin)
        Me.TabImmunization.Controls.Add(Me.TabPageReaction)
        Me.TabImmunization.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabImmunization.Location = New System.Drawing.Point(0, 54)
        Me.TabImmunization.Name = "TabImmunization"
        Me.TabImmunization.SelectedIndex = 0
        Me.TabImmunization.Size = New System.Drawing.Size(933, 597)
        Me.TabImmunization.TabIndex = 1
        '
        'TabPageAdministratin
        '
        Me.TabPageAdministratin.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPageAdministratin.Controls.Add(Me.BtnAddVaccineCategory)
        Me.TabPageAdministratin.Controls.Add(Me.BtnAddManufacturerCategory)
        Me.TabPageAdministratin.Controls.Add(Me.Label16)
        Me.TabPageAdministratin.Controls.Add(Me.BtnAddTradeNameCategory)
        Me.TabPageAdministratin.Controls.Add(Me.Label15)
        Me.TabPageAdministratin.Controls.Add(Me.Label5)
        Me.TabPageAdministratin.Controls.Add(Me.Label14)
        Me.TabPageAdministratin.Controls.Add(Me.Label32)
        Me.TabPageAdministratin.Controls.Add(Me.Label11)
        Me.TabPageAdministratin.Controls.Add(Me.Panel2)
        Me.TabPageAdministratin.Controls.Add(Me.txt_refused_by)
        Me.TabPageAdministratin.Controls.Add(Me.pnlMvxControl)
        Me.TabPageAdministratin.Controls.Add(Me.Label47)
        Me.TabPageAdministratin.Controls.Add(Me.pnlCvxControl)
        Me.TabPageAdministratin.Controls.Add(Me.cmbLocation)
        Me.TabPageAdministratin.Controls.Add(Me.cmbProvider)
        Me.TabPageAdministratin.Controls.Add(Me.pnlTradeNameControl)
        Me.TabPageAdministratin.Controls.Add(Me.dttransaction_date)
        Me.TabPageAdministratin.Controls.Add(Me.btnClearCPT)
        Me.TabPageAdministratin.Controls.Add(Me.Label30)
        Me.TabPageAdministratin.Controls.Add(Me.btnClearDiagnosis)
        Me.TabPageAdministratin.Controls.Add(Me.optAdministered)
        Me.TabPageAdministratin.Controls.Add(Me.txt_notes)
        Me.TabPageAdministratin.Controls.Add(Me.optRefused)
        Me.TabPageAdministratin.Controls.Add(Me.txtNDCcode)
        Me.TabPageAdministratin.Controls.Add(Me.Label9)
        Me.TabPageAdministratin.Controls.Add(Me.Label43)
        Me.TabPageAdministratin.Controls.Add(Me.Label24)
        Me.TabPageAdministratin.Controls.Add(Me.optReported)
        Me.TabPageAdministratin.Controls.Add(Me.Label27)
        Me.TabPageAdministratin.Controls.Add(Me.cmbAdministred)
        Me.TabPageAdministratin.Controls.Add(Me.btnBrowsCPT)
        Me.TabPageAdministratin.Controls.Add(Me.Label7)
        Me.TabPageAdministratin.Controls.Add(Me.btnBrowsDiagnosis)
        Me.TabPageAdministratin.Controls.Add(Me.Label6)
        Me.TabPageAdministratin.Controls.Add(Me.Label8)
        Me.TabPageAdministratin.Controls.Add(Me.Label31)
        Me.TabPageAdministratin.Controls.Add(Me.Label10)
        Me.TabPageAdministratin.Controls.Add(Me.cmbFunding)
        Me.TabPageAdministratin.Controls.Add(Me.cmbIcd)
        Me.TabPageAdministratin.Controls.Add(Me.Label41)
        Me.TabPageAdministratin.Controls.Add(Me.cmbCpt)
        Me.TabPageAdministratin.Controls.Add(Me.Label39)
        Me.TabPageAdministratin.Controls.Add(Me.Label33)
        Me.TabPageAdministratin.Controls.Add(Me.Label36)
        Me.TabPageAdministratin.Controls.Add(Me.Label38)
        Me.TabPageAdministratin.Controls.Add(Me.Label34)
        Me.TabPageAdministratin.Controls.Add(Me.dtDueDate)
        Me.TabPageAdministratin.Controls.Add(Me.Label29)
        Me.TabPageAdministratin.Controls.Add(Me.chkSetReminder)
        Me.TabPageAdministratin.Controls.Add(Me.dtexpDate)
        Me.TabPageAdministratin.Controls.Add(Me.Label25)
        Me.TabPageAdministratin.Controls.Add(Me.btnSearchsku)
        Me.TabPageAdministratin.Controls.Add(Me.lblRefusedBy)
        Me.TabPageAdministratin.Controls.Add(Me.btnTradeName)
        Me.TabPageAdministratin.Controls.Add(Me.lblRefusalreason)
        Me.TabPageAdministratin.Controls.Add(Me.btnCvx)
        Me.TabPageAdministratin.Controls.Add(Me.cmbRefusalreason)
        Me.TabPageAdministratin.Controls.Add(Me.btnMvx)
        Me.TabPageAdministratin.Controls.Add(Me.txt_refusal_comments)
        Me.TabPageAdministratin.Controls.Add(Me.cmbSKU)
        Me.TabPageAdministratin.Controls.Add(Me.cmbLotNumber)
        Me.TabPageAdministratin.Controls.Add(Me.Label28)
        Me.TabPageAdministratin.Controls.Add(Me.Label4)
        Me.TabPageAdministratin.Controls.Add(Me.Label35)
        Me.TabPageAdministratin.Controls.Add(Me.lblLotNumber)
        Me.TabPageAdministratin.Controls.Add(Me.Label40)
        Me.TabPageAdministratin.Controls.Add(Me.Label26)
        Me.TabPageAdministratin.Controls.Add(Me.txtMvx)
        Me.TabPageAdministratin.Controls.Add(Me.Label1)
        Me.TabPageAdministratin.Controls.Add(Me.txtCvx)
        Me.TabPageAdministratin.Controls.Add(Me.Label2)
        Me.TabPageAdministratin.Controls.Add(Me.txt_TradeName)
        Me.TabPageAdministratin.Controls.Add(Me.Label67)
        Me.TabPageAdministratin.Controls.Add(Me.txt_vis)
        Me.TabPageAdministratin.Controls.Add(Me.cmbRoute)
        Me.TabPageAdministratin.Controls.Add(Me.dtpublication_date)
        Me.TabPageAdministratin.Controls.Add(Me.cmbSite)
        Me.TabPageAdministratin.Controls.Add(Me.chk_vis_given)
        Me.TabPageAdministratin.Controls.Add(Me.txt_dosage_given)
        Me.TabPageAdministratin.Controls.Add(Me.Label37)
        Me.TabPageAdministratin.Controls.Add(Me.txt_amount_given)
        Me.TabPageAdministratin.Controls.Add(Me.Label23)
        Me.TabPageAdministratin.Controls.Add(Me.txt_units)
        Me.TabPageAdministratin.Controls.Add(Me.Label44)
        Me.TabPageAdministratin.Controls.Add(Me.lblDosesGiven)
        Me.TabPageAdministratin.Location = New System.Drawing.Point(4, 23)
        Me.TabPageAdministratin.Name = "TabPageAdministratin"
        Me.TabPageAdministratin.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAdministratin.Size = New System.Drawing.Size(925, 570)
        Me.TabPageAdministratin.TabIndex = 0
        Me.TabPageAdministratin.Text = "Administration"
        '
        'BtnAddVaccineCategory
        '
        Me.BtnAddVaccineCategory.BackColor = System.Drawing.Color.Transparent
        Me.BtnAddVaccineCategory.BackgroundImage = CType(resources.GetObject("BtnAddVaccineCategory.BackgroundImage"), System.Drawing.Image)
        Me.BtnAddVaccineCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnAddVaccineCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAddVaccineCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnAddVaccineCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddVaccineCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddVaccineCategory.Image = CType(resources.GetObject("BtnAddVaccineCategory.Image"), System.Drawing.Image)
        Me.BtnAddVaccineCategory.Location = New System.Drawing.Point(888, 86)
        Me.BtnAddVaccineCategory.Name = "BtnAddVaccineCategory"
        Me.BtnAddVaccineCategory.Size = New System.Drawing.Size(24, 23)
        Me.BtnAddVaccineCategory.TabIndex = 11
        Me.BtnAddVaccineCategory.TabStop = False
        Me.BtnAddVaccineCategory.Text = "          "
        Me.ToolTip2.SetToolTip(Me.BtnAddVaccineCategory, "Add Vaccine")
        Me.BtnAddVaccineCategory.UseVisualStyleBackColor = False
        '
        'BtnAddManufacturerCategory
        '
        Me.BtnAddManufacturerCategory.BackColor = System.Drawing.Color.Transparent
        Me.BtnAddManufacturerCategory.BackgroundImage = CType(resources.GetObject("BtnAddManufacturerCategory.BackgroundImage"), System.Drawing.Image)
        Me.BtnAddManufacturerCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnAddManufacturerCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAddManufacturerCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnAddManufacturerCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddManufacturerCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddManufacturerCategory.Image = CType(resources.GetObject("BtnAddManufacturerCategory.Image"), System.Drawing.Image)
        Me.BtnAddManufacturerCategory.Location = New System.Drawing.Point(888, 121)
        Me.BtnAddManufacturerCategory.Name = "BtnAddManufacturerCategory"
        Me.BtnAddManufacturerCategory.Size = New System.Drawing.Size(24, 23)
        Me.BtnAddManufacturerCategory.TabIndex = 17
        Me.BtnAddManufacturerCategory.TabStop = False
        Me.BtnAddManufacturerCategory.Text = "          "
        Me.ToolTip2.SetToolTip(Me.BtnAddManufacturerCategory, "Add Manufacturer")
        Me.BtnAddManufacturerCategory.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Location = New System.Drawing.Point(921, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 562)
        Me.Label16.TabIndex = 17
        '
        'BtnAddTradeNameCategory
        '
        Me.BtnAddTradeNameCategory.BackColor = System.Drawing.Color.Transparent
        Me.BtnAddTradeNameCategory.BackgroundImage = CType(resources.GetObject("BtnAddTradeNameCategory.BackgroundImage"), System.Drawing.Image)
        Me.BtnAddTradeNameCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnAddTradeNameCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAddTradeNameCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnAddTradeNameCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddTradeNameCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddTradeNameCategory.Image = CType(resources.GetObject("BtnAddTradeNameCategory.Image"), System.Drawing.Image)
        Me.BtnAddTradeNameCategory.Location = New System.Drawing.Point(448, 121)
        Me.BtnAddTradeNameCategory.Name = "BtnAddTradeNameCategory"
        Me.BtnAddTradeNameCategory.Size = New System.Drawing.Size(24, 23)
        Me.BtnAddTradeNameCategory.TabIndex = 14
        Me.BtnAddTradeNameCategory.TabStop = False
        Me.BtnAddTradeNameCategory.Text = "          "
        Me.BtnAddTradeNameCategory.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip2.SetToolTip(Me.BtnAddTradeNameCategory, "Add Trade Name")
        Me.BtnAddTradeNameCategory.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Location = New System.Drawing.Point(3, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 562)
        Me.Label15.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(530, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 14)
        Me.Label5.TabIndex = 342
        Me.Label5.Text = "*"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Location = New System.Drawing.Point(3, 566)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(919, 1)
        Me.Label14.TabIndex = 8
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(497, 297)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(103, 14)
        Me.Label32.TabIndex = 45
        Me.Label32.Text = "Publication Date :"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Location = New System.Drawing.Point(3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(919, 1)
        Me.Label11.TabIndex = 14
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnView)
        Me.Panel2.Controls.Add(Me.btnScan)
        Me.Panel2.Location = New System.Drawing.Point(422, 291)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(61, 24)
        Me.Panel2.TabIndex = 27
        Me.Panel2.Visible = False
        '
        'btnView
        '
        Me.btnView.BackgroundImage = CType(resources.GetObject("btnView.BackgroundImage"), System.Drawing.Image)
        Me.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnView.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnView.Image = CType(resources.GetObject("btnView.Image"), System.Drawing.Image)
        Me.btnView.Location = New System.Drawing.Point(24, 0)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(24, 24)
        Me.btnView.TabIndex = 1
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnScan
        '
        Me.btnScan.BackgroundImage = CType(resources.GetObject("btnScan.BackgroundImage"), System.Drawing.Image)
        Me.btnScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnScan.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnScan.Image = CType(resources.GetObject("btnScan.Image"), System.Drawing.Image)
        Me.btnScan.Location = New System.Drawing.Point(0, 0)
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(24, 24)
        Me.btnScan.TabIndex = 0
        Me.btnScan.UseVisualStyleBackColor = True
        '
        'txt_refused_by
        '
        Me.txt_refused_by.Location = New System.Drawing.Point(122, 363)
        Me.txt_refused_by.MaxLength = 50
        Me.txt_refused_by.Name = "txt_refused_by"
        Me.txt_refused_by.Size = New System.Drawing.Size(294, 22)
        Me.txt_refused_by.TabIndex = 30
        '
        'pnlMvxControl
        '
        Me.pnlMvxControl.Location = New System.Drawing.Point(603, 147)
        Me.pnlMvxControl.Name = "pnlMvxControl"
        Me.pnlMvxControl.Size = New System.Drawing.Size(255, 165)
        Me.pnlMvxControl.TabIndex = 328
        Me.pnlMvxControl.Visible = False
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(541, 56)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(59, 14)
        Me.Label47.TabIndex = 11
        Me.Label47.Text = "Provider :"
        '
        'pnlCvxControl
        '
        Me.pnlCvxControl.Location = New System.Drawing.Point(602, 112)
        Me.pnlCvxControl.Name = "pnlCvxControl"
        Me.pnlCvxControl.Size = New System.Drawing.Size(255, 165)
        Me.pnlCvxControl.TabIndex = 327
        Me.pnlCvxControl.Visible = False
        '
        'cmbLocation
        '
        Me.cmbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(297, 52)
        Me.cmbLocation.MaxLength = 1000
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(119, 22)
        Me.cmbLocation.TabIndex = 6
        '
        'cmbProvider
        '
        Me.cmbProvider.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbProvider.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(602, 52)
        Me.cmbProvider.MaxLength = 1000
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(256, 22)
        Me.cmbProvider.TabIndex = 6
        '
        'pnlTradeNameControl
        '
        Me.pnlTradeNameControl.Location = New System.Drawing.Point(122, 147)
        Me.pnlTradeNameControl.Name = "pnlTradeNameControl"
        Me.pnlTradeNameControl.Size = New System.Drawing.Size(294, 165)
        Me.pnlTradeNameControl.TabIndex = 326
        Me.pnlTradeNameControl.Visible = False
        '
        'btnClearCPT
        '
        Me.btnClearCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnClearCPT.BackgroundImage = CType(resources.GetObject("btnClearCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearCPT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearCPT.Image = CType(resources.GetObject("btnClearCPT.Image"), System.Drawing.Image)
        Me.btnClearCPT.Location = New System.Drawing.Point(888, 428)
        Me.btnClearCPT.Name = "btnClearCPT"
        Me.btnClearCPT.Size = New System.Drawing.Size(24, 23)
        Me.btnClearCPT.TabIndex = 38
        Me.btnClearCPT.UseVisualStyleBackColor = False
        '
        'btnClearDiagnosis
        '
        Me.btnClearDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.btnClearDiagnosis.BackgroundImage = CType(resources.GetObject("btnClearDiagnosis.BackgroundImage"), System.Drawing.Image)
        Me.btnClearDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearDiagnosis.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearDiagnosis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearDiagnosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearDiagnosis.Image = CType(resources.GetObject("btnClearDiagnosis.Image"), System.Drawing.Image)
        Me.btnClearDiagnosis.Location = New System.Drawing.Point(448, 428)
        Me.btnClearDiagnosis.Name = "btnClearDiagnosis"
        Me.btnClearDiagnosis.Size = New System.Drawing.Size(24, 23)
        Me.btnClearDiagnosis.TabIndex = 35
        Me.btnClearDiagnosis.UseVisualStyleBackColor = False
        '
        'txt_notes
        '
        Me.txt_notes.Location = New System.Drawing.Point(122, 499)
        Me.txt_notes.MaxLength = 1000
        Me.txt_notes.Multiline = True
        Me.txt_notes.Name = "txt_notes"
        Me.txt_notes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_notes.Size = New System.Drawing.Size(294, 48)
        Me.txt_notes.TabIndex = 41
        '
        'txtNDCcode
        '
        Me.txtNDCcode.Location = New System.Drawing.Point(122, 464)
        Me.txtNDCcode.MaxLength = 11
        Me.txtNDCcode.Name = "txtNDCcode"
        Me.txtNDCcode.Size = New System.Drawing.Size(294, 22)
        Me.txtNDCcode.TabIndex = 39
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(233, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 14)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Location :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(46, 502)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(73, 14)
        Me.Label24.TabIndex = 70
        Me.Label24.Text = "Comments :"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(542, 468)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(58, 14)
        Me.Label27.TabIndex = 68
        Me.Label27.Text = "Funding :"
        '
        'btnBrowsCPT
        '
        Me.btnBrowsCPT.BackgroundImage = CType(resources.GetObject("btnBrowsCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowsCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsCPT.Image = CType(resources.GetObject("btnBrowsCPT.Image"), System.Drawing.Image)
        Me.btnBrowsCPT.Location = New System.Drawing.Point(861, 428)
        Me.btnBrowsCPT.Name = "btnBrowsCPT"
        Me.btnBrowsCPT.Size = New System.Drawing.Size(24, 23)
        Me.btnBrowsCPT.TabIndex = 37
        Me.ToolTip2.SetToolTip(Me.btnBrowsCPT, "Select CPT")
        Me.btnBrowsCPT.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(584, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(14, 14)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "*"
        '
        'btnBrowsDiagnosis
        '
        Me.btnBrowsDiagnosis.BackgroundImage = CType(resources.GetObject("btnBrowsDiagnosis.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowsDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsDiagnosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsDiagnosis.Image = CType(resources.GetObject("btnBrowsDiagnosis.Image"), System.Drawing.Image)
        Me.btnBrowsDiagnosis.Location = New System.Drawing.Point(421, 428)
        Me.btnBrowsDiagnosis.Name = "btnBrowsDiagnosis"
        Me.btnBrowsDiagnosis.Size = New System.Drawing.Size(24, 23)
        Me.btnBrowsDiagnosis.TabIndex = 34
        Me.ToolTip2.SetToolTip(Me.btnBrowsDiagnosis, "Select Diagnosis")
        Me.btnBrowsDiagnosis.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(527, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(14, 14)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "*"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(220, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(14, 14)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "*"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(55, 432)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(64, 14)
        Me.Label31.TabIndex = 58
        Me.Label31.Text = "Diagnosis :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(65, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(14, 14)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "*"
        '
        'cmbFunding
        '
        Me.cmbFunding.FormattingEnabled = True
        Me.cmbFunding.Location = New System.Drawing.Point(602, 464)
        Me.cmbFunding.Name = "cmbFunding"
        Me.cmbFunding.Size = New System.Drawing.Size(255, 22)
        Me.cmbFunding.TabIndex = 40
        '
        'cmbIcd
        '
        Me.cmbIcd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIcd.FormattingEnabled = True
        Me.cmbIcd.Location = New System.Drawing.Point(122, 429)
        Me.cmbIcd.MaxLength = 1000
        Me.cmbIcd.Name = "cmbIcd"
        Me.cmbIcd.Size = New System.Drawing.Size(294, 22)
        Me.cmbIcd.TabIndex = 33
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(82, 91)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(37, 14)
        Me.Label41.TabIndex = 13
        Me.Label41.Text = "SKU :"
        '
        'cmbCpt
        '
        Me.cmbCpt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCpt.FormattingEnabled = True
        Me.cmbCpt.Location = New System.Drawing.Point(602, 429)
        Me.cmbCpt.MaxLength = 1000
        Me.cmbCpt.Name = "cmbCpt"
        Me.cmbCpt.Size = New System.Drawing.Size(255, 22)
        Me.cmbCpt.TabIndex = 36
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(513, 126)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(87, 14)
        Me.Label39.TabIndex = 23
        Me.Label39.Text = "Manufacturer :"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(81, 467)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(38, 14)
        Me.Label33.TabIndex = 66
        Me.Label33.Text = "NDC :"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(543, 91)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(57, 14)
        Me.Label36.TabIndex = 16
        Me.Label36.Text = "Vaccine :"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(563, 433)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(37, 14)
        Me.Label38.TabIndex = 62
        Me.Label38.Text = "CPT :"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(39, 160)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(80, 14)
        Me.Label34.TabIndex = 27
        Me.Label34.Text = "Lot Number :"
        '
        'dtDueDate
        '
        Me.dtDueDate.Checked = False
        Me.dtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDueDate.Location = New System.Drawing.Point(602, 396)
        Me.dtDueDate.Name = "dtDueDate"
        Me.dtDueDate.ShowCheckBox = True
        Me.dtDueDate.Size = New System.Drawing.Size(108, 22)
        Me.dtDueDate.TabIndex = 32
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(37, 125)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(82, 14)
        Me.Label29.TabIndex = 20
        Me.Label29.Text = "Trade Name :"
        '
        'chkSetReminder
        '
        Me.chkSetReminder.AutoSize = True
        Me.chkSetReminder.Location = New System.Drawing.Point(122, 398)
        Me.chkSetReminder.Name = "chkSetReminder"
        Me.chkSetReminder.Size = New System.Drawing.Size(108, 18)
        Me.chkSetReminder.TabIndex = 31
        Me.chkSetReminder.Text = "Set Reminder  "
        Me.chkSetReminder.UseVisualStyleBackColor = True
        '
        'dtexpDate
        '
        Me.dtexpDate.Checked = False
        Me.dtexpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtexpDate.Location = New System.Drawing.Point(605, 157)
        Me.dtexpDate.Name = "dtexpDate"
        Me.dtexpDate.ShowCheckBox = True
        Me.dtexpDate.Size = New System.Drawing.Size(114, 22)
        Me.dtexpDate.TabIndex = 19
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(533, 400)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(67, 14)
        Me.Label25.TabIndex = 56
        Me.Label25.Text = "Due Date :"
        '
        'btnSearchsku
        '
        Me.btnSearchsku.BackgroundImage = CType(resources.GetObject("btnSearchsku.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchsku.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchsku.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchsku.Image = CType(resources.GetObject("btnSearchsku.Image"), System.Drawing.Image)
        Me.btnSearchsku.Location = New System.Drawing.Point(421, 87)
        Me.btnSearchsku.Name = "btnSearchsku"
        Me.btnSearchsku.Size = New System.Drawing.Size(24, 23)
        Me.btnSearchsku.TabIndex = 8
        Me.ToolTip2.SetToolTip(Me.btnSearchsku, "Select SKU")
        Me.btnSearchsku.UseVisualStyleBackColor = True
        '
        'lblRefusedBy
        '
        Me.lblRefusedBy.AutoSize = True
        Me.lblRefusedBy.ForeColor = System.Drawing.Color.Red
        Me.lblRefusedBy.Location = New System.Drawing.Point(31, 367)
        Me.lblRefusedBy.Name = "lblRefusedBy"
        Me.lblRefusedBy.Size = New System.Drawing.Size(14, 14)
        Me.lblRefusedBy.TabIndex = 52
        Me.lblRefusedBy.Text = "*"
        '
        'btnTradeName
        '
        Me.btnTradeName.BackgroundImage = CType(resources.GetObject("btnTradeName.BackgroundImage"), System.Drawing.Image)
        Me.btnTradeName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTradeName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTradeName.Image = CType(resources.GetObject("btnTradeName.Image"), System.Drawing.Image)
        Me.btnTradeName.Location = New System.Drawing.Point(421, 121)
        Me.btnTradeName.Name = "btnTradeName"
        Me.btnTradeName.Size = New System.Drawing.Size(24, 23)
        Me.btnTradeName.TabIndex = 13
        Me.ToolTip2.SetToolTip(Me.btnTradeName, "Select Trade Name")
        Me.btnTradeName.UseVisualStyleBackColor = True
        '
        'lblRefusalreason
        '
        Me.lblRefusalreason.AutoSize = True
        Me.lblRefusalreason.ForeColor = System.Drawing.Color.Red
        Me.lblRefusalreason.Location = New System.Drawing.Point(10, 331)
        Me.lblRefusalreason.Name = "lblRefusalreason"
        Me.lblRefusalreason.Size = New System.Drawing.Size(14, 14)
        Me.lblRefusalreason.TabIndex = 47
        Me.lblRefusalreason.Text = "*"
        '
        'btnCvx
        '
        Me.btnCvx.BackgroundImage = CType(resources.GetObject("btnCvx.BackgroundImage"), System.Drawing.Image)
        Me.btnCvx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCvx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCvx.Image = CType(resources.GetObject("btnCvx.Image"), System.Drawing.Image)
        Me.btnCvx.Location = New System.Drawing.Point(861, 86)
        Me.btnCvx.Name = "btnCvx"
        Me.btnCvx.Size = New System.Drawing.Size(24, 23)
        Me.btnCvx.TabIndex = 10
        Me.ToolTip2.SetToolTip(Me.btnCvx, "Select Vaccine")
        Me.btnCvx.UseVisualStyleBackColor = True
        '
        'cmbRefusalreason
        '
        Me.cmbRefusalreason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbRefusalreason.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbRefusalreason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRefusalreason.FormattingEnabled = True
        Me.cmbRefusalreason.Location = New System.Drawing.Point(122, 328)
        Me.cmbRefusalreason.MaxLength = 1000
        Me.cmbRefusalreason.Name = "cmbRefusalreason"
        Me.cmbRefusalreason.Size = New System.Drawing.Size(294, 22)
        Me.cmbRefusalreason.TabIndex = 29
        '
        'btnMvx
        '
        Me.btnMvx.BackgroundImage = CType(resources.GetObject("btnMvx.BackgroundImage"), System.Drawing.Image)
        Me.btnMvx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMvx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMvx.Image = CType(resources.GetObject("btnMvx.Image"), System.Drawing.Image)
        Me.btnMvx.Location = New System.Drawing.Point(861, 121)
        Me.btnMvx.Name = "btnMvx"
        Me.btnMvx.Size = New System.Drawing.Size(24, 23)
        Me.btnMvx.TabIndex = 16
        Me.ToolTip2.SetToolTip(Me.btnMvx, "Select Manufacturer")
        Me.btnMvx.UseVisualStyleBackColor = True
        '
        'txt_refusal_comments
        '
        Me.txt_refusal_comments.Location = New System.Drawing.Point(602, 328)
        Me.txt_refusal_comments.MaxLength = 1000
        Me.txt_refusal_comments.Multiline = True
        Me.txt_refusal_comments.Name = "txt_refusal_comments"
        Me.txt_refusal_comments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_refusal_comments.Size = New System.Drawing.Size(255, 52)
        Me.txt_refusal_comments.TabIndex = 30
        '
        'cmbSKU
        '
        Me.cmbSKU.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSKU.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSKU.FormattingEnabled = True
        Me.cmbSKU.Location = New System.Drawing.Point(122, 87)
        Me.cmbSKU.MaxLength = 15
        Me.cmbSKU.Name = "cmbSKU"
        Me.cmbSKU.Size = New System.Drawing.Size(294, 22)
        Me.cmbSKU.TabIndex = 7
        '
        'cmbLotNumber
        '
        Me.cmbLotNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbLotNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLotNumber.FormattingEnabled = True
        Me.cmbLotNumber.Location = New System.Drawing.Point(122, 157)
        Me.cmbLotNumber.MaxLength = 50
        Me.cmbLotNumber.Name = "cmbLotNumber"
        Me.cmbLotNumber.Size = New System.Drawing.Size(294, 22)
        Me.cmbLotNumber.TabIndex = 18
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(43, 367)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(76, 14)
        Me.Label28.TabIndex = 53
        Me.Label28.Text = "Refused by :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(26, 125)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 14)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "*"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(485, 329)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(115, 14)
        Me.Label35.TabIndex = 50
        Me.Label35.Text = "Refusal Comments :"
        '
        'lblLotNumber
        '
        Me.lblLotNumber.AutoSize = True
        Me.lblLotNumber.ForeColor = System.Drawing.Color.Red
        Me.lblLotNumber.Location = New System.Drawing.Point(26, 160)
        Me.lblLotNumber.Name = "lblLotNumber"
        Me.lblLotNumber.Size = New System.Drawing.Size(14, 14)
        Me.lblLotNumber.TabIndex = 26
        Me.lblLotNumber.Text = "*"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(23, 331)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(96, 14)
        Me.Label40.TabIndex = 48
        Me.Label40.Text = "Refusal Reason :"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(531, 161)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(69, 14)
        Me.Label26.TabIndex = 29
        Me.Label26.Text = "Exp. Date :"
        '
        'txtMvx
        '
        Me.txtMvx.Location = New System.Drawing.Point(602, 122)
        Me.txtMvx.MaxLength = 1000
        Me.txtMvx.Name = "txtMvx"
        Me.txtMvx.Size = New System.Drawing.Size(256, 22)
        Me.txtMvx.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(71, 231)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 14)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Route :"
        '
        'txtCvx
        '
        Me.txtCvx.Location = New System.Drawing.Point(602, 87)
        Me.txtCvx.MaxLength = 1000
        Me.txtCvx.Name = "txtCvx"
        Me.txtCvx.Size = New System.Drawing.Size(256, 22)
        Me.txtCvx.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(714, 196)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Units :"
        '
        'txt_TradeName
        '
        Me.txt_TradeName.Location = New System.Drawing.Point(122, 122)
        Me.txt_TradeName.MaxLength = 1000
        Me.txt_TradeName.Name = "txt_TradeName"
        Me.txt_TradeName.Size = New System.Drawing.Size(294, 22)
        Me.txt_TradeName.TabIndex = 12
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(564, 231)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(36, 14)
        Me.Label67.TabIndex = 40
        Me.Label67.Text = "Site :"
        '
        'txt_vis
        '
        Me.txt_vis.Enabled = False
        Me.txt_vis.Location = New System.Drawing.Point(122, 293)
        Me.txt_vis.MaxLength = 1000
        Me.txt_vis.Name = "txt_vis"
        Me.txt_vis.Size = New System.Drawing.Size(287, 22)
        Me.txt_vis.TabIndex = 26
        '
        'cmbRoute
        '
        Me.cmbRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRoute.FormattingEnabled = True
        Me.cmbRoute.Location = New System.Drawing.Point(122, 227)
        Me.cmbRoute.MaxLength = 100
        Me.cmbRoute.Name = "cmbRoute"
        Me.cmbRoute.Size = New System.Drawing.Size(294, 22)
        Me.cmbRoute.TabIndex = 23
        '
        'dtpublication_date
        '
        Me.dtpublication_date.Checked = False
        Me.dtpublication_date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpublication_date.Location = New System.Drawing.Point(602, 293)
        Me.dtpublication_date.Name = "dtpublication_date"
        Me.dtpublication_date.ShowCheckBox = True
        Me.dtpublication_date.Size = New System.Drawing.Size(114, 22)
        Me.dtpublication_date.TabIndex = 28
        '
        'cmbSite
        '
        Me.cmbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSite.FormattingEnabled = True
        Me.cmbSite.Location = New System.Drawing.Point(602, 227)
        Me.cmbSite.MaxLength = 100
        Me.cmbSite.Name = "cmbSite"
        Me.cmbSite.Size = New System.Drawing.Size(114, 22)
        Me.cmbSite.TabIndex = 24
        '
        'chk_vis_given
        '
        Me.chk_vis_given.AutoSize = True
        Me.chk_vis_given.Location = New System.Drawing.Point(122, 262)
        Me.chk_vis_given.Name = "chk_vis_given"
        Me.chk_vis_given.Size = New System.Drawing.Size(138, 18)
        Me.chk_vis_given.TabIndex = 25
        Me.chk_vis_given.Text = "VIS Given to Patient"
        Me.chk_vis_given.UseVisualStyleBackColor = True
        '
        'txt_dosage_given
        '
        Me.txt_dosage_given.Location = New System.Drawing.Point(122, 192)
        Me.txt_dosage_given.MaxLength = 6
        Me.txt_dosage_given.Name = "txt_dosage_given"
        Me.txt_dosage_given.Size = New System.Drawing.Size(91, 22)
        Me.txt_dosage_given.TabIndex = 20
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(85, 297)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(34, 14)
        Me.Label37.TabIndex = 43
        Me.Label37.Text = "VIS :"
        '
        'txt_amount_given
        '
        Me.txt_amount_given.Location = New System.Drawing.Point(605, 192)
        Me.txt_amount_given.MaxLength = 6
        Me.txt_amount_given.Name = "txt_amount_given"
        Me.txt_amount_given.Size = New System.Drawing.Size(79, 22)
        Me.txt_amount_given.TabIndex = 21
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(38, 196)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(81, 14)
        Me.Label23.TabIndex = 32
        Me.Label23.Text = "Doses Given :"
        '
        'txt_units
        '
        Me.txt_units.Location = New System.Drawing.Point(761, 192)
        Me.txt_units.MaxLength = 12
        Me.txt_units.Name = "txt_units"
        Me.txt_units.Size = New System.Drawing.Size(97, 22)
        Me.txt_units.TabIndex = 22
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(507, 196)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(93, 14)
        Me.Label44.TabIndex = 34
        Me.Label44.Text = "Amount Given :"
        '
        'lblDosesGiven
        '
        Me.lblDosesGiven.AutoSize = True
        Me.lblDosesGiven.ForeColor = System.Drawing.Color.Red
        Me.lblDosesGiven.Location = New System.Drawing.Point(24, 196)
        Me.lblDosesGiven.Name = "lblDosesGiven"
        Me.lblDosesGiven.Size = New System.Drawing.Size(14, 14)
        Me.lblDosesGiven.TabIndex = 31
        Me.lblDosesGiven.Text = "*"
        '
        'TabPageReaction
        '
        Me.TabPageReaction.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPageReaction.Controls.Add(Me.grpReaction)
        Me.TabPageReaction.Controls.Add(Me.lblNote)
        Me.TabPageReaction.Controls.Add(Me.Label20)
        Me.TabPageReaction.Controls.Add(Me.Label19)
        Me.TabPageReaction.Controls.Add(Me.Label18)
        Me.TabPageReaction.Controls.Add(Me.Label17)
        Me.TabPageReaction.Controls.Add(Me.chkPatientHasReaction)
        Me.TabPageReaction.Location = New System.Drawing.Point(4, 23)
        Me.TabPageReaction.Name = "TabPageReaction"
        Me.TabPageReaction.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageReaction.Size = New System.Drawing.Size(925, 570)
        Me.TabPageReaction.TabIndex = 1
        Me.TabPageReaction.Text = "Reaction  "
        '
        'grpReaction
        '
        Me.grpReaction.Controls.Add(Me.dtOnsetDate)
        Me.grpReaction.Controls.Add(Me.Label12)
        Me.grpReaction.Controls.Add(Me.btnBrowseReaction)
        Me.grpReaction.Controls.Add(Me.lstReaction)
        Me.grpReaction.Controls.Add(Me.Label3)
        Me.grpReaction.Controls.Add(Me.rdo_PatientRecoveredNo)
        Me.grpReaction.Controls.Add(Me.rdo_PatientRecoveredUnknown)
        Me.grpReaction.Controls.Add(Me.rdo_PatientRecoveredYes)
        Me.grpReaction.Controls.Add(Me.Label46)
        Me.grpReaction.Controls.Add(Me.dtPatientDied)
        Me.grpReaction.Controls.Add(Me.txtHospitalizationDays)
        Me.grpReaction.Controls.Add(Me.chk_NoneOfTheAbove)
        Me.grpReaction.Controls.Add(Me.chk_ResultedInPermDisability)
        Me.grpReaction.Controls.Add(Me.chk_RequiredEmergencyRoom)
        Me.grpReaction.Controls.Add(Me.chk_ResultedInProlongation)
        Me.grpReaction.Controls.Add(Me.chk_LifeThreateningIllness)
        Me.grpReaction.Controls.Add(Me.chk_RequiredHospitalization)
        Me.grpReaction.Controls.Add(Me.chkPatientDied)
        Me.grpReaction.Controls.Add(Me.Label42)
        Me.grpReaction.Controls.Add(Me.Label45)
        Me.grpReaction.Controls.Add(Me.Label22)
        Me.grpReaction.Controls.Add(Me.txtAdverseEvent)
        Me.grpReaction.Controls.Add(Me.Label21)
        Me.grpReaction.Location = New System.Drawing.Point(21, 37)
        Me.grpReaction.Name = "grpReaction"
        Me.grpReaction.Size = New System.Drawing.Size(880, 520)
        Me.grpReaction.TabIndex = 2
        Me.grpReaction.TabStop = False
        '
        'dtOnsetDate
        '
        Me.dtOnsetDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtOnsetDate.Location = New System.Drawing.Point(100, 19)
        Me.dtOnsetDate.Name = "dtOnsetDate"
        Me.dtOnsetDate.Size = New System.Drawing.Size(109, 22)
        Me.dtOnsetDate.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(19, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(78, 14)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Onset Date :"
        '
        'btnBrowseReaction
        '
        Me.btnBrowseReaction.BackgroundImage = CType(resources.GetObject("btnBrowseReaction.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseReaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseReaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseReaction.Image = CType(resources.GetObject("btnBrowseReaction.Image"), System.Drawing.Image)
        Me.btnBrowseReaction.Location = New System.Drawing.Point(157, 399)
        Me.btnBrowseReaction.Name = "btnBrowseReaction"
        Me.btnBrowseReaction.Size = New System.Drawing.Size(123, 25)
        Me.btnBrowseReaction.TabIndex = 22
        Me.btnBrowseReaction.Text = " Add to History"
        Me.btnBrowseReaction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnBrowseReaction.UseVisualStyleBackColor = True
        '
        'lstReaction
        '
        Me.lstReaction.FormattingEnabled = True
        Me.lstReaction.ItemHeight = 14
        Me.lstReaction.Location = New System.Drawing.Point(19, 429)
        Me.lstReaction.Name = "lstReaction"
        Me.lstReaction.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lstReaction.Size = New System.Drawing.Size(261, 74)
        Me.lstReaction.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 404)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(133, 14)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Patient Allergy record :"
        '
        'rdo_PatientRecoveredNo
        '
        Me.rdo_PatientRecoveredNo.AutoSize = True
        Me.rdo_PatientRecoveredNo.Location = New System.Drawing.Point(200, 356)
        Me.rdo_PatientRecoveredNo.Name = "rdo_PatientRecoveredNo"
        Me.rdo_PatientRecoveredNo.Size = New System.Drawing.Size(39, 17)
        Me.rdo_PatientRecoveredNo.TabIndex = 18
        Me.rdo_PatientRecoveredNo.TabStop = True
        Me.rdo_PatientRecoveredNo.Text = "No"
        Me.rdo_PatientRecoveredNo.UseVisualStyleBackColor = True
        '
        'rdo_PatientRecoveredUnknown
        '
        Me.rdo_PatientRecoveredUnknown.AutoSize = True
        Me.rdo_PatientRecoveredUnknown.Location = New System.Drawing.Point(254, 356)
        Me.rdo_PatientRecoveredUnknown.Name = "rdo_PatientRecoveredUnknown"
        Me.rdo_PatientRecoveredUnknown.Size = New System.Drawing.Size(71, 17)
        Me.rdo_PatientRecoveredUnknown.TabIndex = 19
        Me.rdo_PatientRecoveredUnknown.TabStop = True
        Me.rdo_PatientRecoveredUnknown.Text = "Unknown"
        Me.rdo_PatientRecoveredUnknown.UseVisualStyleBackColor = True
        '
        'rdo_PatientRecoveredYes
        '
        Me.rdo_PatientRecoveredYes.AutoSize = True
        Me.rdo_PatientRecoveredYes.Location = New System.Drawing.Point(141, 356)
        Me.rdo_PatientRecoveredYes.Name = "rdo_PatientRecoveredYes"
        Me.rdo_PatientRecoveredYes.Size = New System.Drawing.Size(43, 17)
        Me.rdo_PatientRecoveredYes.TabIndex = 17
        Me.rdo_PatientRecoveredYes.TabStop = True
        Me.rdo_PatientRecoveredYes.Text = "Yes"
        Me.rdo_PatientRecoveredYes.UseVisualStyleBackColor = True
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(17, 358)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(113, 14)
        Me.Label46.TabIndex = 16
        Me.Label46.Text = "Patient recovered :"
        '
        'dtPatientDied
        '
        Me.dtPatientDied.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtPatientDied.Location = New System.Drawing.Point(174, 191)
        Me.dtPatientDied.Name = "dtPatientDied"
        Me.dtPatientDied.Size = New System.Drawing.Size(109, 22)
        Me.dtPatientDied.TabIndex = 7
        '
        'txtHospitalizationDays
        '
        Me.txtHospitalizationDays.Location = New System.Drawing.Point(174, 257)
        Me.txtHospitalizationDays.MaxLength = 4
        Me.txtHospitalizationDays.Name = "txtHospitalizationDays"
        Me.txtHospitalizationDays.Size = New System.Drawing.Size(58, 22)
        Me.txtHospitalizationDays.TabIndex = 11
        '
        'chk_NoneOfTheAbove
        '
        Me.chk_NoneOfTheAbove.AutoSize = True
        Me.chk_NoneOfTheAbove.Location = New System.Drawing.Point(19, 325)
        Me.chk_NoneOfTheAbove.Name = "chk_NoneOfTheAbove"
        Me.chk_NoneOfTheAbove.Size = New System.Drawing.Size(115, 17)
        Me.chk_NoneOfTheAbove.TabIndex = 15
        Me.chk_NoneOfTheAbove.Text = "None of the above"
        Me.chk_NoneOfTheAbove.UseVisualStyleBackColor = True
        '
        'chk_ResultedInPermDisability
        '
        Me.chk_ResultedInPermDisability.AutoSize = True
        Me.chk_ResultedInPermDisability.Location = New System.Drawing.Point(19, 303)
        Me.chk_ResultedInPermDisability.Name = "chk_ResultedInPermDisability"
        Me.chk_ResultedInPermDisability.Size = New System.Drawing.Size(174, 17)
        Me.chk_ResultedInPermDisability.TabIndex = 14
        Me.chk_ResultedInPermDisability.Text = "Resulted in permanent disability"
        Me.chk_ResultedInPermDisability.UseVisualStyleBackColor = True
        '
        'chk_RequiredEmergencyRoom
        '
        Me.chk_RequiredEmergencyRoom.AutoSize = True
        Me.chk_RequiredEmergencyRoom.Location = New System.Drawing.Point(19, 237)
        Me.chk_RequiredEmergencyRoom.Name = "chk_RequiredEmergencyRoom"
        Me.chk_RequiredEmergencyRoom.Size = New System.Drawing.Size(206, 17)
        Me.chk_RequiredEmergencyRoom.TabIndex = 9
        Me.chk_RequiredEmergencyRoom.Text = "Required emergency room/doctor visit"
        Me.chk_RequiredEmergencyRoom.UseVisualStyleBackColor = True
        '
        'chk_ResultedInProlongation
        '
        Me.chk_ResultedInProlongation.AutoSize = True
        Me.chk_ResultedInProlongation.Location = New System.Drawing.Point(19, 281)
        Me.chk_ResultedInProlongation.Name = "chk_ResultedInProlongation"
        Me.chk_ResultedInProlongation.Size = New System.Drawing.Size(221, 17)
        Me.chk_ResultedInProlongation.TabIndex = 13
        Me.chk_ResultedInProlongation.Text = "Resulted in prolongation of hospitalization"
        Me.chk_ResultedInProlongation.UseVisualStyleBackColor = True
        '
        'chk_LifeThreateningIllness
        '
        Me.chk_LifeThreateningIllness.AutoSize = True
        Me.chk_LifeThreateningIllness.Location = New System.Drawing.Point(19, 215)
        Me.chk_LifeThreateningIllness.Name = "chk_LifeThreateningIllness"
        Me.chk_LifeThreateningIllness.Size = New System.Drawing.Size(130, 17)
        Me.chk_LifeThreateningIllness.TabIndex = 8
        Me.chk_LifeThreateningIllness.Text = "Life threatening illness"
        Me.chk_LifeThreateningIllness.UseVisualStyleBackColor = True
        '
        'chk_RequiredHospitalization
        '
        Me.chk_RequiredHospitalization.AutoSize = True
        Me.chk_RequiredHospitalization.Location = New System.Drawing.Point(19, 259)
        Me.chk_RequiredHospitalization.Name = "chk_RequiredHospitalization"
        Me.chk_RequiredHospitalization.Size = New System.Drawing.Size(138, 17)
        Me.chk_RequiredHospitalization.TabIndex = 10
        Me.chk_RequiredHospitalization.Text = "Required hospitalization"
        Me.chk_RequiredHospitalization.UseVisualStyleBackColor = True
        '
        'chkPatientDied
        '
        Me.chkPatientDied.AutoSize = True
        Me.chkPatientDied.Location = New System.Drawing.Point(19, 193)
        Me.chkPatientDied.Name = "chkPatientDied"
        Me.chkPatientDied.Size = New System.Drawing.Size(82, 17)
        Me.chkPatientDied.TabIndex = 5
        Me.chkPatientDied.Text = "Patient died"
        Me.chkPatientDied.UseVisualStyleBackColor = True
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(131, 195)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(41, 14)
        Me.Label42.TabIndex = 6
        Me.Label42.Text = "Date :"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(237, 261)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(32, 14)
        Me.Label45.TabIndex = 12
        Me.Label45.Text = "Days"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(16, 171)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(144, 14)
        Me.Label22.TabIndex = 4
        Me.Label22.Text = "Check all appropriate :"
        '
        'txtAdverseEvent
        '
        Me.txtAdverseEvent.Location = New System.Drawing.Point(13, 74)
        Me.txtAdverseEvent.MaxLength = 1000
        Me.txtAdverseEvent.Multiline = True
        Me.txtAdverseEvent.Name = "txtAdverseEvent"
        Me.txtAdverseEvent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAdverseEvent.Size = New System.Drawing.Size(853, 81)
        Me.txtAdverseEvent.TabIndex = 3
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(13, 55)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(446, 14)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "Describe adverse event(s) (symptoms, signs, time course) and treatment, if any"
        '
        'lblNote
        '
        Me.lblNote.AutoSize = True
        Me.lblNote.ForeColor = System.Drawing.Color.Red
        Me.lblNote.Location = New System.Drawing.Point(192, 19)
        Me.lblNote.Name = "lblNote"
        Me.lblNote.Size = New System.Drawing.Size(218, 14)
        Me.lblNote.TabIndex = 36
        Me.lblNote.Text = "(Vaccine refusal cannot have reaction)"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Location = New System.Drawing.Point(4, 566)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(917, 1)
        Me.Label20.TabIndex = 20
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Location = New System.Drawing.Point(4, 3)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(917, 1)
        Me.Label19.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Location = New System.Drawing.Point(921, 3)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 564)
        Me.Label18.TabIndex = 18
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Location = New System.Drawing.Point(3, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 564)
        Me.Label17.TabIndex = 17
        '
        'chkPatientHasReaction
        '
        Me.chkPatientHasReaction.AutoSize = True
        Me.chkPatientHasReaction.Location = New System.Drawing.Point(39, 18)
        Me.chkPatientHasReaction.Name = "chkPatientHasReaction"
        Me.chkPatientHasReaction.Size = New System.Drawing.Size(135, 17)
        Me.chkPatientHasReaction.TabIndex = 1
        Me.chkPatientHasReaction.Text = "Patient had a Reaction"
        Me.chkPatientHasReaction.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tblStrip)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(933, 54)
        Me.Panel1.TabIndex = 18
        '
        'tblStrip
        '
        Me.tblStrip.AddSeparatorsBetweenEachButton = False
        Me.tblStrip.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip.ButtonsToHide = CType(resources.GetObject("tblStrip.ButtonsToHide"), System.Collections.ArrayList)
        Me.tblStrip.ConnectionString = Nothing
        Me.tblStrip.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.tblStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_PrintVis, Me.tblbtn_Save, Me.tblbtn_Close})
        Me.tblStrip.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip.ModuleName = Nothing
        Me.tblStrip.Name = "tblStrip"
        Me.tblStrip.Size = New System.Drawing.Size(933, 53)
        Me.tblStrip.TabIndex = 1
        Me.tblStrip.TabStop = True
        Me.tblStrip.Text = "ToolStrip1"
        Me.tblStrip.UserID = CType(0, Long)
        '
        'tblbtn_PrintVis
        '
        Me.tblbtn_PrintVis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_PrintVis.Image = CType(resources.GetObject("tblbtn_PrintVis.Image"), System.Drawing.Image)
        Me.tblbtn_PrintVis.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_PrintVis.Name = "tblbtn_PrintVis"
        Me.tblbtn_PrintVis.Size = New System.Drawing.Size(36, 50)
        Me.tblbtn_PrintVis.Tag = "Print Due"
        Me.tblbtn_PrintVis.Text = "&VIS"
        Me.tblbtn_PrintVis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_PrintVis.ToolTipText = "Vaccine Information Statement "
        '
        'tblbtn_Save
        '
        Me.tblbtn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Save.Image = CType(resources.GetObject("tblbtn_Save.Image"), System.Drawing.Image)
        Me.tblbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Save.Name = "tblbtn_Save"
        Me.tblbtn_Save.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Save.Tag = "Save and Close"
        Me.tblbtn_Save.Text = "&Save&&Cls"
        Me.tblbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Save.ToolTipText = "Save and Close"
        '
        'tblbtn_Close
        '
        Me.tblbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close.Image = CType(resources.GetObject("tblbtn_Close.Image"), System.Drawing.Image)
        Me.tblbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close.Name = "tblbtn_Close"
        Me.tblbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close.Tag = "Close"
        Me.tblbtn_Close.Text = "&Close"
        Me.tblbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Close.ToolTipText = "Close"
        '
        'frmImTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(933, 651)
        Me.Controls.Add(Me.TabImmunization)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImTransaction"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Immunization"
        Me.TabImmunization.ResumeLayout(False)
        Me.TabPageAdministratin.ResumeLayout(False)
        Me.TabPageAdministratin.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.TabPageReaction.ResumeLayout(False)
        Me.TabPageReaction.PerformLayout()
        Me.grpReaction.ResumeLayout(False)
        Me.grpReaction.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tblStrip.ResumeLayout(False)
        Me.tblStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbAdministred As System.Windows.Forms.ComboBox
    Friend WithEvents dttransaction_date As System.Windows.Forms.DateTimePicker
    Friend WithEvents optRefused As System.Windows.Forms.RadioButton
    Friend WithEvents optReported As System.Windows.Forms.RadioButton
    Friend WithEvents optAdministered As System.Windows.Forms.RadioButton
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents TabImmunization As System.Windows.Forms.TabControl
    Friend WithEvents TabPageAdministratin As System.Windows.Forms.TabPage
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TabPageReaction As System.Windows.Forms.TabPage
    Friend WithEvents grpReaction As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtAdverseEvent As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents chkPatientHasReaction As System.Windows.Forms.CheckBox
    Friend WithEvents btnBrowseReaction As System.Windows.Forms.Button
    Friend WithEvents lstReaction As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdo_PatientRecoveredNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdo_PatientRecoveredUnknown As System.Windows.Forms.RadioButton
    Friend WithEvents rdo_PatientRecoveredYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents dtPatientDied As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtHospitalizationDays As System.Windows.Forms.TextBox
    Friend WithEvents chk_NoneOfTheAbove As System.Windows.Forms.CheckBox
    Friend WithEvents chk_ResultedInPermDisability As System.Windows.Forms.CheckBox
    Friend WithEvents chk_RequiredEmergencyRoom As System.Windows.Forms.CheckBox
    Friend WithEvents chk_ResultedInProlongation As System.Windows.Forms.CheckBox
    Friend WithEvents chk_LifeThreateningIllness As System.Windows.Forms.CheckBox
    Friend WithEvents chk_RequiredHospitalization As System.Windows.Forms.CheckBox
    Friend WithEvents chkPatientDied As System.Windows.Forms.CheckBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents dtOnsetDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblNote As System.Windows.Forms.Label
    Friend WithEvents btnClearCPT As System.Windows.Forms.Button
    Friend WithEvents btnClearDiagnosis As System.Windows.Forms.Button
    Friend WithEvents txt_notes As System.Windows.Forms.TextBox
    Friend WithEvents txtNDCcode As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btnBrowsCPT As System.Windows.Forms.Button
    Friend WithEvents btnBrowsDiagnosis As System.Windows.Forms.Button
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents cmbFunding As System.Windows.Forms.ComboBox
    Friend WithEvents cmbIcd As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCpt As System.Windows.Forms.ComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents dtDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkSetReminder As System.Windows.Forms.CheckBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents lblRefusedBy As System.Windows.Forms.Label
    Friend WithEvents lblRefusalreason As System.Windows.Forms.Label
    Friend WithEvents cmbRefusalreason As System.Windows.Forms.ComboBox
    Friend WithEvents txt_refusal_comments As System.Windows.Forms.TextBox
    Friend WithEvents txt_refused_by As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txt_vis As System.Windows.Forms.TextBox
    Friend WithEvents dtpublication_date As System.Windows.Forms.DateTimePicker
    Friend WithEvents chk_vis_given As System.Windows.Forms.CheckBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents lblDosesGiven As System.Windows.Forms.Label
    Friend WithEvents txt_units As System.Windows.Forms.TextBox
    Friend WithEvents txt_amount_given As System.Windows.Forms.TextBox
    Friend WithEvents txt_dosage_given As System.Windows.Forms.TextBox
    Friend WithEvents cmbSite As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRoute As System.Windows.Forms.ComboBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblLotNumber As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbLotNumber As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSKU As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearchsku As System.Windows.Forms.Button
    Friend WithEvents dtexpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txt_TradeName As System.Windows.Forms.TextBox
    Friend WithEvents btnTradeName As System.Windows.Forms.Button
    Friend WithEvents pnlTradeNameControl As System.Windows.Forms.Panel
    Friend WithEvents pnlCvxControl As System.Windows.Forms.Panel
    Friend WithEvents pnlMvxControl As System.Windows.Forms.Panel
    Friend WithEvents txtCvx As System.Windows.Forms.TextBox
    Friend WithEvents btnCvx As System.Windows.Forms.Button
    Friend WithEvents txtMvx As System.Windows.Forms.TextBox
    Friend WithEvents btnMvx As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents btnScan As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents BtnAddTradeNameCategory As System.Windows.Forms.Button
    Friend WithEvents BtnAddManufacturerCategory As System.Windows.Forms.Button
    Friend WithEvents BtnAddVaccineCategory As System.Windows.Forms.Button
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents tblStrip As gloToolStrip.gloToolStrip
    Friend WithEvents tblbtn_PrintVis As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
End Class
