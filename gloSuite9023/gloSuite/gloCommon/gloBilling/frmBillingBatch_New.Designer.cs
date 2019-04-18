namespace gloBilling
{
    partial class frmBillingBatch_New
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
        private static frmBillingBatch_New frm;


        public static frmBillingBatch_New GetInstance()
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmBillingBatch_New();
                }
            }
            finally
            {

            }
            return frm;
        }
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this.blnDisposed))
            {
                if ((components != null))
                {
                    components.Dispose();
                }
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

                    try
                    {


                        System.Windows.Forms.ContextMenuStrip[] dtpControls = { cntmnuShow997 };
                        System.Windows.Forms.Control[] cntControls = { cntmnuShow997 };
                            if (dtpControls != null)
                            {
                                if (dtpControls.Length > 0)
                                {
                                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControls);

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

                    // Dispose managed resources. 
                  
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
            }
            frm = null;
            this.blnDisposed = true;
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            // Take yourself off of the finalization queue 
            // to prevent finalization code for this object 
            // from executing a second time. 
            System.GC.SuppressFinalize(this);
        }

        ~frmBillingBatch_New()
        {
            Dispose(false);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillingBatch_New));
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_ApplyFilter = new System.Windows.Forms.ToolStripButton();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print1500 = new System.Windows.Forms.ToolStripButton();
            this.tsb_PrintNew1500 = new System.Windows.Forms.ToolStripButton();
            this.tsb_WorkerComp = new System.Windows.Forms.ToolStripButton();
            this.tsb_UB04 = new System.Windows.Forms.ToolStripButton();
            this.tsb_DeleteBatch = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_Void = new System.Windows.Forms.ToolStripButton();
            this.tsb_Select = new System.Windows.Forms.ToolStripButton();
            this.tsb_ReQueue = new System.Windows.Forms.ToolStripButton();
            this.tsb_Queue = new System.Windows.Forms.ToolStripButton();
            this.tsb_Validate = new System.Windows.Forms.ToolStripButton();
            this.tsb_Batch = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsb_Batch_New = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_Batch_Existing = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsb_BatchSend = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsb_ValidateNBatch = new System.Windows.Forms.ToolStripButton();
            this.tsb_Accept = new System.Windows.Forms.ToolStripButton();
            this.tsb_Send = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsb_Send_PaperClaim = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_Send_ElectronicClaim = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_Finished = new System.Windows.Forms.ToolStripButton();
            this.tsb_Reject = new System.Windows.Forms.ToolStripButton();
            this.tsb_RejectBatch = new System.Windows.Forms.ToolStripButton();
            this.tsb_BatchDetailReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_Resend = new System.Windows.Forms.ToolStripButton();
            this.tsb_ClaimStatus = new System.Windows.Forms.ToolStripButton();
            this.tsb_toSecondary = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsb_Elec_Secondary = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_PrintForm = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_View = new System.Windows.Forms.ToolStripButton();
            this.tsb_PrintClaimForm = new System.Windows.Forms.ToolStripButton();
            this.tsb_PrintClaimData = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.cntmnuShow997 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItem_Show997 = new System.Windows.Forms.ToolStripMenuItem();
            this.imgLst = new System.Windows.Forms.ImageList(this.components);
            this.tabManager = new System.Windows.Forms.TabControl();
            this.tbpg_Charges = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label39 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.c1QueuedClaims = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label49 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.cmbReportingCategory = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.lblInsurance = new System.Windows.Forms.Label();
            this.lblClearingHouseCharges = new System.Windows.Forms.Label();
            this.cmbInsuranceCompany = new System.Windows.Forms.ComboBox();
            this.cmbClearingHouse = new System.Windows.Forms.ComboBox();
            this.cmbMultiChargesTray = new System.Windows.Forms.ComboBox();
            this.btnBrowseUser = new System.Windows.Forms.Button();
            this.label50 = new System.Windows.Forms.Label();
            this.chkSelloggedUser = new System.Windows.Forms.CheckBox();
            this.label54 = new System.Windows.Forms.Label();
            this.btnClearUser = new System.Windows.Forms.Button();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.btnBrowseMultiChargesTray = new System.Windows.Forms.Button();
            this.btnClearMultiChargesTray = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.cmbMultiFacility = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.btnClearMultiFacility = new System.Windows.Forms.Button();
            this.btnBrowseMultiFacility = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnBrowseMultiProvider = new System.Windows.Forms.Button();
            this.pnlBusinessCenter = new System.Windows.Forms.Panel();
            this.cmbBusinessCenter = new System.Windows.Forms.ComboBox();
            this.label64 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cmbBillingMethod = new System.Windows.Forms.ComboBox();
            this.maskedCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.chkLstCloseDate = new System.Windows.Forms.CheckBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbSecondaryClaimsCharges = new System.Windows.Forms.RadioButton();
            this.rbPrimaryClaimsCharges = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.chkQueueGeneralSearch = new System.Windows.Forms.CheckBox();
            this.chkQueueClaimCount = new System.Windows.Forms.CheckBox();
            this.numQueueClaimCount = new System.Windows.Forms.NumericUpDown();
            this.txtQueueSearch = new System.Windows.Forms.TextBox();
            this.lblQueueSearch = new System.Windows.Forms.Label();
            this.btnUP = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbpg_Batch = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.c1BatchGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.txtBatchSearch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.btnClearClaimSearchUnsent = new System.Windows.Forms.Button();
            this.label77 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblBillingmethodvalue = new System.Windows.Forms.Label();
            this.lblBillingmethod = new System.Windows.Forms.Label();
            this.lblClearinghouseValue = new System.Windows.Forms.Label();
            this.lblClearinghouse = new System.Windows.Forms.Label();
            this.lblBatchDateValue = new System.Windows.Forms.Label();
            this.lblcalimamtvalue = new System.Windows.Forms.Label();
            this.lblBatchDate = new System.Windows.Forms.Label();
            this.lblClamamtvalue = new System.Windows.Forms.Label();
            this.lblClaimcountvalue = new System.Windows.Forms.Label();
            this.lblNumClaim = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlBase = new System.Windows.Forms.Panel();
            this.c1trvBatch = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.trvBatch = new System.Windows.Forms.TreeView();
            this.pnlBatchSearch = new System.Windows.Forms.Panel();
            this.txtSearchUnsentBatches = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.btnBatchUnsentSearchClear = new System.Windows.Forms.Button();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnlSearchBottomBrd = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbl_pnlBaseBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlBaseLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlBaseRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlBaseTopBrd = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbSecondayClaimsBatch = new System.Windows.Forms.RadioButton();
            this.rbPrimaryClaimsBatch = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.chkBatchClaimCount = new System.Windows.Forms.CheckBox();
            this.numBatchClaimCount = new System.Windows.Forms.NumericUpDown();
            this.chkBatchGeneralSearch = new System.Windows.Forms.CheckBox();
            this.lblSearhBatch = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pnlProgressBar = new System.Windows.Forms.Panel();
            this.lblFileCounter = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.prgFileGeneration = new System.Windows.Forms.ProgressBar();
            this.label21 = new System.Windows.Forms.Label();
            this.tbpg_SentBatch = new System.Windows.Forms.TabPage();
            this.panel9SentBatch = new System.Windows.Forms.Panel();
            this.c1BatchGridSentBatch = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.txtBatchSearch_SentBatch = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.btnClearClaimSearchSent = new System.Windows.Forms.Button();
            this.label97 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.panel6SentBatch = new System.Windows.Forms.Panel();
            this.lblBillingmethodvalue_SentBatch = new System.Windows.Forms.Label();
            this.lblBillingmethod_SentBatch = new System.Windows.Forms.Label();
            this.lblClearinghouseValue_SentBatch = new System.Windows.Forms.Label();
            this.lblClearinghouse_SentBatch = new System.Windows.Forms.Label();
            this.lblBatchDateValue_SentBatch = new System.Windows.Forms.Label();
            this.lblcalimamtvalue_SentBatch = new System.Windows.Forms.Label();
            this.lblBatchDate_SentBatch = new System.Windows.Forms.Label();
            this.lblClamamtvalue_SentBatch = new System.Windows.Forms.Label();
            this.lblClaimcountvalue_SentBatch = new System.Windows.Forms.Label();
            this.lblNumClaim_SentBatch = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.pnlBase_SentBatch = new System.Windows.Forms.Panel();
            this.c1trvBatch_SentBatch = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.trvBatch_SentBatch = new System.Windows.Forms.TreeView();
            this.panel19 = new System.Windows.Forms.Panel();
            this.txtSearchSentBatches = new System.Windows.Forms.TextBox();
            this.label108 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.btnSentBatchSearchClear = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label110 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.panel14_SentBatch = new System.Windows.Forms.Panel();
            this.rbSecondaryClaimsBatch_SentBatch = new System.Windows.Forms.RadioButton();
            this.rbPrimaryClaimsBatch_SentBatch = new System.Windows.Forms.RadioButton();
            this.label2_SentBatch = new System.Windows.Forms.Label();
            this.chkBatchClaimCount_SentBatch = new System.Windows.Forms.CheckBox();
            this.numBatchClaimCount_SentBatch = new System.Windows.Forms.NumericUpDown();
            this.chkBatchGeneralSearch_SentBatch = new System.Windows.Forms.CheckBox();
            this.lblSearhBatch_SentBatch = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.pnlProgressBar_SentBatch = new System.Windows.Forms.Panel();
            this.lblFileCounter_SentBatch = new System.Windows.Forms.Label();
            this.lblFile_SentBatch = new System.Windows.Forms.Label();
            this.prgFileGeneration_SentBatch = new System.Windows.Forms.ProgressBar();
            this.label21_SentBatch = new System.Windows.Forms.Label();
            this.tbpg_ClaimManager = new System.Windows.Forms.TabPage();
            this.pnlGrids = new System.Windows.Forms.Panel();
            this.pnlSubBatch = new System.Windows.Forms.Panel();
            this.c1SubBatch = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlSearchBatchClaim = new System.Windows.Forms.Panel();
            this.panel24 = new System.Windows.Forms.Panel();
            this.txtSearchSubBatch = new System.Windows.Forms.TextBox();
            this.label131 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.btn_ClearC1SubBatch = new System.Windows.Forms.Button();
            this.label134 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.lblSearchSubBatch = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.label138 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.pnl_ProgressBar = new System.Windows.Forms.Panel();
            this.prgProcess = new System.Windows.Forms.ProgressBar();
            this.pnllblBillingMethodValue = new System.Windows.Forms.Panel();
            this.lblBillingMethodValue_ClaimManager = new System.Windows.Forms.Label();
            this.lblBillingMethod_ClaimManager = new System.Windows.Forms.Label();
            this.lblBatchDateValue_ClaimManager = new System.Windows.Forms.Label();
            this.lblBatchDate_ClaimManager = new System.Windows.Forms.Label();
            this.lblClaimAmtValue_ClaimManager = new System.Windows.Forms.Label();
            this.lblClaimAmt_ClaimManager = new System.Windows.Forms.Label();
            this.lblClaimCountValue_ClaimManager = new System.Windows.Forms.Label();
            this.lblNumClaim_ClaimManger = new System.Windows.Forms.Label();
            this.label152 = new System.Windows.Forms.Label();
            this.label153 = new System.Windows.Forms.Label();
            this.label154 = new System.Windows.Forms.Label();
            this.label155 = new System.Windows.Forms.Label();
            this.splitter6 = new System.Windows.Forms.Splitter();
            this.pnlAllBatch = new System.Windows.Forms.Panel();
            this.c1AllBatch = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnltxtSearchAllBatch = new System.Windows.Forms.Panel();
            this.txtSearchAllBatch = new System.Windows.Forms.TextBox();
            this.label156 = new System.Windows.Forms.Label();
            this.label157 = new System.Windows.Forms.Label();
            this.btn_ClearC1AllBatch = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label158 = new System.Windows.Forms.Label();
            this.label160 = new System.Windows.Forms.Label();
            this.label161 = new System.Windows.Forms.Label();
            this.label162 = new System.Windows.Forms.Label();
            this.label163 = new System.Windows.Forms.Label();
            this.pnlCrossClaimSearch = new System.Windows.Forms.Panel();
            this.chkCrossClaimSearch = new System.Windows.Forms.CheckBox();
            this.label111 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.panel23 = new System.Windows.Forms.Panel();
            this.panel35 = new System.Windows.Forms.Panel();
            this.rbSubmitted = new System.Windows.Forms.RadioButton();
            this.rbToBeSubmitted = new System.Windows.Forms.RadioButton();
            this.label164 = new System.Windows.Forms.Label();
            this.label166 = new System.Windows.Forms.Label();
            this.label167 = new System.Windows.Forms.Label();
            this.label168 = new System.Windows.Forms.Label();
            this.label169 = new System.Windows.Forms.Label();
            this.label170 = new System.Windows.Forms.Label();
            this.pnlClaimHeader = new System.Windows.Forms.Panel();
            this.label150 = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.pnllblStatements = new System.Windows.Forms.Panel();
            this.label149 = new System.Windows.Forms.Label();
            this.label151 = new System.Windows.Forms.Label();
            this.label159 = new System.Windows.Forms.Label();
            this.lblStatements = new System.Windows.Forms.Label();
            this.pnllbl837PaperClaims = new System.Windows.Forms.Panel();
            this.label136 = new System.Windows.Forms.Label();
            this.label146 = new System.Windows.Forms.Label();
            this.label147 = new System.Windows.Forms.Label();
            this.label148 = new System.Windows.Forms.Label();
            this.lbl837PaperClaims = new System.Windows.Forms.Label();
            this.label171 = new System.Windows.Forms.Label();
            this.pnllbl837ElectronicClaims = new System.Windows.Forms.Panel();
            this.label127 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.label130 = new System.Windows.Forms.Label();
            this.lbl837ElectronicClaims = new System.Windows.Forms.Label();
            this.tbpg_OnHold = new System.Windows.Forms.TabPage();
            this.pnlPlanHold = new System.Windows.Forms.Panel();
            this.pnlc1PlanHold = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.c1PlanHold = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.pnlC1PlanHoldclaim = new System.Windows.Forms.Panel();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.C1PlanHoldclaim = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlBillingHold = new System.Windows.Forms.Panel();
            this.c1BillingHold = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label29 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.pnlPlanHoldLeft = new System.Windows.Forms.Panel();
            this.pnlgrdPlanHoldBalCalc = new System.Windows.Forms.Panel();
            this.c1HoldBalance = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.rbBillingHold = new System.Windows.Forms.RadioButton();
            this.rbPlanHold = new System.Windows.Forms.RadioButton();
            this.numHoldClaimCount = new System.Windows.Forms.NumericUpDown();
            this.txtSearchHoldClaims = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.tbpg_Void = new System.Windows.Forms.TabPage();
            this.panel17 = new System.Windows.Forms.Panel();
            this.c1VoidClaims = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.chkVoidClaimCount = new System.Windows.Forms.CheckBox();
            this.numVoidClaimCount = new System.Windows.Forms.NumericUpDown();
            this.chkVoidGeneralSearch = new System.Windows.Forms.CheckBox();
            this.txtVoidSearch = new System.Windows.Forms.TextBox();
            this.lblVoidSearch = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.tooltip_Billing = new System.Windows.Forms.ToolTip(this.components);
            this.C1SuperTooltipDx = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cntmnuUpdateBatchStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItem_UpdateStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_UpdateStatement = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_MarkBatchPrinted = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.cntmnuShow997.SuspendLayout();
            this.tabManager.SuspendLayout();
            this.tbpg_Charges.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1QueuedClaims)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel11.SuspendLayout();
            this.pnlBusinessCenter.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQueueClaimCount)).BeginInit();
            this.tbpg_Batch.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1BatchGrid)).BeginInit();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1trvBatch)).BeginInit();
            this.pnlBatchSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchClaimCount)).BeginInit();
            this.pnlProgressBar.SuspendLayout();
            this.tbpg_SentBatch.SuspendLayout();
            this.panel9SentBatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1BatchGridSentBatch)).BeginInit();
            this.panel13.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel6SentBatch.SuspendLayout();
            this.pnlBase_SentBatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1trvBatch_SentBatch)).BeginInit();
            this.panel19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel14_SentBatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchClaimCount_SentBatch)).BeginInit();
            this.pnlProgressBar_SentBatch.SuspendLayout();
            this.tbpg_ClaimManager.SuspendLayout();
            this.pnlGrids.SuspendLayout();
            this.pnlSubBatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SubBatch)).BeginInit();
            this.pnlSearchBatchClaim.SuspendLayout();
            this.panel24.SuspendLayout();
            this.pnl_ProgressBar.SuspendLayout();
            this.pnllblBillingMethodValue.SuspendLayout();
            this.pnlAllBatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1AllBatch)).BeginInit();
            this.pnltxtSearchAllBatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlCrossClaimSearch.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel35.SuspendLayout();
            this.pnlClaimHeader.SuspendLayout();
            this.pnllblStatements.SuspendLayout();
            this.pnllbl837PaperClaims.SuspendLayout();
            this.pnllbl837ElectronicClaims.SuspendLayout();
            this.tbpg_OnHold.SuspendLayout();
            this.pnlPlanHold.SuspendLayout();
            this.pnlc1PlanHold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PlanHold)).BeginInit();
            this.pnlC1PlanHoldclaim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1PlanHoldclaim)).BeginInit();
            this.pnlBillingHold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1BillingHold)).BeginInit();
            this.pnlPlanHoldLeft.SuspendLayout();
            this.pnlgrdPlanHoldBalCalc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1HoldBalance)).BeginInit();
            this.panel12.SuspendLayout();
            this.SearchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHoldClaimCount)).BeginInit();
            this.tbpg_Void.SuspendLayout();
            this.panel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1VoidClaims)).BeginInit();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVoidClaimCount)).BeginInit();
            this.cntmnuUpdateBatchStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1272, 54);
            this.panel2.TabIndex = 18;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_ApplyFilter,
            this.tsb_Modify,
            this.tsb_Print1500,
            this.tsb_PrintNew1500,
            this.tsb_WorkerComp,
            this.tsb_UB04,
            this.tsb_DeleteBatch,
            this.tsb_Delete,
            this.tsb_Void,
            this.tsb_Select,
            this.tsb_ReQueue,
            this.tsb_Queue,
            this.tsb_Validate,
            this.tsb_Batch,
            this.Tsb_BatchSend,
            this.tsb_ValidateNBatch,
            this.tsb_Accept,
            this.tsb_Send,
            this.tsb_Finished,
            this.tsb_Reject,
            this.tsb_RejectBatch,
            this.tsb_BatchDetailReport,
            this.tsb_Resend,
            this.tsb_ClaimStatus,
            this.tsb_toSecondary,
            this.tsb_PrintForm,
            this.tsb_Print,
            this.tsb_View,
            this.tsb_PrintClaimForm,
            this.tsb_PrintClaimData,
            this.tsb_Refresh,
            this.tsb_Close});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1272, 53);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_ApplyFilter
            // 
            this.tsb_ApplyFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ApplyFilter.Image")));
            this.tsb_ApplyFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ApplyFilter.Name = "tsb_ApplyFilter";
            this.tsb_ApplyFilter.Size = new System.Drawing.Size(66, 50);
            this.tsb_ApplyFilter.Text = "Generate";
            this.tsb_ApplyFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ApplyFilter.Click += new System.EventHandler(this.tsb_ApplyFilter_Click);
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(106, 50);
            this.tsb_Modify.Text = "Modify Charges";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Modify.Click += new System.EventHandler(this.tsb_Modify_Click);
            // 
            // tsb_Print1500
            // 
            this.tsb_Print1500.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print1500.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print1500.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print1500.Image")));
            this.tsb_Print1500.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print1500.Name = "tsb_Print1500";
            this.tsb_Print1500.Size = new System.Drawing.Size(113, 50);
            this.tsb_Print1500.Tag = "PrintHCFA1500";
            this.tsb_Print1500.Text = "CMS1500 08/05";
            this.tsb_Print1500.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print1500.Click += new System.EventHandler(this.tsb_Print1500_Click);
            // 
            // tsb_PrintNew1500
            // 
            this.tsb_PrintNew1500.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PrintNew1500.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PrintNew1500.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PrintNew1500.Image")));
            this.tsb_PrintNew1500.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PrintNew1500.Name = "tsb_PrintNew1500";
            this.tsb_PrintNew1500.Size = new System.Drawing.Size(113, 50);
            this.tsb_PrintNew1500.Tag = "PrintHCFA1500";
            this.tsb_PrintNew1500.Text = "CMS1500 02/12";
            this.tsb_PrintNew1500.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PrintNew1500.Click += new System.EventHandler(this.tsb_PrintNew1500_Click);
            // 
            // tsb_WorkerComp
            // 
            this.tsb_WorkerComp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_WorkerComp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_WorkerComp.Image = ((System.Drawing.Image)(resources.GetObject("tsb_WorkerComp.Image")));
            this.tsb_WorkerComp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_WorkerComp.Name = "tsb_WorkerComp";
            this.tsb_WorkerComp.Size = new System.Drawing.Size(94, 50);
            this.tsb_WorkerComp.Tag = "Worker Comp";
            this.tsb_WorkerComp.Text = "Worker Comp";
            this.tsb_WorkerComp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_WorkerComp.Visible = false;
            this.tsb_WorkerComp.Click += new System.EventHandler(this.tsb_WorkerComp_Click);
            // 
            // tsb_UB04
            // 
            this.tsb_UB04.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_UB04.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_UB04.Image = ((System.Drawing.Image)(resources.GetObject("tsb_UB04.Image")));
            this.tsb_UB04.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_UB04.Name = "tsb_UB04";
            this.tsb_UB04.Size = new System.Drawing.Size(47, 50);
            this.tsb_UB04.Tag = "PrintHCFA1500";
            this.tsb_UB04.Text = "UB 04";
            this.tsb_UB04.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_UB04.Click += new System.EventHandler(this.tsb_UB04_Click);
            // 
            // tsb_DeleteBatch
            // 
            this.tsb_DeleteBatch.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DeleteBatch.Image")));
            this.tsb_DeleteBatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DeleteBatch.Name = "tsb_DeleteBatch";
            this.tsb_DeleteBatch.Size = new System.Drawing.Size(89, 50);
            this.tsb_DeleteBatch.Tag = "BatchDelete";
            this.tsb_DeleteBatch.Text = "Delete Batch";
            this.tsb_DeleteBatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_DeleteBatch.Visible = false;
            this.tsb_DeleteBatch.Click += new System.EventHandler(this.tsb_DeleteBatch_Click);
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(50, 50);
            this.tsb_Delete.Text = "Delete";
            this.tsb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Delete.Click += new System.EventHandler(this.tsb_Delete_Click);
            // 
            // tsb_Void
            // 
            this.tsb_Void.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Void.Image")));
            this.tsb_Void.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Void.Name = "tsb_Void";
            this.tsb_Void.Size = new System.Drawing.Size(38, 50);
            this.tsb_Void.Text = "Void";
            this.tsb_Void.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Void.Visible = false;
            this.tsb_Void.Click += new System.EventHandler(this.tsb_Void_Click);
            // 
            // tsb_Select
            // 
            this.tsb_Select.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Select.Image")));
            this.tsb_Select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Select.Name = "tsb_Select";
            this.tsb_Select.Size = new System.Drawing.Size(48, 50);
            this.tsb_Select.Text = "Select";
            this.tsb_Select.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Select.Click += new System.EventHandler(this.tsb_Select_Click);
            // 
            // tsb_ReQueue
            // 
            this.tsb_ReQueue.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ReQueue.Image")));
            this.tsb_ReQueue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ReQueue.Name = "tsb_ReQueue";
            this.tsb_ReQueue.Size = new System.Drawing.Size(71, 50);
            this.tsb_ReQueue.Text = "Re-Queue";
            this.tsb_ReQueue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ReQueue.Click += new System.EventHandler(this.tsb_ReQueue_Click);
            // 
            // tsb_Queue
            // 
            this.tsb_Queue.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Queue.Image")));
            this.tsb_Queue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Queue.Name = "tsb_Queue";
            this.tsb_Queue.Size = new System.Drawing.Size(50, 50);
            this.tsb_Queue.Text = "Queue";
            this.tsb_Queue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Validate
            // 
            this.tsb_Validate.Enabled = false;
            this.tsb_Validate.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Validate.Image")));
            this.tsb_Validate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Validate.Name = "tsb_Validate";
            this.tsb_Validate.Size = new System.Drawing.Size(60, 50);
            this.tsb_Validate.Text = "Validate";
            this.tsb_Validate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Validate.Click += new System.EventHandler(this.tsb_Validate_Click);
            // 
            // tsb_Batch
            // 
            this.tsb_Batch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Batch_New,
            this.tsb_Batch_Existing});
            this.tsb_Batch.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Batch.Image")));
            this.tsb_Batch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Batch.Name = "tsb_Batch";
            this.tsb_Batch.Size = new System.Drawing.Size(55, 50);
            this.tsb_Batch.Text = "Batch";
            this.tsb_Batch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Batch_New
            // 
            this.tsb_Batch_New.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Batch_New.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Batch_New.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Batch_New.Image")));
            this.tsb_Batch_New.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Batch_New.Name = "tsb_Batch_New";
            this.tsb_Batch_New.Size = new System.Drawing.Size(122, 22);
            this.tsb_Batch_New.Text = "New";
            this.tsb_Batch_New.Click += new System.EventHandler(this.tsb_Batch_New_Click);
            // 
            // tsb_Batch_Existing
            // 
            this.tsb_Batch_Existing.Enabled = false;
            this.tsb_Batch_Existing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Batch_Existing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Batch_Existing.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Batch_Existing.Image")));
            this.tsb_Batch_Existing.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Batch_Existing.Name = "tsb_Batch_Existing";
            this.tsb_Batch_Existing.Size = new System.Drawing.Size(122, 22);
            this.tsb_Batch_Existing.Text = "Existing";
            this.tsb_Batch_Existing.Click += new System.EventHandler(this.tsb_Batch_Existing_Click);
            // 
            // Tsb_BatchSend
            // 
            this.Tsb_BatchSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Tsb_BatchSend.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.Tsb_BatchSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tsb_BatchSend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Tsb_BatchSend.Image = ((System.Drawing.Image)(resources.GetObject("Tsb_BatchSend.Image")));
            this.Tsb_BatchSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tsb_BatchSend.Name = "Tsb_BatchSend";
            this.Tsb_BatchSend.Size = new System.Drawing.Size(51, 50);
            this.Tsb_BatchSend.Tag = "Send";
            this.Tsb_BatchSend.Text = "&Send";
            this.Tsb_BatchSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_ValidateNBatch
            // 
            this.tsb_ValidateNBatch.Enabled = false;
            this.tsb_ValidateNBatch.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ValidateNBatch.Image")));
            this.tsb_ValidateNBatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ValidateNBatch.Name = "tsb_ValidateNBatch";
            this.tsb_ValidateNBatch.Size = new System.Drawing.Size(112, 50);
            this.tsb_ValidateNBatch.Text = "Validate && Batch";
            this.tsb_ValidateNBatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ValidateNBatch.ToolTipText = "Validate and Batch";
            this.tsb_ValidateNBatch.Visible = false;
            this.tsb_ValidateNBatch.Click += new System.EventHandler(this.tsb_ValidateNBatch_Click);
            // 
            // tsb_Accept
            // 
            this.tsb_Accept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_Accept.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb_Accept.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Accept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Accept.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Accept.Image")));
            this.tsb_Accept.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Accept.Name = "tsb_Accept";
            this.tsb_Accept.Size = new System.Drawing.Size(53, 50);
            this.tsb_Accept.Text = "Accept";
            this.tsb_Accept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Accept.Visible = false;
            this.tsb_Accept.Click += new System.EventHandler(this.tsb_Accept_Click);
            // 
            // tsb_Send
            // 
            this.tsb_Send.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_Send.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb_Send.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Send.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Send_PaperClaim,
            this.tsb_Send_ElectronicClaim});
            this.tsb_Send.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Send.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Send.Image")));
            this.tsb_Send.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Send.Name = "tsb_Send";
            this.tsb_Send.Size = new System.Drawing.Size(79, 50);
            this.tsb_Send.Text = "&Send_Old";
            this.tsb_Send.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Send.Visible = false;
            // 
            // tsb_Send_PaperClaim
            // 
            this.tsb_Send_PaperClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Send_PaperClaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Send_PaperClaim.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Send_PaperClaim.Image")));
            this.tsb_Send_PaperClaim.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Send_PaperClaim.Name = "tsb_Send_PaperClaim";
            this.tsb_Send_PaperClaim.Size = new System.Drawing.Size(169, 22);
            this.tsb_Send_PaperClaim.Text = "Paper Claim";
            this.tsb_Send_PaperClaim.Click += new System.EventHandler(this.tsb_Send_PaperClaim_Click);
            // 
            // tsb_Send_ElectronicClaim
            // 
            this.tsb_Send_ElectronicClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Send_ElectronicClaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Send_ElectronicClaim.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Send_ElectronicClaim.Image")));
            this.tsb_Send_ElectronicClaim.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Send_ElectronicClaim.Name = "tsb_Send_ElectronicClaim";
            this.tsb_Send_ElectronicClaim.Size = new System.Drawing.Size(169, 22);
            this.tsb_Send_ElectronicClaim.Text = "Electronic Claim";
            this.tsb_Send_ElectronicClaim.Click += new System.EventHandler(this.tsb_Send_ElectronicClaim_Click);
            // 
            // tsb_Finished
            // 
            this.tsb_Finished.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_Finished.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb_Finished.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Finished.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Finished.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Finished.Image")));
            this.tsb_Finished.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Finished.Name = "tsb_Finished";
            this.tsb_Finished.Size = new System.Drawing.Size(60, 50);
            this.tsb_Finished.Text = "&Finished";
            this.tsb_Finished.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Finished.Visible = false;
            this.tsb_Finished.Click += new System.EventHandler(this.tsb_Finished_Click);
            // 
            // tsb_Reject
            // 
            this.tsb_Reject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_Reject.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb_Reject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Reject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Reject.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Reject.Image")));
            this.tsb_Reject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Reject.Name = "tsb_Reject";
            this.tsb_Reject.Size = new System.Drawing.Size(50, 50);
            this.tsb_Reject.Tag = "Reject";
            this.tsb_Reject.Text = "Reject";
            this.tsb_Reject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Reject.Visible = false;
            this.tsb_Reject.Click += new System.EventHandler(this.tsb_Reject_Click);
            // 
            // tsb_RejectBatch
            // 
            this.tsb_RejectBatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_RejectBatch.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb_RejectBatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_RejectBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_RejectBatch.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RejectBatch.Image")));
            this.tsb_RejectBatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RejectBatch.Name = "tsb_RejectBatch";
            this.tsb_RejectBatch.Size = new System.Drawing.Size(89, 50);
            this.tsb_RejectBatch.Tag = "Reject Batch";
            this.tsb_RejectBatch.Text = "Reject Batch";
            this.tsb_RejectBatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_RejectBatch.Visible = false;
            this.tsb_RejectBatch.Click += new System.EventHandler(this.tsb_RejectBatch_Click);
            // 
            // tsb_BatchDetailReport
            // 
            this.tsb_BatchDetailReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_BatchDetailReport.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb_BatchDetailReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_BatchDetailReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_BatchDetailReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_BatchDetailReport.Image")));
            this.tsb_BatchDetailReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_BatchDetailReport.Name = "tsb_BatchDetailReport";
            this.tsb_BatchDetailReport.Size = new System.Drawing.Size(93, 50);
            this.tsb_BatchDetailReport.Tag = "Batch Report";
            this.tsb_BatchDetailReport.Text = "Batch Report";
            this.tsb_BatchDetailReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_BatchDetailReport.Visible = false;
            this.tsb_BatchDetailReport.Click += new System.EventHandler(this.tsb_BatchDetailReport_Click);
            // 
            // tsb_Resend
            // 
            this.tsb_Resend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_Resend.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb_Resend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Resend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Resend.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Resend.Image")));
            this.tsb_Resend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Resend.Name = "tsb_Resend";
            this.tsb_Resend.Size = new System.Drawing.Size(56, 50);
            this.tsb_Resend.Tag = "Resend";
            this.tsb_Resend.Text = "Resend";
            this.tsb_Resend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Resend.Click += new System.EventHandler(this.tsb_Resend_Click);
            // 
            // tsb_ClaimStatus
            // 
            this.tsb_ClaimStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_ClaimStatus.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb_ClaimStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_ClaimStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ClaimStatus.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ClaimStatus.Image")));
            this.tsb_ClaimStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ClaimStatus.Name = "tsb_ClaimStatus";
            this.tsb_ClaimStatus.Size = new System.Drawing.Size(88, 50);
            this.tsb_ClaimStatus.Text = "Claim Status";
            this.tsb_ClaimStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ClaimStatus.Visible = false;
            this.tsb_ClaimStatus.Click += new System.EventHandler(this.tsb_ClaimStatus_Click);
            // 
            // tsb_toSecondary
            // 
            this.tsb_toSecondary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_toSecondary.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb_toSecondary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_toSecondary.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Elec_Secondary});
            this.tsb_toSecondary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_toSecondary.Image = ((System.Drawing.Image)(resources.GetObject("tsb_toSecondary.Image")));
            this.tsb_toSecondary.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_toSecondary.Name = "tsb_toSecondary";
            this.tsb_toSecondary.Size = new System.Drawing.Size(120, 50);
            this.tsb_toSecondary.Text = "Secondary Claim";
            this.tsb_toSecondary.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_toSecondary.ToolTipText = "Send to Secondary";
            // 
            // tsb_Elec_Secondary
            // 
            this.tsb_Elec_Secondary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Elec_Secondary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Elec_Secondary.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Elec_Secondary.Image")));
            this.tsb_Elec_Secondary.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Elec_Secondary.Name = "tsb_Elec_Secondary";
            this.tsb_Elec_Secondary.Size = new System.Drawing.Size(204, 22);
            this.tsb_Elec_Secondary.Text = "Send Electronic Claim";
            // 
            // tsb_PrintForm
            // 
            this.tsb_PrintForm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PrintForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PrintForm.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PrintForm.Image")));
            this.tsb_PrintForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PrintForm.Name = "tsb_PrintForm";
            this.tsb_PrintForm.Size = new System.Drawing.Size(114, 50);
            this.tsb_PrintForm.Tag = "Print";
            this.tsb_PrintForm.Text = "Print Batch Form";
            this.tsb_PrintForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PrintForm.ToolTipText = "Print Form";
            this.tsb_PrintForm.Click += new System.EventHandler(this.tsb_PrintForm_Click);
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(113, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = "Print Batch Data";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.ToolTipText = "Print Data";
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
            // 
            // tsb_View
            // 
            this.tsb_View.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_View.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_View.Image = ((System.Drawing.Image)(resources.GetObject("tsb_View.Image")));
            this.tsb_View.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_View.Name = "tsb_View";
            this.tsb_View.Size = new System.Drawing.Size(40, 50);
            this.tsb_View.Tag = "View";
            this.tsb_View.Text = "&View";
            this.tsb_View.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_View.ToolTipText = "View File";
            this.tsb_View.Click += new System.EventHandler(this.tsb_View_Click);
            // 
            // tsb_PrintClaimForm
            // 
            this.tsb_PrintClaimForm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PrintClaimForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PrintClaimForm.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PrintClaimForm.Image")));
            this.tsb_PrintClaimForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PrintClaimForm.Name = "tsb_PrintClaimForm";
            this.tsb_PrintClaimForm.Size = new System.Drawing.Size(111, 50);
            this.tsb_PrintClaimForm.Tag = "Print";
            this.tsb_PrintClaimForm.Text = "Print Claim Form";
            this.tsb_PrintClaimForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PrintClaimForm.ToolTipText = "Print Form";
            this.tsb_PrintClaimForm.Click += new System.EventHandler(this.tsb_PrintClaimForm_Click);
            // 
            // tsb_PrintClaimData
            // 
            this.tsb_PrintClaimData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PrintClaimData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PrintClaimData.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PrintClaimData.Image")));
            this.tsb_PrintClaimData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PrintClaimData.Name = "tsb_PrintClaimData";
            this.tsb_PrintClaimData.Size = new System.Drawing.Size(110, 50);
            this.tsb_PrintClaimData.Tag = "Print";
            this.tsb_PrintClaimData.Text = "Print Claim Data";
            this.tsb_PrintClaimData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PrintClaimData.ToolTipText = "Print Data";
            this.tsb_PrintClaimData.Click += new System.EventHandler(this.tsb_PrintClaimData_Click);
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Refresh.Image")));
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tsb_Refresh.Text = "Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refresh.Click += new System.EventHandler(this.tsb_Refresh_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_Close.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Text = "Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // cntmnuShow997
            // 
            this.cntmnuShow997.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cntmnuShow997.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItem_Show997});
            this.cntmnuShow997.Name = "cmnu_Appointment";
            this.cntmnuShow997.Size = new System.Drawing.Size(156, 26);
            // 
            // mnuItem_Show997
            // 
            this.mnuItem_Show997.ForeColor = System.Drawing.Color.DarkBlue;
            this.mnuItem_Show997.Name = "mnuItem_Show997";
            this.mnuItem_Show997.Size = new System.Drawing.Size(155, 22);
            this.mnuItem_Show997.Text = "Show 997 Status";
            this.mnuItem_Show997.Click += new System.EventHandler(this.mnuItem_Show997_Click);
            // 
            // imgLst
            // 
            this.imgLst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLst.ImageStream")));
            this.imgLst.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLst.Images.SetKeyName(0, "");
            this.imgLst.Images.SetKeyName(1, "");
            this.imgLst.Images.SetKeyName(2, "");
            this.imgLst.Images.SetKeyName(3, "");
            this.imgLst.Images.SetKeyName(4, "");
            this.imgLst.Images.SetKeyName(5, "");
            this.imgLst.Images.SetKeyName(6, "");
            this.imgLst.Images.SetKeyName(7, "");
            this.imgLst.Images.SetKeyName(8, "");
            this.imgLst.Images.SetKeyName(9, "");
            this.imgLst.Images.SetKeyName(10, "");
            this.imgLst.Images.SetKeyName(11, "Blank.ico");
            this.imgLst.Images.SetKeyName(12, "Billing Hold.ico");
            this.imgLst.Images.SetKeyName(13, "Batch Unsend.ico");
            this.imgLst.Images.SetKeyName(14, "Batch Send.ico");
            this.imgLst.Images.SetKeyName(15, "Q_ClaimManager_16.ico");
            this.imgLst.Images.SetKeyName(16, "Clearing House01.ico");
            this.imgLst.Images.SetKeyName(17, "EDI.ico");
            this.imgLst.Images.SetKeyName(18, "CMS0805.ico");
            this.imgLst.Images.SetKeyName(19, "CMS0212.ico");
            this.imgLst.Images.SetKeyName(20, "UB04.ico");
            // 
            // tabManager
            // 
            this.tabManager.Controls.Add(this.tbpg_Charges);
            this.tabManager.Controls.Add(this.tbpg_Batch);
            this.tabManager.Controls.Add(this.tbpg_SentBatch);
            this.tabManager.Controls.Add(this.tbpg_ClaimManager);
            this.tabManager.Controls.Add(this.tbpg_OnHold);
            this.tabManager.Controls.Add(this.tbpg_Void);
            this.tabManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabManager.ImageList = this.imgLst;
            this.tabManager.ItemSize = new System.Drawing.Size(77, 19);
            this.tabManager.Location = new System.Drawing.Point(0, 54);
            this.tabManager.Name = "tabManager";
            this.tabManager.SelectedIndex = 0;
            this.tabManager.Size = new System.Drawing.Size(1272, 936);
            this.tabManager.TabIndex = 0;
            this.tabManager.TabStop = false;
            this.tabManager.SelectedIndexChanged += new System.EventHandler(this.tabManager_SelectedIndexChanged);
            this.tabManager.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabManager_Selected);
            // 
            // tbpg_Charges
            // 
            this.tbpg_Charges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_Charges.Controls.Add(this.panel1);
            this.tbpg_Charges.Controls.Add(this.panel5);
            this.tbpg_Charges.Controls.Add(this.panel3);
            this.tbpg_Charges.ImageIndex = 5;
            this.tbpg_Charges.Location = new System.Drawing.Point(4, 23);
            this.tbpg_Charges.Name = "tbpg_Charges";
            this.tbpg_Charges.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_Charges.Size = new System.Drawing.Size(1264, 909);
            this.tbpg_Charges.TabIndex = 0;
            this.tbpg_Charges.Tag = "Queue";
            this.tbpg_Charges.Text = "Charges";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.label39);
            this.panel1.Controls.Add(this.label48);
            this.panel1.Controls.Add(this.c1QueuedClaims);
            this.panel1.Controls.Add(this.label49);
            this.panel1.Controls.Add(this.label53);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 121);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel1.Size = new System.Drawing.Size(1258, 785);
            this.panel1.TabIndex = 560;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label39.Location = new System.Drawing.Point(1, 784);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(1256, 1);
            this.label39.TabIndex = 561;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Top;
            this.label48.Location = new System.Drawing.Point(1, 3);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(1256, 1);
            this.label48.TabIndex = 0;
            // 
            // c1QueuedClaims
            // 
            this.c1QueuedClaims.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1QueuedClaims.BackColor = System.Drawing.Color.White;
            this.c1QueuedClaims.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1QueuedClaims.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1QueuedClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1QueuedClaims.ExtendLastCol = true;
            this.c1QueuedClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1QueuedClaims.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1QueuedClaims.Location = new System.Drawing.Point(1, 3);
            this.c1QueuedClaims.Name = "c1QueuedClaims";
            this.c1QueuedClaims.Padding = new System.Windows.Forms.Padding(3);
            this.c1QueuedClaims.Rows.Count = 1;
            this.c1QueuedClaims.Rows.DefaultSize = 19;
            this.c1QueuedClaims.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1QueuedClaims.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1QueuedClaims.Size = new System.Drawing.Size(1256, 782);
            this.c1QueuedClaims.StyleInfo = resources.GetString("c1QueuedClaims.StyleInfo");
            this.c1QueuedClaims.TabIndex = 25;
            this.c1QueuedClaims.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1QueuedClaims_BeforeSort);
            this.c1QueuedClaims.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1QueuedClaims_AfterSort);
            this.c1QueuedClaims.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1QueuedClaims_AfterEdit);
            this.c1QueuedClaims.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1QueuedClaims_MouseClick);
            this.c1QueuedClaims.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1QueuedClaims_MouseDoubleClick);
            this.c1QueuedClaims.MouseLeave += new System.EventHandler(this.c1QueuedClaims_MouseLeave);
            this.c1QueuedClaims.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1QueuedClaims_MouseMove);
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Right;
            this.label49.Location = new System.Drawing.Point(1257, 3);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(1, 782);
            this.label49.TabIndex = 559;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Left;
            this.label53.Location = new System.Drawing.Point(0, 3);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 782);
            this.label53.TabIndex = 558;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel10);
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.label60);
            this.panel5.Controls.Add(this.label42);
            this.panel5.Controls.Add(this.label40);
            this.panel5.Controls.Add(this.label41);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 27);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel5.Size = new System.Drawing.Size(1258, 94);
            this.panel5.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.cmbReportingCategory);
            this.panel10.Controls.Add(this.label58);
            this.panel10.Controls.Add(this.lblInsurance);
            this.panel10.Controls.Add(this.lblClearingHouseCharges);
            this.panel10.Controls.Add(this.cmbInsuranceCompany);
            this.panel10.Controls.Add(this.cmbClearingHouse);
            this.panel10.Controls.Add(this.cmbMultiChargesTray);
            this.panel10.Controls.Add(this.btnBrowseUser);
            this.panel10.Controls.Add(this.label50);
            this.panel10.Controls.Add(this.chkSelloggedUser);
            this.panel10.Controls.Add(this.label54);
            this.panel10.Controls.Add(this.btnClearUser);
            this.panel10.Controls.Add(this.cmbUser);
            this.panel10.Controls.Add(this.btnBrowseMultiChargesTray);
            this.panel10.Controls.Add(this.btnClearMultiChargesTray);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(668, 4);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(589, 89);
            this.panel10.TabIndex = 2023;
            // 
            // cmbReportingCategory
            // 
            this.cmbReportingCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportingCategory.FormattingEnabled = true;
            this.cmbReportingCategory.Location = new System.Drawing.Point(190, 37);
            this.cmbReportingCategory.Name = "cmbReportingCategory";
            this.cmbReportingCategory.Size = new System.Drawing.Size(167, 22);
            this.cmbReportingCategory.TabIndex = 20;
            this.cmbReportingCategory.MouseEnter += new System.EventHandler(this.cmbReportingCategory_MouseEnter);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Location = new System.Drawing.Point(9, 41);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(178, 14);
            this.label58.TabIndex = 20;
            this.label58.Text = "Insurance Reporting Category :";
            // 
            // lblInsurance
            // 
            this.lblInsurance.AutoSize = true;
            this.lblInsurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsurance.Location = new System.Drawing.Point(65, 12);
            this.lblInsurance.Name = "lblInsurance";
            this.lblInsurance.Size = new System.Drawing.Size(122, 14);
            this.lblInsurance.TabIndex = 7;
            this.lblInsurance.Text = "Insurance Company :";
            // 
            // lblClearingHouseCharges
            // 
            this.lblClearingHouseCharges.AutoSize = true;
            this.lblClearingHouseCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClearingHouseCharges.Location = new System.Drawing.Point(96, 69);
            this.lblClearingHouseCharges.Name = "lblClearingHouseCharges";
            this.lblClearingHouseCharges.Size = new System.Drawing.Size(91, 14);
            this.lblClearingHouseCharges.TabIndex = 26;
            this.lblClearingHouseCharges.Text = "ClearingHouse :";
            // 
            // cmbInsuranceCompany
            // 
            this.cmbInsuranceCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsuranceCompany.FormattingEnabled = true;
            this.cmbInsuranceCompany.Location = new System.Drawing.Point(190, 8);
            this.cmbInsuranceCompany.Name = "cmbInsuranceCompany";
            this.cmbInsuranceCompany.Size = new System.Drawing.Size(167, 22);
            this.cmbInsuranceCompany.TabIndex = 11;
            this.cmbInsuranceCompany.MouseEnter += new System.EventHandler(this.cmbInsuranceCompany_MouseEnter);
            // 
            // cmbClearingHouse
            // 
            this.cmbClearingHouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClearingHouse.FormattingEnabled = true;
            this.cmbClearingHouse.Location = new System.Drawing.Point(190, 65);
            this.cmbClearingHouse.Name = "cmbClearingHouse";
            this.cmbClearingHouse.Size = new System.Drawing.Size(167, 22);
            this.cmbClearingHouse.TabIndex = 27;
            this.cmbClearingHouse.MouseEnter += new System.EventHandler(this.cmbClearingHouse_MouseEnter);
            // 
            // cmbMultiChargesTray
            // 
            this.cmbMultiChargesTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMultiChargesTray.FormattingEnabled = true;
            this.cmbMultiChargesTray.Location = new System.Drawing.Point(408, 8);
            this.cmbMultiChargesTray.Name = "cmbMultiChargesTray";
            this.cmbMultiChargesTray.Size = new System.Drawing.Size(120, 22);
            this.cmbMultiChargesTray.TabIndex = 12;
            this.cmbMultiChargesTray.MouseEnter += new System.EventHandler(this.cmbMultiChargesTray_MouseEnter);
            // 
            // btnBrowseUser
            // 
            this.btnBrowseUser.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseUser.BackgroundImage")));
            this.btnBrowseUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseUser.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseUser.Image")));
            this.btnBrowseUser.Location = new System.Drawing.Point(531, 37);
            this.btnBrowseUser.Name = "btnBrowseUser";
            this.btnBrowseUser.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseUser.TabIndex = 22;
            this.btnBrowseUser.UseVisualStyleBackColor = false;
            this.btnBrowseUser.Click += new System.EventHandler(this.btnBrowseUser_Click);
            this.btnBrowseUser.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseUser.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Location = new System.Drawing.Point(366, 12);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(39, 14);
            this.label50.TabIndex = 9;
            this.label50.Text = "Tray :";
            // 
            // chkSelloggedUser
            // 
            this.chkSelloggedUser.AutoSize = true;
            this.chkSelloggedUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkSelloggedUser.Location = new System.Drawing.Point(408, 67);
            this.chkSelloggedUser.Name = "chkSelloggedUser";
            this.chkSelloggedUser.Size = new System.Drawing.Size(146, 18);
            this.chkSelloggedUser.TabIndex = 24;
            this.chkSelloggedUser.Text = "Select Logged in User";
            this.chkSelloggedUser.UseVisualStyleBackColor = true;
            this.chkSelloggedUser.CheckedChanged += new System.EventHandler(this.chkSelloggedUser_CheckedChanged);
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Location = new System.Drawing.Point(366, 41);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(39, 14);
            this.label54.TabIndex = 22;
            this.label54.Text = "User :";
            // 
            // btnClearUser
            // 
            this.btnClearUser.BackColor = System.Drawing.Color.Transparent;
            this.btnClearUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearUser.BackgroundImage")));
            this.btnClearUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearUser.Image = ((System.Drawing.Image)(resources.GetObject("btnClearUser.Image")));
            this.btnClearUser.Location = new System.Drawing.Point(557, 37);
            this.btnClearUser.Name = "btnClearUser";
            this.btnClearUser.Size = new System.Drawing.Size(22, 22);
            this.btnClearUser.TabIndex = 23;
            this.btnClearUser.UseVisualStyleBackColor = false;
            this.btnClearUser.Click += new System.EventHandler(this.btnClearUser_Click);
            this.btnClearUser.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearUser.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(408, 37);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(120, 22);
            this.cmbUser.TabIndex = 21;
            this.cmbUser.MouseEnter += new System.EventHandler(this.cmbUser_MouseEnter);
            // 
            // btnBrowseMultiChargesTray
            // 
            this.btnBrowseMultiChargesTray.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseMultiChargesTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiChargesTray.BackgroundImage")));
            this.btnBrowseMultiChargesTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseMultiChargesTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseMultiChargesTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseMultiChargesTray.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiChargesTray.Image")));
            this.btnBrowseMultiChargesTray.Location = new System.Drawing.Point(531, 8);
            this.btnBrowseMultiChargesTray.Name = "btnBrowseMultiChargesTray";
            this.btnBrowseMultiChargesTray.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseMultiChargesTray.TabIndex = 13;
            this.btnBrowseMultiChargesTray.UseVisualStyleBackColor = false;
            this.btnBrowseMultiChargesTray.Click += new System.EventHandler(this.btnBrowseMultiChargesTray_Click);
            this.btnBrowseMultiChargesTray.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseMultiChargesTray.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearMultiChargesTray
            // 
            this.btnClearMultiChargesTray.BackColor = System.Drawing.Color.Transparent;
            this.btnClearMultiChargesTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearMultiChargesTray.BackgroundImage")));
            this.btnClearMultiChargesTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearMultiChargesTray.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearMultiChargesTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearMultiChargesTray.Image = ((System.Drawing.Image)(resources.GetObject("btnClearMultiChargesTray.Image")));
            this.btnClearMultiChargesTray.Location = new System.Drawing.Point(557, 8);
            this.btnClearMultiChargesTray.Name = "btnClearMultiChargesTray";
            this.btnClearMultiChargesTray.Size = new System.Drawing.Size(22, 22);
            this.btnClearMultiChargesTray.TabIndex = 14;
            this.btnClearMultiChargesTray.UseVisualStyleBackColor = false;
            this.btnClearMultiChargesTray.Click += new System.EventHandler(this.btnClearMultiChargesTray_Click);
            this.btnClearMultiChargesTray.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearMultiChargesTray.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel11);
            this.panel8.Controls.Add(this.pnlBusinessCenter);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(303, 4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(365, 89);
            this.panel8.TabIndex = 2024;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.cmbProvider);
            this.panel11.Controls.Add(this.label51);
            this.panel11.Controls.Add(this.cmbMultiFacility);
            this.panel11.Controls.Add(this.label57);
            this.panel11.Controls.Add(this.btnClearMultiFacility);
            this.panel11.Controls.Add(this.btnBrowseMultiFacility);
            this.panel11.Controls.Add(this.button2);
            this.panel11.Controls.Add(this.btnBrowseMultiProvider);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 31);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(365, 58);
            this.panel11.TabIndex = 2022;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(118, 35);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(177, 22);
            this.cmbProvider.TabIndex = 17;
            this.cmbProvider.MouseEnter += new System.EventHandler(this.cmbProvider_MouseEnter);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Location = new System.Drawing.Point(64, 10);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(50, 14);
            this.label51.TabIndex = 3;
            this.label51.Text = "Facility :";
            // 
            // cmbMultiFacility
            // 
            this.cmbMultiFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMultiFacility.FormattingEnabled = true;
            this.cmbMultiFacility.Location = new System.Drawing.Point(118, 6);
            this.cmbMultiFacility.Name = "cmbMultiFacility";
            this.cmbMultiFacility.Size = new System.Drawing.Size(177, 22);
            this.cmbMultiFacility.TabIndex = 8;
            this.cmbMultiFacility.MouseEnter += new System.EventHandler(this.cmbMultiFacility_MouseEnter);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Location = new System.Drawing.Point(55, 39);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(59, 14);
            this.label57.TabIndex = 16;
            this.label57.Text = "Provider :";
            // 
            // btnClearMultiFacility
            // 
            this.btnClearMultiFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnClearMultiFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearMultiFacility.BackgroundImage")));
            this.btnClearMultiFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearMultiFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearMultiFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearMultiFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnClearMultiFacility.Image")));
            this.btnClearMultiFacility.Location = new System.Drawing.Point(325, 6);
            this.btnClearMultiFacility.Name = "btnClearMultiFacility";
            this.btnClearMultiFacility.Size = new System.Drawing.Size(22, 22);
            this.btnClearMultiFacility.TabIndex = 10;
            this.btnClearMultiFacility.UseVisualStyleBackColor = false;
            this.btnClearMultiFacility.Click += new System.EventHandler(this.btnClearMultiFacility_Click);
            this.btnClearMultiFacility.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearMultiFacility.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseMultiFacility
            // 
            this.btnBrowseMultiFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseMultiFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiFacility.BackgroundImage")));
            this.btnBrowseMultiFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseMultiFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseMultiFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseMultiFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiFacility.Image")));
            this.btnBrowseMultiFacility.Location = new System.Drawing.Point(299, 6);
            this.btnBrowseMultiFacility.Name = "btnBrowseMultiFacility";
            this.btnBrowseMultiFacility.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseMultiFacility.TabIndex = 9;
            this.btnBrowseMultiFacility.UseVisualStyleBackColor = false;
            this.btnBrowseMultiFacility.Click += new System.EventHandler(this.btnBrowseMultiFacility_Click);
            this.btnBrowseMultiFacility.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseMultiFacility.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(325, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(22, 22);
            this.button2.TabIndex = 19;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.button2.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseMultiProvider
            // 
            this.btnBrowseMultiProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseMultiProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiProvider.BackgroundImage")));
            this.btnBrowseMultiProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseMultiProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseMultiProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseMultiProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiProvider.Image")));
            this.btnBrowseMultiProvider.Location = new System.Drawing.Point(299, 35);
            this.btnBrowseMultiProvider.Name = "btnBrowseMultiProvider";
            this.btnBrowseMultiProvider.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseMultiProvider.TabIndex = 18;
            this.btnBrowseMultiProvider.UseVisualStyleBackColor = false;
            this.btnBrowseMultiProvider.Click += new System.EventHandler(this.btnBrowseMultiProvider_Click);
            this.btnBrowseMultiProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseMultiProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlBusinessCenter
            // 
            this.pnlBusinessCenter.Controls.Add(this.cmbBusinessCenter);
            this.pnlBusinessCenter.Controls.Add(this.label64);
            this.pnlBusinessCenter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusinessCenter.Location = new System.Drawing.Point(0, 0);
            this.pnlBusinessCenter.Name = "pnlBusinessCenter";
            this.pnlBusinessCenter.Size = new System.Drawing.Size(365, 31);
            this.pnlBusinessCenter.TabIndex = 2021;
            // 
            // cmbBusinessCenter
            // 
            this.cmbBusinessCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbBusinessCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBusinessCenter.ForeColor = System.Drawing.Color.Black;
            this.cmbBusinessCenter.FormattingEnabled = true;
            this.cmbBusinessCenter.Items.AddRange(new object[] {
            ""});
            this.cmbBusinessCenter.Location = new System.Drawing.Point(118, 8);
            this.cmbBusinessCenter.Name = "cmbBusinessCenter";
            this.cmbBusinessCenter.Size = new System.Drawing.Size(229, 22);
            this.cmbBusinessCenter.TabIndex = 2019;
            this.cmbBusinessCenter.SelectedIndexChanged += new System.EventHandler(this.cmbBusinessCenter_SelectedIndexChanged);
            this.cmbBusinessCenter.MouseEnter += new System.EventHandler(this.cmbBusinessCenter_MouseEnter);
            // 
            // label64
            // 
            this.label64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label64.AutoSize = true;
            this.label64.BackColor = System.Drawing.Color.Transparent;
            this.label64.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label64.Location = new System.Drawing.Point(13, 12);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(101, 14);
            this.label64.TabIndex = 2018;
            this.label64.Text = "Business Center :";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.cmbBillingMethod);
            this.panel7.Controls.Add(this.maskedCloseDate);
            this.panel7.Controls.Add(this.label56);
            this.panel7.Controls.Add(this.chkLstCloseDate);
            this.panel7.Controls.Add(this.label55);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(1, 4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(302, 89);
            this.panel7.TabIndex = 2025;
            // 
            // cmbBillingMethod
            // 
            this.cmbBillingMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBillingMethod.FormattingEnabled = true;
            this.cmbBillingMethod.Location = new System.Drawing.Point(133, 9);
            this.cmbBillingMethod.Name = "cmbBillingMethod";
            this.cmbBillingMethod.Size = new System.Drawing.Size(163, 22);
            this.cmbBillingMethod.TabIndex = 7;
            this.cmbBillingMethod.MouseEnter += new System.EventHandler(this.cmbBillingMethod_MouseEnter_1);
            // 
            // maskedCloseDate
            // 
            this.maskedCloseDate.Location = new System.Drawing.Point(133, 36);
            this.maskedCloseDate.Mask = "00/00/0000";
            this.maskedCloseDate.Name = "maskedCloseDate";
            this.maskedCloseDate.Size = new System.Drawing.Size(108, 22);
            this.maskedCloseDate.TabIndex = 15;
            this.maskedCloseDate.ValidatingType = typeof(System.DateTime);
            this.maskedCloseDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.maskedCloseDate_MouseClick);
            this.maskedCloseDate.TextChanged += new System.EventHandler(this.maskedCloseDate_TextChanged);
            this.maskedCloseDate.Validating += new System.ComponentModel.CancelEventHandler(this.maskedCloseDate_Validating);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Location = new System.Drawing.Point(32, 38);
            this.label56.Name = "label56";
            this.label56.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label56.Size = new System.Drawing.Size(98, 18);
            this.label56.TabIndex = 13;
            this.label56.Text = "End Close Date :";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkLstCloseDate
            // 
            this.chkLstCloseDate.AutoSize = true;
            this.chkLstCloseDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkLstCloseDate.Location = new System.Drawing.Point(133, 67);
            this.chkLstCloseDate.Name = "chkLstCloseDate";
            this.chkLstCloseDate.Size = new System.Drawing.Size(137, 18);
            this.chkLstCloseDate.TabIndex = 16;
            this.chkLstCloseDate.Text = "Last Closed Date     ";
            this.chkLstCloseDate.UseVisualStyleBackColor = true;
            this.chkLstCloseDate.CheckedChanged += new System.EventHandler(this.chkLstCloseDate_CheckedChanged);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Location = new System.Drawing.Point(14, 13);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(116, 14);
            this.label55.TabIndex = 1;
            this.label55.Text = "Plan Billing Method :";
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Left;
            this.label60.Location = new System.Drawing.Point(0, 4);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 89);
            this.label60.TabIndex = 2026;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Right;
            this.label42.Location = new System.Drawing.Point(1257, 4);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1, 89);
            this.label42.TabIndex = 2027;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label40.Location = new System.Drawing.Point(0, 93);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1258, 1);
            this.label40.TabIndex = 2028;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Top;
            this.label41.Location = new System.Drawing.Point(0, 3);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1258, 1);
            this.label41.TabIndex = 2029;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.rbSecondaryClaimsCharges);
            this.panel3.Controls.Add(this.rbPrimaryClaimsCharges);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.chkQueueGeneralSearch);
            this.panel3.Controls.Add(this.chkQueueClaimCount);
            this.panel3.Controls.Add(this.numQueueClaimCount);
            this.panel3.Controls.Add(this.txtQueueSearch);
            this.panel3.Controls.Add(this.lblQueueSearch);
            this.panel3.Controls.Add(this.btnUP);
            this.panel3.Controls.Add(this.btnDown);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1258, 24);
            this.panel3.TabIndex = 17;
            // 
            // rbSecondaryClaimsCharges
            // 
            this.rbSecondaryClaimsCharges.AutoSize = true;
            this.rbSecondaryClaimsCharges.Location = new System.Drawing.Point(389, 3);
            this.rbSecondaryClaimsCharges.Name = "rbSecondaryClaimsCharges";
            this.rbSecondaryClaimsCharges.Size = new System.Drawing.Size(118, 18);
            this.rbSecondaryClaimsCharges.TabIndex = 2;
            this.rbSecondaryClaimsCharges.Tag = "Charges";
            this.rbSecondaryClaimsCharges.Text = "Secondary Claims";
            this.rbSecondaryClaimsCharges.UseVisualStyleBackColor = true;
            this.rbSecondaryClaimsCharges.CheckedChanged += new System.EventHandler(this.rbAllFilters_CheckedChanged);
            // 
            // rbPrimaryClaimsCharges
            // 
            this.rbPrimaryClaimsCharges.AutoSize = true;
            this.rbPrimaryClaimsCharges.Checked = true;
            this.rbPrimaryClaimsCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrimaryClaimsCharges.Location = new System.Drawing.Point(273, 3);
            this.rbPrimaryClaimsCharges.Name = "rbPrimaryClaimsCharges";
            this.rbPrimaryClaimsCharges.Size = new System.Drawing.Size(113, 18);
            this.rbPrimaryClaimsCharges.TabIndex = 1;
            this.rbPrimaryClaimsCharges.TabStop = true;
            this.rbPrimaryClaimsCharges.Tag = "Charges";
            this.rbPrimaryClaimsCharges.Text = "Primary Claims";
            this.rbPrimaryClaimsCharges.UseVisualStyleBackColor = true;
            this.rbPrimaryClaimsCharges.CheckedChanged += new System.EventHandler(this.rbAllFilters_CheckedChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 22);
            this.label1.TabIndex = 2;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkQueueGeneralSearch
            // 
            this.chkQueueGeneralSearch.AutoSize = true;
            this.chkQueueGeneralSearch.Location = new System.Drawing.Point(602, 0);
            this.chkQueueGeneralSearch.Name = "chkQueueGeneralSearch";
            this.chkQueueGeneralSearch.Padding = new System.Windows.Forms.Padding(3);
            this.chkQueueGeneralSearch.Size = new System.Drawing.Size(114, 24);
            this.chkQueueGeneralSearch.TabIndex = 3;
            this.chkQueueGeneralSearch.Tag = "Queue";
            this.chkQueueGeneralSearch.Text = "General Search";
            this.chkQueueGeneralSearch.UseVisualStyleBackColor = true;
            this.chkQueueGeneralSearch.Visible = false;
            // 
            // chkQueueClaimCount
            // 
            this.chkQueueClaimCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkQueueClaimCount.AutoSize = true;
            this.chkQueueClaimCount.Location = new System.Drawing.Point(971, 4);
            this.chkQueueClaimCount.Name = "chkQueueClaimCount";
            this.chkQueueClaimCount.Size = new System.Drawing.Size(175, 18);
            this.chkQueueClaimCount.TabIndex = 4;
            this.chkQueueClaimCount.Tag = "Queue";
            this.chkQueueClaimCount.Text = "Show all for selected batch";
            this.chkQueueClaimCount.UseVisualStyleBackColor = true;
            this.chkQueueClaimCount.Visible = false;
            // 
            // numQueueClaimCount
            // 
            this.numQueueClaimCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numQueueClaimCount.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numQueueClaimCount.Location = new System.Drawing.Point(1152, 1);
            this.numQueueClaimCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numQueueClaimCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQueueClaimCount.Name = "numQueueClaimCount";
            this.numQueueClaimCount.Size = new System.Drawing.Size(56, 22);
            this.numQueueClaimCount.TabIndex = 5;
            this.numQueueClaimCount.Tag = "Queue";
            this.numQueueClaimCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numQueueClaimCount.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numQueueClaimCount.ValueChanged += new System.EventHandler(this.numClaimCount_ValueChanged);
            // 
            // txtQueueSearch
            // 
            this.txtQueueSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtQueueSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQueueSearch.ForeColor = System.Drawing.Color.Black;
            this.txtQueueSearch.Location = new System.Drawing.Point(81, 1);
            this.txtQueueSearch.Name = "txtQueueSearch";
            this.txtQueueSearch.Size = new System.Drawing.Size(182, 22);
            this.txtQueueSearch.TabIndex = 0;
            this.txtQueueSearch.Tag = "Queue";
            this.txtQueueSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblQueueSearch
            // 
            this.lblQueueSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblQueueSearch.AutoSize = true;
            this.lblQueueSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblQueueSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQueueSearch.Location = new System.Drawing.Point(22, 5);
            this.lblQueueSearch.Name = "lblQueueSearch";
            this.lblQueueSearch.Size = new System.Drawing.Size(56, 14);
            this.lblQueueSearch.TabIndex = 0;
            this.lblQueueSearch.Text = "Search :";
            this.lblQueueSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnUP
            // 
            this.btnUP.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUP.FlatAppearance.BorderSize = 0;
            this.btnUP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUP.Location = new System.Drawing.Point(1208, 1);
            this.btnUP.Name = "btnUP";
            this.btnUP.Size = new System.Drawing.Size(22, 22);
            this.btnUP.TabIndex = 63;
            this.btnUP.UseVisualStyleBackColor = true;
            // 
            // btnDown
            // 
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(1230, 1);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(22, 22);
            this.btnDown.TabIndex = 6;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(0, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 22);
            this.label8.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1252, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(5, 22);
            this.label9.TabIndex = 60;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1257, 1);
            this.label10.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(0, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1257, 1);
            this.label11.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(1257, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 24);
            this.label12.TabIndex = 22;
            // 
            // tbpg_Batch
            // 
            this.tbpg_Batch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_Batch.Controls.Add(this.panel9);
            this.tbpg_Batch.Controls.Add(this.panel6);
            this.tbpg_Batch.Controls.Add(this.splitter1);
            this.tbpg_Batch.Controls.Add(this.pnlBase);
            this.tbpg_Batch.Controls.Add(this.panel4);
            this.tbpg_Batch.Controls.Add(this.pnlProgressBar);
            this.tbpg_Batch.ImageIndex = 13;
            this.tbpg_Batch.Location = new System.Drawing.Point(4, 23);
            this.tbpg_Batch.Name = "tbpg_Batch";
            this.tbpg_Batch.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_Batch.Size = new System.Drawing.Size(1264, 909);
            this.tbpg_Batch.TabIndex = 1;
            this.tbpg_Batch.Tag = "Batch";
            this.tbpg_Batch.Text = "Batch-Unsent";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.c1BatchGrid);
            this.panel9.Controls.Add(this.panel14);
            this.panel9.Controls.Add(this.label44);
            this.panel9.Controls.Add(this.label45);
            this.panel9.Controls.Add(this.label46);
            this.panel9.Controls.Add(this.label47);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(220, 27);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel9.Size = new System.Drawing.Size(1041, 854);
            this.panel9.TabIndex = 19;
            // 
            // c1BatchGrid
            // 
            this.c1BatchGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1BatchGrid.BackColor = System.Drawing.Color.White;
            this.c1BatchGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1BatchGrid.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1BatchGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1BatchGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1BatchGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1BatchGrid.Location = new System.Drawing.Point(1, 28);
            this.c1BatchGrid.Name = "c1BatchGrid";
            this.c1BatchGrid.Padding = new System.Windows.Forms.Padding(3);
            this.c1BatchGrid.Rows.Count = 1;
            this.c1BatchGrid.Rows.DefaultSize = 19;
            this.c1BatchGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1BatchGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1BatchGrid.Size = new System.Drawing.Size(1039, 825);
            this.c1BatchGrid.StyleInfo = resources.GetString("c1BatchGrid.StyleInfo");
            this.c1BatchGrid.TabIndex = 27;
            this.c1BatchGrid.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1BatchGrid_BeforeSort);
            this.c1BatchGrid.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1BatchGrid_AfterSort);
            this.c1BatchGrid.RowColChange += new System.EventHandler(this.c1BatchGrid_RowColChange);
            this.c1BatchGrid.SelChange += new System.EventHandler(this.c1BatchGrid_SelChange);
            this.c1BatchGrid.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1BatchGrid_CellChanged);
            this.c1BatchGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1BatchGrid_MouseDoubleClick);
            this.c1BatchGrid.MouseLeave += new System.EventHandler(this.c1BatchGrid_MouseLeave);
            this.c1BatchGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1BatchGrid_MouseMove);
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.Transparent;
            this.panel14.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel14.Controls.Add(this.panel15);
            this.panel14.Controls.Add(this.label71);
            this.panel14.Controls.Add(this.label74);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel14.Location = new System.Drawing.Point(1, 4);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(1039, 24);
            this.panel14.TabIndex = 30;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.Transparent;
            this.panel15.Controls.Add(this.txtBatchSearch);
            this.panel15.Controls.Add(this.label7);
            this.panel15.Controls.Add(this.label69);
            this.panel15.Controls.Add(this.label70);
            this.panel15.Controls.Add(this.btnClearClaimSearchUnsent);
            this.panel15.Controls.Add(this.label77);
            this.panel15.Controls.Add(this.label76);
            this.panel15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel15.ForeColor = System.Drawing.Color.Black;
            this.panel15.Location = new System.Drawing.Point(154, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(212, 23);
            this.panel15.TabIndex = 32;
            // 
            // txtBatchSearch
            // 
            this.txtBatchSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBatchSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBatchSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchSearch.ForeColor = System.Drawing.Color.Black;
            this.txtBatchSearch.Location = new System.Drawing.Point(6, 3);
            this.txtBatchSearch.Name = "txtBatchSearch";
            this.txtBatchSearch.Size = new System.Drawing.Size(184, 15);
            this.txtBatchSearch.TabIndex = 15;
            this.txtBatchSearch.Tag = "Batch";
            this.txtBatchSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(1, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(5, 15);
            this.label7.TabIndex = 42;
            // 
            // label69
            // 
            this.label69.BackColor = System.Drawing.Color.White;
            this.label69.Dock = System.Windows.Forms.DockStyle.Top;
            this.label69.Location = new System.Drawing.Point(1, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(189, 3);
            this.label69.TabIndex = 37;
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.White;
            this.label70.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label70.Location = new System.Drawing.Point(1, 18);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(189, 5);
            this.label70.TabIndex = 38;
            // 
            // btnClearClaimSearchUnsent
            // 
            this.btnClearClaimSearchUnsent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearClaimSearchUnsent.BackgroundImage")));
            this.btnClearClaimSearchUnsent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearClaimSearchUnsent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearClaimSearchUnsent.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearClaimSearchUnsent.FlatAppearance.BorderSize = 0;
            this.btnClearClaimSearchUnsent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearClaimSearchUnsent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearClaimSearchUnsent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearClaimSearchUnsent.Image = ((System.Drawing.Image)(resources.GetObject("btnClearClaimSearchUnsent.Image")));
            this.btnClearClaimSearchUnsent.Location = new System.Drawing.Point(190, 0);
            this.btnClearClaimSearchUnsent.Name = "btnClearClaimSearchUnsent";
            this.btnClearClaimSearchUnsent.Size = new System.Drawing.Size(21, 23);
            this.btnClearClaimSearchUnsent.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btnClearClaimSearchUnsent, "Clear Search");
            this.btnClearClaimSearchUnsent.UseVisualStyleBackColor = true;
            this.btnClearClaimSearchUnsent.Click += new System.EventHandler(this.ClearClaimSearch_Click);
            // 
            // label77
            // 
            this.label77.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label77.Dock = System.Windows.Forms.DockStyle.Right;
            this.label77.Location = new System.Drawing.Point(211, 0);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(1, 23);
            this.label77.TabIndex = 40;
            this.label77.Text = "label4";
            // 
            // label76
            // 
            this.label76.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label76.Dock = System.Windows.Forms.DockStyle.Left;
            this.label76.Location = new System.Drawing.Point(0, 0);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(1, 23);
            this.label76.TabIndex = 39;
            this.label76.Text = "label4";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.BackColor = System.Drawing.Color.Transparent;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(22, 5);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(131, 14);
            this.label71.TabIndex = 6;
            this.label71.Text = "Search Batch Claim :";
            this.label71.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label74
            // 
            this.label74.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label74.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label74.Location = new System.Drawing.Point(0, 23);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(1039, 1);
            this.label74.TabIndex = 20;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label44.Location = new System.Drawing.Point(1, 853);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1039, 1);
            this.label44.TabIndex = 25;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.label45.Location = new System.Drawing.Point(1, 3);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1039, 1);
            this.label45.TabIndex = 24;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Right;
            this.label46.Location = new System.Drawing.Point(1040, 3);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1, 851);
            this.label46.TabIndex = 23;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Left;
            this.label47.Location = new System.Drawing.Point(0, 3);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(1, 851);
            this.label47.TabIndex = 22;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.lblBillingmethodvalue);
            this.panel6.Controls.Add(this.lblBillingmethod);
            this.panel6.Controls.Add(this.lblClearinghouseValue);
            this.panel6.Controls.Add(this.lblClearinghouse);
            this.panel6.Controls.Add(this.lblBatchDateValue);
            this.panel6.Controls.Add(this.lblcalimamtvalue);
            this.panel6.Controls.Add(this.lblBatchDate);
            this.panel6.Controls.Add(this.lblClamamtvalue);
            this.panel6.Controls.Add(this.lblClaimcountvalue);
            this.panel6.Controls.Add(this.lblNumClaim);
            this.panel6.Controls.Add(this.label43);
            this.panel6.Controls.Add(this.label61);
            this.panel6.Controls.Add(this.label62);
            this.panel6.Controls.Add(this.label63);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(220, 881);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel6.Size = new System.Drawing.Size(1041, 25);
            this.panel6.TabIndex = 19;
            // 
            // lblBillingmethodvalue
            // 
            this.lblBillingmethodvalue.AutoSize = true;
            this.lblBillingmethodvalue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBillingmethodvalue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBillingmethodvalue.Location = new System.Drawing.Point(609, 7);
            this.lblBillingmethodvalue.Name = "lblBillingmethodvalue";
            this.lblBillingmethodvalue.Size = new System.Drawing.Size(224, 14);
            this.lblBillingmethodvalue.TabIndex = 35;
            this.lblBillingmethodvalue.Text = "Electronic Professional ANSI [5010]";
            // 
            // lblBillingmethod
            // 
            this.lblBillingmethod.AutoSize = true;
            this.lblBillingmethod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBillingmethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBillingmethod.Location = new System.Drawing.Point(548, 7);
            this.lblBillingmethod.Name = "lblBillingmethod";
            this.lblBillingmethod.Size = new System.Drawing.Size(67, 14);
            this.lblBillingmethod.TabIndex = 34;
            this.lblBillingmethod.Text = "Method : ";
            // 
            // lblClearinghouseValue
            // 
            this.lblClearinghouseValue.AutoSize = true;
            this.lblClearinghouseValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblClearinghouseValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClearinghouseValue.Location = new System.Drawing.Point(938, 7);
            this.lblClearinghouseValue.Name = "lblClearinghouseValue";
            this.lblClearinghouseValue.Size = new System.Drawing.Size(0, 14);
            this.lblClearinghouseValue.TabIndex = 33;
            // 
            // lblClearinghouse
            // 
            this.lblClearinghouse.AutoSize = true;
            this.lblClearinghouse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblClearinghouse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClearinghouse.Location = new System.Drawing.Point(841, 7);
            this.lblClearinghouse.Name = "lblClearinghouse";
            this.lblClearinghouse.Size = new System.Drawing.Size(105, 14);
            this.lblClearinghouse.TabIndex = 32;
            this.lblClearinghouse.Text = "Clearinghouse : ";
            this.lblClearinghouse.Visible = false;
            // 
            // lblBatchDateValue
            // 
            this.lblBatchDateValue.AutoSize = true;
            this.lblBatchDateValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBatchDateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBatchDateValue.Location = new System.Drawing.Point(445, 7);
            this.lblBatchDateValue.Name = "lblBatchDateValue";
            this.lblBatchDateValue.Size = new System.Drawing.Size(0, 14);
            this.lblBatchDateValue.TabIndex = 31;
            // 
            // lblcalimamtvalue
            // 
            this.lblcalimamtvalue.AutoSize = true;
            this.lblcalimamtvalue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblcalimamtvalue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblcalimamtvalue.Location = new System.Drawing.Point(259, 7);
            this.lblcalimamtvalue.Name = "lblcalimamtvalue";
            this.lblcalimamtvalue.Size = new System.Drawing.Size(0, 14);
            this.lblcalimamtvalue.TabIndex = 30;
            // 
            // lblBatchDate
            // 
            this.lblBatchDate.AutoSize = true;
            this.lblBatchDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBatchDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBatchDate.Location = new System.Drawing.Point(364, 7);
            this.lblBatchDate.Name = "lblBatchDate";
            this.lblBatchDate.Size = new System.Drawing.Size(87, 14);
            this.lblBatchDate.TabIndex = 29;
            this.lblBatchDate.Text = "Batch Date : ";
            // 
            // lblClamamtvalue
            // 
            this.lblClamamtvalue.AutoSize = true;
            this.lblClamamtvalue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblClamamtvalue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClamamtvalue.Location = new System.Drawing.Point(146, 7);
            this.lblClamamtvalue.Name = "lblClamamtvalue";
            this.lblClamamtvalue.Size = new System.Drawing.Size(120, 14);
            this.lblClamamtvalue.TabIndex = 28;
            this.lblClamamtvalue.Text = "Total Claim Amt. : ";
            // 
            // lblClaimcountvalue
            // 
            this.lblClaimcountvalue.AutoSize = true;
            this.lblClaimcountvalue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblClaimcountvalue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClaimcountvalue.Location = new System.Drawing.Point(101, 7);
            this.lblClaimcountvalue.Name = "lblClaimcountvalue";
            this.lblClaimcountvalue.Size = new System.Drawing.Size(0, 14);
            this.lblClaimcountvalue.TabIndex = 27;
            // 
            // lblNumClaim
            // 
            this.lblNumClaim.AutoSize = true;
            this.lblNumClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblNumClaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblNumClaim.Location = new System.Drawing.Point(6, 7);
            this.lblNumClaim.Name = "lblNumClaim";
            this.lblNumClaim.Size = new System.Drawing.Size(98, 14);
            this.lblNumClaim.TabIndex = 26;
            this.lblNumClaim.Text = "No. of Claims : ";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label43.Location = new System.Drawing.Point(1, 24);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1039, 1);
            this.label43.TabIndex = 25;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Top;
            this.label61.Location = new System.Drawing.Point(1, 3);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(1039, 1);
            this.label61.TabIndex = 24;
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Dock = System.Windows.Forms.DockStyle.Right;
            this.label62.Location = new System.Drawing.Point(1040, 3);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(1, 22);
            this.label62.TabIndex = 23;
            // 
            // label63
            // 
            this.label63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Dock = System.Windows.Forms.DockStyle.Left;
            this.label63.Location = new System.Drawing.Point(0, 3);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(1, 22);
            this.label63.TabIndex = 22;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(217, 27);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 879);
            this.splitter1.TabIndex = 27;
            this.splitter1.TabStop = false;
            // 
            // pnlBase
            // 
            this.pnlBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlBase.Controls.Add(this.c1trvBatch);
            this.pnlBase.Controls.Add(this.trvBatch);
            this.pnlBase.Controls.Add(this.pnlBatchSearch);
            this.pnlBase.Controls.Add(this.label13);
            this.pnlBase.Controls.Add(this.lbl_pnlBaseBottomBrd);
            this.pnlBase.Controls.Add(this.lbl_pnlBaseLeftBrd);
            this.pnlBase.Controls.Add(this.lbl_pnlBaseRightBrd);
            this.pnlBase.Controls.Add(this.lbl_pnlBaseTopBrd);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBase.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlBase.Location = new System.Drawing.Point(3, 27);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlBase.Size = new System.Drawing.Size(214, 879);
            this.pnlBase.TabIndex = 26;
            // 
            // c1trvBatch
            // 
            this.c1trvBatch.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1trvBatch.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1trvBatch.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1trvBatch.BackColor = System.Drawing.Color.White;
            this.c1trvBatch.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1trvBatch.ColumnInfo = "1,0,0,0,0,95,Columns:0{Width:33;Name:\"COL_IMAGE\";Style:\"TextAlign:CenterCenter;\";" +
    "StyleFixed:\"ImageAlign:LeftCenter;\";}\t";
            this.c1trvBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1trvBatch.ExtendLastCol = true;
            this.c1trvBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1trvBatch.ForeColor = System.Drawing.Color.Black;
            this.c1trvBatch.Location = new System.Drawing.Point(1, 31);
            this.c1trvBatch.Name = "c1trvBatch";
            this.c1trvBatch.Rows.Count = 1;
            this.c1trvBatch.Rows.DefaultSize = 19;
            this.c1trvBatch.Rows.Fixed = 0;
            this.c1trvBatch.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1trvBatch.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1trvBatch.Size = new System.Drawing.Size(212, 476);
            this.c1trvBatch.StyleInfo = resources.GetString("c1trvBatch.StyleInfo");
            this.c1trvBatch.TabIndex = 35;
            this.c1trvBatch.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            this.c1trvBatch.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Batch_AfterSelChange);
            // 
            // trvBatch
            // 
            this.trvBatch.BackColor = System.Drawing.Color.White;
            this.trvBatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvBatch.ContextMenuStrip = this.cntmnuShow997;
            this.trvBatch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trvBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvBatch.ForeColor = System.Drawing.Color.Black;
            this.trvBatch.HideSelection = false;
            this.trvBatch.ImageIndex = 0;
            this.trvBatch.ImageList = this.imgLst;
            this.trvBatch.Indent = 20;
            this.trvBatch.ItemHeight = 20;
            this.trvBatch.Location = new System.Drawing.Point(1, 507);
            this.trvBatch.Name = "trvBatch";
            this.trvBatch.SelectedImageIndex = 0;
            this.trvBatch.ShowLines = false;
            this.trvBatch.ShowNodeToolTips = true;
            this.trvBatch.ShowRootLines = false;
            this.trvBatch.Size = new System.Drawing.Size(212, 371);
            this.trvBatch.TabIndex = 0;
            this.trvBatch.Tag = "Batch";
            this.trvBatch.Visible = false;
            this.trvBatch.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvBatch_AfterSelect);
            this.trvBatch.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvBatch_NodeMouseClick);
            this.trvBatch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvBatch_MouseDown);
            // 
            // pnlBatchSearch
            // 
            this.pnlBatchSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlBatchSearch.Controls.Add(this.txtSearchUnsentBatches);
            this.pnlBatchSearch.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnlBatchSearch.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnlBatchSearch.Controls.Add(this.btnBatchUnsentSearchClear);
            this.pnlBatchSearch.Controls.Add(this.PicBx_Search);
            this.pnlBatchSearch.Controls.Add(this.lbl_pnlSearchBottomBrd);
            this.pnlBatchSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBatchSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBatchSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlBatchSearch.Location = new System.Drawing.Point(1, 7);
            this.pnlBatchSearch.Name = "pnlBatchSearch";
            this.pnlBatchSearch.Size = new System.Drawing.Size(212, 24);
            this.pnlBatchSearch.TabIndex = 32;
            // 
            // txtSearchUnsentBatches
            // 
            this.txtSearchUnsentBatches.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchUnsentBatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchUnsentBatches.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchUnsentBatches.ForeColor = System.Drawing.Color.Black;
            this.txtSearchUnsentBatches.Location = new System.Drawing.Point(28, 3);
            this.txtSearchUnsentBatches.Name = "txtSearchUnsentBatches";
            this.txtSearchUnsentBatches.Size = new System.Drawing.Size(163, 15);
            this.txtSearchUnsentBatches.TabIndex = 29;
            this.txtSearchUnsentBatches.Tag = "Batch";
            this.txtSearchUnsentBatches.TextChanged += new System.EventHandler(this.txtSearchBatches_TextChanged);
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(28, 0);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(163, 3);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(28, 18);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(163, 5);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // btnBatchUnsentSearchClear
            // 
            this.btnBatchUnsentSearchClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBatchUnsentSearchClear.BackgroundImage")));
            this.btnBatchUnsentSearchClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBatchUnsentSearchClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBatchUnsentSearchClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBatchUnsentSearchClear.FlatAppearance.BorderSize = 0;
            this.btnBatchUnsentSearchClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBatchUnsentSearchClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBatchUnsentSearchClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatchUnsentSearchClear.Image = ((System.Drawing.Image)(resources.GetObject("btnBatchUnsentSearchClear.Image")));
            this.btnBatchUnsentSearchClear.Location = new System.Drawing.Point(191, 0);
            this.btnBatchUnsentSearchClear.Name = "btnBatchUnsentSearchClear";
            this.btnBatchUnsentSearchClear.Size = new System.Drawing.Size(21, 23);
            this.btnBatchUnsentSearchClear.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btnBatchUnsentSearchClear, "Clear Search");
            this.btnBatchUnsentSearchClear.UseVisualStyleBackColor = true;
            this.btnBatchUnsentSearchClear.Click += new System.EventHandler(this.BatchSearchClear_Click);
            // 
            // PicBx_Search
            // 
            this.PicBx_Search.BackColor = System.Drawing.Color.White;
            this.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBx_Search.Image = ((System.Drawing.Image)(resources.GetObject("PicBx_Search.Image")));
            this.PicBx_Search.Location = new System.Drawing.Point(0, 0);
            this.PicBx_Search.Name = "PicBx_Search";
            this.PicBx_Search.Size = new System.Drawing.Size(28, 23);
            this.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBx_Search.TabIndex = 9;
            this.PicBx_Search.TabStop = false;
            // 
            // lbl_pnlSearchBottomBrd
            // 
            this.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchBottomBrd.Location = new System.Drawing.Point(0, 23);
            this.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd";
            this.lbl_pnlSearchBottomBrd.Size = new System.Drawing.Size(212, 1);
            this.lbl_pnlSearchBottomBrd.TabIndex = 35;
            this.lbl_pnlSearchBottomBrd.Text = "label1";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(212, 3);
            this.label13.TabIndex = 6;
            // 
            // lbl_pnlBaseBottomBrd
            // 
            this.lbl_pnlBaseBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlBaseBottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlBaseBottomBrd.Location = new System.Drawing.Point(1, 878);
            this.lbl_pnlBaseBottomBrd.Name = "lbl_pnlBaseBottomBrd";
            this.lbl_pnlBaseBottomBrd.Size = new System.Drawing.Size(212, 1);
            this.lbl_pnlBaseBottomBrd.TabIndex = 4;
            this.lbl_pnlBaseBottomBrd.Text = "label2";
            // 
            // lbl_pnlBaseLeftBrd
            // 
            this.lbl_pnlBaseLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlBaseLeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlBaseLeftBrd.Location = new System.Drawing.Point(0, 4);
            this.lbl_pnlBaseLeftBrd.Name = "lbl_pnlBaseLeftBrd";
            this.lbl_pnlBaseLeftBrd.Size = new System.Drawing.Size(1, 875);
            this.lbl_pnlBaseLeftBrd.TabIndex = 3;
            this.lbl_pnlBaseLeftBrd.Text = "label4";
            // 
            // lbl_pnlBaseRightBrd
            // 
            this.lbl_pnlBaseRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlBaseRightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlBaseRightBrd.Location = new System.Drawing.Point(213, 4);
            this.lbl_pnlBaseRightBrd.Name = "lbl_pnlBaseRightBrd";
            this.lbl_pnlBaseRightBrd.Size = new System.Drawing.Size(1, 875);
            this.lbl_pnlBaseRightBrd.TabIndex = 2;
            this.lbl_pnlBaseRightBrd.Text = "label3";
            // 
            // lbl_pnlBaseTopBrd
            // 
            this.lbl_pnlBaseTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlBaseTopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlBaseTopBrd.Location = new System.Drawing.Point(0, 3);
            this.lbl_pnlBaseTopBrd.Name = "lbl_pnlBaseTopBrd";
            this.lbl_pnlBaseTopBrd.Size = new System.Drawing.Size(214, 1);
            this.lbl_pnlBaseTopBrd.TabIndex = 0;
            this.lbl_pnlBaseTopBrd.Text = "label1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.rbSecondayClaimsBatch);
            this.panel4.Controls.Add(this.rbPrimaryClaimsBatch);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.chkBatchClaimCount);
            this.panel4.Controls.Add(this.numBatchClaimCount);
            this.panel4.Controls.Add(this.chkBatchGeneralSearch);
            this.panel4.Controls.Add(this.lblSearhBatch);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1258, 24);
            this.panel4.TabIndex = 17;
            // 
            // rbSecondayClaimsBatch
            // 
            this.rbSecondayClaimsBatch.AutoSize = true;
            this.rbSecondayClaimsBatch.Location = new System.Drawing.Point(208, 3);
            this.rbSecondayClaimsBatch.Name = "rbSecondayClaimsBatch";
            this.rbSecondayClaimsBatch.Size = new System.Drawing.Size(118, 18);
            this.rbSecondayClaimsBatch.TabIndex = 73;
            this.rbSecondayClaimsBatch.Tag = "Batch";
            this.rbSecondayClaimsBatch.Text = "Secondary Claims";
            this.rbSecondayClaimsBatch.UseVisualStyleBackColor = true;
            this.rbSecondayClaimsBatch.CheckedChanged += new System.EventHandler(this.rbAllFilters_CheckedChanged);
            // 
            // rbPrimaryClaimsBatch
            // 
            this.rbPrimaryClaimsBatch.AutoSize = true;
            this.rbPrimaryClaimsBatch.Checked = true;
            this.rbPrimaryClaimsBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrimaryClaimsBatch.Location = new System.Drawing.Point(92, 3);
            this.rbPrimaryClaimsBatch.Name = "rbPrimaryClaimsBatch";
            this.rbPrimaryClaimsBatch.Size = new System.Drawing.Size(113, 18);
            this.rbPrimaryClaimsBatch.TabIndex = 72;
            this.rbPrimaryClaimsBatch.TabStop = true;
            this.rbPrimaryClaimsBatch.Tag = "Batch";
            this.rbPrimaryClaimsBatch.Text = "Primary Claims";
            this.rbPrimaryClaimsBatch.UseVisualStyleBackColor = true;
            this.rbPrimaryClaimsBatch.CheckedChanged += new System.EventHandler(this.rbAllFilters_CheckedChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(91, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 22);
            this.label2.TabIndex = 71;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkBatchClaimCount
            // 
            this.chkBatchClaimCount.AutoSize = true;
            this.chkBatchClaimCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkBatchClaimCount.Location = new System.Drawing.Point(1019, 1);
            this.chkBatchClaimCount.Name = "chkBatchClaimCount";
            this.chkBatchClaimCount.Size = new System.Drawing.Size(175, 22);
            this.chkBatchClaimCount.TabIndex = 66;
            this.chkBatchClaimCount.Tag = "Batch";
            this.chkBatchClaimCount.Text = "Show all for selected batch";
            this.chkBatchClaimCount.UseVisualStyleBackColor = true;
            this.chkBatchClaimCount.Visible = false;
            // 
            // numBatchClaimCount
            // 
            this.numBatchClaimCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.numBatchClaimCount.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBatchClaimCount.Location = new System.Drawing.Point(1194, 1);
            this.numBatchClaimCount.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBatchClaimCount.Name = "numBatchClaimCount";
            this.numBatchClaimCount.Size = new System.Drawing.Size(53, 22);
            this.numBatchClaimCount.TabIndex = 65;
            this.numBatchClaimCount.Tag = "Batch";
            this.numBatchClaimCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numBatchClaimCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBatchClaimCount.Visible = false;
            // 
            // chkBatchGeneralSearch
            // 
            this.chkBatchGeneralSearch.Location = new System.Drawing.Point(344, 2);
            this.chkBatchGeneralSearch.Name = "chkBatchGeneralSearch";
            this.chkBatchGeneralSearch.Padding = new System.Windows.Forms.Padding(3);
            this.chkBatchGeneralSearch.Size = new System.Drawing.Size(114, 19);
            this.chkBatchGeneralSearch.TabIndex = 63;
            this.chkBatchGeneralSearch.Tag = "Batch";
            this.chkBatchGeneralSearch.Text = "General Search";
            this.chkBatchGeneralSearch.UseVisualStyleBackColor = true;
            this.chkBatchGeneralSearch.Visible = false;
            // 
            // lblSearhBatch
            // 
            this.lblSearhBatch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearhBatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearhBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearhBatch.Location = new System.Drawing.Point(1, 1);
            this.lblSearhBatch.Name = "lblSearhBatch";
            this.lblSearhBatch.Size = new System.Drawing.Size(90, 22);
            this.lblSearhBatch.TabIndex = 6;
            this.lblSearhBatch.Text = "Batch Type :";
            this.lblSearhBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(0, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 22);
            this.label16.TabIndex = 21;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1247, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(10, 22);
            this.label17.TabIndex = 60;
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1257, 1);
            this.label18.TabIndex = 19;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(0, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1257, 1);
            this.label19.TabIndex = 20;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(1257, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 24);
            this.label20.TabIndex = 22;
            // 
            // pnlProgressBar
            // 
            this.pnlProgressBar.Controls.Add(this.lblFileCounter);
            this.pnlProgressBar.Controls.Add(this.lblFile);
            this.pnlProgressBar.Controls.Add(this.prgFileGeneration);
            this.pnlProgressBar.Controls.Add(this.label21);
            this.pnlProgressBar.Location = new System.Drawing.Point(353, 46);
            this.pnlProgressBar.Name = "pnlProgressBar";
            this.pnlProgressBar.Size = new System.Drawing.Size(952, 19);
            this.pnlProgressBar.TabIndex = 28;
            this.pnlProgressBar.Visible = false;
            // 
            // lblFileCounter
            // 
            this.lblFileCounter.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblFileCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileCounter.Location = new System.Drawing.Point(555, 10);
            this.lblFileCounter.Name = "lblFileCounter";
            this.lblFileCounter.Size = new System.Drawing.Size(121, 9);
            this.lblFileCounter.TabIndex = 24;
            this.lblFileCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFile
            // 
            this.lblFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFile.Location = new System.Drawing.Point(0, 10);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(286, 9);
            this.lblFile.TabIndex = 1;
            this.lblFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // prgFileGeneration
            // 
            this.prgFileGeneration.Dock = System.Windows.Forms.DockStyle.Right;
            this.prgFileGeneration.Location = new System.Drawing.Point(676, 10);
            this.prgFileGeneration.Name = "prgFileGeneration";
            this.prgFileGeneration.Size = new System.Drawing.Size(276, 9);
            this.prgFileGeneration.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgFileGeneration.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Padding = new System.Windows.Forms.Padding(3);
            this.label21.Size = new System.Drawing.Size(952, 10);
            this.label21.TabIndex = 23;
            // 
            // tbpg_SentBatch
            // 
            this.tbpg_SentBatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_SentBatch.Controls.Add(this.panel9SentBatch);
            this.tbpg_SentBatch.Controls.Add(this.panel6SentBatch);
            this.tbpg_SentBatch.Controls.Add(this.splitter4);
            this.tbpg_SentBatch.Controls.Add(this.pnlBase_SentBatch);
            this.tbpg_SentBatch.Controls.Add(this.panel14_SentBatch);
            this.tbpg_SentBatch.Controls.Add(this.pnlProgressBar_SentBatch);
            this.tbpg_SentBatch.ImageIndex = 14;
            this.tbpg_SentBatch.Location = new System.Drawing.Point(4, 23);
            this.tbpg_SentBatch.Name = "tbpg_SentBatch";
            this.tbpg_SentBatch.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_SentBatch.Size = new System.Drawing.Size(1264, 909);
            this.tbpg_SentBatch.TabIndex = 8;
            this.tbpg_SentBatch.Tag = "SentBatch";
            this.tbpg_SentBatch.Text = "Batch-Sent";
            // 
            // panel9SentBatch
            // 
            this.panel9SentBatch.Controls.Add(this.c1BatchGridSentBatch);
            this.panel9SentBatch.Controls.Add(this.panel13);
            this.panel9SentBatch.Controls.Add(this.label65);
            this.panel9SentBatch.Controls.Add(this.label66);
            this.panel9SentBatch.Controls.Add(this.label67);
            this.panel9SentBatch.Controls.Add(this.label68);
            this.panel9SentBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9SentBatch.Location = new System.Drawing.Point(220, 27);
            this.panel9SentBatch.Name = "panel9SentBatch";
            this.panel9SentBatch.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel9SentBatch.Size = new System.Drawing.Size(1041, 854);
            this.panel9SentBatch.TabIndex = 19;
            // 
            // c1BatchGridSentBatch
            // 
            this.c1BatchGridSentBatch.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1BatchGridSentBatch.BackColor = System.Drawing.Color.White;
            this.c1BatchGridSentBatch.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1BatchGridSentBatch.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1BatchGridSentBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1BatchGridSentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1BatchGridSentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1BatchGridSentBatch.Location = new System.Drawing.Point(1, 28);
            this.c1BatchGridSentBatch.Name = "c1BatchGridSentBatch";
            this.c1BatchGridSentBatch.Padding = new System.Windows.Forms.Padding(3);
            this.c1BatchGridSentBatch.Rows.Count = 1;
            this.c1BatchGridSentBatch.Rows.DefaultSize = 19;
            this.c1BatchGridSentBatch.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1BatchGridSentBatch.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1BatchGridSentBatch.Size = new System.Drawing.Size(1039, 825);
            this.c1BatchGridSentBatch.StyleInfo = resources.GetString("c1BatchGridSentBatch.StyleInfo");
            this.c1BatchGridSentBatch.TabIndex = 27;
            this.c1BatchGridSentBatch.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1BatchGridSentBatch_BeforeSort);
            this.c1BatchGridSentBatch.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1BatchGridSentBatch_AfterSort);
            this.c1BatchGridSentBatch.RowColChange += new System.EventHandler(this.c1BatchGrid_RowColChange);
            this.c1BatchGridSentBatch.SelChange += new System.EventHandler(this.c1BatchGrid_SelChange);
            this.c1BatchGridSentBatch.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1BatchGrid_CellChanged);
            this.c1BatchGridSentBatch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1BatchGrid_MouseDoubleClick);
            this.c1BatchGridSentBatch.MouseLeave += new System.EventHandler(this.c1BatchGrid_MouseLeave);
            this.c1BatchGridSentBatch.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1BatchGrid_MouseMove);
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.Transparent;
            this.panel13.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel13.Controls.Add(this.panel18);
            this.panel13.Controls.Add(this.label104);
            this.panel13.Controls.Add(this.label106);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel13.Location = new System.Drawing.Point(1, 4);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(1039, 24);
            this.panel13.TabIndex = 30;
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.Transparent;
            this.panel18.Controls.Add(this.txtBatchSearch_SentBatch);
            this.panel18.Controls.Add(this.label72);
            this.panel18.Controls.Add(this.label73);
            this.panel18.Controls.Add(this.label86);
            this.panel18.Controls.Add(this.btnClearClaimSearchSent);
            this.panel18.Controls.Add(this.label97);
            this.panel18.Controls.Add(this.label98);
            this.panel18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel18.ForeColor = System.Drawing.Color.Black;
            this.panel18.Location = new System.Drawing.Point(154, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(212, 23);
            this.panel18.TabIndex = 32;
            // 
            // txtBatchSearch_SentBatch
            // 
            this.txtBatchSearch_SentBatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBatchSearch_SentBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBatchSearch_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchSearch_SentBatch.ForeColor = System.Drawing.Color.Black;
            this.txtBatchSearch_SentBatch.Location = new System.Drawing.Point(6, 3);
            this.txtBatchSearch_SentBatch.Name = "txtBatchSearch_SentBatch";
            this.txtBatchSearch_SentBatch.Size = new System.Drawing.Size(184, 15);
            this.txtBatchSearch_SentBatch.TabIndex = 15;
            this.txtBatchSearch_SentBatch.Tag = "SENTBATCH";
            this.txtBatchSearch_SentBatch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.White;
            this.label72.Dock = System.Windows.Forms.DockStyle.Left;
            this.label72.Location = new System.Drawing.Point(1, 3);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(5, 15);
            this.label72.TabIndex = 43;
            // 
            // label73
            // 
            this.label73.BackColor = System.Drawing.Color.White;
            this.label73.Dock = System.Windows.Forms.DockStyle.Top;
            this.label73.Location = new System.Drawing.Point(1, 0);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(189, 3);
            this.label73.TabIndex = 37;
            // 
            // label86
            // 
            this.label86.BackColor = System.Drawing.Color.White;
            this.label86.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label86.Location = new System.Drawing.Point(1, 18);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(189, 5);
            this.label86.TabIndex = 38;
            // 
            // btnClearClaimSearchSent
            // 
            this.btnClearClaimSearchSent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearClaimSearchSent.BackgroundImage")));
            this.btnClearClaimSearchSent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearClaimSearchSent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearClaimSearchSent.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearClaimSearchSent.FlatAppearance.BorderSize = 0;
            this.btnClearClaimSearchSent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearClaimSearchSent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearClaimSearchSent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearClaimSearchSent.Image = ((System.Drawing.Image)(resources.GetObject("btnClearClaimSearchSent.Image")));
            this.btnClearClaimSearchSent.Location = new System.Drawing.Point(190, 0);
            this.btnClearClaimSearchSent.Name = "btnClearClaimSearchSent";
            this.btnClearClaimSearchSent.Size = new System.Drawing.Size(21, 23);
            this.btnClearClaimSearchSent.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btnClearClaimSearchSent, "Clear Search");
            this.btnClearClaimSearchSent.UseVisualStyleBackColor = true;
            this.btnClearClaimSearchSent.Click += new System.EventHandler(this.ClearClaimSearch_Click);
            // 
            // label97
            // 
            this.label97.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label97.Dock = System.Windows.Forms.DockStyle.Right;
            this.label97.Location = new System.Drawing.Point(211, 0);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(1, 23);
            this.label97.TabIndex = 40;
            this.label97.Text = "label4";
            // 
            // label98
            // 
            this.label98.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label98.Dock = System.Windows.Forms.DockStyle.Left;
            this.label98.Location = new System.Drawing.Point(0, 0);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(1, 23);
            this.label98.TabIndex = 39;
            this.label98.Text = "label4";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.BackColor = System.Drawing.Color.Transparent;
            this.label104.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.Location = new System.Drawing.Point(22, 5);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(131, 14);
            this.label104.TabIndex = 6;
            this.label104.Text = "Search Batch Claim :";
            this.label104.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label106
            // 
            this.label106.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label106.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label106.Location = new System.Drawing.Point(0, 23);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(1039, 1);
            this.label106.TabIndex = 20;
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label65.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label65.Location = new System.Drawing.Point(1, 853);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(1039, 1);
            this.label65.TabIndex = 25;
            // 
            // label66
            // 
            this.label66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label66.Dock = System.Windows.Forms.DockStyle.Top;
            this.label66.Location = new System.Drawing.Point(1, 3);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(1039, 1);
            this.label66.TabIndex = 24;
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label67.Dock = System.Windows.Forms.DockStyle.Right;
            this.label67.Location = new System.Drawing.Point(1040, 3);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(1, 851);
            this.label67.TabIndex = 23;
            // 
            // label68
            // 
            this.label68.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label68.Dock = System.Windows.Forms.DockStyle.Left;
            this.label68.Location = new System.Drawing.Point(0, 3);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(1, 851);
            this.label68.TabIndex = 22;
            // 
            // panel6SentBatch
            // 
            this.panel6SentBatch.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel6SentBatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6SentBatch.Controls.Add(this.lblBillingmethodvalue_SentBatch);
            this.panel6SentBatch.Controls.Add(this.lblBillingmethod_SentBatch);
            this.panel6SentBatch.Controls.Add(this.lblClearinghouseValue_SentBatch);
            this.panel6SentBatch.Controls.Add(this.lblClearinghouse_SentBatch);
            this.panel6SentBatch.Controls.Add(this.lblBatchDateValue_SentBatch);
            this.panel6SentBatch.Controls.Add(this.lblcalimamtvalue_SentBatch);
            this.panel6SentBatch.Controls.Add(this.lblBatchDate_SentBatch);
            this.panel6SentBatch.Controls.Add(this.lblClamamtvalue_SentBatch);
            this.panel6SentBatch.Controls.Add(this.lblClaimcountvalue_SentBatch);
            this.panel6SentBatch.Controls.Add(this.lblNumClaim_SentBatch);
            this.panel6SentBatch.Controls.Add(this.label87);
            this.panel6SentBatch.Controls.Add(this.label88);
            this.panel6SentBatch.Controls.Add(this.label89);
            this.panel6SentBatch.Controls.Add(this.label90);
            this.panel6SentBatch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6SentBatch.Location = new System.Drawing.Point(220, 881);
            this.panel6SentBatch.Name = "panel6SentBatch";
            this.panel6SentBatch.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel6SentBatch.Size = new System.Drawing.Size(1041, 25);
            this.panel6SentBatch.TabIndex = 19;
            // 
            // lblBillingmethodvalue_SentBatch
            // 
            this.lblBillingmethodvalue_SentBatch.AutoSize = true;
            this.lblBillingmethodvalue_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBillingmethodvalue_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBillingmethodvalue_SentBatch.Location = new System.Drawing.Point(609, 7);
            this.lblBillingmethodvalue_SentBatch.Name = "lblBillingmethodvalue_SentBatch";
            this.lblBillingmethodvalue_SentBatch.Size = new System.Drawing.Size(224, 14);
            this.lblBillingmethodvalue_SentBatch.TabIndex = 35;
            this.lblBillingmethodvalue_SentBatch.Text = "Electronic Professional ANSI [5010]";
            // 
            // lblBillingmethod_SentBatch
            // 
            this.lblBillingmethod_SentBatch.AutoSize = true;
            this.lblBillingmethod_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBillingmethod_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBillingmethod_SentBatch.Location = new System.Drawing.Point(548, 7);
            this.lblBillingmethod_SentBatch.Name = "lblBillingmethod_SentBatch";
            this.lblBillingmethod_SentBatch.Size = new System.Drawing.Size(67, 14);
            this.lblBillingmethod_SentBatch.TabIndex = 39;
            this.lblBillingmethod_SentBatch.Text = "Method : ";
            // 
            // lblClearinghouseValue_SentBatch
            // 
            this.lblClearinghouseValue_SentBatch.AutoSize = true;
            this.lblClearinghouseValue_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblClearinghouseValue_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClearinghouseValue_SentBatch.Location = new System.Drawing.Point(938, 7);
            this.lblClearinghouseValue_SentBatch.Name = "lblClearinghouseValue_SentBatch";
            this.lblClearinghouseValue_SentBatch.Size = new System.Drawing.Size(0, 14);
            this.lblClearinghouseValue_SentBatch.TabIndex = 38;
            // 
            // lblClearinghouse_SentBatch
            // 
            this.lblClearinghouse_SentBatch.AutoSize = true;
            this.lblClearinghouse_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblClearinghouse_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClearinghouse_SentBatch.Location = new System.Drawing.Point(841, 7);
            this.lblClearinghouse_SentBatch.Name = "lblClearinghouse_SentBatch";
            this.lblClearinghouse_SentBatch.Size = new System.Drawing.Size(105, 14);
            this.lblClearinghouse_SentBatch.TabIndex = 37;
            this.lblClearinghouse_SentBatch.Text = "Clearinghouse : ";
            this.lblClearinghouse_SentBatch.Visible = false;
            // 
            // lblBatchDateValue_SentBatch
            // 
            this.lblBatchDateValue_SentBatch.AutoSize = true;
            this.lblBatchDateValue_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBatchDateValue_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBatchDateValue_SentBatch.Location = new System.Drawing.Point(445, 7);
            this.lblBatchDateValue_SentBatch.Name = "lblBatchDateValue_SentBatch";
            this.lblBatchDateValue_SentBatch.Size = new System.Drawing.Size(0, 14);
            this.lblBatchDateValue_SentBatch.TabIndex = 36;
            // 
            // lblcalimamtvalue_SentBatch
            // 
            this.lblcalimamtvalue_SentBatch.AutoSize = true;
            this.lblcalimamtvalue_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblcalimamtvalue_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblcalimamtvalue_SentBatch.Location = new System.Drawing.Point(259, 7);
            this.lblcalimamtvalue_SentBatch.Name = "lblcalimamtvalue_SentBatch";
            this.lblcalimamtvalue_SentBatch.Size = new System.Drawing.Size(0, 14);
            this.lblcalimamtvalue_SentBatch.TabIndex = 34;
            // 
            // lblBatchDate_SentBatch
            // 
            this.lblBatchDate_SentBatch.AutoSize = true;
            this.lblBatchDate_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBatchDate_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBatchDate_SentBatch.Location = new System.Drawing.Point(364, 7);
            this.lblBatchDate_SentBatch.Name = "lblBatchDate_SentBatch";
            this.lblBatchDate_SentBatch.Size = new System.Drawing.Size(87, 14);
            this.lblBatchDate_SentBatch.TabIndex = 33;
            this.lblBatchDate_SentBatch.Text = "Batch Date : ";
            // 
            // lblClamamtvalue_SentBatch
            // 
            this.lblClamamtvalue_SentBatch.AutoSize = true;
            this.lblClamamtvalue_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblClamamtvalue_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClamamtvalue_SentBatch.Location = new System.Drawing.Point(146, 7);
            this.lblClamamtvalue_SentBatch.Name = "lblClamamtvalue_SentBatch";
            this.lblClamamtvalue_SentBatch.Size = new System.Drawing.Size(120, 14);
            this.lblClamamtvalue_SentBatch.TabIndex = 32;
            this.lblClamamtvalue_SentBatch.Text = "Total Claim Amt. : ";
            // 
            // lblClaimcountvalue_SentBatch
            // 
            this.lblClaimcountvalue_SentBatch.AutoSize = true;
            this.lblClaimcountvalue_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblClaimcountvalue_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClaimcountvalue_SentBatch.Location = new System.Drawing.Point(101, 7);
            this.lblClaimcountvalue_SentBatch.Name = "lblClaimcountvalue_SentBatch";
            this.lblClaimcountvalue_SentBatch.Size = new System.Drawing.Size(0, 14);
            this.lblClaimcountvalue_SentBatch.TabIndex = 31;
            // 
            // lblNumClaim_SentBatch
            // 
            this.lblNumClaim_SentBatch.AutoSize = true;
            this.lblNumClaim_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblNumClaim_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblNumClaim_SentBatch.Location = new System.Drawing.Point(6, 7);
            this.lblNumClaim_SentBatch.Name = "lblNumClaim_SentBatch";
            this.lblNumClaim_SentBatch.Size = new System.Drawing.Size(98, 14);
            this.lblNumClaim_SentBatch.TabIndex = 30;
            this.lblNumClaim_SentBatch.Text = "No. of Claims : ";
            // 
            // label87
            // 
            this.label87.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label87.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label87.Location = new System.Drawing.Point(1, 24);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(1039, 1);
            this.label87.TabIndex = 25;
            // 
            // label88
            // 
            this.label88.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label88.Dock = System.Windows.Forms.DockStyle.Top;
            this.label88.Location = new System.Drawing.Point(1, 3);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(1039, 1);
            this.label88.TabIndex = 24;
            // 
            // label89
            // 
            this.label89.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label89.Dock = System.Windows.Forms.DockStyle.Right;
            this.label89.Location = new System.Drawing.Point(1040, 3);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(1, 22);
            this.label89.TabIndex = 23;
            // 
            // label90
            // 
            this.label90.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label90.Dock = System.Windows.Forms.DockStyle.Left;
            this.label90.Location = new System.Drawing.Point(0, 3);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(1, 22);
            this.label90.TabIndex = 22;
            // 
            // splitter4
            // 
            this.splitter4.Location = new System.Drawing.Point(217, 27);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(3, 879);
            this.splitter4.TabIndex = 27;
            this.splitter4.TabStop = false;
            // 
            // pnlBase_SentBatch
            // 
            this.pnlBase_SentBatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlBase_SentBatch.Controls.Add(this.c1trvBatch_SentBatch);
            this.pnlBase_SentBatch.Controls.Add(this.trvBatch_SentBatch);
            this.pnlBase_SentBatch.Controls.Add(this.panel19);
            this.pnlBase_SentBatch.Controls.Add(this.label91);
            this.pnlBase_SentBatch.Controls.Add(this.label93);
            this.pnlBase_SentBatch.Controls.Add(this.label94);
            this.pnlBase_SentBatch.Controls.Add(this.label95);
            this.pnlBase_SentBatch.Controls.Add(this.label96);
            this.pnlBase_SentBatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBase_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBase_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlBase_SentBatch.Location = new System.Drawing.Point(3, 27);
            this.pnlBase_SentBatch.Name = "pnlBase_SentBatch";
            this.pnlBase_SentBatch.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlBase_SentBatch.Size = new System.Drawing.Size(214, 879);
            this.pnlBase_SentBatch.TabIndex = 26;
            // 
            // c1trvBatch_SentBatch
            // 
            this.c1trvBatch_SentBatch.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1trvBatch_SentBatch.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1trvBatch_SentBatch.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1trvBatch_SentBatch.BackColor = System.Drawing.Color.White;
            this.c1trvBatch_SentBatch.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1trvBatch_SentBatch.ColumnInfo = "1,0,0,0,0,95,Columns:0{Width:33;Name:\"COL_IMAGE\";Style:\"TextAlign:CenterCenter;\";" +
    "StyleFixed:\"ImageAlign:LeftCenter;\";}\t";
            this.c1trvBatch_SentBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1trvBatch_SentBatch.ExtendLastCol = true;
            this.c1trvBatch_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1trvBatch_SentBatch.ForeColor = System.Drawing.Color.Black;
            this.c1trvBatch_SentBatch.Location = new System.Drawing.Point(1, 31);
            this.c1trvBatch_SentBatch.Name = "c1trvBatch_SentBatch";
            this.c1trvBatch_SentBatch.Rows.Count = 1;
            this.c1trvBatch_SentBatch.Rows.DefaultSize = 19;
            this.c1trvBatch_SentBatch.Rows.Fixed = 0;
            this.c1trvBatch_SentBatch.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1trvBatch_SentBatch.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1trvBatch_SentBatch.Size = new System.Drawing.Size(212, 352);
            this.c1trvBatch_SentBatch.StyleInfo = resources.GetString("c1trvBatch_SentBatch.StyleInfo");
            this.c1trvBatch_SentBatch.TabIndex = 34;
            this.c1trvBatch_SentBatch.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            this.c1trvBatch_SentBatch.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Batch_AfterSelChange);
            // 
            // trvBatch_SentBatch
            // 
            this.trvBatch_SentBatch.BackColor = System.Drawing.Color.White;
            this.trvBatch_SentBatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvBatch_SentBatch.ContextMenuStrip = this.cntmnuShow997;
            this.trvBatch_SentBatch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trvBatch_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvBatch_SentBatch.ForeColor = System.Drawing.Color.Black;
            this.trvBatch_SentBatch.HideSelection = false;
            this.trvBatch_SentBatch.ImageIndex = 0;
            this.trvBatch_SentBatch.ImageList = this.imgLst;
            this.trvBatch_SentBatch.Indent = 20;
            this.trvBatch_SentBatch.ItemHeight = 20;
            this.trvBatch_SentBatch.Location = new System.Drawing.Point(1, 383);
            this.trvBatch_SentBatch.Name = "trvBatch_SentBatch";
            this.trvBatch_SentBatch.SelectedImageIndex = 0;
            this.trvBatch_SentBatch.ShowLines = false;
            this.trvBatch_SentBatch.ShowNodeToolTips = true;
            this.trvBatch_SentBatch.ShowRootLines = false;
            this.trvBatch_SentBatch.Size = new System.Drawing.Size(212, 495);
            this.trvBatch_SentBatch.TabIndex = 0;
            this.trvBatch_SentBatch.Tag = "Batch";
            this.trvBatch_SentBatch.Visible = false;
            this.trvBatch_SentBatch.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvBatch_AfterSelect);
            this.trvBatch_SentBatch.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvBatch_NodeMouseClick);
            this.trvBatch_SentBatch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvBatch_MouseDown);
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.Transparent;
            this.panel19.Controls.Add(this.txtSearchSentBatches);
            this.panel19.Controls.Add(this.label108);
            this.panel19.Controls.Add(this.label109);
            this.panel19.Controls.Add(this.btnSentBatchSearchClear);
            this.panel19.Controls.Add(this.pictureBox1);
            this.panel19.Controls.Add(this.label110);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel19.ForeColor = System.Drawing.Color.Black;
            this.panel19.Location = new System.Drawing.Point(1, 7);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(212, 24);
            this.panel19.TabIndex = 33;
            // 
            // txtSearchSentBatches
            // 
            this.txtSearchSentBatches.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchSentBatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchSentBatches.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchSentBatches.ForeColor = System.Drawing.Color.Black;
            this.txtSearchSentBatches.Location = new System.Drawing.Point(28, 3);
            this.txtSearchSentBatches.Name = "txtSearchSentBatches";
            this.txtSearchSentBatches.Size = new System.Drawing.Size(163, 15);
            this.txtSearchSentBatches.TabIndex = 29;
            this.txtSearchSentBatches.Tag = "Batch";
            this.txtSearchSentBatches.TextChanged += new System.EventHandler(this.txtSearchBatches_TextChanged);
            // 
            // label108
            // 
            this.label108.BackColor = System.Drawing.Color.White;
            this.label108.Dock = System.Windows.Forms.DockStyle.Top;
            this.label108.Location = new System.Drawing.Point(28, 0);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(163, 3);
            this.label108.TabIndex = 37;
            // 
            // label109
            // 
            this.label109.BackColor = System.Drawing.Color.White;
            this.label109.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label109.Location = new System.Drawing.Point(28, 18);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(163, 5);
            this.label109.TabIndex = 38;
            // 
            // btnSentBatchSearchClear
            // 
            this.btnSentBatchSearchClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSentBatchSearchClear.BackgroundImage")));
            this.btnSentBatchSearchClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSentBatchSearchClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSentBatchSearchClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSentBatchSearchClear.FlatAppearance.BorderSize = 0;
            this.btnSentBatchSearchClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSentBatchSearchClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSentBatchSearchClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSentBatchSearchClear.Image = ((System.Drawing.Image)(resources.GetObject("btnSentBatchSearchClear.Image")));
            this.btnSentBatchSearchClear.Location = new System.Drawing.Point(191, 0);
            this.btnSentBatchSearchClear.Name = "btnSentBatchSearchClear";
            this.btnSentBatchSearchClear.Size = new System.Drawing.Size(21, 23);
            this.btnSentBatchSearchClear.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btnSentBatchSearchClear, "Clear Search");
            this.btnSentBatchSearchClear.UseVisualStyleBackColor = true;
            this.btnSentBatchSearchClear.Click += new System.EventHandler(this.BatchSearchClear_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label110
            // 
            this.label110.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label110.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label110.Location = new System.Drawing.Point(0, 23);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(212, 1);
            this.label110.TabIndex = 35;
            this.label110.Text = "label1";
            // 
            // label91
            // 
            this.label91.BackColor = System.Drawing.Color.White;
            this.label91.Dock = System.Windows.Forms.DockStyle.Top;
            this.label91.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(1, 4);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(212, 3);
            this.label91.TabIndex = 6;
            // 
            // label93
            // 
            this.label93.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label93.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label93.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label93.Location = new System.Drawing.Point(1, 878);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(212, 1);
            this.label93.TabIndex = 4;
            this.label93.Text = "label2";
            // 
            // label94
            // 
            this.label94.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label94.Dock = System.Windows.Forms.DockStyle.Left;
            this.label94.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label94.Location = new System.Drawing.Point(0, 4);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(1, 875);
            this.label94.TabIndex = 3;
            this.label94.Text = "label4";
            // 
            // label95
            // 
            this.label95.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label95.Dock = System.Windows.Forms.DockStyle.Right;
            this.label95.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label95.Location = new System.Drawing.Point(213, 4);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(1, 875);
            this.label95.TabIndex = 2;
            this.label95.Text = "label3";
            // 
            // label96
            // 
            this.label96.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label96.Dock = System.Windows.Forms.DockStyle.Top;
            this.label96.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label96.Location = new System.Drawing.Point(0, 3);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(214, 1);
            this.label96.TabIndex = 0;
            this.label96.Text = "label1";
            // 
            // panel14_SentBatch
            // 
            this.panel14_SentBatch.BackColor = System.Drawing.Color.Transparent;
            this.panel14_SentBatch.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel14_SentBatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel14_SentBatch.Controls.Add(this.rbSecondaryClaimsBatch_SentBatch);
            this.panel14_SentBatch.Controls.Add(this.rbPrimaryClaimsBatch_SentBatch);
            this.panel14_SentBatch.Controls.Add(this.label2_SentBatch);
            this.panel14_SentBatch.Controls.Add(this.chkBatchClaimCount_SentBatch);
            this.panel14_SentBatch.Controls.Add(this.numBatchClaimCount_SentBatch);
            this.panel14_SentBatch.Controls.Add(this.chkBatchGeneralSearch_SentBatch);
            this.panel14_SentBatch.Controls.Add(this.lblSearhBatch_SentBatch);
            this.panel14_SentBatch.Controls.Add(this.label99);
            this.panel14_SentBatch.Controls.Add(this.label100);
            this.panel14_SentBatch.Controls.Add(this.label101);
            this.panel14_SentBatch.Controls.Add(this.label102);
            this.panel14_SentBatch.Controls.Add(this.label103);
            this.panel14_SentBatch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel14_SentBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel14_SentBatch.Location = new System.Drawing.Point(3, 3);
            this.panel14_SentBatch.Name = "panel14_SentBatch";
            this.panel14_SentBatch.Size = new System.Drawing.Size(1258, 24);
            this.panel14_SentBatch.TabIndex = 17;
            // 
            // rbSecondaryClaimsBatch_SentBatch
            // 
            this.rbSecondaryClaimsBatch_SentBatch.AutoSize = true;
            this.rbSecondaryClaimsBatch_SentBatch.Location = new System.Drawing.Point(208, 3);
            this.rbSecondaryClaimsBatch_SentBatch.Name = "rbSecondaryClaimsBatch_SentBatch";
            this.rbSecondaryClaimsBatch_SentBatch.Size = new System.Drawing.Size(118, 18);
            this.rbSecondaryClaimsBatch_SentBatch.TabIndex = 73;
            this.rbSecondaryClaimsBatch_SentBatch.Tag = "Batch";
            this.rbSecondaryClaimsBatch_SentBatch.Text = "Secondary Claims";
            this.rbSecondaryClaimsBatch_SentBatch.UseVisualStyleBackColor = true;
            this.rbSecondaryClaimsBatch_SentBatch.CheckedChanged += new System.EventHandler(this.rbAllFilters_CheckedChanged);
            // 
            // rbPrimaryClaimsBatch_SentBatch
            // 
            this.rbPrimaryClaimsBatch_SentBatch.AutoSize = true;
            this.rbPrimaryClaimsBatch_SentBatch.Checked = true;
            this.rbPrimaryClaimsBatch_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrimaryClaimsBatch_SentBatch.Location = new System.Drawing.Point(92, 3);
            this.rbPrimaryClaimsBatch_SentBatch.Name = "rbPrimaryClaimsBatch_SentBatch";
            this.rbPrimaryClaimsBatch_SentBatch.Size = new System.Drawing.Size(113, 18);
            this.rbPrimaryClaimsBatch_SentBatch.TabIndex = 72;
            this.rbPrimaryClaimsBatch_SentBatch.TabStop = true;
            this.rbPrimaryClaimsBatch_SentBatch.Tag = "Batch";
            this.rbPrimaryClaimsBatch_SentBatch.Text = "Primary Claims";
            this.rbPrimaryClaimsBatch_SentBatch.UseVisualStyleBackColor = true;
            this.rbPrimaryClaimsBatch_SentBatch.CheckedChanged += new System.EventHandler(this.rbAllFilters_CheckedChanged);
            // 
            // label2_SentBatch
            // 
            this.label2_SentBatch.BackColor = System.Drawing.Color.Transparent;
            this.label2_SentBatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2_SentBatch.Location = new System.Drawing.Point(92, 1);
            this.label2_SentBatch.Name = "label2_SentBatch";
            this.label2_SentBatch.Size = new System.Drawing.Size(10, 22);
            this.label2_SentBatch.TabIndex = 71;
            this.label2_SentBatch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkBatchClaimCount_SentBatch
            // 
            this.chkBatchClaimCount_SentBatch.AutoSize = true;
            this.chkBatchClaimCount_SentBatch.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkBatchClaimCount_SentBatch.Location = new System.Drawing.Point(844, 1);
            this.chkBatchClaimCount_SentBatch.Name = "chkBatchClaimCount_SentBatch";
            this.chkBatchClaimCount_SentBatch.Size = new System.Drawing.Size(175, 22);
            this.chkBatchClaimCount_SentBatch.TabIndex = 66;
            this.chkBatchClaimCount_SentBatch.Tag = "Batch";
            this.chkBatchClaimCount_SentBatch.Text = "Show all for selected batch";
            this.chkBatchClaimCount_SentBatch.UseVisualStyleBackColor = true;
            this.chkBatchClaimCount_SentBatch.Visible = false;
            // 
            // numBatchClaimCount_SentBatch
            // 
            this.numBatchClaimCount_SentBatch.Dock = System.Windows.Forms.DockStyle.Right;
            this.numBatchClaimCount_SentBatch.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBatchClaimCount_SentBatch.Location = new System.Drawing.Point(1019, 1);
            this.numBatchClaimCount_SentBatch.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBatchClaimCount_SentBatch.Name = "numBatchClaimCount_SentBatch";
            this.numBatchClaimCount_SentBatch.Size = new System.Drawing.Size(53, 22);
            this.numBatchClaimCount_SentBatch.TabIndex = 65;
            this.numBatchClaimCount_SentBatch.Tag = "Batch";
            this.numBatchClaimCount_SentBatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numBatchClaimCount_SentBatch.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBatchClaimCount_SentBatch.Visible = false;
            // 
            // chkBatchGeneralSearch_SentBatch
            // 
            this.chkBatchGeneralSearch_SentBatch.AutoSize = true;
            this.chkBatchGeneralSearch_SentBatch.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkBatchGeneralSearch_SentBatch.Location = new System.Drawing.Point(1072, 1);
            this.chkBatchGeneralSearch_SentBatch.Name = "chkBatchGeneralSearch_SentBatch";
            this.chkBatchGeneralSearch_SentBatch.Size = new System.Drawing.Size(175, 22);
            this.chkBatchGeneralSearch_SentBatch.TabIndex = 74;
            this.chkBatchGeneralSearch_SentBatch.Tag = "Batch";
            this.chkBatchGeneralSearch_SentBatch.Text = "Show all for selected batch";
            this.chkBatchGeneralSearch_SentBatch.UseVisualStyleBackColor = true;
            this.chkBatchGeneralSearch_SentBatch.Visible = false;
            // 
            // lblSearhBatch_SentBatch
            // 
            this.lblSearhBatch_SentBatch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearhBatch_SentBatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearhBatch_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearhBatch_SentBatch.Location = new System.Drawing.Point(1, 1);
            this.lblSearhBatch_SentBatch.Name = "lblSearhBatch_SentBatch";
            this.lblSearhBatch_SentBatch.Size = new System.Drawing.Size(91, 22);
            this.lblSearhBatch_SentBatch.TabIndex = 6;
            this.lblSearhBatch_SentBatch.Text = "Batch Type :";
            this.lblSearhBatch_SentBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label99
            // 
            this.label99.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label99.Dock = System.Windows.Forms.DockStyle.Left;
            this.label99.Location = new System.Drawing.Point(0, 1);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(1, 22);
            this.label99.TabIndex = 21;
            // 
            // label100
            // 
            this.label100.BackColor = System.Drawing.Color.Transparent;
            this.label100.Dock = System.Windows.Forms.DockStyle.Right;
            this.label100.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label100.Location = new System.Drawing.Point(1247, 1);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(10, 22);
            this.label100.TabIndex = 60;
            this.label100.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label101
            // 
            this.label101.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label101.Dock = System.Windows.Forms.DockStyle.Top;
            this.label101.Location = new System.Drawing.Point(0, 0);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(1257, 1);
            this.label101.TabIndex = 19;
            // 
            // label102
            // 
            this.label102.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label102.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label102.Location = new System.Drawing.Point(0, 23);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(1257, 1);
            this.label102.TabIndex = 20;
            // 
            // label103
            // 
            this.label103.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label103.Dock = System.Windows.Forms.DockStyle.Right;
            this.label103.Location = new System.Drawing.Point(1257, 0);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(1, 24);
            this.label103.TabIndex = 22;
            // 
            // pnlProgressBar_SentBatch
            // 
            this.pnlProgressBar_SentBatch.Controls.Add(this.lblFileCounter_SentBatch);
            this.pnlProgressBar_SentBatch.Controls.Add(this.lblFile_SentBatch);
            this.pnlProgressBar_SentBatch.Controls.Add(this.prgFileGeneration_SentBatch);
            this.pnlProgressBar_SentBatch.Controls.Add(this.label21_SentBatch);
            this.pnlProgressBar_SentBatch.Location = new System.Drawing.Point(353, 46);
            this.pnlProgressBar_SentBatch.Name = "pnlProgressBar_SentBatch";
            this.pnlProgressBar_SentBatch.Size = new System.Drawing.Size(952, 19);
            this.pnlProgressBar_SentBatch.TabIndex = 28;
            this.pnlProgressBar_SentBatch.Visible = false;
            // 
            // lblFileCounter_SentBatch
            // 
            this.lblFileCounter_SentBatch.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblFileCounter_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileCounter_SentBatch.Location = new System.Drawing.Point(555, 10);
            this.lblFileCounter_SentBatch.Name = "lblFileCounter_SentBatch";
            this.lblFileCounter_SentBatch.Size = new System.Drawing.Size(121, 9);
            this.lblFileCounter_SentBatch.TabIndex = 24;
            this.lblFileCounter_SentBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFile_SentBatch
            // 
            this.lblFile_SentBatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFile_SentBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFile_SentBatch.Location = new System.Drawing.Point(0, 10);
            this.lblFile_SentBatch.Name = "lblFile_SentBatch";
            this.lblFile_SentBatch.Size = new System.Drawing.Size(286, 9);
            this.lblFile_SentBatch.TabIndex = 1;
            this.lblFile_SentBatch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // prgFileGeneration_SentBatch
            // 
            this.prgFileGeneration_SentBatch.Dock = System.Windows.Forms.DockStyle.Right;
            this.prgFileGeneration_SentBatch.Location = new System.Drawing.Point(676, 10);
            this.prgFileGeneration_SentBatch.Name = "prgFileGeneration_SentBatch";
            this.prgFileGeneration_SentBatch.Size = new System.Drawing.Size(276, 9);
            this.prgFileGeneration_SentBatch.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgFileGeneration_SentBatch.TabIndex = 0;
            // 
            // label21_SentBatch
            // 
            this.label21_SentBatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label21_SentBatch.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21_SentBatch.Location = new System.Drawing.Point(0, 0);
            this.label21_SentBatch.Name = "label21_SentBatch";
            this.label21_SentBatch.Padding = new System.Windows.Forms.Padding(3);
            this.label21_SentBatch.Size = new System.Drawing.Size(952, 10);
            this.label21_SentBatch.TabIndex = 23;
            // 
            // tbpg_ClaimManager
            // 
            this.tbpg_ClaimManager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_ClaimManager.Controls.Add(this.pnlGrids);
            this.tbpg_ClaimManager.Controls.Add(this.panel23);
            this.tbpg_ClaimManager.Controls.Add(this.pnlClaimHeader);
            this.tbpg_ClaimManager.ImageIndex = 15;
            this.tbpg_ClaimManager.Location = new System.Drawing.Point(4, 23);
            this.tbpg_ClaimManager.Name = "tbpg_ClaimManager";
            this.tbpg_ClaimManager.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_ClaimManager.Size = new System.Drawing.Size(1264, 909);
            this.tbpg_ClaimManager.TabIndex = 9;
            this.tbpg_ClaimManager.Tag = "Claim Manager";
            this.tbpg_ClaimManager.Text = "Claim Manager";
            // 
            // pnlGrids
            // 
            this.pnlGrids.Controls.Add(this.pnlSubBatch);
            this.pnlGrids.Controls.Add(this.pnl_ProgressBar);
            this.pnlGrids.Controls.Add(this.pnllblBillingMethodValue);
            this.pnlGrids.Controls.Add(this.splitter6);
            this.pnlGrids.Controls.Add(this.pnlAllBatch);
            this.pnlGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrids.Location = new System.Drawing.Point(3, 64);
            this.pnlGrids.Name = "pnlGrids";
            this.pnlGrids.Size = new System.Drawing.Size(1258, 842);
            this.pnlGrids.TabIndex = 31;
            // 
            // pnlSubBatch
            // 
            this.pnlSubBatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSubBatch.Controls.Add(this.c1SubBatch);
            this.pnlSubBatch.Controls.Add(this.pnlSearchBatchClaim);
            this.pnlSubBatch.Controls.Add(this.label138);
            this.pnlSubBatch.Controls.Add(this.label139);
            this.pnlSubBatch.Controls.Add(this.label140);
            this.pnlSubBatch.Controls.Add(this.label141);
            this.pnlSubBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSubBatch.Location = new System.Drawing.Point(217, 0);
            this.pnlSubBatch.Name = "pnlSubBatch";
            this.pnlSubBatch.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlSubBatch.Size = new System.Drawing.Size(1041, 795);
            this.pnlSubBatch.TabIndex = 33;
            // 
            // c1SubBatch
            // 
            this.c1SubBatch.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1SubBatch.BackColor = System.Drawing.Color.White;
            this.c1SubBatch.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SubBatch.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1SubBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SubBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1SubBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SubBatch.Location = new System.Drawing.Point(1, 25);
            this.c1SubBatch.Name = "c1SubBatch";
            this.c1SubBatch.Padding = new System.Windows.Forms.Padding(3);
            this.c1SubBatch.Rows.Count = 1;
            this.c1SubBatch.Rows.DefaultSize = 19;
            this.c1SubBatch.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SubBatch.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1SubBatch.Size = new System.Drawing.Size(1039, 766);
            this.c1SubBatch.StyleInfo = resources.GetString("c1SubBatch.StyleInfo");
            this.c1SubBatch.TabIndex = 27;
            this.c1SubBatch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1SubBatch_MouseDown);
            this.c1SubBatch.MouseLeave += new System.EventHandler(this.c1SubBatch_MouseLeave);
            this.c1SubBatch.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1SubBatch_MouseMove);
            // 
            // pnlSearchBatchClaim
            // 
            this.pnlSearchBatchClaim.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearchBatchClaim.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlSearchBatchClaim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchBatchClaim.Controls.Add(this.panel24);
            this.pnlSearchBatchClaim.Controls.Add(this.lblSearchSubBatch);
            this.pnlSearchBatchClaim.Controls.Add(this.label137);
            this.pnlSearchBatchClaim.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchBatchClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearchBatchClaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlSearchBatchClaim.Location = new System.Drawing.Point(1, 1);
            this.pnlSearchBatchClaim.Name = "pnlSearchBatchClaim";
            this.pnlSearchBatchClaim.Size = new System.Drawing.Size(1039, 24);
            this.pnlSearchBatchClaim.TabIndex = 30;
            // 
            // panel24
            // 
            this.panel24.BackColor = System.Drawing.Color.Transparent;
            this.panel24.Controls.Add(this.txtSearchSubBatch);
            this.panel24.Controls.Add(this.label131);
            this.panel24.Controls.Add(this.label132);
            this.panel24.Controls.Add(this.label133);
            this.panel24.Controls.Add(this.btn_ClearC1SubBatch);
            this.panel24.Controls.Add(this.label134);
            this.panel24.Controls.Add(this.label135);
            this.panel24.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel24.ForeColor = System.Drawing.Color.Black;
            this.panel24.Location = new System.Drawing.Point(74, 0);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(212, 23);
            this.panel24.TabIndex = 32;
            // 
            // txtSearchSubBatch
            // 
            this.txtSearchSubBatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchSubBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchSubBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchSubBatch.ForeColor = System.Drawing.Color.Black;
            this.txtSearchSubBatch.Location = new System.Drawing.Point(6, 3);
            this.txtSearchSubBatch.Name = "txtSearchSubBatch";
            this.txtSearchSubBatch.Size = new System.Drawing.Size(184, 15);
            this.txtSearchSubBatch.TabIndex = 15;
            this.txtSearchSubBatch.Tag = "Claim Manager";
            this.txtSearchSubBatch.TextChanged += new System.EventHandler(this.txtSearchSubBatch_TextChanged);
            // 
            // label131
            // 
            this.label131.BackColor = System.Drawing.Color.White;
            this.label131.Dock = System.Windows.Forms.DockStyle.Left;
            this.label131.Location = new System.Drawing.Point(1, 3);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(5, 15);
            this.label131.TabIndex = 42;
            // 
            // label132
            // 
            this.label132.BackColor = System.Drawing.Color.White;
            this.label132.Dock = System.Windows.Forms.DockStyle.Top;
            this.label132.Location = new System.Drawing.Point(1, 0);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(189, 3);
            this.label132.TabIndex = 37;
            // 
            // label133
            // 
            this.label133.BackColor = System.Drawing.Color.White;
            this.label133.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label133.Location = new System.Drawing.Point(1, 18);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(189, 5);
            this.label133.TabIndex = 38;
            // 
            // btn_ClearC1SubBatch
            // 
            this.btn_ClearC1SubBatch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ClearC1SubBatch.BackgroundImage")));
            this.btn_ClearC1SubBatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ClearC1SubBatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ClearC1SubBatch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_ClearC1SubBatch.FlatAppearance.BorderSize = 0;
            this.btn_ClearC1SubBatch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_ClearC1SubBatch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_ClearC1SubBatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClearC1SubBatch.Image = ((System.Drawing.Image)(resources.GetObject("btn_ClearC1SubBatch.Image")));
            this.btn_ClearC1SubBatch.Location = new System.Drawing.Point(190, 0);
            this.btn_ClearC1SubBatch.Name = "btn_ClearC1SubBatch";
            this.btn_ClearC1SubBatch.Size = new System.Drawing.Size(21, 23);
            this.btn_ClearC1SubBatch.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btn_ClearC1SubBatch, "Clear Search");
            this.btn_ClearC1SubBatch.UseVisualStyleBackColor = true;
            this.btn_ClearC1SubBatch.Click += new System.EventHandler(this.btn_ClearC1SubBatch_Click);
            // 
            // label134
            // 
            this.label134.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label134.Dock = System.Windows.Forms.DockStyle.Right;
            this.label134.Location = new System.Drawing.Point(211, 0);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(1, 23);
            this.label134.TabIndex = 40;
            this.label134.Text = "label4";
            // 
            // label135
            // 
            this.label135.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label135.Dock = System.Windows.Forms.DockStyle.Left;
            this.label135.Location = new System.Drawing.Point(0, 0);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(1, 23);
            this.label135.TabIndex = 39;
            this.label135.Text = "label4";
            // 
            // lblSearchSubBatch
            // 
            this.lblSearchSubBatch.AutoSize = true;
            this.lblSearchSubBatch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchSubBatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearchSubBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchSubBatch.Location = new System.Drawing.Point(0, 0);
            this.lblSearchSubBatch.Name = "lblSearchSubBatch";
            this.lblSearchSubBatch.Padding = new System.Windows.Forms.Padding(10, 4, 4, 0);
            this.lblSearchSubBatch.Size = new System.Drawing.Size(74, 18);
            this.lblSearchSubBatch.TabIndex = 6;
            this.lblSearchSubBatch.Text = "Search  :";
            this.lblSearchSubBatch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label137
            // 
            this.label137.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label137.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label137.Location = new System.Drawing.Point(0, 23);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(1039, 1);
            this.label137.TabIndex = 20;
            // 
            // label138
            // 
            this.label138.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label138.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label138.Location = new System.Drawing.Point(1, 791);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(1039, 1);
            this.label138.TabIndex = 25;
            // 
            // label139
            // 
            this.label139.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label139.Dock = System.Windows.Forms.DockStyle.Top;
            this.label139.Location = new System.Drawing.Point(1, 0);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(1039, 1);
            this.label139.TabIndex = 24;
            // 
            // label140
            // 
            this.label140.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label140.Dock = System.Windows.Forms.DockStyle.Right;
            this.label140.Location = new System.Drawing.Point(1040, 0);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(1, 792);
            this.label140.TabIndex = 23;
            // 
            // label141
            // 
            this.label141.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label141.Dock = System.Windows.Forms.DockStyle.Left;
            this.label141.Location = new System.Drawing.Point(0, 0);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(1, 792);
            this.label141.TabIndex = 22;
            // 
            // pnl_ProgressBar
            // 
            this.pnl_ProgressBar.Controls.Add(this.prgProcess);
            this.pnl_ProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_ProgressBar.Location = new System.Drawing.Point(217, 795);
            this.pnl_ProgressBar.Name = "pnl_ProgressBar";
            this.pnl_ProgressBar.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnl_ProgressBar.Size = new System.Drawing.Size(1041, 22);
            this.pnl_ProgressBar.TabIndex = 34;
            this.pnl_ProgressBar.Visible = false;
            // 
            // prgProcess
            // 
            this.prgProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgProcess.Location = new System.Drawing.Point(3, 0);
            this.prgProcess.Name = "prgProcess";
            this.prgProcess.Size = new System.Drawing.Size(1035, 19);
            this.prgProcess.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgProcess.TabIndex = 0;
            // 
            // pnllblBillingMethodValue
            // 
            this.pnllblBillingMethodValue.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnllblBillingMethodValue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnllblBillingMethodValue.Controls.Add(this.lblBillingMethodValue_ClaimManager);
            this.pnllblBillingMethodValue.Controls.Add(this.lblBillingMethod_ClaimManager);
            this.pnllblBillingMethodValue.Controls.Add(this.lblBatchDateValue_ClaimManager);
            this.pnllblBillingMethodValue.Controls.Add(this.lblBatchDate_ClaimManager);
            this.pnllblBillingMethodValue.Controls.Add(this.lblClaimAmtValue_ClaimManager);
            this.pnllblBillingMethodValue.Controls.Add(this.lblClaimAmt_ClaimManager);
            this.pnllblBillingMethodValue.Controls.Add(this.lblClaimCountValue_ClaimManager);
            this.pnllblBillingMethodValue.Controls.Add(this.lblNumClaim_ClaimManger);
            this.pnllblBillingMethodValue.Controls.Add(this.label152);
            this.pnllblBillingMethodValue.Controls.Add(this.label153);
            this.pnllblBillingMethodValue.Controls.Add(this.label154);
            this.pnllblBillingMethodValue.Controls.Add(this.label155);
            this.pnllblBillingMethodValue.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnllblBillingMethodValue.Location = new System.Drawing.Point(217, 817);
            this.pnllblBillingMethodValue.Name = "pnllblBillingMethodValue";
            this.pnllblBillingMethodValue.Size = new System.Drawing.Size(1041, 25);
            this.pnllblBillingMethodValue.TabIndex = 21;
            // 
            // lblBillingMethodValue_ClaimManager
            // 
            this.lblBillingMethodValue_ClaimManager.AutoEllipsis = true;
            this.lblBillingMethodValue_ClaimManager.AutoSize = true;
            this.lblBillingMethodValue_ClaimManager.BackColor = System.Drawing.Color.Transparent;
            this.lblBillingMethodValue_ClaimManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBillingMethodValue_ClaimManager.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBillingMethodValue_ClaimManager.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBillingMethodValue_ClaimManager.Location = new System.Drawing.Point(498, 1);
            this.lblBillingMethodValue_ClaimManager.Name = "lblBillingMethodValue_ClaimManager";
            this.lblBillingMethodValue_ClaimManager.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblBillingMethodValue_ClaimManager.Size = new System.Drawing.Size(224, 18);
            this.lblBillingMethodValue_ClaimManager.TabIndex = 35;
            this.lblBillingMethodValue_ClaimManager.Text = "Electronic Professional ANSI [5010]";
            // 
            // lblBillingMethod_ClaimManager
            // 
            this.lblBillingMethod_ClaimManager.AutoSize = true;
            this.lblBillingMethod_ClaimManager.BackColor = System.Drawing.Color.Transparent;
            this.lblBillingMethod_ClaimManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBillingMethod_ClaimManager.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillingMethod_ClaimManager.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBillingMethod_ClaimManager.Location = new System.Drawing.Point(422, 1);
            this.lblBillingMethod_ClaimManager.Name = "lblBillingMethod_ClaimManager";
            this.lblBillingMethod_ClaimManager.Padding = new System.Windows.Forms.Padding(15, 4, 0, 0);
            this.lblBillingMethod_ClaimManager.Size = new System.Drawing.Size(76, 18);
            this.lblBillingMethod_ClaimManager.TabIndex = 34;
            this.lblBillingMethod_ClaimManager.Text = "Method : ";
            // 
            // lblBatchDateValue_ClaimManager
            // 
            this.lblBatchDateValue_ClaimManager.AutoEllipsis = true;
            this.lblBatchDateValue_ClaimManager.AutoSize = true;
            this.lblBatchDateValue_ClaimManager.BackColor = System.Drawing.Color.Transparent;
            this.lblBatchDateValue_ClaimManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBatchDateValue_ClaimManager.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBatchDateValue_ClaimManager.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBatchDateValue_ClaimManager.Location = new System.Drawing.Point(386, 1);
            this.lblBatchDateValue_ClaimManager.Name = "lblBatchDateValue_ClaimManager";
            this.lblBatchDateValue_ClaimManager.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblBatchDateValue_ClaimManager.Size = new System.Drawing.Size(36, 18);
            this.lblBatchDateValue_ClaimManager.TabIndex = 31;
            this.lblBatchDateValue_ClaimManager.Text = "Date";
            // 
            // lblBatchDate_ClaimManager
            // 
            this.lblBatchDate_ClaimManager.AutoSize = true;
            this.lblBatchDate_ClaimManager.BackColor = System.Drawing.Color.Transparent;
            this.lblBatchDate_ClaimManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBatchDate_ClaimManager.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchDate_ClaimManager.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBatchDate_ClaimManager.Location = new System.Drawing.Point(291, 1);
            this.lblBatchDate_ClaimManager.Name = "lblBatchDate_ClaimManager";
            this.lblBatchDate_ClaimManager.Padding = new System.Windows.Forms.Padding(15, 4, 0, 0);
            this.lblBatchDate_ClaimManager.Size = new System.Drawing.Size(95, 18);
            this.lblBatchDate_ClaimManager.TabIndex = 29;
            this.lblBatchDate_ClaimManager.Text = "Batch Date : ";
            // 
            // lblClaimAmtValue_ClaimManager
            // 
            this.lblClaimAmtValue_ClaimManager.AutoEllipsis = true;
            this.lblClaimAmtValue_ClaimManager.AutoSize = true;
            this.lblClaimAmtValue_ClaimManager.BackColor = System.Drawing.Color.Transparent;
            this.lblClaimAmtValue_ClaimManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblClaimAmtValue_ClaimManager.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblClaimAmtValue_ClaimManager.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClaimAmtValue_ClaimManager.Location = new System.Drawing.Point(253, 1);
            this.lblClaimAmtValue_ClaimManager.Name = "lblClaimAmtValue_ClaimManager";
            this.lblClaimAmtValue_ClaimManager.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblClaimAmtValue_ClaimManager.Size = new System.Drawing.Size(38, 18);
            this.lblClaimAmtValue_ClaimManager.TabIndex = 30;
            this.lblClaimAmtValue_ClaimManager.Text = "Total";
            // 
            // lblClaimAmt_ClaimManager
            // 
            this.lblClaimAmt_ClaimManager.AutoEllipsis = true;
            this.lblClaimAmt_ClaimManager.AutoSize = true;
            this.lblClaimAmt_ClaimManager.BackColor = System.Drawing.Color.Transparent;
            this.lblClaimAmt_ClaimManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblClaimAmt_ClaimManager.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimAmt_ClaimManager.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClaimAmt_ClaimManager.Location = new System.Drawing.Point(129, 1);
            this.lblClaimAmt_ClaimManager.Name = "lblClaimAmt_ClaimManager";
            this.lblClaimAmt_ClaimManager.Padding = new System.Windows.Forms.Padding(15, 4, 0, 0);
            this.lblClaimAmt_ClaimManager.Size = new System.Drawing.Size(124, 18);
            this.lblClaimAmt_ClaimManager.TabIndex = 28;
            this.lblClaimAmt_ClaimManager.Text = "Total Claim Amt. : ";
            // 
            // lblClaimCountValue_ClaimManager
            // 
            this.lblClaimCountValue_ClaimManager.AutoEllipsis = true;
            this.lblClaimCountValue_ClaimManager.AutoSize = true;
            this.lblClaimCountValue_ClaimManager.BackColor = System.Drawing.Color.Transparent;
            this.lblClaimCountValue_ClaimManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblClaimCountValue_ClaimManager.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblClaimCountValue_ClaimManager.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClaimCountValue_ClaimManager.Location = new System.Drawing.Point(90, 1);
            this.lblClaimCountValue_ClaimManager.Name = "lblClaimCountValue_ClaimManager";
            this.lblClaimCountValue_ClaimManager.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblClaimCountValue_ClaimManager.Size = new System.Drawing.Size(39, 18);
            this.lblClaimCountValue_ClaimManager.TabIndex = 27;
            this.lblClaimCountValue_ClaimManager.Text = "Claim";
            // 
            // lblNumClaim_ClaimManger
            // 
            this.lblNumClaim_ClaimManger.AutoEllipsis = true;
            this.lblNumClaim_ClaimManger.AutoSize = true;
            this.lblNumClaim_ClaimManger.BackColor = System.Drawing.Color.Transparent;
            this.lblNumClaim_ClaimManger.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNumClaim_ClaimManger.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumClaim_ClaimManger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblNumClaim_ClaimManger.Location = new System.Drawing.Point(1, 1);
            this.lblNumClaim_ClaimManger.Name = "lblNumClaim_ClaimManger";
            this.lblNumClaim_ClaimManger.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblNumClaim_ClaimManger.Size = new System.Drawing.Size(89, 18);
            this.lblNumClaim_ClaimManger.TabIndex = 26;
            this.lblNumClaim_ClaimManger.Text = "No. of Claims : ";
            // 
            // label152
            // 
            this.label152.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label152.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label152.Location = new System.Drawing.Point(1, 24);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(1039, 1);
            this.label152.TabIndex = 25;
            // 
            // label153
            // 
            this.label153.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label153.Dock = System.Windows.Forms.DockStyle.Top;
            this.label153.Location = new System.Drawing.Point(1, 0);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(1039, 1);
            this.label153.TabIndex = 24;
            // 
            // label154
            // 
            this.label154.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label154.Dock = System.Windows.Forms.DockStyle.Right;
            this.label154.Location = new System.Drawing.Point(1040, 0);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(1, 25);
            this.label154.TabIndex = 23;
            // 
            // label155
            // 
            this.label155.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label155.Dock = System.Windows.Forms.DockStyle.Left;
            this.label155.Location = new System.Drawing.Point(0, 0);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(1, 25);
            this.label155.TabIndex = 22;
            // 
            // splitter6
            // 
            this.splitter6.Location = new System.Drawing.Point(214, 0);
            this.splitter6.Name = "splitter6";
            this.splitter6.Size = new System.Drawing.Size(3, 842);
            this.splitter6.TabIndex = 32;
            this.splitter6.TabStop = false;
            // 
            // pnlAllBatch
            // 
            this.pnlAllBatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlAllBatch.Controls.Add(this.c1AllBatch);
            this.pnlAllBatch.Controls.Add(this.pnltxtSearchAllBatch);
            this.pnlAllBatch.Controls.Add(this.label160);
            this.pnlAllBatch.Controls.Add(this.label161);
            this.pnlAllBatch.Controls.Add(this.label162);
            this.pnlAllBatch.Controls.Add(this.label163);
            this.pnlAllBatch.Controls.Add(this.pnlCrossClaimSearch);
            this.pnlAllBatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAllBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAllBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlAllBatch.Location = new System.Drawing.Point(0, 0);
            this.pnlAllBatch.Name = "pnlAllBatch";
            this.pnlAllBatch.Size = new System.Drawing.Size(214, 842);
            this.pnlAllBatch.TabIndex = 29;
            // 
            // c1AllBatch
            // 
            this.c1AllBatch.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1AllBatch.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1AllBatch.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1AllBatch.BackColor = System.Drawing.Color.White;
            this.c1AllBatch.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1AllBatch.ColumnInfo = "1,0,0,0,0,95,Columns:0{Width:33;Name:\"COL_IMAGE\";Style:\"TextAlign:CenterCenter;\";" +
    "StyleFixed:\"ImageAlign:LeftCenter;\";}\t";
            this.c1AllBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1AllBatch.ExtendLastCol = true;
            this.c1AllBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1AllBatch.ForeColor = System.Drawing.Color.Black;
            this.c1AllBatch.Location = new System.Drawing.Point(1, 53);
            this.c1AllBatch.Name = "c1AllBatch";
            this.c1AllBatch.Rows.Count = 1;
            this.c1AllBatch.Rows.DefaultSize = 19;
            this.c1AllBatch.Rows.Fixed = 0;
            this.c1AllBatch.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1AllBatch.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1AllBatch.Size = new System.Drawing.Size(212, 788);
            this.c1AllBatch.StyleInfo = resources.GetString("c1AllBatch.StyleInfo");
            this.c1AllBatch.TabIndex = 35;
            this.c1AllBatch.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            this.c1AllBatch.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1AllBatch_AfterSelChange);
            this.c1AllBatch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1AllBatch_MouseDown);
            // 
            // pnltxtSearchAllBatch
            // 
            this.pnltxtSearchAllBatch.BackColor = System.Drawing.Color.Transparent;
            this.pnltxtSearchAllBatch.Controls.Add(this.txtSearchAllBatch);
            this.pnltxtSearchAllBatch.Controls.Add(this.label156);
            this.pnltxtSearchAllBatch.Controls.Add(this.label157);
            this.pnltxtSearchAllBatch.Controls.Add(this.btn_ClearC1AllBatch);
            this.pnltxtSearchAllBatch.Controls.Add(this.pictureBox2);
            this.pnltxtSearchAllBatch.Controls.Add(this.label158);
            this.pnltxtSearchAllBatch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltxtSearchAllBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnltxtSearchAllBatch.ForeColor = System.Drawing.Color.Black;
            this.pnltxtSearchAllBatch.Location = new System.Drawing.Point(1, 29);
            this.pnltxtSearchAllBatch.Name = "pnltxtSearchAllBatch";
            this.pnltxtSearchAllBatch.Size = new System.Drawing.Size(212, 24);
            this.pnltxtSearchAllBatch.TabIndex = 32;
            // 
            // txtSearchAllBatch
            // 
            this.txtSearchAllBatch.BackColor = System.Drawing.SystemColors.Window;
            this.txtSearchAllBatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchAllBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchAllBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchAllBatch.ForeColor = System.Drawing.Color.Black;
            this.txtSearchAllBatch.Location = new System.Drawing.Point(28, 3);
            this.txtSearchAllBatch.Name = "txtSearchAllBatch";
            this.txtSearchAllBatch.Size = new System.Drawing.Size(163, 15);
            this.txtSearchAllBatch.TabIndex = 42;
            this.txtSearchAllBatch.Tag = "Claim Manager";
            this.txtSearchAllBatch.TextChanged += new System.EventHandler(this.txtSearchAllBatch_TextChanged);
            this.txtSearchAllBatch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchAllBatch_KeyDown);
            // 
            // label156
            // 
            this.label156.BackColor = System.Drawing.Color.White;
            this.label156.Dock = System.Windows.Forms.DockStyle.Top;
            this.label156.Location = new System.Drawing.Point(28, 0);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(163, 3);
            this.label156.TabIndex = 37;
            // 
            // label157
            // 
            this.label157.BackColor = System.Drawing.Color.White;
            this.label157.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label157.Location = new System.Drawing.Point(28, 18);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(163, 5);
            this.label157.TabIndex = 38;
            // 
            // btn_ClearC1AllBatch
            // 
            this.btn_ClearC1AllBatch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ClearC1AllBatch.BackgroundImage")));
            this.btn_ClearC1AllBatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ClearC1AllBatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ClearC1AllBatch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_ClearC1AllBatch.FlatAppearance.BorderSize = 0;
            this.btn_ClearC1AllBatch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_ClearC1AllBatch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_ClearC1AllBatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClearC1AllBatch.Image = ((System.Drawing.Image)(resources.GetObject("btn_ClearC1AllBatch.Image")));
            this.btn_ClearC1AllBatch.Location = new System.Drawing.Point(191, 0);
            this.btn_ClearC1AllBatch.Name = "btn_ClearC1AllBatch";
            this.btn_ClearC1AllBatch.Size = new System.Drawing.Size(21, 23);
            this.btn_ClearC1AllBatch.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btn_ClearC1AllBatch, "Clear Search");
            this.btn_ClearC1AllBatch.UseVisualStyleBackColor = true;
            this.btn_ClearC1AllBatch.Click += new System.EventHandler(this.btn_ClearC1AllBatch_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(28, 23);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // label158
            // 
            this.label158.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label158.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label158.Location = new System.Drawing.Point(0, 23);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(212, 1);
            this.label158.TabIndex = 35;
            this.label158.Text = "label1";
            // 
            // label160
            // 
            this.label160.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label160.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label160.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label160.Location = new System.Drawing.Point(1, 841);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(212, 1);
            this.label160.TabIndex = 4;
            this.label160.Text = "label2";
            // 
            // label161
            // 
            this.label161.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label161.Dock = System.Windows.Forms.DockStyle.Left;
            this.label161.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label161.Location = new System.Drawing.Point(0, 29);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(1, 813);
            this.label161.TabIndex = 3;
            this.label161.Text = "label4";
            // 
            // label162
            // 
            this.label162.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label162.Dock = System.Windows.Forms.DockStyle.Right;
            this.label162.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label162.Location = new System.Drawing.Point(213, 29);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(1, 813);
            this.label162.TabIndex = 2;
            this.label162.Text = "label3";
            // 
            // label163
            // 
            this.label163.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label163.Dock = System.Windows.Forms.DockStyle.Top;
            this.label163.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label163.Location = new System.Drawing.Point(0, 28);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(214, 1);
            this.label163.TabIndex = 0;
            this.label163.Text = "label1";
            // 
            // pnlCrossClaimSearch
            // 
            this.pnlCrossClaimSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlCrossClaimSearch.Controls.Add(this.chkCrossClaimSearch);
            this.pnlCrossClaimSearch.Controls.Add(this.label111);
            this.pnlCrossClaimSearch.Controls.Add(this.label107);
            this.pnlCrossClaimSearch.Controls.Add(this.label105);
            this.pnlCrossClaimSearch.Controls.Add(this.label92);
            this.pnlCrossClaimSearch.Controls.Add(this.label75);
            this.pnlCrossClaimSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCrossClaimSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCrossClaimSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlCrossClaimSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlCrossClaimSearch.Name = "pnlCrossClaimSearch";
            this.pnlCrossClaimSearch.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlCrossClaimSearch.Size = new System.Drawing.Size(214, 28);
            this.pnlCrossClaimSearch.TabIndex = 36;
            // 
            // chkCrossClaimSearch
            // 
            this.chkCrossClaimSearch.AutoSize = true;
            this.chkCrossClaimSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCrossClaimSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkCrossClaimSearch.Location = new System.Drawing.Point(9, 1);
            this.chkCrossClaimSearch.Name = "chkCrossClaimSearch";
            this.chkCrossClaimSearch.Padding = new System.Windows.Forms.Padding(3);
            this.chkCrossClaimSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkCrossClaimSearch.Size = new System.Drawing.Size(204, 23);
            this.chkCrossClaimSearch.TabIndex = 74;
            this.chkCrossClaimSearch.Tag = "Queue";
            this.chkCrossClaimSearch.Text = "Search Batches by Claim#";
            this.chkCrossClaimSearch.UseVisualStyleBackColor = true;
            this.chkCrossClaimSearch.CheckedChanged += new System.EventHandler(this.chkCrossClaimSearch_CheckedChanged);
            // 
            // label111
            // 
            this.label111.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label111.Dock = System.Windows.Forms.DockStyle.Left;
            this.label111.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.Location = new System.Drawing.Point(1, 1);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(8, 23);
            this.label111.TabIndex = 75;
            // 
            // label107
            // 
            this.label107.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label107.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label107.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(1, 24);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(212, 1);
            this.label107.TabIndex = 7;
            this.label107.Text = "label1";
            // 
            // label105
            // 
            this.label105.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label105.Dock = System.Windows.Forms.DockStyle.Top;
            this.label105.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.Location = new System.Drawing.Point(1, 0);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(212, 1);
            this.label105.TabIndex = 6;
            this.label105.Text = "label1";
            // 
            // label92
            // 
            this.label92.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label92.Dock = System.Windows.Forms.DockStyle.Right;
            this.label92.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.Location = new System.Drawing.Point(213, 0);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(1, 25);
            this.label92.TabIndex = 5;
            this.label92.Text = "label4";
            // 
            // label75
            // 
            this.label75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label75.Dock = System.Windows.Forms.DockStyle.Left;
            this.label75.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(0, 0);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(1, 25);
            this.label75.TabIndex = 4;
            this.label75.Text = "label4";
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.panel35);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel23.Location = new System.Drawing.Point(3, 32);
            this.panel23.Name = "panel23";
            this.panel23.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel23.Size = new System.Drawing.Size(1258, 32);
            this.panel23.TabIndex = 49;
            // 
            // panel35
            // 
            this.panel35.BackColor = System.Drawing.Color.Transparent;
            this.panel35.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel35.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel35.Controls.Add(this.rbSubmitted);
            this.panel35.Controls.Add(this.rbToBeSubmitted);
            this.panel35.Controls.Add(this.label164);
            this.panel35.Controls.Add(this.label166);
            this.panel35.Controls.Add(this.label167);
            this.panel35.Controls.Add(this.label168);
            this.panel35.Controls.Add(this.label169);
            this.panel35.Controls.Add(this.label170);
            this.panel35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel35.Location = new System.Drawing.Point(0, 3);
            this.panel35.Name = "panel35";
            this.panel35.Size = new System.Drawing.Size(1258, 26);
            this.panel35.TabIndex = 48;
            // 
            // rbSubmitted
            // 
            this.rbSubmitted.AutoSize = true;
            this.rbSubmitted.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbSubmitted.Location = new System.Drawing.Point(149, 1);
            this.rbSubmitted.Name = "rbSubmitted";
            this.rbSubmitted.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.rbSubmitted.Size = new System.Drawing.Size(92, 24);
            this.rbSubmitted.TabIndex = 73;
            this.rbSubmitted.Tag = "Batch";
            this.rbSubmitted.Text = "Submitted";
            this.rbSubmitted.UseVisualStyleBackColor = true;
            this.rbSubmitted.CheckedChanged += new System.EventHandler(this.rbSubmitted_CheckedChanged);
            // 
            // rbToBeSubmitted
            // 
            this.rbToBeSubmitted.AutoSize = true;
            this.rbToBeSubmitted.Checked = true;
            this.rbToBeSubmitted.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbToBeSubmitted.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbToBeSubmitted.Location = new System.Drawing.Point(11, 1);
            this.rbToBeSubmitted.Name = "rbToBeSubmitted";
            this.rbToBeSubmitted.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.rbToBeSubmitted.Size = new System.Drawing.Size(138, 24);
            this.rbToBeSubmitted.TabIndex = 72;
            this.rbToBeSubmitted.TabStop = true;
            this.rbToBeSubmitted.Tag = "Batch";
            this.rbToBeSubmitted.Text = "To Be Submitted";
            this.rbToBeSubmitted.UseVisualStyleBackColor = true;
            this.rbToBeSubmitted.CheckedChanged += new System.EventHandler(this.rbToBeSubmitted_CheckedChanged);
            // 
            // label164
            // 
            this.label164.BackColor = System.Drawing.Color.Transparent;
            this.label164.Dock = System.Windows.Forms.DockStyle.Left;
            this.label164.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label164.Location = new System.Drawing.Point(1, 1);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(10, 24);
            this.label164.TabIndex = 71;
            this.label164.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label166
            // 
            this.label166.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label166.Dock = System.Windows.Forms.DockStyle.Left;
            this.label166.Location = new System.Drawing.Point(0, 1);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(1, 24);
            this.label166.TabIndex = 21;
            // 
            // label167
            // 
            this.label167.BackColor = System.Drawing.Color.Transparent;
            this.label167.Dock = System.Windows.Forms.DockStyle.Right;
            this.label167.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label167.Location = new System.Drawing.Point(1247, 1);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(10, 24);
            this.label167.TabIndex = 60;
            this.label167.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label168
            // 
            this.label168.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label168.Dock = System.Windows.Forms.DockStyle.Top;
            this.label168.Location = new System.Drawing.Point(0, 0);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(1257, 1);
            this.label168.TabIndex = 19;
            // 
            // label169
            // 
            this.label169.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label169.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label169.Location = new System.Drawing.Point(0, 25);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(1257, 1);
            this.label169.TabIndex = 20;
            // 
            // label170
            // 
            this.label170.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label170.Dock = System.Windows.Forms.DockStyle.Right;
            this.label170.Location = new System.Drawing.Point(1257, 0);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(1, 26);
            this.label170.TabIndex = 22;
            // 
            // pnlClaimHeader
            // 
            this.pnlClaimHeader.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.pnlClaimHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlClaimHeader.Controls.Add(this.label150);
            this.pnlClaimHeader.Controls.Add(this.label129);
            this.pnlClaimHeader.Controls.Add(this.pnllblStatements);
            this.pnlClaimHeader.Controls.Add(this.pnllbl837PaperClaims);
            this.pnlClaimHeader.Controls.Add(this.label171);
            this.pnlClaimHeader.Controls.Add(this.pnllbl837ElectronicClaims);
            this.pnlClaimHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlClaimHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlClaimHeader.Name = "pnlClaimHeader";
            this.pnlClaimHeader.Size = new System.Drawing.Size(1258, 29);
            this.pnlClaimHeader.TabIndex = 32;
            // 
            // label150
            // 
            this.label150.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label150.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label150.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label150.Location = new System.Drawing.Point(681, 28);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(576, 1);
            this.label150.TabIndex = 49;
            this.label150.Text = "label1";
            // 
            // label129
            // 
            this.label129.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label129.Dock = System.Windows.Forms.DockStyle.Top;
            this.label129.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label129.Location = new System.Drawing.Point(681, 0);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(576, 1);
            this.label129.TabIndex = 48;
            this.label129.Text = "label1";
            // 
            // pnllblStatements
            // 
            this.pnllblStatements.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnllblStatements.BackgroundImage")));
            this.pnllblStatements.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnllblStatements.Controls.Add(this.label149);
            this.pnllblStatements.Controls.Add(this.label151);
            this.pnllblStatements.Controls.Add(this.label159);
            this.pnllblStatements.Controls.Add(this.lblStatements);
            this.pnllblStatements.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnllblStatements.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnllblStatements.Location = new System.Drawing.Point(454, 0);
            this.pnllblStatements.Name = "pnllblStatements";
            this.pnllblStatements.Size = new System.Drawing.Size(227, 29);
            this.pnllblStatements.TabIndex = 47;
            // 
            // label149
            // 
            this.label149.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label149.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label149.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label149.Location = new System.Drawing.Point(0, 28);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(226, 1);
            this.label149.TabIndex = 13;
            this.label149.Text = "label2";
            // 
            // label151
            // 
            this.label151.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label151.Dock = System.Windows.Forms.DockStyle.Right;
            this.label151.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label151.Location = new System.Drawing.Point(226, 1);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(1, 28);
            this.label151.TabIndex = 11;
            this.label151.Text = "label3";
            // 
            // label159
            // 
            this.label159.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label159.Dock = System.Windows.Forms.DockStyle.Top;
            this.label159.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label159.Location = new System.Drawing.Point(0, 0);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(227, 1);
            this.label159.TabIndex = 10;
            this.label159.Text = "label1";
            // 
            // lblStatements
            // 
            this.lblStatements.BackColor = System.Drawing.Color.Transparent;
            this.lblStatements.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStatements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatements.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblStatements.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatements.ForeColor = System.Drawing.Color.White;
            this.lblStatements.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatements.Location = new System.Drawing.Point(0, 0);
            this.lblStatements.Name = "lblStatements";
            this.lblStatements.Size = new System.Drawing.Size(227, 29);
            this.lblStatements.TabIndex = 9;
            this.lblStatements.Text = "Statements";
            this.lblStatements.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatements.Click += new System.EventHandler(this.lblStatements_Click);
            // 
            // pnllbl837PaperClaims
            // 
            this.pnllbl837PaperClaims.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnllbl837PaperClaims.BackgroundImage")));
            this.pnllbl837PaperClaims.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnllbl837PaperClaims.Controls.Add(this.label136);
            this.pnllbl837PaperClaims.Controls.Add(this.label146);
            this.pnllbl837PaperClaims.Controls.Add(this.label147);
            this.pnllbl837PaperClaims.Controls.Add(this.label148);
            this.pnllbl837PaperClaims.Controls.Add(this.lbl837PaperClaims);
            this.pnllbl837PaperClaims.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnllbl837PaperClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnllbl837PaperClaims.Location = new System.Drawing.Point(227, 0);
            this.pnllbl837PaperClaims.Name = "pnllbl837PaperClaims";
            this.pnllbl837PaperClaims.Size = new System.Drawing.Size(227, 29);
            this.pnllbl837PaperClaims.TabIndex = 46;
            // 
            // label136
            // 
            this.label136.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label136.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label136.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label136.Location = new System.Drawing.Point(1, 28);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(225, 1);
            this.label136.TabIndex = 13;
            this.label136.Text = "label2";
            // 
            // label146
            // 
            this.label146.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label146.Dock = System.Windows.Forms.DockStyle.Left;
            this.label146.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label146.Location = new System.Drawing.Point(0, 1);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(1, 28);
            this.label146.TabIndex = 12;
            this.label146.Text = "label4";
            // 
            // label147
            // 
            this.label147.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label147.Dock = System.Windows.Forms.DockStyle.Right;
            this.label147.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label147.Location = new System.Drawing.Point(226, 1);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(1, 28);
            this.label147.TabIndex = 11;
            this.label147.Text = "label3";
            // 
            // label148
            // 
            this.label148.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label148.Dock = System.Windows.Forms.DockStyle.Top;
            this.label148.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label148.Location = new System.Drawing.Point(0, 0);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(227, 1);
            this.label148.TabIndex = 10;
            this.label148.Text = "label1";
            // 
            // lbl837PaperClaims
            // 
            this.lbl837PaperClaims.BackColor = System.Drawing.Color.Transparent;
            this.lbl837PaperClaims.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl837PaperClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl837PaperClaims.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl837PaperClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl837PaperClaims.ForeColor = System.Drawing.Color.White;
            this.lbl837PaperClaims.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl837PaperClaims.Location = new System.Drawing.Point(0, 0);
            this.lbl837PaperClaims.Name = "lbl837PaperClaims";
            this.lbl837PaperClaims.Size = new System.Drawing.Size(227, 29);
            this.lbl837PaperClaims.TabIndex = 9;
            this.lbl837PaperClaims.Text = "Paper Claims";
            this.lbl837PaperClaims.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl837PaperClaims.Click += new System.EventHandler(this.lbl837PaperClaims_Click);
            // 
            // label171
            // 
            this.label171.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label171.Dock = System.Windows.Forms.DockStyle.Right;
            this.label171.Location = new System.Drawing.Point(1257, 0);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(1, 29);
            this.label171.TabIndex = 47;
            // 
            // pnllbl837ElectronicClaims
            // 
            this.pnllbl837ElectronicClaims.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
            this.pnllbl837ElectronicClaims.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnllbl837ElectronicClaims.Controls.Add(this.label127);
            this.pnllbl837ElectronicClaims.Controls.Add(this.label128);
            this.pnllbl837ElectronicClaims.Controls.Add(this.label130);
            this.pnllbl837ElectronicClaims.Controls.Add(this.lbl837ElectronicClaims);
            this.pnllbl837ElectronicClaims.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnllbl837ElectronicClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnllbl837ElectronicClaims.Location = new System.Drawing.Point(0, 0);
            this.pnllbl837ElectronicClaims.Name = "pnllbl837ElectronicClaims";
            this.pnllbl837ElectronicClaims.Size = new System.Drawing.Size(227, 29);
            this.pnllbl837ElectronicClaims.TabIndex = 45;
            // 
            // label127
            // 
            this.label127.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label127.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label127.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label127.Location = new System.Drawing.Point(1, 28);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(226, 1);
            this.label127.TabIndex = 13;
            this.label127.Text = "label2";
            // 
            // label128
            // 
            this.label128.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label128.Dock = System.Windows.Forms.DockStyle.Left;
            this.label128.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label128.Location = new System.Drawing.Point(0, 1);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(1, 28);
            this.label128.TabIndex = 12;
            this.label128.Text = "label4";
            // 
            // label130
            // 
            this.label130.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label130.Dock = System.Windows.Forms.DockStyle.Top;
            this.label130.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label130.Location = new System.Drawing.Point(0, 0);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(227, 1);
            this.label130.TabIndex = 10;
            this.label130.Text = "label1";
            // 
            // lbl837ElectronicClaims
            // 
            this.lbl837ElectronicClaims.BackColor = System.Drawing.Color.Transparent;
            this.lbl837ElectronicClaims.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl837ElectronicClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl837ElectronicClaims.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl837ElectronicClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl837ElectronicClaims.ForeColor = System.Drawing.Color.Black;
            this.lbl837ElectronicClaims.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl837ElectronicClaims.Location = new System.Drawing.Point(0, 0);
            this.lbl837ElectronicClaims.Name = "lbl837ElectronicClaims";
            this.lbl837ElectronicClaims.Size = new System.Drawing.Size(227, 29);
            this.lbl837ElectronicClaims.TabIndex = 9;
            this.lbl837ElectronicClaims.Text = "  837 Electronic Claims";
            this.lbl837ElectronicClaims.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl837ElectronicClaims.Click += new System.EventHandler(this.lbl837ElectronicClaims_Click);
            // 
            // tbpg_OnHold
            // 
            this.tbpg_OnHold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_OnHold.Controls.Add(this.pnlPlanHold);
            this.tbpg_OnHold.Controls.Add(this.pnlBillingHold);
            this.tbpg_OnHold.Controls.Add(this.splitter3);
            this.tbpg_OnHold.Controls.Add(this.pnlPlanHoldLeft);
            this.tbpg_OnHold.Controls.Add(this.SearchPanel);
            this.tbpg_OnHold.ImageIndex = 12;
            this.tbpg_OnHold.Location = new System.Drawing.Point(4, 23);
            this.tbpg_OnHold.Name = "tbpg_OnHold";
            this.tbpg_OnHold.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tbpg_OnHold.Size = new System.Drawing.Size(1264, 909);
            this.tbpg_OnHold.TabIndex = 7;
            this.tbpg_OnHold.Tag = "OnHold";
            this.tbpg_OnHold.Text = "On Hold";
            // 
            // pnlPlanHold
            // 
            this.pnlPlanHold.Controls.Add(this.pnlc1PlanHold);
            this.pnlPlanHold.Controls.Add(this.splitter2);
            this.pnlPlanHold.Controls.Add(this.pnlC1PlanHoldclaim);
            this.pnlPlanHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlanHold.Location = new System.Drawing.Point(260, 27);
            this.pnlPlanHold.Name = "pnlPlanHold";
            this.pnlPlanHold.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlPlanHold.Size = new System.Drawing.Size(1004, 882);
            this.pnlPlanHold.TabIndex = 1;
            // 
            // pnlc1PlanHold
            // 
            this.pnlc1PlanHold.Controls.Add(this.label6);
            this.pnlc1PlanHold.Controls.Add(this.label26);
            this.pnlc1PlanHold.Controls.Add(this.label5);
            this.pnlc1PlanHold.Controls.Add(this.c1PlanHold);
            this.pnlc1PlanHold.Controls.Add(this.label3);
            this.pnlc1PlanHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlc1PlanHold.Location = new System.Drawing.Point(0, 3);
            this.pnlc1PlanHold.Name = "pnlc1PlanHold";
            this.pnlc1PlanHold.Size = new System.Drawing.Size(1004, 726);
            this.pnlc1PlanHold.TabIndex = 559;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(1003, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 724);
            this.label6.TabIndex = 557;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Location = new System.Drawing.Point(0, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 724);
            this.label26.TabIndex = 556;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1004, 1);
            this.label5.TabIndex = 227;
            // 
            // c1PlanHold
            // 
            this.c1PlanHold.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PlanHold.AllowEditing = false;
            this.c1PlanHold.BackColor = System.Drawing.Color.White;
            this.c1PlanHold.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PlanHold.ColumnInfo = "6,0,0,0,0,95,Columns:0{Width:200;}\t1{Width:200;}\t2{Width:100;}\t3{Width:200;}\t4{Wi" +
    "dth:100;}\t5{Width:100;}\t";
            this.c1PlanHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PlanHold.ExtendLastCol = true;
            this.c1PlanHold.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PlanHold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PlanHold.Location = new System.Drawing.Point(0, 0);
            this.c1PlanHold.Name = "c1PlanHold";
            this.c1PlanHold.Padding = new System.Windows.Forms.Padding(3);
            this.c1PlanHold.Rows.Count = 1;
            this.c1PlanHold.Rows.DefaultSize = 19;
            this.c1PlanHold.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PlanHold.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1PlanHold.ShowCellLabels = true;
            this.c1PlanHold.Size = new System.Drawing.Size(1004, 725);
            this.c1PlanHold.StyleInfo = resources.GetString("c1PlanHold.StyleInfo");
            this.c1PlanHold.TabIndex = 0;
            this.c1PlanHold.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1PlanHold_BeforeSort);
            this.c1PlanHold.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1PlanHold_AfterSort);
            this.c1PlanHold.RowColChange += new System.EventHandler(this.c1PlanHold_RowColChange);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(0, 725);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1004, 1);
            this.label3.TabIndex = 226;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 729);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1004, 3);
            this.splitter2.TabIndex = 561;
            this.splitter2.TabStop = false;
            // 
            // pnlC1PlanHoldclaim
            // 
            this.pnlC1PlanHoldclaim.Controls.Add(this.label36);
            this.pnlC1PlanHoldclaim.Controls.Add(this.label35);
            this.pnlC1PlanHoldclaim.Controls.Add(this.label31);
            this.pnlC1PlanHoldclaim.Controls.Add(this.label28);
            this.pnlC1PlanHoldclaim.Controls.Add(this.C1PlanHoldclaim);
            this.pnlC1PlanHoldclaim.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlC1PlanHoldclaim.Location = new System.Drawing.Point(0, 732);
            this.pnlC1PlanHoldclaim.Name = "pnlC1PlanHoldclaim";
            this.pnlC1PlanHoldclaim.Size = new System.Drawing.Size(1004, 150);
            this.pnlC1PlanHoldclaim.TabIndex = 559;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label36.Location = new System.Drawing.Point(1, 149);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1002, 1);
            this.label36.TabIndex = 561;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Top;
            this.label35.Location = new System.Drawing.Point(1, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1002, 1);
            this.label35.TabIndex = 560;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Right;
            this.label31.Location = new System.Drawing.Point(1003, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 150);
            this.label31.TabIndex = 559;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 150);
            this.label28.TabIndex = 558;
            // 
            // C1PlanHoldclaim
            // 
            this.C1PlanHoldclaim.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1PlanHoldclaim.AllowEditing = false;
            this.C1PlanHoldclaim.BackColor = System.Drawing.Color.White;
            this.C1PlanHoldclaim.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1PlanHoldclaim.ColumnInfo = "4,0,0,0,0,95,Columns:0{Width:100;}\t1{Width:100;}\t2{Width:300;}\t3{Width:150;}\t";
            this.C1PlanHoldclaim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1PlanHoldclaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1PlanHoldclaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1PlanHoldclaim.Location = new System.Drawing.Point(0, 0);
            this.C1PlanHoldclaim.Name = "C1PlanHoldclaim";
            this.C1PlanHoldclaim.Padding = new System.Windows.Forms.Padding(3);
            this.C1PlanHoldclaim.Rows.Count = 1;
            this.C1PlanHoldclaim.Rows.DefaultSize = 19;
            this.C1PlanHoldclaim.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1PlanHoldclaim.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.C1PlanHoldclaim.ShowCellLabels = true;
            this.C1PlanHoldclaim.Size = new System.Drawing.Size(1004, 150);
            this.C1PlanHoldclaim.StyleInfo = resources.GetString("C1PlanHoldclaim.StyleInfo");
            this.C1PlanHoldclaim.TabIndex = 557;
            this.C1PlanHoldclaim.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.C1PlanHoldclaim_BeforeSort);
            this.C1PlanHoldclaim.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.C1PlanHoldclaim_AfterSort);
            this.C1PlanHoldclaim.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.C1PlanHoldclaim_MouseDoubleClick);
            // 
            // pnlBillingHold
            // 
            this.pnlBillingHold.Controls.Add(this.c1BillingHold);
            this.pnlBillingHold.Controls.Add(this.label29);
            this.pnlBillingHold.Controls.Add(this.label33);
            this.pnlBillingHold.Controls.Add(this.label32);
            this.pnlBillingHold.Controls.Add(this.label30);
            this.pnlBillingHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBillingHold.Location = new System.Drawing.Point(260, 27);
            this.pnlBillingHold.Name = "pnlBillingHold";
            this.pnlBillingHold.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlBillingHold.Size = new System.Drawing.Size(1004, 882);
            this.pnlBillingHold.TabIndex = 1;
            // 
            // c1BillingHold
            // 
            this.c1BillingHold.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1BillingHold.AllowEditing = false;
            this.c1BillingHold.BackColor = System.Drawing.Color.White;
            this.c1BillingHold.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1BillingHold.ColumnInfo = "7,0,0,0,0,95,Columns:0{Width:100;}\t1{Width:100;}\t2{Width:200;}\t3{Width:200;}\t4{Wi" +
    "dth:100;}\t5{Width:100;}\t6{Width:100;Style:\"Format:\"\"C2\"\";DataType:System.Decimal" +
    ";TextAlign:RightCenter;\";}\t";
            this.c1BillingHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1BillingHold.ExtendLastCol = true;
            this.c1BillingHold.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1BillingHold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1BillingHold.Location = new System.Drawing.Point(1, 4);
            this.c1BillingHold.Name = "c1BillingHold";
            this.c1BillingHold.Padding = new System.Windows.Forms.Padding(3);
            this.c1BillingHold.Rows.Count = 1;
            this.c1BillingHold.Rows.DefaultSize = 19;
            this.c1BillingHold.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1BillingHold.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1BillingHold.ShowCellLabels = true;
            this.c1BillingHold.Size = new System.Drawing.Size(1002, 877);
            this.c1BillingHold.StyleInfo = resources.GetString("c1BillingHold.StyleInfo");
            this.c1BillingHold.TabIndex = 0;
            this.c1BillingHold.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1BillingHold_BeforeSort);
            this.c1BillingHold.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1BillingHold_AfterSort);
            this.c1BillingHold.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1BillingHold_MouseDoubleClick);
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label29.Location = new System.Drawing.Point(1, 881);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1002, 1);
            this.label29.TabIndex = 25;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Left;
            this.label33.Location = new System.Drawing.Point(0, 4);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1, 878);
            this.label33.TabIndex = 22;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Location = new System.Drawing.Point(1003, 4);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 878);
            this.label32.TabIndex = 23;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Top;
            this.label30.Location = new System.Drawing.Point(0, 3);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1004, 1);
            this.label30.TabIndex = 24;
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(257, 27);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 882);
            this.splitter3.TabIndex = 35;
            this.splitter3.TabStop = false;
            // 
            // pnlPlanHoldLeft
            // 
            this.pnlPlanHoldLeft.Controls.Add(this.pnlgrdPlanHoldBalCalc);
            this.pnlPlanHoldLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPlanHoldLeft.Location = new System.Drawing.Point(0, 27);
            this.pnlPlanHoldLeft.Name = "pnlPlanHoldLeft";
            this.pnlPlanHoldLeft.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlPlanHoldLeft.Size = new System.Drawing.Size(257, 882);
            this.pnlPlanHoldLeft.TabIndex = 32;
            // 
            // pnlgrdPlanHoldBalCalc
            // 
            this.pnlgrdPlanHoldBalCalc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlgrdPlanHoldBalCalc.Controls.Add(this.c1HoldBalance);
            this.pnlgrdPlanHoldBalCalc.Controls.Add(this.panel12);
            this.pnlgrdPlanHoldBalCalc.Controls.Add(this.label4);
            this.pnlgrdPlanHoldBalCalc.Controls.Add(this.label37);
            this.pnlgrdPlanHoldBalCalc.Controls.Add(this.label38);
            this.pnlgrdPlanHoldBalCalc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlgrdPlanHoldBalCalc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlgrdPlanHoldBalCalc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlgrdPlanHoldBalCalc.Location = new System.Drawing.Point(0, 3);
            this.pnlgrdPlanHoldBalCalc.Name = "pnlgrdPlanHoldBalCalc";
            this.pnlgrdPlanHoldBalCalc.Size = new System.Drawing.Size(257, 879);
            this.pnlgrdPlanHoldBalCalc.TabIndex = 27;
            // 
            // c1HoldBalance
            // 
            this.c1HoldBalance.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1HoldBalance.AllowEditing = false;
            this.c1HoldBalance.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1HoldBalance.BackColor = System.Drawing.Color.White;
            this.c1HoldBalance.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1HoldBalance.ColumnInfo = resources.GetString("c1HoldBalance.ColumnInfo");
            this.c1HoldBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1HoldBalance.ExtendLastCol = true;
            this.c1HoldBalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1HoldBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1HoldBalance.Location = new System.Drawing.Point(1, 22);
            this.c1HoldBalance.Name = "c1HoldBalance";
            this.c1HoldBalance.Rows.Count = 2;
            this.c1HoldBalance.Rows.DefaultSize = 19;
            this.c1HoldBalance.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1HoldBalance.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1HoldBalance.ShowCellLabels = true;
            this.c1HoldBalance.Size = new System.Drawing.Size(255, 856);
            this.c1HoldBalance.StyleInfo = resources.GetString("c1HoldBalance.StyleInfo");
            this.c1HoldBalance.TabIndex = 24;
            this.c1HoldBalance.TabStop = false;
            // 
            // panel12
            // 
            this.panel12.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel12.Controls.Add(this.label27);
            this.panel12.Controls.Add(this.label52);
            this.panel12.Controls.Add(this.label59);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(1, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(255, 22);
            this.panel12.TabIndex = 31;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.label27.Location = new System.Drawing.Point(0, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(255, 1);
            this.label27.TabIndex = 27;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.Transparent;
            this.label52.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(0, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(255, 21);
            this.label52.TabIndex = 26;
            this.label52.Text = "  On Hold Total Balance";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label59.Location = new System.Drawing.Point(0, 21);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(255, 1);
            this.label59.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(1, 878);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(255, 1);
            this.label4.TabIndex = 26;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Left;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(0, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 879);
            this.label37.TabIndex = 3;
            this.label37.Text = "label4";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Right;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label38.Location = new System.Drawing.Point(256, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 879);
            this.label38.TabIndex = 1;
            this.label38.Text = "label3";
            // 
            // SearchPanel
            // 
            this.SearchPanel.BackColor = System.Drawing.Color.Transparent;
            this.SearchPanel.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.SearchPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SearchPanel.Controls.Add(this.rbBillingHold);
            this.SearchPanel.Controls.Add(this.rbPlanHold);
            this.SearchPanel.Controls.Add(this.numHoldClaimCount);
            this.SearchPanel.Controls.Add(this.txtSearchHoldClaims);
            this.SearchPanel.Controls.Add(this.label14);
            this.SearchPanel.Controls.Add(this.label15);
            this.SearchPanel.Controls.Add(this.label22);
            this.SearchPanel.Controls.Add(this.label23);
            this.SearchPanel.Controls.Add(this.label25);
            this.SearchPanel.Controls.Add(this.label24);
            this.SearchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SearchPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.SearchPanel.Location = new System.Drawing.Point(0, 3);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(1264, 24);
            this.SearchPanel.TabIndex = 0;
            this.SearchPanel.TabStop = true;
            // 
            // rbBillingHold
            // 
            this.rbBillingHold.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBillingHold.Location = new System.Drawing.Point(273, 1);
            this.rbBillingHold.Name = "rbBillingHold";
            this.rbBillingHold.Size = new System.Drawing.Size(114, 22);
            this.rbBillingHold.TabIndex = 1;
            this.rbBillingHold.TabStop = true;
            this.rbBillingHold.Tag = "BillingHold";
            this.rbBillingHold.Text = "Billing Hold";
            this.rbBillingHold.UseVisualStyleBackColor = true;
            this.rbBillingHold.CheckedChanged += new System.EventHandler(this.rbBillingHold_CheckedChanged);
            // 
            // rbPlanHold
            // 
            this.rbPlanHold.Location = new System.Drawing.Point(389, 3);
            this.rbPlanHold.Name = "rbPlanHold";
            this.rbPlanHold.Size = new System.Drawing.Size(108, 18);
            this.rbPlanHold.TabIndex = 2;
            this.rbPlanHold.Tag = "PlanHold";
            this.rbPlanHold.Text = "Plan Hold";
            this.rbPlanHold.UseVisualStyleBackColor = true;
            this.rbPlanHold.CheckedChanged += new System.EventHandler(this.rbPlanHold_CheckedChanged);
            // 
            // numHoldClaimCount
            // 
            this.numHoldClaimCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.numHoldClaimCount.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numHoldClaimCount.Location = new System.Drawing.Point(1200, 1);
            this.numHoldClaimCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numHoldClaimCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHoldClaimCount.Name = "numHoldClaimCount";
            this.numHoldClaimCount.Size = new System.Drawing.Size(53, 22);
            this.numHoldClaimCount.TabIndex = 3;
            this.numHoldClaimCount.Tag = "Billing Hold";
            this.numHoldClaimCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numHoldClaimCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numHoldClaimCount.ValueChanged += new System.EventHandler(this.numHoldClaimCount_ValueChanged);
            // 
            // txtSearchHoldClaims
            // 
            this.txtSearchHoldClaims.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtSearchHoldClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchHoldClaims.ForeColor = System.Drawing.Color.Black;
            this.txtSearchHoldClaims.Location = new System.Drawing.Point(75, 1);
            this.txtSearchHoldClaims.Name = "txtSearchHoldClaims";
            this.txtSearchHoldClaims.Size = new System.Drawing.Size(182, 22);
            this.txtSearchHoldClaims.TabIndex = 0;
            this.txtSearchHoldClaims.Tag = "OnHold";
            this.txtSearchHoldClaims.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 22);
            this.label14.TabIndex = 0;
            this.label14.Text = "Search :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Location = new System.Drawing.Point(0, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 22);
            this.label15.TabIndex = 21;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(1253, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(10, 22);
            this.label22.TabIndex = 60;
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1263, 1);
            this.label23.TabIndex = 19;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.Location = new System.Drawing.Point(1263, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 23);
            this.label25.TabIndex = 22;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(0, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1264, 1);
            this.label24.TabIndex = 20;
            // 
            // tbpg_Void
            // 
            this.tbpg_Void.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_Void.Controls.Add(this.panel17);
            this.tbpg_Void.Controls.Add(this.panel16);
            this.tbpg_Void.ImageIndex = 10;
            this.tbpg_Void.Location = new System.Drawing.Point(4, 23);
            this.tbpg_Void.Name = "tbpg_Void";
            this.tbpg_Void.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_Void.Size = new System.Drawing.Size(1264, 909);
            this.tbpg_Void.TabIndex = 5;
            this.tbpg_Void.Tag = "Void";
            this.tbpg_Void.Text = "Void";
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.c1VoidClaims);
            this.panel17.Controls.Add(this.label82);
            this.panel17.Controls.Add(this.label83);
            this.panel17.Controls.Add(this.label84);
            this.panel17.Controls.Add(this.label85);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel17.Location = new System.Drawing.Point(3, 27);
            this.panel17.Name = "panel17";
            this.panel17.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel17.Size = new System.Drawing.Size(1258, 879);
            this.panel17.TabIndex = 19;
            // 
            // c1VoidClaims
            // 
            this.c1VoidClaims.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1VoidClaims.BackColor = System.Drawing.Color.White;
            this.c1VoidClaims.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1VoidClaims.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1VoidClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1VoidClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1VoidClaims.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1VoidClaims.Location = new System.Drawing.Point(1, 4);
            this.c1VoidClaims.Name = "c1VoidClaims";
            this.c1VoidClaims.Padding = new System.Windows.Forms.Padding(3);
            this.c1VoidClaims.Rows.Count = 1;
            this.c1VoidClaims.Rows.DefaultSize = 19;
            this.c1VoidClaims.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1VoidClaims.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1VoidClaims.Size = new System.Drawing.Size(1256, 874);
            this.c1VoidClaims.StyleInfo = resources.GetString("c1VoidClaims.StyleInfo");
            this.c1VoidClaims.TabIndex = 26;
            this.c1VoidClaims.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1VoidClaims_BeforeSort);
            this.c1VoidClaims.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1VoidClaims_AfterSort);
            this.c1VoidClaims.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1VoidClaims_MouseDoubleClick);
            this.c1VoidClaims.MouseLeave += new System.EventHandler(this.c1VoidClaims_MouseLeave);
            this.c1VoidClaims.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1VoidClaims_MouseMove);
            // 
            // label82
            // 
            this.label82.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label82.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label82.Location = new System.Drawing.Point(1, 878);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(1256, 1);
            this.label82.TabIndex = 25;
            // 
            // label83
            // 
            this.label83.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label83.Dock = System.Windows.Forms.DockStyle.Top;
            this.label83.Location = new System.Drawing.Point(1, 3);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(1256, 1);
            this.label83.TabIndex = 24;
            // 
            // label84
            // 
            this.label84.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label84.Dock = System.Windows.Forms.DockStyle.Right;
            this.label84.Location = new System.Drawing.Point(1257, 3);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(1, 876);
            this.label84.TabIndex = 23;
            // 
            // label85
            // 
            this.label85.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label85.Dock = System.Windows.Forms.DockStyle.Left;
            this.label85.Location = new System.Drawing.Point(0, 3);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(1, 876);
            this.label85.TabIndex = 22;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.Transparent;
            this.panel16.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel16.Controls.Add(this.chkVoidClaimCount);
            this.panel16.Controls.Add(this.numVoidClaimCount);
            this.panel16.Controls.Add(this.chkVoidGeneralSearch);
            this.panel16.Controls.Add(this.txtVoidSearch);
            this.panel16.Controls.Add(this.lblVoidSearch);
            this.panel16.Controls.Add(this.label34);
            this.panel16.Controls.Add(this.label78);
            this.panel16.Controls.Add(this.label79);
            this.panel16.Controls.Add(this.label80);
            this.panel16.Controls.Add(this.label81);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel16.Location = new System.Drawing.Point(3, 3);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(1258, 24);
            this.panel16.TabIndex = 18;
            // 
            // chkVoidClaimCount
            // 
            this.chkVoidClaimCount.AutoSize = true;
            this.chkVoidClaimCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkVoidClaimCount.Location = new System.Drawing.Point(1019, 1);
            this.chkVoidClaimCount.Name = "chkVoidClaimCount";
            this.chkVoidClaimCount.Size = new System.Drawing.Size(175, 22);
            this.chkVoidClaimCount.TabIndex = 67;
            this.chkVoidClaimCount.Tag = "Void";
            this.chkVoidClaimCount.Text = "Show all for selected batch";
            this.chkVoidClaimCount.UseVisualStyleBackColor = true;
            this.chkVoidClaimCount.Visible = false;
            // 
            // numVoidClaimCount
            // 
            this.numVoidClaimCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.numVoidClaimCount.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numVoidClaimCount.Location = new System.Drawing.Point(1194, 1);
            this.numVoidClaimCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numVoidClaimCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numVoidClaimCount.Name = "numVoidClaimCount";
            this.numVoidClaimCount.Size = new System.Drawing.Size(53, 22);
            this.numVoidClaimCount.TabIndex = 65;
            this.numVoidClaimCount.Tag = "Void";
            this.numVoidClaimCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numVoidClaimCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numVoidClaimCount.ValueChanged += new System.EventHandler(this.numClaimCount_ValueChanged);
            // 
            // chkVoidGeneralSearch
            // 
            this.chkVoidGeneralSearch.AutoSize = true;
            this.chkVoidGeneralSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkVoidGeneralSearch.Location = new System.Drawing.Point(263, 1);
            this.chkVoidGeneralSearch.Name = "chkVoidGeneralSearch";
            this.chkVoidGeneralSearch.Padding = new System.Windows.Forms.Padding(3);
            this.chkVoidGeneralSearch.Size = new System.Drawing.Size(114, 22);
            this.chkVoidGeneralSearch.TabIndex = 62;
            this.chkVoidGeneralSearch.Tag = "Void";
            this.chkVoidGeneralSearch.Text = "General Search";
            this.chkVoidGeneralSearch.UseVisualStyleBackColor = true;
            this.chkVoidGeneralSearch.Visible = false;
            // 
            // txtVoidSearch
            // 
            this.txtVoidSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtVoidSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVoidSearch.ForeColor = System.Drawing.Color.Black;
            this.txtVoidSearch.Location = new System.Drawing.Point(81, 1);
            this.txtVoidSearch.Name = "txtVoidSearch";
            this.txtVoidSearch.Size = new System.Drawing.Size(182, 22);
            this.txtVoidSearch.TabIndex = 15;
            this.txtVoidSearch.Tag = "Void";
            this.txtVoidSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblVoidSearch
            // 
            this.lblVoidSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblVoidSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblVoidSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoidSearch.Location = new System.Drawing.Point(1, 1);
            this.lblVoidSearch.Name = "lblVoidSearch";
            this.lblVoidSearch.Size = new System.Drawing.Size(80, 22);
            this.lblVoidSearch.TabIndex = 6;
            this.lblVoidSearch.Text = "Search :";
            this.lblVoidSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Left;
            this.label34.Location = new System.Drawing.Point(0, 1);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1, 22);
            this.label34.TabIndex = 21;
            // 
            // label78
            // 
            this.label78.BackColor = System.Drawing.Color.Transparent;
            this.label78.Dock = System.Windows.Forms.DockStyle.Right;
            this.label78.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label78.Location = new System.Drawing.Point(1247, 1);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(10, 22);
            this.label78.TabIndex = 60;
            this.label78.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label79
            // 
            this.label79.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label79.Dock = System.Windows.Forms.DockStyle.Top;
            this.label79.Location = new System.Drawing.Point(0, 0);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(1257, 1);
            this.label79.TabIndex = 19;
            // 
            // label80
            // 
            this.label80.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label80.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label80.Location = new System.Drawing.Point(0, 23);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(1257, 1);
            this.label80.TabIndex = 20;
            // 
            // label81
            // 
            this.label81.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label81.Dock = System.Windows.Forms.DockStyle.Right;
            this.label81.Location = new System.Drawing.Point(1257, 0);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(1, 24);
            this.label81.TabIndex = 22;
            // 
            // C1SuperTooltipDx
            // 
            this.C1SuperTooltipDx.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltipDx.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // cntmnuUpdateBatchStatus
            // 
            this.cntmnuUpdateBatchStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cntmnuUpdateBatchStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItem_UpdateStatus,
            this.mnuItem_UpdateStatement,
            this.mnuItem_MarkBatchPrinted});
            this.cntmnuUpdateBatchStatus.Name = "cmnu_Appointment";
            this.cntmnuUpdateBatchStatus.Size = new System.Drawing.Size(317, 70);
            this.cntmnuUpdateBatchStatus.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cntmnuUpdateBatchStatus_ItemClicked);
            // 
            // mnuItem_UpdateStatus
            // 
            this.mnuItem_UpdateStatus.ForeColor = System.Drawing.Color.Black;
            this.mnuItem_UpdateStatus.Name = "mnuItem_UpdateStatus";
            this.mnuItem_UpdateStatus.Size = new System.Drawing.Size(316, 22);
            this.mnuItem_UpdateStatus.Text = "Change batch file status to \'SendToClearingHouse\'";
            // 
            // mnuItem_UpdateStatement
            // 
            this.mnuItem_UpdateStatement.Name = "mnuItem_UpdateStatement";
            this.mnuItem_UpdateStatement.Size = new System.Drawing.Size(316, 22);
            this.mnuItem_UpdateStatement.Text = "Change statement status to \'Submitted\'";
            this.mnuItem_UpdateStatement.Visible = false;
            // 
            // mnuItem_MarkBatchPrinted
            // 
            this.mnuItem_MarkBatchPrinted.Name = "mnuItem_MarkBatchPrinted";
            this.mnuItem_MarkBatchPrinted.Size = new System.Drawing.Size(316, 22);
            this.mnuItem_MarkBatchPrinted.Text = "Change Batch status to \'Printed\'";
            this.mnuItem_MarkBatchPrinted.Visible = false;
            // 
            // frmBillingBatch_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1272, 990);
            this.Controls.Add(this.tabManager);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBillingBatch_New";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBillingBatch_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBillingBatch_FormClosed);
            this.Load += new System.EventHandler(this.frmBillingBatch_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.cntmnuShow997.ResumeLayout(false);
            this.tabManager.ResumeLayout(false);
            this.tbpg_Charges.ResumeLayout(false);
            this.tbpg_Charges.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1QueuedClaims)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.pnlBusinessCenter.ResumeLayout(false);
            this.pnlBusinessCenter.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQueueClaimCount)).EndInit();
            this.tbpg_Batch.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1BatchGrid)).EndInit();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.pnlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1trvBatch)).EndInit();
            this.pnlBatchSearch.ResumeLayout(false);
            this.pnlBatchSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchClaimCount)).EndInit();
            this.pnlProgressBar.ResumeLayout(false);
            this.tbpg_SentBatch.ResumeLayout(false);
            this.panel9SentBatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1BatchGridSentBatch)).EndInit();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panel6SentBatch.ResumeLayout(false);
            this.panel6SentBatch.PerformLayout();
            this.pnlBase_SentBatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1trvBatch_SentBatch)).EndInit();
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel14_SentBatch.ResumeLayout(false);
            this.panel14_SentBatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchClaimCount_SentBatch)).EndInit();
            this.pnlProgressBar_SentBatch.ResumeLayout(false);
            this.tbpg_ClaimManager.ResumeLayout(false);
            this.pnlGrids.ResumeLayout(false);
            this.pnlSubBatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SubBatch)).EndInit();
            this.pnlSearchBatchClaim.ResumeLayout(false);
            this.pnlSearchBatchClaim.PerformLayout();
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.pnl_ProgressBar.ResumeLayout(false);
            this.pnllblBillingMethodValue.ResumeLayout(false);
            this.pnllblBillingMethodValue.PerformLayout();
            this.pnlAllBatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1AllBatch)).EndInit();
            this.pnltxtSearchAllBatch.ResumeLayout(false);
            this.pnltxtSearchAllBatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlCrossClaimSearch.ResumeLayout(false);
            this.pnlCrossClaimSearch.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel35.ResumeLayout(false);
            this.panel35.PerformLayout();
            this.pnlClaimHeader.ResumeLayout(false);
            this.pnllblStatements.ResumeLayout(false);
            this.pnllbl837PaperClaims.ResumeLayout(false);
            this.pnllbl837ElectronicClaims.ResumeLayout(false);
            this.tbpg_OnHold.ResumeLayout(false);
            this.pnlPlanHold.ResumeLayout(false);
            this.pnlc1PlanHold.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PlanHold)).EndInit();
            this.pnlC1PlanHoldclaim.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1PlanHoldclaim)).EndInit();
            this.pnlBillingHold.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1BillingHold)).EndInit();
            this.pnlPlanHoldLeft.ResumeLayout(false);
            this.pnlgrdPlanHoldBalCalc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1HoldBalance)).EndInit();
            this.panel12.ResumeLayout(false);
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHoldClaimCount)).EndInit();
            this.tbpg_Void.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1VoidClaims)).EndInit();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVoidClaimCount)).EndInit();
            this.cntmnuUpdateBatchStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ImageList imgLst;
        private System.Windows.Forms.ContextMenuStrip cntmnuShow997;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_Show997;
        private System.Windows.Forms.TabControl tabManager;
        private System.Windows.Forms.TabPage tbpg_Charges;
        public C1.Win.C1FlexGrid.C1FlexGrid c1QueuedClaims;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkQueueClaimCount;
        private System.Windows.Forms.NumericUpDown numQueueClaimCount;
        private System.Windows.Forms.CheckBox chkQueueGeneralSearch;
        private System.Windows.Forms.TextBox txtQueueSearch;
        private System.Windows.Forms.Label lblQueueSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tbpg_Batch;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Panel pnlProgressBar;
        private System.Windows.Forms.Label lblFileCounter;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.ProgressBar prgFileGeneration;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.TreeView trvBatch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbl_pnlBaseBottomBrd;
        private System.Windows.Forms.Label lbl_pnlBaseLeftBrd;
        private System.Windows.Forms.Label lbl_pnlBaseRightBrd;
        private System.Windows.Forms.Label lbl_pnlBaseTopBrd;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkBatchClaimCount;
        private System.Windows.Forms.NumericUpDown numBatchClaimCount;
        private System.Windows.Forms.CheckBox chkBatchGeneralSearch;
        private System.Windows.Forms.TextBox txtBatchSearch;
        private System.Windows.Forms.Label lblSearhBatch;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TabPage tbpg_Void;
        private System.Windows.Forms.Panel panel17;
        private C1.Win.C1FlexGrid.C1FlexGrid c1VoidClaims;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.CheckBox chkVoidClaimCount;
        private System.Windows.Forms.NumericUpDown numVoidClaimCount;
        private System.Windows.Forms.CheckBox chkVoidGeneralSearch;
        private System.Windows.Forms.TextBox txtVoidSearch;
        private System.Windows.Forms.Label lblVoidSearch;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.RadioButton rbSecondaryClaimsCharges;
        private System.Windows.Forms.RadioButton rbPrimaryClaimsCharges;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbSecondayClaimsBatch;
        private System.Windows.Forms.RadioButton rbPrimaryClaimsBatch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tbpg_OnHold;
        private System.Windows.Forms.Panel pnlBillingHold;
        private C1.Win.C1FlexGrid.C1FlexGrid c1BillingHold;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Panel SearchPanel;
        private System.Windows.Forms.RadioButton rbBillingHold;
        private System.Windows.Forms.RadioButton rbPlanHold;
        private System.Windows.Forms.NumericUpDown numHoldClaimCount;
        private System.Windows.Forms.TextBox txtSearchHoldClaims;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel pnlPlanHold;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PlanHold;
        private System.Windows.Forms.Panel pnlPlanHoldLeft;
        private System.Windows.Forms.Panel pnlgrdPlanHoldBalCalc;
        private C1.Win.C1FlexGrid.C1FlexGrid c1HoldBalance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Label label27;
        private C1.Win.C1FlexGrid.C1FlexGrid C1PlanHoldclaim;
        private System.Windows.Forms.Panel pnlc1PlanHold;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel pnlC1PlanHoldclaim;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label53;
        internal System.Windows.Forms.Button btnDown;
        internal System.Windows.Forms.Button cbtnUP;
        private System.Windows.Forms.ToolTip tooltip_Billing;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private C1.Win.C1FlexGrid.C1FlexGrid c1BatchGrid;
        private System.Windows.Forms.Label lblNumClaim;
        private System.Windows.Forms.Label lblClaimcountvalue;
        private System.Windows.Forms.Label lblClamamtvalue;
        private System.Windows.Forms.Label lblBatchDateValue;
        private System.Windows.Forms.Label lblcalimamtvalue;
        private System.Windows.Forms.Label lblBatchDate;
        private System.Windows.Forms.Label lblClearinghouseValue;
        private System.Windows.Forms.Label lblClearinghouse;
        private System.Windows.Forms.Label lblBillingmethodvalue;
        private System.Windows.Forms.Label lblBillingmethod;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.ComboBox cmbReportingCategory;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label lblInsurance;
        private System.Windows.Forms.Label lblClearingHouseCharges;
        private System.Windows.Forms.ComboBox cmbInsuranceCompany;
        private System.Windows.Forms.ComboBox cmbClearingHouse;
        private System.Windows.Forms.ComboBox cmbMultiChargesTray;
        internal System.Windows.Forms.Button btnBrowseUser;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.CheckBox chkSelloggedUser;
        private System.Windows.Forms.Label label54;
        internal System.Windows.Forms.Button btnClearUser;
        private System.Windows.Forms.ComboBox cmbUser;
        internal System.Windows.Forms.Button btnBrowseMultiChargesTray;
        internal System.Windows.Forms.Button btnClearMultiChargesTray;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.ComboBox cmbMultiFacility;
        private System.Windows.Forms.Label label57;
        internal System.Windows.Forms.Button btnClearMultiFacility;
        internal System.Windows.Forms.Button btnBrowseMultiFacility;
        internal System.Windows.Forms.Button button2;
        internal System.Windows.Forms.Button btnBrowseMultiProvider;
        private System.Windows.Forms.Panel pnlBusinessCenter;
        private System.Windows.Forms.ComboBox cmbBusinessCenter;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Panel panel7;
        public System.Windows.Forms.ComboBox cmbBillingMethod;
        private System.Windows.Forms.MaskedTextBox maskedCloseDate;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.CheckBox chkLstCloseDate;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage tbpg_SentBatch;
        private System.Windows.Forms.Panel panel9SentBatch;
        private C1.Win.C1FlexGrid.C1FlexGrid c1BatchGridSentBatch;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Panel panel6SentBatch;
        private System.Windows.Forms.Label lblBillingmethodvalue_SentBatch;
        private System.Windows.Forms.Label lblBillingmethod_SentBatch;
        private System.Windows.Forms.Label lblClearinghouseValue_SentBatch;
        private System.Windows.Forms.Label lblClearinghouse_SentBatch;
        private System.Windows.Forms.Label lblBatchDateValue_SentBatch;
        private System.Windows.Forms.Label lblcalimamtvalue_SentBatch;
        private System.Windows.Forms.Label lblBatchDate_SentBatch;
        private System.Windows.Forms.Label lblClamamtvalue_SentBatch;
        private System.Windows.Forms.Label lblClaimcountvalue_SentBatch;
        private System.Windows.Forms.Label lblNumClaim_SentBatch;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.Panel pnlBase_SentBatch;
        private System.Windows.Forms.TreeView trvBatch_SentBatch;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Panel panel14_SentBatch;
        private System.Windows.Forms.RadioButton rbSecondaryClaimsBatch_SentBatch;
        private System.Windows.Forms.RadioButton rbPrimaryClaimsBatch_SentBatch;
        private System.Windows.Forms.Label label2_SentBatch;
        private System.Windows.Forms.CheckBox chkBatchClaimCount_SentBatch;
        private System.Windows.Forms.NumericUpDown numBatchClaimCount_SentBatch;
        private System.Windows.Forms.CheckBox chkBatchGeneralSearch_SentBatch;
        private System.Windows.Forms.TextBox txtBatchSearch_SentBatch;
        private System.Windows.Forms.Label lblSearhBatch_SentBatch;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Panel pnlProgressBar_SentBatch;
        private System.Windows.Forms.Label lblFileCounter_SentBatch;
        private System.Windows.Forms.Label lblFile_SentBatch;
        private System.Windows.Forms.ProgressBar prgFileGeneration_SentBatch;
        private System.Windows.Forms.Label label21_SentBatch;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltipDx;
        private System.Windows.Forms.Panel panel14;
        internal System.Windows.Forms.Panel panel15;
        internal System.Windows.Forms.Label label69;
        internal System.Windows.Forms.Label label70;
        internal System.Windows.Forms.Button btnClearClaimSearchUnsent;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label74;
        internal System.Windows.Forms.Panel pnlBatchSearch;
        private System.Windows.Forms.TextBox txtSearchUnsentBatches;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.Button btnBatchUnsentSearchClear;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnlSearchBottomBrd;
        private System.Windows.Forms.Panel panel13;
        internal System.Windows.Forms.Panel panel18;
        internal System.Windows.Forms.Label label73;
        internal System.Windows.Forms.Label label86;
        internal System.Windows.Forms.Button btnClearClaimSearchSent;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label label106;
        private C1.Win.C1FlexGrid.C1FlexGrid c1trvBatch_SentBatch;
        internal System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.TextBox txtSearchSentBatches;
        internal System.Windows.Forms.Label label108;
        internal System.Windows.Forms.Label label109;
        internal System.Windows.Forms.Button btnSentBatchSearchClear;
        internal System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label110;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label72;
        private C1.Win.C1FlexGrid.C1FlexGrid c1trvBatch;
        private System.Windows.Forms.TabPage tbpg_ClaimManager;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_ApplyFilter;
        private System.Windows.Forms.ToolStripButton tsb_Modify;
        private System.Windows.Forms.ToolStripButton tsb_Print1500;
        private System.Windows.Forms.ToolStripButton tsb_PrintNew1500;
        private System.Windows.Forms.ToolStripButton tsb_WorkerComp;
        private System.Windows.Forms.ToolStripButton tsb_UB04;
        private System.Windows.Forms.ToolStripButton tsb_DeleteBatch;
        private System.Windows.Forms.ToolStripButton tsb_Delete;
        private System.Windows.Forms.ToolStripButton tsb_Void;
        private System.Windows.Forms.ToolStripButton tsb_Select;
        private System.Windows.Forms.ToolStripButton tsb_ReQueue;
        private System.Windows.Forms.ToolStripButton tsb_Queue;
        private System.Windows.Forms.ToolStripButton tsb_Validate;
        private System.Windows.Forms.ToolStripDropDownButton tsb_Batch;
        private System.Windows.Forms.ToolStripMenuItem tsb_Batch_New;
        private System.Windows.Forms.ToolStripMenuItem tsb_Batch_Existing;
        private System.Windows.Forms.ToolStripButton tsb_ValidateNBatch;
        private System.Windows.Forms.ToolStripButton tsb_Accept;
        private System.Windows.Forms.ToolStripDropDownButton tsb_Send;
        private System.Windows.Forms.ToolStripMenuItem tsb_Send_PaperClaim;
        private System.Windows.Forms.ToolStripMenuItem tsb_Send_ElectronicClaim;
        private System.Windows.Forms.ToolStripButton tsb_Finished;
        private System.Windows.Forms.ToolStripButton tsb_Reject;
        private System.Windows.Forms.ToolStripButton tsb_RejectBatch;
        private System.Windows.Forms.ToolStripButton tsb_BatchDetailReport;
        private System.Windows.Forms.ToolStripButton tsb_Resend;
        private System.Windows.Forms.ToolStripButton tsb_ClaimStatus;
        private System.Windows.Forms.ToolStripDropDownButton tsb_toSecondary;
        private System.Windows.Forms.ToolStripMenuItem tsb_Elec_Secondary;
        private System.Windows.Forms.Panel pnlGrids;
        private System.Windows.Forms.Panel pnlSubBatch;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SubBatch;
        private System.Windows.Forms.Panel pnlSearchBatchClaim;
        internal System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.TextBox txtSearchSubBatch;
        internal System.Windows.Forms.Label label131;
        internal System.Windows.Forms.Label label132;
        internal System.Windows.Forms.Label label133;
        internal System.Windows.Forms.Button btn_ClearC1SubBatch;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Label lblSearchSubBatch;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Splitter splitter6;
        private System.Windows.Forms.Panel pnlAllBatch;
        private C1.Win.C1FlexGrid.C1FlexGrid c1AllBatch;
        internal System.Windows.Forms.Panel pnltxtSearchAllBatch;
        internal System.Windows.Forms.Label label156;
        internal System.Windows.Forms.Label label157;
        internal System.Windows.Forms.Button btn_ClearC1AllBatch;
        internal System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.Label label160;
        private System.Windows.Forms.Label label161;
        private System.Windows.Forms.Label label162;
        private System.Windows.Forms.Label label163;
        internal System.Windows.Forms.Button btnUP;
        internal System.Windows.Forms.ToolStripButton tsb_View;
        internal System.Windows.Forms.ToolStripDropDownButton Tsb_BatchSend;
        private System.Windows.Forms.TextBox txtSearchAllBatch;
        internal System.Windows.Forms.ToolStripButton tsb_PrintForm;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        private System.Windows.Forms.ContextMenuStrip cntmnuUpdateBatchStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_UpdateStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_UpdateStatement;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_MarkBatchPrinted;
        private System.Windows.Forms.Panel pnl_ProgressBar;
        public  System.Windows.Forms.ProgressBar prgProcess;
        private System.Windows.Forms.Panel pnllblBillingMethodValue;
        private System.Windows.Forms.Label lblBillingMethodValue_ClaimManager;
        private System.Windows.Forms.Label lblBillingMethod_ClaimManager;
        private System.Windows.Forms.Label lblBatchDateValue_ClaimManager;
        private System.Windows.Forms.Label lblClaimAmtValue_ClaimManager;
        private System.Windows.Forms.Label lblBatchDate_ClaimManager;
        private System.Windows.Forms.Label lblClaimAmt_ClaimManager;
        private System.Windows.Forms.Label lblClaimCountValue_ClaimManager;
        private System.Windows.Forms.Label lblNumClaim_ClaimManger;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.Label label154;
        private System.Windows.Forms.Label label155;
        internal System.Windows.Forms.ToolStripButton tsb_PrintClaimForm;
        internal System.Windows.Forms.ToolStripButton tsb_PrintClaimData;
        private System.Windows.Forms.ToolStripButton tsb_Refresh;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        internal System.Windows.Forms.Panel pnllblStatements;
        private System.Windows.Forms.Label label149;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.Label label159;
        internal System.Windows.Forms.Label lblStatements;
        private System.Windows.Forms.Panel pnlClaimHeader;
        internal System.Windows.Forms.Panel pnllbl837PaperClaims;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Label label146;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.Label label148;
        internal System.Windows.Forms.Label lbl837PaperClaims;
        internal System.Windows.Forms.Panel pnllbl837ElectronicClaims;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.Label label130;
        internal System.Windows.Forms.Label lbl837ElectronicClaims;
        private System.Windows.Forms.Panel panel35;
        private System.Windows.Forms.RadioButton rbSubmitted;
        private System.Windows.Forms.RadioButton rbToBeSubmitted;
        private System.Windows.Forms.Label label164;
        private System.Windows.Forms.Label label166;
        private System.Windows.Forms.Label label167;
        private System.Windows.Forms.Label label168;
        private System.Windows.Forms.Label label169;
        private System.Windows.Forms.Label label170;
        private System.Windows.Forms.Label label150;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.Label label171;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.CheckBox chkCrossClaimSearch;
        internal System.Windows.Forms.Panel pnlCrossClaimSearch;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label label75;
    }
}