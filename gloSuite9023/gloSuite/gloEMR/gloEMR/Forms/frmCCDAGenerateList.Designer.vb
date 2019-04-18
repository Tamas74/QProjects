<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCDAGenerateList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

                Try
                    If (IsNothing(dtpToDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpToDate)
                        Catch ex As Exception

                        End Try


                        dtpToDate.Dispose()
                        dtpToDate = Nothing
                    End If
                Catch
                End Try

                Try
                    If (IsNothing(dtpFrom) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpFrom)
                        Catch ex As Exception

                        End Try


                        dtpFrom.Dispose()
                        dtpFrom = Nothing
                    End If
                Catch
                End Try
                ' PrintDialog1 Clean up
                'Try

                '    If Not IsNothing(PrintDialog1) Then
                '        PrintDialog1.Dispose()
                '        PrintDialog1 = Nothing
                '    End If

                'Catch
                'End Try

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCCDAGenerateList))
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblMedication = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblShowCCD = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Print = New System.Windows.Forms.ToolStripButton()
        Me.tsb_Fax = New System.Windows.Forms.ToolStripButton()
        Me.tblSendPortal = New System.Windows.Forms.ToolStripButton()
        Me.tblSendPortalAMB = New System.Windows.Forms.ToolStripSplitButton()
        Me.tblSendPortalAMBPortal = New System.Windows.Forms.ToolStripMenuItem()
        Me.tblSendPortalAMBAPI = New System.Windows.Forms.ToolStripMenuItem()
        Me.tblSendPortalAMBBoth = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlbbtn_Email = New System.Windows.Forms.ToolStripButton()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.pnlClinicalSummary = New System.Windows.Forms.Panel()
        Me.ChkAll = New System.Windows.Forms.CheckBox()
        Me.pnlConfidentialityCode = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.CmbConfidentialityCode = New System.Windows.Forms.ComboBox()
        Me.chkCSProviderName = New System.Windows.Forms.CheckBox()
        Me.chkCSFutureAppt = New System.Windows.Forms.CheckBox()
        Me.chkCSOfcContact = New System.Windows.Forms.CheckBox()
        Me.chkCSRefOtrProvider = New System.Windows.Forms.CheckBox()
        Me.chkCSVisitInfo = New System.Windows.Forms.CheckBox()
        Me.chkCSDecisionAids = New System.Windows.Forms.CheckBox()
        Me.chkCSVisitMedications = New System.Windows.Forms.CheckBox()
        Me.chkCSVisitImmunization = New System.Windows.Forms.CheckBox()
        Me.chkCSDigTestPending = New System.Windows.Forms.CheckBox()
        Me.chkCSFutureTest = New System.Windows.Forms.CheckBox()
        Me.chkCSVisitReason = New System.Windows.Forms.CheckBox()
        Me.pnlTransitionCareRecord = New System.Windows.Forms.Panel()
        Me.chktransDateLocationvisit = New System.Windows.Forms.CheckBox()
        Me.ChkCareOfficeContact = New System.Windows.Forms.CheckBox()
        Me.ChkCareProvider = New System.Windows.Forms.CheckBox()
        Me.chkTransCareEncounter = New System.Windows.Forms.CheckBox()
        Me.chkTransCareImmunization = New System.Windows.Forms.CheckBox()
        Me.chkTransCareCognitiveStat = New System.Windows.Forms.CheckBox()
        Me.chkTransCareResReferral = New System.Windows.Forms.CheckBox()
        Me.chkTransCareRefProvider = New System.Windows.Forms.CheckBox()
        Me.chkTransCareFunctionalStat = New System.Windows.Forms.CheckBox()
        Me.pnlCareSummary = New System.Windows.Forms.Panel()
        Me.chkCareEncounterDiagnoses = New System.Windows.Forms.CheckBox()
        Me.chkCareplanImmunizations = New System.Windows.Forms.CheckBox()
        Me.chkHealthStatus = New System.Windows.Forms.CheckBox()
        Me.chkInterventions = New System.Windows.Forms.CheckBox()
        Me.pnlAmbulatorySummary = New System.Windows.Forms.Panel()
        Me.chkambDatelocationvisit = New System.Windows.Forms.CheckBox()
        Me.chkambEncounters = New System.Windows.Forms.CheckBox()
        Me.ChkAmbMental = New System.Windows.Forms.CheckBox()
        Me.ChkAmbReasonReferral = New System.Windows.Forms.CheckBox()
        Me.ChkAmbReferring = New System.Windows.Forms.CheckBox()
        Me.ChkAmbFunctionalStatus = New System.Windows.Forms.CheckBox()
        Me.chkAmbImmunization = New System.Windows.Forms.CheckBox()
        Me.chkAmbProviderContact = New System.Windows.Forms.CheckBox()
        Me.chkAmbProviderName = New System.Windows.Forms.CheckBox()
        Me.pnlCommonMUData = New System.Windows.Forms.Panel()
        Me.pnlPrintMessage = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblFormularyTransactionMessage = New System.Windows.Forms.Label()
        Me.chkImplant = New System.Windows.Forms.CheckBox()
        Me.chkPrivarySection = New System.Windows.Forms.CheckBox()
        Me.chkCODemographic = New System.Windows.Forms.CheckBox()
        Me.chkPrivaryText = New System.Windows.Forms.CheckBox()
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
        Me.ChkCOAssessments = New System.Windows.Forms.CheckBox()
        Me.ChkCOTreatmentPlan = New System.Windows.Forms.CheckBox()
        Me.ChkCOGoals = New System.Windows.Forms.CheckBox()
        Me.ChkCOHealthConcerns = New System.Windows.Forms.CheckBox()
        Me.chkCOSmoking = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.rbClearAll = New System.Windows.Forms.RadioButton()
        Me.rbSelectAllRestricted = New System.Windows.Forms.RadioButton()
        Me.rbSelectAllNormal = New System.Windows.Forms.RadioButton()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.cmbPurposeofUse = New System.Windows.Forms.ComboBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.pnlFormDate = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.chkintime = New System.Windows.Forms.CheckBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.chkDate = New System.Windows.Forms.CheckBox()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlExanDtl = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnExam = New System.Windows.Forms.Button()
        Me.lblDetails = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkPatientCopy = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbSummaryType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.rbClinicalSummery = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rbCareRecord = New System.Windows.Forms.RadioButton()
        Me.rbAmbulatorySummary = New System.Windows.Forms.RadioButton()
        Me.pnlCCDMessage = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblCCDMessage = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.pnlSelect = New System.Windows.Forms.Panel()
        Me.tlTooltip = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkOvrAdminSettings = New System.Windows.Forms.CheckBox()
        Me.pnlToolStrip.SuspendLayout()
        Me.tblMedication.SuspendLayout()
        Me.PnlMain.SuspendLayout()
        Me.pnlClinicalSummary.SuspendLayout()
        Me.pnlConfidentialityCode.SuspendLayout()
        Me.pnlTransitionCareRecord.SuspendLayout()
        Me.pnlCareSummary.SuspendLayout()
        Me.pnlAmbulatorySummary.SuspendLayout()
        Me.pnlCommonMUData.SuspendLayout()
        Me.pnlPrintMessage.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlFormDate.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlExanDtl.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlCCDMessage.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tblMedication)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(777, 54)
        Me.pnlToolStrip.TabIndex = 5
        '
        'tblMedication
        '
        Me.tblMedication.BackColor = System.Drawing.Color.Transparent
        Me.tblMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblMedication.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblMedication.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblMedication.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblShowCCD, Me.tlbbtn_Print, Me.tsb_Fax, Me.tblSendPortal, Me.tblSendPortalAMB, Me.tlbbtn_Email, Me.tblSave, Me.tblClose})
        Me.tblMedication.Location = New System.Drawing.Point(0, 0)
        Me.tblMedication.Name = "tblMedication"
        Me.tblMedication.Size = New System.Drawing.Size(777, 53)
        Me.tblMedication.TabIndex = 0
        Me.tblMedication.Text = "ToolStrip1"
        '
        'tblShowCCD
        '
        Me.tblShowCCD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblShowCCD.Image = CType(resources.GetObject("tblShowCCD.Image"), System.Drawing.Image)
        Me.tblShowCCD.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblShowCCD.Name = "tblShowCCD"
        Me.tblShowCCD.Size = New System.Drawing.Size(89, 50)
        Me.tblShowCCD.Tag = "Preview CDA"
        Me.tblShowCCD.Text = "Pre&view CDA"
        Me.tblShowCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblShowCCD.ToolTipText = "Preview CDA"
        '
        'tlbbtn_Print
        '
        Me.tlbbtn_Print.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Print.Image = CType(resources.GetObject("tlbbtn_Print.Image"), System.Drawing.Image)
        Me.tlbbtn_Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Print.Name = "tlbbtn_Print"
        Me.tlbbtn_Print.Size = New System.Drawing.Size(45, 50)
        Me.tlbbtn_Print.Tag = "Print"
        Me.tlbbtn_Print.Text = " &Print"
        Me.tlbbtn_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsb_Fax
        '
        Me.tsb_Fax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Fax.Image = CType(resources.GetObject("tsb_Fax.Image"), System.Drawing.Image)
        Me.tsb_Fax.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Fax.Name = "tsb_Fax"
        Me.tsb_Fax.Size = New System.Drawing.Size(36, 50)
        Me.tsb_Fax.Tag = "Fax"
        Me.tsb_Fax.Text = " &Fax"
        Me.tsb_Fax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_Fax.ToolTipText = "Fax"
        '
        'tblSendPortal
        '
        Me.tblSendPortal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSendPortal.Image = CType(resources.GetObject("tblSendPortal.Image"), System.Drawing.Image)
        Me.tblSendPortal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSendPortal.Name = "tblSendPortal"
        Me.tblSendPortal.Size = New System.Drawing.Size(101, 50)
        Me.tblSendPortal.Tag = "SendtoPortal"
        Me.tblSendPortal.Text = "Send to Portal"
        Me.tblSendPortal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSendPortal.ToolTipText = "Send to Portal"
        '
        'tblSendPortalAMB
        '
        Me.tblSendPortalAMB.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblSendPortalAMBPortal, Me.tblSendPortalAMBAPI, Me.tblSendPortalAMBBoth})
        Me.tblSendPortalAMB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSendPortalAMB.Image = CType(resources.GetObject("tblSendPortalAMB.Image"), System.Drawing.Image)
        Me.tblSendPortalAMB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSendPortalAMB.Name = "tblSendPortalAMB"
        Me.tblSendPortalAMB.Size = New System.Drawing.Size(121, 50)
        Me.tblSendPortalAMB.Tag = "SendtoPatient"
        Me.tblSendPortalAMB.Text = "Send to Patient"
        Me.tblSendPortalAMB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSendPortalAMB.ToolTipText = "Send to Patient"
        Me.tblSendPortalAMB.Visible = False
        '
        'tblSendPortalAMBPortal
        '
        Me.tblSendPortalAMBPortal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSendPortalAMBPortal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblSendPortalAMBPortal.Image = CType(resources.GetObject("tblSendPortalAMBPortal.Image"), System.Drawing.Image)
        Me.tblSendPortalAMBPortal.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tblSendPortalAMBPortal.Name = "tblSendPortalAMBPortal"
        Me.tblSendPortalAMBPortal.Size = New System.Drawing.Size(152, 22)
        Me.tblSendPortalAMBPortal.Tag = "Portal"
        Me.tblSendPortalAMBPortal.Text = "Portal"
        Me.tblSendPortalAMBPortal.ToolTipText = "Portal"
        '
        'tblSendPortalAMBAPI
        '
        Me.tblSendPortalAMBAPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSendPortalAMBAPI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblSendPortalAMBAPI.Image = CType(resources.GetObject("tblSendPortalAMBAPI.Image"), System.Drawing.Image)
        Me.tblSendPortalAMBAPI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tblSendPortalAMBAPI.Name = "tblSendPortalAMBAPI"
        Me.tblSendPortalAMBAPI.Size = New System.Drawing.Size(152, 22)
        Me.tblSendPortalAMBAPI.Tag = "API"
        Me.tblSendPortalAMBAPI.Text = "API"
        Me.tblSendPortalAMBAPI.ToolTipText = "API"
        '
        'tblSendPortalAMBBoth
        '
        Me.tblSendPortalAMBBoth.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSendPortalAMBBoth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblSendPortalAMBBoth.Image = CType(resources.GetObject("tblSendPortalAMBBoth.Image"), System.Drawing.Image)
        Me.tblSendPortalAMBBoth.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tblSendPortalAMBBoth.Name = "tblSendPortalAMBBoth"
        Me.tblSendPortalAMBBoth.Size = New System.Drawing.Size(152, 22)
        Me.tblSendPortalAMBBoth.Tag = "PortalAndAPI"
        Me.tblSendPortalAMBBoth.Text = "Portal and API"
        Me.tblSendPortalAMBBoth.ToolTipText = "Portal and API"
        '
        'tlbbtn_Email
        '
        Me.tlbbtn_Email.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Email.Image = CType(resources.GetObject("tlbbtn_Email.Image"), System.Drawing.Image)
        Me.tlbbtn_Email.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Email.Name = "tlbbtn_Email"
        Me.tlbbtn_Email.Size = New System.Drawing.Size(140, 50)
        Me.tlbbtn_Email.Tag = ""
        Me.tlbbtn_Email.Text = "Provider D&IRECT Msg"
        Me.tlbbtn_Email.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Email.ToolTipText = "Provider DIRECT Message"
        '
        'tblSave
        '
        Me.tblSave.Image = Global.gloEMR.My.Resources.Resources.Save_CDA
        Me.tblSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSave.Name = "tblSave"
        Me.tblSave.Size = New System.Drawing.Size(82, 50)
        Me.tblSave.Tag = "Export CDA"
        Me.tblSave.Text = "&Export CDA"
        Me.tblSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSave.ToolTipText = "Export CDA"
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
        'PnlMain
        '
        Me.PnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.PnlMain.Controls.Add(Me.pnlClinicalSummary)
        Me.PnlMain.Controls.Add(Me.pnlTransitionCareRecord)
        Me.PnlMain.Controls.Add(Me.pnlCareSummary)
        Me.PnlMain.Controls.Add(Me.pnlAmbulatorySummary)
        Me.PnlMain.Controls.Add(Me.pnlCommonMUData)
        Me.PnlMain.Controls.Add(Me.chkCOSmoking)
        Me.PnlMain.Controls.Add(Me.Panel2)
        Me.PnlMain.Controls.Add(Me.Panel7)
        Me.PnlMain.Controls.Add(Me.pnlFormDate)
        Me.PnlMain.Controls.Add(Me.Label14)
        Me.PnlMain.Controls.Add(Me.Label11)
        Me.PnlMain.Controls.Add(Me.Label12)
        Me.PnlMain.Controls.Add(Me.Label13)
        Me.PnlMain.Controls.Add(Me.pnlExanDtl)
        Me.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlMain.Location = New System.Drawing.Point(0, 94)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.PnlMain.Size = New System.Drawing.Size(777, 415)
        Me.PnlMain.TabIndex = 6
        '
        'pnlClinicalSummary
        '
        Me.pnlClinicalSummary.BackColor = System.Drawing.Color.Transparent
        Me.pnlClinicalSummary.Controls.Add(Me.ChkAll)
        Me.pnlClinicalSummary.Controls.Add(Me.pnlConfidentialityCode)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSProviderName)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSFutureAppt)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSOfcContact)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSRefOtrProvider)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSVisitInfo)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSDecisionAids)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSVisitMedications)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSVisitImmunization)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSDigTestPending)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSFutureTest)
        Me.pnlClinicalSummary.Controls.Add(Me.chkCSVisitReason)
        Me.pnlClinicalSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlClinicalSummary.Location = New System.Drawing.Point(454, 115)
        Me.pnlClinicalSummary.Name = "pnlClinicalSummary"
        Me.pnlClinicalSummary.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.pnlClinicalSummary.Size = New System.Drawing.Size(319, 296)
        Me.pnlClinicalSummary.TabIndex = 75
        '
        'ChkAll
        '
        Me.ChkAll.AutoSize = True
        Me.ChkAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAll.Location = New System.Drawing.Point(364, 413)
        Me.ChkAll.Name = "ChkAll"
        Me.ChkAll.Padding = New System.Windows.Forms.Padding(10, 5, 5, 5)
        Me.ChkAll.Size = New System.Drawing.Size(97, 28)
        Me.ChkAll.TabIndex = 0
        Me.ChkAll.Text = "Select All"
        Me.ChkAll.UseVisualStyleBackColor = True
        Me.ChkAll.Visible = False
        '
        'pnlConfidentialityCode
        '
        Me.pnlConfidentialityCode.Controls.Add(Me.Label27)
        Me.pnlConfidentialityCode.Controls.Add(Me.CmbConfidentialityCode)
        Me.pnlConfidentialityCode.Location = New System.Drawing.Point(283, 444)
        Me.pnlConfidentialityCode.Name = "pnlConfidentialityCode"
        Me.pnlConfidentialityCode.Size = New System.Drawing.Size(163, 25)
        Me.pnlConfidentialityCode.TabIndex = 1
        Me.pnlConfidentialityCode.Visible = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(7, 6)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(140, 14)
        Me.Label27.TabIndex = 70
        Me.Label27.Text = "Confidentiality Code :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CmbConfidentialityCode
        '
        Me.CmbConfidentialityCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbConfidentialityCode.ForeColor = System.Drawing.Color.Black
        Me.CmbConfidentialityCode.FormattingEnabled = True
        Me.CmbConfidentialityCode.Items.AddRange(New Object() {"Clinical summary", "Ambulatory summary", "Summary of care record"})
        Me.CmbConfidentialityCode.Location = New System.Drawing.Point(150, 2)
        Me.CmbConfidentialityCode.Name = "CmbConfidentialityCode"
        Me.CmbConfidentialityCode.Size = New System.Drawing.Size(93, 22)
        Me.CmbConfidentialityCode.TabIndex = 69
        '
        'chkCSProviderName
        '
        Me.chkCSProviderName.AutoSize = True
        Me.chkCSProviderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSProviderName.Location = New System.Drawing.Point(19, 8)
        Me.chkCSProviderName.Name = "chkCSProviderName"
        Me.chkCSProviderName.Size = New System.Drawing.Size(105, 18)
        Me.chkCSProviderName.TabIndex = 0
        Me.chkCSProviderName.Tag = "CCDACSProviderName"
        Me.chkCSProviderName.Text = "Provider Name"
        Me.chkCSProviderName.UseVisualStyleBackColor = True
        '
        'chkCSFutureAppt
        '
        Me.chkCSFutureAppt.AutoSize = True
        Me.chkCSFutureAppt.BackColor = System.Drawing.Color.Transparent
        Me.chkCSFutureAppt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSFutureAppt.Location = New System.Drawing.Point(19, 164)
        Me.chkCSFutureAppt.Name = "chkCSFutureAppt"
        Me.chkCSFutureAppt.Size = New System.Drawing.Size(143, 18)
        Me.chkCSFutureAppt.TabIndex = 4
        Me.chkCSFutureAppt.Tag = "CCDACSFutureAppointments"
        Me.chkCSFutureAppt.Text = "Future Appointments"
        Me.chkCSFutureAppt.UseVisualStyleBackColor = False
        '
        'chkCSOfcContact
        '
        Me.chkCSOfcContact.AutoSize = True
        Me.chkCSOfcContact.BackColor = System.Drawing.Color.Transparent
        Me.chkCSOfcContact.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSOfcContact.Location = New System.Drawing.Point(19, 269)
        Me.chkCSOfcContact.Name = "chkCSOfcContact"
        Me.chkCSOfcContact.Size = New System.Drawing.Size(172, 18)
        Me.chkCSOfcContact.TabIndex = 6
        Me.chkCSOfcContact.Tag = "CCDACSOfficeContactInformation"
        Me.chkCSOfcContact.Text = "Office Contact Information"
        Me.chkCSOfcContact.UseVisualStyleBackColor = False
        '
        'chkCSRefOtrProvider
        '
        Me.chkCSRefOtrProvider.AutoSize = True
        Me.chkCSRefOtrProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSRefOtrProvider.Location = New System.Drawing.Point(19, 60)
        Me.chkCSRefOtrProvider.Name = "chkCSRefOtrProvider"
        Me.chkCSRefOtrProvider.Size = New System.Drawing.Size(175, 18)
        Me.chkCSRefOtrProvider.TabIndex = 9
        Me.chkCSRefOtrProvider.Tag = "CCDACSReferralsToOtherProviders"
        Me.chkCSRefOtrProvider.Text = "Referrals to other Providers"
        Me.chkCSRefOtrProvider.UseVisualStyleBackColor = True
        '
        'chkCSVisitInfo
        '
        Me.chkCSVisitInfo.AutoSize = True
        Me.chkCSVisitInfo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSVisitInfo.Location = New System.Drawing.Point(19, 34)
        Me.chkCSVisitInfo.Name = "chkCSVisitInfo"
        Me.chkCSVisitInfo.Size = New System.Drawing.Size(165, 18)
        Me.chkCSVisitInfo.TabIndex = 1
        Me.chkCSVisitInfo.Tag = "CCDACSDateLocationofVisit"
        Me.chkCSVisitInfo.Text = "Date and Location of visit"
        Me.chkCSVisitInfo.UseVisualStyleBackColor = True
        '
        'chkCSDecisionAids
        '
        Me.chkCSDecisionAids.AutoSize = True
        Me.chkCSDecisionAids.BackColor = System.Drawing.Color.Transparent
        Me.chkCSDecisionAids.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSDecisionAids.Location = New System.Drawing.Point(19, 242)
        Me.chkCSDecisionAids.Name = "chkCSDecisionAids"
        Me.chkCSDecisionAids.Size = New System.Drawing.Size(225, 18)
        Me.chkCSDecisionAids.TabIndex = 10
        Me.chkCSDecisionAids.Tag = "CCDACSRecommendedPatientDecisionAids"
        Me.chkCSDecisionAids.Text = "Recommended Patient Decision Aids"
        Me.chkCSDecisionAids.UseVisualStyleBackColor = False
        '
        'chkCSVisitMedications
        '
        Me.chkCSVisitMedications.AutoSize = True
        Me.chkCSVisitMedications.BackColor = System.Drawing.Color.Transparent
        Me.chkCSVisitMedications.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSVisitMedications.Location = New System.Drawing.Point(19, 190)
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
        Me.chkCSVisitImmunization.BackColor = System.Drawing.Color.Transparent
        Me.chkCSVisitImmunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSVisitImmunization.Location = New System.Drawing.Point(19, 216)
        Me.chkCSVisitImmunization.Name = "chkCSVisitImmunization"
        Me.chkCSVisitImmunization.Size = New System.Drawing.Size(261, 18)
        Me.chkCSVisitImmunization.TabIndex = 2
        Me.chkCSVisitImmunization.Tag = "CCDACSImmunizationsVisit"
        Me.chkCSVisitImmunization.Text = "Immunizations administered during the visit"
        Me.chkCSVisitImmunization.UseVisualStyleBackColor = False
        '
        'chkCSDigTestPending
        '
        Me.chkCSDigTestPending.AutoSize = True
        Me.chkCSDigTestPending.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSDigTestPending.Location = New System.Drawing.Point(19, 112)
        Me.chkCSDigTestPending.Name = "chkCSDigTestPending"
        Me.chkCSDigTestPending.Size = New System.Drawing.Size(163, 18)
        Me.chkCSDigTestPending.TabIndex = 3
        Me.chkCSDigTestPending.Tag = "CCDACSDiagnosticTestsPending"
        Me.chkCSDigTestPending.Text = "Diagnostic Tests Pending"
        Me.chkCSDigTestPending.UseVisualStyleBackColor = True
        '
        'chkCSFutureTest
        '
        Me.chkCSFutureTest.AutoSize = True
        Me.chkCSFutureTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSFutureTest.Location = New System.Drawing.Point(19, 138)
        Me.chkCSFutureTest.Name = "chkCSFutureTest"
        Me.chkCSFutureTest.Size = New System.Drawing.Size(157, 18)
        Me.chkCSFutureTest.TabIndex = 5
        Me.chkCSFutureTest.Tag = "CCDACSFutureScheduledTests"
        Me.chkCSFutureTest.Text = "Future Scheduled Tests"
        Me.chkCSFutureTest.UseVisualStyleBackColor = True
        '
        'chkCSVisitReason
        '
        Me.chkCSVisitReason.AutoSize = True
        Me.chkCSVisitReason.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSVisitReason.Location = New System.Drawing.Point(19, 86)
        Me.chkCSVisitReason.Name = "chkCSVisitReason"
        Me.chkCSVisitReason.Size = New System.Drawing.Size(197, 18)
        Me.chkCSVisitReason.TabIndex = 7
        Me.chkCSVisitReason.Tag = "CCDACSReasonofVisit"
        Me.chkCSVisitReason.Text = "Reason of visit / ChiefComplaint"
        Me.chkCSVisitReason.UseVisualStyleBackColor = True
        '
        'pnlTransitionCareRecord
        '
        Me.pnlTransitionCareRecord.BackColor = System.Drawing.Color.Transparent
        Me.pnlTransitionCareRecord.Controls.Add(Me.chktransDateLocationvisit)
        Me.pnlTransitionCareRecord.Controls.Add(Me.ChkCareOfficeContact)
        Me.pnlTransitionCareRecord.Controls.Add(Me.ChkCareProvider)
        Me.pnlTransitionCareRecord.Controls.Add(Me.chkTransCareEncounter)
        Me.pnlTransitionCareRecord.Controls.Add(Me.chkTransCareImmunization)
        Me.pnlTransitionCareRecord.Controls.Add(Me.chkTransCareCognitiveStat)
        Me.pnlTransitionCareRecord.Controls.Add(Me.chkTransCareResReferral)
        Me.pnlTransitionCareRecord.Controls.Add(Me.chkTransCareRefProvider)
        Me.pnlTransitionCareRecord.Controls.Add(Me.chkTransCareFunctionalStat)
        Me.pnlTransitionCareRecord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTransitionCareRecord.Location = New System.Drawing.Point(454, 115)
        Me.pnlTransitionCareRecord.Name = "pnlTransitionCareRecord"
        Me.pnlTransitionCareRecord.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.pnlTransitionCareRecord.Size = New System.Drawing.Size(319, 296)
        Me.pnlTransitionCareRecord.TabIndex = 76
        '
        'chktransDateLocationvisit
        '
        Me.chktransDateLocationvisit.AutoSize = True
        Me.chktransDateLocationvisit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chktransDateLocationvisit.Location = New System.Drawing.Point(19, 34)
        Me.chktransDateLocationvisit.Name = "chktransDateLocationvisit"
        Me.chktransDateLocationvisit.Size = New System.Drawing.Size(165, 18)
        Me.chktransDateLocationvisit.TabIndex = 4
        Me.chktransDateLocationvisit.Tag = "CCDACSDateLocationofVisit"
        Me.chktransDateLocationvisit.Text = "Date and Location of visit"
        Me.chktransDateLocationvisit.UseVisualStyleBackColor = True
        '
        'ChkCareOfficeContact
        '
        Me.ChkCareOfficeContact.AutoSize = True
        Me.ChkCareOfficeContact.BackColor = System.Drawing.Color.Transparent
        Me.ChkCareOfficeContact.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCareOfficeContact.Location = New System.Drawing.Point(19, 138)
        Me.ChkCareOfficeContact.Name = "ChkCareOfficeContact"
        Me.ChkCareOfficeContact.Size = New System.Drawing.Size(172, 18)
        Me.ChkCareOfficeContact.TabIndex = 3
        Me.ChkCareOfficeContact.Text = "Office Contact Information"
        Me.ChkCareOfficeContact.UseVisualStyleBackColor = False
        '
        'ChkCareProvider
        '
        Me.ChkCareProvider.AutoSize = True
        Me.ChkCareProvider.BackColor = System.Drawing.Color.Transparent
        Me.ChkCareProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCareProvider.Location = New System.Drawing.Point(19, 86)
        Me.ChkCareProvider.Name = "ChkCareProvider"
        Me.ChkCareProvider.Size = New System.Drawing.Size(105, 18)
        Me.ChkCareProvider.TabIndex = 2
        Me.ChkCareProvider.Text = "Provider Name"
        Me.ChkCareProvider.UseVisualStyleBackColor = False
        '
        'chkTransCareEncounter
        '
        Me.chkTransCareEncounter.AutoSize = True
        Me.chkTransCareEncounter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransCareEncounter.Location = New System.Drawing.Point(19, 60)
        Me.chkTransCareEncounter.Name = "chkTransCareEncounter"
        Me.chkTransCareEncounter.Size = New System.Drawing.Size(141, 18)
        Me.chkTransCareEncounter.TabIndex = 0
        Me.chkTransCareEncounter.Tag = "CCDAEncounterDiagnoses"
        Me.chkTransCareEncounter.Text = "Encounter Diagnoses"
        Me.chkTransCareEncounter.UseVisualStyleBackColor = True
        '
        'chkTransCareImmunization
        '
        Me.chkTransCareImmunization.AutoSize = True
        Me.chkTransCareImmunization.BackColor = System.Drawing.Color.Transparent
        Me.chkTransCareImmunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransCareImmunization.Location = New System.Drawing.Point(19, 216)
        Me.chkTransCareImmunization.Name = "chkTransCareImmunization"
        Me.chkTransCareImmunization.Size = New System.Drawing.Size(103, 18)
        Me.chkTransCareImmunization.TabIndex = 0
        Me.chkTransCareImmunization.Text = "Immunizations"
        Me.chkTransCareImmunization.UseVisualStyleBackColor = False
        '
        'chkTransCareCognitiveStat
        '
        Me.chkTransCareCognitiveStat.AutoSize = True
        Me.chkTransCareCognitiveStat.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransCareCognitiveStat.Location = New System.Drawing.Point(19, 164)
        Me.chkTransCareCognitiveStat.Name = "chkTransCareCognitiveStat"
        Me.chkTransCareCognitiveStat.Size = New System.Drawing.Size(101, 18)
        Me.chkTransCareCognitiveStat.TabIndex = 1
        Me.chkTransCareCognitiveStat.Tag = "CCDAMentalStatus"
        Me.chkTransCareCognitiveStat.Text = "Mental Status"
        Me.chkTransCareCognitiveStat.UseVisualStyleBackColor = True
        '
        'chkTransCareResReferral
        '
        Me.chkTransCareResReferral.AutoSize = True
        Me.chkTransCareResReferral.BackColor = System.Drawing.Color.Transparent
        Me.chkTransCareResReferral.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransCareResReferral.Location = New System.Drawing.Point(19, 8)
        Me.chkTransCareResReferral.Name = "chkTransCareResReferral"
        Me.chkTransCareResReferral.Size = New System.Drawing.Size(129, 18)
        Me.chkTransCareResReferral.TabIndex = 0
        Me.chkTransCareResReferral.Tag = "CCDAReasonforReferral"
        Me.chkTransCareResReferral.Text = "Reason for Referral"
        Me.chkTransCareResReferral.UseVisualStyleBackColor = False
        '
        'chkTransCareRefProvider
        '
        Me.chkTransCareRefProvider.AutoSize = True
        Me.chkTransCareRefProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransCareRefProvider.Location = New System.Drawing.Point(19, 112)
        Me.chkTransCareRefProvider.Name = "chkTransCareRefProvider"
        Me.chkTransCareRefProvider.Size = New System.Drawing.Size(128, 18)
        Me.chkTransCareRefProvider.TabIndex = 1
        Me.chkTransCareRefProvider.Tag = "CCDAReferringProviders"
        Me.chkTransCareRefProvider.Text = "Referring Providers"
        Me.chkTransCareRefProvider.UseVisualStyleBackColor = True
        '
        'chkTransCareFunctionalStat
        '
        Me.chkTransCareFunctionalStat.AutoSize = True
        Me.chkTransCareFunctionalStat.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransCareFunctionalStat.Location = New System.Drawing.Point(19, 190)
        Me.chkTransCareFunctionalStat.Name = "chkTransCareFunctionalStat"
        Me.chkTransCareFunctionalStat.Size = New System.Drawing.Size(120, 18)
        Me.chkTransCareFunctionalStat.TabIndex = 0
        Me.chkTransCareFunctionalStat.Tag = "CCDAFunctionalStatus"
        Me.chkTransCareFunctionalStat.Text = "Functional Status"
        Me.chkTransCareFunctionalStat.UseVisualStyleBackColor = True
        '
        'pnlCareSummary
        '
        Me.pnlCareSummary.BackColor = System.Drawing.Color.Transparent
        Me.pnlCareSummary.Controls.Add(Me.chkCareEncounterDiagnoses)
        Me.pnlCareSummary.Controls.Add(Me.chkCareplanImmunizations)
        Me.pnlCareSummary.Controls.Add(Me.chkHealthStatus)
        Me.pnlCareSummary.Controls.Add(Me.chkInterventions)
        Me.pnlCareSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCareSummary.Location = New System.Drawing.Point(454, 115)
        Me.pnlCareSummary.Name = "pnlCareSummary"
        Me.pnlCareSummary.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.pnlCareSummary.Size = New System.Drawing.Size(319, 296)
        Me.pnlCareSummary.TabIndex = 79
        '
        'chkCareEncounterDiagnoses
        '
        Me.chkCareEncounterDiagnoses.AutoSize = True
        Me.chkCareEncounterDiagnoses.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCareEncounterDiagnoses.Location = New System.Drawing.Point(18, 60)
        Me.chkCareEncounterDiagnoses.Name = "chkCareEncounterDiagnoses"
        Me.chkCareEncounterDiagnoses.Size = New System.Drawing.Size(141, 18)
        Me.chkCareEncounterDiagnoses.TabIndex = 83
        Me.chkCareEncounterDiagnoses.Tag = "CCDAEncounterDiagnoses"
        Me.chkCareEncounterDiagnoses.Text = "Encounter Diagnoses"
        Me.chkCareEncounterDiagnoses.UseVisualStyleBackColor = True
        '
        'chkCareplanImmunizations
        '
        Me.chkCareplanImmunizations.AutoSize = True
        Me.chkCareplanImmunizations.BackColor = System.Drawing.Color.Transparent
        Me.chkCareplanImmunizations.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCareplanImmunizations.Location = New System.Drawing.Point(18, 34)
        Me.chkCareplanImmunizations.Name = "chkCareplanImmunizations"
        Me.chkCareplanImmunizations.Size = New System.Drawing.Size(103, 18)
        Me.chkCareplanImmunizations.TabIndex = 1
        Me.chkCareplanImmunizations.Text = "Immunizations"
        Me.chkCareplanImmunizations.UseVisualStyleBackColor = False
        '
        'chkHealthStatus
        '
        Me.chkHealthStatus.AutoSize = True
        Me.chkHealthStatus.BackColor = System.Drawing.Color.Transparent
        Me.chkHealthStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHealthStatus.Location = New System.Drawing.Point(18, 86)
        Me.chkHealthStatus.Name = "chkHealthStatus"
        Me.chkHealthStatus.Size = New System.Drawing.Size(248, 18)
        Me.chkHealthStatus.TabIndex = 0
        Me.chkHealthStatus.Tag = ""
        Me.chkHealthStatus.Text = "Health Status Evaluations and Outcomes"
        Me.chkHealthStatus.UseVisualStyleBackColor = False
        '
        'chkInterventions
        '
        Me.chkInterventions.AutoSize = True
        Me.chkInterventions.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInterventions.Location = New System.Drawing.Point(18, 8)
        Me.chkInterventions.Name = "chkInterventions"
        Me.chkInterventions.Size = New System.Drawing.Size(99, 18)
        Me.chkInterventions.TabIndex = 0
        Me.chkInterventions.Tag = ""
        Me.chkInterventions.Text = "Interventions"
        Me.chkInterventions.UseVisualStyleBackColor = True
        '
        'pnlAmbulatorySummary
        '
        Me.pnlAmbulatorySummary.BackColor = System.Drawing.Color.Transparent
        Me.pnlAmbulatorySummary.Controls.Add(Me.chkambDatelocationvisit)
        Me.pnlAmbulatorySummary.Controls.Add(Me.chkambEncounters)
        Me.pnlAmbulatorySummary.Controls.Add(Me.ChkAmbMental)
        Me.pnlAmbulatorySummary.Controls.Add(Me.ChkAmbReasonReferral)
        Me.pnlAmbulatorySummary.Controls.Add(Me.ChkAmbReferring)
        Me.pnlAmbulatorySummary.Controls.Add(Me.ChkAmbFunctionalStatus)
        Me.pnlAmbulatorySummary.Controls.Add(Me.chkAmbImmunization)
        Me.pnlAmbulatorySummary.Controls.Add(Me.chkAmbProviderContact)
        Me.pnlAmbulatorySummary.Controls.Add(Me.chkAmbProviderName)
        Me.pnlAmbulatorySummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAmbulatorySummary.Location = New System.Drawing.Point(454, 115)
        Me.pnlAmbulatorySummary.Name = "pnlAmbulatorySummary"
        Me.pnlAmbulatorySummary.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.pnlAmbulatorySummary.Size = New System.Drawing.Size(319, 296)
        Me.pnlAmbulatorySummary.TabIndex = 77
        '
        'chkambDatelocationvisit
        '
        Me.chkambDatelocationvisit.AutoSize = True
        Me.chkambDatelocationvisit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkambDatelocationvisit.Location = New System.Drawing.Point(18, 112)
        Me.chkambDatelocationvisit.Name = "chkambDatelocationvisit"
        Me.chkambDatelocationvisit.Size = New System.Drawing.Size(165, 18)
        Me.chkambDatelocationvisit.TabIndex = 85
        Me.chkambDatelocationvisit.Tag = "CCDACSDateLocationofVisit"
        Me.chkambDatelocationvisit.Text = "Date and Location of visit"
        Me.chkambDatelocationvisit.UseVisualStyleBackColor = True
        '
        'chkambEncounters
        '
        Me.chkambEncounters.AutoSize = True
        Me.chkambEncounters.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkambEncounters.Location = New System.Drawing.Point(18, 164)
        Me.chkambEncounters.Name = "chkambEncounters"
        Me.chkambEncounters.Size = New System.Drawing.Size(141, 18)
        Me.chkambEncounters.TabIndex = 82
        Me.chkambEncounters.Tag = "CCDAEncounterDiagnoses"
        Me.chkambEncounters.Text = "Encounter Diagnoses"
        Me.chkambEncounters.UseVisualStyleBackColor = True
        '
        'ChkAmbMental
        '
        Me.ChkAmbMental.AutoSize = True
        Me.ChkAmbMental.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAmbMental.Location = New System.Drawing.Point(18, 190)
        Me.ChkAmbMental.Name = "ChkAmbMental"
        Me.ChkAmbMental.Size = New System.Drawing.Size(101, 18)
        Me.ChkAmbMental.TabIndex = 83
        Me.ChkAmbMental.Tag = "CCDAMentalStatus"
        Me.ChkAmbMental.Text = "Mental Status"
        Me.ChkAmbMental.UseVisualStyleBackColor = True
        '
        'ChkAmbReasonReferral
        '
        Me.ChkAmbReasonReferral.AutoSize = True
        Me.ChkAmbReasonReferral.BackColor = System.Drawing.Color.Transparent
        Me.ChkAmbReasonReferral.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAmbReasonReferral.Location = New System.Drawing.Point(18, 60)
        Me.ChkAmbReasonReferral.Name = "ChkAmbReasonReferral"
        Me.ChkAmbReasonReferral.Size = New System.Drawing.Size(129, 18)
        Me.ChkAmbReasonReferral.TabIndex = 80
        Me.ChkAmbReasonReferral.Tag = "CCDAReasonforReferral"
        Me.ChkAmbReasonReferral.Text = "Reason for Referral"
        Me.ChkAmbReasonReferral.UseVisualStyleBackColor = False
        '
        'ChkAmbReferring
        '
        Me.ChkAmbReferring.AutoSize = True
        Me.ChkAmbReferring.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAmbReferring.Location = New System.Drawing.Point(18, 34)
        Me.ChkAmbReferring.Name = "ChkAmbReferring"
        Me.ChkAmbReferring.Size = New System.Drawing.Size(128, 18)
        Me.ChkAmbReferring.TabIndex = 84
        Me.ChkAmbReferring.Tag = "CCDAReferringProviders"
        Me.ChkAmbReferring.Text = "Referring Providers"
        Me.ChkAmbReferring.UseVisualStyleBackColor = True
        '
        'ChkAmbFunctionalStatus
        '
        Me.ChkAmbFunctionalStatus.AutoSize = True
        Me.ChkAmbFunctionalStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAmbFunctionalStatus.Location = New System.Drawing.Point(18, 216)
        Me.ChkAmbFunctionalStatus.Name = "ChkAmbFunctionalStatus"
        Me.ChkAmbFunctionalStatus.Size = New System.Drawing.Size(120, 18)
        Me.ChkAmbFunctionalStatus.TabIndex = 81
        Me.ChkAmbFunctionalStatus.Tag = "CCDAFunctionalStatus"
        Me.ChkAmbFunctionalStatus.Text = "Functional Status"
        Me.ChkAmbFunctionalStatus.UseVisualStyleBackColor = True
        '
        'chkAmbImmunization
        '
        Me.chkAmbImmunization.AutoSize = True
        Me.chkAmbImmunization.BackColor = System.Drawing.Color.Transparent
        Me.chkAmbImmunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAmbImmunization.Location = New System.Drawing.Point(18, 138)
        Me.chkAmbImmunization.Name = "chkAmbImmunization"
        Me.chkAmbImmunization.Size = New System.Drawing.Size(103, 18)
        Me.chkAmbImmunization.TabIndex = 1
        Me.chkAmbImmunization.Tag = "CCDAASImmunizations"
        Me.chkAmbImmunization.Text = "Immunizations"
        Me.chkAmbImmunization.UseVisualStyleBackColor = False
        '
        'chkAmbProviderContact
        '
        Me.chkAmbProviderContact.AutoSize = True
        Me.chkAmbProviderContact.BackColor = System.Drawing.Color.Transparent
        Me.chkAmbProviderContact.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAmbProviderContact.Location = New System.Drawing.Point(18, 86)
        Me.chkAmbProviderContact.Name = "chkAmbProviderContact"
        Me.chkAmbProviderContact.Size = New System.Drawing.Size(218, 18)
        Me.chkAmbProviderContact.TabIndex = 0
        Me.chkAmbProviderContact.Tag = "CCDAASProviderOfficeContactInformation"
        Me.chkAmbProviderContact.Text = "Provider office Contact Information"
        Me.chkAmbProviderContact.UseVisualStyleBackColor = False
        '
        'chkAmbProviderName
        '
        Me.chkAmbProviderName.AutoSize = True
        Me.chkAmbProviderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAmbProviderName.Location = New System.Drawing.Point(18, 8)
        Me.chkAmbProviderName.Name = "chkAmbProviderName"
        Me.chkAmbProviderName.Size = New System.Drawing.Size(104, 18)
        Me.chkAmbProviderName.TabIndex = 0
        Me.chkAmbProviderName.Tag = "CCDAASProviderName"
        Me.chkAmbProviderName.Text = "Provider name"
        Me.chkAmbProviderName.UseVisualStyleBackColor = True
        '
        'pnlCommonMUData
        '
        Me.pnlCommonMUData.BackColor = System.Drawing.Color.Transparent
        Me.pnlCommonMUData.Controls.Add(Me.pnlPrintMessage)
        Me.pnlCommonMUData.Controls.Add(Me.chkImplant)
        Me.pnlCommonMUData.Controls.Add(Me.chkPrivarySection)
        Me.pnlCommonMUData.Controls.Add(Me.chkCODemographic)
        Me.pnlCommonMUData.Controls.Add(Me.chkPrivaryText)
        Me.pnlCommonMUData.Controls.Add(Me.chkCOProblems)
        Me.pnlCommonMUData.Controls.Add(Me.chkCOAllergy)
        Me.pnlCommonMUData.Controls.Add(Me.chkCOCareTeamMem)
        Me.pnlCommonMUData.Controls.Add(Me.chkCOProcedures)
        Me.pnlCommonMUData.Controls.Add(Me.chkCOVitalSigns)
        Me.pnlCommonMUData.Controls.Add(Me.chkCOlabResult)
        Me.pnlCommonMUData.Controls.Add(Me.chkCOLabTest)
        Me.pnlCommonMUData.Controls.Add(Me.chkCOMedication)
        Me.pnlCommonMUData.Controls.Add(Me.chkCSClinicalInstru)
        Me.pnlCommonMUData.Controls.Add(Me.chkCOSocialHistory)
        Me.pnlCommonMUData.Controls.Add(Me.chkCOFamilyHistory)
        Me.pnlCommonMUData.Controls.Add(Me.ChkCOAssessments)
        Me.pnlCommonMUData.Controls.Add(Me.ChkCOTreatmentPlan)
        Me.pnlCommonMUData.Controls.Add(Me.ChkCOGoals)
        Me.pnlCommonMUData.Controls.Add(Me.ChkCOHealthConcerns)
        Me.pnlCommonMUData.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlCommonMUData.Location = New System.Drawing.Point(4, 115)
        Me.pnlCommonMUData.Name = "pnlCommonMUData"
        Me.pnlCommonMUData.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.pnlCommonMUData.Size = New System.Drawing.Size(450, 296)
        Me.pnlCommonMUData.TabIndex = 0
        '
        'pnlPrintMessage
        '
        Me.pnlPrintMessage.BackColor = System.Drawing.Color.White
        Me.pnlPrintMessage.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlPrintMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPrintMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPrintMessage.Controls.Add(Me.Label24)
        Me.pnlPrintMessage.Controls.Add(Me.lblFormularyTransactionMessage)
        Me.pnlPrintMessage.Location = New System.Drawing.Point(228, -90)
        Me.pnlPrintMessage.Name = "pnlPrintMessage"
        Me.pnlPrintMessage.Size = New System.Drawing.Size(228, 69)
        Me.pnlPrintMessage.TabIndex = 75
        Me.pnlPrintMessage.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(20, 7)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(119, 19)
        Me.Label24.TabIndex = 61
        Me.Label24.Text = "Please wait..."
        '
        'lblFormularyTransactionMessage
        '
        Me.lblFormularyTransactionMessage.AutoSize = True
        Me.lblFormularyTransactionMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblFormularyTransactionMessage.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormularyTransactionMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFormularyTransactionMessage.Location = New System.Drawing.Point(21, 33)
        Me.lblFormularyTransactionMessage.Name = "lblFormularyTransactionMessage"
        Me.lblFormularyTransactionMessage.Size = New System.Drawing.Size(186, 16)
        Me.lblFormularyTransactionMessage.TabIndex = 61
        Me.lblFormularyTransactionMessage.Text = "Printing CDA Information… "
        '
        'chkImplant
        '
        Me.chkImplant.AutoSize = True
        Me.chkImplant.BackColor = System.Drawing.Color.Transparent
        Me.chkImplant.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkImplant.Location = New System.Drawing.Point(19, 138)
        Me.chkImplant.Name = "chkImplant"
        Me.chkImplant.Size = New System.Drawing.Size(72, 18)
        Me.chkImplant.TabIndex = 14
        Me.chkImplant.Tag = "CCDAImplants"
        Me.chkImplant.Text = "Implants"
        Me.chkImplant.UseVisualStyleBackColor = False
        '
        'chkPrivarySection
        '
        Me.chkPrivarySection.AutoSize = True
        Me.chkPrivarySection.BackColor = System.Drawing.Color.Transparent
        Me.chkPrivarySection.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrivarySection.Location = New System.Drawing.Point(253, 190)
        Me.chkPrivarySection.Name = "chkPrivarySection"
        Me.chkPrivarySection.Size = New System.Drawing.Size(185, 18)
        Me.chkPrivarySection.TabIndex = 13
        Me.chkPrivarySection.Tag = ""
        Me.chkPrivarySection.Text = "Privacy and Security Markings"
        Me.chkPrivarySection.UseVisualStyleBackColor = False
        '
        'chkCODemographic
        '
        Me.chkCODemographic.AutoSize = True
        Me.chkCODemographic.Checked = True
        Me.chkCODemographic.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCODemographic.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCODemographic.Location = New System.Drawing.Point(19, 8)
        Me.chkCODemographic.Name = "chkCODemographic"
        Me.chkCODemographic.Size = New System.Drawing.Size(145, 18)
        Me.chkCODemographic.TabIndex = 0
        Me.chkCODemographic.Tag = "CCDAPatientDemographic"
        Me.chkCODemographic.Text = "Patient Demographics"
        Me.chkCODemographic.UseVisualStyleBackColor = True
        '
        'chkPrivaryText
        '
        Me.chkPrivaryText.AutoSize = True
        Me.chkPrivaryText.BackColor = System.Drawing.Color.Transparent
        Me.chkPrivaryText.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrivaryText.Location = New System.Drawing.Point(253, 216)
        Me.chkPrivaryText.Name = "chkPrivaryText"
        Me.chkPrivaryText.Size = New System.Drawing.Size(165, 18)
        Me.chkPrivaryText.TabIndex = 12
        Me.chkPrivaryText.Tag = ""
        Me.chkPrivaryText.Text = "Privacy and Security Info."
        Me.chkPrivaryText.UseVisualStyleBackColor = False
        Me.chkPrivaryText.Visible = False
        '
        'chkCOProblems
        '
        Me.chkCOProblems.AutoSize = True
        Me.chkCOProblems.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOProblems.Location = New System.Drawing.Point(19, 34)
        Me.chkCOProblems.Name = "chkCOProblems"
        Me.chkCOProblems.Size = New System.Drawing.Size(75, 18)
        Me.chkCOProblems.TabIndex = 1
        Me.chkCOProblems.Tag = "CCDAProblems"
        Me.chkCOProblems.Text = "Problems"
        Me.chkCOProblems.UseVisualStyleBackColor = True
        '
        'chkCOAllergy
        '
        Me.chkCOAllergy.AutoSize = True
        Me.chkCOAllergy.BackColor = System.Drawing.Color.Transparent
        Me.chkCOAllergy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOAllergy.Location = New System.Drawing.Point(19, 60)
        Me.chkCOAllergy.Name = "chkCOAllergy"
        Me.chkCOAllergy.Size = New System.Drawing.Size(130, 18)
        Me.chkCOAllergy.TabIndex = 2
        Me.chkCOAllergy.Tag = "CCDAMedicationAllergies"
        Me.chkCOAllergy.Text = "Medication allergies"
        Me.chkCOAllergy.UseVisualStyleBackColor = False
        '
        'chkCOCareTeamMem
        '
        Me.chkCOCareTeamMem.AutoSize = True
        Me.chkCOCareTeamMem.Checked = True
        Me.chkCOCareTeamMem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCOCareTeamMem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOCareTeamMem.Location = New System.Drawing.Point(19, 164)
        Me.chkCOCareTeamMem.Name = "chkCOCareTeamMem"
        Me.chkCOCareTeamMem.Size = New System.Drawing.Size(148, 18)
        Me.chkCOCareTeamMem.TabIndex = 11
        Me.chkCOCareTeamMem.Tag = "CCDACareTeamMember"
        Me.chkCOCareTeamMem.Text = "Care Team Member(s)"
        Me.chkCOCareTeamMem.UseVisualStyleBackColor = True
        '
        'chkCOProcedures
        '
        Me.chkCOProcedures.AutoSize = True
        Me.chkCOProcedures.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOProcedures.Location = New System.Drawing.Point(19, 190)
        Me.chkCOProcedures.Name = "chkCOProcedures"
        Me.chkCOProcedures.Size = New System.Drawing.Size(87, 18)
        Me.chkCOProcedures.TabIndex = 4
        Me.chkCOProcedures.Tag = "CCDAProcedures"
        Me.chkCOProcedures.Text = "Procedures"
        Me.chkCOProcedures.UseVisualStyleBackColor = True
        '
        'chkCOVitalSigns
        '
        Me.chkCOVitalSigns.AutoSize = True
        Me.chkCOVitalSigns.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOVitalSigns.Location = New System.Drawing.Point(19, 112)
        Me.chkCOVitalSigns.Name = "chkCOVitalSigns"
        Me.chkCOVitalSigns.Size = New System.Drawing.Size(81, 18)
        Me.chkCOVitalSigns.TabIndex = 10
        Me.chkCOVitalSigns.Tag = "CCDAVitalSigns"
        Me.chkCOVitalSigns.Text = "Vital Signs"
        Me.chkCOVitalSigns.UseVisualStyleBackColor = True
        '
        'chkCOlabResult
        '
        Me.chkCOlabResult.AutoSize = True
        Me.chkCOlabResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOlabResult.Location = New System.Drawing.Point(19, 242)
        Me.chkCOlabResult.Name = "chkCOlabResult"
        Me.chkCOlabResult.Size = New System.Drawing.Size(181, 18)
        Me.chkCOlabResult.TabIndex = 3
        Me.chkCOlabResult.Tag = "CCDALaboratoryValue"
        Me.chkCOlabResult.Text = "Laboratory value(s)/result(s)"
        Me.chkCOlabResult.UseVisualStyleBackColor = True
        '
        'chkCOLabTest
        '
        Me.chkCOLabTest.AutoSize = True
        Me.chkCOLabTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOLabTest.Location = New System.Drawing.Point(19, 216)
        Me.chkCOLabTest.Name = "chkCOLabTest"
        Me.chkCOLabTest.Size = New System.Drawing.Size(128, 18)
        Me.chkCOLabTest.TabIndex = 9
        Me.chkCOLabTest.Tag = "CCDALaboratoryTest"
        Me.chkCOLabTest.Text = "Laboratory Test(s)"
        Me.chkCOLabTest.UseVisualStyleBackColor = True
        '
        'chkCOMedication
        '
        Me.chkCOMedication.AutoSize = True
        Me.chkCOMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOMedication.Location = New System.Drawing.Point(19, 86)
        Me.chkCOMedication.Name = "chkCOMedication"
        Me.chkCOMedication.Size = New System.Drawing.Size(89, 18)
        Me.chkCOMedication.TabIndex = 7
        Me.chkCOMedication.Tag = "CCDAMedications"
        Me.chkCOMedication.Text = "Medications"
        Me.chkCOMedication.UseVisualStyleBackColor = True
        '
        'chkCSClinicalInstru
        '
        Me.chkCSClinicalInstru.AutoSize = True
        Me.chkCSClinicalInstru.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCSClinicalInstru.Location = New System.Drawing.Point(253, 60)
        Me.chkCSClinicalInstru.Name = "chkCSClinicalInstru"
        Me.chkCSClinicalInstru.Size = New System.Drawing.Size(128, 18)
        Me.chkCSClinicalInstru.TabIndex = 12
        Me.chkCSClinicalInstru.Tag = "CCDAClinicalInstructions"
        Me.chkCSClinicalInstru.Text = "Clinical Instructions"
        Me.chkCSClinicalInstru.UseVisualStyleBackColor = True
        '
        'chkCOSocialHistory
        '
        Me.chkCOSocialHistory.AutoSize = True
        Me.chkCOSocialHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOSocialHistory.Location = New System.Drawing.Point(253, 34)
        Me.chkCOSocialHistory.Name = "chkCOSocialHistory"
        Me.chkCOSocialHistory.Size = New System.Drawing.Size(97, 18)
        Me.chkCOSocialHistory.TabIndex = 13
        Me.chkCOSocialHistory.Tag = "CCDASocialHistory"
        Me.chkCOSocialHistory.Text = "Social History"
        Me.chkCOSocialHistory.UseVisualStyleBackColor = True
        '
        'chkCOFamilyHistory
        '
        Me.chkCOFamilyHistory.AutoSize = True
        Me.chkCOFamilyHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOFamilyHistory.Location = New System.Drawing.Point(253, 8)
        Me.chkCOFamilyHistory.Name = "chkCOFamilyHistory"
        Me.chkCOFamilyHistory.Size = New System.Drawing.Size(99, 18)
        Me.chkCOFamilyHistory.TabIndex = 6
        Me.chkCOFamilyHistory.Tag = "CCDAFamilyHistory"
        Me.chkCOFamilyHistory.Text = "Family History"
        Me.chkCOFamilyHistory.UseVisualStyleBackColor = True
        '
        'ChkCOAssessments
        '
        Me.ChkCOAssessments.AutoSize = True
        Me.ChkCOAssessments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCOAssessments.Location = New System.Drawing.Point(253, 112)
        Me.ChkCOAssessments.Name = "ChkCOAssessments"
        Me.ChkCOAssessments.Size = New System.Drawing.Size(95, 18)
        Me.ChkCOAssessments.TabIndex = 15
        Me.ChkCOAssessments.Tag = "CCDAAssessments"
        Me.ChkCOAssessments.Text = "Assessments"
        Me.ChkCOAssessments.UseVisualStyleBackColor = True
        '
        'ChkCOTreatmentPlan
        '
        Me.ChkCOTreatmentPlan.AutoSize = True
        Me.ChkCOTreatmentPlan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCOTreatmentPlan.Location = New System.Drawing.Point(253, 164)
        Me.ChkCOTreatmentPlan.Name = "ChkCOTreatmentPlan"
        Me.ChkCOTreatmentPlan.Size = New System.Drawing.Size(111, 18)
        Me.ChkCOTreatmentPlan.TabIndex = 16
        Me.ChkCOTreatmentPlan.Tag = "CCDATreatment"
        Me.ChkCOTreatmentPlan.Text = "Treatment Plan"
        Me.ChkCOTreatmentPlan.UseVisualStyleBackColor = True
        '
        'ChkCOGoals
        '
        Me.ChkCOGoals.AutoSize = True
        Me.ChkCOGoals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCOGoals.Location = New System.Drawing.Point(253, 86)
        Me.ChkCOGoals.Name = "ChkCOGoals"
        Me.ChkCOGoals.Size = New System.Drawing.Size(54, 18)
        Me.ChkCOGoals.TabIndex = 17
        Me.ChkCOGoals.Tag = "CCDAGoals"
        Me.ChkCOGoals.Text = "Goals"
        Me.ChkCOGoals.UseVisualStyleBackColor = True
        '
        'ChkCOHealthConcerns
        '
        Me.ChkCOHealthConcerns.AutoSize = True
        Me.ChkCOHealthConcerns.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCOHealthConcerns.Location = New System.Drawing.Point(253, 138)
        Me.ChkCOHealthConcerns.Name = "ChkCOHealthConcerns"
        Me.ChkCOHealthConcerns.Size = New System.Drawing.Size(115, 18)
        Me.ChkCOHealthConcerns.TabIndex = 18
        Me.ChkCOHealthConcerns.Tag = "CCDAHealthConcerns"
        Me.ChkCOHealthConcerns.Text = "Health Concerns"
        Me.ChkCOHealthConcerns.UseVisualStyleBackColor = True
        '
        'chkCOSmoking
        '
        Me.chkCOSmoking.AutoSize = True
        Me.chkCOSmoking.BackColor = System.Drawing.Color.Transparent
        Me.chkCOSmoking.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCOSmoking.Location = New System.Drawing.Point(559, 611)
        Me.chkCOSmoking.Name = "chkCOSmoking"
        Me.chkCOSmoking.Size = New System.Drawing.Size(111, 18)
        Me.chkCOSmoking.TabIndex = 8
        Me.chkCOSmoking.Tag = "CCDASmokingStatus"
        Me.chkCOSmoking.Text = "Smoking Status"
        Me.chkCOSmoking.UseVisualStyleBackColor = False
        Me.chkCOSmoking.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkOvrAdminSettings)
        Me.Panel2.Controls.Add(Me.Label29)
        Me.Panel2.Controls.Add(Me.rbClearAll)
        Me.Panel2.Controls.Add(Me.rbSelectAllRestricted)
        Me.Panel2.Controls.Add(Me.rbSelectAllNormal)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(4, 86)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(769, 29)
        Me.Panel2.TabIndex = 2
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label29.Location = New System.Drawing.Point(0, 28)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(769, 1)
        Me.Label29.TabIndex = 17
        '
        'rbClearAll
        '
        Me.rbClearAll.AutoSize = True
        Me.rbClearAll.Location = New System.Drawing.Point(447, 5)
        Me.rbClearAll.Name = "rbClearAll"
        Me.rbClearAll.Size = New System.Drawing.Size(71, 18)
        Me.rbClearAll.TabIndex = 2
        Me.rbClearAll.TabStop = True
        Me.rbClearAll.Text = "Clear All "
        Me.rbClearAll.UseVisualStyleBackColor = True
        '
        'rbSelectAllRestricted
        '
        Me.rbSelectAllRestricted.AutoSize = True
        Me.rbSelectAllRestricted.Location = New System.Drawing.Point(229, 5)
        Me.rbSelectAllRestricted.Name = "rbSelectAllRestricted"
        Me.rbSelectAllRestricted.Size = New System.Drawing.Size(144, 18)
        Me.rbSelectAllRestricted.TabIndex = 1
        Me.rbSelectAllRestricted.TabStop = True
        Me.rbSelectAllRestricted.Text = "Select All (Restricted)"
        Me.rbSelectAllRestricted.UseVisualStyleBackColor = True
        '
        'rbSelectAllNormal
        '
        Me.rbSelectAllNormal.AutoSize = True
        Me.rbSelectAllNormal.Checked = True
        Me.rbSelectAllNormal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSelectAllNormal.Location = New System.Drawing.Point(18, 5)
        Me.rbSelectAllNormal.Name = "rbSelectAllNormal"
        Me.rbSelectAllNormal.Size = New System.Drawing.Size(137, 18)
        Me.rbSelectAllNormal.TabIndex = 0
        Me.rbSelectAllNormal.TabStop = True
        Me.rbSelectAllNormal.Text = "Select All (Normal)"
        Me.rbSelectAllNormal.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.Controls.Add(Me.Label28)
        Me.Panel7.Controls.Add(Me.cmbPurposeofUse)
        Me.Panel7.Controls.Add(Me.Label30)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(4, 58)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(769, 28)
        Me.Panel7.TabIndex = 80
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(13, 7)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(98, 14)
        Me.Label28.TabIndex = 72
        Me.Label28.Text = "Purpose of Use :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPurposeofUse
        '
        Me.cmbPurposeofUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPurposeofUse.ForeColor = System.Drawing.Color.Black
        Me.cmbPurposeofUse.FormattingEnabled = True
        Me.cmbPurposeofUse.Location = New System.Drawing.Point(116, 3)
        Me.cmbPurposeofUse.Name = "cmbPurposeofUse"
        Me.cmbPurposeofUse.Size = New System.Drawing.Size(576, 22)
        Me.cmbPurposeofUse.TabIndex = 71
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label30.Location = New System.Drawing.Point(0, 27)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(769, 1)
        Me.Label30.TabIndex = 71
        '
        'pnlFormDate
        '
        Me.pnlFormDate.Controls.Add(Me.Panel6)
        Me.pnlFormDate.Controls.Add(Me.chkDate)
        Me.pnlFormDate.Controls.Add(Me.lblFromDate)
        Me.pnlFormDate.Controls.Add(Me.Label5)
        Me.pnlFormDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFormDate.Location = New System.Drawing.Point(4, 30)
        Me.pnlFormDate.Name = "pnlFormDate"
        Me.pnlFormDate.Size = New System.Drawing.Size(769, 28)
        Me.pnlFormDate.TabIndex = 72
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.lblToDate)
        Me.Panel6.Controls.Add(Me.chkintime)
        Me.Panel6.Controls.Add(Me.dtpFrom)
        Me.Panel6.Controls.Add(Me.dtpToDate)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(114, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(655, 27)
        Me.Panel6.TabIndex = 72
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.BackColor = System.Drawing.Color.Transparent
        Me.lblToDate.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblToDate.Location = New System.Drawing.Point(316, 6)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(64, 14)
        Me.lblToDate.TabIndex = 68
        Me.lblToDate.Text = "To Date : "
        Me.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkintime
        '
        Me.chkintime.AutoSize = True
        Me.chkintime.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkintime.Location = New System.Drawing.Point(0, 0)
        Me.chkintime.Name = "chkintime"
        Me.chkintime.Padding = New System.Windows.Forms.Padding(15, 3, 0, 0)
        Me.chkintime.Size = New System.Drawing.Size(112, 27)
        Me.chkintime.TabIndex = 71
        Me.chkintime.Text = "Include Time"
        Me.chkintime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkintime.UseVisualStyleBackColor = True
        '
        'dtpFrom
        '
        Me.dtpFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpFrom.CustomFormat = "MM/dd/yyyy  hh:mm:ss tt"
        Me.dtpFrom.Enabled = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(114, 2)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(192, 22)
        Me.dtpFrom.TabIndex = 67
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpToDate.CustomFormat = "MM/dd/yyyy  hh:mm:ss tt"
        Me.dtpToDate.Enabled = False
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(383, 2)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(195, 22)
        Me.dtpToDate.TabIndex = 69
        '
        'chkDate
        '
        Me.chkDate.AutoSize = True
        Me.chkDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkDate.Location = New System.Drawing.Point(84, 0)
        Me.chkDate.Name = "chkDate"
        Me.chkDate.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.chkDate.Size = New System.Drawing.Size(30, 27)
        Me.chkDate.TabIndex = 70
        Me.chkDate.Text = ":"
        Me.chkDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkDate.UseVisualStyleBackColor = True
        '
        'lblFromDate
        '
        Me.lblFromDate.BackColor = System.Drawing.Color.Transparent
        Me.lblFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFromDate.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblFromDate.Location = New System.Drawing.Point(0, 0)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(84, 27)
        Me.lblFromDate.TabIndex = 66
        Me.lblFromDate.Text = "From Date "
        Me.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(0, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(769, 1)
        Me.Label5.TabIndex = 71
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(773, 30)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 381)
        Me.Label14.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(3, 30)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 381)
        Me.Label11.TabIndex = 17
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Location = New System.Drawing.Point(3, 411)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(771, 1)
        Me.Label12.TabIndex = 16
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Location = New System.Drawing.Point(3, 29)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(771, 1)
        Me.Label13.TabIndex = 15
        '
        'pnlExanDtl
        '
        Me.pnlExanDtl.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlExanDtl.Controls.Add(Me.Panel4)
        Me.pnlExanDtl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlExanDtl.Location = New System.Drawing.Point(3, 0)
        Me.pnlExanDtl.Name = "pnlExanDtl"
        Me.pnlExanDtl.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlExanDtl.Size = New System.Drawing.Size(771, 29)
        Me.pnlExanDtl.TabIndex = 78
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label19)
        Me.Panel4.Controls.Add(Me.btnExam)
        Me.Panel4.Controls.Add(Me.lblDetails)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(771, 26)
        Me.Panel4.TabIndex = 74
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Location = New System.Drawing.Point(747, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 24)
        Me.Label19.TabIndex = 80
        '
        'btnExam
        '
        Me.btnExam.BackColor = System.Drawing.Color.Transparent
        Me.btnExam.BackgroundImage = CType(resources.GetObject("btnExam.BackgroundImage"), System.Drawing.Image)
        Me.btnExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExam.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExam.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnExam.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnExam.FlatAppearance.BorderSize = 0
        Me.btnExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExam.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExam.Image = CType(resources.GetObject("btnExam.Image"), System.Drawing.Image)
        Me.btnExam.Location = New System.Drawing.Point(748, 1)
        Me.btnExam.Name = "btnExam"
        Me.btnExam.Size = New System.Drawing.Size(22, 24)
        Me.btnExam.TabIndex = 75
        Me.btnExam.Text = "          "
        Me.btnExam.UseVisualStyleBackColor = False
        '
        'lblDetails
        '
        Me.lblDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDetails.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDetails.Location = New System.Drawing.Point(1, 1)
        Me.lblDetails.Name = "lblDetails"
        Me.lblDetails.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblDetails.Size = New System.Drawing.Size(769, 24)
        Me.lblDetails.TabIndex = 71
        Me.lblDetails.Text = "Sample"
        Me.lblDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Location = New System.Drawing.Point(1, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(769, 1)
        Me.Label10.TabIndex = 79
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Location = New System.Drawing.Point(1, 25)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(769, 1)
        Me.Label9.TabIndex = 78
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 26)
        Me.Label7.TabIndex = 76
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Location = New System.Drawing.Point(770, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 26)
        Me.Label8.TabIndex = 77
        '
        'chkPatientCopy
        '
        Me.chkPatientCopy.AutoSize = True
        Me.chkPatientCopy.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.chkPatientCopy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPatientCopy.Location = New System.Drawing.Point(400, 11)
        Me.chkPatientCopy.Name = "chkPatientCopy"
        Me.chkPatientCopy.Size = New System.Drawing.Size(150, 18)
        Me.chkPatientCopy.TabIndex = 2
        Me.chkPatientCopy.Text = "Indicate 'Patient Copy'"
        Me.chkPatientCopy.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.Controls.Add(Me.chkPatientCopy)
        Me.Panel1.Controls.Add(Me.cmbSummaryType)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.rbClinicalSummery)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.rbCareRecord)
        Me.Panel1.Controls.Add(Me.rbAmbulatorySummary)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(777, 40)
        Me.Panel1.TabIndex = 73
        '
        'cmbSummaryType
        '
        Me.cmbSummaryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSummaryType.ForeColor = System.Drawing.Color.Black
        Me.cmbSummaryType.FormattingEnabled = True
        Me.cmbSummaryType.Items.AddRange(New Object() {"Clinical summary", "Ambulatory summary", "Summary of care record", "Care Plan"})
        Me.cmbSummaryType.Location = New System.Drawing.Point(119, 9)
        Me.cmbSummaryType.Name = "cmbSummaryType"
        Me.cmbSummaryType.Size = New System.Drawing.Size(274, 22)
        Me.cmbSummaryType.TabIndex = 68
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(18, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 14)
        Me.Label6.TabIndex = 67
        Me.Label6.Text = "Summary Type :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rbClinicalSummery
        '
        Me.rbClinicalSummery.AutoSize = True
        Me.rbClinicalSummery.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbClinicalSummery.Location = New System.Drawing.Point(400, 11)
        Me.rbClinicalSummery.Name = "rbClinicalSummery"
        Me.rbClinicalSummery.Size = New System.Drawing.Size(111, 18)
        Me.rbClinicalSummery.TabIndex = 19
        Me.rbClinicalSummery.TabStop = True
        Me.rbClinicalSummery.Text = "Clinical summary"
        Me.rbClinicalSummery.UseVisualStyleBackColor = True
        Me.rbClinicalSummery.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label1.Location = New System.Drawing.Point(773, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 32)
        Me.Label1.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(3, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 32)
        Me.Label2.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(3, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(771, 1)
        Me.Label3.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(771, 1)
        Me.Label4.TabIndex = 15
        '
        'rbCareRecord
        '
        Me.rbCareRecord.AutoSize = True
        Me.rbCareRecord.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.rbCareRecord.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCareRecord.Location = New System.Drawing.Point(596, 11)
        Me.rbCareRecord.Name = "rbCareRecord"
        Me.rbCareRecord.Size = New System.Drawing.Size(156, 18)
        Me.rbCareRecord.TabIndex = 19
        Me.rbCareRecord.TabStop = True
        Me.rbCareRecord.Text = "Summary of care record"
        Me.rbCareRecord.UseVisualStyleBackColor = False
        Me.rbCareRecord.Visible = False
        '
        'rbAmbulatorySummary
        '
        Me.rbAmbulatorySummary.AutoSize = True
        Me.rbAmbulatorySummary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbAmbulatorySummary.Location = New System.Drawing.Point(596, 11)
        Me.rbAmbulatorySummary.Name = "rbAmbulatorySummary"
        Me.rbAmbulatorySummary.Size = New System.Drawing.Size(139, 18)
        Me.rbAmbulatorySummary.TabIndex = 19
        Me.rbAmbulatorySummary.TabStop = True
        Me.rbAmbulatorySummary.Text = "Ambulatory summary"
        Me.rbAmbulatorySummary.UseVisualStyleBackColor = True
        Me.rbAmbulatorySummary.Visible = False
        '
        'pnlCCDMessage
        '
        Me.pnlCCDMessage.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlCCDMessage.Controls.Add(Me.Panel5)
        Me.pnlCCDMessage.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlCCDMessage.Location = New System.Drawing.Point(0, 509)
        Me.pnlCCDMessage.Name = "pnlCCDMessage"
        Me.pnlCCDMessage.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlCCDMessage.Size = New System.Drawing.Size(777, 63)
        Me.pnlCCDMessage.TabIndex = 74
        Me.pnlCCDMessage.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.lblCCDMessage)
        Me.Panel5.Controls.Add(Me.Label26)
        Me.Panel5.Controls.Add(Me.Label25)
        Me.Panel5.Controls.Add(Me.Label23)
        Me.Panel5.Controls.Add(Me.Label22)
        Me.Panel5.Controls.Add(Me.Label21)
        Me.Panel5.Controls.Add(Me.Label20)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(771, 60)
        Me.Panel5.TabIndex = 74
        '
        'lblCCDMessage
        '
        Me.lblCCDMessage.BackColor = System.Drawing.Color.White
        Me.lblCCDMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblCCDMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCCDMessage.Location = New System.Drawing.Point(5, 5)
        Me.lblCCDMessage.Multiline = True
        Me.lblCCDMessage.Name = "lblCCDMessage"
        Me.lblCCDMessage.ReadOnly = True
        Me.lblCCDMessage.Size = New System.Drawing.Size(765, 54)
        Me.lblCCDMessage.TabIndex = 0
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Location = New System.Drawing.Point(1, 5)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(4, 54)
        Me.Label26.TabIndex = 75
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.White
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Location = New System.Drawing.Point(1, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(769, 4)
        Me.Label25.TabIndex = 74
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Location = New System.Drawing.Point(1, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(769, 1)
        Me.Label23.TabIndex = 73
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Location = New System.Drawing.Point(1, 59)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(769, 1)
        Me.Label22.TabIndex = 72
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label21.Location = New System.Drawing.Point(770, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 60)
        Me.Label21.TabIndex = 19
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 60)
        Me.Label20.TabIndex = 18
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.WebBrowser1)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Label16)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Location = New System.Drawing.Point(139, 295)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(414, 45)
        Me.Panel3.TabIndex = 76
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(23, 22)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(406, 112)
        Me.WebBrowser1.TabIndex = 17
        Me.WebBrowser1.Visible = False
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Location = New System.Drawing.Point(4, 41)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(406, 1)
        Me.Label18.TabIndex = 17
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Location = New System.Drawing.Point(4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(406, 1)
        Me.Label17.TabIndex = 16
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Location = New System.Drawing.Point(3, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 42)
        Me.Label16.TabIndex = 8
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Location = New System.Drawing.Point(410, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 42)
        Me.Label15.TabIndex = 7
        '
        'pnlSelect
        '
        Me.pnlSelect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSelect.Location = New System.Drawing.Point(0, 0)
        Me.pnlSelect.Name = "pnlSelect"
        Me.pnlSelect.Size = New System.Drawing.Size(777, 572)
        Me.pnlSelect.TabIndex = 1
        '
        'chkOvrAdminSettings
        '
        Me.chkOvrAdminSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkOvrAdminSettings.AutoSize = True
        Me.chkOvrAdminSettings.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOvrAdminSettings.Location = New System.Drawing.Point(592, 5)
        Me.chkOvrAdminSettings.Name = "chkOvrAdminSettings"
        Me.chkOvrAdminSettings.Size = New System.Drawing.Size(154, 18)
        Me.chkOvrAdminSettings.TabIndex = 18
        Me.chkOvrAdminSettings.Tag = ""
        Me.chkOvrAdminSettings.Text = "Override CCDA sections"
        Me.chkOvrAdminSettings.UseVisualStyleBackColor = True
        '
        'frmCCDAGenerateList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(777, 572)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.pnlCCDMessage)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.pnlSelect)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCCDAGenerateList"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate CDA"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblMedication.ResumeLayout(False)
        Me.tblMedication.PerformLayout()
        Me.PnlMain.ResumeLayout(False)
        Me.PnlMain.PerformLayout()
        Me.pnlClinicalSummary.ResumeLayout(False)
        Me.pnlClinicalSummary.PerformLayout()
        Me.pnlConfidentialityCode.ResumeLayout(False)
        Me.pnlConfidentialityCode.PerformLayout()
        Me.pnlTransitionCareRecord.ResumeLayout(False)
        Me.pnlTransitionCareRecord.PerformLayout()
        Me.pnlCareSummary.ResumeLayout(False)
        Me.pnlCareSummary.PerformLayout()
        Me.pnlAmbulatorySummary.ResumeLayout(False)
        Me.pnlAmbulatorySummary.PerformLayout()
        Me.pnlCommonMUData.ResumeLayout(False)
        Me.pnlCommonMUData.PerformLayout()
        Me.pnlPrintMessage.ResumeLayout(False)
        Me.pnlPrintMessage.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.pnlFormDate.ResumeLayout(False)
        Me.pnlFormDate.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.pnlExanDtl.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlCCDMessage.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tblMedication As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtn_Print As System.Windows.Forms.ToolStripButton
    Public WithEvents tlbbtn_Email As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblSendPortal As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents chkCODemographic As System.Windows.Forms.CheckBox
    Friend WithEvents ChkAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOProblems As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOSmoking As System.Windows.Forms.CheckBox
    Friend WithEvents pnlFormDate As System.Windows.Forms.Panel
    Public WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Public WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkDate As System.Windows.Forms.CheckBox
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rbCareRecord As System.Windows.Forms.RadioButton
    Friend WithEvents rbAmbulatorySummary As System.Windows.Forms.RadioButton
    Friend WithEvents rbClinicalSummery As System.Windows.Forms.RadioButton
    Friend WithEvents pnlClinicalSummary As System.Windows.Forms.Panel
    Friend WithEvents chkCSClinicalInstru As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSProviderName As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSFutureAppt As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSOfcContact As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSRefOtrProvider As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSVisitInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSDecisionAids As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSVisitImmunization As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSDigTestPending As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSFutureTest As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSVisitReason As System.Windows.Forms.CheckBox
    Friend WithEvents pnlCommonMUData As System.Windows.Forms.Panel
    Friend WithEvents chkCOAllergy As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOLabTest As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOMedication As System.Windows.Forms.CheckBox
    Friend WithEvents pnlAmbulatorySummary As System.Windows.Forms.Panel
    Friend WithEvents chkAmbProviderName As System.Windows.Forms.CheckBox
    Friend WithEvents chkAmbProviderContact As System.Windows.Forms.CheckBox
    Friend WithEvents pnlTransitionCareRecord As System.Windows.Forms.Panel
    Friend WithEvents chkTransCareEncounter As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransCareImmunization As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransCareCognitiveStat As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransCareResReferral As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransCareRefProvider As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransCareFunctionalStat As System.Windows.Forms.CheckBox
    Friend WithEvents pnlCCDMessage As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkCSVisitMedications As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOCareTeamMem As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOProcedures As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOVitalSigns As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOlabResult As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlExanDtl As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblDetails As System.Windows.Forms.Label
    Friend WithEvents pnlPrintMessage As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblFormularyTransactionMessage As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblCCDMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnExam As System.Windows.Forms.Button
    Public WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblShowCCD As System.Windows.Forms.ToolStripButton
    Friend WithEvents ChkCareOfficeContact As System.Windows.Forms.CheckBox
    Friend WithEvents ChkCareProvider As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbSummaryType As System.Windows.Forms.ComboBox
    Friend WithEvents pnlSelect As System.Windows.Forms.Panel
    Friend WithEvents chkCOSocialHistory As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOFamilyHistory As System.Windows.Forms.CheckBox
    Friend WithEvents chkAmbImmunization As System.Windows.Forms.CheckBox
    Friend WithEvents chkPatientCopy As System.Windows.Forms.CheckBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents tlTooltip As System.Windows.Forms.ToolTip
    Private WithEvents tsb_Fax As System.Windows.Forms.ToolStripButton
    Friend WithEvents CmbConfidentialityCode As System.Windows.Forms.ComboBox
    Friend WithEvents chkPrivaryText As System.Windows.Forms.CheckBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents pnlConfidentialityCode As System.Windows.Forms.Panel
    Friend WithEvents chkPrivarySection As System.Windows.Forms.CheckBox
    Friend WithEvents chkImplant As System.Windows.Forms.CheckBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkintime As System.Windows.Forms.CheckBox
    Friend WithEvents ChkCOAssessments As System.Windows.Forms.CheckBox
    Friend WithEvents ChkCOTreatmentPlan As System.Windows.Forms.CheckBox
    Friend WithEvents ChkCOGoals As System.Windows.Forms.CheckBox
    Friend WithEvents ChkCOHealthConcerns As System.Windows.Forms.CheckBox
    Friend WithEvents pnlCareSummary As System.Windows.Forms.Panel
    Friend WithEvents chkHealthStatus As System.Windows.Forms.CheckBox
    Friend WithEvents chkInterventions As System.Windows.Forms.CheckBox
    Friend WithEvents chkambEncounters As System.Windows.Forms.CheckBox
    Friend WithEvents ChkAmbMental As System.Windows.Forms.CheckBox
    Friend WithEvents ChkAmbReasonReferral As System.Windows.Forms.CheckBox
    Friend WithEvents ChkAmbReferring As System.Windows.Forms.CheckBox
    Friend WithEvents ChkAmbFunctionalStatus As System.Windows.Forms.CheckBox
    Friend WithEvents chktransDateLocationvisit As System.Windows.Forms.CheckBox
    Friend WithEvents chkambDatelocationvisit As System.Windows.Forms.CheckBox
    Friend WithEvents chkCareplanImmunizations As System.Windows.Forms.CheckBox
    Friend WithEvents tblSendPortalAMB As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tblSendPortalAMBPortal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tblSendPortalAMBAPI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tblSendPortalAMBBoth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkCareEncounterDiagnoses As System.Windows.Forms.CheckBox
    Friend WithEvents rbClearAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbSelectAllRestricted As System.Windows.Forms.RadioButton
    Friend WithEvents rbSelectAllNormal As System.Windows.Forms.RadioButton
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents cmbPurposeofUse As System.Windows.Forms.ComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents chkOvrAdminSettings As System.Windows.Forms.CheckBox
    'Private WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    '  Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    ' Friend WithEvents OvalShape1 As Microsoft.VisualBasic.PowerPacks.OvalShape
End Class
