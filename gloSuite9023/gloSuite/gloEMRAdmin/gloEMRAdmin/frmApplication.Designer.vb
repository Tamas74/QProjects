<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmApplication
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmApplication))
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.TsSaveBtn = New System.Windows.Forms.ToolStripButton()
        Me.TsCancelBtn = New System.Windows.Forms.ToolStripButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtApplicationCode = New System.Windows.Forms.TextBox()
        Me.txtApplicationName = New System.Windows.Forms.TextBox()
        Me.txtVendorcode = New System.Windows.Forms.TextBox()
        Me.txtVendorName = New System.Windows.Forms.TextBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.tstrip.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TsSaveBtn, Me.TsCancelBtn})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(372, 53)
        Me.tstrip.TabIndex = 3
        Me.tstrip.TabStop = True
        Me.tstrip.Text = "ToolStrip1"
        '
        'TsSaveBtn
        '
        Me.TsSaveBtn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TsSaveBtn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.TsSaveBtn.Image = CType(resources.GetObject("TsSaveBtn.Image"), System.Drawing.Image)
        Me.TsSaveBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsSaveBtn.Name = "TsSaveBtn"
        Me.TsSaveBtn.Size = New System.Drawing.Size(66, 50)
        Me.TsSaveBtn.Text = "&Save&&Cls"
        Me.TsSaveBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TsSaveBtn.ToolTipText = "Save and Close"
        '
        'TsCancelBtn
        '
        Me.TsCancelBtn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TsCancelBtn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.TsCancelBtn.Image = CType(resources.GetObject("TsCancelBtn.Image"), System.Drawing.Image)
        Me.TsCancelBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsCancelBtn.Name = "TsCancelBtn"
        Me.TsCancelBtn.Size = New System.Drawing.Size(43, 50)
        Me.TsCancelBtn.Text = "&Close"
        Me.TsCancelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TsCancelBtn.ToolTipText = "Close"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(35, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 14)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Application Code :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 14)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Application Name :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(54, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 14)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Vendor Code :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(51, 109)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 14)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Vendor Name :"
        '
        'txtApplicationCode
        '
        Me.txtApplicationCode.Location = New System.Drawing.Point(144, 12)
        Me.txtApplicationCode.MaxLength = 100
        Me.txtApplicationCode.Name = "txtApplicationCode"
        Me.txtApplicationCode.Size = New System.Drawing.Size(199, 22)
        Me.txtApplicationCode.TabIndex = 0
        '
        'txtApplicationName
        '
        Me.txtApplicationName.Location = New System.Drawing.Point(144, 43)
        Me.txtApplicationName.MaxLength = 100
        Me.txtApplicationName.Name = "txtApplicationName"
        Me.txtApplicationName.Size = New System.Drawing.Size(199, 22)
        Me.txtApplicationName.TabIndex = 1
        '
        'txtVendorcode
        '
        Me.txtVendorcode.Location = New System.Drawing.Point(144, 74)
        Me.txtVendorcode.MaxLength = 100
        Me.txtVendorcode.Name = "txtVendorcode"
        Me.txtVendorcode.Size = New System.Drawing.Size(199, 22)
        Me.txtVendorcode.TabIndex = 2
        '
        'txtVendorName
        '
        Me.txtVendorName.Location = New System.Drawing.Point(144, 105)
        Me.txtVendorName.MaxLength = 100
        Me.txtVendorName.Name = "txtVendorName"
        Me.txtVendorName.Size = New System.Drawing.Size(199, 22)
        Me.txtVendorName.TabIndex = 3
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Location = New System.Drawing.Point(146, 136)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(60, 18)
        Me.chkActive.TabIndex = 4
        Me.chkActive.Text = "Active"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(25, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 14)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "*"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(21, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(14, 14)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "*"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.chkActive)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtVendorName)
        Me.Panel1.Controls.Add(Me.txtVendorcode)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtApplicationName)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtApplicationCode)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 56)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(372, 168)
        Me.Panel1.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Location = New System.Drawing.Point(368, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 160)
        Me.Label10.TabIndex = 20
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Location = New System.Drawing.Point(3, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 160)
        Me.Label9.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Location = New System.Drawing.Point(3, 164)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(366, 1)
        Me.Label8.TabIndex = 18
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(3, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(366, 1)
        Me.Label7.TabIndex = 17
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.tstrip)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(372, 56)
        Me.Panel2.TabIndex = 1
        '
        'frmApplication
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(372, 224)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmApplication"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Application"
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents TsSaveBtn As System.Windows.Forms.ToolStripButton
    Friend WithEvents TsCancelBtn As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtApplicationCode As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicationName As System.Windows.Forms.TextBox
    Friend WithEvents txtVendorcode As System.Windows.Forms.TextBox
    Friend WithEvents txtVendorName As System.Windows.Forms.TextBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
