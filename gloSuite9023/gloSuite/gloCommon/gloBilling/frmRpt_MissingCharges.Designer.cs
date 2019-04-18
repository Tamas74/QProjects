namespace gloBilling
{
    partial class frmRpt_MissingCharges
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
                    if (dtpEndDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpEndDate);

                        }
                        catch
                        {
                        }


                        dtpEndDate.Dispose();
                        dtpEndDate = null;
                    }
                }
                catch
                {
                }

                try
                {
                    if (dtpStartDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpStartDate);

                        }
                        catch
                        {
                        }


                        dtpStartDate.Dispose();
                        dtpStartDate = null;
                    }
                }
                catch
                {
                }


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_MissingCharges));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tlsUnlinkAppt = new System.Windows.Forms.ToolStripButton();
            this.tls_Select = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcelOpen = new System.Windows.Forms.ToolStripButton();
            this.tls_RemoveAppt = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlCriteria = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdReportByDOS_Provider = new System.Windows.Forms.RadioButton();
            this.rdReportByDOS = new System.Windows.Forms.RadioButton();
            this.chkExcludeNoCharge = new System.Windows.Forms.CheckBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.pnlLocation = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.trvLocation = new System.Windows.Forms.TreeView();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnDeSelectLocation = new System.Windows.Forms.Button();
            this.btnSelectLocation = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.cmbLocationName = new System.Windows.Forms.ComboBox();
            this.pnlApptType = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.trvApptType = new System.Windows.Forms.TreeView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDeSelectApptType = new System.Windows.Forms.Button();
            this.btnSelectApptType = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.chkShowRemovedAppt = new System.Windows.Forms.CheckBox();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.trvProvider = new System.Windows.Forms.TreeView();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.pnlProviderHeader = new System.Windows.Forms.Panel();
            this.btnDeSelectProvider = new System.Windows.Forms.Button();
            this.btnSelectProvider = new System.Windows.Forms.Button();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.pnlC1Grid = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.c1MissingCharges = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlCriteria.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlLocation.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlApptType.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlProviderHeader.SuspendLayout();
            this.pnlC1Grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1MissingCharges)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1221, 58);
            this.pnlToolStrip.TabIndex = 90;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnOK,
            this.tlsUnlinkAppt,
            this.tls_Select,
            this.tls_btnExportToExcel,
            this.tls_btnExportToExcelOpen,
            this.tls_RemoveAppt,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(1219, 53);
            this.tls_Top.TabIndex = 10;
            this.tls_Top.TabStop = true;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(93, 50);
            this.tls_btnOK.Tag = "Show Report";
            this.tls_btnOK.Text = "&Show Report";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnOK.Click += new System.EventHandler(this.tls_btnOK_Click);
            // 
            // tlsUnlinkAppt
            // 
            this.tlsUnlinkAppt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsUnlinkAppt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsUnlinkAppt.Image = ((System.Drawing.Image)(resources.GetObject("tlsUnlinkAppt.Image")));
            this.tlsUnlinkAppt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsUnlinkAppt.Name = "tlsUnlinkAppt";
            this.tlsUnlinkAppt.Size = new System.Drawing.Size(122, 50);
            this.tlsUnlinkAppt.Tag = "Show Unlink Appt";
            this.tlsUnlinkAppt.Text = "Show &Unlink Appt";
            this.tlsUnlinkAppt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsUnlinkAppt.Visible = false;
            this.tlsUnlinkAppt.Click += new System.EventHandler(this.tlsUnlinkAppt_Click);
            // 
            // tls_Select
            // 
            this.tls_Select.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tls_Select.Image = ((System.Drawing.Image)(resources.GetObject("tls_Select.Image")));
            this.tls_Select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_Select.Name = "tls_Select";
            this.tls_Select.Size = new System.Drawing.Size(67, 50);
            this.tls_Select.Tag = "Select";
            this.tls_Select.Text = "Select &All";
            this.tls_Select.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_Select.Click += new System.EventHandler(this.tls_Select_Click);
            // 
            // tls_btnExportToExcel
            // 
            this.tls_btnExportToExcel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnExportToExcel.Image")));
            this.tls_btnExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnExportToExcel.Name = "tls_btnExportToExcel";
            this.tls_btnExportToExcel.Size = new System.Drawing.Size(105, 50);
            this.tls_btnExportToExcel.Tag = "Export";
            this.tls_btnExportToExcel.Text = "Export To &Excel";
            this.tls_btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnExportToExcel.Click += new System.EventHandler(this.tls_btnExportToExcel_Click);
            // 
            // tls_btnExportToExcelOpen
            // 
            this.tls_btnExportToExcelOpen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnExportToExcelOpen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnExportToExcelOpen.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnExportToExcelOpen.Image")));
            this.tls_btnExportToExcelOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnExportToExcelOpen.Name = "tls_btnExportToExcelOpen";
            this.tls_btnExportToExcelOpen.Size = new System.Drawing.Size(154, 50);
            this.tls_btnExportToExcelOpen.Tag = "ExportnOpen";
            this.tls_btnExportToExcelOpen.Text = "Exp&ort To Excel && Open";
            this.tls_btnExportToExcelOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnExportToExcelOpen.ToolTipText = "Export To Excel and Open";
            this.tls_btnExportToExcelOpen.Click += new System.EventHandler(this.tls_btnExportToExcelOpen_Click);
            // 
            // tls_RemoveAppt
            // 
            this.tls_RemoveAppt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tls_RemoveAppt.Image = ((System.Drawing.Image)(resources.GetObject("tls_RemoveAppt.Image")));
            this.tls_RemoveAppt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_RemoveAppt.Name = "tls_RemoveAppt";
            this.tls_RemoveAppt.Size = new System.Drawing.Size(99, 50);
            this.tls_RemoveAppt.Tag = "RemoveAppointment";
            this.tls_RemoveAppt.Text = "&Remove Appt.";
            this.tls_RemoveAppt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_RemoveAppt.Click += new System.EventHandler(this.tls_RemoveAppt_Click);
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Cancel";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCancel.Click += new System.EventHandler(this.tls_btnCancel_Click);
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.Controls.Add(this.groupBox1);
            this.pnlCriteria.Controls.Add(this.chkExcludeNoCharge);
            this.pnlCriteria.Controls.Add(this.lblLocation);
            this.pnlCriteria.Controls.Add(this.pnlLocation);
            this.pnlCriteria.Controls.Add(this.cmbLocationName);
            this.pnlCriteria.Controls.Add(this.pnlApptType);
            this.pnlCriteria.Controls.Add(this.chkShowRemovedAppt);
            this.pnlCriteria.Controls.Add(this.pnlProvider);
            this.pnlCriteria.Controls.Add(this.cmb_datefilter);
            this.pnlCriteria.Controls.Add(this.lbl_datefilter);
            this.pnlCriteria.Controls.Add(this.dtpEndDate);
            this.pnlCriteria.Controls.Add(this.label9);
            this.pnlCriteria.Controls.Add(this.lblEndDate);
            this.pnlCriteria.Controls.Add(this.label10);
            this.pnlCriteria.Controls.Add(this.label11);
            this.pnlCriteria.Controls.Add(this.dtpStartDate);
            this.pnlCriteria.Controls.Add(this.label12);
            this.pnlCriteria.Controls.Add(this.lblStartDate);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.Location = new System.Drawing.Point(0, 58);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Padding = new System.Windows.Forms.Padding(3);
            this.pnlCriteria.Size = new System.Drawing.Size(1221, 140);
            this.pnlCriteria.TabIndex = 200;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdReportByDOS_Provider);
            this.groupBox1.Controls.Add(this.rdReportByDOS);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox1.Location = new System.Drawing.Point(983, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 51);
            this.groupBox1.TabIndex = 203;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Missing Charge Report";
            // 
            // rdReportByDOS_Provider
            // 
            this.rdReportByDOS_Provider.AutoSize = true;
            this.rdReportByDOS_Provider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdReportByDOS_Provider.Location = new System.Drawing.Point(81, 21);
            this.rdReportByDOS_Provider.Name = "rdReportByDOS_Provider";
            this.rdReportByDOS_Provider.Size = new System.Drawing.Size(126, 18);
            this.rdReportByDOS_Provider.TabIndex = 208;
            this.rdReportByDOS_Provider.Text = "By DOS && Provider";
            this.rdReportByDOS_Provider.UseVisualStyleBackColor = true;
            this.rdReportByDOS_Provider.CheckedChanged += new System.EventHandler(this.rdReportByDOS_Provider_CheckedChanged);
            // 
            // rdReportByDOS
            // 
            this.rdReportByDOS.AutoSize = true;
            this.rdReportByDOS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdReportByDOS.Location = new System.Drawing.Point(6, 21);
            this.rdReportByDOS.Name = "rdReportByDOS";
            this.rdReportByDOS.Size = new System.Drawing.Size(66, 18);
            this.rdReportByDOS.TabIndex = 207;
            this.rdReportByDOS.Text = "By DOS";
            this.rdReportByDOS.UseVisualStyleBackColor = true;
            this.rdReportByDOS.CheckedChanged += new System.EventHandler(this.rdReportByDOS_CheckedChanged);
            // 
            // chkExcludeNoCharge
            // 
            this.chkExcludeNoCharge.AutoSize = true;
            this.chkExcludeNoCharge.Location = new System.Drawing.Point(983, 46);
            this.chkExcludeNoCharge.Name = "chkExcludeNoCharge";
            this.chkExcludeNoCharge.Size = new System.Drawing.Size(219, 18);
            this.chkExcludeNoCharge.TabIndex = 4;
            this.chkExcludeNoCharge.Text = "Exclude No Charge EMR Treatment";
            this.chkExcludeNoCharge.UseVisualStyleBackColor = true;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(973, 4);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 14);
            this.lblLocation.TabIndex = 201;
            this.lblLocation.Text = "Location :";
            this.lblLocation.Visible = false;
            // 
            // pnlLocation
            // 
            this.pnlLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLocation.Controls.Add(this.panel5);
            this.pnlLocation.Controls.Add(this.label22);
            this.pnlLocation.Controls.Add(this.label23);
            this.pnlLocation.Controls.Add(this.label24);
            this.pnlLocation.Location = new System.Drawing.Point(722, 14);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(246, 108);
            this.pnlLocation.TabIndex = 202;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.trvLocation);
            this.panel5.Controls.Add(this.label17);
            this.panel5.Controls.Add(this.label18);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.label21);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(244, 107);
            this.panel5.TabIndex = 92;
            // 
            // trvLocation
            // 
            this.trvLocation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvLocation.CheckBoxes = true;
            this.trvLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvLocation.ForeColor = System.Drawing.Color.Black;
            this.trvLocation.Location = new System.Drawing.Point(6, 26);
            this.trvLocation.Name = "trvLocation";
            this.trvLocation.ShowLines = false;
            this.trvLocation.ShowPlusMinus = false;
            this.trvLocation.ShowRootLines = false;
            this.trvLocation.Size = new System.Drawing.Size(238, 80);
            this.trvLocation.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.White;
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Location = new System.Drawing.Point(0, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(6, 80);
            this.label17.TabIndex = 99;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(0, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(244, 3);
            this.label18.TabIndex = 93;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.btnDeSelectLocation);
            this.panel6.Controls.Add(this.btnSelectLocation);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Controls.Add(this.label20);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(244, 23);
            this.panel6.TabIndex = 92;
            // 
            // btnDeSelectLocation
            // 
            this.btnDeSelectLocation.BackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectLocation.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeSelectLocation.FlatAppearance.BorderSize = 0;
            this.btnDeSelectLocation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectLocation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeSelectLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnDeSelectLocation.Image")));
            this.btnDeSelectLocation.Location = new System.Drawing.Point(182, 0);
            this.btnDeSelectLocation.Name = "btnDeSelectLocation";
            this.btnDeSelectLocation.Size = new System.Drawing.Size(31, 22);
            this.btnDeSelectLocation.TabIndex = 99;
            this.btnDeSelectLocation.Tag = "Select";
            this.btnDeSelectLocation.UseVisualStyleBackColor = false;
            this.btnDeSelectLocation.Visible = false;
            this.btnDeSelectLocation.Click += new System.EventHandler(this.btnDeSelectLocation_Click);
            // 
            // btnSelectLocation
            // 
            this.btnSelectLocation.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectLocation.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectLocation.FlatAppearance.BorderSize = 0;
            this.btnSelectLocation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectLocation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectLocation.Image")));
            this.btnSelectLocation.Location = new System.Drawing.Point(213, 0);
            this.btnSelectLocation.Name = "btnSelectLocation";
            this.btnSelectLocation.Size = new System.Drawing.Size(31, 22);
            this.btnSelectLocation.TabIndex = 98;
            this.btnSelectLocation.Tag = "Select";
            this.btnSelectLocation.UseVisualStyleBackColor = false;
            this.btnSelectLocation.Click += new System.EventHandler(this.btnSelectLocation_Click);
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(0, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(244, 1);
            this.label19.TabIndex = 97;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(244, 23);
            this.label20.TabIndex = 0;
            this.label20.Text = "Location";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.Location = new System.Drawing.Point(0, 106);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(244, 1);
            this.label21.TabIndex = 100;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Location = new System.Drawing.Point(0, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 107);
            this.label22.TabIndex = 93;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Location = new System.Drawing.Point(245, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 107);
            this.label23.TabIndex = 94;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Location = new System.Drawing.Point(0, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(246, 1);
            this.label24.TabIndex = 96;
            // 
            // cmbLocationName
            // 
            this.cmbLocationName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationName.FormattingEnabled = true;
            this.cmbLocationName.Location = new System.Drawing.Point(1035, -1);
            this.cmbLocationName.Name = "cmbLocationName";
            this.cmbLocationName.Size = new System.Drawing.Size(196, 22);
            this.cmbLocationName.TabIndex = 199;
            this.cmbLocationName.Visible = false;
            // 
            // pnlApptType
            // 
            this.pnlApptType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlApptType.Controls.Add(this.panel3);
            this.pnlApptType.Controls.Add(this.label14);
            this.pnlApptType.Controls.Add(this.label15);
            this.pnlApptType.Controls.Add(this.label16);
            this.pnlApptType.Location = new System.Drawing.Point(468, 13);
            this.pnlApptType.Name = "pnlApptType";
            this.pnlApptType.Size = new System.Drawing.Size(246, 108);
            this.pnlApptType.TabIndex = 200;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.trvApptType);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 107);
            this.panel3.TabIndex = 92;
            // 
            // trvApptType
            // 
            this.trvApptType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvApptType.CheckBoxes = true;
            this.trvApptType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvApptType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvApptType.ForeColor = System.Drawing.Color.Black;
            this.trvApptType.Location = new System.Drawing.Point(6, 26);
            this.trvApptType.Name = "trvApptType";
            this.trvApptType.ShowLines = false;
            this.trvApptType.ShowPlusMinus = false;
            this.trvApptType.ShowRootLines = false;
            this.trvApptType.Size = new System.Drawing.Size(238, 80);
            this.trvApptType.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(6, 80);
            this.label5.TabIndex = 99;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(244, 3);
            this.label6.TabIndex = 93;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.btnDeSelectApptType);
            this.panel4.Controls.Add(this.btnSelectApptType);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(244, 23);
            this.panel4.TabIndex = 92;
            // 
            // btnDeSelectApptType
            // 
            this.btnDeSelectApptType.BackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectApptType.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeSelectApptType.FlatAppearance.BorderSize = 0;
            this.btnDeSelectApptType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectApptType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectApptType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeSelectApptType.Image = ((System.Drawing.Image)(resources.GetObject("btnDeSelectApptType.Image")));
            this.btnDeSelectApptType.Location = new System.Drawing.Point(182, 0);
            this.btnDeSelectApptType.Name = "btnDeSelectApptType";
            this.btnDeSelectApptType.Size = new System.Drawing.Size(31, 22);
            this.btnDeSelectApptType.TabIndex = 99;
            this.btnDeSelectApptType.Tag = "Select";
            this.btnDeSelectApptType.UseVisualStyleBackColor = false;
            this.btnDeSelectApptType.Visible = false;
            this.btnDeSelectApptType.Click += new System.EventHandler(this.btnDeSelectApptType_Click);
            // 
            // btnSelectApptType
            // 
            this.btnSelectApptType.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectApptType.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectApptType.FlatAppearance.BorderSize = 0;
            this.btnSelectApptType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectApptType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectApptType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectApptType.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectApptType.Image")));
            this.btnSelectApptType.Location = new System.Drawing.Point(213, 0);
            this.btnSelectApptType.Name = "btnSelectApptType";
            this.btnSelectApptType.Size = new System.Drawing.Size(31, 22);
            this.btnSelectApptType.TabIndex = 98;
            this.btnSelectApptType.Tag = "Select";
            this.btnSelectApptType.UseVisualStyleBackColor = false;
            this.btnSelectApptType.Click += new System.EventHandler(this.btnSelectApptType_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(0, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(244, 1);
            this.label7.TabIndex = 97;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(244, 23);
            this.label13.TabIndex = 0;
            this.label13.Text = "Appointment Type";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(0, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(244, 1);
            this.label4.TabIndex = 100;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(0, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 107);
            this.label14.TabIndex = 93;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Location = new System.Drawing.Point(245, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 107);
            this.label15.TabIndex = 94;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(246, 1);
            this.label16.TabIndex = 96;
            // 
            // chkShowRemovedAppt
            // 
            this.chkShowRemovedAppt.AutoSize = true;
            this.chkShowRemovedAppt.Location = new System.Drawing.Point(983, 17);
            this.chkShowRemovedAppt.Name = "chkShowRemovedAppt";
            this.chkShowRemovedAppt.Size = new System.Drawing.Size(219, 18);
            this.chkShowRemovedAppt.TabIndex = 3;
            this.chkShowRemovedAppt.Text = "Show only Removed Appointments";
            this.chkShowRemovedAppt.UseVisualStyleBackColor = true;
            this.chkShowRemovedAppt.CheckedChanged += new System.EventHandler(this.chkShowRemovedAppt_CheckedChanged);
            // 
            // pnlProvider
            // 
            this.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProvider.Controls.Add(this.label26);
            this.pnlProvider.Controls.Add(this.panel1);
            this.pnlProvider.Controls.Add(this.label28);
            this.pnlProvider.Controls.Add(this.label29);
            this.pnlProvider.Controls.Add(this.label30);
            this.pnlProvider.Location = new System.Drawing.Point(214, 13);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(246, 108);
            this.pnlProvider.TabIndex = 199;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label26.Location = new System.Drawing.Point(1, 107);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(244, 1);
            this.label26.TabIndex = 97;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trvProvider);
            this.panel1.Controls.Add(this.label51);
            this.panel1.Controls.Add(this.label50);
            this.panel1.Controls.Add(this.pnlProviderHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 107);
            this.panel1.TabIndex = 92;
            // 
            // trvProvider
            // 
            this.trvProvider.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvProvider.CheckBoxes = true;
            this.trvProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvProvider.ForeColor = System.Drawing.Color.Black;
            this.trvProvider.Location = new System.Drawing.Point(6, 26);
            this.trvProvider.Name = "trvProvider";
            this.trvProvider.ShowLines = false;
            this.trvProvider.ShowPlusMinus = false;
            this.trvProvider.ShowRootLines = false;
            this.trvProvider.Size = new System.Drawing.Size(238, 81);
            this.trvProvider.TabIndex = 2;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.White;
            this.label51.Dock = System.Windows.Forms.DockStyle.Left;
            this.label51.Location = new System.Drawing.Point(0, 26);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(6, 81);
            this.label51.TabIndex = 99;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.White;
            this.label50.Dock = System.Windows.Forms.DockStyle.Top;
            this.label50.Location = new System.Drawing.Point(0, 23);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(244, 3);
            this.label50.TabIndex = 93;
            // 
            // pnlProviderHeader
            // 
            this.pnlProviderHeader.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlProviderHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProviderHeader.Controls.Add(this.btnDeSelectProvider);
            this.pnlProviderHeader.Controls.Add(this.btnSelectProvider);
            this.pnlProviderHeader.Controls.Add(this.label43);
            this.pnlProviderHeader.Controls.Add(this.label44);
            this.pnlProviderHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProviderHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlProviderHeader.Name = "pnlProviderHeader";
            this.pnlProviderHeader.Size = new System.Drawing.Size(244, 23);
            this.pnlProviderHeader.TabIndex = 92;
            // 
            // btnDeSelectProvider
            // 
            this.btnDeSelectProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectProvider.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeSelectProvider.FlatAppearance.BorderSize = 0;
            this.btnDeSelectProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeSelectProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnDeSelectProvider.Image")));
            this.btnDeSelectProvider.Location = new System.Drawing.Point(182, 0);
            this.btnDeSelectProvider.Name = "btnDeSelectProvider";
            this.btnDeSelectProvider.Size = new System.Drawing.Size(31, 22);
            this.btnDeSelectProvider.TabIndex = 99;
            this.btnDeSelectProvider.Tag = "Select";
            this.btnDeSelectProvider.UseVisualStyleBackColor = false;
            this.btnDeSelectProvider.Visible = false;
            this.btnDeSelectProvider.Click += new System.EventHandler(this.btnDeSelectProvider_Click);
            // 
            // btnSelectProvider
            // 
            this.btnSelectProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectProvider.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectProvider.FlatAppearance.BorderSize = 0;
            this.btnSelectProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectProvider.Image")));
            this.btnSelectProvider.Location = new System.Drawing.Point(213, 0);
            this.btnSelectProvider.Name = "btnSelectProvider";
            this.btnSelectProvider.Size = new System.Drawing.Size(31, 22);
            this.btnSelectProvider.TabIndex = 98;
            this.btnSelectProvider.Tag = "Select";
            this.btnSelectProvider.UseVisualStyleBackColor = false;
            this.btnSelectProvider.Click += new System.EventHandler(this.btnSelectProvider_Click);
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label43.Location = new System.Drawing.Point(0, 22);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(244, 1);
            this.label43.TabIndex = 97;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.label44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Location = new System.Drawing.Point(0, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(244, 23);
            this.label44.TabIndex = 0;
            this.label44.Text = " Providers";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Location = new System.Drawing.Point(0, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 107);
            this.label28.TabIndex = 93;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.Location = new System.Drawing.Point(245, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 107);
            this.label29.TabIndex = 94;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Top;
            this.label30.Location = new System.Drawing.Point(0, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(246, 1);
            this.label30.TabIndex = 96;
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(102, 16);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(103, 22);
            this.cmb_datefilter.TabIndex = 0;
            this.cmb_datefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(15, 20);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(84, 14);
            this.lbl_datefilter.TabIndex = 194;
            this.lbl_datefilter.Text = "Service Date :";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(102, 72);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(101, 22);
            this.dtpEndDate.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1213, 1);
            this.label9.TabIndex = 99;
            this.label9.Text = "label4";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(39, 76);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(60, 14);
            this.lblEndDate.TabIndex = 196;
            this.lblEndDate.Text = "To Date :";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1213, 1);
            this.label10.TabIndex = 98;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1217, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 134);
            this.label11.TabIndex = 97;
            this.label11.Text = "label4";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(102, 44);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(102, 22);
            this.dtpStartDate.TabIndex = 1;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 134);
            this.label12.TabIndex = 96;
            this.label12.Text = "label4";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(27, 48);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 195;
            this.lblStartDate.Text = "From Date :";
            // 
            // pnlC1Grid
            // 
            this.pnlC1Grid.Controls.Add(this.label3);
            this.pnlC1Grid.Controls.Add(this.label2);
            this.pnlC1Grid.Controls.Add(this.label1);
            this.pnlC1Grid.Controls.Add(this.label8);
            this.pnlC1Grid.Controls.Add(this.c1MissingCharges);
            this.pnlC1Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlC1Grid.Location = new System.Drawing.Point(0, 198);
            this.pnlC1Grid.Name = "pnlC1Grid";
            this.pnlC1Grid.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlC1Grid.Size = new System.Drawing.Size(1221, 509);
            this.pnlC1Grid.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 505);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1213, 1);
            this.label3.TabIndex = 91;
            this.label3.Text = "label4";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1213, 1);
            this.label2.TabIndex = 90;
            this.label2.Text = "label4";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1217, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 506);
            this.label1.TabIndex = 89;
            this.label1.Text = "label4";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 506);
            this.label8.TabIndex = 88;
            this.label8.Text = "label4";
            // 
            // c1MissingCharges
            // 
            this.c1MissingCharges.AllowEditing = false;
            this.c1MissingCharges.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1MissingCharges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1MissingCharges.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1MissingCharges.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1MissingCharges.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1MissingCharges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1MissingCharges.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1MissingCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1MissingCharges.ForeColor = System.Drawing.Color.DarkBlue;
            this.c1MissingCharges.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1MissingCharges.Location = new System.Drawing.Point(3, 0);
            this.c1MissingCharges.Name = "c1MissingCharges";
            this.c1MissingCharges.Rows.Count = 1;
            this.c1MissingCharges.Rows.DefaultSize = 19;
            this.c1MissingCharges.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1MissingCharges.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.WhenEditing;
            this.c1MissingCharges.Size = new System.Drawing.Size(1215, 506);
            this.c1MissingCharges.StyleInfo = resources.GetString("c1MissingCharges.StyleInfo");
            this.c1MissingCharges.TabIndex = 6;
            this.c1MissingCharges.DoubleClick += new System.EventHandler(this.c1MissingCharges_DoubleClick);
            this.c1MissingCharges.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1MissingCharges_MouseMove);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmRpt_MissingCharges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1221, 707);
            this.Controls.Add(this.pnlC1Grid);
            this.Controls.Add(this.pnlCriteria);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRpt_MissingCharges";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Missing Charges";
            this.Load += new System.EventHandler(this.frmRpt_MissingCharges_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlCriteria.ResumeLayout(false);
            this.pnlCriteria.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlLocation.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.pnlApptType.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlProvider.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlProviderHeader.ResumeLayout(false);
            this.pnlC1Grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1MissingCharges)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnlCriteria;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Panel pnlC1Grid;
        private C1.Win.C1FlexGrid.C1FlexGrid c1MissingCharges;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcelOpen;
        private System.Windows.Forms.ToolStripButton tls_Select;
        private System.Windows.Forms.ToolStripButton tls_RemoveAppt;
        private System.Windows.Forms.CheckBox chkShowRemovedAppt;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView trvProvider;
        internal System.Windows.Forms.Label label51;
        internal System.Windows.Forms.Label label50;
        private System.Windows.Forms.Panel pnlProviderHeader;
        private System.Windows.Forms.Button btnDeSelectProvider;
        private System.Windows.Forms.Button btnSelectProvider;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Panel pnlApptType;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TreeView trvApptType;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnDeSelectApptType;
        private System.Windows.Forms.Button btnSelectApptType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
		 private System.Windows.Forms.ComboBox cmbLocationName;
         private System.Windows.Forms.Label lblLocation;
         private System.Windows.Forms.Panel pnlLocation;
         private System.Windows.Forms.Panel panel5;
         private System.Windows.Forms.TreeView trvLocation;
         internal System.Windows.Forms.Label label17;
         internal System.Windows.Forms.Label label18;
         private System.Windows.Forms.Panel panel6;
         private System.Windows.Forms.Button btnDeSelectLocation;
         private System.Windows.Forms.Button btnSelectLocation;
         private System.Windows.Forms.Label label19;
         private System.Windows.Forms.Label label20;
         private System.Windows.Forms.Label label21;
         private System.Windows.Forms.Label label22;
         private System.Windows.Forms.Label label23;
         private System.Windows.Forms.Label label24;
         private System.Windows.Forms.Label label4;
         private System.Windows.Forms.CheckBox chkExcludeNoCharge;
         private System.Windows.Forms.ToolStripButton tlsUnlinkAppt;
         private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
         private System.Windows.Forms.GroupBox groupBox1;
         private System.Windows.Forms.RadioButton rdReportByDOS_Provider;
         private System.Windows.Forms.RadioButton rdReportByDOS;
    }
}
