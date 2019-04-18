namespace gloPatient
{
    partial class gloPatientCopyAccountControl
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
                try
                {
                    if (oToolTip1 != null)
                    {
                        oToolTip1.Dispose();
                        oToolTip1 = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (oPatientGuarantors != null)
                    {
                        oPatientGuarantors.Dispose();
                        oPatientGuarantors = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (PatientGuardianDetails != null)
                    {
                        PatientGuardianDetails.Dispose();
                        PatientGuardianDetails = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (oPatientDemographicsDetails != null)
                    {
                        oPatientDemographicsDetails.Dispose();
                        oPatientDemographicsDetails = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (objgloAccount != null)
                    {
                        objgloAccount.Dispose();
                        objgloAccount = null;
                    }
                }
                catch
                {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloPatientCopyAccountControl));
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlGIHeader = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblGIHeader = new System.Windows.Forms.Label();
            this.btnAddPatient = new System.Windows.Forms.Button();
            this.btnPatientDeactivate = new System.Windows.Forms.Button();
            this.pnlAccountInfo = new System.Windows.Forms.Panel();
            this.pnl_Bottom = new System.Windows.Forms.Panel();
            this.gvAccountPatient = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRemovePatient = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlGIAddressDetails = new System.Windows.Forms.Panel();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.txtAccCounty = new System.Windows.Forms.TextBox();
            this.txtAccZip = new System.Windows.Forms.TextBox();
            this.txtAccCity = new System.Windows.Forms.TextBox();
            this.txtAccAddressLine2 = new System.Windows.Forms.TextBox();
            this.txtAccAddressLine1 = new System.Windows.Forms.TextBox();
            this.lblGIAddressDetails = new System.Windows.Forms.Label();
            this.lblGIZip = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblGIState = new System.Windows.Forms.Label();
            this.lblGICity = new System.Windows.Forms.Label();
            this.lblGIAddressLine2 = new System.Windows.Forms.Label();
            this.lblGIAddressLine1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlAddresssControl = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlGIContactDetails = new System.Windows.Forms.Panel();
            this.txtAccGuarantor = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSameAsGuardian = new System.Windows.Forms.ComboBox();
            this.lblSameAsGuardian = new System.Windows.Forms.Label();
            this.chkExcludefromStatement = new System.Windows.Forms.CheckBox();
            this.chkSetToCollection = new System.Windows.Forms.CheckBox();
            this.lblAccountDescription = new System.Windows.Forms.Label();
            this.txtAccountDescription = new System.Windows.Forms.TextBox();
            this.lblGuarantor = new System.Windows.Forms.Label();
            this.btnGuarantorExistingPatientBrowse = new System.Windows.Forms.Button();
            this.btnNewGuarantor = new System.Windows.Forms.Button();
            this.pnlBusinessCenter = new System.Windows.Forms.Panel();
            this.cmbBusinessCenter = new System.Windows.Forms.ComboBox();
            this.lblBusinessCentr = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.chkAccountActive = new System.Windows.Forms.CheckBox();
            this.label39 = new System.Windows.Forms.Label();
            this.lblAccountNo = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.pnlGIHeader.SuspendLayout();
            this.pnlAccountInfo.SuspendLayout();
            this.pnl_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvAccountPatient)).BeginInit();
            this.panel5.SuspendLayout();
            this.pnlGIAddressDetails.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlGIContactDetails.SuspendLayout();
            this.pnlBusinessCenter.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.Location = new System.Drawing.Point(240, 205);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(178, 97);
            this.pnlInternalControl.TabIndex = 110;
            this.pnlInternalControl.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlGIHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(700, 30);
            this.panel1.TabIndex = 26;
            // 
            // pnlGIHeader
            // 
            this.pnlGIHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlGIHeader.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.pnlGIHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGIHeader.Controls.Add(this.label9);
            this.pnlGIHeader.Controls.Add(this.label10);
            this.pnlGIHeader.Controls.Add(this.label11);
            this.pnlGIHeader.Controls.Add(this.label12);
            this.pnlGIHeader.Controls.Add(this.lblGIHeader);
            this.pnlGIHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGIHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGIHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlGIHeader.Name = "pnlGIHeader";
            this.pnlGIHeader.Size = new System.Drawing.Size(694, 24);
            this.pnlGIHeader.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(1, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(692, 1);
            this.label9.TabIndex = 8;
            this.label9.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 23);
            this.label10.TabIndex = 7;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(693, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 23);
            this.label11.TabIndex = 6;
            this.label11.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(694, 1);
            this.label12.TabIndex = 5;
            this.label12.Text = "label1";
            // 
            // lblGIHeader
            // 
            this.lblGIHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGIHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIHeader.ForeColor = System.Drawing.Color.White;
            this.lblGIHeader.Location = new System.Drawing.Point(0, 0);
            this.lblGIHeader.Name = "lblGIHeader";
            this.lblGIHeader.Size = new System.Drawing.Size(694, 24);
            this.lblGIHeader.TabIndex = 0;
            this.lblGIHeader.Text = "Account Information";
            this.lblGIHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAddPatient
            // 
            this.btnAddPatient.AutoEllipsis = true;
            this.btnAddPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnAddPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddPatient.BackgroundImage")));
            this.btnAddPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAddPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPatient.Location = new System.Drawing.Point(368, 5);
            this.btnAddPatient.Name = "btnAddPatient";
            this.btnAddPatient.Size = new System.Drawing.Size(84, 24);
            this.btnAddPatient.TabIndex = 75;
            this.btnAddPatient.Text = "Add Patient";
            this.btnAddPatient.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAddPatient.UseVisualStyleBackColor = false;
            this.btnAddPatient.Click += new System.EventHandler(this.btnAddPatient_Click);
            // 
            // btnPatientDeactivate
            // 
            this.btnPatientDeactivate.AutoEllipsis = true;
            this.btnPatientDeactivate.BackColor = System.Drawing.Color.Transparent;
            this.btnPatientDeactivate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPatientDeactivate.BackgroundImage")));
            this.btnPatientDeactivate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPatientDeactivate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPatientDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientDeactivate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientDeactivate.Location = new System.Drawing.Point(455, 5);
            this.btnPatientDeactivate.Name = "btnPatientDeactivate";
            this.btnPatientDeactivate.Size = new System.Drawing.Size(118, 24);
            this.btnPatientDeactivate.TabIndex = 95;
            this.btnPatientDeactivate.Text = "Deactivate Patient";
            this.btnPatientDeactivate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPatientDeactivate.UseVisualStyleBackColor = false;
            this.btnPatientDeactivate.Click += new System.EventHandler(this.btnPatientDeactivate_Click);
            // 
            // pnlAccountInfo
            // 
            this.pnlAccountInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlAccountInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAccountInfo.Controls.Add(this.pnl_Bottom);
            this.pnlAccountInfo.Controls.Add(this.pnlGIAddressDetails);
            this.pnlAccountInfo.Controls.Add(this.panel2);
            this.pnlAccountInfo.Controls.Add(this.panel1);
            this.pnlAccountInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAccountInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAccountInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlAccountInfo.Name = "pnlAccountInfo";
            this.pnlAccountInfo.Size = new System.Drawing.Size(700, 624);
            this.pnlAccountInfo.TabIndex = 13;
            // 
            // pnl_Bottom
            // 
            this.pnl_Bottom.Controls.Add(this.gvAccountPatient);
            this.pnl_Bottom.Controls.Add(this.panel5);
            this.pnl_Bottom.Controls.Add(this.label20);
            this.pnl_Bottom.Controls.Add(this.label21);
            this.pnl_Bottom.Controls.Add(this.label6);
            this.pnl_Bottom.Controls.Add(this.label5);
            this.pnl_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Bottom.Location = new System.Drawing.Point(0, 384);
            this.pnl_Bottom.Name = "pnl_Bottom";
            this.pnl_Bottom.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnl_Bottom.Size = new System.Drawing.Size(700, 240);
            this.pnl_Bottom.TabIndex = 113;
            // 
            // gvAccountPatient
            // 
            this.gvAccountPatient.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.gvAccountPatient.AllowEditing = false;
            this.gvAccountPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.gvAccountPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gvAccountPatient.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.gvAccountPatient.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.gvAccountPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvAccountPatient.ExtendLastCol = true;
            this.gvAccountPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvAccountPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gvAccountPatient.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.gvAccountPatient.Location = new System.Drawing.Point(4, 37);
            this.gvAccountPatient.Name = "gvAccountPatient";
            this.gvAccountPatient.Rows.Count = 1;
            this.gvAccountPatient.Rows.DefaultSize = 19;
            this.gvAccountPatient.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gvAccountPatient.Size = new System.Drawing.Size(692, 199);
            this.gvAccountPatient.StyleInfo = resources.GetString("gvAccountPatient.StyleInfo");
            this.gvAccountPatient.TabIndex = 96;
            this.gvAccountPatient.Click += new System.EventHandler(this.gvPatient_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.btnRemovePatient);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.btnAddPatient);
            this.panel5.Controls.Add(this.btnPatientDeactivate);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(4, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(692, 36);
            this.panel5.TabIndex = 100;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(0, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(692, 1);
            this.label8.TabIndex = 99;
            this.label8.Text = "label2";
            // 
            // btnRemovePatient
            // 
            this.btnRemovePatient.AutoEllipsis = true;
            this.btnRemovePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnRemovePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemovePatient.BackgroundImage")));
            this.btnRemovePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemovePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRemovePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemovePatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemovePatient.Location = new System.Drawing.Point(576, 5);
            this.btnRemovePatient.Name = "btnRemovePatient";
            this.btnRemovePatient.Size = new System.Drawing.Size(111, 24);
            this.btnRemovePatient.TabIndex = 97;
            this.btnRemovePatient.Text = "Remove Patient";
            this.btnRemovePatient.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRemovePatient.UseVisualStyleBackColor = false;
            this.btnRemovePatient.Click += new System.EventHandler(this.btnRemovePatient_Click);
            // 
            // label14
            // 
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(5, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(195, 22);
            this.label14.TabIndex = 98;
            this.label14.Text = "Account Patients :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 236);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(692, 1);
            this.label20.TabIndex = 27;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(692, 1);
            this.label21.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(696, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 237);
            this.label6.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 237);
            this.label5.TabIndex = 25;
            // 
            // pnlGIAddressDetails
            // 
            this.pnlGIAddressDetails.Controls.Add(this.cmbCountry);
            this.pnlGIAddressDetails.Controls.Add(this.label49);
            this.pnlGIAddressDetails.Controls.Add(this.cmbState);
            this.pnlGIAddressDetails.Controls.Add(this.txtAccCounty);
            this.pnlGIAddressDetails.Controls.Add(this.txtAccZip);
            this.pnlGIAddressDetails.Controls.Add(this.txtAccCity);
            this.pnlGIAddressDetails.Controls.Add(this.txtAccAddressLine2);
            this.pnlGIAddressDetails.Controls.Add(this.txtAccAddressLine1);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIAddressDetails);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIZip);
            this.pnlGIAddressDetails.Controls.Add(this.label22);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIState);
            this.pnlGIAddressDetails.Controls.Add(this.lblGICity);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIAddressLine2);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIAddressLine1);
            this.pnlGIAddressDetails.Controls.Add(this.label1);
            this.pnlGIAddressDetails.Controls.Add(this.label2);
            this.pnlGIAddressDetails.Controls.Add(this.label3);
            this.pnlGIAddressDetails.Controls.Add(this.label4);
            this.pnlGIAddressDetails.Controls.Add(this.pnlAddresssControl);
            this.pnlGIAddressDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGIAddressDetails.Location = new System.Drawing.Point(0, 199);
            this.pnlGIAddressDetails.Name = "pnlGIAddressDetails";
            this.pnlGIAddressDetails.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlGIAddressDetails.Size = new System.Drawing.Size(700, 185);
            this.pnlGIAddressDetails.TabIndex = 112;
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Items.AddRange(new object[] {
            "US"});
            this.cmbCountry.Location = new System.Drawing.Point(112, 153);
            this.cmbCountry.MaxDropDownItems = 3;
            this.cmbCountry.MaxLength = 20;
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(118, 22);
            this.cmbCountry.TabIndex = 22;
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
            this.label49.Location = new System.Drawing.Point(57, 158);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(58, 14);
            this.label49.TabIndex = 28;
            this.label49.Text = "Country :";
            this.label49.Visible = false;
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(294, 98);
            this.cmbState.MaxLength = 20;
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(98, 22);
            this.cmbState.TabIndex = 20;
            this.cmbState.Visible = false;
            // 
            // txtAccCounty
            // 
            this.txtAccCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccCounty.ForeColor = System.Drawing.Color.Black;
            this.txtAccCounty.Location = new System.Drawing.Point(294, 126);
            this.txtAccCounty.MaxLength = 20;
            this.txtAccCounty.Name = "txtAccCounty";
            this.txtAccCounty.Size = new System.Drawing.Size(98, 22);
            this.txtAccCounty.TabIndex = 21;
            this.txtAccCounty.Tag = "";
            this.txtAccCounty.Visible = false;
            // 
            // txtAccZip
            // 
            this.txtAccZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccZip.ForeColor = System.Drawing.Color.Black;
            this.txtAccZip.Location = new System.Drawing.Point(112, 126);
            this.txtAccZip.MaxLength = 10;
            this.txtAccZip.Name = "txtAccZip";
            this.txtAccZip.Size = new System.Drawing.Size(118, 22);
            this.txtAccZip.TabIndex = 18;
            this.txtAccZip.Tag = "";
            this.txtAccZip.Visible = false;
            this.txtAccZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAccZip_KeyPress);
            this.txtAccZip.Leave += new System.EventHandler(this.txtAccZip_Leave);
            // 
            // txtAccCity
            // 
            this.txtAccCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccCity.ForeColor = System.Drawing.Color.Black;
            this.txtAccCity.Location = new System.Drawing.Point(112, 100);
            this.txtAccCity.Name = "txtAccCity";
            this.txtAccCity.Size = new System.Drawing.Size(118, 22);
            this.txtAccCity.TabIndex = 19;
            this.txtAccCity.Tag = "19";
            this.txtAccCity.Visible = false;
            // 
            // txtAccAddressLine2
            // 
            this.txtAccAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccAddressLine2.ForeColor = System.Drawing.Color.Black;
            this.txtAccAddressLine2.Location = new System.Drawing.Point(112, 73);
            this.txtAccAddressLine2.Name = "txtAccAddressLine2";
            this.txtAccAddressLine2.Size = new System.Drawing.Size(279, 22);
            this.txtAccAddressLine2.TabIndex = 17;
            this.txtAccAddressLine2.Visible = false;
            // 
            // txtAccAddressLine1
            // 
            this.txtAccAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccAddressLine1.ForeColor = System.Drawing.Color.Black;
            this.txtAccAddressLine1.Location = new System.Drawing.Point(112, 49);
            this.txtAccAddressLine1.Name = "txtAccAddressLine1";
            this.txtAccAddressLine1.Size = new System.Drawing.Size(280, 22);
            this.txtAccAddressLine1.TabIndex = 16;
            this.txtAccAddressLine1.Visible = false;
            // 
            // lblGIAddressDetails
            // 
            this.lblGIAddressDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGIAddressDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIAddressDetails.Location = new System.Drawing.Point(54, 9);
            this.lblGIAddressDetails.Name = "lblGIAddressDetails";
            this.lblGIAddressDetails.Size = new System.Drawing.Size(195, 22);
            this.lblGIAddressDetails.TabIndex = 0;
            this.lblGIAddressDetails.Text = " Address Details :";
            this.lblGIAddressDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGIZip
            // 
            this.lblGIZip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIZip.AutoEllipsis = true;
            this.lblGIZip.AutoSize = true;
            this.lblGIZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIZip.Location = new System.Drawing.Point(80, 130);
            this.lblGIZip.Name = "lblGIZip";
            this.lblGIZip.Size = new System.Drawing.Size(31, 14);
            this.lblGIZip.TabIndex = 6;
            this.lblGIZip.Text = "Zip :";
            this.lblGIZip.Visible = false;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoEllipsis = true;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(243, 130);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 14);
            this.label22.TabIndex = 5;
            this.label22.Text = "County :";
            this.label22.Visible = false;
            // 
            // lblGIState
            // 
            this.lblGIState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIState.AutoEllipsis = true;
            this.lblGIState.AutoSize = true;
            this.lblGIState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIState.Location = new System.Drawing.Point(252, 104);
            this.lblGIState.Name = "lblGIState";
            this.lblGIState.Size = new System.Drawing.Size(45, 14);
            this.lblGIState.TabIndex = 5;
            this.lblGIState.Text = "State :";
            this.lblGIState.Visible = false;
            // 
            // lblGICity
            // 
            this.lblGICity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGICity.AutoEllipsis = true;
            this.lblGICity.AutoSize = true;
            this.lblGICity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGICity.Location = new System.Drawing.Point(76, 103);
            this.lblGICity.Name = "lblGICity";
            this.lblGICity.Size = new System.Drawing.Size(35, 14);
            this.lblGICity.TabIndex = 10;
            this.lblGICity.Text = "City :";
            this.lblGICity.Visible = false;
            // 
            // lblGIAddressLine2
            // 
            this.lblGIAddressLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIAddressLine2.AutoEllipsis = true;
            this.lblGIAddressLine2.AutoSize = true;
            this.lblGIAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIAddressLine2.Location = new System.Drawing.Point(16, 77);
            this.lblGIAddressLine2.Name = "lblGIAddressLine2";
            this.lblGIAddressLine2.Size = new System.Drawing.Size(95, 14);
            this.lblGIAddressLine2.TabIndex = 9;
            this.lblGIAddressLine2.Text = "Address Line 2 :";
            this.lblGIAddressLine2.Visible = false;
            // 
            // lblGIAddressLine1
            // 
            this.lblGIAddressLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIAddressLine1.AutoEllipsis = true;
            this.lblGIAddressLine1.AutoSize = true;
            this.lblGIAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIAddressLine1.Location = new System.Drawing.Point(16, 53);
            this.lblGIAddressLine1.Name = "lblGIAddressLine1";
            this.lblGIAddressLine1.Size = new System.Drawing.Size(95, 14);
            this.lblGIAddressLine1.TabIndex = 8;
            this.lblGIAddressLine1.Text = "Address Line 1 :";
            this.lblGIAddressLine1.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(4, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(692, 1);
            this.label1.TabIndex = 25;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 180);
            this.label2.TabIndex = 24;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(696, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 180);
            this.label3.TabIndex = 23;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(694, 1);
            this.label4.TabIndex = 22;
            this.label4.Text = "label1";
            // 
            // pnlAddresssControl
            // 
            this.pnlAddresssControl.Location = new System.Drawing.Point(150, 34);
            this.pnlAddresssControl.Name = "pnlAddresssControl";
            this.pnlAddresssControl.Size = new System.Drawing.Size(325, 132);
            this.pnlAddresssControl.TabIndex = 108;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlGIContactDetails);
            this.panel2.Controls.Add(this.pnlBusinessCenter);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label23);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 30);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.panel2.Size = new System.Drawing.Size(700, 169);
            this.panel2.TabIndex = 114;
            // 
            // pnlGIContactDetails
            // 
            this.pnlGIContactDetails.Controls.Add(this.txtAccGuarantor);
            this.pnlGIContactDetails.Controls.Add(this.label7);
            this.pnlGIContactDetails.Controls.Add(this.cmbSameAsGuardian);
            this.pnlGIContactDetails.Controls.Add(this.lblSameAsGuardian);
            this.pnlGIContactDetails.Controls.Add(this.chkExcludefromStatement);
            this.pnlGIContactDetails.Controls.Add(this.chkSetToCollection);
            this.pnlGIContactDetails.Controls.Add(this.lblAccountDescription);
            this.pnlGIContactDetails.Controls.Add(this.txtAccountDescription);
            this.pnlGIContactDetails.Controls.Add(this.lblGuarantor);
            this.pnlGIContactDetails.Controls.Add(this.btnGuarantorExistingPatientBrowse);
            this.pnlGIContactDetails.Controls.Add(this.btnNewGuarantor);
            this.pnlGIContactDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGIContactDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGIContactDetails.Location = new System.Drawing.Point(4, 52);
            this.pnlGIContactDetails.Name = "pnlGIContactDetails";
            this.pnlGIContactDetails.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlGIContactDetails.Size = new System.Drawing.Size(692, 113);
            this.pnlGIContactDetails.TabIndex = 111;
            // 
            // txtAccGuarantor
            // 
            this.txtAccGuarantor.AcceptsReturn = true;
            this.txtAccGuarantor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccGuarantor.Location = new System.Drawing.Point(146, 33);
            this.txtAccGuarantor.Name = "txtAccGuarantor";
            this.txtAccGuarantor.ReadOnly = true;
            this.txtAccGuarantor.Size = new System.Drawing.Size(222, 22);
            this.txtAccGuarantor.TabIndex = 114;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(59, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 14);
            this.label7.TabIndex = 111;
            this.label7.Text = "*";
            // 
            // cmbSameAsGuardian
            // 
            this.cmbSameAsGuardian.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbSameAsGuardian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSameAsGuardian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSameAsGuardian.FormattingEnabled = true;
            this.cmbSameAsGuardian.Location = new System.Drawing.Point(146, 59);
            this.cmbSameAsGuardian.Name = "cmbSameAsGuardian";
            this.cmbSameAsGuardian.Size = new System.Drawing.Size(223, 22);
            this.cmbSameAsGuardian.TabIndex = 84;
            this.cmbSameAsGuardian.SelectedIndexChanged += new System.EventHandler(this.cmbSameAsGuardian_SelectedIndexChanged);
            // 
            // lblSameAsGuardian
            // 
            this.lblSameAsGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSameAsGuardian.AutoEllipsis = true;
            this.lblSameAsGuardian.AutoSize = true;
            this.lblSameAsGuardian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSameAsGuardian.Location = new System.Drawing.Point(79, 64);
            this.lblSameAsGuardian.Name = "lblSameAsGuardian";
            this.lblSameAsGuardian.Size = new System.Drawing.Size(64, 14);
            this.lblSameAsGuardian.TabIndex = 83;
            this.lblSameAsGuardian.Text = "Same as  :";
            // 
            // chkExcludefromStatement
            // 
            this.chkExcludefromStatement.AutoSize = true;
            this.chkExcludefromStatement.Location = new System.Drawing.Point(146, 90);
            this.chkExcludefromStatement.Name = "chkExcludefromStatement";
            this.chkExcludefromStatement.Size = new System.Drawing.Size(158, 18);
            this.chkExcludefromStatement.TabIndex = 81;
            this.chkExcludefromStatement.Text = "Exclude from statement";
            this.chkExcludefromStatement.UseVisualStyleBackColor = true;
            // 
            // chkSetToCollection
            // 
            this.chkSetToCollection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSetToCollection.AutoSize = true;
            this.chkSetToCollection.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSetToCollection.Location = new System.Drawing.Point(320, 90);
            this.chkSetToCollection.Name = "chkSetToCollection";
            this.chkSetToCollection.Size = new System.Drawing.Size(123, 18);
            this.chkSetToCollection.TabIndex = 82;
            this.chkSetToCollection.Text = "Sent to collection";
            this.chkSetToCollection.UseVisualStyleBackColor = true;
            // 
            // lblAccountDescription
            // 
            this.lblAccountDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountDescription.AutoEllipsis = true;
            this.lblAccountDescription.AutoSize = true;
            this.lblAccountDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountDescription.Location = new System.Drawing.Point(68, 10);
            this.lblAccountDescription.Name = "lblAccountDescription";
            this.lblAccountDescription.Size = new System.Drawing.Size(74, 14);
            this.lblAccountDescription.TabIndex = 72;
            this.lblAccountDescription.Text = "Acct. Desc :";
            // 
            // txtAccountDescription
            // 
            this.txtAccountDescription.AcceptsReturn = true;
            this.txtAccountDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccountDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountDescription.Location = new System.Drawing.Point(146, 6);
            this.txtAccountDescription.Name = "txtAccountDescription";
            this.txtAccountDescription.Size = new System.Drawing.Size(222, 22);
            this.txtAccountDescription.TabIndex = 73;
            // 
            // lblGuarantor
            // 
            this.lblGuarantor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGuarantor.AutoEllipsis = true;
            this.lblGuarantor.AutoSize = true;
            this.lblGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuarantor.Location = new System.Drawing.Point(71, 38);
            this.lblGuarantor.Name = "lblGuarantor";
            this.lblGuarantor.Size = new System.Drawing.Size(69, 14);
            this.lblGuarantor.TabIndex = 74;
            this.lblGuarantor.Text = "Guarantor :";
            // 
            // btnGuarantorExistingPatientBrowse
            // 
            this.btnGuarantorExistingPatientBrowse.AutoEllipsis = true;
            this.btnGuarantorExistingPatientBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnGuarantorExistingPatientBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuarantorExistingPatientBrowse.BackgroundImage")));
            this.btnGuarantorExistingPatientBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuarantorExistingPatientBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuarantorExistingPatientBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnGuarantorExistingPatientBrowse.Image")));
            this.btnGuarantorExistingPatientBrowse.Location = new System.Drawing.Point(373, 34);
            this.btnGuarantorExistingPatientBrowse.Name = "btnGuarantorExistingPatientBrowse";
            this.btnGuarantorExistingPatientBrowse.Size = new System.Drawing.Size(22, 21);
            this.btnGuarantorExistingPatientBrowse.TabIndex = 76;
            this.btnGuarantorExistingPatientBrowse.UseVisualStyleBackColor = false;
            this.btnGuarantorExistingPatientBrowse.Click += new System.EventHandler(this.btnGuarantorExistingPatientBrowse_Click);
            // 
            // btnNewGuarantor
            // 
            this.btnNewGuarantor.AutoEllipsis = true;
            this.btnNewGuarantor.BackColor = System.Drawing.Color.Transparent;
            this.btnNewGuarantor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNewGuarantor.BackgroundImage")));
            this.btnNewGuarantor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewGuarantor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewGuarantor.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGuarantor.Image")));
            this.btnNewGuarantor.Location = new System.Drawing.Point(401, 34);
            this.btnNewGuarantor.Name = "btnNewGuarantor";
            this.btnNewGuarantor.Size = new System.Drawing.Size(22, 21);
            this.btnNewGuarantor.TabIndex = 77;
            this.btnNewGuarantor.UseVisualStyleBackColor = false;
            this.btnNewGuarantor.Click += new System.EventHandler(this.btnNewGuarantor_Click);
            // 
            // pnlBusinessCenter
            // 
            this.pnlBusinessCenter.Controls.Add(this.cmbBusinessCenter);
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCentr);
            this.pnlBusinessCenter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusinessCenter.Location = new System.Drawing.Point(4, 28);
            this.pnlBusinessCenter.Name = "pnlBusinessCenter";
            this.pnlBusinessCenter.Size = new System.Drawing.Size(692, 24);
            this.pnlBusinessCenter.TabIndex = 120;
            // 
            // cmbBusinessCenter
            // 
            this.cmbBusinessCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbBusinessCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBusinessCenter.FormattingEnabled = true;
            this.cmbBusinessCenter.Location = new System.Drawing.Point(147, 2);
            this.cmbBusinessCenter.Name = "cmbBusinessCenter";
            this.cmbBusinessCenter.Size = new System.Drawing.Size(221, 22);
            this.cmbBusinessCenter.TabIndex = 72;
            // 
            // lblBusinessCentr
            // 
            this.lblBusinessCentr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBusinessCentr.AutoEllipsis = true;
            this.lblBusinessCentr.AutoSize = true;
            this.lblBusinessCentr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessCentr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBusinessCentr.Location = new System.Drawing.Point(42, 5);
            this.lblBusinessCentr.Name = "lblBusinessCentr";
            this.lblBusinessCentr.Size = new System.Drawing.Size(101, 14);
            this.lblBusinessCentr.TabIndex = 117;
            this.lblBusinessCentr.Text = "Business Center :";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtAccountNo);
            this.panel3.Controls.Add(this.chkAccountActive);
            this.panel3.Controls.Add(this.label39);
            this.panel3.Controls.Add(this.lblAccountNo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(692, 26);
            this.panel3.TabIndex = 119;
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountNo.Location = new System.Drawing.Point(147, 2);
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Size = new System.Drawing.Size(169, 22);
            this.txtAccountNo.TabIndex = 71;
            // 
            // chkAccountActive
            // 
            this.chkAccountActive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAccountActive.AutoSize = true;
            this.chkAccountActive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAccountActive.Location = new System.Drawing.Point(322, 4);
            this.chkAccountActive.Name = "chkAccountActive";
            this.chkAccountActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAccountActive.Size = new System.Drawing.Size(134, 18);
            this.chkAccountActive.TabIndex = 79;
            this.chkAccountActive.Text = "Is Account Active";
            this.chkAccountActive.UseVisualStyleBackColor = true;
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.AutoEllipsis = true;
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(80, 8);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(14, 14);
            this.label39.TabIndex = 111;
            this.label39.Text = "*";
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountNo.AutoEllipsis = true;
            this.lblAccountNo.AutoSize = true;
            this.lblAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountNo.Location = new System.Drawing.Point(90, 7);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.Size = new System.Drawing.Size(53, 14);
            this.lblAccountNo.TabIndex = 70;
            this.lblAccountNo.Text = "Acct.# :";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label19.Location = new System.Drawing.Point(4, 165);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(692, 1);
            this.label19.TabIndex = 30;
            this.label19.Text = "label2";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label23.Location = new System.Drawing.Point(696, 2);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 164);
            this.label23.TabIndex = 28;
            this.label23.Text = "label3";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(4, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(693, 1);
            this.label24.TabIndex = 27;
            this.label24.Text = "label1";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(3, 1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 165);
            this.label25.TabIndex = 29;
            this.label25.Text = "label4";
            // 
            // gloPatientCopyAccountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlAccountInfo);
            this.Controls.Add(this.pnlInternalControl);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloPatientCopyAccountControl";
            this.Size = new System.Drawing.Size(700, 624);
            this.Load += new System.EventHandler(this.gloPatientCopyAccountControl_Load);
            this.panel1.ResumeLayout(false);
            this.pnlGIHeader.ResumeLayout(false);
            this.pnlAccountInfo.ResumeLayout(false);
            this.pnl_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvAccountPatient)).EndInit();
            this.panel5.ResumeLayout(false);
            this.pnlGIAddressDetails.ResumeLayout(false);
            this.pnlGIAddressDetails.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlGIContactDetails.ResumeLayout(false);
            this.pnlGIContactDetails.PerformLayout();
            this.pnlBusinessCenter.ResumeLayout(false);
            this.pnlBusinessCenter.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlGIHeader;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblGIHeader;
        private System.Windows.Forms.Button btnAddPatient;
        private System.Windows.Forms.Button btnPatientDeactivate;
        private System.Windows.Forms.Panel pnlAccountInfo;
        private System.Windows.Forms.Panel pnlGIAddressDetails;
        internal System.Windows.Forms.Panel pnlAddresssControl;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.TextBox txtAccCounty;
        private System.Windows.Forms.TextBox txtAccZip;
        private System.Windows.Forms.TextBox txtAccCity;
        private System.Windows.Forms.TextBox txtAccAddressLine2;
        private System.Windows.Forms.TextBox txtAccAddressLine1;
        private System.Windows.Forms.Label lblGIAddressDetails;
        private System.Windows.Forms.Label lblGIZip;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblGIState;
        private System.Windows.Forms.Label lblGICity;
        private System.Windows.Forms.Label lblGIAddressLine2;
        private System.Windows.Forms.Label lblGIAddressLine1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnl_Bottom;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private C1.Win.C1FlexGrid.C1FlexGrid gvAccountPatient;
        private System.Windows.Forms.Button btnRemovePatient;
        private System.Windows.Forms.Panel pnlGIContactDetails;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.ComboBox cmbSameAsGuardian;
        private System.Windows.Forms.Label lblSameAsGuardian;
        private System.Windows.Forms.CheckBox chkExcludefromStatement;
        private System.Windows.Forms.CheckBox chkSetToCollection;
        private System.Windows.Forms.TextBox txtAccountNo;
        private System.Windows.Forms.Label lblAccountNo;
        private System.Windows.Forms.Label lblAccountDescription;
        private System.Windows.Forms.TextBox txtAccountDescription;
        private System.Windows.Forms.Label lblGuarantor;
        private System.Windows.Forms.Button btnGuarantorExistingPatientBrowse;
        private System.Windows.Forms.Button btnNewGuarantor;
        private System.Windows.Forms.CheckBox chkAccountActive;
        private System.Windows.Forms.TextBox txtAccGuarantor;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlBusinessCenter;
        private System.Windows.Forms.ComboBox cmbBusinessCenter;
        private System.Windows.Forms.Label lblBusinessCentr;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}
