Public Class frmImplantableDeviceMaster
    Inherits System.Windows.Forms.Form

    Public _IssuingAgency As String
    Public _DeviceID As String
    
    'Sanjog
    
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents imgTreeVIew As System.Windows.Forms.ImageList
    Public _DialogResult As Windows.Forms.DialogResult = Windows.Forms.DialogResult.Cancel
    Private dtIssuingAgenecies As DataTable
    
#Region "7010 Phase II Features"
    Friend WithEvents cmbIssuingAgency As System.Windows.Forms.ComboBox
    Friend WithEvents lblHistoryType As System.Windows.Forms.Label
    Friend WithEvents chkNRL As System.Windows.Forms.CheckBox
    Friend WithEvents txtGMDNTerms As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtVersion As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtMRISafetyStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDeviceID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Private oHistListControl As gloListControl.gloListControl
#End Region


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal lnDeviceMSTID As Long, ByVal sDeviceId As String)
        MyBase.New()
        DeviceMSTId = lnDeviceMSTID
        DeviceId = sDeviceId
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBrandName As System.Windows.Forms.TextBox
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Private ImplantableDeviceDBLayer As New ClsImplantDeviceDBLayer
    Private DeviceMSTId As Long
    Private DeviceId As String
    Private errprovider As New ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImplantableDeviceMaster))
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBrandName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkNRL = New System.Windows.Forms.CheckBox()
        Me.txtGMDNTerms = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtVersion = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtMRISafetyStatus = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDeviceID = New System.Windows.Forms.TextBox()
        Me.cmbIssuingAgency = New System.Windows.Forms.ComboBox()
        Me.lblHistoryType = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.tlsp = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.imgTreeVIew = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel2.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyName.Location = New System.Drawing.Point(179, 116)
        Me.txtCompanyName.MaxLength = 255
        Me.txtCompanyName.Multiline = True
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCompanyName.Size = New System.Drawing.Size(351, 37)
        Me.txtCompanyName.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(74, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Company Name :"
        '
        'txtBrandName
        '
        Me.txtBrandName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBrandName.ForeColor = System.Drawing.Color.Black
        Me.txtBrandName.Location = New System.Drawing.Point(179, 74)
        Me.txtBrandName.MaxLength = 100
        Me.txtBrandName.Multiline = True
        Me.txtBrandName.Name = "txtBrandName"
        Me.txtBrandName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBrandName.Size = New System.Drawing.Size(351, 37)
        Me.txtBrandName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(93, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Brand Name :"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.chkNRL)
        Me.Panel2.Controls.Add(Me.txtGMDNTerms)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.txtVersion)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.txtMRISafetyStatus)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.txtDeviceID)
        Me.Panel2.Controls.Add(Me.cmbIssuingAgency)
        Me.Panel2.Controls.Add(Me.lblHistoryType)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel2.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel2.Controls.Add(Me.lbl_pnlRight)
        Me.Panel2.Controls.Add(Me.lbl_pnlTop)
        Me.Panel2.Controls.Add(Me.txtCompanyName)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtBrandName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(566, 341)
        Me.Panel2.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(193, 306)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 14)
        Me.Label4.TabIndex = 237
        Me.Label4.Text = "All fields are compulsory "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(42, 241)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(132, 14)
        Me.Label9.TabIndex = 236
        Me.Label9.Text = "Labeled Contains NRL :"
        '
        'chkNRL
        '
        Me.chkNRL.AutoSize = True
        Me.chkNRL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkNRL.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNRL.Location = New System.Drawing.Point(179, 243)
        Me.chkNRL.Name = "chkNRL"
        Me.chkNRL.Size = New System.Drawing.Size(15, 14)
        Me.chkNRL.TabIndex = 7
        Me.chkNRL.UseVisualStyleBackColor = True
        '
        'txtGMDNTerms
        '
        Me.txtGMDNTerms.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGMDNTerms.ForeColor = System.Drawing.Color.Black
        Me.txtGMDNTerms.Location = New System.Drawing.Point(179, 262)
        Me.txtGMDNTerms.MaxLength = 255
        Me.txtGMDNTerms.Multiline = True
        Me.txtGMDNTerms.Name = "txtGMDNTerms"
        Me.txtGMDNTerms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGMDNTerms.Size = New System.Drawing.Size(351, 37)
        Me.txtGMDNTerms.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(88, 265)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 14)
        Me.Label8.TabIndex = 234
        Me.Label8.Text = "GMDN Terms :"
        '
        'txtVersion
        '
        Me.txtVersion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVersion.ForeColor = System.Drawing.Color.Black
        Me.txtVersion.Location = New System.Drawing.Point(179, 158)
        Me.txtVersion.MaxLength = 80
        Me.txtVersion.Multiline = True
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtVersion.Size = New System.Drawing.Size(351, 37)
        Me.txtVersion.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(21, 161)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(153, 14)
        Me.Label7.TabIndex = 232
        Me.Label7.Text = "Version or Model Number :"
        '
        'txtMRISafetyStatus
        '
        Me.txtMRISafetyStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRISafetyStatus.ForeColor = System.Drawing.Color.Black
        Me.txtMRISafetyStatus.Location = New System.Drawing.Point(179, 200)
        Me.txtMRISafetyStatus.MaxLength = 255
        Me.txtMRISafetyStatus.Multiline = True
        Me.txtMRISafetyStatus.Name = "txtMRISafetyStatus"
        Me.txtMRISafetyStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMRISafetyStatus.Size = New System.Drawing.Size(351, 37)
        Me.txtMRISafetyStatus.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(61, 204)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(113, 14)
        Me.Label6.TabIndex = 230
        Me.Label6.Text = "MRI Safety Status :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(107, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 14)
        Me.Label5.TabIndex = 228
        Me.Label5.Text = "Device ID :"
        '
        'txtDeviceID
        '
        Me.txtDeviceID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeviceID.ForeColor = System.Drawing.Color.Black
        Me.txtDeviceID.Location = New System.Drawing.Point(179, 19)
        Me.txtDeviceID.MaxLength = 30
        Me.txtDeviceID.Name = "txtDeviceID"
        Me.txtDeviceID.Size = New System.Drawing.Size(351, 22)
        Me.txtDeviceID.TabIndex = 1
        '
        'cmbIssuingAgency
        '
        Me.cmbIssuingAgency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIssuingAgency.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbIssuingAgency.ForeColor = System.Drawing.Color.Black
        Me.cmbIssuingAgency.FormattingEnabled = True
        Me.cmbIssuingAgency.Location = New System.Drawing.Point(179, 47)
        Me.cmbIssuingAgency.Name = "cmbIssuingAgency"
        Me.cmbIssuingAgency.Size = New System.Drawing.Size(351, 22)
        Me.cmbIssuingAgency.TabIndex = 2
        '
        'lblHistoryType
        '
        Me.lblHistoryType.AutoSize = True
        Me.lblHistoryType.BackColor = System.Drawing.Color.Transparent
        Me.lblHistoryType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHistoryType.Location = New System.Drawing.Point(77, 50)
        Me.lblHistoryType.Name = "lblHistoryType"
        Me.lblHistoryType.Size = New System.Drawing.Size(97, 14)
        Me.lblHistoryType.TabIndex = 225
        Me.lblHistoryType.Text = "Issuing Agency :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(179, 306)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(13, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "*"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 337)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(558, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 334)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(562, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 334)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(560, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(566, 53)
        Me.pnl_tlsp.TabIndex = 2
        '
        'tlsp
        '
        Me.tlsp.BackColor = System.Drawing.Color.Transparent
        Me.tlsp.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp.Location = New System.Drawing.Point(0, 0)
        Me.tlsp.Name = "tlsp"
        Me.tlsp.Size = New System.Drawing.Size(566, 53)
        Me.tlsp.TabIndex = 0
        Me.tlsp.TabStop = True
        Me.tlsp.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'imgTreeVIew
        '
        Me.imgTreeVIew.ImageStream = CType(resources.GetObject("imgTreeVIew.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeVIew.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeVIew.Images.SetKeyName(0, "Small Arrow.ico")
        Me.imgTreeVIew.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgTreeVIew.Images.SetKeyName(2, "Defination.ico")
        '
        'frmImplantableDeviceMaster
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(566, 394)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImplantableDeviceMaster"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Implantable Devices"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp.ResumeLayout(False)
        Me.tlsp.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmImplantableDeviceMaster_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If Not dtIssuingAgenecies Is Nothing Then
            dtIssuingAgenecies.Dispose()
            dtIssuingAgenecies = Nothing
        End If
    End Sub

    Private Sub frmImplantableDeviceMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dtIssuingAgenecies = ImplantableDeviceDBLayer.GetStandardTypes()
            cmbIssuingAgency.DataSource = dtIssuingAgenecies
            cmbIssuingAgency.ValueMember = dtIssuingAgenecies.Columns(0).ColumnName
            cmbIssuingAgency.DisplayMember = dtIssuingAgenecies.Columns(1).ColumnName            
            If DeviceId <> "" Then
                Try
                    Dim arrlist As ArrayList
                    arrlist = ImplantableDeviceDBLayer.FetchDataForUpdate(DeviceMSTId, DeviceId)
                    If arrlist.Count > 0 Then
                        txtDeviceID.Text = CType(arrlist.Item(1), System.String)
                        cmbIssuingAgency.Text = CType(arrlist.Item(2), System.String)
                        txtBrandName.Text = CType(arrlist.Item(3), System.String)
                        txtCompanyName.Text = CType(arrlist.Item(4), System.String)
                        txtVersion.Text = CType(arrlist.Item(5), System.String)
                        txtMRISafetyStatus.Text = CType(arrlist.Item(6), System.String)
                        chkNRL.Checked = CType(arrlist.Item(7), System.Boolean)
                        txtGMDNTerms.Text = CType(arrlist.Item(8), System.String)
                    End If
                Catch ex As SqlClient.SqlException
                    MessageBox.Show(ex.Message, "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                txtBrandName.Text = ""
                txtCompanyName.Text = ""
                txtGMDNTerms.Text = ""
                txtMRISafetyStatus.Text = ""
                txtDeviceID.Text = ""
                txtVersion.Text = ""
                cmbIssuingAgency.Text = _IssuingAgency
                chkNRL.Checked = False
            End If
        Catch ex As Exception

        End Try

    End Sub

    
    Private Sub CloseHistory()    
        ImplantableDeviceDBLayer = Nothing
        Me.Close()
    End Sub

    Private Sub SaveHistory()

        Dim formclose As Boolean = True
        If Trim(txtDeviceID.Text) = "" Then
            MessageBox.Show("Device ID is Required", "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtDeviceID.Focus()
            Exit Sub
        Else
            If cmbIssuingAgency.Text = "" Then
                MessageBox.Show("Issuing Agency is Required", "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbIssuingAgency.Focus()
                Exit Sub
            Else
                If Trim(txtBrandName.Text) = "" Then
                    MessageBox.Show("Brand Name is Required", "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtBrandName.Focus()
                    Exit Sub
                Else
                    If Trim(txtCompanyName.Text) = "" Then
                        MessageBox.Show("Company Name is Required", "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtCompanyName.Focus()
                        Exit Sub
                    Else
                        If Trim(txtVersion.Text) = "" Then
                            MessageBox.Show("Verion or Model is Required", "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtVersion.Focus()
                            Exit Sub
                        Else
                            If Trim(txtMRISafetyStatus.Text) = "" Then
                                MessageBox.Show("MRI Safety Status is Required", "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                txtMRISafetyStatus.Focus()
                                Exit Sub
                            Else
                                If Not ImplantableDeviceDBLayer.ValidateDescription(DeviceMSTId, txtDeviceID.Text, cmbIssuingAgency.GetItemText(cmbIssuingAgency.SelectedItem), txtBrandName.Text, txtCompanyName.Text, txtVersion.Text) Then
                                    MessageBox.Show("Duplicate Device information", "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    txtBrandName.Focus()
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
        If DeviceMSTId = 0 Then
            Try
                ''IcdType ''parameter added for ICD10 implementation
                If ImplantableDeviceDBLayer.AddData(DeviceMSTId, txtDeviceID.Text, cmbIssuingAgency.GetItemText(cmbIssuingAgency.SelectedItem), txtBrandName.Text, txtCompanyName.Text, txtVersion.Text, txtMRISafetyStatus.Text, chkNRL.Checked, txtGMDNTerms.Text) <> 0 Then
                    _IssuingAgency = cmbIssuingAgency.GetItemText(cmbIssuingAgency.SelectedItem)
                    _DeviceID = txtDeviceID.Text
                End If

            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ImplantableDeviceDBLayer = Nothing
                _DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End Try
        Else
            Try

                ImplantableDeviceDBLayer.UpdateData(DeviceMSTId, txtDeviceID.Text, cmbIssuingAgency.GetItemText(cmbIssuingAgency.SelectedItem), txtBrandName.Text, txtCompanyName.Text, txtVersion.Text, txtMRISafetyStatus.Text, chkNRL.Checked, txtGMDNTerms.Text)
                _IssuingAgency = cmbIssuingAgency.GetItemText(cmbIssuingAgency.SelectedItem)
                _DeviceID = txtDeviceID.Text
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If formclose = True Then
                    ImplantableDeviceDBLayer = Nothing
                    _DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                End If

            End Try
        End If
    End Sub
   
    Private Sub tlsp_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp.ItemClicked
        Try
            Select Case e.ClickedItem.Tag.ToString().ToUpper()
                Case UCase("Save")
                    SaveHistory()

                Case UCase("Close")
                    CloseHistory()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub
    
End Class
