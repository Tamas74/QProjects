<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSocialPsychologicalBehaviorData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSocialPsychologicalBehaviorData))
        Me.pnlSBPToolBar = New System.Windows.Forms.Panel()
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tlstrpbtnSBPHistory = New System.Windows.Forms.ToolStripButton()
        Me.tlstrpBtnSaveClose = New System.Windows.Forms.ToolStripButton()
        Me.tlstrpBtnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlWebbrowserCtrl = New System.Windows.Forms.Panel()
        Me.pnlSBPToolBar.SuspendLayout()
        Me.toolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSBPToolBar
        '
        Me.pnlSBPToolBar.AutoSize = True
        Me.pnlSBPToolBar.Controls.Add(Me.toolStrip1)
        Me.pnlSBPToolBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSBPToolBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlSBPToolBar.Name = "pnlSBPToolBar"
        Me.pnlSBPToolBar.Size = New System.Drawing.Size(849, 53)
        Me.pnlSBPToolBar.TabIndex = 2
        '
        'toolStrip1
        '
        Me.toolStrip1.BackgroundImage = CType(resources.GetObject("toolStrip1.BackgroundImage"), System.Drawing.Image)
        Me.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.toolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlstrpbtnSBPHistory, Me.tlstrpBtnSaveClose, Me.tlstrpBtnClose})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.Size = New System.Drawing.Size(849, 53)
        Me.toolStrip1.TabIndex = 0
        Me.toolStrip1.Text = "toolStrip1"
        '
        'tlstrpbtnSBPHistory
        '
        Me.tlstrpbtnSBPHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlstrpbtnSBPHistory.Image = CType(resources.GetObject("tlstrpbtnSBPHistory.Image"), System.Drawing.Image)
        Me.tlstrpbtnSBPHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlstrpbtnSBPHistory.Name = "tlstrpbtnSBPHistory"
        Me.tlstrpbtnSBPHistory.Size = New System.Drawing.Size(93, 50)
        Me.tlstrpbtnSBPHistory.Text = "Audit History"
        Me.tlstrpbtnSBPHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlstrpBtnSaveClose
        '
        Me.tlstrpBtnSaveClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlstrpBtnSaveClose.Image = CType(resources.GetObject("tlstrpBtnSaveClose.Image"), System.Drawing.Image)
        Me.tlstrpBtnSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlstrpBtnSaveClose.Name = "tlstrpBtnSaveClose"
        Me.tlstrpBtnSaveClose.Size = New System.Drawing.Size(66, 50)
        Me.tlstrpBtnSaveClose.Text = "&Save&&Cls"
        Me.tlstrpBtnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlstrpBtnSaveClose.ToolTipText = "Save and Close"
        '
        'tlstrpBtnClose
        '
        Me.tlstrpBtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlstrpBtnClose.Image = CType(resources.GetObject("tlstrpBtnClose.Image"), System.Drawing.Image)
        Me.tlstrpBtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlstrpBtnClose.Name = "tlstrpBtnClose"
        Me.tlstrpBtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlstrpBtnClose.Text = "&Close"
        Me.tlstrpBtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlWebbrowserCtrl
        '
        Me.pnlWebbrowserCtrl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWebbrowserCtrl.Location = New System.Drawing.Point(0, 53)
        Me.pnlWebbrowserCtrl.Name = "pnlWebbrowserCtrl"
        Me.pnlWebbrowserCtrl.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlWebbrowserCtrl.Size = New System.Drawing.Size(849, 573)
        Me.pnlWebbrowserCtrl.TabIndex = 3
        '
        'frmSocialPsychologicalBehaviorData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(849, 626)
        Me.Controls.Add(Me.pnlWebbrowserCtrl)
        Me.Controls.Add(Me.pnlSBPToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSocialPsychologicalBehaviorData"
        Me.Text = "Social Psychological Behavior Data"
        Me.pnlSBPToolBar.ResumeLayout(False)
        Me.pnlSBPToolBar.PerformLayout()
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents pnlSBPToolBar As System.Windows.Forms.Panel
    Private WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Private WithEvents tlstrpbtnSBPHistory As System.Windows.Forms.ToolStripButton
    Private WithEvents tlstrpBtnSaveClose As System.Windows.Forms.ToolStripButton
    Private WithEvents tlstrpBtnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnlWebbrowserCtrl As System.Windows.Forms.Panel
End Class
