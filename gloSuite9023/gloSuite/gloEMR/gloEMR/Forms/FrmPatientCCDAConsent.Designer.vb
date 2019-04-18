<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPatientCCDAConsent
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPatientCCDAConsent))
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblMedication = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblSaveCls = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlClinicalSummary = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.ChkAll = New System.Windows.Forms.CheckBox()
        Me.chkTransCareEncounter = New System.Windows.Forms.CheckBox()
        Me.chkTransCareCognitiveStat = New System.Windows.Forms.CheckBox()
        Me.chkHealthStatus = New System.Windows.Forms.CheckBox()
        Me.chkInterventions = New System.Windows.Forms.CheckBox()
        Me.chkAmbImmunization = New System.Windows.Forms.CheckBox()
        Me.chkImplant = New System.Windows.Forms.CheckBox()
        Me.chkCOProblems = New System.Windows.Forms.CheckBox()
        Me.chkCOAllergy = New System.Windows.Forms.CheckBox()
        Me.chkCOCareTeamMem = New System.Windows.Forms.CheckBox()
        Me.chkCOProcedures = New System.Windows.Forms.CheckBox()
        Me.chkCOVitalSigns = New System.Windows.Forms.CheckBox()
        Me.chkCOlabResult = New System.Windows.Forms.CheckBox()
        Me.chkCOLabTest = New System.Windows.Forms.CheckBox()
        Me.chkCOMedication = New System.Windows.Forms.CheckBox()
        Me.chkCSClinicalInstru = New System.Windows.Forms.CheckBox()
        Me.chkCOSocialHistory = New System.Windows.Forms.CheckBox()
        Me.chkCOFamilyHistory = New System.Windows.Forms.CheckBox()
        Me.chkCSFutureAppt = New System.Windows.Forms.CheckBox()
        Me.ChkCOAssessments = New System.Windows.Forms.CheckBox()
        Me.ChkHealthConcerns = New System.Windows.Forms.CheckBox()
        Me.ChkGoals = New System.Windows.Forms.CheckBox()
        Me.chkCSVisitMedications = New System.Windows.Forms.CheckBox()
        Me.chkCSVisitImmunization = New System.Windows.Forms.CheckBox()
        Me.ChkTreatmentPlan = New System.Windows.Forms.CheckBox()
        Me.chkCSVisitReason = New System.Windows.Forms.CheckBox()
        Me.cmbPurposeofUse = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCDAPrivacyText = New System.Windows.Forms.TextBox()
        Me.Label1017 = New System.Windows.Forms.Label()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.tlTooltip = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlToolStrip.SuspendLayout()
        Me.tblMedication.SuspendLayout()
        Me.pnlClinicalSummary.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.PnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(869, 4)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 507)
        Me.Label14.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(3, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 507)
        Me.Label11.TabIndex = 17
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Location = New System.Drawing.Point(3, 511)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(867, 1)
        Me.Label12.TabIndex = 16
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Location = New System.Drawing.Point(3, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(867, 1)
        Me.Label13.TabIndex = 15
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.AutoSize = True
        Me.pnlToolStrip.Controls.Add(Me.tblMedication)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(873, 53)
        Me.pnlToolStrip.TabIndex = 78
        '
        'tblMedication
        '
        Me.tblMedication.BackColor = System.Drawing.Color.Transparent
        Me.tblMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblMedication.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblMedication.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblMedication.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblSaveCls, Me.tblClose})
        Me.tblMedication.Location = New System.Drawing.Point(0, 0)
        Me.tblMedication.Name = "tblMedication"
        Me.tblMedication.Size = New System.Drawing.Size(873, 53)
        Me.tblMedication.TabIndex = 0
        Me.tblMedication.Text = "ToolStrip1"
        '
        'tblSaveCls
        '
        Me.tblSaveCls.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSaveCls.Image = CType(resources.GetObject("tblSaveCls.Image"), System.Drawing.Image)
        Me.tblSaveCls.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSaveCls.Name = "tblSaveCls"
        Me.tblSaveCls.Size = New System.Drawing.Size(66, 50)
        Me.tblSaveCls.Text = "&Save&&Cls"
        Me.tblSaveCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSaveCls.ToolTipText = "Save and Close"
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'pnlClinicalSummary
        '
        Me.pnlClinicalSummary.Controls.Add(Me.Panel7)
        Me.pnlClinicalSummary.Controls.Add(Me.ChkAll)
        Me.pnlClinicalSummary.Controls.Add(Me.chkTransCareEncounter)
        Me.pnlClinicalSummary.Controls.Add(Me.chkTransCareCognitiveStat)
        Me.pnlClinicalSummary.Controls.Add(Me.chkHealthStatus)
        Me.pnlClinicalSummary.Controls.Add(Me.chkInterventions)
        Me.pnlClinicalSummary.Controls.Add(Me.chkAmbImmunization)
        Me.pnlClinicalSummary.Controls.Add(Me.chkImplant)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCOProblems)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCOAllergy)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCOCareTeamMem)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCOProcedures)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCOVitalSigns)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCOlabResult)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCOLabTest)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCOMedication)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSClinicalInstru)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCOSocialHistory)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCOFamilyHistory)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSFutureAppt)
        Me.pnlClinicalSummary.Controls.Add(Me.ChkCOAssessments)
        Me.pnlClinicalSummary.Controls.Add(Me.ChkHealthConcerns)
        Me.pnlClinicalSummary.Controls.Add(Me.ChkGoals)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSVisitMedications)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSVisitImmunization)
        Me.pnlClinicalSummary.Controls.Add(Me.ChkTreatmentPlan)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSVisitReason)
        Me.pnlClinicalSummary.Controls.Add(Me.cmbPurposeofUse)
        Me.pnlClinicalSummary.Controls.Add(Me.Label1)
        Me.pnlClinicalSummary.Controls.Add(Me.txtCDAPrivacyText)
        Me.pnlClinicalSummary.Controls.Add(Me.Label1017)
        Me.pnlClinicalSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlClinicalSummary.Location = New System.Drawing.Point(4, 4)
        Me.pnlClinicalSummary.Name = "pnlClinicalSummary"
        Me.pnlClinicalSummary.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.pnlClinicalSummary.Size = New System.Drawing.Size(865, 507)
        Me.pnlClinicalSummary.TabIndex = 75
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel7.Controls.Add(Me.Label112)
        Me.Panel7.Controls.Add(Me.Label113)
        Me.Panel7.Controls.Add(Me.Label115)
        Me.Panel7.Controls.Add(Me.Label114)
        Me.Panel7.Controls.Add(Me.Label106)
        Me.Panel7.Location = New System.Drawing.Point(6, 10)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(850, 38)
        Me.Panel7.TabIndex = 232
        '
        'Label112
        '
        Me.Label112.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label112.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label112.Enabled = False
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label112.Location = New System.Drawing.Point(1, 37)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(848, 1)
        Me.Label112.TabIndex = 27
        Me.Label112.Text = "From"
        Me.Label112.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label113.Enabled = False
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label113.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Location = New System.Drawing.Point(1, 0)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(848, 1)
        Me.Label113.TabIndex = 28
        Me.Label113.Text = "From"
        Me.Label113.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label115
        '
        Me.Label115.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label115.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label115.Enabled = False
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label115.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label115.Location = New System.Drawing.Point(0, 0)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(1, 38)
        Me.Label115.TabIndex = 30
        Me.Label115.Text = "From"
        Me.Label115.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label114
        '
        Me.Label114.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label114.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label114.Enabled = False
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label114.Location = New System.Drawing.Point(849, 0)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(1, 38)
        Me.Label114.TabIndex = 29
        Me.Label114.Text = "From"
        Me.Label114.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label106
        '
        Me.Label106.AutoSize = True
        Me.Label106.BackColor = System.Drawing.Color.Transparent
        Me.Label106.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label106.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label106.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label106.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label106.Location = New System.Drawing.Point(7, 12)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(559, 14)
        Me.Label106.TabIndex = 15
        Me.Label106.Text = "Selection of checkbox against any section will be marked as Restricted in CCDA do" & _
    "cument"
        Me.Label106.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ChkAll
        '
        Me.ChkAll.AutoSize = True
        Me.ChkAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAll.Location = New System.Drawing.Point(6, 55)
        Me.ChkAll.Name = "ChkAll"
        Me.ChkAll.Padding = New System.Windows.Forms.Padding(10, 5, 5, 5)
        Me.ChkAll.Size = New System.Drawing.Size(97, 28)
        Me.ChkAll.TabIndex = 180
        Me.ChkAll.Text = "Select All"
        Me.ChkAll.UseVisualStyleBackColor = True
        '
        'chkTransCareEncounter
        '
        Me.chkTransCareEncounter.AutoSize = True
        Me.chkTransCareEncounter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransCareEncounter.Location = New System.Drawing.Point(46, 227)
        Me.chkTransCareEncounter.Name = "chkTransCareEncounter"
        Me.chkTransCareEncounter.Size = New System.Drawing.Size(135, 18)
        Me.chkTransCareEncounter.TabIndex = 178
        Me.chkTransCareEncounter.Text = "Encounter diagnosis"
        Me.chkTransCareEncounter.UseVisualStyleBackColor = True
        '
        'chkTransCareCognitiveStat
        '
        Me.chkTransCareCognitiveStat.AutoSize = True
        Me.chkTransCareCognitiveStat.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransCareCognitiveStat.Location = New System.Drawing.Point(326, 326)
        Me.chkTransCareCognitiveStat.Name = "chkTransCareCognitiveStat"
        Me.chkTransCareCognitiveStat.Size = New System.Drawing.Size(101, 18)
        Me.chkTransCareCognitiveStat.TabIndex = 179
        Me.chkTransCareCognitiveStat.Text = "Mental Status"
        Me.chkTransCareCognitiveStat.UseVisualStyleBackColor = True
        '
        'chkHealthStatus
        '
        Me.chkHealthStatus.AutoSize = True
        Me.chkHealthStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.chkHealthStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHealthStatus.Location = New System.Drawing.Point(326, 293)
        Me.chkHealthStatus.Name = "chkHealthStatus"
        Me.chkHealthStatus.Size = New System.Drawing.Size(248, 18)
        Me.chkHealthStatus.TabIndex = 177
        Me.chkHealthStatus.Tag = ""
        Me.chkHealthStatus.Text = "Health Status Evaluations and Outcomes"
        Me.chkHealthStatus.UseVisualStyleBackColor = False
        '
        'chkInterventions
        '
        Me.chkInterventions.AutoSize = True
        Me.chkInterventions.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInterventions.Location = New System.Drawing.Point(46, 326)
        Me.chkInterventions.Name = "chkInterventions"
        Me.chkInterventions.Size = New System.Drawing.Size(99, 18)
        Me.chkInterventions.TabIndex = 176
        Me.chkInterventions.Tag = ""
        Me.chkInterventions.Text = "Interventions"
        Me.chkInterventions.UseVisualStyleBackColor = True
        '
        'chkAmbImmunization
        '
        Me.chkAmbImmunization.AutoSize = True
        Me.chkAmbImmunization.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.chkAmbImmunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAmbImmunization.Location = New System.Drawing.Point(46, 95)
        Me.chkAmbImmunization.Name = "chkAmbImmunization"
        Me.chkAmbImmunization.Size = New System.Drawing.Size(103, 18)
        Me.chkAmbImmunization.TabIndex = 175
        Me.chkAmbImmunization.Tag = "CCDAASImmunizations"
        Me.chkAmbImmunization.Text = "Immunizations"
        Me.chkAmbImmunization.UseVisualStyleBackColor = False
        '
        'chkImplant
        '
        Me.chkImplant.AutoSize = True
        Me.chkImplant.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.chkImplant.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkImplant.Location = New System.Drawing.Point(646, 95)
        Me.chkImplant.Name = "chkImplant"
        Me.chkImplant.Size = New System.Drawing.Size(72, 18)
        Me.chkImplant.TabIndex = 173
        Me.chkImplant.Tag = "CCDAImplants"
        Me.chkImplant.Text = "Implants"
        Me.chkImplant.UseVisualStyleBackColor = False
        '
        'chkCOProblems
        '
        Me.chkCOProblems.AutoSize = True
        Me.chkCOProblems.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOProblems.Location = New System.Drawing.Point(326, 95)
        Me.chkCOProblems.Name = "chkCOProblems"
        Me.chkCOProblems.Size = New System.Drawing.Size(75, 18)
        Me.chkCOProblems.TabIndex = 158
        Me.chkCOProblems.Tag = "CCDAProblems"
        Me.chkCOProblems.Text = "Problems"
        Me.chkCOProblems.UseVisualStyleBackColor = True
        '
        'chkCOAllergy
        '
        Me.chkCOAllergy.AutoSize = True
        Me.chkCOAllergy.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.chkCOAllergy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOAllergy.Location = New System.Drawing.Point(46, 161)
        Me.chkCOAllergy.Name = "chkCOAllergy"
        Me.chkCOAllergy.Size = New System.Drawing.Size(130, 18)
        Me.chkCOAllergy.TabIndex = 159
        Me.chkCOAllergy.Tag = "CCDAMedicationAllergies"
        Me.chkCOAllergy.Text = "Medication allergies"
        Me.chkCOAllergy.UseVisualStyleBackColor = False
        '
        'chkCOCareTeamMem
        '
        Me.chkCOCareTeamMem.AutoSize = True
        Me.chkCOCareTeamMem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOCareTeamMem.Location = New System.Drawing.Point(185, 473)
        Me.chkCOCareTeamMem.Name = "chkCOCareTeamMem"
        Me.chkCOCareTeamMem.Size = New System.Drawing.Size(148, 18)
        Me.chkCOCareTeamMem.TabIndex = 168
        Me.chkCOCareTeamMem.Tag = "CCDACareTeamMember"
        Me.chkCOCareTeamMem.Text = "Care Team Member(s)"
        Me.chkCOCareTeamMem.UseVisualStyleBackColor = True
        '
        'chkCOProcedures
        '
        Me.chkCOProcedures.AutoSize = True
        Me.chkCOProcedures.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOProcedures.Location = New System.Drawing.Point(646, 161)
        Me.chkCOProcedures.Name = "chkCOProcedures"
        Me.chkCOProcedures.Size = New System.Drawing.Size(87, 18)
        Me.chkCOProcedures.TabIndex = 161
        Me.chkCOProcedures.Tag = "CCDAProcedures"
        Me.chkCOProcedures.Text = "Procedures"
        Me.chkCOProcedures.UseVisualStyleBackColor = True
        '
        'chkCOVitalSigns
        '
        Me.chkCOVitalSigns.AutoSize = True
        Me.chkCOVitalSigns.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOVitalSigns.Location = New System.Drawing.Point(326, 161)
        Me.chkCOVitalSigns.Name = "chkCOVitalSigns"
        Me.chkCOVitalSigns.Size = New System.Drawing.Size(81, 18)
        Me.chkCOVitalSigns.TabIndex = 167
        Me.chkCOVitalSigns.Tag = "CCDAVitalSigns"
        Me.chkCOVitalSigns.Text = "Vital Signs"
        Me.chkCOVitalSigns.UseVisualStyleBackColor = True
        '
        'chkCOlabResult
        '
        Me.chkCOlabResult.AutoSize = True
        Me.chkCOlabResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOlabResult.Location = New System.Drawing.Point(46, 260)
        Me.chkCOlabResult.Name = "chkCOlabResult"
        Me.chkCOlabResult.Size = New System.Drawing.Size(181, 18)
        Me.chkCOlabResult.TabIndex = 160
        Me.chkCOlabResult.Tag = "CCDALaboratoryValue"
        Me.chkCOlabResult.Text = "Laboratory value(s)/result(s)"
        Me.chkCOlabResult.UseVisualStyleBackColor = True
        '
        'chkCOLabTest
        '
        Me.chkCOLabTest.AutoSize = True
        Me.chkCOLabTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOLabTest.Location = New System.Drawing.Point(46, 128)
        Me.chkCOLabTest.Name = "chkCOLabTest"
        Me.chkCOLabTest.Size = New System.Drawing.Size(128, 18)
        Me.chkCOLabTest.TabIndex = 166
        Me.chkCOLabTest.Tag = "CCDALaboratoryTest"
        Me.chkCOLabTest.Text = "Laboratory Test(s)"
        Me.chkCOLabTest.UseVisualStyleBackColor = True
        '
        'chkCOMedication
        '
        Me.chkCOMedication.AutoSize = True
        Me.chkCOMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOMedication.Location = New System.Drawing.Point(326, 128)
        Me.chkCOMedication.Name = "chkCOMedication"
        Me.chkCOMedication.Size = New System.Drawing.Size(89, 18)
        Me.chkCOMedication.TabIndex = 164
        Me.chkCOMedication.Tag = "CCDAMedications"
        Me.chkCOMedication.Text = "Medications"
        Me.chkCOMedication.UseVisualStyleBackColor = True
        '
        'chkCSClinicalInstru
        '
        Me.chkCSClinicalInstru.AutoSize = True
        Me.chkCSClinicalInstru.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSClinicalInstru.Location = New System.Drawing.Point(646, 227)
        Me.chkCSClinicalInstru.Name = "chkCSClinicalInstru"
        Me.chkCSClinicalInstru.Size = New System.Drawing.Size(128, 18)
        Me.chkCSClinicalInstru.TabIndex = 170
        Me.chkCSClinicalInstru.Tag = "CCDAClinicalInstructions"
        Me.chkCSClinicalInstru.Text = "Clinical Instructions"
        Me.chkCSClinicalInstru.UseVisualStyleBackColor = True
        '
        'chkCOSocialHistory
        '
        Me.chkCOSocialHistory.AutoSize = True
        Me.chkCOSocialHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOSocialHistory.Location = New System.Drawing.Point(46, 194)
        Me.chkCOSocialHistory.Name = "chkCOSocialHistory"
        Me.chkCOSocialHistory.Size = New System.Drawing.Size(97, 18)
        Me.chkCOSocialHistory.TabIndex = 171
        Me.chkCOSocialHistory.Tag = "CCDASocialHistory"
        Me.chkCOSocialHistory.Text = "Social History"
        Me.chkCOSocialHistory.UseVisualStyleBackColor = True
        '
        'chkCOFamilyHistory
        '
        Me.chkCOFamilyHistory.AutoSize = True
        Me.chkCOFamilyHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOFamilyHistory.Location = New System.Drawing.Point(646, 194)
        Me.chkCOFamilyHistory.Name = "chkCOFamilyHistory"
        Me.chkCOFamilyHistory.Size = New System.Drawing.Size(99, 18)
        Me.chkCOFamilyHistory.TabIndex = 163
        Me.chkCOFamilyHistory.Tag = "CCDAFamilyHistory"
        Me.chkCOFamilyHistory.Text = "Family History"
        Me.chkCOFamilyHistory.UseVisualStyleBackColor = True
        '
        'chkCSFutureAppt
        '
        Me.chkCSFutureAppt.AutoSize = True
        Me.chkCSFutureAppt.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.chkCSFutureAppt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSFutureAppt.Location = New System.Drawing.Point(646, 260)
        Me.chkCSFutureAppt.Name = "chkCSFutureAppt"
        Me.chkCSFutureAppt.Size = New System.Drawing.Size(143, 18)
        Me.chkCSFutureAppt.TabIndex = 4
        Me.chkCSFutureAppt.Tag = "CCDACSFutureAppointments"
        Me.chkCSFutureAppt.Text = "Future Appointments"
        Me.chkCSFutureAppt.UseVisualStyleBackColor = False
        '
        'ChkCOAssessments
        '
        Me.ChkCOAssessments.AutoSize = True
        Me.ChkCOAssessments.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ChkCOAssessments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCOAssessments.Location = New System.Drawing.Point(326, 194)
        Me.ChkCOAssessments.Name = "ChkCOAssessments"
        Me.ChkCOAssessments.Size = New System.Drawing.Size(95, 18)
        Me.ChkCOAssessments.TabIndex = 6
        Me.ChkCOAssessments.Tag = "CCDAAssessments"
        Me.ChkCOAssessments.Text = "Assessments"
        Me.ChkCOAssessments.UseVisualStyleBackColor = False
        '
        'ChkHealthConcerns
        '
        Me.ChkHealthConcerns.AutoSize = True
        Me.ChkHealthConcerns.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkHealthConcerns.Location = New System.Drawing.Point(646, 326)
        Me.ChkHealthConcerns.Name = "ChkHealthConcerns"
        Me.ChkHealthConcerns.Size = New System.Drawing.Size(115, 18)
        Me.ChkHealthConcerns.TabIndex = 9
        Me.ChkHealthConcerns.Tag = "CCDAHealthConcerns"
        Me.ChkHealthConcerns.Text = "Health Concerns"
        Me.ChkHealthConcerns.UseVisualStyleBackColor = True
        '
        'ChkGoals
        '
        Me.ChkGoals.AutoSize = True
        Me.ChkGoals.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ChkGoals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkGoals.Location = New System.Drawing.Point(646, 128)
        Me.ChkGoals.Name = "ChkGoals"
        Me.ChkGoals.Size = New System.Drawing.Size(58, 18)
        Me.ChkGoals.TabIndex = 10
        Me.ChkGoals.Tag = "CCDAGoals "
        Me.ChkGoals.Text = "Goals "
        Me.ChkGoals.UseVisualStyleBackColor = True
        '
        'chkCSVisitMedications
        '
        Me.chkCSVisitMedications.AutoSize = True
        Me.chkCSVisitMedications.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.chkCSVisitMedications.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSVisitMedications.Location = New System.Drawing.Point(326, 227)
        Me.chkCSVisitMedications.Name = "chkCSVisitMedications"
        Me.chkCSVisitMedications.Size = New System.Drawing.Size(247, 18)
        Me.chkCSVisitMedications.TabIndex = 8
        Me.chkCSVisitMedications.Tag = "CCDACSMedicationsVisit"
        Me.chkCSVisitMedications.Text = "Medications administered during the visit"
        Me.chkCSVisitMedications.UseVisualStyleBackColor = False
        '
        'chkCSVisitImmunization
        '
        Me.chkCSVisitImmunization.AutoSize = True
        Me.chkCSVisitImmunization.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.chkCSVisitImmunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSVisitImmunization.Location = New System.Drawing.Point(326, 260)
        Me.chkCSVisitImmunization.Name = "chkCSVisitImmunization"
        Me.chkCSVisitImmunization.Size = New System.Drawing.Size(261, 18)
        Me.chkCSVisitImmunization.TabIndex = 2
        Me.chkCSVisitImmunization.Tag = "CCDACSImmunizationsVisit"
        Me.chkCSVisitImmunization.Text = "Immunizations administered during the visit"
        Me.chkCSVisitImmunization.UseVisualStyleBackColor = False
        '
        'ChkTreatmentPlan
        '
        Me.ChkTreatmentPlan.AutoSize = True
        Me.ChkTreatmentPlan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkTreatmentPlan.Location = New System.Drawing.Point(46, 293)
        Me.ChkTreatmentPlan.Name = "ChkTreatmentPlan"
        Me.ChkTreatmentPlan.Size = New System.Drawing.Size(111, 18)
        Me.ChkTreatmentPlan.TabIndex = 3
        Me.ChkTreatmentPlan.Tag = "CCDATreatmentPlan"
        Me.ChkTreatmentPlan.Text = "Treatment Plan"
        Me.ChkTreatmentPlan.UseVisualStyleBackColor = True
        '
        'chkCSVisitReason
        '
        Me.chkCSVisitReason.AutoSize = True
        Me.chkCSVisitReason.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSVisitReason.Location = New System.Drawing.Point(646, 293)
        Me.chkCSVisitReason.Name = "chkCSVisitReason"
        Me.chkCSVisitReason.Size = New System.Drawing.Size(189, 18)
        Me.chkCSVisitReason.TabIndex = 7
        Me.chkCSVisitReason.Tag = "CCDACSReasonofVisit"
        Me.chkCSVisitReason.Text = "Reason of visit/ChiefComplaint"
        Me.chkCSVisitReason.UseVisualStyleBackColor = True
        '
        'cmbPurposeofUse
        '
        Me.cmbPurposeofUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPurposeofUse.ForeColor = System.Drawing.Color.Black
        Me.cmbPurposeofUse.FormattingEnabled = True
        Me.cmbPurposeofUse.Location = New System.Drawing.Point(183, 360)
        Me.cmbPurposeofUse.Name = "cmbPurposeofUse"
        Me.cmbPurposeofUse.Size = New System.Drawing.Size(655, 22)
        Me.cmbPurposeofUse.TabIndex = 69
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(82, 364)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 14)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "Purpose of Use :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCDAPrivacyText
        '
        Me.txtCDAPrivacyText.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCDAPrivacyText.Location = New System.Drawing.Point(183, 390)
        Me.txtCDAPrivacyText.Multiline = True
        Me.txtCDAPrivacyText.Name = "txtCDAPrivacyText"
        Me.txtCDAPrivacyText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCDAPrivacyText.Size = New System.Drawing.Size(655, 76)
        Me.txtCDAPrivacyText.TabIndex = 155
        '
        'Label1017
        '
        Me.Label1017.AutoSize = True
        Me.Label1017.BackColor = System.Drawing.Color.Transparent
        Me.Label1017.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1017.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1017.Location = New System.Drawing.Point(26, 395)
        Me.Label1017.Name = "Label1017"
        Me.Label1017.Size = New System.Drawing.Size(154, 14)
        Me.Label1017.TabIndex = 156
        Me.Label1017.Text = "Privacy and Security Info. :"
        Me.Label1017.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PnlMain
        '
        Me.PnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.PnlMain.Controls.Add(Me.pnlClinicalSummary)
        Me.PnlMain.Controls.Add(Me.Label14)
        Me.PnlMain.Controls.Add(Me.Label11)
        Me.PnlMain.Controls.Add(Me.Label12)
        Me.PnlMain.Controls.Add(Me.Label13)
        Me.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlMain.Location = New System.Drawing.Point(0, 53)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.PnlMain.Size = New System.Drawing.Size(873, 515)
        Me.PnlMain.TabIndex = 79
        '
        'FrmPatientCCDAConsent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(873, 568)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmPatientCCDAConsent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient CCDA Consent"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblMedication.ResumeLayout(False)
        Me.tblMedication.PerformLayout()
        Me.pnlClinicalSummary.ResumeLayout(False)
        Me.pnlClinicalSummary.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.PnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlClinicalSummary As System.Windows.Forms.Panel
    Friend WithEvents chkCSFutureAppt As System.Windows.Forms.CheckBox
    Friend WithEvents ChkCOAssessments As System.Windows.Forms.CheckBox
    Friend WithEvents ChkHealthConcerns As System.Windows.Forms.CheckBox
    Friend WithEvents ChkGoals As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSVisitMedications As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSVisitImmunization As System.Windows.Forms.CheckBox
    Friend WithEvents ChkTreatmentPlan As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSVisitReason As System.Windows.Forms.CheckBox
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents tlTooltip As System.Windows.Forms.ToolTip
    Friend WithEvents tblMedication As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtCDAPrivacyText As System.Windows.Forms.TextBox
    Friend WithEvents Label1017 As System.Windows.Forms.Label
    Friend WithEvents cmbPurposeofUse As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkImplant As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOProblems As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOAllergy As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOCareTeamMem As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOProcedures As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOVitalSigns As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOlabResult As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOLabTest As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOMedication As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSClinicalInstru As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOSocialHistory As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOFamilyHistory As System.Windows.Forms.CheckBox
    Friend WithEvents chkAmbImmunization As System.Windows.Forms.CheckBox
    Friend WithEvents tblSaveCls As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkHealthStatus As System.Windows.Forms.CheckBox
    Friend WithEvents chkInterventions As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransCareEncounter As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransCareCognitiveStat As System.Windows.Forms.CheckBox
    Friend WithEvents ChkAll As System.Windows.Forms.CheckBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label112 As System.Windows.Forms.Label
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents Label106 As System.Windows.Forms.Label
End Class
