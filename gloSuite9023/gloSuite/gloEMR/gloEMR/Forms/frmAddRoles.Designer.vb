<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddRoles
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddRoles))
        Me.Pnl_grid = New System.Windows.Forms.Panel
        Me.C1Providers_Roles = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.tlsbtnSave = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton
        Me.tstrip_Roles = New gloGlobal.gloToolStripIgnoreFocus
        Me.Pnl_Toolstirp = New System.Windows.Forms.Panel
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Pnl_grid.SuspendLayout()
        CType(Me.C1Providers_Roles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tstrip_Roles.SuspendLayout()
        Me.Pnl_Toolstirp.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl_grid
        '
        Me.Pnl_grid.Controls.Add(Me.C1Providers_Roles)
        Me.Pnl_grid.Controls.Add(Me.Label5)
        Me.Pnl_grid.Controls.Add(Me.Label6)
        Me.Pnl_grid.Controls.Add(Me.Label7)
        Me.Pnl_grid.Controls.Add(Me.Label8)
        Me.Pnl_grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pnl_grid.Location = New System.Drawing.Point(0, 54)
        Me.Pnl_grid.Name = "Pnl_grid"
        Me.Pnl_grid.Padding = New System.Windows.Forms.Padding(3)
        Me.Pnl_grid.Size = New System.Drawing.Size(568, 380)
        Me.Pnl_grid.TabIndex = 0
        '
        'C1Providers_Roles
        '
        Me.C1Providers_Roles.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Providers_Roles.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1Providers_Roles.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Providers_Roles.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Providers_Roles.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
            ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Providers_Roles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Providers_Roles.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Providers_Roles.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Providers_Roles.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1Providers_Roles.Location = New System.Drawing.Point(4, 4)
        Me.C1Providers_Roles.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1Providers_Roles.Name = "C1Providers_Roles"
        Me.C1Providers_Roles.Rows.Count = 1
        Me.C1Providers_Roles.Rows.DefaultSize = 19
        Me.C1Providers_Roles.Rows.Fixed = 0
        Me.C1Providers_Roles.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Providers_Roles.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Providers_Roles.ShowSort = False
        Me.C1Providers_Roles.Size = New System.Drawing.Size(560, 372)
        Me.C1Providers_Roles.StyleInfo = resources.GetString("C1Providers_Roles.StyleInfo")
        Me.C1Providers_Roles.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 376)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(560, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 373)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(564, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 373)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(562, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'tlsbtnSave
        '
        Me.tlsbtnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnSave.Image = CType(resources.GetObject("tlsbtnSave.Image"), System.Drawing.Image)
        Me.tlsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnSave.Name = "tlsbtnSave"
        Me.tlsbtnSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsbtnSave.Text = "&Save&&Cls"
        Me.tlsbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnSave.ToolTipText = "Save and Close"
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'tstrip_Roles
        '
        Me.tstrip_Roles.BackColor = System.Drawing.Color.Transparent
        Me.tstrip_Roles.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tstrip_Roles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip_Roles.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip_Roles.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip_Roles.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip_Roles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnSave, Me.tlsbtnClose})
        Me.tstrip_Roles.Location = New System.Drawing.Point(0, 0)
        Me.tstrip_Roles.Name = "tstrip_Roles"
        Me.tstrip_Roles.Size = New System.Drawing.Size(568, 53)
        Me.tstrip_Roles.TabIndex = 1
        Me.tstrip_Roles.Text = "ToolStrip1"
        '
        'Pnl_Toolstirp
        '
        Me.Pnl_Toolstirp.Controls.Add(Me.tstrip_Roles)
        Me.Pnl_Toolstirp.Dock = System.Windows.Forms.DockStyle.Top
        Me.Pnl_Toolstirp.Location = New System.Drawing.Point(0, 0)
        Me.Pnl_Toolstirp.Name = "Pnl_Toolstirp"
        Me.Pnl_Toolstirp.Size = New System.Drawing.Size(568, 54)
        Me.Pnl_Toolstirp.TabIndex = 2
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmAddRoles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(568, 434)
        Me.Controls.Add(Me.Pnl_grid)
        Me.Controls.Add(Me.Pnl_Toolstirp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddRoles"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Specify Provider's Role"
        Me.Pnl_grid.ResumeLayout(False)
        CType(Me.C1Providers_Roles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tstrip_Roles.ResumeLayout(False)
        Me.tstrip_Roles.PerformLayout()
        Me.Pnl_Toolstirp.ResumeLayout(False)
        Me.Pnl_Toolstirp.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pnl_grid As System.Windows.Forms.Panel
    Friend WithEvents C1Providers_Roles As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tlsbtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tstrip_Roles As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents Pnl_Toolstirp As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
