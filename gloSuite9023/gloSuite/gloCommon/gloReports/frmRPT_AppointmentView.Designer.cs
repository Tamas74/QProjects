namespace gloReports
{
    partial class frmRPT_AppointmentView
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
                    if (dtpEndDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpEndDate);
                        }
                        catch
                        {
                        }
                        dtpEndDate.Dispose();
                        dtpEndDate = null;
                    }
                }
                catch
                {
                }


                try
                {
                    if (dtpStartDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpStartDate);
                        }
                        catch
                        {
                        }
                        dtpStartDate.Dispose();
                        dtpStartDate = null;
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
                try
                {
                    if (saveFileDialog1 != null)
                    {
                        try
                        {
                            saveFileDialog1.FileOk -= new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
                        }
                        catch
                        {
                        }
                        saveFileDialog1.Dispose();
                        saveFileDialog1 = null;
                    }
                }
                catch
                {
                }
                //try
                //{
                //    if (PrintDialog1 != null)
                //    {
                //        PrintDialog1.Dispose();
                //        PrintDialog1 = null;
                //    }
                //}
                //catch
                //{
                //}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRPT_AppointmentView));
            this.pnlapptviewer = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlmedicalcat = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMedCategory = new System.Windows.Forms.ComboBox();
            this.btnClearMedCategory = new System.Windows.Forms.Button();
            this.btnbrwMedCategory = new System.Windows.Forms.Button();
            this.pnlSelectedColumns = new System.Windows.Forms.Panel();
            this.cmbSelectedColumns = new System.Windows.Forms.ComboBox();
            this.lblSelectedColumns = new System.Windows.Forms.Label();
            this.btnBrwsSelectedColumns = new System.Windows.Forms.Button();
            this.btnClearSelectedColumns = new System.Windows.Forms.Button();
            this.pnlResource = new System.Windows.Forms.Panel();
            this.cmbResouce = new System.Windows.Forms.ComboBox();
            this.lblReaource = new System.Windows.Forms.Label();
            this.btnBrwsMultiResource = new System.Windows.Forms.Button();
            this.btnClearResource = new System.Windows.Forms.Button();
            this.pnlCancelApp = new System.Windows.Forms.Panel();
            this.rbDeletedAppointments = new System.Windows.Forms.RadioButton();
            this.rbNoShowAppointments = new System.Windows.Forms.RadioButton();
            this.rbCancelAppointments = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlDates = new System.Windows.Forms.Panel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.pnlTransDate = new System.Windows.Forms.Panel();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.btnClearProvider = new System.Windows.Forms.Button();
            this.btnBrowseProvider = new System.Windows.Forms.Button();
            this.pnlPatients = new System.Windows.Forms.Panel();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.btnBrowsePatient = new System.Windows.Forms.Button();
            this.btnClearPatient = new System.Windows.Forms.Button();
            this.pnlLocation = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_GenerateReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnExportReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnGenerateBatch = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlssrsviewer = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearLocation = new System.Windows.Forms.Button();
            this.btnBrowseLocation = new System.Windows.Forms.Button();
            this.pnlapptviewer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlmedicalcat.SuspendLayout();
            this.pnlSelectedColumns.SuspendLayout();
            this.pnlResource.SuspendLayout();
            this.pnlCancelApp.SuspendLayout();
            this.pnlDates.SuspendLayout();
            this.pnlTransDate.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlPatients.SuspendLayout();
            this.pnlLocation.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlssrsviewer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlapptviewer
            // 
            this.pnlapptviewer.Controls.Add(this.panel2);
            this.pnlapptviewer.Controls.Add(this.label11);
            this.pnlapptviewer.Controls.Add(this.label10);
            this.pnlapptviewer.Controls.Add(this.label7);
            this.pnlapptviewer.Controls.Add(this.label9);
            this.pnlapptviewer.Controls.Add(this.pnlDates);
            this.pnlapptviewer.Controls.Add(this.pnlTransDate);
            this.pnlapptviewer.Controls.Add(this.pnlProvider);
            this.pnlapptviewer.Controls.Add(this.pnlPatients);
            this.pnlapptviewer.Controls.Add(this.pnlLocation);
            this.pnlapptviewer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlapptviewer.Location = new System.Drawing.Point(0, 54);
            this.pnlapptviewer.Name = "pnlapptviewer";
            this.pnlapptviewer.Padding = new System.Windows.Forms.Padding(3);
            this.pnlapptviewer.Size = new System.Drawing.Size(1284, 107);
            this.pnlapptviewer.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlmedicalcat);
            this.panel2.Controls.Add(this.pnlSelectedColumns);
            this.panel2.Controls.Add(this.pnlResource);
            this.panel2.Controls.Add(this.pnlCancelApp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(810, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(470, 99);
            this.panel2.TabIndex = 249;
            // 
            // pnlmedicalcat
            // 
            this.pnlmedicalcat.Controls.Add(this.label1);
            this.pnlmedicalcat.Controls.Add(this.cmbMedCategory);
            this.pnlmedicalcat.Controls.Add(this.btnClearMedCategory);
            this.pnlmedicalcat.Controls.Add(this.btnbrwMedCategory);
            this.pnlmedicalcat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlmedicalcat.Location = new System.Drawing.Point(0, 84);
            this.pnlmedicalcat.Name = "pnlmedicalcat";
            this.pnlmedicalcat.Size = new System.Drawing.Size(470, 15);
            this.pnlmedicalcat.TabIndex = 244;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 14);
            this.label1.TabIndex = 185;
            this.label1.Text = "Medical Category :";
            // 
            // cmbMedCategory
            // 
            this.cmbMedCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedCategory.FormattingEnabled = true;
            this.cmbMedCategory.Location = new System.Drawing.Point(127, 4);
            this.cmbMedCategory.Name = "cmbMedCategory";
            this.cmbMedCategory.Size = new System.Drawing.Size(220, 22);
            this.cmbMedCategory.TabIndex = 184;
            // 
            // btnClearMedCategory
            // 
            this.btnClearMedCategory.BackColor = System.Drawing.Color.Transparent;
            this.btnClearMedCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearMedCategory.BackgroundImage")));
            this.btnClearMedCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearMedCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearMedCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearMedCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnClearMedCategory.Image")));
            this.btnClearMedCategory.Location = new System.Drawing.Point(379, 3);
            this.btnClearMedCategory.Name = "btnClearMedCategory";
            this.btnClearMedCategory.Size = new System.Drawing.Size(24, 24);
            this.btnClearMedCategory.TabIndex = 187;
            this.btnClearMedCategory.UseVisualStyleBackColor = false;
            this.btnClearMedCategory.Click += new System.EventHandler(this.btnClearMedCategory_Click);
            // 
            // btnbrwMedCategory
            // 
            this.btnbrwMedCategory.BackColor = System.Drawing.Color.Transparent;
            this.btnbrwMedCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnbrwMedCategory.BackgroundImage")));
            this.btnbrwMedCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnbrwMedCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnbrwMedCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbrwMedCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnbrwMedCategory.Image")));
            this.btnbrwMedCategory.Location = new System.Drawing.Point(350, 3);
            this.btnbrwMedCategory.Name = "btnbrwMedCategory";
            this.btnbrwMedCategory.Size = new System.Drawing.Size(24, 24);
            this.btnbrwMedCategory.TabIndex = 186;
            this.btnbrwMedCategory.UseVisualStyleBackColor = false;
            this.btnbrwMedCategory.Click += new System.EventHandler(this.btnbrwMedCategory_Click);
            // 
            // pnlSelectedColumns
            // 
            this.pnlSelectedColumns.Controls.Add(this.cmbSelectedColumns);
            this.pnlSelectedColumns.Controls.Add(this.lblSelectedColumns);
            this.pnlSelectedColumns.Controls.Add(this.btnBrwsSelectedColumns);
            this.pnlSelectedColumns.Controls.Add(this.btnClearSelectedColumns);
            this.pnlSelectedColumns.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSelectedColumns.Location = new System.Drawing.Point(0, 56);
            this.pnlSelectedColumns.Name = "pnlSelectedColumns";
            this.pnlSelectedColumns.Size = new System.Drawing.Size(470, 28);
            this.pnlSelectedColumns.TabIndex = 245;
            // 
            // cmbSelectedColumns
            // 
            this.cmbSelectedColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectedColumns.FormattingEnabled = true;
            this.cmbSelectedColumns.Location = new System.Drawing.Point(126, 3);
            this.cmbSelectedColumns.Name = "cmbSelectedColumns";
            this.cmbSelectedColumns.Size = new System.Drawing.Size(220, 22);
            this.cmbSelectedColumns.TabIndex = 197;
            // 
            // lblSelectedColumns
            // 
            this.lblSelectedColumns.AutoSize = true;
            this.lblSelectedColumns.Location = new System.Drawing.Point(13, 7);
            this.lblSelectedColumns.Name = "lblSelectedColumns";
            this.lblSelectedColumns.Size = new System.Drawing.Size(112, 14);
            this.lblSelectedColumns.TabIndex = 196;
            this.lblSelectedColumns.Text = "Selected Columns :";
            // 
            // btnBrwsSelectedColumns
            // 
            this.btnBrwsSelectedColumns.BackColor = System.Drawing.Color.Transparent;
            this.btnBrwsSelectedColumns.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrwsSelectedColumns.BackgroundImage")));
            this.btnBrwsSelectedColumns.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrwsSelectedColumns.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrwsSelectedColumns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrwsSelectedColumns.Image = ((System.Drawing.Image)(resources.GetObject("btnBrwsSelectedColumns.Image")));
            this.btnBrwsSelectedColumns.Location = new System.Drawing.Point(350, 2);
            this.btnBrwsSelectedColumns.Name = "btnBrwsSelectedColumns";
            this.btnBrwsSelectedColumns.Size = new System.Drawing.Size(24, 24);
            this.btnBrwsSelectedColumns.TabIndex = 194;
            this.btnBrwsSelectedColumns.UseVisualStyleBackColor = false;
            this.btnBrwsSelectedColumns.Click += new System.EventHandler(this.btnBrwsSelectedColumns_Click);
            // 
            // btnClearSelectedColumns
            // 
            this.btnClearSelectedColumns.BackColor = System.Drawing.Color.Transparent;
            this.btnClearSelectedColumns.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearSelectedColumns.BackgroundImage")));
            this.btnClearSelectedColumns.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearSelectedColumns.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearSelectedColumns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSelectedColumns.Image = ((System.Drawing.Image)(resources.GetObject("btnClearSelectedColumns.Image")));
            this.btnClearSelectedColumns.Location = new System.Drawing.Point(379, 2);
            this.btnClearSelectedColumns.Name = "btnClearSelectedColumns";
            this.btnClearSelectedColumns.Size = new System.Drawing.Size(24, 24);
            this.btnClearSelectedColumns.TabIndex = 195;
            this.btnClearSelectedColumns.UseVisualStyleBackColor = false;
            this.btnClearSelectedColumns.Click += new System.EventHandler(this.btnClearSelectedColumns_Click);
            // 
            // pnlResource
            // 
            this.pnlResource.Controls.Add(this.cmbResouce);
            this.pnlResource.Controls.Add(this.lblReaource);
            this.pnlResource.Controls.Add(this.btnBrwsMultiResource);
            this.pnlResource.Controls.Add(this.btnClearResource);
            this.pnlResource.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResource.Location = new System.Drawing.Point(0, 28);
            this.pnlResource.Name = "pnlResource";
            this.pnlResource.Size = new System.Drawing.Size(470, 28);
            this.pnlResource.TabIndex = 242;
            // 
            // cmbResouce
            // 
            this.cmbResouce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResouce.FormattingEnabled = true;
            this.cmbResouce.Location = new System.Drawing.Point(126, 3);
            this.cmbResouce.Name = "cmbResouce";
            this.cmbResouce.Size = new System.Drawing.Size(220, 22);
            this.cmbResouce.TabIndex = 197;
            // 
            // lblReaource
            // 
            this.lblReaource.AutoSize = true;
            this.lblReaource.Location = new System.Drawing.Point(60, 7);
            this.lblReaource.Name = "lblReaource";
            this.lblReaource.Size = new System.Drawing.Size(65, 14);
            this.lblReaource.TabIndex = 196;
            this.lblReaource.Text = "Resource :";
            // 
            // btnBrwsMultiResource
            // 
            this.btnBrwsMultiResource.BackColor = System.Drawing.Color.Transparent;
            this.btnBrwsMultiResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrwsMultiResource.BackgroundImage")));
            this.btnBrwsMultiResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrwsMultiResource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrwsMultiResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrwsMultiResource.Image = ((System.Drawing.Image)(resources.GetObject("btnBrwsMultiResource.Image")));
            this.btnBrwsMultiResource.Location = new System.Drawing.Point(350, 2);
            this.btnBrwsMultiResource.Name = "btnBrwsMultiResource";
            this.btnBrwsMultiResource.Size = new System.Drawing.Size(24, 24);
            this.btnBrwsMultiResource.TabIndex = 194;
            this.btnBrwsMultiResource.UseVisualStyleBackColor = false;
            this.btnBrwsMultiResource.Click += new System.EventHandler(this.btnBrwsMultiResource_Click);
            // 
            // btnClearResource
            // 
            this.btnClearResource.BackColor = System.Drawing.Color.Transparent;
            this.btnClearResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearResource.BackgroundImage")));
            this.btnClearResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearResource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearResource.Image = ((System.Drawing.Image)(resources.GetObject("btnClearResource.Image")));
            this.btnClearResource.Location = new System.Drawing.Point(379, 2);
            this.btnClearResource.Name = "btnClearResource";
            this.btnClearResource.Size = new System.Drawing.Size(24, 24);
            this.btnClearResource.TabIndex = 195;
            this.btnClearResource.UseVisualStyleBackColor = false;
            this.btnClearResource.Click += new System.EventHandler(this.btnClearResource_Click);
            // 
            // pnlCancelApp
            // 
            this.pnlCancelApp.Controls.Add(this.rbDeletedAppointments);
            this.pnlCancelApp.Controls.Add(this.rbNoShowAppointments);
            this.pnlCancelApp.Controls.Add(this.rbCancelAppointments);
            this.pnlCancelApp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCancelApp.Location = new System.Drawing.Point(0, 0);
            this.pnlCancelApp.Name = "pnlCancelApp";
            this.pnlCancelApp.Size = new System.Drawing.Size(470, 28);
            this.pnlCancelApp.TabIndex = 243;
            this.pnlCancelApp.Visible = false;
            // 
            // rbDeletedAppointments
            // 
            this.rbDeletedAppointments.AutoSize = true;
            this.rbDeletedAppointments.Location = new System.Drawing.Point(335, 5);
            this.rbDeletedAppointments.Name = "rbDeletedAppointments";
            this.rbDeletedAppointments.Size = new System.Drawing.Size(68, 18);
            this.rbDeletedAppointments.TabIndex = 1;
            this.rbDeletedAppointments.Text = "Deleted";
            this.rbDeletedAppointments.UseVisualStyleBackColor = true;
            this.rbDeletedAppointments.CheckedChanged += new System.EventHandler(this.rbDeletedAppointments_CheckedChanged);
            // 
            // rbNoShowAppointments
            // 
            this.rbNoShowAppointments.AutoSize = true;
            this.rbNoShowAppointments.Location = new System.Drawing.Point(227, 5);
            this.rbNoShowAppointments.Name = "rbNoShowAppointments";
            this.rbNoShowAppointments.Size = new System.Drawing.Size(75, 18);
            this.rbNoShowAppointments.TabIndex = 1;
            this.rbNoShowAppointments.Text = "No Show";
            this.rbNoShowAppointments.UseVisualStyleBackColor = true;
            this.rbNoShowAppointments.CheckedChanged += new System.EventHandler(this.rbNoShowAppointments_CheckedChanged);
            // 
            // rbCancelAppointments
            // 
            this.rbCancelAppointments.AutoSize = true;
            this.rbCancelAppointments.Checked = true;
            this.rbCancelAppointments.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCancelAppointments.Location = new System.Drawing.Point(129, 5);
            this.rbCancelAppointments.Name = "rbCancelAppointments";
            this.rbCancelAppointments.Size = new System.Drawing.Size(64, 18);
            this.rbCancelAppointments.TabIndex = 0;
            this.rbCancelAppointments.TabStop = true;
            this.rbCancelAppointments.Text = "Cancel";
            this.rbCancelAppointments.UseVisualStyleBackColor = true;
            this.rbCancelAppointments.CheckedChanged += new System.EventHandler(this.rbCancelAppointments_CheckedChanged);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(4, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1276, 1);
            this.label11.TabIndex = 248;
            this.label11.Text = "Transaction Date :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Location = new System.Drawing.Point(1280, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 100);
            this.label10.TabIndex = 247;
            this.label10.Text = "Transaction Date :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(3, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 100);
            this.label7.TabIndex = 246;
            this.label7.Text = "Transaction Date :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1278, 1);
            this.label9.TabIndex = 245;
            this.label9.Text = "Transaction Date :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlDates
            // 
            this.pnlDates.Controls.Add(this.dtpEndDate);
            this.pnlDates.Controls.Add(this.lblStartDate);
            this.pnlDates.Controls.Add(this.lblEndDate);
            this.pnlDates.Controls.Add(this.dtpStartDate);
            this.pnlDates.Location = new System.Drawing.Point(27, 40);
            this.pnlDates.Name = "pnlDates";
            this.pnlDates.Size = new System.Drawing.Size(321, 55);
            this.pnlDates.TabIndex = 208;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(136, 29);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(110, 22);
            this.dtpEndDate.TabIndex = 7;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(61, 6);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date :";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(67, 33);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(66, 14);
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Text = "End Date :";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(136, 3);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(110, 22);
            this.dtpStartDate.TabIndex = 5;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // pnlTransDate
            // 
            this.pnlTransDate.Controls.Add(this.lbl_datefilter);
            this.pnlTransDate.Controls.Add(this.cmb_datefilter);
            this.pnlTransDate.Location = new System.Drawing.Point(27, 10);
            this.pnlTransDate.Name = "pnlTransDate";
            this.pnlTransDate.Size = new System.Drawing.Size(321, 28);
            this.pnlTransDate.TabIndex = 207;
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(25, 6);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(108, 14);
            this.lbl_datefilter.TabIndex = 216;
            this.lbl_datefilter.Text = "Transaction Date :";
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(136, 2);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(175, 22);
            this.cmb_datefilter.TabIndex = 217;
            this.cmb_datefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
            // 
            // pnlProvider
            // 
            this.pnlProvider.Controls.Add(this.label5);
            this.pnlProvider.Controls.Add(this.cmbProvider);
            this.pnlProvider.Controls.Add(this.btnClearProvider);
            this.pnlProvider.Controls.Add(this.btnBrowseProvider);
            this.pnlProvider.Location = new System.Drawing.Point(405, 39);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(383, 28);
            this.pnlProvider.TabIndex = 210;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 185;
            this.label5.Text = "Provider :";
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(85, 3);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(220, 22);
            this.cmbProvider.TabIndex = 184;
            // 
            // btnClearProvider
            // 
            this.btnClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.BackgroundImage")));
            this.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.Image")));
            this.btnClearProvider.Location = new System.Drawing.Point(337, 2);
            this.btnClearProvider.Name = "btnClearProvider";
            this.btnClearProvider.Size = new System.Drawing.Size(24, 24);
            this.btnClearProvider.TabIndex = 187;
            this.btnClearProvider.UseVisualStyleBackColor = false;
            this.btnClearProvider.Click += new System.EventHandler(this.btnClearProvider_Click);
            // 
            // btnBrowseProvider
            // 
            this.btnBrowseProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.BackgroundImage")));
            this.btnBrowseProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.Image")));
            this.btnBrowseProvider.Location = new System.Drawing.Point(309, 2);
            this.btnBrowseProvider.Name = "btnBrowseProvider";
            this.btnBrowseProvider.Size = new System.Drawing.Size(24, 24);
            this.btnBrowseProvider.TabIndex = 186;
            this.btnBrowseProvider.UseVisualStyleBackColor = false;
            this.btnBrowseProvider.Click += new System.EventHandler(this.btnBrowseProvider_Click);
            // 
            // pnlPatients
            // 
            this.pnlPatients.Controls.Add(this.cmbPatients);
            this.pnlPatients.Controls.Add(this.lblPatient);
            this.pnlPatients.Controls.Add(this.btnBrowsePatient);
            this.pnlPatients.Controls.Add(this.btnClearPatient);
            this.pnlPatients.Location = new System.Drawing.Point(405, 10);
            this.pnlPatients.Name = "pnlPatients";
            this.pnlPatients.Size = new System.Drawing.Size(383, 28);
            this.pnlPatients.TabIndex = 209;
            // 
            // cmbPatients
            // 
            this.cmbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatients.FormattingEnabled = true;
            this.cmbPatients.Location = new System.Drawing.Point(85, 3);
            this.cmbPatients.Name = "cmbPatients";
            this.cmbPatients.Size = new System.Drawing.Size(220, 22);
            this.cmbPatients.TabIndex = 197;
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Location = new System.Drawing.Point(27, 7);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(54, 14);
            this.lblPatient.TabIndex = 196;
            this.lblPatient.Text = "Patient :";
            // 
            // btnBrowsePatient
            // 
            this.btnBrowsePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.BackgroundImage")));
            this.btnBrowsePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.Image")));
            this.btnBrowsePatient.Location = new System.Drawing.Point(309, 2);
            this.btnBrowsePatient.Name = "btnBrowsePatient";
            this.btnBrowsePatient.Size = new System.Drawing.Size(24, 24);
            this.btnBrowsePatient.TabIndex = 194;
            this.btnBrowsePatient.UseVisualStyleBackColor = false;
            this.btnBrowsePatient.Click += new System.EventHandler(this.btnBrowsePatient_Click);
            // 
            // btnClearPatient
            // 
            this.btnClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.BackgroundImage")));
            this.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.Image")));
            this.btnClearPatient.Location = new System.Drawing.Point(337, 2);
            this.btnClearPatient.Name = "btnClearPatient";
            this.btnClearPatient.Size = new System.Drawing.Size(24, 24);
            this.btnClearPatient.TabIndex = 195;
            this.btnClearPatient.UseVisualStyleBackColor = false;
            this.btnClearPatient.Click += new System.EventHandler(this.btnClearPatient_Click);
            // 
            // pnlLocation
            // 
            this.pnlLocation.Controls.Add(this.btnClearLocation);
            this.pnlLocation.Controls.Add(this.btnBrowseLocation);
            this.pnlLocation.Controls.Add(this.label8);
            this.pnlLocation.Controls.Add(this.cmbLocation);
            this.pnlLocation.Location = new System.Drawing.Point(405, 68);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(383, 28);
            this.pnlLocation.TabIndex = 222;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 14);
            this.label8.TabIndex = 14;
            this.label8.Text = "Location :";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(85, 4);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(220, 22);
            this.cmbLocation.TabIndex = 13;
            // 
            // ts_Commands
            // 
            this.ts_Commands.AutoSize = false;
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_GenerateReport,
            this.tsb_btnExportReport,
            this.tsb_btnGenerateBatch,
            this.tsb_Print,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1284, 53);
            this.ts_Commands.TabIndex = 2;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_GenerateReport
            // 
            this.tsb_GenerateReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_GenerateReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_GenerateReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_GenerateReport.Image")));
            this.tsb_GenerateReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_GenerateReport.Name = "tsb_GenerateReport";
            this.tsb_GenerateReport.Size = new System.Drawing.Size(113, 50);
            this.tsb_GenerateReport.Tag = "Generate Report";
            this.tsb_GenerateReport.Text = "&Generate Report";
            this.tsb_GenerateReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_GenerateReport.ToolTipText = "Generate Report";
            // 
            // tsb_btnExportReport
            // 
            this.tsb_btnExportReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnExportReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnExportReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnExportReport.Image")));
            this.tsb_btnExportReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnExportReport.Name = "tsb_btnExportReport";
            this.tsb_btnExportReport.Size = new System.Drawing.Size(52, 50);
            this.tsb_btnExportReport.Tag = "Export";
            this.tsb_btnExportReport.Text = "&Export";
            this.tsb_btnExportReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_btnGenerateBatch
            // 
            this.tsb_btnGenerateBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnGenerateBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnGenerateBatch.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnGenerateBatch.Image")));
            this.tsb_btnGenerateBatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnGenerateBatch.Name = "tsb_btnGenerateBatch";
            this.tsb_btnGenerateBatch.Size = new System.Drawing.Size(105, 50);
            this.tsb_btnGenerateBatch.Tag = "Generate Batch";
            this.tsb_btnGenerateBatch.Text = "Generate Batch";
            this.tsb_btnGenerateBatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnGenerateBatch.Visible = false;
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(41, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = "&Print";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.ToolTipText = "Print";
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Close";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // reportViewer1
            // 
            this.reportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(3, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1278, 567);
            this.reportViewer1.TabIndex = 2;
            this.reportViewer1.RenderingComplete += new Microsoft.Reporting.WinForms.RenderingCompleteEventHandler(this.reportViewer1_RenderingComplete);
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // pnlssrsviewer
            // 
            this.pnlssrsviewer.Controls.Add(this.label6);
            this.pnlssrsviewer.Controls.Add(this.label4);
            this.pnlssrsviewer.Controls.Add(this.label3);
            this.pnlssrsviewer.Controls.Add(this.label2);
            this.pnlssrsviewer.Controls.Add(this.reportViewer1);
            this.pnlssrsviewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlssrsviewer.Location = new System.Drawing.Point(0, 161);
            this.pnlssrsviewer.Name = "pnlssrsviewer";
            this.pnlssrsviewer.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlssrsviewer.Size = new System.Drawing.Size(1284, 570);
            this.pnlssrsviewer.TabIndex = 3;
            this.pnlssrsviewer.Visible = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(1280, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 565);
            this.label6.TabIndex = 220;
            this.label6.Text = "Transaction Date :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 565);
            this.label4.TabIndex = 219;
            this.label4.Text = "Transaction Date :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(3, 566);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1278, 1);
            this.label3.TabIndex = 218;
            this.label3.Text = "Transaction Date :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1278, 1);
            this.label2.TabIndex = 217;
            this.label2.Text = "Transaction Date :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.ts_Commands);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1284, 54);
            this.panel1.TabIndex = 249;
            // 
            // btnClearLocation
            // 
            this.btnClearLocation.BackColor = System.Drawing.Color.Transparent;
            this.btnClearLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearLocation.BackgroundImage")));
            this.btnClearLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearLocation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnClearLocation.Image")));
            this.btnClearLocation.Location = new System.Drawing.Point(337, 2);
            this.btnClearLocation.Name = "btnClearLocation";
            this.btnClearLocation.Size = new System.Drawing.Size(24, 24);
            this.btnClearLocation.TabIndex = 189;
            this.btnClearLocation.UseVisualStyleBackColor = false;
            this.btnClearLocation.Click += new System.EventHandler(this.btnClearLocation_Click);
            // 
            // btnBrowseLocation
            // 
            this.btnBrowseLocation.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseLocation.BackgroundImage")));
            this.btnBrowseLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseLocation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseLocation.Image")));
            this.btnBrowseLocation.Location = new System.Drawing.Point(309, 2);
            this.btnBrowseLocation.Name = "btnBrowseLocation";
            this.btnBrowseLocation.Size = new System.Drawing.Size(24, 24);
            this.btnBrowseLocation.TabIndex = 188;
            this.btnBrowseLocation.UseVisualStyleBackColor = false;
            this.btnBrowseLocation.Click += new System.EventHandler(this.btnBrowseLocation_Click);
            // 
            // frmRPT_AppointmentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1284, 731);
            this.Controls.Add(this.pnlssrsviewer);
            this.Controls.Add(this.pnlapptviewer);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "frmRPT_AppointmentView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Appointment View";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRPT_AppointmentView_FormClosed);
            this.Load += new System.EventHandler(this.frmRPT_AppointmentView_Load);
            this.pnlapptviewer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlmedicalcat.ResumeLayout(false);
            this.pnlmedicalcat.PerformLayout();
            this.pnlSelectedColumns.ResumeLayout(false);
            this.pnlSelectedColumns.PerformLayout();
            this.pnlResource.ResumeLayout(false);
            this.pnlResource.PerformLayout();
            this.pnlCancelApp.ResumeLayout(false);
            this.pnlCancelApp.PerformLayout();
            this.pnlDates.ResumeLayout(false);
            this.pnlDates.PerformLayout();
            this.pnlTransDate.ResumeLayout(false);
            this.pnlTransDate.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
            this.pnlPatients.ResumeLayout(false);
            this.pnlPatients.PerformLayout();
            this.pnlLocation.ResumeLayout(false);
            this.pnlLocation.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlssrsviewer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.PrintDialog PrintDialog1;
        private System.Windows.Forms.Panel pnlapptviewer;
        private System.Windows.Forms.RadioButton rbDeletedAppointments;
        private System.Windows.Forms.RadioButton rbNoShowAppointments;
        private System.Windows.Forms.RadioButton rbCancelAppointments;
        private System.Windows.Forms.Panel pnlmedicalcat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbMedCategory;
        internal System.Windows.Forms.Button btnClearMedCategory;
        internal System.Windows.Forms.Button btnbrwMedCategory;
        private System.Windows.Forms.Panel pnlCancelApp;
        private System.Windows.Forms.Panel pnlResource;
        private System.Windows.Forms.ComboBox cmbResouce;
        private System.Windows.Forms.Label lblReaource;
        internal System.Windows.Forms.Button btnBrwsMultiResource;
        internal System.Windows.Forms.Button btnClearResource;
        private System.Windows.Forms.Panel pnlLocation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Panel pnlPatients;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.Label lblPatient;
        internal System.Windows.Forms.Button btnBrowsePatient;
        internal System.Windows.Forms.Button btnClearPatient;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.Button btnClearProvider;
        internal System.Windows.Forms.Button btnBrowseProvider;
        private System.Windows.Forms.Panel pnlTransDate;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.Panel pnlDates;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_GenerateReport;
        private System.Windows.Forms.ToolStripButton tsb_btnExportReport;
        private System.Windows.Forms.ToolStripButton tsb_btnGenerateBatch;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel pnlssrsviewer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlSelectedColumns;
        private System.Windows.Forms.ComboBox cmbSelectedColumns;
        private System.Windows.Forms.Label lblSelectedColumns;
        internal System.Windows.Forms.Button btnBrwsSelectedColumns;
        internal System.Windows.Forms.Button btnClearSelectedColumns;
        internal System.Windows.Forms.Button btnClearLocation;
        internal System.Windows.Forms.Button btnBrowseLocation;
    }
}