namespace gloEmdeonInterface.Forms
{
    partial class frmViewSpirometryTests
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
                    if (dtpTodate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpTodate);
                        }
                        catch
                        {
                        }
                        dtpTodate.Dispose();
                        dtpTodate = null;
                    }
                }
                catch
                {
                }


                try
                {
                    if (dtpFromDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpFromDate);
                        }
                        catch
                        {
                        }
                        dtpFromDate.Dispose();
                        dtpFromDate = null;
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
                    if (gloUC_PatientStrip1 != null)
                    {
                        gloUC_PatientStrip1.Dispose();
                        gloUC_PatientStrip1 = null;
                    }
                }
                catch
                {
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewSpirometryTests));
            this.ts_LabMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtnNew = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnPost = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnReview = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnView = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnCompare = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnStatus = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnSettings = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnCalibrate = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnConfigureRace = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnclose = new System.Windows.Forms.ToolStripButton();
            this.pnlc1Spiro = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.c1Spiro = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSelected = new System.Windows.Forms.Label();
            this.BtnNext = new System.Windows.Forms.Button();
            this.BtnFirst = new System.Windows.Forms.Button();
            this.BtnLast = new System.Windows.Forms.Button();
            this.BtnPrev = new System.Windows.Forms.Button();
            this.cmbPageSize = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.ChkUseDateRange = new System.Windows.Forms.CheckBox();
            this.cmbTestType = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.dtpTodate = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.PnlInterpretation = new System.Windows.Forms.Panel();
            this.txtInterpreatation = new System.Windows.Forms.RichTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ts_LabMain.SuspendLayout();
            this.pnlc1Spiro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Spiro)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.PnlInterpretation.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_LabMain
            // 
            this.ts_LabMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_LabMain.BackgroundImage")));
            this.ts_LabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_LabMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_LabMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_LabMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtnNew,
            this.tlbbtnPost,
            this.tlbbtnReview,
            this.tlbbtnView,
            this.tlbbtnCompare,
            this.tlbbtnStatus,
            this.tlbbtnPrint,
            this.tlbbtnSettings,
            this.tlbbtnCalibrate,
            this.tlbbtnConfigureRace,
            this.tlbbtnclose});
            this.ts_LabMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_LabMain.Location = new System.Drawing.Point(0, 0);
            this.ts_LabMain.Name = "ts_LabMain";
            this.ts_LabMain.Size = new System.Drawing.Size(1192, 53);
            this.ts_LabMain.TabIndex = 39;
            this.ts_LabMain.Text = "toolStrip1";
            this.ts_LabMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_LabMain_ItemClicked);
            // 
            // tlbbtnNew
            // 
            this.tlbbtnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnNew.Image")));
            this.tlbbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnNew.Name = "tlbbtnNew";
            this.tlbbtnNew.Size = new System.Drawing.Size(41, 50);
            this.tlbbtnNew.Tag = "NewTest";
            this.tlbbtnNew.Text = " &New";
            this.tlbbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnNew.ToolTipText = "Perform New Test";
            // 
            // tlbbtnPost
            // 
            this.tlbbtnPost.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnPost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnPost.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnPost.Image")));
            this.tlbbtnPost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnPost.Name = "tlbbtnPost";
            this.tlbbtnPost.Size = new System.Drawing.Size(39, 50);
            this.tlbbtnPost.Tag = "Post";
            this.tlbbtnPost.Text = "&Post";
            this.tlbbtnPost.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnPost.ToolTipText = "Post Test";
            // 
            // tlbbtnReview
            // 
            this.tlbbtnReview.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnReview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnReview.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnReview.Image")));
            this.tlbbtnReview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnReview.Name = "tlbbtnReview";
            this.tlbbtnReview.Size = new System.Drawing.Size(55, 50);
            this.tlbbtnReview.Tag = "Review";
            this.tlbbtnReview.Text = "&Review";
            this.tlbbtnReview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnReview.ToolTipText = "Review Test  ";
            // 
            // tlbbtnView
            // 
            this.tlbbtnView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnView.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnView.Image")));
            this.tlbbtnView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnView.Name = "tlbbtnView";
            this.tlbbtnView.Size = new System.Drawing.Size(40, 50);
            this.tlbbtnView.Tag = "View";
            this.tlbbtnView.Text = "&View";
            this.tlbbtnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnView.ToolTipText = "View Test";
            // 
            // tlbbtnCompare
            // 
            this.tlbbtnCompare.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnCompare.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnCompare.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnCompare.Image")));
            this.tlbbtnCompare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnCompare.Name = "tlbbtnCompare";
            this.tlbbtnCompare.Size = new System.Drawing.Size(65, 50);
            this.tlbbtnCompare.Tag = "Compare";
            this.tlbbtnCompare.Text = "&Compare";
            this.tlbbtnCompare.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnCompare.ToolTipText = "Compare Test";
            // 
            // tlbbtnStatus
            // 
            this.tlbbtnStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnStatus.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnStatus.Image")));
            this.tlbbtnStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnStatus.Name = "tlbbtnStatus";
            this.tlbbtnStatus.Size = new System.Drawing.Size(52, 50);
            this.tlbbtnStatus.Tag = "Status";
            this.tlbbtnStatus.Text = "&Status";
            this.tlbbtnStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnStatus.ToolTipText = "Status Of Test";
            // 
            // tlbbtnPrint
            // 
            this.tlbbtnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnPrint.Image")));
            this.tlbbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnPrint.Name = "tlbbtnPrint";
            this.tlbbtnPrint.Size = new System.Drawing.Size(41, 50);
            this.tlbbtnPrint.Tag = "Print";
            this.tlbbtnPrint.Text = "&Print";
            this.tlbbtnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnPrint.ToolTipText = "Print Test Report";
            // 
            // tlbbtnSettings
            // 
            this.tlbbtnSettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnSettings.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnSettings.Image")));
            this.tlbbtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnSettings.Name = "tlbbtnSettings";
            this.tlbbtnSettings.Size = new System.Drawing.Size(63, 50);
            this.tlbbtnSettings.Tag = "Settings";
            this.tlbbtnSettings.Text = "&Settings";
            this.tlbbtnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnSettings.ToolTipText = "Change Settings";
            // 
            // tlbbtnCalibrate
            // 
            this.tlbbtnCalibrate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnCalibrate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnCalibrate.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnCalibrate.Image")));
            this.tlbbtnCalibrate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnCalibrate.Name = "tlbbtnCalibrate";
            this.tlbbtnCalibrate.Size = new System.Drawing.Size(65, 50);
            this.tlbbtnCalibrate.Tag = "Calibrate Device";
            this.tlbbtnCalibrate.Text = "&Calibrate";
            this.tlbbtnCalibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnCalibrate.ToolTipText = "Perform Calibration";
            // 
            // tlbbtnConfigureRace
            // 
            this.tlbbtnConfigureRace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnConfigureRace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnConfigureRace.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnConfigureRace.Image")));
            this.tlbbtnConfigureRace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnConfigureRace.Name = "tlbbtnConfigureRace";
            this.tlbbtnConfigureRace.Size = new System.Drawing.Size(104, 50);
            this.tlbbtnConfigureRace.Tag = "Configure Race";
            this.tlbbtnConfigureRace.Text = "&Configure Race";
            this.tlbbtnConfigureRace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tlbbtnclose
            // 
            this.tlbbtnclose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnclose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnclose.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnclose.Image")));
            this.tlbbtnclose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnclose.Name = "tlbbtnclose";
            this.tlbbtnclose.Size = new System.Drawing.Size(43, 50);
            this.tlbbtnclose.Tag = "Close";
            this.tlbbtnclose.Text = "&Close";
            this.tlbbtnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnclose.ToolTipText = "Close";
            // 
            // pnlc1Spiro
            // 
            this.pnlc1Spiro.Controls.Add(this.label4);
            this.pnlc1Spiro.Controls.Add(this.label3);
            this.pnlc1Spiro.Controls.Add(this.label2);
            this.pnlc1Spiro.Controls.Add(this.label1);
            this.pnlc1Spiro.Controls.Add(this.c1Spiro);
            this.pnlc1Spiro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlc1Spiro.Location = new System.Drawing.Point(0, 29);
            this.pnlc1Spiro.Name = "pnlc1Spiro";
            this.pnlc1Spiro.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlc1Spiro.Size = new System.Drawing.Size(1192, 655);
            this.pnlc1Spiro.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(1188, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 650);
            this.label4.TabIndex = 47;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 650);
            this.label3.TabIndex = 46;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 654);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1186, 1);
            this.label2.TabIndex = 45;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1186, 1);
            this.label1.TabIndex = 44;
            this.label1.Text = "label1";
            // 
            // c1Spiro
            // 
            this.c1Spiro.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Spiro.AllowEditing = false;
            this.c1Spiro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Spiro.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Spiro.ColumnInfo = "9,0,0,0,0,95,Columns:";
            this.c1Spiro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Spiro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Spiro.Location = new System.Drawing.Point(3, 3);
            this.c1Spiro.Margin = new System.Windows.Forms.Padding(2);
            this.c1Spiro.Name = "c1Spiro";
            this.c1Spiro.Rows.DefaultSize = 19;
            this.c1Spiro.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Spiro.Size = new System.Drawing.Size(1186, 652);
            this.c1Spiro.StyleInfo = resources.GetString("c1Spiro.StyleInfo");
            this.c1Spiro.TabIndex = 43;
            this.c1Spiro.Tag = "Print";
            this.c1Spiro.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1Spiro.Tree.NodeImageExpanded")));
            this.c1Spiro.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            this.c1Spiro.SelChange += new System.EventHandler(this.c1Spiro_SelChange);
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_LabMain);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1192, 54);
            this.pnlToolStrip.TabIndex = 43;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlc1Spiro);
            this.pnlMain.Controls.Add(this.splitter1);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.PnlInterpretation);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1192, 778);
            this.pnlMain.TabIndex = 44;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 684);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1192, 4);
            this.splitter1.TabIndex = 44;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel1.Size = new System.Drawing.Size(1192, 29);
            this.panel1.TabIndex = 45;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lblSelected);
            this.panel2.Controls.Add(this.BtnNext);
            this.panel2.Controls.Add(this.BtnFirst);
            this.panel2.Controls.Add(this.BtnLast);
            this.panel2.Controls.Add(this.BtnPrev);
            this.panel2.Controls.Add(this.cmbPageSize);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.ChkUseDateRange);
            this.panel2.Controls.Add(this.cmbTestType);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.dtpTodate);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.dtpFromDate);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1186, 26);
            this.panel2.TabIndex = 43;
            // 
            // lblSelected
            // 
            this.lblSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelected.BackColor = System.Drawing.Color.Transparent;
            this.lblSelected.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSelected.Location = new System.Drawing.Point(1018, 4);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(119, 18);
            this.lblSelected.TabIndex = 60;
            this.lblSelected.Text = "Page 1 of 1";
            this.lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnNext
            // 
            this.BtnNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNext.BackColor = System.Drawing.Color.Transparent;
            this.BtnNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnNext.BackgroundImage")));
            this.BtnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnNext.FlatAppearance.BorderSize = 0;
            this.BtnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNext.Location = new System.Drawing.Point(1136, 3);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(24, 21);
            this.BtnNext.TabIndex = 61;
            this.toolTip1.SetToolTip(this.BtnNext, "Next Recored");
            this.BtnNext.UseVisualStyleBackColor = false;
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // BtnFirst
            // 
            this.BtnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFirst.BackColor = System.Drawing.Color.Transparent;
            this.BtnFirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnFirst.BackgroundImage")));
            this.BtnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnFirst.FlatAppearance.BorderSize = 0;
            this.BtnFirst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnFirst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFirst.Location = new System.Drawing.Point(971, 4);
            this.BtnFirst.Name = "BtnFirst";
            this.BtnFirst.Size = new System.Drawing.Size(24, 21);
            this.BtnFirst.TabIndex = 48;
            this.toolTip1.SetToolTip(this.BtnFirst, "First Recored");
            this.BtnFirst.UseVisualStyleBackColor = false;
            this.BtnFirst.Click += new System.EventHandler(this.BtnFirst_Click);
            // 
            // BtnLast
            // 
            this.BtnLast.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnLast.BackColor = System.Drawing.Color.Transparent;
            this.BtnLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnLast.BackgroundImage")));
            this.BtnLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnLast.FlatAppearance.BorderSize = 0;
            this.BtnLast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnLast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLast.Location = new System.Drawing.Point(1159, 4);
            this.BtnLast.Name = "BtnLast";
            this.BtnLast.Size = new System.Drawing.Size(24, 21);
            this.BtnLast.TabIndex = 63;
            this.toolTip1.SetToolTip(this.BtnLast, "Last Recored");
            this.BtnLast.UseVisualStyleBackColor = false;
            this.BtnLast.Click += new System.EventHandler(this.BtnLast_Click);
            // 
            // BtnPrev
            // 
            this.BtnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrev.BackColor = System.Drawing.Color.Transparent;
            this.BtnPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnPrev.BackgroundImage")));
            this.BtnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnPrev.FlatAppearance.BorderSize = 0;
            this.BtnPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrev.Location = new System.Drawing.Point(996, 4);
            this.BtnPrev.Name = "BtnPrev";
            this.BtnPrev.Size = new System.Drawing.Size(24, 21);
            this.BtnPrev.TabIndex = 62;
            this.toolTip1.SetToolTip(this.BtnPrev, "Previous Recored");
            this.BtnPrev.UseVisualStyleBackColor = false;
            this.BtnPrev.Click += new System.EventHandler(this.BtnPrev_Click);
            // 
            // cmbPageSize
            // 
            this.cmbPageSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPageSize.FormattingEnabled = true;
            this.cmbPageSize.Location = new System.Drawing.Point(889, 2);
            this.cmbPageSize.Name = "cmbPageSize";
            this.cmbPageSize.Size = new System.Drawing.Size(74, 22);
            this.cmbPageSize.TabIndex = 59;
            this.cmbPageSize.SelectedIndexChanged += new System.EventHandler(this.cmbPageSize_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(789, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 14);
            this.label15.TabIndex = 58;
            this.label15.Text = "Tests per Page :";
            // 
            // ChkUseDateRange
            // 
            this.ChkUseDateRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ChkUseDateRange.AutoSize = true;
            this.ChkUseDateRange.Checked = true;
            this.ChkUseDateRange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkUseDateRange.Location = new System.Drawing.Point(124, 6);
            this.ChkUseDateRange.Name = "ChkUseDateRange";
            this.ChkUseDateRange.Size = new System.Drawing.Size(15, 14);
            this.ChkUseDateRange.TabIndex = 57;
            this.ChkUseDateRange.UseVisualStyleBackColor = true;
            this.ChkUseDateRange.CheckStateChanged += new System.EventHandler(this.ChkUseDateRange_CheckStateChanged);
            // 
            // cmbTestType
            // 
            this.cmbTestType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbTestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTestType.FormattingEnabled = true;
            this.cmbTestType.Items.AddRange(new object[] {
            "All",
            "Pre",
            "Post",
            "Pre/Post"});
            this.cmbTestType.Location = new System.Drawing.Point(465, 2);
            this.cmbTestType.Name = "cmbTestType";
            this.cmbTestType.Size = new System.Drawing.Size(143, 22);
            this.cmbTestType.TabIndex = 56;
            this.cmbTestType.SelectedIndexChanged += new System.EventHandler(this.cmbTestType_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(391, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 14);
            this.label14.TabIndex = 55;
            this.label14.Text = "Test Type :";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(1, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1184, 1);
            this.label11.TabIndex = 54;
            this.label11.Text = "label11";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(252, 6);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(26, 14);
            this.label20.TabIndex = 53;
            this.label20.Text = "To:";
            // 
            // dtpTodate
            // 
            this.dtpTodate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTodate.Location = new System.Drawing.Point(280, 2);
            this.dtpTodate.Name = "dtpTodate";
            this.dtpTodate.Size = new System.Drawing.Size(109, 22);
            this.dtpTodate.TabIndex = 52;
            this.dtpTodate.ValueChanged += new System.EventHandler(this.dtpTodate_ValueChanged);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(21, 6);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 14);
            this.label19.TabIndex = 49;
            this.label19.Text = "Test Date From :";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(141, 2);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(109, 22);
            this.dtpFromDate.TabIndex = 49;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Location = new System.Drawing.Point(1185, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 25);
            this.label9.TabIndex = 47;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 25);
            this.label10.TabIndex = 46;
            this.label10.Text = "label10";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1186, 1);
            this.label12.TabIndex = 44;
            this.label12.Text = "label12";
            // 
            // PnlInterpretation
            // 
            this.PnlInterpretation.Controls.Add(this.txtInterpreatation);
            this.PnlInterpretation.Controls.Add(this.panel5);
            this.PnlInterpretation.Controls.Add(this.label5);
            this.PnlInterpretation.Controls.Add(this.label6);
            this.PnlInterpretation.Controls.Add(this.label7);
            this.PnlInterpretation.Controls.Add(this.label8);
            this.PnlInterpretation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlInterpretation.Location = new System.Drawing.Point(0, 688);
            this.PnlInterpretation.Name = "PnlInterpretation";
            this.PnlInterpretation.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.PnlInterpretation.Size = new System.Drawing.Size(1192, 90);
            this.PnlInterpretation.TabIndex = 43;
            this.PnlInterpretation.Visible = false;
            // 
            // txtInterpreatation
            // 
            this.txtInterpreatation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInterpreatation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInterpreatation.Location = new System.Drawing.Point(4, 27);
            this.txtInterpreatation.Name = "txtInterpreatation";
            this.txtInterpreatation.ReadOnly = true;
            this.txtInterpreatation.Size = new System.Drawing.Size(1184, 59);
            this.txtInterpreatation.TabIndex = 48;
            this.txtInterpreatation.Text = "";
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label23);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(4, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1184, 26);
            this.panel5.TabIndex = 49;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(8, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 14);
            this.label13.TabIndex = 52;
            this.label13.Text = "Interpretation";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Location = new System.Drawing.Point(0, 25);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1184, 1);
            this.label23.TabIndex = 45;
            this.label23.Text = "label23";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(1188, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 85);
            this.label5.TabIndex = 47;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 85);
            this.label6.TabIndex = 46;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1186, 1);
            this.label7.TabIndex = 45;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1186, 1);
            this.label8.TabIndex = 44;
            this.label8.Text = "label8";
            // 
            // frmViewSpirometryTests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1192, 832);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewSpirometryTests";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spirometry Test";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSpiroTest_Load);
            this.ts_LabMain.ResumeLayout(false);
            this.ts_LabMain.PerformLayout();
            this.pnlc1Spiro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Spiro)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.PnlInterpretation.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus ts_LabMain;
        private System.Windows.Forms.ToolStripButton tlbbtnNew;
        private System.Windows.Forms.ToolStripButton tlbbtnReview;
        private System.Windows.Forms.ToolStripButton tlbbtnCompare;
        private System.Windows.Forms.ToolStripButton tlbbtnCalibrate;
        private System.Windows.Forms.Panel pnlc1Spiro;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Spiro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStripButton tlbbtnPost;
        private System.Windows.Forms.ToolStripButton tlbbtnclose;
        private System.Windows.Forms.ToolStripButton tlbbtnView;
        private System.Windows.Forms.ToolStripButton tlbbtnStatus;
        private System.Windows.Forms.ToolStripButton tlbbtnPrint;
        private System.Windows.Forms.ToolStripButton tlbbtnConfigureRace;
        private System.Windows.Forms.ToolStripButton tlbbtnSettings;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel PnlInterpretation;
        private System.Windows.Forms.RichTextBox txtInterpreatation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker dtpTodate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbTestType;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox ChkUseDateRange;
        private System.Windows.Forms.ComboBox cmbPageSize;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button BtnLast;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.Button BtnFirst;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.ToolTip toolTip1;
       


    }
}