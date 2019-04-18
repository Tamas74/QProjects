namespace gloBilling
{
    partial class frmPaymentReserveRemaning
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
                if (bICDAssigned)
                {
                    if (oICD9List != null)
                    {
                        oICD9List.Clear();
                        oICD9List.Dispose();
                        oICD9List = null;
                    }
                    bICDAssigned = false;
                }
                if (bCPTAssigned)
                {
                    if (oCPTList != null)
                    {
                        oCPTList.Clear();
                        oCPTList.Dispose();
                        oCPTList = null;
                    }
                    bCPTAssigned = false;
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaymentReserveRemaning));
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsbtnSAVEnCLOSE = new System.Windows.Forms.ToolStripButton();
            this.tlsbtnCLOSE = new System.Windows.Forms.ToolStripButton();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.chkIncludeNotes = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbRecType_Ins = new System.Windows.Forms.RadioButton();
            this.rbRecType_Other = new System.Windows.Forms.RadioButton();
            this.rbRecType_Advance = new System.Windows.Forms.RadioButton();
            this.rbRecType_Copay = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlCPT = new System.Windows.Forms.Panel();
            this.pnllstBoxCPT = new System.Windows.Forms.Panel();
            this.c1CPTList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnllstBoxDiagnosisCPT = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnDeleteCPT = new System.Windows.Forms.Button();
            this.btnSelectCPT = new System.Windows.Forms.Button();
            this.c1CPT = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.pnlDX = new System.Windows.Forms.Panel();
            this.pnllstBoxDiagnosis = new System.Windows.Forms.Panel();
            this.c1Dx = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.pnllstBoxDiagnosisHeader = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDeleteDx = new System.Windows.Forms.Button();
            this.btnSelectDx = new System.Windows.Forms.Button();
            this.c1Diagnosis = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlCPT.SuspendLayout();
            this.pnllstBoxCPT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CPTList)).BeginInit();
            this.pnllstBoxDiagnosisCPT.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CPT)).BeginInit();
            this.pnlDX.SuspendLayout();
            this.pnllstBoxDiagnosis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Dx)).BeginInit();
            this.pnllstBoxDiagnosisHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Diagnosis)).BeginInit();
            this.SuspendLayout();
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupResource);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(533, 54);
            this.pnltlsStrip.TabIndex = 1;
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_SetupResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsbtnSAVEnCLOSE,
            this.tlsbtnCLOSE});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Padding = new System.Windows.Forms.Padding(0);
            this.tls_SetupResource.Size = new System.Drawing.Size(533, 53);
            this.tls_SetupResource.TabIndex = 0;
            this.tls_SetupResource.TabStop = true;
            this.tls_SetupResource.Text = "toolStrip1";
            // 
            // tlsbtnSAVEnCLOSE
            // 
            this.tlsbtnSAVEnCLOSE.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtnSAVEnCLOSE.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsbtnSAVEnCLOSE.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtnSAVEnCLOSE.Image")));
            this.tlsbtnSAVEnCLOSE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsbtnSAVEnCLOSE.Name = "tlsbtnSAVEnCLOSE";
            this.tlsbtnSAVEnCLOSE.Size = new System.Drawing.Size(66, 50);
            this.tlsbtnSAVEnCLOSE.Tag = "OK";
            this.tlsbtnSAVEnCLOSE.Text = "&Save&&Cls";
            this.tlsbtnSAVEnCLOSE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtnSAVEnCLOSE.ToolTipText = "Save and Close";
            this.tlsbtnSAVEnCLOSE.Click += new System.EventHandler(this.tlsbtnSAVEnCLOSE_Click);
            // 
            // tlsbtnCLOSE
            // 
            this.tlsbtnCLOSE.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtnCLOSE.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsbtnCLOSE.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtnCLOSE.Image")));
            this.tlsbtnCLOSE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsbtnCLOSE.Name = "tlsbtnCLOSE";
            this.tlsbtnCLOSE.Size = new System.Drawing.Size(43, 50);
            this.tlsbtnCLOSE.Tag = "Cancel";
            this.tlsbtnCLOSE.Text = "&Close";
            this.tlsbtnCLOSE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtnCLOSE.Click += new System.EventHandler(this.tlsbtnCLOSE_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.ForeColor = System.Drawing.Color.Black;
            this.txtNotes.Location = new System.Drawing.Point(144, 35);
            this.txtNotes.MaxLength = 255;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(363, 98);
            this.txtNotes.TabIndex = 2;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.White;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.ForeColor = System.Drawing.Color.Black;
            this.txtAmount.Location = new System.Drawing.Point(144, 10);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ShortcutsEnabled = false;
            this.txtAmount.Size = new System.Drawing.Size(106, 22);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.Leave += new System.EventHandler(this.txtCheckAmount_Leave);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheckAmount_KeyPress);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(3, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(527, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(526, 1);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(529, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 189);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 190);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.txtNotes);
            this.pnlMain.Controls.Add(this.chkIncludeNotes);
            this.pnlMain.Controls.Add(this.panel5);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.txtAmount);
            this.pnlMain.Controls.Add(this.label59);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.lblName);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(533, 197);
            this.pnlMain.TabIndex = 0;
            // 
            // chkIncludeNotes
            // 
            this.chkIncludeNotes.AutoSize = true;
            this.chkIncludeNotes.Location = new System.Drawing.Point(144, 170);
            this.chkIncludeNotes.Name = "chkIncludeNotes";
            this.chkIncludeNotes.Size = new System.Drawing.Size(160, 18);
            this.chkIncludeNotes.TabIndex = 6;
            this.chkIncludeNotes.Text = "Include Note on Receipt";
            this.chkIncludeNotes.UseVisualStyleBackColor = true;
            this.chkIncludeNotes.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rbRecType_Ins);
            this.panel5.Controls.Add(this.rbRecType_Other);
            this.panel5.Controls.Add(this.rbRecType_Advance);
            this.panel5.Controls.Add(this.rbRecType_Copay);
            this.panel5.Location = new System.Drawing.Point(145, 125);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(282, 37);
            this.panel5.TabIndex = 3;
            // 
            // rbRecType_Ins
            // 
            this.rbRecType_Ins.AutoSize = true;
            this.rbRecType_Ins.Location = new System.Drawing.Point(201, 15);
            this.rbRecType_Ins.Name = "rbRecType_Ins";
            this.rbRecType_Ins.Size = new System.Drawing.Size(78, 18);
            this.rbRecType_Ins.TabIndex = 6;
            this.rbRecType_Ins.Text = "Insurance";
            this.rbRecType_Ins.UseVisualStyleBackColor = true;
            // 
            // rbRecType_Other
            // 
            this.rbRecType_Other.AutoSize = true;
            this.rbRecType_Other.Location = new System.Drawing.Point(145, 15);
            this.rbRecType_Other.Name = "rbRecType_Other";
            this.rbRecType_Other.Size = new System.Drawing.Size(57, 18);
            this.rbRecType_Other.TabIndex = 5;
            this.rbRecType_Other.Text = "Other";
            this.rbRecType_Other.UseVisualStyleBackColor = true;
            this.rbRecType_Other.CheckedChanged += new System.EventHandler(this.rbRecType_Other_CheckedChanged);
            // 
            // rbRecType_Advance
            // 
            this.rbRecType_Advance.AutoSize = true;
            this.rbRecType_Advance.Location = new System.Drawing.Point(67, 15);
            this.rbRecType_Advance.Name = "rbRecType_Advance";
            this.rbRecType_Advance.Size = new System.Drawing.Size(72, 18);
            this.rbRecType_Advance.TabIndex = 4;
            this.rbRecType_Advance.Text = "Advance";
            this.rbRecType_Advance.UseVisualStyleBackColor = true;
            this.rbRecType_Advance.CheckedChanged += new System.EventHandler(this.rbRecType_Advance_CheckedChanged);
            // 
            // rbRecType_Copay
            // 
            this.rbRecType_Copay.AutoSize = true;
            this.rbRecType_Copay.Checked = true;
            this.rbRecType_Copay.Location = new System.Drawing.Point(3, 15);
            this.rbRecType_Copay.Name = "rbRecType_Copay";
            this.rbRecType_Copay.Size = new System.Drawing.Size(58, 18);
            this.rbRecType_Copay.TabIndex = 3;
            this.rbRecType_Copay.TabStop = true;
            this.rbRecType_Copay.Text = "Copay";
            this.rbRecType_Copay.UseVisualStyleBackColor = true;
            this.rbRecType_Copay.CheckedChanged += new System.EventHandler(this.rbRecType_Copay_CheckedChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(90, 134);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label7.Size = new System.Drawing.Size(43, 18);
            this.label7.TabIndex = 30;
            this.label7.Text = "Type :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(90, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "Notes : ";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(15, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(126, 14);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Amount to Reserve : ";
            // 
            // pnlCPT
            // 
            this.pnlCPT.Controls.Add(this.pnllstBoxCPT);
            this.pnlCPT.Controls.Add(this.pnllstBoxDiagnosisCPT);
            this.pnlCPT.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCPT.Location = new System.Drawing.Point(0, 251);
            this.pnlCPT.Name = "pnlCPT";
            this.pnlCPT.Size = new System.Drawing.Size(533, 188);
            this.pnlCPT.TabIndex = 33;
            this.pnlCPT.Visible = false;
            // 
            // pnllstBoxCPT
            // 
            this.pnllstBoxCPT.Controls.Add(this.c1CPTList);
            this.pnllstBoxCPT.Controls.Add(this.label5);
            this.pnllstBoxCPT.Controls.Add(this.label6);
            this.pnllstBoxCPT.Controls.Add(this.label8);
            this.pnllstBoxCPT.Controls.Add(this.label9);
            this.pnllstBoxCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnllstBoxCPT.Location = new System.Drawing.Point(0, 28);
            this.pnllstBoxCPT.Name = "pnllstBoxCPT";
            this.pnllstBoxCPT.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnllstBoxCPT.Size = new System.Drawing.Size(533, 160);
            this.pnllstBoxCPT.TabIndex = 220;
            // 
            // c1CPTList
            // 
            this.c1CPTList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1CPTList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1CPTList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1CPTList.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1CPTList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1CPTList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1CPTList.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1CPTList.Location = new System.Drawing.Point(4, 1);
            this.c1CPTList.Name = "c1CPTList";
            this.c1CPTList.Padding = new System.Windows.Forms.Padding(3);
            this.c1CPTList.Rows.Count = 1;
            this.c1CPTList.Rows.DefaultSize = 19;
            this.c1CPTList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1CPTList.ShowCellLabels = true;
            this.c1CPTList.Size = new System.Drawing.Size(525, 155);
            this.c1CPTList.StyleInfo = resources.GetString("c1CPTList.StyleInfo");
            this.c1CPTList.TabIndex = 63;
            this.c1CPTList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_MouseMove);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(525, 1);
            this.label5.TabIndex = 67;
            this.label5.Text = "label1";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(525, 1);
            this.label6.TabIndex = 66;
            this.label6.Text = "label1";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(529, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 157);
            this.label8.TabIndex = 65;
            this.label8.Text = "label4";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 157);
            this.label9.TabIndex = 64;
            this.label9.Text = "label4";
            // 
            // pnllstBoxDiagnosisCPT
            // 
            this.pnllstBoxDiagnosisCPT.Controls.Add(this.panel7);
            this.pnllstBoxDiagnosisCPT.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnllstBoxDiagnosisCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnllstBoxDiagnosisCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnllstBoxDiagnosisCPT.Location = new System.Drawing.Point(0, 0);
            this.pnllstBoxDiagnosisCPT.Name = "pnllstBoxDiagnosisCPT";
            this.pnllstBoxDiagnosisCPT.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnllstBoxDiagnosisCPT.Size = new System.Drawing.Size(533, 28);
            this.pnllstBoxDiagnosisCPT.TabIndex = 219;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.btnDeleteCPT);
            this.panel7.Controls.Add(this.btnSelectCPT);
            this.panel7.Controls.Add(this.c1CPT);
            this.panel7.Controls.Add(this.label10);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Controls.Add(this.label12);
            this.panel7.Controls.Add(this.label13);
            this.panel7.Controls.Add(this.label14);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.Location = new System.Drawing.Point(3, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(527, 25);
            this.panel7.TabIndex = 19;
            // 
            // btnDeleteCPT
            // 
            this.btnDeleteCPT.AutoEllipsis = true;
            this.btnDeleteCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteCPT.BackgroundImage")));
            this.btnDeleteCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteCPT.Image")));
            this.btnDeleteCPT.Location = new System.Drawing.Point(211, 2);
            this.btnDeleteCPT.Name = "btnDeleteCPT";
            this.btnDeleteCPT.Size = new System.Drawing.Size(21, 21);
            this.btnDeleteCPT.TabIndex = 9;
            this.btnDeleteCPT.UseVisualStyleBackColor = false;
            this.btnDeleteCPT.Click += new System.EventHandler(this.btnDeleteCPT_Click);
            // 
            // btnSelectCPT
            // 
            this.btnSelectCPT.AutoEllipsis = true;
            this.btnSelectCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectCPT.BackgroundImage")));
            this.btnSelectCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectCPT.Image")));
            this.btnSelectCPT.Location = new System.Drawing.Point(184, 2);
            this.btnSelectCPT.Name = "btnSelectCPT";
            this.btnSelectCPT.Size = new System.Drawing.Size(21, 21);
            this.btnSelectCPT.TabIndex = 8;
            this.btnSelectCPT.UseVisualStyleBackColor = false;
            this.btnSelectCPT.Click += new System.EventHandler(this.btnSelectCPT_Click);
            // 
            // c1CPT
            // 
            this.c1CPT.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1CPT.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1CPT.AutoResize = false;
            this.c1CPT.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.c1CPT.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1CPT.Location = new System.Drawing.Point(85, 2);
            this.c1CPT.Name = "c1CPT";
            this.c1CPT.Rows.Count = 1;
            this.c1CPT.Rows.DefaultSize = 19;
            this.c1CPT.Rows.Fixed = 0;
            this.c1CPT.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1CPT.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None;
            this.c1CPT.Size = new System.Drawing.Size(93, 21);
            this.c1CPT.StyleInfo = resources.GetString("c1CPT.StyleInfo");
            this.c1CPT.TabIndex = 7;
            this.c1CPT.ChangeEdit += new System.EventHandler(this.c1CPT_ChangeEdit);
            this.c1CPT.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1CPT_StartEdit);
            this.c1CPT.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1CPT_KeyUp);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(1, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 23);
            this.label10.TabIndex = 0;
            this.label10.Text = "CPT :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 23);
            this.label11.TabIndex = 7;
            this.label11.Text = "label4";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label12.Location = new System.Drawing.Point(526, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 23);
            this.label12.TabIndex = 6;
            this.label12.Text = "label3";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(527, 1);
            this.label13.TabIndex = 5;
            this.label13.Text = "label1";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label14.Location = new System.Drawing.Point(0, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(527, 1);
            this.label14.TabIndex = 8;
            this.label14.Text = "label2";
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(0, 0);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(333, 132);
            this.pnlInternalControl.TabIndex = 221;
            this.pnlInternalControl.Visible = false;
            // 
            // pnlDX
            // 
            this.pnlDX.Controls.Add(this.pnllstBoxDiagnosis);
            this.pnlDX.Controls.Add(this.pnllstBoxDiagnosisHeader);
            this.pnlDX.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDX.Location = new System.Drawing.Point(0, 439);
            this.pnlDX.Name = "pnlDX";
            this.pnlDX.Size = new System.Drawing.Size(533, 192);
            this.pnlDX.TabIndex = 31;
            this.pnlDX.Visible = false;
            // 
            // pnllstBoxDiagnosis
            // 
            this.pnllstBoxDiagnosis.Controls.Add(this.c1Dx);
            this.pnllstBoxDiagnosis.Controls.Add(this.label41);
            this.pnllstBoxDiagnosis.Controls.Add(this.label40);
            this.pnllstBoxDiagnosis.Controls.Add(this.label39);
            this.pnllstBoxDiagnosis.Controls.Add(this.label38);
            this.pnllstBoxDiagnosis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnllstBoxDiagnosis.Location = new System.Drawing.Point(0, 28);
            this.pnllstBoxDiagnosis.Name = "pnllstBoxDiagnosis";
            this.pnllstBoxDiagnosis.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnllstBoxDiagnosis.Size = new System.Drawing.Size(533, 164);
            this.pnllstBoxDiagnosis.TabIndex = 220;
            // 
            // c1Dx
            // 
            this.c1Dx.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Dx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Dx.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Dx.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1Dx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Dx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Dx.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1Dx.Location = new System.Drawing.Point(4, 1);
            this.c1Dx.Name = "c1Dx";
            this.c1Dx.Padding = new System.Windows.Forms.Padding(3);
            this.c1Dx.Rows.Count = 1;
            this.c1Dx.Rows.DefaultSize = 19;
            this.c1Dx.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1Dx.ShowCellLabels = true;
            this.c1Dx.Size = new System.Drawing.Size(525, 159);
            this.c1Dx.StyleInfo = resources.GetString("c1Dx.StyleInfo");
            this.c1Dx.TabIndex = 63;
            this.c1Dx.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_MouseMove);
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(4, 160);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(525, 1);
            this.label41.TabIndex = 67;
            this.label41.Text = "label1";
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Top;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(525, 1);
            this.label40.TabIndex = 66;
            this.label40.Text = "label1";
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Right;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(529, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(1, 161);
            this.label39.TabIndex = 65;
            this.label39.Text = "label4";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Left;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(3, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 161);
            this.label38.TabIndex = 64;
            this.label38.Text = "label4";
            // 
            // pnllstBoxDiagnosisHeader
            // 
            this.pnllstBoxDiagnosisHeader.Controls.Add(this.panel2);
            this.pnllstBoxDiagnosisHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnllstBoxDiagnosisHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnllstBoxDiagnosisHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnllstBoxDiagnosisHeader.Location = new System.Drawing.Point(0, 0);
            this.pnllstBoxDiagnosisHeader.Name = "pnllstBoxDiagnosisHeader";
            this.pnllstBoxDiagnosisHeader.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnllstBoxDiagnosisHeader.Size = new System.Drawing.Size(533, 28);
            this.pnllstBoxDiagnosisHeader.TabIndex = 219;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.btnDeleteDx);
            this.panel2.Controls.Add(this.btnSelectDx);
            this.panel2.Controls.Add(this.c1Diagnosis);
            this.panel2.Controls.Add(this.label50);
            this.panel2.Controls.Add(this.label51);
            this.panel2.Controls.Add(this.label52);
            this.panel2.Controls.Add(this.label53);
            this.panel2.Controls.Add(this.label54);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(527, 25);
            this.panel2.TabIndex = 19;
            // 
            // btnDeleteDx
            // 
            this.btnDeleteDx.AutoEllipsis = true;
            this.btnDeleteDx.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteDx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteDx.BackgroundImage")));
            this.btnDeleteDx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteDx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDx.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteDx.Image")));
            this.btnDeleteDx.Location = new System.Drawing.Point(211, 2);
            this.btnDeleteDx.Name = "btnDeleteDx";
            this.btnDeleteDx.Size = new System.Drawing.Size(21, 21);
            this.btnDeleteDx.TabIndex = 12;
            this.btnDeleteDx.UseVisualStyleBackColor = false;
            this.btnDeleteDx.Click += new System.EventHandler(this.btnDeleteDx_Click);
            // 
            // btnSelectDx
            // 
            this.btnSelectDx.AutoEllipsis = true;
            this.btnSelectDx.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectDx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectDx.BackgroundImage")));
            this.btnSelectDx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectDx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectDx.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectDx.Image")));
            this.btnSelectDx.Location = new System.Drawing.Point(184, 2);
            this.btnSelectDx.Name = "btnSelectDx";
            this.btnSelectDx.Size = new System.Drawing.Size(21, 21);
            this.btnSelectDx.TabIndex = 11;
            this.btnSelectDx.UseVisualStyleBackColor = false;
            this.btnSelectDx.Click += new System.EventHandler(this.btnSelectDx_Click);
            // 
            // c1Diagnosis
            // 
            this.c1Diagnosis.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Diagnosis.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1Diagnosis.AutoResize = false;
            this.c1Diagnosis.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.c1Diagnosis.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1Diagnosis.Location = new System.Drawing.Point(85, 2);
            this.c1Diagnosis.Name = "c1Diagnosis";
            this.c1Diagnosis.Rows.Count = 1;
            this.c1Diagnosis.Rows.DefaultSize = 19;
            this.c1Diagnosis.Rows.Fixed = 0;
            this.c1Diagnosis.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1Diagnosis.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None;
            this.c1Diagnosis.Size = new System.Drawing.Size(93, 21);
            this.c1Diagnosis.StyleInfo = resources.GetString("c1Diagnosis.StyleInfo");
            this.c1Diagnosis.TabIndex = 10;
            this.c1Diagnosis.ChangeEdit += new System.EventHandler(this.c1Diagnosis_ChangeEdit);
            this.c1Diagnosis.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Diagnosis_StartEdit);
            this.c1Diagnosis.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1Diagnosis_KeyUp);
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.Transparent;
            this.label50.Dock = System.Windows.Forms.DockStyle.Left;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Location = new System.Drawing.Point(1, 1);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(89, 23);
            this.label50.TabIndex = 0;
            this.label50.Text = " Diagnosis :";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Left;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(0, 1);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(1, 23);
            this.label51.TabIndex = 7;
            this.label51.Text = "label4";
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Right;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label52.Location = new System.Drawing.Point(526, 1);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 23);
            this.label52.TabIndex = 6;
            this.label52.Text = "label3";
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Top;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(0, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(527, 1);
            this.label53.TabIndex = 5;
            this.label53.Text = "label1";
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label54.Location = new System.Drawing.Point(0, 24);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(527, 1);
            this.label54.TabIndex = 8;
            this.label54.Text = "label2";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmPaymentReserveRemaning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(533, 631);
            this.Controls.Add(this.pnlInternalControl);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlCPT);
            this.Controls.Add(this.pnltlsStrip);
            this.Controls.Add(this.pnlDX);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPaymentReserveRemaning";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reserve Remaining";
            this.Load += new System.EventHandler(this.frmPaymentReserveRemaning_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPaymentReserveRemaning_FormClosed);
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlCPT.ResumeLayout(false);
            this.pnllstBoxCPT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1CPTList)).EndInit();
            this.pnllstBoxDiagnosisCPT.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1CPT)).EndInit();
            this.pnlDX.ResumeLayout(false);
            this.pnllstBoxDiagnosis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Dx)).EndInit();
            this.pnllstBoxDiagnosisHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Diagnosis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        private System.Windows.Forms.ToolStripButton tlsbtnSAVEnCLOSE;
        private System.Windows.Forms.ToolStripButton tlsbtnCLOSE;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox chkIncludeNotes;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbRecType_Other;
        private System.Windows.Forms.RadioButton rbRecType_Copay;
        private System.Windows.Forms.RadioButton rbRecType_Advance;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlDX;
        internal System.Windows.Forms.Panel pnllstBoxDiagnosisHeader;
        internal System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDeleteDx;
        private System.Windows.Forms.Button btnSelectDx;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Diagnosis;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Panel pnllstBoxDiagnosis;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Dx;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.Panel pnlCPT;
        private System.Windows.Forms.Panel pnllstBoxCPT;
        private C1.Win.C1FlexGrid.C1FlexGrid c1CPTList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Panel pnllstBoxDiagnosisCPT;
        internal System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnDeleteCPT;
        private System.Windows.Forms.Button btnSelectCPT;
        private C1.Win.C1FlexGrid.C1FlexGrid c1CPT;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.RadioButton rbRecType_Ins;
    }
}