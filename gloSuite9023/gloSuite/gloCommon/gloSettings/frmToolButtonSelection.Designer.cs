namespace gloSettings
{
    partial class frmToolButtonSelection
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
                if (oToolTip != null)
                {
                    oToolTip.Dispose();
                    oToolTip = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmToolButtonSelection));
            this.pnl_tlspTOP = new System.Windows.Forms.Panel();
            this.tls_ToolButton = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClearAll = new System.Windows.Forms.ToolStripButton();
            this.ts_btnReset = new System.Windows.Forms.ToolStripButton();
            this.ts_btnOk = new System.Windows.Forms.ToolStripButton();
            this.ts_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.trvSelectedButtons = new System.Windows.Forms.TreeView();
            this.trvButtons = new System.Windows.Forms.TreeView();
            this.lbl_pnlLeft = new System.Windows.Forms.Label();
            this.lbl_pnlRight = new System.Windows.Forms.Label();
            this.lbl_pnlTop = new System.Windows.Forms.Label();
            this.lbl_pnlBottom = new System.Windows.Forms.Label();
            this.imgDashBoard = new System.Windows.Forms.ImageList(this.components);
            this.imgPatientDetails = new System.Windows.Forms.ImageList(this.components);
            this.pnl_tlspTOP.SuspendLayout();
            this.tls_ToolButton.SuspendLayout();
            this.pnlTOP.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlspTOP
            // 
            this.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlspTOP.Controls.Add(this.tls_ToolButton);
            this.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlspTOP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlspTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlspTOP.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlspTOP.Name = "pnl_tlspTOP";
            this.pnl_tlspTOP.Size = new System.Drawing.Size(562, 53);
            this.pnl_tlspTOP.TabIndex = 26;
            // 
            // tls_ToolButton
            // 
            this.tls_ToolButton.BackColor = System.Drawing.Color.Transparent;
            this.tls_ToolButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_ToolButton.BackgroundImage")));
            this.tls_ToolButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_ToolButton.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tls_ToolButton.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_ToolButton.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnSelectAll,
            this.ts_btnClearAll,
            this.ts_btnReset,
            this.ts_btnOk,
            this.ts_btnCancel});
            this.tls_ToolButton.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_ToolButton.Location = new System.Drawing.Point(0, 0);
            this.tls_ToolButton.Name = "tls_ToolButton";
            this.tls_ToolButton.Size = new System.Drawing.Size(562, 53);
            this.tls_ToolButton.TabIndex = 0;
            this.tls_ToolButton.Text = "toolStrip1";
            // 
            // ts_btnSelectAll
            // 
            this.ts_btnSelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSelectAll.Image")));
            this.ts_btnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSelectAll.Name = "ts_btnSelectAll";
            this.ts_btnSelectAll.Size = new System.Drawing.Size(67, 50);
            this.ts_btnSelectAll.Tag = "SelectAll";
            this.ts_btnSelectAll.Text = "Select &All";
            this.ts_btnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSelectAll.ToolTipText = "Select All";
            this.ts_btnSelectAll.Visible = false;
            this.ts_btnSelectAll.Click += new System.EventHandler(this.ts_btnSelectAll_Click);
            // 
            // ts_btnClearAll
            // 
            this.ts_btnClearAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClearAll.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClearAll.Image")));
            this.ts_btnClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClearAll.Name = "ts_btnClearAll";
            this.ts_btnClearAll.Size = new System.Drawing.Size(60, 50);
            this.ts_btnClearAll.Tag = "ClearAll";
            this.ts_btnClearAll.Text = "Clear &All";
            this.ts_btnClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClearAll.ToolTipText = "Clear All";
            this.ts_btnClearAll.Visible = false;
            this.ts_btnClearAll.Click += new System.EventHandler(this.ts_btnClearAll_Click);
            // 
            // ts_btnReset
            // 
            this.ts_btnReset.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnReset.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnReset.Image")));
            this.ts_btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnReset.Name = "ts_btnReset";
            this.ts_btnReset.Size = new System.Drawing.Size(46, 50);
            this.ts_btnReset.Tag = "Reset";
            this.ts_btnReset.Text = "&Reset";
            this.ts_btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnReset.Click += new System.EventHandler(this.ts_btnReset_Click);
            // 
            // ts_btnOk
            // 
            this.ts_btnOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnOk.Image")));
            this.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnOk.Name = "ts_btnOk";
            this.ts_btnOk.Size = new System.Drawing.Size(66, 50);
            this.ts_btnOk.Tag = "OK";
            this.ts_btnOk.Text = "&Save&&Cls";
            this.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnOk.ToolTipText = "Save and Close";
            this.ts_btnOk.Click += new System.EventHandler(this.ts_btnOk_Click);
            // 
            // ts_btnCancel
            // 
            this.ts_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnCancel.Image")));
            this.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnCancel.Name = "ts_btnCancel";
            this.ts_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.ts_btnCancel.Tag = "Cancel";
            this.ts_btnCancel.Text = "&Close";
            this.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnCancel.Click += new System.EventHandler(this.ts_btnCancel_Click);
            // 
            // pnlTOP
            // 
            this.pnlTOP.BackColor = System.Drawing.Color.Transparent;
            this.pnlTOP.Controls.Add(this.btnDown);
            this.pnlTOP.Controls.Add(this.btnRemove);
            this.pnlTOP.Controls.Add(this.btnUp);
            this.pnlTOP.Controls.Add(this.btnAdd);
            this.pnlTOP.Controls.Add(this.trvSelectedButtons);
            this.pnlTOP.Controls.Add(this.trvButtons);
            this.pnlTOP.Controls.Add(this.lbl_pnlLeft);
            this.pnlTOP.Controls.Add(this.lbl_pnlRight);
            this.pnlTOP.Controls.Add(this.lbl_pnlTop);
            this.pnlTOP.Controls.Add(this.lbl_pnlBottom);
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTOP.Location = new System.Drawing.Point(0, 53);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Padding = new System.Windows.Forms.Padding(2);
            this.pnlTOP.Size = new System.Drawing.Size(562, 364);
            this.pnlTOP.TabIndex = 27;
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(520, 191);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(25, 25);
            this.btnDown.TabIndex = 13;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.MouseLeave += new System.EventHandler(this.btnDown_MouseLeave);
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            this.btnDown.MouseHover += new System.EventHandler(this.btnDown_MouseHover);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Transparent;
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Location = new System.Drawing.Point(246, 191);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(25, 25);
            this.btnRemove.TabIndex = 13;
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.MouseLeave += new System.EventHandler(this.btnRemove_MouseLeave);
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            this.btnRemove.MouseHover += new System.EventHandler(this.btnRemove_MouseHover);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Location = new System.Drawing.Point(520, 148);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(25, 25);
            this.btnUp.TabIndex = 13;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.MouseLeave += new System.EventHandler(this.btnUp_MouseLeave);
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            this.btnUp.MouseHover += new System.EventHandler(this.btnUp_MouseHover);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(246, 148);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(25, 25);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseHover += new System.EventHandler(this.btnAdd_MouseHover);
            // 
            // trvSelectedButtons
            // 
            this.trvSelectedButtons.FullRowSelect = true;
            this.trvSelectedButtons.HideSelection = false;
            this.trvSelectedButtons.Location = new System.Drawing.Point(281, 5);
            this.trvSelectedButtons.Name = "trvSelectedButtons";
            this.trvSelectedButtons.ShowLines = false;
            this.trvSelectedButtons.ShowPlusMinus = false;
            this.trvSelectedButtons.ShowRootLines = false;
            this.trvSelectedButtons.Size = new System.Drawing.Size(229, 354);
            this.trvSelectedButtons.TabIndex = 12;
            this.trvSelectedButtons.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvSelectedButtons_NodeMouseDoubleClick);
            // 
            // trvButtons
            // 
            this.trvButtons.FullRowSelect = true;
            this.trvButtons.HideSelection = false;
            this.trvButtons.Location = new System.Drawing.Point(6, 5);
            this.trvButtons.Name = "trvButtons";
            this.trvButtons.ShowLines = false;
            this.trvButtons.ShowPlusMinus = false;
            this.trvButtons.ShowRootLines = false;
            this.trvButtons.Size = new System.Drawing.Size(229, 354);
            this.trvButtons.TabIndex = 11;
            this.trvButtons.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvButtons_NodeMouseDoubleClick);
            // 
            // lbl_pnlLeft
            // 
            this.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlLeft.Location = new System.Drawing.Point(2, 3);
            this.lbl_pnlLeft.Name = "lbl_pnlLeft";
            this.lbl_pnlLeft.Size = new System.Drawing.Size(1, 358);
            this.lbl_pnlLeft.TabIndex = 7;
            this.lbl_pnlLeft.Text = "label4";
            // 
            // lbl_pnlRight
            // 
            this.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRight.Location = new System.Drawing.Point(559, 3);
            this.lbl_pnlRight.Name = "lbl_pnlRight";
            this.lbl_pnlRight.Size = new System.Drawing.Size(1, 358);
            this.lbl_pnlRight.TabIndex = 6;
            this.lbl_pnlRight.Text = "label3";
            // 
            // lbl_pnlTop
            // 
            this.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTop.Location = new System.Drawing.Point(2, 2);
            this.lbl_pnlTop.Name = "lbl_pnlTop";
            this.lbl_pnlTop.Size = new System.Drawing.Size(558, 1);
            this.lbl_pnlTop.TabIndex = 5;
            this.lbl_pnlTop.Text = "label1";
            // 
            // lbl_pnlBottom
            // 
            this.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlBottom.Location = new System.Drawing.Point(2, 361);
            this.lbl_pnlBottom.Name = "lbl_pnlBottom";
            this.lbl_pnlBottom.Size = new System.Drawing.Size(558, 1);
            this.lbl_pnlBottom.TabIndex = 8;
            this.lbl_pnlBottom.Text = "label2";
            // 
            // imgDashBoard
            // 
            this.imgDashBoard.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgDashBoard.ImageStream")));
            this.imgDashBoard.TransparentColor = System.Drawing.Color.Transparent;
            this.imgDashBoard.Images.SetKeyName(0, "Saperator.png");
            // 
            // imgPatientDetails
            // 
            this.imgPatientDetails.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgPatientDetails.ImageSize = new System.Drawing.Size(16, 16);
            this.imgPatientDetails.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmToolButtonSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(562, 417);
            this.Controls.Add(this.pnlTOP);
            this.Controls.Add(this.pnl_tlspTOP);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmToolButtonSelection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customize Toolbar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmToolButtonSelection_FormClosing);
            this.Load += new System.EventHandler(this.frmToolButtonSelection_Load);
            this.pnl_tlspTOP.ResumeLayout(false);
            this.pnl_tlspTOP.PerformLayout();
            this.tls_ToolButton.ResumeLayout(false);
            this.tls_ToolButton.PerformLayout();
            this.pnlTOP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlspTOP;
        private gloGlobal.gloToolStripIgnoreFocus tls_ToolButton;
        private System.Windows.Forms.ToolStripButton ts_btnSelectAll;
        private System.Windows.Forms.ToolStripButton ts_btnClearAll;
        private System.Windows.Forms.ToolStripButton ts_btnReset;
        private System.Windows.Forms.ToolStripButton ts_btnOk;
        private System.Windows.Forms.ToolStripButton ts_btnCancel;
        internal System.Windows.Forms.Panel pnlTOP;
        internal System.Windows.Forms.Button btnDown;
        internal System.Windows.Forms.Button btnRemove;
        internal System.Windows.Forms.Button btnUp;
        internal System.Windows.Forms.TreeView trvSelectedButtons;
        internal System.Windows.Forms.TreeView trvButtons;
        private System.Windows.Forms.Label lbl_pnlLeft;
        private System.Windows.Forms.Label lbl_pnlRight;
        private System.Windows.Forms.Label lbl_pnlTop;
        private System.Windows.Forms.Label lbl_pnlBottom;
        internal System.Windows.Forms.ImageList imgDashBoard;
        internal System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ImageList imgPatientDetails;

    }
}