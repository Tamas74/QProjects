namespace gloStripControl
{
    partial class frmPatientClaims
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
                if (dvNext != null)
                {
                    dvNext.Dispose();
                    dvNext = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientClaims));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label52 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPatientSearch = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.c1Claims = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlClaims = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlSearchStrip = new System.Windows.Forms.Panel();
            this.pnlPatients = new System.Windows.Forms.Panel();
            this.c1PatientDetails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Claims)).BeginInit();
            this.pnlClaims.SuspendLayout();
            this.pnlSearchStrip.SuspendLayout();
            this.pnlPatients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlToolStrip.Size = new System.Drawing.Size(1109, 54);
            this.pnlToolStrip.TabIndex = 28;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = global::gloStrips.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(3, 1);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1103, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "Save";
            this.tsb_OK.Text = "&Save&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            this.tsb_OK.Click += new System.EventHandler(this.tsb_OK_Click);
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
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTop.BackgroundImage = global::gloStrips.Properties.Resources.Img_Blue2007;
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.label52);
            this.pnlTop.Controls.Add(this.label7);
            this.pnlTop.Controls.Add(this.label9);
            this.pnlTop.Controls.Add(this.label13);
            this.pnlTop.Controls.Add(this.txtPatientSearch);
            this.pnlTop.Controls.Add(this.label6);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(3, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1103, 24);
            this.pnlTop.TabIndex = 29;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(0, 1);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 22);
            this.label52.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(1102, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 22);
            this.label7.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1103, 1);
            this.label9.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(0, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1103, 1);
            this.label13.TabIndex = 58;
            // 
            // txtPatientSearch
            // 
            this.txtPatientSearch.Location = new System.Drawing.Point(78, 1);
            this.txtPatientSearch.Name = "txtPatientSearch";
            this.txtPatientSearch.Size = new System.Drawing.Size(240, 22);
            this.txtPatientSearch.TabIndex = 0;
            this.txtPatientSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatientSearch_KeyPress);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1103, 24);
            this.label6.TabIndex = 20;
            this.label6.Text = "   Patient : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // c1Claims
            // 
            this.c1Claims.AllowEditing = false;
            this.c1Claims.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Claims.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Claims.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.c1Claims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Claims.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1Claims.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1Claims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Claims.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Claims.Location = new System.Drawing.Point(4, 1);
            this.c1Claims.Name = "c1Claims";
            this.c1Claims.Rows.Count = 1;
            this.c1Claims.Rows.DefaultSize = 19;
            this.c1Claims.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Claims.ShowCellLabels = true;
            this.c1Claims.Size = new System.Drawing.Size(1101, 186);
            this.c1Claims.StyleInfo = resources.GetString("c1Claims.StyleInfo");
            this.c1Claims.TabIndex = 54;
            this.c1Claims.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1Claims.Tree.NodeImageCollapsed")));
            this.c1Claims.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1Claims.Tree.NodeImageExpanded")));
            this.c1Claims.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1Claims_BeforeSort);
            this.c1Claims.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1Claims_AfterSort);
            this.c1Claims.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1Claims_MouseDoubleClick);
            // 
            // pnlClaims
            // 
            this.pnlClaims.AutoScroll = true;
            this.pnlClaims.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlClaims.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlClaims.Controls.Add(this.c1Claims);
            this.pnlClaims.Controls.Add(this.label1);
            this.pnlClaims.Controls.Add(this.label2);
            this.pnlClaims.Controls.Add(this.label3);
            this.pnlClaims.Controls.Add(this.label4);
            this.pnlClaims.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlClaims.ForeColor = System.Drawing.Color.White;
            this.pnlClaims.Location = new System.Drawing.Point(0, 408);
            this.pnlClaims.Name = "pnlClaims";
            this.pnlClaims.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlClaims.Size = new System.Drawing.Size(1109, 191);
            this.pnlClaims.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 186);
            this.label1.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(1105, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 186);
            this.label2.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1103, 1);
            this.label3.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(3, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1103, 1);
            this.label4.TabIndex = 58;
            // 
            // pnlSearchStrip
            // 
            this.pnlSearchStrip.Controls.Add(this.pnlTop);
            this.pnlSearchStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchStrip.Location = new System.Drawing.Point(0, 54);
            this.pnlSearchStrip.Name = "pnlSearchStrip";
            this.pnlSearchStrip.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearchStrip.Size = new System.Drawing.Size(1109, 30);
            this.pnlSearchStrip.TabIndex = 30;
            // 
            // pnlPatients
            // 
            this.pnlPatients.AutoScroll = true;
            this.pnlPatients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlPatients.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPatients.Controls.Add(this.c1PatientDetails);
            this.pnlPatients.Controls.Add(this.label5);
            this.pnlPatients.Controls.Add(this.label8);
            this.pnlPatients.Controls.Add(this.label10);
            this.pnlPatients.Controls.Add(this.label11);
            this.pnlPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatients.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPatients.ForeColor = System.Drawing.Color.White;
            this.pnlPatients.Location = new System.Drawing.Point(0, 84);
            this.pnlPatients.Name = "pnlPatients";
            this.pnlPatients.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlPatients.Size = new System.Drawing.Size(1109, 321);
            this.pnlPatients.TabIndex = 31;
            // 
            // c1PatientDetails
            // 
            this.c1PatientDetails.AllowEditing = false;
            this.c1PatientDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientDetails.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.c1PatientDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientDetails.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1PatientDetails.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1PatientDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientDetails.Location = new System.Drawing.Point(4, 1);
            this.c1PatientDetails.Name = "c1PatientDetails";
            this.c1PatientDetails.Rows.Count = 1;
            this.c1PatientDetails.Rows.DefaultSize = 19;
            this.c1PatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientDetails.ShowCellLabels = true;
            this.c1PatientDetails.Size = new System.Drawing.Size(1101, 319);
            this.c1PatientDetails.StyleInfo = resources.GetString("c1PatientDetails.StyleInfo");
            this.c1PatientDetails.TabIndex = 59;
            this.c1PatientDetails.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1PatientDetails.Tree.NodeImageCollapsed")));
            this.c1PatientDetails.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1PatientDetails.Tree.NodeImageExpanded")));
            this.c1PatientDetails.RowColChange += new System.EventHandler(this.c1PatientDetails_RowColChange);
            this.c1PatientDetails.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1PatientDetails_MouseClick);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(3, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 319);
            this.label5.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(1105, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 319);
            this.label8.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1103, 1);
            this.label10.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(3, 320);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1103, 1);
            this.label11.TabIndex = 58;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 405);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1109, 3);
            this.splitter1.TabIndex = 32;
            this.splitter1.TabStop = false;
            // 
            // frmPatientClaims
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1109, 599);
            this.Controls.Add(this.pnlPatients);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlClaims);
            this.Controls.Add(this.pnlSearchStrip);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientClaims";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Claims";
            this.Load += new System.EventHandler(this.frmPatientClaims_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Claims)).EndInit();
            this.pnlClaims.ResumeLayout(false);
            this.pnlSearchStrip.ResumeLayout(false);
            this.pnlPatients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TextBox txtPatientSearch;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Claims;
        private System.Windows.Forms.Panel pnlClaims;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlSearchStrip;
        private System.Windows.Forms.Panel pnlPatients;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Splitter splitter1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientDetails;

    }
}