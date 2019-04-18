<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddGCUser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddGCUser))
        Me.txtAnswer = New System.Windows.Forms.TextBox()
        Me.label6 = New System.Windows.Forms.Label()
        Me.txtQuestion = New System.Windows.Forms.TextBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.txtConfirmPwd = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.txtUserNm = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.btnCreateUser = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtAnswer
        '
        Me.txtAnswer.Location = New System.Drawing.Point(176, 218)
        Me.txtAnswer.Name = "txtAnswer"
        Me.txtAnswer.Size = New System.Drawing.Size(272, 22)
        Me.txtAnswer.TabIndex = 6
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(66, 222)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(108, 14)
        Me.label6.TabIndex = 24
        Me.label6.Text = "Security Answer : "
        '
        'txtQuestion
        '
        Me.txtQuestion.Location = New System.Drawing.Point(176, 180)
        Me.txtQuestion.Name = "txtQuestion"
        Me.txtQuestion.Size = New System.Drawing.Size(272, 22)
        Me.txtQuestion.TabIndex = 5
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(58, 184)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(116, 14)
        Me.label5.TabIndex = 22
        Me.label5.Text = "Security Question : "
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(176, 142)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(272, 22)
        Me.txtEmail.TabIndex = 4
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(116, 146)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(58, 14)
        Me.label4.TabIndex = 20
        Me.label4.Text = "E - mail : "
        '
        'txtConfirmPwd
        '
        Me.txtConfirmPwd.Location = New System.Drawing.Point(176, 104)
        Me.txtConfirmPwd.Name = "txtConfirmPwd"
        Me.txtConfirmPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmPwd.Size = New System.Drawing.Size(272, 22)
        Me.txtConfirmPwd.TabIndex = 3
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(59, 108)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(115, 14)
        Me.label3.TabIndex = 18
        Me.label3.Text = "Confirm Password : "
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(176, 66)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(272, 22)
        Me.txtPassword.TabIndex = 2
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(104, 70)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(70, 14)
        Me.label2.TabIndex = 16
        Me.label2.Text = "Password : "
        '
        'txtUserNm
        '
        Me.txtUserNm.Location = New System.Drawing.Point(176, 28)
        Me.txtUserNm.MaxLength = 30
        Me.txtUserNm.Name = "txtUserNm"
        Me.txtUserNm.Size = New System.Drawing.Size(272, 22)
        Me.txtUserNm.TabIndex = 1
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(96, 32)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(78, 14)
        Me.label1.TabIndex = 14
        Me.label1.Text = "User Name : "
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.btnCreateUser)
        Me.pnlMain.Controls.Add(Me.TextBox1)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.Label9)
        Me.pnlMain.Controls.Add(Me.Label10)
        Me.pnlMain.Controls.Add(Me.txtAnswer)
        Me.pnlMain.Controls.Add(Me.Label11)
        Me.pnlMain.Controls.Add(Me.label6)
        Me.pnlMain.Controls.Add(Me.label1)
        Me.pnlMain.Controls.Add(Me.txtQuestion)
        Me.pnlMain.Controls.Add(Me.txtUserNm)
        Me.pnlMain.Controls.Add(Me.label5)
        Me.pnlMain.Controls.Add(Me.label2)
        Me.pnlMain.Controls.Add(Me.txtEmail)
        Me.pnlMain.Controls.Add(Me.txtPassword)
        Me.pnlMain.Controls.Add(Me.label4)
        Me.pnlMain.Controls.Add(Me.label3)
        Me.pnlMain.Controls.Add(Me.txtConfirmPwd)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.pnlMain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(543, 337)
        Me.pnlMain.TabIndex = 0
        '
        'btnCreateUser
        '
        Me.btnCreateUser.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnCreateUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCreateUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateUser.Location = New System.Drawing.Point(249, 256)
        Me.btnCreateUser.Name = "btnCreateUser"
        Me.btnCreateUser.Size = New System.Drawing.Size(101, 28)
        Me.btnCreateUser.TabIndex = 29
        Me.btnCreateUser.Text = "Register User"
        Me.btnCreateUser.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.Red
        Me.TextBox1.Location = New System.Drawing.Point(12, 300)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(514, 25)
        Me.TextBox1.TabIndex = 28
        Me.TextBox1.Text = "Password Specification : Length minimum - 7 Non-alphanumeric characters required-" & _
            "1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 333)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(535, 1)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(535, 1)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(539, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 331)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 331)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "label4"
        '
        'frmAddGCUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(543, 337)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddGCUser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sign up for gloCommunity"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents txtAnswer As System.Windows.Forms.TextBox
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents txtQuestion As System.Windows.Forms.TextBox
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents txtEmail As System.Windows.Forms.TextBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents txtConfirmPwd As System.Windows.Forms.TextBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents txtPassword As System.Windows.Forms.TextBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents txtUserNm As System.Windows.Forms.TextBox
    Private WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btnCreateUser As System.Windows.Forms.Button
End Class
