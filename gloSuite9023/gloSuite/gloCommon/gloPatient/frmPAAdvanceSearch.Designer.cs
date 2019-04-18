namespace gloPatient
{
    partial class frmPAAdvanceSearch
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
                    if (dtpDOB != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDOB);

                        }
                        catch
                        {
                        }


                        dtpDOB.Dispose();
                        dtpDOB = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPAAdvanceSearch));
            this.pnlPhone = new System.Windows.Forms.Panel();
            this.txtEmployersPhone = new gloMaskControl.gloMaskBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMobile = new gloMaskControl.gloMaskBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPhone = new gloMaskControl.gloMaskBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSSN = new System.Windows.Forms.TextBox();
            this.chkGardianInfo = new System.Windows.Forms.CheckBox();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.dtpDOB = new System.Windows.Forms.DateTimePicker();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.chkAdvSearch = new System.Windows.Forms.CheckBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsbSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlGuardian = new System.Windows.Forms.Panel();
            this.txtFPhone = new gloMaskControl.gloMaskBox();
            this.txtFCellNo = new gloMaskControl.gloMaskBox();
            this.txtMPhone = new gloMaskControl.gloMaskBox();
            this.txtMCellNo = new gloMaskControl.gloMaskBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlPhone.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlGuardian.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPhone
            // 
            this.pnlPhone.Controls.Add(this.txtEmployersPhone);
            this.pnlPhone.Controls.Add(this.label16);
            this.pnlPhone.Controls.Add(this.txtMobile);
            this.pnlPhone.Controls.Add(this.label15);
            this.pnlPhone.Controls.Add(this.txtPhone);
            this.pnlPhone.Controls.Add(this.label20);
            this.pnlPhone.Controls.Add(this.label19);
            this.pnlPhone.Controls.Add(this.label14);
            this.pnlPhone.Controls.Add(this.label13);
            this.pnlPhone.Controls.Add(this.txtSSN);
            this.pnlPhone.Controls.Add(this.chkGardianInfo);
            this.pnlPhone.Controls.Add(this.txtFName);
            this.pnlPhone.Controls.Add(this.Label1);
            this.pnlPhone.Controls.Add(this.dtpDOB);
            this.pnlPhone.Controls.Add(this.txtLName);
            this.pnlPhone.Controls.Add(this.Label2);
            this.pnlPhone.Controls.Add(this.txtCode);
            this.pnlPhone.Controls.Add(this.chkAdvSearch);
            this.pnlPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPhone.Location = new System.Drawing.Point(0, 82);
            this.pnlPhone.Name = "pnlPhone";
            this.pnlPhone.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlPhone.Size = new System.Drawing.Size(377, 171);
            this.pnlPhone.TabIndex = 1;
            // 
            // txtEmployersPhone
            // 
            this.txtEmployersPhone.AllowValidate = true;
            this.txtEmployersPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployersPhone.IncludeLiteralsAndPrompts = false;
            this.txtEmployersPhone.Location = new System.Drawing.Point(187, 87);
            this.txtEmployersPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtEmployersPhone.Name = "txtEmployersPhone";
            this.txtEmployersPhone.ReadOnly = false;
            this.txtEmployersPhone.Size = new System.Drawing.Size(108, 22);
            this.txtEmployersPhone.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(72, 91);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 14);
            this.label16.TabIndex = 45;
            this.label16.Text = "Employer’s Phone :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMobile
            // 
            this.txtMobile.AllowValidate = true;
            this.txtMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobile.IncludeLiteralsAndPrompts = false;
            this.txtMobile.Location = new System.Drawing.Point(187, 61);
            this.txtMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.ReadOnly = false;
            this.txtMobile.Size = new System.Drawing.Size(108, 22);
            this.txtMobile.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(135, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 14);
            this.label15.TabIndex = 44;
            this.label15.Text = "Mobile :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPhone
            // 
            this.txtPhone.AllowValidate = true;
            this.txtPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhone.IncludeLiteralsAndPrompts = false;
            this.txtPhone.Location = new System.Drawing.Point(187, 35);
            this.txtPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.ReadOnly = false;
            this.txtPhone.Size = new System.Drawing.Size(108, 22);
            this.txtPhone.TabIndex = 2;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Location = new System.Drawing.Point(4, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(369, 1);
            this.label20.TabIndex = 43;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(4, 167);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(369, 1);
            this.label19.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Location = new System.Drawing.Point(373, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 167);
            this.label14.TabIndex = 41;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(3, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 167);
            this.label13.TabIndex = 40;
            // 
            // txtSSN
            // 
            this.txtSSN.BackColor = System.Drawing.Color.GhostWhite;
            this.txtSSN.Location = new System.Drawing.Point(347, 112);
            this.txtSSN.Name = "txtSSN";
            this.txtSSN.ReadOnly = true;
            this.txtSSN.Size = new System.Drawing.Size(7, 22);
            this.txtSSN.TabIndex = 23;
            this.txtSSN.Visible = false;
            // 
            // chkGardianInfo
            // 
            this.chkGardianInfo.AutoSize = true;
            this.chkGardianInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGardianInfo.Location = new System.Drawing.Point(9, 139);
            this.chkGardianInfo.Name = "chkGardianInfo";
            this.chkGardianInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkGardianInfo.Size = new System.Drawing.Size(192, 18);
            this.chkGardianInfo.TabIndex = 6;
            this.chkGardianInfo.Text = "Search in Guardian information";
            this.chkGardianInfo.Click += new System.EventHandler(this.chkGardianInfo_Click);
            // 
            // txtFName
            // 
            this.txtFName.BackColor = System.Drawing.Color.GhostWhite;
            this.txtFName.Location = new System.Drawing.Point(349, 61);
            this.txtFName.Name = "txtFName";
            this.txtFName.ReadOnly = true;
            this.txtFName.Size = new System.Drawing.Size(7, 22);
            this.txtFName.TabIndex = 21;
            this.txtFName.Visible = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(99, 117);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(85, 14);
            this.Label1.TabIndex = 20;
            this.Label1.Text = "Date of Birth :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDOB
            // 
            this.dtpDOB.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOB.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOB.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOB.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOB.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOB.CustomFormat = "MM/dd/yyyy";
            this.dtpDOB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDOB.Location = new System.Drawing.Point(187, 113);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.ShowCheckBox = true;
            this.dtpDOB.Size = new System.Drawing.Size(108, 22);
            this.dtpDOB.TabIndex = 5;
            this.dtpDOB.Value = new System.DateTime(2007, 1, 27, 0, 0, 0, 0);
            // 
            // txtLName
            // 
            this.txtLName.BackColor = System.Drawing.Color.GhostWhite;
            this.txtLName.Location = new System.Drawing.Point(349, 30);
            this.txtLName.Name = "txtLName";
            this.txtLName.ReadOnly = true;
            this.txtLName.Size = new System.Drawing.Size(7, 22);
            this.txtLName.TabIndex = 18;
            this.txtLName.Visible = false;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(134, 39);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(50, 14);
            this.Label2.TabIndex = 17;
            this.Label2.Text = "Phone :";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.GhostWhite;
            this.txtCode.Location = new System.Drawing.Point(348, 8);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(10, 22);
            this.txtCode.TabIndex = 15;
            this.txtCode.Visible = false;
            // 
            // chkAdvSearch
            // 
            this.chkAdvSearch.AutoSize = true;
            this.chkAdvSearch.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAdvSearch.Checked = true;
            this.chkAdvSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAdvSearch.Location = new System.Drawing.Point(38, 8);
            this.chkAdvSearch.Name = "chkAdvSearch";
            this.chkAdvSearch.Size = new System.Drawing.Size(162, 18);
            this.chkAdvSearch.TabIndex = 1;
            this.chkAdvSearch.Text = "Search in Previous Result";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.toolStrip1);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBottom.Location = new System.Drawing.Point(0, 0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(377, 54);
            this.pnlBottom.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::gloPatient.Properties.Resources.Img_Toolstrip;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSearch,
            this.tsbCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(377, 53);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // tsbSearch
            // 
            this.tsbSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbSearch.Image")));
            this.tsbSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSearch.Name = "tsbSearch";
            this.tsbSearch.Size = new System.Drawing.Size(52, 50);
            this.tsbSearch.Tag = "Search";
            this.tsbSearch.Text = "&Search";
            this.tsbSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbCancel
            // 
            this.tsbCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(43, 50);
            this.tsbCancel.Tag = "Cancel";
            this.tsbCancel.Text = "&Close";
            this.tsbCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlGuardian
            // 
            this.pnlGuardian.Controls.Add(this.txtFPhone);
            this.pnlGuardian.Controls.Add(this.txtFCellNo);
            this.pnlGuardian.Controls.Add(this.txtMPhone);
            this.pnlGuardian.Controls.Add(this.txtMCellNo);
            this.pnlGuardian.Controls.Add(this.label18);
            this.pnlGuardian.Controls.Add(this.label17);
            this.pnlGuardian.Controls.Add(this.label12);
            this.pnlGuardian.Controls.Add(this.label3);
            this.pnlGuardian.Controls.Add(this.Label11);
            this.pnlGuardian.Controls.Add(this.Label10);
            this.pnlGuardian.Controls.Add(this.Label7);
            this.pnlGuardian.Controls.Add(this.txtFFName);
            this.pnlGuardian.Controls.Add(this.Label8);
            this.pnlGuardian.Controls.Add(this.txtFLName);
            this.pnlGuardian.Controls.Add(this.Label9);
            this.pnlGuardian.Controls.Add(this.Label6);
            this.pnlGuardian.Controls.Add(this.txtMFName);
            this.pnlGuardian.Controls.Add(this.Label5);
            this.pnlGuardian.Controls.Add(this.txtMLName);
            this.pnlGuardian.Controls.Add(this.Label4);
            this.pnlGuardian.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGuardian.Location = new System.Drawing.Point(0, 253);
            this.pnlGuardian.Name = "pnlGuardian";
            this.pnlGuardian.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlGuardian.Size = new System.Drawing.Size(377, 259);
            this.pnlGuardian.TabIndex = 2;
            // 
            // txtFPhone
            // 
            this.txtFPhone.AllowValidate = true;
            this.txtFPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFPhone.IncludeLiteralsAndPrompts = false;
            this.txtFPhone.Location = new System.Drawing.Point(187, 215);
            this.txtFPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtFPhone.Name = "txtFPhone";
            this.txtFPhone.ReadOnly = false;
            this.txtFPhone.Size = new System.Drawing.Size(108, 22);
            this.txtFPhone.TabIndex = 8;
            // 
            // txtFCellNo
            // 
            this.txtFCellNo.AllowValidate = true;
            this.txtFCellNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFCellNo.IncludeLiteralsAndPrompts = false;
            this.txtFCellNo.Location = new System.Drawing.Point(187, 187);
            this.txtFCellNo.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.txtFCellNo.Name = "txtFCellNo";
            this.txtFCellNo.ReadOnly = false;
            this.txtFCellNo.Size = new System.Drawing.Size(108, 22);
            this.txtFCellNo.TabIndex = 7;
            // 
            // txtMPhone
            // 
            this.txtMPhone.AllowValidate = true;
            this.txtMPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMPhone.IncludeLiteralsAndPrompts = false;
            this.txtMPhone.Location = new System.Drawing.Point(187, 100);
            this.txtMPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtMPhone.Name = "txtMPhone";
            this.txtMPhone.ReadOnly = false;
            this.txtMPhone.Size = new System.Drawing.Size(108, 22);
            this.txtMPhone.TabIndex = 4;
            // 
            // txtMCellNo
            // 
            this.txtMCellNo.AllowValidate = true;
            this.txtMCellNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMCellNo.IncludeLiteralsAndPrompts = false;
            this.txtMCellNo.Location = new System.Drawing.Point(187, 69);
            this.txtMCellNo.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.txtMCellNo.Name = "txtMCellNo";
            this.txtMCellNo.ReadOnly = false;
            this.txtMCellNo.Size = new System.Drawing.Size(108, 22);
            this.txtMCellNo.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(4, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(369, 1);
            this.label18.TabIndex = 42;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Location = new System.Drawing.Point(4, 255);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(369, 1);
            this.label17.TabIndex = 41;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(373, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 255);
            this.label12.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 255);
            this.label3.TabIndex = 39;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(87, 220);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(97, 14);
            this.Label11.TabIndex = 38;
            this.Label11.Text = "Father\'s Phone :";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(83, 104);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(101, 14);
            this.Label10.TabIndex = 37;
            this.Label10.Text = "Mother\'s Phone :";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(88, 191);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(96, 14);
            this.Label7.TabIndex = 36;
            this.Label7.Text = "Father\'s Mobile :";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFFName
            // 
            this.txtFFName.ForeColor = System.Drawing.Color.Black;
            this.txtFFName.Location = new System.Drawing.Point(187, 157);
            this.txtFFName.MaxLength = 255;
            this.txtFFName.Name = "txtFFName";
            this.txtFFName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFFName.Size = new System.Drawing.Size(166, 22);
            this.txtFFName.TabIndex = 6;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(65, 162);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(119, 14);
            this.Label8.TabIndex = 35;
            this.Label8.Text = "Father\'s First Name :";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFLName
            // 
            this.txtFLName.ForeColor = System.Drawing.Color.Black;
            this.txtFLName.Location = new System.Drawing.Point(187, 128);
            this.txtFLName.MaxLength = 255;
            this.txtFLName.Name = "txtFLName";
            this.txtFLName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFLName.Size = new System.Drawing.Size(166, 22);
            this.txtFLName.TabIndex = 5;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(65, 133);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(119, 14);
            this.Label9.TabIndex = 34;
            this.Label9.Text = "Father\'s Last Name :";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(84, 75);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(100, 14);
            this.Label6.TabIndex = 33;
            this.Label6.Text = "Mother\'s Mobile :";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMFName
            // 
            this.txtMFName.ForeColor = System.Drawing.Color.Black;
            this.txtMFName.Location = new System.Drawing.Point(187, 41);
            this.txtMFName.MaxLength = 255;
            this.txtMFName.Name = "txtMFName";
            this.txtMFName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMFName.Size = new System.Drawing.Size(166, 22);
            this.txtMFName.TabIndex = 2;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(61, 46);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(123, 14);
            this.Label5.TabIndex = 32;
            this.Label5.Text = "Mother\'s First Name :";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMLName
            // 
            this.txtMLName.ForeColor = System.Drawing.Color.Black;
            this.txtMLName.Location = new System.Drawing.Point(187, 12);
            this.txtMLName.MaxLength = 255;
            this.txtMLName.Name = "txtMLName";
            this.txtMLName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMLName.Size = new System.Drawing.Size(166, 22);
            this.txtMLName.TabIndex = 1;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(61, 17);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(123, 14);
            this.Label4.TabIndex = 31;
            this.Label4.Text = "Mother\'s Last Name :";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlTop
            // 
            this.pnlTop.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.lbl_BottomBrd);
            this.pnlTop.Controls.Add(this.lbl_LeftBrd);
            this.pnlTop.Controls.Add(this.lbl_RightBrd);
            this.pnlTop.Controls.Add(this.lbl_TopBrd);
            this.pnlTop.Controls.Add(this.lblHeader);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(3, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(371, 22);
            this.pnlTop.TabIndex = 0;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 21);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(369, 1);
            this.lbl_BottomBrd.TabIndex = 8;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 21);
            this.lbl_LeftBrd.TabIndex = 7;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(370, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 21);
            this.lbl_RightBrd.TabIndex = 6;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(371, 1);
            this.lbl_TopBrd.TabIndex = 5;
            this.lbl_TopBrd.Text = "label1";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Padding = new System.Windows.Forms.Padding(3);
            this.lblHeader.Size = new System.Drawing.Size(191, 20);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = " Advanced Search on Patient";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(377, 28);
            this.panel1.TabIndex = 4;
            // 
            // frmPAAdvanceSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(377, 512);
            this.Controls.Add(this.pnlGuardian);
            this.Controls.Add(this.pnlPhone);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlBottom);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPAAdvanceSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Search ";
            this.Load += new System.EventHandler(this.frmPAAdvanceSearch_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPAAdvanceSearch_FormClosed);
            this.pnlPhone.ResumeLayout(false);
            this.pnlPhone.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlGuardian.ResumeLayout(false);
            this.pnlGuardian.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        internal System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlPhone;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlGuardian;
        internal System.Windows.Forms.TextBox txtSSN;
        internal System.Windows.Forms.CheckBox chkGardianInfo;
        internal System.Windows.Forms.TextBox txtFName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker dtpDOB;
        internal System.Windows.Forms.TextBox txtLName;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtCode;
        internal System.Windows.Forms.CheckBox chkAdvSearch;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label12;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSearch;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Panel panel1;
        private gloMaskControl.gloMaskBox txtMCellNo;
        private gloMaskControl.gloMaskBox txtFPhone;
        private gloMaskControl.gloMaskBox txtFCellNo;
        private gloMaskControl.gloMaskBox txtMPhone;
        private gloMaskControl.gloMaskBox txtPhone;
        private gloMaskControl.gloMaskBox txtMobile;
        internal System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Label label16;
        private gloMaskControl.gloMaskBox txtEmployersPhone;
    }
}