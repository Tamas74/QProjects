namespace gloPatient
{
    partial class frmRelationShip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelationShip));
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRelationship = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblAppBlockType = new System.Windows.Forms.Label();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.pnlSearch.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(406, 12);
            this.label6.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(0, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(406, 7);
            this.label5.TabIndex = 3;
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(53, 43);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = " Cancel";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_OK
            // 
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(34, 43);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "  OK";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(0, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(7, 52);
            this.label7.TabIndex = 5;
            // 
            // txtRelationship
            // 
            this.txtRelationship.Location = new System.Drawing.Point(96, 18);
            this.txtRelationship.Name = "txtRelationship";
            this.txtRelationship.Size = new System.Drawing.Size(293, 20);
            this.txtRelationship.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Location = new System.Drawing.Point(399, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(7, 52);
            this.label10.TabIndex = 10;
            this.label10.Visible = false;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlSearch.BackgroundImage")));
            this.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearch.Controls.Add(this.txtRelationship);
            this.pnlSearch.Controls.Add(this.label10);
            this.pnlSearch.Controls.Add(this.lblAppBlockType);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(0, 46);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(406, 71);
            this.pnlSearch.TabIndex = 14;
            // 
            // lblAppBlockType
            // 
            this.lblAppBlockType.AutoSize = true;
            this.lblAppBlockType.BackColor = System.Drawing.Color.Transparent;
            this.lblAppBlockType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppBlockType.Location = new System.Drawing.Point(13, 20);
            this.lblAppBlockType.Name = "lblAppBlockType";
            this.lblAppBlockType.Size = new System.Drawing.Size(77, 15);
            this.lblAppBlockType.TabIndex = 6;
            this.lblAppBlockType.Text = "Relationship";
            this.lblAppBlockType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(406, 46);
            this.ts_Commands.TabIndex = 13;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // frmRelationShip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 117);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.ts_Commands);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRelationShip";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relationship";
            this.Load += new System.EventHandler(this.frmRelationShip_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRelationship;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblAppBlockType;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
    }
}