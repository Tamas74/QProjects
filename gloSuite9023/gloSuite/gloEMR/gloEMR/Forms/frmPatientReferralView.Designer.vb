<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientReferralView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientReferralView))
        Me.ts_ViewButtons = New System.Windows.Forms.ToolStrip
        Me.ts_btnPrint = New System.Windows.Forms.ToolStripButton
        Me.ts_btnFax = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.wdPatientReferral = New AxDSOFramer.AxFramerControl
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ts_ViewButtons.SuspendLayout()
        CType(Me.wdPatientReferral, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnPrint, Me.ts_btnFax, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(783, 53)
        Me.ts_ViewButtons.TabIndex = 6
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnPrint
        '
        Me.ts_btnPrint.Image = CType(resources.GetObject("ts_btnPrint.Image"), System.Drawing.Image)
        Me.ts_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPrint.Name = "ts_btnPrint"
        Me.ts_btnPrint.Size = New System.Drawing.Size(41, 50)
        Me.ts_btnPrint.Tag = "Print"
        Me.ts_btnPrint.Text = "&Print"
        Me.ts_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnFax
        '
        Me.ts_btnFax.Image = CType(resources.GetObject("ts_btnFax.Image"), System.Drawing.Image)
        Me.ts_btnFax.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnFax.Name = "ts_btnFax"
        Me.ts_btnFax.Size = New System.Drawing.Size(36, 50)
        Me.ts_btnFax.Tag = "Fax"
        Me.ts_btnFax.Text = "&Fax"
        Me.ts_btnFax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'wdPatientReferral
        '
        Me.wdPatientReferral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPatientReferral.Enabled = True
        Me.wdPatientReferral.Location = New System.Drawing.Point(4, 4)
        Me.wdPatientReferral.Name = "wdPatientReferral"
        Me.wdPatientReferral.OcxState = CType(resources.GetObject("wdPatientReferral.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPatientReferral.Size = New System.Drawing.Size(775, 467)
        Me.wdPatientReferral.TabIndex = 7
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.wdPatientReferral)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 56)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(783, 474)
        Me.Panel2.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 470)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(775, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 467)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(779, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 467)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(777, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ts_ViewButtons)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(783, 56)
        Me.Panel1.TabIndex = 21
        '
        'frmPatientReferralView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(783, 530)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientReferralView"
        Me.ShowInTaskbar = False
        Me.Text = "Patient Referral"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        CType(Me.wdPatientReferral, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ts_ViewButtons As System.Windows.Forms.ToolStrip
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents wdPatientReferral As AxDSOFramer.AxFramerControl
    Friend WithEvents ts_btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ts_btnFax As System.Windows.Forms.ToolStripButton
End Class
