<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExamIntervention
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExamIntervention))
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.c1Intervention = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.pnlDescription = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblDescription = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.tls_Intervention = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlb_Save = New System.Windows.Forms.ToolStripButton
        Me.tlb_Cancel = New System.Windows.Forms.ToolStripButton
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Panel4.SuspendLayout()
        CType(Me.c1Intervention, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.pnlDescription.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.tls_Intervention.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.c1Intervention)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 83)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(601, 356)
        Me.Panel4.TabIndex = 23
        '
        'c1Intervention
        '
        Me.c1Intervention.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Intervention.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Intervention.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.c1Intervention.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Intervention.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Intervention.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Intervention.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1Intervention.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1Intervention.Location = New System.Drawing.Point(4, 1)
        Me.c1Intervention.Name = "c1Intervention"
        Me.c1Intervention.Rows.Count = 5
        Me.c1Intervention.Rows.DefaultSize = 19
        Me.c1Intervention.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Intervention.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1Intervention.Size = New System.Drawing.Size(593, 351)
        Me.c1Intervention.StyleInfo = resources.GetString("c1Intervention.StyleInfo")
        Me.c1Intervention.TabIndex = 31
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(4, 352)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(593, 1)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(597, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 352)
        Me.Label12.TabIndex = 37
        Me.Label12.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 352)
        Me.Label14.TabIndex = 34
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(595, 1)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "label1"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.pnlDescription)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 54)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.Panel5.Size = New System.Drawing.Size(601, 29)
        Me.Panel5.TabIndex = 38
        '
        'pnlDescription
        '
        Me.pnlDescription.BackColor = System.Drawing.Color.Transparent
        Me.pnlDescription.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlDescription.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDescription.Controls.Add(Me.Label7)
        Me.pnlDescription.Controls.Add(Me.lblDescription)
        Me.pnlDescription.Controls.Add(Me.Label10)
        Me.pnlDescription.Controls.Add(Me.Label8)
        Me.pnlDescription.Controls.Add(Me.Label4)
        Me.pnlDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDescription.Location = New System.Drawing.Point(3, 2)
        Me.pnlDescription.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlDescription.Name = "pnlDescription"
        Me.pnlDescription.Size = New System.Drawing.Size(595, 24)
        Me.pnlDescription.TabIndex = 29
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(594, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 22)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "label3"
        '
        'lblDescription
        '
        Me.lblDescription.AutoEllipsis = True
        Me.lblDescription.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblDescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(1, 1)
        Me.lblDescription.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(210, 22)
        Me.lblDescription.TabIndex = 29
        Me.lblDescription.Text = "  Intervention Description "
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(1, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(594, 1)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "label1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 23)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(0, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(595, 1)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "label2"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.tls_Intervention)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(601, 54)
        Me.Panel6.TabIndex = 25
        '
        'tls_Intervention
        '
        Me.tls_Intervention.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_Intervention.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_Intervention.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Intervention.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_Intervention.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_Save, Me.tlb_Cancel})
        Me.tls_Intervention.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_Intervention.Location = New System.Drawing.Point(0, 0)
        Me.tls_Intervention.Name = "tls_Intervention"
        Me.tls_Intervention.Size = New System.Drawing.Size(601, 53)
        Me.tls_Intervention.TabIndex = 4
        Me.tls_Intervention.Text = "toolStrip1"
        '
        'tlb_Save
        '
        Me.tlb_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Save.Image = CType(resources.GetObject("tlb_Save.Image"), System.Drawing.Image)
        Me.tlb_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Save.Name = "tlb_Save"
        Me.tlb_Save.Size = New System.Drawing.Size(66, 50)
        Me.tlb_Save.Tag = "Save"
        Me.tlb_Save.Text = "&Save&&Cls"
        Me.tlb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Save.ToolTipText = "Save and Close"
        '
        'tlb_Cancel
        '
        Me.tlb_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Cancel.Image = CType(resources.GetObject("tlb_Cancel.Image"), System.Drawing.Image)
        Me.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Cancel.Name = "tlb_Cancel"
        Me.tlb_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.tlb_Cancel.Tag = "Close"
        Me.tlb_Cancel.Text = "&Close"
        Me.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Cancel.ToolTipText = "Close"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmExamIntervention
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(601, 439)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel6)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExamIntervention"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Exam Intervention"
        Me.Panel4.ResumeLayout(False)
        CType(Me.c1Intervention, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.pnlDescription.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.tls_Intervention.ResumeLayout(False)
        Me.tls_Intervention.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents c1Intervention As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents pnlDescription As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents lblDescription As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents tls_Intervention As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlb_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents tlb_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
