namespace gloBilling
{
    partial class frmSetupICD9
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupICD9));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_save = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.txtICD9Code = new System.Windows.Forms.TextBox();
            this.lblICD9Code = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cmbSpecialty = new System.Windows.Forms.ComboBox();
            this.lblSpecialty = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_ConceptID = new System.Windows.Forms.Label();
            this.txtsnodesc = new System.Windows.Forms.TextBox();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.lbl_SnomedDescription = new System.Windows.Forms.Label();
            this.lbl_ConceptID = new System.Windows.Forms.Label();
            this.btn_SnomedCode = new System.Windows.Forms.Button();
            this.gb_Immediacy = new System.Windows.Forms.GroupBox();
            this.rbtn_Unknown = new System.Windows.Forms.RadioButton();
            this.rbtn_Chronic = new System.Windows.Forms.RadioButton();
            this.rbt_Acute = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbInactive = new System.Windows.Forms.RadioButton();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ImgICDSetup = new System.Windows.Forms.ImageList(this.components);
            this.pnlICDNotes = new System.Windows.Forms.Panel();
            this.ucICDNotes = new gloGlobal.ICD.gloICDNotes();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.gb_Immediacy.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlICDNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_save,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(830, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Tag = "6";
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_save
            // 
            this.tsb_save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_save.Image")));
            this.tsb_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_save.Name = "tsb_save";
            this.tsb_save.Size = new System.Drawing.Size(40, 50);
            this.tsb_save.Tag = "save";
            this.tsb_save.Text = "&Save";
            this.tsb_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_save.ToolTipText = "Save";
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "Sa&ve&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
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
            // 
            // txtICD9Code
            // 
            this.txtICD9Code.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtICD9Code.ForeColor = System.Drawing.Color.Black;
            this.txtICD9Code.Location = new System.Drawing.Point(211, 16);
            this.txtICD9Code.MaxLength = 50;
            this.txtICD9Code.Name = "txtICD9Code";
            this.txtICD9Code.Size = new System.Drawing.Size(308, 22);
            this.txtICD9Code.TabIndex = 0;
            this.txtICD9Code.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtICD9Code_KeyPress);
            // 
            // lblICD9Code
            // 
            this.lblICD9Code.AutoSize = true;
            this.lblICD9Code.BackColor = System.Drawing.Color.Transparent;
            this.lblICD9Code.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblICD9Code.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblICD9Code.Location = new System.Drawing.Point(17, 0);
            this.lblICD9Code.Name = "lblICD9Code";
            this.lblICD9Code.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblICD9Code.Size = new System.Drawing.Size(77, 16);
            this.lblICD9Code.TabIndex = 18;
            this.lblICD9Code.Text = " ICD9 Code :";
            this.lblICD9Code.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(211, 45);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(308, 43);
            this.txtDescription.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(131, 50);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(75, 14);
            this.lblDescription.TabIndex = 21;
            this.lblDescription.Text = "Description :";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSpecialty
            // 
            this.cmbSpecialty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecialty.ForeColor = System.Drawing.Color.Black;
            this.cmbSpecialty.Location = new System.Drawing.Point(211, 95);
            this.cmbSpecialty.Name = "cmbSpecialty";
            this.cmbSpecialty.Size = new System.Drawing.Size(308, 22);
            this.cmbSpecialty.TabIndex = 2;
            // 
            // lblSpecialty
            // 
            this.lblSpecialty.AutoSize = true;
            this.lblSpecialty.BackColor = System.Drawing.Color.Transparent;
            this.lblSpecialty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpecialty.Location = new System.Drawing.Point(143, 98);
            this.lblSpecialty.Name = "lblSpecialty";
            this.lblSpecialty.Size = new System.Drawing.Size(63, 14);
            this.lblSpecialty.TabIndex = 23;
            this.lblSpecialty.Text = "Specialty :";
            this.lblSpecialty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.txtsnodesc);
            this.panel1.Controls.Add(this.btn_Delete);
            this.panel1.Controls.Add(this.lbl_SnomedDescription);
            this.panel1.Controls.Add(this.lbl_ConceptID);
            this.panel1.Controls.Add(this.btn_SnomedCode);
            this.panel1.Controls.Add(this.gb_Immediacy);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblSpecialty);
            this.panel1.Controls.Add(this.txtICD9Code);
            this.panel1.Controls.Add(this.cmbSpecialty);
            this.panel1.Controls.Add(this.txtDescription);
            this.panel1.Controls.Add(this.lblDescription);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(830, 233);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.txt_ConceptID);
            this.panel5.Location = new System.Drawing.Point(211, 123);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(308, 23);
            this.panel5.TabIndex = 226;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Silver;
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(1, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(306, 1);
            this.label10.TabIndex = 229;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Silver;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(306, 1);
            this.label9.TabIndex = 228;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Silver;
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(307, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 23);
            this.label8.TabIndex = 227;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Silver;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 23);
            this.label7.TabIndex = 226;
            // 
            // txt_ConceptID
            // 
            this.txt_ConceptID.AutoEllipsis = true;
            this.txt_ConceptID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ConceptID.ForeColor = System.Drawing.Color.Black;
            this.txt_ConceptID.Location = new System.Drawing.Point(0, 0);
            this.txt_ConceptID.Name = "txt_ConceptID";
            this.txt_ConceptID.Padding = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.txt_ConceptID.Size = new System.Drawing.Size(308, 23);
            this.txt_ConceptID.TabIndex = 225;
            this.txt_ConceptID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtsnodesc
            // 
            this.txtsnodesc.Enabled = false;
            this.txtsnodesc.Location = new System.Drawing.Point(706, 45);
            this.txtsnodesc.Name = "txtsnodesc";
            this.txtsnodesc.Size = new System.Drawing.Size(81, 22);
            this.txtsnodesc.TabIndex = 224;
            this.txtsnodesc.Visible = false;
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.Transparent;
            this.btn_Delete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Delete.BackgroundImage")));
            this.btn_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Delete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_Delete.Image")));
            this.btn_Delete.Location = new System.Drawing.Point(548, 123);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(21, 21);
            this.btn_Delete.TabIndex = 223;
            this.btn_Delete.UseVisualStyleBackColor = false;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // lbl_SnomedDescription
            // 
            this.lbl_SnomedDescription.AutoSize = true;
            this.lbl_SnomedDescription.BackColor = System.Drawing.Color.Transparent;
            this.lbl_SnomedDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SnomedDescription.Location = new System.Drawing.Point(574, 48);
            this.lbl_SnomedDescription.Name = "lbl_SnomedDescription";
            this.lbl_SnomedDescription.Size = new System.Drawing.Size(127, 14);
            this.lbl_SnomedDescription.TabIndex = 221;
            this.lbl_SnomedDescription.Text = "SNOMED Description :";
            this.lbl_SnomedDescription.Visible = false;
            // 
            // lbl_ConceptID
            // 
            this.lbl_ConceptID.AutoSize = true;
            this.lbl_ConceptID.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ConceptID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ConceptID.Location = new System.Drawing.Point(141, 127);
            this.lbl_ConceptID.Name = "lbl_ConceptID";
            this.lbl_ConceptID.Size = new System.Drawing.Size(63, 14);
            this.lbl_ConceptID.TabIndex = 220;
            this.lbl_ConceptID.Text = "SNOMED :";
            // 
            // btn_SnomedCode
            // 
            this.btn_SnomedCode.BackColor = System.Drawing.Color.Transparent;
            this.btn_SnomedCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_SnomedCode.BackgroundImage")));
            this.btn_SnomedCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_SnomedCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SnomedCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_SnomedCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SnomedCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SnomedCode.Image = ((System.Drawing.Image)(resources.GetObject("btn_SnomedCode.Image")));
            this.btn_SnomedCode.Location = new System.Drawing.Point(524, 123);
            this.btn_SnomedCode.Name = "btn_SnomedCode";
            this.btn_SnomedCode.Size = new System.Drawing.Size(21, 21);
            this.btn_SnomedCode.TabIndex = 218;
            this.btn_SnomedCode.Text = "strSnomedDescription";
            this.btn_SnomedCode.UseVisualStyleBackColor = false;
            this.btn_SnomedCode.Click += new System.EventHandler(this.btn_SnomedCode_Click);
            // 
            // gb_Immediacy
            // 
            this.gb_Immediacy.Controls.Add(this.rbtn_Unknown);
            this.gb_Immediacy.Controls.Add(this.rbtn_Chronic);
            this.gb_Immediacy.Controls.Add(this.rbt_Acute);
            this.gb_Immediacy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_Immediacy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gb_Immediacy.Location = new System.Drawing.Point(208, 181);
            this.gb_Immediacy.Name = "gb_Immediacy";
            this.gb_Immediacy.Size = new System.Drawing.Size(385, 47);
            this.gb_Immediacy.TabIndex = 113;
            this.gb_Immediacy.TabStop = false;
            this.gb_Immediacy.Text = "Default Problem Immediacy";
            // 
            // rbtn_Unknown
            // 
            this.rbtn_Unknown.AutoSize = true;
            this.rbtn_Unknown.Checked = true;
            this.rbtn_Unknown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Unknown.Location = new System.Drawing.Point(243, 19);
            this.rbtn_Unknown.Name = "rbtn_Unknown";
            this.rbtn_Unknown.Size = new System.Drawing.Size(83, 18);
            this.rbtn_Unknown.TabIndex = 2;
            this.rbtn_Unknown.TabStop = true;
            this.rbtn_Unknown.Text = "Unknown";
            this.rbtn_Unknown.UseVisualStyleBackColor = true;
            this.rbtn_Unknown.CheckedChanged += new System.EventHandler(this.rbtn_Unknown_CheckedChanged);
            // 
            // rbtn_Chronic
            // 
            this.rbtn_Chronic.AutoSize = true;
            this.rbtn_Chronic.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Chronic.Location = new System.Drawing.Point(142, 19);
            this.rbtn_Chronic.Name = "rbtn_Chronic";
            this.rbtn_Chronic.Size = new System.Drawing.Size(65, 18);
            this.rbtn_Chronic.TabIndex = 1;
            this.rbtn_Chronic.TabStop = true;
            this.rbtn_Chronic.Text = "Chronic";
            this.rbtn_Chronic.UseVisualStyleBackColor = true;
            this.rbtn_Chronic.CheckedChanged += new System.EventHandler(this.rbtn_Chronic_CheckedChanged);
            // 
            // rbt_Acute
            // 
            this.rbt_Acute.AutoSize = true;
            this.rbt_Acute.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbt_Acute.Location = new System.Drawing.Point(41, 19);
            this.rbt_Acute.Name = "rbt_Acute";
            this.rbt_Acute.Size = new System.Drawing.Size(58, 18);
            this.rbt_Acute.TabIndex = 0;
            this.rbt_Acute.TabStop = true;
            this.rbt_Acute.Text = "Acute";
            this.rbt_Acute.UseVisualStyleBackColor = true;
            this.rbt_Acute.CheckedChanged += new System.EventHandler(this.rbt_Acute_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(156, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 14);
            this.label1.TabIndex = 50;
            this.label1.Text = "Status :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbInactive);
            this.panel3.Controls.Add(this.rbActive);
            this.panel3.Location = new System.Drawing.Point(212, 153);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(165, 22);
            this.panel3.TabIndex = 3;
            // 
            // rbInactive
            // 
            this.rbInactive.AutoSize = true;
            this.rbInactive.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbInactive.Location = new System.Drawing.Point(97, 0);
            this.rbInactive.Name = "rbInactive";
            this.rbInactive.Size = new System.Drawing.Size(68, 22);
            this.rbInactive.TabIndex = 1;
            this.rbInactive.Text = "Inactive";
            this.rbInactive.UseVisualStyleBackColor = true;
            this.rbInactive.CheckedChanged += new System.EventHandler(this.rbInactive_CheckedChanged);
            // 
            // rbActive
            // 
            this.rbActive.AutoSize = true;
            this.rbActive.Checked = true;
            this.rbActive.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbActive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbActive.Location = new System.Drawing.Point(0, 0);
            this.rbActive.Name = "rbActive";
            this.rbActive.Size = new System.Drawing.Size(63, 22);
            this.rbActive.TabIndex = 0;
            this.rbActive.TabStop = true;
            this.rbActive.Text = "Active";
            this.rbActive.UseVisualStyleBackColor = true;
            this.rbActive.CheckedChanged += new System.EventHandler(this.rbActive_CheckedChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(4, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(822, 1);
            this.label6.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(4, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(822, 1);
            this.label4.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 227);
            this.label3.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(826, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 227);
            this.label2.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(116, 50);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(14, 14);
            this.label5.TabIndex = 111;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.lblICD9Code);
            this.panel4.Location = new System.Drawing.Point(111, 16);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(94, 20);
            this.panel4.TabIndex = 114;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(3, 0);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 110;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ts_Commands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(830, 54);
            this.panel2.TabIndex = 1;
            // 
            // ImgICDSetup
            // 
            this.ImgICDSetup.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgICDSetup.ImageStream")));
            this.ImgICDSetup.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgICDSetup.Images.SetKeyName(0, "ICD 09.ico");
            this.ImgICDSetup.Images.SetKeyName(1, "ICD 10.ico");
            this.ImgICDSetup.Images.SetKeyName(2, "ICD.ico");
            // 
            // pnlICDNotes
            // 
            this.pnlICDNotes.Controls.Add(this.ucICDNotes);
            this.pnlICDNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlICDNotes.Location = new System.Drawing.Point(0, 287);
            this.pnlICDNotes.Name = "pnlICDNotes";
            this.pnlICDNotes.Size = new System.Drawing.Size(830, 406);
            this.pnlICDNotes.TabIndex = 3;
            // 
            // ucICDNotes
            // 
            this.ucICDNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ucICDNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucICDNotes.Location = new System.Drawing.Point(0, 0);
            this.ucICDNotes.Name = "ucICDNotes";
            this.ucICDNotes.Size = new System.Drawing.Size(830, 406);
            this.ucICDNotes.TabIndex = 0;
            // 
            // frmSetupICD9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(830, 693);
            this.Controls.Add(this.pnlICDNotes);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupICD9";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ICD9 Setup";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSetupICD9_FormClosed);
            this.Load += new System.EventHandler(this.frmSetupICD9_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.gb_Immediacy.ResumeLayout(false);
            this.gb_Immediacy.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlICDNotes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.TextBox txtICD9Code;
        private System.Windows.Forms.Label lblICD9Code;
        internal System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        internal System.Windows.Forms.ComboBox cmbSpecialty;
        private System.Windows.Forms.Label lblSpecialty;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbInactive;
        private System.Windows.Forms.RadioButton rbActive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label19;
        internal System.Windows.Forms.ToolStripButton tsb_save;
        internal System.Windows.Forms.GroupBox gb_Immediacy;
        internal System.Windows.Forms.RadioButton rbtn_Unknown;
        internal System.Windows.Forms.RadioButton rbtn_Chronic;
        internal System.Windows.Forms.RadioButton rbt_Acute;
        private System.Windows.Forms.ImageList ImgICDSetup;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlICDNotes;
        private gloGlobal.ICD.gloICDNotes ucICDNotes;
        internal System.Windows.Forms.Button btn_Delete;
        internal System.Windows.Forms.Label lbl_SnomedDescription;
        internal System.Windows.Forms.Label lbl_ConceptID;
        internal System.Windows.Forms.Button btn_SnomedCode;
        private System.Windows.Forms.TextBox txtsnodesc;
        private System.Windows.Forms.Label txt_ConceptID;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}