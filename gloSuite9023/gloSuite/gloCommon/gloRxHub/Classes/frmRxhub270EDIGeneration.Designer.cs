namespace gloRxHub
{
    partial class frmRxhub270EDIGeneration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRxhub270EDIGeneration));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ts_Commands = new System.Windows.Forms.ToolStrip();
            this.tsb_GenEDI270 = new System.Windows.Forms.ToolStripDropDownButton();
            this.singleCoverageBenefitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multipleCoverageBenefitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientDeterminedAtPayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EDI997atRxHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tA1MessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientNotFoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nAKErrorAtRxHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_POSTEDI270 = new System.Windows.Forms.ToolStripButton();
            this.tsb_EDI270 = new System.Windows.Forms.ToolStripButton();
            this.tsb_SingleCovrgPhBenefit = new System.Windows.Forms.ToolStripButton();
            this.tsb_MultipleCovrgPhBenefit = new System.Windows.Forms.ToolStripButton();
            this.tsb_PatDeterminedatPayer = new System.Windows.Forms.ToolStripButton();
            this.tsb_997atRxHub = new System.Windows.Forms.ToolStripButton();
            this.tsb_TA1Msg_RxHub = new System.Windows.Forms.ToolStripButton();
            this.tsb_PatientNotFound_RxHub = new System.Windows.Forms.ToolStripButton();
            this.tsb_NAKErr_RxHub = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.txtEDIOutput = new System.Windows.Forms.TextBox();
            this.pnlTop.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.ts_Commands);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(961, 56);
            this.pnlTop.TabIndex = 5;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_GenEDI270,
            this.tsb_POSTEDI270,
            this.tsb_EDI270,
            this.tsb_SingleCovrgPhBenefit,
            this.tsb_MultipleCovrgPhBenefit,
            this.tsb_PatDeterminedatPayer,
            this.tsb_997atRxHub,
            this.tsb_TA1Msg_RxHub,
            this.tsb_PatientNotFound_RxHub,
            this.tsb_NAKErr_RxHub,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(961, 53);
            this.ts_Commands.TabIndex = 58;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_GenEDI270
            // 
            this.tsb_GenEDI270.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleCoverageBenefitToolStripMenuItem,
            this.multipleCoverageBenefitToolStripMenuItem,
            this.patientDeterminedAtPayerToolStripMenuItem,
            this.EDI997atRxHubToolStripMenuItem,
            this.tA1MessageToolStripMenuItem,
            this.patientNotFoundToolStripMenuItem,
            this.nAKErrorAtRxHubToolStripMenuItem});
            this.tsb_GenEDI270.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_GenEDI270.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_GenEDI270.Image = ((System.Drawing.Image)(resources.GetObject("tsb_GenEDI270.Image")));
            this.tsb_GenEDI270.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_GenEDI270.Name = "tsb_GenEDI270";
            this.tsb_GenEDI270.Size = new System.Drawing.Size(69, 50);
            this.tsb_GenEDI270.Tag = "EDI 270";
            this.tsb_GenEDI270.Text = "&EDI 270";
            this.tsb_GenEDI270.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_GenEDI270.ToolTipText = " Generate EDI 270";
            // 
            // singleCoverageBenefitToolStripMenuItem
            // 
            this.singleCoverageBenefitToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleCoverageBenefitToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.singleCoverageBenefitToolStripMenuItem.Name = "singleCoverageBenefitToolStripMenuItem";
            this.singleCoverageBenefitToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.singleCoverageBenefitToolStripMenuItem.Text = "Single Coverage Benefit";
            this.singleCoverageBenefitToolStripMenuItem.Click += new System.EventHandler(this.singleCoverageBenefitToolStripMenuItem_Click);
            // 
            // multipleCoverageBenefitToolStripMenuItem
            // 
            this.multipleCoverageBenefitToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multipleCoverageBenefitToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.multipleCoverageBenefitToolStripMenuItem.Name = "multipleCoverageBenefitToolStripMenuItem";
            this.multipleCoverageBenefitToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.multipleCoverageBenefitToolStripMenuItem.Text = "Multiple Coverage Benefit";
            this.multipleCoverageBenefitToolStripMenuItem.Click += new System.EventHandler(this.multipleCoverageBenefitToolStripMenuItem_Click);
            // 
            // patientDeterminedAtPayerToolStripMenuItem
            // 
            this.patientDeterminedAtPayerToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patientDeterminedAtPayerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.patientDeterminedAtPayerToolStripMenuItem.Name = "patientDeterminedAtPayerToolStripMenuItem";
            this.patientDeterminedAtPayerToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.patientDeterminedAtPayerToolStripMenuItem.Text = "Patient Determined At Payer";
            this.patientDeterminedAtPayerToolStripMenuItem.Click += new System.EventHandler(this.patientDeterminedAtPayerToolStripMenuItem_Click);
            // 
            // EDI997atRxHubToolStripMenuItem
            // 
            this.EDI997atRxHubToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EDI997atRxHubToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.EDI997atRxHubToolStripMenuItem.Name = "EDI997atRxHubToolStripMenuItem";
            this.EDI997atRxHubToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.EDI997atRxHubToolStripMenuItem.Text = "997 at RxHub";
            this.EDI997atRxHubToolStripMenuItem.Click += new System.EventHandler(this.EDI997atRxHubToolStripMenuItem_Click);
            // 
            // tA1MessageToolStripMenuItem
            // 
            this.tA1MessageToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tA1MessageToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tA1MessageToolStripMenuItem.Name = "tA1MessageToolStripMenuItem";
            this.tA1MessageToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.tA1MessageToolStripMenuItem.Text = "TA1 Message";
            this.tA1MessageToolStripMenuItem.Click += new System.EventHandler(this.tA1MessageToolStripMenuItem_Click);
            // 
            // patientNotFoundToolStripMenuItem
            // 
            this.patientNotFoundToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patientNotFoundToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.patientNotFoundToolStripMenuItem.Name = "patientNotFoundToolStripMenuItem";
            this.patientNotFoundToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.patientNotFoundToolStripMenuItem.Text = "Patient Not Found";
            this.patientNotFoundToolStripMenuItem.Click += new System.EventHandler(this.patientNotFoundToolStripMenuItem_Click);
            // 
            // nAKErrorAtRxHubToolStripMenuItem
            // 
            this.nAKErrorAtRxHubToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nAKErrorAtRxHubToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.nAKErrorAtRxHubToolStripMenuItem.Name = "nAKErrorAtRxHubToolStripMenuItem";
            this.nAKErrorAtRxHubToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.nAKErrorAtRxHubToolStripMenuItem.Text = "NAK Error at RxHub";
            this.nAKErrorAtRxHubToolStripMenuItem.Click += new System.EventHandler(this.nAKErrorAtRxHubToolStripMenuItem_Click);
            // 
            // tsb_POSTEDI270
            // 
            this.tsb_POSTEDI270.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_POSTEDI270.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_POSTEDI270.Image = ((System.Drawing.Image)(resources.GetObject("tsb_POSTEDI270.Image")));
            this.tsb_POSTEDI270.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_POSTEDI270.Name = "tsb_POSTEDI270";
            this.tsb_POSTEDI270.Size = new System.Drawing.Size(92, 50);
            this.tsb_POSTEDI270.Tag = "Post EDI 270";
            this.tsb_POSTEDI270.Text = "&Post EDI 270";
            this.tsb_POSTEDI270.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_POSTEDI270.ToolTipText = "Post EDI 270 ";
            this.tsb_POSTEDI270.Click += new System.EventHandler(this.tsb_POSTEDI270_Click);
            // 
            // tsb_EDI270
            // 
            this.tsb_EDI270.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_EDI270.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_EDI270.Image = ((System.Drawing.Image)(resources.GetObject("tsb_EDI270.Image")));
            this.tsb_EDI270.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_EDI270.Name = "tsb_EDI270";
            this.tsb_EDI270.Size = new System.Drawing.Size(60, 50);
            this.tsb_EDI270.Tag = "EDI 270";
            this.tsb_EDI270.Text = "&EDI 270";
            this.tsb_EDI270.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_EDI270.ToolTipText = "EDI 270 Generation";
            this.tsb_EDI270.Visible = false;
            this.tsb_EDI270.Click += new System.EventHandler(this.tsb_EDI270_Click);
            // 
            // tsb_SingleCovrgPhBenefit
            // 
            this.tsb_SingleCovrgPhBenefit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SingleCovrgPhBenefit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_SingleCovrgPhBenefit.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SingleCovrgPhBenefit.Image")));
            this.tsb_SingleCovrgPhBenefit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SingleCovrgPhBenefit.Name = "tsb_SingleCovrgPhBenefit";
            this.tsb_SingleCovrgPhBenefit.Size = new System.Drawing.Size(157, 50);
            this.tsb_SingleCovrgPhBenefit.Tag = "Single Coverage Benefit";
            this.tsb_SingleCovrgPhBenefit.Text = "&Single Coverage Benefit";
            this.tsb_SingleCovrgPhBenefit.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_SingleCovrgPhBenefit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SingleCovrgPhBenefit.Visible = false;
            this.tsb_SingleCovrgPhBenefit.Click += new System.EventHandler(this.tsb_SingleCovrgPhBenefit_Click);
            // 
            // tsb_MultipleCovrgPhBenefit
            // 
            this.tsb_MultipleCovrgPhBenefit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_MultipleCovrgPhBenefit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_MultipleCovrgPhBenefit.Image = ((System.Drawing.Image)(resources.GetObject("tsb_MultipleCovrgPhBenefit.Image")));
            this.tsb_MultipleCovrgPhBenefit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_MultipleCovrgPhBenefit.Name = "tsb_MultipleCovrgPhBenefit";
            this.tsb_MultipleCovrgPhBenefit.Size = new System.Drawing.Size(151, 50);
            this.tsb_MultipleCovrgPhBenefit.Tag = "Multi Coverage Benefit";
            this.tsb_MultipleCovrgPhBenefit.Text = "&Multi Coverage Benefit";
            this.tsb_MultipleCovrgPhBenefit.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_MultipleCovrgPhBenefit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_MultipleCovrgPhBenefit.Visible = false;
            this.tsb_MultipleCovrgPhBenefit.Click += new System.EventHandler(this.tsb_MultipleCovrgPhBenefit_Click);
            // 
            // tsb_PatDeterminedatPayer
            // 
            this.tsb_PatDeterminedatPayer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PatDeterminedatPayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PatDeterminedatPayer.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatDeterminedatPayer.Image")));
            this.tsb_PatDeterminedatPayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatDeterminedatPayer.Name = "tsb_PatDeterminedatPayer";
            this.tsb_PatDeterminedatPayer.Size = new System.Drawing.Size(146, 50);
            this.tsb_PatDeterminedatPayer.Tag = "Pat-Determined Payer";
            this.tsb_PatDeterminedatPayer.Text = "&Pat-Determined Payer";
            this.tsb_PatDeterminedatPayer.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_PatDeterminedatPayer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PatDeterminedatPayer.Visible = false;
            this.tsb_PatDeterminedatPayer.Click += new System.EventHandler(this.tsb_PatDeterminedatPayer_Click);
            // 
            // tsb_997atRxHub
            // 
            this.tsb_997atRxHub.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_997atRxHub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_997atRxHub.Image = ((System.Drawing.Image)(resources.GetObject("tsb_997atRxHub.Image")));
            this.tsb_997atRxHub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_997atRxHub.Name = "tsb_997atRxHub";
            this.tsb_997atRxHub.Size = new System.Drawing.Size(97, 50);
            this.tsb_997atRxHub.Tag = "997 at RxHub";
            this.tsb_997atRxHub.Text = "997 at RxHub";
            this.tsb_997atRxHub.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_997atRxHub.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_997atRxHub.Visible = false;
            this.tsb_997atRxHub.Click += new System.EventHandler(this.tsb_997atRxHub_Click);
            // 
            // tsb_TA1Msg_RxHub
            // 
            this.tsb_TA1Msg_RxHub.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_TA1Msg_RxHub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_TA1Msg_RxHub.Image = ((System.Drawing.Image)(resources.GetObject("tsb_TA1Msg_RxHub.Image")));
            this.tsb_TA1Msg_RxHub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_TA1Msg_RxHub.Name = "tsb_TA1Msg_RxHub";
            this.tsb_TA1Msg_RxHub.Size = new System.Drawing.Size(92, 50);
            this.tsb_TA1Msg_RxHub.Tag = "TA1 Mesaage";
            this.tsb_TA1Msg_RxHub.Text = "TA1 Mesaage";
            this.tsb_TA1Msg_RxHub.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_TA1Msg_RxHub.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_TA1Msg_RxHub.Visible = false;
            this.tsb_TA1Msg_RxHub.Click += new System.EventHandler(this.tsb_TA1Msg_RxHub_Click);
            // 
            // tsb_PatientNotFound_RxHub
            // 
            this.tsb_PatientNotFound_RxHub.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PatientNotFound_RxHub.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatientNotFound_RxHub.Image")));
            this.tsb_PatientNotFound_RxHub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatientNotFound_RxHub.Name = "tsb_PatientNotFound_RxHub";
            this.tsb_PatientNotFound_RxHub.Size = new System.Drawing.Size(124, 50);
            this.tsb_PatientNotFound_RxHub.Tag = "Patient Not Found";
            this.tsb_PatientNotFound_RxHub.Text = "Patient Not Found";
            this.tsb_PatientNotFound_RxHub.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_PatientNotFound_RxHub.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PatientNotFound_RxHub.Visible = false;
            this.tsb_PatientNotFound_RxHub.Click += new System.EventHandler(this.tsb_PatientNotFound_RxHub_Click);
            // 
            // tsb_NAKErr_RxHub
            // 
            this.tsb_NAKErr_RxHub.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_NAKErr_RxHub.Image = ((System.Drawing.Image)(resources.GetObject("tsb_NAKErr_RxHub.Image")));
            this.tsb_NAKErr_RxHub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_NAKErr_RxHub.Name = "tsb_NAKErr_RxHub";
            this.tsb_NAKErr_RxHub.Size = new System.Drawing.Size(70, 50);
            this.tsb_NAKErr_RxHub.Tag = "NAK Error";
            this.tsb_NAKErr_RxHub.Text = "NAK Error";
            this.tsb_NAKErr_RxHub.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_NAKErr_RxHub.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_NAKErr_RxHub.Visible = false;
            this.tsb_NAKErr_RxHub.Click += new System.EventHandler(this.tsb_NAKErr_RxHub_Click);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.txtEDIOutput);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 56);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(961, 448);
            this.pnlMain.TabIndex = 6;
            // 
            // txtEDIOutput
            // 
            this.txtEDIOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEDIOutput.Location = new System.Drawing.Point(3, 3);
            this.txtEDIOutput.Multiline = true;
            this.txtEDIOutput.Name = "txtEDIOutput";
            this.txtEDIOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtEDIOutput.Size = new System.Drawing.Size(955, 442);
            this.txtEDIOutput.TabIndex = 0;
            // 
            // frmRxhub270EDIGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(961, 504);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRxhub270EDIGeneration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EDI 270 Generation ";
            this.Load += new System.EventHandler(this.frmRxhub270EDIGeneration_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        public  System.Windows.Forms.ToolStrip ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_EDI270;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtEDIOutput;
        internal System.Windows.Forms.ToolStripButton tsb_SingleCovrgPhBenefit;
        internal System.Windows.Forms.ToolStripButton tsb_PatDeterminedatPayer;
        internal System.Windows.Forms.ToolStripButton tsb_MultipleCovrgPhBenefit;
        internal System.Windows.Forms.ToolStripButton tsb_997atRxHub;
        internal System.Windows.Forms.ToolStripButton tsb_TA1Msg_RxHub;
        internal System.Windows.Forms.ToolStripButton tsb_PatientNotFound_RxHub;
        internal System.Windows.Forms.ToolStripButton tsb_NAKErr_RxHub;
        internal System.Windows.Forms.ToolStripDropDownButton tsb_GenEDI270;
        private System.Windows.Forms.ToolStripMenuItem singleCoverageBenefitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multipleCoverageBenefitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientDeterminedAtPayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EDI997atRxHubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tA1MessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientNotFoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nAKErrorAtRxHubToolStripMenuItem;
        public System.Windows.Forms.ToolStripButton tsb_POSTEDI270;
    }


}