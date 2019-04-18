namespace gloAccountsV2
{
    partial class frmLoadPendingChecksV2
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtpCloseDate };
            System.Windows.Forms.Control[] cntControls = { dtpCloseDate };

            if (disposing && (components != null))
            {
               
                try
                {
                    components.Dispose();

                    if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }

                    System.Windows.Forms.ContextMenuStrip[] cntMenuControls = { contextMenuPendingCheck };
                    System.Windows.Forms.Control[] cntmnuControls = { contextMenuPendingCheck };
                    if (cntMenuControls != null)
                    {
                        if (cntMenuControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntMenuControls);

                        }
                    }
                    if (cntmnuControls != null)
                    {
                        if (cntmnuControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntmnuControls);

                        }
                    }
                    //if (dtpCloseDate != null)
                    //{
                    //    try
                    //    {
                    //        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpCloseDate);

                    //    }
                    //    catch
                    //    {
                    //    }


                    //    dtpCloseDate.Dispose();
                    //    dtpCloseDate = null;
                    //}
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

                //try
                //{
                //    //if (contextMenuPendingCheck != null)
                //    //{
                //    //    gloGlobal.cEventHelper.RemoveAllEventHandlers(contextMenuPendingCheck);
                //    //    if (contextMenuPendingCheck.Items != null)
                //    //    {
                //    //        contextMenuPendingCheck.Items.Clear();

                //    //    }
                //    //    contextMenuPendingCheck.Dispose();
                //    //    contextMenuPendingCheck = null;
                //    //}
                //}
                //catch
                //{
                //}

               
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadPendingChecksV2));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Generate = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowHideZeroCheck = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlC1Grid = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.c1PendingCheck = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1SinglePaymentTotal = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearInsurance = new System.Windows.Forms.Button();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.dtpCloseDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearchInsuranceCompany = new System.Windows.Forms.Button();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.lblInsCompany = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCloseDate = new System.Windows.Forms.Label();
            this.lblPayType = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuPendingCheck = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmt_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.cmt_Seperator = new System.Windows.Forms.ToolStripSeparator();
            this.cmt_ShowHidden = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlPleasewait = new System.Windows.Forms.Panel();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlC1Grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PendingCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePaymentTotal)).BeginInit();
            this.panel1.SuspendLayout();
            this.contextMenuPendingCheck.SuspendLayout();
            this.pnlPleasewait.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(778, 54);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Generate,
            this.tsb_ShowHideZeroCheck,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(778, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_Generate
            // 
            this.tsb_Generate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Generate.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Generate.Image")));
            this.tsb_Generate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Generate.Name = "tsb_Generate";
            this.tsb_Generate.Size = new System.Drawing.Size(66, 50);
            this.tsb_Generate.Tag = "Cancel";
            this.tsb_Generate.Text = "&Generate";
            this.tsb_Generate.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Generate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Generate.ToolTipText = "Generate Pending Check";
            this.tsb_Generate.Click += new System.EventHandler(this.tsb_Generate_Click);
            // 
            // tsb_ShowHideZeroCheck
            // 
            this.tsb_ShowHideZeroCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowHideZeroCheck.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowHideZeroCheck.Image")));
            this.tsb_ShowHideZeroCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowHideZeroCheck.Name = "tsb_ShowHideZeroCheck";
            this.tsb_ShowHideZeroCheck.Size = new System.Drawing.Size(116, 50);
            this.tsb_ShowHideZeroCheck.Tag = "Show";
            this.tsb_ShowHideZeroCheck.Text = "&Show Completed";
            this.tsb_ShowHideZeroCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowHideZeroCheck.ToolTipText = "Show Completed Check";
            this.tsb_ShowHideZeroCheck.Click += new System.EventHandler(this.tsb_ShowHideZeroCheck_Click);
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "Sa&ve&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            this.tsb_OK.Click += new System.EventHandler(this.tsb_OK_Click);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // pnlC1Grid
            // 
            this.pnlC1Grid.BackColor = System.Drawing.Color.Transparent;
            this.pnlC1Grid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlC1Grid.Controls.Add(this.label11);
            this.pnlC1Grid.Controls.Add(this.c1PendingCheck);
            this.pnlC1Grid.Controls.Add(this.c1SinglePaymentTotal);
            this.pnlC1Grid.Controls.Add(this.label4);
            this.pnlC1Grid.Controls.Add(this.label3);
            this.pnlC1Grid.Controls.Add(this.label2);
            this.pnlC1Grid.Controls.Add(this.label1);
            this.pnlC1Grid.Controls.Add(this.label12);
            this.pnlC1Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlC1Grid.Location = new System.Drawing.Point(0, 126);
            this.pnlC1Grid.Name = "pnlC1Grid";
            this.pnlC1Grid.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlC1Grid.Size = new System.Drawing.Size(778, 294);
            this.pnlC1Grid.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(4, 270);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(770, 1);
            this.label11.TabIndex = 16;
            // 
            // c1PendingCheck
            // 
            this.c1PendingCheck.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PendingCheck.AllowEditing = false;
            this.c1PendingCheck.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1PendingCheck.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1PendingCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PendingCheck.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PendingCheck.ColumnInfo = "1,0,0,0,0,105,Columns:";
            this.c1PendingCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PendingCheck.ExtendLastCol = true;
            this.c1PendingCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PendingCheck.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PendingCheck.Location = new System.Drawing.Point(4, 3);
            this.c1PendingCheck.Name = "c1PendingCheck";
            this.c1PendingCheck.Rows.Count = 1;
            this.c1PendingCheck.Rows.DefaultSize = 21;
            this.c1PendingCheck.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PendingCheck.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1PendingCheck.Size = new System.Drawing.Size(770, 268);
            this.c1PendingCheck.StyleInfo = resources.GetString("c1PendingCheck.StyleInfo");
            this.c1PendingCheck.TabIndex = 5;
            this.c1PendingCheck.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1PendingCheck_MouseDoubleClick);
            this.c1PendingCheck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1PendingCheck_MouseDown);
            // 
            // c1SinglePaymentTotal
            // 
            this.c1SinglePaymentTotal.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1SinglePaymentTotal.AllowEditing = false;
            this.c1SinglePaymentTotal.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1SinglePaymentTotal.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1SinglePaymentTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1SinglePaymentTotal.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SinglePaymentTotal.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1SinglePaymentTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c1SinglePaymentTotal.ExtendLastCol = true;
            this.c1SinglePaymentTotal.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1SinglePaymentTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1SinglePaymentTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SinglePaymentTotal.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1SinglePaymentTotal.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1SinglePaymentTotal.Location = new System.Drawing.Point(4, 271);
            this.c1SinglePaymentTotal.Name = "c1SinglePaymentTotal";
            this.c1SinglePaymentTotal.Rows.Count = 1;
            this.c1SinglePaymentTotal.Rows.DefaultSize = 19;
            this.c1SinglePaymentTotal.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SinglePaymentTotal.ShowCellLabels = true;
            this.c1SinglePaymentTotal.Size = new System.Drawing.Size(770, 19);
            this.c1SinglePaymentTotal.StyleInfo = resources.GetString("c1SinglePaymentTotal.StyleInfo");
            this.c1SinglePaymentTotal.TabIndex = 15;
            this.c1SinglePaymentTotal.TabStop = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 287);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(774, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 287);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 290);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(772, 1);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(772, 1);
            this.label1.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(772, 2);
            this.label12.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnClearInsurance);
            this.panel1.Controls.Add(this.mskCloseDate);
            this.panel1.Controls.Add(this.dtpCloseDate);
            this.panel1.Controls.Add(this.btnSearchInsuranceCompany);
            this.panel1.Controls.Add(this.cmbUsers);
            this.panel1.Controls.Add(this.lblInsCompany);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lblCloseDate);
            this.panel1.Controls.Add(this.lblPayType);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel1.Size = new System.Drawing.Size(778, 72);
            this.panel1.TabIndex = 2;
            // 
            // btnClearInsurance
            // 
            this.btnClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.BackgroundImage")));
            this.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.Image")));
            this.btnClearInsurance.Location = new System.Drawing.Point(548, 12);
            this.btnClearInsurance.Name = "btnClearInsurance";
            this.btnClearInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnClearInsurance.TabIndex = 225;
            this.btnClearInsurance.TabStop = false;
            this.toolTip1.SetToolTip(this.btnClearInsurance, "Clear Insurance Company");
            this.btnClearInsurance.UseVisualStyleBackColor = false;
            this.btnClearInsurance.Click += new System.EventHandler(this.btnClearInsurance_Click);
            // 
            // mskCloseDate
            // 
            this.mskCloseDate.Location = new System.Drawing.Point(465, 39);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(79, 22);
            this.mskCloseDate.TabIndex = 4;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            this.mskCloseDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DateMouseClick);
            this.mskCloseDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskCloseDate_Validating);
            // 
            // dtpCloseDate
            // 
            this.dtpCloseDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpCloseDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpCloseDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpCloseDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpCloseDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpCloseDate.CustomFormat = "MM/dd/yyyy";
            this.dtpCloseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCloseDate.Location = new System.Drawing.Point(426, 12);
            this.dtpCloseDate.Name = "dtpCloseDate";
            this.dtpCloseDate.Size = new System.Drawing.Size(96, 22);
            this.dtpCloseDate.TabIndex = 216;
            this.dtpCloseDate.TabStop = false;
            this.dtpCloseDate.Visible = false;
            // 
            // btnSearchInsuranceCompany
            // 
            this.btnSearchInsuranceCompany.AutoEllipsis = true;
            this.btnSearchInsuranceCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchInsuranceCompany.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchInsuranceCompany.BackgroundImage")));
            this.btnSearchInsuranceCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchInsuranceCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchInsuranceCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchInsuranceCompany.Image")));
            this.btnSearchInsuranceCompany.Location = new System.Drawing.Point(523, 12);
            this.btnSearchInsuranceCompany.Name = "btnSearchInsuranceCompany";
            this.btnSearchInsuranceCompany.Size = new System.Drawing.Size(21, 21);
            this.btnSearchInsuranceCompany.TabIndex = 1;
            this.btnSearchInsuranceCompany.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchInsuranceCompany, "Select Insurance Company");
            this.btnSearchInsuranceCompany.UseVisualStyleBackColor = false;
            this.btnSearchInsuranceCompany.Click += new System.EventHandler(this.btnSearchInsuranceCompany_Click);
            // 
            // cmbUsers
            // 
            this.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsers.ForeColor = System.Drawing.Color.Black;
            this.cmbUsers.FormattingEnabled = true;
            this.cmbUsers.Items.AddRange(new object[] {
            ""});
            this.cmbUsers.Location = new System.Drawing.Point(116, 39);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(256, 22);
            this.cmbUsers.TabIndex = 2;
            // 
            // lblInsCompany
            // 
            this.lblInsCompany.BackColor = System.Drawing.Color.Transparent;
            this.lblInsCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsCompany.Location = new System.Drawing.Point(118, 13);
            this.lblInsCompany.Name = "lblInsCompany";
            this.lblInsCompany.Size = new System.Drawing.Size(417, 19);
            this.lblInsCompany.TabIndex = 215;
            this.lblInsCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(26, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 14);
            this.label10.TabIndex = 211;
            this.label10.Text = "Ins. Company :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCloseDate
            // 
            this.lblCloseDate.AutoSize = true;
            this.lblCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCloseDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCloseDate.Location = new System.Drawing.Point(390, 43);
            this.lblCloseDate.Name = "lblCloseDate";
            this.lblCloseDate.Size = new System.Drawing.Size(73, 14);
            this.lblCloseDate.TabIndex = 212;
            this.lblCloseDate.Text = "Close Date :";
            this.lblCloseDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPayType
            // 
            this.lblPayType.AutoSize = true;
            this.lblPayType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayType.Location = new System.Drawing.Point(76, 43);
            this.lblPayType.Name = "lblPayType";
            this.lblPayType.Size = new System.Drawing.Size(39, 14);
            this.lblPayType.TabIndex = 212;
            this.lblPayType.Text = "User :";
            this.lblPayType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 68);
            this.label5.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(774, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 68);
            this.label6.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(772, 1);
            this.label7.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(772, 1);
            this.label8.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(772, 2);
            this.label9.TabIndex = 14;
            // 
            // contextMenuPendingCheck
            // 
            this.contextMenuPendingCheck.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmt_Remove,
            this.cmt_Seperator,
            this.cmt_ShowHidden});
            this.contextMenuPendingCheck.Name = "contextMenuChargeHistory";
            this.contextMenuPendingCheck.Size = new System.Drawing.Size(146, 54);
            // 
            // cmt_Remove
            // 
            this.cmt_Remove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmt_Remove.Image = ((System.Drawing.Image)(resources.GetObject("cmt_Remove.Image")));
            this.cmt_Remove.Name = "cmt_Remove";
            this.cmt_Remove.Size = new System.Drawing.Size(145, 22);
            this.cmt_Remove.Text = "Hide";
            this.cmt_Remove.Click += new System.EventHandler(this.cmt_Remove_Click);
            // 
            // cmt_Seperator
            // 
            this.cmt_Seperator.Name = "cmt_Seperator";
            this.cmt_Seperator.Size = new System.Drawing.Size(142, 6);
            // 
            // cmt_ShowHidden
            // 
            this.cmt_ShowHidden.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmt_ShowHidden.Image = ((System.Drawing.Image)(resources.GetObject("cmt_ShowHidden.Image")));
            this.cmt_ShowHidden.Name = "cmt_ShowHidden";
            this.cmt_ShowHidden.Size = new System.Drawing.Size(145, 22);
            this.cmt_ShowHidden.Text = "Show Hidden";
            this.cmt_ShowHidden.Click += new System.EventHandler(this.cmt_ShowHidden_Click);
            // 
            // pnlPleasewait
            // 
            this.pnlPleasewait.Controls.Add(this.label45);
            this.pnlPleasewait.Controls.Add(this.label44);
            this.pnlPleasewait.Controls.Add(this.label43);
            this.pnlPleasewait.Controls.Add(this.label42);
            this.pnlPleasewait.Controls.Add(this.label41);
            this.pnlPleasewait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPleasewait.Location = new System.Drawing.Point(0, 126);
            this.pnlPleasewait.Name = "pnlPleasewait";
            this.pnlPleasewait.Padding = new System.Windows.Forms.Padding(3);
            this.pnlPleasewait.Size = new System.Drawing.Size(778, 294);
            this.pnlPleasewait.TabIndex = 34;
            this.pnlPleasewait.Visible = false;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.White;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.Font = new System.Drawing.Font("Baskerville Old Face", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Location = new System.Drawing.Point(4, 4);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(770, 286);
            this.label45.TabIndex = 238;
            this.label45.Text = "Please wait...";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Left;
            this.label44.Location = new System.Drawing.Point(3, 4);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 286);
            this.label44.TabIndex = 36;
            this.label44.Text = "label44";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Right;
            this.label43.Location = new System.Drawing.Point(774, 4);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 286);
            this.label43.TabIndex = 35;
            this.label43.Text = "label43";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Location = new System.Drawing.Point(3, 3);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(772, 1);
            this.label42.TabIndex = 34;
            this.label42.Text = "label42";
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label41.Location = new System.Drawing.Point(3, 290);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(772, 1);
            this.label41.TabIndex = 33;
            this.label41.Text = "label41";
            // 
            // frmLoadPendingChecksV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(778, 420);
            this.Controls.Add(this.pnlPleasewait);
            this.Controls.Add(this.pnlC1Grid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoadPendingChecksV2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pending Check";
            this.Load += new System.EventHandler(this.frmBillingCheckDiff_Load);
            this.Shown += new System.EventHandler(this.frmLoadPendingChecksV2_Shown);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlC1Grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PendingCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePaymentTotal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuPendingCheck.ResumeLayout(false);
            this.pnlPleasewait.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlC1Grid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PendingCheck;
        internal System.Windows.Forms.ToolStripButton tsb_ShowHideZeroCheck;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblInsCompany;
        private System.Windows.Forms.Button btnSearchInsuranceCompany;
        private System.Windows.Forms.ComboBox cmbUsers;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPayType;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.DateTimePicker dtpCloseDate;
        private System.Windows.Forms.Label lblCloseDate;
        private System.Windows.Forms.MaskedTextBox mskCloseDate;
        private System.Windows.Forms.Button btnClearInsurance;
        internal System.Windows.Forms.ToolStripButton tsb_Generate;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SinglePaymentTotal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ContextMenuStrip contextMenuPendingCheck;
        private System.Windows.Forms.ToolStripMenuItem cmt_Remove;
        private System.Windows.Forms.ToolStripSeparator cmt_Seperator;
        private System.Windows.Forms.ToolStripMenuItem cmt_ShowHidden;
        private System.Windows.Forms.Panel pnlPleasewait;
        internal System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
    }
}