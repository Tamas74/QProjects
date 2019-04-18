namespace gloBilling
{
    partial class frmCPTMapping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCPTMapping));
            this.pnlFeeSchedule = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.c1CPTMapping = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.c1Insurance = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_Shortkeys = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_Keyremoveline = new System.Windows.Forms.Label();
            this.lbl_lshrtctKeyremoveline = new System.Windows.Forms.Label();
            this.lbl_KeyAddline = new System.Windows.Forms.Label();
            this.lbl_shrtctKeyAddline = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlSpeciality = new System.Windows.Forms.Panel();
            this.lblCptMapping = new System.Windows.Forms.Label();
            this.txtcptmapping = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pnlTopToolStrip = new System.Windows.Forms.Panel();
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnAddLine = new System.Windows.Forms.ToolStripButton();
            this.ts_btnRemoveLine = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSaveClose = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuBilling = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_AddLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_RemoveLine = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlFeeSchedule.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CPTMapping)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Insurance)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnl_Shortkeys.SuspendLayout();
            this.pnlSpeciality.SuspendLayout();
            this.pnlTopToolStrip.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFeeSchedule
            // 
            this.pnlFeeSchedule.Controls.Add(this.panel3);
            this.pnlFeeSchedule.Controls.Add(this.panel5);
            this.pnlFeeSchedule.Controls.Add(this.panel7);
            this.pnlFeeSchedule.Controls.Add(this.panel4);
            this.pnlFeeSchedule.Controls.Add(this.panel1);
            this.pnlFeeSchedule.Controls.Add(this.pnlSpeciality);
            this.pnlFeeSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFeeSchedule.Location = new System.Drawing.Point(0, 54);
            this.pnlFeeSchedule.Name = "pnlFeeSchedule";
            this.pnlFeeSchedule.Size = new System.Drawing.Size(997, 604);
            this.pnlFeeSchedule.TabIndex = 0;
            this.pnlFeeSchedule.TabStop = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.pnlInternalControl);
            this.panel3.Controls.Add(this.c1CPTMapping);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 68);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel3.Size = new System.Drawing.Size(997, 298);
            this.panel3.TabIndex = 1;
            this.panel3.TabStop = true;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(993, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 293);
            this.label20.TabIndex = 20;
            this.label20.Text = "label20";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(3, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 293);
            this.label19.TabIndex = 0;
            this.label19.Text = "label19";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(3, 294);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(991, 1);
            this.label18.TabIndex = 18;
            this.label18.Text = "label18";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(991, 1);
            this.label17.TabIndex = 0;
            this.label17.Text = "label17";
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(207, 50);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(446, 128);
            this.pnlInternalControl.TabIndex = 28;
            this.pnlInternalControl.Visible = false;
            // 
            // c1CPTMapping
            // 
            this.c1CPTMapping.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1CPTMapping.AutoResize = false;
            this.c1CPTMapping.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1CPTMapping.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1CPTMapping.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1CPTMapping.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1CPTMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1CPTMapping.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1CPTMapping.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1CPTMapping.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1CPTMapping.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1CPTMapping.Location = new System.Drawing.Point(3, 0);
            this.c1CPTMapping.Name = "c1CPTMapping";
            this.c1CPTMapping.Padding = new System.Windows.Forms.Padding(3);
            this.c1CPTMapping.Rows.Count = 1;
            this.c1CPTMapping.Rows.DefaultSize = 19;
            this.c1CPTMapping.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1CPTMapping.ShowCellLabels = true;
            this.c1CPTMapping.Size = new System.Drawing.Size(991, 295);
            this.c1CPTMapping.StyleInfo = resources.GetString("c1CPTMapping.StyleInfo");
            this.c1CPTMapping.TabIndex = 2;
            this.c1CPTMapping.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1CPTMapping_BeforeSelChange);
            this.c1CPTMapping.Enter += new System.EventHandler(this.c1CPTMapping_Enter);
            this.c1CPTMapping.ChangeEdit += new System.EventHandler(this.c1CPTMapping_ChangeEdit);
            this.c1CPTMapping.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1CPTMapping_AfterRowColChange);
            this.c1CPTMapping.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1CPTMapping_StartEdit);
            this.c1CPTMapping.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1CPTMapping_AfterEdit);
            this.c1CPTMapping.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1CPTMapping_KeyDown);
            this.c1CPTMapping.LeaveEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1CPTMapping_LeaveEdit);
            this.c1CPTMapping.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1CPTMapping_KeyUp);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 366);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel5.Size = new System.Drawing.Size(997, 28);
            this.panel5.TabIndex = 4545;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label28);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.label14);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(991, 25);
            this.panel6.TabIndex = 334;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.White;
            this.label28.Location = new System.Drawing.Point(9, 6);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(205, 14);
            this.label28.TabIndex = 22;
            this.label28.Text = "Insurance Plans Using Crosswalk";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Location = new System.Drawing.Point(990, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 23);
            this.label9.TabIndex = 20;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 23);
            this.label10.TabIndex = 0;
            this.label10.Text = "label10";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(0, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(991, 1);
            this.label14.TabIndex = 18;
            this.label14.Text = "label14";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(991, 1);
            this.label22.TabIndex = 0;
            this.label22.Text = "label22";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label23);
            this.panel7.Controls.Add(this.label24);
            this.panel7.Controls.Add(this.label25);
            this.panel7.Controls.Add(this.label26);
            this.panel7.Controls.Add(this.c1Insurance);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 394);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel7.Size = new System.Drawing.Size(997, 184);
            this.panel7.TabIndex = 2;
            this.panel7.TabStop = true;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Location = new System.Drawing.Point(993, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 179);
            this.label23.TabIndex = 20;
            this.label23.Text = "label23";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.label24.Location = new System.Drawing.Point(3, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 179);
            this.label24.TabIndex = 0;
            this.label24.Text = "label24";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label25.Location = new System.Drawing.Point(3, 180);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(991, 1);
            this.label25.TabIndex = 18;
            this.label25.Text = "label25";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Top;
            this.label26.Location = new System.Drawing.Point(3, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(991, 1);
            this.label26.TabIndex = 0;
            this.label26.Text = "label26";
            // 
            // c1Insurance
            // 
            this.c1Insurance.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Insurance.AllowEditing = false;
            this.c1Insurance.AutoResize = false;
            this.c1Insurance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Insurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Insurance.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Insurance.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1Insurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Insurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Insurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Insurance.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Insurance.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1Insurance.Location = new System.Drawing.Point(3, 0);
            this.c1Insurance.Name = "c1Insurance";
            this.c1Insurance.Padding = new System.Windows.Forms.Padding(3);
            this.c1Insurance.Rows.Count = 1;
            this.c1Insurance.Rows.DefaultSize = 19;
            this.c1Insurance.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Insurance.ShowCellLabels = true;
            this.c1Insurance.Size = new System.Drawing.Size(991, 181);
            this.c1Insurance.StyleInfo = resources.GetString("c1Insurance.StyleInfo");
            this.c1Insurance.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 40);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel4.Size = new System.Drawing.Size(997, 28);
            this.panel4.TabIndex = 335;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(991, 25);
            this.panel2.TabIndex = 334;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.White;
            this.label27.Location = new System.Drawing.Point(9, 5);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(108, 14);
            this.label27.TabIndex = 21;
            this.label27.Text = "Billing Crosswalk";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(990, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 23);
            this.label5.TabIndex = 20;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(0, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(991, 1);
            this.label7.TabIndex = 18;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(991, 1);
            this.label8.TabIndex = 0;
            this.label8.Text = "label8";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnl_Shortkeys);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 578);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(997, 26);
            this.panel1.TabIndex = 333;
            // 
            // pnl_Shortkeys
            // 
            this.pnl_Shortkeys.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnl_Shortkeys.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Shortkeys.Controls.Add(this.label4);
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
            this.pnl_Shortkeys.Size = new System.Drawing.Size(991, 23);
            this.pnl_Shortkeys.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(990, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 21);
            this.label4.TabIndex = 68;
            this.label4.Text = "label4";
            // 
            // lbl_Keyremoveline
            // 
            this.lbl_Keyremoveline.AutoSize = true;
            this.lbl_Keyremoveline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Keyremoveline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Keyremoveline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Keyremoveline.Location = new System.Drawing.Point(128, 1);
            this.lbl_Keyremoveline.Name = "lbl_Keyremoveline";
            this.lbl_Keyremoveline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_Keyremoveline.Size = new System.Drawing.Size(98, 18);
            this.lbl_Keyremoveline.TabIndex = 46;
            this.lbl_Keyremoveline.Text = "- Remove Line";
            // 
            // lbl_lshrtctKeyremoveline
            // 
            this.lbl_lshrtctKeyremoveline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_lshrtctKeyremoveline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_lshrtctKeyremoveline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_lshrtctKeyremoveline.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_lshrtctKeyremoveline.Location = new System.Drawing.Point(102, 1);
            this.lbl_lshrtctKeyremoveline.Name = "lbl_lshrtctKeyremoveline";
            this.lbl_lshrtctKeyremoveline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_lshrtctKeyremoveline.Size = new System.Drawing.Size(26, 21);
            this.lbl_lshrtctKeyremoveline.TabIndex = 45;
            this.lbl_lshrtctKeyremoveline.Text = "F3";
            // 
            // lbl_KeyAddline
            // 
            this.lbl_KeyAddline.AutoSize = true;
            this.lbl_KeyAddline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_KeyAddline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_KeyAddline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeyAddline.Location = new System.Drawing.Point(28, 1);
            this.lbl_KeyAddline.Name = "lbl_KeyAddline";
            this.lbl_KeyAddline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_KeyAddline.Size = new System.Drawing.Size(74, 18);
            this.lbl_KeyAddline.TabIndex = 44;
            this.lbl_KeyAddline.Text = "- Add Line";
            // 
            // lbl_shrtctKeyAddline
            // 
            this.lbl_shrtctKeyAddline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_shrtctKeyAddline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_shrtctKeyAddline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shrtctKeyAddline.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_shrtctKeyAddline.Location = new System.Drawing.Point(1, 1);
            this.lbl_shrtctKeyAddline.Name = "lbl_shrtctKeyAddline";
            this.lbl_shrtctKeyAddline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_shrtctKeyAddline.Size = new System.Drawing.Size(27, 21);
            this.lbl_shrtctKeyAddline.TabIndex = 44;
            this.lbl_shrtctKeyAddline.Text = "F2 ";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(990, 1);
            this.label2.TabIndex = 66;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(1, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(990, 1);
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
            this.pnlSpeciality.Controls.Add(this.lblCptMapping);
            this.pnlSpeciality.Controls.Add(this.txtcptmapping);
            this.pnlSpeciality.Controls.Add(this.label16);
            this.pnlSpeciality.Controls.Add(this.label13);
            this.pnlSpeciality.Controls.Add(this.label12);
            this.pnlSpeciality.Controls.Add(this.label11);
            this.pnlSpeciality.Controls.Add(this.label39);
            this.pnlSpeciality.Controls.Add(this.label21);
            this.pnlSpeciality.Controls.Add(this.label15);
            this.pnlSpeciality.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpeciality.Location = new System.Drawing.Point(0, 0);
            this.pnlSpeciality.Name = "pnlSpeciality";
            this.pnlSpeciality.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSpeciality.Size = new System.Drawing.Size(997, 40);
            this.pnlSpeciality.TabIndex = 0;
            this.pnlSpeciality.TabStop = true;
            // 
            // lblCptMapping
            // 
            this.lblCptMapping.AutoSize = true;
            this.lblCptMapping.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCptMapping.Location = new System.Drawing.Point(29, 12);
            this.lblCptMapping.Name = "lblCptMapping";
            this.lblCptMapping.Size = new System.Drawing.Size(161, 14);
            this.lblCptMapping.TabIndex = 20;
            this.lblCptMapping.Text = "CPT Billing Crosswalk Name :";
            // 
            // txtcptmapping
            // 
            this.txtcptmapping.ForeColor = System.Drawing.Color.Black;
            this.txtcptmapping.Location = new System.Drawing.Point(193, 8);
            this.txtcptmapping.MaxLength = 250;
            this.txtcptmapping.Name = "txtcptmapping";
            this.txtcptmapping.Size = new System.Drawing.Size(200, 22);
            this.txtcptmapping.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Location = new System.Drawing.Point(993, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 32);
            this.label16.TabIndex = 18;
            this.label16.Text = "label16";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Location = new System.Drawing.Point(4, 36);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(990, 1);
            this.label13.TabIndex = 17;
            this.label13.Text = "label13";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(4, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(990, 1);
            this.label12.TabIndex = 16;
            this.label12.Text = "label12";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 34);
            this.label11.TabIndex = 15;
            this.label11.Text = "label11";
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.AutoEllipsis = true;
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(17, 11);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(14, 14);
            this.label39.TabIndex = 31;
            this.label39.Text = "*";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoEllipsis = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(61, 137);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(244, 0);
            this.label21.TabIndex = 37;
            this.label21.Text = "*";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoEllipsis = true;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(273, 138);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 14);
            this.label15.TabIndex = 36;
            this.label15.Text = "*";
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
            this.pnlTopToolStrip.Size = new System.Drawing.Size(997, 54);
            this.pnlTopToolStrip.TabIndex = 3;
            this.pnlTopToolStrip.TabStop = true;
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
            this.ts_btnSaveClose,
            this.ts_btnClose});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(997, 53);
            this.TopToolStrip.TabIndex = 0;
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
            // ts_btnSaveClose
            // 
            this.ts_btnSaveClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSaveClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnSaveClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSaveClose.Image")));
            this.ts_btnSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSaveClose.Name = "ts_btnSaveClose";
            this.ts_btnSaveClose.Size = new System.Drawing.Size(66, 50);
            this.ts_btnSaveClose.Tag = "SaveFeeSchedule";
            this.ts_btnSaveClose.Text = "Sa&ve&&Cls";
            this.ts_btnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSaveClose.ToolTipText = "Save and Close";
            this.ts_btnSaveClose.Click += new System.EventHandler(this.ts_btnSaveClose_Click);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBilling});
            this.menuStrip1.Location = new System.Drawing.Point(0, 54);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(997, 24);
            this.menuStrip1.TabIndex = 218;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // mnuBilling
            // 
            this.mnuBilling.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBilling_AddLine,
            this.mnuBilling_RemoveLine});
            this.mnuBilling.Name = "mnuBilling";
            this.mnuBilling.Size = new System.Drawing.Size(22, 20);
            this.mnuBilling.Text = " ";
            // 
            // mnuBilling_AddLine
            // 
            this.mnuBilling_AddLine.Name = "mnuBilling_AddLine";
            this.mnuBilling_AddLine.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuBilling_AddLine.Size = new System.Drawing.Size(165, 22);
            this.mnuBilling_AddLine.Text = "Add Line";
            this.mnuBilling_AddLine.Click += new System.EventHandler(this.mnuBilling_AddLine_Click);
            // 
            // mnuBilling_RemoveLine
            // 
            this.mnuBilling_RemoveLine.Name = "mnuBilling_RemoveLine";
            this.mnuBilling_RemoveLine.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuBilling_RemoveLine.Size = new System.Drawing.Size(165, 22);
            this.mnuBilling_RemoveLine.Text = "Remove Line";
            this.mnuBilling_RemoveLine.Click += new System.EventHandler(this.mnuBilling_RemoveLine_Click);
            // 
            // frmCPTMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(997, 658);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlFeeSchedule);
            this.Controls.Add(this.pnlTopToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCPTMapping";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CPT Billing Crosswalk";
            this.Load += new System.EventHandler(this.frmCPTMapping_Load);
            this.pnlFeeSchedule.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CPTMapping)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Insurance)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnl_Shortkeys.ResumeLayout(false);
            this.pnl_Shortkeys.PerformLayout();
            this.pnlSpeciality.ResumeLayout(false);
            this.pnlSpeciality.PerformLayout();
            this.pnlTopToolStrip.ResumeLayout(false);
            this.pnlTopToolStrip.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtcptmapping;
        private System.Windows.Forms.Panel pnlTopToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnAddLine;
        private System.Windows.Forms.ToolStripButton ts_btnRemoveLine;
        private System.Windows.Forms.ToolStripButton ts_btnSaveClose;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.Panel pnl_Shortkeys;
        private System.Windows.Forms.Label lbl_Keyremoveline;
        private System.Windows.Forms.Label lbl_lshrtctKeyremoveline;
        private System.Windows.Forms.Label lbl_KeyAddline;
        private System.Windows.Forms.Label lbl_shrtctKeyAddline;
        private C1.Win.C1FlexGrid.C1FlexGrid c1CPTMapping;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Label lblCptMapping;
        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_AddLine;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_RemoveLine;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        internal System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Insurance;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}