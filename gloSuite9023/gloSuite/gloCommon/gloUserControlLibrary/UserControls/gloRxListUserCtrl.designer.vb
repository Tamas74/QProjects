<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloRxListUserCtrl
    Inherits gloUserControlLibrary.gloListUsrCtrl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloRxListUserCtrl))
        Me.btnProvider = New System.Windows.Forms.Button()
        Me.btnAllDrugs = New System.Windows.Forms.Button()
        Me.btnFreqDrugs = New System.Windows.Forms.Button()
        Me.pnl_btnClinicalDrugs = New System.Windows.Forms.Panel()
        Me.btnClinicalDrugs = New System.Windows.Forms.Button()
        Me.pnl_btnAllDrugs = New System.Windows.Forms.Panel()
        Me.pnl_btnProvider = New System.Windows.Forms.Panel()
        Me.pnl_btnFreqDrugs = New System.Windows.Forms.Panel()
        Me.btnProvFreqDrugs = New System.Windows.Forms.Button()
        Me.pnl_btnPrvFreqDrugs = New System.Windows.Forms.Panel()
        Me.pnl_btnClassifiedDurgs = New System.Windows.Forms.Panel()
        Me.btnClassifiedDrugs = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        Me.pnlTreeView.SuspendLayout()
        Me.pnl_btnClinicalDrugs.SuspendLayout()
        Me.pnl_btnAllDrugs.SuspendLayout()
        Me.pnl_btnProvider.SuspendLayout()
        Me.pnl_btnFreqDrugs.SuspendLayout()
        Me.pnl_btnPrvFreqDrugs.SuspendLayout()
        Me.pnl_btnClassifiedDurgs.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.pnl_btnPrvFreqDrugs)
        Me.pnlMain.Controls.Add(Me.pnl_btnClinicalDrugs)
        Me.pnlMain.Controls.Add(Me.pnl_btnAllDrugs)
        Me.pnlMain.Controls.Add(Me.pnl_btnProvider)
        Me.pnlMain.Controls.Add(Me.pnl_btnFreqDrugs)
        Me.pnlMain.Controls.Add(Me.pnl_btnClassifiedDurgs)
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Controls.SetChildIndex(Me.pnl_btnClassifiedDurgs, 0)
        Me.pnlMain.Controls.SetChildIndex(Me.pnl_btnFreqDrugs, 0)
        Me.pnlMain.Controls.SetChildIndex(Me.pnl_btnProvider, 0)
        Me.pnlMain.Controls.SetChildIndex(Me.pnl_btnAllDrugs, 0)
        Me.pnlMain.Controls.SetChildIndex(Me.pnl_btnClinicalDrugs, 0)
        Me.pnlMain.Controls.SetChildIndex(Me.pnl_btnPrvFreqDrugs, 0)
        Me.pnlMain.Controls.SetChildIndex(Me.pnlTreeView, 0)
        '
        'txtsearchDrug
        '
        Me.txtsearchDrug.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchDrug.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearchDrug.Size = New System.Drawing.Size(182, 15)
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.Images.SetKeyName(0, "Bullet06.ico")
        Me.ImageList.Images.SetKeyName(1, "Small Arrow.ico")
        Me.ImageList.Images.SetKeyName(2, "Template Category.ico")
        Me.ImageList.Images.SetKeyName(3, "bullet _04.ico")
        Me.ImageList.Images.SetKeyName(4, "bullet _05.ico")
        Me.ImageList.Images.SetKeyName(5, "Drugs.ico")
        Me.ImageList.Images.SetKeyName(6, "U.png")
        Me.ImageList.Images.SetKeyName(7, "Not Reimbursable16.png")
        Me.ImageList.Images.SetKeyName(8, "Off Formulary16.png")
        Me.ImageList.Images.SetKeyName(9, "On Formulary16.png")
        Me.ImageList.Images.SetKeyName(10, "Add Provider specific.ico")
        Me.ImageList.Images.SetKeyName(11, "Add drug.ico")
        '
        'pnlTreeView
        '
        Me.pnlTreeView.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTreeView.Location = New System.Drawing.Point(0, 60)
        Me.pnlTreeView.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnlTreeView.Size = New System.Drawing.Size(233, 568)
        '
        'trvList
        '
        Me.trvList.BackColor = System.Drawing.Color.White
        Me.trvList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvList.LineColor = System.Drawing.Color.Black
        Me.trvList.Location = New System.Drawing.Point(3, 3)
        Me.trvList.Size = New System.Drawing.Size(230, 565)
        '
        'btnProvider
        '
        Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnProvider.BackgroundImage = CType(resources.GetObject("btnProvider.BackgroundImage"), System.Drawing.Image)
        Me.btnProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnProvider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProvider.Location = New System.Drawing.Point(3, 3)
        Me.btnProvider.Name = "btnProvider"
        Me.btnProvider.Size = New System.Drawing.Size(230, 27)
        Me.btnProvider.TabIndex = 8
        Me.btnProvider.UseVisualStyleBackColor = False
        '
        'btnAllDrugs
        '
        Me.btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnAllDrugs.BackgroundImage = CType(resources.GetObject("btnAllDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnAllDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAllDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAllDrugs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAllDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllDrugs.Location = New System.Drawing.Point(3, 3)
        Me.btnAllDrugs.Name = "btnAllDrugs"
        Me.btnAllDrugs.Size = New System.Drawing.Size(230, 27)
        Me.btnAllDrugs.TabIndex = 7
        Me.btnAllDrugs.UseVisualStyleBackColor = False
        '
        'btnFreqDrugs
        '
        Me.btnFreqDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnFreqDrugs.BackgroundImage = CType(resources.GetObject("btnFreqDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnFreqDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFreqDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFreqDrugs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnFreqDrugs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnFreqDrugs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnFreqDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFreqDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFreqDrugs.Location = New System.Drawing.Point(3, 3)
        Me.btnFreqDrugs.Name = "btnFreqDrugs"
        Me.btnFreqDrugs.Size = New System.Drawing.Size(230, 27)
        Me.btnFreqDrugs.TabIndex = 1
        Me.btnFreqDrugs.UseVisualStyleBackColor = False
        '
        'pnl_btnClinicalDrugs
        '
        Me.pnl_btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_btnClinicalDrugs.Controls.Add(Me.btnClinicalDrugs)
        Me.pnl_btnClinicalDrugs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnClinicalDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnClinicalDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnClinicalDrugs.Location = New System.Drawing.Point(0, 658)
        Me.pnl_btnClinicalDrugs.Name = "pnl_btnClinicalDrugs"
        Me.pnl_btnClinicalDrugs.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnl_btnClinicalDrugs.Size = New System.Drawing.Size(233, 30)
        Me.pnl_btnClinicalDrugs.TabIndex = 25
        '
        'btnClinicalDrugs
        '
        Me.btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnClinicalDrugs.BackgroundImage = CType(resources.GetObject("btnClinicalDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnClinicalDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClinicalDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnClinicalDrugs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClinicalDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClinicalDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClinicalDrugs.Location = New System.Drawing.Point(3, 3)
        Me.btnClinicalDrugs.Name = "btnClinicalDrugs"
        Me.btnClinicalDrugs.Size = New System.Drawing.Size(230, 27)
        Me.btnClinicalDrugs.TabIndex = 6
        Me.btnClinicalDrugs.UseVisualStyleBackColor = False
        '
        'pnl_btnAllDrugs
        '
        Me.pnl_btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_btnAllDrugs.Controls.Add(Me.btnAllDrugs)
        Me.pnl_btnAllDrugs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnAllDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnAllDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnAllDrugs.Location = New System.Drawing.Point(0, 688)
        Me.pnl_btnAllDrugs.Name = "pnl_btnAllDrugs"
        Me.pnl_btnAllDrugs.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnl_btnAllDrugs.Size = New System.Drawing.Size(233, 30)
        Me.pnl_btnAllDrugs.TabIndex = 26
        '
        'pnl_btnProvider
        '
        Me.pnl_btnProvider.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_btnProvider.Controls.Add(Me.btnProvider)
        Me.pnl_btnProvider.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnProvider.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnProvider.Location = New System.Drawing.Point(0, 718)
        Me.pnl_btnProvider.Name = "pnl_btnProvider"
        Me.pnl_btnProvider.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnl_btnProvider.Size = New System.Drawing.Size(233, 30)
        Me.pnl_btnProvider.TabIndex = 27
        '
        'pnl_btnFreqDrugs
        '
        Me.pnl_btnFreqDrugs.BackColor = System.Drawing.Color.Transparent
        Me.pnl_btnFreqDrugs.Controls.Add(Me.btnFreqDrugs)
        Me.pnl_btnFreqDrugs.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_btnFreqDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnFreqDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnFreqDrugs.Location = New System.Drawing.Point(0, 30)
        Me.pnl_btnFreqDrugs.Name = "pnl_btnFreqDrugs"
        Me.pnl_btnFreqDrugs.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnl_btnFreqDrugs.Size = New System.Drawing.Size(233, 30)
        Me.pnl_btnFreqDrugs.TabIndex = 28
        '
        'btnProvFreqDrugs
        '
        Me.btnProvFreqDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnProvFreqDrugs.BackgroundImage = CType(resources.GetObject("btnProvFreqDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnProvFreqDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnProvFreqDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnProvFreqDrugs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnProvFreqDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProvFreqDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProvFreqDrugs.Location = New System.Drawing.Point(3, 3)
        Me.btnProvFreqDrugs.Name = "btnProvFreqDrugs"
        Me.btnProvFreqDrugs.Size = New System.Drawing.Size(230, 27)
        Me.btnProvFreqDrugs.TabIndex = 6
        Me.btnProvFreqDrugs.UseVisualStyleBackColor = False
        '
        'pnl_btnPrvFreqDrugs
        '
        Me.pnl_btnPrvFreqDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_btnPrvFreqDrugs.Controls.Add(Me.btnProvFreqDrugs)
        Me.pnl_btnPrvFreqDrugs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnPrvFreqDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnPrvFreqDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnPrvFreqDrugs.Location = New System.Drawing.Point(0, 628)
        Me.pnl_btnPrvFreqDrugs.Name = "pnl_btnPrvFreqDrugs"
        Me.pnl_btnPrvFreqDrugs.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnl_btnPrvFreqDrugs.Size = New System.Drawing.Size(233, 30)
        Me.pnl_btnPrvFreqDrugs.TabIndex = 29
        '
        'pnl_btnClassifiedDurgs
        '
        Me.pnl_btnClassifiedDurgs.BackColor = System.Drawing.Color.Transparent
        Me.pnl_btnClassifiedDurgs.Controls.Add(Me.btnClassifiedDrugs)
        Me.pnl_btnClassifiedDurgs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnClassifiedDurgs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnClassifiedDurgs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_btnClassifiedDurgs.Location = New System.Drawing.Point(0, 748)
        Me.pnl_btnClassifiedDurgs.Name = "pnl_btnClassifiedDurgs"
        Me.pnl_btnClassifiedDurgs.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnl_btnClassifiedDurgs.Size = New System.Drawing.Size(233, 30)
        Me.pnl_btnClassifiedDurgs.TabIndex = 30
        '
        'btnClassifiedDrugs
        '
        Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.Transparent
        Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.btnClassifiedDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClassifiedDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnClassifiedDrugs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClassifiedDrugs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClassifiedDrugs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClassifiedDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClassifiedDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClassifiedDrugs.Location = New System.Drawing.Point(3, 3)
        Me.btnClassifiedDrugs.Name = "btnClassifiedDrugs"
        Me.btnClassifiedDrugs.Size = New System.Drawing.Size(230, 27)
        Me.btnClassifiedDrugs.TabIndex = 1
        Me.btnClassifiedDrugs.UseVisualStyleBackColor = False
        '
        'gloRxListUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Name = "gloRxListUserCtrl"
        Me.Controls.SetChildIndex(Me.pnlMain, 0)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlTreeView.ResumeLayout(False)
        Me.pnl_btnClinicalDrugs.ResumeLayout(False)
        Me.pnl_btnAllDrugs.ResumeLayout(False)
        Me.pnl_btnProvider.ResumeLayout(False)
        Me.pnl_btnFreqDrugs.ResumeLayout(False)
        Me.pnl_btnPrvFreqDrugs.ResumeLayout(False)
        Me.pnl_btnClassifiedDurgs.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents btnFreqDrugs As System.Windows.Forms.Button
    Protected WithEvents btnAllDrugs As System.Windows.Forms.Button
    Protected WithEvents btnProvider As System.Windows.Forms.Button
    Private WithEvents pnl_btnProvider As System.Windows.Forms.Panel
    Private WithEvents pnl_btnAllDrugs As System.Windows.Forms.Panel
    Private WithEvents pnl_btnClinicalDrugs As System.Windows.Forms.Panel
    Private WithEvents pnl_btnFreqDrugs As System.Windows.Forms.Panel
    Protected WithEvents btnClinicalDrugs As System.Windows.Forms.Button
    Private WithEvents pnl_btnPrvFreqDrugs As System.Windows.Forms.Panel
    Protected WithEvents btnProvFreqDrugs As System.Windows.Forms.Button
    Private WithEvents pnl_btnClassifiedDurgs As System.Windows.Forms.Panel
    Protected WithEvents btnClassifiedDrugs As System.Windows.Forms.Button

End Class
