<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssociateProvider
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssociateProvider))
        Me.cmbProvider = New System.Windows.Forms.ComboBox
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.tls_AssociateProvider = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlb_SaveAndClose = New System.Windows.Forms.ToolStripButton
        Me.tlb_Close = New System.Windows.Forms.ToolStripButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlToolStrip.SuspendLayout()
        Me.tls_AssociateProvider.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbProvider
        '
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(78, 30)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(199, 22)
        Me.cmbProvider.TabIndex = 0
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tls_AssociateProvider)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(292, 54)
        Me.pnlToolStrip.TabIndex = 26
        '
        'tls_AssociateProvider
        '
        Me.tls_AssociateProvider.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_AssociateProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_AssociateProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_AssociateProvider.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_AssociateProvider.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_SaveAndClose, Me.tlb_Close})
        Me.tls_AssociateProvider.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_AssociateProvider.Location = New System.Drawing.Point(0, 0)
        Me.tls_AssociateProvider.Name = "tls_AssociateProvider"
        Me.tls_AssociateProvider.Size = New System.Drawing.Size(292, 53)
        Me.tls_AssociateProvider.TabIndex = 4
        Me.tls_AssociateProvider.Text = "toolStrip1"
        '
        'tlb_SaveAndClose
        '
        Me.tlb_SaveAndClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_SaveAndClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_SaveAndClose.Image = CType(resources.GetObject("tlb_SaveAndClose.Image"), System.Drawing.Image)
        Me.tlb_SaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_SaveAndClose.Name = "tlb_SaveAndClose"
        Me.tlb_SaveAndClose.Size = New System.Drawing.Size(66, 50)
        Me.tlb_SaveAndClose.Tag = "SaveAndClose"
        Me.tlb_SaveAndClose.Text = "&Save&&Cls"
        Me.tlb_SaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_SaveAndClose.ToolTipText = "Save and Close"
        '
        'tlb_Close
        '
        Me.tlb_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_Close.Image = CType(resources.GetObject("tlb_Close.Image"), System.Drawing.Image)
        Me.tlb_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Close.Name = "tlb_Close"
        Me.tlb_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlb_Close.Tag = "Close"
        Me.tlb_Close.Text = "&Close"
        Me.tlb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Close.ToolTipText = "Close"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 14)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Provider :"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cmbProvider)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(292, 82)
        Me.Panel1.TabIndex = 28
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Location = New System.Drawing.Point(288, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 75)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Label5"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 75)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(3, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(286, 1)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Label3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Location = New System.Drawing.Point(3, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(286, 1)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Label2"
        '
        'frmAssociateProvider
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(292, 136)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAssociateProvider"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Associate Provider"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tls_AssociateProvider.ResumeLayout(False)
        Me.tls_AssociateProvider.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Private WithEvents tls_AssociateProvider As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlb_SaveAndClose As System.Windows.Forms.ToolStripButton
    Private WithEvents tlb_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
