namespace gloEMRReports
{
    partial class frmReports
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpicTo, dtpicFrom };
            System.Windows.Forms.Control[] cntControls = { dtpicTo, dtpicFrom };

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);


            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControls);

                }
            }

            if (cntControls != null)
            {
                if (cntControls.Length > 0)
                {
                    gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReports));
            this.tblStrip_32 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tblbtnGenReport = new System.Windows.Forms.ToolStripButton();
            this.tblbtn_Print_32 = new System.Windows.Forms.ToolStripButton();
            this.tblbtn_Export = new System.Windows.Forms.ToolStripButton();
            this.Tblbtn_More = new System.Windows.Forms.ToolStripButton();
            this.Tblbtn_Hide = new System.Windows.Forms.ToolStripButton();
            this.tblbtn_Close_32 = new System.Windows.Forms.ToolStripButton();
            this.btnClearCPT = new System.Windows.Forms.Button();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.CRViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.btnClearAllDiag = new System.Windows.Forms.Button();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.lblAgeTo = new System.Windows.Forms.Label();
            this.dtpicTo = new System.Windows.Forms.DateTimePicker();
            this.lblAgeFrom = new System.Windows.Forms.Label();
            this.cmbAgeTo = new System.Windows.Forms.ComboBox();
            this.dtpicFrom = new System.Windows.Forms.DateTimePicker();
            this.cmbAgeFrom = new System.Windows.Forms.ComboBox();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.cmbAge = new System.Windows.Forms.ComboBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Lblage = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Label14 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.Label1 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
           // this.cmnu_Diagnosis = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pnlDrugDiagnosis = new System.Windows.Forms.Panel();
            this.pnlCheckBoxes = new System.Windows.Forms.Panel();
            this.chkDiagnosis = new System.Windows.Forms.CheckBox();
            this.chkShowPieChart = new System.Windows.Forms.CheckBox();
            this.chkShowDeatal = new System.Windows.Forms.CheckBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.pnlMed = new System.Windows.Forms.Panel();
            this.rbtnAllMedications = new System.Windows.Forms.RadioButton();
            this.rbtnPresByClinic = new System.Windows.Forms.RadioButton();
            this.lblMedication = new System.Windows.Forms.Label();
            this.BtnClearAllDrg = new System.Windows.Forms.Button();
            this.btnClearDrug = new System.Windows.Forms.Button();
            this.btnBrowseDrug = new System.Windows.Forms.Button();
            this.LstMedication = new System.Windows.Forms.ListBox();
            this.pnlDiag = new System.Windows.Forms.Panel();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.btnClearDiag = new System.Windows.Forms.Button();
            this.btnBrowseDiag = new System.Windows.Forms.Button();
            this.LstDiagnosis = new System.Windows.Forms.ListBox();
            this.pnlTreat = new System.Windows.Forms.Panel();
            this.btnClearAllCPT = new System.Windows.Forms.Button();
            this.lblTreatment = new System.Windows.Forms.Label();
            this.btnBrowseCPT = new System.Windows.Forms.Button();
            this.LstTreatment = new System.Windows.Forms.ListBox();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.pnlcustomTask = new System.Windows.Forms.Panel();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlSSRSReports = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tblStrip_32.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.pnlDrugDiagnosis.SuspendLayout();
            this.pnlCheckBoxes.SuspendLayout();
            this.pnlMed.SuspendLayout();
            this.pnlDiag.SuspendLayout();
            this.pnlTreat.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.pnlSSRSReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblStrip_32
            // 
            this.tblStrip_32.BackColor = System.Drawing.Color.Transparent;
            this.tblStrip_32.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tblStrip_32.BackgroundImage")));
            this.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tblStrip_32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblStrip_32.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tblStrip_32.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tblbtnGenReport,
            this.tblbtn_Print_32,
            this.tblbtn_Export,
            this.Tblbtn_More,
            this.Tblbtn_Hide,
            this.tblbtn_Close_32});
            this.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tblStrip_32.Location = new System.Drawing.Point(0, 0);
            this.tblStrip_32.Name = "tblStrip_32";
            this.tblStrip_32.Size = new System.Drawing.Size(1280, 53);
            this.tblStrip_32.TabIndex = 6;
            this.tblStrip_32.Text = "ToolStrip1";
            // 
            // tblbtnGenReport
            // 
            this.tblbtnGenReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblbtnGenReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblbtnGenReport.Image = ((System.Drawing.Image)(resources.GetObject("tblbtnGenReport.Image")));
            this.tblbtnGenReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtnGenReport.Name = "tblbtnGenReport";
            this.tblbtnGenReport.Size = new System.Drawing.Size(113, 50);
            this.tblbtnGenReport.Text = "&Generate Report";
            this.tblbtnGenReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tblbtnGenReport.Click += new System.EventHandler(this.tblbtnGenReport_Click);
            // 
            // tblbtn_Print_32
            // 
            this.tblbtn_Print_32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblbtn_Print_32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblbtn_Print_32.Image = ((System.Drawing.Image)(resources.GetObject("tblbtn_Print_32.Image")));
            this.tblbtn_Print_32.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtn_Print_32.Name = "tblbtn_Print_32";
            this.tblbtn_Print_32.Size = new System.Drawing.Size(41, 50);
            this.tblbtn_Print_32.Text = "&Print";
            this.tblbtn_Print_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tblbtn_Print_32.Click += new System.EventHandler(this.tblbtn_Print_32_Click);
            // 
            // tblbtn_Export
            // 
            this.tblbtn_Export.Image = ((System.Drawing.Image)(resources.GetObject("tblbtn_Export.Image")));
            this.tblbtn_Export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtn_Export.Name = "tblbtn_Export";
            this.tblbtn_Export.Size = new System.Drawing.Size(52, 50);
            this.tblbtn_Export.Text = "E&xport";
            this.tblbtn_Export.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tblbtn_Export.Click += new System.EventHandler(this.tblbtn_Export_Click);
            // 
            // Tblbtn_More
            // 
            this.Tblbtn_More.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tblbtn_More.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Tblbtn_More.Image = ((System.Drawing.Image)(resources.GetObject("Tblbtn_More.Image")));
            this.Tblbtn_More.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tblbtn_More.Name = "Tblbtn_More";
            this.Tblbtn_More.Size = new System.Drawing.Size(46, 50);
            this.Tblbtn_More.Text = "&More ";
            this.Tblbtn_More.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tblbtn_More.ToolTipText = "More Options";
            this.Tblbtn_More.Click += new System.EventHandler(this.Tblbtn_More_Click);
            // 
            // Tblbtn_Hide
            // 
            this.Tblbtn_Hide.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tblbtn_Hide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Tblbtn_Hide.Image = ((System.Drawing.Image)(resources.GetObject("Tblbtn_Hide.Image")));
            this.Tblbtn_Hide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tblbtn_Hide.Name = "Tblbtn_Hide";
            this.Tblbtn_Hide.Size = new System.Drawing.Size(38, 50);
            this.Tblbtn_Hide.Text = "&Hide";
            this.Tblbtn_Hide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tblbtn_Hide.ToolTipText = "Hide Options";
            this.Tblbtn_Hide.Visible = false;
            this.Tblbtn_Hide.Click += new System.EventHandler(this.Tblbtn_Hide_Click);
            // 
            // tblbtn_Close_32
            // 
            this.tblbtn_Close_32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblbtn_Close_32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblbtn_Close_32.Image = ((System.Drawing.Image)(resources.GetObject("tblbtn_Close_32.Image")));
            this.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtn_Close_32.Name = "tblbtn_Close_32";
            this.tblbtn_Close_32.Size = new System.Drawing.Size(43, 50);
            this.tblbtn_Close_32.Text = "&Close";
            this.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tblbtn_Close_32.Click += new System.EventHandler(this.tblbtn_Close_32_Click);
            // 
            // btnClearCPT
            // 
            this.btnClearCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.BackgroundImage")));
            this.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.Image")));
            this.btnClearCPT.Location = new System.Drawing.Point(346, 27);
            this.btnClearCPT.Name = "btnClearCPT";
            this.btnClearCPT.Size = new System.Drawing.Size(22, 22);
            this.btnClearCPT.TabIndex = 205;
            this.ToolTip1.SetToolTip(this.btnClearCPT, "Clear Treatment");
            this.btnClearCPT.UseVisualStyleBackColor = false;
            this.btnClearCPT.Click += new System.EventHandler(this.btnClearCPT_Click);
            // 
            // Panel4
            // 
            this.Panel4.AutoSize = true;
            this.Panel4.Controls.Add(this.pnlMessage);
            this.Panel4.Controls.Add(this.CRViewer);
            this.Panel4.Controls.Add(this.Label9);
            this.Panel4.Controls.Add(this.Label10);
            this.Panel4.Controls.Add(this.Label11);
            this.Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel4.Location = new System.Drawing.Point(0, 214);
            this.Panel4.Name = "Panel4";
            this.Panel4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Panel4.Size = new System.Drawing.Size(1280, 586);
            this.Panel4.TabIndex = 37;
            // 
            // pnlMessage
            // 
            this.pnlMessage.BackColor = System.Drawing.Color.Transparent;
            this.pnlMessage.Controls.Add(this.lblMessage);
            this.pnlMessage.Controls.Add(this.label48);
            this.pnlMessage.Controls.Add(this.label47);
            this.pnlMessage.Controls.Add(this.label46);
            this.pnlMessage.Controls.Add(this.label45);
            this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMessage.Location = new System.Drawing.Point(4, 0);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Size = new System.Drawing.Size(1272, 47);
            this.pnlMessage.TabIndex = 217;
            this.pnlMessage.Visible = false;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(17, 14);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(286, 16);
            this.lblMessage.TabIndex = 214;
            this.lblMessage.Text = "Select parameters to generate the report.";
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Right;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(1271, 1);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(1, 45);
            this.label48.TabIndex = 20;
            this.label48.Text = "label1";
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Left;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(0, 1);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(1, 45);
            this.label47.TabIndex = 19;
            this.label47.Text = "label1";
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Top;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(0, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1272, 1);
            this.label46.TabIndex = 18;
            this.label46.Text = "label1";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(0, 46);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1272, 1);
            this.label45.TabIndex = 17;
            this.label45.Text = "label1";
            // 
            // CRViewer
            // 
            this.CRViewer.ActiveViewIndex = -1;
            this.CRViewer.AutoSize = true;
            this.CRViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CRViewer.CausesValidation = false;
            this.CRViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.CRViewer.DisplayBackgroundEdge = false;
            this.CRViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CRViewer.EnableDrillDown = false;
            this.CRViewer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.CRViewer.Location = new System.Drawing.Point(4, 0);
            this.CRViewer.Name = "CRViewer";
            this.CRViewer.SelectionFormula = "";
            this.CRViewer.ShowCloseButton = false;
            this.CRViewer.ShowExportButton = false;
            this.CRViewer.ShowGroupTreeButton = false;
            this.CRViewer.ShowRefreshButton = false;
            this.CRViewer.Size = new System.Drawing.Size(1272, 582);
            this.CRViewer.TabIndex = 32;
            this.CRViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.CRViewer.ViewTimeSelectionFormula = "";
            this.CRViewer.Load += new System.EventHandler(this.CRViewer_Load);
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label9.Location = new System.Drawing.Point(4, 582);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(1272, 1);
            this.Label9.TabIndex = 8;
            this.Label9.Text = "label2";
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.Location = new System.Drawing.Point(3, 0);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(1, 583);
            this.Label10.TabIndex = 7;
            this.Label10.Text = "label4";
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label11.Location = new System.Drawing.Point(1276, 0);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(1, 583);
            this.Label11.TabIndex = 6;
            this.Label11.Text = "label3";
            // 
            // btnClearAllDiag
            // 
            this.btnClearAllDiag.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllDiag.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAllDiag.BackgroundImage")));
            this.btnClearAllDiag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllDiag.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAllDiag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllDiag.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllDiag.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllDiag.Image")));
            this.btnClearAllDiag.Location = new System.Drawing.Point(338, 54);
            this.btnClearAllDiag.Name = "btnClearAllDiag";
            this.btnClearAllDiag.Size = new System.Drawing.Size(22, 22);
            this.btnClearAllDiag.TabIndex = 209;
            this.ToolTip1.SetToolTip(this.btnClearAllDiag, "Clear All Diagnosis");
            this.btnClearAllDiag.UseVisualStyleBackColor = false;
            this.btnClearAllDiag.Click += new System.EventHandler(this.btnClearAllDiag_Click);
            // 
            // pnlProvider
            // 
            this.pnlProvider.Controls.Add(this.lblAgeTo);
            this.pnlProvider.Controls.Add(this.dtpicTo);
            this.pnlProvider.Controls.Add(this.lblAgeFrom);
            this.pnlProvider.Controls.Add(this.cmbAgeTo);
            this.pnlProvider.Controls.Add(this.dtpicFrom);
            this.pnlProvider.Controls.Add(this.cmbAgeFrom);
            this.pnlProvider.Controls.Add(this.cmbProvider);
            this.pnlProvider.Controls.Add(this.cmbAge);
            this.pnlProvider.Controls.Add(this.lblTo);
            this.pnlProvider.Controls.Add(this.Label19);
            this.pnlProvider.Controls.Add(this.lblFrom);
            this.pnlProvider.Controls.Add(this.Label18);
            this.pnlProvider.Controls.Add(this.Label15);
            this.pnlProvider.Controls.Add(this.Lblage);
            this.pnlProvider.Controls.Add(this.lblDate);
            this.pnlProvider.Controls.Add(this.Label5);
            this.pnlProvider.Controls.Add(this.Label13);
            this.pnlProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProvider.Location = new System.Drawing.Point(0, 54);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Padding = new System.Windows.Forms.Padding(3);
            this.pnlProvider.Size = new System.Drawing.Size(1280, 51);
            this.pnlProvider.TabIndex = 39;
            // 
            // lblAgeTo
            // 
            this.lblAgeTo.AutoSize = true;
            this.lblAgeTo.BackColor = System.Drawing.Color.Transparent;
            this.lblAgeTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgeTo.Location = new System.Drawing.Point(982, 16);
            this.lblAgeTo.Name = "lblAgeTo";
            this.lblAgeTo.Size = new System.Drawing.Size(26, 14);
            this.lblAgeTo.TabIndex = 10;
            this.lblAgeTo.Text = "To ";
            // 
            // dtpicTo
            // 
            this.dtpicTo.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpicTo.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpicTo.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpicTo.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpicTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpicTo.CustomFormat = "MM/dd/yyyy";
            this.dtpicTo.Enabled = false;
            this.dtpicTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpicTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpicTo.Location = new System.Drawing.Point(577, 12);
            this.dtpicTo.Name = "dtpicTo";
            this.dtpicTo.Size = new System.Drawing.Size(105, 22);
            this.dtpicTo.TabIndex = 3;
            // 
            // lblAgeFrom
            // 
            this.lblAgeFrom.AutoSize = true;
            this.lblAgeFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblAgeFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgeFrom.Location = new System.Drawing.Point(868, 16);
            this.lblAgeFrom.Name = "lblAgeFrom";
            this.lblAgeFrom.Size = new System.Drawing.Size(34, 14);
            this.lblAgeFrom.TabIndex = 9;
            this.lblAgeFrom.Text = "From";
            this.lblAgeFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbAgeTo
            // 
            this.cmbAgeTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgeTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAgeTo.FormattingEnabled = true;
            this.cmbAgeTo.Location = new System.Drawing.Point(1011, 12);
            this.cmbAgeTo.Name = "cmbAgeTo";
            this.cmbAgeTo.Size = new System.Drawing.Size(63, 22);
            this.cmbAgeTo.TabIndex = 7;
            // 
            // dtpicFrom
            // 
            this.dtpicFrom.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpicFrom.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpicFrom.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpicFrom.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpicFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpicFrom.Checked = false;
            this.dtpicFrom.CustomFormat = "MM/dd/yyyy";
            this.dtpicFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpicFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpicFrom.Location = new System.Drawing.Point(423, 12);
            this.dtpicFrom.Name = "dtpicFrom";
            this.dtpicFrom.ShowCheckBox = true;
            this.dtpicFrom.Size = new System.Drawing.Size(117, 22);
            this.dtpicFrom.TabIndex = 2;
            this.dtpicFrom.ValueChanged += new System.EventHandler(this.dtpicFrom_ValueChanged);
            // 
            // cmbAgeFrom
            // 
            this.cmbAgeFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgeFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAgeFrom.FormattingEnabled = true;
            this.cmbAgeFrom.Location = new System.Drawing.Point(912, 12);
            this.cmbAgeFrom.Name = "cmbAgeFrom";
            this.cmbAgeFrom.Size = new System.Drawing.Size(63, 22);
            this.cmbAgeFrom.TabIndex = 5;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(94, 12);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(183, 22);
            this.cmbProvider.TabIndex = 1;
            // 
            // cmbAge
            // 
            this.cmbAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAge.FormattingEnabled = true;
            this.cmbAge.Location = new System.Drawing.Point(749, 12);
            this.cmbAge.Name = "cmbAge";
            this.cmbAge.Size = new System.Drawing.Size(105, 22);
            this.cmbAge.TabIndex = 4;
            this.cmbAge.SelectedIndexChanged += new System.EventHandler(this.cmbAge_SelectedIndexChanged);
            this.cmbAge.TextChanged += new System.EventHandler(this.cmbAge_TextChanged);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.BackColor = System.Drawing.Color.Transparent;
            this.lblTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(551, 16);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(22, 14);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To";
            // 
            // Label19
            // 
            this.Label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(4, 47);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(1272, 1);
            this.Label19.TabIndex = 16;
            this.Label19.Text = "label1";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(387, 16);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(34, 14);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "From";
            // 
            // Label18
            // 
            this.Label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(4, 3);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(1272, 1);
            this.Label18.TabIndex = 15;
            this.Label18.Text = "label1";
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(1276, 3);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(1, 45);
            this.Label15.TabIndex = 14;
            this.Label15.Text = "label4";
            // 
            // Lblage
            // 
            this.Lblage.AutoSize = true;
            this.Lblage.BackColor = System.Drawing.Color.Transparent;
            this.Lblage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lblage.Location = new System.Drawing.Point(705, 16);
            this.Lblage.Name = "Lblage";
            this.Lblage.Size = new System.Drawing.Size(39, 14);
            this.Lblage.TabIndex = 0;
            this.Lblage.Text = "Age :";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(306, 15);
            this.lblDate.MaximumSize = new System.Drawing.Size(81, 14);
            this.lblDate.MinimumSize = new System.Drawing.Size(81, 14);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(81, 14);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = " Date :";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(25, 15);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(66, 14);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "Provider :";
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(3, 3);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(1, 45);
            this.Label13.TabIndex = 13;
            this.Label13.Text = "label4";
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label20.Location = new System.Drawing.Point(1, 99);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(198, 1);
            this.Label20.TabIndex = 16;
            this.Label20.Text = "label1";
            // 
            // Label17
            // 
            this.Label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(1, 0);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(198, 1);
            this.Label17.TabIndex = 15;
            this.Label17.Text = "label1";
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(199, 0);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(1, 100);
            this.Label16.TabIndex = 14;
            this.Label16.Text = "label4";
            // 
            // Panel3
            // 
            this.Panel3.Controls.Add(this.Label20);
            this.Panel3.Controls.Add(this.Label17);
            this.Panel3.Controls.Add(this.Label16);
            this.Panel3.Controls.Add(this.Label14);
            this.Panel3.Location = new System.Drawing.Point(466, 546);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(200, 100);
            this.Panel3.TabIndex = 38;
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(0, 0);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(1, 100);
            this.Label14.TabIndex = 13;
            this.Label14.Text = "label4";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label1.Location = new System.Drawing.Point(3, 105);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(1274, 1);
            this.Label1.TabIndex = 13;
            this.Label1.Text = "label2";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(3, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(1274, 1);
            this.Label4.TabIndex = 10;
            this.Label4.Text = "label1";
            // 
            // cmnu_Diagnosis
            // 
           // this.cmnu_Diagnosis.Name = "cmnu_Diagnosis";
           // this.cmnu_Diagnosis.Size = new System.Drawing.Size(61, 4);
            // 
            // pnlDrugDiagnosis
            // 
            this.pnlDrugDiagnosis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlDrugDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDrugDiagnosis.Controls.Add(this.pnlCheckBoxes);
            this.pnlDrugDiagnosis.Controls.Add(this.Label3);
            this.pnlDrugDiagnosis.Controls.Add(this.Label2);
            this.pnlDrugDiagnosis.Controls.Add(this.Label1);
            this.pnlDrugDiagnosis.Controls.Add(this.Label4);
            this.pnlDrugDiagnosis.Controls.Add(this.pnlMed);
            this.pnlDrugDiagnosis.Controls.Add(this.pnlDiag);
            this.pnlDrugDiagnosis.Controls.Add(this.pnlTreat);
            this.pnlDrugDiagnosis.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDrugDiagnosis.Location = new System.Drawing.Point(0, 105);
            this.pnlDrugDiagnosis.Name = "pnlDrugDiagnosis";
            this.pnlDrugDiagnosis.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlDrugDiagnosis.Size = new System.Drawing.Size(1280, 109);
            this.pnlDrugDiagnosis.TabIndex = 35;
            this.pnlDrugDiagnosis.Visible = false;
            // 
            // pnlCheckBoxes
            // 
            this.pnlCheckBoxes.Controls.Add(this.chkDiagnosis);
            this.pnlCheckBoxes.Controls.Add(this.chkShowPieChart);
            this.pnlCheckBoxes.Controls.Add(this.chkShowDeatal);
            this.pnlCheckBoxes.Location = new System.Drawing.Point(0, 0);
            this.pnlCheckBoxes.Name = "pnlCheckBoxes";
            this.pnlCheckBoxes.Size = new System.Drawing.Size(383, 88);
            this.pnlCheckBoxes.TabIndex = 216;
            this.pnlCheckBoxes.Visible = false;
            // 
            // chkDiagnosis
            // 
            this.chkDiagnosis.AutoSize = true;
            this.chkDiagnosis.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkDiagnosis.Location = new System.Drawing.Point(14, 59);
            this.chkDiagnosis.Name = "chkDiagnosis";
            this.chkDiagnosis.Size = new System.Drawing.Size(306, 18);
            this.chkDiagnosis.TabIndex = 2;
            this.chkDiagnosis.Text = "Include all hypertension codes ICD 401 – 405";
            this.chkDiagnosis.UseVisualStyleBackColor = true;
            this.chkDiagnosis.CheckedChanged += new System.EventHandler(this.chkDiagnosis_CheckedChanged);
            // 
            // chkShowPieChart
            // 
            this.chkShowPieChart.AutoSize = true;
            this.chkShowPieChart.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkShowPieChart.Location = new System.Drawing.Point(14, 35);
            this.chkShowPieChart.Name = "chkShowPieChart";
            this.chkShowPieChart.Size = new System.Drawing.Size(131, 18);
            this.chkShowPieChart.TabIndex = 1;
            this.chkShowPieChart.Text = "Include Pie Chart";
            this.chkShowPieChart.UseVisualStyleBackColor = true;
            this.chkShowPieChart.CheckedChanged += new System.EventHandler(this.chkShowPieChart_CheckedChanged);
            // 
            // chkShowDeatal
            // 
            this.chkShowDeatal.AutoSize = true;
            this.chkShowDeatal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkShowDeatal.Location = new System.Drawing.Point(14, 9);
            this.chkShowDeatal.Name = "chkShowDeatal";
            this.chkShowDeatal.Size = new System.Drawing.Size(203, 18);
            this.chkShowDeatal.TabIndex = 0;
            this.chkShowDeatal.Text = "Include Measurement Details";
            this.chkShowDeatal.UseVisualStyleBackColor = true;
            this.chkShowDeatal.CheckedChanged += new System.EventHandler(this.chkShowDeatal_CheckedChanged);
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label3.Location = new System.Drawing.Point(1276, 1);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(1, 104);
            this.Label3.TabIndex = 11;
            this.Label3.Text = "label3";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(3, 1);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1, 104);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "label4";
            // 
            // pnlMed
            // 
            this.pnlMed.Controls.Add(this.rbtnAllMedications);
            this.pnlMed.Controls.Add(this.rbtnPresByClinic);
            this.pnlMed.Controls.Add(this.lblMedication);
            this.pnlMed.Controls.Add(this.BtnClearAllDrg);
            this.pnlMed.Controls.Add(this.btnClearDrug);
            this.pnlMed.Controls.Add(this.btnBrowseDrug);
            this.pnlMed.Controls.Add(this.LstMedication);
            this.pnlMed.Location = new System.Drawing.Point(11, 10);
            this.pnlMed.Name = "pnlMed";
            this.pnlMed.Size = new System.Drawing.Size(378, 92);
            this.pnlMed.TabIndex = 213;
            // 
            // rbtnAllMedications
            // 
            this.rbtnAllMedications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.rbtnAllMedications.AutoSize = true;
            this.rbtnAllMedications.Checked = true;
            this.rbtnAllMedications.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnAllMedications.Location = new System.Drawing.Point(85, 71);
            this.rbtnAllMedications.Name = "rbtnAllMedications";
            this.rbtnAllMedications.Size = new System.Drawing.Size(40, 18);
            this.rbtnAllMedications.TabIndex = 215;
            this.rbtnAllMedications.TabStop = true;
            this.rbtnAllMedications.Text = "All";
            this.rbtnAllMedications.UseVisualStyleBackColor = true;
            // 
            // rbtnPresByClinic
            // 
            this.rbtnPresByClinic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.rbtnPresByClinic.AutoSize = true;
            this.rbtnPresByClinic.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnPresByClinic.Location = new System.Drawing.Point(146, 71);
            this.rbtnPresByClinic.Name = "rbtnPresByClinic";
            this.rbtnPresByClinic.Size = new System.Drawing.Size(128, 18);
            this.rbtnPresByClinic.TabIndex = 214;
            this.rbtnPresByClinic.Text = "Prescribed by Clinic";
            this.rbtnPresByClinic.UseVisualStyleBackColor = true;
            // 
            // lblMedication
            // 
            this.lblMedication.BackColor = System.Drawing.Color.Transparent;
            this.lblMedication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedication.Location = new System.Drawing.Point(0, 3);
            this.lblMedication.Name = "lblMedication";
            this.lblMedication.Size = new System.Drawing.Size(80, 46);
            this.lblMedication.TabIndex = 213;
            this.lblMedication.Text = "Medication:";
            // 
            // BtnClearAllDrg
            // 
            this.BtnClearAllDrg.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearAllDrg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnClearAllDrg.BackgroundImage")));
            this.BtnClearAllDrg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnClearAllDrg.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BtnClearAllDrg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClearAllDrg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearAllDrg.Image = ((System.Drawing.Image)(resources.GetObject("BtnClearAllDrg.Image")));
            this.BtnClearAllDrg.Location = new System.Drawing.Point(339, 53);
            this.BtnClearAllDrg.Name = "BtnClearAllDrg";
            this.BtnClearAllDrg.Size = new System.Drawing.Size(22, 22);
            this.BtnClearAllDrg.TabIndex = 208;
            this.ToolTip1.SetToolTip(this.BtnClearAllDrg, "Clear All Medication");
            this.BtnClearAllDrg.UseVisualStyleBackColor = false;
            this.BtnClearAllDrg.Click += new System.EventHandler(this.BtnClearAllDrg_Click);
            // 
            // btnClearDrug
            // 
            this.btnClearDrug.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDrug.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDrug.BackgroundImage")));
            this.btnClearDrug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDrug.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDrug.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDrug.Image")));
            this.btnClearDrug.Location = new System.Drawing.Point(339, 27);
            this.btnClearDrug.Name = "btnClearDrug";
            this.btnClearDrug.Size = new System.Drawing.Size(22, 22);
            this.btnClearDrug.TabIndex = 207;
            this.ToolTip1.SetToolTip(this.btnClearDrug, "Clear Medication");
            this.btnClearDrug.UseVisualStyleBackColor = false;
            this.btnClearDrug.Click += new System.EventHandler(this.btnClearDrug_Click);
            // 
            // btnBrowseDrug
            // 
            this.btnBrowseDrug.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDrug.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseDrug.BackgroundImage")));
            this.btnBrowseDrug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDrug.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDrug.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDrug.Image")));
            this.btnBrowseDrug.Location = new System.Drawing.Point(339, 1);
            this.btnBrowseDrug.Name = "btnBrowseDrug";
            this.btnBrowseDrug.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseDrug.TabIndex = 206;
            this.ToolTip1.SetToolTip(this.btnBrowseDrug, "Browse Medication");
            this.btnBrowseDrug.UseVisualStyleBackColor = false;
            this.btnBrowseDrug.Click += new System.EventHandler(this.btnBrowseDrug_Click);
            // 
            // LstMedication
            // 
            this.LstMedication.FormattingEnabled = true;
            this.LstMedication.Location = new System.Drawing.Point(82, 1);
            this.LstMedication.Name = "LstMedication";
            this.LstMedication.Size = new System.Drawing.Size(250, 69);
            this.LstMedication.TabIndex = 204;
            // 
            // pnlDiag
            // 
            this.pnlDiag.Controls.Add(this.lblDiagnosis);
            this.pnlDiag.Controls.Add(this.btnClearAllDiag);
            this.pnlDiag.Controls.Add(this.btnClearDiag);
            this.pnlDiag.Controls.Add(this.btnBrowseDiag);
            this.pnlDiag.Controls.Add(this.LstDiagnosis);
            this.pnlDiag.Location = new System.Drawing.Point(417, 9);
            this.pnlDiag.Name = "pnlDiag";
            this.pnlDiag.Size = new System.Drawing.Size(390, 92);
            this.pnlDiag.TabIndex = 214;
            // 
            // lblDiagnosis
            // 
            this.lblDiagnosis.BackColor = System.Drawing.Color.Transparent;
            this.lblDiagnosis.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosis.Location = new System.Drawing.Point(4, 4);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Size = new System.Drawing.Size(73, 46);
            this.lblDiagnosis.TabIndex = 213;
            this.lblDiagnosis.Text = "Diagnosis :";
            // 
            // btnClearDiag
            // 
            this.btnClearDiag.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDiag.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDiag.BackgroundImage")));
            this.btnClearDiag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDiag.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearDiag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDiag.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDiag.Image")));
            this.btnClearDiag.Location = new System.Drawing.Point(338, 28);
            this.btnClearDiag.Name = "btnClearDiag";
            this.btnClearDiag.Size = new System.Drawing.Size(22, 22);
            this.btnClearDiag.TabIndex = 205;
            this.ToolTip1.SetToolTip(this.btnClearDiag, "Clear Diagnosis");
            this.btnClearDiag.UseVisualStyleBackColor = false;
            this.btnClearDiag.Click += new System.EventHandler(this.btnClearDiag_Click);
            // 
            // btnBrowseDiag
            // 
            this.btnBrowseDiag.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDiag.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiag.BackgroundImage")));
            this.btnBrowseDiag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDiag.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseDiag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDiag.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiag.Image")));
            this.btnBrowseDiag.Location = new System.Drawing.Point(338, 2);
            this.btnBrowseDiag.Name = "btnBrowseDiag";
            this.btnBrowseDiag.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseDiag.TabIndex = 204;
            this.ToolTip1.SetToolTip(this.btnBrowseDiag, "Browse Diagnosis");
            this.btnBrowseDiag.UseVisualStyleBackColor = false;
            this.btnBrowseDiag.Click += new System.EventHandler(this.btnBrowseDiag_Click);
            // 
            // LstDiagnosis
            // 
            this.LstDiagnosis.FormattingEnabled = true;
            this.LstDiagnosis.Location = new System.Drawing.Point(82, 2);
            this.LstDiagnosis.Name = "LstDiagnosis";
            this.LstDiagnosis.Size = new System.Drawing.Size(250, 69);
            this.LstDiagnosis.TabIndex = 203;
            // 
            // pnlTreat
            // 
            this.pnlTreat.Controls.Add(this.btnClearAllCPT);
            this.pnlTreat.Controls.Add(this.lblTreatment);
            this.pnlTreat.Controls.Add(this.btnClearCPT);
            this.pnlTreat.Controls.Add(this.btnBrowseCPT);
            this.pnlTreat.Controls.Add(this.LstTreatment);
            this.pnlTreat.Location = new System.Drawing.Point(823, 10);
            this.pnlTreat.Name = "pnlTreat";
            this.pnlTreat.Size = new System.Drawing.Size(395, 83);
            this.pnlTreat.TabIndex = 215;
            // 
            // btnClearAllCPT
            // 
            this.btnClearAllCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAllCPT.BackgroundImage")));
            this.btnClearAllCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAllCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllCPT.Image")));
            this.btnClearAllCPT.Location = new System.Drawing.Point(346, 53);
            this.btnClearAllCPT.Name = "btnClearAllCPT";
            this.btnClearAllCPT.Size = new System.Drawing.Size(22, 22);
            this.btnClearAllCPT.TabIndex = 214;
            this.ToolTip1.SetToolTip(this.btnClearAllCPT, "Clear All Treatment");
            this.btnClearAllCPT.UseVisualStyleBackColor = false;
            this.btnClearAllCPT.Click += new System.EventHandler(this.btnClearAllCPT_Click);
            // 
            // lblTreatment
            // 
            this.lblTreatment.AutoSize = true;
            this.lblTreatment.BackColor = System.Drawing.Color.Transparent;
            this.lblTreatment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTreatment.Location = new System.Drawing.Point(8, 3);
            this.lblTreatment.Name = "lblTreatment";
            this.lblTreatment.Size = new System.Drawing.Size(79, 14);
            this.lblTreatment.TabIndex = 213;
            this.lblTreatment.Text = "Treatment :";
            // 
            // btnBrowseCPT
            // 
            this.btnBrowseCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.BackgroundImage")));
            this.btnBrowseCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.Image")));
            this.btnBrowseCPT.Location = new System.Drawing.Point(346, 1);
            this.btnBrowseCPT.Name = "btnBrowseCPT";
            this.btnBrowseCPT.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseCPT.TabIndex = 204;
            this.ToolTip1.SetToolTip(this.btnBrowseCPT, "Browse Treatment");
            this.btnBrowseCPT.UseVisualStyleBackColor = false;
            this.btnBrowseCPT.Click += new System.EventHandler(this.btnBrowseCPT_Click);
            // 
            // LstTreatment
            // 
            this.LstTreatment.FormattingEnabled = true;
            this.LstTreatment.Location = new System.Drawing.Point(90, 1);
            this.LstTreatment.Name = "LstTreatment";
            this.LstTreatment.Size = new System.Drawing.Size(250, 69);
            this.LstTreatment.TabIndex = 203;
            // 
            // Panel2
            // 
            this.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Panel2.Controls.Add(this.tblStrip_32);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(1280, 54);
            this.Panel2.TabIndex = 36;
            // 
            // pnlcustomTask
            // 
            this.pnlcustomTask.Location = new System.Drawing.Point(844, 141);
            this.pnlcustomTask.Name = "pnlcustomTask";
            this.pnlcustomTask.Size = new System.Drawing.Size(373, 225);
            this.pnlcustomTask.TabIndex = 34;
            this.pnlcustomTask.Visible = false;
            // 
            // pnlSSRSReports
            // 
            this.pnlSSRSReports.AutoSize = true;
            this.pnlSSRSReports.Controls.Add(this.label12);
            this.pnlSSRSReports.Controls.Add(this.label6);
            this.pnlSSRSReports.Controls.Add(this.label7);
            this.pnlSSRSReports.Controls.Add(this.label8);
            this.pnlSSRSReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSSRSReports.Location = new System.Drawing.Point(0, 214);
            this.pnlSSRSReports.Name = "pnlSSRSReports";
            this.pnlSSRSReports.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlSSRSReports.Size = new System.Drawing.Size(1280, 586);
            this.pnlSSRSReports.TabIndex = 40;
            this.pnlSSRSReports.Visible = false;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label12.Location = new System.Drawing.Point(4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1272, 1);
            this.label12.TabIndex = 10;
            this.label12.Text = "label2";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(4, 582);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1272, 1);
            this.label6.TabIndex = 8;
            this.label6.Text = "label2";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 583);
            this.label7.TabIndex = 7;
            this.label7.Text = "label4";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(1276, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 583);
            this.label8.TabIndex = 6;
            this.label8.Text = "label3";
            // 
            // frmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.pnlcustomTask);
            this.Controls.Add(this.Panel4);
            this.Controls.Add(this.pnlSSRSReports);
            this.Controls.Add(this.pnlDrugDiagnosis);
            this.Controls.Add(this.Panel3);
            this.Controls.Add(this.pnlProvider);
            this.Controls.Add(this.Panel2);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "frmReports";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.frmReports_Load);
            this.FormClosed +=new System.Windows.Forms.FormClosedEventHandler(frmReports_FormClosed);

            this.tblStrip_32.ResumeLayout(false);
            this.tblStrip_32.PerformLayout();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.pnlMessage.ResumeLayout(false);
            this.pnlMessage.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.pnlDrugDiagnosis.ResumeLayout(false);
            this.pnlCheckBoxes.ResumeLayout(false);
            this.pnlCheckBoxes.PerformLayout();
            this.pnlMed.ResumeLayout(false);
            this.pnlMed.PerformLayout();
            this.pnlDiag.ResumeLayout(false);
            this.pnlTreat.ResumeLayout(false);
            this.pnlTreat.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.pnlSSRSReports.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus tblStrip_32;
        internal System.Windows.Forms.ToolStripButton tblbtnGenReport;
        internal System.Windows.Forms.ToolStripButton tblbtn_Print_32;
        internal System.Windows.Forms.ToolStripButton Tblbtn_More;
        internal System.Windows.Forms.ToolStripButton Tblbtn_Hide;
        internal System.Windows.Forms.ToolStripButton tblbtn_Close_32;
        internal System.Windows.Forms.Button btnClearCPT;
        internal System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.Panel Panel4;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.Label Label10;
        private System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Button btnClearAllDiag;
        internal System.Windows.Forms.Panel pnlProvider;
        internal System.Windows.Forms.Label lblAgeTo;
        internal System.Windows.Forms.DateTimePicker dtpicTo;
        internal System.Windows.Forms.Label lblAgeFrom;
        internal System.Windows.Forms.ComboBox cmbAgeTo;
        internal System.Windows.Forms.DateTimePicker dtpicFrom;
        internal System.Windows.Forms.ComboBox cmbAgeFrom;
        internal System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.ComboBox cmbAge;
        internal System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label Label18;
        private System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Lblage;
        internal System.Windows.Forms.Label lblDate;
        internal System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label13;
        private System.Windows.Forms.Label Label20;
        private System.Windows.Forms.Label Label17;
        private System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Panel Panel3;
        private System.Windows.Forms.Label Label14;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label4;
     //   internal System.Windows.Forms.ContextMenuStrip cmnu_Diagnosis;
        internal System.Windows.Forms.Panel pnlDrugDiagnosis;
        internal System.Windows.Forms.Button btnClearDiag;
        internal System.Windows.Forms.Button btnBrowseCPT;
        internal System.Windows.Forms.Button btnBrowseDiag;
        internal System.Windows.Forms.Button BtnClearAllDrg;
        internal System.Windows.Forms.ListBox LstTreatment;
        internal System.Windows.Forms.ListBox LstDiagnosis;
        internal System.Windows.Forms.Button btnClearDrug;
        private System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button btnBrowseDrug;
        private System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ListBox LstMedication;
        internal System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Panel pnlMed;
        internal System.Windows.Forms.Label lblMedication;
        private System.Windows.Forms.Panel pnlDiag;
        internal System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.Panel pnlTreat;
        internal System.Windows.Forms.Label lblTreatment;
        internal System.Windows.Forms.Button btnClearAllCPT;
        private System.Windows.Forms.ToolStripButton tblbtn_Export;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer CRViewer;
        internal System.Windows.Forms.RadioButton rbtnAllMedications;
        internal System.Windows.Forms.RadioButton rbtnPresByClinic;
        private System.Windows.Forms.Panel pnlCheckBoxes;
        private System.Windows.Forms.CheckBox chkShowPieChart;
        private System.Windows.Forms.CheckBox chkShowDeatal;
        private System.Windows.Forms.CheckBox chkDiagnosis;
        internal System.Windows.Forms.Panel pnlSSRSReports;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlMessage;
        internal System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
     //   internal System.Windows.Forms.Panel pnlcustomTask;
    }
}