<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSBPTransaction
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSBPTransaction))
        Me.pnl_toolstrip = New System.Windows.Forms.Panel()
        Me.lblTransactionDate = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnl_trv = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.trvSBPVisits = New System.Windows.Forms.TreeView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_Visits = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.pnlWebbrowserCtrl = New System.Windows.Forms.Panel()
        Me.pnl_toolstrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnl_trv.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_toolstrip
        '
        Me.pnl_toolstrip.Controls.Add(Me.lblTransactionDate)
        Me.pnl_toolstrip.Controls.Add(Me.ToolStrip1)
        Me.pnl_toolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_toolstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_toolstrip.Name = "pnl_toolstrip"
        Me.pnl_toolstrip.Size = New System.Drawing.Size(743, 56)
        Me.pnl_toolstrip.TabIndex = 6
        '
        'lblTransactionDate
        '
        Me.lblTransactionDate.AutoSize = True
        Me.lblTransactionDate.Location = New System.Drawing.Point(508, 37)
        Me.lblTransactionDate.Name = "lblTransactionDate"
        Me.lblTransactionDate.Size = New System.Drawing.Size(108, 14)
        Me.lblTransactionDate.TabIndex = 1
        Me.lblTransactionDate.Text = "Transaction Date: "
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(743, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnRefresh.ToolTipText = "Refresh Pending Refill Request List"
        Me.ts_btnRefresh.Visible = False
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnl_trv
        '
        Me.pnl_trv.Controls.Add(Me.Panel5)
        Me.pnl_trv.Controls.Add(Me.Panel6)
        Me.pnl_trv.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_trv.Location = New System.Drawing.Point(0, 56)
        Me.pnl_trv.Name = "pnl_trv"
        Me.pnl_trv.Size = New System.Drawing.Size(213, 483)
        Me.pnl_trv.TabIndex = 7
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label39)
        Me.Panel5.Controls.Add(Me.Label40)
        Me.Panel5.Controls.Add(Me.Label41)
        Me.Panel5.Controls.Add(Me.Label42)
        Me.Panel5.Controls.Add(Me.trvSBPVisits)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 27)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel5.Size = New System.Drawing.Size(213, 456)
        Me.Panel5.TabIndex = 2
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label39.Location = New System.Drawing.Point(4, 452)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(208, 1)
        Me.Label39.TabIndex = 8
        Me.Label39.Text = "label2"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(3, 1)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 452)
        Me.Label40.TabIndex = 7
        Me.Label40.Text = "label4"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(212, 1)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 452)
        Me.Label41.TabIndex = 6
        Me.Label41.Text = "label3"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(3, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(210, 1)
        Me.Label42.TabIndex = 5
        Me.Label42.Text = "label1"
        '
        'trvSBPVisits
        '
        Me.trvSBPVisits.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSBPVisits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSBPVisits.ForeColor = System.Drawing.Color.Black
        Me.trvSBPVisits.ItemHeight = 20
        Me.trvSBPVisits.Location = New System.Drawing.Point(3, 0)
        Me.trvSBPVisits.Name = "trvSBPVisits"
        Me.trvSBPVisits.Size = New System.Drawing.Size(210, 453)
        Me.trvSBPVisits.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(213, 27)
        Me.Panel6.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lbl_Visits)
        Me.Panel2.Controls.Add(Me.Label43)
        Me.Panel2.Controls.Add(Me.Label44)
        Me.Panel2.Controls.Add(Me.Label45)
        Me.Panel2.Controls.Add(Me.Label46)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(210, 24)
        Me.Panel2.TabIndex = 1
        '
        'lbl_Visits
        '
        Me.lbl_Visits.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Visits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Visits.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Visits.Location = New System.Drawing.Point(1, 1)
        Me.lbl_Visits.Name = "lbl_Visits"
        Me.lbl_Visits.Size = New System.Drawing.Size(208, 22)
        Me.lbl_Visits.TabIndex = 0
        Me.lbl_Visits.Text = "Transaction Date"
        Me.lbl_Visits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lbl_Visits.Visible = False
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(1, 23)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(208, 1)
        Me.Label43.TabIndex = 8
        Me.Label43.Text = "label2"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 1)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1, 23)
        Me.Label44.TabIndex = 7
        Me.Label44.Text = "label4"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(209, 1)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 23)
        Me.Label45.TabIndex = 6
        Me.Label45.Text = "label3"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(0, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(210, 1)
        Me.Label46.TabIndex = 5
        Me.Label46.Text = "label1"
        '
        'pnlWebbrowserCtrl
        '
        Me.pnlWebbrowserCtrl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWebbrowserCtrl.Location = New System.Drawing.Point(213, 56)
        Me.pnlWebbrowserCtrl.Name = "pnlWebbrowserCtrl"
        Me.pnlWebbrowserCtrl.Size = New System.Drawing.Size(530, 483)
        Me.pnlWebbrowserCtrl.TabIndex = 8
        '
        'frmSBPTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(743, 539)
        Me.Controls.Add(Me.pnlWebbrowserCtrl)
        Me.Controls.Add(Me.pnl_trv)
        Me.Controls.Add(Me.pnl_toolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSBPTransaction"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmSBPTransaction"
        Me.pnl_toolstrip.ResumeLayout(False)
        Me.pnl_toolstrip.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnl_trv.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnl_toolstrip As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnl_trv As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents trvSBPVisits As System.Windows.Forms.TreeView
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Visits As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents pnlWebbrowserCtrl As System.Windows.Forms.Panel
    Friend WithEvents lblTransactionDate As System.Windows.Forms.Label
End Class
