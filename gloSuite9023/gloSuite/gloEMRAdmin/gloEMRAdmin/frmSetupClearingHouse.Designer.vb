<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetupClearingHouse
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetupClearingHouse))
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel
        Me.tls = New System.Windows.Forms.ToolStrip
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnl_Base = New System.Windows.Forms.Panel
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.txt_Password = New System.Windows.Forms.TextBox
        Me.txt_Username = New System.Windows.Forms.TextBox
        Me.txt_ftpURL = New System.Windows.Forms.TextBox
        Me.label21 = New System.Windows.Forms.Label
        Me.label25 = New System.Windows.Forms.Label
        Me.label29 = New System.Windows.Forms.Label
        Me.gbClaimManagement = New System.Windows.Forms.GroupBox
        Me.txt_WorkedTransactions = New System.Windows.Forms.TextBox
        Me.txt_Statements = New System.Windows.Forms.TextBox
        Me.txt_997INAcknowledgement = New System.Windows.Forms.TextBox
        Me.txt_Reports = New System.Windows.Forms.TextBox
        Me.txt_997OUTAcknowledgement = New System.Windows.Forms.TextBox
        Me.txt_835RemittanceAdvice = New System.Windows.Forms.TextBox
        Me.txt_Letters = New System.Windows.Forms.TextBox
        Me.label15 = New System.Windows.Forms.Label
        Me.txt_837PclaimSubmission = New System.Windows.Forms.TextBox
        Me.label14 = New System.Windows.Forms.Label
        Me.txt_277ClaimStatusResponse = New System.Windows.Forms.TextBox
        Me.label7 = New System.Windows.Forms.Label
        Me.txt_276Eligibilityenquiry = New System.Windows.Forms.TextBox
        Me.txt_CSRReports = New System.Windows.Forms.TextBox
        Me.label10 = New System.Windows.Forms.Label
        Me.label13 = New System.Windows.Forms.Label
        Me.txt_271EligibilityResponse = New System.Windows.Forms.TextBox
        Me.label6 = New System.Windows.Forms.Label
        Me.label9 = New System.Windows.Forms.Label
        Me.label12 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.label8 = New System.Windows.Forms.Label
        Me.label11 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.chkISA = New System.Windows.Forms.CheckBox
        Me.chkLoop1000BNM109 = New System.Windows.Forms.CheckBox
        Me.chkVenderCode = New System.Windows.Forms.CheckBox
        Me.chkSenderCode = New System.Windows.Forms.CheckBox
        Me.chk1JQulifier = New System.Windows.Forms.CheckBox
        Me.cmbTypeofData = New System.Windows.Forms.ComboBox
        Me.txtLoop1000BNM109 = New System.Windows.Forms.TextBox
        Me.txtVenderCode = New System.Windows.Forms.TextBox
        Me.txtSenderCode = New System.Windows.Forms.TextBox
        Me.txt1JQulifier = New System.Windows.Forms.TextBox
        Me.txtSubmitterID = New System.Windows.Forms.TextBox
        Me.txtReceiverID = New System.Windows.Forms.TextBox
        Me.txtNameofReceiver = New System.Windows.Forms.TextBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.lblTypeofData = New System.Windows.Forms.Label
        Me.lblSubmitterID = New System.Windows.Forms.Label
        Me.lblReceiverID = New System.Windows.Forms.Label
        Me.lblNameofReceiver = New System.Windows.Forms.Label
        Me.lblCode = New System.Windows.Forms.Label
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tls.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.gbClaimManagement.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tls)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(846, 53)
        Me.pnl_tlspTOP.TabIndex = 4
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = CType(resources.GetObject("tls.BackgroundImage"), System.Drawing.Image)
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(0, 0)
        Me.tls.Name = "tls"
        Me.tls.Size = New System.Drawing.Size(846, 53)
        Me.tls.TabIndex = 0
        Me.tls.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(40, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.groupBox1)
        Me.pnl_Base.Controls.Add(Me.gbClaimManagement)
        Me.pnl_Base.Controls.Add(Me.chkISA)
        Me.pnl_Base.Controls.Add(Me.chkLoop1000BNM109)
        Me.pnl_Base.Controls.Add(Me.chkVenderCode)
        Me.pnl_Base.Controls.Add(Me.chkSenderCode)
        Me.pnl_Base.Controls.Add(Me.chk1JQulifier)
        Me.pnl_Base.Controls.Add(Me.cmbTypeofData)
        Me.pnl_Base.Controls.Add(Me.txtLoop1000BNM109)
        Me.pnl_Base.Controls.Add(Me.txtVenderCode)
        Me.pnl_Base.Controls.Add(Me.txtSenderCode)
        Me.pnl_Base.Controls.Add(Me.txt1JQulifier)
        Me.pnl_Base.Controls.Add(Me.txtSubmitterID)
        Me.pnl_Base.Controls.Add(Me.txtReceiverID)
        Me.pnl_Base.Controls.Add(Me.txtNameofReceiver)
        Me.pnl_Base.Controls.Add(Me.txtName)
        Me.pnl_Base.Controls.Add(Me.lblTypeofData)
        Me.pnl_Base.Controls.Add(Me.lblSubmitterID)
        Me.pnl_Base.Controls.Add(Me.lblReceiverID)
        Me.pnl_Base.Controls.Add(Me.lblNameofReceiver)
        Me.pnl_Base.Controls.Add(Me.lblCode)
        Me.pnl_Base.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 53)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Base.Size = New System.Drawing.Size(846, 466)
        Me.pnl_Base.TabIndex = 5
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.txt_Password)
        Me.groupBox1.Controls.Add(Me.txt_Username)
        Me.groupBox1.Controls.Add(Me.txt_ftpURL)
        Me.groupBox1.Controls.Add(Me.label21)
        Me.groupBox1.Controls.Add(Me.label25)
        Me.groupBox1.Controls.Add(Me.label29)
        Me.groupBox1.Location = New System.Drawing.Point(49, 302)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(334, 121)
        Me.groupBox1.TabIndex = 15
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Connection Parameters"
        '
        'txt_Password
        '
        Me.txt_Password.ForeColor = System.Drawing.Color.Black
        Me.txt_Password.Location = New System.Drawing.Point(128, 86)
        Me.txt_Password.MaxLength = 50
        Me.txt_Password.Name = "txt_Password"
        Me.txt_Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_Password.Size = New System.Drawing.Size(191, 22)
        Me.txt_Password.TabIndex = 2
        '
        'txt_Username
        '
        Me.txt_Username.ForeColor = System.Drawing.Color.Black
        Me.txt_Username.Location = New System.Drawing.Point(128, 58)
        Me.txt_Username.MaxLength = 50
        Me.txt_Username.Name = "txt_Username"
        Me.txt_Username.Size = New System.Drawing.Size(191, 22)
        Me.txt_Username.TabIndex = 1
        '
        'txt_ftpURL
        '
        Me.txt_ftpURL.ForeColor = System.Drawing.Color.Black
        Me.txt_ftpURL.Location = New System.Drawing.Point(128, 30)
        Me.txt_ftpURL.MaxLength = 50
        Me.txt_ftpURL.Name = "txt_ftpURL"
        Me.txt_ftpURL.Size = New System.Drawing.Size(191, 22)
        Me.txt_ftpURL.TabIndex = 0
        '
        'label21
        '
        Me.label21.AutoSize = True
        Me.label21.Location = New System.Drawing.Point(50, 89)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(58, 14)
        Me.label21.TabIndex = 6
        Me.label21.Text = "Password"
        Me.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label25
        '
        Me.label25.AutoSize = True
        Me.label25.Location = New System.Drawing.Point(50, 61)
        Me.label25.Name = "label25"
        Me.label25.Size = New System.Drawing.Size(66, 14)
        Me.label25.TabIndex = 6
        Me.label25.Text = "User Name"
        Me.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label29
        '
        Me.label29.AutoSize = True
        Me.label29.Location = New System.Drawing.Point(50, 33)
        Me.label29.Name = "label29"
        Me.label29.Size = New System.Drawing.Size(48, 14)
        Me.label29.TabIndex = 6
        Me.label29.Text = "ftp URL"
        Me.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gbClaimManagement
        '
        Me.gbClaimManagement.Controls.Add(Me.txt_WorkedTransactions)
        Me.gbClaimManagement.Controls.Add(Me.txt_Statements)
        Me.gbClaimManagement.Controls.Add(Me.txt_997INAcknowledgement)
        Me.gbClaimManagement.Controls.Add(Me.txt_Reports)
        Me.gbClaimManagement.Controls.Add(Me.txt_997OUTAcknowledgement)
        Me.gbClaimManagement.Controls.Add(Me.txt_835RemittanceAdvice)
        Me.gbClaimManagement.Controls.Add(Me.txt_Letters)
        Me.gbClaimManagement.Controls.Add(Me.label15)
        Me.gbClaimManagement.Controls.Add(Me.txt_837PclaimSubmission)
        Me.gbClaimManagement.Controls.Add(Me.label14)
        Me.gbClaimManagement.Controls.Add(Me.txt_277ClaimStatusResponse)
        Me.gbClaimManagement.Controls.Add(Me.label7)
        Me.gbClaimManagement.Controls.Add(Me.txt_276Eligibilityenquiry)
        Me.gbClaimManagement.Controls.Add(Me.txt_CSRReports)
        Me.gbClaimManagement.Controls.Add(Me.label10)
        Me.gbClaimManagement.Controls.Add(Me.label13)
        Me.gbClaimManagement.Controls.Add(Me.txt_271EligibilityResponse)
        Me.gbClaimManagement.Controls.Add(Me.label6)
        Me.gbClaimManagement.Controls.Add(Me.label9)
        Me.gbClaimManagement.Controls.Add(Me.label12)
        Me.gbClaimManagement.Controls.Add(Me.label3)
        Me.gbClaimManagement.Controls.Add(Me.label5)
        Me.gbClaimManagement.Controls.Add(Me.label8)
        Me.gbClaimManagement.Controls.Add(Me.label11)
        Me.gbClaimManagement.Controls.Add(Me.label2)
        Me.gbClaimManagement.Controls.Add(Me.label4)
        Me.gbClaimManagement.Controls.Add(Me.label1)
        Me.gbClaimManagement.Location = New System.Drawing.Point(394, 16)
        Me.gbClaimManagement.Name = "gbClaimManagement"
        Me.gbClaimManagement.Size = New System.Drawing.Size(438, 436)
        Me.gbClaimManagement.TabIndex = 16
        Me.gbClaimManagement.TabStop = False
        Me.gbClaimManagement.Text = "Claim Management"
        '
        'txt_WorkedTransactions
        '
        Me.txt_WorkedTransactions.ForeColor = System.Drawing.Color.Black
        Me.txt_WorkedTransactions.Location = New System.Drawing.Point(209, 400)
        Me.txt_WorkedTransactions.MaxLength = 50
        Me.txt_WorkedTransactions.Name = "txt_WorkedTransactions"
        Me.txt_WorkedTransactions.Size = New System.Drawing.Size(191, 22)
        Me.txt_WorkedTransactions.TabIndex = 11
        '
        'txt_Statements
        '
        Me.txt_Statements.ForeColor = System.Drawing.Color.Black
        Me.txt_Statements.Location = New System.Drawing.Point(209, 372)
        Me.txt_Statements.MaxLength = 50
        Me.txt_Statements.Name = "txt_Statements"
        Me.txt_Statements.Size = New System.Drawing.Size(191, 22)
        Me.txt_Statements.TabIndex = 10
        '
        'txt_997INAcknowledgement
        '
        Me.txt_997INAcknowledgement.ForeColor = System.Drawing.Color.Black
        Me.txt_997INAcknowledgement.Location = New System.Drawing.Point(209, 132)
        Me.txt_997INAcknowledgement.MaxLength = 50
        Me.txt_997INAcknowledgement.Name = "txt_997INAcknowledgement"
        Me.txt_997INAcknowledgement.Size = New System.Drawing.Size(191, 22)
        Me.txt_997INAcknowledgement.TabIndex = 3
        '
        'txt_Reports
        '
        Me.txt_Reports.ForeColor = System.Drawing.Color.Black
        Me.txt_Reports.Location = New System.Drawing.Point(209, 344)
        Me.txt_Reports.MaxLength = 50
        Me.txt_Reports.Name = "txt_Reports"
        Me.txt_Reports.Size = New System.Drawing.Size(191, 22)
        Me.txt_Reports.TabIndex = 9
        '
        'txt_997OUTAcknowledgement
        '
        Me.txt_997OUTAcknowledgement.ForeColor = System.Drawing.Color.Black
        Me.txt_997OUTAcknowledgement.Location = New System.Drawing.Point(209, 241)
        Me.txt_997OUTAcknowledgement.MaxLength = 50
        Me.txt_997OUTAcknowledgement.Name = "txt_997OUTAcknowledgement"
        Me.txt_997OUTAcknowledgement.Size = New System.Drawing.Size(191, 22)
        Me.txt_997OUTAcknowledgement.TabIndex = 6
        '
        'txt_835RemittanceAdvice
        '
        Me.txt_835RemittanceAdvice.ForeColor = System.Drawing.Color.Black
        Me.txt_835RemittanceAdvice.Location = New System.Drawing.Point(209, 104)
        Me.txt_835RemittanceAdvice.MaxLength = 50
        Me.txt_835RemittanceAdvice.Name = "txt_835RemittanceAdvice"
        Me.txt_835RemittanceAdvice.Size = New System.Drawing.Size(191, 22)
        Me.txt_835RemittanceAdvice.TabIndex = 2
        '
        'txt_Letters
        '
        Me.txt_Letters.ForeColor = System.Drawing.Color.Black
        Me.txt_Letters.Location = New System.Drawing.Point(209, 316)
        Me.txt_Letters.MaxLength = 50
        Me.txt_Letters.Name = "txt_Letters"
        Me.txt_Letters.Size = New System.Drawing.Size(191, 22)
        Me.txt_Letters.TabIndex = 8
        '
        'label15
        '
        Me.label15.AutoSize = True
        Me.label15.Location = New System.Drawing.Point(50, 403)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(122, 14)
        Me.label15.TabIndex = 6
        Me.label15.Text = "Worked Transactions"
        Me.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_837PclaimSubmission
        '
        Me.txt_837PclaimSubmission.ForeColor = System.Drawing.Color.Black
        Me.txt_837PclaimSubmission.Location = New System.Drawing.Point(209, 213)
        Me.txt_837PclaimSubmission.MaxLength = 50
        Me.txt_837PclaimSubmission.Name = "txt_837PclaimSubmission"
        Me.txt_837PclaimSubmission.Size = New System.Drawing.Size(191, 22)
        Me.txt_837PclaimSubmission.TabIndex = 5
        '
        'label14
        '
        Me.label14.AutoSize = True
        Me.label14.Location = New System.Drawing.Point(50, 375)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(71, 14)
        Me.label14.TabIndex = 6
        Me.label14.Text = "Statements"
        Me.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_277ClaimStatusResponse
        '
        Me.txt_277ClaimStatusResponse.ForeColor = System.Drawing.Color.Black
        Me.txt_277ClaimStatusResponse.Location = New System.Drawing.Point(209, 76)
        Me.txt_277ClaimStatusResponse.MaxLength = 50
        Me.txt_277ClaimStatusResponse.Name = "txt_277ClaimStatusResponse"
        Me.txt_277ClaimStatusResponse.Size = New System.Drawing.Size(191, 22)
        Me.txt_277ClaimStatusResponse.TabIndex = 1
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Location = New System.Drawing.Point(50, 135)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(135, 14)
        Me.label7.TabIndex = 6
        Me.label7.Text = "997 Acknowledgement"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_276Eligibilityenquiry
        '
        Me.txt_276Eligibilityenquiry.ForeColor = System.Drawing.Color.Black
        Me.txt_276Eligibilityenquiry.Location = New System.Drawing.Point(209, 185)
        Me.txt_276Eligibilityenquiry.MaxLength = 50
        Me.txt_276Eligibilityenquiry.Name = "txt_276Eligibilityenquiry"
        Me.txt_276Eligibilityenquiry.Size = New System.Drawing.Size(191, 22)
        Me.txt_276Eligibilityenquiry.TabIndex = 4
        '
        'txt_CSRReports
        '
        Me.txt_CSRReports.ForeColor = System.Drawing.Color.Black
        Me.txt_CSRReports.Location = New System.Drawing.Point(209, 288)
        Me.txt_CSRReports.MaxLength = 50
        Me.txt_CSRReports.Name = "txt_CSRReports"
        Me.txt_CSRReports.Size = New System.Drawing.Size(191, 22)
        Me.txt_CSRReports.TabIndex = 7
        '
        'label10
        '
        Me.label10.AutoSize = True
        Me.label10.Location = New System.Drawing.Point(50, 244)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(135, 14)
        Me.label10.TabIndex = 6
        Me.label10.Text = "997 Acknowledgement"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label13
        '
        Me.label13.AutoSize = True
        Me.label13.Location = New System.Drawing.Point(50, 347)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(49, 14)
        Me.label13.TabIndex = 6
        Me.label13.Text = "Reports"
        Me.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_271EligibilityResponse
        '
        Me.txt_271EligibilityResponse.ForeColor = System.Drawing.Color.Black
        Me.txt_271EligibilityResponse.Location = New System.Drawing.Point(209, 48)
        Me.txt_271EligibilityResponse.MaxLength = 50
        Me.txt_271EligibilityResponse.Name = "txt_271EligibilityResponse"
        Me.txt_271EligibilityResponse.Size = New System.Drawing.Size(191, 22)
        Me.txt_271EligibilityResponse.TabIndex = 0
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(50, 107)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(134, 14)
        Me.label6.TabIndex = 6
        Me.label6.Text = "835 Remittance Advice"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(50, 216)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(127, 14)
        Me.label9.TabIndex = 6
        Me.label9.Text = "837P Claim submission"
        Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label12
        '
        Me.label12.AutoSize = True
        Me.label12.Location = New System.Drawing.Point(50, 319)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(46, 14)
        Me.label12.TabIndex = 6
        Me.label12.Text = "Letters"
        Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.Location = New System.Drawing.Point(7, 269)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(52, 14)
        Me.label3.TabIndex = 6
        Me.label3.Text = "General"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(50, 79)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(154, 14)
        Me.label5.TabIndex = 6
        Me.label5.Text = "277 Claim Status Response"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(50, 188)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(120, 14)
        Me.label8.TabIndex = 6
        Me.label8.Text = "276 Eligibility Enquiry"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label11
        '
        Me.label11.AutoSize = True
        Me.label11.Location = New System.Drawing.Point(50, 291)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(74, 14)
        Me.label11.TabIndex = 6
        Me.label11.Text = "CSR Reports"
        Me.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(7, 168)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(53, 14)
        Me.label2.TabIndex = 6
        Me.label2.Text = "Outbox"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(50, 51)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(132, 14)
        Me.label4.TabIndex = 6
        Me.label4.Text = "271 Eligibility Response"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(7, 26)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(43, 14)
        Me.label1.TabIndex = 6
        Me.label1.Text = "Inbox"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkISA
        '
        Me.chkISA.AutoSize = True
        Me.chkISA.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkISA.Location = New System.Drawing.Point(107, 270)
        Me.chkISA.Name = "chkISA"
        Me.chkISA.Size = New System.Drawing.Size(84, 18)
        Me.chkISA.TabIndex = 14
        Me.chkISA.Text = "ISA = 01 :"
        Me.chkISA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkISA.UseVisualStyleBackColor = True
        '
        'chkLoop1000BNM109
        '
        Me.chkLoop1000BNM109.AutoSize = True
        Me.chkLoop1000BNM109.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkLoop1000BNM109.Location = New System.Drawing.Point(49, 214)
        Me.chkLoop1000BNM109.Name = "chkLoop1000BNM109"
        Me.chkLoop1000BNM109.Size = New System.Drawing.Size(142, 18)
        Me.chkLoop1000BNM109.TabIndex = 11
        Me.chkLoop1000BNM109.Text = "Loop 1000B NM109 :"
        Me.chkLoop1000BNM109.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkLoop1000BNM109.UseVisualStyleBackColor = True
        '
        'chkVenderCode
        '
        Me.chkVenderCode.AutoSize = True
        Me.chkVenderCode.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkVenderCode.Location = New System.Drawing.Point(79, 184)
        Me.chkVenderCode.Name = "chkVenderCode"
        Me.chkVenderCode.Size = New System.Drawing.Size(112, 18)
        Me.chkVenderCode.TabIndex = 9
        Me.chkVenderCode.Text = "Receiver Code :"
        Me.chkVenderCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkVenderCode.UseVisualStyleBackColor = True
        '
        'chkSenderCode
        '
        Me.chkSenderCode.AutoSize = True
        Me.chkSenderCode.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkSenderCode.Location = New System.Drawing.Point(86, 154)
        Me.chkSenderCode.Name = "chkSenderCode"
        Me.chkSenderCode.Size = New System.Drawing.Size(105, 18)
        Me.chkSenderCode.TabIndex = 7
        Me.chkSenderCode.Text = "Sender Code :"
        Me.chkSenderCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkSenderCode.UseVisualStyleBackColor = True
        '
        'chk1JQulifier
        '
        Me.chk1JQulifier.AutoSize = True
        Me.chk1JQulifier.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk1JQulifier.Location = New System.Drawing.Point(104, 124)
        Me.chk1JQulifier.Name = "chk1JQulifier"
        Me.chk1JQulifier.Size = New System.Drawing.Size(87, 18)
        Me.chk1JQulifier.TabIndex = 5
        Me.chk1JQulifier.Text = "1J Qulifier :"
        Me.chk1JQulifier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk1JQulifier.UseVisualStyleBackColor = True
        '
        'cmbTypeofData
        '
        Me.cmbTypeofData.ForeColor = System.Drawing.Color.Black
        Me.cmbTypeofData.FormattingEnabled = True
        Me.cmbTypeofData.Items.AddRange(New Object() {"", "Test Data", "Production Data"})
        Me.cmbTypeofData.Location = New System.Drawing.Point(177, 240)
        Me.cmbTypeofData.Name = "cmbTypeofData"
        Me.cmbTypeofData.Size = New System.Drawing.Size(191, 22)
        Me.cmbTypeofData.TabIndex = 13
        '
        'txtLoop1000BNM109
        '
        Me.txtLoop1000BNM109.Enabled = False
        Me.txtLoop1000BNM109.ForeColor = System.Drawing.Color.Black
        Me.txtLoop1000BNM109.Location = New System.Drawing.Point(199, 212)
        Me.txtLoop1000BNM109.MaxLength = 50
        Me.txtLoop1000BNM109.Name = "txtLoop1000BNM109"
        Me.txtLoop1000BNM109.Size = New System.Drawing.Size(169, 22)
        Me.txtLoop1000BNM109.TabIndex = 12
        '
        'txtVenderCode
        '
        Me.txtVenderCode.Enabled = False
        Me.txtVenderCode.ForeColor = System.Drawing.Color.Black
        Me.txtVenderCode.Location = New System.Drawing.Point(199, 182)
        Me.txtVenderCode.MaxLength = 50
        Me.txtVenderCode.Name = "txtVenderCode"
        Me.txtVenderCode.Size = New System.Drawing.Size(169, 22)
        Me.txtVenderCode.TabIndex = 10
        '
        'txtSenderCode
        '
        Me.txtSenderCode.Enabled = False
        Me.txtSenderCode.ForeColor = System.Drawing.Color.Black
        Me.txtSenderCode.Location = New System.Drawing.Point(199, 152)
        Me.txtSenderCode.MaxLength = 50
        Me.txtSenderCode.Name = "txtSenderCode"
        Me.txtSenderCode.Size = New System.Drawing.Size(169, 22)
        Me.txtSenderCode.TabIndex = 8
        '
        'txt1JQulifier
        '
        Me.txt1JQulifier.Enabled = False
        Me.txt1JQulifier.ForeColor = System.Drawing.Color.Black
        Me.txt1JQulifier.Location = New System.Drawing.Point(199, 122)
        Me.txt1JQulifier.MaxLength = 50
        Me.txt1JQulifier.Name = "txt1JQulifier"
        Me.txt1JQulifier.Size = New System.Drawing.Size(169, 22)
        Me.txt1JQulifier.TabIndex = 6
        '
        'txtSubmitterID
        '
        Me.txtSubmitterID.ForeColor = System.Drawing.Color.Black
        Me.txtSubmitterID.Location = New System.Drawing.Point(177, 94)
        Me.txtSubmitterID.MaxLength = 50
        Me.txtSubmitterID.Name = "txtSubmitterID"
        Me.txtSubmitterID.Size = New System.Drawing.Size(191, 22)
        Me.txtSubmitterID.TabIndex = 4
        '
        'txtReceiverID
        '
        Me.txtReceiverID.ForeColor = System.Drawing.Color.Black
        Me.txtReceiverID.Location = New System.Drawing.Point(177, 68)
        Me.txtReceiverID.MaxLength = 50
        Me.txtReceiverID.Name = "txtReceiverID"
        Me.txtReceiverID.Size = New System.Drawing.Size(191, 22)
        Me.txtReceiverID.TabIndex = 3
        '
        'txtNameofReceiver
        '
        Me.txtNameofReceiver.ForeColor = System.Drawing.Color.Black
        Me.txtNameofReceiver.Location = New System.Drawing.Point(177, 42)
        Me.txtNameofReceiver.MaxLength = 50
        Me.txtNameofReceiver.Name = "txtNameofReceiver"
        Me.txtNameofReceiver.Size = New System.Drawing.Size(191, 22)
        Me.txtNameofReceiver.TabIndex = 2
        '
        'txtName
        '
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(177, 16)
        Me.txtName.MaxLength = 50
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(191, 22)
        Me.txtName.TabIndex = 1
        '
        'lblTypeofData
        '
        Me.lblTypeofData.AutoSize = True
        Me.lblTypeofData.Location = New System.Drawing.Point(86, 244)
        Me.lblTypeofData.Name = "lblTypeofData"
        Me.lblTypeofData.Size = New System.Drawing.Size(87, 14)
        Me.lblTypeofData.TabIndex = 5
        Me.lblTypeofData.Text = "Type of Data :"
        Me.lblTypeofData.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSubmitterID
        '
        Me.lblSubmitterID.AutoSize = True
        Me.lblSubmitterID.Location = New System.Drawing.Point(103, 97)
        Me.lblSubmitterID.Name = "lblSubmitterID"
        Me.lblSubmitterID.Size = New System.Drawing.Size(70, 14)
        Me.lblSubmitterID.TabIndex = 5
        Me.lblSubmitterID.Text = "Sender ID :"
        Me.lblSubmitterID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblReceiverID
        '
        Me.lblReceiverID.AutoSize = True
        Me.lblReceiverID.Location = New System.Drawing.Point(96, 72)
        Me.lblReceiverID.Name = "lblReceiverID"
        Me.lblReceiverID.Size = New System.Drawing.Size(77, 14)
        Me.lblReceiverID.TabIndex = 5
        Me.lblReceiverID.Text = "Receiver ID :"
        Me.lblReceiverID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNameofReceiver
        '
        Me.lblNameofReceiver.AutoSize = True
        Me.lblNameofReceiver.Location = New System.Drawing.Point(62, 46)
        Me.lblNameofReceiver.Name = "lblNameofReceiver"
        Me.lblNameofReceiver.Size = New System.Drawing.Size(111, 14)
        Me.lblNameofReceiver.TabIndex = 5
        Me.lblNameofReceiver.Text = "Name of Receiver :"
        Me.lblNameofReceiver.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.Location = New System.Drawing.Point(48, 19)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(125, 14)
        Me.lblCode.TabIndex = 5
        Me.lblCode.Text = "Clearinghouse Name :"
        Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 462)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(838, 1)
        Me.lbl_BottomBrd.TabIndex = 4
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 459)
        Me.lbl_LeftBrd.TabIndex = 0
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(842, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 459)
        Me.lbl_RightBrd.TabIndex = 2
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(840, 1)
        Me.lbl_TopBrd.TabIndex = 0
        Me.lbl_TopBrd.Text = "label1"
        '
        'frmSetupClearingHouse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(846, 519)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetupClearingHouse"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ClearingHouse"
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.pnl_Base.PerformLayout()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.gbClaimManagement.ResumeLayout(False)
        Me.gbClaimManagement.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tls As System.Windows.Forms.ToolStrip
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents txt_Password As System.Windows.Forms.TextBox
    Private WithEvents txt_Username As System.Windows.Forms.TextBox
    Private WithEvents txt_ftpURL As System.Windows.Forms.TextBox
    Private WithEvents label21 As System.Windows.Forms.Label
    Private WithEvents label25 As System.Windows.Forms.Label
    Private WithEvents label29 As System.Windows.Forms.Label
    Private WithEvents gbClaimManagement As System.Windows.Forms.GroupBox
    Private WithEvents txt_WorkedTransactions As System.Windows.Forms.TextBox
    Private WithEvents txt_Statements As System.Windows.Forms.TextBox
    Private WithEvents txt_997INAcknowledgement As System.Windows.Forms.TextBox
    Private WithEvents txt_Reports As System.Windows.Forms.TextBox
    Private WithEvents txt_997OUTAcknowledgement As System.Windows.Forms.TextBox
    Private WithEvents txt_835RemittanceAdvice As System.Windows.Forms.TextBox
    Private WithEvents txt_Letters As System.Windows.Forms.TextBox
    Private WithEvents label15 As System.Windows.Forms.Label
    Private WithEvents txt_837PclaimSubmission As System.Windows.Forms.TextBox
    Private WithEvents label14 As System.Windows.Forms.Label
    Private WithEvents txt_277ClaimStatusResponse As System.Windows.Forms.TextBox
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents txt_276Eligibilityenquiry As System.Windows.Forms.TextBox
    Private WithEvents txt_CSRReports As System.Windows.Forms.TextBox
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents label13 As System.Windows.Forms.Label
    Private WithEvents txt_271EligibilityResponse As System.Windows.Forms.TextBox
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents label12 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents label11 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents chkISA As System.Windows.Forms.CheckBox
    Private WithEvents chkLoop1000BNM109 As System.Windows.Forms.CheckBox
    Private WithEvents chkVenderCode As System.Windows.Forms.CheckBox
    Private WithEvents chkSenderCode As System.Windows.Forms.CheckBox
    Private WithEvents chk1JQulifier As System.Windows.Forms.CheckBox
    Private WithEvents cmbTypeofData As System.Windows.Forms.ComboBox
    Private WithEvents txtLoop1000BNM109 As System.Windows.Forms.TextBox
    Private WithEvents txtVenderCode As System.Windows.Forms.TextBox
    Private WithEvents txtSenderCode As System.Windows.Forms.TextBox
    Private WithEvents txt1JQulifier As System.Windows.Forms.TextBox
    Private WithEvents txtSubmitterID As System.Windows.Forms.TextBox
    Private WithEvents txtReceiverID As System.Windows.Forms.TextBox
    Private WithEvents txtNameofReceiver As System.Windows.Forms.TextBox
    Private WithEvents txtName As System.Windows.Forms.TextBox
    Private WithEvents lblTypeofData As System.Windows.Forms.Label
    Private WithEvents lblSubmitterID As System.Windows.Forms.Label
    Private WithEvents lblReceiverID As System.Windows.Forms.Label
    Private WithEvents lblNameofReceiver As System.Windows.Forms.Label
    Private WithEvents lblCode As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
End Class
