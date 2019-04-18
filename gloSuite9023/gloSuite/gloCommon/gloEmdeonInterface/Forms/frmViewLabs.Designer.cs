namespace gloEmdeonInterface.Forms
{
    partial class frmViewLabs
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
                    if (PrintDialog1 != null)
                    {
                        PrintDialog1.Dispose();
                        PrintDialog1 = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (gloUC_PatientStrip1 != null)
                    {
                        gloUC_PatientStrip1.Dispose();
                        gloUC_PatientStrip1 = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewLabs));
            this.tlbbtnSep3 = new System.Windows.Forms.ToolBarButton();
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
            this.tlbbtn_PrvLabs = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnNewExam = new System.Windows.Forms.ToolBarButton();
            this.tlbbtn_Emdeon = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnPastExam = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnClose = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnUnFinishedExams = new System.Windows.Forms.ToolBarButton();
            this.tlbbtnSep9 = new System.Windows.Forms.ToolBarButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.GloUC_TransactionHistory = new gloUserControlLibrary.gloUC_TransactionHistory();
            this.pnlTransactionHistory = new System.Windows.Forms.Panel();
            this.gloUCLab_Transaction = new gloUserControlLibrary.gloUC_Transaction();
            this.c1TestLibrary = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.gloUCLab_TestDetail = new gloUserControlLibrary.gloUC_LabTest();
            this.gloUCLab_OrderDetail = new gloUserControlLibrary.gloUC_LabOrderDetail();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tlbbtnLockScreen = new System.Windows.Forms.ToolBarButton();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_LabMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtn_New = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Save = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Finish = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Print = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Fax = new System.Windows.Forms.ToolStripButton();
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
            this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
            this.gloUCLab_History = new gloUserControlLibrary.gloUC_LabHistory();
            this.pnlMain.SuspendLayout();
            this.pnlTransactionHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TestLibrary)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.ts_LabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlbbtnSep3
            // 
            this.tlbbtnSep3.Name = "tlbbtnSep3";
            this.tlbbtnSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
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
            // tlbbtn_PrvLabs
            // 
            this.tlbbtn_PrvLabs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_PrvLabs.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_PrvLabs.Image")));
            this.tlbbtn_PrvLabs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_PrvLabs.Name = "tlbbtn_PrvLabs";
            this.tlbbtn_PrvLabs.Size = new System.Drawing.Size(59, 50);
            this.tlbbtn_PrvLabs.Text = "PrvLabs";
            this.tlbbtn_PrvLabs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_PrvLabs.ToolTipText = "Previous Labs";
            this.tlbbtn_PrvLabs.Visible = false;
            // 
            // tlbbtn_Close
            // 
            this.tlbbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.Image")));
            this.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Close.Name = "tlbbtn_Close";
            this.tlbbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tlbbtn_Close.Text = "Close";
            this.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tlbbtnNewExam
            // 
            this.tlbbtnNewExam.ImageIndex = 8;
            this.tlbbtnNewExam.Name = "tlbbtnNewExam";
            this.tlbbtnNewExam.Tag = "NewExam";
            this.tlbbtnNewExam.ToolTipText = "New Exam";
            // 
            // tlbbtn_Emdeon
            // 
            this.tlbbtn_Emdeon.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Emdeon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Emdeon.Name = "tlbbtn_Emdeon";
            this.tlbbtn_Emdeon.Size = new System.Drawing.Size(52, 50);
            this.tlbbtn_Emdeon.Text = "gloLab";
            this.tlbbtn_Emdeon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Emdeon.ToolTipText = "Launch Emdeon";
            this.tlbbtn_Emdeon.Visible = false;
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
            this.pnlMain.Controls.Add(this.GloUC_TransactionHistory);
            this.pnlMain.Controls.Add(this.pnlTransactionHistory);
            this.pnlMain.Location = new System.Drawing.Point(0, 56);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(917, 709);
            this.pnlMain.TabIndex = 7;
            // 
            // GloUC_TransactionHistory
            // 
            this.GloUC_TransactionHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.GloUC_TransactionHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GloUC_TransactionHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.GloUC_TransactionHistory.Location = new System.Drawing.Point(109, 499);
            this.GloUC_TransactionHistory.Name = "GloUC_TransactionHistory";
            this.GloUC_TransactionHistory.Padding = new System.Windows.Forms.Padding(3);
            this.GloUC_TransactionHistory.Size = new System.Drawing.Size(460, 36);
            this.GloUC_TransactionHistory.TabIndex = 0;
            // 
            // pnlTransactionHistory
            // 
            this.pnlTransactionHistory.Controls.Add(this.gloUCLab_Transaction);
            this.pnlTransactionHistory.Controls.Add(this.c1TestLibrary);
            this.pnlTransactionHistory.Location = new System.Drawing.Point(3, 30);
            this.pnlTransactionHistory.Name = "pnlTransactionHistory";
            this.pnlTransactionHistory.Size = new System.Drawing.Size(890, 463);
            this.pnlTransactionHistory.TabIndex = 28;
            // 
            // gloUCLab_Transaction
            // 
            this.gloUCLab_Transaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUCLab_Transaction.dtSelectedFromDt = new System.DateTime(((long)(0)));
            this.gloUCLab_Transaction.dtSelectedToDt = new System.DateTime(((long)(0)));
            this.gloUCLab_Transaction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUCLab_Transaction.ForeColor = System.Drawing.Color.Black;
            this.gloUCLab_Transaction.LabResultId = ((long)(0));
            this.gloUCLab_Transaction.LabResultName = null;
            this.gloUCLab_Transaction.LabTestId = ((long)(0));
            this.gloUCLab_Transaction.LabTestName = null;
            this.gloUCLab_Transaction.Location = new System.Drawing.Point(1, 266);
            this.gloUCLab_Transaction.Name = "gloUCLab_Transaction";
            this.gloUCLab_Transaction.Size = new System.Drawing.Size(889, 194);
            this.gloUCLab_Transaction.TabIndex = 30;
            this.gloUCLab_Transaction.TransactionType = gloUserControlLibrary.gloUC_Transaction.enumTransactionType.None;
            // 
            // c1TestLibrary
            // 
            this.c1TestLibrary.AllowEditing = false;
            this.c1TestLibrary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1TestLibrary.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1TestLibrary.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1TestLibrary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1TestLibrary.Location = new System.Drawing.Point(9, 42);
            this.c1TestLibrary.Name = "c1TestLibrary";
            this.c1TestLibrary.Rows.DefaultSize = 19;
            this.c1TestLibrary.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1TestLibrary.ShowCellLabels = true;
            this.c1TestLibrary.Size = new System.Drawing.Size(881, 179);
            this.c1TestLibrary.StyleInfo = resources.GetString("c1TestLibrary.StyleInfo");
            this.c1TestLibrary.TabIndex = 1;
            this.c1TestLibrary.DoubleClick += new System.EventHandler(this.c1TestLibrary_DoubleClick);
            // 
            // gloUCLab_TestDetail
            // 
            this.gloUCLab_TestDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUCLab_TestDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gloUCLab_TestDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUCLab_TestDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUCLab_TestDetail.Location = new System.Drawing.Point(12, 500);
            this.gloUCLab_TestDetail.Name = "gloUCLab_TestDetail";
            this.gloUCLab_TestDetail.Size = new System.Drawing.Size(892, 84);
            this.gloUCLab_TestDetail.TabIndex = 19;
            // 
            // gloUCLab_OrderDetail
            // 
            this.gloUCLab_OrderDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUCLab_OrderDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gloUCLab_OrderDetail.ClinicID = ((long)(0));
            this.gloUCLab_OrderDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUCLab_OrderDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUCLab_OrderDetail.Location = new System.Drawing.Point(778, 56);
            this.gloUCLab_OrderDetail.Name = "gloUCLab_OrderDetail";
            this.gloUCLab_OrderDetail.OrderNumberID = ((short)(0));
            this.gloUCLab_OrderDetail.OrderNumberPrefix = null;
            this.gloUCLab_OrderDetail.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.gloUCLab_OrderDetail.PreferredLab = null;
            this.gloUCLab_OrderDetail.PreferredLabID = ((long)(0));
            this.gloUCLab_OrderDetail.ReferredBy = null;
            this.gloUCLab_OrderDetail.ReferredByFName = "";
            this.gloUCLab_OrderDetail.ReferredByID = ((long)(0));
            this.gloUCLab_OrderDetail.ReferredByLName = "";
            this.gloUCLab_OrderDetail.ReferredByMName = "";
            this.gloUCLab_OrderDetail.SampledBy = null;
            this.gloUCLab_OrderDetail.SampledByID = ((long)(0));
            this.gloUCLab_OrderDetail.Size = new System.Drawing.Size(340, 127);
            this.gloUCLab_OrderDetail.TabIndex = 17;
            this.gloUCLab_OrderDetail.TaskDescription = null;
            this.gloUCLab_OrderDetail.TaskDueDate = new System.DateTime(((long)(0)));
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
            this.pnlToolStrip.Controls.Add(this.ts_LabMain);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1121, 54);
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
            this.tlbbtn_New,
            this.tlbbtn_Save,
            this.tlbbtn_Finish,
            this.tlbbtn_Close,
            this.tlbbtn_Print,
            this.tlbbtn_Fax,
            this.tlbbtn_PrvLabs,
            this.tlbbtn_Emdeon});
            this.ts_LabMain.Location = new System.Drawing.Point(0, 0);
            this.ts_LabMain.Name = "ts_LabMain";
            this.ts_LabMain.Size = new System.Drawing.Size(1121, 53);
            this.ts_LabMain.TabIndex = 0;
            this.ts_LabMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_LabMain_ItemClicked);
            // 
            // tlbbtn_New
            // 
            this.tlbbtn_New.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_New.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_New.Image")));
            this.tlbbtn_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_New.Name = "tlbbtn_New";
            this.tlbbtn_New.Size = new System.Drawing.Size(37, 50);
            this.tlbbtn_New.Text = "New";
            this.tlbbtn_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tlbbtn_Save
            // 
            this.tlbbtn_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Save.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Save.Image")));
            this.tlbbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Save.Name = "tlbbtn_Save";
            this.tlbbtn_Save.Size = new System.Drawing.Size(66, 50);
            this.tlbbtn_Save.Text = "Save&&Cls";
            this.tlbbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Save.ToolTipText = "Save and Close";
            // 
            // tlbbtn_Finish
            // 
            this.tlbbtn_Finish.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Finish.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Finish.Image")));
            this.tlbbtn_Finish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Finish.Name = "tlbbtn_Finish";
            this.tlbbtn_Finish.Size = new System.Drawing.Size(45, 50);
            this.tlbbtn_Finish.Text = "Finish";
            this.tlbbtn_Finish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tlbbtn_Print
            // 
            this.tlbbtn_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Print.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Print.Image")));
            this.tlbbtn_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Print.Name = "tlbbtn_Print";
            this.tlbbtn_Print.Size = new System.Drawing.Size(41, 50);
            this.tlbbtn_Print.Text = "Print";
            this.tlbbtn_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Print.Visible = false;
            // 
            // tlbbtn_Fax
            // 
            this.tlbbtn_Fax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Fax.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Fax.Image")));
            this.tlbbtn_Fax.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Fax.Name = "tlbbtn_Fax";
            this.tlbbtn_Fax.Size = new System.Drawing.Size(36, 50);
            this.tlbbtn_Fax.Text = "Fax";
            this.tlbbtn_Fax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Fax.ToolTipText = "Fax  ";
            this.tlbbtn_Fax.Visible = false;
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
            // PrintDialog1
            // 
            this.PrintDialog1.UseEXDialog = true;
            // 
            // gloUCLab_History
            // 
            this.gloUCLab_History.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUCLab_History.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUCLab_History.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUCLab_History.Location = new System.Drawing.Point(919, 56);
            this.gloUCLab_History.Name = "gloUCLab_History";
            this.gloUCLab_History.Size = new System.Drawing.Size(199, 595);
            this.gloUCLab_History.TabIndex = 20;
            this.gloUCLab_History.gUC_FillOrder += new gloUserControlLibrary.gloUC_LabHistory.gUC_FillOrderEventHandler(this.gloUCLab_History_gUC_FillOrder);
            this.gloUCLab_History.gUC_OpenLabForModify += new gloUserControlLibrary.gloUC_LabHistory.gUC_OpenLabForModifyEventHandler(this.gloUCLab_History_gUC_OpenLabForModify);
            // 
            // frmViewLabs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1121, 765);
            this.Controls.Add(this.gloUCLab_History);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.gloUCLab_OrderDetail);
            this.Controls.Add(this.gloUCLab_TestDetail);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewLabs";
            this.Text = " Lab Orders";
            this.Load += new System.EventHandler(this.frmViewLabs_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlTransactionHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TestLibrary)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_LabMain.ResumeLayout(false);
            this.ts_LabMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ToolBarButton tlbbtnSep3;
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
        internal System.Windows.Forms.ToolStripButton tlbbtn_PrvLabs;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        internal System.Windows.Forms.ToolBarButton tlbbtnNewExam;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Emdeon;
        internal System.Windows.Forms.ToolBarButton tlbbtnPastExam;
        internal System.Windows.Forms.ToolBarButton tlbbtnClose;
        internal System.Windows.Forms.ToolBarButton tlbbtnUnFinishedExams;
        internal System.Windows.Forms.ToolBarButton tlbbtnSep9;
        internal System.Windows.Forms.Panel pnlMain;
        internal gloUserControlLibrary.gloUC_Transaction gloUCLab_Transaction;
        internal System.Windows.Forms.Panel pnlTransactionHistory;
        internal gloUserControlLibrary.gloUC_TransactionHistory GloUC_TransactionHistory;
        internal gloUserControlLibrary.gloUC_LabTest gloUCLab_TestDetail;
        internal gloUserControlLibrary.gloUC_LabOrderDetail gloUCLab_OrderDetail;
        internal System.Windows.Forms.ImageList ImageList1;
        internal System.Windows.Forms.ToolBarButton tlbbtnLockScreen;
        internal System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_LabMain;
        internal System.Windows.Forms.ToolStripButton tlbbtn_New;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Save;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Finish;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Print;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Fax;
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
        internal System.Windows.Forms.PrintDialog PrintDialog1;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1TestLibrary;
        internal gloUserControlLibrary.gloUC_LabHistory gloUCLab_History;

    }
}