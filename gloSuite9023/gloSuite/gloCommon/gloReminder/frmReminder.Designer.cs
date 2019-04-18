namespace gloReminder
{
    partial class frmReminder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        private bool blnDisposed;
        private static frmReminder frm;
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this.blnDisposed))
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    // Dispose managed resources. 
                    if ((components != null))
                    {
                        components.Dispose();
                    }
                    //frm = Nothing 
                }
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed. 

                // Note that this is not thread safe. 
                // Another thread could start disposing the object 
                // after the managed resources are disposed, 
                // but before the disposed flag is set to true. 
                // If thread safety is necessary, it must be 
                // implemented by the client. 
                base.Dispose(disposing);
            }
            frm = null;
            this.blnDisposed = true;
            
        }

        public void Disposer()
        {
            Dispose(true);
            // Take yourself off of the finalization queue 
            // to prevent finalization code for this object 
            // from executing a second time. 
            System.GC.SuppressFinalize(this);
        }

        ~frmReminder()
        {
            Dispose(false);
        }

        public static frmReminder GetInstance()
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmReminder();
                }
            }
            finally
            {

            }
            return frm;
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReminder));
            this.pnlReminderDetails = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.picBoxReminder = new System.Windows.Forms.PictureBox();
            this.lblStrartTime = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlReminders = new System.Windows.Forms.Panel();
            this.lv_Reminders = new System.Windows.Forms.ListView();
            this.imgListReminder = new System.Windows.Forms.ImageList(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbSnoozeTime = new System.Windows.Forms.ComboBox();
            this.btnDismiss = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnDismissAll = new System.Windows.Forms.Button();
            this.btnSnooze = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlReminderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxReminder)).BeginInit();
            this.pnlReminders.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlReminderDetails
            // 
            this.pnlReminderDetails.Controls.Add(this.lbl_BottomBrd);
            this.pnlReminderDetails.Controls.Add(this.lbl_LeftBrd);
            this.pnlReminderDetails.Controls.Add(this.lbl_RightBrd);
            this.pnlReminderDetails.Controls.Add(this.lbl_TopBrd);
            this.pnlReminderDetails.Controls.Add(this.picBoxReminder);
            this.pnlReminderDetails.Controls.Add(this.lblStrartTime);
            this.pnlReminderDetails.Controls.Add(this.lblStartDate);
            this.pnlReminderDetails.Controls.Add(this.lblDescription);
            this.pnlReminderDetails.Controls.Add(this.lblLocation);
            this.pnlReminderDetails.Controls.Add(this.label14);
            this.pnlReminderDetails.Controls.Add(this.label15);
            this.pnlReminderDetails.Controls.Add(this.label13);
            this.pnlReminderDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReminderDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlReminderDetails.Name = "pnlReminderDetails";
            this.pnlReminderDetails.Padding = new System.Windows.Forms.Padding(3);
            this.pnlReminderDetails.Size = new System.Drawing.Size(502, 92);
            this.pnlReminderDetails.TabIndex = 0;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 88);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(494, 1);
            this.lbl_BottomBrd.TabIndex = 13;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 85);
            this.lbl_LeftBrd.TabIndex = 12;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(498, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 85);
            this.lbl_RightBrd.TabIndex = 11;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(496, 1);
            this.lbl_TopBrd.TabIndex = 10;
            this.lbl_TopBrd.Text = "label1";
            // 
            // picBoxReminder
            // 
            this.picBoxReminder.Location = new System.Drawing.Point(44, 8);
            this.picBoxReminder.Name = "picBoxReminder";
            this.picBoxReminder.Size = new System.Drawing.Size(34, 23);
            this.picBoxReminder.TabIndex = 9;
            this.picBoxReminder.TabStop = false;
            // 
            // lblStrartTime
            // 
            this.lblStrartTime.AutoSize = true;
            this.lblStrartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStrartTime.Location = new System.Drawing.Point(263, 39);
            this.lblStrartTime.Name = "lblStrartTime";
            this.lblStrartTime.Size = new System.Drawing.Size(59, 14);
            this.lblStrartTime.TabIndex = 8;
            this.lblStrartTime.Text = "12:00 PM";
            this.lblStrartTime.Visible = false;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(86, 39);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(73, 14);
            this.lblStartDate.TabIndex = 7;
            this.lblStartDate.Text = "04/20/2008";
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(83, 10);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(418, 22);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "Subject :";
            // 
            // lblLocation
            // 
            this.lblLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(83, 62);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(269, 22);
            this.lblLocation.TabIndex = 6;
            this.lblLocation.Text = "Development Center";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(215, 39);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 14);
            this.label14.TabIndex = 4;
            this.label14.Text = "Time :";
            this.label14.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(20, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 14);
            this.label15.TabIndex = 4;
            this.label15.Text = "Location :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(9, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 14);
            this.label13.TabIndex = 4;
            this.label13.Text = "Due Date :";
            // 
            // pnlReminders
            // 
            this.pnlReminders.Controls.Add(this.lv_Reminders);
            this.pnlReminders.Controls.Add(this.label5);
            this.pnlReminders.Controls.Add(this.label6);
            this.pnlReminders.Controls.Add(this.label7);
            this.pnlReminders.Controls.Add(this.label8);
            this.pnlReminders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReminders.Location = new System.Drawing.Point(0, 92);
            this.pnlReminders.Name = "pnlReminders";
            this.pnlReminders.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlReminders.Size = new System.Drawing.Size(502, 134);
            this.pnlReminders.TabIndex = 1;
            // 
            // lv_Reminders
            // 
            this.lv_Reminders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lv_Reminders.Cursor = System.Windows.Forms.Cursors.Default;
            this.lv_Reminders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_Reminders.ForeColor = System.Drawing.Color.Black;
            this.lv_Reminders.FullRowSelect = true;
            this.lv_Reminders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_Reminders.Location = new System.Drawing.Point(4, 2);
            this.lv_Reminders.MultiSelect = false;
            this.lv_Reminders.Name = "lv_Reminders";
            this.lv_Reminders.Size = new System.Drawing.Size(494, 128);
            this.lv_Reminders.SmallImageList = this.imgListReminder;
            this.lv_Reminders.TabIndex = 4;
            this.lv_Reminders.UseCompatibleStateImageBehavior = false;
            this.lv_Reminders.View = System.Windows.Forms.View.Details;
            this.lv_Reminders.SelectedIndexChanged += new System.EventHandler(this.lv_Reminders_SelectedIndexChanged);
            // 
            // imgListReminder
            // 
            this.imgListReminder.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListReminder.ImageStream")));
            this.imgListReminder.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListReminder.Images.SetKeyName(0, "Reminder.ico");
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 128);
            this.label5.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(498, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 128);
            this.label6.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(496, 1);
            this.label7.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(496, 1);
            this.label8.TabIndex = 0;
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.label2);
            this.pnlOptions.Controls.Add(this.label3);
            this.pnlOptions.Controls.Add(this.label4);
            this.pnlOptions.Controls.Add(this.label9);
            this.pnlOptions.Controls.Add(this.cmbSnoozeTime);
            this.pnlOptions.Controls.Add(this.btnDismiss);
            this.pnlOptions.Controls.Add(this.btnOpen);
            this.pnlOptions.Controls.Add(this.btnDismissAll);
            this.pnlOptions.Controls.Add(this.btnSnooze);
            this.pnlOptions.Controls.Add(this.label1);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOptions.Location = new System.Drawing.Point(0, 226);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlOptions.Size = new System.Drawing.Size(502, 104);
            this.pnlOptions.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(4, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(494, 1);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 99);
            this.label3.TabIndex = 9;
            this.label3.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(498, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 99);
            this.label4.TabIndex = 8;
            this.label4.Text = "label3";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(496, 1);
            this.label9.TabIndex = 7;
            this.label9.Text = "label1";
            // 
            // cmbSnoozeTime
            // 
            this.cmbSnoozeTime.FormattingEnabled = true;
            this.cmbSnoozeTime.Items.AddRange(new object[] {
            "5 Minutes",
            "10 Minutes",
            "15 Minutes",
            "30 Minutes",
            "1 Hour",
            "2 Hours",
            "4 Hours",
            "8 Hours",
            "1 Day",
            "2 Days",
            "3 Days",
            "4 Days"});
            this.cmbSnoozeTime.Location = new System.Drawing.Point(19, 69);
            this.cmbSnoozeTime.Name = "cmbSnoozeTime";
            this.cmbSnoozeTime.Size = new System.Drawing.Size(355, 22);
            this.cmbSnoozeTime.TabIndex = 6;
            // 
            // btnDismiss
            // 
            this.btnDismiss.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDismiss.BackgroundImage")));
            this.btnDismiss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDismiss.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnDismiss.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnDismiss.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnDismiss.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDismiss.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDismiss.Location = new System.Drawing.Point(423, 10);
            this.btnDismiss.Name = "btnDismiss";
            this.btnDismiss.Size = new System.Drawing.Size(64, 25);
            this.btnDismiss.TabIndex = 5;
            this.btnDismiss.Text = "Dismiss";
            this.btnDismiss.UseVisualStyleBackColor = true;
            this.btnDismiss.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnDismiss.Click += new System.EventHandler(this.btnDismiss_Click);
            this.btnDismiss.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnOpen
            // 
            this.btnOpen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpen.BackgroundImage")));
            this.btnOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(339, 10);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(78, 25);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "Open Item";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            this.btnOpen.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnDismissAll
            // 
            this.btnDismissAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDismissAll.BackgroundImage")));
            this.btnDismissAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDismissAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnDismissAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnDismissAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnDismissAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDismissAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDismissAll.Location = new System.Drawing.Point(22, 10);
            this.btnDismissAll.Name = "btnDismissAll";
            this.btnDismissAll.Size = new System.Drawing.Size(71, 25);
            this.btnDismissAll.TabIndex = 5;
            this.btnDismissAll.Text = "Dismiss All";
            this.btnDismissAll.UseVisualStyleBackColor = true;
            this.btnDismissAll.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnDismissAll.Click += new System.EventHandler(this.btnDismissAll_Click);
            this.btnDismissAll.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnSnooze
            // 
            this.btnSnooze.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSnooze.BackgroundImage")));
            this.btnSnooze.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSnooze.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnSnooze.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnSnooze.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnSnooze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSnooze.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSnooze.Location = new System.Drawing.Point(380, 67);
            this.btnSnooze.Name = "btnSnooze";
            this.btnSnooze.Size = new System.Drawing.Size(64, 25);
            this.btnSnooze.TabIndex = 5;
            this.btnSnooze.Text = "Snooze";
            this.btnSnooze.UseVisualStyleBackColor = true;
            this.btnSnooze.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnSnooze.Click += new System.EventHandler(this.btnSnooze_Click);
            this.btnSnooze.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Click Snooze to be reminded again in :";
            // 
            // frmReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(502, 330);
            this.Controls.Add(this.pnlReminders);
            this.Controls.Add(this.pnlOptions);
            this.Controls.Add(this.pnlReminderDetails);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(508, 362);
            this.Name = "frmReminder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reminder";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmReminder_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReminder_FormClosing);
            this.Load += new System.EventHandler(this.frmReminder_Load);
            this.pnlReminderDetails.ResumeLayout(false);
            this.pnlReminderDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxReminder)).EndInit();
            this.pnlReminders.ResumeLayout(false);
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlReminderDetails;
        private System.Windows.Forms.Panel pnlReminders;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSnooze;
        private System.Windows.Forms.ComboBox cmbSnoozeTime;
        private System.Windows.Forms.Button btnDismiss;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnDismissAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblStrartTime;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.ListView lv_Reminders;
        private System.Windows.Forms.ImageList imgListReminder;
        private System.Windows.Forms.PictureBox picBoxReminder;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
    }
}