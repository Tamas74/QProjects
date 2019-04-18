<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCardiologyDevice
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCardiologyDevice))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tlstrip_Refresh = New System.Windows.Forms.ToolStripButton
        Me.tlstrip_Delete = New System.Windows.Forms.ToolStripButton
        Me.tlstrip_Save = New System.Windows.Forms.ToolStripButton
        Me.tlstrip_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlPatientDetails = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label175 = New System.Windows.Forms.Label
        Me.Label177 = New System.Windows.Forms.Label
        Me.C1Cardiology = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label115 = New System.Windows.Forms.Label
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label119 = New System.Windows.Forms.Label
        Me.Label118 = New System.Windows.Forms.Label
        Me.Label117 = New System.Windows.Forms.Label
        Me.Label116 = New System.Windows.Forms.Label
        Me.btnClear = New System.Windows.Forms.Button
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStrip1.SuspendLayout()
        Me.pnlPatientDetails.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.C1Cardiology, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlstrip_Refresh, Me.tlstrip_Delete, Me.tlstrip_Save, Me.tlstrip_Close})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(869, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tlstrip_Refresh
        '
        Me.tlstrip_Refresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlstrip_Refresh.Image = CType(resources.GetObject("tlstrip_Refresh.Image"), System.Drawing.Image)
        Me.tlstrip_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlstrip_Refresh.Name = "tlstrip_Refresh"
        Me.tlstrip_Refresh.Size = New System.Drawing.Size(58, 50)
        Me.tlstrip_Refresh.Text = "&Refresh"
        Me.tlstrip_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlstrip_Delete
        '
        Me.tlstrip_Delete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlstrip_Delete.Image = CType(resources.GetObject("tlstrip_Delete.Image"), System.Drawing.Image)
        Me.tlstrip_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlstrip_Delete.Name = "tlstrip_Delete"
        Me.tlstrip_Delete.Size = New System.Drawing.Size(50, 50)
        Me.tlstrip_Delete.Text = "&Delete"
        Me.tlstrip_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlstrip_Save
        '
        Me.tlstrip_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlstrip_Save.Image = CType(resources.GetObject("tlstrip_Save.Image"), System.Drawing.Image)
        Me.tlstrip_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlstrip_Save.Name = "tlstrip_Save"
        Me.tlstrip_Save.Size = New System.Drawing.Size(66, 50)
        Me.tlstrip_Save.Text = "Sa&ve&&Cls"
        Me.tlstrip_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlstrip_Save.ToolTipText = "Save and Close"
        '
        'tlstrip_Close
        '
        Me.tlstrip_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlstrip_Close.Image = CType(resources.GetObject("tlstrip_Close.Image"), System.Drawing.Image)
        Me.tlstrip_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlstrip_Close.Name = "tlstrip_Close"
        Me.tlstrip_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlstrip_Close.Text = "&Close"
        Me.tlstrip_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlPatientDetails
        '
        Me.pnlPatientDetails.Controls.Add(Me.Panel1)
        Me.pnlPatientDetails.Controls.Add(Me.Panel3)
        Me.pnlPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientDetails.Location = New System.Drawing.Point(0, 56)
        Me.pnlPatientDetails.Name = "pnlPatientDetails"
        Me.pnlPatientDetails.Size = New System.Drawing.Size(869, 592)
        Me.pnlPatientDetails.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label175)
        Me.Panel1.Controls.Add(Me.Label177)
        Me.Panel1.Controls.Add(Me.C1Cardiology)
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(869, 562)
        Me.Panel1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(865, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 557)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 558)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(862, 1)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "label1"
        '
        'Label175
        '
        Me.Label175.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label175.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label175.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label175.Location = New System.Drawing.Point(3, 1)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(1, 558)
        Me.Label175.TabIndex = 9
        Me.Label175.Text = "label4"
        '
        'Label177
        '
        Me.Label177.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label177.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label177.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label177.Location = New System.Drawing.Point(3, 0)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(863, 1)
        Me.Label177.TabIndex = 8
        Me.Label177.Text = "label1"
        '
        'C1Cardiology
        '
        Me.C1Cardiology.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Cardiology.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Cardiology.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1Cardiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Cardiology.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Cardiology.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Cardiology.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Cardiology.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Cardiology.Location = New System.Drawing.Point(3, 0)
        Me.C1Cardiology.Name = "C1Cardiology"
        Me.C1Cardiology.Rows.DefaultSize = 19
        Me.C1Cardiology.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Cardiology.Size = New System.Drawing.Size(863, 559)
        Me.C1Cardiology.StyleInfo = resources.GetString("C1Cardiology.StyleInfo")
        Me.C1Cardiology.TabIndex = 2
        Me.C1Cardiology.Tree.NodeImageCollapsed = CType(resources.GetObject("C1Cardiology.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1Cardiology.Tree.NodeImageExpanded = CType(resources.GetObject("C1Cardiology.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.Controls.Add(Me.Label14)
        Me.Panel8.Controls.Add(Me.Label115)
        Me.Panel8.Controls.Add(Me.PictureBox3)
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.ForeColor = System.Drawing.Color.Black
        Me.Panel8.Location = New System.Drawing.Point(388, 145)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(251, 21)
        Me.Panel8.TabIndex = 20
        Me.Panel8.Visible = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.White
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Location = New System.Drawing.Point(27, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(224, 4)
        Me.Label14.TabIndex = 37
        '
        'Label115
        '
        Me.Label115.BackColor = System.Drawing.Color.White
        Me.Label115.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label115.Location = New System.Drawing.Point(27, 19)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(224, 2)
        Me.Label115.TabIndex = 38
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.White
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(27, 21)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 9
        Me.PictureBox3.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label119)
        Me.Panel3.Controls.Add(Me.Label117)
        Me.Panel3.Controls.Add(Me.Label116)
        Me.Panel3.Controls.Add(Me.Label118)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel3.Size = New System.Drawing.Size(869, 30)
        Me.Panel3.TabIndex = 22
        '
        'Label119
        '
        Me.Label119.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label119.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label119.Location = New System.Drawing.Point(865, 4)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(1, 22)
        Me.Label119.TabIndex = 40
        Me.Label119.Text = "label4"
        '
        'Label118
        '
        Me.Label118.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label118.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label118.Location = New System.Drawing.Point(3, 3)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(1, 24)
        Me.Label118.TabIndex = 39
        Me.Label118.Text = "label4"
        '
        'Label117
        '
        Me.Label117.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label117.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label117.Location = New System.Drawing.Point(4, 3)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(862, 1)
        Me.Label117.TabIndex = 36
        Me.Label117.Text = "label1"
        '
        'Label116
        '
        Me.Label116.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label116.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label116.Location = New System.Drawing.Point(4, 26)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(862, 1)
        Me.Label116.TabIndex = 35
        Me.Label116.Text = "label1"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.Transparent
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(751, 12)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 50
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = False
        Me.btnClear.Visible = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(509, 12)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(224, 22)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(435, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label3.Size = New System.Drawing.Size(68, 20)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "  Search :"
        Me.Label3.Visible = False
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.btnClear)
        Me.pnlToolStrip.Controls.Add(Me.Label3)
        Me.pnlToolStrip.Controls.Add(Me.txtSearch)
        Me.pnlToolStrip.Controls.Add(Me.ToolStrip1)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(869, 56)
        Me.pnlToolStrip.TabIndex = 0
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmCardiologyDevice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(869, 648)
        Me.Controls.Add(Me.pnlPatientDetails)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCardiologyDevice"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Implant Device"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlPatientDetails.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.C1Cardiology, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents pnlPatientDetails As System.Windows.Forms.Panel
    Friend WithEvents tlstrip_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlstrip_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlstrip_Delete As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlstrip_Refresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents C1Cardiology As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Private WithEvents Label116 As System.Windows.Forms.Label
    Private WithEvents Label117 As System.Windows.Forms.Label
    Private WithEvents Label118 As System.Windows.Forms.Label
    Private WithEvents Label119 As System.Windows.Forms.Label
    Private WithEvents Label175 As System.Windows.Forms.Label
    Private WithEvents Label177 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
