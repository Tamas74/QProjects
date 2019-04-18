namespace gloEmdeonInterface.Forms
{
    partial class frmMergeOrderPreview
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
                try
                {
                    if (gloUC_Preview != null)
                    {
                        gloUC_Preview.Dispose();
                        gloUC_Preview = null;
                    }
                }
                catch
                {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMergeOrderPreview));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.toolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnPreviewMerge = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.MainSplitPanel = new System.Windows.Forms.SplitContainer();
            this.pnlLabels = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblProvider = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblAcknowledged = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblOrdered = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlStep4Text = new System.Windows.Forms.Panel();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.lblTags = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.pnlUserControl = new System.Windows.Forms.Panel();
            this.gloUC_Preview = new gloUserControlLibrary.gloUC_TransactionHistory();
            this.pnlToolStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitPanel)).BeginInit();
            this.MainSplitPanel.Panel1.SuspendLayout();
            this.MainSplitPanel.Panel2.SuspendLayout();
            this.MainSplitPanel.SuspendLayout();
            this.pnlLabels.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlStep4Text.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.pnlUserControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.toolStrip);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1264, 54);
            this.pnlToolStrip.TabIndex = 4;
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip.BackgroundImage")));
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnPreviewMerge,
            this.ts_btnClose});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1264, 53);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // ts_btnPreviewMerge
            // 
            this.ts_btnPreviewMerge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnPreviewMerge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnPreviewMerge.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnPreviewMerge.Image")));
            this.ts_btnPreviewMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnPreviewMerge.Name = "ts_btnPreviewMerge";
            this.ts_btnPreviewMerge.Size = new System.Drawing.Size(53, 50);
            this.ts_btnPreviewMerge.Tag = "Accept";
            this.ts_btnPreviewMerge.Text = "Accept";
            this.ts_btnPreviewMerge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnPreviewMerge.ToolTipText = "Accept";
            this.ts_btnPreviewMerge.Click += new System.EventHandler(this.AcceptMerge);
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(50, 50);
            this.ts_btnClose.Tag = "Cancel";
            this.ts_btnClose.Text = "Cancel";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.ToolTipText = "Cancel";
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // MainSplitPanel
            // 
            this.MainSplitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MainSplitPanel.IsSplitterFixed = true;
            this.MainSplitPanel.Location = new System.Drawing.Point(0, 54);
            this.MainSplitPanel.Name = "MainSplitPanel";
            this.MainSplitPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainSplitPanel.Panel1
            // 
            this.MainSplitPanel.Panel1.Controls.Add(this.pnlLabels);
            this.MainSplitPanel.Panel1.Controls.Add(this.pnlStep4Text);
            this.MainSplitPanel.Panel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            // 
            // MainSplitPanel.Panel2
            // 
            this.MainSplitPanel.Panel2.Controls.Add(this.pnlUserControl);
            this.MainSplitPanel.Size = new System.Drawing.Size(1264, 750);
            this.MainSplitPanel.SplitterDistance = 85;
            this.MainSplitPanel.TabIndex = 7;
            // 
            // pnlLabels
            // 
            this.pnlLabels.BackColor = System.Drawing.Color.Transparent;
            this.pnlLabels.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLabels.Controls.Add(this.pnlRight);
            this.pnlLabels.Controls.Add(this.pnlMiddle);
            this.pnlLabels.Controls.Add(this.pnlLeft);
            this.pnlLabels.Controls.Add(this.label2);
            this.pnlLabels.Controls.Add(this.label3);
            this.pnlLabels.Controls.Add(this.label4);
            this.pnlLabels.Controls.Add(this.label5);
            this.pnlLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLabels.Location = new System.Drawing.Point(3, 31);
            this.pnlLabels.Name = "pnlLabels";
            this.pnlLabels.Size = new System.Drawing.Size(1258, 54);
            this.pnlLabels.TabIndex = 20;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.lblProvider);
            this.pnlRight.Controls.Add(this.label9);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(921, 1);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(336, 52);
            this.pnlRight.TabIndex = 26;
            // 
            // lblProvider
            // 
            this.lblProvider.BackColor = System.Drawing.Color.Transparent;
            this.lblProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProvider.Location = new System.Drawing.Point(80, 2);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(168, 23);
            this.lblProvider.TabIndex = 33;
            this.lblProvider.Text = "provider";
            this.lblProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(14, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 14);
            this.label9.TabIndex = 32;
            this.label9.Text = "Provider : ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.lblStatus);
            this.pnlMiddle.Controls.Add(this.lblAcknowledged);
            this.pnlMiddle.Controls.Add(this.label8);
            this.pnlMiddle.Controls.Add(this.label13);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMiddle.Location = new System.Drawing.Point(442, 1);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Size = new System.Drawing.Size(479, 52);
            this.pnlMiddle.TabIndex = 24;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblStatus.Location = new System.Drawing.Point(138, 2);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(334, 23);
            this.lblStatus.TabIndex = 36;
            this.lblStatus.Text = "order status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAcknowledged
            // 
            this.lblAcknowledged.BackColor = System.Drawing.Color.Transparent;
            this.lblAcknowledged.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcknowledged.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAcknowledged.Location = new System.Drawing.Point(138, 24);
            this.lblAcknowledged.Name = "lblAcknowledged";
            this.lblAcknowledged.Size = new System.Drawing.Size(168, 23);
            this.lblAcknowledged.TabIndex = 35;
            this.lblAcknowledged.Text = "Ack";
            this.lblAcknowledged.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(30, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 14);
            this.label8.TabIndex = 33;
            this.label8.Text = "Acknowledged : ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(41, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 14);
            this.label13.TabIndex = 32;
            this.label13.Text = "Order Status : ";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lblOrdered);
            this.pnlLeft.Controls.Add(this.label7);
            this.pnlLeft.Controls.Add(this.label1);
            this.pnlLeft.Controls.Add(this.lblOrderNo);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(1, 1);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(441, 52);
            this.pnlLeft.TabIndex = 23;
            // 
            // lblOrdered
            // 
            this.lblOrdered.BackColor = System.Drawing.Color.Transparent;
            this.lblOrdered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdered.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOrdered.Location = new System.Drawing.Point(84, 24);
            this.lblOrdered.Name = "lblOrdered";
            this.lblOrdered.Size = new System.Drawing.Size(349, 23);
            this.lblOrdered.TabIndex = 19;
            this.lblOrdered.Text = "ordered";
            this.lblOrdered.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(17, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 14);
            this.label7.TabIndex = 18;
            this.label7.Text = "Ordered : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(32, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "Order : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.BackColor = System.Drawing.Color.Transparent;
            this.lblOrderNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOrderNo.Location = new System.Drawing.Point(84, 2);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(352, 23);
            this.lblOrderNo.TabIndex = 17;
            this.lblOrderNo.Text = "Order";
            this.lblOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(1, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1256, 1);
            this.label2.TabIndex = 12;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 53);
            this.label3.TabIndex = 11;
            this.label3.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(1257, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 53);
            this.label4.TabIndex = 10;
            this.label4.Text = "label3";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1258, 1);
            this.label5.TabIndex = 9;
            this.label5.Text = "label1";
            // 
            // pnlStep4Text
            // 
            this.pnlStep4Text.Controls.Add(this.pnlContainer);
            this.pnlStep4Text.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep4Text.Location = new System.Drawing.Point(3, 3);
            this.pnlStep4Text.Name = "pnlStep4Text";
            this.pnlStep4Text.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlStep4Text.Size = new System.Drawing.Size(1258, 28);
            this.pnlStep4Text.TabIndex = 17;
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlContainer.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.pnlContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlContainer.Controls.Add(this.lblTags);
            this.pnlContainer.Controls.Add(this.label34);
            this.pnlContainer.Controls.Add(this.label35);
            this.pnlContainer.Controls.Add(this.label36);
            this.pnlContainer.Controls.Add(this.label38);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1258, 25);
            this.pnlContainer.TabIndex = 0;
            // 
            // lblTags
            // 
            this.lblTags.BackColor = System.Drawing.Color.Transparent;
            this.lblTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTags.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTags.ForeColor = System.Drawing.Color.Black;
            this.lblTags.Location = new System.Drawing.Point(1, 1);
            this.lblTags.Name = "lblTags";
            this.lblTags.Size = new System.Drawing.Size(1256, 23);
            this.lblTags.TabIndex = 2;
            this.lblTags.Text = "  Step 4: Review Order Tests.  Finalize Merge.";
            this.lblTags.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label34.Location = new System.Drawing.Point(1, 24);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1256, 1);
            this.label34.TabIndex = 12;
            this.label34.Text = "label2";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Left;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(0, 1);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 24);
            this.label35.TabIndex = 11;
            this.label35.Text = "label4";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Right;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label36.Location = new System.Drawing.Point(1257, 1);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 24);
            this.label36.TabIndex = 10;
            this.label36.Text = "label3";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Top;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1258, 1);
            this.label38.TabIndex = 9;
            this.label38.Text = "label1";
            // 
            // pnlUserControl
            // 
            this.pnlUserControl.Controls.Add(this.gloUC_Preview);
            this.pnlUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUserControl.Location = new System.Drawing.Point(0, 0);
            this.pnlUserControl.Name = "pnlUserControl";
            this.pnlUserControl.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlUserControl.Size = new System.Drawing.Size(1264, 661);
            this.pnlUserControl.TabIndex = 6;
            // 
            // gloUC_Preview
            // 
            this.gloUC_Preview.AutoScroll = true;
            this.gloUC_Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUC_Preview.CurOrderID = ((long)(0));
            this.gloUC_Preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloUC_Preview.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUC_Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUC_Preview.HideCloseButton = false;
            this.gloUC_Preview.Location = new System.Drawing.Point(3, 0);
            this.gloUC_Preview.MergeOrderID = ((long)(0));
            this.gloUC_Preview.Name = "gloUC_Preview";
            this.gloUC_Preview.Size = new System.Drawing.Size(1258, 658);
            this.gloUC_Preview.TabIndex = 2;
            // 
            // frmMergeOrderPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1264, 804);
            this.Controls.Add(this.MainSplitPanel);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMergeOrderPreview";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview Merged Order";
            this.Load += new System.EventHandler(this.frmMergeOrderPreview_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.MainSplitPanel.Panel1.ResumeLayout(false);
            this.MainSplitPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitPanel)).EndInit();
            this.MainSplitPanel.ResumeLayout(false);
            this.pnlLabels.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.pnlMiddle.ResumeLayout(false);
            this.pnlMiddle.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlStep4Text.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.pnlUserControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip;
        internal System.Windows.Forms.ToolStripButton ts_btnPreviewMerge;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.SplitContainer MainSplitPanel;
        private System.Windows.Forms.Panel pnlUserControl;
        private gloUserControlLibrary.gloUC_TransactionHistory gloUC_Preview;
        private System.Windows.Forms.Panel pnlStep4Text;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblTags;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel pnlLabels;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlMiddle;
        private System.Windows.Forms.Label lblAcknowledged;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblOrdered;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOrderNo;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblStatus;

    }
}