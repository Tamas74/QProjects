namespace gloStripControl
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblDobCaption = new System.Windows.Forms.Label();
            this.lblDOB = new System.Windows.Forms.Label();
            this.lblGenderCaption = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblPatCodeCaption = new System.Windows.Forms.Label();
            this.lblPatientCode = new System.Windows.Forms.Label();
            this.btnSearchPatientClaim = new System.Windows.Forms.Button();
            this.txtPatientSearch = new System.Windows.Forms.TextBox();
            this.lblSearchonClaimNo = new System.Windows.Forms.Label();
            this.chk_ClaimNoSearch = new System.Windows.Forms.CheckBox();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btn_ModityPatient = new System.Windows.Forms.Button();
            this.lbltxtPatientSearchCaption = new System.Windows.Forms.Label();
            this.lblPnlHeaderTop = new System.Windows.Forms.Label();
            this.lblPnlHeaderMiddle = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnEditAccount = new System.Windows.Forms.Button();
            this.btn_Alerts = new System.Windows.Forms.Button();
            this.pnlClaimSearch = new System.Windows.Forms.Panel();
            this.label56 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.c1SplitClaims = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1PatientDetails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlInsurace = new System.Windows.Forms.Panel();
            this.lblpnlInsuraceTop = new System.Windows.Forms.Label();
            this.lblpnlInsuraceBottom = new System.Windows.Forms.Label();
            this.lblpnlInsuraceRight = new System.Windows.Forms.Label();
            this.lblpnlInsuraceLeft = new System.Windows.Forms.Label();
            this.c1Insurance = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlBalance = new System.Windows.Forms.Panel();
            this.pnlReserve = new System.Windows.Forms.Panel();
            this.lblBalTotalLine = new System.Windows.Forms.Label();
            this.lblBalOtherReserve = new System.Windows.Forms.Label();
            this.lblBalAdvancedReserve = new System.Windows.Forms.Label();
            this.lblBalCopayReserveCaption = new System.Windows.Forms.Label();
            this.lblBalCopayReserve = new System.Windows.Forms.Label();
            this.lblBalAdvancedReserveCaption = new System.Windows.Forms.Label();
            this.lblBalOtherReserveCaption = new System.Windows.Forms.Label();
            this.pnlBadDebt = new System.Windows.Forms.Panel();
            this.lblBadDebtSatus = new System.Windows.Forms.Label();
            this.lblBalBadDebtCaption = new System.Windows.Forms.Label();
            this.lblBalBadDebt = new System.Windows.Forms.Label();
            this.pnlTotalBal = new System.Windows.Forms.Panel();
            this.lblBalPatientDueCaption = new System.Windows.Forms.Label();
            this.lblBalTotalBalanceCaption = new System.Windows.Forms.Label();
            this.lblBalPatientDue = new System.Windows.Forms.Label();
            this.lblBalInsurancePendingCaption = new System.Windows.Forms.Label();
            this.lblBalInsurancePending = new System.Windows.Forms.Label();
            this.lblBalTotalBalance = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pnlAcctNote = new System.Windows.Forms.Panel();
            this.lblAccountNotes = new System.Windows.Forms.Label();
            this.lblAccountNotesCaption = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.pnlEMRAlertsCaption = new System.Windows.Forms.Panel();
            this.lblDemoEMRAlerts = new System.Windows.Forms.Label();
            this.lblDemogloEMRAlertsCaption = new System.Windows.Forms.Label();
            this.lblDemoNextAppt = new System.Windows.Forms.Label();
            this.lblDemoNextApptCaption = new System.Windows.Forms.Label();
            this.lblDemoLastPatPayment = new System.Windows.Forms.Label();
            this.lblDemoGuarantorCaption = new System.Windows.Forms.Label();
            this.lblDemoGuarantor = new System.Windows.Forms.Label();
            this.lblDemoPatientPayment = new System.Windows.Forms.Label();
            this.pnlAccountDetails = new System.Windows.Forms.Panel();
            this.lblDemoAccountNoCaption = new System.Windows.Forms.Label();
            this.lblDemoAccountNo = new System.Windows.Forms.Label();
            this.lblAccountDesc = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDemoAccountDesc = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlNotesAlerts = new System.Windows.Forms.Panel();
            this.lblAlerts = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblAlertsCap = new System.Windows.Forms.Label();
            this.lblNotesCaption = new System.Windows.Forms.Label();
            this.lblDemoCopay = new System.Windows.Forms.Label();
            this.lblDemoProvider = new System.Windows.Forms.Label();
            this.lblDemoNotes = new System.Windows.Forms.Label();
            this.lblDemoCopayCaption = new System.Windows.Forms.Label();
            this.lblDemoProviderCaption = new System.Windows.Forms.Label();
            this.pnlMedCategory = new System.Windows.Forms.Panel();
            this.lblDemoMedCat = new System.Windows.Forms.Label();
            this.lblDemoMedCatCaption = new System.Windows.Forms.Label();
            this.pnlPatient = new System.Windows.Forms.Panel();
            this.lblDemoPatientCaption = new System.Windows.Forms.Label();
            this.lblDemoPatient = new System.Windows.Forms.Label();
            this.pnlPatientPhoto = new System.Windows.Forms.Panel();
            this.lblBadDebtSatusII = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picPAPhoto = new System.Windows.Forms.PictureBox();
            this.lblucPatientStripBottom = new System.Windows.Forms.Label();
            this.lblucPatientStripRight = new System.Windows.Forms.Label();
            this.lblucPatientStripLeft = new System.Windows.Forms.Label();
            this.cmnu_ChangeInsResponsiblity = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuViewBenefit = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlClaimSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitClaims)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlInsurace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Insurance)).BeginInit();
            this.pnlBalance.SuspendLayout();
            this.pnlReserve.SuspendLayout();
            this.pnlBadDebt.SuspendLayout();
            this.pnlTotalBal.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlAcctNote.SuspendLayout();
            this.panel9.SuspendLayout();
            this.pnlEMRAlertsCaption.SuspendLayout();
            this.pnlAccountDetails.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlNotesAlerts.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlMedCategory.SuspendLayout();
            this.pnlPatient.SuspendLayout();
            this.pnlPatientPhoto.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPAPhoto)).BeginInit();
            this.cmnu_ChangeInsResponsiblity.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(1, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1199, 27);
            this.pnlTop.TabIndex = 56;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lblPatientName);
            this.panel1.Controls.Add(this.lblDobCaption);
            this.panel1.Controls.Add(this.lblDOB);
            this.panel1.Controls.Add(this.lblGenderCaption);
            this.panel1.Controls.Add(this.lblGender);
            this.panel1.Controls.Add(this.lblPatCodeCaption);
            this.panel1.Controls.Add(this.lblPatientCode);
            this.panel1.Controls.Add(this.btnSearchPatientClaim);
            this.panel1.Controls.Add(this.txtPatientSearch);
            this.panel1.Controls.Add(this.lblSearchonClaimNo);
            this.panel1.Controls.Add(this.chk_ClaimNoSearch);
            this.panel1.Controls.Add(this.pnlButton);
            this.panel1.Controls.Add(this.lbltxtPatientSearchCaption);
            this.panel1.Controls.Add(this.lblPnlHeaderTop);
            this.panel1.Controls.Add(this.lblPnlHeaderMiddle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1199, 27);
            this.panel1.TabIndex = 61;
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientName.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPatientName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.ForeColor = System.Drawing.Color.Black;
            this.lblPatientName.Location = new System.Drawing.Point(645, 1);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Padding = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.lblPatientName.Size = new System.Drawing.Size(59, 21);
            this.lblPatientName.TabIndex = 61;
            this.lblPatientName.Text = "            ";
            this.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPatientName.Visible = false;
            // 
            // lblDobCaption
            // 
            this.lblDobCaption.AutoSize = true;
            this.lblDobCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDobCaption.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDobCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDobCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDobCaption.Location = new System.Drawing.Point(704, 1);
            this.lblDobCaption.Name = "lblDobCaption";
            this.lblDobCaption.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblDobCaption.Size = new System.Drawing.Size(37, 21);
            this.lblDobCaption.TabIndex = 60;
            this.lblDobCaption.Text = "DOB:";
            this.lblDobCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDobCaption.Visible = false;
            // 
            // lblDOB
            // 
            this.lblDOB.AutoSize = true;
            this.lblDOB.BackColor = System.Drawing.Color.Transparent;
            this.lblDOB.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOB.ForeColor = System.Drawing.Color.Black;
            this.lblDOB.Location = new System.Drawing.Point(741, 1);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblDOB.Size = new System.Drawing.Size(127, 19);
            this.lblDOB.TabIndex = 59;
            this.lblDOB.Text = "                              ";
            this.lblDOB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDOB.Visible = false;
            // 
            // lblGenderCaption
            // 
            this.lblGenderCaption.AutoSize = true;
            this.lblGenderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblGenderCaption.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblGenderCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblGenderCaption.Location = new System.Drawing.Point(868, 1);
            this.lblGenderCaption.Name = "lblGenderCaption";
            this.lblGenderCaption.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblGenderCaption.Size = new System.Drawing.Size(54, 21);
            this.lblGenderCaption.TabIndex = 62;
            this.lblGenderCaption.Text = "Gender:";
            this.lblGenderCaption.Visible = false;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.BackColor = System.Drawing.Color.Transparent;
            this.lblGender.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblGender.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGender.ForeColor = System.Drawing.Color.Black;
            this.lblGender.Location = new System.Drawing.Point(922, 1);
            this.lblGender.Name = "lblGender";
            this.lblGender.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblGender.Size = new System.Drawing.Size(64, 21);
            this.lblGender.TabIndex = 65;
            this.lblGender.Text = "              ";
            this.lblGender.Visible = false;
            // 
            // lblPatCodeCaption
            // 
            this.lblPatCodeCaption.AutoSize = true;
            this.lblPatCodeCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPatCodeCaption.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPatCodeCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatCodeCaption.ForeColor = System.Drawing.Color.Black;
            this.lblPatCodeCaption.Location = new System.Drawing.Point(986, 1);
            this.lblPatCodeCaption.Name = "lblPatCodeCaption";
            this.lblPatCodeCaption.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblPatCodeCaption.Size = new System.Drawing.Size(85, 21);
            this.lblPatCodeCaption.TabIndex = 64;
            this.lblPatCodeCaption.Text = "Patient Code:";
            this.lblPatCodeCaption.Visible = false;
            // 
            // lblPatientCode
            // 
            this.lblPatientCode.AutoSize = true;
            this.lblPatientCode.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientCode.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPatientCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCode.ForeColor = System.Drawing.Color.Black;
            this.lblPatientCode.Location = new System.Drawing.Point(1071, 1);
            this.lblPatientCode.Name = "lblPatientCode";
            this.lblPatientCode.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblPatientCode.Size = new System.Drawing.Size(100, 21);
            this.lblPatientCode.TabIndex = 63;
            this.lblPatientCode.Text = "                       ";
            this.lblPatientCode.Visible = false;
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
            this.btnSearchPatientClaim.ForeColor = System.Drawing.Color.Black;
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
            this.btnSearchPatientClaim.Click += new System.EventHandler(this.btnSearchPatientClaim_Click);
            this.btnSearchPatientClaim.MouseLeave += new System.EventHandler(this.btnSearchPatientClaim_MouseLeave);
            this.btnSearchPatientClaim.MouseHover += new System.EventHandler(this.btnSearchPatientClaim_MouseHover);
            // 
            // txtPatientSearch
            // 
            this.txtPatientSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPatientSearch.ForeColor = System.Drawing.Color.Black;
            this.txtPatientSearch.Location = new System.Drawing.Point(106, 3);
            this.txtPatientSearch.MaxLength = 15;
            this.txtPatientSearch.Name = "txtPatientSearch";
            this.txtPatientSearch.Size = new System.Drawing.Size(141, 22);
            this.txtPatientSearch.TabIndex = 0;
            this.txtPatientSearch.Visible = false;
            this.txtPatientSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatientSearch_KeyPress);
            this.txtPatientSearch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtPatientSearch_MouseUp);
            // 
            // lblSearchonClaimNo
            // 
            this.lblSearchonClaimNo.AutoSize = true;
            this.lblSearchonClaimNo.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchonClaimNo.ForeColor = System.Drawing.Color.Black;
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
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.Transparent;
            this.pnlButton.Controls.Add(this.btn_ModityPatient);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButton.ForeColor = System.Drawing.Color.Black;
            this.pnlButton.Location = new System.Drawing.Point(1171, 1);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(28, 25);
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
            this.btn_ModityPatient.Location = new System.Drawing.Point(5, 0);
            this.btn_ModityPatient.Name = "btn_ModityPatient";
            this.btn_ModityPatient.Size = new System.Drawing.Size(23, 25);
            this.btn_ModityPatient.TabIndex = 2;
            this.btn_ModityPatient.TabStop = false;
            this.btn_ModityPatient.UseVisualStyleBackColor = false;
            this.btn_ModityPatient.Visible = false;
            this.btn_ModityPatient.Click += new System.EventHandler(this.btn_ModityPatient_Click);
            this.btn_ModityPatient.MouseLeave += new System.EventHandler(this.btn_ModityPatient_MouseLeave);
            this.btn_ModityPatient.MouseHover += new System.EventHandler(this.btn_ModityPatient_MouseHover);
            // 
            // lbltxtPatientSearchCaption
            // 
            this.lbltxtPatientSearchCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbltxtPatientSearchCaption.AutoEllipsis = true;
            this.lbltxtPatientSearchCaption.AutoSize = true;
            this.lbltxtPatientSearchCaption.BackColor = System.Drawing.Color.Transparent;
            this.lbltxtPatientSearchCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtPatientSearchCaption.ForeColor = System.Drawing.Color.Black;
            this.lbltxtPatientSearchCaption.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbltxtPatientSearchCaption.Location = new System.Drawing.Point(4, 3);
            this.lbltxtPatientSearchCaption.MaximumSize = new System.Drawing.Size(101, 18);
            this.lbltxtPatientSearchCaption.MinimumSize = new System.Drawing.Size(101, 18);
            this.lbltxtPatientSearchCaption.Name = "lbltxtPatientSearchCaption";
            this.lbltxtPatientSearchCaption.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lbltxtPatientSearchCaption.Size = new System.Drawing.Size(101, 18);
            this.lbltxtPatientSearchCaption.TabIndex = 20;
            this.lbltxtPatientSearchCaption.Text = "Patient Details";
            this.lbltxtPatientSearchCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPnlHeaderTop
            // 
            this.lblPnlHeaderTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblPnlHeaderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPnlHeaderTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPnlHeaderTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPnlHeaderTop.Location = new System.Drawing.Point(0, 0);
            this.lblPnlHeaderTop.Name = "lblPnlHeaderTop";
            this.lblPnlHeaderTop.Size = new System.Drawing.Size(1199, 1);
            this.lblPnlHeaderTop.TabIndex = 24;
            // 
            // lblPnlHeaderMiddle
            // 
            this.lblPnlHeaderMiddle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblPnlHeaderMiddle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPnlHeaderMiddle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPnlHeaderMiddle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPnlHeaderMiddle.Location = new System.Drawing.Point(0, 26);
            this.lblPnlHeaderMiddle.Name = "lblPnlHeaderMiddle";
            this.lblPnlHeaderMiddle.Size = new System.Drawing.Size(1199, 1);
            this.lblPnlHeaderMiddle.TabIndex = 58;
            // 
            // btnEditAccount
            // 
            this.btnEditAccount.AutoEllipsis = true;
            this.btnEditAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnEditAccount.BackgroundImage = global::gloStrips.Properties.Resources.Img_ButtonHover;
            this.btnEditAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditAccount.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnEditAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditAccount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditAccount.Image = ((System.Drawing.Image)(resources.GetObject("btnEditAccount.Image")));
            this.btnEditAccount.Location = new System.Drawing.Point(268, 3);
            this.btnEditAccount.Name = "btnEditAccount";
            this.btnEditAccount.Size = new System.Drawing.Size(22, 22);
            this.btnEditAccount.TabIndex = 75;
            this.btnEditAccount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.btnEditAccount, "Edit Patient Account");
            this.btnEditAccount.UseVisualStyleBackColor = false;
            this.btnEditAccount.Click += new System.EventHandler(this.btnEditAccount_Click);
            this.btnEditAccount.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnEditAccount.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // btn_Alerts
            // 
            this.btn_Alerts.BackColor = System.Drawing.Color.Transparent;
            this.btn_Alerts.BackgroundImage = global::gloStrips.Properties.Resources.Img_ButtonHover;
            this.btn_Alerts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Alerts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_Alerts.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Alerts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Alerts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Alerts.Image = ((System.Drawing.Image)(resources.GetObject("btn_Alerts.Image")));
            this.btn_Alerts.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Alerts.Location = new System.Drawing.Point(292, 30);
            this.btn_Alerts.Name = "btn_Alerts";
            this.btn_Alerts.Size = new System.Drawing.Size(22, 22);
            this.btn_Alerts.TabIndex = 92;
            this.btn_Alerts.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.toolTip1.SetToolTip(this.btn_Alerts, "Modify Alerts");
            this.btn_Alerts.UseVisualStyleBackColor = false;
            this.btn_Alerts.Click += new System.EventHandler(this.btn_Alerts_Click);
            this.btn_Alerts.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btn_Alerts.MouseHover += new System.EventHandler(this.btnMouseHover);
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
            this.pnlClaimSearch.Location = new System.Drawing.Point(1, 0);
            this.pnlClaimSearch.Name = "pnlClaimSearch";
            this.pnlClaimSearch.Size = new System.Drawing.Size(1199, 175);
            this.pnlClaimSearch.TabIndex = 7;
            this.pnlClaimSearch.Visible = false;
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Location = new System.Drawing.Point(1, 174);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(1197, 1);
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
            this.label55.Size = new System.Drawing.Size(1197, 1);
            this.label55.TabIndex = 64;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Right;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Location = new System.Drawing.Point(1198, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(1, 175);
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
            this.label47.Size = new System.Drawing.Size(1, 175);
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
            this.c1SplitClaims.Size = new System.Drawing.Size(1199, 175);
            this.c1SplitClaims.StyleInfo = resources.GetString("c1SplitClaims.StyleInfo");
            this.c1SplitClaims.TabIndex = 55;
            this.c1SplitClaims.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1SplitClaims.Tree.NodeImageCollapsed")));
            this.c1SplitClaims.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1SplitClaims.Tree.NodeImageExpanded")));
            this.c1SplitClaims.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1SplitClaims_KeyPress);
            this.c1SplitClaims.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1SplitClaims_MouseDoubleClick);
            this.c1SplitClaims.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1SplitClaims_MouseMove);
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
            this.c1PatientDetails.Location = new System.Drawing.Point(1, 0);
            this.c1PatientDetails.Name = "c1PatientDetails";
            this.c1PatientDetails.Rows.Count = 5;
            this.c1PatientDetails.Rows.DefaultSize = 19;
            this.c1PatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientDetails.ShowCellLabels = true;
            this.c1PatientDetails.Size = new System.Drawing.Size(1199, 175);
            this.c1PatientDetails.StyleInfo = resources.GetString("c1PatientDetails.StyleInfo");
            this.c1PatientDetails.TabIndex = 57;
            this.c1PatientDetails.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1PatientDetails.Tree.NodeImageCollapsed")));
            this.c1PatientDetails.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1PatientDetails.Tree.NodeImageExpanded")));
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.pnlRight);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Controls.Add(this.lblucPatientStripBottom);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(1, 27);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1199, 148);
            this.pnlMain.TabIndex = 58;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.Transparent;
            this.pnlRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlRight.BackgroundImage")));
            this.pnlRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRight.Controls.Add(this.pnlInsurace);
            this.pnlRight.Controls.Add(this.pnlBalance);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(621, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(578, 147);
            this.pnlRight.TabIndex = 70;
            // 
            // pnlInsurace
            // 
            this.pnlInsurace.Controls.Add(this.lblpnlInsuraceTop);
            this.pnlInsurace.Controls.Add(this.lblpnlInsuraceBottom);
            this.pnlInsurace.Controls.Add(this.lblpnlInsuraceRight);
            this.pnlInsurace.Controls.Add(this.lblpnlInsuraceLeft);
            this.pnlInsurace.Controls.Add(this.c1Insurance);
            this.pnlInsurace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInsurace.Location = new System.Drawing.Point(240, 0);
            this.pnlInsurace.Name = "pnlInsurace";
            this.pnlInsurace.Size = new System.Drawing.Size(338, 147);
            this.pnlInsurace.TabIndex = 101;
            // 
            // lblpnlInsuraceTop
            // 
            this.lblpnlInsuraceTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblpnlInsuraceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblpnlInsuraceTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpnlInsuraceTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblpnlInsuraceTop.Location = new System.Drawing.Point(1, 0);
            this.lblpnlInsuraceTop.Name = "lblpnlInsuraceTop";
            this.lblpnlInsuraceTop.Size = new System.Drawing.Size(336, 1);
            this.lblpnlInsuraceTop.TabIndex = 65;
            this.lblpnlInsuraceTop.Visible = false;
            // 
            // lblpnlInsuraceBottom
            // 
            this.lblpnlInsuraceBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblpnlInsuraceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblpnlInsuraceBottom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpnlInsuraceBottom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblpnlInsuraceBottom.Location = new System.Drawing.Point(1, 146);
            this.lblpnlInsuraceBottom.Name = "lblpnlInsuraceBottom";
            this.lblpnlInsuraceBottom.Size = new System.Drawing.Size(336, 1);
            this.lblpnlInsuraceBottom.TabIndex = 64;
            this.lblpnlInsuraceBottom.Visible = false;
            // 
            // lblpnlInsuraceRight
            // 
            this.lblpnlInsuraceRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblpnlInsuraceRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblpnlInsuraceRight.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpnlInsuraceRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblpnlInsuraceRight.Location = new System.Drawing.Point(337, 0);
            this.lblpnlInsuraceRight.Name = "lblpnlInsuraceRight";
            this.lblpnlInsuraceRight.Size = new System.Drawing.Size(1, 147);
            this.lblpnlInsuraceRight.TabIndex = 62;
            this.lblpnlInsuraceRight.Visible = false;
            // 
            // lblpnlInsuraceLeft
            // 
            this.lblpnlInsuraceLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblpnlInsuraceLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblpnlInsuraceLeft.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpnlInsuraceLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblpnlInsuraceLeft.Location = new System.Drawing.Point(0, 0);
            this.lblpnlInsuraceLeft.Name = "lblpnlInsuraceLeft";
            this.lblpnlInsuraceLeft.Size = new System.Drawing.Size(1, 147);
            this.lblpnlInsuraceLeft.TabIndex = 61;
            // 
            // c1Insurance
            // 
            this.c1Insurance.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Insurance.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Insurance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Insurance.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Insurance.ColumnInfo = "1,0,0,0,0,105,Columns:";
            this.c1Insurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Insurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Insurance.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1Insurance.Location = new System.Drawing.Point(0, 0);
            this.c1Insurance.Name = "c1Insurance";
            this.c1Insurance.Padding = new System.Windows.Forms.Padding(3);
            this.c1Insurance.Rows.Count = 1;
            this.c1Insurance.Rows.DefaultSize = 21;
            this.c1Insurance.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1Insurance.ShowErrors = true;
            this.c1Insurance.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None;
            this.c1Insurance.Size = new System.Drawing.Size(338, 147);
            this.c1Insurance.StyleInfo = resources.GetString("c1Insurance.StyleInfo");
            this.c1Insurance.TabIndex = 60;
            this.c1Insurance.TabStop = false;
            this.c1Insurance.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_BeforeEdit);
            this.c1Insurance.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Insurance_AfterEdit);
            this.c1Insurance.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1Insurance_MouseDown);
            this.c1Insurance.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Insurance_MouseMove);
            // 
            // pnlBalance
            // 
            this.pnlBalance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlBalance.BackgroundImage")));
            this.pnlBalance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlBalance.Controls.Add(this.pnlReserve);
            this.pnlBalance.Controls.Add(this.pnlBadDebt);
            this.pnlBalance.Controls.Add(this.pnlTotalBal);
            this.pnlBalance.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBalance.ForeColor = System.Drawing.Color.Black;
            this.pnlBalance.Location = new System.Drawing.Point(0, 0);
            this.pnlBalance.Name = "pnlBalance";
            this.pnlBalance.Size = new System.Drawing.Size(240, 147);
            this.pnlBalance.TabIndex = 69;
            // 
            // pnlReserve
            // 
            this.pnlReserve.Controls.Add(this.lblBalTotalLine);
            this.pnlReserve.Controls.Add(this.lblBalOtherReserve);
            this.pnlReserve.Controls.Add(this.lblBalAdvancedReserve);
            this.pnlReserve.Controls.Add(this.lblBalCopayReserveCaption);
            this.pnlReserve.Controls.Add(this.lblBalCopayReserve);
            this.pnlReserve.Controls.Add(this.lblBalAdvancedReserveCaption);
            this.pnlReserve.Controls.Add(this.lblBalOtherReserveCaption);
            this.pnlReserve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReserve.Location = new System.Drawing.Point(0, 75);
            this.pnlReserve.Name = "pnlReserve";
            this.pnlReserve.Size = new System.Drawing.Size(240, 72);
            this.pnlReserve.TabIndex = 66;
            // 
            // lblBalTotalLine
            // 
            this.lblBalTotalLine.BackColor = System.Drawing.Color.Green;
            this.lblBalTotalLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalTotalLine.ForeColor = System.Drawing.Color.Fuchsia;
            this.lblBalTotalLine.Location = new System.Drawing.Point(17, 0);
            this.lblBalTotalLine.Name = "lblBalTotalLine";
            this.lblBalTotalLine.Size = new System.Drawing.Size(222, 2);
            this.lblBalTotalLine.TabIndex = 57;
            // 
            // lblBalOtherReserve
            // 
            this.lblBalOtherReserve.AutoEllipsis = true;
            this.lblBalOtherReserve.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBalOtherReserve.Location = new System.Drawing.Point(132, 42);
            this.lblBalOtherReserve.Name = "lblBalOtherReserve";
            this.lblBalOtherReserve.Size = new System.Drawing.Size(105, 13);
            this.lblBalOtherReserve.TabIndex = 63;
            this.lblBalOtherReserve.Text = "$999.99";
            this.lblBalOtherReserve.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalAdvancedReserve
            // 
            this.lblBalAdvancedReserve.AutoEllipsis = true;
            this.lblBalAdvancedReserve.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBalAdvancedReserve.Location = new System.Drawing.Point(132, 24);
            this.lblBalAdvancedReserve.Name = "lblBalAdvancedReserve";
            this.lblBalAdvancedReserve.Size = new System.Drawing.Size(105, 13);
            this.lblBalAdvancedReserve.TabIndex = 62;
            this.lblBalAdvancedReserve.Text = "$999.99";
            this.lblBalAdvancedReserve.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalCopayReserveCaption
            // 
            this.lblBalCopayReserveCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBalCopayReserveCaption.AutoSize = true;
            this.lblBalCopayReserveCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblBalCopayReserveCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalCopayReserveCaption.ForeColor = System.Drawing.Color.Black;
            this.lblBalCopayReserveCaption.Location = new System.Drawing.Point(30, 5);
            this.lblBalCopayReserveCaption.Name = "lblBalCopayReserveCaption";
            this.lblBalCopayReserveCaption.Size = new System.Drawing.Size(95, 14);
            this.lblBalCopayReserveCaption.TabIndex = 0;
            this.lblBalCopayReserveCaption.Text = "Copay Reserve :";
            // 
            // lblBalCopayReserve
            // 
            this.lblBalCopayReserve.AutoEllipsis = true;
            this.lblBalCopayReserve.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBalCopayReserve.Location = new System.Drawing.Point(132, 6);
            this.lblBalCopayReserve.Name = "lblBalCopayReserve";
            this.lblBalCopayReserve.Size = new System.Drawing.Size(105, 13);
            this.lblBalCopayReserve.TabIndex = 61;
            this.lblBalCopayReserve.Text = "$999.99";
            this.lblBalCopayReserve.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalAdvancedReserveCaption
            // 
            this.lblBalAdvancedReserveCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBalAdvancedReserveCaption.AutoSize = true;
            this.lblBalAdvancedReserveCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblBalAdvancedReserveCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalAdvancedReserveCaption.ForeColor = System.Drawing.Color.Black;
            this.lblBalAdvancedReserveCaption.Location = new System.Drawing.Point(9, 23);
            this.lblBalAdvancedReserveCaption.Name = "lblBalAdvancedReserveCaption";
            this.lblBalAdvancedReserveCaption.Size = new System.Drawing.Size(116, 14);
            this.lblBalAdvancedReserveCaption.TabIndex = 0;
            this.lblBalAdvancedReserveCaption.Text = "Advanced Reserve :";
            // 
            // lblBalOtherReserveCaption
            // 
            this.lblBalOtherReserveCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBalOtherReserveCaption.AutoSize = true;
            this.lblBalOtherReserveCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblBalOtherReserveCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalOtherReserveCaption.ForeColor = System.Drawing.Color.Black;
            this.lblBalOtherReserveCaption.Location = new System.Drawing.Point(31, 41);
            this.lblBalOtherReserveCaption.Name = "lblBalOtherReserveCaption";
            this.lblBalOtherReserveCaption.Size = new System.Drawing.Size(94, 14);
            this.lblBalOtherReserveCaption.TabIndex = 0;
            this.lblBalOtherReserveCaption.Text = "Other Reserve :";
            // 
            // pnlBadDebt
            // 
            this.pnlBadDebt.Controls.Add(this.lblBadDebtSatus);
            this.pnlBadDebt.Controls.Add(this.lblBalBadDebtCaption);
            this.pnlBadDebt.Controls.Add(this.lblBalBadDebt);
            this.pnlBadDebt.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBadDebt.Location = new System.Drawing.Point(0, 54);
            this.pnlBadDebt.Name = "pnlBadDebt";
            this.pnlBadDebt.Size = new System.Drawing.Size(240, 21);
            this.pnlBadDebt.TabIndex = 64;
            this.pnlBadDebt.Visible = false;
            // 
            // lblBadDebtSatus
            // 
            this.lblBadDebtSatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBadDebtSatus.AutoSize = true;
            this.lblBadDebtSatus.BackColor = System.Drawing.Color.Transparent;
            this.lblBadDebtSatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBadDebtSatus.ForeColor = System.Drawing.Color.Red;
            this.lblBadDebtSatus.Location = new System.Drawing.Point(56, 3);
            this.lblBadDebtSatus.Name = "lblBadDebtSatus";
            this.lblBadDebtSatus.Size = new System.Drawing.Size(61, 13);
            this.lblBadDebtSatus.TabIndex = 95;
            this.lblBadDebtSatus.Text = "BAD DEBT";
            this.lblBadDebtSatus.Visible = false;
            // 
            // lblBalBadDebtCaption
            // 
            this.lblBalBadDebtCaption.AutoSize = true;
            this.lblBalBadDebtCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblBalBadDebtCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalBadDebtCaption.ForeColor = System.Drawing.Color.Black;
            this.lblBalBadDebtCaption.Location = new System.Drawing.Point(59, 2);
            this.lblBalBadDebtCaption.Name = "lblBalBadDebtCaption";
            this.lblBalBadDebtCaption.Size = new System.Drawing.Size(66, 14);
            this.lblBalBadDebtCaption.TabIndex = 59;
            this.lblBalBadDebtCaption.Text = "Bad Debt :";
            // 
            // lblBalBadDebt
            // 
            this.lblBalBadDebt.AutoEllipsis = true;
            this.lblBalBadDebt.AutoSize = true;
            this.lblBalBadDebt.BackColor = System.Drawing.Color.Transparent;
            this.lblBalBadDebt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalBadDebt.ForeColor = System.Drawing.Color.Black;
            this.lblBalBadDebt.Location = new System.Drawing.Point(113, -1);
            this.lblBalBadDebt.MaximumSize = new System.Drawing.Size(124, 20);
            this.lblBalBadDebt.MinimumSize = new System.Drawing.Size(124, 20);
            this.lblBalBadDebt.Name = "lblBalBadDebt";
            this.lblBalBadDebt.Size = new System.Drawing.Size(124, 20);
            this.lblBalBadDebt.TabIndex = 58;
            this.lblBalBadDebt.Text = "$999.99";
            this.lblBalBadDebt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlTotalBal
            // 
            this.pnlTotalBal.Controls.Add(this.lblBalPatientDueCaption);
            this.pnlTotalBal.Controls.Add(this.lblBalTotalBalanceCaption);
            this.pnlTotalBal.Controls.Add(this.lblBalPatientDue);
            this.pnlTotalBal.Controls.Add(this.lblBalInsurancePendingCaption);
            this.pnlTotalBal.Controls.Add(this.lblBalInsurancePending);
            this.pnlTotalBal.Controls.Add(this.lblBalTotalBalance);
            this.pnlTotalBal.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTotalBal.Location = new System.Drawing.Point(0, 0);
            this.pnlTotalBal.Name = "pnlTotalBal";
            this.pnlTotalBal.Size = new System.Drawing.Size(240, 54);
            this.pnlTotalBal.TabIndex = 66;
            // 
            // lblBalPatientDueCaption
            // 
            this.lblBalPatientDueCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBalPatientDueCaption.AutoSize = true;
            this.lblBalPatientDueCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblBalPatientDueCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalPatientDueCaption.ForeColor = System.Drawing.Color.Black;
            this.lblBalPatientDueCaption.Location = new System.Drawing.Point(45, 38);
            this.lblBalPatientDueCaption.Name = "lblBalPatientDueCaption";
            this.lblBalPatientDueCaption.Size = new System.Drawing.Size(80, 14);
            this.lblBalPatientDueCaption.TabIndex = 0;
            this.lblBalPatientDueCaption.Text = "Patient Due :";
            // 
            // lblBalTotalBalanceCaption
            // 
            this.lblBalTotalBalanceCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBalTotalBalanceCaption.AutoSize = true;
            this.lblBalTotalBalanceCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblBalTotalBalanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalTotalBalanceCaption.ForeColor = System.Drawing.Color.Black;
            this.lblBalTotalBalanceCaption.Location = new System.Drawing.Point(37, 2);
            this.lblBalTotalBalanceCaption.Name = "lblBalTotalBalanceCaption";
            this.lblBalTotalBalanceCaption.Size = new System.Drawing.Size(88, 14);
            this.lblBalTotalBalanceCaption.TabIndex = 0;
            this.lblBalTotalBalanceCaption.Text = "Total Balance :";
            // 
            // lblBalPatientDue
            // 
            this.lblBalPatientDue.AutoEllipsis = true;
            this.lblBalPatientDue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBalPatientDue.Location = new System.Drawing.Point(133, 38);
            this.lblBalPatientDue.Name = "lblBalPatientDue";
            this.lblBalPatientDue.Size = new System.Drawing.Size(105, 13);
            this.lblBalPatientDue.TabIndex = 60;
            this.lblBalPatientDue.Text = "$999.99";
            this.lblBalPatientDue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalInsurancePendingCaption
            // 
            this.lblBalInsurancePendingCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBalInsurancePendingCaption.AutoSize = true;
            this.lblBalInsurancePendingCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblBalInsurancePendingCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalInsurancePendingCaption.ForeColor = System.Drawing.Color.Black;
            this.lblBalInsurancePendingCaption.Location = new System.Drawing.Point(42, 20);
            this.lblBalInsurancePendingCaption.Name = "lblBalInsurancePendingCaption";
            this.lblBalInsurancePendingCaption.Size = new System.Drawing.Size(83, 14);
            this.lblBalInsurancePendingCaption.TabIndex = 0;
            this.lblBalInsurancePendingCaption.Text = "Ins. Pending :";
            // 
            // lblBalInsurancePending
            // 
            this.lblBalInsurancePending.AutoEllipsis = true;
            this.lblBalInsurancePending.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBalInsurancePending.Location = new System.Drawing.Point(133, 20);
            this.lblBalInsurancePending.Name = "lblBalInsurancePending";
            this.lblBalInsurancePending.Size = new System.Drawing.Size(105, 13);
            this.lblBalInsurancePending.TabIndex = 59;
            this.lblBalInsurancePending.Text = "$999.99";
            this.lblBalInsurancePending.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalTotalBalance
            // 
            this.lblBalTotalBalance.AutoEllipsis = true;
            this.lblBalTotalBalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBalTotalBalance.Location = new System.Drawing.Point(133, 2);
            this.lblBalTotalBalance.Name = "lblBalTotalBalance";
            this.lblBalTotalBalance.Size = new System.Drawing.Size(105, 13);
            this.lblBalTotalBalance.TabIndex = 58;
            this.lblBalTotalBalance.Text = "$999.99";
            this.lblBalTotalBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLeft.BackgroundImage")));
            this.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLeft.Controls.Add(this.panel3);
            this.pnlLeft.Controls.Add(this.pnlPatientPhoto);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(621, 147);
            this.pnlLeft.TabIndex = 68;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(110, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(511, 147);
            this.panel3.TabIndex = 90;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Controls.Add(this.pnlAccountDetails);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(320, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(191, 147);
            this.panel6.TabIndex = 75;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.pnlAcctNote);
            this.panel7.Controls.Add(this.panel9);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 42);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(191, 105);
            this.panel7.TabIndex = 4;
            // 
            // pnlAcctNote
            // 
            this.pnlAcctNote.Controls.Add(this.lblAccountNotes);
            this.pnlAcctNote.Controls.Add(this.lblAccountNotesCaption);
            this.pnlAcctNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAcctNote.Location = new System.Drawing.Point(0, 76);
            this.pnlAcctNote.Name = "pnlAcctNote";
            this.pnlAcctNote.Size = new System.Drawing.Size(191, 29);
            this.pnlAcctNote.TabIndex = 92;
            this.pnlAcctNote.Visible = false;
            // 
            // lblAccountNotes
            // 
            this.lblAccountNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountNotes.AutoEllipsis = true;
            this.lblAccountNotes.AutoSize = true;
            this.lblAccountNotes.BackColor = System.Drawing.Color.Transparent;
            this.lblAccountNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountNotes.ForeColor = System.Drawing.Color.Black;
            this.lblAccountNotes.Location = new System.Drawing.Point(93, 3);
            this.lblAccountNotes.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblAccountNotes.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblAccountNotes.Name = "lblAccountNotes";
            this.lblAccountNotes.Size = new System.Drawing.Size(200, 14);
            this.lblAccountNotes.TabIndex = 91;
            this.lblAccountNotes.Text = "Acc Note";
            // 
            // lblAccountNotesCaption
            // 
            this.lblAccountNotesCaption.AutoSize = true;
            this.lblAccountNotesCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblAccountNotesCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountNotesCaption.ForeColor = System.Drawing.Color.Black;
            this.lblAccountNotesCaption.Location = new System.Drawing.Point(16, 3);
            this.lblAccountNotesCaption.Name = "lblAccountNotesCaption";
            this.lblAccountNotesCaption.Size = new System.Drawing.Size(75, 14);
            this.lblAccountNotesCaption.TabIndex = 90;
            this.lblAccountNotesCaption.Text = "Acct. Note :";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.pnlEMRAlertsCaption);
            this.panel9.Controls.Add(this.lblDemoNextAppt);
            this.panel9.Controls.Add(this.lblDemoNextApptCaption);
            this.panel9.Controls.Add(this.lblDemoLastPatPayment);
            this.panel9.Controls.Add(this.lblDemoGuarantorCaption);
            this.panel9.Controls.Add(this.lblDemoGuarantor);
            this.panel9.Controls.Add(this.lblDemoPatientPayment);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(191, 76);
            this.panel9.TabIndex = 93;
            // 
            // pnlEMRAlertsCaption
            // 
            this.pnlEMRAlertsCaption.Controls.Add(this.lblDemoEMRAlerts);
            this.pnlEMRAlertsCaption.Controls.Add(this.lblDemogloEMRAlertsCaption);
            this.pnlEMRAlertsCaption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEMRAlertsCaption.Location = new System.Drawing.Point(0, 56);
            this.pnlEMRAlertsCaption.Name = "pnlEMRAlertsCaption";
            this.pnlEMRAlertsCaption.Size = new System.Drawing.Size(191, 20);
            this.pnlEMRAlertsCaption.TabIndex = 97;
            // 
            // lblDemoEMRAlerts
            // 
            this.lblDemoEMRAlerts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoEMRAlerts.AutoEllipsis = true;
            this.lblDemoEMRAlerts.AutoSize = true;
            this.lblDemoEMRAlerts.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoEMRAlerts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoEMRAlerts.ForeColor = System.Drawing.Color.Black;
            this.lblDemoEMRAlerts.Location = new System.Drawing.Point(93, 2);
            this.lblDemoEMRAlerts.MaximumSize = new System.Drawing.Size(235, 14);
            this.lblDemoEMRAlerts.MinimumSize = new System.Drawing.Size(235, 14);
            this.lblDemoEMRAlerts.Name = "lblDemoEMRAlerts";
            this.lblDemoEMRAlerts.Size = new System.Drawing.Size(235, 14);
            this.lblDemoEMRAlerts.TabIndex = 93;
            // 
            // lblDemogloEMRAlertsCaption
            // 
            this.lblDemogloEMRAlertsCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemogloEMRAlertsCaption.AutoSize = true;
            this.lblDemogloEMRAlertsCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemogloEMRAlertsCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemogloEMRAlertsCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemogloEMRAlertsCaption.Location = new System.Drawing.Point(18, 2);
            this.lblDemogloEMRAlertsCaption.Name = "lblDemogloEMRAlertsCaption";
            this.lblDemogloEMRAlertsCaption.Size = new System.Drawing.Size(73, 14);
            this.lblDemogloEMRAlertsCaption.TabIndex = 91;
            this.lblDemogloEMRAlertsCaption.Text = "EMR Alerts :";
            // 
            // lblDemoNextAppt
            // 
            this.lblDemoNextAppt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoNextAppt.AutoSize = true;
            this.lblDemoNextAppt.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoNextAppt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoNextAppt.ForeColor = System.Drawing.Color.Black;
            this.lblDemoNextAppt.Location = new System.Drawing.Point(15, 38);
            this.lblDemoNextAppt.Name = "lblDemoNextAppt";
            this.lblDemoNextAppt.Size = new System.Drawing.Size(76, 14);
            this.lblDemoNextAppt.TabIndex = 95;
            this.lblDemoNextAppt.Text = "Next Appt. :";
            // 
            // lblDemoNextApptCaption
            // 
            this.lblDemoNextApptCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoNextApptCaption.AutoSize = true;
            this.lblDemoNextApptCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoNextApptCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoNextApptCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoNextApptCaption.Location = new System.Drawing.Point(93, 38);
            this.lblDemoNextApptCaption.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblDemoNextApptCaption.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblDemoNextApptCaption.Name = "lblDemoNextApptCaption";
            this.lblDemoNextApptCaption.Size = new System.Drawing.Size(200, 14);
            this.lblDemoNextApptCaption.TabIndex = 96;
            // 
            // lblDemoLastPatPayment
            // 
            this.lblDemoLastPatPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoLastPatPayment.AutoSize = true;
            this.lblDemoLastPatPayment.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoLastPatPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoLastPatPayment.ForeColor = System.Drawing.Color.Black;
            this.lblDemoLastPatPayment.Location = new System.Drawing.Point(93, 21);
            this.lblDemoLastPatPayment.MaximumSize = new System.Drawing.Size(195, 14);
            this.lblDemoLastPatPayment.MinimumSize = new System.Drawing.Size(195, 14);
            this.lblDemoLastPatPayment.Name = "lblDemoLastPatPayment";
            this.lblDemoLastPatPayment.Size = new System.Drawing.Size(195, 14);
            this.lblDemoLastPatPayment.TabIndex = 89;
            this.lblDemoLastPatPayment.Text = "Last Pat. Pmt";
            // 
            // lblDemoGuarantorCaption
            // 
            this.lblDemoGuarantorCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoGuarantorCaption.AutoSize = true;
            this.lblDemoGuarantorCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoGuarantorCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoGuarantorCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoGuarantorCaption.Location = new System.Drawing.Point(22, 4);
            this.lblDemoGuarantorCaption.Name = "lblDemoGuarantorCaption";
            this.lblDemoGuarantorCaption.Size = new System.Drawing.Size(69, 14);
            this.lblDemoGuarantorCaption.TabIndex = 4;
            this.lblDemoGuarantorCaption.Text = "Guarantor :";
            // 
            // lblDemoGuarantor
            // 
            this.lblDemoGuarantor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoGuarantor.AutoSize = true;
            this.lblDemoGuarantor.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoGuarantor.ForeColor = System.Drawing.Color.Black;
            this.lblDemoGuarantor.Location = new System.Drawing.Point(93, 4);
            this.lblDemoGuarantor.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblDemoGuarantor.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblDemoGuarantor.Name = "lblDemoGuarantor";
            this.lblDemoGuarantor.Size = new System.Drawing.Size(200, 14);
            this.lblDemoGuarantor.TabIndex = 6;
            this.lblDemoGuarantor.Text = "Guarantor";
            // 
            // lblDemoPatientPayment
            // 
            this.lblDemoPatientPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoPatientPayment.AutoSize = true;
            this.lblDemoPatientPayment.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoPatientPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoPatientPayment.ForeColor = System.Drawing.Color.Black;
            this.lblDemoPatientPayment.Location = new System.Drawing.Point(2, 21);
            this.lblDemoPatientPayment.Name = "lblDemoPatientPayment";
            this.lblDemoPatientPayment.Size = new System.Drawing.Size(89, 14);
            this.lblDemoPatientPayment.TabIndex = 5;
            this.lblDemoPatientPayment.Text = "Last Pat. Pmt :";
            // 
            // pnlAccountDetails
            // 
            this.pnlAccountDetails.Controls.Add(this.lblDemoAccountNoCaption);
            this.pnlAccountDetails.Controls.Add(this.lblDemoAccountNo);
            this.pnlAccountDetails.Controls.Add(this.btnEditAccount);
            this.pnlAccountDetails.Controls.Add(this.lblAccountDesc);
            this.pnlAccountDetails.Controls.Add(this.label5);
            this.pnlAccountDetails.Controls.Add(this.lblDemoAccountDesc);
            this.pnlAccountDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAccountDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlAccountDetails.Name = "pnlAccountDetails";
            this.pnlAccountDetails.Size = new System.Drawing.Size(191, 42);
            this.pnlAccountDetails.TabIndex = 0;
            // 
            // lblDemoAccountNoCaption
            // 
            this.lblDemoAccountNoCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoAccountNoCaption.AutoSize = true;
            this.lblDemoAccountNoCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoAccountNoCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoAccountNoCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoAccountNoCaption.Location = new System.Drawing.Point(30, 7);
            this.lblDemoAccountNoCaption.Name = "lblDemoAccountNoCaption";
            this.lblDemoAccountNoCaption.Size = new System.Drawing.Size(61, 14);
            this.lblDemoAccountNoCaption.TabIndex = 4;
            this.lblDemoAccountNoCaption.Text = "Account :";
            // 
            // lblDemoAccountNo
            // 
            this.lblDemoAccountNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoAccountNo.AutoSize = true;
            this.lblDemoAccountNo.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoAccountNo.ForeColor = System.Drawing.Color.Black;
            this.lblDemoAccountNo.Location = new System.Drawing.Point(93, 7);
            this.lblDemoAccountNo.MaximumSize = new System.Drawing.Size(170, 14);
            this.lblDemoAccountNo.MinimumSize = new System.Drawing.Size(170, 14);
            this.lblDemoAccountNo.Name = "lblDemoAccountNo";
            this.lblDemoAccountNo.Size = new System.Drawing.Size(170, 14);
            this.lblDemoAccountNo.TabIndex = 8;
            this.lblDemoAccountNo.Text = "AccNo.";
            // 
            // lblAccountDesc
            // 
            this.lblAccountDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountDesc.AutoSize = true;
            this.lblAccountDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblAccountDesc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountDesc.ForeColor = System.Drawing.Color.Black;
            this.lblAccountDesc.Location = new System.Drawing.Point(93, 26);
            this.lblAccountDesc.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblAccountDesc.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblAccountDesc.Name = "lblAccountDesc";
            this.lblAccountDesc.Size = new System.Drawing.Size(200, 14);
            this.lblAccountDesc.TabIndex = 8;
            this.lblAccountDesc.Text = "Acc Desc";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(171, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 14);
            this.label5.TabIndex = 7;
            // 
            // lblDemoAccountDesc
            // 
            this.lblDemoAccountDesc.AutoSize = true;
            this.lblDemoAccountDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoAccountDesc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoAccountDesc.ForeColor = System.Drawing.Color.Black;
            this.lblDemoAccountDesc.Location = new System.Drawing.Point(13, 26);
            this.lblDemoAccountDesc.Name = "lblDemoAccountDesc";
            this.lblDemoAccountDesc.Size = new System.Drawing.Size(78, 14);
            this.lblDemoAccountDesc.TabIndex = 5;
            this.lblDemoAccountDesc.Text = "Acct. Desc. :";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pnlNotesAlerts);
            this.panel4.Controls.Add(this.pnlMedCategory);
            this.panel4.Controls.Add(this.pnlPatient);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(320, 147);
            this.panel4.TabIndex = 74;
            // 
            // pnlNotesAlerts
            // 
            this.pnlNotesAlerts.Controls.Add(this.lblAlerts);
            this.pnlNotesAlerts.Controls.Add(this.panel8);
            this.pnlNotesAlerts.Controls.Add(this.lblAlertsCap);
            this.pnlNotesAlerts.Controls.Add(this.btn_Alerts);
            this.pnlNotesAlerts.Controls.Add(this.lblNotesCaption);
            this.pnlNotesAlerts.Controls.Add(this.lblDemoCopay);
            this.pnlNotesAlerts.Controls.Add(this.lblDemoProvider);
            this.pnlNotesAlerts.Controls.Add(this.lblDemoNotes);
            this.pnlNotesAlerts.Controls.Add(this.lblDemoCopayCaption);
            this.pnlNotesAlerts.Controls.Add(this.lblDemoProviderCaption);
            this.pnlNotesAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNotesAlerts.Location = new System.Drawing.Point(0, 41);
            this.pnlNotesAlerts.Name = "pnlNotesAlerts";
            this.pnlNotesAlerts.Size = new System.Drawing.Size(320, 106);
            this.pnlNotesAlerts.TabIndex = 83;
            // 
            // lblAlerts
            // 
            this.lblAlerts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlerts.AutoEllipsis = true;
            this.lblAlerts.BackColor = System.Drawing.Color.Transparent;
            this.lblAlerts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlerts.ForeColor = System.Drawing.Color.Black;
            this.lblAlerts.Location = new System.Drawing.Point(92, 38);
            this.lblAlerts.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblAlerts.MinimumSize = new System.Drawing.Size(170, 14);
            this.lblAlerts.Name = "lblAlerts";
            this.lblAlerts.Size = new System.Drawing.Size(192, 14);
            this.lblAlerts.TabIndex = 87;
            this.lblAlerts.Text = "Alerts";
            // 
            // panel8
            // 
            this.panel8.AutoScroll = true;
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.Controls.Add(this.lblNotes);
            this.panel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel8.Location = new System.Drawing.Point(91, 55);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(222, 45);
            this.panel8.TabIndex = 94;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.lblNotes.ForeColor = System.Drawing.Color.Black;
            this.lblNotes.Location = new System.Drawing.Point(0, 0);
            this.lblNotes.Margin = new System.Windows.Forms.Padding(0);
            this.lblNotes.MaximumSize = new System.Drawing.Size(202, 0);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(0, 14);
            this.lblNotes.TabIndex = 0;
            // 
            // lblAlertsCap
            // 
            this.lblAlertsCap.AutoSize = true;
            this.lblAlertsCap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertsCap.ForeColor = System.Drawing.Color.Black;
            this.lblAlertsCap.Location = new System.Drawing.Point(3, 38);
            this.lblAlertsCap.MaximumSize = new System.Drawing.Size(90, 14);
            this.lblAlertsCap.MinimumSize = new System.Drawing.Size(90, 14);
            this.lblAlertsCap.Name = "lblAlertsCap";
            this.lblAlertsCap.Size = new System.Drawing.Size(90, 14);
            this.lblAlertsCap.TabIndex = 93;
            this.lblAlertsCap.Text = "PM Alerts (#) :";
            this.lblAlertsCap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNotesCaption
            // 
            this.lblNotesCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotesCaption.AutoSize = true;
            this.lblNotesCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblNotesCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotesCaption.ForeColor = System.Drawing.Color.Black;
            this.lblNotesCaption.Location = new System.Drawing.Point(3, 57);
            this.lblNotesCaption.Name = "lblNotesCaption";
            this.lblNotesCaption.Size = new System.Drawing.Size(90, 14);
            this.lblNotesCaption.TabIndex = 91;
            this.lblNotesCaption.Text = "Patient Notes :";
            // 
            // lblDemoCopay
            // 
            this.lblDemoCopay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoCopay.AutoSize = true;
            this.lblDemoCopay.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoCopay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoCopay.ForeColor = System.Drawing.Color.Black;
            this.lblDemoCopay.Location = new System.Drawing.Point(92, 20);
            this.lblDemoCopay.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblDemoCopay.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblDemoCopay.Name = "lblDemoCopay";
            this.lblDemoCopay.Size = new System.Drawing.Size(200, 14);
            this.lblDemoCopay.TabIndex = 89;
            this.lblDemoCopay.Text = "Copay";
            // 
            // lblDemoProvider
            // 
            this.lblDemoProvider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoProvider.AutoSize = true;
            this.lblDemoProvider.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoProvider.ForeColor = System.Drawing.Color.Black;
            this.lblDemoProvider.Location = new System.Drawing.Point(92, 2);
            this.lblDemoProvider.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblDemoProvider.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblDemoProvider.Name = "lblDemoProvider";
            this.lblDemoProvider.Size = new System.Drawing.Size(200, 14);
            this.lblDemoProvider.TabIndex = 88;
            this.lblDemoProvider.Text = "Provider";
            // 
            // lblDemoNotes
            // 
            this.lblDemoNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoNotes.AutoSize = true;
            this.lblDemoNotes.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblDemoNotes.Location = new System.Drawing.Point(92, 58);
            this.lblDemoNotes.MaximumSize = new System.Drawing.Size(0, 14);
            this.lblDemoNotes.MinimumSize = new System.Drawing.Size(0, 14);
            this.lblDemoNotes.Name = "lblDemoNotes";
            this.lblDemoNotes.Size = new System.Drawing.Size(91, 14);
            this.lblDemoNotes.TabIndex = 85;
            this.lblDemoNotes.Text = "Patient Notes";
            this.lblDemoNotes.Visible = false;
            // 
            // lblDemoCopayCaption
            // 
            this.lblDemoCopayCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoCopayCaption.AutoSize = true;
            this.lblDemoCopayCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoCopayCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoCopayCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoCopayCaption.Location = new System.Drawing.Point(45, 20);
            this.lblDemoCopayCaption.Name = "lblDemoCopayCaption";
            this.lblDemoCopayCaption.Size = new System.Drawing.Size(48, 14);
            this.lblDemoCopayCaption.TabIndex = 86;
            this.lblDemoCopayCaption.Text = "Copay :";
            // 
            // lblDemoProviderCaption
            // 
            this.lblDemoProviderCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoProviderCaption.AutoSize = true;
            this.lblDemoProviderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoProviderCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoProviderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoProviderCaption.Location = new System.Drawing.Point(34, 2);
            this.lblDemoProviderCaption.Name = "lblDemoProviderCaption";
            this.lblDemoProviderCaption.Size = new System.Drawing.Size(59, 14);
            this.lblDemoProviderCaption.TabIndex = 84;
            this.lblDemoProviderCaption.Text = "Provider :";
            // 
            // pnlMedCategory
            // 
            this.pnlMedCategory.Controls.Add(this.lblDemoMedCat);
            this.pnlMedCategory.Controls.Add(this.lblDemoMedCatCaption);
            this.pnlMedCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMedCategory.Location = new System.Drawing.Point(0, 21);
            this.pnlMedCategory.Name = "pnlMedCategory";
            this.pnlMedCategory.Size = new System.Drawing.Size(320, 20);
            this.pnlMedCategory.TabIndex = 99;
            // 
            // lblDemoMedCat
            // 
            this.lblDemoMedCat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoMedCat.AutoSize = true;
            this.lblDemoMedCat.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoMedCat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoMedCat.ForeColor = System.Drawing.Color.Black;
            this.lblDemoMedCat.Location = new System.Drawing.Point(93, 3);
            this.lblDemoMedCat.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblDemoMedCat.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblDemoMedCat.Name = "lblDemoMedCat";
            this.lblDemoMedCat.Size = new System.Drawing.Size(200, 14);
            this.lblDemoMedCat.TabIndex = 96;
            this.lblDemoMedCat.Text = "Medical Category";
            // 
            // lblDemoMedCatCaption
            // 
            this.lblDemoMedCatCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoMedCatCaption.AutoSize = true;
            this.lblDemoMedCatCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoMedCatCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoMedCatCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoMedCatCaption.Location = new System.Drawing.Point(29, 3);
            this.lblDemoMedCatCaption.Name = "lblDemoMedCatCaption";
            this.lblDemoMedCatCaption.Size = new System.Drawing.Size(64, 14);
            this.lblDemoMedCatCaption.TabIndex = 95;
            this.lblDemoMedCatCaption.Text = "Med. Cat :";
            // 
            // pnlPatient
            // 
            this.pnlPatient.Controls.Add(this.lblDemoPatientCaption);
            this.pnlPatient.Controls.Add(this.lblDemoPatient);
            this.pnlPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatient.ForeColor = System.Drawing.Color.Black;
            this.pnlPatient.Location = new System.Drawing.Point(0, 0);
            this.pnlPatient.Name = "pnlPatient";
            this.pnlPatient.Size = new System.Drawing.Size(320, 21);
            this.pnlPatient.TabIndex = 0;
            // 
            // lblDemoPatientCaption
            // 
            this.lblDemoPatientCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoPatientCaption.AutoSize = true;
            this.lblDemoPatientCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoPatientCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoPatientCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoPatientCaption.Location = new System.Drawing.Point(39, 6);
            this.lblDemoPatientCaption.Name = "lblDemoPatientCaption";
            this.lblDemoPatientCaption.Size = new System.Drawing.Size(54, 14);
            this.lblDemoPatientCaption.TabIndex = 4;
            this.lblDemoPatientCaption.Text = "Patient :";
            // 
            // lblDemoPatient
            // 
            this.lblDemoPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoPatient.AutoSize = true;
            this.lblDemoPatient.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoPatient.ForeColor = System.Drawing.Color.Black;
            this.lblDemoPatient.Location = new System.Drawing.Point(92, 5);
            this.lblDemoPatient.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblDemoPatient.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblDemoPatient.Name = "lblDemoPatient";
            this.lblDemoPatient.Size = new System.Drawing.Size(200, 14);
            this.lblDemoPatient.TabIndex = 88;
            this.lblDemoPatient.Text = "Patient";
            // 
            // pnlPatientPhoto
            // 
            this.pnlPatientPhoto.Controls.Add(this.lblBadDebtSatusII);
            this.pnlPatientPhoto.Controls.Add(this.panel2);
            this.pnlPatientPhoto.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPatientPhoto.Location = new System.Drawing.Point(0, 0);
            this.pnlPatientPhoto.Name = "pnlPatientPhoto";
            this.pnlPatientPhoto.Size = new System.Drawing.Size(110, 147);
            this.pnlPatientPhoto.TabIndex = 73;
            this.pnlPatientPhoto.Visible = false;
            // 
            // lblBadDebtSatusII
            // 
            this.lblBadDebtSatusII.AutoSize = true;
            this.lblBadDebtSatusII.BackColor = System.Drawing.Color.Transparent;
            this.lblBadDebtSatusII.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBadDebtSatusII.ForeColor = System.Drawing.Color.Red;
            this.lblBadDebtSatusII.Location = new System.Drawing.Point(6, 122);
            this.lblBadDebtSatusII.Name = "lblBadDebtSatusII";
            this.lblBadDebtSatusII.Size = new System.Drawing.Size(61, 13);
            this.lblBadDebtSatusII.TabIndex = 96;
            this.lblBadDebtSatusII.Text = "BAD DEBT";
            this.lblBadDebtSatusII.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.picPAPhoto);
            this.panel2.Location = new System.Drawing.Point(3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(105, 119);
            this.panel2.TabIndex = 73;
            // 
            // picPAPhoto
            // 
            this.picPAPhoto.BackColor = System.Drawing.Color.Transparent;
            this.picPAPhoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPAPhoto.Image = ((System.Drawing.Image)(resources.GetObject("picPAPhoto.Image")));
            this.picPAPhoto.Location = new System.Drawing.Point(0, 0);
            this.picPAPhoto.Name = "picPAPhoto";
            this.picPAPhoto.Size = new System.Drawing.Size(103, 117);
            this.picPAPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picPAPhoto.TabIndex = 0;
            this.picPAPhoto.TabStop = false;
            // 
            // lblucPatientStripBottom
            // 
            this.lblucPatientStripBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblucPatientStripBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblucPatientStripBottom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblucPatientStripBottom.Location = new System.Drawing.Point(0, 147);
            this.lblucPatientStripBottom.Name = "lblucPatientStripBottom";
            this.lblucPatientStripBottom.Size = new System.Drawing.Size(1199, 1);
            this.lblucPatientStripBottom.TabIndex = 11;
            // 
            // lblucPatientStripRight
            // 
            this.lblucPatientStripRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblucPatientStripRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblucPatientStripRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblucPatientStripRight.Location = new System.Drawing.Point(1200, 0);
            this.lblucPatientStripRight.Name = "lblucPatientStripRight";
            this.lblucPatientStripRight.Size = new System.Drawing.Size(1, 175);
            this.lblucPatientStripRight.TabIndex = 8;
            // 
            // lblucPatientStripLeft
            // 
            this.lblucPatientStripLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblucPatientStripLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblucPatientStripLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblucPatientStripLeft.Location = new System.Drawing.Point(0, 0);
            this.lblucPatientStripLeft.Name = "lblucPatientStripLeft";
            this.lblucPatientStripLeft.Size = new System.Drawing.Size(1, 175);
            this.lblucPatientStripLeft.TabIndex = 9;
            // 
            // cmnu_ChangeInsResponsiblity
            // 
            this.cmnu_ChangeInsResponsiblity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmnu_ChangeInsResponsiblity.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewBenefit});
            this.cmnu_ChangeInsResponsiblity.Name = "cmnu_Appointment";
            this.cmnu_ChangeInsResponsiblity.Size = new System.Drawing.Size(153, 48);
            // 
            // mnuViewBenefit
            // 
            this.mnuViewBenefit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuViewBenefit.Image = ((System.Drawing.Image)(resources.GetObject("mnuViewBenefit.Image")));
            this.mnuViewBenefit.Name = "mnuViewBenefit";
            this.mnuViewBenefit.Size = new System.Drawing.Size(152, 22);
            this.mnuViewBenefit.Text = "View Benefits";
            this.mnuViewBenefit.Click += new System.EventHandler(this.mnuViewBenefit_Click);
            // 
            // ucPatientStripControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.c1PatientDetails);
            this.Controls.Add(this.pnlClaimSearch);
            this.Controls.Add(this.lblucPatientStripLeft);
            this.Controls.Add(this.lblucPatientStripRight);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "ucPatientStripControl";
            this.Size = new System.Drawing.Size(1201, 175);
            this.Load += new System.EventHandler(this.PatientStripControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PatientStripControl_Paint);
            this.Leave += new System.EventHandler(this.ucPatientStripControl_Leave);
            this.pnlTop.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            this.pnlClaimSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitClaims)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlInsurace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Insurance)).EndInit();
            this.pnlBalance.ResumeLayout(false);
            this.pnlReserve.ResumeLayout(false);
            this.pnlReserve.PerformLayout();
            this.pnlBadDebt.ResumeLayout(false);
            this.pnlBadDebt.PerformLayout();
            this.pnlTotalBal.ResumeLayout(false);
            this.pnlTotalBal.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.pnlAcctNote.ResumeLayout(false);
            this.pnlAcctNote.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.pnlEMRAlertsCaption.ResumeLayout(false);
            this.pnlEMRAlertsCaption.PerformLayout();
            this.pnlAccountDetails.ResumeLayout(false);
            this.pnlAccountDetails.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.pnlNotesAlerts.ResumeLayout(false);
            this.pnlNotesAlerts.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.pnlMedCategory.ResumeLayout(false);
            this.pnlMedCategory.PerformLayout();
            this.pnlPatient.ResumeLayout(false);
            this.pnlPatient.PerformLayout();
            this.pnlPatientPhoto.ResumeLayout(false);
            this.pnlPatientPhoto.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPAPhoto)).EndInit();
            this.cmnu_ChangeInsResponsiblity.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientDetails;
        private System.Windows.Forms.TextBox txtPatientSearch;
        private System.Windows.Forms.Button btnSearchPatientClaim;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblSearchonClaimNo;
        private System.Windows.Forms.CheckBox chk_ClaimNoSearch;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btn_ModityPatient;
        private System.Windows.Forms.Label lbltxtPatientSearchCaption;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPnlHeaderTop;
        private System.Windows.Forms.Label lblPnlHeaderMiddle;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlClaimSearch;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SplitClaims;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlInsurace;
        private System.Windows.Forms.Label lblpnlInsuraceTop;
        private System.Windows.Forms.Label lblpnlInsuraceBottom;
        private System.Windows.Forms.Label lblpnlInsuraceRight;
        private System.Windows.Forms.Label lblpnlInsuraceLeft;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Insurance;
        private System.Windows.Forms.Panel pnlBalance;
        private System.Windows.Forms.Label lblBalTotalLine;
        private System.Windows.Forms.Label lblBalOtherReserveCaption;
        private System.Windows.Forms.Label lblBalPatientDueCaption;
        private System.Windows.Forms.Label lblBalAdvancedReserveCaption;
        private System.Windows.Forms.Label lblBalCopayReserveCaption;
        private System.Windows.Forms.Label lblBalInsurancePendingCaption;
        private System.Windows.Forms.Label lblBalTotalBalanceCaption;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblDemoGuarantor;
        private System.Windows.Forms.Label lblDemoPatientPayment;
        private System.Windows.Forms.Label lblDemoGuarantorCaption;
        private System.Windows.Forms.Label lblDemoLastPatPayment;
        private System.Windows.Forms.Panel pnlAccountDetails;
        private System.Windows.Forms.Label lblDemoAccountNoCaption;
        private System.Windows.Forms.Label lblDemoAccountNo;
        private System.Windows.Forms.Button btnEditAccount;
        private System.Windows.Forms.Label lblAccountDesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDemoAccountDesc;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlNotesAlerts;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblAlertsCap;
        private System.Windows.Forms.Button btn_Alerts;
        private System.Windows.Forms.Label lblNotesCaption;
        private System.Windows.Forms.Label lblDemoCopay;
        private System.Windows.Forms.Label lblDemoProvider;
        private System.Windows.Forms.Label lblDemoNotes;
        private System.Windows.Forms.Label lblAlerts;
        private System.Windows.Forms.Label lblDemoCopayCaption;
        private System.Windows.Forms.Label lblDemoProviderCaption;
        private System.Windows.Forms.Panel pnlPatient;
        private System.Windows.Forms.Label lblDemoPatientCaption;
        private System.Windows.Forms.Label lblDemoPatient;
        private System.Windows.Forms.Panel pnlPatientPhoto;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picPAPhoto;
        private System.Windows.Forms.Label lblucPatientStripRight;
        private System.Windows.Forms.Label lblucPatientStripLeft;
        private System.Windows.Forms.Label lblucPatientStripBottom;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Label lblBalTotalBalance;
        private System.Windows.Forms.Label lblBalOtherReserve;
        private System.Windows.Forms.Label lblBalAdvancedReserve;
        private System.Windows.Forms.Label lblBalCopayReserve;
        private System.Windows.Forms.Label lblBalPatientDue;
        private System.Windows.Forms.Label lblBalInsurancePending;
        private System.Windows.Forms.Label lblAccountNotes;
        private System.Windows.Forms.Label lblAccountNotesCaption;
        private System.Windows.Forms.Panel pnlAcctNote;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label lblPatientCode;
        private System.Windows.Forms.Label lblPatCodeCaption;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblGenderCaption;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.Label lblDobCaption;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblDemoMedCat;
        private System.Windows.Forms.Label lblDemoMedCatCaption;
        private System.Windows.Forms.Panel pnlMedCategory;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Panel pnlEMRAlertsCaption;
        private System.Windows.Forms.Label lblDemoEMRAlerts;
        private System.Windows.Forms.Label lblDemogloEMRAlertsCaption;
        private System.Windows.Forms.Label lblDemoNextAppt;
        private System.Windows.Forms.Label lblDemoNextApptCaption;
        private System.Windows.Forms.Panel pnlBadDebt;
        private System.Windows.Forms.Label lblBalBadDebtCaption;
        private System.Windows.Forms.Label lblBalBadDebt;
        private System.Windows.Forms.Label lblBadDebtSatus;
        private System.Windows.Forms.Panel pnlReserve;
        private System.Windows.Forms.Panel pnlTotalBal;
        private System.Windows.Forms.Label lblBadDebtSatusII;
        private System.Windows.Forms.ContextMenuStrip cmnu_ChangeInsResponsiblity;
        private System.Windows.Forms.ToolStripMenuItem mnuViewBenefit;
    }
}
