namespace gloBilling
{
    partial class frmBillingHold
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillingHold));
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnSaveNClose = new System.Windows.Forms.ToolStripButton();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblHoldReason = new System.Windows.Forms.Label();
            this.cmbReason = new System.Windows.Forms.ComboBox();
            this.lblMsgHold = new System.Windows.Forms.Label();
            this.txtHoldNote = new System.Windows.Forms.TextBox();
            this.lblHoldNote = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBillingHold = new System.Windows.Forms.Panel();
            this.pnlOnBillingHold = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHoldBillingReason = new System.Windows.Forms.Label();
            this.cmbHoldReason = new System.Windows.Forms.ComboBox();
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
            this.tls_Release = new System.Windows.Forms.ToolStripButton();
            this.tls_saveMod = new System.Windows.Forms.ToolStripButton();
            this.tls_SaveAndCloseMod = new System.Windows.Forms.ToolStripButton();
            this.tls_CloseMod = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.pnlToolstrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlBillingHold.SuspendLayout();
            this.pnlOnBillingHold.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlOnToolstrip.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.Controls.Add(this.toolStrip1);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(510, 54);
            this.pnlToolstrip.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnSaveNClose,
            this.tls_btnClose});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(510, 53);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tls_btnSaveNClose
            // 
            this.tls_btnSaveNClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnSaveNClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnSaveNClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnSaveNClose.Image")));
            this.tls_btnSaveNClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnSaveNClose.Name = "tls_btnSaveNClose";
            this.tls_btnSaveNClose.Size = new System.Drawing.Size(66, 50);
            this.tls_btnSaveNClose.Tag = "SaveNClose";
            this.tls_btnSaveNClose.Text = "&Save&&Cls";
            this.tls_btnSaveNClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnSaveNClose.ToolTipText = "Save and Close";
            this.tls_btnSaveNClose.Click += new System.EventHandler(this.tls_btnSaveNClose_Click);
            // 
            // tls_btnClose
            // 
            this.tls_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnClose.Image")));
            this.tls_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnClose.Name = "tls_btnClose";
            this.tls_btnClose.Size = new System.Drawing.Size(43, 50);
            this.tls_btnClose.Text = "&Close";
            this.tls_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnClose.Click += new System.EventHandler(this.tls_btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.lblHoldReason);
            this.panel2.Controls.Add(this.cmbReason);
            this.panel2.Controls.Add(this.lblMsgHold);
            this.panel2.Controls.Add(this.txtHoldNote);
            this.panel2.Controls.Add(this.lblHoldNote);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(510, 210);
            this.panel2.TabIndex = 1;
            // 
            // lblHoldReason
            // 
            this.lblHoldReason.AutoSize = true;
            this.lblHoldReason.Location = new System.Drawing.Point(50, 70);
            this.lblHoldReason.Name = "lblHoldReason";
            this.lblHoldReason.Size = new System.Drawing.Size(110, 14);
            this.lblHoldReason.TabIndex = 6;
            this.lblHoldReason.Text = "Hold Reason Code:";
            // 
            // cmbReason
            // 
            this.cmbReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReason.FormattingEnabled = true;
            this.cmbReason.Location = new System.Drawing.Point(165, 64);
            this.cmbReason.Name = "cmbReason";
            this.cmbReason.Size = new System.Drawing.Size(311, 22);
            this.cmbReason.TabIndex = 5;
            this.cmbReason.SelectedIndexChanged += new System.EventHandler(this.cmbReason_SelectedIndexChanged);
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
            // txtHoldNote
            // 
            this.txtHoldNote.Location = new System.Drawing.Point(165, 94);
            this.txtHoldNote.Multiline = true;
            this.txtHoldNote.Name = "txtHoldNote";
            this.txtHoldNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHoldNote.Size = new System.Drawing.Size(311, 50);
            this.txtHoldNote.TabIndex = 0;
            this.txtHoldNote.TextChanged += new System.EventHandler(this.txtHoldNote_TextChanged);
            // 
            // lblHoldNote
            // 
            this.lblHoldNote.AutoSize = true;
            this.lblHoldNote.Location = new System.Drawing.Point(78, 97);
            this.lblHoldNote.Name = "lblHoldNote";
            this.lblHoldNote.Size = new System.Drawing.Size(82, 14);
            this.lblHoldNote.TabIndex = 4;
            this.lblHoldNote.Text = "Hold Reason :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(506, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 202);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 202);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(504, 1);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(504, 1);
            this.label1.TabIndex = 0;
            // 
            // pnlBillingHold
            // 
            this.pnlBillingHold.Controls.Add(this.panel2);
            this.pnlBillingHold.Controls.Add(this.pnlToolstrip);
            this.pnlBillingHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBillingHold.Location = new System.Drawing.Point(0, 0);
            this.pnlBillingHold.Name = "pnlBillingHold";
            this.pnlBillingHold.Size = new System.Drawing.Size(510, 264);
            this.pnlBillingHold.TabIndex = 4;
            // 
            // pnlOnBillingHold
            // 
            this.pnlOnBillingHold.Controls.Add(this.panel1);
            this.pnlOnBillingHold.Controls.Add(this.pnlOnToolstrip);
            this.pnlOnBillingHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOnBillingHold.Location = new System.Drawing.Point(0, 0);
            this.pnlOnBillingHold.Name = "pnlOnBillingHold";
            this.pnlOnBillingHold.Size = new System.Drawing.Size(510, 264);
            this.pnlOnBillingHold.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblHoldBillingReason);
            this.panel1.Controls.Add(this.cmbHoldReason);
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
            this.panel1.Size = new System.Drawing.Size(510, 210);
            this.panel1.TabIndex = 2;
            // 
            // lblHoldBillingReason
            // 
            this.lblHoldBillingReason.AutoSize = true;
            this.lblHoldBillingReason.Location = new System.Drawing.Point(42, 89);
            this.lblHoldBillingReason.Name = "lblHoldBillingReason";
            this.lblHoldBillingReason.Size = new System.Drawing.Size(110, 14);
            this.lblHoldBillingReason.TabIndex = 8;
            this.lblHoldBillingReason.Text = "Hold Reason Code:";
            // 
            // cmbHoldReason
            // 
            this.cmbHoldReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHoldReason.FormattingEnabled = true;
            this.cmbHoldReason.Location = new System.Drawing.Point(157, 83);
            this.cmbHoldReason.Name = "cmbHoldReason";
            this.cmbHoldReason.Size = new System.Drawing.Size(311, 22);
            this.cmbHoldReason.TabIndex = 7;
            this.cmbHoldReason.SelectedIndexChanged += new System.EventHandler(this.cmbHoldReason_SelectedIndexChanged);
            // 
            // txtHoldNoteMod
            // 
            this.txtHoldNoteMod.Location = new System.Drawing.Point(157, 116);
            this.txtHoldNoteMod.Multiline = true;
            this.txtHoldNoteMod.Name = "txtHoldNoteMod";
            this.txtHoldNoteMod.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHoldNoteMod.Size = new System.Drawing.Size(311, 52);
            this.txtHoldNoteMod.TabIndex = 1;
            this.txtHoldNoteMod.TextChanged += new System.EventHandler(this.txtHoldNoteMod_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(68, 55);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(84, 14);
            this.label23.TabIndex = 4;
            this.label23.Text = "Hold Started :";
            // 
            // lblHoldUser
            // 
            this.lblHoldUser.AutoSize = true;
            this.lblHoldUser.Location = new System.Drawing.Point(307, 55);
            this.lblHoldUser.Name = "lblHoldUser";
            this.lblHoldUser.Size = new System.Drawing.Size(49, 14);
            this.lblHoldUser.TabIndex = 4;
            this.lblHoldUser.Text = "<User>";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "Note Last Modified :";
            // 
            // lblHoldTime
            // 
            this.lblHoldTime.AutoSize = true;
            this.lblHoldTime.Location = new System.Drawing.Point(237, 55);
            this.lblHoldTime.Name = "lblHoldTime";
            this.lblHoldTime.Size = new System.Drawing.Size(52, 14);
            this.lblHoldTime.TabIndex = 4;
            this.lblHoldTime.Text = "<Time>";
            // 
            // lblHoldModUser
            // 
            this.lblHoldModUser.AutoSize = true;
            this.lblHoldModUser.Location = new System.Drawing.Point(306, 183);
            this.lblHoldModUser.Name = "lblHoldModUser";
            this.lblHoldModUser.Size = new System.Drawing.Size(49, 14);
            this.lblHoldModUser.TabIndex = 4;
            this.lblHoldModUser.Text = "<User>";
            // 
            // lblHoldDate
            // 
            this.lblHoldDate.AutoSize = true;
            this.lblHoldDate.Location = new System.Drawing.Point(155, 55);
            this.lblHoldDate.Name = "lblHoldDate";
            this.lblHoldDate.Size = new System.Drawing.Size(51, 14);
            this.lblHoldDate.TabIndex = 4;
            this.lblHoldDate.Text = "<Date>";
            // 
            // lblHoldModTime
            // 
            this.lblHoldModTime.AutoSize = true;
            this.lblHoldModTime.Location = new System.Drawing.Point(236, 183);
            this.lblHoldModTime.Name = "lblHoldModTime";
            this.lblHoldModTime.Size = new System.Drawing.Size(52, 14);
            this.lblHoldModTime.TabIndex = 4;
            this.lblHoldModTime.Text = "<Time>";
            // 
            // lblHoldModDate
            // 
            this.lblHoldModDate.AutoSize = true;
            this.lblHoldModDate.Location = new System.Drawing.Point(155, 183);
            this.lblHoldModDate.Name = "lblHoldModDate";
            this.lblHoldModDate.Size = new System.Drawing.Size(51, 14);
            this.lblHoldModDate.TabIndex = 4;
            this.lblHoldModDate.Text = "<Date>";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(82, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 4;
            this.label6.Text = "Hold Note :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(488, 14);
            this.label8.TabIndex = 4;
            this.label8.Text = "Claim is On Billing Hold. Claim will not be billed untill released from Billing H" +
    "old.";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(506, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 202);
            this.label13.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(3, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 202);
            this.label14.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(3, 206);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(504, 1);
            this.label15.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(3, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(504, 1);
            this.label16.TabIndex = 0;
            // 
            // pnlOnToolstrip
            // 
            this.pnlOnToolstrip.Controls.Add(this.toolStrip2);
            this.pnlOnToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOnToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlOnToolstrip.Name = "pnlOnToolstrip";
            this.pnlOnToolstrip.Size = new System.Drawing.Size(510, 54);
            this.pnlOnToolstrip.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_Release,
            this.tls_saveMod,
            this.tls_SaveAndCloseMod,
            this.tls_CloseMod});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(510, 53);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.TabStop = true;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tls_Release
            // 
            this.tls_Release.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Release.Image = ((System.Drawing.Image)(resources.GetObject("tls_Release.Image")));
            this.tls_Release.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_Release.Name = "tls_Release";
            this.tls_Release.Size = new System.Drawing.Size(57, 50);
            this.tls_Release.Text = "&Release";
            this.tls_Release.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_Release.Click += new System.EventHandler(this.tls_Release_Click);
            // 
            // tls_saveMod
            // 
            this.tls_saveMod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_saveMod.Image = ((System.Drawing.Image)(resources.GetObject("tls_saveMod.Image")));
            this.tls_saveMod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_saveMod.Name = "tls_saveMod";
            this.tls_saveMod.Size = new System.Drawing.Size(40, 50);
            this.tls_saveMod.Text = "Save";
            this.tls_saveMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_saveMod.Visible = false;
            this.tls_saveMod.Click += new System.EventHandler(this.tls_saveMod_Click);
            // 
            // tls_SaveAndCloseMod
            // 
            this.tls_SaveAndCloseMod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SaveAndCloseMod.Image = ((System.Drawing.Image)(resources.GetObject("tls_SaveAndCloseMod.Image")));
            this.tls_SaveAndCloseMod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_SaveAndCloseMod.Name = "tls_SaveAndCloseMod";
            this.tls_SaveAndCloseMod.Size = new System.Drawing.Size(66, 50);
            this.tls_SaveAndCloseMod.Text = "&Save&&Cls";
            this.tls_SaveAndCloseMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_SaveAndCloseMod.ToolTipText = "Save and Close";
            this.tls_SaveAndCloseMod.Click += new System.EventHandler(this.tls_SaveAndCloseMod_Click);
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
            this.tls_CloseMod.Click += new System.EventHandler(this.tls_CloseMod_Click);
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(68, 96);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(14, 14);
            this.label7.TabIndex = 113;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmBillingHold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(510, 264);
            this.Controls.Add(this.pnlBillingHold);
            this.Controls.Add(this.pnlOnBillingHold);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBillingHold";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hold Billing ";
            this.Load += new System.EventHandler(this.frmBillingHold_Load);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlBillingHold.ResumeLayout(false);
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

        private System.Windows.Forms.Panel pnlToolstrip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlBillingHold;
        private System.Windows.Forms.Panel pnlOnBillingHold;
        private System.Windows.Forms.Panel pnlOnToolstrip;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip2;
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
        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        private System.Windows.Forms.ToolStripButton tls_btnSaveNClose;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.Label lblMsgHold;
        private System.Windows.Forms.ToolStripButton tls_Release;
        private System.Windows.Forms.Label lblHoldReason;
        private System.Windows.Forms.ComboBox cmbReason;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblHoldBillingReason;
        private System.Windows.Forms.ComboBox cmbHoldReason;
        private System.Windows.Forms.Label label7;
    }
}