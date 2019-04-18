<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCV_PatientRisk
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If (IsNothing(oPatientDetail) = False) Then
                oPatientDetail.Dispose()
                oPatientDetail = Nothing
            End If
            If oRisks IsNot Nothing Then
                oRisks.Dispose()
                oRisks = Nothing
            End If
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCV_PatientRisk))
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.tls_PatientRisk = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlb_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlCVRisk = New System.Windows.Forms.Panel
        Me.C1PatientRisk = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label65 = New System.Windows.Forms.Label
        Me.Label85 = New System.Windows.Forms.Label
        Me.Label89 = New System.Windows.Forms.Label
        Me.Label90 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlFamilyHistory = New System.Windows.Forms.Panel
        Me.C1FamilyHistory = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.pnlSocialHistory = New System.Windows.Forms.Panel
        Me.C1SocialHistory = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.pnlPatientHistory = New System.Windows.Forms.Panel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlToolStrip.SuspendLayout()
        Me.tls_PatientRisk.SuspendLayout()
        Me.pnlCVRisk.SuspendLayout()
        CType(Me.C1PatientRisk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlFamilyHistory.SuspendLayout()
        CType(Me.C1FamilyHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSocialHistory.SuspendLayout()
        CType(Me.C1SocialHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPatientHistory.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tls_PatientRisk)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(665, 54)
        Me.pnlToolStrip.TabIndex = 25
        '
        'tls_PatientRisk
        '
        Me.tls_PatientRisk.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_PatientRisk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_PatientRisk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_PatientRisk.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_PatientRisk.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_Close})
        Me.tls_PatientRisk.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_PatientRisk.Location = New System.Drawing.Point(0, 0)
        Me.tls_PatientRisk.Name = "tls_PatientRisk"
        Me.tls_PatientRisk.Size = New System.Drawing.Size(665, 53)
        Me.tls_PatientRisk.TabIndex = 4
        Me.tls_PatientRisk.Text = "toolStrip1"
        '
        'tlb_Close
        '
        Me.tlb_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_Close.Image = CType(resources.GetObject("tlb_Close.Image"), System.Drawing.Image)
        Me.tlb_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Close.Name = "tlb_Close"
        Me.tlb_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlb_Close.Tag = "Close"
        Me.tlb_Close.Text = "&Close"
        Me.tlb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Close.ToolTipText = "Close"
        '
        'pnlCVRisk
        '
        Me.pnlCVRisk.Controls.Add(Me.C1PatientRisk)
        Me.pnlCVRisk.Controls.Add(Me.Label11)
        Me.pnlCVRisk.Controls.Add(Me.Label12)
        Me.pnlCVRisk.Controls.Add(Me.Label14)
        Me.pnlCVRisk.Controls.Add(Me.Label15)
        Me.pnlCVRisk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCVRisk.Location = New System.Drawing.Point(0, 83)
        Me.pnlCVRisk.Name = "pnlCVRisk"
        Me.pnlCVRisk.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlCVRisk.Size = New System.Drawing.Size(665, 339)
        Me.pnlCVRisk.TabIndex = 26
        '
        'C1PatientRisk
        '
        Me.C1PatientRisk.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1PatientRisk.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1PatientRisk.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1PatientRisk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1PatientRisk.ExtendLastCol = True
        Me.C1PatientRisk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PatientRisk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1PatientRisk.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1PatientRisk.Location = New System.Drawing.Point(4, 1)
        Me.C1PatientRisk.Name = "C1PatientRisk"
        Me.C1PatientRisk.Rows.Count = 5
        Me.C1PatientRisk.Rows.DefaultSize = 19
        Me.C1PatientRisk.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PatientRisk.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1PatientRisk.ShowCellLabels = True
        Me.C1PatientRisk.Size = New System.Drawing.Size(657, 337)
        Me.C1PatientRisk.StyleInfo = resources.GetString("C1PatientRisk.StyleInfo")
        Me.C1PatientRisk.TabIndex = 31
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(4, 338)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(657, 1)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(661, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 338)
        Me.Label12.TabIndex = 37
        Me.Label12.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 338)
        Me.Label14.TabIndex = 34
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(659, 1)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "label1"
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "Cardio vascular Criteria01.ico")
        Me.ImageList.Images.SetKeyName(1, "Vitals.ico")
        Me.ImageList.Images.SetKeyName(2, "Pat Demographic.ico")
        Me.ImageList.Images.SetKeyName(3, "History.ico")
        Me.ImageList.Images.SetKeyName(4, "Drugs.ico")
        Me.ImageList.Images.SetKeyName(5, "Lab.ico")
        Me.ImageList.Images.SetKeyName(6, "Radiology_01.ico")
        Me.ImageList.Images.SetKeyName(7, "Bullet06.ico")
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.Panel1)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 54)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlHeader.Size = New System.Drawing.Size(665, 29)
        Me.pnlHeader.TabIndex = 46
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label65)
        Me.Panel1.Controls.Add(Me.Label85)
        Me.Panel1.Controls.Add(Me.Label89)
        Me.Panel1.Controls.Add(Me.Label90)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(659, 23)
        Me.Panel1.TabIndex = 19
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label65.Location = New System.Drawing.Point(1, 22)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(657, 1)
        Me.Label65.TabIndex = 13
        Me.Label65.Text = "label2"
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label85.Location = New System.Drawing.Point(0, 1)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1, 22)
        Me.Label85.TabIndex = 12
        Me.Label85.Text = "label4"
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label89.Location = New System.Drawing.Point(658, 1)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(1, 22)
        Me.Label89.TabIndex = 11
        Me.Label89.Text = "label3"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.Location = New System.Drawing.Point(0, 0)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(659, 1)
        Me.Label90.TabIndex = 10
        Me.Label90.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(659, 23)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "  Risk Factors"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlFamilyHistory
        '
        Me.pnlFamilyHistory.Controls.Add(Me.C1FamilyHistory)
        Me.pnlFamilyHistory.Controls.Add(Me.Label1)
        Me.pnlFamilyHistory.Controls.Add(Me.Label3)
        Me.pnlFamilyHistory.Controls.Add(Me.Label4)
        Me.pnlFamilyHistory.Controls.Add(Me.Label5)
        Me.pnlFamilyHistory.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlFamilyHistory.Location = New System.Drawing.Point(0, 0)
        Me.pnlFamilyHistory.Name = "pnlFamilyHistory"
        Me.pnlFamilyHistory.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlFamilyHistory.Size = New System.Drawing.Size(324, 167)
        Me.pnlFamilyHistory.TabIndex = 48
        '
        'C1FamilyHistory
        '
        Me.C1FamilyHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1FamilyHistory.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1FamilyHistory.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1FamilyHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FamilyHistory.ExtendLastCol = True
        Me.C1FamilyHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FamilyHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1FamilyHistory.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1FamilyHistory.Location = New System.Drawing.Point(4, 1)
        Me.C1FamilyHistory.Name = "C1FamilyHistory"
        Me.C1FamilyHistory.Rows.Count = 5
        Me.C1FamilyHistory.Rows.DefaultSize = 19
        Me.C1FamilyHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1FamilyHistory.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1FamilyHistory.ShowCellLabels = True
        Me.C1FamilyHistory.Size = New System.Drawing.Size(319, 162)
        Me.C1FamilyHistory.StyleInfo = resources.GetString("C1FamilyHistory.StyleInfo")
        Me.C1FamilyHistory.TabIndex = 31
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 163)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(319, 1)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(323, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 163)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 163)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(321, 1)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "label1"
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(324, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(4, 167)
        Me.Splitter2.TabIndex = 49
        Me.Splitter2.TabStop = False
        '
        'pnlSocialHistory
        '
        Me.pnlSocialHistory.Controls.Add(Me.C1SocialHistory)
        Me.pnlSocialHistory.Controls.Add(Me.Label6)
        Me.pnlSocialHistory.Controls.Add(Me.Label7)
        Me.pnlSocialHistory.Controls.Add(Me.Label8)
        Me.pnlSocialHistory.Controls.Add(Me.Label9)
        Me.pnlSocialHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSocialHistory.Location = New System.Drawing.Point(328, 0)
        Me.pnlSocialHistory.Name = "pnlSocialHistory"
        Me.pnlSocialHistory.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlSocialHistory.Size = New System.Drawing.Size(337, 167)
        Me.pnlSocialHistory.TabIndex = 50
        '
        'C1SocialHistory
        '
        Me.C1SocialHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1SocialHistory.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1SocialHistory.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1SocialHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1SocialHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1SocialHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1SocialHistory.Location = New System.Drawing.Point(1, 1)
        Me.C1SocialHistory.Name = "C1SocialHistory"
        Me.C1SocialHistory.Rows.Count = 5
        Me.C1SocialHistory.Rows.DefaultSize = 19
        Me.C1SocialHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1SocialHistory.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1SocialHistory.ShowCellLabels = True
        Me.C1SocialHistory.Size = New System.Drawing.Size(332, 162)
        Me.C1SocialHistory.StyleInfo = resources.GetString("C1SocialHistory.StyleInfo")
        Me.C1SocialHistory.TabIndex = 31
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(1, 163)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(332, 1)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(333, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 163)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 163)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(334, 1)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = "label1"
        '
        'pnlPatientHistory
        '
        Me.pnlPatientHistory.Controls.Add(Me.pnlSocialHistory)
        Me.pnlPatientHistory.Controls.Add(Me.Splitter2)
        Me.pnlPatientHistory.Controls.Add(Me.pnlFamilyHistory)
        Me.pnlPatientHistory.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPatientHistory.Location = New System.Drawing.Point(0, 426)
        Me.pnlPatientHistory.Name = "pnlPatientHistory"
        Me.pnlPatientHistory.Size = New System.Drawing.Size(665, 167)
        Me.pnlPatientHistory.TabIndex = 51
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 422)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(665, 4)
        Me.Splitter1.TabIndex = 52
        Me.Splitter1.TabStop = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmCV_PatientRisk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(665, 593)
        Me.Controls.Add(Me.pnlCVRisk)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Controls.Add(Me.pnlPatientHistory)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCV_PatientRisk"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cardiovascular Risk Factor"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tls_PatientRisk.ResumeLayout(False)
        Me.tls_PatientRisk.PerformLayout()
        Me.pnlCVRisk.ResumeLayout(False)
        CType(Me.C1PatientRisk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlFamilyHistory.ResumeLayout(False)
        CType(Me.C1FamilyHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSocialHistory.ResumeLayout(False)
        CType(Me.C1SocialHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPatientHistory.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Private WithEvents tls_PatientRisk As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlb_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlCVRisk As System.Windows.Forms.Panel
    Friend WithEvents C1PatientRisk As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label89 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlFamilyHistory As System.Windows.Forms.Panel
    Friend WithEvents C1FamilyHistory As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents pnlSocialHistory As System.Windows.Forms.Panel
    Friend WithEvents C1SocialHistory As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pnlPatientHistory As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
