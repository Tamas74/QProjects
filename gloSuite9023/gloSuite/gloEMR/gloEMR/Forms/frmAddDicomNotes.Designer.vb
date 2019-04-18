<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddDicomNotes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddDicomNotes))
        Me.tls_MaintainDoc = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlb_Ok = New System.Windows.Forms.ToolStripButton
        Me.tlb_Cancel = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tlb_Notes = New System.Windows.Forms.ToolStripButton
        Me.tlb_Delete = New System.Windows.Forms.ToolStripButton
        Me.panel1 = New System.Windows.Forms.Panel
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.label59 = New System.Windows.Forms.Label
        Me.txtNotes = New System.Windows.Forms.TextBox
        Me.lvwNotes = New System.Windows.Forms.ListView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.tls_MaintainDoc.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'tls_MaintainDoc
        '
        Me.tls_MaintainDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_MaintainDoc.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_MaintainDoc.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_Ok, Me.tlb_Cancel, Me.toolStripSeparator1, Me.tlb_Notes, Me.tlb_Delete})
        Me.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_MaintainDoc.Location = New System.Drawing.Point(0, 0)
        Me.tls_MaintainDoc.Name = "tls_MaintainDoc"
        Me.tls_MaintainDoc.Size = New System.Drawing.Size(398, 53)
        Me.tls_MaintainDoc.TabIndex = 4
        Me.tls_MaintainDoc.Text = "toolStrip1"
        '
        'tlb_Ok
        '
        Me.tlb_Ok.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Ok.Image = CType(resources.GetObject("tlb_Ok.Image"), System.Drawing.Image)
        Me.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Ok.Name = "tlb_Ok"
        Me.tlb_Ok.Size = New System.Drawing.Size(70, 50)
        Me.tlb_Ok.Tag = "OK"
        Me.tlb_Ok.Text = " &Save&&Cls"
        Me.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Ok.ToolTipText = "Save and Close"
        '
        'tlb_Cancel
        '
        Me.tlb_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Cancel.Image = CType(resources.GetObject("tlb_Cancel.Image"), System.Drawing.Image)
        Me.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Cancel.Name = "tlb_Cancel"
        Me.tlb_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.tlb_Cancel.Tag = "Cancel"
        Me.tlb_Cancel.Text = "&Close"
        Me.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.AutoSize = False
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 51)
        Me.toolStripSeparator1.Visible = False
        '
        'tlb_Notes
        '
        Me.tlb_Notes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Notes.Image = CType(resources.GetObject("tlb_Notes.Image"), System.Drawing.Image)
        Me.tlb_Notes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Notes.Name = "tlb_Notes"
        Me.tlb_Notes.Size = New System.Drawing.Size(79, 50)
        Me.tlb_Notes.Tag = "Notes"
        Me.tlb_Notes.Text = " &Add Notes"
        Me.tlb_Notes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Notes.ToolTipText = "Add Notes"
        Me.tlb_Notes.Visible = False
        '
        'tlb_Delete
        '
        Me.tlb_Delete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Delete.Image = CType(resources.GetObject("tlb_Delete.Image"), System.Drawing.Image)
        Me.tlb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Delete.Name = "tlb_Delete"
        Me.tlb_Delete.Size = New System.Drawing.Size(54, 50)
        Me.tlb_Delete.Tag = " Delete"
        Me.tlb_Delete.Text = " &Delete"
        Me.tlb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Delete.ToolTipText = "Delete"
        Me.tlb_Delete.Visible = False
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.Transparent
        Me.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel1.Controls.Add(Me.label3)
        Me.panel1.Controls.Add(Me.label2)
        Me.panel1.Controls.Add(Me.label1)
        Me.panel1.Controls.Add(Me.label59)
        Me.panel1.Controls.Add(Me.txtNotes)
        Me.panel1.Controls.Add(Me.lvwNotes)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel1.Location = New System.Drawing.Point(0, 95)
        Me.panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.panel1.Name = "panel1"
        Me.panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.panel1.Size = New System.Drawing.Size(398, 156)
        Me.panel1.TabIndex = 20
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label3.Location = New System.Drawing.Point(4, 152)
        Me.label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(390, 1)
        Me.label3.TabIndex = 25
        Me.label3.Text = "label3"
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.label2.Location = New System.Drawing.Point(4, 0)
        Me.label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(390, 1)
        Me.label2.TabIndex = 24
        Me.label2.Text = "label2"
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.label1.Location = New System.Drawing.Point(394, 0)
        Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(1, 153)
        Me.label1.TabIndex = 23
        Me.label1.Text = "label1"
        '
        'label59
        '
        Me.label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label59.Dock = System.Windows.Forms.DockStyle.Left
        Me.label59.Location = New System.Drawing.Point(3, 0)
        Me.label59.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label59.Name = "label59"
        Me.label59.Size = New System.Drawing.Size(1, 153)
        Me.label59.TabIndex = 22
        Me.label59.Text = "label59"
        '
        'txtNotes
        '
        Me.txtNotes.ForeColor = System.Drawing.Color.Black
        Me.txtNotes.Location = New System.Drawing.Point(8, 4)
        Me.txtNotes.Margin = New System.Windows.Forms.Padding(2)
        Me.txtNotes.MaxLength = 255
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(379, 143)
        Me.txtNotes.TabIndex = 15
        '
        'lvwNotes
        '
        Me.lvwNotes.FullRowSelect = True
        Me.lvwNotes.HideSelection = False
        Me.lvwNotes.Location = New System.Drawing.Point(10, 5)
        Me.lvwNotes.Margin = New System.Windows.Forms.Padding(2)
        Me.lvwNotes.MultiSelect = False
        Me.lvwNotes.Name = "lvwNotes"
        Me.lvwNotes.Size = New System.Drawing.Size(374, 142)
        Me.lvwNotes.TabIndex = 16
        Me.lvwNotes.UseCompatibleStateImageBehavior = False
        Me.lvwNotes.View = System.Windows.Forms.View.Details
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.tls_MaintainDoc)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(398, 54)
        Me.Panel2.TabIndex = 26
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.txtUserName)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 54)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel3.Size = New System.Drawing.Size(398, 41)
        Me.Panel3.TabIndex = 27
        '
        'txtUserName
        '
        Me.txtUserName.BackColor = System.Drawing.Color.White
        Me.txtUserName.ForeColor = System.Drawing.Color.Black
        Me.txtUserName.Location = New System.Drawing.Point(86, 9)
        Me.txtUserName.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUserName.MaxLength = 255
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.ReadOnly = True
        Me.txtUserName.Size = New System.Drawing.Size(301, 22)
        Me.txtUserName.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Location = New System.Drawing.Point(4, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 33)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "User Name :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(4, 37)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(390, 1)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(4, 3)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(390, 1)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Location = New System.Drawing.Point(394, 3)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 35)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Label6"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(3, 3)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 35)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Label7"
        '
        'frmAddDicomNotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(398, 251)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddDicomNotes"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Notes"
        Me.tls_MaintainDoc.ResumeLayout(False)
        Me.tls_MaintainDoc.PerformLayout()
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tls_MaintainDoc As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlb_Ok As System.Windows.Forms.ToolStripButton
    Private WithEvents tlb_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents tlb_Notes As System.Windows.Forms.ToolStripButton
    Private WithEvents tlb_Delete As System.Windows.Forms.ToolStripButton
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label59 As System.Windows.Forms.Label
    Private WithEvents txtNotes As System.Windows.Forms.TextBox
    Private WithEvents lvwNotes As System.Windows.Forms.ListView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents txtUserName As System.Windows.Forms.TextBox
End Class
