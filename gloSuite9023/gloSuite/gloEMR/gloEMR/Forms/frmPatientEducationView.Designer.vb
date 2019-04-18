<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientEducationView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientEducationView))
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnPrint = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.ts_btnExport = New System.Windows.Forms.ToolStripButton
        Me.wdPatientEducation = New AxDSOFramer.AxFramerControl
        Me.pnlts_ViewButtons = New System.Windows.Forms.Panel
        Me.pnlwdPatientEducation = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.ts_ViewButtons.SuspendLayout()
        CType(Me.wdPatientEducation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlts_ViewButtons.SuspendLayout()
        Me.pnlwdPatientEducation.SuspendLayout()
        Me.SuspendLayout()
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnPrint, Me.ts_btnClose, Me.ts_btnExport})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(1011, 53)
        Me.ts_ViewButtons.TabIndex = 5
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnPrint
        '
        Me.ts_btnPrint.Image = CType(resources.GetObject("ts_btnPrint.Image"), System.Drawing.Image)
        Me.ts_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPrint.Name = "ts_btnPrint"
        Me.ts_btnPrint.Size = New System.Drawing.Size(41, 50)
        Me.ts_btnPrint.Tag = "Print"
        Me.ts_btnPrint.Text = "&Print"
        Me.ts_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnExport
        '
        Me.ts_btnExport.Image = CType(resources.GetObject("ts_btnExport.Image"), System.Drawing.Image)
        Me.ts_btnExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnExport.Name = "ts_btnExport"
        Me.ts_btnExport.Size = New System.Drawing.Size(52, 50)
        Me.ts_btnExport.Tag = "Print"
        Me.ts_btnExport.Text = "&Export"
        Me.ts_btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'wdPatientEducation
        '
        Me.wdPatientEducation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPatientEducation.Enabled = True
        Me.wdPatientEducation.Location = New System.Drawing.Point(4, 4)
        Me.wdPatientEducation.Name = "wdPatientEducation"
        Me.wdPatientEducation.OcxState = CType(resources.GetObject("wdPatientEducation.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPatientEducation.Size = New System.Drawing.Size(1003, 718)
        Me.wdPatientEducation.TabIndex = 6
        '
        'pnlts_ViewButtons
        '
        Me.pnlts_ViewButtons.Controls.Add(Me.ts_ViewButtons)
        Me.pnlts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.pnlts_ViewButtons.Name = "pnlts_ViewButtons"
        Me.pnlts_ViewButtons.Size = New System.Drawing.Size(1011, 54)
        Me.pnlts_ViewButtons.TabIndex = 7
        '
        'pnlwdPatientEducation
        '
        Me.pnlwdPatientEducation.BackColor = System.Drawing.Color.Transparent
        Me.pnlwdPatientEducation.Controls.Add(Me.wdPatientEducation)
        Me.pnlwdPatientEducation.Controls.Add(Me.Label5)
        Me.pnlwdPatientEducation.Controls.Add(Me.Label6)
        Me.pnlwdPatientEducation.Controls.Add(Me.Label7)
        Me.pnlwdPatientEducation.Controls.Add(Me.Label8)
        Me.pnlwdPatientEducation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlwdPatientEducation.Location = New System.Drawing.Point(0, 54)
        Me.pnlwdPatientEducation.Name = "pnlwdPatientEducation"
        Me.pnlwdPatientEducation.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlwdPatientEducation.Size = New System.Drawing.Size(1011, 726)
        Me.pnlwdPatientEducation.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 722)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1003, 1)
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
        Me.Label6.Size = New System.Drawing.Size(1, 719)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1007, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 719)
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
        Me.Label8.Size = New System.Drawing.Size(1005, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'frmPatientEducationView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1011, 780)
        Me.Controls.Add(Me.pnlwdPatientEducation)
        Me.Controls.Add(Me.pnlts_ViewButtons)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientEducationView"
        Me.ShowInTaskbar = False
        Me.Text = "Patient Education"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        CType(Me.wdPatientEducation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlts_ViewButtons.ResumeLayout(False)
        Me.pnlts_ViewButtons.PerformLayout()
        Me.pnlwdPatientEducation.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents wdPatientEducation As AxDSOFramer.AxFramerControl
    Friend WithEvents ts_btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlts_ViewButtons As System.Windows.Forms.Panel
    Friend WithEvents pnlwdPatientEducation As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ts_btnExport As System.Windows.Forms.ToolStripButton
End Class
