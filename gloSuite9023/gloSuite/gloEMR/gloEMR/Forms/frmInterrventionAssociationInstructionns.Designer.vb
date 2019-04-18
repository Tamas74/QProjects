<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInterrventionAssociationInstructionns
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInterrventionAssociationInstructionns))
        Me.pnlToostrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tStrpSaveClose = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlTextBox = New System.Windows.Forms.Panel()
        Me.txtInterventionAssociationInstructions = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlToostrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlTextBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToostrip
        '
        Me.pnlToostrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToostrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToostrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToostrip.Name = "pnlToostrip"
        Me.pnlToostrip.Size = New System.Drawing.Size(383, 56)
        Me.pnlToostrip.TabIndex = 18
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tStrpSaveClose, Me.tlbbtnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(383, 53)
        Me.ts_ViewButtons.TabIndex = 1
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'tStrpSaveClose
        '
        Me.tStrpSaveClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpSaveClose.Image = CType(resources.GetObject("tStrpSaveClose.Image"), System.Drawing.Image)
        Me.tStrpSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpSaveClose.Name = "tStrpSaveClose"
        Me.tStrpSaveClose.Size = New System.Drawing.Size(66, 50)
        Me.tStrpSaveClose.Text = "&Save&&Cls"
        Me.tStrpSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpSaveClose.ToolTipText = "Save and Close"
        '
        'tlbbtnClose
        '
        Me.tlbbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtnClose.Image = CType(resources.GetObject("tlbbtnClose.Image"), System.Drawing.Image)
        Me.tlbbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnClose.Name = "tlbbtnClose"
        Me.tlbbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtnClose.Text = "&Close"
        Me.tlbbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlTextBox
        '
        Me.pnlTextBox.Controls.Add(Me.txtInterventionAssociationInstructions)
        Me.pnlTextBox.Controls.Add(Me.Label3)
        Me.pnlTextBox.Controls.Add(Me.Label8)
        Me.pnlTextBox.Controls.Add(Me.Label7)
        Me.pnlTextBox.Controls.Add(Me.Label2)
        Me.pnlTextBox.Controls.Add(Me.Label1)
        Me.pnlTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTextBox.Location = New System.Drawing.Point(0, 56)
        Me.pnlTextBox.Name = "pnlTextBox"
        Me.pnlTextBox.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlTextBox.Size = New System.Drawing.Size(383, 210)
        Me.pnlTextBox.TabIndex = 19
        '
        'txtInterventionAssociationInstructions
        '
        Me.txtInterventionAssociationInstructions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInterventionAssociationInstructions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInterventionAssociationInstructions.Location = New System.Drawing.Point(4, 6)
        Me.txtInterventionAssociationInstructions.MaxLength = 1500
        Me.txtInterventionAssociationInstructions.Multiline = True
        Me.txtInterventionAssociationInstructions.Name = "txtInterventionAssociationInstructions"
        Me.txtInterventionAssociationInstructions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInterventionAssociationInstructions.Size = New System.Drawing.Size(375, 200)
        Me.txtInterventionAssociationInstructions.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(4, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(375, 5)
        Me.Label3.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(379, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 205)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "label3"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 205)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "label4"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(377, 1)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(3, 206)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(377, 1)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "label2"
        '
        'frmInterrventionAssociationInstructionns
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(383, 266)
        Me.Controls.Add(Me.pnlTextBox)
        Me.Controls.Add(Me.pnlToostrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInterrventionAssociationInstructionns"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Instructions"
        Me.pnlToostrip.ResumeLayout(False)
        Me.pnlToostrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlTextBox.ResumeLayout(False)
        Me.pnlTextBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToostrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlTextBox As System.Windows.Forms.Panel
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtInterventionAssociationInstructions As System.Windows.Forms.TextBox
    Public WithEvents tStrpSaveClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label3 As System.Windows.Forms.Label
End Class
