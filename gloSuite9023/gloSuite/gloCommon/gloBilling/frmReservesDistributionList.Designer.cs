namespace gloBilling
{
    partial class frmReservesDistributionList
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
                    if (dtEndDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtEndDate);

                        }
                        catch
                        {
                        }


                        dtEndDate.Dispose();
                        dtEndDate = null;
                    }
                }
                catch
                {
                }

                try
                {
                    if (dtStartDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtStartDate);

                        }
                        catch
                        {
                        }


                        dtStartDate.Dispose();
                        dtStartDate = null;
                    }
                }
                catch
                {
                }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReservesDistributionList));
            this.tls_ReserveList = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnPatAcct = new System.Windows.Forms.ToolStripButton();
            this.tsb_PaymentPatient = new System.Windows.Forms.ToolStripButton();
            this.tlsAutoDistributeReserve = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_close = new System.Windows.Forms.ToolStripButton();
            this.pnlText = new System.Windows.Forms.Panel();
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
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.C1ReserveList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.tls_ReserveList.SuspendLayout();
            this.pnlText.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1ReserveList)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_ReserveList
            // 
            this.tls_ReserveList.BackColor = System.Drawing.Color.Transparent;
            this.tls_ReserveList.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_ReserveList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_ReserveList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_ReserveList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_ReserveList.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_ReserveList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnPatAcct,
            this.tsb_PaymentPatient,
            this.tlsAutoDistributeReserve,
            this.tsb_Refresh,
            this.tsb_close});
            this.tls_ReserveList.Location = new System.Drawing.Point(0, 0);
            this.tls_ReserveList.Name = "tls_ReserveList";
            this.tls_ReserveList.Size = new System.Drawing.Size(1274, 53);
            this.tls_ReserveList.TabIndex = 0;
            this.tls_ReserveList.TabStop = true;
            this.tls_ReserveList.Text = "toolStrip1";
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
            this.tls_btnPatAcct.Visible = false;
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
            // tlsAutoDistributeReserve
            // 
            this.tlsAutoDistributeReserve.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsAutoDistributeReserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsAutoDistributeReserve.Image = ((System.Drawing.Image)(resources.GetObject("tlsAutoDistributeReserve.Image")));
            this.tlsAutoDistributeReserve.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsAutoDistributeReserve.Name = "tlsAutoDistributeReserve";
            this.tlsAutoDistributeReserve.Size = new System.Drawing.Size(165, 50);
            this.tlsAutoDistributeReserve.Tag = "Auto Distribute Reserves";
            this.tlsAutoDistributeReserve.Text = "&Auto Distribute Reserves";
            this.tlsAutoDistributeReserve.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsAutoDistributeReserve.ToolTipText = "Auto Distribute Reserves";
            this.tlsAutoDistributeReserve.Click += new System.EventHandler(this.tlsAutoDistributeReserve_Click);
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
            // dtStartDate
            // 
            this.dtStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStartDate.Location = new System.Drawing.Point(98, 9);
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
            this.dtEndDate.Location = new System.Drawing.Point(283, 9);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(105, 22);
            this.dtEndDate.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(215, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 14);
            this.label14.TabIndex = 238;
            this.label14.Text = "End Date :";
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(23, 13);
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
            this.panel3.Controls.Add(this.label4);
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
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(8, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(1128, 28);
            this.label4.TabIndex = 113;
            this.label4.Text = "Reserves Distribution Work List";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.C1ReserveList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 121);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(1274, 615);
            this.panel1.TabIndex = 1;
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
            // C1ReserveList
            // 
            this.C1ReserveList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1ReserveList.ColumnInfo = resources.GetString("C1ReserveList.ColumnInfo");
            this.C1ReserveList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1ReserveList.Location = new System.Drawing.Point(3, 0);
            this.C1ReserveList.Name = "C1ReserveList";
            this.C1ReserveList.Rows.DefaultSize = 21;
            this.C1ReserveList.ShowCellLabels = true;
            this.C1ReserveList.Size = new System.Drawing.Size(1268, 612);
            this.C1ReserveList.StyleInfo = resources.GetString("C1ReserveList.StyleInfo");
            this.C1ReserveList.TabIndex = 0;
            this.C1ReserveList.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1ReserveList_AfterEdit);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tls_ReserveList);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1274, 54);
            this.panel4.TabIndex = 2;
            // 
            // frmReservesDistributionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1274, 736);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReservesDistributionList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reserves Distribution List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReserveDistributionList_Load);
            this.Shown += new System.EventHandler(this.frmReserveDistributionList_Shown);
            this.tls_ReserveList.ResumeLayout(false);
            this.tls_ReserveList.PerformLayout();
            this.pnlText.ResumeLayout(false);
            this.pnlText.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1ReserveList)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_ReserveList;
        internal System.Windows.Forms.ToolStripButton tsb_Refresh;
        private System.Windows.Forms.ToolStripButton tsb_close;
        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1FlexGrid.C1FlexGrid C1ReserveList;
        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.ToolStripButton tlsAutoDistributeReserve;
    }
}
