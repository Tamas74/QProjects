namespace gloBilling
{
    partial class frmEligibilityResponse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEligibilityResponse));
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnCheckResponse = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.c1Response = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtServiceDate = new System.Windows.Forms.TextBox();
            this.lblServiceDate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listResponse = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.rtfeligibilityinfo = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtEligibilityDate = new System.Windows.Forms.TextBox();
            this.txtInsuranceName = new System.Windows.Forms.TextBox();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.lblEligibilityDate = new System.Windows.Forms.Label();
            this.lblInsurance = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.rtfError = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tls_Top.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Response)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnCheckResponse,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(800, 53);
            this.tls_Top.TabIndex = 11;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnCheckResponse
            // 
            this.tls_btnCheckResponse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCheckResponse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCheckResponse.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCheckResponse.Image")));
            this.tls_btnCheckResponse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCheckResponse.Name = "tls_btnCheckResponse";
            this.tls_btnCheckResponse.Size = new System.Drawing.Size(70, 50);
            this.tls_btnCheckResponse.Tag = "Response";
            this.tls_btnCheckResponse.Text = "&Response";
            this.tls_btnCheckResponse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCheckResponse.Visible = false;
            this.tls_btnCheckResponse.Click += new System.EventHandler(this.tls_btnCheckResponse_Click);
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Cancel";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCancel.ToolTipText = "Close";
            this.tls_btnCancel.Click += new System.EventHandler(this.tls_btnCancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMain.Location = new System.Drawing.Point(0, 399);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(800, 145);
            this.pnlMain.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.c1Response);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.txtServiceDate);
            this.panel3.Controls.Add(this.lblServiceDate);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 1, 3);
            this.panel3.Size = new System.Drawing.Size(800, 145);
            this.panel3.TabIndex = 1;
            // 
            // c1Response
            // 
            this.c1Response.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Response.AllowEditing = false;
            this.c1Response.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1Response.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Response.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Response.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Response.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Response.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Response.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Response.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Response.Location = new System.Drawing.Point(4, 0);
            this.c1Response.Name = "c1Response";
            this.c1Response.Rows.Count = 1;
            this.c1Response.Rows.DefaultSize = 19;
            this.c1Response.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Response.Size = new System.Drawing.Size(793, 138);
            this.c1Response.StyleInfo = resources.GetString("c1Response.StyleInfo");
            this.c1Response.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(4, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(794, 1);
            this.label4.TabIndex = 12;
            this.label4.Text = "label2";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 141);
            this.label9.TabIndex = 11;
            this.label9.Text = "label4";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(798, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 141);
            this.label10.TabIndex = 10;
            this.label10.Text = "label3";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(796, 1);
            this.label11.TabIndex = 9;
            this.label11.Text = "label1";
            // 
            // txtServiceDate
            // 
            this.txtServiceDate.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtServiceDate.Enabled = false;
            this.txtServiceDate.Location = new System.Drawing.Point(353, 1);
            this.txtServiceDate.Name = "txtServiceDate";
            this.txtServiceDate.Size = new System.Drawing.Size(447, 22);
            this.txtServiceDate.TabIndex = 1;
            this.txtServiceDate.Visible = false;
            // 
            // lblServiceDate
            // 
            this.lblServiceDate.AutoSize = true;
            this.lblServiceDate.Location = new System.Drawing.Point(260, 7);
            this.lblServiceDate.Name = "lblServiceDate";
            this.lblServiceDate.Size = new System.Drawing.Size(84, 14);
            this.lblServiceDate.TabIndex = 0;
            this.lblServiceDate.Text = "Service Date :";
            this.lblServiceDate.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(237, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "Patient Name: ";
            this.label6.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listResponse);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.Label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.Label7);
            this.panel2.Controls.Add(this.Label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(800, 145);
            this.panel2.TabIndex = 0;
            // 
            // listResponse
            // 
            this.listResponse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listResponse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listResponse.ForeColor = System.Drawing.Color.Black;
            this.listResponse.FormattingEnabled = true;
            this.listResponse.ItemHeight = 14;
            this.listResponse.Location = new System.Drawing.Point(7, 4);
            this.listResponse.Name = "listResponse";
            this.listResponse.Size = new System.Drawing.Size(789, 126);
            this.listResponse.TabIndex = 13;
            this.listResponse.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(789, 3);
            this.label1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(3, 140);
            this.label2.TabIndex = 10;
            this.label2.Text = "label4";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 141);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(792, 1);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 141);
            this.label3.TabIndex = 11;
            this.label3.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(796, 1);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 141);
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
            this.Label8.Size = new System.Drawing.Size(794, 1);
            this.Label8.TabIndex = 9;
            this.Label8.Text = "label1";
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.rtfeligibilityinfo);
            this.pnlTop.Controls.Add(this.label12);
            this.pnlTop.Controls.Add(this.label13);
            this.pnlTop.Controls.Add(this.label14);
            this.pnlTop.Controls.Add(this.label15);
            this.pnlTop.Controls.Add(this.txtEligibilityDate);
            this.pnlTop.Controls.Add(this.txtInsuranceName);
            this.pnlTop.Controls.Add(this.txtPatientName);
            this.pnlTop.Controls.Add(this.lblEligibilityDate);
            this.pnlTop.Controls.Add(this.lblInsurance);
            this.pnlTop.Controls.Add(this.lblPatientName);
            this.pnlTop.Controls.Add(this.rtfError);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 54);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTop.Size = new System.Drawing.Size(800, 345);
            this.pnlTop.TabIndex = 2;
            // 
            // rtfeligibilityinfo
            // 
            this.rtfeligibilityinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfeligibilityinfo.Location = new System.Drawing.Point(4, 4);
            this.rtfeligibilityinfo.Name = "rtfeligibilityinfo";
            this.rtfeligibilityinfo.Size = new System.Drawing.Size(792, 337);
            this.rtfeligibilityinfo.TabIndex = 14;
            this.rtfeligibilityinfo.Text = "";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label12.Location = new System.Drawing.Point(4, 341);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(792, 1);
            this.label12.TabIndex = 12;
            this.label12.Text = "label2";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 338);
            this.label13.TabIndex = 11;
            this.label13.Text = "label4";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label14.Location = new System.Drawing.Point(796, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 338);
            this.label14.TabIndex = 10;
            this.label14.Text = "label3";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(794, 1);
            this.label15.TabIndex = 9;
            this.label15.Text = "label1";
            // 
            // txtEligibilityDate
            // 
            this.txtEligibilityDate.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtEligibilityDate.Enabled = false;
            this.txtEligibilityDate.ForeColor = System.Drawing.Color.Black;
            this.txtEligibilityDate.Location = new System.Drawing.Point(131, 68);
            this.txtEligibilityDate.Name = "txtEligibilityDate";
            this.txtEligibilityDate.Size = new System.Drawing.Size(447, 22);
            this.txtEligibilityDate.TabIndex = 2;
            // 
            // txtInsuranceName
            // 
            this.txtInsuranceName.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtInsuranceName.Enabled = false;
            this.txtInsuranceName.ForeColor = System.Drawing.Color.Black;
            this.txtInsuranceName.Location = new System.Drawing.Point(131, 40);
            this.txtInsuranceName.Name = "txtInsuranceName";
            this.txtInsuranceName.Size = new System.Drawing.Size(447, 22);
            this.txtInsuranceName.TabIndex = 1;
            // 
            // txtPatientName
            // 
            this.txtPatientName.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtPatientName.Enabled = false;
            this.txtPatientName.ForeColor = System.Drawing.Color.Black;
            this.txtPatientName.Location = new System.Drawing.Point(131, 13);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(447, 22);
            this.txtPatientName.TabIndex = 0;
            // 
            // lblEligibilityDate
            // 
            this.lblEligibilityDate.AutoSize = true;
            this.lblEligibilityDate.Location = new System.Drawing.Point(35, 72);
            this.lblEligibilityDate.Name = "lblEligibilityDate";
            this.lblEligibilityDate.Size = new System.Drawing.Size(93, 14);
            this.lblEligibilityDate.TabIndex = 0;
            this.lblEligibilityDate.Text = "Eligibility Date : ";
            // 
            // lblInsurance
            // 
            this.lblInsurance.AutoSize = true;
            this.lblInsurance.Location = new System.Drawing.Point(21, 44);
            this.lblInsurance.Name = "lblInsurance";
            this.lblInsurance.Size = new System.Drawing.Size(107, 14);
            this.lblInsurance.TabIndex = 0;
            this.lblInsurance.Text = "Insurance Name : ";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Location = new System.Drawing.Point(35, 17);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(93, 14);
            this.lblPatientName.TabIndex = 0;
            this.lblPatientName.Text = "Patient Name : ";
            // 
            // rtfError
            // 
            this.rtfError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfError.Location = new System.Drawing.Point(3, 3);
            this.rtfError.Name = "rtfError";
            this.rtfError.Size = new System.Drawing.Size(794, 339);
            this.rtfError.TabIndex = 15;
            this.rtfError.Text = "";
            this.rtfError.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tls_Top);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 54);
            this.panel1.TabIndex = 0;
            // 
            // frmEligibilityResponse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(800, 544);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEligibilityResponse";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eligibility Response";
            this.Load += new System.EventHandler(this.frmEligibilityResponse_Load);
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Response)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStripButton tls_btnCheckResponse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblServiceDate;
        private System.Windows.Forms.Label lblEligibilityDate;
        private System.Windows.Forms.Label lblInsurance;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.TextBox txtInsuranceName;
        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.TextBox txtServiceDate;
        private System.Windows.Forms.TextBox txtEligibilityDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListBox listResponse;
        private System.Windows.Forms.RichTextBox rtfeligibilityinfo;
        private System.Windows.Forms.RichTextBox rtfError;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Response;
    }
}