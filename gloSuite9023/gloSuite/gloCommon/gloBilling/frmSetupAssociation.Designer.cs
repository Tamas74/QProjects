namespace gloBilling
{
    partial class frmSetupAssociation
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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.trvCPT = new System.Windows.Forms.TreeView();
            this.pnlLeftTop = new System.Windows.Forms.Panel();
            this.rdbCPTDesc = new System.Windows.Forms.RadioButton();
            this.rdbCPTCode = new System.Windows.Forms.RadioButton();
            this.txtSearchCPT = new System.Windows.Forms.TextBox();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.trvAssociates = new System.Windows.Forms.TreeView();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnICD9 = new System.Windows.Forms.Button();
            this.pnlRightTop = new System.Windows.Forms.Panel();
            this.rdbICD9Desc = new System.Windows.Forms.RadioButton();
            this.rdbICD9Code = new System.Windows.Forms.RadioButton();
            this.txtSearchICD9 = new System.Windows.Forms.TextBox();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.trvAssociation = new System.Windows.Forms.TreeView();
            this.pnlMiddleTop = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.pnlLeft.SuspendLayout();
            this.pnlLeftTop.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlRightTop.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.trvCPT);
            this.pnlLeft.Controls.Add(this.pnlLeftTop);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 24);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(238, 588);
            this.pnlLeft.TabIndex = 1;
            // 
            // trvCPT
            // 
            this.trvCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCPT.Location = new System.Drawing.Point(0, 65);
            this.trvCPT.Name = "trvCPT";
            this.trvCPT.Size = new System.Drawing.Size(238, 523);
            this.trvCPT.TabIndex = 1;
            this.trvCPT.DoubleClick += new System.EventHandler(this.trvCPT_DoubleClick);
            // 
            // pnlLeftTop
            // 
            this.pnlLeftTop.Controls.Add(this.rdbCPTDesc);
            this.pnlLeftTop.Controls.Add(this.rdbCPTCode);
            this.pnlLeftTop.Controls.Add(this.txtSearchCPT);
            this.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLeftTop.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftTop.Name = "pnlLeftTop";
            this.pnlLeftTop.Size = new System.Drawing.Size(238, 65);
            this.pnlLeftTop.TabIndex = 0;
            // 
            // rdbCPTDesc
            // 
            this.rdbCPTDesc.AutoSize = true;
            this.rdbCPTDesc.Location = new System.Drawing.Point(94, 10);
            this.rdbCPTDesc.Name = "rdbCPTDesc";
            this.rdbCPTDesc.Size = new System.Drawing.Size(78, 17);
            this.rdbCPTDesc.TabIndex = 2;
            this.rdbCPTDesc.Text = "Description";
            this.rdbCPTDesc.UseVisualStyleBackColor = true;
            // 
            // rdbCPTCode
            // 
            this.rdbCPTCode.AutoSize = true;
            this.rdbCPTCode.Checked = true;
            this.rdbCPTCode.Location = new System.Drawing.Point(13, 10);
            this.rdbCPTCode.Name = "rdbCPTCode";
            this.rdbCPTCode.Size = new System.Drawing.Size(50, 17);
            this.rdbCPTCode.TabIndex = 1;
            this.rdbCPTCode.TabStop = true;
            this.rdbCPTCode.Text = "Code";
            this.rdbCPTCode.UseVisualStyleBackColor = true;
            // 
            // txtSearchCPT
            // 
            this.txtSearchCPT.Location = new System.Drawing.Point(13, 33);
            this.txtSearchCPT.Name = "txtSearchCPT";
            this.txtSearchCPT.Size = new System.Drawing.Size(211, 21);
            this.txtSearchCPT.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.trvAssociates);
            this.pnlRight.Controls.Add(this.btnModifier);
            this.pnlRight.Controls.Add(this.btnICD9);
            this.pnlRight.Controls.Add(this.pnlRightTop);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(659, 24);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(248, 588);
            this.pnlRight.TabIndex = 2;
            // 
            // trvAssociates
            // 
            this.trvAssociates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvAssociates.Location = new System.Drawing.Point(0, 88);
            this.trvAssociates.Name = "trvAssociates";
            this.trvAssociates.Size = new System.Drawing.Size(248, 477);
            this.trvAssociates.TabIndex = 1;
            this.trvAssociates.DoubleClick += new System.EventHandler(this.trvAssociates_DoubleClick);
            // 
            // btnModifier
            // 
            this.btnModifier.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifier.Location = new System.Drawing.Point(0, 565);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(248, 23);
            this.btnModifier.TabIndex = 3;
            this.btnModifier.Text = "&Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            // 
            // btnICD9
            // 
            this.btnICD9.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnICD9.Location = new System.Drawing.Point(0, 65);
            this.btnICD9.Name = "btnICD9";
            this.btnICD9.Size = new System.Drawing.Size(248, 23);
            this.btnICD9.TabIndex = 2;
            this.btnICD9.Text = "&ICD9";
            this.btnICD9.UseVisualStyleBackColor = true;
            // 
            // pnlRightTop
            // 
            this.pnlRightTop.Controls.Add(this.rdbICD9Desc);
            this.pnlRightTop.Controls.Add(this.rdbICD9Code);
            this.pnlRightTop.Controls.Add(this.txtSearchICD9);
            this.pnlRightTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRightTop.Location = new System.Drawing.Point(0, 0);
            this.pnlRightTop.Name = "pnlRightTop";
            this.pnlRightTop.Size = new System.Drawing.Size(248, 65);
            this.pnlRightTop.TabIndex = 0;
            // 
            // rdbICD9Desc
            // 
            this.rdbICD9Desc.AutoSize = true;
            this.rdbICD9Desc.Location = new System.Drawing.Point(95, 10);
            this.rdbICD9Desc.Name = "rdbICD9Desc";
            this.rdbICD9Desc.Size = new System.Drawing.Size(78, 17);
            this.rdbICD9Desc.TabIndex = 3;
            this.rdbICD9Desc.Text = "Description";
            this.rdbICD9Desc.UseVisualStyleBackColor = true;
            // 
            // rdbICD9Code
            // 
            this.rdbICD9Code.AutoSize = true;
            this.rdbICD9Code.Checked = true;
            this.rdbICD9Code.Location = new System.Drawing.Point(15, 10);
            this.rdbICD9Code.Name = "rdbICD9Code";
            this.rdbICD9Code.Size = new System.Drawing.Size(50, 17);
            this.rdbICD9Code.TabIndex = 2;
            this.rdbICD9Code.TabStop = true;
            this.rdbICD9Code.Text = "Code";
            this.rdbICD9Code.UseVisualStyleBackColor = true;
            // 
            // txtSearchICD9
            // 
            this.txtSearchICD9.Location = new System.Drawing.Point(15, 33);
            this.txtSearchICD9.Name = "txtSearchICD9";
            this.txtSearchICD9.Size = new System.Drawing.Size(211, 21);
            this.txtSearchICD9.TabIndex = 1;
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.trvAssociation);
            this.pnlMiddle.Controls.Add(this.pnlMiddleTop);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Location = new System.Drawing.Point(238, 24);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Size = new System.Drawing.Size(421, 588);
            this.pnlMiddle.TabIndex = 3;
            // 
            // trvAssociation
            // 
            this.trvAssociation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvAssociation.Location = new System.Drawing.Point(0, 65);
            this.trvAssociation.Name = "trvAssociation";
            this.trvAssociation.Size = new System.Drawing.Size(421, 523);
            this.trvAssociation.TabIndex = 2;
            // 
            // pnlMiddleTop
            // 
            this.pnlMiddleTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMiddleTop.Location = new System.Drawing.Point(0, 0);
            this.pnlMiddleTop.Name = "pnlMiddleTop";
            this.pnlMiddleTop.Size = new System.Drawing.Size(421, 65);
            this.pnlMiddleTop.TabIndex = 3;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.toolStrip1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(907, 24);
            this.pnlTop.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(907, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // frmSetupAssociation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(907, 612);
            this.Controls.Add(this.pnlMiddle);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DarkBlue;
            this.Name = "frmSetupAssociation";
            this.ShowInTaskbar = false;
            this.Text = "Association";
            this.Load += new System.EventHandler(this.frmSetupAssociation_Load);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeftTop.ResumeLayout(false);
            this.pnlLeftTop.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRightTop.ResumeLayout(false);
            this.pnlRightTop.PerformLayout();
            this.pnlMiddle.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlMiddle;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnICD9;
        private System.Windows.Forms.TreeView trvCPT;
        private System.Windows.Forms.Panel pnlLeftTop;
        private System.Windows.Forms.RadioButton rdbCPTDesc;
        private System.Windows.Forms.RadioButton rdbCPTCode;
        private System.Windows.Forms.TextBox txtSearchCPT;
        private System.Windows.Forms.TreeView trvAssociates;
        private System.Windows.Forms.Panel pnlRightTop;
        private System.Windows.Forms.RadioButton rdbICD9Desc;
        private System.Windows.Forms.RadioButton rdbICD9Code;
        private System.Windows.Forms.TextBox txtSearchICD9;
        private System.Windows.Forms.Panel pnlTop;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        private System.Windows.Forms.TreeView trvAssociation;
        private System.Windows.Forms.Panel pnlMiddleTop;
    }
}