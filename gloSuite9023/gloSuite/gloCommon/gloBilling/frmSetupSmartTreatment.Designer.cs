namespace gloBilling
{
    partial class frmSetupSmartTreatment
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
            FormDispose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupSmartTreatment));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.pnlC1TOSCPT = new System.Windows.Forms.Panel();
            this.C1TOSCPT = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlEnteTreatment = new System.Windows.Forms.Panel();
            this.pnlEnterTreatment = new System.Windows.Forms.Panel();
            this.txtTreatment = new System.Windows.Forms.TextBox();
            this.lblTreatmentNames = new System.Windows.Forms.Label();
            this.rbICD9 = new System.Windows.Forms.RadioButton();
            this.rbICD10 = new System.Windows.Forms.RadioButton();
            this.label33 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.PnlCPT = new System.Windows.Forms.Panel();
            this.elementHostICD10 = new System.Windows.Forms.Integration.ElementHost();
            this.pnl_trvCPT = new System.Windows.Forms.Panel();
            this.trvCPT = new System.Windows.Forms.TreeView();
            this.label32 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnl_btnICD9 = new System.Windows.Forms.Panel();
            this.btnICD9 = new System.Windows.Forms.Button();
            this.pnl_btnModifier = new System.Windows.Forms.Panel();
            this.btnModifier = new System.Windows.Forms.Button();
            this.pnl_btnCPT = new System.Windows.Forms.Panel();
            this.btnCPT = new System.Windows.Forms.Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtCPTSearch = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnlSearchBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.pnl_Select = new System.Windows.Forms.Panel();
            this.pnlSelect = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.rbDescription = new System.Windows.Forms.RadioButton();
            this.rbCode = new System.Windows.Forms.RadioButton();
            this.pnlTOS = new System.Windows.Forms.Panel();
            this.pnltrvTreatment = new System.Windows.Forms.Panel();
            this.trvTreatment = new System.Windows.Forms.TreeView();
            this.label34 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.pnllblTreatments = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTreatments = new System.Windows.Forms.Label();
            this.pnltxtTOSSearch = new System.Windows.Forms.Panel();
            this.txtTOSSearch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.pnl_SlectAll = new System.Windows.Forms.Panel();
            this.pnlSearchCriteria = new System.Windows.Forms.Panel();
            this.rbModifier = new System.Windows.Forms.RadioButton();
            this.rbCPT = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkBoxSelect = new System.Windows.Forms.CheckBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_New = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.cxtMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgLstTOS = new System.Windows.Forms.ImageList(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.pnlC1TOSCPT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1TOSCPT)).BeginInit();
            this.pnlEnteTreatment.SuspendLayout();
            this.pnlEnterTreatment.SuspendLayout();
            this.PnlCPT.SuspendLayout();
            this.pnl_trvCPT.SuspendLayout();
            this.pnl_btnICD9.SuspendLayout();
            this.pnl_btnModifier.SuspendLayout();
            this.pnl_btnCPT.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.pnl_Select.SuspendLayout();
            this.pnlSelect.SuspendLayout();
            this.pnlTOS.SuspendLayout();
            this.pnltrvTreatment.SuspendLayout();
            this.pnllblTreatments.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnltxtTOSSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_SlectAll.SuspendLayout();
            this.pnlSearchCriteria.SuspendLayout();
            this.pnlTop.SuspendLayout();
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
            this.pnlMain.Size = new System.Drawing.Size(1255, 687);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.pnlC1TOSCPT);
            this.pnlMiddle.Controls.Add(this.pnlEnteTreatment);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Location = new System.Drawing.Point(264, 0);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Padding = new System.Windows.Forms.Padding(1);
            this.pnlMiddle.Size = new System.Drawing.Size(728, 687);
            this.pnlMiddle.TabIndex = 1;
            this.pnlMiddle.TabStop = true;
            // 
            // pnlC1TOSCPT
            // 
            this.pnlC1TOSCPT.Controls.Add(this.C1TOSCPT);
            this.pnlC1TOSCPT.Controls.Add(this.label16);
            this.pnlC1TOSCPT.Controls.Add(this.label17);
            this.pnlC1TOSCPT.Controls.Add(this.label18);
            this.pnlC1TOSCPT.Controls.Add(this.label19);
            this.pnlC1TOSCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlC1TOSCPT.Location = new System.Drawing.Point(1, 33);
            this.pnlC1TOSCPT.Name = "pnlC1TOSCPT";
            this.pnlC1TOSCPT.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlC1TOSCPT.Size = new System.Drawing.Size(726, 653);
            this.pnlC1TOSCPT.TabIndex = 59;
            // 
            // C1TOSCPT
            // 
            this.C1TOSCPT.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.C1TOSCPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1TOSCPT.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1TOSCPT.ColumnInfo = "10,0,0,0,0,105,Columns:";
            this.C1TOSCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1TOSCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1TOSCPT.Location = new System.Drawing.Point(1, 1);
            this.C1TOSCPT.Name = "C1TOSCPT";
            this.C1TOSCPT.Rows.DefaultSize = 21;
            this.C1TOSCPT.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1TOSCPT.Size = new System.Drawing.Size(724, 648);
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
            this.label16.Location = new System.Drawing.Point(1, 649);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(724, 1);
            this.label16.TabIndex = 8;
            this.label16.Text = "label2";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(0, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 649);
            this.label17.TabIndex = 7;
            this.label17.Text = "label4";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label18.Location = new System.Drawing.Point(725, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 649);
            this.label18.TabIndex = 3;
            this.label18.Text = "label3";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(726, 1);
            this.label19.TabIndex = 5;
            this.label19.Text = "label1";
            // 
            // pnlEnteTreatment
            // 
            this.pnlEnteTreatment.Controls.Add(this.pnlEnterTreatment);
            this.pnlEnteTreatment.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEnteTreatment.Location = new System.Drawing.Point(1, 1);
            this.pnlEnteTreatment.Name = "pnlEnteTreatment";
            this.pnlEnteTreatment.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlEnteTreatment.Size = new System.Drawing.Size(726, 32);
            this.pnlEnteTreatment.TabIndex = 0;
            // 
            // pnlEnterTreatment
            // 
            this.pnlEnterTreatment.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlEnterTreatment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlEnterTreatment.Controls.Add(this.txtTreatment);
            this.pnlEnterTreatment.Controls.Add(this.lblTreatmentNames);
            this.pnlEnterTreatment.Controls.Add(this.rbICD9);
            this.pnlEnterTreatment.Controls.Add(this.rbICD10);
            this.pnlEnterTreatment.Controls.Add(this.label33);
            this.pnlEnterTreatment.Controls.Add(this.label27);
            this.pnlEnterTreatment.Controls.Add(this.label28);
            this.pnlEnterTreatment.Controls.Add(this.label29);
            this.pnlEnterTreatment.Controls.Add(this.label30);
            this.pnlEnterTreatment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEnterTreatment.Location = new System.Drawing.Point(0, 0);
            this.pnlEnterTreatment.Name = "pnlEnterTreatment";
            this.pnlEnterTreatment.Size = new System.Drawing.Size(726, 29);
            this.pnlEnterTreatment.TabIndex = 0;
            // 
            // txtTreatment
            // 
            this.txtTreatment.Location = new System.Drawing.Point(179, 2);
            this.txtTreatment.Name = "txtTreatment";
            this.txtTreatment.Size = new System.Drawing.Size(316, 22);
            this.txtTreatment.TabIndex = 0;
            // 
            // lblTreatmentNames
            // 
            this.lblTreatmentNames.AutoSize = true;
            this.lblTreatmentNames.BackColor = System.Drawing.Color.Transparent;
            this.lblTreatmentNames.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTreatmentNames.Location = new System.Drawing.Point(18, 6);
            this.lblTreatmentNames.Name = "lblTreatmentNames";
            this.lblTreatmentNames.Size = new System.Drawing.Size(157, 14);
            this.lblTreatmentNames.TabIndex = 59;
            this.lblTreatmentNames.Text = "Smart Treatment Name :";
            this.lblTreatmentNames.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbICD9
            // 
            this.rbICD9.AutoSize = true;
            this.rbICD9.BackColor = System.Drawing.Color.Transparent;
            this.rbICD9.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbICD9.Location = new System.Drawing.Point(616, 1);
            this.rbICD9.Name = "rbICD9";
            this.rbICD9.Size = new System.Drawing.Size(51, 27);
            this.rbICD9.TabIndex = 10;
            this.rbICD9.Text = "ICD9";
            this.rbICD9.UseVisualStyleBackColor = false;
            this.rbICD9.CheckedChanged += new System.EventHandler(this.rbICD9_CheckedChanged);
            // 
            // rbICD10
            // 
            this.rbICD10.AutoSize = true;
            this.rbICD10.BackColor = System.Drawing.Color.Transparent;
            this.rbICD10.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbICD10.Location = new System.Drawing.Point(667, 1);
            this.rbICD10.Name = "rbICD10";
            this.rbICD10.Size = new System.Drawing.Size(58, 27);
            this.rbICD10.TabIndex = 63;
            this.rbICD10.Text = "ICD10";
            this.rbICD10.UseVisualStyleBackColor = false;
            this.rbICD10.CheckedChanged += new System.EventHandler(this.rbICD9_CheckedChanged);
            // 
            // label33
            // 
            this.label33.AutoEllipsis = true;
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.Red;
            this.label33.Location = new System.Drawing.Point(4, 6);
            this.label33.Name = "label33";
            this.label33.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label33.Size = new System.Drawing.Size(14, 14);
            this.label33.TabIndex = 110;
            this.label33.Text = "*";
            this.label33.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label27.Location = new System.Drawing.Point(1, 28);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(724, 1);
            this.label27.TabIndex = 64;
            this.label27.Text = "label2";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(0, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 28);
            this.label28.TabIndex = 63;
            this.label28.Text = "label4";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label29.Location = new System.Drawing.Point(725, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 28);
            this.label29.TabIndex = 2;
            this.label29.Text = "label3";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Top;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(0, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(726, 1);
            this.label30.TabIndex = 61;
            this.label30.Text = "label1";
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(992, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 687);
            this.splitter2.TabIndex = 8;
            this.splitter2.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(261, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 687);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // PnlCPT
            // 
            this.PnlCPT.Controls.Add(this.elementHostICD10);
            this.PnlCPT.Controls.Add(this.pnl_trvCPT);
            this.PnlCPT.Controls.Add(this.pnl_btnICD9);
            this.PnlCPT.Controls.Add(this.pnl_btnModifier);
            this.PnlCPT.Controls.Add(this.pnl_btnCPT);
            this.PnlCPT.Controls.Add(this.pnlSearch);
            this.PnlCPT.Controls.Add(this.pnl_Select);
            this.PnlCPT.Dock = System.Windows.Forms.DockStyle.Right;
            this.PnlCPT.Location = new System.Drawing.Point(995, 0);
            this.PnlCPT.Name = "PnlCPT";
            this.PnlCPT.Size = new System.Drawing.Size(260, 687);
            this.PnlCPT.TabIndex = 2;
            this.PnlCPT.TabStop = true;
            // 
            // elementHostICD10
            // 
            this.elementHostICD10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHostICD10.Location = new System.Drawing.Point(0, 88);
            this.elementHostICD10.Name = "elementHostICD10";
            this.elementHostICD10.Size = new System.Drawing.Size(260, 537);
            this.elementHostICD10.TabIndex = 39;
            this.elementHostICD10.Text = "elementHost1";
            this.elementHostICD10.Visible = false;
            this.elementHostICD10.Child = null;
            // 
            // pnl_trvCPT
            // 
            this.pnl_trvCPT.Controls.Add(this.trvCPT);
            this.pnl_trvCPT.Controls.Add(this.label32);
            this.pnl_trvCPT.Controls.Add(this.label10);
            this.pnl_trvCPT.Controls.Add(this.label11);
            this.pnl_trvCPT.Controls.Add(this.label13);
            this.pnl_trvCPT.Controls.Add(this.label14);
            this.pnl_trvCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_trvCPT.Location = new System.Drawing.Point(0, 88);
            this.pnl_trvCPT.Name = "pnl_trvCPT";
            this.pnl_trvCPT.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnl_trvCPT.Size = new System.Drawing.Size(260, 537);
            this.pnl_trvCPT.TabIndex = 62;
            // 
            // trvCPT
            // 
            this.trvCPT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCPT.CheckBoxes = true;
            this.trvCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCPT.ForeColor = System.Drawing.Color.Black;
            this.trvCPT.FullRowSelect = true;
            this.trvCPT.LineColor = System.Drawing.Color.CadetBlue;
            this.trvCPT.Location = new System.Drawing.Point(1, 5);
            this.trvCPT.Name = "trvCPT";
            this.trvCPT.ShowNodeToolTips = true;
            this.trvCPT.Size = new System.Drawing.Size(255, 528);
            this.trvCPT.TabIndex = 12;
            this.trvCPT.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvCPT_AfterCheck);
            this.trvCPT.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCPT_AfterSelect);
            this.trvCPT.Click += new System.EventHandler(this.trvCPT_Click);
            this.trvCPT.DoubleClick += new System.EventHandler(this.trvCPT_DoubleClick);
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.White;
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Location = new System.Drawing.Point(1, 1);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(255, 4);
            this.label32.TabIndex = 38;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(1, 533);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(255, 1);
            this.label10.TabIndex = 8;
            this.label10.Text = "label2";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 533);
            this.label11.TabIndex = 7;
            this.label11.Text = "label4";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(256, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 533);
            this.label13.TabIndex = 6;
            this.label13.Text = "label3";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(257, 1);
            this.label14.TabIndex = 5;
            this.label14.Text = "label1";
            // 
            // pnl_btnICD9
            // 
            this.pnl_btnICD9.Controls.Add(this.btnICD9);
            this.pnl_btnICD9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_btnICD9.Location = new System.Drawing.Point(0, 625);
            this.pnl_btnICD9.Name = "pnl_btnICD9";
            this.pnl_btnICD9.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnl_btnICD9.Size = new System.Drawing.Size(260, 31);
            this.pnl_btnICD9.TabIndex = 62;
            // 
            // btnICD9
            // 
            this.btnICD9.AutoEllipsis = true;
            this.btnICD9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnICD9.BackgroundImage")));
            this.btnICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnICD9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnICD9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnICD9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnICD9.Location = new System.Drawing.Point(0, 0);
            this.btnICD9.Name = "btnICD9";
            this.btnICD9.Size = new System.Drawing.Size(257, 28);
            this.btnICD9.TabIndex = 55;
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
            this.pnl_btnModifier.Location = new System.Drawing.Point(0, 656);
            this.pnl_btnModifier.Name = "pnl_btnModifier";
            this.pnl_btnModifier.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnl_btnModifier.Size = new System.Drawing.Size(260, 31);
            this.pnl_btnModifier.TabIndex = 62;
            // 
            // btnModifier
            // 
            this.btnModifier.AutoEllipsis = true;
            this.btnModifier.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModifier.BackgroundImage")));
            this.btnModifier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnModifier.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifier.Location = new System.Drawing.Point(0, 0);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(257, 28);
            this.btnModifier.TabIndex = 57;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);
            this.btnModifier.MouseLeave += new System.EventHandler(this.btnModifier_MouseLeave);
            this.btnModifier.MouseHover += new System.EventHandler(this.btnModifier_MouseHover);
            // 
            // pnl_btnCPT
            // 
            this.pnl_btnCPT.Controls.Add(this.btnCPT);
            this.pnl_btnCPT.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_btnCPT.Location = new System.Drawing.Point(0, 57);
            this.pnl_btnCPT.Name = "pnl_btnCPT";
            this.pnl_btnCPT.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnl_btnCPT.Size = new System.Drawing.Size(260, 31);
            this.pnl_btnCPT.TabIndex = 61;
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
            this.btnCPT.Size = new System.Drawing.Size(257, 28);
            this.btnCPT.TabIndex = 11;
            this.btnCPT.Text = "CPT";
            this.btnCPT.UseVisualStyleBackColor = true;
            this.btnCPT.Click += new System.EventHandler(this.btnCPT_Click);
            this.btnCPT.MouseLeave += new System.EventHandler(this.btnCPT_MouseLeave);
            this.btnCPT.MouseHover += new System.EventHandler(this.btnCPT_MouseHover);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearch.Controls.Add(this.txtCPTSearch);
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnlSearch.Controls.Add(this.PicBx_Search);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchBottomBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchTopBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlSearch.Location = new System.Drawing.Point(0, 31);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlSearch.Size = new System.Drawing.Size(260, 26);
            this.pnlSearch.TabIndex = 58;
            // 
            // txtCPTSearch
            // 
            this.txtCPTSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCPTSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCPTSearch.ForeColor = System.Drawing.Color.Black;
            this.txtCPTSearch.Location = new System.Drawing.Point(29, 5);
            this.txtCPTSearch.Name = "txtCPTSearch";
            this.txtCPTSearch.Size = new System.Drawing.Size(227, 15);
            this.txtCPTSearch.TabIndex = 7;
            this.txtCPTSearch.TextChanged += new System.EventHandler(this.txtCPTSearch_TextChanged);
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(29, 1);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(227, 4);
            this.lbl_WhiteSpaceTop.TabIndex = 1;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(29, 20);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(227, 2);
            this.lbl_WhiteSpaceBottom.TabIndex = 2;
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
            this.lbl_pnlSearchBottomBrd.Size = new System.Drawing.Size(255, 1);
            this.lbl_pnlSearchBottomBrd.TabIndex = 35;
            this.lbl_pnlSearchBottomBrd.Text = "label1";
            // 
            // lbl_pnlSearchTopBrd
            // 
            this.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSearchTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd";
            this.lbl_pnlSearchTopBrd.Size = new System.Drawing.Size(255, 1);
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
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(256, 0);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // pnl_Select
            // 
            this.pnl_Select.Controls.Add(this.pnlSelect);
            this.pnl_Select.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Select.Location = new System.Drawing.Point(0, 0);
            this.pnl_Select.Name = "pnl_Select";
            this.pnl_Select.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnl_Select.Size = new System.Drawing.Size(260, 31);
            this.pnl_Select.TabIndex = 59;
            // 
            // pnlSelect
            // 
            this.pnlSelect.BackColor = System.Drawing.Color.Transparent;
            this.pnlSelect.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSelect.Controls.Add(this.lbl_BottomBrd);
            this.pnlSelect.Controls.Add(this.lbl_LeftBrd);
            this.pnlSelect.Controls.Add(this.lbl_RightBrd);
            this.pnlSelect.Controls.Add(this.lbl_TopBrd);
            this.pnlSelect.Controls.Add(this.rbDescription);
            this.pnlSelect.Controls.Add(this.rbCode);
            this.pnlSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSelect.Location = new System.Drawing.Point(0, 0);
            this.pnlSelect.Name = "pnlSelect";
            this.pnlSelect.Size = new System.Drawing.Size(257, 28);
            this.pnlSelect.TabIndex = 57;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 27);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(255, 1);
            this.lbl_BottomBrd.TabIndex = 0;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 27);
            this.lbl_LeftBrd.TabIndex = 2;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(256, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 27);
            this.lbl_RightBrd.TabIndex = 59;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(257, 1);
            this.lbl_TopBrd.TabIndex = 58;
            this.lbl_TopBrd.Text = "label1";
            // 
            // rbDescription
            // 
            this.rbDescription.AutoSize = true;
            this.rbDescription.BackColor = System.Drawing.Color.Transparent;
            this.rbDescription.Checked = true;
            this.rbDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDescription.Location = new System.Drawing.Point(126, 4);
            this.rbDescription.Name = "rbDescription";
            this.rbDescription.Size = new System.Drawing.Size(94, 18);
            this.rbDescription.TabIndex = 6;
            this.rbDescription.TabStop = true;
            this.rbDescription.Text = "Description";
            this.rbDescription.UseVisualStyleBackColor = false;
            // 
            // rbCode
            // 
            this.rbCode.AutoSize = true;
            this.rbCode.BackColor = System.Drawing.Color.Transparent;
            this.rbCode.Location = new System.Drawing.Point(20, 4);
            this.rbCode.Name = "rbCode";
            this.rbCode.Size = new System.Drawing.Size(53, 18);
            this.rbCode.TabIndex = 1;
            this.rbCode.TabStop = true;
            this.rbCode.Text = "Code";
            this.rbCode.UseVisualStyleBackColor = false;
            this.rbCode.CheckedChanged += new System.EventHandler(this.rbCode_CheckedChanged);
            // 
            // pnlTOS
            // 
            this.pnlTOS.Controls.Add(this.pnltrvTreatment);
            this.pnlTOS.Controls.Add(this.pnllblTreatments);
            this.pnlTOS.Controls.Add(this.pnltxtTOSSearch);
            this.pnlTOS.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTOS.Location = new System.Drawing.Point(0, 0);
            this.pnlTOS.Name = "pnlTOS";
            this.pnlTOS.Size = new System.Drawing.Size(261, 687);
            this.pnlTOS.TabIndex = 0;
            this.pnlTOS.TabStop = true;
            this.pnlTOS.Visible = false;
            // 
            // pnltrvTreatment
            // 
            this.pnltrvTreatment.Controls.Add(this.trvTreatment);
            this.pnltrvTreatment.Controls.Add(this.label34);
            this.pnltrvTreatment.Controls.Add(this.label31);
            this.pnltrvTreatment.Controls.Add(this.label12);
            this.pnltrvTreatment.Controls.Add(this.label24);
            this.pnltrvTreatment.Controls.Add(this.label25);
            this.pnltrvTreatment.Controls.Add(this.label26);
            this.pnltrvTreatment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnltrvTreatment.Location = new System.Drawing.Point(0, 62);
            this.pnltrvTreatment.Name = "pnltrvTreatment";
            this.pnltrvTreatment.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltrvTreatment.Size = new System.Drawing.Size(261, 625);
            this.pnltrvTreatment.TabIndex = 0;
            // 
            // trvTreatment
            // 
            this.trvTreatment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvTreatment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTreatment.ForeColor = System.Drawing.Color.Black;
            this.trvTreatment.Indent = 20;
            this.trvTreatment.ItemHeight = 20;
            this.trvTreatment.Location = new System.Drawing.Point(8, 5);
            this.trvTreatment.Name = "trvTreatment";
            this.trvTreatment.Size = new System.Drawing.Size(252, 616);
            this.trvTreatment.TabIndex = 0;
            this.trvTreatment.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTreatment_AfterSelect);
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.White;
            this.label34.Dock = System.Windows.Forms.DockStyle.Left;
            this.label34.Location = new System.Drawing.Point(4, 5);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(4, 616);
            this.label34.TabIndex = 39;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.White;
            this.label31.Dock = System.Windows.Forms.DockStyle.Top;
            this.label31.Location = new System.Drawing.Point(4, 1);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(256, 4);
            this.label31.TabIndex = 38;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label12.Location = new System.Drawing.Point(4, 621);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(256, 1);
            this.label12.TabIndex = 8;
            this.label12.Text = "label2";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(3, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 621);
            this.label24.TabIndex = 7;
            this.label24.Text = "label4";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label25.Location = new System.Drawing.Point(260, 1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 621);
            this.label25.TabIndex = 6;
            this.label25.Text = "label3";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Top;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(3, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(258, 1);
            this.label26.TabIndex = 5;
            this.label26.Text = "label1";
            // 
            // pnllblTreatments
            // 
            this.pnllblTreatments.Controls.Add(this.panel2);
            this.pnllblTreatments.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnllblTreatments.Location = new System.Drawing.Point(0, 30);
            this.pnllblTreatments.Name = "pnllblTreatments";
            this.pnllblTreatments.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnllblTreatments.Size = new System.Drawing.Size(261, 32);
            this.pnllblTreatments.TabIndex = 56;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.lblTreatments);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(258, 29);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 1);
            this.label1.TabIndex = 8;
            this.label1.Text = "label2";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 28);
            this.label5.TabIndex = 7;
            this.label5.Text = "label4";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(257, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 28);
            this.label6.TabIndex = 6;
            this.label6.Text = "label3";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(258, 1);
            this.label7.TabIndex = 5;
            this.label7.Text = "label1";
            // 
            // lblTreatments
            // 
            this.lblTreatments.BackColor = System.Drawing.Color.Transparent;
            this.lblTreatments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTreatments.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTreatments.Location = new System.Drawing.Point(0, 0);
            this.lblTreatments.Name = "lblTreatments";
            this.lblTreatments.Size = new System.Drawing.Size(258, 29);
            this.lblTreatments.TabIndex = 0;
            this.lblTreatments.Text = "Smart Treatments";
            this.lblTreatments.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnltxtTOSSearch
            // 
            this.pnltxtTOSSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltxtTOSSearch.Controls.Add(this.txtTOSSearch);
            this.pnltxtTOSSearch.Controls.Add(this.label4);
            this.pnltxtTOSSearch.Controls.Add(this.label15);
            this.pnltxtTOSSearch.Controls.Add(this.pictureBox1);
            this.pnltxtTOSSearch.Controls.Add(this.label20);
            this.pnltxtTOSSearch.Controls.Add(this.label21);
            this.pnltxtTOSSearch.Controls.Add(this.label22);
            this.pnltxtTOSSearch.Controls.Add(this.label23);
            this.pnltxtTOSSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltxtTOSSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnltxtTOSSearch.ForeColor = System.Drawing.Color.Black;
            this.pnltxtTOSSearch.Location = new System.Drawing.Point(0, 0);
            this.pnltxtTOSSearch.Name = "pnltxtTOSSearch";
            this.pnltxtTOSSearch.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnltxtTOSSearch.Size = new System.Drawing.Size(261, 30);
            this.pnltxtTOSSearch.TabIndex = 0;
            // 
            // txtTOSSearch
            // 
            this.txtTOSSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTOSSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTOSSearch.ForeColor = System.Drawing.Color.Black;
            this.txtTOSSearch.Location = new System.Drawing.Point(32, 8);
            this.txtTOSSearch.Multiline = true;
            this.txtTOSSearch.Name = "txtTOSSearch";
            this.txtTOSSearch.Size = new System.Drawing.Size(228, 16);
            this.txtTOSSearch.TabIndex = 2;
            this.txtTOSSearch.TextChanged += new System.EventHandler(this.txtTOSSearch_TextChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(32, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 4);
            this.label4.TabIndex = 37;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(32, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(228, 2);
            this.label15.TabIndex = 38;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Location = new System.Drawing.Point(4, 26);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(256, 1);
            this.label20.TabIndex = 35;
            this.label20.Text = "label1";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(4, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(256, 1);
            this.label21.TabIndex = 36;
            this.label21.Text = "label1";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Location = new System.Drawing.Point(3, 3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 24);
            this.label22.TabIndex = 39;
            this.label22.Text = "label4";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Location = new System.Drawing.Point(260, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 24);
            this.label23.TabIndex = 40;
            this.label23.Text = "label4";
            // 
            // pnl_SlectAll
            // 
            this.pnl_SlectAll.Controls.Add(this.pnlSearchCriteria);
            this.pnl_SlectAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_SlectAll.Location = new System.Drawing.Point(0, 54);
            this.pnl_SlectAll.Name = "pnl_SlectAll";
            this.pnl_SlectAll.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnl_SlectAll.Size = new System.Drawing.Size(1255, 30);
            this.pnl_SlectAll.TabIndex = 60;
            this.pnl_SlectAll.Visible = false;
            // 
            // pnlSearchCriteria
            // 
            this.pnlSearchCriteria.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlSearchCriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchCriteria.Controls.Add(this.rbModifier);
            this.pnlSearchCriteria.Controls.Add(this.rbCPT);
            this.pnlSearchCriteria.Controls.Add(this.label2);
            this.pnlSearchCriteria.Controls.Add(this.label3);
            this.pnlSearchCriteria.Controls.Add(this.label8);
            this.pnlSearchCriteria.Controls.Add(this.label9);
            this.pnlSearchCriteria.Controls.Add(this.chkBoxSelect);
            this.pnlSearchCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchCriteria.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchCriteria.Name = "pnlSearchCriteria";
            this.pnlSearchCriteria.Size = new System.Drawing.Size(1252, 27);
            this.pnlSearchCriteria.TabIndex = 4;
            // 
            // rbModifier
            // 
            this.rbModifier.AutoSize = true;
            this.rbModifier.BackColor = System.Drawing.Color.Transparent;
            this.rbModifier.Location = new System.Drawing.Point(196, 4);
            this.rbModifier.Name = "rbModifier";
            this.rbModifier.Size = new System.Drawing.Size(51, 18);
            this.rbModifier.TabIndex = 10;
            this.rbModifier.Text = "MOD";
            this.rbModifier.UseVisualStyleBackColor = false;
            this.rbModifier.Visible = false;
            // 
            // rbCPT
            // 
            this.rbCPT.AutoSize = true;
            this.rbCPT.BackColor = System.Drawing.Color.Transparent;
            this.rbCPT.Checked = true;
            this.rbCPT.Location = new System.Drawing.Point(92, 4);
            this.rbCPT.Name = "rbCPT";
            this.rbCPT.Size = new System.Drawing.Size(47, 18);
            this.rbCPT.TabIndex = 9;
            this.rbCPT.TabStop = true;
            this.rbCPT.Text = "CPT";
            this.rbCPT.UseVisualStyleBackColor = false;
            this.rbCPT.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(1, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1250, 1);
            this.label2.TabIndex = 62;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 26);
            this.label3.TabIndex = 1;
            this.label3.Text = "label4";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(1251, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 26);
            this.label8.TabIndex = 60;
            this.label8.Text = "label3";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1252, 1);
            this.label9.TabIndex = 59;
            this.label9.Text = "label1";
            // 
            // chkBoxSelect
            // 
            this.chkBoxSelect.AutoSize = true;
            this.chkBoxSelect.BackColor = System.Drawing.Color.Transparent;
            this.chkBoxSelect.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkBoxSelect.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxSelect.Location = new System.Drawing.Point(9, 4);
            this.chkBoxSelect.Name = "chkBoxSelect";
            this.chkBoxSelect.Size = new System.Drawing.Size(82, 18);
            this.chkBoxSelect.TabIndex = 8;
            this.chkBoxSelect.Text = "Select All";
            this.chkBoxSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBoxSelect.UseVisualStyleBackColor = false;
            this.chkBoxSelect.Click += new System.EventHandler(this.chkBoxSelect_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.ts_Commands);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1255, 54);
            this.pnlTop.TabIndex = 4;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_New,
            this.tsbtnAdd,
            this.tsb_Delete,
            this.tsb_Save,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1255, 53);
            this.ts_Commands.TabIndex = 13;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_New
            // 
            this.tsb_New.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_New.Image = ((System.Drawing.Image)(resources.GetObject("tsb_New.Image")));
            this.tsb_New.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsb_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_New.Name = "tsb_New";
            this.tsb_New.Size = new System.Drawing.Size(37, 50);
            this.tsb_New.Tag = "New";
            this.tsb_New.Text = "&New";
            this.tsb_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_New.Visible = false;
            this.tsb_New.Click += new System.EventHandler(this.tsb_New_Click);
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
            // tsb_Delete
            // 
            this.tsb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Delete.Image = global::gloBilling.Properties.Resources.Ico_Delete;
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(50, 50);
            this.tsb_Delete.Tag = "Delete";
            this.tsb_Delete.Text = "&Delete";
            this.tsb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Delete.Visible = false;
            this.tsb_Delete.Click += new System.EventHandler(this.tsb_Delete_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
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
            // frmSetupSmartTreatment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1255, 741);
            this.Controls.Add(this.pnl_SlectAll);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSetupSmartTreatment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Treatment";
            this.Activated += new System.EventHandler(this.frmSetupSmartTreatment_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSetupSmartTreatment_FormClosed);
            this.Load += new System.EventHandler(this.frmSmartTreatment_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMiddle.ResumeLayout(false);
            this.pnlC1TOSCPT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1TOSCPT)).EndInit();
            this.pnlEnteTreatment.ResumeLayout(false);
            this.pnlEnterTreatment.ResumeLayout(false);
            this.pnlEnterTreatment.PerformLayout();
            this.PnlCPT.ResumeLayout(false);
            this.pnl_trvCPT.ResumeLayout(false);
            this.pnl_btnICD9.ResumeLayout(false);
            this.pnl_btnModifier.ResumeLayout(false);
            this.pnl_btnCPT.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.pnl_Select.ResumeLayout(false);
            this.pnlSelect.ResumeLayout(false);
            this.pnlSelect.PerformLayout();
            this.pnlTOS.ResumeLayout(false);
            this.pnltrvTreatment.ResumeLayout(false);
            this.pnllblTreatments.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnltxtTOSSearch.ResumeLayout(false);
            this.pnltxtTOSSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_SlectAll.ResumeLayout(false);
            this.pnlSearchCriteria.ResumeLayout(false);
            this.pnlSearchCriteria.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
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
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.ContextMenuStrip cxtMS;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ImageList imgLstTOS;
        private System.Windows.Forms.Panel pnlEnterTreatment;
        private System.Windows.Forms.Label lblTreatmentNames;
        private System.Windows.Forms.TextBox txtTreatment;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Panel pnl_Select;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        internal System.Windows.Forms.Panel pnlSearch;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnlSearchBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSearchTopBrd;
        private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        private System.Windows.Forms.Panel pnl_btnModifier;
        private System.Windows.Forms.Panel pnl_btnICD9;
        private System.Windows.Forms.Panel pnl_trvCPT;
        private System.Windows.Forms.Panel pnl_btnCPT;
        private System.Windows.Forms.Panel pnl_SlectAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnlTOS;
        private System.Windows.Forms.Panel pnltrvTreatment;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TreeView trvTreatment;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel pnllblTreatments;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTreatments;
        internal System.Windows.Forms.Panel pnltxtTOSSearch;
        private System.Windows.Forms.TextBox txtTOSSearch;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label15;
        internal System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlC1TOSCPT;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel pnlEnteTreatment;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.RadioButton rbModifier;
        private System.Windows.Forms.RadioButton rbICD9;
        private System.Windows.Forms.RadioButton rbCPT;
        internal System.Windows.Forms.Label label32;
        internal System.Windows.Forms.Label label31;
        internal System.Windows.Forms.ToolStripButton tsb_New;
        internal System.Windows.Forms.ToolStripButton tsb_Delete;
        private System.Windows.Forms.Label label33;
        internal System.Windows.Forms.Label label34;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.RadioButton rbICD10;
        private System.Windows.Forms.Integration.ElementHost elementHostICD10;
        //private System.ComponentModel.BackgroundWorker backgroundSearch;
    }
}