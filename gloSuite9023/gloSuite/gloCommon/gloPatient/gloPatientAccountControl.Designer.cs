namespace gloPatient
{
    partial class gloPatientAccountControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloPatientAccountControl));
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
            this.gvPatient = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlGIAddressDetails = new System.Windows.Forms.Panel();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.txtAccZip = new System.Windows.Forms.TextBox();
            this.txtAccCity = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtAccCounty = new System.Windows.Forms.TextBox();
            this.txtAccAddressLine2 = new System.Windows.Forms.TextBox();
            this.txtAccAddressLine1 = new System.Windows.Forms.TextBox();
            this.lblGIAddressDetails = new System.Windows.Forms.Label();
            this.lblGIZip = new System.Windows.Forms.Label();
            this.lblGIState = new System.Windows.Forms.Label();
            this.lblGICity = new System.Windows.Forms.Label();
            this.lblGIAddressLine2 = new System.Windows.Forms.Label();
            this.lblGIAddressLine1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlAddresssControl = new System.Windows.Forms.Panel();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.pnlGIContactDetails = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtAccGuarantor = new System.Windows.Forms.TextBox();
            this.btnNewGuarantor = new System.Windows.Forms.Button();
            this.btnGuarantorClear = new System.Windows.Forms.Button();
            this.btnGuarantorExistingPatientBrowse = new System.Windows.Forms.Button();
            this.lblGuarantor = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAccountDescription = new System.Windows.Forms.TextBox();
            this.cmbSameAsGuardian = new System.Windows.Forms.ComboBox();
            this.lblAccountDescription = new System.Windows.Forms.Label();
            this.lblSameAsGuardian = new System.Windows.Forms.Label();
            this.chkSetToCollection = new System.Windows.Forms.CheckBox();
            this.chkExcludefromStatement = new System.Windows.Forms.CheckBox();
            this.pnlBusinessCenter = new System.Windows.Forms.Panel();
            this.cmbBusinessCenter = new System.Windows.Forms.ComboBox();
            this.lblBusinessCentr = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.chkAccountActive = new System.Windows.Forms.CheckBox();
            this.lblAccountNo = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.pnlGIHeader.SuspendLayout();
            this.pnlAccountInfo.SuspendLayout();
            this.pnl_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).BeginInit();
            this.panel5.SuspendLayout();
            this.pnlGIAddressDetails.SuspendLayout();
            this.pnlAddresssControl.SuspendLayout();
            this.pnlGIContactDetails.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlBusinessCenter.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(629, 30);
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
            this.pnlGIHeader.Size = new System.Drawing.Size(623, 24);
            this.pnlGIHeader.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(1, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(621, 1);
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
            this.label11.Location = new System.Drawing.Point(622, 1);
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
            this.label12.Size = new System.Drawing.Size(623, 1);
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
            this.lblGIHeader.Size = new System.Drawing.Size(623, 24);
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
            this.btnAddPatient.Location = new System.Drawing.Point(405, 6);
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
            this.btnPatientDeactivate.Location = new System.Drawing.Point(493, 6);
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
            this.pnlAccountInfo.Controls.Add(this.pnlGIContactDetails);
            this.pnlAccountInfo.Controls.Add(this.panel1);
            this.pnlAccountInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAccountInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlAccountInfo.Name = "pnlAccountInfo";
            this.pnlAccountInfo.Size = new System.Drawing.Size(629, 623);
            this.pnlAccountInfo.TabIndex = 13;
            // 
            // pnl_Bottom
            // 
            this.pnl_Bottom.Controls.Add(this.gvPatient);
            this.pnl_Bottom.Controls.Add(this.panel5);
            this.pnl_Bottom.Controls.Add(this.label20);
            this.pnl_Bottom.Controls.Add(this.label21);
            this.pnl_Bottom.Controls.Add(this.label6);
            this.pnl_Bottom.Controls.Add(this.label5);
            this.pnl_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Bottom.Location = new System.Drawing.Point(0, 385);
            this.pnl_Bottom.Name = "pnl_Bottom";
            this.pnl_Bottom.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnl_Bottom.Size = new System.Drawing.Size(629, 238);
            this.pnl_Bottom.TabIndex = 113;
            // 
            // gvPatient
            // 
            this.gvPatient.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.gvPatient.AllowEditing = false;
            this.gvPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.gvPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gvPatient.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.gvPatient.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.gvPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvPatient.ExtendLastCol = true;
            this.gvPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gvPatient.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.gvPatient.Location = new System.Drawing.Point(4, 36);
            this.gvPatient.Name = "gvPatient";
            this.gvPatient.Rows.Count = 1;
            this.gvPatient.Rows.DefaultSize = 19;
            this.gvPatient.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gvPatient.Size = new System.Drawing.Size(621, 198);
            this.gvPatient.StyleInfo = resources.GetString("gvPatient.StyleInfo");
            this.gvPatient.TabIndex = 96;
            this.gvPatient.Click += new System.EventHandler(this.gvPatient_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.btnPatientDeactivate);
            this.panel5.Controls.Add(this.btnAddPatient);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(4, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(621, 35);
            this.panel5.TabIndex = 98;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(0, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(621, 1);
            this.label13.TabIndex = 96;
            this.label13.Text = "label2";
            // 
            // label14
            // 
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(4, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(195, 22);
            this.label14.TabIndex = 0;
            this.label14.Text = "Account Patients :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 234);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(621, 1);
            this.label20.TabIndex = 27;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(621, 1);
            this.label21.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(625, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 235);
            this.label6.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 235);
            this.label5.TabIndex = 25;
            // 
            // pnlGIAddressDetails
            // 
            this.pnlGIAddressDetails.Controls.Add(this.cmbState);
            this.pnlGIAddressDetails.Controls.Add(this.txtAccZip);
            this.pnlGIAddressDetails.Controls.Add(this.txtAccCity);
            this.pnlGIAddressDetails.Controls.Add(this.label22);
            this.pnlGIAddressDetails.Controls.Add(this.txtAccCounty);
            this.pnlGIAddressDetails.Controls.Add(this.txtAccAddressLine2);
            this.pnlGIAddressDetails.Controls.Add(this.txtAccAddressLine1);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIAddressDetails);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIZip);
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
            this.pnlGIAddressDetails.Location = new System.Drawing.Point(0, 200);
            this.pnlGIAddressDetails.Name = "pnlGIAddressDetails";
            this.pnlGIAddressDetails.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlGIAddressDetails.Size = new System.Drawing.Size(629, 185);
            this.pnlGIAddressDetails.TabIndex = 112;
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(310, 96);
            this.cmbState.MaxLength = 20;
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(98, 21);
            this.cmbState.TabIndex = 20;
            this.cmbState.Visible = false;
            // 
            // txtAccZip
            // 
            this.txtAccZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccZip.ForeColor = System.Drawing.Color.Black;
            this.txtAccZip.Location = new System.Drawing.Point(128, 124);
            this.txtAccZip.MaxLength = 10;
            this.txtAccZip.Name = "txtAccZip";
            this.txtAccZip.Size = new System.Drawing.Size(104, 22);
            this.txtAccZip.TabIndex = 18;
            this.txtAccZip.Tag = "";
            this.txtAccZip.Visible = false;
            // 
            // txtAccCity
            // 
            this.txtAccCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccCity.ForeColor = System.Drawing.Color.Black;
            this.txtAccCity.Location = new System.Drawing.Point(128, 98);
            this.txtAccCity.Name = "txtAccCity";
            this.txtAccCity.Size = new System.Drawing.Size(103, 22);
            this.txtAccCity.TabIndex = 19;
            this.txtAccCity.Tag = "19";
            this.txtAccCity.Visible = false;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoEllipsis = true;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(73, 149);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 14);
            this.label22.TabIndex = 5;
            this.label22.Text = "County :";
            this.label22.Visible = false;
            // 
            // txtAccCounty
            // 
            this.txtAccCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccCounty.ForeColor = System.Drawing.Color.Black;
            this.txtAccCounty.Location = new System.Drawing.Point(128, 149);
            this.txtAccCounty.MaxLength = 20;
            this.txtAccCounty.Name = "txtAccCounty";
            this.txtAccCounty.Size = new System.Drawing.Size(112, 22);
            this.txtAccCounty.TabIndex = 21;
            this.txtAccCounty.Tag = "";
            this.txtAccCounty.Visible = false;
            // 
            // txtAccAddressLine2
            // 
            this.txtAccAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccAddressLine2.ForeColor = System.Drawing.Color.Black;
            this.txtAccAddressLine2.Location = new System.Drawing.Point(128, 71);
            this.txtAccAddressLine2.Name = "txtAccAddressLine2";
            this.txtAccAddressLine2.Size = new System.Drawing.Size(279, 22);
            this.txtAccAddressLine2.TabIndex = 17;
            this.txtAccAddressLine2.Visible = false;
            // 
            // txtAccAddressLine1
            // 
            this.txtAccAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccAddressLine1.ForeColor = System.Drawing.Color.Black;
            this.txtAccAddressLine1.Location = new System.Drawing.Point(128, 47);
            this.txtAccAddressLine1.Name = "txtAccAddressLine1";
            this.txtAccAddressLine1.Size = new System.Drawing.Size(280, 22);
            this.txtAccAddressLine1.TabIndex = 16;
            this.txtAccAddressLine1.Visible = false;
            // 
            // lblGIAddressDetails
            // 
            this.lblGIAddressDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGIAddressDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIAddressDetails.Location = new System.Drawing.Point(70, 7);
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
            this.lblGIZip.Location = new System.Drawing.Point(96, 128);
            this.lblGIZip.Name = "lblGIZip";
            this.lblGIZip.Size = new System.Drawing.Size(31, 14);
            this.lblGIZip.TabIndex = 6;
            this.lblGIZip.Text = "Zip :";
            this.lblGIZip.Visible = false;
            // 
            // lblGIState
            // 
            this.lblGIState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIState.AutoEllipsis = true;
            this.lblGIState.AutoSize = true;
            this.lblGIState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIState.Location = new System.Drawing.Point(268, 102);
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
            this.lblGICity.Location = new System.Drawing.Point(92, 101);
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
            this.lblGIAddressLine2.Location = new System.Drawing.Point(58, 75);
            this.lblGIAddressLine2.Name = "lblGIAddressLine2";
            this.lblGIAddressLine2.Size = new System.Drawing.Size(69, 14);
            this.lblGIAddressLine2.TabIndex = 9;
            this.lblGIAddressLine2.Text = "Address 2 :";
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
            this.lblGIAddressLine1.Location = new System.Drawing.Point(58, 51);
            this.lblGIAddressLine1.Name = "lblGIAddressLine1";
            this.lblGIAddressLine1.Size = new System.Drawing.Size(69, 14);
            this.lblGIAddressLine1.TabIndex = 8;
            this.lblGIAddressLine1.Text = "Address 1 :";
            this.lblGIAddressLine1.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(621, 1);
            this.label1.TabIndex = 25;
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
            this.label3.Location = new System.Drawing.Point(625, 2);
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
            this.label4.Size = new System.Drawing.Size(623, 1);
            this.label4.TabIndex = 22;
            this.label4.Text = "label1";
            // 
            // pnlAddresssControl
            // 
            this.pnlAddresssControl.Controls.Add(this.cmbCountry);
            this.pnlAddresssControl.Controls.Add(this.label49);
            this.pnlAddresssControl.Location = new System.Drawing.Point(166, 32);
            this.pnlAddresssControl.Name = "pnlAddresssControl";
            this.pnlAddresssControl.Size = new System.Drawing.Size(325, 132);
            this.pnlAddresssControl.TabIndex = 108;
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Items.AddRange(new object[] {
            "US"});
            this.cmbCountry.Location = new System.Drawing.Point(148, 87);
            this.cmbCountry.MaxDropDownItems = 3;
            this.cmbCountry.MaxLength = 20;
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(93, 21);
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
            this.label49.Location = new System.Drawing.Point(93, 92);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(54, 14);
            this.label49.TabIndex = 28;
            this.label49.Text = "Country:";
            this.label49.Visible = false;
            // 
            // pnlGIContactDetails
            // 
            this.pnlGIContactDetails.Controls.Add(this.panel3);
            this.pnlGIContactDetails.Controls.Add(this.pnlBusinessCenter);
            this.pnlGIContactDetails.Controls.Add(this.panel2);
            this.pnlGIContactDetails.Controls.Add(this.lbl_BottomBrd);
            this.pnlGIContactDetails.Controls.Add(this.lbl_LeftBrd);
            this.pnlGIContactDetails.Controls.Add(this.lbl_RightBrd);
            this.pnlGIContactDetails.Controls.Add(this.lbl_TopBrd);
            this.pnlGIContactDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGIContactDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGIContactDetails.Location = new System.Drawing.Point(0, 30);
            this.pnlGIContactDetails.Name = "pnlGIContactDetails";
            this.pnlGIContactDetails.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlGIContactDetails.Size = new System.Drawing.Size(629, 170);
            this.pnlGIContactDetails.TabIndex = 111;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtAccGuarantor);
            this.panel3.Controls.Add(this.btnNewGuarantor);
            this.panel3.Controls.Add(this.btnGuarantorClear);
            this.panel3.Controls.Add(this.btnGuarantorExistingPatientBrowse);
            this.panel3.Controls.Add(this.lblGuarantor);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtAccountDescription);
            this.panel3.Controls.Add(this.cmbSameAsGuardian);
            this.panel3.Controls.Add(this.lblAccountDescription);
            this.panel3.Controls.Add(this.lblSameAsGuardian);
            this.panel3.Controls.Add(this.chkSetToCollection);
            this.panel3.Controls.Add(this.chkExcludefromStatement);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 54);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(622, 112);
            this.panel3.TabIndex = 114;
            // 
            // txtAccGuarantor
            // 
            this.txtAccGuarantor.AcceptsReturn = true;
            this.txtAccGuarantor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccGuarantor.Location = new System.Drawing.Point(181, 33);
            this.txtAccGuarantor.Name = "txtAccGuarantor";
            this.txtAccGuarantor.ReadOnly = true;
            this.txtAccGuarantor.Size = new System.Drawing.Size(222, 22);
            this.txtAccGuarantor.TabIndex = 113;
            // 
            // btnNewGuarantor
            // 
            this.btnNewGuarantor.AutoEllipsis = true;
            this.btnNewGuarantor.BackColor = System.Drawing.Color.Transparent;
            this.btnNewGuarantor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNewGuarantor.BackgroundImage")));
            this.btnNewGuarantor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewGuarantor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewGuarantor.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGuarantor.Image")));
            this.btnNewGuarantor.Location = new System.Drawing.Point(436, 34);
            this.btnNewGuarantor.Name = "btnNewGuarantor";
            this.btnNewGuarantor.Size = new System.Drawing.Size(22, 21);
            this.btnNewGuarantor.TabIndex = 77;
            this.btnNewGuarantor.UseVisualStyleBackColor = false;
            this.btnNewGuarantor.Click += new System.EventHandler(this.btnNewGuarantor_Click);
            // 
            // btnGuarantorClear
            // 
            this.btnGuarantorClear.AutoEllipsis = true;
            this.btnGuarantorClear.BackColor = System.Drawing.Color.Transparent;
            this.btnGuarantorClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuarantorClear.BackgroundImage")));
            this.btnGuarantorClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuarantorClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuarantorClear.Image = ((System.Drawing.Image)(resources.GetObject("btnGuarantorClear.Image")));
            this.btnGuarantorClear.Location = new System.Drawing.Point(464, 33);
            this.btnGuarantorClear.Name = "btnGuarantorClear";
            this.btnGuarantorClear.Size = new System.Drawing.Size(22, 22);
            this.btnGuarantorClear.TabIndex = 114;
            this.btnGuarantorClear.UseVisualStyleBackColor = false;
            this.btnGuarantorClear.Click += new System.EventHandler(this.btnGuarantorClear_Click);
            // 
            // btnGuarantorExistingPatientBrowse
            // 
            this.btnGuarantorExistingPatientBrowse.AutoEllipsis = true;
            this.btnGuarantorExistingPatientBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnGuarantorExistingPatientBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuarantorExistingPatientBrowse.BackgroundImage")));
            this.btnGuarantorExistingPatientBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuarantorExistingPatientBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuarantorExistingPatientBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnGuarantorExistingPatientBrowse.Image")));
            this.btnGuarantorExistingPatientBrowse.Location = new System.Drawing.Point(409, 34);
            this.btnGuarantorExistingPatientBrowse.Name = "btnGuarantorExistingPatientBrowse";
            this.btnGuarantorExistingPatientBrowse.Size = new System.Drawing.Size(22, 21);
            this.btnGuarantorExistingPatientBrowse.TabIndex = 76;
            this.btnGuarantorExistingPatientBrowse.UseVisualStyleBackColor = false;
            this.btnGuarantorExistingPatientBrowse.Click += new System.EventHandler(this.btnGuarantorExistingPatientBrowse_Click);
            // 
            // lblGuarantor
            // 
            this.lblGuarantor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGuarantor.AutoEllipsis = true;
            this.lblGuarantor.AutoSize = true;
            this.lblGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuarantor.Location = new System.Drawing.Point(110, 35);
            this.lblGuarantor.Name = "lblGuarantor";
            this.lblGuarantor.Size = new System.Drawing.Size(67, 19);
            this.lblGuarantor.TabIndex = 74;
            this.lblGuarantor.Text = "Guarantor :";
            this.lblGuarantor.UseCompatibleTextRendering = true;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(99, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 14);
            this.label8.TabIndex = 111;
            this.label8.Text = "*";
            // 
            // txtAccountDescription
            // 
            this.txtAccountDescription.AcceptsReturn = true;
            this.txtAccountDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccountDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountDescription.Location = new System.Drawing.Point(182, 6);
            this.txtAccountDescription.Name = "txtAccountDescription";
            this.txtAccountDescription.Size = new System.Drawing.Size(222, 22);
            this.txtAccountDescription.TabIndex = 73;
            // 
            // cmbSameAsGuardian
            // 
            this.cmbSameAsGuardian.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbSameAsGuardian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSameAsGuardian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSameAsGuardian.FormattingEnabled = true;
            this.cmbSameAsGuardian.Location = new System.Drawing.Point(182, 59);
            this.cmbSameAsGuardian.Name = "cmbSameAsGuardian";
            this.cmbSameAsGuardian.Size = new System.Drawing.Size(223, 22);
            this.cmbSameAsGuardian.TabIndex = 84;
            this.cmbSameAsGuardian.SelectedIndexChanged += new System.EventHandler(this.cmbSameAsGuardian_SelectedIndexChanged);
            // 
            // lblAccountDescription
            // 
            this.lblAccountDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountDescription.AutoEllipsis = true;
            this.lblAccountDescription.AutoSize = true;
            this.lblAccountDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountDescription.Location = new System.Drawing.Point(103, 10);
            this.lblAccountDescription.Name = "lblAccountDescription";
            this.lblAccountDescription.Size = new System.Drawing.Size(74, 14);
            this.lblAccountDescription.TabIndex = 72;
            this.lblAccountDescription.Text = "Acct. Desc :";
            // 
            // lblSameAsGuardian
            // 
            this.lblSameAsGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSameAsGuardian.AutoEllipsis = true;
            this.lblSameAsGuardian.AutoSize = true;
            this.lblSameAsGuardian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSameAsGuardian.Location = new System.Drawing.Point(113, 63);
            this.lblSameAsGuardian.Name = "lblSameAsGuardian";
            this.lblSameAsGuardian.Size = new System.Drawing.Size(64, 14);
            this.lblSameAsGuardian.TabIndex = 83;
            this.lblSameAsGuardian.Text = "Same as  :";
            // 
            // chkSetToCollection
            // 
            this.chkSetToCollection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSetToCollection.AutoSize = true;
            this.chkSetToCollection.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSetToCollection.Location = new System.Drawing.Point(350, 89);
            this.chkSetToCollection.Name = "chkSetToCollection";
            this.chkSetToCollection.Size = new System.Drawing.Size(123, 18);
            this.chkSetToCollection.TabIndex = 82;
            this.chkSetToCollection.Text = "Sent to collection";
            this.chkSetToCollection.UseVisualStyleBackColor = true;
            // 
            // chkExcludefromStatement
            // 
            this.chkExcludefromStatement.AutoSize = true;
            this.chkExcludefromStatement.Location = new System.Drawing.Point(182, 89);
            this.chkExcludefromStatement.Name = "chkExcludefromStatement";
            this.chkExcludefromStatement.Size = new System.Drawing.Size(158, 18);
            this.chkExcludefromStatement.TabIndex = 81;
            this.chkExcludefromStatement.Text = "Exclude from statement";
            this.chkExcludefromStatement.UseVisualStyleBackColor = true;
            // 
            // pnlBusinessCenter
            // 
            this.pnlBusinessCenter.Controls.Add(this.cmbBusinessCenter);
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCentr);
            this.pnlBusinessCenter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusinessCenter.Location = new System.Drawing.Point(3, 30);
            this.pnlBusinessCenter.Name = "pnlBusinessCenter";
            this.pnlBusinessCenter.Size = new System.Drawing.Size(622, 24);
            this.pnlBusinessCenter.TabIndex = 119;
            // 
            // cmbBusinessCenter
            // 
            this.cmbBusinessCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbBusinessCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBusinessCenter.FormattingEnabled = true;
            this.cmbBusinessCenter.Location = new System.Drawing.Point(182, 1);
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
            this.lblBusinessCentr.Location = new System.Drawing.Point(77, 5);
            this.lblBusinessCentr.Name = "lblBusinessCentr";
            this.lblBusinessCentr.Size = new System.Drawing.Size(101, 14);
            this.lblBusinessCentr.TabIndex = 115;
            this.lblBusinessCentr.Text = "Business Center :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtAccountNo);
            this.panel2.Controls.Add(this.chkAccountActive);
            this.panel2.Controls.Add(this.lblAccountNo);
            this.panel2.Controls.Add(this.label39);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(622, 28);
            this.panel2.TabIndex = 118;
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountNo.Location = new System.Drawing.Point(181, 3);
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.ReadOnly = true;
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
            this.chkAccountActive.Location = new System.Drawing.Point(358, 5);
            this.chkAccountActive.Name = "chkAccountActive";
            this.chkAccountActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAccountActive.Size = new System.Drawing.Size(134, 18);
            this.chkAccountActive.TabIndex = 79;
            this.chkAccountActive.Text = "Is Account Active";
            this.chkAccountActive.UseVisualStyleBackColor = true;
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountNo.AutoEllipsis = true;
            this.lblAccountNo.AutoSize = true;
            this.lblAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountNo.Location = new System.Drawing.Point(126, 8);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.Size = new System.Drawing.Size(53, 14);
            this.lblAccountNo.TabIndex = 70;
            this.lblAccountNo.Text = "Acct.# :";
            this.lblAccountNo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.label39.Location = new System.Drawing.Point(113, 8);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(14, 14);
            this.label39.TabIndex = 111;
            this.label39.Text = "*";
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(3, 166);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(622, 1);
            this.lbl_BottomBrd.TabIndex = 30;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 139);
            this.lbl_LeftBrd.TabIndex = 29;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(625, 2);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 165);
            this.lbl_RightBrd.TabIndex = 28;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(623, 1);
            this.lbl_TopBrd.TabIndex = 27;
            this.lbl_TopBrd.Text = "label1";
            // 
            // gloPatientAccountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlAccountInfo);
            this.Controls.Add(this.pnlInternalControl);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloPatientAccountControl";
            this.Size = new System.Drawing.Size(629, 623);
            this.Load += new System.EventHandler(this.gloPatientAccountControl_Load);
            this.panel1.ResumeLayout(false);
            this.pnlGIHeader.ResumeLayout(false);
            this.pnlAccountInfo.ResumeLayout(false);
            this.pnl_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).EndInit();
            this.panel5.ResumeLayout(false);
            this.pnlGIAddressDetails.ResumeLayout(false);
            this.pnlGIAddressDetails.PerformLayout();
            this.pnlAddresssControl.ResumeLayout(false);
            this.pnlAddresssControl.PerformLayout();
            this.pnlGIContactDetails.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlBusinessCenter.ResumeLayout(false);
            this.pnlBusinessCenter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Panel pnlGIContactDetails;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        public System.Windows.Forms.TextBox txtAccountNo;
        private System.Windows.Forms.Label lblAccountNo;
        private System.Windows.Forms.Label lblAccountDescription;
        private System.Windows.Forms.TextBox txtAccountDescription;
        private System.Windows.Forms.Label lblGuarantor;
        private System.Windows.Forms.Button btnGuarantorExistingPatientBrowse;
        private System.Windows.Forms.Button btnNewGuarantor;
        private System.Windows.Forms.CheckBox chkAccountActive;
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
        private System.Windows.Forms.ComboBox cmbSameAsGuardian;
        private System.Windows.Forms.Label lblSameAsGuardian;
        private System.Windows.Forms.CheckBox chkExcludefromStatement;
        private System.Windows.Forms.CheckBox chkSetToCollection;
        private System.Windows.Forms.Panel pnl_Bottom;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private C1.Win.C1FlexGrid.C1FlexGrid gvPatient;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAccGuarantor;
        private System.Windows.Forms.Button btnGuarantorClear;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlBusinessCenter;
        private System.Windows.Forms.ComboBox cmbBusinessCenter;
        private System.Windows.Forms.Label lblBusinessCentr;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}
