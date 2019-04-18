namespace gloEmdeonInterface.Forms
{
    partial class frmSpirometryTestsNew
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSpirometryTestsNew));
            this.ts_LabMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtnNew = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnclose = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnRemoveOrderBy = new System.Windows.Forms.Button();
            this.btnOrederBy = new System.Windows.Forms.Button();
            this.btnConfigureRace = new System.Windows.Forms.Button();
            this.btnGenrateVisit = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtForYears = new System.Windows.Forms.TextBox();
            this.txtCigsDay = new System.Windows.Forms.TextBox();
            this.txtQuitYearAgo = new System.Windows.Forms.TextBox();
            this.optSmoker = new System.Windows.Forms.RadioButton();
            this.optNonSmoker = new System.Windows.Forms.RadioButton();
            this.ChkQuit = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.OptRefferal = new System.Windows.Forms.RadioButton();
            this.LblRaceErrorMsg = new System.Windows.Forms.Label();
            this.optProvider = new System.Windows.Forms.RadioButton();
            this.cmbPARace = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOrederd_by = new System.Windows.Forms.TextBox();
            this.LblTechnician = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbPatientRace = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblVitalinformation = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPAWeight = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtPAHeight = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ts_LabMain.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_LabMain
            // 
            this.ts_LabMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_LabMain.BackgroundImage")));
            this.ts_LabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_LabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ts_LabMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_LabMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_LabMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtnNew,
            this.tlbbtnclose});
            this.ts_LabMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_LabMain.Location = new System.Drawing.Point(0, 0);
            this.ts_LabMain.Name = "ts_LabMain";
            this.ts_LabMain.Size = new System.Drawing.Size(515, 54);
            this.ts_LabMain.TabIndex = 18;
            this.ts_LabMain.Text = "toolStrip1";
            this.ts_LabMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_LabMain_ItemClicked);
            // 
            // tlbbtnNew
            // 
            this.tlbbtnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnNew.Image")));
            this.tlbbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnNew.Name = "tlbbtnNew";
            this.tlbbtnNew.Size = new System.Drawing.Size(85, 50);
            this.tlbbtnNew.Tag = "LaunchTest";
            this.tlbbtnNew.Text = "&Launch Test";
            this.tlbbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnNew.ToolTipText = "Launch New Test";
            // 
            // tlbbtnclose
            // 
            this.tlbbtnclose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnclose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnclose.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnclose.Image")));
            this.tlbbtnclose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnclose.Name = "tlbbtnclose";
            this.tlbbtnclose.Size = new System.Drawing.Size(43, 50);
            this.tlbbtnclose.Tag = "Close";
            this.tlbbtnclose.Text = "&Close";
            this.tlbbtnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnclose.ToolTipText = "Close";
            // 
            // BtnRemoveOrderBy
            // 
            this.BtnRemoveOrderBy.AutoEllipsis = true;
            this.BtnRemoveOrderBy.BackColor = System.Drawing.Color.Transparent;
            this.BtnRemoveOrderBy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnRemoveOrderBy.BackgroundImage")));
            this.BtnRemoveOrderBy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnRemoveOrderBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRemoveOrderBy.Image = ((System.Drawing.Image)(resources.GetObject("BtnRemoveOrderBy.Image")));
            this.BtnRemoveOrderBy.Location = new System.Drawing.Point(420, 61);
            this.BtnRemoveOrderBy.Name = "BtnRemoveOrderBy";
            this.BtnRemoveOrderBy.Size = new System.Drawing.Size(22, 22);
            this.BtnRemoveOrderBy.TabIndex = 4;
            this.toolTip1.SetToolTip(this.BtnRemoveOrderBy, "Remove Oreder by");
            this.BtnRemoveOrderBy.UseVisualStyleBackColor = false;
            this.BtnRemoveOrderBy.Click += new System.EventHandler(this.BtnRemoveOrderBy_Click);
            // 
            // btnOrederBy
            // 
            this.btnOrederBy.AutoEllipsis = true;
            this.btnOrederBy.BackColor = System.Drawing.Color.Transparent;
            this.btnOrederBy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOrederBy.BackgroundImage")));
            this.btnOrederBy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOrederBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrederBy.Image = ((System.Drawing.Image)(resources.GetObject("btnOrederBy.Image")));
            this.btnOrederBy.Location = new System.Drawing.Point(394, 61);
            this.btnOrederBy.Name = "btnOrederBy";
            this.btnOrederBy.Size = new System.Drawing.Size(22, 22);
            this.btnOrederBy.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnOrederBy, "Select Order by");
            this.btnOrederBy.UseVisualStyleBackColor = false;
            this.btnOrederBy.Click += new System.EventHandler(this.btnOrederBy_Click);
            // 
            // btnConfigureRace
            // 
            this.btnConfigureRace.AutoEllipsis = true;
            this.btnConfigureRace.BackColor = System.Drawing.Color.Transparent;
            this.btnConfigureRace.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.btnConfigureRace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConfigureRace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigureRace.Image = ((System.Drawing.Image)(resources.GetObject("btnConfigureRace.Image")));
            this.btnConfigureRace.Location = new System.Drawing.Point(300, 87);
            this.btnConfigureRace.Name = "btnConfigureRace";
            this.btnConfigureRace.Size = new System.Drawing.Size(22, 22);
            this.btnConfigureRace.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnConfigureRace, "Configure Race");
            this.btnConfigureRace.UseVisualStyleBackColor = false;
            this.btnConfigureRace.Click += new System.EventHandler(this.btnConfigureRace_Click);
            // 
            // btnGenrateVisit
            // 
            this.btnGenrateVisit.AutoEllipsis = true;
            this.btnGenrateVisit.BackColor = System.Drawing.Color.Transparent;
            this.btnGenrateVisit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGenrateVisit.BackgroundImage")));
            this.btnGenrateVisit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGenrateVisit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenrateVisit.Image = ((System.Drawing.Image)(resources.GetObject("btnGenrateVisit.Image")));
            this.btnGenrateVisit.Location = new System.Drawing.Point(426, 26);
            this.btnGenrateVisit.Name = "btnGenrateVisit";
            this.btnGenrateVisit.Size = new System.Drawing.Size(22, 22);
            this.btnGenrateVisit.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnGenrateVisit, "Add To Vital");
            this.btnGenrateVisit.UseVisualStyleBackColor = false;
            this.btnGenrateVisit.Click += new System.EventHandler(this.btnGenrateVisit_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtForYears);
            this.panel4.Controls.Add(this.txtCigsDay);
            this.panel4.Controls.Add(this.txtQuitYearAgo);
            this.panel4.Controls.Add(this.optSmoker);
            this.panel4.Controls.Add(this.optNonSmoker);
            this.panel4.Controls.Add(this.ChkQuit);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(1, 214);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel4.Size = new System.Drawing.Size(513, 112);
            this.panel4.TabIndex = 11;
            // 
            // txtForYears
            // 
            this.txtForYears.Location = new System.Drawing.Point(343, 40);
            this.txtForYears.MaxLength = 3;
            this.txtForYears.Name = "txtForYears";
            this.txtForYears.Size = new System.Drawing.Size(34, 22);
            this.txtForYears.TabIndex = 15;
            this.txtForYears.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtForYears_KeyPress);
            // 
            // txtCigsDay
            // 
            this.txtCigsDay.Location = new System.Drawing.Point(188, 40);
            this.txtCigsDay.MaxLength = 3;
            this.txtCigsDay.Name = "txtCigsDay";
            this.txtCigsDay.Size = new System.Drawing.Size(34, 22);
            this.txtCigsDay.TabIndex = 14;
            this.txtCigsDay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCigsDay_KeyPress);
            // 
            // txtQuitYearAgo
            // 
            this.txtQuitYearAgo.Enabled = false;
            this.txtQuitYearAgo.Location = new System.Drawing.Point(188, 70);
            this.txtQuitYearAgo.MaxLength = 3;
            this.txtQuitYearAgo.Name = "txtQuitYearAgo";
            this.txtQuitYearAgo.Size = new System.Drawing.Size(34, 22);
            this.txtQuitYearAgo.TabIndex = 17;
            this.txtQuitYearAgo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuitYearAgo_KeyPress);
            // 
            // optSmoker
            // 
            this.optSmoker.AutoSize = true;
            this.optSmoker.Location = new System.Drawing.Point(114, 44);
            this.optSmoker.Name = "optSmoker";
            this.optSmoker.Size = new System.Drawing.Size(66, 18);
            this.optSmoker.TabIndex = 13;
            this.optSmoker.Text = "Smoker";
            this.optSmoker.UseVisualStyleBackColor = true;
            this.optSmoker.CheckedChanged += new System.EventHandler(this.optSmoker_CheckedChanged);
            // 
            // optNonSmoker
            // 
            this.optNonSmoker.AutoSize = true;
            this.optNonSmoker.Checked = true;
            this.optNonSmoker.Location = new System.Drawing.Point(114, 21);
            this.optNonSmoker.Name = "optNonSmoker";
            this.optNonSmoker.Size = new System.Drawing.Size(92, 18);
            this.optNonSmoker.TabIndex = 12;
            this.optNonSmoker.TabStop = true;
            this.optNonSmoker.Text = "Non Smoker";
            this.optNonSmoker.UseVisualStyleBackColor = true;
            this.optNonSmoker.CheckedChanged += new System.EventHandler(this.optNonSmoker_CheckedChanged);
            // 
            // ChkQuit
            // 
            this.ChkQuit.AutoSize = true;
            this.ChkQuit.Enabled = false;
            this.ChkQuit.Location = new System.Drawing.Point(114, 73);
            this.ChkQuit.Name = "ChkQuit";
            this.ChkQuit.Size = new System.Drawing.Size(49, 18);
            this.ChkQuit.TabIndex = 16;
            this.ChkQuit.Text = "Quit";
            this.ChkQuit.UseVisualStyleBackColor = true;
            this.ChkQuit.CheckedChanged += new System.EventHandler(this.ChkQuit_CheckedChanged);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(509, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 107);
            this.label16.TabIndex = 105;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 107);
            this.label15.TabIndex = 104;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 108);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(507, 1);
            this.label14.TabIndex = 103;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(507, 1);
            this.label13.TabIndex = 102;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(384, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 14);
            this.label4.TabIndex = 73;
            this.label4.Text = "Years";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(316, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 14);
            this.label3.TabIndex = 72;
            this.label3.Text = "For";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 14);
            this.label2.TabIndex = 70;
            this.label2.Text = "Years ago";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 69;
            this.label1.Text = "Cigs/day";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 14);
            this.label9.TabIndex = 61;
            this.label9.Text = "Smoking Histroy :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.OptRefferal);
            this.panel1.Controls.Add(this.LblRaceErrorMsg);
            this.panel1.Controls.Add(this.optProvider);
            this.panel1.Controls.Add(this.cmbPARace);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnConfigureRace);
            this.panel1.Controls.Add(this.txtOrederd_by);
            this.panel1.Controls.Add(this.BtnRemoveOrderBy);
            this.panel1.Controls.Add(this.LblTechnician);
            this.panel1.Controls.Add(this.btnOrederBy);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lbPatientRace);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(513, 138);
            this.panel1.TabIndex = 1;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(11, 41);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(95, 14);
            this.label23.TabIndex = 109;
            this.label23.Text = "Order by Type :";
            // 
            // OptRefferal
            // 
            this.OptRefferal.AutoSize = true;
            this.OptRefferal.Location = new System.Drawing.Point(189, 39);
            this.OptRefferal.Name = "OptRefferal";
            this.OptRefferal.Size = new System.Drawing.Size(66, 18);
            this.OptRefferal.TabIndex = 107;
            this.OptRefferal.Text = "Referral";
            this.OptRefferal.UseVisualStyleBackColor = true;
            this.OptRefferal.Click += new System.EventHandler(this.OptRefferal_Click);
            // 
            // LblRaceErrorMsg
            // 
            this.LblRaceErrorMsg.AutoSize = true;
            this.LblRaceErrorMsg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRaceErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.LblRaceErrorMsg.Location = new System.Drawing.Point(113, 114);
            this.LblRaceErrorMsg.Name = "LblRaceErrorMsg";
            this.LblRaceErrorMsg.Size = new System.Drawing.Size(0, 13);
            this.LblRaceErrorMsg.TabIndex = 108;
            // 
            // optProvider
            // 
            this.optProvider.AutoSize = true;
            this.optProvider.Checked = true;
            this.optProvider.Location = new System.Drawing.Point(111, 39);
            this.optProvider.Name = "optProvider";
            this.optProvider.Size = new System.Drawing.Size(69, 18);
            this.optProvider.TabIndex = 106;
            this.optProvider.TabStop = true;
            this.optProvider.Text = "Provider";
            this.optProvider.UseVisualStyleBackColor = true;
            this.optProvider.Click += new System.EventHandler(this.optProvider_Click);
            // 
            // cmbPARace
            // 
            this.cmbPARace.Enabled = false;
            this.cmbPARace.FormattingEnabled = true;
            this.cmbPARace.Location = new System.Drawing.Point(111, 87);
            this.cmbPARace.Name = "cmbPARace";
            this.cmbPARace.Size = new System.Drawing.Size(184, 22);
            this.cmbPARace.TabIndex = 5;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(509, 4);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 130);
            this.label22.TabIndex = 106;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(3, 4);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 130);
            this.label21.TabIndex = 105;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(507, 1);
            this.label8.TabIndex = 98;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(507, 1);
            this.label6.TabIndex = 97;
            // 
            // txtOrederd_by
            // 
            this.txtOrederd_by.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtOrederd_by.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrederd_by.Location = new System.Drawing.Point(111, 61);
            this.txtOrederd_by.Name = "txtOrederd_by";
            this.txtOrederd_by.ReadOnly = true;
            this.txtOrederd_by.Size = new System.Drawing.Size(279, 22);
            this.txtOrederd_by.TabIndex = 2;
            // 
            // LblTechnician
            // 
            this.LblTechnician.AutoSize = true;
            this.LblTechnician.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTechnician.Location = new System.Drawing.Point(111, 17);
            this.LblTechnician.Name = "LblTechnician";
            this.LblTechnician.Size = new System.Drawing.Size(69, 14);
            this.LblTechnician.TabIndex = 94;
            this.LblTechnician.Text = "Technician ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 14);
            this.label5.TabIndex = 93;
            this.label5.Text = "Technician :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(29, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 14);
            this.label7.TabIndex = 92;
            this.label7.Text = "Ordered by :";
            // 
            // lbPatientRace
            // 
            this.lbPatientRace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPatientRace.AutoEllipsis = true;
            this.lbPatientRace.AutoSize = true;
            this.lbPatientRace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatientRace.Location = new System.Drawing.Point(65, 91);
            this.lbPatientRace.Name = "lbPatientRace";
            this.lbPatientRace.Size = new System.Drawing.Size(41, 14);
            this.lbPatientRace.TabIndex = 75;
            this.lbPatientRace.Text = "Race :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1);
            this.panel2.Size = new System.Drawing.Size(515, 327);
            this.panel2.TabIndex = 87;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblVitalinformation);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.btnGenrateVisit);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.txtPAWeight);
            this.panel3.Controls.Add(this.label25);
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.txtPAHeight);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(1, 139);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel3.Size = new System.Drawing.Size(513, 75);
            this.panel3.TabIndex = 7;
            // 
            // lblVitalinformation
            // 
            this.lblVitalinformation.AutoSize = true;
            this.lblVitalinformation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVitalinformation.ForeColor = System.Drawing.Color.Red;
            this.lblVitalinformation.Location = new System.Drawing.Point(113, 53);
            this.lblVitalinformation.Name = "lblVitalinformation";
            this.lblVitalinformation.Size = new System.Drawing.Size(0, 13);
            this.lblVitalinformation.TabIndex = 109;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(509, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 70);
            this.label18.TabIndex = 106;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 70);
            this.label17.TabIndex = 105;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(507, 1);
            this.label11.TabIndex = 99;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(507, 1);
            this.label10.TabIndex = 98;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(7, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 14);
            this.label12.TabIndex = 96;
            this.label12.Text = "Vital Information :";
            // 
            // txtPAWeight
            // 
            this.txtPAWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAWeight.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAWeight.Location = new System.Drawing.Point(302, 26);
            this.txtPAWeight.MaxLength = 50;
            this.txtPAWeight.Name = "txtPAWeight";
            this.txtPAWeight.ReadOnly = true;
            this.txtPAWeight.Size = new System.Drawing.Size(90, 22);
            this.txtPAWeight.TabIndex = 9;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(207, 33);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(26, 11);
            this.label25.TabIndex = 75;
            this.label25.Text = "(Cm)";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(399, 33);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(22, 11);
            this.label26.TabIndex = 76;
            this.label26.Text = "(lbs)";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(240, 31);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 14);
            this.label20.TabIndex = 73;
            this.label20.Text = "Weight :";
            // 
            // txtPAHeight
            // 
            this.txtPAHeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAHeight.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAHeight.Location = new System.Drawing.Point(110, 26);
            this.txtPAHeight.MaxLength = 50;
            this.txtPAHeight.Name = "txtPAHeight";
            this.txtPAHeight.ReadOnly = true;
            this.txtPAHeight.Size = new System.Drawing.Size(90, 22);
            this.txtPAHeight.TabIndex = 8;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(55, 30);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(51, 14);
            this.label19.TabIndex = 71;
            this.label19.Text = "Height :";
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_LabMain);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(515, 54);
            this.pnlToolStrip.TabIndex = 46;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Controls.Add(this.pnlToolStrip);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(515, 381);
            this.panel5.TabIndex = 106;
            // 
            // frmSpirometryTestsNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(515, 381);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpirometryTestsNew";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spirometry Test";
            this.Load += new System.EventHandler(this.frmSpiroTest_New_Load);
            this.ts_LabMain.ResumeLayout(false);
            this.ts_LabMain.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus ts_LabMain;
        private System.Windows.Forms.ToolStripButton tlbbtnNew;
        private System.Windows.Forms.ToolStripButton tlbbtnclose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtOrederd_by;
        private System.Windows.Forms.Button BtnRemoveOrderBy;
        private System.Windows.Forms.Label LblTechnician;
        private System.Windows.Forms.Button btnOrederBy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbPatientRace;
        private System.Windows.Forms.Button btnConfigureRace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPAWeight;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtPAHeight;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnGenrateVisit;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox ChkQuit;
        private System.Windows.Forms.ComboBox cmbPARace;
        private System.Windows.Forms.Label LblRaceErrorMsg;
        private System.Windows.Forms.Label lblVitalinformation;
        private System.Windows.Forms.RadioButton optSmoker;
        private System.Windows.Forms.RadioButton optNonSmoker;
        private System.Windows.Forms.TextBox txtForYears;
        private System.Windows.Forms.TextBox txtCigsDay;
        private System.Windows.Forms.TextBox txtQuitYearAgo;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.RadioButton OptRefferal;
        private System.Windows.Forms.RadioButton optProvider;
    }
}