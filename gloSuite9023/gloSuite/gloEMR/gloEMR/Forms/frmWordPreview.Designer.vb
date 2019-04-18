<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWordPreview
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWordPreview))
        Me.pnlWDPreview = New System.Windows.Forms.Panel()
        Me.wdPreview = New AxDSOFramer.AxFramerControl()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tls_PreviewStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbtnPrintnCls = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.miniToolStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.pnlWDPreview.SuspendLayout()
        CType(Me.wdPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.tls_PreviewStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlWDPreview
        '
        Me.pnlWDPreview.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlWDPreview.Controls.Add(Me.wdPreview)
        Me.pnlWDPreview.Controls.Add(Me.Label4)
        Me.pnlWDPreview.Controls.Add(Me.Label3)
        Me.pnlWDPreview.Controls.Add(Me.Label2)
        Me.pnlWDPreview.Controls.Add(Me.Label1)
        Me.pnlWDPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWDPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlWDPreview.ForeColor = System.Drawing.Color.Black
        Me.pnlWDPreview.Location = New System.Drawing.Point(0, 53)
        Me.pnlWDPreview.Name = "pnlWDPreview"
        Me.pnlWDPreview.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlWDPreview.Size = New System.Drawing.Size(862, 644)
        Me.pnlWDPreview.TabIndex = 4
        '
        'wdPreview
        '
        Me.wdPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPreview.Enabled = True
        Me.wdPreview.Location = New System.Drawing.Point(4, 4)
        Me.wdPreview.Name = "wdPreview"
        Me.wdPreview.OcxState = CType(resources.GetObject("wdPreview.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPreview.Size = New System.Drawing.Size(854, 636)
        Me.wdPreview.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(858, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 636)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 636)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Label3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(3, 640)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(856, 1)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(856, 1)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Label1"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.AutoSize = True
        Me.pnlToolStrip.Controls.Add(Me.tls_PreviewStrip)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(862, 53)
        Me.pnlToolStrip.TabIndex = 10
        Me.pnlToolStrip.TabStop = True
        '
        'tls_PreviewStrip
        '
        Me.tls_PreviewStrip.BackColor = System.Drawing.Color.Transparent
        Me.tls_PreviewStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_PreviewStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_PreviewStrip.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.tls_PreviewStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_PreviewStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbtnPrintnCls, Me.ts_btnClose})
        Me.tls_PreviewStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_PreviewStrip.Location = New System.Drawing.Point(0, 0)
        Me.tls_PreviewStrip.Name = "tls_PreviewStrip"
        Me.tls_PreviewStrip.Size = New System.Drawing.Size(862, 53)
        Me.tls_PreviewStrip.TabIndex = 1
        Me.tls_PreviewStrip.Text = "toolStrip1"
        '
        'tlbtnPrintnCls
        '
        Me.tlbtnPrintnCls.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbtnPrintnCls.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbtnPrintnCls.Image = CType(resources.GetObject("tlbtnPrintnCls.Image"), System.Drawing.Image)
        Me.tlbtnPrintnCls.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbtnPrintnCls.Name = "tlbtnPrintnCls"
        Me.tlbtnPrintnCls.Size = New System.Drawing.Size(82, 50)
        Me.tlbtnPrintnCls.Tag = "Print"
        Me.tlbtnPrintnCls.Text = "&Print&&Close"
        Me.tlbtnPrintnCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbtnPrintnCls.ToolTipText = "Print and Close document"
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
        'miniToolStrip
        '
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.BackColor = System.Drawing.Color.Transparent
        Me.miniToolStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.miniToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.miniToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.miniToolStrip.Location = New System.Drawing.Point(85, 0)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(734, 53)
        Me.miniToolStrip.TabIndex = 1
        '
        'frmWordPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(862, 697)
        Me.Controls.Add(Me.pnlWDPreview)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "frmWordPreview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Preview"
        Me.pnlWDPreview.ResumeLayout(False)
        CType(Me.wdPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tls_PreviewStrip.ResumeLayout(False)
        Me.tls_PreviewStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlWDPreview As System.Windows.Forms.Panel
    Friend WithEvents wdPreview As AxDSOFramer.AxFramerControl
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Private WithEvents tls_PreviewStrip As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlbtnPrintnCls As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents miniToolStrip As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
