namespace gloBilling
{
    partial class frmSavePaymentDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSavePaymentDialog));
            this.lblTopMsg = new System.Windows.Forms.Label();
            this.lblSelect = new System.Windows.Forms.Label();
            this.chkPendInsPayment = new System.Windows.Forms.CheckBox();
            this.chkNewInsPayment = new System.Windows.Forms.CheckBox();
            this.btnPmntOk = new System.Windows.Forms.Button();
            this.btnPmntCancel = new System.Windows.Forms.Button();
            this.pnlPayment = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCorrection = new System.Windows.Forms.Panel();
            this.chkCorrOrgPayment = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCorrCancel = new System.Windows.Forms.Button();
            this.btnCorrOk = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkCorrNewInsPmnt = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkCorrPenInsPmnt = new System.Windows.Forms.CheckBox();
            this.pnlReserves = new System.Windows.Forms.Panel();
            this.chkResOrgInsPmnt = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnResCancel = new System.Windows.Forms.Button();
            this.btnResOk = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.chkResNewInsPmnt = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.chkResPenInsPmnt = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlPayment.SuspendLayout();
            this.pnlCorrection.SuspendLayout();
            this.pnlReserves.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTopMsg
            // 
            this.lblTopMsg.AutoSize = true;
            this.lblTopMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTopMsg.Location = new System.Drawing.Point(21, 19);
            this.lblTopMsg.Name = "lblTopMsg";
            this.lblTopMsg.Size = new System.Drawing.Size(617, 14);
            this.lblTopMsg.TabIndex = 0;
            this.lblTopMsg.Text = "Saving Claim changes must be done under an Insurance Payment, but there is no Pay" +
                "ment chosen.";
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSelect.Location = new System.Drawing.Point(21, 65);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(160, 14);
            this.lblSelect.TabIndex = 1;
            this.lblSelect.Text = "Select one of the following:";
            // 
            // chkPendInsPayment
            // 
            this.chkPendInsPayment.AutoSize = true;
            this.chkPendInsPayment.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkPendInsPayment.Location = new System.Drawing.Point(21, 90);
            this.chkPendInsPayment.Name = "chkPendInsPayment";
            this.chkPendInsPayment.Size = new System.Drawing.Size(227, 18);
            this.chkPendInsPayment.TabIndex = 1;
            this.chkPendInsPayment.Text = "Select a Pending Insurance Payment";
            this.chkPendInsPayment.UseVisualStyleBackColor = true;
            this.chkPendInsPayment.CheckedChanged += new System.EventHandler(this.chkPendInsPayment_CheckedChanged);
            // 
            // chkNewInsPayment
            // 
            this.chkNewInsPayment.AutoSize = true;
            this.chkNewInsPayment.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkNewInsPayment.Location = new System.Drawing.Point(21, 113);
            this.chkNewInsPayment.Name = "chkNewInsPayment";
            this.chkNewInsPayment.Size = new System.Drawing.Size(200, 18);
            this.chkNewInsPayment.TabIndex = 2;
            this.chkNewInsPayment.Text = "Start a new Insurance Payment";
            this.chkNewInsPayment.UseVisualStyleBackColor = true;
            this.chkNewInsPayment.CheckedChanged += new System.EventHandler(this.chkNewInsPayment_CheckedChanged);
            // 
            // btnPmntOk
            // 
            this.btnPmntOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPmntOk.BackgroundImage")));
            this.btnPmntOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPmntOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPmntOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPmntOk.Location = new System.Drawing.Point(232, 159);
            this.btnPmntOk.Name = "btnPmntOk";
            this.btnPmntOk.Size = new System.Drawing.Size(75, 23);
            this.btnPmntOk.TabIndex = 3;
            this.btnPmntOk.Text = "Ok";
            this.btnPmntOk.UseVisualStyleBackColor = true;
            this.btnPmntOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnPmntCancel
            // 
            this.btnPmntCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPmntCancel.BackgroundImage")));
            this.btnPmntCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPmntCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPmntCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPmntCancel.Location = new System.Drawing.Point(317, 159);
            this.btnPmntCancel.Name = "btnPmntCancel";
            this.btnPmntCancel.Size = new System.Drawing.Size(75, 23);
            this.btnPmntCancel.TabIndex = 4;
            this.btnPmntCancel.Text = "Cancel";
            this.btnPmntCancel.UseVisualStyleBackColor = true;
            this.btnPmntCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlPayment
            // 
            this.pnlPayment.Controls.Add(this.label4);
            this.pnlPayment.Controls.Add(this.btnPmntCancel);
            this.pnlPayment.Controls.Add(this.btnPmntOk);
            this.pnlPayment.Controls.Add(this.label3);
            this.pnlPayment.Controls.Add(this.label2);
            this.pnlPayment.Controls.Add(this.lblSelect);
            this.pnlPayment.Controls.Add(this.chkNewInsPayment);
            this.pnlPayment.Controls.Add(this.lblTopMsg);
            this.pnlPayment.Controls.Add(this.label1);
            this.pnlPayment.Controls.Add(this.chkPendInsPayment);
            this.pnlPayment.Location = new System.Drawing.Point(3, 3);
            this.pnlPayment.Name = "pnlPayment";
            this.pnlPayment.Size = new System.Drawing.Size(660, 202);
            this.pnlPayment.TabIndex = 1;
            this.pnlPayment.TabStop = true;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(1, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(658, 1);
            this.label4.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(658, 1);
            this.label3.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(659, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 202);
            this.label2.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 202);
            this.label1.TabIndex = 7;
            // 
            // pnlCorrection
            // 
            this.pnlCorrection.Controls.Add(this.chkCorrOrgPayment);
            this.pnlCorrection.Controls.Add(this.label5);
            this.pnlCorrection.Controls.Add(this.btnCorrCancel);
            this.pnlCorrection.Controls.Add(this.btnCorrOk);
            this.pnlCorrection.Controls.Add(this.label6);
            this.pnlCorrection.Controls.Add(this.label7);
            this.pnlCorrection.Controls.Add(this.label8);
            this.pnlCorrection.Controls.Add(this.chkCorrNewInsPmnt);
            this.pnlCorrection.Controls.Add(this.label9);
            this.pnlCorrection.Controls.Add(this.label10);
            this.pnlCorrection.Controls.Add(this.chkCorrPenInsPmnt);
            this.pnlCorrection.Location = new System.Drawing.Point(669, 3);
            this.pnlCorrection.Name = "pnlCorrection";
            this.pnlCorrection.Size = new System.Drawing.Size(660, 202);
            this.pnlCorrection.TabIndex = 6;
            this.pnlCorrection.TabStop = true;
            // 
            // chkCorrOrgPayment
            // 
            this.chkCorrOrgPayment.AutoSize = true;
            this.chkCorrOrgPayment.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkCorrOrgPayment.Location = new System.Drawing.Point(21, 78);
            this.chkCorrOrgPayment.Name = "chkCorrOrgPayment";
            this.chkCorrOrgPayment.Size = new System.Drawing.Size(256, 18);
            this.chkCorrOrgPayment.TabIndex = 7;
            this.chkCorrOrgPayment.Text = "Save Changes under the Original Payment";
            this.chkCorrOrgPayment.UseVisualStyleBackColor = true;
            this.chkCorrOrgPayment.CheckedChanged += new System.EventHandler(this.chkCorrOrgPayment_CheckedChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(1, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(658, 1);
            this.label5.TabIndex = 10;
            // 
            // btnCorrCancel
            // 
            this.btnCorrCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCorrCancel.BackgroundImage")));
            this.btnCorrCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCorrCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorrCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorrCancel.Location = new System.Drawing.Point(317, 165);
            this.btnCorrCancel.Name = "btnCorrCancel";
            this.btnCorrCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCorrCancel.TabIndex = 10;
            this.btnCorrCancel.Text = "Cancel";
            this.btnCorrCancel.UseVisualStyleBackColor = true;
            this.btnCorrCancel.Click += new System.EventHandler(this.btnCorrCancel_Click);
            // 
            // btnCorrOk
            // 
            this.btnCorrOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCorrOk.BackgroundImage")));
            this.btnCorrOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCorrOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorrOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorrOk.Location = new System.Drawing.Point(232, 165);
            this.btnCorrOk.Name = "btnCorrOk";
            this.btnCorrOk.Size = new System.Drawing.Size(75, 23);
            this.btnCorrOk.TabIndex = 11;
            this.btnCorrOk.Text = "Ok";
            this.btnCorrOk.UseVisualStyleBackColor = true;
            this.btnCorrOk.Click += new System.EventHandler(this.btnCorrOk_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(658, 1);
            this.label6.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(659, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 202);
            this.label7.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(21, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(160, 14);
            this.label8.TabIndex = 1;
            this.label8.Text = "Select one of the following:";
            // 
            // chkCorrNewInsPmnt
            // 
            this.chkCorrNewInsPmnt.AutoSize = true;
            this.chkCorrNewInsPmnt.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkCorrNewInsPmnt.Location = new System.Drawing.Point(21, 125);
            this.chkCorrNewInsPmnt.Name = "chkCorrNewInsPmnt";
            this.chkCorrNewInsPmnt.Size = new System.Drawing.Size(200, 18);
            this.chkCorrNewInsPmnt.TabIndex = 9;
            this.chkCorrNewInsPmnt.Text = "Start a new Insurance Payment";
            this.chkCorrNewInsPmnt.UseVisualStyleBackColor = true;
            this.chkCorrNewInsPmnt.CheckedChanged += new System.EventHandler(this.chkCorrNewInsPmnt_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(21, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(596, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "Saving Claim Changes must be done under an Insurance Payment, but there is no Pay" +
                "ment, yet";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 202);
            this.label10.TabIndex = 7;
            // 
            // chkCorrPenInsPmnt
            // 
            this.chkCorrPenInsPmnt.AutoSize = true;
            this.chkCorrPenInsPmnt.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkCorrPenInsPmnt.Location = new System.Drawing.Point(21, 103);
            this.chkCorrPenInsPmnt.Name = "chkCorrPenInsPmnt";
            this.chkCorrPenInsPmnt.Size = new System.Drawing.Size(227, 18);
            this.chkCorrPenInsPmnt.TabIndex = 8;
            this.chkCorrPenInsPmnt.Text = "Select a Pending Insurance Payment";
            this.chkCorrPenInsPmnt.UseVisualStyleBackColor = true;
            this.chkCorrPenInsPmnt.CheckedChanged += new System.EventHandler(this.chkCorrPenInsPmnt_CheckedChanged);
            // 
            // pnlReserves
            // 
            this.pnlReserves.Controls.Add(this.chkResOrgInsPmnt);
            this.pnlReserves.Controls.Add(this.label11);
            this.pnlReserves.Controls.Add(this.btnResCancel);
            this.pnlReserves.Controls.Add(this.btnResOk);
            this.pnlReserves.Controls.Add(this.label12);
            this.pnlReserves.Controls.Add(this.label13);
            this.pnlReserves.Controls.Add(this.label14);
            this.pnlReserves.Controls.Add(this.chkResNewInsPmnt);
            this.pnlReserves.Controls.Add(this.label15);
            this.pnlReserves.Controls.Add(this.label16);
            this.pnlReserves.Controls.Add(this.chkResPenInsPmnt);
            this.pnlReserves.Location = new System.Drawing.Point(1335, 3);
            this.pnlReserves.Name = "pnlReserves";
            this.pnlReserves.Size = new System.Drawing.Size(660, 202);
            this.pnlReserves.TabIndex = 3;
            this.pnlReserves.TabStop = true;
            // 
            // chkResOrgInsPmnt
            // 
            this.chkResOrgInsPmnt.AutoSize = true;
            this.chkResOrgInsPmnt.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkResOrgInsPmnt.Location = new System.Drawing.Point(21, 81);
            this.chkResOrgInsPmnt.Name = "chkResOrgInsPmnt";
            this.chkResOrgInsPmnt.Size = new System.Drawing.Size(359, 18);
            this.chkResOrgInsPmnt.TabIndex = 2;
            this.chkResOrgInsPmnt.Text = "Allocate the Reserves under their original Insurance Payment";
            this.chkResOrgInsPmnt.UseVisualStyleBackColor = true;
            this.chkResOrgInsPmnt.CheckedChanged += new System.EventHandler(this.chkResOrgInsPmnt_CheckedChanged);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(1, 201);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(658, 1);
            this.label11.TabIndex = 10;
            // 
            // btnResCancel
            // 
            this.btnResCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnResCancel.BackgroundImage")));
            this.btnResCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResCancel.Location = new System.Drawing.Point(317, 161);
            this.btnResCancel.Name = "btnResCancel";
            this.btnResCancel.Size = new System.Drawing.Size(75, 23);
            this.btnResCancel.TabIndex = 5;
            this.btnResCancel.Text = "Cancel";
            this.btnResCancel.UseVisualStyleBackColor = true;
            this.btnResCancel.Click += new System.EventHandler(this.btnResCancel_Click);
            // 
            // btnResOk
            // 
            this.btnResOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnResOk.BackgroundImage")));
            this.btnResOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResOk.Location = new System.Drawing.Point(232, 161);
            this.btnResOk.Name = "btnResOk";
            this.btnResOk.Size = new System.Drawing.Size(75, 23);
            this.btnResOk.TabIndex = 5;
            this.btnResOk.Text = "Ok";
            this.btnResOk.UseVisualStyleBackColor = true;
            this.btnResOk.Click += new System.EventHandler(this.btnResOk_Click);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(1, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(658, 1);
            this.label12.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(659, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 202);
            this.label13.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label14.Location = new System.Drawing.Point(21, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(160, 14);
            this.label14.TabIndex = 1;
            this.label14.Text = "Select one of the following:";
            // 
            // chkResNewInsPmnt
            // 
            this.chkResNewInsPmnt.AutoSize = true;
            this.chkResNewInsPmnt.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkResNewInsPmnt.Location = new System.Drawing.Point(21, 128);
            this.chkResNewInsPmnt.Name = "chkResNewInsPmnt";
            this.chkResNewInsPmnt.Size = new System.Drawing.Size(200, 18);
            this.chkResNewInsPmnt.TabIndex = 3;
            this.chkResNewInsPmnt.Text = "Start a new Insurance Payment";
            this.chkResNewInsPmnt.UseVisualStyleBackColor = true;
            this.chkResNewInsPmnt.CheckedChanged += new System.EventHandler(this.chkResNewInsPmnt_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(21, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(324, 14);
            this.label15.TabIndex = 0;
            this.label15.Text = "Reserves are to be linked to an Insurance Payment.";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 202);
            this.label16.TabIndex = 7;
            // 
            // chkResPenInsPmnt
            // 
            this.chkResPenInsPmnt.AutoSize = true;
            this.chkResPenInsPmnt.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkResPenInsPmnt.Location = new System.Drawing.Point(21, 105);
            this.chkResPenInsPmnt.Name = "chkResPenInsPmnt";
            this.chkResPenInsPmnt.Size = new System.Drawing.Size(227, 18);
            this.chkResPenInsPmnt.TabIndex = 4;
            this.chkResPenInsPmnt.Text = "Select a Pending Insurance Payment";
            this.chkResPenInsPmnt.UseVisualStyleBackColor = true;
            this.chkResPenInsPmnt.CheckedChanged += new System.EventHandler(this.chkResPenInsPmnt_CheckedChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.pnlPayment);
            this.flowLayoutPanel2.Controls.Add(this.pnlCorrection);
            this.flowLayoutPanel2.Controls.Add(this.pnlReserves);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(667, 208);
            this.flowLayoutPanel2.TabIndex = 13;
            this.flowLayoutPanel2.TabStop = true;
            // 
            // frmSavePaymentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(667, 208);
            this.Controls.Add(this.flowLayoutPanel2);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSavePaymentDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "A Payment is Required";
            this.Load += new System.EventHandler(this.frmSavePaymentDialog_Load);
            this.pnlPayment.ResumeLayout(false);
            this.pnlPayment.PerformLayout();
            this.pnlCorrection.ResumeLayout(false);
            this.pnlCorrection.PerformLayout();
            this.pnlReserves.ResumeLayout(false);
            this.pnlReserves.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTopMsg;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.CheckBox chkPendInsPayment;
        private System.Windows.Forms.CheckBox chkNewInsPayment;
        private System.Windows.Forms.Button btnPmntOk;
        private System.Windows.Forms.Button btnPmntCancel;
        private System.Windows.Forms.Panel pnlPayment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlCorrection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCorrCancel;
        private System.Windows.Forms.Button btnCorrOk;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkCorrNewInsPmnt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkCorrPenInsPmnt;
        private System.Windows.Forms.Panel pnlReserves;
        private System.Windows.Forms.CheckBox chkResOrgInsPmnt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnResCancel;
        private System.Windows.Forms.Button btnResOk;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkResNewInsPmnt;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox chkResPenInsPmnt;
        private System.Windows.Forms.CheckBox chkCorrOrgPayment;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}