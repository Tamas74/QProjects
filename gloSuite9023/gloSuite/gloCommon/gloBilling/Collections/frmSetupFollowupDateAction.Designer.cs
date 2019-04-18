namespace gloBilling.Collections
{
    partial class frmSetupFollowupDateAction
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
                    if (dtpNoteDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpNoteDate);

                        }
                        catch
                        {
                        }


                        dtpNoteDate.Dispose();
                        dtpNoteDate = null;
                    }
                }
                catch
                {
                }


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupFollowupDateAction));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.grpLogAction = new System.Windows.Forms.GroupBox();
            this.chkLogAction = new System.Windows.Forms.CheckBox();
            this.cmbLogAction = new System.Windows.Forms.ComboBox();
            this.mskLogActionDate = new System.Windows.Forms.MaskedTextBox();
            this.lblLogDate = new System.Windows.Forms.Label();
            this.lblLogAction = new System.Windows.Forms.Label();
            this.grpScedule = new System.Windows.Forms.GroupBox();
            this.chkScheduleFollowup = new System.Windows.Forms.CheckBox();
            this.cmbScheduleFollowup = new System.Windows.Forms.ComboBox();
            this.mskScheduleFollowupDate = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpNote = new System.Windows.Forms.GroupBox();
            this.dtpNoteDate = new System.Windows.Forms.DateTimePicker();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnBrowseNotes = new System.Windows.Forms.Button();
            this.btnBrowseReasonCode = new System.Windows.Forms.Button();
            this.btnBrowseRemarkCode = new System.Windows.Forms.Button();
            this.lblNoteCaption = new System.Windows.Forms.Label();
            this.label_BillingAleartMSG = new System.Windows.Forms.Label();
            this.lblNoteDate = new System.Windows.Forms.Label();
            this.label_notes = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
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
            this.tsb_ClaimStatus = new System.Windows.Forms.ToolStripButton();
            this.ts_Commands.SuspendLayout();
            this.grpLogAction.SuspendLayout();
            this.grpScedule.SuspendLayout();
            this.grpNote.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlPatDetails.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_ClaimStatus,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(547, 53);
            this.ts_Commands.TabIndex = 1;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
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
            // grpLogAction
            // 
            this.grpLogAction.Controls.Add(this.chkLogAction);
            this.grpLogAction.Controls.Add(this.cmbLogAction);
            this.grpLogAction.Controls.Add(this.mskLogActionDate);
            this.grpLogAction.Controls.Add(this.lblLogDate);
            this.grpLogAction.Controls.Add(this.lblLogAction);
            this.grpLogAction.Location = new System.Drawing.Point(10, 7);
            this.grpLogAction.Name = "grpLogAction";
            this.grpLogAction.Size = new System.Drawing.Size(525, 56);
            this.grpLogAction.TabIndex = 15;
            this.grpLogAction.TabStop = false;
            // 
            // chkLogAction
            // 
            this.chkLogAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.chkLogAction.Location = new System.Drawing.Point(5, 0);
            this.chkLogAction.Name = "chkLogAction";
            this.chkLogAction.Size = new System.Drawing.Size(85, 18);
            this.chkLogAction.TabIndex = 0;
            this.chkLogAction.Text = "Log Action";
            this.chkLogAction.UseVisualStyleBackColor = false;
            this.chkLogAction.CheckedChanged += new System.EventHandler(this.chkLogAction_CheckedChanged);
            // 
            // cmbLogAction
            // 
            this.cmbLogAction.BackColor = System.Drawing.SystemColors.Window;
            this.cmbLogAction.DropDownHeight = 100;
            this.cmbLogAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogAction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLogAction.FormattingEnabled = true;
            this.cmbLogAction.IntegralHeight = false;
            this.cmbLogAction.Location = new System.Drawing.Point(65, 22);
            this.cmbLogAction.Name = "cmbLogAction";
            this.cmbLogAction.Size = new System.Drawing.Size(318, 22);
            this.cmbLogAction.TabIndex = 217;
            this.cmbLogAction.SelectedIndexChanged += new System.EventHandler(this.cmbLogAction_SelectedIndexChanged);
            this.cmbLogAction.MouseEnter += new System.EventHandler(this.cmbLogAction_MouseEnter);
            // 
            // mskLogActionDate
            // 
            this.mskLogActionDate.Location = new System.Drawing.Point(438, 22);
            this.mskLogActionDate.Mask = "00/00/0000";
            this.mskLogActionDate.Name = "mskLogActionDate";
            this.mskLogActionDate.Size = new System.Drawing.Size(79, 22);
            this.mskLogActionDate.TabIndex = 215;
            this.mskLogActionDate.ValidatingType = typeof(System.DateTime);
            this.mskLogActionDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskLogActionDate_MouseClick);
            this.mskLogActionDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskLogActionDate_Validating);
            // 
            // lblLogDate
            // 
            this.lblLogDate.AutoSize = true;
            this.lblLogDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLogDate.Location = new System.Drawing.Point(394, 26);
            this.lblLogDate.Name = "lblLogDate";
            this.lblLogDate.Size = new System.Drawing.Size(41, 14);
            this.lblLogDate.TabIndex = 216;
            this.lblLogDate.Text = "Date :";
            this.lblLogDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLogAction
            // 
            this.lblLogAction.AutoSize = true;
            this.lblLogAction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLogAction.Location = new System.Drawing.Point(12, 26);
            this.lblLogAction.Name = "lblLogAction";
            this.lblLogAction.Size = new System.Drawing.Size(50, 14);
            this.lblLogAction.TabIndex = 214;
            this.lblLogAction.Text = "Action :";
            this.lblLogAction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpScedule
            // 
            this.grpScedule.Controls.Add(this.chkScheduleFollowup);
            this.grpScedule.Controls.Add(this.cmbScheduleFollowup);
            this.grpScedule.Controls.Add(this.mskScheduleFollowupDate);
            this.grpScedule.Controls.Add(this.label1);
            this.grpScedule.Controls.Add(this.label2);
            this.grpScedule.Location = new System.Drawing.Point(10, 63);
            this.grpScedule.Name = "grpScedule";
            this.grpScedule.Size = new System.Drawing.Size(524, 56);
            this.grpScedule.TabIndex = 16;
            this.grpScedule.TabStop = false;
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
            // cmbScheduleFollowup
            // 
            this.cmbScheduleFollowup.BackColor = System.Drawing.SystemColors.Window;
            this.cmbScheduleFollowup.DropDownHeight = 100;
            this.cmbScheduleFollowup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScheduleFollowup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbScheduleFollowup.FormattingEnabled = true;
            this.cmbScheduleFollowup.IntegralHeight = false;
            this.cmbScheduleFollowup.Location = new System.Drawing.Point(65, 25);
            this.cmbScheduleFollowup.Name = "cmbScheduleFollowup";
            this.cmbScheduleFollowup.Size = new System.Drawing.Size(318, 22);
            this.cmbScheduleFollowup.TabIndex = 221;
            this.cmbScheduleFollowup.MouseEnter += new System.EventHandler(this.cmbScheduleFollowup_MouseEnter);
            // 
            // mskScheduleFollowupDate
            // 
            this.mskScheduleFollowupDate.Location = new System.Drawing.Point(438, 25);
            this.mskScheduleFollowupDate.Mask = "00/00/0000";
            this.mskScheduleFollowupDate.Name = "mskScheduleFollowupDate";
            this.mskScheduleFollowupDate.Size = new System.Drawing.Size(79, 22);
            this.mskScheduleFollowupDate.TabIndex = 219;
            this.mskScheduleFollowupDate.ValidatingType = typeof(System.DateTime);
            this.mskScheduleFollowupDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskLogActionDate_MouseClick);
            this.mskScheduleFollowupDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskLogActionDate_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(394, 29);
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
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 218;
            this.label2.Text = "Action :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpNote
            // 
            this.grpNote.Controls.Add(this.dtpNoteDate);
            this.grpNote.Controls.Add(this.txtNotes);
            this.grpNote.Controls.Add(this.btnBrowseNotes);
            this.grpNote.Controls.Add(this.btnBrowseReasonCode);
            this.grpNote.Controls.Add(this.btnBrowseRemarkCode);
            this.grpNote.Controls.Add(this.lblNoteCaption);
            this.grpNote.Controls.Add(this.label_BillingAleartMSG);
            this.grpNote.Controls.Add(this.lblNoteDate);
            this.grpNote.Controls.Add(this.label_notes);
            this.grpNote.Location = new System.Drawing.Point(10, 119);
            this.grpNote.Name = "grpNote";
            this.grpNote.Size = new System.Drawing.Size(523, 130);
            this.grpNote.TabIndex = 16;
            this.grpNote.TabStop = false;
            // 
            // dtpNoteDate
            // 
            this.dtpNoteDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpNoteDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpNoteDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpNoteDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpNoteDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpNoteDate.Checked = false;
            this.dtpNoteDate.CustomFormat = "MM/dd/yyyy";
            this.dtpNoteDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNoteDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNoteDate.Location = new System.Drawing.Point(65, 17);
            this.dtpNoteDate.Name = "dtpNoteDate";
            this.dtpNoteDate.Size = new System.Drawing.Size(97, 22);
            this.dtpNoteDate.TabIndex = 2004;
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.ForeColor = System.Drawing.Color.Black;
            this.txtNotes.Location = new System.Drawing.Point(65, 43);
            this.txtNotes.MaxLength = 255;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(419, 80);
            this.txtNotes.TabIndex = 2005;
            // 
            // btnBrowseNotes
            // 
            this.btnBrowseNotes.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseNotes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseNotes.BackgroundImage")));
            this.btnBrowseNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseNotes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseNotes.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseNotes.Image")));
            this.btnBrowseNotes.Location = new System.Drawing.Point(488, 44);
            this.btnBrowseNotes.Name = "btnBrowseNotes";
            this.btnBrowseNotes.Size = new System.Drawing.Size(24, 24);
            this.btnBrowseNotes.TabIndex = 2011;
            this.btnBrowseNotes.Tag = "";
            this.toolTip1.SetToolTip(this.btnBrowseNotes, "Browse Notes");
            this.btnBrowseNotes.UseVisualStyleBackColor = false;
            this.btnBrowseNotes.Click += new System.EventHandler(this.btnBrowseNotes_Click);
            this.btnBrowseNotes.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnBrowseNotes.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // btnBrowseReasonCode
            // 
            this.btnBrowseReasonCode.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseReasonCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseReasonCode.BackgroundImage")));
            this.btnBrowseReasonCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseReasonCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseReasonCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseReasonCode.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseReasonCode.Image")));
            this.btnBrowseReasonCode.Location = new System.Drawing.Point(488, 71);
            this.btnBrowseReasonCode.Name = "btnBrowseReasonCode";
            this.btnBrowseReasonCode.Size = new System.Drawing.Size(24, 24);
            this.btnBrowseReasonCode.TabIndex = 2012;
            this.btnBrowseReasonCode.Tag = "";
            this.toolTip1.SetToolTip(this.btnBrowseReasonCode, "Browse Reason Code");
            this.btnBrowseReasonCode.UseVisualStyleBackColor = false;
            this.btnBrowseReasonCode.Click += new System.EventHandler(this.btnBrowseReasonCode_Click);
            this.btnBrowseReasonCode.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnBrowseReasonCode.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // btnBrowseRemarkCode
            // 
            this.btnBrowseRemarkCode.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseRemarkCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseRemarkCode.BackgroundImage")));
            this.btnBrowseRemarkCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseRemarkCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseRemarkCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseRemarkCode.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseRemarkCode.Image")));
            this.btnBrowseRemarkCode.Location = new System.Drawing.Point(488, 98);
            this.btnBrowseRemarkCode.Name = "btnBrowseRemarkCode";
            this.btnBrowseRemarkCode.Size = new System.Drawing.Size(24, 24);
            this.btnBrowseRemarkCode.TabIndex = 2013;
            this.btnBrowseRemarkCode.Tag = "";
            this.toolTip1.SetToolTip(this.btnBrowseRemarkCode, "Browse Remark Code");
            this.btnBrowseRemarkCode.UseVisualStyleBackColor = false;
            this.btnBrowseRemarkCode.Click += new System.EventHandler(this.btnBrowseRemarkCode_Click);
            this.btnBrowseRemarkCode.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnBrowseRemarkCode.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // lblNoteCaption
            // 
            this.lblNoteCaption.AutoSize = true;
            this.lblNoteCaption.Location = new System.Drawing.Point(5, 0);
            this.lblNoteCaption.Margin = new System.Windows.Forms.Padding(0);
            this.lblNoteCaption.Name = "lblNoteCaption";
            this.lblNoteCaption.Size = new System.Drawing.Size(34, 14);
            this.lblNoteCaption.TabIndex = 2009;
            this.lblNoteCaption.Text = "Note";
            // 
            // label_BillingAleartMSG
            // 
            this.label_BillingAleartMSG.AutoSize = true;
            this.label_BillingAleartMSG.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_BillingAleartMSG.ForeColor = System.Drawing.Color.Red;
            this.label_BillingAleartMSG.Location = new System.Drawing.Point(272, 22);
            this.label_BillingAleartMSG.Name = "label_BillingAleartMSG";
            this.label_BillingAleartMSG.Size = new System.Drawing.Size(208, 14);
            this.label_BillingAleartMSG.TabIndex = 2006;
            this.label_BillingAleartMSG.Text = "Maximum 255 characters are allowed";
            // 
            // lblNoteDate
            // 
            this.lblNoteDate.AutoSize = true;
            this.lblNoteDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblNoteDate.Location = new System.Drawing.Point(21, 21);
            this.lblNoteDate.Name = "lblNoteDate";
            this.lblNoteDate.Size = new System.Drawing.Size(41, 14);
            this.lblNoteDate.TabIndex = 2007;
            this.lblNoteDate.Text = "Date :";
            // 
            // label_notes
            // 
            this.label_notes.AutoSize = true;
            this.label_notes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_notes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label_notes.Location = new System.Drawing.Point(20, 46);
            this.label_notes.Name = "label_notes";
            this.label_notes.Size = new System.Drawing.Size(42, 14);
            this.label_notes.TabIndex = 2008;
            this.label_notes.Text = "Note :";
            // 
            // panel3
            // 
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.grpLogAction);
            this.panel3.Controls.Add(this.grpScedule);
            this.panel3.Controls.Add(this.grpNote);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 83);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel3.Size = new System.Drawing.Size(547, 264);
            this.panel3.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(4, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(539, 2);
            this.label13.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Location = new System.Drawing.Point(4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(539, 1);
            this.label14.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(4, 260);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(539, 1);
            this.label15.TabIndex = 6;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Location = new System.Drawing.Point(543, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 261);
            this.label16.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 261);
            this.label17.TabIndex = 4;
            // 
            // pnlPatDetails
            // 
            this.pnlPatDetails.Controls.Add(this.panel2);
            this.pnlPatDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatDetails.Location = new System.Drawing.Point(0, 53);
            this.pnlPatDetails.Name = "pnlPatDetails";
            this.pnlPatDetails.Padding = new System.Windows.Forms.Padding(3);
            this.pnlPatDetails.Size = new System.Drawing.Size(547, 30);
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
            this.panel2.Size = new System.Drawing.Size(541, 24);
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
            this.lblInfo.Size = new System.Drawing.Size(525, 14);
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
            this.label4.Location = new System.Drawing.Point(540, 1);
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
            this.label18.Size = new System.Drawing.Size(541, 1);
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
            this.label19.Size = new System.Drawing.Size(541, 1);
            this.label19.TabIndex = 8;
            this.label19.Text = "label2";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // tsb_ClaimStatus
            // 
            this.tsb_ClaimStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ClaimStatus.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ClaimStatus.Image")));
            this.tsb_ClaimStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ClaimStatus.Name = "tsb_ClaimStatus";
            this.tsb_ClaimStatus.Size = new System.Drawing.Size(88, 50);
            this.tsb_ClaimStatus.Tag = "ClaimStatus";
            this.tsb_ClaimStatus.Text = "Claim &Status";
            this.tsb_ClaimStatus.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_ClaimStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ClaimStatus.Click += new System.EventHandler(this.tsb_ClaimStatus_Click);
            // 
            // frmSetupFollowupDateAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(547, 347);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnlPatDetails);
            this.Controls.Add(this.ts_Commands);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupFollowupDateAction";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log Action / Schedule Follow-up Action";
            this.Load += new System.EventHandler(this.frmSetupFollowupDateAction_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.grpLogAction.ResumeLayout(false);
            this.grpLogAction.PerformLayout();
            this.grpScedule.ResumeLayout(false);
            this.grpScedule.PerformLayout();
            this.grpNote.ResumeLayout(false);
            this.grpNote.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pnlPatDetails.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.GroupBox grpLogAction;
        private System.Windows.Forms.GroupBox grpScedule;
        private System.Windows.Forms.CheckBox chkScheduleFollowup;
        private System.Windows.Forms.CheckBox chkLogAction;
        private System.Windows.Forms.GroupBox grpNote;
        private System.Windows.Forms.Label lblLogAction;
        private System.Windows.Forms.MaskedTextBox mskLogActionDate;
        private System.Windows.Forms.Label lblLogDate;
        private System.Windows.Forms.Label label_BillingAleartMSG;
        private System.Windows.Forms.DateTimePicker dtpNoteDate;
        private System.Windows.Forms.Label lblNoteDate;
        private System.Windows.Forms.Label label_notes;
        private System.Windows.Forms.TextBox txtNotes;
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
        private System.Windows.Forms.Label lblNoteCaption;
        private System.Windows.Forms.ComboBox cmbLogAction;
        private System.Windows.Forms.ComboBox cmbScheduleFollowup;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnBrowseNotes;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Button btnBrowseRemarkCode;
        private System.Windows.Forms.Button btnBrowseReasonCode;
        internal System.Windows.Forms.ToolStripButton tsb_ClaimStatus;
    }
}