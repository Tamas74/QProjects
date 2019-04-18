namespace ChargeRules
{
    partial class frmRuleEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRuleEditor));
            this.pnl_tlstrip = new System.Windows.Forms.Panel();
            this.tlsClaimRule = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsCR_Save = new System.Windows.Forms.ToolStripButton();
            this.tlsCR_Close = new System.Windows.Forms.ToolStripButton();
            this.cmbMaritalSt = new System.Windows.Forms.ComboBox();
            this.cmbRace = new System.Windows.Forms.ComboBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.pnlMsg = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.rdbInformation = new System.Windows.Forms.RadioButton();
            this.rdbWarning = new System.Windows.Forms.RadioButton();
            this.txtErrorMessage = new System.Windows.Forms.TextBox();
            this.rdbError = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label63 = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.trvProperties = new System.Windows.Forms.TreeView();
            this.imgLst = new System.Windows.Forms.ImageList(this.components);
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlRuleGrid = new System.Windows.Forms.Panel();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.c1RuleGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlListControl = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.rdbOr = new System.Windows.Forms.RadioButton();
            this.rdbAnd = new System.Windows.Forms.RadioButton();
            this.label28 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.txtRuleExpression = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.rdbCustom = new System.Windows.Forms.RadioButton();
            this.C1SuperTooltipDx = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.cntRuleCondtionInsert = new System.Windows.Forms.ContextMenu();
            this.mnuInsertAboveSlectedRow = new System.Windows.Forms.MenuItem();
            this.mnuInsertBelowSlectedRow = new System.Windows.Forms.MenuItem();
            this.pnl_tlstrip.SuspendLayout();
            this.tlsClaimRule.SuspendLayout();
            this.pnlMsg.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlRuleGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1RuleGrid)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlstrip
            // 
            this.pnl_tlstrip.AutoSize = true;
            this.pnl_tlstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_tlstrip.Controls.Add(this.tlsClaimRule);
            this.pnl_tlstrip.Controls.Add(this.cmbMaritalSt);
            this.pnl_tlstrip.Controls.Add(this.cmbRace);
            this.pnl_tlstrip.Controls.Add(this.cmbGender);
            this.pnl_tlstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlstrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlstrip.Name = "pnl_tlstrip";
            this.pnl_tlstrip.Size = new System.Drawing.Size(1113, 53);
            this.pnl_tlstrip.TabIndex = 5;
            this.pnl_tlstrip.TabStop = true;
            // 
            // tlsClaimRule
            // 
            this.tlsClaimRule.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsClaimRule.BackgroundImage")));
            this.tlsClaimRule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsClaimRule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsClaimRule.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsClaimRule.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsCR_Save,
            this.tlsCR_Close});
            this.tlsClaimRule.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsClaimRule.Location = new System.Drawing.Point(0, 0);
            this.tlsClaimRule.Name = "tlsClaimRule";
            this.tlsClaimRule.Size = new System.Drawing.Size(1113, 53);
            this.tlsClaimRule.TabIndex = 0;
            this.tlsClaimRule.Text = "ToolStrip1";
            // 
            // tlsCR_Save
            // 
            this.tlsCR_Save.Image = ((System.Drawing.Image)(resources.GetObject("tlsCR_Save.Image")));
            this.tlsCR_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsCR_Save.Name = "tlsCR_Save";
            this.tlsCR_Save.Size = new System.Drawing.Size(66, 50);
            this.tlsCR_Save.Tag = "Save";
            this.tlsCR_Save.Text = "&Save&&Cls";
            this.tlsCR_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsCR_Save.ToolTipText = "Save and Close";
            this.tlsCR_Save.Click += new System.EventHandler(this.tlsDM_Save_Click);
            // 
            // tlsCR_Close
            // 
            this.tlsCR_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlsCR_Close.Image")));
            this.tlsCR_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsCR_Close.Name = "tlsCR_Close";
            this.tlsCR_Close.Size = new System.Drawing.Size(43, 50);
            this.tlsCR_Close.Tag = "Close";
            this.tlsCR_Close.Text = "&Close";
            this.tlsCR_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsCR_Close.Click += new System.EventHandler(this.tlsDM_Close_Click);
            // 
            // cmbMaritalSt
            // 
            this.cmbMaritalSt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaritalSt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMaritalSt.ForeColor = System.Drawing.Color.Black;
            this.cmbMaritalSt.Location = new System.Drawing.Point(776, 13);
            this.cmbMaritalSt.Name = "cmbMaritalSt";
            this.cmbMaritalSt.Size = new System.Drawing.Size(48, 22);
            this.cmbMaritalSt.TabIndex = 7;
            this.cmbMaritalSt.Visible = false;
            // 
            // cmbRace
            // 
            this.cmbRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRace.ForeColor = System.Drawing.Color.Black;
            this.cmbRace.Location = new System.Drawing.Point(888, 13);
            this.cmbRace.Name = "cmbRace";
            this.cmbRace.Size = new System.Drawing.Size(48, 22);
            this.cmbRace.TabIndex = 5;
            this.cmbRace.Visible = false;
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGender.ForeColor = System.Drawing.Color.Black;
            this.cmbGender.Location = new System.Drawing.Point(832, 13);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(48, 22);
            this.cmbGender.TabIndex = 3;
            this.cmbGender.Visible = false;
            // 
            // pnlMsg
            // 
            this.pnlMsg.BackColor = System.Drawing.Color.Transparent;
            this.pnlMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMsg.Controls.Add(this.label22);
            this.pnlMsg.Controls.Add(this.label21);
            this.pnlMsg.Controls.Add(this.rdbInformation);
            this.pnlMsg.Controls.Add(this.rdbWarning);
            this.pnlMsg.Controls.Add(this.txtErrorMessage);
            this.pnlMsg.Controls.Add(this.rdbError);
            this.pnlMsg.Controls.Add(this.label18);
            this.pnlMsg.Controls.Add(this.label19);
            this.pnlMsg.Controls.Add(this.txtName);
            this.pnlMsg.Controls.Add(this.Label3);
            this.pnlMsg.Controls.Add(this.Label23);
            this.pnlMsg.Controls.Add(this.Label24);
            this.pnlMsg.Controls.Add(this.Label63);
            this.pnlMsg.Controls.Add(this.Label25);
            this.pnlMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMsg.Location = new System.Drawing.Point(0, 53);
            this.pnlMsg.Name = "pnlMsg";
            this.pnlMsg.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMsg.Size = new System.Drawing.Size(1113, 122);
            this.pnlMsg.TabIndex = 0;
            // 
            // label22
            // 
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(49, 63);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(12, 10);
            this.label22.TabIndex = 25;
            this.label22.Text = "*";
            // 
            // label21
            // 
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(94, 36);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(12, 10);
            this.label21.TabIndex = 24;
            this.label21.Text = "*";
            // 
            // rdbInformation
            // 
            this.rdbInformation.AutoSize = true;
            this.rdbInformation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbInformation.Location = new System.Drawing.Point(308, 9);
            this.rdbInformation.Name = "rdbInformation";
            this.rdbInformation.Size = new System.Drawing.Size(88, 18);
            this.rdbInformation.TabIndex = 2;
            this.rdbInformation.TabStop = true;
            this.rdbInformation.Text = "Information";
            this.rdbInformation.UseVisualStyleBackColor = true;
            // 
            // rdbWarning
            // 
            this.rdbWarning.AutoSize = true;
            this.rdbWarning.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbWarning.Location = new System.Drawing.Point(221, 9);
            this.rdbWarning.Name = "rdbWarning";
            this.rdbWarning.Size = new System.Drawing.Size(70, 18);
            this.rdbWarning.TabIndex = 1;
            this.rdbWarning.TabStop = true;
            this.rdbWarning.Text = "Warning";
            this.rdbWarning.UseVisualStyleBackColor = true;
            // 
            // txtErrorMessage
            // 
            this.txtErrorMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtErrorMessage.ForeColor = System.Drawing.Color.Black;
            this.txtErrorMessage.Location = new System.Drawing.Point(155, 61);
            this.txtErrorMessage.MaxLength = 255;
            this.txtErrorMessage.Multiline = true;
            this.txtErrorMessage.Name = "txtErrorMessage";
            this.txtErrorMessage.Size = new System.Drawing.Size(886, 51);
            this.txtErrorMessage.TabIndex = 4;
            // 
            // rdbError
            // 
            this.rdbError.AutoSize = true;
            this.rdbError.Checked = true;
            this.rdbError.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbError.Location = new System.Drawing.Point(155, 9);
            this.rdbError.Name = "rdbError";
            this.rdbError.Size = new System.Drawing.Size(51, 18);
            this.rdbError.TabIndex = 0;
            this.rdbError.TabStop = true;
            this.rdbError.Text = "Error";
            this.rdbError.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(60, 64);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(91, 14);
            this.label18.TabIndex = 23;
            this.label18.Text = "Error Message :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(81, 11);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 14);
            this.label19.TabIndex = 22;
            this.label19.Text = "Rule Type :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(155, 33);
            this.txtName.MaxLength = 255;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(886, 22);
            this.txtName.TabIndex = 3;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(97, 37);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(54, 14);
            this.Label3.TabIndex = 11;
            this.Label3.Text = "  Name :";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label23
            // 
            this.Label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label23.Location = new System.Drawing.Point(4, 118);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(1105, 1);
            this.Label23.TabIndex = 17;
            this.Label23.Text = "label2";
            // 
            // Label24
            // 
            this.Label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label24.Location = new System.Drawing.Point(3, 4);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(1, 115);
            this.Label24.TabIndex = 16;
            this.Label24.Text = "label4";
            // 
            // Label63
            // 
            this.Label63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label63.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label63.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label63.Location = new System.Drawing.Point(1109, 4);
            this.Label63.Name = "Label63";
            this.Label63.Size = new System.Drawing.Size(1, 115);
            this.Label63.TabIndex = 15;
            this.Label63.Text = "label3";
            // 
            // Label25
            // 
            this.Label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.Location = new System.Drawing.Point(3, 3);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(1107, 1);
            this.Label25.TabIndex = 21;
            this.Label25.Text = "label1";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(80, 622);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(75, 14);
            this.Label4.TabIndex = 13;
            this.Label4.Text = "Description :";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label4.Visible = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(159, 615);
            this.txtDescription.MaxLength = 1500;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescription.Size = new System.Drawing.Size(594, 51);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.trvProperties);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 543);
            this.panel1.TabIndex = 23;
            // 
            // trvProperties
            // 
            this.trvProperties.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvProperties.ImageIndex = 0;
            this.trvProperties.ImageList = this.imgLst;
            this.trvProperties.Location = new System.Drawing.Point(4, 4);
            this.trvProperties.Name = "trvProperties";
            this.trvProperties.SelectedImageIndex = 0;
            this.trvProperties.ShowLines = false;
            this.trvProperties.ShowPlusMinus = false;
            this.trvProperties.Size = new System.Drawing.Size(289, 538);
            this.trvProperties.TabIndex = 22;
            this.trvProperties.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvProperties_NodeMouseDoubleClick);
            this.trvProperties.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvProperties_MouseDown);
            // 
            // imgLst
            // 
            this.imgLst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLst.ImageStream")));
            this.imgLst.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLst.Images.SetKeyName(0, "Arrow.ico");
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(3, 538);
            this.label16.TabIndex = 24;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(292, 3);
            this.label15.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(1, 542);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(292, 1);
            this.label7.TabIndex = 17;
            this.label7.Text = "label2";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 542);
            this.label8.TabIndex = 16;
            this.label8.Text = "label4";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(293, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 542);
            this.label9.TabIndex = 15;
            this.label9.Text = "label3";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(294, 1);
            this.label10.TabIndex = 21;
            this.label10.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlRuleGrid);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 214);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(1113, 546);
            this.panel2.TabIndex = 3;
            // 
            // pnlRuleGrid
            // 
            this.pnlRuleGrid.BackColor = System.Drawing.Color.Transparent;
            this.pnlRuleGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRuleGrid.Controls.Add(this.pnlInternalControl);
            this.pnlRuleGrid.Controls.Add(this.c1RuleGrid);
            this.pnlRuleGrid.Controls.Add(this.label1);
            this.pnlRuleGrid.Controls.Add(this.label2);
            this.pnlRuleGrid.Controls.Add(this.label5);
            this.pnlRuleGrid.Controls.Add(this.label6);
            this.pnlRuleGrid.Controls.Add(this.pnlListControl);
            this.pnlRuleGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRuleGrid.Location = new System.Drawing.Point(300, 33);
            this.pnlRuleGrid.Name = "pnlRuleGrid";
            this.pnlRuleGrid.Size = new System.Drawing.Size(810, 510);
            this.pnlRuleGrid.TabIndex = 23;
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(237, 162);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(337, 211);
            this.pnlInternalControl.TabIndex = 24;
            this.pnlInternalControl.Visible = false;
            // 
            // c1RuleGrid
            // 
            this.c1RuleGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows;
            this.c1RuleGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1RuleGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1RuleGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1RuleGrid.ColumnInfo = resources.GetString("c1RuleGrid.ColumnInfo");
            this.c1RuleGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1RuleGrid.DragMode = C1.Win.C1FlexGrid.DragModeEnum.AutomaticMove;
            this.c1RuleGrid.ExtendLastCol = true;
            this.c1RuleGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1RuleGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1RuleGrid.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1RuleGrid.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1RuleGrid.Location = new System.Drawing.Point(1, 1);
            this.c1RuleGrid.Name = "c1RuleGrid";
            this.c1RuleGrid.Rows.Count = 1;
            this.c1RuleGrid.Rows.DefaultSize = 18;
            this.c1RuleGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1RuleGrid.Size = new System.Drawing.Size(808, 508);
            this.c1RuleGrid.StyleInfo = resources.GetString("c1RuleGrid.StyleInfo");
            this.c1RuleGrid.TabIndex = 22;
            this.c1RuleGrid.BeforeScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1RuleGrid_BeforeScroll);
            this.c1RuleGrid.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1RuleGrid_AfterScroll);
            this.c1RuleGrid.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1RuleGrid_StartEdit);
            this.c1RuleGrid.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1RuleGrid_AfterEdit);
            this.c1RuleGrid.LeaveEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1RuleGrid_LeaveEdit);
            this.c1RuleGrid.ChangeEdit += new System.EventHandler(this.c1RuleGrid_ChangeEdit);
            this.c1RuleGrid.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.c1RuleGrid_KeyPressEdit);
            this.c1RuleGrid.Click += new System.EventHandler(this.c1RuleGrid_Click);
            this.c1RuleGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1RuleGrid_KeyUp);
            this.c1RuleGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1RuleGrid_MouseDown);
            this.c1RuleGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1RuleGrid_MouseMove);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(808, 1);
            this.label1.TabIndex = 17;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 509);
            this.label2.TabIndex = 16;
            this.label2.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(809, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 509);
            this.label5.TabIndex = 15;
            this.label5.Text = "label3";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(810, 1);
            this.label6.TabIndex = 21;
            this.label6.Text = "label1";
            // 
            // pnlListControl
            // 
            this.pnlListControl.AutoScroll = true;
            this.pnlListControl.AutoSize = true;
            this.pnlListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlListControl.Location = new System.Drawing.Point(0, 0);
            this.pnlListControl.Name = "pnlListControl";
            this.pnlListControl.Size = new System.Drawing.Size(810, 510);
            this.pnlListControl.TabIndex = 26;
            this.pnlListControl.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.label27);
            this.panel3.Controls.Add(this.rdbOr);
            this.panel3.Controls.Add(this.rdbAnd);
            this.panel3.Controls.Add(this.label28);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.label29);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(300, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel3.Size = new System.Drawing.Size(810, 33);
            this.panel3.TabIndex = 0;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label26.Location = new System.Drawing.Point(1, 29);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(808, 1);
            this.label26.TabIndex = 17;
            this.label26.Text = "label2";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Left;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(0, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 29);
            this.label27.TabIndex = 16;
            this.label27.Text = "label4";
            // 
            // rdbOr
            // 
            this.rdbOr.AutoSize = true;
            this.rdbOr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbOr.Location = new System.Drawing.Point(251, 6);
            this.rdbOr.Name = "rdbOr";
            this.rdbOr.Size = new System.Drawing.Size(41, 18);
            this.rdbOr.TabIndex = 1;
            this.rdbOr.TabStop = true;
            this.rdbOr.Text = "OR";
            this.rdbOr.UseVisualStyleBackColor = true;
            // 
            // rdbAnd
            // 
            this.rdbAnd.AutoSize = true;
            this.rdbAnd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbAnd.Location = new System.Drawing.Point(185, 6);
            this.rdbAnd.Name = "rdbAnd";
            this.rdbAnd.Size = new System.Drawing.Size(49, 18);
            this.rdbAnd.TabIndex = 0;
            this.rdbAnd.TabStop = true;
            this.rdbAnd.Text = "AND";
            this.rdbAnd.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Right;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label28.Location = new System.Drawing.Point(809, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 29);
            this.label28.TabIndex = 15;
            this.label28.Text = "label3";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(14, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(164, 14);
            this.label17.TabIndex = 14;
            this.label17.Text = "Default Condition Predicate :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Top;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(0, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(810, 1);
            this.label29.TabIndex = 21;
            this.label29.Text = "label1";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(297, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 543);
            this.splitter1.TabIndex = 25;
            this.splitter1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.txtRuleExpression);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 175);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel4.Size = new System.Drawing.Size(1113, 39);
            this.panel4.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(52, 10);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(99, 14);
            this.label20.TabIndex = 26;
            this.label20.Text = "Rule Expression :";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRuleExpression
            // 
            this.txtRuleExpression.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRuleExpression.ForeColor = System.Drawing.Color.Black;
            this.txtRuleExpression.Location = new System.Drawing.Point(155, 6);
            this.txtRuleExpression.MaxLength = 255;
            this.txtRuleExpression.Name = "txtRuleExpression";
            this.txtRuleExpression.Size = new System.Drawing.Size(886, 22);
            this.txtRuleExpression.TabIndex = 18;
            this.txtRuleExpression.TabStop = false;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(4, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1105, 1);
            this.label11.TabIndex = 17;
            this.label11.Text = "label2";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 35);
            this.label12.TabIndex = 16;
            this.label12.Text = "label4";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(1109, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 35);
            this.label13.TabIndex = 15;
            this.label13.Text = "label3";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1107, 1);
            this.label14.TabIndex = 21;
            this.label14.Text = "label1";
            // 
            // rdbCustom
            // 
            this.rdbCustom.AutoSize = true;
            this.rdbCustom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbCustom.Location = new System.Drawing.Point(60, 587);
            this.rdbCustom.Name = "rdbCustom";
            this.rdbCustom.Size = new System.Drawing.Size(73, 18);
            this.rdbCustom.TabIndex = 17;
            this.rdbCustom.TabStop = true;
            this.rdbCustom.Text = "CUSTOM";
            this.rdbCustom.UseVisualStyleBackColor = true;
            this.rdbCustom.Visible = false;
            // 
            // C1SuperTooltipDx
            // 
            this.C1SuperTooltipDx.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltipDx.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // cntRuleCondtionInsert
            // 
            this.cntRuleCondtionInsert.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuInsertAboveSlectedRow,
            this.mnuInsertBelowSlectedRow});
            // 
            // mnuInsertAboveSlectedRow
            // 
            this.mnuInsertAboveSlectedRow.Index = 0;
            this.mnuInsertAboveSlectedRow.Text = "Insert Above Selected Rule Condition";
            this.mnuInsertAboveSlectedRow.Click += new System.EventHandler(this.mnuInsertAboveSlectedRow_Click);
            // 
            // mnuInsertBelowSlectedRow
            // 
            this.mnuInsertBelowSlectedRow.Index = 1;
            this.mnuInsertBelowSlectedRow.Text = "Insert Below Selected Rule condition";
            this.mnuInsertBelowSlectedRow.Click += new System.EventHandler(this.mnuInsertBelowSlectedRow_Click);
            // 
            // frmRuleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1113, 760);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.rdbCustom);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pnlMsg);
            this.Controls.Add(this.pnl_tlstrip);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.txtDescription);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1119, 726);
            this.Name = "frmRuleEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rule Editor";
            this.Load += new System.EventHandler(this.frmRuleEditor_Load);
            this.pnl_tlstrip.ResumeLayout(false);
            this.pnl_tlstrip.PerformLayout();
            this.tlsClaimRule.ResumeLayout(false);
            this.tlsClaimRule.PerformLayout();
            this.pnlMsg.ResumeLayout(false);
            this.pnlMsg.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlRuleGrid.ResumeLayout(false);
            this.pnlRuleGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1RuleGrid)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Panel pnl_tlstrip;
        internal gloGlobal.gloToolStripIgnoreFocus tlsClaimRule;
        internal System.Windows.Forms.ToolStripButton tlsCR_Save;
        internal System.Windows.Forms.ToolStripButton tlsCR_Close;
        internal System.Windows.Forms.ComboBox cmbMaritalSt;
        internal System.Windows.Forms.ComboBox cmbRace;
        internal System.Windows.Forms.ComboBox cmbGender;
        internal System.Windows.Forms.Panel pnlMsg;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.TextBox txtDescription;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label23;
        private System.Windows.Forms.Label Label24;
        private System.Windows.Forms.Label Label63;
        private System.Windows.Forms.Label Label25;
        internal System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView trvProperties;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel pnlRuleGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Splitter splitter1;
        internal System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private C1.Win.C1FlexGrid.C1FlexGrid c1RuleGrid;
        internal System.Windows.Forms.TextBox txtRuleExpression;
        private System.Windows.Forms.RadioButton rdbCustom;
        internal System.Windows.Forms.TextBox txtErrorMessage;
        private System.Windows.Forms.RadioButton rdbOr;
        internal System.Windows.Forms.Label label18;
        private System.Windows.Forms.RadioButton rdbAnd;
        internal System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.RadioButton rdbInformation;
        private System.Windows.Forms.RadioButton rdbWarning;
        private System.Windows.Forms.RadioButton rdbError;
        internal System.Windows.Forms.Label label19;
        private System.Windows.Forms.ImageList imgLst;
        internal System.Windows.Forms.Label label20;
        internal System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltipDx;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        internal System.Windows.Forms.ContextMenu cntRuleCondtionInsert;
        internal System.Windows.Forms.MenuItem mnuInsertAboveSlectedRow;
        internal System.Windows.Forms.MenuItem mnuInsertBelowSlectedRow;
        private System.Windows.Forms.Panel pnlListControl;
    }
}

