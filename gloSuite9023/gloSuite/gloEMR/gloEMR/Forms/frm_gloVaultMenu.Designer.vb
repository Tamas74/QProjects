<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_gloVaultMenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_gloVaultMenu))
        Me.pnl_tlstrip = New System.Windows.Forms.Panel
        Me.tlsDM = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsDM_Save = New System.Windows.Forms.ToolStripButton
        Me.tlsDM_Close = New System.Windows.Forms.ToolStripButton
        Me.tlbRemoveAuthorization = New System.Windows.Forms.ToolStripButton
        Me.pnInformation = New System.Windows.Forms.Panel
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.chkLstConfig = New System.Windows.Forms.CheckedListBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.pnlProcess = New System.Windows.Forms.Panel
        Me.lblDisplay = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlEmail = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lstQuestions = New System.Windows.Forms.ListBox
        Me.chkPrint = New System.Windows.Forms.CheckBox
        Me.chkEmail = New System.Windows.Forms.CheckBox
        Me.txtQuestions = New System.Windows.Forms.TextBox
        Me.txtAnswer = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblPatientEmail = New System.Windows.Forms.Label
        Me.lblPatientName = New System.Windows.Forms.Label
        Me.lblPatientCode = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.pnl_tlstrip.SuspendLayout()
        Me.tlsDM.SuspendLayout()
        Me.pnInformation.SuspendLayout()
        Me.pnlProcess.SuspendLayout()
        Me.pnlEmail.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_tlstrip
        '
        Me.pnl_tlstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlstrip.Controls.Add(Me.tlsDM)
        Me.pnl_tlstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlstrip.Name = "pnl_tlstrip"
        Me.pnl_tlstrip.Size = New System.Drawing.Size(532, 54)
        Me.pnl_tlstrip.TabIndex = 4
        '
        'tlsDM
        '
        Me.tlsDM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDM.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsDM_Save, Me.tlsDM_Close, Me.tlbRemoveAuthorization})
        Me.tlsDM.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDM.Location = New System.Drawing.Point(0, 0)
        Me.tlsDM.Name = "tlsDM"
        Me.tlsDM.Size = New System.Drawing.Size(532, 53)
        Me.tlsDM.TabIndex = 0
        Me.tlsDM.Text = "ToolStrip1"
        '
        'tlsDM_Save
        '
        Me.tlsDM_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDM_Save.Image = CType(resources.GetObject("tlsDM_Save.Image"), System.Drawing.Image)
        Me.tlsDM_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsDM_Save.Name = "tlsDM_Save"
        Me.tlsDM_Save.Size = New System.Drawing.Size(42, 50)
        Me.tlsDM_Save.Tag = "Save"
        Me.tlsDM_Save.Text = "&Send"
        Me.tlsDM_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsDM_Save.ToolTipText = "Send"
        '
        'tlsDM_Close
        '
        Me.tlsDM_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDM_Close.Image = CType(resources.GetObject("tlsDM_Close.Image"), System.Drawing.Image)
        Me.tlsDM_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsDM_Close.Name = "tlsDM_Close"
        Me.tlsDM_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlsDM_Close.Tag = "Close"
        Me.tlsDM_Close.Text = "&Close"
        Me.tlsDM_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbRemoveAuthorization
        '
        Me.tlbRemoveAuthorization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbRemoveAuthorization.Image = CType(resources.GetObject("tlbRemoveAuthorization.Image"), System.Drawing.Image)
        Me.tlbRemoveAuthorization.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbRemoveAuthorization.Name = "tlbRemoveAuthorization"
        Me.tlbRemoveAuthorization.Size = New System.Drawing.Size(149, 50)
        Me.tlbRemoveAuthorization.Tag = "Disconnect"
        Me.tlbRemoveAuthorization.Text = "Remove Authorization"
        Me.tlbRemoveAuthorization.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbRemoveAuthorization.Visible = False
        '
        'pnInformation
        '
        Me.pnInformation.Controls.Add(Me.Label20)
        Me.pnInformation.Controls.Add(Me.Label19)
        Me.pnInformation.Controls.Add(Me.Label18)
        Me.pnInformation.Controls.Add(Me.Label17)
        Me.pnInformation.Controls.Add(Me.chkLstConfig)
        Me.pnInformation.Controls.Add(Me.Label6)
        Me.pnInformation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnInformation.Location = New System.Drawing.Point(0, 0)
        Me.pnInformation.Name = "pnInformation"
        Me.pnInformation.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnInformation.Size = New System.Drawing.Size(532, 269)
        Me.pnInformation.TabIndex = 95
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Location = New System.Drawing.Point(4, 265)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(524, 1)
        Me.Label20.TabIndex = 104
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Location = New System.Drawing.Point(4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(524, 1)
        Me.Label19.TabIndex = 103
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Location = New System.Drawing.Point(528, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 266)
        Me.Label18.TabIndex = 102
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Location = New System.Drawing.Point(3, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 266)
        Me.Label17.TabIndex = 101
        '
        'chkLstConfig
        '
        Me.chkLstConfig.CheckOnClick = True
        Me.chkLstConfig.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLstConfig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkLstConfig.FormattingEnabled = True
        Me.chkLstConfig.Location = New System.Drawing.Point(11, 27)
        Me.chkLstConfig.Name = "chkLstConfig"
        Me.chkLstConfig.Size = New System.Drawing.Size(458, 140)
        Me.chkLstConfig.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 6)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(117, 14)
        Me.Label6.TabIndex = 94
        Me.Label6.Text = "Patient Summary:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlProcess
        '
        Me.pnlProcess.BackColor = System.Drawing.Color.White
        Me.pnlProcess.BackgroundImage = CType(resources.GetObject("pnlProcess.BackgroundImage"), System.Drawing.Image)
        Me.pnlProcess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlProcess.Controls.Add(Me.lblDisplay)
        Me.pnlProcess.Controls.Add(Me.Label5)
        Me.pnlProcess.Location = New System.Drawing.Point(87, 100)
        Me.pnlProcess.Name = "pnlProcess"
        Me.pnlProcess.Size = New System.Drawing.Size(358, 68)
        Me.pnlProcess.TabIndex = 27
        Me.pnlProcess.Visible = False
        '
        'lblDisplay
        '
        Me.lblDisplay.AutoSize = True
        Me.lblDisplay.BackColor = System.Drawing.Color.Transparent
        Me.lblDisplay.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDisplay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDisplay.Location = New System.Drawing.Point(13, 39)
        Me.lblDisplay.Name = "lblDisplay"
        Me.lblDisplay.Size = New System.Drawing.Size(222, 16)
        Me.lblDisplay.TabIndex = 8
        Me.lblDisplay.Text = "Your request is being processed."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(12, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 19)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Please wait..."
        '
        'pnlEmail
        '
        Me.pnlEmail.Controls.Add(Me.Label7)
        Me.pnlEmail.Controls.Add(Me.Label4)
        Me.pnlEmail.Controls.Add(Me.Label3)
        Me.pnlEmail.Controls.Add(Me.Label2)
        Me.pnlEmail.Controls.Add(Me.lstQuestions)
        Me.pnlEmail.Controls.Add(Me.chkPrint)
        Me.pnlEmail.Controls.Add(Me.chkEmail)
        Me.pnlEmail.Controls.Add(Me.txtQuestions)
        Me.pnlEmail.Controls.Add(Me.txtAnswer)
        Me.pnlEmail.Controls.Add(Me.Label15)
        Me.pnlEmail.Controls.Add(Me.Label14)
        Me.pnlEmail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEmail.Location = New System.Drawing.Point(0, 146)
        Me.pnlEmail.Name = "pnlEmail"
        Me.pnlEmail.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlEmail.Size = New System.Drawing.Size(532, 123)
        Me.pnlEmail.TabIndex = 96
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(524, 1)
        Me.Label7.TabIndex = 121
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(4, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(524, 1)
        Me.Label4.TabIndex = 120
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(528, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 120)
        Me.Label3.TabIndex = 119
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 120)
        Me.Label2.TabIndex = 118
        '
        'lstQuestions
        '
        Me.lstQuestions.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lstQuestions.FormattingEnabled = True
        Me.lstQuestions.ItemHeight = 14
        Me.lstQuestions.Location = New System.Drawing.Point(128, 32)
        Me.lstQuestions.Name = "lstQuestions"
        Me.lstQuestions.Size = New System.Drawing.Size(336, 74)
        Me.lstQuestions.TabIndex = 116
        Me.lstQuestions.Visible = False
        '
        'chkPrint
        '
        Me.chkPrint.AutoSize = True
        Me.chkPrint.Location = New System.Drawing.Point(130, 87)
        Me.chkPrint.Name = "chkPrint"
        Me.chkPrint.Size = New System.Drawing.Size(115, 18)
        Me.chkPrint.TabIndex = 1
        Me.chkPrint.Text = "Print notification"
        Me.chkPrint.UseVisualStyleBackColor = True
        '
        'chkEmail
        '
        Me.chkEmail.AutoSize = True
        Me.chkEmail.Checked = True
        Me.chkEmail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEmail.Location = New System.Drawing.Point(130, 63)
        Me.chkEmail.Name = "chkEmail"
        Me.chkEmail.Size = New System.Drawing.Size(180, 18)
        Me.chkEmail.TabIndex = 0
        Me.chkEmail.Text = "E-mail notification to patient"
        Me.chkEmail.UseVisualStyleBackColor = True
        '
        'txtQuestions
        '
        Me.txtQuestions.Location = New System.Drawing.Point(127, 11)
        Me.txtQuestions.MaxLength = 499
        Me.txtQuestions.Name = "txtQuestions"
        Me.txtQuestions.Size = New System.Drawing.Size(337, 22)
        Me.txtQuestions.TabIndex = 1
        '
        'txtAnswer
        '
        Me.txtAnswer.Location = New System.Drawing.Point(127, 37)
        Me.txtAnswer.MaxLength = 499
        Me.txtAnswer.Name = "txtAnswer"
        Me.txtAnswer.Size = New System.Drawing.Size(337, 22)
        Me.txtAnswer.TabIndex = 2
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(22, 40)
        Me.Label15.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(104, 14)
        Me.Label15.TabIndex = 111
        Me.Label15.Text = "Security Answer :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(14, 14)
        Me.Label14.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(112, 14)
        Me.Label14.TabIndex = 109
        Me.Label14.Text = "Security Question :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lblPatientEmail)
        Me.Panel2.Controls.Add(Me.lblPatientName)
        Me.Panel2.Controls.Add(Me.lblPatientCode)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(532, 92)
        Me.Panel2.TabIndex = 97
        '
        'lblPatientEmail
        '
        Me.lblPatientEmail.AutoSize = True
        Me.lblPatientEmail.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientEmail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientEmail.Location = New System.Drawing.Point(127, 67)
        Me.lblPatientEmail.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPatientEmail.Name = "lblPatientEmail"
        Me.lblPatientEmail.Size = New System.Drawing.Size(34, 14)
        Me.lblPatientEmail.TabIndex = 109
        Me.lblPatientEmail.Text = "Email"
        Me.lblPatientEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.Location = New System.Drawing.Point(127, 41)
        Me.lblPatientName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(86, 14)
        Me.lblPatientName.TabIndex = 108
        Me.lblPatientName.Text = "Patient Code :"
        Me.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPatientCode
        '
        Me.lblPatientCode.AutoSize = True
        Me.lblPatientCode.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientCode.Location = New System.Drawing.Point(127, 15)
        Me.lblPatientCode.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPatientCode.Name = "lblPatientCode"
        Me.lblPatientCode.Size = New System.Drawing.Size(86, 14)
        Me.lblPatientCode.TabIndex = 107
        Me.lblPatientCode.Text = "Patient Code :"
        Me.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(34, 41)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(89, 14)
        Me.Label8.TabIndex = 104
        Me.Label8.Text = "Patient Name :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(81, 67)
        Me.Label16.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(42, 14)
        Me.Label16.TabIndex = 105
        Me.Label16.Text = "Email :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Location = New System.Drawing.Point(4, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(524, 1)
        Me.Label13.TabIndex = 102
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Location = New System.Drawing.Point(4, 88)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(524, 1)
        Me.Label12.TabIndex = 101
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 86)
        Me.Label11.TabIndex = 100
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Location = New System.Drawing.Point(528, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 86)
        Me.Label10.TabIndex = 99
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(37, 15)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(86, 14)
        Me.Label9.TabIndex = 98
        Me.Label9.Text = "Patient Code :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frm_gloVaultMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(532, 269)
        Me.Controls.Add(Me.pnlProcess)
        Me.Controls.Add(Me.pnlEmail)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlstrip)
        Me.Controls.Add(Me.pnInformation)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_gloVaultMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "HealthVault Interface"
        Me.pnl_tlstrip.ResumeLayout(False)
        Me.pnl_tlstrip.PerformLayout()
        Me.tlsDM.ResumeLayout(False)
        Me.tlsDM.PerformLayout()
        Me.pnInformation.ResumeLayout(False)
        Me.pnInformation.PerformLayout()
        Me.pnlProcess.ResumeLayout(False)
        Me.pnlProcess.PerformLayout()
        Me.pnlEmail.ResumeLayout(False)
        Me.pnlEmail.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnl_tlstrip As System.Windows.Forms.Panel
    Friend WithEvents tlsDM As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsDM_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsDM_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkLstConfig As System.Windows.Forms.CheckedListBox
    Private WithEvents pnlProcess As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents lblDisplay As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlEmail As System.Windows.Forms.Panel
    Friend WithEvents pnInformation As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblPatientEmail As System.Windows.Forms.Label
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents lblPatientCode As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtAnswer As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents chkPrint As System.Windows.Forms.CheckBox
    Friend WithEvents chkEmail As System.Windows.Forms.CheckBox
    Friend WithEvents txtQuestions As System.Windows.Forms.TextBox
    Friend WithEvents lstQuestions As System.Windows.Forms.ListBox
    Friend WithEvents tlbRemoveAuthorization As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
