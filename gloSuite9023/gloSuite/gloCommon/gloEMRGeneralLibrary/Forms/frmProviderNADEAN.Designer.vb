<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProviderNADEAN
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProviderNADEAN))
        Me.pnlNADEAN = New System.Windows.Forms.Panel()
        Me.txtNADEAN = New System.Windows.Forms.MaskedTextBox()
        Me.lblNADEAN = New System.Windows.Forms.Label()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.tlsp_ContactInformation = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlNADEAN.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_ContactInformation.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlNADEAN
        '
        Me.pnlNADEAN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlNADEAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlNADEAN.Controls.Add(Me.Label4)
        Me.pnlNADEAN.Controls.Add(Me.Label3)
        Me.pnlNADEAN.Controls.Add(Me.Label2)
        Me.pnlNADEAN.Controls.Add(Me.Label1)
        Me.pnlNADEAN.Controls.Add(Me.txtNADEAN)
        Me.pnlNADEAN.Controls.Add(Me.lblNADEAN)
        Me.pnlNADEAN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNADEAN.Location = New System.Drawing.Point(0, 53)
        Me.pnlNADEAN.Name = "pnlNADEAN"
        Me.pnlNADEAN.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlNADEAN.Size = New System.Drawing.Size(397, 75)
        Me.pnlNADEAN.TabIndex = 0
        '
        'txtNADEAN
        '
        Me.txtNADEAN.Location = New System.Drawing.Point(136, 25)
        Me.txtNADEAN.Mask = "LL0000000"
        Me.txtNADEAN.Name = "txtNADEAN"
        Me.txtNADEAN.Size = New System.Drawing.Size(230, 22)
        Me.txtNADEAN.TabIndex = 0
        '
        'lblNADEAN
        '
        Me.lblNADEAN.AutoSize = True
        Me.lblNADEAN.BackColor = System.Drawing.Color.Transparent
        Me.lblNADEAN.Location = New System.Drawing.Point(24, 29)
        Me.lblNADEAN.Name = "lblNADEAN"
        Me.lblNADEAN.Size = New System.Drawing.Size(109, 14)
        Me.lblNADEAN.TabIndex = 4
        Me.lblNADEAN.Text = "NADEAN Number :"
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
        Me.tlsp_ContactInformation.BackgroundImage = CType(resources.GetObject("tlsp_ContactInformation.BackgroundImage"), System.Drawing.Image)
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
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(389, 1)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "NADEAN Number :"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(389, 1)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "NADEAN Number :"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(391, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 65)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "NADEAN Number :"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(3, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 65)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "NADEAN Number :"
        '
        'frmProviderNADEAN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(397, 128)
        Me.Controls.Add(Me.pnlNADEAN)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProviderNADEAN"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Narcotics Addiction DEA Number(NADEAN)"
        Me.pnlNADEAN.ResumeLayout(False)
        Me.pnlNADEAN.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_ContactInformation.ResumeLayout(False)
        Me.tlsp_ContactInformation.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlNADEAN As System.Windows.Forms.Panel
    Friend WithEvents txtNADEAN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblNADEAN As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_ContactInformation As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
