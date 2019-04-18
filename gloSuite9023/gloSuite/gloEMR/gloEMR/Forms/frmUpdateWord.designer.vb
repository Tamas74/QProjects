<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateWord
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdateWord))
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbCategory = New System.Windows.Forms.ComboBox
        Me.pgrbarStatus = New System.Windows.Forms.ProgressBar
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.lbl_BottomBorder = New System.Windows.Forms.Label
        Me.lbl_LeftBorder = New System.Windows.Forms.Label
        Me.lbl_RightBorder = New System.Windows.Forms.Label
        Me.lbl_TopBorder = New System.Windows.Forms.Label
        Me.lblStatus = New System.Windows.Forms.Label
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.tls = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnUpdate = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.wdTemplate = New AxDSOFramer.AxFramerControl
        Me.pnlMain.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tls.SuspendLayout()
        CType(Me.wdTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select :"
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.ForeColor = System.Drawing.Color.Black
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(65, 21)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(302, 22)
        Me.cmbCategory.TabIndex = 0
        '
        'pgrbarStatus
        '
        Me.pgrbarStatus.BackColor = System.Drawing.Color.White
        Me.pgrbarStatus.ForeColor = System.Drawing.Color.LimeGreen
        Me.pgrbarStatus.Location = New System.Drawing.Point(22, 61)
        Me.pgrbarStatus.Name = "pgrbarStatus"
        Me.pgrbarStatus.Size = New System.Drawing.Size(345, 18)
        Me.pgrbarStatus.Step = 1
        Me.pgrbarStatus.TabIndex = 3
        Me.pgrbarStatus.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.lbl_BottomBorder)
        Me.pnlMain.Controls.Add(Me.lbl_LeftBorder)
        Me.pnlMain.Controls.Add(Me.lbl_RightBorder)
        Me.pnlMain.Controls.Add(Me.lbl_TopBorder)
        Me.pnlMain.Controls.Add(Me.pgrbarStatus)
        Me.pnlMain.Controls.Add(Me.lblStatus)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.cmbCategory)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(394, 95)
        Me.pnlMain.TabIndex = 6
        '
        'lbl_BottomBorder
        '
        Me.lbl_BottomBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBorder.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBorder.Location = New System.Drawing.Point(4, 91)
        Me.lbl_BottomBorder.Name = "lbl_BottomBorder"
        Me.lbl_BottomBorder.Size = New System.Drawing.Size(386, 1)
        Me.lbl_BottomBorder.TabIndex = 8
        Me.lbl_BottomBorder.Text = "label2"
        '
        'lbl_LeftBorder
        '
        Me.lbl_LeftBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBorder.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBorder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBorder.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBorder.Name = "lbl_LeftBorder"
        Me.lbl_LeftBorder.Size = New System.Drawing.Size(1, 88)
        Me.lbl_LeftBorder.TabIndex = 7
        Me.lbl_LeftBorder.Text = "label4"
        '
        'lbl_RightBorder
        '
        Me.lbl_RightBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBorder.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBorder.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBorder.Location = New System.Drawing.Point(390, 4)
        Me.lbl_RightBorder.Name = "lbl_RightBorder"
        Me.lbl_RightBorder.Size = New System.Drawing.Size(1, 88)
        Me.lbl_RightBorder.TabIndex = 6
        Me.lbl_RightBorder.Text = "label3"
        '
        'lbl_TopBorder
        '
        Me.lbl_TopBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBorder.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBorder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBorder.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBorder.Name = "lbl_TopBorder"
        Me.lbl_TopBorder.Size = New System.Drawing.Size(388, 1)
        Me.lbl_TopBorder.TabIndex = 5
        Me.lbl_TopBorder.Text = "label1"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(290, 25)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 14)
        Me.lblStatus.TabIndex = 2
        Me.lblStatus.Visible = False
        '
        'pnlBottom
        '
        Me.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBottom.Controls.Add(Me.tls)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlBottom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlBottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(394, 53)
        Me.pnlBottom.TabIndex = 5
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = CType(resources.GetObject("tls.BackgroundImage"), System.Drawing.Image)
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnUpdate, Me.ts_btnClose})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(0, 0)
        Me.tls.Name = "tls"
        Me.tls.Size = New System.Drawing.Size(394, 53)
        Me.tls.TabIndex = 1
        Me.tls.Text = "toolStrip1"
        '
        'ts_btnUpdate
        '
        Me.ts_btnUpdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnUpdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnUpdate.Image = CType(resources.GetObject("ts_btnUpdate.Image"), System.Drawing.Image)
        Me.ts_btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnUpdate.Name = "ts_btnUpdate"
        Me.ts_btnUpdate.Size = New System.Drawing.Size(55, 50)
        Me.ts_btnUpdate.Tag = "Update"
        Me.ts_btnUpdate.Text = "&Update"
        Me.ts_btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'wdTemplate
        '
        Me.wdTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdTemplate.Enabled = True
        Me.wdTemplate.Location = New System.Drawing.Point(0, 0)
        Me.wdTemplate.Name = "wdTemplate"
        Me.wdTemplate.OcxState = CType(resources.GetObject("wdTemplate.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdTemplate.Size = New System.Drawing.Size(394, 148)
        Me.wdTemplate.TabIndex = 4
        '
        'frmUpdateWord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(394, 148)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.wdTemplate)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdateWord"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Upgrade Templates"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        CType(Me.wdTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents pgrbarStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents wdTemplate As AxDSOFramer.AxFramerControl
    Private WithEvents lbl_BottomBorder As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBorder As System.Windows.Forms.Label
    Private WithEvents lbl_RightBorder As System.Windows.Forms.Label
    Private WithEvents lbl_TopBorder As System.Windows.Forms.Label
    Private WithEvents tls As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnUpdate As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
End Class
