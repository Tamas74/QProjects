namespace gloBilling.Collections
{
    partial class frmSetupFollowupDateActionTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupFollowupDateActionTemplate));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Generate = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.grpScedule = new System.Windows.Forms.GroupBox();
            this.cmbScheduleFollowup = new System.Windows.Forms.ComboBox();
            this.mskScheduleFollowupDate = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkScheduleFollowup = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.lblLogAction = new System.Windows.Forms.Label();
            this.cmbLogAction = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pnlPatDetails = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlProgressBar = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblFileCounter = new System.Windows.Forms.Label();
            this.prgFileGeneration = new System.Windows.Forms.ProgressBar();
            this.label38 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            //this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.ts_Commands.SuspendLayout();
            this.grpScedule.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlPatDetails.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlProgressBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Generate,
            this.tsb_Print,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(519, 53);
            this.ts_Commands.TabIndex = 1;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_Generate
            // 
            this.tsb_Generate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Generate.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Generate.Image")));
            this.tsb_Generate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Generate.Name = "tsb_Generate";
            this.tsb_Generate.Size = new System.Drawing.Size(66, 50);
            this.tsb_Generate.Tag = "Generate";
            this.tsb_Generate.Text = "&Generate";
            this.tsb_Generate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Generate.ToolTipText = "Generate";
            this.tsb_Generate.Click += new System.EventHandler(this.tsb_Generate_Click);
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(41, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = "&Print";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.ToolTipText = "Print";
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
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
            // grpScedule
            // 
            this.grpScedule.Controls.Add(this.cmbScheduleFollowup);
            this.grpScedule.Controls.Add(this.mskScheduleFollowupDate);
            this.grpScedule.Controls.Add(this.label1);
            this.grpScedule.Controls.Add(this.label2);
            this.grpScedule.Controls.Add(this.chkScheduleFollowup);
            this.grpScedule.Location = new System.Drawing.Point(11, 77);
            this.grpScedule.Name = "grpScedule";
            this.grpScedule.Size = new System.Drawing.Size(498, 66);
            this.grpScedule.TabIndex = 16;
            this.grpScedule.TabStop = false;
            // 
            // cmbScheduleFollowup
            // 
            this.cmbScheduleFollowup.BackColor = System.Drawing.SystemColors.Window;
            this.cmbScheduleFollowup.DropDownHeight = 100;
            this.cmbScheduleFollowup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScheduleFollowup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbScheduleFollowup.FormattingEnabled = true;
            this.cmbScheduleFollowup.IntegralHeight = false;
            this.cmbScheduleFollowup.Location = new System.Drawing.Point(65, 29);
            this.cmbScheduleFollowup.Name = "cmbScheduleFollowup";
            this.cmbScheduleFollowup.Size = new System.Drawing.Size(294, 22);
            this.cmbScheduleFollowup.TabIndex = 221;
            this.cmbScheduleFollowup.MouseEnter += new System.EventHandler(this.cmbScheduleFollowup_MouseEnter);
            // 
            // mskScheduleFollowupDate
            // 
            this.mskScheduleFollowupDate.Location = new System.Drawing.Point(407, 29);
            this.mskScheduleFollowupDate.Mask = "00/00/0000";
            this.mskScheduleFollowupDate.Name = "mskScheduleFollowupDate";
            this.mskScheduleFollowupDate.Size = new System.Drawing.Size(79, 22);
            this.mskScheduleFollowupDate.TabIndex = 219;
            this.mskScheduleFollowupDate.ValidatingType = typeof(System.DateTime);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(363, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 14);
            this.label1.TabIndex = 220;
            this.label1.Text = "Date :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(12, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 218;
            this.label2.Text = "Action :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkScheduleFollowup
            // 
            this.chkScheduleFollowup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.chkScheduleFollowup.Location = new System.Drawing.Point(5, 0);
            this.chkScheduleFollowup.Name = "chkScheduleFollowup";
            this.chkScheduleFollowup.Size = new System.Drawing.Size(132, 18);
            this.chkScheduleFollowup.TabIndex = 1;
            this.chkScheduleFollowup.Text = "Schedule Follow-up";
            this.chkScheduleFollowup.UseVisualStyleBackColor = false;
            this.chkScheduleFollowup.CheckedChanged += new System.EventHandler(this.chkScheduleFollowup_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.lblTemplate);
            this.panel3.Controls.Add(this.lblLogAction);
            this.panel3.Controls.Add(this.cmbLogAction);
            this.panel3.Controls.Add(this.grpScedule);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 83);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel3.Size = new System.Drawing.Size(519, 171);
            this.panel3.TabIndex = 18;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoEllipsis = true;
            this.lblTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTemplate.Location = new System.Drawing.Point(157, 51);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(350, 14);
            this.lblTemplate.TabIndex = 218;
            this.lblTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLogAction
            // 
            this.lblLogAction.AutoSize = true;
            this.lblLogAction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLogAction.Location = new System.Drawing.Point(10, 20);
            this.lblLogAction.Name = "lblLogAction";
            this.lblLogAction.Size = new System.Drawing.Size(144, 14);
            this.lblLogAction.TabIndex = 214;
            this.lblLogAction.Text = "Select Template Action :";
            this.lblLogAction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbLogAction
            // 
            this.cmbLogAction.BackColor = System.Drawing.SystemColors.Window;
            this.cmbLogAction.DropDownHeight = 100;
            this.cmbLogAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogAction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLogAction.FormattingEnabled = true;
            this.cmbLogAction.IntegralHeight = false;
            this.cmbLogAction.Location = new System.Drawing.Point(157, 17);
            this.cmbLogAction.Name = "cmbLogAction";
            this.cmbLogAction.Size = new System.Drawing.Size(350, 22);
            this.cmbLogAction.TabIndex = 217;
            this.cmbLogAction.SelectedIndexChanged += new System.EventHandler(this.cmbLogAction_SelectedIndexChanged);
            this.cmbLogAction.MouseEnter += new System.EventHandler(this.cmbLogAction_MouseEnter);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(4, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(511, 2);
            this.label13.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Location = new System.Drawing.Point(4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(511, 1);
            this.label14.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(4, 167);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(511, 1);
            this.label15.TabIndex = 6;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Location = new System.Drawing.Point(515, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 168);
            this.label16.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 168);
            this.label17.TabIndex = 4;
            // 
            // pnlPatDetails
            // 
            this.pnlPatDetails.Controls.Add(this.panel2);
            this.pnlPatDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatDetails.Location = new System.Drawing.Point(0, 53);
            this.pnlPatDetails.Name = "pnlPatDetails";
            this.pnlPatDetails.Padding = new System.Windows.Forms.Padding(3);
            this.pnlPatDetails.Size = new System.Drawing.Size(519, 30);
            this.pnlPatDetails.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lblInfo);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 24);
            this.panel2.TabIndex = 0;
            this.panel2.TabStop = true;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.AutoEllipsis = true;
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.Black;
            this.lblInfo.Location = new System.Drawing.Point(8, 5);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(497, 14);
            this.lblInfo.TabIndex = 2007;
            this.lblInfo.Text = "                   ";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(512, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "label3";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(513, 1);
            this.label18.TabIndex = 5;
            this.label18.Text = "label1";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label19.Location = new System.Drawing.Point(0, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(513, 1);
            this.label19.TabIndex = 8;
            this.label19.Text = "label2";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // pnlProgressBar
            // 
            this.pnlProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlProgressBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProgressBar.Controls.Add(this.panel1);
            this.pnlProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProgressBar.Location = new System.Drawing.Point(0, 231);
            this.pnlProgressBar.Name = "pnlProgressBar";
            this.pnlProgressBar.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlProgressBar.Size = new System.Drawing.Size(519, 23);
            this.pnlProgressBar.TabIndex = 259;
            this.pnlProgressBar.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lblFile);
            this.panel1.Controls.Add(this.lblFileCounter);
            this.panel1.Controls.Add(this.prgFileGeneration);
            this.panel1.Controls.Add(this.label38);
            this.panel1.Controls.Add(this.label32);
            this.panel1.Controls.Add(this.label37);
            this.panel1.Controls.Add(this.label33);
            this.panel1.Controls.Add(this.label34);
            this.panel1.Controls.Add(this.label36);
            this.panel1.Controls.Add(this.label35);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 20);
            this.panel1.TabIndex = 32;
            // 
            // lblFile
            // 
            this.lblFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFile.Location = new System.Drawing.Point(1, 3);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(186, 14);
            this.lblFile.TabIndex = 1;
            this.lblFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFileCounter
            // 
            this.lblFileCounter.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblFileCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileCounter.Location = new System.Drawing.Point(73, 3);
            this.lblFileCounter.Name = "lblFileCounter";
            this.lblFileCounter.Size = new System.Drawing.Size(117, 14);
            this.lblFileCounter.TabIndex = 24;
            this.lblFileCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // prgFileGeneration
            // 
            this.prgFileGeneration.Dock = System.Windows.Forms.DockStyle.Right;
            this.prgFileGeneration.Location = new System.Drawing.Point(190, 3);
            this.prgFileGeneration.Name = "prgFileGeneration";
            this.prgFileGeneration.Size = new System.Drawing.Size(320, 14);
            this.prgFileGeneration.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgFileGeneration.TabIndex = 0;
            // 
            // label38
            // 
            this.label38.Dock = System.Windows.Forms.DockStyle.Right;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label38.Location = new System.Drawing.Point(510, 3);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(2, 14);
            this.label38.TabIndex = 31;
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Location = new System.Drawing.Point(1, 1);
            this.label32.Name = "label32";
            this.label32.Padding = new System.Windows.Forms.Padding(3);
            this.label32.Size = new System.Drawing.Size(511, 2);
            this.label32.TabIndex = 23;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Top;
            this.label37.Location = new System.Drawing.Point(1, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(511, 1);
            this.label37.TabIndex = 30;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label33.Location = new System.Drawing.Point(1, 17);
            this.label33.Name = "label33";
            this.label33.Padding = new System.Windows.Forms.Padding(3);
            this.label33.Size = new System.Drawing.Size(511, 2);
            this.label33.TabIndex = 29;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Location = new System.Drawing.Point(1, 19);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(511, 1);
            this.label34.TabIndex = 26;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 20);
            this.label36.TabIndex = 27;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Right;
            this.label35.Location = new System.Drawing.Point(512, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 20);
            this.label35.TabIndex = 28;
            // 
            // frmSetupFollowupDateActionTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(519, 254);
            this.Controls.Add(this.pnlProgressBar);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnlPatDetails);
            this.Controls.Add(this.ts_Commands);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupFollowupDateActionTemplate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate Account Template";
            this.Load += new System.EventHandler(this.frmSetupFollowupDateActionTemplate_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.grpScedule.ResumeLayout(false);
            this.grpScedule.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlPatDetails.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlProgressBar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Generate;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.GroupBox grpScedule;
        private System.Windows.Forms.CheckBox chkScheduleFollowup;
        private System.Windows.Forms.MaskedTextBox mskScheduleFollowupDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel pnlPatDetails;
        internal System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ComboBox cmbScheduleFollowup;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        private System.Windows.Forms.Label lblLogAction;
        private System.Windows.Forms.ComboBox cmbLogAction;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.Panel pnlProgressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblFileCounter;
        private System.Windows.Forms.ProgressBar prgFileGeneration;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        //private System.Drawing.Printing.PrintDocument printDocument1;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}