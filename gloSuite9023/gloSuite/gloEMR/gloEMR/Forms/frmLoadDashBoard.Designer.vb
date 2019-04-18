<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoadDashBoard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoadDashBoard))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblLastModifiedDate = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCopyrghTag = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lbl_mktngversion = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.lblLastModifiedDate)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.lblDescription)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.lbl_mktngversion)
        Me.Panel1.Controls.Add(Me.lblVersion)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(837, 605)
        Me.Panel1.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(657, 463)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(12, 14)
        Me.PictureBox1.TabIndex = 161
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'lblLastModifiedDate
        '
        Me.lblLastModifiedDate.AutoSize = True
        Me.lblLastModifiedDate.BackColor = System.Drawing.Color.Transparent
        Me.lblLastModifiedDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblLastModifiedDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLastModifiedDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(174, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.lblLastModifiedDate.Location = New System.Drawing.Point(23, 502)
        Me.lblLastModifiedDate.Name = "lblLastModifiedDate"
        Me.lblLastModifiedDate.Size = New System.Drawing.Size(70, 14)
        Me.lblLastModifiedDate.TabIndex = 6
        Me.lblLastModifiedDate.Text = "Mar 04, 2016"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.lblCopyrghTag)
        Me.Panel2.Location = New System.Drawing.Point(24, 520)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(801, 79)
        Me.Panel2.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.05!)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(174, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(0, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(798, 71)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'lblCopyrghTag
        '
        Me.lblCopyrghTag.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblCopyrghTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblCopyrghTag.Font = New System.Drawing.Font("Arial", 8.05!)
        Me.lblCopyrghTag.ForeColor = System.Drawing.Color.FromArgb(CType(CType(174, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.lblCopyrghTag.Location = New System.Drawing.Point(0, 0)
        Me.lblCopyrghTag.Name = "lblCopyrghTag"
        Me.lblCopyrghTag.Size = New System.Drawing.Size(801, 14)
        Me.lblCopyrghTag.TabIndex = 8
        Me.lblCopyrghTag.Text = "CPT® copyright 2015 American Medical Association. All rights reserved."
        '
        'lblDescription
        '
        Me.lblDescription.BackColor = System.Drawing.Color.Transparent
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.lblDescription.Location = New System.Drawing.Point(588, 22)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(232, 22)
        Me.lblDescription.TabIndex = 1
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Arial", 5.0!)
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(763, 470)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 7)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "TM"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.Label5.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Location = New System.Drawing.Point(786, 463)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 10)
        Me.Panel3.TabIndex = 28
        Me.Panel3.Visible = False
        '
        'lbl_mktngversion
        '
        Me.lbl_mktngversion.AutoSize = True
        Me.lbl_mktngversion.BackColor = System.Drawing.Color.Transparent
        Me.lbl_mktngversion.Font = New System.Drawing.Font("Arial", 21.05!, System.Drawing.FontStyle.Bold)
        Me.lbl_mktngversion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lbl_mktngversion.Location = New System.Drawing.Point(502, 394)
        Me.lbl_mktngversion.Name = "lbl_mktngversion"
        Me.lbl_mktngversion.Size = New System.Drawing.Size(0, 34)
        Me.lbl_mktngversion.TabIndex = 27
        Me.lbl_mktngversion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_mktngversion.Visible = False
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Trebuchet MS", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblVersion.Location = New System.Drawing.Point(699, 463)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(58, 18)
        Me.lblVersion.TabIndex = 8
        Me.lblVersion.Text = "Version "
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVersion.Visible = False
        '
        'frmLoadDashBoard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(837, 605)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLoadDashBoard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Loading gloEMR"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCopyrghTag As System.Windows.Forms.Label
    Private WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lbl_mktngversion As System.Windows.Forms.Label
    Friend WithEvents lblLastModifiedDate As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
