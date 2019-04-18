<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewEPA
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewEPA))
        Me.pnlToolstrip = New System.Windows.Forms.Panel()
        Me.tls_Main = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsbtn_WorkList = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtn_Processes = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnl = New System.Windows.Forms.Panel()
        Me.cmbProviders = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblRight = New System.Windows.Forms.Label()
        Me.lblLeft = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlAccelerator = New System.Windows.Forms.Panel()
        Me.lblBottom = New System.Windows.Forms.Label()
        Me.lblGridBottom = New System.Windows.Forms.Label()
        Me.lblGridLeft = New System.Windows.Forms.Label()
        Me.lblGridRight = New System.Windows.Forms.Label()
        Me.pnlToolstrip.SuspendLayout()
        Me.tls_Main.SuspendLayout()
        Me.pnl.SuspendLayout()
        Me.pnlAccelerator.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolstrip
        '
        Me.pnlToolstrip.AutoSize = True
        Me.pnlToolstrip.Controls.Add(Me.tls_Main)
        Me.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolstrip.Name = "pnlToolstrip"
        Me.pnlToolstrip.Size = New System.Drawing.Size(974, 56)
        Me.pnlToolstrip.TabIndex = 29
        '
        'tls_Main
        '
        Me.tls_Main.AutoSize = False
        Me.tls_Main.BackgroundImage = CType(resources.GetObject("tls_Main.BackgroundImage"), System.Drawing.Image)
        Me.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_Main.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_Main.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtn_WorkList, Me.tlsbtn_Processes, Me.tlsbtn_Close})
        Me.tls_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.tls_Main.Location = New System.Drawing.Point(0, 0)
        Me.tls_Main.Name = "tls_Main"
        Me.tls_Main.Size = New System.Drawing.Size(974, 56)
        Me.tls_Main.TabIndex = 3
        Me.tls_Main.Text = "ToolStrip1"
        '
        'tlsbtn_WorkList
        '
        Me.tlsbtn_WorkList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtn_WorkList.Image = CType(resources.GetObject("tlsbtn_WorkList.Image"), System.Drawing.Image)
        Me.tlsbtn_WorkList.Name = "tlsbtn_WorkList"
        Me.tlsbtn_WorkList.Size = New System.Drawing.Size(65, 53)
        Me.tlsbtn_WorkList.Tag = "WorkList"
        Me.tlsbtn_WorkList.Text = "&WorkList"
        Me.tlsbtn_WorkList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtn_WorkList.ToolTipText = "WorkList"
        '
        'tlsbtn_Processes
        '
        Me.tlsbtn_Processes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtn_Processes.Image = CType(resources.GetObject("tlsbtn_Processes.Image"), System.Drawing.Image)
        Me.tlsbtn_Processes.Name = "tlsbtn_Processes"
        Me.tlsbtn_Processes.Size = New System.Drawing.Size(70, 53)
        Me.tlsbtn_Processes.Tag = "Processes"
        Me.tlsbtn_Processes.Text = "Processes"
        Me.tlsbtn_Processes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtn_Processes.ToolTipText = "Processes"
        '
        'tlsbtn_Close
        '
        Me.tlsbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtn_Close.Image = CType(resources.GetObject("tlsbtn_Close.Image"), System.Drawing.Image)
        Me.tlsbtn_Close.Name = "tlsbtn_Close"
        Me.tlsbtn_Close.Size = New System.Drawing.Size(43, 53)
        Me.tlsbtn_Close.Tag = "Close"
        Me.tlsbtn_Close.Text = "&Close"
        Me.tlsbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtn_Close.ToolTipText = "Close"
        '
        'pnl
        '
        Me.pnl.BackColor = System.Drawing.Color.Transparent
        Me.pnl.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl.Controls.Add(Me.cmbProviders)
        Me.pnl.Controls.Add(Me.Label5)
        Me.pnl.Controls.Add(Me.lblRight)
        Me.pnl.Controls.Add(Me.lblLeft)
        Me.pnl.Controls.Add(Me.Label4)
        Me.pnl.Controls.Add(Me.Label2)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl.Location = New System.Drawing.Point(0, 56)
        Me.pnl.Name = "pnl"
        Me.pnl.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnl.Size = New System.Drawing.Size(974, 35)
        Me.pnl.TabIndex = 31
        '
        'cmbProviders
        '
        Me.cmbProviders.FormattingEnabled = True
        Me.cmbProviders.Location = New System.Drawing.Point(92, 8)
        Me.cmbProviders.Name = "cmbProviders"
        Me.cmbProviders.Size = New System.Drawing.Size(296, 22)
        Me.cmbProviders.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 14)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Provider :"
        '
        'lblRight
        '
        Me.lblRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblRight.Location = New System.Drawing.Point(970, 4)
        Me.lblRight.Name = "lblRight"
        Me.lblRight.Size = New System.Drawing.Size(1, 30)
        Me.lblRight.TabIndex = 9
        Me.lblRight.Text = "label1"
        '
        'lblLeft
        '
        Me.lblLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblLeft.Location = New System.Drawing.Point(3, 4)
        Me.lblLeft.Name = "lblLeft"
        Me.lblLeft.Size = New System.Drawing.Size(1, 30)
        Me.lblLeft.TabIndex = 10
        Me.lblLeft.Text = "label1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(968, 1)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(3, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(968, 1)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "label2"
        '
        'pnlAccelerator
        '
        Me.pnlAccelerator.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlAccelerator.Controls.Add(Me.lblBottom)
        Me.pnlAccelerator.Controls.Add(Me.lblGridBottom)
        Me.pnlAccelerator.Controls.Add(Me.lblGridLeft)
        Me.pnlAccelerator.Controls.Add(Me.lblGridRight)
        Me.pnlAccelerator.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAccelerator.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlAccelerator.Location = New System.Drawing.Point(0, 91)
        Me.pnlAccelerator.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlAccelerator.Name = "pnlAccelerator"
        Me.pnlAccelerator.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlAccelerator.Size = New System.Drawing.Size(974, 572)
        Me.pnlAccelerator.TabIndex = 32
        '
        'lblBottom
        '
        Me.lblBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblBottom.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblBottom.Location = New System.Drawing.Point(4, 3)
        Me.lblBottom.Name = "lblBottom"
        Me.lblBottom.Size = New System.Drawing.Size(966, 1)
        Me.lblBottom.TabIndex = 13
        Me.lblBottom.Text = "label1"
        '
        'lblGridBottom
        '
        Me.lblGridBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblGridBottom.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblGridBottom.Location = New System.Drawing.Point(4, 568)
        Me.lblGridBottom.Name = "lblGridBottom"
        Me.lblGridBottom.Size = New System.Drawing.Size(966, 1)
        Me.lblGridBottom.TabIndex = 10
        Me.lblGridBottom.Text = "label2"
        '
        'lblGridLeft
        '
        Me.lblGridLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblGridLeft.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGridLeft.Location = New System.Drawing.Point(3, 3)
        Me.lblGridLeft.Name = "lblGridLeft"
        Me.lblGridLeft.Size = New System.Drawing.Size(1, 566)
        Me.lblGridLeft.TabIndex = 9
        Me.lblGridLeft.Text = "label4"
        '
        'lblGridRight
        '
        Me.lblGridRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblGridRight.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblGridRight.Location = New System.Drawing.Point(970, 3)
        Me.lblGridRight.Name = "lblGridRight"
        Me.lblGridRight.Size = New System.Drawing.Size(1, 566)
        Me.lblGridRight.TabIndex = 8
        Me.lblGridRight.Text = "label3"
        '
        'frmViewEPA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(974, 663)
        Me.Controls.Add(Me.pnlAccelerator)
        Me.Controls.Add(Me.pnl)
        Me.Controls.Add(Me.pnlToolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewEPA"
        Me.Text = "ePA Requests"
        Me.pnlToolstrip.ResumeLayout(False)
        Me.tls_Main.ResumeLayout(False)
        Me.tls_Main.PerformLayout()
        Me.pnl.ResumeLayout(False)
        Me.pnl.PerformLayout()
        Me.pnlAccelerator.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlToolstrip As System.Windows.Forms.Panel
    Friend WithEvents tls_Main As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtn_WorkList As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtn_Processes As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Friend WithEvents cmbProviders As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents lblRight As System.Windows.Forms.Label
    Private WithEvents lblLeft As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlAccelerator As System.Windows.Forms.Panel
    Private WithEvents lblBottom As System.Windows.Forms.Label
    Private WithEvents lblGridBottom As System.Windows.Forms.Label
    Private WithEvents lblGridLeft As System.Windows.Forms.Label
    Private WithEvents lblGridRight As System.Windows.Forms.Label
End Class
