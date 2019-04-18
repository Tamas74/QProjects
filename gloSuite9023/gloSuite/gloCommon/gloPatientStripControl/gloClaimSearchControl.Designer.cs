namespace gloPatientStripControl
{
    partial class gloClaimSearchControl
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
                if (dvNext != null)
                {
                    dvNext.Dispose();
                    dvNext = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloClaimSearchControl));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btn_ModityPatient = new System.Windows.Forms.Button();
            this.btnSearchPatientClaim = new System.Windows.Forms.Button();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlPatientOnStrip = new System.Windows.Forms.Panel();
            this.lblClaimNo = new System.Windows.Forms.Label();
            this.txtPatientSearch = new System.Windows.Forms.TextBox();
            this.lblClaim = new System.Windows.Forms.Label();
            this.lblPatNameNCode = new System.Windows.Forms.Label();
            this.lablel100 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chk_ClaimNoSearch = new System.Windows.Forms.CheckBox();
            this.label52 = new System.Windows.Forms.Label();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblSearchonClaimNo = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.pnlPatientOnStrip.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ModityPatient
            // 
            this.btn_ModityPatient.BackColor = System.Drawing.Color.Transparent;
            this.btn_ModityPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ModityPatient.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_ModityPatient.FlatAppearance.BorderSize = 0;
            this.btn_ModityPatient.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_ModityPatient.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_ModityPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ModityPatient.Location = new System.Drawing.Point(55, 0);
            this.btn_ModityPatient.Name = "btn_ModityPatient";
            this.btn_ModityPatient.Size = new System.Drawing.Size(23, 22);
            this.btn_ModityPatient.TabIndex = 57;
            this.toolTip1.SetToolTip(this.btn_ModityPatient, "Modify Patient");
            this.btn_ModityPatient.UseVisualStyleBackColor = false;
            this.btn_ModityPatient.Click += new System.EventHandler(this.btn_ModityPatient_Click);
            this.btn_ModityPatient.MouseLeave += new System.EventHandler(this.btn_ModityPatient_MouseLeave);
            this.btn_ModityPatient.MouseHover += new System.EventHandler(this.btn_ModityPatient_MouseHover);
            // 
            // btnSearchPatientClaim
            // 
            this.btnSearchPatientClaim.AutoEllipsis = true;
            this.btnSearchPatientClaim.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchPatientClaim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchPatientClaim.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnSearchPatientClaim.FlatAppearance.BorderSize = 0;
            this.btnSearchPatientClaim.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearchPatientClaim.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearchPatientClaim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchPatientClaim.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchPatientClaim.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchPatientClaim.Image")));
            this.btnSearchPatientClaim.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSearchPatientClaim.Location = new System.Drawing.Point(260, 1);
            this.btnSearchPatientClaim.Name = "btnSearchPatientClaim";
            this.btnSearchPatientClaim.Size = new System.Drawing.Size(106, 21);
            this.btnSearchPatientClaim.TabIndex = 60;
            this.btnSearchPatientClaim.Text = "Claim Search";
            this.btnSearchPatientClaim.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchPatientClaim, "Claim Search");
            this.btnSearchPatientClaim.UseVisualStyleBackColor = false;
            this.btnSearchPatientClaim.Visible = false;
            this.btnSearchPatientClaim.Click += new System.EventHandler(this.btnSearchPatientClaim_Click);
            this.btnSearchPatientClaim.MouseLeave += new System.EventHandler(this.btnSearchPatientClaim_MouseLeave);
            this.btnSearchPatientClaim.MouseHover += new System.EventHandler(this.btnSearchPatientClaim_MouseHover);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Img_Blue2007;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pnlPatientOnStrip);
            this.panel1.Controls.Add(this.btnSearchPatientClaim);
            this.panel1.Controls.Add(this.chk_ClaimNoSearch);
            this.panel1.Controls.Add(this.label52);
            this.panel1.Controls.Add(this.pnlButton);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.lblSearchonClaimNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1569, 24);
            this.panel1.TabIndex = 61;
            // 
            // pnlPatientOnStrip
            // 
            this.pnlPatientOnStrip.Controls.Add(this.lblClaimNo);
            this.pnlPatientOnStrip.Controls.Add(this.txtPatientSearch);
            this.pnlPatientOnStrip.Controls.Add(this.lblClaim);
            this.pnlPatientOnStrip.Controls.Add(this.lblPatNameNCode);
            this.pnlPatientOnStrip.Controls.Add(this.lablel100);
            this.pnlPatientOnStrip.Controls.Add(this.label6);
            this.pnlPatientOnStrip.Location = new System.Drawing.Point(3, 1);
            this.pnlPatientOnStrip.Name = "pnlPatientOnStrip";
            this.pnlPatientOnStrip.Size = new System.Drawing.Size(866, 22);
            this.pnlPatientOnStrip.TabIndex = 65;
            // 
            // lblClaimNo
            // 
            this.lblClaimNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClaimNo.AutoEllipsis = true;
            this.lblClaimNo.AutoSize = true;
            this.lblClaimNo.BackColor = System.Drawing.Color.Transparent;
            this.lblClaimNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimNo.ForeColor = System.Drawing.Color.White;
            this.lblClaimNo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblClaimNo.Location = new System.Drawing.Point(553, 1);
            this.lblClaimNo.Name = "lblClaimNo";
            this.lblClaimNo.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblClaimNo.Size = new System.Drawing.Size(0, 18);
            this.lblClaimNo.TabIndex = 64;
            this.lblClaimNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPatientSearch
            // 
            this.txtPatientSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPatientSearch.Location = new System.Drawing.Point(774, 0);
            this.txtPatientSearch.Name = "txtPatientSearch";
            this.txtPatientSearch.Size = new System.Drawing.Size(141, 22);
            this.txtPatientSearch.TabIndex = 0;
            this.txtPatientSearch.Visible = false;
            this.txtPatientSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatientSearch_KeyPress);
            this.txtPatientSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtPatientSearch_MouseDown);
            // 
            // lblClaim
            // 
            this.lblClaim.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClaim.AutoEllipsis = true;
            this.lblClaim.AutoSize = true;
            this.lblClaim.BackColor = System.Drawing.Color.Transparent;
            this.lblClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaim.ForeColor = System.Drawing.Color.White;
            this.lblClaim.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblClaim.Location = new System.Drawing.Point(498, 1);
            this.lblClaim.Name = "lblClaim";
            this.lblClaim.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblClaim.Size = new System.Drawing.Size(60, 18);
            this.lblClaim.TabIndex = 63;
            this.lblClaim.Text = "Claim # :";
            this.lblClaim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatNameNCode
            // 
            this.lblPatNameNCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatNameNCode.AutoEllipsis = true;
            this.lblPatNameNCode.AutoSize = true;
            this.lblPatNameNCode.BackColor = System.Drawing.Color.Transparent;
            this.lblPatNameNCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatNameNCode.ForeColor = System.Drawing.Color.White;
            this.lblPatNameNCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPatNameNCode.Location = new System.Drawing.Point(75, 0);
            this.lblPatNameNCode.Name = "lblPatNameNCode";
            this.lblPatNameNCode.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblPatNameNCode.Size = new System.Drawing.Size(0, 18);
            this.lblPatNameNCode.TabIndex = 62;
            this.lblPatNameNCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lablel100
            // 
            this.lablel100.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lablel100.AutoEllipsis = true;
            this.lablel100.AutoSize = true;
            this.lablel100.BackColor = System.Drawing.Color.Transparent;
            this.lablel100.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablel100.ForeColor = System.Drawing.Color.White;
            this.lablel100.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lablel100.Location = new System.Drawing.Point(5, 0);
            this.lablel100.Name = "lablel100";
            this.lablel100.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lablel100.Size = new System.Drawing.Size(68, 18);
            this.lablel100.TabIndex = 61;
            this.lablel100.Text = " Patient : ";
            this.lablel100.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(670, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label6.Size = new System.Drawing.Size(105, 18);
            this.label6.TabIndex = 20;
            this.label6.Text = "Search Claim # :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chk_ClaimNoSearch
            // 
            this.chk_ClaimNoSearch.AutoSize = true;
            this.chk_ClaimNoSearch.BackColor = System.Drawing.Color.Transparent;
            this.chk_ClaimNoSearch.Location = new System.Drawing.Point(276, 3);
            this.chk_ClaimNoSearch.Name = "chk_ClaimNoSearch";
            this.chk_ClaimNoSearch.Size = new System.Drawing.Size(174, 18);
            this.chk_ClaimNoSearch.TabIndex = 1;
            this.chk_ClaimNoSearch.Text = "Search on Claim Number";
            this.chk_ClaimNoSearch.UseVisualStyleBackColor = false;
            this.chk_ClaimNoSearch.Visible = false;
            this.chk_ClaimNoSearch.CheckedChanged += new System.EventHandler(this.chk_ClaimNoSearch_CheckedChanged);
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(0, 1);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 22);
            this.label52.TabIndex = 22;
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.Transparent;
            this.pnlButton.Controls.Add(this.btn_ModityPatient);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButton.ForeColor = System.Drawing.Color.Black;
            this.pnlButton.Location = new System.Drawing.Point(1490, 1);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(78, 22);
            this.pnlButton.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(1568, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 22);
            this.label7.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1569, 1);
            this.label9.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(0, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1569, 1);
            this.label13.TabIndex = 58;
            // 
            // lblSearchonClaimNo
            // 
            this.lblSearchonClaimNo.AutoSize = true;
            this.lblSearchonClaimNo.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchonClaimNo.Location = new System.Drawing.Point(276, 5);
            this.lblSearchonClaimNo.Name = "lblSearchonClaimNo";
            this.lblSearchonClaimNo.Size = new System.Drawing.Size(155, 14);
            this.lblSearchonClaimNo.TabIndex = 57;
            this.lblSearchonClaimNo.Text = "Search on Claim Number";
            this.lblSearchonClaimNo.Visible = false;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlTop.Size = new System.Drawing.Size(1569, 27);
            this.pnlTop.TabIndex = 0;
            // 
            // gloClaimSearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnlTop);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloClaimSearchControl";
            this.Size = new System.Drawing.Size(1569, 23);
            this.Load += new System.EventHandler(this.gloPatientStripControl_Load);
            this.SizeChanged += new System.EventHandler(this.gloPatientStripControl_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.gloPatientStripControl_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlPatientOnStrip.ResumeLayout(false);
            this.pnlPatientOnStrip.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlPatientOnStrip;
        private System.Windows.Forms.Label lblPatNameNCode;
        private System.Windows.Forms.Label lablel100;
        private System.Windows.Forms.Button btnSearchPatientClaim;
        private System.Windows.Forms.CheckBox chk_ClaimNoSearch;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btn_ModityPatient;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblSearchonClaimNo;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblClaimNo;
        private System.Windows.Forms.Label lblClaim;
        public System.Windows.Forms.TextBox txtPatientSearch;
        public System.Windows.Forms.Label label6;
    }
}
