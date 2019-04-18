namespace gloBilling
{
    partial class frm_SetupRVUSchedule
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpEffectivedate };
            System.Windows.Forms.Control[] cntControls = { dtpEffectivedate };

            if (disposing && (components != null))
            {
                components.Dispose();
                base.Dispose(disposing);
                try
                {
                    
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SetupRVUSchedule));
            this.pnlFeeSchedule = new System.Windows.Forms.Panel();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.c1RVUSchedule = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkHideZeroRVU = new System.Windows.Forms.CheckBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlSpeciality = new System.Windows.Forms.Panel();
            this.rbInactive = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpEffectivedate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_stdFeeSchedule = new System.Windows.Forms.Label();
            this.txt_RVUScheduleNote = new System.Windows.Forms.TextBox();
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
            this.pnlTopToolStrip = new System.Windows.Forms.Panel();
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnAddLine = new System.Windows.Forms.ToolStripButton();
            this.ts_btnRemoveLine = new System.Windows.Forms.ToolStripButton();
            this.tsb_ImportCSV = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSaveCls = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlFeeSchedule.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1RVUSchedule)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnl_Shortkeys.SuspendLayout();
            this.pnlSpeciality.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlTopToolStrip.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFeeSchedule
            // 
            this.pnlFeeSchedule.Controls.Add(this.pnlDetails);
            this.pnlFeeSchedule.Controls.Add(this.pnlSearch);
            this.pnlFeeSchedule.Controls.Add(this.pnlFooter);
            this.pnlFeeSchedule.Controls.Add(this.pnlSpeciality);
            this.pnlFeeSchedule.Controls.Add(this.pnlListControl);
            this.pnlFeeSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFeeSchedule.Location = new System.Drawing.Point(0, 54);
            this.pnlFeeSchedule.Name = "pnlFeeSchedule";
            this.pnlFeeSchedule.Size = new System.Drawing.Size(657, 604);
            this.pnlFeeSchedule.TabIndex = 0;
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.pnlInternalControl);
            this.pnlDetails.Controls.Add(this.label20);
            this.pnlDetails.Controls.Add(this.label19);
            this.pnlDetails.Controls.Add(this.label18);
            this.pnlDetails.Controls.Add(this.label17);
            this.pnlDetails.Controls.Add(this.c1RVUSchedule);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(0, 171);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlDetails.Size = new System.Drawing.Size(657, 407);
            this.pnlDetails.TabIndex = 2;
            this.pnlDetails.Leave += new System.EventHandler(this.pnlDetails_Leave);
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(83, 139);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(446, 128);
            this.pnlInternalControl.TabIndex = 7;
            this.pnlInternalControl.Visible = false;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(653, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 402);
            this.label20.TabIndex = 20;
            this.label20.Text = "label20";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(3, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 402);
            this.label19.TabIndex = 0;
            this.label19.Text = "label19";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(3, 403);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(651, 1);
            this.label18.TabIndex = 18;
            this.label18.Text = "label18";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(651, 1);
            this.label17.TabIndex = 0;
            this.label17.Text = "label17";
            // 
            // c1RVUSchedule
            // 
            this.c1RVUSchedule.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1RVUSchedule.AllowFiltering = true;
            this.c1RVUSchedule.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1RVUSchedule.AutoGenerateColumns = false;
            this.c1RVUSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1RVUSchedule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1RVUSchedule.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1RVUSchedule.ColumnInfo = resources.GetString("c1RVUSchedule.ColumnInfo");
            this.c1RVUSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1RVUSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1RVUSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1RVUSchedule.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1RVUSchedule.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1RVUSchedule.Location = new System.Drawing.Point(3, 0);
            this.c1RVUSchedule.Name = "c1RVUSchedule";
            this.c1RVUSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.c1RVUSchedule.Rows.Count = 1;
            this.c1RVUSchedule.Rows.DefaultSize = 19;
            this.c1RVUSchedule.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1RVUSchedule.Size = new System.Drawing.Size(651, 404);
            this.c1RVUSchedule.StyleInfo = resources.GetString("c1RVUSchedule.StyleInfo");
            this.c1RVUSchedule.TabIndex = 0;
            this.c1RVUSchedule.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1RVUSchedule_AfterRowColChange);
            this.c1RVUSchedule.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1RVUSchedule_BeforeSelChange);
            this.c1RVUSchedule.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1RVUSchedule_StartEdit);
            this.c1RVUSchedule.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1RVUSchedule_AfterEdit);
            this.c1RVUSchedule.LeaveEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1RVUSchedule_LeaveEdit);
            this.c1RVUSchedule.SetupEditor += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1RVUSchedule_SetupEditor);
            this.c1RVUSchedule.ChangeEdit += new System.EventHandler(this.c1RVUSchedule_ChangeEdit);
            this.c1RVUSchedule.KeyDownEdit += new C1.Win.C1FlexGrid.KeyEditEventHandler(this.c1RVUSchedule_KeyDownEdit);
            this.c1RVUSchedule.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.c1RVUSchedule_KeyPressEdit);
            this.c1RVUSchedule.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1RVUSchedule_KeyUp);
            this.c1RVUSchedule.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1RVUSchedule_MouseMove);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.panel4);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 144);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlSearch.Size = new System.Drawing.Size(657, 27);
            this.pnlSearch.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.chkHideZeroRVU);
            this.panel4.Controls.Add(this.txtSearch);
            this.panel4.Controls.Add(this.lblSearch);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(651, 24);
            this.panel4.TabIndex = 1;
            // 
            // chkHideZeroRVU
            // 
            this.chkHideZeroRVU.AutoSize = true;
            this.chkHideZeroRVU.Location = new System.Drawing.Point(522, 3);
            this.chkHideZeroRVU.Name = "chkHideZeroRVU";
            this.chkHideZeroRVU.Size = new System.Drawing.Size(106, 18);
            this.chkHideZeroRVU.TabIndex = 2;
            this.chkHideZeroRVU.Text = "Hide Zero RVU";
            this.chkHideZeroRVU.UseVisualStyleBackColor = true;
            this.chkHideZeroRVU.CheckedChanged += new System.EventHandler(this.chkHideZeroRVU_CheckedChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(65, 1);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(162, 22);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(1, 1);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSearch.Size = new System.Drawing.Size(64, 17);
            this.lblSearch.TabIndex = 6;
            this.lblSearch.Text = "  Search :";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(1, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(649, 1);
            this.label7.TabIndex = 8;
            this.label7.Text = "label2";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 23);
            this.label8.TabIndex = 7;
            this.label8.Text = "label4";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(650, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 23);
            this.label9.TabIndex = 6;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(651, 1);
            this.label10.TabIndex = 5;
            this.label10.Text = "label1";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.pnl_Shortkeys);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 578);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlFooter.Size = new System.Drawing.Size(657, 26);
            this.pnlFooter.TabIndex = 333;
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
            this.pnl_Shortkeys.Controls.Add(this.label1);
            this.pnl_Shortkeys.Controls.Add(this.label3);
            this.pnl_Shortkeys.Controls.Add(this.label2);
            this.pnl_Shortkeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Shortkeys.Location = new System.Drawing.Point(3, 0);
            this.pnl_Shortkeys.Name = "pnl_Shortkeys";
            this.pnl_Shortkeys.Size = new System.Drawing.Size(651, 23);
            this.pnl_Shortkeys.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(650, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 21);
            this.label4.TabIndex = 68;
            this.label4.Text = "label4";
            // 
            // lbl_KeyClose
            // 
            this.lbl_KeyClose.AutoSize = true;
            this.lbl_KeyClose.BackColor = System.Drawing.Color.Transparent;
            this.lbl_KeyClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_KeyClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeyClose.Location = new System.Drawing.Point(445, 1);
            this.lbl_KeyClose.Name = "lbl_KeyClose";
            this.lbl_KeyClose.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_KeyClose.Size = new System.Drawing.Size(52, 18);
            this.lbl_KeyClose.TabIndex = 63;
            this.lbl_KeyClose.Text = "- Close";
            // 
            // lbl_shrtctKeyClose
            // 
            this.lbl_shrtctKeyClose.AutoSize = true;
            this.lbl_shrtctKeyClose.BackColor = System.Drawing.Color.Transparent;
            this.lbl_shrtctKeyClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_shrtctKeyClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shrtctKeyClose.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_shrtctKeyClose.Location = new System.Drawing.Point(384, 1);
            this.lbl_shrtctKeyClose.Name = "lbl_shrtctKeyClose";
            this.lbl_shrtctKeyClose.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_shrtctKeyClose.Size = new System.Drawing.Size(61, 18);
            this.lbl_shrtctKeyClose.TabIndex = 64;
            this.lbl_shrtctKeyClose.Text = "Alt + F4";
            // 
            // lbl_KeySave
            // 
            this.lbl_KeySave.AutoSize = true;
            this.lbl_KeySave.BackColor = System.Drawing.Color.Transparent;
            this.lbl_KeySave.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_KeySave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeySave.Location = new System.Drawing.Point(286, 1);
            this.lbl_KeySave.Name = "lbl_KeySave";
            this.lbl_KeySave.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_KeySave.Size = new System.Drawing.Size(98, 18);
            this.lbl_KeySave.TabIndex = 56;
            this.lbl_KeySave.Text = "- Save && Close";
            // 
            // lbl_shrtctKeySave
            // 
            this.lbl_shrtctKeySave.AutoSize = true;
            this.lbl_shrtctKeySave.BackColor = System.Drawing.Color.Transparent;
            this.lbl_shrtctKeySave.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_shrtctKeySave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shrtctKeySave.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_shrtctKeySave.Location = new System.Drawing.Point(227, 1);
            this.lbl_shrtctKeySave.Name = "lbl_shrtctKeySave";
            this.lbl_shrtctKeySave.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_shrtctKeySave.Size = new System.Drawing.Size(59, 18);
            this.lbl_shrtctKeySave.TabIndex = 55;
            this.lbl_shrtctKeySave.Text = "Ctrl + S";
            // 
            // lbl_Keyremoveline
            // 
            this.lbl_Keyremoveline.AutoSize = true;
            this.lbl_Keyremoveline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Keyremoveline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Keyremoveline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Keyremoveline.Location = new System.Drawing.Point(129, 1);
            this.lbl_Keyremoveline.Name = "lbl_Keyremoveline";
            this.lbl_Keyremoveline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_Keyremoveline.Size = new System.Drawing.Size(98, 18);
            this.lbl_Keyremoveline.TabIndex = 46;
            this.lbl_Keyremoveline.Text = "- Remove Line";
            // 
            // lbl_lshrtctKeyremoveline
            // 
            this.lbl_lshrtctKeyremoveline.AutoSize = true;
            this.lbl_lshrtctKeyremoveline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_lshrtctKeyremoveline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_lshrtctKeyremoveline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_lshrtctKeyremoveline.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_lshrtctKeyremoveline.Location = new System.Drawing.Point(104, 1);
            this.lbl_lshrtctKeyremoveline.Name = "lbl_lshrtctKeyremoveline";
            this.lbl_lshrtctKeyremoveline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_lshrtctKeyremoveline.Size = new System.Drawing.Size(25, 18);
            this.lbl_lshrtctKeyremoveline.TabIndex = 45;
            this.lbl_lshrtctKeyremoveline.Text = "F3";
            // 
            // lbl_KeyAddline
            // 
            this.lbl_KeyAddline.AutoSize = true;
            this.lbl_KeyAddline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_KeyAddline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_KeyAddline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeyAddline.Location = new System.Drawing.Point(30, 1);
            this.lbl_KeyAddline.Name = "lbl_KeyAddline";
            this.lbl_KeyAddline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_KeyAddline.Size = new System.Drawing.Size(74, 18);
            this.lbl_KeyAddline.TabIndex = 44;
            this.lbl_KeyAddline.Text = "- Add Line";
            // 
            // lbl_shrtctKeyAddline
            // 
            this.lbl_shrtctKeyAddline.AutoSize = true;
            this.lbl_shrtctKeyAddline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_shrtctKeyAddline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_shrtctKeyAddline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shrtctKeyAddline.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_shrtctKeyAddline.Location = new System.Drawing.Point(1, 1);
            this.lbl_shrtctKeyAddline.Name = "lbl_shrtctKeyAddline";
            this.lbl_shrtctKeyAddline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_shrtctKeyAddline.Size = new System.Drawing.Size(29, 18);
            this.lbl_shrtctKeyAddline.TabIndex = 44;
            this.lbl_shrtctKeyAddline.Text = "F2 ";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(1, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(650, 1);
            this.label1.TabIndex = 65;
            this.label1.Text = "label1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 22);
            this.label3.TabIndex = 67;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(651, 1);
            this.label2.TabIndex = 69;
            this.label2.Text = "label2";
            // 
            // pnlSpeciality
            // 
            this.pnlSpeciality.Controls.Add(this.rbInactive);
            this.pnlSpeciality.Controls.Add(this.label14);
            this.pnlSpeciality.Controls.Add(this.rbActive);
            this.pnlSpeciality.Controls.Add(this.label6);
            this.pnlSpeciality.Controls.Add(this.dtpEffectivedate);
            this.pnlSpeciality.Controls.Add(this.label5);
            this.pnlSpeciality.Controls.Add(this.lbl_stdFeeSchedule);
            this.pnlSpeciality.Controls.Add(this.txt_RVUScheduleNote);
            this.pnlSpeciality.Controls.Add(this.label16);
            this.pnlSpeciality.Controls.Add(this.label13);
            this.pnlSpeciality.Controls.Add(this.label12);
            this.pnlSpeciality.Controls.Add(this.label11);
            this.pnlSpeciality.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpeciality.Location = new System.Drawing.Point(0, 0);
            this.pnlSpeciality.Name = "pnlSpeciality";
            this.pnlSpeciality.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSpeciality.Size = new System.Drawing.Size(657, 144);
            this.pnlSpeciality.TabIndex = 1;
            // 
            // rbInactive
            // 
            this.rbInactive.AutoSize = true;
            this.rbInactive.Location = new System.Drawing.Point(208, 45);
            this.rbInactive.Name = "rbInactive";
            this.rbInactive.Size = new System.Drawing.Size(68, 18);
            this.rbInactive.TabIndex = 2;
            this.rbInactive.Text = "Inactive";
            this.rbInactive.UseVisualStyleBackColor = true;
            this.rbInactive.CheckedChanged += new System.EventHandler(this.rbInactive_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(82, 45);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 14);
            this.label14.TabIndex = 125;
            this.label14.Text = "Status :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbActive
            // 
            this.rbActive.AutoSize = true;
            this.rbActive.Checked = true;
            this.rbActive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbActive.Location = new System.Drawing.Point(135, 44);
            this.rbActive.Name = "rbActive";
            this.rbActive.Size = new System.Drawing.Size(63, 18);
            this.rbActive.TabIndex = 1;
            this.rbActive.TabStop = true;
            this.rbActive.Text = "Active";
            this.rbActive.UseVisualStyleBackColor = true;
            this.rbActive.CheckedChanged += new System.EventHandler(this.rbActive_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(25, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 14);
            this.label6.TabIndex = 43;
            this.label6.Text = "*";
            // 
            // dtpEffectivedate
            // 
            this.dtpEffectivedate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEffectivedate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEffectivedate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpEffectivedate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpEffectivedate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEffectivedate.CustomFormat = "MM/dd/yyyy";
            this.dtpEffectivedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEffectivedate.Location = new System.Drawing.Point(135, 13);
            this.dtpEffectivedate.Name = "dtpEffectivedate";
            this.dtpEffectivedate.Size = new System.Drawing.Size(113, 22);
            this.dtpEffectivedate.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(39, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 14);
            this.label5.TabIndex = 42;
            this.label5.Text = "Effective Date :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_stdFeeSchedule
            // 
            this.lbl_stdFeeSchedule.AutoSize = true;
            this.lbl_stdFeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_stdFeeSchedule.Location = new System.Drawing.Point(90, 72);
            this.lbl_stdFeeSchedule.Name = "lbl_stdFeeSchedule";
            this.lbl_stdFeeSchedule.Size = new System.Drawing.Size(42, 14);
            this.lbl_stdFeeSchedule.TabIndex = 20;
            this.lbl_stdFeeSchedule.Text = "Note :";
            // 
            // txt_RVUScheduleNote
            // 
            this.txt_RVUScheduleNote.ForeColor = System.Drawing.Color.Black;
            this.txt_RVUScheduleNote.Location = new System.Drawing.Point(135, 71);
            this.txt_RVUScheduleNote.MaxLength = 255;
            this.txt_RVUScheduleNote.Multiline = true;
            this.txt_RVUScheduleNote.Name = "txt_RVUScheduleNote";
            this.txt_RVUScheduleNote.Size = new System.Drawing.Size(316, 58);
            this.txt_RVUScheduleNote.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Location = new System.Drawing.Point(653, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 136);
            this.label16.TabIndex = 18;
            this.label16.Text = "label16";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Location = new System.Drawing.Point(4, 140);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(650, 1);
            this.label13.TabIndex = 17;
            this.label13.Text = "label13";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(4, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(650, 1);
            this.label12.TabIndex = 16;
            this.label12.Text = "label12";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 138);
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
            this.menuStrip1.Location = new System.Drawing.Point(378, 9);
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
            // pnlTopToolStrip
            // 
            this.pnlTopToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTopToolStrip.Controls.Add(this.TopToolStrip);
            this.pnlTopToolStrip.Controls.Add(this.menuStrip1);
            this.pnlTopToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTopToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlTopToolStrip.Name = "pnlTopToolStrip";
            this.pnlTopToolStrip.Size = new System.Drawing.Size(657, 54);
            this.pnlTopToolStrip.TabIndex = 3;
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
            this.tsb_ImportCSV,
            this.ts_btnSave,
            this.ts_btnSaveCls,
            this.ts_btnClose});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(657, 53);
            this.TopToolStrip.TabIndex = 7;
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
            // tsb_ImportCSV
            // 
            this.tsb_ImportCSV.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ImportCSV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ImportCSV.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ImportCSV.Image")));
            this.tsb_ImportCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ImportCSV.Name = "tsb_ImportCSV";
            this.tsb_ImportCSV.Size = new System.Drawing.Size(54, 50);
            this.tsb_ImportCSV.Tag = "Import";
            this.tsb_ImportCSV.Text = "&Import";
            this.tsb_ImportCSV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ImportCSV.ToolTipText = "Create New RVU Schedule from CMS File";
            this.tsb_ImportCSV.Click += new System.EventHandler(this.tsb_ImportCSV_Click);
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
            // ts_btnSaveCls
            // 
            this.ts_btnSaveCls.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSaveCls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnSaveCls.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSaveCls.Image")));
            this.ts_btnSaveCls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSaveCls.Name = "ts_btnSaveCls";
            this.ts_btnSaveCls.Size = new System.Drawing.Size(66, 50);
            this.ts_btnSaveCls.Tag = "SaveFeeSchedule";
            this.ts_btnSaveCls.Text = "Sa&ve&&Cls";
            this.ts_btnSaveCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSaveCls.ToolTipText = "Save and Close";
            this.ts_btnSaveCls.Click += new System.EventHandler(this.ts_btnSaveCls_Click);
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.ToolTipText = "Close";
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frm_SetupRVUSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(657, 658);
            this.Controls.Add(this.pnlFeeSchedule);
            this.Controls.Add(this.pnlTopToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SetupRVUSchedule";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RVU Schedule";
            this.Load += new System.EventHandler(this.frm_SetupRVUSchedule_Load);
            this.pnlFeeSchedule.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1RVUSchedule)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnl_Shortkeys.ResumeLayout(false);
            this.pnl_Shortkeys.PerformLayout();
            this.pnlSpeciality.ResumeLayout(false);
            this.pnlSpeciality.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlTopToolStrip.ResumeLayout(false);
            this.pnlTopToolStrip.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFeeSchedule;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel pnlSpeciality;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlListControl;
        private System.Windows.Forms.TextBox txt_RVUScheduleNote;
        private System.Windows.Forms.Panel pnlTopToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnAddLine;
        private System.Windows.Forms.ToolStripButton ts_btnRemoveLine;
        private System.Windows.Forms.ToolStripButton ts_btnSaveCls;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        internal System.Windows.Forms.Label lbl_stdFeeSchedule;
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
        private System.Windows.Forms.ToolStripButton tsb_ImportCSV;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpEffectivedate;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbInactive;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton rbActive;
        private System.Windows.Forms.CheckBox chkHideZeroRVU;
        private System.Windows.Forms.Panel pnlInternalControl;
        private C1.Win.C1FlexGrid.C1FlexGrid c1RVUSchedule;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}