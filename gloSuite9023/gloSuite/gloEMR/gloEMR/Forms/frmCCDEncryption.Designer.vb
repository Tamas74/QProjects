<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCDEncryption
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
                    If (IsNothing(SaveFileDialog1) = False) Then
                        SaveFileDialog1.Dispose()
                        SaveFileDialog1 = Nothing
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCCDEncryption))
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.chkEncryption = New System.Windows.Forms.CheckBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.lblSaveAs = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtEncryption = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCCDFilePath = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tlbStripMain = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsb_Save = New System.Windows.Forms.ToolStripButton()
        Me.tsb_Close = New System.Windows.Forms.ToolStripButton()
        Me.chkIsExeEncryption = New System.Windows.Forms.CheckBox()
        Me.tlTooltip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tlbStripMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnBrowse
        '
        Me.btnBrowse.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Image = Global.gloEMR.My.Resources.Resources.Browse
        Me.btnBrowse.Location = New System.Drawing.Point(580, 25)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(25, 22)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'chkEncryption
        '
        Me.chkEncryption.AutoSize = True
        Me.chkEncryption.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.chkEncryption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkEncryption.Location = New System.Drawing.Point(10, 65)
        Me.chkEncryption.Name = "chkEncryption"
        Me.chkEncryption.Size = New System.Drawing.Size(178, 18)
        Me.chkEncryption.TabIndex = 2
        Me.chkEncryption.Text = "Encrypt file with password :"
        Me.chkEncryption.UseVisualStyleBackColor = True
        '
        'lblSaveAs
        '
        Me.lblSaveAs.AutoSize = True
        Me.lblSaveAs.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblSaveAs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSaveAs.Location = New System.Drawing.Point(101, 29)
        Me.lblSaveAs.Name = "lblSaveAs"
        Me.lblSaveAs.Size = New System.Drawing.Size(84, 14)
        Me.lblSaveAs.TabIndex = 4
        Me.lblSaveAs.Text = "Save CCD As :"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtEncryption)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.txtCCDFilePath)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.lblSaveAs)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.chkEncryption)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.btnBrowse)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 58)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(616, 118)
        Me.Panel2.TabIndex = 5
        '
        'txtEncryption
        '
        Me.txtEncryption.Location = New System.Drawing.Point(186, 61)
        Me.txtEncryption.Name = "txtEncryption"
        Me.txtEncryption.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtEncryption.Size = New System.Drawing.Size(388, 22)
        Me.txtEncryption.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(296, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(154, 17)
        Me.Label5.TabIndex = 8
        Me.Label5.Visible = False
        '
        'txtCCDFilePath
        '
        Me.txtCCDFilePath.Location = New System.Drawing.Point(187, 26)
        Me.txtCCDFilePath.Name = "txtCCDFilePath"
        Me.txtCCDFilePath.Size = New System.Drawing.Size(387, 22)
        Me.txtCCDFilePath.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(612, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 110)
        Me.Label4.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 110)
        Me.Label3.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(610, 1)
        Me.Label2.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(3, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(610, 1)
        Me.Label7.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tlbStripMain)
        Me.Panel1.Controls.Add(Me.chkIsExeEncryption)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(616, 58)
        Me.Panel1.TabIndex = 6
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
        Me.tlbStripMain.Size = New System.Drawing.Size(616, 53)
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
        Me.chkIsExeEncryption.Location = New System.Drawing.Point(227, 33)
        Me.chkIsExeEncryption.Name = "chkIsExeEncryption"
        Me.chkIsExeEncryption.Size = New System.Drawing.Size(145, 17)
        Me.chkIsExeEncryption.TabIndex = 7
        Me.chkIsExeEncryption.Text = "Encrypt && Create Exe"
        Me.chkIsExeEncryption.UseVisualStyleBackColor = True
        Me.chkIsExeEncryption.Visible = False
        '
        'frmCCDEncryption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(616, 176)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCCDEncryption"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Save CCD"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tlbStripMain.ResumeLayout(False)
        Me.tlbStripMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents chkEncryption As System.Windows.Forms.CheckBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lblSaveAs As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tlbStripMain As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsb_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsb_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkIsExeEncryption As System.Windows.Forms.CheckBox
    Friend WithEvents txtCCDFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEncryption As System.Windows.Forms.TextBox
    Friend WithEvents tlTooltip As System.Windows.Forms.ToolTip
End Class
