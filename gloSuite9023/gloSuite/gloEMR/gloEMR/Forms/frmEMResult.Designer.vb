<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEMResult
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEMResult))
        Me.lblEMCodeCap = New System.Windows.Forms.Label
        Me.lblEMCode = New System.Windows.Forms.Label
        Me.lblHistoryLevelCap = New System.Windows.Forms.Label
        Me.lblHistoryLevel = New System.Windows.Forms.Label
        Me.lblExamLevelCap = New System.Windows.Forms.Label
        Me.lblExamLevel = New System.Windows.Forms.Label
        Me.lblMedicalComplexityLevelCap = New System.Windows.Forms.Label
        Me.lblMedicalComplexityLevel = New System.Windows.Forms.Label
        Me.lblCodeTypeCap = New System.Windows.Forms.Label
        Me.lblCodeType = New System.Windows.Forms.Label
        Me.rchtxtEMResult = New System.Windows.Forms.RichTextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.pnl_tlsEMResult = New System.Windows.Forms.Panel
        Me.tlsDictionary = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlb_Accept = New System.Windows.Forms.ToolStripButton
        Me.tlb_Reject = New System.Windows.Forms.ToolStripButton
        Me.tlb_Print = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label13 = New System.Windows.Forms.Label
        Me.btnEMClose = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnl_tlsEMResult.SuspendLayout()
        Me.tlsDictionary.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblEMCodeCap
        '
        Me.lblEMCodeCap.AutoSize = True
        Me.lblEMCodeCap.BackColor = System.Drawing.Color.Transparent
        Me.lblEMCodeCap.Location = New System.Drawing.Point(25, 12)
        Me.lblEMCodeCap.Name = "lblEMCodeCap"
        Me.lblEMCodeCap.Size = New System.Drawing.Size(137, 14)
        Me.lblEMCodeCap.TabIndex = 0
        Me.lblEMCodeCap.Text = "Evaluation Mgmt Code :"
        '
        'lblEMCode
        '
        Me.lblEMCode.AutoSize = True
        Me.lblEMCode.BackColor = System.Drawing.Color.Transparent
        Me.lblEMCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEMCode.Location = New System.Drawing.Point(168, 12)
        Me.lblEMCode.Name = "lblEMCode"
        Me.lblEMCode.Size = New System.Drawing.Size(38, 14)
        Me.lblEMCode.TabIndex = 0
        Me.lblEMCode.Text = "None"
        '
        'lblHistoryLevelCap
        '
        Me.lblHistoryLevelCap.AutoSize = True
        Me.lblHistoryLevelCap.BackColor = System.Drawing.Color.Transparent
        Me.lblHistoryLevelCap.Location = New System.Drawing.Point(78, 40)
        Me.lblHistoryLevelCap.Name = "lblHistoryLevelCap"
        Me.lblHistoryLevelCap.Size = New System.Drawing.Size(84, 14)
        Me.lblHistoryLevelCap.TabIndex = 0
        Me.lblHistoryLevelCap.Text = "History Level :"
        '
        'lblHistoryLevel
        '
        Me.lblHistoryLevel.AutoSize = True
        Me.lblHistoryLevel.BackColor = System.Drawing.Color.Transparent
        Me.lblHistoryLevel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHistoryLevel.Location = New System.Drawing.Point(168, 40)
        Me.lblHistoryLevel.Name = "lblHistoryLevel"
        Me.lblHistoryLevel.Size = New System.Drawing.Size(38, 14)
        Me.lblHistoryLevel.TabIndex = 0
        Me.lblHistoryLevel.Text = "None"
        '
        'lblExamLevelCap
        '
        Me.lblExamLevelCap.AutoSize = True
        Me.lblExamLevelCap.BackColor = System.Drawing.Color.Transparent
        Me.lblExamLevelCap.Location = New System.Drawing.Point(86, 68)
        Me.lblExamLevelCap.Name = "lblExamLevelCap"
        Me.lblExamLevelCap.Size = New System.Drawing.Size(76, 14)
        Me.lblExamLevelCap.TabIndex = 0
        Me.lblExamLevelCap.Text = "Exam Level :"
        '
        'lblExamLevel
        '
        Me.lblExamLevel.AutoSize = True
        Me.lblExamLevel.BackColor = System.Drawing.Color.Transparent
        Me.lblExamLevel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExamLevel.Location = New System.Drawing.Point(168, 68)
        Me.lblExamLevel.Name = "lblExamLevel"
        Me.lblExamLevel.Size = New System.Drawing.Size(38, 14)
        Me.lblExamLevel.TabIndex = 0
        Me.lblExamLevel.Text = "None"
        '
        'lblMedicalComplexityLevelCap
        '
        Me.lblMedicalComplexityLevelCap.AutoSize = True
        Me.lblMedicalComplexityLevelCap.BackColor = System.Drawing.Color.Transparent
        Me.lblMedicalComplexityLevelCap.Location = New System.Drawing.Point(9, 96)
        Me.lblMedicalComplexityLevelCap.Name = "lblMedicalComplexityLevelCap"
        Me.lblMedicalComplexityLevelCap.Size = New System.Drawing.Size(153, 14)
        Me.lblMedicalComplexityLevelCap.TabIndex = 0
        Me.lblMedicalComplexityLevelCap.Text = "Medical Complexity Level : "
        '
        'lblMedicalComplexityLevel
        '
        Me.lblMedicalComplexityLevel.AutoSize = True
        Me.lblMedicalComplexityLevel.BackColor = System.Drawing.Color.Transparent
        Me.lblMedicalComplexityLevel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMedicalComplexityLevel.Location = New System.Drawing.Point(168, 96)
        Me.lblMedicalComplexityLevel.Name = "lblMedicalComplexityLevel"
        Me.lblMedicalComplexityLevel.Size = New System.Drawing.Size(38, 14)
        Me.lblMedicalComplexityLevel.TabIndex = 0
        Me.lblMedicalComplexityLevel.Text = "None"
        '
        'lblCodeTypeCap
        '
        Me.lblCodeTypeCap.AutoSize = True
        Me.lblCodeTypeCap.BackColor = System.Drawing.Color.Transparent
        Me.lblCodeTypeCap.Location = New System.Drawing.Point(317, 12)
        Me.lblCodeTypeCap.Name = "lblCodeTypeCap"
        Me.lblCodeTypeCap.Size = New System.Drawing.Size(75, 14)
        Me.lblCodeTypeCap.TabIndex = 0
        Me.lblCodeTypeCap.Text = "Code Type :"
        '
        'lblCodeType
        '
        Me.lblCodeType.BackColor = System.Drawing.Color.Transparent
        Me.lblCodeType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodeType.Location = New System.Drawing.Point(398, 12)
        Me.lblCodeType.Name = "lblCodeType"
        Me.lblCodeType.Size = New System.Drawing.Size(295, 42)
        Me.lblCodeType.TabIndex = 0
        Me.lblCodeType.Text = "None"
        '
        'rchtxtEMResult
        '
        Me.rchtxtEMResult.BackColor = System.Drawing.Color.White
        Me.rchtxtEMResult.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rchtxtEMResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rchtxtEMResult.ForeColor = System.Drawing.Color.Black
        Me.rchtxtEMResult.Location = New System.Drawing.Point(8, 3)
        Me.rchtxtEMResult.Name = "rchtxtEMResult"
        Me.rchtxtEMResult.ReadOnly = True
        Me.rchtxtEMResult.Size = New System.Drawing.Size(696, 230)
        Me.rchtxtEMResult.TabIndex = 1
        Me.rchtxtEMResult.Text = ""
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lblCodeType)
        Me.Panel1.Controls.Add(Me.lblCodeTypeCap)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblMedicalComplexityLevel)
        Me.Panel1.Controls.Add(Me.lblHistoryLevelCap)
        Me.Panel1.Controls.Add(Me.lblMedicalComplexityLevelCap)
        Me.Panel1.Controls.Add(Me.lblEMCodeCap)
        Me.Panel1.Controls.Add(Me.lblExamLevel)
        Me.Panel1.Controls.Add(Me.lblEMCode)
        Me.Panel1.Controls.Add(Me.lblExamLevelCap)
        Me.Panel1.Controls.Add(Me.lblHistoryLevel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(702, 122)
        Me.Panel1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Location = New System.Drawing.Point(1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(700, 1)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label1.Location = New System.Drawing.Point(701, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 121)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(1, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(701, 1)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 122)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "label4"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rchtxtEMResult)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 182)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(708, 237)
        Me.Panel2.TabIndex = 2
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.White
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Location = New System.Drawing.Point(8, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(696, 2)
        Me.Label15.TabIndex = 25
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.White
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(4, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(4, 232)
        Me.Label14.TabIndex = 24
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(700, 1)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "label1"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Location = New System.Drawing.Point(704, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 233)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(4, 233)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(701, 1)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "label1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 234)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "label4"
        '
        'pnl_tlsEMResult
        '
        Me.pnl_tlsEMResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsEMResult.Controls.Add(Me.tlsDictionary)
        Me.pnl_tlsEMResult.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsEMResult.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsEMResult.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_tlsEMResult.Name = "pnl_tlsEMResult"
        Me.pnl_tlsEMResult.Size = New System.Drawing.Size(708, 54)
        Me.pnl_tlsEMResult.TabIndex = 6
        '
        'tlsDictionary
        '
        Me.tlsDictionary.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tlsDictionary.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDictionary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDictionary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDictionary.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDictionary.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_Accept, Me.tlb_Reject, Me.tlb_Print, Me.ToolStripButton3})
        Me.tlsDictionary.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDictionary.Location = New System.Drawing.Point(0, 0)
        Me.tlsDictionary.Name = "tlsDictionary"
        Me.tlsDictionary.Size = New System.Drawing.Size(708, 53)
        Me.tlsDictionary.TabIndex = 0
        Me.tlsDictionary.Text = "toolStrip1"
        '
        'tlb_Accept
        '
        Me.tlb_Accept.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Accept.Image = CType(resources.GetObject("tlb_Accept.Image"), System.Drawing.Image)
        Me.tlb_Accept.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Accept.Name = "tlb_Accept"
        Me.tlb_Accept.Size = New System.Drawing.Size(53, 50)
        Me.tlb_Accept.Tag = "Accept"
        Me.tlb_Accept.Text = "&Accept"
        Me.tlb_Accept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlb_Reject
        '
        Me.tlb_Reject.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Reject.Image = CType(resources.GetObject("tlb_Reject.Image"), System.Drawing.Image)
        Me.tlb_Reject.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Reject.Name = "tlb_Reject"
        Me.tlb_Reject.Size = New System.Drawing.Size(50, 50)
        Me.tlb_Reject.Tag = "Reject"
        Me.tlb_Reject.Text = "&Reject"
        Me.tlb_Reject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlb_Print
        '
        Me.tlb_Print.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Print.Image = CType(resources.GetObject("tlb_Print.Image"), System.Drawing.Image)
        Me.tlb_Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Print.Name = "tlb_Print"
        Me.tlb_Print.Size = New System.Drawing.Size(41, 50)
        Me.tlb_Print.Tag = "Print"
        Me.tlb_Print.Text = "&Print"
        Me.tlb_Print.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.tlb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Print.Visible = False
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(43, 50)
        Me.ToolStripButton3.Tag = "Close"
        Me.ToolStripButton3.Text = "&Close"
        Me.ToolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton3.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Location = New System.Drawing.Point(12, 76)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(708, 24)
        Me.Panel3.TabIndex = 7
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.btnEMClose)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(708, 24)
        Me.Panel4.TabIndex = 0
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(1, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label13.Size = New System.Drawing.Size(72, 20)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "  EM Code"
        '
        'btnEMClose
        '
        Me.btnEMClose.BackColor = System.Drawing.Color.Transparent
        Me.btnEMClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnEMClose.FlatAppearance.BorderSize = 0
        Me.btnEMClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnEMClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnEMClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEMClose.Image = CType(resources.GetObject("btnEMClose.Image"), System.Drawing.Image)
        Me.btnEMClose.Location = New System.Drawing.Point(682, 1)
        Me.btnEMClose.Name = "btnEMClose"
        Me.btnEMClose.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.btnEMClose.Size = New System.Drawing.Size(25, 22)
        Me.btnEMClose.TabIndex = 28
        Me.btnEMClose.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(707, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 22)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(1, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(707, 1)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "label1"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Location = New System.Drawing.Point(1, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(707, 1)
        Me.Label10.TabIndex = 25
        Me.Label10.Text = "label1"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 24)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "label4"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 54)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel5.Size = New System.Drawing.Size(708, 128)
        Me.Panel5.TabIndex = 8
        '
        'frmEMResult
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(708, 419)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.pnl_tlsEMResult)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEMResult"
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EM Result"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.pnl_tlsEMResult.ResumeLayout(False)
        Me.pnl_tlsEMResult.PerformLayout()
        Me.tlsDictionary.ResumeLayout(False)
        Me.tlsDictionary.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblEMCodeCap As System.Windows.Forms.Label
    Friend WithEvents lblEMCode As System.Windows.Forms.Label
    Friend WithEvents lblHistoryLevelCap As System.Windows.Forms.Label
    Friend WithEvents lblHistoryLevel As System.Windows.Forms.Label
    Friend WithEvents lblExamLevelCap As System.Windows.Forms.Label
    Friend WithEvents lblExamLevel As System.Windows.Forms.Label
    Friend WithEvents lblMedicalComplexityLevelCap As System.Windows.Forms.Label
    Friend WithEvents lblMedicalComplexityLevel As System.Windows.Forms.Label
    Friend WithEvents lblCodeTypeCap As System.Windows.Forms.Label
    Friend WithEvents lblCodeType As System.Windows.Forms.Label
    Friend WithEvents rchtxtEMResult As System.Windows.Forms.RichTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents pnl_tlsEMResult As System.Windows.Forms.Panel
    Private WithEvents tlsDictionary As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlb_Accept As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnEMClose As System.Windows.Forms.Button
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents tlb_Reject As System.Windows.Forms.ToolStripButton
    Private WithEvents tlb_Print As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
End Class
