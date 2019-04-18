namespace gloBilling.Collections
{
    partial class frmSetupPaymentPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupPaymentPlan));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Notes = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlPatDetails = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAccountNo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label72 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.panel_NoteDtl = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblPlanAmount = new System.Windows.Forms.Label();
            this.txtPlanAmount = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbFollowupAction = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.mskFollowupDate = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_notes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBrowseNotes = new System.Windows.Forms.Button();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Notes.SuspendLayout();
            this.pnlPatDetails.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_NoteDtl.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Notes);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(760, 56);
            this.pnlToolStrip.TabIndex = 3;
            this.pnlToolStrip.TabStop = true;
            // 
            // tls_Notes
            // 
            this.tls_Notes.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.tls_Notes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Notes.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Notes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Ok,
            this.tlb_Close});
            this.tls_Notes.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Notes.Location = new System.Drawing.Point(0, 0);
            this.tls_Notes.Name = "tls_Notes";
            this.tls_Notes.Size = new System.Drawing.Size(760, 53);
            this.tls_Notes.TabIndex = 0;
            this.tls_Notes.TabStop = true;
            this.tls_Notes.Text = "toolStrip1";
            // 
            // tlb_Ok
            // 
            this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.Image")));
            this.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Ok.Name = "tlb_Ok";
            this.tlb_Ok.Size = new System.Drawing.Size(66, 50);
            this.tlb_Ok.Tag = "OK";
            this.tlb_Ok.Text = "Sa&ve&&Cls";
            this.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Ok.ToolTipText = "Save and Close";
            this.tlb_Ok.Click += new System.EventHandler(this.tlb_Ok_Click);
            // 
            // tlb_Close
            // 
            this.tlb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Close.Image")));
            this.tlb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Close.Name = "tlb_Close";
            this.tlb_Close.Size = new System.Drawing.Size(43, 50);
            this.tlb_Close.Tag = "Cancel";
            this.tlb_Close.Text = "&Close";
            this.tlb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Close.ToolTipText = "Close";
            this.tlb_Close.Click += new System.EventHandler(this.tlb_Close_Click);
            // 
            // pnlPatDetails
            // 
            this.pnlPatDetails.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
            this.pnlPatDetails.Controls.Add(this.panel1);
            this.pnlPatDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatDetails.Location = new System.Drawing.Point(3, 3);
            this.pnlPatDetails.Name = "pnlPatDetails";
            this.pnlPatDetails.Size = new System.Drawing.Size(754, 25);
            this.pnlPatDetails.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lblAccountNo);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 25);
            this.panel1.TabIndex = 0;
            this.panel1.TabStop = true;
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountNo.AutoSize = true;
            this.lblAccountNo.BackColor = System.Drawing.Color.Transparent;
            this.lblAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountNo.ForeColor = System.Drawing.Color.Black;
            this.lblAccountNo.Location = new System.Drawing.Point(74, 5);
            this.lblAccountNo.MaximumSize = new System.Drawing.Size(670, 14);
            this.lblAccountNo.MinimumSize = new System.Drawing.Size(670, 14);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.Size = new System.Drawing.Size(670, 14);
            this.lblAccountNo.TabIndex = 2008;
            this.lblAccountNo.Text = "                   ";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 23);
            this.label7.TabIndex = 7;
            this.label7.Text = "label4";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(753, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 23);
            this.label8.TabIndex = 6;
            this.label8.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(7, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 14);
            this.label4.TabIndex = 2002;
            this.label4.Text = "Account :";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(754, 1);
            this.label9.TabIndex = 5;
            this.label9.Text = "label1";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(0, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(754, 1);
            this.label10.TabIndex = 8;
            this.label10.Text = "label2";
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label72.Location = new System.Drawing.Point(3, 204);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(754, 1);
            this.label72.TabIndex = 2007;
            this.label72.Text = "label2";
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Dock = System.Windows.Forms.DockStyle.Top;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(4, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(752, 1);
            this.label71.TabIndex = 2006;
            this.label71.Text = "label1";
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Right;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label70.Location = new System.Drawing.Point(756, 0);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(1, 204);
            this.label70.TabIndex = 2005;
            this.label70.Text = "label3";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Left;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(3, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 204);
            this.label29.TabIndex = 2004;
            this.label29.Text = "label4";
            // 
            // panel_NoteDtl
            // 
            this.panel_NoteDtl.BackColor = System.Drawing.Color.Transparent;
            this.panel_NoteDtl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_NoteDtl.Controls.Add(this.label71);
            this.panel_NoteDtl.Controls.Add(this.groupBox2);
            this.panel_NoteDtl.Controls.Add(this.label29);
            this.panel_NoteDtl.Controls.Add(this.label70);
            this.panel_NoteDtl.Controls.Add(this.label72);
            this.panel_NoteDtl.Controls.Add(this.groupBox1);
            this.panel_NoteDtl.Controls.Add(this.groupBox3);
            this.panel_NoteDtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_NoteDtl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_NoteDtl.Location = new System.Drawing.Point(0, 87);
            this.panel_NoteDtl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel_NoteDtl.Name = "panel_NoteDtl";
            this.panel_NoteDtl.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel_NoteDtl.Size = new System.Drawing.Size(760, 208);
            this.panel_NoteDtl.TabIndex = 12;
            this.panel_NoteDtl.TabStop = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblPlanAmount);
            this.groupBox2.Controls.Add(this.txtPlanAmount);
            this.groupBox2.Location = new System.Drawing.Point(9, -3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.groupBox2.Size = new System.Drawing.Size(741, 39);
            this.groupBox2.TabIndex = 2023;
            this.groupBox2.TabStop = false;
            // 
            // lblPlanAmount
            // 
            this.lblPlanAmount.AutoSize = true;
            this.lblPlanAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlanAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPlanAmount.Location = new System.Drawing.Point(9, 16);
            this.lblPlanAmount.Name = "lblPlanAmount";
            this.lblPlanAmount.Size = new System.Drawing.Size(137, 14);
            this.lblPlanAmount.TabIndex = 2002;
            this.lblPlanAmount.Text = "Payment Plan Amount :";
            // 
            // txtPlanAmount
            // 
            this.txtPlanAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlanAmount.Location = new System.Drawing.Point(150, 12);
            this.txtPlanAmount.MaxLength = 13;
            this.txtPlanAmount.Name = "txtPlanAmount";
            this.txtPlanAmount.ShortcutsEnabled = false;
            this.txtPlanAmount.Size = new System.Drawing.Size(148, 22);
            this.txtPlanAmount.TabIndex = 2009;
            this.txtPlanAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPlanAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlanAmount_KeyPress);
            this.txtPlanAmount.Leave += new System.EventHandler(this.txtPlanAmount_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbFollowupAction);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.mskFollowupDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(9, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(741, 92);
            this.groupBox1.TabIndex = 2022;
            this.groupBox1.TabStop = false;
            // 
            // cmbFollowupAction
            // 
            this.cmbFollowupAction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFollowupAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFollowupAction.ForeColor = System.Drawing.Color.Black;
            this.cmbFollowupAction.FormattingEnabled = true;
            this.cmbFollowupAction.Items.AddRange(new object[] {
            ""});
            this.cmbFollowupAction.Location = new System.Drawing.Point(149, 37);
            this.cmbFollowupAction.Name = "cmbFollowupAction";
            this.cmbFollowupAction.Size = new System.Drawing.Size(276, 22);
            this.cmbFollowupAction.TabIndex = 2013;
            this.cmbFollowupAction.SelectedIndexChanged += new System.EventHandler(this.cmbFollowupAction_SelectedIndexChanged);
            this.cmbFollowupAction.MouseEnter += new System.EventHandler(this.cmbFollowupAction_MouseEnter);
            this.cmbFollowupAction.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmbFollowupAction_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(427, 14);
            this.label1.TabIndex = 2008;
            this.label1.Text = "Set Follow-up Date and Action if Payment Plan Amount is not Paid :";
            // 
            // label42
            // 
            this.label42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(105, 67);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(41, 14);
            this.label42.TabIndex = 2011;
            this.label42.Text = "Date :";
            // 
            // mskFollowupDate
            // 
            this.mskFollowupDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mskFollowupDate.Location = new System.Drawing.Point(149, 63);
            this.mskFollowupDate.Mask = "00/00/0000";
            this.mskFollowupDate.Name = "mskFollowupDate";
            this.mskFollowupDate.Size = new System.Drawing.Size(97, 22);
            this.mskFollowupDate.TabIndex = 2012;
            this.mskFollowupDate.ValidatingType = typeof(System.DateTime);
            this.mskFollowupDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskFollowupDate_MouseClick);
            this.mskFollowupDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskFollowupDate_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(96, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 2010;
            this.label2.Text = "Action :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBrowseNotes);
            this.groupBox3.Controls.Add(this.label_notes);
            this.groupBox3.Controls.Add(this.txtNotes);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(9, 123);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox3.Size = new System.Drawing.Size(741, 74);
            this.groupBox3.TabIndex = 2024;
            this.groupBox3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(522, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 14);
            this.label3.TabIndex = 2017;
            this.label3.Text = "Maximum 255 characters are allowed";
            // 
            // label_notes
            // 
            this.label_notes.AutoSize = true;
            this.label_notes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_notes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label_notes.Location = new System.Drawing.Point(103, 17);
            this.label_notes.Name = "label_notes";
            this.label_notes.Size = new System.Drawing.Size(42, 14);
            this.label_notes.TabIndex = 2016;
            this.label_notes.Text = "Note :";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.ForeColor = System.Drawing.Color.Black;
            this.txtNotes.Location = new System.Drawing.Point(148, 15);
            this.txtNotes.MaxLength = 255;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(370, 53);
            this.txtNotes.TabIndex = 2015;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlPatDetails);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 56);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(760, 31);
            this.panel2.TabIndex = 13;
            // 
            // btnBrowseNotes
            // 
            this.btnBrowseNotes.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseNotes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseNotes.BackgroundImage")));
            this.btnBrowseNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseNotes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseNotes.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseNotes.Image")));
            this.btnBrowseNotes.Location = new System.Drawing.Point(524, 15);
            this.btnBrowseNotes.Name = "btnBrowseNotes";
            this.btnBrowseNotes.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseNotes.TabIndex = 2018;
            this.btnBrowseNotes.Tag = "";
            this.btnBrowseNotes.UseVisualStyleBackColor = false;
            this.btnBrowseNotes.Click += new System.EventHandler(this.btnBrowseNotes_Click);
            this.btnBrowseNotes.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnBrowseNotes.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // frmSetupPaymentPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(760, 295);
            this.Controls.Add(this.panel_NoteDtl);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupPaymentPlan";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment Plan";
            this.Load += new System.EventHandler(this.frmSetupPaymentPlan_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Notes.ResumeLayout(false);
            this.tls_Notes.PerformLayout();
            this.pnlPatDetails.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_NoteDtl.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Notes;
        private System.Windows.Forms.ToolStripButton tlb_Ok;
        private System.Windows.Forms.ToolStripButton tlb_Close;
        private System.Windows.Forms.Panel pnlPatDetails;
        internal System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAccountNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label29;
        internal System.Windows.Forms.Panel panel_NoteDtl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblPlanAmount;
        private System.Windows.Forms.TextBox txtPlanAmount;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_notes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbFollowupAction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.MaskedTextBox mskFollowupDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnBrowseNotes;
    }
}