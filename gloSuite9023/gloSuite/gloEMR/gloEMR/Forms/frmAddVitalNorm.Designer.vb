<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddVitalNorm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddVitalNorm))
        Me.txtOldFrom = New System.Windows.Forms.TextBox()
        Me.txtOldTo = New System.Windows.Forms.TextBox()
        Me.txtNewFrom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tblStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_Ok_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.lblOldFromAge = New System.Windows.Forms.Label()
        Me.lblNewFromAge = New System.Windows.Forms.Label()
        Me.lblToAge = New System.Windows.Forms.Label()
        Me.lblInstruction = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnltblStrip = New System.Windows.Forms.Panel()
        Me.tblStrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnltblStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtOldFrom
        '
        Me.txtOldFrom.Enabled = False
        Me.txtOldFrom.Location = New System.Drawing.Point(109, 12)
        Me.txtOldFrom.Name = "txtOldFrom"
        Me.txtOldFrom.Size = New System.Drawing.Size(116, 22)
        Me.txtOldFrom.TabIndex = 0
        '
        'txtOldTo
        '
        Me.txtOldTo.Enabled = False
        Me.txtOldTo.Location = New System.Drawing.Point(109, 66)
        Me.txtOldTo.Name = "txtOldTo"
        Me.txtOldTo.Size = New System.Drawing.Size(116, 22)
        Me.txtOldTo.TabIndex = 2
        '
        'txtNewFrom
        '
        Me.txtNewFrom.Location = New System.Drawing.Point(109, 39)
        Me.txtNewFrom.MaxLength = 3
        Me.txtNewFrom.Name = "txtNewFrom"
        Me.txtNewFrom.Size = New System.Drawing.Size(116, 22)
        Me.txtNewFrom.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 14)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Old from Age :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "New to Age :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Old to Age :"
        '
        'tblStrip
        '
        Me.tblStrip.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Ok_32, Me.tblbtn_Close_32})
        Me.tblStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip.Name = "tblStrip"
        Me.tblStrip.Size = New System.Drawing.Size(398, 53)
        Me.tblStrip.TabIndex = 6
        Me.tblStrip.Text = "ToolStrip1"
        '
        'tblbtn_Ok_32
        '
        Me.tblbtn_Ok_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Ok_32.Image = CType(resources.GetObject("tblbtn_Ok_32.Image"), System.Drawing.Image)
        Me.tblbtn_Ok_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Ok_32.Name = "tblbtn_Ok_32"
        Me.tblbtn_Ok_32.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Ok_32.Tag = "Ok"
        Me.tblbtn_Ok_32.Text = "&Save&&Cls"
        Me.tblbtn_Ok_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Ok_32.ToolTipText = "Save and Close"
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Tag = "Close"
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'lblOldFromAge
        '
        Me.lblOldFromAge.AutoSize = True
        Me.lblOldFromAge.Location = New System.Drawing.Point(227, 16)
        Me.lblOldFromAge.Name = "lblOldFromAge"
        Me.lblOldFromAge.Size = New System.Drawing.Size(0, 14)
        Me.lblOldFromAge.TabIndex = 7
        '
        'lblNewFromAge
        '
        Me.lblNewFromAge.AutoSize = True
        Me.lblNewFromAge.Location = New System.Drawing.Point(227, 43)
        Me.lblNewFromAge.Name = "lblNewFromAge"
        Me.lblNewFromAge.Size = New System.Drawing.Size(0, 14)
        Me.lblNewFromAge.TabIndex = 8
        '
        'lblToAge
        '
        Me.lblToAge.AutoSize = True
        Me.lblToAge.Location = New System.Drawing.Point(227, 70)
        Me.lblToAge.Name = "lblToAge"
        Me.lblToAge.Size = New System.Drawing.Size(0, 14)
        Me.lblToAge.TabIndex = 8
        '
        'lblInstruction
        '
        Me.lblInstruction.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstruction.Location = New System.Drawing.Point(11, 92)
        Me.lblInstruction.Name = "lblInstruction"
        Me.lblInstruction.Size = New System.Drawing.Size(381, 62)
        Me.lblInstruction.TabIndex = 5
        Me.lblInstruction.Text = "Enter the values in the 'New to Age' field.  E.g: If the current age group is fro" & _
            "m 2 to 5 years and that need to be split in 2 to 3 and 3 to 5,then put the value" & _
            " 3 in the 'New to Age' field."
        Me.lblInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.lblToAge)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.lblNewFromAge)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.lblOldFromAge)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.lblInstruction)
        Me.pnlMain.Controls.Add(Me.txtOldFrom)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.txtOldTo)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.txtNewFrom)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(398, 163)
        Me.pnlMain.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(392, 1)
        Me.Label4.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(3, 159)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(392, 1)
        Me.Label5.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Location = New System.Drawing.Point(394, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 155)
        Me.Label6.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(3, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 155)
        Me.Label7.TabIndex = 7
        '
        'pnltblStrip
        '
        Me.pnltblStrip.Controls.Add(Me.tblStrip)
        Me.pnltblStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltblStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnltblStrip.Name = "pnltblStrip"
        Me.pnltblStrip.Size = New System.Drawing.Size(398, 54)
        Me.pnltblStrip.TabIndex = 1
        '
        'frmAddVitalNorm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(398, 217)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnltblStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddVitalNorm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Set Vital Norms Age Groups"
        Me.tblStrip.ResumeLayout(False)
        Me.tblStrip.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnltblStrip.ResumeLayout(False)
        Me.pnltblStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtOldFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtOldTo As System.Windows.Forms.TextBox
    Friend WithEvents txtNewFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tblStrip As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Ok_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblOldFromAge As System.Windows.Forms.Label
    Friend WithEvents lblNewFromAge As System.Windows.Forms.Label
    Friend WithEvents lblToAge As System.Windows.Forms.Label
    Friend WithEvents lblInstruction As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnltblStrip As System.Windows.Forms.Panel
End Class
