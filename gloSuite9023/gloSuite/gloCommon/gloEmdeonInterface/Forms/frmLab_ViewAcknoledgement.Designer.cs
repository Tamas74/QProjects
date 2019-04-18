namespace gloEmdeonInterface.Forms
{
    partial class frmLab_ViewAcknoledgement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLab_ViewAcknoledgement));
            this.ImgList_Pages = new System.Windows.Forms.ImageList(this.components);
            this.pnl_tlsp = new System.Windows.Forms.Panel();
            this.tlsp_Lab_ViewAcknoledgement = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnOk = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlDocument = new System.Windows.Forms.Panel();
            this.txtReviewed = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.pnlFileName = new System.Windows.Forms.Panel();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.pnl_tlsp.SuspendLayout();
            this.tlsp_Lab_ViewAcknoledgement.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlDocument.SuspendLayout();
            this.pnlFileName.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImgList_Pages
            // 
            this.ImgList_Pages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgList_Pages.ImageStream")));
            this.ImgList_Pages.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgList_Pages.Images.SetKeyName(0, "");
            this.ImgList_Pages.Images.SetKeyName(1, "");
            // 
            // pnl_tlsp
            // 
            this.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp.Controls.Add(this.tlsp_Lab_ViewAcknoledgement);
            this.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnl_tlsp.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp.Name = "pnl_tlsp";
            this.pnl_tlsp.Size = new System.Drawing.Size(458, 54);
            this.pnl_tlsp.TabIndex = 1;
            // 
            // tlsp_Lab_ViewAcknoledgement
            // 
            this.tlsp_Lab_ViewAcknoledgement.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_Lab_ViewAcknoledgement.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsp_Lab_ViewAcknoledgement.BackgroundImage")));
            this.tlsp_Lab_ViewAcknoledgement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_Lab_ViewAcknoledgement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_Lab_ViewAcknoledgement.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_Lab_ViewAcknoledgement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnOk});
            this.tlsp_Lab_ViewAcknoledgement.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_Lab_ViewAcknoledgement.Location = new System.Drawing.Point(0, 0);
            this.tlsp_Lab_ViewAcknoledgement.Name = "tlsp_Lab_ViewAcknoledgement";
            this.tlsp_Lab_ViewAcknoledgement.Size = new System.Drawing.Size(458, 53);
            this.tlsp_Lab_ViewAcknoledgement.TabIndex = 1;
            this.tlsp_Lab_ViewAcknoledgement.TabStop = true;
            this.tlsp_Lab_ViewAcknoledgement.Text = "toolStrip1";
            this.tlsp_Lab_ViewAcknoledgement.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlsp_Lab_ViewAcknoledgement_ItemClicked);
            // 
            // ts_btnOk
            // 
            this.ts_btnOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnOk.Image")));
            this.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnOk.Name = "ts_btnOk";
            this.ts_btnOk.Size = new System.Drawing.Size(43, 50);
            this.ts_btnOk.Tag = "Ok";
            this.ts_btnOk.Text = "&Close";
            this.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnOk.ToolTipText = "Close";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.pnlDocument);
            this.pnlMain.Controls.Add(this.pnlFileName);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(458, 187);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlDocument
            // 
            this.pnlDocument.Controls.Add(this.txtReviewed);
            this.pnlDocument.Controls.Add(this.Label5);
            this.pnlDocument.Controls.Add(this.Label6);
            this.pnlDocument.Controls.Add(this.Label7);
            this.pnlDocument.Controls.Add(this.Label8);
            this.pnlDocument.Controls.Add(this.txtuser);
            this.pnlDocument.Controls.Add(this.txtComments);
            this.pnlDocument.Controls.Add(this.Label4);
            this.pnlDocument.Controls.Add(this.Label3);
            this.pnlDocument.Controls.Add(this.Label2);
            this.pnlDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDocument.Location = new System.Drawing.Point(0, 35);
            this.pnlDocument.Name = "pnlDocument";
            this.pnlDocument.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlDocument.Size = new System.Drawing.Size(458, 152);
            this.pnlDocument.TabIndex = 1;
            // 
            // txtReviewed
            // 
            this.txtReviewed.BackColor = System.Drawing.Color.White;
            this.txtReviewed.ForeColor = System.Drawing.Color.Black;
            this.txtReviewed.Location = new System.Drawing.Point(109, 34);
            this.txtReviewed.Name = "txtReviewed";
            this.txtReviewed.ReadOnly = true;
            this.txtReviewed.Size = new System.Drawing.Size(187, 22);
            this.txtReviewed.TabIndex = 1;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 148);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(450, 1);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 148);
            this.Label6.TabIndex = 11;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(454, 1);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 148);
            this.Label7.TabIndex = 10;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(452, 1);
            this.Label8.TabIndex = 9;
            this.Label8.Text = "label1";
            // 
            // txtuser
            // 
            this.txtuser.BackColor = System.Drawing.Color.White;
            this.txtuser.ForeColor = System.Drawing.Color.Black;
            this.txtuser.Location = new System.Drawing.Point(109, 7);
            this.txtuser.Name = "txtuser";
            this.txtuser.ReadOnly = true;
            this.txtuser.Size = new System.Drawing.Size(327, 22);
            this.txtuser.TabIndex = 0;
            // 
            // txtComments
            // 
            this.txtComments.BackColor = System.Drawing.Color.White;
            this.txtComments.ForeColor = System.Drawing.Color.Black;
            this.txtComments.Location = new System.Drawing.Point(109, 62);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ReadOnly = true;
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComments.Size = new System.Drawing.Size(327, 78);
            this.txtComments.TabIndex = 2;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label4.Location = new System.Drawing.Point(33, 64);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(73, 14);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Comments :";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(65, 38);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(41, 14);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "Date :";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(21, 11);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(85, 14);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Reviewed by :";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlFileName
            // 
            this.pnlFileName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFileName.Controls.Add(this.Panel1);
            this.pnlFileName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFileName.Location = new System.Drawing.Point(0, 0);
            this.pnlFileName.Name = "pnlFileName";
            this.pnlFileName.Padding = new System.Windows.Forms.Padding(3);
            this.pnlFileName.Size = new System.Drawing.Size(458, 35);
            this.pnlFileName.TabIndex = 0;
            // 
            // Panel1
            // 
            this.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel1.Controls.Add(this.txtFileName);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.Label9);
            this.Panel1.Controls.Add(this.Label10);
            this.Panel1.Controls.Add(this.Label11);
            this.Panel1.Controls.Add(this.Label12);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(3, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(452, 29);
            this.Panel1.TabIndex = 1;
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.Color.White;
            this.txtFileName.ForeColor = System.Drawing.Color.Black;
            this.txtFileName.Location = new System.Drawing.Point(106, 3);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(327, 22);
            this.txtFileName.TabIndex = 2;
            // 
            // Label1
            // 
            this.Label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(1, 1);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(102, 27);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "  Order Name :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label9.Location = new System.Drawing.Point(1, 28);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(450, 1);
            this.Label9.TabIndex = 12;
            this.Label9.Text = "label2";
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.Location = new System.Drawing.Point(0, 1);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(1, 28);
            this.Label10.TabIndex = 11;
            this.Label10.Text = "label4";
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label11.Location = new System.Drawing.Point(451, 1);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(1, 28);
            this.Label11.TabIndex = 10;
            this.Label11.Text = "label3";
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(0, 0);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(452, 1);
            this.Label12.TabIndex = 9;
            this.Label12.Text = "label1";
            // 
            // frmLab_ViewAcknoledgement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(458, 241);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnl_tlsp);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLab_ViewAcknoledgement";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Acknowledgment";
            this.Load += new System.EventHandler(this.frmLab_ViewAcknoledgement_Load);
            this.pnl_tlsp.ResumeLayout(false);
            this.pnl_tlsp.PerformLayout();
            this.tlsp_Lab_ViewAcknoledgement.ResumeLayout(false);
            this.tlsp_Lab_ViewAcknoledgement.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlDocument.ResumeLayout(false);
            this.pnlDocument.PerformLayout();
            this.pnlFileName.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ImageList ImgList_Pages;
        private System.Windows.Forms.Panel pnl_tlsp;
        internal System.Windows.Forms.Panel pnlMain;
        internal System.Windows.Forms.Panel pnlDocument;
        internal System.Windows.Forms.TextBox txtReviewed;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox txtuser;
        internal System.Windows.Forms.TextBox txtComments;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Panel pnlFileName;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.TextBox txtFileName;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.Label Label10;
        private System.Windows.Forms.Label Label11;
        private System.Windows.Forms.Label Label12;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_Lab_ViewAcknoledgement;
        private System.Windows.Forms.ToolStripButton ts_btnOk;
    }
}