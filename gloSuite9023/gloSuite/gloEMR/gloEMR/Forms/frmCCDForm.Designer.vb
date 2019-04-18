<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCDForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCCDForm))
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.pnlPrintMessage = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblFormularyTransactionMessage = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblPreview = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlPrintMessage.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.tblPreview.SuspendLayout()
        Me.SuspendLayout()
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(0, 54)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(23, 22)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(999, 708)
        Me.WebBrowser1.TabIndex = 18
        '
        'pnlPrintMessage
        '
        Me.pnlPrintMessage.BackColor = System.Drawing.Color.White
        Me.pnlPrintMessage.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlPrintMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPrintMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPrintMessage.Controls.Add(Me.Label24)
        Me.pnlPrintMessage.Controls.Add(Me.lblFormularyTransactionMessage)
        Me.pnlPrintMessage.Location = New System.Drawing.Point(225, 315)
        Me.pnlPrintMessage.Name = "pnlPrintMessage"
        Me.pnlPrintMessage.Size = New System.Drawing.Size(228, 69)
        Me.pnlPrintMessage.TabIndex = 62
        Me.pnlPrintMessage.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(20, 7)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(119, 19)
        Me.Label24.TabIndex = 61
        Me.Label24.Text = "Please wait..."
        '
        'lblFormularyTransactionMessage
        '
        Me.lblFormularyTransactionMessage.AutoSize = True
        Me.lblFormularyTransactionMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblFormularyTransactionMessage.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormularyTransactionMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFormularyTransactionMessage.Location = New System.Drawing.Point(21, 33)
        Me.lblFormularyTransactionMessage.Name = "lblFormularyTransactionMessage"
        Me.lblFormularyTransactionMessage.Size = New System.Drawing.Size(184, 16)
        Me.lblFormularyTransactionMessage.TabIndex = 61
        Me.lblFormularyTransactionMessage.Text = "Viewing CCD Information… "
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tblPreview)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(999, 54)
        Me.pnlToolStrip.TabIndex = 63
        '
        'tblPreview
        '
        Me.tblPreview.BackColor = System.Drawing.Color.Transparent
        Me.tblPreview.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblPreview.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblPreview.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblPreview.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblClose})
        Me.tblPreview.Location = New System.Drawing.Point(0, 0)
        Me.tblPreview.Name = "tblPreview"
        Me.tblPreview.Size = New System.Drawing.Size(999, 53)
        Me.tblPreview.TabIndex = 0
        Me.tblPreview.Text = "ToolStrip1"
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'frmCCDForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(999, 762)
        Me.Controls.Add(Me.pnlPrintMessage)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCCDForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Preview CCD"
        Me.pnlPrintMessage.ResumeLayout(False)
        Me.pnlPrintMessage.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblPreview.ResumeLayout(False)
        Me.tblPreview.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents pnlPrintMessage As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblFormularyTransactionMessage As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tblPreview As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
End Class
