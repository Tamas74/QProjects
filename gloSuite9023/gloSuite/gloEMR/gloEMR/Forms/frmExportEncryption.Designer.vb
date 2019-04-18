<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportEncryption
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExportEncryption))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.tlbStripMain = New gloGlobal.gloToolStripIgnoreFocus
        Me.tsb_Save = New System.Windows.Forms.ToolStripButton
        Me.tsb_Close = New System.Windows.Forms.ToolStripButton
        Me.chkIsExeEncryption = New System.Windows.Forms.CheckBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtEncryptKey = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.tlbStripMain.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tlbStripMain)
        Me.Panel1.Controls.Add(Me.chkIsExeEncryption)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(395, 54)
        Me.Panel1.TabIndex = 1
        '
        'tlbStripMain
        '
        Me.tlbStripMain.BackColor = System.Drawing.Color.Transparent
        Me.tlbStripMain.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlbStripMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbStripMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlbStripMain.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlbStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsb_Save, Me.tsb_Close})
        Me.tlbStripMain.Location = New System.Drawing.Point(0, 0)
        Me.tlbStripMain.Name = "tlbStripMain"
        Me.tlbStripMain.Size = New System.Drawing.Size(395, 53)
        Me.tlbStripMain.TabIndex = 0
        Me.tlbStripMain.TabStop = True
        '
        'tsb_Save
        '
        Me.tsb_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Save.Image = CType(resources.GetObject("tsb_Save.Image"), System.Drawing.Image)
        Me.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Save.Name = "tsb_Save"
        Me.tsb_Save.Size = New System.Drawing.Size(66, 50)
        Me.tsb_Save.Text = "&Save&&Cls"
        Me.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_Save.ToolTipText = "Save and Close"
        '
        'tsb_Close
        '
        Me.tsb_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Close.Image = CType(resources.GetObject("tsb_Close.Image"), System.Drawing.Image)
        Me.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Close.Name = "tsb_Close"
        Me.tsb_Close.Size = New System.Drawing.Size(43, 50)
        Me.tsb_Close.Text = "&Close"
        Me.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'chkIsExeEncryption
        '
        Me.chkIsExeEncryption.AutoSize = True
        Me.chkIsExeEncryption.Checked = True
        Me.chkIsExeEncryption.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsExeEncryption.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsExeEncryption.Location = New System.Drawing.Point(195, 31)
        Me.chkIsExeEncryption.Name = "chkIsExeEncryption"
        Me.chkIsExeEncryption.Size = New System.Drawing.Size(145, 17)
        Me.chkIsExeEncryption.TabIndex = 7
        Me.chkIsExeEncryption.Text = "Encrypt && Create Exe"
        Me.chkIsExeEncryption.UseVisualStyleBackColor = True
        Me.chkIsExeEncryption.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.txtEncryptKey)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(395, 91)
        Me.Panel2.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(3)
        Me.Label6.Size = New System.Drawing.Size(174, 20)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Enter the Encryption key :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 14)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Key :"
        '
        'txtEncryptKey
        '
        Me.txtEncryptKey.Location = New System.Drawing.Point(61, 43)
        Me.txtEncryptKey.Name = "txtEncryptKey"
        Me.txtEncryptKey.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtEncryptKey.Size = New System.Drawing.Size(307, 22)
        Me.txtEncryptKey.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(391, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 83)
        Me.Label4.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 83)
        Me.Label3.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(389, 1)
        Me.Label2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(389, 1)
        Me.Label1.TabIndex = 0
        '
        'frmExportEncryption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(395, 145)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExportEncryption"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Export Encryption"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tlbStripMain.ResumeLayout(False)
        Me.tlbStripMain.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tlbStripMain As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsb_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEncryptKey As System.Windows.Forms.TextBox
    Friend WithEvents tsb_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkIsExeEncryption As System.Windows.Forms.CheckBox
End Class
