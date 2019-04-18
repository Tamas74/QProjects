<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLab_StorageTemperature
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLab_StorageTemperature))
        Me.txtTemperature = New System.Windows.Forms.TextBox()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.lblTemperature = New System.Windows.Forms.Label()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.tlsp_StorageTemperature = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_StorageTemperature.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtTemperature
        '
        Me.txtTemperature.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTemperature.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTemperature.ForeColor = System.Drawing.Color.Black
        Me.txtTemperature.Location = New System.Drawing.Point(161, 51)
        Me.txtTemperature.Name = "txtTemperature"
        Me.txtTemperature.Size = New System.Drawing.Size(186, 22)
        Me.txtTemperature.TabIndex = 1
        '
        'pnlTop
        '
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlTop.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlTop.Controls.Add(Me.lbl_pnlRight)
        Me.pnlTop.Controls.Add(Me.lbl_pnlTop)
        Me.pnlTop.Controls.Add(Me.txtTemperature)
        Me.pnlTop.Controls.Add(Me.txtCode)
        Me.pnlTop.Controls.Add(Me.lblTemperature)
        Me.pnlTop.Controls.Add(Me.lblCode)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 53)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(355, 90)
        Me.pnlTop.TabIndex = 4
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 86)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(347, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 83)
        Me.lbl_pnlLeft.TabIndex = 0
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(351, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 83)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(349, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'txtCode
        '
        Me.txtCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.Color.Black
        Me.txtCode.Location = New System.Drawing.Point(161, 18)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(186, 22)
        Me.txtCode.TabIndex = 0
        '
        'lblTemperature
        '
        Me.lblTemperature.AutoSize = True
        Me.lblTemperature.BackColor = System.Drawing.Color.Transparent
        Me.lblTemperature.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTemperature.Location = New System.Drawing.Point(23, 55)
        Me.lblTemperature.Name = "lblTemperature"
        Me.lblTemperature.Size = New System.Drawing.Size(136, 14)
        Me.lblTemperature.TabIndex = 0
        Me.lblTemperature.Text = "Temperature Value :"
        Me.lblTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(110, 22)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(49, 14)
        Me.lblCode.TabIndex = 0
        Me.lblCode.Text = "Code :"
        Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_StorageTemperature)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(355, 53)
        Me.pnl_tlsp.TabIndex = 14
        '
        'tlsp_StorageTemperature
        '
        Me.tlsp_StorageTemperature.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_StorageTemperature.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_StorageTemperature.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_StorageTemperature.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_StorageTemperature.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_StorageTemperature.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_StorageTemperature.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_StorageTemperature.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_StorageTemperature.Name = "tlsp_StorageTemperature"
        Me.tlsp_StorageTemperature.Size = New System.Drawing.Size(355, 53)
        Me.tlsp_StorageTemperature.TabIndex = 0
        Me.tlsp_StorageTemperature.Text = "toolStrip1"
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
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(99, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 14)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "*"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(10, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 14)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "*"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmLab_StorageTemperature
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(355, 143)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLab_StorageTemperature"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Storage Temperature Master"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_StorageTemperature.ResumeLayout(False)
        Me.tlsp_StorageTemperature.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTemperature As System.Windows.Forms.TextBox
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents lblTemperature As System.Windows.Forms.Label
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_StorageTemperature As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
