<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewDICOMAmendments
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewDICOMAmendments))
        Me.tls_Amendments = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlb_Notes = New System.Windows.Forms.ToolStripButton
        Me.tlb_Acknowledge = New System.Windows.Forms.ToolStripButton
        Me.tlb_Review = New System.Windows.Forms.ToolStripButton
        Me.tlb_Cancel = New System.Windows.Forms.ToolStripButton
        Me.pnlDescription = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblDescription = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.comboBox1 = New System.Windows.Forms.ComboBox
        Me.label5 = New System.Windows.Forms.Label
        Me.c1Amendments = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label13 = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.tls_Amendments.SuspendLayout()
        Me.pnlDescription.SuspendLayout()
        CType(Me.c1Amendments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tls_Amendments
        '
        Me.tls_Amendments.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Toolstrip
        Me.tls_Amendments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_Amendments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Amendments.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_Amendments.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_Acknowledge, Me.tlb_Notes, Me.tlb_Review, Me.tlb_Cancel})
        Me.tls_Amendments.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_Amendments.Location = New System.Drawing.Point(0, 0)
        Me.tls_Amendments.Name = "tls_Amendments"
        Me.tls_Amendments.Size = New System.Drawing.Size(600, 53)
        Me.tls_Amendments.TabIndex = 4
        Me.tls_Amendments.Text = "toolStrip1"
        '
        'tlb_Notes
        '
        Me.tlb_Notes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Notes.Image = CType(resources.GetObject("tlb_Notes.Image"), System.Drawing.Image)
        Me.tlb_Notes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Notes.Name = "tlb_Notes"
        Me.tlb_Notes.Size = New System.Drawing.Size(46, 50)
        Me.tlb_Notes.Tag = "Notes"
        Me.tlb_Notes.Text = "&Notes"
        Me.tlb_Notes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Notes.ToolTipText = "Notes"
        '
        'tlb_Acknowledge
        '
        Me.tlb_Acknowledge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Acknowledge.Image = CType(resources.GetObject("tlb_Acknowledge.Image"), System.Drawing.Image)
        Me.tlb_Acknowledge.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Acknowledge.Name = "tlb_Acknowledge"
        Me.tlb_Acknowledge.Size = New System.Drawing.Size(97, 50)
        Me.tlb_Acknowledge.Tag = "Acknowledge"
        Me.tlb_Acknowledge.Text = " &Acknowledge"
        Me.tlb_Acknowledge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Acknowledge.ToolTipText = "Acknowledge"
        '
        'tlb_Review
        '
        Me.tlb_Review.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Review.Image = CType(resources.GetObject("tlb_Review.Image"), System.Drawing.Image)
        Me.tlb_Review.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Review.Name = "tlb_Review"
        Me.tlb_Review.Size = New System.Drawing.Size(55, 50)
        Me.tlb_Review.Tag = "Review"
        Me.tlb_Review.Text = "&Review"
        Me.tlb_Review.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Review.ToolTipText = "Review"
        Me.tlb_Review.Visible = False
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
        'pnlDescription
        '
        Me.pnlDescription.BackColor = System.Drawing.Color.Transparent
        Me.pnlDescription.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Button
        Me.pnlDescription.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDescription.Controls.Add(Me.Label7)
        Me.pnlDescription.Controls.Add(Me.lblDescription)
        Me.pnlDescription.Controls.Add(Me.Label10)
        Me.pnlDescription.Controls.Add(Me.Label8)
        Me.pnlDescription.Controls.Add(Me.Label4)
        Me.pnlDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDescription.Location = New System.Drawing.Point(3, 0)
        Me.pnlDescription.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlDescription.Name = "pnlDescription"
        Me.pnlDescription.Size = New System.Drawing.Size(594, 25)
        Me.pnlDescription.TabIndex = 29
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(593, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 23)
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
        Me.lblDescription.Size = New System.Drawing.Size(267, 23)
        Me.lblDescription.TabIndex = 29
        Me.lblDescription.Text = "  Description :"
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(1, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(593, 1)
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
        Me.Label8.Size = New System.Drawing.Size(1, 24)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(0, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(594, 1)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "label2"
        '
        'comboBox1
        '
        Me.comboBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.comboBox1.ForeColor = System.Drawing.Color.Black
        Me.comboBox1.FormattingEnabled = True
        Me.comboBox1.Location = New System.Drawing.Point(56, 1)
        Me.comboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.comboBox1.Name = "comboBox1"
        Me.comboBox1.Size = New System.Drawing.Size(174, 22)
        Me.comboBox1.TabIndex = 4
        '
        'label5
        '
        Me.label5.AutoEllipsis = True
        Me.label5.BackColor = System.Drawing.Color.Transparent
        Me.label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.Location = New System.Drawing.Point(1, 1)
        Me.label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(55, 22)
        Me.label5.TabIndex = 2
        Me.label5.Text = "  User :"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'c1Amendments
        '
        Me.c1Amendments.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Amendments.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Amendments.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.c1Amendments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Amendments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Amendments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Amendments.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1Amendments.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1Amendments.Location = New System.Drawing.Point(4, 1)
        Me.c1Amendments.Name = "c1Amendments"
        Me.c1Amendments.Rows.Count = 5
        Me.c1Amendments.Rows.DefaultSize = 19
        Me.c1Amendments.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Amendments.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1Amendments.Size = New System.Drawing.Size(592, 328)
        Me.c1Amendments.StyleInfo = resources.GetString("c1Amendments.StyleInfo")
        Me.c1Amendments.TabIndex = 31
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.comboBox1)
        Me.Panel2.Controls.Add(Me.label5)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(594, 24)
        Me.Panel2.TabIndex = 21
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(593, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 22)
        Me.Label13.TabIndex = 37
        Me.Label13.Text = "label3"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 22)
        Me.lbl_LeftBrd.TabIndex = 34
        Me.lbl_LeftBrd.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(594, 1)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(0, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(594, 1)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "label2"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.c1Amendments)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 114)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(600, 333)
        Me.Panel4.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(4, 329)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(592, 1)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(596, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 329)
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
        Me.Label14.Size = New System.Drawing.Size(1, 329)
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
        Me.Label15.Size = New System.Drawing.Size(594, 1)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "label1"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.pnlDescription)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 86)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel5.Size = New System.Drawing.Size(600, 28)
        Me.Panel5.TabIndex = 23
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.tls_Amendments)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(600, 56)
        Me.Panel6.TabIndex = 24
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 56)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(600, 30)
        Me.Panel1.TabIndex = 25
        Me.Panel1.Visible = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmViewDICOMAmendments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(600, 447)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel6)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewDICOMAmendments"
        Me.Text = "Amendments"
        Me.tls_Amendments.ResumeLayout(False)
        Me.tls_Amendments.PerformLayout()
        Me.pnlDescription.ResumeLayout(False)
        CType(Me.c1Amendments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tls_Amendments As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlb_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents tlb_Notes As System.Windows.Forms.ToolStripButton
    Private WithEvents tlb_Acknowledge As System.Windows.Forms.ToolStripButton
    Private WithEvents pnlDescription As System.Windows.Forms.Panel
    Private WithEvents lblDescription As System.Windows.Forms.Label
    Private WithEvents comboBox1 As System.Windows.Forms.ComboBox
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents tlb_Review As System.Windows.Forms.ToolStripButton
    Friend WithEvents c1Amendments As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
