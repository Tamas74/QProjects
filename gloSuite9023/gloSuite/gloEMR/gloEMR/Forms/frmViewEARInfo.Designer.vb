<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewEARInfo
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewEARInfo))
        Me.ts_ViewButtons = New System.Windows.Forms.ToolStrip
        Me.tlbbtnClose = New System.Windows.Forms.ToolStripButton
        Me.tlStrpViewEARRequestFile = New System.Windows.Forms.ToolStripButton
        Me.tlStrpViewEARResponseFile = New System.Windows.Forms.ToolStripButton
        Me.pnlToostrip = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me._Flex = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlRefReqFlexGrid = New System.Windows.Forms.Panel
        Me.pnlFormularyCoverage = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.rchtxtbxEARRequestFile = New gloDIControl.gloRichtextbox(Me.components)
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.pnlFormularyCoverageHeading = New System.Windows.Forms.Panel
        Me.btnFormularyCovPnlClose = New System.Windows.Forms.Button
        Me.lblAlternativeDrugName = New System.Windows.Forms.Label
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlToostrip.SuspendLayout()
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRefReqFlexGrid.SuspendLayout()
        Me.pnlFormularyCoverage.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlFormularyCoverageHeading.SuspendLayout()
        Me.SuspendLayout()
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtnClose, Me.tlStrpViewEARRequestFile, Me.tlStrpViewEARResponseFile})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(934, 62)
        Me.ts_ViewButtons.TabIndex = 1
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'tlbbtnClose
        '
        Me.tlbbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtnClose.Image = CType(resources.GetObject("tlbbtnClose.Image"), System.Drawing.Image)
        Me.tlbbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnClose.Name = "tlbbtnClose"
        Me.tlbbtnClose.Size = New System.Drawing.Size(43, 59)
        Me.tlbbtnClose.Text = "&Close"
        Me.tlbbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlStrpViewEARRequestFile
        '
        Me.tlStrpViewEARRequestFile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlStrpViewEARRequestFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlStrpViewEARRequestFile.Image = CType(resources.GetObject("tlStrpViewEARRequestFile.Image"), System.Drawing.Image)
        Me.tlStrpViewEARRequestFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlStrpViewEARRequestFile.Name = "tlStrpViewEARRequestFile"
        Me.tlStrpViewEARRequestFile.Size = New System.Drawing.Size(124, 59)
        Me.tlStrpViewEARRequestFile.Text = "&View EAR Request"
        Me.tlStrpViewEARRequestFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlStrpViewEARResponseFile
        '
        Me.tlStrpViewEARResponseFile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlStrpViewEARResponseFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlStrpViewEARResponseFile.Image = CType(resources.GetObject("tlStrpViewEARResponseFile.Image"), System.Drawing.Image)
        Me.tlStrpViewEARResponseFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlStrpViewEARResponseFile.Name = "tlStrpViewEARResponseFile"
        Me.tlStrpViewEARResponseFile.Size = New System.Drawing.Size(132, 59)
        Me.tlStrpViewEARResponseFile.Text = "View EAR &Response"
        Me.tlStrpViewEARResponseFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlToostrip
        '
        Me.pnlToostrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToostrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToostrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToostrip.Name = "pnlToostrip"
        Me.pnlToostrip.Size = New System.Drawing.Size(934, 62)
        Me.pnlToostrip.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(3, 437)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(928, 1)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(928, 1)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 436)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(930, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 436)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "label3"
        '
        '_Flex
        '
        Me._Flex.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._Flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me._Flex.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me._Flex.Dock = System.Windows.Forms.DockStyle.Fill
        Me._Flex.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Flex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me._Flex.Location = New System.Drawing.Point(4, 1)
        Me._Flex.Name = "_Flex"
        Me._Flex.Rows.DefaultSize = 19
        Me._Flex.Size = New System.Drawing.Size(926, 436)
        Me._Flex.StyleInfo = resources.GetString("_Flex.StyleInfo")
        Me._Flex.TabIndex = 2
        '
        'pnlRefReqFlexGrid
        '
        Me.pnlRefReqFlexGrid.Controls.Add(Me.pnlFormularyCoverage)
        Me.pnlRefReqFlexGrid.Controls.Add(Me._Flex)
        Me.pnlRefReqFlexGrid.Controls.Add(Me.Label8)
        Me.pnlRefReqFlexGrid.Controls.Add(Me.Label7)
        Me.pnlRefReqFlexGrid.Controls.Add(Me.Label2)
        Me.pnlRefReqFlexGrid.Controls.Add(Me.Label1)
        Me.pnlRefReqFlexGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRefReqFlexGrid.Location = New System.Drawing.Point(0, 62)
        Me.pnlRefReqFlexGrid.Name = "pnlRefReqFlexGrid"
        Me.pnlRefReqFlexGrid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlRefReqFlexGrid.Size = New System.Drawing.Size(934, 441)
        Me.pnlRefReqFlexGrid.TabIndex = 21
        '
        'pnlFormularyCoverage
        '
        Me.pnlFormularyCoverage.BackColor = System.Drawing.Color.Transparent
        Me.pnlFormularyCoverage.Controls.Add(Me.Panel7)
        Me.pnlFormularyCoverage.Controls.Add(Me.Panel5)
        Me.pnlFormularyCoverage.Controls.Add(Me.pnlFormularyCoverageHeading)
        Me.pnlFormularyCoverage.Location = New System.Drawing.Point(236, 42)
        Me.pnlFormularyCoverage.Name = "pnlFormularyCoverage"
        Me.pnlFormularyCoverage.Size = New System.Drawing.Size(402, 390)
        Me.pnlFormularyCoverage.TabIndex = 60
        Me.pnlFormularyCoverage.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.BackgroundImage = CType(resources.GetObject("Panel7.BackgroundImage"), System.Drawing.Image)
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(0, 376)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(402, 14)
        Me.Panel7.TabIndex = 27
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.rchtxtbxEARRequestFile)
        Me.Panel5.Controls.Add(Me.Label19)
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.Label14)
        Me.Panel5.Controls.Add(Me.Label16)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.Controls.Add(Me.Label18)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(0, 28)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(402, 362)
        Me.Panel5.TabIndex = 26
        '
        'rchtxtbxEARRequestFile
        '
        Me.rchtxtbxEARRequestFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.rchtxtbxEARRequestFile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rchtxtbxEARRequestFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rchtxtbxEARRequestFile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rchtxtbxEARRequestFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.rchtxtbxEARRequestFile.Location = New System.Drawing.Point(4, 5)
        Me.rchtxtbxEARRequestFile.Name = "rchtxtbxEARRequestFile"
        Me.rchtxtbxEARRequestFile.ReadOnly = True
        Me.rchtxtbxEARRequestFile.Size = New System.Drawing.Size(397, 356)
        Me.rchtxtbxEARRequestFile.TabIndex = 1
        Me.rchtxtbxEARRequestFile.Text = ""
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.White
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1, 5)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(3, 356)
        Me.Label19.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(1, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(400, 4)
        Me.Label13.TabIndex = 9
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 361)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(400, 1)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "label2"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 361)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(401, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 361)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(402, 1)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "label1"
        '
        'pnlFormularyCoverageHeading
        '
        Me.pnlFormularyCoverageHeading.BackColor = System.Drawing.Color.Transparent
        Me.pnlFormularyCoverageHeading.BackgroundImage = CType(resources.GetObject("pnlFormularyCoverageHeading.BackgroundImage"), System.Drawing.Image)
        Me.pnlFormularyCoverageHeading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFormularyCoverageHeading.Controls.Add(Me.btnFormularyCovPnlClose)
        Me.pnlFormularyCoverageHeading.Controls.Add(Me.lblAlternativeDrugName)
        Me.pnlFormularyCoverageHeading.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFormularyCoverageHeading.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlFormularyCoverageHeading.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlFormularyCoverageHeading.Location = New System.Drawing.Point(0, 0)
        Me.pnlFormularyCoverageHeading.Name = "pnlFormularyCoverageHeading"
        Me.pnlFormularyCoverageHeading.Size = New System.Drawing.Size(402, 28)
        Me.pnlFormularyCoverageHeading.TabIndex = 25
        '
        'btnFormularyCovPnlClose
        '
        Me.btnFormularyCovPnlClose.BackColor = System.Drawing.Color.Transparent
        Me.btnFormularyCovPnlClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnFormularyCovPnlClose.FlatAppearance.BorderSize = 0
        Me.btnFormularyCovPnlClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFormularyCovPnlClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFormularyCovPnlClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFormularyCovPnlClose.Image = CType(resources.GetObject("btnFormularyCovPnlClose.Image"), System.Drawing.Image)
        Me.btnFormularyCovPnlClose.Location = New System.Drawing.Point(358, 0)
        Me.btnFormularyCovPnlClose.Name = "btnFormularyCovPnlClose"
        Me.btnFormularyCovPnlClose.Size = New System.Drawing.Size(44, 28)
        Me.btnFormularyCovPnlClose.TabIndex = 0
        Me.btnFormularyCovPnlClose.UseVisualStyleBackColor = False
        '
        'lblAlternativeDrugName
        '
        Me.lblAlternativeDrugName.AutoSize = True
        Me.lblAlternativeDrugName.BackColor = System.Drawing.Color.Transparent
        Me.lblAlternativeDrugName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlternativeDrugName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAlternativeDrugName.Location = New System.Drawing.Point(24, 1)
        Me.lblAlternativeDrugName.Name = "lblAlternativeDrugName"
        Me.lblAlternativeDrugName.Padding = New System.Windows.Forms.Padding(5)
        Me.lblAlternativeDrugName.Size = New System.Drawing.Size(61, 24)
        Me.lblAlternativeDrugName.TabIndex = 10
        Me.lblAlternativeDrugName.Text = "           "
        Me.lblAlternativeDrugName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmViewEARInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(934, 503)
        Me.Controls.Add(Me.pnlRefReqFlexGrid)
        Me.Controls.Add(Me.pnlToostrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewEARInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View EAR Information"
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlToostrip.ResumeLayout(False)
        Me.pnlToostrip.PerformLayout()
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRefReqFlexGrid.ResumeLayout(False)
        Me.pnlFormularyCoverage.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.pnlFormularyCoverageHeading.ResumeLayout(False)
        Me.pnlFormularyCoverageHeading.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ts_ViewButtons As System.Windows.Forms.ToolStrip
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlStrpViewEARRequestFile As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlToostrip As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Protected WithEvents _Flex As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlRefReqFlexGrid As System.Windows.Forms.Panel
    Friend WithEvents tlStrpViewEARResponseFile As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlFormularyCoverage As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Protected Friend WithEvents rchtxtbxEARRequestFile As gloDIControl.gloRichtextbox
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents pnlFormularyCoverageHeading As System.Windows.Forms.Panel
    Friend WithEvents btnFormularyCovPnlClose As System.Windows.Forms.Button
    Friend WithEvents lblAlternativeDrugName As System.Windows.Forms.Label
End Class
