<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdatePayerID
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdatePayerID))
        Me.pnt_tlStrip = New System.Windows.Forms.Panel
        Me.ts_Commands = New System.Windows.Forms.ToolStrip
        Me.tsb_OK = New System.Windows.Forms.ToolStripButton
        Me.tsb_Cancel = New System.Windows.Forms.ToolStripButton
        Me.pnl_RestoreInfo = New System.Windows.Forms.Panel
        Me.btnClearFileName = New System.Windows.Forms.Button
        Me.label2 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.btnBrowseFile = New System.Windows.Forms.Button
        Me.txtCSVFileName = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.dlgBrowseFile = New System.Windows.Forms.OpenFileDialog
        Me.pnt_tlStrip.SuspendLayout()
        Me.ts_Commands.SuspendLayout()
        Me.pnl_RestoreInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnt_tlStrip
        '
        Me.pnt_tlStrip.Controls.Add(Me.ts_Commands)
        Me.pnt_tlStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnt_tlStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnt_tlStrip.Name = "pnt_tlStrip"
        Me.pnt_tlStrip.Size = New System.Drawing.Size(400, 54)
        Me.pnt_tlStrip.TabIndex = 2
        '
        'ts_Commands
        '
        Me.ts_Commands.BackColor = System.Drawing.Color.Transparent
        Me.ts_Commands.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Toolstrip
        Me.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_Commands.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_Commands.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_Commands.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsb_OK, Me.tsb_Cancel})
        Me.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ts_Commands.Location = New System.Drawing.Point(0, 0)
        Me.ts_Commands.Name = "ts_Commands"
        Me.ts_Commands.Size = New System.Drawing.Size(400, 53)
        Me.ts_Commands.TabIndex = 33
        Me.ts_Commands.Text = "ToolStrip1"
        '
        'tsb_OK
        '
        Me.tsb_OK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_OK.Image = CType(resources.GetObject("tsb_OK.Image"), System.Drawing.Image)
        Me.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_OK.Name = "tsb_OK"
        Me.tsb_OK.Size = New System.Drawing.Size(66, 50)
        Me.tsb_OK.Tag = "OK"
        Me.tsb_OK.Text = "&Save&&Cls"
        Me.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_OK.ToolTipText = "Save and Close"
        '
        'tsb_Cancel
        '
        Me.tsb_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Cancel.Image = CType(resources.GetObject("tsb_Cancel.Image"), System.Drawing.Image)
        Me.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Cancel.Name = "tsb_Cancel"
        Me.tsb_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.tsb_Cancel.Tag = "Cancel"
        Me.tsb_Cancel.Text = "&Close"
        Me.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        Me.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnl_RestoreInfo
        '
        Me.pnl_RestoreInfo.Controls.Add(Me.btnClearFileName)
        Me.pnl_RestoreInfo.Controls.Add(Me.label2)
        Me.pnl_RestoreInfo.Controls.Add(Me.label3)
        Me.pnl_RestoreInfo.Controls.Add(Me.label4)
        Me.pnl_RestoreInfo.Controls.Add(Me.label5)
        Me.pnl_RestoreInfo.Controls.Add(Me.btnBrowseFile)
        Me.pnl_RestoreInfo.Controls.Add(Me.txtCSVFileName)
        Me.pnl_RestoreInfo.Controls.Add(Me.Label32)
        Me.pnl_RestoreInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_RestoreInfo.Location = New System.Drawing.Point(0, 54)
        Me.pnl_RestoreInfo.Name = "pnl_RestoreInfo"
        Me.pnl_RestoreInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_RestoreInfo.Size = New System.Drawing.Size(400, 61)
        Me.pnl_RestoreInfo.TabIndex = 3
        '
        'btnClearFileName
        '
        Me.btnClearFileName.AutoEllipsis = True
        Me.btnClearFileName.BackColor = System.Drawing.Color.Transparent
        Me.btnClearFileName.BackgroundImage = CType(resources.GetObject("btnClearFileName.BackgroundImage"), System.Drawing.Image)
        Me.btnClearFileName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearFileName.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnClearFileName.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnClearFileName.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearFileName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearFileName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearFileName.Image = CType(resources.GetObject("btnClearFileName.Image"), System.Drawing.Image)
        Me.btnClearFileName.Location = New System.Drawing.Point(359, 18)
        Me.btnClearFileName.Name = "btnClearFileName"
        Me.btnClearFileName.Size = New System.Drawing.Size(22, 22)
        Me.btnClearFileName.TabIndex = 94
        Me.btnClearFileName.UseVisualStyleBackColor = False
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label2.Location = New System.Drawing.Point(4, 57)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(392, 1)
        Me.label2.TabIndex = 93
        Me.label2.Text = "label2"
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.Location = New System.Drawing.Point(3, 4)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(1, 54)
        Me.label3.TabIndex = 92
        Me.label3.Text = "label4"
        '
        'label4
        '
        Me.label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label4.Location = New System.Drawing.Point(396, 4)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(1, 54)
        Me.label4.TabIndex = 91
        Me.label4.Text = "label3"
        '
        'label5
        '
        Me.label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.Location = New System.Drawing.Point(3, 3)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(394, 1)
        Me.label5.TabIndex = 90
        Me.label5.Text = "label1"
        '
        'btnBrowseFile
        '
        Me.btnBrowseFile.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseFile.BackgroundImage = CType(resources.GetObject("btnBrowseFile.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseFile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseFile.Image = CType(resources.GetObject("btnBrowseFile.Image"), System.Drawing.Image)
        Me.btnBrowseFile.Location = New System.Drawing.Point(333, 18)
        Me.btnBrowseFile.Name = "btnBrowseFile"
        Me.btnBrowseFile.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseFile.TabIndex = 89
        Me.btnBrowseFile.UseVisualStyleBackColor = False
        '
        'txtCSVFileName
        '
        Me.txtCSVFileName.BackColor = System.Drawing.Color.White
        Me.txtCSVFileName.ForeColor = System.Drawing.Color.Black
        Me.txtCSVFileName.Location = New System.Drawing.Point(90, 20)
        Me.txtCSVFileName.Name = "txtCSVFileName"
        Me.txtCSVFileName.ReadOnly = True
        Me.txtCSVFileName.Size = New System.Drawing.Size(240, 20)
        Me.txtCSVFileName.TabIndex = 81
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(20, 23)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(67, 14)
        Me.Label32.TabIndex = 83
        Me.Label32.Text = "File Name :"
        '
        'frmUpdatePayerID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(400, 115)
        Me.Controls.Add(Me.pnl_RestoreInfo)
        Me.Controls.Add(Me.pnt_tlStrip)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdatePayerID"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Update PayerID"
        Me.pnt_tlStrip.ResumeLayout(False)
        Me.pnt_tlStrip.PerformLayout()
        Me.ts_Commands.ResumeLayout(False)
        Me.ts_Commands.PerformLayout()
        Me.pnl_RestoreInfo.ResumeLayout(False)
        Me.pnl_RestoreInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnt_tlStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_Commands As System.Windows.Forms.ToolStrip
    Friend WithEvents tsb_OK As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsb_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_RestoreInfo As System.Windows.Forms.Panel
    Private WithEvents btnClearFileName As System.Windows.Forms.Button
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents btnBrowseFile As System.Windows.Forms.Button
    Private WithEvents txtCSVFileName As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents dlgBrowseFile As System.Windows.Forms.OpenFileDialog
End Class
