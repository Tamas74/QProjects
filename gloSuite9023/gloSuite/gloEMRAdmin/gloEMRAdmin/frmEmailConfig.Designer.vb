Namespace gloCommunity
	Partial Class frmEmailConfig
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmailConfig))
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.groupBox2 = New System.Windows.Forms.GroupBox()
            Me.txtEmail = New System.Windows.Forms.TextBox()
            Me.btnConfig = New System.Windows.Forms.Button()
            Me.label6 = New System.Windows.Forms.Label()
            Me.panel3.SuspendLayout()
            Me.groupBox2.SuspendLayout()
            Me.SuspendLayout()
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.groupBox2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel3.Location = New System.Drawing.Point(0, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Padding = New System.Windows.Forms.Padding(5, 0, 5, 5)
            Me.panel3.Size = New System.Drawing.Size(579, 150)
            Me.panel3.TabIndex = 3
            '
            'groupBox2
            '
            Me.groupBox2.BackColor = System.Drawing.Color.Transparent
            Me.groupBox2.Controls.Add(Me.txtEmail)
            Me.groupBox2.Controls.Add(Me.btnConfig)
            Me.groupBox2.Controls.Add(Me.label6)
            Me.groupBox2.Dock = System.Windows.Forms.DockStyle.Top
            Me.groupBox2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.groupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
            Me.groupBox2.Location = New System.Drawing.Point(5, 0)
            Me.groupBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.groupBox2.Name = "groupBox2"
            Me.groupBox2.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.groupBox2.Size = New System.Drawing.Size(569, 104)
            Me.groupBox2.TabIndex = 0
            Me.groupBox2.TabStop = False
            Me.groupBox2.Text = "Active directory E-mail configuration"
            '
            'txtEmail
            '
            Me.txtEmail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtEmail.Location = New System.Drawing.Point(112, 23)
            Me.txtEmail.Multiline = True
            Me.txtEmail.Name = "txtEmail"
            Me.txtEmail.Size = New System.Drawing.Size(441, 24)
            Me.txtEmail.TabIndex = 1
            '
            'btnConfig
            '
            Me.btnConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.btnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnConfig.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnConfig.Location = New System.Drawing.Point(241, 66)
            Me.btnConfig.Name = "btnConfig"
            Me.btnConfig.Size = New System.Drawing.Size(74, 25)
            Me.btnConfig.TabIndex = 17
            Me.btnConfig.Text = "Configure"
            Me.btnConfig.UseVisualStyleBackColor = True
            '
            'label6
            '
            Me.label6.AutoSize = True
            Me.label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.label6.Location = New System.Drawing.Point(60, 26)
            Me.label6.Name = "label6"
            Me.label6.Size = New System.Drawing.Size(46, 14)
            Me.label6.TabIndex = 14
            Me.label6.Text = "E-mail :"
            '
            'frmEmailConfig
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(579, 108)
            Me.Controls.Add(Me.panel3)
            Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
            Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmEmailConfig"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Add E-mail"
            Me.panel3.ResumeLayout(False)
            Me.groupBox2.ResumeLayout(False)
            Me.groupBox2.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

		#End Region

		Private panel3 As System.Windows.Forms.Panel
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private txtEmail As System.Windows.Forms.TextBox
        'Private btnConfig As System.Windows.Forms.Button
        Private label6 As System.Windows.Forms.Label
        Friend WithEvents btnConfig As System.Windows.Forms.Button
	End Class
End Namespace
