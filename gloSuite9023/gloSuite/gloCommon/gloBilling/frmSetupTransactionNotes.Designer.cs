namespace gloBilling
{
    partial class frmSetupTransactionNotes
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
                if (_tlTip == null)
                {
                    _tlTip.Dispose();
                    _tlTip = null;
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupTransactionNotes));
            this.tls_Notes = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Notes = new System.Windows.Forms.ToolStripButton();
            this.tlb_EditNotes = new System.Windows.Forms.ToolStripButton();
            this.tlb_History = new System.Windows.Forms.ToolStripButton();
            this.tlb_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_Save = new System.Windows.Forms.ToolStripButton();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lvwNotes = new System.Windows.Forms.ListView();
            this.lblVoidAllCharges = new System.Windows.Forms.Label();
            this.lblVoidNotes = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.btnSelectChargeTry = new System.Windows.Forms.Button();
            this.lblCloseDayTray = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.cmbCloseDayTray = new System.Windows.Forms.ComboBox();
            this.label72 = new System.Windows.Forms.Label();
            this.btnSetupJournal = new System.Windows.Forms.Button();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.btnModifyJournal = new System.Windows.Forms.Button();
            this.label90 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.pnlVoidsCriteria = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkNewClaim = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tls_Notes.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlVoidsCriteria.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_Notes
            // 
            this.tls_Notes.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_Notes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Notes.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Notes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Notes,
            this.tlb_EditNotes,
            this.tlb_History,
            this.tlb_Delete,
            this.toolStripSeparator1,
            this.tlb_Save,
            this.tlb_Ok,
            this.tlb_Cancel});
            this.tls_Notes.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Notes.Location = new System.Drawing.Point(0, 0);
            this.tls_Notes.Name = "tls_Notes";
            this.tls_Notes.Size = new System.Drawing.Size(542, 53);
            this.tls_Notes.TabIndex = 2;
            this.tls_Notes.TabStop = true;
            this.tls_Notes.Text = "toolStrip1";
            // 
            // tlb_Notes
            // 
            this.tlb_Notes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Notes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Notes.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Notes.Image")));
            this.tlb_Notes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Notes.Name = "tlb_Notes";
            this.tlb_Notes.Size = new System.Drawing.Size(73, 50);
            this.tlb_Notes.Tag = "Notes";
            this.tlb_Notes.Text = " Add &Note";
            this.tlb_Notes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Notes.ToolTipText = "Notes";
            this.tlb_Notes.Click += new System.EventHandler(this.tlb_Notes_Click);
            // 
            // tlb_EditNotes
            // 
            this.tlb_EditNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_EditNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_EditNotes.Image = ((System.Drawing.Image)(resources.GetObject("tlb_EditNotes.Image")));
            this.tlb_EditNotes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_EditNotes.Name = "tlb_EditNotes";
            this.tlb_EditNotes.Size = new System.Drawing.Size(72, 50);
            this.tlb_EditNotes.Tag = "Edit";
            this.tlb_EditNotes.Text = " &Edit Note";
            this.tlb_EditNotes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_EditNotes.ToolTipText = "Edit Note";
            this.tlb_EditNotes.Visible = false;
            this.tlb_EditNotes.Click += new System.EventHandler(this.tlb_EditNotes_Click);
            // 
            // tlb_History
            // 
            this.tlb_History.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_History.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_History.Image = ((System.Drawing.Image)(resources.GetObject("tlb_History.Image")));
            this.tlb_History.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_History.Name = "tlb_History";
            this.tlb_History.Size = new System.Drawing.Size(59, 50);
            this.tlb_History.Tag = "History";
            this.tlb_History.Text = " &History";
            this.tlb_History.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_History.Click += new System.EventHandler(this.tlb_History_Click);
            // 
            // tlb_Delete
            // 
            this.tlb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Delete.Image")));
            this.tlb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Delete.Name = "tlb_Delete";
            this.tlb_Delete.Size = new System.Drawing.Size(54, 50);
            this.tlb_Delete.Tag = " Delete";
            this.tlb_Delete.Text = " &Delete";
            this.tlb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Delete.Click += new System.EventHandler(this.tlb_Delete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            this.toolStripSeparator1.Visible = false;
            // 
            // tlb_Save
            // 
            this.tlb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Save.Image")));
            this.tlb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Save.Name = "tlb_Save";
            this.tlb_Save.Size = new System.Drawing.Size(40, 50);
            this.tlb_Save.Text = "&Save";
            this.tlb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Save.ToolTipText = "Save";
            this.tlb_Save.Click += new System.EventHandler(this.tlb_Save_Click);
            // 
            // tlb_Ok
            // 
            this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.Image")));
            this.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Ok.Name = "tlb_Ok";
            this.tlb_Ok.Size = new System.Drawing.Size(66, 50);
            this.tlb_Ok.Tag = "OK";
            this.tlb_Ok.Text = "Sa&ve&&Cls";
            this.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Ok.ToolTipText = "Save and Close";
            this.tlb_Ok.Click += new System.EventHandler(this.tlb_Ok_Click);
            // 
            // tlb_Cancel
            // 
            this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Cancel.Image")));
            this.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Cancel.Name = "tlb_Cancel";
            this.tlb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tlb_Cancel.Tag = "Cancel";
            this.tlb_Cancel.Text = "&Close";
            this.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Cancel.ToolTipText = "Close";
            this.tlb_Cancel.Click += new System.EventHandler(this.tlb_Cancel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label59);
            this.panel1.Controls.Add(this.txtNotes);
            this.panel1.Controls.Add(this.lvwNotes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 47);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(534, 262);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(6, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(522, 1);
            this.label3.TabIndex = 25;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(6, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(522, 1);
            this.label2.TabIndex = 24;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(528, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 252);
            this.label1.TabIndex = 23;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(5, 5);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 252);
            this.label59.TabIndex = 22;
            this.label59.Text = "label59";
            // 
            // txtNotes
            // 
            this.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotes.Location = new System.Drawing.Point(5, 5);
            this.txtNotes.MaxLength = 255;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(524, 252);
            this.txtNotes.TabIndex = 1;
            // 
            // lvwNotes
            // 
            this.lvwNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvwNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwNotes.FullRowSelect = true;
            this.lvwNotes.HideSelection = false;
            this.lvwNotes.Location = new System.Drawing.Point(5, 5);
            this.lvwNotes.MultiSelect = false;
            this.lvwNotes.Name = "lvwNotes";
            this.lvwNotes.Size = new System.Drawing.Size(524, 252);
            this.lvwNotes.TabIndex = 16;
            this.lvwNotes.UseCompatibleStateImageBehavior = false;
            this.lvwNotes.View = System.Windows.Forms.View.Details;
            // 
            // lblVoidAllCharges
            // 
            this.lblVoidAllCharges.AutoSize = true;
            this.lblVoidAllCharges.BackColor = System.Drawing.Color.Transparent;
            this.lblVoidAllCharges.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVoidAllCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoidAllCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblVoidAllCharges.Location = new System.Drawing.Point(4, 1);
            this.lblVoidAllCharges.Name = "lblVoidAllCharges";
            this.lblVoidAllCharges.Padding = new System.Windows.Forms.Padding(5, 5, 0, 3);
            this.lblVoidAllCharges.Size = new System.Drawing.Size(258, 22);
            this.lblVoidAllCharges.TabIndex = 209;
            this.lblVoidAllCharges.Text = "All Charges for this Claim will be Voided.";
            this.lblVoidAllCharges.Visible = false;
            // 
            // lblVoidNotes
            // 
            this.lblVoidNotes.AutoSize = true;
            this.lblVoidNotes.BackColor = System.Drawing.Color.Transparent;
            this.lblVoidNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVoidNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoidNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblVoidNotes.Location = new System.Drawing.Point(4, 23);
            this.lblVoidNotes.Name = "lblVoidNotes";
            this.lblVoidNotes.Padding = new System.Windows.Forms.Padding(5);
            this.lblVoidNotes.Size = new System.Drawing.Size(149, 24);
            this.lblVoidNotes.TabIndex = 210;
            this.lblVoidNotes.Text = "Notes for Voided Claim :";
            this.lblVoidNotes.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tls_Notes);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(542, 56);
            this.panel2.TabIndex = 213;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label29);
            this.panel7.Controls.Add(this.btnSelectChargeTry);
            this.panel7.Controls.Add(this.lblCloseDayTray);
            this.panel7.Controls.Add(this.label70);
            this.panel7.Controls.Add(this.label71);
            this.panel7.Controls.Add(this.cmbCloseDayTray);
            this.panel7.Controls.Add(this.label72);
            this.panel7.Controls.Add(this.btnSetupJournal);
            this.panel7.Controls.Add(this.mskCloseDate);
            this.panel7.Controls.Add(this.btnModifyJournal);
            this.panel7.Controls.Add(this.label90);
            this.panel7.Controls.Add(this.label48);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.Location = new System.Drawing.Point(3, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(536, 29);
            this.panel7.TabIndex = 19;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Left;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(0, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 27);
            this.label29.TabIndex = 7;
            this.label29.Text = "label4";
            // 
            // btnSelectChargeTry
            // 
            this.btnSelectChargeTry.AutoEllipsis = true;
            this.btnSelectChargeTry.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectChargeTry.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectChargeTry.BackgroundImage")));
            this.btnSelectChargeTry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectChargeTry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectChargeTry.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectChargeTry.Image")));
            this.btnSelectChargeTry.Location = new System.Drawing.Point(511, 3);
            this.btnSelectChargeTry.Name = "btnSelectChargeTry";
            this.btnSelectChargeTry.Size = new System.Drawing.Size(21, 22);
            this.btnSelectChargeTry.TabIndex = 2;
            this.btnSelectChargeTry.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSelectChargeTry.UseVisualStyleBackColor = false;
            this.btnSelectChargeTry.Click += new System.EventHandler(this.btnSelectChargeTry_Click);
            // 
            // lblCloseDayTray
            // 
            this.lblCloseDayTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCloseDayTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCloseDayTray.Location = new System.Drawing.Point(340, 7);
            this.lblCloseDayTray.Name = "lblCloseDayTray";
            this.lblCloseDayTray.Size = new System.Drawing.Size(140, 14);
            this.lblCloseDayTray.TabIndex = 208;
            this.lblCloseDayTray.Text = "Tray";
            this.lblCloseDayTray.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCloseDayTray.MouseEnter += new System.EventHandler(this.lblCloseDayTray_MouseEnter);
            this.lblCloseDayTray.MouseLeave += new System.EventHandler(this.lblCloseDayTray_MouseLeave);
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Right;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label70.Location = new System.Drawing.Point(535, 1);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(1, 27);
            this.label70.TabIndex = 6;
            this.label70.Text = "label3";
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Dock = System.Windows.Forms.DockStyle.Top;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(0, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(536, 1);
            this.label71.TabIndex = 5;
            this.label71.Text = "label1";
            // 
            // cmbCloseDayTray
            // 
            this.cmbCloseDayTray.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCloseDayTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbCloseDayTray.ForeColor = System.Drawing.Color.Black;
            this.cmbCloseDayTray.FormattingEnabled = true;
            this.cmbCloseDayTray.Items.AddRange(new object[] {
            ""});
            this.cmbCloseDayTray.Location = new System.Drawing.Point(338, 3);
            this.cmbCloseDayTray.Name = "cmbCloseDayTray";
            this.cmbCloseDayTray.Size = new System.Drawing.Size(111, 22);
            this.cmbCloseDayTray.TabIndex = 211;
            this.cmbCloseDayTray.Visible = false;
            this.cmbCloseDayTray.SelectedIndexChanged += new System.EventHandler(this.cmbCloseDayTray_SelectedIndexChanged);
            this.cmbCloseDayTray.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCloseDayTray_KeyPress);
            this.cmbCloseDayTray.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmbCloseDayTray_MouseDown);
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label72.Location = new System.Drawing.Point(0, 28);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(536, 1);
            this.label72.TabIndex = 8;
            this.label72.Text = "label2";
            // 
            // btnSetupJournal
            // 
            this.btnSetupJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetupJournal.AutoEllipsis = true;
            this.btnSetupJournal.BackColor = System.Drawing.Color.Transparent;
            this.btnSetupJournal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSetupJournal.BackgroundImage")));
            this.btnSetupJournal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSetupJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetupJournal.Image = ((System.Drawing.Image)(resources.GetObject("btnSetupJournal.Image")));
            this.btnSetupJournal.Location = new System.Drawing.Point(486, 3);
            this.btnSetupJournal.Name = "btnSetupJournal";
            this.btnSetupJournal.Size = new System.Drawing.Size(21, 22);
            this.btnSetupJournal.TabIndex = 3;
            this.btnSetupJournal.UseVisualStyleBackColor = false;
            this.btnSetupJournal.Visible = false;
            this.btnSetupJournal.Click += new System.EventHandler(this.btnSetupJournal_Click);
            // 
            // mskCloseDate
            // 
            this.mskCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskCloseDate.Location = new System.Drawing.Point(101, 3);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(78, 22);
            this.mskCloseDate.TabIndex = 1;
            this.mskCloseDate.TabStop = false;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            this.mskCloseDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskCloseDate_MouseClick);
            this.mskCloseDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskCloseDate_Validating);
            // 
            // btnModifyJournal
            // 
            this.btnModifyJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModifyJournal.AutoEllipsis = true;
            this.btnModifyJournal.BackColor = System.Drawing.Color.Transparent;
            this.btnModifyJournal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModifyJournal.BackgroundImage")));
            this.btnModifyJournal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModifyJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifyJournal.Image = ((System.Drawing.Image)(resources.GetObject("btnModifyJournal.Image")));
            this.btnModifyJournal.Location = new System.Drawing.Point(511, 3);
            this.btnModifyJournal.Name = "btnModifyJournal";
            this.btnModifyJournal.Size = new System.Drawing.Size(21, 22);
            this.btnModifyJournal.TabIndex = 4;
            this.btnModifyJournal.UseVisualStyleBackColor = false;
            this.btnModifyJournal.Visible = false;
            this.btnModifyJournal.Click += new System.EventHandler(this.btnModifyJournal_Click);
            // 
            // label90
            // 
            this.label90.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label90.AutoSize = true;
            this.label90.BackColor = System.Drawing.Color.Transparent;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label90.Location = new System.Drawing.Point(298, 7);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(43, 14);
            this.label90.TabIndex = 2;
            this.label90.Text = " Tray :";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label48
            // 
            this.label48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label48.AutoEllipsis = true;
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Location = new System.Drawing.Point(25, 7);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(73, 14);
            this.label48.TabIndex = 59;
            this.label48.Text = "Close Date :";
            // 
            // pnlVoidsCriteria
            // 
            this.pnlVoidsCriteria.Controls.Add(this.panel7);
            this.pnlVoidsCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVoidsCriteria.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlVoidsCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlVoidsCriteria.Location = new System.Drawing.Point(0, 56);
            this.pnlVoidsCriteria.Name = "pnlVoidsCriteria";
            this.pnlVoidsCriteria.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlVoidsCriteria.Size = new System.Drawing.Size(542, 32);
            this.pnlVoidsCriteria.TabIndex = 218;
            this.pnlVoidsCriteria.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.lblVoidNotes);
            this.panel3.Controls.Add(this.lblVoidAllCharges);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 88);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel3.Size = new System.Drawing.Size(542, 346);
            this.panel3.TabIndex = 219;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(4, 342);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(534, 1);
            this.label6.TabIndex = 25;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(534, 1);
            this.label7.TabIndex = 24;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(538, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 343);
            this.label8.TabIndex = 23;
            this.label8.Text = "label8";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 343);
            this.label9.TabIndex = 22;
            this.label9.Text = "label9";
            // 
            // chkNewClaim
            // 
            this.chkNewClaim.AutoSize = true;
            this.chkNewClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkNewClaim.Location = new System.Drawing.Point(11, 8);
            this.chkNewClaim.Name = "chkNewClaim";
            this.chkNewClaim.Size = new System.Drawing.Size(244, 18);
            this.chkNewClaim.TabIndex = 211;
            this.chkNewClaim.Text = "Replace void claim with a new claim";
            this.chkNewClaim.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.chkNewClaim);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(4, 309);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(534, 33);
            this.panel4.TabIndex = 212;
            // 
            // frmSetupTransactionNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(542, 434);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnlVoidsCriteria);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupTransactionNotes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaction Notes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetupTransactionNotes_FormClosing);
            this.Load += new System.EventHandler(this.frmSetupTransactionNotes_Load);
            this.Shown += new System.EventHandler(this.frmSetupTransactionNotes_Shown);
            this.tls_Notes.ResumeLayout(false);
            this.tls_Notes.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnlVoidsCriteria.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_Notes;
        private System.Windows.Forms.ToolStripButton tlb_Ok;
        private System.Windows.Forms.ToolStripButton tlb_Cancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlb_Notes;
        private System.Windows.Forms.ToolStripButton tlb_History;
        private System.Windows.Forms.ToolStripButton tlb_Delete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.ListView lvwNotes;
        private System.Windows.Forms.ToolStripButton tlb_EditNotes;
        internal System.Windows.Forms.ToolStripButton tlb_Save;
        private System.Windows.Forms.Label lblVoidAllCharges;
        private System.Windows.Forms.Label lblVoidNotes;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button btnSelectChargeTry;
        private System.Windows.Forms.Label lblCloseDayTray;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.ComboBox cmbCloseDayTray;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Button btnSetupJournal;
        private System.Windows.Forms.MaskedTextBox mskCloseDate;
        private System.Windows.Forms.Button btnModifyJournal;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label48;
        internal System.Windows.Forms.Panel pnlVoidsCriteria;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkNewClaim;
        private System.Windows.Forms.Panel panel4;
    }
}