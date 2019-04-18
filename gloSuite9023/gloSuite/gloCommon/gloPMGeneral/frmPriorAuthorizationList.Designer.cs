namespace gloPMGeneral
{
    partial class frmPriorAuthorizationList
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
                try
                {
                    if (cmnu_PriorAuthorization != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(cmnu_PriorAuthorization);
                        if (cmnu_PriorAuthorization.Items != null)
                        {
                            cmnu_PriorAuthorization.Items.Clear();

                        }
                        cmnu_PriorAuthorization.Dispose();
                        cmnu_PriorAuthorization = null;
                    }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPriorAuthorizationList));
            this.pnl_tlspTOP = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsb_Clear = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnl_PriorAuthorizations = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.c1PriorAuthorization = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.cmnu_PriorAuthorization = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modiFyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnl_tlspTOP.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnl_PriorAuthorizations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PriorAuthorization)).BeginInit();
            this.cmnu_PriorAuthorization.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlspTOP
            // 
            this.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlspTOP.Controls.Add(this.ts_Commands);
            this.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlspTOP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlspTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlspTOP.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlspTOP.Name = "pnl_tlspTOP";
            this.pnl_tlspTOP.Size = new System.Drawing.Size(647, 54);
            this.pnl_tlspTOP.TabIndex = 1;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsb_Clear,
            this.tsb_Save,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(647, 53);
            this.ts_Commands.TabIndex = 8;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 50);
            this.toolStripButton1.Tag = "CheckIn";
            this.toolStripButton1.Text = "&CheckIn";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.ToolTipText = "Patient CheckIn ";
            this.toolStripButton1.Visible = false;
            // 
            // tsb_Clear
            // 
            this.tsb_Clear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Clear.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Clear.Image")));
            this.tsb_Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Clear.Name = "tsb_Clear";
            this.tsb_Clear.Size = new System.Drawing.Size(41, 50);
            this.tsb_Clear.Tag = "Clear";
            this.tsb_Clear.Text = "&Clear";
            this.tsb_Clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Clear.Visible = false;
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(66, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save&&Cls";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "SAve and Close";
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnl_PriorAuthorizations
            // 
            this.pnl_PriorAuthorizations.Controls.Add(this.lbl_BottomBrd);
            this.pnl_PriorAuthorizations.Controls.Add(this.lbl_LeftBrd);
            this.pnl_PriorAuthorizations.Controls.Add(this.lbl_RightBrd);
            this.pnl_PriorAuthorizations.Controls.Add(this.lbl_TopBrd);
            this.pnl_PriorAuthorizations.Controls.Add(this.c1PriorAuthorization);
            this.pnl_PriorAuthorizations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_PriorAuthorizations.Location = new System.Drawing.Point(0, 54);
            this.pnl_PriorAuthorizations.Name = "pnl_PriorAuthorizations";
            this.pnl_PriorAuthorizations.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_PriorAuthorizations.Size = new System.Drawing.Size(647, 351);
            this.pnl_PriorAuthorizations.TabIndex = 0;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 347);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(639, 1);
            this.lbl_BottomBrd.TabIndex = 14;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 344);
            this.lbl_LeftBrd.TabIndex = 13;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(643, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 344);
            this.lbl_RightBrd.TabIndex = 12;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(641, 1);
            this.lbl_TopBrd.TabIndex = 11;
            this.lbl_TopBrd.Text = "label1";
            // 
            // c1PriorAuthorization
            // 
            this.c1PriorAuthorization.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PriorAuthorization.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1PriorAuthorization.AutoGenerateColumns = false;
            this.c1PriorAuthorization.AutoResize = false;
            this.c1PriorAuthorization.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PriorAuthorization.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PriorAuthorization.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1PriorAuthorization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PriorAuthorization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PriorAuthorization.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PriorAuthorization.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PriorAuthorization.Location = new System.Drawing.Point(3, 3);
            this.c1PriorAuthorization.Name = "c1PriorAuthorization";
            this.c1PriorAuthorization.Rows.Count = 1;
            this.c1PriorAuthorization.Rows.DefaultSize = 19;
            this.c1PriorAuthorization.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PriorAuthorization.Size = new System.Drawing.Size(641, 345);
            this.c1PriorAuthorization.StyleInfo = resources.GetString("c1PriorAuthorization.StyleInfo");
            this.c1PriorAuthorization.TabIndex = 0;
            this.c1PriorAuthorization.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1PriorAuthorization_MouseMove);
            this.c1PriorAuthorization.DoubleClick += new System.EventHandler(this.c1PriorAuthorization_DoubleClick);
            // 
            // cmnu_PriorAuthorization
            // 
            this.cmnu_PriorAuthorization.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmnu_PriorAuthorization.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modiFyToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.cmnu_PriorAuthorization.Name = "cmnu_Appointment";
            this.cmnu_PriorAuthorization.Size = new System.Drawing.Size(118, 48);
            // 
            // modiFyToolStripMenuItem
            // 
            this.modiFyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("modiFyToolStripMenuItem.Image")));
            this.modiFyToolStripMenuItem.Name = "modiFyToolStripMenuItem";
            this.modiFyToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.modiFyToolStripMenuItem.Text = "Modify";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmPriorAuthorizationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(647, 405);
            this.Controls.Add(this.pnl_PriorAuthorizations);
            this.Controls.Add(this.pnl_tlspTOP);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPriorAuthorizationList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prior Authorizations";
            this.Load += new System.EventHandler(this.frmPriorAuthorizationList_Load);
            this.pnl_tlspTOP.ResumeLayout(false);
            this.pnl_tlspTOP.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnl_PriorAuthorizations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PriorAuthorization)).EndInit();
            this.cmnu_PriorAuthorization.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlspTOP;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        internal System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel pnl_PriorAuthorizations;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PriorAuthorization;
        internal System.Windows.Forms.ToolStripButton tsb_Clear;
        private System.Windows.Forms.ContextMenuStrip cmnu_PriorAuthorization;
        private System.Windows.Forms.ToolStripMenuItem modiFyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}