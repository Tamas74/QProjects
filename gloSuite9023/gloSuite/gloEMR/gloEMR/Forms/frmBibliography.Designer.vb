<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBibliography
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBibliography))
        Me.pnlBibliographic = New System.Windows.Forms.Panel()
        Me.txtDeveloper = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtBibliography = New System.Windows.Forms.RichTextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.tls_Bibliography = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_Saveandclose = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlBibliographic.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.tls_Bibliography.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBibliographic
        '
        Me.pnlBibliographic.BackColor = System.Drawing.Color.Transparent
        Me.pnlBibliographic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBibliographic.Controls.Add(Me.txtDeveloper)
        Me.pnlBibliographic.Controls.Add(Me.Label1)
        Me.pnlBibliographic.Controls.Add(Me.Label24)
        Me.pnlBibliographic.Controls.Add(Me.txtBibliography)
        Me.pnlBibliographic.Controls.Add(Me.Label25)
        Me.pnlBibliographic.Controls.Add(Me.Label35)
        Me.pnlBibliographic.Controls.Add(Me.Label36)
        Me.pnlBibliographic.Controls.Add(Me.Label37)
        Me.pnlBibliographic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBibliographic.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlBibliographic.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlBibliographic.Location = New System.Drawing.Point(0, 60)
        Me.pnlBibliographic.Name = "pnlBibliographic"
        Me.pnlBibliographic.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlBibliographic.Size = New System.Drawing.Size(718, 361)
        Me.pnlBibliographic.TabIndex = 0
        '
        'txtDeveloper
        '
        Me.txtDeveloper.Location = New System.Drawing.Point(161, 227)
        Me.txtDeveloper.Name = "txtDeveloper"
        Me.txtDeveloper.Size = New System.Drawing.Size(537, 117)
        Me.txtDeveloper.TabIndex = 2
        Me.txtDeveloper.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(12, 230)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Intervention Developer :"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(4, 357)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(710, 1)
        Me.Label24.TabIndex = 28
        '
        'txtBibliography
        '
        Me.txtBibliography.Location = New System.Drawing.Point(161, 18)
        Me.txtBibliography.Name = "txtBibliography"
        Me.txtBibliography.Size = New System.Drawing.Size(537, 202)
        Me.txtBibliography.TabIndex = 1
        Me.txtBibliography.Text = ""
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Location = New System.Drawing.Point(28, 20)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(130, 13)
        Me.Label25.TabIndex = 9
        Me.Label25.Text = "Bibliography Citation :"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Location = New System.Drawing.Point(714, 4)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 354)
        Me.Label35.TabIndex = 30
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Location = New System.Drawing.Point(3, 4)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1, 354)
        Me.Label36.TabIndex = 29
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Location = New System.Drawing.Point(3, 3)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(712, 1)
        Me.Label37.TabIndex = 27
        '
        'Panel7
        '
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.tls_Bibliography)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(718, 60)
        Me.Panel7.TabIndex = 1
        '
        'tls_Bibliography
        '
        Me.tls_Bibliography.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tls_Bibliography.BackgroundImage = CType(resources.GetObject("tls_Bibliography.BackgroundImage"), System.Drawing.Image)
        Me.tls_Bibliography.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_Bibliography.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tls_Bibliography.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Bibliography.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_Bibliography.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_Bibliography.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Saveandclose, Me.ts_btnClose})
        Me.tls_Bibliography.Location = New System.Drawing.Point(0, 0)
        Me.tls_Bibliography.Name = "tls_Bibliography"
        Me.tls_Bibliography.Size = New System.Drawing.Size(718, 60)
        Me.tls_Bibliography.TabIndex = 1
        Me.tls_Bibliography.TabStop = True
        Me.tls_Bibliography.Text = "ToolStrip1"
        '
        'ts_Saveandclose
        '
        Me.ts_Saveandclose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_Saveandclose.Image = CType(resources.GetObject("ts_Saveandclose.Image"), System.Drawing.Image)
        Me.ts_Saveandclose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_Saveandclose.Name = "ts_Saveandclose"
        Me.ts_Saveandclose.Size = New System.Drawing.Size(66, 57)
        Me.ts_Saveandclose.Tag = "Save&Close"
        Me.ts_Saveandclose.Text = "&Save&&Cls"
        Me.ts_Saveandclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 57)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmBibliography
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(718, 421)
        Me.Controls.Add(Me.pnlBibliographic)
        Me.Controls.Add(Me.Panel7)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBibliography"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bibliographic and Developer Citation"
        Me.pnlBibliographic.ResumeLayout(False)
        Me.pnlBibliographic.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.tls_Bibliography.ResumeLayout(False)
        Me.tls_Bibliography.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlBibliographic As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtDeveloper As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tls_Bibliography As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_Saveandclose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtBibliography As System.Windows.Forms.RichTextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
End Class
