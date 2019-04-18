namespace gloTaskMail
{
    partial class frmViewMail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewMail));
            this.pnl_toolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnReply = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnReplyAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnForward = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnJunk = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnl_Mails = new System.Windows.Forms.Panel();
            this.c1ViewMails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlMyMail = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pic_UpcomingAppt_Provider = new System.Windows.Forms.PictureBox();
            this.pic_UpcomingAppt_Description = new System.Windows.Forms.PictureBox();
            this.c1MailFolders = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlFolders = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMyFolders = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnl_toolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnl_Mails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ViewMails)).BeginInit();
            this.pnlMyMail.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_UpcomingAppt_Provider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_UpcomingAppt_Description)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1MailFolders)).BeginInit();
            this.pnlFolders.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_toolStrip
            // 
            this.pnl_toolStrip.Controls.Add(this.ts_Commands);
            this.pnl_toolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_toolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_toolStrip.Name = "pnl_toolStrip";
            this.pnl_toolStrip.Size = new System.Drawing.Size(1007, 54);
            this.pnl_toolStrip.TabIndex = 1;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnNew,
            this.toolStripSeparator1,
            this.ts_btnReply,
            this.toolStripSeparator2,
            this.ts_btnReplyAll,
            this.toolStripSeparator3,
            this.ts_btnForward,
            this.toolStripSeparator4,
            this.ts_btnDelete,
            this.toolStripSeparator5,
            this.ts_btnJunk});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1007, 54);
            this.ts_Commands.TabIndex = 10;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // ts_btnNew
            // 
            this.ts_btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnNew.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnNew.Image")));
            this.ts_btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnNew.Name = "ts_btnNew";
            this.ts_btnNew.Size = new System.Drawing.Size(37, 50);
            this.ts_btnNew.Tag = "New";
            this.ts_btnNew.Text = "New";
            this.ts_btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // ts_btnReply
            // 
            this.ts_btnReply.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnReply.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnReply.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnReply.Image")));
            this.ts_btnReply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnReply.Name = "ts_btnReply";
            this.ts_btnReply.Size = new System.Drawing.Size(45, 50);
            this.ts_btnReply.Tag = "Reply";
            this.ts_btnReply.Text = "Reply";
            this.ts_btnReply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // ts_btnReplyAll
            // 
            this.ts_btnReplyAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnReplyAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnReplyAll.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnReplyAll.Image")));
            this.ts_btnReplyAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnReplyAll.Name = "ts_btnReplyAll";
            this.ts_btnReplyAll.Size = new System.Drawing.Size(64, 50);
            this.ts_btnReplyAll.Tag = "ReplyAll";
            this.ts_btnReplyAll.Text = "Reply All";
            this.ts_btnReplyAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // ts_btnForward
            // 
            this.ts_btnForward.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnForward.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnForward.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnForward.Image")));
            this.ts_btnForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnForward.Name = "ts_btnForward";
            this.ts_btnForward.Size = new System.Drawing.Size(61, 50);
            this.ts_btnForward.Tag = "Forward";
            this.ts_btnForward.Text = "Forward";
            this.ts_btnForward.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 54);
            // 
            // ts_btnDelete
            // 
            this.ts_btnDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnDelete.Image")));
            this.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnDelete.Name = "ts_btnDelete";
            this.ts_btnDelete.Size = new System.Drawing.Size(50, 50);
            this.ts_btnDelete.Tag = "Delete";
            this.ts_btnDelete.Text = "Delete";
            this.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 54);
            // 
            // ts_btnJunk
            // 
            this.ts_btnJunk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnJunk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnJunk.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnJunk.Image")));
            this.ts_btnJunk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnJunk.Name = "ts_btnJunk";
            this.ts_btnJunk.Size = new System.Drawing.Size(40, 50);
            this.ts_btnJunk.Tag = "Junk";
            this.ts_btnJunk.Text = "Junk";
            this.ts_btnJunk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnl_Mails);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Controls.Add(this.lbl_BottomBrd);
            this.pnlMain.Controls.Add(this.lbl_LeftBrd);
            this.pnlMain.Controls.Add(this.lbl_RightBrd);
            this.pnlMain.Controls.Add(this.lbl_TopBrd);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(1007, 692);
            this.pnlMain.TabIndex = 2;
            // 
            // pnl_Mails
            // 
            this.pnl_Mails.Controls.Add(this.c1ViewMails);
            this.pnl_Mails.Controls.Add(this.pnlMyMail);
            this.pnl_Mails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Mails.Location = new System.Drawing.Point(320, 4);
            this.pnl_Mails.Name = "pnl_Mails";
            this.pnl_Mails.Size = new System.Drawing.Size(683, 684);
            this.pnl_Mails.TabIndex = 15;
            // 
            // c1ViewMails
            // 
            this.c1ViewMails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ViewMails.AllowEditing = false;
            this.c1ViewMails.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1ViewMails.AutoGenerateColumns = false;
            this.c1ViewMails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.c1ViewMails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ViewMails.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1ViewMails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ViewMails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(68)))), ((int)(((byte)(65)))));
            this.c1ViewMails.Location = new System.Drawing.Point(0, 23);
            this.c1ViewMails.Name = "c1ViewMails";
            this.c1ViewMails.Rows.Count = 1;
            this.c1ViewMails.Rows.DefaultSize = 19;
            this.c1ViewMails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ViewMails.Size = new System.Drawing.Size(683, 661);
            this.c1ViewMails.StyleInfo = resources.GetString("c1ViewMails.StyleInfo");
            this.c1ViewMails.TabIndex = 13;
            // 
            // pnlMyMail
            // 
            this.pnlMyMail.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Button;
            this.pnlMyMail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMyMail.Controls.Add(this.label2);
            this.pnlMyMail.Controls.Add(this.label12);
            this.pnlMyMail.Controls.Add(this.lblDateTime);
            this.pnlMyMail.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMyMail.Location = new System.Drawing.Point(0, 0);
            this.pnlMyMail.Name = "pnlMyMail";
            this.pnlMyMail.Size = new System.Drawing.Size(683, 23);
            this.pnlMyMail.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(0, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(683, 1);
            this.label2.TabIndex = 20;
            this.label2.Text = "label2";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(683, 23);
            this.label12.TabIndex = 1;
            this.label12.Tag = "";
            this.label12.Text = " My Mail";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateTime
            // 
            this.lblDateTime.BackColor = System.Drawing.Color.Transparent;
            this.lblDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDateTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblDateTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDateTime.Location = new System.Drawing.Point(0, 0);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(683, 23);
            this.lblDateTime.TabIndex = 3;
            this.lblDateTime.Text = "   ";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Controls.Add(this.pic_UpcomingAppt_Provider);
            this.pnlLeft.Controls.Add(this.pic_UpcomingAppt_Description);
            this.pnlLeft.Controls.Add(this.c1MailFolders);
            this.pnlLeft.Controls.Add(this.pnlFolders);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(4, 4);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(316, 684);
            this.pnlLeft.TabIndex = 12;
            // 
            // pic_UpcomingAppt_Provider
            // 
            this.pic_UpcomingAppt_Provider.Image = ((System.Drawing.Image)(resources.GetObject("pic_UpcomingAppt_Provider.Image")));
            this.pic_UpcomingAppt_Provider.Location = new System.Drawing.Point(161, 354);
            this.pic_UpcomingAppt_Provider.Name = "pic_UpcomingAppt_Provider";
            this.pic_UpcomingAppt_Provider.Size = new System.Drawing.Size(23, 26);
            this.pic_UpcomingAppt_Provider.TabIndex = 7;
            this.pic_UpcomingAppt_Provider.TabStop = false;
            this.pic_UpcomingAppt_Provider.Visible = false;
            // 
            // pic_UpcomingAppt_Description
            // 
            this.pic_UpcomingAppt_Description.Image = ((System.Drawing.Image)(resources.GetObject("pic_UpcomingAppt_Description.Image")));
            this.pic_UpcomingAppt_Description.Location = new System.Drawing.Point(131, 354);
            this.pic_UpcomingAppt_Description.Name = "pic_UpcomingAppt_Description";
            this.pic_UpcomingAppt_Description.Size = new System.Drawing.Size(23, 26);
            this.pic_UpcomingAppt_Description.TabIndex = 6;
            this.pic_UpcomingAppt_Description.TabStop = false;
            this.pic_UpcomingAppt_Description.Visible = false;
            // 
            // c1MailFolders
            // 
            this.c1MailFolders.BackColor = System.Drawing.Color.White;
            this.c1MailFolders.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1MailFolders.ColumnInfo = "10,0,0,0,0,90,Columns:";
            this.c1MailFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1MailFolders.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1MailFolders.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1MailFolders.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1MailFolders.Location = new System.Drawing.Point(0, 23);
            this.c1MailFolders.Name = "c1MailFolders";
            this.c1MailFolders.Rows.Count = 5;
            this.c1MailFolders.Rows.DefaultSize = 18;
            this.c1MailFolders.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1MailFolders.Size = new System.Drawing.Size(316, 661);
            this.c1MailFolders.StyleInfo = resources.GetString("c1MailFolders.StyleInfo");
            this.c1MailFolders.TabIndex = 4;
            this.c1MailFolders.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1MailFolders.Tree.NodeImageCollapsed")));
            this.c1MailFolders.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1MailFolders.Tree.NodeImageExpanded")));
            this.c1MailFolders.SelChange += new System.EventHandler(this.c1MailFolders_SelChange);
            // 
            // pnlFolders
            // 
            this.pnlFolders.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Button;
            this.pnlFolders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFolders.Controls.Add(this.label1);
            this.pnlFolders.Controls.Add(this.lblMyFolders);
            this.pnlFolders.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFolders.Location = new System.Drawing.Point(0, 0);
            this.pnlFolders.Name = "pnlFolders";
            this.pnlFolders.Size = new System.Drawing.Size(316, 23);
            this.pnlFolders.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(0, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 1);
            this.label1.TabIndex = 20;
            this.label1.Text = "label2";
            // 
            // lblMyFolders
            // 
            this.lblMyFolders.BackColor = System.Drawing.Color.Transparent;
            this.lblMyFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMyFolders.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMyFolders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblMyFolders.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMyFolders.Location = new System.Drawing.Point(0, 0);
            this.lblMyFolders.Name = "lblMyFolders";
            this.lblMyFolders.Size = new System.Drawing.Size(316, 23);
            this.lblMyFolders.TabIndex = 1;
            this.lblMyFolders.Tag = "";
            this.lblMyFolders.Text = " My Folders";
            this.lblMyFolders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 688);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(999, 1);
            this.lbl_BottomBrd.TabIndex = 19;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 685);
            this.lbl_LeftBrd.TabIndex = 18;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(1003, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 685);
            this.lbl_RightBrd.TabIndex = 17;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(1001, 1);
            this.lbl_TopBrd.TabIndex = 16;
            this.lbl_TopBrd.Text = "label1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Inbox.ico");
            this.imageList1.Images.SetKeyName(1, "Sent.ico");
            this.imageList1.Images.SetKeyName(2, "Delete.ico");
            this.imageList1.Images.SetKeyName(3, "ReadMeassage_03.ico");
            this.imageList1.Images.SetKeyName(4, "UnreadMeassage_03.ico");
            this.imageList1.Images.SetKeyName(5, "Attachment_01.ico");
            this.imageList1.Images.SetKeyName(6, "Drafts.ico");
            // 
            // frmViewMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1007, 746);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnl_toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewMail";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "View Mail";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.frmViewMail_Shown);
            this.Load += new System.EventHandler(this.frmViewMail_Load);
            this.pnl_toolStrip.ResumeLayout(false);
            this.pnl_toolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnl_Mails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ViewMails)).EndInit();
            this.pnlMyMail.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_UpcomingAppt_Provider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_UpcomingAppt_Description)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1MailFolders)).EndInit();
            this.pnlFolders.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_toolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnl_Mails;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ViewMails;
        private C1.Win.C1FlexGrid.C1FlexGrid c1MailFolders;
        private System.Windows.Forms.PictureBox pic_UpcomingAppt_Provider;
        private System.Windows.Forms.PictureBox pic_UpcomingAppt_Description;
        private System.Windows.Forms.ToolStripButton ts_btnNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ts_btnReply;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ts_btnReplyAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton ts_btnForward;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton ts_btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton ts_btnJunk;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel pnlMyMail;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Panel pnlFolders;
        private System.Windows.Forms.Label lblMyFolders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
    }
}