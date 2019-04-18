namespace gloBilling
{
    partial class frmSelectSmartTreatment
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
            DisposeonFormClose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectSmartTreatment));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.pnlC1TOSCPT = new System.Windows.Forms.Panel();
            this.C1TOSCPT = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlEnterTreatment = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTreatment = new System.Windows.Forms.TextBox();
            this.lblTreatmentNames = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.PnlCPT = new System.Windows.Forms.Panel();
            this.pnlTrvCPT = new System.Windows.Forms.Panel();
            this.trvCPT = new System.Windows.Forms.TreeView();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.pnl_btnICD9 = new System.Windows.Forms.Panel();
            this.btnICD9 = new System.Windows.Forms.Button();
            this.pnl_btnModifier = new System.Windows.Forms.Panel();
            this.btnModifier = new System.Windows.Forms.Button();
            this.pnl_btnCPT = new System.Windows.Forms.Panel();
            this.btnCPT = new System.Windows.Forms.Button();
            this.pnlSearchCriteria = new System.Windows.Forms.Panel();
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.chkBoxSelect = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlSearchCPT = new System.Windows.Forms.Panel();
            this.txtCPTSearch = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnlSearchBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.pnlSelect = new System.Windows.Forms.Panel();
            this.rbDescription = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.rbCode = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlTOS = new System.Windows.Forms.Panel();
            this.pnltrvTreatment = new System.Windows.Forms.Panel();
            this.trvTreatment = new System.Windows.Forms.TreeView();
            this.label8 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.pnl_lblTreatments = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTreatments = new System.Windows.Forms.Label();
            this.pnl_SearchTOS = new System.Windows.Forms.Panel();
            this.txtTOSSearch = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.pnl_lblTreatment = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblTreatment = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbICD9 = new System.Windows.Forms.RadioButton();
            this.rbICD10 = new System.Windows.Forms.RadioButton();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Select = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveCls = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.cxtMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgLstTOS = new System.Windows.Forms.ImageList(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.pnlC1TOSCPT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1TOSCPT)).BeginInit();
            this.pnlEnterTreatment.SuspendLayout();
            this.PnlCPT.SuspendLayout();
            this.pnlTrvCPT.SuspendLayout();
            this.pnl_btnICD9.SuspendLayout();
            this.pnl_btnModifier.SuspendLayout();
            this.pnl_btnCPT.SuspendLayout();
            this.pnlSearchCriteria.SuspendLayout();
            this.pnl_Base.SuspendLayout();
            this.pnlSearchCPT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.pnlSelect.SuspendLayout();
            this.pnlTOS.SuspendLayout();
            this.pnltrvTreatment.SuspendLayout();
            this.pnl_lblTreatments.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnl_SearchTOS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_lblTreatment.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.cxtMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlMiddle);
            this.pnlMain.Controls.Add(this.splitter2);
            this.pnlMain.Controls.Add(this.splitter1);
            this.pnlMain.Controls.Add(this.PnlCPT);
            this.pnlMain.Controls.Add(this.pnlTOS);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(855, 580);
            this.pnlMain.TabIndex = 3;
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.pnlC1TOSCPT);
            this.pnlMiddle.Controls.Add(this.pnlEnterTreatment);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Location = new System.Drawing.Point(261, 0);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Size = new System.Drawing.Size(333, 580);
            this.pnlMiddle.TabIndex = 6;
            // 
            // pnlC1TOSCPT
            // 
            this.pnlC1TOSCPT.Controls.Add(this.C1TOSCPT);
            this.pnlC1TOSCPT.Controls.Add(this.label16);
            this.pnlC1TOSCPT.Controls.Add(this.label17);
            this.pnlC1TOSCPT.Controls.Add(this.label18);
            this.pnlC1TOSCPT.Controls.Add(this.label19);
            this.pnlC1TOSCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlC1TOSCPT.Location = new System.Drawing.Point(0, 0);
            this.pnlC1TOSCPT.Name = "pnlC1TOSCPT";
            this.pnlC1TOSCPT.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnlC1TOSCPT.Size = new System.Drawing.Size(333, 580);
            this.pnlC1TOSCPT.TabIndex = 58;
            // 
            // C1TOSCPT
            // 
            this.C1TOSCPT.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.C1TOSCPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1TOSCPT.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1TOSCPT.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.C1TOSCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1TOSCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1TOSCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1TOSCPT.Location = new System.Drawing.Point(1, 4);
            this.C1TOSCPT.Name = "C1TOSCPT";
            this.C1TOSCPT.Rows.DefaultSize = 19;
            this.C1TOSCPT.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1TOSCPT.Size = new System.Drawing.Size(331, 572);
            this.C1TOSCPT.StyleInfo = resources.GetString("C1TOSCPT.StyleInfo");
            this.C1TOSCPT.TabIndex = 0;
            this.C1TOSCPT.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.C1TOSCPT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C1TOSCPT_MouseDown);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label16.Location = new System.Drawing.Point(1, 576);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(331, 1);
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
            this.label17.Size = new System.Drawing.Size(1, 573);
            this.label17.TabIndex = 7;
            this.label17.Text = "label4";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label18.Location = new System.Drawing.Point(332, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 573);
            this.label18.TabIndex = 6;
            this.label18.Text = "label3";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(0, 3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(333, 1);
            this.label19.TabIndex = 5;
            this.label19.Text = "label1";
            // 
            // pnlEnterTreatment
            // 
            this.pnlEnterTreatment.Controls.Add(this.label5);
            this.pnlEnterTreatment.Controls.Add(this.label6);
            this.pnlEnterTreatment.Controls.Add(this.label7);
            this.pnlEnterTreatment.Controls.Add(this.label12);
            this.pnlEnterTreatment.Controls.Add(this.txtTreatment);
            this.pnlEnterTreatment.Controls.Add(this.lblTreatmentNames);
            this.pnlEnterTreatment.Location = new System.Drawing.Point(29, 70);
            this.pnlEnterTreatment.Name = "pnlEnterTreatment";
            this.pnlEnterTreatment.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.pnlEnterTreatment.Size = new System.Drawing.Size(207, 41);
            this.pnlEnterTreatment.TabIndex = 57;
            this.pnlEnterTreatment.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(2, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(203, 1);
            this.label5.TabIndex = 64;
            this.label5.Text = "label2";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 34);
            this.label6.TabIndex = 63;
            this.label6.Text = "label4";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(205, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 34);
            this.label7.TabIndex = 62;
            this.label7.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(205, 1);
            this.label12.TabIndex = 61;
            this.label12.Text = "label1";
            // 
            // txtTreatment
            // 
            this.txtTreatment.BackColor = System.Drawing.Color.White;
            this.txtTreatment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTreatment.Location = new System.Drawing.Point(123, 10);
            this.txtTreatment.Name = "txtTreatment";
            this.txtTreatment.ReadOnly = true;
            this.txtTreatment.Size = new System.Drawing.Size(383, 22);
            this.txtTreatment.TabIndex = 60;
            this.txtTreatment.Visible = false;
            // 
            // lblTreatmentNames
            // 
            this.lblTreatmentNames.AutoSize = true;
            this.lblTreatmentNames.BackColor = System.Drawing.Color.Transparent;
            this.lblTreatmentNames.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTreatmentNames.Location = new System.Drawing.Point(9, 14);
            this.lblTreatmentNames.Name = "lblTreatmentNames";
            this.lblTreatmentNames.Size = new System.Drawing.Size(109, 14);
            this.lblTreatmentNames.TabIndex = 59;
            this.lblTreatmentNames.Text = "Treatment Name :";
            this.lblTreatmentNames.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTreatmentNames.Visible = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(594, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 580);
            this.splitter2.TabIndex = 8;
            this.splitter2.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(258, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 580);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // PnlCPT
            // 
            this.PnlCPT.Controls.Add(this.pnlTrvCPT);
            this.PnlCPT.Controls.Add(this.pnl_btnICD9);
            this.PnlCPT.Controls.Add(this.pnl_btnModifier);
            this.PnlCPT.Controls.Add(this.pnl_btnCPT);
            this.PnlCPT.Controls.Add(this.pnlSearchCriteria);
            this.PnlCPT.Dock = System.Windows.Forms.DockStyle.Right;
            this.PnlCPT.Location = new System.Drawing.Point(597, 0);
            this.PnlCPT.Name = "PnlCPT";
            this.PnlCPT.Size = new System.Drawing.Size(258, 580);
            this.PnlCPT.TabIndex = 4;
            this.PnlCPT.Visible = false;
            // 
            // pnlTrvCPT
            // 
            this.pnlTrvCPT.Controls.Add(this.trvCPT);
            this.pnlTrvCPT.Controls.Add(this.label9);
            this.pnlTrvCPT.Controls.Add(this.label15);
            this.pnlTrvCPT.Controls.Add(this.label20);
            this.pnlTrvCPT.Controls.Add(this.label21);
            this.pnlTrvCPT.Controls.Add(this.label22);
            this.pnlTrvCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTrvCPT.Location = new System.Drawing.Point(0, 111);
            this.pnlTrvCPT.Name = "pnlTrvCPT";
            this.pnlTrvCPT.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlTrvCPT.Size = new System.Drawing.Size(258, 413);
            this.pnlTrvCPT.TabIndex = 61;
            // 
            // trvCPT
            // 
            this.trvCPT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCPT.CheckBoxes = true;
            this.trvCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCPT.ForeColor = System.Drawing.Color.Black;
            this.trvCPT.FullRowSelect = true;
            this.trvCPT.LineColor = System.Drawing.Color.SteelBlue;
            this.trvCPT.Location = new System.Drawing.Point(1, 5);
            this.trvCPT.Name = "trvCPT";
            this.trvCPT.Size = new System.Drawing.Size(253, 404);
            this.trvCPT.TabIndex = 2;
            this.trvCPT.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCPT_AfterSelect);
            this.trvCPT.DoubleClick += new System.EventHandler(this.trvCPT_DoubleClick);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(1, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(253, 4);
            this.label9.TabIndex = 38;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label15.Location = new System.Drawing.Point(1, 409);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(253, 1);
            this.label15.TabIndex = 8;
            this.label15.Text = "label2";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 409);
            this.label20.TabIndex = 7;
            this.label20.Text = "label4";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Right;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(254, 1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 409);
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
            this.label22.Size = new System.Drawing.Size(255, 1);
            this.label22.TabIndex = 5;
            this.label22.Text = "label1";
            // 
            // pnl_btnICD9
            // 
            this.pnl_btnICD9.Controls.Add(this.btnICD9);
            this.pnl_btnICD9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_btnICD9.Location = new System.Drawing.Point(0, 524);
            this.pnl_btnICD9.Name = "pnl_btnICD9";
            this.pnl_btnICD9.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnl_btnICD9.Size = new System.Drawing.Size(258, 28);
            this.pnl_btnICD9.TabIndex = 60;
            // 
            // btnICD9
            // 
            this.btnICD9.AutoEllipsis = true;
            this.btnICD9.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.btnICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnICD9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnICD9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnICD9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnICD9.Location = new System.Drawing.Point(0, 0);
            this.btnICD9.Name = "btnICD9";
            this.btnICD9.Size = new System.Drawing.Size(255, 25);
            this.btnICD9.TabIndex = 55;
            this.btnICD9.Tag = "Unselected";
            this.btnICD9.Text = "ICD9";
            this.btnICD9.UseVisualStyleBackColor = true;
            this.btnICD9.Click += new System.EventHandler(this.btnICD9_Click);
            this.btnICD9.MouseLeave += new System.EventHandler(this.btnICD9_MouseLeave);
            this.btnICD9.MouseHover += new System.EventHandler(this.btnICD9_MouseHover);
            // 
            // pnl_btnModifier
            // 
            this.pnl_btnModifier.Controls.Add(this.btnModifier);
            this.pnl_btnModifier.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_btnModifier.Location = new System.Drawing.Point(0, 552);
            this.pnl_btnModifier.Name = "pnl_btnModifier";
            this.pnl_btnModifier.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnl_btnModifier.Size = new System.Drawing.Size(258, 28);
            this.pnl_btnModifier.TabIndex = 59;
            // 
            // btnModifier
            // 
            this.btnModifier.AutoEllipsis = true;
            this.btnModifier.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.btnModifier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnModifier.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifier.Location = new System.Drawing.Point(0, 0);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(255, 25);
            this.btnModifier.TabIndex = 57;
            this.btnModifier.Tag = "Unselected";
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);
            this.btnModifier.MouseLeave += new System.EventHandler(this.btnModifier_MouseLeave);
            this.btnModifier.MouseHover += new System.EventHandler(this.btnModifier_MouseHover);
            // 
            // pnl_btnCPT
            // 
            this.pnl_btnCPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_btnCPT.Controls.Add(this.btnCPT);
            this.pnl_btnCPT.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_btnCPT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_btnCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_btnCPT.Location = new System.Drawing.Point(0, 83);
            this.pnl_btnCPT.Name = "pnl_btnCPT";
            this.pnl_btnCPT.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnl_btnCPT.Size = new System.Drawing.Size(258, 28);
            this.pnl_btnCPT.TabIndex = 58;
            // 
            // btnCPT
            // 
            this.btnCPT.AutoEllipsis = true;
            this.btnCPT.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
            this.btnCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCPT.Location = new System.Drawing.Point(0, 0);
            this.btnCPT.Name = "btnCPT";
            this.btnCPT.Size = new System.Drawing.Size(255, 25);
            this.btnCPT.TabIndex = 56;
            this.btnCPT.Tag = "Selected";
            this.btnCPT.Text = "CPT";
            this.btnCPT.UseVisualStyleBackColor = true;
            this.btnCPT.Click += new System.EventHandler(this.btnCPT_Click);
            this.btnCPT.MouseLeave += new System.EventHandler(this.btnCPT_MouseLeave);
            this.btnCPT.MouseHover += new System.EventHandler(this.btnCPT_MouseHover);
            // 
            // pnlSearchCriteria
            // 
            this.pnlSearchCriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchCriteria.Controls.Add(this.pnl_Base);
            this.pnlSearchCriteria.Controls.Add(this.pnlSearchCPT);
            this.pnlSearchCriteria.Controls.Add(this.pnlSelect);
            this.pnlSearchCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchCriteria.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchCriteria.Name = "pnlSearchCriteria";
            this.pnlSearchCriteria.Size = new System.Drawing.Size(258, 83);
            this.pnlSearchCriteria.TabIndex = 4;
            // 
            // pnl_Base
            // 
            this.pnl_Base.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Base.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnl_Base.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Base.Controls.Add(this.lbl_BottomBrd);
            this.pnl_Base.Controls.Add(this.chkBoxSelect);
            this.pnl_Base.Controls.Add(this.label2);
            this.pnl_Base.Controls.Add(this.label13);
            this.pnl_Base.Controls.Add(this.label14);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Base.Location = new System.Drawing.Point(0, 56);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnl_Base.Size = new System.Drawing.Size(258, 28);
            this.pnl_Base.TabIndex = 59;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 24);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(253, 1);
            this.lbl_BottomBrd.TabIndex = 4;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // chkBoxSelect
            // 
            this.chkBoxSelect.AutoSize = true;
            this.chkBoxSelect.BackColor = System.Drawing.Color.Transparent;
            this.chkBoxSelect.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkBoxSelect.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxSelect.Location = new System.Drawing.Point(27, 4);
            this.chkBoxSelect.Name = "chkBoxSelect";
            this.chkBoxSelect.Size = new System.Drawing.Size(82, 18);
            this.chkBoxSelect.TabIndex = 58;
            this.chkBoxSelect.Text = "Select All";
            this.chkBoxSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBoxSelect.UseVisualStyleBackColor = false;
            this.chkBoxSelect.Click += new System.EventHandler(this.chkBoxSelect_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "label4";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(254, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 24);
            this.label13.TabIndex = 2;
            this.label13.Text = "label3";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(255, 1);
            this.label14.TabIndex = 0;
            this.label14.Text = "label1";
            // 
            // pnlSearchCPT
            // 
            this.pnlSearchCPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearchCPT.Controls.Add(this.txtCPTSearch);
            this.pnlSearchCPT.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnlSearchCPT.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnlSearchCPT.Controls.Add(this.PicBx_Search);
            this.pnlSearchCPT.Controls.Add(this.lbl_pnlSearchBottomBrd);
            this.pnlSearchCPT.Controls.Add(this.lbl_pnlSearchTopBrd);
            this.pnlSearchCPT.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.pnlSearchCPT.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.pnlSearchCPT.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearchCPT.ForeColor = System.Drawing.Color.Black;
            this.pnlSearchCPT.Location = new System.Drawing.Point(0, 30);
            this.pnlSearchCPT.Name = "pnlSearchCPT";
            this.pnlSearchCPT.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlSearchCPT.Size = new System.Drawing.Size(258, 26);
            this.pnlSearchCPT.TabIndex = 58;
            // 
            // txtCPTSearch
            // 
            this.txtCPTSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCPTSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCPTSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCPTSearch.ForeColor = System.Drawing.Color.Black;
            this.txtCPTSearch.Location = new System.Drawing.Point(29, 5);
            this.txtCPTSearch.Name = "txtCPTSearch";
            this.txtCPTSearch.Size = new System.Drawing.Size(225, 15);
            this.txtCPTSearch.TabIndex = 0;
            this.txtCPTSearch.TextChanged += new System.EventHandler(this.txtCPTSearch_TextChanged);
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(29, 1);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(225, 4);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(29, 20);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(225, 2);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // PicBx_Search
            // 
            this.PicBx_Search.BackColor = System.Drawing.Color.White;
            this.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBx_Search.Image = ((System.Drawing.Image)(resources.GetObject("PicBx_Search.Image")));
            this.PicBx_Search.Location = new System.Drawing.Point(1, 1);
            this.PicBx_Search.Name = "PicBx_Search";
            this.PicBx_Search.Size = new System.Drawing.Size(28, 21);
            this.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBx_Search.TabIndex = 9;
            this.PicBx_Search.TabStop = false;
            // 
            // lbl_pnlSearchBottomBrd
            // 
            this.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchBottomBrd.Location = new System.Drawing.Point(1, 22);
            this.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd";
            this.lbl_pnlSearchBottomBrd.Size = new System.Drawing.Size(253, 1);
            this.lbl_pnlSearchBottomBrd.TabIndex = 35;
            this.lbl_pnlSearchBottomBrd.Text = "label1";
            // 
            // lbl_pnlSearchTopBrd
            // 
            this.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSearchTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd";
            this.lbl_pnlSearchTopBrd.Size = new System.Drawing.Size(253, 1);
            this.lbl_pnlSearchTopBrd.TabIndex = 36;
            this.lbl_pnlSearchTopBrd.Text = "label1";
            // 
            // lbl_pnlSearchLeftBrd
            // 
            this.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd";
            this.lbl_pnlSearchLeftBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlSearchLeftBrd.TabIndex = 39;
            this.lbl_pnlSearchLeftBrd.Text = "label4";
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(254, 0);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // pnlSelect
            // 
            this.pnlSelect.BackColor = System.Drawing.Color.Transparent;
            this.pnlSelect.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSelect.Controls.Add(this.rbDescription);
            this.pnlSelect.Controls.Add(this.label11);
            this.pnlSelect.Controls.Add(this.rbCode);
            this.pnlSelect.Controls.Add(this.label10);
            this.pnlSelect.Controls.Add(this.lbl_RightBrd);
            this.pnlSelect.Controls.Add(this.lbl_TopBrd);
            this.pnlSelect.Controls.Add(this.lbl_LeftBrd);
            this.pnlSelect.Controls.Add(this.label3);
            this.pnlSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSelect.Location = new System.Drawing.Point(0, 0);
            this.pnlSelect.Name = "pnlSelect";
            this.pnlSelect.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlSelect.Size = new System.Drawing.Size(258, 30);
            this.pnlSelect.TabIndex = 57;
            // 
            // rbDescription
            // 
            this.rbDescription.AutoSize = true;
            this.rbDescription.BackColor = System.Drawing.Color.Transparent;
            this.rbDescription.Checked = true;
            this.rbDescription.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDescription.Location = new System.Drawing.Point(134, 4);
            this.rbDescription.Name = "rbDescription";
            this.rbDescription.Size = new System.Drawing.Size(94, 22);
            this.rbDescription.TabIndex = 57;
            this.rbDescription.TabStop = true;
            this.rbDescription.Text = "Description";
            this.rbDescription.UseVisualStyleBackColor = false;
            this.rbDescription.CheckedChanged += new System.EventHandler(this.rbDescription_CheckedChanged);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(228, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 22);
            this.label11.TabIndex = 66;
            // 
            // rbCode
            // 
            this.rbCode.AutoSize = true;
            this.rbCode.BackColor = System.Drawing.Color.Transparent;
            this.rbCode.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCode.Location = new System.Drawing.Point(27, 4);
            this.rbCode.Name = "rbCode";
            this.rbCode.Size = new System.Drawing.Size(53, 22);
            this.rbCode.TabIndex = 0;
            this.rbCode.Text = "Code";
            this.rbCode.UseVisualStyleBackColor = false;
            this.rbCode.CheckedChanged += new System.EventHandler(this.rbCode_CheckedChanged);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(1, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 22);
            this.label10.TabIndex = 65;
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(254, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_RightBrd.TabIndex = 64;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(1, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(254, 1);
            this.lbl_TopBrd.TabIndex = 63;
            this.lbl_TopBrd.Text = "label1";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 3);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_LeftBrd.TabIndex = 62;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(255, 1);
            this.label3.TabIndex = 58;
            this.label3.Text = "label1";
            // 
            // pnlTOS
            // 
            this.pnlTOS.Controls.Add(this.pnltrvTreatment);
            this.pnlTOS.Controls.Add(this.pnl_lblTreatments);
            this.pnlTOS.Controls.Add(this.pnl_SearchTOS);
            this.pnlTOS.Controls.Add(this.pnl_lblTreatment);
            this.pnlTOS.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTOS.Location = new System.Drawing.Point(0, 0);
            this.pnlTOS.Name = "pnlTOS";
            this.pnlTOS.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlTOS.Size = new System.Drawing.Size(258, 580);
            this.pnlTOS.TabIndex = 5;
            // 
            // pnltrvTreatment
            // 
            this.pnltrvTreatment.Controls.Add(this.trvTreatment);
            this.pnltrvTreatment.Controls.Add(this.label8);
            this.pnltrvTreatment.Controls.Add(this.label35);
            this.pnltrvTreatment.Controls.Add(this.label36);
            this.pnltrvTreatment.Controls.Add(this.label37);
            this.pnltrvTreatment.Controls.Add(this.label38);
            this.pnltrvTreatment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnltrvTreatment.Location = new System.Drawing.Point(0, 29);
            this.pnltrvTreatment.Name = "pnltrvTreatment";
            this.pnltrvTreatment.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltrvTreatment.Size = new System.Drawing.Size(258, 551);
            this.pnltrvTreatment.TabIndex = 57;
            // 
            // trvTreatment
            // 
            this.trvTreatment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvTreatment.CheckBoxes = true;
            this.trvTreatment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTreatment.ForeColor = System.Drawing.Color.Black;
            this.trvTreatment.Indent = 20;
            this.trvTreatment.ItemHeight = 20;
            this.trvTreatment.Location = new System.Drawing.Point(4, 5);
            this.trvTreatment.Name = "trvTreatment";
            this.trvTreatment.Size = new System.Drawing.Size(253, 542);
            this.trvTreatment.TabIndex = 2;
            this.trvTreatment.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvTreatment_AfterCheck);
            this.trvTreatment.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTreatment_AfterSelect);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(4, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(253, 4);
            this.label8.TabIndex = 9;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label35.Location = new System.Drawing.Point(4, 547);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(253, 1);
            this.label35.TabIndex = 8;
            this.label35.Text = "label2";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(3, 1);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 547);
            this.label36.TabIndex = 7;
            this.label36.Text = "label4";
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Right;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label37.Location = new System.Drawing.Point(257, 1);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 547);
            this.label37.TabIndex = 6;
            this.label37.Text = "label3";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Top;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(3, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(255, 1);
            this.label38.TabIndex = 5;
            this.label38.Text = "label1";
            // 
            // pnl_lblTreatments
            // 
            this.pnl_lblTreatments.Controls.Add(this.panel2);
            this.pnl_lblTreatments.Location = new System.Drawing.Point(0, 54);
            this.pnl_lblTreatments.Name = "pnl_lblTreatments";
            this.pnl_lblTreatments.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnl_lblTreatments.Size = new System.Drawing.Size(258, 28);
            this.pnl_lblTreatments.TabIndex = 56;
            this.pnl_lblTreatments.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
            this.panel2.Controls.Add(this.label34);
            this.panel2.Controls.Add(this.label33);
            this.panel2.Controls.Add(this.label32);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblTreatments);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 25);
            this.panel2.TabIndex = 1;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Top;
            this.label34.Location = new System.Drawing.Point(1, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(253, 1);
            this.label34.TabIndex = 66;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Right;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(254, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1, 24);
            this.label33.TabIndex = 65;
            this.label33.Text = "label4";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Left;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(0, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 24);
            this.label32.TabIndex = 64;
            this.label32.Text = "label4";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 1);
            this.label1.TabIndex = 56;
            // 
            // lblTreatments
            // 
            this.lblTreatments.BackColor = System.Drawing.Color.Transparent;
            this.lblTreatments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTreatments.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTreatments.Location = new System.Drawing.Point(0, 0);
            this.lblTreatments.Name = "lblTreatments";
            this.lblTreatments.Size = new System.Drawing.Size(255, 25);
            this.lblTreatments.TabIndex = 0;
            this.lblTreatments.Text = "Smart Treatments";
            this.lblTreatments.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_SearchTOS
            // 
            this.pnl_SearchTOS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_SearchTOS.Controls.Add(this.txtTOSSearch);
            this.pnl_SearchTOS.Controls.Add(this.label26);
            this.pnl_SearchTOS.Controls.Add(this.label27);
            this.pnl_SearchTOS.Controls.Add(this.pictureBox1);
            this.pnl_SearchTOS.Controls.Add(this.label28);
            this.pnl_SearchTOS.Controls.Add(this.label29);
            this.pnl_SearchTOS.Controls.Add(this.label30);
            this.pnl_SearchTOS.Controls.Add(this.label31);
            this.pnl_SearchTOS.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_SearchTOS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_SearchTOS.ForeColor = System.Drawing.Color.Black;
            this.pnl_SearchTOS.Location = new System.Drawing.Point(0, 3);
            this.pnl_SearchTOS.Name = "pnl_SearchTOS";
            this.pnl_SearchTOS.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnl_SearchTOS.Size = new System.Drawing.Size(258, 26);
            this.pnl_SearchTOS.TabIndex = 55;
            // 
            // txtTOSSearch
            // 
            this.txtTOSSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTOSSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTOSSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTOSSearch.ForeColor = System.Drawing.Color.Black;
            this.txtTOSSearch.Location = new System.Drawing.Point(32, 5);
            this.txtTOSSearch.Name = "txtTOSSearch";
            this.txtTOSSearch.Size = new System.Drawing.Size(225, 15);
            this.txtTOSSearch.TabIndex = 0;
            this.txtTOSSearch.TextChanged += new System.EventHandler(this.txtTOSSearch_TextChanged);
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.White;
            this.label26.Dock = System.Windows.Forms.DockStyle.Top;
            this.label26.Location = new System.Drawing.Point(32, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(225, 4);
            this.label26.TabIndex = 37;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.White;
            this.label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label27.Location = new System.Drawing.Point(32, 20);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(225, 2);
            this.label27.TabIndex = 38;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 21);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label28.Location = new System.Drawing.Point(4, 22);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(253, 1);
            this.label28.TabIndex = 35;
            this.label28.Text = "label1";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Top;
            this.label29.Location = new System.Drawing.Point(4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(253, 1);
            this.label29.TabIndex = 36;
            this.label29.Text = "label1";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Location = new System.Drawing.Point(3, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 23);
            this.label30.TabIndex = 39;
            this.label30.Text = "label4";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Right;
            this.label31.Location = new System.Drawing.Point(257, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 23);
            this.label31.TabIndex = 40;
            this.label31.Text = "label4";
            // 
            // pnl_lblTreatment
            // 
            this.pnl_lblTreatment.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnl_lblTreatment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_lblTreatment.Controls.Add(this.label25);
            this.pnl_lblTreatment.Controls.Add(this.label24);
            this.pnl_lblTreatment.Controls.Add(this.label23);
            this.pnl_lblTreatment.Controls.Add(this.lblTreatment);
            this.pnl_lblTreatment.Controls.Add(this.label4);
            this.pnl_lblTreatment.Location = new System.Drawing.Point(0, 0);
            this.pnl_lblTreatment.Name = "pnl_lblTreatment";
            this.pnl_lblTreatment.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnl_lblTreatment.Size = new System.Drawing.Size(258, 28);
            this.pnl_lblTreatment.TabIndex = 4;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(257, 4);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 20);
            this.label25.TabIndex = 64;
            this.label25.Text = "label4";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(3, 4);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 20);
            this.label24.TabIndex = 63;
            this.label24.Text = "label4";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Location = new System.Drawing.Point(3, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(255, 1);
            this.label23.TabIndex = 59;
            // 
            // lblTreatment
            // 
            this.lblTreatment.BackColor = System.Drawing.Color.Transparent;
            this.lblTreatment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTreatment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTreatment.Location = new System.Drawing.Point(3, 3);
            this.lblTreatment.Name = "lblTreatment";
            this.lblTreatment.Size = new System.Drawing.Size(255, 21);
            this.lblTreatment.TabIndex = 58;
            this.lblTreatment.Text = "Search Treatment";
            this.lblTreatment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(3, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(255, 1);
            this.label4.TabIndex = 57;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Controls.Add(this.ts_Commands);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(855, 54);
            this.pnlTop.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.rbAll);
            this.panel1.Controls.Add(this.rbICD9);
            this.panel1.Controls.Add(this.rbICD10);
            this.panel1.Location = new System.Drawing.Point(662, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 53);
            this.panel1.TabIndex = 66;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.BackColor = System.Drawing.Color.Transparent;
            this.rbAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbAll.Checked = true;
            this.rbAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbAll.Location = new System.Drawing.Point(6, 0);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(49, 53);
            this.rbAll.TabIndex = 65;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All   ";
            this.rbAll.UseVisualStyleBackColor = false;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbICD9
            // 
            this.rbICD9.AutoSize = true;
            this.rbICD9.BackColor = System.Drawing.Color.Transparent;
            this.rbICD9.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbICD9.Location = new System.Drawing.Point(55, 0);
            this.rbICD9.Name = "rbICD9";
            this.rbICD9.Size = new System.Drawing.Size(59, 53);
            this.rbICD9.TabIndex = 59;
            this.rbICD9.Text = "ICD9  ";
            this.rbICD9.UseVisualStyleBackColor = false;
            this.rbICD9.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbICD10
            // 
            this.rbICD10.AutoSize = true;
            this.rbICD10.BackColor = System.Drawing.Color.Transparent;
            this.rbICD10.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbICD10.Location = new System.Drawing.Point(114, 0);
            this.rbICD10.Name = "rbICD10";
            this.rbICD10.Size = new System.Drawing.Size(58, 53);
            this.rbICD10.TabIndex = 64;
            this.rbICD10.Text = "ICD10";
            this.rbICD10.UseVisualStyleBackColor = false;
            this.rbICD10.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Select,
            this.tsbtnAdd,
            this.tsb_SaveCls,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(855, 53);
            this.ts_Commands.TabIndex = 58;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_Select
            // 
            this.tsb_Select.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Select.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Select.Image")));
            this.tsb_Select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Select.Name = "tsb_Select";
            this.tsb_Select.Size = new System.Drawing.Size(67, 50);
            this.tsb_Select.Tag = "Select";
            this.tsb_Select.Text = "&Select All";
            this.tsb_Select.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Select.Visible = false;
            // 
            // tsbtnAdd
            // 
            this.tsbtnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAdd.Image")));
            this.tsbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAdd.Name = "tsbtnAdd";
            this.tsbtnAdd.Size = new System.Drawing.Size(58, 50);
            this.tsbtnAdd.Tag = "ADD";
            this.tsbtnAdd.Text = "&Refresh";
            this.tsbtnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnAdd.Visible = false;
            // 
            // tsb_SaveCls
            // 
            this.tsb_SaveCls.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SaveCls.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveCls.Image")));
            this.tsb_SaveCls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveCls.Name = "tsb_SaveCls";
            this.tsb_SaveCls.Size = new System.Drawing.Size(66, 50);
            this.tsb_SaveCls.Tag = "Save";
            this.tsb_SaveCls.Text = "Sa&ve&&Cls";
            this.tsb_SaveCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SaveCls.ToolTipText = "Save and Close";
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
            // cxtMS
            // 
            this.cxtMS.BackColor = System.Drawing.SystemColors.Control;
            this.cxtMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.cxtMS.Name = "contextMenuStrip1";
            this.cxtMS.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // imgLstTOS
            // 
            this.imgLstTOS.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLstTOS.ImageStream")));
            this.imgLstTOS.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLstTOS.Images.SetKeyName(0, "CPT.ico");
            this.imgLstTOS.Images.SetKeyName(1, "ICD 09.ico");
            this.imgLstTOS.Images.SetKeyName(2, "Modify.ico");
            this.imgLstTOS.Images.SetKeyName(3, "ICD10GalleryGreen.ico");
            this.imgLstTOS.Images.SetKeyName(4, "ICD 10.ico");
            // 
            // frmSelectSmartTreatment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(855, 634);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSelectSmartTreatment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Treatment";
            this.Load += new System.EventHandler(this.frmSelectSmartTreatment_Load);
            this.Shown += new System.EventHandler(this.frmSelectSmartTreatment_Shown);
            this.pnlMain.ResumeLayout(false);
            this.pnlMiddle.ResumeLayout(false);
            this.pnlC1TOSCPT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1TOSCPT)).EndInit();
            this.pnlEnterTreatment.ResumeLayout(false);
            this.pnlEnterTreatment.PerformLayout();
            this.PnlCPT.ResumeLayout(false);
            this.pnlTrvCPT.ResumeLayout(false);
            this.pnl_btnICD9.ResumeLayout(false);
            this.pnl_btnModifier.ResumeLayout(false);
            this.pnl_btnCPT.ResumeLayout(false);
            this.pnlSearchCriteria.ResumeLayout(false);
            this.pnl_Base.ResumeLayout(false);
            this.pnl_Base.PerformLayout();
            this.pnlSearchCPT.ResumeLayout(false);
            this.pnlSearchCPT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.pnlSelect.ResumeLayout(false);
            this.pnlSelect.PerformLayout();
            this.pnlTOS.ResumeLayout(false);
            this.pnltrvTreatment.ResumeLayout(false);
            this.pnl_lblTreatments.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnl_SearchTOS.ResumeLayout(false);
            this.pnl_SearchTOS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_lblTreatment.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.cxtMS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlMiddle;
        private C1.Win.C1FlexGrid.C1FlexGrid C1TOSCPT;
        private System.Windows.Forms.Panel PnlCPT;
        private System.Windows.Forms.TreeView trvCPT;
        private System.Windows.Forms.Button btnCPT;
        private System.Windows.Forms.Button btnICD9;
        private System.Windows.Forms.Panel pnlSearchCriteria;
        private System.Windows.Forms.CheckBox chkBoxSelect;
        private System.Windows.Forms.TextBox txtCPTSearch;
        private System.Windows.Forms.Panel pnlSelect;
        private System.Windows.Forms.RadioButton rbDescription;
        private System.Windows.Forms.RadioButton rbCode;
        private System.Windows.Forms.Panel pnlTop;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsbtnAdd;
        internal System.Windows.Forms.ToolStripButton tsb_Select;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.ContextMenuStrip cxtMS;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ImageList imgLstTOS;
        private System.Windows.Forms.Panel pnlTOS;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtTOSSearch;
        private System.Windows.Forms.TreeView trvTreatment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTreatments;
        private System.Windows.Forms.Panel pnl_lblTreatment;
        private System.Windows.Forms.Label lblTreatment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlEnterTreatment;
        private System.Windows.Forms.Label lblTreatmentNames;
        private System.Windows.Forms.TextBox txtTreatment;
        private System.Windows.Forms.Button btnModifier;
        internal System.Windows.Forms.Panel pnlSearchCPT;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnlSearchBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSearchTopBrd;
        private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnl_Base;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnl_btnCPT;
        private System.Windows.Forms.Panel pnl_btnICD9;
        private System.Windows.Forms.Panel pnl_btnModifier;
        private System.Windows.Forms.Panel pnlTrvCPT;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        internal System.Windows.Forms.Panel pnl_SearchTOS;
        internal System.Windows.Forms.Label label26;
        internal System.Windows.Forms.Label label27;
        internal System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Panel pnltrvTreatment;
        private System.Windows.Forms.Panel pnl_lblTreatments;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Panel pnlC1TOSCPT;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.ToolStripButton tsb_SaveCls;
        private System.Windows.Forms.RadioButton rbICD9;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbICD10;
        private System.Windows.Forms.Panel panel1;
    }
}