<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportFeeSchedule
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportFeeSchedule))
        Me.pnltlsStrip = New System.Windows.Forms.Panel
        Me.tls_SetupResource = New System.Windows.Forms.ToolStrip
        Me.tsb_Import = New System.Windows.Forms.ToolStripButton
        Me.tsb_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlImport = New System.Windows.Forms.Panel
        Me.numChargePercentage = New System.Windows.Forms.NumericUpDown
        Me.label7 = New System.Windows.Forms.Label
        Me.label6 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.txtFeeScheduleName = New System.Windows.Forms.TextBox
        Me.txtImportFile = New System.Windows.Forms.TextBox
        Me.btn_Browse = New System.Windows.Forms.Button
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.label59 = New System.Windows.Forms.Label
        Me.label19 = New System.Windows.Forms.Label
        Me.label8 = New System.Windows.Forms.Label
        Me.dlgBrowseFile = New System.Windows.Forms.OpenFileDialog
        Me.pnltlsStrip.SuspendLayout()
        Me.tls_SetupResource.SuspendLayout()
        Me.pnlImport.SuspendLayout()
        CType(Me.numChargePercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnltlsStrip
        '
        Me.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltlsStrip.Controls.Add(Me.tls_SetupResource)
        Me.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltlsStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnltlsStrip.Name = "pnltlsStrip"
        Me.pnltlsStrip.Size = New System.Drawing.Size(402, 54)
        Me.pnltlsStrip.TabIndex = 2
        '
        'tls_SetupResource
        '
        Me.tls_SetupResource.BackColor = System.Drawing.Color.Transparent
        Me.tls_SetupResource.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Toolstrip
        Me.tls_SetupResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_SetupResource.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_SetupResource.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_SetupResource.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsb_Import, Me.tsb_Close})
        Me.tls_SetupResource.Location = New System.Drawing.Point(0, 0)
        Me.tls_SetupResource.Name = "tls_SetupResource"
        Me.tls_SetupResource.Size = New System.Drawing.Size(402, 53)
        Me.tls_SetupResource.TabIndex = 0
        Me.tls_SetupResource.TabStop = True
        Me.tls_SetupResource.Text = "toolStrip1"
        '
        'tsb_Import
        '
        Me.tsb_Import.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Import.Image = CType(resources.GetObject("tsb_Import.Image"), System.Drawing.Image)
        Me.tsb_Import.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Import.Name = "tsb_Import"
        Me.tsb_Import.Size = New System.Drawing.Size(54, 50)
        Me.tsb_Import.Tag = "Import"
        Me.tsb_Import.Text = "&Import"
        Me.tsb_Import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_Import.ToolTipText = "Import Fee Schedule"
        '
        'tsb_Close
        '
        Me.tsb_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Close.Image = CType(resources.GetObject("tsb_Close.Image"), System.Drawing.Image)
        Me.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Close.Name = "tsb_Close"
        Me.tsb_Close.Size = New System.Drawing.Size(43, 50)
        Me.tsb_Close.Tag = "Cancel"
        Me.tsb_Close.Text = "&Close"
        Me.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlImport
        '
        Me.pnlImport.Controls.Add(Me.numChargePercentage)
        Me.pnlImport.Controls.Add(Me.label7)
        Me.pnlImport.Controls.Add(Me.label6)
        Me.pnlImport.Controls.Add(Me.label5)
        Me.pnlImport.Controls.Add(Me.label4)
        Me.pnlImport.Controls.Add(Me.txtFeeScheduleName)
        Me.pnlImport.Controls.Add(Me.txtImportFile)
        Me.pnlImport.Controls.Add(Me.btn_Browse)
        Me.pnlImport.Controls.Add(Me.label3)
        Me.pnlImport.Controls.Add(Me.label2)
        Me.pnlImport.Controls.Add(Me.label1)
        Me.pnlImport.Controls.Add(Me.label59)
        Me.pnlImport.Controls.Add(Me.label19)
        Me.pnlImport.Controls.Add(Me.label8)
        Me.pnlImport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImport.Location = New System.Drawing.Point(0, 54)
        Me.pnlImport.Name = "pnlImport"
        Me.pnlImport.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlImport.Size = New System.Drawing.Size(402, 118)
        Me.pnlImport.TabIndex = 3
        '
        'numChargePercentage
        '
        Me.numChargePercentage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numChargePercentage.ForeColor = System.Drawing.Color.Black
        Me.numChargePercentage.Location = New System.Drawing.Point(154, 74)
        Me.numChargePercentage.Name = "numChargePercentage"
        Me.numChargePercentage.Size = New System.Drawing.Size(59, 22)
        Me.numChargePercentage.TabIndex = 4
        Me.numChargePercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.Location = New System.Drawing.Point(218, 78)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(19, 14)
        Me.label7.TabIndex = 33
        Me.label7.Text = "%"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.Location = New System.Drawing.Point(30, 78)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(120, 14)
        Me.label6.TabIndex = 33
        Me.label6.Text = "Charge Percentage :"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.Location = New System.Drawing.Point(26, 48)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(124, 14)
        Me.label5.TabIndex = 33
        Me.label5.Text = "Fee Schedule Name :"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.Location = New System.Drawing.Point(83, 18)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(67, 14)
        Me.label4.TabIndex = 33
        Me.label4.Text = "File Name :"
        '
        'txtFeeScheduleName
        '
        Me.txtFeeScheduleName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFeeScheduleName.ForeColor = System.Drawing.Color.Black
        Me.txtFeeScheduleName.Location = New System.Drawing.Point(154, 44)
        Me.txtFeeScheduleName.Name = "txtFeeScheduleName"
        Me.txtFeeScheduleName.Size = New System.Drawing.Size(204, 22)
        Me.txtFeeScheduleName.TabIndex = 3
        '
        'txtImportFile
        '
        Me.txtImportFile.BackColor = System.Drawing.Color.White
        Me.txtImportFile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImportFile.ForeColor = System.Drawing.Color.Black
        Me.txtImportFile.Location = New System.Drawing.Point(154, 14)
        Me.txtImportFile.Name = "txtImportFile"
        Me.txtImportFile.ReadOnly = True
        Me.txtImportFile.Size = New System.Drawing.Size(204, 22)
        Me.txtImportFile.TabIndex = 1
        '
        'btn_Browse
        '
        Me.btn_Browse.AutoEllipsis = True
        Me.btn_Browse.BackColor = System.Drawing.Color.Transparent
        Me.btn_Browse.BackgroundImage = CType(resources.GetObject("btn_Browse.BackgroundImage"), System.Drawing.Image)
        Me.btn_Browse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Browse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Browse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Browse.Image = CType(resources.GetObject("btn_Browse.Image"), System.Drawing.Image)
        Me.btn_Browse.Location = New System.Drawing.Point(364, 14)
        Me.btn_Browse.Name = "btn_Browse"
        Me.btn_Browse.Size = New System.Drawing.Size(22, 22)
        Me.btn_Browse.TabIndex = 2
        Me.btn_Browse.UseVisualStyleBackColor = False
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label3.Location = New System.Drawing.Point(4, 114)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(394, 1)
        Me.label3.TabIndex = 29
        Me.label3.Text = "label3"
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.label2.Location = New System.Drawing.Point(4, 3)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(394, 1)
        Me.label2.TabIndex = 28
        Me.label2.Text = "label2"
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.label1.Location = New System.Drawing.Point(398, 3)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(1, 112)
        Me.label1.TabIndex = 27
        Me.label1.Text = "label1"
        '
        'label59
        '
        Me.label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label59.Dock = System.Windows.Forms.DockStyle.Left
        Me.label59.Location = New System.Drawing.Point(3, 3)
        Me.label59.Name = "label59"
        Me.label59.Size = New System.Drawing.Size(1, 112)
        Me.label59.TabIndex = 26
        Me.label59.Text = "label59"
        '
        'label19
        '
        Me.label19.AutoEllipsis = True
        Me.label19.AutoSize = True
        Me.label19.BackColor = System.Drawing.Color.Transparent
        Me.label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label19.ForeColor = System.Drawing.Color.Red
        Me.label19.Location = New System.Drawing.Point(69, 16)
        Me.label19.Name = "label19"
        Me.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.label19.Size = New System.Drawing.Size(14, 14)
        Me.label19.TabIndex = 111
        Me.label19.Text = "*"
        Me.label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'label8
        '
        Me.label8.AutoEllipsis = True
        Me.label8.AutoSize = True
        Me.label8.BackColor = System.Drawing.Color.Transparent
        Me.label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.Color.Red
        Me.label8.Location = New System.Drawing.Point(11, 48)
        Me.label8.Name = "label8"
        Me.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.label8.Size = New System.Drawing.Size(14, 14)
        Me.label8.TabIndex = 112
        Me.label8.Text = "*"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'dlgBrowseFile
        '
        Me.dlgBrowseFile.FileName = "openFileDialog1"
        '
        'frmImportFeeSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(402, 172)
        Me.Controls.Add(Me.pnlImport)
        Me.Controls.Add(Me.pnltlsStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportFeeSchedule"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import Fee Schedule"
        Me.pnltlsStrip.ResumeLayout(False)
        Me.pnltlsStrip.PerformLayout()
        Me.tls_SetupResource.ResumeLayout(False)
        Me.tls_SetupResource.PerformLayout()
        Me.pnlImport.ResumeLayout(False)
        Me.pnlImport.PerformLayout()
        CType(Me.numChargePercentage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnltlsStrip As System.Windows.Forms.Panel
    Private WithEvents tls_SetupResource As System.Windows.Forms.ToolStrip
    Private WithEvents tsb_Import As System.Windows.Forms.ToolStripButton
    Private WithEvents tsb_Close As System.Windows.Forms.ToolStripButton
    Private WithEvents pnlImport As System.Windows.Forms.Panel
    Private WithEvents numChargePercentage As System.Windows.Forms.NumericUpDown
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents txtFeeScheduleName As System.Windows.Forms.TextBox
    Private WithEvents txtImportFile As System.Windows.Forms.TextBox
    Private WithEvents btn_Browse As System.Windows.Forms.Button
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label59 As System.Windows.Forms.Label
    Private WithEvents label19 As System.Windows.Forms.Label
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents dlgBrowseFile As System.Windows.Forms.OpenFileDialog
End Class
