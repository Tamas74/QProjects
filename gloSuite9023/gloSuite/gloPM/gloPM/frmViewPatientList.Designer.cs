namespace gloPM
{
    partial class frmViewPatientList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewPatientList));
            this.pnl_main = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.c1ViewPatientList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.pnl_AddCriteria = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmb_Gender = new System.Windows.Forms.ComboBox();
            this.lbl_gender = new System.Windows.Forms.Label();
            this.txt_State = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_City = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ZIP = new System.Windows.Forms.TextBox();
            this.pnl_Toolstrip = new System.Windows.Forms.Panel();
            this.lblItemType = new System.Windows.Forms.Label();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlDiagnosis = new System.Windows.Forms.Panel();
            this.rbICD9 = new System.Windows.Forms.RadioButton();
            this.rbICD10 = new System.Windows.Forms.RadioButton();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip();
            this.pnl_main.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ViewPatientList)).BeginInit();
            this.pnl_AddCriteria.SuspendLayout();
            this.pnl_Toolstrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlDiagnosis.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_main
            // 
            this.pnl_main.Controls.Add(this.Panel2);
            this.pnl_main.Controls.Add(this.pnl_AddCriteria);
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_main.Location = new System.Drawing.Point(0, 54);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(841, 621);
            this.pnl_main.TabIndex = 0;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.c1ViewPatientList);
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Controls.Add(this.Label7);
            this.Panel2.Controls.Add(this.Label8);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel2.Location = new System.Drawing.Point(0, 54);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Panel2.Size = new System.Drawing.Size(841, 567);
            this.Panel2.TabIndex = 20;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 563);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(833, 1);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "label2";
            // 
            // c1ViewPatientList
            // 
            this.c1ViewPatientList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ViewPatientList.AllowEditing = false;
            this.c1ViewPatientList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1ViewPatientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ViewPatientList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1ViewPatientList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ViewPatientList.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1ViewPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ViewPatientList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ViewPatientList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ViewPatientList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1ViewPatientList.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1ViewPatientList.Location = new System.Drawing.Point(4, 1);
            this.c1ViewPatientList.Name = "c1ViewPatientList";
            this.c1ViewPatientList.Rows.Count = 1;
            this.c1ViewPatientList.Rows.DefaultSize = 19;
            this.c1ViewPatientList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ViewPatientList.ShowCellLabels = true;
            this.c1ViewPatientList.Size = new System.Drawing.Size(833, 563);
            this.c1ViewPatientList.StyleInfo = resources.GetString("c1ViewPatientList.StyleInfo");
            this.c1ViewPatientList.TabIndex = 16;
            this.c1ViewPatientList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1ViewPatientList_MouseMove);
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 563);
            this.Label6.TabIndex = 7;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(837, 1);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 563);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(835, 1);
            this.Label8.TabIndex = 5;
            this.Label8.Text = "label1";
            // 
            // pnl_AddCriteria
            // 
            this.pnl_AddCriteria.Controls.Add(this.label4);
            this.pnl_AddCriteria.Controls.Add(this.label9);
            this.pnl_AddCriteria.Controls.Add(this.label10);
            this.pnl_AddCriteria.Controls.Add(this.label11);
            this.pnl_AddCriteria.Controls.Add(this.cmb_Gender);
            this.pnl_AddCriteria.Controls.Add(this.lbl_gender);
            this.pnl_AddCriteria.Controls.Add(this.txt_State);
            this.pnl_AddCriteria.Controls.Add(this.label3);
            this.pnl_AddCriteria.Controls.Add(this.label2);
            this.pnl_AddCriteria.Controls.Add(this.txt_City);
            this.pnl_AddCriteria.Controls.Add(this.label1);
            this.pnl_AddCriteria.Controls.Add(this.txt_ZIP);
            this.pnl_AddCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_AddCriteria.Location = new System.Drawing.Point(0, 0);
            this.pnl_AddCriteria.Name = "pnl_AddCriteria";
            this.pnl_AddCriteria.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_AddCriteria.Size = new System.Drawing.Size(841, 54);
            this.pnl_AddCriteria.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(4, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(833, 1);
            this.label4.TabIndex = 23;
            this.label4.Text = "label2";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 47);
            this.label9.TabIndex = 22;
            this.label9.Text = "label4";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(837, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 47);
            this.label10.TabIndex = 21;
            this.label10.Text = "label3";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(835, 1);
            this.label11.TabIndex = 20;
            this.label11.Text = "label1";
            // 
            // cmb_Gender
            // 
            this.cmb_Gender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Gender.FormattingEnabled = true;
            this.cmb_Gender.Items.AddRange(new object[] {
            "",
            "Male",
            "Female"});
            this.cmb_Gender.Location = new System.Drawing.Point(536, 16);
            this.cmb_Gender.Name = "cmb_Gender";
            this.cmb_Gender.Size = new System.Drawing.Size(105, 22);
            this.cmb_Gender.TabIndex = 19;
            // 
            // lbl_gender
            // 
            this.lbl_gender.AutoSize = true;
            this.lbl_gender.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_gender.Location = new System.Drawing.Point(478, 20);
            this.lbl_gender.Name = "lbl_gender";
            this.lbl_gender.Size = new System.Drawing.Size(55, 14);
            this.lbl_gender.TabIndex = 7;
            this.lbl_gender.Text = "Gender :";
            // 
            // txt_State
            // 
            this.txt_State.Location = new System.Drawing.Point(224, 16);
            this.txt_State.Name = "txt_State";
            this.txt_State.Size = new System.Drawing.Size(100, 22);
            this.txt_State.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(333, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "ZIP :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "State :";
            // 
            // txt_City
            // 
            this.txt_City.Location = new System.Drawing.Point(67, 16);
            this.txt_City.Name = "txt_City";
            this.txt_City.Size = new System.Drawing.Size(100, 22);
            this.txt_City.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(29, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "City :";
            // 
            // txt_ZIP
            // 
            this.txt_ZIP.Location = new System.Drawing.Point(369, 16);
            this.txt_ZIP.Name = "txt_ZIP";
            this.txt_ZIP.Size = new System.Drawing.Size(100, 22);
            this.txt_ZIP.TabIndex = 4;
            // 
            // pnl_Toolstrip
            // 
            this.pnl_Toolstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnl_Toolstrip.Controls.Add(this.lblItemType);
            this.pnl_Toolstrip.Controls.Add(this.tls_Top);
            this.pnl_Toolstrip.Controls.Add(this.pnlDiagnosis);
            this.pnl_Toolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Toolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_Toolstrip.Name = "pnl_Toolstrip";
            this.pnl_Toolstrip.Size = new System.Drawing.Size(841, 54);
            this.pnl_Toolstrip.TabIndex = 8;
            // 
            // lblItemType
            // 
            this.lblItemType.AutoSize = true;
            this.lblItemType.Location = new System.Drawing.Point(76, 10);
            this.lblItemType.Name = "lblItemType";
            this.lblItemType.Size = new System.Drawing.Size(0, 14);
            this.lblItemType.TabIndex = 27;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnOK,
            this.tls_btnExportToExcel,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(686, 53);
            this.tls_Top.TabIndex = 28;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tls_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(113, 50);
            this.tls_btnOK.Tag = "OK";
            this.tls_btnOK.Text = "&Generate Report";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnOK.Click += new System.EventHandler(this.tls_btnOK_Click);
            // 
            // tls_btnExportToExcel
            // 
            this.tls_btnExportToExcel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnExportToExcel.Image")));
            this.tls_btnExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnExportToExcel.Name = "tls_btnExportToExcel";
            this.tls_btnExportToExcel.Size = new System.Drawing.Size(105, 50);
            this.tls_btnExportToExcel.Tag = "Export";
            this.tls_btnExportToExcel.Text = "Export To Excel";
            this.tls_btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnExportToExcel.Click += new System.EventHandler(this.tls_btnExportToExcel_Click);
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Cancel";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCancel.Click += new System.EventHandler(this.tls_btnCancel_Click);
            // 
            // pnlDiagnosis
            // 
            this.pnlDiagnosis.BackColor = System.Drawing.Color.Transparent;
            this.pnlDiagnosis.BackgroundImage = global::gloPM.Properties.Resources.Img_Toolstrip;
            this.pnlDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDiagnosis.Controls.Add(this.rbICD9);
            this.pnlDiagnosis.Controls.Add(this.rbICD10);
            this.pnlDiagnosis.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDiagnosis.Location = new System.Drawing.Point(686, 0);
            this.pnlDiagnosis.Name = "pnlDiagnosis";
            this.pnlDiagnosis.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.pnlDiagnosis.Size = new System.Drawing.Size(155, 54);
            this.pnlDiagnosis.TabIndex = 67;
            this.pnlDiagnosis.Visible = false;
            // 
            // rbICD9
            // 
            this.rbICD9.AutoSize = true;
            this.rbICD9.BackColor = System.Drawing.Color.Transparent;
            this.rbICD9.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbICD9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbICD9.Location = new System.Drawing.Point(23, 0);
            this.rbICD9.Name = "rbICD9";
            this.rbICD9.Size = new System.Drawing.Size(59, 54);
            this.rbICD9.TabIndex = 59;
            this.rbICD9.Text = "ICD9  ";
            this.rbICD9.UseVisualStyleBackColor = false;
            this.rbICD9.CheckedChanged += new System.EventHandler(this.rbICD9_CheckedChanged);
            // 
            // rbICD10
            // 
            this.rbICD10.AutoSize = true;
            this.rbICD10.BackColor = System.Drawing.Color.Transparent;
            this.rbICD10.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbICD10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbICD10.Location = new System.Drawing.Point(82, 0);
            this.rbICD10.Name = "rbICD10";
            this.rbICD10.Size = new System.Drawing.Size(58, 54);
            this.rbICD10.TabIndex = 64;
            this.rbICD10.Text = "ICD10";
            this.rbICD10.UseVisualStyleBackColor = false;
            this.rbICD10.CheckedChanged += new System.EventHandler(this.rbICD9_CheckedChanged);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmViewPatientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(841, 675);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.pnl_Toolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewPatientList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "View Patient List";
            this.Load += new System.EventHandler(this.frmViewPatientList_Load);
            this.pnl_main.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ViewPatientList)).EndInit();
            this.pnl_AddCriteria.ResumeLayout(false);
            this.pnl_AddCriteria.PerformLayout();
            this.pnl_Toolstrip.ResumeLayout(false);
            this.pnl_Toolstrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlDiagnosis.ResumeLayout(false);
            this.pnlDiagnosis.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_main;
        //private C1.Win.C1FlexGrid.C1FlexGrid c1ViewContacts;
        private System.Windows.Forms.Panel pnl_Toolstrip;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ViewPatientList;
        private System.Windows.Forms.Panel pnl_AddCriteria;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_ZIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_State;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_City;
        private System.Windows.Forms.Label lblItemType;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcel;
        private System.Windows.Forms.Label lbl_gender;
        private System.Windows.Forms.ComboBox cmb_Gender;
        internal System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel pnlDiagnosis;
        private System.Windows.Forms.RadioButton rbICD9;
        private System.Windows.Forms.RadioButton rbICD10;
    }
}