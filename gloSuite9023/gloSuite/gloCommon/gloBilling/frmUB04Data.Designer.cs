namespace gloBilling
{
    partial class frmUB04Data
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
                components.Dispose();
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {
                    System.Windows.Forms.ContextMenuStrip[] cntmenuControls = { cmnu_DeleteUB04Data };
                    System.Windows.Forms.Control[] cntControls = { cmnu_DeleteUB04Data };
                    if (cntmenuControls != null)
                    {
                        if (cntmenuControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntmenuControls);

                        }
                    }
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }
                    //if (cmnu_DeleteUB04Data != null)
                    //{
                    //    gloGlobal.cEventHelper.RemoveAllEventHandlers(cmnu_DeleteUB04Data);
                    //    if (cmnu_DeleteUB04Data.Items != null)
                    //    {
                    //        cmnu_DeleteUB04Data.Items.Clear();

                    //    }
                    //    cmnu_DeleteUB04Data.Dispose();
                    //    cmnu_DeleteUB04Data = null;
                    //}
                }
                catch
                {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUB04Data));
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSaveClose = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlInternalOccurrenceCode = new System.Windows.Forms.Panel();
            this.c1OccrenceCode = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.c1ConditionCode = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlInternalSpanCodes = new System.Windows.Forms.Panel();
            this.c1OccurenceSpanCodeRange = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlInternalValueCode = new System.Windows.Forms.Panel();
            this.c1ValueCode = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label42 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label43 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.pnlSpeciality = new System.Windows.Forms.Panel();
            this.label47 = new System.Windows.Forms.Label();
            this.txtDischargeStatus = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.txtdischargeHour = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txtAdmissionType = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtAdmtHour = new System.Windows.Forms.TextBox();
            this.mskAdmitDate = new System.Windows.Forms.MaskedTextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.lblCptMapping = new System.Windows.Forms.Label();
            this.txttypeofbilling = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label41 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label37 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.mnuItem_Authorization = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_RemoveRef = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu_DeleteUB04Data = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.TopToolStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1OccrenceCode)).BeginInit();
            this.panel9.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ConditionCode)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1OccurenceSpanCodeRange)).BeginInit();
            this.panel10.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ValueCode)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel13.SuspendLayout();
            this.pnlSpeciality.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel14.SuspendLayout();
            this.cmnu_DeleteUB04Data.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopToolStrip.BackgroundImage")));
            this.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopToolStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.TopToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnSaveClose,
            this.ts_btnClose});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(1050, 53);
            this.TopToolStrip.TabIndex = 0;
            this.TopToolStrip.Text = "toolStrip1";
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
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click_2);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlInternalOccurrenceCode);
            this.panel1.Controls.Add(this.c1OccrenceCode);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel1.Size = new System.Drawing.Size(508, 205);
            this.panel1.TabIndex = 1;
            this.panel1.TabStop = true;
            // 
            // pnlInternalOccurrenceCode
            // 
            this.pnlInternalOccurrenceCode.AutoScroll = true;
            this.pnlInternalOccurrenceCode.AutoSize = true;
            this.pnlInternalOccurrenceCode.Location = new System.Drawing.Point(181, 83);
            this.pnlInternalOccurrenceCode.Name = "pnlInternalOccurrenceCode";
            this.pnlInternalOccurrenceCode.Size = new System.Drawing.Size(124, 106);
            this.pnlInternalOccurrenceCode.TabIndex = 34;
            this.pnlInternalOccurrenceCode.Visible = false;
            // 
            // c1OccrenceCode
            // 
            this.c1OccrenceCode.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1OccrenceCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1OccrenceCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1OccrenceCode.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1OccrenceCode.ColumnInfo = resources.GetString("c1OccrenceCode.ColumnInfo");
            this.c1OccrenceCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1OccrenceCode.ExtendLastCol = true;
            this.c1OccrenceCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1OccrenceCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1OccrenceCode.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1OccrenceCode.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1OccrenceCode.Location = new System.Drawing.Point(4, 29);
            this.c1OccrenceCode.Name = "c1OccrenceCode";
            this.c1OccrenceCode.Padding = new System.Windows.Forms.Padding(3);
            this.c1OccrenceCode.Rows.Count = 1;
            this.c1OccrenceCode.Rows.DefaultSize = 19;
            this.c1OccrenceCode.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1OccrenceCode.ShowCellLabels = true;
            this.c1OccrenceCode.Size = new System.Drawing.Size(500, 175);
            this.c1OccrenceCode.StyleInfo = resources.GetString("c1OccrenceCode.StyleInfo");
            this.c1OccrenceCode.TabIndex = 5;
            this.c1OccrenceCode.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this._c1flexGrid_BeforeSelChange);
            this.c1OccrenceCode.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1OccrenceCode_AfterSelChange);
            this.c1OccrenceCode.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this._c1flexGrid_StartEdit);
            this.c1OccrenceCode.LeaveEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this._c1flexGrid_LeaveEdit);
            this.c1OccrenceCode.ChangeEdit += new System.EventHandler(this._c1flexGrid_ChangeEdit);
            this.c1OccrenceCode.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1OccrenceCode_CellChanged);
            this.c1OccrenceCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this._c1flexGrid_KeyDown_1);
            this.c1OccrenceCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this._c1flexGrid_KeyUp_1);
            this.c1OccrenceCode.MouseDown += new System.Windows.Forms.MouseEventHandler(this._c1flexGrid_MouseDown);
            // 
            // panel9
            // 
            this.panel9.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel9.Controls.Add(this.label21);
            this.panel9.Controls.Add(this.label22);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(4, 4);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(500, 25);
            this.panel9.TabIndex = 10;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(500, 24);
            this.label21.TabIndex = 2;
            this.label21.Text = "   Occurrence Codes";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Location = new System.Drawing.Point(0, 24);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(500, 1);
            this.label22.TabIndex = 1;
            this.label22.Text = "label22";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(504, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 200);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 200);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(3, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(502, 1);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(502, 1);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlInternalControl);
            this.panel2.Controls.Add(this.c1ConditionCode);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(508, 277);
            this.panel2.TabIndex = 1;
            this.panel2.TabStop = true;
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(214, 78);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(124, 138);
            this.pnlInternalControl.TabIndex = 30;
            this.pnlInternalControl.Visible = false;
            // 
            // c1ConditionCode
            // 
            this.c1ConditionCode.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ConditionCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ConditionCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1ConditionCode.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ConditionCode.ColumnInfo = resources.GetString("c1ConditionCode.ColumnInfo");
            this.c1ConditionCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ConditionCode.ExtendLastCol = true;
            this.c1ConditionCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ConditionCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ConditionCode.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1ConditionCode.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1ConditionCode.Location = new System.Drawing.Point(4, 26);
            this.c1ConditionCode.Name = "c1ConditionCode";
            this.c1ConditionCode.Padding = new System.Windows.Forms.Padding(3);
            this.c1ConditionCode.Rows.Count = 1;
            this.c1ConditionCode.Rows.DefaultSize = 19;
            this.c1ConditionCode.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ConditionCode.ShowCellLabels = true;
            this.c1ConditionCode.Size = new System.Drawing.Size(500, 247);
            this.c1ConditionCode.StyleInfo = resources.GetString("c1ConditionCode.StyleInfo");
            this.c1ConditionCode.TabIndex = 3;
            this.c1ConditionCode.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this._c1flexGrid_AfterRowColChange);
            this.c1ConditionCode.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this._c1flexGrid_BeforeSelChange);
            this.c1ConditionCode.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this._c1flexGrid_StartEdit);
            this.c1ConditionCode.LeaveEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this._c1flexGrid_LeaveEdit);
            this.c1ConditionCode.ChangeEdit += new System.EventHandler(this._c1flexGrid_ChangeEdit);
            this.c1ConditionCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this._c1flexGrid_KeyDown_1);
            this.c1ConditionCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this._c1flexGrid_KeyUp_1);
            this.c1ConditionCode.MouseDown += new System.Windows.Forms.MouseEventHandler(this._c1flexGrid_MouseDown);
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label18);
            this.panel7.Controls.Add(this.label17);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(4, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(500, 25);
            this.panel7.TabIndex = 8;
            this.panel7.TabStop = true;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(508, 24);
            this.label18.TabIndex = 2;
            this.label18.Text = "   Condition Codes";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Location = new System.Drawing.Point(0, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(500, 1);
            this.label17.TabIndex = 1;
            this.label17.Text = "label17";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(504, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 272);
            this.label5.TabIndex = 3;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(3, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 272);
            this.label6.TabIndex = 2;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(3, 273);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(502, 1);
            this.label7.TabIndex = 1;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(502, 1);
            this.label8.TabIndex = 0;
            this.label8.Text = "label8";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlInternalSpanCodes);
            this.panel3.Controls.Add(this.c1OccurenceSpanCodeRange);
            this.panel3.Controls.Add(this.panel10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(513, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel3.Size = new System.Drawing.Size(537, 205);
            this.panel3.TabIndex = 2;
            this.panel3.TabStop = true;
            // 
            // pnlInternalSpanCodes
            // 
            this.pnlInternalSpanCodes.AutoScroll = true;
            this.pnlInternalSpanCodes.AutoSize = true;
            this.pnlInternalSpanCodes.Location = new System.Drawing.Point(188, 61);
            this.pnlInternalSpanCodes.Name = "pnlInternalSpanCodes";
            this.pnlInternalSpanCodes.Size = new System.Drawing.Size(124, 138);
            this.pnlInternalSpanCodes.TabIndex = 35;
            this.pnlInternalSpanCodes.Visible = false;
            // 
            // c1OccurenceSpanCodeRange
            // 
            this.c1OccurenceSpanCodeRange.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1OccurenceSpanCodeRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1OccurenceSpanCodeRange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1OccurenceSpanCodeRange.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1OccurenceSpanCodeRange.ColumnInfo = resources.GetString("c1OccurenceSpanCodeRange.ColumnInfo");
            this.c1OccurenceSpanCodeRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1OccurenceSpanCodeRange.ExtendLastCol = true;
            this.c1OccurenceSpanCodeRange.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1OccurenceSpanCodeRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1OccurenceSpanCodeRange.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1OccurenceSpanCodeRange.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1OccurenceSpanCodeRange.Location = new System.Drawing.Point(4, 29);
            this.c1OccurenceSpanCodeRange.Name = "c1OccurenceSpanCodeRange";
            this.c1OccurenceSpanCodeRange.Padding = new System.Windows.Forms.Padding(3);
            this.c1OccurenceSpanCodeRange.Rows.Count = 1;
            this.c1OccurenceSpanCodeRange.Rows.DefaultSize = 19;
            this.c1OccurenceSpanCodeRange.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1OccurenceSpanCodeRange.ShowCellLabels = true;
            this.c1OccurenceSpanCodeRange.Size = new System.Drawing.Size(529, 175);
            this.c1OccurenceSpanCodeRange.StyleInfo = resources.GetString("c1OccurenceSpanCodeRange.StyleInfo");
            this.c1OccurenceSpanCodeRange.TabIndex = 6;
            this.c1OccurenceSpanCodeRange.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this._c1flexGrid_AfterRowColChange);
            this.c1OccurenceSpanCodeRange.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this._c1flexGrid_BeforeSelChange);
            this.c1OccurenceSpanCodeRange.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this._c1flexGrid_StartEdit);
            this.c1OccurenceSpanCodeRange.LeaveEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this._c1flexGrid_LeaveEdit);
            this.c1OccurenceSpanCodeRange.ChangeEdit += new System.EventHandler(this._c1flexGrid_ChangeEdit);
            this.c1OccurenceSpanCodeRange.KeyDown += new System.Windows.Forms.KeyEventHandler(this._c1flexGrid_KeyDown_1);
            this.c1OccurenceSpanCodeRange.KeyUp += new System.Windows.Forms.KeyEventHandler(this._c1flexGrid_KeyUp_1);
            this.c1OccurenceSpanCodeRange.MouseDown += new System.Windows.Forms.MouseEventHandler(this._c1flexGrid_MouseDown);
            // 
            // panel10
            // 
            this.panel10.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel10.Controls.Add(this.label23);
            this.panel10.Controls.Add(this.label24);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(4, 4);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(529, 25);
            this.panel10.TabIndex = 9;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(529, 24);
            this.label23.TabIndex = 2;
            this.label23.Text = "   Occurrence Span Codes";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Location = new System.Drawing.Point(0, 24);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(529, 1);
            this.label24.TabIndex = 1;
            this.label24.Text = "label24";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(533, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 200);
            this.label9.TabIndex = 3;
            this.label9.Text = "label4";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(3, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 200);
            this.label10.TabIndex = 2;
            this.label10.Text = "label3";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(3, 204);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(531, 1);
            this.label11.TabIndex = 1;
            this.label11.Text = "label2";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(3, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(531, 1);
            this.label12.TabIndex = 0;
            this.label12.Text = "label1";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pnlInternalValueCode);
            this.panel4.Controls.Add(this.c1ValueCode);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(513, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel4.Size = new System.Drawing.Size(537, 277);
            this.panel4.TabIndex = 2;
            this.panel4.TabStop = true;
            // 
            // pnlInternalValueCode
            // 
            this.pnlInternalValueCode.AutoScroll = true;
            this.pnlInternalValueCode.AutoSize = true;
            this.pnlInternalValueCode.Location = new System.Drawing.Point(188, 60);
            this.pnlInternalValueCode.Name = "pnlInternalValueCode";
            this.pnlInternalValueCode.Size = new System.Drawing.Size(124, 138);
            this.pnlInternalValueCode.TabIndex = 33;
            this.pnlInternalValueCode.Visible = false;
            // 
            // c1ValueCode
            // 
            this.c1ValueCode.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ValueCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ValueCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1ValueCode.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ValueCode.ColumnInfo = resources.GetString("c1ValueCode.ColumnInfo");
            this.c1ValueCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ValueCode.ExtendLastCol = true;
            this.c1ValueCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ValueCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ValueCode.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1ValueCode.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1ValueCode.Location = new System.Drawing.Point(4, 26);
            this.c1ValueCode.Name = "c1ValueCode";
            this.c1ValueCode.Padding = new System.Windows.Forms.Padding(3);
            this.c1ValueCode.Rows.Count = 1;
            this.c1ValueCode.Rows.DefaultSize = 19;
            this.c1ValueCode.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ValueCode.ShowCellLabels = true;
            this.c1ValueCode.Size = new System.Drawing.Size(529, 247);
            this.c1ValueCode.StyleInfo = resources.GetString("c1ValueCode.StyleInfo");
            this.c1ValueCode.TabIndex = 4;
            this.c1ValueCode.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this._c1flexGrid_AfterRowColChange);
            this.c1ValueCode.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this._c1flexGrid_BeforeSelChange);
            this.c1ValueCode.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this._c1flexGrid_StartEdit);
            this.c1ValueCode.LeaveEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this._c1flexGrid_LeaveEdit);
            this.c1ValueCode.ChangeEdit += new System.EventHandler(this._c1flexGrid_ChangeEdit);
            this.c1ValueCode.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.c1ValueCode_KeyPressEdit);
            this.c1ValueCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this._c1flexGrid_KeyDown_1);
            this.c1ValueCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this._c1flexGrid_KeyUp_1);
            this.c1ValueCode.MouseDown += new System.Windows.Forms.MouseEventHandler(this._c1flexGrid_MouseDown);
            // 
            // panel8
            // 
            this.panel8.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label19);
            this.panel8.Controls.Add(this.label20);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(4, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(529, 25);
            this.panel8.TabIndex = 11;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(529, 24);
            this.label19.TabIndex = 2;
            this.label19.Text = "   Value Codes";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Location = new System.Drawing.Point(0, 24);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(529, 1);
            this.label20.TabIndex = 1;
            this.label20.Text = "label20";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(533, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 272);
            this.label13.TabIndex = 3;
            this.label13.Text = "label4";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(3, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 272);
            this.label14.TabIndex = 2;
            this.label14.Text = "label3";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Location = new System.Drawing.Point(3, 273);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(531, 1);
            this.label15.TabIndex = 1;
            this.label15.Text = "label2";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(3, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(531, 1);
            this.label16.TabIndex = 0;
            this.label16.Text = "label1";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Controls.Add(this.panel12);
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 132);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel5.Size = new System.Drawing.Size(1050, 280);
            this.panel5.TabIndex = 2;
            this.panel5.TabStop = true;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(230)))));
            this.panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel12.Controls.Add(this.label42);
            this.panel12.Controls.Add(this.label35);
            this.panel12.Controls.Add(this.label36);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel12.Location = new System.Drawing.Point(508, 3);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(5, 277);
            this.panel12.TabIndex = 5;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(1, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(3, 1);
            this.label42.TabIndex = 5;
            this.label42.Text = "label42";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Right;
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Location = new System.Drawing.Point(4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 277);
            this.label35.TabIndex = 4;
            this.label35.Text = "label35";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 277);
            this.label36.TabIndex = 3;
            this.label36.Text = "label36";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel3);
            this.panel6.Controls.Add(this.panel13);
            this.panel6.Controls.Add(this.panel1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 417);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel6.Size = new System.Drawing.Size(1050, 208);
            this.panel6.TabIndex = 3;
            this.panel6.TabStop = true;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(230)))));
            this.panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel13.Controls.Add(this.label43);
            this.panel13.Controls.Add(this.label38);
            this.panel13.Controls.Add(this.label40);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel13.Location = new System.Drawing.Point(508, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(5, 205);
            this.panel13.TabIndex = 16;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label43.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Location = new System.Drawing.Point(1, 204);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(3, 1);
            this.label43.TabIndex = 5;
            this.label43.Text = "label43";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Right;
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Location = new System.Drawing.Point(4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 205);
            this.label38.TabIndex = 4;
            this.label38.Text = "label38";
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Left;
            this.label40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Location = new System.Drawing.Point(0, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1, 205);
            this.label40.TabIndex = 3;
            this.label40.Text = "label40";
            // 
            // pnlSpeciality
            // 
            this.pnlSpeciality.Controls.Add(this.label47);
            this.pnlSpeciality.Controls.Add(this.txtDischargeStatus);
            this.pnlSpeciality.Controls.Add(this.label46);
            this.pnlSpeciality.Controls.Add(this.txtdischargeHour);
            this.pnlSpeciality.Controls.Add(this.label45);
            this.pnlSpeciality.Controls.Add(this.txtAdmissionType);
            this.pnlSpeciality.Controls.Add(this.label44);
            this.pnlSpeciality.Controls.Add(this.txtAdmtHour);
            this.pnlSpeciality.Controls.Add(this.mskAdmitDate);
            this.pnlSpeciality.Controls.Add(this.label39);
            this.pnlSpeciality.Controls.Add(this.lblCptMapping);
            this.pnlSpeciality.Controls.Add(this.txttypeofbilling);
            this.pnlSpeciality.Controls.Add(this.label25);
            this.pnlSpeciality.Controls.Add(this.label26);
            this.pnlSpeciality.Controls.Add(this.label27);
            this.pnlSpeciality.Controls.Add(this.label28);
            this.pnlSpeciality.Controls.Add(this.label29);
            this.pnlSpeciality.Controls.Add(this.label30);
            this.pnlSpeciality.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpeciality.Location = new System.Drawing.Point(0, 53);
            this.pnlSpeciality.Name = "pnlSpeciality";
            this.pnlSpeciality.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlSpeciality.Size = new System.Drawing.Size(1050, 79);
            this.pnlSpeciality.TabIndex = 1;
            this.pnlSpeciality.TabStop = true;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(540, 50);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(106, 14);
            this.label47.TabIndex = 47;
            this.label47.Text = "Discharge Status :";
            // 
            // txtDischargeStatus
            // 
            this.txtDischargeStatus.ForeColor = System.Drawing.Color.Black;
            this.txtDischargeStatus.Location = new System.Drawing.Point(650, 45);
            this.txtDischargeStatus.MaxLength = 2;
            this.txtDischargeStatus.Name = "txtDischargeStatus";
            this.txtDischargeStatus.Size = new System.Drawing.Size(112, 22);
            this.txtDischargeStatus.TabIndex = 6;
            this.txtDischargeStatus.TextChanged += new System.EventHandler(this.txtDischargeStatus_TextChanged);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(302, 49);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(96, 14);
            this.label46.TabIndex = 45;
            this.label46.Text = "Discharge hour :";
            // 
            // txtdischargeHour
            // 
            this.txtdischargeHour.ForeColor = System.Drawing.Color.Black;
            this.txtdischargeHour.Location = new System.Drawing.Point(400, 45);
            this.txtdischargeHour.MaxLength = 2;
            this.txtdischargeHour.Name = "txtdischargeHour";
            this.txtdischargeHour.ShortcutsEnabled = false;
            this.txtdischargeHour.Size = new System.Drawing.Size(112, 22);
            this.txtdischargeHour.TabIndex = 5;
            this.txtdischargeHour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdischargeHour_KeyPress);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(12, 50);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(100, 14);
            this.label45.TabIndex = 43;
            this.label45.Text = "Admission Type :";
            // 
            // txtAdmissionType
            // 
            this.txtAdmissionType.ForeColor = System.Drawing.Color.Black;
            this.txtAdmissionType.Location = new System.Drawing.Point(116, 45);
            this.txtAdmissionType.MaxLength = 1;
            this.txtAdmissionType.Name = "txtAdmissionType";
            this.txtAdmissionType.Size = new System.Drawing.Size(112, 22);
            this.txtAdmissionType.TabIndex = 4;
            this.txtAdmissionType.TextChanged += new System.EventHandler(this.txtAdmissionType_TextChanged);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(569, 15);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(77, 14);
            this.label44.TabIndex = 41;
            this.label44.Text = "Admit Hour :";
            // 
            // txtAdmtHour
            // 
            this.txtAdmtHour.ForeColor = System.Drawing.Color.Black;
            this.txtAdmtHour.Location = new System.Drawing.Point(650, 11);
            this.txtAdmtHour.MaxLength = 2;
            this.txtAdmtHour.Name = "txtAdmtHour";
            this.txtAdmtHour.ShortcutsEnabled = false;
            this.txtAdmtHour.Size = new System.Drawing.Size(112, 22);
            this.txtAdmtHour.TabIndex = 3;
            this.txtAdmtHour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdmtHour_KeyPress);
            // 
            // mskAdmitDate
            // 
            this.mskAdmitDate.Location = new System.Drawing.Point(400, 11);
            this.mskAdmitDate.Mask = "00/00/0000";
            this.mskAdmitDate.Name = "mskAdmitDate";
            this.mskAdmitDate.Size = new System.Drawing.Size(112, 22);
            this.mskAdmitDate.TabIndex = 2;
            this.mskAdmitDate.ValidatingType = typeof(System.DateTime);
            this.mskAdmitDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskAdmitDate_MouseClick);
            this.mskAdmitDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskAdmitDate_Validating);
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.AutoEllipsis = true;
            this.label39.AutoSize = true;
            this.label39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(321, 15);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(77, 14);
            this.label39.TabIndex = 39;
            this.label39.Text = "Admit Date :";
            // 
            // lblCptMapping
            // 
            this.lblCptMapping.AutoSize = true;
            this.lblCptMapping.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCptMapping.Location = new System.Drawing.Point(37, 16);
            this.lblCptMapping.Name = "lblCptMapping";
            this.lblCptMapping.Size = new System.Drawing.Size(75, 14);
            this.lblCptMapping.TabIndex = 20;
            this.lblCptMapping.Text = "Type of Bill :";
            // 
            // txttypeofbilling
            // 
            this.txttypeofbilling.ForeColor = System.Drawing.Color.Black;
            this.txttypeofbilling.Location = new System.Drawing.Point(116, 12);
            this.txttypeofbilling.MaxLength = 4;
            this.txttypeofbilling.Name = "txttypeofbilling";
            this.txttypeofbilling.Size = new System.Drawing.Size(112, 22);
            this.txttypeofbilling.TabIndex = 1;
            this.txttypeofbilling.TextChanged += new System.EventHandler(this.txttypeofbilling_TextChanged);
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.Location = new System.Drawing.Point(1046, 4);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 74);
            this.label25.TabIndex = 18;
            this.label25.Text = "label25";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label26.Location = new System.Drawing.Point(4, 78);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1043, 1);
            this.label26.TabIndex = 17;
            this.label26.Text = "label26";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.label27.Location = new System.Drawing.Point(4, 3);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1043, 1);
            this.label27.TabIndex = 16;
            this.label27.Text = "label27";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Location = new System.Drawing.Point(3, 3);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 76);
            this.label28.TabIndex = 15;
            this.label28.Text = "label28";
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoEllipsis = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(71, 148);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(789, 55);
            this.label29.TabIndex = 37;
            this.label29.Text = "*";
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.AutoEllipsis = true;
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(318, 149);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(14, 14);
            this.label30.TabIndex = 36;
            this.label30.Text = "*";
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Transparent;
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.Controls.Add(this.panel15);
            this.panel11.Controls.Add(this.panel14);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 412);
            this.panel11.Name = "panel11";
            this.panel11.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel11.Size = new System.Drawing.Size(1050, 5);
            this.panel11.TabIndex = 14;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(230)))));
            this.panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel15.Controls.Add(this.label41);
            this.panel15.Controls.Add(this.label34);
            this.panel15.Controls.Add(this.label33);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel15.Location = new System.Drawing.Point(496, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(551, 5);
            this.panel15.TabIndex = 16;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Right;
            this.label41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Location = new System.Drawing.Point(550, 1);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1, 3);
            this.label41.TabIndex = 5;
            this.label41.Text = "label41";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Top;
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Location = new System.Drawing.Point(0, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(551, 1);
            this.label34.TabIndex = 3;
            this.label34.Text = "label34";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Location = new System.Drawing.Point(0, 4);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(551, 1);
            this.label33.TabIndex = 2;
            this.label33.Text = "label33";
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(230)))));
            this.panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel14.Controls.Add(this.label37);
            this.panel14.Controls.Add(this.label32);
            this.panel14.Controls.Add(this.label31);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel14.Location = new System.Drawing.Point(3, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(505, 5);
            this.panel14.TabIndex = 15;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Left;
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Location = new System.Drawing.Point(0, 1);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 3);
            this.label37.TabIndex = 4;
            this.label37.Text = "label37";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Location = new System.Drawing.Point(0, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(505, 1);
            this.label32.TabIndex = 3;
            this.label32.Text = "label32";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(0, 4);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(505, 1);
            this.label31.TabIndex = 2;
            this.label31.Text = "label31";
            // 
            // mnuItem_Authorization
            // 
            this.mnuItem_Authorization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_Authorization.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_Authorization.Image")));
            this.mnuItem_Authorization.Name = "mnuItem_Authorization";
            this.mnuItem_Authorization.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_Authorization.Text = "Prior Authorization";
            // 
            // mnuItem_RemoveRef
            // 
            this.mnuItem_RemoveRef.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_RemoveRef.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_RemoveRef.Image")));
            this.mnuItem_RemoveRef.Name = "mnuItem_RemoveRef";
            this.mnuItem_RemoveRef.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_RemoveRef.Text = "Remove";
            // 
            // cmnu_DeleteUB04Data
            // 
            this.cmnu_DeleteUB04Data.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmnu_DeleteUB04Data.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.cmnu_DeleteUB04Data.Name = "cmnu_Appointment";
            this.cmnu_DeleteUB04Data.Size = new System.Drawing.Size(106, 26);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItem2.Text = "Delete";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // frmUB04Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1050, 625);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlSpeciality);
            this.Controls.Add(this.TopToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUB04Data";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UB/Institutional Data";
            this.Load += new System.EventHandler(this.frmUB04Data_Load);
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1OccrenceCode)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ConditionCode)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1OccurenceSpanCodeRange)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ValueCode)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.pnlSpeciality.ResumeLayout(false);
            this.pnlSpeciality.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.cmnu_DeleteUB04Data.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnSaveClose;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel pnlSpeciality;
        internal System.Windows.Forms.Label lblCptMapping;
        private System.Windows.Forms.TextBox txttypeofbilling;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Panel pnlInternalControl;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ConditionCode=new C1.Win.C1FlexGrid.C1FlexGrid() ;
        private System.Windows.Forms.Panel pnlInternalValueCode;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ValueCode;
        private System.Windows.Forms.Panel pnlInternalOccurrenceCode;
        private C1.Win.C1FlexGrid.C1FlexGrid c1OccrenceCode;
        private System.Windows.Forms.Panel pnlInternalSpanCodes;
        private C1.Win.C1FlexGrid.C1FlexGrid c1OccurenceSpanCodeRange;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_Authorization;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_RemoveRef;
        private System.Windows.Forms.ContextMenuStrip cmnu_DeleteUB04Data;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.MaskedTextBox mskAdmitDate;
        private System.Windows.Forms.Label label39;
        internal System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtAdmissionType;
        internal System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtAdmtHour;
        internal System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox txtdischargeHour;
        internal System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txtDischargeStatus;
    }
}

