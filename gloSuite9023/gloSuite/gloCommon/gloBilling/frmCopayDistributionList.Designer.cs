namespace gloBilling
{
    partial class frmCopayDistributionList
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtEndDate, dtStartDate };
            System.Windows.Forms.Control[] cntControls = { dtEndDate, dtStartDate };

            if (disposing && (components != null))
            {
                components.Dispose();
                base.Dispose(disposing);
                try
                {
                    //if (dtEndDate != null)
                    //{
                    //    try
                    //    {
                    //        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtEndDate);

                    //    }
                    //    catch
                    //    {
                    //    }


                    //    dtEndDate.Dispose();
                    //    dtEndDate = null;
                    //}


                    if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }
                        
                }
                catch
                {
                }

                //try
                //{
                //    //if (dtStartDate != null)
                //    //{
                //    //    try
                //    //    {
                //    //        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtStartDate);

                //    //    }
                //    //    catch
                //    //    {
                //    //    }


                //    //    dtStartDate.Dispose();
                //    //    dtStartDate = null;
                //    //}
                //}
                //catch
                //{
                //}

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
              
            }
           
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCopayDistributionList));
            this.tls_CopayList = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_CopayList = new System.Windows.Forms.ToolStripButton();
            this.tsb_NewAutoDistributeCopay = new System.Windows.Forms.ToolStripButton();
            this.tls_btnPatAcct = new System.Windows.Forms.ToolStripButton();
            this.tsb_PaymentPatient = new System.Windows.Forms.ToolStripButton();
            this.tsb_AutoDistributeCopay = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveAutoCopay = new System.Windows.Forms.ToolStripButton();
            this.tsb_Next = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_close = new System.Windows.Forms.ToolStripButton();
            this.pnlText = new System.Windows.Forms.Panel();
            this.rb_ShowOneByOne = new System.Windows.Forms.RadioButton();
            this.rb_ShowAll = new System.Windows.Forms.RadioButton();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_ListName = new System.Windows.Forms.Label();
            this.pnl_CopayList = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.C1CopayList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnl_AutoCopayDistributionList = new System.Windows.Forms.Panel();
            this.C1AutoCopayList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pnlReserveDetails = new System.Windows.Forms.Panel();
            this.pnlRDGrid = new System.Windows.Forms.Panel();
            this.c1Reserve = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label80 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlReserveDetailsToolStrip = new System.Windows.Forms.Panel();
            this.tls_DXCPT = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_RDClose = new System.Windows.Forms.ToolStripButton();
            this.pnlDxHeader = new System.Windows.Forms.Panel();
            this.pnlRDTop = new System.Windows.Forms.Panel();
            this.label83 = new System.Windows.Forms.Label();
            this.btnRDClose = new System.Windows.Forms.Button();
            this.imgCPTDX = new System.Windows.Forms.PictureBox();
            this.lblExamDxCPT = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.tls_CopayList.SuspendLayout();
            this.pnlText.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnl_CopayList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1CopayList)).BeginInit();
            this.panel4.SuspendLayout();
            this.pnl_AutoCopayDistributionList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1AutoCopayList)).BeginInit();
            this.pnlReserveDetails.SuspendLayout();
            this.pnlRDGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Reserve)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.pnlReserveDetailsToolStrip.SuspendLayout();
            this.tls_DXCPT.SuspendLayout();
            this.pnlDxHeader.SuspendLayout();
            this.pnlRDTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCPTDX)).BeginInit();
            this.SuspendLayout();
            // 
            // tls_CopayList
            // 
            this.tls_CopayList.BackColor = System.Drawing.Color.Transparent;
            this.tls_CopayList.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_CopayList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_CopayList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_CopayList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_CopayList.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_CopayList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_CopayList,
            this.tsb_NewAutoDistributeCopay,
            this.tls_btnPatAcct,
            this.tsb_PaymentPatient,
            this.tsb_AutoDistributeCopay,
            this.tsb_SaveAutoCopay,
            this.tsb_Next,
            this.tsb_Refresh,
            this.tsb_close});
            this.tls_CopayList.Location = new System.Drawing.Point(0, 0);
            this.tls_CopayList.Name = "tls_CopayList";
            this.tls_CopayList.Size = new System.Drawing.Size(1274, 53);
            this.tls_CopayList.TabIndex = 0;
            this.tls_CopayList.TabStop = true;
            this.tls_CopayList.Text = "toolStrip1";
            // 
            // tsb_CopayList
            // 
            this.tsb_CopayList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_CopayList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_CopayList.Image = ((System.Drawing.Image)(resources.GetObject("tsb_CopayList.Image")));
            this.tsb_CopayList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_CopayList.Name = "tsb_CopayList";
            this.tsb_CopayList.Size = new System.Drawing.Size(49, 50);
            this.tsb_CopayList.Tag = "Copay";
            this.tsb_CopayList.Text = "C&opay";
            this.tsb_CopayList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_CopayList.ToolTipText = "Copay";
            this.tsb_CopayList.Visible = false;
            this.tsb_CopayList.Click += new System.EventHandler(this.tsb_CopayList_Click);
            // 
            // tsb_NewAutoDistributeCopay
            // 
            this.tsb_NewAutoDistributeCopay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_NewAutoDistributeCopay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_NewAutoDistributeCopay.Image = ((System.Drawing.Image)(resources.GetObject("tsb_NewAutoDistributeCopay.Image")));
            this.tsb_NewAutoDistributeCopay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_NewAutoDistributeCopay.Name = "tsb_NewAutoDistributeCopay";
            this.tsb_NewAutoDistributeCopay.Size = new System.Drawing.Size(149, 50);
            this.tsb_NewAutoDistributeCopay.Tag = "New Auto Distribute Copay";
            this.tsb_NewAutoDistributeCopay.Text = "&Auto Distribute Copay";
            this.tsb_NewAutoDistributeCopay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_NewAutoDistributeCopay.ToolTipText = "Auto Distribute Copay";
            this.tsb_NewAutoDistributeCopay.Click += new System.EventHandler(this.tsb_NewAutoDistributeCopay_Click);
            // 
            // tls_btnPatAcct
            // 
            this.tls_btnPatAcct.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnPatAcct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnPatAcct.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnPatAcct.Image")));
            this.tls_btnPatAcct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnPatAcct.Name = "tls_btnPatAcct";
            this.tls_btnPatAcct.Size = new System.Drawing.Size(71, 50);
            this.tls_btnPatAcct.Tag = "Patient Account";
            this.tls_btnPatAcct.Text = "&Pat. Acct.";
            this.tls_btnPatAcct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnPatAcct.ToolTipText = "Patient Account";
            this.tls_btnPatAcct.Click += new System.EventHandler(this.tls_btnPatAcct_Click);
            // 
            // tsb_PaymentPatient
            // 
            this.tsb_PaymentPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PaymentPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PaymentPatient.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PaymentPatient.Image")));
            this.tsb_PaymentPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PaymentPatient.Name = "tsb_PaymentPatient";
            this.tsb_PaymentPatient.Size = new System.Drawing.Size(114, 50);
            this.tsb_PaymentPatient.Tag = "Patient Payment";
            this.tsb_PaymentPatient.Text = "Patient Pa&yment";
            this.tsb_PaymentPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PaymentPatient.ToolTipText = "Patient Payment";
            this.tsb_PaymentPatient.Click += new System.EventHandler(this.tsb_PaymentPatient_Click);
            // 
            // tsb_AutoDistributeCopay
            // 
            this.tsb_AutoDistributeCopay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_AutoDistributeCopay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_AutoDistributeCopay.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AutoDistributeCopay.Image")));
            this.tsb_AutoDistributeCopay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AutoDistributeCopay.Name = "tsb_AutoDistributeCopay";
            this.tsb_AutoDistributeCopay.Size = new System.Drawing.Size(149, 50);
            this.tsb_AutoDistributeCopay.Tag = "Auto Distribute Copay";
            this.tsb_AutoDistributeCopay.Text = "&Auto Distribute Copay";
            this.tsb_AutoDistributeCopay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_AutoDistributeCopay.ToolTipText = "Auto Distribute Copay";
            this.tsb_AutoDistributeCopay.Visible = false;
            this.tsb_AutoDistributeCopay.Click += new System.EventHandler(this.tsb_AutoDistributeCopay_Click);
            // 
            // tsb_SaveAutoCopay
            // 
            this.tsb_SaveAutoCopay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SaveAutoCopay.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveAutoCopay.Image")));
            this.tsb_SaveAutoCopay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveAutoCopay.Name = "tsb_SaveAutoCopay";
            this.tsb_SaveAutoCopay.Size = new System.Drawing.Size(40, 50);
            this.tsb_SaveAutoCopay.Tag = "Save";
            this.tsb_SaveAutoCopay.Text = "&Save";
            this.tsb_SaveAutoCopay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SaveAutoCopay.ToolTipText = "Save";
            this.tsb_SaveAutoCopay.Visible = false;
            this.tsb_SaveAutoCopay.Click += new System.EventHandler(this.tsb_SaveAutoCopay_Click);
            // 
            // tsb_Next
            // 
            this.tsb_Next.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Next.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Next.Image")));
            this.tsb_Next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Next.Name = "tsb_Next";
            this.tsb_Next.Size = new System.Drawing.Size(39, 50);
            this.tsb_Next.Tag = "Next";
            this.tsb_Next.Text = "Next";
            this.tsb_Next.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Next.ToolTipText = "Next";
            this.tsb_Next.Click += new System.EventHandler(this.tsb_Next_Click);
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Refresh.Image = global::gloBilling.Properties.Resources.Ico_Refresh;
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tsb_Refresh.Tag = "Refresh";
            this.tsb_Refresh.Text = "&Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refresh.ToolTipText = "Refresh";
            this.tsb_Refresh.Click += new System.EventHandler(this.tsb_Refresh_Click);
            // 
            // tsb_close
            // 
            this.tsb_close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_close.Image")));
            this.tsb_close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_close.Name = "tsb_close";
            this.tsb_close.Size = new System.Drawing.Size(43, 50);
            this.tsb_close.Tag = "Cancel";
            this.tsb_close.Text = "&Close";
            this.tsb_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_close.Click += new System.EventHandler(this.tsb_close_Click);
            // 
            // pnlText
            // 
            this.pnlText.Controls.Add(this.rb_ShowOneByOne);
            this.pnlText.Controls.Add(this.rb_ShowAll);
            this.pnlText.Controls.Add(this.dtStartDate);
            this.pnlText.Controls.Add(this.dtEndDate);
            this.pnlText.Controls.Add(this.label14);
            this.pnlText.Controls.Add(this.lbl_datefilter);
            this.pnlText.Controls.Add(this.label3);
            this.pnlText.Controls.Add(this.label2);
            this.pnlText.Controls.Add(this.label1);
            this.pnlText.Controls.Add(this.label59);
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlText.Location = new System.Drawing.Point(0, 54);
            this.pnlText.Name = "pnlText";
            this.pnlText.Padding = new System.Windows.Forms.Padding(3);
            this.pnlText.Size = new System.Drawing.Size(1274, 39);
            this.pnlText.TabIndex = 0;
            // 
            // rb_ShowOneByOne
            // 
            this.rb_ShowOneByOne.AutoSize = true;
            this.rb_ShowOneByOne.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_ShowOneByOne.Location = new System.Drawing.Point(533, 10);
            this.rb_ShowOneByOne.Name = "rb_ShowOneByOne";
            this.rb_ShowOneByOne.Size = new System.Drawing.Size(127, 18);
            this.rb_ShowOneByOne.TabIndex = 240;
            this.rb_ShowOneByOne.Text = "Show One by One";
            this.rb_ShowOneByOne.UseVisualStyleBackColor = true;
            this.rb_ShowOneByOne.CheckedChanged += new System.EventHandler(this.rb_ShowOneByOne_CheckedChanged);
            // 
            // rb_ShowAll
            // 
            this.rb_ShowAll.AutoSize = true;
            this.rb_ShowAll.Checked = true;
            this.rb_ShowAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_ShowAll.Location = new System.Drawing.Point(441, 10);
            this.rb_ShowAll.Name = "rb_ShowAll";
            this.rb_ShowAll.Size = new System.Drawing.Size(72, 18);
            this.rb_ShowAll.TabIndex = 239;
            this.rb_ShowAll.TabStop = true;
            this.rb_ShowAll.Text = "Show All";
            this.rb_ShowAll.UseVisualStyleBackColor = true;
            this.rb_ShowAll.CheckedChanged += new System.EventHandler(this.rb_ShowAll_CheckedChanged);
            // 
            // dtStartDate
            // 
            this.dtStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStartDate.Location = new System.Drawing.Point(98, 8);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(105, 22);
            this.dtStartDate.TabIndex = 0;
            // 
            // dtEndDate
            // 
            this.dtEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndDate.Location = new System.Drawing.Point(283, 8);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(105, 22);
            this.dtEndDate.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(215, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 14);
            this.label14.TabIndex = 238;
            this.label14.Text = "End Date :";
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(23, 12);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(72, 14);
            this.lbl_datefilter.TabIndex = 237;
            this.lbl_datefilter.Text = "Start Date :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1266, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1266, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(1270, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 33);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 33);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 93);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(1274, 28);
            this.panel2.TabIndex = 116;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.lbl_ListName);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1268, 25);
            this.panel3.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(1267, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 23);
            this.label8.TabIndex = 116;
            this.label8.Text = "label8";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(0, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 23);
            this.label7.TabIndex = 115;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1268, 1);
            this.label6.TabIndex = 114;
            this.label6.Text = "label1";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1268, 1);
            this.label9.TabIndex = 5;
            this.label9.Text = "label1";
            // 
            // lbl_ListName
            // 
            this.lbl_ListName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ListName.Location = new System.Drawing.Point(0, 0);
            this.lbl_ListName.Name = "lbl_ListName";
            this.lbl_ListName.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lbl_ListName.Size = new System.Drawing.Size(1128, 28);
            this.lbl_ListName.TabIndex = 113;
            this.lbl_ListName.Text = " Copay Reserves Distribution Work List";
            // 
            // pnl_CopayList
            // 
            this.pnl_CopayList.Controls.Add(this.label12);
            this.pnl_CopayList.Controls.Add(this.label11);
            this.pnl_CopayList.Controls.Add(this.label10);
            this.pnl_CopayList.Controls.Add(this.label5);
            this.pnl_CopayList.Controls.Add(this.C1CopayList);
            this.pnl_CopayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_CopayList.Location = new System.Drawing.Point(0, 121);
            this.pnl_CopayList.Name = "pnl_CopayList";
            this.pnl_CopayList.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnl_CopayList.Size = new System.Drawing.Size(1274, 615);
            this.pnl_CopayList.TabIndex = 1;
            this.pnl_CopayList.VisibleChanged += new System.EventHandler(this.Btn_Visiblity);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(1270, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 610);
            this.label12.TabIndex = 118;
            this.label12.Text = "label12";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(3, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 610);
            this.label11.TabIndex = 117;
            this.label11.Text = "label11";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 611);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1268, 1);
            this.label10.TabIndex = 116;
            this.label10.Text = "label1";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1268, 1);
            this.label5.TabIndex = 115;
            this.label5.Text = "label5";
            // 
            // C1CopayList
            // 
            this.C1CopayList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1CopayList.ColumnInfo = resources.GetString("C1CopayList.ColumnInfo");
            this.C1CopayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1CopayList.Location = new System.Drawing.Point(3, 0);
            this.C1CopayList.Name = "C1CopayList";
            this.C1CopayList.Rows.DefaultSize = 21;
            this.C1CopayList.ShowCellLabels = true;
            this.C1CopayList.Size = new System.Drawing.Size(1268, 612);
            this.C1CopayList.StyleInfo = resources.GetString("C1CopayList.StyleInfo");
            this.C1CopayList.TabIndex = 0;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tls_CopayList);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1274, 54);
            this.panel4.TabIndex = 2;
            // 
            // pnl_AutoCopayDistributionList
            // 
            this.pnl_AutoCopayDistributionList.Controls.Add(this.C1AutoCopayList);
            this.pnl_AutoCopayDistributionList.Controls.Add(this.label13);
            this.pnl_AutoCopayDistributionList.Controls.Add(this.label15);
            this.pnl_AutoCopayDistributionList.Controls.Add(this.label16);
            this.pnl_AutoCopayDistributionList.Controls.Add(this.label17);
            this.pnl_AutoCopayDistributionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_AutoCopayDistributionList.Location = new System.Drawing.Point(0, 121);
            this.pnl_AutoCopayDistributionList.Name = "pnl_AutoCopayDistributionList";
            this.pnl_AutoCopayDistributionList.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnl_AutoCopayDistributionList.Size = new System.Drawing.Size(1274, 615);
            this.pnl_AutoCopayDistributionList.TabIndex = 117;
            this.pnl_AutoCopayDistributionList.Visible = false;
            this.pnl_AutoCopayDistributionList.VisibleChanged += new System.EventHandler(this.Btn_Visiblity);
            // 
            // C1AutoCopayList
            // 
            this.C1AutoCopayList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.C1AutoCopayList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.C1AutoCopayList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1AutoCopayList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1AutoCopayList.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.C1AutoCopayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1AutoCopayList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1AutoCopayList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1AutoCopayList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.C1AutoCopayList.Location = new System.Drawing.Point(4, 1);
            this.C1AutoCopayList.Name = "C1AutoCopayList";
            this.C1AutoCopayList.Rows.Count = 1;
            this.C1AutoCopayList.Rows.DefaultSize = 19;
            this.C1AutoCopayList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.C1AutoCopayList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.C1AutoCopayList.Size = new System.Drawing.Size(1266, 610);
            this.C1AutoCopayList.StyleInfo = resources.GetString("C1AutoCopayList.StyleInfo");
            this.C1AutoCopayList.TabIndex = 119;
            this.C1AutoCopayList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1AutoCopayList_BeforeEdit);
            this.C1AutoCopayList.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1AutoCopayList_CellButtonClick);
            this.C1AutoCopayList.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.C1AutoCopayList_KeyPressEdit);
            this.C1AutoCopayList.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1AutoCopayList_CellChanged);
            this.C1AutoCopayList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.C1AutoCopayList_KeyUp);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(1270, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 610);
            this.label13.TabIndex = 118;
            this.label13.Text = "label13";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Location = new System.Drawing.Point(3, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 610);
            this.label15.TabIndex = 117;
            this.label15.Text = "label15";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 611);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1268, 1);
            this.label16.TabIndex = 116;
            this.label16.Text = "label1";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1268, 1);
            this.label17.TabIndex = 115;
            this.label17.Text = "label17";
            // 
            // pnlReserveDetails
            // 
            this.pnlReserveDetails.AutoScroll = true;
            this.pnlReserveDetails.AutoSize = true;
            this.pnlReserveDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlReserveDetails.Controls.Add(this.pnlRDGrid);
            this.pnlReserveDetails.Controls.Add(this.panel1);
            this.pnlReserveDetails.Controls.Add(this.pnlReserveDetailsToolStrip);
            this.pnlReserveDetails.Controls.Add(this.pnlDxHeader);
            this.pnlReserveDetails.Controls.Add(this.label76);
            this.pnlReserveDetails.Controls.Add(this.label77);
            this.pnlReserveDetails.Controls.Add(this.label78);
            this.pnlReserveDetails.Location = new System.Drawing.Point(266, 221);
            this.pnlReserveDetails.Name = "pnlReserveDetails";
            this.pnlReserveDetails.Size = new System.Drawing.Size(758, 290);
            this.pnlReserveDetails.TabIndex = 119;
            // 
            // pnlRDGrid
            // 
            this.pnlRDGrid.BackColor = System.Drawing.Color.Transparent;
            this.pnlRDGrid.Controls.Add(this.c1Reserve);
            this.pnlRDGrid.Controls.Add(this.label80);
            this.pnlRDGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRDGrid.Location = new System.Drawing.Point(1, 79);
            this.pnlRDGrid.Name = "pnlRDGrid";
            this.pnlRDGrid.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlRDGrid.Size = new System.Drawing.Size(756, 180);
            this.pnlRDGrid.TabIndex = 3;
            // 
            // c1Reserve
            // 
            this.c1Reserve.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Reserve.AllowEditing = false;
            this.c1Reserve.AutoGenerateColumns = false;
            this.c1Reserve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Reserve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Reserve.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Reserve.ColumnInfo = "1,1,0,0,0,105,Columns:";
            this.c1Reserve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Reserve.ExtendLastCol = true;
            this.c1Reserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Reserve.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1Reserve.Location = new System.Drawing.Point(0, 4);
            this.c1Reserve.Name = "c1Reserve";
            this.c1Reserve.Rows.Count = 1;
            this.c1Reserve.Rows.DefaultSize = 21;
            this.c1Reserve.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.ScrollByRowColumn;
            this.c1Reserve.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Reserve.Size = new System.Drawing.Size(756, 176);
            this.c1Reserve.StyleInfo = resources.GetString("c1Reserve.StyleInfo");
            this.c1Reserve.TabIndex = 12;
            this.c1Reserve.TabStop = false;
            this.c1Reserve.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Reserve_MouseMove);
            // 
            // label80
            // 
            this.label80.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label80.Dock = System.Windows.Forms.DockStyle.Top;
            this.label80.Location = new System.Drawing.Point(0, 3);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(756, 1);
            this.label80.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(1, 259);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(756, 30);
            this.panel1.TabIndex = 15;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.pictureBox1);
            this.pnlMain.Controls.Add(this.label20);
            this.pnlMain.Controls.Add(this.label33);
            this.pnlMain.Controls.Add(this.label21);
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.statusStrip1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(750, 24);
            this.pnlMain.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.pictureBox1.Location = new System.Drawing.Point(732, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(14, 18);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 219;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Location = new System.Drawing.Point(1, 23);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(748, 1);
            this.label20.TabIndex = 218;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Location = new System.Drawing.Point(1, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(748, 1);
            this.label33.TabIndex = 217;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Right;
            this.label21.Location = new System.Drawing.Point(749, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 24);
            this.label21.TabIndex = 216;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 24);
            this.label22.TabIndex = 215;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.statusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(750, 24);
            this.statusStrip1.TabIndex = 214;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Image = global::gloBilling.Properties.Resources.HoldClaim;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(180, 19);
            this.toolStripStatusLabel1.Text = " Reserve Available for Use";
            // 
            // pnlReserveDetailsToolStrip
            // 
            this.pnlReserveDetailsToolStrip.Controls.Add(this.tls_DXCPT);
            this.pnlReserveDetailsToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReserveDetailsToolStrip.Location = new System.Drawing.Point(1, 28);
            this.pnlReserveDetailsToolStrip.Name = "pnlReserveDetailsToolStrip";
            this.pnlReserveDetailsToolStrip.Size = new System.Drawing.Size(756, 51);
            this.pnlReserveDetailsToolStrip.TabIndex = 13;
            this.pnlReserveDetailsToolStrip.TabStop = true;
            // 
            // tls_DXCPT
            // 
            this.tls_DXCPT.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_DXCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_DXCPT.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_DXCPT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_RDClose});
            this.tls_DXCPT.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_DXCPT.Location = new System.Drawing.Point(0, 0);
            this.tls_DXCPT.Name = "tls_DXCPT";
            this.tls_DXCPT.Size = new System.Drawing.Size(756, 53);
            this.tls_DXCPT.TabIndex = 0;
            this.tls_DXCPT.TabStop = true;
            this.tls_DXCPT.Text = "toolStrip1";
            // 
            // tlb_RDClose
            // 
            this.tlb_RDClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_RDClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_RDClose.Image = ((System.Drawing.Image)(resources.GetObject("tlb_RDClose.Image")));
            this.tlb_RDClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_RDClose.Name = "tlb_RDClose";
            this.tlb_RDClose.Size = new System.Drawing.Size(43, 50);
            this.tlb_RDClose.Tag = "Cancel";
            this.tlb_RDClose.Text = "&Close";
            this.tlb_RDClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_RDClose.ToolTipText = "Close";
            this.tlb_RDClose.Click += new System.EventHandler(this.tlb_RDClose_Click_1);
            // 
            // pnlDxHeader
            // 
            this.pnlDxHeader.Controls.Add(this.pnlRDTop);
            this.pnlDxHeader.Controls.Add(this.label75);
            this.pnlDxHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDxHeader.Location = new System.Drawing.Point(1, 0);
            this.pnlDxHeader.Name = "pnlDxHeader";
            this.pnlDxHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlDxHeader.Size = new System.Drawing.Size(756, 28);
            this.pnlDxHeader.TabIndex = 14;
            // 
            // pnlRDTop
            // 
            this.pnlRDTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlRDTop.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.pnlRDTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRDTop.Controls.Add(this.label83);
            this.pnlRDTop.Controls.Add(this.btnRDClose);
            this.pnlRDTop.Controls.Add(this.imgCPTDX);
            this.pnlRDTop.Controls.Add(this.lblExamDxCPT);
            this.pnlRDTop.Controls.Add(this.label79);
            this.pnlRDTop.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pnlRDTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRDTop.Location = new System.Drawing.Point(0, 0);
            this.pnlRDTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlRDTop.Name = "pnlRDTop";
            this.pnlRDTop.Size = new System.Drawing.Size(756, 25);
            this.pnlRDTop.TabIndex = 8;
            this.pnlRDTop.Tag = "pnlToolStrip";
            // 
            // label83
            // 
            this.label83.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label83.Dock = System.Windows.Forms.DockStyle.Top;
            this.label83.Location = new System.Drawing.Point(0, 0);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(735, 1);
            this.label83.TabIndex = 12;
            // 
            // btnRDClose
            // 
            this.btnRDClose.BackColor = System.Drawing.Color.Transparent;
            this.btnRDClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRDClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRDClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRDClose.FlatAppearance.BorderSize = 0;
            this.btnRDClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRDClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRDClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRDClose.Image = ((System.Drawing.Image)(resources.GetObject("btnRDClose.Image")));
            this.btnRDClose.Location = new System.Drawing.Point(735, 0);
            this.btnRDClose.Name = "btnRDClose";
            this.btnRDClose.Size = new System.Drawing.Size(21, 24);
            this.btnRDClose.TabIndex = 6;
            this.btnRDClose.TabStop = false;
            this.btnRDClose.UseVisualStyleBackColor = false;
            this.btnRDClose.Click += new System.EventHandler(this.btnRDClose_Click_1);
            // 
            // imgCPTDX
            // 
            this.imgCPTDX.BackColor = System.Drawing.Color.Transparent;
            this.imgCPTDX.Image = ((System.Drawing.Image)(resources.GetObject("imgCPTDX.Image")));
            this.imgCPTDX.Location = new System.Drawing.Point(3, 3);
            this.imgCPTDX.Name = "imgCPTDX";
            this.imgCPTDX.Size = new System.Drawing.Size(24, 21);
            this.imgCPTDX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgCPTDX.TabIndex = 3;
            this.imgCPTDX.TabStop = false;
            // 
            // lblExamDxCPT
            // 
            this.lblExamDxCPT.AutoSize = true;
            this.lblExamDxCPT.BackColor = System.Drawing.Color.Transparent;
            this.lblExamDxCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExamDxCPT.ForeColor = System.Drawing.Color.White;
            this.lblExamDxCPT.Location = new System.Drawing.Point(28, 6);
            this.lblExamDxCPT.Name = "lblExamDxCPT";
            this.lblExamDxCPT.Size = new System.Drawing.Size(100, 14);
            this.lblExamDxCPT.TabIndex = 5;
            this.lblExamDxCPT.Text = "Reserve Details";
            this.lblExamDxCPT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label79
            // 
            this.label79.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label79.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label79.Location = new System.Drawing.Point(0, 24);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(756, 1);
            this.label79.TabIndex = 11;
            // 
            // label75
            // 
            this.label75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label75.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label75.Location = new System.Drawing.Point(0, 0);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(756, 25);
            this.label75.TabIndex = 9;
            // 
            // label76
            // 
            this.label76.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label76.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label76.Location = new System.Drawing.Point(1, 289);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(756, 1);
            this.label76.TabIndex = 10;
            // 
            // label77
            // 
            this.label77.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label77.Dock = System.Windows.Forms.DockStyle.Left;
            this.label77.Location = new System.Drawing.Point(0, 0);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(1, 290);
            this.label77.TabIndex = 11;
            // 
            // label78
            // 
            this.label78.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label78.Dock = System.Windows.Forms.DockStyle.Right;
            this.label78.Location = new System.Drawing.Point(757, 0);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(1, 290);
            this.label78.TabIndex = 12;
            // 
            // frmCopayDistributionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1274, 736);
            this.Controls.Add(this.pnl_AutoCopayDistributionList);
            this.Controls.Add(this.pnl_CopayList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pnlReserveDetails);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCopayDistributionList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Copay Distribution List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Deactivate += new System.EventHandler(this.frmCopayDistributionList_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCopayDistributionList_FormClosed);
            this.Load += new System.EventHandler(this.frmCopayDistributionList_Load);
            this.Shown += new System.EventHandler(this.frmCopayDistributionList_Shown);
            this.tls_CopayList.ResumeLayout(false);
            this.tls_CopayList.PerformLayout();
            this.pnlText.ResumeLayout(false);
            this.pnlText.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnl_CopayList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1CopayList)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnl_AutoCopayDistributionList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1AutoCopayList)).EndInit();
            this.pnlReserveDetails.ResumeLayout(false);
            this.pnlRDGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Reserve)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlReserveDetailsToolStrip.ResumeLayout(false);
            this.pnlReserveDetailsToolStrip.PerformLayout();
            this.tls_DXCPT.ResumeLayout(false);
            this.tls_DXCPT.PerformLayout();
            this.pnlDxHeader.ResumeLayout(false);
            this.pnlRDTop.ResumeLayout(false);
            this.pnlRDTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCPTDX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_CopayList;
        internal System.Windows.Forms.ToolStripButton tsb_Refresh;
        private System.Windows.Forms.ToolStripButton tsb_close;
        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label lbl_ListName;
        private C1.Win.C1FlexGrid.C1FlexGrid C1CopayList;
        private System.Windows.Forms.Panel pnl_CopayList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_datefilter;
        internal System.Windows.Forms.DateTimePicker dtStartDate;
        internal System.Windows.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.ToolStripButton tls_btnPatAcct;
        private System.Windows.Forms.ToolStripButton tsb_PaymentPatient;
        private System.Windows.Forms.Panel pnl_AutoCopayDistributionList;
        private C1.Win.C1FlexGrid.C1FlexGrid C1AutoCopayList;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ToolStripButton tsb_NewAutoDistributeCopay;
        private System.Windows.Forms.ToolStripButton tsb_CopayList;
        private System.Windows.Forms.ToolStripButton tsb_AutoDistributeCopay;
        internal System.Windows.Forms.ToolStripButton tsb_SaveAutoCopay;
        internal System.Windows.Forms.ToolStripButton tsb_Next;
        private System.Windows.Forms.RadioButton rb_ShowOneByOne;
        private System.Windows.Forms.RadioButton rb_ShowAll;
        private System.Windows.Forms.Panel pnlReserveDetails;
        private System.Windows.Forms.Panel pnlRDGrid;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Panel pnlReserveDetailsToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_DXCPT;
        private System.Windows.Forms.ToolStripButton tlb_RDClose;
        private System.Windows.Forms.Panel pnlDxHeader;
        private System.Windows.Forms.Panel pnlRDTop;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Button btnRDClose;
        private System.Windows.Forms.PictureBox imgCPTDX;
        private System.Windows.Forms.Label lblExamDxCPT;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Reserve;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}