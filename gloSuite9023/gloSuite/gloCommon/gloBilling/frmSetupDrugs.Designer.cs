namespace gloBilling
{
    partial class frmSetupDrugs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupDrugs));
            this.txtDrugsName = new System.Windows.Forms.TextBox();
            this.lblDrugName = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAllergicDrug = new System.Windows.Forms.CheckBox();
            this.chkClinicDrug = new System.Windows.Forms.CheckBox();
            this.cmbNarcotics = new System.Windows.Forms.ComboBox();
            this.lblNarcotics = new System.Windows.Forms.Label();
            this.txtGenericName = new System.Windows.Forms.TextBox();
            this.txtRoute = new System.Windows.Forms.TextBox();
            this.lblDrugsGenericName = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.lblDispense = new System.Windows.Forms.Label();
            this.lblRoute = new System.Windows.Forms.Label();
            this.lblDosage = new System.Windows.Forms.Label();
            this.txtFrequency = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtDosage = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.pnlSearch.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDrugsName
            // 
            this.txtDrugsName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDrugsName.ForeColor = System.Drawing.Color.Black;
            this.txtDrugsName.Location = new System.Drawing.Point(115, 14);
            this.txtDrugsName.Name = "txtDrugsName";
            this.txtDrugsName.Size = new System.Drawing.Size(198, 22);
            this.txtDrugsName.TabIndex = 0;
            // 
            // lblDrugName
            // 
            this.lblDrugName.AutoSize = true;
            this.lblDrugName.BackColor = System.Drawing.Color.Transparent;
            this.lblDrugName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrugName.Location = new System.Drawing.Point(36, 18);
            this.lblDrugName.Name = "lblDrugName";
            this.lblDrugName.Size = new System.Drawing.Size(76, 14);
            this.lblDrugName.TabIndex = 6;
            this.lblDrugName.Text = "Drug Name :";
            this.lblDrugName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.chkAllergicDrug);
            this.pnlSearch.Controls.Add(this.chkClinicDrug);
            this.pnlSearch.Controls.Add(this.cmbNarcotics);
            this.pnlSearch.Controls.Add(this.lblNarcotics);
            this.pnlSearch.Controls.Add(this.txtGenericName);
            this.pnlSearch.Controls.Add(this.txtRoute);
            this.pnlSearch.Controls.Add(this.lblDrugsGenericName);
            this.pnlSearch.Controls.Add(this.txtDuration);
            this.pnlSearch.Controls.Add(this.lblFrequency);
            this.pnlSearch.Controls.Add(this.lblDispense);
            this.pnlSearch.Controls.Add(this.lblRoute);
            this.pnlSearch.Controls.Add(this.lblDosage);
            this.pnlSearch.Controls.Add(this.txtFrequency);
            this.pnlSearch.Controls.Add(this.lblDuration);
            this.pnlSearch.Controls.Add(this.txtAmount);
            this.pnlSearch.Controls.Add(this.txtDosage);
            this.pnlSearch.Controls.Add(this.txtDrugsName);
            this.pnlSearch.Controls.Add(this.lblDrugName);
            this.pnlSearch.Controls.Add(this.label19);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.Location = new System.Drawing.Point(0, 54);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearch.Size = new System.Drawing.Size(343, 318);
            this.pnlSearch.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(4, 314);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(335, 1);
            this.label6.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(4, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(335, 1);
            this.label4.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(339, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 312);
            this.label2.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 312);
            this.label1.TabIndex = 34;
            // 
            // chkAllergicDrug
            // 
            this.chkAllergicDrug.AutoSize = true;
            this.chkAllergicDrug.BackColor = System.Drawing.Color.Transparent;
            this.chkAllergicDrug.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAllergicDrug.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllergicDrug.Location = new System.Drawing.Point(16, 284);
            this.chkAllergicDrug.Name = "chkAllergicDrug";
            this.chkAllergicDrug.Size = new System.Drawing.Size(115, 18);
            this.chkAllergicDrug.TabIndex = 9;
            this.chkAllergicDrug.Text = "Is Allergic Drug :";
            this.chkAllergicDrug.UseVisualStyleBackColor = false;
            // 
            // chkClinicDrug
            // 
            this.chkClinicDrug.AutoSize = true;
            this.chkClinicDrug.BackColor = System.Drawing.Color.Transparent;
            this.chkClinicDrug.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkClinicDrug.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClinicDrug.Location = new System.Drawing.Point(19, 256);
            this.chkClinicDrug.Name = "chkClinicDrug";
            this.chkClinicDrug.Size = new System.Drawing.Size(111, 18);
            this.chkClinicDrug.TabIndex = 8;
            this.chkClinicDrug.Text = "Is Clinical Drug :";
            this.chkClinicDrug.UseVisualStyleBackColor = false;
            // 
            // cmbNarcotics
            // 
            this.cmbNarcotics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNarcotics.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNarcotics.ForeColor = System.Drawing.Color.Black;
            this.cmbNarcotics.Location = new System.Drawing.Point(115, 224);
            this.cmbNarcotics.Name = "cmbNarcotics";
            this.cmbNarcotics.Size = new System.Drawing.Size(198, 22);
            this.cmbNarcotics.TabIndex = 7;
            // 
            // lblNarcotics
            // 
            this.lblNarcotics.AutoSize = true;
            this.lblNarcotics.BackColor = System.Drawing.Color.Transparent;
            this.lblNarcotics.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNarcotics.Location = new System.Drawing.Point(48, 228);
            this.lblNarcotics.Name = "lblNarcotics";
            this.lblNarcotics.Size = new System.Drawing.Size(64, 14);
            this.lblNarcotics.TabIndex = 33;
            this.lblNarcotics.Text = "Narcotics :";
            this.lblNarcotics.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGenericName
            // 
            this.txtGenericName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGenericName.ForeColor = System.Drawing.Color.Black;
            this.txtGenericName.Location = new System.Drawing.Point(115, 44);
            this.txtGenericName.Name = "txtGenericName";
            this.txtGenericName.Size = new System.Drawing.Size(198, 22);
            this.txtGenericName.TabIndex = 1;
            // 
            // txtRoute
            // 
            this.txtRoute.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoute.ForeColor = System.Drawing.Color.Black;
            this.txtRoute.Location = new System.Drawing.Point(115, 104);
            this.txtRoute.Name = "txtRoute";
            this.txtRoute.Size = new System.Drawing.Size(198, 22);
            this.txtRoute.TabIndex = 3;
            // 
            // lblDrugsGenericName
            // 
            this.lblDrugsGenericName.AutoSize = true;
            this.lblDrugsGenericName.BackColor = System.Drawing.Color.Transparent;
            this.lblDrugsGenericName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrugsGenericName.Location = new System.Drawing.Point(21, 48);
            this.lblDrugsGenericName.Name = "lblDrugsGenericName";
            this.lblDrugsGenericName.Size = new System.Drawing.Size(91, 14);
            this.lblDrugsGenericName.TabIndex = 20;
            this.lblDrugsGenericName.Text = "Generic Name :";
            this.lblDrugsGenericName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDuration
            // 
            this.txtDuration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDuration.ForeColor = System.Drawing.Color.Black;
            this.txtDuration.Location = new System.Drawing.Point(115, 164);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(198, 22);
            this.txtDuration.TabIndex = 5;
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.BackColor = System.Drawing.Color.Transparent;
            this.lblFrequency.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrequency.Location = new System.Drawing.Point(40, 138);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(72, 14);
            this.lblFrequency.TabIndex = 27;
            this.lblFrequency.Text = "Frequency :";
            this.lblFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDispense
            // 
            this.lblDispense.AutoSize = true;
            this.lblDispense.BackColor = System.Drawing.Color.Transparent;
            this.lblDispense.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDispense.Location = new System.Drawing.Point(49, 198);
            this.lblDispense.Name = "lblDispense";
            this.lblDispense.Size = new System.Drawing.Size(63, 14);
            this.lblDispense.TabIndex = 29;
            this.lblDispense.Text = "Dispense :";
            this.lblDispense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRoute
            // 
            this.lblRoute.AutoSize = true;
            this.lblRoute.BackColor = System.Drawing.Color.Transparent;
            this.lblRoute.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoute.Location = new System.Drawing.Point(64, 108);
            this.lblRoute.Name = "lblRoute";
            this.lblRoute.Size = new System.Drawing.Size(48, 14);
            this.lblRoute.TabIndex = 26;
            this.lblRoute.Text = "Route :";
            this.lblRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDosage
            // 
            this.lblDosage.AutoSize = true;
            this.lblDosage.BackColor = System.Drawing.Color.Transparent;
            this.lblDosage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDosage.Location = new System.Drawing.Point(57, 78);
            this.lblDosage.Name = "lblDosage";
            this.lblDosage.Size = new System.Drawing.Size(55, 14);
            this.lblDosage.TabIndex = 22;
            this.lblDosage.Text = "Dosage :";
            this.lblDosage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFrequency
            // 
            this.txtFrequency.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrequency.ForeColor = System.Drawing.Color.Black;
            this.txtFrequency.Location = new System.Drawing.Point(115, 134);
            this.txtFrequency.Name = "txtFrequency";
            this.txtFrequency.Size = new System.Drawing.Size(198, 22);
            this.txtFrequency.TabIndex = 4;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.BackColor = System.Drawing.Color.Transparent;
            this.lblDuration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.Location = new System.Drawing.Point(51, 168);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(61, 14);
            this.lblDuration.TabIndex = 28;
            this.lblDuration.Text = "Duration :";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.ForeColor = System.Drawing.Color.Black;
            this.txtAmount.Location = new System.Drawing.Point(115, 194);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(198, 22);
            this.txtAmount.TabIndex = 6;
            // 
            // txtDosage
            // 
            this.txtDosage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDosage.ForeColor = System.Drawing.Color.Black;
            this.txtDosage.Location = new System.Drawing.Point(115, 74);
            this.txtDosage.Name = "txtDosage";
            this.txtDosage.Size = new System.Drawing.Size(198, 22);
            this.txtDosage.TabIndex = 2;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(10, 49);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 110;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(24, 18);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(14, 14);
            this.label3.TabIndex = 111;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ts_Commands);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 54);
            this.panel1.TabIndex = 1;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButton1,
            this.tsb_OK,
            this.toolStripButton2});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(343, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // ToolStripButton1
            // 
            this.ToolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton1.Image")));
            this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton1.Name = "ToolStripButton1";
            this.ToolStripButton1.Size = new System.Drawing.Size(40, 50);
            this.ToolStripButton1.Text = "&Save";
            this.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolStripButton1.ToolTipText = "Save";
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
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(43, 50);
            this.toolStripButton2.Tag = "Cancel";
            this.toolStripButton2.Text = "&Close";
            this.toolStripButton2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // frmSetupDrugs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(343, 372);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupDrugs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Drug";
            this.Load += new System.EventHandler(this.frmSetupDrugs_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        private System.Windows.Forms.TextBox txtDrugsName;
        private System.Windows.Forms.Label lblDrugName;
        private System.Windows.Forms.Panel pnlSearch;
        internal System.Windows.Forms.TextBox txtGenericName;
        internal System.Windows.Forms.TextBox txtRoute;
        internal System.Windows.Forms.Label lblDrugsGenericName;
        internal System.Windows.Forms.TextBox txtDuration;
        internal System.Windows.Forms.Label lblFrequency;
        internal System.Windows.Forms.Label lblDispense;
        internal System.Windows.Forms.Label lblRoute;
        internal System.Windows.Forms.Label lblDosage;
        internal System.Windows.Forms.TextBox txtFrequency;
        internal System.Windows.Forms.Label lblDuration;
        internal System.Windows.Forms.TextBox txtAmount;
        internal System.Windows.Forms.TextBox txtDosage;
        internal System.Windows.Forms.CheckBox chkAllergicDrug;
        internal System.Windows.Forms.CheckBox chkClinicDrug;
        internal System.Windows.Forms.ComboBox cmbNarcotics;
        internal System.Windows.Forms.Label lblNarcotics;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ToolStripButton ToolStripButton1;
    }
}