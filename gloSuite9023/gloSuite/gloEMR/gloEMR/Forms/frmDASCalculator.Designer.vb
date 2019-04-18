<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDASCalculator
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDASCalculator))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.chk_T_R_Shoulder = New System.Windows.Forms.CheckBox()
        Me.lblTenderName = New System.Windows.Forms.Label()
        Me.lblSwollenName = New System.Windows.Forms.Label()
        Me.rbtnESR = New System.Windows.Forms.RadioButton()
        Me.rbtnCRP = New System.Windows.Forms.RadioButton()
        Me.txtESR = New System.Windows.Forms.TextBox()
        Me.txtCRP = New System.Windows.Forms.TextBox()
        Me.chkPainScale = New System.Windows.Forms.CheckBox()
        Me.pnlPainScale = New System.Windows.Forms.Panel()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.Label118 = New System.Windows.Forms.Label()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.trbPainScale = New System.Windows.Forms.TrackBar()
        Me.btnCalculate = New System.Windows.Forms.Button()
        Me.txtCalculatedDAS = New System.Windows.Forms.TextBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.pnlHide = New System.Windows.Forms.Panel()
        Me.pb_HideImage = New System.Windows.Forms.PictureBox()
        Me.tblStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_Ok_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.lblCalculationLabel = New System.Windows.Forms.Label()
        Me.lblCalculationName = New System.Windows.Forms.Label()
        Me.lblCalculationFormula = New System.Windows.Forms.Label()
        Me.lblESRUnit = New System.Windows.Forms.Label()
        Me.lblCRPUnits = New System.Windows.Forms.Label()
        Me.lblCalculatedDAS = New System.Windows.Forms.Label()
        Me.lblSwollenCount = New System.Windows.Forms.Label()
        Me.txtSwollenCount = New System.Windows.Forms.TextBox()
        Me.txtTenderCount = New System.Windows.Forms.TextBox()
        Me.lblTenderCount = New System.Windows.Forms.Label()
        Me.rbtnUseDiagram = New System.Windows.Forms.RadioButton()
        Me.rbtnJointCount = New System.Windows.Forms.RadioButton()
        Me.txtPainScore = New System.Windows.Forms.TextBox()
        Me.chk_T_R_LittleDown = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_RingDown = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_Shoulder = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_LittleDown = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_RingDown = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_MiddleDown = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_IndexDown = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_ThunbDown = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_ThunbUp = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_LittleUp = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_RingUp = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_MiddleUp = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_IndexUp = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_Elbow = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_Wrist = New System.Windows.Forms.CheckBox()
        Me.chk_S_R_Knee = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_Knee = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_Shoulder = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_Elbow = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_Wrist = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_ThunbDown = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_ThunbUp = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_IndexUp = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_MiddleUp = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_IndexDown = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_MiddleDown = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_RingUp = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_RingDown1 = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_LittleDown = New System.Windows.Forms.CheckBox()
        Me.chk_S_L_LittleUp = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_Shoulder = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_Elbow = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_Elbow = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_Wrist = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_Wrist = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_Knee = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_Knee = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_MiddleDown = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_IndexDown = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_ThunbDown = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_ThunbUp = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_LittleUp = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_RingUp = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_MiddleUp = New System.Windows.Forms.CheckBox()
        Me.chk_T_R_IndexUp = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_LittleDown = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_RingDown = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_MiddleDown = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_IndexDown = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_ThunbDown = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_ThunbUp = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_IndexUp = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_MiddleUp = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_RingUp = New System.Windows.Forms.CheckBox()
        Me.chk_T_L_LittleUp = New System.Windows.Forms.CheckBox()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlImages = New System.Windows.Forms.Panel()
        Me.pnlImgHeader = New System.Windows.Forms.Panel()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Left = New System.Windows.Forms.Panel()
        Me.Right = New System.Windows.Forms.Panel()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPainScale.SuspendLayout()
        CType(Me.trbPainScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHide.SuspendLayout()
        CType(Me.pb_HideImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tblStrip.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlImages.SuspendLayout()
        Me.pnlImgHeader.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(961, 714)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'chk_T_R_Shoulder
        '
        Me.chk_T_R_Shoulder.BackColor = System.Drawing.Color.White
        Me.chk_T_R_Shoulder.BackgroundImage = CType(resources.GetObject("chk_T_R_Shoulder.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_Shoulder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_Shoulder.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_Shoulder.Location = New System.Drawing.Point(134, 141)
        Me.chk_T_R_Shoulder.Name = "chk_T_R_Shoulder"
        Me.chk_T_R_Shoulder.Padding = New System.Windows.Forms.Padding(10)
        Me.chk_T_R_Shoulder.Size = New System.Drawing.Size(34, 31)
        Me.chk_T_R_Shoulder.TabIndex = 1
        Me.chk_T_R_Shoulder.UseVisualStyleBackColor = False
        '
        'lblTenderName
        '
        Me.lblTenderName.AutoSize = True
        Me.lblTenderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTenderName.Location = New System.Drawing.Point(172, 3)
        Me.lblTenderName.Name = "lblTenderName"
        Me.lblTenderName.Size = New System.Drawing.Size(139, 14)
        Me.lblTenderName.TabIndex = 3
        Me.lblTenderName.Text = "Tender/Painful Joints"
        '
        'lblSwollenName
        '
        Me.lblSwollenName.AutoSize = True
        Me.lblSwollenName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSwollenName.Location = New System.Drawing.Point(690, 3)
        Me.lblSwollenName.Name = "lblSwollenName"
        Me.lblSwollenName.Size = New System.Drawing.Size(96, 14)
        Me.lblSwollenName.TabIndex = 4
        Me.lblSwollenName.Text = "Swollen Joints"
        '
        'rbtnESR
        '
        Me.rbtnESR.AutoSize = True
        Me.rbtnESR.Checked = True
        Me.rbtnESR.Location = New System.Drawing.Point(8, 22)
        Me.rbtnESR.Name = "rbtnESR"
        Me.rbtnESR.Size = New System.Drawing.Size(78, 18)
        Me.rbtnESR.TabIndex = 9
        Me.rbtnESR.TabStop = True
        Me.rbtnESR.Text = "Use ESR :"
        Me.rbtnESR.UseVisualStyleBackColor = True
        '
        'rbtnCRP
        '
        Me.rbtnCRP.AutoSize = True
        Me.rbtnCRP.Location = New System.Drawing.Point(8, 48)
        Me.rbtnCRP.Name = "rbtnCRP"
        Me.rbtnCRP.Size = New System.Drawing.Size(78, 18)
        Me.rbtnCRP.TabIndex = 10
        Me.rbtnCRP.TabStop = True
        Me.rbtnCRP.Text = "Use CRP :"
        Me.rbtnCRP.UseVisualStyleBackColor = True
        '
        'txtESR
        '
        Me.txtESR.Location = New System.Drawing.Point(90, 20)
        Me.txtESR.Name = "txtESR"
        Me.txtESR.Size = New System.Drawing.Size(45, 22)
        Me.txtESR.TabIndex = 11
        Me.txtESR.Text = "1"
        Me.txtESR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCRP
        '
        Me.txtCRP.Enabled = False
        Me.txtCRP.Location = New System.Drawing.Point(90, 46)
        Me.txtCRP.Name = "txtCRP"
        Me.txtCRP.Size = New System.Drawing.Size(45, 22)
        Me.txtCRP.TabIndex = 12
        Me.txtCRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkPainScale
        '
        Me.chkPainScale.AutoSize = True
        Me.chkPainScale.Location = New System.Drawing.Point(9, 24)
        Me.chkPainScale.Margin = New System.Windows.Forms.Padding(0)
        Me.chkPainScale.Name = "chkPainScale"
        Me.chkPainScale.Size = New System.Drawing.Size(15, 14)
        Me.chkPainScale.TabIndex = 1
        Me.chkPainScale.UseVisualStyleBackColor = True
        '
        'pnlPainScale
        '
        Me.pnlPainScale.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPainScale.Controls.Add(Me.Label100)
        Me.pnlPainScale.Controls.Add(Me.Label101)
        Me.pnlPainScale.Controls.Add(Me.Label102)
        Me.pnlPainScale.Controls.Add(Me.Label103)
        Me.pnlPainScale.Controls.Add(Me.Label104)
        Me.pnlPainScale.Controls.Add(Me.Label105)
        Me.pnlPainScale.Controls.Add(Me.Label106)
        Me.pnlPainScale.Controls.Add(Me.Label107)
        Me.pnlPainScale.Controls.Add(Me.Label108)
        Me.pnlPainScale.Controls.Add(Me.Label109)
        Me.pnlPainScale.Controls.Add(Me.Label110)
        Me.pnlPainScale.Controls.Add(Me.Label111)
        Me.pnlPainScale.Controls.Add(Me.Label112)
        Me.pnlPainScale.Controls.Add(Me.Label113)
        Me.pnlPainScale.Controls.Add(Me.Label114)
        Me.pnlPainScale.Controls.Add(Me.Label115)
        Me.pnlPainScale.Controls.Add(Me.Label116)
        Me.pnlPainScale.Controls.Add(Me.Label117)
        Me.pnlPainScale.Controls.Add(Me.Label118)
        Me.pnlPainScale.Controls.Add(Me.Label119)
        Me.pnlPainScale.Controls.Add(Me.Label120)
        Me.pnlPainScale.Controls.Add(Me.trbPainScale)
        Me.pnlPainScale.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlPainScale.Location = New System.Drawing.Point(117, 1)
        Me.pnlPainScale.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlPainScale.Name = "pnlPainScale"
        Me.pnlPainScale.Padding = New System.Windows.Forms.Padding(5, 5, 5, 3)
        Me.pnlPainScale.Size = New System.Drawing.Size(316, 87)
        Me.pnlPainScale.TabIndex = 0
        Me.pnlPainScale.Visible = False
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label100.Location = New System.Drawing.Point(6, 83)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(304, 1)
        Me.Label100.TabIndex = 0
        Me.Label100.Text = "label2"
        '
        'Label101
        '
        Me.Label101.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label101.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label101.Location = New System.Drawing.Point(5, 6)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(1, 78)
        Me.Label101.TabIndex = 0
        Me.Label101.Text = "label4"
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label102.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label102.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label102.Location = New System.Drawing.Point(310, 6)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(1, 78)
        Me.Label102.TabIndex = 0
        Me.Label102.Text = "label3"
        '
        'Label103
        '
        Me.Label103.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label103.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label103.Location = New System.Drawing.Point(5, 5)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(306, 1)
        Me.Label103.TabIndex = 0
        Me.Label103.Text = "label1"
        '
        'Label104
        '
        Me.Label104.AutoSize = True
        Me.Label104.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label104.Location = New System.Drawing.Point(285, 71)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(26, 12)
        Me.Label104.TabIndex = 0
        Me.Label104.Text = "100"
        '
        'Label105
        '
        Me.Label105.AutoSize = True
        Me.Label105.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label105.Location = New System.Drawing.Point(258, 71)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(19, 12)
        Me.Label105.TabIndex = 0
        Me.Label105.Text = "90"
        '
        'Label106
        '
        Me.Label106.AutoSize = True
        Me.Label106.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label106.Location = New System.Drawing.Point(231, 71)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(19, 12)
        Me.Label106.TabIndex = 0
        Me.Label106.Text = "80"
        '
        'Label107
        '
        Me.Label107.AutoSize = True
        Me.Label107.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label107.Location = New System.Drawing.Point(205, 71)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(19, 12)
        Me.Label107.TabIndex = 0
        Me.Label107.Text = "70"
        '
        'Label108
        '
        Me.Label108.AutoSize = True
        Me.Label108.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label108.Location = New System.Drawing.Point(178, 71)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(19, 12)
        Me.Label108.TabIndex = 0
        Me.Label108.Text = "60"
        '
        'Label109
        '
        Me.Label109.AutoSize = True
        Me.Label109.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label109.Location = New System.Drawing.Point(151, 71)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(19, 12)
        Me.Label109.TabIndex = 0
        Me.Label109.Text = "50"
        '
        'Label110
        '
        Me.Label110.AutoSize = True
        Me.Label110.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label110.Location = New System.Drawing.Point(125, 71)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(19, 12)
        Me.Label110.TabIndex = 0
        Me.Label110.Text = "40"
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label111.Location = New System.Drawing.Point(97, 71)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(19, 12)
        Me.Label111.TabIndex = 0
        Me.Label111.Text = "30"
        '
        'Label112
        '
        Me.Label112.AutoSize = True
        Me.Label112.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.Location = New System.Drawing.Point(70, 71)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(19, 12)
        Me.Label112.TabIndex = 0
        Me.Label112.Text = "20"
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label113.Location = New System.Drawing.Point(44, 71)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(19, 12)
        Me.Label113.TabIndex = 0
        Me.Label113.Text = "10"
        '
        'Label114
        '
        Me.Label114.AutoSize = True
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114.Location = New System.Drawing.Point(258, 23)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(23, 11)
        Me.Label114.TabIndex = 0
        Me.Label114.Text = "Pain"
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label115.Location = New System.Drawing.Point(241, 11)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(52, 11)
        Me.Label115.TabIndex = 0
        Me.Label115.Text = "Unbearable"
        '
        'Label116
        '
        Me.Label116.AutoSize = True
        Me.Label116.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label116.Location = New System.Drawing.Point(139, 23)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(23, 11)
        Me.Label116.TabIndex = 0
        Me.Label116.Text = "Pain"
        '
        'Label117
        '
        Me.Label117.AutoSize = True
        Me.Label117.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label117.Location = New System.Drawing.Point(126, 11)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(44, 11)
        Me.Label117.TabIndex = 0
        Me.Label117.Text = "Moderate"
        '
        'Label118
        '
        Me.Label118.AutoSize = True
        Me.Label118.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label118.Location = New System.Drawing.Point(9, 23)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(23, 11)
        Me.Label118.TabIndex = 0
        Me.Label118.Text = "Pain"
        '
        'Label119
        '
        Me.Label119.AutoSize = True
        Me.Label119.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label119.Location = New System.Drawing.Point(12, 11)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(17, 11)
        Me.Label119.TabIndex = 0
        Me.Label119.Text = "No"
        '
        'Label120
        '
        Me.Label120.AutoSize = True
        Me.Label120.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label120.Location = New System.Drawing.Point(17, 71)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(12, 12)
        Me.Label120.TabIndex = 0
        Me.Label120.Text = "0"
        '
        'trbPainScale
        '
        Me.trbPainScale.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.trbPainScale.LargeChange = 1
        Me.trbPainScale.Location = New System.Drawing.Point(3, 33)
        Me.trbPainScale.Maximum = 100
        Me.trbPainScale.Name = "trbPainScale"
        Me.trbPainScale.Size = New System.Drawing.Size(301, 45)
        Me.trbPainScale.TabIndex = 0
        '
        'btnCalculate
        '
        Me.btnCalculate.Location = New System.Drawing.Point(276, 56)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(10, 22)
        Me.btnCalculate.TabIndex = 14
        Me.btnCalculate.Text = "Calculate"
        Me.btnCalculate.UseVisualStyleBackColor = True
        Me.btnCalculate.Visible = False
        '
        'txtCalculatedDAS
        '
        Me.txtCalculatedDAS.Enabled = False
        Me.txtCalculatedDAS.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCalculatedDAS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtCalculatedDAS.Location = New System.Drawing.Point(144, 54)
        Me.txtCalculatedDAS.Name = "txtCalculatedDAS"
        Me.txtCalculatedDAS.Size = New System.Drawing.Size(71, 27)
        Me.txtCalculatedDAS.TabIndex = 15
        Me.txtCalculatedDAS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(775, 444)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox4.TabIndex = 66
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'pnlHide
        '
        Me.pnlHide.AutoScroll = True
        Me.pnlHide.BackColor = System.Drawing.Color.White
        Me.pnlHide.Controls.Add(Me.pb_HideImage)
        Me.pnlHide.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlHide.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlHide.Location = New System.Drawing.Point(0, 22)
        Me.pnlHide.Name = "pnlHide"
        Me.pnlHide.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlHide.Size = New System.Drawing.Size(968, 720)
        Me.pnlHide.TabIndex = 70
        Me.pnlHide.Visible = False
        '
        'pb_HideImage
        '
        Me.pb_HideImage.Image = CType(resources.GetObject("pb_HideImage.Image"), System.Drawing.Image)
        Me.pb_HideImage.Location = New System.Drawing.Point(4, 4)
        Me.pb_HideImage.Name = "pb_HideImage"
        Me.pb_HideImage.Size = New System.Drawing.Size(960, 714)
        Me.pb_HideImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_HideImage.TabIndex = 1
        Me.pb_HideImage.TabStop = False
        '
        'tblStrip
        '
        Me.tblStrip.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Ok_32, Me.tblbtn_Close_32})
        Me.tblStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip.Name = "tblStrip"
        Me.tblStrip.Size = New System.Drawing.Size(1084, 56)
        Me.tblStrip.TabIndex = 72
        Me.tblStrip.Text = "ToolStrip1"
        '
        'tblbtn_Ok_32
        '
        Me.tblbtn_Ok_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Ok_32.Image = CType(resources.GetObject("tblbtn_Ok_32.Image"), System.Drawing.Image)
        Me.tblbtn_Ok_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Ok_32.Name = "tblbtn_Ok_32"
        Me.tblbtn_Ok_32.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Ok_32.Tag = "Ok"
        Me.tblbtn_Ok_32.Text = "&Save&&Cls"
        Me.tblbtn_Ok_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Ok_32.ToolTipText = "Save and Close"
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Tag = "Close"
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tblStrip)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1084, 56)
        Me.pnlToolStrip.TabIndex = 0
        '
        'lblCalculationLabel
        '
        Me.lblCalculationLabel.AutoSize = True
        Me.lblCalculationLabel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalculationLabel.Location = New System.Drawing.Point(9, 8)
        Me.lblCalculationLabel.Name = "lblCalculationLabel"
        Me.lblCalculationLabel.Size = New System.Drawing.Size(133, 14)
        Me.lblCalculationLabel.TabIndex = 72
        Me.lblCalculationLabel.Text = "DAS-28 Calculation :"
        '
        'lblCalculationName
        '
        Me.lblCalculationName.AutoSize = True
        Me.lblCalculationName.Location = New System.Drawing.Point(143, 8)
        Me.lblCalculationName.Name = "lblCalculationName"
        Me.lblCalculationName.Size = New System.Drawing.Size(166, 14)
        Me.lblCalculationName.TabIndex = 73
        Me.lblCalculationName.Text = "using ESR without pain score"
        '
        'lblCalculationFormula
        '
        Me.lblCalculationFormula.AutoSize = True
        Me.lblCalculationFormula.Location = New System.Drawing.Point(9, 30)
        Me.lblCalculationFormula.Name = "lblCalculationFormula"
        Me.lblCalculationFormula.Size = New System.Drawing.Size(378, 14)
        Me.lblCalculationFormula.TabIndex = 74
        Me.lblCalculationFormula.Text = "(0.56*sqrt(pjc28) + 0.28*sqrt(sjc28) + 0.70*ln(ESR))*1.08 + 0.16"
        '
        'lblESRUnit
        '
        Me.lblESRUnit.AutoSize = True
        Me.lblESRUnit.Location = New System.Drawing.Point(138, 24)
        Me.lblESRUnit.Name = "lblESRUnit"
        Me.lblESRUnit.Size = New System.Drawing.Size(57, 14)
        Me.lblESRUnit.TabIndex = 14
        Me.lblESRUnit.Text = "mm/hour"
        '
        'lblCRPUnits
        '
        Me.lblCRPUnits.AutoSize = True
        Me.lblCRPUnits.Location = New System.Drawing.Point(138, 50)
        Me.lblCRPUnits.Name = "lblCRPUnits"
        Me.lblCRPUnits.Size = New System.Drawing.Size(35, 14)
        Me.lblCRPUnits.TabIndex = 13
        Me.lblCRPUnits.Text = "mg/L"
        '
        'lblCalculatedDAS
        '
        Me.lblCalculatedDAS.AutoSize = True
        Me.lblCalculatedDAS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalculatedDAS.Location = New System.Drawing.Point(11, 61)
        Me.lblCalculatedDAS.Name = "lblCalculatedDAS"
        Me.lblCalculatedDAS.Size = New System.Drawing.Size(129, 14)
        Me.lblCalculatedDAS.TabIndex = 76
        Me.lblCalculatedDAS.Text = "Calculated DAS-28 :"
        '
        'lblSwollenCount
        '
        Me.lblSwollenCount.AutoSize = True
        Me.lblSwollenCount.Location = New System.Drawing.Point(857, 10)
        Me.lblSwollenCount.Name = "lblSwollenCount"
        Me.lblSwollenCount.Size = New System.Drawing.Size(124, 14)
        Me.lblSwollenCount.TabIndex = 7
        Me.lblSwollenCount.Text = "Swollen Joint Count :"
        '
        'txtSwollenCount
        '
        Me.txtSwollenCount.Enabled = False
        Me.txtSwollenCount.Location = New System.Drawing.Point(984, 6)
        Me.txtSwollenCount.Name = "txtSwollenCount"
        Me.txtSwollenCount.Size = New System.Drawing.Size(40, 22)
        Me.txtSwollenCount.TabIndex = 8
        Me.txtSwollenCount.Text = "0"
        Me.txtSwollenCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTenderCount
        '
        Me.txtTenderCount.Enabled = False
        Me.txtTenderCount.Location = New System.Drawing.Point(805, 6)
        Me.txtTenderCount.Name = "txtTenderCount"
        Me.txtTenderCount.Size = New System.Drawing.Size(40, 22)
        Me.txtTenderCount.TabIndex = 6
        Me.txtTenderCount.Text = "0"
        Me.txtTenderCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTenderCount
        '
        Me.lblTenderCount.AutoSize = True
        Me.lblTenderCount.Location = New System.Drawing.Point(639, 10)
        Me.lblTenderCount.Name = "lblTenderCount"
        Me.lblTenderCount.Size = New System.Drawing.Size(162, 14)
        Me.lblTenderCount.TabIndex = 5
        Me.lblTenderCount.Text = "Tender/Painful Joint Count :"
        '
        'rbtnUseDiagram
        '
        Me.rbtnUseDiagram.AutoSize = True
        Me.rbtnUseDiagram.Checked = True
        Me.rbtnUseDiagram.Location = New System.Drawing.Point(60, 8)
        Me.rbtnUseDiagram.Name = "rbtnUseDiagram"
        Me.rbtnUseDiagram.Size = New System.Drawing.Size(92, 18)
        Me.rbtnUseDiagram.TabIndex = 0
        Me.rbtnUseDiagram.TabStop = True
        Me.rbtnUseDiagram.Text = "Use Diagram"
        Me.rbtnUseDiagram.UseVisualStyleBackColor = True
        '
        'rbtnJointCount
        '
        Me.rbtnJointCount.AutoSize = True
        Me.rbtnJointCount.Location = New System.Drawing.Point(158, 8)
        Me.rbtnJointCount.Name = "rbtnJointCount"
        Me.rbtnJointCount.Size = New System.Drawing.Size(127, 18)
        Me.rbtnJointCount.TabIndex = 1
        Me.rbtnJointCount.TabStop = True
        Me.rbtnJointCount.Text = "Enter Joint Counts"
        Me.rbtnJointCount.UseVisualStyleBackColor = True
        '
        'txtPainScore
        '
        Me.txtPainScore.Location = New System.Drawing.Point(8, 47)
        Me.txtPainScore.Name = "txtPainScore"
        Me.txtPainScore.Size = New System.Drawing.Size(46, 22)
        Me.txtPainScore.TabIndex = 13
        Me.txtPainScore.Text = "0"
        Me.txtPainScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPainScore.Visible = False
        '
        'chk_T_R_LittleDown
        '
        Me.chk_T_R_LittleDown.BackColor = System.Drawing.Color.White
        Me.chk_T_R_LittleDown.BackgroundImage = CType(resources.GetObject("chk_T_R_LittleDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_LittleDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_LittleDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_LittleDown.Location = New System.Drawing.Point(94, 446)
        Me.chk_T_R_LittleDown.Name = "chk_T_R_LittleDown"
        Me.chk_T_R_LittleDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_R_LittleDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_R_LittleDown.TabIndex = 78
        Me.chk_T_R_LittleDown.UseVisualStyleBackColor = False
        '
        'chk_T_R_RingDown
        '
        Me.chk_T_R_RingDown.BackColor = System.Drawing.Color.White
        Me.chk_T_R_RingDown.BackgroundImage = CType(resources.GetObject("chk_T_R_RingDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_RingDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_RingDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_RingDown.Location = New System.Drawing.Point(71, 439)
        Me.chk_T_R_RingDown.Name = "chk_T_R_RingDown"
        Me.chk_T_R_RingDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_R_RingDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_R_RingDown.TabIndex = 79
        Me.chk_T_R_RingDown.UseVisualStyleBackColor = False
        '
        'chk_S_R_Shoulder
        '
        Me.chk_S_R_Shoulder.BackColor = System.Drawing.Color.White
        Me.chk_S_R_Shoulder.BackgroundImage = CType(resources.GetObject("chk_S_R_Shoulder.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_Shoulder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_Shoulder.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_Shoulder.Location = New System.Drawing.Point(636, 141)
        Me.chk_S_R_Shoulder.Name = "chk_S_R_Shoulder"
        Me.chk_S_R_Shoulder.Padding = New System.Windows.Forms.Padding(11)
        Me.chk_S_R_Shoulder.Size = New System.Drawing.Size(34, 31)
        Me.chk_S_R_Shoulder.TabIndex = 80
        Me.chk_S_R_Shoulder.UseVisualStyleBackColor = False
        '
        'chk_S_R_LittleDown
        '
        Me.chk_S_R_LittleDown.BackColor = System.Drawing.Color.White
        Me.chk_S_R_LittleDown.BackgroundImage = CType(resources.GetObject("chk_S_R_LittleDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_LittleDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_LittleDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_LittleDown.Location = New System.Drawing.Point(595, 445)
        Me.chk_S_R_LittleDown.Name = "chk_S_R_LittleDown"
        Me.chk_S_R_LittleDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_R_LittleDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_R_LittleDown.TabIndex = 81
        Me.chk_S_R_LittleDown.UseVisualStyleBackColor = False
        '
        'chk_S_R_RingDown
        '
        Me.chk_S_R_RingDown.BackColor = System.Drawing.Color.White
        Me.chk_S_R_RingDown.BackgroundImage = CType(resources.GetObject("chk_S_R_RingDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_RingDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_RingDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_RingDown.Location = New System.Drawing.Point(573, 439)
        Me.chk_S_R_RingDown.Name = "chk_S_R_RingDown"
        Me.chk_S_R_RingDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_R_RingDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_R_RingDown.TabIndex = 82
        Me.chk_S_R_RingDown.UseVisualStyleBackColor = False
        '
        'chk_S_R_MiddleDown
        '
        Me.chk_S_R_MiddleDown.BackColor = System.Drawing.Color.White
        Me.chk_S_R_MiddleDown.BackgroundImage = CType(resources.GetObject("chk_S_R_MiddleDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_MiddleDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_MiddleDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_MiddleDown.Location = New System.Drawing.Point(551, 431)
        Me.chk_S_R_MiddleDown.Name = "chk_S_R_MiddleDown"
        Me.chk_S_R_MiddleDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_R_MiddleDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_R_MiddleDown.TabIndex = 83
        Me.chk_S_R_MiddleDown.UseVisualStyleBackColor = False
        '
        'chk_S_R_IndexDown
        '
        Me.chk_S_R_IndexDown.BackColor = System.Drawing.Color.White
        Me.chk_S_R_IndexDown.BackgroundImage = CType(resources.GetObject("chk_S_R_IndexDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_IndexDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_IndexDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_IndexDown.Location = New System.Drawing.Point(529, 422)
        Me.chk_S_R_IndexDown.Name = "chk_S_R_IndexDown"
        Me.chk_S_R_IndexDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_R_IndexDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_R_IndexDown.TabIndex = 84
        Me.chk_S_R_IndexDown.UseVisualStyleBackColor = False
        '
        'chk_S_R_ThunbDown
        '
        Me.chk_S_R_ThunbDown.BackColor = System.Drawing.Color.White
        Me.chk_S_R_ThunbDown.BackgroundImage = CType(resources.GetObject("chk_S_R_ThunbDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_ThunbDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_ThunbDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_ThunbDown.Location = New System.Drawing.Point(529, 384)
        Me.chk_S_R_ThunbDown.Name = "chk_S_R_ThunbDown"
        Me.chk_S_R_ThunbDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_R_ThunbDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_R_ThunbDown.TabIndex = 85
        Me.chk_S_R_ThunbDown.UseVisualStyleBackColor = False
        '
        'chk_S_R_ThunbUp
        '
        Me.chk_S_R_ThunbUp.BackColor = System.Drawing.Color.White
        Me.chk_S_R_ThunbUp.BackgroundImage = CType(resources.GetObject("chk_S_R_ThunbUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_ThunbUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_ThunbUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_ThunbUp.Location = New System.Drawing.Point(510, 406)
        Me.chk_S_R_ThunbUp.Name = "chk_S_R_ThunbUp"
        Me.chk_S_R_ThunbUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_R_ThunbUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_R_ThunbUp.TabIndex = 86
        Me.chk_S_R_ThunbUp.UseVisualStyleBackColor = False
        '
        'chk_S_R_LittleUp
        '
        Me.chk_S_R_LittleUp.BackColor = System.Drawing.Color.White
        Me.chk_S_R_LittleUp.BackgroundImage = CType(resources.GetObject("chk_S_R_LittleUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_LittleUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_LittleUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_LittleUp.Location = New System.Drawing.Point(585, 472)
        Me.chk_S_R_LittleUp.Name = "chk_S_R_LittleUp"
        Me.chk_S_R_LittleUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_R_LittleUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_R_LittleUp.TabIndex = 87
        Me.chk_S_R_LittleUp.UseVisualStyleBackColor = False
        '
        'chk_S_R_RingUp
        '
        Me.chk_S_R_RingUp.BackColor = System.Drawing.Color.White
        Me.chk_S_R_RingUp.BackgroundImage = CType(resources.GetObject("chk_S_R_RingUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_RingUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_RingUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_RingUp.Location = New System.Drawing.Point(562, 466)
        Me.chk_S_R_RingUp.Name = "chk_S_R_RingUp"
        Me.chk_S_R_RingUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_R_RingUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_R_RingUp.TabIndex = 88
        Me.chk_S_R_RingUp.UseVisualStyleBackColor = False
        '
        'chk_S_R_MiddleUp
        '
        Me.chk_S_R_MiddleUp.BackColor = System.Drawing.Color.White
        Me.chk_S_R_MiddleUp.BackgroundImage = CType(resources.GetObject("chk_S_R_MiddleUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_MiddleUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_MiddleUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_MiddleUp.Location = New System.Drawing.Point(539, 458)
        Me.chk_S_R_MiddleUp.Name = "chk_S_R_MiddleUp"
        Me.chk_S_R_MiddleUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_R_MiddleUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_R_MiddleUp.TabIndex = 89
        Me.chk_S_R_MiddleUp.UseVisualStyleBackColor = False
        '
        'chk_S_R_IndexUp
        '
        Me.chk_S_R_IndexUp.BackColor = System.Drawing.Color.White
        Me.chk_S_R_IndexUp.BackgroundImage = CType(resources.GetObject("chk_S_R_IndexUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_IndexUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_IndexUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_IndexUp.Location = New System.Drawing.Point(517, 450)
        Me.chk_S_R_IndexUp.Name = "chk_S_R_IndexUp"
        Me.chk_S_R_IndexUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_R_IndexUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_R_IndexUp.TabIndex = 90
        Me.chk_S_R_IndexUp.UseVisualStyleBackColor = False
        '
        'chk_S_R_Elbow
        '
        Me.chk_S_R_Elbow.BackColor = System.Drawing.Color.White
        Me.chk_S_R_Elbow.BackgroundImage = CType(resources.GetObject("chk_S_R_Elbow.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_Elbow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_Elbow.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_Elbow.Location = New System.Drawing.Point(624, 248)
        Me.chk_S_R_Elbow.Name = "chk_S_R_Elbow"
        Me.chk_S_R_Elbow.Padding = New System.Windows.Forms.Padding(11)
        Me.chk_S_R_Elbow.Size = New System.Drawing.Size(34, 31)
        Me.chk_S_R_Elbow.TabIndex = 91
        Me.chk_S_R_Elbow.UseVisualStyleBackColor = False
        '
        'chk_S_R_Wrist
        '
        Me.chk_S_R_Wrist.BackColor = System.Drawing.Color.White
        Me.chk_S_R_Wrist.BackgroundImage = CType(resources.GetObject("chk_S_R_Wrist.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_Wrist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_Wrist.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_Wrist.Location = New System.Drawing.Point(590, 325)
        Me.chk_S_R_Wrist.Name = "chk_S_R_Wrist"
        Me.chk_S_R_Wrist.Padding = New System.Windows.Forms.Padding(11)
        Me.chk_S_R_Wrist.Size = New System.Drawing.Size(34, 31)
        Me.chk_S_R_Wrist.TabIndex = 92
        Me.chk_S_R_Wrist.UseVisualStyleBackColor = False
        '
        'chk_S_R_Knee
        '
        Me.chk_S_R_Knee.BackColor = System.Drawing.Color.White
        Me.chk_S_R_Knee.BackgroundImage = CType(resources.GetObject("chk_S_R_Knee.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_R_Knee.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_R_Knee.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_R_Knee.Location = New System.Drawing.Point(675, 500)
        Me.chk_S_R_Knee.Name = "chk_S_R_Knee"
        Me.chk_S_R_Knee.Padding = New System.Windows.Forms.Padding(11)
        Me.chk_S_R_Knee.Size = New System.Drawing.Size(34, 31)
        Me.chk_S_R_Knee.TabIndex = 93
        Me.chk_S_R_Knee.UseVisualStyleBackColor = False
        '
        'chk_S_L_Knee
        '
        Me.chk_S_L_Knee.BackColor = System.Drawing.Color.White
        Me.chk_S_L_Knee.BackgroundImage = CType(resources.GetObject("chk_S_L_Knee.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_Knee.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_Knee.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_Knee.Location = New System.Drawing.Point(763, 500)
        Me.chk_S_L_Knee.Name = "chk_S_L_Knee"
        Me.chk_S_L_Knee.Padding = New System.Windows.Forms.Padding(11)
        Me.chk_S_L_Knee.Size = New System.Drawing.Size(34, 31)
        Me.chk_S_L_Knee.TabIndex = 94
        Me.chk_S_L_Knee.UseVisualStyleBackColor = False
        '
        'chk_S_L_Shoulder
        '
        Me.chk_S_L_Shoulder.BackColor = System.Drawing.Color.White
        Me.chk_S_L_Shoulder.BackgroundImage = CType(resources.GetObject("chk_S_L_Shoulder.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_Shoulder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_Shoulder.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_Shoulder.Location = New System.Drawing.Point(800, 141)
        Me.chk_S_L_Shoulder.Name = "chk_S_L_Shoulder"
        Me.chk_S_L_Shoulder.Padding = New System.Windows.Forms.Padding(11)
        Me.chk_S_L_Shoulder.Size = New System.Drawing.Size(34, 31)
        Me.chk_S_L_Shoulder.TabIndex = 95
        Me.chk_S_L_Shoulder.UseVisualStyleBackColor = False
        '
        'chk_S_L_Elbow
        '
        Me.chk_S_L_Elbow.BackColor = System.Drawing.Color.White
        Me.chk_S_L_Elbow.BackgroundImage = CType(resources.GetObject("chk_S_L_Elbow.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_Elbow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_Elbow.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_Elbow.Location = New System.Drawing.Point(810, 247)
        Me.chk_S_L_Elbow.Name = "chk_S_L_Elbow"
        Me.chk_S_L_Elbow.Padding = New System.Windows.Forms.Padding(11)
        Me.chk_S_L_Elbow.Size = New System.Drawing.Size(34, 31)
        Me.chk_S_L_Elbow.TabIndex = 96
        Me.chk_S_L_Elbow.UseVisualStyleBackColor = False
        '
        'chk_S_L_Wrist
        '
        Me.chk_S_L_Wrist.BackColor = System.Drawing.Color.White
        Me.chk_S_L_Wrist.BackgroundImage = CType(resources.GetObject("chk_S_L_Wrist.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_Wrist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_Wrist.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_Wrist.Location = New System.Drawing.Point(845, 327)
        Me.chk_S_L_Wrist.Name = "chk_S_L_Wrist"
        Me.chk_S_L_Wrist.Padding = New System.Windows.Forms.Padding(11)
        Me.chk_S_L_Wrist.Size = New System.Drawing.Size(34, 31)
        Me.chk_S_L_Wrist.TabIndex = 97
        Me.chk_S_L_Wrist.UseVisualStyleBackColor = False
        '
        'chk_S_L_ThunbDown
        '
        Me.chk_S_L_ThunbDown.BackColor = System.Drawing.Color.White
        Me.chk_S_L_ThunbDown.BackgroundImage = CType(resources.GetObject("chk_S_L_ThunbDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_ThunbDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_ThunbDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_ThunbDown.Location = New System.Drawing.Point(919, 384)
        Me.chk_S_L_ThunbDown.Name = "chk_S_L_ThunbDown"
        Me.chk_S_L_ThunbDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_L_ThunbDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_L_ThunbDown.TabIndex = 98
        Me.chk_S_L_ThunbDown.UseVisualStyleBackColor = False
        '
        'chk_S_L_ThunbUp
        '
        Me.chk_S_L_ThunbUp.BackColor = System.Drawing.Color.White
        Me.chk_S_L_ThunbUp.BackgroundImage = CType(resources.GetObject("chk_S_L_ThunbUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_ThunbUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_ThunbUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_ThunbUp.Location = New System.Drawing.Point(941, 406)
        Me.chk_S_L_ThunbUp.Name = "chk_S_L_ThunbUp"
        Me.chk_S_L_ThunbUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_L_ThunbUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_L_ThunbUp.TabIndex = 99
        Me.chk_S_L_ThunbUp.UseVisualStyleBackColor = False
        '
        'chk_S_L_IndexUp
        '
        Me.chk_S_L_IndexUp.BackColor = System.Drawing.Color.White
        Me.chk_S_L_IndexUp.BackgroundImage = CType(resources.GetObject("chk_S_L_IndexUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_IndexUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_IndexUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_IndexUp.Location = New System.Drawing.Point(932, 450)
        Me.chk_S_L_IndexUp.Name = "chk_S_L_IndexUp"
        Me.chk_S_L_IndexUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_L_IndexUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_L_IndexUp.TabIndex = 100
        Me.chk_S_L_IndexUp.UseVisualStyleBackColor = False
        '
        'chk_S_L_MiddleUp
        '
        Me.chk_S_L_MiddleUp.BackColor = System.Drawing.Color.White
        Me.chk_S_L_MiddleUp.BackgroundImage = CType(resources.GetObject("chk_S_L_MiddleUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_MiddleUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_MiddleUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_MiddleUp.Location = New System.Drawing.Point(910, 460)
        Me.chk_S_L_MiddleUp.Name = "chk_S_L_MiddleUp"
        Me.chk_S_L_MiddleUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_L_MiddleUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_L_MiddleUp.TabIndex = 101
        Me.chk_S_L_MiddleUp.UseVisualStyleBackColor = False
        '
        'chk_S_L_IndexDown
        '
        Me.chk_S_L_IndexDown.BackColor = System.Drawing.Color.White
        Me.chk_S_L_IndexDown.BackgroundImage = CType(resources.GetObject("chk_S_L_IndexDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_IndexDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_IndexDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_IndexDown.Location = New System.Drawing.Point(917, 423)
        Me.chk_S_L_IndexDown.Name = "chk_S_L_IndexDown"
        Me.chk_S_L_IndexDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_L_IndexDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_L_IndexDown.TabIndex = 102
        Me.chk_S_L_IndexDown.UseVisualStyleBackColor = False
        '
        'chk_S_L_MiddleDown
        '
        Me.chk_S_L_MiddleDown.BackColor = System.Drawing.Color.White
        Me.chk_S_L_MiddleDown.BackgroundImage = CType(resources.GetObject("chk_S_L_MiddleDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_MiddleDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_MiddleDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_MiddleDown.Location = New System.Drawing.Point(895, 431)
        Me.chk_S_L_MiddleDown.Name = "chk_S_L_MiddleDown"
        Me.chk_S_L_MiddleDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_L_MiddleDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_L_MiddleDown.TabIndex = 103
        Me.chk_S_L_MiddleDown.UseVisualStyleBackColor = False
        '
        'chk_S_L_RingUp
        '
        Me.chk_S_L_RingUp.BackColor = System.Drawing.Color.White
        Me.chk_S_L_RingUp.BackgroundImage = CType(resources.GetObject("chk_S_L_RingUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_RingUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_RingUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_RingUp.Location = New System.Drawing.Point(887, 467)
        Me.chk_S_L_RingUp.Name = "chk_S_L_RingUp"
        Me.chk_S_L_RingUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_L_RingUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_L_RingUp.TabIndex = 104
        Me.chk_S_L_RingUp.UseVisualStyleBackColor = False
        '
        'chk_S_L_RingDown1
        '
        Me.chk_S_L_RingDown1.BackColor = System.Drawing.Color.White
        Me.chk_S_L_RingDown1.BackgroundImage = CType(resources.GetObject("chk_S_L_RingDown1.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_RingDown1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_RingDown1.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_RingDown1.Location = New System.Drawing.Point(873, 439)
        Me.chk_S_L_RingDown1.Name = "chk_S_L_RingDown1"
        Me.chk_S_L_RingDown1.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_L_RingDown1.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_L_RingDown1.TabIndex = 105
        Me.chk_S_L_RingDown1.UseVisualStyleBackColor = False
        '
        'chk_S_L_LittleDown
        '
        Me.chk_S_L_LittleDown.BackColor = System.Drawing.Color.White
        Me.chk_S_L_LittleDown.BackgroundImage = CType(resources.GetObject("chk_S_L_LittleDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_LittleDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_LittleDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_LittleDown.Location = New System.Drawing.Point(851, 446)
        Me.chk_S_L_LittleDown.Name = "chk_S_L_LittleDown"
        Me.chk_S_L_LittleDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_L_LittleDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_L_LittleDown.TabIndex = 106
        Me.chk_S_L_LittleDown.UseVisualStyleBackColor = False
        '
        'chk_S_L_LittleUp
        '
        Me.chk_S_L_LittleUp.BackColor = System.Drawing.Color.White
        Me.chk_S_L_LittleUp.BackgroundImage = CType(resources.GetObject("chk_S_L_LittleUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_S_L_LittleUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_S_L_LittleUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_S_L_LittleUp.Location = New System.Drawing.Point(863, 471)
        Me.chk_S_L_LittleUp.Name = "chk_S_L_LittleUp"
        Me.chk_S_L_LittleUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_S_L_LittleUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_S_L_LittleUp.TabIndex = 107
        Me.chk_S_L_LittleUp.UseVisualStyleBackColor = False
        '
        'chk_T_L_Shoulder
        '
        Me.chk_T_L_Shoulder.BackColor = System.Drawing.Color.White
        Me.chk_T_L_Shoulder.BackgroundImage = CType(resources.GetObject("chk_T_L_Shoulder.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_Shoulder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_Shoulder.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_Shoulder.Location = New System.Drawing.Point(296, 141)
        Me.chk_T_L_Shoulder.Name = "chk_T_L_Shoulder"
        Me.chk_T_L_Shoulder.Padding = New System.Windows.Forms.Padding(10)
        Me.chk_T_L_Shoulder.Size = New System.Drawing.Size(34, 31)
        Me.chk_T_L_Shoulder.TabIndex = 108
        Me.chk_T_L_Shoulder.UseVisualStyleBackColor = False
        '
        'chk_T_R_Elbow
        '
        Me.chk_T_R_Elbow.BackColor = System.Drawing.Color.White
        Me.chk_T_R_Elbow.BackgroundImage = CType(resources.GetObject("chk_T_R_Elbow.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_Elbow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_Elbow.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_Elbow.Location = New System.Drawing.Point(125, 242)
        Me.chk_T_R_Elbow.Name = "chk_T_R_Elbow"
        Me.chk_T_R_Elbow.Padding = New System.Windows.Forms.Padding(10)
        Me.chk_T_R_Elbow.Size = New System.Drawing.Size(34, 31)
        Me.chk_T_R_Elbow.TabIndex = 109
        Me.chk_T_R_Elbow.UseVisualStyleBackColor = False
        '
        'chk_T_L_Elbow
        '
        Me.chk_T_L_Elbow.BackColor = System.Drawing.Color.White
        Me.chk_T_L_Elbow.BackgroundImage = CType(resources.GetObject("chk_T_L_Elbow.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_Elbow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_Elbow.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_Elbow.Location = New System.Drawing.Point(308, 245)
        Me.chk_T_L_Elbow.Name = "chk_T_L_Elbow"
        Me.chk_T_L_Elbow.Padding = New System.Windows.Forms.Padding(10)
        Me.chk_T_L_Elbow.Size = New System.Drawing.Size(34, 31)
        Me.chk_T_L_Elbow.TabIndex = 110
        Me.chk_T_L_Elbow.UseVisualStyleBackColor = False
        '
        'chk_T_R_Wrist
        '
        Me.chk_T_R_Wrist.BackColor = System.Drawing.Color.White
        Me.chk_T_R_Wrist.BackgroundImage = CType(resources.GetObject("chk_T_R_Wrist.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_Wrist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_Wrist.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_Wrist.Location = New System.Drawing.Point(88, 327)
        Me.chk_T_R_Wrist.Name = "chk_T_R_Wrist"
        Me.chk_T_R_Wrist.Padding = New System.Windows.Forms.Padding(10)
        Me.chk_T_R_Wrist.Size = New System.Drawing.Size(34, 31)
        Me.chk_T_R_Wrist.TabIndex = 111
        Me.chk_T_R_Wrist.UseVisualStyleBackColor = False
        '
        'chk_T_L_Wrist
        '
        Me.chk_T_L_Wrist.BackColor = System.Drawing.Color.White
        Me.chk_T_L_Wrist.BackgroundImage = CType(resources.GetObject("chk_T_L_Wrist.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_Wrist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_Wrist.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_Wrist.Location = New System.Drawing.Point(344, 327)
        Me.chk_T_L_Wrist.Name = "chk_T_L_Wrist"
        Me.chk_T_L_Wrist.Padding = New System.Windows.Forms.Padding(10)
        Me.chk_T_L_Wrist.Size = New System.Drawing.Size(34, 31)
        Me.chk_T_L_Wrist.TabIndex = 112
        Me.chk_T_L_Wrist.UseVisualStyleBackColor = False
        '
        'chk_T_R_Knee
        '
        Me.chk_T_R_Knee.BackColor = System.Drawing.Color.White
        Me.chk_T_R_Knee.BackgroundImage = CType(resources.GetObject("chk_T_R_Knee.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_Knee.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_Knee.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_Knee.Location = New System.Drawing.Point(174, 500)
        Me.chk_T_R_Knee.Name = "chk_T_R_Knee"
        Me.chk_T_R_Knee.Padding = New System.Windows.Forms.Padding(10)
        Me.chk_T_R_Knee.Size = New System.Drawing.Size(34, 31)
        Me.chk_T_R_Knee.TabIndex = 113
        Me.chk_T_R_Knee.UseVisualStyleBackColor = False
        '
        'chk_T_L_Knee
        '
        Me.chk_T_L_Knee.BackColor = System.Drawing.Color.White
        Me.chk_T_L_Knee.BackgroundImage = CType(resources.GetObject("chk_T_L_Knee.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_Knee.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_Knee.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_Knee.Location = New System.Drawing.Point(261, 500)
        Me.chk_T_L_Knee.Name = "chk_T_L_Knee"
        Me.chk_T_L_Knee.Padding = New System.Windows.Forms.Padding(10)
        Me.chk_T_L_Knee.Size = New System.Drawing.Size(34, 31)
        Me.chk_T_L_Knee.TabIndex = 114
        Me.chk_T_L_Knee.UseVisualStyleBackColor = False
        '
        'chk_T_R_MiddleDown
        '
        Me.chk_T_R_MiddleDown.BackColor = System.Drawing.Color.White
        Me.chk_T_R_MiddleDown.BackgroundImage = CType(resources.GetObject("chk_T_R_MiddleDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_MiddleDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_MiddleDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_MiddleDown.Location = New System.Drawing.Point(49, 431)
        Me.chk_T_R_MiddleDown.Name = "chk_T_R_MiddleDown"
        Me.chk_T_R_MiddleDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_R_MiddleDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_R_MiddleDown.TabIndex = 115
        Me.chk_T_R_MiddleDown.UseVisualStyleBackColor = False
        '
        'chk_T_R_IndexDown
        '
        Me.chk_T_R_IndexDown.BackColor = System.Drawing.Color.White
        Me.chk_T_R_IndexDown.BackgroundImage = CType(resources.GetObject("chk_T_R_IndexDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_IndexDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_IndexDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_IndexDown.Location = New System.Drawing.Point(27, 423)
        Me.chk_T_R_IndexDown.Name = "chk_T_R_IndexDown"
        Me.chk_T_R_IndexDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_R_IndexDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_R_IndexDown.TabIndex = 116
        Me.chk_T_R_IndexDown.UseVisualStyleBackColor = False
        '
        'chk_T_R_ThunbDown
        '
        Me.chk_T_R_ThunbDown.BackColor = System.Drawing.Color.White
        Me.chk_T_R_ThunbDown.BackgroundImage = CType(resources.GetObject("chk_T_R_ThunbDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_ThunbDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_ThunbDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_ThunbDown.Location = New System.Drawing.Point(27, 385)
        Me.chk_T_R_ThunbDown.Name = "chk_T_R_ThunbDown"
        Me.chk_T_R_ThunbDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_R_ThunbDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_R_ThunbDown.TabIndex = 117
        Me.chk_T_R_ThunbDown.UseVisualStyleBackColor = False
        '
        'chk_T_R_ThunbUp
        '
        Me.chk_T_R_ThunbUp.BackColor = System.Drawing.Color.White
        Me.chk_T_R_ThunbUp.BackgroundImage = CType(resources.GetObject("chk_T_R_ThunbUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_ThunbUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_ThunbUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_ThunbUp.Location = New System.Drawing.Point(8, 406)
        Me.chk_T_R_ThunbUp.Name = "chk_T_R_ThunbUp"
        Me.chk_T_R_ThunbUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_R_ThunbUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_R_ThunbUp.TabIndex = 118
        Me.chk_T_R_ThunbUp.UseVisualStyleBackColor = False
        '
        'chk_T_R_LittleUp
        '
        Me.chk_T_R_LittleUp.BackColor = System.Drawing.Color.White
        Me.chk_T_R_LittleUp.BackgroundImage = CType(resources.GetObject("chk_T_R_LittleUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_LittleUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_LittleUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_LittleUp.Location = New System.Drawing.Point(84, 473)
        Me.chk_T_R_LittleUp.Name = "chk_T_R_LittleUp"
        Me.chk_T_R_LittleUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_R_LittleUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_R_LittleUp.TabIndex = 119
        Me.chk_T_R_LittleUp.UseVisualStyleBackColor = False
        '
        'chk_T_R_RingUp
        '
        Me.chk_T_R_RingUp.BackColor = System.Drawing.Color.White
        Me.chk_T_R_RingUp.BackgroundImage = CType(resources.GetObject("chk_T_R_RingUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_RingUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_RingUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_RingUp.Location = New System.Drawing.Point(62, 467)
        Me.chk_T_R_RingUp.Name = "chk_T_R_RingUp"
        Me.chk_T_R_RingUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_R_RingUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_R_RingUp.TabIndex = 120
        Me.chk_T_R_RingUp.UseVisualStyleBackColor = False
        '
        'chk_T_R_MiddleUp
        '
        Me.chk_T_R_MiddleUp.BackColor = System.Drawing.Color.White
        Me.chk_T_R_MiddleUp.BackgroundImage = CType(resources.GetObject("chk_T_R_MiddleUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_MiddleUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_MiddleUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_MiddleUp.Location = New System.Drawing.Point(39, 459)
        Me.chk_T_R_MiddleUp.Name = "chk_T_R_MiddleUp"
        Me.chk_T_R_MiddleUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_R_MiddleUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_R_MiddleUp.TabIndex = 121
        Me.chk_T_R_MiddleUp.UseVisualStyleBackColor = False
        '
        'chk_T_R_IndexUp
        '
        Me.chk_T_R_IndexUp.BackColor = System.Drawing.Color.White
        Me.chk_T_R_IndexUp.BackgroundImage = CType(resources.GetObject("chk_T_R_IndexUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_R_IndexUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_R_IndexUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_R_IndexUp.Location = New System.Drawing.Point(15, 450)
        Me.chk_T_R_IndexUp.Name = "chk_T_R_IndexUp"
        Me.chk_T_R_IndexUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_R_IndexUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_R_IndexUp.TabIndex = 122
        Me.chk_T_R_IndexUp.UseVisualStyleBackColor = False
        '
        'chk_T_L_LittleDown
        '
        Me.chk_T_L_LittleDown.BackColor = System.Drawing.Color.White
        Me.chk_T_L_LittleDown.BackgroundImage = CType(resources.GetObject("chk_T_L_LittleDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_LittleDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_LittleDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_LittleDown.Location = New System.Drawing.Point(351, 446)
        Me.chk_T_L_LittleDown.Name = "chk_T_L_LittleDown"
        Me.chk_T_L_LittleDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_L_LittleDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_L_LittleDown.TabIndex = 123
        Me.chk_T_L_LittleDown.UseVisualStyleBackColor = False
        '
        'chk_T_L_RingDown
        '
        Me.chk_T_L_RingDown.BackColor = System.Drawing.Color.White
        Me.chk_T_L_RingDown.BackgroundImage = CType(resources.GetObject("chk_T_L_RingDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_RingDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_RingDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_RingDown.Location = New System.Drawing.Point(373, 439)
        Me.chk_T_L_RingDown.Name = "chk_T_L_RingDown"
        Me.chk_T_L_RingDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_L_RingDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_L_RingDown.TabIndex = 124
        Me.chk_T_L_RingDown.UseVisualStyleBackColor = False
        '
        'chk_T_L_MiddleDown
        '
        Me.chk_T_L_MiddleDown.BackColor = System.Drawing.Color.White
        Me.chk_T_L_MiddleDown.BackgroundImage = CType(resources.GetObject("chk_T_L_MiddleDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_MiddleDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_MiddleDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_MiddleDown.Location = New System.Drawing.Point(396, 431)
        Me.chk_T_L_MiddleDown.Name = "chk_T_L_MiddleDown"
        Me.chk_T_L_MiddleDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_L_MiddleDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_L_MiddleDown.TabIndex = 125
        Me.chk_T_L_MiddleDown.UseVisualStyleBackColor = False
        '
        'chk_T_L_IndexDown
        '
        Me.chk_T_L_IndexDown.BackColor = System.Drawing.Color.White
        Me.chk_T_L_IndexDown.BackgroundImage = CType(resources.GetObject("chk_T_L_IndexDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_IndexDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_IndexDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_IndexDown.Location = New System.Drawing.Point(417, 423)
        Me.chk_T_L_IndexDown.Name = "chk_T_L_IndexDown"
        Me.chk_T_L_IndexDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_L_IndexDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_L_IndexDown.TabIndex = 126
        Me.chk_T_L_IndexDown.UseVisualStyleBackColor = False
        '
        'chk_T_L_ThunbDown
        '
        Me.chk_T_L_ThunbDown.BackColor = System.Drawing.Color.White
        Me.chk_T_L_ThunbDown.BackgroundImage = CType(resources.GetObject("chk_T_L_ThunbDown.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_ThunbDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_ThunbDown.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_ThunbDown.Location = New System.Drawing.Point(417, 384)
        Me.chk_T_L_ThunbDown.Name = "chk_T_L_ThunbDown"
        Me.chk_T_L_ThunbDown.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_L_ThunbDown.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_L_ThunbDown.TabIndex = 127
        Me.chk_T_L_ThunbDown.UseVisualStyleBackColor = False
        '
        'chk_T_L_ThunbUp
        '
        Me.chk_T_L_ThunbUp.BackColor = System.Drawing.Color.White
        Me.chk_T_L_ThunbUp.BackgroundImage = CType(resources.GetObject("chk_T_L_ThunbUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_ThunbUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_ThunbUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_ThunbUp.Location = New System.Drawing.Point(436, 406)
        Me.chk_T_L_ThunbUp.Name = "chk_T_L_ThunbUp"
        Me.chk_T_L_ThunbUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_L_ThunbUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_L_ThunbUp.TabIndex = 128
        Me.chk_T_L_ThunbUp.UseVisualStyleBackColor = False
        '
        'chk_T_L_IndexUp
        '
        Me.chk_T_L_IndexUp.BackColor = System.Drawing.Color.White
        Me.chk_T_L_IndexUp.BackgroundImage = CType(resources.GetObject("chk_T_L_IndexUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_IndexUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_IndexUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_IndexUp.Location = New System.Drawing.Point(430, 450)
        Me.chk_T_L_IndexUp.Name = "chk_T_L_IndexUp"
        Me.chk_T_L_IndexUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_L_IndexUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_L_IndexUp.TabIndex = 129
        Me.chk_T_L_IndexUp.UseVisualStyleBackColor = False
        '
        'chk_T_L_MiddleUp
        '
        Me.chk_T_L_MiddleUp.BackColor = System.Drawing.Color.White
        Me.chk_T_L_MiddleUp.BackgroundImage = CType(resources.GetObject("chk_T_L_MiddleUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_MiddleUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_MiddleUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_MiddleUp.Location = New System.Drawing.Point(407, 460)
        Me.chk_T_L_MiddleUp.Name = "chk_T_L_MiddleUp"
        Me.chk_T_L_MiddleUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_L_MiddleUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_L_MiddleUp.TabIndex = 130
        Me.chk_T_L_MiddleUp.UseVisualStyleBackColor = False
        '
        'chk_T_L_RingUp
        '
        Me.chk_T_L_RingUp.BackColor = System.Drawing.Color.White
        Me.chk_T_L_RingUp.BackgroundImage = CType(resources.GetObject("chk_T_L_RingUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_RingUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_RingUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_RingUp.Location = New System.Drawing.Point(385, 467)
        Me.chk_T_L_RingUp.Name = "chk_T_L_RingUp"
        Me.chk_T_L_RingUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_L_RingUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_L_RingUp.TabIndex = 131
        Me.chk_T_L_RingUp.UseVisualStyleBackColor = False
        '
        'chk_T_L_LittleUp
        '
        Me.chk_T_L_LittleUp.BackColor = System.Drawing.Color.White
        Me.chk_T_L_LittleUp.BackgroundImage = CType(resources.GetObject("chk_T_L_LittleUp.BackgroundImage"), System.Drawing.Image)
        Me.chk_T_L_LittleUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chk_T_L_LittleUp.ForeColor = System.Drawing.Color.Transparent
        Me.chk_T_L_LittleUp.Location = New System.Drawing.Point(361, 473)
        Me.chk_T_L_LittleUp.Name = "chk_T_L_LittleUp"
        Me.chk_T_L_LittleUp.Padding = New System.Windows.Forms.Padding(5)
        Me.chk_T_L_LittleUp.Size = New System.Drawing.Size(23, 21)
        Me.chk_T_L_LittleUp.TabIndex = 132
        Me.chk_T_L_LittleUp.UseVisualStyleBackColor = False
        '
        'pnlMain
        '
        Me.pnlMain.AutoScroll = True
        Me.pnlMain.Controls.Add(Me.pnlImages)
        Me.pnlMain.Controls.Add(Me.pnlHide)
        Me.pnlMain.Controls.Add(Me.pnlImgHeader)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(58, 91)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(968, 742)
        Me.pnlMain.TabIndex = 2
        '
        'pnlImages
        '
        Me.pnlImages.AutoScroll = True
        Me.pnlImages.BackColor = System.Drawing.Color.White
        Me.pnlImages.Controls.Add(Me.chk_T_R_Shoulder)
        Me.pnlImages.Controls.Add(Me.chk_T_L_ThunbUp)
        Me.pnlImages.Controls.Add(Me.chk_T_R_Wrist)
        Me.pnlImages.Controls.Add(Me.chk_S_L_Shoulder)
        Me.pnlImages.Controls.Add(Me.chk_S_R_ThunbUp)
        Me.pnlImages.Controls.Add(Me.chk_T_R_LittleDown)
        Me.pnlImages.Controls.Add(Me.chk_S_L_Knee)
        Me.pnlImages.Controls.Add(Me.chk_T_R_LittleUp)
        Me.pnlImages.Controls.Add(Me.chk_T_L_Elbow)
        Me.pnlImages.Controls.Add(Me.chk_T_R_RingUp)
        Me.pnlImages.Controls.Add(Me.chk_T_L_ThunbDown)
        Me.pnlImages.Controls.Add(Me.chk_T_L_IndexUp)
        Me.pnlImages.Controls.Add(Me.chk_S_R_LittleUp)
        Me.pnlImages.Controls.Add(Me.chk_T_L_Wrist)
        Me.pnlImages.Controls.Add(Me.chk_S_L_MiddleDown)
        Me.pnlImages.Controls.Add(Me.chk_S_L_Elbow)
        Me.pnlImages.Controls.Add(Me.chk_S_R_ThunbDown)
        Me.pnlImages.Controls.Add(Me.chk_T_R_RingDown)
        Me.pnlImages.Controls.Add(Me.chk_S_R_Knee)
        Me.pnlImages.Controls.Add(Me.chk_T_R_ThunbUp)
        Me.pnlImages.Controls.Add(Me.chk_T_R_Elbow)
        Me.pnlImages.Controls.Add(Me.chk_S_L_IndexDown)
        Me.pnlImages.Controls.Add(Me.chk_T_L_IndexDown)
        Me.pnlImages.Controls.Add(Me.chk_T_R_MiddleUp)
        Me.pnlImages.Controls.Add(Me.chk_T_L_MiddleUp)
        Me.pnlImages.Controls.Add(Me.chk_T_R_Knee)
        Me.pnlImages.Controls.Add(Me.chk_S_R_RingUp)
        Me.pnlImages.Controls.Add(Me.chk_S_L_Wrist)
        Me.pnlImages.Controls.Add(Me.chk_S_L_RingUp)
        Me.pnlImages.Controls.Add(Me.chk_S_R_Shoulder)
        Me.pnlImages.Controls.Add(Me.chk_S_R_IndexDown)
        Me.pnlImages.Controls.Add(Me.chk_S_R_Wrist)
        Me.pnlImages.Controls.Add(Me.chk_T_L_Shoulder)
        Me.pnlImages.Controls.Add(Me.chk_T_R_ThunbDown)
        Me.pnlImages.Controls.Add(Me.chk_T_L_MiddleDown)
        Me.pnlImages.Controls.Add(Me.chk_S_L_MiddleUp)
        Me.pnlImages.Controls.Add(Me.chk_T_L_RingUp)
        Me.pnlImages.Controls.Add(Me.chk_T_R_IndexUp)
        Me.pnlImages.Controls.Add(Me.chk_T_L_Knee)
        Me.pnlImages.Controls.Add(Me.chk_S_L_ThunbDown)
        Me.pnlImages.Controls.Add(Me.chk_S_R_MiddleUp)
        Me.pnlImages.Controls.Add(Me.chk_S_R_LittleDown)
        Me.pnlImages.Controls.Add(Me.chk_S_L_RingDown1)
        Me.pnlImages.Controls.Add(Me.chk_S_R_Elbow)
        Me.pnlImages.Controls.Add(Me.chk_S_R_MiddleDown)
        Me.pnlImages.Controls.Add(Me.chk_S_L_LittleUp)
        Me.pnlImages.Controls.Add(Me.chk_T_L_RingDown)
        Me.pnlImages.Controls.Add(Me.chk_T_R_IndexDown)
        Me.pnlImages.Controls.Add(Me.chk_T_L_LittleUp)
        Me.pnlImages.Controls.Add(Me.chk_S_L_IndexUp)
        Me.pnlImages.Controls.Add(Me.chk_T_R_MiddleDown)
        Me.pnlImages.Controls.Add(Me.chk_T_L_LittleDown)
        Me.pnlImages.Controls.Add(Me.chk_S_L_ThunbUp)
        Me.pnlImages.Controls.Add(Me.chk_S_L_LittleDown)
        Me.pnlImages.Controls.Add(Me.chk_S_R_RingDown)
        Me.pnlImages.Controls.Add(Me.chk_S_R_IndexUp)
        Me.pnlImages.Controls.Add(Me.PictureBox1)
        Me.pnlImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImages.Location = New System.Drawing.Point(0, 22)
        Me.pnlImages.Name = "pnlImages"
        Me.pnlImages.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlImages.Size = New System.Drawing.Size(968, 720)
        Me.pnlImages.TabIndex = 133
        '
        'pnlImgHeader
        '
        Me.pnlImgHeader.Controls.Add(Me.lblTenderName)
        Me.pnlImgHeader.Controls.Add(Me.lblSwollenName)
        Me.pnlImgHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlImgHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlImgHeader.Name = "pnlImgHeader"
        Me.pnlImgHeader.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlImgHeader.Size = New System.Drawing.Size(968, 22)
        Me.pnlImgHeader.TabIndex = 135
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.Label5)
        Me.pnlBottom.Controls.Add(Me.Label6)
        Me.pnlBottom.Controls.Add(Me.Label7)
        Me.pnlBottom.Controls.Add(Me.Panel7)
        Me.pnlBottom.Controls.Add(Me.Panel6)
        Me.pnlBottom.Controls.Add(Me.Label8)
        Me.pnlBottom.Controls.Add(Me.Panel2)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 833)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlBottom.Size = New System.Drawing.Size(1084, 109)
        Me.pnlBottom.TabIndex = 135
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1080, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 101)
        Me.Label5.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 101)
        Me.Label6.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 105)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1078, 1)
        Me.Label7.TabIndex = 10
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label19)
        Me.Panel7.Controls.Add(Me.Label20)
        Me.Panel7.Controls.Add(Me.Label21)
        Me.Panel7.Controls.Add(Me.lblCalculatedDAS)
        Me.Panel7.Controls.Add(Me.Label22)
        Me.Panel7.Controls.Add(Me.lblCalculationFormula)
        Me.Panel7.Controls.Add(Me.lblCalculationName)
        Me.Panel7.Controls.Add(Me.lblCalculationLabel)
        Me.Panel7.Controls.Add(Me.txtCalculatedDAS)
        Me.Panel7.Controls.Add(Me.btnCalculate)
        Me.Panel7.Location = New System.Drawing.Point(655, 9)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(423, 89)
        Me.Panel7.TabIndex = 77
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Cornsilk
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1, 88)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(421, 1)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Cornsilk
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(1, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(421, 1)
        Me.Label20.TabIndex = 4
        Me.Label20.Text = "label4"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Cornsilk
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 89)
        Me.Label21.TabIndex = 3
        Me.Label21.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Cornsilk
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(422, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 89)
        Me.Label22.TabIndex = 2
        Me.Label22.Text = "label4"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.pnlPainScale)
        Me.Panel6.Controls.Add(Me.Label17)
        Me.Panel6.Controls.Add(Me.txtPainScore)
        Me.Panel6.Controls.Add(Me.Label13)
        Me.Panel6.Controls.Add(Me.chkPainScale)
        Me.Panel6.Controls.Add(Me.Label14)
        Me.Panel6.Controls.Add(Me.Label15)
        Me.Panel6.Controls.Add(Me.Label16)
        Me.Panel6.Location = New System.Drawing.Point(215, 9)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(434, 89)
        Me.Panel6.TabIndex = 77
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(25, 24)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 14)
        Me.Label17.TabIndex = 14
        Me.Label17.Text = "Use Pain Scale"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Cornsilk
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(1, 88)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(432, 1)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Cornsilk
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(1, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(432, 1)
        Me.Label14.TabIndex = 4
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Cornsilk
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 89)
        Me.Label15.TabIndex = 3
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Cornsilk
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(433, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 89)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1078, 1)
        Me.Label8.TabIndex = 9
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblESRUnit)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.lblCRPUnits)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.rbtnESR)
        Me.Panel2.Controls.Add(Me.rbtnCRP)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.txtESR)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.txtCRP)
        Me.Panel2.Location = New System.Drawing.Point(6, 9)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(204, 89)
        Me.Panel2.TabIndex = 77
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Cornsilk
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(1, 88)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(202, 1)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Cornsilk
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(202, 1)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Cornsilk
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 89)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Cornsilk
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(203, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 89)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "label4"
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.Label4)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.rbtnJointCount)
        Me.pnlTop.Controls.Add(Me.rbtnUseDiagram)
        Me.pnlTop.Controls.Add(Me.lblSwollenCount)
        Me.pnlTop.Controls.Add(Me.txtTenderCount)
        Me.pnlTop.Controls.Add(Me.lblTenderCount)
        Me.pnlTop.Controls.Add(Me.txtSwollenCount)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 56)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(1084, 35)
        Me.pnlTop.TabIndex = 134
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1080, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 27)
        Me.Label4.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 27)
        Me.Label3.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1078, 1)
        Me.Label2.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1078, 1)
        Me.Label1.TabIndex = 9
        '
        'Left
        '
        Me.Left.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Left.Dock = System.Windows.Forms.DockStyle.Left
        Me.Left.Location = New System.Drawing.Point(0, 91)
        Me.Left.Name = "Left"
        Me.Left.Size = New System.Drawing.Size(58, 742)
        Me.Left.TabIndex = 137
        '
        'Right
        '
        Me.Right.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Right.Dock = System.Windows.Forms.DockStyle.Right
        Me.Right.Location = New System.Drawing.Point(1026, 91)
        Me.Right.Name = "Right"
        Me.Right.Size = New System.Drawing.Size(58, 742)
        Me.Right.TabIndex = 138
        '
        'frmDASCalculator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1084, 942)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Right)
        Me.Controls.Add(Me.Left)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmDASCalculator"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "DAS Calculator"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPainScale.ResumeLayout(False)
        Me.pnlPainScale.PerformLayout()
        CType(Me.trbPainScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHide.ResumeLayout(False)
        CType(Me.pb_HideImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tblStrip.ResumeLayout(False)
        Me.tblStrip.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlImages.ResumeLayout(False)
        Me.pnlImgHeader.ResumeLayout(False)
        Me.pnlImgHeader.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chk_T_R_Shoulder As System.Windows.Forms.CheckBox
    Friend WithEvents lblTenderName As System.Windows.Forms.Label
    Friend WithEvents lblSwollenName As System.Windows.Forms.Label
    Friend WithEvents rbtnESR As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnCRP As System.Windows.Forms.RadioButton
    Friend WithEvents txtESR As System.Windows.Forms.TextBox
    Friend WithEvents txtCRP As System.Windows.Forms.TextBox
    Friend WithEvents chkPainScale As System.Windows.Forms.CheckBox
    Private WithEvents pnlPainScale As System.Windows.Forms.Panel
    Private WithEvents Label100 As System.Windows.Forms.Label
    Private WithEvents Label101 As System.Windows.Forms.Label
    Private WithEvents Label102 As System.Windows.Forms.Label
    Private WithEvents Label103 As System.Windows.Forms.Label
    Private WithEvents Label104 As System.Windows.Forms.Label
    Private WithEvents Label105 As System.Windows.Forms.Label
    Private WithEvents Label106 As System.Windows.Forms.Label
    Private WithEvents Label107 As System.Windows.Forms.Label
    Private WithEvents Label108 As System.Windows.Forms.Label
    Private WithEvents Label109 As System.Windows.Forms.Label
    Private WithEvents Label110 As System.Windows.Forms.Label
    Private WithEvents Label111 As System.Windows.Forms.Label
    Private WithEvents Label112 As System.Windows.Forms.Label
    Private WithEvents Label113 As System.Windows.Forms.Label
    Private WithEvents Label114 As System.Windows.Forms.Label
    Private WithEvents Label115 As System.Windows.Forms.Label
    Private WithEvents Label116 As System.Windows.Forms.Label
    Private WithEvents Label117 As System.Windows.Forms.Label
    Private WithEvents Label118 As System.Windows.Forms.Label
    Private WithEvents Label119 As System.Windows.Forms.Label
    Private WithEvents Label120 As System.Windows.Forms.Label
    Private WithEvents trbPainScale As System.Windows.Forms.TrackBar
    Friend WithEvents btnCalculate As System.Windows.Forms.Button
    Friend WithEvents txtCalculatedDAS As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents pnlHide As System.Windows.Forms.Panel
    Friend WithEvents tblStrip As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Ok_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents lblCalculationLabel As System.Windows.Forms.Label
    Friend WithEvents lblCalculationName As System.Windows.Forms.Label
    Friend WithEvents lblCalculationFormula As System.Windows.Forms.Label
    Friend WithEvents lblCalculatedDAS As System.Windows.Forms.Label
    Friend WithEvents lblSwollenCount As System.Windows.Forms.Label
    Friend WithEvents txtSwollenCount As System.Windows.Forms.TextBox
    Friend WithEvents txtTenderCount As System.Windows.Forms.TextBox
    Friend WithEvents lblTenderCount As System.Windows.Forms.Label
    Friend WithEvents rbtnUseDiagram As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnJointCount As System.Windows.Forms.RadioButton
    Friend WithEvents txtPainScore As System.Windows.Forms.TextBox
    Friend WithEvents lblCRPUnits As System.Windows.Forms.Label
    Friend WithEvents lblESRUnit As System.Windows.Forms.Label
    Friend WithEvents pb_HideImage As System.Windows.Forms.PictureBox
    Friend WithEvents chk_T_R_LittleDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_RingDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_Shoulder As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_LittleDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_RingDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_MiddleDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_IndexDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_ThunbDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_ThunbUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_LittleUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_RingUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_MiddleUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_IndexUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_Elbow As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_Wrist As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_R_Knee As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_Knee As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_Shoulder As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_Elbow As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_Wrist As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_ThunbDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_ThunbUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_IndexUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_MiddleUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_IndexDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_MiddleDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_RingUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_RingDown1 As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_LittleDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_S_L_LittleUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_Shoulder As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_Elbow As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_Elbow As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_Wrist As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_Wrist As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_Knee As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_Knee As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_MiddleDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_IndexDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_ThunbDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_ThunbUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_LittleUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_RingUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_MiddleUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_R_IndexUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_LittleDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_RingDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_MiddleDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_IndexDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_ThunbDown As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_ThunbUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_IndexUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_MiddleUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_RingUp As System.Windows.Forms.CheckBox
    Friend WithEvents chk_T_L_LittleUp As System.Windows.Forms.CheckBox
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlImages As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pnlImgHeader As System.Windows.Forms.Panel
    Friend WithEvents Right As System.Windows.Forms.Panel
    Friend WithEvents Left As System.Windows.Forms.Panel
End Class
