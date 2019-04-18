<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVitalNorms
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVitalNorms))
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.trvNorms = New System.Windows.Forms.TreeView()
        Me.imgCommon = New System.Windows.Forms.ImageList(Me.components)
        Me.Label174 = New System.Windows.Forms.Label()
        Me.Label175 = New System.Windows.Forms.Label()
        Me.Label176 = New System.Windows.Forms.Label()
        Me.Label177 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rbFemale = New System.Windows.Forms.RadioButton()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.rbMale = New System.Windows.Forms.RadioButton()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.pnlMainVitalsNorms = New System.Windows.Forms.Panel()
        Me.pnlVitals = New System.Windows.Forms.Panel()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.label54 = New System.Windows.Forms.Label()
        Me.label53 = New System.Windows.Forms.Label()
        Me.txtStatureMaxInInch = New System.Windows.Forms.TextBox()
        Me.txtStatureMinInInch = New System.Windows.Forms.TextBox()
        Me.txtHeadCircumMaxInInch = New System.Windows.Forms.TextBox()
        Me.txtHeadCircumMinInInch = New System.Windows.Forms.TextBox()
        Me.txtTemperatureMaxInCel = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtTemperatureMinIncel = New System.Windows.Forms.TextBox()
        Me.txtWeightMax = New System.Windows.Forms.TextBox()
        Me.txtWeightMin = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.label56 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.label55 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtMaxWtOz = New System.Windows.Forms.TextBox()
        Me.txtWtOz = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtMaxWeightlbs = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtMaxWeightKg = New System.Windows.Forms.TextBox()
        Me.txtWeightlbs = New System.Windows.Forms.TextBox()
        Me.txtWeightKg = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtHeightMaxIncm = New System.Windows.Forms.TextBox()
        Me.txtHeightMaxInInch = New System.Windows.Forms.TextBox()
        Me.txtHeightMinIncm = New System.Windows.Forms.TextBox()
        Me.txtHeightMinInInch = New System.Windows.Forms.TextBox()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.txtHeightMin = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtHeightMaxInch = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtHeightMinInch = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPulseOXmax = New System.Windows.Forms.TextBox()
        Me.lblPulseOXMax = New System.Windows.Forms.Label()
        Me.txtPulseOXmin = New System.Windows.Forms.TextBox()
        Me.lblPulseOXMin = New System.Windows.Forms.Label()
        Me.txtRespRateMax = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPulseMax = New System.Windows.Forms.TextBox()
        Me.txtRespRateMin = New System.Windows.Forms.TextBox()
        Me.lblPulseMax = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPulseMin = New System.Windows.Forms.TextBox()
        Me.lblPulseMin = New System.Windows.Forms.Label()
        Me.txtStatureMax = New System.Windows.Forms.TextBox()
        Me.txtTemperatureMax = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblTempratureMax = New System.Windows.Forms.Label()
        Me.txtHeadCircumMax = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtHeadCircumMin = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblWeightMax = New System.Windows.Forms.Label()
        Me.txtHeightMax = New System.Windows.Forms.TextBox()
        Me.lblHeightMax = New System.Windows.Forms.Label()
        Me.txtBPDiastolicMax = New System.Windows.Forms.TextBox()
        Me.txtBPDiastolicMin = New System.Windows.Forms.TextBox()
        Me.txtBPSystolicMax = New System.Windows.Forms.TextBox()
        Me.txtBPSystolicMin = New System.Windows.Forms.TextBox()
        Me.lblBPStandingMax = New System.Windows.Forms.Label()
        Me.lblBPStandingMin = New System.Windows.Forms.Label()
        Me.lblBPSittingMax = New System.Windows.Forms.Label()
        Me.txtStatureMin = New System.Windows.Forms.TextBox()
        Me.lblBPSittingMin = New System.Windows.Forms.Label()
        Me.txtTemperatureMin = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblWeightMin = New System.Windows.Forms.Label()
        Me.lblTempratureMin = New System.Windows.Forms.Label()
        Me.lblHeightMin = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnl_tlstrip = New System.Windows.Forms.Panel()
        Me.tlsNorms = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.tsb_RestoreDefaultSetting = New System.Windows.Forms.ToolStripButton()
        Me.tsb_Close = New System.Windows.Forms.ToolStripButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.lblNormHeader = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkVitalNorms = New System.Windows.Forms.CheckBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.pnlLeft.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlMainVitalsNorms.SuspendLayout()
        Me.pnlVitals.SuspendLayout()
        Me.pnl_tlstrip.SuspendLayout()
        Me.tlsNorms.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeft.Controls.Add(Me.Panel19)
        Me.pnlLeft.Controls.Add(Me.Panel1)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 110)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(212, 383)
        Me.pnlLeft.TabIndex = 1
        '
        'Panel19
        '
        Me.Panel19.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel19.Controls.Add(Me.trvNorms)
        Me.Panel19.Controls.Add(Me.Label174)
        Me.Panel19.Controls.Add(Me.Label175)
        Me.Panel19.Controls.Add(Me.Label176)
        Me.Panel19.Controls.Add(Me.Label177)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel19.Location = New System.Drawing.Point(0, 28)
        Me.Panel19.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel19.Size = New System.Drawing.Size(212, 355)
        Me.Panel19.TabIndex = 0
        '
        'trvNorms
        '
        Me.trvNorms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvNorms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvNorms.HideSelection = False
        Me.trvNorms.ImageIndex = 0
        Me.trvNorms.ImageList = Me.imgCommon
        Me.trvNorms.Location = New System.Drawing.Point(4, 1)
        Me.trvNorms.Name = "trvNorms"
        Me.trvNorms.SelectedImageIndex = 0
        Me.trvNorms.Size = New System.Drawing.Size(207, 350)
        Me.trvNorms.TabIndex = 9
        '
        'imgCommon
        '
        Me.imgCommon.ImageStream = CType(resources.GetObject("imgCommon.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgCommon.TransparentColor = System.Drawing.Color.Transparent
        Me.imgCommon.Images.SetKeyName(0, "Bullet06.ico")
        '
        'Label174
        '
        Me.Label174.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label174.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label174.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label174.Location = New System.Drawing.Point(4, 351)
        Me.Label174.Name = "Label174"
        Me.Label174.Size = New System.Drawing.Size(207, 1)
        Me.Label174.TabIndex = 8
        Me.Label174.Text = "label2"
        '
        'Label175
        '
        Me.Label175.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label175.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label175.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label175.Location = New System.Drawing.Point(3, 1)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(1, 351)
        Me.Label175.TabIndex = 7
        Me.Label175.Text = "label4"
        '
        'Label176
        '
        Me.Label176.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label176.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label176.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label176.Location = New System.Drawing.Point(211, 1)
        Me.Label176.Name = "Label176"
        Me.Label176.Size = New System.Drawing.Size(1, 351)
        Me.Label176.TabIndex = 6
        Me.Label176.Text = "label3"
        '
        'Label177
        '
        Me.Label177.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label177.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label177.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label177.Location = New System.Drawing.Point(3, 0)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(209, 1)
        Me.Label177.TabIndex = 5
        Me.Label177.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(212, 28)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.rbFemale)
        Me.Panel3.Controls.Add(Me.Label16)
        Me.Panel3.Controls.Add(Me.rbMale)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(209, 25)
        Me.Panel3.TabIndex = 19
        '
        'rbFemale
        '
        Me.rbFemale.AutoSize = True
        Me.rbFemale.Dock = System.Windows.Forms.DockStyle.Right
        Me.rbFemale.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFemale.Location = New System.Drawing.Point(123, 1)
        Me.rbFemale.Name = "rbFemale"
        Me.rbFemale.Size = New System.Drawing.Size(63, 23)
        Me.rbFemale.TabIndex = 1
        Me.rbFemale.Text = "Female"
        Me.rbFemale.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(186, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(22, 23)
        Me.Label16.TabIndex = 50
        '
        'rbMale
        '
        Me.rbMale.AutoSize = True
        Me.rbMale.Dock = System.Windows.Forms.DockStyle.Left
        Me.rbMale.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMale.Location = New System.Drawing.Point(36, 1)
        Me.rbMale.Name = "rbMale"
        Me.rbMale.Size = New System.Drawing.Size(49, 23)
        Me.rbMale.TabIndex = 0
        Me.rbMale.Text = "Male"
        Me.rbMale.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(1, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(35, 23)
        Me.Label15.TabIndex = 49
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(207, 1)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 24)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(208, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 24)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(209, 1)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "label1"
        '
        'pnlMainVitalsNorms
        '
        Me.pnlMainVitalsNorms.Controls.Add(Me.pnlVitals)
        Me.pnlMainVitalsNorms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMainVitalsNorms.Location = New System.Drawing.Point(212, 110)
        Me.pnlMainVitalsNorms.Name = "pnlMainVitalsNorms"
        Me.pnlMainVitalsNorms.Size = New System.Drawing.Size(661, 383)
        Me.pnlMainVitalsNorms.TabIndex = 2
        '
        'pnlVitals
        '
        Me.pnlVitals.BackColor = System.Drawing.Color.Transparent
        Me.pnlVitals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlVitals.Controls.Add(Me.Label38)
        Me.pnlVitals.Controls.Add(Me.Label37)
        Me.pnlVitals.Controls.Add(Me.Label36)
        Me.pnlVitals.Controls.Add(Me.Label35)
        Me.pnlVitals.Controls.Add(Me.Label40)
        Me.pnlVitals.Controls.Add(Me.Label39)
        Me.pnlVitals.Controls.Add(Me.label54)
        Me.pnlVitals.Controls.Add(Me.label53)
        Me.pnlVitals.Controls.Add(Me.txtStatureMaxInInch)
        Me.pnlVitals.Controls.Add(Me.txtStatureMinInInch)
        Me.pnlVitals.Controls.Add(Me.txtHeadCircumMaxInInch)
        Me.pnlVitals.Controls.Add(Me.txtHeadCircumMinInInch)
        Me.pnlVitals.Controls.Add(Me.txtTemperatureMaxInCel)
        Me.pnlVitals.Controls.Add(Me.Label34)
        Me.pnlVitals.Controls.Add(Me.Label33)
        Me.pnlVitals.Controls.Add(Me.Label31)
        Me.pnlVitals.Controls.Add(Me.Label32)
        Me.pnlVitals.Controls.Add(Me.txtTemperatureMinIncel)
        Me.pnlVitals.Controls.Add(Me.txtWeightMax)
        Me.pnlVitals.Controls.Add(Me.txtWeightMin)
        Me.pnlVitals.Controls.Add(Me.Label30)
        Me.pnlVitals.Controls.Add(Me.Label24)
        Me.pnlVitals.Controls.Add(Me.Label23)
        Me.pnlVitals.Controls.Add(Me.Label29)
        Me.pnlVitals.Controls.Add(Me.label56)
        Me.pnlVitals.Controls.Add(Me.Label22)
        Me.pnlVitals.Controls.Add(Me.Label28)
        Me.pnlVitals.Controls.Add(Me.label55)
        Me.pnlVitals.Controls.Add(Me.Label17)
        Me.pnlVitals.Controls.Add(Me.Label27)
        Me.pnlVitals.Controls.Add(Me.Label25)
        Me.pnlVitals.Controls.Add(Me.Label21)
        Me.pnlVitals.Controls.Add(Me.txtMaxWtOz)
        Me.pnlVitals.Controls.Add(Me.txtWtOz)
        Me.pnlVitals.Controls.Add(Me.Label26)
        Me.pnlVitals.Controls.Add(Me.txtMaxWeightlbs)
        Me.pnlVitals.Controls.Add(Me.Label20)
        Me.pnlVitals.Controls.Add(Me.txtMaxWeightKg)
        Me.pnlVitals.Controls.Add(Me.txtWeightlbs)
        Me.pnlVitals.Controls.Add(Me.txtWeightKg)
        Me.pnlVitals.Controls.Add(Me.Label18)
        Me.pnlVitals.Controls.Add(Me.Label19)
        Me.pnlVitals.Controls.Add(Me.txtHeightMaxIncm)
        Me.pnlVitals.Controls.Add(Me.txtHeightMaxInInch)
        Me.pnlVitals.Controls.Add(Me.txtHeightMinIncm)
        Me.pnlVitals.Controls.Add(Me.txtHeightMinInInch)
        Me.pnlVitals.Controls.Add(Me.Label111)
        Me.pnlVitals.Controls.Add(Me.Label112)
        Me.pnlVitals.Controls.Add(Me.Label113)
        Me.pnlVitals.Controls.Add(Me.Label114)
        Me.pnlVitals.Controls.Add(Me.txtHeightMin)
        Me.pnlVitals.Controls.Add(Me.Label11)
        Me.pnlVitals.Controls.Add(Me.txtHeightMaxInch)
        Me.pnlVitals.Controls.Add(Me.Label12)
        Me.pnlVitals.Controls.Add(Me.txtHeightMinInch)
        Me.pnlVitals.Controls.Add(Me.Label9)
        Me.pnlVitals.Controls.Add(Me.txtPulseOXmax)
        Me.pnlVitals.Controls.Add(Me.lblPulseOXMax)
        Me.pnlVitals.Controls.Add(Me.txtPulseOXmin)
        Me.pnlVitals.Controls.Add(Me.lblPulseOXMin)
        Me.pnlVitals.Controls.Add(Me.txtRespRateMax)
        Me.pnlVitals.Controls.Add(Me.Label7)
        Me.pnlVitals.Controls.Add(Me.txtPulseMax)
        Me.pnlVitals.Controls.Add(Me.txtRespRateMin)
        Me.pnlVitals.Controls.Add(Me.lblPulseMax)
        Me.pnlVitals.Controls.Add(Me.Label6)
        Me.pnlVitals.Controls.Add(Me.txtPulseMin)
        Me.pnlVitals.Controls.Add(Me.lblPulseMin)
        Me.pnlVitals.Controls.Add(Me.txtStatureMax)
        Me.pnlVitals.Controls.Add(Me.txtTemperatureMax)
        Me.pnlVitals.Controls.Add(Me.Label5)
        Me.pnlVitals.Controls.Add(Me.lblTempratureMax)
        Me.pnlVitals.Controls.Add(Me.txtHeadCircumMax)
        Me.pnlVitals.Controls.Add(Me.Label4)
        Me.pnlVitals.Controls.Add(Me.txtHeadCircumMin)
        Me.pnlVitals.Controls.Add(Me.Label3)
        Me.pnlVitals.Controls.Add(Me.lblWeightMax)
        Me.pnlVitals.Controls.Add(Me.txtHeightMax)
        Me.pnlVitals.Controls.Add(Me.lblHeightMax)
        Me.pnlVitals.Controls.Add(Me.txtBPDiastolicMax)
        Me.pnlVitals.Controls.Add(Me.txtBPDiastolicMin)
        Me.pnlVitals.Controls.Add(Me.txtBPSystolicMax)
        Me.pnlVitals.Controls.Add(Me.txtBPSystolicMin)
        Me.pnlVitals.Controls.Add(Me.lblBPStandingMax)
        Me.pnlVitals.Controls.Add(Me.lblBPStandingMin)
        Me.pnlVitals.Controls.Add(Me.lblBPSittingMax)
        Me.pnlVitals.Controls.Add(Me.txtStatureMin)
        Me.pnlVitals.Controls.Add(Me.lblBPSittingMin)
        Me.pnlVitals.Controls.Add(Me.txtTemperatureMin)
        Me.pnlVitals.Controls.Add(Me.Label1)
        Me.pnlVitals.Controls.Add(Me.lblWeightMin)
        Me.pnlVitals.Controls.Add(Me.lblTempratureMin)
        Me.pnlVitals.Controls.Add(Me.lblHeightMin)
        Me.pnlVitals.Controls.Add(Me.Label10)
        Me.pnlVitals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlVitals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlVitals.Location = New System.Drawing.Point(0, 0)
        Me.pnlVitals.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlVitals.Name = "pnlVitals"
        Me.pnlVitals.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlVitals.Size = New System.Drawing.Size(661, 383)
        Me.pnlVitals.TabIndex = 0
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(496, 360)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(58, 11)
        Me.Label38.TabIndex = 150
        Me.Label38.Text = "(centimeters)"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(436, 360)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(36, 11)
        Me.Label37.TabIndex = 151
        Me.Label37.Text = "(inches)"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(496, 318)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(58, 11)
        Me.Label36.TabIndex = 150
        Me.Label36.Text = "(centimeters)"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(436, 318)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(36, 11)
        Me.Label35.TabIndex = 151
        Me.Label35.Text = "(inches)"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(216, 360)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(58, 11)
        Me.Label40.TabIndex = 150
        Me.Label40.Text = "(centimeters)"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(156, 360)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(36, 11)
        Me.Label39.TabIndex = 151
        Me.Label39.Text = "(inches)"
        '
        'label54
        '
        Me.label54.AutoSize = True
        Me.label54.BackColor = System.Drawing.Color.Transparent
        Me.label54.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label54.Location = New System.Drawing.Point(215, 318)
        Me.label54.Name = "label54"
        Me.label54.Size = New System.Drawing.Size(58, 11)
        Me.label54.TabIndex = 150
        Me.label54.Text = "(centimeters)"
        '
        'label53
        '
        Me.label53.AutoSize = True
        Me.label53.BackColor = System.Drawing.Color.Transparent
        Me.label53.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label53.Location = New System.Drawing.Point(155, 318)
        Me.label53.Name = "label53"
        Me.label53.Size = New System.Drawing.Size(36, 11)
        Me.label53.TabIndex = 151
        Me.label53.Text = "(inches)"
        '
        'txtStatureMaxInInch
        '
        Me.txtStatureMaxInInch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatureMaxInInch.Location = New System.Drawing.Point(422, 336)
        Me.txtStatureMaxInInch.MaxLength = 5
        Me.txtStatureMaxInInch.Name = "txtStatureMaxInInch"
        Me.txtStatureMaxInInch.Size = New System.Drawing.Size(67, 22)
        Me.txtStatureMaxInInch.TabIndex = 36
        Me.txtStatureMaxInInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtStatureMinInInch
        '
        Me.txtStatureMinInInch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatureMinInInch.Location = New System.Drawing.Point(140, 336)
        Me.txtStatureMinInInch.MaxLength = 5
        Me.txtStatureMinInInch.Name = "txtStatureMinInInch"
        Me.txtStatureMinInInch.Size = New System.Drawing.Size(68, 22)
        Me.txtStatureMinInInch.TabIndex = 34
        Me.txtStatureMinInInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtHeadCircumMaxInInch
        '
        Me.txtHeadCircumMaxInInch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadCircumMaxInInch.Location = New System.Drawing.Point(422, 296)
        Me.txtHeadCircumMaxInInch.MaxLength = 5
        Me.txtHeadCircumMaxInInch.Name = "txtHeadCircumMaxInInch"
        Me.txtHeadCircumMaxInInch.Size = New System.Drawing.Size(67, 22)
        Me.txtHeadCircumMaxInInch.TabIndex = 32
        Me.txtHeadCircumMaxInInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtHeadCircumMinInInch
        '
        Me.txtHeadCircumMinInInch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadCircumMinInInch.Location = New System.Drawing.Point(140, 296)
        Me.txtHeadCircumMinInInch.MaxLength = 5
        Me.txtHeadCircumMinInInch.Name = "txtHeadCircumMinInInch"
        Me.txtHeadCircumMinInInch.Size = New System.Drawing.Size(68, 22)
        Me.txtHeadCircumMinInInch.TabIndex = 30
        Me.txtHeadCircumMinInInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTemperatureMaxInCel
        '
        Me.txtTemperatureMaxInCel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTemperatureMaxInCel.Location = New System.Drawing.Point(492, 258)
        Me.txtTemperatureMaxInCel.MaxLength = 5
        Me.txtTemperatureMaxInCel.Name = "txtTemperatureMaxInCel"
        Me.txtTemperatureMaxInCel.Size = New System.Drawing.Size(67, 22)
        Me.txtTemperatureMaxInCel.TabIndex = 29
        Me.txtTemperatureMaxInCel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(517, 282)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(18, 11)
        Me.Label34.TabIndex = 143
        Me.Label34.Text = "(C)"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(448, 282)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(17, 11)
        Me.Label33.TabIndex = 144
        Me.Label33.Text = "(F)"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(235, 282)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(18, 11)
        Me.Label31.TabIndex = 143
        Me.Label31.Text = "(C)"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(165, 282)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(17, 11)
        Me.Label32.TabIndex = 144
        Me.Label32.Text = "(F)"
        '
        'txtTemperatureMinIncel
        '
        Me.txtTemperatureMinIncel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTemperatureMinIncel.Location = New System.Drawing.Point(211, 258)
        Me.txtTemperatureMinIncel.MaxLength = 5
        Me.txtTemperatureMinIncel.Name = "txtTemperatureMinIncel"
        Me.txtTemperatureMinIncel.Size = New System.Drawing.Size(68, 22)
        Me.txtTemperatureMinIncel.TabIndex = 27
        Me.txtTemperatureMinIncel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWeightMax
        '
        Me.txtWeightMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeightMax.Location = New System.Drawing.Point(502, 55)
        Me.txtWeightMax.MaxLength = 7
        Me.txtWeightMax.Name = "txtWeightMax"
        Me.txtWeightMax.Size = New System.Drawing.Size(68, 22)
        Me.txtWeightMax.TabIndex = 14
        Me.txtWeightMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWeightMin
        '
        Me.txtWeightMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeightMin.Location = New System.Drawing.Point(221, 55)
        Me.txtWeightMin.MaxLength = 7
        Me.txtWeightMin.Name = "txtWeightMin"
        Me.txtWeightMin.Size = New System.Drawing.Size(68, 22)
        Me.txtWeightMin.TabIndex = 10
        Me.txtWeightMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(597, 80)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(21, 11)
        Me.Label30.TabIndex = 139
        Me.Label30.Text = "(kg)"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(319, 80)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(21, 11)
        Me.Label24.TabIndex = 139
        Me.Label24.Text = "(kg)"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(578, 37)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(58, 11)
        Me.Label23.TabIndex = 137
        Me.Label23.Text = "(centimeters)"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(461, 80)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(20, 11)
        Me.Label29.TabIndex = 137
        Me.Label29.Text = "(oz)"
        '
        'label56
        '
        Me.label56.AutoSize = True
        Me.label56.BackColor = System.Drawing.Color.Transparent
        Me.label56.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label56.Location = New System.Drawing.Point(179, 80)
        Me.label56.Name = "label56"
        Me.label56.Size = New System.Drawing.Size(20, 11)
        Me.label56.TabIndex = 137
        Me.label56.Text = "(oz)"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(518, 37)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(36, 11)
        Me.Label22.TabIndex = 138
        Me.Label22.Text = "(inches)"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(421, 80)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(22, 11)
        Me.Label28.TabIndex = 138
        Me.Label28.Text = "(lbs)"
        '
        'label55
        '
        Me.label55.AutoSize = True
        Me.label55.BackColor = System.Drawing.Color.Transparent
        Me.label55.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label55.Location = New System.Drawing.Point(138, 80)
        Me.label55.Name = "label55"
        Me.label55.Size = New System.Drawing.Size(22, 11)
        Me.label55.TabIndex = 138
        Me.label55.Text = "(lbs)"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(300, 37)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(58, 11)
        Me.Label17.TabIndex = 137
        Me.Label17.Text = "(centimeters)"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(525, 80)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(22, 11)
        Me.Label27.TabIndex = 136
        Me.Label27.Text = "(lbs)"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(244, 80)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(22, 11)
        Me.Label25.TabIndex = 136
        Me.Label25.Text = "(lbs)"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(462, 37)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(18, 11)
        Me.Label21.TabIndex = 135
        Me.Label21.Text = "(in)"
        '
        'txtMaxWtOz
        '
        Me.txtMaxWtOz.ForeColor = System.Drawing.Color.Black
        Me.txtMaxWtOz.Location = New System.Drawing.Point(453, 55)
        Me.txtMaxWtOz.MaxLength = 6
        Me.txtMaxWtOz.Name = "txtMaxWtOz"
        Me.txtMaxWtOz.Size = New System.Drawing.Size(37, 22)
        Me.txtMaxWtOz.TabIndex = 13
        Me.txtMaxWtOz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWtOz
        '
        Me.txtWtOz.ForeColor = System.Drawing.Color.Black
        Me.txtWtOz.Location = New System.Drawing.Point(171, 55)
        Me.txtWtOz.MaxLength = 6
        Me.txtWtOz.Name = "txtWtOz"
        Me.txtWtOz.Size = New System.Drawing.Size(37, 22)
        Me.txtWtOz.TabIndex = 9
        Me.txtWtOz.Tag = "01234"
        Me.txtWtOz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(237, 37)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(36, 11)
        Me.Label26.TabIndex = 138
        Me.Label26.Text = "(inches)"
        '
        'txtMaxWeightlbs
        '
        Me.txtMaxWeightlbs.ForeColor = System.Drawing.Color.Black
        Me.txtMaxWeightlbs.Location = New System.Drawing.Point(422, 55)
        Me.txtMaxWeightlbs.MaxLength = 6
        Me.txtMaxWeightlbs.Name = "txtMaxWeightlbs"
        Me.txtMaxWeightlbs.Size = New System.Drawing.Size(20, 22)
        Me.txtMaxWeightlbs.TabIndex = 12
        Me.txtMaxWeightlbs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(424, 37)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(17, 11)
        Me.Label20.TabIndex = 136
        Me.Label20.Text = "(ft)"
        '
        'txtMaxWeightKg
        '
        Me.txtMaxWeightKg.ForeColor = System.Drawing.Color.Black
        Me.txtMaxWeightKg.Location = New System.Drawing.Point(573, 55)
        Me.txtMaxWeightKg.MaxLength = 7
        Me.txtMaxWeightKg.Name = "txtMaxWeightKg"
        Me.txtMaxWeightKg.Size = New System.Drawing.Size(68, 22)
        Me.txtMaxWeightKg.TabIndex = 15
        Me.txtMaxWeightKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWeightlbs
        '
        Me.txtWeightlbs.ForeColor = System.Drawing.Color.Black
        Me.txtWeightlbs.Location = New System.Drawing.Point(140, 55)
        Me.txtWeightlbs.MaxLength = 6
        Me.txtWeightlbs.Name = "txtWeightlbs"
        Me.txtWeightlbs.Size = New System.Drawing.Size(19, 22)
        Me.txtWeightlbs.TabIndex = 8
        Me.txtWeightlbs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWeightKg
        '
        Me.txtWeightKg.ForeColor = System.Drawing.Color.Black
        Me.txtWeightKg.Location = New System.Drawing.Point(295, 55)
        Me.txtWeightKg.MaxLength = 7
        Me.txtWeightKg.Name = "txtWeightKg"
        Me.txtWeightKg.Size = New System.Drawing.Size(68, 22)
        Me.txtWeightKg.TabIndex = 11
        Me.txtWeightKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(180, 37)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(18, 11)
        Me.Label18.TabIndex = 135
        Me.Label18.Text = "(in)"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(141, 37)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(17, 11)
        Me.Label19.TabIndex = 136
        Me.Label19.Text = "(ft)"
        '
        'txtHeightMaxIncm
        '
        Me.txtHeightMaxIncm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMaxIncm.Location = New System.Drawing.Point(573, 14)
        Me.txtHeightMaxIncm.MaxLength = 5
        Me.txtHeightMaxIncm.Name = "txtHeightMaxIncm"
        Me.txtHeightMaxIncm.Size = New System.Drawing.Size(68, 22)
        Me.txtHeightMaxIncm.TabIndex = 7
        Me.txtHeightMaxIncm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtHeightMaxInInch
        '
        Me.txtHeightMaxInInch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMaxInInch.Location = New System.Drawing.Point(502, 14)
        Me.txtHeightMaxInInch.MaxLength = 5
        Me.txtHeightMaxInInch.Name = "txtHeightMaxInInch"
        Me.txtHeightMaxInInch.Size = New System.Drawing.Size(68, 22)
        Me.txtHeightMaxInInch.TabIndex = 6
        Me.txtHeightMaxInInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtHeightMinIncm
        '
        Me.txtHeightMinIncm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMinIncm.Location = New System.Drawing.Point(295, 14)
        Me.txtHeightMinIncm.MaxLength = 5
        Me.txtHeightMinIncm.Name = "txtHeightMinIncm"
        Me.txtHeightMinIncm.Size = New System.Drawing.Size(68, 22)
        Me.txtHeightMinIncm.TabIndex = 3
        Me.txtHeightMinIncm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtHeightMinInInch
        '
        Me.txtHeightMinInInch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMinInInch.Location = New System.Drawing.Point(221, 14)
        Me.txtHeightMinInInch.MaxLength = 5
        Me.txtHeightMinInInch.Name = "txtHeightMinInInch"
        Me.txtHeightMinInInch.Size = New System.Drawing.Size(68, 22)
        Me.txtHeightMinInInch.TabIndex = 2
        Me.txtHeightMinInInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label111
        '
        Me.Label111.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label111.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label111.Location = New System.Drawing.Point(4, 379)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(653, 1)
        Me.Label111.TabIndex = 47
        Me.Label111.Text = "label2"
        '
        'Label112
        '
        Me.Label112.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label112.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.Location = New System.Drawing.Point(3, 1)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(1, 379)
        Me.Label112.TabIndex = 46
        Me.Label112.Text = "label4"
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label113.Location = New System.Drawing.Point(657, 1)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(1, 379)
        Me.Label113.TabIndex = 45
        Me.Label113.Text = "label3"
        '
        'Label114
        '
        Me.Label114.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label114.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114.Location = New System.Drawing.Point(3, 0)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(655, 1)
        Me.Label114.TabIndex = 44
        Me.Label114.Text = "label1"
        '
        'txtHeightMin
        '
        Me.txtHeightMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMin.Location = New System.Drawing.Point(140, 14)
        Me.txtHeightMin.MaxLength = 1
        Me.txtHeightMin.Name = "txtHeightMin"
        Me.txtHeightMin.Size = New System.Drawing.Size(19, 22)
        Me.txtHeightMin.TabIndex = 0
        Me.txtHeightMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(490, 15)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(12, 14)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = """"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMaxInch
        '
        Me.txtHeightMaxInch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMaxInch.Location = New System.Drawing.Point(453, 14)
        Me.txtHeightMaxInch.MaxLength = 5
        Me.txtHeightMaxInch.Name = "txtHeightMaxInch"
        Me.txtHeightMaxInch.Size = New System.Drawing.Size(37, 22)
        Me.txtHeightMaxInch.TabIndex = 5
        Me.txtHeightMaxInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(443, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(10, 14)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "'"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMinInch
        '
        Me.txtHeightMinInch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMinInch.Location = New System.Drawing.Point(171, 14)
        Me.txtHeightMinInch.MaxLength = 5
        Me.txtHeightMinInch.Name = "txtHeightMinInch"
        Me.txtHeightMinInch.Size = New System.Drawing.Size(37, 22)
        Me.txtHeightMinInch.TabIndex = 1
        Me.txtHeightMinInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(161, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 14)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "'"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseOXmax
        '
        Me.txtPulseOXmax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPulseOXmax.Location = New System.Drawing.Point(422, 162)
        Me.txtPulseOXmax.MaxLength = 3
        Me.txtPulseOXmax.Name = "txtPulseOXmax"
        Me.txtPulseOXmax.Size = New System.Drawing.Size(67, 22)
        Me.txtPulseOXmax.TabIndex = 21
        Me.txtPulseOXmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseOXMax
        '
        Me.lblPulseOXMax.AutoSize = True
        Me.lblPulseOXMax.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseOXMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPulseOXMax.Location = New System.Drawing.Point(383, 166)
        Me.lblPulseOXMax.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPulseOXMax.Name = "lblPulseOXMax"
        Me.lblPulseOXMax.Size = New System.Drawing.Size(36, 14)
        Me.lblPulseOXMax.TabIndex = 37
        Me.lblPulseOXMax.Text = "Max :"
        Me.lblPulseOXMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseOXmin
        '
        Me.txtPulseOXmin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPulseOXmin.Location = New System.Drawing.Point(140, 162)
        Me.txtPulseOXmin.MaxLength = 3
        Me.txtPulseOXmin.Name = "txtPulseOXmin"
        Me.txtPulseOXmin.Size = New System.Drawing.Size(68, 22)
        Me.txtPulseOXmin.TabIndex = 20
        Me.txtPulseOXmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseOXMin
        '
        Me.lblPulseOXMin.AutoSize = True
        Me.lblPulseOXMin.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseOXMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPulseOXMin.Location = New System.Drawing.Point(52, 166)
        Me.lblPulseOXMin.Name = "lblPulseOXMin"
        Me.lblPulseOXMin.Size = New System.Drawing.Size(85, 14)
        Me.lblPulseOXMin.TabIndex = 35
        Me.lblPulseOXMin.Text = "Pulse OX Min :"
        Me.lblPulseOXMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRespRateMax
        '
        Me.txtRespRateMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRespRateMax.Location = New System.Drawing.Point(422, 98)
        Me.txtRespRateMax.MaxLength = 3
        Me.txtRespRateMax.Name = "txtRespRateMax"
        Me.txtRespRateMax.Size = New System.Drawing.Size(67, 22)
        Me.txtRespRateMax.TabIndex = 17
        Me.txtRespRateMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(383, 102)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 14)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Max :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseMax
        '
        Me.txtPulseMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPulseMax.Location = New System.Drawing.Point(422, 130)
        Me.txtPulseMax.MaxLength = 3
        Me.txtPulseMax.Name = "txtPulseMax"
        Me.txtPulseMax.Size = New System.Drawing.Size(67, 22)
        Me.txtPulseMax.TabIndex = 19
        Me.txtPulseMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRespRateMin
        '
        Me.txtRespRateMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRespRateMin.Location = New System.Drawing.Point(140, 98)
        Me.txtRespRateMin.MaxLength = 3
        Me.txtRespRateMin.Name = "txtRespRateMin"
        Me.txtRespRateMin.Size = New System.Drawing.Size(68, 22)
        Me.txtRespRateMin.TabIndex = 16
        Me.txtRespRateMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseMax
        '
        Me.lblPulseMax.AutoSize = True
        Me.lblPulseMax.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPulseMax.Location = New System.Drawing.Point(383, 134)
        Me.lblPulseMax.Name = "lblPulseMax"
        Me.lblPulseMax.Size = New System.Drawing.Size(36, 14)
        Me.lblPulseMax.TabIndex = 33
        Me.lblPulseMax.Text = "Max :"
        Me.lblPulseMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 102)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(126, 14)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Respiratory Rate Min :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseMin
        '
        Me.txtPulseMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPulseMin.Location = New System.Drawing.Point(140, 130)
        Me.txtPulseMin.MaxLength = 3
        Me.txtPulseMin.Name = "txtPulseMin"
        Me.txtPulseMin.Size = New System.Drawing.Size(68, 22)
        Me.txtPulseMin.TabIndex = 18
        Me.txtPulseMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseMin
        '
        Me.lblPulseMin.AutoSize = True
        Me.lblPulseMin.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPulseMin.Location = New System.Drawing.Point(72, 134)
        Me.lblPulseMin.Name = "lblPulseMin"
        Me.lblPulseMin.Size = New System.Drawing.Size(65, 14)
        Me.lblPulseMin.TabIndex = 31
        Me.lblPulseMin.Text = "Pulse Min :"
        Me.lblPulseMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtStatureMax
        '
        Me.txtStatureMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatureMax.Location = New System.Drawing.Point(492, 336)
        Me.txtStatureMax.MaxLength = 5
        Me.txtStatureMax.Name = "txtStatureMax"
        Me.txtStatureMax.Size = New System.Drawing.Size(67, 22)
        Me.txtStatureMax.TabIndex = 37
        Me.txtStatureMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTemperatureMax
        '
        Me.txtTemperatureMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTemperatureMax.Location = New System.Drawing.Point(422, 258)
        Me.txtTemperatureMax.MaxLength = 5
        Me.txtTemperatureMax.Name = "txtTemperatureMax"
        Me.txtTemperatureMax.Size = New System.Drawing.Size(67, 22)
        Me.txtTemperatureMax.TabIndex = 28
        Me.txtTemperatureMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(383, 340)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 14)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "Max :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTempratureMax
        '
        Me.lblTempratureMax.AutoSize = True
        Me.lblTempratureMax.BackColor = System.Drawing.Color.Transparent
        Me.lblTempratureMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTempratureMax.Location = New System.Drawing.Point(383, 262)
        Me.lblTempratureMax.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTempratureMax.Name = "lblTempratureMax"
        Me.lblTempratureMax.Size = New System.Drawing.Size(36, 14)
        Me.lblTempratureMax.TabIndex = 29
        Me.lblTempratureMax.Text = "Max :"
        Me.lblTempratureMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeadCircumMax
        '
        Me.txtHeadCircumMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadCircumMax.Location = New System.Drawing.Point(492, 296)
        Me.txtHeadCircumMax.MaxLength = 5
        Me.txtHeadCircumMax.Name = "txtHeadCircumMax"
        Me.txtHeadCircumMax.Size = New System.Drawing.Size(67, 22)
        Me.txtHeadCircumMax.TabIndex = 33
        Me.txtHeadCircumMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(383, 300)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 14)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Max :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeadCircumMin
        '
        Me.txtHeadCircumMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadCircumMin.Location = New System.Drawing.Point(211, 296)
        Me.txtHeadCircumMin.MaxLength = 5
        Me.txtHeadCircumMin.Name = "txtHeadCircumMin"
        Me.txtHeadCircumMin.Size = New System.Drawing.Size(68, 22)
        Me.txtHeadCircumMin.TabIndex = 31
        Me.txtHeadCircumMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(32, 300)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 14)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Head Circum-Min :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblWeightMax
        '
        Me.lblWeightMax.AutoSize = True
        Me.lblWeightMax.BackColor = System.Drawing.Color.Transparent
        Me.lblWeightMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeightMax.Location = New System.Drawing.Point(383, 59)
        Me.lblWeightMax.Margin = New System.Windows.Forms.Padding(0)
        Me.lblWeightMax.Name = "lblWeightMax"
        Me.lblWeightMax.Size = New System.Drawing.Size(36, 14)
        Me.lblWeightMax.TabIndex = 23
        Me.lblWeightMax.Text = "Max :"
        Me.lblWeightMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMax
        '
        Me.txtHeightMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeightMax.Location = New System.Drawing.Point(422, 14)
        Me.txtHeightMax.MaxLength = 1
        Me.txtHeightMax.Name = "txtHeightMax"
        Me.txtHeightMax.Size = New System.Drawing.Size(20, 22)
        Me.txtHeightMax.TabIndex = 4
        Me.txtHeightMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblHeightMax
        '
        Me.lblHeightMax.AutoSize = True
        Me.lblHeightMax.BackColor = System.Drawing.Color.Transparent
        Me.lblHeightMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeightMax.Location = New System.Drawing.Point(383, 18)
        Me.lblHeightMax.Name = "lblHeightMax"
        Me.lblHeightMax.Size = New System.Drawing.Size(36, 14)
        Me.lblHeightMax.TabIndex = 21
        Me.lblHeightMax.Text = "Max :"
        Me.lblHeightMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBPDiastolicMax
        '
        Me.txtBPDiastolicMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPDiastolicMax.Location = New System.Drawing.Point(140, 226)
        Me.txtBPDiastolicMax.MaxLength = 3
        Me.txtBPDiastolicMax.Name = "txtBPDiastolicMax"
        Me.txtBPDiastolicMax.Size = New System.Drawing.Size(68, 22)
        Me.txtBPDiastolicMax.TabIndex = 24
        Me.txtBPDiastolicMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPDiastolicMin
        '
        Me.txtBPDiastolicMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPDiastolicMin.Location = New System.Drawing.Point(422, 226)
        Me.txtBPDiastolicMin.MaxLength = 3
        Me.txtBPDiastolicMin.Name = "txtBPDiastolicMin"
        Me.txtBPDiastolicMin.Size = New System.Drawing.Size(67, 22)
        Me.txtBPDiastolicMin.TabIndex = 25
        Me.txtBPDiastolicMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPSystolicMax
        '
        Me.txtBPSystolicMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPSystolicMax.Location = New System.Drawing.Point(140, 194)
        Me.txtBPSystolicMax.MaxLength = 3
        Me.txtBPSystolicMax.Name = "txtBPSystolicMax"
        Me.txtBPSystolicMax.Size = New System.Drawing.Size(68, 22)
        Me.txtBPSystolicMax.TabIndex = 22
        Me.txtBPSystolicMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPSystolicMin
        '
        Me.txtBPSystolicMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPSystolicMin.Location = New System.Drawing.Point(422, 194)
        Me.txtBPSystolicMin.MaxLength = 3
        Me.txtBPSystolicMin.Name = "txtBPSystolicMin"
        Me.txtBPSystolicMin.Size = New System.Drawing.Size(67, 22)
        Me.txtBPSystolicMin.TabIndex = 23
        Me.txtBPSystolicMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBPStandingMax
        '
        Me.lblBPStandingMax.AutoSize = True
        Me.lblBPStandingMax.BackColor = System.Drawing.Color.Transparent
        Me.lblBPStandingMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPStandingMax.Location = New System.Drawing.Point(36, 230)
        Me.lblBPStandingMax.Name = "lblBPStandingMax"
        Me.lblBPStandingMax.Size = New System.Drawing.Size(101, 14)
        Me.lblBPStandingMax.TabIndex = 16
        Me.lblBPStandingMax.Text = "BP-Diastolic-Max :"
        Me.lblBPStandingMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPStandingMin
        '
        Me.lblBPStandingMin.AutoSize = True
        Me.lblBPStandingMin.BackColor = System.Drawing.Color.Transparent
        Me.lblBPStandingMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPStandingMin.Location = New System.Drawing.Point(386, 230)
        Me.lblBPStandingMin.Margin = New System.Windows.Forms.Padding(0)
        Me.lblBPStandingMin.Name = "lblBPStandingMin"
        Me.lblBPStandingMin.Size = New System.Drawing.Size(33, 14)
        Me.lblBPStandingMin.TabIndex = 15
        Me.lblBPStandingMin.Text = "Min :"
        Me.lblBPStandingMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPSittingMax
        '
        Me.lblBPSittingMax.AutoSize = True
        Me.lblBPSittingMax.BackColor = System.Drawing.Color.Transparent
        Me.lblBPSittingMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPSittingMax.Location = New System.Drawing.Point(39, 198)
        Me.lblBPSittingMax.Name = "lblBPSittingMax"
        Me.lblBPSittingMax.Size = New System.Drawing.Size(98, 14)
        Me.lblBPSittingMax.TabIndex = 14
        Me.lblBPSittingMax.Text = "BP-Systolic-Max :"
        Me.lblBPSittingMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtStatureMin
        '
        Me.txtStatureMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatureMin.Location = New System.Drawing.Point(211, 336)
        Me.txtStatureMin.MaxLength = 5
        Me.txtStatureMin.Name = "txtStatureMin"
        Me.txtStatureMin.Size = New System.Drawing.Size(68, 22)
        Me.txtStatureMin.TabIndex = 35
        Me.txtStatureMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBPSittingMin
        '
        Me.lblBPSittingMin.AutoSize = True
        Me.lblBPSittingMin.BackColor = System.Drawing.Color.Transparent
        Me.lblBPSittingMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPSittingMin.Location = New System.Drawing.Point(386, 198)
        Me.lblBPSittingMin.Name = "lblBPSittingMin"
        Me.lblBPSittingMin.Size = New System.Drawing.Size(33, 14)
        Me.lblBPSittingMin.TabIndex = 13
        Me.lblBPSittingMin.Text = "Min :"
        Me.lblBPSittingMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTemperatureMin
        '
        Me.txtTemperatureMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTemperatureMin.Location = New System.Drawing.Point(140, 258)
        Me.txtTemperatureMin.MaxLength = 5
        Me.txtTemperatureMin.Name = "txtTemperatureMin"
        Me.txtTemperatureMin.Size = New System.Drawing.Size(68, 22)
        Me.txtTemperatureMin.TabIndex = 26
        Me.txtTemperatureMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(59, 340)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 14)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Stature-Min :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblWeightMin
        '
        Me.lblWeightMin.AutoSize = True
        Me.lblWeightMin.BackColor = System.Drawing.Color.Transparent
        Me.lblWeightMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeightMin.Location = New System.Drawing.Point(36, 59)
        Me.lblWeightMin.Name = "lblWeightMin"
        Me.lblWeightMin.Size = New System.Drawing.Size(101, 14)
        Me.lblWeightMin.TabIndex = 3
        Me.lblWeightMin.Text = "Weight(lbs) Min :"
        Me.lblWeightMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTempratureMin
        '
        Me.lblTempratureMin.AutoSize = True
        Me.lblTempratureMin.BackColor = System.Drawing.Color.Transparent
        Me.lblTempratureMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTempratureMin.Location = New System.Drawing.Point(28, 262)
        Me.lblTempratureMin.Name = "lblTempratureMin"
        Me.lblTempratureMin.Size = New System.Drawing.Size(109, 14)
        Me.lblTempratureMin.TabIndex = 4
        Me.lblTempratureMin.Text = "Temperature-Min :"
        Me.lblTempratureMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHeightMin
        '
        Me.lblHeightMin.AutoSize = True
        Me.lblHeightMin.BackColor = System.Drawing.Color.Transparent
        Me.lblHeightMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeightMin.Location = New System.Drawing.Point(20, 18)
        Me.lblHeightMin.Name = "lblHeightMin"
        Me.lblHeightMin.Size = New System.Drawing.Size(117, 14)
        Me.lblHeightMin.TabIndex = 1
        Me.lblHeightMin.Text = "Height/Length Min :"
        Me.lblHeightMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(207, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(12, 14)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = """"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_tlstrip
        '
        Me.pnl_tlstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlstrip.Controls.Add(Me.tlsNorms)
        Me.pnl_tlstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlstrip.Name = "pnl_tlstrip"
        Me.pnl_tlstrip.Size = New System.Drawing.Size(873, 54)
        Me.pnl_tlstrip.TabIndex = 0
        '
        'tlsNorms
        '
        Me.tlsNorms.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsNorms.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsNorms.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsNorms.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsNorms.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.tsb_RestoreDefaultSetting, Me.tsb_Close})
        Me.tlsNorms.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsNorms.Location = New System.Drawing.Point(0, 0)
        Me.tlsNorms.Name = "tlsNorms"
        Me.tlsNorms.Size = New System.Drawing.Size(873, 53)
        Me.tlsNorms.TabIndex = 0
        Me.tlsNorms.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(66, 50)
        Me.ToolStripButton1.Tag = "SaveandClose"
        Me.ToolStripButton1.Text = "&Save&&Cls"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton1.ToolTipText = "Save and Close"
        '
        'tsb_RestoreDefaultSetting
        '
        Me.tsb_RestoreDefaultSetting.Image = CType(resources.GetObject("tsb_RestoreDefaultSetting.Image"), System.Drawing.Image)
        Me.tsb_RestoreDefaultSetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_RestoreDefaultSetting.Name = "tsb_RestoreDefaultSetting"
        Me.tsb_RestoreDefaultSetting.Size = New System.Drawing.Size(101, 50)
        Me.tsb_RestoreDefaultSetting.Tag = "Close"
        Me.tsb_RestoreDefaultSetting.Text = "&Restore Norms"
        Me.tsb_RestoreDefaultSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_RestoreDefaultSetting.ToolTipText = "Restore Default Vital Norms"
        '
        'tsb_Close
        '
        Me.tsb_Close.Image = CType(resources.GetObject("tsb_Close.Image"), System.Drawing.Image)
        Me.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Close.Name = "tsb_Close"
        Me.tsb_Close.Size = New System.Drawing.Size(43, 50)
        Me.tsb_Close.Tag = "Close"
        Me.tsb_Close.Text = "&Close"
        Me.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 83)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(873, 27)
        Me.Panel2.TabIndex = 46
        '
        'Panel6
        '
        Me.Panel6.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Label65)
        Me.Panel6.Controls.Add(Me.Label89)
        Me.Panel6.Controls.Add(Me.Label85)
        Me.Panel6.Controls.Add(Me.lblNormHeader)
        Me.Panel6.Controls.Add(Me.Label90)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(3, 2)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(867, 22)
        Me.Panel6.TabIndex = 19
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label65.Location = New System.Drawing.Point(1, 21)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(865, 1)
        Me.Label65.TabIndex = 13
        Me.Label65.Text = "label2"
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label89.Location = New System.Drawing.Point(866, 1)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(1, 21)
        Me.Label89.TabIndex = 11
        Me.Label89.Text = "label3"
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label85.Location = New System.Drawing.Point(0, 1)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1, 21)
        Me.Label85.TabIndex = 12
        Me.Label85.Text = "label4"
        '
        'lblNormHeader
        '
        Me.lblNormHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblNormHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNormHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNormHeader.ForeColor = System.Drawing.Color.White
        Me.lblNormHeader.Image = CType(resources.GetObject("lblNormHeader.Image"), System.Drawing.Image)
        Me.lblNormHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblNormHeader.Location = New System.Drawing.Point(0, 1)
        Me.lblNormHeader.Name = "lblNormHeader"
        Me.lblNormHeader.Size = New System.Drawing.Size(867, 21)
        Me.lblNormHeader.TabIndex = 9
        Me.lblNormHeader.Text = "     Norms"
        Me.lblNormHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.Location = New System.Drawing.Point(0, 0)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(867, 1)
        Me.Label90.TabIndex = 10
        Me.Label90.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.chkVitalNorms)
        Me.Panel4.Controls.Add(Me.Label41)
        Me.Panel4.Controls.Add(Me.Label42)
        Me.Panel4.Controls.Add(Me.Label43)
        Me.Panel4.Controls.Add(Me.Label44)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 54)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Panel4.Size = New System.Drawing.Size(873, 29)
        Me.Panel4.TabIndex = 20
        '
        'chkVitalNorms
        '
        Me.chkVitalNorms.AutoSize = True
        Me.chkVitalNorms.Checked = True
        Me.chkVitalNorms.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVitalNorms.Location = New System.Drawing.Point(9, 8)
        Me.chkVitalNorms.Name = "chkVitalNorms"
        Me.chkVitalNorms.Size = New System.Drawing.Size(131, 18)
        Me.chkVitalNorms.TabIndex = 14
        Me.chkVitalNorms.Text = "Enable Vital Norms."
        Me.chkVitalNorms.UseVisualStyleBackColor = True
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(4, 28)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(865, 1)
        Me.Label41.TabIndex = 13
        Me.Label41.Text = "label2"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(3, 4)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 25)
        Me.Label42.TabIndex = 12
        Me.Label42.Text = "label4"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(869, 4)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 25)
        Me.Label43.TabIndex = 11
        Me.Label43.Text = "label3"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(3, 3)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(867, 1)
        Me.Label44.TabIndex = 10
        Me.Label44.Text = "label1"
        '
        'frmVitalNorms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(873, 493)
        Me.Controls.Add(Me.pnlMainVitalsNorms)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.pnl_tlstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVitalNorms"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Vital Norms"
        Me.pnlLeft.ResumeLayout(False)
        Me.Panel19.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnlMainVitalsNorms.ResumeLayout(False)
        Me.pnlVitals.ResumeLayout(False)
        Me.pnlVitals.PerformLayout()
        Me.pnl_tlstrip.ResumeLayout(False)
        Me.pnl_tlstrip.PerformLayout()
        Me.tlsNorms.ResumeLayout(False)
        Me.tlsNorms.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlMainVitalsNorms As System.Windows.Forms.Panel
    Friend WithEvents pnlVitals As System.Windows.Forms.Panel
    Private WithEvents Label111 As System.Windows.Forms.Label
    Private WithEvents Label112 As System.Windows.Forms.Label
    Private WithEvents Label113 As System.Windows.Forms.Label
    Private WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMin As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMaxInch As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMinInch As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPulseOXmax As System.Windows.Forms.TextBox
    Friend WithEvents lblPulseOXMax As System.Windows.Forms.Label
    Friend WithEvents txtPulseOXmin As System.Windows.Forms.TextBox
    Friend WithEvents lblPulseOXMin As System.Windows.Forms.Label
    Friend WithEvents txtPulseMax As System.Windows.Forms.TextBox
    Friend WithEvents lblPulseMax As System.Windows.Forms.Label
    Friend WithEvents txtPulseMin As System.Windows.Forms.TextBox
    Friend WithEvents lblPulseMin As System.Windows.Forms.Label
    Friend WithEvents txtTemperatureMax As System.Windows.Forms.TextBox
    Friend WithEvents lblTempratureMax As System.Windows.Forms.Label
    Friend WithEvents txtWeightMax As System.Windows.Forms.TextBox
    Friend WithEvents lblWeightMax As System.Windows.Forms.Label
    Friend WithEvents txtHeightMax As System.Windows.Forms.TextBox
    Friend WithEvents lblHeightMax As System.Windows.Forms.Label
    Friend WithEvents txtBPDiastolicMax As System.Windows.Forms.TextBox
    Friend WithEvents txtBPDiastolicMin As System.Windows.Forms.TextBox
    Friend WithEvents txtBPSystolicMax As System.Windows.Forms.TextBox
    Friend WithEvents txtBPSystolicMin As System.Windows.Forms.TextBox
    Friend WithEvents lblBPStandingMax As System.Windows.Forms.Label
    Friend WithEvents lblBPStandingMin As System.Windows.Forms.Label
    Friend WithEvents lblBPSittingMax As System.Windows.Forms.Label
    Friend WithEvents lblBPSittingMin As System.Windows.Forms.Label
    Friend WithEvents txtTemperatureMin As System.Windows.Forms.TextBox
    Friend WithEvents txtWeightMin As System.Windows.Forms.TextBox
    Friend WithEvents lblWeightMin As System.Windows.Forms.Label
    Friend WithEvents lblTempratureMin As System.Windows.Forms.Label
    Friend WithEvents lblHeightMin As System.Windows.Forms.Label
    Friend WithEvents pnl_tlstrip As System.Windows.Forms.Panel
    Friend WithEvents tlsNorms As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsb_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtStatureMax As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtHeadCircumMax As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtHeadCircumMin As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtStatureMin As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRespRateMax As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtRespRateMin As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents trvNorms As System.Windows.Forms.TreeView
    Private WithEvents Label174 As System.Windows.Forms.Label
    Private WithEvents Label175 As System.Windows.Forms.Label
    Private WithEvents Label176 As System.Windows.Forms.Label
    Private WithEvents Label177 As System.Windows.Forms.Label
    Friend WithEvents imgCommon As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label89 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents lblNormHeader As System.Windows.Forms.Label
    Friend WithEvents rbFemale As System.Windows.Forms.RadioButton
    Friend WithEvents rbMale As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMaxIncm As System.Windows.Forms.TextBox
    Friend WithEvents txtHeightMaxInInch As System.Windows.Forms.TextBox
    Friend WithEvents txtHeightMinIncm As System.Windows.Forms.TextBox
    Friend WithEvents txtHeightMinInInch As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents label56 As System.Windows.Forms.Label
    Friend WithEvents label55 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtWtOz As System.Windows.Forms.TextBox
    Friend WithEvents txtWeightlbs As System.Windows.Forms.TextBox
    Friend WithEvents txtWeightKg As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtMaxWtOz As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxWeightlbs As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxWeightKg As System.Windows.Forms.TextBox
    Friend WithEvents txtTemperatureMinIncel As System.Windows.Forms.TextBox
    Friend WithEvents txtStatureMaxInInch As System.Windows.Forms.TextBox
    Friend WithEvents txtStatureMinInInch As System.Windows.Forms.TextBox
    Friend WithEvents txtHeadCircumMaxInInch As System.Windows.Forms.TextBox
    Friend WithEvents txtHeadCircumMinInInch As System.Windows.Forms.TextBox
    Friend WithEvents txtTemperatureMaxInCel As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents label54 As System.Windows.Forms.Label
    Friend WithEvents label53 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents chkVitalNorms As System.Windows.Forms.CheckBox
    Friend WithEvents tsb_RestoreDefaultSetting As System.Windows.Forms.ToolStripButton
End Class
