namespace gloBilling.gloERA
{
    partial class frmERAPayerSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmERAPayerSetup));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Main = new System.Windows.Forms.ToolStrip();
            this.tsb_Add = new System.Windows.Forms.ToolStripButton();
            this.tsb_Remove = new System.Windows.Forms.ToolStripButton();
            this.tsb_Import = new System.Windows.Forms.ToolStripButton();
            this.tsb_Reset = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveClose = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlMST = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkUseClaimStatus = new System.Windows.Forms.CheckBox();
            this.chkSecondaryAdjst = new System.Windows.Forms.CheckBox();
            this.chkActivate = new System.Windows.Forms.CheckBox();
            this.txtPayerID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPaid = new System.Windows.Forms.ComboBox();
            this.cmbZBilled = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbZNotBilled = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.C1StandardCAS = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlStandardCAS = new System.Windows.Forms.Panel();
            this.pnlStandardCASLabel = new System.Windows.Forms.Panel();
            this.lblStandardCAS = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.C1OtherCAS = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlOtherCASLabel = new System.Windows.Forms.Panel();
            this.lblOtherCAS = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tbCAS = new System.Windows.Forms.TabControl();
            this.tb_StandardCAS = new System.Windows.Forms.TabPage();
            this.tb_OtherCAS = new System.Windows.Forms.TabPage();
            this.pnlTab = new System.Windows.Forms.Panel();
            this.mnuSetup = new System.Windows.Forms.MenuStrip();
            this.mnuItemAddLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemRemoveLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemSaveClose = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlShortcuts = new System.Windows.Forms.Panel();
            this.label34 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Main.SuspendLayout();
            this.pnlMST.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1StandardCAS)).BeginInit();
            this.pnlStandardCAS.SuspendLayout();
            this.pnlStandardCASLabel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1OtherCAS)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlOtherCASLabel.SuspendLayout();
            this.tbCAS.SuspendLayout();
            this.tb_StandardCAS.SuspendLayout();
            this.tb_OtherCAS.SuspendLayout();
            this.pnlTab.SuspendLayout();
            this.mnuSetup.SuspendLayout();
            this.pnlShortcuts.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Main);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(623, 54);
            this.pnlToolStrip.TabIndex = 21;
            // 
            // tls_Main
            // 
            this.tls_Main.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Main.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Add,
            this.tsb_Remove,
            this.tsb_Import,
            this.tsb_Reset,
            this.tsb_SaveClose,
            this.tsb_Close});
            this.tls_Main.Location = new System.Drawing.Point(0, 0);
            this.tls_Main.Name = "tls_Main";
            this.tls_Main.Size = new System.Drawing.Size(623, 53);
            this.tls_Main.TabIndex = 0;
            this.tls_Main.Text = "toolStrip1";
            // 
            // tsb_Add
            // 
            this.tsb_Add.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Add.Image")));
            this.tsb_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Add.Name = "tsb_Add";
            this.tsb_Add.Size = new System.Drawing.Size(65, 50);
            this.tsb_Add.Tag = "Add";
            this.tsb_Add.Text = "&Add Line";
            this.tsb_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Add.ToolTipText = "Add Line";
            this.tsb_Add.Click += new System.EventHandler(this.tsb_Add_Click);
            // 
            // tsb_Remove
            // 
            this.tsb_Remove.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Remove.Image")));
            this.tsb_Remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Remove.Name = "tsb_Remove";
            this.tsb_Remove.Size = new System.Drawing.Size(89, 50);
            this.tsb_Remove.Tag = "Remove";
            this.tsb_Remove.Text = "Re&move Line";
            this.tsb_Remove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Remove.ToolTipText = "Remove Line";
            this.tsb_Remove.Click += new System.EventHandler(this.tsb_Remove_Click);
            // 
            // tsb_Import
            // 
            this.tsb_Import.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Import.Image")));
            this.tsb_Import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Import.Name = "tsb_Import";
            this.tsb_Import.Size = new System.Drawing.Size(80, 50);
            this.tsb_Import.Tag = "Import";
            this.tsb_Import.Text = "&Copy Payer";
            this.tsb_Import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Import.ToolTipText = "Copy Payer Settings";
            this.tsb_Import.Click += new System.EventHandler(this.tsb_Import_Click);
            // 
            // tsb_Reset
            // 
            this.tsb_Reset.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Reset.Image")));
            this.tsb_Reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Reset.Name = "tsb_Reset";
            this.tsb_Reset.Size = new System.Drawing.Size(46, 50);
            this.tsb_Reset.Tag = "Reset";
            this.tsb_Reset.Text = "&Reset";
            this.tsb_Reset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Reset.ToolTipText = "Reset Settings to Default";
            this.tsb_Reset.Click += new System.EventHandler(this.tsb_Reset_Click);
            // 
            // tsb_SaveClose
            // 
            this.tsb_SaveClose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveClose.Image")));
            this.tsb_SaveClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveClose.Name = "tsb_SaveClose";
            this.tsb_SaveClose.Size = new System.Drawing.Size(66, 50);
            this.tsb_SaveClose.Tag = "SaveClose";
            this.tsb_SaveClose.Text = "Sa&ve&&Cls";
            this.tsb_SaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SaveClose.ToolTipText = "Save and Close";
            this.tsb_SaveClose.Click += new System.EventHandler(this.tsb_SaveClose_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // pnlMST
            // 
            this.pnlMST.Controls.Add(this.label10);
            this.pnlMST.Controls.Add(this.label9);
            this.pnlMST.Controls.Add(this.label4);
            this.pnlMST.Controls.Add(this.label8);
            this.pnlMST.Controls.Add(this.chkUseClaimStatus);
            this.pnlMST.Controls.Add(this.chkSecondaryAdjst);
            this.pnlMST.Controls.Add(this.chkActivate);
            this.pnlMST.Controls.Add(this.txtPayerID);
            this.pnlMST.Controls.Add(this.label1);
            this.pnlMST.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMST.Location = new System.Drawing.Point(0, 54);
            this.pnlMST.Name = "pnlMST";
            this.pnlMST.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMST.Size = new System.Drawing.Size(623, 103);
            this.pnlMST.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(4, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(615, 1);
            this.label10.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(4, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(615, 1);
            this.label9.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(619, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 97);
            this.label4.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 97);
            this.label8.TabIndex = 22;
            // 
            // chkUseClaimStatus
            // 
            this.chkUseClaimStatus.AutoSize = true;
            this.chkUseClaimStatus.Location = new System.Drawing.Point(108, 56);
            this.chkUseClaimStatus.Name = "chkUseClaimStatus";
            this.chkUseClaimStatus.Size = new System.Drawing.Size(297, 18);
            this.chkUseClaimStatus.TabIndex = 2;
            this.chkUseClaimStatus.Text = "Use Claim Status (When determining paying plan)";
            this.chkUseClaimStatus.UseVisualStyleBackColor = true;
            this.chkUseClaimStatus.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // chkSecondaryAdjst
            // 
            this.chkSecondaryAdjst.AutoSize = true;
            this.chkSecondaryAdjst.Location = new System.Drawing.Point(108, 77);
            this.chkSecondaryAdjst.Name = "chkSecondaryAdjst";
            this.chkSecondaryAdjst.Size = new System.Drawing.Size(184, 18);
            this.chkSecondaryAdjst.TabIndex = 3;
            this.chkSecondaryAdjst.Text = "Post Secondary Adjustments";
            this.chkSecondaryAdjst.UseVisualStyleBackColor = true;
            this.chkSecondaryAdjst.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // chkActivate
            // 
            this.chkActivate.AutoSize = true;
            this.chkActivate.Location = new System.Drawing.Point(108, 35);
            this.chkActivate.Name = "chkActivate";
            this.chkActivate.Size = new System.Drawing.Size(105, 18);
            this.chkActivate.TabIndex = 1;
            this.chkActivate.Text = "Activate Payer";
            this.chkActivate.UseVisualStyleBackColor = true;
            this.chkActivate.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // txtPayerID
            // 
            this.txtPayerID.Location = new System.Drawing.Point(107, 8);
            this.txtPayerID.MaxLength = 100;
            this.txtPayerID.Name = "txtPayerID";
            this.txtPayerID.Size = new System.Drawing.Size(222, 22);
            this.txtPayerID.TabIndex = 0;
            this.txtPayerID.TextChanged += new System.EventHandler(this.txtPayerID_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "ERA Payer ID :";
            // 
            // cmbPaid
            // 
            this.cmbPaid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaid.FormattingEnabled = true;
            this.cmbPaid.Location = new System.Drawing.Point(282, 71);
            this.cmbPaid.Name = "cmbPaid";
            this.cmbPaid.Size = new System.Drawing.Size(157, 22);
            this.cmbPaid.TabIndex = 2;
            this.cmbPaid.SelectedIndexChanged += new System.EventHandler(this.cmbAll_SelectedIndexChanged);
            // 
            // cmbZBilled
            // 
            this.cmbZBilled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZBilled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbZBilled.FormattingEnabled = true;
            this.cmbZBilled.Location = new System.Drawing.Point(282, 15);
            this.cmbZBilled.Name = "cmbZBilled";
            this.cmbZBilled.Size = new System.Drawing.Size(157, 22);
            this.cmbZBilled.TabIndex = 0;
            this.cmbZBilled.SelectedIndexChanged += new System.EventHandler(this.cmbAll_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(44, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(236, 14);
            this.label7.TabIndex = 5;
            this.label7.Text = "(Paid) Other Reasons not equal to 0.00 : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(36, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(244, 14);
            this.label5.TabIndex = 1;
            this.label5.Text = "(Zero Paid) Other Reasons equal to Billed : ";
            // 
            // cmbZNotBilled
            // 
            this.cmbZNotBilled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZNotBilled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbZNotBilled.FormattingEnabled = true;
            this.cmbZNotBilled.Location = new System.Drawing.Point(282, 43);
            this.cmbZNotBilled.Name = "cmbZNotBilled";
            this.cmbZNotBilled.Size = new System.Drawing.Size(157, 22);
            this.cmbZNotBilled.TabIndex = 1;
            this.cmbZNotBilled.SelectedIndexChanged += new System.EventHandler(this.cmbAll_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(267, 14);
            this.label6.TabIndex = 3;
            this.label6.Text = "(Zero Paid) Other Reasons not equal to Billed : ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.pnlStandardCAS);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel2.Size = new System.Drawing.Size(609, 433);
            this.panel2.TabIndex = 22;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.C1StandardCAS);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 25);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(609, 408);
            this.panel5.TabIndex = 31;
            // 
            // C1StandardCAS
            // 
            this.C1StandardCAS.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1StandardCAS.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.C1StandardCAS.BackColor = System.Drawing.Color.White;
            this.C1StandardCAS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1StandardCAS.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1StandardCAS.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.C1StandardCAS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1StandardCAS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1StandardCAS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1StandardCAS.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.C1StandardCAS.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.C1StandardCAS.Location = new System.Drawing.Point(1, 1);
            this.C1StandardCAS.Name = "C1StandardCAS";
            this.C1StandardCAS.Padding = new System.Windows.Forms.Padding(3);
            this.C1StandardCAS.Rows.Count = 1;
            this.C1StandardCAS.Rows.DefaultSize = 19;
            this.C1StandardCAS.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.ScrollByRowColumn;
            this.C1StandardCAS.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1StandardCAS.Size = new System.Drawing.Size(607, 406);
            this.C1StandardCAS.StyleInfo = resources.GetString("C1StandardCAS.StyleInfo");
            this.C1StandardCAS.TabIndex = 32;
            this.C1StandardCAS.TabStop = false;
            this.C1StandardCAS.BeforeRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1StandardCAS_BeforeRowColChange);
            this.C1StandardCAS.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1StandardCAS_StartEdit);
            this.C1StandardCAS.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1StandardCAS_AfterEdit);
            this.C1StandardCAS.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.C1StandardCAS_KeyPressEdit);
            this.C1StandardCAS.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1StandardCAS_CellChanged);
            this.C1StandardCAS.Leave += new System.EventHandler(this.C1StandardCAS_Leave);
            this.C1StandardCAS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C1StandardCAS_MouseDown);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(1, 407);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(607, 1);
            this.label11.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(1, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(607, 1);
            this.label12.TabIndex = 28;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(608, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 408);
            this.label13.TabIndex = 27;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 408);
            this.label14.TabIndex = 26;
            // 
            // pnlStandardCAS
            // 
            this.pnlStandardCAS.Controls.Add(this.pnlStandardCASLabel);
            this.pnlStandardCAS.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStandardCAS.Location = new System.Drawing.Point(0, 3);
            this.pnlStandardCAS.Name = "pnlStandardCAS";
            this.pnlStandardCAS.Size = new System.Drawing.Size(609, 22);
            this.pnlStandardCAS.TabIndex = 25;
            this.pnlStandardCAS.Visible = false;
            // 
            // pnlStandardCASLabel
            // 
            this.pnlStandardCASLabel.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.pnlStandardCASLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlStandardCASLabel.Controls.Add(this.lblStandardCAS);
            this.pnlStandardCASLabel.Controls.Add(this.label25);
            this.pnlStandardCASLabel.Controls.Add(this.label27);
            this.pnlStandardCASLabel.Controls.Add(this.label30);
            this.pnlStandardCASLabel.Controls.Add(this.label50);
            this.pnlStandardCASLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStandardCASLabel.Location = new System.Drawing.Point(0, 0);
            this.pnlStandardCASLabel.Name = "pnlStandardCASLabel";
            this.pnlStandardCASLabel.Size = new System.Drawing.Size(609, 22);
            this.pnlStandardCASLabel.TabIndex = 21;
            // 
            // lblStandardCAS
            // 
            this.lblStandardCAS.BackColor = System.Drawing.Color.Transparent;
            this.lblStandardCAS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStandardCAS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStandardCAS.Location = new System.Drawing.Point(1, 1);
            this.lblStandardCAS.Name = "lblStandardCAS";
            this.lblStandardCAS.Size = new System.Drawing.Size(607, 20);
            this.lblStandardCAS.TabIndex = 26;
            this.lblStandardCAS.Text = "   Standard Reason Codes";
            this.lblStandardCAS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStandardCAS.Click += new System.EventHandler(this.lblStandardCAS_Click);
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label25.Location = new System.Drawing.Point(1, 21);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(607, 1);
            this.label25.TabIndex = 25;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.label27.Location = new System.Drawing.Point(1, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(607, 1);
            this.label27.TabIndex = 24;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Right;
            this.label30.Location = new System.Drawing.Point(608, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 22);
            this.label30.TabIndex = 23;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Left;
            this.label50.Location = new System.Drawing.Point(0, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 22);
            this.label50.TabIndex = 22;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel3.Size = new System.Drawing.Size(609, 433);
            this.panel3.TabIndex = 23;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label15);
            this.panel6.Controls.Add(this.C1OtherCAS);
            this.panel6.Controls.Add(this.label16);
            this.panel6.Controls.Add(this.label17);
            this.panel6.Controls.Add(this.label18);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 159);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel6.Size = new System.Drawing.Size(609, 274);
            this.panel6.TabIndex = 31;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(1, 273);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(607, 1);
            this.label15.TabIndex = 29;
            // 
            // C1OtherCAS
            // 
            this.C1OtherCAS.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1OtherCAS.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.C1OtherCAS.BackColor = System.Drawing.Color.White;
            this.C1OtherCAS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1OtherCAS.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1OtherCAS.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.C1OtherCAS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1OtherCAS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1OtherCAS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1OtherCAS.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.C1OtherCAS.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.C1OtherCAS.Location = new System.Drawing.Point(1, 4);
            this.C1OtherCAS.Name = "C1OtherCAS";
            this.C1OtherCAS.Padding = new System.Windows.Forms.Padding(3);
            this.C1OtherCAS.Rows.Count = 1;
            this.C1OtherCAS.Rows.DefaultSize = 19;
            this.C1OtherCAS.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.ScrollByRowColumn;
            this.C1OtherCAS.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1OtherCAS.Size = new System.Drawing.Size(607, 270);
            this.C1OtherCAS.StyleInfo = resources.GetString("C1OtherCAS.StyleInfo");
            this.C1OtherCAS.TabIndex = 31;
            this.C1OtherCAS.TabStop = false;
            this.C1OtherCAS.BeforeRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1OtherCAS_BeforeRowColChange);
            this.C1OtherCAS.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1OtherCAS_StartEdit);
            this.C1OtherCAS.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1OtherCAS_AfterEdit);
            this.C1OtherCAS.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.C1OtherCAS_KeyPressEdit);
            this.C1OtherCAS.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1OtherCAS_CellChanged);
            this.C1OtherCAS.Leave += new System.EventHandler(this.C1OtherCAS_Leave);
            this.C1OtherCAS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C1OtherCAS_MouseDown);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(1, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(607, 1);
            this.label16.TabIndex = 28;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Location = new System.Drawing.Point(608, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 271);
            this.label17.TabIndex = 27;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Location = new System.Drawing.Point(0, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 271);
            this.label18.TabIndex = 26;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 22);
            this.panel1.TabIndex = 34;
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label28);
            this.panel7.Controls.Add(this.label29);
            this.panel7.Controls.Add(this.label31);
            this.panel7.Controls.Add(this.label32);
            this.panel7.Controls.Add(this.label33);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(609, 22);
            this.panel7.TabIndex = 22;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(1, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(607, 20);
            this.label28.TabIndex = 0;
            this.label28.Text = "   Action Overrides";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label29.Location = new System.Drawing.Point(1, 21);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(607, 1);
            this.label29.TabIndex = 25;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Top;
            this.label31.Location = new System.Drawing.Point(1, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(607, 1);
            this.label31.TabIndex = 24;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Location = new System.Drawing.Point(608, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 22);
            this.label32.TabIndex = 23;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Left;
            this.label33.Location = new System.Drawing.Point(0, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1, 22);
            this.label33.TabIndex = 22;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label2);
            this.panel8.Controls.Add(this.cmbPaid);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Controls.Add(this.cmbZBilled);
            this.panel8.Controls.Add(this.label19);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.label24);
            this.panel8.Controls.Add(this.label5);
            this.panel8.Controls.Add(this.label26);
            this.panel8.Controls.Add(this.cmbZNotBilled);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 25);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel8.Size = new System.Drawing.Size(609, 112);
            this.panel8.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(456, 49);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(113, 19);
            this.label2.TabIndex = 26;
            this.label2.Text = " Default Action :";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(1, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(607, 1);
            this.label3.TabIndex = 25;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Location = new System.Drawing.Point(1, 3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(607, 1);
            this.label19.TabIndex = 24;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Location = new System.Drawing.Point(608, 3);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 106);
            this.label24.TabIndex = 23;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Location = new System.Drawing.Point(0, 3);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 106);
            this.label26.TabIndex = 22;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pnlOtherCASLabel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(609, 22);
            this.panel4.TabIndex = 32;
            // 
            // pnlOtherCASLabel
            // 
            this.pnlOtherCASLabel.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.pnlOtherCASLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlOtherCASLabel.Controls.Add(this.lblOtherCAS);
            this.pnlOtherCASLabel.Controls.Add(this.label20);
            this.pnlOtherCASLabel.Controls.Add(this.label21);
            this.pnlOtherCASLabel.Controls.Add(this.label22);
            this.pnlOtherCASLabel.Controls.Add(this.label23);
            this.pnlOtherCASLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOtherCASLabel.Location = new System.Drawing.Point(0, 0);
            this.pnlOtherCASLabel.Name = "pnlOtherCASLabel";
            this.pnlOtherCASLabel.Size = new System.Drawing.Size(609, 22);
            this.pnlOtherCASLabel.TabIndex = 22;
            // 
            // lblOtherCAS
            // 
            this.lblOtherCAS.BackColor = System.Drawing.Color.Transparent;
            this.lblOtherCAS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOtherCAS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherCAS.Location = new System.Drawing.Point(1, 1);
            this.lblOtherCAS.Name = "lblOtherCAS";
            this.lblOtherCAS.Size = new System.Drawing.Size(607, 20);
            this.lblOtherCAS.TabIndex = 0;
            this.lblOtherCAS.Text = "   Default Action";
            this.lblOtherCAS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOtherCAS.Click += new System.EventHandler(this.lblOtherCAS_Click);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Location = new System.Drawing.Point(1, 21);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(607, 1);
            this.label20.TabIndex = 25;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(1, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(607, 1);
            this.label21.TabIndex = 24;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Location = new System.Drawing.Point(608, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 22);
            this.label22.TabIndex = 23;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 22);
            this.label23.TabIndex = 22;
            // 
            // tbCAS
            // 
            this.tbCAS.Controls.Add(this.tb_StandardCAS);
            this.tbCAS.Controls.Add(this.tb_OtherCAS);
            this.tbCAS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCAS.Location = new System.Drawing.Point(3, 0);
            this.tbCAS.Name = "tbCAS";
            this.tbCAS.SelectedIndex = 0;
            this.tbCAS.Size = new System.Drawing.Size(617, 460);
            this.tbCAS.TabIndex = 0;
            // 
            // tb_StandardCAS
            // 
            this.tb_StandardCAS.BackColor = System.Drawing.Color.White;
            this.tb_StandardCAS.Controls.Add(this.panel2);
            this.tb_StandardCAS.Location = new System.Drawing.Point(4, 23);
            this.tb_StandardCAS.Name = "tb_StandardCAS";
            this.tb_StandardCAS.Size = new System.Drawing.Size(609, 433);
            this.tb_StandardCAS.TabIndex = 0;
            this.tb_StandardCAS.Tag = "Standard";
            this.tb_StandardCAS.Text = "Standard Reason Codes";
            // 
            // tb_OtherCAS
            // 
            this.tb_OtherCAS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tb_OtherCAS.Controls.Add(this.panel3);
            this.tb_OtherCAS.Location = new System.Drawing.Point(4, 23);
            this.tb_OtherCAS.Name = "tb_OtherCAS";
            this.tb_OtherCAS.Size = new System.Drawing.Size(609, 433);
            this.tb_OtherCAS.TabIndex = 1;
            this.tb_OtherCAS.Tag = "Other";
            this.tb_OtherCAS.Text = "Other Reason Codes";
            this.tb_OtherCAS.UseVisualStyleBackColor = true;
            // 
            // pnlTab
            // 
            this.pnlTab.Controls.Add(this.tbCAS);
            this.pnlTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab.Location = new System.Drawing.Point(0, 157);
            this.pnlTab.Name = "pnlTab";
            this.pnlTab.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlTab.Size = new System.Drawing.Size(623, 460);
            this.pnlTab.TabIndex = 25;
            // 
            // mnuSetup
            // 
            this.mnuSetup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemAddLine,
            this.mnuItemRemoveLine,
            this.mnuItemSaveClose});
            this.mnuSetup.Location = new System.Drawing.Point(0, 0);
            this.mnuSetup.Name = "mnuSetup";
            this.mnuSetup.Size = new System.Drawing.Size(623, 24);
            this.mnuSetup.TabIndex = 26;
            this.mnuSetup.Text = "menuStrip1";
            this.mnuSetup.Visible = false;
            // 
            // mnuItemAddLine
            // 
            this.mnuItemAddLine.Name = "mnuItemAddLine";
            this.mnuItemAddLine.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuItemAddLine.Size = new System.Drawing.Size(66, 20);
            this.mnuItemAddLine.Text = "Add Line";
            this.mnuItemAddLine.Click += new System.EventHandler(this.mnuItemAddLine_Click);
            // 
            // mnuItemRemoveLine
            // 
            this.mnuItemRemoveLine.Name = "mnuItemRemoveLine";
            this.mnuItemRemoveLine.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuItemRemoveLine.Size = new System.Drawing.Size(87, 20);
            this.mnuItemRemoveLine.Text = "Remove Line";
            this.mnuItemRemoveLine.Click += new System.EventHandler(this.mnuItemRemoveLine_Click);
            // 
            // mnuItemSaveClose
            // 
            this.mnuItemSaveClose.Name = "mnuItemSaveClose";
            this.mnuItemSaveClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuItemSaveClose.Size = new System.Drawing.Size(75, 20);
            this.mnuItemSaveClose.Text = "Save Close";
            this.mnuItemSaveClose.Click += new System.EventHandler(this.mnuItemSaveClose_Click);
            // 
            // pnlShortcuts
            // 
            this.pnlShortcuts.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlShortcuts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlShortcuts.Controls.Add(this.label34);
            this.pnlShortcuts.Controls.Add(this.label37);
            this.pnlShortcuts.Controls.Add(this.label44);
            this.pnlShortcuts.Controls.Add(this.label45);
            this.pnlShortcuts.Controls.Add(this.label46);
            this.pnlShortcuts.Controls.Add(this.label47);
            this.pnlShortcuts.Controls.Add(this.label48);
            this.pnlShortcuts.Controls.Add(this.label49);
            this.pnlShortcuts.Controls.Add(this.label51);
            this.pnlShortcuts.Controls.Add(this.label52);
            this.pnlShortcuts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlShortcuts.Location = new System.Drawing.Point(0, 617);
            this.pnlShortcuts.Name = "pnlShortcuts";
            this.pnlShortcuts.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlShortcuts.Size = new System.Drawing.Size(623, 25);
            this.pnlShortcuts.TabIndex = 27;
            this.pnlShortcuts.TabStop = true;
            this.pnlShortcuts.Tag = "pnlTransactionOther2";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Dock = System.Windows.Forms.DockStyle.Left;
            this.label34.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(250, 2);
            this.label34.Name = "label34";
            this.label34.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label34.Size = new System.Drawing.Size(88, 16);
            this.label34.TabIndex = 13;
            this.label34.Text = "- Save && Close";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Dock = System.Windows.Forms.DockStyle.Left;
            this.label37.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.Maroon;
            this.label37.Location = new System.Drawing.Point(201, 2);
            this.label37.Name = "label37";
            this.label37.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label37.Size = new System.Drawing.Size(49, 16);
            this.label37.TabIndex = 12;
            this.label37.Text = "Ctrl + S";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.label44.Dock = System.Windows.Forms.DockStyle.Left;
            this.label44.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(113, 2);
            this.label44.Name = "label44";
            this.label44.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label44.Size = new System.Drawing.Size(88, 16);
            this.label44.TabIndex = 2;
            this.label44.Text = "- Remove Line";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.BackColor = System.Drawing.Color.Transparent;
            this.label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.label45.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.Maroon;
            this.label45.Location = new System.Drawing.Point(93, 2);
            this.label45.Name = "label45";
            this.label45.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label45.Size = new System.Drawing.Size(20, 16);
            this.label45.TabIndex = 3;
            this.label45.Text = "F3";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.BackColor = System.Drawing.Color.Transparent;
            this.label46.Dock = System.Windows.Forms.DockStyle.Left;
            this.label46.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(30, 2);
            this.label46.Name = "label46";
            this.label46.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label46.Size = new System.Drawing.Size(63, 16);
            this.label46.TabIndex = 1;
            this.label46.Text = "- Add Line";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.BackColor = System.Drawing.Color.Transparent;
            this.label47.Dock = System.Windows.Forms.DockStyle.Left;
            this.label47.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.Maroon;
            this.label47.Location = new System.Drawing.Point(4, 2);
            this.label47.Name = "label47";
            this.label47.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label47.Size = new System.Drawing.Size(26, 16);
            this.label47.TabIndex = 0;
            this.label47.Text = "  F2";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label48.Location = new System.Drawing.Point(4, 21);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(615, 1);
            this.label48.TabIndex = 16;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Top;
            this.label49.Location = new System.Drawing.Point(4, 1);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(615, 1);
            this.label49.TabIndex = 0;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Right;
            this.label51.Location = new System.Drawing.Point(619, 1);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(1, 21);
            this.label51.TabIndex = 0;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(3, 1);
            this.label52.Name = "label52";
            this.label52.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label52.Size = new System.Drawing.Size(1, 21);
            this.label52.TabIndex = 0;
            // 
            // frmERAPayerSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(623, 642);
            this.Controls.Add(this.pnlTab);
            this.Controls.Add(this.pnlShortcuts);
            this.Controls.Add(this.pnlMST);
            this.Controls.Add(this.pnlToolStrip);
            this.Controls.Add(this.mnuSetup);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuSetup;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmERAPayerSetup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERA Payer ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmERAPayerSetup_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmERAPayerSetup_FormClosed);
            this.Load += new System.EventHandler(this.frmERAPayerSetup_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Main.ResumeLayout(false);
            this.tls_Main.PerformLayout();
            this.pnlMST.ResumeLayout(false);
            this.pnlMST.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1StandardCAS)).EndInit();
            this.pnlStandardCAS.ResumeLayout(false);
            this.pnlStandardCASLabel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1OtherCAS)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.pnlOtherCASLabel.ResumeLayout(false);
            this.tbCAS.ResumeLayout(false);
            this.tb_StandardCAS.ResumeLayout(false);
            this.tb_OtherCAS.ResumeLayout(false);
            this.pnlTab.ResumeLayout(false);
            this.mnuSetup.ResumeLayout(false);
            this.mnuSetup.PerformLayout();
            this.pnlShortcuts.ResumeLayout(false);
            this.pnlShortcuts.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.ToolStrip tls_Main;
        private System.Windows.Forms.ToolStripButton tsb_Add;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnlMST;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripButton tsb_Remove;
        private System.Windows.Forms.ToolStripButton tsb_Import;
        private System.Windows.Forms.ToolStripButton tsb_Reset;
        private System.Windows.Forms.ToolStripButton tsb_SaveClose;
        private System.Windows.Forms.TextBox txtPayerID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkUseClaimStatus;
        private System.Windows.Forms.CheckBox chkActivate;
        private System.Windows.Forms.CheckBox chkSecondaryAdjst;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblOtherCAS;
        private C1.Win.C1FlexGrid.C1FlexGrid C1OtherCAS;
        private System.Windows.Forms.ComboBox cmbPaid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbZNotBilled;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbZBilled;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlStandardCAS;
        private System.Windows.Forms.Panel pnlStandardCASLabel;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblStandardCAS;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnlOtherCASLabel;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private C1.Win.C1FlexGrid.C1FlexGrid C1StandardCAS;
        private System.Windows.Forms.TabControl tbCAS;
        private System.Windows.Forms.TabPage tb_StandardCAS;
        private System.Windows.Forms.TabPage tb_OtherCAS;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel pnlTab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.MenuStrip mnuSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuItemAddLine;
        private System.Windows.Forms.ToolStripMenuItem mnuItemRemoveLine;
        private System.Windows.Forms.ToolStripMenuItem mnuItemSaveClose;
        private System.Windows.Forms.Panel pnlShortcuts;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;

    }
}