<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmToolCustomize
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmToolCustomize))
        Me.pnlTOP = New System.Windows.Forms.Panel
        Me.btnDown = New System.Windows.Forms.Button
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnUp = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.trvSelectedButtons = New System.Windows.Forms.TreeView
        Me.trvButtons = New System.Windows.Forms.TreeView
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.tls_ToolButton = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnSelectAll = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClearAll = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel
        Me.img32 = New System.Windows.Forms.ImageList(Me.components)
        Me.img16 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlTOP.SuspendLayout()
        Me.tls_ToolButton.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTOP
        '
        Me.pnlTOP.BackColor = System.Drawing.Color.Transparent
        Me.pnlTOP.Controls.Add(Me.btnDown)
        Me.pnlTOP.Controls.Add(Me.btnRemove)
        Me.pnlTOP.Controls.Add(Me.btnUp)
        Me.pnlTOP.Controls.Add(Me.btnAdd)
        Me.pnlTOP.Controls.Add(Me.trvSelectedButtons)
        Me.pnlTOP.Controls.Add(Me.trvButtons)
        Me.pnlTOP.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlTOP.Controls.Add(Me.lbl_pnlRight)
        Me.pnlTOP.Controls.Add(Me.lbl_pnlTop)
        Me.pnlTOP.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlTOP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTOP.Location = New System.Drawing.Point(0, 54)
        Me.pnlTOP.Name = "pnlTOP"
        Me.pnlTOP.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlTOP.Size = New System.Drawing.Size(588, 331)
        Me.pnlTOP.TabIndex = 2
        '
        'btnDown
        '
        Me.btnDown.BackgroundImage = CType(resources.GetObject("btnDown.BackgroundImage"), System.Drawing.Image)
        Me.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(549, 167)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(25, 25)
        Me.btnDown.TabIndex = 13
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.BackgroundImage = CType(resources.GetObject("btnRemove.BackgroundImage"), System.Drawing.Image)
        Me.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnRemove.FlatAppearance.BorderSize = 0
        Me.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.Location = New System.Drawing.Point(255, 167)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(25, 25)
        Me.btnRemove.TabIndex = 13
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.BackgroundImage = CType(resources.GetObject("btnUp.BackgroundImage"), System.Drawing.Image)
        Me.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Location = New System.Drawing.Point(549, 124)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(25, 25)
        Me.btnUp.TabIndex = 13
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.BackgroundImage = CType(resources.GetObject("btnAdd.BackgroundImage"), System.Drawing.Image)
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Location = New System.Drawing.Point(255, 124)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(25, 25)
        Me.btnAdd.TabIndex = 13
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'trvSelectedButtons
        '
        Me.trvSelectedButtons.FullRowSelect = True
        Me.trvSelectedButtons.HideSelection = False
        Me.trvSelectedButtons.Location = New System.Drawing.Point(291, 5)
        Me.trvSelectedButtons.Name = "trvSelectedButtons"
        Me.trvSelectedButtons.ShowLines = False
        Me.trvSelectedButtons.ShowPlusMinus = False
        Me.trvSelectedButtons.ShowRootLines = False
        Me.trvSelectedButtons.Size = New System.Drawing.Size(246, 321)
        Me.trvSelectedButtons.TabIndex = 12
        '
        'trvButtons
        '
        Me.trvButtons.FullRowSelect = True
        Me.trvButtons.HideSelection = False
        Me.trvButtons.Location = New System.Drawing.Point(5, 5)
        Me.trvButtons.Name = "trvButtons"
        Me.trvButtons.ShowLines = False
        Me.trvButtons.ShowPlusMinus = False
        Me.trvButtons.ShowRootLines = False
        Me.trvButtons.Size = New System.Drawing.Size(239, 321)
        Me.trvButtons.TabIndex = 11
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(2, 3)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 325)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(585, 3)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 325)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(2, 2)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(584, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(2, 328)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(584, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'tls_ToolButton
        '
        Me.tls_ToolButton.BackColor = System.Drawing.Color.Transparent
        Me.tls_ToolButton.BackgroundImage = Global.gloToolStrip.My.Resources.Resources.Img_Toolstrip
        Me.tls_ToolButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_ToolButton.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_ToolButton.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_ToolButton.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSelectAll, Me.ts_btnClearAll, Me.ToolStripButton1, Me.ts_btnOk, Me.ts_btnCancel})
        Me.tls_ToolButton.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_ToolButton.Location = New System.Drawing.Point(0, 0)
        Me.tls_ToolButton.Name = "tls_ToolButton"
        Me.tls_ToolButton.Size = New System.Drawing.Size(588, 53)
        Me.tls_ToolButton.TabIndex = 0
        Me.tls_ToolButton.Text = "toolStrip1"
        '
        'ts_btnSelectAll
        '
        Me.ts_btnSelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSelectAll.Image = CType(resources.GetObject("ts_btnSelectAll.Image"), System.Drawing.Image)
        Me.ts_btnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSelectAll.Name = "ts_btnSelectAll"
        Me.ts_btnSelectAll.Size = New System.Drawing.Size(67, 50)
        Me.ts_btnSelectAll.Tag = "SelectAll"
        Me.ts_btnSelectAll.Text = "Select &All"
        Me.ts_btnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSelectAll.ToolTipText = "Select All"
        Me.ts_btnSelectAll.Visible = False
        '
        'ts_btnClearAll
        '
        Me.ts_btnClearAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClearAll.Image = CType(resources.GetObject("ts_btnClearAll.Image"), System.Drawing.Image)
        Me.ts_btnClearAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClearAll.Name = "ts_btnClearAll"
        Me.ts_btnClearAll.Size = New System.Drawing.Size(60, 50)
        Me.ts_btnClearAll.Tag = "ClearAll"
        Me.ts_btnClearAll.Text = "Clear &All"
        Me.ts_btnClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnClearAll.ToolTipText = "Clear All"
        Me.ts_btnClearAll.Visible = False
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(46, 50)
        Me.ToolStripButton1.Tag = "Reset"
        Me.ToolStripButton1.Text = "&Reset"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Save and Close"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tls_ToolButton)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(588, 54)
        Me.pnl_tlspTOP.TabIndex = 3
        '
        'img32
        '
        Me.img32.ImageStream = CType(resources.GetObject("img32.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.img32.TransparentColor = System.Drawing.Color.Transparent
        Me.img32.Images.SetKeyName(0, "Separator.png")
        Me.img32.Images.SetKeyName(1, "Main(32x32)Circle.png")
        '
        'img16
        '
        Me.img16.ImageStream = CType(resources.GetObject("img16.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.img16.TransparentColor = System.Drawing.Color.Transparent
        Me.img16.Images.SetKeyName(0, "Separator.png")
        Me.img16.Images.SetKeyName(1, "Blank.png")
        '
        'frmToolCustomize
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(588, 385)
        Me.Controls.Add(Me.pnlTOP)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmToolCustomize"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customize Toolbar"
        Me.pnlTOP.ResumeLayout(False)
        Me.tls_ToolButton.ResumeLayout(False)
        Me.tls_ToolButton.PerformLayout()
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTOP As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents tls_ToolButton As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents ts_btnSelectAll As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnClearAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents trvSelectedButtons As System.Windows.Forms.TreeView
    Friend WithEvents trvButtons As System.Windows.Forms.TreeView
    Friend WithEvents img32 As System.Windows.Forms.ImageList
    Friend WithEvents img16 As System.Windows.Forms.ImageList
    Private WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
End Class
