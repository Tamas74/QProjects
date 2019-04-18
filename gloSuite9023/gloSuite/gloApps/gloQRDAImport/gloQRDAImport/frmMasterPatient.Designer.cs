namespace gloQRDAImport
{
    partial class frmMasterPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMasterPatient));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlDataGrid = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.tls_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.cmdImport = new System.Windows.Forms.ToolStripButton();
            this.pnlflexgrid = new System.Windows.Forms.Panel();
            this.lblGridBottom = new System.Windows.Forms.Label();
            this.lblGridLeft = new System.Windows.Forms.Label();
            this.lblGridRight = new System.Windows.Forms.Label();
            this.lblGridTop = new System.Windows.Forms.Label();
            this.pnlDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlToolstrip.SuspendLayout();
            this.tls_Main.SuspendLayout();
            this.pnlflexgrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdBrowse.BackgroundImage")));
            this.cmdBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdBrowse.ForeColor = System.Drawing.Color.Black;
            this.cmdBrowse.Image = ((System.Drawing.Image)(resources.GetObject("cmdBrowse.Image")));
            this.cmdBrowse.Location = new System.Drawing.Point(621, 50);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(24, 24);
            this.cmdBrowse.TabIndex = 2;
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            this.cmdBrowse.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.cmdBrowse.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // txtPath
            // 
            this.txtPath.ForeColor = System.Drawing.Color.Black;
            this.txtPath.Location = new System.Drawing.Point(139, 51);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(477, 22);
            this.txtPath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(18, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 14);
            this.label2.TabIndex = 18;
            this.label2.Text = "Select JSON Folder :";
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(620, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.button1.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // textBox1
            // 
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(139, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(476, 22);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(25, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 14);
            this.label1.TabIndex = 22;
            this.label1.Text = "Select XML Folder :";
            // 
            // pnlDataGrid
            // 
            this.pnlDataGrid.Controls.Add(this.dataGridView1);
            this.pnlDataGrid.Controls.Add(this.Label8);
            this.pnlDataGrid.Controls.Add(this.Label7);
            this.pnlDataGrid.Controls.Add(this.Label6);
            this.pnlDataGrid.Controls.Add(this.Label5);
            this.pnlDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDataGrid.Location = new System.Drawing.Point(0, 145);
            this.pnlDataGrid.Name = "pnlDataGrid";
            this.pnlDataGrid.Padding = new System.Windows.Forms.Padding(3);
            this.pnlDataGrid.Size = new System.Drawing.Size(669, 288);
            this.pnlDataGrid.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(143)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(181)))), ((int)(((byte)(221)))));
            this.dataGridView1.Location = new System.Drawing.Point(4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(218)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(661, 280);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label8.Location = new System.Drawing.Point(4, 284);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(661, 1);
            this.Label8.TabIndex = 31;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label7.Location = new System.Drawing.Point(4, 3);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(661, 1);
            this.Label7.TabIndex = 30;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label6.Location = new System.Drawing.Point(665, 3);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 282);
            this.Label6.TabIndex = 29;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label5.Location = new System.Drawing.Point(3, 3);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(1, 282);
            this.Label5.TabIndex = 28;
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.AutoSize = true;
            this.pnlToolstrip.Controls.Add(this.tls_Main);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(669, 56);
            this.pnlToolstrip.TabIndex = 2;
            // 
            // tls_Main
            // 
            this.tls_Main.AutoSize = false;
            this.tls_Main.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Main.BackgroundImage")));
            this.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdImport});
            this.tls_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tls_Main.Location = new System.Drawing.Point(0, 0);
            this.tls_Main.Name = "tls_Main";
            this.tls_Main.Size = new System.Drawing.Size(669, 56);
            this.tls_Main.TabIndex = 3;
            this.tls_Main.TabStop = true;
            this.tls_Main.Text = "ToolStrip1";
            // 
            // cmdImport
            // 
            this.cmdImport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdImport.Image = ((System.Drawing.Image)(resources.GetObject("cmdImport.Image")));
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(54, 53);
            this.cmdImport.Tag = "Import";
            this.cmdImport.Text = "&Import";
            this.cmdImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdImport.ToolTipText = "Import";
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // pnlflexgrid
            // 
            this.pnlflexgrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlflexgrid.Controls.Add(this.lblGridBottom);
            this.pnlflexgrid.Controls.Add(this.lblGridLeft);
            this.pnlflexgrid.Controls.Add(this.lblGridRight);
            this.pnlflexgrid.Controls.Add(this.button1);
            this.pnlflexgrid.Controls.Add(this.lblGridTop);
            this.pnlflexgrid.Controls.Add(this.textBox1);
            this.pnlflexgrid.Controls.Add(this.txtPath);
            this.pnlflexgrid.Controls.Add(this.label2);
            this.pnlflexgrid.Controls.Add(this.label1);
            this.pnlflexgrid.Controls.Add(this.cmdBrowse);
            this.pnlflexgrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlflexgrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlflexgrid.Location = new System.Drawing.Point(0, 56);
            this.pnlflexgrid.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlflexgrid.Name = "pnlflexgrid";
            this.pnlflexgrid.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlflexgrid.Size = new System.Drawing.Size(669, 89);
            this.pnlflexgrid.TabIndex = 0;
            // 
            // lblGridBottom
            // 
            this.lblGridBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGridBottom.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblGridBottom.Location = new System.Drawing.Point(4, 88);
            this.lblGridBottom.Name = "lblGridBottom";
            this.lblGridBottom.Size = new System.Drawing.Size(661, 1);
            this.lblGridBottom.TabIndex = 10;
            this.lblGridBottom.Text = "label2";
            // 
            // lblGridLeft
            // 
            this.lblGridLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGridLeft.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridLeft.Location = new System.Drawing.Point(3, 4);
            this.lblGridLeft.Name = "lblGridLeft";
            this.lblGridLeft.Size = new System.Drawing.Size(1, 85);
            this.lblGridLeft.TabIndex = 9;
            this.lblGridLeft.Text = "label4";
            // 
            // lblGridRight
            // 
            this.lblGridRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblGridRight.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblGridRight.Location = new System.Drawing.Point(665, 4);
            this.lblGridRight.Name = "lblGridRight";
            this.lblGridRight.Size = new System.Drawing.Size(1, 85);
            this.lblGridRight.TabIndex = 8;
            this.lblGridRight.Text = "label3";
            // 
            // lblGridTop
            // 
            this.lblGridTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridTop.Location = new System.Drawing.Point(3, 3);
            this.lblGridTop.Name = "lblGridTop";
            this.lblGridTop.Size = new System.Drawing.Size(663, 1);
            this.lblGridTop.TabIndex = 7;
            this.lblGridTop.Text = "label1";
            // 
            // frmMasterPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(669, 433);
            this.Controls.Add(this.pnlDataGrid);
            this.Controls.Add(this.pnlflexgrid);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMasterPatient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import JSON";
            this.pnlDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlToolstrip.ResumeLayout(false);
            this.tls_Main.ResumeLayout(false);
            this.tls_Main.PerformLayout();
            this.pnlflexgrid.ResumeLayout(false);
            this.pnlflexgrid.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Panel pnlDataGrid;
        internal System.Windows.Forms.DataGridView dataGridView1;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Panel pnlToolstrip;
        internal gloGlobal.gloToolStripIgnoreFocus tls_Main;
        internal System.Windows.Forms.ToolStripButton cmdImport;
        internal System.Windows.Forms.Panel pnlflexgrid;
        private System.Windows.Forms.Label lblGridBottom;
        private System.Windows.Forms.Label lblGridLeft;
        private System.Windows.Forms.Label lblGridRight;
        private System.Windows.Forms.Label lblGridTop;
    }
}