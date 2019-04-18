<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDeviceActivation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDeviceActivation))
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.btnOK = New System.Windows.Forms.ToolStripButton()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.PnlSpirometryInterface = New System.Windows.Forms.Panel()
        Me.txtPrefixForSpirometryDevice = New System.Windows.Forms.TextBox()
        Me.lblPrefixForSpirometryDevice = New System.Windows.Forms.Label()
        Me.PnlVitalDeviceInterface = New System.Windows.Forms.Panel()
        Me.nup_NoofAttemptstoConnectVitalDevice = New System.Windows.Forms.NumericUpDown()
        Me.lblNoOfAttemptsToConnectVitalDevice = New System.Windows.Forms.Label()
        Me.pnlECGInterface = New System.Windows.Forms.Panel()
        Me.txtECGUserProviderId = New System.Windows.Forms.TextBox()
        Me.Label717 = New System.Windows.Forms.Label()
        Me.txtECGInterfaceUrl = New System.Windows.Forms.TextBox()
        Me.Label716 = New System.Windows.Forms.Label()
        Me.txtECGInterfaceId = New System.Windows.Forms.TextBox()
        Me.Label699 = New System.Windows.Forms.Label()
        Me.PnlDeviceName = New System.Windows.Forms.Panel()
        Me.rbPatientPortal = New System.Windows.Forms.RadioButton()
        Me.rbIntuitPortal = New System.Windows.Forms.RadioButton()
        Me.txtDeviceActivationKey = New System.Windows.Forms.TextBox()
        Me.Label729 = New System.Windows.Forms.Label()
        Me.Label730 = New System.Windows.Forms.Label()
        Me.lblAUSUserName = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblDeviceName = New System.Windows.Forms.Label()
        Me.btnPatientPortalTaskUserDelete = New System.Windows.Forms.Button()
        Me.btnPatientPortalTaskUserSearch = New System.Windows.Forms.Button()
        Me.lblPatientPortalDefaultUserTask = New System.Windows.Forms.Label()
        Me.txtPatientPortalTask_DefaultUser = New System.Windows.Forms.TextBox()
        Me.pnlPatientPortal = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlMu2Portal = New System.Windows.Forms.Panel()
        Me.chkOnlinePayment = New System.Windows.Forms.CheckBox()
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath = New System.Windows.Forms.Button()
        Me.txtPatientPortalgloCoreServicesInstallationPath = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPatientPortalEmailService = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPortalSiteNm = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRegistrationKey = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtMerchantId = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.fbdPatientPortalgloCoreServicesInstallationPath = New System.Windows.Forms.FolderBrowserDialog()
        Me.pnlOnlinePayment = New System.Windows.Forms.Panel()
        Me.tstrip.SuspendLayout()
        Me.PnlSpirometryInterface.SuspendLayout()
        Me.PnlVitalDeviceInterface.SuspendLayout()
        CType(Me.nup_NoofAttemptstoConnectVitalDevice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlECGInterface.SuspendLayout()
        Me.PnlDeviceName.SuspendLayout()
        Me.pnlPatientPortal.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlMu2Portal.SuspendLayout()
        Me.pnlOnlinePayment.SuspendLayout()
        Me.SuspendLayout()
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK, Me.btnCancel})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(630, 53)
        Me.tstrip.TabIndex = 2
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(62, 50)
        Me.btnOK.Text = "&Activate"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOK.ToolTipText = "Activate"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.btnCancel.Text = "&Close"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.ToolTipText = "Close"
        '
        'PnlSpirometryInterface
        '
        Me.PnlSpirometryInterface.Controls.Add(Me.txtPrefixForSpirometryDevice)
        Me.PnlSpirometryInterface.Controls.Add(Me.lblPrefixForSpirometryDevice)
        Me.PnlSpirometryInterface.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlSpirometryInterface.Location = New System.Drawing.Point(1, 268)
        Me.PnlSpirometryInterface.Name = "PnlSpirometryInterface"
        Me.PnlSpirometryInterface.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.PnlSpirometryInterface.Size = New System.Drawing.Size(628, 32)
        Me.PnlSpirometryInterface.TabIndex = 3
        '
        'txtPrefixForSpirometryDevice
        '
        Me.txtPrefixForSpirometryDevice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPrefixForSpirometryDevice.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrefixForSpirometryDevice.Location = New System.Drawing.Point(220, 4)
        Me.txtPrefixForSpirometryDevice.MaxLength = 5
        Me.txtPrefixForSpirometryDevice.Name = "txtPrefixForSpirometryDevice"
        Me.txtPrefixForSpirometryDevice.ShortcutsEnabled = False
        Me.txtPrefixForSpirometryDevice.Size = New System.Drawing.Size(53, 22)
        Me.txtPrefixForSpirometryDevice.TabIndex = 0
        Me.txtPrefixForSpirometryDevice.Text = "SPI"
        '
        'lblPrefixForSpirometryDevice
        '
        Me.lblPrefixForSpirometryDevice.AutoSize = True
        Me.lblPrefixForSpirometryDevice.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrefixForSpirometryDevice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPrefixForSpirometryDevice.Location = New System.Drawing.Point(50, 8)
        Me.lblPrefixForSpirometryDevice.Name = "lblPrefixForSpirometryDevice"
        Me.lblPrefixForSpirometryDevice.Size = New System.Drawing.Size(166, 14)
        Me.lblPrefixForSpirometryDevice.TabIndex = 67
        Me.lblPrefixForSpirometryDevice.Text = "Prefix for Spirometry device :"
        '
        'PnlVitalDeviceInterface
        '
        Me.PnlVitalDeviceInterface.Controls.Add(Me.nup_NoofAttemptstoConnectVitalDevice)
        Me.PnlVitalDeviceInterface.Controls.Add(Me.lblNoOfAttemptsToConnectVitalDevice)
        Me.PnlVitalDeviceInterface.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlVitalDeviceInterface.Location = New System.Drawing.Point(1, 236)
        Me.PnlVitalDeviceInterface.Name = "PnlVitalDeviceInterface"
        Me.PnlVitalDeviceInterface.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.PnlVitalDeviceInterface.Size = New System.Drawing.Size(628, 32)
        Me.PnlVitalDeviceInterface.TabIndex = 2
        '
        'nup_NoofAttemptstoConnectVitalDevice
        '
        Me.nup_NoofAttemptstoConnectVitalDevice.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nup_NoofAttemptstoConnectVitalDevice.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nup_NoofAttemptstoConnectVitalDevice.Location = New System.Drawing.Point(267, 5)
        Me.nup_NoofAttemptstoConnectVitalDevice.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.nup_NoofAttemptstoConnectVitalDevice.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nup_NoofAttemptstoConnectVitalDevice.Name = "nup_NoofAttemptstoConnectVitalDevice"
        Me.nup_NoofAttemptstoConnectVitalDevice.Size = New System.Drawing.Size(50, 22)
        Me.nup_NoofAttemptstoConnectVitalDevice.TabIndex = 0
        Me.nup_NoofAttemptstoConnectVitalDevice.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblNoOfAttemptsToConnectVitalDevice
        '
        Me.lblNoOfAttemptsToConnectVitalDevice.AutoSize = True
        Me.lblNoOfAttemptsToConnectVitalDevice.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoOfAttemptsToConnectVitalDevice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblNoOfAttemptsToConnectVitalDevice.Location = New System.Drawing.Point(31, 9)
        Me.lblNoOfAttemptsToConnectVitalDevice.Name = "lblNoOfAttemptsToConnectVitalDevice"
        Me.lblNoOfAttemptsToConnectVitalDevice.Size = New System.Drawing.Size(234, 14)
        Me.lblNoOfAttemptsToConnectVitalDevice.TabIndex = 71
        Me.lblNoOfAttemptsToConnectVitalDevice.Text = "No. of attempts to connect Vital device :"
        '
        'pnlECGInterface
        '
        Me.pnlECGInterface.Controls.Add(Me.txtECGUserProviderId)
        Me.pnlECGInterface.Controls.Add(Me.Label717)
        Me.pnlECGInterface.Controls.Add(Me.txtECGInterfaceUrl)
        Me.pnlECGInterface.Controls.Add(Me.Label716)
        Me.pnlECGInterface.Controls.Add(Me.txtECGInterfaceId)
        Me.pnlECGInterface.Controls.Add(Me.Label699)
        Me.pnlECGInterface.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlECGInterface.Location = New System.Drawing.Point(1, 141)
        Me.pnlECGInterface.Name = "pnlECGInterface"
        Me.pnlECGInterface.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlECGInterface.Size = New System.Drawing.Size(628, 95)
        Me.pnlECGInterface.TabIndex = 1
        '
        'txtECGUserProviderId
        '
        Me.txtECGUserProviderId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtECGUserProviderId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtECGUserProviderId.ForeColor = System.Drawing.Color.Black
        Me.txtECGUserProviderId.Location = New System.Drawing.Point(220, 65)
        Me.txtECGUserProviderId.MaxLength = 5000
        Me.txtECGUserProviderId.Name = "txtECGUserProviderId"
        Me.txtECGUserProviderId.Size = New System.Drawing.Size(372, 22)
        Me.txtECGUserProviderId.TabIndex = 3
        '
        'Label717
        '
        Me.Label717.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label717.AutoSize = True
        Me.Label717.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label717.Location = New System.Drawing.Point(54, 69)
        Me.Label717.MaximumSize = New System.Drawing.Size(162, 14)
        Me.Label717.MinimumSize = New System.Drawing.Size(162, 14)
        Me.Label717.Name = "Label717"
        Me.Label717.Size = New System.Drawing.Size(162, 14)
        Me.Label717.TabIndex = 21
        Me.Label717.Text = "ECG User Provider ID :"
        Me.Label717.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtECGInterfaceUrl
        '
        Me.txtECGInterfaceUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtECGInterfaceUrl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtECGInterfaceUrl.ForeColor = System.Drawing.Color.Black
        Me.txtECGInterfaceUrl.Location = New System.Drawing.Point(220, 35)
        Me.txtECGInterfaceUrl.MaxLength = 5000
        Me.txtECGInterfaceUrl.Name = "txtECGInterfaceUrl"
        Me.txtECGInterfaceUrl.Size = New System.Drawing.Size(372, 22)
        Me.txtECGInterfaceUrl.TabIndex = 1
        '
        'Label716
        '
        Me.Label716.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label716.AutoSize = True
        Me.Label716.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label716.Location = New System.Drawing.Point(54, 39)
        Me.Label716.MaximumSize = New System.Drawing.Size(162, 14)
        Me.Label716.MinimumSize = New System.Drawing.Size(162, 14)
        Me.Label716.Name = "Label716"
        Me.Label716.Size = New System.Drawing.Size(162, 14)
        Me.Label716.TabIndex = 19
        Me.Label716.Text = "ECG Interface URL :"
        Me.Label716.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtECGInterfaceId
        '
        Me.txtECGInterfaceId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtECGInterfaceId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtECGInterfaceId.ForeColor = System.Drawing.Color.Black
        Me.txtECGInterfaceId.Location = New System.Drawing.Point(220, 5)
        Me.txtECGInterfaceId.MaxLength = 5000
        Me.txtECGInterfaceId.Name = "txtECGInterfaceId"
        Me.txtECGInterfaceId.Size = New System.Drawing.Size(372, 22)
        Me.txtECGInterfaceId.TabIndex = 0
        '
        'Label699
        '
        Me.Label699.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label699.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label699.Location = New System.Drawing.Point(16, 9)
        Me.Label699.MaximumSize = New System.Drawing.Size(200, 14)
        Me.Label699.MinimumSize = New System.Drawing.Size(200, 14)
        Me.Label699.Name = "Label699"
        Me.Label699.Size = New System.Drawing.Size(200, 14)
        Me.Label699.TabIndex = 17
        Me.Label699.Text = "ECG Interface Institution ID :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label699.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PnlDeviceName
        '
        Me.PnlDeviceName.Controls.Add(Me.rbPatientPortal)
        Me.PnlDeviceName.Controls.Add(Me.rbIntuitPortal)
        Me.PnlDeviceName.Controls.Add(Me.txtDeviceActivationKey)
        Me.PnlDeviceName.Controls.Add(Me.Label729)
        Me.PnlDeviceName.Controls.Add(Me.Label730)
        Me.PnlDeviceName.Controls.Add(Me.lblAUSUserName)
        Me.PnlDeviceName.Controls.Add(Me.Label5)
        Me.PnlDeviceName.Controls.Add(Me.lblDeviceName)
        Me.PnlDeviceName.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlDeviceName.Location = New System.Drawing.Point(1, 56)
        Me.PnlDeviceName.Name = "PnlDeviceName"
        Me.PnlDeviceName.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.PnlDeviceName.Size = New System.Drawing.Size(628, 85)
        Me.PnlDeviceName.TabIndex = 0
        '
        'rbPatientPortal
        '
        Me.rbPatientPortal.AutoSize = True
        Me.rbPatientPortal.Location = New System.Drawing.Point(325, 10)
        Me.rbPatientPortal.Name = "rbPatientPortal"
        Me.rbPatientPortal.Size = New System.Drawing.Size(99, 18)
        Me.rbPatientPortal.TabIndex = 1
        Me.rbPatientPortal.TabStop = True
        Me.rbPatientPortal.Text = "Patient Portal"
        Me.rbPatientPortal.UseVisualStyleBackColor = True
        '
        'rbIntuitPortal
        '
        Me.rbIntuitPortal.AutoSize = True
        Me.rbIntuitPortal.Location = New System.Drawing.Point(219, 10)
        Me.rbIntuitPortal.Name = "rbIntuitPortal"
        Me.rbIntuitPortal.Size = New System.Drawing.Size(55, 18)
        Me.rbIntuitPortal.TabIndex = 0
        Me.rbIntuitPortal.TabStop = True
        Me.rbIntuitPortal.Text = "Intuit"
        Me.rbIntuitPortal.UseVisualStyleBackColor = True
        '
        'txtDeviceActivationKey
        '
        Me.txtDeviceActivationKey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDeviceActivationKey.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeviceActivationKey.Location = New System.Drawing.Point(220, 60)
        Me.txtDeviceActivationKey.Name = "txtDeviceActivationKey"
        Me.txtDeviceActivationKey.Size = New System.Drawing.Size(372, 22)
        Me.txtDeviceActivationKey.TabIndex = 3
        '
        'Label729
        '
        Me.Label729.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label729.AutoSize = True
        Me.Label729.BackColor = System.Drawing.Color.Transparent
        Me.Label729.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label729.Location = New System.Drawing.Point(54, 37)
        Me.Label729.MaximumSize = New System.Drawing.Size(162, 14)
        Me.Label729.MinimumSize = New System.Drawing.Size(162, 14)
        Me.Label729.Name = "Label729"
        Me.Label729.Size = New System.Drawing.Size(162, 14)
        Me.Label729.TabIndex = 51
        Me.Label729.Text = "AUS User Name :"
        Me.Label729.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label730
        '
        Me.Label730.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label730.AutoSize = True
        Me.Label730.BackColor = System.Drawing.Color.Transparent
        Me.Label730.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label730.Location = New System.Drawing.Point(54, 64)
        Me.Label730.MaximumSize = New System.Drawing.Size(162, 14)
        Me.Label730.MinimumSize = New System.Drawing.Size(162, 14)
        Me.Label730.Name = "Label730"
        Me.Label730.Size = New System.Drawing.Size(162, 14)
        Me.Label730.TabIndex = 53
        Me.Label730.Text = "Authorization Key :"
        Me.Label730.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAUSUserName
        '
        Me.lblAUSUserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblAUSUserName.AutoSize = True
        Me.lblAUSUserName.BackColor = System.Drawing.Color.Transparent
        Me.lblAUSUserName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAUSUserName.Location = New System.Drawing.Point(219, 37)
        Me.lblAUSUserName.Name = "lblAUSUserName"
        Me.lblAUSUserName.Size = New System.Drawing.Size(93, 14)
        Me.lblAUSUserName.TabIndex = 55
        Me.lblAUSUserName.Text = "AUS User Name"
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(54, 12)
        Me.Label5.MaximumSize = New System.Drawing.Size(162, 14)
        Me.Label5.MinimumSize = New System.Drawing.Size(162, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(162, 14)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "Interface :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDeviceName
        '
        Me.lblDeviceName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDeviceName.AutoSize = True
        Me.lblDeviceName.BackColor = System.Drawing.Color.Transparent
        Me.lblDeviceName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeviceName.Location = New System.Drawing.Point(219, 12)
        Me.lblDeviceName.Name = "lblDeviceName"
        Me.lblDeviceName.Size = New System.Drawing.Size(84, 14)
        Me.lblDeviceName.TabIndex = 0
        Me.lblDeviceName.Text = "lblDevicename"
        '
        'btnPatientPortalTaskUserDelete
        '
        Me.btnPatientPortalTaskUserDelete.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnPatientPortalTaskUserDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPatientPortalTaskUserDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnPatientPortalTaskUserDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnPatientPortalTaskUserDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPatientPortalTaskUserDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPatientPortalTaskUserDelete.Image = CType(resources.GetObject("btnPatientPortalTaskUserDelete.Image"), System.Drawing.Image)
        Me.btnPatientPortalTaskUserDelete.Location = New System.Drawing.Point(501, 9)
        Me.btnPatientPortalTaskUserDelete.Name = "btnPatientPortalTaskUserDelete"
        Me.btnPatientPortalTaskUserDelete.Size = New System.Drawing.Size(21, 21)
        Me.btnPatientPortalTaskUserDelete.TabIndex = 5
        Me.btnPatientPortalTaskUserDelete.UseVisualStyleBackColor = True
        '
        'btnPatientPortalTaskUserSearch
        '
        Me.btnPatientPortalTaskUserSearch.BackgroundImage = CType(resources.GetObject("btnPatientPortalTaskUserSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnPatientPortalTaskUserSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPatientPortalTaskUserSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPatientPortalTaskUserSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPatientPortalTaskUserSearch.Image = CType(resources.GetObject("btnPatientPortalTaskUserSearch.Image"), System.Drawing.Image)
        Me.btnPatientPortalTaskUserSearch.Location = New System.Drawing.Point(477, 9)
        Me.btnPatientPortalTaskUserSearch.Name = "btnPatientPortalTaskUserSearch"
        Me.btnPatientPortalTaskUserSearch.Size = New System.Drawing.Size(21, 21)
        Me.btnPatientPortalTaskUserSearch.TabIndex = 4
        '
        'lblPatientPortalDefaultUserTask
        '
        Me.lblPatientPortalDefaultUserTask.AutoSize = True
        Me.lblPatientPortalDefaultUserTask.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientPortalDefaultUserTask.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPatientPortalDefaultUserTask.Location = New System.Drawing.Point(6, 12)
        Me.lblPatientPortalDefaultUserTask.Name = "lblPatientPortalDefaultUserTask"
        Me.lblPatientPortalDefaultUserTask.Size = New System.Drawing.Size(250, 14)
        Me.lblPatientPortalDefaultUserTask.TabIndex = 136
        Me.lblPatientPortalDefaultUserTask.Text = "Default User to Review Patient Portal tasks :"
        '
        'txtPatientPortalTask_DefaultUser
        '
        Me.txtPatientPortalTask_DefaultUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPatientPortalTask_DefaultUser.ForeColor = System.Drawing.Color.Black
        Me.txtPatientPortalTask_DefaultUser.Location = New System.Drawing.Point(259, 8)
        Me.txtPatientPortalTask_DefaultUser.Name = "txtPatientPortalTask_DefaultUser"
        Me.txtPatientPortalTask_DefaultUser.ReadOnly = True
        Me.txtPatientPortalTask_DefaultUser.Size = New System.Drawing.Size(213, 22)
        Me.txtPatientPortalTask_DefaultUser.TabIndex = 400
        '
        'pnlPatientPortal
        '
        Me.pnlPatientPortal.Controls.Add(Me.btnPatientPortalTaskUserDelete)
        Me.pnlPatientPortal.Controls.Add(Me.lblPatientPortalDefaultUserTask)
        Me.pnlPatientPortal.Controls.Add(Me.txtPatientPortalTask_DefaultUser)
        Me.pnlPatientPortal.Controls.Add(Me.btnPatientPortalTaskUserSearch)
        Me.pnlPatientPortal.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientPortal.Location = New System.Drawing.Point(1, 420)
        Me.pnlPatientPortal.Name = "pnlPatientPortal"
        Me.pnlPatientPortal.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlPatientPortal.Size = New System.Drawing.Size(628, 42)
        Me.pnlPatientPortal.TabIndex = 137
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tstrip)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(630, 55)
        Me.Panel1.TabIndex = 58
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(0, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 418)
        Me.Label4.TabIndex = 402
        Me.Label4.Text = " "
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Location = New System.Drawing.Point(629, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 418)
        Me.Label9.TabIndex = 403
        Me.Label9.Text = " "
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Location = New System.Drawing.Point(0, 55)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(630, 1)
        Me.Label17.TabIndex = 404
        Me.Label17.Text = " "
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Location = New System.Drawing.Point(1, 473)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(628, 1)
        Me.Label10.TabIndex = 405
        Me.Label10.Text = " "
        '
        'pnlMu2Portal
        '
        Me.pnlMu2Portal.Controls.Add(Me.chkOnlinePayment)
        Me.pnlMu2Portal.Controls.Add(Me.btnBrowsePatientPortalgloCoreServicesInstallationPath)
        Me.pnlMu2Portal.Controls.Add(Me.txtPatientPortalgloCoreServicesInstallationPath)
        Me.pnlMu2Portal.Controls.Add(Me.Label3)
        Me.pnlMu2Portal.Controls.Add(Me.txtPatientPortalEmailService)
        Me.pnlMu2Portal.Controls.Add(Me.Label2)
        Me.pnlMu2Portal.Controls.Add(Me.txtPortalSiteNm)
        Me.pnlMu2Portal.Controls.Add(Me.Label1)
        Me.pnlMu2Portal.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMu2Portal.Location = New System.Drawing.Point(1, 300)
        Me.pnlMu2Portal.Name = "pnlMu2Portal"
        Me.pnlMu2Portal.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlMu2Portal.Size = New System.Drawing.Size(628, 58)
        Me.pnlMu2Portal.TabIndex = 4
        Me.pnlMu2Portal.Visible = False
        '
        'chkOnlinePayment
        '
        Me.chkOnlinePayment.AutoSize = True
        Me.chkOnlinePayment.Location = New System.Drawing.Point(222, 33)
        Me.chkOnlinePayment.Name = "chkOnlinePayment"
        Me.chkOnlinePayment.Size = New System.Drawing.Size(112, 18)
        Me.chkOnlinePayment.TabIndex = 99
        Me.chkOnlinePayment.Text = "Online Payment"
        Me.chkOnlinePayment.UseVisualStyleBackColor = True
        '
        'btnBrowsePatientPortalgloCoreServicesInstallationPath
        '
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.Image = CType(resources.GetObject("btnBrowsePatientPortalgloCoreServicesInstallationPath.Image"), System.Drawing.Image)
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.Location = New System.Drawing.Point(597, 154)
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.Name = "btnBrowsePatientPortalgloCoreServicesInstallationPath"
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.Size = New System.Drawing.Size(24, 24)
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btnBrowsePatientPortalgloCoreServicesInstallationPath, "Browse gloCore Services  Installation Path")
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.UseVisualStyleBackColor = True
        Me.btnBrowsePatientPortalgloCoreServicesInstallationPath.Visible = False
        '
        'txtPatientPortalgloCoreServicesInstallationPath
        '
        Me.txtPatientPortalgloCoreServicesInstallationPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPatientPortalgloCoreServicesInstallationPath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPatientPortalgloCoreServicesInstallationPath.ForeColor = System.Drawing.Color.Black
        Me.txtPatientPortalgloCoreServicesInstallationPath.Location = New System.Drawing.Point(222, 155)
        Me.txtPatientPortalgloCoreServicesInstallationPath.MaxLength = 5000
        Me.txtPatientPortalgloCoreServicesInstallationPath.Name = "txtPatientPortalgloCoreServicesInstallationPath"
        Me.txtPatientPortalgloCoreServicesInstallationPath.Size = New System.Drawing.Size(372, 22)
        Me.txtPatientPortalgloCoreServicesInstallationPath.TabIndex = 4
        Me.txtPatientPortalgloCoreServicesInstallationPath.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(22, 159)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(195, 14)
        Me.Label3.TabIndex = 98
        Me.Label3.Text = "gloCore Services Installation Path :"
        Me.Label3.Visible = False
        '
        'txtPatientPortalEmailService
        '
        Me.txtPatientPortalEmailService.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPatientPortalEmailService.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPatientPortalEmailService.ForeColor = System.Drawing.Color.Black
        Me.txtPatientPortalEmailService.Location = New System.Drawing.Point(222, 127)
        Me.txtPatientPortalEmailService.MaxLength = 5000
        Me.txtPatientPortalEmailService.Name = "txtPatientPortalEmailService"
        Me.txtPatientPortalEmailService.Size = New System.Drawing.Size(372, 22)
        Me.txtPatientPortalEmailService.TabIndex = 3
        Me.txtPatientPortalEmailService.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(54, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(163, 14)
        Me.Label2.TabIndex = 69
        Me.Label2.Text = "Patient Portal Email Service :"
        Me.Label2.Visible = False
        '
        'txtPortalSiteNm
        '
        Me.txtPortalSiteNm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPortalSiteNm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPortalSiteNm.ForeColor = System.Drawing.Color.Black
        Me.txtPortalSiteNm.Location = New System.Drawing.Point(222, 4)
        Me.txtPortalSiteNm.MaxLength = 5000
        Me.txtPortalSiteNm.Name = "txtPortalSiteNm"
        Me.txtPortalSiteNm.Size = New System.Drawing.Size(372, 22)
        Me.txtPortalSiteNm.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(68, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 14)
        Me.Label1.TabIndex = 67
        Me.Label1.Text = "Patient Portal Site Name :"
        '
        'txtRegistrationKey
        '
        Me.txtRegistrationKey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegistrationKey.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegistrationKey.ForeColor = System.Drawing.Color.Black
        Me.txtRegistrationKey.Location = New System.Drawing.Point(219, 34)
        Me.txtRegistrationKey.MaxLength = 50
        Me.txtRegistrationKey.Name = "txtRegistrationKey"
        Me.txtRegistrationKey.Size = New System.Drawing.Size(372, 22)
        Me.txtRegistrationKey.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(110, 37)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(103, 14)
        Me.Label8.TabIndex = 108
        Me.Label8.Text = "Registration Key :"
        '
        'txtMerchantId
        '
        Me.txtMerchantId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtMerchantId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMerchantId.ForeColor = System.Drawing.Color.Black
        Me.txtMerchantId.Location = New System.Drawing.Point(219, 6)
        Me.txtMerchantId.MaxLength = 50
        Me.txtMerchantId.Name = "txtMerchantId"
        Me.txtMerchantId.Size = New System.Drawing.Size(372, 22)
        Me.txtMerchantId.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(132, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(81, 14)
        Me.Label11.TabIndex = 106
        Me.Label11.Text = "Merchant Id :"
        '
        'pnlOnlinePayment
        '
        Me.pnlOnlinePayment.Controls.Add(Me.txtRegistrationKey)
        Me.pnlOnlinePayment.Controls.Add(Me.Label8)
        Me.pnlOnlinePayment.Controls.Add(Me.Label11)
        Me.pnlOnlinePayment.Controls.Add(Me.txtMerchantId)
        Me.pnlOnlinePayment.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlOnlinePayment.Location = New System.Drawing.Point(1, 358)
        Me.pnlOnlinePayment.Name = "pnlOnlinePayment"
        Me.pnlOnlinePayment.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlOnlinePayment.Size = New System.Drawing.Size(628, 62)
        Me.pnlOnlinePayment.TabIndex = 406
        Me.pnlOnlinePayment.Visible = False
        '
        'FrmDeviceActivation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(630, 474)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.pnlPatientPortal)
        Me.Controls.Add(Me.pnlOnlinePayment)
        Me.Controls.Add(Me.pnlMu2Portal)
        Me.Controls.Add(Me.PnlSpirometryInterface)
        Me.Controls.Add(Me.PnlVitalDeviceInterface)
        Me.Controls.Add(Me.pnlECGInterface)
        Me.Controls.Add(Me.PnlDeviceName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDeviceActivation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Device Activation"
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.PnlSpirometryInterface.ResumeLayout(False)
        Me.PnlSpirometryInterface.PerformLayout()
        Me.PnlVitalDeviceInterface.ResumeLayout(False)
        Me.PnlVitalDeviceInterface.PerformLayout()
        CType(Me.nup_NoofAttemptstoConnectVitalDevice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlECGInterface.ResumeLayout(False)
        Me.pnlECGInterface.PerformLayout()
        Me.PnlDeviceName.ResumeLayout(False)
        Me.PnlDeviceName.PerformLayout()
        Me.pnlPatientPortal.ResumeLayout(False)
        Me.pnlPatientPortal.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlMu2Portal.ResumeLayout(False)
        Me.pnlMu2Portal.PerformLayout()
        Me.pnlOnlinePayment.ResumeLayout(False)
        Me.pnlOnlinePayment.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents PnlSpirometryInterface As System.Windows.Forms.Panel
    Friend WithEvents txtPrefixForSpirometryDevice As System.Windows.Forms.TextBox
    Friend WithEvents lblPrefixForSpirometryDevice As System.Windows.Forms.Label
    Friend WithEvents PnlVitalDeviceInterface As System.Windows.Forms.Panel
    Friend WithEvents nup_NoofAttemptstoConnectVitalDevice As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblNoOfAttemptsToConnectVitalDevice As System.Windows.Forms.Label
    Friend WithEvents pnlECGInterface As System.Windows.Forms.Panel
    Friend WithEvents txtECGUserProviderId As System.Windows.Forms.TextBox
    Friend WithEvents Label717 As System.Windows.Forms.Label
    Friend WithEvents txtECGInterfaceUrl As System.Windows.Forms.TextBox
    Friend WithEvents Label716 As System.Windows.Forms.Label
    Friend WithEvents txtECGInterfaceId As System.Windows.Forms.TextBox
    Friend WithEvents Label699 As System.Windows.Forms.Label
    Friend WithEvents PnlDeviceName As System.Windows.Forms.Panel
    Friend WithEvents txtDeviceActivationKey As System.Windows.Forms.TextBox
    Friend WithEvents Label729 As System.Windows.Forms.Label
    Friend WithEvents Label730 As System.Windows.Forms.Label
    Friend WithEvents lblAUSUserName As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceName As System.Windows.Forms.Label
    Friend WithEvents btnPatientPortalTaskUserDelete As System.Windows.Forms.Button
    Friend WithEvents btnPatientPortalTaskUserSearch As System.Windows.Forms.Button
    Friend WithEvents lblPatientPortalDefaultUserTask As System.Windows.Forms.Label
    Friend WithEvents txtPatientPortalTask_DefaultUser As System.Windows.Forms.TextBox
    Friend WithEvents pnlPatientPortal As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents rbPatientPortal As System.Windows.Forms.RadioButton
    Friend WithEvents rbIntuitPortal As System.Windows.Forms.RadioButton
    Friend WithEvents pnlMu2Portal As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPortalSiteNm As System.Windows.Forms.TextBox
    Friend WithEvents txtPatientPortalEmailService As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnBrowsePatientPortalgloCoreServicesInstallationPath As System.Windows.Forms.Button
    Friend WithEvents txtPatientPortalgloCoreServicesInstallationPath As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents fbdPatientPortalgloCoreServicesInstallationPath As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents txtRegistrationKey As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMerchantId As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkOnlinePayment As System.Windows.Forms.CheckBox
    Friend WithEvents pnlOnlinePayment As System.Windows.Forms.Panel
End Class
