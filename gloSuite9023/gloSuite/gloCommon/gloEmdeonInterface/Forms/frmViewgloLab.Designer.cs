namespace gloEmdeonInterface.Forms
{
    partial class frmViewgloLab
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
                    System.Windows.Forms.DateTimePicker [] cntmnuControls = { dtPickerFromDate, dtPickerToDate };
                    System.Windows.Forms.Control[] cntControls = { dtPickerFromDate, dtPickerToDate };
                    if (cntmnuControls != null)
                    {
                        if (cntmnuControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntmnuControls);

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
                try
                {
                    if (gloLabUC_Transaction1 != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(gloLabUC_Transaction1);
                        gloLabUC_Transaction1.Dispose();
                        gloLabUC_Transaction1 = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (GloUC_TransactionHistory != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_TransactionHistory);
                        GloUC_TransactionHistory.Dispose();
                        GloUC_TransactionHistory = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (gloUCLab_Transaction != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(gloUCLab_Transaction);
                        gloUCLab_Transaction.Dispose();
                        gloUCLab_Transaction = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (gloLabUC_LabFlowSheet1 != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(gloLabUC_LabFlowSheet1);
                        gloLabUC_LabFlowSheet1.Dispose();
                        gloLabUC_LabFlowSheet1 = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (gloUCLab_TestDetail != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(gloUCLab_TestDetail);
                        gloUCLab_TestDetail.Dispose();
                        gloUCLab_TestDetail = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (gloUCLab_History != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(gloUCLab_History);
                        gloUCLab_History.Dispose();
                        gloUCLab_History = null;
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
                    if (OContextMenu != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(OContextMenu);
                        if (OContextMenu.MenuItems != null)
                        {
                            OContextMenu.MenuItems.Clear();
                        }
                        OContextMenu.Dispose();
                        OContextMenu = null;
                    }
                }
                catch
                {
                }

                try
                {
                    if (c1TestLibrary.ContextMenu != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(c1TestLibrary.ContextMenu);
                        if (c1TestLibrary.ContextMenu.MenuItems != null)
                        {
                            c1TestLibrary.ContextMenu.MenuItems.Clear();
                        }
                        c1TestLibrary.ContextMenu.Dispose();
                        c1TestLibrary.ContextMenu = null;
                    }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewgloLab));
            this.tlbbtnSep3 = new System.Windows.Forms.ToolBarButton();
            this.Splitter3 = new System.Windows.Forms.Splitter();
            this.tlbbtnModifyPatient = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnHistory = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnNewPatient = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnSep4 = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnPrescription = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnOrders = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnSep2 = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnMedication = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnMasters = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnSep1 = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnMicrophone = new System.Windows.Forms.ToolBarButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnNewExam = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnPastExam = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnClose = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnUnFinishedExams = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnSep9 = new System.Windows.Forms.ToolBarButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabOrders = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gloLabUC_Transaction1 = new gloUserControlLibrary.gloLabUC_Transaction();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gloUCLab_Transaction = new gloUserControlLibrary.gloUC_Transaction();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlTransactionHistory = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.c1TestLibrary = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chk_PreviousHistory = new System.Windows.Forms.CheckBox();
            this.lblcmbOrderStatus = new System.Windows.Forms.Label();
            this.cmbOrderStatus = new System.Windows.Forms.ComboBox();
            this.dtPickerToDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtPickerFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.GloUC_TransactionHistory = new gloUserControlLibrary.gloUC_TransactionHistory();
            this.gloUCLab_TestDetail = new gloUserControlLibrary.gloUC_LabTest();
            this.tabResult = new System.Windows.Forms.TabPage();
            this.gloUC_TransactionHistory1 = new gloUserControlLibrary.gloUC_TransactionHistory();
            this.tabLabFlowsheet = new System.Windows.Forms.TabPage();
            this.gloLabUC_LabFlowSheet1 = new gloUserControlLibrary.gloLabUC_LabFlowSheet();
            this.tabResultSet = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.gloUCLab_OrderDetail = new gloUserControlLibrary.gloUC_LabOrderDetail();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlregistration = new System.Windows.Forms.Panel();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.lblProcessInformation = new System.Windows.Forms.Label();
            this.splRight = new System.Windows.Forms.Splitter();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tlbbtnLockScreen = new System.Windows.Forms.ToolBarButton();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_LabMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtn_Save = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_OnlySave = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Finish = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbbtn_AssignTask = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_LabOrder = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_RecordResults = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_EditOrder = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_New = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Print = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_HL7 = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Fax = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbbtn_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Message = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_PatientLetter = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_RefLetter = new System.Windows.Forms.ToolStripButton();
            this.tlbBtnPlanofTreatment = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Requisition = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_VWAcknowledgment = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Acknowledgment = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_ReviewAck = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_ViewHistory = new System.Windows.Forms.ToolStripButton();
            this.tblCDA = new System.Windows.Forms.ToolStripButton();
            this.tblCCD = new System.Windows.Forms.ToolStripButton();
            this.tlbResultRange = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_SendtoLab = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbBtnMergeOrder = new System.Windows.Forms.ToolStripButton();
            this.tlbBtnClinicalChart = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnFormGallery = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnSep8 = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnTasks = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnSettings = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnCalendar = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnMessages = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnSep5 = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnSep7 = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnSep10 = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnDocMGMT = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnScanDocs = new System.Windows.Forms.ToolBarButton();
            this.gloUCLab_History = new gloUserControlLibrary.gloUC_LabHistory();
            this.pnlMain.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabOrders.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlTransactionHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TestLibrary)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.tabLabFlowsheet.SuspendLayout();
            this.tabResultSet.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlregistration.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.ts_LabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlbbtnSep3
            // 
            this.tlbbtnSep3.Name = "tlbbtnSep3";
            this.tlbbtnSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // Splitter3
            // 
            this.Splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.Splitter3.Location = new System.Drawing.Point(3, 178);
            this.Splitter3.Name = "Splitter3";
            this.Splitter3.Size = new System.Drawing.Size(1160, 3);
            this.Splitter3.TabIndex = 29;
            this.Splitter3.TabStop = false;
            // 
            // tlbbtnModifyPatient
            // 
            this.tlbbtnModifyPatient.ImageIndex = 2;
            this.tlbbtnModifyPatient.Name = "tlbbtnModifyPatient";
            this.tlbbtnModifyPatient.Tag = "ModifyPatient";
            this.tlbbtnModifyPatient.ToolTipText = "Modify Patient";
            // 
            // tlbbtnHistory
            // 
            this.tlbbtnHistory.ImageIndex = 3;
            this.tlbbtnHistory.Name = "tlbbtnHistory";
            this.tlbbtnHistory.Tag = "History";
            this.tlbbtnHistory.ToolTipText = "Patient History";
            // 
            // tlbbtnNewPatient
            // 
            this.tlbbtnNewPatient.ImageIndex = 1;
            this.tlbbtnNewPatient.Name = "tlbbtnNewPatient";
            this.tlbbtnNewPatient.Tag = "NewPatient";
            this.tlbbtnNewPatient.ToolTipText = "New Patient";
            // 
            // tlbbtnSep4
            // 
            this.tlbbtnSep4.Name = "tlbbtnSep4";
            this.tlbbtnSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tlbbtnPrescription
            // 
            this.tlbbtnPrescription.ImageIndex = 4;
            this.tlbbtnPrescription.Name = "tlbbtnPrescription";
            this.tlbbtnPrescription.Tag = "Prescription";
            this.tlbbtnPrescription.ToolTipText = "Prescription";
            // 
            // tlbbtnOrders
            // 
            this.tlbbtnOrders.ImageIndex = 6;
            this.tlbbtnOrders.Name = "tlbbtnOrders";
            this.tlbbtnOrders.Tag = "Orders";
            this.tlbbtnOrders.ToolTipText = "View Orders";
            // 
            // tlbbtnSep2
            // 
            this.tlbbtnSep2.Name = "tlbbtnSep2";
            this.tlbbtnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tlbbtnMedication
            // 
            this.tlbbtnMedication.ImageIndex = 5;
            this.tlbbtnMedication.Name = "tlbbtnMedication";
            this.tlbbtnMedication.Tag = "Medication";
            this.tlbbtnMedication.ToolTipText = "Medication";
            // 
            // tlbbtnMasters
            // 
            this.tlbbtnMasters.ImageIndex = 0;
            this.tlbbtnMasters.Name = "tlbbtnMasters";
            this.tlbbtnMasters.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.tlbbtnMasters.Tag = "Masters";
            this.tlbbtnMasters.ToolTipText = "Masters";
            // 
            // tlbbtnSep1
            // 
            this.tlbbtnSep1.Name = "tlbbtnSep1";
            this.tlbbtnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.tlbbtnSep1.Visible = false;
            // 
            // tlbbtnMicrophone
            // 
            this.tlbbtnMicrophone.ImageIndex = 20;
            this.tlbbtnMicrophone.Name = "tlbbtnMicrophone";
            this.tlbbtnMicrophone.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tlbbtnMicrophone.Tag = "Microphone";
            this.tlbbtnMicrophone.ToolTipText = "Microphone On/Off";
            this.tlbbtnMicrophone.Visible = false;
            // 
            // tlbbtn_Close
            // 
            this.tlbbtn_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.BackgroundImage")));
            this.tlbbtn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.Image")));
            this.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Close.Name = "tlbbtn_Close";
            this.tlbbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tlbbtn_Close.Text = "&Close";
            this.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tlbbtnNewExam
            // 
            this.tlbbtnNewExam.ImageIndex = 8;
            this.tlbbtnNewExam.Name = "tlbbtnNewExam";
            this.tlbbtnNewExam.Tag = "NewExam";
            this.tlbbtnNewExam.ToolTipText = "New Exam";
            // 
            // tlbbtnPastExam
            // 
            this.tlbbtnPastExam.ImageIndex = 7;
            this.tlbbtnPastExam.Name = "tlbbtnPastExam";
            this.tlbbtnPastExam.Tag = "PastExams";
            this.tlbbtnPastExam.ToolTipText = "Past Exams";
            // 
            // tlbbtnClose
            // 
            this.tlbbtnClose.ImageIndex = 17;
            this.tlbbtnClose.Name = "tlbbtnClose";
            this.tlbbtnClose.Tag = "Close";
            this.tlbbtnClose.ToolTipText = "Close Application";
            // 
            // tlbbtnUnFinishedExams
            // 
            this.tlbbtnUnFinishedExams.ImageIndex = 9;
            this.tlbbtnUnFinishedExams.Name = "tlbbtnUnFinishedExams";
            this.tlbbtnUnFinishedExams.Tag = "UnFinishedExams";
            this.tlbbtnUnFinishedExams.ToolTipText = "UnFinished Exams";
            // 
            // tlbbtnSep9
            // 
            this.tlbbtnSep9.Name = "tlbbtnSep9";
            this.tlbbtnSep9.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.panel5);
            this.pnlMain.Controls.Add(this.gloUCLab_OrderDetail);
            this.pnlMain.Controls.Add(this.splitter1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 53);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlMain.Size = new System.Drawing.Size(1180, 821);
            this.pnlMain.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tabControl1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 148);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel5.Size = new System.Drawing.Size(1177, 673);
            this.panel5.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabOrders);
            this.tabControl1.Controls.Add(this.tabResult);
            this.tabControl1.Controls.Add(this.tabLabFlowsheet);
            this.tabControl1.Controls.Add(this.tabResultSet);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1174, 673);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabOrders
            // 
            this.tabOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabOrders.Controls.Add(this.panel1);
            this.tabOrders.Controls.Add(this.Splitter3);
            this.tabOrders.Controls.Add(this.panel4);
            this.tabOrders.Controls.Add(this.GloUC_TransactionHistory);
            this.tabOrders.Controls.Add(this.gloUCLab_TestDetail);
            this.tabOrders.Location = new System.Drawing.Point(4, 23);
            this.tabOrders.Name = "tabOrders";
            this.tabOrders.Padding = new System.Windows.Forms.Padding(3);
            this.tabOrders.Size = new System.Drawing.Size(1166, 646);
            this.tabOrders.TabIndex = 0;
            this.tabOrders.Text = "Orders";
            this.tabOrders.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.gloLabUC_Transaction1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.gloUCLab_Transaction);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 181);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1160, 378);
            this.panel1.TabIndex = 31;
            // 
            // gloLabUC_Transaction1
            // 
            this.gloLabUC_Transaction1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloLabUC_Transaction1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloLabUC_Transaction1.dtSelectedFromDt = new System.DateTime(((long)(0)));
            this.gloLabUC_Transaction1.dtSelectedToDt = new System.DateTime(((long)(0)));
            this.gloLabUC_Transaction1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloLabUC_Transaction1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloLabUC_Transaction1.IsCQMConceptDisplay = false;
            this.gloLabUC_Transaction1.IsLoadLastTransaction = false;
            this.gloLabUC_Transaction1.IsOrderLocked = false;
            this.gloLabUC_Transaction1.LabModified = false;
            this.gloLabUC_Transaction1.LabResultId = ((long)(0));
            this.gloLabUC_Transaction1.LabResultName = null;
            this.gloLabUC_Transaction1.LabTestId = ((long)(0));
            this.gloLabUC_Transaction1.LabTestName = null;
            this.gloLabUC_Transaction1.Location = new System.Drawing.Point(1, 1);
            this.gloLabUC_Transaction1.Name = "gloLabUC_Transaction1";
            this.gloLabUC_Transaction1.ParentControl = "";
            this.gloLabUC_Transaction1.PatientID = ((long)(0));
            this.gloLabUC_Transaction1.PreferredLab = null;
            this.gloLabUC_Transaction1.PreferredLabID = ((long)(0));
            this.gloLabUC_Transaction1.ProviderID = ((long)(0));
            this.gloLabUC_Transaction1.Size = new System.Drawing.Size(1158, 376);
            this.gloLabUC_Transaction1.TabIndex = 31;
            this.gloLabUC_Transaction1.TransactionType = gloUserControlLibrary.gloLabUC_Transaction.enumTransactionType.None;
            this.gloLabUC_Transaction1.gUC_TestSelected += new gloUserControlLibrary.gloLabUC_Transaction.gUC_TestSelectedEventHandler(this.gloUCLab_Transaction_gUC_TestSelected);
            this.gloLabUC_Transaction1.gUC_ScanDocument += new gloUserControlLibrary.gloLabUC_Transaction.gUC_ScanDocumentEventHandler(this.gloLabUC_Transaction1_gUC_ScanDocument);
            this.gloLabUC_Transaction1.gUC_ViewDocument += new gloUserControlLibrary.gloLabUC_Transaction.gUC_ViewDocumentEventHandler(this.gloLabUC_Transaction1_gUC_ViewDocument);
            this.gloLabUC_Transaction1.gUC_AddFormHandlerClick += new gloUserControlLibrary.gloLabUC_Transaction.gUC_AddFormHandlerClickEventHandler(this.gloLabUC_Transaction1_gUC_AddFormHandlerClick);
            this.gloLabUC_Transaction1.gUC_ViewDicomDocument += new gloUserControlLibrary.gloLabUC_Transaction.gUC_ViewDicomDocumentEventHandler(this.gloLabUC_Transaction1_gUC_ViewDicomDocument);
            this.gloLabUC_Transaction1.gUC_ButtonDiagnCPTClicked += new gloUserControlLibrary.gloLabUC_Transaction.gUC_ButtonDiagnCPTClickedEventHandler(this.gloLabUC_Transaction1_gUC_ButtonDiagnCPTClicked);
            this.gloLabUC_Transaction1.gUC_OkButtonClicked += new gloUserControlLibrary.gloLabUC_Transaction.gUC_OkButtonClickedEventHandler(this.glolabUC_Transaction1_gUC_OkButtonClicked);
            this.gloLabUC_Transaction1.LockOrder += new gloUserControlLibrary.gloLabUC_Transaction.LockOrderEventHandler(this.gloLabUC_Transaction1_LockOrder);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(0, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 376);
            this.label8.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(1159, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 376);
            this.label7.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1160, 1);
            this.label6.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(0, 377);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1160, 1);
            this.label5.TabIndex = 6;
            // 
            // gloUCLab_Transaction
            // 
            this.gloUCLab_Transaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUCLab_Transaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloUCLab_Transaction.dtSelectedFromDt = new System.DateTime(((long)(0)));
            this.gloUCLab_Transaction.dtSelectedToDt = new System.DateTime(((long)(0)));
            this.gloUCLab_Transaction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUCLab_Transaction.ForeColor = System.Drawing.Color.Black;
            this.gloUCLab_Transaction.LabResultId = ((long)(0));
            this.gloUCLab_Transaction.LabResultName = null;
            this.gloUCLab_Transaction.LabTestId = ((long)(0));
            this.gloUCLab_Transaction.LabTestName = null;
            this.gloUCLab_Transaction.Location = new System.Drawing.Point(0, 0);
            this.gloUCLab_Transaction.Name = "gloUCLab_Transaction";
            this.gloUCLab_Transaction.PatientID = ((long)(0));
            this.gloUCLab_Transaction.Size = new System.Drawing.Size(1160, 378);
            this.gloUCLab_Transaction.TabIndex = 30;
            this.gloUCLab_Transaction.TransactionType = gloUserControlLibrary.gloUC_Transaction.enumTransactionType.None;
            this.gloUCLab_Transaction.Visible = false;
            this.gloUCLab_Transaction.gUC_TestSelected += new gloUserControlLibrary.gloUC_Transaction.gUC_TestSelectedEventHandler(this.gloUCLab_Transaction_gUC_TestSelected);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pnlTransactionHistory);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1160, 175);
            this.panel4.TabIndex = 33;
            // 
            // pnlTransactionHistory
            // 
            this.pnlTransactionHistory.Controls.Add(this.label4);
            this.pnlTransactionHistory.Controls.Add(this.label3);
            this.pnlTransactionHistory.Controls.Add(this.label2);
            this.pnlTransactionHistory.Controls.Add(this.label1);
            this.pnlTransactionHistory.Controls.Add(this.c1TestLibrary);
            this.pnlTransactionHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTransactionHistory.Location = new System.Drawing.Point(0, 24);
            this.pnlTransactionHistory.Name = "pnlTransactionHistory";
            this.pnlTransactionHistory.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlTransactionHistory.Size = new System.Drawing.Size(1160, 151);
            this.pnlTransactionHistory.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(1, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1158, 1);
            this.label4.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(1, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1158, 1);
            this.label3.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(1159, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 148);
            this.label2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 148);
            this.label1.TabIndex = 2;
            // 
            // c1TestLibrary
            // 
            this.c1TestLibrary.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1TestLibrary.AllowEditing = false;
            this.c1TestLibrary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1TestLibrary.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1TestLibrary.ColumnInfo = "10,1,0,0,0,105,Columns:1{AllowDragging:False;}\t";
            this.c1TestLibrary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1TestLibrary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1TestLibrary.Location = new System.Drawing.Point(0, 3);
            this.c1TestLibrary.Name = "c1TestLibrary";
            this.c1TestLibrary.Rows.DefaultSize = 21;
            this.c1TestLibrary.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1TestLibrary.ShowCellLabels = true;
            this.c1TestLibrary.Size = new System.Drawing.Size(1160, 148);
            this.c1TestLibrary.StyleInfo = resources.GetString("c1TestLibrary.StyleInfo");
            this.c1TestLibrary.TabIndex = 1;
            this.c1TestLibrary.BeforeRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1TestLibrary_BeforeRowColChange);
            this.c1TestLibrary.RowColChange += new System.EventHandler(this.c1TestLibrary_RowColChange);
            this.c1TestLibrary.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1TestLibrary_MouseDoubleClick);
            this.c1TestLibrary.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1TestLibrary_MouseDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1160, 24);
            this.panel2.TabIndex = 32;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.chk_PreviousHistory);
            this.panel3.Controls.Add(this.lblcmbOrderStatus);
            this.panel3.Controls.Add(this.cmbOrderStatus);
            this.panel3.Controls.Add(this.dtPickerToDate);
            this.panel3.Controls.Add(this.lblToDate);
            this.panel3.Controls.Add(this.dtPickerFromDate);
            this.panel3.Controls.Add(this.lblFromDate);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1160, 24);
            this.panel3.TabIndex = 0;
            // 
            // chk_PreviousHistory
            // 
            this.chk_PreviousHistory.AutoSize = true;
            this.chk_PreviousHistory.BackColor = System.Drawing.Color.Transparent;
            this.chk_PreviousHistory.Dock = System.Windows.Forms.DockStyle.Right;
            this.chk_PreviousHistory.Location = new System.Drawing.Point(1035, 1);
            this.chk_PreviousHistory.Name = "chk_PreviousHistory";
            this.chk_PreviousHistory.Size = new System.Drawing.Size(124, 22);
            this.chk_PreviousHistory.TabIndex = 17;
            this.chk_PreviousHistory.Text = "Show prior results";
            this.chk_PreviousHistory.UseVisualStyleBackColor = false;
            this.chk_PreviousHistory.CheckedChanged += new System.EventHandler(this.chk_PreviousHistory_CheckedChanged);
            // 
            // lblcmbOrderStatus
            // 
            this.lblcmbOrderStatus.AutoSize = true;
            this.lblcmbOrderStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblcmbOrderStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcmbOrderStatus.Location = new System.Drawing.Point(422, 5);
            this.lblcmbOrderStatus.Name = "lblcmbOrderStatus";
            this.lblcmbOrderStatus.Size = new System.Drawing.Size(105, 14);
            this.lblcmbOrderStatus.TabIndex = 16;
            this.lblcmbOrderStatus.Text = "Acknowledged :";
            this.lblcmbOrderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbOrderStatus
            // 
            this.cmbOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrderStatus.FormattingEnabled = true;
            this.cmbOrderStatus.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cmbOrderStatus.Location = new System.Drawing.Point(529, 1);
            this.cmbOrderStatus.Name = "cmbOrderStatus";
            this.cmbOrderStatus.Size = new System.Drawing.Size(132, 22);
            this.cmbOrderStatus.TabIndex = 15;
            this.cmbOrderStatus.SelectedIndexChanged += new System.EventHandler(this.cmbOrderStatus_SelectedIndexChanged);
            this.cmbOrderStatus.SelectionChangeCommitted += new System.EventHandler(this.cmbOrderStatus_SelectionChangeCommitted);
            // 
            // dtPickerToDate
            // 
            this.dtPickerToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPickerToDate.Location = new System.Drawing.Point(296, 1);
            this.dtPickerToDate.Name = "dtPickerToDate";
            this.dtPickerToDate.Size = new System.Drawing.Size(105, 22);
            this.dtPickerToDate.TabIndex = 14;
            this.dtPickerToDate.ValueChanged += new System.EventHandler(this.dtPickerToDate_ValueChanged);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(267, 5);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(30, 14);
            this.lblToDate.TabIndex = 13;
            this.lblToDate.Text = "To :";
            // 
            // dtPickerFromDate
            // 
            this.dtPickerFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPickerFromDate.Location = new System.Drawing.Point(146, 1);
            this.dtPickerFromDate.Name = "dtPickerFromDate";
            this.dtPickerFromDate.Size = new System.Drawing.Size(105, 22);
            this.dtPickerFromDate.TabIndex = 12;
            this.dtPickerFromDate.ValueChanged += new System.EventHandler(this.dtPickerFromDate_ValueChanged);
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(102, 5);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(45, 14);
            this.lblFromDate.TabIndex = 11;
            this.lblFromDate.Text = "From :";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 22);
            this.label13.TabIndex = 10;
            this.label13.Text = "Order List :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(1159, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 22);
            this.label12.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(0, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 22);
            this.label11.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1160, 1);
            this.label10.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(0, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1160, 1);
            this.label9.TabIndex = 6;
            // 
            // GloUC_TransactionHistory
            // 
            this.GloUC_TransactionHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.GloUC_TransactionHistory.CurOrderID = ((long)(0));
            this.GloUC_TransactionHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GloUC_TransactionHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.GloUC_TransactionHistory.ForMerging = false;
            this.GloUC_TransactionHistory.HideCloseButton = false;
            this.GloUC_TransactionHistory.Location = new System.Drawing.Point(0, 0);
            this.GloUC_TransactionHistory.MergeOrderID = ((long)(0));
            this.GloUC_TransactionHistory.Name = "GloUC_TransactionHistory";
            this.GloUC_TransactionHistory.Padding = new System.Windows.Forms.Padding(3);
            this.GloUC_TransactionHistory.Size = new System.Drawing.Size(981, 49);
            this.GloUC_TransactionHistory.TabIndex = 0;
            this.GloUC_TransactionHistory.Visible = false;
            this.GloUC_TransactionHistory.btnShowGraphClick += new gloUserControlLibrary.gloUC_TransactionHistory.btnShowGraphClickEventHandler(this.GloUC_TransactionHistory_btnShowGraphClick);
            this.GloUC_TransactionHistory.gUC_ViewDocument += new gloUserControlLibrary.gloUC_TransactionHistory.gUC_ViewDocumentEventHandler(this.gloLabUC_Transaction1_gUC_ViewDocument);
            // 
            // gloUCLab_TestDetail
            // 
            this.gloUCLab_TestDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUCLab_TestDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gloUCLab_TestDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gloUCLab_TestDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUCLab_TestDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUCLab_TestDetail.Location = new System.Drawing.Point(3, 559);
            this.gloUCLab_TestDetail.Name = "gloUCLab_TestDetail";
            this.gloUCLab_TestDetail.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.gloUCLab_TestDetail.Size = new System.Drawing.Size(1160, 84);
            this.gloUCLab_TestDetail.TabIndex = 19;
            this.gloUCLab_TestDetail.Visible = false;
            this.gloUCLab_TestDetail.gUC_PrecuationChanged += new gloUserControlLibrary.gloUC_LabTest.gUC_PrecuationChangedEventHandler(this.gloUCLab_TestDetail_gUC_PrecuationChanged);
            this.gloUCLab_TestDetail.gUC_InstructionChanged += new gloUserControlLibrary.gloUC_LabTest.gUC_InstructionChangedEventHandler(this.gloUCLab_TestDetail_gUC_InstructionChanged);
            // 
            // tabResult
            // 
            this.tabResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabResult.Controls.Add(this.gloUC_TransactionHistory1);
            this.tabResult.Location = new System.Drawing.Point(4, 23);
            this.tabResult.Name = "tabResult";
            this.tabResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabResult.Size = new System.Drawing.Size(1166, 646);
            this.tabResult.TabIndex = 1;
            this.tabResult.Text = "Results";
            this.tabResult.UseVisualStyleBackColor = true;
            // 
            // gloUC_TransactionHistory1
            // 
            this.gloUC_TransactionHistory1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUC_TransactionHistory1.CurOrderID = ((long)(0));
            this.gloUC_TransactionHistory1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloUC_TransactionHistory1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUC_TransactionHistory1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUC_TransactionHistory1.ForMerging = false;
            this.gloUC_TransactionHistory1.HideCloseButton = false;
            this.gloUC_TransactionHistory1.Location = new System.Drawing.Point(3, 3);
            this.gloUC_TransactionHistory1.MergeOrderID = ((long)(0));
            this.gloUC_TransactionHistory1.Name = "gloUC_TransactionHistory1";
            this.gloUC_TransactionHistory1.Padding = new System.Windows.Forms.Padding(3);
            this.gloUC_TransactionHistory1.Size = new System.Drawing.Size(1160, 640);
            this.gloUC_TransactionHistory1.TabIndex = 1;
            this.gloUC_TransactionHistory1.btnShowGraphClick += new gloUserControlLibrary.gloUC_TransactionHistory.btnShowGraphClickEventHandler(this.GloUC_TransactionHistory_btnShowGraphClick);
            this.gloUC_TransactionHistory1.gUC_ViewDocument += new gloUserControlLibrary.gloUC_TransactionHistory.gUC_ViewDocumentEventHandler(this.gloLabUC_Transaction1_gUC_ViewDocument);
            // 
            // tabLabFlowsheet
            // 
            this.tabLabFlowsheet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabLabFlowsheet.Controls.Add(this.gloLabUC_LabFlowSheet1);
            this.tabLabFlowsheet.Location = new System.Drawing.Point(4, 23);
            this.tabLabFlowsheet.Name = "tabLabFlowsheet";
            this.tabLabFlowsheet.Size = new System.Drawing.Size(1166, 646);
            this.tabLabFlowsheet.TabIndex = 2;
            this.tabLabFlowsheet.Text = "Lab Flowsheet";
            this.tabLabFlowsheet.UseVisualStyleBackColor = true;
            // 
            // gloLabUC_LabFlowSheet1
            // 
            this.gloLabUC_LabFlowSheet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloLabUC_LabFlowSheet1.Location = new System.Drawing.Point(0, 0);
            this.gloLabUC_LabFlowSheet1.Name = "gloLabUC_LabFlowSheet1";
            this.gloLabUC_LabFlowSheet1.Size = new System.Drawing.Size(1166, 646);
            this.gloLabUC_LabFlowSheet1.TabIndex = 0;
            this.gloLabUC_LabFlowSheet1.gUC_LabFlowSheet_Print += new gloUserControlLibrary.gloLabUC_LabFlowSheet.gUC_LabFlowSheet_PrintEventHandler(this.gloLabUC_LabFlowSheet1_gUC_LabFlowSheet_Print);
            this.gloLabUC_LabFlowSheet1.gUC_LabFlowSheet_TestResultPrint += new gloUserControlLibrary.gloLabUC_LabFlowSheet.gUC_LabFlowSheet_TestResultPrintEventHandler(this.gloLabUC_LabFlowSheet1_gUC_LabFlowSheet_TestResultPrint);
            this.gloLabUC_LabFlowSheet1.gUC_LabFlowSheet_FlowSheetPrint += new gloUserControlLibrary.gloLabUC_LabFlowSheet.gUC_LabFlowSheet_FlowSheetPrintEventHandler(this.gloLabUC_LabFlowSheet1_gUC_LabFlowSheet_FlowSheetPrint);
            // 
            // tabResultSet
            // 
            this.tabResultSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabResultSet.Controls.Add(this.panel6);
            this.tabResultSet.Location = new System.Drawing.Point(4, 23);
            this.tabResultSet.Name = "tabResultSet";
            this.tabResultSet.Size = new System.Drawing.Size(1166, 646);
            this.tabResultSet.TabIndex = 3;
            this.tabResultSet.Text = "Result Sets";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.elementHost1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1166, 646);
            this.panel6.TabIndex = 1;
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(1166, 646);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // gloUCLab_OrderDetail
            // 
            this.gloUCLab_OrderDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUCLab_OrderDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gloUCLab_OrderDetail.ClinicID = ((long)(0));
            this.gloUCLab_OrderDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.gloUCLab_OrderDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUCLab_OrderDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUCLab_OrderDetail.IsOpenedFromViewLab = false;
            this.gloUCLab_OrderDetail.IsPreferredIDCleared = "False";
            this.gloUCLab_OrderDetail.Location = new System.Drawing.Point(0, 3);
            this.gloUCLab_OrderDetail.Name = "gloUCLab_OrderDetail";
            this.gloUCLab_OrderDetail.OrderLabType = "";
            this.gloUCLab_OrderDetail.OrderModified = false;
            this.gloUCLab_OrderDetail.OrderNumberID = ((long)(0));
            this.gloUCLab_OrderDetail.OrderNumberPrefix = null;
            this.gloUCLab_OrderDetail.OrderSelected = false;
            this.gloUCLab_OrderDetail.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.gloUCLab_OrderDetail.PreferredLab = null;
            this.gloUCLab_OrderDetail.PreferredLabID = ((long)(0));
            this.gloUCLab_OrderDetail.ReferredBy = null;
            this.gloUCLab_OrderDetail.ReferredByFName = "";
            this.gloUCLab_OrderDetail.ReferredByID = ((long)(0));
            this.gloUCLab_OrderDetail.ReferredByLName = "";
            this.gloUCLab_OrderDetail.ReferredByMName = "";
            this.gloUCLab_OrderDetail.ReferredTo = null;
            this.gloUCLab_OrderDetail.ReferredToID = ((long)(0));
            this.gloUCLab_OrderDetail.SampledBy = null;
            this.gloUCLab_OrderDetail.SampledByID = ((long)(0));
            this.gloUCLab_OrderDetail.SendTo = 1;
            this.gloUCLab_OrderDetail.Size = new System.Drawing.Size(1177, 145);
            this.gloUCLab_OrderDetail.TabIndex = 17;
            this.gloUCLab_OrderDetail.TaskDescription = null;
            this.gloUCLab_OrderDetail.TaskDueDate = new System.DateTime(((long)(0)));
            this.gloUCLab_OrderDetail.Visible = false;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(1177, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 818);
            this.splitter1.TabIndex = 18;
            this.splitter1.TabStop = false;
            // 
            // pnlregistration
            // 
            this.pnlregistration.BackColor = System.Drawing.Color.White;
            this.pnlregistration.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlregistration.BackgroundImage")));
            this.pnlregistration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlregistration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlregistration.Controls.Add(this.lblPleaseWait);
            this.pnlregistration.Controls.Add(this.lblProcessInformation);
            this.pnlregistration.Location = new System.Drawing.Point(369, 462);
            this.pnlregistration.Name = "pnlregistration";
            this.pnlregistration.Size = new System.Drawing.Size(423, 80);
            this.pnlregistration.TabIndex = 6;
            this.pnlregistration.Visible = false;
            this.pnlregistration.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlregistration_Paint);
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.AutoSize = true;
            this.lblPleaseWait.BackColor = System.Drawing.Color.Transparent;
            this.lblPleaseWait.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.Location = new System.Drawing.Point(20, 11);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(119, 19);
            this.lblPleaseWait.TabIndex = 0;
            this.lblPleaseWait.Text = "Please wait...";
            // 
            // lblProcessInformation
            // 
            this.lblProcessInformation.AutoSize = true;
            this.lblProcessInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblProcessInformation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessInformation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblProcessInformation.Location = new System.Drawing.Point(21, 42);
            this.lblProcessInformation.Name = "lblProcessInformation";
            this.lblProcessInformation.Size = new System.Drawing.Size(174, 14);
            this.lblProcessInformation.TabIndex = 0;
            this.lblProcessInformation.Text = "Request is being processed";
            // 
            // splRight
            // 
            this.splRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.splRight.Location = new System.Drawing.Point(1180, 53);
            this.splRight.Name = "splRight";
            this.splRight.Size = new System.Drawing.Size(3, 821);
            this.splRight.TabIndex = 7;
            this.splRight.TabStop = false;
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "");
            this.ImageList1.Images.SetKeyName(1, "");
            // 
            // tlbbtnLockScreen
            // 
            this.tlbbtnLockScreen.ImageIndex = 18;
            this.tlbbtnLockScreen.Name = "tlbbtnLockScreen";
            this.tlbbtnLockScreen.Tag = "LockScreen";
            this.tlbbtnLockScreen.ToolTipText = "Lock Screen";
            this.tlbbtnLockScreen.Visible = false;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.AutoSize = true;
            this.pnlToolStrip.Controls.Add(this.ts_LabMain);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1183, 53);
            this.pnlToolStrip.TabIndex = 6;
            // 
            // ts_LabMain
            // 
            this.ts_LabMain.BackColor = System.Drawing.Color.Transparent;
            this.ts_LabMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_LabMain.BackgroundImage")));
            this.ts_LabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_LabMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_LabMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_LabMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_LabMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtn_Close,
            this.tlbbtn_Save,
            this.tlbbtn_OnlySave,
            this.tlbbtn_Finish,
            this.toolStripSeparator1,
            this.tlbbtn_AssignTask,
            this.tlbbtn_LabOrder,
            this.tlbbtn_RecordResults,
            this.tlbbtn_EditOrder,
            this.tlbbtn_New,
            this.tlbbtn_Print,
            this.tlbbtn_HL7,
            this.tlbbtn_Fax,
            this.toolStripSeparator2,
            this.tlbbtn_Refresh,
            this.tlbbtn_Message,
            this.tlbbtn_PatientLetter,
            this.tlbbtn_RefLetter,
            this.tlbBtnPlanofTreatment,
            this.tlbbtn_Requisition,
            this.tlbbtn_VWAcknowledgment,
            this.tlbbtn_Acknowledgment,
            this.tlbbtn_ReviewAck,
            this.tlbbtn_ViewHistory,
            this.tblCDA,
            this.tblCCD,
            this.tlbResultRange,
            this.tlbbtn_SendtoLab,
            this.tlbbtnRefresh,
            this.toolStripSeparator3,
            this.tlbBtnMergeOrder,
            this.tlbBtnClinicalChart});
            this.ts_LabMain.Location = new System.Drawing.Point(0, 0);
            this.ts_LabMain.Name = "ts_LabMain";
            this.ts_LabMain.Size = new System.Drawing.Size(1183, 53);
            this.ts_LabMain.TabIndex = 0;
            this.ts_LabMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_LabMain_ItemClicked);
            // 
            // tlbbtn_Save
            // 
            this.tlbbtn_Save.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_Save.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Save.BackgroundImage")));
            this.tlbbtn_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Save.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Save.Image")));
            this.tlbbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Save.Name = "tlbbtn_Save";
            this.tlbbtn_Save.Size = new System.Drawing.Size(66, 50);
            this.tlbbtn_Save.Text = "&Save&&Cls";
            this.tlbbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Save.ToolTipText = "Save & Close";
            // 
            // tlbbtn_OnlySave
            // 
            this.tlbbtn_OnlySave.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_OnlySave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_OnlySave.BackgroundImage")));
            this.tlbbtn_OnlySave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_OnlySave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_OnlySave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_OnlySave.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_OnlySave.Image")));
            this.tlbbtn_OnlySave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_OnlySave.Name = "tlbbtn_OnlySave";
            this.tlbbtn_OnlySave.Size = new System.Drawing.Size(40, 50);
            this.tlbbtn_OnlySave.Tag = "Save";
            this.tlbbtn_OnlySave.Text = "Sa&ve";
            this.tlbbtn_OnlySave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_OnlySave.ToolTipText = "Save";
            // 
            // tlbbtn_Finish
            // 
            this.tlbbtn_Finish.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_Finish.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Finish.BackgroundImage")));
            this.tlbbtn_Finish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_Finish.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Finish.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Finish.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Finish.Image")));
            this.tlbbtn_Finish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Finish.Name = "tlbbtn_Finish";
            this.tlbbtn_Finish.Size = new System.Drawing.Size(45, 50);
            this.tlbbtn_Finish.Tag = "Finish";
            this.tlbbtn_Finish.Text = "&Finish";
            this.tlbbtn_Finish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Finish.ToolTipText = "Finish";
            this.tlbbtn_Finish.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            // 
            // tlbbtn_AssignTask
            // 
            this.tlbbtn_AssignTask.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_AssignTask.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_AssignTask.BackgroundImage")));
            this.tlbbtn_AssignTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_AssignTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_AssignTask.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_AssignTask.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_AssignTask.Image")));
            this.tlbbtn_AssignTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_AssignTask.Name = "tlbbtn_AssignTask";
            this.tlbbtn_AssignTask.Size = new System.Drawing.Size(82, 50);
            this.tlbbtn_AssignTask.Text = "Assign &Task";
            this.tlbbtn_AssignTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_AssignTask.ToolTipText = "Assign Task";
            // 
            // tlbbtn_LabOrder
            // 
            this.tlbbtn_LabOrder.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_LabOrder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_LabOrder.BackgroundImage")));
            this.tlbbtn_LabOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_LabOrder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_LabOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_LabOrder.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_LabOrder.Image")));
            this.tlbbtn_LabOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_LabOrder.Name = "tlbbtn_LabOrder";
            this.tlbbtn_LabOrder.Size = new System.Drawing.Size(71, 50);
            this.tlbbtn_LabOrder.Text = "&Lab Order";
            this.tlbbtn_LabOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_LabOrder.ToolTipText = "Lab Order";
            // 
            // tlbbtn_RecordResults
            // 
            this.tlbbtn_RecordResults.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_RecordResults.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_RecordResults.BackgroundImage")));
            this.tlbbtn_RecordResults.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_RecordResults.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_RecordResults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_RecordResults.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_RecordResults.Image")));
            this.tlbbtn_RecordResults.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_RecordResults.Name = "tlbbtn_RecordResults";
            this.tlbbtn_RecordResults.Size = new System.Drawing.Size(82, 50);
            this.tlbbtn_RecordResults.Text = "&Order Entry";
            this.tlbbtn_RecordResults.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_RecordResults.ToolTipText = "Order Entry";
            // 
            // tlbbtn_EditOrder
            // 
            this.tlbbtn_EditOrder.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_EditOrder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_EditOrder.BackgroundImage")));
            this.tlbbtn_EditOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_EditOrder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_EditOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_EditOrder.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_EditOrder.Image")));
            this.tlbbtn_EditOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_EditOrder.Name = "tlbbtn_EditOrder";
            this.tlbbtn_EditOrder.Size = new System.Drawing.Size(53, 50);
            this.tlbbtn_EditOrder.Text = "&Modify";
            this.tlbbtn_EditOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_EditOrder.ToolTipText = "Modify";
            // 
            // tlbbtn_New
            // 
            this.tlbbtn_New.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_New.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_New.BackgroundImage")));
            this.tlbbtn_New.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_New.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_New.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_New.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_New.Image")));
            this.tlbbtn_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_New.Name = "tlbbtn_New";
            this.tlbbtn_New.Size = new System.Drawing.Size(37, 50);
            this.tlbbtn_New.Text = "&New";
            this.tlbbtn_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_New.Visible = false;
            // 
            // tlbbtn_Print
            // 
            this.tlbbtn_Print.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_Print.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Print.BackgroundImage")));
            this.tlbbtn_Print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Print.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Print.Image")));
            this.tlbbtn_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Print.Name = "tlbbtn_Print";
            this.tlbbtn_Print.Size = new System.Drawing.Size(41, 50);
            this.tlbbtn_Print.Text = "&Print";
            this.tlbbtn_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tlbbtn_HL7
            // 
            this.tlbbtn_HL7.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_HL7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_HL7.BackgroundImage")));
            this.tlbbtn_HL7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_HL7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_HL7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_HL7.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_HL7.Image")));
            this.tlbbtn_HL7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_HL7.Name = "tlbbtn_HL7";
            this.tlbbtn_HL7.Size = new System.Drawing.Size(36, 50);
            this.tlbbtn_HL7.Text = "&HL7";
            this.tlbbtn_HL7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_HL7.ToolTipText = "HL7";
            // 
            // tlbbtn_Fax
            // 
            this.tlbbtn_Fax.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_Fax.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Fax.BackgroundImage")));
            this.tlbbtn_Fax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_Fax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Fax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Fax.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Fax.Image")));
            this.tlbbtn_Fax.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Fax.Name = "tlbbtn_Fax";
            this.tlbbtn_Fax.Size = new System.Drawing.Size(36, 50);
            this.tlbbtn_Fax.Text = "&Fax";
            this.tlbbtn_Fax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Fax.ToolTipText = "Fax  ";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 53);
            // 
            // tlbbtn_Refresh
            // 
            this.tlbbtn_Refresh.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_Refresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Refresh.BackgroundImage")));
            this.tlbbtn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Refresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Refresh.Image")));
            this.tlbbtn_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Refresh.Name = "tlbbtn_Refresh";
            this.tlbbtn_Refresh.Size = new System.Drawing.Size(53, 50);
            this.tlbbtn_Refresh.Text = "&Reload";
            this.tlbbtn_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Refresh.ToolTipText = "Reload";
            // 
            // tlbbtn_Message
            // 
            this.tlbbtn_Message.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_Message.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Message.BackgroundImage")));
            this.tlbbtn_Message.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_Message.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Message.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Message.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Message.Image")));
            this.tlbbtn_Message.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Message.Name = "tlbbtn_Message";
            this.tlbbtn_Message.Size = new System.Drawing.Size(69, 50);
            this.tlbbtn_Message.Text = "&Messages";
            this.tlbbtn_Message.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Message.ToolTipText = "Messages";
            // 
            // tlbbtn_PatientLetter
            // 
            this.tlbbtn_PatientLetter.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_PatientLetter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_PatientLetter.BackgroundImage")));
            this.tlbbtn_PatientLetter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_PatientLetter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_PatientLetter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_PatientLetter.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_PatientLetter.Image")));
            this.tlbbtn_PatientLetter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_PatientLetter.Name = "tlbbtn_PatientLetter";
            this.tlbbtn_PatientLetter.Size = new System.Drawing.Size(60, 50);
            this.tlbbtn_PatientLetter.Text = "P&at Ltrs";
            this.tlbbtn_PatientLetter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_PatientLetter.ToolTipText = "Patient Letters";
            // 
            // tlbbtn_RefLetter
            // 
            this.tlbbtn_RefLetter.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_RefLetter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_RefLetter.BackgroundImage")));
            this.tlbbtn_RefLetter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_RefLetter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_RefLetter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_RefLetter.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_RefLetter.Image")));
            this.tlbbtn_RefLetter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_RefLetter.Name = "tlbbtn_RefLetter";
            this.tlbbtn_RefLetter.Size = new System.Drawing.Size(60, 50);
            this.tlbbtn_RefLetter.Text = "&Ref Ltrs";
            this.tlbbtn_RefLetter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_RefLetter.ToolTipText = "Referrals Letters";
            // 
            // tlbBtnPlanofTreatment
            // 
            this.tlbBtnPlanofTreatment.BackColor = System.Drawing.Color.Transparent;
            this.tlbBtnPlanofTreatment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbBtnPlanofTreatment.BackgroundImage")));
            this.tlbBtnPlanofTreatment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbBtnPlanofTreatment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbBtnPlanofTreatment.Image = ((System.Drawing.Image)(resources.GetObject("tlbBtnPlanofTreatment.Image")));
            this.tlbBtnPlanofTreatment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbBtnPlanofTreatment.Name = "tlbBtnPlanofTreatment";
            this.tlbBtnPlanofTreatment.Size = new System.Drawing.Size(36, 50);
            this.tlbBtnPlanofTreatment.Tag = "PlanofTreatment";
            this.tlbBtnPlanofTreatment.Text = "PoT";
            this.tlbBtnPlanofTreatment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbBtnPlanofTreatment.ToolTipText = "Plan of Treatment";
            // 
            // tlbbtn_Requisition
            // 
            this.tlbbtn_Requisition.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_Requisition.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Requisition.BackgroundImage")));
            this.tlbbtn_Requisition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_Requisition.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Requisition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Requisition.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Requisition.Image")));
            this.tlbbtn_Requisition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Requisition.Name = "tlbbtn_Requisition";
            this.tlbbtn_Requisition.Size = new System.Drawing.Size(114, 50);
            this.tlbbtn_Requisition.Text = "&Print Requisition";
            this.tlbbtn_Requisition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Requisition.ToolTipText = "Print Requisition";
            // 
            // tlbbtn_VWAcknowledgment
            // 
            this.tlbbtn_VWAcknowledgment.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_VWAcknowledgment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_VWAcknowledgment.BackgroundImage")));
            this.tlbbtn_VWAcknowledgment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_VWAcknowledgment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_VWAcknowledgment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_VWAcknowledgment.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_VWAcknowledgment.Image")));
            this.tlbbtn_VWAcknowledgment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_VWAcknowledgment.Name = "tlbbtn_VWAcknowledgment";
            this.tlbbtn_VWAcknowledgment.Size = new System.Drawing.Size(77, 50);
            this.tlbbtn_VWAcknowledgment.Text = "&View Ackw";
            this.tlbbtn_VWAcknowledgment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_VWAcknowledgment.ToolTipText = "View Acknowledgment";
            this.tlbbtn_VWAcknowledgment.Visible = false;
            // 
            // tlbbtn_Acknowledgment
            // 
            this.tlbbtn_Acknowledgment.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_Acknowledgment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Acknowledgment.BackgroundImage")));
            this.tlbbtn_Acknowledgment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_Acknowledgment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Acknowledgment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Acknowledgment.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Acknowledgment.Image")));
            this.tlbbtn_Acknowledgment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Acknowledgment.Name = "tlbbtn_Acknowledgment";
            this.tlbbtn_Acknowledgment.Size = new System.Drawing.Size(93, 50);
            this.tlbbtn_Acknowledgment.Text = "&Acknowledge";
            this.tlbbtn_Acknowledgment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Acknowledgment.ToolTipText = "Acknowledge";
            // 
            // tlbbtn_ReviewAck
            // 
            this.tlbbtn_ReviewAck.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_ReviewAck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_ReviewAck.BackgroundImage")));
            this.tlbbtn_ReviewAck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_ReviewAck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_ReviewAck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_ReviewAck.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_ReviewAck.Image")));
            this.tlbbtn_ReviewAck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_ReviewAck.Name = "tlbbtn_ReviewAck";
            this.tlbbtn_ReviewAck.Size = new System.Drawing.Size(59, 50);
            this.tlbbtn_ReviewAck.Text = "Revie&w ";
            this.tlbbtn_ReviewAck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_ReviewAck.ToolTipText = "Review Acknowledgment";
            // 
            // tlbbtn_ViewHistory
            // 
            this.tlbbtn_ViewHistory.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_ViewHistory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_ViewHistory.BackgroundImage")));
            this.tlbbtn_ViewHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_ViewHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_ViewHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_ViewHistory.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_ViewHistory.Image")));
            this.tlbbtn_ViewHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_ViewHistory.Name = "tlbbtn_ViewHistory";
            this.tlbbtn_ViewHistory.Size = new System.Drawing.Size(125, 50);
            this.tlbbtn_ViewHistory.Text = "Ac&knowledgement";
            this.tlbbtn_ViewHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_ViewHistory.ToolTipText = "Acknowledgement";
            // 
            // tblCDA
            // 
            this.tblCDA.BackColor = System.Drawing.Color.Transparent;
            this.tblCDA.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tblCDA.BackgroundImage")));
            this.tblCDA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tblCDA.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblCDA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblCDA.Image = ((System.Drawing.Image)(resources.GetObject("tblCDA.Image")));
            this.tblCDA.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tblCDA.Name = "tblCDA";
            this.tblCDA.Size = new System.Drawing.Size(64, 50);
            this.tblCDA.Tag = "Gen CDA";
            this.tblCDA.Text = "Gen CD&A";
            this.tblCDA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tblCDA.ToolTipText = "Generate CDA";
            // 
            // tblCCD
            // 
            this.tblCCD.BackColor = System.Drawing.Color.Transparent;
            this.tblCCD.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tblCCD.BackgroundImage")));
            this.tblCCD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tblCCD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblCCD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblCCD.Image = ((System.Drawing.Image)(resources.GetObject("tblCCD.Image")));
            this.tblCCD.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tblCCD.Name = "tblCCD";
            this.tblCCD.Size = new System.Drawing.Size(63, 50);
            this.tblCCD.Tag = "Gen CCD";
            this.tblCCD.Text = "Gen CC&D";
            this.tblCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tblCCD.ToolTipText = "Generate CCD";
            // 
            // tlbResultRange
            // 
            this.tlbResultRange.BackColor = System.Drawing.Color.Transparent;
            this.tlbResultRange.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbResultRange.BackgroundImage")));
            this.tlbResultRange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbResultRange.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbResultRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbResultRange.Image = ((System.Drawing.Image)(resources.GetObject("tlbResultRange.Image")));
            this.tlbResultRange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbResultRange.Name = "tlbResultRange";
            this.tlbResultRange.Size = new System.Drawing.Size(93, 50);
            this.tlbResultRange.Tag = "ResultRange";
            this.tlbResultRange.Text = "&Result Range";
            this.tlbResultRange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbResultRange.ToolTipText = "Patient Specific Result Range";
            // 
            // tlbbtn_SendtoLab
            // 
            this.tlbbtn_SendtoLab.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtn_SendtoLab.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtn_SendtoLab.BackgroundImage")));
            this.tlbbtn_SendtoLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtn_SendtoLab.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_SendtoLab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_SendtoLab.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_SendtoLab.Image")));
            this.tlbbtn_SendtoLab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_SendtoLab.Name = "tlbbtn_SendtoLab";
            this.tlbbtn_SendtoLab.Size = new System.Drawing.Size(86, 50);
            this.tlbbtn_SendtoLab.Text = "&Send to Lab";
            this.tlbbtn_SendtoLab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_SendtoLab.ToolTipText = "Send to Lab";
            // 
            // tlbbtnRefresh
            // 
            this.tlbbtnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.tlbbtnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtnRefresh.BackgroundImage")));
            this.tlbbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnRefresh.Image")));
            this.tlbbtnRefresh.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tlbbtnRefresh.Name = "tlbbtnRefresh";
            this.tlbbtnRefresh.Size = new System.Drawing.Size(58, 50);
            this.tlbbtnRefresh.Tag = "Refresh";
            this.tlbbtnRefresh.Text = "Re&fresh";
            this.tlbbtnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnRefresh.ToolTipText = "Refresh";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 53);
            // 
            // tlbBtnMergeOrder
            // 
            this.tlbBtnMergeOrder.BackColor = System.Drawing.Color.Transparent;
            this.tlbBtnMergeOrder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbBtnMergeOrder.BackgroundImage")));
            this.tlbBtnMergeOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbBtnMergeOrder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbBtnMergeOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbBtnMergeOrder.Image = ((System.Drawing.Image)(resources.GetObject("tlbBtnMergeOrder.Image")));
            this.tlbBtnMergeOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbBtnMergeOrder.Name = "tlbBtnMergeOrder";
            this.tlbBtnMergeOrder.Size = new System.Drawing.Size(87, 50);
            this.tlbBtnMergeOrder.Text = "M&erge Order";
            this.tlbBtnMergeOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbBtnMergeOrder.ToolTipText = "Merge Order";
            this.tlbBtnMergeOrder.Visible = false;
            // 
            // tlbBtnClinicalChart
            // 
            this.tlbBtnClinicalChart.BackColor = System.Drawing.Color.Transparent;
            this.tlbBtnClinicalChart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbBtnClinicalChart.BackgroundImage")));
            this.tlbBtnClinicalChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbBtnClinicalChart.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbBtnClinicalChart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbBtnClinicalChart.Image = ((System.Drawing.Image)(resources.GetObject("tlbBtnClinicalChart.Image")));
            this.tlbBtnClinicalChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbBtnClinicalChart.Name = "tlbBtnClinicalChart";
            this.tlbBtnClinicalChart.Size = new System.Drawing.Size(90, 50);
            this.tlbBtnClinicalChart.Text = "Clinical C&hart";
            this.tlbBtnClinicalChart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbBtnClinicalChart.ToolTipText = "Clinical Chart";
            // 
            // tlbbtnFormGallery
            // 
            this.tlbbtnFormGallery.ImageIndex = 16;
            this.tlbbtnFormGallery.Name = "tlbbtnFormGallery";
            this.tlbbtnFormGallery.Tag = "FormGallery";
            this.tlbbtnFormGallery.ToolTipText = "Form Gallery";
            // 
            // tlbbtnSep8
            // 
            this.tlbbtnSep8.Name = "tlbbtnSep8";
            this.tlbbtnSep8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tlbbtnTasks
            // 
            this.tlbbtnTasks.ImageIndex = 11;
            this.tlbbtnTasks.Name = "tlbbtnTasks";
            this.tlbbtnTasks.Tag = "Tasks";
            this.tlbbtnTasks.ToolTipText = "Tasks";
            // 
            // tlbbtnSettings
            // 
            this.tlbbtnSettings.ImageIndex = 15;
            this.tlbbtnSettings.Name = "tlbbtnSettings";
            this.tlbbtnSettings.Tag = "Settings";
            this.tlbbtnSettings.ToolTipText = "Settings";
            // 
            // tlbbtnCalendar
            // 
            this.tlbbtnCalendar.ImageIndex = 10;
            this.tlbbtnCalendar.Name = "tlbbtnCalendar";
            this.tlbbtnCalendar.Tag = "Calendar";
            this.tlbbtnCalendar.ToolTipText = "Appointments";
            // 
            // tlbbtnMessages
            // 
            this.tlbbtnMessages.ImageIndex = 12;
            this.tlbbtnMessages.Name = "tlbbtnMessages";
            this.tlbbtnMessages.Tag = "Messages";
            this.tlbbtnMessages.ToolTipText = "Messages";
            // 
            // tlbbtnSep5
            // 
            this.tlbbtnSep5.Name = "tlbbtnSep5";
            this.tlbbtnSep5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tlbbtnSep7
            // 
            this.tlbbtnSep7.Name = "tlbbtnSep7";
            this.tlbbtnSep7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tlbbtnSep10
            // 
            this.tlbbtnSep10.Name = "tlbbtnSep10";
            this.tlbbtnSep10.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tlbbtnDocMGMT
            // 
            this.tlbbtnDocMGMT.ImageIndex = 13;
            this.tlbbtnDocMGMT.Name = "tlbbtnDocMGMT";
            this.tlbbtnDocMGMT.Tag = "DOCMGMT";
            this.tlbbtnDocMGMT.ToolTipText = "View Documents";
            // 
            // tlbbtnScanDocs
            // 
            this.tlbbtnScanDocs.ImageIndex = 14;
            this.tlbbtnScanDocs.Name = "tlbbtnScanDocs";
            this.tlbbtnScanDocs.Tag = "ScanDocs";
            this.tlbbtnScanDocs.ToolTipText = "Scan Documents";
            // 
            // gloUCLab_History
            // 
            this.gloUCLab_History.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUCLab_History.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUCLab_History.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUCLab_History.Location = new System.Drawing.Point(935, 56);
            this.gloUCLab_History.Name = "gloUCLab_History";
            this.gloUCLab_History.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.gloUCLab_History.Size = new System.Drawing.Size(250, 946);
            this.gloUCLab_History.TabIndex = 21;
            this.gloUCLab_History.Visible = false;
            this.gloUCLab_History.gUC_OpenLabForModify += new gloUserControlLibrary.gloUC_LabHistory.gUC_OpenLabForModifyEventHandler(this.gloUCLab_History_gUC_OpenLabForModify);
            this.gloUCLab_History.gUC_FillOrder += new gloUserControlLibrary.gloUC_LabHistory.gUC_FillOrderEventHandler(this.gloUCLab_History_gUC_FillOrder);
            // 
            // frmViewgloLab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1183, 874);
            this.Controls.Add(this.pnlregistration);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.splRight);
            this.Controls.Add(this.gloUCLab_History);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmViewgloLab";
            this.ShowInTaskbar = false;
            this.Text = "View Orders and Results";
            this.Activated += new System.EventHandler(this.frmViewgloLab_Activated);
            this.Deactivate += new System.EventHandler(this.frmViewgloLab_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmViewgloLab_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmViewgloLab_FormClosed);
            this.Load += new System.EventHandler(this.frmViewgloLab_Load);
            this.Resize += new System.EventHandler(this.frmViewgloLab_Resize);
            this.pnlMain.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabOrders.ResumeLayout(false);
            this.tabOrders.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlTransactionHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TestLibrary)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabResult.ResumeLayout(false);
            this.tabLabFlowsheet.ResumeLayout(false);
            this.tabResultSet.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.pnlregistration.ResumeLayout(false);
            this.pnlregistration.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_LabMain.ResumeLayout(false);
            this.ts_LabMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolBarButton tlbbtnSep3;
        internal System.Windows.Forms.Splitter Splitter3;
        internal System.Windows.Forms.ToolBarButton tlbbtnModifyPatient;
        internal System.Windows.Forms.ToolBarButton tlbbtnHistory;
        internal System.Windows.Forms.ToolBarButton tlbbtnNewPatient;
        internal System.Windows.Forms.ToolBarButton tlbbtnSep4;
        internal System.Windows.Forms.ToolBarButton tlbbtnPrescription;
        internal System.Windows.Forms.ToolBarButton tlbbtnOrders;
        internal System.Windows.Forms.ToolBarButton tlbbtnSep2;
        internal System.Windows.Forms.ToolBarButton tlbbtnMedication;
        internal System.Windows.Forms.ToolBarButton tlbbtnMasters;
        internal System.Windows.Forms.ToolBarButton tlbbtnSep1;
        internal System.Windows.Forms.ToolBarButton tlbbtnMicrophone;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        internal System.Windows.Forms.ToolBarButton tlbbtnNewExam;
        internal System.Windows.Forms.ToolBarButton tlbbtnPastExam;
        internal System.Windows.Forms.ToolBarButton tlbbtnClose;
        internal System.Windows.Forms.ToolBarButton tlbbtnUnFinishedExams;
        internal System.Windows.Forms.ToolBarButton tlbbtnSep9;
        public System.Windows.Forms.Panel pnlMain;
        internal System.Windows.Forms.Panel pnlTransactionHistory;
        internal gloUserControlLibrary.gloUC_TransactionHistory GloUC_TransactionHistory;
        internal gloUserControlLibrary.gloUC_LabTest gloUCLab_TestDetail;
        internal gloUserControlLibrary.gloUC_LabOrderDetail gloUCLab_OrderDetail;
        internal System.Windows.Forms.Splitter splRight;
        internal System.Windows.Forms.ImageList ImageList1;
        internal System.Windows.Forms.ToolBarButton tlbbtnLockScreen;
        internal System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_LabMain;
        internal System.Windows.Forms.ToolStripButton tlbbtn_New;
        internal System.Windows.Forms.ToolBarButton tlbbtnFormGallery;
        internal System.Windows.Forms.ToolBarButton tlbbtnSep8;
        internal System.Windows.Forms.ToolBarButton tlbbtnTasks;
        internal System.Windows.Forms.ToolBarButton tlbbtnSettings;
        internal System.Windows.Forms.ToolBarButton tlbbtnCalendar;
        internal System.Windows.Forms.ToolBarButton tlbbtnMessages;
        internal System.Windows.Forms.ToolBarButton tlbbtnSep5;
        internal System.Windows.Forms.ToolBarButton tlbbtnSep7;
        internal System.Windows.Forms.ToolBarButton tlbbtnSep10;
        internal System.Windows.Forms.ToolBarButton tlbbtnDocMGMT;
        internal System.Windows.Forms.ToolBarButton tlbbtnScanDocs;
        //internal System.Windows.Forms.PrintDialog PrintDialog1;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1TestLibrary;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        internal gloUserControlLibrary.gloUC_LabHistory gloUCLab_History;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnlregistration;
        private System.Windows.Forms.Label lblProcessInformation;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Refresh;
        private System.Windows.Forms.Label lblPleaseWait;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Save;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Print;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Fax;
        public System.Windows.Forms.ToolStripButton tlbbtn_Acknowledgment;
        public System.Windows.Forms.ToolStripButton tlbbtn_VWAcknowledgment;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabOrders;
        private System.Windows.Forms.TabPage tabResult;
        private System.Windows.Forms.Panel panel5;
        internal gloUserControlLibrary.gloUC_TransactionHistory gloUC_TransactionHistory1;
        internal gloUserControlLibrary.gloUC_Transaction gloUCLab_Transaction;
        private gloUserControlLibrary.gloLabUC_Transaction gloLabUC_Transaction1;
        internal System.Windows.Forms.ToolStripButton tlbbtn_SendtoLab;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Finish;
        private System.Windows.Forms.Label lblcmbOrderStatus;
        private System.Windows.Forms.ComboBox cmbOrderStatus;
        private System.Windows.Forms.DateTimePicker dtPickerToDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtPickerFromDate;
        private System.Windows.Forms.Label lblFromDate;
        internal System.Windows.Forms.Splitter splitter1;
        public System.Windows.Forms.ToolStripButton tlbbtn_ViewHistory;
        public System.Windows.Forms.ToolStripButton tlbbtn_ReviewAck;
        internal System.Windows.Forms.ToolStripButton tlbbtn_HL7;
        public System.Windows.Forms.ToolStripButton tlbbtn_AssignTask;
        private System.Windows.Forms.CheckBox chk_PreviousHistory;
        public System.Windows.Forms.ToolStripButton tlbbtn_LabOrder;
        public System.Windows.Forms.ToolStripButton tlbbtn_RecordResults;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton tlbbtn_EditOrder;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Requisition;
        internal System.Windows.Forms.ToolStripButton tblCCD;
        private System.Windows.Forms.TabPage tabLabFlowsheet;
        private gloUserControlLibrary.gloLabUC_LabFlowSheet gloLabUC_LabFlowSheet1;
        internal System.Windows.Forms.ToolStripButton tlbbtn_OnlySave;
        public System.Windows.Forms.ToolStripButton tlbResultRange;
        internal System.Windows.Forms.ToolStripButton tlbbtnRefresh;
        private System.Windows.Forms.TabPage tabResultSet;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Panel panel6;
        internal System.Windows.Forms.ToolStripButton tlbbtn_PatientLetter;
        internal System.Windows.Forms.ToolStripButton tlbbtn_RefLetter;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Message;
        internal System.Windows.Forms.ToolStripButton tblCDA;
        public System.Windows.Forms.ToolStripButton tlbBtnMergeOrder;
        public System.Windows.Forms.ToolStripButton tlbBtnClinicalChart;
        private System.Windows.Forms.ToolStripButton tlbBtnPlanofTreatment;

    }
}
