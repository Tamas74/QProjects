<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdvanceGraph
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
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
            Try
                If (IsNothing(gloUC_PatientStrip1) = False) Then
                    gloUC_PatientStrip1.Dispose()
                    gloUC_PatientStrip1 = Nothing
                End If
            Catch ex As Exception

            End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdvanceGraph))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tls_Chart = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlb_lenweiVsAge = New System.Windows.Forms.ToolStripSplitButton()
        Me.tlsmnu_CDClenweiVsAge = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlsmnu_WHOlenweiVsAge = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlb_HeadcircWeightVsAge = New System.Windows.Forms.ToolStripSplitButton()
        Me.tlsmnu_CDCHeadcircWeightVsAge = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlsmnu_WHOHeadcircWeightVsAge = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripSplitButton()
        Me.tlsmnu_CDCBMIforAge = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlsmnu_WHOBMIforAge = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.tlbSplDownSyndrom = New System.Windows.Forms.ToolStripSplitButton()
        Me.tlsmnu_lenage_weigage = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlsmnu_statageandweigage = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlsmnu_Hedacircage = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tblbtn_ZoomIn = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_ZoomOut = New System.Windows.Forms.ToolStripButton()
        Me.tlb_Print = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlBackground = New System.Windows.Forms.Panel()
        Me.pnlChart = New System.Windows.Forms.Panel()
        Me.AxAdvChart = New AxGROWTHCHARTLib.AxGrowthChart()
        Me.HScrollBar = New System.Windows.Forms.HScrollBar()
        Me.VScrollBar = New System.Windows.Forms.VScrollBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.tls_Chart.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlBackground.SuspendLayout()
        Me.pnlChart.SuspendLayout()
        CType(Me.AxAdvChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.tls_Chart)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1028, 56)
        Me.pnlTop.TabIndex = 0
        '
        'tls_Chart
        '
        Me.tls_Chart.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_Chart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_Chart.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Chart.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_Chart.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_Chart.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_lenweiVsAge, Me.tlb_HeadcircWeightVsAge, Me.ToolStripButton4, Me.ToolStripButton3, Me.ToolStripButton8, Me.tlbSplDownSyndrom, Me.ToolStripSeparator1, Me.tblbtn_ZoomIn, Me.tblbtn_ZoomOut, Me.tlb_Print, Me.ToolStripSeparator3, Me.tblbtn_Close_32})
        Me.tls_Chart.Location = New System.Drawing.Point(0, 0)
        Me.tls_Chart.Name = "tls_Chart"
        Me.tls_Chart.Size = New System.Drawing.Size(1028, 53)
        Me.tls_Chart.TabIndex = 0
        Me.tls_Chart.Text = "ToolStrip1"
        '
        'tlb_lenweiVsAge
        '
        Me.tlb_lenweiVsAge.BackColor = System.Drawing.Color.Transparent
        Me.tlb_lenweiVsAge.BackgroundImage = CType(resources.GetObject("tlb_lenweiVsAge.BackgroundImage"), System.Drawing.Image)
        Me.tlb_lenweiVsAge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlb_lenweiVsAge.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsmnu_CDClenweiVsAge, Me.tlsmnu_WHOlenweiVsAge})
        Me.tlb_lenweiVsAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_lenweiVsAge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_lenweiVsAge.Image = CType(resources.GetObject("tlb_lenweiVsAge.Image"), System.Drawing.Image)
        Me.tlb_lenweiVsAge.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_lenweiVsAge.Name = "tlb_lenweiVsAge"
        Me.tlb_lenweiVsAge.Size = New System.Drawing.Size(166, 50)
        Me.tlb_lenweiVsAge.Tag = "Legnth-Weight Vs Age"
        Me.tlb_lenweiVsAge.Text = "&Length-Weight for Age"
        Me.tlb_lenweiVsAge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsmnu_CDClenweiVsAge
        '
        Me.tlsmnu_CDClenweiVsAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsmnu_CDClenweiVsAge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsmnu_CDClenweiVsAge.Image = CType(resources.GetObject("tlsmnu_CDClenweiVsAge.Image"), System.Drawing.Image)
        Me.tlsmnu_CDClenweiVsAge.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlsmnu_CDClenweiVsAge.Name = "tlsmnu_CDClenweiVsAge"
        Me.tlsmnu_CDClenweiVsAge.Size = New System.Drawing.Size(152, 22)
        Me.tlsmnu_CDClenweiVsAge.Tag = "CDC Standard"
        Me.tlsmnu_CDClenweiVsAge.Text = "CDC"
        Me.tlsmnu_CDClenweiVsAge.ToolTipText = "Legnth-Weight Vs Age with CDC Standard"
        '
        'tlsmnu_WHOlenweiVsAge
        '
        Me.tlsmnu_WHOlenweiVsAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsmnu_WHOlenweiVsAge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsmnu_WHOlenweiVsAge.Image = CType(resources.GetObject("tlsmnu_WHOlenweiVsAge.Image"), System.Drawing.Image)
        Me.tlsmnu_WHOlenweiVsAge.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlsmnu_WHOlenweiVsAge.Name = "tlsmnu_WHOlenweiVsAge"
        Me.tlsmnu_WHOlenweiVsAge.Size = New System.Drawing.Size(152, 22)
        Me.tlsmnu_WHOlenweiVsAge.Tag = "WHO Standard"
        Me.tlsmnu_WHOlenweiVsAge.Text = "WHO"
        Me.tlsmnu_WHOlenweiVsAge.ToolTipText = "Legnth-Weight Vs Age with WHO Standard"
        '
        'tlb_HeadcircWeightVsAge
        '
        Me.tlb_HeadcircWeightVsAge.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsmnu_CDCHeadcircWeightVsAge, Me.tlsmnu_WHOHeadcircWeightVsAge})
        Me.tlb_HeadcircWeightVsAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_HeadcircWeightVsAge.Image = CType(resources.GetObject("tlb_HeadcircWeightVsAge.Image"), System.Drawing.Image)
        Me.tlb_HeadcircWeightVsAge.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_HeadcircWeightVsAge.Name = "tlb_HeadcircWeightVsAge"
        Me.tlb_HeadcircWeightVsAge.Size = New System.Drawing.Size(243, 50)
        Me.tlb_HeadcircWeightVsAge.Tag = "HeadCirc-Weight Vs Age"
        Me.tlb_HeadcircWeightVsAge.Text = "&Headcirc for Age-Weight for Length"
        Me.tlb_HeadcircWeightVsAge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_HeadcircWeightVsAge.ToolTipText = "HeadCircumference  for Age-Weight for Length"
        '
        'tlsmnu_CDCHeadcircWeightVsAge
        '
        Me.tlsmnu_CDCHeadcircWeightVsAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsmnu_CDCHeadcircWeightVsAge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsmnu_CDCHeadcircWeightVsAge.Image = CType(resources.GetObject("tlsmnu_CDCHeadcircWeightVsAge.Image"), System.Drawing.Image)
        Me.tlsmnu_CDCHeadcircWeightVsAge.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlsmnu_CDCHeadcircWeightVsAge.Name = "tlsmnu_CDCHeadcircWeightVsAge"
        Me.tlsmnu_CDCHeadcircWeightVsAge.Size = New System.Drawing.Size(152, 22)
        Me.tlsmnu_CDCHeadcircWeightVsAge.Tag = "CDC Standard"
        Me.tlsmnu_CDCHeadcircWeightVsAge.Text = "CDC"
        Me.tlsmnu_CDCHeadcircWeightVsAge.ToolTipText = "Headcirc for Age-Weight for Length with CDC Standard"
        '
        'tlsmnu_WHOHeadcircWeightVsAge
        '
        Me.tlsmnu_WHOHeadcircWeightVsAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsmnu_WHOHeadcircWeightVsAge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsmnu_WHOHeadcircWeightVsAge.Image = CType(resources.GetObject("tlsmnu_WHOHeadcircWeightVsAge.Image"), System.Drawing.Image)
        Me.tlsmnu_WHOHeadcircWeightVsAge.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlsmnu_WHOHeadcircWeightVsAge.Name = "tlsmnu_WHOHeadcircWeightVsAge"
        Me.tlsmnu_WHOHeadcircWeightVsAge.Size = New System.Drawing.Size(152, 22)
        Me.tlsmnu_WHOHeadcircWeightVsAge.Tag = "WHO Standard"
        Me.tlsmnu_WHOHeadcircWeightVsAge.Text = "WHO"
        Me.tlsmnu_WHOHeadcircWeightVsAge.ToolTipText = "HeadCircumference  for Age-Weight for Length with WHO Standard"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsmnu_CDCBMIforAge, Me.tlsmnu_WHOBMIforAge})
        Me.ToolStripButton4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(97, 50)
        Me.ToolStripButton4.Tag = "BMIVsAge"
        Me.ToolStripButton4.Text = "&BMI for Age"
        Me.ToolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton4.ToolTipText = "BMI for Age"
        '
        'tlsmnu_CDCBMIforAge
        '
        Me.tlsmnu_CDCBMIforAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsmnu_CDCBMIforAge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsmnu_CDCBMIforAge.Image = CType(resources.GetObject("tlsmnu_CDCBMIforAge.Image"), System.Drawing.Image)
        Me.tlsmnu_CDCBMIforAge.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlsmnu_CDCBMIforAge.Name = "tlsmnu_CDCBMIforAge"
        Me.tlsmnu_CDCBMIforAge.Size = New System.Drawing.Size(152, 22)
        Me.tlsmnu_CDCBMIforAge.Tag = "CDC Standard"
        Me.tlsmnu_CDCBMIforAge.Text = "CDC"
        Me.tlsmnu_CDCBMIforAge.ToolTipText = "BMI for Age with CDC Standard"
        '
        'tlsmnu_WHOBMIforAge
        '
        Me.tlsmnu_WHOBMIforAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsmnu_WHOBMIforAge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsmnu_WHOBMIforAge.Image = CType(resources.GetObject("tlsmnu_WHOBMIforAge.Image"), System.Drawing.Image)
        Me.tlsmnu_WHOBMIforAge.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlsmnu_WHOBMIforAge.Name = "tlsmnu_WHOBMIforAge"
        Me.tlsmnu_WHOBMIforAge.Size = New System.Drawing.Size(152, 22)
        Me.tlsmnu_WHOBMIforAge.Tag = "WHO Standard"
        Me.tlsmnu_WHOBMIforAge.Text = "WHO"
        Me.tlsmnu_WHOBMIforAge.ToolTipText = "BMI for Age with WHO Standard"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripButton3.BackgroundImage = CType(resources.GetObject("ToolStripButton3.BackgroundImage"), System.Drawing.Image)
        Me.ToolStripButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStripButton3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(157, 50)
        Me.ToolStripButton3.Tag = "Stature-WeightVsAge"
        Me.ToolStripButton3.Text = "&Stature-Weight for Age"
        Me.ToolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripButton8.BackgroundImage = CType(resources.GetObject("ToolStripButton8.BackgroundImage"), System.Drawing.Image)
        Me.ToolStripButton8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStripButton8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"), System.Drawing.Image)
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(128, 50)
        Me.ToolStripButton8.Tag = "HeightVsStature"
        Me.ToolStripButton8.Text = "&Weight for Stature"
        Me.ToolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbSplDownSyndrom
        '
        Me.tlbSplDownSyndrom.BackColor = System.Drawing.Color.Transparent
        Me.tlbSplDownSyndrom.BackgroundImage = CType(resources.GetObject("tlbSplDownSyndrom.BackgroundImage"), System.Drawing.Image)
        Me.tlbSplDownSyndrom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbSplDownSyndrom.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsmnu_lenage_weigage, Me.tlsmnu_statageandweigage, Me.tlsmnu_Hedacircage})
        Me.tlbSplDownSyndrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbSplDownSyndrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbSplDownSyndrom.Image = CType(resources.GetObject("tlbSplDownSyndrom.Image"), System.Drawing.Image)
        Me.tlbSplDownSyndrom.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbSplDownSyndrom.Name = "tlbSplDownSyndrom"
        Me.tlbSplDownSyndrom.Size = New System.Drawing.Size(125, 50)
        Me.tlbSplDownSyndrom.Tag = "Down Syndrome Charts"
        Me.tlbSplDownSyndrom.Text = "Down Syndrome"
        Me.tlbSplDownSyndrom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsmnu_lenage_weigage
        '
        Me.tlsmnu_lenage_weigage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsmnu_lenage_weigage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsmnu_lenage_weigage.Image = CType(resources.GetObject("tlsmnu_lenage_weigage.Image"), System.Drawing.Image)
        Me.tlsmnu_lenage_weigage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlsmnu_lenage_weigage.Name = "tlsmnu_lenage_weigage"
        Me.tlsmnu_lenage_weigage.Size = New System.Drawing.Size(273, 22)
        Me.tlsmnu_lenage_weigage.Tag = "Length for Age and Weight for Age"
        Me.tlsmnu_lenage_weigage.Text = "Length for Age and Weight for Age"
        Me.tlsmnu_lenage_weigage.ToolTipText = "Length for Age and Weight for Age"
        '
        'tlsmnu_statageandweigage
        '
        Me.tlsmnu_statageandweigage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsmnu_statageandweigage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsmnu_statageandweigage.Image = CType(resources.GetObject("tlsmnu_statageandweigage.Image"), System.Drawing.Image)
        Me.tlsmnu_statageandweigage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlsmnu_statageandweigage.Name = "tlsmnu_statageandweigage"
        Me.tlsmnu_statageandweigage.Size = New System.Drawing.Size(273, 22)
        Me.tlsmnu_statageandweigage.Tag = "Stature for Age and Weight for Age"
        Me.tlsmnu_statageandweigage.Text = "Stature for Age and Weight for Age"
        Me.tlsmnu_statageandweigage.ToolTipText = "Stature for Age and Weight for Age"
        '
        'tlsmnu_Hedacircage
        '
        Me.tlsmnu_Hedacircage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsmnu_Hedacircage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsmnu_Hedacircage.Image = CType(resources.GetObject("tlsmnu_Hedacircage.Image"), System.Drawing.Image)
        Me.tlsmnu_Hedacircage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlsmnu_Hedacircage.Name = "tlsmnu_Hedacircage"
        Me.tlsmnu_Hedacircage.Size = New System.Drawing.Size(273, 22)
        Me.tlsmnu_Hedacircage.Tag = "Head circumference for age"
        Me.tlsmnu_Hedacircage.Text = "Head circumference for Age"
        Me.tlsmnu_Hedacircage.ToolTipText = "Head circumference for age"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.AutoSize = False
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 53)
        '
        'tblbtn_ZoomIn
        '
        Me.tblbtn_ZoomIn.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_ZoomIn.BackgroundImage = CType(resources.GetObject("tblbtn_ZoomIn.BackgroundImage"), System.Drawing.Image)
        Me.tblbtn_ZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtn_ZoomIn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_ZoomIn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_ZoomIn.Image = CType(resources.GetObject("tblbtn_ZoomIn.Image"), System.Drawing.Image)
        Me.tblbtn_ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_ZoomIn.Name = "tblbtn_ZoomIn"
        Me.tblbtn_ZoomIn.Size = New System.Drawing.Size(62, 50)
        Me.tblbtn_ZoomIn.Tag = "ZoomIn"
        Me.tblbtn_ZoomIn.Text = "Zoom &In"
        Me.tblbtn_ZoomIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_ZoomOut
        '
        Me.tblbtn_ZoomOut.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_ZoomOut.BackgroundImage = CType(resources.GetObject("tblbtn_ZoomOut.BackgroundImage"), System.Drawing.Image)
        Me.tblbtn_ZoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtn_ZoomOut.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_ZoomOut.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_ZoomOut.Image = CType(resources.GetObject("tblbtn_ZoomOut.Image"), System.Drawing.Image)
        Me.tblbtn_ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_ZoomOut.Name = "tblbtn_ZoomOut"
        Me.tblbtn_ZoomOut.Size = New System.Drawing.Size(72, 50)
        Me.tblbtn_ZoomOut.Tag = "ZoomOut"
        Me.tblbtn_ZoomOut.Text = "Zoom &Out"
        Me.tblbtn_ZoomOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlb_Print
        '
        Me.tlb_Print.BackColor = System.Drawing.Color.Transparent
        Me.tlb_Print.BackgroundImage = CType(resources.GetObject("tlb_Print.BackgroundImage"), System.Drawing.Image)
        Me.tlb_Print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlb_Print.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Print.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_Print.Image = CType(resources.GetObject("tlb_Print.Image"), System.Drawing.Image)
        Me.tlb_Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Print.Name = "tlb_Print"
        Me.tlb_Print.Size = New System.Drawing.Size(41, 50)
        Me.tlb_Print.Tag = "Print"
        Me.tlb_Print.Text = "&Print"
        Me.tlb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.AutoSize = False
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 53)
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Tag = "Close"
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlBackground)
        Me.pnlMain.Controls.Add(Me.HScrollBar)
        Me.pnlMain.Controls.Add(Me.VScrollBar)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(1028, 635)
        Me.pnlMain.TabIndex = 1
        '
        'pnlBackground
        '
        Me.pnlBackground.BackColor = System.Drawing.Color.White
        Me.pnlBackground.Controls.Add(Me.pnlChart)
        Me.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBackground.Location = New System.Drawing.Point(4, 4)
        Me.pnlBackground.Name = "pnlBackground"
        Me.pnlBackground.Size = New System.Drawing.Size(1003, 610)
        Me.pnlBackground.TabIndex = 1
        '
        'pnlChart
        '
        Me.pnlChart.Controls.Add(Me.AxAdvChart)
        Me.pnlChart.Location = New System.Drawing.Point(0, 0)
        Me.pnlChart.Name = "pnlChart"
        Me.pnlChart.Size = New System.Drawing.Size(614, 520)
        Me.pnlChart.TabIndex = 0
        '
        'AxAdvChart
        '
        Me.AxAdvChart.Enabled = True
        Me.AxAdvChart.Location = New System.Drawing.Point(66, 52)
        Me.AxAdvChart.Name = "AxAdvChart"
        Me.AxAdvChart.OcxState = CType(resources.GetObject("AxAdvChart.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxAdvChart.Size = New System.Drawing.Size(192, 192)
        Me.AxAdvChart.TabIndex = 0
        '
        'HScrollBar
        '
        Me.HScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.HScrollBar.Location = New System.Drawing.Point(4, 614)
        Me.HScrollBar.Name = "HScrollBar"
        Me.HScrollBar.Size = New System.Drawing.Size(1003, 17)
        Me.HScrollBar.TabIndex = 3
        '
        'VScrollBar
        '
        Me.VScrollBar.Dock = System.Windows.Forms.DockStyle.Right
        Me.VScrollBar.Location = New System.Drawing.Point(1007, 4)
        Me.VScrollBar.Name = "VScrollBar"
        Me.VScrollBar.Size = New System.Drawing.Size(17, 627)
        Me.VScrollBar.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 631)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1020, 1)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 628)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(1024, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 628)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1022, 1)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "label1"
        '
        'frmAdvanceGraph
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 691)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAdvanceGraph"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Advance Chart"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tls_Chart.ResumeLayout(False)
        Me.tls_Chart.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlBackground.ResumeLayout(False)
        Me.pnlChart.ResumeLayout(False)
        CType(Me.AxAdvChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tls_Chart As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlb_Print As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents HScrollBar As System.Windows.Forms.HScrollBar
    Friend WithEvents VScrollBar As System.Windows.Forms.VScrollBar
    Friend WithEvents pnlBackground As System.Windows.Forms.Panel
    Friend WithEvents pnlChart As System.Windows.Forms.Panel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tblbtn_ZoomIn As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_ZoomOut As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents AxAdvChart As AxGROWTHCHARTLib.AxGrowthChart
    Friend WithEvents tlbSplDownSyndrom As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tlsmnu_lenage_weigage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlsmnu_statageandweigage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlsmnu_Hedacircage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton8 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlb_lenweiVsAge As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tlsmnu_CDClenweiVsAge As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlsmnu_WHOlenweiVsAge As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlb_HeadcircWeightVsAge As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tlsmnu_CDCHeadcircWeightVsAge As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlsmnu_WHOHeadcircWeightVsAge As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tlsmnu_CDCBMIforAge As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlsmnu_WHOBMIforAge As System.Windows.Forms.ToolStripMenuItem
End Class
