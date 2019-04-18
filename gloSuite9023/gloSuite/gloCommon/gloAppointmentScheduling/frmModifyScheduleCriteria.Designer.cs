namespace gloAppointmentScheduling
{
    partial class frmModifyScheduleCriteria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModifyScheduleCriteria));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlCriteria_ProblemType = new System.Windows.Forms.Panel();
            this.label54 = new System.Windows.Forms.Label();
            this.c1ProblemType = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlCriteria_ProblemType_Header = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblCriteria_ProviderProblemType_Header = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlCriteria_Resource = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.c1Resource = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlCriteria_Resource_Header = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlCriteria_ProblemType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ProblemType)).BeginInit();
            this.pnlCriteria_ProblemType_Header.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.pnlCriteria_Resource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Resource)).BeginInit();
            this.pnlCriteria_Resource_Header.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnl_Base.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(591, 54);
            this.pnlToolStrip.TabIndex = 2;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Modify,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(591, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(53, 50);
            this.tsb_Modify.Tag = "OK";
            this.tsb_Modify.Text = "&Modify";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            // 
            // pnlCriteria_ProblemType
            // 
            this.pnlCriteria_ProblemType.Controls.Add(this.label54);
            this.pnlCriteria_ProblemType.Controls.Add(this.c1ProblemType);
            this.pnlCriteria_ProblemType.Controls.Add(this.label10);
            this.pnlCriteria_ProblemType.Controls.Add(this.label11);
            this.pnlCriteria_ProblemType.Controls.Add(this.label13);
            this.pnlCriteria_ProblemType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_ProblemType.Location = new System.Drawing.Point(0, 130);
            this.pnlCriteria_ProblemType.Name = "pnlCriteria_ProblemType";
            this.pnlCriteria_ProblemType.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlCriteria_ProblemType.Size = new System.Drawing.Size(591, 80);
            this.pnlCriteria_ProblemType.TabIndex = 0;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label54.Location = new System.Drawing.Point(4, 76);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(583, 1);
            this.label54.TabIndex = 143;
            // 
            // c1ProblemType
            // 
            this.c1ProblemType.AllowEditing = false;
            this.c1ProblemType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ProblemType.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ProblemType.ColumnInfo = "3,1,0,0,0,105,Columns:";
            this.c1ProblemType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ProblemType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ProblemType.Location = new System.Drawing.Point(4, 1);
            this.c1ProblemType.Name = "c1ProblemType";
            this.c1ProblemType.Rows.Count = 5;
            this.c1ProblemType.Rows.DefaultSize = 21;
            this.c1ProblemType.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ProblemType.Size = new System.Drawing.Size(583, 76);
            this.c1ProblemType.StyleInfo = resources.GetString("c1ProblemType.StyleInfo");
            this.c1ProblemType.TabIndex = 0;
            this.c1ProblemType.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1ProblemType_MouseMove);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(3, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 76);
            this.label10.TabIndex = 139;
            this.label10.Text = "label10";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Location = new System.Drawing.Point(587, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 76);
            this.label11.TabIndex = 140;
            this.label11.Text = "label11";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(585, 1);
            this.label13.TabIndex = 142;
            // 
            // pnlCriteria_ProblemType_Header
            // 
            this.pnlCriteria_ProblemType_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlCriteria_ProblemType_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCriteria_ProblemType_Header.Controls.Add(this.Panel2);
            this.pnlCriteria_ProblemType_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_ProblemType_Header.Location = new System.Drawing.Point(0, 104);
            this.pnlCriteria_ProblemType_Header.Name = "pnlCriteria_ProblemType_Header";
            this.pnlCriteria_ProblemType_Header.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlCriteria_ProblemType_Header.Size = new System.Drawing.Size(591, 26);
            this.pnlCriteria_ProblemType_Header.TabIndex = 137;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel2.Controls.Add(this.label6);
            this.Panel2.Controls.Add(this.label12);
            this.Panel2.Controls.Add(this.lblCriteria_ProviderProblemType_Header);
            this.Panel2.Controls.Add(this.label17);
            this.Panel2.Controls.Add(this.label18);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel2.Location = new System.Drawing.Point(3, 0);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(585, 23);
            this.Panel2.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(1, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(583, 1);
            this.label6.TabIndex = 8;
            this.label6.Text = "label2";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 22);
            this.label12.TabIndex = 7;
            this.label12.Text = "label4";
            // 
            // lblCriteria_ProviderProblemType_Header
            // 
            this.lblCriteria_ProviderProblemType_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblCriteria_ProviderProblemType_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCriteria_ProviderProblemType_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteria_ProviderProblemType_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCriteria_ProviderProblemType_Header.Location = new System.Drawing.Point(0, 1);
            this.lblCriteria_ProviderProblemType_Header.Name = "lblCriteria_ProviderProblemType_Header";
            this.lblCriteria_ProviderProblemType_Header.Size = new System.Drawing.Size(584, 22);
            this.lblCriteria_ProviderProblemType_Header.TabIndex = 0;
            this.lblCriteria_ProviderProblemType_Header.Text = "  Problem Type";
            this.lblCriteria_ProviderProblemType_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(584, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 22);
            this.label17.TabIndex = 6;
            this.label17.Text = "label3";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(585, 1);
            this.label18.TabIndex = 5;
            this.label18.Text = "label1";
            // 
            // pnlCriteria_Resource
            // 
            this.pnlCriteria_Resource.Controls.Add(this.label1);
            this.pnlCriteria_Resource.Controls.Add(this.label2);
            this.pnlCriteria_Resource.Controls.Add(this.c1Resource);
            this.pnlCriteria_Resource.Controls.Add(this.label4);
            this.pnlCriteria_Resource.Controls.Add(this.label5);
            this.pnlCriteria_Resource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCriteria_Resource.Location = new System.Drawing.Point(0, 236);
            this.pnlCriteria_Resource.Name = "pnlCriteria_Resource";
            this.pnlCriteria_Resource.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlCriteria_Resource.Size = new System.Drawing.Size(591, 80);
            this.pnlCriteria_Resource.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(4, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(583, 1);
            this.label1.TabIndex = 143;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(583, 1);
            this.label2.TabIndex = 141;
            // 
            // c1Resource
            // 
            this.c1Resource.AllowEditing = false;
            this.c1Resource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Resource.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Resource.ColumnInfo = "3,1,0,0,0,105,Columns:";
            this.c1Resource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Resource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Resource.Location = new System.Drawing.Point(4, 0);
            this.c1Resource.Name = "c1Resource";
            this.c1Resource.Rows.Count = 5;
            this.c1Resource.Rows.DefaultSize = 21;
            this.c1Resource.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Resource.Size = new System.Drawing.Size(583, 77);
            this.c1Resource.StyleInfo = resources.GetString("c1Resource.StyleInfo");
            this.c1Resource.TabIndex = 0;
            this.c1Resource.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Resource_MouseMove);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 77);
            this.label4.TabIndex = 139;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(587, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 77);
            this.label5.TabIndex = 140;
            this.label5.Text = "label5";
            // 
            // pnlCriteria_Resource_Header
            // 
            this.pnlCriteria_Resource_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlCriteria_Resource_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCriteria_Resource_Header.Controls.Add(this.panel1);
            this.pnlCriteria_Resource_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_Resource_Header.Location = new System.Drawing.Point(0, 210);
            this.pnlCriteria_Resource_Header.Name = "pnlCriteria_Resource_Header";
            this.pnlCriteria_Resource_Header.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlCriteria_Resource_Header.Size = new System.Drawing.Size(591, 26);
            this.pnlCriteria_Resource_Header.TabIndex = 137;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 23);
            this.panel1.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(583, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "  Resource";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label19.Location = new System.Drawing.Point(1, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(583, 1);
            this.label19.TabIndex = 8;
            this.label19.Text = "label2";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 22);
            this.label20.TabIndex = 7;
            this.label20.Text = "label4";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Right;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(584, 1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 22);
            this.label21.TabIndex = 6;
            this.label21.Text = "label3";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(585, 1);
            this.label22.TabIndex = 5;
            this.label22.Text = "label1";
            // 
            // pnl_Base
            // 
            this.pnl_Base.Controls.Add(this.lblEndTime);
            this.pnl_Base.Controls.Add(this.lblStartTime);
            this.pnl_Base.Controls.Add(this.label16);
            this.pnl_Base.Controls.Add(this.label15);
            this.pnl_Base.Controls.Add(this.lblMessage);
            this.pnl_Base.Controls.Add(this.label8);
            this.pnl_Base.Controls.Add(this.label9);
            this.pnl_Base.Controls.Add(this.label14);
            this.pnl_Base.Controls.Add(this.label7);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Base.Location = new System.Drawing.Point(0, 54);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_Base.Size = new System.Drawing.Size(591, 50);
            this.pnl_Base.TabIndex = 173;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.BackColor = System.Drawing.Color.Transparent;
            this.lblEndTime.Location = new System.Drawing.Point(506, 28);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(60, 14);
            this.lblEndTime.TabIndex = 64;
            this.lblEndTime.Text = "11:00 AM";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.BackColor = System.Drawing.Color.Transparent;
            this.lblStartTime.Location = new System.Drawing.Point(506, 9);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(60, 14);
            this.lblStartTime.TabIndex = 63;
            this.lblStartTime.Text = "09:00 AM";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(434, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 14);
            this.label16.TabIndex = 62;
            this.label16.Text = "End Time :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(428, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 14);
            this.label15.TabIndex = 61;
            this.label15.Text = "Start Time :";
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.Maroon;
            this.lblMessage.Location = new System.Drawing.Point(10, 12);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(412, 30);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "Do you want to modify this Schedule ?\r\n";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(4, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(583, 1);
            this.label8.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Location = new System.Drawing.Point(587, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 43);
            this.label9.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(3, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 43);
            this.label14.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(585, 1);
            this.label7.TabIndex = 3;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmModifyScheduleCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(591, 316);
            this.Controls.Add(this.pnlCriteria_Resource);
            this.Controls.Add(this.pnlCriteria_Resource_Header);
            this.Controls.Add(this.pnlCriteria_ProblemType);
            this.Controls.Add(this.pnlCriteria_ProblemType_Header);
            this.Controls.Add(this.pnl_Base);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModifyScheduleCriteria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modify Schedule";
            this.Load += new System.EventHandler(this.frmModifyScheduleCriteria_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlCriteria_ProblemType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ProblemType)).EndInit();
            this.pnlCriteria_ProblemType_Header.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.pnlCriteria_Resource.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Resource)).EndInit();
            this.pnlCriteria_Resource_Header.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnl_Base.ResumeLayout(false);
            this.pnl_Base.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Modify;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlCriteria_ProblemType;
        internal System.Windows.Forms.Label label54;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ProblemType;
        private System.Windows.Forms.Panel pnlCriteria_ProblemType_Header;
        private System.Windows.Forms.Label lblCriteria_ProviderProblemType_Header;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnlCriteria_Resource;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Resource;
        private System.Windows.Forms.Panel pnlCriteria_Resource_Header;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnl_Base;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblMessage;
        internal System.Windows.Forms.Label lblEndTime;
        internal System.Windows.Forms.Label lblStartTime;
        internal System.Windows.Forms.Label label16;
        internal System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        internal System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}