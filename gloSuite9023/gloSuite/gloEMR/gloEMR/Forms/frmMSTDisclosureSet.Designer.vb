<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMSTDisclosureSet
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Patient Demographics")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("History")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Medication")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Notes")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Labs")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Radiology Orders")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DMS")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Consent")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Flowsheet")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Immunization")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Messages")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Vitals")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTDisclosureSet))
        Me.pnlmain = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.trvSet = New System.Windows.Forms.TreeView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.txtCategoryName = New System.Windows.Forms.TextBox
        Me.lblCategoryName = New System.Windows.Forms.Label
        Me.pnlbottom = New System.Windows.Forms.Panel
        Me.tlsDisclosureSet = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsSave = New System.Windows.Forms.ToolStripButton
        Me.tlsClose = New System.Windows.Forms.ToolStripButton
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlmain.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlbottom.SuspendLayout()
        Me.tlsDisclosureSet.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlmain
        '
        Me.pnlmain.Controls.Add(Me.Label4)
        Me.pnlmain.Controls.Add(Me.Label3)
        Me.pnlmain.Controls.Add(Me.Label2)
        Me.pnlmain.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlmain.Controls.Add(Me.trvSet)
        Me.pnlmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlmain.Location = New System.Drawing.Point(0, 40)
        Me.pnlmain.Name = "pnlmain"
        Me.pnlmain.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlmain.Size = New System.Drawing.Size(436, 271)
        Me.pnlmain.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(428, 1)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "label1"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(432, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 267)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "label4"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 267)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "label4"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(3, 267)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(430, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'trvSet
        '
        Me.trvSet.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSet.CheckBoxes = True
        Me.trvSet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSet.ForeColor = System.Drawing.Color.Black
        Me.trvSet.Indent = 20
        Me.trvSet.ItemHeight = 20
        Me.trvSet.Location = New System.Drawing.Point(3, 0)
        Me.trvSet.Name = "trvSet"
        TreeNode1.Name = "Node1"
        TreeNode1.Text = "Patient Demographics"
        TreeNode2.Name = "Node2"
        TreeNode2.Text = "History"
        TreeNode3.Name = "Node1"
        TreeNode3.Text = "Medication"
        TreeNode4.Name = "Node2"
        TreeNode4.Text = "Notes"
        TreeNode5.Name = "Node3"
        TreeNode5.Text = "Labs"
        TreeNode6.Name = "Node4"
        TreeNode6.Text = "Radiology Orders"
        TreeNode7.Name = "Node5"
        TreeNode7.Text = "DMS"
        TreeNode8.Name = "Node0"
        TreeNode8.Text = "Consent"
        TreeNode9.Name = "Node1"
        TreeNode9.Text = "Flowsheet"
        TreeNode10.Name = "Node2"
        TreeNode10.Text = "Immunization"
        TreeNode11.Name = "Node3"
        TreeNode11.Text = "Messages"
        TreeNode12.Name = "Node4"
        TreeNode12.Text = "Vitals"
        Me.trvSet.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4, TreeNode5, TreeNode6, TreeNode7, TreeNode8, TreeNode9, TreeNode10, TreeNode11, TreeNode12})
        Me.trvSet.ShowLines = False
        Me.trvSet.Size = New System.Drawing.Size(430, 268)
        Me.trvSet.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel1.Controls.Add(Me.lbl_pnlTop)
        Me.Panel1.Controls.Add(Me.lbl_pnlRight)
        Me.Panel1.Controls.Add(Me.txtCategoryName)
        Me.Panel1.Controls.Add(Me.lblCategoryName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(436, 40)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(4, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(428, 1)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "label1"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 33)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(429, 1)
        Me.lbl_pnlTop.TabIndex = 6
        Me.lbl_pnlTop.Text = "label1"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(432, 3)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 34)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'txtCategoryName
        '
        Me.txtCategoryName.ForeColor = System.Drawing.Color.Black
        Me.txtCategoryName.Location = New System.Drawing.Point(128, 9)
        Me.txtCategoryName.Name = "txtCategoryName"
        Me.txtCategoryName.Size = New System.Drawing.Size(277, 22)
        Me.txtCategoryName.TabIndex = 0
        '
        'lblCategoryName
        '
        Me.lblCategoryName.AutoSize = True
        Me.lblCategoryName.Location = New System.Drawing.Point(23, 12)
        Me.lblCategoryName.Name = "lblCategoryName"
        Me.lblCategoryName.Size = New System.Drawing.Size(99, 14)
        Me.lblCategoryName.TabIndex = 0
        Me.lblCategoryName.Text = "Category Name :"
        '
        'pnlbottom
        '
        Me.pnlbottom.Controls.Add(Me.tlsDisclosureSet)
        Me.pnlbottom.Controls.Add(Me.btnOK)
        Me.pnlbottom.Controls.Add(Me.btnClose)
        Me.pnlbottom.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlbottom.Name = "pnlbottom"
        Me.pnlbottom.Size = New System.Drawing.Size(436, 54)
        Me.pnlbottom.TabIndex = 0
        '
        'tlsDisclosureSet
        '
        Me.tlsDisclosureSet.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDisclosureSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDisclosureSet.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDisclosureSet.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsSave, Me.tlsClose})
        Me.tlsDisclosureSet.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDisclosureSet.Location = New System.Drawing.Point(0, 0)
        Me.tlsDisclosureSet.Name = "tlsDisclosureSet"
        Me.tlsDisclosureSet.Size = New System.Drawing.Size(436, 53)
        Me.tlsDisclosureSet.TabIndex = 2
        Me.tlsDisclosureSet.Text = "ToolStrip1"
        '
        'tlsSave
        '
        Me.tlsSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsSave.Image = CType(resources.GetObject("tlsSave.Image"), System.Drawing.Image)
        Me.tlsSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsSave.Name = "tlsSave"
        Me.tlsSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsSave.Tag = "Save"
        Me.tlsSave.Text = "&Save&&Cls"
        Me.tlsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsSave.ToolTipText = "Save and Close"
        '
        'tlsClose
        '
        Me.tlsClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsClose.Image = CType(resources.GetObject("tlsClose.Image"), System.Drawing.Image)
        Me.tlsClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsClose.Name = "tlsClose"
        Me.tlsClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsClose.Tag = "Close"
        Me.tlsClose.Text = "&Close"
        Me.tlsClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(220, 13)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(122, 31)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(342, 13)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(122, 31)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlmain)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(436, 311)
        Me.Panel2.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(12, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 14)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "*"
        '
        'frmMSTDisclosureSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(436, 365)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlbottom)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMSTDisclosureSet"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Disclosure Set"
        Me.pnlmain.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlbottom.ResumeLayout(False)
        Me.pnlbottom.PerformLayout()
        Me.tlsDisclosureSet.ResumeLayout(False)
        Me.tlsDisclosureSet.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlmain As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtCategoryName As System.Windows.Forms.TextBox
    Friend WithEvents lblCategoryName As System.Windows.Forms.Label
    Friend WithEvents pnlbottom As System.Windows.Forms.Panel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents trvSet As System.Windows.Forms.TreeView
    Friend WithEvents tlsDisclosureSet As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
