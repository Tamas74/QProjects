namespace gloEmdeonInterface.Forms
{
    partial class frmLab_Acknoledgement
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtpReviwed };
            System.Windows.Forms.Control[] cntControls = { dtpReviwed };

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

                }
                catch
                {
                }
                components.Dispose();
                base.Dispose(disposing);
                try
                {
                    
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
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
               
            }
            
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLab_Acknoledgement));
            this.pnl_tlsp = new System.Windows.Forms.Panel();
            this.tlsp_LabAcknoledgment = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_AssignTask = new System.Windows.Forms.ToolStripButton();
            this.ts_NormalAck = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlDocument = new System.Windows.Forms.Panel();
            this.btnInternalBr = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.dtpReviwed = new System.Windows.Forms.DateTimePicker();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPatientBr = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_PatientNotes = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_patientNotes = new System.Windows.Forms.Label();
            this.pnlFileName = new System.Windows.Forms.Panel();
            this.lbl_pnlBottom = new System.Windows.Forms.Label();
            this.lbl_pnlLeft = new System.Windows.Forms.Label();
            this.lbl_pnlRight = new System.Windows.Forms.Label();
            this.lbl_pnlTop = new System.Windows.Forms.Label();
            this.txtOrderName = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.ImgList_Pages = new System.Windows.Forms.ImageList(this.components);
            this.pnl_tlsp.SuspendLayout();
            this.tlsp_LabAcknoledgment.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlDocument.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlFileName.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlsp
            // 
            this.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp.Controls.Add(this.tlsp_LabAcknoledgment);
            this.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnl_tlsp.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp.Name = "pnl_tlsp";
            this.pnl_tlsp.Size = new System.Drawing.Size(553, 54);
            this.pnl_tlsp.TabIndex = 1;
            // 
            // tlsp_LabAcknoledgment
            // 
            this.tlsp_LabAcknoledgment.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_LabAcknoledgment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsp_LabAcknoledgment.BackgroundImage")));
            this.tlsp_LabAcknoledgment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_LabAcknoledgment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_LabAcknoledgment.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_LabAcknoledgment.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_AssignTask,
            this.ts_NormalAck,
            this.ts_btnSave,
            this.ts_btnClose});
            this.tlsp_LabAcknoledgment.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_LabAcknoledgment.Location = new System.Drawing.Point(0, 0);
            this.tlsp_LabAcknoledgment.Name = "tlsp_LabAcknoledgment";
            this.tlsp_LabAcknoledgment.Size = new System.Drawing.Size(553, 53);
            this.tlsp_LabAcknoledgment.TabIndex = 0;
            this.tlsp_LabAcknoledgment.TabStop = true;
            this.tlsp_LabAcknoledgment.Text = "toolStrip1";
            this.tlsp_LabAcknoledgment.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlsp_LabAcknoledgment_ItemClicked);
            // 
            // ts_AssignTask
            // 
            this.ts_AssignTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_AssignTask.Image = ((System.Drawing.Image)(resources.GetObject("ts_AssignTask.Image")));
            this.ts_AssignTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_AssignTask.Name = "ts_AssignTask";
            this.ts_AssignTask.Size = new System.Drawing.Size(82, 50);
            this.ts_AssignTask.Tag = "Assign Task";
            this.ts_AssignTask.Text = "&Assign Task";
            this.ts_AssignTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ts_NormalAck
            // 
            this.ts_NormalAck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_NormalAck.Image = ((System.Drawing.Image)(resources.GetObject("ts_NormalAck.Image")));
            this.ts_NormalAck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_NormalAck.Name = "ts_NormalAck";
            this.ts_NormalAck.Size = new System.Drawing.Size(92, 50);
            this.ts_NormalAck.Tag = "Assign Normal";
            this.ts_NormalAck.Text = "Normal &Notes";
            this.ts_NormalAck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ts_btnSave
            // 
            this.ts_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSave.Image")));
            this.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSave.Name = "ts_btnSave";
            this.ts_btnSave.Size = new System.Drawing.Size(66, 50);
            this.ts_btnSave.Tag = "save";
            this.ts_btnSave.Text = "&Save&&Cls";
            this.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSave.ToolTipText = "Save and Close";
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.pnlDocument);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.pnlFileName);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(553, 291);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlDocument
            // 
            this.pnlDocument.Controls.Add(this.btnInternalBr);
            this.pnlDocument.Controls.Add(this.label13);
            this.pnlDocument.Controls.Add(this.Label5);
            this.pnlDocument.Controls.Add(this.Label6);
            this.pnlDocument.Controls.Add(this.Label7);
            this.pnlDocument.Controls.Add(this.Label8);
            this.pnlDocument.Controls.Add(this.txtUser);
            this.pnlDocument.Controls.Add(this.txtComments);
            this.pnlDocument.Controls.Add(this.dtpReviwed);
            this.pnlDocument.Controls.Add(this.cmbUsers);
            this.pnlDocument.Controls.Add(this.Label4);
            this.pnlDocument.Controls.Add(this.Label3);
            this.pnlDocument.Controls.Add(this.Label2);
            this.pnlDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDocument.Location = new System.Drawing.Point(0, 34);
            this.pnlDocument.Name = "pnlDocument";
            this.pnlDocument.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlDocument.Size = new System.Drawing.Size(553, 142);
            this.pnlDocument.TabIndex = 2;
            // 
            // btnInternalBr
            // 
            this.btnInternalBr.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.btnInternalBr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInternalBr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInternalBr.Image = ((System.Drawing.Image)(resources.GetObject("btnInternalBr.Image")));
            this.btnInternalBr.Location = new System.Drawing.Point(522, 38);
            this.btnInternalBr.Name = "btnInternalBr";
            this.btnInternalBr.Size = new System.Drawing.Size(22, 21);
            this.btnInternalBr.TabIndex = 1;
            this.btnInternalBr.UseVisualStyleBackColor = true;
            this.btnInternalBr.Click += new System.EventHandler(this.btnInternalBr_Click);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(12, 56);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 73);
            this.label13.TabIndex = 11;
            this.label13.Text = "(For internal use Only. Not included in lab result liquid links.)";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Location = new System.Drawing.Point(4, 138);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(545, 1);
            this.Label5.TabIndex = 10;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Location = new System.Drawing.Point(3, 1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 138);
            this.Label6.TabIndex = 9;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Location = new System.Drawing.Point(549, 1);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 138);
            this.Label7.TabIndex = 8;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Location = new System.Drawing.Point(3, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(547, 1);
            this.Label8.TabIndex = 7;
            this.Label8.Text = "label1";
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Enabled = false;
            this.txtUser.ForeColor = System.Drawing.Color.Black;
            this.txtUser.Location = new System.Drawing.Point(137, 8);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.ShortcutsEnabled = false;
            this.txtUser.Size = new System.Drawing.Size(380, 22);
            this.txtUser.TabIndex = 0;
            // 
            // txtComments
            // 
            this.txtComments.ForeColor = System.Drawing.Color.Black;
            this.txtComments.Location = new System.Drawing.Point(137, 36);
            this.txtComments.MaxLength = 1000;
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComments.ShortcutsEnabled = false;
            this.txtComments.Size = new System.Drawing.Size(382, 98);
            this.txtComments.TabIndex = 2;
            // 
            // dtpReviwed
            // 
            this.dtpReviwed.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpReviwed.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpReviwed.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpReviwed.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpReviwed.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpReviwed.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpReviwed.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReviwed.Location = new System.Drawing.Point(153, 36);
            this.dtpReviwed.Name = "dtpReviwed";
            this.dtpReviwed.Size = new System.Drawing.Size(176, 22);
            this.dtpReviwed.TabIndex = 5;
            this.dtpReviwed.Visible = false;
            // 
            // cmbUsers
            // 
            this.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsers.ForeColor = System.Drawing.Color.Black;
            this.cmbUsers.Location = new System.Drawing.Point(145, 8);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(368, 22);
            this.cmbUsers.TabIndex = 3;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label4.Location = new System.Drawing.Point(15, 40);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(118, 14);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Internal Comment  :";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(92, 40);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(41, 14);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "Date :";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label3.Visible = false;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(94, 12);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(39, 14);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "User :";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.btnPatientBr);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txt_PatientNotes);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.lbl_patientNotes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 176);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(553, 115);
            this.panel1.TabIndex = 3;
            // 
            // btnPatientBr
            // 
            this.btnPatientBr.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.btnPatientBr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPatientBr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientBr.Image = ((System.Drawing.Image)(resources.GetObject("btnPatientBr.Image")));
            this.btnPatientBr.Location = new System.Drawing.Point(522, 8);
            this.btnPatientBr.Name = "btnPatientBr";
            this.btnPatientBr.Size = new System.Drawing.Size(22, 21);
            this.btnPatientBr.TabIndex = 0;
            this.btnPatientBr.UseVisualStyleBackColor = true;
            this.btnPatientBr.Click += new System.EventHandler(this.btnPatientBr_Click);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(25, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 65);
            this.label14.TabIndex = 12;
            this.label14.Text = "(For display in lab result liquid links.)";
            // 
            // txt_PatientNotes
            // 
            this.txt_PatientNotes.ForeColor = System.Drawing.Color.Black;
            this.txt_PatientNotes.Location = new System.Drawing.Point(137, 7);
            this.txt_PatientNotes.MaxLength = 1000;
            this.txt_PatientNotes.Multiline = true;
            this.txt_PatientNotes.Name = "txt_PatientNotes";
            this.txt_PatientNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_PatientNotes.ShortcutsEnabled = false;
            this.txt_PatientNotes.Size = new System.Drawing.Size(380, 98);
            this.txt_PatientNotes.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(4, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(545, 1);
            this.label9.TabIndex = 8;
            this.label9.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(3, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 111);
            this.label10.TabIndex = 7;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Location = new System.Drawing.Point(549, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 111);
            this.label11.TabIndex = 6;
            this.label11.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(547, 1);
            this.label12.TabIndex = 5;
            this.label12.Text = "label1";
            // 
            // lbl_patientNotes
            // 
            this.lbl_patientNotes.AutoSize = true;
            this.lbl_patientNotes.Location = new System.Drawing.Point(27, 10);
            this.lbl_patientNotes.Name = "lbl_patientNotes";
            this.lbl_patientNotes.Size = new System.Drawing.Size(106, 14);
            this.lbl_patientNotes.TabIndex = 0;
            this.lbl_patientNotes.Text = "Notes to Patient :";
            this.lbl_patientNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlFileName
            // 
            this.pnlFileName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlFileName.Controls.Add(this.lbl_pnlBottom);
            this.pnlFileName.Controls.Add(this.lbl_pnlLeft);
            this.pnlFileName.Controls.Add(this.lbl_pnlRight);
            this.pnlFileName.Controls.Add(this.lbl_pnlTop);
            this.pnlFileName.Controls.Add(this.txtOrderName);
            this.pnlFileName.Controls.Add(this.Label1);
            this.pnlFileName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFileName.Location = new System.Drawing.Point(0, 0);
            this.pnlFileName.Name = "pnlFileName";
            this.pnlFileName.Padding = new System.Windows.Forms.Padding(3);
            this.pnlFileName.Size = new System.Drawing.Size(553, 34);
            this.pnlFileName.TabIndex = 1;
            // 
            // lbl_pnlBottom
            // 
            this.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlBottom.Location = new System.Drawing.Point(4, 30);
            this.lbl_pnlBottom.Name = "lbl_pnlBottom";
            this.lbl_pnlBottom.Size = new System.Drawing.Size(545, 1);
            this.lbl_pnlBottom.TabIndex = 8;
            this.lbl_pnlBottom.Text = "label2";
            // 
            // lbl_pnlLeft
            // 
            this.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlLeft.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlLeft.Name = "lbl_pnlLeft";
            this.lbl_pnlLeft.Size = new System.Drawing.Size(1, 27);
            this.lbl_pnlLeft.TabIndex = 7;
            this.lbl_pnlLeft.Text = "label4";
            // 
            // lbl_pnlRight
            // 
            this.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRight.Location = new System.Drawing.Point(549, 4);
            this.lbl_pnlRight.Name = "lbl_pnlRight";
            this.lbl_pnlRight.Size = new System.Drawing.Size(1, 27);
            this.lbl_pnlRight.TabIndex = 6;
            this.lbl_pnlRight.Text = "label3";
            // 
            // lbl_pnlTop
            // 
            this.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTop.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlTop.Name = "lbl_pnlTop";
            this.lbl_pnlTop.Size = new System.Drawing.Size(547, 1);
            this.lbl_pnlTop.TabIndex = 5;
            this.lbl_pnlTop.Text = "label1";
            // 
            // txtOrderName
            // 
            this.txtOrderName.BackColor = System.Drawing.Color.White;
            this.txtOrderName.ForeColor = System.Drawing.Color.Black;
            this.txtOrderName.Location = new System.Drawing.Point(137, 6);
            this.txtOrderName.Name = "txtOrderName";
            this.txtOrderName.ReadOnly = true;
            this.txtOrderName.ShortcutsEnabled = false;
            this.txtOrderName.Size = new System.Drawing.Size(382, 22);
            this.txtOrderName.TabIndex = 0;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(52, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(81, 14);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Order Name :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ImgList_Pages
            // 
            this.ImgList_Pages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgList_Pages.ImageStream")));
            this.ImgList_Pages.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgList_Pages.Images.SetKeyName(0, "");
            this.ImgList_Pages.Images.SetKeyName(1, "");
            // 
            // frmLab_Acknoledgement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(553, 345);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnl_tlsp);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLab_Acknoledgement";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acknowledge";
            this.Load += new System.EventHandler(this.frmLab_Acknoledgement_Load);
            this.pnl_tlsp.ResumeLayout(false);
            this.pnl_tlsp.PerformLayout();
            this.tlsp_LabAcknoledgment.ResumeLayout(false);
            this.tlsp_LabAcknoledgment.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlDocument.ResumeLayout(false);
            this.pnlDocument.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlFileName.ResumeLayout(false);
            this.pnlFileName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlsp;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_LabAcknoledgment;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        internal System.Windows.Forms.Panel pnlMain;
        internal System.Windows.Forms.Panel pnlDocument;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.TextBox txtComments;
        internal System.Windows.Forms.DateTimePicker dtpReviwed;
        internal System.Windows.Forms.ComboBox cmbUsers;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Panel pnlFileName;
        private System.Windows.Forms.Label lbl_pnlBottom;
        private System.Windows.Forms.Label lbl_pnlLeft;
        private System.Windows.Forms.Label lbl_pnlRight;
        private System.Windows.Forms.Label lbl_pnlTop;
        internal System.Windows.Forms.TextBox txtOrderName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ImageList ImgList_Pages;
        internal System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.TextBox txt_PatientNotes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label lbl_patientNotes;
        internal System.Windows.Forms.ToolStripButton ts_AssignTask;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnInternalBr;
        private System.Windows.Forms.Button btnPatientBr;
        internal System.Windows.Forms.ToolStripButton ts_NormalAck;

    }
}