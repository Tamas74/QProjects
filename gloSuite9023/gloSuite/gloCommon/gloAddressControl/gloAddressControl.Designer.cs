namespace gloAddress
{
    partial class gloAddressControl
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
                if (oToolTip != null)
                {
                    oToolTip.Dispose();
                    oToolTip = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlAddDetails = new System.Windows.Forms.Panel();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtAreaCode = new System.Windows.Forms.TextBox();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.txtCounty = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.lbl_Country = new System.Windows.Forms.Label();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.lblCounty = new System.Windows.Forms.Label();
            this.lblZip = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblAddr2 = new System.Windows.Forms.Label();
            this.lblAddr1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.tooltipZip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlAddDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAddDetails
            // 
            this.pnlAddDetails.Controls.Add(this.cmbCountry);
            this.pnlAddDetails.Controls.Add(this.txtArea);
            this.pnlAddDetails.Controls.Add(this.cmbState);
            this.pnlAddDetails.Controls.Add(this.label16);
            this.pnlAddDetails.Controls.Add(this.txtAreaCode);
            this.pnlAddDetails.Controls.Add(this.txtZip);
            this.pnlAddDetails.Controls.Add(this.txtCounty);
            this.pnlAddDetails.Controls.Add(this.txtCity);
            this.pnlAddDetails.Controls.Add(this.lbl_Country);
            this.pnlAddDetails.Controls.Add(this.txtAddress2);
            this.pnlAddDetails.Controls.Add(this.txtAddress1);
            this.pnlAddDetails.Controls.Add(this.lblCounty);
            this.pnlAddDetails.Controls.Add(this.lblZip);
            this.pnlAddDetails.Controls.Add(this.lblState);
            this.pnlAddDetails.Controls.Add(this.lblCity);
            this.pnlAddDetails.Controls.Add(this.lblAddr2);
            this.pnlAddDetails.Controls.Add(this.lblAddr1);
            this.pnlAddDetails.Controls.Add(this.label2);
            this.pnlAddDetails.Controls.Add(this.label1);
            this.pnlAddDetails.Controls.Add(this.pnlInternalControl);
            this.pnlAddDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAddDetails.Location = new System.Drawing.Point(3, 1);
            this.pnlAddDetails.Name = "pnlAddDetails";
            this.pnlAddDetails.Size = new System.Drawing.Size(325, 132);
            this.pnlAddDetails.TabIndex = 21;
            // 
            // cmbCountry
            // 
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(233, 79);
            this.cmbCountry.MaxLength = 10;
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(86, 22);
            this.cmbCountry.TabIndex = 26;
            this.cmbCountry.SelectedIndexChanged += new System.EventHandler(this.cmbCountry_SelectedIndexChanged);
            this.cmbCountry.EnabledChanged += new System.EventHandler(this.cmbCountry_EnabledChanged);
            this.cmbCountry.MouseEnter += new System.EventHandler(this.cmbCountry_MouseEnter);
            // 
            // txtArea
            // 
            this.txtArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.txtArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtArea.Enabled = false;
            this.txtArea.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArea.HideSelection = false;
            this.txtArea.Location = new System.Drawing.Point(125, 82);
            this.txtArea.MaxLength = 5;
            this.txtArea.Name = "txtArea";
            this.txtArea.ReadOnly = true;
            this.txtArea.Size = new System.Drawing.Size(6, 15);
            this.txtArea.TabIndex = 106;
            this.txtArea.TabStop = false;
            this.txtArea.Text = "-";
            this.txtArea.Visible = false;
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(233, 53);
            this.cmbState.MaxLength = 10;
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(86, 22);
            this.cmbState.TabIndex = 25;
            this.cmbState.SelectedIndexChanged += new System.EventHandler(this.cmbState_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1, 131);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(323, 1);
            this.label16.TabIndex = 26;
            // 
            // txtAreaCode
            // 
            this.txtAreaCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAreaCode.Location = new System.Drawing.Point(133, 79);
            this.txtAreaCode.MaxLength = 4;
            this.txtAreaCode.Name = "txtAreaCode";
            this.txtAreaCode.Size = new System.Drawing.Size(34, 22);
            this.txtAreaCode.TabIndex = 24;
            this.txtAreaCode.Visible = false;
            this.txtAreaCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAreaCode_KeyPress);
            this.txtAreaCode.Leave += new System.EventHandler(this.txtAreaCode_Leave);
            this.txtAreaCode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtAreaCode_MouseDown);
            this.txtAreaCode.Validated += new System.EventHandler(this.txtAreaCode_Validated);
            // 
            // txtZip
            // 
            this.txtZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZip.Location = new System.Drawing.Point(79, 79);
            this.txtZip.MaxLength = 5;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(88, 22);
            this.txtZip.TabIndex = 23;
            this.txtZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
            this.txtZip.Enter += new System.EventHandler(this.txtZip_GotFocus);
            this.txtZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtZip_KeyDown);
            this.txtZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZip_KeyPress);
            this.txtZip.Leave += new System.EventHandler(this.txtZip_LostFocus);
            // 
            // txtCounty
            // 
            this.txtCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounty.Location = new System.Drawing.Point(79, 105);
            this.txtCounty.MaxLength = 50;
            this.txtCounty.Name = "txtCounty";
            this.txtCounty.Size = new System.Drawing.Size(147, 22);
            this.txtCounty.TabIndex = 27;
            this.txtCounty.TextChanged += new System.EventHandler(this.txtAllTextBox_TextChanged);
            // 
            // txtCity
            // 
            this.txtCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.Location = new System.Drawing.Point(79, 53);
            this.txtCity.MaxLength = 50;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(88, 22);
            this.txtCity.TabIndex = 24;
            this.txtCity.TextChanged += new System.EventHandler(this.txtAllTextBox_TextChanged);
            this.txtCity.MouseEnter += new System.EventHandler(this.txtCity_MouseEnter);
            // 
            // lbl_Country
            // 
            this.lbl_Country.AutoSize = true;
            this.lbl_Country.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Country.Location = new System.Drawing.Point(168, 83);
            this.lbl_Country.MaximumSize = new System.Drawing.Size(58, 14);
            this.lbl_Country.MinimumSize = new System.Drawing.Size(58, 14);
            this.lbl_Country.Name = "lbl_Country";
            this.lbl_Country.Size = new System.Drawing.Size(58, 14);
            this.lbl_Country.TabIndex = 7;
            this.lbl_Country.Text = "Country :";
            this.lbl_Country.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddress2
            // 
            this.txtAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.Location = new System.Drawing.Point(79, 27);
            this.txtAddress2.MaxLength = 50;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(240, 22);
            this.txtAddress2.TabIndex = 22;
            this.txtAddress2.TextChanged += new System.EventHandler(this.txtAllTextBox_TextChanged);
            // 
            // txtAddress1
            // 
            this.txtAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.Location = new System.Drawing.Point(79, 1);
            this.txtAddress1.MaxLength = 100;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(240, 22);
            this.txtAddress1.TabIndex = 21;
            this.txtAddress1.TextChanged += new System.EventHandler(this.txtAllTextBox_TextChanged);
            // 
            // lblCounty
            // 
            this.lblCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCounty.Location = new System.Drawing.Point(22, 109);
            this.lblCounty.Name = "lblCounty";
            this.lblCounty.Size = new System.Drawing.Size(54, 14);
            this.lblCounty.TabIndex = 11;
            this.lblCounty.Text = "County :";
            this.lblCounty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblZip
            // 
            this.lblZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZip.Location = new System.Drawing.Point(45, 83);
            this.lblZip.Name = "lblZip";
            this.lblZip.Size = new System.Drawing.Size(31, 14);
            this.lblZip.TabIndex = 7;
            this.lblZip.Text = "Zip :";
            this.lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblState.Location = new System.Drawing.Point(168, 57);
            this.lblState.MaximumSize = new System.Drawing.Size(61, 14);
            this.lblState.MinimumSize = new System.Drawing.Size(58, 14);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(58, 14);
            this.lblState.TabIndex = 9;
            this.lblState.Text = "State :";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCity
            // 
            this.lblCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCity.Location = new System.Drawing.Point(41, 57);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(35, 14);
            this.lblCity.TabIndex = 5;
            this.lblCity.Text = "City :";
            this.lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAddr2
            // 
            this.lblAddr2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddr2.Location = new System.Drawing.Point(6, 31);
            this.lblAddr2.Name = "lblAddr2";
            this.lblAddr2.Size = new System.Drawing.Size(69, 14);
            this.lblAddr2.TabIndex = 3;
            this.lblAddr2.Text = "Address 2 :";
            this.lblAddr2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAddr1
            // 
            this.lblAddr1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddr1.Location = new System.Drawing.Point(6, 5);
            this.lblAddr1.Name = "lblAddr1";
            this.lblAddr1.Size = new System.Drawing.Size(69, 14);
            this.lblAddr1.TabIndex = 1;
            this.lblAddr1.Text = "Address 1 :";
            this.lblAddr1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 132);
            this.label2.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(324, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 132);
            this.label1.TabIndex = 105;
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.Location = new System.Drawing.Point(162, 2);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(157, 98);
            this.pnlInternalControl.TabIndex = 104;
            this.pnlInternalControl.Visible = false;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // gloAddressControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlAddDetails);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloAddressControl";
            this.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.Size = new System.Drawing.Size(331, 136);
            this.pnlAddDetails.ResumeLayout(false);
            this.pnlAddDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAddDetails;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblCounty;
        private System.Windows.Forms.Label lblZip;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblAddr2;
        private System.Windows.Forms.Label lblAddr1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Panel pnlInternalControl;
        public System.Windows.Forms.ComboBox cmbState;
        public System.Windows.Forms.TextBox txtZip;
        public System.Windows.Forms.TextBox txtCounty;
        public System.Windows.Forms.TextBox txtCity;
        public System.Windows.Forms.TextBox txtAddress2;
        public System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label lbl_Country;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtAreaCode;
        public System.Windows.Forms.TextBox txtArea;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        public System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.ToolTip tooltipZip;
    }
}
