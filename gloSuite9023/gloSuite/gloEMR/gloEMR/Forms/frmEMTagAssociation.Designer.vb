<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEMTagAssociation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEMTagAssociation))
        Me.tbpXRay = New System.Windows.Forms.TabPage
        Me.pnlOrdersEMFields = New System.Windows.Forms.Panel
        Me.chkXIndepedent = New System.Windows.Forms.CheckBox
        Me.nudXOtherXray = New System.Windows.Forms.NumericUpDown
        Me.chkXperformingPhy = New System.Windows.Forms.CheckBox
        Me.lblOtherXRay = New System.Windows.Forms.Label
        Me.chkXVascularStudieswrisk = New System.Windows.Forms.CheckBox
        Me.chkXVascularStudies = New System.Windows.Forms.CheckBox
        Me.chkXMRI = New System.Windows.Forms.CheckBox
        Me.chkXcatScan = New System.Windows.Forms.CheckBox
        Me.chkXIVP = New System.Windows.Forms.CheckBox
        Me.chkXGIGallablader = New System.Windows.Forms.CheckBox
        Me.chkXTLSpire = New System.Windows.Forms.CheckBox
        Me.chkXDiscographt = New System.Windows.Forms.CheckBox
        Me.chkXDiagosticUltrasound = New System.Windows.Forms.CheckBox
        Me.chkXCspine = New System.Windows.Forms.CheckBox
        Me.chkXHipPelvis = New System.Windows.Forms.CheckBox
        Me.chkXAbdomen = New System.Windows.Forms.CheckBox
        Me.chkXExtrimities = New System.Windows.Forms.CheckBox
        Me.chkXChest = New System.Windows.Forms.CheckBox
        Me.tbpLabs = New System.Windows.Forms.TabPage
        Me.pnlLabsEMFields = New System.Windows.Forms.Panel
        Me.chkLIndependentVisualizationoftest = New System.Windows.Forms.CheckBox
        Me.nudLOtherLabs = New System.Windows.Forms.NumericUpDown
        Me.lblOtherLabs = New System.Windows.Forms.Label
        Me.chkLDiscussionwithperformingPhysician = New System.Windows.Forms.CheckBox
        Me.lblIndependentVisualizationoftest = New System.Windows.Forms.Label
        Me.chkLDeepIncisionalBiopsy = New System.Windows.Forms.CheckBox
        Me.chkLSuperficialbiopsy = New System.Windows.Forms.CheckBox
        Me.chkLTypesandCrossmatch = New System.Windows.Forms.CheckBox
        Me.chkLPT = New System.Windows.Forms.CheckBox
        Me.chkLABGS = New System.Windows.Forms.CheckBox
        Me.chkLCardiacenzymes = New System.Windows.Forms.CheckBox
        Me.chkLChemicalProfile = New System.Windows.Forms.CheckBox
        Me.chkLETOH = New System.Windows.Forms.CheckBox
        Me.chkLElectrolytes = New System.Windows.Forms.CheckBox
        Me.chkLBun = New System.Windows.Forms.CheckBox
        Me.chkLAmylase = New System.Windows.Forms.CheckBox
        Me.chkLPregnancyTest = New System.Windows.Forms.CheckBox
        Me.chkLFlu = New System.Windows.Forms.CheckBox
        Me.chkLLCBC = New System.Windows.Forms.CheckBox
        Me.tbpManagementOption = New System.Windows.Forms.TabPage
        Me.nudMTimeSpent = New System.Windows.Forms.NumericUpDown
        Me.lblMTimeSpent = New System.Windows.Forms.Label
        Me.chkMDecisionofcase = New System.Windows.Forms.CheckBox
        Me.chkMreviewandsummary = New System.Windows.Forms.CheckBox
        Me.chkMDecisiontoobtain = New System.Windows.Forms.CheckBox
        Me.chkMDecisionnottoresuscitate = New System.Windows.Forms.CheckBox
        Me.chkMMajoremergencySurgery = New System.Windows.Forms.CheckBox
        Me.chkMMajorSurgerywrisk = New System.Windows.Forms.CheckBox
        Me.chkMMajorsurgeryworisk = New System.Windows.Forms.CheckBox
        Me.chkMMinorSurgeryWrisk = New System.Windows.Forms.CheckBox
        Me.chkMminorsurgeryWOrisk = New System.Windows.Forms.CheckBox
        Me.chkMClosefx = New System.Windows.Forms.CheckBox
        Me.chkMPhysicalOccupationaltherapy = New System.Windows.Forms.CheckBox
        Me.chkMNuclearMedicine = New System.Windows.Forms.CheckBox
        Me.chkMRespiratoryTreatment = New System.Windows.Forms.CheckBox
        Me.chkMTelemetry = New System.Windows.Forms.CheckBox
        Me.chkMHighRiskmeds = New System.Windows.Forms.CheckBox
        Me.chkMIVmedswadditives = New System.Windows.Forms.CheckBox
        Me.chkMivmeds = New System.Windows.Forms.CheckBox
        Me.chkMPerscripmeds = New System.Windows.Forms.CheckBox
        Me.chkMOTCmeds = New System.Windows.Forms.CheckBox
        Me.tbEMAssociation = New System.Windows.Forms.TabControl
        Me.tbpOtherDiagnosisTest = New System.Windows.Forms.TabPage
        Me.pnlOtherDiagnosticEMFields = New System.Windows.Forms.Panel
        Me.chkOIndependentVisualization = New System.Windows.Forms.CheckBox
        Me.nudODignosisstudies = New System.Windows.Forms.NumericUpDown
        Me.chkODiscuswithPerfoming = New System.Windows.Forms.CheckBox
        Me.lblOtheradditionaldiagnosisstudies = New System.Windows.Forms.Label
        Me.chkOEndoScopewRisk = New System.Windows.Forms.CheckBox
        Me.chkOEndoscopeworisk = New System.Windows.Forms.CheckBox
        Me.chkOCuldcentesis = New System.Windows.Forms.CheckBox
        Me.chkOThoracentesis = New System.Windows.Forms.CheckBox
        Me.chkOLumbarPunctor = New System.Windows.Forms.CheckBox
        Me.chkONuclearScan = New System.Windows.Forms.CheckBox
        Me.chkOPulmonary = New System.Windows.Forms.CheckBox
        Me.chkODopplerFlowStudies = New System.Windows.Forms.CheckBox
        Me.chkOVectorCardiogram = New System.Windows.Forms.CheckBox
        Me.chkOEEG = New System.Windows.Forms.CheckBox
        Me.chkOTreadmill = New System.Windows.Forms.CheckBox
        Me.chkOHolterMonitor = New System.Windows.Forms.CheckBox
        Me.chkOEKG = New System.Windows.Forms.CheckBox
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.CheckBox4 = New System.Windows.Forms.CheckBox
        Me.CheckBox5 = New System.Windows.Forms.CheckBox
        Me.CheckBox6 = New System.Windows.Forms.CheckBox
        Me.CheckBox7 = New System.Windows.Forms.CheckBox
        Me.CheckBox8 = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.CheckBox9 = New System.Windows.Forms.CheckBox
        Me.CheckBox10 = New System.Windows.Forms.CheckBox
        Me.CheckBox11 = New System.Windows.Forms.CheckBox
        Me.CheckBox12 = New System.Windows.Forms.CheckBox
        Me.CheckBox13 = New System.Windows.Forms.CheckBox
        Me.CheckBox14 = New System.Windows.Forms.CheckBox
        Me.CheckBox15 = New System.Windows.Forms.CheckBox
        Me.CheckBox16 = New System.Windows.Forms.CheckBox
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown
        Me.CheckBox17 = New System.Windows.Forms.CheckBox
        Me.CheckBox18 = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.CheckBox19 = New System.Windows.Forms.CheckBox
        Me.CheckBox20 = New System.Windows.Forms.CheckBox
        Me.CheckBox21 = New System.Windows.Forms.CheckBox
        Me.CheckBox22 = New System.Windows.Forms.CheckBox
        Me.CheckBox23 = New System.Windows.Forms.CheckBox
        Me.CheckBox24 = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.CheckBox25 = New System.Windows.Forms.CheckBox
        Me.CheckBox26 = New System.Windows.Forms.CheckBox
        Me.CheckBox27 = New System.Windows.Forms.CheckBox
        Me.CheckBox28 = New System.Windows.Forms.CheckBox
        Me.CheckBox29 = New System.Windows.Forms.CheckBox
        Me.CheckBox30 = New System.Windows.Forms.CheckBox
        Me.CheckBox31 = New System.Windows.Forms.CheckBox
        Me.CheckBox32 = New System.Windows.Forms.CheckBox
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown
        Me.CheckBox33 = New System.Windows.Forms.CheckBox
        Me.CheckBox34 = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.CheckBox35 = New System.Windows.Forms.CheckBox
        Me.CheckBox36 = New System.Windows.Forms.CheckBox
        Me.CheckBox37 = New System.Windows.Forms.CheckBox
        Me.CheckBox38 = New System.Windows.Forms.CheckBox
        Me.CheckBox39 = New System.Windows.Forms.CheckBox
        Me.CheckBox40 = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.CheckBox41 = New System.Windows.Forms.CheckBox
        Me.CheckBox42 = New System.Windows.Forms.CheckBox
        Me.CheckBox43 = New System.Windows.Forms.CheckBox
        Me.CheckBox44 = New System.Windows.Forms.CheckBox
        Me.CheckBox45 = New System.Windows.Forms.CheckBox
        Me.CheckBox46 = New System.Windows.Forms.CheckBox
        Me.CheckBox47 = New System.Windows.Forms.CheckBox
        Me.CheckBox48 = New System.Windows.Forms.CheckBox
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown
        Me.CheckBox49 = New System.Windows.Forms.CheckBox
        Me.CheckBox50 = New System.Windows.Forms.CheckBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.CheckBox51 = New System.Windows.Forms.CheckBox
        Me.CheckBox52 = New System.Windows.Forms.CheckBox
        Me.CheckBox53 = New System.Windows.Forms.CheckBox
        Me.CheckBox54 = New System.Windows.Forms.CheckBox
        Me.CheckBox55 = New System.Windows.Forms.CheckBox
        Me.CheckBox56 = New System.Windows.Forms.CheckBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.CheckBox57 = New System.Windows.Forms.CheckBox
        Me.CheckBox58 = New System.Windows.Forms.CheckBox
        Me.CheckBox59 = New System.Windows.Forms.CheckBox
        Me.CheckBox60 = New System.Windows.Forms.CheckBox
        Me.CheckBox61 = New System.Windows.Forms.CheckBox
        Me.CheckBox62 = New System.Windows.Forms.CheckBox
        Me.CheckBox63 = New System.Windows.Forms.CheckBox
        Me.CheckBox64 = New System.Windows.Forms.CheckBox
        Me.NumericUpDown5 = New System.Windows.Forms.NumericUpDown
        Me.CheckBox65 = New System.Windows.Forms.CheckBox
        Me.CheckBox66 = New System.Windows.Forms.CheckBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.CheckBox67 = New System.Windows.Forms.CheckBox
        Me.CheckBox68 = New System.Windows.Forms.CheckBox
        Me.CheckBox69 = New System.Windows.Forms.CheckBox
        Me.CheckBox70 = New System.Windows.Forms.CheckBox
        Me.CheckBox71 = New System.Windows.Forms.CheckBox
        Me.CheckBox72 = New System.Windows.Forms.CheckBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.CheckBox73 = New System.Windows.Forms.CheckBox
        Me.CheckBox74 = New System.Windows.Forms.CheckBox
        Me.CheckBox75 = New System.Windows.Forms.CheckBox
        Me.CheckBox76 = New System.Windows.Forms.CheckBox
        Me.CheckBox77 = New System.Windows.Forms.CheckBox
        Me.CheckBox78 = New System.Windows.Forms.CheckBox
        Me.CheckBox79 = New System.Windows.Forms.CheckBox
        Me.CheckBox80 = New System.Windows.Forms.CheckBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.tlsDictionary = New gloGlobal.gloToolStripIgnoreFocus
        Me.tb_Refresh = New System.Windows.Forms.ToolStripButton
        Me.tlb_Ok = New System.Windows.Forms.ToolStripButton
        Me.tb_Cancel = New System.Windows.Forms.ToolStripButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.tbpXRay.SuspendLayout()
        Me.pnlOrdersEMFields.SuspendLayout()
        CType(Me.nudXOtherXray, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbpLabs.SuspendLayout()
        Me.pnlLabsEMFields.SuspendLayout()
        CType(Me.nudLOtherLabs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbpManagementOption.SuspendLayout()
        CType(Me.nudMTimeSpent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbEMAssociation.SuspendLayout()
        Me.tbpOtherDiagnosisTest.SuspendLayout()
        Me.pnlOtherDiagnosticEMFields.SuspendLayout()
        CType(Me.nudODignosisstudies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.tlsDictionary.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbpXRay
        '
        Me.tbpXRay.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpXRay.Controls.Add(Me.pnlOrdersEMFields)
        Me.tbpXRay.Controls.Add(Me.chkXVascularStudieswrisk)
        Me.tbpXRay.Controls.Add(Me.chkXVascularStudies)
        Me.tbpXRay.Controls.Add(Me.chkXMRI)
        Me.tbpXRay.Controls.Add(Me.chkXcatScan)
        Me.tbpXRay.Controls.Add(Me.chkXIVP)
        Me.tbpXRay.Controls.Add(Me.chkXGIGallablader)
        Me.tbpXRay.Controls.Add(Me.chkXTLSpire)
        Me.tbpXRay.Controls.Add(Me.chkXDiscographt)
        Me.tbpXRay.Controls.Add(Me.chkXDiagosticUltrasound)
        Me.tbpXRay.Controls.Add(Me.chkXCspine)
        Me.tbpXRay.Controls.Add(Me.chkXHipPelvis)
        Me.tbpXRay.Controls.Add(Me.chkXAbdomen)
        Me.tbpXRay.Controls.Add(Me.chkXExtrimities)
        Me.tbpXRay.Controls.Add(Me.chkXChest)
        Me.tbpXRay.Location = New System.Drawing.Point(4, 23)
        Me.tbpXRay.Name = "tbpXRay"
        Me.tbpXRay.Size = New System.Drawing.Size(566, 430)
        Me.tbpXRay.TabIndex = 3
        Me.tbpXRay.Text = "X-Ray/Order"
        '
        'pnlOrdersEMFields
        '
        Me.pnlOrdersEMFields.Controls.Add(Me.chkXIndepedent)
        Me.pnlOrdersEMFields.Controls.Add(Me.nudXOtherXray)
        Me.pnlOrdersEMFields.Controls.Add(Me.chkXperformingPhy)
        Me.pnlOrdersEMFields.Controls.Add(Me.lblOtherXRay)
        Me.pnlOrdersEMFields.Location = New System.Drawing.Point(102, 269)
        Me.pnlOrdersEMFields.Name = "pnlOrdersEMFields"
        Me.pnlOrdersEMFields.Size = New System.Drawing.Size(275, 111)
        Me.pnlOrdersEMFields.TabIndex = 69
        '
        'chkXIndepedent
        '
        Me.chkXIndepedent.AutoSize = True
        Me.chkXIndepedent.Location = New System.Drawing.Point(13, 13)
        Me.chkXIndepedent.Name = "chkXIndepedent"
        Me.chkXIndepedent.Size = New System.Drawing.Size(205, 18)
        Me.chkXIndepedent.TabIndex = 66
        Me.chkXIndepedent.Text = "Independent visualization of test"
        Me.chkXIndepedent.UseVisualStyleBackColor = True
        '
        'nudXOtherXray
        '
        Me.nudXOtherXray.Location = New System.Drawing.Point(188, 79)
        Me.nudXOtherXray.Name = "nudXOtherXray"
        Me.nudXOtherXray.Size = New System.Drawing.Size(66, 22)
        Me.nudXOtherXray.TabIndex = 68
        Me.nudXOtherXray.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkXperformingPhy
        '
        Me.chkXperformingPhy.AutoSize = True
        Me.chkXperformingPhy.Location = New System.Drawing.Point(13, 43)
        Me.chkXperformingPhy.Name = "chkXperformingPhy"
        Me.chkXperformingPhy.Size = New System.Drawing.Size(223, 18)
        Me.chkXperformingPhy.TabIndex = 65
        Me.chkXperformingPhy.Text = "Discussion with performing physician"
        Me.chkXperformingPhy.UseVisualStyleBackColor = True
        '
        'lblOtherXRay
        '
        Me.lblOtherXRay.AutoSize = True
        Me.lblOtherXRay.Location = New System.Drawing.Point(29, 81)
        Me.lblOtherXRay.Name = "lblOtherXRay"
        Me.lblOtherXRay.Size = New System.Drawing.Size(106, 14)
        Me.lblOtherXRay.TabIndex = 67
        Me.lblOtherXRay.Text = "Other x-rays[0-9]:"
        '
        'chkXVascularStudieswrisk
        '
        Me.chkXVascularStudieswrisk.AutoSize = True
        Me.chkXVascularStudieswrisk.Location = New System.Drawing.Point(290, 246)
        Me.chkXVascularStudieswrisk.Name = "chkXVascularStudieswrisk"
        Me.chkXVascularStudieswrisk.Size = New System.Drawing.Size(152, 18)
        Me.chkXVascularStudieswrisk.TabIndex = 64
        Me.chkXVascularStudieswrisk.Text = "Vascular studies w/ risk"
        Me.chkXVascularStudieswrisk.UseVisualStyleBackColor = True
        '
        'chkXVascularStudies
        '
        Me.chkXVascularStudies.AutoSize = True
        Me.chkXVascularStudies.Location = New System.Drawing.Point(290, 210)
        Me.chkXVascularStudies.Name = "chkXVascularStudies"
        Me.chkXVascularStudies.Size = New System.Drawing.Size(161, 18)
        Me.chkXVascularStudies.TabIndex = 63
        Me.chkXVascularStudies.Text = "Vascular Studies w/o risk"
        Me.chkXVascularStudies.UseVisualStyleBackColor = True
        '
        'chkXMRI
        '
        Me.chkXMRI.AutoSize = True
        Me.chkXMRI.Location = New System.Drawing.Point(290, 174)
        Me.chkXMRI.Name = "chkXMRI"
        Me.chkXMRI.Size = New System.Drawing.Size(54, 18)
        Me.chkXMRI.TabIndex = 62
        Me.chkXMRI.Text = "M.R.I"
        Me.chkXMRI.UseVisualStyleBackColor = True
        '
        'chkXcatScan
        '
        Me.chkXcatScan.AutoSize = True
        Me.chkXcatScan.Location = New System.Drawing.Point(290, 138)
        Me.chkXcatScan.Name = "chkXcatScan"
        Me.chkXcatScan.Size = New System.Drawing.Size(77, 18)
        Me.chkXcatScan.TabIndex = 61
        Me.chkXcatScan.Text = "CAT scan"
        Me.chkXcatScan.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.chkXcatScan.UseVisualStyleBackColor = True
        '
        'chkXIVP
        '
        Me.chkXIVP.AutoSize = True
        Me.chkXIVP.Location = New System.Drawing.Point(290, 102)
        Me.chkXIVP.Name = "chkXIVP"
        Me.chkXIVP.Size = New System.Drawing.Size(57, 18)
        Me.chkXIVP.TabIndex = 60
        Me.chkXIVP.Text = "I.V.P."
        Me.chkXIVP.UseVisualStyleBackColor = True
        '
        'chkXGIGallablader
        '
        Me.chkXGIGallablader.AutoSize = True
        Me.chkXGIGallablader.Location = New System.Drawing.Point(290, 66)
        Me.chkXGIGallablader.Name = "chkXGIGallablader"
        Me.chkXGIGallablader.Size = New System.Drawing.Size(135, 18)
        Me.chkXGIGallablader.TabIndex = 59
        Me.chkXGIGallablader.Text = "GI/Gallbladder series"
        Me.chkXGIGallablader.UseVisualStyleBackColor = True
        '
        'chkXTLSpire
        '
        Me.chkXTLSpire.AutoSize = True
        Me.chkXTLSpire.Location = New System.Drawing.Point(290, 30)
        Me.chkXTLSpire.Name = "chkXTLSpire"
        Me.chkXTLSpire.Size = New System.Drawing.Size(76, 18)
        Me.chkXTLSpire.TabIndex = 58
        Me.chkXTLSpire.Text = "T/L Spire"
        Me.chkXTLSpire.UseVisualStyleBackColor = True
        '
        'chkXDiscographt
        '
        Me.chkXDiscographt.AutoSize = True
        Me.chkXDiscographt.Location = New System.Drawing.Point(115, 246)
        Me.chkXDiscographt.Name = "chkXDiscographt"
        Me.chkXDiscographt.Size = New System.Drawing.Size(91, 18)
        Me.chkXDiscographt.TabIndex = 57
        Me.chkXDiscographt.Text = "Discography"
        Me.chkXDiscographt.UseVisualStyleBackColor = True
        '
        'chkXDiagosticUltrasound
        '
        Me.chkXDiagosticUltrasound.AutoSize = True
        Me.chkXDiagosticUltrasound.Location = New System.Drawing.Point(115, 210)
        Me.chkXDiagosticUltrasound.Name = "chkXDiagosticUltrasound"
        Me.chkXDiagosticUltrasound.Size = New System.Drawing.Size(143, 18)
        Me.chkXDiagosticUltrasound.TabIndex = 56
        Me.chkXDiagosticUltrasound.Text = "Diagnostic Ultrasound"
        Me.chkXDiagosticUltrasound.UseVisualStyleBackColor = True
        '
        'chkXCspine
        '
        Me.chkXCspine.AutoSize = True
        Me.chkXCspine.Location = New System.Drawing.Point(115, 174)
        Me.chkXCspine.Name = "chkXCspine"
        Me.chkXCspine.Size = New System.Drawing.Size(65, 18)
        Me.chkXCspine.TabIndex = 55
        Me.chkXCspine.Text = "C-spine"
        Me.chkXCspine.UseVisualStyleBackColor = True
        '
        'chkXHipPelvis
        '
        Me.chkXHipPelvis.AutoSize = True
        Me.chkXHipPelvis.Location = New System.Drawing.Point(115, 138)
        Me.chkXHipPelvis.Name = "chkXHipPelvis"
        Me.chkXHipPelvis.Size = New System.Drawing.Size(77, 18)
        Me.chkXHipPelvis.TabIndex = 54
        Me.chkXHipPelvis.Text = "Hip/Pelvis"
        Me.chkXHipPelvis.UseVisualStyleBackColor = True
        '
        'chkXAbdomen
        '
        Me.chkXAbdomen.AutoSize = True
        Me.chkXAbdomen.Location = New System.Drawing.Point(116, 102)
        Me.chkXAbdomen.Name = "chkXAbdomen"
        Me.chkXAbdomen.Size = New System.Drawing.Size(79, 18)
        Me.chkXAbdomen.TabIndex = 53
        Me.chkXAbdomen.Text = "Abdomen"
        Me.chkXAbdomen.UseVisualStyleBackColor = True
        '
        'chkXExtrimities
        '
        Me.chkXExtrimities.AutoSize = True
        Me.chkXExtrimities.Location = New System.Drawing.Point(115, 66)
        Me.chkXExtrimities.Name = "chkXExtrimities"
        Me.chkXExtrimities.Size = New System.Drawing.Size(86, 18)
        Me.chkXExtrimities.TabIndex = 52
        Me.chkXExtrimities.Text = "Extremities"
        Me.chkXExtrimities.UseVisualStyleBackColor = True
        '
        'chkXChest
        '
        Me.chkXChest.AutoSize = True
        Me.chkXChest.Location = New System.Drawing.Point(115, 30)
        Me.chkXChest.Name = "chkXChest"
        Me.chkXChest.Size = New System.Drawing.Size(57, 18)
        Me.chkXChest.TabIndex = 51
        Me.chkXChest.Text = "Chest"
        Me.chkXChest.UseVisualStyleBackColor = True
        '
        'tbpLabs
        '
        Me.tbpLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpLabs.Controls.Add(Me.pnlLabsEMFields)
        Me.tbpLabs.Controls.Add(Me.lblIndependentVisualizationoftest)
        Me.tbpLabs.Controls.Add(Me.chkLDeepIncisionalBiopsy)
        Me.tbpLabs.Controls.Add(Me.chkLSuperficialbiopsy)
        Me.tbpLabs.Controls.Add(Me.chkLTypesandCrossmatch)
        Me.tbpLabs.Controls.Add(Me.chkLPT)
        Me.tbpLabs.Controls.Add(Me.chkLABGS)
        Me.tbpLabs.Controls.Add(Me.chkLCardiacenzymes)
        Me.tbpLabs.Controls.Add(Me.chkLChemicalProfile)
        Me.tbpLabs.Controls.Add(Me.chkLETOH)
        Me.tbpLabs.Controls.Add(Me.chkLElectrolytes)
        Me.tbpLabs.Controls.Add(Me.chkLBun)
        Me.tbpLabs.Controls.Add(Me.chkLAmylase)
        Me.tbpLabs.Controls.Add(Me.chkLPregnancyTest)
        Me.tbpLabs.Controls.Add(Me.chkLFlu)
        Me.tbpLabs.Controls.Add(Me.chkLLCBC)
        Me.tbpLabs.Location = New System.Drawing.Point(4, 23)
        Me.tbpLabs.Name = "tbpLabs"
        Me.tbpLabs.Size = New System.Drawing.Size(566, 430)
        Me.tbpLabs.TabIndex = 2
        Me.tbpLabs.Text = "Labs"
        '
        'pnlLabsEMFields
        '
        Me.pnlLabsEMFields.Controls.Add(Me.chkLIndependentVisualizationoftest)
        Me.pnlLabsEMFields.Controls.Add(Me.nudLOtherLabs)
        Me.pnlLabsEMFields.Controls.Add(Me.lblOtherLabs)
        Me.pnlLabsEMFields.Controls.Add(Me.chkLDiscussionwithperformingPhysician)
        Me.pnlLabsEMFields.Location = New System.Drawing.Point(97, 262)
        Me.pnlLabsEMFields.Name = "pnlLabsEMFields"
        Me.pnlLabsEMFields.Size = New System.Drawing.Size(273, 113)
        Me.pnlLabsEMFields.TabIndex = 59
        '
        'chkLIndependentVisualizationoftest
        '
        Me.chkLIndependentVisualizationoftest.AutoSize = True
        Me.chkLIndependentVisualizationoftest.Location = New System.Drawing.Point(15, 12)
        Me.chkLIndependentVisualizationoftest.Name = "chkLIndependentVisualizationoftest"
        Me.chkLIndependentVisualizationoftest.Size = New System.Drawing.Size(205, 18)
        Me.chkLIndependentVisualizationoftest.TabIndex = 57
        Me.chkLIndependentVisualizationoftest.Text = "Independent visualization of test"
        Me.chkLIndependentVisualizationoftest.UseVisualStyleBackColor = True
        '
        'nudLOtherLabs
        '
        Me.nudLOtherLabs.Location = New System.Drawing.Point(185, 83)
        Me.nudLOtherLabs.Name = "nudLOtherLabs"
        Me.nudLOtherLabs.Size = New System.Drawing.Size(57, 22)
        Me.nudLOtherLabs.TabIndex = 58
        Me.nudLOtherLabs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblOtherLabs
        '
        Me.lblOtherLabs.AutoSize = True
        Me.lblOtherLabs.Location = New System.Drawing.Point(58, 85)
        Me.lblOtherLabs.Name = "lblOtherLabs"
        Me.lblOtherLabs.Size = New System.Drawing.Size(99, 14)
        Me.lblOtherLabs.TabIndex = 55
        Me.lblOtherLabs.Text = "Other Labs[0-9]:"
        '
        'chkLDiscussionwithperformingPhysician
        '
        Me.chkLDiscussionwithperformingPhysician.AutoSize = True
        Me.chkLDiscussionwithperformingPhysician.Location = New System.Drawing.Point(15, 47)
        Me.chkLDiscussionwithperformingPhysician.Name = "chkLDiscussionwithperformingPhysician"
        Me.chkLDiscussionwithperformingPhysician.Size = New System.Drawing.Size(223, 18)
        Me.chkLDiscussionwithperformingPhysician.TabIndex = 56
        Me.chkLDiscussionwithperformingPhysician.Text = "Discussion with performing physician"
        Me.chkLDiscussionwithperformingPhysician.UseVisualStyleBackColor = True
        '
        'lblIndependentVisualizationoftest
        '
        Me.lblIndependentVisualizationoftest.AutoSize = True
        Me.lblIndependentVisualizationoftest.Location = New System.Drawing.Point(125, 338)
        Me.lblIndependentVisualizationoftest.Name = "lblIndependentVisualizationoftest"
        Me.lblIndependentVisualizationoftest.Size = New System.Drawing.Size(0, 14)
        Me.lblIndependentVisualizationoftest.TabIndex = 53
        '
        'chkLDeepIncisionalBiopsy
        '
        Me.chkLDeepIncisionalBiopsy.AutoSize = True
        Me.chkLDeepIncisionalBiopsy.Location = New System.Drawing.Point(302, 234)
        Me.chkLDeepIncisionalBiopsy.Name = "chkLDeepIncisionalBiopsy"
        Me.chkLDeepIncisionalBiopsy.Size = New System.Drawing.Size(146, 18)
        Me.chkLDeepIncisionalBiopsy.TabIndex = 50
        Me.chkLDeepIncisionalBiopsy.Text = "Deep/Incisional biopsy"
        Me.chkLDeepIncisionalBiopsy.UseVisualStyleBackColor = True
        '
        'chkLSuperficialbiopsy
        '
        Me.chkLSuperficialbiopsy.AutoSize = True
        Me.chkLSuperficialbiopsy.Location = New System.Drawing.Point(302, 198)
        Me.chkLSuperficialbiopsy.Name = "chkLSuperficialbiopsy"
        Me.chkLSuperficialbiopsy.Size = New System.Drawing.Size(118, 18)
        Me.chkLSuperficialbiopsy.TabIndex = 49
        Me.chkLSuperficialbiopsy.Text = "Superficial biopsy"
        Me.chkLSuperficialbiopsy.UseVisualStyleBackColor = True
        '
        'chkLTypesandCrossmatch
        '
        Me.chkLTypesandCrossmatch.AutoSize = True
        Me.chkLTypesandCrossmatch.Location = New System.Drawing.Point(302, 160)
        Me.chkLTypesandCrossmatch.Name = "chkLTypesandCrossmatch"
        Me.chkLTypesandCrossmatch.Size = New System.Drawing.Size(147, 18)
        Me.chkLTypesandCrossmatch.TabIndex = 48
        Me.chkLTypesandCrossmatch.Text = "Type and cross match"
        Me.chkLTypesandCrossmatch.UseVisualStyleBackColor = True
        '
        'chkLPT
        '
        Me.chkLPT.AutoSize = True
        Me.chkLPT.Location = New System.Drawing.Point(302, 125)
        Me.chkLPT.Name = "chkLPT"
        Me.chkLPT.Size = New System.Drawing.Size(69, 18)
        Me.chkLPT.TabIndex = 47
        Me.chkLPT.Text = "PT/PTT"
        Me.chkLPT.UseVisualStyleBackColor = True
        '
        'chkLABGS
        '
        Me.chkLABGS.AutoSize = True
        Me.chkLABGS.Location = New System.Drawing.Point(302, 95)
        Me.chkLABGS.Name = "chkLABGS"
        Me.chkLABGS.Size = New System.Drawing.Size(56, 18)
        Me.chkLABGS.TabIndex = 46
        Me.chkLABGS.Text = "ABGS"
        Me.chkLABGS.UseVisualStyleBackColor = True
        '
        'chkLCardiacenzymes
        '
        Me.chkLCardiacenzymes.AutoSize = True
        Me.chkLCardiacenzymes.Location = New System.Drawing.Point(302, 65)
        Me.chkLCardiacenzymes.Name = "chkLCardiacenzymes"
        Me.chkLCardiacenzymes.Size = New System.Drawing.Size(115, 18)
        Me.chkLCardiacenzymes.TabIndex = 45
        Me.chkLCardiacenzymes.Text = "Cardiac enzymes"
        Me.chkLCardiacenzymes.UseVisualStyleBackColor = True
        '
        'chkLChemicalProfile
        '
        Me.chkLChemicalProfile.AutoSize = True
        Me.chkLChemicalProfile.Location = New System.Drawing.Point(302, 34)
        Me.chkLChemicalProfile.Name = "chkLChemicalProfile"
        Me.chkLChemicalProfile.Size = New System.Drawing.Size(110, 18)
        Me.chkLChemicalProfile.TabIndex = 44
        Me.chkLChemicalProfile.Text = "Chemical profile"
        Me.chkLChemicalProfile.UseVisualStyleBackColor = True
        '
        'chkLETOH
        '
        Me.chkLETOH.AutoSize = True
        Me.chkLETOH.Location = New System.Drawing.Point(112, 234)
        Me.chkLETOH.Name = "chkLETOH"
        Me.chkLETOH.Size = New System.Drawing.Size(131, 18)
        Me.chkLETOH.TabIndex = 43
        Me.chkLETOH.Text = "ETOH/Drug Screen"
        Me.chkLETOH.UseVisualStyleBackColor = True
        '
        'chkLElectrolytes
        '
        Me.chkLElectrolytes.AutoSize = True
        Me.chkLElectrolytes.Location = New System.Drawing.Point(112, 198)
        Me.chkLElectrolytes.Name = "chkLElectrolytes"
        Me.chkLElectrolytes.Size = New System.Drawing.Size(89, 18)
        Me.chkLElectrolytes.TabIndex = 42
        Me.chkLElectrolytes.Text = "Electrolytes"
        Me.chkLElectrolytes.UseVisualStyleBackColor = True
        '
        'chkLBun
        '
        Me.chkLBun.AutoSize = True
        Me.chkLBun.Location = New System.Drawing.Point(112, 160)
        Me.chkLBun.Name = "chkLBun"
        Me.chkLBun.Size = New System.Drawing.Size(106, 18)
        Me.chkLBun.TabIndex = 41
        Me.chkLBun.Text = "Bun/Creatinine"
        Me.chkLBun.UseVisualStyleBackColor = True
        '
        'chkLAmylase
        '
        Me.chkLAmylase.AutoSize = True
        Me.chkLAmylase.Location = New System.Drawing.Point(112, 125)
        Me.chkLAmylase.Name = "chkLAmylase"
        Me.chkLAmylase.Size = New System.Drawing.Size(70, 18)
        Me.chkLAmylase.TabIndex = 40
        Me.chkLAmylase.Text = "Amylase"
        Me.chkLAmylase.UseVisualStyleBackColor = True
        '
        'chkLPregnancyTest
        '
        Me.chkLPregnancyTest.AutoSize = True
        Me.chkLPregnancyTest.Location = New System.Drawing.Point(112, 95)
        Me.chkLPregnancyTest.Name = "chkLPregnancyTest"
        Me.chkLPregnancyTest.Size = New System.Drawing.Size(112, 18)
        Me.chkLPregnancyTest.TabIndex = 39
        Me.chkLPregnancyTest.Text = "Pregnancy Test"
        Me.chkLPregnancyTest.UseVisualStyleBackColor = True
        '
        'chkLFlu
        '
        Me.chkLFlu.AutoSize = True
        Me.chkLFlu.Location = New System.Drawing.Point(112, 65)
        Me.chkLFlu.Name = "chkLFlu"
        Me.chkLFlu.Size = New System.Drawing.Size(139, 18)
        Me.chkLFlu.TabIndex = 38
        Me.chkLFlu.Text = "Flu/Strep/Mono spot"
        Me.chkLFlu.UseVisualStyleBackColor = True
        '
        'chkLLCBC
        '
        Me.chkLLCBC.AutoSize = True
        Me.chkLLCBC.Location = New System.Drawing.Point(112, 34)
        Me.chkLLCBC.Name = "chkLLCBC"
        Me.chkLLCBC.Size = New System.Drawing.Size(68, 18)
        Me.chkLLCBC.TabIndex = 37
        Me.chkLLCBC.Text = "CBC/UA"
        Me.chkLLCBC.UseVisualStyleBackColor = True
        '
        'tbpManagementOption
        '
        Me.tbpManagementOption.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpManagementOption.Controls.Add(Me.nudMTimeSpent)
        Me.tbpManagementOption.Controls.Add(Me.lblMTimeSpent)
        Me.tbpManagementOption.Controls.Add(Me.chkMDecisionofcase)
        Me.tbpManagementOption.Controls.Add(Me.chkMreviewandsummary)
        Me.tbpManagementOption.Controls.Add(Me.chkMDecisiontoobtain)
        Me.tbpManagementOption.Controls.Add(Me.chkMDecisionnottoresuscitate)
        Me.tbpManagementOption.Controls.Add(Me.chkMMajoremergencySurgery)
        Me.tbpManagementOption.Controls.Add(Me.chkMMajorSurgerywrisk)
        Me.tbpManagementOption.Controls.Add(Me.chkMMajorsurgeryworisk)
        Me.tbpManagementOption.Controls.Add(Me.chkMMinorSurgeryWrisk)
        Me.tbpManagementOption.Controls.Add(Me.chkMminorsurgeryWOrisk)
        Me.tbpManagementOption.Controls.Add(Me.chkMClosefx)
        Me.tbpManagementOption.Controls.Add(Me.chkMPhysicalOccupationaltherapy)
        Me.tbpManagementOption.Controls.Add(Me.chkMNuclearMedicine)
        Me.tbpManagementOption.Controls.Add(Me.chkMRespiratoryTreatment)
        Me.tbpManagementOption.Controls.Add(Me.chkMTelemetry)
        Me.tbpManagementOption.Controls.Add(Me.chkMHighRiskmeds)
        Me.tbpManagementOption.Controls.Add(Me.chkMIVmedswadditives)
        Me.tbpManagementOption.Controls.Add(Me.chkMivmeds)
        Me.tbpManagementOption.Controls.Add(Me.chkMPerscripmeds)
        Me.tbpManagementOption.Controls.Add(Me.chkMOTCmeds)
        Me.tbpManagementOption.Location = New System.Drawing.Point(4, 23)
        Me.tbpManagementOption.Name = "tbpManagementOption"
        Me.tbpManagementOption.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpManagementOption.Size = New System.Drawing.Size(566, 430)
        Me.tbpManagementOption.TabIndex = 1
        Me.tbpManagementOption.Text = "Management Option"
        '
        'nudMTimeSpent
        '
        Me.nudMTimeSpent.Location = New System.Drawing.Point(365, 382)
        Me.nudMTimeSpent.Name = "nudMTimeSpent"
        Me.nudMTimeSpent.Size = New System.Drawing.Size(66, 22)
        Me.nudMTimeSpent.TabIndex = 42
        Me.nudMTimeSpent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMTimeSpent
        '
        Me.lblMTimeSpent.AutoSize = True
        Me.lblMTimeSpent.Location = New System.Drawing.Point(36, 387)
        Me.lblMTimeSpent.Name = "lblMTimeSpent"
        Me.lblMTimeSpent.Size = New System.Drawing.Size(327, 14)
        Me.lblMTimeSpent.TabIndex = 41
        Me.lblMTimeSpent.Text = "Time Spent in conference with patient or family(minutes):"
        '
        'chkMDecisionofcase
        '
        Me.chkMDecisionofcase.AutoSize = True
        Me.chkMDecisionofcase.Location = New System.Drawing.Point(36, 358)
        Me.chkMDecisionofcase.Name = "chkMDecisionofcase"
        Me.chkMDecisionofcase.Size = New System.Drawing.Size(301, 18)
        Me.chkMDecisionofcase.TabIndex = 40
        Me.chkMDecisionofcase.Text = "Decision of case with another health care provider"
        Me.chkMDecisionofcase.UseVisualStyleBackColor = True
        '
        'chkMreviewandsummary
        '
        Me.chkMreviewandsummary.AutoSize = True
        Me.chkMreviewandsummary.Location = New System.Drawing.Point(36, 329)
        Me.chkMreviewandsummary.Name = "chkMreviewandsummary"
        Me.chkMreviewandsummary.Size = New System.Drawing.Size(494, 18)
        Me.chkMreviewandsummary.TabIndex = 39
        Me.chkMreviewandsummary.Text = "Review and summ. old med. records and/or history from someone other than patient"
        Me.chkMreviewandsummary.UseVisualStyleBackColor = True
        '
        'chkMDecisiontoobtain
        '
        Me.chkMDecisiontoobtain.AutoSize = True
        Me.chkMDecisiontoobtain.Location = New System.Drawing.Point(36, 300)
        Me.chkMDecisiontoobtain.Name = "chkMDecisiontoobtain"
        Me.chkMDecisiontoobtain.Size = New System.Drawing.Size(493, 18)
        Me.chkMDecisiontoobtain.TabIndex = 38
        Me.chkMDecisiontoobtain.Text = "Decision to obtain old med. records and/ or history from someone other than patie" & _
            "nt"
        Me.chkMDecisiontoobtain.UseVisualStyleBackColor = True
        '
        'chkMDecisionnottoresuscitate
        '
        Me.chkMDecisionnottoresuscitate.AutoSize = True
        Me.chkMDecisionnottoresuscitate.Location = New System.Drawing.Point(222, 266)
        Me.chkMDecisionnottoresuscitate.Name = "chkMDecisionnottoresuscitate"
        Me.chkMDecisionnottoresuscitate.Size = New System.Drawing.Size(172, 18)
        Me.chkMDecisionnottoresuscitate.TabIndex = 37
        Me.chkMDecisionnottoresuscitate.Text = "Decision not to resuscitate"
        Me.chkMDecisionnottoresuscitate.UseVisualStyleBackColor = True
        '
        'chkMMajoremergencySurgery
        '
        Me.chkMMajoremergencySurgery.AutoSize = True
        Me.chkMMajoremergencySurgery.Location = New System.Drawing.Point(222, 231)
        Me.chkMMajoremergencySurgery.Name = "chkMMajoremergencySurgery"
        Me.chkMMajoremergencySurgery.Size = New System.Drawing.Size(164, 18)
        Me.chkMMajoremergencySurgery.TabIndex = 36
        Me.chkMMajoremergencySurgery.Text = "Major emergency surgery"
        Me.chkMMajoremergencySurgery.UseVisualStyleBackColor = True
        '
        'chkMMajorSurgerywrisk
        '
        Me.chkMMajorSurgerywrisk.AutoSize = True
        Me.chkMMajorSurgerywrisk.Location = New System.Drawing.Point(222, 196)
        Me.chkMMajorSurgerywrisk.Name = "chkMMajorSurgerywrisk"
        Me.chkMMajorSurgerywrisk.Size = New System.Drawing.Size(182, 18)
        Me.chkMMajorSurgerywrisk.TabIndex = 35
        Me.chkMMajorSurgerywrisk.Text = "Major Surgery w/ risk factors"
        Me.chkMMajorSurgerywrisk.UseVisualStyleBackColor = True
        '
        'chkMMajorsurgeryworisk
        '
        Me.chkMMajorsurgeryworisk.AutoSize = True
        Me.chkMMajorsurgeryworisk.Location = New System.Drawing.Point(222, 161)
        Me.chkMMajorsurgeryworisk.Name = "chkMMajorsurgeryworisk"
        Me.chkMMajorsurgeryworisk.Size = New System.Drawing.Size(189, 18)
        Me.chkMMajorsurgeryworisk.TabIndex = 34
        Me.chkMMajorsurgeryworisk.Text = "Major Surgery w/o risk factors"
        Me.chkMMajorsurgeryworisk.UseVisualStyleBackColor = True
        '
        'chkMMinorSurgeryWrisk
        '
        Me.chkMMinorSurgeryWrisk.AutoSize = True
        Me.chkMMinorSurgeryWrisk.Location = New System.Drawing.Point(222, 126)
        Me.chkMMinorSurgeryWrisk.Name = "chkMMinorSurgeryWrisk"
        Me.chkMMinorSurgeryWrisk.Size = New System.Drawing.Size(182, 18)
        Me.chkMMinorSurgeryWrisk.TabIndex = 33
        Me.chkMMinorSurgeryWrisk.Text = "Minor Surgery w/ risk factors"
        Me.chkMMinorSurgeryWrisk.UseVisualStyleBackColor = True
        '
        'chkMminorsurgeryWOrisk
        '
        Me.chkMminorsurgeryWOrisk.AutoSize = True
        Me.chkMminorsurgeryWOrisk.Location = New System.Drawing.Point(222, 91)
        Me.chkMminorsurgeryWOrisk.Name = "chkMminorsurgeryWOrisk"
        Me.chkMminorsurgeryWOrisk.Size = New System.Drawing.Size(184, 18)
        Me.chkMminorsurgeryWOrisk.TabIndex = 32
        Me.chkMminorsurgeryWOrisk.Text = "Minor Surgery w/o risk factor"
        Me.chkMminorsurgeryWOrisk.UseVisualStyleBackColor = True
        '
        'chkMClosefx
        '
        Me.chkMClosefx.AutoSize = True
        Me.chkMClosefx.Location = New System.Drawing.Point(222, 56)
        Me.chkMClosefx.Name = "chkMClosefx"
        Me.chkMClosefx.Size = New System.Drawing.Size(188, 18)
        Me.chkMClosefx.TabIndex = 31
        Me.chkMClosefx.Text = "Closed fx or w/o manipulation"
        Me.chkMClosefx.UseVisualStyleBackColor = True
        '
        'chkMPhysicalOccupationaltherapy
        '
        Me.chkMPhysicalOccupationaltherapy.AutoSize = True
        Me.chkMPhysicalOccupationaltherapy.Location = New System.Drawing.Point(222, 21)
        Me.chkMPhysicalOccupationaltherapy.Name = "chkMPhysicalOccupationaltherapy"
        Me.chkMPhysicalOccupationaltherapy.Size = New System.Drawing.Size(191, 18)
        Me.chkMPhysicalOccupationaltherapy.TabIndex = 30
        Me.chkMPhysicalOccupationaltherapy.Text = "Physical/Occupational Therapy"
        Me.chkMPhysicalOccupationaltherapy.UseVisualStyleBackColor = True
        '
        'chkMNuclearMedicine
        '
        Me.chkMNuclearMedicine.AutoSize = True
        Me.chkMNuclearMedicine.Location = New System.Drawing.Point(36, 266)
        Me.chkMNuclearMedicine.Name = "chkMNuclearMedicine"
        Me.chkMNuclearMedicine.Size = New System.Drawing.Size(117, 18)
        Me.chkMNuclearMedicine.TabIndex = 29
        Me.chkMNuclearMedicine.Text = "Nuclear Medicine"
        Me.chkMNuclearMedicine.UseVisualStyleBackColor = True
        '
        'chkMRespiratoryTreatment
        '
        Me.chkMRespiratoryTreatment.AutoSize = True
        Me.chkMRespiratoryTreatment.Location = New System.Drawing.Point(36, 231)
        Me.chkMRespiratoryTreatment.Name = "chkMRespiratoryTreatment"
        Me.chkMRespiratoryTreatment.Size = New System.Drawing.Size(149, 18)
        Me.chkMRespiratoryTreatment.TabIndex = 28
        Me.chkMRespiratoryTreatment.Text = "Respiratory Treatment"
        Me.chkMRespiratoryTreatment.UseVisualStyleBackColor = True
        '
        'chkMTelemetry
        '
        Me.chkMTelemetry.AutoSize = True
        Me.chkMTelemetry.Location = New System.Drawing.Point(36, 196)
        Me.chkMTelemetry.Name = "chkMTelemetry"
        Me.chkMTelemetry.Size = New System.Drawing.Size(82, 18)
        Me.chkMTelemetry.TabIndex = 27
        Me.chkMTelemetry.Text = "Telemetry"
        Me.chkMTelemetry.UseVisualStyleBackColor = True
        '
        'chkMHighRiskmeds
        '
        Me.chkMHighRiskmeds.AutoSize = True
        Me.chkMHighRiskmeds.Location = New System.Drawing.Point(36, 161)
        Me.chkMHighRiskmeds.Name = "chkMHighRiskmeds"
        Me.chkMHighRiskmeds.Size = New System.Drawing.Size(107, 18)
        Me.chkMHighRiskmeds.TabIndex = 26
        Me.chkMHighRiskmeds.Text = "High Risk meds"
        Me.chkMHighRiskmeds.UseVisualStyleBackColor = True
        '
        'chkMIVmedswadditives
        '
        Me.chkMIVmedswadditives.AutoSize = True
        Me.chkMIVmedswadditives.Location = New System.Drawing.Point(36, 126)
        Me.chkMIVmedswadditives.Name = "chkMIVmedswadditives"
        Me.chkMIVmedswadditives.Size = New System.Drawing.Size(145, 18)
        Me.chkMIVmedswadditives.TabIndex = 25
        Me.chkMIVmedswadditives.Text = "I.V. meds/w additives"
        Me.chkMIVmedswadditives.UseVisualStyleBackColor = True
        '
        'chkMivmeds
        '
        Me.chkMivmeds.AutoSize = True
        Me.chkMivmeds.Location = New System.Drawing.Point(36, 91)
        Me.chkMivmeds.Name = "chkMivmeds"
        Me.chkMivmeds.Size = New System.Drawing.Size(79, 18)
        Me.chkMivmeds.TabIndex = 24
        Me.chkMivmeds.Text = "I.V. meds"
        Me.chkMivmeds.UseVisualStyleBackColor = True
        '
        'chkMPerscripmeds
        '
        Me.chkMPerscripmeds.AutoSize = True
        Me.chkMPerscripmeds.Location = New System.Drawing.Point(36, 56)
        Me.chkMPerscripmeds.Name = "chkMPerscripmeds"
        Me.chkMPerscripmeds.Size = New System.Drawing.Size(119, 18)
        Me.chkMPerscripmeds.TabIndex = 23
        Me.chkMPerscripmeds.Text = "Prescrip/IM meds"
        Me.chkMPerscripmeds.UseVisualStyleBackColor = True
        '
        'chkMOTCmeds
        '
        Me.chkMOTCmeds.AutoSize = True
        Me.chkMOTCmeds.Location = New System.Drawing.Point(36, 21)
        Me.chkMOTCmeds.Name = "chkMOTCmeds"
        Me.chkMOTCmeds.Size = New System.Drawing.Size(83, 18)
        Me.chkMOTCmeds.TabIndex = 22
        Me.chkMOTCmeds.Text = "OTC meds"
        Me.chkMOTCmeds.UseVisualStyleBackColor = True
        '
        'tbEMAssociation
        '
        Me.tbEMAssociation.Controls.Add(Me.tbpManagementOption)
        Me.tbEMAssociation.Controls.Add(Me.tbpLabs)
        Me.tbEMAssociation.Controls.Add(Me.tbpXRay)
        Me.tbEMAssociation.Controls.Add(Me.tbpOtherDiagnosisTest)
        Me.tbEMAssociation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbEMAssociation.Location = New System.Drawing.Point(3, 2)
        Me.tbEMAssociation.Name = "tbEMAssociation"
        Me.tbEMAssociation.SelectedIndex = 0
        Me.tbEMAssociation.Size = New System.Drawing.Size(574, 457)
        Me.tbEMAssociation.TabIndex = 0
        Me.tbEMAssociation.Tag = "Labs"
        '
        'tbpOtherDiagnosisTest
        '
        Me.tbpOtherDiagnosisTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.pnlOtherDiagnosticEMFields)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkOEndoScopewRisk)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkOEndoscopeworisk)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkOCuldcentesis)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkOThoracentesis)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkOLumbarPunctor)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkONuclearScan)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkOPulmonary)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkODopplerFlowStudies)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkOVectorCardiogram)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkOEEG)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkOTreadmill)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkOHolterMonitor)
        Me.tbpOtherDiagnosisTest.Controls.Add(Me.chkOEKG)
        Me.tbpOtherDiagnosisTest.Location = New System.Drawing.Point(4, 23)
        Me.tbpOtherDiagnosisTest.Name = "tbpOtherDiagnosisTest"
        Me.tbpOtherDiagnosisTest.Size = New System.Drawing.Size(566, 430)
        Me.tbpOtherDiagnosisTest.TabIndex = 4
        Me.tbpOtherDiagnosisTest.Text = "Other Diagnostic Tests"
        '
        'pnlOtherDiagnosticEMFields
        '
        Me.pnlOtherDiagnosticEMFields.Controls.Add(Me.chkOIndependentVisualization)
        Me.pnlOtherDiagnosticEMFields.Controls.Add(Me.nudODignosisstudies)
        Me.pnlOtherDiagnosticEMFields.Controls.Add(Me.chkODiscuswithPerfoming)
        Me.pnlOtherDiagnosticEMFields.Controls.Add(Me.lblOtheradditionaldiagnosisstudies)
        Me.pnlOtherDiagnosticEMFields.Location = New System.Drawing.Point(98, 241)
        Me.pnlOtherDiagnosticEMFields.Name = "pnlOtherDiagnosticEMFields"
        Me.pnlOtherDiagnosticEMFields.Size = New System.Drawing.Size(325, 135)
        Me.pnlOtherDiagnosticEMFields.TabIndex = 81
        '
        'chkOIndependentVisualization
        '
        Me.chkOIndependentVisualization.AutoSize = True
        Me.chkOIndependentVisualization.Location = New System.Drawing.Point(14, 15)
        Me.chkOIndependentVisualization.Name = "chkOIndependentVisualization"
        Me.chkOIndependentVisualization.Size = New System.Drawing.Size(205, 18)
        Me.chkOIndependentVisualization.TabIndex = 78
        Me.chkOIndependentVisualization.Text = "Independent visualization of test"
        Me.chkOIndependentVisualization.UseVisualStyleBackColor = True
        '
        'nudODignosisstudies
        '
        Me.nudODignosisstudies.Location = New System.Drawing.Point(242, 77)
        Me.nudODignosisstudies.Name = "nudODignosisstudies"
        Me.nudODignosisstudies.Size = New System.Drawing.Size(58, 22)
        Me.nudODignosisstudies.TabIndex = 80
        Me.nudODignosisstudies.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkODiscuswithPerfoming
        '
        Me.chkODiscuswithPerfoming.AutoSize = True
        Me.chkODiscuswithPerfoming.Location = New System.Drawing.Point(14, 48)
        Me.chkODiscuswithPerfoming.Name = "chkODiscuswithPerfoming"
        Me.chkODiscuswithPerfoming.Size = New System.Drawing.Size(223, 18)
        Me.chkODiscuswithPerfoming.TabIndex = 77
        Me.chkODiscuswithPerfoming.Text = "Discussion with performing physician"
        Me.chkODiscuswithPerfoming.UseVisualStyleBackColor = True
        '
        'lblOtheradditionaldiagnosisstudies
        '
        Me.lblOtheradditionaldiagnosisstudies.AutoSize = True
        Me.lblOtheradditionaldiagnosisstudies.Location = New System.Drawing.Point(14, 81)
        Me.lblOtheradditionaldiagnosisstudies.Name = "lblOtheradditionaldiagnosisstudies"
        Me.lblOtheradditionaldiagnosisstudies.Size = New System.Drawing.Size(220, 14)
        Me.lblOtheradditionaldiagnosisstudies.TabIndex = 79
        Me.lblOtheradditionaldiagnosisstudies.Text = "Other additional diagnosis studies[0-9]:"
        '
        'chkOEndoScopewRisk
        '
        Me.chkOEndoScopewRisk.AutoSize = True
        Me.chkOEndoScopewRisk.Location = New System.Drawing.Point(321, 188)
        Me.chkOEndoScopewRisk.Name = "chkOEndoScopewRisk"
        Me.chkOEndoScopewRisk.Size = New System.Drawing.Size(131, 18)
        Me.chkOEndoScopewRisk.TabIndex = 76
        Me.chkOEndoScopewRisk.Text = "EndoScope w/ Risk"
        Me.chkOEndoScopewRisk.UseVisualStyleBackColor = True
        '
        'chkOEndoscopeworisk
        '
        Me.chkOEndoscopeworisk.AutoSize = True
        Me.chkOEndoscopeworisk.Location = New System.Drawing.Point(321, 155)
        Me.chkOEndoscopeworisk.Name = "chkOEndoscopeworisk"
        Me.chkOEndoscopeworisk.Size = New System.Drawing.Size(133, 18)
        Me.chkOEndoscopeworisk.TabIndex = 75
        Me.chkOEndoscopeworisk.Text = "Endoscope w/o risk"
        Me.chkOEndoscopeworisk.UseVisualStyleBackColor = True
        '
        'chkOCuldcentesis
        '
        Me.chkOCuldcentesis.AutoSize = True
        Me.chkOCuldcentesis.Location = New System.Drawing.Point(321, 122)
        Me.chkOCuldcentesis.Name = "chkOCuldcentesis"
        Me.chkOCuldcentesis.Size = New System.Drawing.Size(100, 18)
        Me.chkOCuldcentesis.TabIndex = 74
        Me.chkOCuldcentesis.Text = "Culdocentesis"
        Me.chkOCuldcentesis.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.chkOCuldcentesis.UseVisualStyleBackColor = True
        '
        'chkOThoracentesis
        '
        Me.chkOThoracentesis.AutoSize = True
        Me.chkOThoracentesis.Location = New System.Drawing.Point(321, 89)
        Me.chkOThoracentesis.Name = "chkOThoracentesis"
        Me.chkOThoracentesis.Size = New System.Drawing.Size(102, 18)
        Me.chkOThoracentesis.TabIndex = 73
        Me.chkOThoracentesis.Text = "Thoracentesis"
        Me.chkOThoracentesis.UseVisualStyleBackColor = True
        '
        'chkOLumbarPunctor
        '
        Me.chkOLumbarPunctor.AutoSize = True
        Me.chkOLumbarPunctor.Location = New System.Drawing.Point(321, 56)
        Me.chkOLumbarPunctor.Name = "chkOLumbarPunctor"
        Me.chkOLumbarPunctor.Size = New System.Drawing.Size(120, 18)
        Me.chkOLumbarPunctor.TabIndex = 72
        Me.chkOLumbarPunctor.Text = "Lumbar puncture"
        Me.chkOLumbarPunctor.UseVisualStyleBackColor = True
        '
        'chkONuclearScan
        '
        Me.chkONuclearScan.AutoSize = True
        Me.chkONuclearScan.Location = New System.Drawing.Point(321, 23)
        Me.chkONuclearScan.Name = "chkONuclearScan"
        Me.chkONuclearScan.Size = New System.Drawing.Size(94, 18)
        Me.chkONuclearScan.TabIndex = 71
        Me.chkONuclearScan.Text = "Nuclear scan"
        Me.chkONuclearScan.UseVisualStyleBackColor = True
        '
        'chkOPulmonary
        '
        Me.chkOPulmonary.AutoSize = True
        Me.chkOPulmonary.Location = New System.Drawing.Point(112, 221)
        Me.chkOPulmonary.Name = "chkOPulmonary"
        Me.chkOPulmonary.Size = New System.Drawing.Size(126, 18)
        Me.chkOPulmonary.TabIndex = 70
        Me.chkOPulmonary.Text = "Pulmonary Studies"
        Me.chkOPulmonary.UseVisualStyleBackColor = True
        '
        'chkODopplerFlowStudies
        '
        Me.chkODopplerFlowStudies.AutoSize = True
        Me.chkODopplerFlowStudies.Location = New System.Drawing.Point(112, 188)
        Me.chkODopplerFlowStudies.Name = "chkODopplerFlowStudies"
        Me.chkODopplerFlowStudies.Size = New System.Drawing.Size(141, 18)
        Me.chkODopplerFlowStudies.TabIndex = 69
        Me.chkODopplerFlowStudies.Text = "Doppler Flow Studies"
        Me.chkODopplerFlowStudies.UseVisualStyleBackColor = True
        '
        'chkOVectorCardiogram
        '
        Me.chkOVectorCardiogram.AutoSize = True
        Me.chkOVectorCardiogram.Location = New System.Drawing.Point(112, 155)
        Me.chkOVectorCardiogram.Name = "chkOVectorCardiogram"
        Me.chkOVectorCardiogram.Size = New System.Drawing.Size(123, 18)
        Me.chkOVectorCardiogram.TabIndex = 68
        Me.chkOVectorCardiogram.Text = "VectorCardiogram"
        Me.chkOVectorCardiogram.UseVisualStyleBackColor = True
        '
        'chkOEEG
        '
        Me.chkOEEG.AutoSize = True
        Me.chkOEEG.Location = New System.Drawing.Point(112, 122)
        Me.chkOEEG.Name = "chkOEEG"
        Me.chkOEEG.Size = New System.Drawing.Size(77, 18)
        Me.chkOEEG.TabIndex = 67
        Me.chkOEEG.Text = "EEG/EMG"
        Me.chkOEEG.UseVisualStyleBackColor = True
        '
        'chkOTreadmill
        '
        Me.chkOTreadmill.AutoSize = True
        Me.chkOTreadmill.Location = New System.Drawing.Point(112, 89)
        Me.chkOTreadmill.Name = "chkOTreadmill"
        Me.chkOTreadmill.Size = New System.Drawing.Size(136, 18)
        Me.chkOTreadmill.TabIndex = 66
        Me.chkOTreadmill.Text = "Treadmill/stress test"
        Me.chkOTreadmill.UseVisualStyleBackColor = True
        '
        'chkOHolterMonitor
        '
        Me.chkOHolterMonitor.AutoSize = True
        Me.chkOHolterMonitor.Location = New System.Drawing.Point(112, 56)
        Me.chkOHolterMonitor.Name = "chkOHolterMonitor"
        Me.chkOHolterMonitor.Size = New System.Drawing.Size(104, 18)
        Me.chkOHolterMonitor.TabIndex = 65
        Me.chkOHolterMonitor.Text = "Holter Monitor"
        Me.chkOHolterMonitor.UseVisualStyleBackColor = True
        '
        'chkOEKG
        '
        Me.chkOEKG.AutoSize = True
        Me.chkOEKG.Location = New System.Drawing.Point(112, 23)
        Me.chkOEKG.Name = "chkOEKG"
        Me.chkOEKG.Size = New System.Drawing.Size(48, 18)
        Me.chkOEKG.TabIndex = 64
        Me.chkOEKG.Text = "EKG"
        Me.chkOEKG.UseVisualStyleBackColor = True
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(265, 415)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(98, 20)
        Me.NumericUpDown1.TabIndex = 58
        Me.NumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(36, 334)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(179, 17)
        Me.CheckBox1.TabIndex = 57
        Me.CheckBox1.Text = "Independent Visualization of test"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(36, 378)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(199, 17)
        Me.CheckBox2.TabIndex = 56
        Me.CheckBox2.Text = "Discussion with performing Physician"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(62, 425)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "Other Labs"
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(199, 279)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(135, 17)
        Me.CheckBox3.TabIndex = 50
        Me.CheckBox3.Text = "Deep/Incisional Biopsy"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(199, 237)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(108, 17)
        Me.CheckBox4.TabIndex = 49
        Me.CheckBox4.Text = "Superficial biopsy"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(199, 196)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(136, 17)
        Me.CheckBox5.TabIndex = 48
        Me.CheckBox5.Text = "Types and cross match"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.Location = New System.Drawing.Point(199, 163)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(66, 17)
        Me.CheckBox6.TabIndex = 47
        Me.CheckBox6.Text = "PT/PTT"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = True
        Me.CheckBox7.Location = New System.Drawing.Point(199, 116)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(55, 17)
        Me.CheckBox7.TabIndex = 46
        Me.CheckBox7.Text = "ABGS"
        Me.CheckBox7.UseVisualStyleBackColor = True
        '
        'CheckBox8
        '
        Me.CheckBox8.AutoSize = True
        Me.CheckBox8.Location = New System.Drawing.Point(199, 75)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(106, 17)
        Me.CheckBox8.TabIndex = 45
        Me.CheckBox8.Text = "Cardiac enzymes"
        Me.CheckBox8.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(48, 338)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 53
        '
        'CheckBox9
        '
        Me.CheckBox9.AutoSize = True
        Me.CheckBox9.Location = New System.Drawing.Point(199, 33)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(101, 17)
        Me.CheckBox9.TabIndex = 44
        Me.CheckBox9.Text = "Chemical Profile"
        Me.CheckBox9.UseVisualStyleBackColor = True
        '
        'CheckBox10
        '
        Me.CheckBox10.AutoSize = True
        Me.CheckBox10.Location = New System.Drawing.Point(49, 279)
        Me.CheckBox10.Name = "CheckBox10"
        Me.CheckBox10.Size = New System.Drawing.Size(121, 17)
        Me.CheckBox10.TabIndex = 43
        Me.CheckBox10.Text = "ETOH/Drug Screen"
        Me.CheckBox10.UseVisualStyleBackColor = True
        '
        'CheckBox11
        '
        Me.CheckBox11.AutoSize = True
        Me.CheckBox11.Location = New System.Drawing.Point(49, 237)
        Me.CheckBox11.Name = "CheckBox11"
        Me.CheckBox11.Size = New System.Drawing.Size(80, 17)
        Me.CheckBox11.TabIndex = 42
        Me.CheckBox11.Text = "Electrolytes"
        Me.CheckBox11.UseVisualStyleBackColor = True
        '
        'CheckBox12
        '
        Me.CheckBox12.AutoSize = True
        Me.CheckBox12.Location = New System.Drawing.Point(49, 196)
        Me.CheckBox12.Name = "CheckBox12"
        Me.CheckBox12.Size = New System.Drawing.Size(97, 17)
        Me.CheckBox12.TabIndex = 41
        Me.CheckBox12.Text = "Bun/Creatinine"
        Me.CheckBox12.UseVisualStyleBackColor = True
        '
        'CheckBox13
        '
        Me.CheckBox13.AutoSize = True
        Me.CheckBox13.Location = New System.Drawing.Point(49, 163)
        Me.CheckBox13.Name = "CheckBox13"
        Me.CheckBox13.Size = New System.Drawing.Size(64, 17)
        Me.CheckBox13.TabIndex = 40
        Me.CheckBox13.Text = "amylase"
        Me.CheckBox13.UseVisualStyleBackColor = True
        '
        'CheckBox14
        '
        Me.CheckBox14.AutoSize = True
        Me.CheckBox14.Location = New System.Drawing.Point(50, 116)
        Me.CheckBox14.Name = "CheckBox14"
        Me.CheckBox14.Size = New System.Drawing.Size(101, 17)
        Me.CheckBox14.TabIndex = 39
        Me.CheckBox14.Text = "Pregnancy Test"
        Me.CheckBox14.UseVisualStyleBackColor = True
        '
        'CheckBox15
        '
        Me.CheckBox15.AutoSize = True
        Me.CheckBox15.Location = New System.Drawing.Point(49, 75)
        Me.CheckBox15.Name = "CheckBox15"
        Me.CheckBox15.Size = New System.Drawing.Size(125, 17)
        Me.CheckBox15.TabIndex = 38
        Me.CheckBox15.Text = "Flu/Strep/Mono spot"
        Me.CheckBox15.UseVisualStyleBackColor = True
        '
        'CheckBox16
        '
        Me.CheckBox16.AutoSize = True
        Me.CheckBox16.Location = New System.Drawing.Point(49, 33)
        Me.CheckBox16.Name = "CheckBox16"
        Me.CheckBox16.Size = New System.Drawing.Size(67, 17)
        Me.CheckBox16.TabIndex = 37
        Me.CheckBox16.Text = "CBC/UA"
        Me.CheckBox16.UseVisualStyleBackColor = True
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Location = New System.Drawing.Point(265, 415)
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(98, 20)
        Me.NumericUpDown2.TabIndex = 58
        Me.NumericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBox17
        '
        Me.CheckBox17.AutoSize = True
        Me.CheckBox17.Location = New System.Drawing.Point(36, 334)
        Me.CheckBox17.Name = "CheckBox17"
        Me.CheckBox17.Size = New System.Drawing.Size(179, 17)
        Me.CheckBox17.TabIndex = 57
        Me.CheckBox17.Text = "Independent Visualization of test"
        Me.CheckBox17.UseVisualStyleBackColor = True
        '
        'CheckBox18
        '
        Me.CheckBox18.AutoSize = True
        Me.CheckBox18.Location = New System.Drawing.Point(36, 378)
        Me.CheckBox18.Name = "CheckBox18"
        Me.CheckBox18.Size = New System.Drawing.Size(199, 17)
        Me.CheckBox18.TabIndex = 56
        Me.CheckBox18.Text = "Discussion with performing Physician"
        Me.CheckBox18.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(62, 425)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Other Labs"
        '
        'CheckBox19
        '
        Me.CheckBox19.AutoSize = True
        Me.CheckBox19.Location = New System.Drawing.Point(199, 279)
        Me.CheckBox19.Name = "CheckBox19"
        Me.CheckBox19.Size = New System.Drawing.Size(135, 17)
        Me.CheckBox19.TabIndex = 50
        Me.CheckBox19.Text = "Deep/Incisional Biopsy"
        Me.CheckBox19.UseVisualStyleBackColor = True
        '
        'CheckBox20
        '
        Me.CheckBox20.AutoSize = True
        Me.CheckBox20.Location = New System.Drawing.Point(199, 237)
        Me.CheckBox20.Name = "CheckBox20"
        Me.CheckBox20.Size = New System.Drawing.Size(108, 17)
        Me.CheckBox20.TabIndex = 49
        Me.CheckBox20.Text = "Superficial biopsy"
        Me.CheckBox20.UseVisualStyleBackColor = True
        '
        'CheckBox21
        '
        Me.CheckBox21.AutoSize = True
        Me.CheckBox21.Location = New System.Drawing.Point(199, 196)
        Me.CheckBox21.Name = "CheckBox21"
        Me.CheckBox21.Size = New System.Drawing.Size(136, 17)
        Me.CheckBox21.TabIndex = 48
        Me.CheckBox21.Text = "Types and cross match"
        Me.CheckBox21.UseVisualStyleBackColor = True
        '
        'CheckBox22
        '
        Me.CheckBox22.AutoSize = True
        Me.CheckBox22.Location = New System.Drawing.Point(199, 163)
        Me.CheckBox22.Name = "CheckBox22"
        Me.CheckBox22.Size = New System.Drawing.Size(66, 17)
        Me.CheckBox22.TabIndex = 47
        Me.CheckBox22.Text = "PT/PTT"
        Me.CheckBox22.UseVisualStyleBackColor = True
        '
        'CheckBox23
        '
        Me.CheckBox23.AutoSize = True
        Me.CheckBox23.Location = New System.Drawing.Point(199, 116)
        Me.CheckBox23.Name = "CheckBox23"
        Me.CheckBox23.Size = New System.Drawing.Size(55, 17)
        Me.CheckBox23.TabIndex = 46
        Me.CheckBox23.Text = "ABGS"
        Me.CheckBox23.UseVisualStyleBackColor = True
        '
        'CheckBox24
        '
        Me.CheckBox24.AutoSize = True
        Me.CheckBox24.Location = New System.Drawing.Point(199, 75)
        Me.CheckBox24.Name = "CheckBox24"
        Me.CheckBox24.Size = New System.Drawing.Size(106, 17)
        Me.CheckBox24.TabIndex = 45
        Me.CheckBox24.Text = "Cardiac enzymes"
        Me.CheckBox24.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(48, 338)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 13)
        Me.Label4.TabIndex = 53
        '
        'CheckBox25
        '
        Me.CheckBox25.AutoSize = True
        Me.CheckBox25.Location = New System.Drawing.Point(199, 33)
        Me.CheckBox25.Name = "CheckBox25"
        Me.CheckBox25.Size = New System.Drawing.Size(101, 17)
        Me.CheckBox25.TabIndex = 44
        Me.CheckBox25.Text = "Chemical Profile"
        Me.CheckBox25.UseVisualStyleBackColor = True
        '
        'CheckBox26
        '
        Me.CheckBox26.AutoSize = True
        Me.CheckBox26.Location = New System.Drawing.Point(49, 279)
        Me.CheckBox26.Name = "CheckBox26"
        Me.CheckBox26.Size = New System.Drawing.Size(121, 17)
        Me.CheckBox26.TabIndex = 43
        Me.CheckBox26.Text = "ETOH/Drug Screen"
        Me.CheckBox26.UseVisualStyleBackColor = True
        '
        'CheckBox27
        '
        Me.CheckBox27.AutoSize = True
        Me.CheckBox27.Location = New System.Drawing.Point(49, 237)
        Me.CheckBox27.Name = "CheckBox27"
        Me.CheckBox27.Size = New System.Drawing.Size(80, 17)
        Me.CheckBox27.TabIndex = 42
        Me.CheckBox27.Text = "Electrolytes"
        Me.CheckBox27.UseVisualStyleBackColor = True
        '
        'CheckBox28
        '
        Me.CheckBox28.AutoSize = True
        Me.CheckBox28.Location = New System.Drawing.Point(49, 196)
        Me.CheckBox28.Name = "CheckBox28"
        Me.CheckBox28.Size = New System.Drawing.Size(97, 17)
        Me.CheckBox28.TabIndex = 41
        Me.CheckBox28.Text = "Bun/Creatinine"
        Me.CheckBox28.UseVisualStyleBackColor = True
        '
        'CheckBox29
        '
        Me.CheckBox29.AutoSize = True
        Me.CheckBox29.Location = New System.Drawing.Point(49, 163)
        Me.CheckBox29.Name = "CheckBox29"
        Me.CheckBox29.Size = New System.Drawing.Size(64, 17)
        Me.CheckBox29.TabIndex = 40
        Me.CheckBox29.Text = "amylase"
        Me.CheckBox29.UseVisualStyleBackColor = True
        '
        'CheckBox30
        '
        Me.CheckBox30.AutoSize = True
        Me.CheckBox30.Location = New System.Drawing.Point(50, 116)
        Me.CheckBox30.Name = "CheckBox30"
        Me.CheckBox30.Size = New System.Drawing.Size(101, 17)
        Me.CheckBox30.TabIndex = 39
        Me.CheckBox30.Text = "Pregnancy Test"
        Me.CheckBox30.UseVisualStyleBackColor = True
        '
        'CheckBox31
        '
        Me.CheckBox31.AutoSize = True
        Me.CheckBox31.Location = New System.Drawing.Point(49, 75)
        Me.CheckBox31.Name = "CheckBox31"
        Me.CheckBox31.Size = New System.Drawing.Size(125, 17)
        Me.CheckBox31.TabIndex = 38
        Me.CheckBox31.Text = "Flu/Strep/Mono spot"
        Me.CheckBox31.UseVisualStyleBackColor = True
        '
        'CheckBox32
        '
        Me.CheckBox32.AutoSize = True
        Me.CheckBox32.Location = New System.Drawing.Point(49, 33)
        Me.CheckBox32.Name = "CheckBox32"
        Me.CheckBox32.Size = New System.Drawing.Size(67, 17)
        Me.CheckBox32.TabIndex = 37
        Me.CheckBox32.Text = "CBC/UA"
        Me.CheckBox32.UseVisualStyleBackColor = True
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.Location = New System.Drawing.Point(265, 415)
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(98, 20)
        Me.NumericUpDown3.TabIndex = 58
        Me.NumericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBox33
        '
        Me.CheckBox33.AutoSize = True
        Me.CheckBox33.Location = New System.Drawing.Point(36, 334)
        Me.CheckBox33.Name = "CheckBox33"
        Me.CheckBox33.Size = New System.Drawing.Size(179, 17)
        Me.CheckBox33.TabIndex = 57
        Me.CheckBox33.Text = "Independent Visualization of test"
        Me.CheckBox33.UseVisualStyleBackColor = True
        '
        'CheckBox34
        '
        Me.CheckBox34.AutoSize = True
        Me.CheckBox34.Location = New System.Drawing.Point(36, 378)
        Me.CheckBox34.Name = "CheckBox34"
        Me.CheckBox34.Size = New System.Drawing.Size(199, 17)
        Me.CheckBox34.TabIndex = 56
        Me.CheckBox34.Text = "Discussion with performing Physician"
        Me.CheckBox34.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(62, 425)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "Other Labs"
        '
        'CheckBox35
        '
        Me.CheckBox35.AutoSize = True
        Me.CheckBox35.Location = New System.Drawing.Point(199, 279)
        Me.CheckBox35.Name = "CheckBox35"
        Me.CheckBox35.Size = New System.Drawing.Size(135, 17)
        Me.CheckBox35.TabIndex = 50
        Me.CheckBox35.Text = "Deep/Incisional Biopsy"
        Me.CheckBox35.UseVisualStyleBackColor = True
        '
        'CheckBox36
        '
        Me.CheckBox36.AutoSize = True
        Me.CheckBox36.Location = New System.Drawing.Point(199, 237)
        Me.CheckBox36.Name = "CheckBox36"
        Me.CheckBox36.Size = New System.Drawing.Size(108, 17)
        Me.CheckBox36.TabIndex = 49
        Me.CheckBox36.Text = "Superficial biopsy"
        Me.CheckBox36.UseVisualStyleBackColor = True
        '
        'CheckBox37
        '
        Me.CheckBox37.AutoSize = True
        Me.CheckBox37.Location = New System.Drawing.Point(199, 196)
        Me.CheckBox37.Name = "CheckBox37"
        Me.CheckBox37.Size = New System.Drawing.Size(136, 17)
        Me.CheckBox37.TabIndex = 48
        Me.CheckBox37.Text = "Types and cross match"
        Me.CheckBox37.UseVisualStyleBackColor = True
        '
        'CheckBox38
        '
        Me.CheckBox38.AutoSize = True
        Me.CheckBox38.Location = New System.Drawing.Point(199, 163)
        Me.CheckBox38.Name = "CheckBox38"
        Me.CheckBox38.Size = New System.Drawing.Size(66, 17)
        Me.CheckBox38.TabIndex = 47
        Me.CheckBox38.Text = "PT/PTT"
        Me.CheckBox38.UseVisualStyleBackColor = True
        '
        'CheckBox39
        '
        Me.CheckBox39.AutoSize = True
        Me.CheckBox39.Location = New System.Drawing.Point(199, 116)
        Me.CheckBox39.Name = "CheckBox39"
        Me.CheckBox39.Size = New System.Drawing.Size(55, 17)
        Me.CheckBox39.TabIndex = 46
        Me.CheckBox39.Text = "ABGS"
        Me.CheckBox39.UseVisualStyleBackColor = True
        '
        'CheckBox40
        '
        Me.CheckBox40.AutoSize = True
        Me.CheckBox40.Location = New System.Drawing.Point(199, 75)
        Me.CheckBox40.Name = "CheckBox40"
        Me.CheckBox40.Size = New System.Drawing.Size(106, 17)
        Me.CheckBox40.TabIndex = 45
        Me.CheckBox40.Text = "Cardiac enzymes"
        Me.CheckBox40.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(48, 338)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(0, 13)
        Me.Label6.TabIndex = 53
        '
        'CheckBox41
        '
        Me.CheckBox41.AutoSize = True
        Me.CheckBox41.Location = New System.Drawing.Point(199, 33)
        Me.CheckBox41.Name = "CheckBox41"
        Me.CheckBox41.Size = New System.Drawing.Size(101, 17)
        Me.CheckBox41.TabIndex = 44
        Me.CheckBox41.Text = "Chemical Profile"
        Me.CheckBox41.UseVisualStyleBackColor = True
        '
        'CheckBox42
        '
        Me.CheckBox42.AutoSize = True
        Me.CheckBox42.Location = New System.Drawing.Point(49, 279)
        Me.CheckBox42.Name = "CheckBox42"
        Me.CheckBox42.Size = New System.Drawing.Size(121, 17)
        Me.CheckBox42.TabIndex = 43
        Me.CheckBox42.Text = "ETOH/Drug Screen"
        Me.CheckBox42.UseVisualStyleBackColor = True
        '
        'CheckBox43
        '
        Me.CheckBox43.AutoSize = True
        Me.CheckBox43.Location = New System.Drawing.Point(49, 237)
        Me.CheckBox43.Name = "CheckBox43"
        Me.CheckBox43.Size = New System.Drawing.Size(80, 17)
        Me.CheckBox43.TabIndex = 42
        Me.CheckBox43.Text = "Electrolytes"
        Me.CheckBox43.UseVisualStyleBackColor = True
        '
        'CheckBox44
        '
        Me.CheckBox44.AutoSize = True
        Me.CheckBox44.Location = New System.Drawing.Point(49, 196)
        Me.CheckBox44.Name = "CheckBox44"
        Me.CheckBox44.Size = New System.Drawing.Size(97, 17)
        Me.CheckBox44.TabIndex = 41
        Me.CheckBox44.Text = "Bun/Creatinine"
        Me.CheckBox44.UseVisualStyleBackColor = True
        '
        'CheckBox45
        '
        Me.CheckBox45.AutoSize = True
        Me.CheckBox45.Location = New System.Drawing.Point(49, 163)
        Me.CheckBox45.Name = "CheckBox45"
        Me.CheckBox45.Size = New System.Drawing.Size(64, 17)
        Me.CheckBox45.TabIndex = 40
        Me.CheckBox45.Text = "amylase"
        Me.CheckBox45.UseVisualStyleBackColor = True
        '
        'CheckBox46
        '
        Me.CheckBox46.AutoSize = True
        Me.CheckBox46.Location = New System.Drawing.Point(50, 116)
        Me.CheckBox46.Name = "CheckBox46"
        Me.CheckBox46.Size = New System.Drawing.Size(101, 17)
        Me.CheckBox46.TabIndex = 39
        Me.CheckBox46.Text = "Pregnancy Test"
        Me.CheckBox46.UseVisualStyleBackColor = True
        '
        'CheckBox47
        '
        Me.CheckBox47.AutoSize = True
        Me.CheckBox47.Location = New System.Drawing.Point(49, 75)
        Me.CheckBox47.Name = "CheckBox47"
        Me.CheckBox47.Size = New System.Drawing.Size(125, 17)
        Me.CheckBox47.TabIndex = 38
        Me.CheckBox47.Text = "Flu/Strep/Mono spot"
        Me.CheckBox47.UseVisualStyleBackColor = True
        '
        'CheckBox48
        '
        Me.CheckBox48.AutoSize = True
        Me.CheckBox48.Location = New System.Drawing.Point(49, 33)
        Me.CheckBox48.Name = "CheckBox48"
        Me.CheckBox48.Size = New System.Drawing.Size(67, 17)
        Me.CheckBox48.TabIndex = 37
        Me.CheckBox48.Text = "CBC/UA"
        Me.CheckBox48.UseVisualStyleBackColor = True
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.Location = New System.Drawing.Point(265, 415)
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(98, 20)
        Me.NumericUpDown4.TabIndex = 58
        Me.NumericUpDown4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBox49
        '
        Me.CheckBox49.AutoSize = True
        Me.CheckBox49.Location = New System.Drawing.Point(36, 334)
        Me.CheckBox49.Name = "CheckBox49"
        Me.CheckBox49.Size = New System.Drawing.Size(179, 17)
        Me.CheckBox49.TabIndex = 57
        Me.CheckBox49.Text = "Independent Visualization of test"
        Me.CheckBox49.UseVisualStyleBackColor = True
        '
        'CheckBox50
        '
        Me.CheckBox50.AutoSize = True
        Me.CheckBox50.Location = New System.Drawing.Point(36, 378)
        Me.CheckBox50.Name = "CheckBox50"
        Me.CheckBox50.Size = New System.Drawing.Size(199, 17)
        Me.CheckBox50.TabIndex = 56
        Me.CheckBox50.Text = "Discussion with performing Physician"
        Me.CheckBox50.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(62, 425)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 13)
        Me.Label7.TabIndex = 55
        Me.Label7.Text = "Other Labs"
        '
        'CheckBox51
        '
        Me.CheckBox51.AutoSize = True
        Me.CheckBox51.Location = New System.Drawing.Point(199, 279)
        Me.CheckBox51.Name = "CheckBox51"
        Me.CheckBox51.Size = New System.Drawing.Size(135, 17)
        Me.CheckBox51.TabIndex = 50
        Me.CheckBox51.Text = "Deep/Incisional Biopsy"
        Me.CheckBox51.UseVisualStyleBackColor = True
        '
        'CheckBox52
        '
        Me.CheckBox52.AutoSize = True
        Me.CheckBox52.Location = New System.Drawing.Point(199, 237)
        Me.CheckBox52.Name = "CheckBox52"
        Me.CheckBox52.Size = New System.Drawing.Size(108, 17)
        Me.CheckBox52.TabIndex = 49
        Me.CheckBox52.Text = "Superficial biopsy"
        Me.CheckBox52.UseVisualStyleBackColor = True
        '
        'CheckBox53
        '
        Me.CheckBox53.AutoSize = True
        Me.CheckBox53.Location = New System.Drawing.Point(199, 196)
        Me.CheckBox53.Name = "CheckBox53"
        Me.CheckBox53.Size = New System.Drawing.Size(136, 17)
        Me.CheckBox53.TabIndex = 48
        Me.CheckBox53.Text = "Types and cross match"
        Me.CheckBox53.UseVisualStyleBackColor = True
        '
        'CheckBox54
        '
        Me.CheckBox54.AutoSize = True
        Me.CheckBox54.Location = New System.Drawing.Point(199, 163)
        Me.CheckBox54.Name = "CheckBox54"
        Me.CheckBox54.Size = New System.Drawing.Size(66, 17)
        Me.CheckBox54.TabIndex = 47
        Me.CheckBox54.Text = "PT/PTT"
        Me.CheckBox54.UseVisualStyleBackColor = True
        '
        'CheckBox55
        '
        Me.CheckBox55.AutoSize = True
        Me.CheckBox55.Location = New System.Drawing.Point(199, 116)
        Me.CheckBox55.Name = "CheckBox55"
        Me.CheckBox55.Size = New System.Drawing.Size(55, 17)
        Me.CheckBox55.TabIndex = 46
        Me.CheckBox55.Text = "ABGS"
        Me.CheckBox55.UseVisualStyleBackColor = True
        '
        'CheckBox56
        '
        Me.CheckBox56.AutoSize = True
        Me.CheckBox56.Location = New System.Drawing.Point(199, 75)
        Me.CheckBox56.Name = "CheckBox56"
        Me.CheckBox56.Size = New System.Drawing.Size(106, 17)
        Me.CheckBox56.TabIndex = 45
        Me.CheckBox56.Text = "Cardiac enzymes"
        Me.CheckBox56.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(48, 338)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(0, 13)
        Me.Label8.TabIndex = 53
        '
        'CheckBox57
        '
        Me.CheckBox57.AutoSize = True
        Me.CheckBox57.Location = New System.Drawing.Point(199, 33)
        Me.CheckBox57.Name = "CheckBox57"
        Me.CheckBox57.Size = New System.Drawing.Size(101, 17)
        Me.CheckBox57.TabIndex = 44
        Me.CheckBox57.Text = "Chemical Profile"
        Me.CheckBox57.UseVisualStyleBackColor = True
        '
        'CheckBox58
        '
        Me.CheckBox58.AutoSize = True
        Me.CheckBox58.Location = New System.Drawing.Point(49, 279)
        Me.CheckBox58.Name = "CheckBox58"
        Me.CheckBox58.Size = New System.Drawing.Size(121, 17)
        Me.CheckBox58.TabIndex = 43
        Me.CheckBox58.Text = "ETOH/Drug Screen"
        Me.CheckBox58.UseVisualStyleBackColor = True
        '
        'CheckBox59
        '
        Me.CheckBox59.AutoSize = True
        Me.CheckBox59.Location = New System.Drawing.Point(49, 237)
        Me.CheckBox59.Name = "CheckBox59"
        Me.CheckBox59.Size = New System.Drawing.Size(80, 17)
        Me.CheckBox59.TabIndex = 42
        Me.CheckBox59.Text = "Electrolytes"
        Me.CheckBox59.UseVisualStyleBackColor = True
        '
        'CheckBox60
        '
        Me.CheckBox60.AutoSize = True
        Me.CheckBox60.Location = New System.Drawing.Point(49, 196)
        Me.CheckBox60.Name = "CheckBox60"
        Me.CheckBox60.Size = New System.Drawing.Size(97, 17)
        Me.CheckBox60.TabIndex = 41
        Me.CheckBox60.Text = "Bun/Creatinine"
        Me.CheckBox60.UseVisualStyleBackColor = True
        '
        'CheckBox61
        '
        Me.CheckBox61.AutoSize = True
        Me.CheckBox61.Location = New System.Drawing.Point(49, 163)
        Me.CheckBox61.Name = "CheckBox61"
        Me.CheckBox61.Size = New System.Drawing.Size(64, 17)
        Me.CheckBox61.TabIndex = 40
        Me.CheckBox61.Text = "amylase"
        Me.CheckBox61.UseVisualStyleBackColor = True
        '
        'CheckBox62
        '
        Me.CheckBox62.AutoSize = True
        Me.CheckBox62.Location = New System.Drawing.Point(50, 116)
        Me.CheckBox62.Name = "CheckBox62"
        Me.CheckBox62.Size = New System.Drawing.Size(101, 17)
        Me.CheckBox62.TabIndex = 39
        Me.CheckBox62.Text = "Pregnancy Test"
        Me.CheckBox62.UseVisualStyleBackColor = True
        '
        'CheckBox63
        '
        Me.CheckBox63.AutoSize = True
        Me.CheckBox63.Location = New System.Drawing.Point(49, 75)
        Me.CheckBox63.Name = "CheckBox63"
        Me.CheckBox63.Size = New System.Drawing.Size(125, 17)
        Me.CheckBox63.TabIndex = 38
        Me.CheckBox63.Text = "Flu/Strep/Mono spot"
        Me.CheckBox63.UseVisualStyleBackColor = True
        '
        'CheckBox64
        '
        Me.CheckBox64.AutoSize = True
        Me.CheckBox64.Location = New System.Drawing.Point(49, 33)
        Me.CheckBox64.Name = "CheckBox64"
        Me.CheckBox64.Size = New System.Drawing.Size(67, 17)
        Me.CheckBox64.TabIndex = 37
        Me.CheckBox64.Text = "CBC/UA"
        Me.CheckBox64.UseVisualStyleBackColor = True
        '
        'NumericUpDown5
        '
        Me.NumericUpDown5.Location = New System.Drawing.Point(265, 415)
        Me.NumericUpDown5.Name = "NumericUpDown5"
        Me.NumericUpDown5.Size = New System.Drawing.Size(98, 20)
        Me.NumericUpDown5.TabIndex = 58
        Me.NumericUpDown5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBox65
        '
        Me.CheckBox65.AutoSize = True
        Me.CheckBox65.Location = New System.Drawing.Point(36, 334)
        Me.CheckBox65.Name = "CheckBox65"
        Me.CheckBox65.Size = New System.Drawing.Size(179, 17)
        Me.CheckBox65.TabIndex = 57
        Me.CheckBox65.Text = "Independent Visualization of test"
        Me.CheckBox65.UseVisualStyleBackColor = True
        '
        'CheckBox66
        '
        Me.CheckBox66.AutoSize = True
        Me.CheckBox66.Location = New System.Drawing.Point(36, 378)
        Me.CheckBox66.Name = "CheckBox66"
        Me.CheckBox66.Size = New System.Drawing.Size(199, 17)
        Me.CheckBox66.TabIndex = 56
        Me.CheckBox66.Text = "Discussion with performing Physician"
        Me.CheckBox66.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(62, 425)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 13)
        Me.Label9.TabIndex = 55
        Me.Label9.Text = "Other Labs"
        '
        'CheckBox67
        '
        Me.CheckBox67.AutoSize = True
        Me.CheckBox67.Location = New System.Drawing.Point(199, 279)
        Me.CheckBox67.Name = "CheckBox67"
        Me.CheckBox67.Size = New System.Drawing.Size(135, 17)
        Me.CheckBox67.TabIndex = 50
        Me.CheckBox67.Text = "Deep/Incisional Biopsy"
        Me.CheckBox67.UseVisualStyleBackColor = True
        '
        'CheckBox68
        '
        Me.CheckBox68.AutoSize = True
        Me.CheckBox68.Location = New System.Drawing.Point(199, 237)
        Me.CheckBox68.Name = "CheckBox68"
        Me.CheckBox68.Size = New System.Drawing.Size(108, 17)
        Me.CheckBox68.TabIndex = 49
        Me.CheckBox68.Text = "Superficial biopsy"
        Me.CheckBox68.UseVisualStyleBackColor = True
        '
        'CheckBox69
        '
        Me.CheckBox69.AutoSize = True
        Me.CheckBox69.Location = New System.Drawing.Point(199, 196)
        Me.CheckBox69.Name = "CheckBox69"
        Me.CheckBox69.Size = New System.Drawing.Size(136, 17)
        Me.CheckBox69.TabIndex = 48
        Me.CheckBox69.Text = "Types and cross match"
        Me.CheckBox69.UseVisualStyleBackColor = True
        '
        'CheckBox70
        '
        Me.CheckBox70.AutoSize = True
        Me.CheckBox70.Location = New System.Drawing.Point(199, 163)
        Me.CheckBox70.Name = "CheckBox70"
        Me.CheckBox70.Size = New System.Drawing.Size(66, 17)
        Me.CheckBox70.TabIndex = 47
        Me.CheckBox70.Text = "PT/PTT"
        Me.CheckBox70.UseVisualStyleBackColor = True
        '
        'CheckBox71
        '
        Me.CheckBox71.AutoSize = True
        Me.CheckBox71.Location = New System.Drawing.Point(199, 116)
        Me.CheckBox71.Name = "CheckBox71"
        Me.CheckBox71.Size = New System.Drawing.Size(55, 17)
        Me.CheckBox71.TabIndex = 46
        Me.CheckBox71.Text = "ABGS"
        Me.CheckBox71.UseVisualStyleBackColor = True
        '
        'CheckBox72
        '
        Me.CheckBox72.AutoSize = True
        Me.CheckBox72.Location = New System.Drawing.Point(199, 75)
        Me.CheckBox72.Name = "CheckBox72"
        Me.CheckBox72.Size = New System.Drawing.Size(106, 17)
        Me.CheckBox72.TabIndex = 45
        Me.CheckBox72.Text = "Cardiac enzymes"
        Me.CheckBox72.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(48, 338)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(0, 13)
        Me.Label10.TabIndex = 53
        '
        'CheckBox73
        '
        Me.CheckBox73.AutoSize = True
        Me.CheckBox73.Location = New System.Drawing.Point(199, 33)
        Me.CheckBox73.Name = "CheckBox73"
        Me.CheckBox73.Size = New System.Drawing.Size(101, 17)
        Me.CheckBox73.TabIndex = 44
        Me.CheckBox73.Text = "Chemical Profile"
        Me.CheckBox73.UseVisualStyleBackColor = True
        '
        'CheckBox74
        '
        Me.CheckBox74.AutoSize = True
        Me.CheckBox74.Location = New System.Drawing.Point(49, 279)
        Me.CheckBox74.Name = "CheckBox74"
        Me.CheckBox74.Size = New System.Drawing.Size(121, 17)
        Me.CheckBox74.TabIndex = 43
        Me.CheckBox74.Text = "ETOH/Drug Screen"
        Me.CheckBox74.UseVisualStyleBackColor = True
        '
        'CheckBox75
        '
        Me.CheckBox75.AutoSize = True
        Me.CheckBox75.Location = New System.Drawing.Point(49, 237)
        Me.CheckBox75.Name = "CheckBox75"
        Me.CheckBox75.Size = New System.Drawing.Size(80, 17)
        Me.CheckBox75.TabIndex = 42
        Me.CheckBox75.Text = "Electrolytes"
        Me.CheckBox75.UseVisualStyleBackColor = True
        '
        'CheckBox76
        '
        Me.CheckBox76.AutoSize = True
        Me.CheckBox76.Location = New System.Drawing.Point(49, 196)
        Me.CheckBox76.Name = "CheckBox76"
        Me.CheckBox76.Size = New System.Drawing.Size(97, 17)
        Me.CheckBox76.TabIndex = 41
        Me.CheckBox76.Text = "Bun/Creatinine"
        Me.CheckBox76.UseVisualStyleBackColor = True
        '
        'CheckBox77
        '
        Me.CheckBox77.AutoSize = True
        Me.CheckBox77.Location = New System.Drawing.Point(49, 163)
        Me.CheckBox77.Name = "CheckBox77"
        Me.CheckBox77.Size = New System.Drawing.Size(64, 17)
        Me.CheckBox77.TabIndex = 40
        Me.CheckBox77.Text = "amylase"
        Me.CheckBox77.UseVisualStyleBackColor = True
        '
        'CheckBox78
        '
        Me.CheckBox78.AutoSize = True
        Me.CheckBox78.Location = New System.Drawing.Point(50, 116)
        Me.CheckBox78.Name = "CheckBox78"
        Me.CheckBox78.Size = New System.Drawing.Size(101, 17)
        Me.CheckBox78.TabIndex = 39
        Me.CheckBox78.Text = "Pregnancy Test"
        Me.CheckBox78.UseVisualStyleBackColor = True
        '
        'CheckBox79
        '
        Me.CheckBox79.AutoSize = True
        Me.CheckBox79.Location = New System.Drawing.Point(49, 75)
        Me.CheckBox79.Name = "CheckBox79"
        Me.CheckBox79.Size = New System.Drawing.Size(125, 17)
        Me.CheckBox79.TabIndex = 38
        Me.CheckBox79.Text = "Flu/Strep/Mono spot"
        Me.CheckBox79.UseVisualStyleBackColor = True
        '
        'CheckBox80
        '
        Me.CheckBox80.AutoSize = True
        Me.CheckBox80.Location = New System.Drawing.Point(49, 33)
        Me.CheckBox80.Name = "CheckBox80"
        Me.CheckBox80.Size = New System.Drawing.Size(67, 17)
        Me.CheckBox80.TabIndex = 37
        Me.CheckBox80.Text = "CBC/UA"
        Me.CheckBox80.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tlsDictionary)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(580, 54)
        Me.Panel1.TabIndex = 1
        '
        'tlsDictionary
        '
        Me.tlsDictionary.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tlsDictionary.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDictionary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDictionary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDictionary.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDictionary.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tb_Refresh, Me.tlb_Ok, Me.tb_Cancel})
        Me.tlsDictionary.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDictionary.Location = New System.Drawing.Point(0, 0)
        Me.tlsDictionary.Name = "tlsDictionary"
        Me.tlsDictionary.Size = New System.Drawing.Size(580, 53)
        Me.tlsDictionary.TabIndex = 1
        Me.tlsDictionary.Text = "toolStrip1"
        '
        'tb_Refresh
        '
        Me.tb_Refresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tb_Refresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tb_Refresh.Image = CType(resources.GetObject("tb_Refresh.Image"), System.Drawing.Image)
        Me.tb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tb_Refresh.Name = "tb_Refresh"
        Me.tb_Refresh.Size = New System.Drawing.Size(58, 50)
        Me.tb_Refresh.Tag = "Refesh"
        Me.tb_Refresh.Text = "&Refresh"
        Me.tb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlb_Ok
        '
        Me.tlb_Ok.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tlb_Ok.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Ok.Image = CType(resources.GetObject("tlb_Ok.Image"), System.Drawing.Image)
        Me.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Ok.Name = "tlb_Ok"
        Me.tlb_Ok.Size = New System.Drawing.Size(66, 50)
        Me.tlb_Ok.Tag = "OK"
        Me.tlb_Ok.Text = "&Save&&Cls"
        Me.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Ok.ToolTipText = "Save and Close"
        '
        'tb_Cancel
        '
        Me.tb_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tb_Cancel.Image = CType(resources.GetObject("tb_Cancel.Image"), System.Drawing.Image)
        Me.tb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tb_Cancel.Name = "tb_Cancel"
        Me.tb_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.tb_Cancel.Tag = "Cancel"
        Me.tb_Cancel.Text = "&Close"
        Me.tb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.tbEMAssociation)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(580, 462)
        Me.Panel2.TabIndex = 2
        '
        'frmEMTagAssociation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(580, 516)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEMTagAssociation"
        Me.Text = "E&M Association"
        Me.tbpXRay.ResumeLayout(False)
        Me.tbpXRay.PerformLayout()
        Me.pnlOrdersEMFields.ResumeLayout(False)
        Me.pnlOrdersEMFields.PerformLayout()
        CType(Me.nudXOtherXray, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbpLabs.ResumeLayout(False)
        Me.tbpLabs.PerformLayout()
        Me.pnlLabsEMFields.ResumeLayout(False)
        Me.pnlLabsEMFields.PerformLayout()
        CType(Me.nudLOtherLabs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbpManagementOption.ResumeLayout(False)
        Me.tbpManagementOption.PerformLayout()
        CType(Me.nudMTimeSpent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbEMAssociation.ResumeLayout(False)
        Me.tbpOtherDiagnosisTest.ResumeLayout(False)
        Me.tbpOtherDiagnosisTest.PerformLayout()
        Me.pnlOtherDiagnosticEMFields.ResumeLayout(False)
        Me.pnlOtherDiagnosticEMFields.PerformLayout()
        CType(Me.nudODignosisstudies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tlsDictionary.ResumeLayout(False)
        Me.tlsDictionary.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbpXRay As System.Windows.Forms.TabPage
    Friend WithEvents tbpLabs As System.Windows.Forms.TabPage
    Friend WithEvents tbpManagementOption As System.Windows.Forms.TabPage
    Friend WithEvents nudMTimeSpent As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMTimeSpent As System.Windows.Forms.Label
    Friend WithEvents chkMDecisionofcase As System.Windows.Forms.CheckBox
    Friend WithEvents chkMreviewandsummary As System.Windows.Forms.CheckBox
    Friend WithEvents chkMDecisiontoobtain As System.Windows.Forms.CheckBox
    Friend WithEvents chkMDecisionnottoresuscitate As System.Windows.Forms.CheckBox
    Friend WithEvents chkMMajoremergencySurgery As System.Windows.Forms.CheckBox
    Friend WithEvents chkMMajorSurgerywrisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkMMajorsurgeryworisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkMMinorSurgeryWrisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkMminorsurgeryWOrisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkMClosefx As System.Windows.Forms.CheckBox
    Friend WithEvents chkMPhysicalOccupationaltherapy As System.Windows.Forms.CheckBox
    Friend WithEvents chkMNuclearMedicine As System.Windows.Forms.CheckBox
    Friend WithEvents chkMRespiratoryTreatment As System.Windows.Forms.CheckBox
    Friend WithEvents chkMTelemetry As System.Windows.Forms.CheckBox
    Friend WithEvents chkMHighRiskmeds As System.Windows.Forms.CheckBox
    Friend WithEvents chkMIVmedswadditives As System.Windows.Forms.CheckBox
    Friend WithEvents chkMivmeds As System.Windows.Forms.CheckBox
    Friend WithEvents chkMPerscripmeds As System.Windows.Forms.CheckBox
    Friend WithEvents chkMOTCmeds As System.Windows.Forms.CheckBox
    Friend WithEvents tbEMAssociation As System.Windows.Forms.TabControl
    Friend WithEvents tbpOtherDiagnosisTest As System.Windows.Forms.TabPage
    Friend WithEvents chkLDeepIncisionalBiopsy As System.Windows.Forms.CheckBox
    Friend WithEvents chkLSuperficialbiopsy As System.Windows.Forms.CheckBox
    Friend WithEvents chkLTypesandCrossmatch As System.Windows.Forms.CheckBox
    Friend WithEvents chkLPT As System.Windows.Forms.CheckBox
    Friend WithEvents chkLABGS As System.Windows.Forms.CheckBox
    Friend WithEvents chkLCardiacenzymes As System.Windows.Forms.CheckBox
    Friend WithEvents chkLChemicalProfile As System.Windows.Forms.CheckBox
    Friend WithEvents chkLETOH As System.Windows.Forms.CheckBox
    Friend WithEvents chkLElectrolytes As System.Windows.Forms.CheckBox
    Friend WithEvents chkLBun As System.Windows.Forms.CheckBox
    Friend WithEvents chkLAmylase As System.Windows.Forms.CheckBox
    Friend WithEvents chkLPregnancyTest As System.Windows.Forms.CheckBox
    Friend WithEvents chkLFlu As System.Windows.Forms.CheckBox
    Friend WithEvents chkLLCBC As System.Windows.Forms.CheckBox
    Friend WithEvents lblIndependentVisualizationoftest As System.Windows.Forms.Label
    Friend WithEvents nudLOtherLabs As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkLIndependentVisualizationoftest As System.Windows.Forms.CheckBox
    Friend WithEvents chkLDiscussionwithperformingPhysician As System.Windows.Forms.CheckBox
    Friend WithEvents lblOtherLabs As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox10 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox11 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox12 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox13 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox14 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox15 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox16 As System.Windows.Forms.CheckBox
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents CheckBox17 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox18 As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CheckBox19 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox20 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox21 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox22 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox23 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox24 As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBox25 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox26 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox27 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox28 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox29 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox30 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox31 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox32 As System.Windows.Forms.CheckBox
    Friend WithEvents NumericUpDown3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents CheckBox33 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox34 As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CheckBox35 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox36 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox37 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox38 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox39 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox40 As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBox41 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox42 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox43 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox44 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox45 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox46 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox47 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox48 As System.Windows.Forms.CheckBox
    Friend WithEvents NumericUpDown4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents CheckBox49 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox50 As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CheckBox51 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox52 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox53 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox54 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox55 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox56 As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CheckBox57 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox58 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox59 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox60 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox61 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox62 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox63 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox64 As System.Windows.Forms.CheckBox
    Friend WithEvents NumericUpDown5 As System.Windows.Forms.NumericUpDown
    Friend WithEvents CheckBox65 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox66 As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CheckBox67 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox68 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox69 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox70 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox71 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox72 As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CheckBox73 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox74 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox75 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox76 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox77 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox78 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox79 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox80 As System.Windows.Forms.CheckBox
    Friend WithEvents chkXVascularStudieswrisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkXVascularStudies As System.Windows.Forms.CheckBox
    Friend WithEvents chkXMRI As System.Windows.Forms.CheckBox
    Friend WithEvents chkXcatScan As System.Windows.Forms.CheckBox
    Friend WithEvents chkXIVP As System.Windows.Forms.CheckBox
    Friend WithEvents chkXGIGallablader As System.Windows.Forms.CheckBox
    Friend WithEvents chkXTLSpire As System.Windows.Forms.CheckBox
    Friend WithEvents chkXDiscographt As System.Windows.Forms.CheckBox
    Friend WithEvents chkXDiagosticUltrasound As System.Windows.Forms.CheckBox
    Friend WithEvents chkXCspine As System.Windows.Forms.CheckBox
    Friend WithEvents chkXHipPelvis As System.Windows.Forms.CheckBox
    Friend WithEvents chkXAbdomen As System.Windows.Forms.CheckBox
    Friend WithEvents chkXExtrimities As System.Windows.Forms.CheckBox
    Friend WithEvents chkXChest As System.Windows.Forms.CheckBox
    Friend WithEvents chkXIndepedent As System.Windows.Forms.CheckBox
    Friend WithEvents chkXperformingPhy As System.Windows.Forms.CheckBox
    Friend WithEvents nudXOtherXray As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblOtherXRay As System.Windows.Forms.Label
    Friend WithEvents chkOEndoScopewRisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkOEndoscopeworisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkOCuldcentesis As System.Windows.Forms.CheckBox
    Friend WithEvents chkOThoracentesis As System.Windows.Forms.CheckBox
    Friend WithEvents chkOLumbarPunctor As System.Windows.Forms.CheckBox
    Friend WithEvents chkONuclearScan As System.Windows.Forms.CheckBox
    Friend WithEvents chkOPulmonary As System.Windows.Forms.CheckBox
    Friend WithEvents chkODopplerFlowStudies As System.Windows.Forms.CheckBox
    Friend WithEvents chkOVectorCardiogram As System.Windows.Forms.CheckBox
    Friend WithEvents chkOEEG As System.Windows.Forms.CheckBox
    Friend WithEvents chkOTreadmill As System.Windows.Forms.CheckBox
    Friend WithEvents chkOHolterMonitor As System.Windows.Forms.CheckBox
    Friend WithEvents chkOEKG As System.Windows.Forms.CheckBox
    Friend WithEvents nudODignosisstudies As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblOtheradditionaldiagnosisstudies As System.Windows.Forms.Label
    Friend WithEvents chkOIndependentVisualization As System.Windows.Forms.CheckBox
    Friend WithEvents chkODiscuswithPerfoming As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents tlsDictionary As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlb_Ok As System.Windows.Forms.ToolStripButton
    Private WithEvents tb_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents tb_Refresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlLabsEMFields As System.Windows.Forms.Panel
    Friend WithEvents pnlOrdersEMFields As System.Windows.Forms.Panel
    Friend WithEvents pnlOtherDiagnosticEMFields As System.Windows.Forms.Panel
End Class
