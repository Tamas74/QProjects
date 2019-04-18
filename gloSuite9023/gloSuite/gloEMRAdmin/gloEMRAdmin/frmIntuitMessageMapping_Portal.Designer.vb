<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIntuitMessageMapping_Portal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIntuitMessageMapping_Portal))
        Me.Tb_Messages = New System.Windows.Forms.TabControl()
        Me.TbPg_AppointmentRequest = New System.Windows.Forms.TabPage()
        Me.pnlApptDefaultUser = New System.Windows.Forms.Panel()
        Me.cmbApptDefaultUser = New System.Windows.Forms.ComboBox()
        Me.btnSearchApptUser = New System.Windows.Forms.Button()
        Me.btnClearApptUsers = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnApptDown = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbApptUser = New System.Windows.Forms.ComboBox()
        Me.btnApptUp = New System.Windows.Forms.Button()
        Me.c1AppointmentRequest = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TbPg_RxRenewal = New System.Windows.Forms.TabPage()
        Me.pnlRxDefaultUser = New System.Windows.Forms.Panel()
        Me.cmbRxDefaultUser = New System.Windows.Forms.ComboBox()
        Me.btnSearchRxUser = New System.Windows.Forms.Button()
        Me.btnClearRxUsers = New System.Windows.Forms.Button()
        Me.btnRxDown = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmbRxUser = New System.Windows.Forms.ComboBox()
        Me.btnRxUp = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.C1RxRenewal = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TbPg_BillPay = New System.Windows.Forms.TabPage()
        Me.pnlBillPayDefaultUser = New System.Windows.Forms.Panel()
        Me.cmbBillPayDefaultUser = New System.Windows.Forms.ComboBox()
        Me.btnSearchBillPayUser = New System.Windows.Forms.Button()
        Me.btnClearBillPayUsers = New System.Windows.Forms.Button()
        Me.btnBillDown = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbBillPayUser = New System.Windows.Forms.ComboBox()
        Me.btnBillUp = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.C1BillPay = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TbPg_PortalUsers = New System.Windows.Forms.TabPage()
        Me.pnlPortalDefaultUser = New System.Windows.Forms.Panel()
        Me.cmbPortalDefaultUser = New System.Windows.Forms.ComboBox()
        Me.btnSearchPortalUser = New System.Windows.Forms.Button()
        Me.btnClearPortalUsers = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnPortalDown = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.cmbPortalUsers = New System.Windows.Forms.ComboBox()
        Me.btnPortalUp = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.C1PortalUsers = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlAutoCompleteTasK = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.chkAutoCompleteTask = New System.Windows.Forms.CheckBox()
        Me.TbPg_OnlinePatientForm = New System.Windows.Forms.TabPage()
        Me.pnlPFEnableTaskNotification = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.chkPFEnableTaskNotification = New System.Windows.Forms.CheckBox()
        Me.pnlPatientFormDefaultUser = New System.Windows.Forms.Panel()
        Me.cmbPatientFormDefaultUser = New System.Windows.Forms.ComboBox()
        Me.btnSearchPatientFormUser = New System.Windows.Forms.Button()
        Me.btnClearPatientFormUsers = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.btnPatientFormDown = New System.Windows.Forms.Button()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cmbPatientFormUsers = New System.Windows.Forms.ComboBox()
        Me.btnPatientFormUp = New System.Windows.Forms.Button()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.C1PatientFormUsers = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlPatientFormAutoCompleteTasK = New System.Windows.Forms.Panel()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.chkPatientFormAutoCompleteTask = New System.Windows.Forms.CheckBox()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.TopToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ts_btnAddLine = New System.Windows.Forms.ToolStripButton()
        Me.tsb_btnRemoveLine = New System.Windows.Forms.ToolStripButton()
        Me.tsb_Saveclose = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlDetails = New System.Windows.Forms.Panel()
        Me.imgTreeVIew = New System.Windows.Forms.ImageList(Me.components)
        Me.Tb_Messages.SuspendLayout()
        Me.TbPg_AppointmentRequest.SuspendLayout()
        Me.pnlApptDefaultUser.SuspendLayout()
        CType(Me.c1AppointmentRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TbPg_RxRenewal.SuspendLayout()
        Me.pnlRxDefaultUser.SuspendLayout()
        CType(Me.C1RxRenewal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TbPg_BillPay.SuspendLayout()
        Me.pnlBillPayDefaultUser.SuspendLayout()
        CType(Me.C1BillPay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TbPg_PortalUsers.SuspendLayout()
        Me.pnlPortalDefaultUser.SuspendLayout()
        CType(Me.C1PortalUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAutoCompleteTasK.SuspendLayout()
        Me.TbPg_OnlinePatientForm.SuspendLayout()
        Me.pnlPFEnableTaskNotification.SuspendLayout()
        Me.pnlPatientFormDefaultUser.SuspendLayout()
        CType(Me.C1PatientFormUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPatientFormAutoCompleteTasK.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.TopToolStrip.SuspendLayout()
        Me.pnlDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'Tb_Messages
        '
        Me.Tb_Messages.Controls.Add(Me.TbPg_AppointmentRequest)
        Me.Tb_Messages.Controls.Add(Me.TbPg_RxRenewal)
        Me.Tb_Messages.Controls.Add(Me.TbPg_BillPay)
        Me.Tb_Messages.Controls.Add(Me.TbPg_PortalUsers)
        Me.Tb_Messages.Controls.Add(Me.TbPg_OnlinePatientForm)
        Me.Tb_Messages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_Messages.ItemSize = New System.Drawing.Size(90, 19)
        Me.Tb_Messages.Location = New System.Drawing.Point(3, 3)
        Me.Tb_Messages.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Tb_Messages.Name = "Tb_Messages"
        Me.Tb_Messages.SelectedIndex = 0
        Me.Tb_Messages.Size = New System.Drawing.Size(772, 506)
        Me.Tb_Messages.TabIndex = 0
        '
        'TbPg_AppointmentRequest
        '
        Me.TbPg_AppointmentRequest.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TbPg_AppointmentRequest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TbPg_AppointmentRequest.Controls.Add(Me.pnlApptDefaultUser)
        Me.TbPg_AppointmentRequest.Controls.Add(Me.Label6)
        Me.TbPg_AppointmentRequest.Controls.Add(Me.Label5)
        Me.TbPg_AppointmentRequest.Controls.Add(Me.Label4)
        Me.TbPg_AppointmentRequest.Controls.Add(Me.Label3)
        Me.TbPg_AppointmentRequest.Controls.Add(Me.btnApptDown)
        Me.TbPg_AppointmentRequest.Controls.Add(Me.Label2)
        Me.TbPg_AppointmentRequest.Controls.Add(Me.cmbApptUser)
        Me.TbPg_AppointmentRequest.Controls.Add(Me.btnApptUp)
        Me.TbPg_AppointmentRequest.Controls.Add(Me.c1AppointmentRequest)
        Me.TbPg_AppointmentRequest.Controls.Add(Me.Label1)
        Me.TbPg_AppointmentRequest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.TbPg_AppointmentRequest.Location = New System.Drawing.Point(4, 23)
        Me.TbPg_AppointmentRequest.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TbPg_AppointmentRequest.Name = "TbPg_AppointmentRequest"
        Me.TbPg_AppointmentRequest.Size = New System.Drawing.Size(764, 479)
        Me.TbPg_AppointmentRequest.TabIndex = 0
        Me.TbPg_AppointmentRequest.Tag = "Appointment Request"
        Me.TbPg_AppointmentRequest.Text = "Appointment Request"
        '
        'pnlApptDefaultUser
        '
        Me.pnlApptDefaultUser.Controls.Add(Me.cmbApptDefaultUser)
        Me.pnlApptDefaultUser.Controls.Add(Me.btnSearchApptUser)
        Me.pnlApptDefaultUser.Controls.Add(Me.btnClearApptUsers)
        Me.pnlApptDefaultUser.Location = New System.Drawing.Point(280, 7)
        Me.pnlApptDefaultUser.Name = "pnlApptDefaultUser"
        Me.pnlApptDefaultUser.Size = New System.Drawing.Size(478, 29)
        Me.pnlApptDefaultUser.TabIndex = 21
        Me.pnlApptDefaultUser.Visible = False
        '
        'cmbApptDefaultUser
        '
        Me.cmbApptDefaultUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbApptDefaultUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbApptDefaultUser.ForeColor = System.Drawing.Color.Black
        Me.cmbApptDefaultUser.FormattingEnabled = True
        Me.cmbApptDefaultUser.Location = New System.Drawing.Point(3, 3)
        Me.cmbApptDefaultUser.Name = "cmbApptDefaultUser"
        Me.cmbApptDefaultUser.Size = New System.Drawing.Size(420, 22)
        Me.cmbApptDefaultUser.TabIndex = 18
        '
        'btnSearchApptUser
        '
        Me.btnSearchApptUser.BackgroundImage = CType(resources.GetObject("btnSearchApptUser.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchApptUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchApptUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchApptUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchApptUser.Image = CType(resources.GetObject("btnSearchApptUser.Image"), System.Drawing.Image)
        Me.btnSearchApptUser.Location = New System.Drawing.Point(429, 4)
        Me.btnSearchApptUser.Name = "btnSearchApptUser"
        Me.btnSearchApptUser.Size = New System.Drawing.Size(21, 21)
        Me.btnSearchApptUser.TabIndex = 19
        '
        'btnClearApptUsers
        '
        Me.btnClearApptUsers.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnClearApptUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearApptUsers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClearApptUsers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClearApptUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearApptUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearApptUsers.Image = CType(resources.GetObject("btnClearApptUsers.Image"), System.Drawing.Image)
        Me.btnClearApptUsers.Location = New System.Drawing.Point(454, 4)
        Me.btnClearApptUsers.Name = "btnClearApptUsers"
        Me.btnClearApptUsers.Size = New System.Drawing.Size(21, 21)
        Me.btnClearApptUsers.TabIndex = 20
        Me.btnClearApptUsers.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(1, 478)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(762, 1)
        Me.Label6.TabIndex = 17
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(1, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(762, 1)
        Me.Label5.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(763, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 479)
        Me.Label4.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 479)
        Me.Label3.TabIndex = 14
        '
        'btnApptDown
        '
        Me.btnApptDown.BackColor = System.Drawing.Color.Transparent
        Me.btnApptDown.FlatAppearance.BorderSize = 0
        Me.btnApptDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnApptDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApptDown.Image = CType(resources.GetObject("btnApptDown.Image"), System.Drawing.Image)
        Me.btnApptDown.Location = New System.Drawing.Point(21, 106)
        Me.btnApptDown.Name = "btnApptDown"
        Me.btnApptDown.Size = New System.Drawing.Size(32, 30)
        Me.btnApptDown.TabIndex = 13
        Me.btnApptDown.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(123, 14)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Advanced Settings"
        '
        'cmbApptUser
        '
        Me.cmbApptUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbApptUser.FormattingEnabled = True
        Me.cmbApptUser.Location = New System.Drawing.Point(280, 9)
        Me.cmbApptUser.Name = "cmbApptUser"
        Me.cmbApptUser.Size = New System.Drawing.Size(470, 22)
        Me.cmbApptUser.TabIndex = 11
        '
        'btnApptUp
        '
        Me.btnApptUp.AutoSize = True
        Me.btnApptUp.BackColor = System.Drawing.Color.Transparent
        Me.btnApptUp.FlatAppearance.BorderSize = 0
        Me.btnApptUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnApptUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApptUp.Image = CType(resources.GetObject("btnApptUp.Image"), System.Drawing.Image)
        Me.btnApptUp.Location = New System.Drawing.Point(21, 74)
        Me.btnApptUp.Name = "btnApptUp"
        Me.btnApptUp.Size = New System.Drawing.Size(32, 30)
        Me.btnApptUp.TabIndex = 10
        Me.btnApptUp.UseVisualStyleBackColor = False
        '
        'c1AppointmentRequest
        '
        Me.c1AppointmentRequest.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1AppointmentRequest.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.c1AppointmentRequest.AutoGenerateColumns = False
        Me.c1AppointmentRequest.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1AppointmentRequest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.c1AppointmentRequest.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.c1AppointmentRequest.ColumnInfo = resources.GetString("c1AppointmentRequest.ColumnInfo")
        Me.c1AppointmentRequest.ExtendLastCol = True
        Me.c1AppointmentRequest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1AppointmentRequest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1AppointmentRequest.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.c1AppointmentRequest.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1AppointmentRequest.Location = New System.Drawing.Point(65, 74)
        Me.c1AppointmentRequest.Name = "c1AppointmentRequest"
        Me.c1AppointmentRequest.Rows.Count = 1
        Me.c1AppointmentRequest.Rows.DefaultSize = 21
        Me.c1AppointmentRequest.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1AppointmentRequest.Size = New System.Drawing.Size(693, 400)
        Me.c1AppointmentRequest.StyleInfo = resources.GetString("c1AppointmentRequest.StyleInfo")
        Me.c1AppointmentRequest.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(7, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 14)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Send 'Appointment Request' tasks to the User :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TbPg_RxRenewal
        '
        Me.TbPg_RxRenewal.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TbPg_RxRenewal.Controls.Add(Me.pnlRxDefaultUser)
        Me.TbPg_RxRenewal.Controls.Add(Me.btnRxDown)
        Me.TbPg_RxRenewal.Controls.Add(Me.Label15)
        Me.TbPg_RxRenewal.Controls.Add(Me.cmbRxUser)
        Me.TbPg_RxRenewal.Controls.Add(Me.btnRxUp)
        Me.TbPg_RxRenewal.Controls.Add(Me.Label16)
        Me.TbPg_RxRenewal.Controls.Add(Me.C1RxRenewal)
        Me.TbPg_RxRenewal.Controls.Add(Me.Label7)
        Me.TbPg_RxRenewal.Controls.Add(Me.Label8)
        Me.TbPg_RxRenewal.Controls.Add(Me.Label9)
        Me.TbPg_RxRenewal.Controls.Add(Me.Label10)
        Me.TbPg_RxRenewal.Location = New System.Drawing.Point(4, 23)
        Me.TbPg_RxRenewal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TbPg_RxRenewal.Name = "TbPg_RxRenewal"
        Me.TbPg_RxRenewal.Size = New System.Drawing.Size(764, 479)
        Me.TbPg_RxRenewal.TabIndex = 1
        Me.TbPg_RxRenewal.Tag = "Rx Renewal"
        Me.TbPg_RxRenewal.Text = "Rx Renewal"
        '
        'pnlRxDefaultUser
        '
        Me.pnlRxDefaultUser.Controls.Add(Me.cmbRxDefaultUser)
        Me.pnlRxDefaultUser.Controls.Add(Me.btnSearchRxUser)
        Me.pnlRxDefaultUser.Controls.Add(Me.btnClearRxUsers)
        Me.pnlRxDefaultUser.Location = New System.Drawing.Point(221, 7)
        Me.pnlRxDefaultUser.Name = "pnlRxDefaultUser"
        Me.pnlRxDefaultUser.Size = New System.Drawing.Size(478, 29)
        Me.pnlRxDefaultUser.TabIndex = 28
        Me.pnlRxDefaultUser.Visible = False
        '
        'cmbRxDefaultUser
        '
        Me.cmbRxDefaultUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRxDefaultUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbRxDefaultUser.ForeColor = System.Drawing.Color.Black
        Me.cmbRxDefaultUser.FormattingEnabled = True
        Me.cmbRxDefaultUser.Location = New System.Drawing.Point(3, 3)
        Me.cmbRxDefaultUser.Name = "cmbRxDefaultUser"
        Me.cmbRxDefaultUser.Size = New System.Drawing.Size(420, 22)
        Me.cmbRxDefaultUser.TabIndex = 18
        '
        'btnSearchRxUser
        '
        Me.btnSearchRxUser.BackgroundImage = CType(resources.GetObject("btnSearchRxUser.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchRxUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchRxUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchRxUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchRxUser.Image = CType(resources.GetObject("btnSearchRxUser.Image"), System.Drawing.Image)
        Me.btnSearchRxUser.Location = New System.Drawing.Point(429, 4)
        Me.btnSearchRxUser.Name = "btnSearchRxUser"
        Me.btnSearchRxUser.Size = New System.Drawing.Size(21, 21)
        Me.btnSearchRxUser.TabIndex = 19
        '
        'btnClearRxUsers
        '
        Me.btnClearRxUsers.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnClearRxUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearRxUsers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClearRxUsers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClearRxUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearRxUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearRxUsers.Image = CType(resources.GetObject("btnClearRxUsers.Image"), System.Drawing.Image)
        Me.btnClearRxUsers.Location = New System.Drawing.Point(454, 4)
        Me.btnClearRxUsers.Name = "btnClearRxUsers"
        Me.btnClearRxUsers.Size = New System.Drawing.Size(21, 21)
        Me.btnClearRxUsers.TabIndex = 20
        Me.btnClearRxUsers.UseVisualStyleBackColor = True
        '
        'btnRxDown
        '
        Me.btnRxDown.BackColor = System.Drawing.Color.Transparent
        Me.btnRxDown.FlatAppearance.BorderSize = 0
        Me.btnRxDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRxDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRxDown.Image = CType(resources.GetObject("btnRxDown.Image"), System.Drawing.Image)
        Me.btnRxDown.Location = New System.Drawing.Point(21, 106)
        Me.btnRxDown.Name = "btnRxDown"
        Me.btnRxDown.Size = New System.Drawing.Size(32, 30)
        Me.btnRxDown.TabIndex = 27
        Me.btnRxDown.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(9, 51)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(123, 14)
        Me.Label15.TabIndex = 26
        Me.Label15.Text = "Advanced Settings"
        '
        'cmbRxUser
        '
        Me.cmbRxUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRxUser.FormattingEnabled = True
        Me.cmbRxUser.Location = New System.Drawing.Point(224, 10)
        Me.cmbRxUser.Name = "cmbRxUser"
        Me.cmbRxUser.Size = New System.Drawing.Size(470, 22)
        Me.cmbRxUser.TabIndex = 25
        '
        'btnRxUp
        '
        Me.btnRxUp.AutoSize = True
        Me.btnRxUp.BackColor = System.Drawing.Color.Transparent
        Me.btnRxUp.FlatAppearance.BorderSize = 0
        Me.btnRxUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRxUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRxUp.Image = CType(resources.GetObject("btnRxUp.Image"), System.Drawing.Image)
        Me.btnRxUp.Location = New System.Drawing.Point(21, 74)
        Me.btnRxUp.Name = "btnRxUp"
        Me.btnRxUp.Size = New System.Drawing.Size(32, 30)
        Me.btnRxUp.TabIndex = 24
        Me.btnRxUp.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(7, 13)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(214, 14)
        Me.Label16.TabIndex = 23
        Me.Label16.Text = "Send 'Rx Renewal' tasks to the User :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'C1RxRenewal
        '
        Me.C1RxRenewal.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1RxRenewal.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1RxRenewal.AutoGenerateColumns = False
        Me.C1RxRenewal.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1RxRenewal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.C1RxRenewal.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1RxRenewal.ColumnInfo = resources.GetString("C1RxRenewal.ColumnInfo")
        Me.C1RxRenewal.ExtendLastCol = True
        Me.C1RxRenewal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1RxRenewal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1RxRenewal.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1RxRenewal.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1RxRenewal.Location = New System.Drawing.Point(65, 74)
        Me.C1RxRenewal.Name = "C1RxRenewal"
        Me.C1RxRenewal.Rows.Count = 1
        Me.C1RxRenewal.Rows.DefaultSize = 21
        Me.C1RxRenewal.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1RxRenewal.Size = New System.Drawing.Size(693, 400)
        Me.C1RxRenewal.StyleInfo = resources.GetString("C1RxRenewal.StyleInfo")
        Me.C1RxRenewal.TabIndex = 22
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(1, 478)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(762, 1)
        Me.Label7.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(1, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(762, 1)
        Me.Label8.TabIndex = 20
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Location = New System.Drawing.Point(763, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 479)
        Me.Label9.TabIndex = 19
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 479)
        Me.Label10.TabIndex = 18
        '
        'TbPg_BillPay
        '
        Me.TbPg_BillPay.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TbPg_BillPay.Controls.Add(Me.pnlBillPayDefaultUser)
        Me.TbPg_BillPay.Controls.Add(Me.btnBillDown)
        Me.TbPg_BillPay.Controls.Add(Me.Label17)
        Me.TbPg_BillPay.Controls.Add(Me.cmbBillPayUser)
        Me.TbPg_BillPay.Controls.Add(Me.btnBillUp)
        Me.TbPg_BillPay.Controls.Add(Me.Label18)
        Me.TbPg_BillPay.Controls.Add(Me.C1BillPay)
        Me.TbPg_BillPay.Controls.Add(Me.Label11)
        Me.TbPg_BillPay.Controls.Add(Me.Label12)
        Me.TbPg_BillPay.Controls.Add(Me.Label13)
        Me.TbPg_BillPay.Controls.Add(Me.Label14)
        Me.TbPg_BillPay.Location = New System.Drawing.Point(4, 23)
        Me.TbPg_BillPay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TbPg_BillPay.Name = "TbPg_BillPay"
        Me.TbPg_BillPay.Size = New System.Drawing.Size(764, 479)
        Me.TbPg_BillPay.TabIndex = 2
        Me.TbPg_BillPay.Tag = "Online Bill Pay"
        Me.TbPg_BillPay.Text = "Online Bill Pay"
        '
        'pnlBillPayDefaultUser
        '
        Me.pnlBillPayDefaultUser.Controls.Add(Me.cmbBillPayDefaultUser)
        Me.pnlBillPayDefaultUser.Controls.Add(Me.btnSearchBillPayUser)
        Me.pnlBillPayDefaultUser.Controls.Add(Me.btnClearBillPayUsers)
        Me.pnlBillPayDefaultUser.Location = New System.Drawing.Point(233, 7)
        Me.pnlBillPayDefaultUser.Name = "pnlBillPayDefaultUser"
        Me.pnlBillPayDefaultUser.Size = New System.Drawing.Size(478, 29)
        Me.pnlBillPayDefaultUser.TabIndex = 29
        Me.pnlBillPayDefaultUser.Visible = False
        '
        'cmbBillPayDefaultUser
        '
        Me.cmbBillPayDefaultUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBillPayDefaultUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbBillPayDefaultUser.ForeColor = System.Drawing.Color.Black
        Me.cmbBillPayDefaultUser.FormattingEnabled = True
        Me.cmbBillPayDefaultUser.Location = New System.Drawing.Point(3, 3)
        Me.cmbBillPayDefaultUser.Name = "cmbBillPayDefaultUser"
        Me.cmbBillPayDefaultUser.Size = New System.Drawing.Size(420, 22)
        Me.cmbBillPayDefaultUser.TabIndex = 18
        '
        'btnSearchBillPayUser
        '
        Me.btnSearchBillPayUser.BackgroundImage = CType(resources.GetObject("btnSearchBillPayUser.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchBillPayUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchBillPayUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchBillPayUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchBillPayUser.Image = CType(resources.GetObject("btnSearchBillPayUser.Image"), System.Drawing.Image)
        Me.btnSearchBillPayUser.Location = New System.Drawing.Point(429, 4)
        Me.btnSearchBillPayUser.Name = "btnSearchBillPayUser"
        Me.btnSearchBillPayUser.Size = New System.Drawing.Size(21, 21)
        Me.btnSearchBillPayUser.TabIndex = 19
        '
        'btnClearBillPayUsers
        '
        Me.btnClearBillPayUsers.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnClearBillPayUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearBillPayUsers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClearBillPayUsers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClearBillPayUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearBillPayUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearBillPayUsers.Image = CType(resources.GetObject("btnClearBillPayUsers.Image"), System.Drawing.Image)
        Me.btnClearBillPayUsers.Location = New System.Drawing.Point(454, 4)
        Me.btnClearBillPayUsers.Name = "btnClearBillPayUsers"
        Me.btnClearBillPayUsers.Size = New System.Drawing.Size(21, 21)
        Me.btnClearBillPayUsers.TabIndex = 20
        Me.btnClearBillPayUsers.UseVisualStyleBackColor = True
        '
        'btnBillDown
        '
        Me.btnBillDown.BackColor = System.Drawing.Color.Transparent
        Me.btnBillDown.FlatAppearance.BorderSize = 0
        Me.btnBillDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBillDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBillDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBillDown.Image = CType(resources.GetObject("btnBillDown.Image"), System.Drawing.Image)
        Me.btnBillDown.Location = New System.Drawing.Point(21, 106)
        Me.btnBillDown.Name = "btnBillDown"
        Me.btnBillDown.Size = New System.Drawing.Size(32, 30)
        Me.btnBillDown.TabIndex = 27
        Me.btnBillDown.UseVisualStyleBackColor = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(9, 51)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(123, 14)
        Me.Label17.TabIndex = 26
        Me.Label17.Text = "Advanced Settings"
        '
        'cmbBillPayUser
        '
        Me.cmbBillPayUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBillPayUser.FormattingEnabled = True
        Me.cmbBillPayUser.Location = New System.Drawing.Point(233, 10)
        Me.cmbBillPayUser.Name = "cmbBillPayUser"
        Me.cmbBillPayUser.Size = New System.Drawing.Size(470, 22)
        Me.cmbBillPayUser.TabIndex = 25
        '
        'btnBillUp
        '
        Me.btnBillUp.AutoSize = True
        Me.btnBillUp.BackColor = System.Drawing.Color.Transparent
        Me.btnBillUp.FlatAppearance.BorderSize = 0
        Me.btnBillUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBillUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBillUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBillUp.Image = CType(resources.GetObject("btnBillUp.Image"), System.Drawing.Image)
        Me.btnBillUp.Location = New System.Drawing.Point(21, 74)
        Me.btnBillUp.Name = "btnBillUp"
        Me.btnBillUp.Size = New System.Drawing.Size(32, 30)
        Me.btnBillUp.TabIndex = 24
        Me.btnBillUp.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(7, 13)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(225, 14)
        Me.Label18.TabIndex = 23
        Me.Label18.Text = "Send 'Online Bill Pay' tasks to the User :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'C1BillPay
        '
        Me.C1BillPay.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1BillPay.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1BillPay.AutoGenerateColumns = False
        Me.C1BillPay.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1BillPay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.C1BillPay.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1BillPay.ColumnInfo = resources.GetString("C1BillPay.ColumnInfo")
        Me.C1BillPay.ExtendLastCol = True
        Me.C1BillPay.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1BillPay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1BillPay.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1BillPay.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1BillPay.Location = New System.Drawing.Point(65, 74)
        Me.C1BillPay.Name = "C1BillPay"
        Me.C1BillPay.Rows.Count = 1
        Me.C1BillPay.Rows.DefaultSize = 21
        Me.C1BillPay.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1BillPay.Size = New System.Drawing.Size(693, 400)
        Me.C1BillPay.StyleInfo = resources.GetString("C1BillPay.StyleInfo")
        Me.C1BillPay.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(1, 478)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(762, 1)
        Me.Label11.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(1, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(762, 1)
        Me.Label12.TabIndex = 20
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Location = New System.Drawing.Point(763, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 479)
        Me.Label13.TabIndex = 19
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 479)
        Me.Label14.TabIndex = 18
        '
        'TbPg_PortalUsers
        '
        Me.TbPg_PortalUsers.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TbPg_PortalUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TbPg_PortalUsers.Controls.Add(Me.pnlPortalDefaultUser)
        Me.TbPg_PortalUsers.Controls.Add(Me.Label19)
        Me.TbPg_PortalUsers.Controls.Add(Me.Label20)
        Me.TbPg_PortalUsers.Controls.Add(Me.Label21)
        Me.TbPg_PortalUsers.Controls.Add(Me.Label22)
        Me.TbPg_PortalUsers.Controls.Add(Me.btnPortalDown)
        Me.TbPg_PortalUsers.Controls.Add(Me.Label23)
        Me.TbPg_PortalUsers.Controls.Add(Me.cmbPortalUsers)
        Me.TbPg_PortalUsers.Controls.Add(Me.btnPortalUp)
        Me.TbPg_PortalUsers.Controls.Add(Me.Label24)
        Me.TbPg_PortalUsers.Controls.Add(Me.C1PortalUsers)
        Me.TbPg_PortalUsers.Controls.Add(Me.pnlAutoCompleteTasK)
        Me.TbPg_PortalUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.TbPg_PortalUsers.Location = New System.Drawing.Point(4, 23)
        Me.TbPg_PortalUsers.Name = "TbPg_PortalUsers"
        Me.TbPg_PortalUsers.Size = New System.Drawing.Size(764, 479)
        Me.TbPg_PortalUsers.TabIndex = 3
        Me.TbPg_PortalUsers.Tag = "Review Portal Users"
        Me.TbPg_PortalUsers.Text = "Review Portal Users"
        '
        'pnlPortalDefaultUser
        '
        Me.pnlPortalDefaultUser.Controls.Add(Me.cmbPortalDefaultUser)
        Me.pnlPortalDefaultUser.Controls.Add(Me.btnSearchPortalUser)
        Me.pnlPortalDefaultUser.Controls.Add(Me.btnClearPortalUsers)
        Me.pnlPortalDefaultUser.Location = New System.Drawing.Point(268, 6)
        Me.pnlPortalDefaultUser.Name = "pnlPortalDefaultUser"
        Me.pnlPortalDefaultUser.Size = New System.Drawing.Size(478, 29)
        Me.pnlPortalDefaultUser.TabIndex = 29
        Me.pnlPortalDefaultUser.Visible = False
        '
        'cmbPortalDefaultUser
        '
        Me.cmbPortalDefaultUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPortalDefaultUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPortalDefaultUser.ForeColor = System.Drawing.Color.Black
        Me.cmbPortalDefaultUser.FormattingEnabled = True
        Me.cmbPortalDefaultUser.Location = New System.Drawing.Point(3, 3)
        Me.cmbPortalDefaultUser.Name = "cmbPortalDefaultUser"
        Me.cmbPortalDefaultUser.Size = New System.Drawing.Size(420, 22)
        Me.cmbPortalDefaultUser.TabIndex = 18
        '
        'btnSearchPortalUser
        '
        Me.btnSearchPortalUser.BackgroundImage = CType(resources.GetObject("btnSearchPortalUser.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchPortalUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchPortalUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchPortalUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchPortalUser.Image = CType(resources.GetObject("btnSearchPortalUser.Image"), System.Drawing.Image)
        Me.btnSearchPortalUser.Location = New System.Drawing.Point(429, 4)
        Me.btnSearchPortalUser.Name = "btnSearchPortalUser"
        Me.btnSearchPortalUser.Size = New System.Drawing.Size(21, 21)
        Me.btnSearchPortalUser.TabIndex = 19
        '
        'btnClearPortalUsers
        '
        Me.btnClearPortalUsers.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnClearPortalUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearPortalUsers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClearPortalUsers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClearPortalUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearPortalUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearPortalUsers.Image = CType(resources.GetObject("btnClearPortalUsers.Image"), System.Drawing.Image)
        Me.btnClearPortalUsers.Location = New System.Drawing.Point(454, 4)
        Me.btnClearPortalUsers.Name = "btnClearPortalUsers"
        Me.btnClearPortalUsers.Size = New System.Drawing.Size(21, 21)
        Me.btnClearPortalUsers.TabIndex = 20
        Me.btnClearPortalUsers.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Location = New System.Drawing.Point(1, 478)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(762, 1)
        Me.Label19.TabIndex = 17
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(1, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(762, 1)
        Me.Label20.TabIndex = 16
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label21.Location = New System.Drawing.Point(763, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 479)
        Me.Label21.TabIndex = 15
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 479)
        Me.Label22.TabIndex = 14
        '
        'btnPortalDown
        '
        Me.btnPortalDown.BackColor = System.Drawing.Color.Transparent
        Me.btnPortalDown.FlatAppearance.BorderSize = 0
        Me.btnPortalDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPortalDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPortalDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPortalDown.Image = CType(resources.GetObject("btnPortalDown.Image"), System.Drawing.Image)
        Me.btnPortalDown.Location = New System.Drawing.Point(21, 119)
        Me.btnPortalDown.Name = "btnPortalDown"
        Me.btnPortalDown.Size = New System.Drawing.Size(32, 30)
        Me.btnPortalDown.TabIndex = 13
        Me.btnPortalDown.UseVisualStyleBackColor = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(9, 64)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(123, 14)
        Me.Label23.TabIndex = 12
        Me.Label23.Text = "Advanced Settings"
        '
        'cmbPortalUsers
        '
        Me.cmbPortalUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPortalUsers.FormattingEnabled = True
        Me.cmbPortalUsers.Location = New System.Drawing.Point(269, 9)
        Me.cmbPortalUsers.Name = "cmbPortalUsers"
        Me.cmbPortalUsers.Size = New System.Drawing.Size(470, 22)
        Me.cmbPortalUsers.TabIndex = 11
        '
        'btnPortalUp
        '
        Me.btnPortalUp.AutoSize = True
        Me.btnPortalUp.BackColor = System.Drawing.Color.Transparent
        Me.btnPortalUp.FlatAppearance.BorderSize = 0
        Me.btnPortalUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPortalUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPortalUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPortalUp.Image = CType(resources.GetObject("btnPortalUp.Image"), System.Drawing.Image)
        Me.btnPortalUp.Location = New System.Drawing.Point(21, 87)
        Me.btnPortalUp.Name = "btnPortalUp"
        Me.btnPortalUp.Size = New System.Drawing.Size(32, 30)
        Me.btnPortalUp.TabIndex = 10
        Me.btnPortalUp.UseVisualStyleBackColor = False
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(7, 13)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(258, 15)
        Me.Label24.TabIndex = 9
        Me.Label24.Text = "Send 'Review Portal Users' tasks to the User :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'C1PortalUsers
        '
        Me.C1PortalUsers.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1PortalUsers.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1PortalUsers.AutoGenerateColumns = False
        Me.C1PortalUsers.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1PortalUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.C1PortalUsers.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1PortalUsers.ColumnInfo = resources.GetString("C1PortalUsers.ColumnInfo")
        Me.C1PortalUsers.ExtendLastCol = True
        Me.C1PortalUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PortalUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1PortalUsers.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1PortalUsers.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1PortalUsers.Location = New System.Drawing.Point(65, 91)
        Me.C1PortalUsers.Name = "C1PortalUsers"
        Me.C1PortalUsers.Rows.Count = 1
        Me.C1PortalUsers.Rows.DefaultSize = 21
        Me.C1PortalUsers.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PortalUsers.Size = New System.Drawing.Size(693, 383)
        Me.C1PortalUsers.StyleInfo = resources.GetString("C1PortalUsers.StyleInfo")
        Me.C1PortalUsers.TabIndex = 8
        '
        'pnlAutoCompleteTasK
        '
        Me.pnlAutoCompleteTasK.Controls.Add(Me.Label25)
        Me.pnlAutoCompleteTasK.Controls.Add(Me.chkAutoCompleteTask)
        Me.pnlAutoCompleteTasK.Location = New System.Drawing.Point(103, 35)
        Me.pnlAutoCompleteTasK.Name = "pnlAutoCompleteTasK"
        Me.pnlAutoCompleteTasK.Size = New System.Drawing.Size(200, 25)
        Me.pnlAutoCompleteTasK.TabIndex = 20
        Me.pnlAutoCompleteTasK.Visible = False
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(7, 5)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(155, 15)
        Me.Label25.TabIndex = 19
        Me.Label25.Text = "Auto Complete Task :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkAutoCompleteTask
        '
        Me.chkAutoCompleteTask.AutoSize = True
        Me.chkAutoCompleteTask.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkAutoCompleteTask.Location = New System.Drawing.Point(172, 7)
        Me.chkAutoCompleteTask.Name = "chkAutoCompleteTask"
        Me.chkAutoCompleteTask.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkAutoCompleteTask.Size = New System.Drawing.Size(15, 14)
        Me.chkAutoCompleteTask.TabIndex = 18
        Me.chkAutoCompleteTask.UseVisualStyleBackColor = True
        '
        'TbPg_OnlinePatientForm
        '
        Me.TbPg_OnlinePatientForm.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TbPg_OnlinePatientForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.pnlPFEnableTaskNotification)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.pnlPatientFormDefaultUser)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.Label26)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.Label27)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.Label28)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.Label29)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.btnPatientFormDown)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.Label30)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.cmbPatientFormUsers)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.btnPatientFormUp)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.Label31)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.C1PatientFormUsers)
        Me.TbPg_OnlinePatientForm.Controls.Add(Me.pnlPatientFormAutoCompleteTasK)
        Me.TbPg_OnlinePatientForm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.TbPg_OnlinePatientForm.Location = New System.Drawing.Point(4, 23)
        Me.TbPg_OnlinePatientForm.Name = "TbPg_OnlinePatientForm"
        Me.TbPg_OnlinePatientForm.Size = New System.Drawing.Size(764, 479)
        Me.TbPg_OnlinePatientForm.TabIndex = 5
        Me.TbPg_OnlinePatientForm.Tag = "Online Patient Form"
        Me.TbPg_OnlinePatientForm.Text = "Online Patient Form"
        '
        'pnlPFEnableTaskNotification
        '
        Me.pnlPFEnableTaskNotification.Controls.Add(Me.Label33)
        Me.pnlPFEnableTaskNotification.Controls.Add(Me.chkPFEnableTaskNotification)
        Me.pnlPFEnableTaskNotification.Location = New System.Drawing.Point(103, 35)
        Me.pnlPFEnableTaskNotification.Name = "pnlPFEnableTaskNotification"
        Me.pnlPFEnableTaskNotification.Size = New System.Drawing.Size(200, 25)
        Me.pnlPFEnableTaskNotification.TabIndex = 21
        Me.pnlPFEnableTaskNotification.Visible = False
        '
        'Label33
        '
        Me.Label33.Location = New System.Drawing.Point(7, 5)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(155, 15)
        Me.Label33.TabIndex = 19
        Me.Label33.Text = "Enable Task Notification :"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkPFEnableTaskNotification
        '
        Me.chkPFEnableTaskNotification.AutoSize = True
        Me.chkPFEnableTaskNotification.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkPFEnableTaskNotification.Location = New System.Drawing.Point(172, 7)
        Me.chkPFEnableTaskNotification.Name = "chkPFEnableTaskNotification"
        Me.chkPFEnableTaskNotification.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkPFEnableTaskNotification.Size = New System.Drawing.Size(15, 14)
        Me.chkPFEnableTaskNotification.TabIndex = 18
        Me.chkPFEnableTaskNotification.UseVisualStyleBackColor = True
        '
        'pnlPatientFormDefaultUser
        '
        Me.pnlPatientFormDefaultUser.Controls.Add(Me.cmbPatientFormDefaultUser)
        Me.pnlPatientFormDefaultUser.Controls.Add(Me.btnSearchPatientFormUser)
        Me.pnlPatientFormDefaultUser.Controls.Add(Me.btnClearPatientFormUsers)
        Me.pnlPatientFormDefaultUser.Location = New System.Drawing.Point(268, 6)
        Me.pnlPatientFormDefaultUser.Name = "pnlPatientFormDefaultUser"
        Me.pnlPatientFormDefaultUser.Size = New System.Drawing.Size(478, 29)
        Me.pnlPatientFormDefaultUser.TabIndex = 29
        Me.pnlPatientFormDefaultUser.Visible = False
        '
        'cmbPatientFormDefaultUser
        '
        Me.cmbPatientFormDefaultUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPatientFormDefaultUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPatientFormDefaultUser.ForeColor = System.Drawing.Color.Black
        Me.cmbPatientFormDefaultUser.FormattingEnabled = True
        Me.cmbPatientFormDefaultUser.Location = New System.Drawing.Point(3, 3)
        Me.cmbPatientFormDefaultUser.Name = "cmbPatientFormDefaultUser"
        Me.cmbPatientFormDefaultUser.Size = New System.Drawing.Size(420, 22)
        Me.cmbPatientFormDefaultUser.TabIndex = 18
        '
        'btnSearchPatientFormUser
        '
        Me.btnSearchPatientFormUser.BackgroundImage = CType(resources.GetObject("btnSearchPatientFormUser.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchPatientFormUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchPatientFormUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchPatientFormUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchPatientFormUser.Image = CType(resources.GetObject("btnSearchPatientFormUser.Image"), System.Drawing.Image)
        Me.btnSearchPatientFormUser.Location = New System.Drawing.Point(429, 4)
        Me.btnSearchPatientFormUser.Name = "btnSearchPatientFormUser"
        Me.btnSearchPatientFormUser.Size = New System.Drawing.Size(21, 21)
        Me.btnSearchPatientFormUser.TabIndex = 19
        '
        'btnClearPatientFormUsers
        '
        Me.btnClearPatientFormUsers.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnClearPatientFormUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearPatientFormUsers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClearPatientFormUsers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClearPatientFormUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearPatientFormUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearPatientFormUsers.Image = CType(resources.GetObject("btnClearPatientFormUsers.Image"), System.Drawing.Image)
        Me.btnClearPatientFormUsers.Location = New System.Drawing.Point(454, 4)
        Me.btnClearPatientFormUsers.Name = "btnClearPatientFormUsers"
        Me.btnClearPatientFormUsers.Size = New System.Drawing.Size(21, 21)
        Me.btnClearPatientFormUsers.TabIndex = 20
        Me.btnClearPatientFormUsers.UseVisualStyleBackColor = True
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Location = New System.Drawing.Point(1, 478)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(762, 1)
        Me.Label26.TabIndex = 17
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label27.Location = New System.Drawing.Point(1, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(762, 1)
        Me.Label27.TabIndex = 16
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Location = New System.Drawing.Point(763, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 479)
        Me.Label28.TabIndex = 15
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 479)
        Me.Label29.TabIndex = 14
        '
        'btnPatientFormDown
        '
        Me.btnPatientFormDown.BackColor = System.Drawing.Color.Transparent
        Me.btnPatientFormDown.FlatAppearance.BorderSize = 0
        Me.btnPatientFormDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPatientFormDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPatientFormDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPatientFormDown.Image = CType(resources.GetObject("btnPatientFormDown.Image"), System.Drawing.Image)
        Me.btnPatientFormDown.Location = New System.Drawing.Point(21, 141)
        Me.btnPatientFormDown.Name = "btnPatientFormDown"
        Me.btnPatientFormDown.Size = New System.Drawing.Size(32, 30)
        Me.btnPatientFormDown.TabIndex = 13
        Me.btnPatientFormDown.UseVisualStyleBackColor = False
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(9, 89)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(123, 14)
        Me.Label30.TabIndex = 12
        Me.Label30.Text = "Advanced Settings"
        '
        'cmbPatientFormUsers
        '
        Me.cmbPatientFormUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPatientFormUsers.FormattingEnabled = True
        Me.cmbPatientFormUsers.Location = New System.Drawing.Point(269, 9)
        Me.cmbPatientFormUsers.Name = "cmbPatientFormUsers"
        Me.cmbPatientFormUsers.Size = New System.Drawing.Size(470, 22)
        Me.cmbPatientFormUsers.TabIndex = 11
        '
        'btnPatientFormUp
        '
        Me.btnPatientFormUp.AutoSize = True
        Me.btnPatientFormUp.BackColor = System.Drawing.Color.Transparent
        Me.btnPatientFormUp.FlatAppearance.BorderSize = 0
        Me.btnPatientFormUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPatientFormUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPatientFormUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPatientFormUp.Image = CType(resources.GetObject("btnPatientFormUp.Image"), System.Drawing.Image)
        Me.btnPatientFormUp.Location = New System.Drawing.Point(21, 109)
        Me.btnPatientFormUp.Name = "btnPatientFormUp"
        Me.btnPatientFormUp.Size = New System.Drawing.Size(32, 30)
        Me.btnPatientFormUp.TabIndex = 10
        Me.btnPatientFormUp.UseVisualStyleBackColor = False
        '
        'Label31
        '
        Me.Label31.Location = New System.Drawing.Point(4, 13)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(261, 15)
        Me.Label31.TabIndex = 9
        Me.Label31.Text = "Send 'Online Patient Form' tasks to the User :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'C1PatientFormUsers
        '
        Me.C1PatientFormUsers.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1PatientFormUsers.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1PatientFormUsers.AutoGenerateColumns = False
        Me.C1PatientFormUsers.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1PatientFormUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.C1PatientFormUsers.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1PatientFormUsers.ColumnInfo = resources.GetString("C1PatientFormUsers.ColumnInfo")
        Me.C1PatientFormUsers.ExtendLastCol = True
        Me.C1PatientFormUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PatientFormUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1PatientFormUsers.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1PatientFormUsers.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1PatientFormUsers.Location = New System.Drawing.Point(65, 109)
        Me.C1PatientFormUsers.Name = "C1PatientFormUsers"
        Me.C1PatientFormUsers.Rows.Count = 1
        Me.C1PatientFormUsers.Rows.DefaultSize = 21
        Me.C1PatientFormUsers.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PatientFormUsers.Size = New System.Drawing.Size(693, 365)
        Me.C1PatientFormUsers.StyleInfo = resources.GetString("C1PatientFormUsers.StyleInfo")
        Me.C1PatientFormUsers.TabIndex = 8
        '
        'pnlPatientFormAutoCompleteTasK
        '
        Me.pnlPatientFormAutoCompleteTasK.Controls.Add(Me.Label32)
        Me.pnlPatientFormAutoCompleteTasK.Controls.Add(Me.chkPatientFormAutoCompleteTask)
        Me.pnlPatientFormAutoCompleteTasK.Location = New System.Drawing.Point(103, 60)
        Me.pnlPatientFormAutoCompleteTasK.Name = "pnlPatientFormAutoCompleteTasK"
        Me.pnlPatientFormAutoCompleteTasK.Size = New System.Drawing.Size(200, 25)
        Me.pnlPatientFormAutoCompleteTasK.TabIndex = 20
        Me.pnlPatientFormAutoCompleteTasK.Visible = False
        '
        'Label32
        '
        Me.Label32.Location = New System.Drawing.Point(7, 5)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(155, 15)
        Me.Label32.TabIndex = 19
        Me.Label32.Text = "Auto Complete Task :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkPatientFormAutoCompleteTask
        '
        Me.chkPatientFormAutoCompleteTask.AutoSize = True
        Me.chkPatientFormAutoCompleteTask.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkPatientFormAutoCompleteTask.Location = New System.Drawing.Point(172, 7)
        Me.chkPatientFormAutoCompleteTask.Name = "chkPatientFormAutoCompleteTask"
        Me.chkPatientFormAutoCompleteTask.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkPatientFormAutoCompleteTask.Size = New System.Drawing.Size(15, 14)
        Me.chkPatientFormAutoCompleteTask.TabIndex = 18
        Me.chkPatientFormAutoCompleteTask.UseVisualStyleBackColor = True
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.TopToolStrip)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel1.Location = New System.Drawing.Point(0, 0)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(778, 55)
        Me.panel1.TabIndex = 3
        '
        'TopToolStrip
        '
        Me.TopToolStrip.BackColor = System.Drawing.Color.Transparent
        Me.TopToolStrip.BackgroundImage = CType(resources.GetObject("TopToolStrip.BackgroundImage"), System.Drawing.Image)
        Me.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TopToolStrip.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TopToolStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.TopToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAddLine, Me.tsb_btnRemoveLine, Me.tsb_Saveclose, Me.ts_btnClose})
        Me.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.TopToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.TopToolStrip.Name = "TopToolStrip"
        Me.TopToolStrip.Size = New System.Drawing.Size(778, 53)
        Me.TopToolStrip.TabIndex = 8
        Me.TopToolStrip.Text = "toolStrip1"
        '
        'ts_btnAddLine
        '
        Me.ts_btnAddLine.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAddLine.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnAddLine.Image = CType(resources.GetObject("ts_btnAddLine.Image"), System.Drawing.Image)
        Me.ts_btnAddLine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAddLine.Name = "ts_btnAddLine"
        Me.ts_btnAddLine.Size = New System.Drawing.Size(65, 50)
        Me.ts_btnAddLine.Tag = "AddLine"
        Me.ts_btnAddLine.Text = "&Add Line"
        Me.ts_btnAddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsb_btnRemoveLine
        '
        Me.tsb_btnRemoveLine.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_btnRemoveLine.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsb_btnRemoveLine.Image = CType(resources.GetObject("tsb_btnRemoveLine.Image"), System.Drawing.Image)
        Me.tsb_btnRemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_btnRemoveLine.Name = "tsb_btnRemoveLine"
        Me.tsb_btnRemoveLine.Size = New System.Drawing.Size(89, 50)
        Me.tsb_btnRemoveLine.Tag = "RemoveLine"
        Me.tsb_btnRemoveLine.Text = "Re&move Line"
        Me.tsb_btnRemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsb_Saveclose
        '
        Me.tsb_Saveclose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Saveclose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsb_Saveclose.Image = CType(resources.GetObject("tsb_Saveclose.Image"), System.Drawing.Image)
        Me.tsb_Saveclose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Saveclose.Name = "tsb_Saveclose"
        Me.tsb_Saveclose.Size = New System.Drawing.Size(66, 50)
        Me.tsb_Saveclose.Tag = "SaveFeeSchedule"
        Me.tsb_Saveclose.Text = "Sa&ve&&Cls"
        Me.tsb_Saveclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_Saveclose.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnClose.ToolTipText = "Close"
        '
        'pnlDetails
        '
        Me.pnlDetails.Controls.Add(Me.Tb_Messages)
        Me.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetails.Location = New System.Drawing.Point(0, 55)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDetails.Size = New System.Drawing.Size(778, 512)
        Me.pnlDetails.TabIndex = 337
        Me.pnlDetails.TabStop = True
        '
        'imgTreeVIew
        '
        Me.imgTreeVIew.ImageStream = CType(resources.GetObject("imgTreeVIew.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeVIew.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeVIew.Images.SetKeyName(0, "Browse.ico")
        '
        'frmIntuitMessageMapping_Portal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(778, 567)
        Me.Controls.Add(Me.pnlDetails)
        Me.Controls.Add(Me.panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIntuitMessageMapping_Portal"
        Me.Text = "Patient Portal Task Mapping"
        Me.Tb_Messages.ResumeLayout(False)
        Me.TbPg_AppointmentRequest.ResumeLayout(False)
        Me.TbPg_AppointmentRequest.PerformLayout()
        Me.pnlApptDefaultUser.ResumeLayout(False)
        CType(Me.c1AppointmentRequest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TbPg_RxRenewal.ResumeLayout(False)
        Me.TbPg_RxRenewal.PerformLayout()
        Me.pnlRxDefaultUser.ResumeLayout(False)
        CType(Me.C1RxRenewal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TbPg_BillPay.ResumeLayout(False)
        Me.TbPg_BillPay.PerformLayout()
        Me.pnlBillPayDefaultUser.ResumeLayout(False)
        CType(Me.C1BillPay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TbPg_PortalUsers.ResumeLayout(False)
        Me.TbPg_PortalUsers.PerformLayout()
        Me.pnlPortalDefaultUser.ResumeLayout(False)
        CType(Me.C1PortalUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAutoCompleteTasK.ResumeLayout(False)
        Me.pnlAutoCompleteTasK.PerformLayout()
        Me.TbPg_OnlinePatientForm.ResumeLayout(False)
        Me.TbPg_OnlinePatientForm.PerformLayout()
        Me.pnlPFEnableTaskNotification.ResumeLayout(False)
        Me.pnlPFEnableTaskNotification.PerformLayout()
        Me.pnlPatientFormDefaultUser.ResumeLayout(False)
        CType(Me.C1PatientFormUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPatientFormAutoCompleteTasK.ResumeLayout(False)
        Me.pnlPatientFormAutoCompleteTasK.PerformLayout()
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.TopToolStrip.ResumeLayout(False)
        Me.TopToolStrip.PerformLayout()
        Me.pnlDetails.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tb_Messages As System.Windows.Forms.TabControl
    Friend WithEvents TbPg_AppointmentRequest As System.Windows.Forms.TabPage
    Friend WithEvents TbPg_RxRenewal As System.Windows.Forms.TabPage
    Friend WithEvents TbPg_BillPay As System.Windows.Forms.TabPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnApptDown As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbApptUser As System.Windows.Forms.ComboBox
    Friend WithEvents btnApptUp As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents c1AppointmentRequest As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents TopToolStrip As System.Windows.Forms.ToolStrip
    Private WithEvents ts_btnAddLine As System.Windows.Forms.ToolStripButton
    Private WithEvents tsb_btnRemoveLine As System.Windows.Forms.ToolStripButton
    Private WithEvents tsb_Saveclose As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlDetails As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnRxDown As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbRxUser As System.Windows.Forms.ComboBox
    Friend WithEvents btnRxUp As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents C1RxRenewal As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents btnBillDown As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbBillPayUser As System.Windows.Forms.ComboBox
    Friend WithEvents btnBillUp As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents C1BillPay As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents imgTreeVIew As System.Windows.Forms.ImageList
    Friend WithEvents TbPg_PortalUsers As System.Windows.Forms.TabPage
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnPortalDown As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents cmbPortalUsers As System.Windows.Forms.ComboBox
    Friend WithEvents btnPortalUp As System.Windows.Forms.Button
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents C1PortalUsers As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents chkAutoCompleteTask As System.Windows.Forms.CheckBox
    Friend WithEvents pnlAutoCompleteTasK As System.Windows.Forms.Panel
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents cmbApptDefaultUser As System.Windows.Forms.ComboBox
    Friend WithEvents btnClearApptUsers As System.Windows.Forms.Button
    Friend WithEvents btnSearchApptUser As System.Windows.Forms.Button
    Friend WithEvents pnlApptDefaultUser As System.Windows.Forms.Panel
    Friend WithEvents pnlRxDefaultUser As System.Windows.Forms.Panel
    Private WithEvents cmbRxDefaultUser As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearchRxUser As System.Windows.Forms.Button
    Friend WithEvents btnClearRxUsers As System.Windows.Forms.Button
    Friend WithEvents pnlBillPayDefaultUser As System.Windows.Forms.Panel
    Private WithEvents cmbBillPayDefaultUser As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearchBillPayUser As System.Windows.Forms.Button
    Friend WithEvents btnClearBillPayUsers As System.Windows.Forms.Button
    Friend WithEvents pnlPortalDefaultUser As System.Windows.Forms.Panel
    Private WithEvents cmbPortalDefaultUser As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearchPortalUser As System.Windows.Forms.Button
    Friend WithEvents btnClearPortalUsers As System.Windows.Forms.Button
    Friend WithEvents TbPg_OnlinePatientForm As System.Windows.Forms.TabPage
    Friend WithEvents pnlPatientFormDefaultUser As System.Windows.Forms.Panel
    Private WithEvents cmbPatientFormDefaultUser As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearchPatientFormUser As System.Windows.Forms.Button
    Friend WithEvents btnClearPatientFormUsers As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents btnPatientFormDown As System.Windows.Forms.Button
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents cmbPatientFormUsers As System.Windows.Forms.ComboBox
    Friend WithEvents btnPatientFormUp As System.Windows.Forms.Button
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents C1PatientFormUsers As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlPatientFormAutoCompleteTasK As System.Windows.Forms.Panel
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents chkPatientFormAutoCompleteTask As System.Windows.Forms.CheckBox
    Friend WithEvents pnlPFEnableTaskNotification As System.Windows.Forms.Panel
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents chkPFEnableTaskNotification As System.Windows.Forms.CheckBox
End Class
