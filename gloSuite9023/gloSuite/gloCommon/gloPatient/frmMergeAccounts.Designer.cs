namespace gloPatient
{
    partial class frmMergeAccounts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMergeAccounts));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.btnclear1 = new System.Windows.Forms.Button();
            this.btnPatient = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lblDOB_Source = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.lblSSN_Source = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tlsp_MergePatientRecords = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnMerge = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnl_tlsp = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlToBeMergeMain = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.btnclear2 = new System.Windows.Forms.Button();
            this.btnToBeMergePatient = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.lblToBeMergeDOB = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lblToBeMergeSSN = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txtToBeMergePatientName = new System.Windows.Forms.TextBox();
            this.pnlMergeAccount = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblToBeGuarantorDetails = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAccount2Desc = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblBusinessCenter2 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtPatAccount2 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btnToBeMergeAccount = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPAcc2_topBrd = new System.Windows.Forms.Label();
            this.lblPAcc2_rightBrd = new System.Windows.Forms.Label();
            this.lblPAcc2_leftBrd = new System.Windows.Forms.Label();
            this.lblPacc2_btmBrd = new System.Windows.Forms.Label();
            this.pnlGIContactDetails = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblGuarantorDetails = new System.Windows.Forms.Label();
            this.lblAccountDescription = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAccount1Desc = new System.Windows.Forms.Label();
            this.pnlBusinessCenter = new System.Windows.Forms.Panel();
            this.lblBusinessCenter1 = new System.Windows.Forms.Label();
            this.lblBusinessCentr = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPatAccount1 = new System.Windows.Forms.TextBox();
            this.lblAccountNo = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.btnRemoveAccount = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.tlsp_MergePatientRecords.SuspendLayout();
            this.pnl_tlsp.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlToBeMergeMain.SuspendLayout();
            this.pnlMergeAccount.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlGIContactDetails.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlBusinessCenter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.label14);
            this.pnlMain.Controls.Add(this.btnclear1);
            this.pnlMain.Controls.Add(this.btnPatient);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.lbl_BottomBrd);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.lbl_LeftBrd);
            this.pnlMain.Controls.Add(this.lblDOB_Source);
            this.pnlMain.Controls.Add(this.Label1);
            this.pnlMain.Controls.Add(this.Label8);
            this.pnlMain.Controls.Add(this.lblSSN_Source);
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.Label4);
            this.pnlMain.Controls.Add(this.txtPatientName);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.pnlMain.Size = new System.Drawing.Size(633, 85);
            this.pnlMain.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoEllipsis = true;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(66, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 14);
            this.label14.TabIndex = 131;
            this.label14.Text = "*";
            // 
            // btnclear1
            // 
            this.btnclear1.AutoEllipsis = true;
            this.btnclear1.BackColor = System.Drawing.Color.Transparent;
            this.btnclear1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnclear1.BackgroundImage")));
            this.btnclear1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclear1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclear1.Image = ((System.Drawing.Image)(resources.GetObject("btnclear1.Image")));
            this.btnclear1.Location = new System.Drawing.Point(422, 24);
            this.btnclear1.Name = "btnclear1";
            this.btnclear1.Size = new System.Drawing.Size(21, 21);
            this.btnclear1.TabIndex = 43;
            this.btnclear1.UseVisualStyleBackColor = false;
            this.btnclear1.Click += new System.EventHandler(this.btnclear1_Click);
            this.btnclear1.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnclear1.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnPatient
            // 
            this.btnPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPatient.BackgroundImage")));
            this.btnPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnPatient.Image")));
            this.btnPatient.Location = new System.Drawing.Point(396, 24);
            this.btnPatient.Name = "btnPatient";
            this.btnPatient.Size = new System.Drawing.Size(21, 21);
            this.btnPatient.TabIndex = 31;
            this.btnPatient.UseVisualStyleBackColor = false;
            this.btnPatient.Click += new System.EventHandler(this.btnPatient_Click);
            this.btnPatient.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnPatient.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(4, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(625, 1);
            this.label5.TabIndex = 30;
            this.label5.Text = "label2";
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 83);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(625, 1);
            this.lbl_BottomBrd.TabIndex = 29;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(629, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 81);
            this.label3.TabIndex = 28;
            this.label3.Text = "label4";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 81);
            this.lbl_LeftBrd.TabIndex = 27;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lblDOB_Source
            // 
            this.lblDOB_Source.BackColor = System.Drawing.Color.White;
            this.lblDOB_Source.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDOB_Source.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDOB_Source.Location = new System.Drawing.Point(293, 50);
            this.lblDOB_Source.Name = "lblDOB_Source";
            this.lblDOB_Source.Size = new System.Drawing.Size(98, 22);
            this.lblDOB_Source.TabIndex = 18;
            this.lblDOB_Source.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Location = new System.Drawing.Point(78, 29);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(46, 14);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "Name :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Location = new System.Drawing.Point(252, 55);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(39, 14);
            this.Label8.TabIndex = 17;
            this.Label8.Text = "DOB :";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSSN_Source
            // 
            this.lblSSN_Source.BackColor = System.Drawing.Color.White;
            this.lblSSN_Source.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSSN_Source.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSSN_Source.Location = new System.Drawing.Point(125, 50);
            this.lblSSN_Source.Name = "lblSSN_Source";
            this.lblSSN_Source.Size = new System.Drawing.Size(115, 22);
            this.lblSSN_Source.TabIndex = 4;
            this.lblSSN_Source.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(8, 8);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(60, 14);
            this.label22.TabIndex = 12;
            this.label22.Text = "Patient :";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Location = new System.Drawing.Point(87, 57);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(37, 14);
            this.Label4.TabIndex = 12;
            this.Label4.Text = "SSN :";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPatientName
            // 
            this.txtPatientName.BackColor = System.Drawing.Color.White;
            this.txtPatientName.Location = new System.Drawing.Point(125, 23);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(262, 22);
            this.txtPatientName.TabIndex = 32;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(63, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(89, 38);
            this.panel2.TabIndex = 44;
            this.panel2.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(54, 26);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(51, 14);
            this.label21.TabIndex = 34;
            this.label21.Text = "label21";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label21.Visible = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(54, 10);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(567, 56);
            this.label20.TabIndex = 34;
            this.label20.Text = resources.GetString("label20.Text");
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(8, 11);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 14);
            this.label19.TabIndex = 33;
            this.label19.Text = "Note :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(3, 95);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(627, 1);
            this.label17.TabIndex = 32;
            this.label17.Text = "label2";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label15.Location = new System.Drawing.Point(4, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(625, 1);
            this.label15.TabIndex = 31;
            this.label15.Text = "label2";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(629, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 94);
            this.label13.TabIndex = 29;
            this.label13.Text = "label4";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 94);
            this.label12.TabIndex = 28;
            this.label12.Text = "label4";
            // 
            // tlsp_MergePatientRecords
            // 
            this.tlsp_MergePatientRecords.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_MergePatientRecords.BackgroundImage = global::gloPatient.Properties.Resources.Img_Toolstrip;
            this.tlsp_MergePatientRecords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_MergePatientRecords.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_MergePatientRecords.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_MergePatientRecords.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnMerge,
            this.tsb_Close});
            this.tlsp_MergePatientRecords.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_MergePatientRecords.Location = new System.Drawing.Point(0, 0);
            this.tlsp_MergePatientRecords.Name = "tlsp_MergePatientRecords";
            this.tlsp_MergePatientRecords.Size = new System.Drawing.Size(633, 53);
            this.tlsp_MergePatientRecords.TabIndex = 5;
            this.tlsp_MergePatientRecords.Text = "toolStrip1";
            // 
            // ts_btnMerge
            // 
            this.ts_btnMerge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnMerge.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnMerge.Image")));
            this.ts_btnMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnMerge.Name = "ts_btnMerge";
            this.ts_btnMerge.Size = new System.Drawing.Size(49, 50);
            this.ts_btnMerge.Tag = "Merge";
            this.ts_btnMerge.Text = "&Merge";
            this.ts_btnMerge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnMerge.Click += new System.EventHandler(this.ts_btnMerge_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // pnl_tlsp
            // 
            this.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp.Controls.Add(this.tlsp_MergePatientRecords);
            this.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnl_tlsp.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp.Name = "pnl_tlsp";
            this.pnl_tlsp.Size = new System.Drawing.Size(633, 54);
            this.pnl_tlsp.TabIndex = 15;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 139);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.panel3.Size = new System.Drawing.Size(633, 97);
            this.panel3.TabIndex = 45;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(54, 71);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 14);
            this.label18.TabIndex = 35;
            this.label18.Text = "label18";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label18.Visible = false;
            // 
            // pnlToBeMergeMain
            // 
            this.pnlToBeMergeMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToBeMergeMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlToBeMergeMain.Controls.Add(this.label11);
            this.pnlToBeMergeMain.Controls.Add(this.btnclear2);
            this.pnlToBeMergeMain.Controls.Add(this.btnToBeMergePatient);
            this.pnlToBeMergeMain.Controls.Add(this.label27);
            this.pnlToBeMergeMain.Controls.Add(this.label28);
            this.pnlToBeMergeMain.Controls.Add(this.label29);
            this.pnlToBeMergeMain.Controls.Add(this.label30);
            this.pnlToBeMergeMain.Controls.Add(this.lblToBeMergeDOB);
            this.pnlToBeMergeMain.Controls.Add(this.label32);
            this.pnlToBeMergeMain.Controls.Add(this.label33);
            this.pnlToBeMergeMain.Controls.Add(this.lblToBeMergeSSN);
            this.pnlToBeMergeMain.Controls.Add(this.label35);
            this.pnlToBeMergeMain.Controls.Add(this.label36);
            this.pnlToBeMergeMain.Controls.Add(this.txtToBeMergePatientName);
            this.pnlToBeMergeMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToBeMergeMain.Location = new System.Drawing.Point(0, 406);
            this.pnlToBeMergeMain.Name = "pnlToBeMergeMain";
            this.pnlToBeMergeMain.Padding = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.pnlToBeMergeMain.Size = new System.Drawing.Size(633, 84);
            this.pnlToBeMergeMain.TabIndex = 114;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoEllipsis = true;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(66, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 14);
            this.label11.TabIndex = 131;
            this.label11.Text = "*";
            // 
            // btnclear2
            // 
            this.btnclear2.AutoEllipsis = true;
            this.btnclear2.BackColor = System.Drawing.Color.Transparent;
            this.btnclear2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnclear2.BackgroundImage")));
            this.btnclear2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclear2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclear2.Image = ((System.Drawing.Image)(resources.GetObject("btnclear2.Image")));
            this.btnclear2.Location = new System.Drawing.Point(422, 21);
            this.btnclear2.Name = "btnclear2";
            this.btnclear2.Size = new System.Drawing.Size(21, 21);
            this.btnclear2.TabIndex = 43;
            this.btnclear2.UseVisualStyleBackColor = false;
            this.btnclear2.Click += new System.EventHandler(this.btnclear2_Click);
            // 
            // btnToBeMergePatient
            // 
            this.btnToBeMergePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnToBeMergePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnToBeMergePatient.BackgroundImage")));
            this.btnToBeMergePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnToBeMergePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnToBeMergePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToBeMergePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnToBeMergePatient.Image")));
            this.btnToBeMergePatient.Location = new System.Drawing.Point(396, 21);
            this.btnToBeMergePatient.Name = "btnToBeMergePatient";
            this.btnToBeMergePatient.Size = new System.Drawing.Size(21, 21);
            this.btnToBeMergePatient.TabIndex = 31;
            this.btnToBeMergePatient.UseVisualStyleBackColor = false;
            this.btnToBeMergePatient.Click += new System.EventHandler(this.btnToBeMergePatient_Click);
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label27.Location = new System.Drawing.Point(4, 3);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(625, 1);
            this.label27.TabIndex = 30;
            this.label27.Text = "label2";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label28.Location = new System.Drawing.Point(4, 82);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(625, 1);
            this.label28.TabIndex = 29;
            this.label28.Text = "label2";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(629, 3);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 80);
            this.label29.TabIndex = 28;
            this.label29.Text = "label4";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(3, 3);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 80);
            this.label30.TabIndex = 27;
            this.label30.Text = "label4";
            // 
            // lblToBeMergeDOB
            // 
            this.lblToBeMergeDOB.BackColor = System.Drawing.Color.White;
            this.lblToBeMergeDOB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblToBeMergeDOB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblToBeMergeDOB.Location = new System.Drawing.Point(293, 47);
            this.lblToBeMergeDOB.Name = "lblToBeMergeDOB";
            this.lblToBeMergeDOB.Size = new System.Drawing.Size(98, 22);
            this.lblToBeMergeDOB.TabIndex = 18;
            this.lblToBeMergeDOB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Location = new System.Drawing.Point(78, 26);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(46, 14);
            this.label32.TabIndex = 4;
            this.label32.Text = "Name :";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Location = new System.Drawing.Point(252, 52);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(39, 14);
            this.label33.TabIndex = 17;
            this.label33.Text = "DOB :";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblToBeMergeSSN
            // 
            this.lblToBeMergeSSN.BackColor = System.Drawing.Color.White;
            this.lblToBeMergeSSN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblToBeMergeSSN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblToBeMergeSSN.Location = new System.Drawing.Point(125, 47);
            this.lblToBeMergeSSN.Name = "lblToBeMergeSSN";
            this.lblToBeMergeSSN.Size = new System.Drawing.Size(115, 22);
            this.lblToBeMergeSSN.TabIndex = 4;
            this.lblToBeMergeSSN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(8, 8);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(60, 14);
            this.label35.TabIndex = 12;
            this.label35.Text = "Patient :";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.Transparent;
            this.label36.Location = new System.Drawing.Point(87, 54);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(37, 14);
            this.label36.TabIndex = 12;
            this.label36.Text = "SSN :";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtToBeMergePatientName
            // 
            this.txtToBeMergePatientName.BackColor = System.Drawing.Color.White;
            this.txtToBeMergePatientName.Location = new System.Drawing.Point(125, 20);
            this.txtToBeMergePatientName.Name = "txtToBeMergePatientName";
            this.txtToBeMergePatientName.ReadOnly = true;
            this.txtToBeMergePatientName.Size = new System.Drawing.Size(262, 22);
            this.txtToBeMergePatientName.TabIndex = 32;
            // 
            // pnlMergeAccount
            // 
            this.pnlMergeAccount.Controls.Add(this.panel7);
            this.pnlMergeAccount.Controls.Add(this.panel6);
            this.pnlMergeAccount.Controls.Add(this.panel5);
            this.pnlMergeAccount.Controls.Add(this.label10);
            this.pnlMergeAccount.Controls.Add(this.lblPAcc2_topBrd);
            this.pnlMergeAccount.Controls.Add(this.lblPAcc2_rightBrd);
            this.pnlMergeAccount.Controls.Add(this.lblPAcc2_leftBrd);
            this.pnlMergeAccount.Controls.Add(this.lblPacc2_btmBrd);
            this.pnlMergeAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMergeAccount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMergeAccount.Location = new System.Drawing.Point(0, 490);
            this.pnlMergeAccount.Name = "pnlMergeAccount";
            this.pnlMergeAccount.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.pnlMergeAccount.Size = new System.Drawing.Size(633, 168);
            this.pnlMergeAccount.TabIndex = 115;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lblToBeGuarantorDetails);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.txtAccount2Desc);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(4, 67);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(625, 99);
            this.panel7.TabIndex = 116;
            // 
            // lblToBeGuarantorDetails
            // 
            this.lblToBeGuarantorDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToBeGuarantorDetails.AutoEllipsis = true;
            this.lblToBeGuarantorDetails.BackColor = System.Drawing.Color.Transparent;
            this.lblToBeGuarantorDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToBeGuarantorDetails.Location = new System.Drawing.Point(125, 31);
            this.lblToBeGuarantorDetails.Name = "lblToBeGuarantorDetails";
            this.lblToBeGuarantorDetails.Size = new System.Drawing.Size(473, 57);
            this.lblToBeGuarantorDetails.TabIndex = 129;
            this.lblToBeGuarantorDetails.Text = "GuarantorDetails";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(55, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 14);
            this.label7.TabIndex = 74;
            this.label7.Text = "Guarantor :";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(50, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 14);
            this.label6.TabIndex = 72;
            this.label6.Text = "Acct. Desc :";
            // 
            // txtAccount2Desc
            // 
            this.txtAccount2Desc.BackColor = System.Drawing.Color.White;
            this.txtAccount2Desc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtAccount2Desc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAccount2Desc.Location = new System.Drawing.Point(125, 3);
            this.txtAccount2Desc.Name = "txtAccount2Desc";
            this.txtAccount2Desc.Size = new System.Drawing.Size(260, 22);
            this.txtAccount2Desc.TabIndex = 131;
            this.txtAccount2Desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblBusinessCenter2);
            this.panel6.Controls.Add(this.label31);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(4, 43);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(625, 24);
            this.panel6.TabIndex = 135;
            // 
            // lblBusinessCenter2
            // 
            this.lblBusinessCenter2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBusinessCenter2.AutoEllipsis = true;
            this.lblBusinessCenter2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessCenter2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBusinessCenter2.Location = new System.Drawing.Point(125, 5);
            this.lblBusinessCenter2.Name = "lblBusinessCenter2";
            this.lblBusinessCenter2.Size = new System.Drawing.Size(490, 14);
            this.lblBusinessCenter2.TabIndex = 118;
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoEllipsis = true;
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(23, 5);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(101, 14);
            this.label31.TabIndex = 117;
            this.label31.Text = "Business Center :";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtPatAccount2);
            this.panel5.Controls.Add(this.label24);
            this.panel5.Controls.Add(this.btnToBeMergeAccount);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(4, 16);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(625, 27);
            this.panel5.TabIndex = 134;
            // 
            // txtPatAccount2
            // 
            this.txtPatAccount2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPatAccount2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatAccount2.Location = new System.Drawing.Point(125, 3);
            this.txtPatAccount2.Name = "txtPatAccount2";
            this.txtPatAccount2.ReadOnly = true;
            this.txtPatAccount2.Size = new System.Drawing.Size(260, 22);
            this.txtPatAccount2.TabIndex = 71;
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoEllipsis = true;
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(71, 7);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 14);
            this.label24.TabIndex = 70;
            this.label24.Text = "Acct.# :";
            // 
            // btnToBeMergeAccount
            // 
            this.btnToBeMergeAccount.AutoEllipsis = true;
            this.btnToBeMergeAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnToBeMergeAccount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnToBeMergeAccount.BackgroundImage")));
            this.btnToBeMergeAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnToBeMergeAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToBeMergeAccount.Image = ((System.Drawing.Image)(resources.GetObject("btnToBeMergeAccount.Image")));
            this.btnToBeMergeAccount.Location = new System.Drawing.Point(390, 4);
            this.btnToBeMergeAccount.Name = "btnToBeMergeAccount";
            this.btnToBeMergeAccount.Size = new System.Drawing.Size(22, 21);
            this.btnToBeMergeAccount.TabIndex = 127;
            this.btnToBeMergeAccount.UseVisualStyleBackColor = false;
            this.btnToBeMergeAccount.Click += new System.EventHandler(this.btnToBeMergeRemoveAccount_Click);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoEllipsis = true;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(59, 7);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 14);
            this.label16.TabIndex = 118;
            this.label16.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 14);
            this.label10.TabIndex = 133;
            this.label10.Text = "Patient Account 2 :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPAcc2_topBrd
            // 
            this.lblPAcc2_topBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPAcc2_topBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPAcc2_topBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPAcc2_topBrd.Location = new System.Drawing.Point(4, 1);
            this.lblPAcc2_topBrd.Name = "lblPAcc2_topBrd";
            this.lblPAcc2_topBrd.Size = new System.Drawing.Size(625, 1);
            this.lblPAcc2_topBrd.TabIndex = 133;
            this.lblPAcc2_topBrd.Text = "label1";
            // 
            // lblPAcc2_rightBrd
            // 
            this.lblPAcc2_rightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPAcc2_rightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPAcc2_rightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPAcc2_rightBrd.Location = new System.Drawing.Point(629, 1);
            this.lblPAcc2_rightBrd.Name = "lblPAcc2_rightBrd";
            this.lblPAcc2_rightBrd.Size = new System.Drawing.Size(1, 165);
            this.lblPAcc2_rightBrd.TabIndex = 132;
            this.lblPAcc2_rightBrd.Text = "label3";
            // 
            // lblPAcc2_leftBrd
            // 
            this.lblPAcc2_leftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPAcc2_leftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPAcc2_leftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPAcc2_leftBrd.Location = new System.Drawing.Point(3, 1);
            this.lblPAcc2_leftBrd.Name = "lblPAcc2_leftBrd";
            this.lblPAcc2_leftBrd.Size = new System.Drawing.Size(1, 165);
            this.lblPAcc2_leftBrd.TabIndex = 131;
            this.lblPAcc2_leftBrd.Text = "label4";
            // 
            // lblPacc2_btmBrd
            // 
            this.lblPacc2_btmBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPacc2_btmBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPacc2_btmBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPacc2_btmBrd.Location = new System.Drawing.Point(3, 166);
            this.lblPacc2_btmBrd.Name = "lblPacc2_btmBrd";
            this.lblPacc2_btmBrd.Size = new System.Drawing.Size(627, 1);
            this.lblPacc2_btmBrd.TabIndex = 130;
            this.lblPacc2_btmBrd.Text = "label2";
            // 
            // pnlGIContactDetails
            // 
            this.pnlGIContactDetails.Controls.Add(this.panel4);
            this.pnlGIContactDetails.Controls.Add(this.pnlBusinessCenter);
            this.pnlGIContactDetails.Controls.Add(this.panel1);
            this.pnlGIContactDetails.Controls.Add(this.label23);
            this.pnlGIContactDetails.Controls.Add(this.label9);
            this.pnlGIContactDetails.Controls.Add(this.label25);
            this.pnlGIContactDetails.Controls.Add(this.label26);
            this.pnlGIContactDetails.Controls.Add(this.lbl_RightBrd);
            this.pnlGIContactDetails.Controls.Add(this.lbl_TopBrd);
            this.pnlGIContactDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGIContactDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGIContactDetails.Location = new System.Drawing.Point(0, 236);
            this.pnlGIContactDetails.Name = "pnlGIContactDetails";
            this.pnlGIContactDetails.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.pnlGIContactDetails.Size = new System.Drawing.Size(633, 170);
            this.pnlGIContactDetails.TabIndex = 116;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblGuarantorDetails);
            this.panel4.Controls.Add(this.lblAccountDescription);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.txtAccount1Desc);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(4, 70);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(625, 98);
            this.panel4.TabIndex = 134;
            // 
            // lblGuarantorDetails
            // 
            this.lblGuarantorDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGuarantorDetails.AutoEllipsis = true;
            this.lblGuarantorDetails.BackColor = System.Drawing.Color.Transparent;
            this.lblGuarantorDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuarantorDetails.Location = new System.Drawing.Point(125, 35);
            this.lblGuarantorDetails.Name = "lblGuarantorDetails";
            this.lblGuarantorDetails.Size = new System.Drawing.Size(473, 57);
            this.lblGuarantorDetails.TabIndex = 114;
            this.lblGuarantorDetails.Text = "GuarantorDetails";
            // 
            // lblAccountDescription
            // 
            this.lblAccountDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountDescription.AutoEllipsis = true;
            this.lblAccountDescription.AutoSize = true;
            this.lblAccountDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountDescription.Location = new System.Drawing.Point(50, 10);
            this.lblAccountDescription.Name = "lblAccountDescription";
            this.lblAccountDescription.Size = new System.Drawing.Size(74, 14);
            this.lblAccountDescription.TabIndex = 72;
            this.lblAccountDescription.Text = "Acct. Desc :";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 19);
            this.label2.TabIndex = 115;
            this.label2.Text = "Guarantor :";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // txtAccount1Desc
            // 
            this.txtAccount1Desc.BackColor = System.Drawing.Color.White;
            this.txtAccount1Desc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtAccount1Desc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAccount1Desc.Location = new System.Drawing.Point(125, 6);
            this.txtAccount1Desc.Name = "txtAccount1Desc";
            this.txtAccount1Desc.Size = new System.Drawing.Size(264, 22);
            this.txtAccount1Desc.TabIndex = 45;
            this.txtAccount1Desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlBusinessCenter
            // 
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCenter1);
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCentr);
            this.pnlBusinessCenter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusinessCenter.Location = new System.Drawing.Point(4, 46);
            this.pnlBusinessCenter.Name = "pnlBusinessCenter";
            this.pnlBusinessCenter.Size = new System.Drawing.Size(625, 24);
            this.pnlBusinessCenter.TabIndex = 133;
            // 
            // lblBusinessCenter1
            // 
            this.lblBusinessCenter1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBusinessCenter1.AutoEllipsis = true;
            this.lblBusinessCenter1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessCenter1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBusinessCenter1.Location = new System.Drawing.Point(125, 5);
            this.lblBusinessCenter1.Name = "lblBusinessCenter1";
            this.lblBusinessCenter1.Size = new System.Drawing.Size(490, 14);
            this.lblBusinessCenter1.TabIndex = 119;
            // 
            // lblBusinessCentr
            // 
            this.lblBusinessCentr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBusinessCentr.AutoEllipsis = true;
            this.lblBusinessCentr.AutoSize = true;
            this.lblBusinessCentr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessCentr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBusinessCentr.Location = new System.Drawing.Point(23, 5);
            this.lblBusinessCentr.Name = "lblBusinessCentr";
            this.lblBusinessCentr.Size = new System.Drawing.Size(101, 14);
            this.lblBusinessCentr.TabIndex = 117;
            this.lblBusinessCentr.Text = "Business Center :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtPatAccount1);
            this.panel1.Controls.Add(this.lblAccountNo);
            this.panel1.Controls.Add(this.label39);
            this.panel1.Controls.Add(this.btnRemoveAccount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(625, 30);
            this.panel1.TabIndex = 134;
            // 
            // txtPatAccount1
            // 
            this.txtPatAccount1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPatAccount1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatAccount1.Location = new System.Drawing.Point(125, 4);
            this.txtPatAccount1.Name = "txtPatAccount1";
            this.txtPatAccount1.ReadOnly = true;
            this.txtPatAccount1.Size = new System.Drawing.Size(264, 22);
            this.txtPatAccount1.TabIndex = 71;
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountNo.AutoEllipsis = true;
            this.lblAccountNo.AutoSize = true;
            this.lblAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountNo.Location = new System.Drawing.Point(71, 8);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.Size = new System.Drawing.Size(53, 14);
            this.lblAccountNo.TabIndex = 70;
            this.lblAccountNo.Text = "Acct.# :";
            this.lblAccountNo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.AutoEllipsis = true;
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(57, 8);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(14, 14);
            this.label39.TabIndex = 111;
            this.label39.Text = "*";
            // 
            // btnRemoveAccount
            // 
            this.btnRemoveAccount.AutoEllipsis = true;
            this.btnRemoveAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoveAccount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveAccount.BackgroundImage")));
            this.btnRemoveAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveAccount.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveAccount.Image")));
            this.btnRemoveAccount.Location = new System.Drawing.Point(390, 5);
            this.btnRemoveAccount.Name = "btnRemoveAccount";
            this.btnRemoveAccount.Size = new System.Drawing.Size(22, 21);
            this.btnRemoveAccount.TabIndex = 130;
            this.btnRemoveAccount.UseVisualStyleBackColor = false;
            this.btnRemoveAccount.Click += new System.EventHandler(this.btnRemoveAccount_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(4, 2);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(127, 14);
            this.label23.TabIndex = 132;
            this.label23.Text = "Patient Account 1 :";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 202);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.label9.Size = new System.Drawing.Size(70, 22);
            this.label9.TabIndex = 116;
            this.label9.Text = "Patient :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label25.Location = new System.Drawing.Point(4, 168);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(625, 1);
            this.label25.TabIndex = 30;
            this.label25.Text = "label2";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(3, 2);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 167);
            this.label26.TabIndex = 29;
            this.label26.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(629, 2);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 167);
            this.lbl_RightBrd.TabIndex = 28;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(627, 1);
            this.lbl_TopBrd.TabIndex = 27;
            this.lbl_TopBrd.Text = "label1";
            // 
            // frmMergeAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(633, 658);
            this.Controls.Add(this.pnlMergeAccount);
            this.Controls.Add(this.pnlToBeMergeMain);
            this.Controls.Add(this.pnlGIContactDetails);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnl_tlsp);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMergeAccounts";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Merge Patient Account";
            this.Load += new System.EventHandler(this.frmMergeAccounts_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.tlsp_MergePatientRecords.ResumeLayout(false);
            this.tlsp_MergePatientRecords.PerformLayout();
            this.pnl_tlsp.ResumeLayout(false);
            this.pnl_tlsp.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlToBeMergeMain.ResumeLayout(false);
            this.pnlToBeMergeMain.PerformLayout();
            this.pnlMergeAccount.ResumeLayout(false);
            this.pnlMergeAccount.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlGIContactDetails.ResumeLayout(false);
            this.pnlGIContactDetails.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlBusinessCenter.ResumeLayout(false);
            this.pnlBusinessCenter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlMain;
        internal System.Windows.Forms.Label lblDOB_Source;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label lblSSN_Source;
        internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Button btnPatient;
        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.Button btnclear1;
        private System.Windows.Forms.Panel panel2;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_MergePatientRecords;
        private System.Windows.Forms.ToolStripButton ts_btnMerge;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnl_tlsp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Label label21;
        internal System.Windows.Forms.Label label20;
        internal System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label label18;
        internal System.Windows.Forms.Label label22;
        internal System.Windows.Forms.Panel pnlToBeMergeMain;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnclear2;
        private System.Windows.Forms.Button btnToBeMergePatient;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        internal System.Windows.Forms.Label lblToBeMergeDOB;
        internal System.Windows.Forms.Label label32;
        internal System.Windows.Forms.Label label33;
        internal System.Windows.Forms.Label lblToBeMergeSSN;
        internal System.Windows.Forms.Label label35;
        internal System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtToBeMergePatientName;
        private System.Windows.Forms.Panel pnlMergeAccount;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblToBeGuarantorDetails;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label txtAccount2Desc;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblBusinessCenter2;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.TextBox txtPatAccount2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnToBeMergeAccount;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPAcc2_topBrd;
        private System.Windows.Forms.Label lblPAcc2_rightBrd;
        private System.Windows.Forms.Label lblPAcc2_leftBrd;
        private System.Windows.Forms.Label lblPacc2_btmBrd;
        private System.Windows.Forms.Panel pnlGIContactDetails;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblGuarantorDetails;
        private System.Windows.Forms.Label lblAccountDescription;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label txtAccount1Desc;
        private System.Windows.Forms.Panel pnlBusinessCenter;
        private System.Windows.Forms.Label lblBusinessCenter1;
        private System.Windows.Forms.Label lblBusinessCentr;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtPatAccount1;
        private System.Windows.Forms.Label lblAccountNo;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Button btnRemoveAccount;
        internal System.Windows.Forms.Label label23;
        internal System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
    }
}