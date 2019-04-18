<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIm_Validation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIm_Validation))
        Me.tblStrip_32 = New gloGlobal.gloToolStripIgnoreFocus
        Me.tblbtn_Ok = New System.Windows.Forms.ToolStripButton
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton
        Me.rchtxtImValidation = New System.Windows.Forms.RichTextBox
        Me.pnlImValidation = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnlLable = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblImMessage = New System.Windows.Forms.Label
        Me.pnlToolBar = New System.Windows.Forms.Panel
        Me.tblStrip_32.SuspendLayout()
        Me.pnlImValidation.SuspendLayout()
        Me.pnlLable.SuspendLayout()
        Me.pnlToolBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'tblStrip_32
        '
        Me.tblStrip_32.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip_32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip_32.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Ok, Me.tblbtn_Close})
        Me.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip_32.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip_32.Name = "tblStrip_32"
        Me.tblStrip_32.Size = New System.Drawing.Size(598, 53)
        Me.tblStrip_32.TabIndex = 1
        Me.tblStrip_32.Text = "ToolStrip1"
        '
        'tblbtn_Ok
        '
        Me.tblbtn_Ok.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Ok.Image = CType(resources.GetObject("tblbtn_Ok.Image"), System.Drawing.Image)
        Me.tblbtn_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Ok.Name = "tblbtn_Ok"
        Me.tblbtn_Ok.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Ok.Text = "&Save&&Cls"
        Me.tblbtn_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Ok.ToolTipText = "Save and Close"
        '
        'tblbtn_Close
        '
        Me.tblbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close.Image = CType(resources.GetObject("tblbtn_Close.Image"), System.Drawing.Image)
        Me.tblbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close.Name = "tblbtn_Close"
        Me.tblbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close.Text = "&Close"
        Me.tblbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'rchtxtImValidation
        '
        Me.rchtxtImValidation.BackColor = System.Drawing.Color.White
        Me.rchtxtImValidation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rchtxtImValidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rchtxtImValidation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.rchtxtImValidation.Location = New System.Drawing.Point(3, 0)
        Me.rchtxtImValidation.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.rchtxtImValidation.Name = "rchtxtImValidation"
        Me.rchtxtImValidation.ReadOnly = True
        Me.rchtxtImValidation.Size = New System.Drawing.Size(592, 431)
        Me.rchtxtImValidation.TabIndex = 2
        Me.rchtxtImValidation.Text = ""
        '
        'pnlImValidation
        '
        Me.pnlImValidation.AutoScroll = True
        Me.pnlImValidation.Controls.Add(Me.Label1)
        Me.pnlImValidation.Controls.Add(Me.Label2)
        Me.pnlImValidation.Controls.Add(Me.Label3)
        Me.pnlImValidation.Controls.Add(Me.Label4)
        Me.pnlImValidation.Controls.Add(Me.rchtxtImValidation)
        Me.pnlImValidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImValidation.Location = New System.Drawing.Point(0, 110)
        Me.pnlImValidation.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlImValidation.Name = "pnlImValidation"
        Me.pnlImValidation.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlImValidation.Size = New System.Drawing.Size(598, 434)
        Me.pnlImValidation.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 430)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(590, 1)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 430)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(594, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 430)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(592, 1)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "label1"
        '
        'pnlLable
        '
        Me.pnlLable.Controls.Add(Me.Label5)
        Me.pnlLable.Controls.Add(Me.Label6)
        Me.pnlLable.Controls.Add(Me.Label7)
        Me.pnlLable.Controls.Add(Me.Label8)
        Me.pnlLable.Controls.Add(Me.lblImMessage)
        Me.pnlLable.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLable.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlLable.Location = New System.Drawing.Point(0, 54)
        Me.pnlLable.Name = "pnlLable"
        Me.pnlLable.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlLable.Size = New System.Drawing.Size(598, 56)
        Me.pnlLable.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(590, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 49)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(594, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 49)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(592, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'lblImMessage
        '
        Me.lblImMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblImMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblImMessage.Name = "lblImMessage"
        Me.lblImMessage.Size = New System.Drawing.Size(592, 50)
        Me.lblImMessage.TabIndex = 0
        '
        'pnlToolBar
        '
        Me.pnlToolBar.Controls.Add(Me.tblStrip_32)
        Me.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(598, 54)
        Me.pnlToolBar.TabIndex = 4
        '
        'frmIm_Validation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(598, 544)
        Me.Controls.Add(Me.pnlImValidation)
        Me.Controls.Add(Me.pnlLable)
        Me.Controls.Add(Me.pnlToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIm_Validation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MCIR Validation"
        Me.tblStrip_32.ResumeLayout(False)
        Me.tblStrip_32.PerformLayout()
        Me.pnlImValidation.ResumeLayout(False)
        Me.pnlLable.ResumeLayout(False)
        Me.pnlToolBar.ResumeLayout(False)
        Me.pnlToolBar.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tblStrip_32 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Ok As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents rchtxtImValidation As System.Windows.Forms.RichTextBox
    Friend WithEvents pnlImValidation As System.Windows.Forms.Panel
    Friend WithEvents pnlLable As System.Windows.Forms.Panel
    Friend WithEvents lblImMessage As System.Windows.Forms.Label
    Friend WithEvents pnlToolBar As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
End Class
