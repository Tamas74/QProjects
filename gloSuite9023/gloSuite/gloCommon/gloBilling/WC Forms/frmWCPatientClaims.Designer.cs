namespace gloBilling.WC_Forms
{
    partial class frmWCPatientClaims
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWCPatientClaims));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_SaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlSearchStrip = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnl_txtSearch = new System.Windows.Forms.Panel();
            this.txtClaimNo = new System.Windows.Forms.TextBox();
            this.Label77 = new System.Windows.Forms.Label();
            this.Label61 = new System.Windows.Forms.Label();
            this.Label62 = new System.Windows.Forms.Label();
            this.txtClearSearch = new System.Windows.Forms.Button();
            this.Label63 = new System.Windows.Forms.Label();
            this.Label64 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlClaims = new System.Windows.Forms.Panel();
            this.c1Claims = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlSearchStrip.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnl_txtSearch.SuspendLayout();
            this.pnlClaims.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Claims)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlToolStrip.Size = new System.Drawing.Size(814, 54);
            this.pnlToolStrip.TabIndex = 3;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_SaveAndClose,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(3, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(808, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_SaveAndClose
            // 
            this.tsb_SaveAndClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SaveAndClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_SaveAndClose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveAndClose.Image")));
            this.tsb_SaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveAndClose.Name = "tsb_SaveAndClose";
            this.tsb_SaveAndClose.Size = new System.Drawing.Size(66, 50);
            this.tsb_SaveAndClose.Tag = "Save&Close";
            this.tsb_SaveAndClose.Text = "&Save&&Cls";
            this.tsb_SaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SaveAndClose.ToolTipText = "Save and Close";
            this.tsb_SaveAndClose.Click += new System.EventHandler(this.tsb_SaveAndClose_Click);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Close";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // pnlSearchStrip
            // 
            this.pnlSearchStrip.Controls.Add(this.pnlTop);
            this.pnlSearchStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchStrip.Location = new System.Drawing.Point(0, 54);
            this.pnlSearchStrip.Name = "pnlSearchStrip";
            this.pnlSearchStrip.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearchStrip.Size = new System.Drawing.Size(814, 30);
            this.pnlSearchStrip.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTop.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.pnl_txtSearch);
            this.pnlTop.Controls.Add(this.label6);
            this.pnlTop.Controls.Add(this.label52);
            this.pnlTop.Controls.Add(this.label7);
            this.pnlTop.Controls.Add(this.label9);
            this.pnlTop.Controls.Add(this.label13);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(3, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(808, 24);
            this.pnlTop.TabIndex = 29;
            // 
            // pnl_txtSearch
            // 
            this.pnl_txtSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnl_txtSearch.Controls.Add(this.txtClaimNo);
            this.pnl_txtSearch.Controls.Add(this.Label77);
            this.pnl_txtSearch.Controls.Add(this.Label61);
            this.pnl_txtSearch.Controls.Add(this.Label62);
            this.pnl_txtSearch.Controls.Add(this.txtClearSearch);
            this.pnl_txtSearch.Controls.Add(this.Label63);
            this.pnl_txtSearch.Controls.Add(this.Label64);
            this.pnl_txtSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_txtSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_txtSearch.ForeColor = System.Drawing.Color.Black;
            this.pnl_txtSearch.Location = new System.Drawing.Point(71, 1);
            this.pnl_txtSearch.Name = "pnl_txtSearch";
            this.pnl_txtSearch.Size = new System.Drawing.Size(241, 22);
            this.pnl_txtSearch.TabIndex = 231;
            // 
            // txtClaimNo
            // 
            this.txtClaimNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtClaimNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClaimNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaimNo.ForeColor = System.Drawing.Color.Black;
            this.txtClaimNo.Location = new System.Drawing.Point(5, 3);
            this.txtClaimNo.Name = "txtClaimNo";
            this.txtClaimNo.Size = new System.Drawing.Size(212, 15);
            this.txtClaimNo.TabIndex = 5;
            this.txtClaimNo.TextChanged += new System.EventHandler(this.txtClaimNo_TextChanged);
            // 
            // Label77
            // 
            this.Label77.BackColor = System.Drawing.Color.White;
            this.Label77.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label77.Location = new System.Drawing.Point(5, 17);
            this.Label77.Name = "Label77";
            this.Label77.Size = new System.Drawing.Size(212, 5);
            this.Label77.TabIndex = 43;
            // 
            // Label61
            // 
            this.Label61.BackColor = System.Drawing.Color.White;
            this.Label61.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label61.Location = new System.Drawing.Point(5, 0);
            this.Label61.Name = "Label61";
            this.Label61.Size = new System.Drawing.Size(212, 3);
            this.Label61.TabIndex = 37;
            // 
            // Label62
            // 
            this.Label62.BackColor = System.Drawing.Color.White;
            this.Label62.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label62.Location = new System.Drawing.Point(1, 0);
            this.Label62.Name = "Label62";
            this.Label62.Size = new System.Drawing.Size(4, 22);
            this.Label62.TabIndex = 38;
            // 
            // txtClearSearch
            // 
            this.txtClearSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtClearSearch.BackgroundImage")));
            this.txtClearSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtClearSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtClearSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtClearSearch.FlatAppearance.BorderSize = 0;
            this.txtClearSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.txtClearSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.txtClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtClearSearch.Image = ((System.Drawing.Image)(resources.GetObject("txtClearSearch.Image")));
            this.txtClearSearch.Location = new System.Drawing.Point(217, 0);
            this.txtClearSearch.Name = "txtClearSearch";
            this.txtClearSearch.Size = new System.Drawing.Size(23, 22);
            this.txtClearSearch.TabIndex = 41;
            this.txtClearSearch.UseVisualStyleBackColor = true;
            this.txtClearSearch.Click += new System.EventHandler(this.txtClearSearch_Click);
            // 
            // Label63
            // 
            this.Label63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label63.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label63.Location = new System.Drawing.Point(0, 0);
            this.Label63.Name = "Label63";
            this.Label63.Size = new System.Drawing.Size(1, 22);
            this.Label63.TabIndex = 39;
            this.Label63.Text = "label4";
            // 
            // Label64
            // 
            this.Label64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label64.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label64.Location = new System.Drawing.Point(240, 0);
            this.Label64.Name = "Label64";
            this.Label64.Size = new System.Drawing.Size(1, 22);
            this.Label64.TabIndex = 40;
            this.Label64.Text = "label4";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(1, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 22);
            this.label6.TabIndex = 20;
            this.label6.Text = " Claim # :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(0, 1);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 22);
            this.label52.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(807, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 22);
            this.label7.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(808, 1);
            this.label9.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(0, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(808, 1);
            this.label13.TabIndex = 58;
            // 
            // pnlClaims
            // 
            this.pnlClaims.AutoScroll = true;
            this.pnlClaims.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlClaims.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlClaims.Controls.Add(this.c1Claims);
            this.pnlClaims.Controls.Add(this.label1);
            this.pnlClaims.Controls.Add(this.label2);
            this.pnlClaims.Controls.Add(this.label3);
            this.pnlClaims.Controls.Add(this.label4);
            this.pnlClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlClaims.ForeColor = System.Drawing.Color.White;
            this.pnlClaims.Location = new System.Drawing.Point(0, 84);
            this.pnlClaims.Name = "pnlClaims";
            this.pnlClaims.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlClaims.Size = new System.Drawing.Size(814, 505);
            this.pnlClaims.TabIndex = 1;
            // 
            // c1Claims
            // 
            this.c1Claims.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Claims.AllowEditing = false;
            this.c1Claims.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Claims.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Claims.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.c1Claims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Claims.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1Claims.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1Claims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Claims.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Claims.Location = new System.Drawing.Point(4, 1);
            this.c1Claims.Name = "c1Claims";
            this.c1Claims.Rows.Count = 1;
            this.c1Claims.Rows.DefaultSize = 19;
            this.c1Claims.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Claims.ShowCellLabels = true;
            this.c1Claims.Size = new System.Drawing.Size(806, 500);
            this.c1Claims.StyleInfo = resources.GetString("c1Claims.StyleInfo");
            this.c1Claims.TabIndex = 54;
            this.c1Claims.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1Claims.Tree.NodeImageCollapsed")));
            this.c1Claims.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1Claims.Tree.NodeImageExpanded")));
            this.c1Claims.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1Claims_AfterSort);
            this.c1Claims.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1Claims_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 500);
            this.label1.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(810, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 500);
            this.label2.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(808, 1);
            this.label3.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(3, 501);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(808, 1);
            this.label4.TabIndex = 58;
            // 
            // frmWCPatientClaims
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(814, 589);
            this.Controls.Add(this.pnlClaims);
            this.Controls.Add(this.pnlSearchStrip);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmWCPatientClaims";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NY Workers Comp";
            this.Load += new System.EventHandler(this.frmWCPatientClaims_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlSearchStrip.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnl_txtSearch.ResumeLayout(false);
            this.pnl_txtSearch.PerformLayout();
            this.pnlClaims.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Claims)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_SaveAndClose;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlSearchStrip;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlClaims;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Claims;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Panel pnl_txtSearch;
        internal System.Windows.Forms.TextBox txtClaimNo;
        internal System.Windows.Forms.Label Label77;
        internal System.Windows.Forms.Label Label61;
        internal System.Windows.Forms.Label Label62;
        internal System.Windows.Forms.Button txtClearSearch;
        private System.Windows.Forms.Label Label63;
        private System.Windows.Forms.Label Label64;
    }
}