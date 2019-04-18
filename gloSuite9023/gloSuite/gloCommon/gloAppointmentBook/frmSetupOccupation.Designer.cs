namespace gloAppointmentBook
{
    partial class frmSetupOccupation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupOccupation));
            this.tlsp_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tlsp_Occupation = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsp_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnl_ToolStrip = new System.Windows.Forms.Panel();
            this.lblAppBlockType = new System.Windows.Forms.Label();
            this.txtOccupation = new System.Windows.Forms.TextBox();
            this.lbl_L = new System.Windows.Forms.Label();
            this.lbl_T = new System.Windows.Forms.Label();
            this.lbl_B = new System.Windows.Forms.Label();
            this.lblAddr1 = new System.Windows.Forms.Label();
            this.lblAddr2 = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblZip = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtCounty = new System.Windows.Forms.TextBox();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.mtxtFax = new gloMaskControl.gloMaskBox();
            this.pnlAddresssControl = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.txtPlaceOfEmployment = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMobile = new gloMaskControl.gloMaskBox();
            this.txtPhone = new gloMaskControl.gloMaskBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmployer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tlsp_Occupation.SuspendLayout();
            this.pnl_ToolStrip.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlsp_btnOK
            // 
            this.tlsp_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsp_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnOK.Image")));
            this.tlsp_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnOK.Name = "tlsp_btnOK";
            this.tlsp_btnOK.Size = new System.Drawing.Size(66, 50);
            this.tlsp_btnOK.Tag = "OK";
            this.tlsp_btnOK.Text = "Sa&ve&&Cls";
            this.tlsp_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnOK.ToolTipText = "Save and Close";
            // 
            // tlsp_Occupation
            // 
            this.tlsp_Occupation.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.tlsp_Occupation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_Occupation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_Occupation.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_Occupation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsp_btnOK,
            this.tlsp_btnCancel});
            this.tlsp_Occupation.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_Occupation.Location = new System.Drawing.Point(0, 0);
            this.tlsp_Occupation.Name = "tlsp_Occupation";
            this.tlsp_Occupation.Size = new System.Drawing.Size(489, 53);
            this.tlsp_Occupation.TabIndex = 0;
            this.tlsp_Occupation.TabStop = true;
            this.tlsp_Occupation.Text = "ToolStrip1";
            this.tlsp_Occupation.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tlsp_btnCancel
            // 
            this.tlsp_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsp_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnCancel.Image")));
            this.tlsp_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnCancel.Name = "tlsp_btnCancel";
            this.tlsp_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tlsp_btnCancel.Tag = "Cancel";
            this.tlsp_btnCancel.Text = "&Close";
            this.tlsp_btnCancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tlsp_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnCancel.ToolTipText = "Close";
            // 
            // pnl_ToolStrip
            // 
            this.pnl_ToolStrip.Controls.Add(this.tlsp_Occupation);
            this.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_ToolStrip.Name = "pnl_ToolStrip";
            this.pnl_ToolStrip.Size = new System.Drawing.Size(489, 53);
            this.pnl_ToolStrip.TabIndex = 1;
            // 
            // lblAppBlockType
            // 
            this.lblAppBlockType.AutoSize = true;
            this.lblAppBlockType.BackColor = System.Drawing.Color.Transparent;
            this.lblAppBlockType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppBlockType.Location = new System.Drawing.Point(69, 52);
            this.lblAppBlockType.Name = "lblAppBlockType";
            this.lblAppBlockType.Size = new System.Drawing.Size(77, 14);
            this.lblAppBlockType.TabIndex = 6;
            this.lblAppBlockType.Text = "Occupation :";
            this.lblAppBlockType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOccupation
            // 
            this.txtOccupation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOccupation.ForeColor = System.Drawing.Color.Black;
            this.txtOccupation.Location = new System.Drawing.Point(150, 48);
            this.txtOccupation.MaxLength = 125;
            this.txtOccupation.Name = "txtOccupation";
            this.txtOccupation.Size = new System.Drawing.Size(314, 22);
            this.txtOccupation.TabIndex = 1;
            // 
            // lbl_L
            // 
            this.lbl_L.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_L.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_L.Location = new System.Drawing.Point(3, 3);
            this.lbl_L.Name = "lbl_L";
            this.lbl_L.Size = new System.Drawing.Size(1, 325);
            this.lbl_L.TabIndex = 34;
            // 
            // lbl_T
            // 
            this.lbl_T.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_T.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_T.Location = new System.Drawing.Point(4, 3);
            this.lbl_T.Name = "lbl_T";
            this.lbl_T.Size = new System.Drawing.Size(482, 1);
            this.lbl_T.TabIndex = 35;
            // 
            // lbl_B
            // 
            this.lbl_B.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_B.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_B.Location = new System.Drawing.Point(4, 327);
            this.lbl_B.Name = "lbl_B";
            this.lbl_B.Size = new System.Drawing.Size(482, 1);
            this.lbl_B.TabIndex = 36;
            // 
            // lblAddr1
            // 
            this.lblAddr1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAddr1.AutoEllipsis = true;
            this.lblAddr1.AutoSize = true;
            this.lblAddr1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddr1.Location = new System.Drawing.Point(51, 115);
            this.lblAddr1.Name = "lblAddr1";
            this.lblAddr1.Size = new System.Drawing.Size(95, 14);
            this.lblAddr1.TabIndex = 38;
            this.lblAddr1.Text = "Address Line 1 :";
            this.lblAddr1.Visible = false;
            // 
            // lblAddr2
            // 
            this.lblAddr2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAddr2.AutoEllipsis = true;
            this.lblAddr2.AutoSize = true;
            this.lblAddr2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddr2.Location = new System.Drawing.Point(51, 146);
            this.lblAddr2.Name = "lblAddr2";
            this.lblAddr2.Size = new System.Drawing.Size(95, 14);
            this.lblAddr2.TabIndex = 39;
            this.lblAddr2.Text = "Address Line 2 :";
            this.lblAddr2.Visible = false;
            // 
            // lblCity
            // 
            this.lblCity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCity.AutoEllipsis = true;
            this.lblCity.AutoSize = true;
            this.lblCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCity.Location = new System.Drawing.Point(111, 177);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(35, 14);
            this.lblCity.TabIndex = 40;
            this.lblCity.Text = "City :";
            this.lblCity.Visible = false;
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.AutoEllipsis = true;
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(294, 177);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(45, 14);
            this.lblState.TabIndex = 42;
            this.lblState.Text = "State :";
            // 
            // lblZip
            // 
            this.lblZip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblZip.AutoEllipsis = true;
            this.lblZip.AutoSize = true;
            this.lblZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZip.Location = new System.Drawing.Point(115, 208);
            this.lblZip.Name = "lblZip";
            this.lblZip.Size = new System.Drawing.Size(31, 14);
            this.lblZip.TabIndex = 41;
            this.lblZip.Text = "Zip :";
            this.lblZip.Visible = false;
            // 
            // lblCountry
            // 
            this.lblCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountry.AutoEllipsis = true;
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.Location = new System.Drawing.Point(246, 208);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(54, 14);
            this.lblCountry.TabIndex = 43;
            this.lblCountry.Text = "County :";
            // 
            // txtAddress1
            // 
            this.txtAddress1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.ForeColor = System.Drawing.Color.Black;
            this.txtAddress1.Location = new System.Drawing.Point(149, 111);
            this.txtAddress1.MaxLength = 50;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(314, 22);
            this.txtAddress1.TabIndex = 3;
            this.txtAddress1.Visible = false;
            // 
            // txtAddress2
            // 
            this.txtAddress2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtAddress2.Location = new System.Drawing.Point(149, 142);
            this.txtAddress2.MaxLength = 50;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(314, 22);
            this.txtAddress2.TabIndex = 4;
            this.txtAddress2.Visible = false;
            // 
            // txtCity
            // 
            this.txtCity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.ForeColor = System.Drawing.Color.Black;
            this.txtCity.Location = new System.Drawing.Point(149, 173);
            this.txtCity.MaxLength = 50;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(121, 22);
            this.txtCity.TabIndex = 6;
            this.txtCity.Visible = false;
            // 
            // txtCounty
            // 
            this.txtCounty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounty.ForeColor = System.Drawing.Color.Black;
            this.txtCounty.Location = new System.Drawing.Point(306, 204);
            this.txtCounty.MaxLength = 50;
            this.txtCounty.Name = "txtCounty";
            this.txtCounty.Size = new System.Drawing.Size(45, 22);
            this.txtCounty.TabIndex = 8;
            this.txtCounty.Visible = false;
            // 
            // txtZip
            // 
            this.txtZip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZip.ForeColor = System.Drawing.Color.Black;
            this.txtZip.Location = new System.Drawing.Point(149, 204);
            this.txtZip.MaxLength = 8;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(91, 22);
            this.txtZip.TabIndex = 5;
            this.txtZip.Visible = false;
            this.txtZip.Leave += new System.EventHandler(this.txtZip_Leave);
            this.txtZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZip_KeyPress);
            // 
            // cmbState
            // 
            this.cmbState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbState.ForeColor = System.Drawing.Color.Black;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(342, 173);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(120, 22);
            this.cmbState.TabIndex = 7;
            this.cmbState.Visible = false;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearch.Controls.Add(this.mtxtFax);
            this.pnlSearch.Controls.Add(this.pnlAddresssControl);
            this.pnlSearch.Controls.Add(this.txtOccupation);
            this.pnlSearch.Controls.Add(this.label19);
            this.pnlSearch.Controls.Add(this.cmbCountry);
            this.pnlSearch.Controls.Add(this.lblAppBlockType);
            this.pnlSearch.Controls.Add(this.label49);
            this.pnlSearch.Controls.Add(this.txtPlaceOfEmployment);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.txtMobile);
            this.pnlSearch.Controls.Add(this.txtPhone);
            this.pnlSearch.Controls.Add(this.txtEmail);
            this.pnlSearch.Controls.Add(this.txtFax);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.txtEmployer);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.cmbState);
            this.pnlSearch.Controls.Add(this.txtZip);
            this.pnlSearch.Controls.Add(this.txtCounty);
            this.pnlSearch.Controls.Add(this.txtCity);
            this.pnlSearch.Controls.Add(this.txtAddress2);
            this.pnlSearch.Controls.Add(this.txtAddress1);
            this.pnlSearch.Controls.Add(this.lblCountry);
            this.pnlSearch.Controls.Add(this.lblZip);
            this.pnlSearch.Controls.Add(this.lblState);
            this.pnlSearch.Controls.Add(this.lblCity);
            this.pnlSearch.Controls.Add(this.lblAddr2);
            this.pnlSearch.Controls.Add(this.lblAddr1);
            this.pnlSearch.Controls.Add(this.lbl_B);
            this.pnlSearch.Controls.Add(this.lbl_T);
            this.pnlSearch.Controls.Add(this.lbl_L);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(0, 53);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearch.Size = new System.Drawing.Size(489, 331);
            this.pnlSearch.TabIndex = 0;
            // 
            // mtxtFax
            // 
            this.mtxtFax.AllowValidate = true;
            this.mtxtFax.IncludeLiteralsAndPrompts = false;
            this.mtxtFax.Location = new System.Drawing.Point(150, 259);
            this.mtxtFax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.mtxtFax.Name = "mtxtFax";
            this.mtxtFax.ReadOnly = false;
            this.mtxtFax.Size = new System.Drawing.Size(121, 24);
            this.mtxtFax.TabIndex = 6;
            // 
            // pnlAddresssControl
            // 
            this.pnlAddresssControl.Location = new System.Drawing.Point(68, 99);
            this.pnlAddresssControl.Name = "pnlAddresssControl";
            this.pnlAddresssControl.Size = new System.Drawing.Size(325, 132);
            this.pnlAddresssControl.TabIndex = 3;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(58, 52);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 109;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Items.AddRange(new object[] {
            "US",
            "Canada"});
            this.cmbCountry.Location = new System.Drawing.Point(415, 203);
            this.cmbCountry.MaxDropDownItems = 3;
            this.cmbCountry.MaxLength = 20;
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(48, 22);
            this.cmbCountry.TabIndex = 9;
            this.cmbCountry.Visible = false;
            // 
            // label49
            // 
            this.label49.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label49.AutoEllipsis = true;
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(351, 212);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(58, 14);
            this.label49.TabIndex = 125;
            this.label49.Text = "Country :";
            this.label49.Visible = false;
            // 
            // txtPlaceOfEmployment
            // 
            this.txtPlaceOfEmployment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaceOfEmployment.ForeColor = System.Drawing.Color.Black;
            this.txtPlaceOfEmployment.Location = new System.Drawing.Point(150, 75);
            this.txtPlaceOfEmployment.MaxLength = 50;
            this.txtPlaceOfEmployment.Name = "txtPlaceOfEmployment";
            this.txtPlaceOfEmployment.Size = new System.Drawing.Size(314, 22);
            this.txtPlaceOfEmployment.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 14);
            this.label8.TabIndex = 123;
            this.label8.Text = "Place of Employment :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(68, 25);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(14, 14);
            this.label7.TabIndex = 121;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMobile
            // 
            this.txtMobile.AllowValidate = true;
            this.txtMobile.IncludeLiteralsAndPrompts = false;
            this.txtMobile.Location = new System.Drawing.Point(342, 232);
            this.txtMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.ReadOnly = false;
            this.txtMobile.Size = new System.Drawing.Size(121, 22);
            this.txtMobile.TabIndex = 5;
            // 
            // txtPhone
            // 
            this.txtPhone.AllowValidate = true;
            this.txtPhone.IncludeLiteralsAndPrompts = false;
            this.txtPhone.Location = new System.Drawing.Point(150, 232);
            this.txtPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.ReadOnly = false;
            this.txtPhone.Size = new System.Drawing.Size(121, 22);
            this.txtPhone.TabIndex = 4;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.Color.Black;
            this.txtEmail.Location = new System.Drawing.Point(150, 288);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(313, 22);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txttxtEmail_Validating);
            // 
            // txtFax
            // 
            this.txtFax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFax.ForeColor = System.Drawing.Color.Black;
            this.txtFax.Location = new System.Drawing.Point(342, 260);
            this.txtFax.MaxLength = 50;
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(122, 22);
            this.txtFax.TabIndex = 6;
            this.txtFax.TabStop = false;
            this.txtFax.Visible = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(104, 292);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 120;
            this.label5.Text = "Email :";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(113, 264);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 14);
            this.label6.TabIndex = 119;
            this.label6.Text = "Fax :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(290, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 116;
            this.label3.Text = "Mobile :";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(96, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 14);
            this.label4.TabIndex = 115;
            this.label4.Text = "Phone :";
            // 
            // txtEmployer
            // 
            this.txtEmployer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployer.ForeColor = System.Drawing.Color.Black;
            this.txtEmployer.Location = new System.Drawing.Point(150, 21);
            this.txtEmployer.MaxLength = 125;
            this.txtEmployer.Name = "txtEmployer";
            this.txtEmployer.Size = new System.Drawing.Size(314, 22);
            this.txtEmployer.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 14);
            this.label2.TabIndex = 111;
            this.label2.Text = "Employer :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(485, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 323);
            this.label1.TabIndex = 110;
            // 
            // frmSetupOccupation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(489, 384);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnl_ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupOccupation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Occupation";
            this.Load += new System.EventHandler(this.frmSetupOccupation_Load);
            this.tlsp_Occupation.ResumeLayout(false);
            this.tlsp_Occupation.PerformLayout();
            this.pnl_ToolStrip.ResumeLayout(false);
            this.pnl_ToolStrip.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ToolStripButton tlsp_btnOK;
        internal gloGlobal.gloToolStripIgnoreFocus tlsp_Occupation;
        internal System.Windows.Forms.ToolStripButton tlsp_btnCancel;
        private System.Windows.Forms.Panel pnl_ToolStrip;
        private System.Windows.Forms.Label lblAppBlockType;
        private System.Windows.Forms.TextBox txtOccupation;
        private System.Windows.Forms.Label lbl_L;
        private System.Windows.Forms.Label lbl_T;
        private System.Windows.Forms.Label lbl_B;
        private System.Windows.Forms.Label lblAddr1;
        private System.Windows.Forms.Label lblAddr2;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblZip;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtCounty;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmployer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private gloMaskControl.gloMaskBox txtMobile;
        private gloMaskControl.gloMaskBox txtPhone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPlaceOfEmployment;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Label label49;
        internal System.Windows.Forms.Panel pnlAddresssControl;
        private gloMaskControl.gloMaskBox mtxtFax;
    }
}