<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLabMakeNormalLotes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLabMakeNormalLotes))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.cmbPatientNotes = New System.Windows.Forms.ComboBox()
        Me.lblPatientNotes = New System.Windows.Forms.Label()
        Me.cmbInternalNotes = New System.Windows.Forms.ComboBox()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.tlsp_NormalNotes = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlTop.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_NormalNotes.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.cmbPatientNotes)
        Me.pnlTop.Controls.Add(Me.lblPatientNotes)
        Me.pnlTop.Controls.Add(Me.cmbInternalNotes)
        Me.pnlTop.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlTop.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlTop.Controls.Add(Me.lbl_pnlRight)
        Me.pnlTop.Controls.Add(Me.lbl_pnlTop)
        Me.pnlTop.Controls.Add(Me.lblNotes)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 53)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(400, 108)
        Me.pnlTop.TabIndex = 1
        '
        'cmbPatientNotes
        '
        Me.cmbPatientNotes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPatientNotes.FormattingEnabled = True
        Me.cmbPatientNotes.Location = New System.Drawing.Point(148, 56)
        Me.cmbPatientNotes.Name = "cmbPatientNotes"
        Me.cmbPatientNotes.Size = New System.Drawing.Size(222, 22)
        Me.cmbPatientNotes.TabIndex = 2
        '
        'lblPatientNotes
        '
        Me.lblPatientNotes.AutoSize = True
        Me.lblPatientNotes.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientNotes.Location = New System.Drawing.Point(55, 59)
        Me.lblPatientNotes.Name = "lblPatientNotes"
        Me.lblPatientNotes.Size = New System.Drawing.Size(90, 14)
        Me.lblPatientNotes.TabIndex = 10
        Me.lblPatientNotes.Text = "Patient Notes :"
        '
        'cmbInternalNotes
        '
        Me.cmbInternalNotes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInternalNotes.FormattingEnabled = True
        Me.cmbInternalNotes.Location = New System.Drawing.Point(148, 24)
        Me.cmbInternalNotes.Name = "cmbInternalNotes"
        Me.cmbInternalNotes.Size = New System.Drawing.Size(222, 22)
        Me.cmbInternalNotes.TabIndex = 1
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 104)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(392, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 101)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(396, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 101)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(394, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'lblNotes
        '
        Me.lblNotes.AutoSize = True
        Me.lblNotes.BackColor = System.Drawing.Color.Transparent
        Me.lblNotes.Location = New System.Drawing.Point(26, 28)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(119, 14)
        Me.lblNotes.TabIndex = 2
        Me.lblNotes.Text = "Internal Comments :"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_NormalNotes)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(400, 53)
        Me.pnl_tlsp.TabIndex = 2
        Me.pnl_tlsp.TabStop = True
        '
        'tlsp_NormalNotes
        '
        Me.tlsp_NormalNotes.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_NormalNotes.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_NormalNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_NormalNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_NormalNotes.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_NormalNotes.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_NormalNotes.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_NormalNotes.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_NormalNotes.Name = "tlsp_NormalNotes"
        Me.tlsp_NormalNotes.Size = New System.Drawing.Size(400, 53)
        Me.tlsp_NormalNotes.TabIndex = 0
        Me.tlsp_NormalNotes.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmLabMakeNormalLotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(400, 161)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLabMakeNormalLotes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Normal Notes"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_NormalNotes.ResumeLayout(False)
        Me.tlsp_NormalNotes.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents cmbPatientNotes As System.Windows.Forms.ComboBox
    Friend WithEvents lblPatientNotes As System.Windows.Forms.Label
    Friend WithEvents cmbInternalNotes As System.Windows.Forms.ComboBox
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_NormalNotes As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
End Class
