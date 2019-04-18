namespace gloAccountsV2
{
    partial class frmInsuranceReserveRemainingV2
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
                if (toolTip != null)
                {
                    toolTip.Dispose();
                    toolTip = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsuranceReserveRemainingV2));
            this.tls_SetupReserve = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsbtnSaveClose = new System.Windows.Forms.ToolStripButton();
            this.tlsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlBusinessCenter = new System.Windows.Forms.Panel();
            this.cmbBusinessCenter = new System.Windows.Forms.ComboBox();
            this.lblBusinessCenter = new System.Windows.Forms.Label();
            this.btnClearBusinessCenter = new System.Windows.Forms.Button();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.txtProvider = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSearchProvider = new System.Windows.Forms.Button();
            this.btnClearProvider = new System.Windows.Forms.Button();
            this.pnlInsuranceCompany = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.lblInsCompany = new System.Windows.Forms.Label();
            this.txtInsCompany = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPatient = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClearInsCompany = new System.Windows.Forms.Button();
            this.btnSearchPatient = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.btnClearPatient = new System.Windows.Forms.Button();
            this.btnSearchInsuranceCompany = new System.Windows.Forms.Button();
            this.cmbClaimNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tls_SetupReserve.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlBusinessCenter.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlInsuranceCompany.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_SetupReserve
            // 
            this.tls_SetupReserve.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupReserve.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_SetupReserve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_SetupReserve.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupReserve.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupReserve.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsbtnSaveClose,
            this.tlsbtnClose});
            this.tls_SetupReserve.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupReserve.Name = "tls_SetupReserve";
            this.tls_SetupReserve.Padding = new System.Windows.Forms.Padding(0);
            this.tls_SetupReserve.Size = new System.Drawing.Size(582, 53);
            this.tls_SetupReserve.TabIndex = 1;
            this.tls_SetupReserve.TabStop = true;
            // 
            // tlsbtnSaveClose
            // 
            this.tlsbtnSaveClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtnSaveClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsbtnSaveClose.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtnSaveClose.Image")));
            this.tlsbtnSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsbtnSaveClose.Name = "tlsbtnSaveClose";
            this.tlsbtnSaveClose.Size = new System.Drawing.Size(66, 50);
            this.tlsbtnSaveClose.Tag = "OK";
            this.tlsbtnSaveClose.Text = "&Save&&Cls";
            this.tlsbtnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtnSaveClose.ToolTipText = "Save and Close";
            this.tlsbtnSaveClose.Click += new System.EventHandler(this.tlsbtnSaveClose_Click);
            // 
            // tlsbtnClose
            // 
            this.tlsbtnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtnClose.Image")));
            this.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsbtnClose.Name = "tlsbtnClose";
            this.tlsbtnClose.Size = new System.Drawing.Size(43, 50);
            this.tlsbtnClose.Tag = "Cancel";
            this.tlsbtnClose.Text = "&Close";
            this.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtnClose.Click += new System.EventHandler(this.tlsbtnClose_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.pnlBusinessCenter);
            this.pnlMain.Controls.Add(this.pnlProvider);
            this.pnlMain.Controls.Add(this.pnlInsuranceCompany);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label59);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(582, 235);
            this.pnlMain.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(574, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // pnlBusinessCenter
            // 
            this.pnlBusinessCenter.Controls.Add(this.cmbBusinessCenter);
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCenter);
            this.pnlBusinessCenter.Controls.Add(this.btnClearBusinessCenter);
            this.pnlBusinessCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBusinessCenter.Location = new System.Drawing.Point(4, 228);
            this.pnlBusinessCenter.Name = "pnlBusinessCenter";
            this.pnlBusinessCenter.Size = new System.Drawing.Size(574, 4);
            this.pnlBusinessCenter.TabIndex = 269;
            this.pnlBusinessCenter.Visible = false;
            // 
            // cmbBusinessCenter
            // 
            this.cmbBusinessCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBusinessCenter.ForeColor = System.Drawing.Color.Black;
            this.cmbBusinessCenter.FormattingEnabled = true;
            this.cmbBusinessCenter.Location = new System.Drawing.Point(158, 4);
            this.cmbBusinessCenter.Name = "cmbBusinessCenter";
            this.cmbBusinessCenter.Size = new System.Drawing.Size(188, 22);
            this.cmbBusinessCenter.TabIndex = 268;
            this.cmbBusinessCenter.SelectedIndexChanged += new System.EventHandler(this.cmbBusinessCenter_SelectedIndexChanged);
            this.cmbBusinessCenter.MouseEnter += new System.EventHandler(this.cmbBusinessCenter_MouseEnter);
            // 
            // lblBusinessCenter
            // 
            this.lblBusinessCenter.AutoSize = true;
            this.lblBusinessCenter.BackColor = System.Drawing.Color.Transparent;
            this.lblBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblBusinessCenter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBusinessCenter.Location = new System.Drawing.Point(52, 8);
            this.lblBusinessCenter.Name = "lblBusinessCenter";
            this.lblBusinessCenter.Size = new System.Drawing.Size(101, 14);
            this.lblBusinessCenter.TabIndex = 267;
            this.lblBusinessCenter.Text = "Business Center :";
            this.lblBusinessCenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClearBusinessCenter
            // 
            this.btnClearBusinessCenter.AutoEllipsis = true;
            this.btnClearBusinessCenter.BackColor = System.Drawing.Color.Transparent;
            this.btnClearBusinessCenter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearBusinessCenter.BackgroundImage")));
            this.btnClearBusinessCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearBusinessCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearBusinessCenter.Image = ((System.Drawing.Image)(resources.GetObject("btnClearBusinessCenter.Image")));
            this.btnClearBusinessCenter.Location = new System.Drawing.Point(366, 5);
            this.btnClearBusinessCenter.Name = "btnClearBusinessCenter";
            this.btnClearBusinessCenter.Size = new System.Drawing.Size(21, 21);
            this.btnClearBusinessCenter.TabIndex = 266;
            this.toolTip1.SetToolTip(this.btnClearBusinessCenter, "Clear Business Center");
            this.btnClearBusinessCenter.UseVisualStyleBackColor = false;
            this.btnClearBusinessCenter.Visible = false;
            this.btnClearBusinessCenter.Click += new System.EventHandler(this.btnClearBusinessCenter_Click);
            // 
            // pnlProvider
            // 
            this.pnlProvider.Controls.Add(this.txtProvider);
            this.pnlProvider.Controls.Add(this.label8);
            this.pnlProvider.Controls.Add(this.btnSearchProvider);
            this.pnlProvider.Controls.Add(this.btnClearProvider);
            this.pnlProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProvider.Location = new System.Drawing.Point(4, 196);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(574, 32);
            this.pnlProvider.TabIndex = 270;
            // 
            // txtProvider
            // 
            this.txtProvider.BackColor = System.Drawing.Color.White;
            this.txtProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProvider.ForeColor = System.Drawing.Color.Black;
            this.txtProvider.Location = new System.Drawing.Point(159, 5);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.ReadOnly = true;
            this.txtProvider.ShortcutsEnabled = false;
            this.txtProvider.Size = new System.Drawing.Size(136, 22);
            this.txtProvider.TabIndex = 237;
            this.txtProvider.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(94, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 14);
            this.label8.TabIndex = 234;
            this.label8.Text = "Provider : ";
            // 
            // btnSearchProvider
            // 
            this.btnSearchProvider.AutoEllipsis = true;
            this.btnSearchProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchProvider.BackgroundImage")));
            this.btnSearchProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchProvider.Image")));
            this.btnSearchProvider.Location = new System.Drawing.Point(299, 6);
            this.btnSearchProvider.Name = "btnSearchProvider";
            this.btnSearchProvider.Size = new System.Drawing.Size(21, 21);
            this.btnSearchProvider.TabIndex = 235;
            this.btnSearchProvider.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchProvider, "Select Provider");
            this.btnSearchProvider.UseVisualStyleBackColor = false;
            this.btnSearchProvider.Click += new System.EventHandler(this.btnSearchProvider_Click);
            // 
            // btnClearProvider
            // 
            this.btnClearProvider.AutoEllipsis = true;
            this.btnClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.BackgroundImage")));
            this.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.Image")));
            this.btnClearProvider.Location = new System.Drawing.Point(324, 6);
            this.btnClearProvider.Name = "btnClearProvider";
            this.btnClearProvider.Size = new System.Drawing.Size(21, 21);
            this.btnClearProvider.TabIndex = 236;
            this.toolTip1.SetToolTip(this.btnClearProvider, "Clear Provider");
            this.btnClearProvider.UseVisualStyleBackColor = false;
            this.btnClearProvider.Click += new System.EventHandler(this.btnClearProvider_Click);
            // 
            // pnlInsuranceCompany
            // 
            this.pnlInsuranceCompany.Controls.Add(this.label5);
            this.pnlInsuranceCompany.Controls.Add(this.label4);
            this.pnlInsuranceCompany.Controls.Add(this.txtAmount);
            this.pnlInsuranceCompany.Controls.Add(this.txtNotes);
            this.pnlInsuranceCompany.Controls.Add(this.label14);
            this.pnlInsuranceCompany.Controls.Add(this.lblInsCompany);
            this.pnlInsuranceCompany.Controls.Add(this.txtInsCompany);
            this.pnlInsuranceCompany.Controls.Add(this.label7);
            this.pnlInsuranceCompany.Controls.Add(this.txtPatient);
            this.pnlInsuranceCompany.Controls.Add(this.label6);
            this.pnlInsuranceCompany.Controls.Add(this.btnClearInsCompany);
            this.pnlInsuranceCompany.Controls.Add(this.btnSearchPatient);
            this.pnlInsuranceCompany.Controls.Add(this.lblName);
            this.pnlInsuranceCompany.Controls.Add(this.btnClearPatient);
            this.pnlInsuranceCompany.Controls.Add(this.btnSearchInsuranceCompany);
            this.pnlInsuranceCompany.Controls.Add(this.cmbClaimNo);
            this.pnlInsuranceCompany.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInsuranceCompany.Location = new System.Drawing.Point(4, 4);
            this.pnlInsuranceCompany.Name = "pnlInsuranceCompany";
            this.pnlInsuranceCompany.Size = new System.Drawing.Size(574, 192);
            this.pnlInsuranceCompany.TabIndex = 269;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 14);
            this.label5.TabIndex = 35;
            this.label5.Text = "Insurance Company : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(106, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "Notes : ";
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.White;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.ForeColor = System.Drawing.Color.Black;
            this.txtAmount.Location = new System.Drawing.Point(159, 40);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ShortcutsEnabled = false;
            this.txtAmount.Size = new System.Drawing.Size(136, 22);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.ForeColor = System.Drawing.Color.Black;
            this.txtNotes.Location = new System.Drawing.Point(159, 67);
            this.txtNotes.MaxLength = 255;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(388, 94);
            this.txtNotes.TabIndex = 2;
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
            this.label14.Location = new System.Drawing.Point(19, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 14);
            this.label14.TabIndex = 34;
            this.label14.Text = "*";
            // 
            // lblInsCompany
            // 
            this.lblInsCompany.AutoSize = true;
            this.lblInsCompany.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.lblInsCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsCompany.Location = new System.Drawing.Point(156, 17);
            this.lblInsCompany.Name = "lblInsCompany";
            this.lblInsCompany.Size = new System.Drawing.Size(0, 14);
            this.lblInsCompany.TabIndex = 211;
            this.lblInsCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInsCompany.Visible = false;
            // 
            // txtInsCompany
            // 
            this.txtInsCompany.BackColor = System.Drawing.Color.White;
            this.txtInsCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsCompany.ForeColor = System.Drawing.Color.Black;
            this.txtInsCompany.Location = new System.Drawing.Point(159, 13);
            this.txtInsCompany.Name = "txtInsCompany";
            this.txtInsCompany.ReadOnly = true;
            this.txtInsCompany.ShortcutsEnabled = false;
            this.txtInsCompany.Size = new System.Drawing.Size(338, 22);
            this.txtInsCompany.TabIndex = 233;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(362, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 14);
            this.label7.TabIndex = 216;
            this.label7.Text = "Claim # :";
            // 
            // txtPatient
            // 
            this.txtPatient.BackColor = System.Drawing.Color.White;
            this.txtPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient.ForeColor = System.Drawing.Color.Black;
            this.txtPatient.Location = new System.Drawing.Point(159, 166);
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.ReadOnly = true;
            this.txtPatient.ShortcutsEnabled = false;
            this.txtPatient.Size = new System.Drawing.Size(136, 22);
            this.txtPatient.TabIndex = 232;
            this.txtPatient.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(99, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 14);
            this.label6.TabIndex = 215;
            this.label6.Text = "Patient : ";
            // 
            // btnClearInsCompany
            // 
            this.btnClearInsCompany.AutoEllipsis = true;
            this.btnClearInsCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsCompany.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsCompany.BackgroundImage")));
            this.btnClearInsCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsCompany.Image")));
            this.btnClearInsCompany.Location = new System.Drawing.Point(525, 14);
            this.btnClearInsCompany.Name = "btnClearInsCompany";
            this.btnClearInsCompany.Size = new System.Drawing.Size(21, 21);
            this.btnClearInsCompany.TabIndex = 230;
            this.toolTip1.SetToolTip(this.btnClearInsCompany, "Clear Insurance Company");
            this.btnClearInsCompany.UseVisualStyleBackColor = false;
            this.btnClearInsCompany.Click += new System.EventHandler(this.btnClearInsCompany_Click);
            // 
            // btnSearchPatient
            // 
            this.btnSearchPatient.AutoEllipsis = true;
            this.btnSearchPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchPatient.BackgroundImage")));
            this.btnSearchPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchPatient.Image")));
            this.btnSearchPatient.Location = new System.Drawing.Point(299, 167);
            this.btnSearchPatient.Name = "btnSearchPatient";
            this.btnSearchPatient.Size = new System.Drawing.Size(21, 21);
            this.btnSearchPatient.TabIndex = 228;
            this.btnSearchPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchPatient, "Select Patient");
            this.btnSearchPatient.UseVisualStyleBackColor = false;
            this.btnSearchPatient.Click += new System.EventHandler(this.btnSearchPatient_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(31, 44);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(126, 14);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Amount to Reserve : ";
            // 
            // btnClearPatient
            // 
            this.btnClearPatient.AutoEllipsis = true;
            this.btnClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.BackgroundImage")));
            this.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.Image")));
            this.btnClearPatient.Location = new System.Drawing.Point(324, 167);
            this.btnClearPatient.Name = "btnClearPatient";
            this.btnClearPatient.Size = new System.Drawing.Size(21, 21);
            this.btnClearPatient.TabIndex = 229;
            this.toolTip1.SetToolTip(this.btnClearPatient, "Clear Patient");
            this.btnClearPatient.UseVisualStyleBackColor = false;
            this.btnClearPatient.Click += new System.EventHandler(this.btnClearPatient_Click);
            // 
            // btnSearchInsuranceCompany
            // 
            this.btnSearchInsuranceCompany.AutoEllipsis = true;
            this.btnSearchInsuranceCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchInsuranceCompany.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchInsuranceCompany.BackgroundImage")));
            this.btnSearchInsuranceCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchInsuranceCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchInsuranceCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchInsuranceCompany.Image")));
            this.btnSearchInsuranceCompany.Location = new System.Drawing.Point(500, 14);
            this.btnSearchInsuranceCompany.Name = "btnSearchInsuranceCompany";
            this.btnSearchInsuranceCompany.Size = new System.Drawing.Size(21, 21);
            this.btnSearchInsuranceCompany.TabIndex = 229;
            this.btnSearchInsuranceCompany.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchInsuranceCompany, "Select Insurance Company");
            this.btnSearchInsuranceCompany.UseVisualStyleBackColor = false;
            this.btnSearchInsuranceCompany.Click += new System.EventHandler(this.btnSearchInsuranceCompany_Click);
            // 
            // cmbClaimNo
            // 
            this.cmbClaimNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbClaimNo.FormattingEnabled = true;
            this.cmbClaimNo.Location = new System.Drawing.Point(421, 166);
            this.cmbClaimNo.MaxLength = 15;
            this.cmbClaimNo.Name = "cmbClaimNo";
            this.cmbClaimNo.Size = new System.Drawing.Size(126, 22);
            this.cmbClaimNo.TabIndex = 230;
            this.cmbClaimNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbClaimNo_KeyPress);
            this.cmbClaimNo.Leave += new System.EventHandler(this.cmbClaimNo_Leave);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(578, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 228);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(575, 1);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 229);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tls_SetupReserve);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(582, 54);
            this.panel1.TabIndex = 234;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlMain);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(582, 289);
            this.panel2.TabIndex = 235;
            // 
            // frmInsuranceReserveRemainingV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(582, 289);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInsuranceReserveRemainingV2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insurance Reserve";
            this.Load += new System.EventHandler(this.frmInsuranceReserveRemaining_Load);
            this.tls_SetupReserve.ResumeLayout(false);
            this.tls_SetupReserve.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlBusinessCenter.ResumeLayout(false);
            this.pnlBusinessCenter.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
            this.pnlInsuranceCompany.ResumeLayout(false);
            this.pnlInsuranceCompany.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_SetupReserve;
        private System.Windows.Forms.ToolStripButton tlsbtnSaveClose;
        private System.Windows.Forms.ToolStripButton tlsbtnClose;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblInsCompany;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClearPatient;
        private System.Windows.Forms.Button btnSearchPatient;
        private System.Windows.Forms.ComboBox cmbClaimNo;
        private System.Windows.Forms.Button btnClearInsCompany;
        private System.Windows.Forms.Button btnSearchInsuranceCompany;
        private System.Windows.Forms.TextBox txtPatient;
        private System.Windows.Forms.TextBox txtInsCompany;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtProvider;
        private System.Windows.Forms.Button btnClearProvider;
        private System.Windows.Forms.Button btnSearchProvider;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClearBusinessCenter;
        internal System.Windows.Forms.ComboBox cmbBusinessCenter;
        internal System.Windows.Forms.Label lblBusinessCenter;
        private System.Windows.Forms.Panel pnlBusinessCenter;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Panel pnlInsuranceCompany;
    }
}