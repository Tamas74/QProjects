<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMedHxDownload
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
                    If (IsNothing(saveFileDialog1) = False) Then
                        saveFileDialog1.Dispose()
                        saveFileDialog1 = Nothing
                    End If
                Catch ex As Exception

                End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMedHxDownload))
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblStrip_32 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_Reconcile = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Accept = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Preview = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.wbMedHx = New System.Windows.Forms.WebBrowser()
        Me.pnlWebbrowser = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlPleasewait = New System.Windows.Forms.Panel()
        Me.label45 = New System.Windows.Forms.Label()
        Me.label44 = New System.Windows.Forms.Label()
        Me.label43 = New System.Windows.Forms.Label()
        Me.label42 = New System.Windows.Forms.Label()
        Me.label41 = New System.Windows.Forms.Label()
        Me.label25 = New System.Windows.Forms.Label()
        Me.pnlCDuedate = New System.Windows.Forms.Panel()
        Me.saveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.pnlMedHx = New System.Windows.Forms.Panel()
        Me.pnlToolStrip.SuspendLayout()
        Me.tblStrip_32.SuspendLayout()
        Me.pnlWebbrowser.SuspendLayout()
        Me.pnlPleasewait.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.AutoSize = True
        Me.pnlToolStrip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlToolStrip.Controls.Add(Me.tblStrip_32)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1184, 53)
        Me.pnlToolStrip.TabIndex = 40
        '
        'tblStrip_32
        '
        Me.tblStrip_32.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip_32.BackgroundImage = CType(resources.GetObject("tblStrip_32.BackgroundImage"), System.Drawing.Image)
        Me.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip_32.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Reconcile, Me.tblbtn_Accept, Me.tblbtn_Preview, Me.tblbtn_Close})
        Me.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip_32.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip_32.Name = "tblStrip_32"
        Me.tblStrip_32.Size = New System.Drawing.Size(1184, 53)
        Me.tblStrip_32.TabIndex = 6
        Me.tblStrip_32.Text = "ToolStrip1"
        '
        'tblbtn_Reconcile
        '
        Me.tblbtn_Reconcile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Reconcile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Reconcile.Image = CType(resources.GetObject("tblbtn_Reconcile.Image"), System.Drawing.Image)
        Me.tblbtn_Reconcile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Reconcile.Name = "tblbtn_Reconcile"
        Me.tblbtn_Reconcile.Size = New System.Drawing.Size(68, 50)
        Me.tblbtn_Reconcile.Tag = "Reconcile"
        Me.tblbtn_Reconcile.Text = "&Reconcile"
        Me.tblbtn_Reconcile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Accept
        '
        Me.tblbtn_Accept.Enabled = False
        Me.tblbtn_Accept.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Accept.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Accept.Image = CType(resources.GetObject("tblbtn_Accept.Image"), System.Drawing.Image)
        Me.tblbtn_Accept.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Accept.Name = "tblbtn_Accept"
        Me.tblbtn_Accept.Size = New System.Drawing.Size(53, 50)
        Me.tblbtn_Accept.Tag = "Accept"
        Me.tblbtn_Accept.Text = "&Accept"
        Me.tblbtn_Accept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Accept.Visible = False
        '
        'tblbtn_Preview
        '
        Me.tblbtn_Preview.Enabled = False
        Me.tblbtn_Preview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Preview.Image = CType(resources.GetObject("tblbtn_Preview.Image"), System.Drawing.Image)
        Me.tblbtn_Preview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Preview.Name = "tblbtn_Preview"
        Me.tblbtn_Preview.Size = New System.Drawing.Size(59, 50)
        Me.tblbtn_Preview.Tag = "Preview"
        Me.tblbtn_Preview.Text = "&Preview"
        Me.tblbtn_Preview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Preview.Visible = False
        '
        'tblbtn_Close
        '
        Me.tblbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Close.Image = CType(resources.GetObject("tblbtn_Close.Image"), System.Drawing.Image)
        Me.tblbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close.Name = "tblbtn_Close"
        Me.tblbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close.Tag = "Close"
        Me.tblbtn_Close.Text = "&Close"
        Me.tblbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'wbMedHx
        '
        Me.wbMedHx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbMedHx.IsWebBrowserContextMenuEnabled = False
        Me.wbMedHx.Location = New System.Drawing.Point(3, 3)
        Me.wbMedHx.MinimumSize = New System.Drawing.Size(23, 22)
        Me.wbMedHx.Name = "wbMedHx"
        Me.wbMedHx.ScriptErrorsSuppressed = True
        Me.wbMedHx.Size = New System.Drawing.Size(1178, 773)
        Me.wbMedHx.TabIndex = 0
        '
        'pnlWebbrowser
        '
        Me.pnlWebbrowser.Controls.Add(Me.Label4)
        Me.pnlWebbrowser.Controls.Add(Me.Label3)
        Me.pnlWebbrowser.Controls.Add(Me.Label2)
        Me.pnlWebbrowser.Controls.Add(Me.Label1)
        Me.pnlWebbrowser.Controls.Add(Me.wbMedHx)
        Me.pnlWebbrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWebbrowser.Location = New System.Drawing.Point(0, 53)
        Me.pnlWebbrowser.Name = "pnlWebbrowser"
        Me.pnlWebbrowser.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlWebbrowser.Size = New System.Drawing.Size(1184, 779)
        Me.pnlWebbrowser.TabIndex = 41
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(1180, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 771)
        Me.Label4.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 771)
        Me.Label3.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 775)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1178, 1)
        Me.Label2.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1178, 1)
        Me.Label1.TabIndex = 1
        '
        'pnlPleasewait
        '
        Me.pnlPleasewait.Controls.Add(Me.label45)
        Me.pnlPleasewait.Controls.Add(Me.label44)
        Me.pnlPleasewait.Controls.Add(Me.label43)
        Me.pnlPleasewait.Controls.Add(Me.label42)
        Me.pnlPleasewait.Controls.Add(Me.label41)
        Me.pnlPleasewait.Controls.Add(Me.label25)
        Me.pnlPleasewait.Controls.Add(Me.pnlCDuedate)
        Me.pnlPleasewait.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPleasewait.Location = New System.Drawing.Point(0, 53)
        Me.pnlPleasewait.Name = "pnlPleasewait"
        Me.pnlPleasewait.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlPleasewait.Size = New System.Drawing.Size(1184, 779)
        Me.pnlPleasewait.TabIndex = 35
        Me.pnlPleasewait.Visible = False
        '
        'label45
        '
        Me.label45.BackColor = System.Drawing.Color.White
        Me.label45.Dock = System.Windows.Forms.DockStyle.Fill
        Me.label45.Font = New System.Drawing.Font("Baskerville Old Face", 48.0!)
        Me.label45.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label45.Location = New System.Drawing.Point(4, 4)
        Me.label45.Name = "label45"
        Me.label45.Size = New System.Drawing.Size(1176, 771)
        Me.label45.TabIndex = 238
        Me.label45.Text = "Please wait..."
        Me.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label44
        '
        Me.label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label44.Dock = System.Windows.Forms.DockStyle.Left
        Me.label44.Location = New System.Drawing.Point(3, 4)
        Me.label44.Name = "label44"
        Me.label44.Size = New System.Drawing.Size(1, 771)
        Me.label44.TabIndex = 36
        Me.label44.Text = "label44"
        '
        'label43
        '
        Me.label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.label43.Location = New System.Drawing.Point(1180, 4)
        Me.label43.Name = "label43"
        Me.label43.Size = New System.Drawing.Size(1, 771)
        Me.label43.TabIndex = 35
        Me.label43.Text = "label43"
        '
        'label42
        '
        Me.label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label42.Dock = System.Windows.Forms.DockStyle.Top
        Me.label42.Location = New System.Drawing.Point(3, 3)
        Me.label42.Name = "label42"
        Me.label42.Size = New System.Drawing.Size(1178, 1)
        Me.label42.TabIndex = 34
        Me.label42.Text = "label42"
        '
        'label41
        '
        Me.label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label41.Location = New System.Drawing.Point(3, 775)
        Me.label41.Name = "label41"
        Me.label41.Size = New System.Drawing.Size(1178, 1)
        Me.label41.TabIndex = 33
        Me.label41.Text = "label41"
        '
        'label25
        '
        Me.label25.BackColor = System.Drawing.Color.Transparent
        Me.label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label25.Location = New System.Drawing.Point(52, 90)
        Me.label25.Name = "label25"
        Me.label25.Size = New System.Drawing.Size(108, 14)
        Me.label25.TabIndex = 234
        Me.label25.Text = "Transaction Date :"
        Me.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.label25.Visible = False
        '
        'pnlCDuedate
        '
        Me.pnlCDuedate.Location = New System.Drawing.Point(615, 57)
        Me.pnlCDuedate.Name = "pnlCDuedate"
        Me.pnlCDuedate.Size = New System.Drawing.Size(418, 51)
        Me.pnlCDuedate.TabIndex = 236
        '
        'pnlMedHx
        '
        Me.pnlMedHx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMedHx.Location = New System.Drawing.Point(0, 53)
        Me.pnlMedHx.Name = "pnlMedHx"
        Me.pnlMedHx.Size = New System.Drawing.Size(1184, 779)
        Me.pnlMedHx.TabIndex = 239
        '
        'frmMedHxDownload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1184, 832)
        Me.Controls.Add(Me.pnlMedHx)
        Me.Controls.Add(Me.pnlPleasewait)
        Me.Controls.Add(Me.pnlWebbrowser)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "frmMedHxDownload"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Medication History 10.6"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblStrip_32.ResumeLayout(False)
        Me.tblStrip_32.PerformLayout()
        Me.pnlWebbrowser.ResumeLayout(False)
        Me.pnlPleasewait.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tblStrip_32 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Reconcile As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Private WithEvents wbMedHx As System.Windows.Forms.WebBrowser
    Friend WithEvents pnlWebbrowser As System.Windows.Forms.Panel
    Private WithEvents saveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents pnlPleasewait As System.Windows.Forms.Panel
    Friend WithEvents label45 As System.Windows.Forms.Label
    Private WithEvents label44 As System.Windows.Forms.Label
    Private WithEvents label43 As System.Windows.Forms.Label
    Private WithEvents label42 As System.Windows.Forms.Label
    Private WithEvents label41 As System.Windows.Forms.Label
    Private WithEvents label25 As System.Windows.Forms.Label
    Private WithEvents pnlCDuedate As System.Windows.Forms.Panel
    Friend WithEvents pnlMedHx As System.Windows.Forms.Panel
    Friend WithEvents tblbtn_Accept As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Preview As System.Windows.Forms.ToolStripButton
End Class
