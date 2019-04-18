<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloToolBarUserCtrl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
    Protected Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloToolBarUserCtrl))
        Me.pnlToolBar = New System.Windows.Forms.Panel()
        Me.GeneralToolBar = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tStrpPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.tStrpQuickPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.tStrpOpenPrintDialog = New System.Windows.Forms.ToolStripMenuItem()
        Me.tStrpeDrugLink = New System.Windows.Forms.ToolStripMenuItem()
        Me.tStrpFax = New System.Windows.Forms.ToolStripSplitButton()
        Me.SendFaxImmediatelyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendFaxNormalPriorityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tStrpPrvRx = New System.Windows.Forms.ToolStripButton()
        Me.tStrpERx = New System.Windows.Forms.ToolStripButton()
        Me.tStrpSendRx = New System.Windows.Forms.ToolStripButton()
        Me.tStrpRxFill = New System.Windows.Forms.ToolStripButton()
        Me.tlb_Reconcile = New System.Windows.Forms.ToolStripButton()
        Me.tStrpSaveRxMed = New System.Windows.Forms.ToolStripButton()
        Me.tStrpSave = New System.Windows.Forms.ToolStripButton()
        Me.tStrpClose = New System.Windows.Forms.ToolStripButton()
        Me.tblCCD = New System.Windows.Forms.ToolStripButton()
        Me.tStrpShowHide = New System.Windows.Forms.ToolStripButton()
        Me.tStrpVwDeniedReport = New System.Windows.Forms.ToolStripButton()
        Me.tStrpNKMedications = New System.Windows.Forms.ToolStripButton()
        Me.tlb_PlanOfTreatment = New System.Windows.Forms.ToolStripButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlToolBar.SuspendLayout()
        Me.GeneralToolBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolBar
        '
        Me.pnlToolBar.Controls.Add(Me.GeneralToolBar)
        Me.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlToolBar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(936, 53)
        Me.pnlToolBar.TabIndex = 0
        '
        'GeneralToolBar
        '
        Me.GeneralToolBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GeneralToolBar.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.GeneralToolBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GeneralToolBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GeneralToolBar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GeneralToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.GeneralToolBar.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.GeneralToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tStrpPrint, Me.tStrpFax, Me.tStrpPrvRx, Me.tStrpERx, Me.tStrpSendRx, Me.tStrpRxFill, Me.tlb_Reconcile, Me.tStrpSaveRxMed, Me.tStrpSave, Me.tStrpClose, Me.tblCCD, Me.tStrpShowHide, Me.tStrpVwDeniedReport, Me.tStrpNKMedications, Me.tlb_PlanOfTreatment})
        Me.GeneralToolBar.Location = New System.Drawing.Point(0, 0)
        Me.GeneralToolBar.Name = "GeneralToolBar"
        Me.GeneralToolBar.Size = New System.Drawing.Size(936, 53)
        Me.GeneralToolBar.TabIndex = 0
        Me.GeneralToolBar.Text = "ToolStrip1"
        '
        'tStrpPrint
        '
        Me.tStrpPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tStrpQuickPrint, Me.tStrpOpenPrintDialog, Me.tStrpeDrugLink})
        Me.tStrpPrint.Image = CType(resources.GetObject("tStrpPrint.Image"), System.Drawing.Image)
        Me.tStrpPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpPrint.Name = "tStrpPrint"
        Me.tStrpPrint.Size = New System.Drawing.Size(53, 50)
        Me.tStrpPrint.Text = "&Print"
        Me.tStrpPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpPrint.ToolTipText = "Print"
        '
        'tStrpQuickPrint
        '
        Me.tStrpQuickPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tStrpQuickPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpQuickPrint.Image = CType(resources.GetObject("tStrpQuickPrint.Image"), System.Drawing.Image)
        Me.tStrpQuickPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tStrpQuickPrint.Name = "tStrpQuickPrint"
        Me.tStrpQuickPrint.Size = New System.Drawing.Size(133, 22)
        Me.tStrpQuickPrint.Text = "Quick Print"
        '
        'tStrpOpenPrintDialog
        '
        Me.tStrpOpenPrintDialog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tStrpOpenPrintDialog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpOpenPrintDialog.Image = CType(resources.GetObject("tStrpOpenPrintDialog.Image"), System.Drawing.Image)
        Me.tStrpOpenPrintDialog.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tStrpOpenPrintDialog.Name = "tStrpOpenPrintDialog"
        Me.tStrpOpenPrintDialog.Size = New System.Drawing.Size(133, 22)
        Me.tStrpOpenPrintDialog.Text = "Print"
        '
        'tStrpeDrugLink
        '
        Me.tStrpeDrugLink.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tStrpeDrugLink.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpeDrugLink.Image = CType(resources.GetObject("tStrpeDrugLink.Image"), System.Drawing.Image)
        Me.tStrpeDrugLink.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tStrpeDrugLink.Name = "tStrpeDrugLink"
        Me.tStrpeDrugLink.Size = New System.Drawing.Size(133, 22)
        Me.tStrpeDrugLink.Tag = "elink"
        Me.tStrpeDrugLink.Text = "eLink"
        '
        'tStrpFax
        '
        Me.tStrpFax.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendFaxImmediatelyToolStripMenuItem, Me.SendFaxNormalPriorityToolStripMenuItem})
        Me.tStrpFax.Image = CType(resources.GetObject("tStrpFax.Image"), System.Drawing.Image)
        Me.tStrpFax.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpFax.Name = "tStrpFax"
        Me.tStrpFax.Size = New System.Drawing.Size(48, 50)
        Me.tStrpFax.Text = "&Fax"
        Me.tStrpFax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpFax.ToolTipText = "Fax "
        '
        'SendFaxImmediatelyToolStripMenuItem
        '
        Me.SendFaxImmediatelyToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SendFaxImmediatelyToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.SendFaxImmediatelyToolStripMenuItem.Image = CType(resources.GetObject("SendFaxImmediatelyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SendFaxImmediatelyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SendFaxImmediatelyToolStripMenuItem.Name = "SendFaxImmediatelyToolStripMenuItem"
        Me.SendFaxImmediatelyToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.SendFaxImmediatelyToolStripMenuItem.Text = "Send Fax Immediately"
        '
        'SendFaxNormalPriorityToolStripMenuItem
        '
        Me.SendFaxNormalPriorityToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SendFaxNormalPriorityToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.SendFaxNormalPriorityToolStripMenuItem.Image = CType(resources.GetObject("SendFaxNormalPriorityToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SendFaxNormalPriorityToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SendFaxNormalPriorityToolStripMenuItem.Name = "SendFaxNormalPriorityToolStripMenuItem"
        Me.SendFaxNormalPriorityToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.SendFaxNormalPriorityToolStripMenuItem.Text = "Send Fax Normal Priority"
        '
        'tStrpPrvRx
        '
        Me.tStrpPrvRx.BackColor = System.Drawing.Color.Transparent
        Me.tStrpPrvRx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpPrvRx.Image = CType(resources.GetObject("tStrpPrvRx.Image"), System.Drawing.Image)
        Me.tStrpPrvRx.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpPrvRx.Name = "tStrpPrvRx"
        Me.tStrpPrvRx.Size = New System.Drawing.Size(51, 50)
        Me.tStrpPrvRx.Text = " Prv&Rx"
        Me.tStrpPrvRx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpPrvRx.ToolTipText = "Previous Prescription"
        '
        'tStrpERx
        '
        Me.tStrpERx.BackColor = System.Drawing.Color.Transparent
        Me.tStrpERx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpERx.Image = CType(resources.GetObject("tStrpERx.Image"), System.Drawing.Image)
        Me.tStrpERx.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpERx.Name = "tStrpERx"
        Me.tStrpERx.Size = New System.Drawing.Size(36, 50)
        Me.tStrpERx.Text = "&eRx"
        Me.tStrpERx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpERx.ToolTipText = "e-Prescription"
        '
        'tStrpSendRx
        '
        Me.tStrpSendRx.BackColor = System.Drawing.Color.Transparent
        Me.tStrpSendRx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpSendRx.Image = CType(resources.GetObject("tStrpSendRx.Image"), System.Drawing.Image)
        Me.tStrpSendRx.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpSendRx.Name = "tStrpSendRx"
        Me.tStrpSendRx.Size = New System.Drawing.Size(63, 50)
        Me.tStrpSendRx.Text = "&Issue Rx"
        Me.tStrpSendRx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpSendRx.ToolTipText = "Issue Prescription"
        '
        'tStrpRxFill
        '
        Me.tStrpRxFill.BackColor = System.Drawing.Color.Transparent
        Me.tStrpRxFill.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.tStrpRxFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpRxFill.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpRxFill.Image = CType(resources.GetObject("tStrpRxFill.Image"), System.Drawing.Image)
        Me.tStrpRxFill.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpRxFill.Name = "tStrpRxFill"
        Me.tStrpRxFill.Size = New System.Drawing.Size(42, 50)
        Me.tStrpRxFill.Text = "&RxFill"
        Me.tStrpRxFill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpRxFill.ToolTipText = "RxFill Notifications"
        '
        'tlb_Reconcile
        '
        Me.tlb_Reconcile.BackColor = System.Drawing.Color.Transparent
        Me.tlb_Reconcile.BackgroundImage = CType(resources.GetObject("tlb_Reconcile.BackgroundImage"), System.Drawing.Image)
        Me.tlb_Reconcile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlb_Reconcile.Enabled = False
        Me.tlb_Reconcile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_Reconcile.Image = CType(resources.GetObject("tlb_Reconcile.Image"), System.Drawing.Image)
        Me.tlb_Reconcile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Reconcile.Name = "tlb_Reconcile"
        Me.tlb_Reconcile.Size = New System.Drawing.Size(68, 50)
        Me.tlb_Reconcile.Tag = "Reconcile"
        Me.tlb_Reconcile.Text = "&Reconcile"
        Me.tlb_Reconcile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Reconcile.ToolTipText = "Reconcile"
        '
        'tStrpSaveRxMed
        '
        Me.tStrpSaveRxMed.BackColor = System.Drawing.Color.Transparent
        Me.tStrpSaveRxMed.BackgroundImage = CType(resources.GetObject("tStrpSaveRxMed.BackgroundImage"), System.Drawing.Image)
        Me.tStrpSaveRxMed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpSaveRxMed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpSaveRxMed.Image = CType(resources.GetObject("tStrpSaveRxMed.Image"), System.Drawing.Image)
        Me.tStrpSaveRxMed.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpSaveRxMed.Name = "tStrpSaveRxMed"
        Me.tStrpSaveRxMed.Size = New System.Drawing.Size(40, 50)
        Me.tStrpSaveRxMed.Text = "Sa&ve"
        Me.tStrpSaveRxMed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpSaveRxMed.ToolTipText = "Save"
        '
        'tStrpSave
        '
        Me.tStrpSave.BackColor = System.Drawing.Color.Transparent
        Me.tStrpSave.BackgroundImage = CType(resources.GetObject("tStrpSave.BackgroundImage"), System.Drawing.Image)
        Me.tStrpSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpSave.Image = CType(resources.GetObject("tStrpSave.Image"), System.Drawing.Image)
        Me.tStrpSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpSave.Name = "tStrpSave"
        Me.tStrpSave.Size = New System.Drawing.Size(66, 50)
        Me.tStrpSave.Text = "&Save&&Cls"
        Me.tStrpSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpSave.ToolTipText = "Save and Close"
        '
        'tStrpClose
        '
        Me.tStrpClose.BackColor = System.Drawing.Color.Transparent
        Me.tStrpClose.BackgroundImage = CType(resources.GetObject("tStrpClose.BackgroundImage"), System.Drawing.Image)
        Me.tStrpClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpClose.Image = CType(resources.GetObject("tStrpClose.Image"), System.Drawing.Image)
        Me.tStrpClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpClose.Name = "tStrpClose"
        Me.tStrpClose.Size = New System.Drawing.Size(43, 50)
        Me.tStrpClose.Text = "&Close"
        Me.tStrpClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpClose.ToolTipText = "Close "
        '
        'tblCCD
        '
        Me.tblCCD.BackColor = System.Drawing.Color.Transparent
        Me.tblCCD.BackgroundImage = CType(resources.GetObject("tblCCD.BackgroundImage"), System.Drawing.Image)
        Me.tblCCD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblCCD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblCCD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblCCD.Image = CType(resources.GetObject("tblCCD.Image"), System.Drawing.Image)
        Me.tblCCD.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tblCCD.Name = "tblCCD"
        Me.tblCCD.Size = New System.Drawing.Size(63, 50)
        Me.tblCCD.Tag = "Gen CCD"
        Me.tblCCD.Text = "&Gen CCD"
        Me.tblCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblCCD.ToolTipText = "Generate CCD"
        '
        'tStrpShowHide
        '
        Me.tStrpShowHide.BackColor = System.Drawing.Color.Transparent
        Me.tStrpShowHide.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.tStrpShowHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpShowHide.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpShowHide.Image = CType(resources.GetObject("tStrpShowHide.Image"), System.Drawing.Image)
        Me.tStrpShowHide.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpShowHide.Name = "tStrpShowHide"
        Me.tStrpShowHide.Size = New System.Drawing.Size(46, 50)
        Me.tStrpShowHide.Text = "&Show"
        Me.tStrpShowHide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tStrpVwDeniedReport
        '
        Me.tStrpVwDeniedReport.BackColor = System.Drawing.Color.Transparent
        Me.tStrpVwDeniedReport.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.tStrpVwDeniedReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpVwDeniedReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpVwDeniedReport.Image = CType(resources.GetObject("tStrpVwDeniedReport.Image"), System.Drawing.Image)
        Me.tStrpVwDeniedReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpVwDeniedReport.Name = "tStrpVwDeniedReport"
        Me.tStrpVwDeniedReport.Size = New System.Drawing.Size(74, 50)
        Me.tStrpVwDeniedReport.Text = "&Vw DyReq"
        Me.tStrpVwDeniedReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpVwDeniedReport.ToolTipText = "View Denied Refill Request"
        '
        'tStrpNKMedications
        '
        Me.tStrpNKMedications.BackColor = System.Drawing.Color.Transparent
        Me.tStrpNKMedications.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.tStrpNKMedications.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpNKMedications.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpNKMedications.Image = CType(resources.GetObject("tStrpNKMedications.Image"), System.Drawing.Image)
        Me.tStrpNKMedications.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpNKMedications.Name = "tStrpNKMedications"
        Me.tStrpNKMedications.Size = New System.Drawing.Size(112, 50)
        Me.tStrpNKMedications.Tag = "NKMedications"
        Me.tStrpNKMedications.Text = "&N.K. Medications"
        Me.tStrpNKMedications.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpNKMedications.ToolTipText = "No Known Medications"
        '
        'tlb_PlanOfTreatment
        '
        Me.tlb_PlanOfTreatment.BackColor = System.Drawing.Color.Transparent
        Me.tlb_PlanOfTreatment.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.tlb_PlanOfTreatment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlb_PlanOfTreatment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_PlanOfTreatment.Image = CType(resources.GetObject("tlb_PlanOfTreatment.Image"), System.Drawing.Image)
        Me.tlb_PlanOfTreatment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_PlanOfTreatment.Name = "tlb_PlanOfTreatment"
        Me.tlb_PlanOfTreatment.Size = New System.Drawing.Size(36, 50)
        Me.tlb_PlanOfTreatment.Text = "P&oT"
        Me.tlb_PlanOfTreatment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_PlanOfTreatment.ToolTipText = "Plan of Treatment"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'gloToolBarUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.pnlToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloToolBarUserCtrl"
        Me.Size = New System.Drawing.Size(936, 53)
        Me.pnlToolBar.ResumeLayout(False)
        Me.pnlToolBar.PerformLayout()
        Me.GeneralToolBar.ResumeLayout(False)
        Me.GeneralToolBar.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents pnlToolBar As System.Windows.Forms.Panel
    Public WithEvents GeneralToolBar As gloGlobal.gloToolStripIgnoreFocus
    Public WithEvents tStrpSave As System.Windows.Forms.ToolStripButton
    Public WithEvents tStrpClose As System.Windows.Forms.ToolStripButton
    Public WithEvents tStrpShowHide As System.Windows.Forms.ToolStripButton
    Public WithEvents tStrpFax As System.Windows.Forms.ToolStripSplitButton
    Public WithEvents SendFaxNormalPriorityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents SendFaxImmediatelyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents tStrpPrvRx As System.Windows.Forms.ToolStripButton
    Public WithEvents tStrpSendRx As System.Windows.Forms.ToolStripButton
    Public WithEvents tStrpERx As System.Windows.Forms.ToolStripButton
    Public WithEvents tStrpSaveRxMed As System.Windows.Forms.ToolStripButton
    Public WithEvents tStrpPrint As System.Windows.Forms.ToolStripSplitButton
    Public WithEvents tStrpQuickPrint As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents tStrpOpenPrintDialog As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents tStrpVwDeniedReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblCCD As System.Windows.Forms.ToolStripButton
    Public WithEvents tStrpeDrugLink As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents tlb_Reconcile As System.Windows.Forms.ToolStripButton
    Public WithEvents tStrpRxFill As System.Windows.Forms.ToolStripButton
    Public WithEvents tStrpNKMedications As System.Windows.Forms.ToolStripButton
    Public WithEvents tlb_PlanOfTreatment As System.Windows.Forms.ToolStripButton

End Class
