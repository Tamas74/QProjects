<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFollowupCalculator
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFollowupCalculator))
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlRDButton = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbtn_Account_FollowUp = New System.Windows.Forms.RadioButton()
        Me.rdbtn_Claim_FollowUp = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.lblMainTop = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Panel21.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlRDButton.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel21
        '
        Me.Panel21.BackColor = System.Drawing.Color.Transparent
        Me.Panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel21.Controls.Add(Me.Panel3)
        Me.Panel21.Controls.Add(Me.pnlRDButton)
        Me.Panel21.Controls.Add(Me.Panel1)
        Me.Panel21.Controls.Add(Me.lblMainTop)
        Me.Panel21.Controls.Add(Me.Label44)
        Me.Panel21.Controls.Add(Me.Label45)
        Me.Panel21.Controls.Add(Me.Label46)
        Me.Panel21.Controls.Add(Me.Label30)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel21.Location = New System.Drawing.Point(0, 0)
        Me.Panel21.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel21.Size = New System.Drawing.Size(423, 242)
        Me.Panel21.TabIndex = 20
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.btnReset)
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(4, 153)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(415, 85)
        Me.Panel3.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(356, 20)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "This may take a while. Continue?"
        '
        'btnReset
        '
        Me.btnReset.BackgroundImage = CType(resources.GetObject("btnReset.BackgroundImage"), System.Drawing.Image)
        Me.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black
        Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReset.Location = New System.Drawing.Point(244, 30)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(78, 23)
        Me.btnReset.TabIndex = 12
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(329, 30)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(78, 23)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(356, 20)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Note: Action taken by this utility cannot reverted back." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'pnlRDButton
        '
        Me.pnlRDButton.BackColor = System.Drawing.Color.Transparent
        Me.pnlRDButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlRDButton.Controls.Add(Me.GroupBox1)
        Me.pnlRDButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRDButton.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlRDButton.Location = New System.Drawing.Point(4, 65)
        Me.pnlRDButton.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlRDButton.Name = "pnlRDButton"
        Me.pnlRDButton.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlRDButton.Size = New System.Drawing.Size(415, 88)
        Me.pnlRDButton.TabIndex = 21
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbtn_Account_FollowUp)
        Me.GroupBox1.Controls.Add(Me.rdbtn_Claim_FollowUp)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(7, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(399, 78)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Recalculate for"
        '
        'rdbtn_Account_FollowUp
        '
        Me.rdbtn_Account_FollowUp.AutoSize = True
        Me.rdbtn_Account_FollowUp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtn_Account_FollowUp.Location = New System.Drawing.Point(19, 21)
        Me.rdbtn_Account_FollowUp.Name = "rdbtn_Account_FollowUp"
        Me.rdbtn_Account_FollowUp.Size = New System.Drawing.Size(172, 18)
        Me.rdbtn_Account_FollowUp.TabIndex = 12
        Me.rdbtn_Account_FollowUp.TabStop = True
        Me.rdbtn_Account_FollowUp.Text = "Account Balance Follow-up"
        Me.rdbtn_Account_FollowUp.UseVisualStyleBackColor = True
        '
        'rdbtn_Claim_FollowUp
        '
        Me.rdbtn_Claim_FollowUp.AutoSize = True
        Me.rdbtn_Claim_FollowUp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtn_Claim_FollowUp.Location = New System.Drawing.Point(19, 45)
        Me.rdbtn_Claim_FollowUp.Name = "rdbtn_Claim_FollowUp"
        Me.rdbtn_Claim_FollowUp.Size = New System.Drawing.Size(165, 18)
        Me.rdbtn_Claim_FollowUp.TabIndex = 11
        Me.rdbtn_Claim_FollowUp.TabStop = True
        Me.rdbtn_Claim_FollowUp.Text = "Insurance Claim Follow-up"
        Me.rdbtn_Claim_FollowUp.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblMessage)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(415, 61)
        Me.Panel1.TabIndex = 22
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Location = New System.Drawing.Point(10, 10)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(396, 39)
        Me.lblMessage.TabIndex = 11
        '
        'lblMainTop
        '
        Me.lblMainTop.BackColor = System.Drawing.Color.Transparent
        Me.lblMainTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMainTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainTop.Location = New System.Drawing.Point(4, 4)
        Me.lblMainTop.Name = "lblMainTop"
        Me.lblMainTop.Padding = New System.Windows.Forms.Padding(3)
        Me.lblMainTop.Size = New System.Drawing.Size(415, 234)
        Me.lblMainTop.TabIndex = 0
        Me.lblMainTop.Text = "   "
        Me.lblMainTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label44.Location = New System.Drawing.Point(4, 238)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(415, 1)
        Me.Label44.TabIndex = 8
        Me.Label44.Text = "label2"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(3, 4)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 235)
        Me.Label45.TabIndex = 7
        Me.Label45.Text = "label4"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label46.Location = New System.Drawing.Point(419, 4)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 235)
        Me.Label46.TabIndex = 6
        Me.Label46.Text = "label3"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label30.Location = New System.Drawing.Point(3, 3)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(417, 1)
        Me.Label30.TabIndex = 9
        Me.Label30.Text = "label2"
        '
        'frmFollowupCalculator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(423, 242)
        Me.Controls.Add(Me.Panel21)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFollowupCalculator"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FollowUp Calculator"
        Me.Panel21.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlRDButton.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents lblMainTop As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlRDButton As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbtn_Account_FollowUp As System.Windows.Forms.RadioButton
    Friend WithEvents rdbtn_Claim_FollowUp As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
