<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddProblemListComment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddProblemListComment))
        Me.txtComment = New System.Windows.Forms.TextBox
        Me.lblComments = New System.Windows.Forms.Label
        Me.tls_comment = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlb_OK = New System.Windows.Forms.ToolStripButton
        Me.tlb_Cancle = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.tls_comment.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(114, 16)
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(267, 22)
        Me.txtComment.TabIndex = 0
        '
        'lblComments
        '
        Me.lblComments.AutoSize = True
        Me.lblComments.Location = New System.Drawing.Point(17, 18)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(94, 14)
        Me.lblComments.TabIndex = 1
        Me.lblComments.Text = "Add Comment :"
        '
        'tls_comment
        '
        Me.tls_comment.BackColor = System.Drawing.Color.Transparent
        Me.tls_comment.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_comment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_comment.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_comment.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_comment.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_OK, Me.tlb_Cancle})
        Me.tls_comment.Location = New System.Drawing.Point(0, 0)
        Me.tls_comment.Name = "tls_comment"
        Me.tls_comment.Size = New System.Drawing.Size(400, 53)
        Me.tls_comment.TabIndex = 2
        Me.tls_comment.Text = "ToolStrip1"
        '
        'tlb_OK
        '
        Me.tlb_OK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_OK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_OK.Image = CType(resources.GetObject("tlb_OK.Image"), System.Drawing.Image)
        Me.tlb_OK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_OK.Name = "tlb_OK"
        Me.tlb_OK.Size = New System.Drawing.Size(66, 50)
        Me.tlb_OK.Tag = "OK"
        Me.tlb_OK.Text = "&Save&&Cls"
        Me.tlb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_OK.ToolTipText = "Save and Close"
        '
        'tlb_Cancle
        '
        Me.tlb_Cancle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Cancle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_Cancle.Image = CType(resources.GetObject("tlb_Cancle.Image"), System.Drawing.Image)
        Me.tlb_Cancle.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Cancle.Name = "tlb_Cancle"
        Me.tlb_Cancle.Size = New System.Drawing.Size(43, 50)
        Me.tlb_Cancle.Tag = "Cancel"
        Me.tlb_Cancle.Text = "&Close"
        Me.tlb_Cancle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Cancle.ToolTipText = "Close"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Controls.Add(Me.txtComment)
        Me.Panel1.Controls.Add(Me.lblComments)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(400, 88)
        Me.Panel1.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(392, 1)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(392, 1)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 82)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "label1"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(396, 3)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 82)
        Me.Label29.TabIndex = 9
        Me.Label29.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.tls_comment)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(400, 54)
        Me.Panel2.TabIndex = 4
        '
        'frmAddProblemListComment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(400, 142)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddProblemListComment"
        Me.Text = "Add Comment"
        Me.tls_comment.ResumeLayout(False)
        Me.tls_comment.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents lblComments As System.Windows.Forms.Label
    Friend WithEvents tls_comment As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlb_OK As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlb_Cancle As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
End Class
