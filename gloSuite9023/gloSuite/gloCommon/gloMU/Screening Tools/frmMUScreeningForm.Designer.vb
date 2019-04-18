<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMUScreeningForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMUScreeningForm))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlNEToolBar = New System.Windows.Forms.Panel()
        Me.Label150 = New System.Windows.Forms.Label()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.webBrowser = New System.Windows.Forms.WebBrowser()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnDownload = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlMain.SuspendLayout()
        Me.pnlNEToolBar.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.webBrowser)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(0, 83)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(768, 666)
        Me.pnlMain.TabIndex = 49
        '
        'pnlNEToolBar
        '
        Me.pnlNEToolBar.BackColor = System.Drawing.Color.Transparent
        Me.pnlNEToolBar.BackgroundImage = CType(resources.GetObject("pnlNEToolBar.BackgroundImage"), System.Drawing.Image)
        Me.pnlNEToolBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlNEToolBar.Controls.Add(Me.Panel1)
        Me.pnlNEToolBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlNEToolBar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlNEToolBar.Location = New System.Drawing.Point(0, 56)
        Me.pnlNEToolBar.Name = "pnlNEToolBar"
        Me.pnlNEToolBar.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlNEToolBar.Size = New System.Drawing.Size(768, 27)
        Me.pnlNEToolBar.TabIndex = 5
        '
        'Label150
        '
        Me.Label150.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label150.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label150.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label150.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label150.Location = New System.Drawing.Point(761, 1)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(1, 22)
        Me.Label150.TabIndex = 174
        Me.Label150.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbProvider
        '
        Me.cmbProvider.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.ForeColor = System.Drawing.Color.Black
        Me.cmbProvider.Location = New System.Drawing.Point(94, 1)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(190, 22)
        Me.cmbProvider.Sorted = True
        Me.cmbProvider.TabIndex = 164
        '
        'Label144
        '
        Me.Label144.BackColor = System.Drawing.Color.Transparent
        Me.Label144.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label144.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label144.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label144.Location = New System.Drawing.Point(1, 1)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(93, 22)
        Me.Label144.TabIndex = 163
        Me.Label144.Text = "Provider :"
        Me.Label144.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label148
        '
        Me.Label148.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label148.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label148.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label148.Location = New System.Drawing.Point(1, 23)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(761, 1)
        Me.Label148.TabIndex = 167
        Me.Label148.Text = "label1"
        '
        'Label147
        '
        Me.Label147.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label147.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label147.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label147.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label147.Location = New System.Drawing.Point(1, 0)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(761, 1)
        Me.Label147.TabIndex = 166
        Me.Label147.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(764, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 658)
        Me.Label4.TabIndex = 4
        '
        'webBrowser
        '
        Me.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.webBrowser.Location = New System.Drawing.Point(4, 4)
        Me.webBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.webBrowser.Name = "webBrowser"
        Me.webBrowser.Size = New System.Drawing.Size(761, 658)
        Me.webBrowser.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 658)
        Me.Label3.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 662)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(762, 1)
        Me.Label2.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(762, 1)
        Me.Label1.TabIndex = 1
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(768, 56)
        Me.pnlToolStrip.TabIndex = 51
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloMU.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnDownload, Me.tlbbtnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(768, 53)
        Me.ts_ViewButtons.TabIndex = 50
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnDownload
        '
        Me.ts_btnDownload.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDownload.Image = CType(resources.GetObject("ts_btnDownload.Image"), System.Drawing.Image)
        Me.ts_btnDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDownload.Name = "ts_btnDownload"
        Me.ts_btnDownload.Size = New System.Drawing.Size(168, 50)
        Me.ts_btnDownload.Text = "&Download && Close Survey"
        Me.ts_btnDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnDownload.ToolTipText = "Refresh Prescriptions List"
        Me.ts_btnDownload.Visible = False
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
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 24)
        Me.Label5.TabIndex = 175
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label150)
        Me.Panel1.Controls.Add(Me.cmbProvider)
        Me.Panel1.Controls.Add(Me.Label144)
        Me.Panel1.Controls.Add(Me.Label148)
        Me.Panel1.Controls.Add(Me.Label147)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(762, 24)
        Me.Panel1.TabIndex = 176
        '
        'frmMUScreeningForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(768, 749)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlNEToolBar)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMUScreeningForm"
        Me.Text = "HOOS Test"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlNEToolBar.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents webBrowser As System.Windows.Forms.WebBrowser
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlNEToolBar As System.Windows.Forms.Panel
    Friend WithEvents Label150 As System.Windows.Forms.Label
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label144 As System.Windows.Forms.Label
    Private WithEvents Label148 As System.Windows.Forms.Label
    Friend WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label

End Class
