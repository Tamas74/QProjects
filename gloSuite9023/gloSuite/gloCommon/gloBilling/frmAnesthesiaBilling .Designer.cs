namespace gloBilling
{
    partial class frmAnesthesiaBilling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnesthesiaBilling));
            this.PnlFields = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMinPerUnit = new System.Windows.Forms.TextBox();
            this.lblMinPerUnit = new System.Windows.Forms.Label();
            this.txtTimeUnits = new System.Windows.Forms.TextBox();
            this.lblTimeUnits = new System.Windows.Forms.Label();
            this.txtBaseUnits = new System.Windows.Forms.TextBox();
            this.lblBaseUnits = new System.Windows.Forms.Label();
            this.txtOtherUnits = new System.Windows.Forms.TextBox();
            this.lblOtherUnits = new System.Windows.Forms.Label();
            this.txtTotalUnits = new System.Windows.Forms.TextBox();
            this.lblTotalUnits = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.chkAutoCalculate = new System.Windows.Forms.CheckBox();
            this.mskEndDate = new System.Windows.Forms.MaskedTextBox();
            this.mskStartDate = new System.Windows.Forms.MaskedTextBox();
            this.mskEndTime = new System.Windows.Forms.MaskedTextBox();
            this.mskStartTime = new System.Windows.Forms.MaskedTextBox();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.txtTotalMinutes = new System.Windows.Forms.TextBox();
            this.lblTotalMinutes = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDOSText = new System.Windows.Forms.Label();
            this.lblCPTDescText = new System.Windows.Forms.Label();
            this.lblMod2Text = new System.Windows.Forms.Label();
            this.lblMod1Text = new System.Windows.Forms.Label();
            this.lblCPTCodeText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tls_Notes = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_SaveCls = new System.Windows.Forms.ToolStripButton();
            this.tlb_Close = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.PnlFields.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tls_Notes.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlFields
            // 
            this.PnlFields.Controls.Add(this.groupBox2);
            this.PnlFields.Controls.Add(this.groupBox1);
            this.PnlFields.Controls.Add(this.label4);
            this.PnlFields.Controls.Add(this.label3);
            this.PnlFields.Controls.Add(this.label2);
            this.PnlFields.Controls.Add(this.label1);
            this.PnlFields.Controls.Add(this.label9);
            this.PnlFields.Controls.Add(this.label8);
            this.PnlFields.Controls.Add(this.label7);
            this.PnlFields.Controls.Add(this.label6);
            this.PnlFields.Controls.Add(this.label5);
            this.PnlFields.Controls.Add(this.lblDOSText);
            this.PnlFields.Controls.Add(this.lblCPTDescText);
            this.PnlFields.Controls.Add(this.lblMod2Text);
            this.PnlFields.Controls.Add(this.lblMod1Text);
            this.PnlFields.Controls.Add(this.lblCPTCodeText);
            this.PnlFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlFields.Location = new System.Drawing.Point(0, 54);
            this.PnlFields.Name = "PnlFields";
            this.PnlFields.Padding = new System.Windows.Forms.Padding(3);
            this.PnlFields.Size = new System.Drawing.Size(482, 358);
            this.PnlFields.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMinPerUnit);
            this.groupBox2.Controls.Add(this.lblMinPerUnit);
            this.groupBox2.Controls.Add(this.txtTimeUnits);
            this.groupBox2.Controls.Add(this.lblTimeUnits);
            this.groupBox2.Controls.Add(this.txtBaseUnits);
            this.groupBox2.Controls.Add(this.lblBaseUnits);
            this.groupBox2.Controls.Add(this.txtOtherUnits);
            this.groupBox2.Controls.Add(this.lblOtherUnits);
            this.groupBox2.Controls.Add(this.txtTotalUnits);
            this.groupBox2.Controls.Add(this.lblTotalUnits);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox2.Location = new System.Drawing.Point(18, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(442, 170);
            this.groupBox2.TabIndex = 158;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Units";
            // 
            // txtMinPerUnit
            // 
            this.txtMinPerUnit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinPerUnit.ForeColor = System.Drawing.Color.Black;
            this.txtMinPerUnit.Location = new System.Drawing.Point(119, 21);
            this.txtMinPerUnit.MaxLength = 3;
            this.txtMinPerUnit.Name = "txtMinPerUnit";
            this.txtMinPerUnit.Size = new System.Drawing.Size(94, 22);
            this.txtMinPerUnit.TabIndex = 137;
            this.txtMinPerUnit.TextChanged += new System.EventHandler(this.txtMinPerUnit_TextChanged);
            this.txtMinPerUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMinPerUnit_KeyDown);
            this.txtMinPerUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMinPerUnit_KeyPress);
            this.txtMinPerUnit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtMinPerUnit_MouseUp);
            // 
            // lblMinPerUnit
            // 
            this.lblMinPerUnit.AutoSize = true;
            this.lblMinPerUnit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinPerUnit.Location = new System.Drawing.Point(9, 25);
            this.lblMinPerUnit.Name = "lblMinPerUnit";
            this.lblMinPerUnit.Size = new System.Drawing.Size(105, 14);
            this.lblMinPerUnit.TabIndex = 9;
            this.lblMinPerUnit.Text = "Minutes Per Unit :";
            // 
            // txtTimeUnits
            // 
            this.txtTimeUnits.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimeUnits.ForeColor = System.Drawing.Color.Black;
            this.txtTimeUnits.Location = new System.Drawing.Point(119, 49);
            this.txtTimeUnits.MaxLength = 13;
            this.txtTimeUnits.Name = "txtTimeUnits";
            this.txtTimeUnits.ReadOnly = true;
            this.txtTimeUnits.Size = new System.Drawing.Size(94, 22);
            this.txtTimeUnits.TabIndex = 138;
            this.txtTimeUnits.TextChanged += new System.EventHandler(this.txtTimeUnits_TextChanged);
            this.txtTimeUnits.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimeUnits_KeyDown);
            this.txtTimeUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimeUnits_KeyPress);
            this.txtTimeUnits.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTimeUnits_KeyUp);
            // 
            // lblTimeUnits
            // 
            this.lblTimeUnits.AutoSize = true;
            this.lblTimeUnits.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeUnits.Location = new System.Drawing.Point(41, 53);
            this.lblTimeUnits.Name = "lblTimeUnits";
            this.lblTimeUnits.Size = new System.Drawing.Size(73, 14);
            this.lblTimeUnits.TabIndex = 10;
            this.lblTimeUnits.Text = "Time Units :";
            // 
            // txtBaseUnits
            // 
            this.txtBaseUnits.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseUnits.ForeColor = System.Drawing.Color.Black;
            this.txtBaseUnits.Location = new System.Drawing.Point(119, 77);
            this.txtBaseUnits.MaxLength = 6;
            this.txtBaseUnits.Name = "txtBaseUnits";
            this.txtBaseUnits.Size = new System.Drawing.Size(94, 22);
            this.txtBaseUnits.TabIndex = 139;
            this.txtBaseUnits.Text = "0";
            this.txtBaseUnits.TextChanged += new System.EventHandler(this.txtBaseUnits_TextChanged);
            this.txtBaseUnits.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBaseUnits_KeyDown);
            this.txtBaseUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBaseUnits_KeyPress);
            this.txtBaseUnits.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBaseUnits_KeyUp);
            // 
            // lblBaseUnits
            // 
            this.lblBaseUnits.AutoSize = true;
            this.lblBaseUnits.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaseUnits.Location = new System.Drawing.Point(43, 81);
            this.lblBaseUnits.Name = "lblBaseUnits";
            this.lblBaseUnits.Size = new System.Drawing.Size(71, 14);
            this.lblBaseUnits.TabIndex = 11;
            this.lblBaseUnits.Text = "Base Units :";
            // 
            // txtOtherUnits
            // 
            this.txtOtherUnits.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherUnits.ForeColor = System.Drawing.Color.Black;
            this.txtOtherUnits.Location = new System.Drawing.Point(119, 105);
            this.txtOtherUnits.MaxLength = 6;
            this.txtOtherUnits.Name = "txtOtherUnits";
            this.txtOtherUnits.Size = new System.Drawing.Size(94, 22);
            this.txtOtherUnits.TabIndex = 140;
            this.txtOtherUnits.Text = "0";
            this.txtOtherUnits.TextChanged += new System.EventHandler(this.txtOtherUnits_TextChanged);
            this.txtOtherUnits.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherUnits_KeyDown);
            this.txtOtherUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOtherUnits_KeyPress);
            this.txtOtherUnits.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOtherUnits_KeyUp);
            // 
            // lblOtherUnits
            // 
            this.lblOtherUnits.AutoSize = true;
            this.lblOtherUnits.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherUnits.Location = new System.Drawing.Point(36, 109);
            this.lblOtherUnits.Name = "lblOtherUnits";
            this.lblOtherUnits.Size = new System.Drawing.Size(78, 14);
            this.lblOtherUnits.TabIndex = 12;
            this.lblOtherUnits.Text = "Other Units :";
            // 
            // txtTotalUnits
            // 
            this.txtTotalUnits.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalUnits.ForeColor = System.Drawing.Color.Black;
            this.txtTotalUnits.Location = new System.Drawing.Point(119, 133);
            this.txtTotalUnits.MaxLength = 18;
            this.txtTotalUnits.Name = "txtTotalUnits";
            this.txtTotalUnits.ReadOnly = true;
            this.txtTotalUnits.Size = new System.Drawing.Size(94, 22);
            this.txtTotalUnits.TabIndex = 141;
            // 
            // lblTotalUnits
            // 
            this.lblTotalUnits.AutoSize = true;
            this.lblTotalUnits.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalUnits.Location = new System.Drawing.Point(33, 137);
            this.lblTotalUnits.Name = "lblTotalUnits";
            this.lblTotalUnits.Size = new System.Drawing.Size(81, 14);
            this.lblTotalUnits.TabIndex = 13;
            this.lblTotalUnits.Text = "Total Units :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEndTime);
            this.groupBox1.Controls.Add(this.txtStartTime);
            this.groupBox1.Controls.Add(this.chkAutoCalculate);
            this.groupBox1.Controls.Add(this.mskEndDate);
            this.groupBox1.Controls.Add(this.mskStartDate);
            this.groupBox1.Controls.Add(this.mskEndTime);
            this.groupBox1.Controls.Add(this.mskStartTime);
            this.groupBox1.Controls.Add(this.lblStartTime);
            this.groupBox1.Controls.Add(this.lblEndTime);
            this.groupBox1.Controls.Add(this.txtTotalMinutes);
            this.groupBox1.Controls.Add(this.lblTotalMinutes);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox1.Location = new System.Drawing.Point(18, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 111);
            this.groupBox1.TabIndex = 158;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Minutes";
            // 
            // txtEndTime
            // 
            this.txtEndTime.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtEndTime.Location = new System.Drawing.Point(265, 47);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.ReadOnly = true;
            this.txtEndTime.Size = new System.Drawing.Size(70, 22);
            this.txtEndTime.TabIndex = 168;
            // 
            // txtStartTime
            // 
            this.txtStartTime.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtStartTime.Location = new System.Drawing.Point(265, 21);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.ReadOnly = true;
            this.txtStartTime.Size = new System.Drawing.Size(70, 22);
            this.txtStartTime.TabIndex = 167;
            // 
            // chkAutoCalculate
            // 
            this.chkAutoCalculate.AutoSize = true;
            this.chkAutoCalculate.Checked = true;
            this.chkAutoCalculate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCalculate.Location = new System.Drawing.Point(219, 78);
            this.chkAutoCalculate.Name = "chkAutoCalculate";
            this.chkAutoCalculate.Size = new System.Drawing.Size(116, 18);
            this.chkAutoCalculate.TabIndex = 166;
            this.chkAutoCalculate.Text = "Auto Calculate";
            this.chkAutoCalculate.UseVisualStyleBackColor = true;
            this.chkAutoCalculate.CheckedChanged += new System.EventHandler(this.chkAutoCalculate_CheckedChanged);
            // 
            // mskEndDate
            // 
            this.mskEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mskEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskEndDate.Location = new System.Drawing.Point(169, 47);
            this.mskEndDate.Mask = "00/00/0000";
            this.mskEndDate.Name = "mskEndDate";
            this.mskEndDate.Size = new System.Drawing.Size(90, 22);
            this.mskEndDate.TabIndex = 165;
            this.mskEndDate.Tag = "Close Date";
            this.mskEndDate.ValidatingType = typeof(System.DateTime);
            this.mskEndDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskEndDate_MouseClick);
            this.mskEndDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskEndDate_Validating);
            // 
            // mskStartDate
            // 
            this.mskStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mskStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskStartDate.Location = new System.Drawing.Point(169, 21);
            this.mskStartDate.Mask = "00/00/0000";
            this.mskStartDate.Name = "mskStartDate";
            this.mskStartDate.Size = new System.Drawing.Size(90, 22);
            this.mskStartDate.TabIndex = 164;
            this.mskStartDate.Tag = "Close Date";
            this.mskStartDate.ValidatingType = typeof(System.DateTime);
            this.mskStartDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskStartDate_MouseClick);
            this.mskStartDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskStartDate_Validating);
            // 
            // mskEndTime
            // 
            this.mskEndTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskEndTime.Location = new System.Drawing.Point(119, 47);
            this.mskEndTime.Mask = "0000";
            this.mskEndTime.Name = "mskEndTime";
            this.mskEndTime.Size = new System.Drawing.Size(44, 22);
            this.mskEndTime.TabIndex = 163;
            this.mskEndTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskEndTime_MouseClick);
            this.mskEndTime.TextChanged += new System.EventHandler(this.mskEndTime_TextChanged);
            this.mskEndTime.Validating += new System.ComponentModel.CancelEventHandler(this.mskEndTime_Validating);
            // 
            // mskStartTime
            // 
            this.mskStartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskStartTime.Location = new System.Drawing.Point(119, 21);
            this.mskStartTime.Mask = "0000";
            this.mskStartTime.Name = "mskStartTime";
            this.mskStartTime.Size = new System.Drawing.Size(44, 22);
            this.mskStartTime.TabIndex = 162;
            this.mskStartTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskStartTime_MouseClick);
            this.mskStartTime.TextChanged += new System.EventHandler(this.mskStartTime_TextChanged);
            this.mskStartTime.Validating += new System.ComponentModel.CancelEventHandler(this.mskStartTime_Validating);
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartTime.Location = new System.Drawing.Point(41, 24);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(73, 14);
            this.lblStartTime.TabIndex = 6;
            this.lblStartTime.Text = "Start Time :";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndTime.Location = new System.Drawing.Point(47, 52);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(67, 14);
            this.lblEndTime.TabIndex = 7;
            this.lblEndTime.Text = "End Time :";
            // 
            // txtTotalMinutes
            // 
            this.txtTotalMinutes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalMinutes.ForeColor = System.Drawing.Color.Black;
            this.txtTotalMinutes.Location = new System.Drawing.Point(119, 76);
            this.txtTotalMinutes.MaxLength = 3;
            this.txtTotalMinutes.Name = "txtTotalMinutes";
            this.txtTotalMinutes.ReadOnly = true;
            this.txtTotalMinutes.Size = new System.Drawing.Size(94, 22);
            this.txtTotalMinutes.TabIndex = 136;
            this.txtTotalMinutes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalMinutes_KeyDown);
            this.txtTotalMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalMinutes_KeyPress);
            this.txtTotalMinutes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtTotalMinutes_MouseUp);
            // 
            // lblTotalMinutes
            // 
            this.lblTotalMinutes.AutoSize = true;
            this.lblTotalMinutes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalMinutes.Location = new System.Drawing.Point(15, 80);
            this.lblTotalMinutes.Name = "lblTotalMinutes";
            this.lblTotalMinutes.Size = new System.Drawing.Size(99, 14);
            this.lblTotalMinutes.TabIndex = 8;
            this.lblTotalMinutes.Text = "Total Minutes :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(478, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 350);
            this.label4.TabIndex = 155;
            this.label4.Text = "Start Time :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 350);
            this.label3.TabIndex = 154;
            this.label3.Text = "Start Time :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 354);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(476, 1);
            this.label2.TabIndex = 153;
            this.label2.Text = "Start Time :";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(476, 1);
            this.label1.TabIndex = 152;
            this.label1.Text = "Start Time :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(245, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 14);
            this.label9.TabIndex = 147;
            this.label9.Text = "Description";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(208, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 14);
            this.label8.TabIndex = 149;
            this.label8.Text = "M2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(170, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 14);
            this.label7.TabIndex = 148;
            this.label7.Text = "M1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(101, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 14);
            this.label6.TabIndex = 151;
            this.label6.Text = "CPT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 14);
            this.label5.TabIndex = 150;
            this.label5.Text = "DOS";
            // 
            // lblDOSText
            // 
            this.lblDOSText.BackColor = System.Drawing.Color.Transparent;
            this.lblDOSText.Location = new System.Drawing.Point(19, 34);
            this.lblDOSText.Name = "lblDOSText";
            this.lblDOSText.Size = new System.Drawing.Size(73, 14);
            this.lblDOSText.TabIndex = 146;
            this.lblDOSText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPTDescText
            // 
            this.lblCPTDescText.AutoEllipsis = true;
            this.lblCPTDescText.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTDescText.Location = new System.Drawing.Point(245, 34);
            this.lblCPTDescText.Name = "lblCPTDescText";
            this.lblCPTDescText.Size = new System.Drawing.Size(215, 14);
            this.lblCPTDescText.TabIndex = 145;
            this.lblCPTDescText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMod2Text
            // 
            this.lblMod2Text.BackColor = System.Drawing.Color.Transparent;
            this.lblMod2Text.Location = new System.Drawing.Point(208, 34);
            this.lblMod2Text.Name = "lblMod2Text";
            this.lblMod2Text.Size = new System.Drawing.Size(31, 14);
            this.lblMod2Text.TabIndex = 144;
            this.lblMod2Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMod1Text
            // 
            this.lblMod1Text.BackColor = System.Drawing.Color.Transparent;
            this.lblMod1Text.Location = new System.Drawing.Point(170, 34);
            this.lblMod1Text.Name = "lblMod1Text";
            this.lblMod1Text.Size = new System.Drawing.Size(31, 14);
            this.lblMod1Text.TabIndex = 143;
            this.lblMod1Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPTCodeText
            // 
            this.lblCPTCodeText.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTCodeText.Location = new System.Drawing.Point(101, 34);
            this.lblCPTCodeText.Name = "lblCPTCodeText";
            this.lblCPTCodeText.Size = new System.Drawing.Size(67, 14);
            this.lblCPTCodeText.TabIndex = 142;
            this.lblCPTCodeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tls_Notes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(482, 54);
            this.panel1.TabIndex = 3;
            this.panel1.TabStop = true;
            // 
            // tls_Notes
            // 
            this.tls_Notes.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_Notes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Notes.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Notes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_SaveCls,
            this.tlb_Close});
            this.tls_Notes.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Notes.Location = new System.Drawing.Point(0, 0);
            this.tls_Notes.Name = "tls_Notes";
            this.tls_Notes.Size = new System.Drawing.Size(482, 53);
            this.tls_Notes.TabIndex = 0;
            this.tls_Notes.TabStop = true;
            this.tls_Notes.Text = "toolStrip1";
            // 
            // tlb_SaveCls
            // 
            this.tlb_SaveCls.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_SaveCls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_SaveCls.Image = ((System.Drawing.Image)(resources.GetObject("tlb_SaveCls.Image")));
            this.tlb_SaveCls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_SaveCls.Name = "tlb_SaveCls";
            this.tlb_SaveCls.Size = new System.Drawing.Size(66, 50);
            this.tlb_SaveCls.Tag = "Save and Close";
            this.tlb_SaveCls.Text = "Sa&ve&&Cls";
            this.tlb_SaveCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_SaveCls.ToolTipText = "Save and Close";
            this.tlb_SaveCls.Click += new System.EventHandler(this.tlb_SaveCls_Click);
            // 
            // tlb_Close
            // 
            this.tlb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Close.Image")));
            this.tlb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Close.Name = "tlb_Close";
            this.tlb_Close.Size = new System.Drawing.Size(43, 50);
            this.tlb_Close.Tag = "Cancel";
            this.tlb_Close.Text = "&Close";
            this.tlb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Close.ToolTipText = "Close";
            this.tlb_Close.Click += new System.EventHandler(this.tlb_Close_Click);
            // 
            // frmAnesthesiaBilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(482, 412);
            this.Controls.Add(this.PnlFields);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAnesthesiaBilling";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "Anesthesia Billing";
            this.Text = "Anesthesia Billing";
            this.Load += new System.EventHandler(this.frmAnesthesiaBilling_Load);
            this.PnlFields.ResumeLayout(false);
            this.PnlFields.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tls_Notes.ResumeLayout(false);
            this.tls_Notes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlFields;
        private System.Windows.Forms.Panel panel1;
        private gloGlobal.gloToolStripIgnoreFocus tls_Notes;
        private System.Windows.Forms.ToolStripButton tlb_SaveCls;
        private System.Windows.Forms.ToolStripButton tlb_Close;
        private System.Windows.Forms.Label lblTotalUnits;
        private System.Windows.Forms.Label lblOtherUnits;
        private System.Windows.Forms.Label lblBaseUnits;
        private System.Windows.Forms.Label lblTimeUnits;
        private System.Windows.Forms.Label lblMinPerUnit;
        private System.Windows.Forms.Label lblTotalMinutes;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.TextBox txtTotalUnits;
        private System.Windows.Forms.TextBox txtOtherUnits;
        private System.Windows.Forms.TextBox txtBaseUnits;
        private System.Windows.Forms.TextBox txtTimeUnits;
        private System.Windows.Forms.TextBox txtMinPerUnit;
        private System.Windows.Forms.TextBox txtTotalMinutes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDOSText;
        private System.Windows.Forms.Label lblCPTDescText;
        private System.Windows.Forms.Label lblMod2Text;
        private System.Windows.Forms.Label lblMod1Text;
        private System.Windows.Forms.Label lblCPTCodeText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MaskedTextBox mskStartTime;
        private System.Windows.Forms.MaskedTextBox mskEndTime;
        private System.Windows.Forms.MaskedTextBox mskEndDate;
        private System.Windows.Forms.MaskedTextBox mskStartDate;
        private System.Windows.Forms.CheckBox chkAutoCalculate;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.TextBox txtEndTime;
    }
}