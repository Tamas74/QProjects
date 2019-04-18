<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLab_ContactInformation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLab_ContactInformation))
        Me.pnlRefSampledBy = New System.Windows.Forms.Panel()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.lblLname = New System.Windows.Forms.Label()
        Me.lblFname = New System.Windows.Forms.Label()
        Me.txtMiddleName = New System.Windows.Forms.TextBox()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.lblMname = New System.Windows.Forms.Label()
        Me.pnlContactInfo = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtContactName = New System.Windows.Forms.TextBox()
        Me.lblLab = New System.Windows.Forms.Label()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.tlsp_ContactInformation = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.mskGIPhone = New gloMaskControl.gloMaskBox()
        Me.lblGIPhone = New System.Windows.Forms.Label()
        Me.pnlAddresssControl = New System.Windows.Forms.Panel()
        Me.pnlRefSampledBy.SuspendLayout()
        Me.pnlContactInfo.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_ContactInformation.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlRefSampledBy
        '
        Me.pnlRefSampledBy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlRefSampledBy.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlRefSampledBy.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlRefSampledBy.Controls.Add(Me.lbl_pnlRight)
        Me.pnlRefSampledBy.Controls.Add(Me.lbl_pnlTop)
        Me.pnlRefSampledBy.Controls.Add(Me.txtLastName)
        Me.pnlRefSampledBy.Controls.Add(Me.lblLname)
        Me.pnlRefSampledBy.Controls.Add(Me.lblFname)
        Me.pnlRefSampledBy.Controls.Add(Me.txtMiddleName)
        Me.pnlRefSampledBy.Controls.Add(Me.txtFirstName)
        Me.pnlRefSampledBy.Controls.Add(Me.lblMname)
        Me.pnlRefSampledBy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRefSampledBy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlRefSampledBy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlRefSampledBy.Location = New System.Drawing.Point(0, 53)
        Me.pnlRefSampledBy.Name = "pnlRefSampledBy"
        Me.pnlRefSampledBy.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlRefSampledBy.Size = New System.Drawing.Size(397, 207)
        Me.pnlRefSampledBy.TabIndex = 0
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 203)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(389, 1)
        Me.lbl_pnlBottom.TabIndex = 19
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 200)
        Me.lbl_pnlLeft.TabIndex = 18
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlRight.Location = New System.Drawing.Point(393, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 200)
        Me.lbl_pnlRight.TabIndex = 17
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(391, 1)
        Me.lbl_pnlTop.TabIndex = 16
        Me.lbl_pnlTop.Text = "label1"
        '
        'txtLastName
        '
        Me.txtLastName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastName.ForeColor = System.Drawing.Color.Black
        Me.txtLastName.Location = New System.Drawing.Point(110, 76)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(257, 22)
        Me.txtLastName.TabIndex = 2
        '
        'lblLname
        '
        Me.lblLname.AutoSize = True
        Me.lblLname.BackColor = System.Drawing.Color.Transparent
        Me.lblLname.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLname.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLname.Location = New System.Drawing.Point(34, 80)
        Me.lblLname.Name = "lblLname"
        Me.lblLname.Size = New System.Drawing.Size(72, 14)
        Me.lblLname.TabIndex = 15
        Me.lblLname.Text = "Last Name :"
        '
        'lblFname
        '
        Me.lblFname.AutoSize = True
        Me.lblFname.BackColor = System.Drawing.Color.Transparent
        Me.lblFname.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFname.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFname.Location = New System.Drawing.Point(34, 20)
        Me.lblFname.Name = "lblFname"
        Me.lblFname.Size = New System.Drawing.Size(72, 14)
        Me.lblFname.TabIndex = 14
        Me.lblFname.Text = "First Name :"
        '
        'txtMiddleName
        '
        Me.txtMiddleName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMiddleName.ForeColor = System.Drawing.Color.Black
        Me.txtMiddleName.Location = New System.Drawing.Point(110, 46)
        Me.txtMiddleName.Name = "txtMiddleName"
        Me.txtMiddleName.Size = New System.Drawing.Size(257, 22)
        Me.txtMiddleName.TabIndex = 1
        '
        'txtFirstName
        '
        Me.txtFirstName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstName.ForeColor = System.Drawing.Color.Black
        Me.txtFirstName.Location = New System.Drawing.Point(110, 16)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(257, 22)
        Me.txtFirstName.TabIndex = 0
        '
        'lblMname
        '
        Me.lblMname.AutoSize = True
        Me.lblMname.BackColor = System.Drawing.Color.Transparent
        Me.lblMname.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMname.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblMname.Location = New System.Drawing.Point(22, 50)
        Me.lblMname.Name = "lblMname"
        Me.lblMname.Size = New System.Drawing.Size(84, 14)
        Me.lblMname.TabIndex = 11
        Me.lblMname.Text = "Middle Name :"
        '
        'pnlContactInfo
        '
        Me.pnlContactInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlContactInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlContactInfo.Controls.Add(Me.pnlAddresssControl)
        Me.pnlContactInfo.Controls.Add(Me.mskGIPhone)
        Me.pnlContactInfo.Controls.Add(Me.lblGIPhone)
        Me.pnlContactInfo.Controls.Add(Me.Label4)
        Me.pnlContactInfo.Controls.Add(Me.Label3)
        Me.pnlContactInfo.Controls.Add(Me.Label2)
        Me.pnlContactInfo.Controls.Add(Me.Label1)
        Me.pnlContactInfo.Controls.Add(Me.Label5)
        Me.pnlContactInfo.Controls.Add(Me.txtContactName)
        Me.pnlContactInfo.Controls.Add(Me.lblLab)
        Me.pnlContactInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContactInfo.Location = New System.Drawing.Point(0, 53)
        Me.pnlContactInfo.Name = "pnlContactInfo"
        Me.pnlContactInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlContactInfo.Size = New System.Drawing.Size(397, 207)
        Me.pnlContactInfo.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 197)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(388, 1)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(391, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 198)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 201)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(389, 1)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "label2"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(13, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 14)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "*"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtContactName
        '
        Me.txtContactName.Location = New System.Drawing.Point(123, 10)
        Me.txtContactName.Name = "txtContactName"
        Me.txtContactName.Size = New System.Drawing.Size(239, 22)
        Me.txtContactName.TabIndex = 0
        '
        'lblLab
        '
        Me.lblLab.AutoSize = True
        Me.lblLab.BackColor = System.Drawing.Color.Transparent
        Me.lblLab.Location = New System.Drawing.Point(26, 14)
        Me.lblLab.Name = "lblLab"
        Me.lblLab.Size = New System.Drawing.Size(93, 14)
        Me.lblLab.TabIndex = 4
        Me.lblLab.Text = "Contact Name :"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_ContactInformation)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(397, 53)
        Me.pnl_tlsp.TabIndex = 1
        '
        'tlsp_ContactInformation
        '
        Me.tlsp_ContactInformation.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_ContactInformation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_ContactInformation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_ContactInformation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_ContactInformation.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_ContactInformation.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_ContactInformation.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_ContactInformation.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_ContactInformation.Name = "tlsp_ContactInformation"
        Me.tlsp_ContactInformation.Size = New System.Drawing.Size(397, 53)
        Me.tlsp_ContactInformation.TabIndex = 0
        Me.tlsp_ContactInformation.TabStop = True
        Me.tlsp_ContactInformation.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
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
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'mskGIPhone
        '
        Me.mskGIPhone.AllowValidate = True
        Me.mskGIPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskGIPhone.IncludeLiteralsAndPrompts = False
        Me.mskGIPhone.Location = New System.Drawing.Point(124, 166)
        Me.mskGIPhone.MaskType = gloMaskControl.gloMaskType.Phone
        Me.mskGIPhone.Name = "mskGIPhone"
        Me.mskGIPhone.ReadOnly = False
        Me.mskGIPhone.Size = New System.Drawing.Size(146, 22)
        Me.mskGIPhone.TabIndex = 2
        '
        'lblGIPhone
        '
        Me.lblGIPhone.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblGIPhone.AutoEllipsis = True
        Me.lblGIPhone.AutoSize = True
        Me.lblGIPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGIPhone.Location = New System.Drawing.Point(70, 170)
        Me.lblGIPhone.Name = "lblGIPhone"
        Me.lblGIPhone.Size = New System.Drawing.Size(50, 14)
        Me.lblGIPhone.TabIndex = 24
        Me.lblGIPhone.Text = "Phone :"
        '
        'pnlAddresssControl
        '
        Me.pnlAddresssControl.Location = New System.Drawing.Point(41, 34)
        Me.pnlAddresssControl.Name = "pnlAddresssControl"
        Me.pnlAddresssControl.Size = New System.Drawing.Size(325, 132)
        Me.pnlAddresssControl.TabIndex = 1
        '
        'frmLab_ContactInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(397, 260)
        Me.Controls.Add(Me.pnlContactInfo)
        Me.Controls.Add(Me.pnlRefSampledBy)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLab_ContactInformation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Contact Information"
        Me.pnlRefSampledBy.ResumeLayout(False)
        Me.pnlRefSampledBy.PerformLayout()
        Me.pnlContactInfo.ResumeLayout(False)
        Me.pnlContactInfo.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_ContactInformation.ResumeLayout(False)
        Me.tlsp_ContactInformation.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlRefSampledBy As System.Windows.Forms.Panel
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents lblLname As System.Windows.Forms.Label
    Friend WithEvents lblFname As System.Windows.Forms.Label
    Friend WithEvents txtMiddleName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents lblMname As System.Windows.Forms.Label
    Friend WithEvents pnlContactInfo As System.Windows.Forms.Panel
    Friend WithEvents txtContactName As System.Windows.Forms.TextBox
    Friend WithEvents lblLab As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_ContactInformation As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents mskGIPhone As gloMaskControl.gloMaskBox
    Private WithEvents lblGIPhone As System.Windows.Forms.Label
    Friend WithEvents pnlAddresssControl As System.Windows.Forms.Panel
End Class
