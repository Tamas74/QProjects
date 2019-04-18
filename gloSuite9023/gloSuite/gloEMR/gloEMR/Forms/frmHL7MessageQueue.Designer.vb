<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHL7MessageQueue
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHL7MessageQueue))
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.tlsTriage = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlReceivedTriage1 = New System.Windows.Forms.Panel
        Me.pnlToolStrip.SuspendLayout()
        Me.tlsTriage.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.tlsTriage)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(632, 57)
        Me.pnlToolStrip.TabIndex = 16
        '
        'tlsTriage
        '
        Me.tlsTriage.BackColor = System.Drawing.Color.Transparent
        Me.tlsTriage.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsTriage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsTriage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlsTriage.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsTriage.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsTriage.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsTriage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnRefresh, Me.ts_btnClose})
        Me.tlsTriage.Location = New System.Drawing.Point(0, 0)
        Me.tlsTriage.Name = "tlsTriage"
        Me.tlsTriage.Size = New System.Drawing.Size(632, 57)
        Me.tlsTriage.TabIndex = 0
        Me.tlsTriage.Text = "ToolStrip1"
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 54)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 54)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlReceivedTriage1
        '
        Me.pnlReceivedTriage1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlReceivedTriage1.Location = New System.Drawing.Point(0, 57)
        Me.pnlReceivedTriage1.Name = "pnlReceivedTriage1"
        Me.pnlReceivedTriage1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlReceivedTriage1.Size = New System.Drawing.Size(632, 558)
        Me.pnlReceivedTriage1.TabIndex = 17
        '
        'frmHL7MessageQueue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(632, 615)
        Me.Controls.Add(Me.pnlReceivedTriage1)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmHL7MessageQueue"
        Me.ShowInTaskbar = False
        Me.Text = "HL7 Message Queue"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tlsTriage.ResumeLayout(False)
        Me.tlsTriage.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tlsTriage As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlReceivedTriage1 As System.Windows.Forms.Panel
End Class
