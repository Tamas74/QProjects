namespace gloCardScanning
{
    partial class frmCardImage
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtpDate };
            System.Windows.Forms.Control[] cntControls = { dtpDate };

            if (disposing && (components != null))
            {

                try
                {
                    ReleaseScanTimer();
                    //if (myWatcher != null && bReleaseClipboardEvents == false)
                    //{
                    //    try
                    //    {
                    //        myWatcher.OnClipboardContentChanged -= new gloGlobal.gloClipboardControl.ClipboardContentChanged(myWatcher_OnClipboardContentChanged);
                    //        bReleaseClipboardEvents = true;
                    //    }
                    //    catch
                    //    {
                    //    }
                    //}
                    //if (ScanTimer != null)
                    //{
                    //    ScanTimer.Tick -= ScanTimerFired;
                    //    ScanTimer.Dispose();
                    //    ScanTimer = null;
                    //}

                    components.Dispose();
                    if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
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
              
                try
                {
                    if (printDialog1 != null)
                    {
                        printDialog1.Dispose();
                        printDialog1 = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (printDoc != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(printDoc);
                        printDoc.Dispose();
                        printDoc = null;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCardImage));
            this.pnlPatientStrip = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlSecInsurance = new System.Windows.Forms.Panel();
            this.lblSecInsurance = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlPrInsurance = new System.Windows.Forms.Panel();
            this.lblPrInsurance = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlOccupation = new System.Windows.Forms.Panel();
            this.lblOccupation = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlGender = new System.Windows.Forms.Panel();
            this.lblGender = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pnlPharmacyName = new System.Windows.Forms.Panel();
            this.lblPharmacyName = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlPatientPhone = new System.Windows.Forms.Panel();
            this.lblPhone = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.lblProvider = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlName = new System.Windows.Forms.Panel();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlCode = new System.Windows.Forms.Panel();
            this.lblPatientCode = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlSocialSecurity = new System.Windows.Forms.Panel();
            this.lblSocialSecurity = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.pnlReferralPhysician = new System.Windows.Forms.Panel();
            this.lblReferralPhysician = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.pnlCellPhone = new System.Windows.Forms.Panel();
            this.lblCellPhone = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.pnlHandDominance = new System.Windows.Forms.Panel();
            this.lblHandDominance = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.pnlPharmacyFax = new System.Windows.Forms.Panel();
            this.lblPharmacyFax = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.pnlPharmacyPhone = new System.Windows.Forms.Panel();
            this.lblPharmacyPhone = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.lblTodaysDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label27 = new System.Windows.Forms.Label();
            this.pnlAge = new System.Windows.Forms.Panel();
            this.lblAge = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pnlDOB = new System.Windows.Forms.Panel();
            this.lblDOB = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlCards = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.rbInsuranceCard = new System.Windows.Forms.RadioButton();
            this.rbDrivingLicense = new System.Windows.Forms.RadioButton();
            this.pb_BackSide = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pb_FrontSide = new System.Windows.Forms.PictureBox();
            this.imageControl1 = new gloScanImaging.ImageControl();
            //this.imageXView1 = new PegasusImaging.WinForms.ImagXpress9.ImageXView(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Scan = new System.Windows.Forms.ToolStripButton();
            this.tsb_LoadImage = new System.Windows.Forms.ToolStripButton();
            this.tsb_ClearData = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            //this.imagXpress1 = new PegasusImaging.WinForms.ImagXpress9.ImagXpress(this.components);
            this.twainPro1 = new PegasusImaging.WinForms.TwainPro5.TwainPro(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.pnlPatientStrip.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlSecInsurance.SuspendLayout();
            this.pnlPrInsurance.SuspendLayout();
            this.pnlOccupation.SuspendLayout();
            this.pnlGender.SuspendLayout();
            this.pnlPharmacyName.SuspendLayout();
            this.pnlPatientPhone.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlName.SuspendLayout();
            this.pnlCode.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlSocialSecurity.SuspendLayout();
            this.pnlReferralPhysician.SuspendLayout();
            this.pnlCellPhone.SuspendLayout();
            this.pnlHandDominance.SuspendLayout();
            this.pnlPharmacyFax.SuspendLayout();
            this.pnlPharmacyPhone.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.pnlAge.SuspendLayout();
            this.pnlDOB.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlCards.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BackSide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_FrontSide)).BeginInit();
            this.panel1.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPatientStrip
            // 
            this.pnlPatientStrip.Controls.Add(this.pnlMain);
            this.pnlPatientStrip.Controls.Add(this.pnlTop);
            this.pnlPatientStrip.Controls.Add(this.label8);
            this.pnlPatientStrip.Controls.Add(this.label7);
            this.pnlPatientStrip.Controls.Add(this.label10);
            this.pnlPatientStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatientStrip.Location = new System.Drawing.Point(0, 54);
            this.pnlPatientStrip.Name = "pnlPatientStrip";
            this.pnlPatientStrip.Padding = new System.Windows.Forms.Padding(3);
            this.pnlPatientStrip.Size = new System.Drawing.Size(550, 90);
            this.pnlPatientStrip.TabIndex = 209;
            this.pnlPatientStrip.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlMain.BackgroundImage")));
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.pnlSecInsurance);
            this.pnlMain.Controls.Add(this.pnlPrInsurance);
            this.pnlMain.Controls.Add(this.pnlOccupation);
            this.pnlMain.Controls.Add(this.pnlGender);
            this.pnlMain.Controls.Add(this.pnlPharmacyName);
            this.pnlMain.Controls.Add(this.pnlPatientPhone);
            this.pnlMain.Controls.Add(this.pnlProvider);
            this.pnlMain.Controls.Add(this.pnlName);
            this.pnlMain.Controls.Add(this.pnlCode);
            this.pnlMain.Controls.Add(this.pnlRight);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlMain.Location = new System.Drawing.Point(4, 26);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(542, 60);
            this.pnlMain.TabIndex = 56;
            // 
            // pnlSecInsurance
            // 
            this.pnlSecInsurance.BackColor = System.Drawing.Color.Transparent;
            this.pnlSecInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSecInsurance.Controls.Add(this.lblSecInsurance);
            this.pnlSecInsurance.Controls.Add(this.label12);
            this.pnlSecInsurance.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSecInsurance.Location = new System.Drawing.Point(0, 152);
            this.pnlSecInsurance.Name = "pnlSecInsurance";
            this.pnlSecInsurance.Size = new System.Drawing.Size(283, 19);
            this.pnlSecInsurance.TabIndex = 38;
            // 
            // lblSecInsurance
            // 
            this.lblSecInsurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSecInsurance.Location = new System.Drawing.Point(155, 0);
            this.lblSecInsurance.Name = "lblSecInsurance";
            this.lblSecInsurance.Size = new System.Drawing.Size(128, 19);
            this.lblSecInsurance.TabIndex = 12;
            this.lblSecInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(155, 19);
            this.label12.TabIndex = 11;
            this.label12.Text = "Secondary Insurance :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPrInsurance
            // 
            this.pnlPrInsurance.BackColor = System.Drawing.Color.Transparent;
            this.pnlPrInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPrInsurance.Controls.Add(this.lblPrInsurance);
            this.pnlPrInsurance.Controls.Add(this.label11);
            this.pnlPrInsurance.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPrInsurance.Location = new System.Drawing.Point(0, 133);
            this.pnlPrInsurance.Name = "pnlPrInsurance";
            this.pnlPrInsurance.Size = new System.Drawing.Size(283, 19);
            this.pnlPrInsurance.TabIndex = 38;
            // 
            // lblPrInsurance
            // 
            this.lblPrInsurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrInsurance.Location = new System.Drawing.Point(155, 0);
            this.lblPrInsurance.Name = "lblPrInsurance";
            this.lblPrInsurance.Size = new System.Drawing.Size(128, 19);
            this.lblPrInsurance.TabIndex = 15;
            this.lblPrInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(155, 19);
            this.label11.TabIndex = 10;
            this.label11.Text = "Primary Insurance :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlOccupation
            // 
            this.pnlOccupation.BackColor = System.Drawing.Color.Transparent;
            this.pnlOccupation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlOccupation.Controls.Add(this.lblOccupation);
            this.pnlOccupation.Controls.Add(this.label14);
            this.pnlOccupation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOccupation.Location = new System.Drawing.Point(0, 114);
            this.pnlOccupation.Name = "pnlOccupation";
            this.pnlOccupation.Size = new System.Drawing.Size(283, 19);
            this.pnlOccupation.TabIndex = 38;
            // 
            // lblOccupation
            // 
            this.lblOccupation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOccupation.Location = new System.Drawing.Point(155, 0);
            this.lblOccupation.Name = "lblOccupation";
            this.lblOccupation.Size = new System.Drawing.Size(128, 19);
            this.lblOccupation.TabIndex = 18;
            this.lblOccupation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(155, 19);
            this.label14.TabIndex = 13;
            this.label14.Text = "Occupation :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlGender
            // 
            this.pnlGender.BackColor = System.Drawing.Color.Transparent;
            this.pnlGender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGender.Controls.Add(this.lblGender);
            this.pnlGender.Controls.Add(this.label15);
            this.pnlGender.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGender.Location = new System.Drawing.Point(0, 95);
            this.pnlGender.Name = "pnlGender";
            this.pnlGender.Size = new System.Drawing.Size(283, 19);
            this.pnlGender.TabIndex = 38;
            // 
            // lblGender
            // 
            this.lblGender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGender.Location = new System.Drawing.Point(155, 0);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(128, 19);
            this.lblGender.TabIndex = 9;
            this.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(155, 19);
            this.label15.TabIndex = 4;
            this.label15.Text = "Gender :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPharmacyName
            // 
            this.pnlPharmacyName.BackColor = System.Drawing.Color.Transparent;
            this.pnlPharmacyName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPharmacyName.Controls.Add(this.lblPharmacyName);
            this.pnlPharmacyName.Controls.Add(this.label16);
            this.pnlPharmacyName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPharmacyName.Location = new System.Drawing.Point(0, 76);
            this.pnlPharmacyName.Name = "pnlPharmacyName";
            this.pnlPharmacyName.Size = new System.Drawing.Size(283, 19);
            this.pnlPharmacyName.TabIndex = 38;
            // 
            // lblPharmacyName
            // 
            this.lblPharmacyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPharmacyName.Location = new System.Drawing.Point(155, 0);
            this.lblPharmacyName.Name = "lblPharmacyName";
            this.lblPharmacyName.Size = new System.Drawing.Size(128, 19);
            this.lblPharmacyName.TabIndex = 19;
            this.lblPharmacyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(155, 19);
            this.label16.TabIndex = 14;
            this.label16.Text = "Pharmacy Name :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPatientPhone
            // 
            this.pnlPatientPhone.BackColor = System.Drawing.Color.Transparent;
            this.pnlPatientPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPatientPhone.Controls.Add(this.lblPhone);
            this.pnlPatientPhone.Controls.Add(this.label17);
            this.pnlPatientPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatientPhone.Location = new System.Drawing.Point(0, 57);
            this.pnlPatientPhone.Name = "pnlPatientPhone";
            this.pnlPatientPhone.Size = new System.Drawing.Size(283, 19);
            this.pnlPatientPhone.TabIndex = 38;
            // 
            // lblPhone
            // 
            this.lblPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhone.Location = new System.Drawing.Point(155, 0);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(128, 19);
            this.lblPhone.TabIndex = 8;
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(155, 19);
            this.label17.TabIndex = 3;
            this.label17.Text = "Phone :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlProvider
            // 
            this.pnlProvider.BackColor = System.Drawing.Color.Transparent;
            this.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProvider.Controls.Add(this.lblProvider);
            this.pnlProvider.Controls.Add(this.label18);
            this.pnlProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProvider.Location = new System.Drawing.Point(0, 38);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(283, 19);
            this.pnlProvider.TabIndex = 38;
            // 
            // lblProvider
            // 
            this.lblProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProvider.Location = new System.Drawing.Point(125, 0);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(158, 19);
            this.lblProvider.TabIndex = 7;
            this.lblProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(125, 19);
            this.label18.TabIndex = 2;
            this.label18.Text = "Provider :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlName
            // 
            this.pnlName.BackColor = System.Drawing.Color.Transparent;
            this.pnlName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlName.Controls.Add(this.lblPatientName);
            this.pnlName.Controls.Add(this.label19);
            this.pnlName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlName.Location = new System.Drawing.Point(0, 19);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(283, 19);
            this.pnlName.TabIndex = 38;
            // 
            // lblPatientName
            // 
            this.lblPatientName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatientName.Location = new System.Drawing.Point(125, 0);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(158, 19);
            this.lblPatientName.TabIndex = 6;
            this.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(125, 19);
            this.label19.TabIndex = 1;
            this.label19.Text = "Name  :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCode
            // 
            this.pnlCode.BackColor = System.Drawing.Color.Transparent;
            this.pnlCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCode.Controls.Add(this.lblPatientCode);
            this.pnlCode.Controls.Add(this.label20);
            this.pnlCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCode.Location = new System.Drawing.Point(0, 0);
            this.pnlCode.Name = "pnlCode";
            this.pnlCode.Size = new System.Drawing.Size(283, 19);
            this.pnlCode.TabIndex = 37;
            // 
            // lblPatientCode
            // 
            this.lblPatientCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatientCode.Location = new System.Drawing.Point(125, 0);
            this.lblPatientCode.Name = "lblPatientCode";
            this.lblPatientCode.Size = new System.Drawing.Size(158, 19);
            this.lblPatientCode.TabIndex = 5;
            this.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(125, 19);
            this.label20.TabIndex = 0;
            this.label20.Text = "Code :";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.Transparent;
            this.pnlRight.Controls.Add(this.pnlSocialSecurity);
            this.pnlRight.Controls.Add(this.pnlReferralPhysician);
            this.pnlRight.Controls.Add(this.pnlCellPhone);
            this.pnlRight.Controls.Add(this.pnlHandDominance);
            this.pnlRight.Controls.Add(this.pnlPharmacyFax);
            this.pnlRight.Controls.Add(this.pnlPharmacyPhone);
            this.pnlRight.Controls.Add(this.pnlDate);
            this.pnlRight.Controls.Add(this.pnlAge);
            this.pnlRight.Controls.Add(this.pnlDOB);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(283, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(259, 60);
            this.pnlRight.TabIndex = 48;
            // 
            // pnlSocialSecurity
            // 
            this.pnlSocialSecurity.Controls.Add(this.lblSocialSecurity);
            this.pnlSocialSecurity.Controls.Add(this.label21);
            this.pnlSocialSecurity.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSocialSecurity.Location = new System.Drawing.Point(0, 156);
            this.pnlSocialSecurity.Name = "pnlSocialSecurity";
            this.pnlSocialSecurity.Size = new System.Drawing.Size(259, 19);
            this.pnlSocialSecurity.TabIndex = 42;
            // 
            // lblSocialSecurity
            // 
            this.lblSocialSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSocialSecurity.Location = new System.Drawing.Point(147, 0);
            this.lblSocialSecurity.Name = "lblSocialSecurity";
            this.lblSocialSecurity.Size = new System.Drawing.Size(112, 19);
            this.lblSocialSecurity.TabIndex = 12;
            this.lblSocialSecurity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(147, 19);
            this.label21.TabIndex = 11;
            this.label21.Text = "Social Security # :";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlReferralPhysician
            // 
            this.pnlReferralPhysician.Controls.Add(this.lblReferralPhysician);
            this.pnlReferralPhysician.Controls.Add(this.label22);
            this.pnlReferralPhysician.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReferralPhysician.Location = new System.Drawing.Point(0, 137);
            this.pnlReferralPhysician.Name = "pnlReferralPhysician";
            this.pnlReferralPhysician.Size = new System.Drawing.Size(259, 19);
            this.pnlReferralPhysician.TabIndex = 45;
            // 
            // lblReferralPhysician
            // 
            this.lblReferralPhysician.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReferralPhysician.Location = new System.Drawing.Point(147, 0);
            this.lblReferralPhysician.Name = "lblReferralPhysician";
            this.lblReferralPhysician.Size = new System.Drawing.Size(112, 19);
            this.lblReferralPhysician.TabIndex = 15;
            this.lblReferralPhysician.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(147, 19);
            this.label22.TabIndex = 10;
            this.label22.Text = "Referral Physician :";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCellPhone
            // 
            this.pnlCellPhone.Controls.Add(this.lblCellPhone);
            this.pnlCellPhone.Controls.Add(this.label23);
            this.pnlCellPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCellPhone.Location = new System.Drawing.Point(0, 118);
            this.pnlCellPhone.Name = "pnlCellPhone";
            this.pnlCellPhone.Size = new System.Drawing.Size(259, 19);
            this.pnlCellPhone.TabIndex = 44;
            // 
            // lblCellPhone
            // 
            this.lblCellPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCellPhone.Location = new System.Drawing.Point(147, 0);
            this.lblCellPhone.Name = "lblCellPhone";
            this.lblCellPhone.Size = new System.Drawing.Size(112, 19);
            this.lblCellPhone.TabIndex = 18;
            this.lblCellPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(147, 19);
            this.label23.TabIndex = 13;
            this.label23.Text = "Cell Phone :";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlHandDominance
            // 
            this.pnlHandDominance.Controls.Add(this.lblHandDominance);
            this.pnlHandDominance.Controls.Add(this.label24);
            this.pnlHandDominance.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHandDominance.Location = new System.Drawing.Point(0, 99);
            this.pnlHandDominance.Name = "pnlHandDominance";
            this.pnlHandDominance.Size = new System.Drawing.Size(259, 19);
            this.pnlHandDominance.TabIndex = 47;
            // 
            // lblHandDominance
            // 
            this.lblHandDominance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHandDominance.Location = new System.Drawing.Point(147, 0);
            this.lblHandDominance.Name = "lblHandDominance";
            this.lblHandDominance.Size = new System.Drawing.Size(112, 19);
            this.lblHandDominance.TabIndex = 19;
            this.lblHandDominance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.label24.Location = new System.Drawing.Point(0, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(147, 19);
            this.label24.TabIndex = 14;
            this.label24.Text = "Hand Dominance :";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPharmacyFax
            // 
            this.pnlPharmacyFax.Controls.Add(this.lblPharmacyFax);
            this.pnlPharmacyFax.Controls.Add(this.label25);
            this.pnlPharmacyFax.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPharmacyFax.Location = new System.Drawing.Point(0, 80);
            this.pnlPharmacyFax.Name = "pnlPharmacyFax";
            this.pnlPharmacyFax.Size = new System.Drawing.Size(259, 19);
            this.pnlPharmacyFax.TabIndex = 41;
            // 
            // lblPharmacyFax
            // 
            this.lblPharmacyFax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPharmacyFax.Location = new System.Drawing.Point(147, 0);
            this.lblPharmacyFax.Name = "lblPharmacyFax";
            this.lblPharmacyFax.Size = new System.Drawing.Size(112, 19);
            this.lblPharmacyFax.TabIndex = 9;
            this.lblPharmacyFax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(147, 19);
            this.label25.TabIndex = 4;
            this.label25.Text = "Pharmacy Fax :";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPharmacyPhone
            // 
            this.pnlPharmacyPhone.Controls.Add(this.lblPharmacyPhone);
            this.pnlPharmacyPhone.Controls.Add(this.label26);
            this.pnlPharmacyPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPharmacyPhone.Location = new System.Drawing.Point(0, 61);
            this.pnlPharmacyPhone.Name = "pnlPharmacyPhone";
            this.pnlPharmacyPhone.Size = new System.Drawing.Size(259, 19);
            this.pnlPharmacyPhone.TabIndex = 40;
            // 
            // lblPharmacyPhone
            // 
            this.lblPharmacyPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPharmacyPhone.Location = new System.Drawing.Point(147, 0);
            this.lblPharmacyPhone.Name = "lblPharmacyPhone";
            this.lblPharmacyPhone.Size = new System.Drawing.Size(112, 19);
            this.lblPharmacyPhone.TabIndex = 7;
            this.lblPharmacyPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(147, 19);
            this.label26.TabIndex = 3;
            this.label26.Text = "Pharmacy Phone :";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlDate
            // 
            this.pnlDate.Controls.Add(this.lblTodaysDate);
            this.pnlDate.Controls.Add(this.dtpDate);
            this.pnlDate.Controls.Add(this.label27);
            this.pnlDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDate.ForeColor = System.Drawing.Color.Black;
            this.pnlDate.Location = new System.Drawing.Point(0, 38);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(259, 23);
            this.pnlDate.TabIndex = 43;
            // 
            // lblTodaysDate
            // 
            this.lblTodaysDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTodaysDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTodaysDate.Location = new System.Drawing.Point(68, 0);
            this.lblTodaysDate.Name = "lblTodaysDate";
            this.lblTodaysDate.Size = new System.Drawing.Size(292, 23);
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
            this.dtpDate.Location = new System.Drawing.Point(157, 0);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(263, 22);
            this.dtpDate.TabIndex = 4;
            // 
            // label27
            // 
            this.label27.Dock = System.Windows.Forms.DockStyle.Left;
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Location = new System.Drawing.Point(0, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(68, 23);
            this.label27.TabIndex = 2;
            this.label27.Text = "Date :";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlAge
            // 
            this.pnlAge.Controls.Add(this.lblAge);
            this.pnlAge.Controls.Add(this.label28);
            this.pnlAge.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAge.Location = new System.Drawing.Point(0, 19);
            this.pnlAge.Name = "pnlAge";
            this.pnlAge.Size = new System.Drawing.Size(259, 19);
            this.pnlAge.TabIndex = 46;
            // 
            // lblAge
            // 
            this.lblAge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAge.Location = new System.Drawing.Point(68, 0);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(191, 19);
            this.lblAge.TabIndex = 6;
            this.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(68, 19);
            this.label28.TabIndex = 1;
            this.label28.Text = "Age  :";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlDOB
            // 
            this.pnlDOB.Controls.Add(this.lblDOB);
            this.pnlDOB.Controls.Add(this.label29);
            this.pnlDOB.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDOB.Location = new System.Drawing.Point(0, 0);
            this.pnlDOB.Name = "pnlDOB";
            this.pnlDOB.Size = new System.Drawing.Size(259, 19);
            this.pnlDOB.TabIndex = 39;
            // 
            // lblDOB
            // 
            this.lblDOB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDOB.Location = new System.Drawing.Point(68, 0);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(191, 19);
            this.lblDOB.TabIndex = 5;
            this.lblDOB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.Dock = System.Windows.Forms.DockStyle.Left;
            this.label29.Location = new System.Drawing.Point(0, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(68, 19);
            this.label29.TabIndex = 0;
            this.label29.Text = "DOB :";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlTop.BackgroundImage")));
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.label4);
            this.pnlTop.Controls.Add(this.label9);
            this.pnlTop.Controls.Add(this.label13);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(4, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(542, 23);
            this.pnlTop.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(0, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(542, 21);
            this.label4.TabIndex = 20;
            this.label4.Text = " Patient Details";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(542, 1);
            this.label9.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(0, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(542, 1);
            this.label13.TabIndex = 54;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 83);
            this.label8.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(3, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(543, 1);
            this.label7.TabIndex = 55;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Location = new System.Drawing.Point(546, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 84);
            this.label10.TabIndex = 3;
            // 
            // pnlCards
            // 
            this.pnlCards.Controls.Add(this.label6);
            this.pnlCards.Controls.Add(this.panel5);
            this.pnlCards.Controls.Add(this.pb_BackSide);
            this.pnlCards.Controls.Add(this.label5);
            this.pnlCards.Controls.Add(this.label3);
            this.pnlCards.Controls.Add(this.label2);
            this.pnlCards.Controls.Add(this.label1);
            this.pnlCards.Controls.Add(this.pb_FrontSide);
            this.pnlCards.Controls.Add(this.imageControl1);
            //this.pnlCards.Controls.Add(this.imageXView1);
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCards.Location = new System.Drawing.Point(0, 144);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlCards.Size = new System.Drawing.Size(550, 508);
            this.pnlCards.TabIndex = 210;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 14);
            this.label6.TabIndex = 116;
            this.label6.Text = "Card Type :";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rbOther);
            this.panel5.Controls.Add(this.rbInsuranceCard);
            this.panel5.Controls.Add(this.rbDrivingLicense);
            this.panel5.Location = new System.Drawing.Point(162, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(304, 39);
            this.panel5.TabIndex = 115;
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOther.Location = new System.Drawing.Point(242, 15);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(57, 18);
            this.rbOther.TabIndex = 3;
            this.rbOther.Text = "Other";
            this.rbOther.UseVisualStyleBackColor = true;
            this.rbOther.CheckedChanged += new System.EventHandler(this.rbOther_CheckedChanged);
            // 
            // rbInsuranceCard
            // 
            this.rbInsuranceCard.AutoSize = true;
            this.rbInsuranceCard.Checked = true;
            this.rbInsuranceCard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInsuranceCard.Location = new System.Drawing.Point(3, 15);
            this.rbInsuranceCard.Name = "rbInsuranceCard";
            this.rbInsuranceCard.Size = new System.Drawing.Size(117, 18);
            this.rbInsuranceCard.TabIndex = 1;
            this.rbInsuranceCard.TabStop = true;
            this.rbInsuranceCard.Text = "Insurance Card";
            this.rbInsuranceCard.UseVisualStyleBackColor = true;
            this.rbInsuranceCard.CheckedChanged += new System.EventHandler(this.rbInsuranceCard_CheckedChanged);
            // 
            // rbDrivingLicense
            // 
            this.rbDrivingLicense.AutoSize = true;
            this.rbDrivingLicense.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDrivingLicense.Location = new System.Drawing.Point(125, 15);
            this.rbDrivingLicense.Name = "rbDrivingLicense";
            this.rbDrivingLicense.Size = new System.Drawing.Size(108, 18);
            this.rbDrivingLicense.TabIndex = 2;
            this.rbDrivingLicense.Text = "Driver\'s License";
            this.rbDrivingLicense.UseVisualStyleBackColor = true;
            this.rbDrivingLicense.CheckedChanged += new System.EventHandler(this.rbDrivingLicense_CheckedChanged);
            // 
            // pb_BackSide
            // 
            this.pb_BackSide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pb_BackSide.BackgroundImage")));
            this.pb_BackSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_BackSide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_BackSide.Location = new System.Drawing.Point(87, 284);
            this.pb_BackSide.Name = "pb_BackSide";
            this.pb_BackSide.Size = new System.Drawing.Size(379, 207);
            this.pb_BackSide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_BackSide.TabIndex = 114;
            this.pb_BackSide.TabStop = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(4, 504);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(542, 1);
            this.label5.TabIndex = 112;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(542, 1);
            this.label3.TabIndex = 111;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(546, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 505);
            this.label2.TabIndex = 110;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 505);
            this.label1.TabIndex = 109;
            // 
            // pb_FrontSide
            // 
            this.pb_FrontSide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pb_FrontSide.BackgroundImage")));
            this.pb_FrontSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_FrontSide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_FrontSide.Location = new System.Drawing.Point(87, 59);
            this.pb_FrontSide.Name = "pb_FrontSide";
            this.pb_FrontSide.Size = new System.Drawing.Size(379, 207);
            this.pb_FrontSide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_FrontSide.TabIndex = 108;
            this.pb_FrontSide.TabStop = false;
            // 
            // imageControl1
            // 
            this.imageControl1.AutoScroll = true;
            this.imageControl1.CurrImage = null;
            this.imageControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.imageControl1.ImgPath = null;
            this.imageControl1.Location = new System.Drawing.Point(133, 124);
            this.imageControl1.Name = "imageControl1";
            this.imageControl1.Size = new System.Drawing.Size(100, 100);
            this.imageControl1.TabIndex = 117;
            this.imageControl1.Visible = false;
            // 
            // imageXView1
            // 
            //this.imageXView1.Location = new System.Drawing.Point(133, 124);
            //this.imageXView1.MouseWheelCapture = false;
            //this.imageXView1.Name = "imageXView1";
            //this.imageXView1.Size = new System.Drawing.Size(100, 100);
            //this.imageXView1.TabIndex = 113;
            //this.imageXView1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ts_Commands);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 54);
            this.panel1.TabIndex = 109;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloCardScanning.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Scan,
            this.tsb_LoadImage,
            this.tsb_ClearData,
            this.tsb_Print,
            this.tsb_Save,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(550, 53);
            this.ts_Commands.TabIndex = 109;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_Scan
            // 
            this.tsb_Scan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Scan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Scan.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Scan.Image")));
            this.tsb_Scan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Scan.Name = "tsb_Scan";
            this.tsb_Scan.Size = new System.Drawing.Size(40, 50);
            this.tsb_Scan.Tag = "Scan";
            this.tsb_Scan.Text = "Sca&n";
            this.tsb_Scan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Scan.ToolTipText = "Scan";
            this.tsb_Scan.Click += new System.EventHandler(this.tsb_Scan_Click);
            // 
            // tsb_LoadImage
            // 
            this.tsb_LoadImage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_LoadImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_LoadImage.Image = ((System.Drawing.Image)(resources.GetObject("tsb_LoadImage.Image")));
            this.tsb_LoadImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_LoadImage.Name = "tsb_LoadImage";
            this.tsb_LoadImage.Size = new System.Drawing.Size(83, 50);
            this.tsb_LoadImage.Tag = "Load Image";
            this.tsb_LoadImage.Text = "&Load Image";
            this.tsb_LoadImage.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_LoadImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_LoadImage.Click += new System.EventHandler(this.tsb_LoadImage_Click);
            // 
            // tsb_ClearData
            // 
            this.tsb_ClearData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ClearData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ClearData.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ClearData.Image")));
            this.tsb_ClearData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ClearData.Name = "tsb_ClearData";
            this.tsb_ClearData.Size = new System.Drawing.Size(83, 50);
            this.tsb_ClearData.Tag = "Clear";
            this.tsb_ClearData.Text = "Cl&ear Image";
            this.tsb_ClearData.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_ClearData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ClearData.ToolTipText = "Clear Image";
            this.tsb_ClearData.Click += new System.EventHandler(this.tsb_ClearData_Click);
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(41, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = "&Print";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.ToolTipText = "Print";
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(66, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save&&Cls";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save and Close";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
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
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDoc
            // 
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDoc_PrintPage);
            // 
            // frmCardImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(550, 652);
            this.Controls.Add(this.pnlCards);
            this.Controls.Add(this.pnlPatientStrip);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCardImage";
            this.ShowInTaskbar = false;
            this.Text = "Scan Card";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCardImage_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCardImage_FormClosed);
            this.Load += new System.EventHandler(this.frmCardImage_Load);
            this.Shown += new System.EventHandler(this.frmCardImage_Shown);
            this.pnlPatientStrip.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlSecInsurance.ResumeLayout(false);
            this.pnlPrInsurance.ResumeLayout(false);
            this.pnlOccupation.ResumeLayout(false);
            this.pnlGender.ResumeLayout(false);
            this.pnlPharmacyName.ResumeLayout(false);
            this.pnlPatientPhone.ResumeLayout(false);
            this.pnlProvider.ResumeLayout(false);
            this.pnlName.ResumeLayout(false);
            this.pnlCode.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlSocialSecurity.ResumeLayout(false);
            this.pnlReferralPhysician.ResumeLayout(false);
            this.pnlCellPhone.ResumeLayout(false);
            this.pnlHandDominance.ResumeLayout(false);
            this.pnlPharmacyFax.ResumeLayout(false);
            this.pnlPharmacyPhone.ResumeLayout(false);
            this.pnlDate.ResumeLayout(false);
            this.pnlAge.ResumeLayout(false);
            this.pnlDOB.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlCards.ResumeLayout(false);
            this.pnlCards.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BackSide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_FrontSide)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_FrontSide;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_LoadImage;
        internal System.Windows.Forms.ToolStripButton tsb_ClearData;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlPatientStrip;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlSecInsurance;
        private System.Windows.Forms.Label lblSecInsurance;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlPrInsurance;
        private System.Windows.Forms.Label lblPrInsurance;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlOccupation;
        private System.Windows.Forms.Label lblOccupation;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnlGender;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel pnlPharmacyName;
        private System.Windows.Forms.Label lblPharmacyName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel pnlPatientPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel pnlCode;
        private System.Windows.Forms.Label lblPatientCode;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlSocialSecurity;
        private System.Windows.Forms.Label lblSocialSecurity;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel pnlReferralPhysician;
        private System.Windows.Forms.Label lblReferralPhysician;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel pnlCellPhone;
        private System.Windows.Forms.Label lblCellPhone;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel pnlHandDominance;
        private System.Windows.Forms.Label lblHandDominance;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel pnlPharmacyFax;
        private System.Windows.Forms.Label lblPharmacyFax;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel pnlPharmacyPhone;
        private System.Windows.Forms.Label lblPharmacyPhone;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel pnlDate;
        private System.Windows.Forms.Label lblTodaysDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel pnlAge;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel pnlDOB;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlCards;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Scan;
        //private PegasusImaging.WinForms.ImagXpress9.ImageXView imageXView1;
        //private PegasusImaging.WinForms.ImagXpress9.ImagXpress imagXpress1;
        private PegasusImaging.WinForms.TwainPro5.TwainPro twainPro1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.PictureBox pb_BackSide;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.RadioButton rbInsuranceCard;
        private System.Windows.Forms.RadioButton rbDrivingLicense;
        private System.Windows.Forms.Label label6;
        private gloScanImaging.ImageControl imageControl1;
    }
}