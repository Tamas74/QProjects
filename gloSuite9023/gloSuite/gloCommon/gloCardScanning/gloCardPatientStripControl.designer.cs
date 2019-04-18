namespace gloCardPatientStripControl
{
    partial class gloCardPatientStripControl
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
                    if (dtpDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDate);
                        }
                        catch
                        {
                        }
                        dtpDate.Dispose();
                        dtpDate = null;
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
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloCardPatientStripControl));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label52 = new System.Windows.Forms.Label();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btn_ModityPatient = new System.Windows.Forms.Button();
            this.btnUP = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.pnl_Main = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.lblTodaysDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.pnlAge = new System.Windows.Forms.Panel();
            this.lblAge = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pnlDOB = new System.Windows.Forms.Panel();
            this.lblDOB = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlGuarantor = new System.Windows.Forms.Panel();
            this.lblGuarantor = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.pnlGender = new System.Windows.Forms.Panel();
            this.lblGender = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlPatientPhone = new System.Windows.Forms.Panel();
            this.lblPhone = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.lblProvider = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlName = new System.Windows.Forms.Panel();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlCode = new System.Windows.Forms.Panel();
            this.lblPatientCode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.pnlInsurace = new System.Windows.Forms.Panel();
            this.label53 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.c1Insurance = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlAlerts = new System.Windows.Forms.Panel();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.c1PatientDetails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.pnl_Main.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.pnlAge.SuspendLayout();
            this.pnlDOB.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlGuarantor.SuspendLayout();
            this.pnlGender.SuspendLayout();
            this.pnlPatientPhone.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlName.SuspendLayout();
            this.pnlCode.SuspendLayout();
            this.pnlInsurace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Insurance)).BeginInit();
            this.pnlAlerts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlTop.Size = new System.Drawing.Size(1569, 27);
            this.pnlTop.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label52);
            this.panel1.Controls.Add(this.pnlButton);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1569, 24);
            this.panel1.TabIndex = 61;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(0, 1);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 22);
            this.label52.TabIndex = 22;
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.Transparent;
            this.pnlButton.Controls.Add(this.btn_ModityPatient);
            this.pnlButton.Controls.Add(this.btnUP);
            this.pnlButton.Controls.Add(this.btnDown);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButton.ForeColor = System.Drawing.Color.Black;
            this.pnlButton.Location = new System.Drawing.Point(1490, 1);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(78, 22);
            this.pnlButton.TabIndex = 2;
            // 
            // btn_ModityPatient
            // 
            this.btn_ModityPatient.BackColor = System.Drawing.Color.Transparent;
            this.btn_ModityPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ModityPatient.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_ModityPatient.FlatAppearance.BorderSize = 0;
            this.btn_ModityPatient.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_ModityPatient.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_ModityPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ModityPatient.Location = new System.Drawing.Point(15, 0);
            this.btn_ModityPatient.Name = "btn_ModityPatient";
            this.btn_ModityPatient.Size = new System.Drawing.Size(23, 22);
            this.btn_ModityPatient.TabIndex = 57;
            this.toolTip1.SetToolTip(this.btn_ModityPatient, "Modify Patient");
            this.btn_ModityPatient.UseVisualStyleBackColor = false;
            this.btn_ModityPatient.MouseLeave += new System.EventHandler(this.btn_ModityPatient_MouseLeave);
            this.btn_ModityPatient.MouseHover += new System.EventHandler(this.btn_ModityPatient_MouseHover);
            // 
            // btnUP
            // 
            this.btnUP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUP.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUP.FlatAppearance.BorderSize = 0;
            this.btnUP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUP.Location = new System.Drawing.Point(38, 0);
            this.btnUP.Name = "btnUP";
            this.btnUP.Size = new System.Drawing.Size(20, 22);
            this.btnUP.TabIndex = 21;
            this.btnUP.UseVisualStyleBackColor = true;
            this.btnUP.MouseLeave += new System.EventHandler(this.btnUP_MouseLeave);
            this.btnUP.MouseHover += new System.EventHandler(this.btnUP_MouseHover);
            // 
            // btnDown
            // 
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(58, 0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(20, 22);
            this.btnDown.TabIndex = 22;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.MouseLeave += new System.EventHandler(this.btnDown_MouseLeave);
            this.btnDown.MouseHover += new System.EventHandler(this.btnDown_MouseHover);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(1568, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 22);
            this.label7.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1569, 1);
            this.label9.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(0, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1569, 1);
            this.label13.TabIndex = 58;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.BackColor = System.Drawing.Color.Transparent;
            this.pnlMiddle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMiddle.Controls.Add(this.pnl_Main);
            this.pnlMiddle.Controls.Add(this.pnlInsurace);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMiddle.ForeColor = System.Drawing.Color.White;
            this.pnlMiddle.Location = new System.Drawing.Point(0, 27);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Size = new System.Drawing.Size(1569, 182);
            this.pnlMiddle.TabIndex = 55;
            // 
            // pnl_Main
            // 
            this.pnl_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Main.Controls.Add(this.pnlRight);
            this.pnl_Main.Controls.Add(this.pnlLeft);
            this.pnl_Main.Controls.Add(this.label45);
            this.pnl_Main.Controls.Add(this.label42);
            this.pnl_Main.Controls.Add(this.label46);
            this.pnl_Main.Controls.Add(this.label39);
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Main.Location = new System.Drawing.Point(0, 0);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.Size = new System.Drawing.Size(1119, 182);
            this.pnl_Main.TabIndex = 20;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.Transparent;
            this.pnlRight.Controls.Add(this.pnlDate);
            this.pnlRight.Controls.Add(this.pnlAge);
            this.pnlRight.Controls.Add(this.pnlDOB);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(902, 1);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(216, 180);
            this.pnlRight.TabIndex = 48;
            // 
            // pnlDate
            // 
            this.pnlDate.Controls.Add(this.lblTodaysDate);
            this.pnlDate.Controls.Add(this.dtpDate);
            this.pnlDate.Controls.Add(this.label26);
            this.pnlDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlDate.Location = new System.Drawing.Point(0, 36);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(216, 21);
            this.pnlDate.TabIndex = 43;
            this.pnlDate.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDate_Paint);
            // 
            // lblTodaysDate
            // 
            this.lblTodaysDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTodaysDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.lblTodaysDate.Location = new System.Drawing.Point(126, 0);
            this.lblTodaysDate.Name = "lblTodaysDate";
            this.lblTodaysDate.Size = new System.Drawing.Size(250, 21);
            this.lblTodaysDate.TabIndex = 5;
            this.lblTodaysDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpDate
            // 
            this.dtpDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDate.Location = new System.Drawing.Point(135, 0);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(226, 22);
            this.dtpDate.TabIndex = 4;
            // 
            // label26
            // 
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(126, 21);
            this.label26.TabIndex = 2;
            this.label26.Text = "Date :";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlAge
            // 
            this.pnlAge.Controls.Add(this.lblAge);
            this.pnlAge.Controls.Add(this.label20);
            this.pnlAge.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlAge.Location = new System.Drawing.Point(0, 18);
            this.pnlAge.Name = "pnlAge";
            this.pnlAge.Size = new System.Drawing.Size(216, 18);
            this.pnlAge.TabIndex = 46;
            // 
            // lblAge
            // 
            this.lblAge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAge.Location = new System.Drawing.Point(126, 0);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(90, 18);
            this.lblAge.TabIndex = 6;
            this.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(126, 18);
            this.label20.TabIndex = 1;
            this.label20.Text = "Age  :";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlDOB
            // 
            this.pnlDOB.Controls.Add(this.lblDOB);
            this.pnlDOB.Controls.Add(this.label28);
            this.pnlDOB.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDOB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlDOB.Location = new System.Drawing.Point(0, 0);
            this.pnlDOB.Name = "pnlDOB";
            this.pnlDOB.Size = new System.Drawing.Size(216, 18);
            this.pnlDOB.TabIndex = 39;
            // 
            // lblDOB
            // 
            this.lblDOB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDOB.Location = new System.Drawing.Point(126, 0);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(90, 18);
            this.lblDOB.TabIndex = 5;
            this.lblDOB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(126, 18);
            this.label28.TabIndex = 0;
            this.label28.Text = "DOB :";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLeft.Controls.Add(this.pnlGuarantor);
            this.pnlLeft.Controls.Add(this.pnlGender);
            this.pnlLeft.Controls.Add(this.pnlPatientPhone);
            this.pnlLeft.Controls.Add(this.pnlProvider);
            this.pnlLeft.Controls.Add(this.pnlName);
            this.pnlLeft.Controls.Add(this.pnlCode);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlLeft.Location = new System.Drawing.Point(1, 1);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(144, 180);
            this.pnlLeft.TabIndex = 2;
            // 
            // pnlGuarantor
            // 
            this.pnlGuarantor.BackColor = System.Drawing.Color.Transparent;
            this.pnlGuarantor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGuarantor.Controls.Add(this.lblGuarantor);
            this.pnlGuarantor.Controls.Add(this.label47);
            this.pnlGuarantor.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGuarantor.Location = new System.Drawing.Point(0, 90);
            this.pnlGuarantor.Name = "pnlGuarantor";
            this.pnlGuarantor.Size = new System.Drawing.Size(144, 18);
            this.pnlGuarantor.TabIndex = 40;
            // 
            // lblGuarantor
            // 
            this.lblGuarantor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGuarantor.Location = new System.Drawing.Point(109, 0);
            this.lblGuarantor.Name = "lblGuarantor";
            this.lblGuarantor.Size = new System.Drawing.Size(35, 18);
            this.lblGuarantor.TabIndex = 12;
            this.lblGuarantor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label47
            // 
            this.label47.Dock = System.Windows.Forms.DockStyle.Left;
            this.label47.Location = new System.Drawing.Point(0, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(109, 18);
            this.label47.TabIndex = 39;
            this.label47.Text = "Guarantor :";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlGender
            // 
            this.pnlGender.BackColor = System.Drawing.Color.Transparent;
            this.pnlGender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGender.Controls.Add(this.lblGender);
            this.pnlGender.Controls.Add(this.label5);
            this.pnlGender.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGender.Location = new System.Drawing.Point(0, 72);
            this.pnlGender.Name = "pnlGender";
            this.pnlGender.Size = new System.Drawing.Size(144, 18);
            this.pnlGender.TabIndex = 38;
            // 
            // lblGender
            // 
            this.lblGender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGender.Location = new System.Drawing.Point(109, 0);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(35, 18);
            this.lblGender.TabIndex = 9;
            this.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "Gender :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPatientPhone
            // 
            this.pnlPatientPhone.BackColor = System.Drawing.Color.Transparent;
            this.pnlPatientPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPatientPhone.Controls.Add(this.lblPhone);
            this.pnlPatientPhone.Controls.Add(this.label4);
            this.pnlPatientPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatientPhone.Location = new System.Drawing.Point(0, 54);
            this.pnlPatientPhone.Name = "pnlPatientPhone";
            this.pnlPatientPhone.Size = new System.Drawing.Size(144, 18);
            this.pnlPatientPhone.TabIndex = 38;
            // 
            // lblPhone
            // 
            this.lblPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhone.Location = new System.Drawing.Point(109, 0);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(35, 18);
            this.lblPhone.TabIndex = 8;
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Phone :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlProvider
            // 
            this.pnlProvider.BackColor = System.Drawing.Color.Transparent;
            this.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProvider.Controls.Add(this.lblProvider);
            this.pnlProvider.Controls.Add(this.label3);
            this.pnlProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProvider.Location = new System.Drawing.Point(0, 36);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(144, 18);
            this.pnlProvider.TabIndex = 38;
            // 
            // lblProvider
            // 
            this.lblProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProvider.Location = new System.Drawing.Point(109, 0);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(35, 18);
            this.lblProvider.TabIndex = 7;
            this.lblProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Provider :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlName
            // 
            this.pnlName.BackColor = System.Drawing.Color.Transparent;
            this.pnlName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlName.Controls.Add(this.lblPatientName);
            this.pnlName.Controls.Add(this.label2);
            this.pnlName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlName.Location = new System.Drawing.Point(0, 18);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(144, 18);
            this.pnlName.TabIndex = 38;
            // 
            // lblPatientName
            // 
            this.lblPatientName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatientName.Location = new System.Drawing.Point(109, 0);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(35, 18);
            this.lblPatientName.TabIndex = 6;
            this.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name  :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCode
            // 
            this.pnlCode.BackColor = System.Drawing.Color.Transparent;
            this.pnlCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCode.Controls.Add(this.lblPatientCode);
            this.pnlCode.Controls.Add(this.label1);
            this.pnlCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCode.Location = new System.Drawing.Point(0, 0);
            this.pnlCode.Name = "pnlCode";
            this.pnlCode.Size = new System.Drawing.Size(144, 18);
            this.pnlCode.TabIndex = 37;
            // 
            // lblPatientCode
            // 
            this.lblPatientCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatientCode.Location = new System.Drawing.Point(109, 0);
            this.lblPatientCode.Name = "lblPatientCode";
            this.lblPatientCode.Size = new System.Drawing.Size(35, 18);
            this.lblPatientCode.TabIndex = 5;
            this.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Location = new System.Drawing.Point(0, 1);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1, 180);
            this.label45.TabIndex = 62;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(0, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1118, 1);
            this.label42.TabIndex = 64;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Right;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Location = new System.Drawing.Point(1118, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1, 181);
            this.label46.TabIndex = 65;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Location = new System.Drawing.Point(0, 181);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(1119, 1);
            this.label39.TabIndex = 63;
            // 
            // pnlInsurace
            // 
            this.pnlInsurace.Controls.Add(this.label53);
            this.pnlInsurace.Controls.Add(this.label51);
            this.pnlInsurace.Controls.Add(this.label50);
            this.pnlInsurace.Controls.Add(this.label38);
            this.pnlInsurace.Controls.Add(this.c1Insurance);
            this.pnlInsurace.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlInsurace.Location = new System.Drawing.Point(1119, 0);
            this.pnlInsurace.Name = "pnlInsurace";
            this.pnlInsurace.Size = new System.Drawing.Size(450, 182);
            this.pnlInsurace.TabIndex = 21;
            this.pnlInsurace.Visible = false;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Top;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Location = new System.Drawing.Point(1, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(448, 1);
            this.label53.TabIndex = 65;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Location = new System.Drawing.Point(1, 181);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(448, 1);
            this.label51.TabIndex = 64;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Right;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Location = new System.Drawing.Point(449, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 182);
            this.label50.TabIndex = 62;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Left;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Location = new System.Drawing.Point(0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 182);
            this.label38.TabIndex = 61;
            // 
            // c1Insurance
            // 
            this.c1Insurance.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Insurance.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Insurance.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Insurance.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1Insurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Insurance.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1Insurance.Location = new System.Drawing.Point(0, 0);
            this.c1Insurance.Name = "c1Insurance";
            this.c1Insurance.Padding = new System.Windows.Forms.Padding(3);
            this.c1Insurance.Rows.Count = 1;
            this.c1Insurance.Rows.DefaultSize = 19;
            this.c1Insurance.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1Insurance.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None;
            this.c1Insurance.Size = new System.Drawing.Size(450, 182);
            this.c1Insurance.StyleInfo = resources.GetString("c1Insurance.StyleInfo");
            this.c1Insurance.TabIndex = 60;
            //this.c1Insurance.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Insurance_MouseMove);
            // 
            // pnlAlerts
            // 
            this.pnlAlerts.BackColor = System.Drawing.Color.Transparent;
            this.pnlAlerts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAlerts.Controls.Add(this.label44);
            this.pnlAlerts.Controls.Add(this.label43);
            this.pnlAlerts.Controls.Add(this.label32);
            this.pnlAlerts.Controls.Add(this.label27);
            this.pnlAlerts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAlerts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAlerts.ForeColor = System.Drawing.Color.White;
            this.pnlAlerts.Location = new System.Drawing.Point(0, 209);
            this.pnlAlerts.Name = "pnlAlerts";
            this.pnlAlerts.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlAlerts.Size = new System.Drawing.Size(1569, 38);
            this.pnlAlerts.TabIndex = 54;
            this.pnlAlerts.Visible = false;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Location = new System.Drawing.Point(1568, 4);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 33);
            this.label44.TabIndex = 62;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Location = new System.Drawing.Point(0, 4);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 33);
            this.label43.TabIndex = 61;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Location = new System.Drawing.Point(0, 3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1569, 1);
            this.label32.TabIndex = 60;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Location = new System.Drawing.Point(0, 37);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1569, 1);
            this.label27.TabIndex = 59;
            // 
            // c1PatientDetails
            // 
            this.c1PatientDetails.AllowEditing = false;
            this.c1PatientDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.c1PatientDetails.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.c1PatientDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientDetails.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1PatientDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientDetails.Location = new System.Drawing.Point(0, 0);
            this.c1PatientDetails.Name = "c1PatientDetails";
            this.c1PatientDetails.Rows.Count = 5;
            this.c1PatientDetails.Rows.DefaultSize = 19;
            this.c1PatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientDetails.ShowCellLabels = true;
            this.c1PatientDetails.Size = new System.Drawing.Size(1569, 247);
            this.c1PatientDetails.StyleInfo = resources.GetString("c1PatientDetails.StyleInfo");
            this.c1PatientDetails.TabIndex = 53;
            this.c1PatientDetails.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1PatientDetails.Tree.NodeImageCollapsed")));
            this.c1PatientDetails.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1PatientDetails.Tree.NodeImageExpanded")));
            //this.c1PatientDetails.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1PatientDetails_MouseMove);
            // 
            // gloCardPatientStripControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnlMiddle);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlAlerts);
            this.Controls.Add(this.c1PatientDetails);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloCardPatientStripControl";
            this.Size = new System.Drawing.Size(1569, 247);
            this.Load += new System.EventHandler(this.gloPatientStripControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.gloPatientStripControl_Paint);
            this.pnlTop.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlMiddle.ResumeLayout(false);
            this.pnl_Main.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlDate.ResumeLayout(false);
            this.pnlAge.ResumeLayout(false);
            this.pnlDOB.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlGuarantor.ResumeLayout(false);
            this.pnlGender.ResumeLayout(false);
            this.pnlPatientPhone.ResumeLayout(false);
            this.pnlProvider.ResumeLayout(false);
            this.pnlName.ResumeLayout(false);
            this.pnlCode.ResumeLayout(false);
            this.pnlInsurace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Insurance)).EndInit();
            this.pnlAlerts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblPatientCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlGender;
        private System.Windows.Forms.Panel pnlPatientPhone;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Panel pnlCode;
        private System.Windows.Forms.Panel pnlAge;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel pnlDate;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel pnlDOB;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUP;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnl_Main;
        private System.Windows.Forms.Label lblTodaysDate;
        private System.Windows.Forms.Button btn_ModityPatient;
        private System.Windows.Forms.Label label13;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientDetails;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnlInsurace;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Insurance;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel pnlAlerts;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel pnlMiddle;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Panel panel1;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel pnlGuarantor;
        private System.Windows.Forms.Label lblGuarantor;
        private System.Windows.Forms.Label label47;
    }
}
