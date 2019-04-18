namespace gloBilling
{
    partial class frmPaymentUseReserveInsurance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaymentUseReserveInsurance));
            this.panel2 = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_GenerateReserve = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowDetails = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowInsRefund = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlCPTGrid = new System.Windows.Forms.Panel();
            this.c1Reserve = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.pnlSelectReserve = new System.Windows.Forms.Panel();
            this.c1SelectReserve = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.chkCurrentCheck = new System.Windows.Forms.CheckBox();
            this.chkPatient = new System.Windows.Forms.CheckBox();
            this.btnSearchPatient = new System.Windows.Forms.Button();
            this.txtInsCompany = new System.Windows.Forms.TextBox();
            this.txtPatient = new System.Windows.Forms.TextBox();
            this.lblInsCompany = new System.Windows.Forms.Label();
            this.btnClearPatient = new System.Windows.Forms.Button();
            this.btnClearInsCompany = new System.Windows.Forms.Button();
            this.cmbClaimNo = new System.Windows.Forms.ComboBox();
            this.btnSearchInsuranceCompany = new System.Windows.Forms.Button();
            this.txtNoteText = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlCPTGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Reserve)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlSelectReserve.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SelectReserve)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ts_Commands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1186, 55);
            this.panel2.TabIndex = 1;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_GenerateReserve,
            this.tsb_ShowDetails,
            this.tsb_ShowInsRefund,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1186, 55);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_GenerateReserve
            // 
            this.tsb_GenerateReserve.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_GenerateReserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_GenerateReserve.Image = ((System.Drawing.Image)(resources.GetObject("tsb_GenerateReserve.Image")));
            this.tsb_GenerateReserve.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_GenerateReserve.Name = "tsb_GenerateReserve";
            this.tsb_GenerateReserve.Size = new System.Drawing.Size(66, 50);
            this.tsb_GenerateReserve.Tag = "Generate";
            this.tsb_GenerateReserve.Text = "&Generate";
            this.tsb_GenerateReserve.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_GenerateReserve.ToolTipText = "Generate";
            this.tsb_GenerateReserve.Click += new System.EventHandler(this.tsb_GenerateReserve_Click);
            // 
            // tsb_ShowDetails
            // 
            this.tsb_ShowDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ShowDetails.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowDetails.Image")));
            this.tsb_ShowDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowDetails.Name = "tsb_ShowDetails";
            this.tsb_ShowDetails.Size = new System.Drawing.Size(85, 50);
            this.tsb_ShowDetails.Tag = "ShowDetails";
            this.tsb_ShowDetails.Text = "&View Details";
            this.tsb_ShowDetails.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowDetails.ToolTipText = "View Details";
            this.tsb_ShowDetails.Click += new System.EventHandler(this.tsb_ShowDetails_Click);
            // 
            // tsb_ShowInsRefund
            // 
            this.tsb_ShowInsRefund.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowInsRefund.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ShowInsRefund.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowInsRefund.Image")));
            this.tsb_ShowInsRefund.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowInsRefund.Name = "tsb_ShowInsRefund";
            this.tsb_ShowInsRefund.Size = new System.Drawing.Size(120, 50);
            this.tsb_ShowInsRefund.Tag = "InsuranceRefund";
            this.tsb_ShowInsRefund.Text = "Insurance &Refund";
            this.tsb_ShowInsRefund.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowInsRefund.ToolTipText = "Insurance Refund";
            this.tsb_ShowInsRefund.Visible = false;
            this.tsb_ShowInsRefund.Click += new System.EventHandler(this.tsb_ShowInsRefund_Click);
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
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
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.pnlCPTGrid);
            this.pnlMain.Controls.Add(this.panel7);
            this.pnlMain.Controls.Add(this.pnlSelectReserve);
            this.pnlMain.Controls.Add(this.panel6);
            this.pnlMain.Controls.Add(this.pnlFilters);
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1186, 611);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlCPTGrid
            // 
            this.pnlCPTGrid.Controls.Add(this.c1Reserve);
            this.pnlCPTGrid.Controls.Add(this.panel1);
            this.pnlCPTGrid.Controls.Add(this.label3);
            this.pnlCPTGrid.Controls.Add(this.label2);
            this.pnlCPTGrid.Controls.Add(this.label12);
            this.pnlCPTGrid.Controls.Add(this.label1);
            this.pnlCPTGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCPTGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlCPTGrid.Location = new System.Drawing.Point(0, 413);
            this.pnlCPTGrid.Name = "pnlCPTGrid";
            this.pnlCPTGrid.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlCPTGrid.Size = new System.Drawing.Size(1186, 198);
            this.pnlCPTGrid.TabIndex = 4;
            // 
            // c1Reserve
            // 
            this.c1Reserve.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Reserve.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1Reserve.AutoGenerateColumns = false;
            this.c1Reserve.AutoResize = false;
            this.c1Reserve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Reserve.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Reserve.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Reserve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Reserve.ExtendLastCol = true;
            this.c1Reserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Reserve.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1Reserve.Location = new System.Drawing.Point(4, 22);
            this.c1Reserve.Name = "c1Reserve";
            this.c1Reserve.Rows.Count = 1;
            this.c1Reserve.Rows.DefaultSize = 19;
            this.c1Reserve.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Reserve.Size = new System.Drawing.Size(1178, 172);
            this.c1Reserve.StyleInfo = resources.GetString("c1Reserve.StyleInfo");
            this.c1Reserve.TabIndex = 0;
            this.c1Reserve.MouseLeave += new System.EventHandler(this.c1Reserve_MouseLeave);
            this.c1Reserve.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1Reserve_MouseDoubleClick);
            this.c1Reserve.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Reserve_MouseMove);
            this.c1Reserve.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1Reserve_KeyUp);
            this.c1Reserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(4, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1178, 21);
            this.panel1.TabIndex = 214;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Location = new System.Drawing.Point(0, 20);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1178, 1);
            this.label17.TabIndex = 213;
            // 
            // label15
            // 
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1178, 21);
            this.label15.TabIndex = 1;
            this.label15.Text = "  Payment\'s  Reserves";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1178, 1);
            this.label3.TabIndex = 213;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1178, 1);
            this.label2.TabIndex = 212;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 195);
            this.label12.TabIndex = 210;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(1182, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 195);
            this.label1.TabIndex = 211;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 385);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel7.Size = new System.Drawing.Size(1186, 28);
            this.panel7.TabIndex = 220;
            // 
            // panel8
            // 
            this.panel8.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label28);
            this.panel8.Controls.Add(this.label29);
            this.panel8.Controls.Add(this.label30);
            this.panel8.Controls.Add(this.label31);
            this.panel8.Controls.Add(this.label32);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1180, 25);
            this.panel8.TabIndex = 218;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Location = new System.Drawing.Point(1, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1178, 1);
            this.label28.TabIndex = 216;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.Location = new System.Drawing.Point(1179, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 24);
            this.label29.TabIndex = 215;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Location = new System.Drawing.Point(0, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 24);
            this.label30.TabIndex = 214;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label31.Location = new System.Drawing.Point(0, 24);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1180, 1);
            this.label31.TabIndex = 213;
            // 
            // label32
            // 
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(0, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1180, 25);
            this.label32.TabIndex = 0;
            this.label32.Text = "  3. Review the Payment\'s selected Reserves and confirm the Amount to Use :";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSelectReserve
            // 
            this.pnlSelectReserve.Controls.Add(this.c1SelectReserve);
            this.pnlSelectReserve.Controls.Add(this.panel3);
            this.pnlSelectReserve.Controls.Add(this.label9);
            this.pnlSelectReserve.Controls.Add(this.label8);
            this.pnlSelectReserve.Controls.Add(this.label10);
            this.pnlSelectReserve.Controls.Add(this.label11);
            this.pnlSelectReserve.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSelectReserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlSelectReserve.Location = new System.Drawing.Point(0, 173);
            this.pnlSelectReserve.Name = "pnlSelectReserve";
            this.pnlSelectReserve.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlSelectReserve.Size = new System.Drawing.Size(1186, 212);
            this.pnlSelectReserve.TabIndex = 5;
            // 
            // c1SelectReserve
            // 
            this.c1SelectReserve.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1SelectReserve.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1SelectReserve.AutoGenerateColumns = false;
            this.c1SelectReserve.AutoResize = false;
            this.c1SelectReserve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1SelectReserve.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SelectReserve.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1SelectReserve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SelectReserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SelectReserve.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1SelectReserve.Location = new System.Drawing.Point(4, 22);
            this.c1SelectReserve.Name = "c1SelectReserve";
            this.c1SelectReserve.Rows.Count = 1;
            this.c1SelectReserve.Rows.DefaultSize = 19;
            this.c1SelectReserve.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SelectReserve.Size = new System.Drawing.Size(1178, 186);
            this.c1SelectReserve.StyleInfo = resources.GetString("c1SelectReserve.StyleInfo");
            this.c1SelectReserve.TabIndex = 0;
            this.c1SelectReserve.MouseLeave += new System.EventHandler(this.c1SelectReserve_MouseLeave);
            this.c1SelectReserve.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1SelectReserve_MouseDoubleClick);
            this.c1SelectReserve.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1SelectReserve_MouseMove);
            this.c1SelectReserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SelectReserve_CellChanged);
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1178, 21);
            this.panel3.TabIndex = 215;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Location = new System.Drawing.Point(0, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1178, 1);
            this.label16.TabIndex = 213;
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1178, 21);
            this.label13.TabIndex = 0;
            this.label13.Text = "  Select Reserves";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1178, 1);
            this.label9.TabIndex = 212;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(4, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1178, 1);
            this.label8.TabIndex = 213;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 209);
            this.label10.TabIndex = 210;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Location = new System.Drawing.Point(1182, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 209);
            this.label11.TabIndex = 211;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 145);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel6.Size = new System.Drawing.Size(1186, 28);
            this.panel6.TabIndex = 219;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.label27);
            this.panel5.Controls.Add(this.label26);
            this.panel5.Controls.Add(this.label25);
            this.panel5.Controls.Add(this.label23);
            this.panel5.Controls.Add(this.label24);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1180, 25);
            this.panel5.TabIndex = 218;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.label27.Location = new System.Drawing.Point(1, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1178, 1);
            this.label27.TabIndex = 216;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.label26.Location = new System.Drawing.Point(1179, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 24);
            this.label26.TabIndex = 215;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 24);
            this.label25.TabIndex = 214;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Location = new System.Drawing.Point(0, 24);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1180, 1);
            this.label23.TabIndex = 213;
            // 
            // label24
            // 
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(0, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1180, 25);
            this.label24.TabIndex = 0;
            this.label24.Text = "  2. Select Reserves from the list :";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFilters
            // 
            this.pnlFilters.Controls.Add(this.chkCurrentCheck);
            this.pnlFilters.Controls.Add(this.chkPatient);
            this.pnlFilters.Controls.Add(this.btnSearchPatient);
            this.pnlFilters.Controls.Add(this.txtInsCompany);
            this.pnlFilters.Controls.Add(this.txtPatient);
            this.pnlFilters.Controls.Add(this.lblInsCompany);
            this.pnlFilters.Controls.Add(this.btnClearPatient);
            this.pnlFilters.Controls.Add(this.btnClearInsCompany);
            this.pnlFilters.Controls.Add(this.cmbClaimNo);
            this.pnlFilters.Controls.Add(this.btnSearchInsuranceCompany);
            this.pnlFilters.Controls.Add(this.txtNoteText);
            this.pnlFilters.Controls.Add(this.label19);
            this.pnlFilters.Controls.Add(this.panel4);
            this.pnlFilters.Controls.Add(this.label4);
            this.pnlFilters.Controls.Add(this.label5);
            this.pnlFilters.Controls.Add(this.label7);
            this.pnlFilters.Controls.Add(this.label6);
            this.pnlFilters.Controls.Add(this.label18);
            this.pnlFilters.Controls.Add(this.label21);
            this.pnlFilters.Controls.Add(this.label22);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(0, 55);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(3);
            this.pnlFilters.Size = new System.Drawing.Size(1186, 90);
            this.pnlFilters.TabIndex = 217;
            // 
            // chkCurrentCheck
            // 
            this.chkCurrentCheck.AutoSize = true;
            this.chkCurrentCheck.Location = new System.Drawing.Point(664, 58);
            this.chkCurrentCheck.Name = "chkCurrentCheck";
            this.chkCurrentCheck.Size = new System.Drawing.Size(119, 18);
            this.chkCurrentCheck.TabIndex = 246;
            this.chkCurrentCheck.Text = "Current payment";
            this.chkCurrentCheck.UseVisualStyleBackColor = true;
            this.chkCurrentCheck.CheckedChanged += new System.EventHandler(this.chkCurrentCheck_CheckedChanged);
            // 
            // chkPatient
            // 
            this.chkPatient.AutoSize = true;
            this.chkPatient.Location = new System.Drawing.Point(524, 58);
            this.chkPatient.Name = "chkPatient";
            this.chkPatient.Size = new System.Drawing.Size(110, 18);
            this.chkPatient.TabIndex = 245;
            this.chkPatient.Text = "Current patient";
            this.chkPatient.UseVisualStyleBackColor = true;
            this.chkPatient.CheckedChanged += new System.EventHandler(this.chkPatient_CheckedChanged);
            // 
            // btnSearchPatient
            // 
            this.btnSearchPatient.AutoEllipsis = true;
            this.btnSearchPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchPatient.BackgroundImage")));
            this.btnSearchPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchPatient.Image")));
            this.btnSearchPatient.Location = new System.Drawing.Point(776, 32);
            this.btnSearchPatient.Name = "btnSearchPatient";
            this.btnSearchPatient.Size = new System.Drawing.Size(21, 21);
            this.btnSearchPatient.TabIndex = 224;
            this.btnSearchPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchPatient, "Select Patient");
            this.btnSearchPatient.UseVisualStyleBackColor = false;
            this.btnSearchPatient.Click += new System.EventHandler(this.btnSearchPatient_Click);
            // 
            // txtInsCompany
            // 
            this.txtInsCompany.BackColor = System.Drawing.Color.White;
            this.txtInsCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsCompany.ForeColor = System.Drawing.Color.Black;
            this.txtInsCompany.Location = new System.Drawing.Point(141, 32);
            this.txtInsCompany.Name = "txtInsCompany";
            this.txtInsCompany.ReadOnly = true;
            this.txtInsCompany.ShortcutsEnabled = false;
            this.txtInsCompany.Size = new System.Drawing.Size(249, 22);
            this.txtInsCompany.TabIndex = 244;
            // 
            // txtPatient
            // 
            this.txtPatient.BackColor = System.Drawing.Color.White;
            this.txtPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient.ForeColor = System.Drawing.Color.Black;
            this.txtPatient.Location = new System.Drawing.Point(522, 32);
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.ReadOnly = true;
            this.txtPatient.ShortcutsEnabled = false;
            this.txtPatient.Size = new System.Drawing.Size(249, 22);
            this.txtPatient.TabIndex = 243;
            this.txtPatient.TabStop = false;
            // 
            // lblInsCompany
            // 
            this.lblInsCompany.AutoSize = true;
            this.lblInsCompany.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.lblInsCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsCompany.Location = new System.Drawing.Point(655, 29);
            this.lblInsCompany.Name = "lblInsCompany";
            this.lblInsCompany.Size = new System.Drawing.Size(0, 14);
            this.lblInsCompany.TabIndex = 235;
            this.lblInsCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInsCompany.Visible = false;
            // 
            // btnClearPatient
            // 
            this.btnClearPatient.AutoEllipsis = true;
            this.btnClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.BackgroundImage")));
            this.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.Image")));
            this.btnClearPatient.Location = new System.Drawing.Point(802, 32);
            this.btnClearPatient.Name = "btnClearPatient";
            this.btnClearPatient.Size = new System.Drawing.Size(21, 21);
            this.btnClearPatient.TabIndex = 227;
            this.toolTip1.SetToolTip(this.btnClearPatient, "Clear Patient");
            this.btnClearPatient.UseVisualStyleBackColor = false;
            this.btnClearPatient.Click += new System.EventHandler(this.btnClearPatient_Click);
            // 
            // btnClearInsCompany
            // 
            this.btnClearInsCompany.AutoEllipsis = true;
            this.btnClearInsCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsCompany.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsCompany.BackgroundImage")));
            this.btnClearInsCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsCompany.Image")));
            this.btnClearInsCompany.Location = new System.Drawing.Point(421, 32);
            this.btnClearInsCompany.Name = "btnClearInsCompany";
            this.btnClearInsCompany.Size = new System.Drawing.Size(21, 21);
            this.btnClearInsCompany.TabIndex = 226;
            this.toolTip1.SetToolTip(this.btnClearInsCompany, "Clear Insurance Company");
            this.btnClearInsCompany.UseVisualStyleBackColor = false;
            this.btnClearInsCompany.Click += new System.EventHandler(this.btnClearInsCompany_Click);
            // 
            // cmbClaimNo
            // 
            this.cmbClaimNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbClaimNo.FormattingEnabled = true;
            this.cmbClaimNo.Location = new System.Drawing.Point(904, 32);
            this.cmbClaimNo.MaxLength = 15;
            this.cmbClaimNo.Name = "cmbClaimNo";
            this.cmbClaimNo.Size = new System.Drawing.Size(110, 22);
            this.cmbClaimNo.TabIndex = 225;
            this.cmbClaimNo.Leave += new System.EventHandler(this.cmbClaimNo_Leave);
            this.cmbClaimNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbClaimNo_KeyPress);
            // 
            // btnSearchInsuranceCompany
            // 
            this.btnSearchInsuranceCompany.AutoEllipsis = true;
            this.btnSearchInsuranceCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchInsuranceCompany.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchInsuranceCompany.BackgroundImage")));
            this.btnSearchInsuranceCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchInsuranceCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchInsuranceCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchInsuranceCompany.Image")));
            this.btnSearchInsuranceCompany.Location = new System.Drawing.Point(395, 32);
            this.btnSearchInsuranceCompany.Name = "btnSearchInsuranceCompany";
            this.btnSearchInsuranceCompany.Size = new System.Drawing.Size(21, 21);
            this.btnSearchInsuranceCompany.TabIndex = 223;
            this.btnSearchInsuranceCompany.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchInsuranceCompany, "Select Insurance Company");
            this.btnSearchInsuranceCompany.UseVisualStyleBackColor = false;
            this.btnSearchInsuranceCompany.Click += new System.EventHandler(this.btnSearchInsuranceCompany_Click);
            // 
            // txtNoteText
            // 
            this.txtNoteText.Location = new System.Drawing.Point(141, 58);
            this.txtNoteText.Name = "txtNoteText";
            this.txtNoteText.Size = new System.Drawing.Size(249, 22);
            this.txtNoteText.TabIndex = 221;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(4, 86);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1178, 1);
            this.label19.TabIndex = 218;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1178, 21);
            this.panel4.TabIndex = 216;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Location = new System.Drawing.Point(0, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1178, 1);
            this.label20.TabIndex = 213;
            // 
            // label14
            // 
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1178, 21);
            this.label14.TabIndex = 0;
            this.label14.Text = "  1. Choose a filter to search for the Reserve to use :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "Note Text Keyword :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(466, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "Patient :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(848, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 14);
            this.label7.TabIndex = 11;
            this.label7.Text = "Claim # :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "Insurance Company :";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(4, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1178, 1);
            this.label18.TabIndex = 217;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Location = new System.Drawing.Point(3, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 84);
            this.label21.TabIndex = 219;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Location = new System.Drawing.Point(1182, 3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 84);
            this.label22.TabIndex = 220;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmPaymentUseReserveInsurance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1186, 611);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPaymentUseReserveInsurance";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Use Reserve";
            this.Load += new System.EventHandler(this.frmPaymentUseReserve_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPaymentUseReserve_FormClosed);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlCPTGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Reserve)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.pnlSelectReserve.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SelectReserve)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlCPTGrid;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Reserve;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.ToolStripButton tsb_ShowDetails;
        internal System.Windows.Forms.ToolStripButton tsb_ShowInsRefund;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlSelectReserve;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SelectReserve;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtNoteText;
        private System.Windows.Forms.Button btnSearchPatient;
        private System.Windows.Forms.Button btnSearchInsuranceCompany;
        private System.Windows.Forms.ComboBox cmbClaimNo;
        private System.Windows.Forms.Button btnClearPatient;
        private System.Windows.Forms.Button btnClearInsCompany;
        internal System.Windows.Forms.ToolStripButton tsb_GenerateReserve;
        private System.Windows.Forms.TextBox txtInsCompany;
        private System.Windows.Forms.TextBox txtPatient;
        private System.Windows.Forms.Label lblInsCompany;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chkPatient;
        private System.Windows.Forms.CheckBox chkCurrentCheck;
    }
}