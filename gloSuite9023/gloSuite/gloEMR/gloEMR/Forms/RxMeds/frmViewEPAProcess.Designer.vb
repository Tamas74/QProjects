<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewEPAProcess
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewEPAProcess))
        Me.pnlToolstrip = New System.Windows.Forms.Panel()
        Me.tls_Main = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlAccelerator = New System.Windows.Forms.Panel()
        Me.lblBottom = New System.Windows.Forms.Label()
        Me.lblGridBottom = New System.Windows.Forms.Label()
        Me.lblGridLeft = New System.Windows.Forms.Label()
        Me.lblGridRight = New System.Windows.Forms.Label()
        Me.pnlToolstrip.SuspendLayout()
        Me.tls_Main.SuspendLayout()
        Me.pnlAccelerator.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolstrip
        '
        Me.pnlToolstrip.AutoSize = True
        Me.pnlToolstrip.Controls.Add(Me.tls_Main)
        Me.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolstrip.Name = "pnlToolstrip"
        Me.pnlToolstrip.Size = New System.Drawing.Size(974, 56)
        Me.pnlToolstrip.TabIndex = 30
        '
        'tls_Main
        '
        Me.tls_Main.AutoSize = False
        Me.tls_Main.BackgroundImage = CType(resources.GetObject("tls_Main.BackgroundImage"), System.Drawing.Image)
        Me.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_Main.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_Main.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtn_Close})
        Me.tls_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.tls_Main.Location = New System.Drawing.Point(0, 0)
        Me.tls_Main.Name = "tls_Main"
        Me.tls_Main.Size = New System.Drawing.Size(974, 56)
        Me.tls_Main.TabIndex = 3
        Me.tls_Main.Text = "ToolStrip1"
        '
        'tlsbtn_Close
        '
        Me.tlsbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtn_Close.Image = CType(resources.GetObject("tlsbtn_Close.Image"), System.Drawing.Image)
        Me.tlsbtn_Close.Name = "tlsbtn_Close"
        Me.tlsbtn_Close.Size = New System.Drawing.Size(43, 53)
        Me.tlsbtn_Close.Tag = "Close"
        Me.tlsbtn_Close.Text = "&Close"
        Me.tlsbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtn_Close.ToolTipText = "Close"
        '
        'pnlAccelerator
        '
        Me.pnlAccelerator.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlAccelerator.Controls.Add(Me.lblBottom)
        Me.pnlAccelerator.Controls.Add(Me.lblGridBottom)
        Me.pnlAccelerator.Controls.Add(Me.lblGridLeft)
        Me.pnlAccelerator.Controls.Add(Me.lblGridRight)
        Me.pnlAccelerator.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAccelerator.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlAccelerator.Location = New System.Drawing.Point(0, 56)
        Me.pnlAccelerator.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlAccelerator.Name = "pnlAccelerator"
        Me.pnlAccelerator.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlAccelerator.Size = New System.Drawing.Size(974, 607)
        Me.pnlAccelerator.TabIndex = 33
        '
        'lblBottom
        '
        Me.lblBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblBottom.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblBottom.Location = New System.Drawing.Point(4, 3)
        Me.lblBottom.Name = "lblBottom"
        Me.lblBottom.Size = New System.Drawing.Size(966, 1)
        Me.lblBottom.TabIndex = 13
        Me.lblBottom.Text = "label1"
        '
        'lblGridBottom
        '
        Me.lblGridBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblGridBottom.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblGridBottom.Location = New System.Drawing.Point(4, 603)
        Me.lblGridBottom.Name = "lblGridBottom"
        Me.lblGridBottom.Size = New System.Drawing.Size(966, 1)
        Me.lblGridBottom.TabIndex = 10
        Me.lblGridBottom.Text = "label2"
        '
        'lblGridLeft
        '
        Me.lblGridLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblGridLeft.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGridLeft.Location = New System.Drawing.Point(3, 3)
        Me.lblGridLeft.Name = "lblGridLeft"
        Me.lblGridLeft.Size = New System.Drawing.Size(1, 601)
        Me.lblGridLeft.TabIndex = 9
        Me.lblGridLeft.Text = "label4"
        '
        'lblGridRight
        '
        Me.lblGridRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblGridRight.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblGridRight.Location = New System.Drawing.Point(970, 3)
        Me.lblGridRight.Name = "lblGridRight"
        Me.lblGridRight.Size = New System.Drawing.Size(1, 601)
        Me.lblGridRight.TabIndex = 8
        Me.lblGridRight.Text = "label3"
        '
        'frmViewEPAProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 663)
        Me.Controls.Add(Me.pnlAccelerator)
        Me.Controls.Add(Me.pnlToolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewEPAProcess"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EPA Process View"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlToolstrip.ResumeLayout(False)
        Me.tls_Main.ResumeLayout(False)
        Me.tls_Main.PerformLayout()
        Me.pnlAccelerator.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlToolstrip As System.Windows.Forms.Panel
    Friend WithEvents tls_Main As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlAccelerator As System.Windows.Forms.Panel
    Private WithEvents lblBottom As System.Windows.Forms.Label
    Private WithEvents lblGridBottom As System.Windows.Forms.Label
    Private WithEvents lblGridLeft As System.Windows.Forms.Label
    Private WithEvents lblGridRight As System.Windows.Forms.Label
End Class
