namespace gloTaskMail
{
    partial class frmViewUserMail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewUserMail));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_AssociatePatient = new System.Windows.Forms.ToolStripButton();
            this.tsb_UserMails = new System.Windows.Forms.ToolStripButton();
            this.tsb_PatientMails = new System.Windows.Forms.ToolStripButton();
            this.tbs_Receive = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlMailList = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.c1PatientMails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1Mails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMailsHeader = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlViewMail = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlflow_Attachment = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlMailDetails = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblFromHeader = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblDateHeader = new System.Windows.Forms.Label();
            this.lblSubjectHeader = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.imgMail = new System.Windows.Forms.ImageList(this.components);
            this.pnlAssociatePatient = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.ts_Commands.SuspendLayout();
            this.pnlMailList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientMails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Mails)).BeginInit();
            this.pnlViewMail.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlMailDetails.SuspendLayout();
            this.pnlAssociatePatient.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_AssociatePatient,
            this.tsb_UserMails,
            this.tsb_PatientMails,
            this.tbs_Receive,
            this.tsb_Refresh,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1077, 53);
            this.ts_Commands.TabIndex = 11;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_AssociatePatient
            // 
            this.tsb_AssociatePatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_AssociatePatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_AssociatePatient.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AssociatePatient.Image")));
            this.tsb_AssociatePatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AssociatePatient.Name = "tsb_AssociatePatient";
            this.tsb_AssociatePatient.Size = new System.Drawing.Size(118, 50);
            this.tsb_AssociatePatient.Tag = "AssociatePatient";
            this.tsb_AssociatePatient.Text = "&Associate Patient";
            this.tsb_AssociatePatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_AssociatePatient.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.tsb_AssociatePatient.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // tsb_UserMails
            // 
            this.tsb_UserMails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_UserMails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_UserMails.Image = ((System.Drawing.Image)(resources.GetObject("tsb_UserMails.Image")));
            this.tsb_UserMails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_UserMails.Name = "tsb_UserMails";
            this.tsb_UserMails.Size = new System.Drawing.Size(65, 50);
            this.tsb_UserMails.Tag = "UserMails";
            this.tsb_UserMails.Text = "&User Mail";
            this.tsb_UserMails.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_PatientMails
            // 
            this.tsb_PatientMails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PatientMails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PatientMails.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatientMails.Image")));
            this.tsb_PatientMails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatientMails.Name = "tsb_PatientMails";
            this.tsb_PatientMails.Size = new System.Drawing.Size(84, 50);
            this.tsb_PatientMails.Tag = "PatientMails";
            this.tsb_PatientMails.Text = "&Patient Mail";
            this.tsb_PatientMails.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbs_Receive
            // 
            this.tbs_Receive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbs_Receive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tbs_Receive.Image = ((System.Drawing.Image)(resources.GetObject("tbs_Receive.Image")));
            this.tbs_Receive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbs_Receive.Name = "tbs_Receive";
            this.tbs_Receive.Size = new System.Drawing.Size(57, 50);
            this.tbs_Receive.Tag = "Receive";
            this.tbs_Receive.Text = "Re&ceive";
            this.tbs_Receive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbs_Receive.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.tbs_Receive.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Refresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Refresh.Image")));
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tsb_Refresh.Tag = "Refresh";
            this.tsb_Refresh.Text = "&Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refresh.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.tsb_Refresh.MouseHover += new System.EventHandler(this.btn_MouseHover);
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
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.tsb_Close.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlMailList
            // 
            this.pnlMailList.Controls.Add(this.label1);
            this.pnlMailList.Controls.Add(this.c1PatientMails);
            this.pnlMailList.Controls.Add(this.c1Mails);
            this.pnlMailList.Controls.Add(this.label2);
            this.pnlMailList.Controls.Add(this.label13);
            this.pnlMailList.Controls.Add(this.label9);
            this.pnlMailList.Controls.Add(this.label14);
            this.pnlMailList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMailList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMailList.Location = new System.Drawing.Point(0, 28);
            this.pnlMailList.Name = "pnlMailList";
            this.pnlMailList.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMailList.Size = new System.Drawing.Size(1077, 163);
            this.pnlMailList.TabIndex = 28;
            this.pnlMailList.Resize += new System.EventHandler(this.pnlMailList_Resize);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(1073, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 157);
            this.label1.TabIndex = 23;
            // 
            // c1PatientMails
            // 
            this.c1PatientMails.AllowEditing = false;
            this.c1PatientMails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientMails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientMails.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1PatientMails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientMails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientMails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientMails.Location = new System.Drawing.Point(4, 2);
            this.c1PatientMails.Name = "c1PatientMails";
            this.c1PatientMails.Rows.DefaultSize = 19;
            this.c1PatientMails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientMails.Size = new System.Drawing.Size(1070, 157);
            this.c1PatientMails.StyleInfo = resources.GetString("c1PatientMails.StyleInfo");
            this.c1PatientMails.TabIndex = 41;
            this.c1PatientMails.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1PatientMails_MouseMove);
            this.c1PatientMails.Click += new System.EventHandler(this.c1PatientMails_Click);
            // 
            // c1Mails
            // 
            this.c1Mails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Mails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Mails.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1Mails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Mails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Mails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Mails.Location = new System.Drawing.Point(4, 2);
            this.c1Mails.Name = "c1Mails";
            this.c1Mails.Rows.DefaultSize = 19;
            this.c1Mails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Mails.Size = new System.Drawing.Size(1070, 157);
            this.c1Mails.StyleInfo = resources.GetString("c1Mails.StyleInfo");
            this.c1Mails.TabIndex = 25;
            this.c1Mails.Tree.LineColor = System.Drawing.Color.Black;
            this.c1Mails.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.c1Mails.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Lines;
            this.c1Mails.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1Mails_KeyPress);
            this.c1Mails.Click += new System.EventHandler(this.c1Mails_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1070, 1);
            this.label2.TabIndex = 24;
            // 
            // lblMailsHeader
            // 
            this.lblMailsHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblMailsHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMailsHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMailsHeader.ForeColor = System.Drawing.Color.White;
            this.lblMailsHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMailsHeader.Location = new System.Drawing.Point(1, 1);
            this.lblMailsHeader.Name = "lblMailsHeader";
            this.lblMailsHeader.Size = new System.Drawing.Size(1069, 20);
            this.lblMailsHeader.TabIndex = 1;
            this.lblMailsHeader.Tag = "";
            this.lblMailsHeader.Text = "  User Mail";
            this.lblMailsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(3, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 158);
            this.label13.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(3, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1071, 1);
            this.label9.TabIndex = 45;
            // 
            // pnlViewMail
            // 
            this.pnlViewMail.Controls.Add(this.panel2);
            this.pnlViewMail.Controls.Add(this.pnlflow_Attachment);
            this.pnlViewMail.Controls.Add(this.pnlMailDetails);
            this.pnlViewMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewMail.Location = new System.Drawing.Point(0, 0);
            this.pnlViewMail.Name = "pnlViewMail";
            this.pnlViewMail.Size = new System.Drawing.Size(1077, 414);
            this.pnlViewMail.TabIndex = 29;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.webBrowser1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(1077, 334);
            this.panel2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(4, 330);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1069, 1);
            this.label3.TabIndex = 8;
            this.label3.Text = "label2";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(4, 1);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(23, 22);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1069, 330);
            this.webBrowser1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 330);
            this.label4.TabIndex = 7;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(1073, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 330);
            this.label5.TabIndex = 6;
            this.label5.Text = "label3";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1071, 1);
            this.label6.TabIndex = 5;
            this.label6.Text = "label1";
            // 
            // pnlflow_Attachment
            // 
            this.pnlflow_Attachment.AutoSize = true;
            this.pnlflow_Attachment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlflow_Attachment.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlflow_Attachment.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlflow_Attachment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlflow_Attachment.Location = new System.Drawing.Point(0, 80);
            this.pnlflow_Attachment.Name = "pnlflow_Attachment";
            this.pnlflow_Attachment.Size = new System.Drawing.Size(1077, 0);
            this.pnlflow_Attachment.TabIndex = 1;
            // 
            // pnlMailDetails
            // 
            this.pnlMailDetails.Controls.Add(this.lbl_BottomBrd);
            this.pnlMailDetails.Controls.Add(this.lbl_LeftBrd);
            this.pnlMailDetails.Controls.Add(this.lbl_RightBrd);
            this.pnlMailDetails.Controls.Add(this.lbl_TopBrd);
            this.pnlMailDetails.Controls.Add(this.lblFrom);
            this.pnlMailDetails.Controls.Add(this.lblFromHeader);
            this.pnlMailDetails.Controls.Add(this.lblSubject);
            this.pnlMailDetails.Controls.Add(this.lblDate);
            this.pnlMailDetails.Controls.Add(this.lblDateHeader);
            this.pnlMailDetails.Controls.Add(this.lblSubjectHeader);
            this.pnlMailDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMailDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlMailDetails.Name = "pnlMailDetails";
            this.pnlMailDetails.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMailDetails.Size = new System.Drawing.Size(1077, 80);
            this.pnlMailDetails.TabIndex = 2;
            this.pnlMailDetails.Visible = false;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 76);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(1069, 1);
            this.lbl_BottomBrd.TabIndex = 9;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 76);
            this.lbl_LeftBrd.TabIndex = 8;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(1073, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 76);
            this.lbl_RightBrd.TabIndex = 7;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(1071, 1);
            this.lbl_TopBrd.TabIndex = 6;
            this.lbl_TopBrd.Text = "label1";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(76, 53);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(11, 14);
            this.lblFrom.TabIndex = 5;
            this.lblFrom.Text = " ";
            // 
            // lblFromHeader
            // 
            this.lblFromHeader.AutoSize = true;
            this.lblFromHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromHeader.Location = new System.Drawing.Point(30, 52);
            this.lblFromHeader.Name = "lblFromHeader";
            this.lblFromHeader.Size = new System.Drawing.Size(42, 14);
            this.lblFromHeader.TabIndex = 4;
            this.lblFromHeader.Text = "From :";
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubject.Location = new System.Drawing.Point(76, 11);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(11, 14);
            this.lblSubject.TabIndex = 3;
            this.lblSubject.Text = " ";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(76, 32);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(11, 14);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = " ";
            // 
            // lblDateHeader
            // 
            this.lblDateHeader.AutoSize = true;
            this.lblDateHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateHeader.Location = new System.Drawing.Point(31, 31);
            this.lblDateHeader.Name = "lblDateHeader";
            this.lblDateHeader.Size = new System.Drawing.Size(41, 14);
            this.lblDateHeader.TabIndex = 1;
            this.lblDateHeader.Text = "Date :";
            // 
            // lblSubjectHeader
            // 
            this.lblSubjectHeader.AutoSize = true;
            this.lblSubjectHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubjectHeader.Location = new System.Drawing.Point(15, 10);
            this.lblSubjectHeader.Name = "lblSubjectHeader";
            this.lblSubjectHeader.Size = new System.Drawing.Size(57, 14);
            this.lblSubjectHeader.TabIndex = 0;
            this.lblSubjectHeader.Text = "Subject :";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            // 
            // imgMail
            // 
            this.imgMail.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMail.ImageStream")));
            this.imgMail.TransparentColor = System.Drawing.Color.Transparent;
            this.imgMail.Images.SetKeyName(0, "Attachment.ico");
            this.imgMail.Images.SetKeyName(1, "Attachment_01.ico");
            // 
            // pnlAssociatePatient
            // 
            this.pnlAssociatePatient.Controls.Add(this.pnlViewMail);
            this.pnlAssociatePatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAssociatePatient.Location = new System.Drawing.Point(0, 245);
            this.pnlAssociatePatient.Name = "pnlAssociatePatient";
            this.pnlAssociatePatient.Size = new System.Drawing.Size(1077, 414);
            this.pnlAssociatePatient.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ts_Commands);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1077, 54);
            this.panel1.TabIndex = 46;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlMailList);
            this.panel3.Controls.Add(this.Panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 54);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1077, 191);
            this.panel3.TabIndex = 47;
            // 
            // Panel4
            // 
            this.Panel4.Controls.Add(this.panel5);
            this.Panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel4.Location = new System.Drawing.Point(0, 0);
            this.Panel4.Name = "Panel4";
            this.Panel4.Padding = new System.Windows.Forms.Padding(3);
            this.Panel4.Size = new System.Drawing.Size(1077, 28);
            this.Panel4.TabIndex = 21;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.lblMailsHeader);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1071, 22);
            this.panel5.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(1, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1069, 1);
            this.label7.TabIndex = 8;
            this.label7.Text = "label2";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 21);
            this.label8.TabIndex = 7;
            this.label8.Text = "label4";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(1070, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 21);
            this.label10.TabIndex = 6;
            this.label10.Text = "label3";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1071, 1);
            this.label11.TabIndex = 5;
            this.label11.Text = "label1";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Location = new System.Drawing.Point(3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1071, 1);
            this.label14.TabIndex = 25;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmViewUserMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1077, 659);
            this.Controls.Add(this.pnlAssociatePatient);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewUserMail";
            this.Text = "Mail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmViewUserMail_FormClosing);
            this.Load += new System.EventHandler(this.frmViewUserMail_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMailList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientMails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Mails)).EndInit();
            this.pnlViewMail.ResumeLayout(false);
            this.pnlViewMail.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlMailDetails.ResumeLayout(false);
            this.pnlMailDetails.PerformLayout();
            this.pnlAssociatePatient.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_AssociatePatient;
        internal System.Windows.Forms.ToolStripButton tsb_Refresh;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        internal System.Windows.Forms.ToolStripButton tbs_Receive;
        private System.Windows.Forms.Panel pnlMailList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMailsHeader;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Mails;
        private System.Windows.Forms.Panel pnlViewMail;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientMails;
        private System.Windows.Forms.FlowLayoutPanel pnlflow_Attachment;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ImageList imgMail;
        private System.Windows.Forms.Panel pnlMailDetails;
        private System.Windows.Forms.Label lblSubjectHeader;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDateHeader;
        private System.Windows.Forms.Panel pnlAssociatePatient;
        internal System.Windows.Forms.ToolStripButton tsb_UserMails;
        internal System.Windows.Forms.ToolStripButton tsb_PatientMails;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblFromHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}