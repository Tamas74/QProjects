namespace gloBilling
{
    partial class frmSetupStdFeeSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupStdFeeSchedule));
            this.pnlFeeSchedule = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.c1StdFeeSchedule = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_Shortkeys = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_KeyClose = new System.Windows.Forms.Label();
            this.lbl_shrtctKeyClose = new System.Windows.Forms.Label();
            this.lbl_KeySave = new System.Windows.Forms.Label();
            this.lbl_shrtctKeySave = new System.Windows.Forms.Label();
            this.lbl_Keyremoveline = new System.Windows.Forms.Label();
            this.lbl_lshrtctKeyremoveline = new System.Windows.Forms.Label();
            this.lbl_KeyAddline = new System.Windows.Forms.Label();
            this.lbl_shrtctKeyAddline = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlSpeciality = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtToCPT = new System.Windows.Forms.TextBox();
            this.lblToCPT = new System.Windows.Forms.Label();
            this.txtFromCPT = new System.Windows.Forms.TextBox();
            this.lblFromCPT = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.RdbIncreaseChrgeby = new System.Windows.Forms.RadioButton();
            this.txtChargesPercentage = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbNone = new System.Windows.Forms.RadioButton();
            this.rdbroundDown = new System.Windows.Forms.RadioButton();
            this.rdbroundUp = new System.Windows.Forms.RadioButton();
            this.RdbSetCharges = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_StdFeeScheduleType = new System.Windows.Forms.TextBox();
            this.lbl_stdFeeSchedule = new System.Windows.Forms.Label();
            this.mskEndDate = new System.Windows.Forms.MaskedTextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.mskStartDate = new System.Windows.Forms.MaskedTextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.lblMandtryEndDte = new System.Windows.Forms.Label();
            this.lblmandatryStrtDate = new System.Windows.Forms.Label();
            this.lblMandatryFeeSchedule = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlListControl = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFeeSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFeeSchedule_AddLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFeeSchedule_RemoveLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFeeSchedule_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFeeSchedule_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlEOBFeeSchedule = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnGenerateFeeSchedule = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClearInsuranceCompany = new System.Windows.Forms.Button();
            this.mskEobStartDate = new System.Windows.Forms.MaskedTextBox();
            this.btnGetInsuranceCompany = new System.Windows.Forms.Button();
            this.mskEobToDate = new System.Windows.Forms.MaskedTextBox();
            this.cmbInsuranceCompany = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.pnlTopToolStrip = new System.Windows.Forms.Panel();
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnAddLine = new System.Windows.Forms.ToolStripButton();
            this.ts_btnRemoveLine = new System.Windows.Forms.ToolStripButton();
            this.tsbShow = new System.Windows.Forms.ToolStripButton();
            this.ts_btnShowAll = new System.Windows.Forms.ToolStripButton();
            this.tsb_Apply = new System.Windows.Forms.ToolStripButton();
            this.ts_GenerateEOBFeeSchedule = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave_FeeSchedule = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose_FeeSchedule = new System.Windows.Forms.ToolStripButton();
            this.pnlFeeSchedule.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1StdFeeSchedule)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnl_Shortkeys.SuspendLayout();
            this.pnlSpeciality.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlEOBFeeSchedule.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlTopToolStrip.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFeeSchedule
            // 
            this.pnlFeeSchedule.Controls.Add(this.panel3);
            this.pnlFeeSchedule.Controls.Add(this.panel1);
            this.pnlFeeSchedule.Controls.Add(this.pnlSpeciality);
            this.pnlFeeSchedule.Controls.Add(this.pnlListControl);
            this.pnlFeeSchedule.Controls.Add(this.menuStrip1);
            this.pnlFeeSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFeeSchedule.Location = new System.Drawing.Point(0, 118);
            this.pnlFeeSchedule.Name = "pnlFeeSchedule";
            this.pnlFeeSchedule.Size = new System.Drawing.Size(876, 540);
            this.pnlFeeSchedule.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlInternalControl);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.c1StdFeeSchedule);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 137);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel3.Size = new System.Drawing.Size(876, 377);
            this.panel3.TabIndex = 1;
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(361, 164);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(124, 138);
            this.pnlInternalControl.TabIndex = 31;
            this.pnlInternalControl.Visible = false;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(872, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 372);
            this.label20.TabIndex = 20;
            this.label20.Text = "label20";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(3, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 372);
            this.label19.TabIndex = 0;
            this.label19.Text = "label19";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(3, 373);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(870, 1);
            this.label18.TabIndex = 18;
            this.label18.Text = "label18";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(870, 1);
            this.label17.TabIndex = 0;
            this.label17.Text = "label17";
            // 
            // c1StdFeeSchedule
            // 
            this.c1StdFeeSchedule.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1StdFeeSchedule.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1StdFeeSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1StdFeeSchedule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1StdFeeSchedule.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1StdFeeSchedule.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1StdFeeSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1StdFeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1StdFeeSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1StdFeeSchedule.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1StdFeeSchedule.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1StdFeeSchedule.Location = new System.Drawing.Point(3, 0);
            this.c1StdFeeSchedule.Name = "c1StdFeeSchedule";
            this.c1StdFeeSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.c1StdFeeSchedule.Rows.Count = 1;
            this.c1StdFeeSchedule.Rows.DefaultSize = 19;
            this.c1StdFeeSchedule.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1StdFeeSchedule.ShowCellLabels = true;
            this.c1StdFeeSchedule.Size = new System.Drawing.Size(870, 374);
            this.c1StdFeeSchedule.StyleInfo = resources.GetString("c1StdFeeSchedule.StyleInfo");
            this.c1StdFeeSchedule.TabIndex = 0;
            this.c1StdFeeSchedule.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1StdFeeSchedule_BeforeSelChange);
            this.c1StdFeeSchedule.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1StdFeeSchedule_StartEdit);
            this.c1StdFeeSchedule.ChangeEdit += new System.EventHandler(this.c1StdFeeSchedule_ChangeEdit);
            this.c1StdFeeSchedule.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.c1StdFeeSchedule_KeyPressEditEvent);
            this.c1StdFeeSchedule.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1StdFeeSchedule_KeyUp);
            this.c1StdFeeSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1StdFeeSchedule_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnl_Shortkeys);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 514);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(876, 26);
            this.panel1.TabIndex = 333;
            // 
            // pnl_Shortkeys
            // 
            this.pnl_Shortkeys.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnl_Shortkeys.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Shortkeys.Controls.Add(this.label4);
            this.pnl_Shortkeys.Controls.Add(this.lbl_KeyClose);
            this.pnl_Shortkeys.Controls.Add(this.lbl_shrtctKeyClose);
            this.pnl_Shortkeys.Controls.Add(this.lbl_KeySave);
            this.pnl_Shortkeys.Controls.Add(this.lbl_shrtctKeySave);
            this.pnl_Shortkeys.Controls.Add(this.lbl_Keyremoveline);
            this.pnl_Shortkeys.Controls.Add(this.lbl_lshrtctKeyremoveline);
            this.pnl_Shortkeys.Controls.Add(this.lbl_KeyAddline);
            this.pnl_Shortkeys.Controls.Add(this.lbl_shrtctKeyAddline);
            this.pnl_Shortkeys.Controls.Add(this.label2);
            this.pnl_Shortkeys.Controls.Add(this.label1);
            this.pnl_Shortkeys.Controls.Add(this.label3);
            this.pnl_Shortkeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Shortkeys.Location = new System.Drawing.Point(3, 0);
            this.pnl_Shortkeys.Name = "pnl_Shortkeys";
            this.pnl_Shortkeys.Size = new System.Drawing.Size(870, 23);
            this.pnl_Shortkeys.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(869, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 21);
            this.label4.TabIndex = 68;
            this.label4.Text = "label4";
            // 
            // lbl_KeyClose
            // 
            this.lbl_KeyClose.BackColor = System.Drawing.Color.Transparent;
            this.lbl_KeyClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_KeyClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeyClose.Location = new System.Drawing.Point(396, 1);
            this.lbl_KeyClose.Name = "lbl_KeyClose";
            this.lbl_KeyClose.Size = new System.Drawing.Size(52, 21);
            this.lbl_KeyClose.TabIndex = 63;
            this.lbl_KeyClose.Text = "- Close";
            this.lbl_KeyClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_shrtctKeyClose
            // 
            this.lbl_shrtctKeyClose.BackColor = System.Drawing.Color.Transparent;
            this.lbl_shrtctKeyClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_shrtctKeyClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shrtctKeyClose.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_shrtctKeyClose.Location = new System.Drawing.Point(335, 1);
            this.lbl_shrtctKeyClose.Name = "lbl_shrtctKeyClose";
            this.lbl_shrtctKeyClose.Size = new System.Drawing.Size(61, 21);
            this.lbl_shrtctKeyClose.TabIndex = 64;
            this.lbl_shrtctKeyClose.Text = "Alt + F4";
            this.lbl_shrtctKeyClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_KeySave
            // 
            this.lbl_KeySave.BackColor = System.Drawing.Color.Transparent;
            this.lbl_KeySave.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_KeySave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeySave.Location = new System.Drawing.Point(286, 1);
            this.lbl_KeySave.Name = "lbl_KeySave";
            this.lbl_KeySave.Size = new System.Drawing.Size(49, 21);
            this.lbl_KeySave.TabIndex = 56;
            this.lbl_KeySave.Text = "- Save";
            this.lbl_KeySave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_shrtctKeySave
            // 
            this.lbl_shrtctKeySave.BackColor = System.Drawing.Color.Transparent;
            this.lbl_shrtctKeySave.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_shrtctKeySave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shrtctKeySave.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_shrtctKeySave.Location = new System.Drawing.Point(227, 1);
            this.lbl_shrtctKeySave.Name = "lbl_shrtctKeySave";
            this.lbl_shrtctKeySave.Size = new System.Drawing.Size(59, 21);
            this.lbl_shrtctKeySave.TabIndex = 55;
            this.lbl_shrtctKeySave.Text = "Ctrl + S";
            this.lbl_shrtctKeySave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Keyremoveline
            // 
            this.lbl_Keyremoveline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Keyremoveline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Keyremoveline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Keyremoveline.Location = new System.Drawing.Point(129, 1);
            this.lbl_Keyremoveline.Name = "lbl_Keyremoveline";
            this.lbl_Keyremoveline.Size = new System.Drawing.Size(98, 21);
            this.lbl_Keyremoveline.TabIndex = 46;
            this.lbl_Keyremoveline.Text = "- Remove Line";
            this.lbl_Keyremoveline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_lshrtctKeyremoveline
            // 
            this.lbl_lshrtctKeyremoveline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_lshrtctKeyremoveline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_lshrtctKeyremoveline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_lshrtctKeyremoveline.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_lshrtctKeyremoveline.Location = new System.Drawing.Point(104, 1);
            this.lbl_lshrtctKeyremoveline.Name = "lbl_lshrtctKeyremoveline";
            this.lbl_lshrtctKeyremoveline.Size = new System.Drawing.Size(25, 21);
            this.lbl_lshrtctKeyremoveline.TabIndex = 45;
            this.lbl_lshrtctKeyremoveline.Text = "F3";
            this.lbl_lshrtctKeyremoveline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_KeyAddline
            // 
            this.lbl_KeyAddline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_KeyAddline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_KeyAddline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeyAddline.Location = new System.Drawing.Point(30, 1);
            this.lbl_KeyAddline.Name = "lbl_KeyAddline";
            this.lbl_KeyAddline.Size = new System.Drawing.Size(74, 21);
            this.lbl_KeyAddline.TabIndex = 44;
            this.lbl_KeyAddline.Text = "- Add Line";
            this.lbl_KeyAddline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_shrtctKeyAddline
            // 
            this.lbl_shrtctKeyAddline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_shrtctKeyAddline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_shrtctKeyAddline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shrtctKeyAddline.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_shrtctKeyAddline.Location = new System.Drawing.Point(1, 1);
            this.lbl_shrtctKeyAddline.Name = "lbl_shrtctKeyAddline";
            this.lbl_shrtctKeyAddline.Size = new System.Drawing.Size(29, 21);
            this.lbl_shrtctKeyAddline.TabIndex = 44;
            this.lbl_shrtctKeyAddline.Text = "F2 ";
            this.lbl_shrtctKeyAddline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(869, 1);
            this.label2.TabIndex = 66;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(1, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(869, 1);
            this.label1.TabIndex = 65;
            this.label1.Text = "label1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 23);
            this.label3.TabIndex = 67;
            this.label3.Text = "label3";
            // 
            // pnlSpeciality
            // 
            this.pnlSpeciality.Controls.Add(this.panel5);
            this.pnlSpeciality.Controls.Add(this.panel4);
            this.pnlSpeciality.Controls.Add(this.panel2);
            this.pnlSpeciality.Controls.Add(this.panel8);
            this.pnlSpeciality.Controls.Add(this.label16);
            this.pnlSpeciality.Controls.Add(this.label13);
            this.pnlSpeciality.Controls.Add(this.label12);
            this.pnlSpeciality.Controls.Add(this.label11);
            this.pnlSpeciality.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpeciality.Location = new System.Drawing.Point(0, 0);
            this.pnlSpeciality.Name = "pnlSpeciality";
            this.pnlSpeciality.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSpeciality.Size = new System.Drawing.Size(876, 137);
            this.pnlSpeciality.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtToCPT);
            this.panel5.Controls.Add(this.lblToCPT);
            this.panel5.Controls.Add(this.txtFromCPT);
            this.panel5.Controls.Add(this.lblFromCPT);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 100);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(868, 33);
            this.panel5.TabIndex = 3;
            // 
            // txtToCPT
            // 
            this.txtToCPT.ForeColor = System.Drawing.Color.Black;
            this.txtToCPT.Location = new System.Drawing.Point(394, 7);
            this.txtToCPT.MaxLength = 10;
            this.txtToCPT.Name = "txtToCPT";
            this.txtToCPT.Size = new System.Drawing.Size(98, 22);
            this.txtToCPT.TabIndex = 2;
            // 
            // lblToCPT
            // 
            this.lblToCPT.AutoSize = true;
            this.lblToCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToCPT.Location = new System.Drawing.Point(333, 10);
            this.lblToCPT.Name = "lblToCPT";
            this.lblToCPT.Size = new System.Drawing.Size(56, 14);
            this.lblToCPT.TabIndex = 20;
            this.lblToCPT.Text = "To CPT :";
            // 
            // txtFromCPT
            // 
            this.txtFromCPT.ForeColor = System.Drawing.Color.Black;
            this.txtFromCPT.Location = new System.Drawing.Point(140, 6);
            this.txtFromCPT.MaxLength = 10;
            this.txtFromCPT.Name = "txtFromCPT";
            this.txtFromCPT.Size = new System.Drawing.Size(121, 22);
            this.txtFromCPT.TabIndex = 0;
            // 
            // lblFromCPT
            // 
            this.lblFromCPT.AutoSize = true;
            this.lblFromCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromCPT.Location = new System.Drawing.Point(70, 10);
            this.lblFromCPT.Name = "lblFromCPT";
            this.lblFromCPT.Size = new System.Drawing.Size(68, 14);
            this.lblFromCPT.TabIndex = 20;
            this.lblFromCPT.Text = "From CPT :";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.RdbIncreaseChrgeby);
            this.panel4.Controls.Add(this.txtChargesPercentage);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.RdbSetCharges);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(4, 65);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(868, 35);
            this.panel4.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(0, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(868, 1);
            this.label6.TabIndex = 19;
            this.label6.Text = "label6";
            // 
            // RdbIncreaseChrgeby
            // 
            this.RdbIncreaseChrgeby.AutoSize = true;
            this.RdbIncreaseChrgeby.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RdbIncreaseChrgeby.Location = new System.Drawing.Point(246, 8);
            this.RdbIncreaseChrgeby.Name = "RdbIncreaseChrgeby";
            this.RdbIncreaseChrgeby.Size = new System.Drawing.Size(150, 18);
            this.RdbIncreaseChrgeby.TabIndex = 2;
            this.RdbIncreaseChrgeby.Text = "Increase Charge by % ";
            this.RdbIncreaseChrgeby.UseVisualStyleBackColor = true;
            this.RdbIncreaseChrgeby.CheckedChanged += new System.EventHandler(this.RdbIncreaseChrgeby_CheckedChanged);
            // 
            // txtChargesPercentage
            // 
            this.txtChargesPercentage.ForeColor = System.Drawing.Color.Black;
            this.txtChargesPercentage.Location = new System.Drawing.Point(412, 7);
            this.txtChargesPercentage.MaxLength = 10;
            this.txtChargesPercentage.Name = "txtChargesPercentage";
            this.txtChargesPercentage.Size = new System.Drawing.Size(80, 22);
            this.txtChargesPercentage.TabIndex = 3;
            this.txtChargesPercentage.TextChanged += new System.EventHandler(this.txtChargesPercentage_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbNone);
            this.groupBox2.Controls.Add(this.rdbroundDown);
            this.groupBox2.Controls.Add(this.rdbroundUp);
            this.groupBox2.Location = new System.Drawing.Point(527, -1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 30);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // rdbNone
            // 
            this.rdbNone.AutoSize = true;
            this.rdbNone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbNone.Location = new System.Drawing.Point(212, 10);
            this.rdbNone.Name = "rdbNone";
            this.rdbNone.Size = new System.Drawing.Size(54, 18);
            this.rdbNone.TabIndex = 2;
            this.rdbNone.Text = "None";
            this.rdbNone.UseVisualStyleBackColor = true;
            this.rdbNone.CheckedChanged += new System.EventHandler(this.rdbNone_CheckedChanged_1);
            // 
            // rdbroundDown
            // 
            this.rdbroundDown.AutoSize = true;
            this.rdbroundDown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbroundDown.Location = new System.Drawing.Point(102, 10);
            this.rdbroundDown.Name = "rdbroundDown";
            this.rdbroundDown.Size = new System.Drawing.Size(96, 18);
            this.rdbroundDown.TabIndex = 1;
            this.rdbroundDown.Text = "Round Down";
            this.rdbroundDown.UseVisualStyleBackColor = true;
            this.rdbroundDown.CheckedChanged += new System.EventHandler(this.rdbroundDown_CheckedChanged);
            // 
            // rdbroundUp
            // 
            this.rdbroundUp.AutoSize = true;
            this.rdbroundUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbroundUp.Location = new System.Drawing.Point(2, 10);
            this.rdbroundUp.Name = "rdbroundUp";
            this.rdbroundUp.Size = new System.Drawing.Size(79, 18);
            this.rdbroundUp.TabIndex = 0;
            this.rdbroundUp.Text = "Round Up";
            this.rdbroundUp.UseVisualStyleBackColor = true;
            this.rdbroundUp.CheckedChanged += new System.EventHandler(this.rdbroundUp_CheckedChanged);
            // 
            // RdbSetCharges
            // 
            this.RdbSetCharges.AutoSize = true;
            this.RdbSetCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RdbSetCharges.Location = new System.Drawing.Point(38, 8);
            this.RdbSetCharges.Name = "RdbSetCharges";
            this.RdbSetCharges.Size = new System.Drawing.Size(184, 18);
            this.RdbSetCharges.TabIndex = 1;
            this.RdbSetCharges.Text = "Set Charges as % of Allowed";
            this.RdbSetCharges.UseVisualStyleBackColor = true;
            this.RdbSetCharges.CheckedChanged += new System.EventHandler(this.RdbSetCharges_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txt_StdFeeScheduleType);
            this.panel2.Controls.Add(this.lbl_stdFeeSchedule);
            this.panel2.Controls.Add(this.mskEndDate);
            this.panel2.Controls.Add(this.label31);
            this.panel2.Controls.Add(this.mskStartDate);
            this.panel2.Controls.Add(this.label32);
            this.panel2.Controls.Add(this.lblMandtryEndDte);
            this.panel2.Controls.Add(this.lblmandatryStrtDate);
            this.panel2.Controls.Add(this.lblMandatryFeeSchedule);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(868, 35);
            this.panel2.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(0, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(868, 1);
            this.label5.TabIndex = 18;
            this.label5.Text = "label5";
            // 
            // txt_StdFeeScheduleType
            // 
            this.txt_StdFeeScheduleType.ForeColor = System.Drawing.Color.Black;
            this.txt_StdFeeScheduleType.Location = new System.Drawing.Point(138, 6);
            this.txt_StdFeeScheduleType.MaxLength = 250;
            this.txt_StdFeeScheduleType.Name = "txt_StdFeeScheduleType";
            this.txt_StdFeeScheduleType.Size = new System.Drawing.Size(121, 22);
            this.txt_StdFeeScheduleType.TabIndex = 1;
            // 
            // lbl_stdFeeSchedule
            // 
            this.lbl_stdFeeSchedule.AutoSize = true;
            this.lbl_stdFeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_stdFeeSchedule.Location = new System.Drawing.Point(12, 10);
            this.lbl_stdFeeSchedule.Name = "lbl_stdFeeSchedule";
            this.lbl_stdFeeSchedule.Size = new System.Drawing.Size(124, 14);
            this.lbl_stdFeeSchedule.TabIndex = 22;
            this.lbl_stdFeeSchedule.Text = "Fee Schedule Name :";
            // 
            // mskEndDate
            // 
            this.mskEndDate.Location = new System.Drawing.Point(605, 5);
            this.mskEndDate.Mask = "00/00/0000";
            this.mskEndDate.Name = "mskEndDate";
            this.mskEndDate.Size = new System.Drawing.Size(98, 22);
            this.mskEndDate.TabIndex = 3;
            this.mskEndDate.ValidatingType = typeof(System.DateTime);
            this.mskEndDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskEndDate_MouseClick);
            this.mskEndDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskEndDate_Validating);
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoEllipsis = true;
            this.label31.AutoSize = true;
            this.label31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(282, 10);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(128, 14);
            this.label31.TabIndex = 26;
            this.label31.Text = "Effective Start  Date :\r\n";
            // 
            // mskStartDate
            // 
            this.mskStartDate.Location = new System.Drawing.Point(412, 6);
            this.mskStartDate.Mask = "00/00/0000";
            this.mskStartDate.Name = "mskStartDate";
            this.mskStartDate.Size = new System.Drawing.Size(80, 22);
            this.mskStartDate.TabIndex = 2;
            this.mskStartDate.ValidatingType = typeof(System.DateTime);
            this.mskStartDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskStartDate_MouseClick);
            this.mskStartDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskStartDate_Validating);
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.AutoEllipsis = true;
            this.label32.AutoSize = true;
            this.label32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(537, 9);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(66, 14);
            this.label32.TabIndex = 25;
            this.label32.Text = "End Date :";
            // 
            // lblMandtryEndDte
            // 
            this.lblMandtryEndDte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMandtryEndDte.AutoEllipsis = true;
            this.lblMandtryEndDte.AutoSize = true;
            this.lblMandtryEndDte.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMandtryEndDte.ForeColor = System.Drawing.Color.Red;
            this.lblMandtryEndDte.Location = new System.Drawing.Point(526, 9);
            this.lblMandtryEndDte.Name = "lblMandtryEndDte";
            this.lblMandtryEndDte.Size = new System.Drawing.Size(14, 14);
            this.lblMandtryEndDte.TabIndex = 43;
            this.lblMandtryEndDte.Text = "*";
            // 
            // lblmandatryStrtDate
            // 
            this.lblmandatryStrtDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblmandatryStrtDate.AutoEllipsis = true;
            this.lblmandatryStrtDate.AutoSize = true;
            this.lblmandatryStrtDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmandatryStrtDate.ForeColor = System.Drawing.Color.Red;
            this.lblmandatryStrtDate.Location = new System.Drawing.Point(272, 10);
            this.lblmandatryStrtDate.Name = "lblmandatryStrtDate";
            this.lblmandatryStrtDate.Size = new System.Drawing.Size(14, 14);
            this.lblmandatryStrtDate.TabIndex = 41;
            this.lblmandatryStrtDate.Text = "*";
            // 
            // lblMandatryFeeSchedule
            // 
            this.lblMandatryFeeSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMandatryFeeSchedule.AutoEllipsis = true;
            this.lblMandatryFeeSchedule.AutoSize = true;
            this.lblMandatryFeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMandatryFeeSchedule.ForeColor = System.Drawing.Color.Red;
            this.lblMandatryFeeSchedule.Location = new System.Drawing.Point(2, 10);
            this.lblMandatryFeeSchedule.Name = "lblMandatryFeeSchedule";
            this.lblMandatryFeeSchedule.Size = new System.Drawing.Size(14, 14);
            this.lblMandatryFeeSchedule.TabIndex = 42;
            this.lblMandatryFeeSchedule.Text = "*";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label25);
            this.panel8.Controls.Add(this.label26);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(4, 4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(868, 26);
            this.panel8.TabIndex = 144;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(0, 25);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(868, 1);
            this.label25.TabIndex = 141;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(868, 26);
            this.label26.TabIndex = 6;
            this.label26.Text = "Fee Schedule Details";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Location = new System.Drawing.Point(872, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 129);
            this.label16.TabIndex = 18;
            this.label16.Text = "label16";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Location = new System.Drawing.Point(4, 133);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(869, 1);
            this.label13.TabIndex = 17;
            this.label13.Text = "label13";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(4, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(869, 1);
            this.label12.TabIndex = 16;
            this.label12.Text = "label12";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 131);
            this.label11.TabIndex = 15;
            this.label11.Text = "label11";
            // 
            // pnlListControl
            // 
            this.pnlListControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlListControl.Location = new System.Drawing.Point(31, 174);
            this.pnlListControl.Name = "pnlListControl";
            this.pnlListControl.Size = new System.Drawing.Size(455, 233);
            this.pnlListControl.TabIndex = 109;
            this.pnlListControl.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFeeSchedule});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(30, 24);
            this.menuStrip1.TabIndex = 217;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // mnuFeeSchedule
            // 
            this.mnuFeeSchedule.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFeeSchedule_AddLine,
            this.mnuFeeSchedule_RemoveLine,
            this.mnuFeeSchedule_Save,
            this.mnuFeeSchedule_Close});
            this.mnuFeeSchedule.Name = "mnuFeeSchedule";
            this.mnuFeeSchedule.Size = new System.Drawing.Size(22, 20);
            this.mnuFeeSchedule.Text = " ";
            // 
            // mnuFeeSchedule_AddLine
            // 
            this.mnuFeeSchedule_AddLine.Name = "mnuFeeSchedule_AddLine";
            this.mnuFeeSchedule_AddLine.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuFeeSchedule_AddLine.Size = new System.Drawing.Size(161, 22);
            this.mnuFeeSchedule_AddLine.Text = "Add Line";
            this.mnuFeeSchedule_AddLine.Click += new System.EventHandler(this.mnuFeeSchedule_AddLine_Click);
            // 
            // mnuFeeSchedule_RemoveLine
            // 
            this.mnuFeeSchedule_RemoveLine.Name = "mnuFeeSchedule_RemoveLine";
            this.mnuFeeSchedule_RemoveLine.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuFeeSchedule_RemoveLine.Size = new System.Drawing.Size(161, 22);
            this.mnuFeeSchedule_RemoveLine.Text = "Remove Line";
            this.mnuFeeSchedule_RemoveLine.Click += new System.EventHandler(this.mnuFeeSchedule_RemoveLine_Click);
            // 
            // mnuFeeSchedule_Save
            // 
            this.mnuFeeSchedule_Save.Name = "mnuFeeSchedule_Save";
            this.mnuFeeSchedule_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFeeSchedule_Save.Size = new System.Drawing.Size(161, 22);
            this.mnuFeeSchedule_Save.Text = "Save";
            this.mnuFeeSchedule_Save.Click += new System.EventHandler(this.mnuFeeSchedule_Save_Click);
            // 
            // mnuFeeSchedule_Close
            // 
            this.mnuFeeSchedule_Close.Name = "mnuFeeSchedule_Close";
            this.mnuFeeSchedule_Close.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuFeeSchedule_Close.Size = new System.Drawing.Size(161, 22);
            this.mnuFeeSchedule_Close.Text = "Close";
            this.mnuFeeSchedule_Close.Click += new System.EventHandler(this.mnuFeeSchedule_Close_Click);
            // 
            // pnlEOBFeeSchedule
            // 
            this.pnlEOBFeeSchedule.Controls.Add(this.panel6);
            this.pnlEOBFeeSchedule.Controls.Add(this.panel7);
            this.pnlEOBFeeSchedule.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEOBFeeSchedule.Location = new System.Drawing.Point(0, 54);
            this.pnlEOBFeeSchedule.Name = "pnlEOBFeeSchedule";
            this.pnlEOBFeeSchedule.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlEOBFeeSchedule.Size = new System.Drawing.Size(876, 64);
            this.pnlEOBFeeSchedule.TabIndex = 19;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnGenerateFeeSchedule);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.btnClearInsuranceCompany);
            this.panel6.Controls.Add(this.mskEobStartDate);
            this.panel6.Controls.Add(this.btnGetInsuranceCompany);
            this.panel6.Controls.Add(this.mskEobToDate);
            this.panel6.Controls.Add(this.cmbInsuranceCompany);
            this.panel6.Controls.Add(this.label14);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 29);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(870, 35);
            this.panel6.TabIndex = 58;
            // 
            // btnGenerateFeeSchedule
            // 
            this.btnGenerateFeeSchedule.AutoEllipsis = true;
            this.btnGenerateFeeSchedule.BackColor = System.Drawing.Color.Transparent;
            this.btnGenerateFeeSchedule.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGenerateFeeSchedule.BackgroundImage")));
            this.btnGenerateFeeSchedule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGenerateFeeSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateFeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateFeeSchedule.Location = new System.Drawing.Point(789, 5);
            this.btnGenerateFeeSchedule.Name = "btnGenerateFeeSchedule";
            this.btnGenerateFeeSchedule.Size = new System.Drawing.Size(77, 24);
            this.btnGenerateFeeSchedule.TabIndex = 60;
            this.btnGenerateFeeSchedule.Text = "Generate";
            this.btnGenerateFeeSchedule.UseVisualStyleBackColor = false;
            this.btnGenerateFeeSchedule.Click += new System.EventHandler(this.btnGenerateFeeSchedule_Click);
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Location = new System.Drawing.Point(1, 34);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(868, 1);
            this.label23.TabIndex = 59;
            this.label23.Text = "label23";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Location = new System.Drawing.Point(869, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 35);
            this.label22.TabIndex = 57;
            this.label22.Text = "label22";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 35);
            this.label21.TabIndex = 56;
            this.label21.Text = "label21";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(371, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 14);
            this.label7.TabIndex = 47;
            this.label7.Text = "Start  Date :\r\n";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoEllipsis = true;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(361, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 14);
            this.label10.TabIndex = 48;
            this.label10.Text = "*";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoEllipsis = true;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(585, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 14);
            this.label9.TabIndex = 49;
            this.label9.Text = "*";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(596, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 14);
            this.label8.TabIndex = 46;
            this.label8.Text = "End Date :";
            // 
            // btnClearInsuranceCompany
            // 
            this.btnClearInsuranceCompany.AutoEllipsis = true;
            this.btnClearInsuranceCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsuranceCompany.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsuranceCompany.BackgroundImage")));
            this.btnClearInsuranceCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsuranceCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsuranceCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsuranceCompany.Image")));
            this.btnClearInsuranceCompany.Location = new System.Drawing.Point(316, 6);
            this.btnClearInsuranceCompany.Name = "btnClearInsuranceCompany";
            this.btnClearInsuranceCompany.Size = new System.Drawing.Size(22, 22);
            this.btnClearInsuranceCompany.TabIndex = 53;
            this.btnClearInsuranceCompany.UseVisualStyleBackColor = false;
            this.btnClearInsuranceCompany.Click += new System.EventHandler(this.btnClearInsuranceCompany_Click);
            // 
            // mskEobStartDate
            // 
            this.mskEobStartDate.Location = new System.Drawing.Point(451, 6);
            this.mskEobStartDate.Mask = "00/00/0000";
            this.mskEobStartDate.Name = "mskEobStartDate";
            this.mskEobStartDate.Size = new System.Drawing.Size(118, 22);
            this.mskEobStartDate.TabIndex = 58;
            this.mskEobStartDate.ValidatingType = typeof(System.DateTime);
            this.mskEobStartDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskStartDate_MouseClick);
            this.mskEobStartDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskEobStartDate_Validating);
            // 
            // btnGetInsuranceCompany
            // 
            this.btnGetInsuranceCompany.AutoEllipsis = true;
            this.btnGetInsuranceCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnGetInsuranceCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGetInsuranceCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetInsuranceCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnGetInsuranceCompany.Image")));
            this.btnGetInsuranceCompany.Location = new System.Drawing.Point(290, 6);
            this.btnGetInsuranceCompany.Name = "btnGetInsuranceCompany";
            this.btnGetInsuranceCompany.Size = new System.Drawing.Size(22, 22);
            this.btnGetInsuranceCompany.TabIndex = 52;
            this.btnGetInsuranceCompany.UseVisualStyleBackColor = false;
            this.btnGetInsuranceCompany.Click += new System.EventHandler(this.btnGetInsuranceCompany_Click);
            // 
            // mskEobToDate
            // 
            this.mskEobToDate.Location = new System.Drawing.Point(666, 6);
            this.mskEobToDate.Mask = "00/00/0000";
            this.mskEobToDate.Name = "mskEobToDate";
            this.mskEobToDate.Size = new System.Drawing.Size(117, 22);
            this.mskEobToDate.TabIndex = 59;
            this.mskEobToDate.ValidatingType = typeof(System.DateTime);
            this.mskEobToDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskEndDate_MouseClick);
            this.mskEobToDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskEobToDate_Validating);
            // 
            // cmbInsuranceCompany
            // 
            this.cmbInsuranceCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbInsuranceCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbInsuranceCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsuranceCompany.FormattingEnabled = true;
            this.cmbInsuranceCompany.Location = new System.Drawing.Point(139, 6);
            this.cmbInsuranceCompany.Name = "cmbInsuranceCompany";
            this.cmbInsuranceCompany.Size = new System.Drawing.Size(147, 22);
            this.cmbInsuranceCompany.TabIndex = 51;
            this.cmbInsuranceCompany.SelectedIndexChanged += new System.EventHandler(this.cmbInsuranceCompany_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoEllipsis = true;
            this.label14.AutoSize = true;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(15, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(122, 14);
            this.label14.TabIndex = 50;
            this.label14.Text = "Insurance Company :";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label28);
            this.panel7.Controls.Add(this.label27);
            this.panel7.Controls.Add(this.label15);
            this.panel7.Controls.Add(this.label49);
            this.panel7.Controls.Add(this.label24);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(870, 26);
            this.panel7.TabIndex = 143;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Right;
            this.label28.Location = new System.Drawing.Point(869, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 24);
            this.label28.TabIndex = 144;
            this.label28.Text = "label28";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Left;
            this.label27.Location = new System.Drawing.Point(0, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 24);
            this.label27.TabIndex = 143;
            this.label27.Text = "label27";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(870, 1);
            this.label15.TabIndex = 142;
            this.label15.Text = "label15";
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Location = new System.Drawing.Point(0, 25);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(870, 1);
            this.label49.TabIndex = 141;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(0, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(870, 26);
            this.label24.TabIndex = 6;
            this.label24.Text = "Generate Fee Schedule Using Expected Collection";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlTopToolStrip
            // 
            this.pnlTopToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTopToolStrip.Controls.Add(this.TopToolStrip);
            this.pnlTopToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTopToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlTopToolStrip.Name = "pnlTopToolStrip";
            this.pnlTopToolStrip.Size = new System.Drawing.Size(876, 54);
            this.pnlTopToolStrip.TabIndex = 1;
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopToolStrip.BackgroundImage")));
            this.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopToolStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.TopToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnAddLine,
            this.ts_btnRemoveLine,
            this.tsbShow,
            this.ts_btnShowAll,
            this.tsb_Apply,
            this.ts_GenerateEOBFeeSchedule,
            this.ts_btnSave,
            this.ts_btnSave_FeeSchedule,
            this.ts_btnClose_FeeSchedule});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(876, 53);
            this.TopToolStrip.TabIndex = 45;
            this.TopToolStrip.TabStop = true;
            this.TopToolStrip.Text = "toolStrip1";
            // 
            // ts_btnAddLine
            // 
            this.ts_btnAddLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnAddLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnAddLine.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnAddLine.Image")));
            this.ts_btnAddLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnAddLine.Name = "ts_btnAddLine";
            this.ts_btnAddLine.Size = new System.Drawing.Size(65, 50);
            this.ts_btnAddLine.Tag = "AddLine";
            this.ts_btnAddLine.Text = "&Add Line";
            this.ts_btnAddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnAddLine.Click += new System.EventHandler(this.ts_btnAddLine_Click);
            // 
            // ts_btnRemoveLine
            // 
            this.ts_btnRemoveLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnRemoveLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnRemoveLine.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnRemoveLine.Image")));
            this.ts_btnRemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnRemoveLine.Name = "ts_btnRemoveLine";
            this.ts_btnRemoveLine.Size = new System.Drawing.Size(89, 50);
            this.ts_btnRemoveLine.Tag = "RemoveLine";
            this.ts_btnRemoveLine.Text = "Re&move Line";
            this.ts_btnRemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnRemoveLine.Click += new System.EventHandler(this.ts_btnRemoveLine_Click);
            // 
            // tsbShow
            // 
            this.tsbShow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbShow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsbShow.Image = ((System.Drawing.Image)(resources.GetObject("tsbShow.Image")));
            this.tsbShow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShow.Name = "tsbShow";
            this.tsbShow.Size = new System.Drawing.Size(72, 50);
            this.tsbShow.Tag = "Show List";
            this.tsbShow.Text = "Sho&w List";
            this.tsbShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbShow.ToolTipText = "Show List";
            this.tsbShow.Click += new System.EventHandler(this.tsbShow_Click);
            // 
            // ts_btnShowAll
            // 
            this.ts_btnShowAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnShowAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnShowAll.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnShowAll.Image")));
            this.ts_btnShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnShowAll.Name = "ts_btnShowAll";
            this.ts_btnShowAll.Size = new System.Drawing.Size(65, 50);
            this.ts_btnShowAll.Tag = "Show All";
            this.ts_btnShowAll.Text = "Show A&ll";
            this.ts_btnShowAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnShowAll.ToolTipText = "Show All";
            this.ts_btnShowAll.Visible = false;
            this.ts_btnShowAll.Click += new System.EventHandler(this.ts_btnShowAll_Click);
            // 
            // tsb_Apply
            // 
            this.tsb_Apply.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Apply.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Apply.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Apply.Image")));
            this.tsb_Apply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Apply.Name = "tsb_Apply";
            this.tsb_Apply.Size = new System.Drawing.Size(64, 50);
            this.tsb_Apply.Tag = "apply";
            this.tsb_Apply.Text = "A&pply %";
            this.tsb_Apply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Apply.ToolTipText = "Apply Percentage";
            this.tsb_Apply.Click += new System.EventHandler(this.tsb_Apply_Click);
            // 
            // ts_GenerateEOBFeeSchedule
            // 
            this.ts_GenerateEOBFeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.ts_GenerateEOBFeeSchedule.Image = ((System.Drawing.Image)(resources.GetObject("ts_GenerateEOBFeeSchedule.Image")));
            this.ts_GenerateEOBFeeSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_GenerateEOBFeeSchedule.Name = "ts_GenerateEOBFeeSchedule";
            this.ts_GenerateEOBFeeSchedule.Size = new System.Drawing.Size(66, 50);
            this.ts_GenerateEOBFeeSchedule.Text = "&Generate";
            this.ts_GenerateEOBFeeSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_GenerateEOBFeeSchedule.Click += new System.EventHandler(this.ts_GenerateEOBFeeSchedule_Click);
            // 
            // ts_btnSave
            // 
            this.ts_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSave.Image")));
            this.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSave.Name = "ts_btnSave";
            this.ts_btnSave.Size = new System.Drawing.Size(40, 50);
            this.ts_btnSave.Tag = "Save";
            this.ts_btnSave.Text = "&Save";
            this.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSave.ToolTipText = "Save";
            this.ts_btnSave.Click += new System.EventHandler(this.ts_btnSave_Click);
            // 
            // ts_btnSave_FeeSchedule
            // 
            this.ts_btnSave_FeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSave_FeeSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnSave_FeeSchedule.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSave_FeeSchedule.Image")));
            this.ts_btnSave_FeeSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSave_FeeSchedule.Name = "ts_btnSave_FeeSchedule";
            this.ts_btnSave_FeeSchedule.Size = new System.Drawing.Size(66, 50);
            this.ts_btnSave_FeeSchedule.Tag = "SaveFeeSchedule";
            this.ts_btnSave_FeeSchedule.Text = "Sa&ve&&Cls";
            this.ts_btnSave_FeeSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSave_FeeSchedule.ToolTipText = "Save and Close";
            this.ts_btnSave_FeeSchedule.Visible = false;
            this.ts_btnSave_FeeSchedule.Click += new System.EventHandler(this.ts_btnSave_FeeSchedule_Click_1);
            // 
            // ts_btnClose_FeeSchedule
            // 
            this.ts_btnClose_FeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose_FeeSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose_FeeSchedule.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose_FeeSchedule.Image")));
            this.ts_btnClose_FeeSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose_FeeSchedule.Name = "ts_btnClose_FeeSchedule";
            this.ts_btnClose_FeeSchedule.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose_FeeSchedule.Tag = "Close";
            this.ts_btnClose_FeeSchedule.Text = "&Close";
            this.ts_btnClose_FeeSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose_FeeSchedule.ToolTipText = "Close";
            this.ts_btnClose_FeeSchedule.Click += new System.EventHandler(this.ts_btnClose_FeeSchedule_Click);
            // 
            // frmSetupStdFeeSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(876, 658);
            this.Controls.Add(this.pnlFeeSchedule);
            this.Controls.Add(this.pnlEOBFeeSchedule);
            this.Controls.Add(this.pnlTopToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupStdFeeSchedule";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fee Schedule";
            this.Load += new System.EventHandler(this.frmSetupStdFeeSchedule_Load);
            this.pnlFeeSchedule.ResumeLayout(false);
            this.pnlFeeSchedule.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1StdFeeSchedule)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnl_Shortkeys.ResumeLayout(false);
            this.pnlSpeciality.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlEOBFeeSchedule.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.pnlTopToolStrip.ResumeLayout(false);
            this.pnlTopToolStrip.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFeeSchedule;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel pnlSpeciality;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlListControl;
        private System.Windows.Forms.Panel pnlTopToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnAddLine;
        private System.Windows.Forms.ToolStripButton ts_btnRemoveLine;
        private System.Windows.Forms.ToolStripButton ts_btnSave_FeeSchedule;
        private System.Windows.Forms.ToolStripButton ts_btnClose_FeeSchedule;
        private System.Windows.Forms.Panel pnl_Shortkeys;
        private System.Windows.Forms.Label lbl_KeySave;
        private System.Windows.Forms.Label lbl_shrtctKeySave;
        private System.Windows.Forms.Label lbl_Keyremoveline;
        private System.Windows.Forms.Label lbl_lshrtctKeyremoveline;
        private System.Windows.Forms.Label lbl_KeyAddline;
        private System.Windows.Forms.Label lbl_shrtctKeyAddline;
        private System.Windows.Forms.Label lbl_shrtctKeyClose;
        private System.Windows.Forms.Label lbl_KeyClose;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFeeSchedule;
        private System.Windows.Forms.ToolStripMenuItem mnuFeeSchedule_AddLine;
        private System.Windows.Forms.ToolStripMenuItem mnuFeeSchedule_RemoveLine;
        private System.Windows.Forms.ToolStripMenuItem mnuFeeSchedule_Save;
        private System.Windows.Forms.ToolStripMenuItem mnuFeeSchedule_Close;
        private C1.Win.C1FlexGrid.C1FlexGrid c1StdFeeSchedule;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton tsbShow;
        private System.Windows.Forms.ToolStripButton tsb_Apply;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtFromCPT;
        internal System.Windows.Forms.Label lblFromCPT;
        private System.Windows.Forms.TextBox txtToCPT;
        internal System.Windows.Forms.Label lblToCPT;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbNone;
        private System.Windows.Forms.RadioButton rdbroundDown;
        private System.Windows.Forms.RadioButton rdbroundUp;
        private System.Windows.Forms.TextBox txtChargesPercentage;
        internal System.Windows.Forms.Label lbl_stdFeeSchedule;
        private System.Windows.Forms.TextBox txt_StdFeeScheduleType;
        private System.Windows.Forms.Label lblMandtryEndDte;
        private System.Windows.Forms.Label lblMandatryFeeSchedule;
        private System.Windows.Forms.Label lblmandatryStrtDate;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.MaskedTextBox mskStartDate;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.MaskedTextBox mskEndDate;
        private System.Windows.Forms.RadioButton RdbIncreaseChrgeby;
        private System.Windows.Forms.RadioButton RdbSetCharges;
        private System.Windows.Forms.ToolStripButton ts_btnShowAll;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.Panel pnlEOBFeeSchedule;
        private System.Windows.Forms.ToolStripButton ts_GenerateEOBFeeSchedule;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnGenerateFeeSchedule;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClearInsuranceCompany;
        private System.Windows.Forms.MaskedTextBox mskEobStartDate;
        private System.Windows.Forms.Button btnGetInsuranceCompany;
        private System.Windows.Forms.MaskedTextBox mskEobToDate;
        private System.Windows.Forms.ComboBox cmbInsuranceCompany;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label15;
    }
}