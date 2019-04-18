<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmErrorDetailsMessageBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmErrorDetailsMessageBox))
        Me.lblErrorHeading = New System.Windows.Forms.Label()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnDetails = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlErrorDetails = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtErrorDetails = New System.Windows.Forms.TextBox()
        Me.pnlButtons.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlErrorDetails.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblErrorHeading
        '
        Me.lblErrorHeading.Location = New System.Drawing.Point(55, 11)
        Me.lblErrorHeading.Name = "lblErrorHeading"
        Me.lblErrorHeading.Size = New System.Drawing.Size(362, 39)
        Me.lblErrorHeading.TabIndex = 0
        Me.lblErrorHeading.Text = "The following error occurred while receiving CDS file."
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnOk)
        Me.pnlButtons.Controls.Add(Me.btnDetails)
        Me.pnlButtons.Location = New System.Drawing.Point(8, 68)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(410, 31)
        Me.pnlButtons.TabIndex = 3
        '
        'btnOk
        '
        Me.btnOk.BackgroundImage = CType(resources.GetObject("btnOk.BackgroundImage"), System.Drawing.Image)
        Me.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOk.Location = New System.Drawing.Point(310, 3)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(89, 26)
        Me.btnOk.TabIndex = 1
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnDetails
        '
        Me.btnDetails.AutoSize = True
        Me.btnDetails.BackgroundImage = CType(resources.GetObject("btnDetails.BackgroundImage"), System.Drawing.Image)
        Me.btnDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDetails.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDetails.Location = New System.Drawing.Point(2, 3)
        Me.btnDetails.Name = "btnDetails"
        Me.btnDetails.Size = New System.Drawing.Size(89, 26)
        Me.btnDetails.TabIndex = 0
        Me.btnDetails.Text = "Show Details"
        Me.btnDetails.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlErrorDetails)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(426, 105)
        Me.Panel2.TabIndex = 2
        '
        'pnlErrorDetails
        '
        Me.pnlErrorDetails.Controls.Add(Me.pnlButtons)
        Me.pnlErrorDetails.Controls.Add(Me.lblErrorHeading)
        Me.pnlErrorDetails.Controls.Add(Me.PictureBox1)
        Me.pnlErrorDetails.Controls.Add(Me.txtErrorDetails)
        Me.pnlErrorDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlErrorDetails.Location = New System.Drawing.Point(0, 0)
        Me.pnlErrorDetails.Name = "pnlErrorDetails"
        Me.pnlErrorDetails.Size = New System.Drawing.Size(426, 105)
        Me.pnlErrorDetails.TabIndex = 3
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(9, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(42, 41)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'txtErrorDetails
        '
        Me.txtErrorDetails.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtErrorDetails.Location = New System.Drawing.Point(8, 105)
        Me.txtErrorDetails.Multiline = True
        Me.txtErrorDetails.Name = "txtErrorDetails"
        Me.txtErrorDetails.ReadOnly = True
        Me.txtErrorDetails.Size = New System.Drawing.Size(410, 225)
        Me.txtErrorDetails.TabIndex = 1
        '
        'FrmErrorDetailsMessageBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(426, 105)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmErrorDetailsMessageBox"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "gloEMR"
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlButtons.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.pnlErrorDetails.ResumeLayout(False)
        Me.pnlErrorDetails.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblErrorHeading As System.Windows.Forms.Label
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnDetails As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlErrorDetails As System.Windows.Forms.Panel
    Friend WithEvents txtErrorDetails As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

End Class
