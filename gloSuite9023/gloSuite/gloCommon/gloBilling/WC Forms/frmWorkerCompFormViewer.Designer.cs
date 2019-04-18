namespace gloBilling
{
    partial class frmWorkerCompFormViewer
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
            //try
            //{
            //    if (PrintDialog1 != null)
            //    {
            //        PrintDialog1.Dispose();
            //        PrintDialog1 = null;
            //    }
            //}
            //catch
            //{
            //}
            //try
            //{
            //    if (printDocument1 != null)
            //    {
            //        //try
            //        //{
            //        //    printDocument1.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            //        //    printDocument1.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            //        //}
            //        //catch
            //        //{
            //        //}

            //        printDocument1.Dispose();
            //        printDocument1 = null;
            //    }
            //}
            //catch
            //{
            //}
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkerCompFormViewer));
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_SetData = new System.Windows.Forms.ToolStripButton();
            this.tsb_GetData = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveData = new System.Windows.Forms.ToolStripButton();
            this.tsp_Close = new System.Windows.Forms.ToolStripButton();
            this.ViewerPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlListControl = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_txtSearch = new System.Windows.Forms.Panel();
            this.txtClaimNoSearch = new System.Windows.Forms.TextBox();
            this.Label77 = new System.Windows.Forms.Label();
            this.Label61 = new System.Windows.Forms.Label();
            this.Label62 = new System.Windows.Forms.Label();
            this.txtClearSearch = new System.Windows.Forms.Button();
            this.Label63 = new System.Windows.Forms.Label();
            this.Label64 = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblClaimNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSearchPatientClaim = new System.Windows.Forms.Button();
            this.label52 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlClaimWarning = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();

            //this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            //this.PrintDialog1 = new System.Windows.Forms.PrintDialog();

            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.ViewerPanel.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnl_txtSearch.SuspendLayout();
            this.pnlClaimWarning.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(877, 53);
            this.panel2.TabIndex = 19;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_SetData,
            this.tsb_GetData,
            this.tsb_Print,
            this.tsb_SaveData,
            this.tsp_Close});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(877, 53);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_SetData
            // 
            this.tsb_SetData.BackColor = System.Drawing.Color.Transparent;
            this.tsb_SetData.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SetData.Image")));
            this.tsb_SetData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SetData.Name = "tsb_SetData";
            this.tsb_SetData.Size = new System.Drawing.Size(65, 50);
            this.tsb_SetData.Text = "Set Data";
            this.tsb_SetData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SetData.Visible = false;
            // 
            // tsb_GetData
            // 
            this.tsb_GetData.BackColor = System.Drawing.Color.Transparent;
            this.tsb_GetData.Image = ((System.Drawing.Image)(resources.GetObject("tsb_GetData.Image")));
            this.tsb_GetData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_GetData.Name = "tsb_GetData";
            this.tsb_GetData.Size = new System.Drawing.Size(65, 50);
            this.tsb_GetData.Text = "Get Data";
            this.tsb_GetData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Print
            // 
            this.tsb_Print.BackColor = System.Drawing.Color.Transparent;
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(41, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = "&Print";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
            // 
            // tsb_SaveData
            // 
            this.tsb_SaveData.BackColor = System.Drawing.Color.Transparent;
            this.tsb_SaveData.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb_SaveData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_SaveData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_SaveData.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveData.Image")));
            this.tsb_SaveData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveData.Name = "tsb_SaveData";
            this.tsb_SaveData.Size = new System.Drawing.Size(40, 50);
            this.tsb_SaveData.Text = "&Save";
            this.tsb_SaveData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SaveData.Click += new System.EventHandler(this.tsb_SaveData_Click);
            // 
            // tsp_Close
            // 
            this.tsp_Close.BackColor = System.Drawing.Color.Transparent;
            this.tsp_Close.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsp_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsp_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsp_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsp_Close.Image")));
            this.tsp_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsp_Close.Name = "tsp_Close";
            this.tsp_Close.Size = new System.Drawing.Size(43, 50);
            this.tsp_Close.Text = "&Close";
            this.tsp_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsp_Close.Click += new System.EventHandler(this.tsp_Close_Click);
            // 
            // printDocument1
            // 
            //this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            //this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // ViewerPanel
            // 
            this.ViewerPanel.BackColor = System.Drawing.Color.White;
            this.ViewerPanel.Controls.Add(this.label4);
            this.ViewerPanel.Controls.Add(this.label3);
            this.ViewerPanel.Controls.Add(this.pnlListControl);
            this.ViewerPanel.Controls.Add(this.label2);
            this.ViewerPanel.Controls.Add(this.label1);
            this.ViewerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewerPanel.Location = new System.Drawing.Point(0, 112);
            this.ViewerPanel.Name = "ViewerPanel";
            this.ViewerPanel.Size = new System.Drawing.Size(877, 619);
            this.ViewerPanel.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(876, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 617);
            this.label4.TabIndex = 64;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(0, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 617);
            this.label3.TabIndex = 63;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Visible = false;
            // 
            // pnlListControl
            // 
            this.pnlListControl.Location = new System.Drawing.Point(0, 135);
            this.pnlListControl.Name = "pnlListControl";
            this.pnlListControl.Size = new System.Drawing.Size(877, 31);
            this.pnlListControl.TabIndex = 21;
            this.pnlListControl.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(0, 618);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(877, 1);
            this.label2.TabIndex = 62;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(877, 1);
            this.label1.TabIndex = 61;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Visible = false;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(0, 53);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnlTop.Size = new System.Drawing.Size(877, 31);
            this.pnlTop.TabIndex = 57;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pnl_txtSearch);
            this.panel1.Controls.Add(this.lblPatientName);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblClaimNo);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnSearchPatientClaim);
            this.panel1.Controls.Add(this.label52);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 25);
            this.panel1.TabIndex = 61;
            // 
            // pnl_txtSearch
            // 
            this.pnl_txtSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnl_txtSearch.Controls.Add(this.txtClaimNoSearch);
            this.pnl_txtSearch.Controls.Add(this.Label77);
            this.pnl_txtSearch.Controls.Add(this.Label61);
            this.pnl_txtSearch.Controls.Add(this.Label62);
            this.pnl_txtSearch.Controls.Add(this.txtClearSearch);
            this.pnl_txtSearch.Controls.Add(this.Label63);
            this.pnl_txtSearch.Controls.Add(this.Label64);
            this.pnl_txtSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_txtSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_txtSearch.ForeColor = System.Drawing.Color.Black;
            this.pnl_txtSearch.Location = new System.Drawing.Point(71, 1);
            this.pnl_txtSearch.Name = "pnl_txtSearch";
            this.pnl_txtSearch.Size = new System.Drawing.Size(200, 23);
            this.pnl_txtSearch.TabIndex = 232;
            // 
            // txtClaimNoSearch
            // 
            this.txtClaimNoSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtClaimNoSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClaimNoSearch.ForeColor = System.Drawing.Color.Black;
            this.txtClaimNoSearch.Location = new System.Drawing.Point(5, 3);
            this.txtClaimNoSearch.MaxLength = 15;
            this.txtClaimNoSearch.Name = "txtClaimNoSearch";
            this.txtClaimNoSearch.Size = new System.Drawing.Size(171, 15);
            this.txtClaimNoSearch.TabIndex = 0;
            this.txtClaimNoSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaimNoSearch_KeyDown);
            // 
            // Label77
            // 
            this.Label77.BackColor = System.Drawing.Color.White;
            this.Label77.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label77.Location = new System.Drawing.Point(5, 18);
            this.Label77.Name = "Label77";
            this.Label77.Size = new System.Drawing.Size(171, 5);
            this.Label77.TabIndex = 43;
            // 
            // Label61
            // 
            this.Label61.BackColor = System.Drawing.Color.White;
            this.Label61.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label61.Location = new System.Drawing.Point(5, 0);
            this.Label61.Name = "Label61";
            this.Label61.Size = new System.Drawing.Size(171, 3);
            this.Label61.TabIndex = 37;
            // 
            // Label62
            // 
            this.Label62.BackColor = System.Drawing.Color.White;
            this.Label62.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label62.Location = new System.Drawing.Point(1, 0);
            this.Label62.Name = "Label62";
            this.Label62.Size = new System.Drawing.Size(4, 23);
            this.Label62.TabIndex = 38;
            // 
            // txtClearSearch
            // 
            this.txtClearSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtClearSearch.BackgroundImage")));
            this.txtClearSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.txtClearSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtClearSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtClearSearch.FlatAppearance.BorderSize = 0;
            this.txtClearSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.txtClearSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.txtClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtClearSearch.Image = ((System.Drawing.Image)(resources.GetObject("txtClearSearch.Image")));
            this.txtClearSearch.Location = new System.Drawing.Point(176, 0);
            this.txtClearSearch.Name = "txtClearSearch";
            this.txtClearSearch.Size = new System.Drawing.Size(23, 23);
            this.txtClearSearch.TabIndex = 41;
            this.txtClearSearch.UseVisualStyleBackColor = true;
            this.txtClearSearch.Click += new System.EventHandler(this.txtClearSearch_Click);
            // 
            // Label63
            // 
            this.Label63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label63.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label63.Location = new System.Drawing.Point(0, 0);
            this.Label63.Name = "Label63";
            this.Label63.Size = new System.Drawing.Size(1, 23);
            this.Label63.TabIndex = 39;
            this.Label63.Text = "label4";
            // 
            // Label64
            // 
            this.Label64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label64.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label64.Location = new System.Drawing.Point(199, 0);
            this.Label64.Name = "Label64";
            this.Label64.Size = new System.Drawing.Size(1, 23);
            this.Label64.TabIndex = 40;
            this.Label64.Text = "label4";
            // 
            // lblPatientName
            // 
            this.lblPatientName.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientName.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPatientName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.ForeColor = System.Drawing.Color.Black;
            this.lblPatientName.Location = new System.Drawing.Point(594, 1);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblPatientName.Size = new System.Drawing.Size(209, 23);
            this.lblPatientName.TabIndex = 61;
            this.lblPatientName.Text = "Patient name ";
            this.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(803, 1);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(3, 2, 0, 0);
            this.label8.Size = new System.Drawing.Size(66, 18);
            this.label8.TabIndex = 63;
            this.label8.Text = "Claim # :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblClaimNo
            // 
            this.lblClaimNo.AutoSize = true;
            this.lblClaimNo.BackColor = System.Drawing.Color.Transparent;
            this.lblClaimNo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblClaimNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimNo.ForeColor = System.Drawing.Color.Black;
            this.lblClaimNo.Location = new System.Drawing.Point(869, 1);
            this.lblClaimNo.Name = "lblClaimNo";
            this.lblClaimNo.Padding = new System.Windows.Forms.Padding(3, 2, 4, 0);
            this.lblClaimNo.Size = new System.Drawing.Size(7, 18);
            this.lblClaimNo.TabIndex = 62;
            this.lblClaimNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(1, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 23);
            this.label6.TabIndex = 20;
            this.label6.Text = "Claim #";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearchPatientClaim
            // 
            this.btnSearchPatientClaim.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchPatientClaim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchPatientClaim.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnSearchPatientClaim.FlatAppearance.BorderSize = 0;
            this.btnSearchPatientClaim.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearchPatientClaim.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearchPatientClaim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchPatientClaim.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchPatientClaim.ForeColor = System.Drawing.Color.Black;
            this.btnSearchPatientClaim.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchPatientClaim.Image")));
            this.btnSearchPatientClaim.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSearchPatientClaim.Location = new System.Drawing.Point(278, 1);
            this.btnSearchPatientClaim.MaximumSize = new System.Drawing.Size(130, 22);
            this.btnSearchPatientClaim.MinimumSize = new System.Drawing.Size(130, 22);
            this.btnSearchPatientClaim.Name = "btnSearchPatientClaim";
            this.btnSearchPatientClaim.Size = new System.Drawing.Size(130, 22);
            this.btnSearchPatientClaim.TabIndex = 1;
            this.btnSearchPatientClaim.Text = "Claim Search";
            this.btnSearchPatientClaim.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearchPatientClaim.UseVisualStyleBackColor = true;
            this.btnSearchPatientClaim.Click += new System.EventHandler(this.btnSearchPatientClaim_Click);
            this.btnSearchPatientClaim.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnSearchPatientClaim.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(0, 1);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 23);
            this.label52.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(876, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 23);
            this.label7.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(0, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(877, 1);
            this.label13.TabIndex = 58;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(877, 1);
            this.label9.TabIndex = 24;
            // 
            // pnlClaimWarning
            // 
            this.pnlClaimWarning.BackColor = System.Drawing.Color.Transparent;
            this.pnlClaimWarning.Controls.Add(this.panel4);
            this.pnlClaimWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlClaimWarning.Location = new System.Drawing.Point(0, 84);
            this.pnlClaimWarning.Name = "pnlClaimWarning";
            this.pnlClaimWarning.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlClaimWarning.Size = new System.Drawing.Size(877, 28);
            this.pnlClaimWarning.TabIndex = 214;
            this.pnlClaimWarning.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.lblMessage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(877, 25);
            this.panel4.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(876, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 23);
            this.label16.TabIndex = 13;
            this.label16.Text = "label4";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 23);
            this.label15.TabIndex = 12;
            this.label15.Text = "label4";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(877, 1);
            this.label14.TabIndex = 11;
            this.label14.Text = "label1";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(877, 1);
            this.label5.TabIndex = 10;
            this.label5.Text = "label1";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(0, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Padding = new System.Windows.Forms.Padding(5, 4, 0, 0);
            this.lblMessage.Size = new System.Drawing.Size(544, 20);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Warning - The entire claim cannot be displayed. Some service lines are not visibl" +
    "e.\r\n";
            // 
            // frmWorkerCompFormViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(877, 731);
            this.Controls.Add(this.ViewerPanel);
            this.Controls.Add(this.pnlClaimWarning);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWorkerCompFormViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Worker Comp Form Viewer : C-4";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWorkerCompFormViewer_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmWorkerCompFormViewer_FormClosed);
            this.Load += new System.EventHandler(this.frmWorkerCompFormViewer_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ViewerPanel.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl_txtSearch.ResumeLayout(false);
            this.pnl_txtSearch.PerformLayout();
            this.pnlClaimWarning.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_SetData;
        private System.Windows.Forms.ToolStripButton tsb_GetData;
        private System.Windows.Forms.ToolStripButton tsb_Print;
        private System.Windows.Forms.Panel ViewerPanel;
        private System.Windows.Forms.ToolStripButton tsp_Close;
        private System.Windows.Forms.ToolStripButton tsb_SaveData;
        private System.Windows.Forms.Panel pnlListControl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblClaimNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSearchPatientClaim;
        private System.Windows.Forms.TextBox txtClaimNoSearch;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Panel pnl_txtSearch;
        internal System.Windows.Forms.Label Label77;
        internal System.Windows.Forms.Label Label61;
        internal System.Windows.Forms.Label Label62;
        internal System.Windows.Forms.Button txtClearSearch;
        private System.Windows.Forms.Label Label63;
        private System.Windows.Forms.Label Label64;
        private System.Windows.Forms.Panel pnlClaimWarning;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMessage;

        //private System.Drawing.Printing.PrintDocument printDocument1;
        //internal System.Windows.Forms.PrintDialog PrintDialog1;

    }
}