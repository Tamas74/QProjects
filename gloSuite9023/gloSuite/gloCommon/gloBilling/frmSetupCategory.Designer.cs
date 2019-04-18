namespace gloBilling
{
    partial class frmSetupCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupCategory));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.pnlParentRace = new System.Windows.Forms.Panel();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.cmbParentRace = new System.Windows.Forms.ComboBox();
            this.pnlFavourite = new System.Windows.Forms.Panel();
            this.chkFavourite = new System.Windows.Forms.CheckBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.cmbCategoryType = new System.Windows.Forms.ComboBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.lbl_pnlBottom = new System.Windows.Forms.Label();
            this.lbl_pnlLeft = new System.Windows.Forms.Label();
            this.lbl_pnlRight = new System.Windows.Forms.Label();
            this.lbl_pnlTop = new System.Windows.Forms.Label();
            this.ts_Commands.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.pnlParentRace.SuspendLayout();
            this.pnlFavourite.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
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
            this.ts_Commands.Size = new System.Drawing.Size(411, 53);
            this.ts_Commands.TabIndex = 3;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
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
            this.tsb_OK.ToolTipText = "Save and Close";
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
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ts_Commands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(411, 54);
            this.panel2.TabIndex = 1;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.pnlParentRace);
            this.Panel1.Controls.Add(this.pnlFavourite);
            this.Panel1.Controls.Add(this.pnlMain);
            this.Panel1.Controls.Add(this.lbl_pnlBottom);
            this.Panel1.Controls.Add(this.lbl_pnlLeft);
            this.Panel1.Controls.Add(this.lbl_pnlRight);
            this.Panel1.Controls.Add(this.lbl_pnlTop);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 54);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(411, 159);
            this.Panel1.TabIndex = 13;
            // 
            // pnlParentRace
            // 
            this.pnlParentRace.Controls.Add(this.Label7);
            this.pnlParentRace.Controls.Add(this.Label8);
            this.pnlParentRace.Controls.Add(this.cmbParentRace);
            this.pnlParentRace.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlParentRace.Location = new System.Drawing.Point(4, 123);
            this.pnlParentRace.Name = "pnlParentRace";
            this.pnlParentRace.Size = new System.Drawing.Size(403, 34);
            this.pnlParentRace.TabIndex = 21;
            this.pnlParentRace.Visible = false;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Location = new System.Drawing.Point(61, 9);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(81, 14);
            this.Label7.TabIndex = 17;
            this.Label7.Text = "Parent Race :";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.ForeColor = System.Drawing.Color.Red;
            this.Label8.Location = new System.Drawing.Point(48, 9);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(14, 14);
            this.Label8.TabIndex = 18;
            this.Label8.Text = "*";
            // 
            // cmbParentRace
            // 
            this.cmbParentRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParentRace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParentRace.ForeColor = System.Drawing.Color.Black;
            this.cmbParentRace.Location = new System.Drawing.Point(144, 4);
            this.cmbParentRace.Name = "cmbParentRace";
            this.cmbParentRace.Size = new System.Drawing.Size(240, 22);
            this.cmbParentRace.TabIndex = 16;
            // 
            // pnlFavourite
            // 
            this.pnlFavourite.Controls.Add(this.chkFavourite);
            this.pnlFavourite.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFavourite.Location = new System.Drawing.Point(4, 95);
            this.pnlFavourite.Name = "pnlFavourite";
            this.pnlFavourite.Size = new System.Drawing.Size(403, 28);
            this.pnlFavourite.TabIndex = 20;
            this.pnlFavourite.Visible = false;
            // 
            // chkFavourite
            // 
            this.chkFavourite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFavourite.Location = new System.Drawing.Point(58, 4);
            this.chkFavourite.Name = "chkFavourite";
            this.chkFavourite.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkFavourite.Size = new System.Drawing.Size(105, 22);
            this.chkFavourite.TabIndex = 19;
            this.chkFavourite.Text = "      Favorite :";
            this.chkFavourite.UseVisualStyleBackColor = true;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.cmbCategoryType);
            this.pnlMain.Controls.Add(this.txtCategory);
            this.pnlMain.Controls.Add(this.txtCode);
            this.pnlMain.Controls.Add(this.Label6);
            this.pnlMain.Controls.Add(this.Label5);
            this.pnlMain.Controls.Add(this.Label2);
            this.pnlMain.Controls.Add(this.Label3);
            this.pnlMain.Controls.Add(this.Label1);
            this.pnlMain.Controls.Add(this.Label4);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMain.Location = new System.Drawing.Point(4, 4);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(403, 91);
            this.pnlMain.TabIndex = 21;
            // 
            // cmbCategoryType
            // 
            this.cmbCategoryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategoryType.ForeColor = System.Drawing.Color.Black;
            this.cmbCategoryType.Location = new System.Drawing.Point(144, 67);
            this.cmbCategoryType.Name = "cmbCategoryType";
            this.cmbCategoryType.Size = new System.Drawing.Size(240, 22);
            this.cmbCategoryType.TabIndex = 7;
            this.cmbCategoryType.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryType_SelectedIndexChanged);
            // 
            // txtCategory
            // 
            this.txtCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategory.ForeColor = System.Drawing.Color.Black;
            this.txtCategory.Location = new System.Drawing.Point(144, 37);
            this.txtCategory.MaxLength = 254;
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(240, 22);
            this.txtCategory.TabIndex = 6;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.ForeColor = System.Drawing.Color.Black;
            this.txtCode.Location = new System.Drawing.Point(144, 9);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(240, 22);
            this.txtCode.TabIndex = 6;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Red;
            this.Label6.Location = new System.Drawing.Point(24, 13);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(14, 14);
            this.Label6.TabIndex = 16;
            this.Label6.Text = "*";
            this.Label6.Visible = false;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label5.Location = new System.Drawing.Point(37, 13);
            this.Label5.MaximumSize = new System.Drawing.Size(104, 14);
            this.Label5.MinimumSize = new System.Drawing.Size(104, 14);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(104, 14);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "Code :";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Location = new System.Drawing.Point(45, 71);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(96, 14);
            this.Label2.TabIndex = 9;
            this.Label2.Text = "Category Type :";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.Red;
            this.Label3.Location = new System.Drawing.Point(1, 41);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(14, 14);
            this.Label3.TabIndex = 14;
            this.Label3.Text = "*";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label1.Location = new System.Drawing.Point(13, 42);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(128, 14);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Category Description :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.Color.Red;
            this.Label4.Location = new System.Drawing.Point(32, 72);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(14, 14);
            this.Label4.TabIndex = 15;
            this.Label4.Text = "*";
            // 
            // lbl_pnlBottom
            // 
            this.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlBottom.Location = new System.Drawing.Point(4, 155);
            this.lbl_pnlBottom.Name = "lbl_pnlBottom";
            this.lbl_pnlBottom.Size = new System.Drawing.Size(403, 1);
            this.lbl_pnlBottom.TabIndex = 13;
            this.lbl_pnlBottom.Text = "label2";
            // 
            // lbl_pnlLeft
            // 
            this.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlLeft.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlLeft.Name = "lbl_pnlLeft";
            this.lbl_pnlLeft.Size = new System.Drawing.Size(1, 152);
            this.lbl_pnlLeft.TabIndex = 12;
            this.lbl_pnlLeft.Text = "label4";
            // 
            // lbl_pnlRight
            // 
            this.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRight.Location = new System.Drawing.Point(407, 4);
            this.lbl_pnlRight.Name = "lbl_pnlRight";
            this.lbl_pnlRight.Size = new System.Drawing.Size(1, 152);
            this.lbl_pnlRight.TabIndex = 11;
            this.lbl_pnlRight.Text = "label3";
            // 
            // lbl_pnlTop
            // 
            this.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTop.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlTop.Name = "lbl_pnlTop";
            this.lbl_pnlTop.Size = new System.Drawing.Size(405, 1);
            this.lbl_pnlTop.TabIndex = 10;
            this.lbl_pnlTop.Text = "label1";
            // 
            // frmSetupCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(411, 213);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupCategory";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Category";
            this.Load += new System.EventHandler(this.frmSetupCategory_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.pnlParentRace.ResumeLayout(false);
            this.pnlParentRace.PerformLayout();
            this.pnlFavourite.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Panel pnlParentRace;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.ComboBox cmbParentRace;
        internal System.Windows.Forms.Panel pnlFavourite;
        internal System.Windows.Forms.CheckBox chkFavourite;
        internal System.Windows.Forms.Panel pnlMain;
        internal System.Windows.Forms.ComboBox cmbCategoryType;
        internal System.Windows.Forms.TextBox txtCategory;
        internal System.Windows.Forms.TextBox txtCode;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label lbl_pnlBottom;
        private System.Windows.Forms.Label lbl_pnlLeft;
        private System.Windows.Forms.Label lbl_pnlRight;
        private System.Windows.Forms.Label lbl_pnlTop;
    }
}