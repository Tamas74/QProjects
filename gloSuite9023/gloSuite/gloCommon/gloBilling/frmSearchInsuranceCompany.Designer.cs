namespace gloBilling
{
    partial class frmSearchInsuranceCompany
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchInsuranceCompany));
            this.pnl_tlsp_Top = new System.Windows.Forms.Panel();
            this.tstrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tlsbtnModify = new System.Windows.Forms.ToolStripButton();
            this.tlsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tlsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Panel13 = new System.Windows.Forms.Panel();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label36 = new System.Windows.Forms.Label();
            this.Label37 = new System.Windows.Forms.Label();
            this.Label38 = new System.Windows.Forms.Label();
            this.C1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1InsuranceCompany = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnl_tlsp_Top.SuspendLayout();
            this.tstrip.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1FlexGrid1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1InsuranceCompany)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_tlsp_Top
            // 
            this.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp_Top.Controls.Add(this.tstrip);
            this.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp_Top.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlsp_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp_Top.Name = "pnl_tlsp_Top";
            this.pnl_tlsp_Top.Size = new System.Drawing.Size(792, 54);
            this.pnl_tlsp_Top.TabIndex = 18;
            // 
            // tstrip
            // 
            this.tstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tstrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tstrip.BackgroundImage")));
            this.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tstrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tstrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsbtnAdd,
            this.tlsbtnModify,
            this.tlsbtnSave,
            this.tlsbtnClose});
            this.tstrip.Location = new System.Drawing.Point(0, 0);
            this.tstrip.Name = "tstrip";
            this.tstrip.Size = new System.Drawing.Size(792, 53);
            this.tstrip.TabIndex = 3;
            this.tstrip.Text = "ToolStrip1";
            // 
            // tlsbtnAdd
            // 
            this.tlsbtnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtnAdd.Image")));
            this.tlsbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsbtnAdd.Name = "tlsbtnAdd";
            this.tlsbtnAdd.Size = new System.Drawing.Size(36, 50);
            this.tlsbtnAdd.Text = "&Add";
            this.tlsbtnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtnAdd.Click += new System.EventHandler(this.tlsbtnAdd_Click);
            // 
            // tlsbtnModify
            // 
            this.tlsbtnModify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtnModify.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtnModify.Image")));
            this.tlsbtnModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsbtnModify.Name = "tlsbtnModify";
            this.tlsbtnModify.Size = new System.Drawing.Size(53, 50);
            this.tlsbtnModify.Text = "&Modify";
            this.tlsbtnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtnModify.Click += new System.EventHandler(this.tlsbtnModify_Click);
            // 
            // tlsbtnSave
            // 
            this.tlsbtnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtnSave.Image")));
            this.tlsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsbtnSave.Name = "tlsbtnSave";
            this.tlsbtnSave.Size = new System.Drawing.Size(66, 50);
            this.tlsbtnSave.Text = "Sa&ve&&Cls";
            this.tlsbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtnSave.ToolTipText = "Save and Close";
            this.tlsbtnSave.Click += new System.EventHandler(this.tlsbtnSave_Click);
            // 
            // tlsbtnClose
            // 
            this.tlsbtnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtnClose.Image")));
            this.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsbtnClose.Name = "tlsbtnClose";
            this.tlsbtnClose.Size = new System.Drawing.Size(43, 50);
            this.tlsbtnClose.Text = "&Close";
            this.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtnClose.Click += new System.EventHandler(this.tlsbtnClose_Click);
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel2.Controls.Add(this.txtSearch);
            this.Panel2.Controls.Add(this.Label23);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Controls.Add(this.Label7);
            this.Panel2.Controls.Add(this.Label8);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel2.Location = new System.Drawing.Point(0, 54);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.Panel2.Size = new System.Drawing.Size(792, 34);
            this.Panel2.TabIndex = 20;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(80, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(306, 22);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // Label23
            // 
            this.Label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(4, 4);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(784, 26);
            this.Label23.TabIndex = 10;
            this.Label23.Text = "   Search :";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 30);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(784, 1);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 4);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 27);
            this.Label6.TabIndex = 7;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(788, 4);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 27);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 3);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(786, 1);
            this.Label8.TabIndex = 5;
            this.Label8.Text = "label1";
            // 
            // Panel13
            // 
            this.Panel13.Controls.Add(this.Label35);
            this.Panel13.Controls.Add(this.Label36);
            this.Panel13.Controls.Add(this.Label37);
            this.Panel13.Controls.Add(this.Label38);
            this.Panel13.Location = new System.Drawing.Point(0, 0);
            this.Panel13.Name = "Panel13";
            this.Panel13.Size = new System.Drawing.Size(200, 100);
            this.Panel13.TabIndex = 0;
            // 
            // Label35
            // 
            this.Label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label35.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label35.Location = new System.Drawing.Point(199, 1);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(1, 98);
            this.Label35.TabIndex = 11;
            this.Label35.Text = "label4";
            // 
            // Label36
            // 
            this.Label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label36.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label36.Location = new System.Drawing.Point(1, 99);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(199, 1);
            this.Label36.TabIndex = 10;
            this.Label36.Text = "label1";
            // 
            // Label37
            // 
            this.Label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label37.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label37.Location = new System.Drawing.Point(0, 1);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(1, 99);
            this.Label37.TabIndex = 9;
            this.Label37.Text = "label4";
            // 
            // Label38
            // 
            this.Label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label38.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label38.Location = new System.Drawing.Point(0, 0);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(200, 1);
            this.Label38.TabIndex = 8;
            this.Label38.Text = "label1";
            // 
            // C1FlexGrid1
            // 
            this.C1FlexGrid1.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.C1FlexGrid1.Location = new System.Drawing.Point(0, 0);
            this.C1FlexGrid1.Name = "C1FlexGrid1";
            this.C1FlexGrid1.Rows.DefaultSize = 17;
            this.C1FlexGrid1.Size = new System.Drawing.Size(240, 150);
            this.C1FlexGrid1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.c1InsuranceCompany);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 88);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(792, 518);
            this.panel1.TabIndex = 21;
            // 
            // c1InsuranceCompany
            // 
            this.c1InsuranceCompany.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1InsuranceCompany.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1InsuranceCompany.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.c1InsuranceCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1InsuranceCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1InsuranceCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1InsuranceCompany.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1InsuranceCompany.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1InsuranceCompany.Location = new System.Drawing.Point(4, 1);
            this.c1InsuranceCompany.Name = "c1InsuranceCompany";
            this.c1InsuranceCompany.Rows.DefaultSize = 19;
            this.c1InsuranceCompany.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1InsuranceCompany.Size = new System.Drawing.Size(784, 513);
            this.c1InsuranceCompany.StyleInfo = resources.GetString("c1InsuranceCompany.StyleInfo");
            this.c1InsuranceCompany.TabIndex = 2;
            this.c1InsuranceCompany.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1InsuranceCompany.Tree.NodeImageCollapsed")));
            this.c1InsuranceCompany.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1InsuranceCompany.Tree.NodeImageExpanded")));
            this.c1InsuranceCompany.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1InsuranceCompany_MouseDoubleClick);
            this.c1InsuranceCompany.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1InsuranceCompany_KeyPress);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(4, 514);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(784, 1);
            this.label1.TabIndex = 8;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 514);
            this.label2.TabIndex = 7;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(788, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 514);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(786, 1);
            this.label4.TabIndex = 5;
            this.label4.Text = "label1";
            // 
            // frmSearchInsuranceCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(792, 606);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.pnl_tlsp_Top);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchInsuranceCompany";
            this.ShowInTaskbar = false;
            this.Text = "Search Insurance Company";
            this.Load += new System.EventHandler(this.frmSearchInsuranceCompany_Load);
            this.pnl_tlsp_Top.ResumeLayout(false);
            this.pnl_tlsp_Top.PerformLayout();
            this.tstrip.ResumeLayout(false);
            this.tstrip.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1FlexGrid1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1InsuranceCompany)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlsp_Top;
        internal gloGlobal.gloToolStripIgnoreFocus tstrip;
        internal System.Windows.Forms.ToolStripButton tlsbtnAdd;
        internal System.Windows.Forms.ToolStripButton tlsbtnModify;
        internal System.Windows.Forms.ToolStripButton tlsbtnSave;
        internal System.Windows.Forms.ToolStripButton tlsbtnClose;
        internal System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Panel Panel13;
        private System.Windows.Forms.Label Label35;
        private System.Windows.Forms.Label Label36;
        private System.Windows.Forms.Label Label37;
        private System.Windows.Forms.Label Label38;
        private C1.Win.C1FlexGrid.C1FlexGrid C1FlexGrid1;
        internal System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.Label Label23;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1InsuranceCompany;
    }
}