<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmECGHealthCentrix
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
                Try
                    If (IsNothing(gloUC_PatientStrip1) = False) Then
                        gloUC_PatientStrip1.Dispose()
                        gloUC_PatientStrip1 = Nothing
                    End If
                Catch ex As Exception

                End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmECGHealthCentrix))
        Me.pnlControl = New System.Windows.Forms.Panel
        Me.EI = New AxQuintonECG.AxECGIntegrationCtrl
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_HealthCentrix = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnl1 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pnlControl.SuspendLayout()
        CType(Me.EI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_HealthCentrix.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlControl
        '
        Me.pnlControl.Controls.Add(Me.Panel1)
        Me.pnlControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlControl.Location = New System.Drawing.Point(0, 56)
        Me.pnlControl.Name = "pnlControl"
        Me.pnlControl.Size = New System.Drawing.Size(971, 481)
        Me.pnlControl.TabIndex = 0
        '
        'EI
        '
        Me.EI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EI.Enabled = True
        Me.EI.Location = New System.Drawing.Point(3, 3)
        Me.EI.Name = "EI"
        Me.EI.OcxState = CType(resources.GetObject("EI.OcxState"), System.Windows.Forms.AxHost.State)
        Me.EI.Padding = New System.Windows.Forms.Padding(3)
        Me.EI.Size = New System.Drawing.Size(965, 475)
        Me.EI.TabIndex = 0
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_HealthCentrix)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(971, 56)
        Me.pnl_tlsp.TabIndex = 2
        '
        'tlsp_HealthCentrix
        '
        Me.tlsp_HealthCentrix.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_HealthCentrix.BackgroundImage = CType(resources.GetObject("tlsp_HealthCentrix.BackgroundImage"), System.Drawing.Image)
        Me.tlsp_HealthCentrix.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_HealthCentrix.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_HealthCentrix.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_HealthCentrix.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnClose})
        Me.tlsp_HealthCentrix.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_HealthCentrix.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_HealthCentrix.Name = "tlsp_HealthCentrix"
        Me.tlsp_HealthCentrix.Size = New System.Drawing.Size(971, 53)
        Me.tlsp_HealthCentrix.TabIndex = 1
        Me.tlsp_HealthCentrix.TabStop = True
        Me.tlsp_HealthCentrix.Text = "toolStrip1"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnClose.ToolTipText = "Close"
        '
        'pnl1
        '
        Me.pnl1.Location = New System.Drawing.Point(0, 54)
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Size = New System.Drawing.Size(971, 10)
        Me.pnl1.TabIndex = 3
        Me.pnl1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.EI)
        Me.Panel1.Controls.Add(Me.pnl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(971, 481)
        Me.Panel1.TabIndex = 5
        '
        'frmECGHealthCentrix
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(971, 537)
        Me.Controls.Add(Me.pnlControl)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmECGHealthCentrix"
        Me.Text = "Electronic ECG"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlControl.ResumeLayout(False)
        CType(Me.EI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_HealthCentrix.ResumeLayout(False)
        Me.tlsp_HealthCentrix.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlControl As System.Windows.Forms.Panel
    Friend WithEvents EI As AxQuintonECG.AxECGIntegrationCtrl
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_HealthCentrix As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnl1 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
