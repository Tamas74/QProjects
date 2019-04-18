namespace gloEmdeonInterface.Forms
{
    partial class frmMergeOrder
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
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                components.Dispose();
                try
                {
                    if (gloUC_PatientStrip1 != null)
                    {
                        gloUC_PatientStrip1.Dispose();
                        gloUC_PatientStrip1 = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (gloUC_Source != null)
                    {
                        gloUC_Source.Dispose();
                        gloUC_Source = null;
                    }
                }
                catch
                {
                }
                 try
                {
                    if (gloUC_Destination != null)
                    {
                        gloUC_Destination.Dispose();
                        gloUC_Destination = null;
                    }
                }
                catch
                {
                }
                  
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
            gloUserControlLibrary.AgeDetail ageDetail1 = new gloUserControlLibrary.AgeDetail();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMergeOrder));
            this.gloUC_Source = new gloUserControlLibrary.gloUC_TransactionHistory();
            this.gloUC_PatientStrip1 = new gloUserControlLibrary.gloUC_PatientStrip();
            this.pnlUC_Source = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblDestinationOrderNo = new System.Windows.Forms.Label();
            this.lbldes = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.pnlUC_Destination = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.gloUC_Destination = new gloUserControlLibrary.gloUC_TransactionHistory();
            this.miniToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnPreviewMerge = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlsp_MSTCPT = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnDoNotMerge = new System.Windows.Forms.ToolStripButton();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.c1Source = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label55 = new System.Windows.Forms.Label();
            this.c1Destination = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblTags = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.pnlC1Order = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblSourceOrderNo = new System.Windows.Forms.Label();
            this.lblSor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlUC_Source.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlUC_Destination.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlsp_MSTCPT.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Source)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Destination)).BeginInit();
            this.panel6.SuspendLayout();
            this.pnlC1Order.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // gloUC_Source
            // 
            this.gloUC_Source.AutoScroll = true;
            this.gloUC_Source.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUC_Source.CurOrderID = ((long)(0));
            this.gloUC_Source.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloUC_Source.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUC_Source.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUC_Source.ForMerging = false;
            this.gloUC_Source.HideCloseButton = false;
            this.gloUC_Source.Location = new System.Drawing.Point(1, 0);
            this.gloUC_Source.MergeOrderID = ((long)(0));
            this.gloUC_Source.Name = "gloUC_Source";
            this.gloUC_Source.Size = new System.Drawing.Size(638, 265);
            this.gloUC_Source.TabIndex = 1;
            // 
            // gloUC_PatientStrip1
            // 
            this.gloUC_PatientStrip1.AgeLimit = 0;
            this.gloUC_PatientStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUC_PatientStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gloUC_PatientStrip1.btnDownEnable = true;
            this.gloUC_PatientStrip1.btnUpEnable = true;
            this.gloUC_PatientStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gloUC_PatientStrip1.DTPEnabled = true;
            this.gloUC_PatientStrip1.DTPFormat = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.gloUC_PatientStrip1.DTPValue = new System.DateTime(2014, 2, 12, 9, 48, 3, 918);
            this.gloUC_PatientStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUC_PatientStrip1.ForeColor = System.Drawing.Color.Black;
            this.gloUC_PatientStrip1.GlobalPeriod = null;
            this.gloUC_PatientStrip1.HideButton = false;
            this.gloUC_PatientStrip1.HideGender = true;
            this.gloUC_PatientStrip1.HideHandDominance = true;
            this.gloUC_PatientStrip1.HidePatientCellPhone = true;
            this.gloUC_PatientStrip1.HidePatientOccupation = true;
            this.gloUC_PatientStrip1.HidePatientPhone = true;
            this.gloUC_PatientStrip1.HidePharmacyFax = true;
            this.gloUC_PatientStrip1.HidePharmacyName = true;
            this.gloUC_PatientStrip1.HidePharmacyPhone = true;
            this.gloUC_PatientStrip1.HidePrimaryInsurance = true;
            this.gloUC_PatientStrip1.HideReferral = true;
            this.gloUC_PatientStrip1.HideSecondaryInsurance = true;
            this.gloUC_PatientStrip1.HideSSN = true;
            this.gloUC_PatientStrip1.IntuitCommunication = false;
            this.gloUC_PatientStrip1.IsPediatric = false;
            this.gloUC_PatientStrip1.Location = new System.Drawing.Point(0, 54);
            this.gloUC_PatientStrip1.MinimizeStrip = false;
            //this.gloUC_PatientStrip1.MyPictureBoxControl = null;
            this.gloUC_PatientStrip1.Name = "gloUC_PatientStrip1";
            this.gloUC_PatientStrip1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.gloUC_PatientStrip1.PatientAge = ageDetail1;
            this.gloUC_PatientStrip1.PatientCellPhone = null;
            this.gloUC_PatientStrip1.PatientCode = null;
            this.gloUC_PatientStrip1.PatientDateOfBirth = new System.DateTime(((long)(0)));
            this.gloUC_PatientStrip1.PatientGender = null;
            this.gloUC_PatientStrip1.PatientHandDominance = null;
            this.gloUC_PatientStrip1.PatientName = null;
            this.gloUC_PatientStrip1.PatientOccupation = null;
            this.gloUC_PatientStrip1.PatientPhone = null;
            //this.gloUC_PatientStrip1.PatientPhoto = null;
            this.gloUC_PatientStrip1.PCP = null;
            this.gloUC_PatientStrip1.PharmacyFax = null;
            this.gloUC_PatientStrip1.PharmacyName = null;
            this.gloUC_PatientStrip1.PharmacyPhone = null;
            this.gloUC_PatientStrip1.PrimaryInsurance = "";
            this.gloUC_PatientStrip1.Provider = null;
            this.gloUC_PatientStrip1.ProviderID = ((long)(0));
            this.gloUC_PatientStrip1.Referral = "";
            this.gloUC_PatientStrip1.SecondaryInsurance = "";
            this.gloUC_PatientStrip1.ShowAgeInDays = false;
            this.gloUC_PatientStrip1.Size = new System.Drawing.Size(1264, 96);
            this.gloUC_PatientStrip1.SSN = null;
            this.gloUC_PatientStrip1.TabIndex = 2;
            this.gloUC_PatientStrip1.TransactionDate = new System.DateTime(2014, 2, 12, 9, 48, 3, 918);
            this.gloUC_PatientStrip1.Load += new System.EventHandler(this.gloUC_PatientStrip1_Load);
            // 
            // pnlUC_Source
            // 
            this.pnlUC_Source.Controls.Add(this.gloUC_Source);
            this.pnlUC_Source.Controls.Add(this.label1);
            this.pnlUC_Source.Controls.Add(this.label6);
            this.pnlUC_Source.Controls.Add(this.label7);
            this.pnlUC_Source.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUC_Source.Location = new System.Drawing.Point(0, 50);
            this.pnlUC_Source.Name = "pnlUC_Source";
            this.pnlUC_Source.Size = new System.Drawing.Size(640, 266);
            this.pnlUC_Source.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(639, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 265);
            this.label1.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 265);
            this.label6.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(0, 265);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(640, 1);
            this.label7.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.lblDestinationOrderNo);
            this.panel4.Controls.Add(this.lbldes);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.label21);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(615, 25);
            this.panel4.TabIndex = 8;
            // 
            // lblDestinationOrderNo
            // 
            this.lblDestinationOrderNo.AutoSize = true;
            this.lblDestinationOrderNo.BackColor = System.Drawing.Color.Transparent;
            this.lblDestinationOrderNo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDestinationOrderNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestinationOrderNo.ForeColor = System.Drawing.Color.Black;
            this.lblDestinationOrderNo.Location = new System.Drawing.Point(116, 1);
            this.lblDestinationOrderNo.Name = "lblDestinationOrderNo";
            this.lblDestinationOrderNo.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblDestinationOrderNo.Size = new System.Drawing.Size(0, 19);
            this.lblDestinationOrderNo.TabIndex = 10;
            this.lblDestinationOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbldes
            // 
            this.lbldes.AutoSize = true;
            this.lbldes.BackColor = System.Drawing.Color.Transparent;
            this.lbldes.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbldes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldes.ForeColor = System.Drawing.Color.White;
            this.lbldes.Location = new System.Drawing.Point(1, 1);
            this.lbldes.Name = "lbldes";
            this.lbldes.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lbldes.Size = new System.Drawing.Size(115, 19);
            this.lbldes.TabIndex = 9;
            this.lbldes.Text = "  Tests for Order :";
            this.lbldes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Location = new System.Drawing.Point(614, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 24);
            this.label18.TabIndex = 7;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(0, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 24);
            this.label19.TabIndex = 6;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(615, 1);
            this.label21.TabIndex = 4;
            // 
            // pnlUC_Destination
            // 
            this.pnlUC_Destination.Controls.Add(this.label22);
            this.pnlUC_Destination.Controls.Add(this.label23);
            this.pnlUC_Destination.Controls.Add(this.label24);
            this.pnlUC_Destination.Controls.Add(this.gloUC_Destination);
            this.pnlUC_Destination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUC_Destination.Location = new System.Drawing.Point(3, 50);
            this.pnlUC_Destination.Name = "pnlUC_Destination";
            this.pnlUC_Destination.Size = new System.Drawing.Size(615, 266);
            this.pnlUC_Destination.TabIndex = 26;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Location = new System.Drawing.Point(614, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 265);
            this.label22.TabIndex = 7;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 265);
            this.label23.TabIndex = 6;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(0, 265);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(615, 1);
            this.label24.TabIndex = 5;
            // 
            // gloUC_Destination
            // 
            this.gloUC_Destination.AutoScroll = true;
            this.gloUC_Destination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUC_Destination.CurOrderID = ((long)(0));
            this.gloUC_Destination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloUC_Destination.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUC_Destination.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUC_Destination.ForMerging = false;
            this.gloUC_Destination.HideCloseButton = false;
            this.gloUC_Destination.Location = new System.Drawing.Point(0, 0);
            this.gloUC_Destination.MergeOrderID = ((long)(0));
            this.gloUC_Destination.Name = "gloUC_Destination";
            this.gloUC_Destination.Size = new System.Drawing.Size(615, 266);
            this.gloUC_Destination.TabIndex = 1;
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.miniToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("miniToolStrip.BackgroundImage")));
            this.miniToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.miniToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.miniToolStrip.Location = new System.Drawing.Point(188, 0);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(1008, 53);
            this.miniToolStrip.TabIndex = 2;
            // 
            // ts_btnSave
            // 
            this.ts_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSave.Image")));
            this.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSave.Name = "ts_btnSave";
            this.ts_btnSave.Size = new System.Drawing.Size(49, 50);
            this.ts_btnSave.Tag = "Merge";
            this.ts_btnSave.Text = "Merge";
            this.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSave.ToolTipText = "Merge Order";
            this.ts_btnSave.Visible = false;
            this.ts_btnSave.Click += new System.EventHandler(this.ts_btnSave_Click);
            // 
            // ts_btnPreviewMerge
            // 
            this.ts_btnPreviewMerge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnPreviewMerge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnPreviewMerge.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnPreviewMerge.Image")));
            this.ts_btnPreviewMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnPreviewMerge.Name = "ts_btnPreviewMerge";
            this.ts_btnPreviewMerge.Size = new System.Drawing.Size(101, 50);
            this.ts_btnPreviewMerge.Tag = "Preview Merge";
            this.ts_btnPreviewMerge.Text = "Preview Merge";
            this.ts_btnPreviewMerge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnPreviewMerge.ToolTipText = "Preview Merge";
            this.ts_btnPreviewMerge.Click += new System.EventHandler(this.ts_btnPreviewMerge_Click);
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tlsp_MSTCPT);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 54);
            this.panel1.TabIndex = 4;
            // 
            // tlsp_MSTCPT
            // 
            this.tlsp_MSTCPT.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_MSTCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsp_MSTCPT.BackgroundImage")));
            this.tlsp_MSTCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_MSTCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_MSTCPT.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_MSTCPT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnSave,
            this.ts_btnDoNotMerge,
            this.ts_btnPreviewMerge,
            this.ts_btnClose});
            this.tlsp_MSTCPT.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_MSTCPT.Location = new System.Drawing.Point(0, 0);
            this.tlsp_MSTCPT.Name = "tlsp_MSTCPT";
            this.tlsp_MSTCPT.Size = new System.Drawing.Size(1264, 53);
            this.tlsp_MSTCPT.TabIndex = 2;
            this.tlsp_MSTCPT.Text = "toolStrip1";
            // 
            // ts_btnDoNotMerge
            // 
            this.ts_btnDoNotMerge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnDoNotMerge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnDoNotMerge.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnDoNotMerge.Image")));
            this.ts_btnDoNotMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnDoNotMerge.Name = "ts_btnDoNotMerge";
            this.ts_btnDoNotMerge.Size = new System.Drawing.Size(96, 50);
            this.ts_btnDoNotMerge.Tag = "Do Not Merge";
            this.ts_btnDoNotMerge.Text = "&Do Not Merge";
            this.ts_btnDoNotMerge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnDoNotMerge.ToolTipText = "Do Not Merge";
            this.ts_btnDoNotMerge.Visible = false;
            this.ts_btnDoNotMerge.Click += new System.EventHandler(this.ts_btnDoNotMerge_Click);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel12.Controls.Add(this.label48);
            this.panel12.Controls.Add(this.label49);
            this.panel12.Controls.Add(this.label50);
            this.panel12.Controls.Add(this.label51);
            this.panel12.Controls.Add(this.label52);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(3, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(640, 25);
            this.panel12.TabIndex = 0;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.Transparent;
            this.label48.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.Black;
            this.label48.Location = new System.Drawing.Point(1, 1);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(638, 23);
            this.label48.TabIndex = 2;
            this.label48.Text = "  Step 2 : Select Order to Merge";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label49.Location = new System.Drawing.Point(1, 24);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(638, 1);
            this.label49.TabIndex = 12;
            this.label49.Text = "label2";
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Left;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(0, 1);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 24);
            this.label50.TabIndex = 11;
            this.label50.Text = "label4";
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Right;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label51.Location = new System.Drawing.Point(639, 1);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(1, 24);
            this.label51.TabIndex = 10;
            this.label51.Text = "label3";
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Top;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(0, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(640, 1);
            this.label52.TabIndex = 9;
            this.label52.Text = "label1";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.label53);
            this.panel13.Controls.Add(this.label54);
            this.panel13.Controls.Add(this.c1Source);
            this.panel13.Controls.Add(this.label55);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 25);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(640, 382);
            this.panel13.TabIndex = 18;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Right;
            this.label53.Location = new System.Drawing.Point(639, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 381);
            this.label53.TabIndex = 7;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Left;
            this.label54.Location = new System.Drawing.Point(0, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(1, 381);
            this.label54.TabIndex = 6;
            // 
            // c1Source
            // 
            this.c1Source.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Source.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.c1Source.ColumnInfo = "10,1,0,0,0,100,Columns:";
            this.c1Source.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Source.ExtendLastCol = true;
            this.c1Source.Location = new System.Drawing.Point(0, 0);
            this.c1Source.Name = "c1Source";
            this.c1Source.Rows.DefaultSize = 20;
            this.c1Source.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1Source.Size = new System.Drawing.Size(640, 381);
            this.c1Source.StyleInfo = resources.GetString("c1Source.StyleInfo");
            this.c1Source.TabIndex = 3;
            this.c1Source.EnterCell += new System.EventHandler(this.c1Source_EnterCell);
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label55.Location = new System.Drawing.Point(0, 381);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(640, 1);
            this.label55.TabIndex = 5;
            // 
            // c1Destination
            // 
            this.c1Destination.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Destination.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.c1Destination.ColumnInfo = "10,1,0,0,0,100,Columns:";
            this.c1Destination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Destination.ExtendLastCol = true;
            this.c1Destination.Location = new System.Drawing.Point(0, 1);
            this.c1Destination.Name = "c1Destination";
            this.c1Destination.Rows.DefaultSize = 20;
            this.c1Destination.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1Destination.Size = new System.Drawing.Size(615, 381);
            this.c1Destination.StyleInfo = resources.GetString("c1Destination.StyleInfo");
            this.c1Destination.TabIndex = 3;
            this.c1Destination.EnterCell += new System.EventHandler(this.c1Destination_EnterCell);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.lblTags);
            this.panel6.Controls.Add(this.label35);
            this.panel6.Controls.Add(this.label36);
            this.panel6.Controls.Add(this.label38);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(615, 25);
            this.panel6.TabIndex = 0;
            // 
            // lblTags
            // 
            this.lblTags.BackColor = System.Drawing.Color.Transparent;
            this.lblTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTags.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTags.ForeColor = System.Drawing.Color.Black;
            this.lblTags.Location = new System.Drawing.Point(1, 1);
            this.lblTags.Name = "lblTags";
            this.lblTags.Size = new System.Drawing.Size(613, 24);
            this.lblTags.TabIndex = 2;
            this.lblTags.Text = "  Step 1 : Select Order to Keep";
            this.lblTags.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Left;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(0, 1);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 24);
            this.label35.TabIndex = 11;
            this.label35.Text = "label4";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Right;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label36.Location = new System.Drawing.Point(614, 1);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 24);
            this.label36.TabIndex = 10;
            this.label36.Text = "label3";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Top;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(615, 1);
            this.label38.TabIndex = 9;
            this.label38.Text = "label1";
            // 
            // pnlC1Order
            // 
            this.pnlC1Order.Controls.Add(this.label9);
            this.pnlC1Order.Controls.Add(this.label10);
            this.pnlC1Order.Controls.Add(this.label11);
            this.pnlC1Order.Controls.Add(this.c1Destination);
            this.pnlC1Order.Controls.Add(this.label12);
            this.pnlC1Order.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlC1Order.Location = new System.Drawing.Point(3, 25);
            this.pnlC1Order.Name = "pnlC1Order";
            this.pnlC1Order.Size = new System.Drawing.Size(615, 382);
            this.pnlC1Order.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Location = new System.Drawing.Point(614, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 380);
            this.label9.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 380);
            this.label10.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(0, 381);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(615, 1);
            this.label11.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(615, 1);
            this.label12.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlC1Order);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel2.Size = new System.Drawing.Size(618, 407);
            this.panel2.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel13);
            this.panel5.Controls.Add(this.panel12);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(618, 3);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel5.Size = new System.Drawing.Size(646, 407);
            this.panel5.TabIndex = 8;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.pnlUC_Source);
            this.panel7.Controls.Add(this.panel3);
            this.panel7.Controls.Add(this.panel11);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(621, 0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel7.Size = new System.Drawing.Size(643, 319);
            this.panel7.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.lblSourceOrderNo);
            this.panel3.Controls.Add(this.lblSor);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(640, 25);
            this.panel3.TabIndex = 8;
            // 
            // lblSourceOrderNo
            // 
            this.lblSourceOrderNo.AutoSize = true;
            this.lblSourceOrderNo.BackColor = System.Drawing.Color.Transparent;
            this.lblSourceOrderNo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSourceOrderNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSourceOrderNo.ForeColor = System.Drawing.Color.Black;
            this.lblSourceOrderNo.Location = new System.Drawing.Point(116, 1);
            this.lblSourceOrderNo.Name = "lblSourceOrderNo";
            this.lblSourceOrderNo.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblSourceOrderNo.Size = new System.Drawing.Size(0, 19);
            this.lblSourceOrderNo.TabIndex = 10;
            this.lblSourceOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSor
            // 
            this.lblSor.AutoSize = true;
            this.lblSor.BackColor = System.Drawing.Color.Transparent;
            this.lblSor.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSor.ForeColor = System.Drawing.Color.White;
            this.lblSor.Location = new System.Drawing.Point(1, 1);
            this.lblSor.Name = "lblSor";
            this.lblSor.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblSor.Size = new System.Drawing.Size(115, 19);
            this.lblSor.TabIndex = 8;
            this.lblSor.Text = "  Tests for Order :";
            this.lblSor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(639, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 24);
            this.label2.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(639, 1);
            this.label5.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 25);
            this.label3.TabIndex = 6;
            // 
            // panel11
            // 
            this.panel11.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.Controls.Add(this.label17);
            this.panel11.Controls.Add(this.label13);
            this.panel11.Controls.Add(this.label14);
            this.panel11.Controls.Add(this.label15);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(640, 25);
            this.panel11.TabIndex = 9;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(1, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(638, 24);
            this.label17.TabIndex = 8;
            this.label17.Text = "  Step 3 : Match Tests";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(639, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 24);
            this.label13.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Location = new System.Drawing.Point(1, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(639, 1);
            this.label14.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 25);
            this.label15.TabIndex = 6;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.pnlUC_Destination);
            this.panel8.Controls.Add(this.panel4);
            this.panel8.Controls.Add(this.panel14);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel8.Size = new System.Drawing.Size(621, 319);
            this.panel8.TabIndex = 8;
            // 
            // panel14
            // 
            this.panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel14.Controls.Add(this.label4);
            this.panel14.Controls.Add(this.label8);
            this.panel14.Controls.Add(this.label16);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Location = new System.Drawing.Point(3, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(615, 25);
            this.panel14.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(614, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 24);
            this.label4.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(614, 1);
            this.label8.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 25);
            this.label16.TabIndex = 6;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.panel2);
            this.panel9.Controls.Add(this.panel5);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 150);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel9.Size = new System.Drawing.Size(1264, 410);
            this.panel9.TabIndex = 9;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.panel8);
            this.panel10.Controls.Add(this.panel7);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 563);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1264, 319);
            this.panel10.TabIndex = 9;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 560);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1264, 3);
            this.splitter1.TabIndex = 10;
            this.splitter1.TabStop = false;
            // 
            // frmMergeOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1264, 882);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.gloUC_PatientStrip1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMergeOrder";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Merge Order";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMergeOrder_FormClosing);
            this.Load += new System.EventHandler(this.frmMergeOrder_Load);
            this.pnlUC_Source.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlUC_Destination.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tlsp_MSTCPT.ResumeLayout(false);
            this.tlsp_MSTCPT.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Source)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Destination)).EndInit();
            this.panel6.ResumeLayout(false);
            this.pnlC1Order.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private gloUserControlLibrary.gloUC_TransactionHistory gloUC_Source;
        private gloUserControlLibrary.gloUC_PatientStrip gloUC_PatientStrip1;
        private System.Windows.Forms.Panel pnlUC_Source;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel pnlUC_Destination;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private gloUserControlLibrary.gloUC_TransactionHistory gloUC_Destination;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbldes;
        private System.Windows.Forms.Label lblDestinationOrderNo;
        private gloGlobal.gloToolStripIgnoreFocus miniToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        private System.Windows.Forms.ToolStripButton ts_btnPreviewMerge;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.Panel panel1;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_MSTCPT;
        private System.Windows.Forms.ToolStripButton ts_btnDoNotMerge;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Destination;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblTags;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel pnlC1Order;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Source;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblSourceOrderNo;
        private System.Windows.Forms.Label lblSor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
    }
}
