<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCD_ExtractReconcillation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCCD_ExtractReconcillation))
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblReconcilation = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblSaveClose = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.txtReconcileListName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.pnlToolStrip.SuspendLayout()
        Me.tblReconcilation.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tblReconcilation)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(500, 54)
        Me.pnlToolStrip.TabIndex = 5
        '
        'tblReconcilation
        '
        Me.tblReconcilation.BackColor = System.Drawing.Color.Transparent
        Me.tblReconcilation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblReconcilation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblReconcilation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblReconcilation.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblReconcilation.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblReconcilation.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblSaveClose, Me.tblClose})
        Me.tblReconcilation.Location = New System.Drawing.Point(0, 0)
        Me.tblReconcilation.Name = "tblReconcilation"
        Me.tblReconcilation.Size = New System.Drawing.Size(500, 53)
        Me.tblReconcilation.TabIndex = 0
        Me.tblReconcilation.Text = "tls_ExtractReconcilation"
        '
        'tblSaveClose
        '
        Me.tblSaveClose.Image = CType(resources.GetObject("tblSaveClose.Image"), System.Drawing.Image)
        Me.tblSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSaveClose.Name = "tblSaveClose"
        Me.tblSaveClose.Size = New System.Drawing.Size(66, 50)
        Me.tblSaveClose.Tag = "Save&Close"
        Me.tblSaveClose.Text = "&Save&&Cls"
        Me.tblSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSaveClose.ToolTipText = "Save and Close"
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Tag = "Close"
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'txtReconcileListName
        '
        Me.txtReconcileListName.Location = New System.Drawing.Point(135, 31)
        Me.txtReconcileListName.MaxLength = 200
        Me.txtReconcileListName.Name = "txtReconcileListName"
        Me.txtReconcileListName.Size = New System.Drawing.Size(354, 22)
        Me.txtReconcileListName.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 14)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Reconcile List Name : "
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.txtReconcileListName)
        Me.Panel4.Controls.Add(Me.Label19)
        Me.Panel4.Controls.Add(Me.Label20)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 54)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel4.Size = New System.Drawing.Size(500, 81)
        Me.Panel4.TabIndex = 64
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Location = New System.Drawing.Point(496, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 73)
        Me.Label2.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 73)
        Me.Label6.TabIndex = 2
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Location = New System.Drawing.Point(3, 77)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(494, 1)
        Me.Label19.TabIndex = 4
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(3, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(494, 1)
        Me.Label20.TabIndex = 3
        '
        'frmCCD_ExtractReconcillation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(500, 135)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmCCD_ExtractReconcillation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Extract Reconciliation List"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblReconcilation.ResumeLayout(False)
        Me.tblReconcilation.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tblReconcilation As gloGlobal.gloToolStripIgnoreFocus
    Public WithEvents tblSaveClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtReconcileListName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
End Class
