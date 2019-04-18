<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLab_TestMaster
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLab_TestMaster))
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblLOINCCode = New System.Windows.Forms.Label()
        Me.lblLoincId = New System.Windows.Forms.Label()
        Me.btnLOINCCode = New System.Windows.Forms.Button()
        Me.btnCPT = New System.Windows.Forms.Button()
        Me.BtnAddLOINCCode = New System.Windows.Forms.Button()
        Me.BtnAddCPTCode = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnPreferredLabDelete = New System.Windows.Forms.Button()
        Me.gbSingle = New System.Windows.Forms.GroupBox()
        Me.C1LabResult = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.chkOrderable = New System.Windows.Forms.CheckBox()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.gbProfile = New System.Windows.Forms.GroupBox()
        Me.cmbValueType = New System.Windows.Forms.ComboBox()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtResultName = New System.Windows.Forms.TextBox()
        Me.txtRefRange = New System.Windows.Forms.TextBox()
        Me.txtLoincId = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAlternateResultCode = New System.Windows.Forms.TextBox()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.txt_testLoinicCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbTemplate = New System.Windows.Forms.ComboBox()
        Me.lblTemplate = New System.Windows.Forms.Label()
        Me.lblCPTCode = New System.Windows.Forms.Label()
        Me.txtCPTCode = New System.Windows.Forms.TextBox()
        Me.optSingle = New System.Windows.Forms.RadioButton()
        Me.optProfile = New System.Windows.Forms.RadioButton()
        Me.chkOutboundTransistion = New System.Windows.Forms.CheckBox()
        Me.cmbStructuredLabResults = New System.Windows.Forms.ComboBox()
        Me.lblStructuredLabResults = New System.Windows.Forms.Label()
        Me.lblMUReportingcat = New System.Windows.Forms.Label()
        Me.cmbMUReportingCat = New System.Windows.Forms.ComboBox()
        Me.lblStorageTemperature = New System.Windows.Forms.Label()
        Me.cmbStorageTemp = New System.Windows.Forms.ComboBox()
        Me.lblCollectionContainer = New System.Windows.Forms.Label()
        Me.cmbCollContainer = New System.Windows.Forms.ComboBox()
        Me.lblSpecimen = New System.Windows.Forms.Label()
        Me.cmbSpecimen = New System.Windows.Forms.ComboBox()
        Me.lblAssociatedEMField = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.pnlLOINCControl = New System.Windows.Forms.Panel()
        Me.pnlotherctrl = New System.Windows.Forms.Panel()
        Me.lblDept = New System.Windows.Forms.Label()
        Me.lblTestHead = New System.Windows.Forms.Label()
        Me.cmbDept = New System.Windows.Forms.ComboBox()
        Me.cmbTestHead = New System.Windows.Forms.ComboBox()
        Me.lblPrecaution = New System.Windows.Forms.Label()
        Me.txtPrecaution = New System.Windows.Forms.TextBox()
        Me.lblInstruction = New System.Windows.Forms.Label()
        Me.txtInstruction = New System.Windows.Forms.TextBox()
        Me.pnlCPTCode = New System.Windows.Forms.Panel()
        Me.lblResultType = New System.Windows.Forms.Label()
        Me.cmbAssociatedEM = New System.Windows.Forms.ComboBox()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.gbPreferedLab = New System.Windows.Forms.GroupBox()
        Me.C1PreferedLab = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.miniToolStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.tlsp_TestMaster = New gloGlobal.gloToolStripIgnoreFocus()
        Me.gbSingle.SuspendLayout()
        CType(Me.C1LabResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbProfile.SuspendLayout()
        Me.pnlotherctrl.SuspendLayout()
        Me.pnlCPTCode.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.gbPreferedLab.SuspendLayout()
        CType(Me.C1PreferedLab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.tlsp_TestMaster.SuspendLayout()
        Me.SuspendLayout()
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.BackgroundImageLayout = C1.Win.C1SuperTooltip.BackgroundImageLayout.Tile
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.C1SuperTooltip1.ForeColor = System.Drawing.Color.Black
        '
        'lblLOINCCode
        '
        Me.lblLOINCCode.BackColor = System.Drawing.Color.Transparent
        Me.lblLOINCCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLOINCCode.Location = New System.Drawing.Point(50, 131)
        Me.lblLOINCCode.Name = "lblLOINCCode"
        Me.lblLOINCCode.Size = New System.Drawing.Size(123, 18)
        Me.lblLOINCCode.TabIndex = 6
        Me.lblLOINCCode.Text = "LOINC Order Code :"
        Me.lblLOINCCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.lblLOINCCode, "Logical Observations Identifiers Names and Codes")
        '
        'lblLoincId
        '
        Me.lblLoincId.AutoSize = True
        Me.lblLoincId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoincId.Location = New System.Drawing.Point(61, 142)
        Me.lblLoincId.Name = "lblLoincId"
        Me.lblLoincId.Size = New System.Drawing.Size(81, 14)
        Me.lblLoincId.TabIndex = 517
        Me.lblLoincId.Text = "LOINC Code :"
        Me.lblLoincId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.lblLoincId, "Logical Observations Identifiers Names and Codes")
        '
        'btnLOINCCode
        '
        Me.btnLOINCCode.BackColor = System.Drawing.Color.Transparent
        Me.btnLOINCCode.BackgroundImage = CType(resources.GetObject("btnLOINCCode.BackgroundImage"), System.Drawing.Image)
        Me.btnLOINCCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLOINCCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLOINCCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnLOINCCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLOINCCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLOINCCode.Image = CType(resources.GetObject("btnLOINCCode.Image"), System.Drawing.Image)
        Me.btnLOINCCode.Location = New System.Drawing.Point(563, 130)
        Me.btnLOINCCode.Name = "btnLOINCCode"
        Me.btnLOINCCode.Size = New System.Drawing.Size(24, 23)
        Me.btnLOINCCode.TabIndex = 5
        Me.btnLOINCCode.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnLOINCCode, "Select LOINC Order Code")
        Me.btnLOINCCode.UseVisualStyleBackColor = False
        '
        'btnCPT
        '
        Me.btnCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnCPT.BackgroundImage = CType(resources.GetObject("btnCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCPT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCPT.Image = CType(resources.GetObject("btnCPT.Image"), System.Drawing.Image)
        Me.btnCPT.Location = New System.Drawing.Point(563, 183)
        Me.btnCPT.Name = "btnCPT"
        Me.btnCPT.Size = New System.Drawing.Size(24, 23)
        Me.btnCPT.TabIndex = 9
        Me.btnCPT.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnCPT, "Select CPT")
        Me.btnCPT.UseVisualStyleBackColor = False
        '
        'BtnAddLOINCCode
        '
        Me.BtnAddLOINCCode.BackColor = System.Drawing.Color.Transparent
        Me.BtnAddLOINCCode.BackgroundImage = CType(resources.GetObject("BtnAddLOINCCode.BackgroundImage"), System.Drawing.Image)
        Me.BtnAddLOINCCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnAddLOINCCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAddLOINCCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnAddLOINCCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddLOINCCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddLOINCCode.Image = CType(resources.GetObject("BtnAddLOINCCode.Image"), System.Drawing.Image)
        Me.BtnAddLOINCCode.Location = New System.Drawing.Point(592, 130)
        Me.BtnAddLOINCCode.Name = "BtnAddLOINCCode"
        Me.BtnAddLOINCCode.Size = New System.Drawing.Size(24, 23)
        Me.BtnAddLOINCCode.TabIndex = 6
        Me.BtnAddLOINCCode.TabStop = False
        Me.BtnAddLOINCCode.Text = "          "
        Me.ToolTip1.SetToolTip(Me.BtnAddLOINCCode, "Add LOINC Order Code")
        Me.BtnAddLOINCCode.UseVisualStyleBackColor = False
        '
        'BtnAddCPTCode
        '
        Me.BtnAddCPTCode.BackColor = System.Drawing.Color.Transparent
        Me.BtnAddCPTCode.BackgroundImage = CType(resources.GetObject("BtnAddCPTCode.BackgroundImage"), System.Drawing.Image)
        Me.BtnAddCPTCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnAddCPTCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAddCPTCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnAddCPTCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddCPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddCPTCode.Image = CType(resources.GetObject("BtnAddCPTCode.Image"), System.Drawing.Image)
        Me.BtnAddCPTCode.Location = New System.Drawing.Point(63, 180)
        Me.BtnAddCPTCode.Name = "BtnAddCPTCode"
        Me.BtnAddCPTCode.Size = New System.Drawing.Size(185, 23)
        Me.BtnAddCPTCode.TabIndex = 329
        Me.BtnAddCPTCode.TabStop = False
        Me.BtnAddCPTCode.Text = "          "
        Me.ToolTip1.SetToolTip(Me.BtnAddCPTCode, "Add CPT")
        Me.BtnAddCPTCode.UseVisualStyleBackColor = False
        Me.BtnAddCPTCode.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(47, 170)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 14)
        Me.Label7.TabIndex = 521
        Me.Label7.Text = "Lab Test Code :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.Label7, "Logical Observations Identifiers Names and Codes")
        '
        'btnPreferredLabDelete
        '
        Me.btnPreferredLabDelete.BackColor = System.Drawing.Color.Transparent
        Me.btnPreferredLabDelete.BackgroundImage = CType(resources.GetObject("btnPreferredLabDelete.BackgroundImage"), System.Drawing.Image)
        Me.btnPreferredLabDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPreferredLabDelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPreferredLabDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPreferredLabDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreferredLabDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreferredLabDelete.Image = CType(resources.GetObject("btnPreferredLabDelete.Image"), System.Drawing.Image)
        Me.btnPreferredLabDelete.Location = New System.Drawing.Point(744, 602)
        Me.btnPreferredLabDelete.Name = "btnPreferredLabDelete"
        Me.btnPreferredLabDelete.Size = New System.Drawing.Size(23, 23)
        Me.btnPreferredLabDelete.TabIndex = 518
        Me.btnPreferredLabDelete.TabStop = False
        Me.btnPreferredLabDelete.Text = "          "
        Me.ToolTip1.SetToolTip(Me.btnPreferredLabDelete, "Delete Selected Preferred Lab")
        Me.btnPreferredLabDelete.UseVisualStyleBackColor = False
        '
        'gbSingle
        '
        Me.gbSingle.BackColor = System.Drawing.Color.Transparent
        Me.gbSingle.Controls.Add(Me.C1LabResult)
        Me.gbSingle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSingle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gbSingle.Location = New System.Drawing.Point(30, 256)
        Me.gbSingle.Name = "gbSingle"
        Me.gbSingle.Size = New System.Drawing.Size(705, 178)
        Me.gbSingle.TabIndex = 15
        Me.gbSingle.TabStop = False
        Me.gbSingle.Text = "Result Details"
        Me.gbSingle.Visible = False
        '
        'C1LabResult
        '
        Me.C1LabResult.AllowAddNew = True
        Me.C1LabResult.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1LabResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1LabResult.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1LabResult.ColumnInfo = "10,1,0,0,0,95,Columns:1{AllowDragging:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1LabResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1LabResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1LabResult.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1LabResult.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1LabResult.Location = New System.Drawing.Point(3, 18)
        Me.C1LabResult.Name = "C1LabResult"
        Me.C1LabResult.Rows.DefaultSize = 19
        Me.C1LabResult.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1LabResult.Size = New System.Drawing.Size(699, 157)
        Me.C1LabResult.StyleInfo = resources.GetString("C1LabResult.StyleInfo")
        Me.C1LabResult.TabIndex = 0
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(130, 26)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(43, 14)
        Me.lblCode.TabIndex = 1
        Me.lblCode.Text = "Code :"
        Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(127, 53)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(46, 14)
        Me.lblName.TabIndex = 2
        Me.lblName.Text = "Name :"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCode
        '
        Me.txtCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.Color.Black
        Me.txtCode.Location = New System.Drawing.Point(176, 22)
        Me.txtCode.MaxLength = 60
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(380, 22)
        Me.txtCode.TabIndex = 0
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(176, 49)
        Me.txtName.MaxLength = 255
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(380, 22)
        Me.txtName.TabIndex = 1
        '
        'chkOrderable
        '
        Me.chkOrderable.AutoSize = True
        Me.chkOrderable.BackColor = System.Drawing.Color.Transparent
        Me.chkOrderable.Checked = True
        Me.chkOrderable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOrderable.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOrderable.Location = New System.Drawing.Point(7, 8)
        Me.chkOrderable.Name = "chkOrderable"
        Me.chkOrderable.Size = New System.Drawing.Size(89, 18)
        Me.chkOrderable.TabIndex = 3
        Me.chkOrderable.Text = "Orderable ?"
        Me.chkOrderable.UseVisualStyleBackColor = False
        Me.chkOrderable.Visible = False
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(813, 1)
        Me.lbl_TopBrd.TabIndex = 15
        Me.lbl_TopBrd.Text = "label1"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(815, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 795)
        Me.lbl_RightBrd.TabIndex = 16
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 795)
        Me.lbl_LeftBrd.TabIndex = 17
        Me.lbl_LeftBrd.Text = "label4"
        '
        'gbProfile
        '
        Me.gbProfile.BackColor = System.Drawing.Color.Transparent
        Me.gbProfile.Controls.Add(Me.Label7)
        Me.gbProfile.Controls.Add(Me.cmbValueType)
        Me.gbProfile.Controls.Add(Me.txtComment)
        Me.gbProfile.Controls.Add(Me.Label3)
        Me.gbProfile.Controls.Add(Me.Label2)
        Me.gbProfile.Controls.Add(Me.Label1)
        Me.gbProfile.Controls.Add(Me.lblLoincId)
        Me.gbProfile.Controls.Add(Me.txtResultName)
        Me.gbProfile.Controls.Add(Me.txtRefRange)
        Me.gbProfile.Controls.Add(Me.txtLoincId)
        Me.gbProfile.Controls.Add(Me.Label4)
        Me.gbProfile.Controls.Add(Me.txtAlternateResultCode)
        Me.gbProfile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbProfile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gbProfile.Location = New System.Drawing.Point(30, 256)
        Me.gbProfile.Name = "gbProfile"
        Me.gbProfile.Size = New System.Drawing.Size(644, 196)
        Me.gbProfile.TabIndex = 15
        Me.gbProfile.TabStop = False
        Me.gbProfile.Text = "Result Details"
        '
        'cmbValueType
        '
        Me.cmbValueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbValueType.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbValueType.ForeColor = System.Drawing.Color.Black
        Me.cmbValueType.FormattingEnabled = True
        Me.cmbValueType.Location = New System.Drawing.Point(145, 52)
        Me.cmbValueType.Name = "cmbValueType"
        Me.cmbValueType.Size = New System.Drawing.Size(203, 22)
        Me.cmbValueType.TabIndex = 14
        '
        'txtComment
        '
        Me.txtComment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.ForeColor = System.Drawing.Color.Black
        Me.txtComment.Location = New System.Drawing.Point(145, 110)
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(209, 22)
        Me.txtComment.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(36, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 14)
        Me.Label3.TabIndex = 502
        Me.Label3.Text = "Reference range :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(60, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 14)
        Me.Label2.TabIndex = 510
        Me.Label2.Text = "Result name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(68, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 14)
        Me.Label1.TabIndex = 512
        Me.Label1.Text = "Value type :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtResultName
        '
        Me.txtResultName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResultName.ForeColor = System.Drawing.Color.Black
        Me.txtResultName.Location = New System.Drawing.Point(145, 23)
        Me.txtResultName.Name = "txtResultName"
        Me.txtResultName.Size = New System.Drawing.Size(121, 22)
        Me.txtResultName.TabIndex = 13
        Me.txtResultName.Text = "None"
        '
        'txtRefRange
        '
        Me.txtRefRange.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefRange.ForeColor = System.Drawing.Color.Black
        Me.txtRefRange.Location = New System.Drawing.Point(145, 81)
        Me.txtRefRange.Name = "txtRefRange"
        Me.txtRefRange.Size = New System.Drawing.Size(204, 22)
        Me.txtRefRange.TabIndex = 15
        '
        'txtLoincId
        '
        Me.txtLoincId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoincId.ForeColor = System.Drawing.Color.Black
        Me.txtLoincId.Location = New System.Drawing.Point(145, 167)
        Me.txtLoincId.MaxLength = 50
        Me.txtLoincId.Name = "txtLoincId"
        Me.txtLoincId.Size = New System.Drawing.Size(209, 22)
        Me.txtLoincId.TabIndex = 18
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(74, 114)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 14)
        Me.Label4.TabIndex = 519
        Me.Label4.Text = "Comment :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAlternateResultCode
        '
        Me.txtAlternateResultCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAlternateResultCode.ForeColor = System.Drawing.Color.Black
        Me.txtAlternateResultCode.Location = New System.Drawing.Point(145, 139)
        Me.txtAlternateResultCode.MaxLength = 50
        Me.txtAlternateResultCode.Name = "txtAlternateResultCode"
        Me.txtAlternateResultCode.Size = New System.Drawing.Size(209, 22)
        Me.txtAlternateResultCode.TabIndex = 17
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 798)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(811, 1)
        Me.lbl_BottomBrd.TabIndex = 18
        Me.lbl_BottomBrd.Text = "label2"
        '
        'txt_testLoinicCode
        '
        Me.txt_testLoinicCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_testLoinicCode.ForeColor = System.Drawing.Color.Black
        Me.txt_testLoinicCode.Location = New System.Drawing.Point(176, 130)
        Me.txt_testLoinicCode.Name = "txt_testLoinicCode"
        Me.txt_testLoinicCode.Size = New System.Drawing.Size(380, 22)
        Me.txt_testLoinicCode.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(119, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 14)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "*"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(114, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(14, 14)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "*"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbTemplate
        '
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTemplate.ForeColor = System.Drawing.Color.Black
        Me.cmbTemplate.FormattingEnabled = True
        Me.cmbTemplate.Location = New System.Drawing.Point(176, 103)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(380, 22)
        Me.cmbTemplate.TabIndex = 3
        '
        'lblTemplate
        '
        Me.lblTemplate.AutoSize = True
        Me.lblTemplate.BackColor = System.Drawing.Color.Transparent
        Me.lblTemplate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTemplate.Location = New System.Drawing.Point(106, 107)
        Me.lblTemplate.Name = "lblTemplate"
        Me.lblTemplate.Size = New System.Drawing.Size(67, 14)
        Me.lblTemplate.TabIndex = 27
        Me.lblTemplate.Text = "Template :"
        Me.lblTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCPTCode
        '
        Me.lblCPTCode.AutoSize = True
        Me.lblCPTCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCPTCode.Location = New System.Drawing.Point(104, 186)
        Me.lblCPTCode.Name = "lblCPTCode"
        Me.lblCPTCode.Size = New System.Drawing.Size(69, 14)
        Me.lblCPTCode.TabIndex = 28
        Me.lblCPTCode.Text = "CPT Code :"
        Me.lblCPTCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCPTCode
        '
        Me.txtCPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCPTCode.ForeColor = System.Drawing.Color.Black
        Me.txtCPTCode.Location = New System.Drawing.Point(176, 184)
        Me.txtCPTCode.Name = "txtCPTCode"
        Me.txtCPTCode.Size = New System.Drawing.Size(380, 22)
        Me.txtCPTCode.TabIndex = 8
        '
        'optSingle
        '
        Me.optSingle.AutoSize = True
        Me.optSingle.BackColor = System.Drawing.Color.Transparent
        Me.optSingle.Checked = True
        Me.optSingle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSingle.Location = New System.Drawing.Point(176, 234)
        Me.optSingle.Name = "optSingle"
        Me.optSingle.Size = New System.Drawing.Size(62, 18)
        Me.optSingle.TabIndex = 11
        Me.optSingle.TabStop = True
        Me.optSingle.Text = "Single"
        Me.optSingle.UseVisualStyleBackColor = False
        '
        'optProfile
        '
        Me.optProfile.AutoSize = True
        Me.optProfile.BackColor = System.Drawing.Color.Transparent
        Me.optProfile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optProfile.Location = New System.Drawing.Point(244, 236)
        Me.optProfile.Name = "optProfile"
        Me.optProfile.Size = New System.Drawing.Size(58, 18)
        Me.optProfile.TabIndex = 12
        Me.optProfile.TabStop = True
        Me.optProfile.Text = "Profile"
        Me.optProfile.UseVisualStyleBackColor = False
        '
        'chkOutboundTransistion
        '
        Me.chkOutboundTransistion.AutoSize = True
        Me.chkOutboundTransistion.BackColor = System.Drawing.Color.Transparent
        Me.chkOutboundTransistion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOutboundTransistion.Location = New System.Drawing.Point(176, 211)
        Me.chkOutboundTransistion.Name = "chkOutboundTransistion"
        Me.chkOutboundTransistion.Size = New System.Drawing.Size(182, 18)
        Me.chkOutboundTransistion.TabIndex = 10
        Me.chkOutboundTransistion.Text = "Outbound Transition of Care"
        Me.chkOutboundTransistion.UseVisualStyleBackColor = False
        '
        'cmbStructuredLabResults
        '
        Me.cmbStructuredLabResults.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStructuredLabResults.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbStructuredLabResults.ForeColor = System.Drawing.Color.Black
        Me.cmbStructuredLabResults.FormattingEnabled = True
        Me.cmbStructuredLabResults.Items.AddRange(New Object() {"Yes", "No"})
        Me.cmbStructuredLabResults.Location = New System.Drawing.Point(176, 157)
        Me.cmbStructuredLabResults.Name = "cmbStructuredLabResults"
        Me.cmbStructuredLabResults.Size = New System.Drawing.Size(380, 22)
        Me.cmbStructuredLabResults.TabIndex = 7
        '
        'lblStructuredLabResults
        '
        Me.lblStructuredLabResults.AutoSize = True
        Me.lblStructuredLabResults.BackColor = System.Drawing.Color.Transparent
        Me.lblStructuredLabResults.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStructuredLabResults.Location = New System.Drawing.Point(34, 159)
        Me.lblStructuredLabResults.Name = "lblStructuredLabResults"
        Me.lblStructuredLabResults.Size = New System.Drawing.Size(139, 14)
        Me.lblStructuredLabResults.TabIndex = 330
        Me.lblStructuredLabResults.Text = "Structured Lab Results :"
        Me.lblStructuredLabResults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMUReportingcat
        '
        Me.lblMUReportingcat.AutoSize = True
        Me.lblMUReportingcat.BackColor = System.Drawing.Color.Transparent
        Me.lblMUReportingcat.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMUReportingcat.Location = New System.Drawing.Point(95, 79)
        Me.lblMUReportingcat.Name = "lblMUReportingcat"
        Me.lblMUReportingcat.Size = New System.Drawing.Size(78, 14)
        Me.lblMUReportingcat.TabIndex = 26
        Me.lblMUReportingcat.Text = "Order Type :"
        Me.lblMUReportingcat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbMUReportingCat
        '
        Me.cmbMUReportingCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMUReportingCat.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMUReportingCat.ForeColor = System.Drawing.Color.Black
        Me.cmbMUReportingCat.FormattingEnabled = True
        Me.cmbMUReportingCat.Location = New System.Drawing.Point(176, 76)
        Me.cmbMUReportingCat.Name = "cmbMUReportingCat"
        Me.cmbMUReportingCat.Size = New System.Drawing.Size(380, 22)
        Me.cmbMUReportingCat.TabIndex = 2
        '
        'lblStorageTemperature
        '
        Me.lblStorageTemperature.AutoSize = True
        Me.lblStorageTemperature.BackColor = System.Drawing.Color.Transparent
        Me.lblStorageTemperature.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStorageTemperature.Location = New System.Drawing.Point(38, 543)
        Me.lblStorageTemperature.Name = "lblStorageTemperature"
        Me.lblStorageTemperature.Size = New System.Drawing.Size(131, 14)
        Me.lblStorageTemperature.TabIndex = 503
        Me.lblStorageTemperature.Text = "Storage temperature :"
        Me.lblStorageTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbStorageTemp
        '
        Me.cmbStorageTemp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStorageTemp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbStorageTemp.ForeColor = System.Drawing.Color.Black
        Me.cmbStorageTemp.FormattingEnabled = True
        Me.cmbStorageTemp.Location = New System.Drawing.Point(173, 541)
        Me.cmbStorageTemp.Name = "cmbStorageTemp"
        Me.cmbStorageTemp.Size = New System.Drawing.Size(254, 22)
        Me.cmbStorageTemp.TabIndex = 22
        '
        'lblCollectionContainer
        '
        Me.lblCollectionContainer.AutoSize = True
        Me.lblCollectionContainer.BackColor = System.Drawing.Color.Transparent
        Me.lblCollectionContainer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCollectionContainer.Location = New System.Drawing.Point(47, 517)
        Me.lblCollectionContainer.Name = "lblCollectionContainer"
        Me.lblCollectionContainer.Size = New System.Drawing.Size(122, 14)
        Me.lblCollectionContainer.TabIndex = 507
        Me.lblCollectionContainer.Text = "Collection container :"
        Me.lblCollectionContainer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbCollContainer
        '
        Me.cmbCollContainer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCollContainer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCollContainer.ForeColor = System.Drawing.Color.Black
        Me.cmbCollContainer.FormattingEnabled = True
        Me.cmbCollContainer.Location = New System.Drawing.Point(173, 514)
        Me.cmbCollContainer.Name = "cmbCollContainer"
        Me.cmbCollContainer.Size = New System.Drawing.Size(254, 22)
        Me.cmbCollContainer.TabIndex = 21
        '
        'lblSpecimen
        '
        Me.lblSpecimen.AutoSize = True
        Me.lblSpecimen.BackColor = System.Drawing.Color.Transparent
        Me.lblSpecimen.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpecimen.Location = New System.Drawing.Point(101, 489)
        Me.lblSpecimen.Name = "lblSpecimen"
        Me.lblSpecimen.Size = New System.Drawing.Size(68, 14)
        Me.lblSpecimen.TabIndex = 510
        Me.lblSpecimen.Text = "Specimen :"
        Me.lblSpecimen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSpecimen
        '
        Me.cmbSpecimen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpecimen.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSpecimen.ForeColor = System.Drawing.Color.Black
        Me.cmbSpecimen.FormattingEnabled = True
        Me.cmbSpecimen.Location = New System.Drawing.Point(173, 487)
        Me.cmbSpecimen.Name = "cmbSpecimen"
        Me.cmbSpecimen.Size = New System.Drawing.Size(253, 22)
        Me.cmbSpecimen.TabIndex = 20
        '
        'lblAssociatedEMField
        '
        Me.lblAssociatedEMField.AutoSize = True
        Me.lblAssociatedEMField.BackColor = System.Drawing.Color.Transparent
        Me.lblAssociatedEMField.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssociatedEMField.Location = New System.Drawing.Point(44, 462)
        Me.lblAssociatedEMField.Name = "lblAssociatedEMField"
        Me.lblAssociatedEMField.Size = New System.Drawing.Size(125, 14)
        Me.lblAssociatedEMField.TabIndex = 511
        Me.lblAssociatedEMField.Text = "Associate E&&M fields :"
        Me.lblAssociatedEMField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnBrowse
        '
        Me.btnBrowse.BackgroundImage = CType(resources.GetObject("btnBrowse.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBrowse.Location = New System.Drawing.Point(173, 458)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(34, 24)
        Me.btnBrowse.TabIndex = 19
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'pnlLOINCControl
        '
        Me.pnlLOINCControl.Location = New System.Drawing.Point(176, 152)
        Me.pnlLOINCControl.Name = "pnlLOINCControl"
        Me.pnlLOINCControl.Size = New System.Drawing.Size(380, 174)
        Me.pnlLOINCControl.TabIndex = 324
        Me.pnlLOINCControl.Visible = False
        '
        'pnlotherctrl
        '
        Me.pnlotherctrl.Controls.Add(Me.lblDept)
        Me.pnlotherctrl.Controls.Add(Me.lblTestHead)
        Me.pnlotherctrl.Controls.Add(Me.cmbDept)
        Me.pnlotherctrl.Controls.Add(Me.cmbTestHead)
        Me.pnlotherctrl.Controls.Add(Me.lblPrecaution)
        Me.pnlotherctrl.Controls.Add(Me.txtPrecaution)
        Me.pnlotherctrl.Controls.Add(Me.BtnAddCPTCode)
        Me.pnlotherctrl.Controls.Add(Me.lblInstruction)
        Me.pnlotherctrl.Controls.Add(Me.txtInstruction)
        Me.pnlotherctrl.Location = New System.Drawing.Point(609, 223)
        Me.pnlotherctrl.Name = "pnlotherctrl"
        Me.pnlotherctrl.Size = New System.Drawing.Size(49, 27)
        Me.pnlotherctrl.TabIndex = 516
        Me.pnlotherctrl.Visible = False
        '
        'lblDept
        '
        Me.lblDept.BackColor = System.Drawing.Color.Transparent
        Me.lblDept.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.Location = New System.Drawing.Point(30, 146)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(80, 10)
        Me.lblDept.TabIndex = 9
        Me.lblDept.Text = "Department/Category :"
        Me.lblDept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDept.Visible = False
        '
        'lblTestHead
        '
        Me.lblTestHead.AutoSize = True
        Me.lblTestHead.BackColor = System.Drawing.Color.Transparent
        Me.lblTestHead.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestHead.Location = New System.Drawing.Point(100, 190)
        Me.lblTestHead.Name = "lblTestHead"
        Me.lblTestHead.Size = New System.Drawing.Size(71, 14)
        Me.lblTestHead.TabIndex = 6
        Me.lblTestHead.Text = "Test head :"
        Me.lblTestHead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTestHead.Visible = False
        '
        'cmbDept
        '
        Me.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDept.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDept.ForeColor = System.Drawing.Color.Black
        Me.cmbDept.FormattingEnabled = True
        Me.cmbDept.Location = New System.Drawing.Point(106, 144)
        Me.cmbDept.Name = "cmbDept"
        Me.cmbDept.Size = New System.Drawing.Size(186, 22)
        Me.cmbDept.TabIndex = 10
        Me.cmbDept.Visible = False
        '
        'cmbTestHead
        '
        Me.cmbTestHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTestHead.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTestHead.ForeColor = System.Drawing.Color.Black
        Me.cmbTestHead.FormattingEnabled = True
        Me.cmbTestHead.Location = New System.Drawing.Point(177, 187)
        Me.cmbTestHead.Name = "cmbTestHead"
        Me.cmbTestHead.Size = New System.Drawing.Size(245, 22)
        Me.cmbTestHead.TabIndex = 10
        Me.cmbTestHead.TabStop = False
        Me.cmbTestHead.Visible = False
        '
        'lblPrecaution
        '
        Me.lblPrecaution.AutoSize = True
        Me.lblPrecaution.BackColor = System.Drawing.Color.Transparent
        Me.lblPrecaution.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrecaution.Location = New System.Drawing.Point(21, 86)
        Me.lblPrecaution.Name = "lblPrecaution"
        Me.lblPrecaution.Size = New System.Drawing.Size(73, 14)
        Me.lblPrecaution.TabIndex = 8
        Me.lblPrecaution.Text = "Precaution :"
        Me.lblPrecaution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPrecaution.Visible = False
        '
        'txtPrecaution
        '
        Me.txtPrecaution.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrecaution.ForeColor = System.Drawing.Color.Black
        Me.txtPrecaution.Location = New System.Drawing.Point(97, 83)
        Me.txtPrecaution.Multiline = True
        Me.txtPrecaution.Name = "txtPrecaution"
        Me.txtPrecaution.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPrecaution.Size = New System.Drawing.Size(186, 49)
        Me.txtPrecaution.TabIndex = 7
        Me.txtPrecaution.Visible = False
        '
        'lblInstruction
        '
        Me.lblInstruction.AutoSize = True
        Me.lblInstruction.BackColor = System.Drawing.Color.Transparent
        Me.lblInstruction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstruction.Location = New System.Drawing.Point(21, 24)
        Me.lblInstruction.Name = "lblInstruction"
        Me.lblInstruction.Size = New System.Drawing.Size(74, 14)
        Me.lblInstruction.TabIndex = 3
        Me.lblInstruction.Text = "Instruction :"
        Me.lblInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblInstruction.Visible = False
        '
        'txtInstruction
        '
        Me.txtInstruction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstruction.ForeColor = System.Drawing.Color.Black
        Me.txtInstruction.Location = New System.Drawing.Point(98, 21)
        Me.txtInstruction.Multiline = True
        Me.txtInstruction.Name = "txtInstruction"
        Me.txtInstruction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInstruction.Size = New System.Drawing.Size(186, 49)
        Me.txtInstruction.TabIndex = 2
        Me.txtInstruction.Visible = False
        '
        'pnlCPTCode
        '
        Me.pnlCPTCode.Controls.Add(Me.lblResultType)
        Me.pnlCPTCode.Controls.Add(Me.cmbAssociatedEM)
        Me.pnlCPTCode.Location = New System.Drawing.Point(175, 205)
        Me.pnlCPTCode.Name = "pnlCPTCode"
        Me.pnlCPTCode.Size = New System.Drawing.Size(380, 138)
        Me.pnlCPTCode.TabIndex = 323
        Me.pnlCPTCode.Visible = False
        '
        'lblResultType
        '
        Me.lblResultType.AutoSize = True
        Me.lblResultType.BackColor = System.Drawing.Color.Transparent
        Me.lblResultType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResultType.Location = New System.Drawing.Point(3, 197)
        Me.lblResultType.Name = "lblResultType"
        Me.lblResultType.Size = New System.Drawing.Size(84, 14)
        Me.lblResultType.TabIndex = 6
        Me.lblResultType.Text = "Result Type  :"
        Me.lblResultType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbAssociatedEM
        '
        Me.cmbAssociatedEM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAssociatedEM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAssociatedEM.ForeColor = System.Drawing.Color.Black
        Me.cmbAssociatedEM.FormattingEnabled = True
        Me.cmbAssociatedEM.Items.AddRange(New Object() {"Select EM Field"})
        Me.cmbAssociatedEM.Location = New System.Drawing.Point(6, 253)
        Me.cmbAssociatedEM.Name = "cmbAssociatedEM"
        Me.cmbAssociatedEM.Size = New System.Drawing.Size(221, 22)
        Me.cmbAssociatedEM.TabIndex = 2
        Me.cmbAssociatedEM.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.btnPreferredLabDelete)
        Me.pnlMain.Controls.Add(Me.gbPreferedLab)
        Me.pnlMain.Controls.Add(Me.pnlCPTCode)
        Me.pnlMain.Controls.Add(Me.pnlotherctrl)
        Me.pnlMain.Controls.Add(Me.pnlLOINCControl)
        Me.pnlMain.Controls.Add(Me.btnBrowse)
        Me.pnlMain.Controls.Add(Me.lblAssociatedEMField)
        Me.pnlMain.Controls.Add(Me.cmbSpecimen)
        Me.pnlMain.Controls.Add(Me.lblSpecimen)
        Me.pnlMain.Controls.Add(Me.cmbCollContainer)
        Me.pnlMain.Controls.Add(Me.lblCollectionContainer)
        Me.pnlMain.Controls.Add(Me.cmbStorageTemp)
        Me.pnlMain.Controls.Add(Me.lblStorageTemperature)
        Me.pnlMain.Controls.Add(Me.cmbMUReportingCat)
        Me.pnlMain.Controls.Add(Me.lblMUReportingcat)
        Me.pnlMain.Controls.Add(Me.lblStructuredLabResults)
        Me.pnlMain.Controls.Add(Me.cmbStructuredLabResults)
        Me.pnlMain.Controls.Add(Me.chkOutboundTransistion)
        Me.pnlMain.Controls.Add(Me.optProfile)
        Me.pnlMain.Controls.Add(Me.optSingle)
        Me.pnlMain.Controls.Add(Me.BtnAddLOINCCode)
        Me.pnlMain.Controls.Add(Me.btnCPT)
        Me.pnlMain.Controls.Add(Me.btnLOINCCode)
        Me.pnlMain.Controls.Add(Me.txtCPTCode)
        Me.pnlMain.Controls.Add(Me.lblCPTCode)
        Me.pnlMain.Controls.Add(Me.lblTemplate)
        Me.pnlMain.Controls.Add(Me.cmbTemplate)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.txt_testLoinicCode)
        Me.pnlMain.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlMain.Controls.Add(Me.gbProfile)
        Me.pnlMain.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlMain.Controls.Add(Me.lbl_RightBrd)
        Me.pnlMain.Controls.Add(Me.lbl_TopBrd)
        Me.pnlMain.Controls.Add(Me.chkOrderable)
        Me.pnlMain.Controls.Add(Me.lblLOINCCode)
        Me.pnlMain.Controls.Add(Me.txtName)
        Me.pnlMain.Controls.Add(Me.txtCode)
        Me.pnlMain.Controls.Add(Me.lblName)
        Me.pnlMain.Controls.Add(Me.lblCode)
        Me.pnlMain.Controls.Add(Me.gbSingle)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(819, 802)
        Me.pnlMain.TabIndex = 0
        '
        'gbPreferedLab
        '
        Me.gbPreferedLab.BackColor = System.Drawing.Color.Transparent
        Me.gbPreferedLab.Controls.Add(Me.C1PreferedLab)
        Me.gbPreferedLab.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbPreferedLab.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gbPreferedLab.Location = New System.Drawing.Point(33, 595)
        Me.gbPreferedLab.Name = "gbPreferedLab"
        Me.gbPreferedLab.Size = New System.Drawing.Size(705, 178)
        Me.gbPreferedLab.TabIndex = 517
        Me.gbPreferedLab.TabStop = False
        Me.gbPreferedLab.Text = "Preferred Lab"
        '
        'C1PreferedLab
        '
        Me.C1PreferedLab.AllowAddNew = True
        Me.C1PreferedLab.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1PreferedLab.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1PreferedLab.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1PreferedLab.ColumnInfo = "10,1,0,0,0,95,Columns:1{AllowDragging:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1PreferedLab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1PreferedLab.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PreferedLab.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1PreferedLab.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1PreferedLab.Location = New System.Drawing.Point(3, 18)
        Me.C1PreferedLab.Name = "C1PreferedLab"
        Me.C1PreferedLab.Rows.DefaultSize = 19
        Me.C1PreferedLab.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PreferedLab.Size = New System.Drawing.Size(699, 157)
        Me.C1PreferedLab.StyleInfo = resources.GetString("C1PreferedLab.StyleInfo")
        Me.C1PreferedLab.TabIndex = 0
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.BackColor = System.Drawing.Color.Transparent
        Me.miniToolStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.miniToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.miniToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.miniToolStrip.Location = New System.Drawing.Point(109, 0)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(110, 21)
        Me.miniToolStrip.TabIndex = 0
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 18)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 18)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.ToolStrip1)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(819, 53)
        Me.pnl_tlsp.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(819, 53)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "toolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(66, 50)
        Me.ToolStripButton1.Tag = "Save"
        Me.ToolStripButton1.Text = "&Save&&Cls"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton1.ToolTipText = "Save and Close"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(43, 50)
        Me.ToolStripButton2.Tag = "Close"
        Me.ToolStripButton2.Text = "&Close"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsp_TestMaster
        '
        Me.tlsp_TestMaster.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_TestMaster.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_TestMaster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_TestMaster.Dock = System.Windows.Forms.DockStyle.None
        Me.tlsp_TestMaster.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_TestMaster.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_TestMaster.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_TestMaster.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_TestMaster.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_TestMaster.Name = "tlsp_TestMaster"
        Me.tlsp_TestMaster.Size = New System.Drawing.Size(110, 21)
        Me.tlsp_TestMaster.TabIndex = 0
        Me.tlsp_TestMaster.Text = "toolStrip1"
        '
        'frmLab_TestMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(819, 855)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLab_TestMaster"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test Master"
        Me.gbSingle.ResumeLayout(False)
        CType(Me.C1LabResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbProfile.ResumeLayout(False)
        Me.gbProfile.PerformLayout()
        Me.pnlotherctrl.ResumeLayout(False)
        Me.pnlotherctrl.PerformLayout()
        Me.pnlCPTCode.ResumeLayout(False)
        Me.pnlCPTCode.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.gbPreferedLab.ResumeLayout(False)
        CType(Me.C1PreferedLab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.tlsp_TestMaster.ResumeLayout(False)
        Me.tlsp_TestMaster.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents gbSingle As System.Windows.Forms.GroupBox
    Friend WithEvents C1LabResult As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblLOINCCode As System.Windows.Forms.Label
    Friend WithEvents chkOrderable As System.Windows.Forms.CheckBox
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Friend WithEvents gbProfile As System.Windows.Forms.GroupBox
    Friend WithEvents cmbValueType As System.Windows.Forms.ComboBox
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblLoincId As System.Windows.Forms.Label
    Friend WithEvents txtResultName As System.Windows.Forms.TextBox
    Friend WithEvents txtRefRange As System.Windows.Forms.TextBox
    Friend WithEvents txtLoincId As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Friend WithEvents txt_testLoinicCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents lblTemplate As System.Windows.Forms.Label
    Friend WithEvents lblCPTCode As System.Windows.Forms.Label
    Friend WithEvents txtCPTCode As System.Windows.Forms.TextBox
    Friend WithEvents btnLOINCCode As System.Windows.Forms.Button
    Friend WithEvents btnCPT As System.Windows.Forms.Button
    Friend WithEvents BtnAddLOINCCode As System.Windows.Forms.Button
    Friend WithEvents optSingle As System.Windows.Forms.RadioButton
    Friend WithEvents optProfile As System.Windows.Forms.RadioButton
    Friend WithEvents chkOutboundTransistion As System.Windows.Forms.CheckBox
    Friend WithEvents cmbStructuredLabResults As System.Windows.Forms.ComboBox
    Friend WithEvents lblStructuredLabResults As System.Windows.Forms.Label
    Friend WithEvents lblMUReportingcat As System.Windows.Forms.Label
    Friend WithEvents cmbMUReportingCat As System.Windows.Forms.ComboBox
    Friend WithEvents lblStorageTemperature As System.Windows.Forms.Label
    Friend WithEvents cmbStorageTemp As System.Windows.Forms.ComboBox
    Friend WithEvents lblCollectionContainer As System.Windows.Forms.Label
    Friend WithEvents cmbCollContainer As System.Windows.Forms.ComboBox
    Friend WithEvents lblSpecimen As System.Windows.Forms.Label
    Friend WithEvents cmbSpecimen As System.Windows.Forms.ComboBox
    Friend WithEvents lblAssociatedEMField As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents pnlLOINCControl As System.Windows.Forms.Panel
    Friend WithEvents pnlotherctrl As System.Windows.Forms.Panel
    Friend WithEvents lblDept As System.Windows.Forms.Label
    Friend WithEvents lblTestHead As System.Windows.Forms.Label
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTestHead As System.Windows.Forms.ComboBox
    Friend WithEvents lblPrecaution As System.Windows.Forms.Label
    Friend WithEvents txtPrecaution As System.Windows.Forms.TextBox
    Friend WithEvents BtnAddCPTCode As System.Windows.Forms.Button
    Friend WithEvents lblInstruction As System.Windows.Forms.Label
    Friend WithEvents txtInstruction As System.Windows.Forms.TextBox
    Friend WithEvents pnlCPTCode As System.Windows.Forms.Panel
    Friend WithEvents lblResultType As System.Windows.Forms.Label
    Friend WithEvents cmbAssociatedEM As System.Windows.Forms.ComboBox
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents miniToolStrip As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_TestMaster As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAlternateResultCode As System.Windows.Forms.TextBox
    Friend WithEvents gbPreferedLab As System.Windows.Forms.GroupBox
    Friend WithEvents C1PreferedLab As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents btnPreferredLabDelete As System.Windows.Forms.Button

End Class
