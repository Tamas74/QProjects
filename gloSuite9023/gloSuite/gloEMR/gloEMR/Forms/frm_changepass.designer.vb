<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_changepass
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_changepass))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtChangePassword = New System.Windows.Forms.TextBox
        Me.txtConfirmPass = New System.Windows.Forms.TextBox
        Me.txtOldPass = New System.Windows.Forms.TextBox
        Me.tsbtn_32 = New gloGlobal.gloToolStripIgnoreFocus
        Me.tsbtn_save = New System.Windows.Forms.ToolStripButton
        Me.pnl_ToolStrip = New System.Windows.Forms.Panel
        Me.tls = New gloGlobal.gloToolStripIgnoreFocus
        Me.tsbtnGenPass = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Ok = New System.Windows.Forms.ToolStripButton
        Me.txbtncl = New System.Windows.Forms.ToolStripButton
        Me.pnl_Base = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.tsbtn_32.SuspendLayout()
        Me.pnl_ToolStrip.SuspendLayout()
        Me.tls.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(41, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Old Password :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "New  Password :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Confirm Password :"
        '
        'txtChangePassword
        '
        Me.txtChangePassword.BackColor = System.Drawing.Color.GhostWhite
        Me.txtChangePassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChangePassword.ForeColor = System.Drawing.Color.Black
        Me.txtChangePassword.Location = New System.Drawing.Point(131, 46)
        Me.txtChangePassword.Name = "txtChangePassword"
        Me.txtChangePassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtChangePassword.Size = New System.Drawing.Size(189, 22)
        Me.txtChangePassword.TabIndex = 1
        '
        'txtConfirmPass
        '
        Me.txtConfirmPass.BackColor = System.Drawing.Color.GhostWhite
        Me.txtConfirmPass.Enabled = False
        Me.txtConfirmPass.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmPass.ForeColor = System.Drawing.Color.Black
        Me.txtConfirmPass.Location = New System.Drawing.Point(131, 77)
        Me.txtConfirmPass.Name = "txtConfirmPass"
        Me.txtConfirmPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmPass.Size = New System.Drawing.Size(189, 22)
        Me.txtConfirmPass.TabIndex = 2
        '
        'txtOldPass
        '
        Me.txtOldPass.BackColor = System.Drawing.Color.GhostWhite
        Me.txtOldPass.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldPass.ForeColor = System.Drawing.Color.Black
        Me.txtOldPass.Location = New System.Drawing.Point(131, 16)
        Me.txtOldPass.Name = "txtOldPass"
        Me.txtOldPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtOldPass.Size = New System.Drawing.Size(189, 22)
        Me.txtOldPass.TabIndex = 0
        '
        'tsbtn_32
        '
        Me.tsbtn_32.Dock = System.Windows.Forms.DockStyle.None
        Me.tsbtn_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtn_save})
        Me.tsbtn_32.Location = New System.Drawing.Point(0, 0)
        Me.tsbtn_32.Name = "tsbtn_32"
        Me.tsbtn_32.Size = New System.Drawing.Size(35, 25)
        Me.tsbtn_32.TabIndex = 4
        Me.tsbtn_32.Tag = ""
        Me.tsbtn_32.Text = "Save"
        '
        'tsbtn_save
        '
        Me.tsbtn_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtn_save.Image = CType(resources.GetObject("tsbtn_save.Image"), System.Drawing.Image)
        Me.tsbtn_save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_save.Name = "tsbtn_save"
        Me.tsbtn_save.Size = New System.Drawing.Size(23, 22)
        Me.tsbtn_save.Tag = "save"
        Me.tsbtn_save.Text = "Save"
        Me.tsbtn_save.ToolTipText = "Save"
        '
        'pnl_ToolStrip
        '
        Me.pnl_ToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_ToolStrip.Controls.Add(Me.tls)
        Me.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_ToolStrip.Name = "pnl_ToolStrip"
        Me.pnl_ToolStrip.Size = New System.Drawing.Size(360, 54)
        Me.pnl_ToolStrip.TabIndex = 9
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtnGenPass, Me.btn_tls_Ok, Me.txbtncl})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(0, 0)
        Me.tls.Name = "tls"
        Me.tls.Size = New System.Drawing.Size(360, 53)
        Me.tls.TabIndex = 0
        Me.tls.Text = "toolStrip1"
        '
        'tsbtnGenPass
        '
        Me.tsbtnGenPass.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtnGenPass.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsbtnGenPass.Image = CType(resources.GetObject("tsbtnGenPass.Image"), System.Drawing.Image)
        Me.tsbtnGenPass.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnGenPass.Name = "tsbtnGenPass"
        Me.tsbtnGenPass.Size = New System.Drawing.Size(90, 50)
        Me.tsbtnGenPass.Tag = "Close"
        Me.tsbtnGenPass.Text = "&Generate PW"
        Me.tsbtnGenPass.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtnGenPass.ToolTipText = "Generate Password"
        '
        'btn_tls_Ok
        '
        Me.btn_tls_Ok.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Ok.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_tls_Ok.Image = CType(resources.GetObject("btn_tls_Ok.Image"), System.Drawing.Image)
        Me.btn_tls_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Ok.Name = "btn_tls_Ok"
        Me.btn_tls_Ok.Size = New System.Drawing.Size(66, 50)
        Me.btn_tls_Ok.Tag = "OK"
        Me.btn_tls_Ok.Text = "&Save&&Cls"
        Me.btn_tls_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Ok.ToolTipText = "Save and Close"
        '
        'txbtncl
        '
        Me.txbtncl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txbtncl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txbtncl.Image = Global.gloEMR.My.Resources.Resources.Close01
        Me.txbtncl.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.txbtncl.Name = "txbtncl"
        Me.txbtncl.Size = New System.Drawing.Size(43, 50)
        Me.txbtncl.Tag = "Close"
        Me.txbtncl.Text = "&Close"
        Me.txbtncl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_Base.Controls.Add(Me.txtOldPass)
        Me.pnl_Base.Controls.Add(Me.txtConfirmPass)
        Me.pnl_Base.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_Base.Controls.Add(Me.txtChangePassword)
        Me.pnl_Base.Controls.Add(Me.Label1)
        Me.pnl_Base.Controls.Add(Me.Label2)
        Me.pnl_Base.Controls.Add(Me.Label3)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 54)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Base.Size = New System.Drawing.Size(360, 147)
        Me.pnl_Base.TabIndex = 10
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 143)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(352, 1)
        Me.lbl_BottomBrd.TabIndex = 4
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 140)
        Me.lbl_LeftBrd.TabIndex = 3
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(356, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 140)
        Me.lbl_RightBrd.TabIndex = 2
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(354, 1)
        Me.lbl_TopBrd.TabIndex = 0
        Me.lbl_TopBrd.Text = "label1"
        '
        'frm_changepass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(360, 201)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.pnl_ToolStrip)
        Me.Controls.Add(Me.tsbtn_32)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frm_changepass"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Password"
        Me.tsbtn_32.ResumeLayout(False)
        Me.tsbtn_32.PerformLayout()
        Me.pnl_ToolStrip.ResumeLayout(False)
        Me.pnl_ToolStrip.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.pnl_Base.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnGenPass As System.Windows.Forms.Button
    Friend WithEvents txtChangePassword As System.Windows.Forms.TextBox
    Friend WithEvents txtConfirmPass As System.Windows.Forms.TextBox
    Friend WithEvents txtOldPass As System.Windows.Forms.TextBox
    Friend WithEvents tsbtn_32 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsbtn_save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnGenPass As System.Windows.Forms.ToolStripButton


    Private WithEvents pnl_ToolStrip As System.Windows.Forms.Panel
    Private WithEvents tls As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Ok As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents txbtncl As System.Windows.Forms.ToolStripButton
End Class
