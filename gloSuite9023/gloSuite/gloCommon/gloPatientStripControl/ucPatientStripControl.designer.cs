namespace gloPatientStripControl
{
    partial class ucPatientStripControl
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
                    if (dtpDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDate);
                        }
                        catch
                        {
                        }
                        dtpDate.Dispose();
                        dtpDate = null;
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
                if (toolTip1 != null)
                {
                    toolTip1.Dispose();
                    toolTip1 = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPatientStripControl));
            this.c1PatientDetails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label27 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearchPatientClaim = new System.Windows.Forms.Button();
            this.txtPatientSearch = new System.Windows.Forms.TextBox();
            this.btn_AddInsurancePlan = new System.Windows.Forms.Button();
            this.lblSearchonClaimNo = new System.Windows.Forms.Label();
            this.chk_ClaimNoSearch = new System.Windows.Forms.CheckBox();
            this.label52 = new System.Windows.Forms.Label();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btn_ModityPatient = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlAlerts = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.pnl_Main = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlSocialSecurity = new System.Windows.Forms.Panel();
            this.lblSocialSecurity = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlReferralPhysician = new System.Windows.Forms.Panel();
            this.lblReferralPhysician = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlCellPhone = new System.Windows.Forms.Panel();
            this.lblCellPhone = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlHandDominance = new System.Windows.Forms.Panel();
            this.lblHandDominance = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlPharmacyFax = new System.Windows.Forms.Panel();
            this.lblPharmacyFax = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.pnlPharmacyPhone = new System.Windows.Forms.Panel();
            this.lblPharmacyPhone = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.lblTodaysDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.pnlAge = new System.Windows.Forms.Panel();
            this.lblAge = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pnlDOB = new System.Windows.Forms.Panel();
            this.lblDOB = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlSecInsurance = new System.Windows.Forms.Panel();
            this.lblSecInsurance = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlPrInsurance = new System.Windows.Forms.Panel();
            this.lblPrInsurance = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlOccupation = new System.Windows.Forms.Panel();
            this.lblOccupation = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlGender = new System.Windows.Forms.Panel();
            this.lblGender = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlPharmacyName = new System.Windows.Forms.Panel();
            this.lblPharmacyName = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pnlPatientPhone = new System.Windows.Forms.Panel();
            this.lblPhone = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.lblProvider = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlName = new System.Windows.Forms.Panel();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlCode = new System.Windows.Forms.Panel();
            this.lblPatientCode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.pnlTotalBalance = new System.Windows.Forms.Panel();
            this.label49 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lblBudget = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblCollection = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lblPendingOtherReserved = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lblPendingAdvance = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.lblPendingCopay = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblPatientPending = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lblInsurancePending = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblTotalCharges = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblTotalBalance = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.pnlInsurace = new System.Windows.Forms.Panel();
            this.label53 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.c1Insurance = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlClaimSearch = new System.Windows.Forms.Panel();
            this.label56 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.c1SplitClaims = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlAlerts.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.pnl_Main.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlSocialSecurity.SuspendLayout();
            this.pnlReferralPhysician.SuspendLayout();
            this.pnlCellPhone.SuspendLayout();
            this.pnlHandDominance.SuspendLayout();
            this.pnlPharmacyFax.SuspendLayout();
            this.pnlPharmacyPhone.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.pnlAge.SuspendLayout();
            this.pnlDOB.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlSecInsurance.SuspendLayout();
            this.pnlPrInsurance.SuspendLayout();
            this.pnlOccupation.SuspendLayout();
            this.pnlGender.SuspendLayout();
            this.pnlPharmacyName.SuspendLayout();
            this.pnlPatientPhone.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlName.SuspendLayout();
            this.pnlCode.SuspendLayout();
            this.pnlTotalBalance.SuspendLayout();
            this.pnlInsurace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Insurance)).BeginInit();
            this.pnlClaimSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitClaims)).BeginInit();
            this.SuspendLayout();
            // 
            // c1PatientDetails
            // 
            this.c1PatientDetails.AllowEditing = false;
            this.c1PatientDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.c1PatientDetails.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.c1PatientDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientDetails.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1PatientDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientDetails.Location = new System.Drawing.Point(0, 0);
            this.c1PatientDetails.Name = "c1PatientDetails";
            this.c1PatientDetails.Rows.Count = 5;
            this.c1PatientDetails.Rows.DefaultSize = 19;
            this.c1PatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientDetails.ShowCellLabels = true;
            this.c1PatientDetails.Size = new System.Drawing.Size(1125, 239);
            this.c1PatientDetails.StyleInfo = resources.GetString("c1PatientDetails.StyleInfo");
            this.c1PatientDetails.TabIndex = 57;
            this.c1PatientDetails.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1PatientDetails.Tree.NodeImageCollapsed")));
            this.c1PatientDetails.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1PatientDetails.Tree.NodeImageExpanded")));
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Location = new System.Drawing.Point(0, 34);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1125, 1);
            this.label27.TabIndex = 59;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Location = new System.Drawing.Point(0, 4);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 30);
            this.label43.TabIndex = 61;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Location = new System.Drawing.Point(0, 3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1125, 1);
            this.label32.TabIndex = 60;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Location = new System.Drawing.Point(1124, 4);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 30);
            this.label44.TabIndex = 62;
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label36.AutoEllipsis = true;
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Location = new System.Drawing.Point(27, 12);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(51, 14);
            this.label36.TabIndex = 63;
            this.label36.Text = "Alerts :";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.pnlTop.Size = new System.Drawing.Size(1125, 28);
            this.pnlTop.TabIndex = 56;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Img_Blue2007;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnSearchPatientClaim);
            this.panel1.Controls.Add(this.txtPatientSearch);
            this.panel1.Controls.Add(this.btn_AddInsurancePlan);
            this.panel1.Controls.Add(this.lblSearchonClaimNo);
            this.panel1.Controls.Add(this.chk_ClaimNoSearch);
            this.panel1.Controls.Add(this.label52);
            this.panel1.Controls.Add(this.pnlButton);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1125, 25);
            this.panel1.TabIndex = 61;
            // 
            // btnSearchPatientClaim
            // 
            this.btnSearchPatientClaim.AutoSize = true;
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
            this.btnSearchPatientClaim.Location = new System.Drawing.Point(253, 1);
            this.btnSearchPatientClaim.MaximumSize = new System.Drawing.Size(130, 22);
            this.btnSearchPatientClaim.MinimumSize = new System.Drawing.Size(130, 22);
            this.btnSearchPatientClaim.Name = "btnSearchPatientClaim";
            this.btnSearchPatientClaim.Size = new System.Drawing.Size(130, 22);
            this.btnSearchPatientClaim.TabIndex = 1;
            this.btnSearchPatientClaim.TabStop = false;
            this.btnSearchPatientClaim.Text = "Claim Search";
            this.btnSearchPatientClaim.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchPatientClaim, "    Claim Search");
            this.btnSearchPatientClaim.UseVisualStyleBackColor = true;
            this.btnSearchPatientClaim.Visible = false;
            this.btnSearchPatientClaim.MouseLeave += new System.EventHandler(this.btnSearchPatientClaim_MouseLeave);
            this.btnSearchPatientClaim.Click += new System.EventHandler(this.btnSearchPatientClaim_Click);
            this.btnSearchPatientClaim.MouseHover += new System.EventHandler(this.btnSearchPatientClaim_MouseHover);
            // 
            // txtPatientSearch
            // 
            this.txtPatientSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPatientSearch.Location = new System.Drawing.Point(106, 1);
            this.txtPatientSearch.MaxLength = 15;
            this.txtPatientSearch.Name = "txtPatientSearch";
            this.txtPatientSearch.Size = new System.Drawing.Size(141, 22);
            this.txtPatientSearch.TabIndex = 0;
            this.txtPatientSearch.Visible = false;
            this.txtPatientSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatientSearch_KeyPress);
            // 
            // btn_AddInsurancePlan
            // 
            this.btn_AddInsurancePlan.AutoEllipsis = true;
            this.btn_AddInsurancePlan.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddInsurancePlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddInsurancePlan.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_AddInsurancePlan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btn_AddInsurancePlan.FlatAppearance.BorderSize = 0;
            this.btn_AddInsurancePlan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btn_AddInsurancePlan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btn_AddInsurancePlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddInsurancePlan.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddInsurancePlan.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddInsurancePlan.Image")));
            this.btn_AddInsurancePlan.Location = new System.Drawing.Point(948, 1);
            this.btn_AddInsurancePlan.Name = "btn_AddInsurancePlan";
            this.btn_AddInsurancePlan.Size = new System.Drawing.Size(98, 23);
            this.btn_AddInsurancePlan.TabIndex = 59;
            this.btn_AddInsurancePlan.TabStop = false;
            this.btn_AddInsurancePlan.Text = "Add Plan";
            this.btn_AddInsurancePlan.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolTip1.SetToolTip(this.btn_AddInsurancePlan, "Add Plan");
            this.btn_AddInsurancePlan.UseVisualStyleBackColor = false;
            this.btn_AddInsurancePlan.Visible = false;
            // 
            // lblSearchonClaimNo
            // 
            this.lblSearchonClaimNo.AutoSize = true;
            this.lblSearchonClaimNo.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchonClaimNo.Location = new System.Drawing.Point(461, 5);
            this.lblSearchonClaimNo.Name = "lblSearchonClaimNo";
            this.lblSearchonClaimNo.Size = new System.Drawing.Size(155, 14);
            this.lblSearchonClaimNo.TabIndex = 57;
            this.lblSearchonClaimNo.Text = "Search on Claim Number";
            this.lblSearchonClaimNo.Visible = false;
            // 
            // chk_ClaimNoSearch
            // 
            this.chk_ClaimNoSearch.AutoSize = true;
            this.chk_ClaimNoSearch.BackColor = System.Drawing.Color.Transparent;
            this.chk_ClaimNoSearch.Location = new System.Drawing.Point(447, 2);
            this.chk_ClaimNoSearch.Name = "chk_ClaimNoSearch";
            this.chk_ClaimNoSearch.Size = new System.Drawing.Size(174, 18);
            this.chk_ClaimNoSearch.TabIndex = 1;
            this.chk_ClaimNoSearch.TabStop = false;
            this.chk_ClaimNoSearch.Text = "Search on Claim Number";
            this.chk_ClaimNoSearch.UseVisualStyleBackColor = false;
            this.chk_ClaimNoSearch.Visible = false;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(0, 1);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 23);
            this.label52.TabIndex = 22;
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.Transparent;
            this.pnlButton.Controls.Add(this.btn_ModityPatient);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButton.ForeColor = System.Drawing.Color.Black;
            this.pnlButton.Location = new System.Drawing.Point(1046, 1);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(78, 23);
            this.pnlButton.TabIndex = 2;
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
            this.btn_ModityPatient.Size = new System.Drawing.Size(23, 23);
            this.btn_ModityPatient.TabIndex = 2;
            this.btn_ModityPatient.TabStop = false;
            this.btn_ModityPatient.UseVisualStyleBackColor = false;
            this.btn_ModityPatient.MouseLeave += new System.EventHandler(this.btn_ModityPatient_MouseLeave);
            this.btn_ModityPatient.Click += new System.EventHandler(this.btn_ModityPatient_Click);
            this.btn_ModityPatient.MouseHover += new System.EventHandler(this.btn_ModityPatient_MouseHover);
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
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(4, 3);
            this.label6.MaximumSize = new System.Drawing.Size(101, 18);
            this.label6.MinimumSize = new System.Drawing.Size(101, 18);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.label6.Size = new System.Drawing.Size(101, 18);
            this.label6.TabIndex = 20;
            this.label6.Text = "Patient Details";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(1124, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 23);
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
            this.label9.Size = new System.Drawing.Size(1125, 1);
            this.label9.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(0, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1125, 1);
            this.label13.TabIndex = 58;
            // 
            // pnlAlerts
            // 
            this.pnlAlerts.BackColor = System.Drawing.Color.Transparent;
            this.pnlAlerts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAlerts.Controls.Add(this.label36);
            this.pnlAlerts.Controls.Add(this.label44);
            this.pnlAlerts.Controls.Add(this.label43);
            this.pnlAlerts.Controls.Add(this.label32);
            this.pnlAlerts.Controls.Add(this.label27);
            this.pnlAlerts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAlerts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAlerts.ForeColor = System.Drawing.Color.White;
            this.pnlAlerts.Location = new System.Drawing.Point(0, 204);
            this.pnlAlerts.Name = "pnlAlerts";
            this.pnlAlerts.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlAlerts.Size = new System.Drawing.Size(1125, 35);
            this.pnlAlerts.TabIndex = 58;
            this.pnlAlerts.Visible = false;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.BackColor = System.Drawing.Color.Transparent;
            this.pnlMiddle.BackgroundImage = global::gloPatientStripControl.Properties.Resources.Img_Gradient;
            this.pnlMiddle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMiddle.Controls.Add(this.pnl_Main);
            this.pnlMiddle.Controls.Add(this.pnlTotalBalance);
            this.pnlMiddle.Controls.Add(this.pnlInsurace);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMiddle.ForeColor = System.Drawing.Color.White;
            this.pnlMiddle.Location = new System.Drawing.Point(0, 28);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Size = new System.Drawing.Size(1125, 176);
            this.pnlMiddle.TabIndex = 59;
            // 
            // pnl_Main
            // 
            this.pnl_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Main.Controls.Add(this.pnlRight);
            this.pnl_Main.Controls.Add(this.pnlLeft);
            this.pnl_Main.Controls.Add(this.label45);
            this.pnl_Main.Controls.Add(this.label42);
            this.pnl_Main.Controls.Add(this.label46);
            this.pnl_Main.Controls.Add(this.label39);
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Main.Location = new System.Drawing.Point(0, 0);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.Size = new System.Drawing.Size(414, 176);
            this.pnl_Main.TabIndex = 20;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.Transparent;
            this.pnlRight.Controls.Add(this.pnlSocialSecurity);
            this.pnlRight.Controls.Add(this.pnlReferralPhysician);
            this.pnlRight.Controls.Add(this.pnlCellPhone);
            this.pnlRight.Controls.Add(this.pnlHandDominance);
            this.pnlRight.Controls.Add(this.pnlPharmacyFax);
            this.pnlRight.Controls.Add(this.pnlPharmacyPhone);
            this.pnlRight.Controls.Add(this.pnlDate);
            this.pnlRight.Controls.Add(this.pnlAge);
            this.pnlRight.Controls.Add(this.pnlDOB);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(197, 1);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(216, 174);
            this.pnlRight.TabIndex = 48;
            // 
            // pnlSocialSecurity
            // 
            this.pnlSocialSecurity.Controls.Add(this.lblSocialSecurity);
            this.pnlSocialSecurity.Controls.Add(this.label8);
            this.pnlSocialSecurity.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSocialSecurity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlSocialSecurity.Location = new System.Drawing.Point(0, 147);
            this.pnlSocialSecurity.Name = "pnlSocialSecurity";
            this.pnlSocialSecurity.Size = new System.Drawing.Size(216, 18);
            this.pnlSocialSecurity.TabIndex = 42;
            // 
            // lblSocialSecurity
            // 
            this.lblSocialSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSocialSecurity.Location = new System.Drawing.Point(126, 0);
            this.lblSocialSecurity.Name = "lblSocialSecurity";
            this.lblSocialSecurity.Size = new System.Drawing.Size(90, 18);
            this.lblSocialSecurity.TabIndex = 12;
            this.lblSocialSecurity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 18);
            this.label8.TabIndex = 11;
            this.label8.Text = "Social Security # :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlReferralPhysician
            // 
            this.pnlReferralPhysician.Controls.Add(this.lblReferralPhysician);
            this.pnlReferralPhysician.Controls.Add(this.label10);
            this.pnlReferralPhysician.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReferralPhysician.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlReferralPhysician.Location = new System.Drawing.Point(0, 129);
            this.pnlReferralPhysician.Name = "pnlReferralPhysician";
            this.pnlReferralPhysician.Size = new System.Drawing.Size(216, 18);
            this.pnlReferralPhysician.TabIndex = 45;
            // 
            // lblReferralPhysician
            // 
            this.lblReferralPhysician.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReferralPhysician.Location = new System.Drawing.Point(126, 0);
            this.lblReferralPhysician.Name = "lblReferralPhysician";
            this.lblReferralPhysician.Size = new System.Drawing.Size(90, 18);
            this.lblReferralPhysician.TabIndex = 15;
            this.lblReferralPhysician.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(126, 18);
            this.label10.TabIndex = 10;
            this.label10.Text = "Referral Physician :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCellPhone
            // 
            this.pnlCellPhone.Controls.Add(this.lblCellPhone);
            this.pnlCellPhone.Controls.Add(this.label16);
            this.pnlCellPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCellPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlCellPhone.Location = new System.Drawing.Point(0, 111);
            this.pnlCellPhone.Name = "pnlCellPhone";
            this.pnlCellPhone.Size = new System.Drawing.Size(216, 18);
            this.pnlCellPhone.TabIndex = 44;
            // 
            // lblCellPhone
            // 
            this.lblCellPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCellPhone.Location = new System.Drawing.Point(126, 0);
            this.lblCellPhone.Name = "lblCellPhone";
            this.lblCellPhone.Size = new System.Drawing.Size(90, 18);
            this.lblCellPhone.TabIndex = 18;
            this.lblCellPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(126, 18);
            this.label16.TabIndex = 13;
            this.label16.Text = "Cell Phone :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlHandDominance
            // 
            this.pnlHandDominance.Controls.Add(this.lblHandDominance);
            this.pnlHandDominance.Controls.Add(this.label18);
            this.pnlHandDominance.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHandDominance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlHandDominance.Location = new System.Drawing.Point(0, 93);
            this.pnlHandDominance.Name = "pnlHandDominance";
            this.pnlHandDominance.Size = new System.Drawing.Size(216, 18);
            this.pnlHandDominance.TabIndex = 47;
            // 
            // lblHandDominance
            // 
            this.lblHandDominance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHandDominance.Location = new System.Drawing.Point(126, 0);
            this.lblHandDominance.Name = "lblHandDominance";
            this.lblHandDominance.Size = new System.Drawing.Size(90, 18);
            this.lblHandDominance.TabIndex = 19;
            this.lblHandDominance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(126, 18);
            this.label18.TabIndex = 14;
            this.label18.Text = "Hand Dominance :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPharmacyFax
            // 
            this.pnlPharmacyFax.Controls.Add(this.lblPharmacyFax);
            this.pnlPharmacyFax.Controls.Add(this.label22);
            this.pnlPharmacyFax.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPharmacyFax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlPharmacyFax.Location = new System.Drawing.Point(0, 75);
            this.pnlPharmacyFax.Name = "pnlPharmacyFax";
            this.pnlPharmacyFax.Size = new System.Drawing.Size(216, 18);
            this.pnlPharmacyFax.TabIndex = 41;
            // 
            // lblPharmacyFax
            // 
            this.lblPharmacyFax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPharmacyFax.Location = new System.Drawing.Point(126, 0);
            this.lblPharmacyFax.Name = "lblPharmacyFax";
            this.lblPharmacyFax.Size = new System.Drawing.Size(90, 18);
            this.lblPharmacyFax.TabIndex = 9;
            this.lblPharmacyFax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(126, 18);
            this.label22.TabIndex = 4;
            this.label22.Text = "Pharmacy Fax :";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPharmacyPhone
            // 
            this.pnlPharmacyPhone.Controls.Add(this.lblPharmacyPhone);
            this.pnlPharmacyPhone.Controls.Add(this.label24);
            this.pnlPharmacyPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPharmacyPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlPharmacyPhone.Location = new System.Drawing.Point(0, 57);
            this.pnlPharmacyPhone.Name = "pnlPharmacyPhone";
            this.pnlPharmacyPhone.Size = new System.Drawing.Size(216, 18);
            this.pnlPharmacyPhone.TabIndex = 40;
            // 
            // lblPharmacyPhone
            // 
            this.lblPharmacyPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPharmacyPhone.Location = new System.Drawing.Point(126, 0);
            this.lblPharmacyPhone.Name = "lblPharmacyPhone";
            this.lblPharmacyPhone.Size = new System.Drawing.Size(90, 18);
            this.lblPharmacyPhone.TabIndex = 7;
            this.lblPharmacyPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.label24.Location = new System.Drawing.Point(0, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(126, 18);
            this.label24.TabIndex = 3;
            this.label24.Text = "Pharmacy Phone :";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlDate
            // 
            this.pnlDate.Controls.Add(this.lblTodaysDate);
            this.pnlDate.Controls.Add(this.dtpDate);
            this.pnlDate.Controls.Add(this.label26);
            this.pnlDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlDate.Location = new System.Drawing.Point(0, 36);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(216, 21);
            this.pnlDate.TabIndex = 43;
            // 
            // lblTodaysDate
            // 
            this.lblTodaysDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTodaysDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.lblTodaysDate.Location = new System.Drawing.Point(126, 0);
            this.lblTodaysDate.Name = "lblTodaysDate";
            this.lblTodaysDate.Size = new System.Drawing.Size(250, 21);
            this.lblTodaysDate.TabIndex = 5;
            this.lblTodaysDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpDate
            // 
            this.dtpDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDate.Location = new System.Drawing.Point(135, 0);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(226, 22);
            this.dtpDate.TabIndex = 4;
            this.dtpDate.TabStop = false;
            // 
            // label26
            // 
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(126, 21);
            this.label26.TabIndex = 2;
            this.label26.Text = "Date :";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlAge
            // 
            this.pnlAge.Controls.Add(this.lblAge);
            this.pnlAge.Controls.Add(this.label20);
            this.pnlAge.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlAge.Location = new System.Drawing.Point(0, 18);
            this.pnlAge.Name = "pnlAge";
            this.pnlAge.Size = new System.Drawing.Size(216, 18);
            this.pnlAge.TabIndex = 46;
            // 
            // lblAge
            // 
            this.lblAge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAge.Location = new System.Drawing.Point(126, 0);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(90, 18);
            this.lblAge.TabIndex = 6;
            this.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(126, 18);
            this.label20.TabIndex = 1;
            this.label20.Text = "Age  :";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlDOB
            // 
            this.pnlDOB.Controls.Add(this.lblDOB);
            this.pnlDOB.Controls.Add(this.label28);
            this.pnlDOB.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDOB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlDOB.Location = new System.Drawing.Point(0, 0);
            this.pnlDOB.Name = "pnlDOB";
            this.pnlDOB.Size = new System.Drawing.Size(216, 18);
            this.pnlDOB.TabIndex = 39;
            // 
            // lblDOB
            // 
            this.lblDOB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDOB.Location = new System.Drawing.Point(126, 0);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(90, 18);
            this.lblDOB.TabIndex = 5;
            this.lblDOB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(126, 18);
            this.label28.TabIndex = 0;
            this.label28.Text = "DOB :";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLeft.Controls.Add(this.pnlSecInsurance);
            this.pnlLeft.Controls.Add(this.pnlPrInsurance);
            this.pnlLeft.Controls.Add(this.pnlOccupation);
            this.pnlLeft.Controls.Add(this.pnlGender);
            this.pnlLeft.Controls.Add(this.pnlPharmacyName);
            this.pnlLeft.Controls.Add(this.pnlPatientPhone);
            this.pnlLeft.Controls.Add(this.pnlProvider);
            this.pnlLeft.Controls.Add(this.pnlName);
            this.pnlLeft.Controls.Add(this.pnlCode);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlLeft.Location = new System.Drawing.Point(1, 1);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(144, 174);
            this.pnlLeft.TabIndex = 2;
            // 
            // pnlSecInsurance
            // 
            this.pnlSecInsurance.BackColor = System.Drawing.Color.Transparent;
            this.pnlSecInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSecInsurance.Controls.Add(this.lblSecInsurance);
            this.pnlSecInsurance.Controls.Add(this.label12);
            this.pnlSecInsurance.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSecInsurance.Location = new System.Drawing.Point(0, 144);
            this.pnlSecInsurance.Name = "pnlSecInsurance";
            this.pnlSecInsurance.Size = new System.Drawing.Size(144, 18);
            this.pnlSecInsurance.TabIndex = 38;
            // 
            // lblSecInsurance
            // 
            this.lblSecInsurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSecInsurance.Location = new System.Drawing.Point(109, 0);
            this.lblSecInsurance.Name = "lblSecInsurance";
            this.lblSecInsurance.Size = new System.Drawing.Size(35, 18);
            this.lblSecInsurance.TabIndex = 12;
            this.lblSecInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 18);
            this.label12.TabIndex = 11;
            this.label12.Text = "Sec Insurance :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPrInsurance
            // 
            this.pnlPrInsurance.BackColor = System.Drawing.Color.Transparent;
            this.pnlPrInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPrInsurance.Controls.Add(this.lblPrInsurance);
            this.pnlPrInsurance.Controls.Add(this.label11);
            this.pnlPrInsurance.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPrInsurance.Location = new System.Drawing.Point(0, 126);
            this.pnlPrInsurance.Name = "pnlPrInsurance";
            this.pnlPrInsurance.Size = new System.Drawing.Size(144, 18);
            this.pnlPrInsurance.TabIndex = 38;
            // 
            // lblPrInsurance
            // 
            this.lblPrInsurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrInsurance.Location = new System.Drawing.Point(109, 0);
            this.lblPrInsurance.Name = "lblPrInsurance";
            this.lblPrInsurance.Size = new System.Drawing.Size(35, 18);
            this.lblPrInsurance.TabIndex = 15;
            this.lblPrInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 18);
            this.label11.TabIndex = 10;
            this.label11.Text = "Pri Insurance :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlOccupation
            // 
            this.pnlOccupation.BackColor = System.Drawing.Color.Transparent;
            this.pnlOccupation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlOccupation.Controls.Add(this.lblOccupation);
            this.pnlOccupation.Controls.Add(this.label14);
            this.pnlOccupation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOccupation.Location = new System.Drawing.Point(0, 108);
            this.pnlOccupation.Name = "pnlOccupation";
            this.pnlOccupation.Size = new System.Drawing.Size(144, 18);
            this.pnlOccupation.TabIndex = 38;
            // 
            // lblOccupation
            // 
            this.lblOccupation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOccupation.Location = new System.Drawing.Point(109, 0);
            this.lblOccupation.Name = "lblOccupation";
            this.lblOccupation.Size = new System.Drawing.Size(35, 18);
            this.lblOccupation.TabIndex = 18;
            this.lblOccupation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(109, 18);
            this.label14.TabIndex = 13;
            this.label14.Text = "Occupation :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlGender
            // 
            this.pnlGender.BackColor = System.Drawing.Color.Transparent;
            this.pnlGender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGender.Controls.Add(this.lblGender);
            this.pnlGender.Controls.Add(this.label5);
            this.pnlGender.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGender.Location = new System.Drawing.Point(0, 90);
            this.pnlGender.Name = "pnlGender";
            this.pnlGender.Size = new System.Drawing.Size(144, 18);
            this.pnlGender.TabIndex = 38;
            // 
            // lblGender
            // 
            this.lblGender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGender.Location = new System.Drawing.Point(109, 0);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(35, 18);
            this.lblGender.TabIndex = 9;
            this.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "Gender :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPharmacyName
            // 
            this.pnlPharmacyName.BackColor = System.Drawing.Color.Transparent;
            this.pnlPharmacyName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPharmacyName.Controls.Add(this.lblPharmacyName);
            this.pnlPharmacyName.Controls.Add(this.label15);
            this.pnlPharmacyName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPharmacyName.Location = new System.Drawing.Point(0, 72);
            this.pnlPharmacyName.Name = "pnlPharmacyName";
            this.pnlPharmacyName.Size = new System.Drawing.Size(144, 18);
            this.pnlPharmacyName.TabIndex = 38;
            // 
            // lblPharmacyName
            // 
            this.lblPharmacyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPharmacyName.Location = new System.Drawing.Point(109, 0);
            this.lblPharmacyName.Name = "lblPharmacyName";
            this.lblPharmacyName.Size = new System.Drawing.Size(35, 18);
            this.lblPharmacyName.TabIndex = 19;
            this.lblPharmacyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(109, 18);
            this.label15.TabIndex = 14;
            this.label15.Text = "Pharmacy Name :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPatientPhone
            // 
            this.pnlPatientPhone.BackColor = System.Drawing.Color.Transparent;
            this.pnlPatientPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPatientPhone.Controls.Add(this.lblPhone);
            this.pnlPatientPhone.Controls.Add(this.label4);
            this.pnlPatientPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatientPhone.Location = new System.Drawing.Point(0, 54);
            this.pnlPatientPhone.Name = "pnlPatientPhone";
            this.pnlPatientPhone.Size = new System.Drawing.Size(144, 18);
            this.pnlPatientPhone.TabIndex = 38;
            // 
            // lblPhone
            // 
            this.lblPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhone.Location = new System.Drawing.Point(109, 0);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(35, 18);
            this.lblPhone.TabIndex = 8;
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Phone :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlProvider
            // 
            this.pnlProvider.BackColor = System.Drawing.Color.Transparent;
            this.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProvider.Controls.Add(this.lblProvider);
            this.pnlProvider.Controls.Add(this.label3);
            this.pnlProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProvider.Location = new System.Drawing.Point(0, 36);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(144, 18);
            this.pnlProvider.TabIndex = 38;
            // 
            // lblProvider
            // 
            this.lblProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProvider.Location = new System.Drawing.Point(109, 0);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(35, 18);
            this.lblProvider.TabIndex = 7;
            this.lblProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Provider :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlName
            // 
            this.pnlName.BackColor = System.Drawing.Color.Transparent;
            this.pnlName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlName.Controls.Add(this.lblPatientName);
            this.pnlName.Controls.Add(this.label2);
            this.pnlName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlName.Location = new System.Drawing.Point(0, 18);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(144, 18);
            this.pnlName.TabIndex = 38;
            // 
            // lblPatientName
            // 
            this.lblPatientName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatientName.Location = new System.Drawing.Point(109, 0);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(35, 18);
            this.lblPatientName.TabIndex = 6;
            this.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name  :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCode
            // 
            this.pnlCode.BackColor = System.Drawing.Color.Transparent;
            this.pnlCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCode.Controls.Add(this.lblPatientCode);
            this.pnlCode.Controls.Add(this.label1);
            this.pnlCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCode.Location = new System.Drawing.Point(0, 0);
            this.pnlCode.Name = "pnlCode";
            this.pnlCode.Size = new System.Drawing.Size(144, 18);
            this.pnlCode.TabIndex = 37;
            // 
            // lblPatientCode
            // 
            this.lblPatientCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatientCode.Location = new System.Drawing.Point(109, 0);
            this.lblPatientCode.Name = "lblPatientCode";
            this.lblPatientCode.Size = new System.Drawing.Size(35, 18);
            this.lblPatientCode.TabIndex = 5;
            this.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Location = new System.Drawing.Point(0, 1);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1, 174);
            this.label45.TabIndex = 62;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(0, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(413, 1);
            this.label42.TabIndex = 64;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Right;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Location = new System.Drawing.Point(413, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1, 175);
            this.label46.TabIndex = 65;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Location = new System.Drawing.Point(0, 175);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(414, 1);
            this.label39.TabIndex = 63;
            // 
            // pnlTotalBalance
            // 
            this.pnlTotalBalance.BackColor = System.Drawing.Color.Transparent;
            this.pnlTotalBalance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTotalBalance.Controls.Add(this.label49);
            this.pnlTotalBalance.Controls.Add(this.label48);
            this.pnlTotalBalance.Controls.Add(this.label41);
            this.pnlTotalBalance.Controls.Add(this.label40);
            this.pnlTotalBalance.Controls.Add(this.label21);
            this.pnlTotalBalance.Controls.Add(this.label17);
            this.pnlTotalBalance.Controls.Add(this.label37);
            this.pnlTotalBalance.Controls.Add(this.lblBudget);
            this.pnlTotalBalance.Controls.Add(this.label35);
            this.pnlTotalBalance.Controls.Add(this.lblCollection);
            this.pnlTotalBalance.Controls.Add(this.label33);
            this.pnlTotalBalance.Controls.Add(this.lblPendingOtherReserved);
            this.pnlTotalBalance.Controls.Add(this.label34);
            this.pnlTotalBalance.Controls.Add(this.lblPendingAdvance);
            this.pnlTotalBalance.Controls.Add(this.label30);
            this.pnlTotalBalance.Controls.Add(this.lblPendingCopay);
            this.pnlTotalBalance.Controls.Add(this.label23);
            this.pnlTotalBalance.Controls.Add(this.lblPatientPending);
            this.pnlTotalBalance.Controls.Add(this.label31);
            this.pnlTotalBalance.Controls.Add(this.lblInsurancePending);
            this.pnlTotalBalance.Controls.Add(this.label29);
            this.pnlTotalBalance.Controls.Add(this.lblTotalCharges);
            this.pnlTotalBalance.Controls.Add(this.label19);
            this.pnlTotalBalance.Controls.Add(this.lblTotalBalance);
            this.pnlTotalBalance.Controls.Add(this.label25);
            this.pnlTotalBalance.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTotalBalance.Location = new System.Drawing.Point(414, 0);
            this.pnlTotalBalance.Name = "pnlTotalBalance";
            this.pnlTotalBalance.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.pnlTotalBalance.Size = new System.Drawing.Size(261, 176);
            this.pnlTotalBalance.TabIndex = 22;
            this.pnlTotalBalance.Visible = false;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Location = new System.Drawing.Point(3, 175);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(254, 1);
            this.label49.TabIndex = 65;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Top;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Location = new System.Drawing.Point(3, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(254, 1);
            this.label48.TabIndex = 64;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.Green;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.Fuchsia;
            this.label41.Location = new System.Drawing.Point(15, 126);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(230, 2);
            this.label41.TabIndex = 57;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.Green;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.Fuchsia;
            this.label40.Location = new System.Drawing.Point(15, 60);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(230, 2);
            this.label40.TabIndex = 56;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Location = new System.Drawing.Point(2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 176);
            this.label21.TabIndex = 55;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(2, 176);
            this.label17.TabIndex = 54;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Right;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Location = new System.Drawing.Point(257, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 176);
            this.label37.TabIndex = 53;
            // 
            // lblBudget
            // 
            this.lblBudget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBudget.AutoEllipsis = true;
            this.lblBudget.AutoSize = true;
            this.lblBudget.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBudget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBudget.Location = new System.Drawing.Point(127, 155);
            this.lblBudget.MaximumSize = new System.Drawing.Size(105, 15);
            this.lblBudget.MinimumSize = new System.Drawing.Size(105, 15);
            this.lblBudget.Name = "lblBudget";
            this.lblBudget.Size = new System.Drawing.Size(105, 15);
            this.lblBudget.TabIndex = 11;
            this.lblBudget.Text = "$ 0.00";
            this.lblBudget.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBudget.Visible = false;
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoEllipsis = true;
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Location = new System.Drawing.Point(7, 155);
            this.label35.MaximumSize = new System.Drawing.Size(120, 16);
            this.label35.MinimumSize = new System.Drawing.Size(120, 16);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(120, 16);
            this.label35.TabIndex = 10;
            this.label35.Text = "Budget :";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label35.Visible = false;
            // 
            // lblCollection
            // 
            this.lblCollection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCollection.AutoEllipsis = true;
            this.lblCollection.AutoSize = true;
            this.lblCollection.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCollection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCollection.Location = new System.Drawing.Point(127, 137);
            this.lblCollection.MaximumSize = new System.Drawing.Size(105, 15);
            this.lblCollection.MinimumSize = new System.Drawing.Size(105, 15);
            this.lblCollection.Name = "lblCollection";
            this.lblCollection.Size = new System.Drawing.Size(105, 15);
            this.lblCollection.TabIndex = 9;
            this.lblCollection.Text = "$ 0.00";
            this.lblCollection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCollection.Visible = false;
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoEllipsis = true;
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Location = new System.Drawing.Point(7, 137);
            this.label33.MaximumSize = new System.Drawing.Size(120, 16);
            this.label33.MinimumSize = new System.Drawing.Size(120, 16);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(120, 16);
            this.label33.TabIndex = 8;
            this.label33.Text = "Collection :";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label33.Visible = false;
            // 
            // lblPendingOtherReserved
            // 
            this.lblPendingOtherReserved.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPendingOtherReserved.AutoEllipsis = true;
            this.lblPendingOtherReserved.AutoSize = true;
            this.lblPendingOtherReserved.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingOtherReserved.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPendingOtherReserved.Location = new System.Drawing.Point(127, 106);
            this.lblPendingOtherReserved.MaximumSize = new System.Drawing.Size(105, 15);
            this.lblPendingOtherReserved.MinimumSize = new System.Drawing.Size(105, 15);
            this.lblPendingOtherReserved.Name = "lblPendingOtherReserved";
            this.lblPendingOtherReserved.Size = new System.Drawing.Size(105, 15);
            this.lblPendingOtherReserved.TabIndex = 7;
            this.lblPendingOtherReserved.Text = "$ 0.00";
            this.lblPendingOtherReserved.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoEllipsis = true;
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Location = new System.Drawing.Point(7, 106);
            this.label34.MaximumSize = new System.Drawing.Size(120, 16);
            this.label34.MinimumSize = new System.Drawing.Size(120, 16);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(120, 16);
            this.label34.TabIndex = 6;
            this.label34.Text = " Other Resv :";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPendingAdvance
            // 
            this.lblPendingAdvance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPendingAdvance.AutoEllipsis = true;
            this.lblPendingAdvance.AutoSize = true;
            this.lblPendingAdvance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingAdvance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPendingAdvance.Location = new System.Drawing.Point(127, 86);
            this.lblPendingAdvance.MaximumSize = new System.Drawing.Size(105, 15);
            this.lblPendingAdvance.MinimumSize = new System.Drawing.Size(105, 15);
            this.lblPendingAdvance.Name = "lblPendingAdvance";
            this.lblPendingAdvance.Size = new System.Drawing.Size(105, 15);
            this.lblPendingAdvance.TabIndex = 7;
            this.lblPendingAdvance.Text = "$ 0.00";
            this.lblPendingAdvance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.AutoEllipsis = true;
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Location = new System.Drawing.Point(7, 86);
            this.label30.MaximumSize = new System.Drawing.Size(120, 16);
            this.label30.MinimumSize = new System.Drawing.Size(120, 16);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(120, 16);
            this.label30.TabIndex = 6;
            this.label30.Text = " Advance :";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPendingCopay
            // 
            this.lblPendingCopay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPendingCopay.AutoEllipsis = true;
            this.lblPendingCopay.AutoSize = true;
            this.lblPendingCopay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingCopay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPendingCopay.Location = new System.Drawing.Point(127, 66);
            this.lblPendingCopay.MaximumSize = new System.Drawing.Size(105, 15);
            this.lblPendingCopay.MinimumSize = new System.Drawing.Size(105, 15);
            this.lblPendingCopay.Name = "lblPendingCopay";
            this.lblPendingCopay.Size = new System.Drawing.Size(105, 15);
            this.lblPendingCopay.TabIndex = 7;
            this.lblPendingCopay.Text = "$ 0.00";
            this.lblPendingCopay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoEllipsis = true;
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Location = new System.Drawing.Point(7, 66);
            this.label23.MaximumSize = new System.Drawing.Size(120, 16);
            this.label23.MinimumSize = new System.Drawing.Size(120, 16);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(120, 16);
            this.label23.TabIndex = 6;
            this.label23.Text = " Copay :";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPatientPending
            // 
            this.lblPatientPending.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientPending.AutoEllipsis = true;
            this.lblPatientPending.AutoSize = true;
            this.lblPatientPending.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientPending.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatientPending.Location = new System.Drawing.Point(127, 41);
            this.lblPatientPending.MaximumSize = new System.Drawing.Size(105, 15);
            this.lblPatientPending.MinimumSize = new System.Drawing.Size(105, 15);
            this.lblPatientPending.Name = "lblPatientPending";
            this.lblPatientPending.Size = new System.Drawing.Size(105, 15);
            this.lblPatientPending.TabIndex = 7;
            this.lblPatientPending.Text = "$ 0.00";
            this.lblPatientPending.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoEllipsis = true;
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(7, 41);
            this.label31.MaximumSize = new System.Drawing.Size(120, 16);
            this.label31.MinimumSize = new System.Drawing.Size(120, 16);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(120, 16);
            this.label31.TabIndex = 6;
            this.label31.Text = "Pat. Due :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInsurancePending
            // 
            this.lblInsurancePending.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInsurancePending.AutoEllipsis = true;
            this.lblInsurancePending.AutoSize = true;
            this.lblInsurancePending.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsurancePending.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsurancePending.Location = new System.Drawing.Point(127, 23);
            this.lblInsurancePending.MaximumSize = new System.Drawing.Size(105, 15);
            this.lblInsurancePending.MinimumSize = new System.Drawing.Size(105, 15);
            this.lblInsurancePending.Name = "lblInsurancePending";
            this.lblInsurancePending.Size = new System.Drawing.Size(105, 15);
            this.lblInsurancePending.TabIndex = 5;
            this.lblInsurancePending.Text = "$ 0.00";
            this.lblInsurancePending.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoEllipsis = true;
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Location = new System.Drawing.Point(7, 23);
            this.label29.MaximumSize = new System.Drawing.Size(120, 16);
            this.label29.MinimumSize = new System.Drawing.Size(120, 16);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(120, 16);
            this.label29.TabIndex = 4;
            this.label29.Text = "Ins.Pending :";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCharges
            // 
            this.lblTotalCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTotalCharges.Location = new System.Drawing.Point(106, 146);
            this.lblTotalCharges.Name = "lblTotalCharges";
            this.lblTotalCharges.Size = new System.Drawing.Size(105, 14);
            this.lblTotalCharges.TabIndex = 3;
            this.lblTotalCharges.Text = "$ 0.00";
            this.lblTotalCharges.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Location = new System.Drawing.Point(9, 146);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(90, 14);
            this.label19.TabIndex = 1;
            this.label19.Text = "Total Charges :";
            // 
            // lblTotalBalance
            // 
            this.lblTotalBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalBalance.AutoEllipsis = true;
            this.lblTotalBalance.AutoSize = true;
            this.lblTotalBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalBalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTotalBalance.Location = new System.Drawing.Point(127, 5);
            this.lblTotalBalance.MaximumSize = new System.Drawing.Size(105, 15);
            this.lblTotalBalance.MinimumSize = new System.Drawing.Size(105, 15);
            this.lblTotalBalance.Name = "lblTotalBalance";
            this.lblTotalBalance.Size = new System.Drawing.Size(105, 15);
            this.lblTotalBalance.TabIndex = 3;
            this.lblTotalBalance.Text = "$ 0.00";
            this.lblTotalBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoEllipsis = true;
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(7, 5);
            this.label25.MaximumSize = new System.Drawing.Size(120, 16);
            this.label25.MinimumSize = new System.Drawing.Size(120, 16);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(120, 16);
            this.label25.TabIndex = 1;
            this.label25.Text = "Total Balance :";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlInsurace
            // 
            this.pnlInsurace.Controls.Add(this.label53);
            this.pnlInsurace.Controls.Add(this.label51);
            this.pnlInsurace.Controls.Add(this.label50);
            this.pnlInsurace.Controls.Add(this.label38);
            this.pnlInsurace.Controls.Add(this.c1Insurance);
            this.pnlInsurace.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlInsurace.Location = new System.Drawing.Point(675, 0);
            this.pnlInsurace.Name = "pnlInsurace";
            this.pnlInsurace.Size = new System.Drawing.Size(450, 176);
            this.pnlInsurace.TabIndex = 21;
            this.pnlInsurace.Visible = false;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Top;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Location = new System.Drawing.Point(1, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(448, 1);
            this.label53.TabIndex = 65;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Location = new System.Drawing.Point(1, 175);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(448, 1);
            this.label51.TabIndex = 64;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Right;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Location = new System.Drawing.Point(449, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 176);
            this.label50.TabIndex = 62;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Left;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Location = new System.Drawing.Point(0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 176);
            this.label38.TabIndex = 61;
            // 
            // c1Insurance
            // 
            this.c1Insurance.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Insurance.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Insurance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Insurance.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Insurance.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1Insurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Insurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Insurance.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1Insurance.Location = new System.Drawing.Point(0, 0);
            this.c1Insurance.Name = "c1Insurance";
            this.c1Insurance.Padding = new System.Windows.Forms.Padding(3);
            this.c1Insurance.Rows.Count = 1;
            this.c1Insurance.Rows.DefaultSize = 19;
            this.c1Insurance.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1Insurance.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None;
            this.c1Insurance.Size = new System.Drawing.Size(450, 176);
            this.c1Insurance.StyleInfo = resources.GetString("c1Insurance.StyleInfo");
            this.c1Insurance.TabIndex = 60;
            this.c1Insurance.TabStop = false;
            this.c1Insurance.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_AfterEdit);
            this.c1Insurance.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Insurance_MouseMove);
            // 
            // pnlClaimSearch
            // 
            this.pnlClaimSearch.AutoSize = true;
            this.pnlClaimSearch.Controls.Add(this.label56);
            this.pnlClaimSearch.Controls.Add(this.label55);
            this.pnlClaimSearch.Controls.Add(this.label54);
            this.pnlClaimSearch.Controls.Add(this.label47);
            this.pnlClaimSearch.Controls.Add(this.c1SplitClaims);
            this.pnlClaimSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlClaimSearch.Location = new System.Drawing.Point(0, 28);
            this.pnlClaimSearch.Name = "pnlClaimSearch";
            this.pnlClaimSearch.Size = new System.Drawing.Size(1125, 176);
            this.pnlClaimSearch.TabIndex = 7;
            this.pnlClaimSearch.Visible = false;
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Location = new System.Drawing.Point(1, 175);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(1123, 1);
            this.label56.TabIndex = 65;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Top;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Location = new System.Drawing.Point(1, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1123, 1);
            this.label55.TabIndex = 64;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Right;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Location = new System.Drawing.Point(1124, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(1, 176);
            this.label54.TabIndex = 63;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Left;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Location = new System.Drawing.Point(0, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(1, 176);
            this.label47.TabIndex = 62;
            // 
            // c1SplitClaims
            // 
            this.c1SplitClaims.AllowEditing = false;
            this.c1SplitClaims.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1SplitClaims.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SplitClaims.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.c1SplitClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SplitClaims.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1SplitClaims.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1SplitClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1SplitClaims.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SplitClaims.Location = new System.Drawing.Point(0, 0);
            this.c1SplitClaims.Name = "c1SplitClaims";
            this.c1SplitClaims.Rows.Count = 5;
            this.c1SplitClaims.Rows.DefaultSize = 19;
            this.c1SplitClaims.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SplitClaims.ShowCellLabels = true;
            this.c1SplitClaims.Size = new System.Drawing.Size(1125, 176);
            this.c1SplitClaims.StyleInfo = resources.GetString("c1SplitClaims.StyleInfo");
            this.c1SplitClaims.TabIndex = 55;
            this.c1SplitClaims.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1SplitClaims.Tree.NodeImageCollapsed")));
            this.c1SplitClaims.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1SplitClaims.Tree.NodeImageExpanded")));
            this.c1SplitClaims.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1SplitClaims_MouseDoubleClick);
            this.c1SplitClaims.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1SplitClaims_MouseMove);
            this.c1SplitClaims.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1SplitClaims_KeyPress);
            // 
            // ucPatientStripControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnlClaimSearch);
            this.Controls.Add(this.pnlMiddle);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlAlerts);
            this.Controls.Add(this.c1PatientDetails);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "ucPatientStripControl";
            this.Size = new System.Drawing.Size(1125, 239);
            this.Load += new System.EventHandler(this.PatientStripControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PatientStripControl_Paint);
            this.Leave += new System.EventHandler(this.ucPatientStripControl_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            this.pnlAlerts.ResumeLayout(false);
            this.pnlAlerts.PerformLayout();
            this.pnlMiddle.ResumeLayout(false);
            this.pnl_Main.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlSocialSecurity.ResumeLayout(false);
            this.pnlReferralPhysician.ResumeLayout(false);
            this.pnlCellPhone.ResumeLayout(false);
            this.pnlHandDominance.ResumeLayout(false);
            this.pnlPharmacyFax.ResumeLayout(false);
            this.pnlPharmacyPhone.ResumeLayout(false);
            this.pnlDate.ResumeLayout(false);
            this.pnlAge.ResumeLayout(false);
            this.pnlDOB.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlSecInsurance.ResumeLayout(false);
            this.pnlPrInsurance.ResumeLayout(false);
            this.pnlOccupation.ResumeLayout(false);
            this.pnlGender.ResumeLayout(false);
            this.pnlPharmacyName.ResumeLayout(false);
            this.pnlPatientPhone.ResumeLayout(false);
            this.pnlProvider.ResumeLayout(false);
            this.pnlName.ResumeLayout(false);
            this.pnlCode.ResumeLayout(false);
            this.pnlTotalBalance.ResumeLayout(false);
            this.pnlTotalBalance.PerformLayout();
            this.pnlInsurace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Insurance)).EndInit();
            this.pnlClaimSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitClaims)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientDetails;
        private System.Windows.Forms.Label lblPatientCode;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Panel pnlTotalBalance;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label lblBudget;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label lblCollection;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label lblPendingOtherReserved;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label lblPendingAdvance;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label lblPendingCopay;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblPatientPending;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblInsurancePending;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblTotalCharges;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblTotalBalance;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSecInsurance;
        private System.Windows.Forms.Label lblPrInsurance;
        private System.Windows.Forms.Panel pnlPrInsurance;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblOccupation;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblPharmacyName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlInsurace;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label38;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Insurance;
        private System.Windows.Forms.Panel pnlSecInsurance;
        private System.Windows.Forms.TextBox txtPatientSearch;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlOccupation;
        private System.Windows.Forms.Panel pnlGender;
        private System.Windows.Forms.Panel pnlPharmacyName;
        private System.Windows.Forms.Panel pnlPatientPhone;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Panel pnlCode;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btnSearchPatientClaim;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnlMiddle;
        private System.Windows.Forms.Panel pnl_Main;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlSocialSecurity;
        private System.Windows.Forms.Label lblSocialSecurity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlReferralPhysician;
        private System.Windows.Forms.Label lblReferralPhysician;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlCellPhone;
        private System.Windows.Forms.Label lblCellPhone;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel pnlHandDominance;
        private System.Windows.Forms.Label lblHandDominance;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnlPharmacyFax;
        private System.Windows.Forms.Label lblPharmacyFax;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel pnlPharmacyPhone;
        private System.Windows.Forms.Label lblPharmacyPhone;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel pnlDate;
        private System.Windows.Forms.Label lblTodaysDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel pnlAge;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel pnlDOB;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btn_AddInsurancePlan;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label lblSearchonClaimNo;
        private System.Windows.Forms.CheckBox chk_ClaimNoSearch;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btn_ModityPatient;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlAlerts;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel pnlClaimSearch;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SplitClaims;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label47;
    }
}
