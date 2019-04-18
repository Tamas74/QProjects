namespace gloBilling
{
    partial class frmPatientStatementNotes
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtToDate, dtFromDate };
            System.Windows.Forms.Control[] cntControls = { dtToDate, dtFromDate };
 
            if (disposing && (components != null))
            {
                components.Dispose();
                base.Dispose(disposing);
                try
                {
               
                   if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }
                    //    if (dtToDate != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtToDate);

                //        }
                //        catch
                //        {
                //        }


                //        dtToDate.Dispose();
                //        dtToDate = null;
                //    }
                }
                catch
                {
                }

                //try
                //{
                //    if (dtFromDate != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtFromDate);

                //        }
                //        catch
                //        {
                //        }


                //        dtFromDate.Dispose();
                //        dtFromDate = null;
                //    }
                //}
                //catch
                //{
                //}

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
               
            }
         
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientStatementNotes));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Notes = new System.Windows.Forms.ToolStripButton();
            this.tlb_EditNotes = new System.Windows.Forms.ToolStripButton();
            this.tlb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnSaveClose = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnl_Header = new System.Windows.Forms.Panel();
            this.pnl = new System.Windows.Forms.Panel();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStatementNote = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_pnl_BaseBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_BaseTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_BaseLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_BaseRightBrd = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlAllNotes = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.C1AllNotes = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.btnBrowseNotes = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnl_Header.SuspendLayout();
            this.pnl.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlAllNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1AllNotes)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tls_Top);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 54);
            this.panel1.TabIndex = 110;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Notes,
            this.tlb_EditNotes,
            this.tlb_Delete,
            this.tls_btnOK,
            this.tls_btnSaveClose,
            this.tlb_Cancel,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(720, 53);
            this.tls_Top.TabIndex = 110;
            this.tls_Top.Text = "toolStrip1";
            this.tls_Top.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tlb_Notes
            // 
            this.tlb_Notes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Notes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Notes.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Notes.Image")));
            this.tlb_Notes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Notes.Name = "tlb_Notes";
            this.tlb_Notes.Size = new System.Drawing.Size(69, 50);
            this.tlb_Notes.Tag = "NewNote";
            this.tlb_Notes.Text = "Add &Note";
            this.tlb_Notes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Notes.ToolTipText = "Add Note";
            // 
            // tlb_EditNotes
            // 
            this.tlb_EditNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_EditNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_EditNotes.Image = ((System.Drawing.Image)(resources.GetObject("tlb_EditNotes.Image")));
            this.tlb_EditNotes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_EditNotes.Name = "tlb_EditNotes";
            this.tlb_EditNotes.Size = new System.Drawing.Size(90, 50);
            this.tlb_EditNotes.Tag = "Edit";
            this.tlb_EditNotes.Text = " &Modify Note";
            this.tlb_EditNotes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tlb_Delete
            // 
            this.tlb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Delete.Image")));
            this.tlb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Delete.Name = "tlb_Delete";
            this.tlb_Delete.Size = new System.Drawing.Size(50, 50);
            this.tlb_Delete.Tag = "Delete";
            this.tlb_Delete.Text = "&Delete";
            this.tlb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(40, 50);
            this.tls_btnOK.Tag = "Save";
            this.tls_btnOK.Text = "&Save";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tls_btnSaveClose
            // 
            this.tls_btnSaveClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnSaveClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnSaveClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnSaveClose.Image")));
            this.tls_btnSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnSaveClose.Name = "tls_btnSaveClose";
            this.tls_btnSaveClose.Size = new System.Drawing.Size(66, 50);
            this.tls_btnSaveClose.Tag = "SaveClose";
            this.tls_btnSaveClose.Text = "Sa&ve&&Cls";
            this.tls_btnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnSaveClose.ToolTipText = "Save and Close";
            // 
            // tlb_Cancel
            // 
            this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Cancel.Image")));
            this.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Cancel.Name = "tlb_Cancel";
            this.tlb_Cancel.Size = new System.Drawing.Size(50, 50);
            this.tlb_Cancel.Tag = "Cancel";
            this.tlb_Cancel.Text = "&Cancel";
            this.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Cancel.ToolTipText = "Cancel";
            this.tlb_Cancel.Visible = false;
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Close";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnl_Header
            // 
            this.pnl_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Header.Controls.Add(this.pnl);
            this.pnl_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_Header.Size = new System.Drawing.Size(720, 28);
            this.pnl_Header.TabIndex = 113;
            // 
            // pnl
            // 
            this.pnl.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl.Controls.Add(this.label41);
            this.pnl.Controls.Add(this.label40);
            this.pnl.Controls.Add(this.label39);
            this.pnl.Controls.Add(this.label6);
            this.pnl.Controls.Add(this.lblHeader);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(3, 3);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(714, 22);
            this.pnl.TabIndex = 5;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Right;
            this.label41.Location = new System.Drawing.Point(713, 1);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1, 20);
            this.label41.TabIndex = 7;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Left;
            this.label40.Location = new System.Drawing.Point(0, 1);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1, 20);
            this.label40.TabIndex = 6;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Top;
            this.label39.Location = new System.Drawing.Point(0, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(714, 1);
            this.label39.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(0, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(714, 1);
            this.label6.TabIndex = 4;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(714, 22);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "  Patient Statement Notes";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDetail
            // 
            this.pnlDetail.Controls.Add(this.btnBrowseNotes);
            this.pnlDetail.Controls.Add(this.label9);
            this.pnlDetail.Controls.Add(this.txtStatementNote);
            this.pnlDetail.Controls.Add(this.label1);
            this.pnlDetail.Controls.Add(this.dtToDate);
            this.pnlDetail.Controls.Add(this.dtFromDate);
            this.pnlDetail.Controls.Add(this.label2);
            this.pnlDetail.Controls.Add(this.label3);
            this.pnlDetail.Controls.Add(this.lbl_pnl_BaseBottomBrd);
            this.pnlDetail.Controls.Add(this.lbl_pnl_BaseTopBrd);
            this.pnlDetail.Controls.Add(this.lbl_pnl_BaseLeftBrd);
            this.pnlDetail.Controls.Add(this.lbl_pnl_BaseRightBrd);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetail.Location = new System.Drawing.Point(0, 28);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlDetail.Size = new System.Drawing.Size(720, 160);
            this.pnlDetail.TabIndex = 114;
            this.pnlDetail.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(135, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(188, 13);
            this.label9.TabIndex = 102;
            this.label9.Text = "Maximum 200 characters are allowed.";
            // 
            // txtStatementNote
            // 
            this.txtStatementNote.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtStatementNote.Location = new System.Drawing.Point(138, 61);
            this.txtStatementNote.MaxLength = 200;
            this.txtStatementNote.Multiline = true;
            this.txtStatementNote.Name = "txtStatementNote";
            this.txtStatementNote.Size = new System.Drawing.Size(537, 72);
            this.txtStatementNote.TabIndex = 2;
            this.txtStatementNote.TextChanged += new System.EventHandler(this.txtStatementNote_TextChanged);
            this.txtStatementNote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStatementNote_KeyPress);
            this.txtStatementNote.Leave += new System.EventHandler(this.txtStatementNote_Leave);
            this.txtStatementNote.Validating += new System.ComponentModel.CancelEventHandler(this.txtStatementNote_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(30, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 101;
            this.label1.Text = "Statement Note :";
            // 
            // dtToDate
            // 
            this.dtToDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtToDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtToDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtToDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtToDate.CustomFormat = "MM/dd/yyyy";
            this.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtToDate.Location = new System.Drawing.Point(138, 34);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(102, 22);
            this.dtToDate.TabIndex = 1;
            this.dtToDate.ValueChanged += new System.EventHandler(this.dtToDate_ValueChanged);
            // 
            // dtFromDate
            // 
            this.dtFromDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtFromDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtFromDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtFromDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtFromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtFromDate.CustomFormat = "MM/dd/yyyy";
            this.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFromDate.Location = new System.Drawing.Point(138, 7);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(102, 22);
            this.dtFromDate.TabIndex = 0;
            this.dtFromDate.ValueChanged += new System.EventHandler(this.dtFromDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(23, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 14);
            this.label2.TabIndex = 99;
            this.label2.Text = "Effective Date To :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(11, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 14);
            this.label3.TabIndex = 100;
            this.label3.Text = "Effective Date From :";
            // 
            // lbl_pnl_BaseBottomBrd
            // 
            this.lbl_pnl_BaseBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_BaseBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnl_BaseBottomBrd.Location = new System.Drawing.Point(4, 156);
            this.lbl_pnl_BaseBottomBrd.Name = "lbl_pnl_BaseBottomBrd";
            this.lbl_pnl_BaseBottomBrd.Size = new System.Drawing.Size(712, 1);
            this.lbl_pnl_BaseBottomBrd.TabIndex = 3;
            // 
            // lbl_pnl_BaseTopBrd
            // 
            this.lbl_pnl_BaseTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_BaseTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnl_BaseTopBrd.Location = new System.Drawing.Point(4, 0);
            this.lbl_pnl_BaseTopBrd.Name = "lbl_pnl_BaseTopBrd";
            this.lbl_pnl_BaseTopBrd.Size = new System.Drawing.Size(712, 1);
            this.lbl_pnl_BaseTopBrd.TabIndex = 2;
            // 
            // lbl_pnl_BaseLeftBrd
            // 
            this.lbl_pnl_BaseLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_BaseLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnl_BaseLeftBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_pnl_BaseLeftBrd.Name = "lbl_pnl_BaseLeftBrd";
            this.lbl_pnl_BaseLeftBrd.Size = new System.Drawing.Size(1, 157);
            this.lbl_pnl_BaseLeftBrd.TabIndex = 0;
            // 
            // lbl_pnl_BaseRightBrd
            // 
            this.lbl_pnl_BaseRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_BaseRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnl_BaseRightBrd.Location = new System.Drawing.Point(716, 0);
            this.lbl_pnl_BaseRightBrd.Name = "lbl_pnl_BaseRightBrd";
            this.lbl_pnl_BaseRightBrd.Size = new System.Drawing.Size(1, 157);
            this.lbl_pnl_BaseRightBrd.TabIndex = 1;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlAllNotes);
            this.pnlMain.Controls.Add(this.pnlDetail);
            this.pnlMain.Controls.Add(this.pnl_Header);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(720, 572);
            this.pnlMain.TabIndex = 111;
            // 
            // pnlAllNotes
            // 
            this.pnlAllNotes.Controls.Add(this.label8);
            this.pnlAllNotes.Controls.Add(this.label7);
            this.pnlAllNotes.Controls.Add(this.label5);
            this.pnlAllNotes.Controls.Add(this.label4);
            this.pnlAllNotes.Controls.Add(this.C1AllNotes);
            this.pnlAllNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAllNotes.Location = new System.Drawing.Point(0, 188);
            this.pnlAllNotes.Name = "pnlAllNotes";
            this.pnlAllNotes.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlAllNotes.Size = new System.Drawing.Size(720, 384);
            this.pnlAllNotes.TabIndex = 102;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(712, 1);
            this.label8.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(4, 380);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(712, 1);
            this.label7.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(716, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 381);
            this.label5.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 381);
            this.label4.TabIndex = 4;
            // 
            // C1AllNotes
            // 
            this.C1AllNotes.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1AllNotes.AllowEditing = false;
            this.C1AllNotes.BackColor = System.Drawing.Color.White;
            this.C1AllNotes.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1AllNotes.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.C1AllNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1AllNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1AllNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1AllNotes.Location = new System.Drawing.Point(3, 0);
            this.C1AllNotes.Name = "C1AllNotes";
            this.C1AllNotes.Padding = new System.Windows.Forms.Padding(3);
            this.C1AllNotes.Rows.Count = 1;
            this.C1AllNotes.Rows.DefaultSize = 19;
            this.C1AllNotes.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.ShowScrollTips;
            this.C1AllNotes.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1AllNotes.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.C1AllNotes.Size = new System.Drawing.Size(714, 381);
            this.C1AllNotes.StyleInfo = resources.GetString("C1AllNotes.StyleInfo");
            this.C1AllNotes.TabIndex = 3;
            this.C1AllNotes.RowColChange += new System.EventHandler(this.C1AllNotes_RowColChange);
            this.C1AllNotes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1AllNotes_MouseMove);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.C1SuperTooltip1.ShowAlways = true;
            // 
            // btnBrowseNotes
            // 
            this.btnBrowseNotes.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseNotes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseNotes.BackgroundImage")));
            this.btnBrowseNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseNotes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseNotes.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseNotes.Image")));
            this.btnBrowseNotes.Location = new System.Drawing.Point(680, 62);
            this.btnBrowseNotes.Name = "btnBrowseNotes";
            this.btnBrowseNotes.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseNotes.TabIndex = 2019;
            this.btnBrowseNotes.Tag = "";
            this.btnBrowseNotes.UseVisualStyleBackColor = false;
            this.btnBrowseNotes.Click += new System.EventHandler(this.btnBrowseNotes_Click);
            this.btnBrowseNotes.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnBrowseNotes.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // frmPatientStatementNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(720, 626);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientStatementNotes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Statement Notes";
            this.Load += new System.EventHandler(this.frmPatientStatementNotes_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnl_Header.ResumeLayout(false);
            this.pnl.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlAllNotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1AllNotes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tlb_Delete;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnSaveClose;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnl_Header;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.TextBox txtStatementNote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_pnl_BaseBottomBrd;
        private System.Windows.Forms.Label lbl_pnl_BaseTopBrd;
        private System.Windows.Forms.Label lbl_pnl_BaseLeftBrd;
        private System.Windows.Forms.Label lbl_pnl_BaseRightBrd;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlAllNotes;
        private System.Windows.Forms.ToolStripButton tlb_Notes;
        private C1.Win.C1FlexGrid.C1FlexGrid C1AllNotes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton tlb_EditNotes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripButton tlb_Cancel;
        private System.Windows.Forms.Button btnBrowseNotes;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;


    }
}