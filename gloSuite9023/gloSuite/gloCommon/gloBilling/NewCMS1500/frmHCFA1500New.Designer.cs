namespace gloBilling
{
    partial class frmHCFA1500New
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
                    if (dtpTransactionDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpTransactionDate);
                        }
                        catch
                        {
                        }
                        dtpTransactionDate.Dispose();
                        dtpTransactionDate = null;
                    }
                }
                catch
                {
                }

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
                    if (printdoc_HCFA1500 != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(printdoc_HCFA1500);
                        printdoc_HCFA1500.Dispose();
                        printdoc_HCFA1500 = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHCFA1500New));
            this.panel2 = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnPrint = new System.Windows.Forms.ToolStripButton();
            this.tls_btnPrintData = new System.Windows.Forms.ToolStripButton();
            this.tls_btnPrevious = new System.Windows.Forms.ToolStripButton();
            this.tls_btnNext = new System.Windows.Forms.ToolStripButton();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Jump = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblbottom = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtClaimNo = new System.Windows.Forms.TextBox();
            this.txtTransactionID = new System.Windows.Forms.TextBox();
            this.txtMaterAppointmentID = new System.Windows.Forms.TextBox();
            this.txtAppointmentID = new System.Windows.Forms.TextBox();
            this.txtVisitId = new System.Windows.Forms.TextBox();
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFacilityCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFacilityDescription = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlListControl = new System.Windows.Forms.Panel();
            this.printdoc_HCFA1500 = new System.Drawing.Printing.PrintDocument();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSendCounter = new System.Windows.Forms.TextBox();
            this.lblSendCounter = new System.Windows.Forms.Label();
            this.txtSendToRejection = new System.Windows.Forms.TextBox();
            this.lblSendToRejection = new System.Windows.Forms.Label();
            this.txtLastStatusId = new System.Windows.Forms.TextBox();
            this.lblLastStatusId = new System.Windows.Forms.Label();
            this.txtTransactionStutusId = new System.Windows.Forms.TextBox();
            this.lblTransactionStutusId = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ts_Commands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(924, 56);
            this.panel2.TabIndex = 7;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnPrint,
            this.tls_btnPrintData,
            this.tls_btnPrevious,
            this.tls_btnNext,
            this.tsb_Modify,
            this.tsb_OK,
            this.tsb_Jump,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(924, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tls_btnPrint
            // 
            this.tls_btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnPrint.Image")));
            this.tls_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnPrint.Name = "tls_btnPrint";
            this.tls_btnPrint.Size = new System.Drawing.Size(75, 50);
            this.tls_btnPrint.Tag = "Print";
            this.tls_btnPrint.Text = "Print &Form";
            this.tls_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnPrint.ToolTipText = "Print Form";
            this.tls_btnPrint.Click += new System.EventHandler(this.tls_btnPrint_Click);
            // 
            // tls_btnPrintData
            // 
            this.tls_btnPrintData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnPrintData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnPrintData.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnPrintData.Image")));
            this.tls_btnPrintData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnPrintData.Name = "tls_btnPrintData";
            this.tls_btnPrintData.Size = new System.Drawing.Size(74, 50);
            this.tls_btnPrintData.Tag = "PrintData";
            this.tls_btnPrintData.Text = "Print &Data";
            this.tls_btnPrintData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnPrintData.ToolTipText = "Print Data";
            this.tls_btnPrintData.Click += new System.EventHandler(this.tls_btnPrintData_Click);
            // 
            // tls_btnPrevious
            // 
            this.tls_btnPrevious.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnPrevious.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnPrevious.Image")));
            this.tls_btnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnPrevious.Name = "tls_btnPrevious";
            this.tls_btnPrevious.Size = new System.Drawing.Size(63, 50);
            this.tls_btnPrevious.Tag = "Previous";
            this.tls_btnPrevious.Text = "&Previous";
            this.tls_btnPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnPrevious.ToolTipText = "Previous";
            this.tls_btnPrevious.Click += new System.EventHandler(this.tls_btnPrevious_Click);
            // 
            // tls_btnNext
            // 
            this.tls_btnNext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnNext.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnNext.Image")));
            this.tls_btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnNext.Name = "tls_btnNext";
            this.tls_btnNext.Size = new System.Drawing.Size(39, 50);
            this.tls_btnNext.Tag = "Next";
            this.tls_btnNext.Text = "&Next";
            this.tls_btnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnNext.ToolTipText = "Next";
            this.tls_btnNext.Click += new System.EventHandler(this.tls_btnNext_Click);
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_Modify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(106, 50);
            this.tsb_Modify.Text = "Modify Charges";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Modify.Click += new System.EventHandler(this.tsb_Modify_Click);
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
            this.tsb_OK.Visible = false;
            this.tsb_OK.Click += new System.EventHandler(this.tsb_OK_Click);
            // 
            // tsb_Jump
            // 
            this.tsb_Jump.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_Jump.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Jump.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Jump.Image")));
            this.tsb_Jump.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Jump.Name = "tsb_Jump";
            this.tsb_Jump.Size = new System.Drawing.Size(63, 50);
            this.tsb_Jump.Text = "&Jump To";
            this.tsb_Jump.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Jump.Click += new System.EventHandler(this.tsb_Jump_Click);
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
            this.tsb_Cancel.ToolTipText = "Close";
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.lblbottom);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.lblMessage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(918, 25);
            this.panel4.TabIndex = 2;
            // 
            // lblbottom
            // 
            this.lblbottom.AutoSize = true;
            this.lblbottom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbottom.ForeColor = System.Drawing.Color.Black;
            this.lblbottom.Location = new System.Drawing.Point(805, 1);
            this.lblbottom.Name = "lblbottom";
            this.lblbottom.Padding = new System.Windows.Forms.Padding(5, 4, 0, 0);
            this.lblbottom.Size = new System.Drawing.Size(5, 20);
            this.lblbottom.TabIndex = 14;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(917, 1);
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
            this.label14.Size = new System.Drawing.Size(918, 1);
            this.label14.TabIndex = 11;
            this.label14.Text = "label1";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(918, 1);
            this.label13.TabIndex = 10;
            this.label13.Text = "label1";
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
            this.lblMessage.Size = new System.Drawing.Size(5, 20);
            this.lblMessage.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(918, 856);
            this.panel1.TabIndex = 8;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(1, 855);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(916, 1);
            this.label9.TabIndex = 12;
            this.label9.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 855);
            this.label10.TabIndex = 11;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(917, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 855);
            this.label11.TabIndex = 10;
            this.label11.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(918, 1);
            this.label12.TabIndex = 9;
            this.label12.Text = "label1";
            // 
            // txtClaimNo
            // 
            this.txtClaimNo.BackColor = System.Drawing.Color.White;
            this.txtClaimNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClaimNo.Enabled = false;
            this.txtClaimNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaimNo.Location = new System.Drawing.Point(937, 79);
            this.txtClaimNo.MaxLength = 7;
            this.txtClaimNo.Name = "txtClaimNo";
            this.txtClaimNo.Size = new System.Drawing.Size(97, 22);
            this.txtClaimNo.TabIndex = 206;
            // 
            // txtTransactionID
            // 
            this.txtTransactionID.BackColor = System.Drawing.Color.White;
            this.txtTransactionID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransactionID.Enabled = false;
            this.txtTransactionID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransactionID.Location = new System.Drawing.Point(937, 120);
            this.txtTransactionID.MaxLength = 7;
            this.txtTransactionID.Name = "txtTransactionID";
            this.txtTransactionID.Size = new System.Drawing.Size(97, 22);
            this.txtTransactionID.TabIndex = 207;
            // 
            // txtMaterAppointmentID
            // 
            this.txtMaterAppointmentID.BackColor = System.Drawing.Color.White;
            this.txtMaterAppointmentID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaterAppointmentID.Enabled = false;
            this.txtMaterAppointmentID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaterAppointmentID.Location = new System.Drawing.Point(937, 161);
            this.txtMaterAppointmentID.MaxLength = 7;
            this.txtMaterAppointmentID.Name = "txtMaterAppointmentID";
            this.txtMaterAppointmentID.Size = new System.Drawing.Size(97, 22);
            this.txtMaterAppointmentID.TabIndex = 208;
            // 
            // txtAppointmentID
            // 
            this.txtAppointmentID.BackColor = System.Drawing.Color.White;
            this.txtAppointmentID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAppointmentID.Enabled = false;
            this.txtAppointmentID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppointmentID.Location = new System.Drawing.Point(937, 201);
            this.txtAppointmentID.MaxLength = 7;
            this.txtAppointmentID.Name = "txtAppointmentID";
            this.txtAppointmentID.Size = new System.Drawing.Size(97, 22);
            this.txtAppointmentID.TabIndex = 209;
            // 
            // txtVisitId
            // 
            this.txtVisitId.BackColor = System.Drawing.Color.White;
            this.txtVisitId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVisitId.Enabled = false;
            this.txtVisitId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVisitId.Location = new System.Drawing.Point(937, 243);
            this.txtVisitId.MaxLength = 7;
            this.txtVisitId.Name = "txtVisitId";
            this.txtVisitId.Size = new System.Drawing.Size(97, 22);
            this.txtVisitId.TabIndex = 210;
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTransactionDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpTransactionDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpTransactionDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpTransactionDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpTransactionDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpTransactionDate.CustomFormat = "MM/dd/yy";
            this.dtpTransactionDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTransactionDate.Location = new System.Drawing.Point(937, 284);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(99, 22);
            this.dtpTransactionDate.TabIndex = 208;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(938, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 211;
            this.label1.Text = "Claim No";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(938, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 212;
            this.label2.Text = "TransactionID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(938, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 212;
            this.label3.Text = "MasterAppointID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(938, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 212;
            this.label4.Text = "AppointmentID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(938, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 212;
            this.label5.Text = "VisitID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(938, 268);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 212;
            this.label6.Text = "TransactionDate";
            // 
            // txtFacilityCode
            // 
            this.txtFacilityCode.BackColor = System.Drawing.Color.White;
            this.txtFacilityCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFacilityCode.Enabled = false;
            this.txtFacilityCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityCode.Location = new System.Drawing.Point(933, 348);
            this.txtFacilityCode.MaxLength = 7;
            this.txtFacilityCode.Name = "txtFacilityCode";
            this.txtFacilityCode.Size = new System.Drawing.Size(97, 22);
            this.txtFacilityCode.TabIndex = 210;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(934, 331);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 212;
            this.label7.Text = "FacilityCode";
            // 
            // txtFacilityDescription
            // 
            this.txtFacilityDescription.BackColor = System.Drawing.Color.White;
            this.txtFacilityDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFacilityDescription.Enabled = false;
            this.txtFacilityDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityDescription.Location = new System.Drawing.Point(933, 390);
            this.txtFacilityDescription.MaxLength = 7;
            this.txtFacilityDescription.Name = "txtFacilityDescription";
            this.txtFacilityDescription.Size = new System.Drawing.Size(97, 22);
            this.txtFacilityDescription.TabIndex = 210;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(934, 373);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 212;
            this.label8.Text = "FacilityDesc";
            // 
            // pnlListControl
            // 
            this.pnlListControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlListControl.Location = new System.Drawing.Point(296, 388);
            this.pnlListControl.Name = "pnlListControl";
            this.pnlListControl.Size = new System.Drawing.Size(384, 233);
            this.pnlListControl.TabIndex = 0;
            this.pnlListControl.Visible = false;
            // 
            // printdoc_HCFA1500
            // 
            this.printdoc_HCFA1500.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printdoc_HCFA1500_BeginPrint);
            this.printdoc_HCFA1500.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printdoc_HCFA1500_EndPrint);
            this.printdoc_HCFA1500.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printdoc_HCFA1500_PrintPage);
            this.printdoc_HCFA1500.QueryPageSettings += new System.Drawing.Printing.QueryPageSettingsEventHandler(this.printdoc_HCFA1500_QueryPageSettings);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 84);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(924, 862);
            this.panel3.TabIndex = 208;
            // 
            // txtSendCounter
            // 
            this.txtSendCounter.BackColor = System.Drawing.Color.White;
            this.txtSendCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSendCounter.Enabled = false;
            this.txtSendCounter.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendCounter.Location = new System.Drawing.Point(933, 445);
            this.txtSendCounter.MaxLength = 7;
            this.txtSendCounter.Name = "txtSendCounter";
            this.txtSendCounter.Size = new System.Drawing.Size(97, 22);
            this.txtSendCounter.TabIndex = 210;
            this.txtSendCounter.Text = "0";
            // 
            // lblSendCounter
            // 
            this.lblSendCounter.AutoSize = true;
            this.lblSendCounter.Location = new System.Drawing.Point(934, 428);
            this.lblSendCounter.Name = "lblSendCounter";
            this.lblSendCounter.Size = new System.Drawing.Size(69, 13);
            this.lblSendCounter.TabIndex = 212;
            this.lblSendCounter.Text = "SendCounter";
            // 
            // txtSendToRejection
            // 
            this.txtSendToRejection.BackColor = System.Drawing.Color.White;
            this.txtSendToRejection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSendToRejection.Enabled = false;
            this.txtSendToRejection.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendToRejection.Location = new System.Drawing.Point(933, 497);
            this.txtSendToRejection.MaxLength = 7;
            this.txtSendToRejection.Name = "txtSendToRejection";
            this.txtSendToRejection.Size = new System.Drawing.Size(97, 22);
            this.txtSendToRejection.TabIndex = 210;
            this.txtSendToRejection.Text = "0";
            // 
            // lblSendToRejection
            // 
            this.lblSendToRejection.AutoSize = true;
            this.lblSendToRejection.Location = new System.Drawing.Point(934, 480);
            this.lblSendToRejection.Name = "lblSendToRejection";
            this.lblSendToRejection.Size = new System.Drawing.Size(90, 13);
            this.lblSendToRejection.TabIndex = 212;
            this.lblSendToRejection.Text = "SendToRejection";
            // 
            // txtLastStatusId
            // 
            this.txtLastStatusId.BackColor = System.Drawing.Color.White;
            this.txtLastStatusId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastStatusId.Enabled = false;
            this.txtLastStatusId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastStatusId.Location = new System.Drawing.Point(933, 552);
            this.txtLastStatusId.MaxLength = 7;
            this.txtLastStatusId.Name = "txtLastStatusId";
            this.txtLastStatusId.Size = new System.Drawing.Size(97, 22);
            this.txtLastStatusId.TabIndex = 210;
            this.txtLastStatusId.Text = "0";
            // 
            // lblLastStatusId
            // 
            this.lblLastStatusId.AutoSize = true;
            this.lblLastStatusId.Location = new System.Drawing.Point(934, 535);
            this.lblLastStatusId.Name = "lblLastStatusId";
            this.lblLastStatusId.Size = new System.Drawing.Size(66, 13);
            this.lblLastStatusId.TabIndex = 212;
            this.lblLastStatusId.Text = "LastStatusId";
            // 
            // txtTransactionStutusId
            // 
            this.txtTransactionStutusId.BackColor = System.Drawing.Color.White;
            this.txtTransactionStutusId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransactionStutusId.Enabled = false;
            this.txtTransactionStutusId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransactionStutusId.Location = new System.Drawing.Point(933, 608);
            this.txtTransactionStutusId.MaxLength = 7;
            this.txtTransactionStutusId.Name = "txtTransactionStutusId";
            this.txtTransactionStutusId.Size = new System.Drawing.Size(97, 22);
            this.txtTransactionStutusId.TabIndex = 210;
            this.txtTransactionStutusId.Text = "0";
            // 
            // lblTransactionStutusId
            // 
            this.lblTransactionStutusId.AutoSize = true;
            this.lblTransactionStutusId.Location = new System.Drawing.Point(934, 591);
            this.lblTransactionStutusId.Name = "lblTransactionStutusId";
            this.lblTransactionStutusId.Size = new System.Drawing.Size(102, 13);
            this.lblTransactionStutusId.TabIndex = 212;
            this.lblTransactionStutusId.Text = "TransactionStatusId";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 56);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel5.Size = new System.Drawing.Size(924, 28);
            this.panel5.TabIndex = 213;
            // 
            // frmHCFA1500New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(924, 946);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTransactionStutusId);
            this.Controls.Add(this.lblLastStatusId);
            this.Controls.Add(this.lblSendToRejection);
            this.Controls.Add(this.lblSendCounter);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTransactionStutusId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLastStatusId);
            this.Controls.Add(this.txtSendToRejection);
            this.Controls.Add(this.txtSendCounter);
            this.Controls.Add(this.txtFacilityDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFacilityCode);
            this.Controls.Add(this.dtpTransactionDate);
            this.Controls.Add(this.txtVisitId);
            this.Controls.Add(this.txtAppointmentID);
            this.Controls.Add(this.txtMaterAppointmentID);
            this.Controls.Add(this.txtTransactionID);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtClaimNo);
            this.Controls.Add(this.pnlListControl);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHCFA1500New";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CMS1500 02/12";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHCFA1500_FormClosing);
            this.Load += new System.EventHandler(this.frmHCFA1500_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tls_btnPrint;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtClaimNo;
        private System.Windows.Forms.TextBox txtTransactionID;
        private System.Windows.Forms.TextBox txtMaterAppointmentID;
        private System.Windows.Forms.TextBox txtAppointmentID;
        private System.Windows.Forms.TextBox txtVisitId;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFacilityCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFacilityDescription;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlListControl;
        //private Microsoft.VisualBasic.PowerPacks.Printing.PrintForm printForm1;
        private System.Drawing.Printing.PrintDocument printdoc_HCFA1500;
        internal System.Windows.Forms.ToolStripButton tls_btnPrevious;
        internal System.Windows.Forms.ToolStripButton tls_btnNext;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblLastStatusId;
        private System.Windows.Forms.Label lblSendToRejection;
        private System.Windows.Forms.Label lblSendCounter;
        private System.Windows.Forms.TextBox txtLastStatusId;
        private System.Windows.Forms.TextBox txtSendToRejection;
        private System.Windows.Forms.TextBox txtSendCounter;
        private System.Windows.Forms.Label lblTransactionStutusId;
        private System.Windows.Forms.TextBox txtTransactionStutusId;
        internal System.Windows.Forms.ToolStripButton tls_btnPrintData;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolStripButton tsb_Modify;
        private System.Windows.Forms.ToolStripButton tsb_Jump;
        private System.Windows.Forms.Label lblbottom;
    }
}