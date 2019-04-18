<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddDicom_Acknowledgement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddDicom_Acknowledgement))
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.label62 = New System.Windows.Forms.Label
        Me.label59 = New System.Windows.Forms.Label
        Me.lblComment = New System.Windows.Forms.Label
        Me.cmbUser = New System.Windows.Forms.ComboBox
        Me.panel1 = New System.Windows.Forms.Panel
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.lblUser = New System.Windows.Forms.Label
        Me.txtComment = New System.Windows.Forms.TextBox
        Me.pbDocument = New System.Windows.Forms.ProgressBar
        Me.lvwAcknowledge = New System.Windows.Forms.ListView
        Me.tlb_Delete = New System.Windows.Forms.ToolStripButton
        Me.tlb_History = New System.Windows.Forms.ToolStripButton
        Me.tls_MaintainDoc = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlb_Ok = New System.Windows.Forms.ToolStripButton
        Me.tlb_Cancel = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tlb_Review = New System.Windows.Forms.ToolStripButton
        Me.pnlMaintainDoc = New System.Windows.Forms.Panel
        Me.panel1.SuspendLayout()
        Me.tls_MaintainDoc.SuspendLayout()
        Me.pnlMaintainDoc.SuspendLayout()
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label2.Location = New System.Drawing.Point(4, 198)
        Me.label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(437, 1)
        Me.label2.TabIndex = 23
        Me.label2.Text = "label2"
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.label1.Location = New System.Drawing.Point(441, 4)
        Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(1, 195)
        Me.label1.TabIndex = 22
        Me.label1.Text = "label1"
        '
        'label62
        '
        Me.label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label62.Dock = System.Windows.Forms.DockStyle.Top
        Me.label62.Location = New System.Drawing.Point(4, 3)
        Me.label62.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label62.Name = "label62"
        Me.label62.Size = New System.Drawing.Size(438, 1)
        Me.label62.TabIndex = 20
        Me.label62.Text = "label62"
        '
        'label59
        '
        Me.label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label59.Dock = System.Windows.Forms.DockStyle.Left
        Me.label59.Location = New System.Drawing.Point(3, 3)
        Me.label59.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label59.Name = "label59"
        Me.label59.Size = New System.Drawing.Size(1, 196)
        Me.label59.TabIndex = 21
        Me.label59.Text = "label59"
        '
        'lblComment
        '
        Me.lblComment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblComment.AutoEllipsis = True
        Me.lblComment.AutoSize = True
        Me.lblComment.BackColor = System.Drawing.Color.Transparent
        Me.lblComment.Location = New System.Drawing.Point(25, 38)
        Me.lblComment.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(68, 14)
        Me.lblComment.TabIndex = 19
        Me.lblComment.Text = "Comment :"
        '
        'cmbUser
        '
        Me.cmbUser.ForeColor = System.Drawing.Color.Black
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(101, 10)
        Me.cmbUser.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(334, 22)
        Me.cmbUser.TabIndex = 18
        Me.cmbUser.Visible = False
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.Transparent
        Me.panel1.Controls.Add(Me.txtUserName)
        Me.panel1.Controls.Add(Me.label2)
        Me.panel1.Controls.Add(Me.label1)
        Me.panel1.Controls.Add(Me.label62)
        Me.panel1.Controls.Add(Me.label59)
        Me.panel1.Controls.Add(Me.lblComment)
        Me.panel1.Controls.Add(Me.cmbUser)
        Me.panel1.Controls.Add(Me.lblUser)
        Me.panel1.Controls.Add(Me.txtComment)
        Me.panel1.Controls.Add(Me.pbDocument)
        Me.panel1.Controls.Add(Me.lvwAcknowledge)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel1.Location = New System.Drawing.Point(0, 54)
        Me.panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.panel1.Name = "panel1"
        Me.panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.panel1.Size = New System.Drawing.Size(445, 202)
        Me.panel1.TabIndex = 22
        '
        'txtUserName
        '
        Me.txtUserName.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtUserName.Location = New System.Drawing.Point(101, 10)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.ReadOnly = True
        Me.txtUserName.Size = New System.Drawing.Size(334, 22)
        Me.txtUserName.TabIndex = 24
        '
        'lblUser
        '
        Me.lblUser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUser.AutoEllipsis = True
        Me.lblUser.AutoSize = True
        Me.lblUser.Location = New System.Drawing.Point(54, 13)
        Me.lblUser.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(39, 14)
        Me.lblUser.TabIndex = 17
        Me.lblUser.Text = "User :"
        '
        'txtComment
        '
        Me.txtComment.ForeColor = System.Drawing.Color.Black
        Me.txtComment.Location = New System.Drawing.Point(101, 36)
        Me.txtComment.Margin = New System.Windows.Forms.Padding(2)
        Me.txtComment.MaxLength = 255
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(334, 136)
        Me.txtComment.TabIndex = 15
        '
        'pbDocument
        '
        Me.pbDocument.ForeColor = System.Drawing.Color.LawnGreen
        Me.pbDocument.Location = New System.Drawing.Point(10, 175)
        Me.pbDocument.Margin = New System.Windows.Forms.Padding(2)
        Me.pbDocument.Name = "pbDocument"
        Me.pbDocument.Size = New System.Drawing.Size(425, 19)
        Me.pbDocument.TabIndex = 5
        Me.pbDocument.Visible = False
        '
        'lvwAcknowledge
        '
        Me.lvwAcknowledge.ForeColor = System.Drawing.Color.Black
        Me.lvwAcknowledge.FullRowSelect = True
        Me.lvwAcknowledge.HideSelection = False
        Me.lvwAcknowledge.Location = New System.Drawing.Point(9, 10)
        Me.lvwAcknowledge.Margin = New System.Windows.Forms.Padding(2)
        Me.lvwAcknowledge.MultiSelect = False
        Me.lvwAcknowledge.Name = "lvwAcknowledge"
        Me.lvwAcknowledge.Size = New System.Drawing.Size(426, 162)
        Me.lvwAcknowledge.TabIndex = 16
        Me.lvwAcknowledge.UseCompatibleStateImageBehavior = False
        Me.lvwAcknowledge.View = System.Windows.Forms.View.Details
        Me.lvwAcknowledge.Visible = False
        '
        'tlb_Delete
        '
        Me.tlb_Delete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Delete.Image = CType(resources.GetObject("tlb_Delete.Image"), System.Drawing.Image)
        Me.tlb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Delete.Name = "tlb_Delete"
        Me.tlb_Delete.Size = New System.Drawing.Size(50, 50)
        Me.tlb_Delete.Tag = "Delete"
        Me.tlb_Delete.Text = "&Delete"
        Me.tlb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Delete.ToolTipText = "Delete"
        Me.tlb_Delete.Visible = False
        '
        'tlb_History
        '
        Me.tlb_History.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_History.Image = CType(resources.GetObject("tlb_History.Image"), System.Drawing.Image)
        Me.tlb_History.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_History.Name = "tlb_History"
        Me.tlb_History.Size = New System.Drawing.Size(55, 50)
        Me.tlb_History.Tag = "Histroy"
        Me.tlb_History.Text = "&History"
        Me.tlb_History.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_History.ToolTipText = "History"
        Me.tlb_History.Visible = False
        '
        'tls_MaintainDoc
        '
        Me.tls_MaintainDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_MaintainDoc.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_MaintainDoc.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_Ok, Me.tlb_Cancel, Me.toolStripSeparator1, Me.tlb_Review, Me.tlb_History, Me.tlb_Delete})
        Me.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_MaintainDoc.Location = New System.Drawing.Point(0, 0)
        Me.tls_MaintainDoc.Name = "tls_MaintainDoc"
        Me.tls_MaintainDoc.Size = New System.Drawing.Size(445, 53)
        Me.tls_MaintainDoc.TabIndex = 3
        Me.tls_MaintainDoc.Text = "toolStrip1"
        '
        'tlb_Ok
        '
        Me.tlb_Ok.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Ok.Image = CType(resources.GetObject("tlb_Ok.Image"), System.Drawing.Image)
        Me.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Ok.Name = "tlb_Ok"
        Me.tlb_Ok.Size = New System.Drawing.Size(66, 50)
        Me.tlb_Ok.Tag = "OK"
        Me.tlb_Ok.Text = "&Save&&Cls"
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
        Me.tlb_Cancel.ToolTipText = "Close"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.AutoSize = False
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 51)
        Me.toolStripSeparator1.Visible = False
        '
        'tlb_Review
        '
        Me.tlb_Review.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Review.Image = CType(resources.GetObject("tlb_Review.Image"), System.Drawing.Image)
        Me.tlb_Review.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Review.Name = "tlb_Review"
        Me.tlb_Review.Size = New System.Drawing.Size(55, 50)
        Me.tlb_Review.Tag = "Review"
        Me.tlb_Review.Text = "&Review"
        Me.tlb_Review.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Review.ToolTipText = "Review"
        Me.tlb_Review.Visible = False
        '
        'pnlMaintainDoc
        '
        Me.pnlMaintainDoc.Controls.Add(Me.tls_MaintainDoc)
        Me.pnlMaintainDoc.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMaintainDoc.Location = New System.Drawing.Point(0, 0)
        Me.pnlMaintainDoc.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlMaintainDoc.Name = "pnlMaintainDoc"
        Me.pnlMaintainDoc.Size = New System.Drawing.Size(445, 54)
        Me.pnlMaintainDoc.TabIndex = 23
        '
        'frmAddDicom_Acknowledgement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(445, 256)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.pnlMaintainDoc)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddDicom_Acknowledgement"
        Me.Text = "Add Acknowledgement"
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.tls_MaintainDoc.ResumeLayout(False)
        Me.tls_MaintainDoc.PerformLayout()
        Me.pnlMaintainDoc.ResumeLayout(False)
        Me.pnlMaintainDoc.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label62 As System.Windows.Forms.Label
    Private WithEvents label59 As System.Windows.Forms.Label
    Private WithEvents lblComment As System.Windows.Forms.Label
    Private WithEvents cmbUser As System.Windows.Forms.ComboBox
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents lblUser As System.Windows.Forms.Label
    Private WithEvents txtComment As System.Windows.Forms.TextBox
    Private WithEvents pbDocument As System.Windows.Forms.ProgressBar
    Private WithEvents lvwAcknowledge As System.Windows.Forms.ListView
    Private WithEvents tlb_Delete As System.Windows.Forms.ToolStripButton
    Private WithEvents tlb_History As System.Windows.Forms.ToolStripButton
    Private WithEvents tls_MaintainDoc As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlb_Ok As System.Windows.Forms.ToolStripButton
    Private WithEvents tlb_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents tlb_Review As System.Windows.Forms.ToolStripButton
    Private WithEvents pnlMaintainDoc As System.Windows.Forms.Panel
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
End Class
