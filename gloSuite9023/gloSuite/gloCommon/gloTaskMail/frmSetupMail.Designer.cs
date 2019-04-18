namespace gloTaskMail
{
    partial class frmSetupMail
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
                    if (fileDialog != null)
                    {

                        fileDialog.Dispose();
                        fileDialog = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (saveFileDialog1 != null)
                    {

                        saveFileDialog1.Dispose();
                        saveFileDialog1 = null;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupMail));
            this.pnl_ToolStrip = new System.Windows.Forms.Panel();
            this.ts_Mail = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSend = new System.Windows.Forms.ToolStripButton();
            this.tsSeprator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnAttachment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnHighPriority = new System.Windows.Forms.ToolStripButton();
            this.ts_btnLowPriority = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnInsertSignature = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnSaveClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.pnl_MailTo = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.txtBCc = new System.Windows.Forms.TextBox();
            this.txtCc = new System.Windows.Forms.TextBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.btnBCc = new System.Windows.Forms.Button();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.btnCc = new System.Windows.Forms.Button();
            this.lblSubject = new System.Windows.Forms.Label();
            this.btnTo = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlflow_Attachment = new System.Windows.Forms.FlowLayoutPanel();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.pnl_ToolStrip.SuspendLayout();
            this.ts_Mail.SuspendLayout();
            this.pnl_MailTo.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_ToolStrip
            // 
            this.pnl_ToolStrip.Controls.Add(this.ts_Mail);
            this.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_ToolStrip.Name = "pnl_ToolStrip";
            this.pnl_ToolStrip.Size = new System.Drawing.Size(779, 53);
            this.pnl_ToolStrip.TabIndex = 1;
            // 
            // ts_Mail
            // 
            this.ts_Mail.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Toolstrip;
            this.ts_Mail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Mail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Mail.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Mail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnSend,
            this.tsSeprator1,
            this.ts_btnAttachment,
            this.toolStripSeparator1,
            this.ts_btnHighPriority,
            this.ts_btnLowPriority,
            this.toolStripSeparator2,
            this.ts_btnInsertSignature,
            this.toolStripSeparator3,
            this.ts_btnSave,
            this.ts_btnSaveClose,
            this.toolStripButton1});
            this.ts_Mail.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Mail.Location = new System.Drawing.Point(0, 0);
            this.ts_Mail.Name = "ts_Mail";
            this.ts_Mail.Size = new System.Drawing.Size(779, 53);
            this.ts_Mail.TabIndex = 5;
            this.ts_Mail.TabStop = true;
            this.ts_Mail.Text = "toolStrip1";
            this.ts_Mail.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Mail_ItemClicked);
            // 
            // ts_btnSend
            // 
            this.ts_btnSend.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSend.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSend.Image")));
            this.ts_btnSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSend.Name = "ts_btnSend";
            this.ts_btnSend.Size = new System.Drawing.Size(42, 50);
            this.ts_btnSend.Tag = "Send";
            this.ts_btnSend.Text = "&Send";
            this.ts_btnSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSend.ToolTipText = "Send";
            // 
            // tsSeprator1
            // 
            this.tsSeprator1.AutoSize = false;
            this.tsSeprator1.Name = "tsSeprator1";
            this.tsSeprator1.Size = new System.Drawing.Size(6, 53);
            this.tsSeprator1.Tag = "\"\"";
            // 
            // ts_btnAttachment
            // 
            this.ts_btnAttachment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnAttachment.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnAttachment.Image")));
            this.ts_btnAttachment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnAttachment.Name = "ts_btnAttachment";
            this.ts_btnAttachment.Size = new System.Drawing.Size(85, 50);
            this.ts_btnAttachment.Tag = "Attachment";
            this.ts_btnAttachment.Text = "&Attachment";
            this.ts_btnAttachment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            this.toolStripSeparator1.Tag = "\"\"";
            // 
            // ts_btnHighPriority
            // 
            this.ts_btnHighPriority.CheckOnClick = true;
            this.ts_btnHighPriority.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnHighPriority.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnHighPriority.Image")));
            this.ts_btnHighPriority.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnHighPriority.Name = "ts_btnHighPriority";
            this.ts_btnHighPriority.Size = new System.Drawing.Size(84, 50);
            this.ts_btnHighPriority.Tag = "HighPriority";
            this.ts_btnHighPriority.Text = "&HighPriority";
            this.ts_btnHighPriority.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnHighPriority.ToolTipText = "High Priority";
            // 
            // ts_btnLowPriority
            // 
            this.ts_btnLowPriority.CheckOnClick = true;
            this.ts_btnLowPriority.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnLowPriority.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnLowPriority.Image")));
            this.ts_btnLowPriority.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnLowPriority.Name = "ts_btnLowPriority";
            this.ts_btnLowPriority.Size = new System.Drawing.Size(82, 50);
            this.ts_btnLowPriority.Tag = "LowPriority";
            this.ts_btnLowPriority.Text = "&LowPriority";
            this.ts_btnLowPriority.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnLowPriority.ToolTipText = "Low Priority";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 53);
            this.toolStripSeparator2.Tag = "\"\"";
            // 
            // ts_btnInsertSignature
            // 
            this.ts_btnInsertSignature.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnInsertSignature.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnInsertSignature.Image")));
            this.ts_btnInsertSignature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnInsertSignature.Name = "ts_btnInsertSignature";
            this.ts_btnInsertSignature.Size = new System.Drawing.Size(71, 50);
            this.ts_btnInsertSignature.Tag = "Signature";
            this.ts_btnInsertSignature.Text = "&Signature";
            this.ts_btnInsertSignature.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnInsertSignature.ToolTipText = "Insert Signature";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 53);
            this.toolStripSeparator3.Tag = "\"\"";
            // 
            // ts_btnSaveClose
            // 
            this.ts_btnSaveClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSaveClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSaveClose.Image")));
            this.ts_btnSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSaveClose.Name = "ts_btnSaveClose";
            this.ts_btnSaveClose.Size = new System.Drawing.Size(66, 50);
            this.ts_btnSaveClose.Tag = "Save";
            this.ts_btnSaveClose.Text = "&Save&&Cls";
            this.ts_btnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSaveClose.ToolTipText = "Save and Close";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(43, 50);
            this.toolStripButton1.Tag = "Save";
            this.toolStripButton1.Text = "&Close";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnl_MailTo
            // 
            this.pnl_MailTo.Controls.Add(this.lbl_BottomBrd);
            this.pnl_MailTo.Controls.Add(this.lbl_LeftBrd);
            this.pnl_MailTo.Controls.Add(this.lbl_RightBrd);
            this.pnl_MailTo.Controls.Add(this.lbl_TopBrd);
            this.pnl_MailTo.Controls.Add(this.txtBCc);
            this.pnl_MailTo.Controls.Add(this.txtCc);
            this.pnl_MailTo.Controls.Add(this.txtTo);
            this.pnl_MailTo.Controls.Add(this.btnBCc);
            this.pnl_MailTo.Controls.Add(this.txtSubject);
            this.pnl_MailTo.Controls.Add(this.btnCc);
            this.pnl_MailTo.Controls.Add(this.lblSubject);
            this.pnl_MailTo.Controls.Add(this.btnTo);
            this.pnl_MailTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_MailTo.Location = new System.Drawing.Point(0, 0);
            this.pnl_MailTo.Name = "pnl_MailTo";
            this.pnl_MailTo.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_MailTo.Size = new System.Drawing.Size(779, 135);
            this.pnl_MailTo.TabIndex = 1;
            this.pnl_MailTo.TabStop = true;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 131);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(771, 1);
            this.lbl_BottomBrd.TabIndex = 55;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 128);
            this.lbl_LeftBrd.TabIndex = 54;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(775, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 128);
            this.lbl_RightBrd.TabIndex = 53;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(773, 1);
            this.lbl_TopBrd.TabIndex = 52;
            this.lbl_TopBrd.Text = "label1";
            // 
            // txtBCc
            // 
            this.txtBCc.ForeColor = System.Drawing.Color.Black;
            this.txtBCc.Location = new System.Drawing.Point(75, 70);
            this.txtBCc.Name = "txtBCc";
            this.txtBCc.Size = new System.Drawing.Size(684, 22);
            this.txtBCc.TabIndex = 3;
            // 
            // txtCc
            // 
            this.txtCc.ForeColor = System.Drawing.Color.Black;
            this.txtCc.Location = new System.Drawing.Point(75, 42);
            this.txtCc.Name = "txtCc";
            this.txtCc.Size = new System.Drawing.Size(684, 22);
            this.txtCc.TabIndex = 2;
            // 
            // txtTo
            // 
            this.txtTo.ForeColor = System.Drawing.Color.Black;
            this.txtTo.Location = new System.Drawing.Point(75, 14);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(684, 22);
            this.txtTo.TabIndex = 1;
            this.txtTo.TextChanged += new System.EventHandler(this.txtTo_TextChanged);
            // 
            // btnBCc
            // 
            this.btnBCc.BackColor = System.Drawing.Color.Transparent;
            this.btnBCc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBCc.BackgroundImage")));
            this.btnBCc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBCc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBCc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBCc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBCc.Location = new System.Drawing.Point(16, 69);
            this.btnBCc.Name = "btnBCc";
            this.btnBCc.Size = new System.Drawing.Size(51, 23);
            this.btnBCc.TabIndex = 48;
            this.btnBCc.Text = "Bcc...";
            this.btnBCc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBCc.UseVisualStyleBackColor = false;
            this.btnBCc.TextChanged += new System.EventHandler(this.btnBCc_TextChanged);
            this.btnBCc.Click += new System.EventHandler(this.btnBCc_Click);
            // 
            // txtSubject
            // 
            this.txtSubject.ForeColor = System.Drawing.Color.Black;
            this.txtSubject.Location = new System.Drawing.Point(75, 98);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(684, 22);
            this.txtSubject.TabIndex = 4;
            // 
            // btnCc
            // 
            this.btnCc.BackColor = System.Drawing.Color.Transparent;
            this.btnCc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCc.BackgroundImage")));
            this.btnCc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnCc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCc.Location = new System.Drawing.Point(16, 41);
            this.btnCc.Name = "btnCc";
            this.btnCc.Size = new System.Drawing.Size(51, 23);
            this.btnCc.TabIndex = 45;
            this.btnCc.Text = "Cc...";
            this.btnCc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCc.UseVisualStyleBackColor = false;
            this.btnCc.TextChanged += new System.EventHandler(this.btnCc_TextChanged);
            this.btnCc.Click += new System.EventHandler(this.btnCc_Click);
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubject.Location = new System.Drawing.Point(11, 102);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(54, 14);
            this.lblSubject.TabIndex = 43;
            this.lblSubject.Text = "Subject";
            // 
            // btnTo
            // 
            this.btnTo.BackColor = System.Drawing.Color.Transparent;
            this.btnTo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTo.BackgroundImage")));
            this.btnTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTo.Location = new System.Drawing.Point(16, 13);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(51, 23);
            this.btnTo.TabIndex = 37;
            this.btnTo.Text = "To...";
            this.btnTo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTo.UseVisualStyleBackColor = false;
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.label1);
            this.pnlBody.Controls.Add(this.label2);
            this.pnlBody.Controls.Add(this.label3);
            this.pnlBody.Controls.Add(this.label4);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 135);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlBody.Size = new System.Drawing.Size(779, 497);
            this.pnlBody.TabIndex = 2;
            this.pnlBody.TabStop = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(4, 493);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(771, 1);
            this.label1.TabIndex = 8;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 492);
            this.label2.TabIndex = 7;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(775, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 492);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(773, 1);
            this.label4.TabIndex = 5;
            this.label4.Text = "label1";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlBody);
            this.pnlMain.Controls.Add(this.pnlflow_Attachment);
            this.pnlMain.Controls.Add(this.pnl_MailTo);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 53);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(779, 632);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlflow_Attachment
            // 
            this.pnlflow_Attachment.AutoSize = true;
            this.pnlflow_Attachment.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlflow_Attachment.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlflow_Attachment.Location = new System.Drawing.Point(0, 135);
            this.pnlflow_Attachment.Name = "pnlflow_Attachment";
            this.pnlflow_Attachment.Size = new System.Drawing.Size(779, 0);
            this.pnlflow_Attachment.TabIndex = 0;
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "File";
            this.fileDialog.Title = "Select File to Attach";
            // 
            // ts_btnSave
            // 
            this.ts_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSave.Image")));
            this.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSave.Name = "ts_btnSave";
            this.ts_btnSave.Size = new System.Drawing.Size(40, 50);
            this.ts_btnSave.Tag = "Save";
            this.ts_btnSave.Text = "&Save";
            this.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSave.ToolTipText = "Save";
            // 
            // frmSetupMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(779, 685);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnl_ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupMail";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gloMail";
            this.Load += new System.EventHandler(this.frmSetupMail_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetupMail_FormClosing);
            this.pnl_ToolStrip.ResumeLayout(false);
            this.pnl_ToolStrip.PerformLayout();
            this.ts_Mail.ResumeLayout(false);
            this.ts_Mail.PerformLayout();
            this.pnl_MailTo.ResumeLayout(false);
            this.pnl_MailTo.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_ToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus ts_Mail;
        private System.Windows.Forms.ToolStripButton ts_btnSend;
        private System.Windows.Forms.ToolStripButton ts_btnSaveClose;
        private System.Windows.Forms.ToolStripSeparator tsSeprator1;
        private System.Windows.Forms.ToolStripButton ts_btnAttachment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ts_btnHighPriority;
        private System.Windows.Forms.ToolStripButton ts_btnLowPriority;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ts_btnInsertSignature;
        private System.Windows.Forms.Panel pnl_MailTo;
        private System.Windows.Forms.Button btnTo;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Button btnCc;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnBCc;
        private System.Windows.Forms.TextBox txtBCc;
        private System.Windows.Forms.TextBox txtCc;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.FlowLayoutPanel pnlflow_Attachment;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        internal System.Windows.Forms.ToolStripButton ts_btnSave;
    }
}