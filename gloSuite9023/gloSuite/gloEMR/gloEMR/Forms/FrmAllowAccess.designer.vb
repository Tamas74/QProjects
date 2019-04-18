<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAllowAccess
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAllowAccess))
        Me.pnlLogin = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.pnl_tls_ = New System.Windows.Forms.Panel
        Me.tlsDropdown = New gloGlobal.gloToolStripIgnoreFocus
        Me.btnOK = New System.Windows.Forms.ToolStripButton
        Me.BtnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlLogin.SuspendLayout()
        Me.pnl_tls_.SuspendLayout()
        Me.tlsDropdown.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLogin
        '
        Me.pnlLogin.BackColor = System.Drawing.Color.Transparent
        Me.pnlLogin.Controls.Add(Me.Label1)
        Me.pnlLogin.Controls.Add(Me.Label2)
        Me.pnlLogin.Controls.Add(Me.Label13)
        Me.pnlLogin.Controls.Add(Me.Label4)
        Me.pnlLogin.Controls.Add(Me.Label3)
        Me.pnlLogin.Controls.Add(Me.txtPassword)
        Me.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLogin.Location = New System.Drawing.Point(0, 56)
        Me.pnlLogin.Name = "pnlLogin"
        Me.pnlLogin.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlLogin.Size = New System.Drawing.Size(330, 81)
        Me.pnlLogin.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(4, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(322, 1)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Location = New System.Drawing.Point(326, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 74)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(4, 77)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(323, 1)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "label1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 75)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "label4"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 14)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Password : "
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(103, 26)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(177, 22)
        Me.txtPassword.TabIndex = 2
        '
        'pnl_tls_
        '
        Me.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_.Controls.Add(Me.tlsDropdown)
        Me.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_.Name = "pnl_tls_"
        Me.pnl_tls_.Size = New System.Drawing.Size(330, 56)
        Me.pnl_tls_.TabIndex = 13
        '
        'tlsDropdown
        '
        Me.tlsDropdown.BackColor = System.Drawing.Color.Transparent
        Me.tlsDropdown.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDropdown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDropdown.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDropdown.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK, Me.BtnClose})
        Me.tlsDropdown.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDropdown.Location = New System.Drawing.Point(0, 0)
        Me.tlsDropdown.Name = "tlsDropdown"
        Me.tlsDropdown.Size = New System.Drawing.Size(330, 53)
        Me.tlsDropdown.TabIndex = 0
        Me.tlsDropdown.Text = "toolStrip1"
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(36, 50)
        Me.btnOK.Tag = "Ok"
        Me.btnOK.Text = "&Ok"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOK.ToolTipText = "OK"
        '
        'BtnClose
        '
        Me.BtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnClose.Image = CType(resources.GetObject("BtnClose.Image"), System.Drawing.Image)
        Me.BtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(43, 50)
        Me.BtnClose.Tag = "Cancel"
        Me.BtnClose.Text = "&Close"
        Me.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnClose.ToolTipText = "Close"
        '
        'FrmAllowAccess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(330, 137)
        Me.Controls.Add(Me.pnlLogin)
        Me.Controls.Add(Me.pnl_tls_)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAllowAccess"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Allow Emergency Access of Patient Chart"
        Me.pnlLogin.ResumeLayout(False)
        Me.pnlLogin.PerformLayout()
        Me.pnl_tls_.ResumeLayout(False)
        Me.pnl_tls_.PerformLayout()
        Me.tlsDropdown.ResumeLayout(False)
        Me.tlsDropdown.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlLogin As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Private WithEvents pnl_tls_ As System.Windows.Forms.Panel
    Private WithEvents tlsDropdown As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Private WithEvents BtnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
End Class
