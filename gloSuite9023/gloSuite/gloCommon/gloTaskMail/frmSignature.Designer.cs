namespace gloTaskMail
{
    partial class frmSignature
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
                //try
                //{
                //    if (fontDialog1 != null)
                //    {

                //        fontDialog1.Dispose();
                //        fontDialog1 = null;
                //    }
                //}
                //catch
                //{
                //}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSignature));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lst_Signature = new System.Windows.Forms.ListBox();
            this.pnl_Toolstrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnl_lstBox = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.txtSignatureName = new System.Windows.Forms.TextBox();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btn_DeleteSign = new System.Windows.Forms.Button();
            this.btn_NewSign = new System.Windows.Forms.Button();
            this.btn_SaveAsSign = new System.Windows.Forms.Button();
            this.btn_SaveSign = new System.Windows.Forms.Button();
        //    this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.panel2.SuspendLayout();
            this.pnl_Toolstrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnl_lstBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 97);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(643, 336);
            this.panel2.TabIndex = 1;
            this.panel2.TabStop = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(4, 332);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(635, 1);
            this.label1.TabIndex = 8;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 329);
            this.label2.TabIndex = 7;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(639, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 329);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(637, 1);
            this.label4.TabIndex = 5;
            this.label4.Text = "label1";
            // 
            // lst_Signature
            // 
            this.lst_Signature.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lst_Signature.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_Signature.ForeColor = System.Drawing.Color.Black;
            this.lst_Signature.FormattingEnabled = true;
            this.lst_Signature.ItemHeight = 14;
            this.lst_Signature.Location = new System.Drawing.Point(647, 48);
            this.lst_Signature.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.lst_Signature.Name = "lst_Signature";
            this.lst_Signature.Size = new System.Drawing.Size(52, 14);
            this.lst_Signature.TabIndex = 6;
            this.lst_Signature.Visible = false;
            this.lst_Signature.SelectedIndexChanged += new System.EventHandler(this.lst_Signature_SelectedIndexChanged);
            // 
            // pnl_Toolstrip
            // 
            this.pnl_Toolstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Toolstrip.Controls.Add(this.ts_Commands);
            this.pnl_Toolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Toolstrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Toolstrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Toolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_Toolstrip.Name = "pnl_Toolstrip";
            this.pnl_Toolstrip.Size = new System.Drawing.Size(643, 54);
            this.pnl_Toolstrip.TabIndex = 22;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(643, 53);
            this.ts_Commands.TabIndex = 3;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "Sa&ve&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close\r\n";
            this.tsb_OK.Click += new System.EventHandler(this.tsb_OK_Click);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // pnl_lstBox
            // 
            this.pnl_lstBox.Controls.Add(this.lblUserName);
            this.pnl_lstBox.Controls.Add(this.lbl_BottomBrd);
            this.pnl_lstBox.Controls.Add(this.label5);
            this.pnl_lstBox.Controls.Add(this.lbl_LeftBrd);
            this.pnl_lstBox.Controls.Add(this.txtSignatureName);
            this.pnl_lstBox.Controls.Add(this.lbl_RightBrd);
            this.pnl_lstBox.Controls.Add(this.checkBox1);
            this.pnl_lstBox.Controls.Add(this.lbl_TopBrd);
            this.pnl_lstBox.Controls.Add(this.label20);
            this.pnl_lstBox.Controls.Add(this.lst_Signature);
            this.pnl_lstBox.Controls.Add(this.btn_DeleteSign);
            this.pnl_lstBox.Controls.Add(this.btn_NewSign);
            this.pnl_lstBox.Controls.Add(this.btn_SaveAsSign);
            this.pnl_lstBox.Controls.Add(this.btn_SaveSign);
            this.pnl_lstBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_lstBox.Location = new System.Drawing.Point(0, 54);
            this.pnl_lstBox.Name = "pnl_lstBox";
            this.pnl_lstBox.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_lstBox.Size = new System.Drawing.Size(643, 43);
            this.pnl_lstBox.TabIndex = 0;
            this.pnl_lstBox.TabStop = true;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(453, 13);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(94, 14);
            this.lblUserName.TabIndex = 27;
            this.lblUserName.Text = "Signature For ";
            this.lblUserName.Visible = false;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 39);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(635, 1);
            this.lbl_BottomBrd.TabIndex = 10;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 14);
            this.label5.TabIndex = 26;
            this.label5.Text = "Signature Name :";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 36);
            this.lbl_LeftBrd.TabIndex = 9;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // txtSignatureName
            // 
            this.txtSignatureName.Location = new System.Drawing.Point(132, 10);
            this.txtSignatureName.Name = "txtSignatureName";
            this.txtSignatureName.Size = new System.Drawing.Size(193, 22);
            this.txtSignatureName.TabIndex = 1;
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(639, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 36);
            this.lbl_RightBrd.TabIndex = 8;
            this.lbl_RightBrd.Text = "label3";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(331, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(65, 18);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Default";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(637, 1);
            this.lbl_TopBrd.TabIndex = 7;
            this.lbl_TopBrd.Text = "label1";
            // 
            // label20
            // 
            this.label20.AutoEllipsis = true;
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(15, 15);
            this.label20.Name = "label20";
            this.label20.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label20.Size = new System.Drawing.Size(14, 14);
            this.label20.TabIndex = 116;
            this.label20.Text = "*";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btn_DeleteSign
            // 
            this.btn_DeleteSign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_DeleteSign.BackgroundImage")));
            this.btn_DeleteSign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_DeleteSign.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_DeleteSign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeleteSign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DeleteSign.Location = new System.Drawing.Point(404, 8);
            this.btn_DeleteSign.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_DeleteSign.Name = "btn_DeleteSign";
            this.btn_DeleteSign.Size = new System.Drawing.Size(69, 23);
            this.btn_DeleteSign.TabIndex = 7;
            this.btn_DeleteSign.Text = "Delete";
            this.btn_DeleteSign.UseVisualStyleBackColor = true;
            this.btn_DeleteSign.Visible = false;
            this.btn_DeleteSign.Click += new System.EventHandler(this.btn_DeleteSign_Click);
            // 
            // btn_NewSign
            // 
            this.btn_NewSign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_NewSign.BackgroundImage")));
            this.btn_NewSign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_NewSign.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_NewSign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_NewSign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NewSign.Location = new System.Drawing.Point(481, 9);
            this.btn_NewSign.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_NewSign.Name = "btn_NewSign";
            this.btn_NewSign.Size = new System.Drawing.Size(69, 23);
            this.btn_NewSign.TabIndex = 8;
            this.btn_NewSign.Text = "New";
            this.btn_NewSign.UseVisualStyleBackColor = true;
            this.btn_NewSign.Visible = false;
            this.btn_NewSign.Click += new System.EventHandler(this.btn_NewSign_Click);
            // 
            // btn_SaveAsSign
            // 
            this.btn_SaveAsSign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_SaveAsSign.BackgroundImage")));
            this.btn_SaveAsSign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_SaveAsSign.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_SaveAsSign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SaveAsSign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SaveAsSign.Location = new System.Drawing.Point(625, 9);
            this.btn_SaveAsSign.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_SaveAsSign.Name = "btn_SaveAsSign";
            this.btn_SaveAsSign.Size = new System.Drawing.Size(69, 23);
            this.btn_SaveAsSign.TabIndex = 10;
            this.btn_SaveAsSign.Text = "Save As";
            this.btn_SaveAsSign.UseVisualStyleBackColor = true;
            this.btn_SaveAsSign.Visible = false;
            this.btn_SaveAsSign.Click += new System.EventHandler(this.btn_SaveAsSign_Click);
            // 
            // btn_SaveSign
            // 
            this.btn_SaveSign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_SaveSign.BackgroundImage")));
            this.btn_SaveSign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_SaveSign.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_SaveSign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SaveSign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SaveSign.Location = new System.Drawing.Point(553, 9);
            this.btn_SaveSign.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_SaveSign.Name = "btn_SaveSign";
            this.btn_SaveSign.Size = new System.Drawing.Size(69, 23);
            this.btn_SaveSign.TabIndex = 9;
            this.btn_SaveSign.Text = "Save";
            this.btn_SaveSign.UseVisualStyleBackColor = true;
            this.btn_SaveSign.Visible = false;
            this.btn_SaveSign.Click += new System.EventHandler(this.btn_SaveSign_Click);
            // 
            // frmSignature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(643, 433);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnl_lstBox);
            this.Controls.Add(this.pnl_Toolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSignature";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Signature";
            this.Load += new System.EventHandler(this.frmSignature_Load);
            this.panel2.ResumeLayout(false);
            this.pnl_Toolstrip.ResumeLayout(false);
            this.pnl_Toolstrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnl_lstBox.ResumeLayout(false);
            this.pnl_lstBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_SaveAsSign;
        private System.Windows.Forms.Button btn_SaveSign;
        private System.Windows.Forms.Button btn_NewSign;
        private System.Windows.Forms.Button btn_DeleteSign;
        private System.Windows.Forms.ListBox lst_Signature;
        private System.Windows.Forms.Panel pnl_Toolstrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnl_lstBox;
      //  private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.TextBox txtSignatureName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
    }
}