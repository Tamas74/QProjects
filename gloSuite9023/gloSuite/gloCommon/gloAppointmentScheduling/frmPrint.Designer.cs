namespace gloAppointmentScheduling
{
    partial class frmPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrint));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtTodaysDate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPatientName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAppointmentProvider = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDepartment = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtProviderName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            //this.printForm1 = new Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(56, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(859, 165);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtTodaysDate
            // 
            this.txtTodaysDate.Location = new System.Drawing.Point(57, 248);
            this.txtTodaysDate.Name = "txtTodaysDate";
            this.txtTodaysDate.Size = new System.Drawing.Size(249, 20);
            this.txtTodaysDate.TabIndex = 1;
            this.txtTodaysDate.Text = "Thursday, September 04, 2008";
            this.txtTodaysDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(56, 1002);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "6632 Telegraph Road";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Suite 363 ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(57, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(249, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Bloomfield Hills, MI 48301";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(57, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(846, 2);
            this.label4.TabIndex = 1;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(57, 305);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Dear";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPatientName
            // 
            this.txtPatientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.Location = new System.Drawing.Point(91, 305);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(132, 20);
            this.txtPatientName.TabIndex = 1;
            this.txtPatientName.Text = "Mickey Mouse";
            this.txtPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(57, 339);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(530, 23);
            this.label7.TabIndex = 1;
            this.label7.Text = "Thank you for your recent inquiry into the gloClinic. We are pleased that you are" +
                " considering ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(58, 363);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(530, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "gloClinic as a provider of your health care services. Your appointment details as" +
                " follows";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(57, 402);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "Date :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDate
            // 
            this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Location = new System.Drawing.Point(148, 402);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(132, 20);
            this.txtDate.TabIndex = 1;
            this.txtDate.Text = "Mickey Mouse";
            this.txtDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(57, 431);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 20);
            this.label10.TabIndex = 1;
            this.label10.Text = "Time :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTime
            // 
            this.txtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.Location = new System.Drawing.Point(148, 431);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(132, 20);
            this.txtTime.TabIndex = 1;
            this.txtTime.Text = "Mickey Mouse";
            this.txtTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(57, 463);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 20);
            this.label12.TabIndex = 1;
            this.label12.Text = "Provider :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAppointmentProvider
            // 
            this.txtAppointmentProvider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppointmentProvider.Location = new System.Drawing.Point(148, 463);
            this.txtAppointmentProvider.Name = "txtAppointmentProvider";
            this.txtAppointmentProvider.Size = new System.Drawing.Size(132, 20);
            this.txtAppointmentProvider.TabIndex = 1;
            this.txtAppointmentProvider.Text = "Mickey Mouse";
            this.txtAppointmentProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(57, 496);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 20);
            this.label14.TabIndex = 1;
            this.label14.Text = "Location :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.Location = new System.Drawing.Point(148, 496);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(132, 20);
            this.txtLocation.TabIndex = 1;
            this.txtLocation.Text = "Mickey Mouse";
            this.txtLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(57, 528);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 20);
            this.label16.TabIndex = 1;
            this.label16.Text = "Department :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDepartment
            // 
            this.txtDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartment.Location = new System.Drawing.Point(148, 528);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(132, 20);
            this.txtDepartment.TabIndex = 1;
            this.txtDepartment.Text = "Mickey Mouse";
            this.txtDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(58, 573);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(615, 23);
            this.label18.TabIndex = 1;
            this.label18.Text = "If you have additional questions, please feel free to call again. We also encoura" +
                "ge patients to check if ";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(58, 596);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(637, 23);
            this.label19.TabIndex = 1;
            this.label19.Text = "their insurance plan requires pre-certifications, referral approvals or other imp" +
                "ortant items prior to the time of service. ";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(58, 637);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(637, 23);
            this.label20.TabIndex = 1;
            this.label20.Text = "If you do not have insurance coverage, a variety of payment plans and assistance " +
                "programs are available";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(57, 682);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(637, 23);
            this.label21.TabIndex = 1;
            this.label21.Text = "Thank you for considering gloClinic as your health care provider. ";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(57, 744);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(637, 23);
            this.label22.TabIndex = 1;
            this.label22.Text = "Sincerely, ";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProviderName
            // 
            this.txtProviderName.Location = new System.Drawing.Point(57, 767);
            this.txtProviderName.Name = "txtProviderName";
            this.txtProviderName.Size = new System.Drawing.Size(637, 23);
            this.txtProviderName.TabIndex = 1;
            this.txtProviderName.Text = "Provider Name";
            this.txtProviderName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(57, 790);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(637, 23);
            this.label9.TabIndex = 1;
            this.label9.Text = "gloClinic.";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // printForm1
            // 
            //this.printForm1.Form = this;
            //this.printForm1.PrintAction = System.Drawing.Printing.PrintAction.PrintToPrinter;
            //this.printForm1.PrinterSettings = ((System.Drawing.Printing.PrinterSettings)(resources.GetObject("printForm1.PrinterSettings")));
            //this.printForm1.PrintFileName = null;
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(915, 1002);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtAppointmentProvider);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPatientName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtProviderName);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTodaysDate);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrint";
            this.Text = "Print";
            this.Load += new System.EventHandler(this.frmPrint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label txtTodaysDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label txtPatientName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label txtDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label txtTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label txtAppointmentProvider;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label txtLocation;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label txtDepartment;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label txtProviderName;
        private System.Windows.Forms.Label label9;
        //private Microsoft.VisualBasic.PowerPacks.Printing.PrintForm printForm1;
        private System.Windows.Forms.Timer timer1;
    }
}