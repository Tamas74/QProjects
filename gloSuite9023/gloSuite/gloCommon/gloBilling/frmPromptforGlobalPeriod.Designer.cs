namespace gloBilling
{
    partial class frmPromptforGlobalPeriod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPromptforGlobalPeriod));
            this.pnlSingleGlobalPeriodPrompt = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkStoptoReviewCharges = new System.Windows.Forms.RadioButton();
            this.chkContinueSaveofNewCharges = new System.Windows.Forms.RadioButton();
            this.chkStoptoReviewGlobalPeriodDetails = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblReminder = new System.Windows.Forms.Label();
            this.lblNotesCaption = new System.Windows.Forms.Label();
            this.lblReminderCaption = new System.Windows.Forms.Label();
            this.lblInsurance = new System.Windows.Forms.Label();
            this.lblProvider = new System.Windows.Forms.Label();
            this.lblInsuranceCaption = new System.Windows.Forms.Label();
            this.lblProviderCaption = new System.Windows.Forms.Label();
            this.lblPeriodDateRange = new System.Windows.Forms.Label();
            this.lblDatesCaption = new System.Windows.Forms.Label();
            this.lblCPT = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSingleGPAction = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCPTCaption = new System.Windows.Forms.Label();
            this.lblTopMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMultipleGlobalPeriodPrompt = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkMultiStoptoReviewCharges = new System.Windows.Forms.RadioButton();
            this.chkMultiContinueSaveofNewCharges = new System.Windows.Forms.RadioButton();
            this.chkMultiStoptoReviewGlobalPeriodDetails = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.btnMultipleGPAction = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblCaption = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tooltip_Billing = new System.Windows.Forms.ToolTip(this.components);
            this.pnlSingleGlobalPeriodPrompt.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlMultipleGlobalPeriodPrompt.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSingleGlobalPeriodPrompt
            // 
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.panel2);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.label5);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblNotes);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblReminder);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblNotesCaption);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblReminderCaption);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblInsurance);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblProvider);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblInsuranceCaption);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblProviderCaption);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblPeriodDateRange);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblDatesCaption);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblCPT);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.label4);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.btnSingleGPAction);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.label3);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.label2);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblCPTCaption);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.lblTopMsg);
            this.pnlSingleGlobalPeriodPrompt.Controls.Add(this.label1);
            this.pnlSingleGlobalPeriodPrompt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSingleGlobalPeriodPrompt.Location = new System.Drawing.Point(0, 0);
            this.pnlSingleGlobalPeriodPrompt.Name = "pnlSingleGlobalPeriodPrompt";
            this.pnlSingleGlobalPeriodPrompt.Size = new System.Drawing.Size(528, 310);
            this.pnlSingleGlobalPeriodPrompt.TabIndex = 1;
            this.pnlSingleGlobalPeriodPrompt.TabStop = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkStoptoReviewCharges);
            this.panel2.Controls.Add(this.chkContinueSaveofNewCharges);
            this.panel2.Controls.Add(this.chkStoptoReviewGlobalPeriodDetails);
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(72, 206);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 72);
            this.panel2.TabIndex = 30;
            // 
            // chkStoptoReviewCharges
            // 
            this.chkStoptoReviewCharges.AutoSize = true;
            this.chkStoptoReviewCharges.Location = new System.Drawing.Point(3, 3);
            this.chkStoptoReviewCharges.Name = "chkStoptoReviewCharges";
            this.chkStoptoReviewCharges.Size = new System.Drawing.Size(157, 18);
            this.chkStoptoReviewCharges.TabIndex = 26;
            this.chkStoptoReviewCharges.TabStop = true;
            this.chkStoptoReviewCharges.Text = "Stop to Review Charges";
            this.chkStoptoReviewCharges.UseVisualStyleBackColor = true;
            // 
            // chkContinueSaveofNewCharges
            // 
            this.chkContinueSaveofNewCharges.AutoSize = true;
            this.chkContinueSaveofNewCharges.Location = new System.Drawing.Point(3, 49);
            this.chkContinueSaveofNewCharges.Name = "chkContinueSaveofNewCharges";
            this.chkContinueSaveofNewCharges.Size = new System.Drawing.Size(195, 18);
            this.chkContinueSaveofNewCharges.TabIndex = 28;
            this.chkContinueSaveofNewCharges.TabStop = true;
            this.chkContinueSaveofNewCharges.Text = "Continue Save of New Charges";
            this.chkContinueSaveofNewCharges.UseVisualStyleBackColor = true;
            // 
            // chkStoptoReviewGlobalPeriodDetails
            // 
            this.chkStoptoReviewGlobalPeriodDetails.AutoSize = true;
            this.chkStoptoReviewGlobalPeriodDetails.Location = new System.Drawing.Point(3, 26);
            this.chkStoptoReviewGlobalPeriodDetails.Name = "chkStoptoReviewGlobalPeriodDetails";
            this.chkStoptoReviewGlobalPeriodDetails.Size = new System.Drawing.Size(223, 18);
            this.chkStoptoReviewGlobalPeriodDetails.TabIndex = 27;
            this.chkStoptoReviewGlobalPeriodDetails.TabStop = true;
            this.chkStoptoReviewGlobalPeriodDetails.Text = "Stop to Review Global Period Details";
            this.chkStoptoReviewGlobalPeriodDetails.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 16);
            this.label5.TabIndex = 26;
            this.label5.Text = "Do you wish to? ";
            // 
            // lblNotes
            // 
            this.lblNotes.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblNotes.Location = new System.Drawing.Point(91, 141);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(427, 43);
            this.lblNotes.TabIndex = 21;
            this.lblNotes.Text = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
    "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";
            this.lblNotes.MouseHover += new System.EventHandler(this.lblNotes_MouseHover);
            // 
            // lblReminder
            // 
            this.lblReminder.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblReminder.Location = new System.Drawing.Point(91, 93);
            this.lblReminder.Name = "lblReminder";
            this.lblReminder.Size = new System.Drawing.Size(427, 43);
            this.lblReminder.TabIndex = 20;
            this.lblReminder.Text = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
    "WWWWWWWWWWWWW";
            this.lblReminder.MouseHover += new System.EventHandler(this.lblReminder_MouseHover);
            // 
            // lblNotesCaption
            // 
            this.lblNotesCaption.AutoSize = true;
            this.lblNotesCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblNotesCaption.Location = new System.Drawing.Point(21, 141);
            this.lblNotesCaption.Name = "lblNotesCaption";
            this.lblNotesCaption.Size = new System.Drawing.Size(68, 14);
            this.lblNotesCaption.TabIndex = 19;
            this.lblNotesCaption.Text = "Comment :";
            // 
            // lblReminderCaption
            // 
            this.lblReminderCaption.AutoSize = true;
            this.lblReminderCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblReminderCaption.Location = new System.Drawing.Point(23, 93);
            this.lblReminderCaption.Name = "lblReminderCaption";
            this.lblReminderCaption.Size = new System.Drawing.Size(66, 14);
            this.lblReminderCaption.TabIndex = 18;
            this.lblReminderCaption.Text = "Reminder :";
            // 
            // lblInsurance
            // 
            this.lblInsurance.AutoSize = true;
            this.lblInsurance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblInsurance.Location = new System.Drawing.Point(91, 74);
            this.lblInsurance.Name = "lblInsurance";
            this.lblInsurance.Size = new System.Drawing.Size(114, 14);
            this.lblInsurance.TabIndex = 17;
            this.lblInsurance.Text = "Insurance Company";
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblProvider.Location = new System.Drawing.Point(91, 55);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(61, 14);
            this.lblProvider.TabIndex = 16;
            this.lblProvider.Text = "Dr. Hitesh";
            // 
            // lblInsuranceCaption
            // 
            this.lblInsuranceCaption.AutoSize = true;
            this.lblInsuranceCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblInsuranceCaption.Location = new System.Drawing.Point(21, 74);
            this.lblInsuranceCaption.Name = "lblInsuranceCaption";
            this.lblInsuranceCaption.Size = new System.Drawing.Size(68, 14);
            this.lblInsuranceCaption.TabIndex = 15;
            this.lblInsuranceCaption.Text = "Insurance :";
            // 
            // lblProviderCaption
            // 
            this.lblProviderCaption.AutoSize = true;
            this.lblProviderCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblProviderCaption.Location = new System.Drawing.Point(30, 55);
            this.lblProviderCaption.Name = "lblProviderCaption";
            this.lblProviderCaption.Size = new System.Drawing.Size(59, 14);
            this.lblProviderCaption.TabIndex = 14;
            this.lblProviderCaption.Text = "Provider :";
            // 
            // lblPeriodDateRange
            // 
            this.lblPeriodDateRange.AutoSize = true;
            this.lblPeriodDateRange.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPeriodDateRange.Location = new System.Drawing.Point(213, 36);
            this.lblPeriodDateRange.Name = "lblPeriodDateRange";
            this.lblPeriodDateRange.Size = new System.Drawing.Size(151, 14);
            this.lblPeriodDateRange.TabIndex = 13;
            this.lblPeriodDateRange.Text = "09/20/2011 - 09/25/2011";
            // 
            // lblDatesCaption
            // 
            this.lblDatesCaption.AutoSize = true;
            this.lblDatesCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblDatesCaption.Location = new System.Drawing.Point(168, 36);
            this.lblDatesCaption.Name = "lblDatesCaption";
            this.lblDatesCaption.Size = new System.Drawing.Size(46, 14);
            this.lblDatesCaption.TabIndex = 12;
            this.lblDatesCaption.Text = "Dates :";
            // 
            // lblCPT
            // 
            this.lblCPT.AutoEllipsis = true;
            this.lblCPT.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblCPT.Location = new System.Drawing.Point(91, 36);
            this.lblCPT.Name = "lblCPT";
            this.lblCPT.Size = new System.Drawing.Size(73, 14);
            this.lblCPT.TabIndex = 11;
            this.lblCPT.Text = "99999";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(1, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(526, 1);
            this.label4.TabIndex = 10;
            // 
            // btnSingleGPAction
            // 
            this.btnSingleGPAction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSingleGPAction.BackgroundImage")));
            this.btnSingleGPAction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSingleGPAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSingleGPAction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSingleGPAction.Location = new System.Drawing.Point(232, 281);
            this.btnSingleGPAction.Name = "btnSingleGPAction";
            this.btnSingleGPAction.Size = new System.Drawing.Size(75, 23);
            this.btnSingleGPAction.TabIndex = 3;
            this.btnSingleGPAction.Text = "Ok";
            this.btnSingleGPAction.UseVisualStyleBackColor = true;
            this.btnSingleGPAction.Click += new System.EventHandler(this.btnSingleGPAction_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(526, 1);
            this.label3.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(527, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 310);
            this.label2.TabIndex = 8;
            // 
            // lblCPTCaption
            // 
            this.lblCPTCaption.AutoSize = true;
            this.lblCPTCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblCPTCaption.Location = new System.Drawing.Point(53, 36);
            this.lblCPTCaption.Name = "lblCPTCaption";
            this.lblCPTCaption.Size = new System.Drawing.Size(37, 14);
            this.lblCPTCaption.TabIndex = 1;
            this.lblCPTCaption.Text = "CPT :";
            // 
            // lblTopMsg
            // 
            this.lblTopMsg.AutoSize = true;
            this.lblTopMsg.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTopMsg.Location = new System.Drawing.Point(16, 12);
            this.lblTopMsg.Name = "lblTopMsg";
            this.lblTopMsg.Size = new System.Drawing.Size(266, 16);
            this.lblTopMsg.TabIndex = 0;
            this.lblTopMsg.Text = "Service Dates are within a Global Period";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 310);
            this.label1.TabIndex = 7;
            // 
            // pnlMultipleGlobalPeriodPrompt
            // 
            this.pnlMultipleGlobalPeriodPrompt.Controls.Add(this.panel1);
            this.pnlMultipleGlobalPeriodPrompt.Controls.Add(this.label16);
            this.pnlMultipleGlobalPeriodPrompt.Controls.Add(this.btnMultipleGPAction);
            this.pnlMultipleGlobalPeriodPrompt.Controls.Add(this.label17);
            this.pnlMultipleGlobalPeriodPrompt.Controls.Add(this.label18);
            this.pnlMultipleGlobalPeriodPrompt.Controls.Add(this.label19);
            this.pnlMultipleGlobalPeriodPrompt.Controls.Add(this.lblCaption);
            this.pnlMultipleGlobalPeriodPrompt.Controls.Add(this.label21);
            this.pnlMultipleGlobalPeriodPrompt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMultipleGlobalPeriodPrompt.Location = new System.Drawing.Point(0, 0);
            this.pnlMultipleGlobalPeriodPrompt.Name = "pnlMultipleGlobalPeriodPrompt";
            this.pnlMultipleGlobalPeriodPrompt.Size = new System.Drawing.Size(528, 310);
            this.pnlMultipleGlobalPeriodPrompt.TabIndex = 2;
            this.pnlMultipleGlobalPeriodPrompt.TabStop = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkMultiStoptoReviewCharges);
            this.panel1.Controls.Add(this.chkMultiContinueSaveofNewCharges);
            this.panel1.Controls.Add(this.chkMultiStoptoReviewGlobalPeriodDetails);
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(155, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 72);
            this.panel1.TabIndex = 30;
            // 
            // chkMultiStoptoReviewCharges
            // 
            this.chkMultiStoptoReviewCharges.AutoSize = true;
            this.chkMultiStoptoReviewCharges.Location = new System.Drawing.Point(3, 3);
            this.chkMultiStoptoReviewCharges.Name = "chkMultiStoptoReviewCharges";
            this.chkMultiStoptoReviewCharges.Size = new System.Drawing.Size(157, 18);
            this.chkMultiStoptoReviewCharges.TabIndex = 26;
            this.chkMultiStoptoReviewCharges.TabStop = true;
            this.chkMultiStoptoReviewCharges.Text = "Stop to Review Charges";
            this.chkMultiStoptoReviewCharges.UseVisualStyleBackColor = true;
            // 
            // chkMultiContinueSaveofNewCharges
            // 
            this.chkMultiContinueSaveofNewCharges.AutoSize = true;
            this.chkMultiContinueSaveofNewCharges.Location = new System.Drawing.Point(3, 49);
            this.chkMultiContinueSaveofNewCharges.Name = "chkMultiContinueSaveofNewCharges";
            this.chkMultiContinueSaveofNewCharges.Size = new System.Drawing.Size(195, 18);
            this.chkMultiContinueSaveofNewCharges.TabIndex = 28;
            this.chkMultiContinueSaveofNewCharges.TabStop = true;
            this.chkMultiContinueSaveofNewCharges.Text = "Continue Save of New Charges";
            this.chkMultiContinueSaveofNewCharges.UseVisualStyleBackColor = true;
            // 
            // chkMultiStoptoReviewGlobalPeriodDetails
            // 
            this.chkMultiStoptoReviewGlobalPeriodDetails.AutoSize = true;
            this.chkMultiStoptoReviewGlobalPeriodDetails.Location = new System.Drawing.Point(3, 26);
            this.chkMultiStoptoReviewGlobalPeriodDetails.Name = "chkMultiStoptoReviewGlobalPeriodDetails";
            this.chkMultiStoptoReviewGlobalPeriodDetails.Size = new System.Drawing.Size(223, 18);
            this.chkMultiStoptoReviewGlobalPeriodDetails.TabIndex = 27;
            this.chkMultiStoptoReviewGlobalPeriodDetails.TabStop = true;
            this.chkMultiStoptoReviewGlobalPeriodDetails.Text = "Stop to Review Global Period Details";
            this.chkMultiStoptoReviewGlobalPeriodDetails.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Location = new System.Drawing.Point(1, 309);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(526, 1);
            this.label16.TabIndex = 10;
            // 
            // btnMultipleGPAction
            // 
            this.btnMultipleGPAction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMultipleGPAction.BackgroundImage")));
            this.btnMultipleGPAction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMultipleGPAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMultipleGPAction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMultipleGPAction.Location = new System.Drawing.Point(232, 141);
            this.btnMultipleGPAction.Name = "btnMultipleGPAction";
            this.btnMultipleGPAction.Size = new System.Drawing.Size(75, 23);
            this.btnMultipleGPAction.TabIndex = 3;
            this.btnMultipleGPAction.Text = "Ok";
            this.btnMultipleGPAction.UseVisualStyleBackColor = true;
            this.btnMultipleGPAction.Click += new System.EventHandler(this.btnMultipleGPAction_Click);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(1, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(526, 1);
            this.label17.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Location = new System.Drawing.Point(527, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 310);
            this.label18.TabIndex = 8;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(103, 38);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(111, 16);
            this.label19.TabIndex = 1;
            this.label19.Text = "Do you wish to?";
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.Location = new System.Drawing.Point(19, 13);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(210, 16);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "Multiple Global Periods in effect";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 310);
            this.label21.TabIndex = 7;
            // 
            // frmPromptforGlobalPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(528, 310);
            this.Controls.Add(this.pnlSingleGlobalPeriodPrompt);
            this.Controls.Add(this.pnlMultipleGlobalPeriodPrompt);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPromptforGlobalPeriod";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gloPM";
            this.Load += new System.EventHandler(this.frmPromptforGlobalPeriod_Load);
            this.pnlSingleGlobalPeriodPrompt.ResumeLayout(false);
            this.pnlSingleGlobalPeriodPrompt.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlMultipleGlobalPeriodPrompt.ResumeLayout(false);
            this.pnlMultipleGlobalPeriodPrompt.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSingleGlobalPeriodPrompt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSingleGPAction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTopMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCPTCaption;
        private System.Windows.Forms.Label lblReminderCaption;
        private System.Windows.Forms.Label lblInsurance;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.Label lblInsuranceCaption;
        private System.Windows.Forms.Label lblProviderCaption;
        private System.Windows.Forms.Label lblPeriodDateRange;
        private System.Windows.Forms.Label lblDatesCaption;
        private System.Windows.Forms.Label lblCPT;
        private System.Windows.Forms.Label lblReminder;
        private System.Windows.Forms.Label lblNotesCaption;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Panel pnlMultipleGlobalPeriodPrompt;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnMultipleGPAction;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton chkMultiStoptoReviewCharges;
        private System.Windows.Forms.RadioButton chkMultiContinueSaveofNewCharges;
        private System.Windows.Forms.RadioButton chkMultiStoptoReviewGlobalPeriodDetails;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton chkStoptoReviewCharges;
        private System.Windows.Forms.RadioButton chkContinueSaveofNewCharges;
        private System.Windows.Forms.RadioButton chkStoptoReviewGlobalPeriodDetails;
        private System.Windows.Forms.ToolTip tooltip_Billing;
    }
}