<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmECGOrderOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmECGOrderOptions))
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.panel7 = New System.Windows.Forms.Panel()
        Me.panel6 = New System.Windows.Forms.Panel()
        Me.panel5 = New System.Windows.Forms.Panel()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.panel3 = New System.Windows.Forms.Panel()
        Me.label7 = New System.Windows.Forms.Label()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.label1 = New System.Windows.Forms.Label()
        Me.btn_RecordEcg = New System.Windows.Forms.Button()
        Me.btn_deviceOrder = New System.Windows.Forms.Button()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.pnlCloseBtn = New System.Windows.Forms.Panel()
        Me.panel2.SuspendLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'panel2
        '
        Me.panel2.BackColor = System.Drawing.Color.White
        Me.panel2.Controls.Add(Me.panel7)
        Me.panel2.Controls.Add(Me.panel6)
        Me.panel2.Controls.Add(Me.panel5)
        Me.panel2.Controls.Add(Me.pictureBox1)
        Me.panel2.Controls.Add(Me.panel3)
        Me.panel2.Controls.Add(Me.label1)
        Me.panel2.Controls.Add(Me.btn_RecordEcg)
        Me.panel2.Controls.Add(Me.btn_deviceOrder)
        Me.panel2.Controls.Add(Me.btn_Cancel)
        Me.panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel2.Location = New System.Drawing.Point(0, 0)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(494, 133)
        Me.panel2.TabIndex = 35
        '
        'panel7
        '
        Me.panel7.BackgroundImage = CType(resources.GetObject("panel7.BackgroundImage"), System.Drawing.Image)
        Me.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panel7.Location = New System.Drawing.Point(3, 130)
        Me.panel7.Name = "panel7"
        Me.panel7.Size = New System.Drawing.Size(488, 3)
        Me.panel7.TabIndex = 9
        '
        'panel6
        '
        Me.panel6.BackgroundImage = CType(resources.GetObject("panel6.BackgroundImage"), System.Drawing.Image)
        Me.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel6.Dock = System.Windows.Forms.DockStyle.Right
        Me.panel6.Location = New System.Drawing.Point(491, 26)
        Me.panel6.Name = "panel6"
        Me.panel6.Size = New System.Drawing.Size(3, 107)
        Me.panel6.TabIndex = 8
        '
        'panel5
        '
        Me.panel5.BackgroundImage = CType(resources.GetObject("panel5.BackgroundImage"), System.Drawing.Image)
        Me.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel5.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel5.Location = New System.Drawing.Point(0, 26)
        Me.panel5.Name = "panel5"
        Me.panel5.Size = New System.Drawing.Size(3, 107)
        Me.panel5.TabIndex = 7
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox1.BackgroundImage = CType(resources.GetObject("pictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pictureBox1.Location = New System.Drawing.Point(22, 46)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(48, 48)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox1.TabIndex = 6
        Me.pictureBox1.TabStop = False
        '
        'panel3
        '
        Me.panel3.BackColor = System.Drawing.Color.Transparent
        Me.panel3.BackgroundImage = CType(resources.GetObject("panel3.BackgroundImage"), System.Drawing.Image)
        Me.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel3.Controls.Add(Me.pnlCloseBtn)
        Me.panel3.Controls.Add(Me.label7)
        Me.panel3.Controls.Add(Me.panel4)
        Me.panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel3.Location = New System.Drawing.Point(0, 0)
        Me.panel3.Name = "panel3"
        Me.panel3.Size = New System.Drawing.Size(494, 26)
        Me.panel3.TabIndex = 5
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.Color.White
        Me.label7.Location = New System.Drawing.Point(23, 7)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(120, 14)
        Me.label7.TabIndex = 4
        Me.label7.Text = "ECG Order Options"
        '
        'panel4
        '
        Me.panel4.BackgroundImage = CType(resources.GetObject("panel4.BackgroundImage"), System.Drawing.Image)
        Me.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.panel4.Location = New System.Drawing.Point(0, 0)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(27, 26)
        Me.panel4.TabIndex = 0
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(76, 53)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(399, 14)
        Me.label1.TabIndex = 3
        Me.label1.Text = "Please choose one of the following options to create a new ECG order."
        '
        'btn_RecordEcg
        '
        Me.btn_RecordEcg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_RecordEcg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn_RecordEcg.Location = New System.Drawing.Point(217, 81)
        Me.btn_RecordEcg.Name = "btn_RecordEcg"
        Me.btn_RecordEcg.Size = New System.Drawing.Size(161, 24)
        Me.btn_RecordEcg.TabIndex = 2
        Me.btn_RecordEcg.Tag = "RecordECG"
        Me.btn_RecordEcg.Text = "Record ECG manually"
        Me.btn_RecordEcg.UseVisualStyleBackColor = True
        '
        'btn_deviceOrder
        '
        Me.btn_deviceOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_deviceOrder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn_deviceOrder.Location = New System.Drawing.Point(90, 81)
        Me.btn_deviceOrder.Name = "btn_deviceOrder"
        Me.btn_deviceOrder.Size = New System.Drawing.Size(122, 24)
        Me.btn_deviceOrder.TabIndex = 1
        Me.btn_deviceOrder.Tag = "DeviceOrder"
        Me.btn_deviceOrder.Text = "ECG from device"
        Me.btn_deviceOrder.UseVisualStyleBackColor = True
        '
        'btn_Cancel
        '
        Me.btn_Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn_Cancel.Location = New System.Drawing.Point(384, 81)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(73, 24)
        Me.btn_Cancel.TabIndex = 0
        Me.btn_Cancel.Tag = "Cancel"
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'pnlCloseBtn
        '
        Me.pnlCloseBtn.BackgroundImage = CType(resources.GetObject("pnlCloseBtn.BackgroundImage"), System.Drawing.Image)
        Me.pnlCloseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pnlCloseBtn.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlCloseBtn.Location = New System.Drawing.Point(467, 0)
        Me.pnlCloseBtn.Name = "pnlCloseBtn"
        Me.pnlCloseBtn.Size = New System.Drawing.Size(27, 26)
        Me.pnlCloseBtn.TabIndex = 5
        '
        'frmECGOrderOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 133)
        Me.Controls.Add(Me.panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmECGOrderOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmECGOrderOptions"
        Me.panel2.ResumeLayout(False)
        Me.panel2.PerformLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel3.ResumeLayout(False)
        Me.panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents panel7 As System.Windows.Forms.Panel
    Private WithEvents panel6 As System.Windows.Forms.Panel
    Private WithEvents panel5 As System.Windows.Forms.Panel
    Private WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents panel3 As System.Windows.Forms.Panel
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents panel4 As System.Windows.Forms.Panel
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents btn_RecordEcg As System.Windows.Forms.Button
    Private WithEvents btn_deviceOrder As System.Windows.Forms.Button
    Private WithEvents btn_Cancel As System.Windows.Forms.Button
    Private WithEvents pnlCloseBtn As System.Windows.Forms.Panel
End Class
