namespace gloBilling
{
    partial class frmClaimNotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClaimNotes));
            this.tls_Notes = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Notes = new System.Windows.Forms.ToolStripButton();
            this.tlb_EditNotes = new System.Windows.Forms.ToolStripButton();
            this.tlb_History = new System.Windows.Forms.ToolStripButton();
            this.tlb_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_Save = new System.Windows.Forms.ToolStripButton();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tlb_Close = new System.Windows.Forms.ToolStripButton();
            this.panelC1 = new System.Windows.Forms.Panel();
            this.C1NotesGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.panel_NoteDtl = new System.Windows.Forms.Panel();
            this.btnBrowseNotes = new System.Windows.Forms.Button();
            this.label_BillingAleartMSG = new System.Windows.Forms.Label();
            this.dtpNoteDate = new System.Windows.Forms.DateTimePicker();
            this.lblNoteDate = new System.Windows.Forms.Label();
            this.label_notes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.tls_Notes.SuspendLayout();
            this.panelC1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1NotesGrid)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel_NoteDtl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_Notes
            // 
            this.tls_Notes.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_Notes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Notes.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Notes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Notes,
            this.tlb_EditNotes,
            this.tlb_History,
            this.tlb_Delete,
            this.toolStripSeparator1,
            this.tlb_Save,
            this.tlb_Ok,
            this.tlb_Cancel,
            this.tlb_Close});
            this.tls_Notes.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Notes.Location = new System.Drawing.Point(0, 0);
            this.tls_Notes.Name = "tls_Notes";
            this.tls_Notes.Size = new System.Drawing.Size(725, 53);
            this.tls_Notes.TabIndex = 2;
            this.tls_Notes.Text = "toolStrip1";
            // 
            // tlb_Notes
            // 
            this.tlb_Notes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Notes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Notes.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Notes.Image")));
            this.tlb_Notes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Notes.Name = "tlb_Notes";
            this.tlb_Notes.Size = new System.Drawing.Size(73, 50);
            this.tlb_Notes.Tag = "Notes";
            this.tlb_Notes.Text = " Add &Note";
            this.tlb_Notes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Notes.ToolTipText = "Add Note";
            this.tlb_Notes.Click += new System.EventHandler(this.tlb_Notes_Click);
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
            this.tlb_EditNotes.Click += new System.EventHandler(this.tlb_EditNotes_Click);
            // 
            // tlb_History
            // 
            this.tlb_History.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_History.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_History.Image = ((System.Drawing.Image)(resources.GetObject("tlb_History.Image")));
            this.tlb_History.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_History.Name = "tlb_History";
            this.tlb_History.Size = new System.Drawing.Size(59, 50);
            this.tlb_History.Tag = "History";
            this.tlb_History.Text = " &History";
            this.tlb_History.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_History.Visible = false;
            // 
            // tlb_Delete
            // 
            this.tlb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Delete.Image")));
            this.tlb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Delete.Name = "tlb_Delete";
            this.tlb_Delete.Size = new System.Drawing.Size(54, 50);
            this.tlb_Delete.Tag = " Delete";
            this.tlb_Delete.Text = " &Delete";
            this.tlb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Delete.Click += new System.EventHandler(this.tlb_Delete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            this.toolStripSeparator1.Visible = false;
            // 
            // tlb_Save
            // 
            this.tlb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Save.Image")));
            this.tlb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Save.Name = "tlb_Save";
            this.tlb_Save.Size = new System.Drawing.Size(40, 50);
            this.tlb_Save.Text = "&Save";
            this.tlb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Save.ToolTipText = "Save";
            this.tlb_Save.Click += new System.EventHandler(this.tlb_Save_Click);
            // 
            // tlb_Ok
            // 
            this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.Image")));
            this.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Ok.Name = "tlb_Ok";
            this.tlb_Ok.Size = new System.Drawing.Size(66, 50);
            this.tlb_Ok.Tag = "OK";
            this.tlb_Ok.Text = "Sa&ve&&Cls";
            this.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Ok.ToolTipText = "Save and Close";
            this.tlb_Ok.Click += new System.EventHandler(this.tlb_Ok_Click);
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
            this.tlb_Cancel.Click += new System.EventHandler(this.tlb_Cancel_Click);
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
            // panelC1
            // 
            this.panelC1.BackColor = System.Drawing.Color.Transparent;
            this.panelC1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelC1.Controls.Add(this.C1NotesGrid);
            this.panelC1.Controls.Add(this.label3);
            this.panelC1.Controls.Add(this.label2);
            this.panelC1.Controls.Add(this.label1);
            this.panelC1.Controls.Add(this.label59);
            this.panelC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelC1.Location = new System.Drawing.Point(0, 160);
            this.panelC1.Name = "panelC1";
            this.panelC1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panelC1.Size = new System.Drawing.Size(725, 357);
            this.panelC1.TabIndex = 2000;
            // 
            // C1NotesGrid
            // 
            this.C1NotesGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.C1NotesGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.C1NotesGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1NotesGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1NotesGrid.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.C1NotesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1NotesGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1NotesGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1NotesGrid.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.C1NotesGrid.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.C1NotesGrid.Location = new System.Drawing.Point(4, 1);
            this.C1NotesGrid.Name = "C1NotesGrid";
            this.C1NotesGrid.Rows.Count = 1;
            this.C1NotesGrid.Rows.DefaultSize = 19;
            this.C1NotesGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1NotesGrid.Size = new System.Drawing.Size(717, 352);
            this.C1NotesGrid.StyleInfo = resources.GetString("C1NotesGrid.StyleInfo");
            this.C1NotesGrid.TabIndex = 300;
            this.C1NotesGrid.TabStop = false;
            this.C1NotesGrid.RowColChange += new System.EventHandler(this.C1NotesGrid_RowColChange);
            this.C1NotesGrid.Click += new System.EventHandler(this.C1NotesGrid_Click);
            this.C1NotesGrid.MouseLeave += new System.EventHandler(this.C1NotesGrid_MouseLeave);
            this.C1NotesGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1NotesGrid_MouseMove);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(717, 1);
            this.label3.TabIndex = 25;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(717, 1);
            this.label2.TabIndex = 24;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(721, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 354);
            this.label1.TabIndex = 23;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 354);
            this.label59.TabIndex = 22;
            this.label59.Text = "label59";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tls_Notes);
            this.panel2.Controls.Add(this.mskCloseDate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(725, 56);
            this.panel2.TabIndex = 1;
            this.panel2.TabStop = true;
            // 
            // mskCloseDate
            // 
            this.mskCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskCloseDate.Location = new System.Drawing.Point(708, 12);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(78, 22);
            this.mskCloseDate.TabIndex = 800;
            this.mskCloseDate.TabStop = false;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            this.mskCloseDate.Visible = false;
            // 
            // panel_NoteDtl
            // 
            this.panel_NoteDtl.BackColor = System.Drawing.Color.Transparent;
            this.panel_NoteDtl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_NoteDtl.Controls.Add(this.btnBrowseNotes);
            this.panel_NoteDtl.Controls.Add(this.label_BillingAleartMSG);
            this.panel_NoteDtl.Controls.Add(this.dtpNoteDate);
            this.panel_NoteDtl.Controls.Add(this.lblNoteDate);
            this.panel_NoteDtl.Controls.Add(this.label_notes);
            this.panel_NoteDtl.Controls.Add(this.txtNotes);
            this.panel_NoteDtl.Controls.Add(this.label29);
            this.panel_NoteDtl.Controls.Add(this.label70);
            this.panel_NoteDtl.Controls.Add(this.label71);
            this.panel_NoteDtl.Controls.Add(this.label72);
            this.panel_NoteDtl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_NoteDtl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_NoteDtl.Location = new System.Drawing.Point(0, 56);
            this.panel_NoteDtl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel_NoteDtl.Name = "panel_NoteDtl";
            this.panel_NoteDtl.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel_NoteDtl.Size = new System.Drawing.Size(725, 104);
            this.panel_NoteDtl.TabIndex = 0;
            this.panel_NoteDtl.TabStop = true;
            this.panel_NoteDtl.Visible = false;
            // 
            // btnBrowseNotes
            // 
            this.btnBrowseNotes.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseNotes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseNotes.BackgroundImage")));
            this.btnBrowseNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseNotes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseNotes.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseNotes.Image")));
            this.btnBrowseNotes.Location = new System.Drawing.Point(492, 34);
            this.btnBrowseNotes.Name = "btnBrowseNotes";
            this.btnBrowseNotes.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseNotes.TabIndex = 2009;
            this.btnBrowseNotes.Tag = "";
            this.btnBrowseNotes.UseVisualStyleBackColor = false;
            this.btnBrowseNotes.Click += new System.EventHandler(this.btnBrowseNotes_Click);
            this.btnBrowseNotes.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnBrowseNotes.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // label_BillingAleartMSG
            // 
            this.label_BillingAleartMSG.AutoSize = true;
            this.label_BillingAleartMSG.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_BillingAleartMSG.ForeColor = System.Drawing.Color.Red;
            this.label_BillingAleartMSG.Location = new System.Drawing.Point(492, 62);
            this.label_BillingAleartMSG.Name = "label_BillingAleartMSG";
            this.label_BillingAleartMSG.Size = new System.Drawing.Size(215, 14);
            this.label_BillingAleartMSG.TabIndex = 62;
            this.label_BillingAleartMSG.Text = "Maximum 1000 characters are allowed";
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
            this.dtpNoteDate.Location = new System.Drawing.Point(103, 9);
            this.dtpNoteDate.Name = "dtpNoteDate";
            this.dtpNoteDate.Size = new System.Drawing.Size(97, 22);
            this.dtpNoteDate.TabIndex = 2001;
            // 
            // lblNoteDate
            // 
            this.lblNoteDate.AutoSize = true;
            this.lblNoteDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblNoteDate.Location = new System.Drawing.Point(29, 12);
            this.lblNoteDate.Name = "lblNoteDate";
            this.lblNoteDate.Size = new System.Drawing.Size(72, 14);
            this.lblNoteDate.TabIndex = 2002;
            this.lblNoteDate.Text = "Note Date :";
            // 
            // label_notes
            // 
            this.label_notes.AutoSize = true;
            this.label_notes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_notes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label_notes.Location = new System.Drawing.Point(59, 36);
            this.label_notes.Name = "label_notes";
            this.label_notes.Size = new System.Drawing.Size(42, 14);
            this.label_notes.TabIndex = 60;
            this.label_notes.Text = "Note :";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.ForeColor = System.Drawing.Color.Black;
            this.txtNotes.Location = new System.Drawing.Point(103, 34);
            this.txtNotes.MaxLength = 1000;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(383, 53);
            this.txtNotes.TabIndex = 1;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Left;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(3, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 99);
            this.label29.TabIndex = 7;
            this.label29.Text = "label4";
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Right;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label70.Location = new System.Drawing.Point(721, 1);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(1, 99);
            this.label70.TabIndex = 6;
            this.label70.Text = "label3";
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Dock = System.Windows.Forms.DockStyle.Top;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(3, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(719, 1);
            this.label71.TabIndex = 5;
            this.label71.Text = "label1";
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label72.Location = new System.Drawing.Point(3, 100);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(719, 1);
            this.label72.TabIndex = 8;
            this.label72.Text = "label2";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmClaimNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(725, 517);
            this.Controls.Add(this.panelC1);
            this.Controls.Add(this.panel_NoteDtl);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClaimNotes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Claim Notes";
            this.Load += new System.EventHandler(this.frmClaimNotes_Load);
            this.tls_Notes.ResumeLayout(false);
            this.tls_Notes.PerformLayout();
            this.panelC1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1NotesGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel_NoteDtl.ResumeLayout(false);
            this.panel_NoteDtl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_Notes;
        private System.Windows.Forms.ToolStripButton tlb_Ok;
        private System.Windows.Forms.ToolStripButton tlb_Close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlb_Notes;
        private System.Windows.Forms.ToolStripButton tlb_History;
        private System.Windows.Forms.ToolStripButton tlb_Delete;
        private System.Windows.Forms.Panel panelC1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.ToolStripButton tlb_EditNotes;
        internal System.Windows.Forms.ToolStripButton tlb_Save;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel panel_NoteDtl;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.MaskedTextBox mskCloseDate;
        protected internal C1.Win.C1FlexGrid.C1FlexGrid C1NotesGrid;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label_notes;
        private System.Windows.Forms.Label label_BillingAleartMSG;
        private System.Windows.Forms.ToolStripButton tlb_Cancel;
        private System.Windows.Forms.DateTimePicker dtpNoteDate;
        private System.Windows.Forms.Label lblNoteDate;
        private System.Windows.Forms.Button btnBrowseNotes;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}