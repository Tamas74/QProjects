namespace gloBilling
{
    partial class frmSetupPatientStatementCriteria
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtpOfficeEndTime, dtpOfficeStartTime };
            System.Windows.Forms.Control[] cntControls = { dtpOfficeEndTime, dtpOfficeStartTime };

            if (disposing && (components != null))
            {

                try
                {
                    if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }
                    //if (dtpOfficeEndTime != null)
                    //{
                    //    try
                    //    {
                    //        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpOfficeEndTime);
                    //    }
                    //    catch
                    //    {
                    //    }
                    //    dtpOfficeEndTime.Dispose();
                    //    dtpOfficeEndTime = null;
                    //}
                }
                catch
                {
                }

                //try
                //{
                //    if (dtpOfficeStartTime != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpOfficeStartTime);
                //        }
                //        catch
                //        {
                //        }
                //        dtpOfficeStartTime.Dispose();
                //        dtpOfficeStartTime = null;
                //    }
                //}
                //catch
                //{
                //}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupPatientStatementCriteria));
            this.pnl_tlsp_Top = new System.Windows.Forms.Panel();
            this.tstrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsp_btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnOK = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.Panel20 = new System.Windows.Forms.Panel();
            this.Panel21 = new System.Windows.Forms.Panel();
            this.chkDefault = new System.Windows.Forms.CheckBox();
            this.txtStatementCriteriaName = new System.Windows.Forms.TextBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label44 = new System.Windows.Forms.Label();
            this.Label45 = new System.Windows.Forms.Label();
            this.Label46 = new System.Windows.Forms.Label();
            this.Label47 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpFilter = new System.Windows.Forms.TabPage();
            this.label62 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.nmWaitFordays = new System.Windows.Forms.NumericUpDown();
            this.cmbNameTo = new System.Windows.Forms.ComboBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.cmbNameFrom = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.txtWaitFordays = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.lblCPT = new System.Windows.Forms.Label();
            this.cmbCPT = new System.Windows.Forms.ComboBox();
            this.btnClearCPT = new System.Windows.Forms.Button();
            this.btnBrowseCPT = new System.Windows.Forms.Button();
            this.btnClearInsurance = new System.Windows.Forms.Button();
            this.btnBrowseInsurance = new System.Windows.Forms.Button();
            this.txtDueAmount = new System.Windows.Forms.TextBox();
            this.rbGreaterthen = new System.Windows.Forms.RadioButton();
            this.rbLessThen = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.cmbFacility = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbPaymentTray = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbChargesTray = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbpDisplay = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label51 = new System.Windows.Forms.Label();
            this.chkGuarantorIndicator = new System.Windows.Forms.CheckBox();
            this.chkPendingInsurance = new System.Windows.Forms.CheckBox();
            this.txtClinicMessage2 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtClinicMessage1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtRemitName = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.cmbRemitState = new System.Windows.Forms.ComboBox();
            this.txtRemitZip = new System.Windows.Forms.TextBox();
            this.txtRemitCity = new System.Windows.Forms.TextBox();
            this.txtRemitAddress2 = new System.Windows.Forms.TextBox();
            this.txtRemitAddress1 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPracticeTaxID = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.dtpOfficeEndTime = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpOfficeStartTime = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.txtBillingContactPhone = new gloMaskControl.gloMaskBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBillingContactName = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.lbl_pnlProviderBottomBrd = new System.Windows.Forms.Label();
            this.pnlProviderBody = new System.Windows.Forms.Panel();
            this.trvCreditCard = new System.Windows.Forms.TreeView();
            this.pnlProviderHeader = new System.Windows.Forms.Panel();
            this.btnDeSelectCreditCard = new System.Windows.Forms.Button();
            this.btnSelectCreditCard = new System.Windows.Forms.Button();
            this.lbl_pnlProviderHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblCreditCard = new System.Windows.Forms.Label();
            this.lbl_pnlProviderLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderTopBrd = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnl = new System.Windows.Forms.Panel();
            this.lblAddressDetails = new System.Windows.Forms.Label();
            this.cmbPracState = new System.Windows.Forms.ComboBox();
            this.txtPracZip = new System.Windows.Forms.TextBox();
            this.txtPracCity = new System.Windows.Forms.TextBox();
            this.txtPracAddress2 = new System.Windows.Forms.TextBox();
            this.txtPracAddress1 = new System.Windows.Forms.TextBox();
            this.lblOIZip = new System.Windows.Forms.Label();
            this.lblOIState = new System.Windows.Forms.Label();
            this.lblOICity = new System.Windows.Forms.Label();
            this.lblOIAddressLine2 = new System.Windows.Forms.Label();
            this.lblOIAddressLine1 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.imgtabControl1 = new System.Windows.Forms.ImageList(this.components);
            this.pnl_tlsp_Top.SuspendLayout();
            this.tstrip.SuspendLayout();
            this.Panel20.SuspendLayout();
            this.Panel21.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmWaitFordays)).BeginInit();
            this.tbpDisplay.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlProviderBody.SuspendLayout();
            this.pnlProviderHeader.SuspendLayout();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlsp_Top
            // 
            this.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp_Top.Controls.Add(this.tstrip);
            this.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp_Top.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlsp_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp_Top.Name = "pnl_tlsp_Top";
            this.pnl_tlsp_Top.Size = new System.Drawing.Size(547, 54);
            this.pnl_tlsp_Top.TabIndex = 18;
            // 
            // tstrip
            // 
            this.tstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tstrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tstrip.BackgroundImage")));
            this.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tstrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tstrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsp_btnSave,
            this.btnOK,
            this.btnClose});
            this.tstrip.Location = new System.Drawing.Point(0, 0);
            this.tstrip.Name = "tstrip";
            this.tstrip.Size = new System.Drawing.Size(547, 53);
            this.tstrip.TabIndex = 0;
            this.tstrip.Text = "ToolStrip1";
            this.tstrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tlsp_btnSave
            // 
            this.tlsp_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnSave.Image")));
            this.tlsp_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnSave.Name = "tlsp_btnSave";
            this.tlsp_btnSave.Size = new System.Drawing.Size(40, 50);
            this.tlsp_btnSave.Tag = "Save";
            this.tlsp_btnSave.Text = "&Save";
            this.tlsp_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnSave.ToolTipText = "Save";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 50);
            this.btnOK.Tag = "Save&Close";
            this.btnOK.Text = "Sa&ve&&Cls";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOK.ToolTipText = "Save and Close";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(43, 50);
            this.btnClose.Tag = "Close";
            this.btnClose.Text = "&Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.ToolTipText = "Close";
            // 
            // Panel20
            // 
            this.Panel20.Controls.Add(this.Panel21);
            this.Panel20.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel20.Location = new System.Drawing.Point(0, 54);
            this.Panel20.Name = "Panel20";
            this.Panel20.Padding = new System.Windows.Forms.Padding(3);
            this.Panel20.Size = new System.Drawing.Size(547, 30);
            this.Panel20.TabIndex = 81;
            // 
            // Panel21
            // 
            this.Panel21.BackColor = System.Drawing.Color.Transparent;
            this.Panel21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel21.BackgroundImage")));
            this.Panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel21.Controls.Add(this.chkDefault);
            this.Panel21.Controls.Add(this.txtStatementCriteriaName);
            this.Panel21.Controls.Add(this.Label23);
            this.Panel21.Controls.Add(this.Label44);
            this.Panel21.Controls.Add(this.Label45);
            this.Panel21.Controls.Add(this.Label46);
            this.Panel21.Controls.Add(this.Label47);
            this.Panel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel21.Location = new System.Drawing.Point(3, 3);
            this.Panel21.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel21.Name = "Panel21";
            this.Panel21.Size = new System.Drawing.Size(541, 24);
            this.Panel21.TabIndex = 19;
            // 
            // chkDefault
            // 
            this.chkDefault.AutoSize = true;
            this.chkDefault.Location = new System.Drawing.Point(374, 3);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Size = new System.Drawing.Size(65, 18);
            this.chkDefault.TabIndex = 2;
            this.chkDefault.Text = "Default";
            this.chkDefault.UseVisualStyleBackColor = true;
            // 
            // txtStatementCriteriaName
            // 
            this.txtStatementCriteriaName.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtStatementCriteriaName.Location = new System.Drawing.Point(63, 1);
            this.txtStatementCriteriaName.MaxLength = 50;
            this.txtStatementCriteriaName.Name = "txtStatementCriteriaName";
            this.txtStatementCriteriaName.Size = new System.Drawing.Size(272, 22);
            this.txtStatementCriteriaName.TabIndex = 1;
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(1, 1);
            this.Label23.Name = "Label23";
            this.Label23.Padding = new System.Windows.Forms.Padding(3);
            this.Label23.Size = new System.Drawing.Size(62, 20);
            this.Label23.TabIndex = 10;
            this.Label23.Text = "  Name :";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label44
            // 
            this.Label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label44.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label44.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label44.Location = new System.Drawing.Point(1, 23);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(539, 1);
            this.Label44.TabIndex = 8;
            this.Label44.Text = "label2";
            // 
            // Label45
            // 
            this.Label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label45.Location = new System.Drawing.Point(0, 1);
            this.Label45.Name = "Label45";
            this.Label45.Size = new System.Drawing.Size(1, 23);
            this.Label45.TabIndex = 7;
            this.Label45.Text = "label4";
            // 
            // Label46
            // 
            this.Label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label46.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label46.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label46.Location = new System.Drawing.Point(540, 1);
            this.Label46.Name = "Label46";
            this.Label46.Size = new System.Drawing.Size(1, 23);
            this.Label46.TabIndex = 6;
            this.Label46.Text = "label3";
            // 
            // Label47
            // 
            this.Label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label47.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label47.Location = new System.Drawing.Point(0, 0);
            this.Label47.Name = "Label47";
            this.Label47.Size = new System.Drawing.Size(541, 1);
            this.Label47.TabIndex = 5;
            this.Label47.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 84);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(547, 659);
            this.panel1.TabIndex = 82;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpFilter);
            this.tabControl1.Controls.Add(this.tbpDisplay);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imgtabControl1;
            this.tabControl1.ItemSize = new System.Drawing.Size(60, 21);
            this.tabControl1.Location = new System.Drawing.Point(3, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(541, 656);
            this.tabControl1.TabIndex = 0;
            // 
            // tbpFilter
            // 
            this.tbpFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpFilter.Controls.Add(this.label62);
            this.tbpFilter.Controls.Add(this.label61);
            this.tbpFilter.Controls.Add(this.nmWaitFordays);
            this.tbpFilter.Controls.Add(this.cmbNameTo);
            this.tbpFilter.Controls.Add(this.label59);
            this.tbpFilter.Controls.Add(this.label60);
            this.tbpFilter.Controls.Add(this.cmbNameFrom);
            this.tbpFilter.Controls.Add(this.label58);
            this.tbpFilter.Controls.Add(this.label57);
            this.tbpFilter.Controls.Add(this.txtWaitFordays);
            this.tbpFilter.Controls.Add(this.label56);
            this.tbpFilter.Controls.Add(this.label55);
            this.tbpFilter.Controls.Add(this.label54);
            this.tbpFilter.Controls.Add(this.label53);
            this.tbpFilter.Controls.Add(this.lblCPT);
            this.tbpFilter.Controls.Add(this.cmbCPT);
            this.tbpFilter.Controls.Add(this.btnClearCPT);
            this.tbpFilter.Controls.Add(this.btnBrowseCPT);
            this.tbpFilter.Controls.Add(this.btnClearInsurance);
            this.tbpFilter.Controls.Add(this.btnBrowseInsurance);
            this.tbpFilter.Controls.Add(this.txtDueAmount);
            this.tbpFilter.Controls.Add(this.rbGreaterthen);
            this.tbpFilter.Controls.Add(this.rbLessThen);
            this.tbpFilter.Controls.Add(this.label1);
            this.tbpFilter.Controls.Add(this.label11);
            this.tbpFilter.Controls.Add(this.txtZipCode);
            this.tbpFilter.Controls.Add(this.cmbFacility);
            this.tbpFilter.Controls.Add(this.label9);
            this.tbpFilter.Controls.Add(this.cmbPaymentTray);
            this.tbpFilter.Controls.Add(this.label7);
            this.tbpFilter.Controls.Add(this.cmbChargesTray);
            this.tbpFilter.Controls.Add(this.label6);
            this.tbpFilter.Controls.Add(this.cmbInsurance);
            this.tbpFilter.Controls.Add(this.label3);
            this.tbpFilter.ImageIndex = 0;
            this.tbpFilter.Location = new System.Drawing.Point(4, 25);
            this.tbpFilter.Name = "tbpFilter";
            this.tbpFilter.Size = new System.Drawing.Size(533, 627);
            this.tbpFilter.TabIndex = 0;
            this.tbpFilter.Text = "Filter";
            this.tbpFilter.Click += new System.EventHandler(this.tbpFilter_Click);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label62.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Location = new System.Drawing.Point(328, 80);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(32, 14);
            this.label62.TabIndex = 320;
            this.label62.Text = "Days";
            this.label62.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.BackColor = System.Drawing.Color.Transparent;
            this.label61.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label61.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Location = new System.Drawing.Point(154, 75);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(61, 14);
            this.label61.TabIndex = 319;
            this.label61.Text = "Wait For :";
            this.label61.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // nmWaitFordays
            // 
            this.nmWaitFordays.Location = new System.Drawing.Point(225, 73);
            this.nmWaitFordays.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nmWaitFordays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmWaitFordays.Name = "nmWaitFordays";
            this.nmWaitFordays.Size = new System.Drawing.Size(92, 22);
            this.nmWaitFordays.TabIndex = 318;
            this.nmWaitFordays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbNameTo
            // 
            this.cmbNameTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNameTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNameTo.FormattingEnabled = true;
            this.cmbNameTo.Location = new System.Drawing.Point(362, 105);
            this.cmbNameTo.Name = "cmbNameTo";
            this.cmbNameTo.Size = new System.Drawing.Size(46, 22);
            this.cmbNameTo.TabIndex = 315;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label59.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Location = new System.Drawing.Point(326, 108);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(30, 14);
            this.label59.TabIndex = 316;
            this.label59.Text = "To :";
            this.label59.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.BackColor = System.Drawing.Color.Transparent;
            this.label60.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label60.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Location = new System.Drawing.Point(223, 108);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(42, 14);
            this.label60.TabIndex = 317;
            this.label60.Text = "From :";
            this.label60.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cmbNameFrom
            // 
            this.cmbNameFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNameFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNameFrom.FormattingEnabled = true;
            this.cmbNameFrom.Location = new System.Drawing.Point(271, 105);
            this.cmbNameFrom.Name = "cmbNameFrom";
            this.cmbNameFrom.Size = new System.Drawing.Size(46, 22);
            this.cmbNameFrom.TabIndex = 314;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.BackColor = System.Drawing.Color.Transparent;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label58.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Location = new System.Drawing.Point(126, 108);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(89, 14);
            this.label58.TabIndex = 313;
            this.label58.Text = "Patient Name :";
            this.label58.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label57.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Location = new System.Drawing.Point(56, 366);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(90, 14);
            this.label57.TabIndex = 312;
            this.label57.Text = "Wait For Days :";
            this.label57.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label57.Visible = false;
            // 
            // txtWaitFordays
            // 
            this.txtWaitFordays.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtWaitFordays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtWaitFordays.Location = new System.Drawing.Point(147, 359);
            this.txtWaitFordays.MaxLength = 10;
            this.txtWaitFordays.Name = "txtWaitFordays";
            this.txtWaitFordays.Size = new System.Drawing.Size(58, 22);
            this.txtWaitFordays.TabIndex = 311;
            this.txtWaitFordays.Visible = false;
            this.txtWaitFordays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWaitFordays_KeyPress);
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Right;
            this.label56.Location = new System.Drawing.Point(532, 1);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(1, 625);
            this.label56.TabIndex = 310;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Left;
            this.label55.Location = new System.Drawing.Point(0, 1);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1, 625);
            this.label55.TabIndex = 309;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Top;
            this.label54.Location = new System.Drawing.Point(0, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(533, 1);
            this.label54.TabIndex = 308;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label53.Location = new System.Drawing.Point(0, 626);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(533, 1);
            this.label53.TabIndex = 307;
            // 
            // lblCPT
            // 
            this.lblCPT.AutoSize = true;
            this.lblCPT.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCPT.Location = new System.Drawing.Point(105, 396);
            this.lblCPT.Name = "lblCPT";
            this.lblCPT.Size = new System.Drawing.Size(37, 14);
            this.lblCPT.TabIndex = 250;
            this.lblCPT.Text = "CPT :";
            this.lblCPT.Visible = false;
            // 
            // cmbCPT
            // 
            this.cmbCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCPT.ForeColor = System.Drawing.Color.Black;
            this.cmbCPT.FormattingEnabled = true;
            this.cmbCPT.Location = new System.Drawing.Point(147, 392);
            this.cmbCPT.Name = "cmbCPT";
            this.cmbCPT.Size = new System.Drawing.Size(301, 22);
            this.cmbCPT.TabIndex = 5;
            this.cmbCPT.Visible = false;
            // 
            // btnClearCPT
            // 
            this.btnClearCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.BackgroundImage")));
            this.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.Image")));
            this.btnClearCPT.Location = new System.Drawing.Point(479, 392);
            this.btnClearCPT.Name = "btnClearCPT";
            this.btnClearCPT.Size = new System.Drawing.Size(22, 22);
            this.btnClearCPT.TabIndex = 7;
            this.btnClearCPT.UseVisualStyleBackColor = false;
            this.btnClearCPT.Visible = false;
            this.btnClearCPT.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearCPT.Click += new System.EventHandler(this.btnClearCPT_Click);
            this.btnClearCPT.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseCPT
            // 
            this.btnBrowseCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.BackgroundImage")));
            this.btnBrowseCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.Image")));
            this.btnBrowseCPT.Location = new System.Drawing.Point(453, 392);
            this.btnBrowseCPT.Name = "btnBrowseCPT";
            this.btnBrowseCPT.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseCPT.TabIndex = 6;
            this.btnBrowseCPT.UseVisualStyleBackColor = false;
            this.btnBrowseCPT.Visible = false;
            this.btnBrowseCPT.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseCPT.Click += new System.EventHandler(this.btnBrowseCPT_Click);
            this.btnBrowseCPT.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearInsurance
            // 
            this.btnClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.BackgroundImage")));
            this.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.Image")));
            this.btnClearInsurance.Location = new System.Drawing.Point(479, 423);
            this.btnClearInsurance.Name = "btnClearInsurance";
            this.btnClearInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnClearInsurance.TabIndex = 10;
            this.btnClearInsurance.UseVisualStyleBackColor = false;
            this.btnClearInsurance.Visible = false;
            this.btnClearInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearInsurance.Click += new System.EventHandler(this.btnClearInsurance_Click);
            this.btnClearInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseInsurance
            // 
            this.btnBrowseInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.BackgroundImage")));
            this.btnBrowseInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.Image")));
            this.btnBrowseInsurance.Location = new System.Drawing.Point(453, 423);
            this.btnBrowseInsurance.Name = "btnBrowseInsurance";
            this.btnBrowseInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseInsurance.TabIndex = 9;
            this.btnBrowseInsurance.UseVisualStyleBackColor = false;
            this.btnBrowseInsurance.Visible = false;
            this.btnBrowseInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseInsurance.Click += new System.EventHandler(this.btnBrowseInsurance_Click);
            this.btnBrowseInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // txtDueAmount
            // 
            this.txtDueAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtDueAmount.Location = new System.Drawing.Point(225, 41);
            this.txtDueAmount.MaxLength = 20;
            this.txtDueAmount.Name = "txtDueAmount";
            this.txtDueAmount.Size = new System.Drawing.Size(183, 22);
            this.txtDueAmount.TabIndex = 3;
            this.txtDueAmount.Text = "0.00";
            this.txtDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDueAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDueAmount_KeyPress);
            // 
            // rbGreaterthen
            // 
            this.rbGreaterthen.AutoSize = true;
            this.rbGreaterthen.Checked = true;
            this.rbGreaterthen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGreaterthen.Location = new System.Drawing.Point(213, 363);
            this.rbGreaterthen.Name = "rbGreaterthen";
            this.rbGreaterthen.Size = new System.Drawing.Size(104, 18);
            this.rbGreaterthen.TabIndex = 4;
            this.rbGreaterthen.TabStop = true;
            this.rbGreaterthen.Text = "Greater Than";
            this.rbGreaterthen.UseVisualStyleBackColor = true;
            this.rbGreaterthen.Visible = false;
            this.rbGreaterthen.CheckedChanged += new System.EventHandler(this.rbGreaterthen_CheckedChanged);
            // 
            // rbLessThen
            // 
            this.rbLessThen.AutoSize = true;
            this.rbLessThen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLessThen.Location = new System.Drawing.Point(329, 363);
            this.rbLessThen.Name = "rbLessThen";
            this.rbLessThen.Size = new System.Drawing.Size(80, 18);
            this.rbLessThen.TabIndex = 4;
            this.rbLessThen.Text = "Less Than";
            this.rbLessThen.UseVisualStyleBackColor = true;
            this.rbLessThen.Visible = false;
            this.rbLessThen.CheckedChanged += new System.EventHandler(this.rbLessThen_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(154, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 14);
            this.label1.TabIndex = 246;
            this.label1.Text = "Balance  :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(79, 551);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 14);
            this.label11.TabIndex = 244;
            this.label11.Text = "Zip Code :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label11.Visible = false;
            // 
            // txtZipCode
            // 
            this.txtZipCode.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtZipCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtZipCode.Location = new System.Drawing.Point(147, 547);
            this.txtZipCode.MaxLength = 10;
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(301, 22);
            this.txtZipCode.TabIndex = 14;
            this.txtZipCode.Visible = false;
            // 
            // cmbFacility
            // 
            this.cmbFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFacility.ForeColor = System.Drawing.Color.Black;
            this.cmbFacility.FormattingEnabled = true;
            this.cmbFacility.Location = new System.Drawing.Point(147, 516);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new System.Drawing.Size(301, 22);
            this.cmbFacility.TabIndex = 13;
            this.cmbFacility.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(92, 520);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 14);
            this.label9.TabIndex = 239;
            this.label9.Text = "Facility :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label9.Visible = false;
            // 
            // cmbPaymentTray
            // 
            this.cmbPaymentTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentTray.ForeColor = System.Drawing.Color.Black;
            this.cmbPaymentTray.FormattingEnabled = true;
            this.cmbPaymentTray.Location = new System.Drawing.Point(147, 485);
            this.cmbPaymentTray.Name = "cmbPaymentTray";
            this.cmbPaymentTray.Size = new System.Drawing.Size(301, 22);
            this.cmbPaymentTray.TabIndex = 12;
            this.cmbPaymentTray.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(51, 489);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 14);
            this.label7.TabIndex = 237;
            this.label7.Text = "Payment Tray :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label7.Visible = false;
            // 
            // cmbChargesTray
            // 
            this.cmbChargesTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChargesTray.ForeColor = System.Drawing.Color.Black;
            this.cmbChargesTray.FormattingEnabled = true;
            this.cmbChargesTray.Location = new System.Drawing.Point(147, 454);
            this.cmbChargesTray.Name = "cmbChargesTray";
            this.cmbChargesTray.Size = new System.Drawing.Size(301, 22);
            this.cmbChargesTray.TabIndex = 11;
            this.cmbChargesTray.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(61, 458);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 14);
            this.label6.TabIndex = 235;
            this.label6.Text = "Charge Tray :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label6.Visible = false;
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.ForeColor = System.Drawing.Color.Black;
            this.cmbInsurance.FormattingEnabled = true;
            this.cmbInsurance.Location = new System.Drawing.Point(147, 423);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(301, 22);
            this.cmbInsurance.TabIndex = 8;
            this.cmbInsurance.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(74, 427);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 14);
            this.label3.TabIndex = 231;
            this.label3.Text = "Insurance :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label3.Visible = false;
            // 
            // tbpDisplay
            // 
            this.tbpDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpDisplay.Controls.Add(this.panel7);
            this.tbpDisplay.Controls.Add(this.panel6);
            this.tbpDisplay.Controls.Add(this.panel5);
            this.tbpDisplay.Controls.Add(this.panel4);
            this.tbpDisplay.Controls.Add(this.panel3);
            this.tbpDisplay.Controls.Add(this.panel2);
            this.tbpDisplay.Controls.Add(this.pnl);
            this.tbpDisplay.ImageIndex = 1;
            this.tbpDisplay.Location = new System.Drawing.Point(4, 25);
            this.tbpDisplay.Name = "tbpDisplay";
            this.tbpDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tbpDisplay.Size = new System.Drawing.Size(533, 627);
            this.tbpDisplay.TabIndex = 1;
            this.tbpDisplay.Text = "Display";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label51);
            this.panel7.Controls.Add(this.chkGuarantorIndicator);
            this.panel7.Controls.Add(this.chkPendingInsurance);
            this.panel7.Controls.Add(this.txtClinicMessage2);
            this.panel7.Controls.Add(this.label30);
            this.panel7.Controls.Add(this.label16);
            this.panel7.Controls.Add(this.txtClinicMessage1);
            this.panel7.Controls.Add(this.label15);
            this.panel7.Controls.Add(this.label39);
            this.panel7.Controls.Add(this.label50);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 592);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(527, 32);
            this.panel7.TabIndex = 330;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label51.Location = new System.Drawing.Point(1, 31);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(525, 1);
            this.label51.TabIndex = 317;
            // 
            // chkGuarantorIndicator
            // 
            this.chkGuarantorIndicator.AutoSize = true;
            this.chkGuarantorIndicator.Location = new System.Drawing.Point(207, 68);
            this.chkGuarantorIndicator.Name = "chkGuarantorIndicator";
            this.chkGuarantorIndicator.Size = new System.Drawing.Size(132, 18);
            this.chkGuarantorIndicator.TabIndex = 33;
            this.chkGuarantorIndicator.Text = "Guarantor Indicator";
            this.chkGuarantorIndicator.UseVisualStyleBackColor = true;
            // 
            // chkPendingInsurance
            // 
            this.chkPendingInsurance.AutoSize = true;
            this.chkPendingInsurance.Location = new System.Drawing.Point(206, 12);
            this.chkPendingInsurance.Name = "chkPendingInsurance";
            this.chkPendingInsurance.Size = new System.Drawing.Size(127, 18);
            this.chkPendingInsurance.TabIndex = 30;
            this.chkPendingInsurance.Text = "Pending Insurance";
            this.chkPendingInsurance.UseVisualStyleBackColor = true;
            // 
            // txtClinicMessage2
            // 
            this.txtClinicMessage2.Location = new System.Drawing.Point(206, 66);
            this.txtClinicMessage2.MaxLength = 255;
            this.txtClinicMessage2.Name = "txtClinicMessage2";
            this.txtClinicMessage2.Size = new System.Drawing.Size(297, 22);
            this.txtClinicMessage2.TabIndex = 32;
            this.txtClinicMessage2.Visible = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Location = new System.Drawing.Point(101, 71);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(102, 14);
            this.label30.TabIndex = 314;
            this.label30.Text = "Clinic Message 2 :";
            this.label30.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label30.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(101, 41);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(102, 14);
            this.label16.TabIndex = 312;
            this.label16.Text = "Clinic Message 1 :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtClinicMessage1
            // 
            this.txtClinicMessage1.Location = new System.Drawing.Point(206, 37);
            this.txtClinicMessage1.MaxLength = 255;
            this.txtClinicMessage1.Name = "txtClinicMessage1";
            this.txtClinicMessage1.Size = new System.Drawing.Size(297, 22);
            this.txtClinicMessage1.TabIndex = 31;
            // 
            // label15
            // 
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(525, 12);
            this.label15.TabIndex = 305;
            this.label15.Text = "Other Information :";
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Left;
            this.label39.Location = new System.Drawing.Point(0, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(1, 32);
            this.label39.TabIndex = 315;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Right;
            this.label50.Location = new System.Drawing.Point(526, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 32);
            this.label50.TabIndex = 316;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtRemitName);
            this.panel6.Controls.Add(this.label63);
            this.panel6.Controls.Add(this.label32);
            this.panel6.Controls.Add(this.cmbRemitState);
            this.panel6.Controls.Add(this.txtRemitZip);
            this.panel6.Controls.Add(this.txtRemitCity);
            this.panel6.Controls.Add(this.txtRemitAddress2);
            this.panel6.Controls.Add(this.txtRemitAddress1);
            this.panel6.Controls.Add(this.label25);
            this.panel6.Controls.Add(this.label26);
            this.panel6.Controls.Add(this.label27);
            this.panel6.Controls.Add(this.label28);
            this.panel6.Controls.Add(this.label29);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label38);
            this.panel6.Controls.Add(this.label49);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 437);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(527, 155);
            this.panel6.TabIndex = 329;
            // 
            // txtRemitName
            // 
            this.txtRemitName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemitName.ForeColor = System.Drawing.Color.Black;
            this.txtRemitName.Location = new System.Drawing.Point(206, 19);
            this.txtRemitName.MaxLength = 50;
            this.txtRemitName.Name = "txtRemitName";
            this.txtRemitName.Size = new System.Drawing.Size(297, 22);
            this.txtRemitName.TabIndex = 318;
            // 
            // label63
            // 
            this.label63.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label63.AutoEllipsis = true;
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Location = new System.Drawing.Point(127, 23);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(81, 14);
            this.label63.TabIndex = 319;
            this.label63.Text = "Remit Name :";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label32.Location = new System.Drawing.Point(1, 154);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(525, 1);
            this.label32.TabIndex = 315;
            // 
            // cmbRemitState
            // 
            this.cmbRemitState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemitState.FormattingEnabled = true;
            this.cmbRemitState.Location = new System.Drawing.Point(407, 98);
            this.cmbRemitState.MaxLength = 20;
            this.cmbRemitState.Name = "cmbRemitState";
            this.cmbRemitState.Size = new System.Drawing.Size(94, 22);
            this.cmbRemitState.TabIndex = 29;
            // 
            // txtRemitZip
            // 
            this.txtRemitZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemitZip.ForeColor = System.Drawing.Color.Black;
            this.txtRemitZip.Location = new System.Drawing.Point(206, 124);
            this.txtRemitZip.MaxLength = 10;
            this.txtRemitZip.Name = "txtRemitZip";
            this.txtRemitZip.Size = new System.Drawing.Size(74, 22);
            this.txtRemitZip.TabIndex = 27;
            this.txtRemitZip.Leave += new System.EventHandler(this.txtRemitZip_Leave);
            this.txtRemitZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemitZip_KeyPress);
            // 
            // txtRemitCity
            // 
            this.txtRemitCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemitCity.ForeColor = System.Drawing.Color.Black;
            this.txtRemitCity.Location = new System.Drawing.Point(206, 98);
            this.txtRemitCity.MaxLength = 50;
            this.txtRemitCity.Name = "txtRemitCity";
            this.txtRemitCity.Size = new System.Drawing.Size(151, 22);
            this.txtRemitCity.TabIndex = 28;
            // 
            // txtRemitAddress2
            // 
            this.txtRemitAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemitAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtRemitAddress2.Location = new System.Drawing.Point(206, 72);
            this.txtRemitAddress2.MaxLength = 50;
            this.txtRemitAddress2.Name = "txtRemitAddress2";
            this.txtRemitAddress2.Size = new System.Drawing.Size(296, 22);
            this.txtRemitAddress2.TabIndex = 26;
            // 
            // txtRemitAddress1
            // 
            this.txtRemitAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemitAddress1.ForeColor = System.Drawing.Color.Black;
            this.txtRemitAddress1.Location = new System.Drawing.Point(206, 46);
            this.txtRemitAddress1.MaxLength = 50;
            this.txtRemitAddress1.Name = "txtRemitAddress1";
            this.txtRemitAddress1.Size = new System.Drawing.Size(297, 22);
            this.txtRemitAddress1.TabIndex = 25;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoEllipsis = true;
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(172, 128);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(31, 14);
            this.label25.TabIndex = 314;
            this.label25.Text = "Zip :";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Location = new System.Drawing.Point(359, 102);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(45, 14);
            this.label26.TabIndex = 311;
            this.label26.Text = "State :";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoEllipsis = true;
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Location = new System.Drawing.Point(168, 102);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(35, 14);
            this.label27.TabIndex = 310;
            this.label27.Text = "City :";
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoEllipsis = true;
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Location = new System.Drawing.Point(108, 76);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(95, 14);
            this.label28.TabIndex = 309;
            this.label28.Text = "Address Line 2 :";
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoEllipsis = true;
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Location = new System.Drawing.Point(108, 50);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(95, 14);
            this.label29.TabIndex = 308;
            this.label29.Text = "Address Line 1 :";
            // 
            // label22
            // 
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(1, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(525, 13);
            this.label22.TabIndex = 304;
            this.label22.Text = "Remit Address :";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Left;
            this.label38.Location = new System.Drawing.Point(0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 155);
            this.label38.TabIndex = 316;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Right;
            this.label49.Location = new System.Drawing.Point(526, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(1, 155);
            this.label49.TabIndex = 317;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label31);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.txtPracticeTaxID);
            this.panel5.Controls.Add(this.label21);
            this.panel5.Controls.Add(this.label37);
            this.panel5.Controls.Add(this.label48);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 395);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(527, 42);
            this.panel5.TabIndex = 328;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label31.Location = new System.Drawing.Point(1, 41);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(525, 1);
            this.label31.TabIndex = 306;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoEllipsis = true;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(105, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 14);
            this.label13.TabIndex = 305;
            this.label13.Text = "Practice Tax ID :";
            // 
            // txtPracticeTaxID
            // 
            this.txtPracticeTaxID.Location = new System.Drawing.Point(206, 12);
            this.txtPracticeTaxID.MaxLength = 50;
            this.txtPracticeTaxID.Name = "txtPracticeTaxID";
            this.txtPracticeTaxID.Size = new System.Drawing.Size(193, 22);
            this.txtPracticeTaxID.TabIndex = 24;
            // 
            // label21
            // 
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(1, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(525, 13);
            this.label21.TabIndex = 303;
            this.label21.Text = "Tax Information :";
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Left;
            this.label37.Location = new System.Drawing.Point(0, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 42);
            this.label37.TabIndex = 307;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Right;
            this.label48.Location = new System.Drawing.Point(526, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(1, 42);
            this.label48.TabIndex = 308;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.dtpOfficeEndTime);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.dtpOfficeStartTime);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.label36);
            this.panel4.Controls.Add(this.label43);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 328);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(527, 67);
            this.panel4.TabIndex = 327;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(1, 66);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(525, 1);
            this.label24.TabIndex = 328;
            // 
            // dtpOfficeEndTime
            // 
            this.dtpOfficeEndTime.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(66)))), ((int)(((byte)(125)))));
            this.dtpOfficeEndTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpOfficeEndTime.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(146)))), ((int)(((byte)(207)))));
            this.dtpOfficeEndTime.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpOfficeEndTime.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(222)))));
            this.dtpOfficeEndTime.CustomFormat = "hh:mm tt";
            this.dtpOfficeEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOfficeEndTime.Location = new System.Drawing.Point(206, 39);
            this.dtpOfficeEndTime.Name = "dtpOfficeEndTime";
            this.dtpOfficeEndTime.ShowUpDown = true;
            this.dtpOfficeEndTime.Size = new System.Drawing.Size(95, 22);
            this.dtpOfficeEndTime.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoEllipsis = true;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(98, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 14);
            this.label12.TabIndex = 327;
            this.label12.Text = "Office End Time :";
            // 
            // dtpOfficeStartTime
            // 
            this.dtpOfficeStartTime.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(66)))), ((int)(((byte)(125)))));
            this.dtpOfficeStartTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpOfficeStartTime.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(146)))), ((int)(((byte)(207)))));
            this.dtpOfficeStartTime.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpOfficeStartTime.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(222)))));
            this.dtpOfficeStartTime.CustomFormat = "hh:mm tt";
            this.dtpOfficeStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOfficeStartTime.Location = new System.Drawing.Point(206, 13);
            this.dtpOfficeStartTime.Name = "dtpOfficeStartTime";
            this.dtpOfficeStartTime.ShowUpDown = true;
            this.dtpOfficeStartTime.Size = new System.Drawing.Size(95, 22);
            this.dtpOfficeStartTime.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(92, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 14);
            this.label8.TabIndex = 325;
            this.label8.Text = "Office Start Time :";
            // 
            // label20
            // 
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(1, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(525, 13);
            this.label20.TabIndex = 302;
            this.label20.Text = "Office Hours :";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 67);
            this.label36.TabIndex = 329;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Right;
            this.label43.Location = new System.Drawing.Point(526, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 67);
            this.label43.TabIndex = 330;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.txtBillingContactPhone);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtBillingContactName);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label35);
            this.panel3.Controls.Add(this.label42);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 256);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(527, 72);
            this.panel3.TabIndex = 326;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(1, 71);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(525, 1);
            this.label18.TabIndex = 307;
            // 
            // txtBillingContactPhone
            // 
            this.txtBillingContactPhone.AllowValidate = true;
            this.txtBillingContactPhone.IncludeLiteralsAndPrompts = false;
            this.txtBillingContactPhone.Location = new System.Drawing.Point(206, 42);
            this.txtBillingContactPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtBillingContactPhone.Name = "txtBillingContactPhone";
            this.txtBillingContactPhone.ReadOnly = false;
            this.txtBillingContactPhone.Size = new System.Drawing.Size(96, 22);
            this.txtBillingContactPhone.TabIndex = 306;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(73, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 14);
            this.label5.TabIndex = 305;
            this.label5.Text = "Billing Contact Phone :";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(77, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 14);
            this.label4.TabIndex = 304;
            this.label4.Text = "Billing Contact Name :";
            // 
            // txtBillingContactName
            // 
            this.txtBillingContactName.Location = new System.Drawing.Point(206, 17);
            this.txtBillingContactName.MaxLength = 50;
            this.txtBillingContactName.Name = "txtBillingContactName";
            this.txtBillingContactName.Size = new System.Drawing.Size(297, 22);
            this.txtBillingContactName.TabIndex = 20;
            // 
            // label19
            // 
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(1, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(525, 14);
            this.label19.TabIndex = 301;
            this.label19.Text = "Billing Contact Information :";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Left;
            this.label35.Location = new System.Drawing.Point(0, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 72);
            this.label35.TabIndex = 308;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Right;
            this.label42.Location = new System.Drawing.Point(526, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1, 72);
            this.label42.TabIndex = 309;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.pnlProvider);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label34);
            this.panel2.Controls.Add(this.label41);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 128);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(527, 128);
            this.panel2.TabIndex = 325;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(1, 127);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(525, 1);
            this.label14.TabIndex = 302;
            // 
            // pnlProvider
            // 
            this.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderBottomBrd);
            this.pnlProvider.Controls.Add(this.pnlProviderBody);
            this.pnlProvider.Controls.Add(this.pnlProviderHeader);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderLeftBrd);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderRightBrd);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderTopBrd);
            this.pnlProvider.Location = new System.Drawing.Point(206, 17);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(294, 105);
            this.pnlProvider.TabIndex = 301;
            // 
            // lbl_pnlProviderBottomBrd
            // 
            this.lbl_pnlProviderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderBottomBrd.Location = new System.Drawing.Point(1, 104);
            this.lbl_pnlProviderBottomBrd.Name = "lbl_pnlProviderBottomBrd";
            this.lbl_pnlProviderBottomBrd.Size = new System.Drawing.Size(292, 1);
            this.lbl_pnlProviderBottomBrd.TabIndex = 97;
            // 
            // pnlProviderBody
            // 
            this.pnlProviderBody.Controls.Add(this.trvCreditCard);
            this.pnlProviderBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProviderBody.Location = new System.Drawing.Point(1, 24);
            this.pnlProviderBody.Name = "pnlProviderBody";
            this.pnlProviderBody.Size = new System.Drawing.Size(292, 81);
            this.pnlProviderBody.TabIndex = 92;
            // 
            // trvCreditCard
            // 
            this.trvCreditCard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCreditCard.CheckBoxes = true;
            this.trvCreditCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCreditCard.ForeColor = System.Drawing.Color.Black;
            this.trvCreditCard.Location = new System.Drawing.Point(0, 0);
            this.trvCreditCard.Name = "trvCreditCard";
            this.trvCreditCard.ShowLines = false;
            this.trvCreditCard.ShowPlusMinus = false;
            this.trvCreditCard.ShowRootLines = false;
            this.trvCreditCard.Size = new System.Drawing.Size(292, 81);
            this.trvCreditCard.TabIndex = 19;
            // 
            // pnlProviderHeader
            // 
            this.pnlProviderHeader.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlProviderHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProviderHeader.Controls.Add(this.btnDeSelectCreditCard);
            this.pnlProviderHeader.Controls.Add(this.btnSelectCreditCard);
            this.pnlProviderHeader.Controls.Add(this.lbl_pnlProviderHeaderBottomBrd);
            this.pnlProviderHeader.Controls.Add(this.lblCreditCard);
            this.pnlProviderHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProviderHeader.Location = new System.Drawing.Point(1, 1);
            this.pnlProviderHeader.Name = "pnlProviderHeader";
            this.pnlProviderHeader.Size = new System.Drawing.Size(292, 23);
            this.pnlProviderHeader.TabIndex = 91;
            // 
            // btnDeSelectCreditCard
            // 
            this.btnDeSelectCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeSelectCreditCard.FlatAppearance.BorderSize = 0;
            this.btnDeSelectCreditCard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeSelectCreditCard.Image = ((System.Drawing.Image)(resources.GetObject("btnDeSelectCreditCard.Image")));
            this.btnDeSelectCreditCard.Location = new System.Drawing.Point(230, 0);
            this.btnDeSelectCreditCard.Name = "btnDeSelectCreditCard";
            this.btnDeSelectCreditCard.Size = new System.Drawing.Size(31, 22);
            this.btnDeSelectCreditCard.TabIndex = 101;
            this.btnDeSelectCreditCard.Tag = "Select";
            this.btnDeSelectCreditCard.UseVisualStyleBackColor = false;
            this.btnDeSelectCreditCard.Visible = false;
            this.btnDeSelectCreditCard.Click += new System.EventHandler(this.btnDeSelectCreditCard_Click);
            // 
            // btnSelectCreditCard
            // 
            this.btnSelectCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectCreditCard.FlatAppearance.BorderSize = 0;
            this.btnSelectCreditCard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectCreditCard.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectCreditCard.Image")));
            this.btnSelectCreditCard.Location = new System.Drawing.Point(261, 0);
            this.btnSelectCreditCard.Name = "btnSelectCreditCard";
            this.btnSelectCreditCard.Size = new System.Drawing.Size(31, 22);
            this.btnSelectCreditCard.TabIndex = 100;
            this.btnSelectCreditCard.Tag = "Select";
            this.btnSelectCreditCard.UseVisualStyleBackColor = false;
            this.btnSelectCreditCard.Click += new System.EventHandler(this.btnSelectCreditCard_Click);
            // 
            // lbl_pnlProviderHeaderBottomBrd
            // 
            this.lbl_pnlProviderHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderHeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlProviderHeaderBottomBrd.Name = "lbl_pnlProviderHeaderBottomBrd";
            this.lbl_pnlProviderHeaderBottomBrd.Size = new System.Drawing.Size(292, 1);
            this.lbl_pnlProviderHeaderBottomBrd.TabIndex = 97;
            // 
            // lblCreditCard
            // 
            this.lblCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCreditCard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCreditCard.Location = new System.Drawing.Point(0, 0);
            this.lblCreditCard.Name = "lblCreditCard";
            this.lblCreditCard.Size = new System.Drawing.Size(292, 23);
            this.lblCreditCard.TabIndex = 0;
            this.lblCreditCard.Text = "Credit Card Type";
            this.lblCreditCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlProviderLeftBrd
            // 
            this.lbl_pnlProviderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlProviderLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlProviderLeftBrd.Name = "lbl_pnlProviderLeftBrd";
            this.lbl_pnlProviderLeftBrd.Size = new System.Drawing.Size(1, 104);
            this.lbl_pnlProviderLeftBrd.TabIndex = 93;
            // 
            // lbl_pnlProviderRightBrd
            // 
            this.lbl_pnlProviderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlProviderRightBrd.Location = new System.Drawing.Point(293, 1);
            this.lbl_pnlProviderRightBrd.Name = "lbl_pnlProviderRightBrd";
            this.lbl_pnlProviderRightBrd.Size = new System.Drawing.Size(1, 104);
            this.lbl_pnlProviderRightBrd.TabIndex = 94;
            // 
            // lbl_pnlProviderTopBrd
            // 
            this.lbl_pnlProviderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlProviderTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlProviderTopBrd.Name = "lbl_pnlProviderTopBrd";
            this.lbl_pnlProviderTopBrd.Size = new System.Drawing.Size(294, 1);
            this.lbl_pnlProviderTopBrd.TabIndex = 96;
            // 
            // label17
            // 
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(525, 15);
            this.label17.TabIndex = 300;
            this.label17.Text = "Credit Card Information :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(93, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 14);
            this.label2.TabIndex = 298;
            this.label2.Text = "Credit Card Type :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Left;
            this.label34.Location = new System.Drawing.Point(0, 1);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1, 127);
            this.label34.TabIndex = 303;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Right;
            this.label41.Location = new System.Drawing.Point(526, 1);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1, 127);
            this.label41.TabIndex = 304;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(527, 1);
            this.label10.TabIndex = 305;
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.lblAddressDetails);
            this.pnl.Controls.Add(this.cmbPracState);
            this.pnl.Controls.Add(this.txtPracZip);
            this.pnl.Controls.Add(this.txtPracCity);
            this.pnl.Controls.Add(this.txtPracAddress2);
            this.pnl.Controls.Add(this.txtPracAddress1);
            this.pnl.Controls.Add(this.lblOIZip);
            this.pnl.Controls.Add(this.lblOIState);
            this.pnl.Controls.Add(this.lblOICity);
            this.pnl.Controls.Add(this.lblOIAddressLine2);
            this.pnl.Controls.Add(this.lblOIAddressLine1);
            this.pnl.Controls.Add(this.label33);
            this.pnl.Controls.Add(this.label40);
            this.pnl.Controls.Add(this.label52);
            this.pnl.Controls.Add(this.label64);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl.Location = new System.Drawing.Point(3, 3);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(527, 125);
            this.pnl.TabIndex = 324;
            this.pnl.Visible = false;
            // 
            // lblAddressDetails
            // 
            this.lblAddressDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAddressDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAddressDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddressDetails.Location = new System.Drawing.Point(1, 1);
            this.lblAddressDetails.Name = "lblAddressDetails";
            this.lblAddressDetails.Size = new System.Drawing.Size(525, 17);
            this.lblAddressDetails.TabIndex = 285;
            this.lblAddressDetails.Text = "Practice Address :";
            // 
            // cmbPracState
            // 
            this.cmbPracState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPracState.FormattingEnabled = true;
            this.cmbPracState.Location = new System.Drawing.Point(408, 72);
            this.cmbPracState.MaxLength = 20;
            this.cmbPracState.Name = "cmbPracState";
            this.cmbPracState.Size = new System.Drawing.Size(94, 22);
            this.cmbPracState.TabIndex = 18;
            // 
            // txtPracZip
            // 
            this.txtPracZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPracZip.ForeColor = System.Drawing.Color.Black;
            this.txtPracZip.Location = new System.Drawing.Point(206, 96);
            this.txtPracZip.MaxLength = 10;
            this.txtPracZip.Name = "txtPracZip";
            this.txtPracZip.Size = new System.Drawing.Size(74, 22);
            this.txtPracZip.TabIndex = 16;
            this.txtPracZip.Leave += new System.EventHandler(this.txtPracZip_Leave);
            this.txtPracZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPracZip_KeyPress);
            // 
            // txtPracCity
            // 
            this.txtPracCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPracCity.ForeColor = System.Drawing.Color.Black;
            this.txtPracCity.Location = new System.Drawing.Point(206, 70);
            this.txtPracCity.MaxLength = 50;
            this.txtPracCity.Name = "txtPracCity";
            this.txtPracCity.Size = new System.Drawing.Size(151, 22);
            this.txtPracCity.TabIndex = 17;
            // 
            // txtPracAddress2
            // 
            this.txtPracAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPracAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtPracAddress2.Location = new System.Drawing.Point(206, 44);
            this.txtPracAddress2.MaxLength = 50;
            this.txtPracAddress2.Name = "txtPracAddress2";
            this.txtPracAddress2.Size = new System.Drawing.Size(296, 22);
            this.txtPracAddress2.TabIndex = 15;
            // 
            // txtPracAddress1
            // 
            this.txtPracAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPracAddress1.ForeColor = System.Drawing.Color.Black;
            this.txtPracAddress1.Location = new System.Drawing.Point(206, 18);
            this.txtPracAddress1.MaxLength = 50;
            this.txtPracAddress1.Name = "txtPracAddress1";
            this.txtPracAddress1.Size = new System.Drawing.Size(297, 22);
            this.txtPracAddress1.TabIndex = 14;
            // 
            // lblOIZip
            // 
            this.lblOIZip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIZip.AutoEllipsis = true;
            this.lblOIZip.AutoSize = true;
            this.lblOIZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIZip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIZip.Location = new System.Drawing.Point(172, 100);
            this.lblOIZip.Name = "lblOIZip";
            this.lblOIZip.Size = new System.Drawing.Size(31, 14);
            this.lblOIZip.TabIndex = 281;
            this.lblOIZip.Text = "Zip :";
            // 
            // lblOIState
            // 
            this.lblOIState.AutoSize = true;
            this.lblOIState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIState.Location = new System.Drawing.Point(360, 76);
            this.lblOIState.Name = "lblOIState";
            this.lblOIState.Size = new System.Drawing.Size(45, 14);
            this.lblOIState.TabIndex = 278;
            this.lblOIState.Text = "State :";
            // 
            // lblOICity
            // 
            this.lblOICity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOICity.AutoEllipsis = true;
            this.lblOICity.AutoSize = true;
            this.lblOICity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOICity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOICity.Location = new System.Drawing.Point(168, 74);
            this.lblOICity.Name = "lblOICity";
            this.lblOICity.Size = new System.Drawing.Size(35, 14);
            this.lblOICity.TabIndex = 277;
            this.lblOICity.Text = "City :";
            // 
            // lblOIAddressLine2
            // 
            this.lblOIAddressLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIAddressLine2.AutoEllipsis = true;
            this.lblOIAddressLine2.AutoSize = true;
            this.lblOIAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIAddressLine2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIAddressLine2.Location = new System.Drawing.Point(108, 48);
            this.lblOIAddressLine2.Name = "lblOIAddressLine2";
            this.lblOIAddressLine2.Size = new System.Drawing.Size(95, 14);
            this.lblOIAddressLine2.TabIndex = 276;
            this.lblOIAddressLine2.Text = "Address Line 2 :";
            // 
            // lblOIAddressLine1
            // 
            this.lblOIAddressLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIAddressLine1.AutoEllipsis = true;
            this.lblOIAddressLine1.AutoSize = true;
            this.lblOIAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIAddressLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIAddressLine1.Location = new System.Drawing.Point(108, 22);
            this.lblOIAddressLine1.Name = "lblOIAddressLine1";
            this.lblOIAddressLine1.Size = new System.Drawing.Size(95, 14);
            this.lblOIAddressLine1.TabIndex = 275;
            this.lblOIAddressLine1.Text = "Address Line 1 :";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Left;
            this.label33.Location = new System.Drawing.Point(0, 1);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1, 123);
            this.label33.TabIndex = 287;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Right;
            this.label40.Location = new System.Drawing.Point(526, 1);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1, 123);
            this.label40.TabIndex = 288;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Top;
            this.label52.Location = new System.Drawing.Point(0, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(527, 1);
            this.label52.TabIndex = 307;
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label64.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label64.Location = new System.Drawing.Point(0, 124);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(527, 1);
            this.label64.TabIndex = 308;
            // 
            // imgtabControl1
            // 
            this.imgtabControl1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgtabControl1.ImageStream")));
            this.imgtabControl1.TransparentColor = System.Drawing.Color.Transparent;
            this.imgtabControl1.Images.SetKeyName(0, "Filter Criteria.ico");
            this.imgtabControl1.Images.SetKeyName(1, "Display.ico");
            // 
            // frmSetupPatientStatementCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(547, 743);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Panel20);
            this.Controls.Add(this.pnl_tlsp_Top);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupPatientStatementCriteria";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Statement Settings";
            this.Load += new System.EventHandler(this.frmSetupPatientStatementCriteria_Load);
            this.pnl_tlsp_Top.ResumeLayout(false);
            this.pnl_tlsp_Top.PerformLayout();
            this.tstrip.ResumeLayout(false);
            this.tstrip.PerformLayout();
            this.Panel20.ResumeLayout(false);
            this.Panel21.ResumeLayout(false);
            this.Panel21.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbpFilter.ResumeLayout(false);
            this.tbpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmWaitFordays)).EndInit();
            this.tbpDisplay.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProviderBody.ResumeLayout(false);
            this.pnlProviderHeader.ResumeLayout(false);
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlsp_Top;
        internal gloGlobal.gloToolStripIgnoreFocus tstrip;
        internal System.Windows.Forms.ToolStripButton btnOK;
        internal System.Windows.Forms.ToolStripButton btnClose;
        internal System.Windows.Forms.ToolStripButton tlsp_btnSave;
        internal System.Windows.Forms.Panel Panel20;
        internal System.Windows.Forms.Panel Panel21;
        internal System.Windows.Forms.Label Label23;
        private System.Windows.Forms.Label Label44;
        private System.Windows.Forms.Label Label45;
        private System.Windows.Forms.Label Label46;
        private System.Windows.Forms.Label Label47;
        private System.Windows.Forms.TextBox txtStatementCriteriaName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        internal System.Windows.Forms.TabPage tbpFilter;
        private System.Windows.Forms.TabPage tbpDisplay;
        private System.Windows.Forms.TextBox txtDueAmount;
        private System.Windows.Forms.RadioButton rbGreaterthen;
        private System.Windows.Forms.RadioButton rbLessThen;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtZipCode;
        internal System.Windows.Forms.ComboBox cmbFacility;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.ComboBox cmbPaymentTray;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.ComboBox cmbChargesTray;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.ComboBox cmbInsurance;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button btnClearInsurance;
        internal System.Windows.Forms.Button btnBrowseInsurance;
        private System.Windows.Forms.ImageList imgtabControl1;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.ComboBox cmbPracState;
        private System.Windows.Forms.TextBox txtPracZip;
        private System.Windows.Forms.TextBox txtPracCity;
        private System.Windows.Forms.TextBox txtPracAddress2;
        private System.Windows.Forms.TextBox txtPracAddress1;
        private System.Windows.Forms.Label lblOIZip;
        private System.Windows.Forms.Label lblOIState;
        private System.Windows.Forms.Label lblOICity;
        private System.Windows.Forms.Label lblOIAddressLine2;
        private System.Windows.Forms.Label lblOIAddressLine1;
        private System.Windows.Forms.Label lblAddressDetails;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.DateTimePicker dtpOfficeEndTime;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.DateTimePicker dtpOfficeStartTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBillingContactName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtClinicMessage2;
        internal System.Windows.Forms.Label label30;
        internal System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtClinicMessage1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ComboBox cmbRemitState;
        private System.Windows.Forms.TextBox txtRemitZip;
        private System.Windows.Forms.TextBox txtRemitCity;
        private System.Windows.Forms.TextBox txtRemitAddress2;
        private System.Windows.Forms.TextBox txtRemitAddress1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPracticeTaxID;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox chkGuarantorIndicator;
        private System.Windows.Forms.CheckBox chkPendingInsurance;
        private System.Windows.Forms.Label lblCPT;
        private System.Windows.Forms.ComboBox cmbCPT;
        internal System.Windows.Forms.Button btnClearCPT;
        internal System.Windows.Forms.Button btnBrowseCPT;
        private gloMaskControl.gloMaskBox txtBillingContactPhone;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Label lbl_pnlProviderBottomBrd;
        private System.Windows.Forms.Panel pnlProviderBody;
        private System.Windows.Forms.TreeView trvCreditCard;
        private System.Windows.Forms.Panel pnlProviderHeader;
        private System.Windows.Forms.Button btnDeSelectCreditCard;
        private System.Windows.Forms.Button btnSelectCreditCard;
        private System.Windows.Forms.Label lbl_pnlProviderHeaderBottomBrd;
        private System.Windows.Forms.Label lblCreditCard;
        private System.Windows.Forms.Label lbl_pnlProviderLeftBrd;
        private System.Windows.Forms.Label lbl_pnlProviderRightBrd;
        private System.Windows.Forms.Label lbl_pnlProviderTopBrd;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.CheckBox chkDefault;
        private System.Windows.Forms.TextBox txtWaitFordays;
        internal System.Windows.Forms.Label label58;
        internal System.Windows.Forms.Label label57;
        private System.Windows.Forms.ComboBox cmbNameTo;
        internal System.Windows.Forms.Label label59;
        internal System.Windows.Forms.Label label60;
        private System.Windows.Forms.ComboBox cmbNameFrom;
        private System.Windows.Forms.NumericUpDown nmWaitFordays;
        internal System.Windows.Forms.Label label62;
        internal System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox txtRemitName;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label10;
    }
}