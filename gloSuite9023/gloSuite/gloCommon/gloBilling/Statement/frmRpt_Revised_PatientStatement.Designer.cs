namespace gloBilling.Statement
{
    partial class frmRpt_Revised_PatientStatement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    

        //protected virtual void Dispose(bool disposing)
        //{
        //    // Check to see if Dispose has already been called. 
        //    if (!(this.blnDisposed))
        //    {
        //        // If disposing equals true, dispose all managed 
        //        // and unmanaged resources. 
        //        if ((disposing))
        //        {
        //            // Dispose managed resources. 
        //            if ((components != null))
        //            {
        //                components.Dispose();
        //            }
        //            //frm = Nothing 
        //        }
        //        // Release unmanaged resources. If disposing is false, 
        //        // only the following code is executed. 

        //        // Note that this is not thread safe. 
        //        // Another thread could start disposing the object 
        //        // after the managed resources are disposed, 
        //        // but before the disposed flag is set to true. 
        //        // If thread safety is necessary, it must be 
        //        // implemented by the client. 
        //    }
        //    frm = null;
        //    this.blnDisposed = true;

        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    // Take yourself off of the finalization queue 
        //    // to prevent finalization code for this object 
        //    // from executing a second time. 
        //    System.GC.SuppressFinalize(this);
        //}

        //protected void Finalize()
        //{
        //    Dispose(false);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_Revised_PatientStatement));
            this.pnlcrvReportViewer = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.crvReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.grpPayType = new System.Windows.Forms.GroupBox();
            this.cmbSettings = new System.Windows.Forms.ComboBox();
            this.lblSelectSettings = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_btnIndividual = new System.Windows.Forms.ToolStripButton();
            this.tsb_GenerateBatch = new System.Windows.Forms.ToolStripButton();
            this.tsb_PatAcctAccount = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnBatch = new System.Windows.Forms.ToolStripButton();
            this.tsb_ViewStatement = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnExportReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnShowList = new System.Windows.Forms.ToolStripButton();
            this.tsb_Send = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsb_Send_Electronic = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_Print = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtDueAmt = new System.Windows.Forms.TextBox();
            this.cmbWaitDays = new System.Windows.Forms.TextBox();
            this.grpPatientNameCriteria = new System.Windows.Forms.GroupBox();
            this.cmbNameTo = new System.Windows.Forms.TextBox();
            this.cmbNameFrom = new System.Windows.Forms.TextBox();
            this.pnlMainStatement = new System.Windows.Forms.Panel();
            this.pnlCriteria = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.pnlSelectSet = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblNameTo = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lblNameFrom = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblWaitDays = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDueAmt = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.dtCriteriaEndDate = new System.Windows.Forms.DateTimePicker();
            this.pnlLastBatch = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblmaxCreateDate = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbldtStatementDate = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.lblSettings = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.grpCDueDate = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.dtCriteriaStartDate = new System.Windows.Forms.DateTimePicker();
            this.label24 = new System.Windows.Forms.Label();
            this.numCriteriaDuration = new System.Windows.Forms.NumericUpDown();
            this.pnlPatientNameCriteria = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlCurrentBatch = new System.Windows.Forms.Panel();
            this.lblTotaldue = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.pnlBusinessCenter = new System.Windows.Forms.Panel();
            this.btnClearBusinessCenter = new System.Windows.Forms.Button();
            this.cmbBusinessCenter = new System.Windows.Forms.ComboBox();
            this.lblBusinessCenter = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.pnlPatientList = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.lblPatientDue = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.lbldtcreate = new System.Windows.Forms.Label();
            this.lblUName = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.lbldtstdate = new System.Windows.Forms.Label();
            this.lblptName = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label78 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label79 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label62 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label63 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlDuedate = new System.Windows.Forms.Panel();
            this.gbSelectFollowupType = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlNoFiliterPatient = new System.Windows.Forms.Panel();
            this.btnBrowseAcount = new System.Windows.Forms.Button();
            this.lblAccount = new System.Windows.Forms.Label();
            this.cmbAccounts = new System.Windows.Forms.ComboBox();
            this.btnBrowsePatient = new System.Windows.Forms.Button();
            this.btnClearPatient = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlCDuedate = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbCriteriaTransactionDate = new System.Windows.Forms.ComboBox();
            this.pnlFilteredPatList = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.c1PatientList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlc1PatientListHeader = new System.Windows.Forms.Panel();
            this.pnlc1PatientList = new System.Windows.Forms.Panel();
            this.numQueueClaimCount = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.pnlProgressBar = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
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
            this.pnlPleasewait = new System.Windows.Forms.Panel();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlcrvReportViewer.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.grpPatientNameCriteria.SuspendLayout();
            this.pnlMainStatement.SuspendLayout();
            this.pnlCriteria.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlSelectSet.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlLastBatch.SuspendLayout();
            this.panel6.SuspendLayout();
            this.grpCDueDate.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCriteriaDuration)).BeginInit();
            this.pnlCurrentBatch.SuspendLayout();
            this.pnlBusinessCenter.SuspendLayout();
            this.pnlPatientList.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.pnlDuedate.SuspendLayout();
            this.gbSelectFollowupType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            this.pnlNoFiliterPatient.SuspendLayout();
            this.pnlFilteredPatList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).BeginInit();
            this.pnlc1PatientListHeader.SuspendLayout();
            this.pnlc1PatientList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQueueClaimCount)).BeginInit();
            this.pnlProgressBar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlPleasewait.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlcrvReportViewer
            // 
            this.pnlcrvReportViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlcrvReportViewer.Controls.Add(this.label19);
            this.pnlcrvReportViewer.Controls.Add(this.label20);
            this.pnlcrvReportViewer.Controls.Add(this.label21);
            this.pnlcrvReportViewer.Controls.Add(this.label22);
            this.pnlcrvReportViewer.Controls.Add(this.crvReportViewer);
            this.pnlcrvReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlcrvReportViewer.Location = new System.Drawing.Point(0, 144);
            this.pnlcrvReportViewer.Name = "pnlcrvReportViewer";
            this.pnlcrvReportViewer.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlcrvReportViewer.Size = new System.Drawing.Size(1117, 611);
            this.pnlcrvReportViewer.TabIndex = 91;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(4, 607);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1109, 1);
            this.label19.TabIndex = 33;
            this.label19.Text = "label19";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Location = new System.Drawing.Point(4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1109, 1);
            this.label20.TabIndex = 32;
            this.label20.Text = "label20";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Right;
            this.label21.Location = new System.Drawing.Point(1113, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 608);
            this.label21.TabIndex = 31;
            this.label21.Text = "label21";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Location = new System.Drawing.Point(3, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 608);
            this.label22.TabIndex = 30;
            this.label22.Text = "label22";
            // 
            // crvReportViewer
            // 
            this.crvReportViewer.ActiveViewIndex = -1;
            this.crvReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvReportViewer.DisplayBackgroundEdge = false;
            this.crvReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvReportViewer.EnableDrillDown = false;
            this.crvReportViewer.Font = new System.Drawing.Font("Tahoma", 9F);
            this.crvReportViewer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.crvReportViewer.Location = new System.Drawing.Point(3, 0);
            this.crvReportViewer.Name = "crvReportViewer";
            this.crvReportViewer.SelectionFormula = "";
            this.crvReportViewer.ShowCloseButton = false;
            this.crvReportViewer.ShowGroupTreeButton = false;
            this.crvReportViewer.ShowPrintButton = false;
            this.crvReportViewer.ShowRefreshButton = false;
            this.crvReportViewer.Size = new System.Drawing.Size(1111, 608);
            this.crvReportViewer.TabIndex = 29;
            this.crvReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crvReportViewer.ViewTimeSelectionFormula = "";
            this.crvReportViewer.ClickPage += new CrystalDecisions.Windows.Forms.PageMouseEventHandler(this.CRV_ClickPage);
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_datefilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_datefilter.Location = new System.Drawing.Point(17, 36);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(108, 14);
            this.lbl_datefilter.TabIndex = 216;
            this.lbl_datefilter.Text = "Transaction Date :";
            this.lbl_datefilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_datefilter.Visible = false;
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(129, 32);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(118, 22);
            this.cmb_datefilter.TabIndex = 217;
            this.cmb_datefilter.Visible = false;
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
            this.dtpEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(129, 32);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(100, 22);
            this.dtpEndDate.TabIndex = 7;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblStartDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblStartDate.Location = new System.Drawing.Point(256, 36);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date :";
            this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStartDate.Visible = false;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEndDate.Location = new System.Drawing.Point(60, 36);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(66, 14);
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Text = "End Date :";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(331, 32);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(93, 22);
            this.dtpStartDate.TabIndex = 5;
            this.dtpStartDate.Visible = false;
            // 
            // cmbPatients
            // 
            this.cmbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatients.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatients.FormattingEnabled = true;
            this.cmbPatients.Location = new System.Drawing.Point(125, 2);
            this.cmbPatients.Name = "cmbPatients";
            this.cmbPatients.Size = new System.Drawing.Size(186, 22);
            this.cmbPatients.TabIndex = 197;
            this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
            this.cmbPatients.MouseEnter += new System.EventHandler(this.cmbPatients_MouseEnter);
            // 
            // lblPatient
            // 
            this.lblPatient.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatient.Location = new System.Drawing.Point(67, 6);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(54, 14);
            this.lblPatient.TabIndex = 196;
            this.lblPatient.Text = "Patient :";
            this.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpPayType
            // 
            this.grpPayType.Location = new System.Drawing.Point(1066, 66);
            this.grpPayType.Name = "grpPayType";
            this.grpPayType.Size = new System.Drawing.Size(36, 33);
            this.grpPayType.TabIndex = 2;
            this.grpPayType.TabStop = false;
            this.grpPayType.Visible = false;
            // 
            // cmbSettings
            // 
            this.cmbSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSettings.ForeColor = System.Drawing.Color.Black;
            this.cmbSettings.FormattingEnabled = true;
            this.cmbSettings.Location = new System.Drawing.Point(141, 0);
            this.cmbSettings.Name = "cmbSettings";
            this.cmbSettings.Size = new System.Drawing.Size(203, 22);
            this.cmbSettings.TabIndex = 219;
            this.cmbSettings.SelectedIndexChanged += new System.EventHandler(this.cmbSettings_SelectedIndexChanged);
            this.cmbSettings.MouseEnter += new System.EventHandler(this.cmbSettings_MouseEnter);
            // 
            // lblSelectSettings
            // 
            this.lblSelectSettings.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSelectSettings.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSelectSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSelectSettings.Location = new System.Drawing.Point(0, 0);
            this.lblSelectSettings.Name = "lblSelectSettings";
            this.lblSelectSettings.Size = new System.Drawing.Size(141, 22);
            this.lblSelectSettings.TabIndex = 218;
            this.lblSelectSettings.Text = "Settings :";
            this.lblSelectSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Controls.Add(this.groupBox4);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1117, 56);
            this.pnlToolStrip.TabIndex = 257;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_btnIndividual,
            this.tsb_GenerateBatch,
            this.tsb_PatAcctAccount,
            this.tsb_btnBatch,
            this.tsb_ViewStatement,
            this.tsb_btnExportReport,
            this.tsb_btnShowList,
            this.tsb_Send,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1117, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_btnIndividual
            // 
            this.tsb_btnIndividual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnIndividual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnIndividual.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnIndividual.Image")));
            this.tsb_btnIndividual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnIndividual.Name = "tsb_btnIndividual";
            this.tsb_btnIndividual.Size = new System.Drawing.Size(71, 50);
            this.tsb_btnIndividual.Tag = "ShowList";
            this.tsb_btnIndividual.Text = "&Individual";
            this.tsb_btnIndividual.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnIndividual.ToolTipText = "Show Individual";
            this.tsb_btnIndividual.Click += new System.EventHandler(this.tsb_btnIndividual_Click);
            // 
            // tsb_GenerateBatch
            // 
            this.tsb_GenerateBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_GenerateBatch.Image = ((System.Drawing.Image)(resources.GetObject("tsb_GenerateBatch.Image")));
            this.tsb_GenerateBatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_GenerateBatch.Name = "tsb_GenerateBatch";
            this.tsb_GenerateBatch.Size = new System.Drawing.Size(105, 50);
            this.tsb_GenerateBatch.Tag = "GenerateBatch";
            this.tsb_GenerateBatch.Text = "&Generate Batch";
            this.tsb_GenerateBatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_GenerateBatch.Click += new System.EventHandler(this.tsb_GenerateBatch_Click);
            // 
            // tsb_PatAcctAccount
            // 
            this.tsb_PatAcctAccount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PatAcctAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PatAcctAccount.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatAcctAccount.Image")));
            this.tsb_PatAcctAccount.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatAcctAccount.Name = "tsb_PatAcctAccount";
            this.tsb_PatAcctAccount.Size = new System.Drawing.Size(71, 50);
            this.tsb_PatAcctAccount.Tag = "Hide";
            this.tsb_PatAcctAccount.Text = "P&at. Acct.";
            this.tsb_PatAcctAccount.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PatAcctAccount.ToolTipText = "Patient Account";
            this.tsb_PatAcctAccount.Visible = false;
            this.tsb_PatAcctAccount.Click += new System.EventHandler(this.tsb_PatAcctAccount_Click);
            // 
            // tsb_btnBatch
            // 
            this.tsb_btnBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnBatch.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnBatch.Image")));
            this.tsb_btnBatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnBatch.Name = "tsb_btnBatch";
            this.tsb_btnBatch.Size = new System.Drawing.Size(46, 50);
            this.tsb_btnBatch.Tag = "Show Batch";
            this.tsb_btnBatch.Text = "&Batch";
            this.tsb_btnBatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnBatch.ToolTipText = "Show Batch";
            this.tsb_btnBatch.Visible = false;
            this.tsb_btnBatch.Click += new System.EventHandler(this.tsb_btnBatch_Click);
            // 
            // tsb_ViewStatement
            // 
            this.tsb_ViewStatement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ViewStatement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ViewStatement.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ViewStatement.Image")));
            this.tsb_ViewStatement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ViewStatement.Name = "tsb_ViewStatement";
            this.tsb_ViewStatement.Size = new System.Drawing.Size(110, 50);
            this.tsb_ViewStatement.Tag = "View Statement";
            this.tsb_ViewStatement.Text = "&View Statement";
            this.tsb_ViewStatement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ViewStatement.ToolTipText = "View Statement";
            this.tsb_ViewStatement.Click += new System.EventHandler(this.tsb_ViewStatement_Click);
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
            this.tsb_btnExportReport.Visible = false;
            // 
            // tsb_btnShowList
            // 
            this.tsb_btnShowList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnShowList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnShowList.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnShowList.Image")));
            this.tsb_btnShowList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnShowList.Name = "tsb_btnShowList";
            this.tsb_btnShowList.Size = new System.Drawing.Size(72, 50);
            this.tsb_btnShowList.Tag = "ShowList";
            this.tsb_btnShowList.Text = "Show &List";
            this.tsb_btnShowList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnShowList.ToolTipText = "Show List";
            this.tsb_btnShowList.Visible = false;
            this.tsb_btnShowList.Click += new System.EventHandler(this.tsb_btnShowList_Click);
            // 
            // tsb_Send
            // 
            this.tsb_Send.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Send_Electronic,
            this.tsb_Print});
            this.tsb_Send.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Send.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Send.Image")));
            this.tsb_Send.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Send.Name = "tsb_Send";
            this.tsb_Send.Size = new System.Drawing.Size(51, 50);
            this.tsb_Send.Text = "&Send";
            this.tsb_Send.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Send_Electronic
            // 
            this.tsb_Send_Electronic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Send_Electronic.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Send_Electronic.Image")));
            this.tsb_Send_Electronic.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Send_Electronic.Name = "tsb_Send_Electronic";
            this.tsb_Send_Electronic.Size = new System.Drawing.Size(168, 22);
            this.tsb_Send_Electronic.Text = "Send Electronic";
            this.tsb_Send_Electronic.Click += new System.EventHandler(this.tsb_Send_Electronic_Click);
            // 
            // tsb_Print
            // 
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(168, 22);
            this.tsb_Print.Text = "Print";
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
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
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(962, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(19, 24);
            this.groupBox4.TabIndex = 231;
            this.groupBox4.TabStop = false;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Location = new System.Drawing.Point(104, 51);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(42, 14);
            this.label39.TabIndex = 228;
            this.label39.Text = "From :";
            this.label39.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label39.Visible = false;
            // 
            // txtDueAmt
            // 
            this.txtDueAmt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtDueAmt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDueAmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtDueAmt.Location = new System.Drawing.Point(66, 48);
            this.txtDueAmt.MaxLength = 12;
            this.txtDueAmt.Name = "txtDueAmt";
            this.txtDueAmt.ReadOnly = true;
            this.txtDueAmt.Size = new System.Drawing.Size(10, 22);
            this.txtDueAmt.TabIndex = 227;
            this.txtDueAmt.Text = "0.00";
            this.txtDueAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDueAmt.Visible = false;
            // 
            // cmbWaitDays
            // 
            this.cmbWaitDays.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmbWaitDays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWaitDays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmbWaitDays.Location = new System.Drawing.Point(88, 48);
            this.cmbWaitDays.MaxLength = 12;
            this.cmbWaitDays.Name = "cmbWaitDays";
            this.cmbWaitDays.ReadOnly = true;
            this.cmbWaitDays.Size = new System.Drawing.Size(10, 22);
            this.cmbWaitDays.TabIndex = 242;
            this.cmbWaitDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cmbWaitDays.Visible = false;
            // 
            // grpPatientNameCriteria
            // 
            this.grpPatientNameCriteria.Controls.Add(this.cmbNameTo);
            this.grpPatientNameCriteria.Controls.Add(this.cmbNameFrom);
            this.grpPatientNameCriteria.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPatientNameCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpPatientNameCriteria.Location = new System.Drawing.Point(173, 43);
            this.grpPatientNameCriteria.Name = "grpPatientNameCriteria";
            this.grpPatientNameCriteria.Size = new System.Drawing.Size(27, 22);
            this.grpPatientNameCriteria.TabIndex = 231;
            this.grpPatientNameCriteria.TabStop = false;
            this.grpPatientNameCriteria.Text = "Patient Name ";
            this.grpPatientNameCriteria.Visible = false;
            // 
            // cmbNameTo
            // 
            this.cmbNameTo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmbNameTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNameTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmbNameTo.Location = new System.Drawing.Point(131, 19);
            this.cmbNameTo.MaxLength = 12;
            this.cmbNameTo.Name = "cmbNameTo";
            this.cmbNameTo.ReadOnly = true;
            this.cmbNameTo.Size = new System.Drawing.Size(31, 22);
            this.cmbNameTo.TabIndex = 244;
            this.cmbNameTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cmbNameTo.Visible = false;
            // 
            // cmbNameFrom
            // 
            this.cmbNameFrom.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmbNameFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNameFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmbNameFrom.Location = new System.Drawing.Point(58, 19);
            this.cmbNameFrom.MaxLength = 12;
            this.cmbNameFrom.Name = "cmbNameFrom";
            this.cmbNameFrom.ReadOnly = true;
            this.cmbNameFrom.Size = new System.Drawing.Size(29, 22);
            this.cmbNameFrom.TabIndex = 243;
            this.cmbNameFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cmbNameFrom.Visible = false;
            // 
            // pnlMainStatement
            // 
            this.pnlMainStatement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMainStatement.Controls.Add(this.pnlCriteria);
            this.pnlMainStatement.Controls.Add(this.pnlPatientList);
            this.pnlMainStatement.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMainStatement.Location = new System.Drawing.Point(0, 56);
            this.pnlMainStatement.Name = "pnlMainStatement";
            this.pnlMainStatement.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMainStatement.Size = new System.Drawing.Size(1117, 62);
            this.pnlMainStatement.TabIndex = 30;
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.Controls.Add(this.panel2);
            this.pnlCriteria.Controls.Add(this.pnlBusinessCenter);
            this.pnlCriteria.Controls.Add(this.label8);
            this.pnlCriteria.Controls.Add(this.label4);
            this.pnlCriteria.Controls.Add(this.label5);
            this.pnlCriteria.Controls.Add(this.label48);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCriteria.Location = new System.Drawing.Point(3, 0);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Size = new System.Drawing.Size(1111, 59);
            this.pnlCriteria.TabIndex = 233;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.pnlSelectSet);
            this.panel2.Controls.Add(this.dtCriteriaEndDate);
            this.panel2.Controls.Add(this.pnlLastBatch);
            this.panel2.Controls.Add(this.pnlCurrentBatch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1109, 29);
            this.panel2.TabIndex = 236;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Location = new System.Drawing.Point(88, 34);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(66, 14);
            this.label27.TabIndex = 232;
            this.label27.Text = "End Date :";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlSelectSet
            // 
            this.pnlSelectSet.Controls.Add(this.panel4);
            this.pnlSelectSet.Controls.Add(this.label53);
            this.pnlSelectSet.Controls.Add(this.label52);
            this.pnlSelectSet.Controls.Add(this.cmbSettings);
            this.pnlSelectSet.Controls.Add(this.lblSelectSettings);
            this.pnlSelectSet.Location = new System.Drawing.Point(12, 3);
            this.pnlSelectSet.Name = "pnlSelectSet";
            this.pnlSelectSet.Size = new System.Drawing.Size(1109, 22);
            this.pnlSelectSet.TabIndex = 232;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblNameTo);
            this.panel4.Controls.Add(this.label40);
            this.panel4.Controls.Add(this.lblNameFrom);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.lblWaitDays);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.lblDueAmt);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(358, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(751, 22);
            this.panel4.TabIndex = 238;
            // 
            // lblNameTo
            // 
            this.lblNameTo.AutoSize = true;
            this.lblNameTo.BackColor = System.Drawing.Color.Transparent;
            this.lblNameTo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNameTo.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblNameTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblNameTo.Location = new System.Drawing.Point(499, 0);
            this.lblNameTo.Name = "lblNameTo";
            this.lblNameTo.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblNameTo.Size = new System.Drawing.Size(0, 18);
            this.lblNameTo.TabIndex = 248;
            this.lblNameTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Dock = System.Windows.Forms.DockStyle.Left;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Location = new System.Drawing.Point(448, 0);
            this.label40.Name = "label40";
            this.label40.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label40.Size = new System.Drawing.Size(51, 18);
            this.label40.TabIndex = 228;
            this.label40.Text = "through";
            this.label40.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblNameFrom
            // 
            this.lblNameFrom.AutoSize = true;
            this.lblNameFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblNameFrom.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNameFrom.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblNameFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblNameFrom.Location = new System.Drawing.Point(448, 0);
            this.lblNameFrom.Name = "lblNameFrom";
            this.lblNameFrom.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblNameFrom.Size = new System.Drawing.Size(0, 18);
            this.lblNameFrom.TabIndex = 247;
            this.lblNameFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(337, 0);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label9.Size = new System.Drawing.Size(111, 18);
            this.label9.TabIndex = 246;
            this.label9.Text = "  Include Patients :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWaitDays
            // 
            this.lblWaitDays.AutoSize = true;
            this.lblWaitDays.BackColor = System.Drawing.Color.Transparent;
            this.lblWaitDays.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblWaitDays.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblWaitDays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblWaitDays.Location = new System.Drawing.Point(337, 0);
            this.lblWaitDays.Name = "lblWaitDays";
            this.lblWaitDays.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblWaitDays.Size = new System.Drawing.Size(0, 18);
            this.lblWaitDays.TabIndex = 245;
            this.lblWaitDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(167, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label6.Size = new System.Drawing.Size(170, 18);
            this.label6.TabIndex = 240;
            this.label6.Text = "  Days Between Statements: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDueAmt
            // 
            this.lblDueAmt.AutoSize = true;
            this.lblDueAmt.BackColor = System.Drawing.Color.Transparent;
            this.lblDueAmt.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDueAmt.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblDueAmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblDueAmt.Location = new System.Drawing.Point(167, 0);
            this.lblDueAmt.Name = "lblDueAmt";
            this.lblDueAmt.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblDueAmt.Size = new System.Drawing.Size(0, 18);
            this.lblDueAmt.TabIndex = 243;
            this.lblDueAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(156, 0);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label11.Size = new System.Drawing.Size(11, 22);
            this.label11.TabIndex = 244;
            this.label11.Text = "$";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 22);
            this.label3.TabIndex = 238;
            this.label3.Text = "Balances Greater Than:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.Transparent;
            this.label53.Dock = System.Windows.Forms.DockStyle.Left;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label53.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Location = new System.Drawing.Point(354, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(4, 22);
            this.label53.TabIndex = 261;
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.Transparent;
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(344, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(10, 22);
            this.label52.TabIndex = 260;
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtCriteriaEndDate
            // 
            this.dtCriteriaEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtCriteriaEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtCriteriaEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtCriteriaEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtCriteriaEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtCriteriaEndDate.Checked = false;
            this.dtCriteriaEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtCriteriaEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtCriteriaEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCriteriaEndDate.Location = new System.Drawing.Point(155, 29);
            this.dtCriteriaEndDate.Name = "dtCriteriaEndDate";
            this.dtCriteriaEndDate.Size = new System.Drawing.Size(100, 22);
            this.dtCriteriaEndDate.TabIndex = 233;
            // 
            // pnlLastBatch
            // 
            this.pnlLastBatch.Controls.Add(this.panel6);
            this.pnlLastBatch.Controls.Add(this.lbldtStatementDate);
            this.pnlLastBatch.Controls.Add(this.label46);
            this.pnlLastBatch.Controls.Add(this.lblSettings);
            this.pnlLastBatch.Controls.Add(this.label47);
            this.pnlLastBatch.Controls.Add(this.grpCDueDate);
            this.pnlLastBatch.Location = new System.Drawing.Point(35, 55);
            this.pnlLastBatch.Name = "pnlLastBatch";
            this.pnlLastBatch.Size = new System.Drawing.Size(1109, 22);
            this.pnlLastBatch.TabIndex = 237;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.lblmaxCreateDate);
            this.panel6.Controls.Add(this.lblUserName);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.ForeColor = System.Drawing.Color.Blue;
            this.panel6.Location = new System.Drawing.Point(229, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(243, 22);
            this.panel6.TabIndex = 256;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(12, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label7.Size = new System.Drawing.Size(12, 18);
            this.label7.TabIndex = 258;
            this.label7.Text = "]";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblmaxCreateDate
            // 
            this.lblmaxCreateDate.AutoSize = true;
            this.lblmaxCreateDate.BackColor = System.Drawing.Color.Transparent;
            this.lblmaxCreateDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblmaxCreateDate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblmaxCreateDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblmaxCreateDate.Location = new System.Drawing.Point(12, 0);
            this.lblmaxCreateDate.Name = "lblmaxCreateDate";
            this.lblmaxCreateDate.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblmaxCreateDate.Size = new System.Drawing.Size(0, 18);
            this.lblmaxCreateDate.TabIndex = 255;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblUserName.Location = new System.Drawing.Point(12, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblUserName.Size = new System.Drawing.Size(0, 18);
            this.lblUserName.TabIndex = 254;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label2.Size = new System.Drawing.Size(12, 18);
            this.label2.TabIndex = 257;
            this.label2.Text = "[";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbldtStatementDate
            // 
            this.lbldtStatementDate.AutoSize = true;
            this.lbldtStatementDate.BackColor = System.Drawing.Color.Transparent;
            this.lbldtStatementDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbldtStatementDate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbldtStatementDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbldtStatementDate.Location = new System.Drawing.Point(229, 0);
            this.lbldtStatementDate.Name = "lbldtStatementDate";
            this.lbldtStatementDate.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lbldtStatementDate.Size = new System.Drawing.Size(0, 18);
            this.lbldtStatementDate.TabIndex = 252;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.Transparent;
            this.label46.Dock = System.Windows.Forms.DockStyle.Left;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Location = new System.Drawing.Point(137, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(92, 22);
            this.label46.TabIndex = 251;
            this.label46.Text = "Generated On";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSettings
            // 
            this.lblSettings.AutoSize = true;
            this.lblSettings.BackColor = System.Drawing.Color.Transparent;
            this.lblSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSettings.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSettings.Location = new System.Drawing.Point(137, 0);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblSettings.Size = new System.Drawing.Size(0, 18);
            this.lblSettings.TabIndex = 249;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.Transparent;
            this.label47.Dock = System.Windows.Forms.DockStyle.Left;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Location = new System.Drawing.Point(0, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(137, 22);
            this.label47.TabIndex = 250;
            this.label47.Text = "Last Batch For";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpCDueDate
            // 
            this.grpCDueDate.Controls.Add(this.groupBox1);
            this.grpCDueDate.Controls.Add(this.label39);
            this.grpCDueDate.Controls.Add(this.txtDueAmt);
            this.grpCDueDate.Controls.Add(this.grpPatientNameCriteria);
            this.grpCDueDate.Controls.Add(this.label24);
            this.grpCDueDate.Controls.Add(this.numCriteriaDuration);
            this.grpCDueDate.Controls.Add(this.cmbWaitDays);
            this.grpCDueDate.Controls.Add(this.pnlPatientNameCriteria);
            this.grpCDueDate.Controls.Add(this.groupBox2);
            this.grpCDueDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCDueDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpCDueDate.Location = new System.Drawing.Point(1065, 87);
            this.grpCDueDate.Name = "grpCDueDate";
            this.grpCDueDate.Size = new System.Drawing.Size(22, 34);
            this.grpCDueDate.TabIndex = 230;
            this.grpCDueDate.TabStop = false;
            this.grpCDueDate.Text = "Due Date ";
            this.grpCDueDate.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.dtCriteriaStartDate);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox1.Location = new System.Drawing.Point(13, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 26);
            this.groupBox1.TabIndex = 229;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Balance Greater Than";
            this.groupBox1.Visible = false;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Location = new System.Drawing.Point(7, 18);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(72, 14);
            this.label26.TabIndex = 230;
            this.label26.Text = "Start Date :";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label26.Visible = false;
            // 
            // dtCriteriaStartDate
            // 
            this.dtCriteriaStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtCriteriaStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtCriteriaStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtCriteriaStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtCriteriaStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtCriteriaStartDate.Checked = false;
            this.dtCriteriaStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtCriteriaStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtCriteriaStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCriteriaStartDate.Location = new System.Drawing.Point(83, 14);
            this.dtCriteriaStartDate.Name = "dtCriteriaStartDate";
            this.dtCriteriaStartDate.ShowCheckBox = true;
            this.dtCriteriaStartDate.Size = new System.Drawing.Size(108, 22);
            this.dtCriteriaStartDate.TabIndex = 231;
            this.dtCriteriaStartDate.Visible = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Location = new System.Drawing.Point(12, 21);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 14);
            this.label24.TabIndex = 228;
            this.label24.Text = "Due Date :";
            this.label24.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // numCriteriaDuration
            // 
            this.numCriteriaDuration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCriteriaDuration.Location = new System.Drawing.Point(84, 17);
            this.numCriteriaDuration.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numCriteriaDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCriteriaDuration.Name = "numCriteriaDuration";
            this.numCriteriaDuration.Size = new System.Drawing.Size(52, 22);
            this.numCriteriaDuration.TabIndex = 229;
            this.numCriteriaDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // pnlPatientNameCriteria
            // 
            this.pnlPatientNameCriteria.Location = new System.Drawing.Point(324, 61);
            this.pnlPatientNameCriteria.Name = "pnlPatientNameCriteria";
            this.pnlPatientNameCriteria.Size = new System.Drawing.Size(27, 43);
            this.pnlPatientNameCriteria.TabIndex = 237;
            this.pnlPatientNameCriteria.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(384, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(195, 40);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Visible = false;
            // 
            // pnlCurrentBatch
            // 
            this.pnlCurrentBatch.Controls.Add(this.lblTotaldue);
            this.pnlCurrentBatch.Controls.Add(this.label51);
            this.pnlCurrentBatch.Controls.Add(this.label50);
            this.pnlCurrentBatch.Controls.Add(this.lblCount);
            this.pnlCurrentBatch.Controls.Add(this.label49);
            this.pnlCurrentBatch.Location = new System.Drawing.Point(20, 82);
            this.pnlCurrentBatch.Name = "pnlCurrentBatch";
            this.pnlCurrentBatch.Size = new System.Drawing.Size(596, 23);
            this.pnlCurrentBatch.TabIndex = 261;
            // 
            // lblTotaldue
            // 
            this.lblTotaldue.AutoSize = true;
            this.lblTotaldue.BackColor = System.Drawing.Color.Transparent;
            this.lblTotaldue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTotaldue.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblTotaldue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTotaldue.Location = new System.Drawing.Point(208, 0);
            this.lblTotaldue.Name = "lblTotaldue";
            this.lblTotaldue.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTotaldue.Size = new System.Drawing.Size(0, 18);
            this.lblTotaldue.TabIndex = 259;
            this.lblTotaldue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Dock = System.Windows.Forms.DockStyle.Left;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Location = new System.Drawing.Point(194, 0);
            this.label51.Name = "label51";
            this.label51.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label51.Size = new System.Drawing.Size(14, 18);
            this.label51.TabIndex = 260;
            this.label51.Text = "$";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.BackColor = System.Drawing.Color.Transparent;
            this.label50.Dock = System.Windows.Forms.DockStyle.Left;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Location = new System.Drawing.Point(143, 0);
            this.label50.Name = "label50";
            this.label50.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label50.Size = new System.Drawing.Size(51, 18);
            this.label50.TabIndex = 258;
            this.label50.Text = "Patients";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCount.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCount.Location = new System.Drawing.Point(143, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblCount.Size = new System.Drawing.Size(0, 18);
            this.lblCount.TabIndex = 257;
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.Transparent;
            this.label49.Dock = System.Windows.Forms.DockStyle.Left;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Location = new System.Drawing.Point(0, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(143, 23);
            this.label49.TabIndex = 256;
            this.label49.Text = "Current Batch :";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlBusinessCenter
            // 
            this.pnlBusinessCenter.Controls.Add(this.btnClearBusinessCenter);
            this.pnlBusinessCenter.Controls.Add(this.cmbBusinessCenter);
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCenter);
            this.pnlBusinessCenter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusinessCenter.Location = new System.Drawing.Point(1, 1);
            this.pnlBusinessCenter.Name = "pnlBusinessCenter";
            this.pnlBusinessCenter.Size = new System.Drawing.Size(1109, 28);
            this.pnlBusinessCenter.TabIndex = 239;
            // 
            // btnClearBusinessCenter
            // 
            this.btnClearBusinessCenter.AutoEllipsis = true;
            this.btnClearBusinessCenter.BackColor = System.Drawing.Color.Transparent;
            this.btnClearBusinessCenter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearBusinessCenter.BackgroundImage")));
            this.btnClearBusinessCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearBusinessCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearBusinessCenter.Image = ((System.Drawing.Image)(resources.GetObject("btnClearBusinessCenter.Image")));
            this.btnClearBusinessCenter.Location = new System.Drawing.Point(363, 5);
            this.btnClearBusinessCenter.Name = "btnClearBusinessCenter";
            this.btnClearBusinessCenter.Size = new System.Drawing.Size(21, 21);
            this.btnClearBusinessCenter.TabIndex = 263;
            this.toolTip1.SetToolTip(this.btnClearBusinessCenter, "Clear Business Center");
            this.btnClearBusinessCenter.UseVisualStyleBackColor = false;
            this.btnClearBusinessCenter.Visible = false;
            this.btnClearBusinessCenter.Click += new System.EventHandler(this.btnClearBusinessCenter_Click);
            // 
            // cmbBusinessCenter
            // 
            this.cmbBusinessCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBusinessCenter.ForeColor = System.Drawing.Color.Black;
            this.cmbBusinessCenter.FormattingEnabled = true;
            this.cmbBusinessCenter.Location = new System.Drawing.Point(153, 4);
            this.cmbBusinessCenter.Name = "cmbBusinessCenter";
            this.cmbBusinessCenter.Size = new System.Drawing.Size(203, 22);
            this.cmbBusinessCenter.TabIndex = 265;
            this.cmbBusinessCenter.SelectedIndexChanged += new System.EventHandler(this.cmbSettings_SelectedIndexChanged);
            this.cmbBusinessCenter.MouseEnter += new System.EventHandler(this.cmbBusinessCenter_MouseEnter);
            // 
            // lblBusinessCenter
            // 
            this.lblBusinessCenter.BackColor = System.Drawing.Color.Transparent;
            this.lblBusinessCenter.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblBusinessCenter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBusinessCenter.Location = new System.Drawing.Point(0, 0);
            this.lblBusinessCenter.Name = "lblBusinessCenter";
            this.lblBusinessCenter.Size = new System.Drawing.Size(153, 28);
            this.lblBusinessCenter.TabIndex = 264;
            this.lblBusinessCenter.Text = "Business Center :";
            this.lblBusinessCenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(1, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1109, 1);
            this.label8.TabIndex = 236;
            this.label8.Text = "label8";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(1110, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 58);
            this.label4.TabIndex = 234;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 58);
            this.label5.TabIndex = 233;
            this.label5.Text = "label5";
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Top;
            this.label48.Location = new System.Drawing.Point(0, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(1111, 1);
            this.label48.TabIndex = 262;
            this.label48.Text = "label48";
            // 
            // pnlPatientList
            // 
            this.pnlPatientList.Controls.Add(this.panel14);
            this.pnlPatientList.Controls.Add(this.panel10);
            this.pnlPatientList.Controls.Add(this.panel11);
            this.pnlPatientList.Controls.Add(this.panel7);
            this.pnlPatientList.Controls.Add(this.label10);
            this.pnlPatientList.Controls.Add(this.pnlDuedate);
            this.pnlPatientList.Controls.Add(this.label12);
            this.pnlPatientList.Controls.Add(this.pnlNoFiliterPatient);
            this.pnlPatientList.Controls.Add(this.grpPayType);
            this.pnlPatientList.Controls.Add(this.dtpEndDate);
            this.pnlPatientList.Controls.Add(this.lbl_datefilter);
            this.pnlPatientList.Controls.Add(this.label14);
            this.pnlPatientList.Controls.Add(this.lblStartDate);
            this.pnlPatientList.Controls.Add(this.cmb_datefilter);
            this.pnlPatientList.Controls.Add(this.dtpStartDate);
            this.pnlPatientList.Controls.Add(this.lblEndDate);
            this.pnlPatientList.Controls.Add(this.label13);
            this.pnlPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientList.Location = new System.Drawing.Point(3, 0);
            this.pnlPatientList.Name = "pnlPatientList";
            this.pnlPatientList.Size = new System.Drawing.Size(1111, 59);
            this.pnlPatientList.TabIndex = 235;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.lblPatientDue);
            this.panel14.Controls.Add(this.label82);
            this.panel14.Controls.Add(this.label84);
            this.panel14.Controls.Add(this.label85);
            this.panel14.Location = new System.Drawing.Point(20, 90);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(292, 23);
            this.panel14.TabIndex = 262;
            // 
            // lblPatientDue
            // 
            this.lblPatientDue.AutoSize = true;
            this.lblPatientDue.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientDue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPatientDue.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPatientDue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatientDue.Location = new System.Drawing.Point(120, 0);
            this.lblPatientDue.Name = "lblPatientDue";
            this.lblPatientDue.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblPatientDue.Size = new System.Drawing.Size(0, 18);
            this.lblPatientDue.TabIndex = 259;
            this.lblPatientDue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Dock = System.Windows.Forms.DockStyle.Left;
            this.label82.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label82.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label82.Location = new System.Drawing.Point(106, 0);
            this.label82.Name = "label82";
            this.label82.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label82.Size = new System.Drawing.Size(14, 18);
            this.label82.TabIndex = 260;
            this.label82.Text = "$";
            this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.BackColor = System.Drawing.Color.Transparent;
            this.label84.Dock = System.Windows.Forms.DockStyle.Left;
            this.label84.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label84.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label84.Location = new System.Drawing.Point(106, 0);
            this.label84.Name = "label84";
            this.label84.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label84.Size = new System.Drawing.Size(0, 18);
            this.label84.TabIndex = 257;
            this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label85
            // 
            this.label85.BackColor = System.Drawing.Color.Transparent;
            this.label85.Dock = System.Windows.Forms.DockStyle.Left;
            this.label85.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label85.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label85.Location = new System.Drawing.Point(0, 0);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(106, 23);
            this.label85.TabIndex = 256;
            this.label85.Text = "Total Due :";
            this.label85.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.label65);
            this.panel10.Controls.Add(this.label66);
            this.panel10.Controls.Add(this.label67);
            this.panel10.Controls.Add(this.label68);
            this.panel10.Controls.Add(this.label69);
            this.panel10.Location = new System.Drawing.Point(18, 90);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(292, 23);
            this.panel10.TabIndex = 262;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.BackColor = System.Drawing.Color.Transparent;
            this.label65.Dock = System.Windows.Forms.DockStyle.Left;
            this.label65.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label65.ForeColor = System.Drawing.Color.Blue;
            this.label65.Location = new System.Drawing.Point(208, 0);
            this.label65.Name = "label65";
            this.label65.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label65.Size = new System.Drawing.Size(0, 18);
            this.label65.TabIndex = 259;
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.BackColor = System.Drawing.Color.Transparent;
            this.label66.Dock = System.Windows.Forms.DockStyle.Left;
            this.label66.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.ForeColor = System.Drawing.Color.Blue;
            this.label66.Location = new System.Drawing.Point(194, 0);
            this.label66.Name = "label66";
            this.label66.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label66.Size = new System.Drawing.Size(14, 18);
            this.label66.TabIndex = 260;
            this.label66.Text = "$";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.BackColor = System.Drawing.Color.Transparent;
            this.label67.Dock = System.Windows.Forms.DockStyle.Left;
            this.label67.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label67.ForeColor = System.Drawing.Color.Blue;
            this.label67.Location = new System.Drawing.Point(143, 0);
            this.label67.Name = "label67";
            this.label67.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label67.Size = new System.Drawing.Size(51, 18);
            this.label67.TabIndex = 258;
            this.label67.Text = "Patients";
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.BackColor = System.Drawing.Color.Transparent;
            this.label68.Dock = System.Windows.Forms.DockStyle.Left;
            this.label68.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label68.ForeColor = System.Drawing.Color.Blue;
            this.label68.Location = new System.Drawing.Point(143, 0);
            this.label68.Name = "label68";
            this.label68.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label68.Size = new System.Drawing.Size(0, 18);
            this.label68.TabIndex = 257;
            this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label69
            // 
            this.label69.BackColor = System.Drawing.Color.Transparent;
            this.label69.Dock = System.Windows.Forms.DockStyle.Left;
            this.label69.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label69.ForeColor = System.Drawing.Color.Blue;
            this.label69.Location = new System.Drawing.Point(0, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(143, 23);
            this.label69.TabIndex = 256;
            this.label69.Text = "Current Batch :";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.panel12);
            this.panel11.Controls.Add(this.lbldtstdate);
            this.panel11.Controls.Add(this.lblptName);
            this.panel11.Controls.Add(this.label77);
            this.panel11.Controls.Add(this.label74);
            this.panel11.Controls.Add(this.groupBox8);
            this.panel11.Location = new System.Drawing.Point(27, 61);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(803, 22);
            this.panel11.TabIndex = 238;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.lbldtcreate);
            this.panel12.Controls.Add(this.lblUName);
            this.panel12.Controls.Add(this.label71);
            this.panel12.Controls.Add(this.label72);
            this.panel12.Controls.Add(this.label73);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel12.ForeColor = System.Drawing.Color.Blue;
            this.panel12.Location = new System.Drawing.Point(137, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(309, 22);
            this.panel12.TabIndex = 256;
            // 
            // lbldtcreate
            // 
            this.lbldtcreate.AutoSize = true;
            this.lbldtcreate.BackColor = System.Drawing.Color.Transparent;
            this.lbldtcreate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbldtcreate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbldtcreate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbldtcreate.Location = new System.Drawing.Point(77, 0);
            this.lbldtcreate.Name = "lbldtcreate";
            this.lbldtcreate.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lbldtcreate.Size = new System.Drawing.Size(0, 18);
            this.lbldtcreate.TabIndex = 259;
            this.lbldtcreate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUName
            // 
            this.lblUName.AutoSize = true;
            this.lblUName.BackColor = System.Drawing.Color.Transparent;
            this.lblUName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblUName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblUName.Location = new System.Drawing.Point(77, 0);
            this.lblUName.Name = "lblUName";
            this.lblUName.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblUName.Size = new System.Drawing.Size(0, 18);
            this.lblUName.TabIndex = 258;
            this.lblUName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.BackColor = System.Drawing.Color.Transparent;
            this.label71.Dock = System.Windows.Forms.DockStyle.Left;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label71.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Location = new System.Drawing.Point(77, 0);
            this.label71.Name = "label71";
            this.label71.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label71.Size = new System.Drawing.Size(0, 18);
            this.label71.TabIndex = 255;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.BackColor = System.Drawing.Color.Transparent;
            this.label72.Dock = System.Windows.Forms.DockStyle.Left;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label72.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Location = new System.Drawing.Point(77, 0);
            this.label72.Name = "label72";
            this.label72.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label72.Size = new System.Drawing.Size(0, 18);
            this.label72.TabIndex = 254;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.BackColor = System.Drawing.Color.Transparent;
            this.label73.Dock = System.Windows.Forms.DockStyle.Left;
            this.label73.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label73.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label73.Location = new System.Drawing.Point(0, 0);
            this.label73.Name = "label73";
            this.label73.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label73.Size = new System.Drawing.Size(77, 18);
            this.label73.TabIndex = 257;
            this.label73.Text = "Generated - ";
            this.label73.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbldtstdate
            // 
            this.lbldtstdate.AutoSize = true;
            this.lbldtstdate.BackColor = System.Drawing.Color.Transparent;
            this.lbldtstdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbldtstdate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbldtstdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbldtstdate.Location = new System.Drawing.Point(137, 0);
            this.lbldtstdate.Name = "lbldtstdate";
            this.lbldtstdate.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lbldtstdate.Size = new System.Drawing.Size(0, 18);
            this.lbldtstdate.TabIndex = 251;
            this.lbldtstdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblptName
            // 
            this.lblptName.AutoSize = true;
            this.lblptName.BackColor = System.Drawing.Color.Transparent;
            this.lblptName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblptName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblptName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblptName.Location = new System.Drawing.Point(137, 0);
            this.lblptName.Name = "lblptName";
            this.lblptName.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblptName.Size = new System.Drawing.Size(0, 18);
            this.lblptName.TabIndex = 249;
            // 
            // label77
            // 
            this.label77.BackColor = System.Drawing.Color.Transparent;
            this.label77.Dock = System.Windows.Forms.DockStyle.Left;
            this.label77.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label77.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label77.Location = new System.Drawing.Point(0, 0);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(137, 22);
            this.label77.TabIndex = 250;
            this.label77.Text = "Last Statement For";
            this.label77.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.BackColor = System.Drawing.Color.Transparent;
            this.label74.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label74.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label74.Location = new System.Drawing.Point(137, 0);
            this.label74.Name = "label74";
            this.label74.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label74.Size = new System.Drawing.Size(0, 18);
            this.label74.TabIndex = 252;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.groupBox9);
            this.groupBox8.Controls.Add(this.label79);
            this.groupBox8.Controls.Add(this.textBox5);
            this.groupBox8.Controls.Add(this.groupBox10);
            this.groupBox8.Controls.Add(this.label80);
            this.groupBox8.Controls.Add(this.numericUpDown2);
            this.groupBox8.Controls.Add(this.textBox8);
            this.groupBox8.Controls.Add(this.panel13);
            this.groupBox8.Controls.Add(this.groupBox11);
            this.groupBox8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox8.Location = new System.Drawing.Point(1065, 87);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(22, 34);
            this.groupBox8.TabIndex = 230;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Due Date ";
            this.groupBox8.Visible = false;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label78);
            this.groupBox9.Controls.Add(this.dateTimePicker2);
            this.groupBox9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox9.Location = new System.Drawing.Point(13, 80);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(303, 26);
            this.groupBox9.TabIndex = 229;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Balance Greater Than";
            this.groupBox9.Visible = false;
            // 
            // label78
            // 
            this.label78.BackColor = System.Drawing.Color.Transparent;
            this.label78.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label78.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label78.Location = new System.Drawing.Point(7, 18);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(72, 14);
            this.label78.TabIndex = 230;
            this.label78.Text = "Start Date :";
            this.label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label78.Visible = false;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dateTimePicker2.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker2.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dateTimePicker2.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dateTimePicker2.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dateTimePicker2.Checked = false;
            this.dateTimePicker2.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(83, 14);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowCheckBox = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(108, 22);
            this.dateTimePicker2.TabIndex = 231;
            this.dateTimePicker2.Visible = false;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.BackColor = System.Drawing.Color.Transparent;
            this.label79.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label79.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label79.Location = new System.Drawing.Point(104, 51);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(42, 14);
            this.label79.TabIndex = 228;
            this.label79.Text = "From :";
            this.label79.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label79.Visible = false;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.textBox5.Location = new System.Drawing.Point(66, 48);
            this.textBox5.MaxLength = 12;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(10, 22);
            this.textBox5.TabIndex = 227;
            this.textBox5.Text = "0.00";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox5.Visible = false;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.textBox6);
            this.groupBox10.Controls.Add(this.textBox7);
            this.groupBox10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox10.Location = new System.Drawing.Point(173, 43);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(27, 22);
            this.groupBox10.TabIndex = 231;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Patient Name ";
            this.groupBox10.Visible = false;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.textBox6.Location = new System.Drawing.Point(131, 19);
            this.textBox6.MaxLength = 12;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(31, 22);
            this.textBox6.TabIndex = 244;
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox6.Visible = false;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.textBox7.Location = new System.Drawing.Point(58, 19);
            this.textBox7.MaxLength = 12;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(29, 22);
            this.textBox7.TabIndex = 243;
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox7.Visible = false;
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.BackColor = System.Drawing.Color.Transparent;
            this.label80.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label80.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label80.Location = new System.Drawing.Point(12, 21);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(67, 14);
            this.label80.TabIndex = 228;
            this.label80.Text = "Due Date :";
            this.label80.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown2.Location = new System.Drawing.Point(84, 17);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(52, 22);
            this.numericUpDown2.TabIndex = 229;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.textBox8.Location = new System.Drawing.Point(88, 48);
            this.textBox8.MaxLength = 12;
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(10, 22);
            this.textBox8.TabIndex = 242;
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox8.Visible = false;
            // 
            // panel13
            // 
            this.panel13.Location = new System.Drawing.Point(324, 61);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(27, 43);
            this.panel13.TabIndex = 237;
            this.panel13.Visible = false;
            // 
            // groupBox11
            // 
            this.groupBox11.Location = new System.Drawing.Point(384, 44);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(195, 40);
            this.groupBox11.TabIndex = 2;
            this.groupBox11.TabStop = false;
            this.groupBox11.Visible = false;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.label58);
            this.panel7.Controls.Add(this.label59);
            this.panel7.Controls.Add(this.label60);
            this.panel7.Controls.Add(this.label61);
            this.panel7.Controls.Add(this.groupBox3);
            this.panel7.Location = new System.Drawing.Point(27, 61);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(803, 22);
            this.panel7.TabIndex = 238;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label54);
            this.panel8.Controls.Add(this.label55);
            this.panel8.Controls.Add(this.label56);
            this.panel8.Controls.Add(this.label57);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.ForeColor = System.Drawing.Color.Blue;
            this.panel8.Location = new System.Drawing.Point(229, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(243, 22);
            this.panel8.TabIndex = 256;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.BackColor = System.Drawing.Color.Transparent;
            this.label54.Dock = System.Windows.Forms.DockStyle.Left;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label54.ForeColor = System.Drawing.Color.Blue;
            this.label54.Location = new System.Drawing.Point(12, 0);
            this.label54.Name = "label54";
            this.label54.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label54.Size = new System.Drawing.Size(12, 18);
            this.label54.TabIndex = 258;
            this.label54.Text = "]";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.BackColor = System.Drawing.Color.Transparent;
            this.label55.Dock = System.Windows.Forms.DockStyle.Left;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label55.ForeColor = System.Drawing.Color.Blue;
            this.label55.Location = new System.Drawing.Point(12, 0);
            this.label55.Name = "label55";
            this.label55.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label55.Size = new System.Drawing.Size(0, 18);
            this.label55.TabIndex = 255;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.BackColor = System.Drawing.Color.Transparent;
            this.label56.Dock = System.Windows.Forms.DockStyle.Left;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label56.ForeColor = System.Drawing.Color.Blue;
            this.label56.Location = new System.Drawing.Point(12, 0);
            this.label56.Name = "label56";
            this.label56.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label56.Size = new System.Drawing.Size(0, 18);
            this.label56.TabIndex = 254;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.Dock = System.Windows.Forms.DockStyle.Left;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label57.ForeColor = System.Drawing.Color.Blue;
            this.label57.Location = new System.Drawing.Point(0, 0);
            this.label57.Name = "label57";
            this.label57.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label57.Size = new System.Drawing.Size(12, 18);
            this.label57.TabIndex = 257;
            this.label57.Text = "[";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.BackColor = System.Drawing.Color.Transparent;
            this.label58.Dock = System.Windows.Forms.DockStyle.Left;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label58.ForeColor = System.Drawing.Color.Blue;
            this.label58.Location = new System.Drawing.Point(229, 0);
            this.label58.Name = "label58";
            this.label58.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label58.Size = new System.Drawing.Size(0, 18);
            this.label58.TabIndex = 252;
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label59.ForeColor = System.Drawing.Color.Blue;
            this.label59.Location = new System.Drawing.Point(137, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(92, 22);
            this.label59.TabIndex = 251;
            this.label59.Text = "Generated On";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.BackColor = System.Drawing.Color.Transparent;
            this.label60.Dock = System.Windows.Forms.DockStyle.Left;
            this.label60.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label60.ForeColor = System.Drawing.Color.Blue;
            this.label60.Location = new System.Drawing.Point(137, 0);
            this.label60.Name = "label60";
            this.label60.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label60.Size = new System.Drawing.Size(0, 18);
            this.label60.TabIndex = 249;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.Transparent;
            this.label61.Dock = System.Windows.Forms.DockStyle.Left;
            this.label61.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label61.ForeColor = System.Drawing.Color.Blue;
            this.label61.Location = new System.Drawing.Point(0, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(137, 22);
            this.label61.TabIndex = 250;
            this.label61.Text = "Last Statement For";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.label63);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.label64);
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.textBox4);
            this.groupBox3.Controls.Add(this.panel9);
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox3.Location = new System.Drawing.Point(1065, 87);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(22, 34);
            this.groupBox3.TabIndex = 230;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Due Date ";
            this.groupBox3.Visible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label62);
            this.groupBox5.Controls.Add(this.dateTimePicker1);
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox5.Location = new System.Drawing.Point(13, 80);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(303, 26);
            this.groupBox5.TabIndex = 229;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Balance Greater Than";
            this.groupBox5.Visible = false;
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label62.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Location = new System.Drawing.Point(7, 18);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(72, 14);
            this.label62.TabIndex = 230;
            this.label62.Text = "Start Date :";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label62.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dateTimePicker1.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker1.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dateTimePicker1.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dateTimePicker1.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(83, 14);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(108, 22);
            this.dateTimePicker1.TabIndex = 231;
            this.dateTimePicker1.Visible = false;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.BackColor = System.Drawing.Color.Transparent;
            this.label63.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label63.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Location = new System.Drawing.Point(104, 51);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(42, 14);
            this.label63.TabIndex = 228;
            this.label63.Text = "From :";
            this.label63.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label63.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.textBox1.Location = new System.Drawing.Point(66, 48);
            this.textBox1.MaxLength = 12;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(10, 22);
            this.textBox1.TabIndex = 227;
            this.textBox1.Text = "0.00";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox1.Visible = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBox2);
            this.groupBox6.Controls.Add(this.textBox3);
            this.groupBox6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox6.Location = new System.Drawing.Point(173, 43);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(27, 22);
            this.groupBox6.TabIndex = 231;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Patient Name ";
            this.groupBox6.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.textBox2.Location = new System.Drawing.Point(131, 19);
            this.textBox2.MaxLength = 12;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(31, 22);
            this.textBox2.TabIndex = 244;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox2.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.textBox3.Location = new System.Drawing.Point(58, 19);
            this.textBox3.MaxLength = 12;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(29, 22);
            this.textBox3.TabIndex = 243;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox3.Visible = false;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.BackColor = System.Drawing.Color.Transparent;
            this.label64.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label64.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label64.Location = new System.Drawing.Point(12, 21);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(67, 14);
            this.label64.TabIndex = 228;
            this.label64.Text = "Due Date :";
            this.label64.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(84, 17);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(52, 22);
            this.numericUpDown1.TabIndex = 229;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.textBox4.Location = new System.Drawing.Point(88, 48);
            this.textBox4.MaxLength = 12;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(10, 22);
            this.textBox4.TabIndex = 242;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox4.Visible = false;
            // 
            // panel9
            // 
            this.panel9.Location = new System.Drawing.Point(324, 61);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(27, 43);
            this.panel9.TabIndex = 237;
            this.panel9.Visible = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Location = new System.Drawing.Point(384, 44);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(195, 40);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Visible = false;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(1, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1109, 1);
            this.label10.TabIndex = 235;
            this.label10.Text = "label10";
            // 
            // pnlDuedate
            // 
            this.pnlDuedate.Controls.Add(this.gbSelectFollowupType);
            this.pnlDuedate.Location = new System.Drawing.Point(1050, 103);
            this.pnlDuedate.Name = "pnlDuedate";
            this.pnlDuedate.Size = new System.Drawing.Size(54, 21);
            this.pnlDuedate.TabIndex = 229;
            // 
            // gbSelectFollowupType
            // 
            this.gbSelectFollowupType.Controls.Add(this.label1);
            this.gbSelectFollowupType.Controls.Add(this.numDuration);
            this.gbSelectFollowupType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSelectFollowupType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gbSelectFollowupType.Location = new System.Drawing.Point(3, 8);
            this.gbSelectFollowupType.Name = "gbSelectFollowupType";
            this.gbSelectFollowupType.Size = new System.Drawing.Size(404, 57);
            this.gbSelectFollowupType.TabIndex = 230;
            this.gbSelectFollowupType.TabStop = false;
            this.gbSelectFollowupType.Text = "Due Date Type";
            this.gbSelectFollowupType.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(14, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 228;
            this.label1.Text = "Due Date :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numDuration
            // 
            this.numDuration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDuration.Location = new System.Drawing.Point(84, 22);
            this.numDuration.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(55, 22);
            this.numDuration.TabIndex = 229;
            this.numDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(1, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1109, 1);
            this.label12.TabIndex = 234;
            this.label12.Text = "label12";
            // 
            // pnlNoFiliterPatient
            // 
            this.pnlNoFiliterPatient.Controls.Add(this.btnBrowseAcount);
            this.pnlNoFiliterPatient.Controls.Add(this.lblAccount);
            this.pnlNoFiliterPatient.Controls.Add(this.cmbAccounts);
            this.pnlNoFiliterPatient.Controls.Add(this.btnBrowsePatient);
            this.pnlNoFiliterPatient.Controls.Add(this.lblPatient);
            this.pnlNoFiliterPatient.Controls.Add(this.cmbPatients);
            this.pnlNoFiliterPatient.Controls.Add(this.btnClearPatient);
            this.pnlNoFiliterPatient.Location = new System.Drawing.Point(3, 2);
            this.pnlNoFiliterPatient.Name = "pnlNoFiliterPatient";
            this.pnlNoFiliterPatient.Size = new System.Drawing.Size(1107, 27);
            this.pnlNoFiliterPatient.TabIndex = 218;
            // 
            // btnBrowseAcount
            // 
            this.btnBrowseAcount.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseAcount.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.btnBrowseAcount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseAcount.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseAcount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseAcount.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseAcount.Image")));
            this.btnBrowseAcount.Location = new System.Drawing.Point(629, 2);
            this.btnBrowseAcount.Name = "btnBrowseAcount";
            this.btnBrowseAcount.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseAcount.TabIndex = 200;
            this.toolTip1.SetToolTip(this.btnBrowseAcount, "Browse Accounts");
            this.btnBrowseAcount.UseVisualStyleBackColor = false;
            this.btnBrowseAcount.Visible = false;
            this.btnBrowseAcount.Click += new System.EventHandler(this.btnBrowseAcount_Click);
            // 
            // lblAccount
            // 
            this.lblAccount.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAccount.Location = new System.Drawing.Point(373, 6);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(62, 14);
            this.lblAccount.TabIndex = 199;
            this.lblAccount.Text = "Account :";
            this.lblAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAccount.Visible = false;
            // 
            // cmbAccounts
            // 
            this.cmbAccounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccounts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAccounts.FormattingEnabled = true;
            this.cmbAccounts.Location = new System.Drawing.Point(437, 2);
            this.cmbAccounts.Name = "cmbAccounts";
            this.cmbAccounts.Size = new System.Drawing.Size(187, 22);
            this.cmbAccounts.TabIndex = 198;
            this.cmbAccounts.Visible = false;
            this.cmbAccounts.SelectedIndexChanged += new System.EventHandler(this.cmbAccounts_SelectedIndexChanged);
            this.cmbAccounts.MouseEnter += new System.EventHandler(this.cmbAccounts_MouseEnter);
            // 
            // btnBrowsePatient
            // 
            this.btnBrowsePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePatient.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.btnBrowsePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.Image")));
            this.btnBrowsePatient.Location = new System.Drawing.Point(316, 2);
            this.btnBrowsePatient.Name = "btnBrowsePatient";
            this.btnBrowsePatient.Size = new System.Drawing.Size(22, 22);
            this.btnBrowsePatient.TabIndex = 194;
            this.toolTip1.SetToolTip(this.btnBrowsePatient, "Browse Patients");
            this.btnBrowsePatient.UseVisualStyleBackColor = false;
            this.btnBrowsePatient.Click += new System.EventHandler(this.btnBrowsePatient_Click);
            // 
            // btnClearPatient
            // 
            this.btnClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatient.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.Image")));
            this.btnClearPatient.Location = new System.Drawing.Point(343, 2);
            this.btnClearPatient.Name = "btnClearPatient";
            this.btnClearPatient.Size = new System.Drawing.Size(22, 22);
            this.btnClearPatient.TabIndex = 195;
            this.toolTip1.SetToolTip(this.btnClearPatient, "Clear Patient");
            this.btnClearPatient.UseVisualStyleBackColor = false;
            this.btnClearPatient.Visible = false;
            this.btnClearPatient.Click += new System.EventHandler(this.btnClearPatient_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 59);
            this.label14.TabIndex = 232;
            this.label14.Text = "label14";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(1110, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 59);
            this.label13.TabIndex = 233;
            this.label13.Text = "label13";
            // 
            // pnlCDuedate
            // 
            this.pnlCDuedate.Location = new System.Drawing.Point(615, 57);
            this.pnlCDuedate.Name = "pnlCDuedate";
            this.pnlCDuedate.Size = new System.Drawing.Size(418, 51);
            this.pnlCDuedate.TabIndex = 236;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(52, 90);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(108, 14);
            this.label25.TabIndex = 234;
            this.label25.Text = "Transaction Date :";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label25.Visible = false;
            // 
            // cmbCriteriaTransactionDate
            // 
            this.cmbCriteriaTransactionDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriteriaTransactionDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCriteriaTransactionDate.FormattingEnabled = true;
            this.cmbCriteriaTransactionDate.Location = new System.Drawing.Point(164, 86);
            this.cmbCriteriaTransactionDate.Name = "cmbCriteriaTransactionDate";
            this.cmbCriteriaTransactionDate.Size = new System.Drawing.Size(108, 22);
            this.cmbCriteriaTransactionDate.TabIndex = 235;
            this.cmbCriteriaTransactionDate.Visible = false;
            // 
            // pnlFilteredPatList
            // 
            this.pnlFilteredPatList.Controls.Add(this.label15);
            this.pnlFilteredPatList.Controls.Add(this.c1PatientList);
            this.pnlFilteredPatList.Controls.Add(this.label16);
            this.pnlFilteredPatList.Controls.Add(this.label17);
            this.pnlFilteredPatList.Controls.Add(this.label18);
            this.pnlFilteredPatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilteredPatList.Location = new System.Drawing.Point(0, 144);
            this.pnlFilteredPatList.Name = "pnlFilteredPatList";
            this.pnlFilteredPatList.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlFilteredPatList.Size = new System.Drawing.Size(1117, 611);
            this.pnlFilteredPatList.TabIndex = 218;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(4, 607);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1109, 1);
            this.label15.TabIndex = 32;
            this.label15.Text = "label15";
            // 
            // c1PatientList
            // 
            this.c1PatientList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PatientList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientList.ColumnInfo = "1,0,0,0,0,95,Columns:0{Style:\"TextAlign:GeneralCenter;ImageAlign:CenterCenter;\";}" +
    "\t";
            this.c1PatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1PatientList.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1PatientList.Location = new System.Drawing.Point(4, 1);
            this.c1PatientList.Name = "c1PatientList";
            this.c1PatientList.Padding = new System.Windows.Forms.Padding(3);
            this.c1PatientList.Rows.Count = 1;
            this.c1PatientList.Rows.DefaultSize = 19;
            this.c1PatientList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1PatientList.ShowCellLabels = true;
            this.c1PatientList.Size = new System.Drawing.Size(1109, 607);
            this.c1PatientList.StyleInfo = resources.GetString("c1PatientList.StyleInfo");
            this.c1PatientList.TabIndex = 28;
            this.c1PatientList.Tag = "ClosePeriod";
            this.c1PatientList.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1PatientList_AfterEdit);
            this.c1PatientList.Click += new System.EventHandler(this.c1PatientList_Click);
            this.c1PatientList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1PatientList_MouseDoubleClick);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1109, 1);
            this.label16.TabIndex = 31;
            this.label16.Text = "label16";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Location = new System.Drawing.Point(1113, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 608);
            this.label17.TabIndex = 30;
            this.label17.Text = "label17";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Location = new System.Drawing.Point(3, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 608);
            this.label18.TabIndex = 29;
            this.label18.Text = "label18";
            // 
            // pnlc1PatientListHeader
            // 
            this.pnlc1PatientListHeader.Controls.Add(this.pnlc1PatientList);
            this.pnlc1PatientListHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlc1PatientListHeader.Location = new System.Drawing.Point(0, 118);
            this.pnlc1PatientListHeader.Name = "pnlc1PatientListHeader";
            this.pnlc1PatientListHeader.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlc1PatientListHeader.Size = new System.Drawing.Size(1117, 26);
            this.pnlc1PatientListHeader.TabIndex = 83;
            // 
            // pnlc1PatientList
            // 
            this.pnlc1PatientList.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.pnlc1PatientList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlc1PatientList.Controls.Add(this.numQueueClaimCount);
            this.pnlc1PatientList.Controls.Add(this.label31);
            this.pnlc1PatientList.Controls.Add(this.btnDown);
            this.pnlc1PatientList.Controls.Add(this.btnUp);
            this.pnlc1PatientList.Controls.Add(this.label30);
            this.pnlc1PatientList.Controls.Add(this.label29);
            this.pnlc1PatientList.Controls.Add(this.label28);
            this.pnlc1PatientList.Controls.Add(this.label23);
            this.pnlc1PatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlc1PatientList.Location = new System.Drawing.Point(3, 0);
            this.pnlc1PatientList.Name = "pnlc1PatientList";
            this.pnlc1PatientList.Size = new System.Drawing.Size(1111, 23);
            this.pnlc1PatientList.TabIndex = 1;
            // 
            // numQueueClaimCount
            // 
            this.numQueueClaimCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numQueueClaimCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.numQueueClaimCount.Location = new System.Drawing.Point(1010, 1);
            this.numQueueClaimCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numQueueClaimCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQueueClaimCount.Name = "numQueueClaimCount";
            this.numQueueClaimCount.Size = new System.Drawing.Size(56, 21);
            this.numQueueClaimCount.TabIndex = 238;
            this.numQueueClaimCount.Tag = "Queue";
            this.numQueueClaimCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numQueueClaimCount.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(1, 1);
            this.label31.Name = "label31";
            this.label31.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.label31.Size = new System.Drawing.Size(105, 15);
            this.label31.TabIndex = 236;
            this.label31.Text = "Batch Patients :";
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(1066, 1);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(22, 21);
            this.btnDown.TabIndex = 217;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            this.btnDown.MouseLeave += new System.EventHandler(this.btnDown_MouseLeave);
            this.btnDown.MouseHover += new System.EventHandler(this.btnDown_MouseHover);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Location = new System.Drawing.Point(1088, 1);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(22, 21);
            this.btnUp.TabIndex = 218;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            this.btnUp.MouseLeave += new System.EventHandler(this.btnUp_MouseLeave);
            this.btnUp.MouseHover += new System.EventHandler(this.btnUp_MouseHover);
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Right;
            this.label30.Location = new System.Drawing.Point(1110, 1);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 21);
            this.label30.TabIndex = 235;
            this.label30.Text = "label30";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Left;
            this.label29.Location = new System.Drawing.Point(0, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 21);
            this.label29.TabIndex = 234;
            this.label29.Text = "label29";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1111, 1);
            this.label28.TabIndex = 219;
            this.label28.Text = "label2";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label23.Location = new System.Drawing.Point(0, 22);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1111, 1);
            this.label23.TabIndex = 12;
            this.label23.Text = "label2";
            // 
            // pnlProgressBar
            // 
            this.pnlProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlProgressBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProgressBar.Controls.Add(this.panel3);
            this.pnlProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProgressBar.Location = new System.Drawing.Point(0, 755);
            this.pnlProgressBar.Name = "pnlProgressBar";
            this.pnlProgressBar.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlProgressBar.Size = new System.Drawing.Size(1117, 25);
            this.pnlProgressBar.TabIndex = 258;
            this.pnlProgressBar.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.lblFile);
            this.panel3.Controls.Add(this.lblFileCounter);
            this.panel3.Controls.Add(this.prgFileGeneration);
            this.panel3.Controls.Add(this.label38);
            this.panel3.Controls.Add(this.label32);
            this.panel3.Controls.Add(this.label37);
            this.panel3.Controls.Add(this.label33);
            this.panel3.Controls.Add(this.label34);
            this.panel3.Controls.Add(this.label36);
            this.panel3.Controls.Add(this.label35);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1111, 22);
            this.panel3.TabIndex = 32;
            // 
            // lblFile
            // 
            this.lblFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFile.Location = new System.Drawing.Point(1, 3);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(186, 16);
            this.lblFile.TabIndex = 1;
            this.lblFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFileCounter
            // 
            this.lblFileCounter.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblFileCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileCounter.Location = new System.Drawing.Point(101, 3);
            this.lblFileCounter.Name = "lblFileCounter";
            this.lblFileCounter.Size = new System.Drawing.Size(117, 16);
            this.lblFileCounter.TabIndex = 24;
            this.lblFileCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // prgFileGeneration
            // 
            this.prgFileGeneration.Dock = System.Windows.Forms.DockStyle.Right;
            this.prgFileGeneration.Location = new System.Drawing.Point(218, 3);
            this.prgFileGeneration.Name = "prgFileGeneration";
            this.prgFileGeneration.Size = new System.Drawing.Size(890, 16);
            this.prgFileGeneration.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgFileGeneration.TabIndex = 0;
            // 
            // label38
            // 
            this.label38.Dock = System.Windows.Forms.DockStyle.Right;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label38.Location = new System.Drawing.Point(1108, 3);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(2, 16);
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
            this.label32.Size = new System.Drawing.Size(1109, 2);
            this.label32.TabIndex = 23;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Top;
            this.label37.Location = new System.Drawing.Point(1, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1109, 1);
            this.label37.TabIndex = 30;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label33.Location = new System.Drawing.Point(1, 19);
            this.label33.Name = "label33";
            this.label33.Padding = new System.Windows.Forms.Padding(3);
            this.label33.Size = new System.Drawing.Size(1109, 2);
            this.label33.TabIndex = 29;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Location = new System.Drawing.Point(1, 21);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1109, 1);
            this.label34.TabIndex = 26;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 22);
            this.label36.TabIndex = 27;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Right;
            this.label35.Location = new System.Drawing.Point(1110, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 22);
            this.label35.TabIndex = 28;
            // 
            // pnlPleasewait
            // 
            this.pnlPleasewait.Controls.Add(this.label45);
            this.pnlPleasewait.Controls.Add(this.label44);
            this.pnlPleasewait.Controls.Add(this.label43);
            this.pnlPleasewait.Controls.Add(this.label42);
            this.pnlPleasewait.Controls.Add(this.label41);
            this.pnlPleasewait.Controls.Add(this.cmbCriteriaTransactionDate);
            this.pnlPleasewait.Controls.Add(this.label25);
            this.pnlPleasewait.Controls.Add(this.pnlCDuedate);
            this.pnlPleasewait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPleasewait.Location = new System.Drawing.Point(0, 144);
            this.pnlPleasewait.Name = "pnlPleasewait";
            this.pnlPleasewait.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlPleasewait.Size = new System.Drawing.Size(1117, 611);
            this.pnlPleasewait.TabIndex = 33;
            this.pnlPleasewait.Visible = false;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.White;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.Font = new System.Drawing.Font("Baskerville Old Face", 48F);
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Location = new System.Drawing.Point(4, 1);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1109, 606);
            this.label45.TabIndex = 238;
            this.label45.Text = "Please wait...";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Left;
            this.label44.Location = new System.Drawing.Point(3, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 606);
            this.label44.TabIndex = 36;
            this.label44.Text = "label44";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Right;
            this.label43.Location = new System.Drawing.Point(1113, 1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 606);
            this.label43.TabIndex = 35;
            this.label43.Text = "label43";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Location = new System.Drawing.Point(3, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1111, 1);
            this.label42.TabIndex = 34;
            this.label42.Text = "label42";
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label41.Location = new System.Drawing.Point(3, 607);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1111, 1);
            this.label41.TabIndex = 33;
            this.label41.Text = "label41";
            // 
            // frmRpt_Revised_PatientStatement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1117, 780);
            this.Controls.Add(this.pnlPleasewait);
            this.Controls.Add(this.pnlFilteredPatList);
            this.Controls.Add(this.pnlcrvReportViewer);
            this.Controls.Add(this.pnlc1PatientListHeader);
            this.Controls.Add(this.pnlMainStatement);
            this.Controls.Add(this.pnlToolStrip);
            this.Controls.Add(this.pnlProgressBar);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "frmRpt_Revised_PatientStatement";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Statement";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRpt_Revised_PatientStatement_FormClosed);
            this.Load += new System.EventHandler(this.frmRpt_Revised_PatientStatement_Load);
            this.Shown += new System.EventHandler(this.frmRpt_Revised_PatientStatement_Shown);
            this.pnlcrvReportViewer.ResumeLayout(false);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.grpPatientNameCriteria.ResumeLayout(false);
            this.grpPatientNameCriteria.PerformLayout();
            this.pnlMainStatement.ResumeLayout(false);
            this.pnlCriteria.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlSelectSet.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlLastBatch.ResumeLayout(false);
            this.pnlLastBatch.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.grpCDueDate.ResumeLayout(false);
            this.grpCDueDate.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numCriteriaDuration)).EndInit();
            this.pnlCurrentBatch.ResumeLayout(false);
            this.pnlCurrentBatch.PerformLayout();
            this.pnlBusinessCenter.ResumeLayout(false);
            this.pnlPatientList.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.pnlDuedate.ResumeLayout(false);
            this.gbSelectFollowupType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            this.pnlNoFiliterPatient.ResumeLayout(false);
            this.pnlFilteredPatList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).EndInit();
            this.pnlc1PatientListHeader.ResumeLayout(false);
            this.pnlc1PatientList.ResumeLayout(false);
            this.pnlc1PatientList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQueueClaimCount)).EndInit();
            this.pnlProgressBar.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlPleasewait.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlcrvReportViewer;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReportViewer;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.Label lblPatient;
        internal System.Windows.Forms.Button btnBrowsePatient;
        internal System.Windows.Forms.Button btnClearPatient;
        private System.Windows.Forms.GroupBox grpPayType;
        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_ViewStatement;
        private System.Windows.Forms.ToolStripButton tsb_btnExportReport;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.ComboBox cmbSettings;
        internal System.Windows.Forms.Label lblSelectSettings;
        private System.Windows.Forms.Panel pnlMainStatement;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel pnlSelectSet;
        private System.Windows.Forms.Panel pnlFilteredPatList;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientList;
        private System.Windows.Forms.Panel pnlNoFiliterPatient;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlPatientList;
        private System.Windows.Forms.Panel pnlCriteria;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        internal System.Windows.Forms.Panel pnlc1PatientListHeader;
        private System.Windows.Forms.Panel pnlc1PatientList;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel pnlDuedate;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cmbCriteriaTransactionDate;
        internal System.Windows.Forms.Button btnUp;
        internal System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        internal System.Windows.Forms.ToolStripButton tsb_btnIndividual;
        internal System.Windows.Forms.ToolStripButton tsb_btnBatch;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.GroupBox gbSelectFollowupType;
        private System.Windows.Forms.Panel pnlCDuedate;
        internal System.Windows.Forms.ToolStripButton tsb_btnShowList;
        private System.Windows.Forms.Panel pnlProgressBar;
        private System.Windows.Forms.Label lblFileCounter;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.ProgressBar prgFileGeneration;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel pnlPleasewait;
        internal System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Panel pnlLastBatch;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label lblmaxCreateDate;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lbldtStatementDate;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label lblNameTo;
        private System.Windows.Forms.Label lblNameFrom;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label lblWaitDays;
        internal System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblDueAmt;
        private System.Windows.Forms.GroupBox grpPatientNameCriteria;
        private System.Windows.Forms.TextBox cmbNameTo;
        private System.Windows.Forms.TextBox cmbNameFrom;
        private System.Windows.Forms.TextBox cmbWaitDays;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDueAmt;
        private System.Windows.Forms.GroupBox grpCDueDate;
        internal System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown numCriteriaDuration;
        private System.Windows.Forms.Panel pnlPatientNameCriteria;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtCriteriaEndDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DateTimePicker dtCriteriaStartDate;
        private System.Windows.Forms.Label lblTotaldue;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.ToolStripDropDownButton tsb_Send;
        private System.Windows.Forms.ToolStripMenuItem tsb_Send_Electronic;
        private System.Windows.Forms.ToolStripMenuItem tsb_Print;
        private System.Windows.Forms.Panel pnlCurrentBatch;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        internal System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        internal System.Windows.Forms.Label label64;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label lblPatientDue;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label lblUName;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label lbldtstdate;
        private System.Windows.Forms.Label lblptName;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        internal System.Windows.Forms.Label label79;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        internal System.Windows.Forms.Label label80;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label lbldtcreate;
        internal System.Windows.Forms.ToolStripButton tsb_GenerateBatch;
        private System.Windows.Forms.NumericUpDown numQueueClaimCount;
        private System.Windows.Forms.ComboBox cmbAccounts;
        private System.Windows.Forms.Label lblAccount;
        internal System.Windows.Forms.Button btnBrowseAcount;
        internal System.Windows.Forms.ToolStripButton tsb_PatAcctAccount;
        private System.Windows.Forms.Panel pnlBusinessCenter;
        internal System.Windows.Forms.ComboBox cmbBusinessCenter;
        internal System.Windows.Forms.Label lblBusinessCenter;
        private System.Windows.Forms.Button btnClearBusinessCenter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}