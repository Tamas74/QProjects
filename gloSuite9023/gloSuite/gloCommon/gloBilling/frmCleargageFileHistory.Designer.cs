namespace gloBilling
{
    partial class frmCleargageFileHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 

        private bool blnDisposed;
        private static frmCleargageFileHistory ofrmCleargageFileHistory;

        public static frmCleargageFileHistory GetInstance()
        {
            try
            {
                if (ofrmCleargageFileHistory == null)
                {
                    ofrmCleargageFileHistory = new frmCleargageFileHistory();
                }
            }
            finally { }
            return ofrmCleargageFileHistory;
        }

        protected override void Dispose(bool disposing)
        {
            if (!(this.blnDisposed))
            {
                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    if ((components != null))
                    {
                        components.Dispose();
                    }
                }
            }
            ofrmCleargageFileHistory = null;
            this.blnDisposed = true;
            base.Dispose(disposing);

            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            //base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        ~frmCleargageFileHistory()
        {
            Dispose(false);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCleargageFileHistory));
            this.pnl_Main = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlUploadedFileDetails = new System.Windows.Forms.Panel();
            this.c1UploadedFileDetails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label138 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlSearchUploadedFileDetails = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel24 = new System.Windows.Forms.Panel();
            this.txtSearchUploadedFileDetials = new System.Windows.Forms.TextBox();
            this.label131 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.btn_ClearC1UploadedFileDetails = new System.Windows.Forms.Button();
            this.label134 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.lblSearchUploadedFileDetails = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.splitter6 = new System.Windows.Forms.Splitter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlAllUploadedFiles = new System.Windows.Forms.Panel();
            this.c1AllUploadedFiles = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label160 = new System.Windows.Forms.Label();
            this.label161 = new System.Windows.Forms.Label();
            this.label162 = new System.Windows.Forms.Label();
            this.label163 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnltxtSearchAllUploadedFiles = new System.Windows.Forms.Panel();
            this.txtSearchAllUploadedFiles = new System.Windows.Forms.TextBox();
            this.label156 = new System.Windows.Forms.Label();
            this.label157 = new System.Windows.Forms.Label();
            this.btn_ClearC1AllUploadedFiles = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label158 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnllblCleargageUploadedFileValue = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblFileUploadedDateValue = new System.Windows.Forms.Label();
            this.lblFileUploadedDate = new System.Windows.Forms.Label();
            this.lblUploadedFilesCountValue = new System.Windows.Forms.Label();
            this.lblUploadedFiles = new System.Windows.Forms.Label();
            this.label152 = new System.Windows.Forms.Label();
            this.label153 = new System.Windows.Forms.Label();
            this.label154 = new System.Windows.Forms.Label();
            this.label155 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tls_Strip = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_View = new System.Windows.Forms.ToolStripButton();
            this.tsb_ViewAcknowledgement = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.C1SuperTooltipDx = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnl_Main.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlUploadedFileDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1UploadedFileDetails)).BeginInit();
            this.panel5.SuspendLayout();
            this.pnlSearchUploadedFileDetails.SuspendLayout();
            this.panel24.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlAllUploadedFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1AllUploadedFiles)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnltxtSearchAllUploadedFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnllblCleargageUploadedFileValue.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tls_Strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Main
            // 
            this.pnl_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Main.Controls.Add(this.panel3);
            this.pnl_Main.Controls.Add(this.splitter6);
            this.pnl_Main.Controls.Add(this.panel4);
            this.pnl_Main.Controls.Add(this.pnllblCleargageUploadedFileValue);
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Main.Location = new System.Drawing.Point(0, 53);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.Size = new System.Drawing.Size(1272, 937);
            this.pnl_Main.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel3.Controls.Add(this.pnlUploadedFileDetails);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel3.Location = new System.Drawing.Point(384, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 3, 3, 0);
            this.panel3.Size = new System.Drawing.Size(888, 908);
            this.panel3.TabIndex = 3;
            // 
            // pnlUploadedFileDetails
            // 
            this.pnlUploadedFileDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlUploadedFileDetails.Controls.Add(this.c1UploadedFileDetails);
            this.pnlUploadedFileDetails.Controls.Add(this.label138);
            this.pnlUploadedFileDetails.Controls.Add(this.label139);
            this.pnlUploadedFileDetails.Controls.Add(this.label140);
            this.pnlUploadedFileDetails.Controls.Add(this.label141);
            this.pnlUploadedFileDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUploadedFileDetails.Location = new System.Drawing.Point(0, 30);
            this.pnlUploadedFileDetails.Name = "pnlUploadedFileDetails";
            this.pnlUploadedFileDetails.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlUploadedFileDetails.Size = new System.Drawing.Size(885, 878);
            this.pnlUploadedFileDetails.TabIndex = 4;
            // 
            // c1UploadedFileDetails
            // 
            this.c1UploadedFileDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1UploadedFileDetails.BackColor = System.Drawing.Color.White;
            this.c1UploadedFileDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1UploadedFileDetails.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1UploadedFileDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1UploadedFileDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1UploadedFileDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1UploadedFileDetails.Location = new System.Drawing.Point(1, 4);
            this.c1UploadedFileDetails.Name = "c1UploadedFileDetails";
            this.c1UploadedFileDetails.Padding = new System.Windows.Forms.Padding(3);
            this.c1UploadedFileDetails.Rows.Count = 1;
            this.c1UploadedFileDetails.Rows.DefaultSize = 19;
            this.c1UploadedFileDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1UploadedFileDetails.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1UploadedFileDetails.Size = new System.Drawing.Size(883, 873);
            this.c1UploadedFileDetails.StyleInfo = resources.GetString("c1UploadedFileDetails.StyleInfo");
            this.c1UploadedFileDetails.TabIndex = 5;
            this.c1UploadedFileDetails.MouseLeave += new System.EventHandler(this.c1UploadedFileDetails_MouseLeave);
            this.c1UploadedFileDetails.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1UploadedFileDetails_MouseMove);
            // 
            // label138
            // 
            this.label138.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label138.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label138.Location = new System.Drawing.Point(1, 877);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(883, 1);
            this.label138.TabIndex = 6;
            // 
            // label139
            // 
            this.label139.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label139.Dock = System.Windows.Forms.DockStyle.Top;
            this.label139.Location = new System.Drawing.Point(1, 3);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(883, 1);
            this.label139.TabIndex = 7;
            // 
            // label140
            // 
            this.label140.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label140.Dock = System.Windows.Forms.DockStyle.Right;
            this.label140.Location = new System.Drawing.Point(884, 3);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(1, 875);
            this.label140.TabIndex = 8;
            // 
            // label141
            // 
            this.label141.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label141.Dock = System.Windows.Forms.DockStyle.Left;
            this.label141.Location = new System.Drawing.Point(0, 3);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(1, 875);
            this.label141.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel5.Controls.Add(this.pnlSearchUploadedFileDetails);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel5.Location = new System.Drawing.Point(0, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(885, 27);
            this.panel5.TabIndex = 10;
            // 
            // pnlSearchUploadedFileDetails
            // 
            this.pnlSearchUploadedFileDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearchUploadedFileDetails.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlSearchUploadedFileDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchUploadedFileDetails.Controls.Add(this.label6);
            this.pnlSearchUploadedFileDetails.Controls.Add(this.panel24);
            this.pnlSearchUploadedFileDetails.Controls.Add(this.lblSearchUploadedFileDetails);
            this.pnlSearchUploadedFileDetails.Controls.Add(this.label137);
            this.pnlSearchUploadedFileDetails.Controls.Add(this.label5);
            this.pnlSearchUploadedFileDetails.Controls.Add(this.label7);
            this.pnlSearchUploadedFileDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchUploadedFileDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearchUploadedFileDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlSearchUploadedFileDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchUploadedFileDetails.Name = "pnlSearchUploadedFileDetails";
            this.pnlSearchUploadedFileDetails.Size = new System.Drawing.Size(885, 27);
            this.pnlSearchUploadedFileDetails.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(884, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 25);
            this.label6.TabIndex = 12;
            // 
            // panel24
            // 
            this.panel24.BackColor = System.Drawing.Color.White;
            this.panel24.Controls.Add(this.txtSearchUploadedFileDetials);
            this.panel24.Controls.Add(this.label131);
            this.panel24.Controls.Add(this.label132);
            this.panel24.Controls.Add(this.label133);
            this.panel24.Controls.Add(this.btn_ClearC1UploadedFileDetails);
            this.panel24.Controls.Add(this.label134);
            this.panel24.Controls.Add(this.label135);
            this.panel24.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel24.ForeColor = System.Drawing.Color.Black;
            this.panel24.Location = new System.Drawing.Point(75, 1);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(212, 25);
            this.panel24.TabIndex = 13;
            // 
            // txtSearchUploadedFileDetials
            // 
            this.txtSearchUploadedFileDetials.BackColor = System.Drawing.Color.White;
            this.txtSearchUploadedFileDetials.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchUploadedFileDetials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchUploadedFileDetials.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchUploadedFileDetials.ForeColor = System.Drawing.Color.Black;
            this.txtSearchUploadedFileDetials.Location = new System.Drawing.Point(6, 3);
            this.txtSearchUploadedFileDetials.Name = "txtSearchUploadedFileDetials";
            this.txtSearchUploadedFileDetials.Size = new System.Drawing.Size(184, 15);
            this.txtSearchUploadedFileDetials.TabIndex = 4;
            this.txtSearchUploadedFileDetials.Tag = "Claim Manager";
            this.txtSearchUploadedFileDetials.TextChanged += new System.EventHandler(this.txtSearchUploadedFileDetials_TextChanged);
            // 
            // label131
            // 
            this.label131.BackColor = System.Drawing.Color.White;
            this.label131.Dock = System.Windows.Forms.DockStyle.Left;
            this.label131.Location = new System.Drawing.Point(1, 3);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(5, 17);
            this.label131.TabIndex = 14;
            // 
            // label132
            // 
            this.label132.BackColor = System.Drawing.Color.White;
            this.label132.Dock = System.Windows.Forms.DockStyle.Top;
            this.label132.Location = new System.Drawing.Point(1, 0);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(189, 3);
            this.label132.TabIndex = 15;
            // 
            // label133
            // 
            this.label133.BackColor = System.Drawing.Color.White;
            this.label133.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label133.Location = new System.Drawing.Point(1, 20);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(189, 5);
            this.label133.TabIndex = 16;
            // 
            // btn_ClearC1UploadedFileDetails
            // 
            this.btn_ClearC1UploadedFileDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ClearC1UploadedFileDetails.BackgroundImage")));
            this.btn_ClearC1UploadedFileDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ClearC1UploadedFileDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ClearC1UploadedFileDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_ClearC1UploadedFileDetails.FlatAppearance.BorderSize = 0;
            this.btn_ClearC1UploadedFileDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_ClearC1UploadedFileDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_ClearC1UploadedFileDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClearC1UploadedFileDetails.Image = ((System.Drawing.Image)(resources.GetObject("btn_ClearC1UploadedFileDetails.Image")));
            this.btn_ClearC1UploadedFileDetails.Location = new System.Drawing.Point(190, 0);
            this.btn_ClearC1UploadedFileDetails.Name = "btn_ClearC1UploadedFileDetails";
            this.btn_ClearC1UploadedFileDetails.Size = new System.Drawing.Size(21, 25);
            this.btn_ClearC1UploadedFileDetails.TabIndex = 5;
            this.btn_ClearC1UploadedFileDetails.UseVisualStyleBackColor = true;
            this.btn_ClearC1UploadedFileDetails.Click += new System.EventHandler(this.btn_ClearC1UploadedFileDetails_Click);
            // 
            // label134
            // 
            this.label134.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label134.Dock = System.Windows.Forms.DockStyle.Right;
            this.label134.Location = new System.Drawing.Point(211, 0);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(1, 25);
            this.label134.TabIndex = 17;
            this.label134.Text = "label4";
            // 
            // label135
            // 
            this.label135.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label135.Dock = System.Windows.Forms.DockStyle.Left;
            this.label135.Location = new System.Drawing.Point(0, 0);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(1, 25);
            this.label135.TabIndex = 18;
            this.label135.Text = "label4";
            // 
            // lblSearchUploadedFileDetails
            // 
            this.lblSearchUploadedFileDetails.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchUploadedFileDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearchUploadedFileDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchUploadedFileDetails.Location = new System.Drawing.Point(1, 1);
            this.lblSearchUploadedFileDetails.Name = "lblSearchUploadedFileDetails";
            this.lblSearchUploadedFileDetails.Size = new System.Drawing.Size(74, 25);
            this.lblSearchUploadedFileDetails.TabIndex = 19;
            this.lblSearchUploadedFileDetails.Text = "Search  :";
            this.lblSearchUploadedFileDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label137
            // 
            this.label137.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label137.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label137.Location = new System.Drawing.Point(1, 26);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(884, 1);
            this.label137.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(884, 1);
            this.label5.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 27);
            this.label7.TabIndex = 22;
            // 
            // splitter6
            // 
            this.splitter6.Location = new System.Drawing.Point(381, 0);
            this.splitter6.Name = "splitter6";
            this.splitter6.Size = new System.Drawing.Size(3, 908);
            this.splitter6.TabIndex = 23;
            this.splitter6.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pnlAllUploadedFiles);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.panel4.Size = new System.Drawing.Size(381, 908);
            this.panel4.TabIndex = 24;
            // 
            // pnlAllUploadedFiles
            // 
            this.pnlAllUploadedFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlAllUploadedFiles.Controls.Add(this.c1AllUploadedFiles);
            this.pnlAllUploadedFiles.Controls.Add(this.label160);
            this.pnlAllUploadedFiles.Controls.Add(this.label161);
            this.pnlAllUploadedFiles.Controls.Add(this.label162);
            this.pnlAllUploadedFiles.Controls.Add(this.label163);
            this.pnlAllUploadedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAllUploadedFiles.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAllUploadedFiles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlAllUploadedFiles.Location = new System.Drawing.Point(3, 30);
            this.pnlAllUploadedFiles.Name = "pnlAllUploadedFiles";
            this.pnlAllUploadedFiles.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlAllUploadedFiles.Size = new System.Drawing.Size(378, 878);
            this.pnlAllUploadedFiles.TabIndex = 25;
            // 
            // c1AllUploadedFiles
            // 
            this.c1AllUploadedFiles.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1AllUploadedFiles.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1AllUploadedFiles.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1AllUploadedFiles.BackColor = System.Drawing.Color.White;
            this.c1AllUploadedFiles.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1AllUploadedFiles.ColumnInfo = "1,0,0,0,0,95,Columns:0{Width:33;Name:\"COL_IMAGE\";Style:\"TextAlign:CenterCenter;\";" +
    "StyleFixed:\"ImageAlign:LeftCenter;\";}\t";
            this.c1AllUploadedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1AllUploadedFiles.ExtendLastCol = true;
            this.c1AllUploadedFiles.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1AllUploadedFiles.ForeColor = System.Drawing.Color.Black;
            this.c1AllUploadedFiles.Location = new System.Drawing.Point(1, 4);
            this.c1AllUploadedFiles.Name = "c1AllUploadedFiles";
            this.c1AllUploadedFiles.Rows.Count = 0;
            this.c1AllUploadedFiles.Rows.DefaultSize = 19;
            this.c1AllUploadedFiles.Rows.Fixed = 0;
            this.c1AllUploadedFiles.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1AllUploadedFiles.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1AllUploadedFiles.Size = new System.Drawing.Size(376, 873);
            this.c1AllUploadedFiles.StyleInfo = resources.GetString("c1AllUploadedFiles.StyleInfo");
            this.c1AllUploadedFiles.TabIndex = 3;
            this.c1AllUploadedFiles.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            this.c1AllUploadedFiles.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1AllUploadedFiles_AfterSelChange);
            this.c1AllUploadedFiles.MouseLeave += new System.EventHandler(this.c1AllUploadedFiles_MouseLeave);
            this.c1AllUploadedFiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1AllUploadedFiles_MouseMove);
            // 
            // label160
            // 
            this.label160.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label160.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label160.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label160.Location = new System.Drawing.Point(1, 877);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(376, 1);
            this.label160.TabIndex = 27;
            this.label160.Text = "label2";
            // 
            // label161
            // 
            this.label161.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label161.Dock = System.Windows.Forms.DockStyle.Left;
            this.label161.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label161.Location = new System.Drawing.Point(0, 4);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(1, 874);
            this.label161.TabIndex = 28;
            this.label161.Text = "label4";
            // 
            // label162
            // 
            this.label162.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label162.Dock = System.Windows.Forms.DockStyle.Right;
            this.label162.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label162.Location = new System.Drawing.Point(377, 4);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(1, 874);
            this.label162.TabIndex = 29;
            this.label162.Text = "label3";
            // 
            // label163
            // 
            this.label163.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label163.Dock = System.Windows.Forms.DockStyle.Top;
            this.label163.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label163.Location = new System.Drawing.Point(0, 3);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(378, 1);
            this.label163.TabIndex = 30;
            this.label163.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pnltxtSearchAllUploadedFiles);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(378, 27);
            this.panel2.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 1);
            this.label1.TabIndex = 32;
            this.label1.Text = "label2";
            // 
            // pnltxtSearchAllUploadedFiles
            // 
            this.pnltxtSearchAllUploadedFiles.BackColor = System.Drawing.Color.White;
            this.pnltxtSearchAllUploadedFiles.Controls.Add(this.txtSearchAllUploadedFiles);
            this.pnltxtSearchAllUploadedFiles.Controls.Add(this.label156);
            this.pnltxtSearchAllUploadedFiles.Controls.Add(this.label157);
            this.pnltxtSearchAllUploadedFiles.Controls.Add(this.btn_ClearC1AllUploadedFiles);
            this.pnltxtSearchAllUploadedFiles.Controls.Add(this.pictureBox2);
            this.pnltxtSearchAllUploadedFiles.Controls.Add(this.label158);
            this.pnltxtSearchAllUploadedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnltxtSearchAllUploadedFiles.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnltxtSearchAllUploadedFiles.ForeColor = System.Drawing.Color.Black;
            this.pnltxtSearchAllUploadedFiles.Location = new System.Drawing.Point(1, 1);
            this.pnltxtSearchAllUploadedFiles.Name = "pnltxtSearchAllUploadedFiles";
            this.pnltxtSearchAllUploadedFiles.Size = new System.Drawing.Size(376, 26);
            this.pnltxtSearchAllUploadedFiles.TabIndex = 33;
            // 
            // txtSearchAllUploadedFiles
            // 
            this.txtSearchAllUploadedFiles.BackColor = System.Drawing.SystemColors.Window;
            this.txtSearchAllUploadedFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchAllUploadedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchAllUploadedFiles.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchAllUploadedFiles.ForeColor = System.Drawing.Color.Black;
            this.txtSearchAllUploadedFiles.Location = new System.Drawing.Point(28, 3);
            this.txtSearchAllUploadedFiles.Name = "txtSearchAllUploadedFiles";
            this.txtSearchAllUploadedFiles.Size = new System.Drawing.Size(327, 15);
            this.txtSearchAllUploadedFiles.TabIndex = 1;
            this.txtSearchAllUploadedFiles.Tag = "Claim Manager";
            this.txtSearchAllUploadedFiles.TextChanged += new System.EventHandler(this.txtSearchAllUploadedFiles_TextChanged);
            // 
            // label156
            // 
            this.label156.BackColor = System.Drawing.Color.White;
            this.label156.Dock = System.Windows.Forms.DockStyle.Top;
            this.label156.Location = new System.Drawing.Point(28, 0);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(327, 3);
            this.label156.TabIndex = 34;
            // 
            // label157
            // 
            this.label157.BackColor = System.Drawing.Color.White;
            this.label157.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label157.Location = new System.Drawing.Point(28, 20);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(327, 5);
            this.label157.TabIndex = 35;
            // 
            // btn_ClearC1AllUploadedFiles
            // 
            this.btn_ClearC1AllUploadedFiles.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ClearC1AllUploadedFiles.BackgroundImage")));
            this.btn_ClearC1AllUploadedFiles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ClearC1AllUploadedFiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ClearC1AllUploadedFiles.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_ClearC1AllUploadedFiles.FlatAppearance.BorderSize = 0;
            this.btn_ClearC1AllUploadedFiles.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_ClearC1AllUploadedFiles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_ClearC1AllUploadedFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClearC1AllUploadedFiles.Image = ((System.Drawing.Image)(resources.GetObject("btn_ClearC1AllUploadedFiles.Image")));
            this.btn_ClearC1AllUploadedFiles.Location = new System.Drawing.Point(355, 0);
            this.btn_ClearC1AllUploadedFiles.Name = "btn_ClearC1AllUploadedFiles";
            this.btn_ClearC1AllUploadedFiles.Size = new System.Drawing.Size(21, 25);
            this.btn_ClearC1AllUploadedFiles.TabIndex = 2;
            this.btn_ClearC1AllUploadedFiles.UseVisualStyleBackColor = true;
            this.btn_ClearC1AllUploadedFiles.Click += new System.EventHandler(this.btn_ClearC1AllUploadedFiles_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(28, 25);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // label158
            // 
            this.label158.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label158.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label158.Location = new System.Drawing.Point(0, 25);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(376, 1);
            this.label158.TabIndex = 36;
            this.label158.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 26);
            this.label2.TabIndex = 37;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(377, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 26);
            this.label3.TabIndex = 38;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(378, 1);
            this.label4.TabIndex = 39;
            this.label4.Text = "label1";
            // 
            // pnllblCleargageUploadedFileValue
            // 
            this.pnllblCleargageUploadedFileValue.BackColor = System.Drawing.Color.Transparent;
            this.pnllblCleargageUploadedFileValue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnllblCleargageUploadedFileValue.Controls.Add(this.panel6);
            this.pnllblCleargageUploadedFileValue.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnllblCleargageUploadedFileValue.Location = new System.Drawing.Point(0, 908);
            this.pnllblCleargageUploadedFileValue.Name = "pnllblCleargageUploadedFileValue";
            this.pnllblCleargageUploadedFileValue.Padding = new System.Windows.Forms.Padding(3);
            this.pnllblCleargageUploadedFileValue.Size = new System.Drawing.Size(1272, 29);
            this.pnllblCleargageUploadedFileValue.TabIndex = 40;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.lblFileUploadedDateValue);
            this.panel6.Controls.Add(this.lblFileUploadedDate);
            this.panel6.Controls.Add(this.lblUploadedFilesCountValue);
            this.panel6.Controls.Add(this.lblUploadedFiles);
            this.panel6.Controls.Add(this.label152);
            this.panel6.Controls.Add(this.label153);
            this.panel6.Controls.Add(this.label154);
            this.panel6.Controls.Add(this.label155);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1266, 23);
            this.panel6.TabIndex = 41;
            // 
            // lblFileUploadedDateValue
            // 
            this.lblFileUploadedDateValue.AutoEllipsis = true;
            this.lblFileUploadedDateValue.AutoSize = true;
            this.lblFileUploadedDateValue.BackColor = System.Drawing.Color.Transparent;
            this.lblFileUploadedDateValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFileUploadedDateValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileUploadedDateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblFileUploadedDateValue.Location = new System.Drawing.Point(276, 1);
            this.lblFileUploadedDateValue.Name = "lblFileUploadedDateValue";
            this.lblFileUploadedDateValue.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblFileUploadedDateValue.Size = new System.Drawing.Size(36, 17);
            this.lblFileUploadedDateValue.TabIndex = 42;
            this.lblFileUploadedDateValue.Text = "Date";
            // 
            // lblFileUploadedDate
            // 
            this.lblFileUploadedDate.AutoSize = true;
            this.lblFileUploadedDate.BackColor = System.Drawing.Color.Transparent;
            this.lblFileUploadedDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFileUploadedDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileUploadedDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblFileUploadedDate.Location = new System.Drawing.Point(135, 1);
            this.lblFileUploadedDate.Name = "lblFileUploadedDate";
            this.lblFileUploadedDate.Padding = new System.Windows.Forms.Padding(20, 3, 0, 0);
            this.lblFileUploadedDate.Size = new System.Drawing.Size(141, 17);
            this.lblFileUploadedDate.TabIndex = 43;
            this.lblFileUploadedDate.Text = "File Uploaded Date : ";
            this.lblFileUploadedDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUploadedFilesCountValue
            // 
            this.lblUploadedFilesCountValue.AutoEllipsis = true;
            this.lblUploadedFilesCountValue.AutoSize = true;
            this.lblUploadedFilesCountValue.BackColor = System.Drawing.Color.Transparent;
            this.lblUploadedFilesCountValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUploadedFilesCountValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblUploadedFilesCountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblUploadedFilesCountValue.Location = new System.Drawing.Point(109, 1);
            this.lblUploadedFilesCountValue.Name = "lblUploadedFilesCountValue";
            this.lblUploadedFilesCountValue.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblUploadedFilesCountValue.Size = new System.Drawing.Size(26, 17);
            this.lblUploadedFilesCountValue.TabIndex = 44;
            this.lblUploadedFilesCountValue.Text = "File";
            // 
            // lblUploadedFiles
            // 
            this.lblUploadedFiles.AutoEllipsis = true;
            this.lblUploadedFiles.BackColor = System.Drawing.Color.Transparent;
            this.lblUploadedFiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUploadedFiles.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadedFiles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblUploadedFiles.Location = new System.Drawing.Point(1, 1);
            this.lblUploadedFiles.Name = "lblUploadedFiles";
            this.lblUploadedFiles.Size = new System.Drawing.Size(108, 21);
            this.lblUploadedFiles.TabIndex = 45;
            this.lblUploadedFiles.Text = "No. of Files : ";
            this.lblUploadedFiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label152
            // 
            this.label152.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label152.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label152.Location = new System.Drawing.Point(1, 22);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(1264, 1);
            this.label152.TabIndex = 46;
            // 
            // label153
            // 
            this.label153.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label153.Dock = System.Windows.Forms.DockStyle.Top;
            this.label153.Location = new System.Drawing.Point(1, 0);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(1264, 1);
            this.label153.TabIndex = 47;
            // 
            // label154
            // 
            this.label154.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label154.Dock = System.Windows.Forms.DockStyle.Right;
            this.label154.Location = new System.Drawing.Point(1265, 0);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(1, 23);
            this.label154.TabIndex = 48;
            // 
            // label155
            // 
            this.label155.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label155.Dock = System.Windows.Forms.DockStyle.Left;
            this.label155.Location = new System.Drawing.Point(0, 0);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(1, 23);
            this.label155.TabIndex = 49;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.tls_Strip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1272, 53);
            this.panel1.TabIndex = 50;
            // 
            // tls_Strip
            // 
            this.tls_Strip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Strip.BackgroundImage")));
            this.tls_Strip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Strip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Strip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_View,
            this.tsb_ViewAcknowledgement,
            this.tsb_Refresh,
            this.tsb_Close});
            this.tls_Strip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Strip.Location = new System.Drawing.Point(0, 0);
            this.tls_Strip.Name = "tls_Strip";
            this.tls_Strip.Size = new System.Drawing.Size(1272, 53);
            this.tls_Strip.TabIndex = 0;
            this.tls_Strip.Text = "toolStrip1";
            // 
            // tsb_View
            // 
            this.tsb_View.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_View.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_View.Image = ((System.Drawing.Image)(resources.GetObject("tsb_View.Image")));
            this.tsb_View.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_View.Name = "tsb_View";
            this.tsb_View.Size = new System.Drawing.Size(40, 50);
            this.tsb_View.Tag = "View";
            this.tsb_View.Text = "&View";
            this.tsb_View.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_View.Click += new System.EventHandler(this.tsb_View_Click);
            // 
            // tsb_ViewAcknowledgement
            // 
            this.tsb_ViewAcknowledgement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ViewAcknowledgement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ViewAcknowledgement.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ViewAcknowledgement.Image")));
            this.tsb_ViewAcknowledgement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ViewAcknowledgement.Name = "tsb_ViewAcknowledgement";
            this.tsb_ViewAcknowledgement.Size = new System.Drawing.Size(158, 50);
            this.tsb_ViewAcknowledgement.Tag = "View";
            this.tsb_ViewAcknowledgement.Text = "View &Acknowledgement";
            this.tsb_ViewAcknowledgement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ViewAcknowledgement.Click += new System.EventHandler(this.tsb_ViewAcknowledgement_Click);
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Refresh.Image")));
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tsb_Refresh.Tag = "Refresh";
            this.tsb_Refresh.Text = "&Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refresh.Click += new System.EventHandler(this.tsb_Refresh_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // C1SuperTooltipDx
            // 
            this.C1SuperTooltipDx.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltipDx.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmCleargageFileHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1272, 990);
            this.Controls.Add(this.pnl_Main);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCleargageFileHistory";
            this.Text = "Cleargage File History";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCleargageFileHistory_FormClosed);
            this.Load += new System.EventHandler(this.frmCleargageFileHistory_Load);
            this.pnl_Main.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlUploadedFileDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1UploadedFileDetails)).EndInit();
            this.panel5.ResumeLayout(false);
            this.pnlSearchUploadedFileDetails.ResumeLayout(false);
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.pnlAllUploadedFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1AllUploadedFiles)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnltxtSearchAllUploadedFiles.ResumeLayout(false);
            this.pnltxtSearchAllUploadedFiles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnllblCleargageUploadedFileValue.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tls_Strip.ResumeLayout(false);
            this.tls_Strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Main;
        private System.Windows.Forms.Panel panel1;
        private gloGlobal.gloToolStripIgnoreFocus tls_Strip;
        private System.Windows.Forms.ToolStripButton tsb_View;
        private System.Windows.Forms.ToolStripButton tsb_ViewAcknowledgement;
        private System.Windows.Forms.Panel pnllblCleargageUploadedFileValue;
        private System.Windows.Forms.Label lblUploadedFilesCountValue;
        private System.Windows.Forms.Label lblUploadedFiles;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.Label label154;
        private System.Windows.Forms.Label label155;
        private System.Windows.Forms.Panel pnlUploadedFileDetails;
        private C1.Win.C1FlexGrid.C1FlexGrid c1UploadedFileDetails;
        private System.Windows.Forms.Panel pnlSearchUploadedFileDetails;
        internal System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.TextBox txtSearchUploadedFileDetials;
        internal System.Windows.Forms.Label label131;
        internal System.Windows.Forms.Label label132;
        internal System.Windows.Forms.Label label133;
        internal System.Windows.Forms.Button btn_ClearC1UploadedFileDetails;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Label lblSearchUploadedFileDetails;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Panel pnlAllUploadedFiles;
        private C1.Win.C1FlexGrid.C1FlexGrid c1AllUploadedFiles;
        internal System.Windows.Forms.Panel pnltxtSearchAllUploadedFiles;
        private System.Windows.Forms.TextBox txtSearchAllUploadedFiles;
        internal System.Windows.Forms.Label label156;
        internal System.Windows.Forms.Label label157;
        internal System.Windows.Forms.Button btn_ClearC1AllUploadedFiles;
        internal System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.Label label160;
        private System.Windows.Forms.Label label161;
        private System.Windows.Forms.Label label162;
        private System.Windows.Forms.Label label163;
        private System.Windows.Forms.Label lblFileUploadedDateValue;
        private System.Windows.Forms.Label lblFileUploadedDate;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltipDx;
        private System.Windows.Forms.ToolStripButton tsb_Refresh;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Splitter splitter6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label7;
    }
}