namespace gloPatient
{
    partial class frmMergePatients
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMergePatients));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.btnclear1 = new System.Windows.Forms.Button();
            this.btnRemovePatient = new System.Windows.Forms.Button();
            this.rbConflict = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lblDOB_Source = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.lblProvider_Source = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.lblSSN_Source = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtpatientsource = new System.Windows.Forms.TextBox();
            this.cmbPatientToMerge = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblDOB_Destination = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.lblProvider_Destination = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.lblSSN_Destination = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.cmbPatientToMergeIn = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.btnclear2 = new System.Windows.Forms.Button();
            this.txtpatientdest = new System.Windows.Forms.TextBox();
            this.btnSurvivingPatient = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tlsp_MergePatientRecords = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnMerge = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnl_tlsp = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlsp_MergePatientRecords.SuspendLayout();
            this.pnl_tlsp.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.btnclear1);
            this.pnlMain.Controls.Add(this.btnRemovePatient);
            this.pnlMain.Controls.Add(this.rbConflict);
            this.pnlMain.Controls.Add(this.rbAll);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.lbl_BottomBrd);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.lbl_LeftBrd);
            this.pnlMain.Controls.Add(this.lblDOB_Source);
            this.pnlMain.Controls.Add(this.Label1);
            this.pnlMain.Controls.Add(this.Label8);
            this.pnlMain.Controls.Add(this.lblProvider_Source);
            this.pnlMain.Controls.Add(this.Label6);
            this.pnlMain.Controls.Add(this.lblSSN_Source);
            this.pnlMain.Controls.Add(this.Label4);
            this.pnlMain.Controls.Add(this.txtpatientsource);
            this.pnlMain.Controls.Add(this.cmbPatientToMerge);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMain.Location = new System.Drawing.Point(0, 116);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Size = new System.Drawing.Size(692, 140);
            this.pnlMain.TabIndex = 0;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(4, 1);
            this.label22.Name = "label22";
            this.label22.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.label22.Size = new System.Drawing.Size(125, 22);
            this.label22.TabIndex = 44;
            this.label22.Text = "Patient Record 1 ";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnclear1
            // 
            this.btnclear1.AutoEllipsis = true;
            this.btnclear1.BackColor = System.Drawing.Color.Transparent;
            this.btnclear1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnclear1.BackgroundImage")));
            this.btnclear1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclear1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclear1.Image = ((System.Drawing.Image)(resources.GetObject("btnclear1.Image")));
            this.btnclear1.Location = new System.Drawing.Point(654, 21);
            this.btnclear1.Name = "btnclear1";
            this.btnclear1.Size = new System.Drawing.Size(21, 21);
            this.btnclear1.TabIndex = 43;
            this.btnclear1.UseVisualStyleBackColor = false;
            this.btnclear1.Click += new System.EventHandler(this.btnclear1_Click);
            this.btnclear1.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnclear1.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnRemovePatient
            // 
            this.btnRemovePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnRemovePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemovePatient.BackgroundImage")));
            this.btnRemovePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemovePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRemovePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemovePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnRemovePatient.Image")));
            this.btnRemovePatient.Location = new System.Drawing.Point(629, 21);
            this.btnRemovePatient.Name = "btnRemovePatient";
            this.btnRemovePatient.Size = new System.Drawing.Size(21, 21);
            this.btnRemovePatient.TabIndex = 31;
            this.btnRemovePatient.UseVisualStyleBackColor = false;
            this.btnRemovePatient.Click += new System.EventHandler(this.btnRemovePatient_Click);
            this.btnRemovePatient.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnRemovePatient.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // rbConflict
            // 
            this.rbConflict.AutoSize = true;
            this.rbConflict.Location = new System.Drawing.Point(462, 113);
            this.rbConflict.Name = "rbConflict";
            this.rbConflict.Size = new System.Drawing.Size(100, 18);
            this.rbConflict.TabIndex = 2;
            this.rbConflict.Text = "Show Conflict";
            this.rbConflict.UseVisualStyleBackColor = true;
            this.rbConflict.CheckedChanged += new System.EventHandler(this.rbConflict_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAll.Location = new System.Drawing.Point(361, 113);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(79, 18);
            this.rbAll.TabIndex = 1;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "Show All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(684, 1);
            this.label5.TabIndex = 30;
            this.label5.Text = "label2";
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 136);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(684, 1);
            this.lbl_BottomBrd.TabIndex = 29;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(688, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 137);
            this.label3.TabIndex = 28;
            this.label3.Text = "label4";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 137);
            this.lbl_LeftBrd.TabIndex = 27;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lblDOB_Source
            // 
            this.lblDOB_Source.BackColor = System.Drawing.Color.White;
            this.lblDOB_Source.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDOB_Source.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDOB_Source.Location = new System.Drawing.Point(523, 53);
            this.lblDOB_Source.Name = "lblDOB_Source";
            this.lblDOB_Source.Size = new System.Drawing.Size(98, 22);
            this.lblDOB_Source.TabIndex = 18;
            this.lblDOB_Source.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Location = new System.Drawing.Point(9, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(342, 14);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "Select patient record you wish to remove from the system  :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Location = new System.Drawing.Point(479, 57);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(39, 14);
            this.Label8.TabIndex = 17;
            this.Label8.Text = "DOB :";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProvider_Source
            // 
            this.lblProvider_Source.BackColor = System.Drawing.Color.White;
            this.lblProvider_Source.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProvider_Source.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProvider_Source.Location = new System.Drawing.Point(360, 84);
            this.lblProvider_Source.Name = "lblProvider_Source";
            this.lblProvider_Source.Size = new System.Drawing.Size(261, 22);
            this.lblProvider_Source.TabIndex = 16;
            this.lblProvider_Source.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Location = new System.Drawing.Point(292, 88);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(59, 14);
            this.Label6.TabIndex = 15;
            this.Label6.Text = "Provider :";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSSN_Source
            // 
            this.lblSSN_Source.BackColor = System.Drawing.Color.White;
            this.lblSSN_Source.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSSN_Source.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSSN_Source.Location = new System.Drawing.Point(360, 53);
            this.lblSSN_Source.Name = "lblSSN_Source";
            this.lblSSN_Source.Size = new System.Drawing.Size(115, 22);
            this.lblSSN_Source.TabIndex = 4;
            this.lblSSN_Source.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Location = new System.Drawing.Point(314, 57);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(37, 14);
            this.Label4.TabIndex = 12;
            this.Label4.Text = "SSN :";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtpatientsource
            // 
            this.txtpatientsource.Location = new System.Drawing.Point(360, 22);
            this.txtpatientsource.Name = "txtpatientsource";
            this.txtpatientsource.ReadOnly = true;
            this.txtpatientsource.ShortcutsEnabled = false;
            this.txtpatientsource.Size = new System.Drawing.Size(262, 22);
            this.txtpatientsource.TabIndex = 32;
            this.txtpatientsource.TabStop = false;
            // 
            // cmbPatientToMerge
            // 
            this.cmbPatientToMerge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatientToMerge.ForeColor = System.Drawing.Color.Black;
            this.cmbPatientToMerge.Location = new System.Drawing.Point(360, 22);
            this.cmbPatientToMerge.Name = "cmbPatientToMerge";
            this.cmbPatientToMerge.Size = new System.Drawing.Size(262, 22);
            this.cmbPatientToMerge.TabIndex = 3;
            this.cmbPatientToMerge.Visible = false;
            this.cmbPatientToMerge.SelectionChangeCommitted += new System.EventHandler(this.cmbPatientToMerge_SelectionChangeCommitted);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Location = new System.Drawing.Point(34, 27);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(317, 14);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "Select patient record you wish to remain in the system :";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDOB_Destination
            // 
            this.lblDOB_Destination.BackColor = System.Drawing.Color.White;
            this.lblDOB_Destination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDOB_Destination.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDOB_Destination.Location = new System.Drawing.Point(523, 53);
            this.lblDOB_Destination.Name = "lblDOB_Destination";
            this.lblDOB_Destination.Size = new System.Drawing.Size(102, 22);
            this.lblDOB_Destination.TabIndex = 26;
            this.lblDOB_Destination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.BackColor = System.Drawing.Color.Transparent;
            this.Label14.Location = new System.Drawing.Point(481, 57);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(39, 14);
            this.Label14.TabIndex = 25;
            this.Label14.Text = "DOB :";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProvider_Destination
            // 
            this.lblProvider_Destination.BackColor = System.Drawing.Color.White;
            this.lblProvider_Destination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProvider_Destination.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProvider_Destination.Location = new System.Drawing.Point(360, 82);
            this.lblProvider_Destination.Name = "lblProvider_Destination";
            this.lblProvider_Destination.Size = new System.Drawing.Size(265, 22);
            this.lblProvider_Destination.TabIndex = 24;
            this.lblProvider_Destination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Location = new System.Drawing.Point(292, 86);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(59, 14);
            this.Label16.TabIndex = 23;
            this.Label16.Text = "Provider :";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSSN_Destination
            // 
            this.lblSSN_Destination.BackColor = System.Drawing.Color.White;
            this.lblSSN_Destination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSSN_Destination.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSSN_Destination.Location = new System.Drawing.Point(360, 53);
            this.lblSSN_Destination.Name = "lblSSN_Destination";
            this.lblSSN_Destination.Size = new System.Drawing.Size(108, 22);
            this.lblSSN_Destination.TabIndex = 22;
            this.lblSSN_Destination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.BackColor = System.Drawing.Color.Transparent;
            this.Label18.Location = new System.Drawing.Point(314, 57);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(37, 14);
            this.Label18.TabIndex = 21;
            this.Label18.Text = "SSN :";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPatientToMergeIn
            // 
            this.cmbPatientToMergeIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatientToMergeIn.ForeColor = System.Drawing.Color.Black;
            this.cmbPatientToMergeIn.Location = new System.Drawing.Point(360, 24);
            this.cmbPatientToMergeIn.Name = "cmbPatientToMergeIn";
            this.cmbPatientToMergeIn.Size = new System.Drawing.Size(265, 22);
            this.cmbPatientToMergeIn.TabIndex = 4;
            this.cmbPatientToMergeIn.Visible = false;
            this.cmbPatientToMergeIn.SelectionChangeCommitted += new System.EventHandler(this.cmbPatientToMergeIn_SelectionChangeCommitted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.btnclear2);
            this.panel1.Controls.Add(this.txtpatientdest);
            this.panel1.Controls.Add(this.btnSurvivingPatient);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cmbPatientToMergeIn);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblSSN_Destination);
            this.panel1.Controls.Add(this.lblDOB_Destination);
            this.panel1.Controls.Add(this.Label16);
            this.panel1.Controls.Add(this.Label18);
            this.panel1.Controls.Add(this.lblProvider_Destination);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.Label14);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 256);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(692, 122);
            this.panel1.TabIndex = 1;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(4, 1);
            this.label23.Name = "label23";
            this.label23.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.label23.Size = new System.Drawing.Size(125, 22);
            this.label23.TabIndex = 45;
            this.label23.Text = "Patient Record 2 ";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnclear2
            // 
            this.btnclear2.AutoEllipsis = true;
            this.btnclear2.BackColor = System.Drawing.Color.Transparent;
            this.btnclear2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnclear2.BackgroundImage")));
            this.btnclear2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclear2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclear2.Image = ((System.Drawing.Image)(resources.GetObject("btnclear2.Image")));
            this.btnclear2.Location = new System.Drawing.Point(654, 24);
            this.btnclear2.Name = "btnclear2";
            this.btnclear2.Size = new System.Drawing.Size(21, 21);
            this.btnclear2.TabIndex = 43;
            this.btnclear2.UseVisualStyleBackColor = false;
            this.btnclear2.Click += new System.EventHandler(this.btnclear2_Click);
            this.btnclear2.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnclear2.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // txtpatientdest
            // 
            this.txtpatientdest.Location = new System.Drawing.Point(360, 24);
            this.txtpatientdest.Name = "txtpatientdest";
            this.txtpatientdest.ReadOnly = true;
            this.txtpatientdest.ShortcutsEnabled = false;
            this.txtpatientdest.Size = new System.Drawing.Size(265, 22);
            this.txtpatientdest.TabIndex = 34;
            this.txtpatientdest.TabStop = false;
            // 
            // btnSurvivingPatient
            // 
            this.btnSurvivingPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnSurvivingPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSurvivingPatient.BackgroundImage")));
            this.btnSurvivingPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSurvivingPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnSurvivingPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSurvivingPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnSurvivingPatient.Image")));
            this.btnSurvivingPatient.Location = new System.Drawing.Point(629, 24);
            this.btnSurvivingPatient.Name = "btnSurvivingPatient";
            this.btnSurvivingPatient.Size = new System.Drawing.Size(21, 21);
            this.btnSurvivingPatient.TabIndex = 33;
            this.btnSurvivingPatient.UseVisualStyleBackColor = false;
            this.btnSurvivingPatient.Click += new System.EventHandler(this.btnSurvivingPatient_Click);
            this.btnSurvivingPatient.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnSurvivingPatient.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(4, 118);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(684, 1);
            this.label11.TabIndex = 32;
            this.label11.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(684, 1);
            this.label10.TabIndex = 31;
            this.label10.Text = "label2";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(688, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 119);
            this.label9.TabIndex = 29;
            this.label9.Text = "label4";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 119);
            this.label7.TabIndex = 28;
            this.label7.Text = "label4";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(63, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(89, 38);
            this.panel2.TabIndex = 44;
            this.panel2.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(10, 34);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(598, 14);
            this.label21.TabIndex = 34;
            this.label21.Text = "All information currently associated with Patient Record 1 will be merged with Pa" +
                "tient Record 2";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(53, 13);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(476, 14);
            this.label20.TabIndex = 34;
            this.label20.Text = "The bottom box is for the patient record you wish to remain in the system. ";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(10, 13);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 14);
            this.label19.TabIndex = 33;
            this.label19.Text = "Note :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(3, 58);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(686, 1);
            this.label17.TabIndex = 32;
            this.label17.Text = "label2";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label15.Location = new System.Drawing.Point(4, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(684, 1);
            this.label15.TabIndex = 31;
            this.label15.Text = "label2";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(688, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 55);
            this.label13.TabIndex = 29;
            this.label13.Text = "label4";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 55);
            this.label12.TabIndex = 28;
            this.label12.Text = "label4";
            // 
            // tlsp_MergePatientRecords
            // 
            this.tlsp_MergePatientRecords.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_MergePatientRecords.BackgroundImage = global::gloPatient.Properties.Resources.Img_Toolstrip;
            this.tlsp_MergePatientRecords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_MergePatientRecords.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_MergePatientRecords.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_MergePatientRecords.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnMerge,
            this.tsb_Close});
            this.tlsp_MergePatientRecords.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_MergePatientRecords.Location = new System.Drawing.Point(0, 0);
            this.tlsp_MergePatientRecords.Name = "tlsp_MergePatientRecords";
            this.tlsp_MergePatientRecords.Size = new System.Drawing.Size(692, 53);
            this.tlsp_MergePatientRecords.TabIndex = 5;
            this.tlsp_MergePatientRecords.Text = "toolStrip1";
            // 
            // ts_btnMerge
            // 
            this.ts_btnMerge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnMerge.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnMerge.Image")));
            this.ts_btnMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnMerge.Name = "ts_btnMerge";
            this.ts_btnMerge.Size = new System.Drawing.Size(49, 50);
            this.ts_btnMerge.Tag = "Merge";
            this.ts_btnMerge.Text = "&Merge";
            this.ts_btnMerge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnMerge.Click += new System.EventHandler(this.ts_btnMerge_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // pnl_tlsp
            // 
            this.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp.Controls.Add(this.tlsp_MergePatientRecords);
            this.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnl_tlsp.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp.Name = "pnl_tlsp";
            this.pnl_tlsp.Size = new System.Drawing.Size(692, 54);
            this.pnl_tlsp.TabIndex = 15;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 54);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(692, 62);
            this.panel3.TabIndex = 45;
            // 
            // frmMergePatients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(692, 378);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnl_tlsp);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMergePatients";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Merge Patients";
            this.Load += new System.EventHandler(this.frmMergePatients_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tlsp_MergePatientRecords.ResumeLayout(false);
            this.tlsp_MergePatientRecords.PerformLayout();
            this.pnl_tlsp.ResumeLayout(false);
            this.pnl_tlsp.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlMain;
        internal System.Windows.Forms.Label lblDOB_Destination;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label lblProvider_Destination;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Label lblSSN_Destination;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.Label lblDOB_Source;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label lblProvider_Source;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label lblSSN_Source;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox cmbPatientToMergeIn;
        internal System.Windows.Forms.ComboBox cmbPatientToMerge;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbConflict;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Button btnRemovePatient;
        private System.Windows.Forms.Button btnSurvivingPatient;
        private System.Windows.Forms.TextBox txtpatientsource;
        private System.Windows.Forms.TextBox txtpatientdest;
        private System.Windows.Forms.Button btnclear1;
        private System.Windows.Forms.Button btnclear2;
        private System.Windows.Forms.Panel panel2;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_MergePatientRecords;
        private System.Windows.Forms.ToolStripButton ts_btnMerge;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnl_tlsp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Label label21;
        internal System.Windows.Forms.Label label20;
        internal System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Label label22;
        internal System.Windows.Forms.Label label23;
    }
}