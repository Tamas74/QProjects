﻿namespace gloBilling.Collections
{
    partial class frmBatchFollowUpStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatchFollowUpStatus));
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.tls_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsbtn_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tlsOK = new System.Windows.Forms.ToolStripButton();
            this.pnlAccountFollowUp = new System.Windows.Forms.Panel();
            this.c1AccountFollowUp = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAccounts = new System.Windows.Forms.Label();
            this.pnlClaimFollowUp = new System.Windows.Forms.Panel();
            this.c1ClaimFollowUp = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblClaims = new System.Windows.Forms.Label();
            this.c1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlToolstrip.SuspendLayout();
            this.tls_Main.SuspendLayout();
            this.pnlAccountFollowUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1AccountFollowUp)).BeginInit();
            this.pnlClaimFollowUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ClaimFollowUp)).BeginInit();
            this.pnlMessage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.AutoSize = true;
            this.pnlToolstrip.Controls.Add(this.tls_Main);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(840, 53);
            this.pnlToolstrip.TabIndex = 28;
            // 
            // tls_Main
            // 
            this.tls_Main.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Main.BackgroundImage")));
            this.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsbtn_Cancel,
            this.tlsOK});
            this.tls_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tls_Main.Location = new System.Drawing.Point(0, 0);
            this.tls_Main.Name = "tls_Main";
            this.tls_Main.Size = new System.Drawing.Size(840, 53);
            this.tls_Main.TabIndex = 3;
            this.tls_Main.Text = "ToolStrip1";
            // 
            // tlsbtn_Cancel
            // 
            this.tlsbtn_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtn_Cancel.Image")));
            this.tlsbtn_Cancel.Name = "tlsbtn_Cancel";
            this.tlsbtn_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tlsbtn_Cancel.Tag = "Close";
            this.tlsbtn_Cancel.Text = "&Close";
            this.tlsbtn_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtn_Cancel.ToolTipText = "Close";
            this.tlsbtn_Cancel.Click += new System.EventHandler(this.tlsbtn_Cancel_Click);
            // 
            // tlsOK
            // 
            this.tlsOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsOK.Image = ((System.Drawing.Image)(resources.GetObject("tlsOK.Image")));
            this.tlsOK.Name = "tlsOK";
            this.tlsOK.Size = new System.Drawing.Size(81, 50);
            this.tlsOK.Tag = "Save&Close";
            this.tlsOK.Text = "&Save&&Close";
            this.tlsOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsOK.ToolTipText = "Close";
            this.tlsOK.Visible = false;
            this.tlsOK.Click += new System.EventHandler(this.tlsOK_Click);
            // 
            // pnlAccountFollowUp
            // 
            this.pnlAccountFollowUp.Controls.Add(this.c1AccountFollowUp);
            this.pnlAccountFollowUp.Controls.Add(this.label1);
            this.pnlAccountFollowUp.Controls.Add(this.label2);
            this.pnlAccountFollowUp.Controls.Add(this.label59);
            this.pnlAccountFollowUp.Controls.Add(this.label3);
            this.pnlAccountFollowUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAccountFollowUp.Location = new System.Drawing.Point(0, 83);
            this.pnlAccountFollowUp.Name = "pnlAccountFollowUp";
            this.pnlAccountFollowUp.Padding = new System.Windows.Forms.Padding(3);
            this.pnlAccountFollowUp.Size = new System.Drawing.Size(840, 441);
            this.pnlAccountFollowUp.TabIndex = 29;
            // 
            // c1AccountFollowUp
            // 
            this.c1AccountFollowUp.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1AccountFollowUp.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None;
            this.c1AccountFollowUp.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1AccountFollowUp.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1AccountFollowUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1AccountFollowUp.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1AccountFollowUp.ColumnInfo = "1,0,0,0,0,105,Columns:";
            this.c1AccountFollowUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1AccountFollowUp.ExtendLastCol = true;
            this.c1AccountFollowUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1AccountFollowUp.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1AccountFollowUp.Location = new System.Drawing.Point(4, 4);
            this.c1AccountFollowUp.Name = "c1AccountFollowUp";
            this.c1AccountFollowUp.Padding = new System.Windows.Forms.Padding(3);
            this.c1AccountFollowUp.Rows.Count = 1;
            this.c1AccountFollowUp.Rows.DefaultSize = 21;
            this.c1AccountFollowUp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1AccountFollowUp.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1AccountFollowUp.Size = new System.Drawing.Size(832, 433);
            this.c1AccountFollowUp.StyleInfo = resources.GetString("c1AccountFollowUp.StyleInfo");
            this.c1AccountFollowUp.TabIndex = 65;
            this.c1AccountFollowUp.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1AccountFollowUp_MouseMove);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(836, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 433);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(833, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 434);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(3, 437);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(834, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // lblAccounts
            // 
            this.lblAccounts.BackColor = System.Drawing.Color.Transparent;
            this.lblAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAccounts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccounts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.lblAccounts.Location = new System.Drawing.Point(0, 0);
            this.lblAccounts.Name = "lblAccounts";
            this.lblAccounts.Padding = new System.Windows.Forms.Padding(8, 6, 0, 0);
            this.lblAccounts.Size = new System.Drawing.Size(834, 27);
            this.lblAccounts.TabIndex = 0;
            this.lblAccounts.Text = "Batch Action Status";
            // 
            // pnlClaimFollowUp
            // 
            this.pnlClaimFollowUp.Controls.Add(this.c1ClaimFollowUp);
            this.pnlClaimFollowUp.Controls.Add(this.label4);
            this.pnlClaimFollowUp.Controls.Add(this.label5);
            this.pnlClaimFollowUp.Controls.Add(this.label6);
            this.pnlClaimFollowUp.Controls.Add(this.label7);
            this.pnlClaimFollowUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlClaimFollowUp.Location = new System.Drawing.Point(0, 83);
            this.pnlClaimFollowUp.Name = "pnlClaimFollowUp";
            this.pnlClaimFollowUp.Padding = new System.Windows.Forms.Padding(3);
            this.pnlClaimFollowUp.Size = new System.Drawing.Size(840, 441);
            this.pnlClaimFollowUp.TabIndex = 30;
            // 
            // c1ClaimFollowUp
            // 
            this.c1ClaimFollowUp.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ClaimFollowUp.AllowEditing = false;
            this.c1ClaimFollowUp.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None;
            this.c1ClaimFollowUp.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1ClaimFollowUp.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1ClaimFollowUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ClaimFollowUp.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ClaimFollowUp.ColumnInfo = "1,0,0,0,0,105,Columns:";
            this.c1ClaimFollowUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ClaimFollowUp.ExtendLastCol = true;
            this.c1ClaimFollowUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ClaimFollowUp.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1ClaimFollowUp.Location = new System.Drawing.Point(4, 4);
            this.c1ClaimFollowUp.Name = "c1ClaimFollowUp";
            this.c1ClaimFollowUp.Padding = new System.Windows.Forms.Padding(3);
            this.c1ClaimFollowUp.Rows.Count = 1;
            this.c1ClaimFollowUp.Rows.DefaultSize = 21;
            this.c1ClaimFollowUp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ClaimFollowUp.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1ClaimFollowUp.Size = new System.Drawing.Size(832, 433);
            this.c1ClaimFollowUp.StyleInfo = resources.GetString("c1ClaimFollowUp.StyleInfo");
            this.c1ClaimFollowUp.TabIndex = 65;
            this.c1ClaimFollowUp.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1ClaimFollowUp_MouseMove);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(836, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 433);
            this.label4.TabIndex = 27;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(4, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(833, 1);
            this.label5.TabIndex = 28;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 434);
            this.label6.TabIndex = 26;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 437);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(834, 1);
            this.label7.TabIndex = 29;
            this.label7.Text = "label7";
            // 
            // pnlMessage
            // 
            this.pnlMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMessage.Controls.Add(this.panel1);
            this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMessage.Location = new System.Drawing.Point(0, 53);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlMessage.Size = new System.Drawing.Size(840, 30);
            this.pnlMessage.TabIndex = 67;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.lblAccounts);
            this.panel1.Controls.Add(this.lblClaims);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(834, 27);
            this.panel1.TabIndex = 34;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Location = new System.Drawing.Point(1, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(832, 1);
            this.label14.TabIndex = 30;
            this.label14.Text = "label14";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Location = new System.Drawing.Point(833, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 26);
            this.label16.TabIndex = 32;
            this.label16.Text = "label16";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(1, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(833, 1);
            this.label15.TabIndex = 31;
            this.label15.Text = "label15";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 27);
            this.label17.TabIndex = 33;
            this.label17.Text = "label17";
            // 
            // lblClaims
            // 
            this.lblClaims.BackColor = System.Drawing.Color.Transparent;
            this.lblClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaims.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.lblClaims.Location = new System.Drawing.Point(0, 0);
            this.lblClaims.Name = "lblClaims";
            this.lblClaims.Padding = new System.Windows.Forms.Padding(8, 6, 0, 0);
            this.lblClaims.Size = new System.Drawing.Size(834, 27);
            this.lblClaims.TabIndex = 1;
            this.lblClaims.Text = "Batch Action Status";
            // 
            // c1SuperTooltip1
            // 
            this.c1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmBatchFollowUpStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(840, 524);
            this.Controls.Add(this.pnlAccountFollowUp);
            this.Controls.Add(this.pnlClaimFollowUp);
            this.Controls.Add(this.pnlMessage);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBatchFollowUpStatus";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch FollowUp Status";
            this.Load += new System.EventHandler(this.frmBatchFollowUpStatus_Load);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.tls_Main.ResumeLayout(false);
            this.tls_Main.PerformLayout();
            this.pnlAccountFollowUp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1AccountFollowUp)).EndInit();
            this.pnlClaimFollowUp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ClaimFollowUp)).EndInit();
            this.pnlMessage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Panel pnlToolstrip;
        internal gloGlobal.gloToolStripIgnoreFocus tls_Main;
        internal System.Windows.Forms.ToolStripButton tlsbtn_Cancel;
        private System.Windows.Forms.Panel pnlAccountFollowUp;
        private C1.Win.C1FlexGrid.C1FlexGrid c1AccountFollowUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlClaimFollowUp;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ClaimFollowUp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private C1.Win.C1SuperTooltip.C1SuperTooltip c1SuperTooltip1;
        private System.Windows.Forms.Label lblAccounts;
        private System.Windows.Forms.Panel pnlMessage;
        private System.Windows.Forms.Label lblClaims;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ToolStripButton tlsOK;
    }
}