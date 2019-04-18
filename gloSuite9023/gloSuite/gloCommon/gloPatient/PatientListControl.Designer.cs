namespace gloPatient
{
    partial class PatientListControl
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtPatientOn };
            System.Windows.Forms.Control[] cntControls = { dtPatientOn };
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
                try
                {
                    if (oTimer != null)
                    {
                        oTimer.Tick -= new System.EventHandler(this.oTimer_Tick);
                        oTimer.Dispose();
                        oTimer = null;
                    }
                }
                catch
                {
                }
                if (toolTip != null)
                {
                    toolTip.Dispose();
                    toolTip = null;
                }

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientListControl));
            this.dgPatientView = new System.Windows.Forms.DataGridView();
            this.chkInstringSearch = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnrecpat = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.Label77 = new System.Windows.Forms.Label();
            this.picSearch = new System.Windows.Forms.PictureBox();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.btnSearchClose = new System.Windows.Forms.Button();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbProviders = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlPatientON = new System.Windows.Forms.Panel();
            this.dtPatientOn = new System.Windows.Forms.DateTimePicker();
            this.chkPatientOn = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUnmask = new System.Windows.Forms.Button();
            this.btnMask = new System.Windows.Forms.Button();
            this.btnNewPatientReg = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnGetAllPatients = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAdvanceSearch = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.toolTipMask = new System.Windows.Forms.ToolTip(this.components);
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsbtn_Save = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pn_CustomList = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatientView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).BeginInit();
            this.panel3.SuspendLayout();
            this.pnlPatientON.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pn_CustomList.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgPatientView
            // 
            this.dgPatientView.AllowUserToAddRows = false;
            this.dgPatientView.AllowUserToDeleteRows = false;
            this.dgPatientView.AllowUserToOrderColumns = true;
            this.dgPatientView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgPatientView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPatientView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgPatientView.BackgroundColor = System.Drawing.Color.White;
            this.dgPatientView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(126)))), ((int)(((byte)(211)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPatientView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgPatientView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPatientView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgPatientView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPatientView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgPatientView.EnableHeadersVisualStyles = false;
            this.dgPatientView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(181)))), ((int)(((byte)(221)))));
            this.dgPatientView.Location = new System.Drawing.Point(0, 4);
            this.dgPatientView.MultiSelect = false;
            this.dgPatientView.Name = "dgPatientView";
            this.dgPatientView.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(231)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPatientView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgPatientView.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(231)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgPatientView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgPatientView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPatientView.Size = new System.Drawing.Size(831, 340);
            this.dgPatientView.TabIndex = 0;
            this.dgPatientView.TabStop = false;
            this.dgPatientView.Tag = "PatientList";
            this.dgPatientView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPatientView_CellClick);
            this.dgPatientView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPatientView_CellContentClick);
            this.dgPatientView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPatientView_CellDoubleClick);
            this.dgPatientView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgPatientView_CellFormatting);
            this.dgPatientView.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgPatientView_ColumnDisplayIndexChanged);
            this.dgPatientView.Sorted += new System.EventHandler(this.dgPatientView_Sorted);
            this.dgPatientView.Click += new System.EventHandler(this.dgPatientView_Click);
            this.dgPatientView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgPatientView_KeyDown);
            this.dgPatientView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgPatientView_MouseDown);
            // 
            // chkInstringSearch
            // 
            this.chkInstringSearch.AutoSize = true;
            this.chkInstringSearch.BackColor = System.Drawing.Color.Transparent;
            this.chkInstringSearch.Checked = true;
            this.chkInstringSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInstringSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInstringSearch.Location = new System.Drawing.Point(53, 86);
            this.chkInstringSearch.Name = "chkInstringSearch";
            this.chkInstringSearch.Size = new System.Drawing.Size(108, 18);
            this.chkInstringSearch.TabIndex = 22;
            this.chkInstringSearch.Text = "General Search";
            this.chkInstringSearch.UseVisualStyleBackColor = false;
            this.chkInstringSearch.Visible = false;
            this.chkInstringSearch.CheckedChanged += new System.EventHandler(this.chkInstantSearch_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.dgPatientView);
            this.panel1.Controls.Add(this.chkInstringSearch);
            this.panel1.Controls.Add(this.lbl_TopBrd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 111);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel1.Size = new System.Drawing.Size(831, 344);
            this.panel1.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(830, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 339);
            this.label16.TabIndex = 25;
            this.label16.Text = "label1";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 339);
            this.label15.TabIndex = 24;
            this.label15.Text = "label1";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 343);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(831, 1);
            this.label14.TabIndex = 23;
            this.label14.Text = "label1";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(0, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(831, 1);
            this.lbl_TopBrd.TabIndex = 5;
            this.lbl_TopBrd.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.pnlSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 85);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel2.Size = new System.Drawing.Size(831, 26);
            this.panel2.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(830, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 23);
            this.label13.TabIndex = 8;
            this.label13.Text = "label4";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlSearch.BackgroundImage")));
            this.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearch.Controls.Add(this.btnrecpat);
            this.pnlSearch.Controls.Add(this.label19);
            this.pnlSearch.Controls.Add(this.panel4);
            this.pnlSearch.Controls.Add(this.panel3);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.pnlPatientON);
            this.pnlSearch.Controls.Add(this.btnNewPatientReg);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.btnGetAllPatients);
            this.pnlSearch.Controls.Add(this.label11);
            this.pnlSearch.Controls.Add(this.btnRefresh);
            this.pnlSearch.Controls.Add(this.label10);
            this.pnlSearch.Controls.Add(this.btnAdvanceSearch);
            this.pnlSearch.Controls.Add(this.label9);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Controls.Add(this.label7);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label12);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(0, 3);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(831, 23);
            this.pnlSearch.TabIndex = 0;
            // 
            // btnrecpat
            // 
            this.btnrecpat.BackColor = System.Drawing.Color.Transparent;
            this.btnrecpat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrecpat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnrecpat.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnrecpat.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnrecpat.FlatAppearance.BorderSize = 0;
            this.btnrecpat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Turquoise;
            this.btnrecpat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnrecpat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnrecpat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrecpat.Image = ((System.Drawing.Image)(resources.GetObject("btnrecpat.Image")));
            this.btnrecpat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnrecpat.Location = new System.Drawing.Point(268, 1);
            this.btnrecpat.Name = "btnrecpat";
            this.btnrecpat.Size = new System.Drawing.Size(25, 21);
            this.btnrecpat.TabIndex = 45;
            this.btnrecpat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTipMask.SetToolTip(this.btnrecpat, "View Recent Patients");
            this.btnrecpat.UseVisualStyleBackColor = false;
            this.btnrecpat.Click += new System.EventHandler(this.btnrecpat_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.txtSearch);
            this.panel4.Controls.Add(this.lbl_WhiteSpaceTop);
            this.panel4.Controls.Add(this.Label77);
            this.panel4.Controls.Add(this.picSearch);
            this.panel4.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.panel4.Controls.Add(this.btnSearchClose);
            this.panel4.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.panel4.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(57, 1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(206, 21);
            this.panel4.TabIndex = 44;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(5, 4);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(163, 14);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(5, 1);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(163, 3);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // Label77
            // 
            this.Label77.BackColor = System.Drawing.Color.White;
            this.Label77.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label77.Location = new System.Drawing.Point(5, 15);
            this.Label77.Name = "Label77";
            this.Label77.Size = new System.Drawing.Size(163, 5);
            this.Label77.TabIndex = 43;
            // 
            // picSearch
            // 
            this.picSearch.BackColor = System.Drawing.Color.White;
            this.picSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.picSearch.Image = ((System.Drawing.Image)(resources.GetObject("picSearch.Image")));
            this.picSearch.Location = new System.Drawing.Point(168, 1);
            this.picSearch.Name = "picSearch";
            this.picSearch.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.picSearch.Size = new System.Drawing.Size(16, 19);
            this.picSearch.TabIndex = 30;
            this.picSearch.TabStop = false;
            this.picSearch.Visible = false;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(1, 1);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(4, 19);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // btnSearchClose
            // 
            this.btnSearchClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchClose.BackgroundImage")));
            this.btnSearchClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearchClose.FlatAppearance.BorderSize = 0;
            this.btnSearchClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearchClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearchClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchClose.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchClose.Image")));
            this.btnSearchClose.Location = new System.Drawing.Point(184, 1);
            this.btnSearchClose.Name = "btnSearchClose";
            this.btnSearchClose.Size = new System.Drawing.Size(21, 19);
            this.btnSearchClose.TabIndex = 41;
            this.btnSearchClose.UseVisualStyleBackColor = true;
            this.btnSearchClose.Click += new System.EventHandler(this.btnSearchClose_Click);
            // 
            // lbl_pnlSearchLeftBrd
            // 
            this.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd";
            this.lbl_pnlSearchLeftBrd.Size = new System.Drawing.Size(1, 19);
            this.lbl_pnlSearchLeftBrd.TabIndex = 39;
            this.lbl_pnlSearchLeftBrd.Text = "label4";
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(205, 1);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 19);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(206, 1);
            this.label17.TabIndex = 44;
            this.label17.Text = "label1";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 20);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(206, 1);
            this.label18.TabIndex = 45;
            this.label18.Text = "label1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.cmbProviders);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(293, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(236, 21);
            this.panel3.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(-9, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 29;
            this.label3.Text = "Provider ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProviders
            // 
            this.cmbProviders.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProviders.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProviders.FormattingEnabled = true;
            this.cmbProviders.Location = new System.Drawing.Point(65, 0);
            this.cmbProviders.Name = "cmbProviders";
            this.cmbProviders.Size = new System.Drawing.Size(171, 21);
            this.cmbProviders.TabIndex = 0;
            this.cmbProviders.SelectedIndexChanged += new System.EventHandler(this.cmbProviders_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(53, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(4, 21);
            this.label2.TabIndex = 29;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPatientON
            // 
            this.pnlPatientON.BackColor = System.Drawing.Color.Transparent;
            this.pnlPatientON.Controls.Add(this.dtPatientOn);
            this.pnlPatientON.Controls.Add(this.chkPatientOn);
            this.pnlPatientON.Controls.Add(this.label6);
            this.pnlPatientON.Controls.Add(this.btnUnmask);
            this.pnlPatientON.Controls.Add(this.btnMask);
            this.pnlPatientON.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPatientON.Location = new System.Drawing.Point(529, 1);
            this.pnlPatientON.Name = "pnlPatientON";
            this.pnlPatientON.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.pnlPatientON.Size = new System.Drawing.Size(210, 21);
            this.pnlPatientON.TabIndex = 2;
            this.pnlPatientON.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlPatientON_MouseDown);
            // 
            // dtPatientOn
            // 
            this.dtPatientOn.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtPatientOn.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtPatientOn.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtPatientOn.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtPatientOn.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtPatientOn.CustomFormat = "MM/dd/yyyy";
            this.dtPatientOn.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtPatientOn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPatientOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPatientOn.Location = new System.Drawing.Point(99, 0);
            this.dtPatientOn.Name = "dtPatientOn";
            this.dtPatientOn.Size = new System.Drawing.Size(90, 21);
            this.dtPatientOn.TabIndex = 1;
            this.dtPatientOn.ValueChanged += new System.EventHandler(this.dtPatientOn_ValueChanged);
            // 
            // chkPatientOn
            // 
            this.chkPatientOn.AutoSize = true;
            this.chkPatientOn.BackColor = System.Drawing.Color.Transparent;
            this.chkPatientOn.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkPatientOn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPatientOn.Location = new System.Drawing.Point(8, 0);
            this.chkPatientOn.Name = "chkPatientOn";
            this.chkPatientOn.Size = new System.Drawing.Size(91, 21);
            this.chkPatientOn.TabIndex = 0;
            this.chkPatientOn.Text = "Patients On";
            this.chkPatientOn.UseVisualStyleBackColor = false;
            this.chkPatientOn.CheckedChanged += new System.EventHandler(this.chkPatientOn_CheckedChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(157, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(3, 21);
            this.label6.TabIndex = 32;
            // 
            // btnUnmask
            // 
            this.btnUnmask.BackColor = System.Drawing.Color.Transparent;
            this.btnUnmask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUnmask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnmask.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUnmask.FlatAppearance.BorderSize = 0;
            this.btnUnmask.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUnmask.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUnmask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnmask.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnmask.Image = ((System.Drawing.Image)(resources.GetObject("btnUnmask.Image")));
            this.btnUnmask.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUnmask.Location = new System.Drawing.Point(160, 0);
            this.btnUnmask.Name = "btnUnmask";
            this.btnUnmask.Size = new System.Drawing.Size(25, 21);
            this.btnUnmask.TabIndex = 31;
            this.btnUnmask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTipMask.SetToolTip(this.btnUnmask, "Unmask");
            this.btnUnmask.UseVisualStyleBackColor = false;
            this.btnUnmask.Visible = false;
            this.btnUnmask.Click += new System.EventHandler(this.btnUnmask_Click);
            this.btnUnmask.MouseLeave += new System.EventHandler(this.btnUnmask_MouseLeave);
            this.btnUnmask.MouseHover += new System.EventHandler(this.btnUnmask_MouseHover);
            // 
            // btnMask
            // 
            this.btnMask.BackColor = System.Drawing.Color.Transparent;
            this.btnMask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMask.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMask.FlatAppearance.BorderSize = 0;
            this.btnMask.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMask.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMask.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMask.Image = ((System.Drawing.Image)(resources.GetObject("btnMask.Image")));
            this.btnMask.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMask.Location = new System.Drawing.Point(185, 0);
            this.btnMask.Name = "btnMask";
            this.btnMask.Size = new System.Drawing.Size(25, 21);
            this.btnMask.TabIndex = 30;
            this.btnMask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTipMask.SetToolTip(this.btnMask, "Mask");
            this.btnMask.UseVisualStyleBackColor = false;
            this.btnMask.Click += new System.EventHandler(this.btnMask_Click);
            this.btnMask.MouseLeave += new System.EventHandler(this.btnMask_MouseLeave);
            this.btnMask.MouseHover += new System.EventHandler(this.btnMask_MouseHover);
            // 
            // btnNewPatientReg
            // 
            this.btnNewPatientReg.BackColor = System.Drawing.Color.Transparent;
            this.btnNewPatientReg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewPatientReg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewPatientReg.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNewPatientReg.FlatAppearance.BorderSize = 0;
            this.btnNewPatientReg.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNewPatientReg.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNewPatientReg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewPatientReg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewPatientReg.Image = ((System.Drawing.Image)(resources.GetObject("btnNewPatientReg.Image")));
            this.btnNewPatientReg.Location = new System.Drawing.Point(739, 1);
            this.btnNewPatientReg.Name = "btnNewPatientReg";
            this.btnNewPatientReg.Size = new System.Drawing.Size(21, 21);
            this.btnNewPatientReg.TabIndex = 3;
            this.btnNewPatientReg.UseVisualStyleBackColor = false;
            this.btnNewPatientReg.Visible = false;
            this.btnNewPatientReg.Click += new System.EventHandler(this.btnNewPatientReg_Click);
            this.btnNewPatientReg.MouseLeave += new System.EventHandler(this.btnNewPatientReg_MouseLeave);
            this.btnNewPatientReg.MouseHover += new System.EventHandler(this.btnNewPatientReg_MouseHover);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(760, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(2, 21);
            this.label8.TabIndex = 16;
            // 
            // btnGetAllPatients
            // 
            this.btnGetAllPatients.BackColor = System.Drawing.Color.Transparent;
            this.btnGetAllPatients.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGetAllPatients.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetAllPatients.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGetAllPatients.FlatAppearance.BorderSize = 0;
            this.btnGetAllPatients.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnGetAllPatients.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnGetAllPatients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetAllPatients.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAllPatients.Image = ((System.Drawing.Image)(resources.GetObject("btnGetAllPatients.Image")));
            this.btnGetAllPatients.Location = new System.Drawing.Point(762, 1);
            this.btnGetAllPatients.Name = "btnGetAllPatients";
            this.btnGetAllPatients.Size = new System.Drawing.Size(21, 21);
            this.btnGetAllPatients.TabIndex = 4;
            this.btnGetAllPatients.UseVisualStyleBackColor = false;
            this.btnGetAllPatients.Visible = false;
            this.btnGetAllPatients.Click += new System.EventHandler(this.btnGetAllPatients_Click);
            this.btnGetAllPatients.MouseLeave += new System.EventHandler(this.btnGetAllPatients_MouseLeave);
            this.btnGetAllPatients.MouseHover += new System.EventHandler(this.btnGetAllPatients_MouseHover);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Location = new System.Drawing.Point(783, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(2, 21);
            this.label11.TabIndex = 12;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(785, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(21, 21);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.MouseLeave += new System.EventHandler(this.btnRefresh_MouseLeave);
            this.btnRefresh.MouseHover += new System.EventHandler(this.btnRefresh_MouseHover);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Location = new System.Drawing.Point(806, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(2, 21);
            this.label10.TabIndex = 10;
            // 
            // btnAdvanceSearch
            // 
            this.btnAdvanceSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnAdvanceSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdvanceSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdvanceSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdvanceSearch.FlatAppearance.BorderSize = 0;
            this.btnAdvanceSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdvanceSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdvanceSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdvanceSearch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdvanceSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnAdvanceSearch.Image")));
            this.btnAdvanceSearch.Location = new System.Drawing.Point(808, 1);
            this.btnAdvanceSearch.Name = "btnAdvanceSearch";
            this.btnAdvanceSearch.Size = new System.Drawing.Size(21, 21);
            this.btnAdvanceSearch.TabIndex = 6;
            this.btnAdvanceSearch.UseVisualStyleBackColor = false;
            this.btnAdvanceSearch.Click += new System.EventHandler(this.btnAdvanceSearch_Click);
            this.btnAdvanceSearch.MouseLeave += new System.EventHandler(this.btnAdvanceSearch_MouseLeave);
            this.btnAdvanceSearch.MouseHover += new System.EventHandler(this.btnAdvanceSearch_MouseHover);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Location = new System.Drawing.Point(829, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(2, 21);
            this.label9.TabIndex = 8;
            // 
            // lblSearch
            // 
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(4, 1);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(49, 21);
            this.lblSearch.TabIndex = 6;
            this.lblSearch.Text = "Search ";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(1, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(3, 21);
            this.label7.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(830, 1);
            this.label1.TabIndex = 27;
            this.label1.Text = "label2";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(830, 1);
            this.label4.TabIndex = 24;
            this.label4.Text = "label1";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 23);
            this.label12.TabIndex = 32;
            this.label12.Text = "label4";
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.Controls.Add(this.ts_Commands);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 31);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(831, 54);
            this.pnlToolstrip.TabIndex = 2;
            this.pnlToolstrip.Visible = false;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloPatient.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_Save,
            this.tsbtn_Close});
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(831, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "toolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsbtn_Save
            // 
            this.tsbtn_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_Save.Image")));
            this.tsbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Save.Name = "tsbtn_Save";
            this.tsbtn_Save.Size = new System.Drawing.Size(66, 50);
            this.tsbtn_Save.Tag = "Save";
            this.tsbtn_Save.Text = "&Save&&Cls";
            this.tsbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_Save.ToolTipText = "Save and Close";
            // 
            // tsbtn_Close
            // 
            this.tsbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_Close.Image")));
            this.tsbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Close.Name = "tsbtn_Close";
            this.tsbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tsbtn_Close.Tag = "Close";
            this.tsbtn_Close.Text = "&Close";
            this.tsbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_Close.ToolTipText = "Close";
            this.tsbtn_Close.Click += new System.EventHandler(this.tsbtn_Close_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.pn_CustomList);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnlHeader.Size = new System.Drawing.Size(831, 31);
            this.pnlHeader.TabIndex = 29;
            this.pnlHeader.Visible = false;
            // 
            // pn_CustomList
            // 
            this.pn_CustomList.BackColor = System.Drawing.Color.Transparent;
            this.pn_CustomList.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.pn_CustomList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pn_CustomList.Controls.Add(this.lblHeader);
            this.pn_CustomList.Controls.Add(this.lbl_BottomBrd);
            this.pn_CustomList.Controls.Add(this.lbl_LeftBrd);
            this.pn_CustomList.Controls.Add(this.lbl_RightBrd);
            this.pn_CustomList.Controls.Add(this.label5);
            this.pn_CustomList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_CustomList.ForeColor = System.Drawing.Color.White;
            this.pn_CustomList.Location = new System.Drawing.Point(3, 3);
            this.pn_CustomList.Name = "pn_CustomList";
            this.pn_CustomList.Size = new System.Drawing.Size(825, 25);
            this.pn_CustomList.TabIndex = 5;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(1, 1);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(823, 23);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "-";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 24);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(823, 1);
            this.lbl_BottomBrd.TabIndex = 8;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_LeftBrd.TabIndex = 7;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(824, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_RightBrd.TabIndex = 6;
            this.lbl_RightBrd.Text = "label3";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(825, 1);
            this.label5.TabIndex = 5;
            this.label5.Text = "label1";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(263, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(5, 21);
            this.label19.TabIndex = 46;
            // 
            // PatientListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlToolstrip);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "PatientListControl";
            this.Size = new System.Drawing.Size(831, 455);
            this.Load += new System.EventHandler(this.PatientListControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPatientView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).EndInit();
            this.panel3.ResumeLayout(false);
            this.pnlPatientON.ResumeLayout(false);
            this.pnlPatientON.PerformLayout();
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pn_CustomList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.DataGridView dgPatientView;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnGetAllPatients;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnAdvanceSearch;
        public  System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnNewPatientReg;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkInstringSearch;
        private System.Windows.Forms.Panel pnlPatientON;
        private System.Windows.Forms.DateTimePicker dtPatientOn;
        private System.Windows.Forms.CheckBox chkPatientOn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbProviders;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnMask;
        private System.Windows.Forms.ToolTip toolTipMask;
        private System.Windows.Forms.Button btnUnmask;
        private System.Windows.Forms.ToolStripButton tsbtn_Save;
        private System.Windows.Forms.ToolStripButton tsbtn_Close;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        private System.Windows.Forms.Panel pnlToolstrip;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pn_CustomList;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label Label77;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.Button btnSearchClose;
        private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnrecpat;
        private System.Windows.Forms.Label label19;
    }
}
