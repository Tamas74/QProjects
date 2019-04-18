namespace gloBilling
{
    partial class frmDrugBilling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDrugBilling));
            this.PnlFields = new System.Windows.Forms.Panel();
            this.txtPrescriptionDesc = new System.Windows.Forms.TextBox();
            this.chkIncDesc = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPrescription = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNDCCode = new System.Windows.Forms.Label();
            this.txtNDCCode = new System.Windows.Forms.TextBox();
            this.txtNDCQty = new System.Windows.Forms.TextBox();
            this.lblNDCQty = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDOSText = new System.Windows.Forms.Label();
            this.lblCPTDescText = new System.Windows.Forms.Label();
            this.lblMod2Text = new System.Windows.Forms.Label();
            this.lblMod1Text = new System.Windows.Forms.Label();
            this.lblCPTCodeText = new System.Windows.Forms.Label();
            this.cmbNDCCode = new System.Windows.Forms.ComboBox();
            this.lblNDCCodetext = new System.Windows.Forms.Label();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btnAddNDCCode = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tls_Notes = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_Close = new System.Windows.Forms.ToolStripButton();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.PnlFields.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tls_Notes.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlFields
            // 
            this.PnlFields.Controls.Add(this.txtPrescriptionDesc);
            this.PnlFields.Controls.Add(this.chkIncDesc);
            this.PnlFields.Controls.Add(this.label12);
            this.PnlFields.Controls.Add(this.label11);
            this.PnlFields.Controls.Add(this.txtPrescription);
            this.PnlFields.Controls.Add(this.label10);
            this.PnlFields.Controls.Add(this.label9);
            this.PnlFields.Controls.Add(this.label8);
            this.PnlFields.Controls.Add(this.label7);
            this.PnlFields.Controls.Add(this.label6);
            this.PnlFields.Controls.Add(this.label5);
            this.PnlFields.Controls.Add(this.groupBox1);
            this.PnlFields.Controls.Add(this.label4);
            this.PnlFields.Controls.Add(this.label3);
            this.PnlFields.Controls.Add(this.label2);
            this.PnlFields.Controls.Add(this.label1);
            this.PnlFields.Controls.Add(this.lblDOSText);
            this.PnlFields.Controls.Add(this.lblCPTDescText);
            this.PnlFields.Controls.Add(this.lblMod2Text);
            this.PnlFields.Controls.Add(this.lblMod1Text);
            this.PnlFields.Controls.Add(this.lblCPTCodeText);
            this.PnlFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlFields.Location = new System.Drawing.Point(0, 54);
            this.PnlFields.Name = "PnlFields";
            this.PnlFields.Padding = new System.Windows.Forms.Padding(3);
            this.PnlFields.Size = new System.Drawing.Size(594, 285);
            this.PnlFields.TabIndex = 1;
            // 
            // txtPrescriptionDesc
            // 
            this.txtPrescriptionDesc.Location = new System.Drawing.Point(105, 222);
            this.txtPrescriptionDesc.MaxLength = 80;
            this.txtPrescriptionDesc.Multiline = true;
            this.txtPrescriptionDesc.Name = "txtPrescriptionDesc";
            this.txtPrescriptionDesc.ReadOnly = true;
            this.txtPrescriptionDesc.Size = new System.Drawing.Size(477, 49);
            this.txtPrescriptionDesc.TabIndex = 36;
            // 
            // chkIncDesc
            // 
            this.chkIncDesc.AutoSize = true;
            this.chkIncDesc.Location = new System.Drawing.Point(15, 224);
            this.chkIncDesc.Name = "chkIncDesc";
            this.chkIncDesc.Size = new System.Drawing.Size(89, 18);
            this.chkIncDesc.TabIndex = 35;
            this.chkIncDesc.Text = "Inc. Desc. :";
            this.chkIncDesc.UseVisualStyleBackColor = true;
            this.chkIncDesc.CheckedChanged += new System.EventHandler(this.chkIncDesc_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(12, 190);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(503, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "Note : For compound billing, enter a matching prescription number with each servi" +
                "ce line";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 162);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 14);
            this.label11.TabIndex = 33;
            this.label11.Text = "Prescription # :";
            // 
            // txtPrescription
            // 
            this.txtPrescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrescription.Location = new System.Drawing.Point(105, 159);
            this.txtPrescription.MaxLength = 50;
            this.txtPrescription.Name = "txtPrescription";
            this.txtPrescription.Size = new System.Drawing.Size(123, 22);
            this.txtPrescription.TabIndex = 32;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(12, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(289, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Note : Enter NDC 07234-0982-29 as ‘07234098229’";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(238, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 14);
            this.label9.TabIndex = 30;
            this.label9.Text = "Description";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(201, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 14);
            this.label8.TabIndex = 30;
            this.label8.Text = "M2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(163, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 14);
            this.label7.TabIndex = 30;
            this.label7.Text = "M1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(94, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 14);
            this.label6.TabIndex = 30;
            this.label6.Text = "CPT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 14);
            this.label5.TabIndex = 30;
            this.label5.Text = "DOS";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNDCCode);
            this.groupBox1.Controls.Add(this.txtNDCCode);
            this.groupBox1.Controls.Add(this.txtNDCQty);
            this.groupBox1.Controls.Add(this.lblNDCQty);
            this.groupBox1.Controls.Add(this.cmbUnit);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox1.Location = new System.Drawing.Point(10, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 51);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NDC";
            // 
            // lblNDCCode
            // 
            this.lblNDCCode.AutoSize = true;
            this.lblNDCCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNDCCode.Location = new System.Drawing.Point(6, 23);
            this.lblNDCCode.Name = "lblNDCCode";
            this.lblNDCCode.Size = new System.Drawing.Size(70, 14);
            this.lblNDCCode.TabIndex = 5;
            this.lblNDCCode.Text = "NDC Code :";
            // 
            // txtNDCCode
            // 
            this.txtNDCCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNDCCode.Location = new System.Drawing.Point(78, 19);
            this.txtNDCCode.MaxLength = 11;
            this.txtNDCCode.Name = "txtNDCCode";
            this.txtNDCCode.Size = new System.Drawing.Size(140, 22);
            this.txtNDCCode.TabIndex = 11;
            this.txtNDCCode.TextChanged += new System.EventHandler(this.txtNDCCode_TextChanged);
            this.txtNDCCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNDCCode_KeyDown);
            this.txtNDCCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNDCCode_KeyPress);
           
            // 
            // txtNDCQty
            // 
            this.txtNDCQty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNDCQty.Location = new System.Drawing.Point(288, 19);
            this.txtNDCQty.MaxLength = 13;
            this.txtNDCQty.Name = "txtNDCQty";
            this.txtNDCQty.Size = new System.Drawing.Size(101, 22);
            this.txtNDCQty.TabIndex = 14;
            this.txtNDCQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNDCQty_KeyDown);
            this.txtNDCQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNDCQty_KeyPress);
            // 
            // lblNDCQty
            // 
            this.lblNDCQty.AutoSize = true;
            this.lblNDCQty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNDCQty.Location = new System.Drawing.Point(224, 23);
            this.lblNDCQty.Name = "lblNDCQty";
            this.lblNDCQty.Size = new System.Drawing.Size(62, 14);
            this.lblNDCQty.TabIndex = 6;
            this.lblNDCQty.Text = "Quantity :";
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(395, 19);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(167, 22);
            this.cmbUnit.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(590, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 277);
            this.label4.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 277);
            this.label3.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(588, 1);
            this.label2.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(588, 1);
            this.label1.TabIndex = 25;
            // 
            // lblDOSText
            // 
            this.lblDOSText.BackColor = System.Drawing.Color.Transparent;
            this.lblDOSText.Location = new System.Drawing.Point(12, 35);
            this.lblDOSText.Name = "lblDOSText";
            this.lblDOSText.Size = new System.Drawing.Size(73, 14);
            this.lblDOSText.TabIndex = 20;
            this.lblDOSText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPTDescText
            // 
            this.lblCPTDescText.AutoEllipsis = true;
            this.lblCPTDescText.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTDescText.Location = new System.Drawing.Point(238, 35);
            this.lblCPTDescText.Name = "lblCPTDescText";
            this.lblCPTDescText.Size = new System.Drawing.Size(344, 14);
            this.lblCPTDescText.TabIndex = 19;
            this.lblCPTDescText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMod2Text
            // 
            this.lblMod2Text.BackColor = System.Drawing.Color.Transparent;
            this.lblMod2Text.Location = new System.Drawing.Point(201, 35);
            this.lblMod2Text.Name = "lblMod2Text";
            this.lblMod2Text.Size = new System.Drawing.Size(31, 14);
            this.lblMod2Text.TabIndex = 18;
            this.lblMod2Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMod1Text
            // 
            this.lblMod1Text.BackColor = System.Drawing.Color.Transparent;
            this.lblMod1Text.Location = new System.Drawing.Point(163, 35);
            this.lblMod1Text.Name = "lblMod1Text";
            this.lblMod1Text.Size = new System.Drawing.Size(31, 14);
            this.lblMod1Text.TabIndex = 17;
            this.lblMod1Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPTCodeText
            // 
            this.lblCPTCodeText.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTCodeText.Location = new System.Drawing.Point(94, 35);
            this.lblCPTCodeText.Name = "lblCPTCodeText";
            this.lblCPTCodeText.Size = new System.Drawing.Size(67, 14);
            this.lblCPTCodeText.TabIndex = 16;
            this.lblCPTCodeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbNDCCode
            // 
            this.cmbNDCCode.FormattingEnabled = true;
            this.cmbNDCCode.Location = new System.Drawing.Point(393, 12);
            this.cmbNDCCode.Name = "cmbNDCCode";
            this.cmbNDCCode.Size = new System.Drawing.Size(47, 22);
            this.cmbNDCCode.TabIndex = 24;
            this.cmbNDCCode.Visible = false;
            // 
            // lblNDCCodetext
            // 
            this.lblNDCCodetext.AutoSize = true;
            this.lblNDCCodetext.Location = new System.Drawing.Point(404, 15);
            this.lblNDCCodetext.Name = "lblNDCCodetext";
            this.lblNDCCodetext.Size = new System.Drawing.Size(0, 14);
            this.lblNDCCodetext.TabIndex = 23;
            // 
            // btn_Clear
            // 
            this.btn_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Clear.AutoEllipsis = true;
            this.btn_Clear.BackColor = System.Drawing.Color.Transparent;
            this.btn_Clear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Clear.BackgroundImage")));
            this.btn_Clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Clear.Image = ((System.Drawing.Image)(resources.GetObject("btn_Clear.Image")));
            this.btn_Clear.Location = new System.Drawing.Point(551, 13);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(21, 21);
            this.btn_Clear.TabIndex = 13;
            this.btn_Clear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Clear.UseVisualStyleBackColor = false;
            this.btn_Clear.Visible = false;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btnAddNDCCode
            // 
            this.btnAddNDCCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNDCCode.AutoEllipsis = true;
            this.btnAddNDCCode.BackColor = System.Drawing.Color.Transparent;
            this.btnAddNDCCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddNDCCode.BackgroundImage")));
            this.btnAddNDCCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNDCCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNDCCode.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNDCCode.Image")));
            this.btnAddNDCCode.Location = new System.Drawing.Point(527, 13);
            this.btnAddNDCCode.Name = "btnAddNDCCode";
            this.btnAddNDCCode.Size = new System.Drawing.Size(21, 21);
            this.btnAddNDCCode.TabIndex = 12;
            this.btnAddNDCCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddNDCCode.UseVisualStyleBackColor = false;
            this.btnAddNDCCode.Visible = false;
            this.btnAddNDCCode.Click += new System.EventHandler(this.btnAddNDCCode_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tls_Notes);
            this.panel1.Controls.Add(this.mskCloseDate);
            this.panel1.Controls.Add(this.cmbNDCCode);
            this.panel1.Controls.Add(this.btn_Clear);
            this.panel1.Controls.Add(this.btnAddNDCCode);
            this.panel1.Controls.Add(this.lblNDCCodetext);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 54);
            this.panel1.TabIndex = 2;
            this.panel1.TabStop = true;
            // 
            // tls_Notes
            // 
            this.tls_Notes.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_Notes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Notes.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Notes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Ok,
            this.tlb_Close});
            this.tls_Notes.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Notes.Location = new System.Drawing.Point(0, 0);
            this.tls_Notes.Name = "tls_Notes";
            this.tls_Notes.Size = new System.Drawing.Size(594, 53);
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
            // mskCloseDate
            // 
            this.mskCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskCloseDate.Location = new System.Drawing.Point(826, 13);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(90, 22);
            this.mskCloseDate.TabIndex = 800;
            this.mskCloseDate.TabStop = false;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            this.mskCloseDate.Visible = false;
            // 
            // frmDrugBilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(594, 339);
            this.Controls.Add(this.PnlFields);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDrugBilling";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Charge NDC";
            this.Load += new System.EventHandler(this.frmDrugBilling_Load);
            this.PnlFields.ResumeLayout(false);
            this.PnlFields.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tls_Notes.ResumeLayout(false);
            this.tls_Notes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlFields;
        private System.Windows.Forms.Panel panel1;
        private gloGlobal.gloToolStripIgnoreFocus tls_Notes;
        private System.Windows.Forms.ToolStripButton tlb_Ok;
        private System.Windows.Forms.ToolStripButton tlb_Close;
        private System.Windows.Forms.MaskedTextBox mskCloseDate;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btnAddNDCCode;
        private System.Windows.Forms.Label lblNDCCodetext;
        private System.Windows.Forms.ComboBox cmbNDCCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNDCCode;
        private System.Windows.Forms.TextBox txtNDCCode;
        private System.Windows.Forms.TextBox txtNDCQty;
        private System.Windows.Forms.Label lblNDCQty;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label lblDOSText;
        private System.Windows.Forms.Label lblCPTDescText;
        private System.Windows.Forms.Label lblMod2Text;
        private System.Windows.Forms.Label lblMod1Text;
        private System.Windows.Forms.Label lblCPTCodeText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPrescription;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPrescriptionDesc;
        private System.Windows.Forms.CheckBox chkIncDesc;
    }
}