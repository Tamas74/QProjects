namespace gloContacts
{
    partial class frmPlanHold
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlanHold));
            this.tls_btnSaveNClose = new System.Windows.Forms.ToolStripButton();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotBalance = new System.Windows.Forms.Label();
            this.lblClaimCount = new System.Windows.Forms.Label();
            this.lblNoOfClaims = new System.Windows.Forms.Label();
            this.lblTotBalCaption = new System.Windows.Forms.Label();
            this.txtHoldNote = new System.Windows.Forms.TextBox();
            this.lblMsgHold = new System.Windows.Forms.Label();
            this.lblHoldNote = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBillingHold = new System.Windows.Forms.Panel();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_SaveandCls = new System.Windows.Forms.ToolStripButton();
            this.tls_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlOnBillingHold = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.lblModTotalAmt = new System.Windows.Forms.Label();
            this.lbModlNoOfClaim = new System.Windows.Forms.Label();
            this.lblModNoOfClaimCaption = new System.Windows.Forms.Label();
            this.lblModTotalAmtCaption = new System.Windows.Forms.Label();
            this.txtHoldNoteMod = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.lblHoldUser = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblHoldTime = new System.Windows.Forms.Label();
            this.lblHoldModUser = new System.Windows.Forms.Label();
            this.lblHoldDate = new System.Windows.Forms.Label();
            this.lblHoldModTime = new System.Windows.Forms.Label();
            this.lblHoldModDate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlOnToolstrip = new System.Windows.Forms.Panel();
            this.toolStrip2 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_Releas = new System.Windows.Forms.ToolStripButton();
            this.tls_ModSave = new System.Windows.Forms.ToolStripButton();
            this.tls_ModSavenClose = new System.Windows.Forms.ToolStripButton();
            this.tls_ModClose = new System.Windows.Forms.ToolStripButton();
            this.tls_Release = new System.Windows.Forms.ToolStripButton();
            this.tls_saveMod = new System.Windows.Forms.ToolStripButton();
            this.tls_SaveAndCloseMod = new System.Windows.Forms.ToolStripButton();
            this.tls_CloseMod = new System.Windows.Forms.ToolStripButton();
            this.panel2.SuspendLayout();
            this.pnlBillingHold.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlOnBillingHold.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlOnToolstrip.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_btnSaveNClose
            // 
            this.tls_btnSaveNClose.Name = "tls_btnSaveNClose";
            this.tls_btnSaveNClose.Size = new System.Drawing.Size(23, 23);
            // 
            // tls_btnClose
            // 
            this.tls_btnClose.Name = "tls_btnClose";
            this.tls_btnClose.Size = new System.Drawing.Size(23, 23);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.lblTotBalance);
            this.panel2.Controls.Add(this.lblClaimCount);
            this.panel2.Controls.Add(this.lblNoOfClaims);
            this.panel2.Controls.Add(this.lblTotBalCaption);
            this.panel2.Controls.Add(this.txtHoldNote);
            this.panel2.Controls.Add(this.lblMsgHold);
            this.panel2.Controls.Add(this.lblHoldNote);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(517, 240);
            this.panel2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(82, 133);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(14, 14);
            this.label7.TabIndex = 115;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotBalance
            // 
            this.lblTotBalance.AutoSize = true;
            this.lblTotBalance.Location = new System.Drawing.Point(178, 107);
            this.lblTotBalance.Name = "lblTotBalance";
            this.lblTotBalance.Size = new System.Drawing.Size(72, 14);
            this.lblTotBalance.TabIndex = 11;
            this.lblTotBalance.Text = "<Total Bal>";
            // 
            // lblClaimCount
            // 
            this.lblClaimCount.AutoSize = true;
            this.lblClaimCount.Location = new System.Drawing.Point(178, 79);
            this.lblClaimCount.Name = "lblClaimCount";
            this.lblClaimCount.Size = new System.Drawing.Size(89, 14);
            this.lblClaimCount.TabIndex = 10;
            this.lblClaimCount.Text = "<Claim Count>";
            // 
            // lblNoOfClaims
            // 
            this.lblNoOfClaims.AutoSize = true;
            this.lblNoOfClaims.Location = new System.Drawing.Point(22, 79);
            this.lblNoOfClaims.Name = "lblNoOfClaims";
            this.lblNoOfClaims.Size = new System.Drawing.Size(154, 14);
            this.lblNoOfClaims.TabIndex = 9;
            this.lblNoOfClaims.Text = "Current Number of Claims :";
            // 
            // lblTotBalCaption
            // 
            this.lblTotBalCaption.AutoSize = true;
            this.lblTotBalCaption.Location = new System.Drawing.Point(88, 107);
            this.lblTotBalCaption.Name = "lblTotBalCaption";
            this.lblTotBalCaption.Size = new System.Drawing.Size(88, 14);
            this.lblTotBalCaption.TabIndex = 8;
            this.lblTotBalCaption.Text = "Total Balance :";
            // 
            // txtHoldNote
            // 
            this.txtHoldNote.Location = new System.Drawing.Point(178, 132);
            this.txtHoldNote.Multiline = true;
            this.txtHoldNote.Name = "txtHoldNote";
            this.txtHoldNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHoldNote.Size = new System.Drawing.Size(311, 50);
            this.txtHoldNote.TabIndex = 0;
            this.txtHoldNote.TextChanged += new System.EventHandler(this.txtHoldNote_TextChanged);
            // 
            // lblMsgHold
            // 
            this.lblMsgHold.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsgHold.Location = new System.Drawing.Point(13, 22);
            this.lblMsgHold.Name = "lblMsgHold";
            this.lblMsgHold.Size = new System.Drawing.Size(491, 28);
            this.lblMsgHold.TabIndex = 7;
            this.lblMsgHold.Text = "label1";
            // 
            // lblHoldNote
            // 
            this.lblHoldNote.AutoSize = true;
            this.lblHoldNote.Location = new System.Drawing.Point(94, 135);
            this.lblHoldNote.Name = "lblHoldNote";
            this.lblHoldNote.Size = new System.Drawing.Size(82, 14);
            this.lblHoldNote.TabIndex = 4;
            this.lblHoldNote.Text = "Hold Reason :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(513, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 232);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 232);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(511, 1);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(511, 1);
            this.label1.TabIndex = 0;
            // 
            // pnlBillingHold
            // 
            this.pnlBillingHold.Controls.Add(this.panel2);
            this.pnlBillingHold.Controls.Add(this.pnlToolstrip);
            this.pnlBillingHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBillingHold.Location = new System.Drawing.Point(0, 0);
            this.pnlBillingHold.Name = "pnlBillingHold";
            this.pnlBillingHold.Size = new System.Drawing.Size(517, 294);
            this.pnlBillingHold.TabIndex = 4;
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.Controls.Add(this.toolStrip1);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(517, 54);
            this.pnlToolstrip.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::gloContacts.Properties.Resources.Img_Toolstrip;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_SaveandCls,
            this.tls_Close});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(517, 53);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tls_SaveandCls
            // 
            this.tls_SaveandCls.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SaveandCls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_SaveandCls.Image = ((System.Drawing.Image)(resources.GetObject("tls_SaveandCls.Image")));
            this.tls_SaveandCls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_SaveandCls.Name = "tls_SaveandCls";
            this.tls_SaveandCls.Size = new System.Drawing.Size(66, 50);
            this.tls_SaveandCls.Tag = "SaveNClose";
            this.tls_SaveandCls.Text = "Sa&ve&&Cls";
            this.tls_SaveandCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_SaveandCls.ToolTipText = "Save and Close";
            this.tls_SaveandCls.Click += new System.EventHandler(this.tls_SaveandCls_Click);
            // 
            // tls_Close
            // 
            this.tls_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Close.Image = ((System.Drawing.Image)(resources.GetObject("tls_Close.Image")));
            this.tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_Close.Name = "tls_Close";
            this.tls_Close.Size = new System.Drawing.Size(43, 50);
            this.tls_Close.Text = "Close";
            this.tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_Close.Click += new System.EventHandler(this.tls_Close_Click);
            // 
            // pnlOnBillingHold
            // 
            this.pnlOnBillingHold.Controls.Add(this.panel1);
            this.pnlOnBillingHold.Controls.Add(this.pnlOnToolstrip);
            this.pnlOnBillingHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOnBillingHold.Location = new System.Drawing.Point(0, 0);
            this.pnlOnBillingHold.Name = "pnlOnBillingHold";
            this.pnlOnBillingHold.Size = new System.Drawing.Size(517, 294);
            this.pnlOnBillingHold.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.lblModTotalAmt);
            this.panel1.Controls.Add(this.lbModlNoOfClaim);
            this.panel1.Controls.Add(this.lblModNoOfClaimCaption);
            this.panel1.Controls.Add(this.lblModTotalAmtCaption);
            this.panel1.Controls.Add(this.txtHoldNoteMod);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.lblHoldUser);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblHoldTime);
            this.panel1.Controls.Add(this.lblHoldModUser);
            this.panel1.Controls.Add(this.lblHoldDate);
            this.panel1.Controls.Add(this.lblHoldModTime);
            this.panel1.Controls.Add(this.lblHoldModDate);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(517, 240);
            this.panel1.TabIndex = 2;
            // 
            // label22
            // 
            this.label22.AutoEllipsis = true;
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(87, 139);
            this.label22.Name = "label22";
            this.label22.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label22.Size = new System.Drawing.Size(14, 14);
            this.label22.TabIndex = 114;
            this.label22.Text = "*";
            this.label22.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblModTotalAmt
            // 
            this.lblModTotalAmt.AutoSize = true;
            this.lblModTotalAmt.Location = new System.Drawing.Point(172, 88);
            this.lblModTotalAmt.Name = "lblModTotalAmt";
            this.lblModTotalAmt.Size = new System.Drawing.Size(72, 14);
            this.lblModTotalAmt.TabIndex = 15;
            this.lblModTotalAmt.Text = "<Total Bal>";
            // 
            // lbModlNoOfClaim
            // 
            this.lbModlNoOfClaim.AutoSize = true;
            this.lbModlNoOfClaim.Location = new System.Drawing.Point(172, 64);
            this.lbModlNoOfClaim.Name = "lbModlNoOfClaim";
            this.lbModlNoOfClaim.Size = new System.Drawing.Size(89, 14);
            this.lbModlNoOfClaim.TabIndex = 14;
            this.lbModlNoOfClaim.Text = "<Claim Count>";
            // 
            // lblModNoOfClaimCaption
            // 
            this.lblModNoOfClaimCaption.AutoSize = true;
            this.lblModNoOfClaimCaption.Location = new System.Drawing.Point(14, 64);
            this.lblModNoOfClaimCaption.Name = "lblModNoOfClaimCaption";
            this.lblModNoOfClaimCaption.Size = new System.Drawing.Size(154, 14);
            this.lblModNoOfClaimCaption.TabIndex = 13;
            this.lblModNoOfClaimCaption.Text = "Current Number of Claims :";
            // 
            // lblModTotalAmtCaption
            // 
            this.lblModTotalAmtCaption.AutoSize = true;
            this.lblModTotalAmtCaption.Location = new System.Drawing.Point(80, 88);
            this.lblModTotalAmtCaption.Name = "lblModTotalAmtCaption";
            this.lblModTotalAmtCaption.Size = new System.Drawing.Size(88, 14);
            this.lblModTotalAmtCaption.TabIndex = 12;
            this.lblModTotalAmtCaption.Text = "Total Balance :";
            // 
            // txtHoldNoteMod
            // 
            this.txtHoldNoteMod.Location = new System.Drawing.Point(172, 139);
            this.txtHoldNoteMod.Multiline = true;
            this.txtHoldNoteMod.Name = "txtHoldNoteMod";
            this.txtHoldNoteMod.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHoldNoteMod.Size = new System.Drawing.Size(327, 52);
            this.txtHoldNoteMod.TabIndex = 1;
            this.txtHoldNoteMod.TextChanged += new System.EventHandler(this.txtHoldNoteMod_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(84, 112);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(84, 14);
            this.label23.TabIndex = 4;
            this.label23.Text = "Hold Started :";
            // 
            // lblHoldUser
            // 
            this.lblHoldUser.AutoSize = true;
            this.lblHoldUser.Location = new System.Drawing.Point(337, 112);
            this.lblHoldUser.Name = "lblHoldUser";
            this.lblHoldUser.Size = new System.Drawing.Size(49, 14);
            this.lblHoldUser.TabIndex = 4;
            this.lblHoldUser.Text = "<User>";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "Note Last Modified :";
            // 
            // lblHoldTime
            // 
            this.lblHoldTime.AutoSize = true;
            this.lblHoldTime.Location = new System.Drawing.Point(258, 112);
            this.lblHoldTime.Name = "lblHoldTime";
            this.lblHoldTime.Size = new System.Drawing.Size(52, 14);
            this.lblHoldTime.TabIndex = 4;
            this.lblHoldTime.Text = "<Time>";
            // 
            // lblHoldModUser
            // 
            this.lblHoldModUser.AutoSize = true;
            this.lblHoldModUser.Location = new System.Drawing.Point(337, 201);
            this.lblHoldModUser.Name = "lblHoldModUser";
            this.lblHoldModUser.Size = new System.Drawing.Size(49, 14);
            this.lblHoldModUser.TabIndex = 4;
            this.lblHoldModUser.Text = "<User>";
            // 
            // lblHoldDate
            // 
            this.lblHoldDate.AutoSize = true;
            this.lblHoldDate.Location = new System.Drawing.Point(172, 112);
            this.lblHoldDate.Name = "lblHoldDate";
            this.lblHoldDate.Size = new System.Drawing.Size(51, 14);
            this.lblHoldDate.TabIndex = 4;
            this.lblHoldDate.Text = "<Date>";
            // 
            // lblHoldModTime
            // 
            this.lblHoldModTime.AutoSize = true;
            this.lblHoldModTime.Location = new System.Drawing.Point(258, 201);
            this.lblHoldModTime.Name = "lblHoldModTime";
            this.lblHoldModTime.Size = new System.Drawing.Size(52, 14);
            this.lblHoldModTime.TabIndex = 4;
            this.lblHoldModTime.Text = "<Time>";
            // 
            // lblHoldModDate
            // 
            this.lblHoldModDate.AutoSize = true;
            this.lblHoldModDate.Location = new System.Drawing.Point(172, 201);
            this.lblHoldModDate.Name = "lblHoldModDate";
            this.lblHoldModDate.Size = new System.Drawing.Size(51, 14);
            this.lblHoldModDate.TabIndex = 4;
            this.lblHoldModDate.Text = "<Date>";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 4;
            this.label6.Text = "Hold Note :";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(484, 38);
            this.label8.TabIndex = 4;
            this.label8.Text = "Plan is On Billing Hold. Plan\'s Claims will not be billed until Plan is released " +
                "from Plan Billing Hold.";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(513, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 232);
            this.label13.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(3, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 232);
            this.label14.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(3, 236);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(511, 1);
            this.label15.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(3, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(511, 1);
            this.label16.TabIndex = 0;
            // 
            // pnlOnToolstrip
            // 
            this.pnlOnToolstrip.Controls.Add(this.toolStrip2);
            this.pnlOnToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOnToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlOnToolstrip.Name = "pnlOnToolstrip";
            this.pnlOnToolstrip.Size = new System.Drawing.Size(517, 54);
            this.pnlOnToolstrip.TabIndex = 3;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::gloContacts.Properties.Resources.Img_Toolstrip;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_Releas,
            this.tls_ModSave,
            this.tls_ModSavenClose,
            this.tls_ModClose});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(517, 53);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.TabStop = true;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tls_Releas
            // 
            this.tls_Releas.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Releas.Image = ((System.Drawing.Image)(resources.GetObject("tls_Releas.Image")));
            this.tls_Releas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_Releas.Name = "tls_Releas";
            this.tls_Releas.Size = new System.Drawing.Size(57, 50);
            this.tls_Releas.Text = "&Release";
            this.tls_Releas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_Releas.Click += new System.EventHandler(this.tls_Releas_Click);
            // 
            // tls_ModSave
            // 
            this.tls_ModSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_ModSave.Image = ((System.Drawing.Image)(resources.GetObject("tls_ModSave.Image")));
            this.tls_ModSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_ModSave.Name = "tls_ModSave";
            this.tls_ModSave.Size = new System.Drawing.Size(40, 50);
            this.tls_ModSave.Text = "Save";
            this.tls_ModSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_ModSave.Visible = false;
            // 
            // tls_ModSavenClose
            // 
            this.tls_ModSavenClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_ModSavenClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_ModSavenClose.Image")));
            this.tls_ModSavenClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_ModSavenClose.Name = "tls_ModSavenClose";
            this.tls_ModSavenClose.Size = new System.Drawing.Size(66, 50);
            this.tls_ModSavenClose.Text = "&Save&&Cls";
            this.tls_ModSavenClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_ModSavenClose.ToolTipText = "Save and Close";
            this.tls_ModSavenClose.Click += new System.EventHandler(this.tls_ModSavenClose_Click);
            // 
            // tls_ModClose
            // 
            this.tls_ModClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_ModClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_ModClose.Image")));
            this.tls_ModClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_ModClose.Name = "tls_ModClose";
            this.tls_ModClose.Size = new System.Drawing.Size(43, 50);
            this.tls_ModClose.Text = "&Close";
            this.tls_ModClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_ModClose.Click += new System.EventHandler(this.tls_ModClose_Click);
            // 
            // tls_Release
            // 
            this.tls_Release.Name = "tls_Release";
            this.tls_Release.Size = new System.Drawing.Size(23, 23);
            // 
            // tls_saveMod
            // 
            this.tls_saveMod.Name = "tls_saveMod";
            this.tls_saveMod.Size = new System.Drawing.Size(23, 23);
            // 
            // tls_SaveAndCloseMod
            // 
            this.tls_SaveAndCloseMod.Name = "tls_SaveAndCloseMod";
            this.tls_SaveAndCloseMod.Size = new System.Drawing.Size(23, 23);
            // 
            // tls_CloseMod
            // 
            this.tls_CloseMod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_CloseMod.Image = ((System.Drawing.Image)(resources.GetObject("tls_CloseMod.Image")));
            this.tls_CloseMod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_CloseMod.Name = "tls_CloseMod";
            this.tls_CloseMod.Size = new System.Drawing.Size(43, 50);
            this.tls_CloseMod.Text = "&Close";
            this.tls_CloseMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // frmPlanHold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(517, 294);
            this.Controls.Add(this.pnlOnBillingHold);
            this.Controls.Add(this.pnlBillingHold);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlanHold";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hold Plan ";
            this.Load += new System.EventHandler(this.frmPlanHold_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlBillingHold.ResumeLayout(false);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlOnBillingHold.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlOnToolstrip.ResumeLayout(false);
            this.pnlOnToolstrip.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlBillingHold;
        private System.Windows.Forms.Panel pnlOnBillingHold;
        private System.Windows.Forms.ToolStripButton tls_saveMod;
        private System.Windows.Forms.ToolStripButton tls_CloseMod;
        private System.Windows.Forms.TextBox txtHoldNote;
        private System.Windows.Forms.Label lblHoldNote;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtHoldNoteMod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblHoldModTime;
        private System.Windows.Forms.Label lblHoldModDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblHoldUser;
        private System.Windows.Forms.Label lblHoldTime;
        private System.Windows.Forms.Label lblHoldModUser;
        private System.Windows.Forms.Label lblHoldDate;
        private System.Windows.Forms.ToolStripButton tls_SaveAndCloseMod;
        private System.Windows.Forms.ToolStripButton tls_btnSaveNClose;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.Label lblMsgHold;
        private System.Windows.Forms.ToolStripButton tls_Release;
        private System.Windows.Forms.Panel pnlToolstrip;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        private System.Windows.Forms.ToolStripButton tls_SaveandCls;
        private System.Windows.Forms.ToolStripButton tls_Close;
        private System.Windows.Forms.Panel pnlOnToolstrip;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip2;
        private System.Windows.Forms.ToolStripButton tls_Releas;
        private System.Windows.Forms.ToolStripButton tls_ModSave;
        private System.Windows.Forms.ToolStripButton tls_ModSavenClose;
        private System.Windows.Forms.ToolStripButton tls_ModClose;
        private System.Windows.Forms.Label lblTotBalance;
        private System.Windows.Forms.Label lblClaimCount;
        private System.Windows.Forms.Label lblNoOfClaims;
        private System.Windows.Forms.Label lblTotBalCaption;
        private System.Windows.Forms.Label lblModTotalAmt;
        private System.Windows.Forms.Label lbModlNoOfClaim;
        private System.Windows.Forms.Label lblModNoOfClaimCaption;
        private System.Windows.Forms.Label lblModTotalAmtCaption;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label7;
    }
}