namespace gloCommunity.Forms
{
    partial class frmgloSkypeInvitations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmgloSkypeInvitations));
            this.Lblstatus = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Lbl_close = new System.Windows.Forms.Label();
            this.Pnl_Profile = new System.Windows.Forms.Panel();
            this.Lbl_profile = new System.Windows.Forms.Label();
            this.Lbl_Profilehead = new System.Windows.Forms.Label();
            this.Lbl_profilepanel = new System.Windows.Forms.Label();
            this.Btn_Ignore = new System.Windows.Forms.Button();
            this.Btn_block = new System.Windows.Forms.Button();
            this.Btn_viewprofie = new System.Windows.Forms.Button();
            this.Btn_Addcontact = new System.Windows.Forms.Button();
            this.ListBox_inviations = new System.Windows.Forms.ListBox();
            this.Pnl_Profile.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lblstatus
            // 
            this.Lblstatus.AutoSize = true;
            this.Lblstatus.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lblstatus.Location = new System.Drawing.Point(207, 292);
            this.Lblstatus.Name = "Lblstatus";
            this.Lblstatus.Size = new System.Drawing.Size(85, 15);
            this.Lblstatus.TabIndex = 21;
            this.Lblstatus.Text = "Ivitation from ";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(107)))), ((int)(((byte)(126)))));
            this.Label2.Location = new System.Drawing.Point(12, 14);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(168, 19);
            this.Label2.TabIndex = 20;
            this.Label2.Text = "Pending Invitations List";
            // 
            // Lbl_close
            // 
            this.Lbl_close.Location = new System.Drawing.Point(260, 1);
            this.Lbl_close.Name = "Lbl_close";
            this.Lbl_close.Size = new System.Drawing.Size(15, 16);
            this.Lbl_close.TabIndex = 3;
            this.Lbl_close.Text = "X";
            this.Lbl_close.Click += new System.EventHandler(this.Lbl_close_Click);
            // 
            // Pnl_Profile
            // 
            this.Pnl_Profile.BackColor = System.Drawing.SystemColors.Info;
            this.Pnl_Profile.Controls.Add(this.Lbl_close);
            this.Pnl_Profile.Controls.Add(this.Lbl_profile);
            this.Pnl_Profile.Controls.Add(this.Lbl_Profilehead);
            this.Pnl_Profile.Controls.Add(this.Lbl_profilepanel);
            this.Pnl_Profile.Location = new System.Drawing.Point(294, 85);
            this.Pnl_Profile.Name = "Pnl_Profile";
            this.Pnl_Profile.Size = new System.Drawing.Size(280, 200);
            this.Pnl_Profile.TabIndex = 19;
            this.Pnl_Profile.Visible = false;
            // 
            // Lbl_profile
            // 
            this.Lbl_profile.AutoSize = true;
            this.Lbl_profile.Location = new System.Drawing.Point(3, 40);
            this.Lbl_profile.Name = "Lbl_profile";
            this.Lbl_profile.Size = new System.Drawing.Size(39, 13);
            this.Lbl_profile.TabIndex = 2;
            this.Lbl_profile.Text = "Label2";
            // 
            // Lbl_Profilehead
            // 
            this.Lbl_Profilehead.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Profilehead.Location = new System.Drawing.Point(3, 3);
            this.Lbl_Profilehead.Name = "Lbl_Profilehead";
            this.Lbl_Profilehead.Size = new System.Drawing.Size(274, 26);
            this.Lbl_Profilehead.TabIndex = 1;
            this.Lbl_Profilehead.Text = "Profile";
            this.Lbl_Profilehead.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Lbl_profilepanel
            // 
            this.Lbl_profilepanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_profilepanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lbl_profilepanel.Location = new System.Drawing.Point(0, 0);
            this.Lbl_profilepanel.Name = "Lbl_profilepanel";
            this.Lbl_profilepanel.Padding = new System.Windows.Forms.Padding(3);
            this.Lbl_profilepanel.Size = new System.Drawing.Size(280, 200);
            this.Lbl_profilepanel.TabIndex = 0;
            // 
            // Btn_Ignore
            // 
            this.Btn_Ignore.Location = new System.Drawing.Point(552, 53);
            this.Btn_Ignore.Name = "Btn_Ignore";
            this.Btn_Ignore.Size = new System.Drawing.Size(99, 25);
            this.Btn_Ignore.TabIndex = 18;
            this.Btn_Ignore.Text = "Ignore";
            this.Btn_Ignore.UseVisualStyleBackColor = true;
            this.Btn_Ignore.Click += new System.EventHandler(this.Btn_Ignore_Click);
            // 
            // Btn_block
            // 
            this.Btn_block.Location = new System.Drawing.Point(439, 53);
            this.Btn_block.Name = "Btn_block";
            this.Btn_block.Size = new System.Drawing.Size(99, 25);
            this.Btn_block.TabIndex = 17;
            this.Btn_block.Text = "Block";
            this.Btn_block.UseVisualStyleBackColor = true;
            this.Btn_block.Click += new System.EventHandler(this.Btn_block_Click);
            // 
            // Btn_viewprofie
            // 
            this.Btn_viewprofie.Location = new System.Drawing.Point(326, 53);
            this.Btn_viewprofie.Name = "Btn_viewprofie";
            this.Btn_viewprofie.Size = new System.Drawing.Size(99, 25);
            this.Btn_viewprofie.TabIndex = 16;
            this.Btn_viewprofie.Text = "View Profile";
            this.Btn_viewprofie.UseVisualStyleBackColor = true;
            this.Btn_viewprofie.Click += new System.EventHandler(this.Btn_viewprofie_Click);
            // 
            // Btn_Addcontact
            // 
            this.Btn_Addcontact.Location = new System.Drawing.Point(213, 53);
            this.Btn_Addcontact.Name = "Btn_Addcontact";
            this.Btn_Addcontact.Size = new System.Drawing.Size(99, 25);
            this.Btn_Addcontact.TabIndex = 15;
            this.Btn_Addcontact.Text = "Add to Contacts";
            this.Btn_Addcontact.UseVisualStyleBackColor = true;
            this.Btn_Addcontact.Click += new System.EventHandler(this.Btn_Addcontact_Click);
            // 
            // ListBox_inviations
            // 
            this.ListBox_inviations.FormattingEnabled = true;
            this.ListBox_inviations.Location = new System.Drawing.Point(12, 53);
            this.ListBox_inviations.Name = "ListBox_inviations";
            this.ListBox_inviations.Size = new System.Drawing.Size(180, 251);
            this.ListBox_inviations.TabIndex = 14;
            // 
            // frmgloSkypeInvitations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 320);
            this.Controls.Add(this.Lblstatus);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Pnl_Profile);
            this.Controls.Add(this.Btn_Ignore);
            this.Controls.Add(this.Btn_block);
            this.Controls.Add(this.Btn_viewprofie);
            this.Controls.Add(this.Btn_Addcontact);
            this.Controls.Add(this.ListBox_inviations);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmgloSkypeInvitations";
            this.Text = "frmgloSkypeInvitations";
            this.Load += new System.EventHandler(this.frmgloSkypeInvitations_Load);
            this.Pnl_Profile.ResumeLayout(false);
            this.Pnl_Profile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Lblstatus;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Lbl_close;
        internal System.Windows.Forms.Panel Pnl_Profile;
        internal System.Windows.Forms.Label Lbl_profile;
        internal System.Windows.Forms.Label Lbl_Profilehead;
        internal System.Windows.Forms.Label Lbl_profilepanel;
        internal System.Windows.Forms.Button Btn_Ignore;
        internal System.Windows.Forms.Button Btn_block;
        internal System.Windows.Forms.Button Btn_viewprofie;
        internal System.Windows.Forms.Button Btn_Addcontact;
        internal System.Windows.Forms.ListBox ListBox_inviations;
    }
}