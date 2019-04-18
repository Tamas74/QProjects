namespace gloBilling
{
    partial class frmCleargagePaymentDistributionList
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = {  };
            System.Windows.Forms.Control[] cntControls = { };

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCleargagePaymentDistributionList));
            this.tls_CopayList = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_AutoDistributeCleargagePayment = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Next = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_close = new System.Windows.Forms.ToolStripButton();
            this.pnlText = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.rb_ShowOneByOne = new System.Windows.Forms.RadioButton();
            this.rb_ShowAll = new System.Windows.Forms.RadioButton();
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
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnl_CleargagePaymentDistributionList = new System.Windows.Forms.Panel();
            this.C1Cleargage = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tls_CopayList.SuspendLayout();
            this.pnlText.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnl_CleargagePaymentDistributionList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Cleargage)).BeginInit();
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
            this.tsb_AutoDistributeCleargagePayment,
            this.tsb_Save,
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
            // tsb_AutoDistributeCleargagePayment
            // 
            this.tsb_AutoDistributeCleargagePayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_AutoDistributeCleargagePayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_AutoDistributeCleargagePayment.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AutoDistributeCleargagePayment.Image")));
            this.tsb_AutoDistributeCleargagePayment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AutoDistributeCleargagePayment.Name = "tsb_AutoDistributeCleargagePayment";
            this.tsb_AutoDistributeCleargagePayment.Size = new System.Drawing.Size(165, 50);
            this.tsb_AutoDistributeCleargagePayment.Tag = "Auto Distribute Cleargage Payment";
            this.tsb_AutoDistributeCleargagePayment.Text = "&Auto Distribute Payment";
            this.tsb_AutoDistributeCleargagePayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_AutoDistributeCleargagePayment.ToolTipText = "Auto Distribute Cleargage Payment";
            this.tsb_AutoDistributeCleargagePayment.Click += new System.EventHandler(this.tsb_AutoDistributeCleargagePayment_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
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
            this.tsb_Next.Visible = false;
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
            this.pnlText.Controls.Add(this.label4);
            this.pnlText.Controls.Add(this.rb_ShowOneByOne);
            this.pnlText.Controls.Add(this.rb_ShowAll);
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
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 4);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.label4.Size = new System.Drawing.Size(309, 31);
            this.label4.TabIndex = 241;
            this.label4.Text = " Cleargage Payment Distribution Work List";
            // 
            // rb_ShowOneByOne
            // 
            this.rb_ShowOneByOne.AutoSize = true;
            this.rb_ShowOneByOne.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_ShowOneByOne.Location = new System.Drawing.Point(431, 11);
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
            this.rb_ShowAll.Location = new System.Drawing.Point(339, 11);
            this.rb_ShowAll.Name = "rb_ShowAll";
            this.rb_ShowAll.Size = new System.Drawing.Size(72, 18);
            this.rb_ShowAll.TabIndex = 239;
            this.rb_ShowAll.TabStop = true;
            this.rb_ShowAll.Text = "Show All";
            this.rb_ShowAll.UseVisualStyleBackColor = true;
            this.rb_ShowAll.CheckedChanged += new System.EventHandler(this.rb_ShowAll_CheckedChanged);
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
            this.panel2.Visible = false;
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
            this.lbl_ListName.Text = " Cleargage Payment Distribution Work List";
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
            // pnl_CleargagePaymentDistributionList
            // 
            this.pnl_CleargagePaymentDistributionList.Controls.Add(this.C1Cleargage);
            this.pnl_CleargagePaymentDistributionList.Controls.Add(this.label13);
            this.pnl_CleargagePaymentDistributionList.Controls.Add(this.label15);
            this.pnl_CleargagePaymentDistributionList.Controls.Add(this.label16);
            this.pnl_CleargagePaymentDistributionList.Controls.Add(this.label17);
            this.pnl_CleargagePaymentDistributionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_CleargagePaymentDistributionList.Location = new System.Drawing.Point(0, 121);
            this.pnl_CleargagePaymentDistributionList.Name = "pnl_CleargagePaymentDistributionList";
            this.pnl_CleargagePaymentDistributionList.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnl_CleargagePaymentDistributionList.Size = new System.Drawing.Size(1274, 615);
            this.pnl_CleargagePaymentDistributionList.TabIndex = 117;
            this.pnl_CleargagePaymentDistributionList.Visible = false;
            // 
            // C1Cleargage
            // 
            this.C1Cleargage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.C1Cleargage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1Cleargage.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1Cleargage.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.C1Cleargage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1Cleargage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1Cleargage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1Cleargage.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.C1Cleargage.Location = new System.Drawing.Point(4, 1);
            this.C1Cleargage.Name = "C1Cleargage";
            this.C1Cleargage.Rows.Count = 1;
            this.C1Cleargage.Rows.DefaultSize = 19;
            this.C1Cleargage.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.C1Cleargage.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.C1Cleargage.Size = new System.Drawing.Size(1266, 610);
            this.C1Cleargage.StyleInfo = resources.GetString("C1Cleargage.StyleInfo");
            this.C1Cleargage.TabIndex = 119;
            this.C1Cleargage.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_BeforeEdit);
            this.C1Cleargage.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.C1Cleargage_KeyPressEdit);
            this.C1Cleargage.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1Cleargage_CellChanged);
            this.C1Cleargage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.C1Cleargage_KeyUp);
            this.C1Cleargage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1Cleargage_MouseMove);
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
            // frmCleargagePaymentDistributionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1274, 736);
            this.Controls.Add(this.pnl_CleargagePaymentDistributionList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCleargagePaymentDistributionList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cleargage Payment Distribution List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCleargagePaymentDistributionList_Load);
            this.tls_CopayList.ResumeLayout(false);
            this.tls_CopayList.PerformLayout();
            this.pnlText.ResumeLayout(false);
            this.pnlText.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnl_CleargagePaymentDistributionList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1Cleargage)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel4;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel pnl_CleargagePaymentDistributionList;
        private C1.Win.C1FlexGrid.C1FlexGrid C1Cleargage;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ToolStripButton tsb_AutoDistributeCleargagePayment;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Next;
        private System.Windows.Forms.RadioButton rb_ShowOneByOne;
        private System.Windows.Forms.RadioButton rb_ShowAll;
        private System.Windows.Forms.Label label4;
    }
}