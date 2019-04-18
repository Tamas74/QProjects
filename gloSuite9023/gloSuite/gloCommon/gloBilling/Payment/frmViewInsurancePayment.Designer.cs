namespace gloBilling.Payment
{
    partial class frmViewInsurancePayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewInsurancePayment));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_ViewRemit = new System.Windows.Forms.ToolStripButton();
            this.tsb_Void = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1LogDetails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCloseDate = new System.Windows.Forms.Label();
            this.lblAlertMessage = new System.Windows.Forms.Label();
            this.lblPaymentTray = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.btnViewReserveRemaining = new System.Windows.Forms.Button();
            this.lblCheckNo = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnViewUsedReserve = new System.Windows.Forms.Button();
            this.lblInsuranceCompany = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblPaymentType = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtPaymentNote = new System.Windows.Forms.TextBox();
            this.lblPaymentAmount = new System.Windows.Forms.Label();
            this.lblRemainingAmount = new System.Windows.Forms.Label();
            this.lblReserveUsed = new System.Windows.Forms.Label();
            this.lblAmountAddedToReserve = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.rbSortByEOB = new System.Windows.Forms.RadioButton();
            this.rbSortByClaim = new System.Windows.Forms.RadioButton();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.pnlVoidNote = new System.Windows.Forms.Panel();
            this.lblVoidDesc = new System.Windows.Forms.Label();
            this.lblVoidNote = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1LogDetails)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlVoidNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(965, 54);
            this.pnlToolStrip.TabIndex = 3;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_ViewRemit,
            this.tsb_Void,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(965, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_ViewRemit
            // 
            this.tsb_ViewRemit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ViewRemit.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ViewRemit.Image")));
            this.tsb_ViewRemit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ViewRemit.Name = "tsb_ViewRemit";
            this.tsb_ViewRemit.Size = new System.Drawing.Size(80, 50);
            this.tsb_ViewRemit.Tag = "Cancel";
            this.tsb_ViewRemit.Text = "View &Remit";
            this.tsb_ViewRemit.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_ViewRemit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ViewRemit.Click += new System.EventHandler(this.tsb_ViewRemit_Click);
            // 
            // tsb_Void
            // 
            this.tsb_Void.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Void.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Void.Image")));
            this.tsb_Void.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Void.Name = "tsb_Void";
            this.tsb_Void.Size = new System.Drawing.Size(96, 50);
            this.tsb_Void.Tag = "Cancel";
            this.tsb_Void.Text = "&Void Payment";
            this.tsb_Void.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Void.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Void.Click += new System.EventHandler(this.tsb_Void_Click);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.c1LogDetails);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 244);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(965, 369);
            this.panel1.TabIndex = 4;
            // 
            // c1LogDetails
            // 
            this.c1LogDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1LogDetails.AllowEditing = false;
            this.c1LogDetails.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1LogDetails.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1LogDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1LogDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1LogDetails.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1LogDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1LogDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1LogDetails.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1LogDetails.Location = new System.Drawing.Point(4, 3);
            this.c1LogDetails.Name = "c1LogDetails";
            this.c1LogDetails.Rows.Count = 1;
            this.c1LogDetails.Rows.DefaultSize = 19;
            this.c1LogDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1LogDetails.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1LogDetails.Size = new System.Drawing.Size(957, 362);
            this.c1LogDetails.StyleInfo = resources.GetString("c1LogDetails.StyleInfo");
            this.c1LogDetails.TabIndex = 224;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 362);
            this.label5.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(961, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 362);
            this.label6.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 365);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(959, 1);
            this.label7.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(959, 1);
            this.label8.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(959, 2);
            this.label9.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 54);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(965, 32);
            this.panel5.TabIndex = 229;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lblCloseDate);
            this.panel2.Controls.Add(this.lblAlertMessage);
            this.panel2.Controls.Add(this.lblPaymentTray);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Controls.Add(this.label90);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(959, 26);
            this.panel2.TabIndex = 0;
            // 
            // lblCloseDate
            // 
            this.lblCloseDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseDate.AutoSize = true;
            this.lblCloseDate.BackColor = System.Drawing.Color.Transparent;
            this.lblCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCloseDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCloseDate.Location = new System.Drawing.Point(449, 6);
            this.lblCloseDate.Name = "lblCloseDate";
            this.lblCloseDate.Size = new System.Drawing.Size(0, 14);
            this.lblCloseDate.TabIndex = 214;
            this.lblCloseDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAlertMessage
            // 
            this.lblAlertMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlertMessage.AutoSize = true;
            this.lblAlertMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblAlertMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertMessage.ForeColor = System.Drawing.Color.Red;
            this.lblAlertMessage.Location = new System.Drawing.Point(14, 6);
            this.lblAlertMessage.Name = "lblAlertMessage";
            this.lblAlertMessage.Size = new System.Drawing.Size(0, 14);
            this.lblAlertMessage.TabIndex = 213;
            // 
            // lblPaymentTray
            // 
            this.lblPaymentTray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaymentTray.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPaymentTray.Location = new System.Drawing.Point(673, 6);
            this.lblPaymentTray.Name = "lblPaymentTray";
            this.lblPaymentTray.Size = new System.Drawing.Size(281, 14);
            this.lblPaymentTray.TabIndex = 212;
            this.lblPaymentTray.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            this.label42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(370, 6);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(73, 14);
            this.label42.TabIndex = 208;
            this.label42.Text = "Close Date :";
            // 
            // label90
            // 
            this.label90.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label90.AutoSize = true;
            this.label90.BackColor = System.Drawing.Color.Transparent;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label90.Location = new System.Drawing.Point(579, 6);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(91, 14);
            this.label90.TabIndex = 210;
            this.label90.Text = "Payment Tray :";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(958, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 24);
            this.label1.TabIndex = 205;
            this.label1.Text = "Close Date :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 24);
            this.label2.TabIndex = 204;
            this.label2.Text = "Close Date :";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Location = new System.Drawing.Point(0, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(959, 1);
            this.label21.TabIndex = 203;
            this.label21.Text = "Close Date :";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(959, 1);
            this.label22.TabIndex = 202;
            this.label22.Text = "Close Date :";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Location = new System.Drawing.Point(36, 71);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(101, 14);
            this.label40.TabIndex = 7;
            this.label40.Tag = "";
            this.label40.Text = "Check# / Ref.# :";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnViewReserveRemaining
            // 
            this.btnViewReserveRemaining.AutoEllipsis = true;
            this.btnViewReserveRemaining.BackColor = System.Drawing.Color.Transparent;
            this.btnViewReserveRemaining.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnViewReserveRemaining.BackgroundImage")));
            this.btnViewReserveRemaining.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnViewReserveRemaining.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewReserveRemaining.Image = ((System.Drawing.Image)(resources.GetObject("btnViewReserveRemaining.Image")));
            this.btnViewReserveRemaining.Location = new System.Drawing.Point(587, 94);
            this.btnViewReserveRemaining.Name = "btnViewReserveRemaining";
            this.btnViewReserveRemaining.Size = new System.Drawing.Size(22, 22);
            this.btnViewReserveRemaining.TabIndex = 5;
            this.btnViewReserveRemaining.TabStop = false;
            this.toolTip1.SetToolTip(this.btnViewReserveRemaining, "View To Reserves");
            this.btnViewReserveRemaining.UseVisualStyleBackColor = false;
            this.btnViewReserveRemaining.Click += new System.EventHandler(this.btnViewReserveRemaining_Click);
            // 
            // lblCheckNo
            // 
            this.lblCheckNo.BackColor = System.Drawing.Color.Transparent;
            this.lblCheckNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckNo.Location = new System.Drawing.Point(140, 67);
            this.lblCheckNo.Name = "lblCheckNo";
            this.lblCheckNo.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblCheckNo.Size = new System.Drawing.Size(222, 21);
            this.lblCheckNo.TabIndex = 3;
            this.lblCheckNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(13, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(122, 14);
            this.label12.TabIndex = 7;
            this.label12.Text = "Insurance Company :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(71, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 14);
            this.label11.TabIndex = 7;
            this.label11.Text = "Pay Type :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnViewUsedReserve
            // 
            this.btnViewUsedReserve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewUsedReserve.AutoEllipsis = true;
            this.btnViewUsedReserve.BackColor = System.Drawing.Color.Transparent;
            this.btnViewUsedReserve.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnViewUsedReserve.BackgroundImage")));
            this.btnViewUsedReserve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnViewUsedReserve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewUsedReserve.Image = ((System.Drawing.Image)(resources.GetObject("btnViewUsedReserve.Image")));
            this.btnViewUsedReserve.Location = new System.Drawing.Point(587, 65);
            this.btnViewUsedReserve.Name = "btnViewUsedReserve";
            this.btnViewUsedReserve.Size = new System.Drawing.Size(22, 22);
            this.btnViewUsedReserve.TabIndex = 4;
            this.btnViewUsedReserve.TabStop = false;
            this.toolTip1.SetToolTip(this.btnViewUsedReserve, "View Used Reserves");
            this.btnViewUsedReserve.UseVisualStyleBackColor = false;
            this.btnViewUsedReserve.Click += new System.EventHandler(this.btnViewUsedReserve_Click);
            // 
            // lblInsuranceCompany
            // 
            this.lblInsuranceCompany.BackColor = System.Drawing.Color.Transparent;
            this.lblInsuranceCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuranceCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsuranceCompany.Location = new System.Drawing.Point(140, 9);
            this.lblInsuranceCompany.Name = "lblInsuranceCompany";
            this.lblInsuranceCompany.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblInsuranceCompany.Size = new System.Drawing.Size(259, 21);
            this.lblInsuranceCompany.TabIndex = 3;
            this.lblInsuranceCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label39
            // 
            this.label39.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Location = new System.Drawing.Point(408, 14);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(59, 14);
            this.label39.TabIndex = 3;
            this.label39.Text = "Amount :";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(677, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 205;
            this.label4.Text = "Note :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(397, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 14);
            this.label10.TabIndex = 3;
            this.label10.Text = "Remaining :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(373, 70);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 14);
            this.label16.TabIndex = 3;
            this.label16.Text = "Used Reserves :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPaymentType
            // 
            this.lblPaymentType.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPaymentType.Location = new System.Drawing.Point(140, 39);
            this.lblPaymentType.Name = "lblPaymentType";
            this.lblPaymentType.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblPaymentType.Size = new System.Drawing.Size(127, 21);
            this.lblPaymentType.TabIndex = 15;
            this.lblPaymentType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Location = new System.Drawing.Point(385, 98);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 14);
            this.label18.TabIndex = 3;
            this.label18.Text = "To Reserves :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPaymentNote
            // 
            this.txtPaymentNote.BackColor = System.Drawing.Color.White;
            this.txtPaymentNote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaymentNote.ForeColor = System.Drawing.Color.Black;
            this.txtPaymentNote.Location = new System.Drawing.Point(722, 15);
            this.txtPaymentNote.MaxLength = 255;
            this.txtPaymentNote.Multiline = true;
            this.txtPaymentNote.Name = "txtPaymentNote";
            this.txtPaymentNote.ReadOnly = true;
            this.txtPaymentNote.Size = new System.Drawing.Size(231, 105);
            this.txtPaymentNote.TabIndex = 5;
            this.txtPaymentNote.TabStop = false;
            // 
            // lblPaymentAmount
            // 
            this.lblPaymentAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentAmount.ForeColor = System.Drawing.Color.Green;
            this.lblPaymentAmount.Location = new System.Drawing.Point(470, 12);
            this.lblPaymentAmount.Name = "lblPaymentAmount";
            this.lblPaymentAmount.Size = new System.Drawing.Size(104, 19);
            this.lblPaymentAmount.TabIndex = 208;
            this.lblPaymentAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRemainingAmount
            // 
            this.lblRemainingAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblRemainingAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemainingAmount.ForeColor = System.Drawing.Color.Green;
            this.lblRemainingAmount.Location = new System.Drawing.Point(470, 40);
            this.lblRemainingAmount.Name = "lblRemainingAmount";
            this.lblRemainingAmount.Size = new System.Drawing.Size(104, 19);
            this.lblRemainingAmount.TabIndex = 208;
            this.lblRemainingAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReserveUsed
            // 
            this.lblReserveUsed.BackColor = System.Drawing.Color.Transparent;
            this.lblReserveUsed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveUsed.ForeColor = System.Drawing.Color.Green;
            this.lblReserveUsed.Location = new System.Drawing.Point(470, 68);
            this.lblReserveUsed.Name = "lblReserveUsed";
            this.lblReserveUsed.Size = new System.Drawing.Size(104, 19);
            this.lblReserveUsed.TabIndex = 208;
            this.lblReserveUsed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAmountAddedToReserve
            // 
            this.lblAmountAddedToReserve.BackColor = System.Drawing.Color.Transparent;
            this.lblAmountAddedToReserve.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountAddedToReserve.ForeColor = System.Drawing.Color.Green;
            this.lblAmountAddedToReserve.Location = new System.Drawing.Point(470, 96);
            this.lblAmountAddedToReserve.Name = "lblAmountAddedToReserve";
            this.lblAmountAddedToReserve.Size = new System.Drawing.Size(104, 19);
            this.lblAmountAddedToReserve.TabIndex = 208;
            this.lblAmountAddedToReserve.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label29);
            this.panel4.Controls.Add(this.label28);
            this.panel4.Controls.Add(this.label27);
            this.panel4.Controls.Add(this.label26);
            this.panel4.Controls.Add(this.lblAmountAddedToReserve);
            this.panel4.Controls.Add(this.lblReserveUsed);
            this.panel4.Controls.Add(this.lblRemainingAmount);
            this.panel4.Controls.Add(this.lblPaymentAmount);
            this.panel4.Controls.Add(this.btnViewReserveRemaining);
            this.panel4.Controls.Add(this.btnViewUsedReserve);
            this.panel4.Controls.Add(this.txtPaymentNote);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.lblPaymentType);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label39);
            this.panel4.Controls.Add(this.lblInsuranceCompany);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.lblCheckNo);
            this.panel4.Controls.Add(this.label40);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 86);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel4.Size = new System.Drawing.Size(965, 132);
            this.panel4.TabIndex = 230;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label29.Location = new System.Drawing.Point(4, 131);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(957, 1);
            this.label29.TabIndex = 212;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Location = new System.Drawing.Point(4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(957, 1);
            this.label28.TabIndex = 211;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.Location = new System.Drawing.Point(961, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 132);
            this.label27.TabIndex = 210;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Location = new System.Drawing.Point(3, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 132);
            this.label26.TabIndex = 209;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label30);
            this.panel3.Controls.Add(this.rbSortByEOB);
            this.panel3.Controls.Add(this.rbSortByClaim);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.label25);
            this.panel3.Controls.Add(this.label24);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(959, 23);
            this.panel3.TabIndex = 4;
            // 
            // label30
            // 
            this.label30.Dock = System.Windows.Forms.DockStyle.Right;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Location = new System.Drawing.Point(777, 3);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(67, 19);
            this.label30.TabIndex = 232;
            this.label30.Text = "Sort by :";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbSortByEOB
            // 
            this.rbSortByEOB.AutoSize = true;
            this.rbSortByEOB.Checked = true;
            this.rbSortByEOB.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbSortByEOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSortByEOB.Location = new System.Drawing.Point(844, 3);
            this.rbSortByEOB.Name = "rbSortByEOB";
            this.rbSortByEOB.Size = new System.Drawing.Size(49, 19);
            this.rbSortByEOB.TabIndex = 233;
            this.rbSortByEOB.TabStop = true;
            this.rbSortByEOB.Text = "EOB";
            this.rbSortByEOB.UseVisualStyleBackColor = true;
            this.rbSortByEOB.Click += new System.EventHandler(this.rbSortByEOB_Click);
            // 
            // rbSortByClaim
            // 
            this.rbSortByClaim.AutoSize = true;
            this.rbSortByClaim.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbSortByClaim.Location = new System.Drawing.Point(893, 3);
            this.rbSortByClaim.Name = "rbSortByClaim";
            this.rbSortByClaim.Size = new System.Drawing.Size(65, 19);
            this.rbSortByClaim.TabIndex = 233;
            this.rbSortByClaim.Text = "Claim #";
            this.rbSortByClaim.UseVisualStyleBackColor = true;
            this.rbSortByClaim.Click += new System.EventHandler(this.rbSortByClaim_Click);
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Location = new System.Drawing.Point(1, 22);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(957, 1);
            this.label23.TabIndex = 1;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Location = new System.Drawing.Point(1, 1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(957, 2);
            this.label25.TabIndex = 14;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Location = new System.Drawing.Point(1, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(957, 1);
            this.label24.TabIndex = 0;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(958, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 23);
            this.label20.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 23);
            this.label13.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 218);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel6.Size = new System.Drawing.Size(965, 26);
            this.panel6.TabIndex = 231;
            // 
            // pnlVoidNote
            // 
            this.pnlVoidNote.BackColor = System.Drawing.Color.Transparent;
            this.pnlVoidNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlVoidNote.Controls.Add(this.lblVoidDesc);
            this.pnlVoidNote.Controls.Add(this.lblVoidNote);
            this.pnlVoidNote.Controls.Add(this.label14);
            this.pnlVoidNote.Controls.Add(this.label15);
            this.pnlVoidNote.Controls.Add(this.label17);
            this.pnlVoidNote.Controls.Add(this.label19);
            this.pnlVoidNote.Controls.Add(this.label31);
            this.pnlVoidNote.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlVoidNote.Location = new System.Drawing.Point(0, 613);
            this.pnlVoidNote.Name = "pnlVoidNote";
            this.pnlVoidNote.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlVoidNote.Size = new System.Drawing.Size(965, 86);
            this.pnlVoidNote.TabIndex = 232;
            // 
            // lblVoidDesc
            // 
            this.lblVoidDesc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoidDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblVoidDesc.Location = new System.Drawing.Point(89, 16);
            this.lblVoidDesc.Name = "lblVoidDesc";
            this.lblVoidDesc.Size = new System.Drawing.Size(864, 56);
            this.lblVoidDesc.TabIndex = 207;
            // 
            // lblVoidNote
            // 
            this.lblVoidNote.AutoSize = true;
            this.lblVoidNote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoidNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblVoidNote.Location = new System.Drawing.Point(13, 16);
            this.lblVoidNote.Name = "lblVoidNote";
            this.lblVoidNote.Size = new System.Drawing.Size(75, 14);
            this.lblVoidNote.TabIndex = 206;
            this.lblVoidNote.Text = "Void Note :";
            this.lblVoidNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(4, 82);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(957, 1);
            this.label14.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Location = new System.Drawing.Point(4, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(957, 2);
            this.label15.TabIndex = 14;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(957, 1);
            this.label17.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Location = new System.Drawing.Point(961, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 83);
            this.label19.TabIndex = 2;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Location = new System.Drawing.Point(3, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 83);
            this.label31.TabIndex = 3;
            // 
            // frmViewInsurancePayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(965, 699);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlVoidNote);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewInsurancePayment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " View Insurance Payment";
            this.Load += new System.EventHandler(this.frmViewInsurancePayment_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1LogDetails)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.pnlVoidNote.ResumeLayout(false);
            this.pnlVoidNote.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Void;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private C1.Win.C1FlexGrid.C1FlexGrid c1LogDetails;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblPaymentTray;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label lblAlertMessage;
        private System.Windows.Forms.Label lblCloseDate;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Button btnViewReserveRemaining;
        private System.Windows.Forms.Label lblCheckNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnViewUsedReserve;
        private System.Windows.Forms.Label lblInsuranceCompany;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblPaymentType;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtPaymentNote;
        private System.Windows.Forms.Label lblPaymentAmount;
        private System.Windows.Forms.Label lblRemainingAmount;
        private System.Windows.Forms.Label lblReserveUsed;
        private System.Windows.Forms.Label lblAmountAddedToReserve;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        internal System.Windows.Forms.ToolStripButton tsb_ViewRemit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton rbSortByEOB;
        private System.Windows.Forms.RadioButton rbSortByClaim;
        private System.Windows.Forms.Panel pnlVoidNote;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblVoidNote;
        private System.Windows.Forms.Label lblVoidDesc;
    }
}