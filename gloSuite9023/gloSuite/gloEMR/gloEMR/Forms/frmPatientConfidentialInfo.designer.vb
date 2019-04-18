<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientConfidentialInfo
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
                Try
                    If (IsNothing(ToList) = False) Then
                        ToList.Dispose()
                        ToList = Nothing
                    End If
                Catch ex As Exception

                End Try

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientConfidentialInfo))
        Me.PnlMain = New System.Windows.Forms.Panel
        Me.pnlInformationCtl = New System.Windows.Forms.Panel
        Me.chk_Active = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtInformation = New System.Windows.Forms.TextBox
        Me.pnlUserCombo = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnClearUser = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSelectInfo = New System.Windows.Forms.Button
        Me.cmbUser = New System.Windows.Forms.ComboBox
        Me.PnlToolStrip = New System.Windows.Forms.Panel
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsSave = New System.Windows.Forms.ToolStripButton
        Me.tlsClose = New System.Windows.Forms.ToolStripButton
        Me.PnlMain.SuspendLayout()
        Me.pnlInformationCtl.SuspendLayout()
        Me.pnlUserCombo.SuspendLayout()
        Me.PnlToolStrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlMain
        '
        Me.PnlMain.BackColor = System.Drawing.Color.Transparent
        Me.PnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PnlMain.Controls.Add(Me.pnlInformationCtl)
        Me.PnlMain.Controls.Add(Me.pnlUserCombo)
        Me.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMain.Location = New System.Drawing.Point(0, 53)
        Me.PnlMain.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(569, 321)
        Me.PnlMain.TabIndex = 0
        '
        'pnlInformationCtl
        '
        Me.pnlInformationCtl.Controls.Add(Me.chk_Active)
        Me.pnlInformationCtl.Controls.Add(Me.Label5)
        Me.pnlInformationCtl.Controls.Add(Me.Label6)
        Me.pnlInformationCtl.Controls.Add(Me.Label7)
        Me.pnlInformationCtl.Controls.Add(Me.Label8)
        Me.pnlInformationCtl.Controls.Add(Me.Label2)
        Me.pnlInformationCtl.Controls.Add(Me.txtInformation)
        Me.pnlInformationCtl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlInformationCtl.Location = New System.Drawing.Point(0, 40)
        Me.pnlInformationCtl.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlInformationCtl.Name = "pnlInformationCtl"
        Me.pnlInformationCtl.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlInformationCtl.Size = New System.Drawing.Size(569, 281)
        Me.pnlInformationCtl.TabIndex = 1
        '
        'chk_Active
        '
        Me.chk_Active.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_Active.Location = New System.Drawing.Point(50, 247)
        Me.chk_Active.Name = "chk_Active"
        Me.chk_Active.Size = New System.Drawing.Size(69, 17)
        Me.chk_Active.TabIndex = 13
        Me.chk_Active.Text = "Active :"
        Me.chk_Active.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Active.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 277)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(561, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 277)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(565, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 277)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(563, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Information :"
        '
        'txtInformation
        '
        Me.txtInformation.ForeColor = System.Drawing.Color.Black
        Me.txtInformation.Location = New System.Drawing.Point(104, 13)
        Me.txtInformation.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtInformation.MaxLength = 255
        Me.txtInformation.Multiline = True
        Me.txtInformation.Name = "txtInformation"
        Me.txtInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInformation.Size = New System.Drawing.Size(448, 225)
        Me.txtInformation.TabIndex = 0
        '
        'pnlUserCombo
        '
        Me.pnlUserCombo.Controls.Add(Me.Label3)
        Me.pnlUserCombo.Controls.Add(Me.Label4)
        Me.pnlUserCombo.Controls.Add(Me.Label9)
        Me.pnlUserCombo.Controls.Add(Me.Label10)
        Me.pnlUserCombo.Controls.Add(Me.btnClearUser)
        Me.pnlUserCombo.Controls.Add(Me.Label1)
        Me.pnlUserCombo.Controls.Add(Me.btnSelectInfo)
        Me.pnlUserCombo.Controls.Add(Me.cmbUser)
        Me.pnlUserCombo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUserCombo.Location = New System.Drawing.Point(0, 0)
        Me.pnlUserCombo.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlUserCombo.Name = "pnlUserCombo"
        Me.pnlUserCombo.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlUserCombo.Size = New System.Drawing.Size(569, 40)
        Me.pnlUserCombo.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(4, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(561, 1)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 33)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(565, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 33)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(563, 1)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "label1"
        '
        'btnClearUser
        '
        Me.btnClearUser.BackgroundImage = CType(resources.GetObject("btnClearUser.BackgroundImage"), System.Drawing.Image)
        Me.btnClearUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClearUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClearUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearUser.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearUser.Image = CType(resources.GetObject("btnClearUser.Image"), System.Drawing.Image)
        Me.btnClearUser.Location = New System.Drawing.Point(530, 9)
        Me.btnClearUser.Name = "btnClearUser"
        Me.btnClearUser.Size = New System.Drawing.Size(22, 22)
        Me.btnClearUser.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(54, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Users :"
        '
        'btnSelectInfo
        '
        Me.btnSelectInfo.BackgroundImage = CType(resources.GetObject("btnSelectInfo.BackgroundImage"), System.Drawing.Image)
        Me.btnSelectInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSelectInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectInfo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectInfo.Image = CType(resources.GetObject("btnSelectInfo.Image"), System.Drawing.Image)
        Me.btnSelectInfo.Location = New System.Drawing.Point(502, 9)
        Me.btnSelectInfo.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSelectInfo.Name = "btnSelectInfo"
        Me.btnSelectInfo.Size = New System.Drawing.Size(22, 22)
        Me.btnSelectInfo.TabIndex = 1
        Me.btnSelectInfo.UseVisualStyleBackColor = True
        '
        'cmbUser
        '
        Me.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(104, 9)
        Me.cmbUser.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(391, 22)
        Me.cmbUser.TabIndex = 0
        '
        'PnlToolStrip
        '
        Me.PnlToolStrip.Controls.Add(Me.ToolStrip1)
        Me.PnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.PnlToolStrip.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PnlToolStrip.Name = "PnlToolStrip"
        Me.PnlToolStrip.Size = New System.Drawing.Size(569, 53)
        Me.PnlToolStrip.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsSave, Me.tlsClose})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(569, 53)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tlsSave
        '
        Me.tlsSave.BackColor = System.Drawing.Color.Transparent
        Me.tlsSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsSave.Image = CType(resources.GetObject("tlsSave.Image"), System.Drawing.Image)
        Me.tlsSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsSave.Name = "tlsSave"
        Me.tlsSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsSave.Tag = "Save"
        Me.tlsSave.Text = "&Save&&Cls"
        Me.tlsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsSave.ToolTipText = "Save and Close"
        '
        'tlsClose
        '
        Me.tlsClose.BackColor = System.Drawing.Color.Transparent
        Me.tlsClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsClose.Image = CType(resources.GetObject("tlsClose.Image"), System.Drawing.Image)
        Me.tlsClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsClose.Name = "tlsClose"
        Me.tlsClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsClose.Tag = "Close"
        Me.tlsClose.Text = "Close"
        Me.tlsClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmPatientConfidentialInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(569, 374)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.PnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientConfidentialInfo"
        Me.Text = "Patient Confidential Information"
        Me.PnlMain.ResumeLayout(False)
        Me.pnlInformationCtl.ResumeLayout(False)
        Me.pnlInformationCtl.PerformLayout()
        Me.pnlUserCombo.ResumeLayout(False)
        Me.pnlUserCombo.PerformLayout()
        Me.PnlToolStrip.ResumeLayout(False)
        Me.PnlToolStrip.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents PnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlUserCombo As System.Windows.Forms.Panel
    Friend WithEvents pnlInformationCtl As System.Windows.Forms.Panel
    Friend WithEvents btnSelectInfo As System.Windows.Forms.Button
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents txtInformation As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnClearUser As System.Windows.Forms.Button
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chk_Active As System.Windows.Forms.CheckBox
End Class
