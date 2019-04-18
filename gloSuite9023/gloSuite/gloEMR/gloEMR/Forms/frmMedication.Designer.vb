<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMedication
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMedication))
        Me.pnlRight = New System.Windows.Forms.Panel
        Me.splleft = New System.Windows.Forms.Splitter
        Me.pnlleft = New System.Windows.Forms.Panel
        Me.pnltop = New System.Windows.Forms.Panel
        Me.pnlDI = New System.Windows.Forms.Panel
        Me.pnlmainToolBar = New System.Windows.Forms.Panel
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.splRight = New System.Windows.Forms.Splitter
        Me.pnlcenter = New System.Windows.Forms.Panel
        Me.pnlFlexGrid = New System.Windows.Forms.Panel
        Me.pnlAllergiesAlerts = New System.Windows.Forms.Panel
        Me.lblAlert1 = New System.Windows.Forms.Label
        Me.picAlertClose1 = New System.Windows.Forms.PictureBox
        Me.picInfo = New System.Windows.Forms.PictureBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.picMiddle = New System.Windows.Forms.PictureBox
        Me.picBottom = New System.Windows.Forms.PictureBox
        Me.picTop = New System.Windows.Forms.PictureBox
        Me.splTop = New System.Windows.Forms.Splitter
        Me.pnlmonograph = New System.Windows.Forms.Panel
        Me.pnlDIScreenResult = New System.Windows.Forms.Panel
        Me.pnlRefill = New System.Windows.Forms.Panel
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnltop.SuspendLayout()
        Me.pnlcenter.SuspendLayout()
        Me.pnlFlexGrid.SuspendLayout()
        Me.pnlAllergiesAlerts.SuspendLayout()
        CType(Me.picAlertClose1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picMiddle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlRight
        '
        Me.pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRight.Location = New System.Drawing.Point(774, 57)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlRight.Size = New System.Drawing.Size(254, 689)
        Me.pnlRight.TabIndex = 3
        '
        'splleft
        '
        Me.splleft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.splleft.Location = New System.Drawing.Point(233, 57)
        Me.splleft.Name = "splleft"
        Me.splleft.Size = New System.Drawing.Size(3, 689)
        Me.splleft.TabIndex = 2
        Me.splleft.TabStop = False
        '
        'pnlleft
        '
        Me.pnlleft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlleft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlleft.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlleft.Location = New System.Drawing.Point(0, 57)
        Me.pnlleft.Name = "pnlleft"
        Me.pnlleft.Size = New System.Drawing.Size(233, 689)
        Me.pnlleft.TabIndex = 1
        '
        'pnltop
        '
        Me.pnltop.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltop.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.pnltop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltop.Controls.Add(Me.pnlDI)
        Me.pnltop.Controls.Add(Me.pnlmainToolBar)
        Me.pnltop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltop.Location = New System.Drawing.Point(0, 0)
        Me.pnltop.Name = "pnltop"
        Me.pnltop.Size = New System.Drawing.Size(1028, 57)
        Me.pnltop.TabIndex = 0
        '
        'pnlDI
        '
        Me.pnlDI.BackColor = System.Drawing.Color.Transparent
        Me.pnlDI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDI.Location = New System.Drawing.Point(501, 0)
        Me.pnlDI.Name = "pnlDI"
        Me.pnlDI.Size = New System.Drawing.Size(527, 57)
        Me.pnlDI.TabIndex = 1
        '
        'pnlmainToolBar
        '
        Me.pnlmainToolBar.BackColor = System.Drawing.Color.Transparent
        Me.pnlmainToolBar.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlmainToolBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlmainToolBar.Name = "pnlmainToolBar"
        Me.pnlmainToolBar.Size = New System.Drawing.Size(501, 57)
        Me.pnlmainToolBar.TabIndex = 0
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'splRight
        '
        Me.splRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.splRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.splRight.Location = New System.Drawing.Point(771, 57)
        Me.splRight.Name = "splRight"
        Me.splRight.Size = New System.Drawing.Size(3, 689)
        Me.splRight.TabIndex = 4
        Me.splRight.TabStop = False
        '
        'pnlcenter
        '
        Me.pnlcenter.AutoScroll = True
        Me.pnlcenter.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.pnlcenter.AutoSize = True
        Me.pnlcenter.Controls.Add(Me.pnlFlexGrid)
        Me.pnlcenter.Controls.Add(Me.splTop)
        Me.pnlcenter.Controls.Add(Me.pnlmonograph)
        Me.pnlcenter.Controls.Add(Me.pnlDIScreenResult)
        Me.pnlcenter.Controls.Add(Me.pnlRefill)
        Me.pnlcenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlcenter.Location = New System.Drawing.Point(236, 57)
        Me.pnlcenter.Name = "pnlcenter"
        Me.pnlcenter.Size = New System.Drawing.Size(535, 689)
        Me.pnlcenter.TabIndex = 5
        '
        'pnlFlexGrid
        '
        Me.pnlFlexGrid.AutoScroll = True
        Me.pnlFlexGrid.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.pnlFlexGrid.AutoSize = True
        Me.pnlFlexGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlFlexGrid.Controls.Add(Me.pnlAllergiesAlerts)
        Me.pnlFlexGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFlexGrid.Location = New System.Drawing.Point(0, 3)
        Me.pnlFlexGrid.Name = "pnlFlexGrid"
        Me.pnlFlexGrid.Size = New System.Drawing.Size(535, 380)
        Me.pnlFlexGrid.TabIndex = 1
        '
        'pnlAllergiesAlerts
        '
        Me.pnlAllergiesAlerts.BackColor = System.Drawing.Color.Transparent
        Me.pnlAllergiesAlerts.BackgroundImage = CType(resources.GetObject("pnlAllergiesAlerts.BackgroundImage"), System.Drawing.Image)
        Me.pnlAllergiesAlerts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlAllergiesAlerts.Controls.Add(Me.lblAlert1)
        Me.pnlAllergiesAlerts.Controls.Add(Me.picAlertClose1)
        Me.pnlAllergiesAlerts.Controls.Add(Me.picInfo)
        Me.pnlAllergiesAlerts.Controls.Add(Me.Label15)
        Me.pnlAllergiesAlerts.Controls.Add(Me.picMiddle)
        Me.pnlAllergiesAlerts.Controls.Add(Me.picBottom)
        Me.pnlAllergiesAlerts.Controls.Add(Me.picTop)
        Me.pnlAllergiesAlerts.Location = New System.Drawing.Point(123, 180)
        Me.pnlAllergiesAlerts.Name = "pnlAllergiesAlerts"
        Me.pnlAllergiesAlerts.Size = New System.Drawing.Size(412, 112)
        Me.pnlAllergiesAlerts.TabIndex = 58
        '
        'lblAlert1
        '
        Me.lblAlert1.BackColor = System.Drawing.Color.Transparent
        Me.lblAlert1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlert1.Location = New System.Drawing.Point(50, 31)
        Me.lblAlert1.Name = "lblAlert1"
        Me.lblAlert1.Size = New System.Drawing.Size(327, 64)
        Me.lblAlert1.TabIndex = 64
        '
        'picAlertClose1
        '
        Me.picAlertClose1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picAlertClose1.BackColor = System.Drawing.Color.Transparent
        Me.picAlertClose1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picAlertClose1.Image = CType(resources.GetObject("picAlertClose1.Image"), System.Drawing.Image)
        Me.picAlertClose1.Location = New System.Drawing.Point(380, 8)
        Me.picAlertClose1.Name = "picAlertClose1"
        Me.picAlertClose1.Size = New System.Drawing.Size(16, 16)
        Me.picAlertClose1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picAlertClose1.TabIndex = 65
        Me.picAlertClose1.TabStop = False
        '
        'picInfo
        '
        Me.picInfo.BackColor = System.Drawing.Color.Transparent
        Me.picInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.picInfo.Image = CType(resources.GetObject("picInfo.Image"), System.Drawing.Image)
        Me.picInfo.Location = New System.Drawing.Point(14, 17)
        Me.picInfo.Name = "picInfo"
        Me.picInfo.Size = New System.Drawing.Size(32, 32)
        Me.picInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picInfo.TabIndex = 67
        Me.picInfo.TabStop = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(50, 9)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(66, 18)
        Me.Label15.TabIndex = 66
        Me.Label15.Text = "Allergies"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picMiddle
        '
        Me.picMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picMiddle.Image = CType(resources.GetObject("picMiddle.Image"), System.Drawing.Image)
        Me.picMiddle.Location = New System.Drawing.Point(0, 17)
        Me.picMiddle.Name = "picMiddle"
        Me.picMiddle.Size = New System.Drawing.Size(412, 77)
        Me.picMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picMiddle.TabIndex = 63
        Me.picMiddle.TabStop = False
        '
        'picBottom
        '
        Me.picBottom.BackColor = System.Drawing.SystemColors.Info
        Me.picBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.picBottom.Image = CType(resources.GetObject("picBottom.Image"), System.Drawing.Image)
        Me.picBottom.Location = New System.Drawing.Point(0, 94)
        Me.picBottom.Name = "picBottom"
        Me.picBottom.Size = New System.Drawing.Size(412, 18)
        Me.picBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picBottom.TabIndex = 62
        Me.picBottom.TabStop = False
        '
        'picTop
        '
        Me.picTop.BackColor = System.Drawing.SystemColors.Info
        Me.picTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.picTop.Image = CType(resources.GetObject("picTop.Image"), System.Drawing.Image)
        Me.picTop.Location = New System.Drawing.Point(0, 0)
        Me.picTop.Name = "picTop"
        Me.picTop.Size = New System.Drawing.Size(412, 17)
        Me.picTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picTop.TabIndex = 61
        Me.picTop.TabStop = False
        '
        'splTop
        '
        Me.splTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.splTop.Location = New System.Drawing.Point(0, 0)
        Me.splTop.Name = "splTop"
        Me.splTop.Size = New System.Drawing.Size(535, 3)
        Me.splTop.TabIndex = 1
        Me.splTop.TabStop = False
        Me.splTop.Visible = False
        '
        'pnlmonograph
        '
        Me.pnlmonograph.AutoScroll = True
        Me.pnlmonograph.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.pnlmonograph.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlmonograph.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlmonograph.Location = New System.Drawing.Point(0, 0)
        Me.pnlmonograph.Name = "pnlmonograph"
        Me.pnlmonograph.Size = New System.Drawing.Size(535, 383)
        Me.pnlmonograph.TabIndex = 1
        '
        'pnlDIScreenResult
        '
        Me.pnlDIScreenResult.AutoScroll = True
        Me.pnlDIScreenResult.AutoScrollMargin = New System.Drawing.Size(2, 2)
        Me.pnlDIScreenResult.AutoSize = True
        Me.pnlDIScreenResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlDIScreenResult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDIScreenResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDIScreenResult.Location = New System.Drawing.Point(0, 0)
        Me.pnlDIScreenResult.Name = "pnlDIScreenResult"
        Me.pnlDIScreenResult.Size = New System.Drawing.Size(535, 383)
        Me.pnlDIScreenResult.TabIndex = 0
        '
        'pnlRefill
        '
        Me.pnlRefill.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlRefill.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlRefill.Location = New System.Drawing.Point(0, 383)
        Me.pnlRefill.Name = "pnlRefill"
        Me.pnlRefill.Size = New System.Drawing.Size(535, 306)
        Me.pnlRefill.TabIndex = 0
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlcenter)
        Me.pnlMain.Controls.Add(Me.splRight)
        Me.pnlMain.Controls.Add(Me.pnlRight)
        Me.pnlMain.Controls.Add(Me.splleft)
        Me.pnlMain.Controls.Add(Me.pnlleft)
        Me.pnlMain.Controls.Add(Me.pnltop)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1028, 746)
        Me.pnlMain.TabIndex = 1
        '
        'frmMedication
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMedication"
        Me.Text = "Medication"
        Me.pnltop.ResumeLayout(False)
        Me.pnlcenter.ResumeLayout(False)
        Me.pnlcenter.PerformLayout()
        Me.pnlFlexGrid.ResumeLayout(False)
        Me.pnlAllergiesAlerts.ResumeLayout(False)
        CType(Me.picAlertClose1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picMiddle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBottom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents splleft As System.Windows.Forms.Splitter
    Friend WithEvents pnlleft As System.Windows.Forms.Panel
    Friend WithEvents pnltop As System.Windows.Forms.Panel
    Friend WithEvents pnlDI As System.Windows.Forms.Panel
    Friend WithEvents pnlmainToolBar As System.Windows.Forms.Panel
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents splRight As System.Windows.Forms.Splitter
    Friend WithEvents pnlcenter As System.Windows.Forms.Panel
    Friend WithEvents pnlFlexGrid As System.Windows.Forms.Panel
    Friend WithEvents pnlmonograph As System.Windows.Forms.Panel
    Friend WithEvents pnlDIScreenResult As System.Windows.Forms.Panel
    Friend WithEvents splTop As System.Windows.Forms.Splitter
    Friend WithEvents pnlRefill As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlAllergiesAlerts As System.Windows.Forms.Panel
    Friend WithEvents lblAlert1 As System.Windows.Forms.Label
    Friend WithEvents picAlertClose1 As System.Windows.Forms.PictureBox
    Friend WithEvents picInfo As System.Windows.Forms.PictureBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents picMiddle As System.Windows.Forms.PictureBox
    Friend WithEvents picBottom As System.Windows.Forms.PictureBox
    Friend WithEvents picTop As System.Windows.Forms.PictureBox
    ' Friend WithEvents GloRxToolBarUserCtrl1 As gloUserControl_Prescription.gloRxToolbarUserCtrl
End Class
