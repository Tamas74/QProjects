namespace gloEmdeonInterface.Forms
{
    partial class FrmMatchConfirmation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMatchConfirmation));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblUnmatchPatient = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblUnmatchPatientSSNValue = new System.Windows.Forms.Label();
            this.lblUnmatchPatientGenderValue = new System.Windows.Forms.Label();
            this.lblUnmatchPatientDOBValue = new System.Windows.Forms.Label();
            this.lblUnmatchPatientLNameValue = new System.Windows.Forms.Label();
            this.lblUnmatchPatientMNameValue = new System.Windows.Forms.Label();
            this.lblUnmatchPatientFNameValue = new System.Windows.Forms.Label();
            this.lblUnmatchPatientSSN = new System.Windows.Forms.Label();
            this.lblUnmatchPatientGender = new System.Windows.Forms.Label();
            this.lblUnmatchPatientDOB = new System.Windows.Forms.Label();
            this.lblUnmatchPatientLName = new System.Windows.Forms.Label();
            this.lblUnmatchPatientMName = new System.Windows.Forms.Label();
            this.lblUnmatchPatientFName = new System.Windows.Forms.Label();
            this.lblSelectedPatientSSNValue = new System.Windows.Forms.Label();
            this.lblSelectedPatientGenderValue = new System.Windows.Forms.Label();
            this.lblSelectedPatientDOBValue = new System.Windows.Forms.Label();
            this.lblSelectedPatientLNameValue = new System.Windows.Forms.Label();
            this.lblSelectedPatientMNameValue = new System.Windows.Forms.Label();
            this.lblSelectedPatientFNameValue = new System.Windows.Forms.Label();
            this.lblSelectedPatientSSN = new System.Windows.Forms.Label();
            this.lblSelectedPatientGender = new System.Windows.Forms.Label();
            this.lblSelectedPatientDOB = new System.Windows.Forms.Label();
            this.lblSelectedPatientLName = new System.Windows.Forms.Label();
            this.lblSelectedPatientMName = new System.Windows.Forms.Label();
            this.lblSelectedPatientFName = new System.Windows.Forms.Label();
            this.lblSelectedPatient = new System.Windows.Forms.Label();
            this.lblPatientMatching = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_LabMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtn_save = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkIgnoreCase = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.ts_LabMain.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblUnmatchPatientSSNValue);
            this.panel1.Controls.Add(this.lblUnmatchPatientGenderValue);
            this.panel1.Controls.Add(this.lblUnmatchPatientDOBValue);
            this.panel1.Controls.Add(this.lblUnmatchPatientLNameValue);
            this.panel1.Controls.Add(this.lblUnmatchPatientMNameValue);
            this.panel1.Controls.Add(this.lblUnmatchPatientFNameValue);
            this.panel1.Controls.Add(this.lblUnmatchPatientSSN);
            this.panel1.Controls.Add(this.lblUnmatchPatientGender);
            this.panel1.Controls.Add(this.lblUnmatchPatientDOB);
            this.panel1.Controls.Add(this.lblUnmatchPatientLName);
            this.panel1.Controls.Add(this.lblUnmatchPatientMName);
            this.panel1.Controls.Add(this.lblUnmatchPatientFName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 84);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.panel1.Size = new System.Drawing.Size(297, 244);
            this.panel1.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Location = new System.Drawing.Point(4, 240);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(292, 1);
            this.label13.TabIndex = 58;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(4, 1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(292, 26);
            this.panel4.TabIndex = 29;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.lblUnmatchPatient);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(292, 26);
            this.panel5.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(0, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(292, 1);
            this.label5.TabIndex = 3;
            // 
            // lblUnmatchPatient
            // 
            this.lblUnmatchPatient.BackColor = System.Drawing.Color.Transparent;
            this.lblUnmatchPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUnmatchPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblUnmatchPatient.Location = new System.Drawing.Point(0, 0);
            this.lblUnmatchPatient.Name = "lblUnmatchPatient";
            this.lblUnmatchPatient.Size = new System.Drawing.Size(292, 26);
            this.lblUnmatchPatient.TabIndex = 28;
            this.lblUnmatchPatient.Text = "Unmatched Result Patient";
            this.lblUnmatchPatient.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(296, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 240);
            this.label7.TabIndex = 57;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(293, 1);
            this.label6.TabIndex = 56;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 241);
            this.label8.TabIndex = 55;
            // 
            // lblUnmatchPatientSSNValue
            // 
            this.lblUnmatchPatientSSNValue.AutoSize = true;
            this.lblUnmatchPatientSSNValue.ForeColor = System.Drawing.Color.Green;
            this.lblUnmatchPatientSSNValue.Location = new System.Drawing.Point(99, 202);
            this.lblUnmatchPatientSSNValue.Name = "lblUnmatchPatientSSNValue";
            this.lblUnmatchPatientSSNValue.Size = new System.Drawing.Size(29, 14);
            this.lblUnmatchPatientSSNValue.TabIndex = 41;
            this.lblUnmatchPatientSSNValue.Text = "SSN";
            // 
            // lblUnmatchPatientGenderValue
            // 
            this.lblUnmatchPatientGenderValue.AutoSize = true;
            this.lblUnmatchPatientGenderValue.ForeColor = System.Drawing.Color.Green;
            this.lblUnmatchPatientGenderValue.Location = new System.Drawing.Point(99, 170);
            this.lblUnmatchPatientGenderValue.Name = "lblUnmatchPatientGenderValue";
            this.lblUnmatchPatientGenderValue.Size = new System.Drawing.Size(47, 14);
            this.lblUnmatchPatientGenderValue.TabIndex = 40;
            this.lblUnmatchPatientGenderValue.Text = "Gender";
            // 
            // lblUnmatchPatientDOBValue
            // 
            this.lblUnmatchPatientDOBValue.AutoSize = true;
            this.lblUnmatchPatientDOBValue.ForeColor = System.Drawing.Color.Green;
            this.lblUnmatchPatientDOBValue.Location = new System.Drawing.Point(99, 138);
            this.lblUnmatchPatientDOBValue.Name = "lblUnmatchPatientDOBValue";
            this.lblUnmatchPatientDOBValue.Size = new System.Drawing.Size(31, 14);
            this.lblUnmatchPatientDOBValue.TabIndex = 39;
            this.lblUnmatchPatientDOBValue.Text = "DOB";
            // 
            // lblUnmatchPatientLNameValue
            // 
            this.lblUnmatchPatientLNameValue.AutoSize = true;
            this.lblUnmatchPatientLNameValue.ForeColor = System.Drawing.Color.Green;
            this.lblUnmatchPatientLNameValue.Location = new System.Drawing.Point(99, 106);
            this.lblUnmatchPatientLNameValue.Name = "lblUnmatchPatientLNameValue";
            this.lblUnmatchPatientLNameValue.Size = new System.Drawing.Size(64, 14);
            this.lblUnmatchPatientLNameValue.TabIndex = 38;
            this.lblUnmatchPatientLNameValue.Text = "Last Name";
            // 
            // lblUnmatchPatientMNameValue
            // 
            this.lblUnmatchPatientMNameValue.AutoSize = true;
            this.lblUnmatchPatientMNameValue.ForeColor = System.Drawing.Color.Green;
            this.lblUnmatchPatientMNameValue.Location = new System.Drawing.Point(99, 74);
            this.lblUnmatchPatientMNameValue.Name = "lblUnmatchPatientMNameValue";
            this.lblUnmatchPatientMNameValue.Size = new System.Drawing.Size(76, 14);
            this.lblUnmatchPatientMNameValue.TabIndex = 37;
            this.lblUnmatchPatientMNameValue.Text = "Middle Name";
            // 
            // lblUnmatchPatientFNameValue
            // 
            this.lblUnmatchPatientFNameValue.AutoSize = true;
            this.lblUnmatchPatientFNameValue.ForeColor = System.Drawing.Color.Green;
            this.lblUnmatchPatientFNameValue.Location = new System.Drawing.Point(99, 42);
            this.lblUnmatchPatientFNameValue.Name = "lblUnmatchPatientFNameValue";
            this.lblUnmatchPatientFNameValue.Size = new System.Drawing.Size(64, 14);
            this.lblUnmatchPatientFNameValue.TabIndex = 36;
            this.lblUnmatchPatientFNameValue.Text = "First Name";
            // 
            // lblUnmatchPatientSSN
            // 
            this.lblUnmatchPatientSSN.AutoSize = true;
            this.lblUnmatchPatientSSN.Location = new System.Drawing.Point(60, 202);
            this.lblUnmatchPatientSSN.Name = "lblUnmatchPatientSSN";
            this.lblUnmatchPatientSSN.Size = new System.Drawing.Size(37, 14);
            this.lblUnmatchPatientSSN.TabIndex = 35;
            this.lblUnmatchPatientSSN.Text = "SSN :";
            // 
            // lblUnmatchPatientGender
            // 
            this.lblUnmatchPatientGender.AutoSize = true;
            this.lblUnmatchPatientGender.Location = new System.Drawing.Point(42, 170);
            this.lblUnmatchPatientGender.Name = "lblUnmatchPatientGender";
            this.lblUnmatchPatientGender.Size = new System.Drawing.Size(55, 14);
            this.lblUnmatchPatientGender.TabIndex = 34;
            this.lblUnmatchPatientGender.Text = "Gender :";
            // 
            // lblUnmatchPatientDOB
            // 
            this.lblUnmatchPatientDOB.AutoSize = true;
            this.lblUnmatchPatientDOB.Location = new System.Drawing.Point(58, 138);
            this.lblUnmatchPatientDOB.Name = "lblUnmatchPatientDOB";
            this.lblUnmatchPatientDOB.Size = new System.Drawing.Size(39, 14);
            this.lblUnmatchPatientDOB.TabIndex = 33;
            this.lblUnmatchPatientDOB.Text = "DOB :";
            // 
            // lblUnmatchPatientLName
            // 
            this.lblUnmatchPatientLName.AutoSize = true;
            this.lblUnmatchPatientLName.Location = new System.Drawing.Point(25, 106);
            this.lblUnmatchPatientLName.Name = "lblUnmatchPatientLName";
            this.lblUnmatchPatientLName.Size = new System.Drawing.Size(72, 14);
            this.lblUnmatchPatientLName.TabIndex = 32;
            this.lblUnmatchPatientLName.Text = "Last Name :";
            // 
            // lblUnmatchPatientMName
            // 
            this.lblUnmatchPatientMName.AutoSize = true;
            this.lblUnmatchPatientMName.Location = new System.Drawing.Point(13, 74);
            this.lblUnmatchPatientMName.Name = "lblUnmatchPatientMName";
            this.lblUnmatchPatientMName.Size = new System.Drawing.Size(84, 14);
            this.lblUnmatchPatientMName.TabIndex = 31;
            this.lblUnmatchPatientMName.Text = "Middle Name :";
            // 
            // lblUnmatchPatientFName
            // 
            this.lblUnmatchPatientFName.AutoSize = true;
            this.lblUnmatchPatientFName.Location = new System.Drawing.Point(25, 42);
            this.lblUnmatchPatientFName.Name = "lblUnmatchPatientFName";
            this.lblUnmatchPatientFName.Size = new System.Drawing.Size(72, 14);
            this.lblUnmatchPatientFName.TabIndex = 30;
            this.lblUnmatchPatientFName.Text = "First Name :";
            // 
            // lblSelectedPatientSSNValue
            // 
            this.lblSelectedPatientSSNValue.AutoSize = true;
            this.lblSelectedPatientSSNValue.ForeColor = System.Drawing.Color.Green;
            this.lblSelectedPatientSSNValue.Location = new System.Drawing.Point(100, 202);
            this.lblSelectedPatientSSNValue.Name = "lblSelectedPatientSSNValue";
            this.lblSelectedPatientSSNValue.Size = new System.Drawing.Size(29, 14);
            this.lblSelectedPatientSSNValue.TabIndex = 53;
            this.lblSelectedPatientSSNValue.Text = "SSN";
            // 
            // lblSelectedPatientGenderValue
            // 
            this.lblSelectedPatientGenderValue.AutoSize = true;
            this.lblSelectedPatientGenderValue.ForeColor = System.Drawing.Color.Green;
            this.lblSelectedPatientGenderValue.Location = new System.Drawing.Point(100, 170);
            this.lblSelectedPatientGenderValue.Name = "lblSelectedPatientGenderValue";
            this.lblSelectedPatientGenderValue.Size = new System.Drawing.Size(47, 14);
            this.lblSelectedPatientGenderValue.TabIndex = 52;
            this.lblSelectedPatientGenderValue.Text = "Gender";
            // 
            // lblSelectedPatientDOBValue
            // 
            this.lblSelectedPatientDOBValue.AutoSize = true;
            this.lblSelectedPatientDOBValue.ForeColor = System.Drawing.Color.Green;
            this.lblSelectedPatientDOBValue.Location = new System.Drawing.Point(100, 138);
            this.lblSelectedPatientDOBValue.Name = "lblSelectedPatientDOBValue";
            this.lblSelectedPatientDOBValue.Size = new System.Drawing.Size(31, 14);
            this.lblSelectedPatientDOBValue.TabIndex = 51;
            this.lblSelectedPatientDOBValue.Text = "DOB";
            // 
            // lblSelectedPatientLNameValue
            // 
            this.lblSelectedPatientLNameValue.AutoSize = true;
            this.lblSelectedPatientLNameValue.ForeColor = System.Drawing.Color.Green;
            this.lblSelectedPatientLNameValue.Location = new System.Drawing.Point(100, 106);
            this.lblSelectedPatientLNameValue.Name = "lblSelectedPatientLNameValue";
            this.lblSelectedPatientLNameValue.Size = new System.Drawing.Size(64, 14);
            this.lblSelectedPatientLNameValue.TabIndex = 50;
            this.lblSelectedPatientLNameValue.Text = "Last Name";
            // 
            // lblSelectedPatientMNameValue
            // 
            this.lblSelectedPatientMNameValue.AutoSize = true;
            this.lblSelectedPatientMNameValue.ForeColor = System.Drawing.Color.Green;
            this.lblSelectedPatientMNameValue.Location = new System.Drawing.Point(100, 74);
            this.lblSelectedPatientMNameValue.Name = "lblSelectedPatientMNameValue";
            this.lblSelectedPatientMNameValue.Size = new System.Drawing.Size(76, 14);
            this.lblSelectedPatientMNameValue.TabIndex = 49;
            this.lblSelectedPatientMNameValue.Text = "Middle Name";
            // 
            // lblSelectedPatientFNameValue
            // 
            this.lblSelectedPatientFNameValue.AutoSize = true;
            this.lblSelectedPatientFNameValue.ForeColor = System.Drawing.Color.Green;
            this.lblSelectedPatientFNameValue.Location = new System.Drawing.Point(100, 42);
            this.lblSelectedPatientFNameValue.Name = "lblSelectedPatientFNameValue";
            this.lblSelectedPatientFNameValue.Size = new System.Drawing.Size(64, 14);
            this.lblSelectedPatientFNameValue.TabIndex = 48;
            this.lblSelectedPatientFNameValue.Text = "First Name";
            // 
            // lblSelectedPatientSSN
            // 
            this.lblSelectedPatientSSN.AutoSize = true;
            this.lblSelectedPatientSSN.Location = new System.Drawing.Point(60, 202);
            this.lblSelectedPatientSSN.Name = "lblSelectedPatientSSN";
            this.lblSelectedPatientSSN.Size = new System.Drawing.Size(37, 14);
            this.lblSelectedPatientSSN.TabIndex = 47;
            this.lblSelectedPatientSSN.Text = "SSN :";
            // 
            // lblSelectedPatientGender
            // 
            this.lblSelectedPatientGender.AutoSize = true;
            this.lblSelectedPatientGender.Location = new System.Drawing.Point(42, 170);
            this.lblSelectedPatientGender.Name = "lblSelectedPatientGender";
            this.lblSelectedPatientGender.Size = new System.Drawing.Size(55, 14);
            this.lblSelectedPatientGender.TabIndex = 46;
            this.lblSelectedPatientGender.Text = "Gender :";
            // 
            // lblSelectedPatientDOB
            // 
            this.lblSelectedPatientDOB.AutoSize = true;
            this.lblSelectedPatientDOB.Location = new System.Drawing.Point(58, 138);
            this.lblSelectedPatientDOB.Name = "lblSelectedPatientDOB";
            this.lblSelectedPatientDOB.Size = new System.Drawing.Size(39, 14);
            this.lblSelectedPatientDOB.TabIndex = 45;
            this.lblSelectedPatientDOB.Text = "DOB :";
            // 
            // lblSelectedPatientLName
            // 
            this.lblSelectedPatientLName.AutoSize = true;
            this.lblSelectedPatientLName.Location = new System.Drawing.Point(25, 106);
            this.lblSelectedPatientLName.Name = "lblSelectedPatientLName";
            this.lblSelectedPatientLName.Size = new System.Drawing.Size(72, 14);
            this.lblSelectedPatientLName.TabIndex = 44;
            this.lblSelectedPatientLName.Text = "Last Name :";
            // 
            // lblSelectedPatientMName
            // 
            this.lblSelectedPatientMName.AutoSize = true;
            this.lblSelectedPatientMName.Location = new System.Drawing.Point(13, 74);
            this.lblSelectedPatientMName.Name = "lblSelectedPatientMName";
            this.lblSelectedPatientMName.Size = new System.Drawing.Size(84, 14);
            this.lblSelectedPatientMName.TabIndex = 43;
            this.lblSelectedPatientMName.Text = "Middle Name :";
            // 
            // lblSelectedPatientFName
            // 
            this.lblSelectedPatientFName.AutoSize = true;
            this.lblSelectedPatientFName.Location = new System.Drawing.Point(25, 42);
            this.lblSelectedPatientFName.Name = "lblSelectedPatientFName";
            this.lblSelectedPatientFName.Size = new System.Drawing.Size(72, 14);
            this.lblSelectedPatientFName.TabIndex = 42;
            this.lblSelectedPatientFName.Text = "First Name :";
            // 
            // lblSelectedPatient
            // 
            this.lblSelectedPatient.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectedPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSelectedPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectedPatient.Location = new System.Drawing.Point(0, 0);
            this.lblSelectedPatient.Name = "lblSelectedPatient";
            this.lblSelectedPatient.Size = new System.Drawing.Size(296, 26);
            this.lblSelectedPatient.TabIndex = 29;
            this.lblSelectedPatient.Text = "Selected Patient To Be Matched";
            this.lblSelectedPatient.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPatientMatching
            // 
            this.lblPatientMatching.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientMatching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatientMatching.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPatientMatching.ForeColor = System.Drawing.Color.White;
            this.lblPatientMatching.Location = new System.Drawing.Point(0, 0);
            this.lblPatientMatching.Name = "lblPatientMatching";
            this.lblPatientMatching.Size = new System.Drawing.Size(595, 24);
            this.lblPatientMatching.TabIndex = 27;
            this.lblPatientMatching.Text = "Patient Matching";
            this.lblPatientMatching.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_LabMain);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(601, 54);
            this.pnlToolStrip.TabIndex = 28;
            // 
            // ts_LabMain
            // 
            this.ts_LabMain.BackColor = System.Drawing.Color.Transparent;
            this.ts_LabMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_LabMain.BackgroundImage")));
            this.ts_LabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_LabMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_LabMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_LabMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_LabMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtn_save,
            this.tlbbtn_Close});
            this.ts_LabMain.Location = new System.Drawing.Point(0, 0);
            this.ts_LabMain.Name = "ts_LabMain";
            this.ts_LabMain.Size = new System.Drawing.Size(601, 53);
            this.ts_LabMain.TabIndex = 0;
            this.ts_LabMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_LabMain_ItemClicked);
            // 
            // tlbbtn_save
            // 
            this.tlbbtn_save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_save.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_save.Image")));
            this.tlbbtn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_save.Name = "tlbbtn_save";
            this.tlbbtn_save.Size = new System.Drawing.Size(40, 50);
            this.tlbbtn_save.Text = "&Save";
            this.tlbbtn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tlbbtn_Close
            // 
            this.tlbbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.Image")));
            this.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Close.Name = "tlbbtn_Close";
            this.tlbbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tlbbtn_Close.Text = "&Close";
            this.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(601, 30);
            this.panel2.TabIndex = 29;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.chkIgnoreCase);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lblPatientMatching);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(595, 24);
            this.panel3.TabIndex = 0;
            // 
            // chkIgnoreCase
            // 
            this.chkIgnoreCase.AutoSize = true;
            this.chkIgnoreCase.BackColor = System.Drawing.Color.Transparent;
            this.chkIgnoreCase.Checked = true;
            this.chkIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreCase.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkIgnoreCase.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIgnoreCase.ForeColor = System.Drawing.Color.White;
            this.chkIgnoreCase.Location = new System.Drawing.Point(500, 1);
            this.chkIgnoreCase.Name = "chkIgnoreCase";
            this.chkIgnoreCase.Size = new System.Drawing.Size(94, 22);
            this.chkIgnoreCase.TabIndex = 1;
            this.chkIgnoreCase.Text = "Ignore Case";
            this.chkIgnoreCase.UseVisualStyleBackColor = false;
            this.chkIgnoreCase.Visible = false;
            this.chkIgnoreCase.CheckedChanged += new System.EventHandler(this.chkIgnoreCase_CheckedChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(1, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(593, 1);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(593, 1);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(594, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 24);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 24);
            this.label1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.label14);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.label11);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Controls.Add(this.lblSelectedPatientFName);
            this.panel6.Controls.Add(this.lblSelectedPatientSSNValue);
            this.panel6.Controls.Add(this.lblSelectedPatientMName);
            this.panel6.Controls.Add(this.lblSelectedPatientGenderValue);
            this.panel6.Controls.Add(this.lblSelectedPatientLName);
            this.panel6.Controls.Add(this.lblSelectedPatientDOBValue);
            this.panel6.Controls.Add(this.lblSelectedPatientDOB);
            this.panel6.Controls.Add(this.lblSelectedPatientLNameValue);
            this.panel6.Controls.Add(this.lblSelectedPatientGender);
            this.panel6.Controls.Add(this.lblSelectedPatientMNameValue);
            this.panel6.Controls.Add(this.lblSelectedPatientSSN);
            this.panel6.Controls.Add(this.lblSelectedPatientFNameValue);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(300, 84);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel6.Size = new System.Drawing.Size(301, 244);
            this.panel6.TabIndex = 30;
            // 
            // panel8
            // 
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.lblSelectedPatient);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(1, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(296, 26);
            this.panel8.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(0, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(296, 1);
            this.label9.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(1, 240);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(296, 1);
            this.label14.TabIndex = 59;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(296, 1);
            this.label10.TabIndex = 58;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Location = new System.Drawing.Point(297, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 241);
            this.label11.TabIndex = 57;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 241);
            this.label12.TabIndex = 56;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(297, 84);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 244);
            this.splitter1.TabIndex = 31;
            this.splitter1.TabStop = false;
            // 
            // FrmMatchConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(601, 328);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMatchConfirmation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unmatched Result Patient Confirmation";
            this.Load += new System.EventHandler(this.FrmMatchConfirmation_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_LabMain.ResumeLayout(false);
            this.ts_LabMain.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSelectedPatientSSNValue;
        private System.Windows.Forms.Label lblSelectedPatientGenderValue;
        private System.Windows.Forms.Label lblSelectedPatientDOBValue;
        private System.Windows.Forms.Label lblSelectedPatientLNameValue;
        private System.Windows.Forms.Label lblSelectedPatientMNameValue;
        private System.Windows.Forms.Label lblSelectedPatientFNameValue;
        private System.Windows.Forms.Label lblSelectedPatientSSN;
        private System.Windows.Forms.Label lblSelectedPatientGender;
        private System.Windows.Forms.Label lblSelectedPatientDOB;
        private System.Windows.Forms.Label lblSelectedPatientLName;
        private System.Windows.Forms.Label lblSelectedPatientMName;
        private System.Windows.Forms.Label lblSelectedPatientFName;
        private System.Windows.Forms.Label lblUnmatchPatientSSNValue;
        private System.Windows.Forms.Label lblUnmatchPatientGenderValue;
        private System.Windows.Forms.Label lblUnmatchPatientDOBValue;
        private System.Windows.Forms.Label lblUnmatchPatientLNameValue;
        private System.Windows.Forms.Label lblUnmatchPatientMNameValue;
        private System.Windows.Forms.Label lblUnmatchPatientFNameValue;
        private System.Windows.Forms.Label lblUnmatchPatientSSN;
        private System.Windows.Forms.Label lblUnmatchPatientGender;
        private System.Windows.Forms.Label lblUnmatchPatientDOB;
        private System.Windows.Forms.Label lblUnmatchPatientLName;
        private System.Windows.Forms.Label lblUnmatchPatientMName;
        private System.Windows.Forms.Label lblUnmatchPatientFName;
        private System.Windows.Forms.Label lblSelectedPatient;
        private System.Windows.Forms.Label lblUnmatchPatient;
        private System.Windows.Forms.Label lblPatientMatching;
        internal System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_LabMain;
        internal System.Windows.Forms.ToolStripButton tlbbtn_save;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.CheckBox chkIgnoreCase;

    }
}