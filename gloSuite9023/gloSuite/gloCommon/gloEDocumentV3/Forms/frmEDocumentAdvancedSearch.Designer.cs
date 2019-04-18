namespace gloEDocumentV3.Forms
{
    partial class frmEDocumentAdvancedSearch
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpDOB };
            System.Windows.Forms.Control[] cntControls = { dtpDOB };
            if (disposing && (components != null))
            {
               
                components.Dispose();
            }
            base.Dispose(disposing);

 

            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControls);

                }
            }

            if (cntControls != null)
            {
                if (cntControls.Length > 0)
                {
                    gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                }
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocumentAdvancedSearch));
            this.pnl_tls_ = new System.Windows.Forms.Panel();
            this.tls = new gloGlobal.gloToolStripIgnoreFocus();
            this.btn_tls_Search = new System.Windows.Forms.ToolStripButton();
            this.btn_tls_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlPhone = new System.Windows.Forms.Panel();
            this.txtEMPPhone = new gloMaskControl.gloMaskBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtMobile = new gloMaskControl.gloMaskBox();
            this.lblMobile = new System.Windows.Forms.Label();
            this.txtPhone = new gloMaskControl.gloMaskBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.txtSSN = new System.Windows.Forms.TextBox();
            this.chkAdvSearch = new System.Windows.Forms.CheckBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.dtpDOB = new System.Windows.Forms.DateTimePicker();
            this.chkGardianInfo = new System.Windows.Forms.CheckBox();
            this.pnlGardian = new System.Windows.Forms.Panel();
            this.txtFPhone = new gloMaskControl.gloMaskBox();
            this.txtFCellNo = new gloMaskControl.gloMaskBox();
            this.txtMPhone = new gloMaskControl.gloMaskBox();
            this.txtMCellNo = new gloMaskControl.gloMaskBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtFFName = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtFLName = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtMFName = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtMLName = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.pnl_tls_.SuspendLayout();
            this.tls.SuspendLayout();
            this.pnlTOP.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlPhone.SuspendLayout();
            this.pnlGardian.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tls_
            // 
            this.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tls_.Controls.Add(this.tls);
            this.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tls_.Location = new System.Drawing.Point(0, 0);
            this.pnl_tls_.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl_tls_.Name = "pnl_tls_";
            this.pnl_tls_.Size = new System.Drawing.Size(368, 54);
            this.pnl_tls_.TabIndex = 31;
            // 
            // tls
            // 
            this.tls.BackColor = System.Drawing.Color.Transparent;
            this.tls.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_tls_Search,
            this.btn_tls_Cancel});
            this.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls.Location = new System.Drawing.Point(0, 0);
            this.tls.Name = "tls";
            this.tls.Size = new System.Drawing.Size(368, 53);
            this.tls.TabIndex = 0;
            this.tls.Text = "toolStrip1";
            this.tls.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_ItemClicked);
            // 
            // btn_tls_Search
            // 
            this.btn_tls_Search.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tls_Search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_tls_Search.Image = ((System.Drawing.Image)(resources.GetObject("btn_tls_Search.Image")));
            this.btn_tls_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_tls_Search.Name = "btn_tls_Search";
            this.btn_tls_Search.Size = new System.Drawing.Size(52, 50);
            this.btn_tls_Search.Tag = "Search";
            this.btn_tls_Search.Text = "&Search";
            this.btn_tls_Search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_tls_Search.ToolTipText = "Search";
            // 
            // btn_tls_Cancel
            // 
            this.btn_tls_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tls_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_tls_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_tls_Cancel.Image")));
            this.btn_tls_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_tls_Cancel.Name = "btn_tls_Cancel";
            this.btn_tls_Cancel.Size = new System.Drawing.Size(43, 50);
            this.btn_tls_Cancel.Tag = "Cancel";
            this.btn_tls_Cancel.Text = "&Close";
            this.btn_tls_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_tls_Cancel.ToolTipText = "Close";
            // 
            // pnlTOP
            // 
            this.pnlTOP.BackColor = System.Drawing.Color.Transparent;
            this.pnlTOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTOP.Controls.Add(this.panel1);
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTOP.Location = new System.Drawing.Point(0, 54);
            this.pnlTOP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTOP.Size = new System.Drawing.Size(368, 32);
            this.pnlTOP.TabIndex = 32;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Blue2007;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lblHeader);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 26);
            this.panel1.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(1, 1);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(360, 24);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = " Advanced Search on Patient";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(1, 25);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(360, 1);
            this.label14.TabIndex = 12;
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(1, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(360, 1);
            this.label13.TabIndex = 11;
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(361, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 26);
            this.label12.TabIndex = 10;
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 26);
            this.label3.TabIndex = 9;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPhone
            // 
            this.pnlPhone.BackColor = System.Drawing.Color.Transparent;
            this.pnlPhone.Controls.Add(this.txtEMPPhone);
            this.pnlPhone.Controls.Add(this.label23);
            this.pnlPhone.Controls.Add(this.txtMobile);
            this.pnlPhone.Controls.Add(this.lblMobile);
            this.pnlPhone.Controls.Add(this.txtPhone);
            this.pnlPhone.Controls.Add(this.label15);
            this.pnlPhone.Controls.Add(this.label16);
            this.pnlPhone.Controls.Add(this.label17);
            this.pnlPhone.Controls.Add(this.label18);
            this.pnlPhone.Controls.Add(this.txtCode);
            this.pnlPhone.Controls.Add(this.txtLName);
            this.pnlPhone.Controls.Add(this.txtFName);
            this.pnlPhone.Controls.Add(this.txtSSN);
            this.pnlPhone.Controls.Add(this.chkAdvSearch);
            this.pnlPhone.Controls.Add(this.Label1);
            this.pnlPhone.Controls.Add(this.Label2);
            this.pnlPhone.Controls.Add(this.dtpDOB);
            this.pnlPhone.Controls.Add(this.chkGardianInfo);
            this.pnlPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlPhone.Location = new System.Drawing.Point(0, 86);
            this.pnlPhone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlPhone.Name = "pnlPhone";
            this.pnlPhone.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlPhone.Size = new System.Drawing.Size(368, 156);
            this.pnlPhone.TabIndex = 33;
            // 
            // txtEMPPhone
            // 
            this.txtEMPPhone.IncludeLiteralsAndPrompts = false;
            this.txtEMPPhone.Location = new System.Drawing.Point(196, 78);
            this.txtEMPPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtEMPPhone.Name = "txtEMPPhone";
            this.txtEMPPhone.Size = new System.Drawing.Size(141, 23);
            this.txtEMPPhone.TabIndex = 23;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(78, 81);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(112, 14);
            this.label23.TabIndex = 22;
            this.label23.Text = "Employer\'s Phone :";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMobile
            // 
            this.txtMobile.IncludeLiteralsAndPrompts = false;
            this.txtMobile.Location = new System.Drawing.Point(196, 53);
            this.txtMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(141, 23);
            this.txtMobile.TabIndex = 21;
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Location = new System.Drawing.Point(141, 57);
            this.lblMobile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(49, 14);
            this.lblMobile.TabIndex = 20;
            this.lblMobile.Text = "Mobile :";
            this.lblMobile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPhone
            // 
            this.txtPhone.IncludeLiteralsAndPrompts = false;
            this.txtPhone.Location = new System.Drawing.Point(196, 29);
            this.txtPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(141, 23);
            this.txtPhone.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(4, 152);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(360, 1);
            this.label15.TabIndex = 18;
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(4, 0);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(360, 1);
            this.label16.TabIndex = 17;
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Location = new System.Drawing.Point(364, 0);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 153);
            this.label17.TabIndex = 16;
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Location = new System.Drawing.Point(3, 0);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 153);
            this.label18.TabIndex = 15;
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.GhostWhite;
            this.txtCode.Location = new System.Drawing.Point(391, 12);
            this.txtCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(9, 22);
            this.txtCode.TabIndex = 14;
            this.txtCode.Visible = false;
            // 
            // txtLName
            // 
            this.txtLName.BackColor = System.Drawing.Color.GhostWhite;
            this.txtLName.Location = new System.Drawing.Point(391, 35);
            this.txtLName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLName.Name = "txtLName";
            this.txtLName.ReadOnly = true;
            this.txtLName.Size = new System.Drawing.Size(9, 22);
            this.txtLName.TabIndex = 13;
            this.txtLName.Visible = false;
            // 
            // txtFName
            // 
            this.txtFName.BackColor = System.Drawing.Color.GhostWhite;
            this.txtFName.Location = new System.Drawing.Point(391, 58);
            this.txtFName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtFName.Name = "txtFName";
            this.txtFName.ReadOnly = true;
            this.txtFName.Size = new System.Drawing.Size(9, 22);
            this.txtFName.TabIndex = 12;
            this.txtFName.Visible = false;
            // 
            // txtSSN
            // 
            this.txtSSN.BackColor = System.Drawing.Color.GhostWhite;
            this.txtSSN.Location = new System.Drawing.Point(391, 81);
            this.txtSSN.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSSN.Name = "txtSSN";
            this.txtSSN.ReadOnly = true;
            this.txtSSN.Size = new System.Drawing.Size(9, 22);
            this.txtSSN.TabIndex = 11;
            this.txtSSN.Visible = false;
            // 
            // chkAdvSearch
            // 
            this.chkAdvSearch.AutoSize = true;
            this.chkAdvSearch.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAdvSearch.Checked = true;
            this.chkAdvSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAdvSearch.Location = new System.Drawing.Point(39, 8);
            this.chkAdvSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkAdvSearch.Name = "chkAdvSearch";
            this.chkAdvSearch.Size = new System.Drawing.Size(170, 18);
            this.chkAdvSearch.TabIndex = 0;
            this.chkAdvSearch.Text = "Search in Previous Result :";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(105, 106);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(85, 14);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "Date of Birth :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(140, 33);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(50, 14);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "Phone :";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDOB
            // 
            this.dtpDOB.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOB.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOB.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOB.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOB.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDOB.Location = new System.Drawing.Point(196, 103);
            this.dtpDOB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.ShowCheckBox = true;
            this.dtpDOB.Size = new System.Drawing.Size(141, 22);
            this.dtpDOB.TabIndex = 2;
            this.dtpDOB.Value = new System.DateTime(2007, 1, 27, 0, 0, 0, 0);
            // 
            // chkGardianInfo
            // 
            this.chkGardianInfo.AutoSize = true;
            this.chkGardianInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGardianInfo.Location = new System.Drawing.Point(8, 128);
            this.chkGardianInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkGardianInfo.Name = "chkGardianInfo";
            this.chkGardianInfo.Size = new System.Drawing.Size(204, 18);
            this.chkGardianInfo.TabIndex = 3;
            this.chkGardianInfo.Text = "Search in Guardian information : ";
            this.chkGardianInfo.Click += new System.EventHandler(this.chkGardianInfo_Click);
            // 
            // pnlGardian
            // 
            this.pnlGardian.BackColor = System.Drawing.Color.Transparent;
            this.pnlGardian.Controls.Add(this.txtFPhone);
            this.pnlGardian.Controls.Add(this.txtFCellNo);
            this.pnlGardian.Controls.Add(this.txtMPhone);
            this.pnlGardian.Controls.Add(this.txtMCellNo);
            this.pnlGardian.Controls.Add(this.label19);
            this.pnlGardian.Controls.Add(this.label20);
            this.pnlGardian.Controls.Add(this.label21);
            this.pnlGardian.Controls.Add(this.label22);
            this.pnlGardian.Controls.Add(this.Label11);
            this.pnlGardian.Controls.Add(this.Label10);
            this.pnlGardian.Controls.Add(this.Label7);
            this.pnlGardian.Controls.Add(this.txtFFName);
            this.pnlGardian.Controls.Add(this.Label8);
            this.pnlGardian.Controls.Add(this.txtFLName);
            this.pnlGardian.Controls.Add(this.Label9);
            this.pnlGardian.Controls.Add(this.Label6);
            this.pnlGardian.Controls.Add(this.txtMFName);
            this.pnlGardian.Controls.Add(this.Label5);
            this.pnlGardian.Controls.Add(this.txtMLName);
            this.pnlGardian.Controls.Add(this.Label4);
            this.pnlGardian.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGardian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGardian.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlGardian.Location = new System.Drawing.Point(0, 242);
            this.pnlGardian.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlGardian.Name = "pnlGardian";
            this.pnlGardian.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlGardian.Size = new System.Drawing.Size(368, 235);
            this.pnlGardian.TabIndex = 34;
            // 
            // txtFPhone
            // 
            this.txtFPhone.IncludeLiteralsAndPrompts = false;
            this.txtFPhone.Location = new System.Drawing.Point(196, 198);
            this.txtFPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtFPhone.Name = "txtFPhone";
            this.txtFPhone.Size = new System.Drawing.Size(141, 22);
            this.txtFPhone.TabIndex = 30;
            // 
            // txtFCellNo
            // 
            this.txtFCellNo.IncludeLiteralsAndPrompts = false;
            this.txtFCellNo.Location = new System.Drawing.Point(196, 171);
            this.txtFCellNo.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.txtFCellNo.Name = "txtFCellNo";
            this.txtFCellNo.Size = new System.Drawing.Size(141, 22);
            this.txtFCellNo.TabIndex = 29;
            // 
            // txtMPhone
            // 
            this.txtMPhone.IncludeLiteralsAndPrompts = false;
            this.txtMPhone.Location = new System.Drawing.Point(196, 90);
            this.txtMPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtMPhone.Name = "txtMPhone";
            this.txtMPhone.Size = new System.Drawing.Size(141, 22);
            this.txtMPhone.TabIndex = 28;
            // 
            // txtMCellNo
            // 
            this.txtMCellNo.IncludeLiteralsAndPrompts = false;
            this.txtMCellNo.Location = new System.Drawing.Point(196, 63);
            this.txtMCellNo.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.txtMCellNo.Name = "txtMCellNo";
            this.txtMCellNo.Size = new System.Drawing.Size(141, 22);
            this.txtMCellNo.TabIndex = 27;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(4, 231);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(360, 1);
            this.label19.TabIndex = 26;
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Location = new System.Drawing.Point(4, 0);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(360, 1);
            this.label20.TabIndex = 25;
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Right;
            this.label21.Location = new System.Drawing.Point(364, 0);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 232);
            this.label21.TabIndex = 24;
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Location = new System.Drawing.Point(3, 0);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 232);
            this.label22.TabIndex = 23;
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(95, 202);
            this.Label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(97, 14);
            this.Label11.TabIndex = 22;
            this.Label11.Text = "Father\'s Phone :";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(91, 94);
            this.Label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(101, 14);
            this.Label10.TabIndex = 20;
            this.Label10.Text = "Mother\'s Phone :";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(93, 175);
            this.Label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(99, 14);
            this.Label7.TabIndex = 18;
            this.Label7.Text = "Father\'s Cell No :";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFFName
            // 
            this.txtFFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFFName.ForeColor = System.Drawing.Color.Black;
            this.txtFFName.Location = new System.Drawing.Point(196, 144);
            this.txtFFName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtFFName.MaxLength = 255;
            this.txtFFName.Name = "txtFFName";
            this.txtFFName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFFName.Size = new System.Drawing.Size(141, 22);
            this.txtFFName.TabIndex = 5;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(73, 148);
            this.Label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(119, 14);
            this.Label8.TabIndex = 16;
            this.Label8.Text = "Father\'s First Name :";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFLName
            // 
            this.txtFLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFLName.ForeColor = System.Drawing.Color.Black;
            this.txtFLName.Location = new System.Drawing.Point(196, 117);
            this.txtFLName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtFLName.MaxLength = 255;
            this.txtFLName.Name = "txtFLName";
            this.txtFLName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFLName.Size = new System.Drawing.Size(141, 22);
            this.txtFLName.TabIndex = 4;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(73, 121);
            this.Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(119, 14);
            this.Label9.TabIndex = 14;
            this.Label9.Text = "Father\'s Last Name :";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(89, 67);
            this.Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(103, 14);
            this.Label6.TabIndex = 12;
            this.Label6.Text = "Mother\'s Cell No :";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMFName
            // 
            this.txtMFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMFName.ForeColor = System.Drawing.Color.Black;
            this.txtMFName.Location = new System.Drawing.Point(196, 36);
            this.txtMFName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMFName.MaxLength = 255;
            this.txtMFName.Name = "txtMFName";
            this.txtMFName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMFName.Size = new System.Drawing.Size(141, 22);
            this.txtMFName.TabIndex = 1;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(69, 40);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(123, 14);
            this.Label5.TabIndex = 10;
            this.Label5.Text = "Mother\'s First Name :";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMLName
            // 
            this.txtMLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMLName.ForeColor = System.Drawing.Color.Black;
            this.txtMLName.Location = new System.Drawing.Point(196, 9);
            this.txtMLName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMLName.MaxLength = 255;
            this.txtMLName.Name = "txtMLName";
            this.txtMLName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMLName.Size = new System.Drawing.Size(141, 22);
            this.txtMLName.TabIndex = 0;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(69, 13);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(123, 14);
            this.Label4.TabIndex = 8;
            this.Label4.Text = "Mother\'s Last Name :";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmEDocumentAdvancedSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(368, 477);
            this.Controls.Add(this.pnlGardian);
            this.Controls.Add(this.pnlPhone);
            this.Controls.Add(this.pnlTOP);
            this.Controls.Add(this.pnl_tls_);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEDocumentAdvancedSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Search";
            this.Load += new System.EventHandler(this.frmEDocumentAdvancedSearch_Load);
            this.pnl_tls_.ResumeLayout(false);
            this.pnl_tls_.PerformLayout();
            this.tls.ResumeLayout(false);
            this.tls.PerformLayout();
            this.pnlTOP.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlPhone.ResumeLayout(false);
            this.pnlPhone.PerformLayout();
            this.pnlGardian.ResumeLayout(false);
            this.pnlGardian.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tls_;
        private gloGlobal.gloToolStripIgnoreFocus tls;
        private System.Windows.Forms.ToolStripButton btn_tls_Search;
        private System.Windows.Forms.ToolStripButton btn_tls_Cancel;
        internal System.Windows.Forms.Panel pnlTOP;
        internal System.Windows.Forms.Label lblHeader;
        internal System.Windows.Forms.Panel pnlPhone;
        internal System.Windows.Forms.TextBox txtCode;
        internal System.Windows.Forms.TextBox txtLName;
        internal System.Windows.Forms.TextBox txtFName;
        internal System.Windows.Forms.TextBox txtSSN;
        internal System.Windows.Forms.CheckBox chkAdvSearch;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.DateTimePicker dtpDOB;
        internal System.Windows.Forms.CheckBox chkGardianInfo;
        internal System.Windows.Forms.Panel pnlGardian;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txtFFName;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox txtFLName;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox txtMFName;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtMLName;
        internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Label label16;
        internal System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label18;
        internal System.Windows.Forms.Label label19;
        internal System.Windows.Forms.Label label20;
        internal System.Windows.Forms.Label label21;
        internal System.Windows.Forms.Label label22;
        private gloMaskControl.gloMaskBox txtPhone;
        private gloMaskControl.gloMaskBox txtFPhone;
        private gloMaskControl.gloMaskBox txtFCellNo;
        private gloMaskControl.gloMaskBox txtMPhone;
        private gloMaskControl.gloMaskBox txtMCellNo;
        private gloMaskControl.gloMaskBox txtMobile;
        internal System.Windows.Forms.Label lblMobile;
        private gloMaskControl.gloMaskBox txtEMPPhone;
        internal System.Windows.Forms.Label label23;
    }
}