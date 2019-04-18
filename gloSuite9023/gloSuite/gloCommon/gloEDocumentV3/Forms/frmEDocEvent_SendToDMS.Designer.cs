
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_SendToDMS
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
                System.Windows.Forms.DateTimePicker[] dtpControls = { dtpDueDate };
                System.Windows.Forms.Control[] cntControls = { dtpDueDate };
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_SendToDMS));
            this.pbDocument = new System.Windows.Forms.ProgressBar();
            this.txtTask = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblTask = new System.Windows.Forms.Label();
            this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_Review = new System.Windows.Forms.ToolStripButton();
            this.tlb_History = new System.Windows.Forms.ToolStripButton();
            this.tlb_Delete = new System.Windows.Forms.ToolStripButton();
            this.txtDocumentName = new System.Windows.Forms.TextBox();
            this.lblDocName = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.btnRemove_user = new System.Windows.Forms.Button();
            this.btnbrowse = new System.Windows.Forms.Button();
            this.pnlTask = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnPatientRemove = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnPatientBrowse = new System.Windows.Forms.Button();
            this.txtPatient = new System.Windows.Forms.TextBox();
            this.chkSendTask = new System.Windows.Forms.CheckBox();
            this.pnlTasksPanel = new System.Windows.Forms.Panel();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.lblDuedate = new System.Windows.Forms.Label();
            this.pnlpriority = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_CntrOK = new System.Windows.Forms.ToolStripButton();
            this.tlb_CntrCancel = new System.Windows.Forms.ToolStripButton();
            this.tls_MaintainDoc.SuspendLayout();
            this.pnlTask.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlTasksPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbDocument
            // 
            this.pbDocument.Location = new System.Drawing.Point(12, 6);
            this.pbDocument.Name = "pbDocument";
            this.pbDocument.Size = new System.Drawing.Size(458, 15);
            this.pbDocument.TabIndex = 5;
            // 
            // txtTask
            // 
            this.txtTask.Location = new System.Drawing.Point(89, 84);
            this.txtTask.MaxLength = 255;
            this.txtTask.Multiline = true;
            this.txtTask.Name = "txtTask";
            this.txtTask.Size = new System.Drawing.Size(353, 100);
            this.txtTask.TabIndex = 6;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(3, 7);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(82, 14);
            this.lblUser.TabIndex = 17;
            this.lblUser.Text = "Assigned To :";
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(89, 3);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(303, 22);
            this.cmbUser.TabIndex = 3;
            this.cmbUser.SelectedIndexChanged += new System.EventHandler(this.cmbUser_SelectedIndexChanged);
            // 
            // lblTask
            // 
            this.lblTask.AutoSize = true;
            this.lblTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTask.Location = new System.Drawing.Point(45, 88);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(40, 14);
            this.lblTask.TabIndex = 19;
            this.lblTask.Text = "Task :";
            // 
            // tls_MaintainDoc
            // 
            this.tls_MaintainDoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Ok,
            this.tlb_Cancel,
            this.toolStripSeparator1,
            this.tlb_Review,
            this.tlb_History,
            this.tlb_Delete});
            this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.tls_MaintainDoc.Name = "tls_MaintainDoc";
            this.tls_MaintainDoc.Size = new System.Drawing.Size(484, 53);
            this.tls_MaintainDoc.TabIndex = 3;
            this.tls_MaintainDoc.TabStop = true;
            this.tls_MaintainDoc.Text = "toolStrip1";
            // 
            // tlb_Ok
            // 
            this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.Image")));
            this.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Ok.Name = "tlb_Ok";
            this.tlb_Ok.Size = new System.Drawing.Size(70, 50);
            this.tlb_Ok.Tag = "OK";
            this.tlb_Ok.Text = " &Save&&Cls";
            this.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Ok.ToolTipText = "Save and Close";
            this.tlb_Ok.Click += new System.EventHandler(this.tlb_Ok_Click);
            // 
            // tlb_Cancel
            // 
            this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Cancel.Image")));
            this.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Cancel.Name = "tlb_Cancel";
            this.tlb_Cancel.Size = new System.Drawing.Size(47, 50);
            this.tlb_Cancel.Tag = "Cancel";
            this.tlb_Cancel.Text = " &Close";
            this.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Cancel.ToolTipText = "Close";
            this.tlb_Cancel.Click += new System.EventHandler(this.tlb_Cancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            this.toolStripSeparator1.Visible = false;
            // 
            // tlb_Review
            // 
            this.tlb_Review.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Review.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Review.Image")));
            this.tlb_Review.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Review.Name = "tlb_Review";
            this.tlb_Review.Size = new System.Drawing.Size(59, 50);
            this.tlb_Review.Tag = "Review";
            this.tlb_Review.Text = " &Review";
            this.tlb_Review.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Review.ToolTipText = "Review";
            this.tlb_Review.Visible = false;
            // 
            // tlb_History
            // 
            this.tlb_History.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_History.Image = ((System.Drawing.Image)(resources.GetObject("tlb_History.Image")));
            this.tlb_History.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_History.Name = "tlb_History";
            this.tlb_History.Size = new System.Drawing.Size(59, 50);
            this.tlb_History.Tag = "History";
            this.tlb_History.Text = " &History";
            this.tlb_History.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_History.ToolTipText = "History";
            this.tlb_History.Visible = false;
            // 
            // tlb_Delete
            // 
            this.tlb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Delete.Image")));
            this.tlb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Delete.Name = "tlb_Delete";
            this.tlb_Delete.Size = new System.Drawing.Size(54, 50);
            this.tlb_Delete.Tag = "Delete";
            this.tlb_Delete.Text = " &Delete";
            this.tlb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Delete.ToolTipText = "Delete";
            this.tlb_Delete.Visible = false;
            // 
            // txtDocumentName
            // 
            this.txtDocumentName.Location = new System.Drawing.Point(104, 43);
            this.txtDocumentName.Name = "txtDocumentName";
            this.txtDocumentName.Size = new System.Drawing.Size(303, 22);
            this.txtDocumentName.TabIndex = 4;
            this.txtDocumentName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDocumentName_KeyUp);
            // 
            // lblDocName
            // 
            this.lblDocName.AutoSize = true;
            this.lblDocName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocName.Location = new System.Drawing.Point(29, 49);
            this.lblDocName.Name = "lblDocName";
            this.lblDocName.Size = new System.Drawing.Size(72, 14);
            this.lblDocName.TabIndex = 21;
            this.lblDocName.Text = "Document :";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(104, 73);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(303, 22);
            this.cmbCategory.TabIndex = 5;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(37, 77);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(64, 14);
            this.lblCategory.TabIndex = 22;
            this.lblCategory.Text = "Category :";
            // 
            // btnRemove_user
            // 
            this.btnRemove_user.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove_user.BackColor = System.Drawing.Color.Transparent;
            this.btnRemove_user.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemove_user.BackgroundImage")));
            this.btnRemove_user.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemove_user.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRemove_user.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnRemove_user.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnRemove_user.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove_user.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove_user.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove_user.Image")));
            this.btnRemove_user.Location = new System.Drawing.Point(420, 2);
            this.btnRemove_user.Name = "btnRemove_user";
            this.btnRemove_user.Size = new System.Drawing.Size(22, 22);
            this.btnRemove_user.TabIndex = 1;
            this.btnRemove_user.UseVisualStyleBackColor = false;
            this.btnRemove_user.Click += new System.EventHandler(this.btnRemove_user_Click);
            // 
            // btnbrowse
            // 
            this.btnbrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnbrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnbrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnbrowse.BackgroundImage")));
            this.btnbrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnbrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnbrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnbrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnbrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnbrowse.Image")));
            this.btnbrowse.Location = new System.Drawing.Point(395, 2);
            this.btnbrowse.Name = "btnbrowse";
            this.btnbrowse.Size = new System.Drawing.Size(22, 22);
            this.btnbrowse.TabIndex = 0;
            this.btnbrowse.UseVisualStyleBackColor = false;
            this.btnbrowse.Click += new System.EventHandler(this.btnbrowse_Click);
            // 
            // pnlTask
            // 
            this.pnlTask.BackColor = System.Drawing.Color.Transparent;
            this.pnlTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTask.Controls.Add(this.panel4);
            this.pnlTask.Controls.Add(this.panel2);
            this.pnlTask.Controls.Add(this.panel3);
            this.pnlTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTask.Location = new System.Drawing.Point(0, 0);
            this.pnlTask.Name = "pnlTask";
            this.pnlTask.Size = new System.Drawing.Size(484, 407);
            this.pnlTask.TabIndex = 0;
            this.pnlTask.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTask_Paint);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnPatientRemove);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.btnPatientBrowse);
            this.panel4.Controls.Add(this.txtPatient);
            this.panel4.Controls.Add(this.chkSendTask);
            this.panel4.Controls.Add(this.pnlTasksPanel);
            this.panel4.Controls.Add(this.cmbCategory);
            this.panel4.Controls.Add(this.lblDocName);
            this.panel4.Controls.Add(this.lblCategory);
            this.panel4.Controls.Add(this.txtDocumentName);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 54);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(484, 323);
            this.panel4.TabIndex = 0;
            // 
            // btnPatientRemove
            // 
            this.btnPatientRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPatientRemove.BackColor = System.Drawing.Color.Transparent;
            this.btnPatientRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPatientRemove.BackgroundImage")));
            this.btnPatientRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPatientRemove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPatientRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnPatientRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnPatientRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientRemove.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnPatientRemove.Image")));
            this.btnPatientRemove.Location = new System.Drawing.Point(435, 14);
            this.btnPatientRemove.Name = "btnPatientRemove";
            this.btnPatientRemove.Size = new System.Drawing.Size(22, 22);
            this.btnPatientRemove.TabIndex = 2;
            this.btnPatientRemove.UseVisualStyleBackColor = false;
            this.btnPatientRemove.Click += new System.EventHandler(this.btnPatientRemove_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(47, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 14);
            this.label13.TabIndex = 36;
            this.label13.Text = "Patient :";
            // 
            // btnPatientBrowse
            // 
            this.btnPatientBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPatientBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnPatientBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPatientBrowse.BackgroundImage")));
            this.btnPatientBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPatientBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPatientBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnPatientBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnPatientBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnPatientBrowse.Image")));
            this.btnPatientBrowse.Location = new System.Drawing.Point(410, 14);
            this.btnPatientBrowse.Name = "btnPatientBrowse";
            this.btnPatientBrowse.Size = new System.Drawing.Size(22, 22);
            this.btnPatientBrowse.TabIndex = 0;
            this.btnPatientBrowse.UseVisualStyleBackColor = false;
            this.btnPatientBrowse.Click += new System.EventHandler(this.btnPatientBrowse_Click);
            // 
            // txtPatient
            // 
            this.txtPatient.Location = new System.Drawing.Point(104, 14);
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.ReadOnly = true;
            this.txtPatient.Size = new System.Drawing.Size(303, 22);
            this.txtPatient.TabIndex = 3;
            // 
            // chkSendTask
            // 
            this.chkSendTask.AutoSize = true;
            this.chkSendTask.Checked = true;
            this.chkSendTask.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSendTask.Enabled = false;
            this.chkSendTask.Location = new System.Drawing.Point(104, 103);
            this.chkSendTask.Name = "chkSendTask";
            this.chkSendTask.Size = new System.Drawing.Size(83, 18);
            this.chkSendTask.TabIndex = 6;
            this.chkSendTask.Text = "Send Task";
            this.chkSendTask.UseVisualStyleBackColor = true;
            this.chkSendTask.CheckedChanged += new System.EventHandler(this.chkSendTask_CheckedChanged);
            // 
            // pnlTasksPanel
            // 
            this.pnlTasksPanel.Controls.Add(this.dtpDueDate);
            this.pnlTasksPanel.Controls.Add(this.lblUser);
            this.pnlTasksPanel.Controls.Add(this.cmbUser);
            this.pnlTasksPanel.Controls.Add(this.cmbPriority);
            this.pnlTasksPanel.Controls.Add(this.lblDuedate);
            this.pnlTasksPanel.Controls.Add(this.pnlpriority);
            this.pnlTasksPanel.Controls.Add(this.btnRemove_user);
            this.pnlTasksPanel.Controls.Add(this.txtTask);
            this.pnlTasksPanel.Controls.Add(this.btnbrowse);
            this.pnlTasksPanel.Controls.Add(this.lblTask);
            this.pnlTasksPanel.Location = new System.Drawing.Point(15, 125);
            this.pnlTasksPanel.Name = "pnlTasksPanel";
            this.pnlTasksPanel.Size = new System.Drawing.Size(455, 190);
            this.pnlTasksPanel.TabIndex = 7;
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDueDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDueDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDueDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDueDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDueDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDueDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDueDate.Location = new System.Drawing.Point(89, 57);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(112, 22);
            this.dtpDueDate.TabIndex = 5;
            // 
            // cmbPriority
            // 
            this.cmbPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Items.AddRange(new object[] {
            "Low",
            "Normal",
            "High"});
            this.cmbPriority.Location = new System.Drawing.Point(89, 30);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(303, 22);
            this.cmbPriority.TabIndex = 4;
            // 
            // lblDuedate
            // 
            this.lblDuedate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDuedate.AutoSize = true;
            this.lblDuedate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuedate.Location = new System.Drawing.Point(18, 60);
            this.lblDuedate.Name = "lblDuedate";
            this.lblDuedate.Size = new System.Drawing.Size(67, 14);
            this.lblDuedate.TabIndex = 28;
            this.lblDuedate.Text = "Due Date :";
            this.lblDuedate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlpriority
            // 
            this.pnlpriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlpriority.AutoSize = true;
            this.pnlpriority.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlpriority.Location = new System.Drawing.Point(33, 34);
            this.pnlpriority.Name = "pnlpriority";
            this.pnlpriority.Size = new System.Drawing.Size(52, 14);
            this.pnlpriority.TabIndex = 26;
            this.pnlpriority.Text = "Priority :";
            this.pnlpriority.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(4, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(476, 1);
            this.label3.TabIndex = 32;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(480, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 316);
            this.label2.TabIndex = 31;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 316);
            this.label1.TabIndex = 30;
            this.label1.Text = "label1";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(3, 319);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(478, 1);
            this.label4.TabIndex = 33;
            this.label4.Text = "label4";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.pbDocument);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 377);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(484, 30);
            this.panel2.TabIndex = 15555;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(4, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(476, 1);
            this.label6.TabIndex = 37;
            this.label6.Text = "label2";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 26);
            this.label7.TabIndex = 36;
            this.label7.Text = "label4";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(480, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 26);
            this.label8.TabIndex = 35;
            this.label8.Text = "label3";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(478, 1);
            this.label9.TabIndex = 34;
            this.label9.Text = "label1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel3.Controls.Add(this.tls_MaintainDoc);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(484, 54);
            this.panel3.TabIndex = 2;
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlGrid);
            this.pnlControl.Controls.Add(this.panel1);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControl.Location = new System.Drawing.Point(0, 0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(484, 407);
            this.pnlControl.TabIndex = 34;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.label12);
            this.pnlGrid.Controls.Add(this.label11);
            this.pnlGrid.Controls.Add(this.label10);
            this.pnlGrid.Controls.Add(this.label5);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 54);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(3);
            this.pnlGrid.Size = new System.Drawing.Size(484, 353);
            this.pnlGrid.TabIndex = 41;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 349);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(476, 1);
            this.label12.TabIndex = 40;
            this.label12.Text = "label1";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(476, 1);
            this.label11.TabIndex = 39;
            this.label11.Text = "label1";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(480, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 347);
            this.label10.TabIndex = 38;
            this.label10.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 347);
            this.label5.TabIndex = 37;
            this.label5.Text = "label4";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 54);
            this.panel1.TabIndex = 27;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_CntrOK,
            this.tlb_CntrCancel});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(484, 53);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlb_CntrOK
            // 
            this.tlb_CntrOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_CntrOK.Image = ((System.Drawing.Image)(resources.GetObject("tlb_CntrOK.Image")));
            this.tlb_CntrOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_CntrOK.Name = "tlb_CntrOK";
            this.tlb_CntrOK.Size = new System.Drawing.Size(66, 50);
            this.tlb_CntrOK.Tag = "CntrOK";
            this.tlb_CntrOK.Text = "&Save&&Cls";
            this.tlb_CntrOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_CntrOK.ToolTipText = "Save and Close";
            this.tlb_CntrOK.Click += new System.EventHandler(this.tlb_CntrOK_Click);
            // 
            // tlb_CntrCancel
            // 
            this.tlb_CntrCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_CntrCancel.Image = ((System.Drawing.Image)(resources.GetObject("tlb_CntrCancel.Image")));
            this.tlb_CntrCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_CntrCancel.Name = "tlb_CntrCancel";
            this.tlb_CntrCancel.Size = new System.Drawing.Size(47, 50);
            this.tlb_CntrCancel.Tag = "CntrCancel";
            this.tlb_CntrCancel.Text = " &Close";
            this.tlb_CntrCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_CntrCancel.ToolTipText = "Close";
            this.tlb_CntrCancel.Click += new System.EventHandler(this.tlb_CntrCancel_Click);
            // 
            // frmEDocEvent_SendToDMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(484, 407);
            this.Controls.Add(this.pnlTask);
            this.Controls.Add(this.pnlControl);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEDocEvent_SendToDMS";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Send To DMS";
            this.Load += new System.EventHandler(this.frmEDocEvent_SendToDMS_Load);
            this.tls_MaintainDoc.ResumeLayout(false);
            this.tls_MaintainDoc.PerformLayout();
            this.pnlTask.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTasksPanel.ResumeLayout(false);
            this.pnlTasksPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlControl.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

            }

            #endregion

            private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
            private System.Windows.Forms.ToolStripButton tlb_Ok;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            private System.Windows.Forms.ProgressBar pbDocument;
            private System.Windows.Forms.TextBox txtTask;
            private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            private System.Windows.Forms.ToolStripButton tlb_History;
            private System.Windows.Forms.ToolStripButton tlb_Delete;
            private System.Windows.Forms.ToolStripButton tlb_Review;
            private System.Windows.Forms.Label lblUser;
            private System.Windows.Forms.ComboBox cmbUser;
            private System.Windows.Forms.Label lblTask;
            private System.Windows.Forms.TextBox txtDocumentName;
            private System.Windows.Forms.Label lblDocName;
            private System.Windows.Forms.ComboBox cmbCategory;
            private System.Windows.Forms.Label lblCategory;
            internal System.Windows.Forms.Button btnRemove_user;
            internal System.Windows.Forms.Button btnbrowse;
            private System.Windows.Forms.Panel pnlTask;
            private System.Windows.Forms.Panel panel2;
            internal System.Windows.Forms.ComboBox cmbPriority;
            internal System.Windows.Forms.Label pnlpriority;
            internal System.Windows.Forms.DateTimePicker dtpDueDate;
            internal System.Windows.Forms.Label lblDuedate;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Panel pnlControl;
            private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
            private System.Windows.Forms.ToolStripButton tlb_CntrOK;
            private System.Windows.Forms.ToolStripButton tlb_CntrCancel;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Label label6;
            private System.Windows.Forms.Label label7;
            private System.Windows.Forms.Label label8;
            private System.Windows.Forms.Label label9;
            private System.Windows.Forms.Panel panel4;
            private System.Windows.Forms.Panel panel3;
            private System.Windows.Forms.Panel pnlGrid;
            private System.Windows.Forms.Label label12;
            private System.Windows.Forms.Label label11;
            private System.Windows.Forms.Label label10;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.Panel pnlTasksPanel;
            internal System.Windows.Forms.CheckBox chkSendTask;
            internal System.Windows.Forms.Button btnPatientRemove;
            private System.Windows.Forms.Label label13;
            internal System.Windows.Forms.Button btnPatientBrowse;
            private System.Windows.Forms.TextBox txtPatient;
        }
    }
