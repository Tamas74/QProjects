<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAccBusCenterUtility
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
        Me.panel6 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CPTStatus = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label9 = New System.Windows.Forms.Label()
        Me.panel6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.CPTStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'panel6
        '
        Me.panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel6.Controls.Add(Me.Panel1)
        Me.panel6.Controls.Add(Me.Label2)
        Me.panel6.Controls.Add(Me.label8)
        Me.panel6.Controls.Add(Me.label1)
        Me.panel6.Controls.Add(Me.label7)
        Me.panel6.Controls.Add(Me.label9)
        Me.panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel6.Location = New System.Drawing.Point(0, 0)
        Me.panel6.Name = "panel6"
        Me.panel6.Padding = New System.Windows.Forms.Padding(3)
        Me.panel6.Size = New System.Drawing.Size(635, 82)
        Me.panel6.TabIndex = 35
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CPTStatus)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(4, 35)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(627, 43)
        Me.Panel1.TabIndex = 34
        '
        'CPTStatus
        '
        Me.CPTStatus.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.CPTStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CPTStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CPTStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.ProgressBar})
        Me.CPTStatus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.CPTStatus.Location = New System.Drawing.Point(0, 1)
        Me.CPTStatus.Name = "CPTStatus"
        Me.CPTStatus.Padding = New System.Windows.Forms.Padding(1, 0, 22, 0)
        Me.CPTStatus.Size = New System.Drawing.Size(627, 42)
        Me.CPTStatus.TabIndex = 21
        Me.CPTStatus.Text = "statusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(131, 14)
        Me.lblStatus.Text = "   Processing Accounts"
        '
        'ProgressBar
        '
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(632, 19)
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(627, 1)
        Me.Label3.TabIndex = 33
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(199, 14)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Account Business Center Utility"
        '
        'label8
        '
        Me.label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.label8.Location = New System.Drawing.Point(4, 3)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(627, 1)
        Me.label8.TabIndex = 31
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.label1.Location = New System.Drawing.Point(3, 3)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(1, 75)
        Me.label1.TabIndex = 29
        '
        'label7
        '
        Me.label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.label7.Location = New System.Drawing.Point(631, 3)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(1, 75)
        Me.label7.TabIndex = 30
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Location = New System.Drawing.Point(3, 78)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(629, 1)
        Me.label9.TabIndex = 32
        '
        'frmAccBusCenterUtility
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 82)
        Me.Controls.Add(Me.panel6)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmAccBusCenterUtility"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Account Business Center Utility"
        Me.panel6.ResumeLayout(False)
        Me.panel6.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.CPTStatus.ResumeLayout(False)
        Me.CPTStatus.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents panel6 As System.Windows.Forms.Panel
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents CPTStatus As System.Windows.Forms.StatusStrip
    Private WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
