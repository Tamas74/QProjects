namespace gloAppointmentScheduling
{
    partial class frmSetupUsedAppSch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupUsedAppSch));
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.rbDontDelete = new System.Windows.Forms.RadioButton();
            this.rbDelete = new System.Windows.Forms.RadioButton();
            this.lbl_pnl_BaseBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_BaseTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_BaseRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_BaseLeftBrd = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlCriteria_ProviderProblemType = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd = new System.Windows.Forms.Label();
            this.c1UsedList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCriteria_ProviderProblemType_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblCriteria_ProviderProblemType_Header = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnl_Base.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlCriteria_ProviderProblemType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1UsedList)).BeginInit();
            this.pnlCriteria_ProviderProblemType_Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Base
            // 
            this.pnl_Base.Controls.Add(this.rbDontDelete);
            this.pnl_Base.Controls.Add(this.rbDelete);
            this.pnl_Base.Controls.Add(this.lbl_pnl_BaseBottomBrd);
            this.pnl_Base.Controls.Add(this.lbl_pnl_BaseTopBrd);
            this.pnl_Base.Controls.Add(this.lbl_pnl_BaseRightBrd);
            this.pnl_Base.Controls.Add(this.lbl_pnl_BaseLeftBrd);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Base.Location = new System.Drawing.Point(0, 263);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Padding = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.pnl_Base.Size = new System.Drawing.Size(558, 95);
            this.pnl_Base.TabIndex = 0;
            // 
            // rbDontDelete
            // 
            this.rbDontDelete.AutoSize = true;
            this.rbDontDelete.Checked = true;
            this.rbDontDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDontDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.rbDontDelete.Location = new System.Drawing.Point(22, 52);
            this.rbDontDelete.Name = "rbDontDelete";
            this.rbDontDelete.Size = new System.Drawing.Size(343, 18);
            this.rbDontDelete.TabIndex = 5;
            this.rbDontDelete.TabStop = true;
            this.rbDontDelete.Text = "Don\'t Delete Occurence And Create New Recurrence";
            this.rbDontDelete.UseVisualStyleBackColor = true;
            this.rbDontDelete.CheckedChanged += new System.EventHandler(this.rbDontDelete_CheckedChanged);
            // 
            // rbDelete
            // 
            this.rbDelete.AutoSize = true;
            this.rbDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDelete.ForeColor = System.Drawing.Color.Maroon;
            this.rbDelete.Location = new System.Drawing.Point(22, 18);
            this.rbDelete.Name = "rbDelete";
            this.rbDelete.Size = new System.Drawing.Size(285, 18);
            this.rbDelete.TabIndex = 4;
            this.rbDelete.TabStop = true;
            this.rbDelete.Text = "Delete Occurence And Create New Recurrence";
            this.rbDelete.UseVisualStyleBackColor = true;
            this.rbDelete.CheckedChanged += new System.EventHandler(this.rbDelete_CheckedChanged);
            // 
            // lbl_pnl_BaseBottomBrd
            // 
            this.lbl_pnl_BaseBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_BaseBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnl_BaseBottomBrd.Location = new System.Drawing.Point(4, 91);
            this.lbl_pnl_BaseBottomBrd.Name = "lbl_pnl_BaseBottomBrd";
            this.lbl_pnl_BaseBottomBrd.Size = new System.Drawing.Size(550, 1);
            this.lbl_pnl_BaseBottomBrd.TabIndex = 3;
            // 
            // lbl_pnl_BaseTopBrd
            // 
            this.lbl_pnl_BaseTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_BaseTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnl_BaseTopBrd.Location = new System.Drawing.Point(4, 2);
            this.lbl_pnl_BaseTopBrd.Name = "lbl_pnl_BaseTopBrd";
            this.lbl_pnl_BaseTopBrd.Size = new System.Drawing.Size(550, 1);
            this.lbl_pnl_BaseTopBrd.TabIndex = 2;
            // 
            // lbl_pnl_BaseRightBrd
            // 
            this.lbl_pnl_BaseRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_BaseRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnl_BaseRightBrd.Location = new System.Drawing.Point(554, 2);
            this.lbl_pnl_BaseRightBrd.Name = "lbl_pnl_BaseRightBrd";
            this.lbl_pnl_BaseRightBrd.Size = new System.Drawing.Size(1, 90);
            this.lbl_pnl_BaseRightBrd.TabIndex = 1;
            // 
            // lbl_pnl_BaseLeftBrd
            // 
            this.lbl_pnl_BaseLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_BaseLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnl_BaseLeftBrd.Location = new System.Drawing.Point(3, 2);
            this.lbl_pnl_BaseLeftBrd.Name = "lbl_pnl_BaseLeftBrd";
            this.lbl_pnl_BaseLeftBrd.Size = new System.Drawing.Size(1, 90);
            this.lbl_pnl_BaseLeftBrd.TabIndex = 0;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(558, 54);
            this.pnlToolStrip.TabIndex = 16;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(558, 53);
            this.ts_Commands.TabIndex = 11;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "&Save&&Cls";
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
            this.tsb_Cancel.ToolTipText = "Close";
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // pnlCriteria_ProviderProblemType
            // 
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.c1UsedList);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.pnlCriteria_ProviderProblemType_Header);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeRightBrd);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeTopBrd);
            this.pnlCriteria_ProviderProblemType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_ProviderProblemType.Location = new System.Drawing.Point(0, 54);
            this.pnlCriteria_ProviderProblemType.Name = "pnlCriteria_ProviderProblemType";
            this.pnlCriteria_ProviderProblemType.Padding = new System.Windows.Forms.Padding(3);
            this.pnlCriteria_ProviderProblemType.Size = new System.Drawing.Size(558, 210);
            this.pnlCriteria_ProviderProblemType.TabIndex = 170;
            // 
            // lbl_pnlCriteria_ProviderProblemTypeBottomBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Location = new System.Drawing.Point(4, 206);
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeBottomBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Size = new System.Drawing.Size(550, 1);
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.TabIndex = 143;
            // 
            // c1UsedList
            // 
            this.c1UsedList.AllowEditing = false;
            this.c1UsedList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1UsedList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1UsedList.ColumnInfo = "3,1,0,0,0,95,Columns:";
            this.c1UsedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1UsedList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1UsedList.Location = new System.Drawing.Point(4, 27);
            this.c1UsedList.Name = "c1UsedList";
            this.c1UsedList.Rows.Count = 5;
            this.c1UsedList.Rows.DefaultSize = 19;
            this.c1UsedList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1UsedList.Size = new System.Drawing.Size(550, 180);
            this.c1UsedList.StyleInfo = resources.GetString("c1UsedList.StyleInfo");
            this.c1UsedList.TabIndex = 138;
            this.c1UsedList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1UsedList_MouseMove);
            // 
            // pnlCriteria_ProviderProblemType_Header
            // 
            this.pnlCriteria_ProviderProblemType_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlCriteria_ProviderProblemType_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCriteria_ProviderProblemType_Header.Controls.Add(this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd);
            this.pnlCriteria_ProviderProblemType_Header.Controls.Add(this.lblCriteria_ProviderProblemType_Header);
            this.pnlCriteria_ProviderProblemType_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_ProviderProblemType_Header.Location = new System.Drawing.Point(4, 4);
            this.pnlCriteria_ProviderProblemType_Header.Name = "pnlCriteria_ProviderProblemType_Header";
            this.pnlCriteria_ProviderProblemType_Header.Size = new System.Drawing.Size(550, 23);
            this.pnlCriteria_ProviderProblemType_Header.TabIndex = 137;
            // 
            // lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.Name = "lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd";
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.Size = new System.Drawing.Size(550, 1);
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd.TabIndex = 141;
            // 
            // lblCriteria_ProviderProblemType_Header
            // 
            this.lblCriteria_ProviderProblemType_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblCriteria_ProviderProblemType_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCriteria_ProviderProblemType_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteria_ProviderProblemType_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCriteria_ProviderProblemType_Header.Location = new System.Drawing.Point(0, 0);
            this.lblCriteria_ProviderProblemType_Header.Name = "lblCriteria_ProviderProblemType_Header";
            this.lblCriteria_ProviderProblemType_Header.Size = new System.Drawing.Size(550, 23);
            this.lblCriteria_ProviderProblemType_Header.TabIndex = 0;
            this.lblCriteria_ProviderProblemType_Header.Text = " Used Appointments";
            this.lblCriteria_ProviderProblemType_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlCriteria_ProviderProblemTypeLeftBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeLeftBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.Size = new System.Drawing.Size(1, 203);
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.TabIndex = 139;
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.Text = "label10";
            // 
            // lbl_pnlCriteria_ProviderProblemTypeRightBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Location = new System.Drawing.Point(554, 4);
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeRightBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Size = new System.Drawing.Size(1, 203);
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.TabIndex = 140;
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Text = "label11";
            // 
            // lbl_pnlCriteria_ProviderProblemTypeTopBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeTopBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Size = new System.Drawing.Size(552, 1);
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.TabIndex = 142;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmSetupUsedAppSch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(558, 358);
            this.Controls.Add(this.pnlCriteria_ProviderProblemType);
            this.Controls.Add(this.pnl_Base);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupUsedAppSch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Used Appointment";
            this.Load += new System.EventHandler(this.frmSetupUsedAppSch_Load);
            this.pnl_Base.ResumeLayout(false);
            this.pnl_Base.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlCriteria_ProviderProblemType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1UsedList)).EndInit();
            this.pnlCriteria_ProviderProblemType_Header.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Base;
        private System.Windows.Forms.Label lbl_pnl_BaseLeftBrd;
        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Label lbl_pnl_BaseBottomBrd;
        private System.Windows.Forms.Label lbl_pnl_BaseTopBrd;
        private System.Windows.Forms.Label lbl_pnl_BaseRightBrd;
        private System.Windows.Forms.Panel pnlCriteria_ProviderProblemType;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemType_HeaderBottomBrd;
        private C1.Win.C1FlexGrid.C1FlexGrid c1UsedList;
        private System.Windows.Forms.Panel pnlCriteria_ProviderProblemType_Header;
        private System.Windows.Forms.Label lblCriteria_ProviderProblemType_Header;
        private System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeLeftBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeRightBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeTopBrd;
        private System.Windows.Forms.RadioButton rbDontDelete;
        private System.Windows.Forms.RadioButton rbDelete;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}