namespace gloBilling
{
    partial class frmClaimExp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClaimExp));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlC1TOSCPT = new System.Windows.Forms.Panel();
            this.C1MainClaim = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.C1NewClaim = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.C1SplitClaim = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblChargeSplit = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnUnSplit = new System.Windows.Forms.Button();
            this.btnSplit = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.lblCharges = new System.Windows.Forms.Label();
            this.lblclaim = new System.Windows.Forms.Label();
            this.lblpatient = new System.Windows.Forms.Label();
            this.lbldx = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblClaimNo = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.tooltip_Dx = new System.Windows.Forms.ToolTip(this.components);
            this.tsb_AutoSplit = new System.Windows.Forms.ToolStripButton();
            this.pnlTop.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlC1TOSCPT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1MainClaim)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1NewClaim)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1SplitClaim)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.ts_Commands);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1249, 54);
            this.pnlTop.TabIndex = 5;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_AutoSplit,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1249, 53);
            this.ts_Commands.TabIndex = 13;
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
            // pnlC1TOSCPT
            // 
            this.pnlC1TOSCPT.Controls.Add(this.C1MainClaim);
            this.pnlC1TOSCPT.Controls.Add(this.label16);
            this.pnlC1TOSCPT.Controls.Add(this.label17);
            this.pnlC1TOSCPT.Controls.Add(this.label18);
            this.pnlC1TOSCPT.Controls.Add(this.label19);
            this.pnlC1TOSCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlC1TOSCPT.Location = new System.Drawing.Point(0, 83);
            this.pnlC1TOSCPT.Name = "pnlC1TOSCPT";
            this.pnlC1TOSCPT.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlC1TOSCPT.Size = new System.Drawing.Size(657, 327);
            this.pnlC1TOSCPT.TabIndex = 60;
            // 
            // C1MainClaim
            // 
            this.C1MainClaim.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.C1MainClaim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1MainClaim.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1MainClaim.ColumnInfo = "14,0,0,0,0,105,Columns:";
            this.C1MainClaim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1MainClaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1MainClaim.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.C1MainClaim.Location = new System.Drawing.Point(1, 4);
            this.C1MainClaim.Name = "C1MainClaim";
            this.C1MainClaim.Rows.Count = 1;
            this.C1MainClaim.Rows.DefaultSize = 21;
            this.C1MainClaim.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1MainClaim.Size = new System.Drawing.Size(655, 322);
            this.C1MainClaim.StyleInfo = resources.GetString("C1MainClaim.StyleInfo");
            this.C1MainClaim.TabIndex = 0;
            this.C1MainClaim.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.C1MainClaim.KeyDownEdit += new C1.Win.C1FlexGrid.KeyEditEventHandler(this.C1MainClaim_KeyDownEdit);
            this.C1MainClaim.KeyUpEdit += new C1.Win.C1FlexGrid.KeyEditEventHandler(this.C1MainClaim_KeyUpEdit);
            this.C1MainClaim.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.C1MainClaim_KeyPressEdit);
            this.C1MainClaim.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1MainClaim_CellChanged);
            this.C1MainClaim.Click += new System.EventHandler(this.C1MainClaim_Click);
            this.C1MainClaim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C1MainClaim_KeyDown);
            this.C1MainClaim.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.C1MainClaim_KeyPress);
            this.C1MainClaim.KeyUp += new System.Windows.Forms.KeyEventHandler(this.C1MainClaim_KeyUp);
            this.C1MainClaim.MouseLeave += new System.EventHandler(this.C1NewClaim_MouseLeave);
            this.C1MainClaim.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1NewClaim_MouseMove);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label16.Location = new System.Drawing.Point(1, 326);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(655, 1);
            this.label16.TabIndex = 8;
            this.label16.Text = "label2";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(0, 4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 323);
            this.label17.TabIndex = 7;
            this.label17.Text = "label4";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label18.Location = new System.Drawing.Point(656, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 323);
            this.label18.TabIndex = 3;
            this.label18.Text = "label3";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(0, 3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(657, 1);
            this.label19.TabIndex = 5;
            this.label19.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.C1NewClaim);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel1.Size = new System.Drawing.Size(589, 204);
            this.panel1.TabIndex = 60;
            // 
            // C1NewClaim
            // 
            this.C1NewClaim.AllowEditing = false;
            this.C1NewClaim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1NewClaim.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1NewClaim.ColumnInfo = "6,0,0,0,0,105,Columns:";
            this.C1NewClaim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1NewClaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1NewClaim.Location = new System.Drawing.Point(1, 32);
            this.C1NewClaim.Name = "C1NewClaim";
            this.C1NewClaim.Rows.Count = 1;
            this.C1NewClaim.Rows.DefaultSize = 21;
            this.C1NewClaim.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.C1NewClaim.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1NewClaim.Size = new System.Drawing.Size(587, 171);
            this.C1NewClaim.StyleInfo = resources.GetString("C1NewClaim.StyleInfo");
            this.C1NewClaim.TabIndex = 0;
            this.C1NewClaim.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.C1NewClaim.RowColChange += new System.EventHandler(this.C1NewClaim_RowColChange);
            this.C1NewClaim.MouseLeave += new System.EventHandler(this.C1NewClaim_MouseLeave);
            this.C1NewClaim.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1NewClaim_MouseMove);
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label11);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(1, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(587, 28);
            this.panel6.TabIndex = 63;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(587, 27);
            this.label11.TabIndex = 10;
            this.label11.Text = "   New Claims";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label12.Location = new System.Drawing.Point(0, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(587, 1);
            this.label12.TabIndex = 9;
            this.label12.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(587, 1);
            this.label1.TabIndex = 8;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 200);
            this.label2.TabIndex = 7;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(588, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 200);
            this.label3.TabIndex = 3;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(589, 1);
            this.label4.TabIndex = 5;
            this.label4.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.C1SplitClaim);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 204);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel2.Size = new System.Drawing.Size(589, 206);
            this.panel2.TabIndex = 60;
            // 
            // C1SplitClaim
            // 
            this.C1SplitClaim.AllowEditing = false;
            this.C1SplitClaim.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.C1SplitClaim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1SplitClaim.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1SplitClaim.ColumnInfo = "12,0,0,0,0,105,Columns:";
            this.C1SplitClaim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1SplitClaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1SplitClaim.Location = new System.Drawing.Point(1, 32);
            this.C1SplitClaim.Name = "C1SplitClaim";
            this.C1SplitClaim.Rows.Count = 1;
            this.C1SplitClaim.Rows.DefaultSize = 21;
            this.C1SplitClaim.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.C1SplitClaim.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1SplitClaim.Size = new System.Drawing.Size(587, 173);
            this.C1SplitClaim.StyleInfo = resources.GetString("C1SplitClaim.StyleInfo");
            this.C1SplitClaim.TabIndex = 0;
            this.C1SplitClaim.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.C1SplitClaim.MouseLeave += new System.EventHandler(this.C1NewClaim_MouseLeave);
            this.C1SplitClaim.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1NewClaim_MouseMove);
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.lblChargeSplit);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(1, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(587, 28);
            this.panel5.TabIndex = 62;
            // 
            // lblChargeSplit
            // 
            this.lblChargeSplit.BackColor = System.Drawing.Color.Transparent;
            this.lblChargeSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChargeSplit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChargeSplit.Location = new System.Drawing.Point(0, 0);
            this.lblChargeSplit.Name = "lblChargeSplit";
            this.lblChargeSplit.Size = new System.Drawing.Size(587, 27);
            this.lblChargeSplit.TabIndex = 10;
            this.lblChargeSplit.Text = "   Charges for Split";
            this.lblChargeSplit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(0, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(587, 1);
            this.label9.TabIndex = 9;
            this.label9.Text = "label2";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(1, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(587, 1);
            this.label5.TabIndex = 8;
            this.label5.Text = "label2";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 202);
            this.label6.TabIndex = 7;
            this.label6.Text = "label4";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(588, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 202);
            this.label7.TabIndex = 3;
            this.label7.Text = "label3";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(589, 1);
            this.label8.TabIndex = 5;
            this.label8.Text = "label1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(657, 54);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel3.Size = new System.Drawing.Size(592, 410);
            this.panel3.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnUnSplit);
            this.panel4.Controls.Add(this.btnSplit);
            this.panel4.Location = new System.Drawing.Point(651, 54);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(56, 50);
            this.panel4.TabIndex = 61;
            // 
            // btnUnSplit
            // 
            this.btnUnSplit.AutoEllipsis = true;
            this.btnUnSplit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUnSplit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnUnSplit.FlatAppearance.BorderSize = 0;
            this.btnUnSplit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUnSplit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUnSplit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnSplit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnSplit.Image = ((System.Drawing.Image)(resources.GetObject("btnUnSplit.Image")));
            this.btnUnSplit.Location = new System.Drawing.Point(6, 210);
            this.btnUnSplit.Name = "btnUnSplit";
            this.btnUnSplit.Size = new System.Drawing.Size(38, 41);
            this.btnUnSplit.TabIndex = 57;
            this.btnUnSplit.UseVisualStyleBackColor = true;
            this.btnUnSplit.Click += new System.EventHandler(this.btnUnSplit_Click);
            // 
            // btnSplit
            // 
            this.btnSplit.AutoEllipsis = true;
            this.btnSplit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSplit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnSplit.FlatAppearance.BorderSize = 0;
            this.btnSplit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSplit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSplit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSplit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSplit.Image = ((System.Drawing.Image)(resources.GetObject("btnSplit.Image")));
            this.btnSplit.Location = new System.Drawing.Point(6, 210);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(38, 41);
            this.btnSplit.TabIndex = 56;
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.pnlC1TOSCPT);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 54);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel7.Size = new System.Drawing.Size(657, 410);
            this.panel7.TabIndex = 62;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.lblDiagnosis);
            this.panel8.Controls.Add(this.lblCharges);
            this.panel8.Controls.Add(this.lblclaim);
            this.panel8.Controls.Add(this.lblpatient);
            this.panel8.Controls.Add(this.lbldx);
            this.panel8.Controls.Add(this.label22);
            this.panel8.Controls.Add(this.label21);
            this.panel8.Controls.Add(this.lblClaimNo);
            this.panel8.Controls.Add(this.label13);
            this.panel8.Controls.Add(this.label14);
            this.panel8.Controls.Add(this.label15);
            this.panel8.Controls.Add(this.label20);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(657, 80);
            this.panel8.TabIndex = 60;
            // 
            // lblDiagnosis
            // 
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblDiagnosis.Location = new System.Drawing.Point(334, 7);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Size = new System.Drawing.Size(0, 14);
            this.lblDiagnosis.TabIndex = 65;
            // 
            // lblCharges
            // 
            this.lblCharges.AutoSize = true;
            this.lblCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCharges.Location = new System.Drawing.Point(174, 7);
            this.lblCharges.Name = "lblCharges";
            this.lblCharges.Size = new System.Drawing.Size(0, 14);
            this.lblCharges.TabIndex = 64;
            // 
            // lblclaim
            // 
            this.lblclaim.AutoSize = true;
            this.lblclaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblclaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblclaim.Location = new System.Drawing.Point(103, 11);
            this.lblclaim.Name = "lblclaim";
            this.lblclaim.Size = new System.Drawing.Size(0, 14);
            this.lblclaim.TabIndex = 63;
            // 
            // lblpatient
            // 
            this.lblpatient.AutoSize = true;
            this.lblpatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblpatient.Location = new System.Drawing.Point(102, 33);
            this.lblpatient.Name = "lblpatient";
            this.lblpatient.Size = new System.Drawing.Size(0, 14);
            this.lblpatient.TabIndex = 62;
            // 
            // lbldx
            // 
            this.lbldx.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbldx.Location = new System.Drawing.Point(102, 56);
            this.lbldx.Name = "lbldx";
            this.lbldx.Size = new System.Drawing.Size(548, 14);
            this.lbldx.TabIndex = 61;
            this.lbldx.MouseEnter += new System.EventHandler(this.lbldx_MouseEnter);
            this.lbldx.MouseLeave += new System.EventHandler(this.lbldx_MouseLeave);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Location = new System.Drawing.Point(27, 56);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(69, 14);
            this.label22.TabIndex = 60;
            this.label22.Text = "Diagnoses :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Location = new System.Drawing.Point(42, 33);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 14);
            this.label21.TabIndex = 59;
            this.label21.Text = "Patient :";
            // 
            // lblClaimNo
            // 
            this.lblClaimNo.AutoSize = true;
            this.lblClaimNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClaimNo.Location = new System.Drawing.Point(12, 11);
            this.lblClaimNo.Name = "lblClaimNo";
            this.lblClaimNo.Size = new System.Drawing.Size(85, 14);
            this.lblClaimNo.TabIndex = 58;
            this.lblClaimNo.Text = "Original Claim :";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(1, 79);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(655, 1);
            this.label13.TabIndex = 8;
            this.label13.Text = "label2";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 79);
            this.label14.TabIndex = 7;
            this.label14.Text = "label4";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label15.Location = new System.Drawing.Point(656, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 79);
            this.label15.TabIndex = 3;
            this.label15.Text = "label3";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(657, 1);
            this.label20.TabIndex = 5;
            this.label20.Text = "label1";
            // 
            // label23
            // 
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(1, 4);
            this.label23.Name = "label23";
            this.label23.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label23.Size = new System.Drawing.Size(1247, 24);
            this.label23.TabIndex = 9;
            this.label23.Text = "Note : Split the claim by entering a split number for each charge. Enter a split " +
    "# ‘1’ for the charges that should become the first new claim, enter a split # ‘2" +
    "’ for the second claim’s charges, etc.";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label31);
            this.panel9.Controls.Add(this.label23);
            this.panel9.Controls.Add(this.label32);
            this.panel9.Controls.Add(this.label33);
            this.panel9.Controls.Add(this.label34);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(0, 464);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel9.Size = new System.Drawing.Size(1249, 28);
            this.panel9.TabIndex = 63;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label31.Location = new System.Drawing.Point(1, 27);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1247, 1);
            this.label31.TabIndex = 8;
            this.label31.Text = "label2";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Left;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(0, 4);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 24);
            this.label32.TabIndex = 7;
            this.label32.Text = "label4";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Right;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label33.Location = new System.Drawing.Point(1248, 4);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1, 24);
            this.label33.TabIndex = 3;
            this.label33.Text = "label3";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Top;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(0, 3);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1249, 1);
            this.label34.TabIndex = 5;
            this.label34.Text = "label1";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // tsb_AutoSplit
            // 
            this.tsb_AutoSplit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_AutoSplit.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AutoSplit.Image")));
            this.tsb_AutoSplit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AutoSplit.Name = "tsb_AutoSplit";
            this.tsb_AutoSplit.Size = new System.Drawing.Size(137, 50);
            this.tsb_AutoSplit.Tag = "OK";
            this.tsb_AutoSplit.Text = "&Quick Split for Paper";
            this.tsb_AutoSplit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_AutoSplit.ToolTipText = "Quick Split for Paper";
            this.tsb_AutoSplit.Click += new System.EventHandler(this.tsb_AutoSplit_Click);
            // 
            // frmClaimExp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1249, 492);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel9);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClaimExp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Split Claim";
            this.Load += new System.EventHandler(this.frmClaimExp_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlC1TOSCPT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1MainClaim)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1NewClaim)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1SplitClaim)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlC1TOSCPT;
        private C1.Win.C1FlexGrid.C1FlexGrid C1MainClaim;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1FlexGrid.C1FlexGrid C1NewClaim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1FlexGrid.C1FlexGrid C1SplitClaim;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblChargeSplit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblClaimNo;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblclaim;
        private System.Windows.Forms.Label lblpatient;
        private System.Windows.Forms.Label lbldx;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.Label lblCharges;
        private System.Windows.Forms.Button btnUnSplit;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.ToolTip tooltip_Dx;
        internal System.Windows.Forms.ToolStripButton tsb_AutoSplit;

    }
}