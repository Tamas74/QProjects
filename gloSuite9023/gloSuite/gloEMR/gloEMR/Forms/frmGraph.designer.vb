<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Graph
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
                    If (IsNothing(_PatientStrip) = False) Then
                        _PatientStrip.Dispose()
                        _PatientStrip = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Graph))
        Me.tblStrip_32 = New gloGlobal.gloToolStripIgnoreFocus
        Me.btngraph = New System.Windows.Forms.ToolStripButton
        Me.btntimeline = New System.Windows.Forms.ToolStripButton
        Me.btncuitimeline = New System.Windows.Forms.ToolStripButton
        Me.btnmedication = New System.Windows.Forms.ToolStripButton
        Me.tblbtn_Reset_32 = New System.Windows.Forms.ToolStripButton
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton
        Me.PnlGraph = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label68 = New System.Windows.Forms.Label
        Me.pnlToolstrip = New System.Windows.Forms.Panel
        Me.tblStrip_32.SuspendLayout()
        Me.PnlGraph.SuspendLayout()
        Me.pnlToolstrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'tblStrip_32
        '
        Me.tblStrip_32.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip_32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip_32.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btngraph, Me.btntimeline, Me.btncuitimeline, Me.btnmedication, Me.tblbtn_Reset_32, Me.tblbtn_Close_32})
        Me.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip_32.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip_32.Name = "tblStrip_32"
        Me.tblStrip_32.Size = New System.Drawing.Size(794, 53)
        Me.tblStrip_32.TabIndex = 2
        Me.tblStrip_32.Text = "ToolStrip1"
        '
        'btngraph
        '
        Me.btngraph.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngraph.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btngraph.Image = CType(resources.GetObject("btngraph.Image"), System.Drawing.Image)
        Me.btngraph.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btngraph.Name = "btngraph"
        Me.btngraph.Size = New System.Drawing.Size(96, 50)
        Me.btngraph.Tag = "PatientGraph"
        Me.btngraph.Text = "&Patient Graph"
        Me.btngraph.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btngraph.Visible = False
        '
        'btntimeline
        '
        Me.btntimeline.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntimeline.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btntimeline.Image = CType(resources.GetObject("btntimeline.Image"), System.Drawing.Image)
        Me.btntimeline.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btntimeline.Name = "btntimeline"
        Me.btntimeline.Size = New System.Drawing.Size(126, 50)
        Me.btntimeline.Tag = "ListTimeline"
        Me.btntimeline.Text = "&List Timeline Graph"
        Me.btntimeline.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btntimeline.Visible = False
        '
        'btncuitimeline
        '
        Me.btncuitimeline.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncuitimeline.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btncuitimeline.Image = CType(resources.GetObject("btncuitimeline.Image"), System.Drawing.Image)
        Me.btncuitimeline.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btncuitimeline.Name = "btncuitimeline"
        Me.btncuitimeline.Size = New System.Drawing.Size(125, 50)
        Me.btncuitimeline.Tag = "Timeline"
        Me.btncuitimeline.Text = "&CUI Timeline Graph"
        Me.btncuitimeline.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btncuitimeline.ToolTipText = "CUI  Timeline Graph "
        Me.btncuitimeline.Visible = False
        '
        'btnmedication
        '
        Me.btnmedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnmedication.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnmedication.Image = CType(resources.GetObject("btnmedication.Image"), System.Drawing.Image)
        Me.btnmedication.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnmedication.Name = "btnmedication"
        Me.btnmedication.Size = New System.Drawing.Size(104, 50)
        Me.btnmedication.Tag = "MedicationList"
        Me.btnmedication.Text = "&Medication List"
        Me.btnmedication.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnmedication.Visible = False
        '
        'tblbtn_Reset_32
        '
        Me.tblbtn_Reset_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Reset_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Reset_32.Image = CType(resources.GetObject("tblbtn_Reset_32.Image"), System.Drawing.Image)
        Me.tblbtn_Reset_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Reset_32.Name = "tblbtn_Reset_32"
        Me.tblbtn_Reset_32.Size = New System.Drawing.Size(46, 50)
        Me.tblbtn_Reset_32.Tag = "Reset"
        Me.tblbtn_Reset_32.Text = "&Reset"
        Me.tblbtn_Reset_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Reset_32.Visible = False
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Tag = "Close"
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'PnlGraph
        '
        Me.PnlGraph.AutoScroll = True
        Me.PnlGraph.AutoSize = True
        Me.PnlGraph.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.PnlGraph.Controls.Add(Me.Label3)
        Me.PnlGraph.Controls.Add(Me.Label2)
        Me.PnlGraph.Controls.Add(Me.Label1)
        Me.PnlGraph.Controls.Add(Me.Label68)
        Me.PnlGraph.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlGraph.Location = New System.Drawing.Point(0, 56)
        Me.PnlGraph.Name = "PnlGraph"
        Me.PnlGraph.Padding = New System.Windows.Forms.Padding(3)
        Me.PnlGraph.Size = New System.Drawing.Size(794, 552)
        Me.PnlGraph.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 548)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(786, 1)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "label4"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(786, 1)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(790, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 546)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "label4"
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(3, 3)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(1, 546)
        Me.Label68.TabIndex = 12
        Me.Label68.Text = "label4"
        '
        'pnlToolstrip
        '
        Me.pnlToolstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolstrip.Controls.Add(Me.tblStrip_32)
        Me.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolstrip.Name = "pnlToolstrip"
        Me.pnlToolstrip.Size = New System.Drawing.Size(794, 56)
        Me.pnlToolstrip.TabIndex = 16
        '
        'frm_Graph
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(794, 608)
        Me.Controls.Add(Me.PnlGraph)
        Me.Controls.Add(Me.pnlToolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_Graph"
        Me.ShowInTaskbar = False
        Me.Text = "Timeline"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tblStrip_32.ResumeLayout(False)
        Me.tblStrip_32.PerformLayout()
        Me.PnlGraph.ResumeLayout(False)
        Me.pnlToolstrip.ResumeLayout(False)
        Me.pnlToolstrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tblStrip_32 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents PnlGraph As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents pnlToolstrip As System.Windows.Forms.Panel
    Friend WithEvents btngraph As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Reset_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnmedication As System.Windows.Forms.ToolStripButton
    Friend WithEvents btntimeline As System.Windows.Forms.ToolStripButton
    Friend WithEvents btncuitimeline As System.Windows.Forms.ToolStripButton
End Class
