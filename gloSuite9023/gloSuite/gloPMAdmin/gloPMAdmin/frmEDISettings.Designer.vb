<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEDISettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEDISettings))
        Me.pnlButtons = New System.Windows.Forms.Panel
        Me.tstrip = New System.Windows.Forms.ToolStrip
        Me.tlsbtnSave = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.tb_Settings = New System.Windows.Forms.TabControl
        Me.tbpg_ValidationSettings = New System.Windows.Forms.TabPage
        Me.label16 = New System.Windows.Forms.Label
        Me.label17 = New System.Windows.Forms.Label
        Me.label18 = New System.Windows.Forms.Label
        Me.label19 = New System.Windows.Forms.Label
        Me.trvFields = New System.Windows.Forms.TreeView
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.chkSelectAll = New System.Windows.Forms.CheckBox
        Me.lbl_pnlSearchSpaceMiddle = New System.Windows.Forms.Label
        Me.lbl_pnlSearchSpaceBottom = New System.Windows.Forms.Label
        Me.lbl_pnlSearchSpaceTop = New System.Windows.Forms.Label
        Me.lbl_pnlSearchSpace = New System.Windows.Forms.Label
        Me.pnlButtons.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.tb_Settings.SuspendLayout()
        Me.tbpg_ValidationSettings.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlButtons.Controls.Add(Me.tstrip)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlButtons.Location = New System.Drawing.Point(0, 0)
        Me.pnlButtons.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(685, 59)
        Me.pnlButtons.TabIndex = 26
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnSave, Me.tlsbtnClose})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(685, 53)
        Me.tstrip.TabIndex = 2
        Me.tstrip.Text = "ToolStrip1"
        '
        'tlsbtnSave
        '
        Me.tlsbtnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnSave.Image = CType(resources.GetObject("tlsbtnSave.Image"), System.Drawing.Image)
        Me.tlsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnSave.Name = "tlsbtnSave"
        Me.tlsbtnSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsbtnSave.Tag = "Save"
        Me.tlsbtnSave.Text = "&Save&&Cls"
        Me.tlsbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnSave.ToolTipText = "Save and Close"
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Tag = "close"
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "close"
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.tb_Settings)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 59)
        Me.pnlMain.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(685, 464)
        Me.pnlMain.TabIndex = 27
        '
        'tb_Settings
        '
        Me.tb_Settings.Controls.Add(Me.tbpg_ValidationSettings)
        Me.tb_Settings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_Settings.Location = New System.Drawing.Point(0, 0)
        Me.tb_Settings.Margin = New System.Windows.Forms.Padding(2)
        Me.tb_Settings.Name = "tb_Settings"
        Me.tb_Settings.SelectedIndex = 0
        Me.tb_Settings.Size = New System.Drawing.Size(685, 464)
        Me.tb_Settings.TabIndex = 0
        '
        'tbpg_ValidationSettings
        '
        Me.tbpg_ValidationSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpg_ValidationSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tbpg_ValidationSettings.Controls.Add(Me.pnlSearch)
        Me.tbpg_ValidationSettings.Controls.Add(Me.label16)
        Me.tbpg_ValidationSettings.Controls.Add(Me.label17)
        Me.tbpg_ValidationSettings.Controls.Add(Me.label18)
        Me.tbpg_ValidationSettings.Controls.Add(Me.label19)
        Me.tbpg_ValidationSettings.Controls.Add(Me.trvFields)
        Me.tbpg_ValidationSettings.Location = New System.Drawing.Point(4, 29)
        Me.tbpg_ValidationSettings.Margin = New System.Windows.Forms.Padding(2)
        Me.tbpg_ValidationSettings.Name = "tbpg_ValidationSettings"
        Me.tbpg_ValidationSettings.Padding = New System.Windows.Forms.Padding(2)
        Me.tbpg_ValidationSettings.Size = New System.Drawing.Size(677, 431)
        Me.tbpg_ValidationSettings.TabIndex = 1
        Me.tbpg_ValidationSettings.Text = "Validation Fields For EDI Claims"
        '
        'label16
        '
        Me.label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label16.Location = New System.Drawing.Point(3, 428)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(671, 1)
        Me.label16.TabIndex = 12
        Me.label16.Text = "label2"
        '
        'label17
        '
        Me.label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label17.Location = New System.Drawing.Point(2, 3)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(1, 426)
        Me.label17.TabIndex = 11
        Me.label17.Text = "label4"
        '
        'label18
        '
        Me.label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label18.Location = New System.Drawing.Point(674, 3)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(1, 426)
        Me.label18.TabIndex = 10
        Me.label18.Text = "label3"
        '
        'label19
        '
        Me.label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label19.Location = New System.Drawing.Point(2, 2)
        Me.label19.Name = "label19"
        Me.label19.Size = New System.Drawing.Size(673, 1)
        Me.label19.TabIndex = 9
        Me.label19.Text = "label1"
        '
        'trvFields
        '
        Me.trvFields.BackColor = System.Drawing.SystemColors.Window
        Me.trvFields.CheckBoxes = True
        Me.trvFields.FullRowSelect = True
        Me.trvFields.HideSelection = False
        Me.trvFields.Location = New System.Drawing.Point(2, 36)
        Me.trvFields.Name = "trvFields"
        Me.trvFields.ShowNodeToolTips = True
        Me.trvFields.Size = New System.Drawing.Size(673, 399)
        Me.trvFields.TabIndex = 13
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearch.Controls.Add(Me.chkSelectAll)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchSpaceMiddle)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchSpace)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(3, 3)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(671, 35)
        Me.pnlSearch.TabIndex = 14
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Location = New System.Drawing.Point(10, 9)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(80, 18)
        Me.chkSelectAll.TabIndex = 2
        Me.chkSelectAll.Text = "Select All "
        Me.chkSelectAll.UseVisualStyleBackColor = True
        '
        'lbl_pnlSearchSpaceMiddle
        '
        Me.lbl_pnlSearchSpaceMiddle.BackColor = System.Drawing.Color.Transparent
        Me.lbl_pnlSearchSpaceMiddle.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchSpaceMiddle.Location = New System.Drawing.Point(8, 1)
        Me.lbl_pnlSearchSpaceMiddle.Name = "lbl_pnlSearchSpaceMiddle"
        Me.lbl_pnlSearchSpaceMiddle.Size = New System.Drawing.Size(19, 30)
        Me.lbl_pnlSearchSpaceMiddle.TabIndex = 0
        '
        'lbl_pnlSearchSpaceBottom
        '
        Me.lbl_pnlSearchSpaceBottom.BackColor = System.Drawing.Color.Transparent
        Me.lbl_pnlSearchSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchSpaceBottom.Location = New System.Drawing.Point(8, 31)
        Me.lbl_pnlSearchSpaceBottom.Name = "lbl_pnlSearchSpaceBottom"
        Me.lbl_pnlSearchSpaceBottom.Size = New System.Drawing.Size(660, 1)
        Me.lbl_pnlSearchSpaceBottom.TabIndex = 9
        '
        'lbl_pnlSearchSpaceTop
        '
        Me.lbl_pnlSearchSpaceTop.BackColor = System.Drawing.Color.Transparent
        Me.lbl_pnlSearchSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchSpaceTop.Location = New System.Drawing.Point(8, 0)
        Me.lbl_pnlSearchSpaceTop.Name = "lbl_pnlSearchSpaceTop"
        Me.lbl_pnlSearchSpaceTop.Size = New System.Drawing.Size(660, 1)
        Me.lbl_pnlSearchSpaceTop.TabIndex = 8
        '
        'lbl_pnlSearchSpace
        '
        Me.lbl_pnlSearchSpace.BackColor = System.Drawing.Color.Transparent
        Me.lbl_pnlSearchSpace.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchSpace.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlSearchSpace.Name = "lbl_pnlSearchSpace"
        Me.lbl_pnlSearchSpace.Size = New System.Drawing.Size(5, 32)
        Me.lbl_pnlSearchSpace.TabIndex = 2
        '
        'frmEDISettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(685, 523)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlButtons)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEDISettings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Claim Validation Settings"
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlButtons.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.tb_Settings.ResumeLayout(False)
        Me.tbpg_ValidationSettings.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents tlsbtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents tb_Settings As System.Windows.Forms.TabControl
    Private WithEvents tbpg_ValidationSettings As System.Windows.Forms.TabPage
    Private WithEvents label16 As System.Windows.Forms.Label
    Private WithEvents label17 As System.Windows.Forms.Label
    Private WithEvents label18 As System.Windows.Forms.Label
    Private WithEvents label19 As System.Windows.Forms.Label
    Private WithEvents trvFields As System.Windows.Forms.TreeView
    Private WithEvents pnlSearch As System.Windows.Forms.Panel
    Private WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Private WithEvents lbl_pnlSearchSpaceMiddle As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchSpaceBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchSpaceTop As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchSpace As System.Windows.Forms.Label
End Class
