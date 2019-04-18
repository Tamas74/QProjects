namespace gloOffice
{
    partial class frmUpdateTemplates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateTemplates));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_UpdateTemplate = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_SelectAll = new System.Windows.Forms.ToolStripButton();
            this.tlb_ClearAll = new System.Windows.Forms.ToolStripButton();
            this.tlb_Update = new System.Windows.Forms.ToolStripButton();
            this.tlb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.trvCategory = new System.Windows.Forms.TreeView();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.pgrbarStatus = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.wdTemplate = new AxDSOFramer.AxFramerControl();
            this.Panel22 = new System.Windows.Forms.Panel();
            this.pnlTemplateName = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_search = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_UpdateTemplate.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wdTemplate)).BeginInit();
            this.Panel22.SuspendLayout();
            this.pnlTemplateName.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_UpdateTemplate);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(369, 54);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // tls_UpdateTemplate
            // 
            this.tls_UpdateTemplate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_UpdateTemplate.BackgroundImage")));
            this.tls_UpdateTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_UpdateTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_UpdateTemplate.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_UpdateTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_SelectAll,
            this.tlb_ClearAll,
            this.tlb_Update,
            this.tlb_Close});
            this.tls_UpdateTemplate.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_UpdateTemplate.Location = new System.Drawing.Point(0, 0);
            this.tls_UpdateTemplate.Name = "tls_UpdateTemplate";
            this.tls_UpdateTemplate.Size = new System.Drawing.Size(369, 53);
            this.tls_UpdateTemplate.TabIndex = 10;
            this.tls_UpdateTemplate.Text = "toolStrip1";
            // 
            // tlb_SelectAll
            // 
            this.tlb_SelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_SelectAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_SelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tlb_SelectAll.Image")));
            this.tlb_SelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_SelectAll.Name = "tlb_SelectAll";
            this.tlb_SelectAll.Size = new System.Drawing.Size(67, 50);
            this.tlb_SelectAll.Tag = "SelectAll";
            this.tlb_SelectAll.Text = "Select &All";
            this.tlb_SelectAll.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.tlb_SelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_SelectAll.Click += new System.EventHandler(this.tlb_SelectAll_Click);
            // 
            // tlb_ClearAll
            // 
            this.tlb_ClearAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_ClearAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_ClearAll.Image = ((System.Drawing.Image)(resources.GetObject("tlb_ClearAll.Image")));
            this.tlb_ClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_ClearAll.Name = "tlb_ClearAll";
            this.tlb_ClearAll.Size = new System.Drawing.Size(60, 50);
            this.tlb_ClearAll.Tag = "ClearAll";
            this.tlb_ClearAll.Text = "C&lear All";
            this.tlb_ClearAll.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.tlb_ClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_ClearAll.Visible = false;
            this.tlb_ClearAll.Click += new System.EventHandler(this.tlb_ClearAll_Click);
            // 
            // tlb_Update
            // 
            this.tlb_Update.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Update.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Update.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Update.Image")));
            this.tlb_Update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Update.Name = "tlb_Update";
            this.tlb_Update.Size = new System.Drawing.Size(55, 50);
            this.tlb_Update.Tag = "Update";
            this.tlb_Update.Text = "&Update";
            this.tlb_Update.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.tlb_Update.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Update.Click += new System.EventHandler(this.tlb_Update_Click);
            // 
            // tlb_Close
            // 
            this.tlb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Close.Image")));
            this.tlb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Close.Name = "tlb_Close";
            this.tlb_Close.Size = new System.Drawing.Size(43, 50);
            this.tlb_Close.Tag = "Close";
            this.tlb_Close.Text = "&Close";
            this.tlb_Close.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.tlb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Close.Click += new System.EventHandler(this.tlb_Close_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.trvCategory);
            this.pnlMain.Controls.Add(this.lbl_BottomBrd);
            this.pnlMain.Controls.Add(this.lbl_LeftBrd);
            this.pnlMain.Controls.Add(this.lbl_RightBrd);
            this.pnlMain.Controls.Add(this.lbl_TopBrd);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlMain.Location = new System.Drawing.Point(0, 82);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Size = new System.Drawing.Size(369, 246);
            this.pnlMain.TabIndex = 0;
            // 
            // trvCategory
            // 
            this.trvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCategory.CheckBoxes = true;
            this.trvCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCategory.Location = new System.Drawing.Point(4, 1);
            this.trvCategory.Name = "trvCategory";
            this.trvCategory.ShowLines = false;
            this.trvCategory.ShowPlusMinus = false;
            this.trvCategory.ShowRootLines = false;
            this.trvCategory.Size = new System.Drawing.Size(361, 241);
            this.trvCategory.TabIndex = 9;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 242);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(361, 1);
            this.lbl_BottomBrd.TabIndex = 8;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 242);
            this.lbl_LeftBrd.TabIndex = 7;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(365, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 242);
            this.lbl_RightBrd.TabIndex = 6;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(363, 1);
            this.lbl_TopBrd.TabIndex = 5;
            this.lbl_TopBrd.Text = "label1";
            // 
            // pgrbarStatus
            // 
            this.pgrbarStatus.BackColor = System.Drawing.Color.White;
            this.pgrbarStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pgrbarStatus.ForeColor = System.Drawing.Color.LimeGreen;
            this.pgrbarStatus.Location = new System.Drawing.Point(1, 27);
            this.pgrbarStatus.Name = "pgrbarStatus";
            this.pgrbarStatus.Size = new System.Drawing.Size(361, 18);
            this.pgrbarStatus.Step = 1;
            this.pgrbarStatus.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgrbarStatus.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(5, 7);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 14);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // wdTemplate
            // 
            this.wdTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wdTemplate.Enabled = true;
            this.wdTemplate.Location = new System.Drawing.Point(0, 0);
            this.wdTemplate.Name = "wdTemplate";
            this.wdTemplate.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wdTemplate.OcxState")));
            this.wdTemplate.Size = new System.Drawing.Size(369, 377);
            this.wdTemplate.TabIndex = 15;
            this.wdTemplate.Visible = false;
            this.wdTemplate.OnDocumentOpened += new AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEventHandler(this.wdTemplate_OnDocumentOpened);
            this.wdTemplate.OnDocumentClosed += new System.EventHandler(this.wdTemplate_OnDocumentClosed);
            // 
            // Panel22
            // 
            this.Panel22.Controls.Add(this.pnlTemplateName);
            this.Panel22.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel22.Location = new System.Drawing.Point(0, 54);
            this.Panel22.Name = "Panel22";
            this.Panel22.Padding = new System.Windows.Forms.Padding(3);
            this.Panel22.Size = new System.Drawing.Size(369, 28);
            this.Panel22.TabIndex = 83;
            // 
            // pnlTemplateName
            // 
            this.pnlTemplateName.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.pnlTemplateName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTemplateName.Controls.Add(this.label9);
            this.pnlTemplateName.Controls.Add(this.label10);
            this.pnlTemplateName.Controls.Add(this.label11);
            this.pnlTemplateName.Controls.Add(this.label12);
            this.pnlTemplateName.Controls.Add(this.lbl_search);
            this.pnlTemplateName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTemplateName.Location = new System.Drawing.Point(3, 3);
            this.pnlTemplateName.Name = "pnlTemplateName";
            this.pnlTemplateName.Size = new System.Drawing.Size(363, 22);
            this.pnlTemplateName.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(1, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(361, 1);
            this.label9.TabIndex = 12;
            this.label9.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 21);
            this.label10.TabIndex = 11;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(362, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 21);
            this.label11.TabIndex = 10;
            this.label11.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(363, 1);
            this.label12.TabIndex = 9;
            this.label12.Text = "label1";
            // 
            // lbl_search
            // 
            this.lbl_search.BackColor = System.Drawing.Color.Transparent;
            this.lbl_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_search.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_search.Location = new System.Drawing.Point(0, 0);
            this.lbl_search.Name = "lbl_search";
            this.lbl_search.Size = new System.Drawing.Size(363, 22);
            this.lbl_search.TabIndex = 1;
            this.lbl_search.Text = "  Template Category";
            this.lbl_search.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 328);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(369, 49);
            this.panel1.TabIndex = 84;
            // 
            // panel2
            // 
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.pgrbarStatus);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(363, 46);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 1);
            this.label1.TabIndex = 12;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 45);
            this.label2.TabIndex = 11;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(362, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 45);
            this.label3.TabIndex = 10;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(363, 1);
            this.label4.TabIndex = 9;
            this.label4.Text = "label1";
            // 
            // frmUpdateTemplates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(369, 377);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Panel22);
            this.Controls.Add(this.pnlToolStrip);
            this.Controls.Add(this.wdTemplate);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdateTemplates";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Templates";
            this.Load += new System.EventHandler(this.frmUpdateTemplates_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_UpdateTemplate.ResumeLayout(false);
            this.tls_UpdateTemplate.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wdTemplate)).EndInit();
            this.Panel22.ResumeLayout(false);
            this.pnlTemplateName.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_UpdateTemplate;
        private System.Windows.Forms.ToolStripButton tlb_Update;
        private System.Windows.Forms.ToolStripButton tlb_Close;
        internal System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        internal System.Windows.Forms.ProgressBar pgrbarStatus;
        internal System.Windows.Forms.Label lblStatus;
        internal AxDSOFramer.AxFramerControl wdTemplate;
        internal System.Windows.Forms.Panel Panel22;
        private System.Windows.Forms.Panel pnlTemplateName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_search;
        private System.Windows.Forms.TreeView trvCategory;
        internal System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton tlb_SelectAll;
        private System.Windows.Forms.ToolStripButton tlb_ClearAll;
    }
}