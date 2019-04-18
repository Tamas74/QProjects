namespace gloBilling
{
    partial class frmCleargagePaymentVoid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 
        
        private bool blnDisposed;
        private static frmCleargagePaymentVoid ofrmCleargagePaymentVoid;

        public static frmCleargagePaymentVoid GetInstance()
        {
            try
            {
                if (ofrmCleargagePaymentVoid == null)
                {
                    ofrmCleargagePaymentVoid = new frmCleargagePaymentVoid();
                }
            }
            finally { }
            return ofrmCleargagePaymentVoid;
        }

        protected override void Dispose(bool disposing)
        {
            if (!(this.blnDisposed))
            {
                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    if ((components != null))
                    {
                        components.Dispose();
                    }
                }
            }
            ofrmCleargagePaymentVoid = null;
            this.blnDisposed = true;
            base.Dispose(disposing);

            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            //base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        ~frmCleargagePaymentVoid()
        {
            Dispose(false);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCleargagePaymentVoid));
            this.panel4 = new System.Windows.Forms.Panel();
            this.tls_Strip = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_AutoDistributeCleargageVoidPayment = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Next = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlText = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.rb_ShowOneByOne = new System.Windows.Forms.RadioButton();
            this.rb_ShowAll = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.pnl_CleargagePaymentVoidList = new System.Windows.Forms.Panel();
            this.C1CleargageVoid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.tls_Strip.SuspendLayout();
            this.pnlText.SuspendLayout();
            this.pnl_CleargagePaymentVoidList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1CleargageVoid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tls_Strip);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1101, 54);
            this.panel4.TabIndex = 3;
            // 
            // tls_Strip
            // 
            this.tls_Strip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tls_Strip.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_Strip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Strip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Strip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_AutoDistributeCleargageVoidPayment,
            this.tsb_Save,
            this.tsb_Next,
            this.tsb_Refresh,
            this.tsb_Close});
            this.tls_Strip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Strip.Location = new System.Drawing.Point(0, 0);
            this.tls_Strip.Name = "tls_Strip";
            this.tls_Strip.Size = new System.Drawing.Size(1101, 53);
            this.tls_Strip.TabIndex = 0;
            this.tls_Strip.Text = "toolStrip1";
            // 
            // tsb_AutoDistributeCleargageVoidPayment
            // 
            this.tsb_AutoDistributeCleargageVoidPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_AutoDistributeCleargageVoidPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_AutoDistributeCleargageVoidPayment.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AutoDistributeCleargageVoidPayment.Image")));
            this.tsb_AutoDistributeCleargageVoidPayment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AutoDistributeCleargageVoidPayment.Name = "tsb_AutoDistributeCleargageVoidPayment";
            this.tsb_AutoDistributeCleargageVoidPayment.Size = new System.Drawing.Size(148, 50);
            this.tsb_AutoDistributeCleargageVoidPayment.Tag = "Auto Distribute Credit";
            this.tsb_AutoDistributeCleargageVoidPayment.Text = "&Auto Distribute Credit";
            this.tsb_AutoDistributeCleargageVoidPayment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsb_AutoDistributeCleargageVoidPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_AutoDistributeCleargageVoidPayment.ToolTipText = "Auto Distribute Credit";
            this.tsb_AutoDistributeCleargageVoidPayment.Click += new System.EventHandler(this.tsb_AutoDistributeCleargageVoidPayment_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // tsb_Next
            // 
            this.tsb_Next.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Next.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Next.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Next.Image")));
            this.tsb_Next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Next.Name = "tsb_Next";
            this.tsb_Next.Size = new System.Drawing.Size(39, 50);
            this.tsb_Next.Tag = "Next";
            this.tsb_Next.Text = "&Next";
            this.tsb_Next.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Next.ToolTipText = "Next";
            this.tsb_Next.Visible = false;
            this.tsb_Next.Click += new System.EventHandler(this.tsb_Next_Click);
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Refresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Refresh.Image = global::gloBilling.Properties.Resources.Ico_Refresh;
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tsb_Refresh.Tag = "Refresh";
            this.tsb_Refresh.Text = "&Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refresh.ToolTipText = "Refresh";
            this.tsb_Refresh.Click += new System.EventHandler(this.tsb_Refresh_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // pnlText
            // 
            this.pnlText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlText.Controls.Add(this.label4);
            this.pnlText.Controls.Add(this.rb_ShowOneByOne);
            this.pnlText.Controls.Add(this.rb_ShowAll);
            this.pnlText.Controls.Add(this.label3);
            this.pnlText.Controls.Add(this.label2);
            this.pnlText.Controls.Add(this.label1);
            this.pnlText.Controls.Add(this.label59);
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlText.Location = new System.Drawing.Point(0, 54);
            this.pnlText.Name = "pnlText";
            this.pnlText.Padding = new System.Windows.Forms.Padding(3);
            this.pnlText.Size = new System.Drawing.Size(1101, 39);
            this.pnlText.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 4);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.label4.Size = new System.Drawing.Size(309, 31);
            this.label4.TabIndex = 241;
            this.label4.Text = " Cleargage Credit Distribution Work List";
            // 
            // rb_ShowOneByOne
            // 
            this.rb_ShowOneByOne.AutoSize = true;
            this.rb_ShowOneByOne.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_ShowOneByOne.Location = new System.Drawing.Point(431, 11);
            this.rb_ShowOneByOne.Name = "rb_ShowOneByOne";
            this.rb_ShowOneByOne.Size = new System.Drawing.Size(127, 18);
            this.rb_ShowOneByOne.TabIndex = 240;
            this.rb_ShowOneByOne.Text = "Show One by One";
            this.rb_ShowOneByOne.UseVisualStyleBackColor = true;
            this.rb_ShowOneByOne.CheckedChanged += new System.EventHandler(this.rb_ShowOneByOne_CheckedChanged);
            // 
            // rb_ShowAll
            // 
            this.rb_ShowAll.AutoSize = true;
            this.rb_ShowAll.Checked = true;
            this.rb_ShowAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_ShowAll.Location = new System.Drawing.Point(339, 11);
            this.rb_ShowAll.Name = "rb_ShowAll";
            this.rb_ShowAll.Size = new System.Drawing.Size(79, 18);
            this.rb_ShowAll.TabIndex = 239;
            this.rb_ShowAll.TabStop = true;
            this.rb_ShowAll.Text = "Show All";
            this.rb_ShowAll.UseVisualStyleBackColor = true;
            this.rb_ShowAll.CheckedChanged += new System.EventHandler(this.rb_ShowAll_CheckedChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1093, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1093, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(1097, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 33);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 33);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // pnl_CleargagePaymentVoidList
            // 
            this.pnl_CleargagePaymentVoidList.Controls.Add(this.C1CleargageVoid);
            this.pnl_CleargagePaymentVoidList.Controls.Add(this.label13);
            this.pnl_CleargagePaymentVoidList.Controls.Add(this.label15);
            this.pnl_CleargagePaymentVoidList.Controls.Add(this.label16);
            this.pnl_CleargagePaymentVoidList.Controls.Add(this.label17);
            this.pnl_CleargagePaymentVoidList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_CleargagePaymentVoidList.Location = new System.Drawing.Point(0, 93);
            this.pnl_CleargagePaymentVoidList.Name = "pnl_CleargagePaymentVoidList";
            this.pnl_CleargagePaymentVoidList.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnl_CleargagePaymentVoidList.Size = new System.Drawing.Size(1101, 633);
            this.pnl_CleargagePaymentVoidList.TabIndex = 118;
            // 
            // C1CleargageVoid
            // 
            this.C1CleargageVoid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.C1CleargageVoid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1CleargageVoid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1CleargageVoid.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.C1CleargageVoid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1CleargageVoid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1CleargageVoid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1CleargageVoid.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.C1CleargageVoid.Location = new System.Drawing.Point(4, 1);
            this.C1CleargageVoid.Name = "C1CleargageVoid";
            this.C1CleargageVoid.Rows.Count = 1;
            this.C1CleargageVoid.Rows.DefaultSize = 19;
            this.C1CleargageVoid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.C1CleargageVoid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.C1CleargageVoid.Size = new System.Drawing.Size(1093, 628);
            this.C1CleargageVoid.StyleInfo = resources.GetString("C1CleargageVoid.StyleInfo");
            this.C1CleargageVoid.TabIndex = 119;
            this.C1CleargageVoid.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1CleargageVoid_BeforeEdit);
            this.C1CleargageVoid.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.C1CleargageVoid_KeyPressEdit);
            this.C1CleargageVoid.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1CleargageVoid_CellChanged);
            this.C1CleargageVoid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.C1CleargageVoid_KeyUp);
            this.C1CleargageVoid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1CleargageVoid_MouseMove);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(1097, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 628);
            this.label13.TabIndex = 118;
            this.label13.Text = "label13";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Location = new System.Drawing.Point(3, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 628);
            this.label15.TabIndex = 117;
            this.label15.Text = "label15";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 629);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1095, 1);
            this.label16.TabIndex = 116;
            this.label16.Text = "label1";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1095, 1);
            this.label17.TabIndex = 115;
            this.label17.Text = "label17";
            // 
            // frmCleargagePaymentVoid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1101, 726);
            this.Controls.Add(this.pnl_CleargagePaymentVoidList);
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCleargagePaymentVoid";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cleargage Credit Distribution List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCleargagePaymentVoid_FormClosed);
            this.Load += new System.EventHandler(this.frmCleargagePaymentVoid_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tls_Strip.ResumeLayout(false);
            this.tls_Strip.PerformLayout();
            this.pnlText.ResumeLayout(false);
            this.pnlText.PerformLayout();
            this.pnl_CleargagePaymentVoidList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1CleargageVoid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private gloGlobal.gloToolStripIgnoreFocus tls_Strip;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Next;
        internal System.Windows.Forms.ToolStripButton tsb_Refresh;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rb_ShowOneByOne;
        private System.Windows.Forms.RadioButton rb_ShowAll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Panel pnl_CleargagePaymentVoidList;
        private C1.Win.C1FlexGrid.C1FlexGrid C1CleargageVoid;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ToolStripButton tsb_AutoDistributeCleargageVoidPayment;
    }
}